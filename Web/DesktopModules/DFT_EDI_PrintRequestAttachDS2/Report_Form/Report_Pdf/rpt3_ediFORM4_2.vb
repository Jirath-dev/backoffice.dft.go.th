Imports DataDynamics.ActiveReports
Imports DataDynamics.ActiveReports.Document

Imports Microsoft.ApplicationBlocks.Data
Imports System.Data.SqlClient
Imports System.Data
Imports System.Text
Imports DFT.Dotnetnuke.ClassLibrary
Imports ReportPrintClass
Public Class rpt3_ediFORM4_2 
    Dim Cpage As Integer
    Dim CpageNum As Integer
    Private Sub PageHeader1_Format(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PageHeader1.Format
        CpageNum += 1
        '        txtTemp_PageOf.Text = "Page : " & CpageNum & " of " & txt_PageCount.Value

        txtreference_code2_Temp.Text = "I" & txtreference_code2.Value
        '===============================================================

        '===============================================================
        Head_Checkcompany_v1()
        Head_Checkdestination_v2()
    End Sub

    Sub Head_Checkcompany_v1()
        Select Case CommonUtility.Get_StringValue(txtTax_Status.Text)
            Case 1 'ช่อง ให้แสดงหรือไม่แสดง Tax 1 คือ แสดง
                If Mid(txtdest_remark.Text, 1, 3) = "C/O" Then
                    txtCompany_Check_1.Text = txtob_address.Text & " CARE OF " & txtcompany_name.Text & " " & txtcompany_address.Text _
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
                                        IIf(CommonUtility.Get_StringValue(txtob_address.Text) <> "", " ON BEHALF OF " & txtob_address.Text, "")
                ElseIf Mid(txtdest_remark.Text, 2, 1) <> "/" Then
                    txtCompany_Check_1.Text = txtcompany_name.Text & " " & txtcompany_address.Text & " " & txtcompany_province.Text _
                                        & " " & txtcompany_country.Text & IIf(CommonUtility.Get_StringValue(txtcompany_phone.Text) <> "", " TEL: " & txtcompany_phone.Text, "") & _
                                        IIf(CommonUtility.Get_StringValue(txtcompany_fax.Text) <> "", " FAX: " & txtcompany_fax.Text, "") _
                                        & IIf(CommonUtility.Get_StringValue(txtcompany_taxno.Text) <> "", " TAX ID: " & txtcompany_taxno.Text, "") & _
                                        IIf(CommonUtility.Get_StringValue(txtcompany_email.Text) <> "", " E-mail: " & txtcompany_email.Text, "")
                End If
            Case 0 'ช่อง ให้แสดงหรือไม่แสดง Tax 0 คือ ไม่แสดง
                If Mid(txtdest_remark.Text, 1, 3) = "C/O" Then
                    txtCompany_Check_1.Text = txtob_address.Text & " CARE OF " & txtcompany_name.Text & " " & txtcompany_address.Text _
                                       & " " & txtcompany_province.Text & " " & txtcompany_country.Text & _
                                       IIf(CommonUtility.Get_StringValue(txtcompany_phone.Text) <> "", " TEL: " & txtcompany_phone.Text, "") & _
                                       IIf(CommonUtility.Get_StringValue(txtcompany_fax.Text) <> "", " FAX: " & txtcompany_fax.Text, "") & _
                                       IIf(CommonUtility.Get_StringValue(txtcompany_email.Text) <> "", " E-mail: " & txtcompany_email.Text, "")
                ElseIf Mid(txtdest_remark.Text, 1, 3) = "O/B" Then
                    txtCompany_Check_1.Text = txtcompany_name.Text & " " & txtcompany_address.Text & " " & txtcompany_province.Text _
                                        & " " & txtcompany_country.Text & IIf(CommonUtility.Get_StringValue(txtcompany_phone.Text) <> "", " TEL: " & txtcompany_phone.Text, "") & _
                                        IIf(CommonUtility.Get_StringValue(txtcompany_fax.Text) <> "", " FAX: " & txtcompany_fax.Text, "") _
                                        & IIf(CommonUtility.Get_StringValue(txtcompany_email.Text) <> "", " E-mail: " & txtcompany_email.Text, "") & _
                                        IIf(CommonUtility.Get_StringValue(txtob_address.Text) <> "", " ON BEHALF OF " & txtob_address.Text, "")
                ElseIf Mid(txtdest_remark.Text, 2, 1) <> "/" Then
                    txtCompany_Check_1.Text = txtcompany_name.Text & " " & txtcompany_address.Text & " " & txtcompany_province.Text _
                                        & " " & txtcompany_country.Text & IIf(CommonUtility.Get_StringValue(txtcompany_phone.Text) <> "", " TEL: " & txtcompany_phone.Text, "") & _
                                        IIf(CommonUtility.Get_StringValue(txtcompany_fax.Text) <> "", " FAX: " & txtcompany_fax.Text, "") _
                                        & IIf(CommonUtility.Get_StringValue(txtcompany_email.Text) <> "", " E-mail: " & txtcompany_email.Text, "")
                End If
        End Select
        
    End Sub

    Sub Head_Checkdestination_v2()
        txtdestination_Check2.Text = txtdestination_company.Text & " " & txtdestination_address.Text & " " & txtdestination_province.Text _
                                & " " & txtdest_Receive_country.Text & _
                                IIf(CommonUtility.Get_StringValue(txtdestination_phone.Text) <> "", " TEL: " & txtdestination_phone.Text, "") & _
                                IIf(CommonUtility.Get_StringValue(txtdestination_fax.Text) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                & IIf(CommonUtility.Get_StringValue(txtdestination_taxid.Text) <> "", " TAX ID: " & txtdestination_taxid.Text, "") & _
                                IIf(CommonUtility.Get_StringValue(txtdestination_email.Text) <> "", " E-mail: " & txtdestination_email.Text, "") & _
                                IIf(txtplace_exibition.Text <> "", " " & txtplace_exibition.Text, "")
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
    Sub Head_Checkcompany()
        Dim V_Com As Integer = CheckValue_Text(Len(CommonUtility.Get_StringValue(txtcompany_phone.Text)) _
                                , Len(CommonUtility.Get_StringValue(txtcompany_fax.Text)) _
                                , Len(CommonUtility.Get_StringValue(txtcompany_email.Text)) _
                                , Len(CommonUtility.Get_StringValue(txtcompany_taxno.Text)))

        If Mid(txtdest_remark.Text, 1, 3) = "C/O" Then
            Select Case V_Com
                Case 1 'not phone fax mail tax
                    txtCompany_Check_1.Text = txtob_address.Text + " CARE OF " + txtcompany_name.Text + " " + txtcompany_address.Text _
                    + " " + txtcompany_province.Text + " " + txtcompany_country.Text

                Case 2 'phone fax email tax
                    txtCompany_Check_1.Text = txtob_address.Text + " CARE OF " + txtcompany_name.Text + " " + txtcompany_address.Text _
                    + " " + txtcompany_province.Text + " " + txtcompany_country.Text + " TEL: " + txtcompany_phone.Text + " FAX: " _
                    + txtcompany_fax.Text + " TAX ID:  " + txtcompany_taxno.Text + " E-mail: " + txtcompany_email.Text

                Case 3 'phone fax mail
                    txtCompany_Check_1.Text = txtob_address.Text + " CARE OF " + txtcompany_name.Text + " " + txtcompany_address.Text _
                    + " " + txtcompany_province.Text + " " + txtcompany_country.Text + " TEL: " + txtcompany_phone.Text + " FAX: " _
                    + txtcompany_fax.Text + " E-mail: " + txtcompany_email.Text

                Case 4 'phone fax tax
                    txtCompany_Check_1.Text = txtob_address.Text + " CARE OF " + txtcompany_name.Text + " " + txtcompany_address.Text _
                    + " " + txtcompany_province.Text + " " + txtcompany_country.Text + " TEL: " + txtcompany_phone.Text + " FAX: " _
                    + txtcompany_fax.Text + " TAX ID:  " + txtcompany_taxno.Text

                Case 5 'phone fax 
                    txtCompany_Check_1.Text = txtob_address.Text + " CARE OF " + txtcompany_name.Text + " " + txtcompany_address.Text _
                    + " " + txtcompany_province.Text + " " + txtcompany_country.Text + " TEL: " + txtcompany_phone.Text + " FAX: " _
                    + txtcompany_fax.Text

                Case 6 'phone
                    txtCompany_Check_1.Text = txtob_address.Text + " CARE OF " + txtcompany_name.Text + " " + txtcompany_address.Text _
                    + " " + txtcompany_province.Text + " " + txtcompany_country.Text + " TEL: " + txtcompany_phone.Text

                Case 7 'phone mail tax
                    txtCompany_Check_1.Text = txtob_address.Text + " CARE OF " + txtcompany_name.Text + " " + txtcompany_address.Text _
                    + " " + txtcompany_province.Text + " " + txtcompany_country.Text + " TEL: " + txtcompany_phone.Text _
                    + " TAX ID:  " + txtcompany_taxno.Text + " E-mail: " + txtcompany_email.Text

                Case 8 'phone tax
                    txtCompany_Check_1.Text = txtob_address.Text + " CARE OF " + txtcompany_name.Text + " " + txtcompany_address.Text _
                    + " " + txtcompany_province.Text + " " + txtcompany_country.Text + " TEL: " + txtcompany_phone.Text _
                    + " TAX ID:  " + txtcompany_taxno.Text

                Case 9 'phone mail
                    txtCompany_Check_1.Text = txtob_address.Text + " CARE OF " + txtcompany_name.Text + " " + txtcompany_address.Text _
                    + " " + txtcompany_province.Text + " " + txtcompany_country.Text + " TEL: " + txtcompany_phone.Text _
                    + " E-mail: " + txtcompany_email.Text

                Case 10 'fax mail tax
                    txtCompany_Check_1.Text = txtob_address.Text + " CARE OF " + txtcompany_name.Text + " " + txtcompany_address.Text _
                    + " " + txtcompany_province.Text + " " + txtcompany_country.Text + " FAX: " _
                    + txtcompany_fax.Text + " TAX ID:  " + txtcompany_taxno.Text + " E-mail: " + txtcompany_email.Text

                Case 11 'fax mail
                    txtCompany_Check_1.Text = txtob_address.Text + " CARE OF " + txtcompany_name.Text + " " + txtcompany_address.Text _
                    + " " + txtcompany_province.Text + " " + txtcompany_country.Text + " FAX: " _
                    + txtcompany_fax.Text + " E-mail: " + txtcompany_email.Text

                Case 12 'fax tax
                    txtCompany_Check_1.Text = txtob_address.Text + " CARE OF " + txtcompany_name.Text + " " + txtcompany_address.Text _
                    + " " + txtcompany_province.Text + " " + txtcompany_country.Text + " FAX: " _
                    + txtcompany_fax.Text + " TAX ID:  " + txtcompany_taxno.Text

                Case 13 'fax
                    txtCompany_Check_1.Text = txtob_address.Text + " CARE OF " + txtcompany_name.Text + " " + txtcompany_address.Text _
                    + " " + txtcompany_province.Text + " " + txtcompany_country.Text + " FAX: " _
                    + txtcompany_fax.Text

                Case 14 'mail tax
                    txtCompany_Check_1.Text = txtob_address.Text + " CARE OF " + txtcompany_name.Text + " " + txtcompany_address.Text _
                    + " " + txtcompany_province.Text + " " + txtcompany_country.Text + " TAX ID:  " + txtcompany_taxno.Text + " E-mail: " + txtcompany_email.Text

                Case 15 'mail
                    txtCompany_Check_1.Text = txtob_address.Text + " CARE OF " + txtcompany_name.Text + " " + txtcompany_address.Text _
                    + " " + txtcompany_province.Text + " " + txtcompany_country.Text + " E-mail: " + txtcompany_email.Text

                Case 16 'tax
                    txtCompany_Check_1.Text = txtob_address.Text + " CARE OF " + txtcompany_name.Text + " " + txtcompany_address.Text _
                    + " " + txtcompany_province.Text + " " + txtcompany_country.Text + " TAX ID:  " + txtcompany_taxno.Text

                Case Else
                    txtCompany_Check_1.Text = txtob_address.Text + " CARE OF " + txtcompany_name.Text + " " + txtcompany_address.Text + " " + txtcompany_province.Text + " " + txtcompany_country.Text
            End Select

        ElseIf Mid(txtdest_remark.Text, 1, 3) = "O/B" Then
            Select Case V_Com
                Case 1 'not phone fax mail tax
                    txtCompany_Check_1.Text = txtcompany_name.Text + " " + txtcompany_address.Text + " " + txtcompany_province.Text _
                    + " " + txtcompany_country.Text + " ON BEHALF OF " + txtob_address.Text

                Case 2 'phone fax email tax
                    txtCompany_Check_1.Text = txtcompany_name.Text + " " + txtcompany_address.Text + " " + txtcompany_province.Text _
                    + " " + txtcompany_country.Text + " TEL: " + txtcompany_phone.Text + " FAX: " + txtcompany_fax.Text _
                    + " TAX ID: " + txtcompany_taxno.Text + " E-mail: " + txtcompany_email.Text + " ON BEHALF OF " + txtob_address.Text

                Case 3 'phone fax mail
                    txtCompany_Check_1.Text = txtcompany_name.Text + " " + txtcompany_address.Text + " " + txtcompany_province.Text _
                    + " " + txtcompany_country.Text + " TEL: " + txtcompany_phone.Text + " FAX: " + txtcompany_fax.Text _
                    + " E-mail: " + txtcompany_email.Text + " ON BEHALF OF " + txtob_address.Text

                Case 4 'phone fax tax
                    txtCompany_Check_1.Text = txtcompany_name.Text + " " + txtcompany_address.Text + " " + txtcompany_province.Text _
                    + " " + txtcompany_country.Text + " TEL: " + txtcompany_phone.Text + " FAX: " + txtcompany_fax.Text _
                    + " TAX ID: " + txtcompany_taxno.Text + " ON BEHALF OF " + txtob_address.Text

                Case 5 'phone fax 
                    txtCompany_Check_1.Text = txtcompany_name.Text + " " + txtcompany_address.Text + " " + txtcompany_province.Text _
                    + " " + txtcompany_country.Text + " TEL: " + txtcompany_phone.Text + " FAX: " + txtcompany_fax.Text _
                    + " ON BEHALF OF " + txtob_address.Text

                Case 6 'phone
                    txtCompany_Check_1.Text = txtcompany_name.Text + " " + txtcompany_address.Text + " " + txtcompany_province.Text _
                    + " " + txtcompany_country.Text + " TEL: " + txtcompany_phone.Text + " ON BEHALF OF " + txtob_address.Text

                Case 7 'phone mail tax
                    txtCompany_Check_1.Text = txtcompany_name.Text + " " + txtcompany_address.Text + " " + txtcompany_province.Text _
                    + " " + txtcompany_country.Text + " TEL: " + txtcompany_phone.Text _
                    + " TAX ID: " + txtcompany_taxno.Text + " E-mail: " + txtcompany_email.Text + " ON BEHALF OF " + txtob_address.Text

                Case 8 'phone tax
                    txtCompany_Check_1.Text = txtcompany_name.Text + " " + txtcompany_address.Text + " " + txtcompany_province.Text _
                    + " " + txtcompany_country.Text + " TEL: " + txtcompany_phone.Text _
                    + " TAX ID: " + txtcompany_taxno.Text + " ON BEHALF OF " + txtob_address.Text

                Case 9 'phone mail
                    txtCompany_Check_1.Text = txtcompany_name.Text + " " + txtcompany_address.Text + " " + txtcompany_province.Text _
                    + " " + txtcompany_country.Text + " TEL: " + txtcompany_phone.Text _
                    + " E-mail: " + txtcompany_email.Text + " ON BEHALF OF " + txtob_address.Text

                Case 10 'fax mail tax
                    txtCompany_Check_1.Text = txtcompany_name.Text + " " + txtcompany_address.Text + " " + txtcompany_province.Text _
                    + " " + txtcompany_country.Text + " FAX: " + txtcompany_fax.Text _
                    + " TAX ID: " + txtcompany_taxno.Text + " E-mail: " + txtcompany_email.Text + " ON BEHALF OF " + txtob_address.Text

                Case 11 'fax mail
                    txtCompany_Check_1.Text = txtcompany_name.Text + " " + txtcompany_address.Text + " " + txtcompany_province.Text _
                    + " " + txtcompany_country.Text + " FAX: " + txtcompany_fax.Text _
                    + " E-mail: " + txtcompany_email.Text + " ON BEHALF OF " + txtob_address.Text

                Case 12 'fax tax
                    txtCompany_Check_1.Text = txtcompany_name.Text + " " + txtcompany_address.Text + " " + txtcompany_province.Text _
                    + " " + txtcompany_country.Text + " FAX: " + txtcompany_fax.Text _
                    + " TAX ID: " + txtcompany_taxno.Text + " ON BEHALF OF " + txtob_address.Text

                Case 13 'fax
                    txtCompany_Check_1.Text = txtcompany_name.Text + " " + txtcompany_address.Text + " " + txtcompany_province.Text _
                    + " " + txtcompany_country.Text + " FAX: " + txtcompany_fax.Text + " ON BEHALF OF " + txtob_address.Text

                Case 14 'mail tax
                    txtCompany_Check_1.Text = txtcompany_name.Text + " " + txtcompany_address.Text + " " + txtcompany_province.Text _
                    + " " + txtcompany_country.Text + " TAX ID: " + txtcompany_taxno.Text + " E-mail: " + txtcompany_email.Text + " ON BEHALF OF " + txtob_address.Text

                Case 15 'mail
                    txtCompany_Check_1.Text = txtcompany_name.Text + " " + txtcompany_address.Text + " " + txtcompany_province.Text _
                    + " " + txtcompany_country.Text + " E-mail: " + txtcompany_email.Text + " ON BEHALF OF " + txtob_address.Text

                Case 16 'tax
                    txtCompany_Check_1.Text = txtcompany_name.Text + " " + txtcompany_address.Text + " " + txtcompany_province.Text _
                    + " " + txtcompany_country.Text + " TAX ID: " + txtcompany_taxno.Text + " ON BEHALF OF " + txtob_address.Text

                Case Else
                    txtCompany_Check_1.Text = txtcompany_name.Text + " " + txtcompany_address.Text + " " + txtcompany_province.Text _
                                        + " " + txtcompany_country.Text + " ON BEHALF OF " + txtob_address.Text
            End Select
        ElseIf Mid(txtdest_remark.Text, 2, 1) <> "/" Then
            Select Case V_Com
                Case 1 'not phone fax mail tax
                    txtCompany_Check_1.Text = txtcompany_name.Text + " " + txtcompany_address.Text + " " + txtcompany_province.Text _
                    + " " + txtcompany_country.Text

                Case 2 'phone fax email tax
                    txtCompany_Check_1.Text = txtcompany_name.Text + " " + txtcompany_address.Text + " " + txtcompany_province.Text _
                    + " " + txtcompany_country.Text + " TEL: " + txtcompany_phone.Text + " FAX: " + txtcompany_fax.Text _
                    + " TAX ID: " + txtcompany_taxno.Text + " E-mail: " + txtcompany_email.Text

                Case 3 'phone fax mail
                    txtCompany_Check_1.Text = txtcompany_name.Text + " " + txtcompany_address.Text + " " + txtcompany_province.Text _
                     + " " + txtcompany_country.Text + " TEL: " + txtcompany_phone.Text + " FAX: " + txtcompany_fax.Text _
                     + " E-mail: " + txtcompany_email.Text

                Case 4 'phone fax tax
                    txtCompany_Check_1.Text = txtcompany_name.Text + " " + txtcompany_address.Text + " " + txtcompany_province.Text _
                    + " " + txtcompany_country.Text + " TEL: " + txtcompany_phone.Text + " FAX: " + txtcompany_fax.Text _
                    + " TAX ID: " + txtcompany_taxno.Text

                Case 5 'phone fax 
                    txtCompany_Check_1.Text = txtcompany_name.Text + " " + txtcompany_address.Text + " " + txtcompany_province.Text _
                     + " " + txtcompany_country.Text + " TEL: " + txtcompany_phone.Text + " FAX: " + txtcompany_fax.Text

                Case 6 'phone
                    txtCompany_Check_1.Text = txtcompany_name.Text + " " + txtcompany_address.Text + " " + txtcompany_province.Text _
                     + " " + txtcompany_country.Text + " TEL: " + txtcompany_phone.Text

                Case 7 'phone mail tax
                    txtCompany_Check_1.Text = txtcompany_name.Text + " " + txtcompany_address.Text + " " + txtcompany_province.Text _
                    + " " + txtcompany_country.Text + " TEL: " + txtcompany_phone.Text _
                    + " TAX ID: " + txtcompany_taxno.Text + " E-mail: " + txtcompany_email.Text

                Case 8 'phone tax
                    txtCompany_Check_1.Text = txtcompany_name.Text + " " + txtcompany_address.Text + " " + txtcompany_province.Text _
                     + " " + txtcompany_country.Text + " TEL: " + txtcompany_phone.Text _
                     + " TAX ID: " + txtcompany_taxno.Text

                Case 9 'phone mail
                    txtCompany_Check_1.Text = txtcompany_name.Text + " " + txtcompany_address.Text + " " + txtcompany_province.Text _
                    + " " + txtcompany_country.Text + " TEL: " + txtcompany_phone.Text _
                    + " E-mail: " + txtcompany_email.Text

                Case 10 'fax mail tax
                    txtCompany_Check_1.Text = txtcompany_name.Text + " " + txtcompany_address.Text + " " + txtcompany_province.Text _
                    + " " + txtcompany_country.Text + " FAX: " + txtcompany_fax.Text _
                    + " TAX ID: " + txtcompany_taxno.Text + " E-mail: " + txtcompany_email.Text

                Case 11 'fax mail
                    txtCompany_Check_1.Text = txtcompany_name.Text + " " + txtcompany_address.Text + " " + txtcompany_province.Text _
                    + " " + txtcompany_country.Text + " FAX: " + txtcompany_fax.Text _
                    + " E-mail: " + txtcompany_email.Text

                Case 12 'fax tax
                    txtCompany_Check_1.Text = txtcompany_name.Text + " " + txtcompany_address.Text + " " + txtcompany_province.Text _
                     + " " + txtcompany_country.Text + " FAX: " + txtcompany_fax.Text _
                     + " TAX ID: " + txtcompany_taxno.Text

                Case 13 'fax
                    txtCompany_Check_1.Text = txtcompany_name.Text + " " + txtcompany_address.Text + " " + txtcompany_province.Text _
                    + " " + txtcompany_country.Text + " FAX: " + txtcompany_fax.Text

                Case 14 'mail tax
                    txtCompany_Check_1.Text = txtcompany_name.Text + " " + txtcompany_address.Text + " " + txtcompany_province.Text _
                    + " " + txtcompany_country.Text + " TAX ID: " + txtcompany_taxno.Text + " E-mail: " + txtcompany_email.Text

                Case 15 'mail
                    txtCompany_Check_1.Text = txtcompany_name.Text + " " + txtcompany_address.Text + " " + txtcompany_province.Text _
                    + " " + txtcompany_country.Text + " E-mail: " + txtcompany_email.Text

                Case 16 'tax
                    txtCompany_Check_1.Text = txtcompany_name.Text + " " + txtcompany_address.Text + " " + txtcompany_province.Text _
                    + " " + txtcompany_country.Text + " TAX ID: " + txtcompany_taxno.Text

                Case Else
                    txtCompany_Check_1.Text = txtcompany_name.Text + " " + txtcompany_address.Text + " " + txtcompany_province.Text _
                    + " " + txtcompany_country.Text
            End Select
        End If
    End Sub
    Sub Head_Checkdestination()
        Dim V_Com_dest As Integer = CheckValue_Text(Len(CommonUtility.Get_StringValue(txtdestination_phone.Text)) _
                                    , Len(CommonUtility.Get_StringValue(txtdestination_fax.Text)) _
                                    , Len(CommonUtility.Get_StringValue(txtdestination_email.Text)) _
                                    , Len(CommonUtility.Get_StringValue(txtdestination_taxid.Text)))

        If Mid(txtdest_remark1.Text, 1, 3) = "C/O" Then

            Select Case V_Com_dest
                Case 1 'not phone fax mail tax
                    txtdestination_Check2.Text = txtob_dest_address.Text + " CARE OF " + txtdestination_company.Text + " " + txtdestination_address.Text _
                   + " " + txtdestination_province.Text + " " + txtdest_Receive_country.Text

                Case 2 'phone fax email tax
                    txtdestination_Check2.Text = txtob_dest_address.Text + " CARE OF " + txtdestination_company.Text + " " + txtdestination_address.Text _
                    + " " + txtdestination_province.Text + " " + txtdest_Receive_country.Text + " TEL: " + txtdestination_phone.Text + " FAX: " _
                    + txtdestination_fax.Text + " TAX ID:  " + txtdestination_taxid.Text + " E-mail: " + txtdestination_email.Text

                Case 3 'phone fax mail
                    txtdestination_Check2.Text = txtob_dest_address.Text + " CARE OF " + txtdestination_company.Text + " " + txtdestination_address.Text _
                   + " " + txtdestination_province.Text + " " + txtdest_Receive_country.Text + " TEL: " + txtdestination_phone.Text + " FAX: " _
                   + txtdestination_fax.Text + " E-mail: " + txtdestination_email.Text

                Case 4 'phone fax tax
                    txtdestination_Check2.Text = txtob_dest_address.Text + " CARE OF " + txtdestination_company.Text + " " + txtdestination_address.Text _
                   + " " + txtdestination_province.Text + " " + txtdest_Receive_country.Text + " TEL: " + txtdestination_phone.Text + " FAX: " _
                   + txtdestination_fax.Text + " TAX ID:  " + txtdestination_taxid.Text

                Case 5 'phone fax 
                    txtdestination_Check2.Text = txtob_dest_address.Text + " CARE OF " + txtdestination_company.Text + " " + txtdestination_address.Text _
                   + " " + txtdestination_province.Text + " " + txtdest_Receive_country.Text + " TEL: " + txtdestination_phone.Text + " FAX: " _
                   + txtdestination_fax.Text

                Case 6 'phone
                    txtdestination_Check2.Text = txtob_dest_address.Text + " CARE OF " + txtdestination_company.Text + " " + txtdestination_address.Text _
                   + " " + txtdestination_province.Text + " " + txtdest_Receive_country.Text + " TEL: " + txtdestination_phone.Text

                Case 7 'phone mail tax
                    txtdestination_Check2.Text = txtob_dest_address.Text + " CARE OF " + txtdestination_company.Text + " " + txtdestination_address.Text _
                   + " " + txtdestination_province.Text + " " + txtdest_Receive_country.Text + " TEL: " + txtdestination_phone.Text _
                   + " TAX ID:  " + txtdestination_taxid.Text + " E-mail: " + txtdestination_email.Text

                Case 8 'phone tax
                    txtdestination_Check2.Text = txtob_dest_address.Text + " CARE OF " + txtdestination_company.Text + " " + txtdestination_address.Text _
                   + " " + txtdestination_province.Text + " " + txtdest_Receive_country.Text + " TEL: " + txtdestination_phone.Text _
                   + " TAX ID:  " + txtdestination_taxid.Text

                Case 9 'phone mail
                    txtdestination_Check2.Text = txtob_dest_address.Text + " CARE OF " + txtdestination_company.Text + " " + txtdestination_address.Text _
                   + " " + txtdestination_province.Text + " " + txtdest_Receive_country.Text + " TEL: " + txtdestination_phone.Text _
                   + " E-mail: " + txtdestination_email.Text

                Case 10 'fax mail tax
                    txtdestination_Check2.Text = txtob_dest_address.Text + " CARE OF " + txtdestination_company.Text + " " + txtdestination_address.Text _
                   + " " + txtdestination_province.Text + " " + txtdest_Receive_country.Text + " FAX: " _
                   + txtdestination_fax.Text + " TAX ID:  " + txtdestination_taxid.Text + " E-mail: " + txtdestination_email.Text

                Case 11 'fax mail
                    txtdestination_Check2.Text = txtob_dest_address.Text + " CARE OF " + txtdestination_company.Text + " " + txtdestination_address.Text _
                   + " " + txtdestination_province.Text + " " + txtdest_Receive_country.Text + " FAX: " _
                   + txtdestination_fax.Text + " E-mail: " + txtdestination_email.Text

                Case 12 'fax tax
                    txtdestination_Check2.Text = txtob_dest_address.Text + " CARE OF " + txtdestination_company.Text + " " + txtdestination_address.Text _
                   + " " + txtdestination_province.Text + " " + txtdest_Receive_country.Text + " FAX: " _
                   + txtdestination_fax.Text + " TAX ID:  " + txtdestination_taxid.Text

                Case 13 'fax
                    txtdestination_Check2.Text = txtob_dest_address.Text + " CARE OF " + txtdestination_company.Text + " " + txtdestination_address.Text _
                   + " " + txtdestination_province.Text + " " + txtdest_Receive_country.Text + " FAX: " _
                   + txtdestination_fax.Text

                Case 14 'mail tax
                    txtdestination_Check2.Text = txtob_dest_address.Text + " CARE OF " + txtdestination_company.Text + " " + txtdestination_address.Text _
                   + " " + txtdestination_province.Text + " " + txtdest_Receive_country.Text + " TAX ID:  " + txtdestination_taxid.Text + " E-mail: " + txtdestination_email.Text

                Case 15 'mail
                    txtdestination_Check2.Text = txtob_dest_address.Text + " CARE OF " + txtdestination_company.Text + " " + txtdestination_address.Text _
                   + " " + txtdestination_province.Text + " " + txtdest_Receive_country.Text + " E-mail: " + txtdestination_email.Text

                Case 16 'tax
                    txtdestination_Check2.Text = txtob_dest_address.Text + " CARE OF " + txtdestination_company.Text + " " + txtdestination_address.Text _
                   + " " + txtdestination_province.Text + " " + txtdest_Receive_country.Text + " TAX ID:  " + txtdestination_taxid.Text

                Case Else
                    txtdestination_Check2.Text = txtob_dest_address.Text + " CARE OF " + txtdestination_company.Text + " " + txtdestination_address.Text _
                   + " " + txtdestination_province.Text + " " + txtdest_Receive_country.Text

            End Select
        ElseIf Mid(txtdest_remark1.Text, 1, 3) = "O/B" Then
            Select Case V_Com_dest
                Case 1 'not phone fax mail tax
                    txtdestination_Check2.Text = txtdestination_company.Text + " " + txtdestination_address.Text + " " + txtdestination_province.Text _
                     + " " + txtdest_Receive_country.Text + " ON BEHALF OF " + txtob_dest_address.Text

                Case 2 'phone fax email tax
                    txtdestination_Check2.Text = txtdestination_company.Text + " " + txtdestination_address.Text + " " + txtdestination_province.Text _
                    + " " + txtdest_Receive_country.Text + " TEL: " + txtdestination_phone.Text + " FAX: " + txtdestination_fax.Text _
                    + " TAX ID: " + txtdestination_taxid.Text + " E-mail: " + txtdestination_email.Text + " ON BEHALF OF " + txtob_dest_address.Text

                Case 3 'phone fax mail
                    txtdestination_Check2.Text = txtdestination_company.Text + " " + txtdestination_address.Text + " " + txtdestination_province.Text _
                     + " " + txtdest_Receive_country.Text + " TEL: " + txtdestination_phone.Text + " FAX: " + txtdestination_fax.Text _
                     + " E-mail: " + txtdestination_email.Text + " ON BEHALF OF " + txtob_dest_address.Text

                Case 4 'phone fax tax
                    txtdestination_Check2.Text = txtdestination_company.Text + " " + txtdestination_address.Text + " " + txtdestination_province.Text _
                    + " " + txtdest_Receive_country.Text + " TEL: " + txtdestination_phone.Text + " FAX: " + txtdestination_fax.Text _
                    + " TAX ID: " + txtdestination_taxid.Text + " ON BEHALF OF " + txtob_dest_address.Text

                Case 5 'phone fax 
                    txtdestination_Check2.Text = txtdestination_company.Text + " " + txtdestination_address.Text + " " + txtdestination_province.Text _
                    + " " + txtdest_Receive_country.Text + " TEL: " + txtdestination_phone.Text + " FAX: " + txtdestination_fax.Text _
                    + " ON BEHALF OF " + txtob_dest_address.Text

                Case 6 'phone
                    txtdestination_Check2.Text = txtdestination_company.Text + " " + txtdestination_address.Text + " " + txtdestination_province.Text _
                    + " " + txtdest_Receive_country.Text + " TEL: " + txtdestination_phone.Text + " ON BEHALF OF " + txtob_dest_address.Text

                Case 7 'phone mail tax
                    txtdestination_Check2.Text = txtdestination_company.Text + " " + txtdestination_address.Text + " " + txtdestination_province.Text _
                    + " " + txtdest_Receive_country.Text + " TEL: " + txtdestination_phone.Text _
                    + " TAX ID: " + txtdestination_taxid.Text + " E-mail: " + txtdestination_email.Text + " ON BEHALF OF " + txtob_dest_address.Text

                Case 8 'phone tax
                    txtdestination_Check2.Text = txtdestination_company.Text + " " + txtdestination_address.Text + " " + txtdestination_province.Text _
                    + " " + txtdest_Receive_country.Text + " TEL: " + txtdestination_phone.Text _
                    + " TAX ID: " + txtdestination_taxid.Text + " ON BEHALF OF " + txtob_dest_address.Text

                Case 9 'phone mail
                    txtdestination_Check2.Text = txtdestination_company.Text + " " + txtdestination_address.Text + " " + txtdestination_province.Text _
                    + " " + txtdest_Receive_country.Text + " TEL: " + txtdestination_phone.Text _
                    + " E-mail: " + txtdestination_email.Text + " ON BEHALF OF " + txtob_dest_address.Text

                Case 10 'fax mail tax
                    txtdestination_Check2.Text = txtdestination_company.Text + " " + txtdestination_address.Text + " " + txtdestination_province.Text _
                    + " " + txtdest_Receive_country.Text + " FAX: " + txtdestination_fax.Text _
                    + " TAX ID: " + txtdestination_taxid.Text + " E-mail: " + txtdestination_email.Text + " ON BEHALF OF " + txtob_dest_address.Text

                Case 11 'fax mail
                    txtdestination_Check2.Text = txtdestination_company.Text + " " + txtdestination_address.Text + " " + txtdestination_province.Text _
                    + " " + txtdest_Receive_country.Text + " FAX: " + txtdestination_fax.Text _
                    + " E-mail: " + txtdestination_email.Text + " ON BEHALF OF " + txtob_dest_address.Text

                Case 12 'fax tax
                    txtdestination_Check2.Text = txtdestination_company.Text + " " + txtdestination_address.Text + " " + txtdestination_province.Text _
                    + " " + txtdest_Receive_country.Text + " FAX: " + txtdestination_fax.Text _
                    + " TAX ID: " + txtdestination_taxid.Text + " ON BEHALF OF " + txtob_dest_address.Text

                Case 13 'fax
                    txtdestination_Check2.Text = txtdestination_company.Text + " " + txtdestination_address.Text + " " + txtdestination_province.Text _
                    + " " + txtdest_Receive_country.Text + " FAX: " + txtdestination_fax.Text + " ON BEHALF OF " + txtob_dest_address.Text

                Case 14 'mail tax
                    txtdestination_Check2.Text = txtdestination_company.Text + " " + txtdestination_address.Text + " " + txtdestination_province.Text _
                    + " " + txtdest_Receive_country.Text + " TAX ID: " + txtdestination_taxid.Text + " E-mail: " + txtdestination_email.Text + " ON BEHALF OF " + txtob_dest_address.Text

                Case 15 'mail
                    txtdestination_Check2.Text = txtdestination_company.Text + " " + txtdestination_address.Text + " " + txtdestination_province.Text _
                    + " " + txtdest_Receive_country.Text + " E-mail: " + txtdestination_email.Text + " ON BEHALF OF " + txtob_dest_address.Text

                Case 16 'tax
                    txtdestination_Check2.Text = txtdestination_company.Text + " " + txtdestination_address.Text + " " + txtdestination_province.Text _
                    + " " + txtdest_Receive_country.Text + " TAX ID: " + txtdestination_taxid.Text + " ON BEHALF OF " + txtob_dest_address.Text

                Case Else
                    txtdestination_Check2.Text = txtdestination_company.Text + " " + txtdestination_address.Text + " " + txtdestination_province.Text _
                    + " " + txtdest_Receive_country.Text + " ON BEHALF OF " + txtob_dest_address.Text

            End Select
        ElseIf Mid(txtdest_remark1.Text, 2, 1) <> "/" Then
            Select Case V_Com_dest
                Case 1 'not phone fax mail tax
                    txtdestination_Check2.Text = txtdestination_company.Text + " " + txtdestination_address.Text + " " + txtdestination_province.Text _
                    + " " + txtdest_Receive_country.Text

                Case 2 'phone fax email tax
                    txtdestination_Check2.Text = txtdestination_company.Text + " " + txtdestination_address.Text + " " + txtdestination_province.Text _
                    + " " + txtdest_Receive_country.Text + " TEL: " + txtdestination_phone.Text + " FAX: " + txtdestination_fax.Text _
                    + " TAX ID: " + txtdestination_taxid.Text + " E-mail: " + txtdestination_email.Text

                Case 3 'phone fax mail
                    txtdestination_Check2.Text = txtdestination_company.Text + " " + txtdestination_address.Text + " " + txtdestination_province.Text _
                    + " " + txtdest_Receive_country.Text + " TEL: " + txtdestination_phone.Text + " FAX: " + txtdestination_fax.Text _
                    + " E-mail: " + txtdestination_email.Text

                Case 4 'phone fax tax
                    txtdestination_Check2.Text = txtdestination_company.Text + " " + txtdestination_address.Text + " " + txtdestination_province.Text _
                    + " " + txtdest_Receive_country.Text + " TEL: " + txtdestination_phone.Text + " FAX: " + txtdestination_fax.Text _
                    + " TAX ID: " + txtdestination_taxid.Text


                Case 5 'phone fax 
                    txtdestination_Check2.Text = txtdestination_company.Text + " " + txtdestination_address.Text + " " + txtdestination_province.Text _
                    + " " + txtdest_Receive_country.Text + " TEL: " + txtdestination_phone.Text + " FAX: " + txtdestination_fax.Text

                Case 6 'phone
                    txtdestination_Check2.Text = txtdestination_company.Text + " " + txtdestination_address.Text + " " + txtdestination_province.Text _
                    + " " + txtdest_Receive_country.Text + " TEL: " + txtdestination_phone.Text

                Case 7 'phone mail tax
                    txtdestination_Check2.Text = txtdestination_company.Text + " " + txtdestination_address.Text + " " + txtdestination_province.Text _
                    + " " + txtdest_Receive_country.Text + " TEL: " + txtdestination_phone.Text _
                    + " TAX ID: " + txtdestination_taxid.Text + " E-mail: " + txtdestination_email.Text

                Case 8 'phone tax
                    txtdestination_Check2.Text = txtdestination_company.Text + " " + txtdestination_address.Text + " " + txtdestination_province.Text _
                     + " " + txtdest_Receive_country.Text + " TEL: " + txtdestination_phone.Text + " TAX ID: " + txtdestination_taxid.Text

                Case 9 'phone mail
                    txtdestination_Check2.Text = txtdestination_company.Text + " " + txtdestination_address.Text + " " + txtdestination_province.Text _
                     + " " + txtdest_Receive_country.Text + " TEL: " + txtdestination_phone.Text + " E-mail: " + txtdestination_email.Text

                Case 10 'fax mail tax
                    txtdestination_Check2.Text = txtdestination_company.Text + " " + txtdestination_address.Text + " " + txtdestination_province.Text _
                     + " " + txtdest_Receive_country.Text + " FAX: " + txtdestination_fax.Text _
                     + " TAX ID: " + txtdestination_taxid.Text + " E-mail: " + txtdestination_email.Text

                Case 11 'fax mail
                    txtdestination_Check2.Text = txtdestination_company.Text + " " + txtdestination_address.Text + " " + txtdestination_province.Text _
                    + " " + txtdest_Receive_country.Text + " FAX: " + txtdestination_fax.Text + " E-mail: " + txtdestination_email.Text

                Case 12 'fax tax
                    txtdestination_Check2.Text = txtdestination_company.Text + " " + txtdestination_address.Text + " " + txtdestination_province.Text _
                    + " " + txtdest_Receive_country.Text + " FAX: " + txtdestination_fax.Text + " TAX ID: " + txtdestination_taxid.Text

                Case 13 'fax
                    txtdestination_Check2.Text = txtdestination_company.Text + " " + txtdestination_address.Text + " " + txtdestination_province.Text _
                    + " " + txtdest_Receive_country.Text + " FAX: " + txtdestination_fax.Text

                Case 14 'mail tax
                    txtdestination_Check2.Text = txtdestination_company.Text + " " + txtdestination_address.Text + " " + txtdestination_province.Text _
                     + " " + txtdest_Receive_country.Text + " TAX ID: " + txtdestination_taxid.Text + " E-mail: " + txtdestination_email.Text

                Case 15 'mail
                    txtdestination_Check2.Text = txtdestination_company.Text + " " + txtdestination_address.Text + " " + txtdestination_province.Text _
                     + " " + txtdest_Receive_country.Text + " E-mail: " + txtdestination_email.Text

                Case 16 'tax
                    txtdestination_Check2.Text = txtdestination_company.Text + " " + txtdestination_address.Text + " " + txtdestination_province.Text _
                    + " " + txtdest_Receive_country.Text + " TAX ID: " + txtdestination_taxid.Text

                Case Else
                    txtdestination_Check2.Text = txtdestination_company.Text + " " + txtdestination_address.Text + " " + txtdestination_province.Text _
                     + " " + txtdest_Receive_country.Text

            End Select
        End If
    End Sub
    Dim TFOB As Decimal
    Dim TNet As Decimal
    Dim str_mark As String
    Private Sub Detail1_Format(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Detail1.Format
        Cpage += 1
        'txtPage2.Text = Cpage
        TFOB += txtFOB_AMT.Value
        TNet += txtnet_weight.Value

        txtNumRowCount.Text = Cpage
        Count_RowToPage_DetailData()

        If Cpage = 1 Then
            str_mark = CommonUtility.Get_StringValue(txtmarks.Text)
        End If
        '===================================================
        'box8 ตัวเก่าก่อนเปลี่ยนเป็นอันเดิม
        'txtTemp_box8.Text = Check_BOX8(CommonUtility.Get_StringValue(txtbox8.Value), Check_Letter(CommonUtility.Get_StringValue(txtletter.Value)), CommonUtility.Get_StringValue(txtprofit_per_unit.Value))
        txtTemp_box8.Text = Check_BOX8_Ole(CommonUtility.Get_StringValue(txtbox8.Value))
    End Sub

    Function NetWeight_(ByVal _netweight, ByVal _uni) As String
        Dim sumStr As String
        sumStr = Format(_netweight, "#,##0.00##") & " " & _uni

        Return sumStr
    End Function
    'แบบไม่มี invoice ต่าางประเทศ
    Sub CHeck_DisPlay()

        Dim sss As String = txtgross_weight.Text

        Dim str_gross As String

        'str_gross = txtgross_weight.Text
        'ไม่มี invoice ต่างประเทศ
        If Mid(CommonUtility.Get_StringValue(txtFOBDisplay.Value), 1, 50) = "Display" Then

            If Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "GROSS WEIGHT - NET WEIGHT" Then
                str_gross = GrossTxt() & vbNewLine & FobTxt() & vbNewLine & NetWeight_(txtnet_weight.Value, txtunit_code2.Value)
                Select Case C_TotalRowDe.Text
                    Case 1
                        txtGrossTxt.Text = str_gross
                    Case Else
                        If Cpage > 1 Then
                            str_gross = ""
                            txtGrossTxt.Text = str_gross '& vbNewLine & str_USDInvoice
                        ElseIf Cpage = 1 Then 'แถวแรกต้องมีข้อมูล

                            txtGrossTxt.Text = str_gross
                        End If
                End Select
            ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "GROSS WEIGHT" Then
                str_gross = GrossTxt() & vbNewLine & FobTxt()
                Select Case C_TotalRowDe.Text
                    Case 1
                        txtGrossTxt.Text = str_gross
                    Case Else
                        If Cpage > 1 Then
                            str_gross = ""
                            txtGrossTxt.Text = str_gross '& vbNewLine & str_USDInvoice
                        ElseIf Cpage = 1 Then 'แถวแรกต้องมีข้อมูล

                            txtGrossTxt.Text = str_gross
                        End If
                End Select
            ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "" Then
                str_gross = GrossTxt() & vbNewLine & FobTxt()
                Select Case C_TotalRowDe.Text
                    Case 1
                        txtGrossTxt.Text = str_gross
                    Case Else
                        If Cpage > 1 Then
                            str_gross = ""
                            txtGrossTxt.Text = str_gross '& vbNewLine & str_USDInvoice
                        ElseIf Cpage = 1 Then 'แถวแรกต้องมีข้อมูล

                            txtGrossTxt.Text = str_gross
                        End If
                End Select
            End If
        End If
    End Sub
    Function CHeck_DisPlayF() As String
        Dim str_GrossF As String
        Dim sss As String = txtGross_Weight.Text

        Dim str_gross As String

        'str_gross = txtgross_weight.Text
        'ไม่มี invoice ต่างประเทศ
        If Mid(CommonUtility.Get_StringValue(txtFOBDisplay.Value), 1, 50) = "Display" Then

            If Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "GROSS WEIGHT - NET WEIGHT" Then
                str_gross = "G.W." & vbNewLine & GrossTxt() & vbNewLine & FobTxtAllSum() & vbNewLine & "N.W." & vbNewLine & NetWeight_(TNet, txtunit_code2.Value)
                str_GrossF = str_gross
            ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "GROSS WEIGHT" Then
                str_gross = "G.W." & vbNewLine & GrossTxt() & vbNewLine & FobTxtAllSum()
                str_GrossF = str_gross
            ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "" Then
                str_gross = "G.W." & vbNewLine & GrossTxt() & vbNewLine & FobTxtAllSum()
                str_GrossF = str_gross
            End If
        Else
            'ระบบเก่า
            str_gross = "G.W." & vbNewLine & GrossTxt() & vbNewLine & FobTxtAllSum() 'FobTxtAll()
            str_GrossF = str_gross
        End If
        Return str_GrossF
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

        If (CommonUtility.Get_StringValue(txtquantity1.Text) <> "" And CommonUtility.Get_Decimal(txtquantity1.Text) <> 0 And CommonUtility.Get_StringValue(txtq_unit_code1.Text) <> "") = True And _
        (CommonUtility.Get_StringValue(txtquantity2.Text) <> "" And CommonUtility.Get_Decimal(txtquantity2.Text) <> 0 And CommonUtility.Get_StringValue(txtq_unit_code2.Text) <> "") = True And _
        (CommonUtility.Get_StringValue(txtquantity3.Text) <> "" And CommonUtility.Get_Decimal(txtquantity3.Text) <> 0 And CommonUtility.Get_StringValue(txtq_unit_code3.Text) <> "") = True And _
        (CommonUtility.Get_StringValue(txtquantity4.Text) <> "" And CommonUtility.Get_Decimal(txtquantity4.Text) <> 0 And CommonUtility.Get_StringValue(txtq_unit_code4.Text) <> "") = True And _
        (CommonUtility.Get_StringValue(txtquantity5.Text) <> "" And CommonUtility.Get_Decimal(txtquantity5.Text) <> 0 And CommonUtility.Get_StringValue(txtq_unit_code5.Text) <> "") = True Then
            'มีหมด
            All_CheckThird_5(Cpage)
            'If CommonUtility.Get_StringValue(txtthird_country.Value) <> "" Then
            '    txtT_product.Text = "HS. CODE. " + tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) _
            '                            + txtproduct_n1.Text + txtproduct_n2.Text + "****" + vbCrLf + _
            '                            "TOTAL: " + BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) + vbCrLf + "        " + _
            '                            BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) + vbCrLf + "        " + _
            '                            BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) + vbCrLf + "        " + _
            '                            BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) + vbCrLf + "        " + _
            '                            BahtOnly(txtquantity5.Text, txtq_unit_code5.Text) + vbCrLf + txtthird_country.Text + vbCrLf + _
            '                            "_____________________________"
            'Else
            '    txtT_product.Text = "HS. CODE. " + tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) _
            '                            + txtproduct_n1.Text + txtproduct_n2.Text + "****" + vbCrLf + _
            '                            "TOTAL: " + BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) + vbCrLf + "        " + _
            '                            BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) + vbCrLf + "        " + _
            '                            BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) + vbCrLf + "        " + _
            '                            BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) + vbCrLf + "        " + _
            '                            BahtOnly(txtquantity5.Text, txtq_unit_code5.Text) + vbCrLf + _
            '                            "_____________________________"
            'End If
        ElseIf (CommonUtility.Get_StringValue(txtquantity1.Text) <> "" And CommonUtility.Get_Decimal(txtquantity1.Text) <> 0 And CommonUtility.Get_StringValue(txtq_unit_code1.Text) <> "") = True And _
        (CommonUtility.Get_StringValue(txtquantity2.Text) <> "" And CommonUtility.Get_Decimal(txtquantity2.Text) <> 0 And CommonUtility.Get_StringValue(txtq_unit_code2.Text) <> "") = True And _
        (CommonUtility.Get_StringValue(txtquantity3.Text) <> "" And CommonUtility.Get_Decimal(txtquantity3.Text) <> 0 And CommonUtility.Get_StringValue(txtq_unit_code3.Text) <> "") = True And _
        (CommonUtility.Get_StringValue(txtquantity4.Text) <> "" And CommonUtility.Get_Decimal(txtquantity4.Text) <> 0 And CommonUtility.Get_StringValue(txtq_unit_code4.Text) <> "") = True And _
        (CommonUtility.Get_StringValue(txtquantity5.Text) <> "" And CommonUtility.Get_Decimal(txtquantity5.Text) <> 0 And CommonUtility.Get_StringValue(txtq_unit_code5.Text) <> "") = False Then
            'ขาด 5
            All_CheckThird_4(Cpage)
            'If CommonUtility.Get_StringValue(txtthird_country.Value) <> "" Then
            '    txtT_product.Text = "HS. CODE. " + tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) _
            '                            + txtproduct_n1.Text + txtproduct_n2.Text + "****" + vbCrLf + _
            '                            "TOTAL: " + BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) + vbCrLf + "        " + _
            '                            BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) + vbCrLf + "        " + _
            '                            BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) + vbCrLf + "        " + _
            '                            BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) + vbCrLf + txtthird_country.Text + vbCrLf + _
            '                            "_____________________________"
            'Else
            '    txtT_product.Text = "HS. CODE. " + tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) _
            '                            + txtproduct_n1.Text + txtproduct_n2.Text + "****" + vbCrLf + _
            '                            "TOTAL: " + BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) + vbCrLf + "        " + _
            '                            BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) + vbCrLf + "        " + _
            '                            BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) + vbCrLf + "        " + _
            '                            BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) + vbCrLf + _
            '                            "_____________________________"
            'End If


        ElseIf (CommonUtility.Get_StringValue(txtquantity1.Text) <> "" And CommonUtility.Get_Decimal(txtquantity1.Text) <> 0 And CommonUtility.Get_StringValue(txtq_unit_code1.Text) <> "") = True And _
         (CommonUtility.Get_StringValue(txtquantity2.Text) <> "" And CommonUtility.Get_Decimal(txtquantity2.Text) <> 0 And CommonUtility.Get_StringValue(txtq_unit_code2.Text) <> "") = True And _
         (CommonUtility.Get_StringValue(txtquantity3.Text) <> "" And CommonUtility.Get_Decimal(txtquantity3.Text) <> 0 And CommonUtility.Get_StringValue(txtq_unit_code3.Text) <> "") = True And _
         (CommonUtility.Get_StringValue(txtquantity4.Text) <> "" And CommonUtility.Get_Decimal(txtquantity4.Text) <> 0 And CommonUtility.Get_StringValue(txtq_unit_code4.Text) <> "") = False And _
         (CommonUtility.Get_StringValue(txtquantity5.Text) <> "" And CommonUtility.Get_Decimal(txtquantity5.Text) <> 0 And CommonUtility.Get_StringValue(txtq_unit_code5.Text) <> "") = False Then
            'ขาด 4,5
            All_CheckThird_3(Cpage)
            'If CommonUtility.Get_StringValue(txtthird_country.Value) <> "" Then
            '    txtT_product.Text = "HS. CODE. " + tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) _
            '                            + txtproduct_n1.Text + txtproduct_n2.Text + "****" + vbCrLf + _
            '                            "TOTAL: " + BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) + vbCrLf + "        " + _
            '                            BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) + vbCrLf + "        " + _
            '                            BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) + vbCrLf + txtthird_country.Text + vbCrLf + _
            '                            "_____________________________"
            'Else
            '    txtT_product.Text = "HS. CODE. " + tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) _
            '                            + txtproduct_n1.Text + txtproduct_n2.Text + "****" + vbCrLf + _
            '                            "TOTAL: " + BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) + vbCrLf + "        " + _
            '                            BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) + vbCrLf + "        " + _
            '                            BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) + vbCrLf + _
            '                            "_____________________________"
            'End If

        ElseIf (CommonUtility.Get_StringValue(txtquantity1.Text) <> "" And CommonUtility.Get_Decimal(txtquantity1.Text) <> 0 And CommonUtility.Get_StringValue(txtq_unit_code1.Text) <> "") = True And _
          (CommonUtility.Get_StringValue(txtquantity2.Text) <> "" And CommonUtility.Get_Decimal(txtquantity2.Text) <> 0 And CommonUtility.Get_StringValue(txtq_unit_code2.Text) <> "") = True And _
          (CommonUtility.Get_StringValue(txtquantity3.Text) <> "" And CommonUtility.Get_Decimal(txtquantity3.Text) <> 0 And CommonUtility.Get_StringValue(txtq_unit_code3.Text) <> "") = False And _
          (CommonUtility.Get_StringValue(txtquantity4.Text) <> "" And CommonUtility.Get_Decimal(txtquantity4.Text) <> 0 And CommonUtility.Get_StringValue(txtq_unit_code4.Text) <> "") = False And _
          (CommonUtility.Get_StringValue(txtquantity5.Text) <> "" And CommonUtility.Get_Decimal(txtquantity5.Text) <> 0 And CommonUtility.Get_StringValue(txtq_unit_code5.Text) <> "") = False Then
            'ขาด 3,4,5
            All_CheckThird_2(Cpage)
            'If CommonUtility.Get_StringValue(txtthird_country.Value) <> "" Then
            '    txtT_product.Text = "HS. CODE. " + tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) _
            '                            + txtproduct_n1.Text + txtproduct_n2.Text + "****" + vbCrLf + _
            '                            "TOTAL: " + BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) + vbCrLf + "        " + _
            '                            BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) + vbCrLf + txtthird_country.Text + vbCrLf + _
            '                            "_____________________________"

            'Else
            '    txtT_product.Text = "HS. CODE. " + tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) _
            '                            + txtproduct_n1.Text + txtproduct_n2.Text + "****" + vbCrLf + _
            '                            "TOTAL: " + BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) + vbCrLf + "        " + _
            '                            BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) + vbCrLf + _
            '                            "_____________________________"

            'End If

        ElseIf (CommonUtility.Get_StringValue(txtquantity1.Text) <> "" And CommonUtility.Get_Decimal(txtquantity1.Text) <> 0 And CommonUtility.Get_StringValue(txtq_unit_code1.Text) <> "") = True And _
          (CommonUtility.Get_StringValue(txtquantity2.Text) <> "" And CommonUtility.Get_Decimal(txtquantity2.Text) <> 0 And CommonUtility.Get_StringValue(txtq_unit_code2.Text) <> "") = False And _
          (CommonUtility.Get_StringValue(txtquantity3.Text) <> "" And CommonUtility.Get_Decimal(txtquantity3.Text) <> 0 And CommonUtility.Get_StringValue(txtq_unit_code3.Text) <> "") = False And _
          (CommonUtility.Get_StringValue(txtquantity4.Text) <> "" And CommonUtility.Get_Decimal(txtquantity4.Text) <> 0 And CommonUtility.Get_StringValue(txtq_unit_code4.Text) <> "") = False And _
          (CommonUtility.Get_StringValue(txtquantity5.Text) <> "" And CommonUtility.Get_Decimal(txtquantity5.Text) <> 0 And CommonUtility.Get_StringValue(txtq_unit_code5.Text) <> "") = False Then
            'ขาด 2,3,4,5
            All_CheckThird_1(Cpage)
            'If CommonUtility.Get_StringValue(txtthird_country.Value) <> "" Then
            '    txtT_product.Text = "HS. CODE. " + tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) _
            '                            + txtproduct_n1.Text + txtproduct_n2.Text + "****" + vbCrLf + _
            '                            "TOTAL: " + BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) + vbCrLf + txtthird_country.Text + vbCrLf + _
            '                            "_____________________________"
            'Else
            '    txtT_product.Text = "HS. CODE. " + tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) _
            '                            + txtproduct_n1.Text + txtproduct_n2.Text + "****" + vbCrLf + _
            '                            "TOTAL: " + BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) + vbCrLf + _
            '                            "_____________________________"
            'End If

        Else
            All_CheckThird_1(Cpage)
            'If CommonUtility.Get_StringValue(txtthird_country.Value) <> "" Then
            '    txtT_product.Text = "HS. CODE. " + tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) _
            '                                                    + txtproduct_n1.Text + txtproduct_n2.Text + "****" + vbCrLf + txtthird_country.Text + vbCrLf + _
            '                            "_____________________________"
            'Else
            '    txtT_product.Text = "HS. CODE. " + tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) _
            '                                                    + txtproduct_n1.Text + txtproduct_n2.Text + "****" + vbCrLf + _
            '                            "_____________________________"
            'End If

        End If

        'Else
        'txtT_product.Text = "HS. CODE. " + tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) + vbCrLf _
        '+ txtproduct_n1.Text + txtproduct_n2.Text + "****"
        'End If

        '============================================================
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

    Function Check_BOX8(ByVal _Box8Str As String, ByVal _LetterStr As String, ByVal _profit_per As String) As String
        Dim str_text As String
        Select Case Mid(_Box8Str, 2, 3)
            Case "RVC"
                str_text = """RVC" & " " & _profit_per & " %""" & " " & _LetterStr
            Case Else
                str_text = _Box8Str
        End Select
        Return str_text
    End Function
    Function Check_BOX8_Ole(ByVal _Box8Str As String) As String
        Dim str_text As String
        str_text = _Box8Str
        Return str_text
    End Function
    'check letter
    Function Check_Letter(ByVal str_Letter As String) As String
        Dim f_letter As String
        Select Case str_Letter
            Case "SING" 'SINGLE
                f_letter = "SINGLE"
            Case "COUN" 'COUNTRY
                f_letter = "COUNTRY"
            Case "CONT" 'CONTENT
                f_letter = "CONTENT"
            Case Else
                f_letter = ""
        End Select
        Return f_letter
    End Function

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
    Function FobTxtAll() As String
        Dim Str_FobTxt As String
        Dim Dec_Fob As Decimal

        If txtFOB_AMT.Value > 0 Then
            Dec_Fob = CDec(Check_Null(CommonUtility.Get_StringValue(TFOB))) 'Format(CDec(Check_Null(CommonUtility.Get_StringValue(txtFOB_AMT.Value))), "#,##0.0###")
            Str_FobTxt = Format(Dec_Fob, "#,##0.00##") & " USD"
        Else
            Str_FobTxt = ""
        End If
        Return Str_FobTxt
    End Function

    'Sub CallInvoiceCheck() 'Check Invoince
    '    If CommonUtility.Get_StringValue(txtinvoice_no1.Text) <> "" And CommonUtility.Get_StringValue(txtinvoice_date1.Text) <> "" Then
    '        txtTolInvoice.Text = txtinvoice_no1.Text & vbNewLine & CDate(txtinvoice_date1.Text).Date
    '    ElseIf CommonUtility.Get_StringValue(txtinvoice_no2.Text) <> "" And CommonUtility.Get_StringValue(txtinvoice_date2.Text) <> "" Then
    '        txtTolInvoice.Text = txtinvoice_no1.Text & vbNewLine & CDate(txtinvoice_date1.Text).Date & vbNewLine _
    '                            + txtinvoice_no2.Text & vbNewLine & CDate(txtinvoice_date2.Text).Date
    '    ElseIf CommonUtility.Get_StringValue(txtinvoice_no3.Text) <> "" And CommonUtility.Get_StringValue(txtinvoice_date3.Text) <> "" Then
    '        txtTolInvoice.Text = txtinvoice_no1.Text & vbNewLine & CDate(txtinvoice_date1.Text).Date & vbNewLine _
    '                            + txtinvoice_no2.Text & vbNewLine & CDate(txtinvoice_date2.Text).Date & vbNewLine _
    '                            + txtinvoice_no3.Text & vbNewLine & CDate(txtinvoice_date3.Text).Date
    '    ElseIf CommonUtility.Get_StringValue(txtinvoice_no4.Text) <> "" And CommonUtility.Get_StringValue(txtinvoice_date4.Text) <> "" Then
    '        txtTolInvoice.Text = txtinvoice_no1.Text & vbNewLine & CDate(txtinvoice_date1.Text).Date & vbNewLine _
    '                            + txtinvoice_no2.Text & vbNewLine & CDate(txtinvoice_date2.Text).Date & vbNewLine _
    '                            + txtinvoice_no3.Text & vbNewLine & CDate(txtinvoice_date3.Text).Date & vbNewLine _
    '                            + txtinvoice_no4.Text & vbNewLine & CDate(txtinvoice_date4.Text).Date
    '    ElseIf CommonUtility.Get_StringValue(txtinvoice_no5.Text) <> "" And CommonUtility.Get_StringValue(txtinvoice_date5.Text) <> "" Then
    '        txtTolInvoice.Text = txtinvoice_no1.Text & vbNewLine & CDate(txtinvoice_date1.Text).Date & vbNewLine _
    '                            + txtinvoice_no2.Text & vbNewLine & CDate(txtinvoice_date2.Text).Date & vbNewLine _
    '                            + txtinvoice_no3.Text & vbNewLine & CDate(txtinvoice_date3.Text).Date & vbNewLine _
    '                            + txtinvoice_no4.Text & vbNewLine & CDate(txtinvoice_date4.Text).Date & vbNewLine _
    '                            + txtinvoice_no5.Text & vbNewLine & CDate(txtinvoice_date5.Text).Date
    '    End If

    'End Sub

    Private Sub PageFooter1_Format(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PageFooter1.Format
        txtcompany_provincefoot.Text = txtcompany_provincefoot1.Text & " " & format_dateSelect()

    End Sub

    'Function con_date(ByVal str_date As Date) As Date
    '    Dim f_date As Date = Format(str_date, "d/MM/yyyy")

    '    Return f_date
    'End Function

    'Sub CallInvoiceCheck() 'Check Invoince 
    '    If CommonUtility.Get_StringValue(txtinvoice_no1.Text) <> "" And CommonUtility.Get_StringValue(txtinvoice_date1.Text) <> "" Then
    '        txtTolInvoice.Text = txtinvoice_no1.Text & vbNewLine & CDate(con_date(txtinvoice_date1.Value)).Date
    '    ElseIf CommonUtility.Get_StringValue(txtinvoice_no2.Text) <> "" And CommonUtility.Get_StringValue(txtinvoice_date2.Text) <> "" Then
    '        txtTolInvoice.Text = txtinvoice_no1.Text & vbNewLine & CDate(con_date(txtinvoice_date1.Value)).Date & vbNewLine _
    '                            + txtinvoice_no2.Text & vbNewLine & CDate(con_date(txtinvoice_date2.Value)).Date
    '    ElseIf CommonUtility.Get_StringValue(txtinvoice_no3.Text) <> "" And CommonUtility.Get_StringValue(txtinvoice_date3.Text) <> "" Then
    '        txtTolInvoice.Text = txtinvoice_no1.Text & vbNewLine & CDate(con_date(txtinvoice_date1.Value)).Date & vbNewLine _
    '                            + txtinvoice_no2.Text & vbNewLine & CDate(con_date(txtinvoice_date2.Value)).Date & vbNewLine _
    '                            + txtinvoice_no3.Text & vbNewLine & CDate(con_date(txtinvoice_date3.Value)).Date
    '    ElseIf CommonUtility.Get_StringValue(txtinvoice_no4.Text) <> "" And CommonUtility.Get_StringValue(txtinvoice_date4.Text) <> "" Then
    '        txtTolInvoice.Text = txtinvoice_no1.Text & vbNewLine & CDate(con_date(txtinvoice_date1.Value)).Date & vbNewLine _
    '                            + txtinvoice_no2.Text & vbNewLine & CDate(con_date(txtinvoice_date2.Value)).Date & vbNewLine _
    '                            + txtinvoice_no3.Text & vbNewLine & CDate(con_date(txtinvoice_date3.Value)).Date & vbNewLine _
    '                            + txtinvoice_no4.Text & vbNewLine & CDate(con_date(txtinvoice_date4.Value)).Date
    '    ElseIf CommonUtility.Get_StringValue(txtinvoice_no5.Text) <> "" And CommonUtility.Get_StringValue(txtinvoice_date5.Text) <> "" Then
    '        txtTolInvoice.Text = txtinvoice_no1.Text & vbNewLine & CDate(con_date(txtinvoice_date1.Value)).Date & vbNewLine _
    '                            + txtinvoice_no2.Text & vbNewLine & CDate(con_date(txtinvoice_date2.Value)).Date & vbNewLine _
    '                            + txtinvoice_no3.Text & vbNewLine & CDate(con_date(txtinvoice_date3.Value)).Date & vbNewLine _
    '                            + txtinvoice_no4.Text & vbNewLine & CDate(con_date(txtinvoice_date4.Value)).Date & vbNewLine _
    '                            + txtinvoice_no5.Text & vbNewLine & CDate(con_date(txtinvoice_date5.Value)).Date
    '    End If

    'End Sub

    Function confor_mat(ByVal d_date As Date) As String
        Dim str_for As String = Format(d_date, "d/MM/yyyy")
        Return str_for
    End Function
    Sub CallInvoiceCheck() 'Check Invoince
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
    End Sub
    Function CallInvoiceCheckF() As String 'Check Invoince
        Dim str_invoice As String
        If CommonUtility.Get_String(txtinvoice_no1.Text) <> "" And CommonUtility.Get_String(txtinvoice_date1.Text) <> "" And CommonUtility.Get_String(txtinvoice_no2.Text) = "" And CommonUtility.Get_String(txtinvoice_date2.Text) = "" And CommonUtility.Get_String(txtinvoice_no3.Text) = "" And CommonUtility.Get_String(txtinvoice_date3.Text) = "" And CommonUtility.Get_String(txtinvoice_no4.Text) = "" And CommonUtility.Get_String(txtinvoice_date4.Text) = "" And CommonUtility.Get_String(txtinvoice_no5.Text) = "" And CommonUtility.Get_String(txtinvoice_date5.Text) = "" Then
            '1 invoice
            str_invoice = txtinvoice_no1.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date1.Text) <> "", confor_mat(CDate(txtinvoice_date1.Text).Date), Nothing)
        ElseIf CommonUtility.Get_String(txtinvoice_no1.Text) <> "" And CommonUtility.Get_String(txtinvoice_date1.Text) <> "" And CommonUtility.Get_String(txtinvoice_no2.Text) <> "" And CommonUtility.Get_String(txtinvoice_date2.Text) <> "" And CommonUtility.Get_String(txtinvoice_no3.Text) = "" And CommonUtility.Get_String(txtinvoice_date3.Text) = "" And CommonUtility.Get_String(txtinvoice_no4.Text) = "" And CommonUtility.Get_String(txtinvoice_date4.Text) = "" And CommonUtility.Get_String(txtinvoice_no5.Text) = "" And CommonUtility.Get_String(txtinvoice_date5.Text) = "" Then
            '2 invoice
            str_invoice = txtinvoice_no1.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date1.Text) <> "", confor_mat(CDate(txtinvoice_date1.Text).Date), Nothing) & vbNewLine _
                                & txtinvoice_no2.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date2.Text) <> "", confor_mat(CDate(txtinvoice_date2.Text).Date), Nothing)

        ElseIf CommonUtility.Get_String(txtinvoice_no1.Text) <> "" And CommonUtility.Get_String(txtinvoice_date1.Text) <> "" And CommonUtility.Get_String(txtinvoice_no2.Text) <> "" And CommonUtility.Get_String(txtinvoice_date2.Text) <> "" And CommonUtility.Get_String(txtinvoice_no3.Text) <> "" And CommonUtility.Get_String(txtinvoice_date3.Text) <> "" And CommonUtility.Get_String(txtinvoice_no4.Text) = "" And CommonUtility.Get_String(txtinvoice_date4.Text) = "" And CommonUtility.Get_String(txtinvoice_no5.Text) = "" And CommonUtility.Get_String(txtinvoice_date5.Text) = "" Then
            '3 invoice
            str_invoice = txtinvoice_no1.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date1.Text) <> "", confor_mat(CDate(txtinvoice_date1.Text).Date), Nothing) & vbNewLine _
                                & txtinvoice_no2.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date2.Text) <> "", confor_mat(CDate(txtinvoice_date2.Text).Date), Nothing) & vbNewLine _
                                & txtinvoice_no3.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date3.Text) <> "", confor_mat(CDate(txtinvoice_date3.Text).Date), Nothing)

        ElseIf CommonUtility.Get_String(txtinvoice_no1.Text) <> "" And CommonUtility.Get_String(txtinvoice_date1.Text) <> "" And CommonUtility.Get_String(txtinvoice_no2.Text) <> "" And CommonUtility.Get_String(txtinvoice_date2.Text) <> "" And CommonUtility.Get_String(txtinvoice_no3.Text) <> "" And CommonUtility.Get_String(txtinvoice_date3.Text) <> "" And CommonUtility.Get_String(txtinvoice_no4.Text) <> "" And CommonUtility.Get_String(txtinvoice_date4.Text) <> "" And CommonUtility.Get_String(txtinvoice_no5.Text) = "" And CommonUtility.Get_String(txtinvoice_date5.Text) = "" Then
            '4 invoice
            str_invoice = txtinvoice_no1.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date1.Text) <> "", confor_mat(CDate(txtinvoice_date1.Text).Date), Nothing) & vbNewLine _
                                & txtinvoice_no2.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date2.Text) <> "", confor_mat(CDate(txtinvoice_date2.Text).Date), Nothing) & vbNewLine _
                                & txtinvoice_no3.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date3.Text) <> "", confor_mat(CDate(txtinvoice_date3.Text).Date), Nothing) & vbNewLine _
                                & txtinvoice_no4.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date4.Text) <> "", confor_mat(CDate(txtinvoice_date4.Text).Date), Nothing)

        ElseIf CommonUtility.Get_String(txtinvoice_no1.Text) <> "" And CommonUtility.Get_String(txtinvoice_date1.Text) <> "" And CommonUtility.Get_String(txtinvoice_no2.Text) <> "" And CommonUtility.Get_String(txtinvoice_date2.Text) <> "" And CommonUtility.Get_String(txtinvoice_no3.Text) <> "" And CommonUtility.Get_String(txtinvoice_date3.Text) <> "" And CommonUtility.Get_String(txtinvoice_no4.Text) <> "" And CommonUtility.Get_String(txtinvoice_date4.Text) <> "" And CommonUtility.Get_String(txtinvoice_no5.Text) <> "" And CommonUtility.Get_String(txtinvoice_date5.Text) <> "" Then
            '5 invoice
            str_invoice = txtinvoice_no1.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date1.Text) <> "", confor_mat(CDate(txtinvoice_date1.Text).Date), Nothing) & vbNewLine _
                                & txtinvoice_no2.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date2.Text) <> "", confor_mat(CDate(txtinvoice_date2.Text).Date), Nothing) & vbNewLine _
                                & txtinvoice_no3.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date3.Text) <> "", confor_mat(CDate(txtinvoice_date3.Text).Date), Nothing) & vbNewLine _
                                & txtinvoice_no4.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date4.Text) <> "", confor_mat(CDate(txtinvoice_date4.Text).Date), Nothing) & vbNewLine _
                                & txtinvoice_no5.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date5.Text) <> "", confor_mat(CDate(txtinvoice_date5.Text).Date), Nothing)

        End If
        Return str_invoice
    End Function
    Function tariff_All(ByVal _tariff As String) As String
        Dim str_tariff As String
        str_tariff = Mid(_tariff, 1, 4) & "." & Mid(_tariff, 5, 2) & "." & Mid(_tariff, 7, 4)

        Return str_tariff
    End Function
    'Function tariff_All(ByVal _tariff As String) As String
    '    Dim str_tariff As String
    '    str_tariff = Mid(_tariff, 1, 4) & "." & Mid(_tariff, 5, 4)

    '    Return str_tariff
    'End Function

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
                All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                                            & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

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
                All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                                                            & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

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
                All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                                                & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & _
                     "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                    BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
                                                                    BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " & _
                                                                    BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf & "        " & _
                                                                    BahtOnly(txtquantity5.Text, txtq_unit_code5.Text) & vbCrLf & _
                                                                    txtplace_exibition.Text & vbCrLf & _
                                                                    "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 4 'ไม่มีเลย
                All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                                                & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

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
                All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                                                & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

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
                All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                                                & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & _
                     "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                    BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
                                                                    BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " & _
                                                                    BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf & "        " & _
                                                                    BahtOnly(txtquantity5.Text, txtq_unit_code5.Text) & vbCrLf & _
                                                                    txtplace_exibition.Text & vbCrLf & _
                                                                    "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 7 'T1_third_country,T2_back_country
                All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                                                & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

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
                All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                                                & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

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
                All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                                            & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

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
                All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                                            & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

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
                All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                                                            & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

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
                All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                                            & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & _
                    "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf & "        " & _
                                                                txtplace_exibition.Text & vbCrLf & _
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 4 'ไม่มีเลย
                All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                                            & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

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
                All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                                            & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

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
            Case 6 'T3_place_exibition
                All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                                            & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & _
                    "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf & "        " & _
                                                                txtplace_exibition.Text & vbCrLf & _
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 7 'T1_third_country,T2_back_country
                All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                                                & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

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
                All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                                                & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

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
                All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                                                & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

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
                All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                                            & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

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
                All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                                                            & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

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
                All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                                            & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & _
                    "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " & _
                                                                txtplace_exibition.Text & vbCrLf & _
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 4 'ไม่มีเลย
                All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                                            & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & _
                     "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & _
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 5 'T2_back_country
                All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                                            & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

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
                All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                                            & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & _
                    "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " & _
                                                                txtplace_exibition.Text & vbCrLf & _
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 7 'T1_third_country,T2_back_country
                All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                                            & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

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
                All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                                            & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

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
                All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                                            & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

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
                All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                                            & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

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
                All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                                                            & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

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
                All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                                            & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & _
                    "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
                                                                txtplace_exibition.Text & vbCrLf & _
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 4 'ไม่มีเลย
                All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                                            & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & _
                    "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & _
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 5 'T2_back_country
                All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                                            & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & _
                    "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & _
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 6 'T3_place_exibition
                All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                                            & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & _
                    "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
                                                                txtplace_exibition.Text & vbCrLf & _
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 7 'T1_third_country,T2_back_country
                All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                                            & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

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
                All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                                            & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

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
                All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                                            & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

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
                All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                                            & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

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
                All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                                                            & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

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
                All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                                            & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & _
                    "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                txtplace_exibition.Text & vbCrLf & _
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 4 'ไม่มีเลย
                All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                                            & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & _
                    "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & _
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 5 'T2_back_country
                All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                                            & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & _
                    "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & _
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 6 'T3_place_exibition
                All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                                            & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & _
                    "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                txtplace_exibition.Text & vbCrLf & _
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 7 'T1_third_country,T2_back_country
                All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                                            & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

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
                All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                                            & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

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
                All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                                            & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

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
                All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                                            & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

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
                All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                                                            & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & _
                    txtback_country.Text & vbCrLf & _
                                                                                txtplace_exibition.Text & vbCrLf & _
                                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 3 'T3_place_exibition
                All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                                            & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & _
                     txtplace_exibition.Text & vbCrLf & _
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 4 'ไม่มีเลย
                All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                                            & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & _
                     "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 5 'T2_back_country
                All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                                            & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & _
                    vbCrLf & "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 6 'T3_place_exibition
                All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                                            & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & _
                     txtplace_exibition.Text & vbCrLf & _
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 7 'T1_third_country,T2_back_country
                All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                                            & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & _
                    txtthird_country.Text & vbCrLf & _
                                                                txtback_country.Text & vbCrLf & _
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 8 'T1_third_country,T3_place_exibition
                All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                                            & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & _
                     txtthird_country.Text & vbCrLf & _
                                                                txtplace_exibition.Text & vbCrLf & _
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 9 'T1_third_country
                All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                                            & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & _
                    txtthird_country.Text & vbCrLf & _
                                                                    "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
        End Select
    End Sub

    Private Sub rpt3_ediFORM4_2_PageEnd(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PageEnd
        Dim f As New System.Drawing.Font("BrowalliaUPC", 11)

        Dim xm As Single = 6.81
        Dim ym As Single = 5.219
        'Dim width As Single = 6.0F
        Dim widthm As Single = 1.18
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
        Dim x_txtGrossTxt As Single = 6
        Dim y_txtGrossTxt As Single = 5.219
        'Dim width As Single = 6.0F
        Dim width_txtGrossTxt As Single = 0.79
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
        Dim x_txtmarks As Single = 0.74
        Dim y_txtmarks As Single = 5.219
        'Dim width As Single = 6.0F
        Dim width_txtmarks As Single = 1.01
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

    Private Sub rpt3_ediFORM4_2_ReportEnd(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.ReportEnd
        If updatePrintTotal(txtinvh_run_auto.Value, CStr(Me.Document.Pages.Count)) = True Then

        End If
    End Sub
    Private Sub rpt3_ediFORM4_2_ReportStart(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.ReportStart
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
