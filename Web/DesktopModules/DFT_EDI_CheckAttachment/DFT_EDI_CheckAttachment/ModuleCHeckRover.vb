Imports Microsoft.ApplicationBlocks.Data
Imports DFT.Dotnetnuke.ClassLibrary
Imports System.Data.SqlClient

Module ModuleCHeckRover
    Dim RollverConn As String = ConfigurationManager.ConnectionStrings("RollverConnection").ConnectionString
    Dim strConn As String = ConfigurationManager.ConnectionStrings("OriginConnection").ConnectionString


    Function CheckRollver(ByVal Company_Taxno As String, ByVal Country As String, ByVal Harmonized_no As String, ByVal CerNo As String) As Integer
        Try
            Dim strCommand As String
            strCommand = "SELECT transfer_date, tax_id, country, harmonized_no, certoforigin_no, CONVERT(varchar(8), certoforigin_date, 112) AS certoforigin_date, company_name_th, " & _
                         "company_name_en, goods_desc_th, goods_desc_en, models, country_code, CONVERT(varchar(8), DATEADD(yyyy, 2, certoforigin_date), 112) AS Cert_ExpiredDate " & _
                         "FROM dbo.tbl_certoforigin " & _
                         "WHERE (tax_id = '" & Company_Taxno & "' OR tax_id_old = '" & Company_Taxno & "') AND (LEFT(harmonized_no, 4)= '" & Left(Harmonized_no, 4) & "') AND (country_code = '" & Country & "') AND (certoforigin_no = '" & CerNo & "')"
            Dim ds As New DataSet
            ds = SqlHelper.ExecuteDataset(RollverConn, CommandType.Text, strCommand)
            If ds.Tables(0).Rows.Count > 0 Then
                'ตรวจสอบว่าค่าที่ได้มานี้ หมดอายุ หรือว่า ยังไม่หมด
                With ds.Tables(0).Rows(0)
                    '20120910<=20110609
                    If CommonUtility.Get_StringValue(.Item("Cert_ExpiredDate")) <= FunctionUtility.DMY2YMD(Today) Then
                        Return 2 'certoforigin หมดอายุ
                    Else
                        Return 1 'certoforigin ไม่หมดอายุ
                    End If
                End With
            Else
                Return 3 'ไม่พบข้อมูลต้นทุนที่ทำการกรอก
            End If
        Catch ex As Exception
            Return 3
        End Try
    End Function


    Function CheckCarTax(ByVal Company_Taxno As String) As Boolean
        Dim str_Command As String
        str_Command = "SELECT     company_taxno " & _
        "FROM DS_TaxCar " & _
        "WHERE (company_taxno = '" & Company_Taxno & "')"

        Dim ds As New DataSet
        ds = SqlHelper.ExecuteDataset(strConn, CommandType.Text, str_Command)

        If ds.Tables(0).Rows.Count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function


    'by rut ใส่เพิ่มเพื่อแก้ Site ที่หน้าฟอร์มได้
    Function CallSiteForm_DS() As DataSet
        Dim ds As New DataSet
        Dim strCommand As String
        strCommand = "SELECT * FROM site_plus " & _
                     "WHERE (active_status = 'Y') AND (type_site = 'A') AND (ds_status='Y') " & _
                     "ORDER BY site_code"
        ds = SqlHelper.ExecuteDataset(strConn, CommandType.Text, strCommand)

        Return ds
    End Function

    'check tariff ต้องอย่างน้อย 6 ตัวถึงจะบันทึกได้
    Function CheckTariff_num2_4(ByVal _Tariff As String) As Boolean
        Dim Check_return As Boolean = True

        Select Case _Tariff.Length
            Case 1, 2, 3, 4, 5
                Check_return = False
        End Select

        Return Check_return
    End Function

    Function CheckUse_imagesign(ByVal str_session As String, ByVal st_action As String, ByVal st_caseset As String, _
    ByVal st_copy As String, ByVal st_sign As String, ByVal st_ListSign As String) As String
        Dim F_str As String = "5" 'ไม่เข้า case ไหนเลย

        Select Case st_action
            Case Is <> "new" 'เป็น view or edit
                Select Case st_caseset
                    Case ""
                        Select Case st_ListSign 'check ว่า เลือกชื่อลายเซ็นหรือไม่
                            Case "Y"
                                F_str = "1"
                            Case "N"
                                F_str = "0"
                        End Select
                    Case "0"
                        'F_str = "0"
                        Select Case st_ListSign
                            Case "Y"
                                F_str = "1"
                            Case "N"
                                F_str = "0"
                        End Select
                    Case "1"
                        F_str = "1"
                    Case "3"
                        Select Case st_ListSign
                            Case "Y"
                                F_str = "1"
                            Case "N"
                                F_str = "0"
                        End Select
                End Select
            Case Is = "new"
                Select Case st_copy
                    Case "true"
                        Select Case st_caseset
                            Case ""
                                Select Case st_ListSign
                                    Case "Y"
                                        F_str = "1"
                                    Case "N"
                                        F_str = "0"
                                End Select
                            Case "0"
                                'F_str = "0"
                                Select Case st_ListSign
                                    Case "Y"
                                        F_str = "1"
                                    Case "N"
                                        F_str = "0"
                                End Select
                            Case "1"
                                F_str = "1"
                            Case "3"
                                F_str = "3" 'ตอน copy มา แต่ติ๊กเลือกชื่อกรรมการออก
                        End Select
                    Case Else
                        Select Case st_caseset
                            Case ""
                                Select Case st_ListSign
                                    Case "Y"
                                        F_str = "1"
                                    Case "N"
                                        Select Case str_session
                                            Case "1"
                                                F_str = "3"
                                            Case "0"
                                                'F_str = "0"
                                                Select Case st_ListSign
                                                    Case "Y"
                                                        F_str = "1"
                                                    Case "N"
                                                        F_str = "0"
                                                End Select
                                            Case Else
                                                F_str = "4" 'error
                                        End Select
                                End Select
                            Case "0"
                                'F_str = "0"
                                Select Case st_ListSign
                                    Case "Y"
                                        F_str = "1"
                                    Case "N"
                                        F_str = "0"
                                End Select
                            Case "1"
                                F_str = "1"
                        End Select
                End Select
        End Select
        Return F_str
    End Function
