Imports DataDynamics.ActiveReports
Imports DataDynamics.ActiveReports.Document

Imports Microsoft.ApplicationBlocks.Data
Imports System.Data.SqlClient
Imports System.Data
Imports System.Text
Imports DFT.Dotnetnuke.ClassLibrary
Imports ReportPrintClass
Public Class rpt3_ediFORME_ESS
    Dim Cpage, CpageNum, Check_CaseRVCCount As Integer
    Public _TempB2B As String
    Dim TFOB, TGross, TNet, TUSD, TUSDOther As Decimal
    Dim str_mark, Str_TitleDe, Str_TitleDe2, Str_USDinvoiceDetail, Gr_ As String

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        Me.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.Custom
        Me.PageSettings.PaperName = "Fanfold 210 x 305 mm"
        ' Add any initialization after the InitializeComponent() call.

    End Sub

    'by rut Title New Begin-----------------
    Dim Str_TitlePage As String
    'by rut Title New End-------------------
    'by rut ��Ѻ page end ���͡�˹�
    Dim head_height As String

    Private Sub PageHeader1_Format(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PageHeader1.Format
        CpageNum += 1
        '        txtTemp_PageOf.Text = "Page : " & CpageNum & " of " & txt_PageCount.Value

        txtreference_code2_Temp.Text = txtreference_code2.Text
        '===============================================================

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
        ''ByTine 12-2-2561 ���������� ����¹ Field �纤���������ͧ�ҡ �ͧ���������Ť�ҵ�ҧ����� �������纤�� TaxCar �֧����¹��� TaxCar ���纷�� Field DIGIT1 ᷹
        If DBNull.Value.Equals(txtDIGIT1.Text) = False Then
            If CommonUtility.Get_StringValue(txtDIGIT1.Value) = "1" And CommonUtility.Get_StringValue(txtTitleMain.Text) <> "" Then
                txtTitleHead.Visible = True
            End If
        Else
            If CommonUtility.Get_StringValue(txtUSDInvoiceDetail.Value) = "1" And CommonUtility.Get_StringValue(txtTitleMain.Text) <> "" Then
                txtTitleHead.Visible = True
            End If
        End If
        'by rut Title New End-------------------------------------------
    End Sub
    Sub Head_Checkcompany_v1()
        Select Case CommonUtility.Get_StringValue(txtTax_Status.Text)
            Case 1 '��ͧ ����ʴ���������ʴ� Tax 1 ��� �ʴ�
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
                End If
            Case 0 '��ͧ ����ʴ���������ʴ� Tax 0 ��� ����ʴ�
                If Mid(txtdest_remark.Text, 1, 3) = "C/O" Then
                    txtCompany_Check_1.Text = txtob_address.Text & IIf(Check_NULLALL(txtNewEmail_ch01.Text) <> "", " E-mail: " & txtNewEmail_ch01.Text, "") & " CARE OF " & txtcompany_name.Text & " " & txtcompany_address.Text _
                                       & " " & txtcompany_province.Text & " " & txtcompany_country.Text &
                                       IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") &
                                       IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") &
                                       IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")
                ElseIf Mid(txtdest_remark.Text, 1, 3) = "O/B" Then
                    txtCompany_Check_1.Text = txtcompany_name.Text & " " & txtcompany_address.Text & " " & txtcompany_province.Text _
                                        & " " & txtcompany_country.Text & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") _
                                        & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtob_address.Text)) <> "", " ON BEHALF OF " & txtob_address.Text, "") & IIf(Check_NULLALL(txtNewEmail_ch01.Text) <> "", " E-mail: " & txtNewEmail_ch01.Text, "")
                ElseIf Mid(txtdest_remark.Text, 2, 1) <> "/" Then
                    txtCompany_Check_1.Text = txtcompany_name.Text & " " & txtcompany_address.Text & " " & txtcompany_province.Text _
                                        & " " & txtcompany_country.Text & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") _
                                        & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")
                End If
        End Select

    End Sub

    Sub Head_Checkdestination_v2()
        txtdestination_Check2.Text = txtdestination_company.Text & " " & txtdestination_address.Text & " " & txtdestination_province.Text _
                                & " " & txtdest_Receive_country.Text &
                                IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "")
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

    Private Sub Detail1_Format(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Detail1.Format
        Cpage += 1
        'txtPage2.Text = Cpage
        TFOB += txtFOB_AMT.Value
        TNet += Check_Null(txtgross_weightD.Value) '//txtnet_weight.Value

        ''ByTine 13-2-2561 �����������¡ GW
        TGross += Check_Null(txtgross_weightD.Value)
        TUSD += Check_Null(txtUSDInvoiceDetail.Value)

        'by rut FOB Other
        TUSDOther += Check_Null(txtPriceOtherDetail.Value)

        txtNumRowCount.Text = Cpage
        Count_RowToPage_DetailData()

        If Cpage = 1 Then 'invoice ��ҧ����� �ʴ���Ť�� ��¡���á��¡������
            Str_USDinvoiceDetail = Format(CommonUtility.Get_Decimal(txtUSDInvoiceDetail.Text), "#,##0.00##") & " " & txtCurrency_Code.Text
        End If

        If Cpage = 1 Then
            str_mark = CommonUtility.Get_StringValue(txtmarks.Text)
        End If
        '===================================================
        'txtTemp_box8.Text = Check_BOX8(CommonUtility.Get_StringValue(txtbox8.Value), Check_Letter(CommonUtility.Get_StringValue(txtletter.Value)), CommonUtility.Get_StringValue(txtprofit_per_unit.Value))
        txtTemp_box8.Text = Check_BOX8_Ole(CommonUtility.Get_StringValue(txtbox8.Value), CommonUtility.Get_StringValue(txtletter.Value),
                                    CommonUtility.Get_StringValue(txtCheck_StatusWeb.Value), CommonUtility.Get_StringValue(txtSINGLE_COUNTRY_CONTENT.Value))

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

        Check_CaseRVCCount = CInt(txtCheck_CaseRVCCount.Text)
        'by rut Title New End--------------------------------------------------
    End Sub

    Function NetWeight_(ByVal _netweight, ByVal _uni) As String
        Dim sumStr As String
        Dim ChkTempUnit3 As String = ""
        ChkTempUnit3 = CheckUnitDetail_FormE(txtinvh_run_auto.Value)

        Select Case ChkTempUnit3.ToUpper
            Case "KGM", "KGS" '' �ó��� KGM ����ʴ� GW ���� NW
                '//sumStr = "N.W." & vbNewLine & Format(_netweight, "#,##0.00##") & " " & _uni
                sumStr = "N.W." & Format(_netweight, "#,##0.00##") & " " & _uni
            Case Else
                sumStr = Format(_netweight, "#,##0.00##") & " " & _uni
        End Select

        'sumStr = "N.W." & vbNewLine & Format(_netweight, "#,##0.00##") & " " & _uni

        Return sumStr

        ''�ͧ���
        'sumStr = Format(_netweight, "#,##0.00##") & " " & _uni

        'Return sumStr
    End Function

#Region "ByTine �ͧ���� ���¡ GW/NW 28-8-2560"
    Function CallInvoice_() As String 'Check Invoince
        Dim str_invoice As String
        Select Case txtshow_check.Text
            Case "100", "110", "111"
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
    '//madnattz
    Function GrossTxtDetail() As String
        Dim Str_GrossTxt As String
        Dim Dec_Gross As Decimal
        Dec_Gross = CDec(Check_Null(DataUtil.ConvertToString(txtgross_weightD.Value))) 'Format(CDec(Check_Null(DataUtil.ConvertToString(txtgross_weightH.Value))), "#,##0.00##")

        Dim W_type As String = ""
        If txtDisplayUnitType.Text = "NET WEIGHT" Then
            W_type = "N.W. "
            '//Dec_Gross = CDec(Check_Null(DataUtil.ConvertToString(txtnet_weight.Value)))
        End If

        Str_GrossTxt = W_type & Format(Dec_Gross, "#,##0.00##") & " " & CheckUnitDetail_ASW(txtinvh_run_auto.Value) 'DataUtil.ConvertToString(txtunit_code3.Value) 'CStr(Dec_Gross) & " " & DataUtil.ConvertToString(txtg_unit_code.Value)

        Return Str_GrossTxt
    End Function

    Function AddGW_NWLineForFormE(ByVal By_CaseGW_NW As Integer) As String
        Dim Line_temp As String = ""
        Dim ChkTempUnit3 As String = ""
        ChkTempUnit3 = CheckUnitDetail_FormE(txtinvh_run_auto.Value)

        Select Case ChkTempUnit3.ToUpper
            Case "KGM", "KGS" '' �ó��� KGM ����ʴ� GW ���� NW
                Select Case By_CaseGW_NW
                    Case 0 'GROSS WEIGHT
                        '//Line_temp = "G.W." & vbNewLine
                        Line_temp = ""
                    Case 1 'NET WEIGHT
                        Line_temp = "N.W."
                    Case Else '""
                        '//Line_temp = "G.W." & vbNewLine
                        Line_temp = ""
                End Select
        End Select

        Return Line_temp

    End Function


    Function CHeck_DisPlay(ByVal CountAllDetail As String, ByVal CheckDetailGross As String) As String
        '�ѹ������ͧ�ҡ ����������Ǩ�ͺ˹��� grossweight �����¡���á��¡������ ���ʹ��¡�����
        Dim TempUnit3 As String = ""
        TempUnit3 = CheckUnitDetail_ASW(txtinvh_run_auto.Value)

        Callinvoice_board()
        Dim str_Display As String
        Dim sss As String = txtGross_Weight.Text

        Dim str_gross As String

        'str_gross = txtgross_weight.Text

        '����¹�������� Detail
        '����ͧ invoice ��ҧ�����
        'Dim str_NumInvoice As String = DataUtil.ConvertToString(txtNumInvoice.Text)
        Dim str_USDInvoice As String
        '����ͧ ��Ť�� ��ҧ�����

        Dim Total_FOB As Decimal = 0

        If txtCurrency_Code.Text = "USD" Then
            Total_FOB = TUSD
            str_USDInvoice = Format(CommonUtility.Get_Decimal(txtUSDInvoiceDetail.Text), "#,##0.00##") & " " & txtCurrency_Code.Text
        Else
            Total_FOB = TUSDOther
            str_USDInvoice = Format(CommonUtility.Get_Decimal(txtPriceOtherDetail.Text), "#,##0.00##") & " " & txtCurrency_Code.Text
        End If

        Select Case checkNull_Show(DataUtil.ConvertToString(txtshow_check.Text))
            Case "100", "101", "111" ' ����ͧ invoice ��ҧ����� �ʴ���Ť��
                Select Case CheckDetailGross
                    Case "GWDetail" '�¡��¡��
                        '���͹����� ��Ѻ�� �ѹ��� 26-10-2012 ����ǡѺ RVC �ͧ�ѹ Case 2,8 ��ͧ�ʴ���Ť�� ������������ʴ�
                        'begin RVC
                        Select Case Mid(DataUtil.ConvertToString(txtletter.Value), 1, 50)
                            Case "3", "6", "7" '�ʴ���Ť�� Detail
                                If Mid(DataUtil.ConvertToString(txtFOBDisplay.Value), 1, 50) = "Display" Then
                                    str_gross = GrossTxtDetail() '& vbNewLine & FobTxt()
                                    Select Case C_TotalRowDe.Text
                                        Case 1
                                            str_Display = str_gross & vbNewLine & str_USDInvoice
                                        Case Else
                                            If Cpage = CountAllDetail Then
                                                Select Case Check_CaseRVCCount
                                                    Case 9 '�����ҧ��蹻� ����ʴ���Ť�����
                                                        str_Display = str_gross & vbNewLine & str_USDInvoice & vbNewLine &
                                                                         "_______" & vbNewLine & IIf(txtDisplayUnitType.Text = "NET WEIGHT", "N.W.", "") & Format(IIf(txtDisplayUnitType.Text = "NET WEIGHT", TNet, TGross), "#,##0.00##") & " " & TempUnit3
                                                    Case Else '�� RVC ���ҧ���� �ʴ���Ť�����
                                                        str_Display = str_gross & vbNewLine & str_USDInvoice & vbNewLine &
                                                                         "_______" & vbNewLine & IIf(txtDisplayUnitType.Text = "NET WEIGHT", "N.W.", "") & Format(IIf(txtDisplayUnitType.Text = "NET WEIGHT", TNet, TGross), "#,##0.00##") & " " & TempUnit3 &
                                                                         vbNewLine & Format(Total_FOB, "#,##0.00##") & " " & txtCurrency_Code.Text


                                                End Select
                                            Else
                                                str_Display = str_gross & vbNewLine & str_USDInvoice
                                            End If
                                    End Select
                                End If
                            Case Else '����ʴ���Ť�� Detail
                                If Mid(DataUtil.ConvertToString(txtFOBDisplay.Value), 1, 50) = "Display" Then
                                    str_gross = GrossTxtDetail() '& vbNewLine & FobTxt()
                                    Select Case C_TotalRowDe.Text
                                        Case 1
                                            str_Display = str_gross '& vbNewLine & str_USDInvoice
                                        Case Else
                                            If Cpage = CountAllDetail Then
                                                Select Case Check_CaseRVCCount
                                                    Case 9 '�����ҧ��蹻� ����ʴ���Ť�����
                                                        '//str_Display = str_gross & vbNewLine &
                                                        '//           "_______" & vbNewLine & Format(IIf(txtDisplayUnitType.Text = "NET WEIGHT", TNet, TGross), "#,##0.00##") & " " & TempUnit3
                                                        str_Display = str_gross & vbNewLine &
                                                                                "_______" & vbNewLine & IIf(txtDisplayUnitType.Text = "NET WEIGHT", "N.W.", "") &
                                                                                Format(IIf(txtDisplayUnitType.Text = "NET WEIGHT", TNet, TGross), "#,##0.00##") & " " & TempUnit3
                                                    Case Else '�� RVC ���ҧ���� �ʴ���Ť�����
                                                        str_Display = str_gross & vbNewLine &
                                                                               "_______" & vbNewLine & IIf(txtDisplayUnitType.Text = "NET WEIGHT", "N.W.", "") &
                                                                               Format(IIf(txtDisplayUnitType.Text = "NET WEIGHT", TNet, TGross), "#,##0.00##") & " " & TempUnit3 &
                                                                               vbNewLine & Format(Total_FOB, "#,##0.00##") & " " & txtCurrency_Code.Text

                                                End Select
                                            Else
                                                str_Display = str_gross '& vbNewLine & str_USDInvoice
                                            End If
                                    End Select
                                End If
                        End Select
                        'end RVC
                End Select

            Case Else '����� invoice ��ҧ�����
                Select Case CheckDetailGross
                    Case "GWDetail" '�¡��¡��
                        '���͹����� ��Ѻ�� �ѹ��� 26-10-2012 ����ǡѺ RVC �ͧ�ѹ Case 2,8 ��ͧ�ʴ���Ť�� ������������ʴ�
                        'begin RVC
                        Select Case Mid(DataUtil.ConvertToString(txtletter.Value), 1, 50)
                            Case "3", "6", "7" '�ʴ���Ť�� Detail
                                str_gross = GrossTxtDetail() & vbNewLine & FobTxt()
                            Case Else '����ʴ���Ť�� Detail
                                'form4 �� WeightDisplayHeader
                                str_gross = GrossTxtDetail()
                        End Select

                        Select Case C_TotalRowDe.Text
                            Case 1
                                str_Display = str_gross
                            Case Else
                                If Cpage = CountAllDetail Then
                                    Select Case Check_CaseRVCCount
                                        Case 9  '�����ҧ��蹻� ����ʴ���Ť�����
                                            str_Display = str_gross & vbNewLine &
                                                                        "_______" & vbNewLine & IIf(txtDisplayUnitType.Text = "NET WEIGHT", "N.W.", "") &
                                                                        Format(IIf(txtDisplayUnitType.Text = "NET WEIGHT", TNet, TGross), "#,##0.00##") & " " & TempUnit3
                                        Case Else '�� RVC ���ҧ���� �ʴ���Ť�����

                                            str_Display = str_gross & vbNewLine &
                                                                        "_______" & vbNewLine & IIf(txtDisplayUnitType.Text = "NET WEIGHT", "N.W.", "") &
                                                                        Format(IIf(txtDisplayUnitType.Text = "NET WEIGHT", TNet, TGross), "#,##0.00##") & " " & TempUnit3 &
                                                                        vbNewLine & FobTxtAll()

                                    End Select
                                Else
                                    str_Display = str_gross
                                End If

                        End Select
                        'end RVC

                End Select
        End Select

        Return str_Display
    End Function

    Function FobTxtAll() As String
        Dim Str_FobTxt As String
        Dim Dec_Fob As Decimal

        'by rut FOB Other
        Select Case DataUtil.ConvertToString(txtCurrency_Code.Text)
            Case Is <> "USD"
                If Check_Null(DataUtil.ConvertToString(txtPriceOtherDetail.Value)) > 0 Then
                    Dec_Fob = CDec(Check_Null(DataUtil.ConvertToString(TUSDOther))) 'Format(CDec(Check_Null(DataUtil.ConvertToString(txtFOB_AMT.Value))), "#,##0.0###")
                    Str_FobTxt = Format(Dec_Fob, "#,##0.00##") & " " & txtCurrency_Code.Text
                Else
                    Str_FobTxt = Format(0, "#,##0.00##") & " " & txtCurrency_Code.Text '""
                End If
            Case Else
                If Check_Null(DataUtil.ConvertToString(txtFOB_AMT.Value)) > 0 Then
                    Dec_Fob = CDec(Check_Null(DataUtil.ConvertToString(TFOB))) 'Format(CDec(Check_Null(DataUtil.ConvertToString(txtFOB_AMT.Value))), "#,##0.0###")
                    Str_FobTxt = Format(Dec_Fob, "#,##0.00##") & " " & txtCurrency_Code.Text '& " USD"
                Else
                    Str_FobTxt = Format(0, "#,##0.00##") & " " & txtCurrency_Code.Text '""
                End If
        End Select

        Return Str_FobTxt
    End Function

    'by rut FOB Other
    Function FobTxtAllSum() As String
        Dim Str_FobTxt As String
        Dim Dec_Fob As Decimal
        'by rut FOB Other
        Select Case DataUtil.ConvertToString(txtCurrency_Code.Text)
            Case Is <> "USD"
                If Check_Null(DataUtil.ConvertToString(txtPriceOtherDetail.Value)) > 0 Then
                    Dec_Fob = CDec(Check_Null(DataUtil.ConvertToString(TUSDOther))) 'Format(CDec(Check_Null(DataUtil.ConvertToString(txtFOB_AMT.Value))), "#,##0.0###")
                    Str_FobTxt = Format(Dec_Fob, "#,##0.00##") & " " & txtCurrency_Code.Text
                Else
                    Str_FobTxt = Format(0, "#,##0.00##") & " " & txtCurrency_Code.Text '""
                End If
            Case Else
                If Check_Null(DataUtil.ConvertToString(txtFOB_AMT.Value)) > 0 Then
                    Dec_Fob = CDec(Check_Null(DataUtil.ConvertToString(txttotalSum_fob_amt.Value))) 'Format(CDec(Check_Null(DataUtil.ConvertToString(txtFOB_AMT.Value))), "#,##0.0###")
                    Str_FobTxt = Format(Dec_Fob, "#,##0.00##") & " " & txtCurrency_Code.Text '& " USD"
                Else
                    Str_FobTxt = Format(0, "#,##0.00##") & " " & txtCurrency_Code.Text '""
                End If
        End Select

        Return Str_FobTxt
    End Function

#End Region

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

    Sub Count_RowToPage_DetailData()
        Dim CountData_T, CountData_T2 As Integer
        CountData_T = C_TotalRowDe.Text
        CountData_T2 = CountData_T / 7

        Select Case CommonUtility.Get_StringValue(txtCheckGrossDetail.Text)
            Case "GWDetail"
                txtGrossTxt.Text = CHeck_DisPlay(CommonUtility.Get_StringValue(C_TotalRowDe.Text), CommonUtility.Get_StringValue(txtCheckGrossDetail.Text))
        End Select

        'CallInvoiceCheck()
        'CHeck_DisPlay()

        If (CommonUtility.Get_StringValue(txtquantity1.Text) <> "" And CommonUtility.Get_Decimal(txtquantity1.Text) <> 0 And CommonUtility.Get_StringValue(txtq_unit_code1.Text) <> "") = True And
        (CommonUtility.Get_StringValue(txtquantity2.Text) <> "" And CommonUtility.Get_Decimal(txtquantity2.Text) <> 0 And CommonUtility.Get_StringValue(txtq_unit_code2.Text) <> "") = True And
        (CommonUtility.Get_StringValue(txtquantity3.Text) <> "" And CommonUtility.Get_Decimal(txtquantity3.Text) <> 0 And CommonUtility.Get_StringValue(txtq_unit_code3.Text) <> "") = True And
        (CommonUtility.Get_StringValue(txtquantity4.Text) <> "" And CommonUtility.Get_Decimal(txtquantity4.Text) <> 0 And CommonUtility.Get_StringValue(txtq_unit_code4.Text) <> "") = True And
        (CommonUtility.Get_StringValue(txtquantity5.Text) <> "" And CommonUtility.Get_Decimal(txtquantity5.Text) <> 0 And CommonUtility.Get_StringValue(txtq_unit_code5.Text) <> "") = True Then
            '�����
            All_CheckThird_5(Cpage)

        ElseIf (CommonUtility.Get_StringValue(txtquantity1.Text) <> "" And CommonUtility.Get_Decimal(txtquantity1.Text) <> 0 And CommonUtility.Get_StringValue(txtq_unit_code1.Text) <> "") = True And
        (CommonUtility.Get_StringValue(txtquantity2.Text) <> "" And CommonUtility.Get_Decimal(txtquantity2.Text) <> 0 And CommonUtility.Get_StringValue(txtq_unit_code2.Text) <> "") = True And
        (CommonUtility.Get_StringValue(txtquantity3.Text) <> "" And CommonUtility.Get_Decimal(txtquantity3.Text) <> 0 And CommonUtility.Get_StringValue(txtq_unit_code3.Text) <> "") = True And
        (CommonUtility.Get_StringValue(txtquantity4.Text) <> "" And CommonUtility.Get_Decimal(txtquantity4.Text) <> 0 And CommonUtility.Get_StringValue(txtq_unit_code4.Text) <> "") = True And
        (CommonUtility.Get_StringValue(txtquantity5.Text) <> "" And CommonUtility.Get_Decimal(txtquantity5.Text) <> 0 And CommonUtility.Get_StringValue(txtq_unit_code5.Text) <> "") = False Then
            '�Ҵ 5
            All_CheckThird_4(Cpage)

        ElseIf (CommonUtility.Get_StringValue(txtquantity1.Text) <> "" And CommonUtility.Get_Decimal(txtquantity1.Text) <> 0 And CommonUtility.Get_StringValue(txtq_unit_code1.Text) <> "") = True And
         (CommonUtility.Get_StringValue(txtquantity2.Text) <> "" And CommonUtility.Get_Decimal(txtquantity2.Text) <> 0 And CommonUtility.Get_StringValue(txtq_unit_code2.Text) <> "") = True And
         (CommonUtility.Get_StringValue(txtquantity3.Text) <> "" And CommonUtility.Get_Decimal(txtquantity3.Text) <> 0 And CommonUtility.Get_StringValue(txtq_unit_code3.Text) <> "") = True And
         (CommonUtility.Get_StringValue(txtquantity4.Text) <> "" And CommonUtility.Get_Decimal(txtquantity4.Text) <> 0 And CommonUtility.Get_StringValue(txtq_unit_code4.Text) <> "") = False And
         (CommonUtility.Get_StringValue(txtquantity5.Text) <> "" And CommonUtility.Get_Decimal(txtquantity5.Text) <> 0 And CommonUtility.Get_StringValue(txtq_unit_code5.Text) <> "") = False Then
            '�Ҵ 4,5
            All_CheckThird_3(Cpage)

        ElseIf (CommonUtility.Get_StringValue(txtquantity1.Text) <> "" And CommonUtility.Get_Decimal(txtquantity1.Text) <> 0 And CommonUtility.Get_StringValue(txtq_unit_code1.Text) <> "") = True And
          (CommonUtility.Get_StringValue(txtquantity2.Text) <> "" And CommonUtility.Get_Decimal(txtquantity2.Text) <> 0 And CommonUtility.Get_StringValue(txtq_unit_code2.Text) <> "") = True And
          (CommonUtility.Get_StringValue(txtquantity3.Text) <> "" And CommonUtility.Get_Decimal(txtquantity3.Text) <> 0 And CommonUtility.Get_StringValue(txtq_unit_code3.Text) <> "") = False And
          (CommonUtility.Get_StringValue(txtquantity4.Text) <> "" And CommonUtility.Get_Decimal(txtquantity4.Text) <> 0 And CommonUtility.Get_StringValue(txtq_unit_code4.Text) <> "") = False And
          (CommonUtility.Get_StringValue(txtquantity5.Text) <> "" And CommonUtility.Get_Decimal(txtquantity5.Text) <> 0 And CommonUtility.Get_StringValue(txtq_unit_code5.Text) <> "") = False Then
            '�Ҵ 3,4,5
            All_CheckThird_2(Cpage)

        ElseIf (CommonUtility.Get_StringValue(txtquantity1.Text) <> "" And CommonUtility.Get_Decimal(txtquantity1.Text) <> 0 And CommonUtility.Get_StringValue(txtq_unit_code1.Text) <> "") = True And
          (CommonUtility.Get_StringValue(txtquantity2.Text) <> "" And CommonUtility.Get_Decimal(txtquantity2.Text) <> 0 And CommonUtility.Get_StringValue(txtq_unit_code2.Text) <> "") = False And
          (CommonUtility.Get_StringValue(txtquantity3.Text) <> "" And CommonUtility.Get_Decimal(txtquantity3.Text) <> 0 And CommonUtility.Get_StringValue(txtq_unit_code3.Text) <> "") = False And
          (CommonUtility.Get_StringValue(txtquantity4.Text) <> "" And CommonUtility.Get_Decimal(txtquantity4.Text) <> 0 And CommonUtility.Get_StringValue(txtq_unit_code4.Text) <> "") = False And
          (CommonUtility.Get_StringValue(txtquantity5.Text) <> "" And CommonUtility.Get_Decimal(txtquantity5.Text) <> 0 And CommonUtility.Get_StringValue(txtq_unit_code5.Text) <> "") = False Then
            '�Ҵ 2,3,4,5
            All_CheckThird_1(Cpage)

        Else
            All_CheckThird_1(Cpage)

        End If

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

    Function Check_BOX8_Ole(ByVal _Box8Str As String, ByVal _LETTER As String, ByVal _CheckWeb As String, ByVal _SINGLE_COUNTRY_CONTENT As String) As String
        Dim str_text As String
        Select Case _CheckWeb
            Case "1"
                Select Case _LETTER
                    Case "3"
                        If _SINGLE_COUNTRY_CONTENT <> "" Then
                            str_text = """" & Format(CDec(_Box8Str), "#,###.00") & "%""" & vbNewLine & _SINGLE_COUNTRY_CONTENT
                        Else
                            str_text = """" & Format(CDec(_Box8Str), "#,###.00") & "%"""
                        End If
                    Case Else
                        str_text = _Box8Str
                End Select
            Case Else
                str_text = _Box8Str
        End Select


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

        'by rut FOB Other
        Select Case DataUtil.ConvertToString(txtCurrency_Code.Text)
            Case Is <> "USD"
                If Check_Null(DataUtil.ConvertToString(txtPriceOtherDetail.Value)) > 0 Then
                    Dec_Fob = CDec(Check_Null(DataUtil.ConvertToString(txtPriceOtherDetail.Value))) 'Format(CDec(Check_Null(DataUtil.ConvertToString(txtFOB_AMT.Value))), "#,##0.0###")
                    Str_FobTxt = Format(Dec_Fob, "#,##0.00##") & " " & txtCurrency_Code.Text
                Else
                    Str_FobTxt = Format(0, "#,##0.00##") & " " & txtCurrency_Code.Text '""
                End If
            Case Else
                If Check_Null(DataUtil.ConvertToString(txtFOB_AMT.Value)) > 0 Then
                    Dec_Fob = CDec(Check_Null(DataUtil.ConvertToString(txtFOB_AMT.Value))) 'Format(CDec(Check_Null(DataUtil.ConvertToString(txtFOB_AMT.Value))), "#,##0.0###")
                    Str_FobTxt = Format(Dec_Fob, "#,##0.00##") & " " & txtCurrency_Code.Text '& " USD"
                Else
                    Str_FobTxt = Format(0, "#,##0.00##") & " " & txtCurrency_Code.Text '""
                End If
        End Select

        Return Str_FobTxt
    End Function

    'Private Sub PageFooter1_Format(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PageFooter1.Format
    '    check_show_check()

    '    txtcompany_provincefoot.Text = txtcompany_provincefoot1.Text & " " & format_dateSelect()

    '    'issued
    '    Select Case CheckIssuedDateAllForms(CDate(txtdeparture_date.Value), CDate(IIf(IsDBNull(txtprintFormDate.Value) = True, Now.Date, txtprintFormDate.Value)))
    '        Case True
    '            Pic_ch7_Issued.Visible = True
    '        Case False
    '            Pic_ch7_Issued.Visible = False
    '    End Select
    'End Sub

    Private Sub PageFooter1_Format(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PageFooter1.Format
        check_show_check()

        Select Case txtSendCheckSeletedate.Text
            Case True '���ѹ������͡�ͧ
                txtcompany_provincefoot.Text = txtcompany_provincefoot1.Text & " " & txtdateSelectRPT.Text 'format_dateSelect()
            Case False '�� �ѹ͹��ѵ�
                txtcompany_provincefoot.Text = txtcompany_provincefoot1.Text & " " & CommonUtility.Get_DateTime(txtapprove_date.Text).ToString("dd/MM/yyyy") 'format_dateSelect()
        End Select

        'issued
        Select Case CheckIssuedDateAllForms(CDate(txtdeparture_date.Value), CDate(IIf(IsDBNull(txtprintFormDate.Value) = True, Now.Date, txtprintFormDate.Value)))
            Case True
                Pic_ch7_Issued.Visible = True
            Case False
                Pic_ch7_Issued.Visible = False
        End Select
        Dim dsESS As DataSet = search_imageForm(txtinvh_run_auto.Text)
        If dsESS.Tables(0).Rows.Count > 0 Then
            CaseCheck_numimagesSign(dsESS)
            CaseCheck_ApproveSign(dsESS)
        End If
    End Sub

    Function checkNull_Show(ByVal _sendTXT As String) As String
        Dim _reShow As String

        If _sendTXT = "" Then
            _reShow = "000"
        Else
            _reShow = _sendTXT
        End If

        Return _reShow
    End Function
    Sub check_show_check()
        Dim i As Integer
        Dim str_arr As Array
        Dim str_ As String

        '��������Ҥ��� ����� show_check ����� ; ���ͨ� split �����������͹�����
        For i = 0 To checkNull_Show(CommonUtility.Get_StringValue(txtshow_check.Value)).Length - 1
            str_ += Mid(checkNull_Show(CommonUtility.Get_StringValue(txtshow_check.Value)), i + 1, 1) & ";"

        Next
        Dim jarr As Integer
        Dim num_strArr As String = ""

        str_arr = str_.Split(";") '�� 0;0;1;0;1;0;

        '�����Ҥ����������͹䢷�������
        For jarr = 0 To str_arr.Length - 1
            If str_arr(jarr) = "1" Then

                num_strArr += jarr & ";"
            End If
        Next

        Dim garr As Integer
        '������͹��ҵ�Ǩ�ͺ ����� case ����
        If CommonUtility.Get_StringValue(num_strArr) <> "" Then
            For garr = 0 To num_strArr.Length - 1
                Select Case num_strArr(garr)
                    Case "0" 'third
                        Pic_ch1_third.Visible = True
                        txtTHAILAND.Text = "THAILAND"
                    Case "1" 'back
                        Pic_ch3_back.Visible = True
                        txtTHAILAND.Text = txtback_country.Value
                        Exit For
                    Case "2" 'ex
                        Pic_ch5_exhibi.Visible = True
                        txtTHAILAND.Text = "THAILAND"
                    Case Else
                        txtTHAILAND.Text = "THAILAND"
                End Select
            Next
        Else
            txtTHAILAND.Text = "THAILAND"
        End If
    End Sub

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
        Select Case txtshow_check.Text
            Case "100", "110", "111"
                Select Case CommonUtility.Get_StringValue(Check_NULLALL(txtNumInvoice.Text))
                    Case ""
                        str_invoice = Callinvoice_board(0)
                    Case Is <> ""
                        str_invoice = txtNumInvoice.Text
                End Select
                'Str_invoice = txtNumInvoice.Text
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

    Function tariff_All(ByVal _tariff As String) As String
        Dim str_tariff As String
        str_tariff = Mid(_tariff, 1, 4) & "." & Mid(_tariff, 5, 2) '//& "." & Mid(_tariff, 7, 4)

        Return str_tariff
    End Function

    Function Check_Third(ByVal T1_third_country As String, ByVal T2_back_country As String, ByVal T3_place_exibition As String) As Integer
        If T1_third_country <> "" And T2_back_country <> "" And T3_place_exibition <> "" Then
            Return 1 '�����

        ElseIf T1_third_country = "" And T2_back_country <> "" And T3_place_exibition <> "" Then
            Return 2 'T2_back_country,T3_place_exibition

        ElseIf T1_third_country = "" And T2_back_country = "" And T3_place_exibition <> "" Then
            Return 3 'T3_place_exibition

        ElseIf T1_third_country = "" And T2_back_country = "" And T3_place_exibition = "" Then
            Return 4 '��������

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

    Dim All_str As String
    Sub Title_StrDetail()
        'All_str = CarTxt(CommonUtility.Get_StringValue(txtUSDInvoiceDetail.Value), CommonUtility.Get_StringValue(txtInvoiceDetailTH.Value)) & "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
        '                                   & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
        All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
        & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
    End Sub
    Sub All_CheckThird_5(ByVal count_data As Integer)
        Title_StrDetail()
        Select Case Check_Third(DataUtil.ConvertToString(txtthird_country.Value),
                                DataUtil.ConvertToString(txtback_country.Value),
                                DataUtil.ConvertToString(txtplace_exibition.Value))
            Case 1
                'All_str = "HS. CODE. " & tariff_All(DataUtil.ConvertToString(txttariff_code.Text)) & vbCrLf _
                '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & NewStr_AddressDetail(DataUtil.ConvertToString(txtByCom_CH03.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " &
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " &
                                                                BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " &
                                                                BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf & "        " &
                                                                BahtOnly(txtquantity5.Text, txtq_unit_code5.Text) & vbCrLf &
                                                                String_Total_USDOnly_New_FORME(txtDisplayUDS7.Text, txtCurrency_Code.Text, TFOB, DataUtil.ConvertToString(txtshow_check.Text), TUSD, TUSDOther) &
                                                                txtthird_country.Text & vbCrLf &
                                                                txtback_country.Text & vbCrLf &
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 2 'T2_back_country,T3_place_Exibition
                'All_str = "HS. CODE. " & tariff_All(DataUtil.ConvertToString(txttariff_code.Text)) & vbCrLf _
                '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & NewStr_AddressDetail(DataUtil.ConvertToString(txtByCom_CH03.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " &
                                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " &
                                                                                BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " &
                                                                                BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf & "        " &
                                                                                BahtOnly(txtquantity5.Text, txtq_unit_code5.Text) & vbCrLf &
                                                                                String_Total_USDOnly_New_FORME(txtDisplayUDS7.Text, txtCurrency_Code.Text, TFOB, DataUtil.ConvertToString(txtshow_check.Text), TUSD, TUSDOther) &
                                                                                txtback_country.Text & vbCrLf &
                                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 3 'T3_place_Exibition
                'All_str = "HS. CODE. " & tariff_All(DataUtil.ConvertToString(txttariff_code.Text)) & vbCrLf _
                '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & NewStr_AddressDetail(DataUtil.ConvertToString(txtByCom_CH03.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " &
                                                                    BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " &
                                                                    BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " &
                                                                    BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf & "        " &
                                                                    BahtOnly(txtquantity5.Text, txtq_unit_code5.Text) & vbCrLf &
                                                                    String_Total_USDOnly_New_FORME(txtDisplayUDS7.Text, txtCurrency_Code.Text, TFOB, DataUtil.ConvertToString(txtshow_check.Text), TUSD, TUSDOther) &
                                                                    "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 4 '��������
                'All_str = "HS. CODE. " & tariff_All(DataUtil.ConvertToString(txttariff_code.Text)) & vbCrLf _
                '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & NewStr_AddressDetail(DataUtil.ConvertToString(txtByCom_CH03.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " &
                                                                    BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " &
                                                                    BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " &
                                                                    BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf & "        " &
                                                                    BahtOnly(txtquantity5.Text, txtq_unit_code5.Text) & vbCrLf &
                                                                    String_Total_USDOnly_New_FORME(txtDisplayUDS7.Text, txtCurrency_Code.Text, TFOB, DataUtil.ConvertToString(txtshow_check.Text), TUSD, TUSDOther) &
                                                                    "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 5 'T2_back_country
                'All_str = "HS. CODE. " & tariff_All(DataUtil.ConvertToString(txttariff_code.Text)) & vbCrLf _
                '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & NewStr_AddressDetail(DataUtil.ConvertToString(txtByCom_CH03.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " &
                                                                    BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " &
                                                                    BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " &
                                                                    BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf & "        " &
                                                                    BahtOnly(txtquantity5.Text, txtq_unit_code5.Text) & vbCrLf &
                                                                    String_Total_USDOnly_New_FORME(txtDisplayUDS7.Text, txtCurrency_Code.Text, TFOB, DataUtil.ConvertToString(txtshow_check.Text), TUSD, TUSDOther) &
                                                                    DataUtil.ConvertToString(txtsupplementary_details.Value) & vbCrLf &
                                                                    _TempB2B & vbNewLine &
                                                                    "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 6 'T3_place_Exibition
                'All_str = "HS. CODE. " & tariff_All(DataUtil.ConvertToString(txttariff_code.Text)) & vbCrLf _
                '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & NewStr_AddressDetail(DataUtil.ConvertToString(txtByCom_CH03.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " &
                                                                    BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " &
                                                                    BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " &
                                                                    BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf & "        " &
                                                                    BahtOnly(txtquantity5.Text, txtq_unit_code5.Text) & vbCrLf &
                                                                    String_Total_USDOnly_New_FORME(txtDisplayUDS7.Text, txtCurrency_Code.Text, TFOB, DataUtil.ConvertToString(txtshow_check.Text), TUSD, TUSDOther) &
                                                                    "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 7 'T1_third_country,T2_back_country
                'All_str = "HS. CODE. " & tariff_All(DataUtil.ConvertToString(txttariff_code.Text)) & vbCrLf _
                '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & NewStr_AddressDetail(DataUtil.ConvertToString(txtByCom_CH03.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " &
                                                                    BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " &
                                                                    BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " &
                                                                    BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf & "        " &
                                                                    BahtOnly(txtquantity5.Text, txtq_unit_code5.Text) & vbCrLf &
                                                                    String_Total_USDOnly_New_FORME(txtDisplayUDS7.Text, txtCurrency_Code.Text, TFOB, DataUtil.ConvertToString(txtshow_check.Text), TUSD, TUSDOther) &
                                                                    txtthird_country.Text & vbCrLf &
                                                                    txtback_country.Text & vbCrLf &
                                                                    "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 8 'T1_third_country,T3_place_Exibition
                'All_str = "HS. CODE. " & tariff_All(DataUtil.ConvertToString(txttariff_code.Text)) & vbCrLf _
                '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & NewStr_AddressDetail(DataUtil.ConvertToString(txtByCom_CH03.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " &
                                                                    BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " &
                                                                    BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " &
                                                                    BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf & "        " &
                                                                    BahtOnly(txtquantity5.Text, txtq_unit_code5.Text) & vbCrLf &
                                                                    String_Total_USDOnly_New_FORME(txtDisplayUDS7.Text, txtCurrency_Code.Text, TFOB, DataUtil.ConvertToString(txtshow_check.Text), TUSD, TUSDOther) &
                                                                    txtthird_country.Text & vbCrLf &
                                                                    "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 9 'T1_third_country
                'All_str = "HS. CODE. " & tariff_All(DataUtil.ConvertToString(txttariff_code.Text)) & vbCrLf _
                '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & NewStr_AddressDetail(DataUtil.ConvertToString(txtByCom_CH03.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " &
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " &
                                                                BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " &
                                                                BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf & "        " &
                                                                BahtOnly(txtquantity5.Text, txtq_unit_code5.Text) & vbCrLf &
                                                                String_Total_USDOnly_New_FORME(txtDisplayUDS7.Text, txtCurrency_Code.Text, TFOB, DataUtil.ConvertToString(txtshow_check.Text), TUSD, TUSDOther) &
                                                                txtthird_country.Text & vbCrLf &
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
        End Select
    End Sub
    Sub All_CheckThird_4(ByVal count_data As Integer)
        Title_StrDetail()

        Select Case Check_Third(DataUtil.ConvertToString(txtthird_country.Value),
                                DataUtil.ConvertToString(txtback_country.Value),
                                DataUtil.ConvertToString(txtplace_exibition.Value))
            Case 1
                'All_str = "HS. CODE. " & tariff_All(DataUtil.ConvertToString(txttariff_code.Text)) & vbCrLf _
                '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & NewStr_AddressDetail(DataUtil.ConvertToString(txtByCom_CH03.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " &
                                            BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " &
                                            BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " &
                                            BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf &
                                            String_Total_USDOnly_New_FORME(txtDisplayUDS7.Text, txtCurrency_Code.Text, TFOB, DataUtil.ConvertToString(txtshow_check.Text), TUSD, TUSDOther) &
                                            txtthird_country.Text & vbCrLf &
                                            txtback_country.Text & vbCrLf &
                                            "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 2 'T2_back_country,T3_place_Exibition
                'All_str = "HS. CODE. " & tariff_All(DataUtil.ConvertToString(txttariff_code.Text)) & vbCrLf _
                '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & NewStr_AddressDetail(DataUtil.ConvertToString(txtByCom_CH03.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " &
                                                            BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " &
                                                            BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " &
                                                            BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf &
                                                            String_Total_USDOnly_New_FORME(txtDisplayUDS7.Text, txtCurrency_Code.Text, TFOB, DataUtil.ConvertToString(txtshow_check.Text), TUSD, TUSDOther) &
                                                            txtback_country.Text & vbCrLf &
                                                            "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 3 'T3_place_Exibition
                'All_str = "HS. CODE. " & tariff_All(DataUtil.ConvertToString(txttariff_code.Text)) & vbCrLf _
                '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & NewStr_AddressDetail(DataUtil.ConvertToString(txtByCom_CH03.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " &
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " &
                                                                BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " &
                                                                BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf &
                                                                String_Total_USDOnly_New_FORME(txtDisplayUDS7.Text, txtCurrency_Code.Text, TFOB, DataUtil.ConvertToString(txtshow_check.Text), TUSD, TUSDOther) &
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 4 '��������
                'All_str = "HS. CODE. " & tariff_All(DataUtil.ConvertToString(txttariff_code.Text)) & vbCrLf _
                '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & NewStr_AddressDetail(DataUtil.ConvertToString(txtByCom_CH03.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " &
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " &
                                                                BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " &
                                                                BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf &
                                                                String_Total_USDOnly_New_FORME(txtDisplayUDS7.Text, txtCurrency_Code.Text, TFOB, DataUtil.ConvertToString(txtshow_check.Text), TUSD, TUSDOther) &
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 5 'T2_back_country
                'All_str = "HS. CODE. " & tariff_All(DataUtil.ConvertToString(txttariff_code.Text)) & vbCrLf _
                '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & NewStr_AddressDetail(DataUtil.ConvertToString(txtByCom_CH03.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " &
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " &
                                                                BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " &
                                                                BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf &
                                                                String_Total_USDOnly_New_FORME(txtDisplayUDS7.Text, txtCurrency_Code.Text, TFOB, DataUtil.ConvertToString(txtshow_check.Text), TUSD, TUSDOther) &
                                                                DataUtil.ConvertToString(txtsupplementary_details.Value) & vbCrLf &
                                                                _TempB2B & vbNewLine &
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 6 'T3_place_Exibition
                'All_str = "HS. CODE. " & tariff_All(DataUtil.ConvertToString(txttariff_code.Text)) & vbCrLf _
                '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & NewStr_AddressDetail(DataUtil.ConvertToString(txtByCom_CH03.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " &
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " &
                                                                BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " &
                                                                BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf &
                                                                String_Total_USDOnly_New_FORME(txtDisplayUDS7.Text, txtCurrency_Code.Text, TFOB, DataUtil.ConvertToString(txtshow_check.Text), TUSD, TUSDOther) &
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 7 'T1_third_country,T2_back_country
                'All_str = "HS. CODE. " & tariff_All(DataUtil.ConvertToString(txttariff_code.Text)) & vbCrLf _
                '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & NewStr_AddressDetail(DataUtil.ConvertToString(txtByCom_CH03.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " &
                                                                    BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " &
                                                                    BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " &
                                                                    BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf &
                                                                    String_Total_USDOnly_New_FORME(txtDisplayUDS7.Text, txtCurrency_Code.Text, TFOB, DataUtil.ConvertToString(txtshow_check.Text), TUSD, TUSDOther) &
                                                                    txtthird_country.Text & vbCrLf &
                                                                    txtback_country.Text & vbCrLf &
                                                                    "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 8 'T1_third_country,T3_place_Exibition
                'All_str = "HS. CODE. " & tariff_All(DataUtil.ConvertToString(txttariff_code.Text)) & vbCrLf _
                '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & NewStr_AddressDetail(DataUtil.ConvertToString(txtByCom_CH03.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " &
                                                                    BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " &
                                                                    BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " &
                                                                    BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf &
                                                                    String_Total_USDOnly_New_FORME(txtDisplayUDS7.Text, txtCurrency_Code.Text, TFOB, DataUtil.ConvertToString(txtshow_check.Text), TUSD, TUSDOther) &
                                                                    txtthird_country.Text & vbCrLf &
                                                                    "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 9 'T1_third_country
                'All_str = "HS. CODE. " & tariff_All(DataUtil.ConvertToString(txttariff_code.Text)) & vbCrLf _
                '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & NewStr_AddressDetail(DataUtil.ConvertToString(txtByCom_CH03.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " &
                                                                    BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " &
                                                                    BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " &
                                                                    BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf &
                                                                    String_Total_USDOnly_New_FORME(txtDisplayUDS7.Text, txtCurrency_Code.Text, TFOB, DataUtil.ConvertToString(txtshow_check.Text), TUSD, TUSDOther) &
                                                                    txtthird_country.Text & vbCrLf &
                                                                    "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
        End Select
    End Sub
    Sub All_CheckThird_3(ByVal count_data As Integer)
        Title_StrDetail()
        Select Case Check_Third(DataUtil.ConvertToString(txtthird_country.Value),
                                DataUtil.ConvertToString(txtback_country.Value),
                                DataUtil.ConvertToString(txtplace_exibition.Value))
            Case 1
                'All_str = "HS. CODE. " & tariff_All(DataUtil.ConvertToString(txttariff_code.Text)) & vbCrLf _
                '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & NewStr_AddressDetail(DataUtil.ConvertToString(txtByCom_CH03.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " &
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " &
                                                                BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf &
                                                                String_Total_USDOnly_New_FORME(txtDisplayUDS7.Text, txtCurrency_Code.Text, TFOB, DataUtil.ConvertToString(txtshow_check.Text), TUSD, TUSDOther) &
                                                                txtthird_country.Text & vbCrLf &
                                                                txtback_country.Text & vbCrLf &
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 2 'T2_back_country,T3_place_Exibition
                'All_str = "HS. CODE. " & tariff_All(DataUtil.ConvertToString(txttariff_code.Text)) & vbCrLf _
                '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & NewStr_AddressDetail(DataUtil.ConvertToString(txtByCom_CH03.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " &
                                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " &
                                                                                BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf &
                                                                                String_Total_USDOnly_New_FORME(txtDisplayUDS7.Text, txtCurrency_Code.Text, TFOB, DataUtil.ConvertToString(txtshow_check.Text), TUSD, TUSDOther) &
                                                                                txtback_country.Text & vbCrLf &
                                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 3 'T3_place_Exibition
                'All_str = "HS. CODE. " & tariff_All(DataUtil.ConvertToString(txttariff_code.Text)) & vbCrLf _
                '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & NewStr_AddressDetail(DataUtil.ConvertToString(txtByCom_CH03.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " &
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " &
                                                                BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf &
                                                                String_Total_USDOnly_New_FORME(txtDisplayUDS7.Text, txtCurrency_Code.Text, TFOB, DataUtil.ConvertToString(txtshow_check.Text), TUSD, TUSDOther) &
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 4 '��������
                'All_str = "HS. CODE. " & tariff_All(DataUtil.ConvertToString(txttariff_code.Text)) & vbCrLf _
                '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & NewStr_AddressDetail(DataUtil.ConvertToString(txtByCom_CH03.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " &
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " &
                                                                BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf &
                                                                String_Total_USDOnly_New_FORME(txtDisplayUDS7.Text, txtCurrency_Code.Text, TFOB, DataUtil.ConvertToString(txtshow_check.Text), TUSD, TUSDOther) &
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 5 'T2_back_country
                'All_str = "HS. CODE. " & tariff_All(DataUtil.ConvertToString(txttariff_code.Text)) & vbCrLf _
                '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & NewStr_AddressDetail(DataUtil.ConvertToString(txtByCom_CH03.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " &
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " &
                                                                BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf &
                                                                String_Total_USDOnly_New_FORME(txtDisplayUDS7.Text, txtCurrency_Code.Text, TFOB, DataUtil.ConvertToString(txtshow_check.Text), TUSD, TUSDOther) &
                                                                DataUtil.ConvertToString(txtsupplementary_details.Value) & vbCrLf &
                                                                _TempB2B & vbNewLine &
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 6 'T3_place_Exibition
                'All_str = "HS. CODE. " & tariff_All(DataUtil.ConvertToString(txttariff_code.Text)) & vbCrLf _
                '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & NewStr_AddressDetail(DataUtil.ConvertToString(txtByCom_CH03.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " &
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " &
                                                                BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf &
                                                                String_Total_USDOnly_New_FORME(txtDisplayUDS7.Text, txtCurrency_Code.Text, TFOB, DataUtil.ConvertToString(txtshow_check.Text), TUSD, TUSDOther) &
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 7 'T1_third_country,T2_back_country
                'All_str = "HS. CODE. " & tariff_All(DataUtil.ConvertToString(txttariff_code.Text)) & vbCrLf _
                '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & NewStr_AddressDetail(DataUtil.ConvertToString(txtByCom_CH03.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " &
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " &
                                                                BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf &
                                                                String_Total_USDOnly_New_FORME(txtDisplayUDS7.Text, txtCurrency_Code.Text, TFOB, DataUtil.ConvertToString(txtshow_check.Text), TUSD, TUSDOther) &
                                                                txtthird_country.Text & vbCrLf &
                                                                txtback_country.Text & vbCrLf &
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 8 'T1_third_country,T3_place_Exibition
                'All_str = "HS. CODE. " & tariff_All(DataUtil.ConvertToString(txttariff_code.Text)) & vbCrLf _
                '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & NewStr_AddressDetail(DataUtil.ConvertToString(txtByCom_CH03.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " &
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " &
                                                                BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf &
                                                                String_Total_USDOnly_New_FORME(txtDisplayUDS7.Text, txtCurrency_Code.Text, TFOB, DataUtil.ConvertToString(txtshow_check.Text), TUSD, TUSDOther) &
                                                                txtthird_country.Text & vbCrLf &
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 9 'T1_third_country
                'All_str = "HS. CODE. " & tariff_All(DataUtil.ConvertToString(txttariff_code.Text)) & vbCrLf _
                '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & NewStr_AddressDetail(DataUtil.ConvertToString(txtByCom_CH03.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " &
                                                                    BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " &
                                                                    BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf &
                                                                    String_Total_USDOnly_New_FORME(txtDisplayUDS7.Text, txtCurrency_Code.Text, TFOB, DataUtil.ConvertToString(txtshow_check.Text), TUSD, TUSDOther) &
                                                                    txtthird_country.Text & vbCrLf &
                                                                    "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
        End Select
    End Sub
    Sub All_CheckThird_2(ByVal count_data As Integer)
        Title_StrDetail()
        Select Case Check_Third(DataUtil.ConvertToString(txtthird_country.Value),
                                DataUtil.ConvertToString(txtback_country.Value),
                                DataUtil.ConvertToString(txtplace_exibition.Value))
            Case 1
                'All_str = "HS. CODE. " & tariff_All(DataUtil.ConvertToString(txttariff_code.Text)) & vbCrLf _
                '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & NewStr_AddressDetail(DataUtil.ConvertToString(txtByCom_CH03.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " &
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf &
                                                                String_Total_USDOnly_New_FORME(txtDisplayUDS7.Text, txtCurrency_Code.Text, TFOB, DataUtil.ConvertToString(txtshow_check.Text), TUSD, TUSDOther) &
                                                                txtthird_country.Text & vbCrLf &
                                                                txtback_country.Text & vbCrLf &
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 2 'T2_back_country,T3_place_Exibition
                'All_str = "HS. CODE. " & tariff_All(DataUtil.ConvertToString(txttariff_code.Text)) & vbCrLf _
                '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & NewStr_AddressDetail(DataUtil.ConvertToString(txtByCom_CH03.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " &
                                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf &
                                                                                String_Total_USDOnly_New_FORME(txtDisplayUDS7.Text, txtCurrency_Code.Text, TFOB, DataUtil.ConvertToString(txtshow_check.Text), TUSD, TUSDOther) &
                                                                                txtback_country.Text & vbCrLf &
                                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 3 'T3_place_Exibition
                'All_str = "HS. CODE. " & tariff_All(DataUtil.ConvertToString(txttariff_code.Text)) & vbCrLf _
                '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & NewStr_AddressDetail(DataUtil.ConvertToString(txtByCom_CH03.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " &
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf &
                                                                String_Total_USDOnly_New_FORME(txtDisplayUDS7.Text, txtCurrency_Code.Text, TFOB, DataUtil.ConvertToString(txtshow_check.Text), TUSD, TUSDOther) &
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 4 '��������
                'All_str = "HS. CODE. " & tariff_All(DataUtil.ConvertToString(txttariff_code.Text)) & vbCrLf _
                '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & NewStr_AddressDetail(DataUtil.ConvertToString(txtByCom_CH03.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " &
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf &
                                                                String_Total_USDOnly_New_FORME(txtDisplayUDS7.Text, txtCurrency_Code.Text, TFOB, DataUtil.ConvertToString(txtshow_check.Text), TUSD, TUSDOther) &
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 5 'T2_back_country
                'All_str = "HS. CODE. " & tariff_All(DataUtil.ConvertToString(txttariff_code.Text)) & vbCrLf _
                '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & NewStr_AddressDetail(DataUtil.ConvertToString(txtByCom_CH03.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " &
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf &
                                                                String_Total_USDOnly_New_FORME(txtDisplayUDS7.Text, txtCurrency_Code.Text, TFOB, DataUtil.ConvertToString(txtshow_check.Text), TUSD, TUSDOther) &
                                                                DataUtil.ConvertToString(txtsupplementary_details.Value) & vbCrLf &
                                                                _TempB2B & vbNewLine &
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 6 'T3_place_Exibition
                'All_str = "HS. CODE. " & tariff_All(DataUtil.ConvertToString(txttariff_code.Text)) & vbCrLf _
                '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & NewStr_AddressDetail(DataUtil.ConvertToString(txtByCom_CH03.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " &
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf &
                                                                String_Total_USDOnly_New_FORME(txtDisplayUDS7.Text, txtCurrency_Code.Text, TFOB, DataUtil.ConvertToString(txtshow_check.Text), TUSD, TUSDOther) &
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 7 'T1_third_country,T2_back_country
                'All_str = "HS. CODE. " & tariff_All(DataUtil.ConvertToString(txttariff_code.Text)) & vbCrLf _
                '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & NewStr_AddressDetail(DataUtil.ConvertToString(txtByCom_CH03.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " &
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " &
                                                                String_Total_USDOnly_New_FORME(txtDisplayUDS7.Text, txtCurrency_Code.Text, TFOB, DataUtil.ConvertToString(txtshow_check.Text), TUSD, TUSDOther) &
                                                                txtthird_country.Text & vbCrLf &
                                                                txtback_country.Text & vbCrLf &
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 8 'T1_third_country,T3_place_Exibition
                'All_str = "HS. CODE. " & tariff_All(DataUtil.ConvertToString(txttariff_code.Text)) & vbCrLf _
                '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & NewStr_AddressDetail(DataUtil.ConvertToString(txtByCom_CH03.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " &
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf &
                                                                String_Total_USDOnly_New_FORME(txtDisplayUDS7.Text, txtCurrency_Code.Text, TFOB, DataUtil.ConvertToString(txtshow_check.Text), TUSD, TUSDOther) &
                                                                txtthird_country.Text & vbCrLf &
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 9 'T1_third_country
                'All_str = "HS. CODE. " & tariff_All(DataUtil.ConvertToString(txttariff_code.Text)) & vbCrLf _
                '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & NewStr_AddressDetail(DataUtil.ConvertToString(txtByCom_CH03.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " &
                                                                    BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf &
                                                                    String_Total_USDOnly_New_FORME(txtDisplayUDS7.Text, txtCurrency_Code.Text, TFOB, DataUtil.ConvertToString(txtshow_check.Text), TUSD, TUSDOther) &
                                                                    txtthird_country.Text & vbCrLf &
                                                                    "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
        End Select
    End Sub
    Sub All_CheckThird_1(ByVal count_data As Integer)
        Title_StrDetail()
        Select Case Check_Third(DataUtil.ConvertToString(txtthird_country.Value),
                                DataUtil.ConvertToString(txtback_country.Value),
                                DataUtil.ConvertToString(txtplace_exibition.Value))
            Case 1
                'All_str = "HS. CODE. " & tariff_All(DataUtil.ConvertToString(txttariff_code.Text)) & vbCrLf _
                '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    If (Not txtquantity1.Text = String.Empty = True) And DataUtil.ConvertToString(txtquantity1.Text) <> "" And CommonUtility.Get_Decimal(IIf(Not (txtquantity1.Text = String.Empty) = False, 0, txtquantity1.Value)) <> 0 Then
                        txtT_product.Text = All_str & NewStr_AddressDetail(DataUtil.ConvertToString(txtByCom_CH03.Text))
                        GroupFooter1.Visible = True
                        txtTotalAll.Visible = True
                        txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf &
                                                                                        String_Total_USDOnly_New_FORME(txtDisplayUDS7.Text, txtCurrency_Code.Text, TFOB, DataUtil.ConvertToString(txtshow_check.Text), TUSD, TUSDOther) &
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
            Case 2 'T2_back_country,T3_place_Exibition
                'All_str = "HS. CODE. " & tariff_All(DataUtil.ConvertToString(txttariff_code.Text)) & vbCrLf _
                '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    If (Not txtquantity1.Text = String.Empty = True) And DataUtil.ConvertToString(txtquantity1.Text) <> "" And CommonUtility.Get_Decimal(IIf(Not (txtquantity1.Text = String.Empty) = False, 0, txtquantity1.Value)) <> 0 Then
                        txtT_product.Text = All_str & NewStr_AddressDetail(DataUtil.ConvertToString(txtByCom_CH03.Text))
                        GroupFooter1.Visible = True
                        txtTotalAll.Visible = True
                        txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf &
                                            String_Total_USDOnly_New_FORME(txtDisplayUDS7.Text, txtCurrency_Code.Text, TFOB, DataUtil.ConvertToString(txtshow_check.Text), TUSD, TUSDOther) &
                                                                                                       txtback_country.Text & vbCrLf &
                                                                                                       "       _____________________________"
                    Else
                        txtT_product.Text = All_str &
                                           "        " &
                                                                                                       txtback_country.Text & vbCrLf &
                                                                                                       "       _____________________________"
                    End If

                Else
                    txtT_product.Text = All_str
                End If
            Case 3 'T3_place_Exibition
                'All_str = "HS. CODE. " & tariff_All(DataUtil.ConvertToString(txttariff_code.Text)) & vbCrLf _
                '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    If (Not txtquantity1.Text = String.Empty = True) And DataUtil.ConvertToString(txtquantity1.Text) <> "" And CommonUtility.Get_Decimal(IIf(Not (txtquantity1.Text = String.Empty) = False, 0, txtquantity1.Value)) <> 0 Then
                        txtT_product.Text = All_str & NewStr_AddressDetail(DataUtil.ConvertToString(txtByCom_CH03.Text))
                        GroupFooter1.Visible = True
                        txtTotalAll.Visible = True
                        txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf &
                                            String_Total_USDOnly_New_FORME(txtDisplayUDS7.Text, txtCurrency_Code.Text, TFOB, DataUtil.ConvertToString(txtshow_check.Text), TUSD, TUSDOther) &
                                                                                        "       _____________________________"
                    Else
                        txtT_product.Text = All_str &
                                            vbCrLf &
                                                                                        "       _____________________________"
                    End If

                Else
                    txtT_product.Text = All_str
                End If
            Case 4 '��������
                'All_str = "HS. CODE. " & tariff_All(DataUtil.ConvertToString(txttariff_code.Text)) & vbCrLf _
                '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    If (Not txtquantity1.Text = String.Empty = True) And DataUtil.ConvertToString(txtquantity1.Text) <> "" And CommonUtility.Get_Decimal(IIf(Not (txtquantity1.Text = String.Empty) = False, 0, txtquantity1.Value)) <> 0 Then
                        txtT_product.Text = All_str & NewStr_AddressDetail(DataUtil.ConvertToString(txtByCom_CH03.Text))
                        GroupFooter1.Visible = True
                        txtTotalAll.Visible = True
                        txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf &
                                                    String_Total_USDOnly_New_FORME(txtDisplayUDS7.Text, txtCurrency_Code.Text, TFOB, DataUtil.ConvertToString(txtshow_check.Text), TUSD, TUSDOther) &
                                                                                        "       _____________________________"
                    Else
                        txtT_product.Text = All_str &
                                            "       _____________________________"
                    End If

                Else
                    txtT_product.Text = All_str
                End If
            Case 5 'T2_back_country
                'All_str = "HS. CODE. " & tariff_All(DataUtil.ConvertToString(txttariff_code.Text)) & vbCrLf _
                '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    If (Not txtquantity1.Text = String.Empty = True) And DataUtil.ConvertToString(txtquantity1.Text) <> "" And CommonUtility.Get_Decimal(IIf(Not (txtquantity1.Text = String.Empty) = False, 0, txtquantity1.Value)) <> 0 Then
                        txtT_product.Text = All_str & NewStr_AddressDetail(DataUtil.ConvertToString(txtByCom_CH03.Text))
                        GroupFooter1.Visible = True
                        txtTotalAll.Visible = True
                        txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf &
                                                               String_Total_USDOnly_New_FORME(txtDisplayUDS7.Text, txtCurrency_Code.Text, TFOB, DataUtil.ConvertToString(txtshow_check.Text), TUSD, TUSDOther) &
                                                                DataUtil.ConvertToString(txtsupplementary_details.Value) & vbCrLf &
                                                                _TempB2B & vbNewLine &
                                                                                       "       _____________________________"
                    Else
                        txtT_product.Text = All_str &
                        _TempB2B & vbNewLine &
                                           "       _____________________________"
                    End If

                Else
                    txtT_product.Text = All_str
                End If
            Case 6 'T3_place_Exibition
                'All_str = "HS. CODE. " & tariff_All(DataUtil.ConvertToString(txttariff_code.Text)) & vbCrLf _
                '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    If (Not txtquantity1.Text = String.Empty = True) And DataUtil.ConvertToString(txtquantity1.Text) <> "" And CommonUtility.Get_Decimal(IIf(Not (txtquantity1.Text = String.Empty) = False, 0, txtquantity1.Value)) <> 0 Then
                        txtT_product.Text = All_str & NewStr_AddressDetail(DataUtil.ConvertToString(txtByCom_CH03.Text))
                        GroupFooter1.Visible = True
                        txtTotalAll.Visible = True
                        txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf &
                                                    String_Total_USDOnly_New_FORME(txtDisplayUDS7.Text, txtCurrency_Code.Text, TFOB, DataUtil.ConvertToString(txtshow_check.Text), TUSD, TUSDOther) &
                                                                                        "       _____________________________"
                    Else
                        txtT_product.Text = All_str &
                                            vbCrLf &
                                                                                        "       _____________________________"
                    End If

                Else
                    txtT_product.Text = All_str
                End If
            Case 7 'T1_third_country,T2_back_country
                'All_str = "HS. CODE. " & tariff_All(DataUtil.ConvertToString(txttariff_code.Text)) & vbCrLf _
                '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    If (Not txtquantity1.Text = String.Empty = True) And DataUtil.ConvertToString(txtquantity1.Text) <> "" And CommonUtility.Get_Decimal(IIf(Not (txtquantity1.Text = String.Empty) = False, 0, txtquantity1.Value)) <> 0 Then
                        txtT_product.Text = All_str & NewStr_AddressDetail(DataUtil.ConvertToString(txtByCom_CH03.Text))
                        GroupFooter1.Visible = True
                        txtTotalAll.Visible = True
                        txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf &
                                                    String_Total_USDOnly_New_FORME(txtDisplayUDS7.Text, txtCurrency_Code.Text, TFOB, DataUtil.ConvertToString(txtshow_check.Text), TUSD, TUSDOther) &
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
            Case 8 'T1_third_country,T3_place_Exibition
                'All_str = "HS. CODE. " & tariff_All(DataUtil.ConvertToString(txttariff_code.Text)) & vbCrLf _
                '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    If (Not txtquantity1.Text = String.Empty = True) And DataUtil.ConvertToString(txtquantity1.Text) <> "" And CommonUtility.Get_Decimal(IIf(Not (txtquantity1.Text = String.Empty) = False, 0, txtquantity1.Value)) <> 0 Then
                        txtT_product.Text = All_str & NewStr_AddressDetail(DataUtil.ConvertToString(txtByCom_CH03.Text))
                        GroupFooter1.Visible = True
                        txtTotalAll.Visible = True
                        txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf &
                            String_Total_USDOnly_New_FORME(txtDisplayUDS7.Text, txtCurrency_Code.Text, TFOB, DataUtil.ConvertToString(txtshow_check.Text), TUSD, TUSDOther) &
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
            Case 9 'T1_third_country
                'All_str = "HS. CODE. " & tariff_All(DataUtil.ConvertToString(txttariff_code.Text)) & vbCrLf _
                '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    If (Not txtquantity1.Text = String.Empty = True) And DataUtil.ConvertToString(txtquantity1.Text) <> "" And CommonUtility.Get_Decimal(IIf(Not (txtquantity1.Text = String.Empty) = False, 0, txtquantity1.Value)) <> 0 Then
                        txtT_product.Text = All_str & NewStr_AddressDetail(DataUtil.ConvertToString(txtByCom_CH03.Text))
                        GroupFooter1.Visible = True
                        txtTotalAll.Visible = True
                        txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf &
                           String_Total_USDOnly_New_FORME(txtDisplayUDS7.Text, txtCurrency_Code.Text, TFOB, DataUtil.ConvertToString(txtshow_check.Text), TUSD, TUSDOther) &
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
        Select Case Check_Third(DataUtil.ConvertToString(txtthird_country.Value),
                                DataUtil.ConvertToString(txtback_country.Value),
                                DataUtil.ConvertToString(txtplace_exibition.Value))
            Case 1
                'All_str = "HS. CODE. " & tariff_All(DataUtil.ConvertToString(txttariff_code.Text)) & vbCrLf _
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
            Case 2 'T2_back_country,T3_place_Exibition
                'All_str = "HS. CODE. " & tariff_All(DataUtil.ConvertToString(txttariff_code.Text)) & vbCrLf _
                '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str &
                    txtback_country.Text & vbCrLf &
                                                                                txtplace_exibition.Text & vbCrLf &
                                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 3 'T3_place_Exibition
                'All_str = "HS. CODE. " & tariff_All(DataUtil.ConvertToString(txttariff_code.Text)) & vbCrLf _
                '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str &
                     txtplace_exibition.Text & vbCrLf &
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 4 '��������
                'All_str = "HS. CODE. " & tariff_All(DataUtil.ConvertToString(txttariff_code.Text)) & vbCrLf _
                '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str &
                     "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 5 'T2_back_country
                'All_str = "HS. CODE. " & tariff_All(DataUtil.ConvertToString(txttariff_code.Text)) & vbCrLf _
                '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & vbCrLf &
                    _TempB2B & vbNewLine &
                    "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 6 'T3_place_Exibition
                'All_str = "HS. CODE. " & tariff_All(DataUtil.ConvertToString(txttariff_code.Text)) & vbCrLf _
                '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str &
                     txtplace_exibition.Text & vbCrLf &
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 7 'T1_third_country,T2_back_country
                'All_str = "HS. CODE. " & tariff_All(DataUtil.ConvertToString(txttariff_code.Text)) & vbCrLf _
                '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str &
                    txtthird_country.Text & vbCrLf &
                                                                txtback_country.Text & vbCrLf &
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 8 'T1_third_country,T3_place_Exibition
                'All_str = "HS. CODE. " & tariff_All(DataUtil.ConvertToString(txttariff_code.Text)) & vbCrLf _
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
                'All_str = "HS. CODE. " & tariff_All(DataUtil.ConvertToString(txttariff_code.Text)) & vbCrLf _
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

    '��᷹ ���͹� ��ͧ��� 1 ���ͧ�ҡ�Դ OB,OC  ���� ����Сͺ��õ�ͧ��͹�ͧ����ͧ���� txtob_address.text
    Function NewStr_AddressDetail(ByVal strOB_address As String) As String
        Dim Substr_OB_add As String = ""
        If strOB_address <> "" And strOB_address.Trim <> "" And Not strOB_address = String.Empty = True Then
            Substr_OB_add = strOB_address & vbCrLf
        Else
            Substr_OB_add = ""
        End If
        Return Substr_OB_add
    End Function

    Private Sub rpt3_ediFORM4_2_pr_PageEnd(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PageEnd
        head_height = PageHeader1.Height

        Dim f As New System.Drawing.Font("BrowalliaUPC", 11)

        Dim x_txtmarks As Single = 0.81
        Dim y_txtmarks As Single = CSng(head_height) '4.61
        'Dim width As Single = 6.0F
        Dim width_txtmarks As Single = 0.94
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

    Private Sub rpt3_ediFORM4_2_pr_ReportEnd(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.ReportEnd
        If updatePrintTotal(txtinvh_run_auto.Value, CStr(Me.Document.Pages.Count)) = True Then

        End If
    End Sub

    Private Sub rpt3_ediFORM4_2_pr_ReportStart(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.ReportStart
        'Me.ReportInfo1.FormatString = "Page {PageNumber} of {PageCount} on {RunDateTime}"
        Me.ReportInfo1.FormatString = "Page : {PageNumber} of {PageCount}"
    End Sub
#Region "Code sign image"
    'by rut sign image � report
    '�� reports_CardID,search_imageForm ����� Module_CallBathEng
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
                    Dim u As String = reports_CardID(arr_(0).ToString) '"http://edi.dft.go.th/" & "/Portals/0/Images_Sign/0405533000247/2013/SealPerson/3660400121998/3660400121998_New_��͹ѹ�� NK1.jpg"
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

                txtTemp_Bangkok01.Text = String_SiteNameReport01(txtTemp_SiteSend.Text).ToUpper
        End Select
        txtTemp_Date.Text = String_DateSiteReport(txtapprove_date.Value)
    End Sub
#End Region
End Class
