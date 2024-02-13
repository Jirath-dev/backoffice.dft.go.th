Imports DataDynamics.ActiveReports
Imports DataDynamics.ActiveReports.Document

Imports Microsoft.ApplicationBlocks.Data
Imports System.Data.SqlClient
Imports System.Data
Imports System.Text
Imports DFT.Dotnetnuke.ClassLibrary
Imports ReportPrintClass
Public Class rpt3_ediFORMTP_ESS_pr
    Dim Cpage As Integer
    Dim CpageNum As Integer

    'by rut Title New Begin-----------------
    Dim Str_TitlePage As String
    'by rut Title New End-------------------
    'by rut ใช้กับ page end เพื่อกำหนด
    Dim head_height As String
    Private Sub PageHeader1_Format(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PageHeader1.Format
        CpageNum += 1
        txtreference_code2_Temp.Text = txtreference_code2.Text
        '===============================================================

        'txtPage2.Text = Cpage
        '===============================================================
        Head_Checkcompany_v1()
        Head_Checkdestination_v2()

        'by rut Title New Begin-------------------------------------------
        Dim Str_TitleHead As String
        Str_TitleHead = txtTitleMain.Text
        Select Case PageNumber
            Case 1
                If Str_TitleHead <> Str_TitlePage Then
                    txtTitleHead.Text = Str_TitleHead
                End If
            Case Else
                txtTitleHead.Text = Str_TitlePage
        End Select

        If CommonUtility.Get_StringValue(txtSINGLE_COUNTRY_CONTENT.Value) = "1" And CommonUtility.Get_StringValue(txtTitleMain.Text) <> "" Then
            txtTitleHead.Visible = True
        End If
        'by rut Title New End-------------------------------------------
        'by rut sign image ลายเซ็น กรรมการและผู้รับมอบ
        'check รายการก่อนว่ามีการใช้ Seal Sign หรือไม่ ถ้าไม่มีก็ข้ามไป
        Dim dsESS As DataSet = search_imageForm(txtinvh_run_auto.Text)
        If dsESS.Tables(0).Rows.Count > 0 Then
            CaseCheck_numimagesSign(dsESS)
            CaseCheck_ApproveSign(dsESS)
        End If
    End Sub
    'company
    Sub Head_Checkcompany_v1()
        If Mid(txtdest_remark.Text, 1, 3) = "C/O" Then
            txtCompany_Check_1.Text = txtob_address.Text & IIf(Check_NULLALL(txtNewEmail_ch01.Text) <> "", " E-mail: " & txtNewEmail_ch01.Text, "") & " CARE OF " & txtcompany_name.Text & " " & txtcompany_address.Text _
                               & " " & txtcompany_province.Text & " " & txtcompany_country.Text &
                               IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") &
                               IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") &
                               IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID:  " & txtcompany_taxno.Text, "") &
                               IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")
        ElseIf Mid(txtdest_remark.Text, 1, 3) = "O/B" Then
            txtCompany_Check_1.Text = txtcompany_name.Text & " " & txtcompany_address.Text & " " & txtcompany_province.Text _
                                & " " & txtcompany_country.Text & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") &
                                IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") _
                                & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID: " & txtcompany_taxno.Text, "") &
                                IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "") &
                                IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtob_address.Text)) <> "", " ON BEHALF OF " & txtob_address.Text, "") & IIf(Check_NULLALL(txtNewEmail_ch01.Text) <> "", " E-mail: " & txtNewEmail_ch01.Text, "")
        ElseIf Mid(txtdest_remark.Text, 2, 1) <> "/" Then
            txtCompany_Check_1.Text = txtcompany_name.Text & " " & txtcompany_address.Text & " " & txtcompany_province.Text _
                                & " " & txtcompany_country.Text & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") &
                                IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") _
                                & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID: " & txtcompany_taxno.Text, "") &
                                IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")

        ElseIf Mid(txtdest_remark.Text, 1, 5) = "O/TOB" Then
            txtCompany_Check_1.Text = txtcompany_name.Text & " " & txtcompany_address.Text & " " & txtcompany_province.Text _
                            & " " & txtcompany_country.Text &
                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") &
                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") _
                            & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID: " & txtcompany_taxno.Text, "") &
                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "") & " " & txtob_address.Text

        ElseIf Mid(txtdest_remark.Text, 1, 5) = "O/TCO" Then
            txtCompany_Check_1.Text = txtob_address.Text & " " & txtcompany_name.Text & " " & txtcompany_address.Text & " " & txtcompany_province.Text _
                            & " " & txtcompany_country.Text &
                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") &
                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") _
                            & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID: " & txtcompany_taxno.Text, "") &
                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")

        End If
    End Sub
    Sub Head_Checkdestination_v2()
        If Mid(txtdest_remark1.Text, 1, 3) = "C/O" Then
            txtdestination_Check2.Text = txtob_dest_address.Text & " CARE OF " & txtdestination_company.Text & " " & txtdestination_address.Text _
                                & " " & txtdestination_province.Text & " " & txtdest_Receive_country.Text &
                                IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") &
                                IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID:  " & txtdestination_taxid.Text, "") &
                                IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "")

        ElseIf Mid(txtdest_remark1.Text, 1, 3) = "O/B" Then
            txtdestination_Check2.Text = txtdestination_company.Text & " " & txtdestination_address.Text & " " &
                                    txtdestination_province.Text & " " & txtdest_Receive_country.Text &
                                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                    & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                    " ON BEHALF OF " & txtob_dest_address.Text & IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "")

        ElseIf Mid(txtdest_remark1.Text, 2, 1) <> "/" Then
            txtdestination_Check2.Text = txtdestination_company.Text & " " & txtdestination_address.Text & " " & txtdestination_province.Text _
                                & " " & txtdest_Receive_country.Text &
                                IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "")

        ElseIf Mid(txtdest_remark1.Text, 1, 5) = "O/TOB" Then
            txtdestination_Check2.Text = txtdestination_company.Text & " " & txtdestination_address.Text & " " & txtdestination_province.Text _
                            & " " & txtdest_Receive_country.Text &
                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                            & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                            IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "") & " " & txtob_dest_address.Text

        ElseIf Mid(txtdest_remark1.Text, 1, 5) = "O/TCO" Then
            txtdestination_Check2.Text = txtob_dest_address.Text & " " & txtdestination_company.Text & " " & txtdestination_address.Text & " " & txtdestination_province.Text _
                            & " " & txtdest_Receive_country.Text &
                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                            & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                            IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "")

        End If
    End Sub
    Function confor_mat(ByVal d_date As Date) As String
        Dim str_for As String = Format(d_date, "d/MM/yyyy")
        Return str_for
    End Function

    ''ไม่มี invoice ต่างประเทศ
    'Sub CallInvoiceCheck() 'Check Invoince
    '    If CommonUtility.Get_String(txtinvoice_no1.Text) <> "" And CommonUtility.Get_String(txtinvoice_date1.Text) <> "" And CommonUtility.Get_String(txtinvoice_no2.Text) = "" And CommonUtility.Get_String(txtinvoice_date2.Text) = "" And CommonUtility.Get_String(txtinvoice_no3.Text) = "" And CommonUtility.Get_String(txtinvoice_date3.Text) = "" And CommonUtility.Get_String(txtinvoice_no4.Text) = "" And CommonUtility.Get_String(txtinvoice_date4.Text) = "" And CommonUtility.Get_String(txtinvoice_no5.Text) = "" And CommonUtility.Get_String(txtinvoice_date5.Text) = "" Then
    '        '1 invoice
    '        txtTolInvoice.Text = txtinvoice_no1.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date1.Text) <> "", confor_mat(CDate(txtinvoice_date1.Text).Date), Nothing)
    '    ElseIf CommonUtility.Get_String(txtinvoice_no1.Text) <> "" And CommonUtility.Get_String(txtinvoice_date1.Text) <> "" And CommonUtility.Get_String(txtinvoice_no2.Text) <> "" And CommonUtility.Get_String(txtinvoice_date2.Text) <> "" And CommonUtility.Get_String(txtinvoice_no3.Text) = "" And CommonUtility.Get_String(txtinvoice_date3.Text) = "" And CommonUtility.Get_String(txtinvoice_no4.Text) = "" And CommonUtility.Get_String(txtinvoice_date4.Text) = "" And CommonUtility.Get_String(txtinvoice_no5.Text) = "" And CommonUtility.Get_String(txtinvoice_date5.Text) = "" Then
    '        '2 invoice
    '        txtTolInvoice.Text = txtinvoice_no1.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date1.Text) <> "", confor_mat(CDate(txtinvoice_date1.Text).Date), Nothing) & vbNewLine _
    '                            & txtinvoice_no2.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date2.Text) <> "", confor_mat(CDate(txtinvoice_date2.Text).Date), Nothing)

    '    ElseIf CommonUtility.Get_String(txtinvoice_no1.Text) <> "" And CommonUtility.Get_String(txtinvoice_date1.Text) <> "" And CommonUtility.Get_String(txtinvoice_no2.Text) <> "" And CommonUtility.Get_String(txtinvoice_date2.Text) <> "" And CommonUtility.Get_String(txtinvoice_no3.Text) <> "" And CommonUtility.Get_String(txtinvoice_date3.Text) <> "" And CommonUtility.Get_String(txtinvoice_no4.Text) = "" And CommonUtility.Get_String(txtinvoice_date4.Text) = "" And CommonUtility.Get_String(txtinvoice_no5.Text) = "" And CommonUtility.Get_String(txtinvoice_date5.Text) = "" Then
    '        '3 invoice
    '        txtTolInvoice.Text = txtinvoice_no1.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date1.Text) <> "", confor_mat(CDate(txtinvoice_date1.Text).Date), Nothing) & vbNewLine _
    '                            & txtinvoice_no2.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date2.Text) <> "", confor_mat(CDate(txtinvoice_date2.Text).Date), Nothing) & vbNewLine _
    '                            & txtinvoice_no3.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date3.Text) <> "", confor_mat(CDate(txtinvoice_date3.Text).Date), Nothing)

    '    ElseIf CommonUtility.Get_String(txtinvoice_no1.Text) <> "" And CommonUtility.Get_String(txtinvoice_date1.Text) <> "" And CommonUtility.Get_String(txtinvoice_no2.Text) <> "" And CommonUtility.Get_String(txtinvoice_date2.Text) <> "" And CommonUtility.Get_String(txtinvoice_no3.Text) <> "" And CommonUtility.Get_String(txtinvoice_date3.Text) <> "" And CommonUtility.Get_String(txtinvoice_no4.Text) <> "" And CommonUtility.Get_String(txtinvoice_date4.Text) <> "" And CommonUtility.Get_String(txtinvoice_no5.Text) = "" And CommonUtility.Get_String(txtinvoice_date5.Text) = "" Then
    '        '4 invoice
    '        txtTolInvoice.Text = txtinvoice_no1.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date1.Text) <> "", confor_mat(CDate(txtinvoice_date1.Text).Date), Nothing) & vbNewLine _
    '                            & txtinvoice_no2.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date2.Text) <> "", confor_mat(CDate(txtinvoice_date2.Text).Date), Nothing) & vbNewLine _
    '                            & txtinvoice_no3.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date3.Text) <> "", confor_mat(CDate(txtinvoice_date3.Text).Date), Nothing) & vbNewLine _
    '                            & txtinvoice_no4.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date4.Text) <> "", confor_mat(CDate(txtinvoice_date4.Text).Date), Nothing)

    '    ElseIf CommonUtility.Get_String(txtinvoice_no1.Text) <> "" And CommonUtility.Get_String(txtinvoice_date1.Text) <> "" And CommonUtility.Get_String(txtinvoice_no2.Text) <> "" And CommonUtility.Get_String(txtinvoice_date2.Text) <> "" And CommonUtility.Get_String(txtinvoice_no3.Text) <> "" And CommonUtility.Get_String(txtinvoice_date3.Text) <> "" And CommonUtility.Get_String(txtinvoice_no4.Text) <> "" And CommonUtility.Get_String(txtinvoice_date4.Text) <> "" And CommonUtility.Get_String(txtinvoice_no5.Text) <> "" And CommonUtility.Get_String(txtinvoice_date5.Text) <> "" Then
    '        '5 invoice
    '        txtTolInvoice.Text = txtinvoice_no1.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date1.Text) <> "", confor_mat(CDate(txtinvoice_date1.Text).Date), Nothing) & vbNewLine _
    '                            & txtinvoice_no2.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date2.Text) <> "", confor_mat(CDate(txtinvoice_date2.Text).Date), Nothing) & vbNewLine _
    '                            & txtinvoice_no3.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date3.Text) <> "", confor_mat(CDate(txtinvoice_date3.Text).Date), Nothing) & vbNewLine _
    '                            & txtinvoice_no4.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date4.Text) <> "", confor_mat(CDate(txtinvoice_date4.Text).Date), Nothing) & vbNewLine _
    '                            & txtinvoice_no5.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date5.Text) <> "", confor_mat(CDate(txtinvoice_date5.Text).Date), Nothing)

    '    End If

    'End Sub
    'Function CallInvoiceCheckF() As String 'Check Invoince
    '    Dim str_invoice As String
    '    If CommonUtility.Get_String(txtinvoice_no1.Text) <> "" And CommonUtility.Get_String(txtinvoice_date1.Text) <> "" And CommonUtility.Get_String(txtinvoice_no2.Text) = "" And CommonUtility.Get_String(txtinvoice_date2.Text) = "" And CommonUtility.Get_String(txtinvoice_no3.Text) = "" And CommonUtility.Get_String(txtinvoice_date3.Text) = "" And CommonUtility.Get_String(txtinvoice_no4.Text) = "" And CommonUtility.Get_String(txtinvoice_date4.Text) = "" And CommonUtility.Get_String(txtinvoice_no5.Text) = "" And CommonUtility.Get_String(txtinvoice_date5.Text) = "" Then
    '        '1 invoice
    '        str_invoice = txtinvoice_no1.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date1.Text) <> "", confor_mat(CDate(txtinvoice_date1.Text).Date), Nothing)
    '    ElseIf CommonUtility.Get_String(txtinvoice_no1.Text) <> "" And CommonUtility.Get_String(txtinvoice_date1.Text) <> "" And CommonUtility.Get_String(txtinvoice_no2.Text) <> "" And CommonUtility.Get_String(txtinvoice_date2.Text) <> "" And CommonUtility.Get_String(txtinvoice_no3.Text) = "" And CommonUtility.Get_String(txtinvoice_date3.Text) = "" And CommonUtility.Get_String(txtinvoice_no4.Text) = "" And CommonUtility.Get_String(txtinvoice_date4.Text) = "" And CommonUtility.Get_String(txtinvoice_no5.Text) = "" And CommonUtility.Get_String(txtinvoice_date5.Text) = "" Then
    '        '2 invoice
    '        str_invoice = txtinvoice_no1.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date1.Text) <> "", confor_mat(CDate(txtinvoice_date1.Text).Date), Nothing) & vbNewLine _
    '                            & txtinvoice_no2.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date2.Text) <> "", confor_mat(CDate(txtinvoice_date2.Text).Date), Nothing)

    '    ElseIf CommonUtility.Get_String(txtinvoice_no1.Text) <> "" And CommonUtility.Get_String(txtinvoice_date1.Text) <> "" And CommonUtility.Get_String(txtinvoice_no2.Text) <> "" And CommonUtility.Get_String(txtinvoice_date2.Text) <> "" And CommonUtility.Get_String(txtinvoice_no3.Text) <> "" And CommonUtility.Get_String(txtinvoice_date3.Text) <> "" And CommonUtility.Get_String(txtinvoice_no4.Text) = "" And CommonUtility.Get_String(txtinvoice_date4.Text) = "" And CommonUtility.Get_String(txtinvoice_no5.Text) = "" And CommonUtility.Get_String(txtinvoice_date5.Text) = "" Then
    '        '3 invoice
    '        str_invoice = txtinvoice_no1.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date1.Text) <> "", confor_mat(CDate(txtinvoice_date1.Text).Date), Nothing) & vbNewLine _
    '                            & txtinvoice_no2.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date2.Text) <> "", confor_mat(CDate(txtinvoice_date2.Text).Date), Nothing) & vbNewLine _
    '                            & txtinvoice_no3.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date3.Text) <> "", confor_mat(CDate(txtinvoice_date3.Text).Date), Nothing)

    '    ElseIf CommonUtility.Get_String(txtinvoice_no1.Text) <> "" And CommonUtility.Get_String(txtinvoice_date1.Text) <> "" And CommonUtility.Get_String(txtinvoice_no2.Text) <> "" And CommonUtility.Get_String(txtinvoice_date2.Text) <> "" And CommonUtility.Get_String(txtinvoice_no3.Text) <> "" And CommonUtility.Get_String(txtinvoice_date3.Text) <> "" And CommonUtility.Get_String(txtinvoice_no4.Text) <> "" And CommonUtility.Get_String(txtinvoice_date4.Text) <> "" And CommonUtility.Get_String(txtinvoice_no5.Text) = "" And CommonUtility.Get_String(txtinvoice_date5.Text) = "" Then
    '        '4 invoice
    '        str_invoice = txtinvoice_no1.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date1.Text) <> "", confor_mat(CDate(txtinvoice_date1.Text).Date), Nothing) & vbNewLine _
    '                            & txtinvoice_no2.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date2.Text) <> "", confor_mat(CDate(txtinvoice_date2.Text).Date), Nothing) & vbNewLine _
    '                            & txtinvoice_no3.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date3.Text) <> "", confor_mat(CDate(txtinvoice_date3.Text).Date), Nothing) & vbNewLine _
    '                            & txtinvoice_no4.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date4.Text) <> "", confor_mat(CDate(txtinvoice_date4.Text).Date), Nothing)

    '    ElseIf CommonUtility.Get_String(txtinvoice_no1.Text) <> "" And CommonUtility.Get_String(txtinvoice_date1.Text) <> "" And CommonUtility.Get_String(txtinvoice_no2.Text) <> "" And CommonUtility.Get_String(txtinvoice_date2.Text) <> "" And CommonUtility.Get_String(txtinvoice_no3.Text) <> "" And CommonUtility.Get_String(txtinvoice_date3.Text) <> "" And CommonUtility.Get_String(txtinvoice_no4.Text) <> "" And CommonUtility.Get_String(txtinvoice_date4.Text) <> "" And CommonUtility.Get_String(txtinvoice_no5.Text) <> "" And CommonUtility.Get_String(txtinvoice_date5.Text) <> "" Then
    '        '5 invoice
    '        str_invoice = txtinvoice_no1.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date1.Text) <> "", confor_mat(CDate(txtinvoice_date1.Text).Date), Nothing) & vbNewLine _
    '                            & txtinvoice_no2.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date2.Text) <> "", confor_mat(CDate(txtinvoice_date2.Text).Date), Nothing) & vbNewLine _
    '                            & txtinvoice_no3.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date3.Text) <> "", confor_mat(CDate(txtinvoice_date3.Text).Date), Nothing) & vbNewLine _
    '                            & txtinvoice_no4.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date4.Text) <> "", confor_mat(CDate(txtinvoice_date4.Text).Date), Nothing) & vbNewLine _
    '                            & txtinvoice_no5.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date5.Text) <> "", confor_mat(CDate(txtinvoice_date5.Text).Date), Nothing)

    '    End If
    '    Return str_invoice
    'End Function

    'มี invoice ต่างประเทศ
    'มี invoice ต่างประเทศ
    Sub CallInvoiceCheck() 'Check Invoince
        Select Case CommonUtility.Get_StringValue(Check_NULLALL(txtNumInvoice.Text))
            Case Is <> ""
                txtTolInvoice.Text = CommonUtility.Get_StringValue(txtNumInvoice.Text)
            Case Else
                If CommonUtility.Get_String(txtinvoice_no1.Text) <> "" And CommonUtility.Get_String(txtinvoice_date1.Text) <> "" And CommonUtility.Get_String(txtinvoice_no2.Text) = "" And CommonUtility.Get_String(txtinvoice_date2.Text) = "" And CommonUtility.Get_String(txtinvoice_no3.Text) = "" And CommonUtility.Get_String(txtinvoice_date3.Text) = "" And CommonUtility.Get_String(txtinvoice_no4.Text) = "" And CommonUtility.Get_String(txtinvoice_date4.Text) = "" And CommonUtility.Get_String(txtinvoice_no5.Text) = "" And CommonUtility.Get_String(txtinvoice_date5.Text) = "" Then
                    '1 invoice
                    txtTolInvoice.Text = txtinvoice_no1.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date1.Text) <> "", confor_mat(CDate(txtinvoice_date1.Text).Date), Nothing)
                ElseIf CommonUtility.Get_String(txtinvoice_no1.Text) <> "" And CommonUtility.Get_String(txtinvoice_date1.Text) <> "" And CommonUtility.Get_String(txtinvoice_no2.Text) <> "" And CommonUtility.Get_String(txtinvoice_date2.Text) <> "" And CommonUtility.Get_String(txtinvoice_no3.Text) = "" And CommonUtility.Get_String(txtinvoice_date3.Text) = "" And CommonUtility.Get_String(txtinvoice_no4.Text) = "" And CommonUtility.Get_String(txtinvoice_date4.Text) = "" And CommonUtility.Get_String(txtinvoice_no5.Text) = "" And CommonUtility.Get_String(txtinvoice_date5.Text) = "" Then
                    '2 invoice
                    txtTolInvoice.Text = txtinvoice_no1.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date1.Text) <> "", confor_mat(CDate(txtinvoice_date1.Text).Date), Nothing) & vbNewLine _
                                        & txtinvoice_no2.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date2.Text) <> "", confor_mat(CDate(txtinvoice_date2.Text).Date), Nothing)

                ElseIf CommonUtility.Get_String(txtinvoice_no1.Text) <> "" And CommonUtility.Get_String(txtinvoice_date1.Text) <> "" And CommonUtility.Get_String(txtinvoice_no2.Text) <> "" And CommonUtility.Get_String(txtinvoice_date2.Text) <> "" And CommonUtility.Get_String(txtinvoice_no3.Text) <> "" And CommonUtility.Get_String(txtinvoice_date3.Text) <> "" And CommonUtility.Get_String(txtinvoice_no4.Text) = "" And CommonUtility.Get_String(txtinvoice_date4.Text) = "" And CommonUtility.Get_String(txtinvoice_no5.Text) = "" And CommonUtility.Get_String(txtinvoice_date5.Text) = "" Then
                    '3 invoice
                    txtTolInvoice.Text = txtinvoice_no1.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date1.Text) <> "", confor_mat(CDate(txtinvoice_date1.Text).Date), Nothing) & vbNewLine _
                                        & txtinvoice_no2.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date2.Text) <> "", confor_mat(CDate(txtinvoice_date2.Text).Date), Nothing) & vbNewLine _
                                        & txtinvoice_no3.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date3.Text) <> "", confor_mat(CDate(txtinvoice_date3.Text).Date), Nothing)

                ElseIf CommonUtility.Get_String(txtinvoice_no1.Text) <> "" And CommonUtility.Get_String(txtinvoice_date1.Text) <> "" And CommonUtility.Get_String(txtinvoice_no2.Text) <> "" And CommonUtility.Get_String(txtinvoice_date2.Text) <> "" And CommonUtility.Get_String(txtinvoice_no3.Text) <> "" And CommonUtility.Get_String(txtinvoice_date3.Text) <> "" And CommonUtility.Get_String(txtinvoice_no4.Text) <> "" And CommonUtility.Get_String(txtinvoice_date4.Text) <> "" And CommonUtility.Get_String(txtinvoice_no5.Text) = "" And CommonUtility.Get_String(txtinvoice_date5.Text) = "" Then
                    '4 invoice
                    txtTolInvoice.Text = txtinvoice_no1.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date1.Text) <> "", confor_mat(CDate(txtinvoice_date1.Text).Date), Nothing) & vbNewLine _
                                        & txtinvoice_no2.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date2.Text) <> "", confor_mat(CDate(txtinvoice_date2.Text).Date), Nothing) & vbNewLine _
                                        & txtinvoice_no3.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date3.Text) <> "", confor_mat(CDate(txtinvoice_date3.Text).Date), Nothing) & vbNewLine _
                                        & txtinvoice_no4.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date4.Text) <> "", confor_mat(CDate(txtinvoice_date4.Text).Date), Nothing)

                ElseIf CommonUtility.Get_String(txtinvoice_no1.Text) <> "" And CommonUtility.Get_String(txtinvoice_date1.Text) <> "" And CommonUtility.Get_String(txtinvoice_no2.Text) <> "" And CommonUtility.Get_String(txtinvoice_date2.Text) <> "" And CommonUtility.Get_String(txtinvoice_no3.Text) <> "" And CommonUtility.Get_String(txtinvoice_date3.Text) <> "" And CommonUtility.Get_String(txtinvoice_no4.Text) <> "" And CommonUtility.Get_String(txtinvoice_date4.Text) <> "" And CommonUtility.Get_String(txtinvoice_no5.Text) <> "" And CommonUtility.Get_String(txtinvoice_date5.Text) <> "" Then
                    '5 invoice
                    txtTolInvoice.Text = txtinvoice_no1.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date1.Text) <> "", confor_mat(CDate(txtinvoice_date1.Text).Date), Nothing) & vbNewLine _
                                        & txtinvoice_no2.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date2.Text) <> "", confor_mat(CDate(txtinvoice_date2.Text).Date), Nothing) & vbNewLine _
                                        & txtinvoice_no3.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date3.Text) <> "", confor_mat(CDate(txtinvoice_date3.Text).Date), Nothing) & vbNewLine _
                                        & txtinvoice_no4.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date4.Text) <> "", confor_mat(CDate(txtinvoice_date4.Text).Date), Nothing) & vbNewLine _
                                        & txtinvoice_no5.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date5.Text) <> "", confor_mat(CDate(txtinvoice_date5.Text).Date), Nothing)

                End If
        End Select
    End Sub