#Region "no use"
    'Function CheckUse_imagesign(ByVal str_session As String, ByVal st_action As String, ByVal st_caseset As String, _
    '    ByVal st_copy As String, ByVal st_sign As String, ByVal st_ListSign As String) As String
    '    Dim F_str As String = "5" 'ไม่เข้า case ไหนเลย

    '    Select Case st_action
    '        Case Is <> "new" 'เป็น view or edit
    '            Select Case st_caseset
    '                Case ""
    '                    Select Case st_ListSign 'check ว่า เลือกชื่อลายเซ็นหรือไม่
    '                        Case "Y"
    '                            F_str = "1"
    '                        Case "N"
    '                            F_str = "0"
    '                    End Select
    '                Case "0"
    '                    F_str = "0"
    '                Case "1"
    '                    F_str = "1"
    '                Case "3"
    '                    Select Case st_ListSign
    '                        Case "Y"
    '                            F_str = "1"
    '                        Case "N"
    '                            F_str = "0"
    '                    End Select
    '            End Select
    '        Case Is = "new"
    '            Select Case st_copy
    '                Case "true"
    '                    Select Case st_caseset
    '                        Case ""
    '                            Select Case st_ListSign
    '                                Case "Y"
    '                                    F_str = "1"
    '                                Case "N"
    '                                    F_str = "0"
    '                            End Select
    '                        Case "0"
    '                            F_str = "0"
    '                        Case "1"
    '                            F_str = "1"
    '                        Case "3"
    '                            F_str = "3" 'ตอน copy มา แต่ติ๊กเลือกชื่อกรรมการออก
    '                    End Select
    '                Case Else
    '                    Select Case st_caseset
    '                        Case ""
    '                            Select Case st_ListSign
    '                                Case "Y"
    '                                    F_str = "1"
    '                                Case "N"
    '                                    Select Case str_session
    '                                        Case "1"
    '                                            F_str = "3"
    '                                        Case "0"
    '                                            F_str = "0"
    '                                        Case Else
    '                                            F_str = "4" 'error
    '                                    End Select
    '                            End Select
    '                        Case "0"
    '                            F_str = "0"
    '                        Case "1"
    '                            F_str = "1"
    '                    End Select
    '            End Select
    '    End Select
    '    Return F_str
    'End Function
#End Region

    'ไม่ได้ใช้แล้ว
    Function CheckUse_imagesignp(ByVal str_session As String, ByVal st_action As String, ByVal st_caseset As String, _
    ByVal st_copy As String, ByVal st_sign As String) As String
        Dim F_str As String = "3" '

        Select Case st_action
            Case Is <> "new" 'เป็น view or edit
                Select Case str_session
                    Case str_session.Equals(System.DBNull.Value), "" 'session nothing
                        F_str = st_caseset
                    Case Else '
                        F_str = str_session
                End Select
            Case Is = "new"
                Select Case st_copy
                    Case "true"
                        Select Case st_sign
                            Case "true"
                                F_str = st_caseset
                            Case Else
                                F_str = str_session
                        End Select
                    Case "basix" 'copy จาก sign แต่เลือกแบบธรรมดา
                        F_str = st_caseset
                    Case Else
                        Select Case str_session
                            Case str_session.Equals(System.DBNull.Value), "" 'session nothing
                                F_str = st_caseset
                            Case Else
                                F_str = str_session
                        End Select
                End Select
        End Select
        Return F_str
    End Function

