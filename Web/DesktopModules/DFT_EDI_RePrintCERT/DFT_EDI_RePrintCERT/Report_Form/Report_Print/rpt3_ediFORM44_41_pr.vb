Imports DataDynamics.ActiveReports
Imports DataDynamics.ActiveReports.Document

Imports Microsoft.ApplicationBlocks.Data
Imports System.Data.SqlClient
Imports System.Data
Imports System.Text
Imports DFT.Dotnetnuke.ClassLibrary
Imports ReportPrintClass
Public Class rpt3_ediFORM44_41_pr
    'by rut D new
    Dim TGross, TNet, TFob, TUSD, TUSDOther As Decimal
    Public _TempB2B As String
    Public _IsAgent As Boolean
    Public _InvAgentType, _CompanyName_Agent, _Invoice_Agent As String
    Dim _TAgent As Decimal

    'Public Sub New()

    '     This call is required by the Windows Form Designer.
    '    InitializeComponent()
    '    Me.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.Custom
    '    Me.PageSettings.PaperName = "Fanfold 210 x 305 mm"
    '     Add any initialization after the InitializeComponent() call.

    'End Sub
    Dim Cpage As Integer
    Dim CpageNum As Integer
    Dim Str_invoice As String = ""
    Dim Str_USDinvoiceDetail As String
    'by rut d new
    Dim Str_USDinvoiceDetailSum As String
    '���͹����� ��Ѻ�� �ѹ��� 2-12-2012 ����ǡѺ RVC �ͧ�ѹ Case 2,8 ��ͧ�ʴ���Ť�� ������������ʴ�
    'begin RVC
    Dim Check_CaseRVCCount As Integer
    'end RVC

    Sub check_Issued_()
        Dim date_now As Date = Now.Date
        Dim set_issued As Integer

        Dim str_1, str_2, str_3 As String
        str_1 = CommonUtility.Get_DateTime(txtdeparture_date.Value).Day
        str_2 = CInt(str_1) + 1
        str_3 = CInt(str_1) + 2

        '੾��㹹���������ǡѺ�͹ print ��͹ �����͹ issued �ա��ͧ��
        Select Case CInt(date_now.Day)
            Case CInt(str_1)
                Pic_ch7_Issued.Visible = False
                checkIssued_date(CommonUtility.Get_StringValue(txtinvh_run_auto.Value), CommonUtility.Get_StringValue("0"))
            Case CInt(str_2)
                Pic_ch7_Issued.Visible = False
                checkIssued_date(CommonUtility.Get_StringValue(txtinvh_run_auto.Value), CommonUtility.Get_StringValue("0"))
            Case CInt(str_3)
                Pic_ch7_Issued.Visible = False
                checkIssued_date(CommonUtility.Get_StringValue(txtinvh_run_auto.Value), CommonUtility.Get_StringValue("0"))
            Case Else
                Pic_ch7_Issued.Visible = True
                checkIssued_date(CommonUtility.Get_StringValue(txtinvh_run_auto.Value), CommonUtility.Get_StringValue("1"))
        End Select
    End Sub
    Private Sub PageFooter1_Format(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PageFooter1.Format
        'Check_show_check(Len(CommonUtility.Get_StringValue(txtshow_check.Text)))
        check_show_check()

        'check_Issued_()
        ''1 ��� �ʴ�,0 ����ʴ�
        'Select Case CommonUtility.Get_StringValue(txtCheckIssued.Text)
        '    Case 1
        '        Pic_ch7_Issued.Visible = True
        '    Case Else
        '        Pic_ch7_Issued.Visible = False
        'End Select
        'If Mid(CommonUtility.Get_StringValue(checkNull_Show(txtshow_check.Text)), 3, 1) = 1 Then
        '    txtTemp_back_country.Text = txtback_country.Text
        'Else
        '    txtTemp_back_country.Text = "THAILAND"
        'End If

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
    End Sub
    'by rut Title New Begin-----------------
    Dim Str_TitlePage As String
    'by rut Title New End-------------------
    'by rut ��Ѻ page end ���͡�˹�
    Dim head_height As String
    Private Sub PageHeader1_Format(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PageHeader1.Format
        CpageNum += 1
        txtreference_code2_Temp.Text = txtreference_code2.Text
        '===============================================================

        'txtPage2.Text = Cpage
        '===============================================================
        Head_Checkcompany_v1()
        Head_Checkdestination_v2()

        'testxx.Text = C_TotalRowDe.Text

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
    End Sub

    'company
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

        ElseIf Mid(txtdest_remark.Text, 1, 5) <> "O/TOB" Then
            txtCompany_Check_1.Text = txtcompany_name.Text & " " & txtcompany_address.Text & " " & txtcompany_province.Text _
                            & " " & txtcompany_country.Text & " " & txtob_address.Text & _
                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") & _
                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") _
                            & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID: " & txtcompany_taxno.Text, "") & _
                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")

        ElseIf Mid(txtdest_remark.Text, 1, 5) <> "O/TCO" Then
            txtCompany_Check_1.Text = txtob_address.Text & txtcompany_name.Text & " " & txtcompany_address.Text & " " & txtcompany_province.Text _
                            & " " & txtcompany_country.Text & _
                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") & _
                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") _
                            & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID: " & txtcompany_taxno.Text, "") & _
                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")

        End If
    End Sub
    Sub Head_Checkdestination_v2()
        If Mid(txtdest_remark1.Text, 1, 3) = "C/O" Then
            txtdestination_Check2.Text = txtob_dest_address.Text & " CARE OF " & txtdestination_company.Text & " " & txtdestination_address.Text _
                                & " " & txtdestination_province.Text & " " & txtdest_Receive_country.Text & _
                                IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") & _
                                IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") & _
                                IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID:  " & txtdestination_taxid.Text, "") & _
                                IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") & _
                                IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "")

        ElseIf Mid(txtdest_remark1.Text, 1, 3) = "O/B" Then
            txtdestination_Check2.Text = txtdestination_company.Text & " " & txtdestination_address.Text & " " & _
                                    txtdestination_province.Text & " " & txtdest_Receive_country.Text & _
                                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") & _
                                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                    & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") & _
                                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") & _
                                    " ON BEHALF OF " & txtob_dest_address.Text & IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "")

        ElseIf Mid(txtdest_remark1.Text, 2, 1) <> "/" Then
            txtdestination_Check2.Text = txtdestination_company.Text & " " & txtdestination_address.Text & " " & txtdestination_province.Text _
                                & " " & txtdest_Receive_country.Text & _
                                IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") & _
                                IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") & _
                                IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") & _
                                IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "")

        ElseIf Mid(txtdest_remark1.Text, 1, 5) <> "O/TOB" Then
            txtdestination_Check2.Text = txtdestination_company.Text & " " & txtdestination_address.Text & " " & txtdestination_province.Text _
                            & " " & txtdest_Receive_country.Text & " " & txtob_dest_address.Text & _
                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") & _
                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                            & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") & _
                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") & _
                            IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "")

        ElseIf Mid(txtdest_remark1.Text, 1, 5) <> "O/TCO" Then
            txtdestination_Check2.Text = txtob_dest_address.Text & " " & txtdestination_company.Text & " " & txtdestination_address.Text & " " & txtdestination_province.Text _
                            & " " & txtdest_Receive_country.Text & _
                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") & _
                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                            & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") & _
                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") & _
                            IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "")

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

    ''����Ѻ form4_1 ��������բ�������ҷ����� show_check
    Function checkNull_Show(ByVal _sendTXT As String) As String
        Dim _reShow As String

        If _sendTXT = "" Then
            _reShow = "000000"
        Else
            _reShow = _sendTXT
        End If

        Return _reShow
    End Function

    Function confor_mat(ByVal d_date As Date) As String
        Dim str_for As String = Format(d_date, "d/MM/yyyy")
        Return str_for
    End Function

    'USD
    Function CallInvoice_() As String 'Check Invoince
        If _IsAgent = True Then ''<<< �� Inv ���˹��
            If _InvAgentType = 0 Then ''<<< �ʴ� Inv ���˹��
                Str_invoice = _Invoice_Agent.Replace("INVOICE NO ", "").Replace("DATE ", "")

            Else ''<<< �ʴ� Inv �µ������
                Select Case Check_numinvoiceAll(CommonUtility.Get_String(txtinvoice_no1.Text), CommonUtility.Get_String(txtinvoice_no2.Text), CommonUtility.Get_String(txtinvoice_no3.Text), CommonUtility.Get_String(txtinvoice_no4.Text), CommonUtility.Get_String(txtinvoice_no5.Text))
                    Case 1
                        If CommonUtility.Get_String(txtinvoice_no1.Text) <> "" And CommonUtility.Get_String(txtinvoice_date1.Text) <> "" Then
                            '1 invoice
                            Str_invoice = txtinvoice_no1.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date1.Text) <> "", confor_mat(CDate(txtinvoice_date1.Value).Date), Nothing)
                        End If
                    Case 2
                        If CommonUtility.Get_String(txtinvoice_no1.Text) <> "" And CommonUtility.Get_String(txtinvoice_date1.Text) <> "" And CommonUtility.Get_String(txtinvoice_no2.Text) <> "" And CommonUtility.Get_String(txtinvoice_date2.Text) <> "" Then
                            '2 invoice
                            Str_invoice = txtinvoice_no1.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date1.Text) <> "", confor_mat(CDate(txtinvoice_date1.Value).Date), Nothing) & vbNewLine _
                                                & txtinvoice_no2.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date2.Text) <> "", confor_mat(CDate(txtinvoice_date2.Value).Date), Nothing)
                        End If
                    Case 3
                        If CommonUtility.Get_String(txtinvoice_no1.Text) <> "" And CommonUtility.Get_String(txtinvoice_date1.Text) <> "" And CommonUtility.Get_String(txtinvoice_no2.Text) <> "" And CommonUtility.Get_String(txtinvoice_date2.Text) <> "" And CommonUtility.Get_String(txtinvoice_no3.Text) <> "" And CommonUtility.Get_String(txtinvoice_date3.Text) <> "" Then
                            '3 invoice
                            Str_invoice = txtinvoice_no1.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date1.Text) <> "", confor_mat(CDate(txtinvoice_date1.Value).Date), Nothing) & vbNewLine _
                                                & txtinvoice_no2.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date2.Text) <> "", confor_mat(CDate(txtinvoice_date2.Value).Date), Nothing) & vbNewLine _
                                                & txtinvoice_no3.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date3.Text) <> "", confor_mat(CDate(txtinvoice_date3.Value).Date), Nothing)
                        End If
                    Case 4
                        If CommonUtility.Get_String(txtinvoice_no1.Text) <> "" And CommonUtility.Get_String(txtinvoice_date1.Text) <> "" And CommonUtility.Get_String(txtinvoice_no2.Text) <> "" And CommonUtility.Get_String(txtinvoice_date2.Text) <> "" And CommonUtility.Get_String(txtinvoice_no3.Text) <> "" And CommonUtility.Get_String(txtinvoice_date3.Text) <> "" And CommonUtility.Get_String(txtinvoice_no4.Text) <> "" And CommonUtility.Get_String(txtinvoice_date4.Text) <> "" Then
                            '4 invoice
                            Str_invoice = txtinvoice_no1.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date1.Text) <> "", confor_mat(CDate(txtinvoice_date1.Value).Date), Nothing) & vbNewLine _
                                                & txtinvoice_no2.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date2.Text) <> "", confor_mat(CDate(txtinvoice_date2.Value).Date), Nothing) & vbNewLine _
                                                & txtinvoice_no3.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date3.Text) <> "", confor_mat(CDate(txtinvoice_date3.Value).Date), Nothing) & vbNewLine _
                                                & txtinvoice_no4.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date4.Text) <> "", confor_mat(CDate(txtinvoice_date4.Value).Date), Nothing)
                        End If
                    Case 5
                        If CommonUtility.Get_String(txtinvoice_no1.Text) <> "" And CommonUtility.Get_String(txtinvoice_date1.Text) <> "" And CommonUtility.Get_String(txtinvoice_no2.Text) <> "" And CommonUtility.Get_String(txtinvoice_date2.Text) <> "" And CommonUtility.Get_String(txtinvoice_no3.Text) <> "" And CommonUtility.Get_String(txtinvoice_date3.Text) <> "" And CommonUtility.Get_String(txtinvoice_no4.Text) <> "" And CommonUtility.Get_String(txtinvoice_date4.Text) <> "" And CommonUtility.Get_String(txtinvoice_no5.Text) <> "" And CommonUtility.Get_String(txtinvoice_date5.Text) <> "" Then
                            '5 invoice
                            Str_invoice = txtinvoice_no1.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date1.Text) <> "", confor_mat(CDate(txtinvoice_date1.Value).Date), Nothing) & vbNewLine _
                                                & txtinvoice_no2.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date2.Text) <> "", confor_mat(CDate(txtinvoice_date2.Value).Date), Nothing) & vbNewLine _
                                                & txtinvoice_no3.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date3.Text) <> "", confor_mat(CDate(txtinvoice_date3.Value).Date), Nothing) & vbNewLine _
                                                & txtinvoice_no4.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date4.Text) <> "", confor_mat(CDate(txtinvoice_date4.Value).Date), Nothing) & vbNewLine _
                                                & txtinvoice_no5.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date5.Text) <> "", confor_mat(CDate(txtinvoice_date5.Value).Date), Nothing)
                        End If
                End Select
            End If
        Else ''<<< ����� Inv ���˹��
            Select Case txtshow_check.Text
                Case "100000"
                    Select Case CommonUtility.Get_StringValue(Check_NULLALL(txtNumInvoice.Text))
                        Case ""
                            Str_invoice = Callinvoice_board(0)
                        Case Is <> ""
                            Str_invoice = txtNumInvoice.Text
                    End Select
                    'Str_invoice = txtNumInvoice.Text
                Case Else
                    Select Case Check_numinvoiceAll(CommonUtility.Get_String(txtinvoice_no1.Text), CommonUtility.Get_String(txtinvoice_no2.Text), CommonUtility.Get_String(txtinvoice_no3.Text), CommonUtility.Get_String(txtinvoice_no4.Text), CommonUtility.Get_String(txtinvoice_no5.Text))
                        Case 1
                            If CommonUtility.Get_String(txtinvoice_no1.Text) <> "" And CommonUtility.Get_String(txtinvoice_date1.Text) <> "" Then
                                '1 invoice
                                Str_invoice = txtinvoice_no1.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date1.Text) <> "", confor_mat(CDate(txtinvoice_date1.Value).Date), Nothing)
                            End If
                        Case 2
                            If CommonUtility.Get_String(txtinvoice_no1.Text) <> "" And CommonUtility.Get_String(txtinvoice_date1.Text) <> "" And CommonUtility.Get_String(txtinvoice_no2.Text) <> "" And CommonUtility.Get_String(txtinvoice_date2.Text) <> "" Then
                                '2 invoice
                                Str_invoice = txtinvoice_no1.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date1.Text) <> "", confor_mat(CDate(txtinvoice_date1.Value).Date), Nothing) & vbNewLine _
                                                    & txtinvoice_no2.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date2.Text) <> "", confor_mat(CDate(txtinvoice_date2.Value).Date), Nothing)
                            End If
                        Case 3
                            If CommonUtility.Get_String(txtinvoice_no1.Text) <> "" And CommonUtility.Get_String(txtinvoice_date1.Text) <> "" And CommonUtility.Get_String(txtinvoice_no2.Text) <> "" And CommonUtility.Get_String(txtinvoice_date2.Text) <> "" And CommonUtility.Get_String(txtinvoice_no3.Text) <> "" And CommonUtility.Get_String(txtinvoice_date3.Text) <> "" Then
                                '3 invoice
                                Str_invoice = txtinvoice_no1.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date1.Text) <> "", confor_mat(CDate(txtinvoice_date1.Value).Date), Nothing) & vbNewLine _
                                                    & txtinvoice_no2.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date2.Text) <> "", confor_mat(CDate(txtinvoice_date2.Value).Date), Nothing) & vbNewLine _
                                                    & txtinvoice_no3.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date3.Text) <> "", confor_mat(CDate(txtinvoice_date3.Value).Date), Nothing)
                            End If
                        Case 4
                            If CommonUtility.Get_String(txtinvoice_no1.Text) <> "" And CommonUtility.Get_String(txtinvoice_date1.Text) <> "" And CommonUtility.Get_String(txtinvoice_no2.Text) <> "" And CommonUtility.Get_String(txtinvoice_date2.Text) <> "" And CommonUtility.Get_String(txtinvoice_no3.Text) <> "" And CommonUtility.Get_String(txtinvoice_date3.Text) <> "" And CommonUtility.Get_String(txtinvoice_no4.Text) <> "" And CommonUtility.Get_String(txtinvoice_date4.Text) <> "" Then
                                '4 invoice
                                Str_invoice = txtinvoice_no1.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date1.Text) <> "", confor_mat(CDate(txtinvoice_date1.Value).Date), Nothing) & vbNewLine _
                                                    & txtinvoice_no2.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date2.Text) <> "", confor_mat(CDate(txtinvoice_date2.Value).Date), Nothing) & vbNewLine _
                                                    & txtinvoice_no3.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date3.Text) <> "", confor_mat(CDate(txtinvoice_date3.Value).Date), Nothing) & vbNewLine _
                                                    & txtinvoice_no4.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date4.Text) <> "", confor_mat(CDate(txtinvoice_date4.Value).Date), Nothing)
                            End If
                        Case 5
                            If CommonUtility.Get_String(txtinvoice_no1.Text) <> "" And CommonUtility.Get_String(txtinvoice_date1.Text) <> "" And CommonUtility.Get_String(txtinvoice_no2.Text) <> "" And CommonUtility.Get_String(txtinvoice_date2.Text) <> "" And CommonUtility.Get_String(txtinvoice_no3.Text) <> "" And CommonUtility.Get_String(txtinvoice_date3.Text) <> "" And CommonUtility.Get_String(txtinvoice_no4.Text) <> "" And CommonUtility.Get_String(txtinvoice_date4.Text) <> "" And CommonUtility.Get_String(txtinvoice_no5.Text) <> "" And CommonUtility.Get_String(txtinvoice_date5.Text) <> "" Then
                                '5 invoice
                                Str_invoice = txtinvoice_no1.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date1.Text) <> "", confor_mat(CDate(txtinvoice_date1.Value).Date), Nothing) & vbNewLine _
                                                    & txtinvoice_no2.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date2.Text) <> "", confor_mat(CDate(txtinvoice_date2.Value).Date), Nothing) & vbNewLine _
                                                    & txtinvoice_no3.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date3.Text) <> "", confor_mat(CDate(txtinvoice_date3.Value).Date), Nothing) & vbNewLine _
                                                    & txtinvoice_no4.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date4.Text) <> "", confor_mat(CDate(txtinvoice_date4.Value).Date), Nothing) & vbNewLine _
                                                    & txtinvoice_no5.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date5.Text) <> "", confor_mat(CDate(txtinvoice_date5.Value).Date), Nothing)
                            End If
                    End Select
            End Select
        End If

        Return Str_invoice
    End Function

    Sub CallInvoiceCheck() 'Check Invoince
        Select Case txtshow_check.Text
            Case "100000"
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

    'Sub All_CheckThird_5(ByVal count_data As Integer)
    '    Dim All_str As String
    '    Select Case Check_Third(CommonUtility.Get_StringValue(txtthird_country.Value), _
    '                            CommonUtility.Get_StringValue(txtback_country.Value), _
    '                            CommonUtility.Get_StringValue(txtplace_exibition.Value))
    '        Case 1
    '            All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '                                          & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
    '            If C_TotalRowDe.Text = count_data Then
    '                txtT_product.Text = All_str & Form_NewStr_AddressDetail(CommonUtility.Get_StringValue(txtByCom_CH03.Text))
    '                GroupFooter1.Visible = True
    '                txtTotalAll.Visible = True
    '                txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
    '                                                            BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
    '                                                            BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " & _
    '                                                            BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf & "        " & _
    '                                                            BahtOnly(txtquantity5.Text, txtq_unit_code5.Text) & vbCrLf & _
    '                                                            String_Total_USDOnly_New(txtDisplayUDS7.Text, txtCurrency_Code.Text, IIf(_IsAgent = False, TFob, _TAgent), CommonUtility.Get_StringValue(txtshow_check.Text), TUSD, TUSDOther) & "        " & _
    '                                                            IIf(CheckIsZeroOrNothing(txtthird_country.Text) = True, txtthird_country.Text & vbCrLf, "") & _
    '                                                            IIf(CheckIsZeroOrNothing(txtback_country.Text) = True, txtback_country.Text & vbCrLf, "") & _
    '                                                            IIf(CheckIsZeroOrNothing(txtplace_exibition.Text) = True, txtplace_exibition.Text & vbCrLf, "") & _
    '                                                            "       _____________________________"
    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 2 'T2_back_country,T3_place_exibition
    '            All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '                                         & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
    '            If C_TotalRowDe.Text = count_data Then
    '                txtT_product.Text = All_str & Form_NewStr_AddressDetail(CommonUtility.Get_StringValue(txtByCom_CH03.Text))
    '                GroupFooter1.Visible = True
    '                txtTotalAll.Visible = True
    '                txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
    '                                                                            BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
    '                                                                            BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " & _
    '                                                                            BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf & "        " & _
    '                                                                            BahtOnly(txtquantity5.Text, txtq_unit_code5.Text) & vbCrLf & _
    '                                                                            String_Total_USDOnly_New(txtDisplayUDS7.Text, txtCurrency_Code.Text, IIf(_IsAgent = False, TFob, _TAgent), CommonUtility.Get_StringValue(txtshow_check.Text), TUSD, TUSDOther) & "        " & _
    '                                                                            IIf(CheckIsZeroOrNothing(txtback_country.Text) = True, txtback_country.Text & vbCrLf, "") & _
    '                                                                            IIf(CheckIsZeroOrNothing(txtplace_exibition.Text) = True, txtplace_exibition.Text & vbCrLf, "") & _
    '                                                                            "       _____________________________"
    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 3 'T3_place_exibition
    '            All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '                                         & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
    '            If C_TotalRowDe.Text = count_data Then
    '                txtT_product.Text = All_str & Form_NewStr_AddressDetail(CommonUtility.Get_StringValue(txtByCom_CH03.Text))
    '                GroupFooter1.Visible = True
    '                txtTotalAll.Visible = True
    '                txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
    '                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
    '                                                                BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " & _
    '                                                                BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf & "        " & _
    '                                                                BahtOnly(txtquantity5.Text, txtq_unit_code5.Text) & vbCrLf & String_Total_USDOnly_New(txtDisplayUDS7.Text, txtCurrency_Code.Text, IIf(_IsAgent = False, TFob, _TAgent), CommonUtility.Get_StringValue(txtshow_check.Text), TUSD, TUSDOther) & _
    '                                                                "       _____________________________"
    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 4 '��������
    '            All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '                                         & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
    '            If C_TotalRowDe.Text = count_data Then
    '                txtT_product.Text = All_str & Form_NewStr_AddressDetail(CommonUtility.Get_StringValue(txtByCom_CH03.Text))
    '                GroupFooter1.Visible = True
    '                txtTotalAll.Visible = True
    '                txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
    '                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
    '                                                                BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " & _
    '                                                                BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf & "        " & _
    '                                                                BahtOnly(txtquantity5.Text, txtq_unit_code5.Text) & vbCrLf & String_Total_USDOnly_New(txtDisplayUDS7.Text, txtCurrency_Code.Text, IIf(_IsAgent = False, TFob, _TAgent), CommonUtility.Get_StringValue(txtshow_check.Text), TUSD, TUSDOther) & _
    '                                                                "       _____________________________"
    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 5 'T2_back_country
    '            All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '                                         & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
    '            If C_TotalRowDe.Text = count_data Then
    '                txtT_product.Text = All_str & Form_NewStr_AddressDetail(CommonUtility.Get_StringValue(txtByCom_CH03.Text))
    '                GroupFooter1.Visible = True
    '                txtTotalAll.Visible = True
    '                txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
    '                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
    '                                                                BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " & _
    '                                                                BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf & "        " & _
    '                                                                BahtOnly(txtquantity5.Text, txtq_unit_code5.Text) & vbCrLf & String_Total_USDOnly_New(txtDisplayUDS7.Text, txtCurrency_Code.Text, IIf(_IsAgent = False, TFob, _TAgent), CommonUtility.Get_StringValue(txtshow_check.Text), TUSD, TUSDOther) & vbNewLine & _
    '                                                                _TempB2B & vbNewLine & _
    '                                                                "       _____________________________"
    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 6 'T3_place_exibition
    '            All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '                                         & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
    '            If C_TotalRowDe.Text = count_data Then
    '                txtT_product.Text = All_str & Form_NewStr_AddressDetail(CommonUtility.Get_StringValue(txtByCom_CH03.Text))
    '                GroupFooter1.Visible = True
    '                txtTotalAll.Visible = True
    '                txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
    '                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
    '                                                                BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " & _
    '                                                                BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf & "        " & _
    '                                                                BahtOnly(txtquantity5.Text, txtq_unit_code5.Text) & vbCrLf & String_Total_USDOnly_New(txtDisplayUDS7.Text, txtCurrency_Code.Text, IIf(_IsAgent = False, TFob, _TAgent), CommonUtility.Get_StringValue(txtshow_check.Text), TUSD, TUSDOther) & _
    '                                                                "       _____________________________"
    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 7 'T1_third_country,T2_back_country
    '            All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '                                         & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
    '            If C_TotalRowDe.Text = count_data Then
    '                txtT_product.Text = All_str & Form_NewStr_AddressDetail(CommonUtility.Get_StringValue(txtByCom_CH03.Text))
    '                GroupFooter1.Visible = True
    '                txtTotalAll.Visible = True
    '                txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
    '                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
    '                                                                BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " & _
    '                                                                BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf & "        " & _
    '                                                                BahtOnly(txtquantity5.Text, txtq_unit_code5.Text) & vbCrLf & _
    '                                                                String_Total_USDOnly_New(txtDisplayUDS7.Text, txtCurrency_Code.Text, IIf(_IsAgent = False, TFob, _TAgent), CommonUtility.Get_StringValue(txtshow_check.Text), TUSD, TUSDOther) & "        " & _
    '                                                                IIf(CheckIsZeroOrNothing(txtthird_country.Text) = True, txtthird_country.Text & vbCrLf, "") & _
    '                                                                IIf(CheckIsZeroOrNothing(txtback_country.Text) = True, txtback_country.Text & vbCrLf, "") & _
    '                                                                "       _____________________________"
    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 8 'T1_third_country,T3_place_exibition
    '            All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '                                         & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
    '            If C_TotalRowDe.Text = count_data Then
    '                txtT_product.Text = All_str & Form_NewStr_AddressDetail(CommonUtility.Get_StringValue(txtByCom_CH03.Text))
    '                GroupFooter1.Visible = True
    '                txtTotalAll.Visible = True
    '                txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
    '                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
    '                                                                BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " & _
    '                                                                BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf & "        " & _
    '                                                                BahtOnly(txtquantity5.Text, txtq_unit_code5.Text) & vbCrLf & _
    '                                                                String_Total_USDOnly_New(txtDisplayUDS7.Text, txtCurrency_Code.Text, IIf(_IsAgent = False, TFob, _TAgent), CommonUtility.Get_StringValue(txtshow_check.Text), TUSD, TUSDOther) & "        " & _
    '                                                                IIf(CheckIsZeroOrNothing(txtthird_country.Text) = True, txtthird_country.Text & vbCrLf, "") & _
    '                                                                IIf(CheckIsZeroOrNothing(txtplace_exibition.Text) = True, txtplace_exibition.Text & vbCrLf, "") & _
    '                                                                "       _____________________________"
    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 9 'T1_third_country
    '            All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '                                         & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
    '            If C_TotalRowDe.Text = count_data Then
    '                txtT_product.Text = All_str & Form_NewStr_AddressDetail(CommonUtility.Get_StringValue(txtByCom_CH03.Text))
    '                GroupFooter1.Visible = True
    '                txtTotalAll.Visible = True
    '                txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
    '                                                            BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
    '                                                            BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " & _
    '                                                            BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf & "        " & _
    '                                                            BahtOnly(txtquantity5.Text, txtq_unit_code5.Text) & vbCrLf & _
    '                                                            String_Total_USDOnly_New(txtDisplayUDS7.Text, txtCurrency_Code.Text, IIf(_IsAgent = False, TFob, _TAgent), CommonUtility.Get_StringValue(txtshow_check.Text), TUSD, TUSDOther) & "        " & _
    '                                                            IIf(CheckIsZeroOrNothing(txtthird_country.Text) = True, txtthird_country.Text & vbCrLf, "") & _
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
    '                                         & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
    '            If C_TotalRowDe.Text = count_data Then
    '                txtT_product.Text = All_str & Form_NewStr_AddressDetail(CommonUtility.Get_StringValue(txtByCom_CH03.Text))
    '                GroupFooter1.Visible = True
    '                txtTotalAll.Visible = True
    '                txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
    '                                        BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
    '                                        BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " & _
    '                                        BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf & _
    '                                        String_Total_USDOnly_New(txtDisplayUDS7.Text, txtCurrency_Code.Text, IIf(_IsAgent = False, TFob, _TAgent), CommonUtility.Get_StringValue(txtshow_check.Text), TUSD, TUSDOther) & "        " & _
    '                                        IIf(CheckIsZeroOrNothing(txtthird_country.Text) = True, txtthird_country.Text & vbCrLf, "") & _
    '                                        IIf(CheckIsZeroOrNothing(txtback_country.Text) = True, txtback_country.Text & vbCrLf, "") & _
    '                                        IIf(CheckIsZeroOrNothing(txtplace_exibition.Text) = True, txtplace_exibition.Text & vbCrLf, "") & _
    '                                        "       _____________________________"
    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 2 'T2_back_country,T3_place_exibition
    '            All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '                                         & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
    '            If C_TotalRowDe.Text = count_data Then
    '                txtT_product.Text = All_str & Form_NewStr_AddressDetail(CommonUtility.Get_StringValue(txtByCom_CH03.Text))
    '                GroupFooter1.Visible = True
    '                txtTotalAll.Visible = True
    '                txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
    '                                                        BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
    '                                                        BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " & _
    '                                                        BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf & _
    '                                                        String_Total_USDOnly_New(txtDisplayUDS7.Text, txtCurrency_Code.Text, IIf(_IsAgent = False, TFob, _TAgent), CommonUtility.Get_StringValue(txtshow_check.Text), TUSD, TUSDOther) & "        " & _
    '                                                        IIf(CheckIsZeroOrNothing(txtback_country.Text) = True, txtback_country.Text & vbCrLf, "") & _
    '                                                        IIf(CheckIsZeroOrNothing(txtplace_exibition.Text) = True, txtplace_exibition.Text & vbCrLf, "") & _
    '                                                        "       _____________________________"
    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 3 'T3_place_exibition
    '            All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '                                         & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
    '            If C_TotalRowDe.Text = count_data Then
    '                txtT_product.Text = All_str & Form_NewStr_AddressDetail(CommonUtility.Get_StringValue(txtByCom_CH03.Text))
    '                GroupFooter1.Visible = True
    '                txtTotalAll.Visible = True
    '                txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
    '                                                            BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
    '                                                            BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " & _
    '                                                            BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf & String_Total_USDOnly_New(txtDisplayUDS7.Text, txtCurrency_Code.Text, IIf(_IsAgent = False, TFob, _TAgent), CommonUtility.Get_StringValue(txtshow_check.Text), TUSD, TUSDOther) & _
    '                                                            "       _____________________________"
    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 4 '��������
    '            All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '                                         & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
    '            If C_TotalRowDe.Text = count_data Then
    '                txtT_product.Text = All_str & Form_NewStr_AddressDetail(CommonUtility.Get_StringValue(txtByCom_CH03.Text))
    '                GroupFooter1.Visible = True
    '                txtTotalAll.Visible = True
    '                txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
    '                                                            BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
    '                                                            BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " & _
    '                                                            BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf & String_Total_USDOnly_New(txtDisplayUDS7.Text, txtCurrency_Code.Text, IIf(_IsAgent = False, TFob, _TAgent), CommonUtility.Get_StringValue(txtshow_check.Text), TUSD, TUSDOther) & _
    '                                                            "       _____________________________"
    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 5 'T2_back_country
    '            All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '                                         & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
    '            If C_TotalRowDe.Text = count_data Then
    '                txtT_product.Text = All_str & Form_NewStr_AddressDetail(CommonUtility.Get_StringValue(txtByCom_CH03.Text))
    '                GroupFooter1.Visible = True
    '                txtTotalAll.Visible = True
    '                txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
    '                                                            BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
    '                                                            BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " & _
    '                                                            BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf & String_Total_USDOnly_New(txtDisplayUDS7.Text, txtCurrency_Code.Text, IIf(_IsAgent = False, TFob, _TAgent), CommonUtility.Get_StringValue(txtshow_check.Text), TUSD, TUSDOther) & vbNewLine & _
    '                                                                _TempB2B & vbNewLine & _
    '                                                                "       _____________________________"
    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 6 'T3_place_exibition
    '            All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '                                         & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
    '            If C_TotalRowDe.Text = count_data Then
    '                txtT_product.Text = All_str & Form_NewStr_AddressDetail(CommonUtility.Get_StringValue(txtByCom_CH03.Text))
    '                GroupFooter1.Visible = True
    '                txtTotalAll.Visible = True
    '                txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
    '                                                            BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
    '                                                            BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " & _
    '                                                            BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf & String_Total_USDOnly_New(txtDisplayUDS7.Text, txtCurrency_Code.Text, IIf(_IsAgent = False, TFob, _TAgent), CommonUtility.Get_StringValue(txtshow_check.Text), TUSD, TUSDOther) & _
    '                                                            "       _____________________________"
    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 7 'T1_third_country,T2_back_country
    '            All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '                                         & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
    '            If C_TotalRowDe.Text = count_data Then
    '                txtT_product.Text = All_str & Form_NewStr_AddressDetail(CommonUtility.Get_StringValue(txtByCom_CH03.Text))
    '                GroupFooter1.Visible = True
    '                txtTotalAll.Visible = True
    '                txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
    '                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
    '                                                                BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " & _
    '                                                                BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf & _
    '                                                                String_Total_USDOnly_New(txtDisplayUDS7.Text, txtCurrency_Code.Text, IIf(_IsAgent = False, TFob, _TAgent), CommonUtility.Get_StringValue(txtshow_check.Text), TUSD, TUSDOther) & "        " & _
    '                                                                IIf(CheckIsZeroOrNothing(txtthird_country.Text) = True, txtthird_country.Text & vbCrLf, "") & _
    '                                                                IIf(CheckIsZeroOrNothing(txtback_country.Text) = True, txtback_country.Text & vbCrLf, "") & _
    '                                                                "       _____________________________"
    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 8 'T1_third_country,T3_place_exibition
    '            All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '                                         & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
    '            If C_TotalRowDe.Text = count_data Then
    '                txtT_product.Text = All_str & Form_NewStr_AddressDetail(CommonUtility.Get_StringValue(txtByCom_CH03.Text))
    '                GroupFooter1.Visible = True
    '                txtTotalAll.Visible = True
    '                txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
    '                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
    '                                                                BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " & _
    '                                                                BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf & _
    '                                                                String_Total_USDOnly_New(txtDisplayUDS7.Text, txtCurrency_Code.Text, IIf(_IsAgent = False, TFob, _TAgent), CommonUtility.Get_StringValue(txtshow_check.Text), TUSD, TUSDOther) & "        " & _
    '                                                                IIf(CheckIsZeroOrNothing(txtthird_country.Text) = True, txtthird_country.Text & vbCrLf, "") & _
    '                                                                IIf(CheckIsZeroOrNothing(txtplace_exibition.Text) = True, txtplace_exibition.Text & vbCrLf, "") & _
    '                                                                "       _____________________________"
    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 9 'T1_third_country
    '            All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '                                         & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
    '            If C_TotalRowDe.Text = count_data Then
    '                txtT_product.Text = All_str & Form_NewStr_AddressDetail(CommonUtility.Get_StringValue(txtByCom_CH03.Text))
    '                GroupFooter1.Visible = True
    '                txtTotalAll.Visible = True
    '                txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
    '                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
    '                                                                BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " & _
    '                                                                BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf & _
    '                                                                String_Total_USDOnly_New(txtDisplayUDS7.Text, txtCurrency_Code.Text, IIf(_IsAgent = False, TFob, _TAgent), CommonUtility.Get_StringValue(txtshow_check.Text), TUSD, TUSDOther) & "        " & _
    '                                                                IIf(CheckIsZeroOrNothing(txtthird_country.Text) = True, txtthird_country.Text & vbCrLf, "") & _
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
    '                                         & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
    '            If C_TotalRowDe.Text = count_data Then
    '                txtT_product.Text = All_str & Form_NewStr_AddressDetail(CommonUtility.Get_StringValue(txtByCom_CH03.Text))
    '                GroupFooter1.Visible = True
    '                txtTotalAll.Visible = True
    '                txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
    '                                                            BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
    '                                                            BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & _
    '                                                            String_Total_USDOnly_New(txtDisplayUDS7.Text, txtCurrency_Code.Text, IIf(_IsAgent = False, TFob, _TAgent), CommonUtility.Get_StringValue(txtshow_check.Text), TUSD, TUSDOther) & "        " & _
    '                                                            IIf(CheckIsZeroOrNothing(txtthird_country.Text) = True, txtthird_country.Text & vbCrLf, "") & _
    '                                                            IIf(CheckIsZeroOrNothing(txtback_country.Text) = True, txtback_country.Text & vbCrLf, "") & _
    '                                                            IIf(CheckIsZeroOrNothing(txtplace_exibition.Text) = True, txtplace_exibition.Text & vbCrLf, "") & _
    '                                                            "       _____________________________"
    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 2 'T2_back_country,T3_place_exibition
    '            All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '                                         & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
    '            If C_TotalRowDe.Text = count_data Then
    '                txtT_product.Text = All_str & Form_NewStr_AddressDetail(CommonUtility.Get_StringValue(txtByCom_CH03.Text))
    '                GroupFooter1.Visible = True
    '                txtTotalAll.Visible = True
    '                txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
    '                                                                            BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
    '                                                                            BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & _
    '                                                                            String_Total_USDOnly_New(txtDisplayUDS7.Text, txtCurrency_Code.Text, IIf(_IsAgent = False, TFob, _TAgent), CommonUtility.Get_StringValue(txtshow_check.Text), TUSD, TUSDOther) & "        " & _
    '                                                                            IIf(CheckIsZeroOrNothing(txtback_country.Text) = True, txtback_country.Text & vbCrLf, "") & _
    '                                                                            IIf(CheckIsZeroOrNothing(txtplace_exibition.Text) = True, txtplace_exibition.Text & vbCrLf, "") & _
    '                                                                            "       _____________________________"
    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 3 'T3_place_exibition
    '            All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '                                         & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
    '            If C_TotalRowDe.Text = count_data Then
    '                txtT_product.Text = All_str & Form_NewStr_AddressDetail(CommonUtility.Get_StringValue(txtByCom_CH03.Text))
    '                GroupFooter1.Visible = True
    '                txtTotalAll.Visible = True
    '                txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
    '                                                            BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
    '                                                            BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & String_Total_USDOnly_New(txtDisplayUDS7.Text, txtCurrency_Code.Text, IIf(_IsAgent = False, TFob, _TAgent), CommonUtility.Get_StringValue(txtshow_check.Text), TUSD, TUSDOther) & _
    '                                                            "       _____________________________"
    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 4 '��������
    '            All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '                                         & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
    '            If C_TotalRowDe.Text = count_data Then
    '                txtT_product.Text = All_str & Form_NewStr_AddressDetail(CommonUtility.Get_StringValue(txtByCom_CH03.Text))
    '                GroupFooter1.Visible = True
    '                txtTotalAll.Visible = True
    '                txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
    '                                                            BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
    '                                                            BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & String_Total_USDOnly_New(txtDisplayUDS7.Text, txtCurrency_Code.Text, IIf(_IsAgent = False, TFob, _TAgent), CommonUtility.Get_StringValue(txtshow_check.Text), TUSD, TUSDOther) & _
    '                                                            "       _____________________________"
    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 5 'T2_back_country
    '            All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '                                         & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
    '            If C_TotalRowDe.Text = count_data Then
    '                txtT_product.Text = All_str & Form_NewStr_AddressDetail(CommonUtility.Get_StringValue(txtByCom_CH03.Text))
    '                GroupFooter1.Visible = True
    '                txtTotalAll.Visible = True
    '                txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
    '                                                            BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
    '                                                            BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & String_Total_USDOnly_New(txtDisplayUDS7.Text, txtCurrency_Code.Text, IIf(_IsAgent = False, TFob, _TAgent), CommonUtility.Get_StringValue(txtshow_check.Text), TUSD, TUSDOther) & vbNewLine & _
    '                                                                _TempB2B & vbNewLine & _
    '                                                                "       _____________________________"
    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 6 'T3_place_exibition
    '            All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '                                         & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
    '            If C_TotalRowDe.Text = count_data Then
    '                txtT_product.Text = All_str & Form_NewStr_AddressDetail(CommonUtility.Get_StringValue(txtByCom_CH03.Text))
    '                GroupFooter1.Visible = True
    '                txtTotalAll.Visible = True
    '                txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
    '                                                            BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
    '                                                            BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & String_Total_USDOnly_New(txtDisplayUDS7.Text, txtCurrency_Code.Text, IIf(_IsAgent = False, TFob, _TAgent), CommonUtility.Get_StringValue(txtshow_check.Text), TUSD, TUSDOther) & _
    '                                                            "       _____________________________"
    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 7 'T1_third_country,T2_back_country
    '            All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '                                          & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
    '            If C_TotalRowDe.Text = count_data Then
    '                txtT_product.Text = All_str & Form_NewStr_AddressDetail(CommonUtility.Get_StringValue(txtByCom_CH03.Text))
    '                GroupFooter1.Visible = True
    '                txtTotalAll.Visible = True
    '                txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
    '                                                            BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
    '                                                            BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & _
    '                                                            String_Total_USDOnly_New(txtDisplayUDS7.Text, txtCurrency_Code.Text, IIf(_IsAgent = False, TFob, _TAgent), CommonUtility.Get_StringValue(txtshow_check.Text), TUSD, TUSDOther) & "        " & _
    '                                                            IIf(CheckIsZeroOrNothing(txtthird_country.Text) = True, txtthird_country.Text & vbCrLf, "") & _
    '                                                            IIf(CheckIsZeroOrNothing(txtback_country.Text) = True, txtback_country.Text & vbCrLf, "") & _
    '                                                            "       _____________________________"
    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 8 'T1_third_country,T3_place_exibition
    '            All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '                                          & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
    '            If C_TotalRowDe.Text = count_data Then
    '                txtT_product.Text = All_str & Form_NewStr_AddressDetail(CommonUtility.Get_StringValue(txtByCom_CH03.Text))
    '                GroupFooter1.Visible = True
    '                txtTotalAll.Visible = True
    '                txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
    '                                                            BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
    '                                                            BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & _
    '                                                            String_Total_USDOnly_New(txtDisplayUDS7.Text, txtCurrency_Code.Text, IIf(_IsAgent = False, TFob, _TAgent), CommonUtility.Get_StringValue(txtshow_check.Text), TUSD, TUSDOther) & "        " & _
    '                                                            IIf(CheckIsZeroOrNothing(txtthird_country.Text) = True, txtthird_country.Text & vbCrLf, "") & _
    '                                                            IIf(CheckIsZeroOrNothing(txtplace_exibition.Text) = True, txtplace_exibition.Text & vbCrLf, "") & _
    '                                                            "       _____________________________"
    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 9 'T1_third_country
    '            All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '                                         & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
    '            If C_TotalRowDe.Text = count_data Then
    '                txtT_product.Text = All_str & Form_NewStr_AddressDetail(CommonUtility.Get_StringValue(txtByCom_CH03.Text))
    '                GroupFooter1.Visible = True
    '                txtTotalAll.Visible = True
    '                txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
    '                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
    '                                                                BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & _
    '                                                                String_Total_USDOnly_New(txtDisplayUDS7.Text, txtCurrency_Code.Text, IIf(_IsAgent = False, TFob, _TAgent), CommonUtility.Get_StringValue(txtshow_check.Text), TUSD, TUSDOther) & "        " & _
    '                                                                IIf(CheckIsZeroOrNothing(txtthird_country.Text) = True, txtthird_country.Text & vbCrLf, "") & _
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
    '                                         & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
    '            If C_TotalRowDe.Text = count_data Then
    '                txtT_product.Text = All_str & Form_NewStr_AddressDetail(CommonUtility.Get_StringValue(txtByCom_CH03.Text))
    '                GroupFooter1.Visible = True
    '                txtTotalAll.Visible = True
    '                txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
    '                                                            BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & _
    '                                                            String_Total_USDOnly_New(txtDisplayUDS7.Text, txtCurrency_Code.Text, IIf(_IsAgent = False, TFob, _TAgent), CommonUtility.Get_StringValue(txtshow_check.Text), TUSD, TUSDOther) & "        " & _
    '                                                            IIf(CheckIsZeroOrNothing(txtthird_country.Text) = True, txtthird_country.Text & vbCrLf, "") & _
    '                                                            IIf(CheckIsZeroOrNothing(txtback_country.Text) = True, txtback_country.Text & vbCrLf, "") & _
    '                                                            IIf(CheckIsZeroOrNothing(txtplace_exibition.Text) = True, txtplace_exibition.Text & vbCrLf, "") & _
    '                                                            "       _____________________________"
    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 2 'T2_back_country,T3_place_exibition
    '            All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '                                         & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
    '            If C_TotalRowDe.Text = count_data Then
    '                txtT_product.Text = All_str & Form_NewStr_AddressDetail(CommonUtility.Get_StringValue(txtByCom_CH03.Text))
    '                GroupFooter1.Visible = True
    '                txtTotalAll.Visible = True
    '                txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
    '                                                                            BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & _
    '                                                                            String_Total_USDOnly_New(txtDisplayUDS7.Text, txtCurrency_Code.Text, IIf(_IsAgent = False, TFob, _TAgent), CommonUtility.Get_StringValue(txtshow_check.Text), TUSD, TUSDOther) & "        " & _
    '                                                                            IIf(CheckIsZeroOrNothing(txtback_country.Text) = True, txtback_country.Text & vbCrLf, "") & _
    '                                                                            IIf(CheckIsZeroOrNothing(txtplace_exibition.Text) = True, txtplace_exibition.Text & vbCrLf, "") & _
    '                                                                            "       _____________________________"
    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 3 'T3_place_exibition
    '            All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '                                         & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
    '            If C_TotalRowDe.Text = count_data Then
    '                txtT_product.Text = All_str & Form_NewStr_AddressDetail(CommonUtility.Get_StringValue(txtByCom_CH03.Text))
    '                GroupFooter1.Visible = True
    '                txtTotalAll.Visible = True
    '                txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
    '                                                            BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & String_Total_USDOnly_New(txtDisplayUDS7.Text, txtCurrency_Code.Text, IIf(_IsAgent = False, TFob, _TAgent), CommonUtility.Get_StringValue(txtshow_check.Text), TUSD, TUSDOther) & _
    '                                                            "       _____________________________"
    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 4 '��������
    '            All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '                                         & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
    '            If C_TotalRowDe.Text = count_data Then
    '                txtT_product.Value = All_str & Form_NewStr_AddressDetail(CommonUtility.Get_StringValue(txtByCom_CH03.Text))
    '                GroupFooter1.Visible = True
    '                txtTotalAll.Visible = True
    '                txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
    '                                                            BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & String_Total_USDOnly_New(txtDisplayUDS7.Text, txtCurrency_Code.Text, IIf(_IsAgent = False, TFob, _TAgent), CommonUtility.Get_StringValue(txtshow_check.Text), TUSD, TUSDOther) & _
    '                                                            "       _____________________________"
    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 5 'T2_back_country
    '            All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '                                         & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
    '            If C_TotalRowDe.Text = count_data Then
    '                txtT_product.Text = All_str & Form_NewStr_AddressDetail(CommonUtility.Get_StringValue(txtByCom_CH03.Text))
    '                GroupFooter1.Visible = True
    '                txtTotalAll.Visible = True
    '                txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
    '                                                            BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & String_Total_USDOnly_New(txtDisplayUDS7.Text, txtCurrency_Code.Text, IIf(_IsAgent = False, TFob, _TAgent), CommonUtility.Get_StringValue(txtshow_check.Text), TUSD, TUSDOther) & vbNewLine & _
    '                                                                _TempB2B & vbNewLine & _
    '                                                                "       _____________________________"
    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 6 'T3_place_exibition
    '            All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '                                         & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
    '            If C_TotalRowDe.Text = count_data Then
    '                txtT_product.Text = All_str & Form_NewStr_AddressDetail(CommonUtility.Get_StringValue(txtByCom_CH03.Text))
    '                GroupFooter1.Visible = True
    '                txtTotalAll.Visible = True
    '                txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
    '                                                            BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & String_Total_USDOnly_New(txtDisplayUDS7.Text, txtCurrency_Code.Text, IIf(_IsAgent = False, TFob, _TAgent), CommonUtility.Get_StringValue(txtshow_check.Text), TUSD, TUSDOther) & _
    '                                                            "       _____________________________"
    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 7 'T1_third_country,T2_back_country
    '            All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '                                         & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
    '            If C_TotalRowDe.Text = count_data Then
    '                txtT_product.Text = All_str & Form_NewStr_AddressDetail(CommonUtility.Get_StringValue(txtByCom_CH03.Text))
    '                GroupFooter1.Visible = True
    '                txtTotalAll.Visible = True
    '                txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
    '                                                            BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & _
    '                                                            String_Total_USDOnly_New(txtDisplayUDS7.Text, txtCurrency_Code.Text, IIf(_IsAgent = False, TFob, _TAgent), CommonUtility.Get_StringValue(txtshow_check.Text), TUSD, TUSDOther) & "        " & _
    '                                                            IIf(CheckIsZeroOrNothing(txtthird_country.Text) = True, txtthird_country.Text & vbCrLf, "") & _
    '                                                            IIf(CheckIsZeroOrNothing(txtback_country.Text) = True, txtback_country.Text & vbCrLf, "") & _
    '                                                            "       _____________________________"
    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 8 'T1_third_country,T3_place_exibition
    '            All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '                                         & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
    '            If C_TotalRowDe.Text = count_data Then
    '                txtT_product.Text = All_str & Form_NewStr_AddressDetail(CommonUtility.Get_StringValue(txtByCom_CH03.Text))
    '                GroupFooter1.Visible = True
    '                txtTotalAll.Visible = True
    '                txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
    '                                                            BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & _
    '                                                            String_Total_USDOnly_New(txtDisplayUDS7.Text, txtCurrency_Code.Text, IIf(_IsAgent = False, TFob, _TAgent), CommonUtility.Get_StringValue(txtshow_check.Text), TUSD, TUSDOther) & "        " & _
    '                                                            IIf(CheckIsZeroOrNothing(txtthird_country.Text) = True, txtthird_country.Text & vbCrLf, "") & _
    '                                                            IIf(CheckIsZeroOrNothing(txtplace_exibition.Text) = True, txtplace_exibition.Text & vbCrLf, "") & _
    '                                                            "       _____________________________"
    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 9 'T1_third_country
    '            All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '                                         & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
    '            If C_TotalRowDe.Text = count_data Then
    '                txtT_product.Text = All_str & Form_NewStr_AddressDetail(CommonUtility.Get_StringValue(txtByCom_CH03.Text))
    '                GroupFooter1.Visible = True
    '                txtTotalAll.Visible = True
    '                txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
    '                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & _
    '                                                                String_Total_USDOnly_New(txtDisplayUDS7.Text, txtCurrency_Code.Text, IIf(_IsAgent = False, TFob, _TAgent), CommonUtility.Get_StringValue(txtshow_check.Text), TUSD, TUSDOther) & "        " & _
    '                                                                IIf(CheckIsZeroOrNothing(txtthird_country.Text) = True, txtthird_country.Text & vbCrLf, "") & _
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
    '                                         & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
    '            If C_TotalRowDe.Text = count_data Then
    '                If (Not txtquantity1.Text = String.Empty = True) And CommonUtility.Get_StringValue(txtquantity1.Text) <> "" And CommonUtility.Get_Decimal(IIf(Not (txtquantity1.Text = String.Empty) = False, 0, txtquantity1.Value)) <> 0 Then
    '                    txtT_product.Text = All_str & Form_NewStr_AddressDetail(CommonUtility.Get_StringValue(txtByCom_CH03.Text))
    '                    GroupFooter1.Visible = True
    '                    txtTotalAll.Visible = True
    '                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & _
    '                                        String_Total_USDOnly_New(txtDisplayUDS7.Text, txtCurrency_Code.Text, IIf(_IsAgent = False, TFob, _TAgent), CommonUtility.Get_StringValue(txtshow_check.Text), TUSD, TUSDOther) & "        " & _
    '                                                                                    IIf(CheckIsZeroOrNothing(txtthird_country.Text) = True, txtthird_country.Text & vbCrLf, "") & _
    '                                                                                    IIf(CheckIsZeroOrNothing(txtback_country.Text) = True, txtback_country.Text & vbCrLf, "") & _
    '                                                                                    IIf(CheckIsZeroOrNothing(txtplace_exibition.Text) = True, txtplace_exibition.Text & vbCrLf, "") & _
    '                                                                                    "       _____________________________"
    '                Else
    '                    txtT_product.Text = All_str & _
    '                                       "        " & _
    '                                                                                    IIf(CheckIsZeroOrNothing(txtthird_country.Text) = True, txtthird_country.Text & vbCrLf, "") & _
    '                                                                                    IIf(CheckIsZeroOrNothing(txtback_country.Text) = True, txtback_country.Text & vbCrLf, "") & _
    '                                                                                    IIf(CheckIsZeroOrNothing(txtplace_exibition.Text) = True, txtplace_exibition.Text & vbCrLf, "") & _
    '                                                                                    "       _____________________________"
    '                End If

    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 2 'T2_back_country,T3_place_exibition
    '            All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '                                         & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
    '            If C_TotalRowDe.Text = count_data Then
    '                If (Not txtquantity1.Text = String.Empty = True) And CommonUtility.Get_StringValue(txtquantity1.Text) <> "" And CommonUtility.Get_Decimal(IIf(Not (txtquantity1.Text = String.Empty) = False, 0, txtquantity1.Value)) <> 0 Then
    '                    txtT_product.Text = All_str & Form_NewStr_AddressDetail(CommonUtility.Get_StringValue(txtByCom_CH03.Text))
    '                    GroupFooter1.Visible = True
    '                    txtTotalAll.Visible = True
    '                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & _
    '                                        String_Total_USDOnly_New(txtDisplayUDS7.Text, txtCurrency_Code.Text, IIf(_IsAgent = False, TFob, _TAgent), CommonUtility.Get_StringValue(txtshow_check.Text), TUSD, TUSDOther) & "        " & _
    '                                                                                                    IIf(CheckIsZeroOrNothing(txtback_country.Text) = True, txtback_country.Text & vbCrLf, "") & _
    '                                                                                                    IIf(CheckIsZeroOrNothing(txtplace_exibition.Text) = True, txtplace_exibition.Text & vbCrLf, "") & _
    '                                                                                                    "       _____________________________"
    '                Else
    '                    txtT_product.Text = All_str & _
    '                                        "        " & _
    '                                                                                                    IIf(CheckIsZeroOrNothing(txtback_country.Text) = True, txtback_country.Text & vbCrLf, "") & _
    '                                                                                                    IIf(CheckIsZeroOrNothing(txtplace_exibition.Text) = True, txtplace_exibition.Text & vbCrLf, "") & _
    '                                                                                                    "       _____________________________"
    '                End If

    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 3 'T3_place_exibition
    '            All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '                                         & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
    '            If C_TotalRowDe.Text = count_data Then
    '                If (Not txtquantity1.Text = String.Empty = True) And CommonUtility.Get_StringValue(txtquantity1.Text) <> "" And CommonUtility.Get_Decimal(IIf(Not (txtquantity1.Text = String.Empty) = False, 0, txtquantity1.Value)) <> 0 Then
    '                    txtT_product.Text = All_str & Form_NewStr_AddressDetail(CommonUtility.Get_StringValue(txtByCom_CH03.Text))
    '                    GroupFooter1.Visible = True
    '                    txtTotalAll.Visible = True
    '                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & String_Total_USDOnly_New(txtDisplayUDS7.Text, txtCurrency_Code.Text, IIf(_IsAgent = False, TFob, _TAgent), CommonUtility.Get_StringValue(txtshow_check.Text), TUSD, TUSDOther) & _
    '                                                                                    "       _____________________________"
    '                Else
    '                    txtT_product.Text = All_str & _
    '                                       "       _____________________________"
    '                End If

    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 4 '��������
    '            All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '                                          & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
    '            If C_TotalRowDe.Text = count_data Then
    '                If (Not txtquantity1.Text = String.Empty = True) And CommonUtility.Get_StringValue(txtquantity1.Text) <> "" And CommonUtility.Get_Decimal(IIf(Not (txtquantity1.Text = String.Empty) = False, 0, txtquantity1.Value)) <> 0 Then
    '                    txtT_product.Text = All_str & Form_NewStr_AddressDetail(CommonUtility.Get_StringValue(txtByCom_CH03.Text))
    '                    GroupFooter1.Visible = True
    '                    txtTotalAll.Visible = True
    '                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & String_Total_USDOnly_New(txtDisplayUDS7.Text, txtCurrency_Code.Text, IIf(_IsAgent = False, TFob, _TAgent), CommonUtility.Get_StringValue(txtshow_check.Text), TUSD, TUSDOther) & _
    '                                                                                    "       _____________________________"
    '                Else
    '                    txtT_product.Text = All_str & _
    '                                        "       _____________________________"
    '                End If

    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 5 'T2_back_country
    '            All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '                                         & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
    '            If C_TotalRowDe.Text = count_data Then
    '                If (Not txtquantity1.Text = String.Empty = True) And CommonUtility.Get_StringValue(txtquantity1.Text) <> "" And CommonUtility.Get_Decimal(IIf(Not (txtquantity1.Text = String.Empty) = False, 0, txtquantity1.Value)) <> 0 Then
    '                    txtT_product.Text = All_str & Form_NewStr_AddressDetail(CommonUtility.Get_StringValue(txtByCom_CH03.Text))
    '                    GroupFooter1.Visible = True
    '                    txtTotalAll.Visible = True
    '                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & String_Total_USDOnly_New(txtDisplayUDS7.Text, txtCurrency_Code.Text, IIf(_IsAgent = False, TFob, _TAgent), CommonUtility.Get_StringValue(txtshow_check.Text), TUSD, TUSDOther) & vbNewLine & _
    '                                                                _TempB2B & vbNewLine & _
    '                                                                "       _____________________________"
    '                Else
    '                    txtT_product.Text = All_str & _
    '                                                                _TempB2B & vbNewLine & _
    '                                                                "       _____________________________"
    '                End If

    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 6 'T3_place_exibition
    '            All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '                                         & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
    '            If C_TotalRowDe.Text = count_data Then
    '                If (Not txtquantity1.Text = String.Empty = True) And CommonUtility.Get_StringValue(txtquantity1.Text) <> "" And CommonUtility.Get_Decimal(IIf(Not (txtquantity1.Text = String.Empty) = False, 0, txtquantity1.Value)) <> 0 Then
    '                    txtT_product.Text = All_str & Form_NewStr_AddressDetail(CommonUtility.Get_StringValue(txtByCom_CH03.Text))
    '                    GroupFooter1.Visible = True
    '                    txtTotalAll.Visible = True
    '                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & String_Total_USDOnly_New(txtDisplayUDS7.Text, txtCurrency_Code.Text, IIf(_IsAgent = False, TFob, _TAgent), CommonUtility.Get_StringValue(txtshow_check.Text), TUSD, TUSDOther) & _
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
    '                                          & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
    '            If C_TotalRowDe.Text = count_data Then
    '                If (Not txtquantity1.Text = String.Empty = True) And CommonUtility.Get_StringValue(txtquantity1.Text) <> "" And CommonUtility.Get_Decimal(IIf(Not (txtquantity1.Text = String.Empty) = False, 0, txtquantity1.Value)) <> 0 Then
    '                    txtT_product.Text = All_str & Form_NewStr_AddressDetail(CommonUtility.Get_StringValue(txtByCom_CH03.Text))
    '                    GroupFooter1.Visible = True
    '                    txtTotalAll.Visible = True
    '                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & _
    '                                        String_Total_USDOnly_New(txtDisplayUDS7.Text, txtCurrency_Code.Text, IIf(_IsAgent = False, TFob, _TAgent), CommonUtility.Get_StringValue(txtshow_check.Text), TUSD, TUSDOther) & "        " & _
    '                                                                                    IIf(CheckIsZeroOrNothing(txtthird_country.Text) = True, txtthird_country.Text & vbCrLf, "") & _
    '                                                                                    IIf(CheckIsZeroOrNothing(txtback_country.Text) = True, txtback_country.Text & vbCrLf, "") & _
    '                                                                                    "       _____________________________"
    '                Else
    '                    txtT_product.Text = All_str & _
    '                                        "        " & _
    '                                                                                    IIf(CheckIsZeroOrNothing(txtthird_country.Text) = True, txtthird_country.Text & vbCrLf, "") & _
    '                                                                                    IIf(CheckIsZeroOrNothing(txtback_country.Text) = True, txtback_country.Text & vbCrLf, "") & _
    '                                                                                    "       _____________________________"
    '                End If

    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 8 'T1_third_country,T3_place_exibition
    '            All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '                                         & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
    '            If C_TotalRowDe.Text = count_data Then
    '                If (Not txtquantity1.Text = String.Empty = True) And CommonUtility.Get_StringValue(txtquantity1.Text) <> "" And CommonUtility.Get_Decimal(IIf(Not (txtquantity1.Text = String.Empty) = False, 0, txtquantity1.Value)) <> 0 Then
    '                    txtT_product.Text = All_str & Form_NewStr_AddressDetail(CommonUtility.Get_StringValue(txtByCom_CH03.Text))
    '                    GroupFooter1.Visible = True
    '                    txtTotalAll.Visible = True
    '                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & _
    '                                        String_Total_USDOnly_New(txtDisplayUDS7.Text, txtCurrency_Code.Text, IIf(_IsAgent = False, TFob, _TAgent), CommonUtility.Get_StringValue(txtshow_check.Text), TUSD, TUSDOther) & "        " & _
    '                                                                                    IIf(CheckIsZeroOrNothing(txtthird_country.Text) = True, txtthird_country.Text & vbCrLf, "") & _
    '                                                                                    IIf(CheckIsZeroOrNothing(txtplace_exibition.Text) = True, txtplace_exibition.Text & vbCrLf, "") & _
    '                                                                                    "       _____________________________"
    '                Else
    '                    txtT_product.Text = All_str & _
    '                                        "        " & _
    '                                                                                    IIf(CheckIsZeroOrNothing(txtthird_country.Text) = True, txtthird_country.Text & vbCrLf, "") & _
    '                                                                                    IIf(CheckIsZeroOrNothing(txtplace_exibition.Text) = True, txtplace_exibition.Text & vbCrLf, "") & _
    '                                                                                    "       _____________________________"
    '                End If

    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 9 'T1_third_country
    '            All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '                                         & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
    '            If C_TotalRowDe.Text = count_data Then
    '                If (Not txtquantity1.Text = String.Empty = True) And CommonUtility.Get_StringValue(txtquantity1.Text) <> "" And CommonUtility.Get_Decimal(IIf(Not (txtquantity1.Text = String.Empty) = False, 0, txtquantity1.Value)) <> 0 Then
    '                    txtT_product.Text = All_str & Form_NewStr_AddressDetail(CommonUtility.Get_StringValue(txtByCom_CH03.Text))
    '                    GroupFooter1.Visible = True
    '                    txtTotalAll.Visible = True
    '                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & _
    '                                         String_Total_USDOnly_New(txtDisplayUDS7.Text, txtCurrency_Code.Text, IIf(_IsAgent = False, TFob, _TAgent), CommonUtility.Get_StringValue(txtshow_check.Text), TUSD, TUSDOther) & "        " & _
    '                                                                                        IIf(CheckIsZeroOrNothing(txtthird_country.Text) = True, txtthird_country.Text & vbCrLf, "") & _
    '                                                                                        "       _____________________________"
    '                Else
    '                    txtT_product.Text = All_str & _
    '                                        "        " & _
    '                                                                                        IIf(CheckIsZeroOrNothing(txtthird_country.Text) = True, txtthird_country.Text & vbCrLf, "") & _
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
    '                                         & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
    '            If C_TotalRowDe.Text = count_data Then
    '                txtT_product.Text = All_str & "        " & _
    '                IIf(CheckIsZeroOrNothing(txtthird_country.Text) = True, txtthird_country.Text & vbCrLf, "") & _
    '                                                            IIf(CheckIsZeroOrNothing(txtback_country.Text) = True, txtback_country.Text & vbCrLf, "") & _
    '                                                            IIf(CheckIsZeroOrNothing(txtplace_exibition.Text) = True, txtplace_exibition.Text & vbCrLf, "") & _
    '                                                            "       _____________________________"
    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 2 'T2_back_country,T3_place_exibition
    '            All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '                                         & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
    '            If C_TotalRowDe.Text = count_data Then
    '                txtT_product.Text = All_str & _
    '                IIf(CheckIsZeroOrNothing(txtback_country.Text) = True, txtback_country.Text & vbCrLf, "") & _
    '                                                                            IIf(CheckIsZeroOrNothing(txtplace_exibition.Text) = True, txtplace_exibition.Text & vbCrLf, "") & _
    '                                                                            "       _____________________________"
    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 3 'T3_place_exibition
    '            All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '                                         & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
    '            If C_TotalRowDe.Text = count_data Then
    '                txtT_product.Text = All_str & _
    '                 "       _____________________________"
    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 4 '��������
    '            All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '                                         & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
    '            If C_TotalRowDe.Text = count_data Then
    '                txtT_product.Text = All_str & _
    '                 "       _____________________________"
    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 5 'T2_back_country
    '            All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '                                         & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
    '            If C_TotalRowDe.Text = count_data Then
    '                txtT_product.Text = All_str & vbNewLine & _
    '                                                                _TempB2B & vbNewLine & _
    '                                                                "       _____________________________"
    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 6 'T3_place_exibition
    '            All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '                                         & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
    '            If C_TotalRowDe.Text = count_data Then
    '                txtT_product.Text = All_str & _
    '                 "       _____________________________"
    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 7 'T1_third_country,T2_back_country
    '            All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '                                         & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
    '            If C_TotalRowDe.Text = count_data Then
    '                txtT_product.Text = All_str & _
    '                IIf(CheckIsZeroOrNothing(txtthird_country.Text) = True, txtthird_country.Text & vbCrLf, "") & _
    '                                                            IIf(CheckIsZeroOrNothing(txtback_country.Text) = True, txtback_country.Text & vbCrLf, "") & _
    '                                                            "       _____________________________"
    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 8 'T1_third_country,T3_place_exibition
    '            All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '                                         & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
    '            If C_TotalRowDe.Text = count_data Then
    '                txtT_product.Text = All_str & _
    '                 IIf(CheckIsZeroOrNothing(txtthird_country.Text) = True, txtthird_country.Text & vbCrLf, "") & _
    '                                                            IIf(CheckIsZeroOrNothing(txtplace_exibition.Text) = True, txtplace_exibition.Text & vbCrLf, "") & _
    '                                                            "       _____________________________"
    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 9 'T1_third_country
    '            All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '                                         & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
    '            If C_TotalRowDe.Text = count_data Then
    '                txtT_product.Text = All_str & _
    '                IIf(CheckIsZeroOrNothing(txtthird_country.Text) = True, txtthird_country.Text & vbCrLf, "") & _
    '                                                                "       _____________________________"
    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '    End Select
    'End Sub
    Sub All_CheckThird_5(ByVal count_data As Integer)
        Dim All_str As String
        Select Case Check_Third(CommonUtility.Get_StringValue(txtthird_country.Value), _
                                CommonUtility.Get_StringValue(txtback_country.Value), _
                                CommonUtility.Get_StringValue(txtplace_exibition.Value))
            Case 1
                All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                                              & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & Form_NewStr_AddressDetail(CommonUtility.Get_StringValue(txtByCom_CH03.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity5.Text, txtq_unit_code5.Text) & vbCrLf & _
                                                                String_Total_USDOnly_New_Attach(txtDisplayUDS7.Text, txtCurrency_Code.Text, IIf(_IsAgent = False, TFob, _TAgent), CommonUtility.Get_StringValue(txtshow_check.Text), TUSD, TUSDOther, txtgross_weightH.Value, txtg_unit_code.Text, txtinvh_run_auto.Value) & "        " & _
                                                                IIf(CheckIsZeroOrNothing(txtthird_country.Text) = True, txtthird_country.Text & vbCrLf, "") & _
                                                                IIf(CheckIsZeroOrNothing(txtback_country.Text) = True, txtback_country.Text & vbCrLf, "") & _
                                                                IIf(CheckIsZeroOrNothing(txtplace_exibition.Text) = True, txtplace_exibition.Text & vbCrLf, "") & _
                                                               IIf(_IsAgent = False, "", CheckInvAgent(_IsAgent, _Invoice_Agent, _CompanyName_Agent)) & _
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 2 'T2_back_country,T3_place_exibition
                All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                                             & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & Form_NewStr_AddressDetail(CommonUtility.Get_StringValue(txtByCom_CH03.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
                                                                                BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " & _
                                                                                BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf & "        " & _
                                                                                BahtOnly(txtquantity5.Text, txtq_unit_code5.Text) & vbCrLf & _
                                                                                String_Total_USDOnly_New_Attach(txtDisplayUDS7.Text, txtCurrency_Code.Text, IIf(_IsAgent = False, TFob, _TAgent), CommonUtility.Get_StringValue(txtshow_check.Text), TUSD, TUSDOther, txtgross_weightH.Value, txtg_unit_code.Text, txtinvh_run_auto.Value) & "        " & _
                                                                                IIf(CheckIsZeroOrNothing(txtback_country.Text) = True, txtback_country.Text & vbCrLf, "") & _
                                                                                IIf(CheckIsZeroOrNothing(txtplace_exibition.Text) = True, txtplace_exibition.Text & vbCrLf, "") & _
                                                                                IIf(_IsAgent = False, "", CheckInvAgent(_IsAgent, _Invoice_Agent, _CompanyName_Agent)) & _
                                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 3 'T3_place_exibition
                All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                                             & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & Form_NewStr_AddressDetail(CommonUtility.Get_StringValue(txtByCom_CH03.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                    BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
                                                                    BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " & _
                                                                    BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf & "        " & _
                                                                    BahtOnly(txtquantity5.Text, txtq_unit_code5.Text) & vbCrLf & String_Total_USDOnly_New_Attach(txtDisplayUDS7.Text, txtCurrency_Code.Text, IIf(_IsAgent = False, TFob, _TAgent), CommonUtility.Get_StringValue(txtshow_check.Text), TUSD, TUSDOther, txtgross_weightH.Value, txtg_unit_code.Text, txtinvh_run_auto.Value) & _
                                                                    IIf(_IsAgent = False, "", CheckInvAgent(_IsAgent, _Invoice_Agent, _CompanyName_Agent)) & _
                                                                    "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 4 '��������
                All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                                             & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & Form_NewStr_AddressDetail(CommonUtility.Get_StringValue(txtByCom_CH03.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                    BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
                                                                    BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " & _
                                                                    BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf & "        " & _
                                                                    BahtOnly(txtquantity5.Text, txtq_unit_code5.Text) & vbCrLf & String_Total_USDOnly_New_Attach(txtDisplayUDS7.Text, txtCurrency_Code.Text, IIf(_IsAgent = False, TFob, _TAgent), CommonUtility.Get_StringValue(txtshow_check.Text), TUSD, TUSDOther, txtgross_weightH.Value, txtg_unit_code.Text, txtinvh_run_auto.Value) & _
                                                                    IIf(_IsAgent = False, "", CheckInvAgent(_IsAgent, _Invoice_Agent, _CompanyName_Agent)) & _
                                                                    "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 5 'T2_back_country
                All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                                             & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & Form_NewStr_AddressDetail(CommonUtility.Get_StringValue(txtByCom_CH03.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                    BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
                                                                    BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " & _
                                                                    BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf & "        " & _
                                                                    BahtOnly(txtquantity5.Text, txtq_unit_code5.Text) & vbCrLf & String_Total_USDOnly_New_Attach(txtDisplayUDS7.Text, txtCurrency_Code.Text, IIf(_IsAgent = False, TFob, _TAgent), CommonUtility.Get_StringValue(txtshow_check.Text), TUSD, TUSDOther, txtgross_weightH.Value, txtg_unit_code.Text, txtinvh_run_auto.Value) & vbNewLine & _
                                                                    _TempB2B & vbNewLine & _
                                                                    IIf(_IsAgent = False, "", CheckInvAgent(_IsAgent, _Invoice_Agent, _CompanyName_Agent)) & _
                                                                    "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 6 'T3_place_exibition
                All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                                             & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & Form_NewStr_AddressDetail(CommonUtility.Get_StringValue(txtByCom_CH03.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                    BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
                                                                    BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " & _
                                                                    BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf & "        " & _
                                                                    BahtOnly(txtquantity5.Text, txtq_unit_code5.Text) & vbCrLf & String_Total_USDOnly_New_Attach(txtDisplayUDS7.Text, txtCurrency_Code.Text, IIf(_IsAgent = False, TFob, _TAgent), CommonUtility.Get_StringValue(txtshow_check.Text), TUSD, TUSDOther, txtgross_weightH.Value, txtg_unit_code.Text, txtinvh_run_auto.Value) & _
                                                                    IIf(_IsAgent = False, "", CheckInvAgent(_IsAgent, _Invoice_Agent, _CompanyName_Agent)) & _
                                                                    "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 7 'T1_third_country,T2_back_country
                All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                                             & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & Form_NewStr_AddressDetail(CommonUtility.Get_StringValue(txtByCom_CH03.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                    BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
                                                                    BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " & _
                                                                    BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf & "        " & _
                                                                    BahtOnly(txtquantity5.Text, txtq_unit_code5.Text) & vbCrLf & _
                                                                    String_Total_USDOnly_New_Attach(txtDisplayUDS7.Text, txtCurrency_Code.Text, IIf(_IsAgent = False, TFob, _TAgent), CommonUtility.Get_StringValue(txtshow_check.Text), TUSD, TUSDOther, txtgross_weightH.Value, txtg_unit_code.Text, txtinvh_run_auto.Value) & "        " & _
                                                                    IIf(CheckIsZeroOrNothing(txtthird_country.Text) = True, txtthird_country.Text & vbCrLf, "") & _
                                                                    IIf(CheckIsZeroOrNothing(txtback_country.Text) = True, txtback_country.Text & vbCrLf, "") & _
                                                                    IIf(_IsAgent = False, "", CheckInvAgent(_IsAgent, _Invoice_Agent, _CompanyName_Agent)) & _
                                                                    "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 8 'T1_third_country,T3_place_exibition
                All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                                             & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & Form_NewStr_AddressDetail(CommonUtility.Get_StringValue(txtByCom_CH03.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                    BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
                                                                    BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " & _
                                                                    BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf & "        " & _
                                                                    BahtOnly(txtquantity5.Text, txtq_unit_code5.Text) & vbCrLf & _
                                                                    String_Total_USDOnly_New_Attach(txtDisplayUDS7.Text, txtCurrency_Code.Text, IIf(_IsAgent = False, TFob, _TAgent), CommonUtility.Get_StringValue(txtshow_check.Text), TUSD, TUSDOther, txtgross_weightH.Value, txtg_unit_code.Text, txtinvh_run_auto.Value) & "        " & _
                                                                    IIf(CheckIsZeroOrNothing(txtthird_country.Text) = True, txtthird_country.Text & vbCrLf, "") & _
                                                                    IIf(CheckIsZeroOrNothing(txtplace_exibition.Text) = True, txtplace_exibition.Text & vbCrLf, "") & _
                                                                    IIf(_IsAgent = False, "", CheckInvAgent(_IsAgent, _Invoice_Agent, _CompanyName_Agent)) & _
                                                                    "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 9 'T1_third_country
                All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                                             & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & Form_NewStr_AddressDetail(CommonUtility.Get_StringValue(txtByCom_CH03.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity5.Text, txtq_unit_code5.Text) & vbCrLf & _
                                                                String_Total_USDOnly_New_Attach(txtDisplayUDS7.Text, txtCurrency_Code.Text, IIf(_IsAgent = False, TFob, _TAgent), CommonUtility.Get_StringValue(txtshow_check.Text), TUSD, TUSDOther, txtgross_weightH.Value, txtg_unit_code.Text, txtinvh_run_auto.Value) & "        " & _
                                                                IIf(CheckIsZeroOrNothing(txtthird_country.Text) = True, txtthird_country.Text & vbCrLf, "") & _
                                                                IIf(_IsAgent = False, "", CheckInvAgent(_IsAgent, _Invoice_Agent, _CompanyName_Agent)) & _
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
                    txtT_product.Text = All_str & Form_NewStr_AddressDetail(CommonUtility.Get_StringValue(txtByCom_CH03.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                            BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
                                            BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " & _
                                            BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf & _
                                            String_Total_USDOnly_New_Attach(txtDisplayUDS7.Text, txtCurrency_Code.Text, IIf(_IsAgent = False, TFob, _TAgent), CommonUtility.Get_StringValue(txtshow_check.Text), TUSD, TUSDOther, txtgross_weightH.Value, txtg_unit_code.Text, txtinvh_run_auto.Value) & "        " & _
                                            IIf(CheckIsZeroOrNothing(txtthird_country.Text) = True, txtthird_country.Text & vbCrLf, "") & _
                                            IIf(CheckIsZeroOrNothing(txtback_country.Text) = True, txtback_country.Text & vbCrLf, "") & _
                                            IIf(CheckIsZeroOrNothing(txtplace_exibition.Text) = True, txtplace_exibition.Text & vbCrLf, "") & _
                                            IIf(_IsAgent = False, "", CheckInvAgent(_IsAgent, _Invoice_Agent, _CompanyName_Agent)) & _
                                            "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 2 'T2_back_country,T3_place_exibition
                All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                                             & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & Form_NewStr_AddressDetail(CommonUtility.Get_StringValue(txtByCom_CH03.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                            BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
                                                            BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " & _
                                                            BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf & _
                                                            String_Total_USDOnly_New_Attach(txtDisplayUDS7.Text, txtCurrency_Code.Text, IIf(_IsAgent = False, TFob, _TAgent), CommonUtility.Get_StringValue(txtshow_check.Text), TUSD, TUSDOther, txtgross_weightH.Value, txtg_unit_code.Text, txtinvh_run_auto.Value) & "        " & _
                                                            IIf(CheckIsZeroOrNothing(txtback_country.Text) = True, txtback_country.Text & vbCrLf, "") & _
                                                            IIf(CheckIsZeroOrNothing(txtplace_exibition.Text) = True, txtplace_exibition.Text & vbCrLf, "") & _
                                                            IIf(_IsAgent = False, "", CheckInvAgent(_IsAgent, _Invoice_Agent, _CompanyName_Agent)) & _
                                                            "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 3 'T3_place_exibition
                All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                                             & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & Form_NewStr_AddressDetail(CommonUtility.Get_StringValue(txtByCom_CH03.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf & String_Total_USDOnly_New_Attach(txtDisplayUDS7.Text, txtCurrency_Code.Text, IIf(_IsAgent = False, TFob, _TAgent), CommonUtility.Get_StringValue(txtshow_check.Text), TUSD, TUSDOther, txtgross_weightH.Value, txtg_unit_code.Text, txtinvh_run_auto.Value) & _
                                                                IIf(_IsAgent = False, "", CheckInvAgent(_IsAgent, _Invoice_Agent, _CompanyName_Agent)) & _
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 4 '��������
                All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                                             & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & Form_NewStr_AddressDetail(CommonUtility.Get_StringValue(txtByCom_CH03.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf & String_Total_USDOnly_New_Attach(txtDisplayUDS7.Text, txtCurrency_Code.Text, IIf(_IsAgent = False, TFob, _TAgent), CommonUtility.Get_StringValue(txtshow_check.Text), TUSD, TUSDOther, txtgross_weightH.Value, txtg_unit_code.Text, txtinvh_run_auto.Value) & _
                                                                IIf(_IsAgent = False, "", CheckInvAgent(_IsAgent, _Invoice_Agent, _CompanyName_Agent)) & _
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 5 'T2_back_country
                All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                                             & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & Form_NewStr_AddressDetail(CommonUtility.Get_StringValue(txtByCom_CH03.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf & String_Total_USDOnly_New_Attach(txtDisplayUDS7.Text, txtCurrency_Code.Text, IIf(_IsAgent = False, TFob, _TAgent), CommonUtility.Get_StringValue(txtshow_check.Text), TUSD, TUSDOther, txtgross_weightH.Value, txtg_unit_code.Text, txtinvh_run_auto.Value) & vbNewLine & _
                                                                    _TempB2B & vbNewLine & _
                                                                    IIf(_IsAgent = False, "", CheckInvAgent(_IsAgent, _Invoice_Agent, _CompanyName_Agent)) & _
                                                                    "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 6 'T3_place_exibition
                All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                                             & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & Form_NewStr_AddressDetail(CommonUtility.Get_StringValue(txtByCom_CH03.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf & String_Total_USDOnly_New_Attach(txtDisplayUDS7.Text, txtCurrency_Code.Text, IIf(_IsAgent = False, TFob, _TAgent), CommonUtility.Get_StringValue(txtshow_check.Text), TUSD, TUSDOther, txtgross_weightH.Value, txtg_unit_code.Text, txtinvh_run_auto.Value) & _
                                                                IIf(_IsAgent = False, "", CheckInvAgent(_IsAgent, _Invoice_Agent, _CompanyName_Agent)) & _
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 7 'T1_third_country,T2_back_country
                All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                                             & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & Form_NewStr_AddressDetail(CommonUtility.Get_StringValue(txtByCom_CH03.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                    BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
                                                                    BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " & _
                                                                    BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf & _
                                                                    String_Total_USDOnly_New_Attach(txtDisplayUDS7.Text, txtCurrency_Code.Text, IIf(_IsAgent = False, TFob, _TAgent), CommonUtility.Get_StringValue(txtshow_check.Text), TUSD, TUSDOther, txtgross_weightH.Value, txtg_unit_code.Text, txtinvh_run_auto.Value) & "        " & _
                                                                    IIf(CheckIsZeroOrNothing(txtthird_country.Text) = True, txtthird_country.Text & vbCrLf, "") & _
                                                                    IIf(CheckIsZeroOrNothing(txtback_country.Text) = True, txtback_country.Text & vbCrLf, "") & _
                                                                    IIf(_IsAgent = False, "", CheckInvAgent(_IsAgent, _Invoice_Agent, _CompanyName_Agent)) & _
                                                                    "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 8 'T1_third_country,T3_place_exibition
                All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                                             & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & Form_NewStr_AddressDetail(CommonUtility.Get_StringValue(txtByCom_CH03.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                    BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
                                                                    BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " & _
                                                                    BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf & _
                                                                    String_Total_USDOnly_New_Attach(txtDisplayUDS7.Text, txtCurrency_Code.Text, IIf(_IsAgent = False, TFob, _TAgent), CommonUtility.Get_StringValue(txtshow_check.Text), TUSD, TUSDOther, txtgross_weightH.Value, txtg_unit_code.Text, txtinvh_run_auto.Value) & "        " & _
                                                                    IIf(CheckIsZeroOrNothing(txtthird_country.Text) = True, txtthird_country.Text & vbCrLf, "") & _
                                                                    IIf(CheckIsZeroOrNothing(txtplace_exibition.Text) = True, txtplace_exibition.Text & vbCrLf, "") & _
                                                                    IIf(_IsAgent = False, "", CheckInvAgent(_IsAgent, _Invoice_Agent, _CompanyName_Agent)) & _
                                                                    "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 9 'T1_third_country
                All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                                             & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & Form_NewStr_AddressDetail(CommonUtility.Get_StringValue(txtByCom_CH03.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                    BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
                                                                    BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " & _
                                                                    BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf & _
                                                                    String_Total_USDOnly_New_Attach(txtDisplayUDS7.Text, txtCurrency_Code.Text, IIf(_IsAgent = False, TFob, _TAgent), CommonUtility.Get_StringValue(txtshow_check.Text), TUSD, TUSDOther, txtgross_weightH.Value, txtg_unit_code.Text, txtinvh_run_auto.Value) & "        " & _
                                                                    IIf(CheckIsZeroOrNothing(txtthird_country.Text) = True, txtthird_country.Text & vbCrLf, "") & _
                                                                    IIf(_IsAgent = False, "", CheckInvAgent(_IsAgent, _Invoice_Agent, _CompanyName_Agent)) & _
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
                    txtT_product.Text = All_str & Form_NewStr_AddressDetail(CommonUtility.Get_StringValue(txtByCom_CH03.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & _
                                                                String_Total_USDOnly_New_Attach(txtDisplayUDS7.Text, txtCurrency_Code.Text, IIf(_IsAgent = False, TFob, _TAgent), CommonUtility.Get_StringValue(txtshow_check.Text), TUSD, TUSDOther, txtgross_weightH.Value, txtg_unit_code.Text, txtinvh_run_auto.Value) & "        " & _
                                                                IIf(CheckIsZeroOrNothing(txtthird_country.Text) = True, txtthird_country.Text & vbCrLf, "") & _
                                                                IIf(CheckIsZeroOrNothing(txtback_country.Text) = True, txtback_country.Text & vbCrLf, "") & _
                                                                IIf(CheckIsZeroOrNothing(txtplace_exibition.Text) = True, txtplace_exibition.Text & vbCrLf, "") & _
                                                                IIf(_IsAgent = False, "", CheckInvAgent(_IsAgent, _Invoice_Agent, _CompanyName_Agent)) & _
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 2 'T2_back_country,T3_place_exibition
                All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                                             & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & Form_NewStr_AddressDetail(CommonUtility.Get_StringValue(txtByCom_CH03.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
                                                                                BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & _
                                                                                String_Total_USDOnly_New_Attach(txtDisplayUDS7.Text, txtCurrency_Code.Text, IIf(_IsAgent = False, TFob, _TAgent), CommonUtility.Get_StringValue(txtshow_check.Text), TUSD, TUSDOther, txtgross_weightH.Value, txtg_unit_code.Text, txtinvh_run_auto.Value) & "        " & _
                                                                                IIf(CheckIsZeroOrNothing(txtback_country.Text) = True, txtback_country.Text & vbCrLf, "") & _
                                                                                IIf(CheckIsZeroOrNothing(txtplace_exibition.Text) = True, txtplace_exibition.Text & vbCrLf, "") & _
                                                                                IIf(_IsAgent = False, "", CheckInvAgent(_IsAgent, _Invoice_Agent, _CompanyName_Agent)) & _
                                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 3 'T3_place_exibition
                All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                                             & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & Form_NewStr_AddressDetail(CommonUtility.Get_StringValue(txtByCom_CH03.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & String_Total_USDOnly_New_Attach(txtDisplayUDS7.Text, txtCurrency_Code.Text, IIf(_IsAgent = False, TFob, _TAgent), CommonUtility.Get_StringValue(txtshow_check.Text), TUSD, TUSDOther, txtgross_weightH.Value, txtg_unit_code.Text, txtinvh_run_auto.Value) & _
                                                                IIf(_IsAgent = False, "", CheckInvAgent(_IsAgent, _Invoice_Agent, _CompanyName_Agent)) & _
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 4 '��������
                All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                                             & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & Form_NewStr_AddressDetail(CommonUtility.Get_StringValue(txtByCom_CH03.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & String_Total_USDOnly_New_Attach(txtDisplayUDS7.Text, txtCurrency_Code.Text, IIf(_IsAgent = False, TFob, _TAgent), CommonUtility.Get_StringValue(txtshow_check.Text), TUSD, TUSDOther, txtgross_weightH.Value, txtg_unit_code.Text, txtinvh_run_auto.Value) & _
                                                                IIf(_IsAgent = False, "", CheckInvAgent(_IsAgent, _Invoice_Agent, _CompanyName_Agent)) & _
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 5 'T2_back_country
                All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                                             & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & Form_NewStr_AddressDetail(CommonUtility.Get_StringValue(txtByCom_CH03.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & String_Total_USDOnly_New_Attach(txtDisplayUDS7.Text, txtCurrency_Code.Text, IIf(_IsAgent = False, TFob, _TAgent), CommonUtility.Get_StringValue(txtshow_check.Text), TUSD, TUSDOther, txtgross_weightH.Value, txtg_unit_code.Text, txtinvh_run_auto.Value) & vbNewLine & _
                                                                    _TempB2B & vbNewLine & _
                                                                    IIf(_IsAgent = False, "", CheckInvAgent(_IsAgent, _Invoice_Agent, _CompanyName_Agent)) & _
                                                                    "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 6 'T3_place_exibition
                All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                                             & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & Form_NewStr_AddressDetail(CommonUtility.Get_StringValue(txtByCom_CH03.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & String_Total_USDOnly_New_Attach(txtDisplayUDS7.Text, txtCurrency_Code.Text, IIf(_IsAgent = False, TFob, _TAgent), CommonUtility.Get_StringValue(txtshow_check.Text), TUSD, TUSDOther, txtgross_weightH.Value, txtg_unit_code.Text, txtinvh_run_auto.Value) & _
                                                                IIf(_IsAgent = False, "", CheckInvAgent(_IsAgent, _Invoice_Agent, _CompanyName_Agent)) & _
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 7 'T1_third_country,T2_back_country
                All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                                              & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & Form_NewStr_AddressDetail(CommonUtility.Get_StringValue(txtByCom_CH03.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & _
                                                                String_Total_USDOnly_New_Attach(txtDisplayUDS7.Text, txtCurrency_Code.Text, IIf(_IsAgent = False, TFob, _TAgent), CommonUtility.Get_StringValue(txtshow_check.Text), TUSD, TUSDOther, txtgross_weightH.Value, txtg_unit_code.Text, txtinvh_run_auto.Value) & "        " & _
                                                                IIf(CheckIsZeroOrNothing(txtthird_country.Text) = True, txtthird_country.Text & vbCrLf, "") & _
                                                                IIf(CheckIsZeroOrNothing(txtback_country.Text) = True, txtback_country.Text & vbCrLf, "") & _
                                                                IIf(_IsAgent = False, "", CheckInvAgent(_IsAgent, _Invoice_Agent, _CompanyName_Agent)) & _
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 8 'T1_third_country,T3_place_exibition
                All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                                              & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & Form_NewStr_AddressDetail(CommonUtility.Get_StringValue(txtByCom_CH03.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & _
                                                                String_Total_USDOnly_New_Attach(txtDisplayUDS7.Text, txtCurrency_Code.Text, IIf(_IsAgent = False, TFob, _TAgent), CommonUtility.Get_StringValue(txtshow_check.Text), TUSD, TUSDOther, txtgross_weightH.Value, txtg_unit_code.Text, txtinvh_run_auto.Value) & "        " & _
                                                                IIf(CheckIsZeroOrNothing(txtthird_country.Text) = True, txtthird_country.Text & vbCrLf, "") & _
                                                                IIf(CheckIsZeroOrNothing(txtplace_exibition.Text) = True, txtplace_exibition.Text & vbCrLf, "") & _
                                                                IIf(_IsAgent = False, "", CheckInvAgent(_IsAgent, _Invoice_Agent, _CompanyName_Agent)) & _
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 9 'T1_third_country
                All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                                             & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & Form_NewStr_AddressDetail(CommonUtility.Get_StringValue(txtByCom_CH03.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                    BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
                                                                    BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & _
                                                                    String_Total_USDOnly_New_Attach(txtDisplayUDS7.Text, txtCurrency_Code.Text, IIf(_IsAgent = False, TFob, _TAgent), CommonUtility.Get_StringValue(txtshow_check.Text), TUSD, TUSDOther, txtgross_weightH.Value, txtg_unit_code.Text, txtinvh_run_auto.Value) & "        " & _
                                                                    IIf(CheckIsZeroOrNothing(txtthird_country.Text) = True, txtthird_country.Text & vbCrLf, "") & _
                                                                    IIf(_IsAgent = False, "", CheckInvAgent(_IsAgent, _Invoice_Agent, _CompanyName_Agent)) & _
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
                    txtT_product.Text = All_str & Form_NewStr_AddressDetail(CommonUtility.Get_StringValue(txtByCom_CH03.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & _
                                                                String_Total_USDOnly_New_Attach(txtDisplayUDS7.Text, txtCurrency_Code.Text, IIf(_IsAgent = False, TFob, _TAgent), CommonUtility.Get_StringValue(txtshow_check.Text), TUSD, TUSDOther, txtgross_weightH.Value, txtg_unit_code.Text, txtinvh_run_auto.Value) & "        " & _
                                                                IIf(CheckIsZeroOrNothing(txtthird_country.Text) = True, txtthird_country.Text & vbCrLf, "") & _
                                                                IIf(CheckIsZeroOrNothing(txtback_country.Text) = True, txtback_country.Text & vbCrLf, "") & _
                                                                IIf(CheckIsZeroOrNothing(txtplace_exibition.Text) = True, txtplace_exibition.Text & vbCrLf, "") & _
                                                                IIf(_IsAgent = False, "", CheckInvAgent(_IsAgent, _Invoice_Agent, _CompanyName_Agent)) & _
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 2 'T2_back_country,T3_place_exibition
                All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                                             & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & Form_NewStr_AddressDetail(CommonUtility.Get_StringValue(txtByCom_CH03.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & _
                                                                                String_Total_USDOnly_New_Attach(txtDisplayUDS7.Text, txtCurrency_Code.Text, IIf(_IsAgent = False, TFob, _TAgent), CommonUtility.Get_StringValue(txtshow_check.Text), TUSD, TUSDOther, txtgross_weightH.Value, txtg_unit_code.Text, txtinvh_run_auto.Value) & "        " & _
                                                                                IIf(CheckIsZeroOrNothing(txtback_country.Text) = True, txtback_country.Text & vbCrLf, "") & _
                                                                                IIf(CheckIsZeroOrNothing(txtplace_exibition.Text) = True, txtplace_exibition.Text & vbCrLf, "") & _
                                                                                IIf(_IsAgent = False, "", CheckInvAgent(_IsAgent, _Invoice_Agent, _CompanyName_Agent)) & _
                                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 3 'T3_place_exibition
                All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                                             & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & Form_NewStr_AddressDetail(CommonUtility.Get_StringValue(txtByCom_CH03.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & String_Total_USDOnly_New_Attach(txtDisplayUDS7.Text, txtCurrency_Code.Text, IIf(_IsAgent = False, TFob, _TAgent), CommonUtility.Get_StringValue(txtshow_check.Text), TUSD, TUSDOther, txtgross_weightH.Value, txtg_unit_code.Text, txtinvh_run_auto.Value) & _
                                                                IIf(_IsAgent = False, "", CheckInvAgent(_IsAgent, _Invoice_Agent, _CompanyName_Agent)) & _
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 4 '��������
                All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                                             & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Value = All_str & Form_NewStr_AddressDetail(CommonUtility.Get_StringValue(txtByCom_CH03.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & String_Total_USDOnly_New_Attach(txtDisplayUDS7.Text, txtCurrency_Code.Text, IIf(_IsAgent = False, TFob, _TAgent), CommonUtility.Get_StringValue(txtshow_check.Text), TUSD, TUSDOther, txtgross_weightH.Value, txtg_unit_code.Text, txtinvh_run_auto.Value) & _
                                                                IIf(_IsAgent = False, "", CheckInvAgent(_IsAgent, _Invoice_Agent, _CompanyName_Agent)) & _
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 5 'T2_back_country
                All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                                             & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & Form_NewStr_AddressDetail(CommonUtility.Get_StringValue(txtByCom_CH03.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & String_Total_USDOnly_New_Attach(txtDisplayUDS7.Text, txtCurrency_Code.Text, IIf(_IsAgent = False, TFob, _TAgent), CommonUtility.Get_StringValue(txtshow_check.Text), TUSD, TUSDOther, txtgross_weightH.Value, txtg_unit_code.Text, txtinvh_run_auto.Value) & vbNewLine & _
                                                                    _TempB2B & vbNewLine & _
                                                                    IIf(_IsAgent = False, "", CheckInvAgent(_IsAgent, _Invoice_Agent, _CompanyName_Agent)) & _
                                                                    "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 6 'T3_place_exibition
                All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                                             & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & Form_NewStr_AddressDetail(CommonUtility.Get_StringValue(txtByCom_CH03.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & String_Total_USDOnly_New_Attach(txtDisplayUDS7.Text, txtCurrency_Code.Text, IIf(_IsAgent = False, TFob, _TAgent), CommonUtility.Get_StringValue(txtshow_check.Text), TUSD, TUSDOther, txtgross_weightH.Value, txtg_unit_code.Text, txtinvh_run_auto.Value) & _
                                                                IIf(_IsAgent = False, "", CheckInvAgent(_IsAgent, _Invoice_Agent, _CompanyName_Agent)) & _
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 7 'T1_third_country,T2_back_country
                All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                                             & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & Form_NewStr_AddressDetail(CommonUtility.Get_StringValue(txtByCom_CH03.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & _
                                                                String_Total_USDOnly_New_Attach(txtDisplayUDS7.Text, txtCurrency_Code.Text, IIf(_IsAgent = False, TFob, _TAgent), CommonUtility.Get_StringValue(txtshow_check.Text), TUSD, TUSDOther, txtgross_weightH.Value, txtg_unit_code.Text, txtinvh_run_auto.Value) & "        " & _
                                                                IIf(CheckIsZeroOrNothing(txtthird_country.Text) = True, txtthird_country.Text & vbCrLf, "") & _
                                                                IIf(CheckIsZeroOrNothing(txtback_country.Text) = True, txtback_country.Text & vbCrLf, "") & _
                                                                IIf(_IsAgent = False, "", CheckInvAgent(_IsAgent, _Invoice_Agent, _CompanyName_Agent)) & _
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 8 'T1_third_country,T3_place_exibition
                All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                                             & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & Form_NewStr_AddressDetail(CommonUtility.Get_StringValue(txtByCom_CH03.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & _
                                                                String_Total_USDOnly_New_Attach(txtDisplayUDS7.Text, txtCurrency_Code.Text, IIf(_IsAgent = False, TFob, _TAgent), CommonUtility.Get_StringValue(txtshow_check.Text), TUSD, TUSDOther, txtgross_weightH.Value, txtg_unit_code.Text, txtinvh_run_auto.Value) & "        " & _
                                                                IIf(CheckIsZeroOrNothing(txtthird_country.Text) = True, txtthird_country.Text & vbCrLf, "") & _
                                                                IIf(CheckIsZeroOrNothing(txtplace_exibition.Text) = True, txtplace_exibition.Text & vbCrLf, "") & _
                                                                IIf(_IsAgent = False, "", CheckInvAgent(_IsAgent, _Invoice_Agent, _CompanyName_Agent)) & _
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 9 'T1_third_country
                All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                                             & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & Form_NewStr_AddressDetail(CommonUtility.Get_StringValue(txtByCom_CH03.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                    BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & _
                                                                    String_Total_USDOnly_New_Attach(txtDisplayUDS7.Text, txtCurrency_Code.Text, IIf(_IsAgent = False, TFob, _TAgent), CommonUtility.Get_StringValue(txtshow_check.Text), TUSD, TUSDOther, txtgross_weightH.Value, txtg_unit_code.Text, txtinvh_run_auto.Value) & "        " & _
                                                                    IIf(CheckIsZeroOrNothing(txtthird_country.Text) = True, txtthird_country.Text & vbCrLf, "") & _
                                                                    IIf(_IsAgent = False, "", CheckInvAgent(_IsAgent, _Invoice_Agent, _CompanyName_Agent)) & _
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
                    If (Not txtquantity1.Text = String.Empty = True) And CommonUtility.Get_StringValue(txtquantity1.Text) <> "" And CommonUtility.Get_Decimal(IIf(Not (txtquantity1.Text = String.Empty) = False, 0, txtquantity1.Value)) <> 0 Then
                        txtT_product.Text = All_str & Form_NewStr_AddressDetail(CommonUtility.Get_StringValue(txtByCom_CH03.Text))
                        GroupFooter1.Visible = True
                        txtTotalAll.Visible = True
                        txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & _
                                            String_Total_USDOnly_New_Attach(txtDisplayUDS7.Text, txtCurrency_Code.Text, IIf(_IsAgent = False, TFob, _TAgent), CommonUtility.Get_StringValue(txtshow_check.Text), TUSD, TUSDOther, txtgross_weightH.Value, txtg_unit_code.Text, txtinvh_run_auto.Value) & "        " & _
                                                                                        IIf(CheckIsZeroOrNothing(txtthird_country.Text) = True, txtthird_country.Text & vbCrLf, "") & _
                                                                                        IIf(CheckIsZeroOrNothing(txtback_country.Text) = True, txtback_country.Text & vbCrLf, "") & _
                                                                                        IIf(CheckIsZeroOrNothing(txtplace_exibition.Text) = True, txtplace_exibition.Text & vbCrLf, "") & _
                                                                                        IIf(_IsAgent = False, "", CheckInvAgent(_IsAgent, _Invoice_Agent, _CompanyName_Agent)) & _
                                                                                        "       _____________________________"
                    Else
                        txtT_product.Text = All_str & _
                                           "        " & _
                                                                                        IIf(CheckIsZeroOrNothing(txtthird_country.Text) = True, txtthird_country.Text & vbCrLf, "") & _
                                                                                        IIf(CheckIsZeroOrNothing(txtback_country.Text) = True, txtback_country.Text & vbCrLf, "") & _
                                                                                        IIf(CheckIsZeroOrNothing(txtplace_exibition.Text) = True, txtplace_exibition.Text & vbCrLf, "") & _
                                                                                        IIf(_IsAgent = False, "", CheckInvAgent(_IsAgent, _Invoice_Agent, _CompanyName_Agent)) & _
                                                                                        "       _____________________________"
                    End If

                Else
                    txtT_product.Text = All_str
                End If
            Case 2 'T2_back_country,T3_place_exibition
                All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                                             & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
                If C_TotalRowDe.Text = count_data Then
                    If (Not txtquantity1.Text = String.Empty = True) And CommonUtility.Get_StringValue(txtquantity1.Text) <> "" And CommonUtility.Get_Decimal(IIf(Not (txtquantity1.Text = String.Empty) = False, 0, txtquantity1.Value)) <> 0 Then
                        txtT_product.Text = All_str & Form_NewStr_AddressDetail(CommonUtility.Get_StringValue(txtByCom_CH03.Text))
                        GroupFooter1.Visible = True
                        txtTotalAll.Visible = True
                        txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & _
                                            String_Total_USDOnly_New_Attach(txtDisplayUDS7.Text, txtCurrency_Code.Text, IIf(_IsAgent = False, TFob, _TAgent), CommonUtility.Get_StringValue(txtshow_check.Text), TUSD, TUSDOther, txtgross_weightH.Value, txtg_unit_code.Text, txtinvh_run_auto.Value) & "        " & _
                                                                                                        IIf(CheckIsZeroOrNothing(txtback_country.Text) = True, txtback_country.Text & vbCrLf, "") & _
                                                                                                        IIf(CheckIsZeroOrNothing(txtplace_exibition.Text) = True, txtplace_exibition.Text & vbCrLf, "") & _
                                                                                                        IIf(_IsAgent = False, "", CheckInvAgent(_IsAgent, _Invoice_Agent, _CompanyName_Agent)) & _
                                                                                                        "       _____________________________"
                    Else
                        txtT_product.Text = All_str & _
                                            "        " & _
                                                                                                        IIf(CheckIsZeroOrNothing(txtback_country.Text) = True, txtback_country.Text & vbCrLf, "") & _
                                                                                                        IIf(CheckIsZeroOrNothing(txtplace_exibition.Text) = True, txtplace_exibition.Text & vbCrLf, "") & _
                                                                                                        IIf(_IsAgent = False, "", CheckInvAgent(_IsAgent, _Invoice_Agent, _CompanyName_Agent)) & _
                                                                                                        "       _____________________________"
                    End If

                Else
                    txtT_product.Text = All_str
                End If
            Case 3 'T3_place_exibition
                All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                                             & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
                If C_TotalRowDe.Text = count_data Then
                    If (Not txtquantity1.Text = String.Empty = True) And CommonUtility.Get_StringValue(txtquantity1.Text) <> "" And CommonUtility.Get_Decimal(IIf(Not (txtquantity1.Text = String.Empty) = False, 0, txtquantity1.Value)) <> 0 Then
                        txtT_product.Text = All_str & Form_NewStr_AddressDetail(CommonUtility.Get_StringValue(txtByCom_CH03.Text))
                        GroupFooter1.Visible = True
                        txtTotalAll.Visible = True
                        txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & String_Total_USDOnly_New_Attach(txtDisplayUDS7.Text, txtCurrency_Code.Text, IIf(_IsAgent = False, TFob, _TAgent), CommonUtility.Get_StringValue(txtshow_check.Text), TUSD, TUSDOther, txtgross_weightH.Value, txtg_unit_code.Text, txtinvh_run_auto.Value) & _
                        IIf(_IsAgent = False, "", CheckInvAgent(_IsAgent, _Invoice_Agent, _CompanyName_Agent)) & _
                                                                                        "       _____________________________"
                    Else
                        txtT_product.Text = All_str & _
                        IIf(_IsAgent = False, "", CheckInvAgent(_IsAgent, _Invoice_Agent, _CompanyName_Agent)) & _
                                           "       _____________________________"
                    End If

                Else
                    txtT_product.Text = All_str
                End If
            Case 4 '��������
                All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                                              & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
                If C_TotalRowDe.Text = count_data Then
                    If (Not txtquantity1.Text = String.Empty = True) And CommonUtility.Get_StringValue(txtquantity1.Text) <> "" And CommonUtility.Get_Decimal(IIf(Not (txtquantity1.Text = String.Empty) = False, 0, txtquantity1.Value)) <> 0 Then
                        txtT_product.Text = All_str & Form_NewStr_AddressDetail(CommonUtility.Get_StringValue(txtByCom_CH03.Text))
                        GroupFooter1.Visible = True
                        txtTotalAll.Visible = True
                        txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & String_Total_USDOnly_New_Attach(txtDisplayUDS7.Text, txtCurrency_Code.Text, IIf(_IsAgent = False, TFob, _TAgent), CommonUtility.Get_StringValue(txtshow_check.Text), TUSD, TUSDOther, txtgross_weightH.Value, txtg_unit_code.Text, txtinvh_run_auto.Value) & _
                        IIf(_IsAgent = False, "", CheckInvAgent(_IsAgent, _Invoice_Agent, _CompanyName_Agent)) & _
                                                                                        "       _____________________________"
                    Else
                        txtT_product.Text = All_str & _
                        IIf(_IsAgent = False, "", CheckInvAgent(_IsAgent, _Invoice_Agent, _CompanyName_Agent)) & _
                                            "       _____________________________"
                    End If

                Else
                    txtT_product.Text = All_str
                End If
            Case 5 'T2_back_country
                All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                                             & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
                If C_TotalRowDe.Text = count_data Then
                    If (Not txtquantity1.Text = String.Empty = True) And CommonUtility.Get_StringValue(txtquantity1.Text) <> "" And CommonUtility.Get_Decimal(IIf(Not (txtquantity1.Text = String.Empty) = False, 0, txtquantity1.Value)) <> 0 Then
                        txtT_product.Text = All_str & Form_NewStr_AddressDetail(CommonUtility.Get_StringValue(txtByCom_CH03.Text))
                        GroupFooter1.Visible = True
                        txtTotalAll.Visible = True
                        txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & String_Total_USDOnly_New_Attach(txtDisplayUDS7.Text, txtCurrency_Code.Text, IIf(_IsAgent = False, TFob, _TAgent), CommonUtility.Get_StringValue(txtshow_check.Text), TUSD, TUSDOther, txtgross_weightH.Value, txtg_unit_code.Text, txtinvh_run_auto.Value) & vbNewLine & _
                                                                    _TempB2B & vbNewLine & _
                                                                    IIf(_IsAgent = False, "", CheckInvAgent(_IsAgent, _Invoice_Agent, _CompanyName_Agent)) & _
                                                                    "       _____________________________"
                    Else
                        txtT_product.Text = All_str & _
                        _TempB2B & vbNewLine & _
                        IIf(_IsAgent = False, "", CheckInvAgent(_IsAgent, _Invoice_Agent, _CompanyName_Agent)) & _
                                            "       _____________________________"
                    End If

                Else
                    txtT_product.Text = All_str
                End If
            Case 6 'T3_place_exibition
                All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                                             & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
                If C_TotalRowDe.Text = count_data Then
                    If (Not txtquantity1.Text = String.Empty = True) And CommonUtility.Get_StringValue(txtquantity1.Text) <> "" And CommonUtility.Get_Decimal(IIf(Not (txtquantity1.Text = String.Empty) = False, 0, txtquantity1.Value)) <> 0 Then
                        txtT_product.Text = All_str & Form_NewStr_AddressDetail(CommonUtility.Get_StringValue(txtByCom_CH03.Text))
                        GroupFooter1.Visible = True
                        txtTotalAll.Visible = True
                        txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & String_Total_USDOnly_New_Attach(txtDisplayUDS7.Text, txtCurrency_Code.Text, IIf(_IsAgent = False, TFob, _TAgent), CommonUtility.Get_StringValue(txtshow_check.Text), TUSD, TUSDOther, txtgross_weightH.Value, txtg_unit_code.Text, txtinvh_run_auto.Value) & _
                        IIf(_IsAgent = False, "", CheckInvAgent(_IsAgent, _Invoice_Agent, _CompanyName_Agent)) & _
                                                                                        "       _____________________________"
                    Else
                        txtT_product.Text = All_str & _
                        IIf(_IsAgent = False, "", CheckInvAgent(_IsAgent, _Invoice_Agent, _CompanyName_Agent)) & _
                                            "       _____________________________"
                    End If

                Else
                    txtT_product.Text = All_str
                End If
            Case 7 'T1_third_country,T2_back_country
                All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                                              & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
                If C_TotalRowDe.Text = count_data Then
                    If (Not txtquantity1.Text = String.Empty = True) And CommonUtility.Get_StringValue(txtquantity1.Text) <> "" And CommonUtility.Get_Decimal(IIf(Not (txtquantity1.Text = String.Empty) = False, 0, txtquantity1.Value)) <> 0 Then
                        txtT_product.Text = All_str & Form_NewStr_AddressDetail(CommonUtility.Get_StringValue(txtByCom_CH03.Text))
                        GroupFooter1.Visible = True
                        txtTotalAll.Visible = True
                        txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & _
                                            String_Total_USDOnly_New_Attach(txtDisplayUDS7.Text, txtCurrency_Code.Text, IIf(_IsAgent = False, TFob, _TAgent), CommonUtility.Get_StringValue(txtshow_check.Text), TUSD, TUSDOther, txtgross_weightH.Value, txtg_unit_code.Text, txtinvh_run_auto.Value) & "        " & _
                                                                                        IIf(CheckIsZeroOrNothing(txtthird_country.Text) = True, txtthird_country.Text & vbCrLf, "") & _
                                                                                        IIf(CheckIsZeroOrNothing(txtback_country.Text) = True, txtback_country.Text & vbCrLf, "") & _
                                                                                        IIf(_IsAgent = False, "", CheckInvAgent(_IsAgent, _Invoice_Agent, _CompanyName_Agent)) & _
                                                                                        "       _____________________________"
                    Else
                        txtT_product.Text = All_str & _
                                            "        " & _
                                                                                        IIf(CheckIsZeroOrNothing(txtthird_country.Text) = True, txtthird_country.Text & vbCrLf, "") & _
                                                                                        IIf(CheckIsZeroOrNothing(txtback_country.Text) = True, txtback_country.Text & vbCrLf, "") & _
                                                                                        IIf(_IsAgent = False, "", CheckInvAgent(_IsAgent, _Invoice_Agent, _CompanyName_Agent)) & _
                                                                                        "       _____________________________"
                    End If

                Else
                    txtT_product.Text = All_str
                End If
            Case 8 'T1_third_country,T3_place_exibition
                All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                                             & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
                If C_TotalRowDe.Text = count_data Then
                    If (Not txtquantity1.Text = String.Empty = True) And CommonUtility.Get_StringValue(txtquantity1.Text) <> "" And CommonUtility.Get_Decimal(IIf(Not (txtquantity1.Text = String.Empty) = False, 0, txtquantity1.Value)) <> 0 Then
                        txtT_product.Text = All_str & Form_NewStr_AddressDetail(CommonUtility.Get_StringValue(txtByCom_CH03.Text))
                        GroupFooter1.Visible = True
                        txtTotalAll.Visible = True
                        txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & _
                                            String_Total_USDOnly_New_Attach(txtDisplayUDS7.Text, txtCurrency_Code.Text, IIf(_IsAgent = False, TFob, _TAgent), CommonUtility.Get_StringValue(txtshow_check.Text), TUSD, TUSDOther, txtgross_weightH.Value, txtg_unit_code.Text, txtinvh_run_auto.Value) & "        " & _
                                                                                        IIf(CheckIsZeroOrNothing(txtthird_country.Text) = True, txtthird_country.Text & vbCrLf, "") & _
                                                                                        IIf(CheckIsZeroOrNothing(txtplace_exibition.Text) = True, txtplace_exibition.Text & vbCrLf, "") & _
                                                                                        IIf(_IsAgent = False, "", CheckInvAgent(_IsAgent, _Invoice_Agent, _CompanyName_Agent)) & _
                                                                                        "       _____________________________"
                    Else
                        txtT_product.Text = All_str & _
                                            "        " & _
                                                                                        IIf(CheckIsZeroOrNothing(txtthird_country.Text) = True, txtthird_country.Text & vbCrLf, "") & _
                                                                                        IIf(CheckIsZeroOrNothing(txtplace_exibition.Text) = True, txtplace_exibition.Text & vbCrLf, "") & _
                                                                                        IIf(_IsAgent = False, "", CheckInvAgent(_IsAgent, _Invoice_Agent, _CompanyName_Agent)) & _
                                                                                        "       _____________________________"
                    End If

                Else
                    txtT_product.Text = All_str
                End If
            Case 9 'T1_third_country
                All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                                             & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
                If C_TotalRowDe.Text = count_data Then
                    If (Not txtquantity1.Text = String.Empty = True) And CommonUtility.Get_StringValue(txtquantity1.Text) <> "" And CommonUtility.Get_Decimal(IIf(Not (txtquantity1.Text = String.Empty) = False, 0, txtquantity1.Value)) <> 0 Then
                        txtT_product.Text = All_str & Form_NewStr_AddressDetail(CommonUtility.Get_StringValue(txtByCom_CH03.Text))
                        GroupFooter1.Visible = True
                        txtTotalAll.Visible = True
                        txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & _
                                             String_Total_USDOnly_New_Attach(txtDisplayUDS7.Text, txtCurrency_Code.Text, IIf(_IsAgent = False, TFob, _TAgent), CommonUtility.Get_StringValue(txtshow_check.Text), TUSD, TUSDOther, txtgross_weightH.Value, txtg_unit_code.Text, txtinvh_run_auto.Value) & "        " & _
                                                                                            IIf(CheckIsZeroOrNothing(txtthird_country.Text) = True, txtthird_country.Text & vbCrLf, "") & _
                                                                                            IIf(_IsAgent = False, "", CheckInvAgent(_IsAgent, _Invoice_Agent, _CompanyName_Agent)) & _
                                                                                            "       _____________________________"
                    Else
                        txtT_product.Text = All_str & _
                                            "        " & _
                                                                                            IIf(CheckIsZeroOrNothing(txtthird_country.Text) = True, txtthird_country.Text & vbCrLf, "") & _
                                                                                            IIf(_IsAgent = False, "", CheckInvAgent(_IsAgent, _Invoice_Agent, _CompanyName_Agent)) & _
                                                                                            "       _____________________________"
                    End If

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
                    IIf(CheckIsZeroOrNothing(txtthird_country.Text) = True, txtthird_country.Text & vbCrLf, "") & _
                                                                IIf(CheckIsZeroOrNothing(txtback_country.Text) = True, txtback_country.Text & vbCrLf, "") & _
                                                                IIf(CheckIsZeroOrNothing(txtplace_exibition.Text) = True, txtplace_exibition.Text & vbCrLf, "") & _
                                                                IIf(_IsAgent = False, "", CheckInvAgent(_IsAgent, _Invoice_Agent, _CompanyName_Agent)) & _
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 2 'T2_back_country,T3_place_exibition
                All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                                             & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & _
                    IIf(CheckIsZeroOrNothing(txtback_country.Text) = True, txtback_country.Text & vbCrLf, "") & _
                                                                                IIf(CheckIsZeroOrNothing(txtplace_exibition.Text) = True, txtplace_exibition.Text & vbCrLf, "") & _
                                                                                IIf(_IsAgent = False, "", CheckInvAgent(_IsAgent, _Invoice_Agent, _CompanyName_Agent)) & _
                                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 3 'T3_place_exibition
                All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                                             & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & _
                    IIf(_IsAgent = False, "", CheckInvAgent(_IsAgent, _Invoice_Agent, _CompanyName_Agent)) & _
                     "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 4 '��������
                All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                                             & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & _
                    IIf(_IsAgent = False, "", CheckInvAgent(_IsAgent, _Invoice_Agent, _CompanyName_Agent)) & _
                     "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 5 'T2_back_country
                All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                                             & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & vbCrLf & _
                    _TempB2B & vbNewLine & _
                    IIf(_IsAgent = False, "", CheckInvAgent(_IsAgent, _Invoice_Agent, _CompanyName_Agent)) & _
                    "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 6 'T3_place_exibition
                All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                                             & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & _
                    IIf(_IsAgent = False, "", CheckInvAgent(_IsAgent, _Invoice_Agent, _CompanyName_Agent)) & _
                     "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 7 'T1_third_country,T2_back_country
                All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                                             & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & _
                    IIf(CheckIsZeroOrNothing(txtthird_country.Text) = True, txtthird_country.Text & vbCrLf, "") & _
                                                                IIf(CheckIsZeroOrNothing(txtback_country.Text) = True, txtback_country.Text & vbCrLf, "") & _
                                                                IIf(_IsAgent = False, "", CheckInvAgent(_IsAgent, _Invoice_Agent, _CompanyName_Agent)) & _
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 8 'T1_third_country,T3_place_exibition
                All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                                             & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & _
                     IIf(CheckIsZeroOrNothing(txtthird_country.Text) = True, txtthird_country.Text & vbCrLf, "") & _
                                                                IIf(CheckIsZeroOrNothing(txtplace_exibition.Text) = True, txtplace_exibition.Text & vbCrLf, "") & _
                                                                IIf(_IsAgent = False, "", CheckInvAgent(_IsAgent, _Invoice_Agent, _CompanyName_Agent)) & _
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 9 'T1_third_country
                All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                                             & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & _
                    IIf(CheckIsZeroOrNothing(txtthird_country.Text) = True, txtthird_country.Text & vbCrLf, "") & _
                    IIf(_IsAgent = False, "", CheckInvAgent(_IsAgent, _Invoice_Agent, _CompanyName_Agent)) & _
                                                                    "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
        End Select
    End Sub

    Function tariff_All(ByVal _tariff As String) As String
        Dim str_tariff As String
        str_tariff = Mid(_tariff, 1, 4) & "." & Mid(_tariff, 5, 2) & "." & Mid(_tariff, 7, 4)

        Return str_tariff
    End Function
    Dim Gr_ As String
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
    'by rut D new
    Function CHeck_DisPlay(ByVal CountAllDetail As String, ByVal CheckDetailGross As String) As String
        '�ѹ������ͧ�ҡ ����������Ǩ�ͺ˹��� grossweight �����¡���á��¡������ ���ʹ��¡�����
        Dim TempUnit3 As String = ""
        TempUnit3 = CheckUnitDetail(txtinvh_run_auto.Value)

        Callinvoice_board()
        Dim str_Display As String
        Dim sss As String = txtGross_Weight.Text

        Dim str_gross As String

        'str_gross = txtgross_weight.Text

        '����¹�������� Detail
        '����ͧ invoice ��ҧ�����
        'Dim str_NumInvoice As String = CommonUtility.Get_StringValue(txtNumInvoice.Text)
        Dim str_USDInvoice As String
        '����ͧ ��Ť�� ��ҧ�����
        If CommonUtility.Get_StringValue(txtCheckGrossDetail.Text) = "GWDetail" Then
            'by rut FOBOther
            str_USDInvoice = Format(CommonUtility.Get_Decimal(txtUSDInvoiceDetail.Text), "#,##0.00##") & " " & txtCurrency_Code.Text '& " USD" 'invoice ��ҧ����� �ʴ���Ť�� ��¡�÷ء��¡��
        Else
            str_USDInvoice = Str_USDinvoiceDetail 'invoice ��ҧ����� �ʴ���Ť�� ��¡���á��¡������
        End If

        Select Case checkNull_Show(CommonUtility.Get_StringValue(txtshow_check.Text))
            Case "100000" ' ����ͧ invoice ��ҧ����� �ʴ���Ť��
                Select Case CheckDetailGross
                    Case "GWDetail" '�¡��¡��
                        '���͹����� ��Ѻ�� �ѹ��� 26-10-2012 ����ǡѺ RVC �ͧ�ѹ Case 2,8 ��ͧ�ʴ���Ť�� ������������ʴ�
                        'begin RVC
                        Select Case Mid(CommonUtility.Get_StringValue(txtletter.Value), 1, 50)
                            Case "2", "8" '�ʴ���Ť�� Detail
                                If Mid(CommonUtility.Get_StringValue(txtFOBDisplay.Value), 1, 50) = "Display" Then
                                    If Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeader.Value), 1, 50) = "GROSS WEIGHT - NET WEIGHT" Then
                                        str_gross = GrossTxtDetail() & vbNewLine & NetWeight_(txtnet_weight.Value, txtunit_code2.Value) 'Mid(txtnet_weight.Value, 1, 9) & " " & Mid(txtunit_code2.Value, 1, 10) '& vbNewLine & FobTxt()
                                        Select Case C_TotalRowDe.Text
                                            Case 1
                                                str_Display = str_gross & vbNewLine & str_USDInvoice
                                            Case Else
                                                If Cpage = CountAllDetail Then
                                                    Select Case Check_CaseRVCCount
                                                        Case 9 '�����ҧ��蹻� ����ʴ���Ť�����
                                                            str_Display = str_gross & vbNewLine & str_USDInvoice & vbNewLine & _
                                                                                                               "_______" & vbNewLine & Format(TGross, "#,##0.00##") & " " & TempUnit3 & _
                                                                                                               vbNewLine & Format(TNet, "#,##0.00##") & " " & CommonUtility.Get_StringValue(txtunit_code2.Value)

                                                            Gr_ = Format(TGross, "#,##0.00##") & " " & TempUnit3 & _
                                                            vbNewLine & Format(TNet, "#,##0.00##") & " " & CommonUtility.Get_StringValue(txtunit_code2.Value)
                                                        Case Else '�� RVC ���ҧ���� �ʴ���Ť�����
                                                            str_Display = str_gross & vbNewLine & str_USDInvoice & vbNewLine & _
                                                                                                               "_______" & vbNewLine & Format(TGross, "#,##0.00##") & " " & TempUnit3 & _
                                                                                                               vbNewLine & Format(TNet, "#,##0.00##") & " " & CommonUtility.Get_StringValue(txtunit_code2.Value) & _
                                                                                                               vbNewLine & Format(TUSD, "#,##0.00##") & " " & txtCurrency_Code.Text

                                                            Gr_ = Format(TGross, "#,##0.00##") & " " & TempUnit3 & _
                                                            vbNewLine & Format(TNet, "#,##0.00##") & " " & CommonUtility.Get_StringValue(txtunit_code2.Value) & _
                                                            vbNewLine & Format(TUSD, "#,##0.00##") & " " & txtCurrency_Code.Text
                                                    End Select
                                                Else
                                                    str_Display = str_gross & vbNewLine & str_USDInvoice
                                                End If

                                        End Select
                                    ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeader.Value), 1, 50) = "GROSS WEIGHT" Then
                                        str_gross = GrossTxtDetail() '& vbNewLine & FobTxt()
                                        Select Case C_TotalRowDe.Text
                                            Case 1
                                                str_Display = str_gross & vbNewLine & str_USDInvoice
                                            Case Else
                                                If Cpage = CountAllDetail Then
                                                    Select Case Check_CaseRVCCount
                                                        Case 9 '�����ҧ��蹻� ����ʴ���Ť�����
                                                            str_Display = str_gross & vbNewLine & str_USDInvoice & vbNewLine & _
                                                                         "_______" & vbNewLine & Format(TGross, "#,##0.00##") & " " & TempUnit3
                                                        Case Else '�� RVC ���ҧ���� �ʴ���Ť�����
                                                            str_Display = str_gross & vbNewLine & str_USDInvoice & vbNewLine & _
                                                                          "_______" & vbNewLine & Format(TGross, "#,##0.00##") & " " & TempUnit3 & _
                                                                          vbNewLine & Format(TUSD, "#,##0.00##") & " " & txtCurrency_Code.Text
                                                    End Select
                                                Else
                                                    str_Display = str_gross & vbNewLine & str_USDInvoice
                                                End If
                                        End Select

                                    End If

                                End If
                            Case Else '����ʴ���Ť�� Detail
                                If Mid(CommonUtility.Get_StringValue(txtFOBDisplay.Value), 1, 50) = "Display" Then
                                    If Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeader.Value), 1, 50) = "GROSS WEIGHT - NET WEIGHT" Then
                                        str_gross = GrossTxtDetail() & vbNewLine & NetWeight_(txtnet_weight.Value, txtunit_code2.Value) 'Mid(txtnet_weight.Value, 1, 9) & " " & Mid(txtunit_code2.Value, 1, 10) '& vbNewLine & FobTxt()
                                        Select Case C_TotalRowDe.Text
                                            Case 1
                                                str_Display = str_gross '& vbNewLine & str_USDInvoice
                                            Case Else
                                                If Cpage = CountAllDetail Then
                                                    Select Case Check_CaseRVCCount
                                                        Case 9 '�����ҧ��蹻� ����ʴ���Ť�����
                                                            str_Display = str_gross & vbNewLine & _
                                                                        "_______" & vbNewLine & Format(TGross, "#,##0.00##") & " " & TempUnit3 & _
                                                                        vbNewLine & Format(TNet, "#,##0.00##") & " " & CommonUtility.Get_StringValue(txtunit_code2.Value)

                                                            Gr_ = Format(TGross, "#,##0.00##") & " " & TempUnit3 & _
                                                            vbNewLine & Format(TNet, "#,##0.00##") & " " & CommonUtility.Get_StringValue(txtunit_code2.Value)
                                                        Case Else '�� RVC ���ҧ���� �ʴ���Ť�����
                                                            str_Display = str_gross & vbNewLine & _
                                                                          "_______" & vbNewLine & Format(TGross, "#,##0.00##") & " " & TempUnit3 & _
                                                                          vbNewLine & Format(TNet, "#,##0.00##") & " " & CommonUtility.Get_StringValue(txtunit_code2.Value) & _
                                                                          vbNewLine & Format(TUSD, "#,##0.00##") & " " & txtCurrency_Code.Text

                                                            Gr_ = Format(TGross, "#,##0.00##") & " " & TempUnit3 & _
                                                            vbNewLine & Format(TNet, "#,##0.00##") & " " & CommonUtility.Get_StringValue(txtunit_code2.Value) & _
                                                            vbNewLine & Format(TUSD, "#,##0.00##") & " " & txtCurrency_Code.Text
                                                    End Select
                                                Else
                                                    str_Display = str_gross '& vbNewLine & str_USDInvoice
                                                End If

                                        End Select
                                    ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeader.Value), 1, 50) = "GROSS WEIGHT" Then
                                        str_gross = GrossTxtDetail() '& vbNewLine & FobTxt()
                                        Select Case C_TotalRowDe.Text
                                            Case 1
                                                str_Display = str_gross '& vbNewLine & str_USDInvoice
                                            Case Else
                                                If Cpage = CountAllDetail Then
                                                    Select Case Check_CaseRVCCount
                                                        Case 9 '�����ҧ��蹻� ����ʴ���Ť�����
                                                            str_Display = str_gross & vbNewLine & _
                                                                        "_______" & vbNewLine & Format(TGross, "#,##0.00##") & " " & TempUnit3
                                                        Case Else '�� RVC ���ҧ���� �ʴ���Ť�����
                                                            str_Display = str_gross & vbNewLine & _
                                                                          "_______" & vbNewLine & Format(TGross, "#,##0.00##") & " " & TempUnit3 & _
                                                                          vbNewLine & Format(TUSD, "#,##0.00##") & " " & txtCurrency_Code.Text
                                                    End Select
                                                Else
                                                    str_Display = str_gross '& vbNewLine & str_USDInvoice
                                                End If
                                        End Select
                                    End If
                                End If
                        End Select
                        'end RVC

                    Case Else '����¡��¡�� Gross
                        '���͹����� ��Ѻ�� �ѹ��� 26-10-2012 ����ǡѺ RVC �ͧ�ѹ Case 2,8 ��ͧ�ʴ���Ť�� ������������ʴ�
                        'begin RVC
                        Select Case Mid(CommonUtility.Get_StringValue(txtletter.Value), 1, 50)
                            Case "2", "8" '�ʴ���Ť�� Detail
                                If Mid(CommonUtility.Get_StringValue(txtFOBDisplay.Value), 1, 50) = "Display" Then
                                    If Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeader.Value), 1, 50) = "GROSS WEIGHT - NET WEIGHT" Then
                                        str_gross = GrossTxt() & vbNewLine & NetWeight_(txtnet_weight.Value, txtunit_code2.Value) 'Mid(txtnet_weight.Value, 1, 9) & " " & Mid(txtunit_code2.Value, 1, 10) '& vbNewLine & FobTxt()
                                        Select Case C_TotalRowDe.Text
                                            Case 1
                                                str_Display = str_gross & vbNewLine & str_USDInvoice
                                            Case Else

                                                str_Display = str_gross & vbNewLine & str_USDInvoice
                                        End Select
                                    ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeader.Value), 1, 50) = "GROSS WEIGHT" Then
                                        str_gross = GrossTxt() '& vbNewLine & FobTxt()
                                        Select Case C_TotalRowDe.Text
                                            Case 1
                                                str_Display = str_gross & vbNewLine & str_USDInvoice
                                            Case Else
                                                str_Display = str_gross & vbNewLine & str_USDInvoice
                                        End Select

                                    End If
                                Else '�к���� invoice ��ҧ�������
                                    str_gross = GrossTxt() '& vbNewLine & FobTxt()
                                    Select Case C_TotalRowDe.Text
                                        Case 1
                                            str_Display = str_gross & vbNewLine & Callinvoice_board(1)
                                        Case Else
                                            str_Display = str_gross & vbNewLine & Callinvoice_board(1)
                                    End Select
                                End If
                            Case Else '����ʴ���Ť�� Detail
                                If Mid(CommonUtility.Get_StringValue(txtFOBDisplay.Value), 1, 50) = "Display" Then
                                    If Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeader.Value), 1, 50) = "GROSS WEIGHT - NET WEIGHT" Then
                                        str_gross = GrossTxt() & vbNewLine & NetWeight_(txtnet_weight.Value, txtunit_code2.Value) 'Mid(txtnet_weight.Value, 1, 9) & " " & Mid(txtunit_code2.Value, 1, 10) '& vbNewLine & FobTxt()
                                        Select Case C_TotalRowDe.Text
                                            Case 1
                                                str_Display = str_gross '& vbNewLine & str_USDInvoice
                                            Case Else

                                                str_Display = str_gross '& vbNewLine & str_USDInvoice
                                        End Select
                                    ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeader.Value), 1, 50) = "GROSS WEIGHT" Then
                                        str_gross = GrossTxt() '& vbNewLine & FobTxt()
                                        Select Case C_TotalRowDe.Text
                                            Case 1
                                                str_Display = str_gross '& vbNewLine & str_USDInvoice
                                            Case Else
                                                str_Display = str_gross '& vbNewLine & str_USDInvoice
                                        End Select

                                    End If
                                Else '�к���� invoice ��ҧ�������
                                    str_gross = GrossTxt() '& vbNewLine & FobTxt()
                                    Select Case C_TotalRowDe.Text
                                        Case 1
                                            str_Display = str_gross '& vbNewLine & Callinvoice_board(1)
                                        Case Else
                                            str_Display = str_gross '& vbNewLine & Callinvoice_board(1)
                                    End Select
                                End If
                        End Select
                        'end RVC
                End Select

            Case Else '����� invoice ��ҧ�����
                Select Case txtform_type.Text
                    Case "FORM44_41"
                        Select Case CheckDetailGross
                            Case "GWDetail" '�¡��¡��
                                '���͹����� ��Ѻ�� �ѹ��� 26-10-2012 ����ǡѺ RVC �ͧ�ѹ Case 2,8 ��ͧ�ʴ���Ť�� ������������ʴ�
                                'begin RVC
                                Select Case Mid(CommonUtility.Get_StringValue(txtletter.Value), 1, 50)
                                    Case "2", "8" '�ʴ���Ť�� Detail
                                        'form4 �� WeightDisplayHeader
                                        If Mid(CommonUtility.Get_StringValue(txtFOBDisplay.Value), 1, 50) = "Display" Then

                                            If Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeader.Value), 1, 50) = "GROSS WEIGHT - NET WEIGHT" Then
                                                str_gross = GrossTxtDetail() & vbNewLine & FobTxt() & vbNewLine & NetWeight_(txtnet_weight.Value, txtunit_code2.Value)
                                                Select Case C_TotalRowDe.Text
                                                    Case 1
                                                        str_Display = str_gross
                                                    Case Else
                                                        If Cpage = CountAllDetail Then
                                                            Select Case Check_CaseRVCCount
                                                                Case 9 '�����ҧ��蹻� ����ʴ���Ť�����
                                                                    str_Display = str_gross & vbNewLine & _
                                                                                "_______" & vbNewLine & Format(TGross, "#,##0.00##") & " " & TempUnit3 & _
                                                                                vbNewLine & Format(TNet, "#,##0.00##") & " " & CommonUtility.Get_StringValue(txtunit_code2.Value)
                                                                Case Else '�� RVC ���ҧ���� �ʴ���Ť�����
                                                                    str_Display = str_gross & vbNewLine & _
                                                                                    "_______" & vbNewLine & Format(TGross, "#,##0.00##") & " " & TempUnit3 & _
                                                                                    vbNewLine & FobTxtAll() & _
                                                                                    vbNewLine & Format(TNet, "#,##0.00##") & " " & CommonUtility.Get_StringValue(txtunit_code2.Value)
                                                            End Select
                                                        Else
                                                            str_Display = str_gross
                                                        End If

                                                End Select
                                            ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeader.Value), 1, 50) = "GROSS WEIGHT" Then
                                                str_gross = GrossTxtDetail() & vbNewLine & FobTxt()
                                                Select Case C_TotalRowDe.Text
                                                    Case 1
                                                        str_Display = str_gross
                                                    Case Else
                                                        If Cpage = CountAllDetail Then
                                                            Select Case Check_CaseRVCCount
                                                                Case 9 '�����ҧ��蹻� ����ʴ���Ť�����
                                                                    str_Display = str_gross & vbNewLine & _
                                                                                "_______" & vbNewLine & Format(TGross, "#,##0.00##") & " " & TempUnit3
                                                                Case Else '�� RVC ���ҧ���� �ʴ���Ť�����
                                                                    str_Display = str_gross & vbNewLine & _
                                                                                "_______" & vbNewLine & Format(TGross, "#,##0.00##") & " " & TempUnit3 & _
                                                                                vbNewLine & FobTxtAll()
                                                            End Select
                                                        Else
                                                            str_Display = str_gross
                                                        End If

                                                End Select
                                            End If
                                        End If
                                    Case Else '����ʴ���Ť�� Detail
                                        'form4 �� WeightDisplayHeader
                                        If Mid(CommonUtility.Get_StringValue(txtFOBDisplay.Value), 1, 50) = "Display" Then

                                            If Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeader.Value), 1, 50) = "GROSS WEIGHT - NET WEIGHT" Then
                                                str_gross = GrossTxtDetail() & vbNewLine & NetWeight_(txtnet_weight.Value, txtunit_code2.Value)
                                                Select Case C_TotalRowDe.Text
                                                    Case 1
                                                        str_Display = str_gross
                                                    Case Else
                                                        If Cpage = CountAllDetail Then
                                                            Select Case Check_CaseRVCCount
                                                                Case 9 '�����ҧ��蹻� ����ʴ���Ť�����
                                                                    str_Display = str_gross & vbNewLine & _
                                                                                    "_______" & vbNewLine & Format(TGross, "#,##0.00##") & " " & TempUnit3 & _
                                                                                    vbNewLine & Format(TNet, "#,##0.00##") & " " & CommonUtility.Get_StringValue(txtunit_code2.Value)
                                                                Case Else '�� RVC ���ҧ���� �ʴ���Ť�����
                                                                    str_Display = str_gross & vbNewLine & _
                                                                                    "_______" & vbNewLine & Format(TGross, "#,##0.00##") & " " & TempUnit3 & _
                                                                                    vbNewLine & FobTxtAll() & _
                                                                                    vbNewLine & Format(TNet, "#,##0.00##") & " " & CommonUtility.Get_StringValue(txtunit_code2.Value)
                                                            End Select
                                                        Else
                                                            str_Display = str_gross
                                                        End If

                                                End Select
                                            ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeader.Value), 1, 50) = "GROSS WEIGHT" Then
                                                str_gross = GrossTxtDetail() '& vbNewLine & FobTxt()
                                                Select Case C_TotalRowDe.Text
                                                    Case 1
                                                        str_Display = str_gross
                                                    Case Else
                                                        If Cpage = CountAllDetail Then
                                                            Select Case Check_CaseRVCCount
                                                                Case 9 '�����ҧ��蹻� ����ʴ���Ť�����
                                                                    str_Display = str_gross & vbNewLine & _
                                                                                    "_______" & vbNewLine & Format(TGross, "#,##0.00##") & " " & TempUnit3
                                                                Case Else '�� RVC ���ҧ���� �ʴ���Ť�����
                                                                    str_Display = str_gross & vbNewLine & _
                                                                                    "_______" & vbNewLine & Format(TGross, "#,##0.00##") & " " & TempUnit3 & _
                                                                                    vbNewLine & FobTxtAll()
                                                            End Select
                                                        Else
                                                            str_Display = str_gross
                                                        End If
                                                End Select
                                            End If
                                        End If
                                End Select
                                'end RVC

                            Case Else '����¡��¡�� ��ѧ�ҡ�� ���͹��������Ǩ�����աóչ���Դ���
                                '���͹����� ��Ѻ�� �ѹ��� 26-10-2012 ����ǡѺ RVC �ͧ�ѹ Case 2,8 ��ͧ�ʴ���Ť�� ������������ʴ�
                                'begin RVC
                                Select Case Mid(CommonUtility.Get_StringValue(txtletter.Value), 1, 50)
                                    Case "2", "8" '�ʴ���Ť�� Detail
                                        'form4 �� WeightDisplayHeader
                                        If Mid(CommonUtility.Get_StringValue(txtFOBDisplay.Value), 1, 50) = "Display" Then

                                            If Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeader.Value), 1, 50) = "GROSS WEIGHT - NET WEIGHT" Then
                                                str_gross = GrossTxt() & vbNewLine & FobTxtAllSum() & vbNewLine & NetWeight_(TNet, txtunit_code2.Value)
                                                Select Case C_TotalRowDe.Text
                                                    Case 1
                                                        str_Display = str_gross
                                                    Case Else
                                                        str_Display = str_gross
                                                End Select
                                            ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeader.Value), 1, 50) = "GROSS WEIGHT" Then
                                                str_gross = GrossTxt() & vbNewLine & FobTxtAllSum()
                                                Select Case C_TotalRowDe.Text
                                                    Case 1
                                                        str_Display = str_gross
                                                    Case Else
                                                        str_Display = str_gross
                                                End Select
                                            End If
                                        Else '�к����
                                            str_gross = GrossTxt() & vbNewLine & FobTxtAllSum() 'FobTxtAll()
                                            Select Case C_TotalRowDe.Text
                                                Case 1
                                                    str_Display = str_gross
                                                Case Else
                                                    str_Display = str_gross
                                            End Select
                                        End If
                                    Case Else '����ʴ���Ť�� Detail
                                        'form4 �� WeightDisplayHeader
                                        If Mid(CommonUtility.Get_StringValue(txtFOBDisplay.Value), 1, 50) = "Display" Then

                                            If Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeader.Value), 1, 50) = "GROSS WEIGHT - NET WEIGHT" Then
                                                str_gross = GrossTxt() & vbNewLine & NetWeight_(TNet, txtunit_code2.Value)
                                                Select Case C_TotalRowDe.Text
                                                    Case 1
                                                        str_Display = str_gross
                                                    Case Else
                                                        str_Display = str_gross
                                                End Select
                                            ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeader.Value), 1, 50) = "GROSS WEIGHT" Then
                                                str_gross = GrossTxt() '& vbNewLine & FobTxtAllSum()
                                                Select Case C_TotalRowDe.Text
                                                    Case 1
                                                        str_Display = str_gross
                                                    Case Else
                                                        str_Display = str_gross
                                                End Select
                                            End If
                                        Else '�к����
                                            str_gross = GrossTxt() '& vbNewLine & FobTxtAllSum() 'FobTxtAll()
                                            Select Case C_TotalRowDe.Text
                                                Case 1
                                                    str_Display = str_gross
                                                Case Else
                                                    str_Display = str_gross
                                            End Select
                                        End If
                                End Select
                                'end RVC
                        End Select
                End Select
        End Select

        Return str_Display
    End Function

    'Function CHeck_DisPlay(ByVal CountAllDetail As String, ByVal CheckDetailGross As String) As String

    '    Dim str_Display As String
    '    Dim sss As String = txtGross_Weight.Text

    '    Dim str_gross As String

    '    'str_gross = txtgross_weight.Text

    '    '����¹�������� Detail
    '    '����ͧ invoice ��ҧ�����
    '    'Dim str_NumInvoice As String = CommonUtility.Get_StringValue(txtNumInvoice.Text)
    '    Dim str_USDInvoice As String
    '    '����ͧ ��Ť�� ��ҧ�����
    '    If CommonUtility.Get_StringValue(txtCheckGrossDetail.Text) = "GWDetail" Then
    '        str_USDInvoice = Format(CommonUtility.Get_Decimal(txtUSDInvoiceDetail.Text), "#,##0.00##") & " USD" 'invoice ��ҧ����� �ʴ���Ť�� ��¡�÷ء��¡��
    '    Else
    '        str_USDInvoice = Str_USDinvoiceDetail 'invoice ��ҧ����� �ʴ���Ť�� ��¡���á��¡������
    '    End If

    '    Select Case checkNull_Show(CommonUtility.Get_StringValue(txtshow_check.Text))
    '        Case "100000" ' ����ͧ invoice ��ҧ����� �ʴ���Ť��
    '            Select Case CheckDetailGross
    '                Case "GWDetail" '�¡��¡��
    '                    If Mid(CommonUtility.Get_StringValue(txtFOBDisplay.Value), 1, 50) = "Display" Then
    '                        If Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeader.Value), 1, 50) = "GROSS WEIGHT - NET WEIGHT" Then
    '                            str_gross = GrossTxtDetail() & vbNewLine & NetWeight_(txtnet_weight.Value, txtunit_code2.Value) 'Mid(txtnet_weight.Value, 1, 9) & " " & Mid(txtunit_code2.Value, 1, 10) '& vbNewLine & FobTxt()
    '                            Select Case C_TotalRowDe.Text
    '                                Case 1
    '                                    str_Display = str_gross & vbNewLine & str_USDInvoice
    '                                Case Else
    '                                    If Cpage = CountAllDetail Then
    '                                        str_Display = str_gross & vbNewLine & str_USDInvoice & vbNewLine & _
    '                                        "_______" & vbNewLine & Format(TGross, "#,##0.00##") & " " & CommonUtility.Get_StringValue(txtunit_code3.Value) & _
    '                                        vbNewLine & Format(TNet, "#,##0.00##") & " " & CommonUtility.Get_StringValue(txtunit_code2.Value) & _
    '                                        vbNewLine & Format(TUSD, "#,##0.00##") & " USD"
    '                                    Else
    '                                        str_Display = str_gross & vbNewLine & str_USDInvoice
    '                                    End If

    '                            End Select
    '                        ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeader.Value), 1, 50) = "GROSS WEIGHT" Then
    '                            str_gross = GrossTxtDetail() '& vbNewLine & FobTxt()
    '                            Select Case C_TotalRowDe.Text
    '                                Case 1
    '                                    str_Display = str_gross & vbNewLine & str_USDInvoice
    '                                Case Else
    '                                    If Cpage = CountAllDetail Then
    '                                        str_Display = str_gross & vbNewLine & str_USDInvoice & vbNewLine & _
    '                                        "_______" & vbNewLine & Format(TGross, "#,##0.00##") & " " & CommonUtility.Get_StringValue(txtunit_code3.Value) & _
    '                                        vbNewLine & Format(TUSD, "#,##0.00##") & " USD"

    '                                    Else
    '                                        str_Display = str_gross & vbNewLine & str_USDInvoice
    '                                    End If
    '                            End Select

    '                        End If

    '                    End If
    '                Case Else
    '                    If Mid(CommonUtility.Get_StringValue(txtFOBDisplay.Value), 1, 50) = "Display" Then
    '                        If Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeader.Value), 1, 50) = "GROSS WEIGHT - NET WEIGHT" Then
    '                            str_gross = GrossTxt() & vbNewLine & NetWeight_(txtnet_weight.Value, txtunit_code2.Value) 'Mid(txtnet_weight.Value, 1, 9) & " " & Mid(txtunit_code2.Value, 1, 10) '& vbNewLine & FobTxt()
    '                            Select Case C_TotalRowDe.Text
    '                                Case 1
    '                                    str_Display = str_gross & vbNewLine & str_USDInvoice
    '                                Case Else

    '                                    str_Display = str_gross & vbNewLine & str_USDInvoice
    '                            End Select
    '                        ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeader.Value), 1, 50) = "GROSS WEIGHT" Then
    '                            str_gross = GrossTxt() '& vbNewLine & FobTxt()
    '                            Select Case C_TotalRowDe.Text
    '                                Case 1
    '                                    str_Display = str_gross & vbNewLine & str_USDInvoice
    '                                Case Else
    '                                    str_Display = str_gross & vbNewLine & str_USDInvoice
    '                            End Select

    '                        End If

    '                    End If
    '            End Select

    '        Case Else '����� invoice ��ҧ�����
    '            Select Case txtform_type.Text
    '                Case "FORM4"
    '                    Select Case CheckDetailGross
    '                        Case "GWDetail"
    '                            'form4 �� WeightDisplayHeader
    '                            If Mid(CommonUtility.Get_StringValue(txtFOBDisplay.Value), 1, 50) = "Display" Then

    '                                If Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeader.Value), 1, 50) = "GROSS WEIGHT - NET WEIGHT" Then
    '                                    str_gross = GrossTxtDetail() & vbNewLine & FobTxt() & vbNewLine & NetWeight_(txtnet_weight.Value, txtunit_code2.Value)
    '                                    Select Case C_TotalRowDe.Text
    '                                        Case 1
    '                                            str_Display = str_gross
    '                                        Case Else
    '                                            If Cpage = CountAllDetail Then
    '                                                str_Display = str_gross & vbNewLine & _
    '                                                "_______" & vbNewLine & Format(TGross, "#,##0.00##") & " " & CommonUtility.Get_StringValue(txtunit_code3.Value) & _
    '                                                vbNewLine & FobTxtAll() & _
    '                                                vbNewLine & Format(TNet, "#,##0.00##") & " " & CommonUtility.Get_StringValue(txtunit_code2.Value)
    '                                            Else
    '                                                str_Display = str_gross
    '                                            End If

    '                                    End Select
    '                                ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeader.Value), 1, 50) = "GROSS WEIGHT" Then
    '                                    str_gross = GrossTxtDetail() & vbNewLine & FobTxt()
    '                                    Select Case C_TotalRowDe.Text
    '                                        Case 1
    '                                            str_Display = str_gross
    '                                        Case Else
    '                                            If Cpage = CountAllDetail Then
    '                                                str_Display = str_gross & vbNewLine & _
    '                                                "_______" & vbNewLine & Format(TGross, "#,##0.00##") & " " & CommonUtility.Get_StringValue(txtunit_code3.Value) & _
    '                                                vbNewLine & FobTxtAll()
    '                                            Else
    '                                                str_Display = str_gross
    '                                            End If

    '                                    End Select
    '                                End If
    '                            End If
    '                        Case Else
    '                            'form4 �� WeightDisplayHeader
    '                            If Mid(CommonUtility.Get_StringValue(txtFOBDisplay.Value), 1, 50) = "Display" Then

    '                                If Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeader.Value), 1, 50) = "GROSS WEIGHT - NET WEIGHT" Then
    '                                    str_gross = GrossTxt() & vbNewLine & FobTxtAll() & vbNewLine & NetWeight_(TNet, txtunit_code2.Value)
    '                                    Select Case C_TotalRowDe.Text
    '                                        Case 1
    '                                            str_Display = str_gross
    '                                        Case Else
    '                                            str_Display = str_gross
    '                                    End Select
    '                                ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeader.Value), 1, 50) = "GROSS WEIGHT" Then
    '                                    str_gross = GrossTxt() & vbNewLine & FobTxtAll()
    '                                    Select Case C_TotalRowDe.Text
    '                                        Case 1
    '                                            str_Display = str_gross
    '                                        Case Else
    '                                            str_Display = str_gross
    '                                    End Select
    '                                End If
    '                            End If
    '                    End Select
    '            End Select
    '    End Select

    '    Return str_Display
    'End Function
    '������͹����������
    'Sub CHeck_DisPlay()


    '    Dim sss As String = txtgross_weight.Text

    '    Dim str_gross As String

    '    'str_gross = txtgross_weight.Text

    '    '����ͧ invoice ��ҧ�����
    '    Dim str_NumInvoice As String = CommonUtility.Get_StringValue(txtNumInvoice.Text)
    '    '����ͧ ��Ť�� ��ҧ�����
    '    Dim str_USDInvoice As String = CommonUtility.Get_StringValue(txtUSDInvoice.Text)

    '    Select Case txtshow_check.Text
    '        Case "100000" ' ����ͧ invoice ��ҧ�����
    '            If Mid(CommonUtility.Get_StringValue(txtFOBDisplay.Value), 1, 50) = "Display" Then
    '                If Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeader.Value), 1, 50) = "GROSS WEIGHT - NET WEIGHT" Then
    '                    str_gross = GrossTxt() & vbNewLine & NetWeight_(txtnet_weight.Value, txtunit_code2.Value) 'Mid(txtnet_weight.Value, 1, 9) & " " & Mid(txtunit_code2.Value, 1, 10) '& vbNewLine & FobTxt()
    '                    Select Case C_TotalRowDe.Text
    '                        Case 1
    '                            txtGrossTxt.Text = str_gross & vbNewLine & str_USDInvoice
    '                        Case Else
    '                            If Cpage > 1 Then
    '                                str_gross = ""
    '                                txtGrossTxt.Text = str_gross '& vbNewLine & str_USDInvoice
    '                            ElseIf Cpage = 1 Then

    '                                txtGrossTxt.Text = str_gross & vbNewLine & str_USDInvoice
    '                            End If
    '                    End Select
    '                ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeader.Value), 1, 50) = "GROSS WEIGHT" Then
    '                    str_gross = GrossTxt() '& vbNewLine & FobTxt()
    '                    Select Case C_TotalRowDe.Text
    '                        Case 1
    '                            txtGrossTxt.Text = str_gross & vbNewLine & str_USDInvoice
    '                        Case Else
    '                            If Cpage > 1 Then
    '                                str_gross = ""
    '                                txtGrossTxt.Text = str_gross '& vbNewLine & str_USDInvoice
    '                            ElseIf Cpage = 1 Then

    '                                txtGrossTxt.Text = str_gross & vbNewLine & str_USDInvoice
    '                            End If
    '                    End Select
    '                ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeader.Value), 1, 50) = "" Then
    '                    str_gross = GrossTxt() '& vbNewLine & FobTxt()
    '                    Select Case C_TotalRowDe.Text
    '                        Case 1
    '                            txtGrossTxt.Text = str_gross & vbNewLine & str_USDInvoice
    '                        Case Else
    '                            If Cpage > 1 Then
    '                                str_gross = ""
    '                                txtGrossTxt.Text = str_gross '& vbNewLine & str_USDInvoice
    '                            ElseIf Cpage = 1 Then

    '                                txtGrossTxt.Text = str_gross & vbNewLine & str_USDInvoice
    '                            End If
    '                    End Select
    '                End If
    '            ElseIf Mid(CommonUtility.Get_StringValue(txtFOBDisplay.Value), 1, 50) = "NoDisplay" Then
    '                If Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeader.Value), 1, 50) = "GROSS WEIGHT - NET WEIGHT" Then
    '                    str_gross = GrossTxt() & vbNewLine & NetWeight_(txtnet_weight.Value, txtunit_code2.Value) 'Mid(txtnet_weight.Value, 1, 9) & " " & Mid(txtunit_code2.Value, 1, 10)
    '                    Select Case C_TotalRowDe.Text
    '                        Case 1
    '                            txtGrossTxt.Text = str_gross & vbNewLine & str_USDInvoice
    '                        Case Else
    '                            If Cpage > 1 Then
    '                                str_gross = ""
    '                                txtGrossTxt.Text = str_gross '& vbNewLine & str_USDInvoice
    '                            ElseIf Cpage = 1 Then

    '                                txtGrossTxt.Text = str_gross & vbNewLine & str_USDInvoice
    '                            End If
    '                    End Select
    '                ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeader.Value), 1, 50) = "GROSS WEIGHT" Then
    '                    str_gross = GrossTxt()
    '                    Select Case C_TotalRowDe.Text
    '                        Case 1
    '                            txtGrossTxt.Text = str_gross & vbNewLine & str_USDInvoice
    '                        Case Else
    '                            If Cpage > 1 Then
    '                                str_gross = ""
    '                                txtGrossTxt.Text = str_gross '& vbNewLine & str_USDInvoice
    '                            ElseIf Cpage = 1 Then

    '                                txtGrossTxt.Text = str_gross & vbNewLine & str_USDInvoice
    '                            End If
    '                    End Select
    '                ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeader.Value), 1, 50) = "" Then
    '                    str_gross = GrossTxt()
    '                    Select Case C_TotalRowDe.Text
    '                        Case 1
    '                            txtGrossTxt.Text = str_gross & vbNewLine & str_USDInvoice
    '                        Case Else
    '                            If Cpage > 1 Then
    '                                str_gross = ""
    '                                txtGrossTxt.Text = str_gross '& vbNewLine & str_USDInvoice
    '                            ElseIf Cpage = 1 Then

    '                                txtGrossTxt.Text = str_gross & vbNewLine & str_USDInvoice
    '                            End If
    '                    End Select
    '                End If

    '            ElseIf Mid(CommonUtility.Get_StringValue(txtFOBDisplay.Value), 1, 50) = "" Then

    '                If Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeader.Value), 1, 50) = "GROSS WEIGHT - NET WEIGHT" Then
    '                    str_gross = GrossTxt() & vbNewLine & NetWeight_(txtnet_weight.Value, txtunit_code2.Value) 'Mid(txtnet_weight.Value, 1, 9) & " " & Mid(txtunit_code2.Value, 1, 10) '& vbNewLine & FobTxt()
    '                    Select Case C_TotalRowDe.Text
    '                        Case 1
    '                            txtGrossTxt.Text = str_gross & vbNewLine & str_USDInvoice
    '                        Case Else
    '                            If Cpage > 1 Then
    '                                str_gross = ""
    '                                txtGrossTxt.Text = str_gross '& vbNewLine & str_USDInvoice
    '                            ElseIf Cpage = 1 Then

    '                                txtGrossTxt.Text = str_gross & vbNewLine & str_USDInvoice
    '                            End If
    '                    End Select
    '                ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeader.Value), 1, 50) = "GROSS WEIGHT" Then
    '                    str_gross = GrossTxt() '& vbNewLine & FobTxt()
    '                    Select Case C_TotalRowDe.Text
    '                        Case 1
    '                            txtGrossTxt.Text = str_gross & vbNewLine & str_USDInvoice
    '                        Case Else
    '                            If Cpage > 1 Then
    '                                str_gross = ""
    '                                txtGrossTxt.Text = str_gross '& vbNewLine & str_USDInvoice
    '                            ElseIf Cpage = 1 Then

    '                                txtGrossTxt.Text = str_gross & vbNewLine & str_USDInvoice
    '                            End If
    '                    End Select
    '                ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeader.Value), 1, 50) = "" Then
    '                    str_gross = GrossTxt() '& vbNewLine & FobTxt()
    '                    Select Case C_TotalRowDe.Text
    '                        Case 1
    '                            txtGrossTxt.Text = str_gross & vbNewLine & str_USDInvoice
    '                        Case Else
    '                            If Cpage > 1 Then
    '                                str_gross = ""
    '                                txtGrossTxt.Text = str_gross '& vbNewLine & str_USDInvoice
    '                            ElseIf Cpage = 1 Then

    '                                txtGrossTxt.Text = str_gross & vbNewLine & str_USDInvoice
    '                            End If
    '                    End Select
    '                End If
    '            End If

    '        Case Else '����� invoice ��ҧ�����
    '            If Mid(CommonUtility.Get_StringValue(txtFOBDisplay.Value), 1, 50) = "Display" Then

    '                If Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeader.Value), 1, 50) = "GROSS WEIGHT - NET WEIGHT" Then
    '                    str_gross = GrossTxt() & vbNewLine & NetWeight_(txtnet_weight.Value, txtunit_code2.Value) & vbNewLine & FobTxt()
    '                    Select Case C_TotalRowDe.Text
    '                        Case 1
    '                            txtGrossTxt.Text = str_gross
    '                        Case Else
    '                            If Cpage > 1 Then
    '                                str_gross = ""
    '                                txtGrossTxt.Text = str_gross
    '                            ElseIf Cpage = 1 Then

    '                                txtGrossTxt.Text = str_gross
    '                            End If
    '                    End Select
    '                ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeader.Value), 1, 50) = "GROSS WEIGHT" Then
    '                    str_gross = GrossTxt() & vbNewLine & FobTxt()
    '                    Select Case C_TotalRowDe.Text
    '                        Case 1
    '                            txtGrossTxt.Text = str_gross
    '                        Case Else
    '                            If Cpage > 1 Then
    '                                str_gross = ""
    '                                txtGrossTxt.Text = str_gross
    '                            ElseIf Cpage = 1 Then

    '                                txtGrossTxt.Text = str_gross
    '                            End If
    '                    End Select
    '                ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeader.Value), 1, 50) = "" Then
    '                    str_gross = GrossTxt() & vbNewLine & FobTxt()
    '                    Select Case C_TotalRowDe.Text
    '                        Case 1
    '                            txtGrossTxt.Text = str_gross
    '                        Case Else
    '                            If Cpage > 1 Then
    '                                str_gross = ""
    '                                txtGrossTxt.Text = str_gross
    '                            ElseIf Cpage = 1 Then

    '                                txtGrossTxt.Text = str_gross
    '                            End If
    '                    End Select
    '                End If
    '            ElseIf Mid(CommonUtility.Get_StringValue(txtFOBDisplay.Value), 1, 50) = "NoDisplay" Then
    '                If Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeader.Value), 1, 50) = "NET WEIGHT" Then
    '                    str_gross = NetWeight_(txtnet_weight.Value, txtunit_code2.Value) 'Mid(txtnet_weight.Value, 1, 9) & " " & Mid(txtunit_code2.Value, 1, 10)
    '                    Select Case C_TotalRowDe.Text
    '                        Case 1
    '                            txtGrossTxt.Text = str_gross
    '                        Case Else
    '                            If Cpage > 1 Then
    '                                str_gross = ""
    '                                txtGrossTxt.Text = str_gross
    '                            ElseIf Cpage = 1 Then

    '                                txtGrossTxt.Text = str_gross
    '                            End If
    '                    End Select
    '                ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeader.Value), 1, 50) = "GROSS WEIGHT - NET WEIGHT" Then
    '                    str_gross = GrossTxt() & vbNewLine & NetWeight_(txtnet_weight.Value, txtunit_code2.Value) 'Mid(txtnet_weight.Value, 1, 9) & " " & Mid(txtunit_code2.Value, 1, 10)
    '                    Select Case C_TotalRowDe.Text
    '                        Case 1
    '                            txtGrossTxt.Text = str_gross
    '                        Case Else
    '                            If Cpage > 1 Then
    '                                str_gross = ""
    '                                txtGrossTxt.Text = str_gross
    '                            ElseIf Cpage = 1 Then

    '                                txtGrossTxt.Text = str_gross
    '                            End If
    '                    End Select
    '                ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeader.Value), 1, 50) = "GROSS WEIGHT" Then
    '                    str_gross = GrossTxt()
    '                    Select Case C_TotalRowDe.Text
    '                        Case 1
    '                            txtGrossTxt.Text = str_gross
    '                        Case Else
    '                            If Cpage > 1 Then
    '                                str_gross = ""
    '                                txtGrossTxt.Text = str_gross
    '                            ElseIf Cpage = 1 Then

    '                                txtGrossTxt.Text = str_gross
    '                            End If
    '                    End Select
    '                ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeader.Value), 1, 50) = "" Then
    '                    str_gross = GrossTxt()
    '                    Select Case C_TotalRowDe.Text
    '                        Case 1
    '                            txtGrossTxt.Text = str_gross
    '                        Case Else
    '                            If Cpage > 1 Then
    '                                str_gross = ""
    '                                txtGrossTxt.Text = str_gross
    '                            ElseIf Cpage = 1 Then

    '                                txtGrossTxt.Text = str_gross
    '                            End If
    '                    End Select
    '                End If

    '            ElseIf Mid(CommonUtility.Get_StringValue(txtFOBDisplay.Value), 1, 50) = "" Then

    '                If Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeader.Value), 1, 50) = "NET WEIGHT" Then
    '                    str_gross = NetWeight_(txtnet_weight.Value, txtunit_code2.Value) 'Mid(txtnet_weight.Value, 1, 9) & " " & Mid(txtunit_code2.Value, 1, 10) '& vbNewLine & FobTxt()
    '                    Select Case C_TotalRowDe.Text
    '                        Case 1
    '                            txtGrossTxt.Text = str_gross
    '                        Case Else
    '                            If Cpage > 1 Then
    '                                str_gross = ""
    '                                txtGrossTxt.Text = str_gross
    '                            ElseIf Cpage = 1 Then

    '                                txtGrossTxt.Text = str_gross
    '                            End If
    '                    End Select
    '                ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeader.Value), 1, 50) = "GROSS WEIGHT - NET WEIGHT" Then
    '                    str_gross = GrossTxt() & vbNewLine & NetWeight_(txtnet_weight.Value, txtunit_code2.Value) 'Mid(txtnet_weight.Value, 1, 9) & " " & Mid(txtunit_code2.Value, 1, 10) '& vbNewLine & FobTxt()
    '                    Select Case C_TotalRowDe.Text
    '                        Case 1
    '                            txtGrossTxt.Text = str_gross
    '                        Case Else
    '                            If Cpage > 1 Then
    '                                str_gross = ""
    '                                txtGrossTxt.Text = str_gross
    '                            ElseIf Cpage = 1 Then

    '                                txtGrossTxt.Text = str_gross
    '                            End If
    '                    End Select
    '                ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeader.Value), 1, 50) = "GROSS WEIGHT" Then
    '                    str_gross = GrossTxt() '& vbNewLine & FobTxt()
    '                    Select Case C_TotalRowDe.Text
    '                        Case 1
    '                            txtGrossTxt.Text = str_gross
    '                        Case Else
    '                            If Cpage > 1 Then
    '                                str_gross = ""
    '                                txtGrossTxt.Text = str_gross
    '                            ElseIf Cpage = 1 Then

    '                                txtGrossTxt.Text = str_gross
    '                            End If
    '                    End Select
    '                ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeader.Value), 1, 50) = "" Then
    '                    str_gross = GrossTxt() '& vbNewLine & FobTxt()
    '                    Select Case C_TotalRowDe.Text
    '                        Case 1
    '                            txtGrossTxt.Text = str_gross
    '                        Case Else
    '                            If Cpage > 1 Then
    '                                str_gross = ""
    '                                txtGrossTxt.Text = str_gross
    '                            ElseIf Cpage = 1 Then

    '                                txtGrossTxt.Text = str_gross
    '                            End If
    '                    End Select
    '                End If
    '            End If

    '    End Select

    'End Sub

    Function GrossTxt() As String
        Dim Str_GrossTxt As String
        Dim Dec_Gross As Decimal
        Dec_Gross = CDec(Check_Null(CommonUtility.Get_StringValue(txtgross_weightH.Value))) 'Format(CDec(Check_Null(CommonUtility.Get_StringValue(txtgross_weightH.Value))), "#,##0.00##")
        Str_GrossTxt = Format(Dec_Gross, "#,##0.00##") & " " & CommonUtility.Get_StringValue(txtg_unit_code.Value) 'CStr(Dec_Gross) & " " & CommonUtility.Get_StringValue(txtg_unit_code.Value)

        Return Str_GrossTxt
    End Function
    'by rut Dnew
    Function GrossTxtDetail() As String
        Dim Str_GrossTxt As String
        Dim Dec_Gross As Decimal
        Dec_Gross = CDec(Check_Null(CommonUtility.Get_StringValue(txtgross_weightD.Value))) 'Format(CDec(Check_Null(CommonUtility.Get_StringValue(txtgross_weightH.Value))), "#,##0.00##")
        Str_GrossTxt = Format(Dec_Gross, "#,##0.00##") & " " & CheckUnitDetail(txtinvh_run_auto.Value) 'CommonUtility.Get_StringValue(txtunit_code3.Value) 'CStr(Dec_Gross) & " " & CommonUtility.Get_StringValue(txtg_unit_code.Value)

        Return Str_GrossTxt
    End Function
    'Function GrossTxtDetail() As String
    '    Dim Str_GrossTxt As String
    '    Dim Dec_Gross As Decimal
    '    Dec_Gross = CDec(Check_Null(CommonUtility.Get_StringValue(txtgross_weightD.Value))) 'Format(CDec(Check_Null(CommonUtility.Get_StringValue(txtgross_weightH.Value))), "#,##0.00##")
    '    Str_GrossTxt = Format(Dec_Gross, "#,##0.00##") & " " & CommonUtility.Get_StringValue(txtunit_code3.Value) 'CStr(Dec_Gross) & " " & CommonUtility.Get_StringValue(txtg_unit_code.Value)

    '    Return Str_GrossTxt
    'End Function


    'by rut D new
    'by rut FOB Other
    Function FobTxt() As String
        Dim Str_FobTxt As String
        Dim Dec_Fob As Decimal

        ''ByTine 21-02-2560 ��Ť�� Invoice ���˹��
        If _IsAgent = True Then
            'by rut FOB Other
            Select Case CommonUtility.Get_StringValue(txtCurrency_Code.Text)
                Case Is <> "USD"
                    If Check_Null(CommonUtility.Get_StringValue(txtPriceOtherDetail.Value)) > 0 Then
                        Dec_Fob = CDec(Check_Null(CommonUtility.Get_StringValue(txtPriceOtherDetail.Value))) 'Format(CDec(Check_Null(CommonUtility.Get_StringValue(txtFOB_AMT.Value))), "#,##0.0###")
                        Str_FobTxt = Format(Dec_Fob, "#,##0.00##") & " " & txtCurrency_Code.Text
                    Else
                        Str_FobTxt = Format(0, "#,##0.00##") & " " & txtCurrency_Code.Text '""
                    End If
                Case Else
                    If Check_Null(CommonUtility.Get_StringValue(txtUSDAgent.Value)) > 0 Then
                        Dec_Fob = CDec(Check_Null(CommonUtility.Get_StringValue(txtUSDAgent.Value))) 'Format(CDec(Check_Null(CommonUtility.Get_StringValue(txtFOB_AMT.Value))), "#,##0.0###")
                        Str_FobTxt = Format(Dec_Fob, "#,##0.00##") & " " & txtCurrency_Code.Text '& " USD"
                    Else
                        Str_FobTxt = Format(0, "#,##0.00##") & " " & txtCurrency_Code.Text '""
                    End If
            End Select
        Else
            'by rut FOB Other
            Select Case CommonUtility.Get_StringValue(txtCurrency_Code.Text)
                Case Is <> "USD"
                    If Check_Null(CommonUtility.Get_StringValue(txtPriceOtherDetail.Value)) > 0 Then
                        Dec_Fob = CDec(Check_Null(CommonUtility.Get_StringValue(txtPriceOtherDetail.Value))) 'Format(CDec(Check_Null(CommonUtility.Get_StringValue(txtFOB_AMT.Value))), "#,##0.0###")
                        Str_FobTxt = Format(Dec_Fob, "#,##0.00##") & " " & txtCurrency_Code.Text
                    Else
                        Str_FobTxt = Format(0, "#,##0.00##") & " " & txtCurrency_Code.Text '""
                    End If
                Case Else
                    If Check_Null(CommonUtility.Get_StringValue(txtFOB_AMT.Value)) > 0 Then
                        Dec_Fob = CDec(Check_Null(CommonUtility.Get_StringValue(txtFOB_AMT.Value))) 'Format(CDec(Check_Null(CommonUtility.Get_StringValue(txtFOB_AMT.Value))), "#,##0.0###")
                        Str_FobTxt = Format(Dec_Fob, "#,##0.00##") & " " & txtCurrency_Code.Text '& " USD"
                    Else
                        Str_FobTxt = Format(0, "#,##0.00##") & " " & txtCurrency_Code.Text '""
                    End If
            End Select
        End If
        'If txtFOB_AMT.Value > 0 Then
        '    Dec_Fob = CDec(Check_Null(CommonUtility.Get_StringValue(txtFOB_AMT.Value))) 'Format(CDec(Check_Null(CommonUtility.Get_StringValue(txtFOB_AMT.Value))), "#,##0.0###")
        '    Str_FobTxt = Format(Dec_Fob, "#,##0.00##") & " USD"
        'Else
        '    Str_FobTxt = ""
        'End If

        Return Str_FobTxt
    End Function
    'by rut FOB Other
    Function FobTxtAll() As String
        Dim Str_FobTxt As String
        Dim Dec_Fob As Decimal

        ''ByTine 21-02-2560 ��Ť�� Invoice ���˹��
        If _IsAgent = True Then
            'by rut FOB Other
            Select Case CommonUtility.Get_StringValue(txtCurrency_Code.Text)
                Case Is <> "USD"
                    If Check_Null(CommonUtility.Get_StringValue(txtPriceOtherDetail.Value)) > 0 Then
                        Dec_Fob = CDec(Check_Null(CommonUtility.Get_StringValue(txtPriceOtherDetail.Value))) 'Format(CDec(Check_Null(CommonUtility.Get_StringValue(txtFOB_AMT.Value))), "#,##0.0###")
                        Str_FobTxt = Format(Dec_Fob, "#,##0.00##") & " " & txtCurrency_Code.Text
                    Else
                        Str_FobTxt = Format(0, "#,##0.00##") & " " & txtCurrency_Code.Text '""
                    End If
                Case Else
                    If Check_Null(CommonUtility.Get_StringValue(txtUSDAgent.Value)) > 0 Then
                        Dec_Fob = CDec(Check_Null(CommonUtility.Get_StringValue(_TAgent))) 'Format(CDec(Check_Null(CommonUtility.Get_StringValue(txtFOB_AMT.Value))), "#,##0.0###")
                        Str_FobTxt = Format(Dec_Fob, "#,##0.00##") & " " & txtCurrency_Code.Text '& " USD"
                    Else
                        Str_FobTxt = Format(0, "#,##0.00##") & " " & txtCurrency_Code.Text '""
                    End If
            End Select
        Else
            'by rut FOB Other
            Select Case CommonUtility.Get_StringValue(txtCurrency_Code.Text)
                Case Is <> "USD"
                    If Check_Null(CommonUtility.Get_StringValue(txtPriceOtherDetail.Value)) > 0 Then
                        Dec_Fob = CDec(Check_Null(CommonUtility.Get_StringValue(TUSDOther))) 'Format(CDec(Check_Null(CommonUtility.Get_StringValue(txtFOB_AMT.Value))), "#,##0.0###")
                        Str_FobTxt = Format(Dec_Fob, "#,##0.00##") & " " & txtCurrency_Code.Text
                    Else
                        Str_FobTxt = Format(0, "#,##0.00##") & " " & txtCurrency_Code.Text '""
                    End If
                Case Else
                    If Check_Null(CommonUtility.Get_StringValue(txtFOB_AMT.Value)) > 0 Then
                        Dec_Fob = CDec(Check_Null(CommonUtility.Get_StringValue(TFob))) 'Format(CDec(Check_Null(CommonUtility.Get_StringValue(txtFOB_AMT.Value))), "#,##0.0###")
                        Str_FobTxt = Format(Dec_Fob, "#,##0.00##") & " " & txtCurrency_Code.Text '& " USD"
                    Else
                        Str_FobTxt = Format(0, "#,##0.00##") & " " & txtCurrency_Code.Text '""
                    End If
            End Select
        End If
        'If txtFOB_AMT.Value > 0 Then
        '    Dec_Fob = CDec(Check_Null(CommonUtility.Get_StringValue(TFob))) 'Format(CDec(Check_Null(CommonUtility.Get_StringValue(txtFOB_AMT.Value))), "#,##0.0###")
        '    Str_FobTxt = Format(Dec_Fob, "#,##0.00##") & " USD"
        'Else
        '    Str_FobTxt = ""
        'End If

        Return Str_FobTxt
    End Function
    'by rut FOB Other
    Function FobTxtAllSum() As String
        Dim Str_FobTxt As String
        Dim Dec_Fob As Decimal
        'by rut FOB Other
        Select Case CommonUtility.Get_StringValue(txtCurrency_Code.Text)
            Case Is <> "USD"
                If Check_Null(CommonUtility.Get_StringValue(txtPriceOtherDetail.Value)) > 0 Then
                    Dec_Fob = CDec(Check_Null(CommonUtility.Get_StringValue(TUSDOther))) 'Format(CDec(Check_Null(CommonUtility.Get_StringValue(txtFOB_AMT.Value))), "#,##0.0###")
                    Str_FobTxt = Format(Dec_Fob, "#,##0.00##") & " " & txtCurrency_Code.Text
                Else
                    Str_FobTxt = Format(0, "#,##0.00##") & " " & txtCurrency_Code.Text '""
                End If
            Case Else
                If Check_Null(CommonUtility.Get_StringValue(txtFOB_AMT.Value)) > 0 Then
                    Dec_Fob = CDec(Check_Null(CommonUtility.Get_StringValue(txttotalSum_fob_amt.Value))) 'Format(CDec(Check_Null(CommonUtility.Get_StringValue(txtFOB_AMT.Value))), "#,##0.0###")
                    Str_FobTxt = Format(Dec_Fob, "#,##0.00##") & " " & txtCurrency_Code.Text '& " USD"
                Else
                    Str_FobTxt = Format(0, "#,##0.00##") & " " & txtCurrency_Code.Text '""
                End If
        End Select

        'If txtFOB_AMT.Value > 0 Then
        '    Dec_Fob = CDec(Check_Null(CommonUtility.Get_StringValue(txttotalSum_fob_amt.Value))) 'Format(CDec(Check_Null(CommonUtility.Get_StringValue(txtFOB_AMT.Value))), "#,##0.0###")
        '    Str_FobTxt = Format(Dec_Fob, "#,##0.00##") & " USD"
        'Else
        '    Str_FobTxt = ""
        'End If

        Return Str_FobTxt
    End Function

    Function NetWeight_(ByVal _netweight, ByVal _uni) As String
        Dim sumStr As String
        sumStr = Format(_netweight, "#,##0.00##") & " " & _uni

        Return sumStr
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
                    Case "0"
                        Pic_ch1_third.Visible = True
                        txtTemp_back_country.Text = "THAILAND"
                    Case "1"
                        Pic_ch2_accu.Visible = True
                        txtTemp_back_country.Text = "THAILAND"
                    Case "2"
                        Pic_ch3_back.Visible = True
                        txtTemp_back_country.Text = txtback_country.Value
                        Exit For
                    Case "3"
                        Pic_ch4_par.Visible = True
                        txtTemp_back_country.Text = "THAILAND"
                    Case "4"
                        Pic_ch5_exhibi.Visible = True
                        txtTemp_back_country.Text = "THAILAND"
                    Case "5"
                        Pic_ch6_demin.Visible = True
                        txtTemp_back_country.Text = "THAILAND"
                    Case Else
                        txtTemp_back_country.Text = "THAILAND"
                End Select
            Next
        Else
            txtTemp_back_country.Text = "THAILAND"
        End If


        'Select Case checkNull_Show(CommonUtility.Get_StringValue(txtshow_check.Value))
        '    Case Mid(checkNull_Show(CommonUtility.Get_StringValue(txtshow_check.Value)), 1, 1) = 1 '"100000"
        '        Pic_ch1_third.Visible = True
        '        txtTemp_back_country.Text = "THAILAND"
        '    Case Mid(checkNull_Show(CommonUtility.Get_StringValue(txtshow_check.Value)), 2, 1) = 1 '"010000"
        '        Pic_ch2_accu.Visible = True
        '        txtTemp_back_country.Text = "THAILAND"
        '    Case Mid(checkNull_Show(CommonUtility.Get_StringValue(txtshow_check.Value)), 3, 1) = 1 '"001000"
        '        Pic_ch3_back.Visible = True
        '        txtTemp_back_country.Text = txtback_country.Value
        '    Case Mid(checkNull_Show(CommonUtility.Get_StringValue(txtshow_check.Value)), 4, 1) = 1 '"000100"
        '        Pic_ch4_par.Visible = True
        '        txtTemp_back_country.Text = "THAILAND"
        '    Case Mid(checkNull_Show(CommonUtility.Get_StringValue(txtshow_check.Value)), 5, 1) = 1 '"000010"
        '        Pic_ch5_exhibi.Visible = True
        '        txtTemp_back_country.Text = "THAILAND"
        '    Case Mid(checkNull_Show(CommonUtility.Get_StringValue(txtshow_check.Value)), 6, 1) = 1 '"000001"
        '        Pic_ch6_demin.Visible = True
        '        txtTemp_back_country.Text = "THAILAND"
        '    Case Else
        '        txtTemp_back_country.Text = "THAILAND"
        'End Select


    End Sub
    '�������
    Sub check_show_check_NoUsd()

        Select Case checkNull_Show(CommonUtility.Get_StringValue(txtshow_check.Value))
            Case "100000"
                Pic_ch1_third.Visible = True
                txtTemp_back_country.Text = "THAILAND"
            Case "010000"
                Pic_ch2_accu.Visible = True
                txtTemp_back_country.Text = "THAILAND"
            Case "001000"
                Pic_ch3_back.Visible = True
                txtTemp_back_country.Text = txtback_country.Value
            Case "000100"
                Pic_ch4_par.Visible = True
                txtTemp_back_country.Text = "THAILAND"
            Case "000010"
                Pic_ch5_exhibi.Visible = True
                txtTemp_back_country.Text = "THAILAND"
            Case "000001"
                Pic_ch6_demin.Visible = True
                txtTemp_back_country.Text = "THAILAND"
            Case Else
                txtTemp_back_country.Text = "THAILAND"
        End Select
    End Sub
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

        Select Case CommonUtility.Get_StringValue(txtCheckGrossDetail.Text)
            Case "GWDetail"
                txtGrossTxt.Text = CHeck_DisPlay(CommonUtility.Get_StringValue(C_TotalRowDe.Text), CommonUtility.Get_StringValue(txtCheckGrossDetail.Text))
        End Select
        '============================================================
        'If txtNumRowCount.Text = C_TotalRowDe.Text Then

        If (CommonUtility.Get_StringValue(txtquantity1.Text) <> "" And CommonUtility.Get_Decimal(txtquantity1.Text) <> 0 And CommonUtility.Get_StringValue(txtq_unit_code1.Text) <> "") = True And _
        (CommonUtility.Get_StringValue(txtquantity2.Text) <> "" And CommonUtility.Get_Decimal(txtquantity2.Text) <> 0 And CommonUtility.Get_StringValue(txtq_unit_code2.Text) <> "") = True And _
        (CommonUtility.Get_StringValue(txtquantity3.Text) <> "" And CommonUtility.Get_Decimal(txtquantity3.Text) <> 0 And CommonUtility.Get_StringValue(txtq_unit_code3.Text) <> "") = True And _
        (CommonUtility.Get_StringValue(txtquantity4.Text) <> "" And CommonUtility.Get_Decimal(txtquantity4.Text) <> 0 And CommonUtility.Get_StringValue(txtq_unit_code4.Text) <> "") = True And _
        (CommonUtility.Get_StringValue(txtquantity5.Text) <> "" And CommonUtility.Get_Decimal(txtquantity5.Text) <> 0 And CommonUtility.Get_StringValue(txtq_unit_code5.Text) <> "") = True Then
            '�����
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
        ElseIf (CommonUtility.Get_StringValue(txtquantity1.Text) <> "" And CommonUtility.Get_Decimal(txtquantity1.Text) <> 0 And CommonUtility.Get_StringValue(txtq_unit_code1.Text) <> "") = True And _
        (CommonUtility.Get_StringValue(txtquantity2.Text) <> "" And CommonUtility.Get_Decimal(txtquantity2.Text) <> 0 And CommonUtility.Get_StringValue(txtq_unit_code2.Text) <> "") = True And _
        (CommonUtility.Get_StringValue(txtquantity3.Text) <> "" And CommonUtility.Get_Decimal(txtquantity3.Text) <> 0 And CommonUtility.Get_StringValue(txtq_unit_code3.Text) <> "") = True And _
        (CommonUtility.Get_StringValue(txtquantity4.Text) <> "" And CommonUtility.Get_Decimal(txtquantity4.Text) <> 0 And CommonUtility.Get_StringValue(txtq_unit_code4.Text) <> "") = True And _
        (CommonUtility.Get_StringValue(txtquantity5.Text) <> "" And CommonUtility.Get_Decimal(txtquantity5.Text) <> 0 And CommonUtility.Get_StringValue(txtq_unit_code5.Text) <> "") = False Then
            '�Ҵ 5
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


        ElseIf (CommonUtility.Get_StringValue(txtquantity1.Text) <> "" And CommonUtility.Get_Decimal(txtquantity1.Text) <> 0 And CommonUtility.Get_StringValue(txtq_unit_code1.Text) <> "") = True And _
         (CommonUtility.Get_StringValue(txtquantity2.Text) <> "" And CommonUtility.Get_Decimal(txtquantity2.Text) <> 0 And CommonUtility.Get_StringValue(txtq_unit_code2.Text) <> "") = True And _
         (CommonUtility.Get_StringValue(txtquantity3.Text) <> "" And CommonUtility.Get_Decimal(txtquantity3.Text) <> 0 And CommonUtility.Get_StringValue(txtq_unit_code3.Text) <> "") = True And _
         (CommonUtility.Get_StringValue(txtquantity4.Text) <> "" And CommonUtility.Get_Decimal(txtquantity4.Text) <> 0 And CommonUtility.Get_StringValue(txtq_unit_code4.Text) <> "") = False And _
         (CommonUtility.Get_StringValue(txtquantity5.Text) <> "" And CommonUtility.Get_Decimal(txtquantity5.Text) <> 0 And CommonUtility.Get_StringValue(txtq_unit_code5.Text) <> "") = False Then
            '�Ҵ 4,5
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

        ElseIf (CommonUtility.Get_StringValue(txtquantity1.Text) <> "" And CommonUtility.Get_Decimal(txtquantity1.Text) <> 0 And CommonUtility.Get_StringValue(txtq_unit_code1.Text) <> "") = True And _
          (CommonUtility.Get_StringValue(txtquantity2.Text) <> "" And CommonUtility.Get_Decimal(txtquantity2.Text) <> 0 And CommonUtility.Get_StringValue(txtq_unit_code2.Text) <> "") = True And _
          (CommonUtility.Get_StringValue(txtquantity3.Text) <> "" And CommonUtility.Get_Decimal(txtquantity3.Text) <> 0 And CommonUtility.Get_StringValue(txtq_unit_code3.Text) <> "") = False And _
          (CommonUtility.Get_StringValue(txtquantity4.Text) <> "" And CommonUtility.Get_Decimal(txtquantity4.Text) <> 0 And CommonUtility.Get_StringValue(txtq_unit_code4.Text) <> "") = False And _
          (CommonUtility.Get_StringValue(txtquantity5.Text) <> "" And CommonUtility.Get_Decimal(txtquantity5.Text) <> 0 And CommonUtility.Get_StringValue(txtq_unit_code5.Text) <> "") = False Then
            '�Ҵ 3,4,5
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

        ElseIf (CommonUtility.Get_StringValue(txtquantity1.Text) <> "" And CommonUtility.Get_Decimal(txtquantity1.Text) <> 0 And CommonUtility.Get_StringValue(txtq_unit_code1.Text) <> "") = True And _
          (CommonUtility.Get_StringValue(txtquantity2.Text) <> "" And CommonUtility.Get_Decimal(txtquantity2.Text) <> 0 And CommonUtility.Get_StringValue(txtq_unit_code2.Text) <> "") = False And _
          (CommonUtility.Get_StringValue(txtquantity3.Text) <> "" And CommonUtility.Get_Decimal(txtquantity3.Text) <> 0 And CommonUtility.Get_StringValue(txtq_unit_code3.Text) <> "") = False And _
          (CommonUtility.Get_StringValue(txtquantity4.Text) <> "" And CommonUtility.Get_Decimal(txtquantity4.Text) <> 0 And CommonUtility.Get_StringValue(txtq_unit_code4.Text) <> "") = False And _
          (CommonUtility.Get_StringValue(txtquantity5.Text) <> "" And CommonUtility.Get_Decimal(txtquantity5.Text) <> 0 And CommonUtility.Get_StringValue(txtq_unit_code5.Text) <> "") = False Then
            '�Ҵ 2,3,4,5
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

    'Sub Count_RowToPage_DetailData()
    '    Dim CountData_T, CountData_T2 As Integer
    '    CountData_T = C_TotalRowDe.Text
    '    CountData_T2 = CountData_T / 7

    '    '===============================================================
    '    'If CommonUtility.Get_StringValue(txtgross_weight.Text) <> "" Then
    '    '    txtgross_weight1.Text = txtTemp_Gross_Weight.Text + " " + txtg_Unit_Desc.Text
    '    'Else
    '    '    txtgross_weight1.Text = ""
    '    'End If
    '    '============================================================
    '    CallInvoiceCheck()

    '    Select Case CommonUtility.Get_StringValue(txtCheckGrossDetail.Text)
    '        Case "GWDetail"
    '            txtGrossTxt.Text = CHeck_DisPlay(CommonUtility.Get_StringValue(C_TotalRowDe.Text), CommonUtility.Get_StringValue(txtCheckGrossDetail.Text))
    '    End Select
    '    '============================================================
    '    'If txtNumRowCount.Text = C_TotalRowDe.Text Then

    '    If (CommonUtility.Get_StringValue(txtquantity1.Text) <> "" And CommonUtility.Get_Decimal(txtquantity1.Text) <> 0 And CommonUtility.Get_StringValue(txtq_unit_code1.Text) <> "") = True And _
    '    (CommonUtility.Get_StringValue(txtquantity2.Text) <> "" And CommonUtility.Get_Decimal(txtquantity2.Text) <> 0 And CommonUtility.Get_StringValue(txtq_unit_code2.Text) <> "") = True And _
    '    (CommonUtility.Get_StringValue(txtquantity3.Text) <> "" And CommonUtility.Get_Decimal(txtquantity3.Text) <> 0 And CommonUtility.Get_StringValue(txtq_unit_code3.Text) <> "") = True And _
    '    (CommonUtility.Get_StringValue(txtquantity4.Text) <> "" And CommonUtility.Get_Decimal(txtquantity4.Text) <> 0 And CommonUtility.Get_StringValue(txtq_unit_code4.Text) <> "") = True And _
    '    (CommonUtility.Get_StringValue(txtquantity5.Text) <> "" And CommonUtility.Get_Decimal(txtquantity5.Text) <> 0 And CommonUtility.Get_StringValue(txtq_unit_code5.Text) <> "") = True Then
    '        '�����
    '        All_CheckThird_5(Cpage)
    '        'If CommonUtility.Get_StringValue(txtthird_country.Value) <> "" Then
    '        '    txtT_product.Text = "HS. CODE. " + Mid(CommonUtility.Get_StringValue(txttariff_code.Text), 1, 6) _
    '        '                            + txtproduct_n1.Text + txtproduct_n2.Text + "****" + vbCrLf + _
    '        '                            "TOTAL: " + BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) + vbCrLf + "        " + _
    '        '                            BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) + vbCrLf + "        " + _
    '        '                            BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) + vbCrLf + "        " + _
    '        '                            BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) + vbCrLf + "        " + _
    '        '                            BahtOnly(txtquantity5.Text, txtq_unit_code5.Text) + vbCrLf + txtthird_country.Text + vbCrLf + _
    '        '                            "_____________________________"
    '        'Else
    '        '    txtT_product.Text = "HS. CODE. " + Mid(CommonUtility.Get_StringValue(txttariff_code.Text), 1, 6) _
    '        '                            + txtproduct_n1.Text + txtproduct_n2.Text + "****" + vbCrLf + _
    '        '                            "TOTAL: " + BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) + vbCrLf + "        " + _
    '        '                            BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) + vbCrLf + "        " + _
    '        '                            BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) + vbCrLf + "        " + _
    '        '                            BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) + vbCrLf + "        " + _
    '        '                            BahtOnly(txtquantity5.Text, txtq_unit_code5.Text) + vbCrLf + _
    '        '                            "_____________________________"
    '        'End If
    '    ElseIf (CommonUtility.Get_StringValue(txtquantity1.Text) <> "" And CommonUtility.Get_Decimal(txtquantity1.Text) <> 0 And CommonUtility.Get_StringValue(txtq_unit_code1.Text) <> "") = True And _
    '    (CommonUtility.Get_StringValue(txtquantity2.Text) <> "" And CommonUtility.Get_Decimal(txtquantity2.Text) <> 0 And CommonUtility.Get_StringValue(txtq_unit_code2.Text) <> "") = True And _
    '    (CommonUtility.Get_StringValue(txtquantity3.Text) <> "" And CommonUtility.Get_Decimal(txtquantity3.Text) <> 0 And CommonUtility.Get_StringValue(txtq_unit_code3.Text) <> "") = True And _
    '    (CommonUtility.Get_StringValue(txtquantity4.Text) <> "" And CommonUtility.Get_Decimal(txtquantity4.Text) <> 0 And CommonUtility.Get_StringValue(txtq_unit_code4.Text) <> "") = True And _
    '    (CommonUtility.Get_StringValue(txtquantity5.Text) <> "" And CommonUtility.Get_Decimal(txtquantity5.Text) <> 0 And CommonUtility.Get_StringValue(txtq_unit_code5.Text) <> "") = False Then
    '        '�Ҵ 5
    '        All_CheckThird_4(Cpage)
    '        'If CommonUtility.Get_StringValue(txtthird_country.Value) <> "" Then
    '        '    txtT_product.Text = "HS. CODE. " + Mid(CommonUtility.Get_StringValue(txttariff_code.Text), 1, 6) _
    '        '                            + txtproduct_n1.Text + txtproduct_n2.Text + "****" + vbCrLf + _
    '        '                            "TOTAL: " + BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) + vbCrLf + "        " + _
    '        '                            BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) + vbCrLf + "        " + _
    '        '                            BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) + vbCrLf + "        " + _
    '        '                            BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) + vbCrLf + txtthird_country.Text + vbCrLf + _
    '        '                            "_____________________________"
    '        'Else
    '        '    txtT_product.Text = "HS. CODE. " + Mid(CommonUtility.Get_StringValue(txttariff_code.Text), 1, 6) _
    '        '                            + txtproduct_n1.Text + txtproduct_n2.Text + "****" + vbCrLf + _
    '        '                            "TOTAL: " + BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) + vbCrLf + "        " + _
    '        '                            BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) + vbCrLf + "        " + _
    '        '                            BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) + vbCrLf + "        " + _
    '        '                            BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) + vbCrLf + _
    '        '                            "_____________________________"
    '        'End If


    '    ElseIf (CommonUtility.Get_StringValue(txtquantity1.Text) <> "" And CommonUtility.Get_Decimal(txtquantity1.Text) <> 0 And CommonUtility.Get_StringValue(txtq_unit_code1.Text) <> "") = True And _
    '     (CommonUtility.Get_StringValue(txtquantity2.Text) <> "" And CommonUtility.Get_Decimal(txtquantity2.Text) <> 0 And CommonUtility.Get_StringValue(txtq_unit_code2.Text) <> "") = True And _
    '     (CommonUtility.Get_StringValue(txtquantity3.Text) <> "" And CommonUtility.Get_Decimal(txtquantity3.Text) <> 0 And CommonUtility.Get_StringValue(txtq_unit_code3.Text) <> "") = True And _
    '     (CommonUtility.Get_StringValue(txtquantity4.Text) <> "" And CommonUtility.Get_Decimal(txtquantity4.Text) <> 0 And CommonUtility.Get_StringValue(txtq_unit_code4.Text) <> "") = False And _
    '     (CommonUtility.Get_StringValue(txtquantity5.Text) <> "" And CommonUtility.Get_Decimal(txtquantity5.Text) <> 0 And CommonUtility.Get_StringValue(txtq_unit_code5.Text) <> "") = False Then
    '        '�Ҵ 4,5
    '        All_CheckThird_3(Cpage)
    '        'If CommonUtility.Get_StringValue(txtthird_country.Value) <> "" Then
    '        '    txtT_product.Text = "HS. CODE. " + Mid(CommonUtility.Get_StringValue(txttariff_code.Text), 1, 6) _
    '        '                            + txtproduct_n1.Text + txtproduct_n2.Text + "****" + vbCrLf + _
    '        '                            "TOTAL: " + BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) + vbCrLf + "        " + _
    '        '                            BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) + vbCrLf + "        " + _
    '        '                            BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) + vbCrLf + txtthird_country.Text + vbCrLf + _
    '        '                            "_____________________________"
    '        'Else
    '        '    txtT_product.Text = "HS. CODE. " + Mid(CommonUtility.Get_StringValue(txttariff_code.Text), 1, 6) _
    '        '                            + txtproduct_n1.Text + txtproduct_n2.Text + "****" + vbCrLf + _
    '        '                            "TOTAL: " + BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) + vbCrLf + "        " + _
    '        '                            BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) + vbCrLf + "        " + _
    '        '                            BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) + vbCrLf + _
    '        '                            "_____________________________"
    '        'End If

    '    ElseIf (CommonUtility.Get_StringValue(txtquantity1.Text) <> "" And CommonUtility.Get_Decimal(txtquantity1.Text) <> 0 And CommonUtility.Get_StringValue(txtq_unit_code1.Text) <> "") = True And _
    '      (CommonUtility.Get_StringValue(txtquantity2.Text) <> "" And CommonUtility.Get_Decimal(txtquantity2.Text) <> 0 And CommonUtility.Get_StringValue(txtq_unit_code2.Text) <> "") = True And _
    '      (CommonUtility.Get_StringValue(txtquantity3.Text) <> "" And CommonUtility.Get_Decimal(txtquantity3.Text) <> 0 And CommonUtility.Get_StringValue(txtq_unit_code3.Text) <> "") = False And _
    '      (CommonUtility.Get_StringValue(txtquantity4.Text) <> "" And CommonUtility.Get_Decimal(txtquantity4.Text) <> 0 And CommonUtility.Get_StringValue(txtq_unit_code4.Text) <> "") = False And _
    '      (CommonUtility.Get_StringValue(txtquantity5.Text) <> "" And CommonUtility.Get_Decimal(txtquantity5.Text) <> 0 And CommonUtility.Get_StringValue(txtq_unit_code5.Text) <> "") = False Then
    '        '�Ҵ 3,4,5
    '        All_CheckThird_2(Cpage)
    '        'If CommonUtility.Get_StringValue(txtthird_country.Value) <> "" Then
    '        '    txtT_product.Text = "HS. CODE. " + Mid(CommonUtility.Get_StringValue(txttariff_code.Text), 1, 6) _
    '        '                            + txtproduct_n1.Text + txtproduct_n2.Text + "****" + vbCrLf + _
    '        '                            "TOTAL: " + BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) + vbCrLf + "        " + _
    '        '                            BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) + vbCrLf + txtthird_country.Text + vbCrLf + _
    '        '                            "_____________________________"

    '        'Else
    '        '    txtT_product.Text = "HS. CODE. " + Mid(CommonUtility.Get_StringValue(txttariff_code.Text), 1, 6) _
    '        '                            + txtproduct_n1.Text + txtproduct_n2.Text + "****" + vbCrLf + _
    '        '                            "TOTAL: " + BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) + vbCrLf + "        " + _
    '        '                            BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) + vbCrLf + _
    '        '                            "_____________________________"

    '        'End If

    '    ElseIf (CommonUtility.Get_StringValue(txtquantity1.Text) <> "" And CommonUtility.Get_Decimal(txtquantity1.Text) <> 0 And CommonUtility.Get_StringValue(txtq_unit_code1.Text) <> "") = True And _
    '      (CommonUtility.Get_StringValue(txtquantity2.Text) <> "" And CommonUtility.Get_Decimal(txtquantity2.Text) <> 0 And CommonUtility.Get_StringValue(txtq_unit_code2.Text) <> "") = False And _
    '      (CommonUtility.Get_StringValue(txtquantity3.Text) <> "" And CommonUtility.Get_Decimal(txtquantity3.Text) <> 0 And CommonUtility.Get_StringValue(txtq_unit_code3.Text) <> "") = False And _
    '      (CommonUtility.Get_StringValue(txtquantity4.Text) <> "" And CommonUtility.Get_Decimal(txtquantity4.Text) <> 0 And CommonUtility.Get_StringValue(txtq_unit_code4.Text) <> "") = False And _
    '      (CommonUtility.Get_StringValue(txtquantity5.Text) <> "" And CommonUtility.Get_Decimal(txtquantity5.Text) <> 0 And CommonUtility.Get_StringValue(txtq_unit_code5.Text) <> "") = False Then
    '        '�Ҵ 2,3,4,5
    '        All_CheckThird_1(Cpage)
    '        'If CommonUtility.Get_StringValue(txtthird_country.Value) <> "" Then
    '        '    txtT_product.Text = "HS. CODE. " + Mid(CommonUtility.Get_StringValue(txttariff_code.Text), 1, 6) _
    '        '                            + txtproduct_n1.Text + txtproduct_n2.Text + "****" + vbCrLf + _
    '        '                            "TOTAL: " + BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) + vbCrLf + txtthird_country.Text + vbCrLf + _
    '        '                            "_____________________________"
    '        'Else
    '        '    txtT_product.Text = "HS. CODE. " + Mid(CommonUtility.Get_StringValue(txttariff_code.Text), 1, 6) _
    '        '                            + txtproduct_n1.Text + txtproduct_n2.Text + "****" + vbCrLf + _
    '        '                            "TOTAL: " + BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) + vbCrLf + _
    '        '                            "_____________________________"
    '        'End If

    '    Else
    '        All_CheckThird_0(Cpage)
    '        'If CommonUtility.Get_StringValue(txtthird_country.Value) <> "" Then
    '        '    txtT_product.Text = "HS. CODE. " + Mid(CommonUtility.Get_StringValue(txttariff_code.Text), 1, 6) _
    '        '                                                    + txtproduct_n1.Text + txtproduct_n2.Text + "****" + vbCrLf + txtthird_country.Text + vbCrLf + _
    '        '                            "_____________________________"
    '        'Else
    '        '    txtT_product.Text = "HS. CODE. " + Mid(CommonUtility.Get_StringValue(txttariff_code.Text), 1, 6) _
    '        '                                                    + txtproduct_n1.Text + txtproduct_n2.Text + "****" + vbCrLf + _
    '        '                            "_____________________________"
    '        'End If

    '    End If

    '    'Else
    '    '    txtT_product.Text = "HS. CODE. " + Mid(CommonUtility.Get_StringValue(txttariff_code.Text), 1, 6) + " " _
    '    '    + txtproduct_n1.Text + txtproduct_n2.Text + "****"
    '    'End If

    '    ''============================================================
    '    'If CommonUtility.Get_StringValue(txtNumRowCount.Text) = "1" Then
    '    '    txtmarks.Visible = True
    '    '    'txtgross_weight1.Visible = True
    '    '    txtTolInvoice.Visible = True
    '    'Else
    '    '    txtmarks.Visible = False
    '    '    'txtgross_weight1.Visible = False
    '    '    txtTolInvoice.Visible = False
    '    'End If
    'End Sub
    Dim numCheck As Integer = 1
    Dim str_mark As String

    'by rut Title New Begin---------------
    Dim Str_TitleDe As String
    Dim Str_TitleDe2 As String
    'by rut Title New End-----------------
    Private Sub Detail1_Format(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Detail1.Format
        Cpage += 1
        '====================================
        TGross += Check_Null(txtgross_weightD.Value)
        TNet += txtnet_weight.Value
        TFob += txtFOB_AMT.Value
        TUSD += Check_Null(txtUSDInvoiceDetail.Value)
        _TAgent += Check_Null(txtUSDAgent.Value)

        'by rut FOB Other
        TUSDOther += Check_Null(txtPriceOtherDetail.Value)
        '====================================
        'by rut FOBOther
        If Cpage = 1 Then 'invoice ��ҧ����� �ʴ���Ť�� ��¡���á��¡������
            Str_USDinvoiceDetail = Format(CommonUtility.Get_Decimal(txtUSDInvoiceDetail.Text), "#,##0.00##") & " " & txtCurrency_Code.Text
        End If
        'If Cpage = 1 Then 'invoice ��ҧ����� �ʴ���Ť�� ��¡���á��¡������
        '    Str_USDinvoiceDetail = Format(CommonUtility.Get_Decimal(txtUSDInvoiceDetail.Text), "#,##0.00##") & " USD"
        'End If
        Count_RowToPage_DetailData()

        txtNumRowCount.Text = Cpage
        Check_box8()

        If Cpage = 1 Then
            str_mark = CommonUtility.Get_StringValue(txtmarks.Text)
        End If

        'byrut 14/06/2011 invoice100
        Select Case Mid(txtshow_check.Text, 1, 1) 'txtshow_check.Text
            Case "1", "100000"
                txtvoince.Text = check_invoice_more(CommonUtility.Get_StringValue(""), CommonUtility.Get_StringValue(txtproduct_model.Text))
            Case Else
                txtvoince.Text = check_invoice_more(CommonUtility.Get_StringValue(txt_NewEmail_ch02.Text), CommonUtility.Get_StringValue(txtproduct_model.Text))
        End Select
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

        'by rut rvc
        Check_CaseRVCCount = CInt(txtCheck_CaseRVCCount.Text)


        'by rut Title New End--------------------------------------------------

        'Select Case C_TotalRowDe.Text
        '    Case 1
        '        txtTemp_marks.Text = str_mark
        '    Case Else
        '        If Cpage > 1 Then
        '            str_mark = ""
        '            txtTemp_marks.Text = str_mark
        '        ElseIf Cpage = 1 Then

        '            txtTemp_marks.Text = str_mark
        '        End If
        'End Select
    End Sub
    'by rut 100 invoice
    Function check_invoice_more(ByVal str_checkCase As String, ByVal value_invoiceMore As String) As String
        Dim send_invoice As String = ""

        Select Case str_checkCase
            Case "1"
                txtvoince.Visible = True
                send_invoice = value_invoiceMore
            Case Else
                txtvoince.Visible = False
                send_invoice = ""
        End Select
        Return send_invoice
    End Function

    Private Sub rpt3_ediFORM4_pr_PageEnd(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PageEnd
        'Event PageEnd �зӷء˹�� ��е���ѡ�è������ҹ���ش (�Ѻ��ͤ�������) �ó� PageStart ��������ҧ�ش
        '������ҧ�� ��������ʹ DrawText (�ա�� Overload ���������Ẻ)
        head_height = PageHeader1.Height

        Dim f As New System.Drawing.Font("BrowalliaUPC", 11)

        'Ẻ�кص��˹� (Default ˹����繹���)
        'Me.CurrentPage.DrawText("Confidential", 4.0F, 10.0F, 2.0F, 0.188F)

        'With Me.CurrentPage
        '    .Font = f
        '    .ForeColor = System.Drawing.Color.YellowGreen
        '    .TextAlignment = DataDynamics.ActiveReports.TextAlignment.Center
        '    .TextAngle = 900
        '    .VerticalTextAlignment = DataDynamics.ActiveReports.VerticalTextAlignment.Middle
        '    .DrawText("Confidential", 1, 1, 6.5, 9)

        'End With


        'Ẻ�к�����ŧ� Box �ͧ��Ҵ����˹�
        'Dim f As New System.Drawing.Font("Times New Roman", 36)
        'Dim x As Single = 1.0F
        Dim x As Single = 6.99
        Dim y As Single = CSng(head_height) '4.63 '4.606
        'Dim width As Single = 6.0F
        Dim width As Single = 0.94
        Dim height As Single = 3.1
        Dim drawRect As New Drawing.RectangleF(x, y, width, height)

        'byrut 14/06/2011
        Select Case Mid(txtshow_check.Text, 1, 1) 'txtshow_check.Text
            Case "1", "100000"
                With Me.CurrentPage
                    .Font = f
                    .ForeColor = System.Drawing.Color.Blue
                    .TextAlignment = DataDynamics.ActiveReports.TextAlignment.Center
                    .VerticalTextAlignment = VerticalTextAlignment.Top
                    '.BackColor = Drawing.Color.Green
                    '.TextAngle = 900
                    '.VerticalTextAlignment = DataDynamics.ActiveReports.VerticalTextAlignment.Middle
                    .DrawText(CallInvoice_, drawRect)
                End With
            Case Else
                If CommonUtility.Get_StringValue(txt_NewEmail_ch02.Text) <> "1" Then '��ͻ�������� invoice100
                    With Me.CurrentPage
                        .Font = f
                        .ForeColor = System.Drawing.Color.Blue
                        .TextAlignment = DataDynamics.ActiveReports.TextAlignment.Center
                        .VerticalTextAlignment = VerticalTextAlignment.Top
                        '.BackColor = Drawing.Color.Green
                        '.TextAngle = 900
                        '.VerticalTextAlignment = DataDynamics.ActiveReports.VerticalTextAlignment.Middle
                        .DrawText(CallInvoice_, drawRect)
                    End With
                End If
        End Select


        '==========================
        Dim xm As Single = 0.89
        Dim ym As Single = CSng(head_height) '4.63
        'Dim width As Single = 6.0F
        Dim widthm As Single = 0.94
        Dim heightm As Single = 3.1
        Dim drawRectm As New Drawing.RectangleF(xm, ym, widthm, heightm)

        With Me.CurrentPage
            .Font = f
            .ForeColor = System.Drawing.Color.Black
            .TextAlignment = DataDynamics.ActiveReports.TextAlignment.Left
            .VerticalTextAlignment = VerticalTextAlignment.Top
            '.BackColor = Drawing.Color.Green
            '.TextAngle = 900
            '.VerticalTextAlignment = DataDynamics.ActiveReports.VerticalTextAlignment.Middle
            .DrawText(CommonUtility.Get_StringValue(str_mark), drawRectm)
        End With
        '==========================
        Dim f9_font As New System.Drawing.Font("BrowalliaUPC", 12)
        Select Case CommonUtility.Get_StringValue(txtCheckGrossDetail.Text)
            Case Is <> "GWDetail"
                Dim x_GrossDetail As Single = 6
                Dim y_GrossDetail As Single = CSng(head_height) '4.63
                'Dim width As Single = 6.0F
                Dim width_GrossDetail As Single = 0.94
                Dim height_GrossDetail As Single = 3.1
                Dim drawRect_GrossDetail As New Drawing.RectangleF(x_GrossDetail, y_GrossDetail, width_GrossDetail, height_GrossDetail)

                With Me.CurrentPage
                    .Font = f9_font
                    .ForeColor = System.Drawing.Color.Black
                    .TextAlignment = DataDynamics.ActiveReports.TextAlignment.Center
                    .VerticalTextAlignment = VerticalTextAlignment.Top
                    '.BackColor = Drawing.Color.Green
                    '.TextAngle = 900
                    '.VerticalTextAlignment = DataDynamics.ActiveReports.VerticalTextAlignment.Middle
                    .DrawText(CHeck_DisPlay(CommonUtility.Get_StringValue(C_TotalRowDe.Text), CommonUtility.Get_StringValue(txtCheckGrossDetail.Text)), drawRect_GrossDetail)

                End With
        End Select
    End Sub

    Private Sub rpt3_ediFORM4_pr_ReportEnd(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.ReportEnd
        If updatePrintTotal(txtinvh_run_auto.Value, CStr(Me.Document.Pages.Count)) = True Then

        End If
    End Sub

    Private Sub rpt3_ediFORM4_pr_ReportStart(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.ReportStart
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