#Region "by rut 15-10-2012 ปรับเรื่อง invoice eng ให้แสดง invoice thai ด้วยเวลาเลือก third country"
    Function return_NewAll_InvoiceString() As String
        Dim str_invoice As String = ""
        Dim Tempth_invoices As String = Th_invoice()
        Dim TempEng_invoices As String = CommonUtility.Get_StringValue(txtNumInvoice.Text.ToUpper)

        'เทียบ invoice th ใน invoice eng ว่ามีหรือไม่ ถ้ามีอยู่ใน invoice eng ก็ไม่เอา invoice th ใส่เพิ่มเข้าไป
        Select Case Check_Th_invoiceOnly(TempEng_invoices)
            Case True 'กรณี เจอ invoice th อยู่ใน invoice eng
                'str_invoice = CommonUtility.Get_StringValue(txtNumInvoice.Text)
                str_invoice = Th_invoice() & vbNewLine & CommonUtility.Get_StringValue(txtNumInvoice.Text)

            Case False 'กรณี ไม่เจอ invoice th อยู่ใน invoice eng
                str_invoice = Th_invoice() & vbNewLine & CommonUtility.Get_StringValue(txtNumInvoice.Text)
        End Select

        Return str_invoice
    End Function
    'ปัญหา เวลาป้อน invoice th ในช่องเดียว 2 invoice แต่ตอนป้อน invoice eng ป้อนแยก หาไม่เจอ
    'invoice eng
    'ty7757576/-1
    '15/10/2012
    'ty7757576/-2
    '15/10/2012
    'ty7757576/-3
    '15/10/2012
    'Eng555-901
    '15/10/2012
    'Eng555-902
    '15/10/2012

    'invoice th
    'ty7757576/-1
    'ty7757576/-2
    Function Check_Th_invoiceOnly(ByVal By_invoiceEng As String) As Boolean
        Dim Tempth_invoices As Boolean = False
        Dim str_tempInvoiceTH As String = ""

        str_tempInvoiceTH = txtinvoice_no1.Text.ToUpper
        'check แค่ invoice1 อันเดียว
        If CommonUtility.Get_String(txtinvoice_no1.Text) <> "" And CommonUtility.Get_String(txtinvoice_date1.Text) <> "" Then
            '1 invoice
            Tempth_invoices = By_invoiceEng.Replace(" ", "").Contains(str_tempInvoiceTH.Replace(" ", ""))
        End If
        'เก็บไว้ก่อน
        'Select Case Check_numinvoiceAll(CommonUtility.Get_String(txtinvoice_no1.Text), CommonUtility.Get_String(txtinvoice_no2.Text), CommonUtility.Get_String(txtinvoice_no3.Text), CommonUtility.Get_String(txtinvoice_no4.Text), CommonUtility.Get_String(txtinvoice_no5.Text))
        '    Case 1
        '        If CommonUtility.Get_String(txtinvoice_no1.Text) <> "" And CommonUtility.Get_String(txtinvoice_date1.Text) <> "" Then
        '            '1 invoice
        '            Tempth_invoices = By_invoiceEng.Contains(str_tempInvoiceTH.Replace(" ", ""))
        '        End If
        '    Case 2
        '        If CommonUtility.Get_String(txtinvoice_no1.Text) <> "" And CommonUtility.Get_String(txtinvoice_date1.Text) <> "" And CommonUtility.Get_String(txtinvoice_no2.Text) <> "" And CommonUtility.Get_String(txtinvoice_date2.Text) <> "" Then
        '            '2 invoice
        '            Tempth_invoices = By_invoiceEng.Contains(str_tempInvoiceTH.Replace(" ", ""))
        '        End If
        '    Case 3
        '        If CommonUtility.Get_String(txtinvoice_no1.Text) <> "" And CommonUtility.Get_String(txtinvoice_date1.Text) <> "" And CommonUtility.Get_String(txtinvoice_no2.Text) <> "" And CommonUtility.Get_String(txtinvoice_date2.Text) <> "" And CommonUtility.Get_String(txtinvoice_no3.Text) <> "" And CommonUtility.Get_String(txtinvoice_date3.Text) <> "" Then
        '            '3 invoice
        '            Tempth_invoices = By_invoiceEng.Contains(str_tempInvoiceTH.Replace(" ", ""))
        '        End If
        '    Case 4
        '        If CommonUtility.Get_String(txtinvoice_no1.Text) <> "" And CommonUtility.Get_String(txtinvoice_date1.Text) <> "" And CommonUtility.Get_String(txtinvoice_no2.Text) <> "" And CommonUtility.Get_String(txtinvoice_date2.Text) <> "" And CommonUtility.Get_String(txtinvoice_no3.Text) <> "" And CommonUtility.Get_String(txtinvoice_date3.Text) <> "" And CommonUtility.Get_String(txtinvoice_no4.Text) <> "" And CommonUtility.Get_String(txtinvoice_date4.Text) <> "" Then
        '            '4 invoice
        '            Tempth_invoices = By_invoiceEng.Contains(str_tempInvoiceTH.Replace(" ", ""))
        '        End If
        '    Case 5
        '        If CommonUtility.Get_String(txtinvoice_no1.Text) <> "" And CommonUtility.Get_String(txtinvoice_date1.Text) <> "" And CommonUtility.Get_String(txtinvoice_no2.Text) <> "" And CommonUtility.Get_String(txtinvoice_date2.Text) <> "" And CommonUtility.Get_String(txtinvoice_no3.Text) <> "" And CommonUtility.Get_String(txtinvoice_date3.Text) <> "" And CommonUtility.Get_String(txtinvoice_no4.Text) <> "" And CommonUtility.Get_String(txtinvoice_date4.Text) <> "" And CommonUtility.Get_String(txtinvoice_no5.Text) <> "" And CommonUtility.Get_String(txtinvoice_date5.Text) <> "" Then
        '            '5 invoice
        '            Tempth_invoices = By_invoiceEng.Contains(str_tempInvoiceTH.Replace(" ", ""))
        '        End If
        'End Select

        Return Tempth_invoices
    End Function
    Function Th_invoice() As String
        Dim Tempth_invoices As String = ""
        Select Case Check_numinvoiceAll(CommonUtility.Get_String(txtinvoice_no1.Text), CommonUtility.Get_String(txtinvoice_no2.Text), CommonUtility.Get_String(txtinvoice_no3.Text), CommonUtility.Get_String(txtinvoice_no4.Text), CommonUtility.Get_String(txtinvoice_no5.Text))
            Case 1
                If CommonUtility.Get_String(txtinvoice_no1.Text) <> "" And CommonUtility.Get_String(txtinvoice_date1.Text) <> "" Then
                    '1 invoice
                    Tempth_invoices = txtinvoice_no1.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date1.Text) <> "", confor_mat(CDate(txtinvoice_date1.Value).Date), Nothing)
                End If
            Case 2
                If CommonUtility.Get_String(txtinvoice_no1.Text) <> "" And CommonUtility.Get_String(txtinvoice_date1.Text) <> "" And CommonUtility.Get_String(txtinvoice_no2.Text) <> "" And CommonUtility.Get_String(txtinvoice_date2.Text) <> "" Then
                    '2 invoice
                    Tempth_invoices = txtinvoice_no1.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date1.Text) <> "", confor_mat(CDate(txtinvoice_date1.Value).Date), Nothing) & vbNewLine _
                                        & txtinvoice_no2.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date2.Text) <> "", confor_mat(CDate(txtinvoice_date2.Value).Date), Nothing)
                End If
            Case 3
                If CommonUtility.Get_String(txtinvoice_no1.Text) <> "" And CommonUtility.Get_String(txtinvoice_date1.Text) <> "" And CommonUtility.Get_String(txtinvoice_no2.Text) <> "" And CommonUtility.Get_String(txtinvoice_date2.Text) <> "" And CommonUtility.Get_String(txtinvoice_no3.Text) <> "" And CommonUtility.Get_String(txtinvoice_date3.Text) <> "" Then
                    '3 invoice
                    Tempth_invoices = txtinvoice_no1.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date1.Text) <> "", confor_mat(CDate(txtinvoice_date1.Value).Date), Nothing) & vbNewLine _
                                        & txtinvoice_no2.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date2.Text) <> "", confor_mat(CDate(txtinvoice_date2.Value).Date), Nothing) & vbNewLine _
                                        & txtinvoice_no3.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date3.Text) <> "", confor_mat(CDate(txtinvoice_date3.Value).Date), Nothing)
                End If
            Case 4
                If CommonUtility.Get_String(txtinvoice_no1.Text) <> "" And CommonUtility.Get_String(txtinvoice_date1.Text) <> "" And CommonUtility.Get_String(txtinvoice_no2.Text) <> "" And CommonUtility.Get_String(txtinvoice_date2.Text) <> "" And CommonUtility.Get_String(txtinvoice_no3.Text) <> "" And CommonUtility.Get_String(txtinvoice_date3.Text) <> "" And CommonUtility.Get_String(txtinvoice_no4.Text) <> "" And CommonUtility.Get_String(txtinvoice_date4.Text) <> "" Then
                    '4 invoice
                    Tempth_invoices = txtinvoice_no1.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date1.Text) <> "", confor_mat(CDate(txtinvoice_date1.Value).Date), Nothing) & vbNewLine _
                                        & txtinvoice_no2.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date2.Text) <> "", confor_mat(CDate(txtinvoice_date2.Value).Date), Nothing) & vbNewLine _
                                        & txtinvoice_no3.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date3.Text) <> "", confor_mat(CDate(txtinvoice_date3.Value).Date), Nothing) & vbNewLine _
                                        & txtinvoice_no4.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date4.Text) <> "", confor_mat(CDate(txtinvoice_date4.Value).Date), Nothing)
                End If
            Case 5
                If CommonUtility.Get_String(txtinvoice_no1.Text) <> "" And CommonUtility.Get_String(txtinvoice_date1.Text) <> "" And CommonUtility.Get_String(txtinvoice_no2.Text) <> "" And CommonUtility.Get_String(txtinvoice_date2.Text) <> "" And CommonUtility.Get_String(txtinvoice_no3.Text) <> "" And CommonUtility.Get_String(txtinvoice_date3.Text) <> "" And CommonUtility.Get_String(txtinvoice_no4.Text) <> "" And CommonUtility.Get_String(txtinvoice_date4.Text) <> "" And CommonUtility.Get_String(txtinvoice_no5.Text) <> "" And CommonUtility.Get_String(txtinvoice_date5.Text) <> "" Then
                    '5 invoice
                    Tempth_invoices = txtinvoice_no1.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date1.Text) <> "", confor_mat(CDate(txtinvoice_date1.Value).Date), Nothing) & vbNewLine _
                                        & txtinvoice_no2.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date2.Text) <> "", confor_mat(CDate(txtinvoice_date2.Value).Date), Nothing) & vbNewLine _
                                        & txtinvoice_no3.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date3.Text) <> "", confor_mat(CDate(txtinvoice_date3.Value).Date), Nothing) & vbNewLine _
                                        & txtinvoice_no4.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date4.Text) <> "", confor_mat(CDate(txtinvoice_date4.Value).Date), Nothing) & vbNewLine _
                                        & txtinvoice_no5.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date5.Text) <> "", confor_mat(CDate(txtinvoice_date5.Value).Date), Nothing)
                End If
        End Select

        Return Tempth_invoices
    End Function
