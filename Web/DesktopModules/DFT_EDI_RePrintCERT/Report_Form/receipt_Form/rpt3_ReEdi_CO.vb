Imports DataDynamics.ActiveReports
Imports DataDynamics.ActiveReports.Document

Imports Microsoft.ApplicationBlocks.Data
Imports System.Data.SqlClient
Imports System.Data
Imports System.Text
Imports DFT.Dotnetnuke.ClassLibrary
Imports ReportPrintClass
Public Class rpt3_ReEdi_CO
    Dim C_num As Integer
    Dim i As Integer

    Dim C_NET_WEIGHT As Decimal
    Dim C_Fob_Amt As Decimal
#Region "Case New Box1 and Box2 01-10-2012"
    Dim str_01 As String = "" '"                            "
    Dim str_02 As String = "" '"                            "
    Dim str_03 As String = "" '"                            "
    Function v1_Head_Checkcompany(ByVal ByCase_Form As String) As String
        Dim StrtxtRerutn As String = ""
        Dim StrtxtRerutn1 As String = ""
        Dim StrtxtRerutnMain As String = ""
        StrtxtRerutn1 = "�ӹҨ��Ţ���  " & txtcard_id.Text & "   �ͧ����ѷ/��ҧ/��ҹ  " & txtcompany_name.Text &
        vbNewLine & "�Ţ��Шӵ�Ǽ�����������ҡ�   " & txtcompany_taxno.Text &
        " ��������Ţ���  " & txtcompany_address.Text & " " & txtcompany_province.Text & " ����� " & txtcompany_country.Text & " " &
        IIf(CommonUtility.Get_StringValue(txtcompany_phone.Text) <> "", " ���Ѿ��  " & txtcompany_phone.Text, " ���Ѿ��    -") &
        IIf(CommonUtility.Get_StringValue(txtcompany_fax.Text) <> "", "   �����  " & txtcompany_fax.Text, "   �����    -") & vbNewLine

        '੾�� �Ѻ�Ӣͪ�ͧ��� 1
        'O/TOB �տ���� Form4_5, Form4_6, Form5_1
        'O/TCO �ѧ�����

        Select Case ByCase_Form.ToUpper
            Case "FORM2_4"
                If Mid(txtdest_remark.Text, 1, 3) = "C/O" Then
                    StrtxtRerutn = txtob_address.Text & IIf(Check_NULLALL(txtNewEmail_ch01.Text) <> "", " E-mail: " & txtNewEmail_ch01.Text, "") & " CARE OF " & txtcompany_name.Text & " " & txtcompany_address.Text _
                                       & " " & txtcompany_province.Text & " " & txtcompany_country.Text &
                                       IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") &
                                       IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") &
                                       IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID:  " & txtcompany_taxno.Text, "") &
                                       IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")
                ElseIf Mid(txtdest_remark.Text, 1, 3) = "O/B" Then
                    StrtxtRerutn = IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_email.Text)) <> "", "E-mail: " & txtcompany_email.Text & " ", "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtob_address.Text)) <> "", "ON BEHALF OF " & txtob_address.Text, "") & IIf(Check_NULLALL(txtNewEmail_ch01.Text) <> "", " E-mail: " & txtNewEmail_ch01.Text, "")
                ElseIf Mid(txtdest_remark.Text, 2, 1) <> "/" Then
                    StrtxtRerutn = ""
                    'StrtxtRerutn = txtcompany_name.Text & " " & txtcompany_address.Text & " " & txtcompany_province.Text _
                    '                    & " " & txtcompany_country.Text & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") & _
                    '                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") _
                    '                    & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID: " & txtcompany_taxno.Text, "") & _
                    '                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")

                End If
            Case "FORM4_3"
                If Mid(txtdest_remark.Text, 1, 3) = "C/O" Then
                    StrtxtRerutn = txtob_address.Text & IIf(Check_NULLALL(txtNewEmail_ch01.Text) <> "", " E-mail: " & txtNewEmail_ch01.Text, "") & " CARE OF " & txtcompany_name.Text & " " & txtcompany_address.Text _
                                       & " " & txtcompany_province.Text & " " & txtcompany_country.Text &
                                       IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") &
                                       IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") &
                                       IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID:  " & txtcompany_taxno.Text, "") &
                                       IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")
                ElseIf Mid(txtdest_remark.Text, 1, 3) = "O/B" Then
                    StrtxtRerutn = IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_email.Text)) <> "", "E-mail: " & txtcompany_email.Text & " ", "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtob_address.Text)) <> "", "ON BEHALF OF " & txtob_address.Text, "") & IIf(Check_NULLALL(txtNewEmail_ch01.Text) <> "", " E-mail: " & txtNewEmail_ch01.Text, "")
                ElseIf Mid(txtdest_remark.Text, 2, 1) <> "/" Then
                    StrtxtRerutn = ""
                    'StrtxtRerutn = txtcompany_name.Text & " " & txtcompany_address.Text & " " & txtcompany_province.Text _
                    '                    & " " & txtcompany_country.Text & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") & _
                    '                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") _
                    '                    & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID: " & txtcompany_taxno.Text, "") & _
                    '                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")

                ElseIf Mid(txtdest_remark.Text, 1, 5) <> "O/TOB" Then
                    StrtxtRerutn = txtcompany_name.Text & " " & txtcompany_address.Text & " " & txtcompany_province.Text _
                                    & " " & txtcompany_country.Text & " " & txtob_address.Text &
                                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") &
                                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") _
                                    & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID: " & txtcompany_taxno.Text, "") &
                                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")

                ElseIf Mid(txtdest_remark.Text, 1, 5) <> "O/TCO" Then
                    StrtxtRerutn = txtob_address.Text & txtcompany_name.Text & " " & txtcompany_address.Text & " " & txtcompany_province.Text _
                                    & " " & txtcompany_country.Text &
                                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") &
                                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") _
                                    & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID: " & txtcompany_taxno.Text, "") &
                                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")

                End If
            Case "FORM9"
                If Mid(txtdest_remark.Text, 1, 3) = "C/O" Then
                    StrtxtRerutn = txtob_address.Text & IIf(Check_NULLALL(txtNewEmail_ch01.Text) <> "", " E-mail: " & txtNewEmail_ch01.Text, "") & " CARE OF " & txtcompany_name.Text & " " & txtcompany_address.Text _
                                       & " " & txtcompany_province.Text & " " & txtcompany_country.Text &
                                       IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") &
                                       IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") &
                                       IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID:  " & txtcompany_taxno.Text, "") &
                                       IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")
                ElseIf Mid(txtdest_remark.Text, 1, 3) = "O/B" Then
                    StrtxtRerutn = IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_email.Text)) <> "", "E-mail: " & txtcompany_email.Text & " ", "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtob_address.Text)) <> "", "ON BEHALF OF " & txtob_address.Text, "") & IIf(Check_NULLALL(txtNewEmail_ch01.Text) <> "", " E-mail: " & txtNewEmail_ch01.Text, "")
                ElseIf Mid(txtdest_remark.Text, 2, 1) <> "/" Then
                    StrtxtRerutn = ""
                    'StrtxtRerutn = txtcompany_name.Text & " " & txtcompany_address.Text & " " & txtcompany_province.Text _
                    '                    & " " & txtcompany_country.Text & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") & _
                    '                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") _
                    '                    & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID: " & txtcompany_taxno.Text, "") & _
                    '                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")

                End If
            Case "FORM8"
                If Mid(txtdest_remark.Text, 1, 3) = "C/O" Then
                    StrtxtRerutn = txtob_address.Text & IIf(Check_NULLALL(txtNewEmail_ch01.Text) <> "", " E-mail: " & txtNewEmail_ch01.Text, "") & " CARE OF " & txtcompany_name.Text & " " & txtcompany_address.Text _
                                       & " " & txtcompany_province.Text & " " & txtcompany_country.Text &
                                       IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") &
                                       IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") &
                                       IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID:  " & txtcompany_taxno.Text, "") &
                                       IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")
                ElseIf Mid(txtdest_remark.Text, 1, 3) = "O/B" Then
                    StrtxtRerutn = IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_email.Text)) <> "", "E-mail: " & txtcompany_email.Text & " ", "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtob_address.Text)) <> "", "ON BEHALF OF " & txtob_address.Text, "") & IIf(Check_NULLALL(txtNewEmail_ch01.Text) <> "", " E-mail: " & txtNewEmail_ch01.Text, "")
                ElseIf Mid(txtdest_remark.Text, 2, 1) <> "/" Then
                    StrtxtRerutn = ""
                    'StrtxtRerutn = txtcompany_name.Text & " " & txtcompany_address.Text & " " & txtcompany_province.Text _
                    '                    & " " & txtcompany_country.Text & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") & _
                    '                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") _
                    '                    & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID: " & txtcompany_taxno.Text, "") & _
                    '                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")

                End If
            Case "FORM7"
                If Mid(txtdest_remark.Text, 1, 3) = "C/O" Then
                    StrtxtRerutn = txtob_address.Text & IIf(Check_NULLALL(txtNewEmail_ch01.Text) <> "", " E-mail: " & txtNewEmail_ch01.Text, "") & " CARE OF " & txtcompany_name.Text & " " & txtcompany_address.Text _
                                       & " " & txtcompany_province.Text & " " & txtcompany_country.Text &
                                       IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") &
                                       IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") &
                                       IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID:  " & txtcompany_taxno.Text, "") &
                                       IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")
                ElseIf Mid(txtdest_remark.Text, 1, 3) = "O/B" Then
                    StrtxtRerutn = IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_email.Text)) <> "", "E-mail: " & txtcompany_email.Text & " ", "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtob_address.Text)) <> "", "ON BEHALF OF " & txtob_address.Text, "") & IIf(Check_NULLALL(txtNewEmail_ch01.Text) <> "", " E-mail: " & txtNewEmail_ch01.Text, "")
                ElseIf Mid(txtdest_remark.Text, 2, 1) <> "/" Then
                    StrtxtRerutn = ""
                    'StrtxtRerutn = txtcompany_name.Text & " " & txtcompany_address.Text & " " & txtcompany_province.Text _
                    '                    & " " & txtcompany_country.Text & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") & _
                    '                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") _
                    '                    & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID: " & txtcompany_taxno.Text, "") & _
                    '                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")

                End If
            Case "FORM6"
                If Mid(txtdest_remark.Text, 1, 3) = "C/O" Then
                    StrtxtRerutn = txtob_address.Text & IIf(Check_NULLALL(txtNewEmail_ch01.Text) <> "", " E-mail: " & txtNewEmail_ch01.Text, "") & " CARE OF " & txtcompany_name.Text & " " & txtcompany_address.Text _
                                       & " " & txtcompany_province.Text & " " & txtcompany_country.Text &
                                       IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") &
                                       IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") &
                                       IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID:  " & txtcompany_taxno.Text, "") &
                                       IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")
                ElseIf Mid(txtdest_remark.Text, 1, 3) = "O/B" Then
                    StrtxtRerutn = IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_email.Text)) <> "", "E-mail: " & txtcompany_email.Text & " ", "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtob_address.Text)) <> "", "ON BEHALF OF " & txtob_address.Text, "") & IIf(Check_NULLALL(txtNewEmail_ch01.Text) <> "", " E-mail: " & txtNewEmail_ch01.Text, "")
                ElseIf Mid(txtdest_remark.Text, 2, 1) <> "/" Then
                    StrtxtRerutn = ""
                    'StrtxtRerutn = txtcompany_name.Text & " " & txtcompany_address.Text & " " & txtcompany_province.Text _
                    '                    & " " & txtcompany_country.Text & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") & _
                    '                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") _
                    '                    & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID: " & txtcompany_taxno.Text, "") & _
                    '                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")

                End If
            Case "FORM5"
                If Mid(txtdest_remark.Text, 1, 3) = "C/O" Then
                    StrtxtRerutn = txtob_address.Text & IIf(Check_NULLALL(txtNewEmail_ch01.Text) <> "", " E-mail: " & txtNewEmail_ch01.Text, "") & " CARE OF " & txtcompany_name.Text & " " & txtcompany_address.Text _
                                       & " " & txtcompany_province.Text & " " & txtcompany_country.Text &
                                       IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") &
                                       IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") &
                                       IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID:  " & txtcompany_taxno.Text, "") &
                                       IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")
                ElseIf Mid(txtdest_remark.Text, 1, 3) = "O/B" Then
                    StrtxtRerutn = IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_email.Text)) <> "", "E-mail: " & txtcompany_email.Text & " ", "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtob_address.Text)) <> "", "ON BEHALF OF " & txtob_address.Text, "") & IIf(Check_NULLALL(txtNewEmail_ch01.Text) <> "", " E-mail: " & txtNewEmail_ch01.Text, "")
                ElseIf Mid(txtdest_remark.Text, 2, 1) <> "/" Then
                    StrtxtRerutn = ""
                    'StrtxtRerutn = txtcompany_name.Text & " " & txtcompany_address.Text & " " & txtcompany_province.Text _
                    '                    & " " & txtcompany_country.Text & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") & _
                    '                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") _
                    '                    & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID: " & txtcompany_taxno.Text, "") & _
                    '                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")

                End If
            Case "FORM5_1"
                If Mid(txtdest_remark.Text, 1, 3) = "C/O" Then
                    StrtxtRerutn = txtob_address.Text & IIf(Check_NULLALL(txtNewEmail_ch01.Text) <> "", " E-mail: " & txtNewEmail_ch01.Text, "") & " CARE OF " & txtcompany_name.Text & " " & txtcompany_address.Text _
                                       & " " & txtcompany_province.Text & " " & txtcompany_country.Text &
                                       IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") &
                                       IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") &
                                       IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID:  " & txtcompany_taxno.Text, "") &
                                       IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")
                ElseIf Mid(txtdest_remark.Text, 1, 3) = "O/B" Then
                    StrtxtRerutn = IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_email.Text)) <> "", "E-mail: " & txtcompany_email.Text & " ", "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtob_address.Text)) <> "", "ON BEHALF OF " & txtob_address.Text, "") & IIf(Check_NULLALL(txtNewEmail_ch01.Text) <> "", " E-mail: " & txtNewEmail_ch01.Text, "")
                ElseIf Mid(txtdest_remark.Text, 2, 1) <> "/" Then
                    StrtxtRerutn = ""
                    'StrtxtRerutn = txtcompany_name.Text & " " & txtcompany_address.Text & " " & txtcompany_province.Text _
                    '                    & " " & txtcompany_country.Text & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") & _
                    '                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") _
                    '                    & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID: " & txtcompany_taxno.Text, "") & _
                    '                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")

                ElseIf Mid(txtdest_remark.Text, 1, 5) = "O/TOB" Then
                    StrtxtRerutn = IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text & " ", "") & txtob_address.Text

                ElseIf Mid(txtdest_remark.Text, 1, 5) = "O/TCO" Then
                    StrtxtRerutn = txtob_address.Text & " " & txtcompany_name.Text & " " & txtcompany_address.Text & " " & txtcompany_province.Text _
                                    & " " & txtcompany_country.Text &
                                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") &
                                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") _
                                    & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID: " & txtcompany_taxno.Text, "") &
                                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")

                End If
            Case "FORM5_2"
                If Mid(txtdest_remark.Text, 1, 3) = "C/O" Then
                    StrtxtRerutn = txtob_address.Text & IIf(Check_NULLALL(txtNewEmail_ch01.Text) <> "", " E-mail: " & txtNewEmail_ch01.Text, "") & " CARE OF " & txtcompany_name.Text & " " & txtcompany_address.Text _
                                       & " " & txtcompany_province.Text & " " & txtcompany_country.Text &
                                       IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") &
                                       IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") &
                                       IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID:  " & txtcompany_taxno.Text, "") &
                                       IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")
                ElseIf Mid(txtdest_remark.Text, 1, 3) = "O/B" Then
                    StrtxtRerutn = IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_email.Text)) <> "", "E-mail: " & txtcompany_email.Text & " ", "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtob_address.Text)) <> "", "ON BEHALF OF " & txtob_address.Text, "") & IIf(Check_NULLALL(txtNewEmail_ch01.Text) <> "", " E-mail: " & txtNewEmail_ch01.Text, "")
                ElseIf Mid(txtdest_remark.Text, 2, 1) <> "/" Then
                    StrtxtRerutn = ""
                    'StrtxtRerutn = txtcompany_name.Text & " " & txtcompany_address.Text & " " & txtcompany_province.Text _
                    '                    & " " & txtcompany_country.Text & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") & _
                    '                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") _
                    '                    & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID: " & txtcompany_taxno.Text, "") & _
                    '                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")

                ElseIf Mid(txtdest_remark.Text, 1, 5) = "O/TOB" Then
                    StrtxtRerutn = IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text & " ", "") & txtob_address.Text

                ElseIf Mid(txtdest_remark.Text, 1, 5) = "O/TCO" Then
                    StrtxtRerutn = txtob_address.Text & " " & txtcompany_name.Text & " " & txtcompany_address.Text & " " & txtcompany_province.Text _
                                    & " " & txtcompany_country.Text &
                                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") &
                                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") _
                                    & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID: " & txtcompany_taxno.Text, "") &
                                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")

                End If
            Case "FORM441", "FORM441_4"
                If Mid(txtdest_remark.Text, 1, 3) = "C/O" Then
                    StrtxtRerutn = txtob_address.Text & IIf(Check_NULLALL(txtNewEmail_ch01.Text) <> "", " E-mail: " & txtNewEmail_ch01.Text, "") & " CARE OF " & txtcompany_name.Text & " " & txtcompany_address.Text _
                                       & " " & txtcompany_province.Text & " " & txtcompany_country.Text &
                                       IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") &
                                       IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") &
                                       IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID:  " & txtcompany_taxno.Text, "") &
                                       IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")
                ElseIf Mid(txtdest_remark.Text, 1, 3) = "O/B" Then
                    StrtxtRerutn = IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_email.Text)) <> "", "E-mail: " & txtcompany_email.Text & " ", "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtob_address.Text)) <> "", "ON BEHALF OF " & txtob_address.Text, "") & IIf(Check_NULLALL(txtNewEmail_ch01.Text) <> "", " E-mail: " & txtNewEmail_ch01.Text, "")
                ElseIf Mid(txtdest_remark.Text, 2, 1) <> "/" Then
                    StrtxtRerutn = ""
                    'StrtxtRerutn = txtcompany_name.Text & " " & txtcompany_address.Text & " " & txtcompany_province.Text _
                    '                    & " " & txtcompany_country.Text & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") & _
                    '                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") _
                    '                    & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID: " & txtcompany_taxno.Text, "") & _
                    '                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")

                ElseIf Mid(txtdest_remark.Text, 1, 5) <> "O/TOB" Then
                    StrtxtRerutn = txtcompany_name.Text & " " & txtcompany_address.Text & " " & txtcompany_province.Text _
                                    & " " & txtcompany_country.Text & " " & txtob_address.Text &
                                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") &
                                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") _
                                    & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID: " & txtcompany_taxno.Text, "") &
                                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")

                ElseIf Mid(txtdest_remark.Text, 1, 5) <> "O/TCO" Then
                    StrtxtRerutn = txtob_address.Text & txtcompany_name.Text & " " & txtcompany_address.Text & " " & txtcompany_province.Text _
                                    & " " & txtcompany_country.Text &
                                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") &
                                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") _
                                    & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID: " & txtcompany_taxno.Text, "") &
                                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")

                End If
            Case "FORM44", "FORM44_44", "FORM44_02"
                If Mid(txtdest_remark.Text, 1, 3) = "C/O" Then
                    StrtxtRerutn = txtob_address.Text & IIf(Check_NULLALL(txtNewEmail_ch01.Text) <> "", " E-mail: " & txtNewEmail_ch01.Text, "") & " CARE OF " & txtcompany_name.Text & " " & txtcompany_address.Text _
                                       & " " & txtcompany_province.Text & " " & txtcompany_country.Text &
                                       IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") &
                                       IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") &
                                       IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID:  " & txtcompany_taxno.Text, "") &
                                       IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")
                ElseIf Mid(txtdest_remark.Text, 1, 3) = "O/B" Then
                    StrtxtRerutn = IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_email.Text)) <> "", "E-mail: " & txtcompany_email.Text & " ", "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtob_address.Text)) <> "", "ON BEHALF OF " & txtob_address.Text, "") & IIf(Check_NULLALL(txtNewEmail_ch01.Text) <> "", " E-mail: " & txtNewEmail_ch01.Text, "")
                ElseIf Mid(txtdest_remark.Text, 2, 1) <> "/" Then
                    StrtxtRerutn = ""
                    'StrtxtRerutn = txtcompany_name.Text & " " & txtcompany_address.Text & " " & txtcompany_province.Text _
                    '                    & " " & txtcompany_country.Text & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") & _
                    '                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") _
                    '                    & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID: " & txtcompany_taxno.Text, "") & _
                    '                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")

                ElseIf Mid(txtdest_remark.Text, 1, 5) <> "O/TOB" Then
                    StrtxtRerutn = txtcompany_name.Text & " " & txtcompany_address.Text & " " & txtcompany_province.Text _
                                    & " " & txtcompany_country.Text & " " & txtob_address.Text &
                                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") &
                                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") _
                                    & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID: " & txtcompany_taxno.Text, "") &
                                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")

                ElseIf Mid(txtdest_remark.Text, 1, 5) <> "O/TCO" Then
                    StrtxtRerutn = txtob_address.Text & txtcompany_name.Text & " " & txtcompany_address.Text & " " & txtcompany_province.Text _
                                    & " " & txtcompany_country.Text &
                                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") &
                                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") _
                                    & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID: " & txtcompany_taxno.Text, "") &
                                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")

                End If
            Case "FORM4", "FORM44_4", "FORM44_01", "FORMAHK"
                If Mid(txtdest_remark.Text, 1, 3) = "C/O" Then
                    StrtxtRerutn = txtob_address.Text & IIf(Check_NULLALL(txtNewEmail_ch01.Text) <> "", " E-mail: " & txtNewEmail_ch01.Text, "") & " CARE OF " & txtcompany_name.Text & " " & txtcompany_address.Text _
                                       & " " & txtcompany_province.Text & " " & txtcompany_country.Text &
                                       IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") &
                                       IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") &
                                       IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID:  " & txtcompany_taxno.Text, "") &
                                       IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")
                ElseIf Mid(txtdest_remark.Text, 1, 3) = "O/B" Then
                    StrtxtRerutn = IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_email.Text)) <> "", "E-mail: " & txtcompany_email.Text & " ", "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtob_address.Text)) <> "", "ON BEHALF OF " & txtob_address.Text, "") & IIf(Check_NULLALL(txtNewEmail_ch01.Text) <> "", " E-mail: " & txtNewEmail_ch01.Text, "")
                ElseIf Mid(txtdest_remark.Text, 2, 1) <> "/" Then
                    StrtxtRerutn = ""
                    'StrtxtRerutn = txtcompany_name.Text & " " & txtcompany_address.Text & " " & txtcompany_province.Text _
                    '                    & " " & txtcompany_country.Text & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") & _
                    '                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") _
                    '                    & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID: " & txtcompany_taxno.Text, "") & _
                    '                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")

                ElseIf Mid(txtdest_remark.Text, 1, 5) <> "O/TOB" Then
                    StrtxtRerutn = txtcompany_name.Text & " " & txtcompany_address.Text & " " & txtcompany_province.Text _
                                    & " " & txtcompany_country.Text & " " & txtob_address.Text &
                                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") &
                                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") _
                                    & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID: " & txtcompany_taxno.Text, "") &
                                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")

                ElseIf Mid(txtdest_remark.Text, 1, 5) <> "O/TCO" Then
                    StrtxtRerutn = txtob_address.Text & txtcompany_name.Text & " " & txtcompany_address.Text & " " & txtcompany_province.Text _
                                    & " " & txtcompany_country.Text &
                                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") &
                                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") _
                                    & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID: " & txtcompany_taxno.Text, "") &
                                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")

                End If
            Case "FORM4_91", "FORM4_911"
                Select Case CommonUtility.Get_StringValue(txtCheck_StatusWeb.Text)
                    Case Is <> ""
                        If Mid(txtdest_remark.Text, 1, 3) = "C/O" Then
                            StrtxtRerutn = txtob_address.Text & IIf(Check_NULLALL(txtNewEmail_ch01.Text) <> "", " E-mail: " & txtNewEmail_ch01.Text, "") & " CARE OF " & txtcompany_name.Text & " " & txtcompany_address.Text _
                                               & " " & txtcompany_province.Text & " " & txtcompany_country.Text &
                                               IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") &
                                               IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") &
                                               IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID:  " & txtcompany_taxno.Text, "") &
                                               IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")
                        ElseIf Mid(txtdest_remark.Text, 1, 3) = "O/B" Then
                            StrtxtRerutn = IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_email.Text)) <> "", "E-mail: " & txtcompany_email.Text & " ", "") &
                                                IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtob_address.Text)) <> "", "ON BEHALF OF " & txtob_address.Text, "") & IIf(Check_NULLALL(txtNewEmail_ch01.Text) <> "", " E-mail: " & txtNewEmail_ch01.Text, "")
                        ElseIf Mid(txtdest_remark.Text, 2, 1) <> "/" Then
                            StrtxtRerutn = ""
                            'StrtxtRerutn = txtcompany_name.Text & " " & txtcompany_address.Text & " " & txtcompany_province.Text _
                            '                    & " " & txtcompany_country.Text & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") & _
                            '                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") _
                            '                    & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID: " & txtcompany_taxno.Text, "") & _
                            '                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")

                        ElseIf Mid(txtdest_remark.Text, 1, 5) <> "O/TOB" Then
                            StrtxtRerutn = txtcompany_name.Text & " " & txtcompany_address.Text & " " & txtcompany_province.Text _
                                            & " " & txtcompany_country.Text & " " & txtob_address.Text &
                                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") &
                                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") _
                                            & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID: " & txtcompany_taxno.Text, "") &
                                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")

                        ElseIf Mid(txtdest_remark.Text, 1, 5) <> "O/TCO" Then
                            StrtxtRerutn = txtob_address.Text & txtcompany_name.Text & " " & txtcompany_address.Text & " " & txtcompany_province.Text _
                                            & " " & txtcompany_country.Text &
                                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") &
                                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") _
                                            & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID: " & txtcompany_taxno.Text, "") &
                                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")

                        End If
                    Case Is = "" '�к����
                        If Mid(txtdest_remark.Text, 1, 3) = "C/O" Then
                            StrtxtRerutn = txtob_address.Text & " CARE OF " & txtcompany_name.Text & " " & txtcompany_address.Text _
                                               & " " & txtcompany_province.Text & " " & txtcompany_country.Text &
                                               IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") &
                                               IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") &
                                               IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID:  " & txtcompany_taxno.Text, "") &
                                               IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdeclare_doc.Text)) <> "", " E-mail: " & txtdeclare_doc.Text, "")
                        ElseIf Mid(txtdest_remark.Text, 1, 3) = "O/B" Then
                            StrtxtRerutn = IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdeclare_doc.Text)) <> "", "E-mail: " & txtdeclare_doc.Text & " ", "") &
                                                IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtob_address.Text)) <> "", "ON BEHALF OF " & txtob_address.Text, "")
                        ElseIf Mid(txtdest_remark.Text, 2, 1) <> "/" Then
                            StrtxtRerutn = ""
                            'StrtxtRerutn = txtcompany_name.Text & " " & txtcompany_address.Text & " " & txtcompany_province.Text _
                            '                    & " " & txtcompany_country.Text & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") & _
                            '                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") _
                            '                    & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID: " & txtcompany_taxno.Text, "") & _
                            '                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdeclare_doc.Text)) <> "", " E-mail: " & txtdeclare_doc.Text, "")

                        ElseIf Mid(txtdest_remark.Text, 1, 5) <> "O/TOB" Then
                            StrtxtRerutn = txtcompany_name.Text & " " & txtcompany_address.Text & " " & txtcompany_province.Text _
                                            & " " & txtcompany_country.Text & " " & txtob_address.Text &
                                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") &
                                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") _
                                            & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID: " & txtcompany_taxno.Text, "") &
                                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdeclare_doc.Text)) <> "", " E-mail: " & txtdeclare_doc.Text, "")

                        ElseIf Mid(txtdest_remark.Text, 1, 5) <> "O/TCO" Then
                            StrtxtRerutn = txtob_address.Text & txtcompany_name.Text & " " & txtcompany_address.Text & " " & txtcompany_province.Text _
                                            & " " & txtcompany_country.Text &
                                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") &
                                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") _
                                            & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID: " & txtcompany_taxno.Text, "") &
                                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdeclare_doc.Text)) <> "", " E-mail: " & txtdeclare_doc.Text, "")

                        End If
                End Select
            Case "FORM4_9"
                Select Case CommonUtility.Get_StringValue(txtCheck_StatusWeb.Text)
                    Case Is <> ""
                        If Mid(txtdest_remark.Text, 1, 3) = "C/O" Then
                            StrtxtRerutn = txtob_address.Text & IIf(Check_NULLALL(txtNewEmail_ch01.Text) <> "", " E-mail: " & txtNewEmail_ch01.Text, "") & " CARE OF " & txtcompany_name.Text & " " & txtcompany_address.Text _
                                               & " " & txtcompany_province.Text & " " & txtcompany_country.Text &
                                               IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") &
                                               IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") &
                                               IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID:  " & txtcompany_taxno.Text, "") &
                                               IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")
                        ElseIf Mid(txtdest_remark.Text, 1, 3) = "O/B" Then
                            StrtxtRerutn = IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_email.Text)) <> "", "E-mail: " & txtcompany_email.Text & " ", "") &
                                                IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtob_address.Text)) <> "", "ON BEHALF OF " & txtob_address.Text, "") & IIf(Check_NULLALL(txtNewEmail_ch01.Text) <> "", " E-mail: " & txtNewEmail_ch01.Text, "")
                        ElseIf Mid(txtdest_remark.Text, 2, 1) <> "/" Then
                            StrtxtRerutn = ""
                            'StrtxtRerutn = txtcompany_name.Text & " " & txtcompany_address.Text & " " & txtcompany_province.Text _
                            '                    & " " & txtcompany_country.Text & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") & _
                            '                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") _
                            '                    & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID: " & txtcompany_taxno.Text, "") & _
                            '                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")

                        ElseIf Mid(txtdest_remark.Text, 1, 5) <> "O/TOB" Then
                            StrtxtRerutn = txtcompany_name.Text & " " & txtcompany_address.Text & " " & txtcompany_province.Text _
                                            & " " & txtcompany_country.Text & " " & txtob_address.Text &
                                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") &
                                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") _
                                            & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID: " & txtcompany_taxno.Text, "") &
                                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")

                        ElseIf Mid(txtdest_remark.Text, 1, 5) <> "O/TCO" Then
                            StrtxtRerutn = txtob_address.Text & txtcompany_name.Text & " " & txtcompany_address.Text & " " & txtcompany_province.Text _
                                            & " " & txtcompany_country.Text &
                                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") &
                                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") _
                                            & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID: " & txtcompany_taxno.Text, "") &
                                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")

                        End If
                    Case Is = "" '�к����
                        If Mid(txtdest_remark.Text, 1, 3) = "C/O" Then
                            StrtxtRerutn = txtob_address.Text & " CARE OF " & txtcompany_name.Text & " " & txtcompany_address.Text _
                                               & " " & txtcompany_province.Text & " " & txtcompany_country.Text &
                                               IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") &
                                               IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") &
                                               IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID:  " & txtcompany_taxno.Text, "") &
                                               IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdeclare_doc.Text)) <> "", " E-mail: " & txtdeclare_doc.Text, "")
                        ElseIf Mid(txtdest_remark.Text, 1, 3) = "O/B" Then
                            StrtxtRerutn = IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdeclare_doc.Text)) <> "", "E-mail: " & txtdeclare_doc.Text & " ", "") &
                                                IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtob_address.Text)) <> "", "ON BEHALF OF " & txtob_address.Text, "")
                        ElseIf Mid(txtdest_remark.Text, 2, 1) <> "/" Then
                            StrtxtRerutn = ""
                            'StrtxtRerutn = txtcompany_name.Text & " " & txtcompany_address.Text & " " & txtcompany_province.Text _
                            '                    & " " & txtcompany_country.Text & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") & _
                            '                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") _
                            '                    & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID: " & txtcompany_taxno.Text, "") & _
                            '                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdeclare_doc.Text)) <> "", " E-mail: " & txtdeclare_doc.Text, "")

                        ElseIf Mid(txtdest_remark.Text, 1, 5) <> "O/TOB" Then
                            StrtxtRerutn = txtcompany_name.Text & " " & txtcompany_address.Text & " " & txtcompany_province.Text _
                                            & " " & txtcompany_country.Text & " " & txtob_address.Text &
                                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") &
                                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") _
                                            & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID: " & txtcompany_taxno.Text, "") &
                                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdeclare_doc.Text)) <> "", " E-mail: " & txtdeclare_doc.Text, "")

                        ElseIf Mid(txtdest_remark.Text, 1, 5) <> "O/TCO" Then
                            StrtxtRerutn = txtob_address.Text & txtcompany_name.Text & " " & txtcompany_address.Text & " " & txtcompany_province.Text _
                                            & " " & txtcompany_country.Text &
                                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") &
                                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") _
                                            & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID: " & txtcompany_taxno.Text, "") &
                                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdeclare_doc.Text)) <> "", " E-mail: " & txtdeclare_doc.Text, "")

                        End If
                End Select
            Case "FORM4_8", "FORM4_81"
                If Mid(txtdest_remark.Text, 1, 3) = "C/O" Then
                    StrtxtRerutn = txtob_address.Text & IIf(Check_NULLALL(txtNewEmail_ch01.Text) <> "", " E-mail: " & txtNewEmail_ch01.Text, "") & " CARE OF " & txtcompany_name.Text & " " & txtcompany_address.Text _
                                       & " " & txtcompany_province.Text & " " & txtcompany_country.Text &
                                       IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") &
                                       IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") &
                                       IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID:  " & txtcompany_taxno.Text, "") &
                                       IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")
                ElseIf Mid(txtdest_remark.Text, 1, 3) = "O/B" Then
                    StrtxtRerutn = IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_email.Text)) <> "", "E-mail: " & txtcompany_email.Text & " ", "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtob_address.Text)) <> "", "ON BEHALF OF " & txtob_address.Text, "") & IIf(Check_NULLALL(txtNewEmail_ch01.Text) <> "", " E-mail: " & txtNewEmail_ch01.Text, "")
                ElseIf Mid(txtdest_remark.Text, 2, 1) <> "/" Then
                    StrtxtRerutn = ""
                    'StrtxtRerutn = txtcompany_name.Text & " " & txtcompany_address.Text & " " & txtcompany_province.Text _
                    '                    & " " & txtcompany_country.Text & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") & _
                    '                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") _
                    '                    & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID: " & txtcompany_taxno.Text, "") & _
                    '                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")

                ElseIf Mid(txtdest_remark.Text, 1, 5) <> "O/TOB" Then
                    StrtxtRerutn = txtcompany_name.Text & " " & txtcompany_address.Text & " " & txtcompany_province.Text _
                                    & " " & txtcompany_country.Text & " " & txtob_address.Text &
                                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") &
                                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") _
                                    & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID: " & txtcompany_taxno.Text, "") &
                                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")

                ElseIf Mid(txtdest_remark.Text, 1, 5) <> "O/TCO" Then
                    StrtxtRerutn = txtob_address.Text & txtcompany_name.Text & " " & txtcompany_address.Text & " " & txtcompany_province.Text _
                                    & " " & txtcompany_country.Text &
                                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") &
                                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") _
                                    & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID: " & txtcompany_taxno.Text, "") &
                                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")

                End If
            Case "FORM4_6", "FORM4_61"
                If Mid(txtdest_remark.Text, 1, 3) = "C/O" Then
                    StrtxtRerutn = txtob_address.Text & IIf(Check_NULLALL(txtNewEmail_ch01.Text) <> "", " E-mail: " & txtNewEmail_ch01.Text, "") & " CARE OF " & txtcompany_name.Text & " " & txtcompany_address.Text _
                                       & " " & txtcompany_province.Text & " " & txtcompany_country.Text &
                                       IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") &
                                       IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") &
                                       IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID:  " & txtcompany_taxno.Text, "") &
                                       IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")
                ElseIf Mid(txtdest_remark.Text, 1, 3) = "O/B" Then
                    StrtxtRerutn = IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_email.Text)) <> "", "E-mail: " & txtcompany_email.Text & " ", "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtob_address.Text)) <> "", "ON BEHALF OF " & txtob_address.Text, "") & IIf(Check_NULLALL(txtNewEmail_ch01.Text) <> "", " E-mail: " & txtNewEmail_ch01.Text, "")
                ElseIf Mid(txtdest_remark.Text, 2, 1) <> "/" Then
                    StrtxtRerutn = ""
                    'StrtxtRerutn = txtcompany_name.Text & " " & txtcompany_address.Text & " " & txtcompany_province.Text _
                    '                    & " " & txtcompany_country.Text & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") & _
                    '                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") _
                    '                    & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID: " & txtcompany_taxno.Text, "") & _
                    '                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")

                ElseIf Mid(txtdest_remark.Text, 1, 5) <> "O/TOB" Then
                    StrtxtRerutn = IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text & " ", "") & txtob_address.Text

                ElseIf Mid(txtdest_remark.Text, 1, 5) <> "O/TCO" Then
                    StrtxtRerutn = txtob_address.Text & " " & txtcompany_name.Text & " " & txtcompany_address.Text & " " & txtcompany_province.Text _
                                    & " " & txtcompany_country.Text &
                                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") &
                                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") _
                                    & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID: " & txtcompany_taxno.Text, "") &
                                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")

                End If
            Case "FORM4_5"
                If Mid(txtdest_remark.Text, 1, 3) = "C/O" Then
                    StrtxtRerutn = txtob_address.Text & IIf(Check_NULLALL(txtNewEmail_ch01.Text) <> "", " E-mail: " & txtNewEmail_ch01.Text, "") & " CARE OF " & txtcompany_name.Text & " " & txtcompany_address.Text _
                                       & " " & txtcompany_province.Text & " " & txtcompany_country.Text &
                                       IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") &
                                       IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") &
                                       IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID:  " & txtcompany_taxno.Text, "") &
                                       IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")
                ElseIf Mid(txtdest_remark.Text, 1, 3) = "O/B" Then
                    StrtxtRerutn = IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_email.Text)) <> "", "E-mail: " & txtcompany_email.Text & " ", "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtob_address.Text)) <> "", "ON BEHALF OF " & txtob_address.Text, "") & IIf(Check_NULLALL(txtNewEmail_ch01.Text) <> "", " E-mail: " & txtNewEmail_ch01.Text, "")
                ElseIf Mid(txtdest_remark.Text, 2, 1) <> "/" Then
                    StrtxtRerutn = ""
                    'StrtxtRerutn = txtcompany_name.Text & " " & txtcompany_address.Text & " " & txtcompany_province.Text _
                    '                    & " " & txtcompany_country.Text & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") & _
                    '                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") _
                    '                    & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID: " & txtcompany_taxno.Text, "") & _
                    '                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")

                ElseIf Mid(txtdest_remark.Text, 1, 5) = "O/TOB" Then
                    StrtxtRerutn = IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text & " ", "") & txtob_address.Text

                ElseIf Mid(txtdest_remark.Text, 1, 5) = "O/TCO" Then
                    StrtxtRerutn = txtob_address.Text & " " & txtcompany_name.Text & " " & txtcompany_address.Text & " " & txtcompany_province.Text _
                                    & " " & txtcompany_country.Text &
                                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") &
                                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") _
                                    & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID: " & txtcompany_taxno.Text, "") &
                                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")

                End If
            Case "FORM4_4"
                If Mid(txtdest_remark.Text, 1, 3) = "C/O" Then
                    StrtxtRerutn = txtob_address.Text & IIf(Check_NULLALL(txtNewEmail_ch01.Text) <> "", " E-mail: " & txtNewEmail_ch01.Text, "") & " CARE OF " & txtcompany_name.Text & " " & txtcompany_address.Text _
                                       & " " & txtcompany_province.Text & " " & txtcompany_country.Text &
                                       IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") &
                                       IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") &
                                       IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID:  " & txtcompany_taxno.Text, "") &
                                       IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")
                ElseIf Mid(txtdest_remark.Text, 1, 3) = "O/B" Then
                    StrtxtRerutn = IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_email.Text)) <> "", "E-mail: " & txtcompany_email.Text & " ", "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtob_address.Text)) <> "", "ON BEHALF OF " & txtob_address.Text, "") & IIf(Check_NULLALL(txtNewEmail_ch01.Text) <> "", " E-mail: " & txtNewEmail_ch01.Text, "")
                ElseIf Mid(txtdest_remark.Text, 2, 1) <> "/" Then
                    StrtxtRerutn = ""
                    'StrtxtRerutn = txtcompany_name.Text & " " & txtcompany_address.Text & " " & txtcompany_province.Text _
                    '                    & " " & txtcompany_country.Text & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") & _
                    '                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") _
                    '                    & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID: " & txtcompany_taxno.Text, "") & _
                    '                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")

                ElseIf Mid(txtdest_remark.Text, 1, 5) <> "O/TOB" Then
                    StrtxtRerutn = txtcompany_name.Text & " " & txtcompany_address.Text & " " & txtcompany_province.Text _
                                    & " " & txtcompany_country.Text & " " & txtob_address.Text &
                                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") &
                                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") _
                                    & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID: " & txtcompany_taxno.Text, "") &
                                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")

                ElseIf Mid(txtdest_remark.Text, 1, 5) <> "O/TCO" Then
                    StrtxtRerutn = txtob_address.Text & txtcompany_name.Text & " " & txtcompany_address.Text & " " & txtcompany_province.Text _
                                    & " " & txtcompany_country.Text &
                                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") &
                                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") _
                                    & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID: " & txtcompany_taxno.Text, "") &
                                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")

                End If
            Case "FORM4_2", "FORME_01"
                Select Case CommonUtility.Get_StringValue(txtTax_Status.Text)
                    Case 1 '��ͧ ����ʴ���������ʴ� Tax 1 ��� �ʴ�
                        If Mid(txtdest_remark.Text, 1, 3) = "C/O" Then
                            StrtxtRerutn = txtob_address.Text & IIf(Check_NULLALL(txtNewEmail_ch01.Text) <> "", " E-mail: " & txtNewEmail_ch01.Text, "") & " CARE OF " & txtcompany_name.Text & " " & txtcompany_address.Text _
                                               & " " & txtcompany_province.Text & " " & txtcompany_country.Text &
                                               IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") &
                                               IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") &
                                               IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID:  " & txtcompany_taxno.Text, "") &
                                               IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")
                        ElseIf Mid(txtdest_remark.Text, 1, 3) = "O/B" Then
                            StrtxtRerutn = IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_email.Text)) <> "", "E-mail: " & txtcompany_email.Text & " ", "") &
                                                IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtob_address.Text)) <> "", "ON BEHALF OF " & txtob_address.Text, "") & IIf(Check_NULLALL(txtNewEmail_ch01.Text) <> "", " E-mail: " & txtNewEmail_ch01.Text, "")
                        ElseIf Mid(txtdest_remark.Text, 2, 1) <> "/" Then
                            StrtxtRerutn = ""
                            'StrtxtRerutn = txtcompany_name.Text & " " & txtcompany_address.Text & " " & txtcompany_province.Text _
                            '                    & " " & txtcompany_country.Text & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") & _
                            '                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") _
                            '                    & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID: " & txtcompany_taxno.Text, "") & _
                            '                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")
                        End If
                    Case 0 '��ͧ ����ʴ���������ʴ� Tax 0 ��� ����ʴ�
                        If Mid(txtdest_remark.Text, 1, 3) = "C/O" Then
                            StrtxtRerutn = txtob_address.Text & IIf(Check_NULLALL(txtNewEmail_ch01.Text) <> "", " E-mail: " & txtNewEmail_ch01.Text, "") & " CARE OF " & txtcompany_name.Text & " " & txtcompany_address.Text _
                                               & " " & txtcompany_province.Text & " " & txtcompany_country.Text &
                                               IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") &
                                               IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") &
                                               IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")
                        ElseIf Mid(txtdest_remark.Text, 1, 3) = "O/B" Then
                            StrtxtRerutn = IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_email.Text)) <> "", "E-mail: " & txtcompany_email.Text & " ", "") &
                                                IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtob_address.Text)) <> "", "ON BEHALF OF " & txtob_address.Text, "") & IIf(Check_NULLALL(txtNewEmail_ch01.Text) <> "", " E-mail: " & txtNewEmail_ch01.Text, "")
                        ElseIf Mid(txtdest_remark.Text, 2, 1) <> "/" Then
                            StrtxtRerutn = ""
                            'StrtxtRerutn = txtcompany_name.Text & " " & txtcompany_address.Text & " " & txtcompany_province.Text _
                            '                    & " " & txtcompany_country.Text & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") & _
                            '                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") _
                            '                    & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")
                        End If
                End Select
            Case "FORM4_1", "FORM44_41"
                If Mid(txtdest_remark.Text, 1, 3) = "C/O" Then
                    StrtxtRerutn = txtob_address.Text & IIf(Check_NULLALL(txtNewEmail_ch01.Text) <> "", " E-mail: " & txtNewEmail_ch01.Text, "") & " CARE OF " & txtcompany_name.Text & " " & txtcompany_address.Text _
                                       & " " & txtcompany_province.Text & " " & txtcompany_country.Text &
                                       IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") &
                                       IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") &
                                       IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID:  " & txtcompany_taxno.Text, "") &
                                       IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")
                ElseIf Mid(txtdest_remark.Text, 1, 3) = "O/B" Then
                    StrtxtRerutn = IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_email.Text)) <> "", "E-mail: " & txtcompany_email.Text & " ", "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtob_address.Text)) <> "", "ON BEHALF OF " & txtob_address.Text, "") & IIf(Check_NULLALL(txtNewEmail_ch01.Text) <> "", " E-mail: " & txtNewEmail_ch01.Text, "")
                ElseIf Mid(txtdest_remark.Text, 2, 1) <> "/" Then
                    StrtxtRerutn = ""
                    'StrtxtRerutn = txtcompany_name.Text & " " & txtcompany_address.Text & " " & txtcompany_province.Text _
                    '                    & " " & txtcompany_country.Text & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") & _
                    '                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") _
                    '                    & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID: " & txtcompany_taxno.Text, "") & _
                    '                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")

                ElseIf Mid(txtdest_remark.Text, 1, 5) <> "O/TOB" Then
                    StrtxtRerutn = txtcompany_name.Text & " " & txtcompany_address.Text & " " & txtcompany_province.Text _
                                    & " " & txtcompany_country.Text & " " & txtob_address.Text &
                                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") &
                                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") _
                                    & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID: " & txtcompany_taxno.Text, "") &
                                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")

                ElseIf Mid(txtdest_remark.Text, 1, 5) <> "O/TCO" Then
                    StrtxtRerutn = txtob_address.Text & txtcompany_name.Text & " " & txtcompany_address.Text & " " & txtcompany_province.Text _
                                    & " " & txtcompany_country.Text &
                                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") &
                                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") _
                                    & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID: " & txtcompany_taxno.Text, "") &
                                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")

                End If
            Case "FORM3_1"
                StrtxtRerutn = str_01 & CommonUtility.Get_StringValue(txtByCom_CH01.Text)
            Case "FORM3"
                If Mid(txtdest_remark.Text, 1, 3) = "C/O" Then
                    StrtxtRerutn = str_01 & txtob_address.Text & IIf(Check_NULLALL(txtNewEmail_ch01.Text) <> "", " E-mail: " & txtNewEmail_ch01.Text, "") & " CARE OF " & txtcompany_name.Text & " " & txtcompany_address.Text _
                                       & " " & txtcompany_province.Text & " " & txtcompany_country.Text &
                                       IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") &
                                       IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") &
                                       IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID:  " & txtcompany_taxno.Text, "") &
                                       IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")
                ElseIf Mid(txtdest_remark.Text, 1, 3) = "O/B" Then
                    StrtxtRerutn = IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_email.Text)) <> "", "E-mail: " & txtcompany_email.Text & " ", "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtob_address.Text)) <> "", "ON BEHALF OF " & txtob_address.Text, "") & IIf(Check_NULLALL(txtNewEmail_ch01.Text) <> "", " E-mail: " & txtNewEmail_ch01.Text, "")
                ElseIf Mid(txtdest_remark.Text, 2, 1) <> "/" Then
                    StrtxtRerutn = ""
                    'StrtxtRerutn = str_01 & txtcompany_name.Text & " " & txtcompany_address.Text & " " & txtcompany_province.Text _
                    '                    & " " & txtcompany_country.Text & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") & _
                    '                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") _
                    '                    & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID: " & txtcompany_taxno.Text, "") & _
                    '                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")

                End If
            Case "FORM2"
                If Mid(txtdest_remark.Text, 1, 3) = "C/O" Then
                    StrtxtRerutn = txtob_address.Text & IIf(Check_NULLALL(txtNewEmail_ch01.Text) <> "", " E-mail: " & txtNewEmail_ch01.Text, "") & " CARE OF " & txtcompany_name.Text & " " & txtcompany_address.Text _
                                       & " " & txtcompany_province.Text & " " & txtcompany_country.Text &
                                       IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") &
                                       IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") &
                                       IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID:  " & txtcompany_taxno.Text, "") &
                                       IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")
                ElseIf Mid(txtdest_remark.Text, 1, 3) = "O/B" Then
                    StrtxtRerutn = IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_email.Text)) <> "", "E-mail: " & txtcompany_email.Text & " ", "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtob_address.Text)) <> "", "ON BEHALF OF " & txtob_address.Text, "") & IIf(Check_NULLALL(txtNewEmail_ch01.Text) <> "", " E-mail: " & txtNewEmail_ch01.Text, "")
                ElseIf Mid(txtdest_remark.Text, 2, 1) <> "/" Then
                    StrtxtRerutn = ""
                    'StrtxtRerutn = txtcompany_name.Text & " " & txtcompany_address.Text & " " & txtcompany_province.Text _
                    '                    & " " & txtcompany_country.Text & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") & _
                    '                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") _
                    '                    & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID: " & txtcompany_taxno.Text, "") & _
                    '                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")

                End If
            Case "FORM2_3"
                If Mid(txtdest_remark.Text, 1, 3) = "C/O" Then
                    StrtxtRerutn = txtob_address.Text & IIf(Check_NULLALL(txtNewEmail_ch01.Text) <> "", " E-mail: " & txtNewEmail_ch01.Text, "") & " CARE OF " & txtcompany_name.Text & " " & txtcompany_address.Text _
                                       & " " & txtcompany_province.Text & " " & txtcompany_country.Text &
                                       IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") &
                                       IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") &
                                       IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID:  " & txtcompany_taxno.Text, "") &
                                       IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")
                ElseIf Mid(txtdest_remark.Text, 1, 3) = "O/B" Then
                    StrtxtRerutn = IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_email.Text)) <> "", "E-mail: " & txtcompany_email.Text & " ", "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtob_address.Text)) <> "", "ON BEHALF OF " & txtob_address.Text, "") & IIf(Check_NULLALL(txtNewEmail_ch01.Text) <> "", " E-mail: " & txtNewEmail_ch01.Text, "")
                ElseIf Mid(txtdest_remark.Text, 2, 1) <> "/" Then
                    StrtxtRerutn = ""
                    'StrtxtRerutn = txtcompany_name.Text & " " & txtcompany_address.Text & " " & txtcompany_province.Text _
                    '                    & " " & txtcompany_country.Text & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") & _
                    '                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") _
                    '                    & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID: " & txtcompany_taxno.Text, "") & _
                    '                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")

                End If
            Case "FORM2_2"
                If Mid(txtdest_remark.Text, 1, 3) = "C/O" Then
                    StrtxtRerutn = txtob_address.Text & IIf(Check_NULLALL(txtNewEmail_ch01.Text) <> "", " E-mail: " & txtNewEmail_ch01.Text, "") & " CARE OF " & txtcompany_name.Text & " " & txtcompany_address.Text _
                                       & " " & txtcompany_province.Text & " " & txtcompany_country.Text &
                                       IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") &
                                       IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") &
                                       IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID:  " & txtcompany_taxno.Text, "") &
                                       IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")
                ElseIf Mid(txtdest_remark.Text, 1, 3) = "O/B" Then
                    StrtxtRerutn = IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_email.Text)) <> "", "E-mail: " & txtcompany_email.Text & " ", "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtob_address.Text)) <> "", "ON BEHALF OF " & txtob_address.Text, "") & IIf(Check_NULLALL(txtNewEmail_ch01.Text) <> "", " E-mail: " & txtNewEmail_ch01.Text, "")
                ElseIf Mid(txtdest_remark.Text, 2, 1) <> "/" Then
                    StrtxtRerutn = ""
                    'StrtxtRerutn = txtcompany_name.Text & " " & txtcompany_address.Text & " " & txtcompany_province.Text _
                    '                    & " " & txtcompany_country.Text & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") & _
                    '                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") _
                    '                    & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID: " & txtcompany_taxno.Text, "") & _
                    '                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")

                End If

                ''ByTine 11-12-2559
                ''��������� CO��� / CO����
            Case "FORM2_5"
                If Mid(txtdest_remark.Text, 1, 3) = "C/O" Then
                    StrtxtRerutn = txtob_address.Text & IIf(Check_NULLALL(txtNewEmail_ch01.Text) <> "", " E-mail: " & txtNewEmail_ch01.Text, "") & " CARE OF " & txtcompany_name.Text & " " & txtcompany_address.Text _
                                       & " " & txtcompany_province.Text & " " & txtcompany_country.Text &
                                       IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") &
                                       IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") &
                                       IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID:  " & txtcompany_taxno.Text, "") &
                                       IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")
                ElseIf Mid(txtdest_remark.Text, 1, 3) = "O/B" Then
                    StrtxtRerutn = IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_email.Text)) <> "", "E-mail: " & txtcompany_email.Text & " ", "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtob_address.Text)) <> "", "ON BEHALF OF " & txtob_address.Text, "") & IIf(Check_NULLALL(txtNewEmail_ch01.Text) <> "", " E-mail: " & txtNewEmail_ch01.Text, "")
                ElseIf Mid(txtdest_remark.Text, 2, 1) <> "/" Then
                    StrtxtRerutn = ""
                    'StrtxtRerutn = txtcompany_name.Text & " " & txtcompany_address.Text & " " & txtcompany_province.Text _
                    '                    & " " & txtcompany_country.Text & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") & _
                    '                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") _
                    '                    & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID: " & txtcompany_taxno.Text, "") & _
                    '                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")

                End If

                ''ByTine 11-12-2559
                ''��������� CO��� / CO����
            Case "FORM2_6"
                If Mid(txtdest_remark.Text, 1, 3) = "C/O" Then
                    StrtxtRerutn = txtob_address.Text & IIf(Check_NULLALL(txtNewEmail_ch01.Text) <> "", " E-mail: " & txtNewEmail_ch01.Text, "") & " CARE OF " & txtcompany_name.Text & " " & txtcompany_address.Text _
                                       & " " & txtcompany_province.Text & " " & txtcompany_country.Text &
                                       IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") &
                                       IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") &
                                       IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID:  " & txtcompany_taxno.Text, "") &
                                       IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")
                ElseIf Mid(txtdest_remark.Text, 1, 3) = "O/B" Then
                    StrtxtRerutn = IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_email.Text)) <> "", "E-mail: " & txtcompany_email.Text & " ", "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtob_address.Text)) <> "", "ON BEHALF OF " & txtob_address.Text, "") & IIf(Check_NULLALL(txtNewEmail_ch01.Text) <> "", " E-mail: " & txtNewEmail_ch01.Text, "")
                ElseIf Mid(txtdest_remark.Text, 2, 1) <> "/" Then
                    StrtxtRerutn = ""
                    'StrtxtRerutn = txtcompany_name.Text & " " & txtcompany_address.Text & " " & txtcompany_province.Text _
                    '                    & " " & txtcompany_country.Text & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") & _
                    '                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") _
                    '                    & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID: " & txtcompany_taxno.Text, "") & _
                    '                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")

                End If

            Case "FORMRUSSIA"
                If Mid(txtdest_remark.Text, 1, 3) = "C/O" Then
                    StrtxtRerutn = txtob_address.Text & IIf(Check_NULLALL(txtNewEmail_ch01.Text) <> "", " E-mail: " & txtNewEmail_ch01.Text, "") & " CARE OF " & txtcompany_name.Text & " " & txtcompany_address.Text _
                                       & " " & txtcompany_province.Text & " " & txtcompany_country.Text &
                                       IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") &
                                       IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") &
                                       IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID:  " & txtcompany_taxno.Text, "") &
                                       IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")
                ElseIf Mid(txtdest_remark.Text, 1, 3) = "O/B" Then
                    StrtxtRerutn = IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_email.Text)) <> "", "E-mail: " & txtcompany_email.Text & " ", "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtob_address.Text)) <> "", "ON BEHALF OF " & txtob_address.Text, "") & IIf(Check_NULLALL(txtNewEmail_ch01.Text) <> "", " E-mail: " & txtNewEmail_ch01.Text, "")
                ElseIf Mid(txtdest_remark.Text, 2, 1) <> "/" Then
                    StrtxtRerutn = ""
                    'StrtxtRerutn = txtcompany_name.Text & " " & txtcompany_address.Text & " " & txtcompany_province.Text _
                    '                    & " " & txtcompany_country.Text & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") & _
                    '                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") _
                    '                    & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID: " & txtcompany_taxno.Text, "") & _
                    '                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")

                End If
            Case "FORM1"
                If Mid(txtdest_remark.Text, 1, 3) = "C/O" Then
                    StrtxtRerutn = txtob_address.Text & IIf(Check_NULLALL(txtNewEmail_ch01.Text) <> "", " E-mail: " & txtNewEmail_ch01.Text, "") & " CARE OF " & txtcompany_name.Text & " " & txtcompany_address.Text _
                                       & " " & txtcompany_province.Text & " " & txtcompany_country.Text &
                                       IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") &
                                       IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") &
                                       IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID:  " & txtcompany_taxno.Text, "") &
                                       IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")
                ElseIf Mid(txtdest_remark.Text, 1, 3) = "O/B" Then
                    StrtxtRerutn = IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_email.Text)) <> "", "E-mail: " & txtcompany_email.Text & " ", "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtob_address.Text)) <> "", "ON BEHALF OF " & txtob_address.Text, "") & IIf(Check_NULLALL(txtNewEmail_ch01.Text) <> "", " E-mail: " & txtNewEmail_ch01.Text, "")
                ElseIf Mid(txtdest_remark.Text, 2, 1) <> "/" Then
                    StrtxtRerutn = ""
                    'StrtxtRerutn = txtcompany_name.Text & " " & txtcompany_address.Text & " " & txtcompany_province.Text _
                    '                    & " " & txtcompany_country.Text & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") & _
                    '                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") _
                    '                    & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID: " & txtcompany_taxno.Text, "") & _
                    '                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")

                End If
            Case "FORM1_4"
                If Mid(txtdest_remark.Text, 1, 3) = "C/O" Then
                    StrtxtRerutn = txtob_address.Text & IIf(Check_NULLALL(txtNewEmail_ch01.Text) <> "", " E-mail: " & txtNewEmail_ch01.Text, "") & " CARE OF " & txtcompany_name.Text & " " & txtcompany_address.Text _
                                       & " " & txtcompany_province.Text & " " & txtcompany_country.Text &
                                       IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") &
                                       IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") &
                                       IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID:  " & txtcompany_taxno.Text, "") &
                                       IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")
                ElseIf Mid(txtdest_remark.Text, 1, 3) = "O/B" Then
                    StrtxtRerutn = IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_email.Text)) <> "", "E-mail: " & txtcompany_email.Text & " ", "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtob_address.Text)) <> "", "ON BEHALF OF " & txtob_address.Text, "") & IIf(Check_NULLALL(txtNewEmail_ch01.Text) <> "", " E-mail: " & txtNewEmail_ch01.Text, "")
                ElseIf Mid(txtdest_remark.Text, 2, 1) <> "/" Then
                    StrtxtRerutn = ""
                    'StrtxtRerutn = txtcompany_name.Text & " " & txtcompany_address.Text & " " & txtcompany_province.Text _
                    '                    & " " & txtcompany_country.Text & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") & _
                    '                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") _
                    '                    & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID: " & txtcompany_taxno.Text, "") & _
                    '                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")

                End If
            Case "FORM1_3"
                If Mid(txtdest_remark.Text, 1, 3) = "C/O" Then
                    StrtxtRerutn = txtob_address.Text & IIf(Check_NULLALL(txtNewEmail_ch01.Text) <> "", " E-mail: " & txtNewEmail_ch01.Text, "") & " CARE OF " & txtcompany_name.Text & " " & txtcompany_address.Text _
                                       & " " & txtcompany_province.Text & " " & txtcompany_country.Text &
                                       IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") &
                                       IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") &
                                       IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID:  " & txtcompany_taxno.Text, "") &
                                       IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")
                ElseIf Mid(txtdest_remark.Text, 1, 3) = "O/B" Then
                    StrtxtRerutn = IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_email.Text)) <> "", "E-mail: " & txtcompany_email.Text & " ", "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtob_address.Text)) <> "", "ON BEHALF OF " & txtob_address.Text, "") & IIf(Check_NULLALL(txtNewEmail_ch01.Text) <> "", " E-mail: " & txtNewEmail_ch01.Text, "")
                ElseIf Mid(txtdest_remark.Text, 2, 1) <> "/" Then
                    StrtxtRerutn = ""
                    'StrtxtRerutn = txtcompany_name.Text & " " & txtcompany_address.Text & " " & txtcompany_province.Text _
                    '                    & " " & txtcompany_country.Text & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") & _
                    '                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") _
                    '                    & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID: " & txtcompany_taxno.Text, "") & _
                    '                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")

                End If
            Case "FORM1_2"
                If Mid(txtdest_remark.Text, 1, 3) = "C/O" Then
                    StrtxtRerutn = txtob_address.Text & IIf(Check_NULLALL(txtNewEmail_ch01.Text) <> "", " E-mail: " & txtNewEmail_ch01.Text, "") & " CARE OF " & txtcompany_name.Text & " " & txtcompany_address.Text _
                                       & " " & txtcompany_province.Text & " " & txtcompany_country.Text &
                                       IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") &
                                       IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") &
                                       IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID:  " & txtcompany_taxno.Text, "") &
                                       IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")
                ElseIf Mid(txtdest_remark.Text, 1, 3) = "O/B" Then
                    StrtxtRerutn = IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_email.Text)) <> "", "E-mail: " & txtcompany_email.Text & " ", "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtob_address.Text)) <> "", "ON BEHALF OF " & txtob_address.Text, "") & IIf(Check_NULLALL(txtNewEmail_ch01.Text) <> "", " E-mail: " & txtNewEmail_ch01.Text, "")
                ElseIf Mid(txtdest_remark.Text, 2, 1) <> "/" Then
                    StrtxtRerutn = ""
                    'StrtxtRerutn = txtcompany_name.Text & " " & txtcompany_address.Text & " " & txtcompany_province.Text _
                    '                    & " " & txtcompany_country.Text & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") & _
                    '                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") _
                    '                    & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID: " & txtcompany_taxno.Text, "") & _
                    '                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")

                End If
            Case "FORM1_1"
                If Mid(txtdest_remark.Text, 1, 3) = "C/O" Then
                    StrtxtRerutn = txtob_address.Text & IIf(Check_NULLALL(txtNewEmail_ch01.Text) <> "", " E-mail: " & txtNewEmail_ch01.Text, "") & " CARE OF " & txtcompany_name.Text & " " & txtcompany_address.Text _
                                       & " " & txtcompany_province.Text & " " & txtcompany_country.Text &
                                       IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") &
                                       IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") &
                                       IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID:  " & txtcompany_taxno.Text, "") &
                                       IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")
                ElseIf Mid(txtdest_remark.Text, 1, 3) = "O/B" Then
                    StrtxtRerutn = IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_email.Text)) <> "", "E-mail: " & txtcompany_email.Text & " ", "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtob_address.Text)) <> "", "ON BEHALF OF " & txtob_address.Text, "") & IIf(Check_NULLALL(txtNewEmail_ch01.Text) <> "", " E-mail: " & txtNewEmail_ch01.Text, "")
                ElseIf Mid(txtdest_remark.Text, 2, 1) <> "/" Then
                    StrtxtRerutn = ""
                    'StrtxtRerutn = txtcompany_name.Text & " " & txtcompany_address.Text & " " & txtcompany_province.Text _
                    '                    & " " & txtcompany_country.Text & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") & _
                    '                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") _
                    '                    & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID: " & txtcompany_taxno.Text, "") & _
                    '                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")

                End If

        End Select
        StrtxtRerutnMain = StrtxtRerutn1 & StrtxtRerutn
        Return StrtxtRerutnMain
    End Function
    '===========================================================
    Function v2_Head_Checkdestination(ByVal ByCase_Form As String) As String
        Dim StrtxtRerutn As String = ""
        Select Case ByCase_Form.ToUpper
            Case "FORM2_4"
                If Mid(txtdest_remark1.Text, 1, 3) = "C/O" Then
                    StrtxtRerutn = txtob_dest_address.Text & IIf(Check_NULLALL(txtNewEmail_ch02.Text) <> "", " E-mail: " & txtNewEmail_ch02.Text, "") & " CARE OF " & txtdestination_company.Text & " " & txtdestination_address.Text _
                                        & " " & txtdestination_province.Text & " " & txtdest_receive_country.Text &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID:  " & txtdestination_taxid.Text, "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                        IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "")

                ElseIf Mid(txtdest_remark1.Text, 1, 3) = "O/B" Then
                    StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " &
                                            txtdestination_province.Text & " " & txtdest_receive_country.Text &
                                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                            & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                            " ON BEHALF OF " & txtob_dest_address.Text & IIf(Check_NULLALL(txtNewEmail_ch02.Text) <> "", " E-mail: " & txtNewEmail_ch02.Text, "") & IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "")

                ElseIf Mid(txtdest_remark1.Text, 2, 1) <> "/" Then
                    StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " & txtdestination_province.Text _
                                        & " " & txtdest_receive_country.Text &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                        & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                        IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "")

                End If
            Case "FORM4_3"
                If Mid(txtdest_remark1.Text, 1, 3) = "C/O" Then
                    StrtxtRerutn = txtob_dest_address.Text & " CARE OF " & txtdestination_company.Text & " " & txtdestination_address.Text _
                                        & " " & txtdestination_province.Text & " " & txtdest_receive_country.Text &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID:  " & txtdestination_taxid.Text, "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                        IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "")

                ElseIf Mid(txtdest_remark1.Text, 1, 3) = "O/B" Then
                    StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " &
                                            txtdestination_province.Text & " " & txtdest_receive_country.Text &
                                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                            & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                            " ON BEHALF OF " & txtob_dest_address.Text & IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "")

                ElseIf Mid(txtdest_remark1.Text, 2, 1) <> "/" Then
                    StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " & txtdestination_province.Text _
                                        & " " & txtdest_receive_country.Text &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                        & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                        IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "")

                ElseIf Mid(txtdest_remark1.Text, 1, 5) <> "O/TOB" Then
                    StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " & txtdestination_province.Text _
                                    & " " & txtdest_receive_country.Text & " " & txtob_dest_address.Text &
                                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                    & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                    IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "")

                ElseIf Mid(txtdest_remark1.Text, 1, 5) <> "O/TCO" Then
                    StrtxtRerutn = txtob_dest_address.Text & " " & txtdestination_company.Text & " " & txtdestination_address.Text & " " & txtdestination_province.Text _
                                    & " " & txtdest_receive_country.Text &
                                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                    & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                    IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "")

                End If
            Case "FORM9"
                If Mid(txtdest_remark1.Text, 1, 3) = "C/O" Then
                    StrtxtRerutn = txtob_dest_address.Text & IIf(Check_NULLALL(txtNewEmail_ch02.Text) <> "", " E-mail: " & txtNewEmail_ch02.Text, "") & " CARE OF " & txtdestination_company.Text & " " & txtdestination_address.Text _
                                        & " " & txtdestination_province.Text & " " & txtdest_receive_country.Text &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID:  " & txtdestination_taxid.Text, "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                        IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "")

                ElseIf Mid(txtdest_remark1.Text, 1, 3) = "O/B" Then
                    StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " &
                                            txtdestination_province.Text & " " & txtdest_receive_country.Text &
                                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                            & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                            " ON BEHALF OF " & txtob_dest_address.Text & IIf(Check_NULLALL(txtNewEmail_ch02.Text) <> "", " E-mail: " & txtNewEmail_ch02.Text, "") & IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "")

                ElseIf Mid(txtdest_remark1.Text, 2, 1) <> "/" Then
                    StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " & txtdestination_province.Text _
                                        & " " & txtdest_receive_country.Text &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                        & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                        IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "")

                End If
            Case "FORM8"
                If Mid(txtdest_remark1.Text, 1, 3) = "C/O" Then
                    StrtxtRerutn = txtob_dest_address.Text & IIf(Check_NULLALL(txtNewEmail_ch02.Text) <> "", " E-mail: " & txtNewEmail_ch02.Text, "") & " CARE OF " & txtdestination_company.Text & " " & txtdestination_address.Text _
                                        & " " & txtdestination_province.Text & " " & txtdest_receive_country.Text &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID:  " & txtdestination_taxid.Text, "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                        IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "")

                ElseIf Mid(txtdest_remark1.Text, 1, 3) = "O/B" Then
                    StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " &
                                            txtdestination_province.Text & " " & txtdest_receive_country.Text &
                                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                            & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                            " ON BEHALF OF " & txtob_dest_address.Text & IIf(Check_NULLALL(txtNewEmail_ch02.Text) <> "", " E-mail: " & txtNewEmail_ch02.Text, "") & IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "")

                ElseIf Mid(txtdest_remark1.Text, 2, 1) <> "/" Then
                    StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " & txtdestination_province.Text _
                                        & " " & txtdest_receive_country.Text &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                        & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                        IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "")

                End If
            Case "FORM7"
                If Mid(txtdest_remark1.Text, 1, 3) = "C/O" Then
                    StrtxtRerutn = txtob_dest_address.Text & IIf(Check_NULLALL(txtNewEmail_ch02.Text) <> "", " E-mail: " & txtNewEmail_ch02.Text, "") & " CARE OF " & txtdestination_company.Text & " " & txtdestination_address.Text _
                                        & " " & txtdestination_province.Text & " " & txtdest_receive_country.Text &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID:  " & txtdestination_taxid.Text, "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                        IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "")

                ElseIf Mid(txtdest_remark1.Text, 1, 3) = "O/B" Then
                    StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " &
                                            txtdestination_province.Text & " " & txtdest_receive_country.Text &
                                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                            & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                            " ON BEHALF OF " & txtob_dest_address.Text & IIf(Check_NULLALL(txtNewEmail_ch02.Text) <> "", " E-mail: " & txtNewEmail_ch02.Text, "") & IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "")

                ElseIf Mid(txtdest_remark1.Text, 2, 1) <> "/" Then
                    StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " & txtdestination_province.Text _
                                        & " " & txtdest_receive_country.Text &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                        & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                        IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "")

                End If
            Case "FORM6"
                If Mid(txtdest_remark1.Text, 1, 3) = "C/O" Then
                    StrtxtRerutn = txtob_dest_address.Text & IIf(txtNewEmail_ch02.Text <> "", " E-mail: " & txtNewEmail_ch02.Text, "") & " CARE OF " & txtdestination_company.Text & " " & txtdestination_address.Text _
                                        & " " & txtdestination_province.Text & " " & txtdest_receive_country.Text &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID:  " & txtdestination_taxid.Text, "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                        IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "")

                ElseIf Mid(txtdest_remark1.Text, 1, 3) = "O/B" Then
                    StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " &
                                            txtdestination_province.Text & " " & txtdest_receive_country.Text &
                                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                            & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                            " ON BEHALF OF " & txtob_dest_address.Text & IIf(Check_NULLALL(txtNewEmail_ch02.Text) <> "", " E-mail: " & txtNewEmail_ch02.Text, "") & IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "")

                ElseIf Mid(txtdest_remark1.Text, 2, 1) <> "/" Then
                    StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " & txtdestination_province.Text _
                                        & " " & txtdest_receive_country.Text &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                        & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                        IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "")

                End If
            Case "FORM5"
                If Mid(txtdest_remark1.Text, 1, 3) = "C/O" Then
                    StrtxtRerutn = txtob_dest_address.Text & " CARE OF " & txtdestination_company.Text & " " & txtdestination_address.Text _
                                        & " " & txtdestination_province.Text & " " & txtdest_receive_country.Text &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID:  " & txtdestination_taxid.Text, "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                        IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "")

                ElseIf Mid(txtdest_remark1.Text, 1, 3) = "O/B" Then
                    StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " &
                                            txtdestination_province.Text & " " & txtdest_receive_country.Text &
                                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                            & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                            " ON BEHALF OF " & txtob_dest_address.Text & IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "")

                ElseIf Mid(txtdest_remark1.Text, 2, 1) <> "/" Then
                    StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " & txtdestination_province.Text _
                                        & " " & txtdest_receive_country.Text &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                        & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                        IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "")

                End If
            Case "FORM5_1"
                If Mid(txtdest_remark1.Text, 1, 3) = "C/O" Then
                    StrtxtRerutn = txtob_dest_address.Text & " CARE OF " & txtdestination_company.Text & " " & txtdestination_address.Text _
                                        & " " & txtdestination_province.Text & " " & txtdest_receive_country.Text &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID:  " & txtdestination_taxid.Text, "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                        IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "")

                ElseIf Mid(txtdest_remark1.Text, 1, 3) = "O/B" Then
                    StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " &
                                            txtdestination_province.Text & " " & txtdest_receive_country.Text &
                                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                            & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                            " ON BEHALF OF " & txtob_dest_address.Text & IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "")

                ElseIf Mid(txtdest_remark1.Text, 2, 1) <> "/" Then
                    StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " & txtdestination_province.Text _
                                        & " " & txtdest_receive_country.Text &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                        & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                        IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "")

                ElseIf Mid(txtdest_remark1.Text, 1, 5) = "O/TOB" Then
                    StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " & txtdestination_province.Text _
                                    & " " & txtdest_receive_country.Text &
                                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                    & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                    IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "") & " " & txtob_dest_address.Text

                ElseIf Mid(txtdest_remark1.Text, 1, 5) = "O/TCO" Then
                    StrtxtRerutn = txtob_dest_address.Text & " " & txtdestination_company.Text & " " & txtdestination_address.Text & " " & txtdestination_province.Text _
                                    & " " & txtdest_receive_country.Text &
                                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                    & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                    IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "")

                End If
            Case "FORM5_2"
                If Mid(txtdest_remark1.Text, 1, 3) = "C/O" Then
                    StrtxtRerutn = txtob_dest_address.Text & " CARE OF " & txtdestination_company.Text & " " & txtdestination_address.Text _
                                        & " " & txtdestination_province.Text & " " & txtdest_receive_country.Text &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID:  " & txtdestination_taxid.Text, "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                        IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "")

                ElseIf Mid(txtdest_remark1.Text, 1, 3) = "O/B" Then
                    StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " &
                                            txtdestination_province.Text & " " & txtdest_receive_country.Text &
                                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                            & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                            " ON BEHALF OF " & txtob_dest_address.Text & IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "")

                ElseIf Mid(txtdest_remark1.Text, 2, 1) <> "/" Then
                    StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " & txtdestination_province.Text _
                                        & " " & txtdest_receive_country.Text &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                        & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                        IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "")

                ElseIf Mid(txtdest_remark1.Text, 1, 5) = "O/TOB" Then
                    StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " & txtdestination_province.Text _
                                    & " " & txtdest_receive_country.Text &
                                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                    & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                    IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "") & " " & txtob_dest_address.Text

                ElseIf Mid(txtdest_remark1.Text, 1, 5) = "O/TCO" Then
                    StrtxtRerutn = txtob_dest_address.Text & " " & txtdestination_company.Text & " " & txtdestination_address.Text & " " & txtdestination_province.Text _
                                    & " " & txtdest_receive_country.Text &
                                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                    & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                    IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "")

                End If
            Case "FORM441", "FORM441_4"
                If Mid(txtdest_remark1.Text, 1, 3) = "C/O" Then
                    StrtxtRerutn = txtob_dest_address.Text & " CARE OF " & txtdestination_company.Text & " " & txtdestination_address.Text _
                                        & " " & txtdestination_province.Text & " " & txtdest_receive_country.Text &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID:  " & txtdestination_taxid.Text, "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                        IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "")

                ElseIf Mid(txtdest_remark1.Text, 1, 3) = "O/B" Then
                    StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " &
                                            txtdestination_province.Text & " " & txtdest_receive_country.Text &
                                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                            & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                            " ON BEHALF OF " & txtob_dest_address.Text & IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "")

                ElseIf Mid(txtdest_remark1.Text, 2, 1) <> "/" Then
                    StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " & txtdestination_province.Text _
                                        & " " & txtdest_receive_country.Text &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                        & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                        IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "")

                ElseIf Mid(txtdest_remark1.Text, 1, 5) <> "O/TOB" Then
                    StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " & txtdestination_province.Text _
                                    & " " & txtdest_receive_country.Text & " " & txtob_dest_address.Text &
                                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                    & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                    IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "")

                ElseIf Mid(txtdest_remark1.Text, 1, 5) <> "O/TCO" Then
                    StrtxtRerutn = txtob_dest_address.Text & " " & txtdestination_company.Text & " " & txtdestination_address.Text & " " & txtdestination_province.Text _
                                    & " " & txtdest_receive_country.Text &
                                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                    & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                    IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "")

                End If
            Case "FORM44", "FORM44_44", "FORM44_02"
                If Mid(txtdest_remark1.Text, 1, 3) = "C/O" Then
                    StrtxtRerutn = txtob_dest_address.Text & " CARE OF " & txtdestination_company.Text & " " & txtdestination_address.Text _
                                        & " " & txtdestination_province.Text & " " & txtdest_receive_country.Text &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID:  " & txtdestination_taxid.Text, "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                        IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "")

                ElseIf Mid(txtdest_remark1.Text, 1, 3) = "O/B" Then
                    StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " &
                                            txtdestination_province.Text & " " & txtdest_receive_country.Text &
                                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                            & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                            " ON BEHALF OF " & txtob_dest_address.Text & IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "")

                ElseIf Mid(txtdest_remark1.Text, 2, 1) <> "/" Then
                    StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " & txtdestination_province.Text _
                                        & " " & txtdest_receive_country.Text &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                        & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                        IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "")

                ElseIf Mid(txtdest_remark1.Text, 1, 5) <> "O/TOB" Then
                    StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " & txtdestination_province.Text _
                                    & " " & txtdest_receive_country.Text & " " & txtob_dest_address.Text &
                                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                    & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                    IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "")

                ElseIf Mid(txtdest_remark1.Text, 1, 5) <> "O/TCO" Then
                    StrtxtRerutn = txtob_dest_address.Text & " " & txtdestination_company.Text & " " & txtdestination_address.Text & " " & txtdestination_province.Text _
                                    & " " & txtdest_receive_country.Text &
                                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                    & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                    IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "")

                End If
            Case "FORM4", "FORM44_4", "FORM44_01", "FORMAHK"
                If Mid(txtdest_remark1.Text, 1, 3) = "C/O" Then
                    StrtxtRerutn = txtob_dest_address.Text & " CARE OF " & txtdestination_company.Text & " " & txtdestination_address.Text _
                                        & " " & txtdestination_province.Text & " " & txtdest_receive_country.Text &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID:  " & txtdestination_taxid.Text, "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                        IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "")

                ElseIf Mid(txtdest_remark1.Text, 1, 3) = "O/B" Then
                    StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " &
                                            txtdestination_province.Text & " " & txtdest_receive_country.Text &
                                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                            & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                            " ON BEHALF OF " & txtob_dest_address.Text & IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "")

                ElseIf Mid(txtdest_remark1.Text, 2, 1) <> "/" Then
                    StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " & txtdestination_province.Text _
                                        & " " & txtdest_receive_country.Text &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                        & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                        IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "")

                ElseIf Mid(txtdest_remark1.Text, 1, 5) <> "O/TOB" Then
                    StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " & txtdestination_province.Text _
                                    & " " & txtdest_receive_country.Text & " " & txtob_dest_address.Text &
                                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                    & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                    IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "")

                ElseIf Mid(txtdest_remark1.Text, 1, 5) <> "O/TCO" Then
                    StrtxtRerutn = txtob_dest_address.Text & " " & txtdestination_company.Text & " " & txtdestination_address.Text & " " & txtdestination_province.Text _
                                    & " " & txtdest_receive_country.Text &
                                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                    & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                    IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "")

                End If
            Case "FORM4_91", "FORM4_911"
                Select Case CommonUtility.Get_StringValue(txtCheck_StatusWeb.Text)
                    Case Is <> ""
                        If Mid(txtdest_remark1.Text, 1, 3) = "C/O" Then
                            StrtxtRerutn = txtob_dest_address.Text & " CARE OF " & txtdestination_company.Text & " " & txtdestination_address.Text _
                                                & " " & txtdestination_province.Text & " " & txtdest_receive_country.Text &
                                                IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                                IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") &
                                                IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID:  " & txtdestination_taxid.Text, "") &
                                                IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                                IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "")

                        ElseIf Mid(txtdest_remark1.Text, 1, 3) = "O/B" Then
                            StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " &
                                                    txtdestination_province.Text & " " & txtdest_receive_country.Text &
                                                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                                    & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                                    " ON BEHALF OF " & txtob_dest_address.Text & IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "")

                        ElseIf Mid(txtdest_remark1.Text, 2, 1) <> "/" Then
                            StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " & txtdestination_province.Text _
                                                & " " & txtdest_receive_country.Text &
                                                IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                                IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                                & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                                IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                                IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "")

                        ElseIf Mid(txtdest_remark1.Text, 1, 5) <> "O/TOB" Then
                            StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " & txtdestination_province.Text _
                                            & " " & txtdest_receive_country.Text & " " & txtob_dest_address.Text &
                                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                            & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                            IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "")

                        ElseIf Mid(txtdest_remark1.Text, 1, 5) <> "O/TCO" Then
                            StrtxtRerutn = txtob_dest_address.Text & " " & txtdestination_company.Text & " " & txtdestination_address.Text & " " & txtdestination_province.Text _
                                            & " " & txtdest_receive_country.Text &
                                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                            & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                            IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "")

                        End If
                    Case Is = "" '�к����
                        If Mid(txtdest_remark1.Text, 1, 3) = "C/O" Then
                            StrtxtRerutn = txtob_dest_address.Text & " CARE OF " & txtdestination_company.Text & " " & txtdestination_address.Text _
                                                & " " & txtdestination_province.Text & " " & txtdest_receive_country.Text &
                                                IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                                IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") &
                                                IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID:  " & txtdestination_taxid.Text, "") &
                                                IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdeclare_Remark.Text)) <> "", " E-mail: " & txtdeclare_Remark.Text, "") &
                                                IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "")

                        ElseIf Mid(txtdest_remark1.Text, 1, 3) = "O/B" Then
                            StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " &
                                                    txtdestination_province.Text & " " & txtdest_receive_country.Text &
                                                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                                    & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdeclare_Remark.Text)) <> "", " E-mail: " & txtdeclare_Remark.Text, "") &
                                                    " ON BEHALF OF " & txtob_dest_address.Text & IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "")

                        ElseIf Mid(txtdest_remark1.Text, 2, 1) <> "/" Then
                            StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " & txtdestination_province.Text _
                                                & " " & txtdest_receive_country.Text &
                                                IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                                IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                                & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                                IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdeclare_Remark.Text)) <> "", " E-mail: " & txtdeclare_Remark.Text, "") &
                                                IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "")

                        ElseIf Mid(txtdest_remark1.Text, 1, 5) <> "O/TOB" Then
                            StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " & txtdestination_province.Text _
                                            & " " & txtdest_receive_country.Text & " " & txtob_dest_address.Text &
                                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                            & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdeclare_Remark.Text)) <> "", " E-mail: " & txtdeclare_Remark.Text, "") &
                                            IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "")

                        ElseIf Mid(txtdest_remark1.Text, 1, 5) <> "O/TCO" Then
                            StrtxtRerutn = txtob_dest_address.Text & " " & txtdestination_company.Text & " " & txtdestination_address.Text & " " & txtdestination_province.Text _
                                            & " " & txtdest_receive_country.Text &
                                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                            & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdeclare_Remark.Text)) <> "", " E-mail: " & txtdeclare_Remark.Text, "") &
                                            IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "")

                        End If
                End Select
            Case "FORM4_9"
                Select Case CommonUtility.Get_StringValue(txtCheck_StatusWeb.Text)
                    Case Is <> ""
                        If Mid(txtdest_remark1.Text, 1, 3) = "C/O" Then
                            StrtxtRerutn = txtob_dest_address.Text & " CARE OF " & txtdestination_company.Text & " " & txtdestination_address.Text _
                                                & " " & txtdestination_province.Text & " " & txtdest_receive_country.Text &
                                                IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                                IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") &
                                                IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID:  " & txtdestination_taxid.Text, "") &
                                                IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                                IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "")

                        ElseIf Mid(txtdest_remark1.Text, 1, 3) = "O/B" Then
                            StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " &
                                                    txtdestination_province.Text & " " & txtdest_receive_country.Text &
                                                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                                    & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                                    " ON BEHALF OF " & txtob_dest_address.Text & IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "")

                        ElseIf Mid(txtdest_remark1.Text, 2, 1) <> "/" Then
                            StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " & txtdestination_province.Text _
                                                & " " & txtdest_receive_country.Text &
                                                IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                                IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                                & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                                IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                                IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "")

                        ElseIf Mid(txtdest_remark1.Text, 1, 5) <> "O/TOB" Then
                            StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " & txtdestination_province.Text _
                                            & " " & txtdest_receive_country.Text & " " & txtob_dest_address.Text &
                                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                            & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                            IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "")

                        ElseIf Mid(txtdest_remark1.Text, 1, 5) <> "O/TCO" Then
                            StrtxtRerutn = txtob_dest_address.Text & " " & txtdestination_company.Text & " " & txtdestination_address.Text & " " & txtdestination_province.Text _
                                            & " " & txtdest_receive_country.Text &
                                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                            & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                            IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "")

                        End If
                    Case Is = "" '�к����
                        If Mid(txtdest_remark1.Text, 1, 3) = "C/O" Then
                            StrtxtRerutn = txtob_dest_address.Text & " CARE OF " & txtdestination_company.Text & " " & txtdestination_address.Text _
                                                & " " & txtdestination_province.Text & " " & txtdest_receive_country.Text &
                                                IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                                IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") &
                                                IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID:  " & txtdestination_taxid.Text, "") &
                                                IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdeclare_Remark.Text)) <> "", " E-mail: " & txtdeclare_Remark.Text, "") &
                                                IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "")

                        ElseIf Mid(txtdest_remark1.Text, 1, 3) = "O/B" Then
                            StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " &
                                                    txtdestination_province.Text & " " & txtdest_receive_country.Text &
                                                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                                    & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdeclare_Remark.Text)) <> "", " E-mail: " & txtdeclare_Remark.Text, "") &
                                                    " ON BEHALF OF " & txtob_dest_address.Text & IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "")

                        ElseIf Mid(txtdest_remark1.Text, 2, 1) <> "/" Then
                            StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " & txtdestination_province.Text _
                                                & " " & txtdest_receive_country.Text &
                                                IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                                IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                                & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                                IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdeclare_Remark.Text)) <> "", " E-mail: " & txtdeclare_Remark.Text, "") &
                                                IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "")

                        ElseIf Mid(txtdest_remark1.Text, 1, 5) <> "O/TOB" Then
                            StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " & txtdestination_province.Text _
                                            & " " & txtdest_receive_country.Text & " " & txtob_dest_address.Text &
                                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                            & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdeclare_Remark.Text)) <> "", " E-mail: " & txtdeclare_Remark.Text, "") &
                                            IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "")

                        ElseIf Mid(txtdest_remark1.Text, 1, 5) <> "O/TCO" Then
                            StrtxtRerutn = txtob_dest_address.Text & " " & txtdestination_company.Text & " " & txtdestination_address.Text & " " & txtdestination_province.Text _
                                            & " " & txtdest_receive_country.Text &
                                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                            & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdeclare_Remark.Text)) <> "", " E-mail: " & txtdeclare_Remark.Text, "") &
                                            IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "")

                        End If
                End Select
            Case "FORM4_8", "FORM4_81"
                If Mid(txtdest_remark1.Text, 1, 3) = "C/O" Then
                    StrtxtRerutn = txtob_dest_address.Text & " CARE OF " & txtdestination_company.Text & " " & txtdestination_address.Text _
                                        & " " & txtdestination_province.Text & " " & txtdest_receive_country.Text &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID:  " & txtdestination_taxid.Text, "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                        IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "")

                ElseIf Mid(txtdest_remark1.Text, 1, 3) = "O/B" Then
                    StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " &
                                            txtdestination_province.Text & " " & txtdest_receive_country.Text &
                                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                            & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                            " ON BEHALF OF " & txtob_dest_address.Text & IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "")

                ElseIf Mid(txtdest_remark1.Text, 2, 1) <> "/" Then
                    StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " & txtdestination_province.Text _
                                        & " " & txtdest_receive_country.Text &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                        & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                        IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "")

                ElseIf Mid(txtdest_remark1.Text, 1, 5) <> "O/TOB" Then
                    StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " & txtdestination_province.Text _
                                    & " " & txtdest_receive_country.Text & " " & txtob_dest_address.Text &
                                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                    & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                    IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "")

                ElseIf Mid(txtdest_remark1.Text, 1, 5) <> "O/TCO" Then
                    StrtxtRerutn = txtob_dest_address.Text & " " & txtdestination_company.Text & " " & txtdestination_address.Text & " " & txtdestination_province.Text _
                                    & " " & txtdest_receive_country.Text &
                                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                    & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                    IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "")

                End If
            Case "FORM4_6", "FORM4_61"
                If Mid(txtdest_remark1.Text, 1, 3) = "C/O" Then
                    StrtxtRerutn = txtob_dest_address.Text & " CARE OF " & txtdestination_company.Text & " " & txtdestination_address.Text _
                                        & " " & txtdestination_province.Text & " " & txtdest_receive_country.Text &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID:  " & txtdestination_taxid.Text, "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                        IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "")

                ElseIf Mid(txtdest_remark1.Text, 1, 3) = "O/B" Then
                    StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " &
                                            txtdestination_province.Text & " " & txtdest_receive_country.Text &
                                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                            & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                            " ON BEHALF OF " & txtob_dest_address.Text & IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "")

                ElseIf Mid(txtdest_remark1.Text, 2, 1) <> "/" Then
                    StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " & txtdestination_province.Text _
                                        & " " & txtdest_receive_country.Text &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                        & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                        IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "")

                ElseIf Mid(txtdest_remark1.Text, 1, 5) <> "O/TOB" Then
                    StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " & txtdestination_province.Text _
                                    & " " & txtdest_receive_country.Text &
                                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                    & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                    IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "") & " " & txtob_dest_address.Text

                ElseIf Mid(txtdest_remark1.Text, 1, 5) <> "O/TCO" Then
                    StrtxtRerutn = txtob_dest_address.Text & " " & txtdestination_company.Text & " " & txtdestination_address.Text & " " & txtdestination_province.Text _
                                    & " " & txtdest_receive_country.Text &
                                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                    & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                    IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "")

                End If
            Case "FORM4_5"
                If Mid(txtdest_remark1.Text, 1, 3) = "C/O" Then
                    StrtxtRerutn = txtob_dest_address.Text & " CARE OF " & txtdestination_company.Text & " " & txtdestination_address.Text _
                                        & " " & txtdestination_province.Text & " " & txtdest_receive_country.Text &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID:  " & txtdestination_taxid.Text, "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                        IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "")

                ElseIf Mid(txtdest_remark1.Text, 1, 3) = "O/B" Then
                    StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " &
                                            txtdestination_province.Text & " " & txtdest_receive_country.Text &
                                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                            & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                            " ON BEHALF OF " & txtob_dest_address.Text & IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "")

                ElseIf Mid(txtdest_remark1.Text, 2, 1) <> "/" Then
                    StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " & txtdestination_province.Text _
                                        & " " & txtdest_receive_country.Text &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                        & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                        IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "")

                ElseIf Mid(txtdest_remark1.Text, 1, 5) = "O/TOB" Then
                    StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " & txtdestination_province.Text _
                                    & " " & txtdest_receive_country.Text &
                                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                    & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                    IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "") & " " & txtob_dest_address.Text

                ElseIf Mid(txtdest_remark1.Text, 1, 5) = "O/TCO" Then
                    StrtxtRerutn = txtob_dest_address.Text & " " & txtdestination_company.Text & " " & txtdestination_address.Text & " " & txtdestination_province.Text _
                                    & " " & txtdest_receive_country.Text &
                                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                    & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                    IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "")

                End If
            Case "FORM4_4"
                If Mid(txtdest_remark1.Text, 1, 3) = "C/O" Then
                    StrtxtRerutn = txtob_dest_address.Text & " CARE OF " & txtdestination_company.Text & " " & txtdestination_address.Text _
                                        & " " & txtdestination_province.Text & " " & txtdest_receive_country.Text &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID:  " & txtdestination_taxid.Text, "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                        IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "")

                ElseIf Mid(txtdest_remark1.Text, 1, 3) = "O/B" Then
                    StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " &
                                            txtdestination_province.Text & " " & txtdest_receive_country.Text &
                                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                            & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                            " ON BEHALF OF " & txtob_dest_address.Text & IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "")

                ElseIf Mid(txtdest_remark1.Text, 2, 1) <> "/" Then
                    StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " & txtdestination_province.Text _
                                        & " " & txtdest_receive_country.Text &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                        & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                        IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "")

                ElseIf Mid(txtdest_remark1.Text, 1, 5) <> "O/TOB" Then
                    StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " & txtdestination_province.Text _
                                    & " " & txtdest_receive_country.Text & " " & txtob_dest_address.Text &
                                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                    & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                    IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "")

                ElseIf Mid(txtdest_remark1.Text, 1, 5) <> "O/TCO" Then
                    StrtxtRerutn = txtob_dest_address.Text & " " & txtdestination_company.Text & " " & txtdestination_address.Text & " " & txtdestination_province.Text _
                                    & " " & txtdest_receive_country.Text &
                                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                    & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                    IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "")

                End If
            Case "FORM4_2", "FORME_01"
                StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " & txtdestination_province.Text _
                                & " " & txtdest_receive_country.Text &
                                IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "")

            Case "FORM4_1", "FORM44_41"
                If Mid(txtdest_remark1.Text, 1, 3) = "C/O" Then
                    StrtxtRerutn = txtob_dest_address.Text & " CARE OF " & txtdestination_company.Text & " " & txtdestination_address.Text _
                                        & " " & txtdestination_province.Text & " " & txtdest_receive_country.Text &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID:  " & txtdestination_taxid.Text, "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                        IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "")

                ElseIf Mid(txtdest_remark1.Text, 1, 3) = "O/B" Then
                    StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " &
                                            txtdestination_province.Text & " " & txtdest_receive_country.Text &
                                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                            & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                            " ON BEHALF OF " & txtob_dest_address.Text & IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "")

                ElseIf Mid(txtdest_remark1.Text, 2, 1) <> "/" Then
                    StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " & txtdestination_province.Text _
                                        & " " & txtdest_receive_country.Text &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                        & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                        IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "")

                ElseIf Mid(txtdest_remark1.Text, 1, 5) <> "O/TOB" Then
                    StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " & txtdestination_province.Text _
                                    & " " & txtdest_receive_country.Text & " " & txtob_dest_address.Text &
                                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                    & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                    IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "")

                ElseIf Mid(txtdest_remark1.Text, 1, 5) <> "O/TCO" Then
                    StrtxtRerutn = txtob_dest_address.Text & " " & txtdestination_company.Text & " " & txtdestination_address.Text & " " & txtdestination_province.Text _
                                    & " " & txtdest_receive_country.Text &
                                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                    & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                    IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "")

                End If
            Case "FORM3_1"
                StrtxtRerutn = str_03 & CommonUtility.Get_StringValue(txtByCom_CH03.Text)
            Case "FORM3"
                If Mid(txtdest_remark1.Text, 1, 3) = "C/O" Then
                    StrtxtRerutn = str_02 & txtob_dest_address.Text & IIf(Check_NULLALL(txtNewEmail_ch02.Text) <> "", " E-mail: " & txtNewEmail_ch02.Text, "") & " CARE OF " & txtdestination_company.Text & " " & txtdestination_address.Text _
                                        & " " & txtdestination_province.Text & " " & txtdest_receive_country.Text &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID:  " & txtdestination_taxid.Text, "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                        IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "")

                ElseIf Mid(txtdest_remark1.Text, 1, 3) = "O/B" Then
                    StrtxtRerutn = str_02 & txtdestination_company.Text & " " & txtdestination_address.Text & " " &
                                            txtdestination_province.Text & " " & txtdest_receive_country.Text &
                                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                            & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                            " ON BEHALF OF " & txtob_dest_address.Text & IIf(Check_NULLALL(txtNewEmail_ch02.Text) <> "", " E-mail: " & txtNewEmail_ch02.Text, "") & IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "")

                ElseIf Mid(txtdest_remark1.Text, 2, 1) <> "/" Then
                    StrtxtRerutn = str_02 & txtdestination_company.Text & " " & txtdestination_address.Text & " " & txtdestination_province.Text _
                                        & " " & txtdest_receive_country.Text &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                        & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                        IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "")

                End If
            Case "FORM2"
                Select Case Check_RFC_Tax(CommonUtility.Get_StringValue(txtdestination_taxid.Text))
                    Case True 'rfc
                        If Mid(txtdest_remark1.Text, 1, 3) = "C/O" Then
                            StrtxtRerutn = txtob_dest_address.Text & IIf(Check_NULLALL(txtNewEmail_ch02.Text) <> "", " E-mail: " & txtNewEmail_ch02.Text, "") & " CARE OF " & txtdestination_company.Text & " " & txtdestination_address.Text _
                                                & " " & txtdestination_province.Text & " " & txtdest_receive_country.Text &
                                                IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                                IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") &
                                                IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " " & txtdestination_taxid.Text, "") &
                                                IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                                IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "")

                        ElseIf Mid(txtdest_remark1.Text, 1, 3) = "O/B" Then
                            StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " &
                                                    txtdestination_province.Text & " " & txtdest_receive_country.Text &
                                                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                                    & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " " & txtdestination_taxid.Text, "") &
                                                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                                    " ON BEHALF OF " & txtob_dest_address.Text & IIf(Check_NULLALL(txtNewEmail_ch02.Text) <> "", " E-mail: " & txtNewEmail_ch02.Text, "") & IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "")

                        ElseIf Mid(txtdest_remark1.Text, 2, 1) <> "/" Then
                            StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " & txtdestination_province.Text _
                                                & " " & txtdest_receive_country.Text &
                                                IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                                IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                                & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " " & txtdestination_taxid.Text, "") &
                                                IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                                IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "")

                        End If
                    Case False '����� rfc
                        If Mid(txtdest_remark1.Text, 1, 3) = "C/O" Then
                            StrtxtRerutn = txtob_dest_address.Text & IIf(Check_NULLALL(txtNewEmail_ch02.Text) <> "", " E-mail: " & txtNewEmail_ch02.Text, "") & " CARE OF " & txtdestination_company.Text & " " & txtdestination_address.Text _
                                                & " " & txtdestination_province.Text & " " & txtdest_receive_country.Text &
                                                IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                                IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") &
                                                IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                                IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                                IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "")

                        ElseIf Mid(txtdest_remark1.Text, 1, 3) = "O/B" Then
                            StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " &
                                                    txtdestination_province.Text & " " & txtdest_receive_country.Text &
                                                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                                    & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                                    IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                                    " ON BEHALF OF " & txtob_dest_address.Text & IIf(Check_NULLALL(txtNewEmail_ch02.Text) <> "", " E-mail: " & txtNewEmail_ch02.Text, "") & IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "")

                        ElseIf Mid(txtdest_remark1.Text, 2, 1) <> "/" Then
                            StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " & txtdestination_province.Text _
                                                & " " & txtdest_receive_country.Text &
                                                IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                                IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                                & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                                IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                                IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "")

                        End If
                End Select
            Case "FORM2_3"
                If Mid(txtdest_remark1.Text, 1, 3) = "C/O" Then
                    StrtxtRerutn = txtob_dest_address.Text & IIf(Check_NULLALL(txtNewEmail_ch02.Text) <> "", " E-mail: " & txtNewEmail_ch02.Text, "") & " CARE OF " & txtdestination_company.Text & " " & txtdestination_address.Text _
                                        & " " & txtdestination_province.Text & " " & txtdest_receive_country.Text &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID:  " & txtdestination_taxid.Text, "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                        IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "")

                ElseIf Mid(txtdest_remark1.Text, 1, 3) = "O/B" Then
                    StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " &
                                            txtdestination_province.Text & " " & txtdest_receive_country.Text &
                                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                            & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                            " ON BEHALF OF " & txtob_dest_address.Text & IIf(Check_NULLALL(txtNewEmail_ch02.Text) <> "", " E-mail: " & txtNewEmail_ch02.Text, "") & IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "")

                ElseIf Mid(txtdest_remark1.Text, 2, 1) <> "/" Then
                    StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " & txtdestination_province.Text _
                                        & " " & txtdest_receive_country.Text &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                        & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                        IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "")

                End If
            Case "FORM2_2"
                If Mid(txtdest_remark1.Text, 1, 3) = "C/O" Then
                    StrtxtRerutn = txtob_dest_address.Text & IIf(Check_NULLALL(txtNewEmail_ch02.Text) <> "", " E-mail: " & txtNewEmail_ch02.Text, "") & " CARE OF " & txtdestination_company.Text & " " & txtdestination_address.Text _
                                        & " " & txtdestination_province.Text & " " & txtdest_receive_country.Text &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID:  " & txtdestination_taxid.Text, "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                        IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "")

                ElseIf Mid(txtdest_remark1.Text, 1, 3) = "O/B" Then
                    StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " &
                                            txtdestination_province.Text & " " & txtdest_receive_country.Text &
                                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                            & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                            " ON BEHALF OF " & txtob_dest_address.Text & IIf(Check_NULLALL(txtNewEmail_ch02.Text) <> "", " E-mail: " & txtNewEmail_ch02.Text, "") & IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "")

                ElseIf Mid(txtdest_remark1.Text, 2, 1) <> "/" Then
                    StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " & txtdestination_province.Text _
                                        & " " & txtdest_receive_country.Text &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                        & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                        IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "")

                End If

                ''ByTine 11-12-2559
                ''��������� CO���� / Co ���
            Case "FORM2_5"
                If Mid(txtdest_remark1.Text, 1, 3) = "C/O" Then
                    StrtxtRerutn = txtob_dest_address.Text & IIf(Check_NULLALL(txtNewEmail_ch02.Text) <> "", " E-mail: " & txtNewEmail_ch02.Text, "") & " CARE OF " & txtdestination_company.Text & " " & txtdestination_address.Text _
                                        & " " & txtdestination_province.Text & " " & txtdest_receive_country.Text &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID:  " & txtdestination_taxid.Text, "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                        IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "")

                ElseIf Mid(txtdest_remark1.Text, 1, 3) = "O/B" Then
                    StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " &
                                            txtdestination_province.Text & " " & txtdest_receive_country.Text &
                                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                            & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                            " ON BEHALF OF " & txtob_dest_address.Text & IIf(Check_NULLALL(txtNewEmail_ch02.Text) <> "", " E-mail: " & txtNewEmail_ch02.Text, "") & IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "")

                ElseIf Mid(txtdest_remark1.Text, 2, 1) <> "/" Then
                    StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " & txtdestination_province.Text _
                                        & " " & txtdest_receive_country.Text &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                        & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                        IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "")

                End If

                ''ByTine 11-12-2559
                ''��������� CO���� / Co ���
            Case "FORM2_6"
                If Mid(txtdest_remark1.Text, 1, 3) = "C/O" Then
                    StrtxtRerutn = txtob_dest_address.Text & IIf(Check_NULLALL(txtNewEmail_ch02.Text) <> "", " E-mail: " & txtNewEmail_ch02.Text, "") & " CARE OF " & txtdestination_company.Text & " " & txtdestination_address.Text _
                                        & " " & txtdestination_province.Text & " " & txtdest_receive_country.Text &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID:  " & txtdestination_taxid.Text, "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                        IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "")

                ElseIf Mid(txtdest_remark1.Text, 1, 3) = "O/B" Then
                    StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " &
                                            txtdestination_province.Text & " " & txtdest_receive_country.Text &
                                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                            & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                            " ON BEHALF OF " & txtob_dest_address.Text & IIf(Check_NULLALL(txtNewEmail_ch02.Text) <> "", " E-mail: " & txtNewEmail_ch02.Text, "") & IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "")

                ElseIf Mid(txtdest_remark1.Text, 2, 1) <> "/" Then
                    StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " & txtdestination_province.Text _
                                        & " " & txtdest_receive_country.Text &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                        & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                        IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "")

                End If

            Case "FORMRUSSIA"
                If Mid(txtdest_remark1.Text, 1, 3) = "C/O" Then
                    StrtxtRerutn = txtob_dest_address.Text & IIf(Check_NULLALL(txtNewEmail_ch02.Text) <> "", " E-mail: " & txtNewEmail_ch02.Text, "") & " CARE OF " & txtdestination_company.Text & " " & txtdestination_address.Text _
                                        & " " & txtdestination_province.Text & " " & txtdest_receive_country.Text &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID:  " & txtdestination_taxid.Text, "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                        IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "")

                ElseIf Mid(txtdest_remark1.Text, 1, 3) = "O/B" Then
                    StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " &
                                            txtdestination_province.Text & " " & txtdest_receive_country.Text &
                                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                            & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                            " ON BEHALF OF " & txtob_dest_address.Text & IIf(Check_NULLALL(txtNewEmail_ch02.Text) <> "", " E-mail: " & txtNewEmail_ch02.Text, "") & IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "")

                ElseIf Mid(txtdest_remark1.Text, 2, 1) <> "/" Then
                    StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " & txtdestination_province.Text _
                                        & " " & txtdest_receive_country.Text &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                        & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                        IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "")

                End If
            Case "FORM1"
                If Mid(txtdest_remark1.Text, 1, 3) = "C/O" Then
                    StrtxtRerutn = txtob_dest_address.Text & IIf(Check_NULLALL(txtNewEmail_ch02.Text) <> "", " E-mail: " & txtNewEmail_ch02.Text, "") & " CARE OF " & txtdestination_company.Text & " " & txtdestination_address.Text _
                                        & " " & txtdestination_province.Text & " " & txtdest_receive_country.Text &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID:  " & txtdestination_taxid.Text, "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                        IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "")

                ElseIf Mid(txtdest_remark1.Text, 1, 3) = "O/B" Then
                    StrtxtRerutn = txtdestination_company.Text & vbNewLine & txtdestination_address.Text & " " &
                                            txtdestination_province.Text & " " & txtdest_receive_country.Text &
                                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                            & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                            " ON BEHALF OF " & txtob_dest_address.Text & IIf(Check_NULLALL(txtNewEmail_ch02.Text) <> "", " E-mail: " & txtNewEmail_ch02.Text, "") & IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "")

                ElseIf Mid(txtdest_remark1.Text, 2, 1) <> "/" Then
                    StrtxtRerutn = txtdestination_company.Text & vbNewLine & txtdestination_address.Text & " " & txtdestination_province.Text _
                                        & " " & txtdest_receive_country.Text &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                        & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                        IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "")

                End If
            Case "FORM1_4"
                If Mid(txtdest_remark1.Text, 1, 3) = "C/O" Then
                    StrtxtRerutn = txtob_dest_address.Text & IIf(Check_NULLALL(txtNewEmail_ch02.Text) <> "", " E-mail: " & txtNewEmail_ch02.Text, "") & " CARE OF " & txtdestination_company.Text & " " & txtdestination_address.Text _
                                        & " " & txtdestination_province.Text & " " & txtdest_receive_country.Text &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID:  " & txtdestination_taxid.Text, "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                        IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "")

                ElseIf Mid(txtdest_remark1.Text, 1, 3) = "O/B" Then
                    StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " &
                                            txtdestination_province.Text & " " & txtdest_receive_country.Text &
                                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                            & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                            " ON BEHALF OF " & txtob_dest_address.Text & IIf(Check_NULLALL(txtNewEmail_ch02.Text) <> "", " E-mail: " & txtNewEmail_ch02.Text, "") & IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "")

                ElseIf Mid(txtdest_remark1.Text, 2, 1) <> "/" Then
                    StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " & txtdestination_province.Text _
                                        & " " & txtdest_receive_country.Text &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                        & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                        IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "")

                End If
            Case "FORM1_3"
                If Mid(txtdest_remark1.Text, 1, 3) = "C/O" Then
                    StrtxtRerutn = txtob_dest_address.Text & IIf(Check_NULLALL(txtNewEmail_ch02.Text) <> "", " E-mail: " & txtNewEmail_ch02.Text, "") & " CARE OF " & txtdestination_company.Text & " " & txtdestination_address.Text _
                                        & " " & txtdestination_province.Text & " " & txtdest_receive_country.Text &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID:  " & txtdestination_taxid.Text, "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                        IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "")

                ElseIf Mid(txtdest_remark1.Text, 1, 3) = "O/B" Then
                    StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " &
                                            txtdestination_province.Text & " " & txtdest_receive_country.Text &
                                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                            & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                            " ON BEHALF OF " & txtob_dest_address.Text & IIf(Check_NULLALL(txtNewEmail_ch02.Text) <> "", " E-mail: " & txtNewEmail_ch02.Text, "") & IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "")

                ElseIf Mid(txtdest_remark1.Text, 2, 1) <> "/" Then
                    StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " & txtdestination_province.Text _
                                        & " " & txtdest_receive_country.Text &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                        & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                        IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "")

                End If
            Case "FORM1_2"
                If Mid(txtdest_remark1.Text, 1, 3) = "C/O" Then
                    StrtxtRerutn = txtob_dest_address.Text & IIf(Check_NULLALL(txtNewEmail_ch02.Text) <> "", " E-mail: " & txtNewEmail_ch02.Text, "") & " CARE OF " & txtdestination_company.Text & " " & txtdestination_address.Text _
                                        & " " & txtdestination_province.Text & " " & txtdest_receive_country.Text &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID:  " & txtdestination_taxid.Text, "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                        IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "")

                ElseIf Mid(txtdest_remark1.Text, 1, 3) = "O/B" Then
                    StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " &
                                            txtdestination_province.Text & " " & txtdest_receive_country.Text &
                                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                            & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                            " ON BEHALF OF " & txtob_dest_address.Text & IIf(Check_NULLALL(txtNewEmail_ch02.Text) <> "", " E-mail: " & txtNewEmail_ch02.Text, "") & IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "")

                ElseIf Mid(txtdest_remark1.Text, 2, 1) <> "/" Then
                    StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " & txtdestination_province.Text _
                                        & " " & txtdest_receive_country.Text &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                        & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                        IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "")

                End If
            Case "FORM1_1"
                If Mid(txtdest_remark1.Text, 1, 3) = "C/O" Then
                    StrtxtRerutn = txtob_dest_address.Text & IIf(Check_NULLALL(txtNewEmail_ch02.Text) <> "", " E-mail: " & txtNewEmail_ch02.Text, "") & " CARE OF " & txtdestination_company.Text & " " & txtdestination_address.Text _
                                        & " " & txtdestination_province.Text & " " & txtdest_receive_country.Text &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID:  " & txtdestination_taxid.Text, "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                        IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "")

                ElseIf Mid(txtdest_remark1.Text, 1, 3) = "O/B" Then
                    StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " &
                                            txtdestination_province.Text & " " & txtdest_receive_country.Text &
                                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                            & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                            IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                            " ON BEHALF OF " & txtob_dest_address.Text & IIf(Check_NULLALL(txtNewEmail_ch02.Text) <> "", " E-mail: " & txtNewEmail_ch02.Text, "") & IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "")

                ElseIf Mid(txtdest_remark1.Text, 2, 1) <> "/" Then
                    StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " & txtdestination_province.Text _
                                        & " " & txtdest_receive_country.Text &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                        & IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                        IIf(CommonUtility.Get_StringValue(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                        IIf(Check_NULLALL(txtplace_exibition.Text) <> "", " " & txtplace_exibition.Text, "")

                End If

        End Select

        Return StrtxtRerutn
    End Function
    '====================================
    'Check RFC
    Function Check_RFC_Tax(ByVal ValueTaxID As String) As Boolean
        If Mid(ValueTaxID, 1, 3) = "RFC" Or Mid(ValueTaxID, 1, 3) = "rfc" Then
            Return True
        Else
            Return False
        End If
    End Function
#End Region

    'by rut D att
    Dim TAttNetweightTotal, TAttUSDTotal As Decimal
    Private Sub Detail1_Format(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Detail1.Format
        C_num += 1
        txtNum_Product.Text = CStr(C_num) & "."
        '===================================================
        txtTARIFF_CODE_Temp.Text = Mid(txtTARIFF_CODE.Text, 1, 4) + "." + Mid(txtTARIFF_CODE.Text, 5, 2)
        txtNET_WEIGHT_unit_code2.Text = Format(txtNET_WEIGHT.Value, "#,##0.00##") + " " + txtunit_code2.Text
        txtFOB_AMT_Temp.Text = Format(txtFOB_AMT.Value, "#,##0.00##")

        '===================================================
        C_NET_WEIGHT += CDec(txtNET_WEIGHT.Text)
        C_Fob_Amt += CDec(txtFOB_AMT.Text)

        Select Case CommonUtility.Get_StringValue(txtform_type.Text)
            Case "FORM2", "FORM2_2", "FORM2_3", "FORM2_4", "FORM2_5", "FORM2_6", "FORM3", "FORM3_1"
                txtPRODUCT_NAME.Text = txtTemp_PRODUCT_NAME.Text
            Case Else
                txtPRODUCT_NAME.Text = txtTemp_PRODUCT_NAME.Text & " " & txtRover.Text
        End Select

        'by rut new D att
        TAttNetweightTotal += Check_Null(CommonUtility.Get_StringValue(txtAttNetweightTotal.Value))
        TAttUSDTotal += Check_Null(CommonUtility.Get_StringValue(txtAttUSDTotal.Value))
    End Sub
    Dim ii As Integer
    Private Sub PageHeader1_Format(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PageHeader1.Format
        '===============txtForm_Name_Temp===================================
        Select Case CommonUtility.Get_StringValue(txtform_type.Text)
            Case "FORM2_2"
                'txtForm_Name_Temp.Text = "�Ӣ�˹ѧ����Ѻ�ͧ��蹡��Դ�Թ����ɵ����͹�������Ҿ���û (��)"
                'txtForm_Name_Temp.Text = "�Ӣ�˹ѧ����Ѻ�ͧ��蹡��Դ�Թ��� �Թ����ɵ����͹�������Ҿ���û (��)"
                txtForm_Name_Temp.Text = "�Ӣ�˹ѧ����Ѻ�ͧ��蹡��Դ�Թ�������Ѻ�Թ��ҷ�����Ѻ�ǵ�㹡�ù�������Ҿ���û (��)"
            Case "FORM2_3"
                'txtForm_Name_Temp.Text = "�Ӣ�˹ѧ����Ѻ�ͧ��蹡��Դ�Թ����ɵ����͹�������Ҿ���û (�ѹ�ӻ���ѧ)"
                'txtForm_Name_Temp.Text = "�Ӣ�˹ѧ����Ѻ�ͧ��蹡��Դ�Թ��� �Թ����ɵ����͹�������Ҿ���û (�ѹ�ӻ���ѧ)"
                txtForm_Name_Temp.Text = "�Ӣ�˹ѧ����Ѻ�ͧ��蹡��Դ�Թ�������Ѻ�Թ��ҷ�����Ѻ�ǵ�㹡�ù�������Ҿ���û (�ѹ�ӻ���ѧ)"
            Case "FORM2_5"
                'txtForm_Name_Temp.Text = "�Ӣ�˹ѧ����Ѻ�ͧ��蹡��Դ�Թ����ɵ����͹�������Ҿ���û (���)"
                'txtForm_Name_Temp.Text = "�Ӣ�˹ѧ����Ѻ�ͧ��蹡��Դ�Թ��� �Թ����ɵ����͹�������Ҿ���û (���)"
                txtForm_Name_Temp.Text = "�Ӣ�˹ѧ����Ѻ�ͧ��蹡��Դ�Թ�������Ѻ�Թ��ҷ�����Ѻ�ǵ�㹡�ù�������Ҿ���û (���)"
            Case "FORM2_6"
                'txtForm_Name_Temp.Text = "�Ӣ�˹ѧ����Ѻ�ͧ��蹡��Դ�Թ����ɵ����͹�������Ҿ���û (����)"
                'txtForm_Name_Temp.Text = "�Ӣ�˹ѧ����Ѻ�ͧ��蹡��Դ�Թ��� �Թ����ɵ����͹�������Ҿ���û (����)"
                txtForm_Name_Temp.Text = "�Ӣ�˹ѧ����Ѻ�ͧ��蹡��Դ�Թ�������Ѻ�Թ��ҷ�����Ѻ�ǵ�㹡�ù�������Ҿ���û (����)"
            Case Else
                txtForm_Name_Temp.Text = "�Ӣ�˹ѧ����Ѻ�ͧ��蹡��Դ�Թ���Ẻ" & txtForm_Name.Text
        End Select
        Select Case CommonUtility.Get_StringValue(txtform_type.Text)
            Case "FORM2", "FORM2_2", "FORM2_3", "FORM2_4", "FORM2_5", "FORM2_6", "FORM3", "FORM3_1"
                txtL1.Visible = False
                txtL2.Visible = False
                txtL3.Visible = False
                txtL4.Visible = False
        End Select
        ii += 1
        '===============txtREFERENCE_CODE2_Temp===================================
        'txtREFERENCE_CODE2_Temp.Text = "I" & txtREFERENCE_CODE2.Text
        txtREFERENCE_CODE2_Temp.Text = txtREFERENCE_CODE2.Text

        '=====================================================
        txtTemp_request_person.Text = txtrequest_person.Text & "    ���.......������ӹҨ��зӡ��᷹�ԵԺؤ��"
        '============txtcard_id_Temp=txtcompany_name=====================================
        txtcard_id_Temp.Text = v1_Head_Checkcompany(txtform_type.Text)
        ''txtcard_id_Temp.Text = "�ӹҨ��Ţ���  " & txtcard_id.Text & "   �ͧ����ѷ/��ҧ/��ҹ  " & txtcompany_name.Text & _
        ''vbNewLine & "�Ţ��Шӵ�Ǽ�����������ҡ�   " & txtcompany_taxno.Text & _
        ''vbNewLine & "��������Ţ���  " & txtcompany_address.Text & " " & txtcompany_province.Text & " ����� " & txtcompany_country.Text & vbNewLine & _
        ''IIf(CommonUtility.Get_StringValue(txtcompany_phone.Text) <> "", "���Ѿ��  " & txtcompany_phone.Text, "���Ѿ��    -") & _
        ''IIf(CommonUtility.Get_StringValue(txtcompany_fax.Text) <> "", "   �����  " & txtcompany_fax.Text, "   �����    -")

        '================txtdestination_address_province_dest_receive_country==================================
        txtdestination_address_province_dest_receive_country.Text = v2_Head_Checkdestination(txtform_type.Text)


        '===============txtimport_country_Temp===================================
        txtimport_country_Temp.Text = "����Ȼ��·ҧ  " & txtimport_country.Text

        '==================txtship_by================================
        Select Case CommonUtility.Get_StringValue(txtship_by.Text)
            Case "0"
                txtship_by0.Text = "X"
            Case "1"
                txtship_by1.Text = "X"
            Case "2"
                txtship_by2.Text = "X"
            Case "3"
                txtship_by3.Text = "X"
            Case "4"
                txtship_by4.Text = "X"
        End Select
        '==================================================
        'check ��Ѥ��
        '੾�п���� CO ����� �Ѻ ��硫��
        Select Case CommonUtility.Get_StringValue(txtform_type.Text)
            Case "FORM2", "FORM3", "FORM3_1"
                Select Case check2_1request(txtcompany_taxno.Text)
                    Case True
                        txtCheckTax.Visible = True
                    Case False
                        txtCheckTax.Visible = False
                End Select
            Case Else
                txtCheckTax.Visible = False
        End Select

        '��꡶١ ͹��ѵ�
        If TextBox3.Value <> 0 Then
            Pic_ch4_par.Visible = True
        End If

    End Sub

    Private Sub PageFooter1_Format(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PageFooter1.Format
        If txtNumpageForm.Value = 1 Then 'check �����ʴ���˹���á˹������
            txtTotal.Text = "�ʹ���       GROSS WEIGHT = " & GrossTxt() 'txtSum_Gross_Weight.Text
        Else
            txtTotal.Text = ""
        End If

        txtTemp_sailing_date.Text = Format(Get_DateTime_rpt(txtsailing_date.Value), "dd/MM/yyy") 'Format(Get_DateTime_rpt(txtsailing_date.Value), "dd/MM/yyy")
        txtTemp_edi_date.Text = Format(Get_DateTime_rpt(txtedi_date.Value), "dd/MM/yyy")

        'Dim str_in As String = "�Ţ��� " & IIf(txtinvoice_no1.Text <> "", txtinvoice_no1.Text.Replace(vbCrLf, ""), "") & " " & _
        '                                IIf(txtinvoice_no2.Text <> "", txtinvoice_no2.Text.Replace(vbCrLf, ""), "")
        'mpReplace = tmpReplace.Replace(vbCrLf, "chr(13) + chr(10)")



        'Dim str_in As String = "�Ţ��� " & IIf(txtinvoice_no1.Text <> "", txtinvoice_no1.Text.Replace(vbCrLf, ""), "") & " " & _
        '                        IIf(txtinvoice_no2.Text <> "", txtinvoice_no2.Text.Replace(vbCrLf, ""), "") & " " & _
        '                        IIf(txtinvoice_no3.Text <> "", txtinvoice_no3.Text.Replace(vbCrLf, ""), "") & " " & _
        '                        IIf(txtinvoice_no4.Text <> "", txtinvoice_no4.Text.Replace(vbCrLf, ""), "") & " " & _
        '                        IIf(txtinvoice_no5.Text <> "", txtinvoice_no5.Text.Replace(vbCrLf, ""), "")
        'Dim str_inDate As String = "ŧ�ѹ��� " & IIf(txtinvoice_no1.Text <> "" And txtinvoice_date1.Text <> Nothing, Format(Get_DateTime_rpt(txtinvoice_date1.Text), "dd/MM/yyy"), "") & " " & _
        '                            IIf(txtinvoice_no2.Text <> "" And txtinvoice_date2.Text <> Nothing, Format(Get_DateTime_rpt(txtinvoice_date2.Text), "dd/MM/yyy"), "") & " " & _
        '                            IIf(txtinvoice_no3.Text <> "" And txtinvoice_date3.Text <> Nothing, Format(Get_DateTime_rpt(txtinvoice_date3.Text), "dd/MM/yyy"), "") & " " & _
        '                            IIf(txtinvoice_no4.Text <> "" And txtinvoice_date4.Text <> Nothing, Format(Get_DateTime_rpt(txtinvoice_date4.Text), "dd/MM/yyy"), "") & " " & _
        '                            IIf(txtinvoice_no5.Text <> "" And txtinvoice_date5.Text <> Nothing, Format(Get_DateTime_rpt(txtinvoice_date5.Text), "dd/MM/yyy"), "") & " "
        Dim str_in As String = ""
        str_in = "�Ţ��� " & IIf(CommonUtility.Get_StringValue(txtinvoice_no1.Text) <> "", CommonUtility.Get_StringValue(txtinvoice_no1.Text).Replace(vbCrLf, ""), "") & " " &
                                IIf(CommonUtility.Get_StringValue(txtinvoice_no2.Text) <> "", CommonUtility.Get_StringValue(txtinvoice_no2.Text).Replace(vbCrLf, ""), "") & " " &
                                IIf(CommonUtility.Get_StringValue(txtinvoice_no3.Text) <> "", CommonUtility.Get_StringValue(txtinvoice_no3.Text).Replace(vbCrLf, ""), "") & " " &
                                IIf(CommonUtility.Get_StringValue(txtinvoice_no4.Text) <> "", CommonUtility.Get_StringValue(txtinvoice_no4.Text).Replace(vbCrLf, ""), "") & " " &
                                IIf(CommonUtility.Get_StringValue(txtinvoice_no5.Text) <> "", CommonUtility.Get_StringValue(txtinvoice_no5.Text).Replace(vbCrLf, ""), "")
        Dim str_inDate As String = ""
        str_inDate = "ŧ�ѹ��� " & IIf(CommonUtility.Get_StringValue(txtinvoice_no1.Text) <> "" And txtinvoice_date1.Text <> Nothing, Format(Get_DateTime_rpt(txtinvoice_date1.Text), "dd/MM/yyy"), "") & " " &
                                    IIf(CommonUtility.Get_StringValue(txtinvoice_no2.Text) <> "" And txtinvoice_date2.Text <> Nothing, Format(Get_DateTime_rpt(txtinvoice_date2.Text), "dd/MM/yyy"), "") & " " &
                                    IIf(CommonUtility.Get_StringValue(txtinvoice_no3.Text) <> "" And txtinvoice_date3.Text <> Nothing, Format(Get_DateTime_rpt(txtinvoice_date3.Text), "dd/MM/yyy"), "") & " " &
                                    IIf(CommonUtility.Get_StringValue(txtinvoice_no4.Text) <> "" And txtinvoice_date4.Text <> Nothing, Format(Get_DateTime_rpt(txtinvoice_date4.Text), "dd/MM/yyy"), "") & " " &
                                    IIf(CommonUtility.Get_StringValue(txtinvoice_no5.Text) <> "" And txtinvoice_date5.Text <> Nothing, Format(Get_DateTime_rpt(txtinvoice_date5.Text), "dd/MM/yyy"), "") & " "

        txtInvoince_Date.Text = str_in & " " & str_inDate

        'txtSum_Net_Weight.Text = Format(CDec(txtNET_WEIGHTPage.Text), "#,##0.00##") & " " & txtunit_code2.Text
        'txtSum_Fob_Amt.Text = Format(CDec(txtSumFOB_AMTPage.Text), "#,##0.00##")
        'by rut d att
        Select Case CommonUtility.Get_StringValue(txtform_type.Value)
            Case Is = "FORM44_44", "FORM441_4", "FORM44_02"
                '�͡Ẻ���� �� att
                txtSum_Net_Weight.Text = Format(CDec(TAttNetweightTotal), "#,##0.00##") & " " & txtunit_code2.Text
                txtSum_Fob_Amt.Text = Format(CDec(TAttUSDTotal), "#,##0.00##")

            Case Else
                '�͡Ẻ���
                txtSum_Net_Weight.Text = Format(CDec(txtNET_WEIGHTPage.Text), "#,##0.00##") & " " & txtunit_code2.Text 'Format(CDec(txttotal_real_net_weight.Text), "#,##0.00##") & " " & txtunit_code2.Text
                txtSum_Fob_Amt.Text = Format(CDec(txtSumFOB_AMTPage.Text), "#,##0.00##") 'Format(CDec(txttotalSum_fob_amt.Text), "#,##0.00##")
        End Select
        '==============txtrequest_person_2====================================
        txtrequest_person_2.Text = "( " + txtrequest_person.Text + " )    ����Ѻ�ͺ" '"( " + txtrequest_person.Text + " )    ����Ѻ�ͺ"

        '===============txtbill_type===================================
        Select Case CommonUtility.Get_StringValue(txtbill_type.Text)
            Case "0"
                txtbill_type0.Text = "X"
            Case "1"
                txtbill_type1.Text = "X"
            Case "2"
                txtbill_type2.Text = "X"
            Case "3"
                txtbill_type3.Text = "X"
        End Select
        '==============txtfactory_Temp====================================
        '//txtfactory_Temp.Text = txtfactory.Text + " ��������� " + txtfactory_address.Text
        txtFactoryName.Text = txtfactory.Text
        txtfactory_Temp.Text = txtfactory_address.Text

        'by rut sign image 
        If Get_CheckDataBy_Auto(txtinvh_run_auto.Text) = True Then
            Select Case txtTemp_form_type.Text.ToUpper
                Case "FORM4_91", "FORM4_911", "FORM4_4" '੾���Թ��ҷ�����͡���� ��������ҹ��
                    If txtTemp_destination_country.Text.ToUpper = "AU" Then
                        If Get_Cardid_Num(txtcard_id.Text).Tables(0).Rows.Count > 0 Then '��������Ţ�ѵû�ЪҪ���� cardid
                            With Get_Cardid_Num(txtcard_id.Text).Tables(0).Rows(0)
                                'check �ѵá�͹ ����繺ѵú���ѷ
                                Select Case CommonUtility.Get_StringValue(.Item("card_type"))
                                    Case "C" '���ͧ�ҡ�ѵá������ � rfcard ��������Ţ�ѵû�ЪҪ�
                                        '����Ţ id �ͧ�ٻ�������
                                        CaseCheck_numimagesSign_Request(Req_IDSeal(txtinvh_run_auto.Text))
                                    Case Else
                                        'check �Ţ�ѵû�ЪҪ� �ͧ �ѵ���� id sealsign ����繤����ǡѹ������� ����繤����ǡѹ
                                        '��������� sealsign �͡ ������������������� sealsign
                                        If Req_CheckIDSealSign_perID(Req_IDSeal(txtinvh_run_auto.Text)) = .Item("AuthPersonID") Then
                                            '����Ţ id �ͧ�ٻ�������
                                            CaseCheck_numimagesSign_Request(Get_DataSealSignidby_cardid(.Item("AuthPersonID"), txtcompany_taxno.Text, .Item("company_branchNo")))
                                        End If
                                        ''����Ţ id �ͧ�ٻ�������
                                        'CaseCheck_numimagesSign_Request(Get_DataSealSignidby_cardid(.Item("AuthPersonID"), txtcompany_taxno.Text, .Item("company_branchNo")))
                                End Select

                            End With
                        End If
                    End If
                Case "FORM4_5", "FORM4_6", "FORM4_61" '੾���Թ��ҷ�����͡任���ȭ���� ��ҹ��
                    If txtTemp_destination_country.Text.ToUpper = "JP" Then
                        If Get_Cardid_Num(txtcard_id.Text).Tables(0).Rows.Count > 0 Then '��������Ţ�ѵû�ЪҪ���� cardid
                            With Get_Cardid_Num(txtcard_id.Text).Tables(0).Rows(0)
                                'check �ѵá�͹ ����繺ѵú���ѷ
                                Select Case CommonUtility.Get_StringValue(.Item("card_type"))
                                    Case "C" '���ͧ�ҡ�ѵá������ � rfcard ��������Ţ�ѵû�ЪҪ�
                                        '����Ţ id �ͧ�ٻ�������
                                        CaseCheck_numimagesSign_Request(Req_IDSeal(txtinvh_run_auto.Text))
                                    Case Else
                                        'check �Ţ�ѵû�ЪҪ� �ͧ �ѵ���� id sealsign ����繤����ǡѹ������� ����繤����ǡѹ
                                        '��������� sealsign �͡ ������������������� sealsign
                                        If Req_CheckIDSealSign_perID(Req_IDSeal(txtinvh_run_auto.Text)) = .Item("AuthPersonID") Then
                                            '����Ţ id �ͧ�ٻ�������
                                            CaseCheck_numimagesSign_Request(Get_DataSealSignidby_cardid(.Item("AuthPersonID"), txtcompany_taxno.Text, .Item("company_branchNo")))
                                        End If
                                        ''����Ţ id �ͧ�ٻ�������
                                        'CaseCheck_numimagesSign_Request(Get_DataSealSignidby_cardid(.Item("AuthPersonID"), txtcompany_taxno.Text, .Item("company_branchNo")))
                                End Select
                            End With
                        End If
                    End If

                    ''ByTine 17-12-2558 ������������ AK �� Seal Sign ����ء�����
                Case "FORM4_8", "FORM4_81", "FORMAHk"
                    If Get_Cardid_Num(txtcard_id.Text).Tables(0).Rows.Count > 0 Then '��������Ţ�ѵû�ЪҪ���� cardid
                        With Get_Cardid_Num(txtcard_id.Text).Tables(0).Rows(0)
                            'check �ѵá�͹ ����繺ѵú���ѷ
                            Select Case CommonUtility.Get_StringValue(.Item("card_type"))
                                Case "C" '���ͧ�ҡ�ѵá������ � rfcard ��������Ţ�ѵû�ЪҪ�
                                    '����Ţ id �ͧ�ٻ�������
                                    CaseCheck_numimagesSign_Request(Req_IDSeal(txtinvh_run_auto.Text))
                                Case Else
                                    'check �Ţ�ѵû�ЪҪ� �ͧ �ѵ���� id sealsign ����繤����ǡѹ������� ����繤����ǡѹ
                                    '��������� sealsign �͡ ������������������� sealsign
                                    If Req_CheckIDSealSign_perID(Req_IDSeal(txtinvh_run_auto.Text)) = .Item("AuthPersonID") Then
                                        '����Ţ id �ͧ�ٻ�������
                                        CaseCheck_numimagesSign_Request(Get_DataSealSignidby_cardid(.Item("AuthPersonID"), txtcompany_taxno.Text, .Item("company_branchNo")))
                                    End If
                                    ''����Ţ id �ͧ�ٻ�������
                                    'CaseCheck_numimagesSign_Request(Get_DataSealSignidby_cardid(.Item("AuthPersonID"), txtcompany_taxno.Text, .Item("company_branchNo")))
                            End Select
                        End With
                    End If
                Case Else

            End Select
        End If
        ' ''by rut sign image 
        ''If Get_CheckDataBy_Auto(txtinvh_run_auto.Text) = True Then
        ''    Select Case txtTemp_form_type.Text.ToUpper
        ''        Case "FORM4_91" '੾���Թ��ҷ�����͡���� ��������ҹ��
        ''            If txtTemp_destination_country.Text.ToUpper = "AU" Then
        ''                If Get_Cardid_Num(txtcard_id.Text).Tables(0).Rows.Count > 0 Then '��������Ţ�ѵû�ЪҪ���� cardid
        ''                    With Get_Cardid_Num(txtcard_id.Text).Tables(0).Rows(0)
        ''                        '����Ţ id �ͧ�ٻ�������
        ''                        CaseCheck_numimagesSign_Request(Get_DataSealSignidby_cardid(.Item("AuthPersonID"), txtcompany_taxno.Text, .Item("company_branchNo")))
        ''                    End With
        ''                End If
        ''            End If
        ''        Case "FORM4_5", "FORM4_6" '੾���Թ��ҷ�����͡任���ȭ���� ��ҹ��
        ''            If txtTemp_destination_country.Text.ToUpper = "JP" Then
        ''                If Get_Cardid_Num(txtcard_id.Text).Tables(0).Rows.Count > 0 Then '��������Ţ�ѵû�ЪҪ���� cardid
        ''                    With Get_Cardid_Num(txtcard_id.Text).Tables(0).Rows(0)
        ''                        '����Ţ id �ͧ�ٻ�������
        ''                        CaseCheck_numimagesSign_Request(Get_DataSealSignidby_cardid(.Item("AuthPersonID"), txtcompany_taxno.Text, .Item("company_branchNo")))
        ''                    End With
        ''                End If
        ''            End If
        ''        Case Else

        ''    End Select
        ''End If
    End Sub
#Region "Code sign image"
    'by rut sign image � report
    '�� reports_CardID,search_imageForm ����� Module_CallBathEng
    Sub CaseCheck_numimagesSign_Request(ByVal _numValue As String)
        Try
            Picture_SealAuthor.Visible = True

            'Picture_SealAuthor.SizeMode = SizeModes.Stretch
            'Picture_SealAuthor.Image = New Drawing.Bitmap(reports_CardID(_numValue.ToString))
            Dim u As String = reports_CardID(_numValue.ToString) '"http://edi.dft.go.th/" & "/Portals/0/Images_Sign/0405533000247/2013/SealPerson/3660400121998/3660400121998_New_��͹ѹ�� NK1.jpg"
            Dim client As System.Net.WebClient = New System.Net.WebClient()
            Dim data As System.IO.Stream = client.OpenRead(u)
            Dim reader As System.IO.StreamReader = New System.IO.StreamReader(data)

            Picture_SealAuthor.Image = System.Drawing.Image.FromStream(reader.BaseStream())
        Catch ex As Exception

        End Try
    End Sub
#End Region

    Private Sub GroupFooter1_Format(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GroupFooter1.Format

        '==================================================



    End Sub

    Function GrossTxt() As String
        Dim Str_GrossTxt As String
        Dim Dec_Gross As Decimal
        Dec_Gross = Format(CDec(Check_Null(CommonUtility.Get_StringValue(txtgross_weightH.Value))), "#,##0.00##")
        Str_GrossTxt = Format(Dec_Gross, "#,##0.00##") & " " & CommonUtility.Get_StringValue(txtg_unit_code.Value)

        Return Str_GrossTxt
    End Function

    'Public Sub New()

    '    ' This call is required by the Windows Form Designer.
    '    InitializeComponent()

    '    Me.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.A4
    '    'Me.PageSettings.PaperName = "A4"
    '    ' Add any initialization after the InitializeComponent() call.

    'End Sub

    Private Sub rpt3_ReEdi_A_PageStart(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PageStart

        Me.CurrentPage.DrawLine(0.31, 4.188, 0.31, 9) '��鹫����ش
        Me.CurrentPage.DrawLine(8, 4.188, 8, 9) '��鹢���ش

        Me.CurrentPage.DrawLine(4.625, 4.188, 4.625, 6.69)
        Me.CurrentPage.DrawLine(5.625, 4.188, 5.625, 6.69)
        Me.CurrentPage.DrawLine(7, 4.188, 7, 6.58)
    End Sub


    Private Sub ReportFooter1_Format(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ReportFooter1.Format

    End Sub


    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub
End Class
