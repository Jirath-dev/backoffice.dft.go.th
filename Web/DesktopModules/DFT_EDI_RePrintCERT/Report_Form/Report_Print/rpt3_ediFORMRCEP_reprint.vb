Imports DataDynamics.ActiveReports
Imports DataDynamics.ActiveReports.Document

Imports Microsoft.ApplicationBlocks.Data
Imports System.Data.SqlClient
Imports System.Data
Imports System.Text
Imports DFT.Dotnetnuke.ClassLibrary
Imports ReportPrintClass
Public Class rpt3_ediFORMrcep_reprint
    Dim strEDIConn As String = ConfigurationManager.ConnectionStrings("OriginConnection").ConnectionString
    Dim TGross, TNet, TFob, TUSD, TUSDOther As Decimal
    Public _TempB2B As String
    Public _IsAgent As Boolean
    Public _InvAgentType, _CompanyName_Agent, _Invoice_Agent As String
    Dim _TAgent As Decimal = 0.0

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
                checkIssued_date(DataUtil.ConvertToString(txtinvh_run_auto.Value), DataUtil.ConvertToString("0"))
            Case CInt(str_2)
                Pic_ch7_Issued.Visible = False
                checkIssued_date(DataUtil.ConvertToString(txtinvh_run_auto.Value), DataUtil.ConvertToString("0"))
            Case CInt(str_3)
                Pic_ch7_Issued.Visible = False
                checkIssued_date(DataUtil.ConvertToString(txtinvh_run_auto.Value), DataUtil.ConvertToString("0"))
            Case Else
                Pic_ch7_Issued.Visible = True
                checkIssued_date(DataUtil.ConvertToString(txtinvh_run_auto.Value), DataUtil.ConvertToString("1"))
        End Select
    End Sub
    Private Sub PageFooter1_Format(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PageFooter1.Format
        'Check_show_check(Len(DataUtil.ConvertToString(txtshow_check.Text)))
        check_show_check()

        ''ByTine 7-5-2561 ��Ѻ�������ͧ�ҡ������ط����ҡ���֧�ѹ�������óտ��������ա�þ��������
        Dim strcommand As String = " SELECT reference_code2, isnull(printFormDate,getdate()) printFormDate FROM form_header_edi WHERE (invh_run_auto = @invh_run_auto)"
        Dim ds As DataSet = SqlHelper.ExecuteDataset(strEDIConn, CommandType.Text, strcommand, New SqlParameter("@invh_run_auto", txtinvh_run_auto.Text))
        If ds.Tables(0).Rows.Count > 0 Then
            If DBNull.Value.Equals(ds.Tables(0).Rows(0).Item("reference_code2")) = False Then
                txtcompany_provincefoot.Text = txtcompany_provincefoot1.Text & " " & Format(CDate(ds.Tables(0).Rows(0).Item("printFormDate")), "dd/MM/yyy")
            Else
                txtcompany_provincefoot.Text = txtcompany_provincefoot1.Text & " " & format_dateSelect()
            End If

            'issued
            Select Case CheckIssuedDate_Forms_AHK(CDate(txtdeparture_date.Value), CDate(IIf(IsDBNull(CDate(ds.Tables(0).Rows(0).Item("printFormDate"))) = True, Now.Date, CDate(ds.Tables(0).Rows(0).Item("printFormDate")))))
                Case True
                    Pic_ch7_Issued.Visible = True
                Case False
                    Pic_ch7_Issued.Visible = False
            End Select

        Else
            txtcompany_provincefoot.Text = txtcompany_provincefoot1.Text & " " & format_dateSelect()
        End If

        '//txtcompany_provincefoot.Text = WorkingDaysElapsed(Date.Now, Date.Now.AddDays(5))

        'by rut sign image ����� ���������м���Ѻ�ͺ
        'check ��¡�á�͹����ա���� Seal Sign ������� �������ա�����
        Dim dsESS As DataSet = search_imageForm(txtinvh_run_auto.Text)
        If dsESS.Tables(0).Rows.Count > 0 Then
            CaseCheck_numimagesSign(dsESS)
            CaseCheck_ApproveSign(dsESS)
        End If

        load_qrcode(txtEncodeRef2.Text)

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

        If DataUtil.ConvertToString(txtSINGLE_COUNTRY_CONTENT.Value) = "1" And DataUtil.ConvertToString(txtTitleMain.Text) <> "" Then
            txtTitleHead.Visible = True
        End If

        'by rut Title New End-------------------------------------------
    End Sub

    'company
    Sub Head_Checkcompany_v1()
        If Mid(txtdest_remark.Text, 1, 3) = "C/O" Then
            txtCompany_Check_1.Text = txtob_address.Text & IIf(Check_NULLALL(txtNewEmail_ch01.Text) <> "", " E-mail: " & txtNewEmail_ch01.Text, "") & " CARE OF " & txtcompany_name.Text & " " & txtcompany_address.Text _
                               & " " & txtcompany_province.Text & " " & txtcompany_country.Text &
                               IIf(DataUtil.ConvertToString(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") &
                               IIf(DataUtil.ConvertToString(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") &
                               IIf(DataUtil.ConvertToString(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID:  " & txtcompany_taxno.Text, "") &
                               IIf(DataUtil.ConvertToString(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")
        ElseIf Mid(txtdest_remark.Text, 1, 3) = "O/B" Then
            txtCompany_Check_1.Text = txtcompany_name.Text & " " & txtcompany_address.Text & " " & txtcompany_province.Text _
                                & " " & txtcompany_country.Text & IIf(DataUtil.ConvertToString(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") &
                                IIf(DataUtil.ConvertToString(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") _
                                & IIf(DataUtil.ConvertToString(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID: " & txtcompany_taxno.Text, "") &
                                IIf(DataUtil.ConvertToString(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "") &
                                IIf(DataUtil.ConvertToString(Check_NULLALL(txtob_address.Text)) <> "", " ON BEHALF OF " & txtob_address.Text, "") & IIf(Check_NULLALL(txtNewEmail_ch01.Text) <> "", " E-mail: " & txtNewEmail_ch01.Text, "")
        ElseIf Mid(txtdest_remark.Text, 2, 1) <> "/" Then
            txtCompany_Check_1.Text = txtcompany_name.Text & " " & txtcompany_address.Text & " " & txtcompany_province.Text _
                                & " " & txtcompany_country.Text & IIf(DataUtil.ConvertToString(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") &
                                IIf(DataUtil.ConvertToString(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") _
                                & IIf(DataUtil.ConvertToString(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID: " & txtcompany_taxno.Text, "") &
                                IIf(DataUtil.ConvertToString(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")

        ElseIf Mid(txtdest_remark.Text, 1, 5) <> "O/TOB" Then
            txtCompany_Check_1.Text = txtcompany_name.Text & " " & txtcompany_address.Text & " " & txtcompany_province.Text _
                            & " " & txtcompany_country.Text & " " & txtob_address.Text &
                            IIf(DataUtil.ConvertToString(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") &
                            IIf(DataUtil.ConvertToString(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") _
                            & IIf(DataUtil.ConvertToString(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID: " & txtcompany_taxno.Text, "") &
                            IIf(DataUtil.ConvertToString(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")

        ElseIf Mid(txtdest_remark.Text, 1, 5) <> "O/TCO" Then
            txtCompany_Check_1.Text = txtob_address.Text & txtcompany_name.Text & " " & txtcompany_address.Text & " " & txtcompany_province.Text _
                            & " " & txtcompany_country.Text &
                            IIf(DataUtil.ConvertToString(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") &
                            IIf(DataUtil.ConvertToString(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") _
                            & IIf(DataUtil.ConvertToString(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID: " & txtcompany_taxno.Text, "") &
                            IIf(DataUtil.ConvertToString(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")

        End If
    End Sub
    Sub Head_Checkdestination_v2()
        If Mid(txtdest_remark1.Text, 1, 3) = "C/O" Then
            txtdestination_Check2.Text = txtob_dest_address.Text & " CARE OF " & txtdestination_company.Text & " " & txtdestination_address.Text _
                                & " " & txtdestination_province.Text & " " & txtdest_Receive_country.Text &
                                IIf(DataUtil.ConvertToString(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                IIf(DataUtil.ConvertToString(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") &
                                IIf(DataUtil.ConvertToString(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID:  " & txtdestination_taxid.Text, "") &
                                IIf(DataUtil.ConvertToString(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                IIf(Check_NULLALL(txtplace_Exibition.Text) <> "", " " & txtplace_Exibition.Text, "")

        ElseIf Mid(txtdest_remark1.Text, 1, 3) = "O/B" Then
            txtdestination_Check2.Text = txtdestination_company.Text & " " & txtdestination_address.Text & " " &
                                    txtdestination_province.Text & " " & txtdest_Receive_country.Text &
                                    IIf(DataUtil.ConvertToString(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                    IIf(DataUtil.ConvertToString(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                    & IIf(DataUtil.ConvertToString(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                    IIf(DataUtil.ConvertToString(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                    " ON BEHALF OF " & txtob_dest_address.Text & IIf(Check_NULLALL(txtplace_Exibition.Text) <> "", " " & txtplace_Exibition.Text, "")

        ElseIf Mid(txtdest_remark1.Text, 2, 1) <> "/" Then
            txtdestination_Check2.Text = txtdestination_company.Text & " " & txtdestination_address.Text & " " & txtdestination_province.Text _
                                & " " & txtdest_Receive_country.Text &
                                IIf(DataUtil.ConvertToString(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                IIf(DataUtil.ConvertToString(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                & IIf(DataUtil.ConvertToString(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                IIf(DataUtil.ConvertToString(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                IIf(Check_NULLALL(txtplace_Exibition.Text) <> "", " " & txtplace_Exibition.Text, "")

        ElseIf Mid(txtdest_remark1.Text, 1, 5) <> "O/TOB" Then
            txtdestination_Check2.Text = txtdestination_company.Text & " " & txtdestination_address.Text & " " & txtdestination_province.Text _
                            & " " & txtdest_Receive_country.Text & " " & txtob_dest_address.Text &
                            IIf(DataUtil.ConvertToString(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                            IIf(DataUtil.ConvertToString(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                            & IIf(DataUtil.ConvertToString(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                            IIf(DataUtil.ConvertToString(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                            IIf(Check_NULLALL(txtplace_Exibition.Text) <> "", " " & txtplace_Exibition.Text, "")

        ElseIf Mid(txtdest_remark1.Text, 1, 5) <> "O/TCO" Then
            txtdestination_Check2.Text = txtob_dest_address.Text & " " & txtdestination_company.Text & " " & txtdestination_address.Text & " " & txtdestination_province.Text _
                            & " " & txtdest_Receive_country.Text &
                            IIf(DataUtil.ConvertToString(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                            IIf(DataUtil.ConvertToString(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                            & IIf(DataUtil.ConvertToString(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                            IIf(DataUtil.ConvertToString(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                            IIf(Check_NULLALL(txtplace_Exibition.Text) <> "", " " & txtplace_Exibition.Text, "")

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
                    Select Case DataUtil.ConvertToString(Check_NULLALL(txtNumInvoice.Text))
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

    Function Check_Third(ByVal T1_third_country As String, ByVal T2_back_country As String, ByVal T3_place_Exibition As String) As Integer
        If T1_third_country <> "" And T2_back_country <> "" And T3_place_Exibition <> "" Then
            Return 1 '�����

        ElseIf T1_third_country = "" And T2_back_country <> "" And T3_place_Exibition <> "" Then
            Return 2 'T2_back_country,T3_place_Exibition

        ElseIf T1_third_country = "" And T2_back_country = "" And T3_place_Exibition <> "" Then
            Return 3 'T3_place_Exibition

        ElseIf T1_third_country = "" And T2_back_country = "" And T3_place_Exibition = "" Then
            Return 4 '��������

        ElseIf T1_third_country = "" And T2_back_country <> "" And T3_place_Exibition = "" Then
            Return 5 'T2_back_country

        ElseIf T1_third_country = "" And T2_back_country = "" And T3_place_Exibition <> "" Then
            Return 6 'T3_place_Exibition

        ElseIf T1_third_country <> "" And T2_back_country <> "" And T3_place_Exibition = "" Then
            Return 7 'T1_third_country,T2_back_country

        ElseIf T1_third_country <> "" And T2_back_country = "" And T3_place_Exibition <> "" Then
            Return 8 'T1_third_country,T3_place_Exibition

        ElseIf T1_third_country <> "" And T2_back_country = "" And T3_place_Exibition = "" Then
            Return 9 'T1_third_country

        End If
    End Function

    ''check ������� ����ͤ�������͹ HS.Code ����价�� Module Class Module_CallBathEng
    'Function CarTxt(ByVal _SINGLE_COUNTRY_CONTENT As String, ByVal _InvoiceDetailTH As String) As String
    '    Dim str_txt As String = ""

    '    '_SINGLE_COUNTRY_CONTENT --1=����ѷ Car,0=����� Car
    '    Select Case _SINGLE_COUNTRY_CONTENT
    '        Case "1"
    '            Select Case _InvoiceDetailTH.Trim
    '                Case Is = ""
    '                    str_txt = ""
    '                Case Else
    '                    str_txt = _InvoiceDetailTH & vbCrLf
    '            End Select

    '        Case Else
    '            str_txt = ""
    '    End Select
    '    Return str_txt
    'End Function

    Sub All_CheckThird_5(ByVal count_data As Integer)
        Dim All_str As String
        Select Case Check_Third(DataUtil.ConvertToString(txtthird_country.Value),
                                DataUtil.ConvertToString(txtback_country.Value),
                                DataUtil.ConvertToString(txtplace_Exibition.Value))
            Case 1
                All_str = "HS. CODE. " & tariff_All(DataUtil.ConvertToString(txttariff_code.Text)) & vbCrLf _
                                              & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & Form_NewStr_AddressDetail(DataUtil.ConvertToString(txtByCom_CH03.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " &
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " &
                                                                BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " &
                                                                BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf & "        " &
                                                                BahtOnly(txtquantity5.Text, txtq_unit_code5.Text) & vbCrLf &
                                                                String_Total_USDOnly_New_ForAgent(txtDisplayUDS7.Text, txtCurrency_Code.Text, TFob, DataUtil.ConvertToString(txtshow_check.Text), TUSD, TUSDOther, _IsAgent, _TAgent, IIf(_IsAgent = True, txtInvAgentType.Text, "")) & "        " &
                                                                IIf(CheckIsZeroOrNothing(txtthird_country.Text) = True, txtthird_country.Text & vbCrLf, "") &
                                                                IIf(CheckIsZeroOrNothing(txtback_country.Text) = True, txtback_country.Text & vbCrLf, "") &
                                                                IIf(CheckIsZeroOrNothing(txtplace_Exibition.Text) = True, txtplace_Exibition.Text & vbCrLf, "") &
                                                                IIf(_IsAgent = False, "", CheckInvAgent(_IsAgent, _Invoice_Agent, _CompanyName_Agent)) &
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 2 'T2_back_country,T3_place_Exibition
                All_str = "HS. CODE. " & tariff_All(DataUtil.ConvertToString(txttariff_code.Text)) & vbCrLf _
                                             & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & Form_NewStr_AddressDetail(DataUtil.ConvertToString(txtByCom_CH03.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " &
                                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " &
                                                                                BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " &
                                                                                BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf & "        " &
                                                                                BahtOnly(txtquantity5.Text, txtq_unit_code5.Text) & vbCrLf &
                                                                                String_Total_USDOnly_New_ForAgent(txtDisplayUDS7.Text, txtCurrency_Code.Text, TFob, DataUtil.ConvertToString(txtshow_check.Text), TUSD, TUSDOther, _IsAgent, _TAgent, IIf(_IsAgent = True, txtInvAgentType.Text, "")) & "        " &
                                                                                IIf(CheckIsZeroOrNothing(txtback_country.Text) = True, txtback_country.Text & vbCrLf, "") &
                                                                                IIf(CheckIsZeroOrNothing(txtplace_Exibition.Text) = True, txtplace_Exibition.Text & vbCrLf, "") &
                                                                                IIf(_IsAgent = False, "", CheckInvAgent(_IsAgent, _Invoice_Agent, _CompanyName_Agent)) &
                                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 3 'T3_place_Exibition
                All_str = "HS. CODE. " & tariff_All(DataUtil.ConvertToString(txttariff_code.Text)) & vbCrLf _
                                             & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & Form_NewStr_AddressDetail(DataUtil.ConvertToString(txtByCom_CH03.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " &
                                                                    BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " &
                                                                    BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " &
                                                                    BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf & "        " &
                                                                    BahtOnly(txtquantity5.Text, txtq_unit_code5.Text) & vbCrLf &
                                                                    String_Total_USDOnly_New_ForAgent(txtDisplayUDS7.Text, txtCurrency_Code.Text, TFob, DataUtil.ConvertToString(txtshow_check.Text), TUSD, TUSDOther, _IsAgent, _TAgent, IIf(_IsAgent = True, txtInvAgentType.Text, "")) & "        " &
                                                                    IIf(_IsAgent = False, "", CheckInvAgent(_IsAgent, _Invoice_Agent, _CompanyName_Agent)) &
                                                                    "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 4 '��������
                All_str = "HS. CODE. " & tariff_All(DataUtil.ConvertToString(txttariff_code.Text)) & vbCrLf _
                                             & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & Form_NewStr_AddressDetail(DataUtil.ConvertToString(txtByCom_CH03.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " &
                                                                    BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " &
                                                                    BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " &
                                                                    BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf & "        " &
                                                                    BahtOnly(txtquantity5.Text, txtq_unit_code5.Text) & vbCrLf &
                                                                    String_Total_USDOnly_New_ForAgent(txtDisplayUDS7.Text, txtCurrency_Code.Text, TFob, DataUtil.ConvertToString(txtshow_check.Text), TUSD, TUSDOther, _IsAgent, _TAgent, IIf(_IsAgent = True, txtInvAgentType.Text, "")) & "        " &
                                                                    IIf(_IsAgent = False, "", CheckInvAgent(_IsAgent, _Invoice_Agent, _CompanyName_Agent)) &
                                                                    "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 5 'T2_back_country
                All_str = "HS. CODE. " & tariff_All(DataUtil.ConvertToString(txttariff_code.Text)) & vbCrLf _
                                             & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & Form_NewStr_AddressDetail(DataUtil.ConvertToString(txtByCom_CH03.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " &
                                                                    BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " &
                                                                    BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " &
                                                                    BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf & "        " &
                                                                    BahtOnly(txtquantity5.Text, txtq_unit_code5.Text) & vbCrLf &
                                                                    String_Total_USDOnly_New_ForAgent(txtDisplayUDS7.Text, txtCurrency_Code.Text, TFob, DataUtil.ConvertToString(txtshow_check.Text), TUSD, TUSDOther, _IsAgent, _TAgent, IIf(_IsAgent = True, txtInvAgentType.Text, "")) &
                                                                    "        " & _TempB2B & vbNewLine &
                                                                    IIf(_IsAgent = False, "", CheckInvAgent(_IsAgent, _Invoice_Agent, _CompanyName_Agent)) &
                                                                    "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 6 'T3_place_Exibition
                All_str = "HS. CODE. " & tariff_All(DataUtil.ConvertToString(txttariff_code.Text)) & vbCrLf _
                                             & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & Form_NewStr_AddressDetail(DataUtil.ConvertToString(txtByCom_CH03.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " &
                                                                    BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " &
                                                                    BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " &
                                                                    BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf & "        " &
                                                                    BahtOnly(txtquantity5.Text, txtq_unit_code5.Text) & vbCrLf &
                                                                    String_Total_USDOnly_New_ForAgent(txtDisplayUDS7.Text, txtCurrency_Code.Text, TFob, DataUtil.ConvertToString(txtshow_check.Text), TUSD, TUSDOther, _IsAgent, _TAgent, IIf(_IsAgent = True, txtInvAgentType.Text, "")) & "        " &
                                                                    IIf(_IsAgent = False, "", CheckInvAgent(_IsAgent, _Invoice_Agent, _CompanyName_Agent)) &
                                                                    "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 7 'T1_third_country,T2_back_country
                All_str = "HS. CODE. " & tariff_All(DataUtil.ConvertToString(txttariff_code.Text)) & vbCrLf _
                                             & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & Form_NewStr_AddressDetail(DataUtil.ConvertToString(txtByCom_CH03.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " &
                                                                    BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " &
                                                                    BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " &
                                                                    BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf & "        " &
                                                                    BahtOnly(txtquantity5.Text, txtq_unit_code5.Text) & vbCrLf &
                                                                    String_Total_USDOnly_New_ForAgent(txtDisplayUDS7.Text, txtCurrency_Code.Text, TFob, DataUtil.ConvertToString(txtshow_check.Text), TUSD, TUSDOther, _IsAgent, _TAgent, IIf(_IsAgent = True, txtInvAgentType.Text, "")) & "        " &
                                                                    IIf(CheckIsZeroOrNothing(txtthird_country.Text) = True, txtthird_country.Text & vbCrLf, "") &
                                                                    IIf(CheckIsZeroOrNothing(txtback_country.Text) = True, txtback_country.Text & vbCrLf, "") &
                                                                    IIf(_IsAgent = False, "", CheckInvAgent(_IsAgent, _Invoice_Agent, _CompanyName_Agent)) &
                                                                    "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 8 'T1_third_country,T3_place_Exibition
                All_str = "HS. CODE. " & tariff_All(DataUtil.ConvertToString(txttariff_code.Text)) & vbCrLf _
                                             & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & Form_NewStr_AddressDetail(DataUtil.ConvertToString(txtByCom_CH03.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " &
                                                                    BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " &
                                                                    BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " &
                                                                    BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf & "        " &
                                                                    BahtOnly(txtquantity5.Text, txtq_unit_code5.Text) & vbCrLf &
                                                                    String_Total_USDOnly_New_ForAgent(txtDisplayUDS7.Text, txtCurrency_Code.Text, TFob, DataUtil.ConvertToString(txtshow_check.Text), TUSD, TUSDOther, _IsAgent, _TAgent, IIf(_IsAgent = True, txtInvAgentType.Text, "")) & "        " &
                                                                    IIf(CheckIsZeroOrNothing(txtthird_country.Text) = True, txtthird_country.Text & vbCrLf, "") &
                                                                   IIf(CheckIsZeroOrNothing(txtplace_Exibition.Text) = True, txtplace_Exibition.Text & vbCrLf, "") &
                                                                    IIf(_IsAgent = False, "", CheckInvAgent(_IsAgent, _Invoice_Agent, _CompanyName_Agent)) &
                                                                    "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 9 'T1_third_country
                All_str = "HS. CODE. " & tariff_All(DataUtil.ConvertToString(txttariff_code.Text)) & vbCrLf _
                                             & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & Form_NewStr_AddressDetail(DataUtil.ConvertToString(txtByCom_CH03.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " &
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " &
                                                                BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " &
                                                                BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf & "        " &
                                                                BahtOnly(txtquantity5.Text, txtq_unit_code5.Text) & vbCrLf &
                                                                String_Total_USDOnly_New_ForAgent(txtDisplayUDS7.Text, txtCurrency_Code.Text, TFob, DataUtil.ConvertToString(txtshow_check.Text), TUSD, TUSDOther, _IsAgent, _TAgent, IIf(_IsAgent = True, txtInvAgentType.Text, "")) & "        " &
                                                                IIf(CheckIsZeroOrNothing(txtthird_country.Text) = True, txtthird_country.Text & vbCrLf, "") &
                                                                IIf(_IsAgent = False, "", CheckInvAgent(_IsAgent, _Invoice_Agent, _CompanyName_Agent)) &
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
        End Select
    End Sub
    Sub All_CheckThird_4(ByVal count_data As Integer)
        Dim All_str As String

        Select Case Check_Third(DataUtil.ConvertToString(txtthird_country.Value),
                                DataUtil.ConvertToString(txtback_country.Value),
                                DataUtil.ConvertToString(txtplace_Exibition.Value))
            Case 1
                All_str = "HS. CODE. " & tariff_All(DataUtil.ConvertToString(txttariff_code.Text)) & vbCrLf _
                                             & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & Form_NewStr_AddressDetail(DataUtil.ConvertToString(txtByCom_CH03.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " &
                                            BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " &
                                            BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " &
                                            BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf &
                                           String_Total_USDOnly_New_ForAgent(txtDisplayUDS7.Text, txtCurrency_Code.Text, TFob, DataUtil.ConvertToString(txtshow_check.Text), TUSD, TUSDOther, _IsAgent, _TAgent, IIf(_IsAgent = True, txtInvAgentType.Text, "")) & "        " &
                                            IIf(CheckIsZeroOrNothing(txtthird_country.Text) = True, txtthird_country.Text & vbCrLf, "") &
                                            IIf(CheckIsZeroOrNothing(txtback_country.Text) = True, txtback_country.Text & vbCrLf, "") &
                                           IIf(CheckIsZeroOrNothing(txtplace_Exibition.Text) = True, txtplace_Exibition.Text & vbCrLf, "") &
                                            IIf(_IsAgent = False, "", CheckInvAgent(_IsAgent, _Invoice_Agent, _CompanyName_Agent)) &
                                            "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 2 'T2_back_country,T3_place_Exibition
                All_str = "HS. CODE. " & tariff_All(DataUtil.ConvertToString(txttariff_code.Text)) & vbCrLf _
                                             & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & Form_NewStr_AddressDetail(DataUtil.ConvertToString(txtByCom_CH03.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " &
                                                            BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " &
                                                            BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " &
                                                            BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf &
                                                            String_Total_USDOnly_New_ForAgent(txtDisplayUDS7.Text, txtCurrency_Code.Text, TFob, DataUtil.ConvertToString(txtshow_check.Text), TUSD, TUSDOther, _IsAgent, _TAgent, IIf(_IsAgent = True, txtInvAgentType.Text, "")) & "        " &
                                                            IIf(CheckIsZeroOrNothing(txtback_country.Text) = True, txtback_country.Text & vbCrLf, "") &
                                                           IIf(CheckIsZeroOrNothing(txtplace_Exibition.Text) = True, txtplace_Exibition.Text & vbCrLf, "") &
                                                            IIf(_IsAgent = False, "", CheckInvAgent(_IsAgent, _Invoice_Agent, _CompanyName_Agent)) &
                                                            "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 3 'T3_place_Exibition
                All_str = "HS. CODE. " & tariff_All(DataUtil.ConvertToString(txttariff_code.Text)) & vbCrLf _
                                             & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & Form_NewStr_AddressDetail(DataUtil.ConvertToString(txtByCom_CH03.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " &
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " &
                                                                BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " &
                                                                BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf &
                                                                String_Total_USDOnly_New_ForAgent(txtDisplayUDS7.Text, txtCurrency_Code.Text, TFob, DataUtil.ConvertToString(txtshow_check.Text), TUSD, TUSDOther, _IsAgent, _TAgent, IIf(_IsAgent = True, txtInvAgentType.Text, "")) & "        " &
                                                                IIf(_IsAgent = False, "", CheckInvAgent(_IsAgent, _Invoice_Agent, _CompanyName_Agent)) &
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 4 '��������
                All_str = "HS. CODE. " & tariff_All(DataUtil.ConvertToString(txttariff_code.Text)) & vbCrLf _
                                             & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & Form_NewStr_AddressDetail(DataUtil.ConvertToString(txtByCom_CH03.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " &
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " &
                                                                BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " &
                                                                BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf &
                                                                String_Total_USDOnly_New_ForAgent(txtDisplayUDS7.Text, txtCurrency_Code.Text, TFob, DataUtil.ConvertToString(txtshow_check.Text), TUSD, TUSDOther, _IsAgent, _TAgent, IIf(_IsAgent = True, txtInvAgentType.Text, "")) & "        " &
                                                                IIf(_IsAgent = False, "", CheckInvAgent(_IsAgent, _Invoice_Agent, _CompanyName_Agent)) &
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 5 'T2_back_country
                All_str = "HS. CODE. " & tariff_All(DataUtil.ConvertToString(txttariff_code.Text)) & vbCrLf _
                                             & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & Form_NewStr_AddressDetail(DataUtil.ConvertToString(txtByCom_CH03.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " &
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " &
                                                                BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " &
                                                                BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf &
                                                                String_Total_USDOnly_New_ForAgent(txtDisplayUDS7.Text, txtCurrency_Code.Text, TFob, DataUtil.ConvertToString(txtshow_check.Text), TUSD, TUSDOther, _IsAgent, _TAgent, IIf(_IsAgent = True, txtInvAgentType.Text, "")) & "        " &
                                                                _TempB2B & vbNewLine &
                                                                IIf(_IsAgent = False, "", CheckInvAgent(_IsAgent, _Invoice_Agent, _CompanyName_Agent)) &
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 6 'T3_place_Exibition
                All_str = "HS. CODE. " & tariff_All(DataUtil.ConvertToString(txttariff_code.Text)) & vbCrLf _
                                             & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & Form_NewStr_AddressDetail(DataUtil.ConvertToString(txtByCom_CH03.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " &
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " &
                                                                BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " &
                                                                BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf &
                                                                String_Total_USDOnly_New_ForAgent(txtDisplayUDS7.Text, txtCurrency_Code.Text, TFob, DataUtil.ConvertToString(txtshow_check.Text), TUSD, TUSDOther, _IsAgent, _TAgent, IIf(_IsAgent = True, txtInvAgentType.Text, "")) & "        " &
                                                                IIf(_IsAgent = False, "", CheckInvAgent(_IsAgent, _Invoice_Agent, _CompanyName_Agent)) &
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 7 'T1_third_country,T2_back_country
                All_str = "HS. CODE. " & tariff_All(DataUtil.ConvertToString(txttariff_code.Text)) & vbCrLf _
                                             & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & Form_NewStr_AddressDetail(DataUtil.ConvertToString(txtByCom_CH03.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " &
                                                                    BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " &
                                                                    BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " &
                                                                    BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf &
                                                                    String_Total_USDOnly_New_ForAgent(txtDisplayUDS7.Text, txtCurrency_Code.Text, TFob, DataUtil.ConvertToString(txtshow_check.Text), TUSD, TUSDOther, _IsAgent, _TAgent, IIf(_IsAgent = True, txtInvAgentType.Text, "")) & "        " &
                                                                    IIf(CheckIsZeroOrNothing(txtthird_country.Text) = True, txtthird_country.Text & vbCrLf, "") &
                                                                    IIf(CheckIsZeroOrNothing(txtback_country.Text) = True, txtback_country.Text & vbCrLf, "") &
                                                                    IIf(_IsAgent = False, "", CheckInvAgent(_IsAgent, _Invoice_Agent, _CompanyName_Agent)) &
                                                                    "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 8 'T1_third_country,T3_place_Exibition
                All_str = "HS. CODE. " & tariff_All(DataUtil.ConvertToString(txttariff_code.Text)) & vbCrLf _
                                             & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & Form_NewStr_AddressDetail(DataUtil.ConvertToString(txtByCom_CH03.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " &
                                                                    BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " &
                                                                    BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " &
                                                                    BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf &
                                                                   String_Total_USDOnly_New_ForAgent(txtDisplayUDS7.Text, txtCurrency_Code.Text, TFob, DataUtil.ConvertToString(txtshow_check.Text), TUSD, TUSDOther, _IsAgent, _TAgent, IIf(_IsAgent = True, txtInvAgentType.Text, "")) & "        " &
                                                                    IIf(CheckIsZeroOrNothing(txtthird_country.Text) = True, txtthird_country.Text & vbCrLf, "") &
                                                                   IIf(CheckIsZeroOrNothing(txtplace_Exibition.Text) = True, txtplace_Exibition.Text & vbCrLf, "") &
                                                                    IIf(_IsAgent = False, "", CheckInvAgent(_IsAgent, _Invoice_Agent, _CompanyName_Agent)) &
                                                                    "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 9 'T1_third_country
                All_str = "HS. CODE. " & tariff_All(DataUtil.ConvertToString(txttariff_code.Text)) & vbCrLf _
                                             & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & Form_NewStr_AddressDetail(DataUtil.ConvertToString(txtByCom_CH03.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " &
                                                                    BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " &
                                                                    BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " &
                                                                    BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf &
                                                                    String_Total_USDOnly_New_ForAgent(txtDisplayUDS7.Text, txtCurrency_Code.Text, TFob, DataUtil.ConvertToString(txtshow_check.Text), TUSD, TUSDOther, _IsAgent, _TAgent, IIf(_IsAgent = True, txtInvAgentType.Text, "")) & "        " &
                                                                    IIf(CheckIsZeroOrNothing(txtthird_country.Text) = True, txtthird_country.Text & vbCrLf, "") &
                                                                    IIf(_IsAgent = False, "", CheckInvAgent(_IsAgent, _Invoice_Agent, _CompanyName_Agent)) &
                                                                    "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
        End Select
    End Sub
    Sub All_CheckThird_3(ByVal count_data As Integer)
        Dim All_str As String
        Select Case Check_Third(DataUtil.ConvertToString(txtthird_country.Value),
                                DataUtil.ConvertToString(txtback_country.Value),
                                DataUtil.ConvertToString(txtplace_Exibition.Value))
            Case 1
                All_str = "HS. CODE. " & tariff_All(DataUtil.ConvertToString(txttariff_code.Text)) & vbCrLf _
                                             & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & Form_NewStr_AddressDetail(DataUtil.ConvertToString(txtByCom_CH03.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " &
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " &
                                                                BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf &
                                                                String_Total_USDOnly_New_ForAgent(txtDisplayUDS7.Text, txtCurrency_Code.Text, TFob, DataUtil.ConvertToString(txtshow_check.Text), TUSD, TUSDOther, _IsAgent, _TAgent, IIf(_IsAgent = True, txtInvAgentType.Text, "")) & "        " &
                                                                IIf(CheckIsZeroOrNothing(txtthird_country.Text) = True, txtthird_country.Text & vbCrLf, "") &
                                                                IIf(CheckIsZeroOrNothing(txtback_country.Text) = True, txtback_country.Text & vbCrLf, "") &
                                                               IIf(CheckIsZeroOrNothing(txtplace_Exibition.Text) = True, txtplace_Exibition.Text & vbCrLf, "") &
                                                                IIf(_IsAgent = False, "", CheckInvAgent(_IsAgent, _Invoice_Agent, _CompanyName_Agent)) &
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 2 'T2_back_country,T3_place_Exibition
                All_str = "HS. CODE. " & tariff_All(DataUtil.ConvertToString(txttariff_code.Text)) & vbCrLf _
                                             & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & Form_NewStr_AddressDetail(DataUtil.ConvertToString(txtByCom_CH03.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " &
                                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " &
                                                                                BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf &
                                                                                String_Total_USDOnly_New_ForAgent(txtDisplayUDS7.Text, txtCurrency_Code.Text, TFob, DataUtil.ConvertToString(txtshow_check.Text), TUSD, TUSDOther, _IsAgent, _TAgent, IIf(_IsAgent = True, txtInvAgentType.Text, "")) & "        " &
                                                                                IIf(CheckIsZeroOrNothing(txtback_country.Text) = True, txtback_country.Text & vbCrLf, "") &
                                                                               IIf(CheckIsZeroOrNothing(txtplace_Exibition.Text) = True, txtplace_Exibition.Text & vbCrLf, "") &
                                                                                IIf(_IsAgent = False, "", CheckInvAgent(_IsAgent, _Invoice_Agent, _CompanyName_Agent)) &
                                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 3 'T3_place_Exibition
                All_str = "HS. CODE. " & tariff_All(DataUtil.ConvertToString(txttariff_code.Text)) & vbCrLf _
                                             & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & Form_NewStr_AddressDetail(DataUtil.ConvertToString(txtByCom_CH03.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " &
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " &
                                                                BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & String_Total_USDOnly_New_ForAgent(txtDisplayUDS7.Text, txtCurrency_Code.Text, TFob, DataUtil.ConvertToString(txtshow_check.Text), TUSD, TUSDOther, _IsAgent, _TAgent, IIf(_IsAgent = True, txtInvAgentType.Text, "")) & "        " &
                                                                IIf(_IsAgent = False, "", CheckInvAgent(_IsAgent, _Invoice_Agent, _CompanyName_Agent)) &
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 4 '��������
                All_str = "HS. CODE. " & tariff_All(DataUtil.ConvertToString(txttariff_code.Text)) & vbCrLf _
                                             & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & Form_NewStr_AddressDetail(DataUtil.ConvertToString(txtByCom_CH03.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " &
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " &
                                                                BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & String_Total_USDOnly_New_ForAgent(txtDisplayUDS7.Text, txtCurrency_Code.Text, TFob, DataUtil.ConvertToString(txtshow_check.Text), TUSD, TUSDOther, _IsAgent, _TAgent, IIf(_IsAgent = True, txtInvAgentType.Text, "")) & "        " &
                                                                IIf(_IsAgent = False, "", CheckInvAgent(_IsAgent, _Invoice_Agent, _CompanyName_Agent)) &
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 5 'T2_back_country
                All_str = "HS. CODE. " & tariff_All(DataUtil.ConvertToString(txttariff_code.Text)) & vbCrLf _
                                             & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & Form_NewStr_AddressDetail(DataUtil.ConvertToString(txtByCom_CH03.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " &
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " &
                                                                BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & String_Total_USDOnly_New_ForAgent(txtDisplayUDS7.Text, txtCurrency_Code.Text, TFob, DataUtil.ConvertToString(txtshow_check.Text), TUSD, TUSDOther, _IsAgent, _TAgent, IIf(_IsAgent = True, txtInvAgentType.Text, "")) & "        " &
                                                                _TempB2B & vbNewLine &
                                                                IIf(_IsAgent = False, "", CheckInvAgent(_IsAgent, _Invoice_Agent, _CompanyName_Agent)) &
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 6 'T3_place_Exibition
                All_str = "HS. CODE. " & tariff_All(DataUtil.ConvertToString(txttariff_code.Text)) & vbCrLf _
                                             & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & Form_NewStr_AddressDetail(DataUtil.ConvertToString(txtByCom_CH03.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " &
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " &
                                                                BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & String_Total_USDOnly_New_ForAgent(txtDisplayUDS7.Text, txtCurrency_Code.Text, TFob, DataUtil.ConvertToString(txtshow_check.Text), TUSD, TUSDOther, _IsAgent, _TAgent, IIf(_IsAgent = True, txtInvAgentType.Text, "")) & "        " &
                                                                IIf(_IsAgent = False, "", CheckInvAgent(_IsAgent, _Invoice_Agent, _CompanyName_Agent)) &
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 7 'T1_third_country,T2_back_country
                All_str = "HS. CODE. " & tariff_All(DataUtil.ConvertToString(txttariff_code.Text)) & vbCrLf _
                                              & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & Form_NewStr_AddressDetail(DataUtil.ConvertToString(txtByCom_CH03.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " &
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " &
                                                                BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf &
                                                                String_Total_USDOnly_New_ForAgent(txtDisplayUDS7.Text, txtCurrency_Code.Text, TFob, DataUtil.ConvertToString(txtshow_check.Text), TUSD, TUSDOther, _IsAgent, _TAgent, IIf(_IsAgent = True, txtInvAgentType.Text, "")) & "        " &
                                                                IIf(CheckIsZeroOrNothing(txtthird_country.Text) = True, txtthird_country.Text & vbCrLf, "") &
                                                                IIf(CheckIsZeroOrNothing(txtback_country.Text) = True, txtback_country.Text & vbCrLf, "") &
                                                                IIf(_IsAgent = False, "", CheckInvAgent(_IsAgent, _Invoice_Agent, _CompanyName_Agent)) &
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 8 'T1_third_country,T3_place_Exibition
                All_str = "HS. CODE. " & tariff_All(DataUtil.ConvertToString(txttariff_code.Text)) & vbCrLf _
                                              & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & Form_NewStr_AddressDetail(DataUtil.ConvertToString(txtByCom_CH03.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " &
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " &
                                                                BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf &
                                                                String_Total_USDOnly_New_ForAgent(txtDisplayUDS7.Text, txtCurrency_Code.Text, TFob, DataUtil.ConvertToString(txtshow_check.Text), TUSD, TUSDOther, _IsAgent, _TAgent, IIf(_IsAgent = True, txtInvAgentType.Text, "")) & "        " &
                                                                IIf(CheckIsZeroOrNothing(txtthird_country.Text) = True, txtthird_country.Text & vbCrLf, "") &
                                                                IIf(CheckIsZeroOrNothing(txtplace_Exibition.Text) = True, txtplace_Exibition.Text & vbCrLf, "") &
                                                                IIf(_IsAgent = False, "", CheckInvAgent(_IsAgent, _Invoice_Agent, _CompanyName_Agent)) &
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 9 'T1_third_country
                All_str = "HS. CODE. " & tariff_All(DataUtil.ConvertToString(txttariff_code.Text)) & vbCrLf _
                                             & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & Form_NewStr_AddressDetail(DataUtil.ConvertToString(txtByCom_CH03.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " &
                                                                    BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " &
                                                                    BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf &
                                                                    String_Total_USDOnly_New_ForAgent(txtDisplayUDS7.Text, txtCurrency_Code.Text, TFob, DataUtil.ConvertToString(txtshow_check.Text), TUSD, TUSDOther, _IsAgent, _TAgent, IIf(_IsAgent = True, txtInvAgentType.Text, "")) & "        " &
                                                                    IIf(CheckIsZeroOrNothing(txtthird_country.Text) = True, txtthird_country.Text & vbCrLf, "") &
                                                                    IIf(_IsAgent = False, "", CheckInvAgent(_IsAgent, _Invoice_Agent, _CompanyName_Agent)) &
                                                                    "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
        End Select
    End Sub
    Sub All_CheckThird_2(ByVal count_data As Integer)
        Dim All_str As String
        Select Case Check_Third(DataUtil.ConvertToString(txtthird_country.Value),
                                DataUtil.ConvertToString(txtback_country.Value),
                                DataUtil.ConvertToString(txtplace_Exibition.Value))
            Case 1
                All_str = "HS. CODE. " & tariff_All(DataUtil.ConvertToString(txttariff_code.Text)) & vbCrLf _
                                             & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & Form_NewStr_AddressDetail(DataUtil.ConvertToString(txtByCom_CH03.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " &
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf &
                                                                String_Total_USDOnly_New_ForAgent(txtDisplayUDS7.Text, txtCurrency_Code.Text, TFob, DataUtil.ConvertToString(txtshow_check.Text), TUSD, TUSDOther, _IsAgent, _TAgent, IIf(_IsAgent = True, txtInvAgentType.Text, "")) & "        " &
                                                                IIf(CheckIsZeroOrNothing(txtthird_country.Text) = True, txtthird_country.Text & vbCrLf, "") &
                                                                IIf(CheckIsZeroOrNothing(txtback_country.Text) = True, txtback_country.Text & vbCrLf, "") &
                                                                IIf(CheckIsZeroOrNothing(txtplace_Exibition.Text) = True, txtplace_Exibition.Text & vbCrLf, "") &
                                                                IIf(_IsAgent = False, "", CheckInvAgent(_IsAgent, _Invoice_Agent, _CompanyName_Agent)) &
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 2 'T2_back_country,T3_place_Exibition
                All_str = "HS. CODE. " & tariff_All(DataUtil.ConvertToString(txttariff_code.Text)) & vbCrLf _
                                             & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & Form_NewStr_AddressDetail(DataUtil.ConvertToString(txtByCom_CH03.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " &
                                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf &
                                                                                String_Total_USDOnly_New_ForAgent(txtDisplayUDS7.Text, txtCurrency_Code.Text, TFob, DataUtil.ConvertToString(txtshow_check.Text), TUSD, TUSDOther, _IsAgent, _TAgent, IIf(_IsAgent = True, txtInvAgentType.Text, "")) & "        " &
                                                                                IIf(CheckIsZeroOrNothing(txtback_country.Text) = True, txtback_country.Text & vbCrLf, "") &
                                                                               IIf(CheckIsZeroOrNothing(txtplace_Exibition.Text) = True, txtplace_Exibition.Text & vbCrLf, "") &
                                                                                IIf(_IsAgent = False, "", CheckInvAgent(_IsAgent, _Invoice_Agent, _CompanyName_Agent)) &
                                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 3 'T3_place_Exibition
                All_str = "HS. CODE. " & tariff_All(DataUtil.ConvertToString(txttariff_code.Text)) & vbCrLf _
                                             & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & Form_NewStr_AddressDetail(DataUtil.ConvertToString(txtByCom_CH03.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " &
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & String_Total_USDOnly_New_ForAgent(txtDisplayUDS7.Text, txtCurrency_Code.Text, TFob, DataUtil.ConvertToString(txtshow_check.Text), TUSD, TUSDOther, _IsAgent, _TAgent, IIf(_IsAgent = True, txtInvAgentType.Text, "")) & "        " &
                                                                IIf(_IsAgent = False, "", CheckInvAgent(_IsAgent, _Invoice_Agent, _CompanyName_Agent)) &
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 4 '��������
                All_str = "HS. CODE. " & tariff_All(DataUtil.ConvertToString(txttariff_code.Text)) & vbCrLf _
                                             & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Value = All_str & Form_NewStr_AddressDetail(DataUtil.ConvertToString(txtByCom_CH03.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " &
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & String_Total_USDOnly_New_ForAgent(txtDisplayUDS7.Text, txtCurrency_Code.Text, TFob, DataUtil.ConvertToString(txtshow_check.Text), TUSD, TUSDOther, _IsAgent, _TAgent, IIf(_IsAgent = True, txtInvAgentType.Text, "")) & "        " &
                                                                IIf(_IsAgent = False, "", CheckInvAgent(_IsAgent, _Invoice_Agent, _CompanyName_Agent)) &
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 5 'T2_back_country
                All_str = "HS. CODE. " & tariff_All(DataUtil.ConvertToString(txttariff_code.Text)) & vbCrLf _
                                             & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & Form_NewStr_AddressDetail(DataUtil.ConvertToString(txtByCom_CH03.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " &
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & String_Total_USDOnly_New_ForAgent(txtDisplayUDS7.Text, txtCurrency_Code.Text, TFob, DataUtil.ConvertToString(txtshow_check.Text), TUSD, TUSDOther, _IsAgent, _TAgent, IIf(_IsAgent = True, txtInvAgentType.Text, "")) & "        " & vbNewLine &
                                                                _TempB2B & vbNewLine &
                                                                IIf(_IsAgent = False, "", CheckInvAgent(_IsAgent, _Invoice_Agent, _CompanyName_Agent)) &
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 6 'T3_place_Exibition
                All_str = "HS. CODE. " & tariff_All(DataUtil.ConvertToString(txttariff_code.Text)) & vbCrLf _
                                             & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & Form_NewStr_AddressDetail(DataUtil.ConvertToString(txtByCom_CH03.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " &
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & String_Total_USDOnly_New_ForAgent(txtDisplayUDS7.Text, txtCurrency_Code.Text, TFob, DataUtil.ConvertToString(txtshow_check.Text), TUSD, TUSDOther, _IsAgent, _TAgent, IIf(_IsAgent = True, txtInvAgentType.Text, "")) & "        " &
                                                                IIf(_IsAgent = False, "", CheckInvAgent(_IsAgent, _Invoice_Agent, _CompanyName_Agent)) &
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 7 'T1_third_country,T2_back_country
                All_str = "HS. CODE. " & tariff_All(DataUtil.ConvertToString(txttariff_code.Text)) & vbCrLf _
                                             & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & Form_NewStr_AddressDetail(DataUtil.ConvertToString(txtByCom_CH03.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " &
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf &
                                                                String_Total_USDOnly_New_ForAgent(txtDisplayUDS7.Text, txtCurrency_Code.Text, TFob, DataUtil.ConvertToString(txtshow_check.Text), TUSD, TUSDOther, _IsAgent, _TAgent, IIf(_IsAgent = True, txtInvAgentType.Text, "")) & "        " &
                                                                IIf(CheckIsZeroOrNothing(txtthird_country.Text) = True, txtthird_country.Text & vbCrLf, "") &
                                                                IIf(CheckIsZeroOrNothing(txtback_country.Text) = True, txtback_country.Text & vbCrLf, "") &
                                                                IIf(_IsAgent = False, "", CheckInvAgent(_IsAgent, _Invoice_Agent, _CompanyName_Agent)) &
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 8 'T1_third_country,T3_place_Exibition
                All_str = "HS. CODE. " & tariff_All(DataUtil.ConvertToString(txttariff_code.Text)) & vbCrLf _
                                             & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & Form_NewStr_AddressDetail(DataUtil.ConvertToString(txtByCom_CH03.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " &
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf &
                                                                String_Total_USDOnly_New_ForAgent(txtDisplayUDS7.Text, txtCurrency_Code.Text, TFob, DataUtil.ConvertToString(txtshow_check.Text), TUSD, TUSDOther, _IsAgent, _TAgent, IIf(_IsAgent = True, txtInvAgentType.Text, "")) & "        " &
                                                                IIf(CheckIsZeroOrNothing(txtthird_country.Text) = True, txtthird_country.Text & vbCrLf, "") &
                                                               IIf(CheckIsZeroOrNothing(txtplace_Exibition.Text) = True, txtplace_Exibition.Text & vbCrLf, "") &
                                                                IIf(_IsAgent = False, "", CheckInvAgent(_IsAgent, _Invoice_Agent, _CompanyName_Agent)) &
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 9 'T1_third_country
                All_str = "HS. CODE. " & tariff_All(DataUtil.ConvertToString(txttariff_code.Text)) & vbCrLf _
                                             & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & Form_NewStr_AddressDetail(DataUtil.ConvertToString(txtByCom_CH03.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " &
                                                                    BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf &
                                                                    String_Total_USDOnly_New_ForAgent(txtDisplayUDS7.Text, txtCurrency_Code.Text, TFob, DataUtil.ConvertToString(txtshow_check.Text), TUSD, TUSDOther, _IsAgent, _TAgent, IIf(_IsAgent = True, txtInvAgentType.Text, "")) & "        " &
                                                                    IIf(CheckIsZeroOrNothing(txtthird_country.Text) = True, txtthird_country.Text & vbCrLf, "") &
                                                                    IIf(_IsAgent = False, "", CheckInvAgent(_IsAgent, _Invoice_Agent, _CompanyName_Agent)) &
                                                                    "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
        End Select
    End Sub
    Sub All_CheckThird_1(ByVal count_data As Integer)
        Dim All_str As String
        Select Case Check_Third(DataUtil.ConvertToString(txtthird_country.Value),
                                DataUtil.ConvertToString(txtback_country.Value),
                                DataUtil.ConvertToString(txtplace_Exibition.Value))
            Case 1
                All_str = "HS. CODE. " & tariff_All(DataUtil.ConvertToString(txttariff_code.Text)) & vbCrLf _
                                             & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
                If C_TotalRowDe.Text = count_data Then
                    If (Not txtquantity1.Text = String.Empty = True) And DataUtil.ConvertToString(txtquantity1.Text) <> "" And CommonUtility.Get_Decimal(IIf(Not (txtquantity1.Text = String.Empty) = False, 0, txtquantity1.Value)) <> 0 Then
                        txtT_product.Text = All_str & Form_NewStr_AddressDetail(DataUtil.ConvertToString(txtByCom_CH03.Text))
                        GroupFooter1.Visible = True
                        txtTotalAll.Visible = True
                        txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf &
                                            String_Total_USDOnly_New_ForAgent(txtDisplayUDS7.Text, txtCurrency_Code.Text, TFob, DataUtil.ConvertToString(txtshow_check.Text), TUSD, TUSDOther, _IsAgent, _TAgent, IIf(_IsAgent = True, txtInvAgentType.Text, "")) & "        " &
                                                                                        IIf(CheckIsZeroOrNothing(txtthird_country.Text) = True, txtthird_country.Text & vbCrLf, "") &
                                                                                        IIf(CheckIsZeroOrNothing(txtback_country.Text) = True, txtback_country.Text & vbCrLf, "") &
                                                                                       IIf(CheckIsZeroOrNothing(txtplace_Exibition.Text) = True, txtplace_Exibition.Text & vbCrLf, "") &
                                                                                        IIf(_IsAgent = False, "", CheckInvAgent(_IsAgent, _Invoice_Agent, _CompanyName_Agent)) &
                                                                                        "       _____________________________"
                    Else
                        txtT_product.Text = All_str &
                                           "        " &
                                                                                        IIf(CheckIsZeroOrNothing(txtthird_country.Text) = True, txtthird_country.Text & vbCrLf, "") &
                                                                                        IIf(CheckIsZeroOrNothing(txtback_country.Text) = True, txtback_country.Text & vbCrLf, "") &
                                                                                       IIf(CheckIsZeroOrNothing(txtplace_Exibition.Text) = True, txtplace_Exibition.Text & vbCrLf, "") &
                                                                                        IIf(_IsAgent = False, "", CheckInvAgent(_IsAgent, _Invoice_Agent, _CompanyName_Agent)) &
                                                                                        "       _____________________________"
                    End If

                Else
                    txtT_product.Text = All_str
                End If
            Case 2 'T2_back_country,T3_place_Exibition
                All_str = "HS. CODE. " & tariff_All(DataUtil.ConvertToString(txttariff_code.Text)) & vbCrLf _
                                             & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
                If C_TotalRowDe.Text = count_data Then
                    If (Not txtquantity1.Text = String.Empty = True) And DataUtil.ConvertToString(txtquantity1.Text) <> "" And CommonUtility.Get_Decimal(IIf(Not (txtquantity1.Text = String.Empty) = False, 0, txtquantity1.Value)) <> 0 Then
                        txtT_product.Text = All_str & Form_NewStr_AddressDetail(DataUtil.ConvertToString(txtByCom_CH03.Text))
                        GroupFooter1.Visible = True
                        txtTotalAll.Visible = True
                        txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf &
                                            String_Total_USDOnly_New_ForAgent(txtDisplayUDS7.Text, txtCurrency_Code.Text, TFob, DataUtil.ConvertToString(txtshow_check.Text), TUSD, TUSDOther, _IsAgent, _TAgent, IIf(_IsAgent = True, txtInvAgentType.Text, "")) & "        " &
                                                                                                        IIf(CheckIsZeroOrNothing(txtback_country.Text) = True, txtback_country.Text & vbCrLf, "") &
                                                                                                       IIf(CheckIsZeroOrNothing(txtplace_Exibition.Text) = True, txtplace_Exibition.Text & vbCrLf, "") &
                                                                                                        IIf(_IsAgent = False, "", CheckInvAgent(_IsAgent, _Invoice_Agent, _CompanyName_Agent)) &
                                                                                                        "       _____________________________"
                    Else
                        txtT_product.Text = All_str &
                                            "        " &
                                                                                                        IIf(CheckIsZeroOrNothing(txtback_country.Text) = True, txtback_country.Text & vbCrLf, "") &
                                                                                                       IIf(CheckIsZeroOrNothing(txtplace_Exibition.Text) = True, txtplace_Exibition.Text & vbCrLf, "") &
                                                                                                        IIf(_IsAgent = False, "", CheckInvAgent(_IsAgent, _Invoice_Agent, _CompanyName_Agent)) &
                                                                                                        "       _____________________________"
                    End If

                Else
                    txtT_product.Text = All_str
                End If
            Case 3 'T3_place_Exibition
                All_str = "HS. CODE. " & tariff_All(DataUtil.ConvertToString(txttariff_code.Text)) & vbCrLf _
                                             & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
                If C_TotalRowDe.Text = count_data Then
                    If (Not txtquantity1.Text = String.Empty = True) And DataUtil.ConvertToString(txtquantity1.Text) <> "" And CommonUtility.Get_Decimal(IIf(Not (txtquantity1.Text = String.Empty) = False, 0, txtquantity1.Value)) <> 0 Then
                        txtT_product.Text = All_str & Form_NewStr_AddressDetail(DataUtil.ConvertToString(txtByCom_CH03.Text))
                        GroupFooter1.Visible = True
                        txtTotalAll.Visible = True
                        txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & String_Total_USDOnly_New_ForAgent(txtDisplayUDS7.Text, txtCurrency_Code.Text, TFob, DataUtil.ConvertToString(txtshow_check.Text), TUSD, TUSDOther, _IsAgent, _TAgent, IIf(_IsAgent = True, txtInvAgentType.Text, "")) & "        " &
                            IIf(_IsAgent = False, "", CheckInvAgent(_IsAgent, _Invoice_Agent, _CompanyName_Agent)) &
                                                                                        "       _____________________________"
                    Else
                        txtT_product.Text = All_str &
                            IIf(_IsAgent = False, "", CheckInvAgent(_IsAgent, _Invoice_Agent, _CompanyName_Agent)) &
                                           "       _____________________________"
                    End If

                Else
                    txtT_product.Text = All_str
                End If
            Case 4 '��������
                All_str = "HS. CODE. " & tariff_All(DataUtil.ConvertToString(txttariff_code.Text)) & vbCrLf _
                                              & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
                If C_TotalRowDe.Text = count_data Then
                    If (Not txtquantity1.Text = String.Empty = True) And DataUtil.ConvertToString(txtquantity1.Text) <> "" And CommonUtility.Get_Decimal(IIf(Not (txtquantity1.Text = String.Empty) = False, 0, txtquantity1.Value)) <> 0 Then
                        txtT_product.Text = All_str & Form_NewStr_AddressDetail(DataUtil.ConvertToString(txtByCom_CH03.Text))
                        GroupFooter1.Visible = True
                        txtTotalAll.Visible = True
                        txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & String_Total_USDOnly_New_ForAgent(txtDisplayUDS7.Text, txtCurrency_Code.Text, TFob, DataUtil.ConvertToString(txtshow_check.Text), TUSD, TUSDOther, _IsAgent, _TAgent, IIf(_IsAgent = True, txtInvAgentType.Text, "")) & "        " &
                            IIf(_IsAgent = False, "", CheckInvAgent(_IsAgent, _Invoice_Agent, _CompanyName_Agent)) &
                                                                                        "       _____________________________"
                    Else
                        txtT_product.Text = All_str &
                            IIf(_IsAgent = False, "", CheckInvAgent(_IsAgent, _Invoice_Agent, _CompanyName_Agent)) &
                                            "       _____________________________"
                    End If

                Else
                    txtT_product.Text = All_str
                End If
            Case 5 'T2_back_country
                All_str = "HS. CODE. " & tariff_All(DataUtil.ConvertToString(txttariff_code.Text)) & vbCrLf _
                                             & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
                If C_TotalRowDe.Text = count_data Then
                    If (Not txtquantity1.Text = String.Empty = True) And DataUtil.ConvertToString(txtquantity1.Text) <> "" And CommonUtility.Get_Decimal(IIf(Not (txtquantity1.Text = String.Empty) = False, 0, txtquantity1.Value)) <> 0 Then
                        txtT_product.Text = All_str & Form_NewStr_AddressDetail(DataUtil.ConvertToString(txtByCom_CH03.Text))
                        GroupFooter1.Visible = True
                        txtTotalAll.Visible = True
                        txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & String_Total_USDOnly_New_ForAgent(txtDisplayUDS7.Text, txtCurrency_Code.Text, TFob, DataUtil.ConvertToString(txtshow_check.Text), TUSD, TUSDOther, _IsAgent, _TAgent, IIf(_IsAgent = True, txtInvAgentType.Text, "")) &
                        IIf(_IsAgent = False, "", CheckInvAgent(_IsAgent, _Invoice_Agent, _CompanyName_Agent)) &
                                                                                        "       _____________________________"
                    Else
                        txtT_product.Text = All_str &
                        vbNewLine &
                        IIf(_IsAgent = False, "", CheckInvAgent(_IsAgent, _Invoice_Agent, _CompanyName_Agent)) &
                                            "       _____________________________"
                    End If

                Else
                    txtT_product.Text = All_str
                End If
            Case 6 'T3_place_Exibition
                All_str = "HS. CODE. " & tariff_All(DataUtil.ConvertToString(txttariff_code.Text)) & vbCrLf _
                                             & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
                If C_TotalRowDe.Text = count_data Then
                    If (Not txtquantity1.Text = String.Empty = True) And DataUtil.ConvertToString(txtquantity1.Text) <> "" And CommonUtility.Get_Decimal(IIf(Not (txtquantity1.Text = String.Empty) = False, 0, txtquantity1.Value)) <> 0 Then
                        txtT_product.Text = All_str & Form_NewStr_AddressDetail(DataUtil.ConvertToString(txtByCom_CH03.Text))
                        GroupFooter1.Visible = True
                        txtTotalAll.Visible = True
                        txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & String_Total_USDOnly_New_ForAgent(txtDisplayUDS7.Text, txtCurrency_Code.Text, TFob, DataUtil.ConvertToString(txtshow_check.Text), TUSD, TUSDOther, _IsAgent, _TAgent, IIf(_IsAgent = True, txtInvAgentType.Text, "")) & "        " &
                            IIf(_IsAgent = False, "", CheckInvAgent(_IsAgent, _Invoice_Agent, _CompanyName_Agent)) &
                                                                                        "       _____________________________"
                    Else
                        txtT_product.Text = All_str &
                            IIf(_IsAgent = False, "", CheckInvAgent(_IsAgent, _Invoice_Agent, _CompanyName_Agent)) &
                                            "       _____________________________"
                    End If

                Else
                    txtT_product.Text = All_str
                End If
            Case 7 'T1_third_country,T2_back_country
                All_str = "HS. CODE. " & tariff_All(DataUtil.ConvertToString(txttariff_code.Text)) & vbCrLf _
                                              & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
                If C_TotalRowDe.Text = count_data Then
                    If (Not txtquantity1.Text = String.Empty = True) And DataUtil.ConvertToString(txtquantity1.Text) <> "" And CommonUtility.Get_Decimal(IIf(Not (txtquantity1.Text = String.Empty) = False, 0, txtquantity1.Value)) <> 0 Then
                        txtT_product.Text = All_str & Form_NewStr_AddressDetail(DataUtil.ConvertToString(txtByCom_CH03.Text))
                        GroupFooter1.Visible = True
                        txtTotalAll.Visible = True
                        txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf &
                                            String_Total_USDOnly_New_ForAgent(txtDisplayUDS7.Text, txtCurrency_Code.Text, TFob, DataUtil.ConvertToString(txtshow_check.Text), TUSD, TUSDOther, _IsAgent, _TAgent, IIf(_IsAgent = True, txtInvAgentType.Text, "")) & "        " &
                                                                                        IIf(CheckIsZeroOrNothing(txtthird_country.Text) = True, txtthird_country.Text & vbCrLf, "") &
                                                                                        IIf(CheckIsZeroOrNothing(txtback_country.Text) = True, txtback_country.Text & vbCrLf, "") &
                                                                                        IIf(_IsAgent = False, "", CheckInvAgent(_IsAgent, _Invoice_Agent, _CompanyName_Agent)) &
                                                                                        "       _____________________________"
                    Else
                        txtT_product.Text = All_str &
                                            "        " &
                                                                                        IIf(CheckIsZeroOrNothing(txtthird_country.Text) = True, txtthird_country.Text & vbCrLf, "") &
                                                                                        IIf(CheckIsZeroOrNothing(txtback_country.Text) = True, txtback_country.Text & vbCrLf, "") &
                                                                                        IIf(_IsAgent = False, "", CheckInvAgent(_IsAgent, _Invoice_Agent, _CompanyName_Agent)) &
                                                                                        "       _____________________________"
                    End If

                Else
                    txtT_product.Text = All_str
                End If
            Case 8 'T1_third_country,T3_place_Exibition
                All_str = "HS. CODE. " & tariff_All(DataUtil.ConvertToString(txttariff_code.Text)) & vbCrLf _
                                             & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
                If C_TotalRowDe.Text = count_data Then
                    If (Not txtquantity1.Text = String.Empty = True) And DataUtil.ConvertToString(txtquantity1.Text) <> "" And CommonUtility.Get_Decimal(IIf(Not (txtquantity1.Text = String.Empty) = False, 0, txtquantity1.Value)) <> 0 Then
                        txtT_product.Text = All_str & Form_NewStr_AddressDetail(DataUtil.ConvertToString(txtByCom_CH03.Text))
                        GroupFooter1.Visible = True
                        txtTotalAll.Visible = True
                        txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf &
                                            String_Total_USDOnly_New_ForAgent(txtDisplayUDS7.Text, txtCurrency_Code.Text, TFob, DataUtil.ConvertToString(txtshow_check.Text), TUSD, TUSDOther, _IsAgent, _TAgent, IIf(_IsAgent = True, txtInvAgentType.Text, "")) & "        " &
                                                                                        IIf(CheckIsZeroOrNothing(txtthird_country.Text) = True, txtthird_country.Text & vbCrLf, "") &
                                                                                       IIf(CheckIsZeroOrNothing(txtplace_Exibition.Text) = True, txtplace_Exibition.Text & vbCrLf, "") &
                                                                                        IIf(_IsAgent = False, "", CheckInvAgent(_IsAgent, _Invoice_Agent, _CompanyName_Agent)) &
                                                                                        "       _____________________________"
                    Else
                        txtT_product.Text = All_str &
                                            "        " &
                                                                                        IIf(CheckIsZeroOrNothing(txtthird_country.Text) = True, txtthird_country.Text & vbCrLf, "") &
                                                                                       IIf(CheckIsZeroOrNothing(txtplace_Exibition.Text) = True, txtplace_Exibition.Text & vbCrLf, "") &
                                                                                        IIf(_IsAgent = False, "", CheckInvAgent(_IsAgent, _Invoice_Agent, _CompanyName_Agent)) &
                                                                                        "       _____________________________"
                    End If

                Else
                    txtT_product.Text = All_str
                End If
            Case 9 'T1_third_country
                All_str = "HS. CODE. " & tariff_All(DataUtil.ConvertToString(txttariff_code.Text)) & vbCrLf _
                                             & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
                If C_TotalRowDe.Text = count_data Then
                    If (Not txtquantity1.Text = String.Empty = True) And DataUtil.ConvertToString(txtquantity1.Text) <> "" And CommonUtility.Get_Decimal(IIf(Not (txtquantity1.Text = String.Empty) = False, 0, txtquantity1.Value)) <> 0 Then
                        txtT_product.Text = All_str & Form_NewStr_AddressDetail(DataUtil.ConvertToString(txtByCom_CH03.Text))
                        GroupFooter1.Visible = True
                        txtTotalAll.Visible = True
                        txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf &
                                             String_Total_USDOnly_New_ForAgent(txtDisplayUDS7.Text, txtCurrency_Code.Text, TFob, DataUtil.ConvertToString(txtshow_check.Text), TUSD, TUSDOther, _IsAgent, _TAgent, IIf(_IsAgent = True, txtInvAgentType.Text, "")) & "        " &
                                                                                            IIf(CheckIsZeroOrNothing(txtthird_country.Text) = True, txtthird_country.Text & vbCrLf, "") &
                                                                                            IIf(_IsAgent = False, "", CheckInvAgent(_IsAgent, _Invoice_Agent, _CompanyName_Agent)) &
                                                                                            "       _____________________________"
                    Else
                        txtT_product.Text = All_str &
                                            "        " &
                                                                                            IIf(CheckIsZeroOrNothing(txtthird_country.Text) = True, txtthird_country.Text & vbCrLf, "") &
                                                                                            IIf(_IsAgent = False, "", CheckInvAgent(_IsAgent, _Invoice_Agent, _CompanyName_Agent)) &
                                                                                            "       _____________________________"
                    End If

                Else
                    txtT_product.Text = All_str
                End If
        End Select
    End Sub
    Sub All_CheckThird_0(ByVal count_data As Integer)
        Dim All_str As String
        Select Case Check_Third(DataUtil.ConvertToString(txtthird_country.Value),
                                DataUtil.ConvertToString(txtback_country.Value),
                                DataUtil.ConvertToString(txtplace_Exibition.Value))
            Case 1
                All_str = "HS. CODE. " & tariff_All(DataUtil.ConvertToString(txttariff_code.Text)) & vbCrLf _
                                             & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & "        " &
                    IIf(CheckIsZeroOrNothing(txtthird_country.Text) = True, txtthird_country.Text & vbCrLf, "") &
                                                                IIf(CheckIsZeroOrNothing(txtback_country.Text) = True, txtback_country.Text & vbCrLf, "") &
                                                               IIf(CheckIsZeroOrNothing(txtplace_Exibition.Text) = True, txtplace_Exibition.Text & vbCrLf, "") &
                                                                IIf(_IsAgent = False, "", CheckInvAgent(_IsAgent, _Invoice_Agent, _CompanyName_Agent)) &
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 2 'T2_back_country,T3_place_Exibition
                All_str = "HS. CODE. " & tariff_All(DataUtil.ConvertToString(txttariff_code.Text)) & vbCrLf _
                                             & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str &
                    IIf(CheckIsZeroOrNothing(txtback_country.Text) = True, txtback_country.Text & vbCrLf, "") &
                                                                               IIf(CheckIsZeroOrNothing(txtplace_Exibition.Text) = True, txtplace_Exibition.Text & vbCrLf, "") &
                                                                                IIf(_IsAgent = False, "", CheckInvAgent(_IsAgent, _Invoice_Agent, _CompanyName_Agent)) &
                                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 3 'T3_place_Exibition
                All_str = "HS. CODE. " & tariff_All(DataUtil.ConvertToString(txttariff_code.Text)) & vbCrLf _
                                             & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str &
                        IIf(_IsAgent = False, "", CheckInvAgent(_IsAgent, _Invoice_Agent, _CompanyName_Agent)) &
                     "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 4 '��������
                All_str = "HS. CODE. " & tariff_All(DataUtil.ConvertToString(txttariff_code.Text)) & vbCrLf _
                                             & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str &
                        IIf(_IsAgent = False, "", CheckInvAgent(_IsAgent, _Invoice_Agent, _CompanyName_Agent)) &
                     "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 5 'T2_back_country
                All_str = "HS. CODE. " & tariff_All(DataUtil.ConvertToString(txttariff_code.Text)) & vbCrLf _
                                             & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & vbCrLf &
                    _TempB2B & vbNewLine &
                    IIf(_IsAgent = False, "", CheckInvAgent(_IsAgent, _Invoice_Agent, _CompanyName_Agent)) &
                    "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 6 'T3_place_Exibition
                All_str = "HS. CODE. " & tariff_All(DataUtil.ConvertToString(txttariff_code.Text)) & vbCrLf _
                                             & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str &
                    IIf(_IsAgent = False, "", CheckInvAgent(_IsAgent, _Invoice_Agent, _CompanyName_Agent)) &
                     "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 7 'T1_third_country,T2_back_country
                All_str = "HS. CODE. " & tariff_All(DataUtil.ConvertToString(txttariff_code.Text)) & vbCrLf _
                                             & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str &
                    IIf(CheckIsZeroOrNothing(txtthird_country.Text) = True, txtthird_country.Text & vbCrLf, "") &
                    IIf(CheckIsZeroOrNothing(txtback_country.Text) = True, txtback_country.Text & vbCrLf, "") &
                    IIf(_IsAgent = False, "", CheckInvAgent(_IsAgent, _Invoice_Agent, _CompanyName_Agent)) &
                    "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 8 'T1_third_country,T3_place_Exibition
                All_str = "HS. CODE. " & tariff_All(DataUtil.ConvertToString(txttariff_code.Text)) & vbCrLf _
                                             & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str &
                     IIf(CheckIsZeroOrNothing(txtthird_country.Text) = True, txtthird_country.Text & vbCrLf, "") &
                   IIf(CheckIsZeroOrNothing(txtplace_Exibition.Text) = True, txtplace_Exibition.Text & vbCrLf, "") &
                    IIf(_IsAgent = False, "", CheckInvAgent(_IsAgent, _Invoice_Agent, _CompanyName_Agent)) &
                    "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 9 'T1_third_country
                All_str = "HS. CODE. " & tariff_All(DataUtil.ConvertToString(txttariff_code.Text)) & vbCrLf _
                                             & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str &
                    IIf(CheckIsZeroOrNothing(txtthird_country.Text) = True, txtthird_country.Text & vbCrLf, "") &
                    IIf(_IsAgent = False, "", CheckInvAgent(_IsAgent, _Invoice_Agent, _CompanyName_Agent)) &
                    "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
        End Select
    End Sub

    Function tariff_All(ByVal _tariff As String) As String
        _tariff = Left(_tariff, 6)

        Dim str_tariff As String
        str_tariff = Mid(_tariff, 1, 4) & "." & Mid(_tariff, 5, 2)

        Return str_tariff
    End Function

    Dim Gr_ As String
    Function Callinvoice_board() As Array
        Dim sNewValue As Array
        Dim sValue As Array = Split(txtinvoice_board.Text, "%")
        Dim _persenStr As String = DataUtil.ConvertToString(txtinvoice_board.Text) & "%"
        Select Case sValue.Length
            Case 1
                sNewValue = Split(_persenStr, "%")
                Return sNewValue
            Case Else
                Return sValue
        End Select
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
        If DataUtil.ConvertToString(txtCheckGrossDetail.Text) = "GWDetail" Then
            'by rut FOBOther
            If txtdestination_country.Text = "HK" Then
                str_USDInvoice = ""
            Else
                str_USDInvoice = Format(CommonUtility.Get_Decimal(txtUSDInvoiceDetail.Text), "#,##0.00##") & " " & txtCurrency_Code.Text '& " USD" 'invoice ��ҧ����� �ʴ���Ť�� ��¡�÷ء��¡��
            End If
        Else
            str_USDInvoice = Str_USDinvoiceDetail 'invoice ��ҧ����� �ʴ���Ť�� ��¡���á��¡������
        End If

        Select Case checkNull_Show(DataUtil.ConvertToString(txtshow_check.Text))
            Case "100000" ' ����ͧ invoice ��ҧ����� �ʴ���Ť��
                Select Case CheckDetailGross
                    Case "GWDetail" '�¡��¡��
                        '���͹����� ��Ѻ�� �ѹ��� 26-10-2012 ����ǡѺ RVC �ͧ�ѹ Case 2,8 ��ͧ�ʴ���Ť�� ������������ʴ�
                        'begin RVC
                        Select Case Mid(DataUtil.ConvertToString(txtletter.Value), 1, 50)
                            Case "2", "8" '�ʴ���Ť�� Detail
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
                                                                         "_______" & vbNewLine & Format(IIf(txtDisplayUnitType.Text = "NET WEIGHT", TNet, TGross), "#,##0.00##") & " " & TempUnit3
                                                    Case Else '�� RVC ���ҧ���� �ʴ���Ť�����
                                                        If txtdestination_country.Text = "HK" Then
                                                            str_Display = str_gross & vbNewLine & str_USDInvoice & vbNewLine &
                                                                         "_______" & vbNewLine & Format(IIf(txtDisplayUnitType.Text = "NET WEIGHT", TNet, TGross), "#,##0.00##") & " " & TempUnit3
                                                        Else
                                                            str_Display = str_gross & vbNewLine & str_USDInvoice & vbNewLine &
                                                                         "_______" & vbNewLine & Format(IIf(txtDisplayUnitType.Text = "NET WEIGHT", TNet, TGross), "#,##0.00##") & " " & TempUnit3 &
                                                                         vbNewLine & Format(TUSD, "#,##0.00##") & " " & txtCurrency_Code.Text
                                                        End If

                                                End Select
                                            Else
                                                str_Display = str_gross & vbNewLine & str_USDInvoice
                                            End If
                                    End Select
                                End If
                            Case Else '����ʴ���Ť�� Detail
                                If Mid(DataUtil.ConvertToString(txtFOBDisplay.Value), 1, 50) = "Display" Then
                                    If Mid(DataUtil.ConvertToString(txtWeightDisplayHeader.Value), 1, 50) = "GROSS WEIGHT - NET WEIGHT" Then
                                        str_gross = GrossTxtDetail() & vbNewLine & NetWeight_(txtnet_weight.Value, txtunit_code2.Value) 'Mid(txtnet_weight.Value, 1, 9) & " " & Mid(txtunit_code2.Value, 1, 10) '& vbNewLine & FobTxt()
                                        Select Case C_TotalRowDe.Text
                                            Case 1
                                                str_Display = str_gross '& vbNewLine & str_USDInvoice
                                            Case Else
                                                If Cpage = CountAllDetail Then
                                                    Select Case Check_CaseRVCCount
                                                        Case 9 '�����ҧ��蹻� ����ʴ���Ť�����
                                                            str_Display = str_gross & vbNewLine &
                                                                        "_______" & vbNewLine & Format(IIf(txtDisplayUnitType.Text = "NET WEIGHT", TNet, TGross), "#,##0.00##") & " " & TempUnit3 &
                                                                        vbNewLine & Format(TNet, "#,##0.00##") & " " & DataUtil.ConvertToString(txtunit_code2.Value)

                                                        Case Else '�� RVC ���ҧ���� �ʴ���Ť�����
                                                            If txtdestination_country.Text = "HK" Then
                                                                str_Display = str_gross & vbNewLine &
                                                                        "_______" & vbNewLine & Format(IIf(txtDisplayUnitType.Text = "NET WEIGHT", TNet, TGross), "#,##0.00##") & " " & TempUnit3 &
                                                                        vbNewLine & Format(TNet, "#,##0.00##") & " " & DataUtil.ConvertToString(txtunit_code2.Value)

                                                            Else
                                                                str_Display = str_gross & vbNewLine &
                                                                         "_______" & vbNewLine & Format(IIf(txtDisplayUnitType.Text = "NET WEIGHT", TNet, TGross), "#,##0.00##") & " " & TempUnit3 &
                                                                         vbNewLine & Format(TNet, "#,##0.00##") & " " & DataUtil.ConvertToString(txtunit_code2.Value) &
                                                                         vbNewLine & Format(TUSD, "#,##0.00##") & " " & txtCurrency_Code.Text
                                                            End If

                                                    End Select
                                                Else
                                                    str_Display = str_gross '& vbNewLine & str_USDInvoice
                                                End If

                                        End Select
                                    ElseIf Mid(DataUtil.ConvertToString(txtWeightDisplayHeader.Value), 1, 50) = "GROSS WEIGHT" Then
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
                                                            If txtdestination_country.Text = "HK" Then
                                                                str_Display = str_gross & vbNewLine &
                                                                               "_______" & vbNewLine & IIf(txtDisplayUnitType.Text = "NET WEIGHT", "N.W.", "") &
                                                                               Format(IIf(txtDisplayUnitType.Text = "NET WEIGHT", TNet, TGross), "#,##0.00##") & " " & TempUnit3
                                                            Else
                                                                str_Display = str_gross & vbNewLine &
                                                                               "_______" & vbNewLine & IIf(txtDisplayUnitType.Text = "NET WEIGHT", "N.W.", "") &
                                                                               Format(IIf(txtDisplayUnitType.Text = "NET WEIGHT", TNet, TGross), "#,##0.00##") & " " & TempUnit3 &
                                                                               vbNewLine & Format(TUSD, "#,##0.00##") & " " & txtCurrency_Code.Text
                                                            End If

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
                        Select Case Mid(DataUtil.ConvertToString(txtletter.Value), 1, 50)
                            Case "2", "8" '�ʴ���Ť�� Detail
                                If Mid(DataUtil.ConvertToString(txtFOBDisplay.Value), 1, 50) = "Display" Then
                                    If Mid(DataUtil.ConvertToString(txtWeightDisplayHeader.Value), 1, 50) = "GROSS WEIGHT - NET WEIGHT" Then
                                        str_gross = GrossTxt() & vbNewLine & NetWeight_(txtnet_weight.Value, txtunit_code2.Value) 'Mid(txtnet_weight.Value, 1, 9) & " " & Mid(txtunit_code2.Value, 1, 10) '& vbNewLine & FobTxt()
                                        Select Case C_TotalRowDe.Text
                                            Case 1
                                                str_Display = str_gross & vbNewLine & str_USDInvoice
                                            Case Else

                                                str_Display = str_gross & vbNewLine & str_USDInvoice
                                        End Select
                                    ElseIf Mid(DataUtil.ConvertToString(txtWeightDisplayHeader.Value), 1, 50) = "GROSS WEIGHT" Then
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
                                If Mid(DataUtil.ConvertToString(txtFOBDisplay.Value), 1, 50) = "Display" Then
                                    If Mid(DataUtil.ConvertToString(txtWeightDisplayHeader.Value), 1, 50) = "GROSS WEIGHT - NET WEIGHT" Then
                                        str_gross = GrossTxt() & vbNewLine & NetWeight_(txtnet_weight.Value, txtunit_code2.Value) 'Mid(txtnet_weight.Value, 1, 9) & " " & Mid(txtunit_code2.Value, 1, 10) '& vbNewLine & FobTxt()
                                        Select Case C_TotalRowDe.Text
                                            Case 1
                                                str_Display = str_gross '& vbNewLine & str_USDInvoice
                                            Case Else

                                                str_Display = str_gross '& vbNewLine & str_USDInvoice
                                        End Select
                                    ElseIf Mid(DataUtil.ConvertToString(txtWeightDisplayHeader.Value), 1, 50) = "GROSS WEIGHT" Then
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
                Select Case CheckDetailGross
                    Case "GWDetail" '�¡��¡��
                        '���͹����� ��Ѻ�� �ѹ��� 26-10-2012 ����ǡѺ RVC �ͧ�ѹ Case 2,8 ��ͧ�ʴ���Ť�� ������������ʴ�
                        'begin RVC
                        Select Case Mid(DataUtil.ConvertToString(txtletter.Value), 1, 50)
                            Case "2", "8" '�ʴ���Ť�� Detail
                                'form4 �� WeightDisplayHeader
                                If Mid(DataUtil.ConvertToString(txtFOBDisplay.Value), 1, 50) = "Display" Then

                                    If Mid(DataUtil.ConvertToString(txtWeightDisplayHeader.Value), 1, 50) = "GROSS WEIGHT - NET WEIGHT" Then
                                        str_gross = GrossTxtDetail() & vbNewLine & FobTxt() & vbNewLine & NetWeight_(txtnet_weight.Value, txtunit_code2.Value)
                                        Select Case C_TotalRowDe.Text
                                            Case 1
                                                str_Display = str_gross
                                            Case Else
                                                If Cpage = CountAllDetail Then
                                                    Select Case Check_CaseRVCCount
                                                        Case 9 '�����ҧ��蹻� ����ʴ���Ť�����
                                                            str_Display = str_gross & vbNewLine &
                                                                                "_______" & vbNewLine & IIf(txtDisplayUnitType.Text = "NET WEIGHT", "N.W.", "") &
                                                                                Format(IIf(txtDisplayUnitType.Text = "NET WEIGHT", TNet, TGross), "#,##0.00##") & " " & TempUnit3 &
                                                                                vbNewLine & Format(TNet, "#,##0.00##") & " " & DataUtil.ConvertToString(txtunit_code2.Value)
                                                        Case Else '�� RVC ���ҧ���� �ʴ���Ť�����
                                                            If txtdestination_country.Text = "HK" Then
                                                                str_Display = str_gross & vbNewLine &
                                                                               "_______" & vbNewLine & IIf(txtDisplayUnitType.Text = "NET WEIGHT", "N.W.", "") &
                                                                               Format(IIf(txtDisplayUnitType.Text = "NET WEIGHT", TNet, TGross), "#,##0.00##") & " " & TempUnit3 &
                                                                               vbNewLine & Format(TNet, "#,##0.00##") & " " & DataUtil.ConvertToString(txtunit_code2.Value)
                                                            Else
                                                                str_Display = str_gross & vbNewLine &
                                                                                   "_______" & vbNewLine & IIf(txtDisplayUnitType.Text = "NET WEIGHT", "N.W.", "") &
                                                                                   Format(IIf(txtDisplayUnitType.Text = "NET WEIGHT", TNet, TGross), "#,##0.00##") & " " & TempUnit3 &
                                                                                   vbNewLine & FobTxtAll() &
                                                                                   vbNewLine & Format(TNet, "#,##0.00##") & " " & DataUtil.ConvertToString(txtunit_code2.Value)
                                                            End If

                                                    End Select
                                                Else
                                                    str_Display = str_gross
                                                End If

                                        End Select
                                    ElseIf Mid(DataUtil.ConvertToString(txtWeightDisplayHeader.Value), 1, 50) = "GROSS WEIGHT" Then
                                        str_gross = GrossTxtDetail() & vbNewLine & FobTxt()
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
                                                            If txtdestination_country.Text = "HK" Then
                                                                str_Display = str_gross & vbNewLine &
                                                                               "_______" & vbNewLine & IIf(txtDisplayUnitType.Text = "NET WEIGHT", "N.W.", "") &
                                                                               Format(IIf(txtDisplayUnitType.Text = "NET WEIGHT", TNet, TGross), "#,##0.00##") & " " & TempUnit3
                                                            Else
                                                                str_Display = str_gross & vbNewLine &
                                                                                "_______" & vbNewLine & IIf(txtDisplayUnitType.Text = "NET WEIGHT", "N.W.", "") &
                                                                                Format(IIf(txtDisplayUnitType.Text = "NET WEIGHT", TNet, TGross), "#,##0.00##") & " " & TempUnit3 &
                                                                                vbNewLine & FobTxtAll()
                                                            End If

                                                    End Select
                                                Else
                                                    str_Display = str_gross
                                                End If

                                        End Select
                                    End If
                                End If
                            Case Else '����ʴ���Ť�� Detail
                                'form4 �� WeightDisplayHeader
                                If Mid(DataUtil.ConvertToString(txtFOBDisplay.Value), 1, 50) = "Display" Then

                                    If Mid(DataUtil.ConvertToString(txtWeightDisplayHeader.Value), 1, 50) = "GROSS WEIGHT - NET WEIGHT" Then
                                        str_gross = GrossTxtDetail() & vbNewLine & NetWeight_(txtnet_weight.Value, txtunit_code2.Value)
                                        Select Case C_TotalRowDe.Text
                                            Case 1
                                                str_Display = str_gross
                                            Case Else
                                                If Cpage = CountAllDetail Then
                                                    Select Case Check_CaseRVCCount
                                                        Case 9 '�����ҧ��蹻� ����ʴ���Ť�����
                                                            str_Display = str_gross & vbNewLine &
                                                                                    "_______" & vbNewLine & IIf(txtDisplayUnitType.Text = "NET WEIGHT", "N.W.", "") &
                                                                                    Format(IIf(txtDisplayUnitType.Text = "NET WEIGHT", TNet, TGross), "#,##0.00##") & " " & TempUnit3 &
                                                                                    vbNewLine & Format(TNet, "#,##0.00##") & " " & DataUtil.ConvertToString(txtunit_code2.Value)
                                                        Case Else '�� RVC ���ҧ���� �ʴ���Ť�����
                                                            If txtdestination_country.Text = "HK" Then
                                                                str_Display = str_gross & vbNewLine &
                                                                                    "_______" & vbNewLine & IIf(txtDisplayUnitType.Text = "NET WEIGHT", "N.W.", "") &
                                                                                    Format(IIf(txtDisplayUnitType.Text = "NET WEIGHT", TNet, TGross), "#,##0.00##") & " " & TempUnit3 &
                                                                                    vbNewLine & Format(TNet, "#,##0.00##") & " " & DataUtil.ConvertToString(txtunit_code2.Value)
                                                            Else
                                                                str_Display = str_gross & vbNewLine &
                                                                                    "_______" & vbNewLine & IIf(txtDisplayUnitType.Text = "NET WEIGHT", "N.W.", "") &
                                                                                    Format(IIf(txtDisplayUnitType.Text = "NET WEIGHT", TNet, TGross), "#,##0.00##") & " " & TempUnit3 &
                                                                                    vbNewLine & FobTxtAll() &
                                                                                    vbNewLine & Format(TNet, "#,##0.00##") & " " & DataUtil.ConvertToString(txtunit_code2.Value)
                                                            End If

                                                    End Select
                                                Else
                                                    str_Display = str_gross
                                                End If

                                        End Select
                                    ElseIf Mid(DataUtil.ConvertToString(txtWeightDisplayHeader.Value), 1, 50) = "GROSS WEIGHT" Then
                                        str_gross = GrossTxtDetail() '& vbNewLine & FobTxt()
                                        Select Case C_TotalRowDe.Text
                                            Case 1
                                                str_Display = str_gross
                                            Case Else
                                                If Cpage = CountAllDetail Then
                                                    Select Case Check_CaseRVCCount
                                                        Case 9 '�����ҧ��蹻� ����ʴ���Ť�����
                                                            str_Display = str_gross & vbNewLine &
                                                                                    "_______" & vbNewLine & IIf(txtDisplayUnitType.Text = "NET WEIGHT", "N.W.", "") &
                                                                                    Format(IIf(txtDisplayUnitType.Text = "NET WEIGHT", TNet, TGross), "#,##0.00##") & " " & TempUnit3
                                                        Case Else '�� RVC ���ҧ���� �ʴ���Ť�����
                                                            If txtdestination_country.Text = "HK" Then
                                                                str_Display = str_gross & vbNewLine &
                                                                                   "_______" & vbNewLine & IIf(txtDisplayUnitType.Text = "NET WEIGHT", "N.W.", "") &
                                                                                   Format(IIf(txtDisplayUnitType.Text = "NET WEIGHT", TNet, TGross), "#,##0.00##") & " " & TempUnit3
                                                            Else
                                                                str_Display = str_gross & vbNewLine &
                                                                                   "_______" & vbNewLine & IIf(txtDisplayUnitType.Text = "NET WEIGHT", "N.W.", "") &
                                                                                   Format(IIf(txtDisplayUnitType.Text = "NET WEIGHT", TNet, TGross), "#,##0.00##") & " " & TempUnit3 &
                                                                                   vbNewLine & FobTxtAll()
                                                            End If

                                                    End Select
                                                Else
                                                    str_Display = str_gross
                                                End If
                                        End Select
                                    End If
                                End If
                        End Select
                        'end RVC

                End Select
        End Select

        Return str_Display
    End Function

    '//madnattz
    Function GrossTxt() As String
        Dim Str_GrossTxt As String
        Dim Dec_Gross As Decimal
        Dec_Gross = CDec(Check_Null(DataUtil.ConvertToString(txtgross_weightH.Value))) 'Format(CDec(Check_Null(DataUtil.ConvertToString(txtgross_weightH.Value))), "#,##0.00##")

        Dim W_type As String = ""
        If txtDisplayUnitType.Text = "NET WEIGHT" Then
            W_type = "N.W. "
            '//Dec_Gross = CDec(Check_Null(DataUtil.ConvertToString(txtnet_weight.Value)))
        End If

        Str_GrossTxt = W_type & Format(Dec_Gross, "#,##0.00##") & " " & DataUtil.ConvertToString(txtg_unit_code.Value) 'CStr(Dec_Gross) & " " & DataUtil.ConvertToString(txtg_unit_code.Value)

        Return Str_GrossTxt
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

    'by rut FOB Other
    Function FobTxt() As String
        Dim Str_FobTxt As String
        Dim Dec_Fob As Decimal

        If txtdestination_country.Text = "HK" Then
            Return ""
        End If

        ''ByTine 21-02-2560 ��Ť�� Invoice ���˹��
        If _IsAgent = True Then
            'by rut FOB Other
            Select Case DataUtil.ConvertToString(txtCurrency_Code.Text)
                Case Is <> "USD"
                    '' �����ҡѺ USD = ��ʡ���Թ����
                    If txtInvAgentType.Text = 0 Then
                        ''0 = �ʴ���Ť�� Inv ���˹��
                        If Check_Null(DataUtil.ConvertToString(txtUSDAgent.Value)) > 0 Then
                            Dec_Fob = CDec(Check_Null(DataUtil.ConvertToString(txtUSDAgent.Value))) 'Format(CDec(Check_Null(DataUtil.ConvertToString(txtFOB_AMT.Value))), "#,##0.0###")
                            Str_FobTxt = Format(Dec_Fob, "#,##0.00##") & " " & txtCurrency_Code.Text
                        Else
                            Str_FobTxt = Format(0, "#,##0.00##") & " " & txtCurrency_Code.Text '""
                        End If
                    Else
                        ''1 = �ʴ���Ť�� FOB �ͧʡ���Թ��蹵������
                        If Check_Null(DataUtil.ConvertToString(txtPriceOtherDetail.Value)) > 0 Then
                            Dec_Fob = CDec(Check_Null(DataUtil.ConvertToString(txtPriceOtherDetail.Value))) 'Format(CDec(Check_Null(DataUtil.ConvertToString(txtFOB_AMT.Value))), "#,##0.0###")
                            Str_FobTxt = Format(Dec_Fob, "#,##0.00##") & " " & txtCurrency_Code.Text '& " USD"
                        Else
                            Str_FobTxt = Format(0, "#,##0.00##") & " " & txtCurrency_Code.Text '""
                        End If
                    End If
                Case Else
                    '' ��ʡ���Թ USD
                    If txtInvAgentType.Text = 0 Then
                        ''0 = �ʴ���Ť�� Inv ���˹��
                        If Check_Null(DataUtil.ConvertToString(txtUSDAgent.Value)) > 0 Then
                            Dec_Fob = CDec(Check_Null(DataUtil.ConvertToString(txtUSDAgent.Value))) 'Format(CDec(Check_Null(DataUtil.ConvertToString(txtFOB_AMT.Value))), "#,##0.0###")
                            Str_FobTxt = Format(Dec_Fob, "#,##0.00##") & " " & txtCurrency_Code.Text
                        Else
                            Str_FobTxt = Format(0, "#,##0.00##") & " " & txtCurrency_Code.Text '""
                        End If
                    Else
                        ''1 = �ʴ���Ť�� FOB �������
                        If Check_Null(DataUtil.ConvertToString(txtFOB_AMT.Value)) > 0 Then
                            Dec_Fob = CDec(Check_Null(DataUtil.ConvertToString(txtFOB_AMT.Value))) 'Format(CDec(Check_Null(DataUtil.ConvertToString(txtFOB_AMT.Value))), "#,##0.0###")
                            Str_FobTxt = Format(Dec_Fob, "#,##0.00##") & " " & txtCurrency_Code.Text '& " USD"
                        Else
                            Str_FobTxt = Format(0, "#,##0.00##") & " " & txtCurrency_Code.Text '""
                        End If
                    End If

            End Select
        Else
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

        End If

        Return Str_FobTxt
    End Function
    'by rut FOB Other
    Function FobTxtAll() As String
        Dim Str_FobTxt As String
        Dim Dec_Fob As Decimal

        ''ByTine 21-02-2560 ��Ť�� Invoice ���˹��
        If _IsAgent = True Then
            'by rut FOB Other
            Select Case DataUtil.ConvertToString(txtCurrency_Code.Text)
                Case Is <> "USD"
                    '' �����ҡѺ USD = ��ʡ���Թ����
                    If txtInvAgentType.Text = 0 Then
                        ''0 = �ʴ���Ť�� Inv ���˹��
                        If Check_Null(DataUtil.ConvertToString(txtUSDAgent.Value)) > 0 Then
                            Dec_Fob = CDec(Check_Null(DataUtil.ConvertToString(_TAgent))) 'Format(CDec(Check_Null(DataUtil.ConvertToString(txtFOB_AMT.Value))), "#,##0.0###")
                            Str_FobTxt = Format(Dec_Fob, "#,##0.00##") & " " & txtCurrency_Code.Text
                        Else
                            Str_FobTxt = Format(0, "#,##0.00##") & " " & txtCurrency_Code.Text '""
                        End If
                    Else
                        ''1 = �ʴ���Ť�� FOB �ͧʡ���Թ��蹵������
                        If Check_Null(DataUtil.ConvertToString(txtPriceOtherDetail.Value)) > 0 Then
                            Dec_Fob = CDec(Check_Null(DataUtil.ConvertToString(TUSDOther))) 'Format(CDec(Check_Null(DataUtil.ConvertToString(txtFOB_AMT.Value))), "#,##0.0###")
                            Str_FobTxt = Format(Dec_Fob, "#,##0.00##") & " " & txtCurrency_Code.Text '& " USD"
                        Else
                            Str_FobTxt = Format(0, "#,##0.00##") & " " & txtCurrency_Code.Text '""
                        End If
                    End If
                Case Else
                    '' ��ʡ���Թ USD
                    If txtInvAgentType.Text = 0 Then
                        ''0 = �ʴ���Ť�� Inv ���˹��
                        If Check_Null(DataUtil.ConvertToString(txtUSDAgent.Value)) > 0 Then
                            Dec_Fob = CDec(Check_Null(DataUtil.ConvertToString(_TAgent))) 'Format(CDec(Check_Null(DataUtil.ConvertToString(txtFOB_AMT.Value))), "#,##0.0###")
                            Str_FobTxt = Format(Dec_Fob, "#,##0.00##") & " " & txtCurrency_Code.Text
                        Else
                            Str_FobTxt = Format(0, "#,##0.00##") & " " & txtCurrency_Code.Text '""
                        End If
                    Else
                        ''1 = �ʴ���Ť�� FOB �������
                        If Check_Null(DataUtil.ConvertToString(txtFOB_AMT.Value)) > 0 Then
                            Dec_Fob = CDec(Check_Null(DataUtil.ConvertToString(TFob))) 'Format(CDec(Check_Null(DataUtil.ConvertToString(txtFOB_AMT.Value))), "#,##0.0###")
                            Str_FobTxt = Format(Dec_Fob, "#,##0.00##") & " " & txtCurrency_Code.Text '& " USD"
                        Else
                            Str_FobTxt = Format(0, "#,##0.00##") & " " & txtCurrency_Code.Text '""
                        End If
                    End If
                    'Case Is <> "USD"
                    '    If Check_Null(DataUtil.ConvertToString(txtPriceOtherDetail.Value)) > 0 Then
                    '        Dec_Fob = CDec(Check_Null(DataUtil.ConvertToString(TUSDOther.Value))) 'Format(CDec(Check_Null(DataUtil.ConvertToString(txtFOB_AMT.Value))), "#,##0.0###")
                    '        Str_FobTxt = Format(Dec_Fob, "#,##0.00##") & " " & txtCurrency_Code.Text
                    '    Else
                    '        Str_FobTxt = Format(0, "#,##0.00##") & " " & txtCurrency_Code.Text '""
                    '    End If
                    'Case Else
                    '    If Check_Null(DataUtil.ConvertToString(txtUSDAgent.Value)) > 0 Then
                    '        Dec_Fob = CDec(Check_Null(DataUtil.ConvertToString(_TAgent))) 'Format(CDec(Check_Null(DataUtil.ConvertToString(txtFOB_AMT.Value))), "#,##0.0###")
                    '        Str_FobTxt = Format(Dec_Fob, "#,##0.00##") & " " & txtCurrency_Code.Text '& " USD"
                    '    Else
                    '        Str_FobTxt = Format(0, "#,##0.00##") & " " & txtCurrency_Code.Text '""
                    '    End If
            End Select
        Else
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
                        Dec_Fob = CDec(Check_Null(DataUtil.ConvertToString(TFob))) 'Format(CDec(Check_Null(DataUtil.ConvertToString(txtFOB_AMT.Value))), "#,##0.0###")
                        Str_FobTxt = Format(Dec_Fob, "#,##0.00##") & " " & txtCurrency_Code.Text '& " USD"
                    Else
                        Str_FobTxt = Format(0, "#,##0.00##") & " " & txtCurrency_Code.Text '""
                    End If
            End Select
        End If

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
        For i = 0 To checkNull_Show(DataUtil.ConvertToString(txtshow_check.Value)).Length - 1
            str_ += Mid(checkNull_Show(DataUtil.ConvertToString(txtshow_check.Value)), i + 1, 1) & ";"

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
        If DataUtil.ConvertToString(num_strArr) <> "" Then
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
                        Pic_ch5_exhibi.Visible = True
                        txtTemp_back_country.Text = "THAILAND"
                    Case "4"
                        Pic_ch6_demin.Visible = True
                        txtTemp_back_country.Text = "THAILAND"
                    Case Else
                        txtTemp_back_country.Text = "THAILAND"
                End Select
            Next
        Else
            txtTemp_back_country.Text = "THAILAND"
        End If


    End Sub
    '�������
    Sub check_show_check_NoUsd()

        Select Case checkNull_Show(DataUtil.ConvertToString(txtshow_check.Value))
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
                'Pic_ch4_par.Visible = True
                'txtTemp_back_country.Text = "THAILAND"
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
        'If DataUtil.ConvertToString(txtgross_weight.Text) <> "" Then
        '    txtgross_weight1.Text = txtTemp_Gross_Weight.Text + " " + txtg_Unit_Desc.Text
        'Else
        '    txtgross_weight1.Text = ""
        'End If
        '============================================================
        'CallInvoiceCheck()

        Select Case DataUtil.ConvertToString(txtCheckGrossDetail.Text)
            Case "GWDetail"
                txtGrossTxt.Text = CHeck_DisPlay(DataUtil.ConvertToString(C_TotalRowDe.Text), DataUtil.ConvertToString(txtCheckGrossDetail.Text))
        End Select
        '============================================================
        'If txtNumRowCount.Text = C_TotalRowDe.Text Then

        If (DataUtil.ConvertToString(txtquantity1.Text) <> "" And CommonUtility.Get_Decimal(txtquantity1.Text) <> 0 And DataUtil.ConvertToString(txtq_unit_code1.Text) <> "") = True And
        (DataUtil.ConvertToString(txtquantity2.Text) <> "" And CommonUtility.Get_Decimal(txtquantity2.Text) <> 0 And DataUtil.ConvertToString(txtq_unit_code2.Text) <> "") = True And
        (DataUtil.ConvertToString(txtquantity3.Text) <> "" And CommonUtility.Get_Decimal(txtquantity3.Text) <> 0 And DataUtil.ConvertToString(txtq_unit_code3.Text) <> "") = True And
        (DataUtil.ConvertToString(txtquantity4.Text) <> "" And CommonUtility.Get_Decimal(txtquantity4.Text) <> 0 And DataUtil.ConvertToString(txtq_unit_code4.Text) <> "") = True And
        (DataUtil.ConvertToString(txtquantity5.Text) <> "" And CommonUtility.Get_Decimal(txtquantity5.Text) <> 0 And DataUtil.ConvertToString(txtq_unit_code5.Text) <> "") = True Then
            '�����
            All_CheckThird_5(Cpage)

        ElseIf (DataUtil.ConvertToString(txtquantity1.Text) <> "" And CommonUtility.Get_Decimal(txtquantity1.Text) <> 0 And DataUtil.ConvertToString(txtq_unit_code1.Text) <> "") = True And
        (DataUtil.ConvertToString(txtquantity2.Text) <> "" And CommonUtility.Get_Decimal(txtquantity2.Text) <> 0 And DataUtil.ConvertToString(txtq_unit_code2.Text) <> "") = True And
        (DataUtil.ConvertToString(txtquantity3.Text) <> "" And CommonUtility.Get_Decimal(txtquantity3.Text) <> 0 And DataUtil.ConvertToString(txtq_unit_code3.Text) <> "") = True And
        (DataUtil.ConvertToString(txtquantity4.Text) <> "" And CommonUtility.Get_Decimal(txtquantity4.Text) <> 0 And DataUtil.ConvertToString(txtq_unit_code4.Text) <> "") = True And
        (DataUtil.ConvertToString(txtquantity5.Text) <> "" And CommonUtility.Get_Decimal(txtquantity5.Text) <> 0 And DataUtil.ConvertToString(txtq_unit_code5.Text) <> "") = False Then
            '�Ҵ 5
            All_CheckThird_4(Cpage)

        ElseIf (DataUtil.ConvertToString(txtquantity1.Text) <> "" And CommonUtility.Get_Decimal(txtquantity1.Text) <> 0 And DataUtil.ConvertToString(txtq_unit_code1.Text) <> "") = True And
         (DataUtil.ConvertToString(txtquantity2.Text) <> "" And CommonUtility.Get_Decimal(txtquantity2.Text) <> 0 And DataUtil.ConvertToString(txtq_unit_code2.Text) <> "") = True And
         (DataUtil.ConvertToString(txtquantity3.Text) <> "" And CommonUtility.Get_Decimal(txtquantity3.Text) <> 0 And DataUtil.ConvertToString(txtq_unit_code3.Text) <> "") = True And
         (DataUtil.ConvertToString(txtquantity4.Text) <> "" And CommonUtility.Get_Decimal(txtquantity4.Text) <> 0 And DataUtil.ConvertToString(txtq_unit_code4.Text) <> "") = False And
         (DataUtil.ConvertToString(txtquantity5.Text) <> "" And CommonUtility.Get_Decimal(txtquantity5.Text) <> 0 And DataUtil.ConvertToString(txtq_unit_code5.Text) <> "") = False Then
            '�Ҵ 4,5
            All_CheckThird_3(Cpage)

        ElseIf (DataUtil.ConvertToString(txtquantity1.Text) <> "" And CommonUtility.Get_Decimal(txtquantity1.Text) <> 0 And DataUtil.ConvertToString(txtq_unit_code1.Text) <> "") = True And
          (DataUtil.ConvertToString(txtquantity2.Text) <> "" And CommonUtility.Get_Decimal(txtquantity2.Text) <> 0 And DataUtil.ConvertToString(txtq_unit_code2.Text) <> "") = True And
          (DataUtil.ConvertToString(txtquantity3.Text) <> "" And CommonUtility.Get_Decimal(txtquantity3.Text) <> 0 And DataUtil.ConvertToString(txtq_unit_code3.Text) <> "") = False And
          (DataUtil.ConvertToString(txtquantity4.Text) <> "" And CommonUtility.Get_Decimal(txtquantity4.Text) <> 0 And DataUtil.ConvertToString(txtq_unit_code4.Text) <> "") = False And
          (DataUtil.ConvertToString(txtquantity5.Text) <> "" And CommonUtility.Get_Decimal(txtquantity5.Text) <> 0 And DataUtil.ConvertToString(txtq_unit_code5.Text) <> "") = False Then
            '�Ҵ 3,4,5
            All_CheckThird_2(Cpage)

        ElseIf (DataUtil.ConvertToString(txtquantity1.Text) <> "" And CommonUtility.Get_Decimal(txtquantity1.Text) <> 0 And DataUtil.ConvertToString(txtq_unit_code1.Text) <> "") = True And
          (DataUtil.ConvertToString(txtquantity2.Text) <> "" And CommonUtility.Get_Decimal(txtquantity2.Text) <> 0 And DataUtil.ConvertToString(txtq_unit_code2.Text) <> "") = False And
          (DataUtil.ConvertToString(txtquantity3.Text) <> "" And CommonUtility.Get_Decimal(txtquantity3.Text) <> 0 And DataUtil.ConvertToString(txtq_unit_code3.Text) <> "") = False And
          (DataUtil.ConvertToString(txtquantity4.Text) <> "" And CommonUtility.Get_Decimal(txtquantity4.Text) <> 0 And DataUtil.ConvertToString(txtq_unit_code4.Text) <> "") = False And
          (DataUtil.ConvertToString(txtquantity5.Text) <> "" And CommonUtility.Get_Decimal(txtquantity5.Text) <> 0 And DataUtil.ConvertToString(txtq_unit_code5.Text) <> "") = False Then
            '�Ҵ 2,3,4,5
            All_CheckThird_1(Cpage)

        Else
            All_CheckThird_1(Cpage)

        End If

    End Sub

    Dim numCheck As Integer = 1
    Dim str_mark As String

    'by rut Title New Begin---------------
    Dim Str_TitleDe As String
    Dim Str_TitleDe2 As String
    'by rut Title New End-----------------

    'by yut
    Dim noRec As Integer = 0
    Dim isNewPage As Boolean = True

    Private TempInvoice As String = ""
    Private C_CurrentPageNo As Integer = 0
    Private Sub Detail1_Format(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Detail1.Format
        Cpage += 1
        '====================================
        TGross += Check_Null(txtgross_weightD.Value)
        TNet += Check_Null(txtgross_weightD.Value) '//txtnet_weight.Value
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

        Count_RowToPage_DetailData()

        txtNumRowCount.Text = Cpage
        Check_box8()

        If Cpage = 1 Then
            str_mark = DataUtil.ConvertToString(txtmarks.Text)
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

        'by rut rvc
        Check_CaseRVCCount = CInt(txtCheck_CaseRVCCount.Text)

        ''//�Դ/�Դ Invoice ��ͧ 10 ���� Group ��� Invoice
        'If TempInvoice <> txtvoince.Text Then
        '    txtvoince.Visible = True
        '    TempInvoice = txtvoince.Text
        'Else
        '    txtvoince.Visible = False
        'End If

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

        Dim f As New System.Drawing.Font("BrowalliaUPC", 10)

        '==========================
        Dim xm As Single = 0.7
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
            .DrawText(DataUtil.ConvertToString(str_mark), drawRectm)
        End With
        '==========================
        Select Case DataUtil.ConvertToString(txtCheckGrossDetail.Text)
            Case Is <> "GWDetail"
                Dim x_GrossDetail As Single = 6
                Dim y_GrossDetail As Single = CSng(head_height) '4.63
                'Dim width As Single = 6.0F
                Dim width_GrossDetail As Single = 0.6
                Dim height_GrossDetail As Single = 3.1
                Dim drawRect_GrossDetail As New Drawing.RectangleF(x_GrossDetail, y_GrossDetail, width_GrossDetail, height_GrossDetail)

                With Me.CurrentPage
                    .Font = f
                    .ForeColor = System.Drawing.Color.Black
                    .TextAlignment = DataDynamics.ActiveReports.TextAlignment.Center
                    .VerticalTextAlignment = VerticalTextAlignment.Top
                    '.BackColor = Drawing.Color.Green
                    '.TextAngle = 900
                    '.VerticalTextAlignment = DataDynamics.ActiveReports.VerticalTextAlignment.Middle
                    .DrawText(CHeck_DisPlay(DataUtil.ConvertToString(C_TotalRowDe.Text), DataUtil.ConvertToString(txtCheckGrossDetail.Text)), drawRect_GrossDetail)
                End With
        End Select
    End Sub

    Private Sub rpt3_ediFORM4_pr_PageStart(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PageStart

        'by rut edit ���
        Dim detail_line_start As Double = 3.815
        Dim detail_line_end As Double = 7.785

        Me.CurrentPage.DrawLine(0.188, detail_line_start, 0.188, detail_line_end)
        Me.CurrentPage.DrawLine(0.63, detail_line_start, 0.63, detail_line_end)
        Me.CurrentPage.DrawLine(1.648, detail_line_start, 1.648, detail_line_end)
        Me.CurrentPage.DrawLine(5.28, detail_line_start, 5.28, detail_line_end)
        Me.CurrentPage.DrawLine(6, detail_line_start, 6, detail_line_end)
        Me.CurrentPage.DrawLine(7.15, detail_line_start, 7.15, detail_line_end)
        Me.CurrentPage.DrawLine(8.062, detail_line_start, 8.062, detail_line_end)

    End Sub
    Function FLine_SizeSetting(ByVal By_form As String, ByVal By_Headline As Single, ByVal By_FootLine As Single, ByVal By_TempLineX As Single) As Single
        Dim THF As Single = 0.0

        THF = By_TempLineX + (12 - (By_Headline + By_FootLine))

        Return THF
    End Function
    Private Sub rpt3_ediFORM4_pr_ReportEnd(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.ReportEnd
        'If updatePrintTotal(txtinvh_run_auto.Value, CStr(Me.Document.Pages.Count)) = True Then

        'End If

        Me.Document.Printer.PrinterSettings.Copies = 2
    End Sub

    Private Sub GroupFooter1_Format(sender As Object, e As EventArgs) Handles GroupFooter1.Format

    End Sub

    Private Sub rpt3_ediFORM4_pr_ReportStart(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.ReportStart
        'Me.ReportInfo1.FormatString = "Page {PageNumber} of {PageCount} on {RunDateTime}"
        Me.ReportInfo1.FormatString = "Page : {PageNumber} of {PageCount}"

        'by yut
        noRec = 0
        isNewPage = True

    End Sub

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        Me.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.Custom
        Me.PageSettings.PaperName = "Fanfold 210 x 305 mm"
        ' Add any initialization after the InitializeComponent() call.

    End Sub

#Region "Code sign image"
    'by rut sign image � report
    '�� reports_CardID,search_imageForm ����� Module_CallBathEng
    Sub CaseCheck_numimagesSign(ByVal _numValue As DataSet)
        Try
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
        Catch ex As Exception

        End Try

    End Sub
    Sub CaseCheck_ApproveSign(ByVal _numValue As DataSet)
        Try
            Dim arr_ As Array

            Dim num_return As Integer = 0
            If _numValue.Tables(0).Rows.Count > 0 Then
                With _numValue.Tables(0).Rows(0)
                    If ConvertToString(.Item("SignImage_ApproveID").ToString) <> "" And ConvertToString(.Item("im_CheckCaseSave").ToString) = "1" Then
                        arr_ = .Item("SignImage_ApproveID").ToString.Split(";")
                    Else
                        Exit Sub
                    End If
                End With
            Else
                Exit Sub
            End If

            PictureApproveSign.Visible = True

            'Picture_SealAuthor.SizeMode = SizeModes.Stretch
            PictureApproveSign.Image = New Drawing.Bitmap(reports_Approveid(arr_(0).ToString))

            Select Case txtTemp_SiteSend.Text.ToUpper
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
            txtTemp_Date.Text = String_DateSiteReport(txtprintFormDate.Value)

        Catch ex As Exception

        End Try
    End Sub
#End Region

    Private Function ConvertToString(obj As Object) As String
        Dim ret As String = ""
        Try
            ret = Convert.ToString(obj)
        Catch ex As Exception

        End Try

        Return ret
    End Function

    Public Sub load_qrcode(ByVal _RefNo As String)
        Try
            Dim RefNo As String = System.Web.HttpUtility.UrlEncode(_RefNo)
            Dim url_tmp As String = ConfigurationManager.AppSettings("url_qrcode").ToString()
            Picture1.Visible = True

            Dim url As String = System.Web.HttpUtility.UrlDecode(url_tmp) + RefNo

            Dim client As System.Net.WebClient = New System.Net.WebClient()
            Dim data As System.IO.Stream = client.OpenRead(url)
            Dim reader As System.IO.StreamReader = New System.IO.StreamReader(data)

            Picture1.Image = System.Drawing.Image.FromStream(reader.BaseStream())

        Catch ex As Exception

        End Try
    End Sub


End Class