#Region "Form4_2 invoice vip"
    'check invoice ของ form2 อีกทีว่าซ้ำหรือไม่ เนื่องจากสิทธิ์ โดยหอร์มอีต้องส่งได้สิทธิ์ก่อน แล้ว form2 จึงจะขอผ่าน แต่ถ้า form2 ส่งก่อน แล้วฟอร์มอีส่งตามมา จะต้องไม่ผ่าน
    Function check_invoiceForm2(ByVal By1_ref As String, ByVal By1_invh_run_auto As String, ByVal By1_Tax As String, ByVal By1_invoice1 As String, ByVal By1_invoice2 As String, _
        ByVal By1_invoice3 As String, ByVal By1_invoice4 As String, ByVal By1_invoice5 As String) As Integer

        Dim By1_year As String = Now.Year

        Dim SQLForm2 As String = ""
        SQLForm2 = "SELECT     edi_year, company_taxno, invoice_no, active_flag, cancel_by, form_type, invh_run_auto, invh_run_autoTrue " & _
                        "FROM         invoice_EDI " & _
                        "WHERE     (company_taxno = @company_taxno) AND (active_flag = 'Y') AND (edi_year = @edi_year) AND (form_type = 'FORM2') AND (invoice_no = @invoice_no)"

        Dim prmForm2(2) As SqlClient.SqlParameter
        prmForm2(0) = New SqlClient.SqlParameter("@company_taxno", By1_Tax)
        prmForm2(1) = New SqlClient.SqlParameter("@edi_year", By1_year)

        Dim ds As New DataSet

        For iform2 As Integer = 0 To 4
            Select Case iform2
                Case 0
                    prmForm2(2) = New SqlClient.SqlParameter("@invoice_no", Replace(By1_invoice1, " ", ""))
                    ds = SqlHelper.ExecuteDataset(strConn, CommandType.Text, SQLForm2, prmForm2)
                    If ds.Tables(0).Rows.Count > 0 Then
                        'แสดงว่าซ้ำ กับ form2
                        Return 2222
                    End If
                Case 1
                    prmForm2(2) = New SqlClient.SqlParameter("@invoice_no", Replace(By1_invoice2, " ", ""))
                    ds = SqlHelper.ExecuteDataset(strConn, CommandType.Text, SQLForm2, prmForm2)
                    If ds.Tables(0).Rows.Count > 0 Then
                        'แสดงว่าซ้ำ กับ form2
                        Return 2222
                    End If
                Case 2
                    prmForm2(2) = New SqlClient.SqlParameter("@invoice_no", Replace(By1_invoice3, " ", ""))
                    ds = SqlHelper.ExecuteDataset(strConn, CommandType.Text, SQLForm2, prmForm2)
                    If ds.Tables(0).Rows.Count > 0 Then
                        'แสดงว่าซ้ำ กับ form2
                        Return 2222
                    End If
                Case 3
                    prmForm2(2) = New SqlClient.SqlParameter("@invoice_no", Replace(By1_invoice4, " ", ""))
                    ds = SqlHelper.ExecuteDataset(strConn, CommandType.Text, SQLForm2, prmForm2)
                    If ds.Tables(0).Rows.Count > 0 Then
                        'แสดงว่าซ้ำ กับ form2
                        Return 2222
                    End If
                Case 4
                    prmForm2(2) = New SqlClient.SqlParameter("@invoice_no", Replace(By1_invoice5, " ", ""))
                    ds = SqlHelper.ExecuteDataset(strConn, CommandType.Text, SQLForm2, prmForm2)
                    If ds.Tables(0).Rows.Count > 0 Then
                        'แสดงว่าซ้ำ กับ form2
                        Return 2222
                    End If
            End Select
        Next

        Return 1
    End Function
    'เรื่อง invoice Form4_2
    Function Begin_check_insert_invoice(ByVal By_ref As String, ByVal By_invh_run_auto As String, ByVal By_Tax As String, ByVal By_invoice1 As String, ByVal By_invoice2 As String, _
        ByVal By_invoice3 As String, ByVal By_invoice4 As String, ByVal By_invoice5 As String) As Integer
        Dim ds As DataSet
        Dim By_year As String = Now.Year

        Dim SQL As String
        'check เลข invoice
        Select Case Replace(By_ref, " ", "")
            Case Is <> "" 'ป้อนเลขอ้างอิง
                SQL = "SELECT     edi_year, company_taxno, invoice_no, active_flag, cancel_by, form_type, invh_run_auto, invh_run_autoTrue " & _
                        "FROM         invoice_EDI " & _
                        "WHERE     (company_taxno = @company_taxno) AND (active_flag = 'Y') AND (edi_year = @edi_year) AND (form_type = 'FORM4_2') AND (invh_run_auto = @invh_run_auto)"

                Dim prm(2) As SqlClient.SqlParameter
                prm(0) = New SqlClient.SqlParameter("@company_taxno", By_Tax)
                prm(1) = New SqlClient.SqlParameter("@edi_year", By_year)
                prm(2) = New SqlClient.SqlParameter("@invh_run_auto", By_ref)

                ds = SqlHelper.ExecuteDataset(strConn, CommandType.Text, SQL, prm)

                If ds.Tables(0).Rows.Count > 0 Then
                    'แสดงว่ามีรายการ ที่ใช้อ้างอิง เป็นรายการชุดของ invoice
                    For i As Integer = 0 To 4
                        Select Case i
                            Case 0
                                If ds.Tables(0).Rows(0).Item("invoice_no") = By_invoice1 Then
                                    Return 8
                                    Exit Function
                                End If
                            Case 1
                                If ds.Tables(0).Rows(0).Item("invoice_no") = By_invoice2 Then
                                    Return 8
                                    Exit Function
                                End If
                            Case 2
                                If ds.Tables(0).Rows(0).Item("invoice_no") = By_invoice3 Then
                                    Return 8
                                    Exit Function
                                End If
                            Case 3
                                If ds.Tables(0).Rows(0).Item("invoice_no") = By_invoice4 Then
                                    Return 8
                                    Exit Function
                                End If
                            Case 4
                                If ds.Tables(0).Rows(0).Item("invoice_no") = By_invoice5 Then
                                    Return 8
                                    Exit Function
                                End If
                        End Select

                    Next
                End If
                '===============================================
                'กรณี รายการแรกที่เข้าไป เป็นรายการเริ่มต้น ซึ่งจะไม่มีเลขอ้างอิง ต้องหาในฟิวล์ true อีกที
                Dim SQLTrue As String = ""
                SQLTrue = "SELECT     edi_year, company_taxno, invoice_no, active_flag, cancel_by, form_type, invh_run_auto, invh_run_autoTrue " & _
                        "FROM         invoice_EDI " & _
                        "WHERE     (company_taxno = @company_taxno) AND (active_flag = 'Y') AND (edi_year = @edi_year) AND (form_type = 'FORM4_2') AND (invh_run_autoTrue = @invh_run_autoTrue)"
                Dim prmTrue(2) As SqlClient.SqlParameter
                prmTrue(0) = New SqlClient.SqlParameter("@company_taxno", By_Tax)
                prmTrue(1) = New SqlClient.SqlParameter("@edi_year", By_year)
                prmTrue(2) = New SqlClient.SqlParameter("@invh_run_autoTrue", By_ref)
                Dim dsTrue As DataSet
                dsTrue = SqlHelper.ExecuteDataset(strConn, CommandType.Text, SQLTrue, prmTrue)
                If dsTrue.Tables(0).Rows.Count > 0 Then
                    'แสดงว่ามีรายการ ที่ใช้อ้างอิง เป็นรายการชุดของ invoice
                    For i As Integer = 0 To 4
                        Select Case i
                            Case 0
                                If dsTrue.Tables(0).Rows(0).Item("invoice_no") = By_invoice1 Then
                                    Return 8
                                    Exit Function
                                End If
                            Case 1
                                If dsTrue.Tables(0).Rows(0).Item("invoice_no") = By_invoice2 Then
                                    Return 8
                                    Exit Function
                                End If
                            Case 2
                                If dsTrue.Tables(0).Rows(0).Item("invoice_no") = By_invoice3 Then
                                    Return 8
                                    Exit Function
                                End If
                            Case 3
                                If dsTrue.Tables(0).Rows(0).Item("invoice_no") = By_invoice4 Then
                                    Return 8
                                    Exit Function
                                End If
                            Case 4
                                If dsTrue.Tables(0).Rows(0).Item("invoice_no") = By_invoice5 Then
                                    Return 8
                                    Exit Function
                                End If
                        End Select

                    Next
                End If
                '===============================================
                'กรณี ใช้เลขอ้างอิงหาแล้วไม่เจอ ต้องเอาเลข invoice หาอีกทีว่ามีซ้ำหรือไม่
                Dim SQL2 As String = ""
                SQL2 = "SELECT     edi_year, company_taxno, invoice_no, active_flag, cancel_by, form_type, invh_run_auto, invh_run_autoTrue " & _
                        "FROM         invoice_EDI " & _
                        "WHERE     (company_taxno = @company_taxno) AND (active_flag = 'Y') AND (edi_year = @edi_year) AND (form_type = 'FORM4_2') AND (invoice_no = @invoice_no)"

                Dim prm2(2) As SqlClient.SqlParameter
                prm2(0) = New SqlClient.SqlParameter("@company_taxno", By_Tax)
                prm2(1) = New SqlClient.SqlParameter("@edi_year", By_year)
                Dim ds2 As DataSet

                For iin As Integer = 0 To 4
                    Select Case iin
                        Case 0
                            prm2(2) = New SqlClient.SqlParameter("@invoice_no", By_invoice1)
                            ds2 = SqlHelper.ExecuteDataset(strConn, CommandType.Text, SQL2, prm2)
                            If ds2.Tables(0).Rows.Count > 0 Then
                                Return 666
                                Exit Function
                            End If
                        Case 1
                            prm2(2) = New SqlClient.SqlParameter("@invoice_no", By_invoice2)
                            ds2 = SqlHelper.ExecuteDataset(strConn, CommandType.Text, SQL2, prm2)
                            If ds2.Tables(0).Rows.Count > 0 Then
                                Return 666
                                Exit Function
                            End If
                        Case 2
                            prm2(2) = New SqlClient.SqlParameter("@invoice_no", By_invoice3)
                            ds2 = SqlHelper.ExecuteDataset(strConn, CommandType.Text, SQL2, prm2)
                            If ds2.Tables(0).Rows.Count > 0 Then
                                Return 666
                                Exit Function
                            End If
                        Case 3
                            prm2(2) = New SqlClient.SqlParameter("@invoice_no", By_invoice4)
                            ds2 = SqlHelper.ExecuteDataset(strConn, CommandType.Text, SQL2, prm2)
                            If ds2.Tables(0).Rows.Count > 0 Then
                                Return 666
                                Exit Function
                            End If
                        Case 4
                            prm2(2) = New SqlClient.SqlParameter("@invoice_no", By_invoice5)
                            ds2 = SqlHelper.ExecuteDataset(strConn, CommandType.Text, SQL2, prm2)
                            If ds2.Tables(0).Rows.Count > 0 Then
                                Return 666
                                Exit Function
                            End If
                    End Select

                Next

                '===============================================
            Case Is = "" 'ไม่ป้อน เป็นรายการแรก
                SQL = "SELECT     edi_year, company_taxno, invoice_no, active_flag, cancel_by, form_type, invh_run_auto " & _
                                "FROM         invoice_EDI " & _
                                "WHERE     (company_taxno = @company_taxno) AND (active_flag = 'Y') AND (edi_year = @edi_year) AND " & _
                                "(form_type = 'FORM4_2') AND (invoice_no = @invoice_no)"

                Dim prm(2) As SqlClient.SqlParameter
                prm(0) = New SqlClient.SqlParameter("@company_taxno", By_Tax)
                prm(1) = New SqlClient.SqlParameter("@edi_year", By_year)

                For i As Integer = 0 To 4
                    Select Case i
                        Case 0
                            prm(2) = New SqlClient.SqlParameter("@invoice_no", Replace(By_invoice1, " ", ""))
                            ds = SqlHelper.ExecuteDataset(strConn, CommandType.Text, SQL, prm)
                        Case 1
                            prm(2) = New SqlClient.SqlParameter("@invoice_no", Replace(By_invoice2, " ", ""))
                            ds = SqlHelper.ExecuteDataset(strConn, CommandType.Text, SQL, prm)
                        Case 2
                            prm(2) = New SqlClient.SqlParameter("@invoice_no", Replace(By_invoice3, " ", ""))
                            ds = SqlHelper.ExecuteDataset(strConn, CommandType.Text, SQL, prm)
                        Case 3
                            prm(2) = New SqlClient.SqlParameter("@invoice_no", Replace(By_invoice4, " ", ""))
                            ds = SqlHelper.ExecuteDataset(strConn, CommandType.Text, SQL, prm)
                        Case 4
                            prm(2) = New SqlClient.SqlParameter("@invoice_no", Replace(By_invoice5, " ", ""))
                            ds = SqlHelper.ExecuteDataset(strConn, CommandType.Text, SQL, prm)
                    End Select

                    If ds.Tables(0).Rows.Count > 0 Then
                        'invoice ซ้ำ check อีกว่า เป็นรายการที่เลขเดียวกันหรือป่าว 
                        If CommonUtility.Get_StringValue(ds.Tables(0).Rows(0).Item("invh_run_auto")) <> By_invh_run_auto Then
                            Return i
                        ElseIf CommonUtility.Get_StringValue(ds.Tables(0).Rows(0).Item("invh_run_auto")) = By_invh_run_auto Then
                            'กรณีเป็นเลขเดียวกัน หมายความว่าส่ง เข้ามาพร้อมกัน แต่ รายการอันอื่น insert เข้า invoice ไปแล้ว
                        End If
                    End If
                Next
        End Select

        Return 9 'คือไม่ซ้ำ
    End Function
    Function End_insert_invoice(ByVal By1_ref As String, ByVal By1_invh_run_auto As String, ByVal By1_Tax As String, ByVal By1_invoice1 As String, ByVal By1_invoice2 As String, _
        ByVal By1_invoice3 As String, ByVal By1_invoice4 As String, ByVal By1_invoice5 As String) As Integer

        Dim Case_ErrorIn_invoice As Integer = 777 'ซ้ำในตัวเอง
        For iCheck As Integer = 0 To 4
            Select Case iCheck
                Case 0
                    If Replace(By1_invoice1, " ", "") = Replace(By1_invoice2, " ", "") And Replace(By1_invoice1, " ", "") <> "" Then Return Case_ErrorIn_invoice
                    If Replace(By1_invoice1, " ", "") = Replace(By1_invoice3, " ", "") And Replace(By1_invoice1, " ", "") <> "" Then Return Case_ErrorIn_invoice
                    If Replace(By1_invoice1, " ", "") = Replace(By1_invoice4, " ", "") And Replace(By1_invoice1, " ", "") <> "" Then Return Case_ErrorIn_invoice
                    If Replace(By1_invoice1, " ", "") = Replace(By1_invoice5, " ", "") And Replace(By1_invoice1, " ", "") <> "" Then Return Case_ErrorIn_invoice
                Case 1
                    If Replace(By1_invoice2, " ", "") = Replace(By1_invoice1, " ", "") And Replace(By1_invoice2, " ", "") <> "" Then Return Case_ErrorIn_invoice
                    If Replace(By1_invoice2, " ", "") = Replace(By1_invoice3, " ", "") And Replace(By1_invoice2, " ", "") <> "" Then Return Case_ErrorIn_invoice
                    If Replace(By1_invoice2, " ", "") = Replace(By1_invoice4, " ", "") And Replace(By1_invoice2, " ", "") <> "" Then Return Case_ErrorIn_invoice
                    If Replace(By1_invoice2, " ", "") = Replace(By1_invoice5, " ", "") And Replace(By1_invoice2, " ", "") <> "" Then Return Case_ErrorIn_invoice
                Case 2
                    If Replace(By1_invoice3, " ", "") = Replace(By1_invoice1, " ", "") And Replace(By1_invoice3, " ", "") <> "" Then Return Case_ErrorIn_invoice
                    If Replace(By1_invoice3, " ", "") = Replace(By1_invoice2, " ", "") And Replace(By1_invoice3, " ", "") <> "" Then Return Case_ErrorIn_invoice
                    If Replace(By1_invoice3, " ", "") = Replace(By1_invoice4, " ", "") And Replace(By1_invoice3, " ", "") <> "" Then Return Case_ErrorIn_invoice
                    If Replace(By1_invoice3, " ", "") = Replace(By1_invoice5, " ", "") And Replace(By1_invoice3, " ", "") <> "" Then Return Case_ErrorIn_invoice
                Case 3
                    If Replace(By1_invoice4, " ", "") = Replace(By1_invoice1, " ", "") And Replace(By1_invoice4, " ", "") <> "" Then Return Case_ErrorIn_invoice
                    If Replace(By1_invoice4, " ", "") = Replace(By1_invoice2, " ", "") And Replace(By1_invoice4, " ", "") <> "" Then Return Case_ErrorIn_invoice
                    If Replace(By1_invoice4, " ", "") = Replace(By1_invoice3, " ", "") And Replace(By1_invoice4, " ", "") <> "" Then Return Case_ErrorIn_invoice
                    If Replace(By1_invoice4, " ", "") = Replace(By1_invoice5, " ", "") And Replace(By1_invoice4, " ", "") <> "" Then Return Case_ErrorIn_invoice
                Case 4
                    If Replace(By1_invoice5, " ", "") = Replace(By1_invoice1, " ", "") And Replace(By1_invoice5, " ", "") <> "" Then Return Case_ErrorIn_invoice
                    If Replace(By1_invoice5, " ", "") = Replace(By1_invoice2, " ", "") And Replace(By1_invoice5, " ", "") <> "" Then Return Case_ErrorIn_invoice
                    If Replace(By1_invoice5, " ", "") = Replace(By1_invoice3, " ", "") And Replace(By1_invoice5, " ", "") <> "" Then Return Case_ErrorIn_invoice
                    If Replace(By1_invoice5, " ", "") = Replace(By1_invoice4, " ", "") And Replace(By1_invoice5, " ", "") <> "" Then Return Case_ErrorIn_invoice
            End Select
        Next

        Dim CaseErrorCheck As Integer = Begin_check_insert_invoice(By1_ref, By1_invh_run_auto, By1_Tax, By1_invoice1, By1_invoice2, By1_invoice3, By1_invoice4, By1_invoice5)
        Select Case CaseErrorCheck
            Case 9 'ไม่ซ้ำ
                Dim ds As DataSet
                Dim By1_year As String = Now.Year

                Dim SQL As String

                Select Case check_Add_invoice(By1_Tax, By1_invh_run_auto, By1_ref)
                    Case True 'รายการมีแล้ว update
                        'check เลข invoice
                        'SQL = "UPDATE    invoice_EDI " & _
                        '        "SET              invoice_no =@invoice_no " & _
                        '        "WHERE     (company_taxno = @company_taxno) AND (active_flag = 'Y') AND (edi_year = @edi_year) AND (form_type = 'FORM4_2') AND " & _
                        '        "                      (invh_run_auto = @invh_run_auto) AND (invoice_no=@invoice_no) AND (invh_run_autoTrue=@invh_run_autoTrue)"
                        GoTo Case_invoiceInTotal
                    Case False 'รายการไม่มีแล้ว insert
                        'check เลข invoice
                        SQL = "INSERT INTO invoice_EDI " & _
                                "                      (company_taxno, edi_year, form_type, invh_run_auto,active_flag , invoice_no, cancel_by, invh_run_autoTrue) " & _
                                "VALUES     (@company_taxno, @edi_year, @form_type, @invh_run_auto,@active_flag, @invoice_no, '',@invh_run_autoTrue)"
                End Select
                '=========================================

                Dim prm(6) As SqlClient.SqlParameter
                prm(0) = New SqlClient.SqlParameter("@company_taxno", By1_Tax)
                prm(1) = New SqlClient.SqlParameter("@edi_year", By1_year)
                prm(2) = New SqlClient.SqlParameter("@form_type", "FORM4_2")
                prm(3) = New SqlClient.SqlParameter("@invh_run_auto", By1_ref)
                prm(4) = New SqlClient.SqlParameter("@active_flag", "Y")
                prm(5) = New SqlClient.SqlParameter("@invh_run_autoTrue", By1_invh_run_auto)

                For i As Integer = 0 To 4
                    Select Case i
                        Case 0
                            If Replace(By1_invoice1, " ", "") <> "" Then
                                prm(6) = New SqlClient.SqlParameter("@invoice_no", Replace(By1_invoice1, " ", ""))
                                ds = SqlHelper.ExecuteDataset(strConn, CommandType.Text, SQL, prm)
                            End If
                        Case 1
                            If Replace(By1_invoice2, " ", "") <> "" Then
                                prm(6) = New SqlClient.SqlParameter("@invoice_no", Replace(By1_invoice2, " ", ""))
                                ds = SqlHelper.ExecuteDataset(strConn, CommandType.Text, SQL, prm)
                            End If
                        Case 2
                            If Replace(By1_invoice3, " ", "") <> "" Then
                                prm(6) = New SqlClient.SqlParameter("@invoice_no", Replace(By1_invoice3, " ", ""))
                                ds = SqlHelper.ExecuteDataset(strConn, CommandType.Text, SQL, prm)
                            End If
                        Case 3
                            If Replace(By1_invoice4, " ", "") <> "" Then
                                prm(6) = New SqlClient.SqlParameter("@invoice_no", Replace(By1_invoice4, " ", ""))
                                ds = SqlHelper.ExecuteDataset(strConn, CommandType.Text, SQL, prm)
                            End If
                        Case 4
                            If Replace(By1_invoice5, " ", "") <> "" Then
                                prm(6) = New SqlClient.SqlParameter("@invoice_no", Replace(By1_invoice5, " ", ""))
                                ds = SqlHelper.ExecuteDataset(strConn, CommandType.Text, SQL, prm)
                            End If
                    End Select

                Next

                Return 99
            Case 8 'ซ้ำแบบ เป็นชุดเดียวกัน
