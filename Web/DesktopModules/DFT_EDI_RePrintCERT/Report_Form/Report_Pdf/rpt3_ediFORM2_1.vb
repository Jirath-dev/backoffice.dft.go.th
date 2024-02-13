Imports DataDynamics.ActiveReports
Imports DataDynamics.ActiveReports.Document

Imports Microsoft.ApplicationBlocks.Data
Imports System.Data.SqlClient
Imports System.Data
Imports System.Text
Imports DFT.Dotnetnuke.ClassLibrary
Imports ReportPrintClass
Public Class rpt3_ediFORM2_1 

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        Me.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.Custom
        Me.PageSettings.PaperName = "Fanfold 210 x 305 mm"
        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Dim Cpage As Integer
    Private Sub PageHeader1_Format(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PageHeader1.Format
        '==============txttemp_edi_date=================================================
        txttemp_edi_date.Text = CommonUtility.Get_StringValue(txtedi_date.Text)

        '==============txtTemp_company_taxno=================================================
        txtTemp_company_taxno.Value = Mid(txtcompany_taxno.Value, 1, 10)

        '===============================================================
        txtTemp_IMPORT_COUNTRY.Value = txtIMPORT_COUNTRY.value ' txtBANGKOK.Value

        '===============================================================
        txtreference_code2_Temp.Text = "TH/" & CommonUtility.Get_StringValue(txtdestination_country.Text) _
                                        & "/" & Mid(CommonUtility.Get_StringValue(txtreference_code2.Text), 4, 2) _
                                        & "/08/" & Mid(CommonUtility.Get_StringValue(txtreference_code2.Text), 8, 6)


        '===============================================================
        Head_Checkcompany_v1()
        Head_Checkdestination_v2()
    End Sub
    
    Sub Head_Checkcompany_v1()
        If Mid(txtdest_remark.Text, 1, 3) = "C/O" Then
            txtCompany_Check_1.Text = txtob_address.Text & IIf(Check_NULLALL(txtNewEmail_ch01.Text) <> "", " E-mail: " & txtNewEmail_ch01.Text, "") & " CARE OF " & txtcompany_name.Text & " " & txtcompany_address.Text _
                               & " " & txtcompany_province.Text & " " & txtcompany_country.Text & _
                               IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") & _
                               IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") & _
                               IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID:  " & txtcompany_taxno.Text, "") & _
                               IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")
        ElseIf Mid(txtdest_remark.Text, 1, 3) = "O/B" Then
            txtCompany_Check_1.Text = txtcompany_name.Text & " " & txtcompany_address.Text & " " & txtcompany_province.Text _
                                & " " & txtcompany_country.Text & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") & _
                                IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") _
                                & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID: " & txtcompany_taxno.Text, "") & _
                                IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "") & _
                                IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtob_address.Text)) <> "", " ON BEHALF OF " & txtob_address.Text, "") & IIf(Check_NULLALL(txtNewEmail_ch01.Text) <> "", " E-mail: " & txtNewEmail_ch01.Text, "")
        ElseIf Mid(txtdest_remark.Text, 2, 1) <> "/" Then
            txtCompany_Check_1.Text = txtcompany_name.Text & " " & txtcompany_address.Text & " " & txtcompany_province.Text _
                                & " " & txtcompany_country.Text & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") & _
                                IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") _
                                & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID: " & txtcompany_taxno.Text, "") & _
                                IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")
        End If

    End Sub

    Sub Head_Checkdestination_v2()
        If Mid(txtdest_remark1.Text, 1, 3) = "C/O" Then
            txtdestination_Check2.Text = txtOB_Dest_Address.Text & " CARE OF " & txtdestination_company.Text & " " & txtdestination_address.Text _
                                & " " & txtdestination_province.Text & " " & txtdest_Receive_country.Text & _
                                IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") & _
                                IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") & _
                                IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID:  " & txtdestination_taxid.Text, "") & _
                                IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "")

        ElseIf Mid(txtdest_remark1.Text, 1, 3) = "O/B" Then
            txtdestination_Check2.Text = txtdestination_company.Text & " " & txtdestination_address.Text & " " & _
                                    txtdestination_province.Text & " " & txtdest_Receive_country.Text & _
                                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") & _
                                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                    & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") & _
                                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") & _
                                    " ON BEHALF OF " & txtOB_Dest_Address.Text

        ElseIf Mid(txtdest_remark1.Text, 2, 1) <> "/" Then
            txtdestination_Check2.Text = txtdestination_company.Text & " " & txtdestination_address.Text & " " & txtdestination_province.Text _
                                & " " & txtdest_Receive_country.Text & _
                                IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") & _
                                IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") & _
                                IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "")

        End If
    End Sub

    Dim str_ As Double
    Dim str_mark As String

    Dim Sum_net_weightD As Double
    Dim Sum_net_weightDC As Decimal

    Dim All_str As String
    Sub Title_StrDetail()
        All_str = CarTxt(CommonUtility.Get_StringValue(txtSINGLE_COUNTRY_CONTENT.Value), CommonUtility.Get_StringValue(txtInvoiceDetailTH.Value))
    End Sub

    Private Sub Detail1_Format(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Detail1.Format
        Cpage += 1

        If Cpage = 1 Then
            str_mark = CommonUtility.Get_StringValue(txtmarks.Text) & "" & CommonUtility.Get_StringValue(txtmarks1.Text)

        End If

        'txttemp_marks.Text = CommonUtility.Get_StringValue(txtmarks.Text) & " " & CommonUtility.Get_StringValue(txtmarks1.Text)

        Title_StrDetail()

        Select Case CommonUtility.Get_StringValue(txtFOBDisplay.Text)
            Case "0" 'แสดง 6
                txtT_product.Text = All_str & "H.S.CODE " & Mid(CommonUtility.Get_StringValue(txttariff_code.Text), 1, 4) _
                                            & "." & Mid(CommonUtility.Get_StringValue(txttariff_code.Text), 5, 2) _
                                            & vbNewLine & CommonUtility.Get_StringValue(txtproduct_n1.Text) _
                                            & CommonUtility.Get_StringValue(txtproduct_n2.Text) & " ***"
            Case "1" 'แสดง 8
                txtT_product.Text = All_str & "H.S.CODE " & Mid(CommonUtility.Get_StringValue(txttariff_code.Text), 1, 4) _
                                            & "." & Mid(CommonUtility.Get_StringValue(txttariff_code.Text), 5, 4) _
                                            & vbNewLine & CommonUtility.Get_StringValue(txtproduct_n1.Text) _
                                            & CommonUtility.Get_StringValue(txtproduct_n2.Text) & " ***"
            Case Else
                txtT_product.Text = All_str & "H.S.CODE " & Mid(CommonUtility.Get_StringValue(txttariff_code.Text), 1, 4) _
                                                            & "." & Mid(CommonUtility.Get_StringValue(txttariff_code.Text), 5, 2) _
                                                            & vbNewLine & CommonUtility.Get_StringValue(txtproduct_n1.Text) _
                                                            & CommonUtility.Get_StringValue(txtproduct_n2.Text) & " ***"
        End Select


        'Count_RowToPage_DetailData()

        str_ += CDbl(txtfob_amt.Value)


        Select Case CommonUtility.Get_StringValue(txti_unit_code.Value)
            Case "PCS" 'จุดทศนิยมไม่มี
                Sum_net_weightD += CDbl(txtnet_weight_value.Value)
                txtnet_weight.Text = Format(txtnet_weight_value.Value, "#,###")
            Case "SET" 'จุดทศนิยมไม่มี
                Sum_net_weightD += CDbl(txtnet_weight_value.Value)
                txtnet_weight.Text = Format(txtnet_weight_value.Value, "#,###")
            Case "PRS" 'จุดทศนิยมไม่มี
                Sum_net_weightD += CDbl(txtnet_weight_value.Value)
                txtnet_weight.Text = Format(txtnet_weight_value.Value, "#,###")
            Case Else
                Sum_net_weightDC += CDec(txtnet_weight_value.Value)
                txtnet_weight.Text = Format(txtnet_weight_value.Value, "#,###.00##")
        End Select
    End Sub

    Private Sub PageFooter1_Format(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PageFooter1.Format
        txtTemp_AllInvoince.Text = "DETAILS AS PER INVOICE NO " _
                                    & IIf(CommonUtility.Get_StringValue(txtinvoice_no1.Text) <> "", CommonUtility.Get_StringValue(txtinvoice_no1.Text) & " ", "") _
                                    & IIf(CommonUtility.Get_StringValue(txtinvoice_no2.Text) <> "", CommonUtility.Get_StringValue(txtinvoice_no2.Text) & " ", "") _
                                    & IIf(CommonUtility.Get_StringValue(txtinvoice_no3.Text) <> "", CommonUtility.Get_StringValue(txtinvoice_no3.Text) & " ", "") _
                                    & IIf(CommonUtility.Get_StringValue(txtinvoice_no4.Text) <> "", CommonUtility.Get_StringValue(txtinvoice_no4.Text) & " ", "") _
                                    & IIf(CommonUtility.Get_StringValue(txtinvoice_no5.Text) <> "", CommonUtility.Get_StringValue(txtinvoice_no5.Text) & " ", "")
        txtTemp_invoice_date1.Text = txtinvoice_date1.Value

        Select Case txtSendCheckSeletedate.Text
            Case True 'ใช้วันที่เลือกเอง
                txttemp_Today.Value = txtdateSelectRPT.Text
            Case False 'ใช้ วันอนุมัติ
                txttemp_Today.Value = CommonUtility.Get_DateTime(txtapprove_date.Text).ToString("dd/MM/yyyy") 'format_dateSelect
        End Select

        txtTemp_PageFoot_reference_code2.Text = CommonUtility.Get_StringValue(txtreference_code2.Text)

    End Sub

    'Sub Count_RowToPage_DetailData()
    '    Dim CountData_T, CountData_T2 As Integer
    '    CountData_T = C_TotalRowDe.Text
    '    CountData_T2 = CountData_T / 7

    '    Dim i As Integer
    '    txtNumRowCount.Text += 1
    '    For i = 0 To CountData_T2
    '        Dim arrnum(i) As Integer

    '        arrnum(i) = i * 7 'ทำเพื่อเช็คจำนวนเปรียบเทียบ

    '        If (txtNumRowCount.Text = arrnum(i)) = True Then
    '            Me.Detail1.NewPage = NewPage.Before
    '            'txtmarks.Visible = True
    '            'txtgross_weight1.Visible = True
    '            'txtTolInvoice.Visible = True

    '            Exit For
    '        Else
    '            '===============================================================
    '            If CommonUtility.Get_StringValue(txtgross_weight.Text) <> "" Then
    '                txtgross_weight1.Text = txtgross_weight.Text + " " + txtg_unit_code.Text
    '            Else
    '                txtgross_weight1.Text = ""
    '            End If
    '            '============================================================
    '            'CallInvoiceCheck()
    '            '============================================================

    '            'Count_quantity(txtquantity1.Text, txtquantity2.Text, txtquantity3.Text, txtquantity4.Text, txtquantity5.Text)
    '            If txtNumRowCount.Text = C_TotalRowDe.Text Then

    '                If (CommonUtility.Get_StringValue(txtquantity1.Text) <> "" And CommonUtility.Get_StringValue(txtquantity1.Text) <> "0.0000" And CommonUtility.Get_StringValue(txtq_unit_code1.Text) <> "") = True And _
    '                (CommonUtility.Get_StringValue(txtquantity2.Text) <> "" And CommonUtility.Get_StringValue(txtquantity2.Text) <> "0.0000" And CommonUtility.Get_StringValue(txtq_unit_code2.Text) <> "") = True And _
    '                (CommonUtility.Get_StringValue(txtquantity3.Text) <> "" And CommonUtility.Get_StringValue(txtquantity3.Text) <> "0.0000" And CommonUtility.Get_StringValue(txtq_unit_code3.Text) <> "") = True And _
    '                (CommonUtility.Get_StringValue(txtquantity4.Text) <> "" And CommonUtility.Get_StringValue(txtquantity4.Text) <> "0.0000" And CommonUtility.Get_StringValue(txtq_unit_code4.Text) <> "") = True And _
    '                (CommonUtility.Get_StringValue(txtquantity5.Text) <> "" And CommonUtility.Get_StringValue(txtquantity5.Text) <> "0.0000" And CommonUtility.Get_StringValue(txtq_unit_code5.Text) <> "") = True Then
    '                    'มีหมด
    '                    txtT_product.Text = "H.S.CODE " & Mid(txttariff_code.Value, 1, 4) & "." & Mid(txttariff_code.Value, 5, 2) & vbNewLine & txtproduct_n1.Text + txtproduct_n2.Text + " ****"

    '                ElseIf (CommonUtility.Get_StringValue(txtquantity1.Text) <> "" And CommonUtility.Get_StringValue(txtquantity1.Text) <> "0.0000" And CommonUtility.Get_StringValue(txtq_unit_code1.Text) <> "") = True And _
    '                (CommonUtility.Get_StringValue(txtquantity2.Text) <> "" And CommonUtility.Get_StringValue(txtquantity2.Text) <> "0.0000" And CommonUtility.Get_StringValue(txtq_unit_code2.Text) <> "") = True And _
    '                (CommonUtility.Get_StringValue(txtquantity3.Text) <> "" And CommonUtility.Get_StringValue(txtquantity3.Text) <> "0.0000" And CommonUtility.Get_StringValue(txtq_unit_code3.Text) <> "") = True And _
    '                (CommonUtility.Get_StringValue(txtquantity4.Text) <> "" And CommonUtility.Get_StringValue(txtquantity4.Text) <> "0.0000" And CommonUtility.Get_StringValue(txtq_unit_code4.Text) <> "") = False And _
    '                (CommonUtility.Get_StringValue(txtquantity5.Text) <> "" And CommonUtility.Get_StringValue(txtquantity5.Text) <> "0.0000" And CommonUtility.Get_StringValue(txtq_unit_code5.Text) <> "") = True Then
    '                    'ขาด 4
    '                    txtT_product.Text = "H.S.CODE " & Mid(txttariff_code.Value, 1, 4) & "." & Mid(txttariff_code.Value, 5, 2) & vbNewLine & txtproduct_n1.Text + txtproduct_n2.Text + " ****"

    '                ElseIf (CommonUtility.Get_StringValue(txtquantity1.Text) <> "" And CommonUtility.Get_StringValue(txtquantity1.Text) <> "0.0000" And CommonUtility.Get_StringValue(txtq_unit_code1.Text) <> "") = True And _
    '                 (CommonUtility.Get_StringValue(txtquantity2.Text) <> "" And CommonUtility.Get_StringValue(txtquantity2.Text) <> "0.0000" And CommonUtility.Get_StringValue(txtq_unit_code2.Text) <> "") = True And _
    '                 (CommonUtility.Get_StringValue(txtquantity3.Text) <> "" And CommonUtility.Get_StringValue(txtquantity3.Text) <> "0.0000" And CommonUtility.Get_StringValue(txtq_unit_code3.Text) <> "") = False And _
    '                 (CommonUtility.Get_StringValue(txtquantity4.Text) <> "" And CommonUtility.Get_StringValue(txtquantity4.Text) <> "0.0000" And CommonUtility.Get_StringValue(txtq_unit_code4.Text) <> "") = False And _
    '                 (CommonUtility.Get_StringValue(txtquantity5.Text) <> "" And CommonUtility.Get_StringValue(txtquantity5.Text) <> "0.0000" And CommonUtility.Get_StringValue(txtq_unit_code5.Text) <> "") = True Then
    '                    'ขาด 3,4
    '                    txtT_product.Text = "H.S.CODE " & Mid(txttariff_code.Value, 1, 4) & "." & Mid(txttariff_code.Value, 5, 2) & vbNewLine & txtproduct_n1.Text + txtproduct_n2.Text + " ****"

    '                ElseIf (CommonUtility.Get_StringValue(txtquantity1.Text) <> "" And CommonUtility.Get_StringValue(txtquantity1.Text) <> "0.0000" And CommonUtility.Get_StringValue(txtq_unit_code1.Text) <> "") = True And _
    '                  (CommonUtility.Get_StringValue(txtquantity2.Text) <> "" And CommonUtility.Get_StringValue(txtquantity2.Text) <> "0.0000" And CommonUtility.Get_StringValue(txtq_unit_code2.Text) <> "") = False And _
    '                  (CommonUtility.Get_StringValue(txtquantity3.Text) <> "" And CommonUtility.Get_StringValue(txtquantity3.Text) <> "0.0000" And CommonUtility.Get_StringValue(txtq_unit_code3.Text) <> "") = False And _
    '                  (CommonUtility.Get_StringValue(txtquantity4.Text) <> "" And CommonUtility.Get_StringValue(txtquantity4.Text) <> "0.0000" And CommonUtility.Get_StringValue(txtq_unit_code4.Text) <> "") = False And _
    '                  (CommonUtility.Get_StringValue(txtquantity5.Text) <> "" And CommonUtility.Get_StringValue(txtquantity5.Text) <> "0.0000" And CommonUtility.Get_StringValue(txtq_unit_code5.Text) <> "") = True Then
    '                    'ขาด 2,3,4
    '                    txtT_product.Text = "H.S.CODE " & Mid(txttariff_code.Value, 1, 4) & "." & Mid(txttariff_code.Value, 5, 2) & vbNewLine & txtproduct_n1.Text + txtproduct_n2.Text + " ****"

    '                ElseIf (CommonUtility.Get_StringValue(txtquantity1.Text) <> "" And CommonUtility.Get_StringValue(txtquantity1.Text) <> "0.0000" And CommonUtility.Get_StringValue(txtq_unit_code1.Text) <> "") = True And _
    '                  (CommonUtility.Get_StringValue(txtquantity2.Text) <> "" And CommonUtility.Get_StringValue(txtquantity2.Text) <> "0.0000" And CommonUtility.Get_StringValue(txtq_unit_code2.Text) <> "") = False And _
    '                  (CommonUtility.Get_StringValue(txtquantity3.Text) <> "" And CommonUtility.Get_StringValue(txtquantity3.Text) <> "0.0000" And CommonUtility.Get_StringValue(txtq_unit_code3.Text) <> "") = False And _
    '                  (CommonUtility.Get_StringValue(txtquantity4.Text) <> "" And CommonUtility.Get_StringValue(txtquantity4.Text) <> "0.0000" And CommonUtility.Get_StringValue(txtq_unit_code4.Text) <> "") = False And _
    '                  (CommonUtility.Get_StringValue(txtquantity5.Text) <> "" And CommonUtility.Get_StringValue(txtquantity5.Text) <> "0.0000" And CommonUtility.Get_StringValue(txtq_unit_code5.Text) <> "") = False Then
    '                    'ขาด 2,3,4,5
    '                    txtT_product.Text = "H.S.CODE " & Mid(txttariff_code.Value, 1, 4) & "." & Mid(txttariff_code.Value, 5, 2) & vbNewLine & txtproduct_n1.Text + txtproduct_n2.Text + " ****"

    '                End If

    '            Else
    '                txtT_product.Text = txtproduct_n1.Text + txtproduct_n2.Text + " ****"
    '            End If

    '            '============================================================
    '            If CommonUtility.Get_StringValue(txtNumRowCount.Text) = "1" Then
    '                txtTemp_marks.Visible = True
    '                txtgross_weight1.Visible = True
    '                txtTolInvoice.Visible = True
    '            Else
    '                txtTemp_marks.Visible = False
    '                txtgross_weight1.Visible = False
    '                txtTolInvoice.Visible = False
    '            End If

    '            Me.Detail1.NewPage = NewPage.None
    '        End If

    '    Next


    'End Sub

    'Sub Count_RowToPage_DetailData()
    '    Dim CountData_T, CountData_T2 As Integer
    '    CountData_T = C_TotalRowDe.Text
    '    CountData_T2 = CountData_T / 7

    '    Dim i As Integer
    '    txtNumRowCount.Text += 1
    '    For i = 0 To CountData_T2
    '        Dim arrnum(i) As Integer

    '        arrnum(i) = i * 7 'ทำเพื่อเช็คจำนวนเปรียบเทียบ

    '        If (txtNumRowCount.Text = arrnum(i)) = True Then
    '            Me.Detail1.NewPage = NewPage.Before
    '            'txtmarks.Visible = True
    '            'txtgross_weight1.Visible = True
    '            'txtTolInvoice.Visible = True

    '            Exit For
    '        Else
    '            '===============================================================
    '            If CommonUtility.Get_StringValue(txtgross_weight.Text) <> "" Then
    '                txtgross_weight1.Text = txtgross_weight.Text + " " + txtg_unit_code.Text
    '            Else
    '                txtgross_weight1.Text = ""
    '            End If
    '            '============================================================
    '            'CallInvoiceCheck()
    '            '============================================================

    '            If txtNumRowCount.Text = C_TotalRowDe.Text Then

    '                If (CommonUtility.Get_StringValue(txtquantity1.Text) <> "" And CommonUtility.Get_StringValue(txtquantity1.Text) <> "0.0000" And CommonUtility.Get_StringValue(txtq_unit_code1.Text) <> "") = True And _
    '                (CommonUtility.Get_StringValue(txtquantity2.Text) <> "" And CommonUtility.Get_StringValue(txtquantity2.Text) <> "0.0000" And CommonUtility.Get_StringValue(txtq_unit_code2.Text) <> "") = True And _
    '                (CommonUtility.Get_StringValue(txtquantity3.Text) <> "" And CommonUtility.Get_StringValue(txtquantity3.Text) <> "0.0000" And CommonUtility.Get_StringValue(txtq_unit_code3.Text) <> "") = True And _
    '                (CommonUtility.Get_StringValue(txtquantity4.Text) <> "" And CommonUtility.Get_StringValue(txtquantity4.Text) <> "0.0000" And CommonUtility.Get_StringValue(txtq_unit_code4.Text) <> "") = True And _
    '                (CommonUtility.Get_StringValue(txtquantity5.Text) <> "" And CommonUtility.Get_StringValue(txtquantity5.Text) <> "0.0000" And CommonUtility.Get_StringValue(txtq_unit_code5.Text) <> "") = True Then
    '                    'มีหมด
    '                    txtT_product.Text = txtproduct_n1.Text + txtproduct_n2.Text + " ****" + vbCrLf + _
    '                    "TOTAL: " + BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) + vbCrLf + "        " + _
    '                    BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) + vbCrLf + "        " + _
    '                    BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) + vbCrLf + "        " + _
    '                    BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) + vbCrLf + "        " + _
    '                    BahtOnly(txtquantity5.Text, txtq_unit_code5.Text)

    '                ElseIf (CommonUtility.Get_StringValue(txtquantity1.Text) <> "" And CommonUtility.Get_StringValue(txtquantity1.Text) <> "0.0000" And CommonUtility.Get_StringValue(txtq_unit_code1.Text) <> "") = True And _
    '                (CommonUtility.Get_StringValue(txtquantity2.Text) <> "" And CommonUtility.Get_StringValue(txtquantity2.Text) <> "0.0000" And CommonUtility.Get_StringValue(txtq_unit_code2.Text) <> "") = True And _
    '                (CommonUtility.Get_StringValue(txtquantity3.Text) <> "" And CommonUtility.Get_StringValue(txtquantity3.Text) <> "0.0000" And CommonUtility.Get_StringValue(txtq_unit_code3.Text) <> "") = True And _
    '                (CommonUtility.Get_StringValue(txtquantity4.Text) <> "" And CommonUtility.Get_StringValue(txtquantity4.Text) <> "0.0000" And CommonUtility.Get_StringValue(txtq_unit_code4.Text) <> "") = True And _
    '                (CommonUtility.Get_StringValue(txtquantity5.Text) <> "" And CommonUtility.Get_StringValue(txtquantity5.Text) <> "0.0000" And CommonUtility.Get_StringValue(txtq_unit_code5.Text) <> "") = False Then
    '                    'ขาด 5
    '                    txtT_product.Text = txtproduct_n1.Text + txtproduct_n2.Text + " ****" + vbCrLf + _
    '                    "TOTAL: " + BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) + vbCrLf + "        " + _
    '                    BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) + vbCrLf + "        " + _
    '                    BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) + vbCrLf + "        " + _
    '                    BahtOnly(txtquantity4.Text, txtq_unit_code4.Text)

    '                ElseIf (CommonUtility.Get_StringValue(txtquantity1.Text) <> "" And CommonUtility.Get_StringValue(txtquantity1.Text) <> "0.0000" And CommonUtility.Get_StringValue(txtq_unit_code1.Text) <> "") = True And _
    '                 (CommonUtility.Get_StringValue(txtquantity2.Text) <> "" And CommonUtility.Get_StringValue(txtquantity2.Text) <> "0.0000" And CommonUtility.Get_StringValue(txtq_unit_code2.Text) <> "") = True And _
    '                 (CommonUtility.Get_StringValue(txtquantity3.Text) <> "" And CommonUtility.Get_StringValue(txtquantity3.Text) <> "0.0000" And CommonUtility.Get_StringValue(txtq_unit_code3.Text) <> "") = True And _
    '                 (CommonUtility.Get_StringValue(txtquantity4.Text) <> "" And CommonUtility.Get_StringValue(txtquantity4.Text) <> "0.0000" And CommonUtility.Get_StringValue(txtq_unit_code4.Text) <> "") = False And _
    '                 (CommonUtility.Get_StringValue(txtquantity5.Text) <> "" And CommonUtility.Get_StringValue(txtquantity5.Text) <> "0.0000" And CommonUtility.Get_StringValue(txtq_unit_code5.Text) <> "") = False Then
    '                    'ขาด 4,5
    '                    txtT_product.Text = txtproduct_n1.Text + txtproduct_n2.Text + " ****" + vbCrLf + _
    '                    "TOTAL: " + BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) + vbCrLf + "        " + _
    '                    BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) + vbCrLf + "        " + _
    '                    BahtOnly(txtquantity3.Text, txtq_unit_code3.Text)

    '                ElseIf (CommonUtility.Get_StringValue(txtquantity1.Text) <> "" And CommonUtility.Get_StringValue(txtquantity1.Text) <> "0.0000" And CommonUtility.Get_StringValue(txtq_unit_code1.Text) <> "") = True And _
    '                  (CommonUtility.Get_StringValue(txtquantity2.Text) <> "" And CommonUtility.Get_StringValue(txtquantity2.Text) <> "0.0000" And CommonUtility.Get_StringValue(txtq_unit_code2.Text) <> "") = True And _
    '                  (CommonUtility.Get_StringValue(txtquantity3.Text) <> "" And CommonUtility.Get_StringValue(txtquantity3.Text) <> "0.0000" And CommonUtility.Get_StringValue(txtq_unit_code3.Text) <> "") = False And _
    '                  (CommonUtility.Get_StringValue(txtquantity4.Text) <> "" And CommonUtility.Get_StringValue(txtquantity4.Text) <> "0.0000" And CommonUtility.Get_StringValue(txtq_unit_code4.Text) <> "") = False And _
    '                  (CommonUtility.Get_StringValue(txtquantity5.Text) <> "" And CommonUtility.Get_StringValue(txtquantity5.Text) <> "0.0000" And CommonUtility.Get_StringValue(txtq_unit_code5.Text) <> "") = False Then
    '                    'ขาด 3,4,5
    '                    txtT_product.Text = txtproduct_n1.Text + txtproduct_n2.Text + " ****" + vbCrLf + _
    '                    "TOTAL: " + BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) + vbCrLf + "        " + _
    '                    BahtOnly(txtquantity2.Text, txtq_unit_code2.Text)

    '                ElseIf (CommonUtility.Get_StringValue(txtquantity1.Text) <> "" And CommonUtility.Get_StringValue(txtquantity1.Text) <> "0.0000" And CommonUtility.Get_StringValue(txtq_unit_code1.Text) <> "") = True And _
    '                  (CommonUtility.Get_StringValue(txtquantity2.Text) <> "" And CommonUtility.Get_StringValue(txtquantity2.Text) <> "0.0000" And CommonUtility.Get_StringValue(txtq_unit_code2.Text) <> "") = False And _
    '                  (CommonUtility.Get_StringValue(txtquantity3.Text) <> "" And CommonUtility.Get_StringValue(txtquantity3.Text) <> "0.0000" And CommonUtility.Get_StringValue(txtq_unit_code3.Text) <> "") = False And _
    '                  (CommonUtility.Get_StringValue(txtquantity4.Text) <> "" And CommonUtility.Get_StringValue(txtquantity4.Text) <> "0.0000" And CommonUtility.Get_StringValue(txtq_unit_code4.Text) <> "") = False And _
    '                  (CommonUtility.Get_StringValue(txtquantity5.Text) <> "" And CommonUtility.Get_StringValue(txtquantity5.Text) <> "0.0000" And CommonUtility.Get_StringValue(txtq_unit_code5.Text) <> "") = False Then
    '                    'ขาด 2,3,4,5
    '                    txtT_product.Text = txtproduct_n1.Text + txtproduct_n2.Text + " ****" + vbCrLf + _
    '                    "TOTAL: " + BahtOnly(txtquantity1.Text, txtq_unit_code1.Text)

    '                End If

    '            Else
    '                txtT_product.Text = txtproduct_n1.Text + txtproduct_n2.Text + " ****"
    '            End If

    '            '============================================================
    '            If CommonUtility.Get_StringValue(txtNumRowCount.Text) = "1" Then
    '                txtTemp_marks.Visible = True
    '                txtgross_weight1.Visible = True
    '                txtTolInvoice.Visible = True
    '            Else
    '                txtTemp_marks.Visible = False
    '                txtgross_weight1.Visible = False
    '                txtTolInvoice.Visible = False
    '            End If

    '            Me.Detail1.NewPage = NewPage.None
    '        End If

    '    Next


    'End Sub
    'Function Count_quantity(ByVal _quantity1 As String, ByVal _quantity2 As String, ByVal _quantity3 As String, ByVal _quantity4 As String, ByVal _quantity5 As String) As Array
    '    Dim Check_1 As String
    '    Dim Check_2 As String
    '    Dim Check_3 As String
    '    Dim Check_4 As String
    '    Dim Check_5 As String
    '    Dim i As Integer
    '    'For i = 0 To 5 - 1
    '    If (CommonUtility.Get_StringValue(_quantity1) <> "" And CommonUtility.Get_StringValue(_quantity1) <> "0.0000" And CommonUtility.Get_StringValue(_quantity1) <> "") = True Then
    '        Check_1 = "1"
    '    End If
    '    If (CommonUtility.Get_StringValue(_quantity2) <> "" And CommonUtility.Get_StringValue(_quantity2) <> "0.0000" And CommonUtility.Get_StringValue(_quantity2) <> "") = True Then
    '        Check_2 = "2"
    '    End If
    '    If (CommonUtility.Get_StringValue(_quantity3) <> "" And CommonUtility.Get_StringValue(_quantity3) <> "0.0000" And CommonUtility.Get_StringValue(_quantity3) <> "") = True Then
    '        Check_3 = "3"
    '    End If
    '    If (CommonUtility.Get_StringValue(_quantity4) <> "" And CommonUtility.Get_StringValue(_quantity4) <> "0.0000" And CommonUtility.Get_StringValue(_quantity4) <> "") = True Then
    '        Check_4 = "4"
    '    End If
    '    If (CommonUtility.Get_StringValue(_quantity5) <> "" And CommonUtility.Get_StringValue(_quantity5) <> "0.0000" And CommonUtility.Get_StringValue(_quantity5) <> "") = True Then
    '        Check_5 = "5"
    '    End If
    '    'Next


    '    Dim _strArr(4) As String
    '    _strArr(0) = Check_1
    '    _strArr(1) = Check_2
    '    _strArr(2) = Check_3
    '    _strArr(3) = Check_4
    '    _strArr(4) = Check_5

    '    Return _strArr
    'End Function
    Function Count_quantity(ByVal _quantity1 As String, ByVal _quantity2 As String, ByVal _quantity3 As String, ByVal _quantity4 As String, ByVal _quantity5 As String) As Array
        Dim Check_1 As String
        Dim Check_2 As String
        Dim Check_3 As String
        Dim Check_4 As String
        Dim Check_5 As String
        Dim i As Integer
        'For i = 0 To 5 - 1
        If (CommonUtility.Get_StringValue(_quantity1) <> "" And CommonUtility.Get_Decimal(_quantity1) <> 0 And CommonUtility.Get_StringValue(_quantity1) <> "") = True Then
            Check_1 = "1"
        End If
        If (CommonUtility.Get_StringValue(_quantity2) <> "" And CommonUtility.Get_Decimal(_quantity2) <> 0 And CommonUtility.Get_StringValue(_quantity2) <> "") = True Then
            Check_2 = "2"
        End If
        If (CommonUtility.Get_StringValue(_quantity3) <> "" And CommonUtility.Get_Decimal(_quantity3) <> 0 And CommonUtility.Get_StringValue(_quantity3) <> "") = True Then
            Check_3 = "3"
        End If
        If (CommonUtility.Get_StringValue(_quantity4) <> "" And CommonUtility.Get_Decimal(_quantity4) <> 0 And CommonUtility.Get_StringValue(_quantity4) <> "") = True Then
            Check_4 = "4"
        End If
        If (CommonUtility.Get_StringValue(_quantity5) <> "" And CommonUtility.Get_Decimal(_quantity5) <> 0 And CommonUtility.Get_StringValue(_quantity5) <> "") = True Then
            Check_5 = "5"
        End If
        'Next


        Dim _strArr(4) As String
        _strArr(0) = Check_1
        _strArr(1) = Check_2
        _strArr(2) = Check_3
        _strArr(3) = Check_4
        _strArr(4) = Check_5

        Return _strArr
    End Function
    Function select_q(ByVal _arr As Array) As String
        Dim T_test As String
        Dim i_select As Integer
        For i_select = 0 To 5 - 1
            Select Case _arr(i_select)
                Case "1"
                Case "2"
                Case "3"
                Case "4"
                Case "5"

            End Select
        Next
        Return T_test
    End Function
    Private Sub GroupFooter1_Format(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GroupFooter1.Format
        Dim Temp_strFooter As String
        Temp_strFooter = "TOTAL :" & BahtOnly(Format(CommonUtility.Get_Decimal(txtTotal_net_weight.Text), "#,##0.00##"), txtg_unit_code.Value) & vbNewLine & _
                "     " & BahtOnly(Format(str_, "#,##0.00##"), txtcurrency_code.Value) & _
                IIf(CommonUtility.Get_Decimal(txtquantity5.Text) <> 0 And CommonUtility.Get_StringValue(txtquantity5.Text) <> "", vbNewLine & "     " & BahtOnly(Format(CommonUtility.Get_Decimal(txtquantity5.Text), "#,##0.00##"), txtq_unit_code5.Value), "") & _
                IIf(CommonUtility.Get_Decimal(txtquantity2.Text) <> 0 And CommonUtility.Get_StringValue(txtquantity2.Text) <> "", vbNewLine & "     " & BahtOnly(Format(CommonUtility.Get_Decimal(txtquantity2.Text), "#,##0.00##"), txtq_unit_code2.Value), "") & _
                IIf(CommonUtility.Get_Decimal(txtquantity3.Text) <> 0 And CommonUtility.Get_StringValue(txtquantity3.Text) <> "", vbNewLine & "     " & BahtOnly(Format(CommonUtility.Get_Decimal(txtquantity3.Text), "#,##0.00##"), txtq_unit_code3.Value), "") & _
                IIf(CommonUtility.Get_Decimal(txtquantity4.Text) <> 0 And CommonUtility.Get_StringValue(txtquantity4.Text) <> "", vbNewLine & "     " & BahtOnly(Format(CommonUtility.Get_Decimal(txtquantity4.Text), "#,##0.00##"), txtq_unit_code4.Value), "")

        'Temp_strFooter = IIf(CommonUtility.Get_Decimal(txtquantity4.Text) <> 0 And CommonUtility.Get_StringValue(txtquantity4.Text) <> "", BahtOnly(Format(CommonUtility.Get_Decimal(txtquantity4.Text), "#,##0.00##"), txtq_unit_code4.Value), "2")

        'Temp_strFooter = "TOTAL :" & BahtOnly(Format(txtTotal_net_weight.Value, "#,##0.00##"), txtg_unit_code.Value) & vbNewLine & _
        '        "     " & BahtOnly(Format(str_, "#,##0.00##"), txtcurrency_code.Value) & _
        '        IIf(CommonUtility.Get_Decimal(txtquantity5.Value) <> 0, vbNewLine & "     " & BahtOnly(Format(CommonUtility.Get_StringValue(txtquantity5.Value), "#,##0.00##"), txtq_unit_code5.Value), "") & _
        '        IIf(CommonUtility.Get_Decimal(txtquantity2.Value) <> 0, vbNewLine & "     " & BahtOnly(Format(CommonUtility.Get_StringValue(txtquantity2.Value), "#,##0.00##"), txtq_unit_code2.Value), "") & _
        '        IIf(CommonUtility.Get_Decimal(txtquantity3.Value) <> 0, vbNewLine & "     " & BahtOnly(Format(CommonUtility.Get_StringValue(txtquantity3.Value), "#,##0.00##"), txtq_unit_code3.Value), "") & _
        '        IIf(CommonUtility.Get_Decimal(txtquantity4.Value) <> 0, vbNewLine & "     " & BahtOnly(Format(CommonUtility.Get_StringValue(txtquantity4.Value), "#,##0.00##"), txtq_unit_code4.Value), "")


        'txtTemp_Total.Text = "TOTAL :" & BahtOnly(txtTotal_net_weight.Value, txtg_unit_code.Value) & vbNewLine & _
        'BahtOnly(str_, txtcurrency_code.Value) & _
        'IIf(txtquantity5.Value <> 0, vbNewLine & BahtOnly(txtquantity5.Value, txtq_unit_code5.Value), "") & _
        'IIf(txtquantity2.Value <> 0, vbNewLine & BahtOnly(txtquantity2.Value, txtq_unit_code2.Value), "") & _
        'IIf(txtquantity3.Value <> 0, vbNewLine & BahtOnly(txtquantity3.Value, txtq_unit_code3.Value), "") & _
        'IIf(txtquantity4.Value <> 0, vbNewLine & BahtOnly(txtquantity4.Value, txtq_unit_code4.Value), "")

        txtTemp_Total.Text = Temp_strFooter & vbNewLine & "____________________________________"

        Select Case CommonUtility.Get_StringValue(txti_unit_code.Value)
            Case "PCS" 'จุดทศนิยมไม่มี
                txtSum_net_weight.Text = Format(Sum_net_weightD, "#,###")
            Case "SET" 'จุดทศนิยมไม่มี
                txtSum_net_weight.Text = Format(Sum_net_weightD, "#,###")
            Case "PRS" 'จุดทศนิยมไม่มี
                txtSum_net_weight.Text = Format(Sum_net_weightD, "#,###")
            Case Else
                txtSum_net_weight.Text = Format(Sum_net_weightDC, "#,##0.00###")
        End Select
    End Sub

    Private Sub rpt3_ediFORM2_1_PageEnd(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PageEnd
        Dim f As New System.Drawing.Font("BrowalliaUPC", 12)
        Dim x_txtmarks As Single = 0.56
        Dim y_txtmarks As Single = 4.99
        'Dim width As Single = 6.0F
        Dim width_txtmarks As Single = 1.63
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

    Private Sub rpt3_ediFORM2_1_ReportEnd(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.ReportEnd
        If updatePrintTotal(txtinvh_run_auto.Value, CStr(Me.Document.Pages.Count)) = True Then

        End If
    End Sub

    Private Sub rpt3_ediFORM2_1_ReportStart(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.ReportStart
        'Me.ReportInfo1.FormatString = "Page {PageNumber} of {PageCount} on {RunDateTime}"
        Me.ReportInfo1.FormatString = "Page : {PageNumber} of {PageCount}"
    End Sub
End Class
