Imports DataDynamics.ActiveReports
Imports DataDynamics.ActiveReports.Document

Imports Microsoft.ApplicationBlocks.Data
Imports System.Data.SqlClient
Imports System.Data
Imports System.Text
Imports DFT.Dotnetnuke.ClassLibrary
Imports ReportPrintClass
Public Class rpt3_ediFORM2_pr
    Dim Cpage As Integer
    Dim CpageNum As Integer
    Private Sub PageFooter1_Format(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PageFooter1.Format
        Select Case txtSendCheckSeletedate.Text
            Case True '���ѹ������͡�ͧ
                txtcompany_provincefoot.Text = txtcompany_provincefoot1.Text & " " & txtdateSelectRPT.Text 'format_dateSelect()
            Case False '�� �ѹ͹��ѵ�
                txtcompany_provincefoot.Text = txtcompany_provincefoot1.Text & " " & CommonUtility.Get_DateTime(txtapprove_date.Text).ToString("dd/MM/yyyy") 'format_dateSelect()
        End Select

        Dim dsESS As DataSet = search_imageForm(txtinvh_run_auto.Text)
        If dsESS.Tables(0).Rows.Count > 0 Then
            CaseCheck_numimagesSign(dsESS)
            CaseCheck_ApproveSign(dsESS)
        End If
    End Sub
    'Dim TGross, TNet, TFob, TUSD As Decimal
    Dim TGross, TNet As Decimal
    Dim Str_USDinvoiceDetail As String
    Dim str_mark As String

    'by rut Title New Begin---------------
    Dim Str_TitleDe As String
    Dim Str_TitleDe2 As String
    'by rut Title New End-----------------
    Private Sub Detail1_Format(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Detail1.Format
        
        '=================
        Cpage += 1
        '===============================
        TGross += txtgross_weightD.Value
        TNet += txtnet_weight.Value
        
        '===============================
        Count_RowToPage_DetailData()

        txtNumRowCount.Text = Cpage & "."
        Check_box8()

        If Cpage = 1 Then
            str_mark = CommonUtility.Get_StringValue(txtmarks.Text) '& "" & CommonUtility.Get_StringValue(txtmarks1.Text)
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

    'by rut Title New Begin-----------------
    Dim Str_TitlePage As String
    'by rut Title New End-------------------
    'by rut ��Ѻ page end ���͡�˹�
    Dim head_height As String
    Private Sub PageHeader1_Format(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PageHeader1.Format
        CpageNum += 1
        txtreference_code2_Temp.Text = txtreference_code2.Text
        '===============================================================
        Check_ship_by()
        'txtPage2.Text = Cpage
        '===============================================================
        Head_Checkcompany_v1()
        Head_Checkdestination_v2()

        new_AllCountry()

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
    Sub new_AllCountry()
        Dim str_ As String

        str_ = CommonUtility.Get_StringValue(txtReserve1.Text) & " " & CommonUtility.Get_StringValue(Check_NULLALL(txtIMPORT_COUNTRY.Text))
        If CommonUtility.Get_StringValue(txtReserve1.Text) = "" Then
            txtTemp_AllCountry.Text = CommonUtility.Get_StringValue(txtIMPORT_COUNTRY.Text)
        Else
            Select Case Len(str_)
                Case Is > 24
                    txtTemp_AllCountry.Text = CommonUtility.Get_StringValue(txtReserve1.Text) & vbNewLine & CommonUtility.Get_StringValue(Check_NULLALL(txtIMPORT_COUNTRY.Text))
                Case Else
                    txtTemp_AllCountry.Text = str_
            End Select
        End If

    End Sub

    Sub Check_ship_by()
        Select Case CommonUtility.Get_StringValue(txtship_by.Text)
            Case "0"
                txtship_by_0.Visible = True
                txtship_by_0.Text = "X"

                txtship_by_1.Visible = False
                txtship_by_2.Visible = False
                txtship_by_3.Visible = False
            Case "1"
                txtship_by_1.Visible = True
                txtship_by_1.Text = "X"

                txtship_by_0.Visible = False
                txtship_by_2.Visible = False
                txtship_by_3.Visible = False
            Case "2"
                txtship_by_2.Visible = True
                txtship_by_2.Text = "X"

                txtship_by_0.Visible = False
                txtship_by_1.Visible = False
                txtship_by_3.Visible = False
            Case "3"
                txtship_by_3.Visible = True
                txtship_by_3.Text = "X"

                txtship_by_0.Visible = False
                txtship_by_1.Visible = False
                txtship_by_2.Visible = False
        End Select

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

        End If
    End Sub
    Sub Head_Checkdestination_v2()
        Select Case Check_RFC_Tax(CommonUtility.Get_StringValue(txtdestination_taxid.Text))
            Case True 'rfc
                If Mid(txtdest_remark1.Text, 1, 3) = "C/O" Then
                    txtdestination_Check2.Text = txtOB_Dest_Address.Text & IIf(Check_NULLALL(txtNewEmail_ch02.Text) <> "", " E-mail: " & txtNewEmail_ch02.Text, "") & " CARE OF " & txtdestination_company.Text & " " & txtdestination_address.Text _
                                        & " " & txtdestination_province.Text & " " & txtdest_Receive_country.Text & _
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") & _
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") & _
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " " & txtdestination_taxid.Text, "") & _
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") & _
                                        IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "")

                ElseIf Mid(txtdest_remark1.Text, 1, 3) = "O/B" Then
                    txtdestination_Check2.Text = txtdestination_company.Text & " " & txtdestination_address.Text & " " & _
                                            txtdestination_province.Text & " " & txtdest_Receive_country.Text & _
                                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") & _
                                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                            & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " " & txtdestination_taxid.Text, "") & _
                                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") & _
                                            " ON BEHALF OF " & txtOB_Dest_Address.Text & IIf(Check_NULLALL(txtNewEmail_ch02.Text) <> "", " E-mail: " & txtNewEmail_ch02.Text, "") & IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "")

                ElseIf Mid(txtdest_remark1.Text, 2, 1) <> "/" Then
                    txtdestination_Check2.Text = txtdestination_company.Text & " " & txtdestination_address.Text & " " & txtdestination_province.Text _
                                        & " " & txtdest_Receive_country.Text & _
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") & _
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                        & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " " & txtdestination_taxid.Text, "") & _
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") & _
                                        IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "")

                End If
            Case False '����� rfc
                If Mid(txtdest_remark1.Text, 1, 3) = "C/O" Then
                    txtdestination_Check2.Text = txtOB_Dest_Address.Text & IIf(Check_NULLALL(txtNewEmail_ch02.Text) <> "", " E-mail: " & txtNewEmail_ch02.Text, "") & " CARE OF " & txtdestination_company.Text & " " & txtdestination_address.Text _
                                        & " " & txtdestination_province.Text & " " & txtdest_Receive_country.Text & _
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") & _
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") & _
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") & _
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") & _
                                        IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "")

                ElseIf Mid(txtdest_remark1.Text, 1, 3) = "O/B" Then
                    txtdestination_Check2.Text = txtdestination_company.Text & " " & txtdestination_address.Text & " " & _
                                            txtdestination_province.Text & " " & txtdest_Receive_country.Text & _
                                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") & _
                                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                            & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") & _
                                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") & _
                                            " ON BEHALF OF " & txtOB_Dest_Address.Text & IIf(Check_NULLALL(txtNewEmail_ch02.Text) <> "", " E-mail: " & txtNewEmail_ch02.Text, "") & IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "")

                ElseIf Mid(txtdest_remark1.Text, 2, 1) <> "/" Then
                    txtdestination_Check2.Text = txtdestination_company.Text & " " & txtdestination_address.Text & " " & txtdestination_province.Text _
                                        & " " & txtdest_Receive_country.Text & _
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") & _
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                        & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") & _
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") & _
                                        IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "")

                End If
        End Select

    End Sub
    'Check RFC
    Function Check_RFC_Tax(ByVal ValueTaxID As String) As Boolean
        If Mid(ValueTaxID, 1, 3) = "RFC" Or Mid(ValueTaxID, 1, 3) = "rfc" Then
            Return True
        Else
            Return False
        End If
    End Function

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
        '����ͧ invoice ��ҧ�����
        Dim str_NumInvoice As String = CommonUtility.Get_StringValue(Check_NULLALL(txtNumInvoice.Text))

        Select Case str_NumInvoice
            Case Is <> ""
                txtTolInvoice.Text = txtNumInvoice.Text
            Case Else
                If CommonUtility.Get_String(txtinvoice_no1.Text) <> "" And CommonUtility.Get_String(txtinvoice_date1.Text) <> "" And CommonUtility.Get_String(txtinvoice_no2.Text) = "" And CommonUtility.Get_String(txtinvoice_date2.Text) = "" And CommonUtility.Get_String(txtinvoice_no3.Text) = "" And CommonUtility.Get_String(txtinvoice_date3.Text) = "" And CommonUtility.Get_String(txtinvoice_no4.Text) = "" And CommonUtility.Get_String(txtinvoice_date4.Text) = "" And CommonUtility.Get_String(txtinvoice_no5.Text) = "" And CommonUtility.Get_String(txtinvoice_date5.Text) = "" Then
                    '1 invoice
                    txtTolInvoice.Text = txtinvoice_no1.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date1.Text) <> "", confor_mat(CDate(txtinvoice_date1.Value).Date), Nothing)

                ElseIf CommonUtility.Get_String(txtinvoice_no1.Text) <> "" And CommonUtility.Get_String(txtinvoice_date1.Text) <> "" And CommonUtility.Get_String(txtinvoice_no2.Text) <> "" And CommonUtility.Get_String(txtinvoice_date2.Text) <> "" And CommonUtility.Get_String(txtinvoice_no3.Text) = "" And CommonUtility.Get_String(txtinvoice_date3.Text) = "" And CommonUtility.Get_String(txtinvoice_no4.Text) = "" And CommonUtility.Get_String(txtinvoice_date4.Text) = "" And CommonUtility.Get_String(txtinvoice_no5.Text) = "" And CommonUtility.Get_String(txtinvoice_date5.Text) = "" Then
                    '2 invoice
                    txtTolInvoice.Text = txtinvoice_no1.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date1.Text) <> "", confor_mat(CDate(txtinvoice_date1.Value).Date), Nothing) & vbNewLine _
                                        & txtinvoice_no2.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date2.Text) <> "", confor_mat(CDate(txtinvoice_date2.Value).Date), Nothing)

                ElseIf CommonUtility.Get_String(txtinvoice_no1.Text) <> "" And CommonUtility.Get_String(txtinvoice_date1.Text) <> "" And CommonUtility.Get_String(txtinvoice_no2.Text) <> "" And CommonUtility.Get_String(txtinvoice_date2.Text) <> "" And CommonUtility.Get_String(txtinvoice_no3.Text) <> "" And CommonUtility.Get_String(txtinvoice_date3.Text) <> "" And CommonUtility.Get_String(txtinvoice_no4.Text) = "" And CommonUtility.Get_String(txtinvoice_date4.Text) = "" And CommonUtility.Get_String(txtinvoice_no5.Text) = "" And CommonUtility.Get_String(txtinvoice_date5.Text) = "" Then
                    '3 invoice
                    txtTolInvoice.Text = txtinvoice_no1.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date1.Text) <> "", confor_mat(CDate(txtinvoice_date1.Value).Date), Nothing) & vbNewLine _
                                        & txtinvoice_no2.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date2.Text) <> "", confor_mat(CDate(txtinvoice_date2.Value).Date), Nothing) & vbNewLine _
                                        & txtinvoice_no3.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date3.Text) <> "", confor_mat(CDate(txtinvoice_date3.Value).Date), Nothing)

                ElseIf CommonUtility.Get_String(txtinvoice_no1.Text) <> "" And CommonUtility.Get_String(txtinvoice_date1.Text) <> "" And CommonUtility.Get_String(txtinvoice_no2.Text) <> "" And CommonUtility.Get_String(txtinvoice_date2.Text) <> "" And CommonUtility.Get_String(txtinvoice_no3.Text) <> "" And CommonUtility.Get_String(txtinvoice_date3.Text) <> "" And CommonUtility.Get_String(txtinvoice_no4.Text) <> "" And CommonUtility.Get_String(txtinvoice_date4.Text) <> "" And CommonUtility.Get_String(txtinvoice_no5.Text) = "" And CommonUtility.Get_String(txtinvoice_date5.Text) = "" Then
                    '4 invoice
                    txtTolInvoice.Text = txtinvoice_no1.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date1.Text) <> "", confor_mat(CDate(txtinvoice_date1.Value).Date), Nothing) & vbNewLine _
                                        & txtinvoice_no2.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date2.Text) <> "", confor_mat(CDate(txtinvoice_date2.Value).Date), Nothing) & vbNewLine _
                                        & txtinvoice_no3.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date3.Text) <> "", confor_mat(CDate(txtinvoice_date3.Value).Date), Nothing) & vbNewLine _
                                        & txtinvoice_no4.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date4.Text) <> "", confor_mat(CDate(txtinvoice_date4.Value).Date), Nothing)

                ElseIf CommonUtility.Get_String(txtinvoice_no1.Text) <> "" And CommonUtility.Get_String(txtinvoice_date1.Text) <> "" And CommonUtility.Get_String(txtinvoice_no2.Text) <> "" And CommonUtility.Get_String(txtinvoice_date2.Text) <> "" And CommonUtility.Get_String(txtinvoice_no3.Text) <> "" And CommonUtility.Get_String(txtinvoice_date3.Text) <> "" And CommonUtility.Get_String(txtinvoice_no4.Text) <> "" And CommonUtility.Get_String(txtinvoice_date4.Text) <> "" And CommonUtility.Get_String(txtinvoice_no5.Text) <> "" And CommonUtility.Get_String(txtinvoice_date5.Text) <> "" Then
                    '5 invoice
                    txtTolInvoice.Text = txtinvoice_no1.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date1.Text) <> "", confor_mat(CDate(txtinvoice_date1.Value).Date), Nothing) & vbNewLine _
                                        & txtinvoice_no2.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date2.Text) <> "", confor_mat(CDate(txtinvoice_date2.Value).Date), Nothing) & vbNewLine _
                                        & txtinvoice_no3.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date3.Text) <> "", confor_mat(CDate(txtinvoice_date3.Value).Date), Nothing) & vbNewLine _
                                        & txtinvoice_no4.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date4.Text) <> "", confor_mat(CDate(txtinvoice_date4.Value).Date), Nothing) & vbNewLine _
                                        & txtinvoice_no5.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date5.Text) <> "", confor_mat(CDate(txtinvoice_date5.Value).Date), Nothing)

                End If
        End Select

    End Sub
    Function CallInvoiceCheckF() As String 'Check Invoince
        '����ͧ invoice ��ҧ�����
        Dim str_NumInvoice As String = CommonUtility.Get_StringValue(Check_NULLALL(txtNumInvoice.Text))
        Dim str_invoice As String
        Select Case str_NumInvoice
            Case Is <> ""
                str_invoice = txtNumInvoice.Text
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
                'If CommonUtility.Get_String(txtinvoice_no1.Text) <> "" And CommonUtility.Get_String(txtinvoice_date1.Text) <> "" And CommonUtility.Get_String(txtinvoice_no2.Text) = "" And CommonUtility.Get_String(txtinvoice_date2.Text) = "" And CommonUtility.Get_String(txtinvoice_no3.Text) = "" And CommonUtility.Get_String(txtinvoice_date3.Text) = "" And CommonUtility.Get_String(txtinvoice_no4.Text) = "" And CommonUtility.Get_String(txtinvoice_date4.Text) = "" And CommonUtility.Get_String(txtinvoice_no5.Text) = "" And CommonUtility.Get_String(txtinvoice_date5.Text) = "" Then
                '    '1 invoice
                '    str_invoice = txtinvoice_no1.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date1.Text) <> "", confor_mat(CDate(txtinvoice_date1.Value).Date), Nothing)

                'ElseIf CommonUtility.Get_String(txtinvoice_no1.Text) <> "" And CommonUtility.Get_String(txtinvoice_date1.Text) <> "" And CommonUtility.Get_String(txtinvoice_no2.Text) <> "" And CommonUtility.Get_String(txtinvoice_date2.Text) <> "" And CommonUtility.Get_String(txtinvoice_no3.Text) = "" And CommonUtility.Get_String(txtinvoice_date3.Text) = "" And CommonUtility.Get_String(txtinvoice_no4.Text) = "" And CommonUtility.Get_String(txtinvoice_date4.Text) = "" And CommonUtility.Get_String(txtinvoice_no5.Text) = "" And CommonUtility.Get_String(txtinvoice_date5.Text) = "" Then
                '    '2 invoice
                '    str_invoice = txtinvoice_no1.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date1.Text) <> "", confor_mat(CDate(txtinvoice_date1.Value).Date), Nothing) & vbNewLine _
                '                        & txtinvoice_no2.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date2.Text) <> "", confor_mat(CDate(txtinvoice_date2.Value).Date), Nothing)

                'ElseIf CommonUtility.Get_String(txtinvoice_no1.Text) <> "" And CommonUtility.Get_String(txtinvoice_date1.Text) <> "" And CommonUtility.Get_String(txtinvoice_no2.Text) <> "" And CommonUtility.Get_String(txtinvoice_date2.Text) <> "" And CommonUtility.Get_String(txtinvoice_no3.Text) <> "" And CommonUtility.Get_String(txtinvoice_date3.Text) <> "" And CommonUtility.Get_String(txtinvoice_no4.Text) = "" And CommonUtility.Get_String(txtinvoice_date4.Text) = "" And CommonUtility.Get_String(txtinvoice_no5.Text) = "" And CommonUtility.Get_String(txtinvoice_date5.Text) = "" Then
                '    '3 invoice
                '    str_invoice = txtinvoice_no1.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date1.Text) <> "", confor_mat(CDate(txtinvoice_date1.Value).Date), Nothing) & vbNewLine _
                '                        & txtinvoice_no2.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date2.Text) <> "", confor_mat(CDate(txtinvoice_date2.Value).Date), Nothing) & vbNewLine _
                '                        & txtinvoice_no3.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date3.Text) <> "", confor_mat(CDate(txtinvoice_date3.Value).Date), Nothing)

                'ElseIf CommonUtility.Get_String(txtinvoice_no1.Text) <> "" And CommonUtility.Get_String(txtinvoice_date1.Text) <> "" And CommonUtility.Get_String(txtinvoice_no2.Text) <> "" And CommonUtility.Get_String(txtinvoice_date2.Text) <> "" And CommonUtility.Get_String(txtinvoice_no3.Text) <> "" And CommonUtility.Get_String(txtinvoice_date3.Text) <> "" And CommonUtility.Get_String(txtinvoice_no4.Text) <> "" And CommonUtility.Get_String(txtinvoice_date4.Text) <> "" And CommonUtility.Get_String(txtinvoice_no5.Text) = "" And CommonUtility.Get_String(txtinvoice_date5.Text) = "" Then
                '    '4 invoice
                '    str_invoice = txtinvoice_no1.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date1.Text) <> "", confor_mat(CDate(txtinvoice_date1.Value).Date), Nothing) & vbNewLine _
                '                        & txtinvoice_no2.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date2.Text) <> "", confor_mat(CDate(txtinvoice_date2.Value).Date), Nothing) & vbNewLine _
                '                        & txtinvoice_no3.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date3.Text) <> "", confor_mat(CDate(txtinvoice_date3.Value).Date), Nothing) & vbNewLine _
                '                        & txtinvoice_no4.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date4.Text) <> "", confor_mat(CDate(txtinvoice_date4.Value).Date), Nothing)

                'ElseIf CommonUtility.Get_String(txtinvoice_no1.Text) <> "" And CommonUtility.Get_String(txtinvoice_date1.Text) <> "" And CommonUtility.Get_String(txtinvoice_no2.Text) <> "" And CommonUtility.Get_String(txtinvoice_date2.Text) <> "" And CommonUtility.Get_String(txtinvoice_no3.Text) <> "" And CommonUtility.Get_String(txtinvoice_date3.Text) <> "" And CommonUtility.Get_String(txtinvoice_no4.Text) <> "" And CommonUtility.Get_String(txtinvoice_date4.Text) <> "" And CommonUtility.Get_String(txtinvoice_no5.Text) <> "" And CommonUtility.Get_String(txtinvoice_date5.Text) <> "" Then
                '    '5 invoice
                '    str_invoice = txtinvoice_no1.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date1.Text) <> "", confor_mat(CDate(txtinvoice_date1.Value).Date), Nothing) & vbNewLine _
                '                        & txtinvoice_no2.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date2.Text) <> "", confor_mat(CDate(txtinvoice_date2.Value).Date), Nothing) & vbNewLine _
                '                        & txtinvoice_no3.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date3.Text) <> "", confor_mat(CDate(txtinvoice_date3.Value).Date), Nothing) & vbNewLine _
                '                        & txtinvoice_no4.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date4.Text) <> "", confor_mat(CDate(txtinvoice_date4.Value).Date), Nothing) & vbNewLine _
                '                        & txtinvoice_no5.Text & vbNewLine & IIf(CommonUtility.Get_String(txtinvoice_date5.Text) <> "", confor_mat(CDate(txtinvoice_date5.Value).Date), Nothing)

                'End If
        End Select
        Return str_invoice
    End Function

    'Function Check_numinvoiceAll(ByVal v0 As String, ByVal v1 As String, ByVal v2 As String, ByVal v3 As String, ByVal v4 As String) As Integer
    '    Dim iv As Integer
    '    Dim reInteger As Integer = 0
    '    Dim str_ As String = ""
    '    str_ = v0 & ";" & v1 & ";" & v2 & ";" & v3 & ";" & v4 & ";"
    '    Dim arr_str As Array = str_.Split(";")

    '    For iv = 0 To 4
    '        If arr_str(iv).ToString.Trim <> "" Then
    '            reInteger += 1
    '        End If
    '    Next
    '    Return reInteger
    'End Function
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
    Sub All_strHScode()
        Select Case CommonUtility.Get_StringValue(txtFOBDisplay.Text)
            Case "0" '�ʴ� HS Code
                'All_str = CarTxt(CommonUtility.Get_StringValue(txtSINGLE_COUNTRY_CONTENT.Value), CommonUtility.Get_StringValue(txtInvoiceDetailTH.Value)) & "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                '                            & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
                All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                                                            & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
            Case "1" '����ʴ� HS Code
                'All_str = CarTxt(CommonUtility.Get_StringValue(txtSINGLE_COUNTRY_CONTENT.Value), CommonUtility.Get_StringValue(txtInvoiceDetailTH.Value)) & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
                All_str = txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
            Case Else
                'All_str = CarTxt(CommonUtility.Get_StringValue(txtSINGLE_COUNTRY_CONTENT.Value), CommonUtility.Get_StringValue(txtInvoiceDetailTH.Value)) & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
                All_str = txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
        End Select
    End Sub