Case_invoiceInTotal:
                Return 999
            Case 666 'ซ้ำ
                Return CaseErrorCheck
            Case Else 'ซ้ำ
                Return CaseErrorCheck

        End Select
    End Function
    'check ตอน insert ถ้าแก้ไข invoice แล้วบันทึก เพื่อไม่ให้เกิดการ add invoice ไปหลายแถว
    Function check_Add_invoice(ByVal By_company_taxno As String, ByVal By_invh_run_auto As String, ByVal By_ref As String) As Boolean
        Dim SQl As String
        Dim By_year As String = Now.Year
        Dim status_invoice As Boolean = False

        Dim prm(2) As SqlClient.SqlParameter
        prm(0) = New SqlClient.SqlParameter("@company_taxno", By_company_taxno)
        prm(1) = New SqlClient.SqlParameter("@edi_year", By_year)

        If By_ref <> "" Then
            SQl = "SELECT     edi_year, company_taxno, invoice_no, active_flag, cancel_by, form_type, invh_run_auto " & _
                "FROM         invoice_EDI " & _
                "WHERE     (company_taxno = @company_taxno) AND (active_flag = 'Y') AND (edi_year = @edi_year) AND " & _
                "(form_type = 'FORM4_2') AND (invh_run_autoTrue = @invh_run_autoTrue)"

            prm(2) = New SqlClient.SqlParameter("@invh_run_autoTrue", By_ref)
        Else
            SQl = "SELECT     edi_year, company_taxno, invoice_no, active_flag, cancel_by, form_type, invh_run_auto " & _
                "FROM         invoice_EDI " & _
                "WHERE     (company_taxno = @company_taxno) AND (active_flag = 'Y') AND (edi_year = @edi_year) AND " & _
                "(form_type = 'FORM4_2') AND (invh_run_auto = @invh_run_auto)"

            prm(2) = New SqlClient.SqlParameter("@invh_run_auto", By_invh_run_auto)
        End If

        Dim ds As DataSet
        ds = SqlHelper.ExecuteDataset(strConn, CommandType.Text, SQl, prm)

        If ds.Tables(0).Rows.Count > 0 Then
            status_invoice = True 'รายการมีแล้ว
        End If

        Return status_invoice
    End Function

    'check tariff ตอนบันทึกเลยเพื่อกันเรื่องตอนป้อนพิกัดผิด เพื่อความถูกต้องในระดับนึงเนื่องจาก อาจจะมีปัญหาตอน invoice ผ่าน แล้วต้องมาลบเนื่องจากพิกัดผิด
    Function CheckTariff_Form4_2True(ByVal By_country As String, ByVal By_Tariff As String) As Boolean
        Dim ds As DataSet
        ds = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, "sp_common_get_TariffForm4_2_NewDS", _
                 New SqlParameter("@tariff_code", By_Tariff), _
                 New SqlParameter("@country_code", By_country))

        If ds.Tables(0).Rows.Count > 0 Then
            Return True 'พิกัดถูก
        Else
            Return False 'พิกัดผิด
        End If
    End Function

    'return พิกัด
    Function get_dataForm4_2Check(ByVal By_invh_run_auto As String, Optional ByVal By_destination_country As String = "", Optional ByVal By_Savebtn As String = "") As String
        Dim ds As DataSet
        ds = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, "sp_common_get_DataAllForm4_2_NewDS", _
                 New SqlParameter("@invh_run_auto", By_invh_run_auto))

        Dim str_tariff As String = ""
        Select Case By_Savebtn
            Case ""
                For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                    Select Case CheckTariff_Form4_2True(IIf(Return_CheckNullValueAllStr(ds.Tables(0).Rows(i).Item("destination_country")) <> "", _
                                ds.Tables(0).Rows(i).Item("destination_country"), By_destination_country), ds.Tables(0).Rows(i).Item("tariff_code"))
                        Case False
                            str_tariff &= ds.Tables(0).Rows(i).Item("tariff_code") & ", "
                    End Select
                Next
            Case Else
                For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                    Select Case CheckTariff_Form4_2True(By_destination_country, ds.Tables(0).Rows(i).Item("tariff_code"))
                        Case False
                            str_tariff &= ds.Tables(0).Rows(i).Item("tariff_code") & ", "
                    End Select
                Next
        End Select


        Return str_tariff
    End Function

    'return เลขอ้างอิง
    Function get_dataForm4_2CheckReturn_id(ByVal By_invh_run_auto As String) As String
        Dim ds As DataSet
        ds = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, "sp_common_get_DataAllForm4_2_NewDS", _
                 New SqlParameter("@invh_run_auto", By_invh_run_auto))

        Dim str_tariff As String = ""

        For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
            Select Case CheckTariff_Form4_2True(ds.Tables(0).Rows(i).Item("destination_country"), ds.Tables(0).Rows(i).Item("tariff_code"))
                Case False
                    str_tariff &= ds.Tables(0).Rows(i).Item("invh_run_auto") & ", "
            End Select
        Next

        Return str_tariff
    End Function
    'พักก่อน อาจจะไม่ต้องใช้
    'check detail form4_2 รายการถึง 20 รายการหรือไม่ ถ้าถึงให้ check
    Function checkDetail_20(ByVal By_invh_run_auto As String) As Boolean
        Dim SQL As String = "SELECT     invh_run_auto, invd_run_auto, tariff_code FROM         form_detail_edi WHERE     (invh_run_auto = @invh_run_auto)"

        Dim prm As SqlClient.SqlParameter = New SqlClient.SqlParameter("@invh_run_auto", By_invh_run_auto)

        Dim ds As DataSet
        ds = SqlHelper.ExecuteDataset(strConn, CommandType.Text, SQL, prm)

        If ds.Tables(0).Rows.Count >= 20 Then
            Return True
        Else
            Return False
        End If
    End Function
