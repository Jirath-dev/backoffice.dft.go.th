Imports DataDynamics.ActiveReports
Imports DataDynamics.ActiveReports.Document

Imports Microsoft.ApplicationBlocks.Data
Imports System.Data.SqlClient
Imports System.Data
Imports System.Text
Imports DFT.Dotnetnuke.ClassLibrary
Imports ReportPrintClass
Public Class rpt3_ediFORM1RU 
    Dim Cpage As Integer
    Dim CpageNum As Integer
    'Public Sub New()

    '    ' This call is required by the Windows Form Designer.
    '    InitializeComponent()
    '    Me.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.Custom
    '    Me.PageSettings.PaperName = "Fanfold 210 x 305 mm"
    '    ' Add any initialization after the InitializeComponent() call.

    'End Sub
   
    

    Sub check8()
        'check เรื่องจำนวน ทศนิยม
        Dim regex As New System.Text.RegularExpressions.Regex("^("")(.*)("")(.*)(%)$")

        Dim match As System.Text.RegularExpressions.Match
        Dim i As Integer
        Dim arr(5) As String

        If regex.IsMatch(txtbox8.Text) Then
            match = regex.Match(txtbox8.Text)
            For i = 0 To match.Groups.Count - 1
                arr(i) = match.Groups(i).Value
            Next
        End If
        'txtdigit1
        Select Case CommonUtility.Get_StringValue(txtdigit2.Text)
            Case "2"
                txtTemp_box8.Text = """" + arr(2) + """" + Format(CDec(arr(4)), "0.00") + "%"
            Case "3"
                txtTemp_box8.Text = """" + arr(2) + """" + Format(CDec(arr(4)), "0.000") + "%"
            Case "4"
                txtTemp_box8.Text = """" + arr(2) + """" + Format(CDec(arr(4)), "0.0000") + "%"
        End Select
    End Sub

    'company
    Sub Head_Checkcompany_v1()
        If Mid(txtdest_remark.Text, 1, 3) = "C/O" Then
            txtCompany_Check_1.Text = txtob_address.Text & IIf(txtNewEmail_ch01.Text <> "", " E-mail: " & txtNewEmail_ch01.Text, "") & " CARE OF " & txtcompany_name.Text & " " & txtcompany_address.Text _
                               & " " & txtcompany_province.Text & " " & txtcompany_country.Text & _
                               IIf(CommonUtility.Get_StringValue(txtcompany_phone.Text) <> "", " TEL: " & txtcompany_phone.Text, "") & _
                               IIf(CommonUtility.Get_StringValue(txtcompany_fax.Text) <> "", " FAX: " & txtcompany_fax.Text, "") & _
                               IIf(CommonUtility.Get_StringValue(txtcompany_taxno.Text) <> "", " TAX ID:  " & txtcompany_taxno.Text, "") & _
                               IIf(CommonUtility.Get_StringValue(txtcompany_email.Text) <> "", " E-mail: " & txtcompany_email.Text, "")
        ElseIf Mid(txtdest_remark.Text, 1, 3) = "O/B" Then
            txtCompany_Check_1.Text = txtcompany_name.Text & " " & txtcompany_address.Text & " " & txtcompany_province.Text _
                                & " " & txtcompany_country.Text & IIf(CommonUtility.Get_StringValue(txtcompany_phone.Text) <> "", " TEL: " & txtcompany_phone.Text, "") & _
                                IIf(CommonUtility.Get_StringValue(txtcompany_fax.Text) <> "", " FAX: " & txtcompany_fax.Text, "") _
                                & IIf(CommonUtility.Get_StringValue(txtcompany_taxno.Text) <> "", " TAX ID: " & txtcompany_taxno.Text, "") & _
                                IIf(CommonUtility.Get_StringValue(txtcompany_email.Text) <> "", " E-mail: " & txtcompany_email.Text, "") & _
                                IIf(CommonUtility.Get_StringValue(txtob_address.Text) <> "", " ON BEHALF OF " & txtob_address.Text, "") & IIf(txtNewEmail_ch01.Text <> "", " E-mail: " & txtNewEmail_ch01.Text, "")
        ElseIf Mid(txtdest_remark.Text, 2, 1) <> "/" Then
            txtCompany_Check_1.Text = txtcompany_name.Text & " " & txtcompany_address.Text & " " & txtcompany_province.Text _
                                & " " & txtcompany_country.Text & IIf(CommonUtility.Get_StringValue(txtcompany_phone.Text) <> "", " TEL: " & txtcompany_phone.Text, "") & _
                                IIf(CommonUtility.Get_StringValue(txtcompany_fax.Text) <> "", " FAX: " & txtcompany_fax.Text, "") _
                                & IIf(CommonUtility.Get_StringValue(txtcompany_taxno.Text) <> "", " TAX ID: " & txtcompany_taxno.Text, "") & _
                                IIf(CommonUtility.Get_StringValue(txtcompany_email.Text) <> "", " E-mail: " & txtcompany_email.Text, "")

        End If
    End Sub
    Sub Head_Checkdestination_v2()
        If Mid(txtdest_remark1.Text, 1, 3) = "C/O" Then
            txtdestination_Check2.Text = txtob_dest_address.Text & IIf(txtNewEmail_ch02.Text <> "", " E-mail: " & txtNewEmail_ch02.Text, "") & " CARE OF " & txtdestination_company.Text & " " & txtdestination_address.Text _
                                & " " & txtdestination_province.Text & " " & txtdest_Receive_country.Text & _
                                IIf(CommonUtility.Get_StringValue(txtdestination_phone.Text) <> "", " TEL: " & txtdestination_phone.Text, "") & _
                                IIf(CommonUtility.Get_StringValue(txtdestination_fax.Text) <> "", " FAX: " & txtdestination_fax.Text, "") & _
                                IIf(CommonUtility.Get_StringValue(txtdestination_taxid.Text) <> "", " TAX ID:  " & txtdestination_taxid.Text, "") & _
                                IIf(CommonUtility.Get_StringValue(txtdestination_email.Text) <> "", " E-mail: " & txtdestination_email.Text, "") & _
                                IIf(txtplace_exibition.Text <> "", " " & txtplace_exibition.Text, "")

        ElseIf Mid(txtdest_remark1.Text, 1, 3) = "O/B" Then
            txtdestination_Check2.Text = txtdestination_company.Text & " " & txtdestination_address.Text & " " & _
                                    txtdestination_province.Text & " " & txtdest_Receive_country.Text & _
                                    IIf(CommonUtility.Get_StringValue(txtdestination_phone.Text) <> "", " TEL: " & txtdestination_phone.Text, "") & _
                                    IIf(CommonUtility.Get_StringValue(txtdestination_fax.Text) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                    & IIf(CommonUtility.Get_StringValue(txtdestination_taxid.Text) <> "", " TAX ID: " & txtdestination_taxid.Text, "") & _
                                    IIf(CommonUtility.Get_StringValue(txtdestination_email.Text) <> "", " E-mail: " & txtdestination_email.Text, "") & _
                                    " ON BEHALF OF " & txtob_dest_address.Text & IIf(txtNewEmail_ch02.Text <> "", " E-mail: " & txtNewEmail_ch02.Text, "") & IIf(txtplace_exibition.Text <> "", " " & txtplace_exibition.Text, "")

        ElseIf Mid(txtdest_remark1.Text, 2, 1) <> "/" Then
            txtdestination_Check2.Text = txtdestination_company.Text & " " & txtdestination_address.Text & " " & txtdestination_province.Text _
                                & " " & txtdest_Receive_country.Text & _
                                IIf(CommonUtility.Get_StringValue(txtdestination_phone.Text) <> "", " TEL: " & txtdestination_phone.Text, "") & _
                                IIf(CommonUtility.Get_StringValue(txtdestination_fax.Text) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                & IIf(CommonUtility.Get_StringValue(txtdestination_taxid.Text) <> "", " TAX ID: " & txtdestination_taxid.Text, "") & _
                                IIf(CommonUtility.Get_StringValue(txtdestination_email.Text) <> "", " E-mail: " & txtdestination_email.Text, "") & _
                                IIf(txtplace_exibition.Text <> "", " " & txtplace_exibition.Text, "")

        End If
    End Sub

    Function CheckValue_Text(ByVal Ch_Phone As Integer, ByVal Ch_Fax As Integer, ByVal Ch_Email As Integer, ByVal Ch_Tax As Integer) As Integer


        If Ch_Phone = 0 And Ch_Fax = 0 And Ch_Email = 0 And Ch_Tax = 0 Then
            'not phone fax mail tax
            Return 1

        ElseIf Ch_Phone <> 0 And Ch_Fax <> 0 And Ch_Email <> 0 And Ch_Tax <> 0 Then
            'phone fax email tax
            Return 2

        ElseIf Ch_Phone <> 0 And Ch_Fax <> 0 And Ch_Email <> 0 And Ch_Tax = 0 Then
            'phone fax mail
            Return 3

        ElseIf Ch_Phone <> 0 And Ch_Fax <> 0 And Ch_Email = 0 And Ch_Tax <> 0 Then
            'phone fax tax
            Return 4

        ElseIf Ch_Phone <> 0 And Ch_Fax <> 0 And Ch_Email = 0 And Ch_Tax = 0 Then
            'phone fax 
            Return 5

        ElseIf Ch_Phone <> 0 And Ch_Fax = 0 And Ch_Email = 0 And Ch_Tax = 0 Then
            'phone
            Return 6

        ElseIf Ch_Phone <> 0 And Ch_Fax = 0 And Ch_Email <> 0 And Ch_Tax <> 0 Then
            'phone mail tax
            Return 7

        ElseIf Ch_Phone <> 0 And Ch_Fax = 0 And Ch_Email = 0 And Ch_Tax <> 0 Then
            'phone tax
            Return 8

        ElseIf Ch_Phone <> 0 And Ch_Fax = 0 And Ch_Email <> 0 And Ch_Tax = 0 Then
            'phone mail
            Return 9

        ElseIf Ch_Phone = 0 And Ch_Fax <> 0 And Ch_Email <> 0 And Ch_Tax <> 0 Then
            'fax mail tax
            Return 10

        ElseIf Ch_Phone = 0 And Ch_Fax <> 0 And Ch_Email <> 0 And Ch_Tax = 0 Then
            'fax mail
            Return 11

        ElseIf Ch_Phone = 0 And Ch_Fax <> 0 And Ch_Email = 0 And Ch_Tax <> 0 Then
            'fax tax
            Return 12

        ElseIf Ch_Phone = 0 And Ch_Fax <> 0 And Ch_Email = 0 And Ch_Tax = 0 Then
            'fax
            Return 13

        ElseIf Ch_Phone = 0 And Ch_Fax = 0 And Ch_Email <> 0 And Ch_Tax <> 0 Then
            'mail tax
            Return 14

        ElseIf Ch_Phone = 0 And Ch_Fax = 0 And Ch_Email <> 0 And Ch_Tax = 0 Then
            'mail
            Return 15

        ElseIf Ch_Phone = 0 And Ch_Fax = 0 And Ch_Email = 0 And Ch_Tax <> 0 Then
            'tax
            Return 16

        Else
            Return 0
        End If
    End Function

    Function confor_mat(ByVal d_date As Date) As String
        Dim str_for As String = Format(d_date, "d/MM/yyyy")
        Return str_for
    End Function
    Sub CallInvoiceCheck() 'Check Invoince
        'เรื่อง invoice ต่างประเทศ
        Dim str_NumInvoice As String = CommonUtility.Get_StringValue(txtNumInvoice.Text)

        Select Case str_NumInvoice
            Case Is <> ""
                txtTolInvoice.Text = txtNumInvoice.Text
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

    Sub All_CheckThird_5(ByVal count_data As Integer)
        Dim All_str As String
        Select Case Check_Third(CommonUtility.Get_StringValue(txtthird_country.Value), _
                                CommonUtility.Get_StringValue(txtback_country.Value), _
                                CommonUtility.Get_StringValue(txtplace_exibition.Value))
            Case 1
                All_str = txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & _
                    "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity5.Text, txtq_unit_code5.Text) & vbCrLf & txtthird_country.Text & vbCrLf & _
                                                                txtback_country.Text & vbCrLf & _
                                                                txtplace_exibition.Text & vbCrLf & _
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 2 'T2_back_country,T3_place_exibition
                All_str = txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & _
                     "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
                                                                                BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " & _
                                                                                BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf & "        " & _
                                                                                BahtOnly(txtquantity5.Text, txtq_unit_code5.Text) & vbCrLf & _
                                                                                txtback_country.Text & vbCrLf & _
                                                                                txtplace_exibition.Text & vbCrLf & _
                                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 3 'T3_place_exibition
                All_str = txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & _
                     "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                    BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
                                                                    BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " & _
                                                                    BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf & "        " & _
                                                                    BahtOnly(txtquantity5.Text, txtq_unit_code5.Text) & vbCrLf & _
                                                                    "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 4 'ไม่มีเลย
                All_str = txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & _
                    "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                    BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
                                                                    BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " & _
                                                                    BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf & "        " & _
                                                                    BahtOnly(txtquantity5.Text, txtq_unit_code5.Text) & vbCrLf & _
                                                                    "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 5 'T2_back_country
                All_str = txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & _
                    "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                    BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
                                                                    BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " & _
                                                                    BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf & "        " & _
                                                                    BahtOnly(txtquantity5.Text, txtq_unit_code5.Text) & vbCrLf & _
                                                                    "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 6 'T3_place_exibition
                All_str = txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & _
                     "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                    BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
                                                                    BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " & _
                                                                    BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf & "        " & _
                                                                    BahtOnly(txtquantity5.Text, txtq_unit_code5.Text) & vbCrLf & _
                                                                    "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 7 'T1_third_country,T2_back_country
                All_str = txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & _
                    "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                    BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
                                                                    BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " & _
                                                                    BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf & "        " & _
                                                                    BahtOnly(txtquantity5.Text, txtq_unit_code5.Text) & vbCrLf & txtthird_country.Text & vbCrLf & _
                                                                    txtback_country.Text & vbCrLf & _
                                                                    "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 8 'T1_third_country,T3_place_exibition
                All_str = txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & _
                     "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                    BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
                                                                    BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " & _
                                                                    BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf & "        " & _
                                                                    BahtOnly(txtquantity5.Text, txtq_unit_code5.Text) & vbCrLf & txtthird_country.Text & vbCrLf & _
                                                                    txtplace_exibition.Text & vbCrLf & _
                                                                    "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 9 'T1_third_country
                All_str = txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & _
                    "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity5.Text, txtq_unit_code5.Text) & vbCrLf & "        " & txtthird_country.Text & vbCrLf & _
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
        End Select
    End Sub
    Sub All_CheckThird_4(ByVal count_data As Integer)
        Dim All_str As String

        Select Case Check_Third(CommonUtility.Get_StringValue(txtthird_country.Value), _
                                CommonUtility.Get_StringValue(txtback_country.Value), _
                                CommonUtility.Get_StringValue(txtplace_exibition.Value))
            Case 1
                All_str = txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & _
                    "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                            BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
                                            BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " & _
                                            BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf & "        " & _
                                            txtthird_country.Text & vbCrLf & _
                                            txtback_country.Text & vbCrLf & _
                                            txtplace_exibition.Text & vbCrLf & _
                                            "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 2 'T2_back_country,T3_place_exibition
                All_str = txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & _
                    "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                            BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
                                                            BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " & _
                                                            BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf & "        " & _
                                                            txtback_country.Text & vbCrLf & _
                                                            txtplace_exibition.Text & vbCrLf & _
                                                            "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 3 'T3_place_exibition
                All_str = txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & _
                    "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf & _
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 4 'ไม่มีเลย
                All_str = txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & _
                    "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf & _
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 5 'T2_back_country
                All_str = txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & _
                    "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf & "        " & _
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 6 'T3_place_exibition
                All_str = txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & _
                    "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf & _
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 7 'T1_third_country,T2_back_country
                All_str = txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & _
                    "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                    BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
                                                                    BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " & _
                                                                    BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf & "        " & _
                                                                    txtthird_country.Text & vbCrLf & _
                                                                    txtback_country.Text & vbCrLf & _
                                                                    "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 8 'T1_third_country,T3_place_exibition
                All_str = txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & _
                    "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                    BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
                                                                    BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " & _
                                                                    BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf & "        " & _
                                                                    txtthird_country.Text & vbCrLf & _
                                                                    txtplace_exibition.Text & vbCrLf & _
                                                                    "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 9 'T1_third_country
                All_str = txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & _
                    "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                    BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
                                                                    BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " & _
                                                                    BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf & "        " & _
                                                                    txtthird_country.Text & vbCrLf & _
                                                                    "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
        End Select
    End Sub
    Sub All_CheckThird_3(ByVal count_data As Integer)
        Dim All_str As String
        Select Case Check_Third(CommonUtility.Get_StringValue(txtthird_country.Value), _
                                CommonUtility.Get_StringValue(txtback_country.Value), _
                                CommonUtility.Get_StringValue(txtplace_exibition.Value))
            Case 1
                All_str = txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & _
                     "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " & _
                                                                txtthird_country.Text & vbCrLf & _
                                                                txtback_country.Text & vbCrLf & _
                                                                txtplace_exibition.Text & vbCrLf & _
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 2 'T2_back_country,T3_place_exibition
                All_str = txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & _
                    "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
                                                                                BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " & _
                                                                                txtback_country.Text & vbCrLf & _
                                                                                txtplace_exibition.Text & vbCrLf & _
                                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 3 'T3_place_exibition
                All_str = txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & _
                    "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & _
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 4 'ไม่มีเลย
                All_str = txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & _
                     "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " & _
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 5 'T2_back_country
                All_str = txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & _
                    "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & _
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 6 'T3_place_exibition
                All_str = txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & _
                    "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & _
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 7 'T1_third_country,T2_back_country
                All_str = txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & _
                    "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " & _
                                                                txtthird_country.Text & vbCrLf & _
                                                                txtback_country.Text & vbCrLf & _
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 8 'T1_third_country,T3_place_exibition
                All_str = txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & _
                    "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " & _
                                                                txtthird_country.Text & vbCrLf & _
                                                                txtplace_exibition.Text & vbCrLf & _
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 9 'T1_third_country
                All_str = txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & _
                     "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                    BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
                                                                    BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " & _
                                                                    txtthird_country.Text & vbCrLf & _
                                                                    "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
        End Select
    End Sub
    Sub All_CheckThird_2(ByVal count_data As Integer)
        Dim All_str As String
        Select Case Check_Third(CommonUtility.Get_StringValue(txtthird_country.Value), _
                                CommonUtility.Get_StringValue(txtback_country.Value), _
                                CommonUtility.Get_StringValue(txtplace_exibition.Value))
            Case 1
                All_str = txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & _
                     "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
                                                                txtthird_country.Text & vbCrLf & _
                                                                txtback_country.Text & vbCrLf & _
                                                                txtplace_exibition.Text & vbCrLf & _
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 2 'T2_back_country,T3_place_exibition
                All_str = txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & _
                    "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
                                                                                txtback_country.Text & vbCrLf & _
                                                                                txtplace_exibition.Text & vbCrLf & _
                                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 3 'T3_place_exibition
                All_str = txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & _
                    "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & _
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 4 'ไม่มีเลย
                All_str = txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & _
                    "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 5 'T2_back_country
                All_str = txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & _
                    "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & _
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 6 'T3_place_exibition
                All_str = txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & _
                    "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & _
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 7 'T1_third_country,T2_back_country
                All_str = txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & _
                    "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
                                                                txtthird_country.Text & vbCrLf & _
                                                                txtback_country.Text & vbCrLf & _
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 8 'T1_third_country,T3_place_exibition
                All_str = txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & _
                    "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
                                                                txtthird_country.Text & vbCrLf & _
                                                                txtplace_exibition.Text & vbCrLf & _
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 9 'T1_third_country
                All_str = txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & _
                    "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                    BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
                                                                    txtthird_country.Text & vbCrLf & _
                                                                    "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
        End Select
    End Sub
    Sub All_CheckThird_1(ByVal count_data As Integer)
        Dim All_str As String
        Select Case Check_Third(CommonUtility.Get_StringValue(txtthird_country.Value), _
                                CommonUtility.Get_StringValue(txtback_country.Value), _
                                CommonUtility.Get_StringValue(txtplace_exibition.Value))
            Case 1
                All_str = txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & _
                    "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                txtthird_country.Text & vbCrLf & _
                                                                txtback_country.Text & vbCrLf & _
                                                                txtplace_exibition.Text & vbCrLf & _
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 2 'T2_back_country,T3_place_exibition
                All_str = txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & _
                    "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                                txtback_country.Text & vbCrLf & _
                                                                                txtplace_exibition.Text & vbCrLf & _
                                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 3 'T3_place_exibition
                All_str = txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & _
                    "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & _
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 4 'ไม่มีเลย
                All_str = txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & _
                    "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & _
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 5 'T2_back_country
                All_str = txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & _
                    "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & _
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 6 'T3_place_exibition
                All_str = txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & _
                    "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & _
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 7 'T1_third_country,T2_back_country
                All_str = txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & _
                    "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                txtthird_country.Text & vbCrLf & _
                                                                txtback_country.Text & vbCrLf & _
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 8 'T1_third_country,T3_place_exibition
                All_str = txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & _
                    "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                txtthird_country.Text & vbCrLf & _
                                                                txtplace_exibition.Text & vbCrLf & _
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 9 'T1_third_country
                All_str = txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & _
                    "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                    txtthird_country.Text & vbCrLf & _
                                                                    "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
        End Select
    End Sub
    Sub All_CheckThird_0(ByVal count_data As Integer)
        Dim All_str As String
        Select Case Check_Third(CommonUtility.Get_StringValue(txtthird_country.Value), _
                                CommonUtility.Get_StringValue(txtback_country.Value), _
                                CommonUtility.Get_StringValue(txtplace_exibition.Value))
            Case 1
                All_str = txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & "        " & _
                    txtthird_country.Text & vbCrLf & _
                                                                txtback_country.Text & vbCrLf & _
                                                                txtplace_exibition.Text & vbCrLf & _
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 2 'T2_back_country,T3_place_exibition
                All_str = txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & _
                    txtback_country.Text & vbCrLf & _
                                                                                txtplace_exibition.Text & vbCrLf & _
                                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 3 'T3_place_exibition
                All_str = txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & _
                     "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 4 'ไม่มีเลย
                All_str = txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & _
                     "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 5 'T2_back_country
                All_str = txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & _
                    vbCrLf & "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 6 'T3_place_exibition
                All_str = txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & _
                     "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 7 'T1_third_country,T2_back_country
                All_str = txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & _
                    txtthird_country.Text & vbCrLf & _
                                                                txtback_country.Text & vbCrLf & _
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 8 'T1_third_country,T3_place_exibition
                All_str = txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & _
                     txtthird_country.Text & vbCrLf & _
                                                                txtplace_exibition.Text & vbCrLf & _
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 9 'T1_third_country
                All_str = txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & _
                    txtthird_country.Text & vbCrLf & _
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

    Sub CHeck_DisPlay()


        Dim sss As String = txtgross_weight.Text

        Dim str_gross As String

        'str_gross = txtgross_weight.Text

        'เรื่อง invoice ต่างประเทศ
        Dim str_NumInvoice As String = CommonUtility.Get_StringValue(txtNumInvoice.Text)
        'เฉพาะรัซเซีย เก็บไว้ที่FOBDisplay
        If Mid(CommonUtility.Get_StringValue(txtFOBDisplay.Value), 1, 50) = "GROSS WEIGHT - NET WEIGHT" Then
            str_gross = GrossTxt() & vbNewLine & NetWeight_(txtnet_weight.Value, txtunit_code2.Value)
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
        ElseIf Mid(CommonUtility.Get_StringValue(txtFOBDisplay.Value), 1, 50) = "GROSS WEIGHT" Then
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
        ElseIf Mid(CommonUtility.Get_StringValue(txtFOBDisplay.Value), 1, 50) = "" Then
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
    End Sub

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

    Function NetWeight_(ByVal _netweight, ByVal _uni) As String
        Dim sumStr As String
        sumStr = Format(_netweight, "#,##0.00##") & " " & _uni

        Return sumStr
    End Function

    Sub Check_box8()
        txtTemp_box8.Text = txtbox8.Value
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

        CHeck_DisPlay()
        '============================================================
        'If txtNumRowCount.Text = C_TotalRowDe.Text Then

        If (CommonUtility.Get_StringValue(txtquantity1.Text) <> "" And CommonUtility.Get_StringValue(txtquantity1.Text) <> "0.0000" And CommonUtility.Get_StringValue(txtq_unit_code1.Text) <> "") = True And _
        (CommonUtility.Get_StringValue(txtquantity2.Text) <> "" And CommonUtility.Get_StringValue(txtquantity2.Text) <> "0.0000" And CommonUtility.Get_StringValue(txtq_unit_code2.Text) <> "") = True And _
        (CommonUtility.Get_StringValue(txtquantity3.Text) <> "" And CommonUtility.Get_StringValue(txtquantity3.Text) <> "0.0000" And CommonUtility.Get_StringValue(txtq_unit_code3.Text) <> "") = True And _
        (CommonUtility.Get_StringValue(txtquantity4.Text) <> "" And CommonUtility.Get_StringValue(txtquantity4.Text) <> "0.0000" And CommonUtility.Get_StringValue(txtq_unit_code4.Text) <> "") = True And _
        (CommonUtility.Get_StringValue(txtquantity5.Text) <> "" And CommonUtility.Get_StringValue(txtquantity5.Text) <> "0.0000" And CommonUtility.Get_StringValue(txtq_unit_code5.Text) <> "") = True Then
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
        ElseIf (CommonUtility.Get_StringValue(txtquantity1.Text) <> "" And CommonUtility.Get_StringValue(txtquantity1.Text) <> "0.0000" And CommonUtility.Get_StringValue(txtq_unit_code1.Text) <> "") = True And _
        (CommonUtility.Get_StringValue(txtquantity2.Text) <> "" And CommonUtility.Get_StringValue(txtquantity2.Text) <> "0.0000" And CommonUtility.Get_StringValue(txtq_unit_code2.Text) <> "") = True And _
        (CommonUtility.Get_StringValue(txtquantity3.Text) <> "" And CommonUtility.Get_StringValue(txtquantity3.Text) <> "0.0000" And CommonUtility.Get_StringValue(txtq_unit_code3.Text) <> "") = True And _
        (CommonUtility.Get_StringValue(txtquantity4.Text) <> "" And CommonUtility.Get_StringValue(txtquantity4.Text) <> "0.0000" And CommonUtility.Get_StringValue(txtq_unit_code4.Text) <> "") = True And _
        (CommonUtility.Get_StringValue(txtquantity5.Text) <> "" And CommonUtility.Get_StringValue(txtquantity5.Text) <> "0.0000" And CommonUtility.Get_StringValue(txtq_unit_code5.Text) <> "") = False Then
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


        ElseIf (CommonUtility.Get_StringValue(txtquantity1.Text) <> "" And CommonUtility.Get_StringValue(txtquantity1.Text) <> "0.0000" And CommonUtility.Get_StringValue(txtq_unit_code1.Text) <> "") = True And _
         (CommonUtility.Get_StringValue(txtquantity2.Text) <> "" And CommonUtility.Get_StringValue(txtquantity2.Text) <> "0.0000" And CommonUtility.Get_StringValue(txtq_unit_code2.Text) <> "") = True And _
         (CommonUtility.Get_StringValue(txtquantity3.Text) <> "" And CommonUtility.Get_StringValue(txtquantity3.Text) <> "0.0000" And CommonUtility.Get_StringValue(txtq_unit_code3.Text) <> "") = True And _
         (CommonUtility.Get_StringValue(txtquantity4.Text) <> "" And CommonUtility.Get_StringValue(txtquantity4.Text) <> "0.0000" And CommonUtility.Get_StringValue(txtq_unit_code4.Text) <> "") = False And _
         (CommonUtility.Get_StringValue(txtquantity5.Text) <> "" And CommonUtility.Get_StringValue(txtquantity5.Text) <> "0.0000" And CommonUtility.Get_StringValue(txtq_unit_code5.Text) <> "") = False Then
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

        ElseIf (CommonUtility.Get_StringValue(txtquantity1.Text) <> "" And CommonUtility.Get_StringValue(txtquantity1.Text) <> "0.0000" And CommonUtility.Get_StringValue(txtq_unit_code1.Text) <> "") = True And _
          (CommonUtility.Get_StringValue(txtquantity2.Text) <> "" And CommonUtility.Get_StringValue(txtquantity2.Text) <> "0.0000" And CommonUtility.Get_StringValue(txtq_unit_code2.Text) <> "") = True And _
          (CommonUtility.Get_StringValue(txtquantity3.Text) <> "" And CommonUtility.Get_StringValue(txtquantity3.Text) <> "0.0000" And CommonUtility.Get_StringValue(txtq_unit_code3.Text) <> "") = False And _
          (CommonUtility.Get_StringValue(txtquantity4.Text) <> "" And CommonUtility.Get_StringValue(txtquantity4.Text) <> "0.0000" And CommonUtility.Get_StringValue(txtq_unit_code4.Text) <> "") = False And _
          (CommonUtility.Get_StringValue(txtquantity5.Text) <> "" And CommonUtility.Get_StringValue(txtquantity5.Text) <> "0.0000" And CommonUtility.Get_StringValue(txtq_unit_code5.Text) <> "") = False Then
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

        ElseIf (CommonUtility.Get_StringValue(txtquantity1.Text) <> "" And CommonUtility.Get_StringValue(txtquantity1.Text) <> "0.0000" And CommonUtility.Get_StringValue(txtq_unit_code1.Text) <> "") = True And _
          (CommonUtility.Get_StringValue(txtquantity2.Text) <> "" And CommonUtility.Get_StringValue(txtquantity2.Text) <> "0.0000" And CommonUtility.Get_StringValue(txtq_unit_code2.Text) <> "") = False And _
          (CommonUtility.Get_StringValue(txtquantity3.Text) <> "" And CommonUtility.Get_StringValue(txtquantity3.Text) <> "0.0000" And CommonUtility.Get_StringValue(txtq_unit_code3.Text) <> "") = False And _
          (CommonUtility.Get_StringValue(txtquantity4.Text) <> "" And CommonUtility.Get_StringValue(txtquantity4.Text) <> "0.0000" And CommonUtility.Get_StringValue(txtq_unit_code4.Text) <> "") = False And _
          (CommonUtility.Get_StringValue(txtquantity5.Text) <> "" And CommonUtility.Get_StringValue(txtquantity5.Text) <> "0.0000" And CommonUtility.Get_StringValue(txtq_unit_code5.Text) <> "") = False Then
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
            All_CheckThird_0(Cpage)
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

    Private Sub PageFooter1_Format(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PageFooter1.Format
        '//txtcompany_provincefoot.Text = txtcompany_provincefoot1.Text & " " & Now.Day & "/" & Now.Month & "/" & Now.Year

    End Sub

    Private Sub Detail1_Format(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Detail1.Format
        Cpage += 1

        Count_RowToPage_DetailData()

        txtNumRowCount.Text = Cpage
        check8() 'Check_box8()

        Dim str_mark As String

        str_mark = txtmarks.Text
        Select Case C_TotalRowDe.Text
            Case 1
                txtTemp_marks.Text = str_mark
            Case Else
                If Cpage > 1 Then
                    str_mark = ""
                    txtTemp_marks.Text = str_mark
                ElseIf Cpage = 1 Then

                    txtTemp_marks.Text = str_mark
                End If
        End Select
    End Sub

    Private Sub PageHeader1_Format(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PageHeader1.Format
        CpageNum += 1
        'txtreference_code2_Temp.Text = "I" & txtreference_code2.Value

        ''ByTine 01-01-2560
        txtreference_code2_Temp.Text = txtreference_code2.Text
        '===============================================================

        'txtPage2.Text = Cpage
        '===============================================================
        Head_Checkcompany_v1()
        Head_Checkdestination_v2()
    End Sub

    Private Sub rpt3_ediFORM1RU_PageStart(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PageStart
        Me.CurrentPage.DrawLine(0.06, 4.25, 0.06, 8.5)
        Me.CurrentPage.DrawLine(1, 4.25, 1, 8.5)
        Me.CurrentPage.DrawLine(2, 4.25, 2, 8.5)
        Me.CurrentPage.DrawLine(5.2, 4.25, 5.2, 8.5)
        Me.CurrentPage.DrawLine(6.07, 4.25, 6.07, 8.5)
        Me.CurrentPage.DrawLine(7.1, 4.25, 7.1, 8.5)
        Me.CurrentPage.DrawLine(8.19, 4.25, 8.19, 8.5)
    End Sub

    Private Sub rpt3_ediFORM1RU_ReportStart(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.ReportStart
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
End Class