#Region "Back"
    'Sub All_strHScode()
    '    Select Case CommonUtility.Get_StringValue(txtFOBDisplay.Text)
    '        Case "0" '�ʴ� HS Code
    '            All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '                                        & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

    '        Case "1" '����ʴ� HS Code
    '            All_str = txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
    '        Case Else
    '            All_str = txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
    '    End Select
    'End Sub
#End Region
    
    Sub All_CheckThird_5(ByVal count_data As Integer)
        'Dim All_str As String
        'Select Case CommonUtility.Get_StringValue(txtFOBDisplay.Text)
        '    Case "0" '�ʴ� HS Code
        '        All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
        '                                    & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

        '    Case "1" '����ʴ� HS Code
        '        All_str = txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
        'End Select
        All_strHScode()

        Select Case Check_Third(CommonUtility.Get_StringValue(txtthird_country.Value), _
                                CommonUtility.Get_StringValue(txtback_country.Value), _
                                CommonUtility.Get_StringValue(txtplace_exibition.Value))
            Case 1
                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str
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
                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str
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
                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str
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
                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str
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
                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str
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
            Case 6 'T3_place_exibition
                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str
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
                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str
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
                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str
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
                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str
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
        'Dim All_str As String
        'Select Case CommonUtility.Get_StringValue(txtFOBDisplay.Text)
        '    Case "0" '�ʴ� HS Code
        '        All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
        '                                    & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

        '    Case "1" '����ʴ� HS Code
        '        All_str = txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
        'End Select
        All_strHScode()
        Select Case Check_Third(CommonUtility.Get_StringValue(txtthird_country.Value), _
                                CommonUtility.Get_StringValue(txtback_country.Value), _
                                CommonUtility.Get_StringValue(txtplace_exibition.Value))
            Case 1
                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str
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
                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str
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
                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str
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
                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str
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
                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf & "        " & _
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 6 'T3_place_exibition
                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str
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
                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str
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
                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str
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
                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str
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
        'Dim All_str As String
        'Select Case CommonUtility.Get_StringValue(txtFOBDisplay.Text)
        '    Case "0" '�ʴ� HS Code
        '        All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
        '                                    & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

        '    Case "1" '����ʴ� HS Code
        '        All_str = txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
        'End Select
        All_strHScode()
        Select Case Check_Third(CommonUtility.Get_StringValue(txtthird_country.Value), _
                                CommonUtility.Get_StringValue(txtback_country.Value), _
                                CommonUtility.Get_StringValue(txtplace_exibition.Value))
            Case 1
                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str
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
                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str
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
                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str
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
                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & _
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 5 'T2_back_country
                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & _
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 6 'T3_place_exibition
                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str
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
                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str
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
                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str
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
                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Visible = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
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
        'Dim All_str As String
        'Select Case CommonUtility.Get_StringValue(txtFOBDisplay.Text)
        '    Case "0" '�ʴ� HS Code
        '        All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
        '                                    & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

        '    Case "1" '����ʴ� HS Code
        '        All_str = txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
        'End Select
        All_strHScode()
        Select Case Check_Third(CommonUtility.Get_StringValue(txtthird_country.Value), _
                                CommonUtility.Get_StringValue(txtback_country.Value), _
                                CommonUtility.Get_StringValue(txtplace_exibition.Value))
            Case 1
                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str
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
                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str
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
                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & _
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 4 '��������
                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & _
                                                                 "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 5 'T2_back_country
                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & _
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 6 'T3_place_exibition
                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & _
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 7 'T1_third_country,T2_back_country
                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str
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
                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str
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
                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str
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
        'Dim All_str As String
        'Select Case CommonUtility.Get_StringValue(txtFOBDisplay.Text)
        '    Case "0" '�ʴ� HS Code
        '        All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
        '                                    & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

        '    Case "1" '����ʴ� HS Code
        '        All_str = txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
        'End Select
        All_strHScode()
        Select Case Check_Third(CommonUtility.Get_StringValue(txtthird_country.Value), _
                                CommonUtility.Get_StringValue(txtback_country.Value), _
                                CommonUtility.Get_StringValue(txtplace_exibition.Value))
            Case 1
                If C_TotalRowDe.Text = count_data Then
                    If (Not txtquantity1.Text = String.Empty = True) And CommonUtility.Get_StringValue(txtquantity1.Text) <> "" And CommonUtility.Get_Decimal(IIf(Not (txtquantity1.Text = String.Empty) = False, 0, txtquantity1.Value)) <> 0 Then
                        txtT_product.Text = All_str
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
                If C_TotalRowDe.Text = count_data Then
                    If (Not txtquantity1.Text = String.Empty = True) And CommonUtility.Get_StringValue(txtquantity1.Text) <> "" And CommonUtility.Get_Decimal(IIf(Not (txtquantity1.Text = String.Empty) = False, 0, txtquantity1.Value)) <> 0 Then
                        txtT_product.Text = All_str
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
                If C_TotalRowDe.Text = count_data Then
                    If (Not txtquantity1.Text = String.Empty = True) And CommonUtility.Get_StringValue(txtquantity1.Text) <> "" And CommonUtility.Get_Decimal(IIf(Not (txtquantity1.Text = String.Empty) = False, 0, txtquantity1.Value)) <> 0 Then
                        txtT_product.Text = All_str
                        GroupFooter1.Visible = True
                        txtTotalAll.Visible = True
                        txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & _
                                                                                        "       _____________________________"
                    Else
                        txtT_product.Text = All_str & vbCrLf & _
                                            "       _____________________________"
                    End If
                    
                Else
                    txtT_product.Text = All_str
                End If
            Case 4 '��������
                If C_TotalRowDe.Text = count_data Then
                    If (Not txtquantity1.Text = String.Empty = True) And CommonUtility.Get_StringValue(txtquantity1.Text) <> "" And CommonUtility.Get_Decimal(IIf(Not (txtquantity1.Text = String.Empty) = False, 0, txtquantity1.Value)) <> 0 Then
                        txtT_product.Text = All_str
                        GroupFooter1.Visible = True
                        txtTotalAll.Visible = True
                        txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & _
                                                                                        "       _____________________________"
                    Else
                        txtT_product.Text = All_str & vbCrLf & _
                                            "       _____________________________"
                    End If
                    
                Else
                    txtT_product.Text = All_str
                End If
            Case 5 'T2_back_country
                If C_TotalRowDe.Text = count_data Then
                    If (Not txtquantity1.Text = String.Empty = True) And CommonUtility.Get_StringValue(txtquantity1.Text) <> "" And CommonUtility.Get_Decimal(IIf(Not (txtquantity1.Text = String.Empty) = False, 0, txtquantity1.Value)) <> 0 Then
                        txtT_product.Text = All_str
                        GroupFooter1.Visible = True
                        txtTotalAll.Visible = True
                        txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & _
                                                                                        "       _____________________________"
                    Else
                        txtT_product.Text = All_str & vbCrLf & _
                                            "       _____________________________"
                    End If
                    
                Else
                    txtT_product.Text = All_str
                End If
            Case 6 'T3_place_exibition
                If C_TotalRowDe.Text = count_data Then
                    If (Not txtquantity1.Text = String.Empty = True) And CommonUtility.Get_StringValue(txtquantity1.Text) <> "" And CommonUtility.Get_Decimal(IIf(Not (txtquantity1.Text = String.Empty) = False, 0, txtquantity1.Value)) <> 0 Then
                        txtT_product.Text = All_str
                        GroupFooter1.Visible = True
                        txtTotalAll.Visible = True
                        txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & _
                                                                                        "       _____________________________"
                    Else
                        txtT_product.Text = All_str & vbCrLf & _
                                            "       _____________________________"
                    End If
                    
                Else
                    txtT_product.Text = All_str
                End If
            Case 7 'T1_third_country,T2_back_country
                If C_TotalRowDe.Text = count_data Then
                    If (Not txtquantity1.Text = String.Empty = True) And CommonUtility.Get_StringValue(txtquantity1.Text) <> "" And CommonUtility.Get_Decimal(IIf(Not (txtquantity1.Text = String.Empty) = False, 0, txtquantity1.Value)) <> 0 Then
                        txtT_product.Text = All_str
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
                If C_TotalRowDe.Text = count_data Then
                    If (Not txtquantity1.Text = String.Empty = True) And CommonUtility.Get_StringValue(txtquantity1.Text) <> "" And CommonUtility.Get_Decimal(IIf(Not (txtquantity1.Text = String.Empty) = False, 0, txtquantity1.Value)) <> 0 Then
                        txtT_product.Text = All_str
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
                If C_TotalRowDe.Text = count_data Then
                    If (Not txtquantity1.Text = String.Empty = True) And CommonUtility.Get_StringValue(txtquantity1.Text) <> "" And CommonUtility.Get_Decimal(IIf(Not (txtquantity1.Text = String.Empty) = False, 0, txtquantity1.Value)) <> 0 Then
                        txtT_product.Text = All_str
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
        'Dim All_str As String
        'Select Case CommonUtility.Get_StringValue(txtFOBDisplay.Text)
        '    Case "0" '�ʴ� HS Code
        '        All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
        '                                    & txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

        '    Case "1" '����ʴ� HS Code
        '        All_str = txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf
        'End Select
        All_strHScode()
        Select Case Check_Third(CommonUtility.Get_StringValue(txtthird_country.Value), _
                                CommonUtility.Get_StringValue(txtback_country.Value), _
                                CommonUtility.Get_StringValue(txtplace_exibition.Value))
            Case 1
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
                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & _
                    txtback_country.Text & vbCrLf & _
                                                                                txtplace_exibition.Text & vbCrLf & _
                                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 3 'T3_place_exibition
                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & _
                     "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 4 '��������
                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & _
                     "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 5 'T2_back_country
                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & _
                    vbCrLf & "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 6 'T3_place_exibition
                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & _
                     "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 7 'T1_third_country,T2_back_country
                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & _
                    txtthird_country.Text & vbCrLf & _
                                                                txtback_country.Text & vbCrLf & _
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 8 'T1_third_country,T3_place_exibition
                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str & _
                     txtthird_country.Text & vbCrLf & _
                                                                txtplace_exibition.Text & vbCrLf & _
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 9 'T1_third_country
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
        Select Case _tariff.Trim.Length
            Case 6
                str_tariff = Mid(_tariff, 1, 4) & "." & Mid(_tariff, 5, 2)
            Case 8
                str_tariff = Mid(_tariff, 1, 4) & "." & Mid(_tariff, 5, 2) '//& "." & Mid(_tariff, 7, 2)
            Case Else
                str_tariff = Mid(_tariff, 1, 4) & "." & Mid(_tariff, 5, 2)
        End Select


        Return str_tariff
    End Function

    Function CHeck_DisPlay(ByVal CountAllDetail As String, ByVal CheckDetailGross As String) As String

        Dim str_Display As String
        Dim str_gross As String

        'str_gross = txtgross_weight.Text

        '����¹�������� Detail
        '����ͧ invoice ��ҧ�����
        'Dim str_NumInvoice As String = CommonUtility.Get_StringValue(txtNumInvoice.Text)
        Dim str_USDInvoice As String
        '����ͧ ��Ť�� ��ҧ�����
        If CommonUtility.Get_StringValue(txtCheckGrossDetail.Text) = "GWDetail" Then
            str_USDInvoice = Format(CommonUtility.Get_Decimal(txtUSDInvoiceDetail.Text), "#,##0.00##") & " USD" 'invoice ��ҧ����� �ʴ���Ť�� ��¡�÷ء��¡��
        Else
            str_USDInvoice = Str_USDinvoiceDetail 'invoice ��ҧ����� �ʴ���Ť�� ��¡���á��¡������
        End If

        Select Case CommonUtility.Get_StringValue(txtNumInvoice.Text)
            Case Is <> "" ' ����ͧ invoice ��ҧ����� �ʴ���Ť��
                Select Case CheckDetailGross
                    Case "GWDetail" '�¡��¡��, FOBDisplay ������纴���� pol �������纤���ʴ� HS CODE
                        If Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "GROSS WEIGHT - NET WEIGHT" Then
                            str_gross = GrossTxtDetail() & vbNewLine & NetWeight_(txtnet_weight.Value, txtunit_code2.Value) 'Mid(txtnet_weight.Value, 1, 9) & " " & Mid(txtunit_code2.Value, 1, 10) '& vbNewLine & FobTxt()
                            Select Case C_TotalRowDe.Text
                                Case 1
                                    str_Display = str_gross '& vbNewLine & str_USDInvoice
                                Case Else
                                    If Cpage = CountAllDetail Then
                                        str_Display = str_gross & vbNewLine & _
                                        "_______" & vbNewLine & Format(TGross, "#,##0.00##") & " " & CommonUtility.Get_StringValue(txtunit_code3.Value) & _
                                        vbNewLine & Format(TNet, "#,##0.00##") & " " & CommonUtility.Get_StringValue(txtunit_code2.Value)
                                    Else
                                        str_Display = str_gross '& vbNewLine & str_USDInvoice
                                    End If

                            End Select
                        ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "GROSS WEIGHT" Then
                            str_gross = GrossTxtDetail() '& vbNewLine & FobTxt()
                            Select Case C_TotalRowDe.Text
                                Case 1
                                    str_Display = str_gross '& vbNewLine & str_USDInvoice
                                Case Else
                                    If Cpage = CountAllDetail Then
                                        str_Display = str_gross & vbNewLine & _
                                        "_______" & vbNewLine & Format(TGross, "#,##0.00##") & " " & CommonUtility.Get_StringValue(txtunit_code3.Value)

                                    Else
                                        str_Display = str_gross '& vbNewLine & str_USDInvoice
                                    End If
                            End Select

                        End If
                    Case Else
                        If Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "GROSS WEIGHT - NET WEIGHT" Then
                            str_gross = GrossTxt() & vbNewLine & NetWeight_(txtnet_weight.Value, txtunit_code2.Value) 'Mid(txtnet_weight.Value, 1, 9) & " " & Mid(txtunit_code2.Value, 1, 10) '& vbNewLine & FobTxt()
                            Select Case C_TotalRowDe.Text
                                Case 1
                                    str_Display = str_gross '& vbNewLine & str_USDInvoice
                                Case Else

                                    str_Display = str_gross '& vbNewLine & str_USDInvoice
                            End Select
                        ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "GROSS WEIGHT" Then
                            str_gross = GrossTxt() '& vbNewLine & FobTxt()
                            Select Case C_TotalRowDe.Text
                                Case 1
                                    str_Display = str_gross '& vbNewLine & str_USDInvoice
                                Case Else
                                    str_Display = str_gross '& vbNewLine & str_USDInvoice
                            End Select

                        End If
                End Select

            Case Else '����� invoice ��ҧ�����
                Select Case CheckDetailGross
                    Case "GWDetail"
                        'form2 �� WeightDisplayHeaderH
                        If Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "GROSS WEIGHT - NET WEIGHT" Then
                            str_gross = GrossTxtDetail() & vbNewLine & NetWeight_(txtnet_weight.Value, txtunit_code2.Value)
                            Select Case C_TotalRowDe.Text
                                Case 1
                                    str_Display = str_gross
                                Case Else
                                    If Cpage = CountAllDetail Then
                                        str_Display = str_gross & vbNewLine & _
                                        "_______" & vbNewLine & Format(TGross, "#,##0.00##") & " " & CommonUtility.Get_StringValue(txtunit_code3.Value) & _
                                        vbNewLine & Format(TNet, "#,##0.00##") & " " & CommonUtility.Get_StringValue(txtunit_code2.Value)
                                    Else
                                        str_Display = str_gross
                                    End If

                            End Select
                        ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "GROSS WEIGHT" Then
                            str_gross = GrossTxtDetail() '& vbNewLine & FobTxt()
                            Select Case C_TotalRowDe.Text
                                Case 1
                                    str_Display = str_gross
                                Case Else
                                    If Cpage = CountAllDetail Then
                                        str_Display = str_gross & vbNewLine & _
                                        "_______" & vbNewLine & Format(TGross, "#,##0.00##") & " " & CommonUtility.Get_StringValue(txtunit_code3.Value)
                                    Else
                                        str_Display = str_gross
                                    End If

                            End Select
                        End If
                    Case Else
                        If Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "GROSS WEIGHT - NET WEIGHT" Then
                            str_gross = GrossTxt() & vbNewLine & NetWeight_(TNet, txtunit_code2.Value)
                            Select Case C_TotalRowDe.Text
                                Case 1
                                    str_Display = str_gross
                                Case Else
                                    str_Display = str_gross
                            End Select
                        ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "GROSS WEIGHT" Then
                            str_gross = GrossTxt() '& vbNewLine & FobTxtAll()
                            Select Case C_TotalRowDe.Text
                                Case 1
                                    str_Display = str_gross
                                Case Else
                                    str_Display = str_gross
                            End Select
                        Else
                            str_gross = GrossTxt() '& vbNewLine & FobTxtAll()
                            str_Display = str_gross
                        End If
                End Select
        End Select

        Return str_Display
    End Function

    'Function CHeck_DisPlay(ByVal CountAllDetail As String, ByVal CheckDetailGross As String) As String

    '    Dim str_Display As String
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

    '    Select Case CommonUtility.Get_StringValue(txtNumInvoice.Text)
    '        Case Is <> "" ' ����ͧ invoice ��ҧ����� �ʴ���Ť��
    '            Select Case CheckDetailGross
    '                Case "GWDetail" '�¡��¡��, FOBDisplay ������纴���� pol �������纤���ʴ� HS CODE
    '                    If Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "GROSS WEIGHT - NET WEIGHT" Then
    '                        str_gross = GrossTxtDetail() & vbNewLine & NetWeight_(txtnet_weight.Value, txtunit_code2.Value) 'Mid(txtnet_weight.Value, 1, 9) & " " & Mid(txtunit_code2.Value, 1, 10) '& vbNewLine & FobTxt()
    '                        Select Case C_TotalRowDe.Text
    '                            Case 1
    '                                str_Display = str_gross '& vbNewLine & str_USDInvoice
    '                            Case Else
    '                                If Cpage = CountAllDetail Then
    '                                    str_Display = str_gross & vbNewLine & _
    '                                    "_______" & vbNewLine & Format(TGross, "#,##0.00##") & " " & CommonUtility.Get_StringValue(txtunit_code3.Value) & _
    '                                    vbNewLine & Format(TNet, "#,##0.00##") & " " & CommonUtility.Get_StringValue(txtunit_code2.Value)
    '                                Else
    '                                    str_Display = str_gross '& vbNewLine & str_USDInvoice
    '                                End If

    '                        End Select
    '                    ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "GROSS WEIGHT" Then
    '                        str_gross = GrossTxtDetail() '& vbNewLine & FobTxt()
    '                        Select Case C_TotalRowDe.Text
    '                            Case 1
    '                                str_Display = str_gross '& vbNewLine & str_USDInvoice
    '                            Case Else
    '                                If Cpage = CountAllDetail Then
    '                                    str_Display = str_gross & vbNewLine & _
    '                                    "_______" & vbNewLine & Format(TGross, "#,##0.00##") & " " & CommonUtility.Get_StringValue(txtunit_code3.Value)

    '                                Else
    '                                    str_Display = str_gross '& vbNewLine & str_USDInvoice
    '                                End If
    '                        End Select

    '                    End If
    '                Case Else
    '                    If Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "GROSS WEIGHT - NET WEIGHT" Then
    '                        str_gross = GrossTxt() & vbNewLine & NetWeight_(txtnet_weight.Value, txtunit_code2.Value) 'Mid(txtnet_weight.Value, 1, 9) & " " & Mid(txtunit_code2.Value, 1, 10) '& vbNewLine & FobTxt()
    '                        Select Case C_TotalRowDe.Text
    '                            Case 1
    '                                str_Display = str_gross '& vbNewLine & str_USDInvoice
    '                            Case Else

    '                                str_Display = str_gross '& vbNewLine & str_USDInvoice
    '                        End Select
    '                    ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "GROSS WEIGHT" Then
    '                        str_gross = GrossTxt() '& vbNewLine & FobTxt()
    '                        Select Case C_TotalRowDe.Text
    '                            Case 1
    '                                str_Display = str_gross '& vbNewLine & str_USDInvoice
    '                            Case Else
    '                                str_Display = str_gross '& vbNewLine & str_USDInvoice
    '                        End Select

    '                    End If
    '            End Select

    '        Case Else '����� invoice ��ҧ�����
    '            Select Case CheckDetailGross
    '                Case "GWDetail"
    '                    'form2 �� WeightDisplayHeaderH
    '                    If Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "GROSS WEIGHT - NET WEIGHT" Then
    '                        str_gross = GrossTxtDetail() & vbNewLine & NetWeight_(txtnet_weight.Value, txtunit_code2.Value)
    '                        Select Case C_TotalRowDe.Text
    '                            Case 1
    '                                str_Display = str_gross
    '                            Case Else
    '                                If Cpage = CountAllDetail Then
    '                                    str_Display = str_gross & vbNewLine & _
    '                                    "_______" & vbNewLine & Format(TGross, "#,##0.00##") & " " & CommonUtility.Get_StringValue(txtunit_code3.Value) & _
    '                                    vbNewLine & Format(TNet, "#,##0.00##") & " " & CommonUtility.Get_StringValue(txtunit_code2.Value)
    '                                Else
    '                                    str_Display = str_gross
    '                                End If

    '                        End Select
    '                    ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "GROSS WEIGHT" Then
    '                        str_gross = GrossTxtDetail() '& vbNewLine & FobTxt()
    '                        Select Case C_TotalRowDe.Text
    '                            Case 1
    '                                str_Display = str_gross
    '                            Case Else
    '                                If Cpage = CountAllDetail Then
    '                                    str_Display = str_gross & vbNewLine & _
    '                                    "_______" & vbNewLine & Format(TGross, "#,##0.00##") & " " & CommonUtility.Get_StringValue(txtunit_code3.Value)
    '                                Else
    '                                    str_Display = str_gross
    '                                End If

    '                        End Select
    '                    End If
    '                Case Else
    '                    If Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "GROSS WEIGHT - NET WEIGHT" Then
    '                        str_gross = GrossTxt() & vbNewLine & NetWeight_(TNet, txtunit_code2.Value)
    '                        Select Case C_TotalRowDe.Text
    '                            Case 1
    '                                str_Display = str_gross
    '                            Case Else
    '                                str_Display = str_gross
    '                        End Select
    '                    ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "GROSS WEIGHT" Then
    '                        str_gross = GrossTxt() '& vbNewLine & FobTxtAll()
    '                        Select Case C_TotalRowDe.Text
    '                            Case 1
    '                                str_Display = str_gross
    '                            Case Else
    '                                str_Display = str_gross
    '                        End Select
    '                    End If
    '            End Select
    '    End Select

    '    Return str_Display
    'End Function

    Function GrossTxt() As String
        Dim Str_GrossTxt As String
        Dim Dec_Gross As Decimal
        Dec_Gross = CDec(Check_Null(CommonUtility.Get_StringValue(txtgross_weightH.Value))) 'Format(CDec(Check_Null(CommonUtility.Get_StringValue(txtgross_weightH.Value))), "#,##0.00##")
        Str_GrossTxt = Format(Dec_Gross, "#,##0.00##") & " " & CommonUtility.Get_StringValue(txtg_unit_code.Value) 'CStr(Dec_Gross) & " " & CommonUtility.Get_StringValue(txtg_unit_code.Value)

        Return Str_GrossTxt
    End Function
    Function GrossTxtDetail() As String
        Dim Str_GrossTxt As String
        Dim Dec_Gross As Decimal
        Dec_Gross = CDec(Check_Null(CommonUtility.Get_StringValue(txtgross_weightD.Value))) 'Format(CDec(Check_Null(CommonUtility.Get_StringValue(txtgross_weightH.Value))), "#,##0.00##")
        Str_GrossTxt = Format(Dec_Gross, "#,##0.00##") & " " & CommonUtility.Get_StringValue(txtunit_code3.Value) 'CStr(Dec_Gross) & " " & CommonUtility.Get_StringValue(txtg_unit_code.Value)

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
    'Function FobTxtAll() As String
    '    Dim Str_FobTxt As String
    '    Dim Dec_Fob As Decimal

    '    If txtfob_amt.Value > 0 Then
    '        Dec_Fob = CDec(Check_Null(CommonUtility.Get_StringValue(TFob))) 'Format(CDec(Check_Null(CommonUtility.Get_StringValue(txtFOB_AMT.Value))), "#,##0.0###")
    '        Str_FobTxt = Format(Dec_Fob, "#,##0.00##") & " USD"
    '    Else
    '        Str_FobTxt = ""
    '    End If
    '    Return Str_FobTxt
    'End Function

    Function NetWeight_(ByVal _netweight, ByVal _uni) As String
        Dim sumStr As String
        sumStr = Format(_netweight, "#,##0.00##") & " " & _uni

        Return sumStr
    End Function

    Sub Check_box8()
        'txtTemp_box8.Text = txtbox8.Value
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

    Private Sub rpt3_ediFORM2_pr_PageEnd(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PageEnd
        head_height = PageHeader1.Height

        Dim f As New System.Drawing.Font("BrowalliaUPC", 11)

        Dim xm As Single = 6.79
        Dim ym As Single = CSng(head_height) '5.315
        'Dim width As Single = 6.0F
        Dim widthm As Single = 0.86
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
        Select Case CommonUtility.Get_StringValue(txtCheckGrossDetail.Text)
            Case Is <> "GWDetail"
                Dim x_txtGrossTxt As Single = 5.9
                Dim y_txtGrossTxt As Single = CSng(head_height) '5.315
                'Dim width As Single = 6.0F
                Dim width_txtGrossTxt As Single = 0.86
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
                    .DrawText(CHeck_DisPlay(CommonUtility.Get_StringValue(C_TotalRowDe.Text), CommonUtility.Get_StringValue(txtCheckGrossDetail.Text)), drawRect_txtGrossTxt)

                End With
        End Select
        
        '============================
        Dim x_txtmarks As Single = 0.57
        Dim y_txtmarks As Single = CSng(head_height) '5.315
        'Dim width As Single = 6.0F
        Dim width_txtmarks As Single = 1.67
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

    Private Sub rpt3_ediFORM2_pr_ReportEnd(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.ReportEnd
        If updatePrintTotal(txtinvh_run_auto.Value, CStr(Me.Document.Pages.Count)) = True Then

        End If
    End Sub

    Private Sub rpt3_ediFORM2_pr_ReportStart(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.ReportStart
        'Me.ReportInfo1.FormatString = "Page {PageNumber} of {PageCount} on {RunDateTime}"
        Me.ReportInfo1.FormatString = "Page : {PageNumber} of {PageCount}"
    End Sub

    Private Sub GroupFooter1_Format(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GroupFooter1.Format

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

        'If reports_CardID(arr_(0).ToString) <> "" Then
        '    Select Case arr_.Length - 1
        '        Case 1
        '            Picture_SealAuthor.Visible = True

        '            'Picture_SealAuthor.SizeMode = SizeModes.Stretch
        '            Dim u As String = reports_CardID(arr_(0).ToString) '"http://edi.dft.go.th/" & "/Portals/0/Images_Sign/0405533000247/2013/SealPerson/3660400121998/3660400121998_New_��͹ѹ�� NK1.jpg"
        '            Dim client As System.Net.WebClient = New System.Net.WebClient()
        '            Dim data As System.IO.Stream = client.OpenRead(u)
        '            Dim reader As System.IO.StreamReader = New System.IO.StreamReader(data)

        '            Picture_SealAuthor.Image = System.Drawing.Image.FromStream(reader.BaseStream())
        '    End Select
        'End If
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

        'Picture_SealAuthor.SizeMode = SizeModes.Stretch
        PictureApproveSign.Image = New Drawing.Bitmap(reports_Approveid(arr_(0).ToString))
        'txtTemp_SiteSend.Text = "ST-003"
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