#End Region
    'มี invoice ต่างประเทศ
    Function CallInvoiceCheckF() As String 'Check Invoince
        Dim str_invoice As String
        Select Case CommonUtility.Get_StringValue(Check_NULLALL(txtNumInvoice.Text))
            Case Is <> ""
                str_invoice = return_NewAll_InvoiceString() 'CommonUtility.Get_StringValue(txtNumInvoice.Text)
            Case Else
                Select Case Check_numinvoiceAll(CommonUtility.Get_String(txtinvoice_no1.Text), CommonUtility.Get_String(txtinvoice_no2.Text), CommonUtility.Get_String(txtinvoice_no3.Text), CommonUtility.Get_String(txtinvoice_no4.Text), CommonUtility.Get_String(txtinvoice_no5.Text))
                    Case 1
                        If CommonUtility.Get_String(txtinvoice_no1.Text) <> "" And CommonUtility.Get_String(txtinvoice_date1.Text) <> "" Then
                            '1 invoice
                            str_invoice = txtinvoice_no1.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date1.Text) <> "", confor_mat(CDate(txtinvoice_date1.Value).Date), Nothing)
                        End If
                    Case 2
                        If CommonUtility.Get_String(txtinvoice_no1.Text) <> "" And CommonUtility.Get_String(txtinvoice_date1.Text) <> "" And CommonUtility.Get_String(txtinvoice_no2.Text) <> "" And CommonUtility.Get_String(txtinvoice_date2.Text) <> "" Then
                            '2 invoice
                            str_invoice = txtinvoice_no1.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date1.Text) <> "", confor_mat(CDate(txtinvoice_date1.Value).Date), Nothing) & vbNewLine _
                                                & txtinvoice_no2.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date2.Text) <> "", confor_mat(CDate(txtinvoice_date2.Value).Date), Nothing)
                        End If
                    Case 3
                        If CommonUtility.Get_String(txtinvoice_no1.Text) <> "" And CommonUtility.Get_String(txtinvoice_date1.Text) <> "" And CommonUtility.Get_String(txtinvoice_no2.Text) <> "" And CommonUtility.Get_String(txtinvoice_date2.Text) <> "" And CommonUtility.Get_String(txtinvoice_no3.Text) <> "" And CommonUtility.Get_String(txtinvoice_date3.Text) <> "" Then
                            '3 invoice
                            str_invoice = txtinvoice_no1.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date1.Text) <> "", confor_mat(CDate(txtinvoice_date1.Value).Date), Nothing) & vbNewLine _
                                                & txtinvoice_no2.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date2.Text) <> "", confor_mat(CDate(txtinvoice_date2.Value).Date), Nothing) & vbNewLine _
                                                & txtinvoice_no3.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date3.Text) <> "", confor_mat(CDate(txtinvoice_date3.Value).Date), Nothing)
                        End If
                    Case 4
                        If CommonUtility.Get_String(txtinvoice_no1.Text) <> "" And CommonUtility.Get_String(txtinvoice_date1.Text) <> "" And CommonUtility.Get_String(txtinvoice_no2.Text) <> "" And CommonUtility.Get_String(txtinvoice_date2.Text) <> "" And CommonUtility.Get_String(txtinvoice_no3.Text) <> "" And CommonUtility.Get_String(txtinvoice_date3.Text) <> "" And CommonUtility.Get_String(txtinvoice_no4.Text) <> "" And CommonUtility.Get_String(txtinvoice_date4.Text) <> "" Then
                            '4 invoice
                            str_invoice = txtinvoice_no1.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date1.Text) <> "", confor_mat(CDate(txtinvoice_date1.Value).Date), Nothing) & vbNewLine _
                                                & txtinvoice_no2.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date2.Text) <> "", confor_mat(CDate(txtinvoice_date2.Value).Date), Nothing) & vbNewLine _
                                                & txtinvoice_no3.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date3.Text) <> "", confor_mat(CDate(txtinvoice_date3.Value).Date), Nothing) & vbNewLine _
                                                & txtinvoice_no4.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date4.Text) <> "", confor_mat(CDate(txtinvoice_date4.Value).Date), Nothing)
                        End If
                    Case 5
                        If CommonUtility.Get_String(txtinvoice_no1.Text) <> "" And CommonUtility.Get_String(txtinvoice_date1.Text) <> "" And CommonUtility.Get_String(txtinvoice_no2.Text) <> "" And CommonUtility.Get_String(txtinvoice_date2.Text) <> "" And CommonUtility.Get_String(txtinvoice_no3.Text) <> "" And CommonUtility.Get_String(txtinvoice_date3.Text) <> "" And CommonUtility.Get_String(txtinvoice_no4.Text) <> "" And CommonUtility.Get_String(txtinvoice_date4.Text) <> "" And CommonUtility.Get_String(txtinvoice_no5.Text) <> "" And CommonUtility.Get_String(txtinvoice_date5.Text) <> "" Then
                            '5 invoice
                            str_invoice = txtinvoice_no1.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date1.Text) <> "", confor_mat(CDate(txtinvoice_date1.Value).Date), Nothing) & vbNewLine _
                                                & txtinvoice_no2.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date2.Text) <> "", confor_mat(CDate(txtinvoice_date2.Value).Date), Nothing) & vbNewLine _
                                                & txtinvoice_no3.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date3.Text) <> "", confor_mat(CDate(txtinvoice_date3.Value).Date), Nothing) & vbNewLine _
                                                & txtinvoice_no4.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date4.Text) <> "", confor_mat(CDate(txtinvoice_date4.Value).Date), Nothing) & vbNewLine _
                                                & txtinvoice_no5.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date5.Text) <> "", confor_mat(CDate(txtinvoice_date5.Value).Date), Nothing)
                        End If
                End Select
        End Select

        Return str_invoice
    End Function

    Function Check_Third(ByVal T1_third_country As String, ByVal T2_back_country As String, ByVal T3_place_exibition As String) As Integer
        If T1_third_country <> "" And T2_back_country <> "" And T3_place_exibition <> "" Then
            Return 1 'มีหมด

        ElseIf T1_third_country = "" And T2_back_country <> "" And T3_place_exibition <> "" Then
            Return 2 'T2_back_country,T3_place_exibition

        ElseIf T1_third_country = "" And T2_back_country = "" And T3_place_exibition <> "" Then
            Return 3 'T3_place_exibition

        ElseIf T1_third_country = "" And T2_back_country = "" And T3_place_exibition = "" Then
            Return 4 'ไม่มีเลย

        ElseIf T1_third_country = "" And T2_back_country <> "" And T3_place_exibition = "" Then
            Return 5 'T2_back_country

        ElseIf T1_third_country = "" And T2_back_country = "" And T3_place_exibition <> "" Then
            Return 6 'T3_place_exibition

        ElseIf T1_third_country <> "" And T2_back_country <> "" And T3_place_exibition = "" Then
            Return 7 'T1_third_country,T2_back_country

        ElseIf T1_third_country <> "" And T2_back_country = "" And T3_place_exibition <> "" Then
            Return 8 'T1_third_country,T3_place_exibition

        ElseIf T1_third_country <> "" And T2_back_country = "" And T3_place_exibition = "" Then
            Return 9 'T1_third_country

        End If
    End Function

