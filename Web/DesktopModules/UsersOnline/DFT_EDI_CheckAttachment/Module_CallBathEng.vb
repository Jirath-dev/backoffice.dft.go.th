Imports Microsoft.VisualBasic
Imports Microsoft.ApplicationBlocks.Data
Imports System.Data.SqlClient
Imports System.Data
Imports System.Text
Imports DFT.Dotnetnuke.ClassLibrary
Module Module_CallBathEng
    '===========================================
    'Printer
    Function CallPrintConfig(ByVal MyPrinter_ As String) As String
        Select Case MyPrinter_
            Case "ST-001"
                Return ConfigurationManager.AppSettings("printName_dev").ToString()
            Case ""
                Return ConfigurationManager.AppSettings("printName_Pdf").ToString()
        End Select

    End Function

    '===========================================
    'Bath Eng
    Function BahtOnly(ByVal MyNumber, ByVal Myq_unit_code)
        Dim F_Mynumber As String = MyNumber
        Dim Baht, Temp
        Dim Satang As String = ""
        Dim DecimalPlace, Count

        Dim Place() As String
        ReDim Place(9)
        Place(2) = " THOUSAND "
        Place(3) = " MILLION "
        Place(4) = " BILLION "
        Place(5) = " TRILLION "

        ' String representation of amount
        MyNumber = Trim(Str(MyNumber))

        ' Position of decimal place 0 if none
        DecimalPlace = InStr(MyNumber, ".")
        'Convert Satang and set MyNumber to Baht amount
        If DecimalPlace > 0 Then
            Dim str_digi As String = Left(Mid(MyNumber, DecimalPlace + 1) & "0", 3)

            Dim str_digi1 As String = MyNumber 'Left(Mid(MyNumber, DecimalPlace + 1) & "00", 1)
            Dim str_digi2 As String = MyNumber 'Left(Mid(MyNumber, DecimalPlace + 1) & "00", 2)
            Dim str_digi3 As String = MyNumber 'Left(Mid(MyNumber, DecimalPlace + 1) & "00", 3)
            Dim str_digi4 As String = MyNumber 'Left(Mid(MyNumber, DecimalPlace + 1) & "00", 4)

            Dim str_digiSP As String = MyNumber
            Dim _str() As String
            _str = str_digiSP.Split(".")

            Select Case Len(_str(1))
                Case 1
                    Satang = GetDigitcheck(Mid(_str(1), 1, 1)) & " " & GetDigitcheck(Mid(_str(1), 2, 1))
                Case 2
                    Satang = GetDigitcheck(Mid(_str(1), 1, 1)) & " " & GetDigitcheck(Mid(_str(1), 2, 1))
                Case 3
                    Satang = GetDigitcheck(Mid(_str(1), 1, 1)) & " " & GetDigitcheck(Mid(_str(1), 2, 1)) & " " & GetDigitcheck(Mid(_str(1), 3, 1))
                Case 4
                    Satang = GetDigitcheck(Mid(_str(1), 1, 1)) & " " & GetDigitcheck(Mid(_str(1), 2, 1)) & " " & GetDigitcheck(Mid(_str(1), 3, 1)) & " " & GetDigitcheck(Mid(_str(1), 4, 1))
            End Select


            'Select Case Len(DecimalPlace)
            '    Case 1
            '        'Satang = GetDigitcheck(Mid(str_digi1, 1, 1)) & " "
            '    Case 2
            '        'If Right(str_digi2, 1) = "0" Then 'check เรื่อง ตัวสุดท้ายหลังจุดทดศนิยมเป็น ศูนย์ไม่เอา
            '        '    Satang = GetDigitcheck(Mid(str_digi2, 1, 1)) '& " " & GetDigitcheck(Mid(str_digi2, 2, 1))
            '        'Else
            '        '    Satang = GetDigitcheck(Mid(str_digi2, 1, 1)) & " " & GetDigitcheck(Mid(str_digi2, 2, 1))
            '        'End If

            '    Case 3
            '        'If Right(str_digi3, 1) = "0" Then 'check เรื่อง ตัวสุดท้ายหลังจุดทดศนิยมเป็น ศูนย์ไม่เอา
            '        '    Satang = GetDigitcheck(Mid(str_digi3, 1, 1)) & " " & GetDigitcheck(Mid(str_digi3, 2, 1)) '& " " & GetDigitcheck(Mid(str_digi3, 3, 1))
            '        'Else
            '        '    Satang = GetDigitcheck(Mid(str_digi3, 1, 1)) & " " & GetDigitcheck(Mid(str_digi3, 2, 1)) & " " & GetDigitcheck(Mid(str_digi3, 3, 1))
            '        'End If

            '    Case 4
            '        'If Right(str_digi4, 1) = "0" And Right(str_digi3, 1) = "0" And Right(str_digi2, 1) = "0" And Right(str_digi1, 1) <> "0" Then 'check เรื่อง ตัวสุดท้ายหลังจุดทดศนิยมเป็น ศูนย์ไม่เอา
            '        '    'display 1
            '        '    Satang = GetDigitcheck(Mid(str_digi4, 1, 1)) '& " " & GetDigitcheck(Mid(str_digi4, 2, 1)) & " " '& GetDigitcheck(Mid(str_digi4, 3, 1)) & " " & GetDigitcheck(Mid(str_digi4, 4, 1))
            '        'ElseIf Right(str_digi4, 1) = "0" And Right(str_digi3, 1) = "0" And Right(str_digi2, 1) <> "0" And Right(str_digi1, 1) <> "0" Then 'check เรื่อง ตัวสุดท้ายหลังจุดทดศนิยมเป็น ศูนย์ไม่เอา
            '        '    'display 2
            '        '    Satang = GetDigitcheck(Mid(str_digi4, 1, 1)) & " " & GetDigitcheck(Mid(str_digi4, 2, 1)) & " " '& GetDigitcheck(Mid(str_digi4, 3, 1)) & " " & GetDigitcheck(Mid(str_digi4, 4, 1))
            '        'ElseIf Right(str_digi4, 1) = "0" And Right(str_digi3, 1) <> "0" And Right(str_digi2, 1) <> "0" And Right(str_digi1, 1) <> "0" Then 'check เรื่อง ตัวสุดท้ายหลังจุดทดศนิยมเป็น ศูนย์ไม่เอา
            '        '    'display 3
            '        '    Satang = GetDigitcheck(Mid(str_digi4, 1, 1)) & " " & GetDigitcheck(Mid(str_digi4, 2, 1)) & " " & GetDigitcheck(Mid(str_digi4, 3, 1)) '& " " & GetDigitcheck(Mid(str_digi4, 4, 1))
            '        'ElseIf Right(str_digi4, 1) <> "0" And Right(str_digi3, 1) <> "0" And Right(str_digi2, 1) <> "0" And Right(str_digi1, 1) <> "0" Then 'check เรื่อง ตัวสุดท้ายหลังจุดทดศนิยมเป็น ศูนย์ไม่เอา
            '        '    'display 4
            '        'Satang = GetDigitcheck(Mid(str_digi4, 1, 1)) & " " & GetDigitcheck(Mid(str_digi4, 2, 1)) & " " & GetDigitcheck(Mid(str_digi4, 3, 1)) & " " & GetDigitcheck(Mid(str_digi4, 4, 1))
            '        'End If

            'End Select

            'Satang = GetTenscheck(Left(Mid(MyNumber, DecimalPlace + 1) & "00", 2)) 'check เรื่องหลังจุดมีกี่ตัว 'GetTens(Left(Mid(MyNumber, DecimalPlace + 1) & "00", 2)) 'เอาจุดทศนิยมมา
            MyNumber = Trim(Left(MyNumber, DecimalPlace - 1))
        End If

        Count = 1
        Do While MyNumber <> ""
            Temp = GetHundreds(Right(MyNumber, 3))
            If Temp <> "" Then Baht = Temp & Place(Count) & Baht
            If Len(MyNumber) > 3 Then
                MyNumber = Left(MyNumber, Len(MyNumber) - 3)
            Else
                MyNumber = ""
            End If
            Count = Count + 1
        Loop

        Select Case Baht
            Case ""
                Baht = "ZERO " '"No "
            Case "One"
                Baht = "One"
            Case Else
                Baht = Baht & ""
        End Select

        Select Case Satang
            Case ""
                Satang = " POINT ZERO ZERO"
            Case "One"
                Satang = " POINT One "
            Case Else
                Satang = " POINT " & Satang & ""
        End Select
        If Check_Point(F_Mynumber) = True Then
            BahtOnly = Baht & Satang + " (" + Format(CDec(F_Mynumber), "#,##0.00##") + ") " + Myq_unit_code + "****"
        Else
            BahtOnly = Baht + " (" + Format(CInt(F_Mynumber), "#,##0") + ") " + Myq_unit_code + "****"
            ' BahtOnly = Baht & Satang + " (" + Format(CInt(F_Mynumber), "#,##0.00") + ") " + Myq_unit_code + "****"

        End If
        '        BahtOnly = Baht & Satang
    End Function
    '*******************************************
    ' Converts a number from 100-999 into text *
    '*******************************************
    Private Function GetHundreds(ByVal MyNumber)
        Dim Result As String

        If Val(MyNumber) = 0 Then Exit Function
        MyNumber = Right("000" & MyNumber, 3)

        'Convert the hundreds place
        If Mid(MyNumber, 1, 1) <> "0" Then
            Result = GetDigit(Mid(MyNumber, 1, 1)) & " HUNDRED "
        End If

        'Convert the tens and ones place
        If Mid(MyNumber, 2, 1) <> "0" Then
            Result = Result & GetTens(Mid(MyNumber, 2))
        Else
            Result = Result & GetDigit(Mid(MyNumber, 3))
        End If

        GetHundreds = Result
    End Function
    '*********************************************
    ' Converts a number from 10 to 99 into text. *
    '*********************************************
    Private Function GetTens(ByVal TensText)
        Dim Result As String

        Result = "" 'null out the temporary function value
        If Val(Left(TensText, 1)) = 1 Then ' If value between 10-19
            Select Case Val(TensText)
                Case 10 : Result = "TEN"
                Case 11 : Result = "ELEVEN"
                Case 12 : Result = "TWELVE"
                Case 13 : Result = "THIRTEEN"
                Case 14 : Result = "FOURTEEN"
                Case 15 : Result = "FIFTEEN"
                Case 16 : Result = "SIXTEEN"
                Case 17 : Result = "SEVENTEEN"
                Case 18 : Result = "EIGHTEEN"
                Case 19 : Result = "NINETEEN"
                Case Else
            End Select
        Else ' If value between 20-99
            Select Case Val(Left(TensText, 1))
                Case 2 : Result = "TWENTY "
                Case 3 : Result = "THIRTY "
                Case 4 : Result = "FORTY "
                Case 5 : Result = "FIFTY "
                Case 6 : Result = "SIXTY "
                Case 7 : Result = "SEVENTY "
                Case 8 : Result = "EIGHTY "
                Case 9 : Result = "NINETY "
                Case Else
            End Select
            Result = Result & GetDigit _
            (Right(TensText, 1)) 'Retrieve ones place
        End If
        GetTens = Result
    End Function
    Private Function GetTenscheck(ByVal TensText)
        Dim Result As String

        Result = "" 'null out the temporary function value
        If Val(Left(TensText, 1)) = 1 Then ' If value between 10-19
            Select Case Val(TensText)
                Case 10 : Result = "ONE ZERO"
                Case 11 : Result = "ONE ONE"
                Case 12 : Result = "ONE TWO"
                Case 13 : Result = "ONE THREE"
                Case 14 : Result = "ONE FOUR"
                Case 15 : Result = "ONE FIVE"
                Case 16 : Result = "ONE SIX"
                Case 17 : Result = "ONE SEVEN"
                Case 18 : Result = "ONE EIGHT"
                Case 19 : Result = "ONE NINE"
                Case Else
            End Select
        Else ' If value between 20-99
            Select Case Val(Left(TensText, 1))
                Case 0 : Result = "ZERO "
                Case 2 : Result = "TWO "
                Case 3 : Result = "THREE "
                Case 4 : Result = "FOUR "
                Case 5 : Result = "FIVE "
                Case 6 : Result = "SIX "
                Case 7 : Result = "SEVEN "
                Case 8 : Result = "EIGHT "
                Case 9 : Result = "NINE "
                Case Else
            End Select
            Result = Result & GetDigitcheck(Right(TensText, 1)) 'Retrieve ones place

            'Select Case Val(TensText)
            '    Case 1

            '    Case 2

            '    Case 3

            '    Case 4

            'End Select
        End If
        GetTenscheck = Result
    End Function
    '*******************************************
    ' Converts a number from 1 to 9 into text. *
    '*******************************************
    Private Function GetDigit(ByVal Digit)
        Select Case Val(Digit)
            Case 1 : GetDigit = "ONE"
            Case 2 : GetDigit = "TWO"
            Case 3 : GetDigit = "THREE"
            Case 4 : GetDigit = "FOUR"
            Case 5 : GetDigit = "FIVE"
            Case 6 : GetDigit = "SIX"
            Case 7 : GetDigit = "SEVEN"
            Case 8 : GetDigit = "EIGHT"
            Case 9 : GetDigit = "NINE"
            Case Else : GetDigit = ""
        End Select
    End Function
    Private Function GetDigitcheck(ByVal Digit)
        Select Case Val(Digit)
            Case 0 : GetDigitcheck = "ZERO"
            Case 1 : GetDigitcheck = "ONE"
            Case 2 : GetDigitcheck = "TWO"
            Case 3 : GetDigitcheck = "THREE"
            Case 4 : GetDigitcheck = "FOUR"
            Case 5 : GetDigitcheck = "FIVE"
            Case 6 : GetDigitcheck = "SIX"
            Case 7 : GetDigitcheck = "SEVEN"
            Case 8 : GetDigitcheck = "EIGHT"
            Case 9 : GetDigitcheck = "NINE"
            Case Else : GetDigitcheck = ""
        End Select
    End Function

    Function Check_Point(ByVal MFunc_Quantity) As Boolean
        Dim Count_Quantity As Decimal
        Select Case MFunc_Quantity
            Case Is = ""
                Return False
            Case Is <> ""
                Count_Quantity = (CDec(MFunc_Quantity) * 100) Mod 100
                If Count_Quantity > 0 Then
                    Return True
                Else
                    Return False
                End If
        End Select

    End Function
    '===========================================
    'ใช้อันนี้
    Public Function confor_mat(ByVal d_date As Date) As String
        Dim str_for As String = Format(d_date, "d/MM/yyyy")
        Return str_for
    End Function

    Public Function Get_DateTime_rpt(ByVal val As String) As DateTime
        If val Is Nothing Then
            Return Now
        Else
            val = val.ToLower().Trim()
            If (val.StartsWith("today")) Then

                If val = "today" Then
                    Return DateTime.Today
                End If
                Try
                    Dim htValues As Hashtable
                    htValues = GetAllValues_rpt(val.Substring(5))
                    Dim strTimeVal As String = htValues("TimeVal").ToString()
                    Select Case strTimeVal

                        Case "Days"
                            Dim days As Double = Convert.ToDouble(htValues("Operator").ToString() + htValues("Number").ToString())
                            Return DateTime.Today.AddDays(days)
                        Case "Months"
                            Dim Months As Int32 = Convert.ToInt32(htValues("Operator").ToString() + htValues("Number").ToString())
                            Return DateTime.Today.AddMonths(Months)
                        Case "Years"
                            Dim years As Int32 = Convert.ToInt32(htValues("Operator").ToString() + htValues("Number").ToString())
                            Return DateTime.Today.AddYears(years)
                        Case Else
                            Return DateTime.Today
                    End Select
                Catch ex As Exception
                    Return DateTime.Today
                End Try

            End If
            If val <> "" Then
                Return Convert.ToDateTime(val)
            End If
            Return New DateTime
        End If

    End Function

    Public Function GetAllValues_rpt(ByVal val As String) As Hashtable

        val = val.Trim()
        Dim ht As Hashtable = New Hashtable(3)
        Dim strMember As String = ""
        strMember = val.Substring(0, 1)
        If strMember = "+" Or strMember = "-" Then
            ht.Add("Operator", strMember)
        End If

        Dim nEndChars As Int32 = 0
        strMember = val.Substring(1).Trim()

        If strMember.EndsWith("days") Or strMember.EndsWith("day") Then
            ht.Add("TimeVal", "Days")
            If strMember.EndsWith("days") Then
                nEndChars = 4
            Else
                nEndChars = 3
            End If
        ElseIf strMember.EndsWith("months") Or strMember.EndsWith("month") Then
            ht.Add("TimeVal", "Months")
            If strMember.EndsWith("months") Then
                nEndChars = 6
            Else
                nEndChars = 5
            End If

        ElseIf strMember.EndsWith("years") Or strMember.EndsWith("year") Then
            ht.Add("TimeVal", "Years")
            If strMember.EndsWith("years") Then
                nEndChars = 5
            Else
                nEndChars = 4
            End If
        Else
            ht.Add("TimeVal", "Days")
        End If

        strMember = strMember.Substring(0, (strMember.Length - nEndChars))
        strMember = strMember.Trim()
        ht.Add("Number", strMember)

        Return ht
    End Function

    'function check updatePrintTotal
    Public Function updatePrintTotal(ByVal Rpt_INVH_RUN_AUTO As String, ByVal Rpt_TOTAL As String) As Boolean
        Dim strEDIConn As String = ConfigurationManager.ConnectionStrings("OriginConnection").ConnectionString

        If Rpt_INVH_RUN_AUTO <> "" And Rpt_TOTAL <> "" Then
            Dim ds As New DataSet
            ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, "sp_update_totalPrintPage", New SqlParameter("@INVH_RUN_AUTO", CommonUtility.Get_StringValue(Rpt_INVH_RUN_AUTO)), New SqlParameter("@TOTAL", Rpt_TOTAL))
            Return True
        Else
            Return False
        End If
        
    End Function

    'function ตรวจเรื่องวัน DEPARTURE_DATE เกี่ยวกับ Issued ทั้ง 5 form
    Public Sub checkIssued_date(ByVal _numINVH_RUN_AUTO As String, ByVal _strIssued As String)
        Dim strEDIConn As String = ConfigurationManager.ConnectionStrings("OriginConnection").ConnectionString

        Dim _md As Integer

        If _numINVH_RUN_AUTO <> "" Then
            'Dim ds As New DataSet
            _md = SqlHelper.ExecuteNonQuery(strEDIConn, CommandType.StoredProcedure, "viCheckIssuedDatePrint_NewDS", New SqlParameter("@INVH_RUN_AUTO", CommonUtility.Get_StringValue(_numINVH_RUN_AUTO)), New SqlParameter("@CheckIssued", _strIssued))

        End If
    End Sub

    Public Sub checkUpdate_user(ByVal _numINVH_RUN_AUTO As String, ByVal _strUser As String, ByVal _CheckAll As String)
        Dim strEDIConn As String = ConfigurationManager.ConnectionStrings("OriginConnection").ConnectionString

        Dim _md As Integer

        If _numINVH_RUN_AUTO <> "" Then
            'Dim ds As New DataSet
            _md = SqlHelper.ExecuteNonQuery(strEDIConn, CommandType.StoredProcedure, "viCheckUpdateByUserPrint_NewDS", New SqlParameter("@INVH_RUN_AUTO", CommonUtility.Get_StringValue(_numINVH_RUN_AUTO)), New SqlParameter("@UserPrintForm", _strUser), New SqlParameter("@CheckAll", _CheckAll))

        End If
    End Sub
    ''function ตรวจเรื่องวัน DEPARTURE_DATE เกี่ยวกับ Issued ทั้ง 5 form
    'Public Function checkIssued_date(ByVal _numINVH_RUN_AUTO As String, ByVal _date As Date) As Integer
    '    Dim strEDIConn As String = ConfigurationManager.ConnectionStrings("OriginConnection").ConnectionString

    '    If _numINVH_RUN_AUTO <> "" Then
    '        Dim ds As New DataSet
    '        ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, "viCheckIssuedDatePrint_NewDS", New SqlParameter("@INVH_RUN_AUTO", CommonUtility.Get_StringValue(_numINVH_RUN_AUTO)), New SqlParameter("@DEPARTURE_DATE", _date))

    '        If ds.Tables(0).Rows.Count > 0 Then
    '            Return 1 'check issued
    '        Else
    '            Return 2
    '        End If
    '    Else
    '        Return 3
    '    End If
    'End Function

    Public Function Check_NULLALL(ByVal _strAll As Object) As String
        Dim _ReStr As String = ""
        If (Not _strAll = String.Empty = True) And CommonUtility.Get_StringValue(_strAll) <> "" Then
            If _strAll.ToString.Trim <> "" Then
                _ReStr = _strAll
                Return _ReStr
            Else
                Return _ReStr
            End If
        Else
            Return _ReStr
        End If
    End Function

    'check แยก invoice
    Public Function Check_numinvoiceAll(ByVal v0 As String, ByVal v1 As String, ByVal v2 As String, ByVal v3 As String, ByVal v4 As String) As Integer
        Dim iv As Integer
        Dim reInteger As Integer = 0
        Dim str_ As String = ""
        str_ = v0 & ";" & v1 & ";" & v2 & ";" & v3 & ";" & v4 & ";"
        Dim arr_str As Array = str_.Split(";")

        For iv = 0 To 4
            If arr_str(iv).ToString.Trim <> "" Then
                reInteger += 1
            End If
        Next
        Return reInteger
    End Function

    'check form2_1 สมัครใจ
    Public Function check2_1request(ByVal _tax As String) As Boolean
        Dim strEDIConn As String = ConfigurationManager.ConnectionStrings("OriginConnection").ConnectionString

        If _tax <> "" Then
            Dim ds As New DataSet
            ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, "viCheck2_1Tax_NewDS", New SqlParameter("@company_taxno", CommonUtility.Get_StringValue(_tax)))
            If ds.Tables(0).Rows.Count > 0 Then
                'ds.Tables(0).Rows(0).Item("Company_Taxno") = _tax
                Return True
            Else
                Return False
            End If

        Else
            Return False
        End If

    End Function

    'check ค่าเพื่อ ใส่ข้อความไว้ก่อน HS.Code
    Public Function CarTxt(ByVal _SINGLE_COUNTRY_CONTENT As String, ByVal _InvoiceDetailTH As String) As String
        Dim str_txt As String = ""

        '_SINGLE_COUNTRY_CONTENT --1=บริษัท Car,0=ไม่ใช่ Car
        Select Case _SINGLE_COUNTRY_CONTENT
            Case "1"
                Select Case _InvoiceDetailTH.Trim
                    Case Is = ""
                        str_txt = ""
                    Case Else
                        str_txt = _InvoiceDetailTH & vbCrLf
                End Select

            Case Else
                str_txt = ""
        End Select
        Return str_txt
    End Function
End Module
