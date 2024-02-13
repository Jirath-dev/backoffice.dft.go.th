Imports DataDynamics.ActiveReports
Imports DataDynamics.ActiveReports.Document

Imports Microsoft.ApplicationBlocks.Data
Imports System.Data.SqlClient
Imports System.Data
Imports System.Text
Imports DFT.Dotnetnuke.ClassLibrary
Imports ReportPrintClass
Public Class rpt3_ediFORM4_9_pr
    Dim Cpage As Integer
    Dim CpageNum As Integer
    Public _TempB2B As String

    'by rut Title New Begin-----------------
    Dim Str_TitlePage As String
    'by rut Title New End-------------------
    'by rut ��Ѻ page end ���͡�˹�
    Dim head_height As String
    Private Sub PageHeader1_Format(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PageHeader1.Format
        CpageNum += 1

        'txtreference_code2_Temp.Text = "I" & txtreference_code2.Value

        ''ByTine 01-01-2560
        txtreference_code2_Temp.Text = txtreference_code2.Text
        '===============================================================

        '===============================================================
        Select Case CommonUtility.Get_StringValue(txtCheck_StatusWeb.Text)
            Case Is <> ""
                Head_Checkcompany_v1()
                Head_Checkdestination_v2()
            Case Is = "" '�к����
                Head_Checkcompany_v1_Old()
                Head_Checkdestination_v2_Old()
        End Select

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
        check_show_check()
        'check_Issued_()
        ' txtcompany_provincefoot.Text = txtcompany_province.Value & " " & format_dateSelect()

    End Sub
    Dim TNet As Decimal
    Dim TFOB As Decimal
    Dim str_mark As String

    'by rut Title New Begin---------------
    Dim Str_TitleDe As String
    Dim Str_TitleDe2 As String
    'by rut Title New End-----------------
    Private Sub Detail1_Format(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Detail1.Format
        Cpage += 1
        TNet += txtnet_weight.Value
        TFOB += txtFOB_AMT.Value

        Count_RowToPage_DetailData()

        txtNumRowCount.Text = Cpage
        Check_box8()

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

    Sub Head_Checkcompany_v1_Old() '�к����
        If Mid(txtdest_remark.Text, 1, 3) = "C/O" Then
            txtCompany_Check_1.Text = txtob_address.Text & " CARE OF " & txtcompany_name.Text & " " & txtcompany_address.Text _
                               & " " & txtcompany_province.Text & " " & txtcompany_country.Text & _
                               IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") & _
                               IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") & _
                               IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID:  " & txtcompany_taxno.Text, "") & _
                               IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdeclare_doc.Text)) <> "", " E-mail: " & txtdeclare_doc.Text, "")
        ElseIf Mid(txtdest_remark.Text, 1, 3) = "O/B" Then
            txtCompany_Check_1.Text = txtcompany_name.Text & " " & txtcompany_address.Text & " " & txtcompany_province.Text _
                                & " " & txtcompany_country.Text & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") & _
                                IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") _
                                & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID: " & txtcompany_taxno.Text, "") & _
                                IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdeclare_doc.Text)) <> "", " E-mail: " & txtdeclare_doc.Text, "") & _
                                IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtob_address.Text)) <> "", " ON BEHALF OF " & txtob_address.Text, "")
        ElseIf Mid(txtdest_remark.Text, 2, 1) <> "/" Then
            txtCompany_Check_1.Text = txtcompany_name.Text & " " & txtcompany_address.Text & " " & txtcompany_province.Text _
                                & " " & txtcompany_country.Text & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") & _
                                IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") _
                                & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID: " & txtcompany_taxno.Text, "") & _
                                IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdeclare_doc.Text)) <> "", " E-mail: " & txtdeclare_doc.Text, "")

        ElseIf Mid(txtdest_remark.Text, 1, 5) <> "O/TOB" Then
            txtCompany_Check_1.Text = txtcompany_name.Text & " " & txtcompany_address.Text & " " & txtcompany_province.Text _
                            & " " & txtcompany_country.Text & " " & txtob_address.Text & _
                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") & _
                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") _
                            & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID: " & txtcompany_taxno.Text, "") & _
                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdeclare_doc.Text)) <> "", " E-mail: " & txtdeclare_doc.Text, "")

        ElseIf Mid(txtdest_remark.Text, 1, 5) <> "O/TCO" Then
            txtCompany_Check_1.Text = txtob_address.Text & txtcompany_name.Text & " " & txtcompany_address.Text & " " & txtcompany_province.Text _
                            & " " & txtcompany_country.Text & _
                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") & _
                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") _
                            & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID: " & txtcompany_taxno.Text, "") & _
                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdeclare_doc.Text)) <> "", " E-mail: " & txtdeclare_doc.Text, "")

        End If
    End Sub

    Sub Head_Checkdestination_v2_Old()
        If Mid(txtdest_remark1.Text, 1, 3) = "C/O" Then
            txtdestination_Check2.Text = txtob_dest_address.Text & " CARE OF " & txtdestination_company.Text & " " & txtdestination_address.Text _
                                & " " & txtdestination_province.Text & " " & txtdest_Receive_country.Text & _
                                IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") & _
                                IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") & _
                                IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID:  " & txtdestination_taxid.Text, "") & _
                                IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdeclare_Remark.Text)) <> "", " E-mail: " & txtdeclare_Remark.Text, "") & _
                                IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "")

        ElseIf Mid(txtdest_remark1.Text, 1, 3) = "O/B" Then
            txtdestination_Check2.Text = txtdestination_company.Text & " " & txtdestination_address.Text & " " & _
                                    txtdestination_province.Text & " " & txtdest_Receive_country.Text & _
                                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") & _
                                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                    & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") & _
                                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdeclare_Remark.Text)) <> "", " E-mail: " & txtdeclare_Remark.Text, "") & _
                                    " ON BEHALF OF " & txtob_dest_address.Text & IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "")

        ElseIf Mid(txtdest_remark1.Text, 2, 1) <> "/" Then
            txtdestination_Check2.Text = txtdestination_company.Text & " " & txtdestination_address.Text & " " & txtdestination_province.Text _
                                & " " & txtdest_Receive_country.Text & _
                                IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") & _
                                IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") & _
                                IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdeclare_Remark.Text)) <> "", " E-mail: " & txtdeclare_Remark.Text, "") & _
                                IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "")

        ElseIf Mid(txtdest_remark1.Text, 1, 5) <> "O/TOB" Then
            txtdestination_Check2.Text = txtdestination_company.Text & " " & txtdestination_address.Text & " " & txtdestination_province.Text _
                            & " " & txtdest_Receive_country.Text & " " & txtob_dest_address.Text & _
                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") & _
                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                            & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") & _
                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdeclare_Remark.Text)) <> "", " E-mail: " & txtdeclare_Remark.Text, "") & _
                            IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "")

        ElseIf Mid(txtdest_remark1.Text, 1, 5) <> "O/TCO" Then
            txtdestination_Check2.Text = txtob_dest_address.Text & " " & txtdestination_company.Text & " " & txtdestination_address.Text & " " & txtdestination_province.Text _
                            & " " & txtdest_Receive_country.Text & _
                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") & _
                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                            & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") & _
                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdeclare_Remark.Text)) <> "", " E-mail: " & txtdeclare_Remark.Text, "") & _
                            IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "")

        End If
    End Sub

    Sub comCheck()
        If (Len(CommonUtility.Get_StringValue(txtcompany_phone.Value)) <> 0) And (Len(CommonUtility.Get_StringValue(txtcompany_fax.Value)) <> 0) And (Len(CommonUtility.Get_StringValue(txtcompany_taxno.Value)) <> 0) And (Mid(CommonUtility.Get_StringValue(txtdest_remark.Value), 1, 3)) = "C/O" Then

            txtCompany_Check_1.Text = (Mid(CommonUtility.Get_StringValue(txtob_address.Value), 1, 255)) & " CARE OF " & CommonUtility.Get_StringValue(txtcompany_name.Value) & " " & CommonUtility.Get_StringValue(txtcompany_address.Value) & " " & CommonUtility.Get_StringValue(txtcompany_province.Value) & " " & CommonUtility.Get_StringValue(txtcompany_country.Value) & " TEL: " & CommonUtility.Get_StringValue(txtcompany_phone.Value) & " FAX: " & CommonUtility.Get_StringValue(txtcompany_fax.Value) & " TAX ID:  " & CommonUtility.Get_StringValue(txtcompany_taxno.Value)

        ElseIf (Len(CommonUtility.Get_StringValue(txtcompany_phone.Value)) <> 0) And (Len(CommonUtility.Get_StringValue(txtcompany_fax.Value)) <> 0) And (Len(CommonUtility.Get_StringValue(txtcompany_taxno.Value)) <> 0) And (Mid(CommonUtility.Get_StringValue(txtdest_remark.Value), 1, 3)) = "O/B" Then

            txtCompany_Check_1.Text = (CommonUtility.Get_StringValue(txtcompany_name.Value) & "  " & CommonUtility.Get_StringValue(txtcompany_address.Value) & " " & CommonUtility.Get_StringValue(txtcompany_province.Value) & " " & CommonUtility.Get_StringValue(txtcompany_country.Value) & " TEL: " & CommonUtility.Get_StringValue(txtcompany_phone.Value) & " FAX: " & CommonUtility.Get_StringValue(txtcompany_fax.Value) & " TAX ID:  " & CommonUtility.Get_StringValue(txtcompany_taxno.Value) & " ON BEHALF OF " & Mid(CommonUtility.Get_StringValue(txtob_address.Value), 1, 255))

        ElseIf (Len(CommonUtility.Get_StringValue(txtcompany_phone.Value)) <> 0) And (Len(CommonUtility.Get_StringValue(txtcompany_fax.Value)) <> 0) And (Len(CommonUtility.Get_StringValue(txtcompany_taxno.Value)) <> 0) And (Mid(CommonUtility.Get_StringValue(txtdest_remark.Value), 2, 1)) <> "/" Then

            txtCompany_Check_1.Text = (CommonUtility.Get_StringValue(txtcompany_name.Value) & "  " & CommonUtility.Get_StringValue(txtcompany_address.Value) & " " & CommonUtility.Get_StringValue(txtcompany_province.Value) & " " & CommonUtility.Get_StringValue(txtcompany_country.Value) & " TEL: " & CommonUtility.Get_StringValue(txtcompany_phone.Value) & " FAX: " & CommonUtility.Get_StringValue(txtcompany_fax.Value) & " TAX ID:  " & CommonUtility.Get_StringValue(txtcompany_taxno.Value))

        ElseIf (Len(CommonUtility.Get_StringValue(txtcompany_phone.Value)) <> 0) And (Len(CommonUtility.Get_StringValue(txtcompany_fax.Value)) = 0) And (Len(CommonUtility.Get_StringValue(txtcompany_taxno.Value)) <> 0) And (Mid(CommonUtility.Get_StringValue(txtdest_remark.Value), 1, 3)) = "C/O" Then

            txtCompany_Check_1.Text = (Mid(CommonUtility.Get_StringValue(txtob_address.Value), 1, 255) & " CARE OF " & CommonUtility.Get_StringValue(txtcompany_name.Value) & " " & CommonUtility.Get_StringValue(txtcompany_address.Value) & " " & CommonUtility.Get_StringValue(txtcompany_province.Value) & " " & CommonUtility.Get_StringValue(txtcompany_country.Value) & " TEL: " & CommonUtility.Get_StringValue(txtcompany_phone.Value) & " TAX ID:  " & CommonUtility.Get_StringValue(txtcompany_taxno.Value))

        ElseIf (Len(CommonUtility.Get_StringValue(txtcompany_phone.Value)) <> 0) And (Len(CommonUtility.Get_StringValue(txtcompany_fax.Value)) = 0) And (Len(CommonUtility.Get_StringValue(txtcompany_taxno.Value)) = 0) And (Mid(CommonUtility.Get_StringValue(txtdest_remark.Value), 1, 3)) = "C/O" Then

            txtCompany_Check_1.Text = (Mid(CommonUtility.Get_StringValue(txtob_address.Value), 1, 255) & " CARE OF " & CommonUtility.Get_StringValue(txtcompany_name.Value) & " " & CommonUtility.Get_StringValue(txtcompany_address.Value) & " " & CommonUtility.Get_StringValue(txtcompany_province.Value) & " " & CommonUtility.Get_StringValue(txtcompany_country.Value) & " TEL: " & CommonUtility.Get_StringValue(txtcompany_phone.Value))

        ElseIf (Len(CommonUtility.Get_StringValue(txtcompany_phone.Value)) = 0) And (Len(CommonUtility.Get_StringValue(txtcompany_fax.Value)) <> 0) And (Len(CommonUtility.Get_StringValue(txtcompany_taxno.Value)) = 0) And (Mid(CommonUtility.Get_StringValue(txtdest_remark.Value), 1, 3)) = "C/O" Then

            txtCompany_Check_1.Text = (Mid(CommonUtility.Get_StringValue(txtob_address.Value), 1, 255) & " CARE OF " & CommonUtility.Get_StringValue(txtcompany_name.Value) & " " & CommonUtility.Get_StringValue(txtcompany_address.Value) & " " & CommonUtility.Get_StringValue(txtcompany_province.Value) & " " & CommonUtility.Get_StringValue(txtcompany_country.Value) & " FAX: " & CommonUtility.Get_StringValue(txtcompany_fax.Value))

        ElseIf (Len(CommonUtility.Get_StringValue(txtcompany_phone.Value)) = 0) And (Len(CommonUtility.Get_StringValue(txtcompany_fax.Value)) <> 0) And (Len(CommonUtility.Get_StringValue(txtcompany_taxno.Value)) <> 0) And (Mid(CommonUtility.Get_StringValue(txtdest_remark.Value), 1, 3)) = "C/O" Then

            txtCompany_Check_1.Text = (Mid(CommonUtility.Get_StringValue(txtob_address.Value), 1, 255) & " CARE OF " & CommonUtility.Get_StringValue(txtcompany_name.Value) & " " & CommonUtility.Get_StringValue(txtcompany_address.Value) & " " & CommonUtility.Get_StringValue(txtcompany_province.Value) & " " & CommonUtility.Get_StringValue(txtcompany_country.Value) & " FAX: " & CommonUtility.Get_StringValue(txtcompany_fax.Value) & " TAX ID:  " & CommonUtility.Get_StringValue(txtcompany_taxno.Value))

        ElseIf (Len(CommonUtility.Get_StringValue(txtcompany_phone.Value)) <> 0) And (Len(CommonUtility.Get_StringValue(txtcompany_fax.Value)) = 0) And (Len(CommonUtility.Get_StringValue(txtcompany_taxno.Value)) <> 0) And (Mid(CommonUtility.Get_StringValue(txtdest_remark.Value), 1, 3)) = "O/B" Then

            txtCompany_Check_1.Text = (CommonUtility.Get_StringValue(txtcompany_name.Value) & "  " & CommonUtility.Get_StringValue(txtcompany_address.Value) & " " & CommonUtility.Get_StringValue(txtcompany_province.Value) & " " & CommonUtility.Get_StringValue(txtcompany_country.Value) & " TEL: " & CommonUtility.Get_StringValue(txtcompany_phone.Value) & " TAX ID:  " & CommonUtility.Get_StringValue(txtcompany_taxno.Value) & " ON BEHALF OF " & Mid(CommonUtility.Get_StringValue(txtob_address.Value), 1, 255))

        ElseIf (Len(CommonUtility.Get_StringValue(txtcompany_phone.Value)) <> 0) And (Len(CommonUtility.Get_StringValue(txtcompany_fax.Value)) = 0) And (Len(CommonUtility.Get_StringValue(txtcompany_taxno.Value)) = 0) And (Mid(CommonUtility.Get_StringValue(txtdest_remark.Value), 1, 3)) = "O/B" Then

            txtCompany_Check_1.Text = (CommonUtility.Get_StringValue(txtcompany_name.Value) & "  " & CommonUtility.Get_StringValue(txtcompany_address.Value) & " " & CommonUtility.Get_StringValue(txtcompany_province.Value) & " " & CommonUtility.Get_StringValue(txtcompany_country.Value) & " TEL: " & CommonUtility.Get_StringValue(txtcompany_phone.Value) & " ON BEHALF OF " & Mid(CommonUtility.Get_StringValue(txtob_address.Value), 1, 255))

        ElseIf (Len(CommonUtility.Get_StringValue(txtcompany_phone.Value)) = 0) And (Len(CommonUtility.Get_StringValue(txtcompany_fax.Value)) <> 0) And (Len(CommonUtility.Get_StringValue(txtcompany_taxno.Value)) = 0) And (Mid(CommonUtility.Get_StringValue(txtdest_remark.Value), 1, 3)) = "O/B" Then

            txtCompany_Check_1.Text = (CommonUtility.Get_StringValue(txtcompany_name.Value) & "  " & CommonUtility.Get_StringValue(txtcompany_address.Value) & " " & CommonUtility.Get_StringValue(txtcompany_province.Value) & " " & CommonUtility.Get_StringValue(txtcompany_country.Value) & " FAX: " & CommonUtility.Get_StringValue(txtcompany_fax.Value) & " ON BEHALF OF " & Mid(CommonUtility.Get_StringValue(txtob_address.Value), 1, 255))

        ElseIf (Len(CommonUtility.Get_StringValue(txtcompany_phone.Value)) = 0) And (Len(CommonUtility.Get_StringValue(txtcompany_fax.Value)) <> 0) And (Len(CommonUtility.Get_StringValue(txtcompany_taxno.Value)) <> 0) And (Mid(CommonUtility.Get_StringValue(txtdest_remark.Value), 1, 3)) = "O/B" Then

            txtCompany_Check_1.Text = (CommonUtility.Get_StringValue(txtcompany_name.Value) & "  " & CommonUtility.Get_StringValue(txtcompany_address.Value) & " " & CommonUtility.Get_StringValue(txtcompany_province.Value) & " " & CommonUtility.Get_StringValue(txtcompany_country.Value) & " FAX: " & CommonUtility.Get_StringValue(txtcompany_fax.Value) & " TAX ID:  " & CommonUtility.Get_StringValue(txtcompany_taxno.Value) & " ON BEHALF OF " & Mid(CommonUtility.Get_StringValue(txtob_address.Value), 1, 255))

        ElseIf (Len(CommonUtility.Get_StringValue(txtcompany_phone.Value)) <> 0) And (Len(CommonUtility.Get_StringValue(txtcompany_fax.Value)) = 0) And (Len(CommonUtility.Get_StringValue(txtcompany_taxno.Value)) <> 0) And (Mid(CommonUtility.Get_StringValue(txtdest_remark.Value), 2, 1)) <> "/" Then

            txtCompany_Check_1.Text = (CommonUtility.Get_StringValue(txtcompany_name.Value) & "  " & CommonUtility.Get_StringValue(txtcompany_address.Value) & " " & CommonUtility.Get_StringValue(txtcompany_province.Value) & " " & CommonUtility.Get_StringValue(txtcompany_country.Value) & " TEL: " & CommonUtility.Get_StringValue(txtcompany_phone.Value) & " TAX ID:  " & CommonUtility.Get_StringValue(txtcompany_taxno.Value))

        ElseIf (Len(CommonUtility.Get_StringValue(txtcompany_phone.Value)) <> 0) And (Len(CommonUtility.Get_StringValue(txtcompany_fax.Value)) = 0) And (Len(CommonUtility.Get_StringValue(txtcompany_taxno.Value)) = 0) And (Mid(CommonUtility.Get_StringValue(txtdest_remark.Value), 2, 1)) <> "/" Then

            txtCompany_Check_1.Text = (CommonUtility.Get_StringValue(txtcompany_name.Value) & "  " & CommonUtility.Get_StringValue(txtcompany_address.Value) & " " & CommonUtility.Get_StringValue(txtcompany_province.Value) & " " & CommonUtility.Get_StringValue(txtcompany_country.Value) & " TEL: " & CommonUtility.Get_StringValue(txtcompany_phone.Value))

        ElseIf (Len(CommonUtility.Get_StringValue(txtcompany_phone.Value)) = 0) And (Len(CommonUtility.Get_StringValue(txtcompany_fax.Value)) <> 0) And (Len(CommonUtility.Get_StringValue(txtcompany_taxno.Value)) = 0) And (Mid(CommonUtility.Get_StringValue(txtdest_remark.Value), 2, 1)) <> "/" Then

            txtCompany_Check_1.Text = (CommonUtility.Get_StringValue(txtcompany_name.Value) & "  " & CommonUtility.Get_StringValue(txtcompany_address.Value) & " " & CommonUtility.Get_StringValue(txtcompany_province.Value) & " " & CommonUtility.Get_StringValue(txtcompany_country.Value) & " FAX: " & CommonUtility.Get_StringValue(txtcompany_fax.Value))

        ElseIf (Len(CommonUtility.Get_StringValue(txtcompany_phone.Value)) = 0) And (Len(CommonUtility.Get_StringValue(txtcompany_fax.Value)) <> 0) And (Len(CommonUtility.Get_StringValue(txtcompany_taxno.Value)) <> 0) And (Mid(CommonUtility.Get_StringValue(txtdest_remark.Value), 2, 1)) <> "/" Then

            txtCompany_Check_1.Text = (CommonUtility.Get_StringValue(txtcompany_name.Value) & "  " & CommonUtility.Get_StringValue(txtcompany_address.Value) & " " & CommonUtility.Get_StringValue(txtcompany_province.Value) & " " & CommonUtility.Get_StringValue(txtcompany_country.Value) & " FAX: " & CommonUtility.Get_StringValue(txtcompany_fax.Value) & " TAX ID:  " & CommonUtility.Get_StringValue(txtcompany_taxno.Value))

        ElseIf (Len(CommonUtility.Get_StringValue(txtcompany_phone.Value)) = 0) And (Len(CommonUtility.Get_StringValue(txtcompany_fax.Value)) = 0) And (Len(CommonUtility.Get_StringValue(txtcompany_taxno.Value)) <> 0) And (Mid(CommonUtility.Get_StringValue(txtdest_remark.Value), 1, 3)) = "C/O" Then

            txtCompany_Check_1.Text = (Mid(CommonUtility.Get_StringValue(txtob_address.Value), 1, 255) & " CARE OF " & CommonUtility.Get_StringValue(txtcompany_name.Value) & " " & CommonUtility.Get_StringValue(txtcompany_address.Value) & " " & CommonUtility.Get_StringValue(txtcompany_province.Value) & " " & CommonUtility.Get_StringValue(txtcompany_country.Value) & " TAX ID:  " & CommonUtility.Get_StringValue(txtcompany_taxno.Value))

        ElseIf (Len(CommonUtility.Get_StringValue(txtcompany_phone.Value)) = 0) And (Len(CommonUtility.Get_StringValue(txtcompany_fax.Value)) = 0) And (Len(CommonUtility.Get_StringValue(txtcompany_taxno.Value)) = 0) And (Mid(CommonUtility.Get_StringValue(txtdest_remark.Value), 1, 3)) = "C/O" Then

            txtCompany_Check_1.Text = (Mid(CommonUtility.Get_StringValue(txtob_address.Value), 1, 255) & " CARE OF " & CommonUtility.Get_StringValue(txtcompany_name.Value) & " " & CommonUtility.Get_StringValue(txtcompany_address.Value) & " " & CommonUtility.Get_StringValue(txtcompany_province.Value) & " " & CommonUtility.Get_StringValue(txtcompany_country.Value))

        ElseIf (Len(CommonUtility.Get_StringValue(txtcompany_phone.Value)) = 0) And (Len(CommonUtility.Get_StringValue(txtcompany_fax.Value)) = 0) And (Len(CommonUtility.Get_StringValue(txtcompany_taxno.Value)) <> 0) And (Mid(CommonUtility.Get_StringValue(txtdest_remark.Value), 1, 3)) = "O/B" Then

            txtCompany_Check_1.Text = (CommonUtility.Get_StringValue(txtcompany_name.Value) & "  " & CommonUtility.Get_StringValue(txtcompany_address.Value) & " " & CommonUtility.Get_StringValue(txtcompany_province.Value) & " " & CommonUtility.Get_StringValue(txtcompany_country.Value) & " TAX ID:  " & CommonUtility.Get_StringValue(txtcompany_taxno.Value) & " ON BEHALF OF " & Mid(CommonUtility.Get_StringValue(txtob_address.Value), 1, 255))

        ElseIf (Len(CommonUtility.Get_StringValue(txtcompany_phone.Value)) = 0) And (Len(CommonUtility.Get_StringValue(txtcompany_fax.Value)) = 0) And (Len(CommonUtility.Get_StringValue(txtcompany_taxno.Value)) = 0) And (Mid(CommonUtility.Get_StringValue(txtdest_remark.Value), 1, 3)) = "O/B" Then

            txtCompany_Check_1.Text = (CommonUtility.Get_StringValue(txtcompany_name.Value) & "  " & CommonUtility.Get_StringValue(txtcompany_address.Value) & " " & CommonUtility.Get_StringValue(txtcompany_province.Value) & " " & CommonUtility.Get_StringValue(txtcompany_country.Value) & " ON BEHALF OF " & Mid(CommonUtility.Get_StringValue(txtob_address.Value), 1, 255))

        ElseIf (Len(CommonUtility.Get_StringValue(txtcompany_phone.Value)) = 0) And (Len(CommonUtility.Get_StringValue(txtcompany_fax.Value)) = 0) And (Len(CommonUtility.Get_StringValue(txtcompany_taxno.Value)) <> 0) And (Mid(CommonUtility.Get_StringValue(txtdest_remark.Value), 2, 1)) <> "/" Then

            txtCompany_Check_1.Text = (CommonUtility.Get_StringValue(txtcompany_name.Value) & "  " & CommonUtility.Get_StringValue(txtcompany_address.Value) & " " & CommonUtility.Get_StringValue(txtcompany_province.Value) & " " & CommonUtility.Get_StringValue(txtcompany_country.Value) & " TAX ID:  " & CommonUtility.Get_StringValue(txtcompany_taxno.Value))

        ElseIf (Len(CommonUtility.Get_StringValue(txtcompany_phone.Value)) = 0) And (Len(CommonUtility.Get_StringValue(txtcompany_fax.Value)) = 0) And (Len(CommonUtility.Get_StringValue(txtcompany_taxno.Value)) = 0) And (Mid(CommonUtility.Get_StringValue(txtdest_remark.Value), 2, 1)) <> "/" Then

            txtCompany_Check_1.Text = (CommonUtility.Get_StringValue(txtcompany_name.Value) & "  " & CommonUtility.Get_StringValue(txtcompany_address.Value) & " " & CommonUtility.Get_StringValue(txtcompany_province.Value) & " " & CommonUtility.Get_StringValue(txtcompany_country.Value))

        ElseIf (Len(CommonUtility.Get_StringValue(txtcompany_phone.Value)) <> 0) And (Len(CommonUtility.Get_StringValue(txtcompany_fax.Value)) <> 0) And (Len(CommonUtility.Get_StringValue(txtcompany_taxno.Value)) <> 0) And (Mid(CommonUtility.Get_StringValue(txtdest_remark.Value), 1, 5) = "O/TCO") Then

            txtCompany_Check_1.Text = (Mid(CommonUtility.Get_StringValue(txtob_address.Value), 1, 255) & " " & CommonUtility.Get_StringValue(txtcompany_name.Value) & " " & CommonUtility.Get_StringValue(txtcompany_address.Value) & " " & CommonUtility.Get_StringValue(txtcompany_province.Value) & " " & CommonUtility.Get_StringValue(txtcompany_country.Value) & " TEL: " & CommonUtility.Get_StringValue(txtcompany_phone.Value) & " FAX: " & CommonUtility.Get_StringValue(txtcompany_fax.Value) & " TAX ID:  " & CommonUtility.Get_StringValue(txtcompany_taxno.Value))

        ElseIf (Len(CommonUtility.Get_StringValue(txtcompany_phone.Value)) <> 0) And (Len(CommonUtility.Get_StringValue(txtcompany_fax.Value)) <> 0) And (Len(CommonUtility.Get_StringValue(txtcompany_taxno.Value)) <> 0) And (Mid(CommonUtility.Get_StringValue(txtdest_remark.Value), 1, 5) = "O/TOB") Then

            txtCompany_Check_1.Text = (CommonUtility.Get_StringValue(txtcompany_name.Value) & " " & CommonUtility.Get_StringValue(txtcompany_address.Value) & " " & CommonUtility.Get_StringValue(txtcompany_province.Value) & " " & CommonUtility.Get_StringValue(txtcompany_country.Value) & " " & Mid(CommonUtility.Get_StringValue(txtob_address.Value), 1, 255) & " TEL: " & CommonUtility.Get_StringValue(txtcompany_phone.Value) & " FAX: " & CommonUtility.Get_StringValue(txtcompany_fax.Value) & " TAX ID:  " & CommonUtility.Get_StringValue(txtcompany_taxno.Value))

        ElseIf (Len(CommonUtility.Get_StringValue(txtcompany_phone.Value)) <> 0) And (Len(CommonUtility.Get_StringValue(txtcompany_fax.Value)) = 0) And (Len(CommonUtility.Get_StringValue(txtcompany_taxno.Value)) <> 0) And (Mid(CommonUtility.Get_StringValue(txtdest_remark.Value), 1, 5) = "O/TCO") Then

            txtCompany_Check_1.Text = (Mid(CommonUtility.Get_StringValue(txtob_address.Value), 1, 255) & " " & CommonUtility.Get_StringValue(txtcompany_name.Value) & " " & CommonUtility.Get_StringValue(txtcompany_address.Value) & " " & CommonUtility.Get_StringValue(txtcompany_province.Value) & " " & CommonUtility.Get_StringValue(txtcompany_country.Value) & " TEL: " & CommonUtility.Get_StringValue(txtcompany_phone.Value) & " TAX ID:  " & CommonUtility.Get_StringValue(txtcompany_taxno.Value))

        ElseIf (Len(CommonUtility.Get_StringValue(txtcompany_phone.Value)) <> 0) And (Len(CommonUtility.Get_StringValue(txtcompany_fax.Value)) = 0) And (Len(CommonUtility.Get_StringValue(txtcompany_taxno.Value)) = 0) And (Mid(CommonUtility.Get_StringValue(txtdest_remark.Value), 1, 5) = "O/TCO") Then

            txtCompany_Check_1.Text = (Mid(CommonUtility.Get_StringValue(txtob_address.Value), 1, 255) & " " & CommonUtility.Get_StringValue(txtcompany_name.Value) & " " & CommonUtility.Get_StringValue(txtcompany_address.Value) & " " & CommonUtility.Get_StringValue(txtcompany_province.Value) & " " & CommonUtility.Get_StringValue(txtcompany_country.Value) & " TEL: " & CommonUtility.Get_StringValue(txtcompany_phone.Value))

        ElseIf (Len(CommonUtility.Get_StringValue(txtcompany_phone.Value)) = 0) And (Len(CommonUtility.Get_StringValue(txtcompany_fax.Value)) <> 0) And (Len(CommonUtility.Get_StringValue(txtcompany_taxno.Value)) = 0) And (Mid(CommonUtility.Get_StringValue(txtdest_remark.Value), 1, 5) = "O/TCO") Then

            txtCompany_Check_1.Text = (Mid(CommonUtility.Get_StringValue(txtob_address.Value), 1, 255) & " " & CommonUtility.Get_StringValue(txtcompany_name.Value) & " " & CommonUtility.Get_StringValue(txtcompany_address.Value) & " " & CommonUtility.Get_StringValue(txtcompany_province.Value) & " " & CommonUtility.Get_StringValue(txtcompany_country.Value) & " FAX: " & CommonUtility.Get_StringValue(txtcompany_fax.Value))

        ElseIf (Len(CommonUtility.Get_StringValue(txtcompany_phone.Value)) = 0) And (Len(CommonUtility.Get_StringValue(txtcompany_fax.Value)) <> 0) And (Len(CommonUtility.Get_StringValue(txtcompany_taxno.Value)) <> 0) And (Mid(CommonUtility.Get_StringValue(txtdest_remark.Value), 1, 5) = "O/TCO") Then

            txtCompany_Check_1.Text = (Mid(CommonUtility.Get_StringValue(txtob_address.Value), 1, 255) & " " & CommonUtility.Get_StringValue(txtcompany_name.Value) & " " & CommonUtility.Get_StringValue(txtcompany_address.Value) & " " & CommonUtility.Get_StringValue(txtcompany_province.Value) & " " & CommonUtility.Get_StringValue(txtcompany_country.Value) & " FAX: " & CommonUtility.Get_StringValue(txtcompany_fax.Value) & " TAX ID:  " & CommonUtility.Get_StringValue(txtcompany_taxno.Value))

        ElseIf (Len(CommonUtility.Get_StringValue(txtcompany_phone.Value)) <> 0) And (Len(CommonUtility.Get_StringValue(txtcompany_fax.Value)) = 0) And (Len(CommonUtility.Get_StringValue(txtcompany_taxno.Value)) <> 0) And (Mid(CommonUtility.Get_StringValue(txtdest_remark.Value), 1, 5) = "O/TOB") Then

            txtCompany_Check_1.Text = (CommonUtility.Get_StringValue(txtcompany_name.Value) & " " & CommonUtility.Get_StringValue(txtcompany_address.Value) & " " & CommonUtility.Get_StringValue(txtcompany_province.Value) & " " & CommonUtility.Get_StringValue(txtcompany_country.Value) & " " & Mid(CommonUtility.Get_StringValue(txtob_address.Value), 1, 255) & " TEL: " & CommonUtility.Get_StringValue(txtcompany_phone.Value) & " TAX ID:  " & CommonUtility.Get_StringValue(txtcompany_taxno.Value))

        ElseIf (Len(CommonUtility.Get_StringValue(txtcompany_phone.Value)) <> 0) And (Len(CommonUtility.Get_StringValue(txtcompany_fax.Value)) = 0) And (Len(CommonUtility.Get_StringValue(txtcompany_taxno.Value)) = 0) And (Mid(CommonUtility.Get_StringValue(txtdest_remark.Value), 1, 5) = "O/TOB") Then

            txtCompany_Check_1.Text = (CommonUtility.Get_StringValue(txtcompany_name.Value) & " " & CommonUtility.Get_StringValue(txtcompany_address.Value) & " " & CommonUtility.Get_StringValue(txtcompany_province.Value) & " " & CommonUtility.Get_StringValue(txtcompany_country.Value) & " " & Mid(CommonUtility.Get_StringValue(txtob_address.Value), 1, 255) & " TEL: " & CommonUtility.Get_StringValue(txtcompany_phone.Value))

        ElseIf (Len(CommonUtility.Get_StringValue(txtcompany_phone.Value)) = 0) And (Len(CommonUtility.Get_StringValue(txtcompany_fax.Value)) <> 0) And (Len(CommonUtility.Get_StringValue(txtcompany_taxno.Value)) = 0) And (Mid(CommonUtility.Get_StringValue(txtdest_remark.Value), 1, 5) = "O/TOB") Then

            txtCompany_Check_1.Text = (CommonUtility.Get_StringValue(txtcompany_name.Value) & " " & CommonUtility.Get_StringValue(txtcompany_address.Value) & " " & CommonUtility.Get_StringValue(txtcompany_province.Value) & " " & CommonUtility.Get_StringValue(txtcompany_country.Value) & " " & Mid(CommonUtility.Get_StringValue(txtob_address.Value), 1, 255) & " FAX: " & CommonUtility.Get_StringValue(txtcompany_fax.Value))

        ElseIf (Len(CommonUtility.Get_StringValue(txtcompany_phone.Value)) = 0) And (Len(CommonUtility.Get_StringValue(txtcompany_fax.Value)) <> 0) And (Len(CommonUtility.Get_StringValue(txtcompany_taxno.Value)) <> 0) And (Mid(CommonUtility.Get_StringValue(txtdest_remark.Value), 1, 5) = "O/TOB") Then

            txtCompany_Check_1.Text = (CommonUtility.Get_StringValue(txtcompany_name.Value) & " " & CommonUtility.Get_StringValue(txtcompany_address.Value) & " " & CommonUtility.Get_StringValue(txtcompany_province.Value) & " " & CommonUtility.Get_StringValue(txtcompany_country.Value) & " " & Mid(CommonUtility.Get_StringValue(txtob_address.Value), 1, 255) & " FAX: " & CommonUtility.Get_StringValue(txtcompany_fax.Value) & " TAX ID:  " & CommonUtility.Get_StringValue(txtcompany_taxno.Value))

        ElseIf (Len(CommonUtility.Get_StringValue(txtcompany_phone.Value)) = 0) And (Len(CommonUtility.Get_StringValue(txtcompany_fax.Value)) = 0) And (Len(CommonUtility.Get_StringValue(txtcompany_taxno.Value)) <> 0) And (Mid(CommonUtility.Get_StringValue(txtdest_remark.Value), 1, 5) = "O/TCO") Then

            txtCompany_Check_1.Text = (Mid(CommonUtility.Get_StringValue(txtob_address.Value), 1, 255) & " " & CommonUtility.Get_StringValue(txtcompany_name.Value) & " " & CommonUtility.Get_StringValue(txtcompany_address.Value) & " " & CommonUtility.Get_StringValue(txtcompany_province.Value) & " " & CommonUtility.Get_StringValue(txtcompany_country.Value) & " TAX ID:  " & CommonUtility.Get_StringValue(txtcompany_taxno.Value))

        ElseIf (Len(CommonUtility.Get_StringValue(txtcompany_phone.Value)) = 0) And (Len(CommonUtility.Get_StringValue(txtcompany_fax.Value)) = 0) And (Len(CommonUtility.Get_StringValue(txtcompany_taxno.Value)) = 0) And (Mid(CommonUtility.Get_StringValue(txtdest_remark.Value), 1, 5) = "O/TCO") Then

            txtCompany_Check_1.Text = (Mid(CommonUtility.Get_StringValue(txtob_address.Value), 1, 255) & " " & CommonUtility.Get_StringValue(txtcompany_name.Value) & " " & CommonUtility.Get_StringValue(txtcompany_address.Value) & " " & CommonUtility.Get_StringValue(txtcompany_province.Value) & " " & CommonUtility.Get_StringValue(txtcompany_country.Value))

        ElseIf (Len(CommonUtility.Get_StringValue(txtcompany_phone.Value)) = 0) And (Len(CommonUtility.Get_StringValue(txtcompany_fax.Value)) = 0) And (Len(CommonUtility.Get_StringValue(txtcompany_taxno.Value)) <> 0) And (Mid(CommonUtility.Get_StringValue(txtdest_remark.Value), 1, 5) = "O/TOB") Then

            txtCompany_Check_1.Text = (CommonUtility.Get_StringValue(txtcompany_name.Value) & " " & CommonUtility.Get_StringValue(txtcompany_address.Value) & " " & CommonUtility.Get_StringValue(txtcompany_province.Value) & " " & CommonUtility.Get_StringValue(txtcompany_country.Value) & " " & Mid(CommonUtility.Get_StringValue(txtob_address.Value), 1, 255) & " TAX ID:  " & CommonUtility.Get_StringValue(txtcompany_taxno.Value))

        ElseIf (Len(CommonUtility.Get_StringValue(txtcompany_phone.Value)) = 0) And (Len(CommonUtility.Get_StringValue(txtcompany_fax.Value)) = 0) And (Len(CommonUtility.Get_StringValue(txtcompany_taxno.Value)) = 0) And (Mid(CommonUtility.Get_StringValue(txtdest_remark.Value), 1, 5) = "O/TOB") Then

            txtCompany_Check_1.Text = (CommonUtility.Get_StringValue(txtcompany_name.Value) & " " & CommonUtility.Get_StringValue(txtcompany_address.Value) & " " & CommonUtility.Get_StringValue(txtcompany_province.Value) & " " & CommonUtility.Get_StringValue(txtcompany_country.Value) & " " & Mid(CommonUtility.Get_StringValue(txtob_address.Value), 1, 255))
        End If
    End Sub

    Sub destination_check()
        If (Len(CommonUtility.Get_StringValue(txtdestination_phone.Value) <> 0) And (Len(CommonUtility.Get_StringValue(txtdestination_fax.Value)) <> 0) And (Len(CommonUtility.Get_StringValue(txtdestination_taxid.Value)) <> 0) And (Mid(CommonUtility.Get_StringValue(txtdest_remark1.Value), 1, 3)) = "C/O") Then

            txtdestination_Check2.Text = Mid(CommonUtility.Get_StringValue(txtob_dest_address.Value), 1, 400) & " CARE OF " & CommonUtility.Get_StringValue(txtdestination_company.Value) & " " & CommonUtility.Get_StringValue(txtdestination_address.Value) & " " & CommonUtility.Get_StringValue(txtdestination_province.Value) & " " & CommonUtility.Get_StringValue(txtdest_Receive_country.Value) & " TEL: " & CommonUtility.Get_StringValue(txtdestination_phone.Value) & " FAX: " & CommonUtility.Get_StringValue(txtdestination_fax.Value) & " TAX ID:  " & CommonUtility.Get_StringValue(txtdestination_taxid.Value)

        ElseIf (Len(CommonUtility.Get_StringValue(txtdestination_phone.Value) <> 0) And (Len(CommonUtility.Get_StringValue(txtdestination_fax.Value)) <> 0) And (Len(CommonUtility.Get_StringValue(txtdestination_taxid.Value)) <> 0) And (Mid(CommonUtility.Get_StringValue(txtdest_remark1.Value), 1, 3) = "O/B")) Then

            txtdestination_Check2.Text = (CommonUtility.Get_StringValue(txtdestination_company.Value) & "  " & CommonUtility.Get_StringValue(txtdestination_address.Value) & " " & CommonUtility.Get_StringValue(txtdestination_province.Value) & " " & CommonUtility.Get_StringValue(txtdest_Receive_country.Value) & " TEL: " & CommonUtility.Get_StringValue(txtdestination_phone.Value) & " FAX: " & CommonUtility.Get_StringValue(txtdestination_fax.Value) & " TAX ID:  " & CommonUtility.Get_StringValue(txtdestination_taxid.Value) & " ON BEHALF OF " & Mid(CommonUtility.Get_StringValue(txtob_dest_address.Value), 1, 400))

        ElseIf (Len(CommonUtility.Get_StringValue(txtdestination_phone.Value) <> 0) And (Len(CommonUtility.Get_StringValue(txtdestination_fax.Value)) <> 0) And (Len(CommonUtility.Get_StringValue(txtdestination_taxid.Value)) <> 0) And (Mid(CommonUtility.Get_StringValue(txtdest_remark1.Value), 2, 1) <> "/")) Then

            txtdestination_Check2.Text = (CommonUtility.Get_StringValue(txtdestination_company.Value) & "  " & CommonUtility.Get_StringValue(txtdestination_address.Value) & " " & CommonUtility.Get_StringValue(txtdestination_province.Value) & " " & CommonUtility.Get_StringValue(txtdest_Receive_country.Value) & " TEL: " & CommonUtility.Get_StringValue(txtdestination_phone.Value) & " FAX: " & CommonUtility.Get_StringValue(txtdestination_fax.Value) & " TAX ID:  " & CommonUtility.Get_StringValue(txtdestination_taxid.Value))

        ElseIf (Len(CommonUtility.Get_StringValue(txtdestination_phone.Value) <> 0) And (Len(CommonUtility.Get_StringValue(txtdestination_fax.Value)) = 0) And (Len(CommonUtility.Get_StringValue(txtdestination_taxid.Value)) <> 0) And (Mid(CommonUtility.Get_StringValue(txtdest_remark1.Value), 1, 3)) = "C/O") Then

            txtdestination_Check2.Text = Mid(CommonUtility.Get_StringValue(txtob_dest_address.Value), 1, 400) & " CARE OF " & CommonUtility.Get_StringValue(txtdestination_company.Value) & " " & CommonUtility.Get_StringValue(txtdestination_address.Value) & " " & CommonUtility.Get_StringValue(txtdestination_province.Value) & " " & CommonUtility.Get_StringValue(txtdest_Receive_country.Value) & " TEL: " & CommonUtility.Get_StringValue(txtdestination_phone.Value) & " TAX ID:  " & CommonUtility.Get_StringValue(txtdestination_taxid.Value)

        ElseIf (Len(CommonUtility.Get_StringValue(txtdestination_phone.Value) <> 0) And (Len(CommonUtility.Get_StringValue(txtdestination_fax.Value)) = 0) And (Len(CommonUtility.Get_StringValue(txtdestination_taxid.Value)) = 0) And (Mid(CommonUtility.Get_StringValue(txtdest_remark1.Value), 1, 3)) = "C/O") Then

            txtdestination_Check2.Text = Mid(CommonUtility.Get_StringValue(txtob_dest_address.Value), 1, 400) & " CARE OF " & CommonUtility.Get_StringValue(txtdestination_company.Value) & " " & CommonUtility.Get_StringValue(txtdestination_address.Value) & " " & CommonUtility.Get_StringValue(txtdestination_province.Value) & " " & CommonUtility.Get_StringValue(txtdest_Receive_country.Value) & " TEL: " & CommonUtility.Get_StringValue(txtdestination_phone.Value)

        ElseIf (Len(CommonUtility.Get_StringValue(txtdestination_phone.Value) = 0) And (Len(CommonUtility.Get_StringValue(txtdestination_fax.Value)) <> 0) And (Len(CommonUtility.Get_StringValue(txtdestination_taxid.Value)) = 0) And (Mid(CommonUtility.Get_StringValue(txtdest_remark1.Value), 1, 3)) = "C/O") Then

            txtdestination_Check2.Text = Mid(CommonUtility.Get_StringValue(txtob_dest_address.Value), 1, 400) & " CARE OF " & CommonUtility.Get_StringValue(txtdestination_company.Value) & " " & CommonUtility.Get_StringValue(txtdestination_address.Value) & " " & CommonUtility.Get_StringValue(txtdestination_province.Value) & " " & CommonUtility.Get_StringValue(txtdest_Receive_country.Value) & " FAX: " & CommonUtility.Get_StringValue(txtdestination_fax.Value)

        ElseIf (Len(CommonUtility.Get_StringValue(txtdestination_phone.Value) = 0) And (Len(CommonUtility.Get_StringValue(txtdestination_fax.Value)) <> 0) And (Len(CommonUtility.Get_StringValue(txtdestination_taxid.Value)) <> 0) And (Mid(CommonUtility.Get_StringValue(txtdest_remark1.Value), 1, 3)) = "C/O") Then

            txtdestination_Check2.Text = Mid(CommonUtility.Get_StringValue(txtob_dest_address.Value), 1, 400) & " CARE OF " & CommonUtility.Get_StringValue(txtdestination_company.Value) & " " & CommonUtility.Get_StringValue(txtdestination_address.Value) & " " & CommonUtility.Get_StringValue(txtdestination_province.Value) & " " & CommonUtility.Get_StringValue(txtdest_Receive_country.Value) & " FAX: " & CommonUtility.Get_StringValue(txtdestination_fax.Value) & " TAX ID:  " & CommonUtility.Get_StringValue(txtdestination_taxid.Value)

        ElseIf (Len(CommonUtility.Get_StringValue(txtdestination_phone.Value) <> 0) And (Len(CommonUtility.Get_StringValue(txtdestination_fax.Value)) = 0) And (Len(CommonUtility.Get_StringValue(txtdestination_taxid.Value)) <> 0) And (Mid(CommonUtility.Get_StringValue(txtdest_remark1.Value), 1, 3) = "O/B")) Then

            txtdestination_Check2.Text = (CommonUtility.Get_StringValue(txtdestination_company.Value) & "  " & CommonUtility.Get_StringValue(txtdestination_address.Value) & " " & CommonUtility.Get_StringValue(txtdestination_province.Value) & " " & CommonUtility.Get_StringValue(txtdest_Receive_country.Value) & " TEL: " & CommonUtility.Get_StringValue(txtdestination_phone.Value) & " TAX ID:  " & CommonUtility.Get_StringValue(txtdestination_taxid.Value) & " ON BEHALF OF " & Mid(CommonUtility.Get_StringValue(txtob_dest_address.Value), 1, 400))

        ElseIf (Len(CommonUtility.Get_StringValue(txtdestination_phone.Value) <> 0) And (Len(CommonUtility.Get_StringValue(txtdestination_fax.Value)) = 0) And (Len(CommonUtility.Get_StringValue(txtdestination_taxid.Value)) = 0) And (Mid(CommonUtility.Get_StringValue(txtdest_remark1.Value), 1, 3) = "O/B")) Then

            txtdestination_Check2.Text = (CommonUtility.Get_StringValue(txtdestination_company.Value) & "  " & CommonUtility.Get_StringValue(txtdestination_address.Value) & " " & CommonUtility.Get_StringValue(txtdestination_province.Value) & " " & CommonUtility.Get_StringValue(txtdest_Receive_country.Value) & " TEL: " & CommonUtility.Get_StringValue(txtdestination_phone.Value) & " ON BEHALF OF " & Mid(CommonUtility.Get_StringValue(txtob_dest_address.Value), 1, 400))

        ElseIf (Len(CommonUtility.Get_StringValue(txtdestination_phone.Value) = 0) And (Len(CommonUtility.Get_StringValue(txtdestination_fax.Value)) <> 0) And (Len(CommonUtility.Get_StringValue(txtdestination_taxid.Value)) = 0) And (Mid(CommonUtility.Get_StringValue(txtdest_remark1.Value), 1, 3) = "O/B")) Then

            txtdestination_Check2.Text = (CommonUtility.Get_StringValue(txtdestination_company.Value) & "  " & CommonUtility.Get_StringValue(txtdestination_address.Value) & " " & CommonUtility.Get_StringValue(txtdestination_province.Value) & " " & CommonUtility.Get_StringValue(txtdest_Receive_country.Value) & " FAX: " & CommonUtility.Get_StringValue(txtdestination_fax.Value) & " ON BEHALF OF " & Mid(CommonUtility.Get_StringValue(txtob_dest_address.Value), 1, 400))

        ElseIf (Len(CommonUtility.Get_StringValue(txtdestination_phone.Value) = 0) And (Len(CommonUtility.Get_StringValue(txtdestination_fax.Value)) <> 0) And (Len(CommonUtility.Get_StringValue(txtdestination_taxid.Value)) <> 0) And (Mid(CommonUtility.Get_StringValue(txtdest_remark1.Value), 1, 3) = "O/B")) Then

            txtdestination_Check2.Text = (CommonUtility.Get_StringValue(txtdestination_company.Value) & "  " & CommonUtility.Get_StringValue(txtdestination_address.Value) & " " & CommonUtility.Get_StringValue(txtdestination_province.Value) & " " & CommonUtility.Get_StringValue(txtdest_Receive_country.Value) & " FAX: " & CommonUtility.Get_StringValue(txtdestination_fax.Value) & " TAX ID:  " & CommonUtility.Get_StringValue(txtdestination_taxid.Value) & " ON BEHALF OF " & Mid(CommonUtility.Get_StringValue(txtob_dest_address.Value), 1, 400))

        ElseIf (Len(CommonUtility.Get_StringValue(txtdestination_phone.Value) <> 0) And (Len(CommonUtility.Get_StringValue(txtdestination_fax.Value)) = 0) And (Len(CommonUtility.Get_StringValue(txtdestination_taxid.Value)) <> 0) And (Mid(CommonUtility.Get_StringValue(txtdest_remark1.Value), 2, 1) <> "/")) Then

            txtdestination_Check2.Text = (CommonUtility.Get_StringValue(txtdestination_company.Value) & "  " & CommonUtility.Get_StringValue(txtdestination_address.Value) & " " & CommonUtility.Get_StringValue(txtdestination_province.Value) & " " & CommonUtility.Get_StringValue(txtdest_Receive_country.Value) & " TEL: " & CommonUtility.Get_StringValue(txtdestination_phone.Value) & " TAX ID:  " & CommonUtility.Get_StringValue(txtdestination_taxid.Value))

        ElseIf (Len(CommonUtility.Get_StringValue(txtdestination_phone.Value) <> 0) And (Len(CommonUtility.Get_StringValue(txtdestination_fax.Value)) = 0) And (Len(CommonUtility.Get_StringValue(txtdestination_taxid.Value)) = 0) And (Mid(CommonUtility.Get_StringValue(txtdest_remark1.Value), 2, 1) <> "/")) Then

            txtdestination_Check2.Text = (CommonUtility.Get_StringValue(txtdestination_company.Value) & "  " & CommonUtility.Get_StringValue(txtdestination_address.Value) & " " & CommonUtility.Get_StringValue(txtdestination_province.Value) & " " & CommonUtility.Get_StringValue(txtdest_Receive_country.Value) & " TEL: " & CommonUtility.Get_StringValue(txtdestination_phone.Value))

        ElseIf (Len(CommonUtility.Get_StringValue(txtdestination_phone.Value) = 0) And (Len(CommonUtility.Get_StringValue(txtdestination_fax.Value)) <> 0) And (Len(CommonUtility.Get_StringValue(txtdestination_taxid.Value)) = 0) And (Mid(CommonUtility.Get_StringValue(txtdest_remark1.Value), 2, 1) <> "/")) Then

            txtdestination_Check2.Text = (CommonUtility.Get_StringValue(txtdestination_company.Value) & "  " & CommonUtility.Get_StringValue(txtdestination_address.Value) & " " & CommonUtility.Get_StringValue(txtdestination_province.Value) & " " & CommonUtility.Get_StringValue(txtdest_Receive_country.Value) & " FAX: " & CommonUtility.Get_StringValue(txtdestination_fax.Value))

        ElseIf (Len(CommonUtility.Get_StringValue(txtdestination_phone.Value) = 0) And (Len(CommonUtility.Get_StringValue(txtdestination_fax.Value)) <> 0) And (Len(CommonUtility.Get_StringValue(txtdestination_taxid.Value)) <> 0) And (Mid(CommonUtility.Get_StringValue(txtdest_remark1.Value), 2, 1) <> "/")) Then

            txtdestination_Check2.Text = (CommonUtility.Get_StringValue(txtdestination_company.Value) & "  " & CommonUtility.Get_StringValue(txtdestination_address.Value) & " " & CommonUtility.Get_StringValue(txtdestination_province.Value) & " " & CommonUtility.Get_StringValue(txtdest_Receive_country.Value) & " FAX: " & CommonUtility.Get_StringValue(txtdestination_fax.Value) & " TAX ID:  " & CommonUtility.Get_StringValue(txtdestination_taxid.Value))

        ElseIf (Len(CommonUtility.Get_StringValue(txtdestination_phone.Value) <> 0) And (Len(CommonUtility.Get_StringValue(txtdestination_fax.Value)) <> 0) And (Len(CommonUtility.Get_StringValue(txtdestination_taxid.Value)) = 0) And (Mid(CommonUtility.Get_StringValue(txtdest_remark1.Value), 2, 1) <> "/")) Then

            txtdestination_Check2.Text = (CommonUtility.Get_StringValue(txtdestination_company.Value) & "  " & CommonUtility.Get_StringValue(txtdestination_address.Value) & " " & CommonUtility.Get_StringValue(txtdestination_province.Value) & " " & CommonUtility.Get_StringValue(txtdest_Receive_country.Value) & " TEL: " & CommonUtility.Get_StringValue(txtdestination_phone.Value) & " FAX: " & CommonUtility.Get_StringValue(txtdestination_fax.Value))

        ElseIf (Len(CommonUtility.Get_StringValue(txtdestination_phone.Value) = 0) And (Len(CommonUtility.Get_StringValue(txtdestination_fax.Value)) = 0) And (Len(CommonUtility.Get_StringValue(txtdestination_taxid.Value)) <> 0) And (Mid(CommonUtility.Get_StringValue(txtdest_remark1.Value), 1, 3)) = "C/O") Then

            txtdestination_Check2.Text = Mid(CommonUtility.Get_StringValue(txtob_dest_address.Value), 1, 400) & " CARE OF " & CommonUtility.Get_StringValue(txtdestination_company.Value) & " " & CommonUtility.Get_StringValue(txtdestination_address.Value) & " " & CommonUtility.Get_StringValue(txtdestination_province.Value) & " " & CommonUtility.Get_StringValue(txtdest_Receive_country.Value) & " TAX ID:  " & CommonUtility.Get_StringValue(txtdestination_taxid.Value)

        ElseIf (Len(CommonUtility.Get_StringValue(txtdestination_phone.Value) = 0) And (Len(CommonUtility.Get_StringValue(txtdestination_fax.Value)) = 0) And (Len(CommonUtility.Get_StringValue(txtdestination_taxid.Value)) = 0) And (Mid(CommonUtility.Get_StringValue(txtdest_remark1.Value), 1, 3)) = "C/O") Then

            txtdestination_Check2.Text = Mid(CommonUtility.Get_StringValue(txtob_dest_address.Value), 1, 400) & " CARE OF " & CommonUtility.Get_StringValue(txtdestination_company.Value) & " " & CommonUtility.Get_StringValue(txtdestination_address.Value) & " " & CommonUtility.Get_StringValue(txtdestination_province.Value) & " " & CommonUtility.Get_StringValue(txtdest_Receive_country.Value)

        ElseIf (Len(CommonUtility.Get_StringValue(txtdestination_phone.Value) = 0) And (Len(CommonUtility.Get_StringValue(txtdestination_fax.Value)) = 0) And (Len(CommonUtility.Get_StringValue(txtdestination_taxid.Value)) <> 0) And (Mid(CommonUtility.Get_StringValue(txtdest_remark1.Value), 1, 3) = "O/B")) Then

            txtdestination_Check2.Text = (CommonUtility.Get_StringValue(txtdestination_company.Value) & "  " & CommonUtility.Get_StringValue(txtdestination_address.Value) & " " & CommonUtility.Get_StringValue(txtdestination_province.Value) & " " & CommonUtility.Get_StringValue(txtdest_Receive_country.Value) & " TAX ID:  " & CommonUtility.Get_StringValue(txtdestination_taxid.Value) & " ON BEHALF OF " & Mid(CommonUtility.Get_StringValue(txtob_dest_address.Value), 1, 400))

        ElseIf (Len(CommonUtility.Get_StringValue(txtdestination_phone.Value) = 0) And (Len(CommonUtility.Get_StringValue(txtdestination_fax.Value)) = 0) And (Len(CommonUtility.Get_StringValue(txtdestination_taxid.Value)) = 0) And (Mid(CommonUtility.Get_StringValue(txtdest_remark1.Value), 1, 3) = "O/B")) Then

            txtdestination_Check2.Text = (CommonUtility.Get_StringValue(txtdestination_company.Value) & "  " & CommonUtility.Get_StringValue(txtdestination_address.Value) & " " & CommonUtility.Get_StringValue(txtdestination_province.Value) & " " & CommonUtility.Get_StringValue(txtdest_Receive_country.Value) & " ON BEHALF OF " & Mid(CommonUtility.Get_StringValue(txtob_dest_address.Value), 1, 400))

        ElseIf (Len(CommonUtility.Get_StringValue(txtdestination_phone.Value) = 0) And (Len(CommonUtility.Get_StringValue(txtdestination_fax.Value)) = 0) And (Len(CommonUtility.Get_StringValue(txtdestination_taxid.Value)) <> 0) And (Mid(CommonUtility.Get_StringValue(txtdest_remark1.Value), 2, 1) <> "/")) Then

            txtdestination_Check2.Text = (CommonUtility.Get_StringValue(txtdestination_company.Value) & "  " & CommonUtility.Get_StringValue(txtdestination_address.Value) & " " & CommonUtility.Get_StringValue(txtdestination_province.Value) & " " & CommonUtility.Get_StringValue(txtdest_Receive_country.Value) & " TAX ID:  " & CommonUtility.Get_StringValue(txtdestination_taxid.Value))

        ElseIf (Len(CommonUtility.Get_StringValue(txtdestination_phone.Value) = 0) And (Len(CommonUtility.Get_StringValue(txtdestination_fax.Value)) = 0) And (Len(CommonUtility.Get_StringValue(txtdestination_taxid.Value)) = 0) And (Mid(CommonUtility.Get_StringValue(txtdest_remark1.Value), 2, 1) <> "/")) Then

            txtdestination_Check2.Text = (CommonUtility.Get_StringValue(txtdestination_company.Value) & "  " & CommonUtility.Get_StringValue(txtdestination_address.Value) & " " & CommonUtility.Get_StringValue(txtdestination_province.Value) & " " & CommonUtility.Get_StringValue(txtdest_Receive_country.Value))

        ElseIf (Len(CommonUtility.Get_StringValue(txtdestination_phone.Value) <> 0) And (Len(CommonUtility.Get_StringValue(txtdestination_fax.Value)) <> 0) And (Len(CommonUtility.Get_StringValue(txtdestination_taxid.Value)) <> 0) And (Mid(CommonUtility.Get_StringValue(txtdest_remark1.Value), 1, 5)) = "O/TCO") Then

            txtdestination_Check2.Text = Mid(CommonUtility.Get_StringValue(txtob_dest_address.Value), 1, 400) & " " & CommonUtility.Get_StringValue(txtdestination_company.Value) & " " & CommonUtility.Get_StringValue(txtdestination_address.Value) & " " & CommonUtility.Get_StringValue(txtdestination_province.Value) & " " & CommonUtility.Get_StringValue(txtdest_Receive_country.Value) & " TEL: " & CommonUtility.Get_StringValue(txtdestination_phone.Value) & " FAX: " & CommonUtility.Get_StringValue(txtdestination_fax.Value) & " TAX ID:  " & CommonUtility.Get_StringValue(txtdestination_taxid.Value)

        ElseIf (Len(CommonUtility.Get_StringValue(txtdestination_phone.Value) <> 0) And (Len(CommonUtility.Get_StringValue(txtdestination_fax.Value)) <> 0) And (Len(CommonUtility.Get_StringValue(txtdestination_taxid.Value)) = 0) And (Mid(CommonUtility.Get_StringValue(txtdest_remark1.Value), 1, 5)) = "O/TCO") Then

            txtdestination_Check2.Text = Mid(CommonUtility.Get_StringValue(txtob_dest_address.Value), 1, 400) & " " & CommonUtility.Get_StringValue(txtdestination_company.Value) & " " & CommonUtility.Get_StringValue(txtdestination_address.Value) & " " & CommonUtility.Get_StringValue(txtdestination_province.Value) & " " & CommonUtility.Get_StringValue(txtdest_Receive_country.Value) & " TEL: " & CommonUtility.Get_StringValue(txtdestination_phone.Value) & " FAX: " & CommonUtility.Get_StringValue(txtdestination_fax.Value)

        ElseIf (Len(CommonUtility.Get_StringValue(txtdestination_phone.Value) <> 0) And (Len(CommonUtility.Get_StringValue(txtdestination_fax.Value)) <> 0) And (Len(CommonUtility.Get_StringValue(txtdestination_taxid.Value)) <> 0) And (Mid(CommonUtility.Get_StringValue(txtdest_remark1.Value), 1, 5)) = "O/TOB") Then

            txtdestination_Check2.Text = (CommonUtility.Get_StringValue(txtdestination_company.Value) & " " & CommonUtility.Get_StringValue(txtdestination_address.Value) & " " & CommonUtility.Get_StringValue(txtdestination_province.Value) & " " & CommonUtility.Get_StringValue(txtdest_Receive_country.Value) & "  " & Mid(CommonUtility.Get_StringValue(txtob_dest_address.Value), 1, 400) & " TEL: " & CommonUtility.Get_StringValue(txtdestination_phone.Value) & " FAX: " & CommonUtility.Get_StringValue(txtdestination_fax.Value) & " TAX ID:  " & CommonUtility.Get_StringValue(txtdestination_taxid.Value))

        ElseIf (Len(CommonUtility.Get_StringValue(txtdestination_phone.Value) <> 0) And (Len(CommonUtility.Get_StringValue(txtdestination_fax.Value)) <> 0) And (Len(CommonUtility.Get_StringValue(txtdestination_taxid.Value)) = 0) And (Mid(CommonUtility.Get_StringValue(txtdest_remark1.Value), 1, 5)) = "O/TOB") Then

            txtdestination_Check2.Text = (CommonUtility.Get_StringValue(txtdestination_company.Value) & " " & CommonUtility.Get_StringValue(txtdestination_address.Value) & " " & CommonUtility.Get_StringValue(txtdestination_province.Value) & " " & CommonUtility.Get_StringValue(txtdest_Receive_country.Value) & "  " & Mid(CommonUtility.Get_StringValue(txtob_dest_address.Value), 1, 400) & " TEL: " & CommonUtility.Get_StringValue(txtdestination_phone.Value) & " FAX: " & CommonUtility.Get_StringValue(txtdestination_fax.Value))

        ElseIf (Len(CommonUtility.Get_StringValue(txtdestination_phone.Value) <> 0) And (Len(CommonUtility.Get_StringValue(txtdestination_fax.Value)) = 0) And (Len(CommonUtility.Get_StringValue(txtdestination_taxid.Value)) <> 0) And (Mid(CommonUtility.Get_StringValue(txtdest_remark1.Value), 1, 5)) = "O/TCO") Then

            txtdestination_Check2.Text = Mid(CommonUtility.Get_StringValue(txtob_dest_address.Value), 1, 400) & " " & CommonUtility.Get_StringValue(txtdestination_company.Value) & " " & CommonUtility.Get_StringValue(txtdestination_address.Value) & " " & CommonUtility.Get_StringValue(txtdestination_province.Value) & " " & CommonUtility.Get_StringValue(txtdest_Receive_country.Value) & " TEL: " & CommonUtility.Get_StringValue(txtdestination_phone.Value) & " TAX ID:  " & CommonUtility.Get_StringValue(txtdestination_taxid.Value)

        ElseIf (Len(CommonUtility.Get_StringValue(txtdestination_phone.Value) <> 0) And (Len(CommonUtility.Get_StringValue(txtdestination_fax.Value)) = 0) And (Len(CommonUtility.Get_StringValue(txtdestination_taxid.Value)) = 0) And (Mid(CommonUtility.Get_StringValue(txtdest_remark1.Value), 1, 5)) = "O/TCO") Then

            txtdestination_Check2.Text = Mid(CommonUtility.Get_StringValue(txtob_dest_address.Value), 1, 400) & " " & CommonUtility.Get_StringValue(txtdestination_company.Value) & " " & CommonUtility.Get_StringValue(txtdestination_address.Value) & " " & CommonUtility.Get_StringValue(txtdestination_province.Value) & " " & CommonUtility.Get_StringValue(txtdest_Receive_country.Value) & " TEL: " & CommonUtility.Get_StringValue(txtdestination_phone.Value)

        ElseIf (Len(CommonUtility.Get_StringValue(txtdestination_phone.Value) = 0) And (Len(CommonUtility.Get_StringValue(txtdestination_fax.Value)) <> 0) And (Len(CommonUtility.Get_StringValue(txtdestination_taxid.Value)) = 0) And (Mid(CommonUtility.Get_StringValue(txtdest_remark1.Value), 1, 5)) = "O/TCO") Then

            txtdestination_Check2.Text = Mid(CommonUtility.Get_StringValue(txtob_dest_address.Value), 1, 400) & " " & CommonUtility.Get_StringValue(txtdestination_company.Value) & " " & CommonUtility.Get_StringValue(txtdestination_address.Value) & " " & CommonUtility.Get_StringValue(txtdestination_province.Value) & " " & CommonUtility.Get_StringValue(txtdest_Receive_country.Value) & " FAX: " & CommonUtility.Get_StringValue(txtdestination_fax.Value)

        ElseIf (Len(CommonUtility.Get_StringValue(txtdestination_phone.Value) = 0) And (Len(CommonUtility.Get_StringValue(txtdestination_fax.Value)) <> 0) And (Len(CommonUtility.Get_StringValue(txtdestination_taxid.Value)) <> 0) And (Mid(CommonUtility.Get_StringValue(txtdest_remark1.Value), 1, 5)) = "O/TCO") Then

            txtdestination_Check2.Text = Mid(CommonUtility.Get_StringValue(txtob_dest_address.Value), 1, 400) & " " & CommonUtility.Get_StringValue(txtdestination_company.Value) & " " & CommonUtility.Get_StringValue(txtdestination_address.Value) & " " & CommonUtility.Get_StringValue(txtdestination_province.Value) & " " & CommonUtility.Get_StringValue(txtdest_Receive_country.Value) & " FAX: " & CommonUtility.Get_StringValue(txtdestination_fax.Value) & " TAX ID:  " & CommonUtility.Get_StringValue(txtdestination_taxid.Value)

        ElseIf (Len(CommonUtility.Get_StringValue(txtdestination_phone.Value) <> 0) And (Len(CommonUtility.Get_StringValue(txtdestination_fax.Value)) = 0) And (Len(CommonUtility.Get_StringValue(txtdestination_taxid.Value)) <> 0) And (Mid(CommonUtility.Get_StringValue(txtdest_remark1.Value), 1, 5)) = "O/TOB") Then

            txtdestination_Check2.Text = (CommonUtility.Get_StringValue(txtdestination_company.Value) & " " & CommonUtility.Get_StringValue(txtdestination_address.Value) & " " & CommonUtility.Get_StringValue(txtdestination_province.Value) & " " & CommonUtility.Get_StringValue(txtdest_Receive_country.Value) & "  " & Mid(CommonUtility.Get_StringValue(txtob_dest_address.Value), 1, 400) & " TEL: " & CommonUtility.Get_StringValue(txtdestination_phone.Value) & " TAX ID:  " & CommonUtility.Get_StringValue(txtdestination_taxid.Value))

        ElseIf (Len(CommonUtility.Get_StringValue(txtdestination_phone.Value) <> 0) And (Len(CommonUtility.Get_StringValue(txtdestination_fax.Value)) = 0) And (Len(CommonUtility.Get_StringValue(txtdestination_taxid.Value)) = 0) And (Mid(CommonUtility.Get_StringValue(txtdest_remark1.Value), 1, 5)) = "O/TOB") Then

            txtdestination_Check2.Text = (CommonUtility.Get_StringValue(txtdestination_company.Value) & " " & CommonUtility.Get_StringValue(txtdestination_address.Value) & " " & CommonUtility.Get_StringValue(txtdestination_province.Value) & " " & CommonUtility.Get_StringValue(txtdest_Receive_country.Value) & "  " & Mid(CommonUtility.Get_StringValue(txtob_dest_address.Value), 1, 400) & " TEL: " & CommonUtility.Get_StringValue(txtdestination_phone.Value))

        ElseIf (Len(CommonUtility.Get_StringValue(txtdestination_phone.Value) = 0) And (Len(CommonUtility.Get_StringValue(txtdestination_fax.Value)) <> 0) And (Len(CommonUtility.Get_StringValue(txtdestination_taxid.Value)) = 0) And (Mid(CommonUtility.Get_StringValue(txtdest_remark1.Value), 1, 5)) = "O/TOB") Then

            txtdestination_Check2.Text = (CommonUtility.Get_StringValue(txtdestination_company.Value) & " " & CommonUtility.Get_StringValue(txtdestination_address.Value) & " " & CommonUtility.Get_StringValue(txtdestination_province.Value) & " " & CommonUtility.Get_StringValue(txtdest_Receive_country.Value) & "  " & Mid(CommonUtility.Get_StringValue(txtob_dest_address.Value), 1, 400) & " FAX: " & CommonUtility.Get_StringValue(txtdestination_fax.Value))

        ElseIf (Len(CommonUtility.Get_StringValue(txtdestination_phone.Value) = 0) And (Len(CommonUtility.Get_StringValue(txtdestination_fax.Value)) <> 0) And (Len(CommonUtility.Get_StringValue(txtdestination_taxid.Value)) <> 0) And (Mid(CommonUtility.Get_StringValue(txtdest_remark1.Value), 1, 5)) = "O/TOB") Then

            txtdestination_Check2.Text = (CommonUtility.Get_StringValue(txtdestination_company.Value) & " " & CommonUtility.Get_StringValue(txtdestination_address.Value) & " " & CommonUtility.Get_StringValue(txtdestination_province.Value) & " " & CommonUtility.Get_StringValue(txtdest_Receive_country.Value) & "  " & Mid(CommonUtility.Get_StringValue(txtob_dest_address.Value), 1, 400) & " FAX: " & CommonUtility.Get_StringValue(txtdestination_fax.Value) & " TAX ID:  " & CommonUtility.Get_StringValue(txtdestination_taxid.Value))

        ElseIf (Len(CommonUtility.Get_StringValue(txtdestination_phone.Value) = 0) And (Len(CommonUtility.Get_StringValue(txtdestination_fax.Value)) = 0) And (Len(CommonUtility.Get_StringValue(txtdestination_taxid.Value)) <> 0) And (Mid(CommonUtility.Get_StringValue(txtdest_remark1.Value), 1, 5)) = "O/TCO") Then

            txtdestination_Check2.Text = Mid(CommonUtility.Get_StringValue(txtob_dest_address.Value), 1, 400) & " " & CommonUtility.Get_StringValue(txtdestination_company.Value) & " " & CommonUtility.Get_StringValue(txtdestination_address.Value) & " " & CommonUtility.Get_StringValue(txtdestination_province.Value) & " " & CommonUtility.Get_StringValue(txtdest_Receive_country.Value) & " TAX ID:  " & CommonUtility.Get_StringValue(txtdestination_taxid.Value)

        ElseIf (Len(CommonUtility.Get_StringValue(txtdestination_phone.Value) = 0) And (Len(CommonUtility.Get_StringValue(txtdestination_fax.Value)) = 0) And (Len(CommonUtility.Get_StringValue(txtdestination_taxid.Value)) = 0) And (Mid(CommonUtility.Get_StringValue(txtdest_remark1.Value), 1, 5)) = "O/TCO") Then

            txtdestination_Check2.Text = Mid(CommonUtility.Get_StringValue(txtob_dest_address.Value), 1, 400) & " " & CommonUtility.Get_StringValue(txtdestination_company.Value) & " " & CommonUtility.Get_StringValue(txtdestination_address.Value) & " " & CommonUtility.Get_StringValue(txtdestination_province.Value) & " " & CommonUtility.Get_StringValue(txtdest_Receive_country.Value)

        ElseIf (Len(CommonUtility.Get_StringValue(txtdestination_phone.Value) = 0) And (Len(CommonUtility.Get_StringValue(txtdestination_fax.Value)) = 0) And (Len(CommonUtility.Get_StringValue(txtdestination_taxid.Value)) <> 0) And (Mid(CommonUtility.Get_StringValue(txtdest_remark1.Value), 1, 5)) = "O/TOB") Then

            txtdestination_Check2.Text = (CommonUtility.Get_StringValue(txtdestination_company.Value) & " " & CommonUtility.Get_StringValue(txtdestination_address.Value) & " " & CommonUtility.Get_StringValue(txtdestination_province.Value) & " " & CommonUtility.Get_StringValue(txtdest_Receive_country.Value) & "  " & Mid(CommonUtility.Get_StringValue(txtob_dest_address.Value), 1, 400) & " TAX ID:  " & CommonUtility.Get_StringValue(txtdestination_taxid.Value))

        ElseIf (Len(CommonUtility.Get_StringValue(txtdestination_phone.Value) = 0) And (Len(CommonUtility.Get_StringValue(txtdestination_fax.Value)) = 0) And (Len(CommonUtility.Get_StringValue(txtdestination_taxid.Value)) = 0) And (Mid(CommonUtility.Get_StringValue(txtdest_remark1.Value), 1, 5)) = "O/TOB") Then

            txtdestination_Check2.Text = (CommonUtility.Get_StringValue(txtdestination_company.Value) & " " & CommonUtility.Get_StringValue(txtdestination_address.Value) & " " & CommonUtility.Get_StringValue(txtdestination_province.Value) & " " & CommonUtility.Get_StringValue(txtdest_Receive_country.Value) & "  " & Mid(CommonUtility.Get_StringValue(txtob_dest_address.Value), 1, 400))
        End If
    End Sub

    Function confor_mat(ByVal d_date As Date) As String
        Dim str_for As String = Format(d_date, "d/MM/yyyy")
        Return str_for
    End Function
    Sub CallInvoiceCheck() 'Check Invoince
        Select Case txtshow_check.Text
            Case "1000", "10000"
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
    Function CallInvoiceCheckF() As String 'Check Invoince
        Dim str_invoice As String
        Select Case txtshow_check.Text
            Case "1000", "10000"
                str_invoice = txtNumInvoice.Text
            Case Else
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
    Function CallInvoice_() As String 'Check Invoince
        Dim str_invoice As String
        Select Case txtshow_check.Text
            Case "1000", "10000"
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
    'Sub CallInvoiceCheck() 'Check Invoince
    '    If CommonUtility.Get_StringValue(txtinvoice_board.Value) <> "" Then
    '        txtTolInvoice.Text = txtinvoice_board.Value
    '    Else
    '        If CommonUtility.Get_StringValue(txtinvoice_no1.Text) <> "" And CommonUtility.Get_StringValue(txtinvoice_date1.Text) <> "" Then
    '            txtTolInvoice.Text = txtinvoice_no1.Text & vbNewLine & CDate(txtinvoice_date1.Text).Date
    '        ElseIf CommonUtility.Get_StringValue(txtinvoice_no2.Text) <> "" And CommonUtility.Get_StringValue(txtinvoice_date2.Text) <> "" Then
    '            txtTolInvoice.Text = txtinvoice_no1.Text & vbNewLine & CDate(txtinvoice_date1.Text).Date & vbNewLine _
    '                                + txtinvoice_no2.Text & vbNewLine & CDate(txtinvoice_date2.Text).Date
    '        ElseIf CommonUtility.Get_StringValue(txtinvoice_no3.Text) <> "" And CommonUtility.Get_StringValue(txtinvoice_date3.Text) <> "" Then
    '            txtTolInvoice.Text = txtinvoice_no1.Text & vbNewLine & CDate(txtinvoice_date1.Text).Date & vbNewLine _
    '                                + txtinvoice_no2.Text & vbNewLine & CDate(txtinvoice_date2.Text).Date & vbNewLine _
    '                                + txtinvoice_no3.Text & vbNewLine & CDate(txtinvoice_date3.Text).Date
    '        ElseIf CommonUtility.Get_StringValue(txtinvoice_no4.Text) <> "" And CommonUtility.Get_StringValue(txtinvoice_date4.Text) <> "" Then
    '            txtTolInvoice.Text = txtinvoice_no1.Text & vbNewLine & CDate(txtinvoice_date1.Text).Date & vbNewLine _
    '                                + txtinvoice_no2.Text & vbNewLine & CDate(txtinvoice_date2.Text).Date & vbNewLine _
    '                                + txtinvoice_no3.Text & vbNewLine & CDate(txtinvoice_date3.Text).Date & vbNewLine _
    '                                + txtinvoice_no4.Text & vbNewLine & CDate(txtinvoice_date4.Text).Date
    '        ElseIf CommonUtility.Get_StringValue(txtinvoice_no5.Text) <> "" And CommonUtility.Get_StringValue(txtinvoice_date5.Text) <> "" Then
    '            txtTolInvoice.Text = txtinvoice_no1.Text & vbNewLine & CDate(txtinvoice_date1.Text).Date & vbNewLine _
    '                                + txtinvoice_no2.Text & vbNewLine & CDate(txtinvoice_date2.Text).Date & vbNewLine _
    '                                + txtinvoice_no3.Text & vbNewLine & CDate(txtinvoice_date3.Text).Date & vbNewLine _
    '                                + txtinvoice_no4.Text & vbNewLine & CDate(txtinvoice_date4.Text).Date & vbNewLine _
    '                                + txtinvoice_no5.Text & vbNewLine & CDate(txtinvoice_date5.Text).Date
    '        End If
    '    End If


    'End Sub

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
    '        Case 4 '��������
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
    '                                                                "DATE " & Format(txtRep_Doc_date.Value, "d/MM/yyy") & _
    '                                                                " , Ref." & txtsupplementary_details.Value & vbNewLine & _
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
    '        Case 4 '��������
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
    '                                                            "DATE " & Format(txtRep_Doc_date.Value, "d/MM/yyy") & _
    '                                                                " , Ref." & txtsupplementary_details.Value & vbNewLine & _
    '                                                                "       _____________________________"
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
    '        Case 4 '��������
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
    '                                                            "DATE " & Format(txtRep_Doc_date.Value, "d/MM/yyy") & _
    '                                                                " , Ref." & txtsupplementary_details.Value & vbNewLine & _
    '                                                                "       _____________________________"
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
    '        Case 4 '��������
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
    '                                                            "DATE " & Format(txtRep_Doc_date.Value, "d/MM/yyy") & _
    '                                                                " , Ref." & txtsupplementary_details.Value & vbNewLine & _
    '                                                                "       _____________________________"
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
    '                                                                                   txtthird_country.Text & vbCrLf & _
    '                                                                                   txtback_country.Text & vbCrLf & _
    '                                                                                   txtplace_exibition.Text & vbCrLf & _
    '                                                                                   "       _____________________________"
    '                Else
    '                    txtT_product.Text = All_str & _
    '                                       "        " & _
    '                                                                                   txtthird_country.Text & vbCrLf & _
    '                                                                                   txtback_country.Text & vbCrLf & _
    '                                                                                   txtplace_exibition.Text & vbCrLf & _
    '                                                                                   "       _____________________________"
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
    '                                       "        " & _
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
    '        Case 4 '��������
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
    '        Case 5 'T2_back_country
    '            All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '                                        & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

    '            If C_TotalRowDe.Text = count_data Then
    '                If (Not txtquantity1.Text = String.Empty = True) And CommonUtility.Get_StringValue(txtquantity1.Text) <> "" And CommonUtility.Get_Decimal(IIf(Not (txtquantity1.Text = String.Empty) = False, 0, txtquantity1.Value)) <> 0 Then
    '                    txtT_product.Text = All_str
    '                    GroupFooter1.Visible = True
    '                    txtTotalAll.Visible = True
    '                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & _
    '                                                                                    "DATE " & Format(txtRep_Doc_date.Value, "d/MM/yyy") & _
    '                                                                                        " , Ref." & txtsupplementary_details.Value & vbNewLine & _
    '                                                                                        "       _____________________________"
    '                Else
    '                    txtT_product.Text = All_str & _
    '                                        "DATE " & Format(txtRep_Doc_date.Value, "d/MM/yyy") & _
    '                                                                                        " , Ref." & txtsupplementary_details.Value & vbNewLine & _
    '                                                                                        "       _____________________________"
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
    '                                       "        " & _
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
    '        Case 4 '��������
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
    '                txtT_product.Text = All_str & vbCrLf & _
    '                "DATE " & Format(txtRep_Doc_date.Value, "d/MM/yyy") & _
    '                                                                " , Ref." & txtsupplementary_details.Value & vbNewLine & _
    '                                                                "       _____________________________"
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
    Sub Title_StrDetail()
        'All_str = CarTxt(CommonUtility.Get_StringValue(txtSINGLE_COUNTRY_CONTENT.Value), CommonUtility.Get_StringValue(txtInvoiceDetailTH.Value)) & "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
        '                                    & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
        All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                                            & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
    End Sub
    '��᷹ ���͹� ��ͧ��� 1 ���ͧ�ҡ�Դ OB,OC  ���� ����Сͺ��õ�ͧ��͹�ͧ����ͧ���� txtob_address.text �����������͹������� 30-05-2556
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
        Select Case Check_Third(CommonUtility.Get_StringValue(txtthird_country.Value), _
                                CommonUtility.Get_StringValue(txtback_country.Value), _
                                CommonUtility.Get_StringValue(txtplace_exibition.Value))
            Case 1
                'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & NewStr_AddressDetail(CommonUtility.Get_StringValue(txtNewEmail_ch02.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
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
                'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & NewStr_AddressDetail(CommonUtility.Get_StringValue(txtNewEmail_ch02.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
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
                'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & NewStr_AddressDetail(CommonUtility.Get_StringValue(txtNewEmail_ch02.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                    BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
                                                                    BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " & _
                                                                    BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf & "        " & _
                                                                    BahtOnly(txtquantity5.Text, txtq_unit_code5.Text) & vbCrLf & _
                                                                    "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 4 '��������
                'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & NewStr_AddressDetail(CommonUtility.Get_StringValue(txtNewEmail_ch02.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                    BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
                                                                    BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " & _
                                                                    BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf & "        " & _
                                                                    BahtOnly(txtquantity5.Text, txtq_unit_code5.Text) & vbCrLf & _
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
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                    BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
                                                                    BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " & _
                                                                    BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf & "        " & _
                                                                    BahtOnly(txtquantity5.Text, txtq_unit_code5.Text) & vbCrLf & _
                                                                    _TempB2B & vbNewLine & _
                                                                    "       _____________________________"
                    '"DATE " & Format(txtRep_Doc_date.Value, "d/MM/yyy") & _
                    '" , Ref." & txtsupplementary_details.Value & vbNewLine & _
                    '"       _____________________________"

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
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                    BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
                                                                    BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " & _
                                                                    BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf & "        " & _
                                                                    BahtOnly(txtquantity5.Text, txtq_unit_code5.Text) & vbCrLf & _
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
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
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
                'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & NewStr_AddressDetail(CommonUtility.Get_StringValue(txtNewEmail_ch02.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
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
                'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & NewStr_AddressDetail(CommonUtility.Get_StringValue(txtNewEmail_ch02.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
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
        Title_StrDetail()

        Select Case Check_Third(CommonUtility.Get_StringValue(txtthird_country.Value), _
                                CommonUtility.Get_StringValue(txtback_country.Value), _
                                CommonUtility.Get_StringValue(txtplace_exibition.Value))
            Case 1
                'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & NewStr_AddressDetail(CommonUtility.Get_StringValue(txtNewEmail_ch02.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
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
                'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & NewStr_AddressDetail(CommonUtility.Get_StringValue(txtNewEmail_ch02.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
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
                'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & NewStr_AddressDetail(CommonUtility.Get_StringValue(txtNewEmail_ch02.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf & _
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 4 '��������
                'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & NewStr_AddressDetail(CommonUtility.Get_StringValue(txtNewEmail_ch02.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf & _
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
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf & "        " & _
                                                                    _TempB2B & vbNewLine & _
                                                                    "       _____________________________"
                    '"DATE " & Format(txtRep_Doc_date.Value, "d/MM/yyy") & _
                    '    " , Ref." & txtsupplementary_details.Value & vbNewLine & _
                    '    "       _____________________________"
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
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf & _
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
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
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
                'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & NewStr_AddressDetail(CommonUtility.Get_StringValue(txtNewEmail_ch02.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
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
                'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & NewStr_AddressDetail(CommonUtility.Get_StringValue(txtNewEmail_ch02.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
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
        Title_StrDetail()
        Select Case Check_Third(CommonUtility.Get_StringValue(txtthird_country.Value), _
                                CommonUtility.Get_StringValue(txtback_country.Value), _
                                CommonUtility.Get_StringValue(txtplace_exibition.Value))
            Case 1
                'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & NewStr_AddressDetail(CommonUtility.Get_StringValue(txtNewEmail_ch02.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
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
                'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & NewStr_AddressDetail(CommonUtility.Get_StringValue(txtNewEmail_ch02.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
                                                                                BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " & _
                                                                                txtback_country.Text & vbCrLf & _
                                                                                txtplace_exibition.Text & vbCrLf & _
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
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & _
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 4 '��������
                'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & NewStr_AddressDetail(CommonUtility.Get_StringValue(txtNewEmail_ch02.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " & _
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
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & _
                                                                    _TempB2B & vbNewLine & _
                                                                    "       _____________________________"
                    '"DATE " & Format(txtRep_Doc_date.Value, "d/MM/yyy") & _
                    '    " , Ref." & txtsupplementary_details.Value & vbNewLine & _
                    '    "       _____________________________"
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
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & _
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
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " & _
                                                                txtthird_country.Text & vbCrLf & _
                                                                txtback_country.Text & vbCrLf & _
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
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " & _
                                                                txtthird_country.Text & vbCrLf & _
                                                                txtplace_exibition.Text & vbCrLf & _
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
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
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
        Title_StrDetail()
        Select Case Check_Third(CommonUtility.Get_StringValue(txtthird_country.Value), _
                                CommonUtility.Get_StringValue(txtback_country.Value), _
                                CommonUtility.Get_StringValue(txtplace_exibition.Value))
            Case 1
                'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & NewStr_AddressDetail(CommonUtility.Get_StringValue(txtNewEmail_ch02.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
                                                                txtthird_country.Text & vbCrLf & _
                                                                txtback_country.Text & vbCrLf & _
                                                                txtplace_exibition.Text & vbCrLf & _
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
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
                                                                                txtback_country.Text & vbCrLf & _
                                                                                txtplace_exibition.Text & vbCrLf & _
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
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & _
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 4 '��������
                'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & NewStr_AddressDetail(CommonUtility.Get_StringValue(txtNewEmail_ch02.Text))
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
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
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & _
                                                                    _TempB2B & vbNewLine & _
                                                                    "       _____________________________"
                    '"DATE " & Format(txtRep_Doc_date.Value, "d/MM/yyy") & _
                    '    " , Ref." & txtsupplementary_details.Value & vbNewLine & _
                    '    "       _____________________________"
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
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & _
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
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
                                                                txtthird_country.Text & vbCrLf & _
                                                                txtback_country.Text & vbCrLf & _
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
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
                                                                txtthird_country.Text & vbCrLf & _
                                                                txtplace_exibition.Text & vbCrLf & _
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
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                    BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
                                                                    txtthird_country.Text & vbCrLf & _
                                                                    "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
        End Select
    End Sub
    Sub All_CheckThird_1(ByVal count_data As Integer)
        Title_StrDetail()
        Select Case Check_Third(CommonUtility.Get_StringValue(txtthird_country.Value), _
                                CommonUtility.Get_StringValue(txtback_country.Value), _
                                CommonUtility.Get_StringValue(txtplace_exibition.Value))
            Case 1
                'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    If (Not txtquantity1.Text = String.Empty = True) And CommonUtility.Get_StringValue(txtquantity1.Text) <> "" And CommonUtility.Get_Decimal(IIf(Not (txtquantity1.Text = String.Empty) = False, 0, txtquantity1.Value)) <> 0 Then
                        txtT_product.Text = All_str & NewStr_AddressDetail(CommonUtility.Get_StringValue(txtNewEmail_ch02.Text))
                        GroupFooter1.Visible = True
                        txtTotalAll.Visible = True
                        txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                                       txtthird_country.Text & vbCrLf & _
                                                                                       txtback_country.Text & vbCrLf & _
                                                                                       txtplace_exibition.Text & vbCrLf & _
                                                                                       "       _____________________________"
                    Else
                        txtT_product.Text = All_str & _
                                           "        " & _
                                                                                       txtthird_country.Text & vbCrLf & _
                                                                                       txtback_country.Text & vbCrLf & _
                                                                                       txtplace_exibition.Text & vbCrLf & _
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
                        txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                                                        txtback_country.Text & vbCrLf & _
                                                                                                        txtplace_exibition.Text & vbCrLf & _
                                                                                                        "       _____________________________"
                    Else
                        txtT_product.Text = All_str & _
                                           "        " & _
                                                                                                        txtback_country.Text & vbCrLf & _
                                                                                                        txtplace_exibition.Text & vbCrLf & _
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
                        txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & _
                                                                                        "       _____________________________"
                    Else
                        txtT_product.Text = All_str & _
                                            "       _____________________________"
                    End If

                Else
                    txtT_product.Text = All_str
                End If
            Case 4 '��������
                'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    If (Not txtquantity1.Text = String.Empty = True) And CommonUtility.Get_StringValue(txtquantity1.Text) <> "" And CommonUtility.Get_Decimal(IIf(Not (txtquantity1.Text = String.Empty) = False, 0, txtquantity1.Value)) <> 0 Then
                        txtT_product.Text = All_str & NewStr_AddressDetail(CommonUtility.Get_StringValue(txtNewEmail_ch02.Text))
                        GroupFooter1.Visible = True
                        txtTotalAll.Visible = True
                        txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & _
                                                                                        "       _____________________________"
                    Else
                        txtT_product.Text = All_str & _
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
                        txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & _
                        _TempB2B & vbNewLine & _
                        "       _____________________________"
                        '"DATE " & Format(txtRep_Doc_date.Value, "d/MM/yyy") & _
                        '    " , Ref." & txtsupplementary_details.Value & vbNewLine & _
                        '    "       _____________________________"
                    Else
                        txtT_product.Text = All_str & _
                        _TempB2B & vbNewLine & _
                        "       _____________________________"
                        '"DATE " & Format(txtRep_Doc_date.Value, "d/MM/yyy") & _
                        '                                                " , Ref." & txtsupplementary_details.Value & vbNewLine & _
                        '                                                "       _____________________________"
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
                        txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & _
                                                                                        "       _____________________________"
                    Else
                        txtT_product.Text = All_str & _
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
                        txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                                        txtthird_country.Text & vbCrLf & _
                                                                                        txtback_country.Text & vbCrLf & _
                                                                                        "       _____________________________"
                    Else
                        txtT_product.Text = All_str & _
                                            "        " & _
                                                                                        txtthird_country.Text & vbCrLf & _
                                                                                        txtback_country.Text & vbCrLf & _
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
                        txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                                        txtthird_country.Text & vbCrLf & _
                                                                                        txtplace_exibition.Text & vbCrLf & _
                                                                                        "       _____________________________"
                    Else
                        txtT_product.Text = All_str & _
                                           "        " & _
                                                                                        txtthird_country.Text & vbCrLf & _
                                                                                        txtplace_exibition.Text & vbCrLf & _
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
                        txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                                            txtthird_country.Text & vbCrLf & _
                                                                                            "       _____________________________"
                    Else
                        txtT_product.Text = All_str & _
                                            "        " & _
                                                                                            txtthird_country.Text & vbCrLf & _
                                                                                            "       _____________________________"
                    End If

                Else
                    txtT_product.Text = All_str
                End If
        End Select
    End Sub
    Sub All_CheckThird_0(ByVal count_data As Integer)
        Title_StrDetail()
        Select Case Check_Third(CommonUtility.Get_StringValue(txtthird_country.Value), _
                                CommonUtility.Get_StringValue(txtback_country.Value), _
                                CommonUtility.Get_StringValue(txtplace_exibition.Value))
            Case 1
                'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

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
                'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & _
                    txtback_country.Text & vbCrLf & _
                                                                                txtplace_exibition.Text & vbCrLf & _
                                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 3 'T3_place_exibition
                'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & _
                     "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 4 '��������
                'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & _
                     "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 5 'T2_back_country
                'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & vbCrLf & _
                    _TempB2B & vbNewLine & _
                    "       _____________________________"
                    '"DATE " & Format(txtRep_Doc_date.Value, "d/MM/yyy") & _
                    '                                                " , Ref." & txtsupplementary_details.Value & vbNewLine & _
                    '                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 6 'T3_place_exibition
                'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & _
                     "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 7 'T1_third_country,T2_back_country
                'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & _
                    txtthird_country.Text & vbCrLf & _
                                                                txtback_country.Text & vbCrLf & _
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 8 'T1_third_country,T3_place_exibition
                'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & _
                     txtthird_country.Text & vbCrLf & _
                                                                txtplace_exibition.Text & vbCrLf & _
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 9 'T1_third_country
                'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

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

        If _tariff.Length > 4 And _tariff.Length <= 6 Then
            str_tariff = Mid(_tariff, 1, 4) & "." & Mid(_tariff, 5, 2)
        ElseIf _tariff.Length > 6 And _tariff.Length <= 8 Then
            str_tariff = Mid(_tariff, 1, 4) & "." & Mid(_tariff, 5, 2) & "." & Mid(_tariff, 7, 2)
        ElseIf _tariff.Length > 8 And _tariff.Length <= 10 Then
            str_tariff = Mid(_tariff, 1, 4) & "." & Mid(_tariff, 5, 2) & "." & Mid(_tariff, 7, 4)
        ElseIf _tariff.Length > 8 And _tariff.Length = 11 Then
            str_tariff = Mid(_tariff, 1, 4) & "." & Mid(_tariff, 5, 2) & "." & Mid(_tariff, 7, 4) & "." & Mid(_tariff, 11, 1)
        ElseIf _tariff.Length > 0 And _tariff.Length <= 4 Then
            str_tariff = Mid(_tariff, 1, 4)
        End If

        Return str_tariff
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
            '�����
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
            '�Ҵ 5
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
            '�Ҵ 4,5
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
            '�Ҵ 3,4,5
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
            '�Ҵ 2,3,4,5
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
    'Sub CHeck_DisPlay()
    '    Dim sss As String = txtgross_weight.Text

    '    If Mid(CommonUtility.Get_StringValue(txtFOBDisplay.Value), 1, 50) = "Display" Then

    '        If Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "NET WEIGHT" Then

    '            txtGrossTxt.Text = Mid(txtnet_weight.Value, 1, 9) & " " & Mid(txtunit_code2.Value, 1, 10) '& vbNewLine & FobTxt()

    '        ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "GROSS WEIGHT - NET WEIGHT" Then
    '            txtGrossTxt.Text = GrossTxt() & vbNewLine & Mid(txtnet_weight.Value, 1, 9) & " " & Mid(txtunit_code2.Value, 1, 10) '& vbNewLine & FobTxt()

    '        ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "GROSS WEIGHT" Then
    '            txtGrossTxt.Text = GrossTxt() '& vbNewLine & FobTxt()

    '        ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "" Then
    '            txtGrossTxt.Text = GrossTxt() '& vbNewLine & FobTxt()

    '        End If
    '    ElseIf Mid(CommonUtility.Get_StringValue(txtFOBDisplay.Value), 1, 50) = "NoDisplay" Then
    '        If Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "NET WEIGHT" Then
    '            txtGrossTxt.Text = Mid(txtnet_weight.Value, 1, 9) & " " & Mid(txtunit_code2.Value, 1, 10)

    '        ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "GROSS WEIGHT - NET WEIGHT" Then
    '            txtGrossTxt.Text = GrossTxt() & vbNewLine & Mid(txtnet_weight.Value, 1, 9) & " " & Mid(txtunit_code2.Value, 1, 10)

    '        ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "GROSS WEIGHT" Then
    '            txtGrossTxt.Text = GrossTxt()

    '        ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "" Then
    '            txtGrossTxt.Text = GrossTxt()

    '        End If

    '    ElseIf Mid(CommonUtility.Get_StringValue(txtFOBDisplay.Value), 1, 50) = "" Then

    '        If Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "NET WEIGHT" Then
    '            txtGrossTxt.Text = Mid(txtnet_weight.Value, 1, 9) & " " & Mid(txtunit_code2.Value, 1, 10) '& vbNewLine & FobTxt()

    '        ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "GROSS WEIGHT - NET WEIGHT" Then
    '            txtGrossTxt.Text = GrossTxt() & vbNewLine & Mid(txtnet_weight.Value, 1, 9) & " " & Mid(txtunit_code2.Value, 1, 10) '& vbNewLine & FobTxt()

    '        ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "GROSS WEIGHT" Then
    '            txtGrossTxt.Text = GrossTxt() '& vbNewLine & FobTxt()

    '        ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "" Then
    '            txtGrossTxt.Text = GrossTxt() '& vbNewLine & FobTxt()

    '        End If
    '    End If

    'End Sub
    Sub CHeck_DisPlay()


        Dim sss As String = txtgross_weight.Text

        Dim str_gross As String

        'str_gross = txtgross_weight.Text

        '����ͧ invoice ��ҧ�����
        Dim str_NumInvoice As String = CommonUtility.Get_StringValue(txtNumInvoice.Text)
        '����ͧ ��Ť�� ��ҧ�����
        Dim str_USDInvoice As String = CommonUtility.Get_StringValue(txtUSDInvoice.Text)

        Select Case txtshow_check.Text
            Case "100" ' ����ͧ invoice ��ҧ�����
                If Mid(CommonUtility.Get_StringValue(txtFOBDisplay.Value), 1, 50) = "Display" Then

                    If Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "NET WEIGHT" Then

                        str_gross = NetWeight_(txtnet_weight.Value, txtunit_code2.Value) 'Mid(txtnet_weight.Value, 1, 9) & " " & Mid(txtunit_code2.Value, 1, 10) '& vbNewLine & FobTxt()
                        Select Case C_TotalRowDe.Text
                            Case 1
                                txtGrossTxt.Text = str_gross & vbNewLine & str_USDInvoice
                            Case Else

                                If Cpage > 1 Then
                                    str_gross = ""
                                    txtGrossTxt.Text = str_gross '& vbNewLine & str_USDInvoice
                                ElseIf Cpage = 1 Then

                                    txtGrossTxt.Text = str_gross & vbNewLine & str_USDInvoice
                                End If
                        End Select

                    ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "GROSS WEIGHT - NET WEIGHT" Then
                        str_gross = GrossTxt() & vbNewLine & NetWeight_(txtnet_weight.Value, txtunit_code2.Value) 'Mid(txtnet_weight.Value, 1, 9) & " " & Mid(txtunit_code2.Value, 1, 10) '& vbNewLine & FobTxt()
                        Select Case C_TotalRowDe.Text
                            Case 1
                                txtGrossTxt.Text = str_gross & vbNewLine & str_USDInvoice
                            Case Else
                                If Cpage > 1 Then
                                    str_gross = ""
                                    txtGrossTxt.Text = str_gross '& vbNewLine & str_USDInvoice
                                ElseIf Cpage = 1 Then

                                    txtGrossTxt.Text = str_gross & vbNewLine & str_USDInvoice
                                End If
                        End Select
                    ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "GROSS WEIGHT" Then
                        str_gross = GrossTxt() '& vbNewLine & FobTxt()
                        Select Case C_TotalRowDe.Text
                            Case 1
                                txtGrossTxt.Text = str_gross & vbNewLine & str_USDInvoice
                            Case Else
                                If Cpage > 1 Then
                                    str_gross = ""
                                    txtGrossTxt.Text = str_gross '& vbNewLine & str_USDInvoice
                                ElseIf Cpage = 1 Then

                                    txtGrossTxt.Text = str_gross & vbNewLine & str_USDInvoice
                                End If
                        End Select
                    ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "" Then
                        str_gross = GrossTxt() '& vbNewLine & FobTxt()
                        Select Case C_TotalRowDe.Text
                            Case 1
                                txtGrossTxt.Text = str_gross & vbNewLine & str_USDInvoice
                            Case Else
                                If Cpage > 1 Then
                                    str_gross = ""
                                    txtGrossTxt.Text = str_gross '& vbNewLine & str_USDInvoice
                                ElseIf Cpage = 1 Then

                                    txtGrossTxt.Text = str_gross & vbNewLine & str_USDInvoice
                                End If
                        End Select
                    End If
                ElseIf Mid(CommonUtility.Get_StringValue(txtFOBDisplay.Value), 1, 50) = "NoDisplay" Then
                    If Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "NET WEIGHT" Then
                        str_gross = NetWeight_(txtnet_weight.Value, txtunit_code2.Value) 'Mid(txtnet_weight.Value, 1, 9) & " " & Mid(txtunit_code2.Value, 1, 10)
                        Select Case C_TotalRowDe.Text
                            Case 1
                                txtGrossTxt.Text = str_gross & vbNewLine & str_USDInvoice
                            Case Else
                                If Cpage > 1 Then
                                    str_gross = ""
                                    txtGrossTxt.Text = str_gross '& vbNewLine & str_USDInvoice
                                ElseIf Cpage = 1 Then

                                    txtGrossTxt.Text = str_gross & vbNewLine & str_USDInvoice
                                End If
                        End Select
                    ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "GROSS WEIGHT - NET WEIGHT" Then
                        str_gross = GrossTxt() & vbNewLine & NetWeight_(txtnet_weight.Value, txtunit_code2.Value) 'Mid(txtnet_weight.Value, 1, 9) & " " & Mid(txtunit_code2.Value, 1, 10)
                        Select Case C_TotalRowDe.Text
                            Case 1
                                txtGrossTxt.Text = str_gross & vbNewLine & str_USDInvoice
                            Case Else
                                If Cpage > 1 Then
                                    str_gross = ""
                                    txtGrossTxt.Text = str_gross '& vbNewLine & str_USDInvoice
                                ElseIf Cpage = 1 Then

                                    txtGrossTxt.Text = str_gross & vbNewLine & str_USDInvoice
                                End If
                        End Select
                    ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "GROSS WEIGHT" Then
                        str_gross = GrossTxt()
                        Select Case C_TotalRowDe.Text
                            Case 1
                                txtGrossTxt.Text = str_gross & vbNewLine & str_USDInvoice
                            Case Else
                                If Cpage > 1 Then
                                    str_gross = ""
                                    txtGrossTxt.Text = str_gross '& vbNewLine & str_USDInvoice
                                ElseIf Cpage = 1 Then

                                    txtGrossTxt.Text = str_gross & vbNewLine & str_USDInvoice
                                End If
                        End Select
                    ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "" Then
                        str_gross = GrossTxt()
                        Select Case C_TotalRowDe.Text
                            Case 1
                                txtGrossTxt.Text = str_gross & vbNewLine & str_USDInvoice
                            Case Else
                                If Cpage > 1 Then
                                    str_gross = ""
                                    txtGrossTxt.Text = str_gross '& vbNewLine & str_USDInvoice
                                ElseIf Cpage = 1 Then

                                    txtGrossTxt.Text = str_gross & vbNewLine & str_USDInvoice
                                End If
                        End Select
                    End If

                ElseIf Mid(CommonUtility.Get_StringValue(txtFOBDisplay.Value), 1, 50) = "" Then

                    If Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "NET WEIGHT" Then
                        str_gross = NetWeight_(txtnet_weight.Value, txtunit_code2.Value) 'Mid(txtnet_weight.Value, 1, 9) & " " & Mid(txtunit_code2.Value, 1, 10) '& vbNewLine & FobTxt()
                        Select Case C_TotalRowDe.Text
                            Case 1
                                txtGrossTxt.Text = str_gross & vbNewLine & str_USDInvoice
                            Case Else
                                If Cpage > 1 Then
                                    str_gross = ""
                                    txtGrossTxt.Text = str_gross '& vbNewLine & str_USDInvoice
                                ElseIf Cpage = 1 Then

                                    txtGrossTxt.Text = str_gross & vbNewLine & str_USDInvoice
                                End If
                        End Select
                    ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "GROSS WEIGHT - NET WEIGHT" Then
                        str_gross = GrossTxt() & vbNewLine & NetWeight_(txtnet_weight.Value, txtunit_code2.Value) 'Mid(txtnet_weight.Value, 1, 9) & " " & Mid(txtunit_code2.Value, 1, 10) '& vbNewLine & FobTxt()
                        Select Case C_TotalRowDe.Text
                            Case 1
                                txtGrossTxt.Text = str_gross & vbNewLine & str_USDInvoice
                            Case Else
                                If Cpage > 1 Then
                                    str_gross = ""
                                    txtGrossTxt.Text = str_gross '& vbNewLine & str_USDInvoice
                                ElseIf Cpage = 1 Then

                                    txtGrossTxt.Text = str_gross & vbNewLine & str_USDInvoice
                                End If
                        End Select
                    ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "GROSS WEIGHT" Then
                        str_gross = GrossTxt() '& vbNewLine & FobTxt()
                        Select Case C_TotalRowDe.Text
                            Case 1
                                txtGrossTxt.Text = str_gross & vbNewLine & str_USDInvoice
                            Case Else
                                If Cpage > 1 Then
                                    str_gross = ""
                                    txtGrossTxt.Text = str_gross '& vbNewLine & str_USDInvoice
                                ElseIf Cpage = 1 Then

                                    txtGrossTxt.Text = str_gross & vbNewLine & str_USDInvoice
                                End If
                        End Select
                    ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "" Then
                        str_gross = GrossTxt() '& vbNewLine & FobTxt()
                        Select Case C_TotalRowDe.Text
                            Case 1
                                txtGrossTxt.Text = str_gross & vbNewLine & str_USDInvoice
                            Case Else
                                If Cpage > 1 Then
                                    str_gross = ""
                                    txtGrossTxt.Text = str_gross '& vbNewLine & str_USDInvoice
                                ElseIf Cpage = 1 Then

                                    txtGrossTxt.Text = str_gross & vbNewLine & str_USDInvoice
                                End If
                        End Select
                    End If
                End If

            Case Else '����� invoice ��ҧ�����
                If Mid(CommonUtility.Get_StringValue(txtFOBDisplay.Value), 1, 50) = "Display" Then

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
                        str_gross = GrossTxt() & vbNewLine & FobTxt()
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

        End Select

    End Sub
    Function CHeck_DisPlayF() As String
        CallInvoice_()
        Dim str_GrossF As String

        Dim sss As String = txtgross_weight.Text

        Dim str_gross As String

        'str_gross = txtgross_weight.Text

        '����ͧ invoice ��ҧ�����
        Dim str_NumInvoice As String = CommonUtility.Get_StringValue(txtNumInvoice.Text)
        '����ͧ ��Ť�� ��ҧ�����
        Dim str_USDInvoice As String
        If CommonUtility.Get_StringValue(txtUSDInvoice.Value) <> "" Then
            str_USDInvoice = Format(CommonUtility.Get_Decimal(txtUSDInvoice.Value), "#,##0.00") & " USD"
            'str_USDInvoice = Format(CommonUtility.Get_Decimal(txtUSDInvoice.Value), "#,###.#0") & " USD"
        Else
            str_USDInvoice = ""
        End If

        Select Case txtshow_check.Text
            Case "1000", "10000" ' ����ͧ invoice ��ҧ�����
                If Mid(CommonUtility.Get_StringValue(txtFOBDisplay.Value), 1, 50) = "Display" Then

                    If Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "NET WEIGHT" Then

                        str_gross = NetWeight_(TNet, txtunit_code2.Value) 'Mid(Tnet, 1, 9) & " " & Mid(txtunit_code2.Value, 1, 10) '& vbNewLine & FobTxtAll()
                        str_GrossF = str_gross & vbNewLine & str_USDInvoice

                    ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "GROSS WEIGHT - NET WEIGHT" Then
                        str_gross = GrossTxt() & vbNewLine & NetWeight_(TNet, txtunit_code2.Value) 'Mid(Tnet, 1, 9) & " " & Mid(txtunit_code2.Value, 1, 10) '& vbNewLine & FobTxtAll()
                        str_GrossF = str_gross & vbNewLine & str_USDInvoice
                    ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "GROSS WEIGHT" Then
                        str_gross = GrossTxt() '& vbNewLine & FobTxtAll()
                        str_GrossF = str_gross & vbNewLine & str_USDInvoice
                    ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "GW" Then
                        str_gross = GrossTxt() '& vbNewLine & FobTxtAll()
                        str_GrossF = str_gross & vbNewLine & Callinvoice_board(1)
                    ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "" Then
                        str_gross = GrossTxt() '& vbNewLine & FobTxtAll()
                        str_GrossF = str_gross & vbNewLine & str_USDInvoice
                    End If
                ElseIf Mid(CommonUtility.Get_StringValue(txtFOBDisplay.Value), 1, 50) = "NoDisplay" Then
                    If Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "NET WEIGHT" Then
                        str_gross = NetWeight_(TNet, txtunit_code2.Value) 'Mid(Tnet, 1, 9) & " " & Mid(txtunit_code2.Value, 1, 10)
                        str_GrossF = str_gross & vbNewLine & str_USDInvoice
                    ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "GROSS WEIGHT - NET WEIGHT" Then
                        str_gross = GrossTxt() & vbNewLine & NetWeight_(TNet, txtunit_code2.Value) 'Mid(Tnet, 1, 9) & " " & Mid(txtunit_code2.Value, 1, 10)
                        str_GrossF = str_gross & vbNewLine & str_USDInvoice
                    ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "GROSS WEIGHT" Then
                        str_gross = GrossTxt()
                        str_GrossF = str_gross & vbNewLine & str_USDInvoice
                    ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "" Then
                        str_gross = GrossTxt()
                        str_GrossF = str_gross & vbNewLine & str_USDInvoice
                    End If

                ElseIf Mid(CommonUtility.Get_StringValue(txtFOBDisplay.Value), 1, 50) = "" Then

                    If Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "NET WEIGHT" Then
                        str_gross = NetWeight_(TNet, txtunit_code2.Value) 'Mid(Tnet, 1, 9) & " " & Mid(txtunit_code2.Value, 1, 10) '& vbNewLine & FobTxtAll()
                        str_GrossF = str_gross & vbNewLine & str_USDInvoice
                    ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "GROSS WEIGHT - NET WEIGHT" Then
                        str_gross = GrossTxt() & vbNewLine & NetWeight_(TNet, txtunit_code2.Value) 'Mid(Tnet, 1, 9) & " " & Mid(txtunit_code2.Value, 1, 10) '& vbNewLine & FobTxtAll()
                        str_GrossF = str_gross & vbNewLine & str_USDInvoice
                    ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "GROSS WEIGHT" Then
                        str_gross = GrossTxt() '& vbNewLine & FobTxtAll()
                        str_GrossF = str_gross & vbNewLine & str_USDInvoice
                    ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "" Then
                        str_gross = GrossTxt() '& vbNewLine & FobTxtAll()
                        str_GrossF = str_gross & vbNewLine & str_USDInvoice
                    End If
                End If

            Case Else '����� invoice ��ҧ�����
                If Mid(CommonUtility.Get_StringValue(txtFOBDisplay.Value), 1, 50) = "Display" Then

                    If Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "NET WEIGHT" Then

                        str_gross = NetWeight_(TNet, txtunit_code2.Value) & vbNewLine & FobTxtAllSum() 'Mid(Tnet, 1, 9) & " " & Mid(txtunit_code2.Value, 1, 10) '& vbNewLine & FobTxtAll()
                        str_GrossF = str_gross

                    ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "GROSS WEIGHT - NET WEIGHT" Then
                        str_gross = GrossTxt() & vbNewLine & NetWeight_(TNet, txtunit_code2.Value) & vbNewLine & FobTxtAllSum() 'Mid(Tnet, 1, 9) & " " & Mid(txtunit_code2.Value, 1, 10) '& vbNewLine & FobTxtAll()
                        str_GrossF = str_gross
                    ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "GROSS WEIGHT" Then
                        str_gross = GrossTxt() & vbNewLine & FobTxtAllSum()
                        str_GrossF = str_gross
                    ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "" Then
                        str_gross = GrossTxt() & vbNewLine & FobTxtAllSum()
                        str_GrossF = str_gross
                    ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "GW" Then '�к����
                        str_gross = GrossTxt() & vbNewLine & FobTxtAllSum()
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
                    End If

                ElseIf Mid(CommonUtility.Get_StringValue(txtFOBDisplay.Value), 1, 50) = "" Then

                    If Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "NET WEIGHT" Then
                        str_gross = NetWeight_(TNet, txtunit_code2.Value) 'Mid(Tnet, 1, 9) & " " & Mid(txtunit_code2.Value, 1, 10) '& vbNewLine & FobTxtAll()
                        str_GrossF = str_gross
                    ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "GROSS WEIGHT - NET WEIGHT" Then
                        str_gross = GrossTxt() & vbNewLine & NetWeight_(TNet, txtunit_code2.Value) 'Mid(Tnet, 1, 9) & " " & Mid(txtunit_code2.Value, 1, 10) '& vbNewLine & FobTxtAll()
                        str_GrossF = str_gross
                    ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "GROSS WEIGHT" Then
                        str_gross = GrossTxt() '& vbNewLine & FobTxtAll()
                        str_GrossF = str_gross
                    ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "" Then
                        str_gross = GrossTxt() '& vbNewLine & FobTxtAll()
                        str_GrossF = str_gross
                    End If
                End If

        End Select
        Return str_GrossF
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
    Sub Check_box8()
        Select Case CommonUtility.Get_StringValue(txtbox8.Value)
            Case """RVC+CTSH"""
                txtTemp_box8.Text = """RVC" & " " & txtprofit_per_unit.Value & "%" & vbNewLine & "+ CTSH" & """"
            Case """CTSH+RVC""" '�к����
                txtTemp_box8.Text = """RVC" & " " & txtprofit_per_unit.Value & "%" & vbNewLine & "+ CTSH" & """"
            Case Else
                txtTemp_box8.Text = txtbox8.Value
        End Select
    End Sub

    'check third ex back
    Sub check_show_check()
        Select Case txtshow_check.Value
            Case "1000", "10000" 'Third-Country 
                If txtthird_country.Value <> "" Then
                    PCheck_third_country.Visible = True
                    txtTemp_back_country.Text = "THAILAND"
                Else
                    PCheck_third_country.Visible = False
                    txtTemp_back_country.Text = "THAILAND"
                End If
            Case "0100", "01000" 'Exhibittion 
                PCheck_place_exibition.Visible = True
                txtTemp_back_country.Text = "THAILAND"
            Case "0010", "00110" 'Back-to-Back CO
                If txtback_country.Value <> "" Then
                    PCheck_back_country.Visible = True

                    txtTemp_back_country.Text = txtback_country.Value

                Else
                    PCheck_back_country.Visible = False

                    txtTemp_back_country.Text = "THAILAND"
                End If
            Case "0001", "00001" 'Cumulation
                PCheck_Cumulation.Visible = True
                txtTemp_back_country.Text = "THAILAND"
            Case Else
                txtTemp_back_country.Text = "THAILAND"
        End Select
        
    End Sub

    Private Sub rpt3_ediFORM4_9_pr_PageEnd(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PageEnd
        head_height = PageHeader1.Height

        Dim f As New System.Drawing.Font("BrowalliaUPC", 11)

        Dim xm As Single = 7
        Dim ym As Single = CSng(head_height) '4.896
        'Dim width As Single = 6.0F
        Dim widthm As Single = 0.94
        Dim heightm As Single = 4
        Dim drawRectm As New Drawing.RectangleF(xm, ym, widthm, heightm)

        With Me.CurrentPage
            .Font = f
            .ForeColor = System.Drawing.Color.Blue
            .TextAlignment = DataDynamics.ActiveReports.TextAlignment.Center
            .VerticalTextAlignment = VerticalTextAlignment.Top
            '.BackColor = Drawing.Color.Green
            '.TextAngle = 900
            '.VerticalTextAlignment = DataDynamics.ActiveReports.VerticalTextAlignment.Middle
            .DrawText(CallInvoice_, drawRectm)
        End With
        '============================
        Dim x_txtGrossTxt As Single = 6
        Dim y_txtGrossTxt As Single = CSng(head_height) '4.896
        'Dim width As Single = 6.0F
        Dim width_txtGrossTxt As Single = 0.94
        Dim height_txtGrossTxt As Single = 4
        Dim drawRect_txtGrossTxt As New Drawing.RectangleF(x_txtGrossTxt, y_txtGrossTxt, width_txtGrossTxt, height_txtGrossTxt)

        With Me.CurrentPage
            .Font = f
            .ForeColor = System.Drawing.Color.Blue
            .TextAlignment = DataDynamics.ActiveReports.TextAlignment.Center
            .VerticalTextAlignment = VerticalTextAlignment.Top
            '.BackColor = Drawing.Color.Green
            '.TextAngle = 900
            '.VerticalTextAlignment = DataDynamics.ActiveReports.VerticalTextAlignment.Middle
            .DrawText(CHeck_DisPlayF, drawRect_txtGrossTxt)
        End With
        '============================
        Dim x_txtmarks As Single = 0.75
        Dim y_txtmarks As Single = CSng(head_height) '4.896
        'Dim width As Single = 6.0F
        Dim width_txtmarks As Single = 1
        Dim height_txtmarks As Single = 4
        Dim drawRect_txtmarks As New Drawing.RectangleF(x_txtmarks, y_txtmarks, width_txtmarks, height_txtmarks)

        With Me.CurrentPage
            .Font = f
            .ForeColor = System.Drawing.Color.Blue
            .TextAlignment = DataDynamics.ActiveReports.TextAlignment.Left
            .VerticalTextAlignment = VerticalTextAlignment.Top
            '.BackColor = Drawing.Color.Green
            '.TextAngle = 900
            '.VerticalTextAlignment = DataDynamics.ActiveReports.VerticalTextAlignment.Middle
            .DrawText(str_mark, drawRect_txtmarks)
        End With
    End Sub

    Private Sub rpt3_ediFORM4_9_pr_PageStart(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PageStart
        Me.CurrentPage.DrawLine(0.06, 4.13, 0.06, 7.02)
        Me.CurrentPage.DrawLine(0.72, 4.13, 0.72, 7.02)
        Me.CurrentPage.DrawLine(1.8, 4.13, 1.8, 7.02)
        Me.CurrentPage.DrawLine(5.2, 4.13, 5.2, 7.02)
        Me.CurrentPage.DrawLine(6.0, 4.13, 6.0, 7.02)
        Me.CurrentPage.DrawLine(7.1, 4.13, 7.1, 7.02)
        Me.CurrentPage.DrawLine(8.19, 4.13, 8.19, 7.02)
    End Sub

    Private Sub rpt3_ediFORM4_9_pr_ReportEnd(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.ReportEnd
        ''If updatePrintTotal(txtinvh_run_auto.Value, CStr(Me.Document.Pages.Count)) = True Then

        ''End If
    End Sub
    
    Private Sub rpt3_ediFORM4_9_pr_ReportStart(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.ReportStart
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