#Region "Back"
    'Sub All_CheckThird_5(ByVal count_data As Integer)
    '    Dim All_str As String
    '    Select Case Check_Third(CommonUtility.Get_StringValue(txtthird_country.Value), _
    '                            CommonUtility.Get_StringValue(txtback_country.Value), _
    '                            CommonUtility.Get_StringValue(txtplace_exibition.Value))
    '        Case 1
    '            All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '                                        & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

    '            If C_TotalRowDe.Text = count_data Then
    '                txtT_product.Text = All_str
    '                GroupFooter1.Visible = True
    '                txtTotalAll.Visible = True
    '                txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
    '                                                            BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
    '                                                            BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " & _
    '                                                            BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf & "        " & _
    '                                                            BahtOnly(txtquantity5.Text, txtq_unit_code5.Text) & vbCrLf & txtthird_country.Text & vbCrLf & _
    '                                                            txtback_country.Text & vbCrLf & _
    '                                                            txtplace_exibition.Text & vbCrLf & _
    '                                                            "       _____________________________"
    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 2 'T2_back_country,T3_place_exibition
    '            All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '                                                        & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

    '            If C_TotalRowDe.Text = count_data Then
    '                txtT_product.Text = All_str
    '                GroupFooter1.Visible = True
    '                txtTotalAll.Visible = True
    '                txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
    '                                                                            BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
    '                                                                            BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " & _
    '                                                                            BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf & "        " & _
    '                                                                            BahtOnly(txtquantity5.Text, txtq_unit_code5.Text) & vbCrLf & _
    '                                                                            txtback_country.Text & vbCrLf & _
    '                                                                            txtplace_exibition.Text & vbCrLf & _
    '                                                                            "       _____________________________"
    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 3 'T3_place_exibition
    '            All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '                                            & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

    '            If C_TotalRowDe.Text = count_data Then
    '                txtT_product.Text = All_str
    '                GroupFooter1.Visible = True
    '                txtTotalAll.Visible = True
    '                txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
    '                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
    '                                                                BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " & _
    '                                                                BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf & "        " & _
    '                                                                BahtOnly(txtquantity5.Text, txtq_unit_code5.Text) & vbCrLf & _
    '                                                                "       _____________________________"
    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 4 'ไม่มีเลย
    '            All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '                                            & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

    '            If C_TotalRowDe.Text = count_data Then
    '                txtT_product.Text = All_str
    '                GroupFooter1.Visible = True
    '                txtTotalAll.Visible = True
    '                txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
    '                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
    '                                                                BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " & _
    '                                                                BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf & "        " & _
    '                                                                BahtOnly(txtquantity5.Text, txtq_unit_code5.Text) & vbCrLf & _
    '                                                                "       _____________________________"
    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 5 'T2_back_country
    '            All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '                                            & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

    '            If C_TotalRowDe.Text = count_data Then
    '                txtT_product.Text = All_str
    '                GroupFooter1.Visible = True
    '                txtTotalAll.Visible = True
    '                txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
    '                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
    '                                                                BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " & _
    '                                                                BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf & "        " & _
    '                                                                BahtOnly(txtquantity5.Text, txtq_unit_code5.Text) & vbCrLf & _
    '                                                                "       _____________________________"
    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 6 'T3_place_exibition
    '            All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '                                            & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

    '            If C_TotalRowDe.Text = count_data Then
    '                txtT_product.Text = All_str
    '                GroupFooter1.Visible = True
    '                txtTotalAll.Visible = True
    '                txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
    '                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
    '                                                                BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " & _
    '                                                                BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf & "        " & _
    '                                                                BahtOnly(txtquantity5.Text, txtq_unit_code5.Text) & vbCrLf & _
    '                                                                "       _____________________________"
    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 7 'T1_third_country,T2_back_country
    '            All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '                                            & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

    '            If C_TotalRowDe.Text = count_data Then
    '                txtT_product.Text = All_str
    '                GroupFooter1.Visible = True
    '                txtTotalAll.Visible = True
    '                txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
    '                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
    '                                                                BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " & _
    '                                                                BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf & "        " & _
    '                                                                BahtOnly(txtquantity5.Text, txtq_unit_code5.Text) & vbCrLf & txtthird_country.Text & vbCrLf & _
    '                                                                txtback_country.Text & vbCrLf & _
    '                                                                "       _____________________________"
    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 8 'T1_third_country,T3_place_exibition
    '            All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '                                            & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

    '            If C_TotalRowDe.Text = count_data Then
    '                txtT_product.Text = All_str
    '                GroupFooter1.Visible = True
    '                txtTotalAll.Visible = True
    '                txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
    '                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
    '                                                                BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " & _
    '                                                                BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf & "        " & _
    '                                                                BahtOnly(txtquantity5.Text, txtq_unit_code5.Text) & vbCrLf & txtthird_country.Text & vbCrLf & _
    '                                                                txtplace_exibition.Text & vbCrLf & _
    '                                                                "       _____________________________"
    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 9 'T1_third_country
    '            All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '                                        & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

    '            If C_TotalRowDe.Text = count_data Then
    '                txtT_product.Text = All_str
    '                GroupFooter1.Visible = True
    '                txtTotalAll.Visible = True
    '                txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
    '                                                            BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
    '                                                            BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " & _
    '                                                            BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf & "        " & _
    '                                                            BahtOnly(txtquantity5.Text, txtq_unit_code5.Text) & vbCrLf & "        " & txtthird_country.Text & vbCrLf & _
    '                                                            "       _____________________________"
    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '    End Select
    'End Sub
    'Sub All_CheckThird_4(ByVal count_data As Integer)
    '    Dim All_str As String

    '    Select Case Check_Third(CommonUtility.Get_StringValue(txtthird_country.Value), _
    '                            CommonUtility.Get_StringValue(txtback_country.Value), _
    '                            CommonUtility.Get_StringValue(txtplace_exibition.Value))
    '        Case 1
    '            All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '                                        & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

    '            If C_TotalRowDe.Text = count_data Then
    '                txtT_product.Text = All_str
    '                GroupFooter1.Visible = True
    '                txtTotalAll.Visible = True
    '                txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
    '                                        BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
    '                                        BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " & _
    '                                        BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf & "        " & _
    '                                        txtthird_country.Text & vbCrLf & _
    '                                        txtback_country.Text & vbCrLf & _
    '                                        txtplace_exibition.Text & vbCrLf & _
    '                                        "       _____________________________"
    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 2 'T2_back_country,T3_place_exibition
    '            All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '                                                        & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

    '            If C_TotalRowDe.Text = count_data Then
    '                txtT_product.Text = All_str
    '                GroupFooter1.Visible = True
    '                txtTotalAll.Visible = True
    '                txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
    '                                                        BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
    '                                                        BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " & _
    '                                                        BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf & "        " & _
    '                                                        txtback_country.Text & vbCrLf & _
    '                                                        txtplace_exibition.Text & vbCrLf & _
    '                                                        "       _____________________________"
    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 3 'T3_place_exibition
    '            All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '                                        & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

    '            If C_TotalRowDe.Text = count_data Then
    '                txtT_product.Text = All_str
    '                GroupFooter1.Visible = True
    '                txtTotalAll.Visible = True
    '                txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
    '                                                            BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
    '                                                            BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " & _
    '                                                            BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf & _
    '                                                            "       _____________________________"
    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 4 'ไม่มีเลย
    '            All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '                                        & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

    '            If C_TotalRowDe.Text = count_data Then
    '                txtT_product.Text = All_str
    '                GroupFooter1.Visible = True
    '                txtTotalAll.Visible = True
    '                txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
    '                                                            BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
    '                                                            BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " & _
    '                                                            BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf & _
    '                                                            "       _____________________________"
    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 5 'T2_back_country
    '            All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '                                        & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

    '            If C_TotalRowDe.Text = count_data Then
    '                txtT_product.Text = All_str
    '                GroupFooter1.Visible = True
    '                txtTotalAll.Visible = True
    '                txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
    '                                                            BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
    '                                                            BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " & _
    '                                                            BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf & "        " & _
    '                                                            "       _____________________________"
    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 6 'T3_place_exibition
    '            All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '                                        & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

    '            If C_TotalRowDe.Text = count_data Then
    '                txtT_product.Text = All_str
    '                GroupFooter1.Visible = True
    '                txtTotalAll.Visible = True
    '                txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
    '                                                            BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
    '                                                            BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " & _
    '                                                            BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf & _
    '                                                            "       _____________________________"
    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 7 'T1_third_country,T2_back_country
    '            All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '                                            & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

    '            If C_TotalRowDe.Text = count_data Then
    '                txtT_product.Text = All_str
    '                GroupFooter1.Visible = True
    '                txtTotalAll.Visible = True
    '                txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
    '                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
    '                                                                BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " & _
    '                                                                BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf & "        " & _
    '                                                                txtthird_country.Text & vbCrLf & _
    '                                                                txtback_country.Text & vbCrLf & _
    '                                                                "       _____________________________"
    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 8 'T1_third_country,T3_place_exibition
    '            All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '                                            & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

    '            If C_TotalRowDe.Text = count_data Then
    '                txtT_product.Text = All_str
    '                GroupFooter1.Visible = True
    '                txtTotalAll.Visible = True
    '                txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
    '                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
    '                                                                BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " & _
    '                                                                BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf & "        " & _
    '                                                                txtthird_country.Text & vbCrLf & _
    '                                                                txtplace_exibition.Text & vbCrLf & _
    '                                                                "       _____________________________"
    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 9 'T1_third_country
    '            All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '                                            & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

    '            If C_TotalRowDe.Text = count_data Then
    '                txtT_product.Text = All_str
    '                GroupFooter1.Visible = True
    '                txtTotalAll.Visible = True
    '                txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
    '                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
    '                                                                BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " & _
    '                                                                BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf & "        " & _
    '                                                                txtthird_country.Text & vbCrLf & _
    '                                                                "       _____________________________"
    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '    End Select
    'End Sub
    'Sub All_CheckThird_3(ByVal count_data As Integer)
    '    Dim All_str As String
    '    Select Case Check_Third(CommonUtility.Get_StringValue(txtthird_country.Value), _
    '                            CommonUtility.Get_StringValue(txtback_country.Value), _
    '                            CommonUtility.Get_StringValue(txtplace_exibition.Value))
    '        Case 1
    '            All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '                                        & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

    '            If C_TotalRowDe.Text = count_data Then
    '                txtT_product.Text = All_str
    '                GroupFooter1.Visible = True
    '                txtTotalAll.Visible = True
    '                txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
    '                                                            BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
    '                                                            BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " & _
    '                                                            txtthird_country.Text & vbCrLf & _
    '                                                            txtback_country.Text & vbCrLf & _
    '                                                            txtplace_exibition.Text & vbCrLf & _
    '                                                            "       _____________________________"
    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 2 'T2_back_country,T3_place_exibition
    '            All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '                                                        & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

    '            If C_TotalRowDe.Text = count_data Then
    '                txtT_product.Text = All_str
    '                GroupFooter1.Visible = True
    '                txtTotalAll.Visible = True
    '                txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
    '                                                                            BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
    '                                                                            BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " & _
    '                                                                            txtback_country.Text & vbCrLf & _
    '                                                                            txtplace_exibition.Text & vbCrLf & _
    '                                                                            "       _____________________________"
    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 3 'T3_place_exibition
    '            All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '                                        & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

    '            If C_TotalRowDe.Text = count_data Then
    '                txtT_product.Text = All_str
    '                GroupFooter1.Visible = True
    '                txtTotalAll.Visible = True
    '                txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
    '                                                            BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
    '                                                            BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & _
    '                                                            "       _____________________________"
    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 4 'ไม่มีเลย
    '            All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '                                        & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

    '            If C_TotalRowDe.Text = count_data Then
    '                txtT_product.Text = All_str
    '                GroupFooter1.Visible = True
    '                txtTotalAll.Visible = True
    '                txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
    '                                                            BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
    '                                                            BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " & _
    '                                                            "       _____________________________"
    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 5 'T2_back_country
    '            All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '                                        & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

    '            If C_TotalRowDe.Text = count_data Then
    '                txtT_product.Text = All_str
    '                GroupFooter1.Visible = True
    '                txtTotalAll.Visible = True
    '                txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
    '                                                            BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
    '                                                            BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & _
    '                                                            "       _____________________________"
    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 6 'T3_place_exibition
    '            All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '                                        & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

    '            If C_TotalRowDe.Text = count_data Then
    '                txtT_product.Text = All_str
    '                GroupFooter1.Visible = True
    '                txtTotalAll.Visible = True
    '                txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
    '                                                            BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
    '                                                            BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & _
    '                                                            "       _____________________________"
    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 7 'T1_third_country,T2_back_country
    '            All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '                                        & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

    '            If C_TotalRowDe.Text = count_data Then
    '                txtT_product.Text = All_str
    '                GroupFooter1.Visible = True
    '                txtTotalAll.Visible = True
    '                txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
    '                                                            BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
    '                                                            BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " & _
    '                                                            txtthird_country.Text & vbCrLf & _
    '                                                            txtback_country.Text & vbCrLf & _
    '                                                            "       _____________________________"
    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 8 'T1_third_country,T3_place_exibition
    '            All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '                                        & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

    '            If C_TotalRowDe.Text = count_data Then
    '                txtT_product.Text = All_str
    '                GroupFooter1.Visible = True
    '                txtTotalAll.Visible = True
    '                txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
    '                                                            BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
    '                                                            BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " & _
    '                                                            txtthird_country.Text & vbCrLf & _
    '                                                            txtplace_exibition.Text & vbCrLf & _
    '                                                            "       _____________________________"
    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 9 'T1_third_country
    '            All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '                                        & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

    '            If C_TotalRowDe.Text = count_data Then
    '                txtT_product.Text = All_str
    '                GroupFooter1.Visible = True
    '                txtTotalAll.Visible = True
    '                txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
    '                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
    '                                                                BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " & _
    '                                                                txtthird_country.Text & vbCrLf & _
    '                                                                "       _____________________________"
    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '    End Select
    'End Sub
    'Sub All_CheckThird_2(ByVal count_data As Integer)
    '    Dim All_str As String
    '    Select Case Check_Third(CommonUtility.Get_StringValue(txtthird_country.Value), _
    '                            CommonUtility.Get_StringValue(txtback_country.Value), _
    '                            CommonUtility.Get_StringValue(txtplace_exibition.Value))
    '        Case 1
    '            All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '                                        & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

    '            If C_TotalRowDe.Text = count_data Then
    '                txtT_product.Text = All_str
    '                GroupFooter1.Visible = True
    '                txtTotalAll.Visible = True
    '                txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
    '                                                            BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
    '                                                            txtthird_country.Text & vbCrLf & _
    '                                                            txtback_country.Text & vbCrLf & _
    '                                                            txtplace_exibition.Text & vbCrLf & _
    '                                                            "       _____________________________"
    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 2 'T2_back_country,T3_place_exibition
    '            All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '                                                        & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

    '            If C_TotalRowDe.Text = count_data Then
    '                txtT_product.Text = All_str
    '                GroupFooter1.Visible = True
    '                txtTotalAll.Visible = True
    '                txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
    '                                                                            BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
    '                                                                            txtback_country.Text & vbCrLf & _
    '                                                                            txtplace_exibition.Text & vbCrLf & _
    '                                                                            "       _____________________________"
    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 3 'T3_place_exibition
    '            All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '                                        & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

    '            If C_TotalRowDe.Text = count_data Then
    '                txtT_product.Text = All_str
    '                GroupFooter1.Visible = True
    '                txtTotalAll.Visible = True
    '                txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
    '                                                            BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & _
    '                                                            "       _____________________________"
    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 4 'ไม่มีเลย
    '            All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '                                        & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

    '            If C_TotalRowDe.Text = count_data Then
    '                txtT_product.Text = All_str
    '                GroupFooter1.Visible = True
    '                txtTotalAll.Visible = True
    '                txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
    '                                                            BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
    '                                                            "       _____________________________"
    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 5 'T2_back_country
    '            All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '                                        & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

    '            If C_TotalRowDe.Text = count_data Then
    '                txtT_product.Text = All_str
    '                GroupFooter1.Visible = True
    '                txtTotalAll.Visible = True
    '                txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
    '                                                            BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & _
    '                                                            "       _____________________________"
    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 6 'T3_place_exibition
    '            All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '                                        & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

    '            If C_TotalRowDe.Text = count_data Then
    '                txtT_product.Text = All_str
    '                GroupFooter1.Visible = True
    '                txtTotalAll.Visible = True
    '                txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
    '                                                            BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & _
    '                                                            "       _____________________________"
    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 7 'T1_third_country,T2_back_country
    '            All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '                                        & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

    '            If C_TotalRowDe.Text = count_data Then
    '                txtT_product.Text = All_str
    '                GroupFooter1.Visible = True
    '                txtTotalAll.Visible = True
    '                txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
    '                                                            BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
    '                                                            txtthird_country.Text & vbCrLf & _
    '                                                            txtback_country.Text & vbCrLf & _
    '                                                            "       _____________________________"
    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 8 'T1_third_country,T3_place_exibition
    '            All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '                                        & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

    '            If C_TotalRowDe.Text = count_data Then
    '                txtT_product.Text = All_str
    '                GroupFooter1.Visible = True
    '                txtTotalAll.Visible = True
    '                txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
    '                                                            BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
    '                                                            txtthird_country.Text & vbCrLf & _
    '                                                            txtplace_exibition.Text & vbCrLf & _
    '                                                            "       _____________________________"
    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 9 'T1_third_country
    '            All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '                                        & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

    '            If C_TotalRowDe.Text = count_data Then
    '                txtT_product.Text = All_str
    '                GroupFooter1.Visible = True
    '                txtTotalAll.Visible = True
    '                txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
    '                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
    '                                                                txtthird_country.Text & vbCrLf & _
    '                                                                "       _____________________________"
    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '    End Select
    'End Sub
    'Sub All_CheckThird_1(ByVal count_data As Integer)
    '    Dim All_str As String
    '    Select Case Check_Third(CommonUtility.Get_StringValue(txtthird_country.Value), _
    '                            CommonUtility.Get_StringValue(txtback_country.Value), _
    '                            CommonUtility.Get_StringValue(txtplace_exibition.Value))
    '        Case 1
    '            All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '                                        & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

    '            If C_TotalRowDe.Text = count_data Then
    '                If (Not txtquantity1.Text = String.Empty = True) And CommonUtility.Get_StringValue(txtquantity1.Text) <> "" And CommonUtility.Get_Decimal(IIf(Not (txtquantity1.Text = String.Empty) = False, 0, txtquantity1.Value)) <> 0 Then
    '                    txtT_product.Text = All_str
    '                    GroupFooter1.Visible = True
    '                    txtTotalAll.Visible = True
    '                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
    '                                                                                    txtthird_country.Text & vbCrLf & _
    '                                                                                    txtback_country.Text & vbCrLf & _
    '                                                                                    txtplace_exibition.Text & vbCrLf & _
    '                                                                                    "       _____________________________"
    '                Else
    '                    txtT_product.Text = All_str & _
    '                                        "        " & _
    '                                                                                    txtthird_country.Text & vbCrLf & _
    '                                                                                    txtback_country.Text & vbCrLf & _
    '                                                                                    txtplace_exibition.Text & vbCrLf & _
    '                                                                                    "       _____________________________"
    '                End If

    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 2 'T2_back_country,T3_place_exibition
    '            All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '                                                        & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

    '            If C_TotalRowDe.Text = count_data Then
    '                If (Not txtquantity1.Text = String.Empty = True) And CommonUtility.Get_StringValue(txtquantity1.Text) <> "" And CommonUtility.Get_Decimal(IIf(Not (txtquantity1.Text = String.Empty) = False, 0, txtquantity1.Value)) <> 0 Then
    '                    txtT_product.Text = All_str
    '                    GroupFooter1.Visible = True
    '                    txtTotalAll.Visible = True
    '                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
    '                                                                                                    txtback_country.Text & vbCrLf & _
    '                                                                                                    txtplace_exibition.Text & vbCrLf & _
    '                                                                                                    "       _____________________________"
    '                Else
    '                    txtT_product.Text = All_str & _
    '                                        "        " & _
    '                                                                                                    txtback_country.Text & vbCrLf & _
    '                                                                                                    txtplace_exibition.Text & vbCrLf & _
    '                                                                                                    "       _____________________________"
    '                End If

    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 3 'T3_place_exibition
    '            All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '                                        & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

    '            If C_TotalRowDe.Text = count_data Then
    '                If (Not txtquantity1.Text = String.Empty = True) And CommonUtility.Get_StringValue(txtquantity1.Text) <> "" And CommonUtility.Get_Decimal(IIf(Not (txtquantity1.Text = String.Empty) = False, 0, txtquantity1.Value)) <> 0 Then
    '                    txtT_product.Text = All_str
    '                    GroupFooter1.Visible = True
    '                    txtTotalAll.Visible = True
    '                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & _
    '                                                                                    "       _____________________________"
    '                Else
    '                    txtT_product.Text = All_str & _
    '                                        "       _____________________________"
    '                End If

    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 4 'ไม่มีเลย
    '            All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '                                        & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

    '            If C_TotalRowDe.Text = count_data Then
    '                If (Not txtquantity1.Text = String.Empty = True) And CommonUtility.Get_StringValue(txtquantity1.Text) <> "" And CommonUtility.Get_Decimal(IIf(Not (txtquantity1.Text = String.Empty) = False, 0, txtquantity1.Value)) <> 0 Then
    '                    txtT_product.Text = All_str
    '                    GroupFooter1.Visible = True
    '                    txtTotalAll.Visible = True
    '                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & _
    '                                                                                                           "       _____________________________"
    '                Else
    '                    txtT_product.Text = All_str & _
    '                                        "       _____________________________"
    '                End If

    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 5 'T2_back_country
    '            All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '                                        & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

    '            If C_TotalRowDe.Text = count_data Then
    '                If (Not txtquantity1.Text = String.Empty = True) And CommonUtility.Get_StringValue(txtquantity1.Text) <> "" And CommonUtility.Get_Decimal(IIf(Not (txtquantity1.Text = String.Empty) = False, 0, txtquantity1.Value)) <> 0 Then
    '                    txtT_product.Text = All_str
    '                    GroupFooter1.Visible = True
    '                    txtTotalAll.Visible = True
    '                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & _
    '                                                                                    "       _____________________________"
    '                Else
    '                    txtT_product.Text = All_str & _
    '                                       "       _____________________________"
    '                End If

    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 6 'T3_place_exibition
    '            All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '                                        & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

    '            If C_TotalRowDe.Text = count_data Then
    '                If (Not txtquantity1.Text = String.Empty = True) And CommonUtility.Get_StringValue(txtquantity1.Text) <> "" And CommonUtility.Get_Decimal(IIf(Not (txtquantity1.Text = String.Empty) = False, 0, txtquantity1.Value)) <> 0 Then
    '                    txtT_product.Text = All_str
    '                    GroupFooter1.Visible = True
    '                    txtTotalAll.Visible = True
    '                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & _
    '                                                                                    "       _____________________________"
    '                Else
    '                    txtT_product.Text = All_str & _
    '                                        "       _____________________________"
    '                End If

    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 7 'T1_third_country,T2_back_country
    '            All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '                                        & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

    '            If C_TotalRowDe.Text = count_data Then
    '                If (Not txtquantity1.Text = String.Empty = True) And CommonUtility.Get_StringValue(txtquantity1.Text) <> "" And CommonUtility.Get_Decimal(IIf(Not (txtquantity1.Text = String.Empty) = False, 0, txtquantity1.Value)) <> 0 Then
    '                    txtT_product.Text = All_str
    '                    GroupFooter1.Visible = True
    '                    txtTotalAll.Visible = True
    '                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
    '                                                                                    txtthird_country.Text & vbCrLf & _
    '                                                                                    txtback_country.Text & vbCrLf & _
    '                                                                                    "       _____________________________"
    '                Else
    '                    txtT_product.Text = All_str & _
    '                                        "        " & _
    '                                                                                    txtthird_country.Text & vbCrLf & _
    '                                                                                    txtback_country.Text & vbCrLf & _
    '                                                                                    "       _____________________________"
    '                End If

    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 8 'T1_third_country,T3_place_exibition
    '            All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '                                        & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

    '            If C_TotalRowDe.Text = count_data Then
    '                If (Not txtquantity1.Text = String.Empty = True) And CommonUtility.Get_StringValue(txtquantity1.Text) <> "" And CommonUtility.Get_Decimal(IIf(Not (txtquantity1.Text = String.Empty) = False, 0, txtquantity1.Value)) <> 0 Then
    '                    txtT_product.Text = All_str
    '                    GroupFooter1.Visible = True
    '                    txtTotalAll.Visible = True
    '                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
    '                                                                                    txtthird_country.Text & vbCrLf & _
    '                                                                                    txtplace_exibition.Text & vbCrLf & _
    '                                                                                    "       _____________________________"
    '                Else
    '                    txtT_product.Text = All_str & _
    '                                        "        " & _
    '                                                                                    txtthird_country.Text & vbCrLf & _
    '                                                                                    txtplace_exibition.Text & vbCrLf & _
    '                                                                                    "       _____________________________"
    '                End If

    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 9 'T1_third_country
    '            All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '                                        & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

    '            If C_TotalRowDe.Text = count_data Then
    '                If (Not txtquantity1.Text = String.Empty = True) And CommonUtility.Get_StringValue(txtquantity1.Text) <> "" And CommonUtility.Get_Decimal(IIf(Not (txtquantity1.Text = String.Empty) = False, 0, txtquantity1.Value)) <> 0 Then
    '                    txtT_product.Text = All_str
    '                    GroupFooter1.Visible = True
    '                    txtTotalAll.Visible = True
    '                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
    '                                                                                        txtthird_country.Text & vbCrLf & _
    '                                                                                        "       _____________________________"
    '                Else
    '                    txtT_product.Text = All_str & _
    '                                        "        " & _
    '                                                                                        txtthird_country.Text & vbCrLf & _
    '                                                                                        "       _____________________________"
    '                End If

    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '    End Select
    'End Sub
    'Sub All_CheckThird_0(ByVal count_data As Integer)
    '    Dim All_str As String
    '    Select Case Check_Third(CommonUtility.Get_StringValue(txtthird_country.Value), _
    '                            CommonUtility.Get_StringValue(txtback_country.Value), _
    '                            CommonUtility.Get_StringValue(txtplace_exibition.Value))
    '        Case 1
    '            All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '                                        & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

    '            If C_TotalRowDe.Text = count_data Then
    '                txtT_product.Text = All_str & "        " & _
    '                txtthird_country.Text & vbCrLf & _
    '                                                            txtback_country.Text & vbCrLf & _
    '                                                            txtplace_exibition.Text & vbCrLf & _
    '                                                            "       _____________________________"
    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 2 'T2_back_country,T3_place_exibition
    '            All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '                                                        & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

    '            If C_TotalRowDe.Text = count_data Then
    '                txtT_product.Text = All_str & _
    '                txtback_country.Text & vbCrLf & _
    '                                                                            txtplace_exibition.Text & vbCrLf & _
    '                                                                            "       _____________________________"
    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 3 'T3_place_exibition
    '            All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '                                        & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

    '            If C_TotalRowDe.Text = count_data Then
    '                txtT_product.Text = All_str & _
    '                 "       _____________________________"
    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 4 'ไม่มีเลย
    '            All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '                                        & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

    '            If C_TotalRowDe.Text = count_data Then
    '                txtT_product.Text = All_str & _
    '                 "       _____________________________"
    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 5 'T2_back_country
    '            All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '                                        & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

    '            If C_TotalRowDe.Text = count_data Then
    '                txtT_product.Text = All_str & _
    '                vbCrLf & "       _____________________________"
    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 6 'T3_place_exibition
    '            All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '                                        & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

    '            If C_TotalRowDe.Text = count_data Then
    '                txtT_product.Text = All_str & _
    '                 "       _____________________________"
    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 7 'T1_third_country,T2_back_country
    '            All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '                                        & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

    '            If C_TotalRowDe.Text = count_data Then
    '                txtT_product.Text = All_str & _
    '                txtthird_country.Text & vbCrLf & _
    '                                                            txtback_country.Text & vbCrLf & _
    '                                                            "       _____________________________"
    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 8 'T1_third_country,T3_place_exibition
    '            All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '                                        & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

    '            If C_TotalRowDe.Text = count_data Then
    '                txtT_product.Text = All_str & _
    '                 txtthird_country.Text & vbCrLf & _
    '                                                            txtplace_exibition.Text & vbCrLf & _
    '                                                            "       _____________________________"
    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 9 'T1_third_country
    '            All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '                                        & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

    '            If C_TotalRowDe.Text = count_data Then
    '                txtT_product.Text = All_str & _
    '                txtthird_country.Text & vbCrLf & _
    '                                                                "       _____________________________"
    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '    End Select
    'End Sub