#End Region
    Function Return_CheckNullValueAllStr(ByVal By_valueData As Object) As String
        If Not IsDBNull(By_valueData) Or By_valueData.ToString <> "" Then
            Return By_valueData
        Else
            Return ""
        End If
    End Function
#Region "By rut check Seal Sign"
    Function Check_Company_SealApprove(ByVal By_Tax As String) As Boolean
        Dim sqlcheck_sealsign As String = ""
        sqlcheck_sealsign = "SELECT     * FROM         company WHERE     (company_taxno = @company_taxno)"
        Dim ds As DataSet
        Dim prm(0) As SqlClient.SqlParameter
        prm(0) = New SqlClient.SqlParameter("@company_taxno", By_Tax)
        ds = SqlHelper.ExecuteDataset(strConn, CommandType.Text, sqlcheck_sealsign, prm)

        Dim check_status As Boolean = False
        If ds.Tables(0).Rows.Count > 0 Then
            With ds.Tables(0).Rows(0)
                Select Case .Item("company_internet_seal_edi").ToString
                    Case "Y"
                        check_status = True
                    Case Else
                        check_status = False
                End Select
            End With
        End If

        Return check_status
    End Function
#End Region

#Region "check เรื่องการแก้ไข url"
    Function checkData_UrlCase(ByVal By_ref As String, ByVal By_actionForm As String, Optional ByVal By_UserCheck As String = "") As Boolean
        Dim ReCaseCheck As Boolean = False 'view
        Dim ds As DataSet
        Dim sql As String = ""

        Select Case By_UserCheck
            Case "@dm1nPr0DFT" 'true แก้ไข ได้
                Return True
            Case Else 'ถ้าไม่ใช่ สถานะ O View ได้อย่างเดียว
                sql = "SELECT        invh_run_auto, company_taxno, edi_status_id FROM            dbo.form_header_edi WHERE        (invh_run_auto = @invh_run_auto)"
        End Select

        Dim prm As SqlClient.SqlParameter = New SqlClient.SqlParameter("@invh_run_auto", By_ref)

        ds = SqlHelper.ExecuteDataset(strConn, CommandType.Text, sql, prm)
        If ds.Tables(0).Rows.Count > 0 Then
            Select Case By_actionForm
                Case "edit"
                    If ds.Tables(0).Rows(0).Item("edi_status_id") = "O" Then
                        'ถ้า = O สามารถแก้ไขได้
                        Return True
                    Else
                        'ถ้า <> O ออกจากระบบ เข้ามาแบบผิดทาง
                        Return False
                    End If
                Case Else
                    Return True
            End Select
        End If

        Return ReCaseCheck
    End Function
#End Region
End Module
