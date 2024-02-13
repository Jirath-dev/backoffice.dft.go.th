Imports DataDynamics.ActiveReports
Imports DataDynamics.ActiveReports.Document

Imports Microsoft.ApplicationBlocks.Data
Imports System.Data.SqlClient
Imports System.Data
Imports System.Text
Imports DFT.Dotnetnuke.ClassLibrary
Imports ReportPrintClass
Public Class rpt3_ediFORM4_3 
    Dim Cpage As Integer
    Private Sub PageHeader1_Format(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PageHeader1.Format
        '=================txtreference_code2_Temp==============================================
        txtreference_code2_Temp.Text = "I" + txtreference_code2.Text
        '===============================================================
        Cpage += 1
        txtPage2.Text = Cpage
        '===============================================================
        Head_Checkcompany()
        '===============================================================
        Head_Checkdestination()
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
    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        Me.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.Custom
        Me.PageSettings.PaperName = "Fanfold 210 x 305 mm"
        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub PageFooter1_Format(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PageFooter1.Format
        Dim str_datenow As String = CommonUtility.Get_StringValue(Now.Day) + "/" + CommonUtility.Get_StringValue(Now.Month) + "/" + CommonUtility.Get_StringValue(Now.Year)

        txtcompany_provincefoot.Text = txtcompany_provincefoot1.Text & " " & str_datenow
    End Sub

    Private Sub Detail1_Format(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Detail1.Format
        '===============================================================
        Count_RowToPage_DetailData()
        '===============================================================
        'txtGrossTxt_FobTxt.Text = CommonUtility.Get_StringValue(txtGross_Weight.Text) + " " + CommonUtility.Get_StringValue(txtg_Unit_Desc.Text) _
        '                            + vbNewLine + CommonUtility.Get_StringValue(txtFOB_AMT.Text) + " USD"
        '===================================================
        'CallInvoiceCheck()
        CHeck_DisPlay()
    End Sub
    Sub Count_RowToPage_DetailData()
        Dim CountData_T, CountData_T2 As Integer
        CountData_T = C_TotalRowDe.Text
        CountData_T2 = CountData_T / 7

        Dim i As Integer
        txtNumRowCount.Text += 1
        For i = 0 To CountData_T2
            Dim arrnum(i) As Integer

            arrnum(i) = i * 7 'ทำเพื่อเช็คจำนวนเปรียบเทียบ

            If (txtNumRowCount.Text = arrnum(i)) = True Then
                Me.Detail1.NewPage = NewPage.Before
                txtmarks.Visible = True
                'txtgross_weight1.Visible = True
                txtTolInvoice.Visible = True
                Exit For
            Else
                '===============================================================
                If CommonUtility.Get_StringValue(txtgross_weight.Text) <> "" Then
                    'txtgross_weight1.Text = txtgross_weight.Text + " " + txtg_unit_code.Text
                Else
                    'txtgross_weight1.Text = ""
                End If
                '============================================================
                CallInvoiceCheck()
                '============================================================
                If txtNumRowCount.Text = C_TotalRowDe.Text Then

                    If (CommonUtility.Get_StringValue(txtquantity1.Text) <> "" And CommonUtility.Get_StringValue(txtquantity1.Text) <> "0.0000" And CommonUtility.Get_StringValue(txtq_unit_code1.Text) <> "") = True And _
                    (CommonUtility.Get_StringValue(txtquantity2.Text) <> "" And CommonUtility.Get_StringValue(txtquantity2.Text) <> "0.0000" And CommonUtility.Get_StringValue(txtq_unit_code2.Text) <> "") = True And _
                    (CommonUtility.Get_StringValue(txtquantity3.Text) <> "" And CommonUtility.Get_StringValue(txtquantity3.Text) <> "0.0000" And CommonUtility.Get_StringValue(txtq_unit_code3.Text) <> "") = True And _
                    (CommonUtility.Get_StringValue(txtquantity4.Text) <> "" And CommonUtility.Get_StringValue(txtquantity4.Text) <> "0.0000" And CommonUtility.Get_StringValue(txtq_unit_code4.Text) <> "") = True And _
                    (CommonUtility.Get_StringValue(txtquantity5.Text) <> "" And CommonUtility.Get_StringValue(txtquantity5.Text) <> "0.0000" And CommonUtility.Get_StringValue(txtq_unit_code5.Text) <> "") = True Then
                        'มีหมด
                        txtT_product.Text = "H.S.CODE " + Mid(CommonUtility.Get_StringValue(txttariff_code.Text), 1, 4) + "." + Mid(CommonUtility.Get_StringValue(txttariff_code.Text), 5, 4) + vbCrLf _
                        + txtproduct_n1.Text + txtproduct_n2.Text + "****" + vbCrLf + _
                        "TOTAL: " + BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) + vbCrLf + "        " + _
                        BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) + vbCrLf + "        " + _
                        BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) + vbCrLf + "        " + _
                        BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) + vbCrLf + "        " + _
                        BahtOnly(txtquantity5.Text, txtq_unit_code5.Text) & vbCrLf & "_____________________________"

                    ElseIf (CommonUtility.Get_StringValue(txtquantity1.Text) <> "" And CommonUtility.Get_StringValue(txtquantity1.Text) <> "0.0000" And CommonUtility.Get_StringValue(txtq_unit_code1.Text) <> "") = True And _
                    (CommonUtility.Get_StringValue(txtquantity2.Text) <> "" And CommonUtility.Get_StringValue(txtquantity2.Text) <> "0.0000" And CommonUtility.Get_StringValue(txtq_unit_code2.Text) <> "") = True And _
                    (CommonUtility.Get_StringValue(txtquantity3.Text) <> "" And CommonUtility.Get_StringValue(txtquantity3.Text) <> "0.0000" And CommonUtility.Get_StringValue(txtq_unit_code3.Text) <> "") = True And _
                    (CommonUtility.Get_StringValue(txtquantity4.Text) <> "" And CommonUtility.Get_StringValue(txtquantity4.Text) <> "0.0000" And CommonUtility.Get_StringValue(txtq_unit_code4.Text) <> "") = True And _
                    (CommonUtility.Get_StringValue(txtquantity5.Text) <> "" And CommonUtility.Get_StringValue(txtquantity5.Text) <> "0.0000" And CommonUtility.Get_StringValue(txtq_unit_code5.Text) <> "") = False Then
                        'ขาด 5
                        txtT_product.Text = "H.S.CODE " + Mid(CommonUtility.Get_StringValue(txttariff_code.Text), 1, 4) + "." + Mid(CommonUtility.Get_StringValue(txttariff_code.Text), 5, 4) + vbCrLf _
                        + txtproduct_n1.Text + txtproduct_n2.Text + "****" + vbCrLf + _
                        "TOTAL: " + BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) + vbCrLf + "        " + _
                        BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) + vbCrLf + "        " + _
                        BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) + vbCrLf + "        " + _
                        BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf & "_____________________________"

                    ElseIf (CommonUtility.Get_StringValue(txtquantity1.Text) <> "" And CommonUtility.Get_StringValue(txtquantity1.Text) <> "0.0000" And CommonUtility.Get_StringValue(txtq_unit_code1.Text) <> "") = True And _
                     (CommonUtility.Get_StringValue(txtquantity2.Text) <> "" And CommonUtility.Get_StringValue(txtquantity2.Text) <> "0.0000" And CommonUtility.Get_StringValue(txtq_unit_code2.Text) <> "") = True And _
                     (CommonUtility.Get_StringValue(txtquantity3.Text) <> "" And CommonUtility.Get_StringValue(txtquantity3.Text) <> "0.0000" And CommonUtility.Get_StringValue(txtq_unit_code3.Text) <> "") = True And _
                     (CommonUtility.Get_StringValue(txtquantity4.Text) <> "" And CommonUtility.Get_StringValue(txtquantity4.Text) <> "0.0000" And CommonUtility.Get_StringValue(txtq_unit_code4.Text) <> "") = False And _
                     (CommonUtility.Get_StringValue(txtquantity5.Text) <> "" And CommonUtility.Get_StringValue(txtquantity5.Text) <> "0.0000" And CommonUtility.Get_StringValue(txtq_unit_code5.Text) <> "") = False Then
                        'ขาด 4,5
                        txtT_product.Text = "H.S.CODE " + Mid(CommonUtility.Get_StringValue(txttariff_code.Text), 1, 4) + "." + Mid(CommonUtility.Get_StringValue(txttariff_code.Text), 5, 4) + vbCrLf _
                        + txtproduct_n1.Text + txtproduct_n2.Text + "****" + vbCrLf + _
                        "TOTAL: " + BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) + vbCrLf + "        " + _
                        BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) + vbCrLf + "        " + _
                        BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "_____________________________"

                    ElseIf (CommonUtility.Get_StringValue(txtquantity1.Text) <> "" And CommonUtility.Get_StringValue(txtquantity1.Text) <> "0.0000" And CommonUtility.Get_StringValue(txtq_unit_code1.Text) <> "") = True And _
                      (CommonUtility.Get_StringValue(txtquantity2.Text) <> "" And CommonUtility.Get_StringValue(txtquantity2.Text) <> "0.0000" And CommonUtility.Get_StringValue(txtq_unit_code2.Text) <> "") = True And _
                      (CommonUtility.Get_StringValue(txtquantity3.Text) <> "" And CommonUtility.Get_StringValue(txtquantity3.Text) <> "0.0000" And CommonUtility.Get_StringValue(txtq_unit_code3.Text) <> "") = False And _
                      (CommonUtility.Get_StringValue(txtquantity4.Text) <> "" And CommonUtility.Get_StringValue(txtquantity4.Text) <> "0.0000" And CommonUtility.Get_StringValue(txtq_unit_code4.Text) <> "") = False And _
                      (CommonUtility.Get_StringValue(txtquantity5.Text) <> "" And CommonUtility.Get_StringValue(txtquantity5.Text) <> "0.0000" And CommonUtility.Get_StringValue(txtq_unit_code5.Text) <> "") = False Then
                        'ขาด 3,4,5
                        txtT_product.Text = "H.S.CODE " + Mid(CommonUtility.Get_StringValue(txttariff_code.Text), 1, 4) + "." + Mid(CommonUtility.Get_StringValue(txttariff_code.Text), 5, 4) + vbCrLf _
                        + txtproduct_n1.Text + txtproduct_n2.Text + "****" + vbCrLf + _
                        "TOTAL: " + BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) + vbCrLf + "        " + _
                        BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "_____________________________"

                    ElseIf (CommonUtility.Get_StringValue(txtquantity1.Text) <> "" And CommonUtility.Get_StringValue(txtquantity1.Text) <> "0.0000" And CommonUtility.Get_StringValue(txtq_unit_code1.Text) <> "") = True And _
                      (CommonUtility.Get_StringValue(txtquantity2.Text) <> "" And CommonUtility.Get_StringValue(txtquantity2.Text) <> "0.0000" And CommonUtility.Get_StringValue(txtq_unit_code2.Text) <> "") = False And _
                      (CommonUtility.Get_StringValue(txtquantity3.Text) <> "" And CommonUtility.Get_StringValue(txtquantity3.Text) <> "0.0000" And CommonUtility.Get_StringValue(txtq_unit_code3.Text) <> "") = False And _
                      (CommonUtility.Get_StringValue(txtquantity4.Text) <> "" And CommonUtility.Get_StringValue(txtquantity4.Text) <> "0.0000" And CommonUtility.Get_StringValue(txtq_unit_code4.Text) <> "") = False And _
                      (CommonUtility.Get_StringValue(txtquantity5.Text) <> "" And CommonUtility.Get_StringValue(txtquantity5.Text) <> "0.0000" And CommonUtility.Get_StringValue(txtq_unit_code5.Text) <> "") = False Then
                        'ขาด 2,3,4,5
                        txtT_product.Text = "H.S.CODE " + Mid(CommonUtility.Get_StringValue(txttariff_code.Text), 1, 4) + "." + Mid(CommonUtility.Get_StringValue(txttariff_code.Text), 5, 4) + vbCrLf _
                        + txtproduct_n1.Text + txtproduct_n2.Text + "****" + vbCrLf + _
                        "TOTAL: " + BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "_____________________________"

                    End If

                Else
                    txtT_product.Text = "H.S.CODE " + Mid(CommonUtility.Get_StringValue(txttariff_code.Text), 1, 4) + "." + Mid(CommonUtility.Get_StringValue(txttariff_code.Text), 5, 4) + vbCrLf _
                    + txtproduct_n1.Text + txtproduct_n2.Text + "****"
                End If

                '============================================================
                If CommonUtility.Get_StringValue(txtNumRowCount.Text) = "1" Then
                    txtmarks.Visible = True
                    'txtgross_weight1.Visible = True
                    txtTolInvoice.Visible = True
                Else
                    txtmarks.Visible = False
                    'txtgross_weight1.Visible = False
                    txtTolInvoice.Visible = False
                End If

                Me.Detail1.NewPage = NewPage.None
            End If

        Next


    End Sub
    Function confor_mat(ByVal d_date As Date) As String
        Dim str_for As String = Format(d_date, "d/MM/yyyy")
        Return str_for
    End Function
    Sub CallInvoiceCheck() 'Check Invoince
        If CommonUtility.Get_StringValue(txtinvoice_no1.Text) <> "" And CommonUtility.Get_StringValue(txtinvoice_date1.Text) <> "" Then
            txtTolInvoice.Text = txtinvoice_no1.Text & vbNewLine & confor_mat(CDate(txtinvoice_date1.Text).Date)
        ElseIf CommonUtility.Get_StringValue(txtinvoice_no2.Text) <> "" And CommonUtility.Get_StringValue(txtinvoice_date2.Text) <> "" Then
            txtTolInvoice.Text = txtinvoice_no1.Text & vbNewLine & confor_mat(CDate(txtinvoice_date1.Text).Date) & vbNewLine _
                                + txtinvoice_no2.Text & vbNewLine & confor_mat(CDate(txtinvoice_date2.Text).Date)
        ElseIf CommonUtility.Get_StringValue(txtinvoice_no3.Text) <> "" And CommonUtility.Get_StringValue(txtinvoice_date3.Text) <> "" Then
            txtTolInvoice.Text = txtinvoice_no1.Text & vbNewLine & confor_mat(CDate(txtinvoice_date1.Text).Date) & vbNewLine _
                                + txtinvoice_no2.Text & vbNewLine & confor_mat(CDate(txtinvoice_date2.Text).Date) & vbNewLine _
                                + txtinvoice_no3.Text & vbNewLine & confor_mat(CDate(txtinvoice_date3.Text).Date)
        ElseIf CommonUtility.Get_StringValue(txtinvoice_no4.Text) <> "" And CommonUtility.Get_StringValue(txtinvoice_date4.Text) <> "" Then
            txtTolInvoice.Text = txtinvoice_no1.Text & vbNewLine & confor_mat(CDate(txtinvoice_date1.Text).Date) & vbNewLine _
                                + txtinvoice_no2.Text & vbNewLine & confor_mat(CDate(txtinvoice_date2.Text).Date) & vbNewLine _
                                + txtinvoice_no3.Text & vbNewLine & confor_mat(CDate(txtinvoice_date3.Text).Date) & vbNewLine _
                                + txtinvoice_no4.Text & vbNewLine & confor_mat(CDate(txtinvoice_date4.Text).Date)
        ElseIf CommonUtility.Get_StringValue(txtinvoice_no5.Text) <> "" And CommonUtility.Get_StringValue(txtinvoice_date5.Text) <> "" Then
            txtTolInvoice.Text = txtinvoice_no1.Text & vbNewLine & confor_mat(CDate(txtinvoice_date1.Text).Date) & vbNewLine _
                                + txtinvoice_no2.Text & vbNewLine & confor_mat(CDate(txtinvoice_date2.Text).Date) & vbNewLine _
                                + txtinvoice_no3.Text & vbNewLine & confor_mat(CDate(txtinvoice_date3.Text).Date) & vbNewLine _
                                + txtinvoice_no4.Text & vbNewLine & confor_mat(CDate(txtinvoice_date4.Text).Date) & vbNewLine _
                                + txtinvoice_no5.Text & vbNewLine & confor_mat(CDate(txtinvoice_date5.Text).Date)
        End If

    End Sub

    Function GrossTxt() As String
        Dim Str_GrossTxt As String
        Dim Dec_Gross As Decimal
        Dec_Gross = Format(CDec(Check_Null(CommonUtility.Get_StringValue(txtgross_weightH.Value))), "#,##0.0###")
        Str_GrossTxt = CStr(Dec_Gross) & " " & CommonUtility.Get_StringValue(txtg_unit_code.Value) 'CommonUtility.Get_StringValue(txtg_Unit_Desc.Value)

        Return Str_GrossTxt
    End Function

    Function FobTxt() As String
        Dim Str_FobTxt As String
        Dim Dec_Fob As Decimal

        If txtFOB_AMT.Value > 0 Then
            Dec_Fob = Format(CDec(Check_Null(CommonUtility.Get_StringValue(txtFOB_AMT.Value))), "#,##0.0###")
            Str_FobTxt = CStr(Dec_Fob) & " USD"
        Else
            Str_FobTxt = ""
        End If
        Return Str_FobTxt
    End Function

    Function con_date(ByVal str_date As Date) As Date

        Dim f_date As Date = Format(str_date, "d/MM/yyyy")

        Return f_date
    End Function

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

    Sub CHeck_DisPlay()

        If Mid(CommonUtility.Get_StringValue(txtFOBDisplay.Value), 1, 50) = "Display" Then

            If Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeader.Value), 1, 50) = "NW" Then

                txtGrossTxt.Text = Mid(txtnet_weight.Value, 1, 9) & " " & Mid(txtunit_code2.Value, 1, 10) '& vbNewLine & FobTxt()

            ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeader.Value), 1, 50) = "GROSS WEIGHT - NET WEIGHT" Then
                txtGrossTxt.Text = GrossTxt() & vbNewLine & Mid(txtnet_weight.Value, 1, 9) & " " & Mid(txtunit_code2.Value, 1, 10) '& vbNewLine & FobTxt()

            ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeader.Value), 1, 50) = "GROSS WEIGHT" Then
                txtGrossTxt.Text = GrossTxt() '& vbNewLine & FobTxt()

            ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeader.Value), 1, 50) = "" Then
                txtGrossTxt.Text = GrossTxt() '& vbNewLine & FobTxt()

            End If
        ElseIf Mid(CommonUtility.Get_StringValue(txtFOBDisplay.Value), 1, 50) = "NoDisplay" Then
            If Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeader.Value), 1, 50) = "NW" Then
                txtGrossTxt.Text = Mid(txtnet_weight.Value, 1, 9) & " " & Mid(txtunit_code2.Value, 1, 10)

            ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeader.Value), 1, 50) = "GROSS WEIGHT - NET WEIGHT" Then
                txtGrossTxt.Text = GrossTxt() & vbNewLine & Mid(txtnet_weight.Value, 1, 9) & " " & Mid(txtunit_code2.Value, 1, 10)

            ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeader.Value), 1, 50) = "GROSS WEIGHT" Then
                txtGrossTxt.Text = GrossTxt()

            ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeader.Value), 1, 50) = "" Then
                txtGrossTxt.Text = GrossTxt()

            End If

        ElseIf Mid(CommonUtility.Get_StringValue(txtFOBDisplay.Value), 1, 50) = "" Then

            If Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeader.Value), 1, 50) = "NW" Then
                txtGrossTxt.Text = Mid(txtnet_weight.Value, 1, 9) & " " & Mid(txtunit_code2.Value, 1, 10) '& vbNewLine & FobTxt()

            ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeader.Value), 1, 50) = "GROSS WEIGHT - NET WEIGHT" Then
                txtGrossTxt.Text = GrossTxt() & vbNewLine & Mid(txtnet_weight.Value, 1, 9) & " " & Mid(txtunit_code2.Value, 1, 10) '& vbNewLine & FobTxt()

            ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeader.Value), 1, 50) = "GROSS WEIGHT" Then
                txtGrossTxt.Text = GrossTxt() '& vbNewLine & FobTxt()

            ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeader.Value), 1, 50) = "" Then
                txtGrossTxt.Text = GrossTxt() '& vbNewLine & FobTxt()

            End If
        End If

    End Sub
End Class