#End Region
    Dim All_str As String
    'Sub Title_StrDetail()
    '    'All_str = CarTxt(CommonUtility.Get_StringValue(txtSINGLE_COUNTRY_CONTENT.Value), CommonUtility.Get_StringValue(txtInvoiceDetailTH.Value)) & "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '    '                                    & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
    '    All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '                                        & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
    'End Sub
    Sub Title_StrDetail()
        'All_str = CarTxt(CommonUtility.Get_StringValue(txtSINGLE_COUNTRY_CONTENT.Value), CommonUtility.Get_StringValue(txtInvoiceDetailTH.Value)) & "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
        '                                    & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
        All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                                            & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
    End Sub
    'ใช้แทน เงื่อนไข ช่องที่ 1 เนื่องจากปิด OB,OC  แล้ว ผู้ประกอบการต้องป้อนเองที่ช่องใหม่ txtob_address.text เพิ่มให้เหมือนฟอร์มอี 30-05-2556
    Function NewStr_AddressDetail(ByVal strOB_address As String) As String
        Dim Substr_OB_add As String = ""
        If strOB_address <> "" And strOB_address.Trim <> "" And Not strOB_address = String.Empty = True Then
            Substr_OB_add = strOB_address & vbCrLf
        Else
            Substr_OB_add = ""
        End If
        Return Substr_OB_add
    End Function
    Sub All_CheckThird_5(ByVal count_data As Integer)
        Title_StrDetail()
        Select Case Check_Third(CommonUtility.Get_StringValue(txtthird_country.Value),
                                CommonUtility.Get_StringValue(txtback_country.Value),
                                CommonUtility.Get_StringValue(txtplace_exibition.Value))
            Case 1
                'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & NewStr_AddressDetail(CommonUtility.Get_StringValue(txtNewEmail_ch02.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " &
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " &
                                                                BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " &
                                                                BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf & "        " &
                                                                BahtOnly(txtquantity5.Text, txtq_unit_code5.Text) & vbCrLf & txtthird_country.Text & vbCrLf &
                                                                txtback_country.Text & vbCrLf &
                                                                txtplace_exibition.Text & vbCrLf &
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 2 'T2_back_country,T3_place_exibition
                'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & NewStr_AddressDetail(CommonUtility.Get_StringValue(txtNewEmail_ch02.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " &
                                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " &
                                                                                BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " &
                                                                                BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf & "        " &
                                                                                BahtOnly(txtquantity5.Text, txtq_unit_code5.Text) & vbCrLf &
                                                                                txtback_country.Text & vbCrLf &
                                                                                txtplace_exibition.Text & vbCrLf &
                                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 3 'T3_place_exibition
                'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & NewStr_AddressDetail(CommonUtility.Get_StringValue(txtNewEmail_ch02.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " &
                                                                    BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " &
                                                                    BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " &
                                                                    BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf & "        " &
                                                                    BahtOnly(txtquantity5.Text, txtq_unit_code5.Text) & vbCrLf &
                                                                    "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 4 'ไม่มีเลย
                'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & NewStr_AddressDetail(CommonUtility.Get_StringValue(txtNewEmail_ch02.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " &
                                                                    BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " &
                                                                    BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " &
                                                                    BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf & "        " &
                                                                    BahtOnly(txtquantity5.Text, txtq_unit_code5.Text) & vbCrLf &
                                                                    "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 5 'T2_back_country
                'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & NewStr_AddressDetail(CommonUtility.Get_StringValue(txtNewEmail_ch02.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " &
                                                                    BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " &
                                                                    BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " &
                                                                    BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf & "        " &
                                                                    BahtOnly(txtquantity5.Text, txtq_unit_code5.Text) & vbCrLf &
                                                                    "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 6 'T3_place_exibition
                'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & NewStr_AddressDetail(CommonUtility.Get_StringValue(txtNewEmail_ch02.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " &
                                                                    BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " &
                                                                    BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " &
                                                                    BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf & "        " &
                                                                    BahtOnly(txtquantity5.Text, txtq_unit_code5.Text) & vbCrLf &
                                                                    "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 7 'T1_third_country,T2_back_country
                'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & NewStr_AddressDetail(CommonUtility.Get_StringValue(txtNewEmail_ch02.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " &
                                                                    BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " &
                                                                    BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " &
                                                                    BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf & "        " &
                                                                    BahtOnly(txtquantity5.Text, txtq_unit_code5.Text) & vbCrLf & txtthird_country.Text & vbCrLf &
                                                                    txtback_country.Text & vbCrLf &
                                                                    "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 8 'T1_third_country,T3_place_exibition
                'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & NewStr_AddressDetail(CommonUtility.Get_StringValue(txtNewEmail_ch02.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " &
                                                                    BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " &
                                                                    BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " &
                                                                    BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf & "        " &
                                                                    BahtOnly(txtquantity5.Text, txtq_unit_code5.Text) & vbCrLf & txtthird_country.Text & vbCrLf &
                                                                    txtplace_exibition.Text & vbCrLf &
                                                                    "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 9 'T1_third_country
                'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & NewStr_AddressDetail(CommonUtility.Get_StringValue(txtNewEmail_ch02.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " &
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " &
                                                                BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " &
                                                                BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf & "        " &
                                                                BahtOnly(txtquantity5.Text, txtq_unit_code5.Text) & vbCrLf & "        " & txtthird_country.Text & vbCrLf &
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
        End Select
    End Sub
    Sub All_CheckThird_4(ByVal count_data As Integer)
        Title_StrDetail()

        Select Case Check_Third(CommonUtility.Get_StringValue(txtthird_country.Value),
                                CommonUtility.Get_StringValue(txtback_country.Value),
                                CommonUtility.Get_StringValue(txtplace_exibition.Value))
            Case 1
                'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & NewStr_AddressDetail(CommonUtility.Get_StringValue(txtNewEmail_ch02.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " &
                                            BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " &
                                            BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " &
                                            BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf & "        " &
                                            txtthird_country.Text & vbCrLf &
                                            txtback_country.Text & vbCrLf &
                                            txtplace_exibition.Text & vbCrLf &
                                            "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 2 'T2_back_country,T3_place_exibition
                'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & NewStr_AddressDetail(CommonUtility.Get_StringValue(txtNewEmail_ch02.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " &
                                                            BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " &
                                                            BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " &
                                                            BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf & "        " &
                                                            txtback_country.Text & vbCrLf &
                                                            txtplace_exibition.Text & vbCrLf &
                                                            "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 3 'T3_place_exibition
                'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & NewStr_AddressDetail(CommonUtility.Get_StringValue(txtNewEmail_ch02.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " &
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " &
                                                                BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " &
                                                                BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf &
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 4 'ไม่มีเลย
                'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & NewStr_AddressDetail(CommonUtility.Get_StringValue(txtNewEmail_ch02.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " &
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " &
                                                                BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " &
                                                                BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf &
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 5 'T2_back_country
                'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & NewStr_AddressDetail(CommonUtility.Get_StringValue(txtNewEmail_ch02.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " &
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " &
                                                                BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " &
                                                                BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf & "        " &
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 6 'T3_place_exibition
                'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & NewStr_AddressDetail(CommonUtility.Get_StringValue(txtNewEmail_ch02.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " &
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " &
                                                                BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " &
                                                                BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf &
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 7 'T1_third_country,T2_back_country
                'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & NewStr_AddressDetail(CommonUtility.Get_StringValue(txtNewEmail_ch02.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " &
                                                                    BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " &
                                                                    BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " &
                                                                    BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf & "        " &
                                                                    txtthird_country.Text & vbCrLf &
                                                                    txtback_country.Text & vbCrLf &
                                                                    "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 8 'T1_third_country,T3_place_exibition
                'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & NewStr_AddressDetail(CommonUtility.Get_StringValue(txtNewEmail_ch02.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " &
                                                                    BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " &
                                                                    BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " &
                                                                    BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf & "        " &
                                                                    txtthird_country.Text & vbCrLf &
                                                                    txtplace_exibition.Text & vbCrLf &
                                                                    "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 9 'T1_third_country
                'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & NewStr_AddressDetail(CommonUtility.Get_StringValue(txtNewEmail_ch02.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " &
                                                                    BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " &
                                                                    BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " &
                                                                    BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf & "        " &
                                                                    txtthird_country.Text & vbCrLf &
                                                                    "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
        End Select
    End Sub
    Sub All_CheckThird_3(ByVal count_data As Integer)
        Title_StrDetail()
        Select Case Check_Third(CommonUtility.Get_StringValue(txtthird_country.Value),
                                CommonUtility.Get_StringValue(txtback_country.Value),
                                CommonUtility.Get_StringValue(txtplace_exibition.Value))
            Case 1
                'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & NewStr_AddressDetail(CommonUtility.Get_StringValue(txtNewEmail_ch02.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " &
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " &
                                                                BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " &
                                                                txtthird_country.Text & vbCrLf &
                                                                txtback_country.Text & vbCrLf &
                                                                txtplace_exibition.Text & vbCrLf &
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 2 'T2_back_country,T3_place_exibition
                'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & NewStr_AddressDetail(CommonUtility.Get_StringValue(txtNewEmail_ch02.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " &
                                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " &
                                                                                BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " &
                                                                                txtback_country.Text & vbCrLf &
                                                                                txtplace_exibition.Text & vbCrLf &
                                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 3 'T3_place_exibition
                'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & NewStr_AddressDetail(CommonUtility.Get_StringValue(txtNewEmail_ch02.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " &
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " &
                                                                BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf &
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 4 'ไม่มีเลย
                'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & NewStr_AddressDetail(CommonUtility.Get_StringValue(txtNewEmail_ch02.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " &
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " &
                                                                BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " &
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 5 'T2_back_country
                'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & NewStr_AddressDetail(CommonUtility.Get_StringValue(txtNewEmail_ch02.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " &
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " &
                                                                BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf &
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 6 'T3_place_exibition
                'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & NewStr_AddressDetail(CommonUtility.Get_StringValue(txtNewEmail_ch02.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " &
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " &
                                                                BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf &
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 7 'T1_third_country,T2_back_country
                'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & NewStr_AddressDetail(CommonUtility.Get_StringValue(txtNewEmail_ch02.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " &
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " &
                                                                BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " &
                                                                txtthird_country.Text & vbCrLf &
                                                                txtback_country.Text & vbCrLf &
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 8 'T1_third_country,T3_place_exibition
                'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & NewStr_AddressDetail(CommonUtility.Get_StringValue(txtNewEmail_ch02.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " &
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " &
                                                                BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " &
                                                                txtthird_country.Text & vbCrLf &
                                                                txtplace_exibition.Text & vbCrLf &
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 9 'T1_third_country
                'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & NewStr_AddressDetail(CommonUtility.Get_StringValue(txtNewEmail_ch02.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " &
                                                                    BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " &
                                                                    BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " &
                                                                    txtthird_country.Text & vbCrLf &
                                                                    "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
        End Select
    End Sub
    Sub All_CheckThird_2(ByVal count_data As Integer)
        Title_StrDetail()
        Select Case Check_Third(CommonUtility.Get_StringValue(txtthird_country.Value),
                                CommonUtility.Get_StringValue(txtback_country.Value),
                                CommonUtility.Get_StringValue(txtplace_exibition.Value))
            Case 1
                'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & NewStr_AddressDetail(CommonUtility.Get_StringValue(txtNewEmail_ch02.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " &
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " &
                                                                txtthird_country.Text & vbCrLf &
                                                                txtback_country.Text & vbCrLf &
                                                                txtplace_exibition.Text & vbCrLf &
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 2 'T2_back_country,T3_place_exibition
                'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & NewStr_AddressDetail(CommonUtility.Get_StringValue(txtNewEmail_ch02.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " &
                                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " &
                                                                                txtback_country.Text & vbCrLf &
                                                                                txtplace_exibition.Text & vbCrLf &
                                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 3 'T3_place_exibition
                'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & NewStr_AddressDetail(CommonUtility.Get_StringValue(txtNewEmail_ch02.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " &
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf &
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 4 'ไม่มีเลย
                'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & NewStr_AddressDetail(CommonUtility.Get_StringValue(txtNewEmail_ch02.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " &
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " &
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 5 'T2_back_country
                'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & NewStr_AddressDetail(CommonUtility.Get_StringValue(txtNewEmail_ch02.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " &
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf &
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 6 'T3_place_exibition
                'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & NewStr_AddressDetail(CommonUtility.Get_StringValue(txtNewEmail_ch02.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " &
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf &
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 7 'T1_third_country,T2_back_country
                'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & NewStr_AddressDetail(CommonUtility.Get_StringValue(txtNewEmail_ch02.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " &
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " &
                                                                txtthird_country.Text & vbCrLf &
                                                                txtback_country.Text & vbCrLf &
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 8 'T1_third_country,T3_place_exibition
                'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & NewStr_AddressDetail(CommonUtility.Get_StringValue(txtNewEmail_ch02.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " &
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " &
                                                                txtthird_country.Text & vbCrLf &
                                                                txtplace_exibition.Text & vbCrLf &
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 9 'T1_third_country
                'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & NewStr_AddressDetail(CommonUtility.Get_StringValue(txtNewEmail_ch02.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " &
                                                                    BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " &
                                                                    txtthird_country.Text & vbCrLf &
                                                                    "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
        End Select
    End Sub
    Sub All_CheckThird_1(ByVal count_data As Integer)
        Title_StrDetail()
        Select Case Check_Third(CommonUtility.Get_StringValue(txtthird_country.Value),
                                CommonUtility.Get_StringValue(txtback_country.Value),
                                CommonUtility.Get_StringValue(txtplace_exibition.Value))
            Case 1
                'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    If (Not txtquantity1.Text = String.Empty = True) And CommonUtility.Get_StringValue(txtquantity1.Text) <> "" And CommonUtility.Get_Decimal(IIf(Not (txtquantity1.Text = String.Empty) = False, 0, txtquantity1.Value)) <> 0 Then
                        txtT_product.Text = All_str & NewStr_AddressDetail(CommonUtility.Get_StringValue(txtNewEmail_ch02.Text))
                        GroupFooter1.Visible = True
                        txtTotalAll.Visible = True
                        txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " &
                                                                                        txtthird_country.Text & vbCrLf &
                                                                                        txtback_country.Text & vbCrLf &
                                                                                        txtplace_exibition.Text & vbCrLf &
                                                                                        "       _____________________________"
                    Else
                        txtT_product.Text = All_str &
                                            "        " &
                                                                                        txtthird_country.Text & vbCrLf &
                                                                                        txtback_country.Text & vbCrLf &
                                                                                        txtplace_exibition.Text & vbCrLf &
                                                                                        "       _____________________________"
                    End If

                Else
                    txtT_product.Text = All_str
                End If
            Case 2 'T2_back_country,T3_place_exibition
                'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    If (Not txtquantity1.Text = String.Empty = True) And CommonUtility.Get_StringValue(txtquantity1.Text) <> "" And CommonUtility.Get_Decimal(IIf(Not (txtquantity1.Text = String.Empty) = False, 0, txtquantity1.Value)) <> 0 Then
                        txtT_product.Text = All_str & NewStr_AddressDetail(CommonUtility.Get_StringValue(txtNewEmail_ch02.Text))
                        GroupFooter1.Visible = True
                        txtTotalAll.Visible = True
                        txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " &
                                                                                                        txtback_country.Text & vbCrLf &
                                                                                                        txtplace_exibition.Text & vbCrLf &
                                                                                                        "       _____________________________"
                    Else
                        txtT_product.Text = All_str &
                                            "        " &
                                                                                                        txtback_country.Text & vbCrLf &
                                                                                                        txtplace_exibition.Text & vbCrLf &
                                                                                                        "       _____________________________"
                    End If

                Else
                    txtT_product.Text = All_str
                End If
            Case 3 'T3_place_exibition
                'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    If (Not txtquantity1.Text = String.Empty = True) And CommonUtility.Get_StringValue(txtquantity1.Text) <> "" And CommonUtility.Get_Decimal(IIf(Not (txtquantity1.Text = String.Empty) = False, 0, txtquantity1.Value)) <> 0 Then
                        txtT_product.Text = All_str & NewStr_AddressDetail(CommonUtility.Get_StringValue(txtNewEmail_ch02.Text))
                        GroupFooter1.Visible = True
                        txtTotalAll.Visible = True
                        txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf &
                                                                                        "       _____________________________"
                    Else
                        txtT_product.Text = All_str &
                                            "       _____________________________"
                    End If

                Else
                    txtT_product.Text = All_str
                End If
            Case 4 'ไม่มีเลย
                'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    If (Not txtquantity1.Text = String.Empty = True) And CommonUtility.Get_StringValue(txtquantity1.Text) <> "" And CommonUtility.Get_Decimal(IIf(Not (txtquantity1.Text = String.Empty) = False, 0, txtquantity1.Value)) <> 0 Then
                        txtT_product.Text = All_str & NewStr_AddressDetail(CommonUtility.Get_StringValue(txtNewEmail_ch02.Text))
                        GroupFooter1.Visible = True
                        txtTotalAll.Visible = True
                        txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf &
                                                                                                               "       _____________________________"
                    Else
                        txtT_product.Text = All_str &
                                            "       _____________________________"
                    End If

                Else
                    txtT_product.Text = All_str
                End If
            Case 5 'T2_back_country
                'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    If (Not txtquantity1.Text = String.Empty = True) And CommonUtility.Get_StringValue(txtquantity1.Text) <> "" And CommonUtility.Get_Decimal(IIf(Not (txtquantity1.Text = String.Empty) = False, 0, txtquantity1.Value)) <> 0 Then
                        txtT_product.Text = All_str & NewStr_AddressDetail(CommonUtility.Get_StringValue(txtNewEmail_ch02.Text))
                        GroupFooter1.Visible = True
                        txtTotalAll.Visible = True
                        txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf &
                                                                                        "       _____________________________"
                    Else
                        txtT_product.Text = All_str &
                                           "       _____________________________"
                    End If

                Else
                    txtT_product.Text = All_str
                End If
            Case 6 'T3_place_exibition
                'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    If (Not txtquantity1.Text = String.Empty = True) And CommonUtility.Get_StringValue(txtquantity1.Text) <> "" And CommonUtility.Get_Decimal(IIf(Not (txtquantity1.Text = String.Empty) = False, 0, txtquantity1.Value)) <> 0 Then
                        txtT_product.Text = All_str & NewStr_AddressDetail(CommonUtility.Get_StringValue(txtNewEmail_ch02.Text))
                        GroupFooter1.Visible = True
                        txtTotalAll.Visible = True
                        txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf &
                                                                                        "       _____________________________"
                    Else
                        txtT_product.Text = All_str &
                                            "       _____________________________"
                    End If

                Else
                    txtT_product.Text = All_str
                End If
            Case 7 'T1_third_country,T2_back_country
                'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    If (Not txtquantity1.Text = String.Empty = True) And CommonUtility.Get_StringValue(txtquantity1.Text) <> "" And CommonUtility.Get_Decimal(IIf(Not (txtquantity1.Text = String.Empty) = False, 0, txtquantity1.Value)) <> 0 Then
                        txtT_product.Text = All_str & NewStr_AddressDetail(CommonUtility.Get_StringValue(txtNewEmail_ch02.Text))
                        GroupFooter1.Visible = True
                        txtTotalAll.Visible = True
                        txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " &
                                                                                        txtthird_country.Text & vbCrLf &
                                                                                        txtback_country.Text & vbCrLf &
                                                                                        "       _____________________________"
                    Else
                        txtT_product.Text = All_str &
                                            "        " &
                                                                                        txtthird_country.Text & vbCrLf &
                                                                                        txtback_country.Text & vbCrLf &
                                                                                        "       _____________________________"
                    End If

                Else
                    txtT_product.Text = All_str
                End If
            Case 8 'T1_third_country,T3_place_exibition
                'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    If (Not txtquantity1.Text = String.Empty = True) And CommonUtility.Get_StringValue(txtquantity1.Text) <> "" And CommonUtility.Get_Decimal(IIf(Not (txtquantity1.Text = String.Empty) = False, 0, txtquantity1.Value)) <> 0 Then
                        txtT_product.Text = All_str & NewStr_AddressDetail(CommonUtility.Get_StringValue(txtNewEmail_ch02.Text))
                        GroupFooter1.Visible = True
                        txtTotalAll.Visible = True
                        txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " &
                                                                                        txtthird_country.Text & vbCrLf &
                                                                                        txtplace_exibition.Text & vbCrLf &
                                                                                        "       _____________________________"
                    Else
                        txtT_product.Text = All_str &
                                            "        " &
                                                                                        txtthird_country.Text & vbCrLf &
                                                                                        txtplace_exibition.Text & vbCrLf &
                                                                                        "       _____________________________"
                    End If

                Else
                    txtT_product.Text = All_str
                End If
            Case 9 'T1_third_country
                'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    If (Not txtquantity1.Text = String.Empty = True) And CommonUtility.Get_StringValue(txtquantity1.Text) <> "" And CommonUtility.Get_Decimal(IIf(Not (txtquantity1.Text = String.Empty) = False, 0, txtquantity1.Value)) <> 0 Then
                        txtT_product.Text = All_str & NewStr_AddressDetail(CommonUtility.Get_StringValue(txtNewEmail_ch02.Text))
                        GroupFooter1.Visible = True
                        txtTotalAll.Visible = True
                        txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " &
                                                                                            txtthird_country.Text & vbCrLf &
                                                                                            "       _____________________________"
                    Else
                        txtT_product.Text = All_str &
                                            "        " &
                                                                                            txtthird_country.Text & vbCrLf &
                                                                                            "       _____________________________"
                    End If

                Else
                    txtT_product.Text = All_str
                End If
        End Select
    End Sub
    Sub All_CheckThird_0(ByVal count_data As Integer)
        Title_StrDetail()
        Select Case Check_Third(CommonUtility.Get_StringValue(txtthird_country.Value),
                                CommonUtility.Get_StringValue(txtback_country.Value),
                                CommonUtility.Get_StringValue(txtplace_exibition.Value))
            Case 1
                'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & "        " &
                    txtthird_country.Text & vbCrLf &
                                                                txtback_country.Text & vbCrLf &
                                                                txtplace_exibition.Text & vbCrLf &
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 2 'T2_back_country,T3_place_exibition
                'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str &
                    txtback_country.Text & vbCrLf &
                                                                                txtplace_exibition.Text & vbCrLf &
                                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 3 'T3_place_exibition
                'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str &
                     "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 4 'ไม่มีเลย
                'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str &
                     "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 5 'T2_back_country
                'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str &
                    vbCrLf & "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 6 'T3_place_exibition
                'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str &
                     "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 7 'T1_third_country,T2_back_country
                'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str &
                    txtthird_country.Text & vbCrLf &
                                                                txtback_country.Text & vbCrLf &
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 8 'T1_third_country,T3_place_exibition
                'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str &
                     txtthird_country.Text & vbCrLf &
                                                                txtplace_exibition.Text & vbCrLf &
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 9 'T1_third_country
                'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str &
                    txtthird_country.Text & vbCrLf &
                                                                    "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
        End Select
    End Sub

    Function tariff_All(ByVal _tariff As String) As String
        Dim str_tariff As String
        str_tariff = Mid(_tariff, 1, 4) & "." & Mid(_tariff, 5, 2)

        Return str_tariff
    End Function

    Private Sub PageFooter1_Format(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PageFooter1.Format
        'txtcompany_provincefoot.Text = txtcompany_provincefoot1.Text & " " & format_dateSelect()

        Select Case txtSendCheckSeletedate.Text
            Case True 'ใช้วันที่เลือกเอง
                txtcompany_provincefoot.Text = txtcompany_provincefoot1.Text & " " & txtdateSelectRPT.Text 'format_dateSelect()
            Case False 'ใช้ วันอนุมัติ
                txtcompany_provincefoot.Text = txtcompany_provincefoot1.Text & " " & CommonUtility.Get_DateTime(txtapprove_date.Text).ToString("dd/MM/yyyy") 'format_dateSelect()
        End Select

        'by rut 20-12-2011
        check_show_check()

        'issued
        Select Case CheckIssuedDate_Forms_TP(CDate(txtedi_date.Value), CDate(IIf(IsDBNull(txtprintFormDate.Value) = True, Now.Date, txtprintFormDate.Value)))
            Case True
                Pic_ch7_Issued.Visible = True
            Case False
                Pic_ch7_Issued.Visible = False
        End Select
    End Sub
    'by rut 20-12-2011
    Sub check_show_check()
        Select Case txtshow_check.value
            Case "10" 'Accu
                PCheck_Accu.Visible = True
            Case "01" 'third
                If CommonUtility.Get_StringValue(txtthird_country.Value) <> "" Then
                    PCheck_third_country.Visible = True
                    ''txtTemp_back_country.Text = "THAILAND"
                Else
                    PCheck_third_country.Visible = False
                    ''txtTemp_back_country.Text = "THAILAND"
                End If
            Case "00" 'none
                PCheck_Accu.Visible = False
                PCheck_third_country.Visible = False
                ''txtTemp_back_country.Text = "THAILAND"
            Case "11"
                PCheck_third_country.Visible = True
                PCheck_Accu.Visible = True
        End Select
    End Sub

    Sub Count_RowToPage_DetailData()
        Dim CountData_T, CountData_T2 As Integer
        CountData_T = C_TotalRowDe.Text
        CountData_T2 = CountData_T / 7

        '===============================================================
        'If CommonUtility.Get_StringValue(txtgross_weight.Text) <> "" Then
        '    txtgross_weight1.Text = txtTemp_Gross_Weight.Text + " " + txtg_Unit_Desc.Text
        'Else
        '    txtgross_weight1.Text = ""
        'End If
        '============================================================
        CallInvoiceCheck()

        'CHeck_DisPlay()
        '============================================================
        'If txtNumRowCount.Text = C_TotalRowDe.Text Then

        If (CommonUtility.Get_StringValue(txtquantity1.Text) <> "" And CommonUtility.Get_Decimal(txtquantity1.Text) <> 0 And CommonUtility.Get_StringValue(txtq_unit_code1.Text) <> "") = True And
        (CommonUtility.Get_StringValue(txtquantity2.Text) <> "" And CommonUtility.Get_Decimal(txtquantity2.Text) <> 0 And CommonUtility.Get_StringValue(txtq_unit_code2.Text) <> "") = True And
        (CommonUtility.Get_StringValue(txtquantity3.Text) <> "" And CommonUtility.Get_Decimal(txtquantity3.Text) <> 0 And CommonUtility.Get_StringValue(txtq_unit_code3.Text) <> "") = True And
        (CommonUtility.Get_StringValue(txtquantity4.Text) <> "" And CommonUtility.Get_Decimal(txtquantity4.Text) <> 0 And CommonUtility.Get_StringValue(txtq_unit_code4.Text) <> "") = True And
        (CommonUtility.Get_StringValue(txtquantity5.Text) <> "" And CommonUtility.Get_Decimal(txtquantity5.Text) <> 0 And CommonUtility.Get_StringValue(txtq_unit_code5.Text) <> "") = True Then
            'มีหมด
            All_CheckThird_5(Cpage)
            'If CommonUtility.Get_StringValue(txtthird_country.Value) <> "" Then
            '    txtT_product.Text = "HS. CODE. " + Mid(CommonUtility.Get_StringValue(txttariff_code.Text), 1, 6) _
            '                            + txtproduct_n1.Text + txtproduct_n2.Text + "****" + vbCrLf + _
            '                            "TOTAL: " + BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) + vbCrLf + "        " + _
            '                            BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) + vbCrLf + "        " + _
            '                            BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) + vbCrLf + "        " + _
            '                            BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) + vbCrLf + "        " + _
            '                            BahtOnly(txtquantity5.Text, txtq_unit_code5.Text) + vbCrLf + txtthird_country.Text + vbCrLf + _
            '                            "_____________________________"
            'Else
            '    txtT_product.Text = "HS. CODE. " + Mid(CommonUtility.Get_StringValue(txttariff_code.Text), 1, 6) _
            '                            + txtproduct_n1.Text + txtproduct_n2.Text + "****" + vbCrLf + _
            '                            "TOTAL: " + BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) + vbCrLf + "        " + _
            '                            BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) + vbCrLf + "        " + _
            '                            BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) + vbCrLf + "        " + _
            '                            BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) + vbCrLf + "        " + _
            '                            BahtOnly(txtquantity5.Text, txtq_unit_code5.Text) + vbCrLf + _
            '                            "_____________________________"
            'End If
        ElseIf (CommonUtility.Get_StringValue(txtquantity1.Text) <> "" And CommonUtility.Get_Decimal(txtquantity1.Text) <> 0 And CommonUtility.Get_StringValue(txtq_unit_code1.Text) <> "") = True And
        (CommonUtility.Get_StringValue(txtquantity2.Text) <> "" And CommonUtility.Get_Decimal(txtquantity2.Text) <> 0 And CommonUtility.Get_StringValue(txtq_unit_code2.Text) <> "") = True And
        (CommonUtility.Get_StringValue(txtquantity3.Text) <> "" And CommonUtility.Get_Decimal(txtquantity3.Text) <> 0 And CommonUtility.Get_StringValue(txtq_unit_code3.Text) <> "") = True And
        (CommonUtility.Get_StringValue(txtquantity4.Text) <> "" And CommonUtility.Get_Decimal(txtquantity4.Text) <> 0 And CommonUtility.Get_StringValue(txtq_unit_code4.Text) <> "") = True And
        (CommonUtility.Get_StringValue(txtquantity5.Text) <> "" And CommonUtility.Get_Decimal(txtquantity5.Text) <> 0 And CommonUtility.Get_StringValue(txtq_unit_code5.Text) <> "") = False Then
            'ขาด 5
            All_CheckThird_4(Cpage)
            'If CommonUtility.Get_StringValue(txtthird_country.Value) <> "" Then
            '    txtT_product.Text = "HS. CODE. " + Mid(CommonUtility.Get_StringValue(txttariff_code.Text), 1, 6) _
            '                            + txtproduct_n1.Text + txtproduct_n2.Text + "****" + vbCrLf + _
            '                            "TOTAL: " + BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) + vbCrLf + "        " + _
            '                            BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) + vbCrLf + "        " + _
            '                            BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) + vbCrLf + "        " + _
            '                            BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) + vbCrLf + txtthird_country.Text + vbCrLf + _
            '                            "_____________________________"
            'Else
            '    txtT_product.Text = "HS. CODE. " + Mid(CommonUtility.Get_StringValue(txttariff_code.Text), 1, 6) _
            '                            + txtproduct_n1.Text + txtproduct_n2.Text + "****" + vbCrLf + _
            '                            "TOTAL: " + BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) + vbCrLf + "        " + _
            '                            BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) + vbCrLf + "        " + _
            '                            BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) + vbCrLf + "        " + _
            '                            BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) + vbCrLf + _
            '                            "_____________________________"
            'End If


        ElseIf (CommonUtility.Get_StringValue(txtquantity1.Text) <> "" And CommonUtility.Get_Decimal(txtquantity1.Text) <> 0 And CommonUtility.Get_StringValue(txtq_unit_code1.Text) <> "") = True And
         (CommonUtility.Get_StringValue(txtquantity2.Text) <> "" And CommonUtility.Get_Decimal(txtquantity2.Text) <> 0 And CommonUtility.Get_StringValue(txtq_unit_code2.Text) <> "") = True And
         (CommonUtility.Get_StringValue(txtquantity3.Text) <> "" And CommonUtility.Get_Decimal(txtquantity3.Text) <> 0 And CommonUtility.Get_StringValue(txtq_unit_code3.Text) <> "") = True And
         (CommonUtility.Get_StringValue(txtquantity4.Text) <> "" And CommonUtility.Get_Decimal(txtquantity4.Text) <> 0 And CommonUtility.Get_StringValue(txtq_unit_code4.Text) <> "") = False And
         (CommonUtility.Get_StringValue(txtquantity5.Text) <> "" And CommonUtility.Get_Decimal(txtquantity5.Text) <> 0 And CommonUtility.Get_StringValue(txtq_unit_code5.Text) <> "") = False Then
            'ขาด 4,5
            All_CheckThird_3(Cpage)
            'If CommonUtility.Get_StringValue(txtthird_country.Value) <> "" Then
            '    txtT_product.Text = "HS. CODE. " + Mid(CommonUtility.Get_StringValue(txttariff_code.Text), 1, 6) _
            '                            + txtproduct_n1.Text + txtproduct_n2.Text + "****" + vbCrLf + _
            '                            "TOTAL: " + BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) + vbCrLf + "        " + _
            '                            BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) + vbCrLf + "        " + _
            '                            BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) + vbCrLf + txtthird_country.Text + vbCrLf + _
            '                            "_____________________________"
            'Else
            '    txtT_product.Text = "HS. CODE. " + Mid(CommonUtility.Get_StringValue(txttariff_code.Text), 1, 6) _
            '                            + txtproduct_n1.Text + txtproduct_n2.Text + "****" + vbCrLf + _
            '                            "TOTAL: " + BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) + vbCrLf + "        " + _
            '                            BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) + vbCrLf + "        " + _
            '                            BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) + vbCrLf + _
            '                            "_____________________________"
            'End If

        ElseIf (CommonUtility.Get_StringValue(txtquantity1.Text) <> "" And CommonUtility.Get_Decimal(txtquantity1.Text) <> 0 And CommonUtility.Get_StringValue(txtq_unit_code1.Text) <> "") = True And
          (CommonUtility.Get_StringValue(txtquantity2.Text) <> "" And CommonUtility.Get_Decimal(txtquantity2.Text) <> 0 And CommonUtility.Get_StringValue(txtq_unit_code2.Text) <> "") = True And
          (CommonUtility.Get_StringValue(txtquantity3.Text) <> "" And CommonUtility.Get_Decimal(txtquantity3.Text) <> 0 And CommonUtility.Get_StringValue(txtq_unit_code3.Text) <> "") = False And
          (CommonUtility.Get_StringValue(txtquantity4.Text) <> "" And CommonUtility.Get_Decimal(txtquantity4.Text) <> 0 And CommonUtility.Get_StringValue(txtq_unit_code4.Text) <> "") = False And
          (CommonUtility.Get_StringValue(txtquantity5.Text) <> "" And CommonUtility.Get_Decimal(txtquantity5.Text) <> 0 And CommonUtility.Get_StringValue(txtq_unit_code5.Text) <> "") = False Then
            'ขาด 3,4,5
            All_CheckThird_2(Cpage)
            'If CommonUtility.Get_StringValue(txtthird_country.Value) <> "" Then
            '    txtT_product.Text = "HS. CODE. " + Mid(CommonUtility.Get_StringValue(txttariff_code.Text), 1, 6) _
            '                            + txtproduct_n1.Text + txtproduct_n2.Text + "****" + vbCrLf + _
            '                            "TOTAL: " + BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) + vbCrLf + "        " + _
            '                            BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) + vbCrLf + txtthird_country.Text + vbCrLf + _
            '                            "_____________________________"

            'Else
            '    txtT_product.Text = "HS. CODE. " + Mid(CommonUtility.Get_StringValue(txttariff_code.Text), 1, 6) _
            '                            + txtproduct_n1.Text + txtproduct_n2.Text + "****" + vbCrLf + _
            '                            "TOTAL: " + BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) + vbCrLf + "        " + _
            '                            BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) + vbCrLf + _
            '                            "_____________________________"

            'End If

        ElseIf (CommonUtility.Get_StringValue(txtquantity1.Text) <> "" And CommonUtility.Get_Decimal(txtquantity1.Text) <> 0 And CommonUtility.Get_StringValue(txtq_unit_code1.Text) <> "") = True And
          (CommonUtility.Get_StringValue(txtquantity2.Text) <> "" And CommonUtility.Get_Decimal(txtquantity2.Text) <> 0 And CommonUtility.Get_StringValue(txtq_unit_code2.Text) <> "") = False And
          (CommonUtility.Get_StringValue(txtquantity3.Text) <> "" And CommonUtility.Get_Decimal(txtquantity3.Text) <> 0 And CommonUtility.Get_StringValue(txtq_unit_code3.Text) <> "") = False And
          (CommonUtility.Get_StringValue(txtquantity4.Text) <> "" And CommonUtility.Get_Decimal(txtquantity4.Text) <> 0 And CommonUtility.Get_StringValue(txtq_unit_code4.Text) <> "") = False And
          (CommonUtility.Get_StringValue(txtquantity5.Text) <> "" And CommonUtility.Get_Decimal(txtquantity5.Text) <> 0 And CommonUtility.Get_StringValue(txtq_unit_code5.Text) <> "") = False Then
            'ขาด 2,3,4,5
            All_CheckThird_1(Cpage)
            'If CommonUtility.Get_StringValue(txtthird_country.Value) <> "" Then
            '    txtT_product.Text = "HS. CODE. " + Mid(CommonUtility.Get_StringValue(txttariff_code.Text), 1, 6) _
            '                            + txtproduct_n1.Text + txtproduct_n2.Text + "****" + vbCrLf + _
            '                            "TOTAL: " + BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) + vbCrLf + txtthird_country.Text + vbCrLf + _
            '                            "_____________________________"
            'Else
            '    txtT_product.Text = "HS. CODE. " + Mid(CommonUtility.Get_StringValue(txttariff_code.Text), 1, 6) _
            '                            + txtproduct_n1.Text + txtproduct_n2.Text + "****" + vbCrLf + _
            '                            "TOTAL: " + BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) + vbCrLf + _
            '                            "_____________________________"
            'End If

        Else
            All_CheckThird_1(Cpage)
            'If CommonUtility.Get_StringValue(txtthird_country.Value) <> "" Then
            '    txtT_product.Text = "HS. CODE. " + Mid(CommonUtility.Get_StringValue(txttariff_code.Text), 1, 6) _
            '                                                    + txtproduct_n1.Text + txtproduct_n2.Text + "****" + vbCrLf + txtthird_country.Text + vbCrLf + _
            '                            "_____________________________"
            'Else
            '    txtT_product.Text = "HS. CODE. " + Mid(CommonUtility.Get_StringValue(txttariff_code.Text), 1, 6) _
            '                                                    + txtproduct_n1.Text + txtproduct_n2.Text + "****" + vbCrLf + _
            '                            "_____________________________"
            'End If

        End If

        'Else
        '    txtT_product.Text = "HS. CODE. " + Mid(CommonUtility.Get_StringValue(txttariff_code.Text), 1, 6) + " " _
        '    + txtproduct_n1.Text + txtproduct_n2.Text + "****"
        'End If

        ''============================================================
        'If CommonUtility.Get_StringValue(txtNumRowCount.Text) = "1" Then
        '    txtmarks.Visible = True
        '    'txtgross_weight1.Visible = True
        '    txtTolInvoice.Visible = True
        'Else
        '    txtmarks.Visible = False
        '    'txtgross_weight1.Visible = False
        '    txtTolInvoice.Visible = False
        'End If
    End Sub

    'มี gross ไม่มี fob
    Sub CHeck_DisPlay()
        Dim str_gross As String

        If Mid(CommonUtility.Get_StringValue(txtFOBDisplay.Value), 1, 50) = "Display" Then

            If Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "GROSS WEIGHT - NET WEIGHT" Then
                str_gross = GrossTxt() & vbNewLine & NetWeight_(txtnet_weight.Value, txtunit_code2.Value) 'Mid(txtnet_weight.Value, 1, 9) & " " & Mid(txtunit_code2.Value, 1, 10) '& vbNewLine & FobTxt()
                Select Case C_TotalRowDe.Text
                    Case 1
                        txtGrossTxt.Text = str_gross
                    Case Else
                        If Cpage > 1 Then
                            str_gross = ""
                            txtGrossTxt.Text = str_gross
                        ElseIf Cpage = 1 Then

                            txtGrossTxt.Text = str_gross
                        End If
                End Select
            ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "GROSS WEIGHT" Then
                str_gross = GrossTxt() '& vbNewLine & FobTxt()
                Select Case C_TotalRowDe.Text
                    Case 1
                        txtGrossTxt.Text = str_gross
                    Case Else
                        If Cpage > 1 Then
                            str_gross = ""
                            txtGrossTxt.Text = str_gross
                        ElseIf Cpage = 1 Then

                            txtGrossTxt.Text = str_gross
                        End If
                End Select
            ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "" Then
                str_gross = GrossTxt() '& vbNewLine & FobTxt()
                Select Case C_TotalRowDe.Text
                    Case 1
                        txtGrossTxt.Text = str_gross
                    Case Else
                        If Cpage > 1 Then
                            str_gross = ""
                            txtGrossTxt.Text = str_gross
                        ElseIf Cpage = 1 Then

                            txtGrossTxt.Text = str_gross
                        End If
                End Select
            End If
        ElseIf Mid(CommonUtility.Get_StringValue(txtFOBDisplay.Value), 1, 50) = "NoDisplay" Then
            If Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "NET WEIGHT" Then
                str_gross = NetWeight_(txtnet_weight.Value, txtunit_code2.Value) 'Mid(txtnet_weight.Value, 1, 9) & " " & Mid(txtunit_code2.Value, 1, 10)
                Select Case C_TotalRowDe.Text
                    Case 1
                        txtGrossTxt.Text = str_gross
                    Case Else
                        If Cpage > 1 Then
                            str_gross = ""
                            txtGrossTxt.Text = str_gross
                        ElseIf Cpage = 1 Then

                            txtGrossTxt.Text = str_gross
                        End If
                End Select
            ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "GROSS WEIGHT - NET WEIGHT" Then
                str_gross = GrossTxt() & vbNewLine & NetWeight_(txtnet_weight.Value, txtunit_code2.Value) 'Mid(txtnet_weight.Value, 1, 9) & " " & Mid(txtunit_code2.Value, 1, 10)
                Select Case C_TotalRowDe.Text
                    Case 1
                        txtGrossTxt.Text = str_gross
                    Case Else
                        If Cpage > 1 Then
                            str_gross = ""
                            txtGrossTxt.Text = str_gross
                        ElseIf Cpage = 1 Then

                            txtGrossTxt.Text = str_gross
                        End If
                End Select
            ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "GROSS WEIGHT" Then
                str_gross = GrossTxt()
                Select Case C_TotalRowDe.Text
                    Case 1
                        txtGrossTxt.Text = str_gross
                    Case Else
                        If Cpage > 1 Then
                            str_gross = ""
                            txtGrossTxt.Text = str_gross
                        ElseIf Cpage = 1 Then

                            txtGrossTxt.Text = str_gross
                        End If
                End Select
            ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "" Then
                str_gross = GrossTxt()
                Select Case C_TotalRowDe.Text
                    Case 1
                        txtGrossTxt.Text = str_gross
                    Case Else
                        If Cpage > 1 Then
                            str_gross = ""
                            txtGrossTxt.Text = str_gross
                        ElseIf Cpage = 1 Then

                            txtGrossTxt.Text = str_gross
                        End If
                End Select
            End If

        ElseIf Mid(CommonUtility.Get_StringValue(txtFOBDisplay.Value), 1, 50) = "" Then

            If Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "NET WEIGHT" Then
                str_gross = NetWeight_(txtnet_weight.Value, txtunit_code2.Value) 'Mid(txtnet_weight.Value, 1, 9) & " " & Mid(txtunit_code2.Value, 1, 10) '& vbNewLine & FobTxt()
                Select Case C_TotalRowDe.Text
                    Case 1
                        txtGrossTxt.Text = str_gross
                    Case Else
                        If Cpage > 1 Then
                            str_gross = ""
                            txtGrossTxt.Text = str_gross
                        ElseIf Cpage = 1 Then

                            txtGrossTxt.Text = str_gross
                        End If
                End Select
            ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "GROSS WEIGHT - NET WEIGHT" Then
                str_gross = GrossTxt() & vbNewLine & NetWeight_(txtnet_weight.Value, txtunit_code2.Value) 'Mid(txtnet_weight.Value, 1, 9) & " " & Mid(txtunit_code2.Value, 1, 10) '& vbNewLine & FobTxt()
                Select Case C_TotalRowDe.Text
                    Case 1
                        txtGrossTxt.Text = str_gross
                    Case Else
                        If Cpage > 1 Then
                            str_gross = ""
                            txtGrossTxt.Text = str_gross
                        ElseIf Cpage = 1 Then

                            txtGrossTxt.Text = str_gross
                        End If
                End Select
            ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "GROSS WEIGHT" Then
                str_gross = GrossTxt() '& vbNewLine & FobTxt()
                Select Case C_TotalRowDe.Text
                    Case 1
                        txtGrossTxt.Text = str_gross
                    Case Else
                        If Cpage > 1 Then
                            str_gross = ""
                            txtGrossTxt.Text = str_gross
                        ElseIf Cpage = 1 Then

                            txtGrossTxt.Text = str_gross
                        End If
                End Select
            ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "" Then
                str_gross = GrossTxt() '& vbNewLine & FobTxt()
                Select Case C_TotalRowDe.Text
                    Case 1
                        txtGrossTxt.Text = str_gross
                    Case Else
                        If Cpage > 1 Then
                            str_gross = ""
                            txtGrossTxt.Text = str_gross
                        ElseIf Cpage = 1 Then

                            txtGrossTxt.Text = str_gross
                        End If
                End Select
            End If
        End If
    End Sub
    Function CHeck_DisPlayF() As String
        CallInvoice_()
        Dim str_GrossF As String

        Dim sss As String = txtgross_weight.Text

        Dim str_gross As String

        'str_gross = txtgross_weight.Text

        'เรื่อง invoice ต่างประเทศ
        Dim str_NumInvoice As String = CommonUtility.Get_StringValue(txtNumInvoice.Text)
        'เรื่อง มูลค่า ต่างประเทศ
        Dim str_USDInvoice As String
        If CommonUtility.Get_StringValue(txtUSDInvoice.Value) <> "" Then
            str_USDInvoice = Format(CommonUtility.Get_Decimal(txtUSDInvoice.Value), "#,##0.00") & " USD"
        Else
            str_USDInvoice = FobTxtAllSum()
        End If

        Select Case txtshow_check.Text
            Case "01" ' เรื่อง invoice ต่างประเทศ
                If Mid(CommonUtility.Get_StringValue(txtFOBDisplay.Value), 1, 50) = "Display" Then
                    If Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "GROSS WEIGHT - NET WEIGHT" Then
                        str_gross = GrossTxt() & vbNewLine & NetWeight_(TNet, txtunit_code2.Value) 'Mid(Tnet, 1, 9) & " " & Mid(txtunit_code2.Value, 1, 10) '& vbNewLine & FobTxtAll()
                        str_GrossF = str_gross & vbNewLine & str_USDInvoice
                    ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "GROSS WEIGHT" Then
                        str_gross = GrossTxt() '& vbNewLine & FobTxtAll()
                        str_GrossF = str_gross & vbNewLine & str_USDInvoice
                    ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "" Then
                        str_gross = GrossTxt() '& vbNewLine & FobTxtAll()
                        str_GrossF = str_gross & vbNewLine & str_USDInvoice

                    ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "NW" Then 'ระบบเก่า
                        str_gross = NetWeight_(TNet, txtunit_code2.Value)
                        str_GrossF = str_gross & vbNewLine & Callinvoice_board(1)
                    ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "GW-NW" Then 'ระบบเก่า
                        str_gross = GrossTxt() & vbNewLine & NetWeight_(TNet, txtunit_code2.Value)
                        str_GrossF = str_gross & vbNewLine & Callinvoice_board(1)
                    ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "GW" Then 'ระบบเก่า
                        str_gross = GrossTxt()
                        str_GrossF = str_gross & vbNewLine & Callinvoice_board(1)
                    End If
                ElseIf Mid(CommonUtility.Get_StringValue(txtFOBDisplay.Value), 1, 50) = "NoDisplay" Then
                    If Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "GROSS WEIGHT - NET WEIGHT" Then
                        str_gross = GrossTxt() & vbNewLine & NetWeight_(TNet, txtunit_code2.Value) 'Mid(Tnet, 1, 9) & " " & Mid(txtunit_code2.Value, 1, 10)
                        str_GrossF = str_gross & vbNewLine & str_USDInvoice
                    ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "GROSS WEIGHT" Then
                        str_gross = GrossTxt()
                        str_GrossF = str_gross & vbNewLine & str_USDInvoice
                    ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "" Then
                        str_gross = GrossTxt()
                        str_GrossF = str_gross & vbNewLine & str_USDInvoice

                    ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "NW" Then 'ระบบเก่า
                        str_gross = NetWeight_(TNet, txtunit_code2.Value)
                        str_GrossF = str_gross & vbNewLine & Callinvoice_board(1)
                    ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "GW-NW" Then 'ระบบเก่า
                        str_gross = GrossTxt() & vbNewLine & NetWeight_(TNet, txtunit_code2.Value)
                        str_GrossF = str_gross & vbNewLine & Callinvoice_board(1)
                    ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "GW" Then 'ระบบเก่า
                        str_gross = GrossTxt()
                        str_GrossF = str_gross & vbNewLine & Callinvoice_board(1)
                    End If

                End If

            Case Else 'ไม่ใช่ invoice ต่างประเทศ
                If Mid(CommonUtility.Get_StringValue(txtFOBDisplay.Value), 1, 50) = "Display" Then

                    If Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "GROSS WEIGHT - NET WEIGHT" Then
                        str_gross = GrossTxt() & vbNewLine & NetWeight_(TNet, txtunit_code2.Value) & vbNewLine & FobTxtAllSum()
                        str_GrossF = str_gross
                    ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "GROSS WEIGHT" Then
                        str_gross = GrossTxt() & vbNewLine & FobTxtAllSum()
                        str_GrossF = str_gross
                    ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "" Then
                        str_gross = GrossTxt() & vbNewLine & FobTxtAllSum()
                        str_GrossF = str_gross

                    ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "GW" Then 'ระบบเก่า
                        str_gross = GrossTxt() & vbNewLine & FobTxtAllSum()
                        str_GrossF = str_gross
                    ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "GW-NW" Then 'ระบบเก่า
                        str_gross = GrossTxt() & vbNewLine & NetWeight_(TNet, txtunit_code2.Value) & vbNewLine & FobTxtAllSum()
                        str_GrossF = str_gross
                    ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "NW" Then 'ระบบเก่า
                        str_gross = NetWeight_(TNet, txtunit_code2.Value) & vbNewLine & FobTxtAllSum()
                        str_GrossF = str_gross
                    End If
                ElseIf Mid(CommonUtility.Get_StringValue(txtFOBDisplay.Value), 1, 50) = "NoDisplay" Then
                    If Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "NET WEIGHT" Then
                        str_gross = NetWeight_(TNet, txtunit_code2.Value) 'Mid(Tnet, 1, 9) & " " & Mid(txtunit_code2.Value, 1, 10)
                        str_GrossF = str_gross
                    ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "GROSS WEIGHT - NET WEIGHT" Then
                        str_gross = GrossTxt() & vbNewLine & NetWeight_(TNet, txtunit_code2.Value) 'Mid(Tnet, 1, 9) & " " & Mid(txtunit_code2.Value, 1, 10)
                        str_GrossF = str_gross
                    ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "GROSS WEIGHT" Then
                        str_gross = GrossTxt()
                        str_GrossF = str_gross
                    ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "" Then
                        str_gross = GrossTxt()
                        str_GrossF = str_gross

                    ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "GW" Then 'ระบบเก่า
                        str_gross = GrossTxt()
                        str_GrossF = str_gross
                    ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "GW-NW" Then 'ระบบเก่า
                        str_gross = GrossTxt() & vbNewLine & NetWeight_(TNet, txtunit_code2.Value)
                        str_GrossF = str_gross
                    ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "NW" Then 'ระบบเก่า
                        str_gross = NetWeight_(TNet, txtunit_code2.Value)
                        str_GrossF = str_gross
                    End If

                End If

        End Select
        Return str_GrossF
    End Function
    Function CallInvoice_() As String 'Check Invoince
        Dim str_invoice As String
        Select Case txtshow_check.Text
            Case "10"
                Select Case CommonUtility.Get_StringValue(Check_NULLALL(txtNumInvoice.Text))
                    Case ""
                        str_invoice = Callinvoice_board(0)
                    Case Is <> ""
                        str_invoice = txtNumInvoice.Text
                End Select

            Case Else
                Select Case Check_numinvoiceAll(CommonUtility.Get_String(txtinvoice_no1.Text), CommonUtility.Get_String(txtinvoice_no2.Text), CommonUtility.Get_String(txtinvoice_no3.Text), CommonUtility.Get_String(txtinvoice_no4.Text), CommonUtility.Get_String(txtinvoice_no5.Text))
                    Case 1
                        If CommonUtility.Get_String(txtinvoice_no1.Text) <> "" And CommonUtility.Get_String(txtinvoice_date1.Text) <> "" Then
                            '1 invoice
                            str_invoice = txtinvoice_no1.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date1.Text) <> "", confor_mat(CDate(txtinvoice_date1.Value).Date), Nothing)
                        End If
                    Case 2
                        If CommonUtility.Get_String(txtinvoice_no1.Text) <> "" And CommonUtility.Get_String(txtinvoice_date1.Text) <> "" And CommonUtility.Get_String(txtinvoice_no2.Text) <> "" And CommonUtility.Get_String(txtinvoice_date2.Text) <> "" Then
                            '2 invoice
                            str_invoice = txtinvoice_no1.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date1.Text) <> "", confor_mat(CDate(txtinvoice_date1.Value).Date), Nothing) & vbNewLine _
                                                & txtinvoice_no2.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date2.Text) <> "", confor_mat(CDate(txtinvoice_date2.Value).Date), Nothing)
                        End If
                    Case 3
                        If CommonUtility.Get_String(txtinvoice_no1.Text) <> "" And CommonUtility.Get_String(txtinvoice_date1.Text) <> "" And CommonUtility.Get_String(txtinvoice_no2.Text) <> "" And CommonUtility.Get_String(txtinvoice_date2.Text) <> "" And CommonUtility.Get_String(txtinvoice_no3.Text) <> "" And CommonUtility.Get_String(txtinvoice_date3.Text) <> "" Then
                            '3 invoice
                            str_invoice = txtinvoice_no1.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date1.Text) <> "", confor_mat(CDate(txtinvoice_date1.Value).Date), Nothing) & vbNewLine _
                                                & txtinvoice_no2.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date2.Text) <> "", confor_mat(CDate(txtinvoice_date2.Value).Date), Nothing) & vbNewLine _
                                                & txtinvoice_no3.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date3.Text) <> "", confor_mat(CDate(txtinvoice_date3.Value).Date), Nothing)
                        End If
                    Case 4
                        If CommonUtility.Get_String(txtinvoice_no1.Text) <> "" And CommonUtility.Get_String(txtinvoice_date1.Text) <> "" And CommonUtility.Get_String(txtinvoice_no2.Text) <> "" And CommonUtility.Get_String(txtinvoice_date2.Text) <> "" And CommonUtility.Get_String(txtinvoice_no3.Text) <> "" And CommonUtility.Get_String(txtinvoice_date3.Text) <> "" And CommonUtility.Get_String(txtinvoice_no4.Text) <> "" And CommonUtility.Get_String(txtinvoice_date4.Text) <> "" Then
                            '4 invoice
                            str_invoice = txtinvoice_no1.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date1.Text) <> "", confor_mat(CDate(txtinvoice_date1.Value).Date), Nothing) & vbNewLine _
                                                & txtinvoice_no2.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date2.Text) <> "", confor_mat(CDate(txtinvoice_date2.Value).Date), Nothing) & vbNewLine _
                                                & txtinvoice_no3.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date3.Text) <> "", confor_mat(CDate(txtinvoice_date3.Value).Date), Nothing) & vbNewLine _
                                                & txtinvoice_no4.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date4.Text) <> "", confor_mat(CDate(txtinvoice_date4.Value).Date), Nothing)
                        End If
                    Case 5
                        If CommonUtility.Get_String(txtinvoice_no1.Text) <> "" And CommonUtility.Get_String(txtinvoice_date1.Text) <> "" And CommonUtility.Get_String(txtinvoice_no2.Text) <> "" And CommonUtility.Get_String(txtinvoice_date2.Text) <> "" And CommonUtility.Get_String(txtinvoice_no3.Text) <> "" And CommonUtility.Get_String(txtinvoice_date3.Text) <> "" And CommonUtility.Get_String(txtinvoice_no4.Text) <> "" And CommonUtility.Get_String(txtinvoice_date4.Text) <> "" And CommonUtility.Get_String(txtinvoice_no5.Text) <> "" And CommonUtility.Get_String(txtinvoice_date5.Text) <> "" Then
                            '5 invoice
                            str_invoice = txtinvoice_no1.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date1.Text) <> "", confor_mat(CDate(txtinvoice_date1.Value).Date), Nothing) & vbNewLine _
                                                & txtinvoice_no2.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date2.Text) <> "", confor_mat(CDate(txtinvoice_date2.Value).Date), Nothing) & vbNewLine _
                                                & txtinvoice_no3.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date3.Text) <> "", confor_mat(CDate(txtinvoice_date3.Value).Date), Nothing) & vbNewLine _
                                                & txtinvoice_no4.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date4.Text) <> "", confor_mat(CDate(txtinvoice_date4.Value).Date), Nothing) & vbNewLine _
                                                & txtinvoice_no5.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date5.Text) <> "", confor_mat(CDate(txtinvoice_date5.Value).Date), Nothing)
                        End If
                End Select
        End Select
        Return str_invoice
    End Function
    Function Callinvoice_board() As Array
        Dim sNewValue As Array
        Dim sValue As Array = Split(txtinvoice_board.Text, "%")
        Dim _persenStr As String = CommonUtility.Get_StringValue(txtinvoice_board.Text) & "%"
        Select Case sValue.Length
            Case 1
                sNewValue = Split(_persenStr, "%")
                Return sNewValue
            Case Else
                Return sValue
        End Select
    End Function
    ''Function CHeck_DisPlayF() As String
    ''    Dim str_GrossF As String

    ''    Dim str_gross As String

    ''    If Mid(CommonUtility.Get_StringValue(txtFOBDisplay.Value), 1, 50) = "Display" Then

    ''        If Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "GROSS WEIGHT - NET WEIGHT" Then
    ''            str_gross = GrossTxt() & vbNewLine & NetWeight_(Tnet, txtunit_code2.Value) 'Mid(Tnet, 1, 9) & " " & Mid(txtunit_code2.Value, 1, 10) '& vbNewLine & FobTxt()
    ''            str_GrossF = str_gross
    ''        ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "GROSS WEIGHT" Then
    ''            str_gross = GrossTxt() '& vbNewLine & FobTxt()
    ''            str_GrossF = str_gross
    ''        ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "" Then
    ''            str_gross = GrossTxt() '& vbNewLine & FobTxt()
    ''            str_GrossF = str_gross
    ''        End If
    ''    ElseIf Mid(CommonUtility.Get_StringValue(txtFOBDisplay.Value), 1, 50) = "NoDisplay" Then
    ''        If Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "NET WEIGHT" Then
    ''            str_gross = NetWeight_(Tnet, txtunit_code2.Value) 'Mid(Tnet, 1, 9) & " " & Mid(txtunit_code2.Value, 1, 10)
    ''            str_GrossF = str_gross
    ''        ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "GROSS WEIGHT - NET WEIGHT" Then
    ''            str_gross = GrossTxt() & vbNewLine & NetWeight_(Tnet, txtunit_code2.Value) 'Mid(Tnet, 1, 9) & " " & Mid(txtunit_code2.Value, 1, 10)
    ''            str_GrossF = str_gross
    ''        ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "GROSS WEIGHT" Then
    ''            str_gross = GrossTxt()
    ''            str_GrossF = str_gross
    ''        ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "" Then
    ''            str_gross = GrossTxt()
    ''            str_GrossF = str_gross
    ''        End If

    ''    ElseIf Mid(CommonUtility.Get_StringValue(txtFOBDisplay.Value), 1, 50) = "" Then

    ''        If Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "NET WEIGHT" Then
    ''            str_gross = NetWeight_(Tnet, txtunit_code2.Value) 'Mid(Tnet, 1, 9) & " " & Mid(txtunit_code2.Value, 1, 10) '& vbNewLine & FobTxt()
    ''            str_GrossF = str_gross
    ''        ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "GROSS WEIGHT - NET WEIGHT" Then
    ''            str_gross = GrossTxt() & vbNewLine & NetWeight_(Tnet, txtunit_code2.Value) 'Mid(Tnet, 1, 9) & " " & Mid(txtunit_code2.Value, 1, 10) '& vbNewLine & FobTxt()
    ''            str_GrossF = str_gross
    ''        ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "GROSS WEIGHT" Then
    ''            str_gross = GrossTxt() '& vbNewLine & FobTxt()
    ''            str_GrossF = str_gross
    ''        ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "" Then
    ''            str_gross = GrossTxt() '& vbNewLine & FobTxt()
    ''            str_GrossF = str_gross
    ''        End If
    ''    End If
    ''    Return str_GrossF
    ''End Function

    Function GrossTxt() As String
        Dim Str_GrossTxt As String
        Dim Dec_Gross As Decimal
        Dec_Gross = CDec(Check_Null(CommonUtility.Get_StringValue(txtgross_weightH.Value))) 'Format(CDec(Check_Null(CommonUtility.Get_StringValue(txtgross_weightH.Value))), "#,##0.00##")
        Str_GrossTxt = Format(Dec_Gross, "#,##0.00##") & " " & CommonUtility.Get_StringValue(txtg_unit_code.Value) 'CStr(Dec_Gross) & " " & CommonUtility.Get_StringValue(txtg_unit_code.Value)

        Return Str_GrossTxt
    End Function

    Function FobTxt() As String
        Dim Str_FobTxt As String
        Dim Dec_Fob As Decimal

        If txtFOB_AMT.Value > 0 Then
            Dec_Fob = CDec(Check_Null(CommonUtility.Get_StringValue(txtFOB_AMT.Value))) 'Format(CDec(Check_Null(CommonUtility.Get_StringValue(txtFOB_AMT.Value))), "#,##0.0###")
            Str_FobTxt = Format(Dec_Fob, "#,##0.00##") & " USD"
        Else
            Str_FobTxt = ""
        End If
        Return Str_FobTxt
    End Function
    Function FobTxtAllSum() As String
        Dim Str_FobTxt As String
        Dim Dec_Fob As Decimal

        If txtFOB_AMT.Value > 0 Then
            Dec_Fob = CDec(Check_Null(CommonUtility.Get_StringValue(txttotalSum_fob_amt.Value))) 'Format(CDec(Check_Null(CommonUtility.Get_StringValue(txtFOB_AMT.Value))), "#,##0.0###")
            Str_FobTxt = Format(Dec_Fob, "#,##0.00##") & " USD"
        Else
            Str_FobTxt = ""
        End If
        Return Str_FobTxt
    End Function
    Function NetWeight_(ByVal _netweight, ByVal _uni) As String
        Dim sumStr As String
        sumStr = Format(_netweight, "#,##0.00##") & " " & _uni

        Return sumStr
    End Function
    Dim TNet As Decimal
    Dim str_mark As String

    'by rut Title New Begin---------------
    Dim Str_TitleDe As String
    Dim Str_TitleDe2 As String
    'by rut Title New End-----------------
    Private Sub Detail1_Format(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Detail1.Format
        Cpage += 1
        TNet += txtnet_weight.Value

        Count_RowToPage_DetailData()

        txtNumRowCount.Text = Cpage


        If Cpage = 1 Then
            str_mark = CommonUtility.Get_StringValue(txtmarks.Text)
        End If


        'by rut Title New Begin--------------------------------------------------
        If txtTitleMain.Text <> "" Then
            Str_TitleDe = txtTitleMain.Text
        Else
            Str_TitleDe2 = txtTitleMain.Text
        End If

        If Str_TitleDe = Str_TitleDe2 Then
            Str_TitlePage = Str_TitlePage
        Else
            Str_TitlePage = Str_TitleDe
        End If
        'by rut Title New End--------------------------------------------------
    End Sub

    Private Sub rpt3_ediFORM4_5_pr_PageEnd(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PageEnd
        head_height = PageHeader1.Height

        Dim f As New System.Drawing.Font("BrowalliaUPC", 11)

        Dim xm As Single = 6.91 '7
        Dim ym As Single = CSng(head_height) '4.927
        'Dim width As Single = 6.0F
        Dim widthm As Single = 0.96 '0.91 '0.81
        Dim heightm As Single = 4
        Dim drawRectm As New Drawing.RectangleF(xm, ym, widthm, heightm)

        With Me.CurrentPage
            .Font = f
            .ForeColor = System.Drawing.Color.Black
            .TextAlignment = DataDynamics.ActiveReports.TextAlignment.Center
            .VerticalTextAlignment = VerticalTextAlignment.Top
            '.BackColor = Drawing.Color.Green
            '.TextAngle = 900
            '.VerticalTextAlignment = DataDynamics.ActiveReports.VerticalTextAlignment.Middle
            .DrawText(CallInvoiceCheckF, drawRectm)
        End With
        '============================
        Dim x_txtGrossTxt As Single = 5.98 '6 '6.25
        Dim y_txtGrossTxt As Single = CSng(head_height) '4.927
        'Dim width As Single = 6.0F
        Dim width_txtGrossTxt As Single = 0.91 '0.75
        Dim height_txtGrossTxt As Single = 4
        Dim drawRect_txtGrossTxt As New Drawing.RectangleF(x_txtGrossTxt, y_txtGrossTxt, width_txtGrossTxt, height_txtGrossTxt)

        With Me.CurrentPage
            .Font = f
            .ForeColor = System.Drawing.Color.Black
            .TextAlignment = DataDynamics.ActiveReports.TextAlignment.Center
            .VerticalTextAlignment = VerticalTextAlignment.Top
            '.BackColor = Drawing.Color.Green
            '.TextAngle = 900
            '.VerticalTextAlignment = DataDynamics.ActiveReports.VerticalTextAlignment.Middle
            .DrawText(CHeck_DisPlayF, drawRect_txtGrossTxt)
        End With
        '============================
        Dim x_txtmarks As Single = 0.85 '1.25
        Dim y_txtmarks As Single = CSng(head_height) '4.927
        'Dim width As Single = 6.0F
        Dim width_txtmarks As Single = 0.94 '0.88
        Dim height_txtmarks As Single = 4
        Dim drawRect_txtmarks As New Drawing.RectangleF(x_txtmarks, y_txtmarks, width_txtmarks, height_txtmarks)

        With Me.CurrentPage
            .Font = f
            .ForeColor = System.Drawing.Color.Black
            .TextAlignment = DataDynamics.ActiveReports.TextAlignment.Left
            .VerticalTextAlignment = VerticalTextAlignment.Top
            '.BackColor = Drawing.Color.Green
            '.TextAngle = 900
            '.VerticalTextAlignment = DataDynamics.ActiveReports.VerticalTextAlignment.Middle
            .DrawText(str_mark, drawRect_txtmarks)
        End With
    End Sub

    Private Sub rpt3_ediFORM4_5_pr_ReportEnd(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.ReportEnd
        If updatePrintTotal(txtinvh_run_auto.Value, CStr(Me.Document.Pages.Count)) = True Then

        End If
    End Sub

    Private Sub rpt3_ediFORM4_5_pr_ReportStart(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.ReportStart
        'Me.ReportInfo1.FormatString = "Page {PageNumber} of {PageCount} on {RunDateTime}"
        Me.ReportInfo1.FormatString = "Page : {PageNumber} of {PageCount}"

    End Sub

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        Me.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.Custom
        Me.PageSettings.PaperName = "Fanfold 210 x 305 mm"
        ' Add any initialization after the InitializeComponent() call.

    End Sub
#Region "Code sign image"
    'by rut sign image ใน report
    'มี reports_CardID,search_imageForm อยู่ใน Module_CallBathEng
    Sub CaseCheck_numimagesSign(ByVal _numValue As DataSet)
        Dim arr_ As Array

        Dim num_return As Integer = 0
        If _numValue.Tables(0).Rows.Count > 0 Then
            If CommonUtility.Get_StringValue(_numValue.Tables(0).Rows(0).Item("SignImageID").ToString) <> "" Then
                arr_ = _numValue.Tables(0).Rows(0).Item("SignImageID").ToString.Split(";")
            Else
                Exit Sub
            End If
        Else
            Exit Sub
        End If

        If reports_CardID(arr_(0).ToString) <> "" Then
            Select Case arr_.Length - 1
                Case 1
                    Picture_SealAuthor.Visible = True

                    'Picture_SealAuthor.SizeMode = SizeModes.Stretch
                    Dim u As String = reports_CardID(arr_(0).ToString) '"http://edi.dft.go.th/" & "/Portals/0/Images_Sign/0405533000247/2013/SealPerson/3660400121998/3660400121998_New_นพอนันต์ NK1.jpg"
                    Dim client As System.Net.WebClient = New System.Net.WebClient()
                    Dim data As System.IO.Stream = client.OpenRead(u)
                    Dim reader As System.IO.StreamReader = New System.IO.StreamReader(data)

                    Picture_SealAuthor.Image = System.Drawing.Image.FromStream(reader.BaseStream())
            End Select
        End If
    End Sub
    Sub CaseCheck_ApproveSign(ByVal _numValue As DataSet)
        Dim arr_ As Array

        Dim num_return As Integer = 0
        If _numValue.Tables(0).Rows.Count > 0 Then
            With _numValue.Tables(0).Rows(0)
                If CommonUtility.Get_StringValue(.Item("SignImage_ApproveID").ToString) <> "" And CommonUtility.Get_StringValue(.Item("im_CheckCaseSave").ToString) = "1" Then
                    arr_ = .Item("SignImage_ApproveID").ToString.Split(";")
                Else
                    Exit Sub
                End If
            End With
        Else
            Exit Sub
        End If

        PictureApproveSign.Visible = True

        Picture_SealAuthor.SizeMode = SizeModes.Clip
        PictureApproveSign.Image = New Drawing.Bitmap(reports_Approveid(arr_(0).ToString))
        '        txtTemp_SiteSend.Text = "ST-003"
        Select Case txtTemp_SiteSend.Text
            Case "ST-003"
                txtTemp_Date.Visible = True
                txtTemp_Bangkok02.Visible = True
                txtTemp_Bangkok01.Visible = True
                txtTemp_Bangkok01.Text = ""
                txtTemp_Bangkok02.Text = ""

                txtTemp_Bangkok01.Text = String_SiteNameReport02(txtTemp_SiteSend.Text).ToUpper
                txtTemp_Bangkok02.Text = String_SiteNameReport01(txtTemp_SiteSend.Text).ToUpper
            Case Else
                txtTemp_Date.Visible = True
                txtTemp_Bangkok01.Visible = True
                txtTemp_Bangkok02.Visible = False
                txtTemp_Bangkok01.Text = ""
                txtTemp_Bangkok02.Text = ""

                txtTemp_Bangkok01.Text = String_SiteNameReport03(txtTemp_SiteSend.Text).ToUpper
        End Select
        txtTemp_Date.Text = String_DateSiteReport(txtapprove_date.Value)
    End Sub
#End Region
End Class
