Imports DataDynamics.ActiveReports
Imports DataDynamics.ActiveReports.Document

Imports Microsoft.ApplicationBlocks.Data
Imports System.Data.SqlClient
Imports System.Data
Imports System.Text
Imports DFT.Dotnetnuke.ClassLibrary

Public Class rpt3_ReEdi_AView3
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
        StrtxtRerutn1 = "อำนาจฯเลขที่  " & txtcard_id.Text & "   ของบริษัท/ห้าง/ร้าน  " & txtcompany_name.Text &
        vbNewLine & "เลขประจำตัวผู้เสียภาษีอากร   " & txtcompany_taxno.Text &
        " ตั้งอยู่เลขที่  " & txtcompany_address.Text & " " & txtcompany_province.Text & " ประเทศ " & txtcompany_country.Text & " " &
        IIf(ConvertToString(txtcompany_phone.Text) <> "", " โทรศัพท์  " & txtcompany_phone.Text, " โทรศัพท์    -") &
        IIf(ConvertToString(txtcompany_fax.Text) <> "", "   โทรสาร  " & txtcompany_fax.Text, "   โทรสาร    -") & vbNewLine

        'เฉพาะ กับคำขอช่องที่ 1
        'O/TOB มีฟอร์ม Form4_5, Form4_6, Form5_1
        'O/TCO ยังไม่มี

        Select Case ByCase_Form.ToUpper
            Case "FORM2_4"
                If Mid(txtdest_remark.Text, 1, 3) = "C/O" Then
                    StrtxtRerutn = txtob_address.Text & IIf(Check_NULLALL(txtNewEmail_ch01.Text) <> "", " E-mail: " & txtNewEmail_ch01.Text, "") & " CARE OF " & txtcompany_name.Text & " " & txtcompany_address.Text _
                                       & " " & txtcompany_province.Text & " " & txtcompany_country.Text &
                                       IIf(ConvertToString(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") &
                                       IIf(ConvertToString(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") &
                                       IIf(ConvertToString(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID:  " & txtcompany_taxno.Text, "") &
                                       IIf(ConvertToString(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")
                ElseIf Mid(txtdest_remark.Text, 1, 3) = "O/B" Then
                    StrtxtRerutn = IIf(ConvertToString(Check_NULLALL(txtcompany_email.Text)) <> "", "E-mail: " & txtcompany_email.Text & " ", "") &
                                        IIf(ConvertToString(Check_NULLALL(txtob_address.Text)) <> "", "ON BEHALF OF " & txtob_address.Text, "") & IIf(Check_NULLALL(txtNewEmail_ch01.Text) <> "", " E-mail: " & txtNewEmail_ch01.Text, "")
                ElseIf Mid(txtdest_remark.Text, 2, 1) <> "/" Then
                    StrtxtRerutn = ""
                    'StrtxtRerutn = txtcompany_name.Text & " " & txtcompany_address.Text & " " & txtcompany_province.Text _
                    '                    & " " & txtcompany_country.Text & IIf(ConvertToString(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") & _
                    '                    IIf(ConvertToString(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") _
                    '                    & IIf(ConvertToString(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID: " & txtcompany_taxno.Text, "") & _
                    '                    IIf(ConvertToString(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")

                End If
            Case "FORM4_3"
                If Mid(txtdest_remark.Text, 1, 3) = "C/O" Then
                    StrtxtRerutn = txtob_address.Text & IIf(Check_NULLALL(txtNewEmail_ch01.Text) <> "", " E-mail: " & txtNewEmail_ch01.Text, "") & " CARE OF " & txtcompany_name.Text & " " & txtcompany_address.Text _
                                       & " " & txtcompany_province.Text & " " & txtcompany_country.Text &
                                       IIf(ConvertToString(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") &
                                       IIf(ConvertToString(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") &
                                       IIf(ConvertToString(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID:  " & txtcompany_taxno.Text, "") &
                                       IIf(ConvertToString(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")
                ElseIf Mid(txtdest_remark.Text, 1, 3) = "O/B" Then
                    StrtxtRerutn = IIf(ConvertToString(Check_NULLALL(txtcompany_email.Text)) <> "", "E-mail: " & txtcompany_email.Text & " ", "") &
                                        IIf(ConvertToString(Check_NULLALL(txtob_address.Text)) <> "", "ON BEHALF OF " & txtob_address.Text, "") & IIf(Check_NULLALL(txtNewEmail_ch01.Text) <> "", " E-mail: " & txtNewEmail_ch01.Text, "")
                ElseIf Mid(txtdest_remark.Text, 2, 1) <> "/" Then
                    StrtxtRerutn = ""
                    'StrtxtRerutn = txtcompany_name.Text & " " & txtcompany_address.Text & " " & txtcompany_province.Text _
                    '                    & " " & txtcompany_country.Text & IIf(ConvertToString(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") & _
                    '                    IIf(ConvertToString(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") _
                    '                    & IIf(ConvertToString(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID: " & txtcompany_taxno.Text, "") & _
                    '                    IIf(ConvertToString(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")

                ElseIf Mid(txtdest_remark.Text, 1, 5) <> "O/TOB" Then
                    StrtxtRerutn = txtcompany_name.Text & " " & txtcompany_address.Text & " " & txtcompany_province.Text _
                                    & " " & txtcompany_country.Text & " " & txtob_address.Text &
                                    IIf(ConvertToString(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") &
                                    IIf(ConvertToString(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") _
                                    & IIf(ConvertToString(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID: " & txtcompany_taxno.Text, "") &
                                    IIf(ConvertToString(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")

                ElseIf Mid(txtdest_remark.Text, 1, 5) <> "O/TCO" Then
                    StrtxtRerutn = txtob_address.Text & txtcompany_name.Text & " " & txtcompany_address.Text & " " & txtcompany_province.Text _
                                    & " " & txtcompany_country.Text &
                                    IIf(ConvertToString(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") &
                                    IIf(ConvertToString(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") _
                                    & IIf(ConvertToString(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID: " & txtcompany_taxno.Text, "") &
                                    IIf(ConvertToString(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")

                End If
            Case "FORM9"
                If Mid(txtdest_remark.Text, 1, 3) = "C/O" Then
                    StrtxtRerutn = txtob_address.Text & IIf(Check_NULLALL(txtNewEmail_ch01.Text) <> "", " E-mail: " & txtNewEmail_ch01.Text, "") & " CARE OF " & txtcompany_name.Text & " " & txtcompany_address.Text _
                                       & " " & txtcompany_province.Text & " " & txtcompany_country.Text &
                                       IIf(ConvertToString(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") &
                                       IIf(ConvertToString(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") &
                                       IIf(ConvertToString(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID:  " & txtcompany_taxno.Text, "") &
                                       IIf(ConvertToString(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")
                ElseIf Mid(txtdest_remark.Text, 1, 3) = "O/B" Then
                    StrtxtRerutn = IIf(ConvertToString(Check_NULLALL(txtcompany_email.Text)) <> "", "E-mail: " & txtcompany_email.Text & " ", "") &
                                        IIf(ConvertToString(Check_NULLALL(txtob_address.Text)) <> "", "ON BEHALF OF " & txtob_address.Text, "") & IIf(Check_NULLALL(txtNewEmail_ch01.Text) <> "", " E-mail: " & txtNewEmail_ch01.Text, "")
                ElseIf Mid(txtdest_remark.Text, 2, 1) <> "/" Then
                    StrtxtRerutn = ""
                    'StrtxtRerutn = txtcompany_name.Text & " " & txtcompany_address.Text & " " & txtcompany_province.Text _
                    '                    & " " & txtcompany_country.Text & IIf(ConvertToString(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") & _
                    '                    IIf(ConvertToString(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") _
                    '                    & IIf(ConvertToString(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID: " & txtcompany_taxno.Text, "") & _
                    '                    IIf(ConvertToString(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")

                End If
            Case "FORM8"
                If Mid(txtdest_remark.Text, 1, 3) = "C/O" Then
                    StrtxtRerutn = txtob_address.Text & IIf(Check_NULLALL(txtNewEmail_ch01.Text) <> "", " E-mail: " & txtNewEmail_ch01.Text, "") & " CARE OF " & txtcompany_name.Text & " " & txtcompany_address.Text _
                                       & " " & txtcompany_province.Text & " " & txtcompany_country.Text &
                                       IIf(ConvertToString(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") &
                                       IIf(ConvertToString(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") &
                                       IIf(ConvertToString(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID:  " & txtcompany_taxno.Text, "") &
                                       IIf(ConvertToString(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")
                ElseIf Mid(txtdest_remark.Text, 1, 3) = "O/B" Then
                    StrtxtRerutn = IIf(ConvertToString(Check_NULLALL(txtcompany_email.Text)) <> "", "E-mail: " & txtcompany_email.Text & " ", "") &
                                        IIf(ConvertToString(Check_NULLALL(txtob_address.Text)) <> "", "ON BEHALF OF " & txtob_address.Text, "") & IIf(Check_NULLALL(txtNewEmail_ch01.Text) <> "", " E-mail: " & txtNewEmail_ch01.Text, "")
                ElseIf Mid(txtdest_remark.Text, 2, 1) <> "/" Then
                    StrtxtRerutn = ""
                    'StrtxtRerutn = txtcompany_name.Text & " " & txtcompany_address.Text & " " & txtcompany_province.Text _
                    '                    & " " & txtcompany_country.Text & IIf(ConvertToString(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") & _
                    '                    IIf(ConvertToString(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") _
                    '                    & IIf(ConvertToString(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID: " & txtcompany_taxno.Text, "") & _
                    '                    IIf(ConvertToString(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")

                End If
            Case "FORM7"
                If Mid(txtdest_remark.Text, 1, 3) = "C/O" Then
                    StrtxtRerutn = txtob_address.Text & IIf(Check_NULLALL(txtNewEmail_ch01.Text) <> "", " E-mail: " & txtNewEmail_ch01.Text, "") & " CARE OF " & txtcompany_name.Text & " " & txtcompany_address.Text _
                                       & " " & txtcompany_province.Text & " " & txtcompany_country.Text &
                                       IIf(ConvertToString(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") &
                                       IIf(ConvertToString(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") &
                                       IIf(ConvertToString(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID:  " & txtcompany_taxno.Text, "") &
                                       IIf(ConvertToString(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")
                ElseIf Mid(txtdest_remark.Text, 1, 3) = "O/B" Then
                    StrtxtRerutn = IIf(ConvertToString(Check_NULLALL(txtcompany_email.Text)) <> "", "E-mail: " & txtcompany_email.Text & " ", "") &
                                        IIf(ConvertToString(Check_NULLALL(txtob_address.Text)) <> "", "ON BEHALF OF " & txtob_address.Text, "") & IIf(Check_NULLALL(txtNewEmail_ch01.Text) <> "", " E-mail: " & txtNewEmail_ch01.Text, "")
                ElseIf Mid(txtdest_remark.Text, 2, 1) <> "/" Then
                    StrtxtRerutn = ""
                    'StrtxtRerutn = txtcompany_name.Text & " " & txtcompany_address.Text & " " & txtcompany_province.Text _
                    '                    & " " & txtcompany_country.Text & IIf(ConvertToString(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") & _
                    '                    IIf(ConvertToString(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") _
                    '                    & IIf(ConvertToString(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID: " & txtcompany_taxno.Text, "") & _
                    '                    IIf(ConvertToString(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")

                End If
            Case "FORM6"
                If Mid(txtdest_remark.Text, 1, 3) = "C/O" Then
                    StrtxtRerutn = txtob_address.Text & IIf(Check_NULLALL(txtNewEmail_ch01.Text) <> "", " E-mail: " & txtNewEmail_ch01.Text, "") & " CARE OF " & txtcompany_name.Text & " " & txtcompany_address.Text _
                                       & " " & txtcompany_province.Text & " " & txtcompany_country.Text &
                                       IIf(ConvertToString(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") &
                                       IIf(ConvertToString(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") &
                                       IIf(ConvertToString(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID:  " & txtcompany_taxno.Text, "") &
                                       IIf(ConvertToString(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")
                ElseIf Mid(txtdest_remark.Text, 1, 3) = "O/B" Then
                    StrtxtRerutn = IIf(ConvertToString(Check_NULLALL(txtcompany_email.Text)) <> "", "E-mail: " & txtcompany_email.Text & " ", "") &
                                        IIf(ConvertToString(Check_NULLALL(txtob_address.Text)) <> "", "ON BEHALF OF " & txtob_address.Text, "") & IIf(Check_NULLALL(txtNewEmail_ch01.Text) <> "", " E-mail: " & txtNewEmail_ch01.Text, "")
                ElseIf Mid(txtdest_remark.Text, 2, 1) <> "/" Then
                    StrtxtRerutn = ""
                    'StrtxtRerutn = txtcompany_name.Text & " " & txtcompany_address.Text & " " & txtcompany_province.Text _
                    '                    & " " & txtcompany_country.Text & IIf(ConvertToString(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") & _
                    '                    IIf(ConvertToString(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") _
                    '                    & IIf(ConvertToString(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID: " & txtcompany_taxno.Text, "") & _
                    '                    IIf(ConvertToString(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")

                End If
            Case "FORM5", "FORM5_ESS"
                If Mid(txtdest_remark.Text, 1, 3) = "C/O" Then
                    StrtxtRerutn = txtob_address.Text & IIf(Check_NULLALL(txtNewEmail_ch01.Text) <> "", " E-mail: " & txtNewEmail_ch01.Text, "") & " CARE OF " & txtcompany_name.Text & " " & txtcompany_address.Text _
                                       & " " & txtcompany_province.Text & " " & txtcompany_country.Text &
                                       IIf(ConvertToString(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") &
                                       IIf(ConvertToString(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") &
                                       IIf(ConvertToString(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID:  " & txtcompany_taxno.Text, "") &
                                       IIf(ConvertToString(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")
                ElseIf Mid(txtdest_remark.Text, 1, 3) = "O/B" Then
                    StrtxtRerutn = IIf(ConvertToString(Check_NULLALL(txtcompany_email.Text)) <> "", "E-mail: " & txtcompany_email.Text & " ", "") &
                                        IIf(ConvertToString(Check_NULLALL(txtob_address.Text)) <> "", "ON BEHALF OF " & txtob_address.Text, "") & IIf(Check_NULLALL(txtNewEmail_ch01.Text) <> "", " E-mail: " & txtNewEmail_ch01.Text, "")
                ElseIf Mid(txtdest_remark.Text, 2, 1) <> "/" Then
                    StrtxtRerutn = ""
                    'StrtxtRerutn = txtcompany_name.Text & " " & txtcompany_address.Text & " " & txtcompany_province.Text _
                    '                    & " " & txtcompany_country.Text & IIf(ConvertToString(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") & _
                    '                    IIf(ConvertToString(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") _
                    '                    & IIf(ConvertToString(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID: " & txtcompany_taxno.Text, "") & _
                    '                    IIf(ConvertToString(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")

                End If
            Case "FORM5_1"
                If Mid(txtdest_remark.Text, 1, 3) = "C/O" Then
                    StrtxtRerutn = txtob_address.Text & IIf(Check_NULLALL(txtNewEmail_ch01.Text) <> "", " E-mail: " & txtNewEmail_ch01.Text, "") & " CARE OF " & txtcompany_name.Text & " " & txtcompany_address.Text _
                                       & " " & txtcompany_province.Text & " " & txtcompany_country.Text &
                                       IIf(ConvertToString(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") &
                                       IIf(ConvertToString(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") &
                                       IIf(ConvertToString(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID:  " & txtcompany_taxno.Text, "") &
                                       IIf(ConvertToString(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")
                ElseIf Mid(txtdest_remark.Text, 1, 3) = "O/B" Then
                    StrtxtRerutn = IIf(ConvertToString(Check_NULLALL(txtcompany_email.Text)) <> "", "E-mail: " & txtcompany_email.Text & " ", "") &
                                        IIf(ConvertToString(Check_NULLALL(txtob_address.Text)) <> "", "ON BEHALF OF " & txtob_address.Text, "") & IIf(Check_NULLALL(txtNewEmail_ch01.Text) <> "", " E-mail: " & txtNewEmail_ch01.Text, "")
                ElseIf Mid(txtdest_remark.Text, 2, 1) <> "/" Then
                    StrtxtRerutn = ""
                    'StrtxtRerutn = txtcompany_name.Text & " " & txtcompany_address.Text & " " & txtcompany_province.Text _
                    '                    & " " & txtcompany_country.Text & IIf(ConvertToString(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") & _
                    '                    IIf(ConvertToString(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") _
                    '                    & IIf(ConvertToString(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID: " & txtcompany_taxno.Text, "") & _
                    '                    IIf(ConvertToString(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")

                ElseIf Mid(txtdest_remark.Text, 1, 5) = "O/TOB" Then
                    StrtxtRerutn = IIf(ConvertToString(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text & " ", "") & txtob_address.Text

                ElseIf Mid(txtdest_remark.Text, 1, 5) = "O/TCO" Then
                    StrtxtRerutn = txtob_address.Text & " " & txtcompany_name.Text & " " & txtcompany_address.Text & " " & txtcompany_province.Text _
                                    & " " & txtcompany_country.Text &
                                    IIf(ConvertToString(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") &
                                    IIf(ConvertToString(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") _
                                    & IIf(ConvertToString(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID: " & txtcompany_taxno.Text, "") &
                                    IIf(ConvertToString(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")

                End If
            Case "FORM5_2"
                If Mid(txtdest_remark.Text, 1, 3) = "C/O" Then
                    StrtxtRerutn = txtob_address.Text & IIf(Check_NULLALL(txtNewEmail_ch01.Text) <> "", " E-mail: " & txtNewEmail_ch01.Text, "") & " CARE OF " & txtcompany_name.Text & " " & txtcompany_address.Text _
                                       & " " & txtcompany_province.Text & " " & txtcompany_country.Text &
                                       IIf(ConvertToString(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") &
                                       IIf(ConvertToString(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") &
                                       IIf(ConvertToString(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID:  " & txtcompany_taxno.Text, "") &
                                       IIf(ConvertToString(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")
                ElseIf Mid(txtdest_remark.Text, 1, 3) = "O/B" Then
                    StrtxtRerutn = IIf(ConvertToString(Check_NULLALL(txtcompany_email.Text)) <> "", "E-mail: " & txtcompany_email.Text & " ", "") &
                                        IIf(ConvertToString(Check_NULLALL(txtob_address.Text)) <> "", "ON BEHALF OF " & txtob_address.Text, "") & IIf(Check_NULLALL(txtNewEmail_ch01.Text) <> "", " E-mail: " & txtNewEmail_ch01.Text, "")
                ElseIf Mid(txtdest_remark.Text, 2, 1) <> "/" Then
                    StrtxtRerutn = ""
                    'StrtxtRerutn = txtcompany_name.Text & " " & txtcompany_address.Text & " " & txtcompany_province.Text _
                    '                    & " " & txtcompany_country.Text & IIf(ConvertToString(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") & _
                    '                    IIf(ConvertToString(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") _
                    '                    & IIf(ConvertToString(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID: " & txtcompany_taxno.Text, "") & _
                    '                    IIf(ConvertToString(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")

                ElseIf Mid(txtdest_remark.Text, 1, 5) = "O/TOB" Then
                    StrtxtRerutn = IIf(ConvertToString(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text & " ", "") & txtob_address.Text

                ElseIf Mid(txtdest_remark.Text, 1, 5) = "O/TCO" Then
                    StrtxtRerutn = txtob_address.Text & " " & txtcompany_name.Text & " " & txtcompany_address.Text & " " & txtcompany_province.Text _
                                    & " " & txtcompany_country.Text &
                                    IIf(ConvertToString(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") &
                                    IIf(ConvertToString(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") _
                                    & IIf(ConvertToString(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID: " & txtcompany_taxno.Text, "") &
                                    IIf(ConvertToString(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")

                End If
            Case "FORM441", "FORM441_4"
                If Mid(txtdest_remark.Text, 1, 3) = "C/O" Then
                    StrtxtRerutn = txtob_address.Text & IIf(Check_NULLALL(txtNewEmail_ch01.Text) <> "", " E-mail: " & txtNewEmail_ch01.Text, "") & " CARE OF " & txtcompany_name.Text & " " & txtcompany_address.Text _
                                       & " " & txtcompany_province.Text & " " & txtcompany_country.Text &
                                       IIf(ConvertToString(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") &
                                       IIf(ConvertToString(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") &
                                       IIf(ConvertToString(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID:  " & txtcompany_taxno.Text, "") &
                                       IIf(ConvertToString(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")
                ElseIf Mid(txtdest_remark.Text, 1, 3) = "O/B" Then
                    StrtxtRerutn = IIf(ConvertToString(Check_NULLALL(txtcompany_email.Text)) <> "", "E-mail: " & txtcompany_email.Text & " ", "") &
                                        IIf(ConvertToString(Check_NULLALL(txtob_address.Text)) <> "", "ON BEHALF OF " & txtob_address.Text, "") & IIf(Check_NULLALL(txtNewEmail_ch01.Text) <> "", " E-mail: " & txtNewEmail_ch01.Text, "")
                ElseIf Mid(txtdest_remark.Text, 2, 1) <> "/" Then
                    StrtxtRerutn = ""
                    'StrtxtRerutn = txtcompany_name.Text & " " & txtcompany_address.Text & " " & txtcompany_province.Text _
                    '                    & " " & txtcompany_country.Text & IIf(ConvertToString(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") & _
                    '                    IIf(ConvertToString(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") _
                    '                    & IIf(ConvertToString(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID: " & txtcompany_taxno.Text, "") & _
                    '                    IIf(ConvertToString(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")

                ElseIf Mid(txtdest_remark.Text, 1, 5) <> "O/TOB" Then
                    StrtxtRerutn = txtcompany_name.Text & " " & txtcompany_address.Text & " " & txtcompany_province.Text _
                                    & " " & txtcompany_country.Text & " " & txtob_address.Text &
                                    IIf(ConvertToString(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") &
                                    IIf(ConvertToString(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") _
                                    & IIf(ConvertToString(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID: " & txtcompany_taxno.Text, "") &
                                    IIf(ConvertToString(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")

                ElseIf Mid(txtdest_remark.Text, 1, 5) <> "O/TCO" Then
                    StrtxtRerutn = txtob_address.Text & txtcompany_name.Text & " " & txtcompany_address.Text & " " & txtcompany_province.Text _
                                    & " " & txtcompany_country.Text &
                                    IIf(ConvertToString(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") &
                                    IIf(ConvertToString(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") _
                                    & IIf(ConvertToString(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID: " & txtcompany_taxno.Text, "") &
                                    IIf(ConvertToString(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")

                End If
            Case "FORM44", "FORM44_44", "FORM44_02", "FORMD_ESS_", "FORMD_ESS_ATTS"
                If Mid(txtdest_remark.Text, 1, 3) = "C/O" Then
                    StrtxtRerutn = txtob_address.Text & IIf(Check_NULLALL(txtNewEmail_ch01.Text) <> "", " E-mail: " & txtNewEmail_ch01.Text, "") & " CARE OF " & txtcompany_name.Text & " " & txtcompany_address.Text _
                                       & " " & txtcompany_province.Text & " " & txtcompany_country.Text &
                                       IIf(ConvertToString(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") &
                                       IIf(ConvertToString(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") &
                                       IIf(ConvertToString(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID:  " & txtcompany_taxno.Text, "") &
                                       IIf(ConvertToString(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")
                ElseIf Mid(txtdest_remark.Text, 1, 3) = "O/B" Then
                    StrtxtRerutn = IIf(ConvertToString(Check_NULLALL(txtcompany_email.Text)) <> "", "E-mail: " & txtcompany_email.Text & " ", "") &
                                        IIf(ConvertToString(Check_NULLALL(txtob_address.Text)) <> "", "ON BEHALF OF " & txtob_address.Text, "") & IIf(Check_NULLALL(txtNewEmail_ch01.Text) <> "", " E-mail: " & txtNewEmail_ch01.Text, "")
                ElseIf Mid(txtdest_remark.Text, 2, 1) <> "/" Then
                    StrtxtRerutn = ""
                    'StrtxtRerutn = txtcompany_name.Text & " " & txtcompany_address.Text & " " & txtcompany_province.Text _
                    '                    & " " & txtcompany_country.Text & IIf(ConvertToString(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") & _
                    '                    IIf(ConvertToString(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") _
                    '                    & IIf(ConvertToString(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID: " & txtcompany_taxno.Text, "") & _
                    '                    IIf(ConvertToString(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")

                ElseIf Mid(txtdest_remark.Text, 1, 5) <> "O/TOB" Then
                    StrtxtRerutn = txtcompany_name.Text & " " & txtcompany_address.Text & " " & txtcompany_province.Text _
                                    & " " & txtcompany_country.Text & " " & txtob_address.Text &
                                    IIf(ConvertToString(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") &
                                    IIf(ConvertToString(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") _
                                    & IIf(ConvertToString(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID: " & txtcompany_taxno.Text, "") &
                                    IIf(ConvertToString(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")

                ElseIf Mid(txtdest_remark.Text, 1, 5) <> "O/TCO" Then
                    StrtxtRerutn = txtob_address.Text & txtcompany_name.Text & " " & txtcompany_address.Text & " " & txtcompany_province.Text _
                                    & " " & txtcompany_country.Text &
                                    IIf(ConvertToString(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") &
                                    IIf(ConvertToString(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") _
                                    & IIf(ConvertToString(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID: " & txtcompany_taxno.Text, "") &
                                    IIf(ConvertToString(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")

                End If
            Case "FORM4", "FORM44_4", "FORM44_01", "FORMRCEP", "FORMAHK", "FORME_01", "FORMD_ESS"
                If Mid(txtdest_remark.Text, 1, 3) = "C/O" Then
                    StrtxtRerutn = txtob_address.Text & IIf(Check_NULLALL(txtNewEmail_ch01.Text) <> "", " E-mail: " & txtNewEmail_ch01.Text, "") & " CARE OF " & txtcompany_name.Text & " " & txtcompany_address.Text _
                                       & " " & txtcompany_province.Text & " " & txtcompany_country.Text &
                                       IIf(ConvertToString(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") &
                                       IIf(ConvertToString(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") &
                                       IIf(ConvertToString(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID:  " & txtcompany_taxno.Text, "") &
                                       IIf(ConvertToString(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")
                ElseIf Mid(txtdest_remark.Text, 1, 3) = "O/B" Then
                    StrtxtRerutn = IIf(ConvertToString(Check_NULLALL(txtcompany_email.Text)) <> "", "E-mail: " & txtcompany_email.Text & " ", "") &
                                        IIf(ConvertToString(Check_NULLALL(txtob_address.Text)) <> "", "ON BEHALF OF " & txtob_address.Text, "") & IIf(Check_NULLALL(txtNewEmail_ch01.Text) <> "", " E-mail: " & txtNewEmail_ch01.Text, "")
                ElseIf Mid(txtdest_remark.Text, 2, 1) <> "/" Then
                    StrtxtRerutn = ""
                    'StrtxtRerutn = txtcompany_name.Text & " " & txtcompany_address.Text & " " & txtcompany_province.Text _
                    '                    & " " & txtcompany_country.Text & IIf(ConvertToString(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") & _
                    '                    IIf(ConvertToString(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") _
                    '                    & IIf(ConvertToString(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID: " & txtcompany_taxno.Text, "") & _
                    '                    IIf(ConvertToString(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")

                ElseIf Mid(txtdest_remark.Text, 1, 5) <> "O/TOB" Then
                    StrtxtRerutn = txtcompany_name.Text & " " & txtcompany_address.Text & " " & txtcompany_province.Text _
                                    & " " & txtcompany_country.Text & " " & txtob_address.Text &
                                    IIf(ConvertToString(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") &
                                    IIf(ConvertToString(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") _
                                    & IIf(ConvertToString(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID: " & txtcompany_taxno.Text, "") &
                                    IIf(ConvertToString(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")

                ElseIf Mid(txtdest_remark.Text, 1, 5) <> "O/TCO" Then
                    StrtxtRerutn = txtob_address.Text & txtcompany_name.Text & " " & txtcompany_address.Text & " " & txtcompany_province.Text _
                                    & " " & txtcompany_country.Text &
                                    IIf(ConvertToString(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") &
                                    IIf(ConvertToString(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") _
                                    & IIf(ConvertToString(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID: " & txtcompany_taxno.Text, "") &
                                    IIf(ConvertToString(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")

                End If
            Case "FORM4_91", "FORM4_911", "FORMAI_ESS"
                Select Case ConvertToString(txtCheck_StatusWeb.Text)
                    Case Is <> ""
                        If Mid(txtdest_remark.Text, 1, 3) = "C/O" Then
                            StrtxtRerutn = txtob_address.Text & IIf(Check_NULLALL(txtNewEmail_ch01.Text) <> "", " E-mail: " & txtNewEmail_ch01.Text, "") & " CARE OF " & txtcompany_name.Text & " " & txtcompany_address.Text _
                                               & " " & txtcompany_province.Text & " " & txtcompany_country.Text &
                                               IIf(ConvertToString(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") &
                                               IIf(ConvertToString(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") &
                                               IIf(ConvertToString(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID:  " & txtcompany_taxno.Text, "") &
                                               IIf(ConvertToString(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")
                        ElseIf Mid(txtdest_remark.Text, 1, 3) = "O/B" Then
                            StrtxtRerutn = IIf(ConvertToString(Check_NULLALL(txtcompany_email.Text)) <> "", "E-mail: " & txtcompany_email.Text & " ", "") &
                                                IIf(ConvertToString(Check_NULLALL(txtob_address.Text)) <> "", "ON BEHALF OF " & txtob_address.Text, "") & IIf(Check_NULLALL(txtNewEmail_ch01.Text) <> "", " E-mail: " & txtNewEmail_ch01.Text, "")
                        ElseIf Mid(txtdest_remark.Text, 2, 1) <> "/" Then
                            StrtxtRerutn = ""
                            'StrtxtRerutn = txtcompany_name.Text & " " & txtcompany_address.Text & " " & txtcompany_province.Text _
                            '                    & " " & txtcompany_country.Text & IIf(ConvertToString(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") & _
                            '                    IIf(ConvertToString(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") _
                            '                    & IIf(ConvertToString(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID: " & txtcompany_taxno.Text, "") & _
                            '                    IIf(ConvertToString(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")

                        ElseIf Mid(txtdest_remark.Text, 1, 5) <> "O/TOB" Then
                            StrtxtRerutn = txtcompany_name.Text & " " & txtcompany_address.Text & " " & txtcompany_province.Text _
                                            & " " & txtcompany_country.Text & " " & txtob_address.Text &
                                            IIf(ConvertToString(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") &
                                            IIf(ConvertToString(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") _
                                            & IIf(ConvertToString(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID: " & txtcompany_taxno.Text, "") &
                                            IIf(ConvertToString(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")

                        ElseIf Mid(txtdest_remark.Text, 1, 5) <> "O/TCO" Then
                            StrtxtRerutn = txtob_address.Text & txtcompany_name.Text & " " & txtcompany_address.Text & " " & txtcompany_province.Text _
                                            & " " & txtcompany_country.Text &
                                            IIf(ConvertToString(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") &
                                            IIf(ConvertToString(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") _
                                            & IIf(ConvertToString(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID: " & txtcompany_taxno.Text, "") &
                                            IIf(ConvertToString(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")

                        End If
                    Case Is = "" 'ระบบเก่า
                        If Mid(txtdest_remark.Text, 1, 3) = "C/O" Then
                            StrtxtRerutn = txtob_address.Text & " CARE OF " & txtcompany_name.Text & " " & txtcompany_address.Text _
                                               & " " & txtcompany_province.Text & " " & txtcompany_country.Text &
                                               IIf(ConvertToString(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") &
                                               IIf(ConvertToString(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") &
                                               IIf(ConvertToString(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID:  " & txtcompany_taxno.Text, "") &
                                               IIf(ConvertToString(Check_NULLALL(txtdeclare_doc.Text)) <> "", " E-mail: " & txtdeclare_doc.Text, "")
                        ElseIf Mid(txtdest_remark.Text, 1, 3) = "O/B" Then
                            StrtxtRerutn = IIf(ConvertToString(Check_NULLALL(txtdeclare_doc.Text)) <> "", "E-mail: " & txtdeclare_doc.Text & " ", "") &
                                                IIf(ConvertToString(Check_NULLALL(txtob_address.Text)) <> "", "ON BEHALF OF " & txtob_address.Text, "")
                        ElseIf Mid(txtdest_remark.Text, 2, 1) <> "/" Then
                            StrtxtRerutn = ""
                            'StrtxtRerutn = txtcompany_name.Text & " " & txtcompany_address.Text & " " & txtcompany_province.Text _
                            '                    & " " & txtcompany_country.Text & IIf(ConvertToString(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") & _
                            '                    IIf(ConvertToString(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") _
                            '                    & IIf(ConvertToString(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID: " & txtcompany_taxno.Text, "") & _
                            '                    IIf(ConvertToString(Check_NULLALL(txtdeclare_doc.Text)) <> "", " E-mail: " & txtdeclare_doc.Text, "")

                        ElseIf Mid(txtdest_remark.Text, 1, 5) <> "O/TOB" Then
                            StrtxtRerutn = txtcompany_name.Text & " " & txtcompany_address.Text & " " & txtcompany_province.Text _
                                            & " " & txtcompany_country.Text & " " & txtob_address.Text &
                                            IIf(ConvertToString(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") &
                                            IIf(ConvertToString(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") _
                                            & IIf(ConvertToString(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID: " & txtcompany_taxno.Text, "") &
                                            IIf(ConvertToString(Check_NULLALL(txtdeclare_doc.Text)) <> "", " E-mail: " & txtdeclare_doc.Text, "")

                        ElseIf Mid(txtdest_remark.Text, 1, 5) <> "O/TCO" Then
                            StrtxtRerutn = txtob_address.Text & txtcompany_name.Text & " " & txtcompany_address.Text & " " & txtcompany_province.Text _
                                            & " " & txtcompany_country.Text &
                                            IIf(ConvertToString(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") &
                                            IIf(ConvertToString(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") _
                                            & IIf(ConvertToString(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID: " & txtcompany_taxno.Text, "") &
                                            IIf(ConvertToString(Check_NULLALL(txtdeclare_doc.Text)) <> "", " E-mail: " & txtdeclare_doc.Text, "")

                        End If
                End Select
            Case "FORM4_9", "FORMAI_ESS"
                Select Case ConvertToString(txtCheck_StatusWeb.Text)
                    Case Is <> ""
                        If Mid(txtdest_remark.Text, 1, 3) = "C/O" Then
                            StrtxtRerutn = txtob_address.Text & IIf(Check_NULLALL(txtNewEmail_ch01.Text) <> "", " E-mail: " & txtNewEmail_ch01.Text, "") & " CARE OF " & txtcompany_name.Text & " " & txtcompany_address.Text _
                                               & " " & txtcompany_province.Text & " " & txtcompany_country.Text &
                                               IIf(ConvertToString(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") &
                                               IIf(ConvertToString(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") &
                                               IIf(ConvertToString(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID:  " & txtcompany_taxno.Text, "") &
                                               IIf(ConvertToString(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")
                        ElseIf Mid(txtdest_remark.Text, 1, 3) = "O/B" Then
                            StrtxtRerutn = IIf(ConvertToString(Check_NULLALL(txtcompany_email.Text)) <> "", "E-mail: " & txtcompany_email.Text & " ", "") &
                                                IIf(ConvertToString(Check_NULLALL(txtob_address.Text)) <> "", "ON BEHALF OF " & txtob_address.Text, "") & IIf(Check_NULLALL(txtNewEmail_ch01.Text) <> "", " E-mail: " & txtNewEmail_ch01.Text, "")
                        ElseIf Mid(txtdest_remark.Text, 2, 1) <> "/" Then
                            StrtxtRerutn = ""
                            'StrtxtRerutn = txtcompany_name.Text & " " & txtcompany_address.Text & " " & txtcompany_province.Text _
                            '                    & " " & txtcompany_country.Text & IIf(ConvertToString(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") & _
                            '                    IIf(ConvertToString(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") _
                            '                    & IIf(ConvertToString(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID: " & txtcompany_taxno.Text, "") & _
                            '                    IIf(ConvertToString(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")

                        ElseIf Mid(txtdest_remark.Text, 1, 5) <> "O/TOB" Then
                            StrtxtRerutn = txtcompany_name.Text & " " & txtcompany_address.Text & " " & txtcompany_province.Text _
                                            & " " & txtcompany_country.Text & " " & txtob_address.Text &
                                            IIf(ConvertToString(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") &
                                            IIf(ConvertToString(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") _
                                            & IIf(ConvertToString(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID: " & txtcompany_taxno.Text, "") &
                                            IIf(ConvertToString(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")

                        ElseIf Mid(txtdest_remark.Text, 1, 5) <> "O/TCO" Then
                            StrtxtRerutn = txtob_address.Text & txtcompany_name.Text & " " & txtcompany_address.Text & " " & txtcompany_province.Text _
                                            & " " & txtcompany_country.Text &
                                            IIf(ConvertToString(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") &
                                            IIf(ConvertToString(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") _
                                            & IIf(ConvertToString(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID: " & txtcompany_taxno.Text, "") &
                                            IIf(ConvertToString(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")

                        End If
                    Case Is = "" 'ระบบเก่า
                        If Mid(txtdest_remark.Text, 1, 3) = "C/O" Then
                            StrtxtRerutn = txtob_address.Text & " CARE OF " & txtcompany_name.Text & " " & txtcompany_address.Text _
                                               & " " & txtcompany_province.Text & " " & txtcompany_country.Text &
                                               IIf(ConvertToString(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") &
                                               IIf(ConvertToString(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") &
                                               IIf(ConvertToString(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID:  " & txtcompany_taxno.Text, "") &
                                               IIf(ConvertToString(Check_NULLALL(txtdeclare_doc.Text)) <> "", " E-mail: " & txtdeclare_doc.Text, "")
                        ElseIf Mid(txtdest_remark.Text, 1, 3) = "O/B" Then
                            StrtxtRerutn = IIf(ConvertToString(Check_NULLALL(txtdeclare_doc.Text)) <> "", "E-mail: " & txtdeclare_doc.Text & " ", "") &
                                                IIf(ConvertToString(Check_NULLALL(txtob_address.Text)) <> "", "ON BEHALF OF " & txtob_address.Text, "")
                        ElseIf Mid(txtdest_remark.Text, 2, 1) <> "/" Then
                            StrtxtRerutn = ""
                            'StrtxtRerutn = txtcompany_name.Text & " " & txtcompany_address.Text & " " & txtcompany_province.Text _
                            '                    & " " & txtcompany_country.Text & IIf(ConvertToString(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") & _
                            '                    IIf(ConvertToString(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") _
                            '                    & IIf(ConvertToString(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID: " & txtcompany_taxno.Text, "") & _
                            '                    IIf(ConvertToString(Check_NULLALL(txtdeclare_doc.Text)) <> "", " E-mail: " & txtdeclare_doc.Text, "")

                        ElseIf Mid(txtdest_remark.Text, 1, 5) <> "O/TOB" Then
                            StrtxtRerutn = txtcompany_name.Text & " " & txtcompany_address.Text & " " & txtcompany_province.Text _
                                            & " " & txtcompany_country.Text & " " & txtob_address.Text &
                                            IIf(ConvertToString(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") &
                                            IIf(ConvertToString(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") _
                                            & IIf(ConvertToString(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID: " & txtcompany_taxno.Text, "") &
                                            IIf(ConvertToString(Check_NULLALL(txtdeclare_doc.Text)) <> "", " E-mail: " & txtdeclare_doc.Text, "")

                        ElseIf Mid(txtdest_remark.Text, 1, 5) <> "O/TCO" Then
                            StrtxtRerutn = txtob_address.Text & txtcompany_name.Text & " " & txtcompany_address.Text & " " & txtcompany_province.Text _
                                            & " " & txtcompany_country.Text &
                                            IIf(ConvertToString(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") &
                                            IIf(ConvertToString(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") _
                                            & IIf(ConvertToString(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID: " & txtcompany_taxno.Text, "") &
                                            IIf(ConvertToString(Check_NULLALL(txtdeclare_doc.Text)) <> "", " E-mail: " & txtdeclare_doc.Text, "")

                        End If
                End Select
            Case "FORM4_8", "FORM4_81"
                If Mid(txtdest_remark.Text, 1, 3) = "C/O" Then
                    StrtxtRerutn = txtob_address.Text & IIf(Check_NULLALL(txtNewEmail_ch01.Text) <> "", " E-mail: " & txtNewEmail_ch01.Text, "") & " CARE OF " & txtcompany_name.Text & " " & txtcompany_address.Text _
                                       & " " & txtcompany_province.Text & " " & txtcompany_country.Text &
                                       IIf(ConvertToString(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") &
                                       IIf(ConvertToString(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") &
                                       IIf(ConvertToString(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID:  " & txtcompany_taxno.Text, "") &
                                       IIf(ConvertToString(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")
                ElseIf Mid(txtdest_remark.Text, 1, 3) = "O/B" Then
                    StrtxtRerutn = IIf(ConvertToString(Check_NULLALL(txtcompany_email.Text)) <> "", "E-mail: " & txtcompany_email.Text & " ", "") &
                                        IIf(ConvertToString(Check_NULLALL(txtob_address.Text)) <> "", "ON BEHALF OF " & txtob_address.Text, "") & IIf(Check_NULLALL(txtNewEmail_ch01.Text) <> "", " E-mail: " & txtNewEmail_ch01.Text, "")
                ElseIf Mid(txtdest_remark.Text, 2, 1) <> "/" Then
                    StrtxtRerutn = ""
                    'StrtxtRerutn = txtcompany_name.Text & " " & txtcompany_address.Text & " " & txtcompany_province.Text _
                    '                    & " " & txtcompany_country.Text & IIf(ConvertToString(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") & _
                    '                    IIf(ConvertToString(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") _
                    '                    & IIf(ConvertToString(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID: " & txtcompany_taxno.Text, "") & _
                    '                    IIf(ConvertToString(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")

                ElseIf Mid(txtdest_remark.Text, 1, 5) <> "O/TOB" Then
                    StrtxtRerutn = txtcompany_name.Text & " " & txtcompany_address.Text & " " & txtcompany_province.Text _
                                    & " " & txtcompany_country.Text & " " & txtob_address.Text &
                                    IIf(ConvertToString(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") &
                                    IIf(ConvertToString(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") _
                                    & IIf(ConvertToString(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID: " & txtcompany_taxno.Text, "") &
                                    IIf(ConvertToString(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")

                ElseIf Mid(txtdest_remark.Text, 1, 5) <> "O/TCO" Then
                    StrtxtRerutn = txtob_address.Text & txtcompany_name.Text & " " & txtcompany_address.Text & " " & txtcompany_province.Text _
                                    & " " & txtcompany_country.Text &
                                    IIf(ConvertToString(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") &
                                    IIf(ConvertToString(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") _
                                    & IIf(ConvertToString(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID: " & txtcompany_taxno.Text, "") &
                                    IIf(ConvertToString(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")

                End If
            Case "FORM4_6", "FORM4_61"
                If Mid(txtdest_remark.Text, 1, 3) = "C/O" Then
                    StrtxtRerutn = txtob_address.Text & IIf(Check_NULLALL(txtNewEmail_ch01.Text) <> "", " E-mail: " & txtNewEmail_ch01.Text, "") & " CARE OF " & txtcompany_name.Text & " " & txtcompany_address.Text _
                                       & " " & txtcompany_province.Text & " " & txtcompany_country.Text &
                                       IIf(ConvertToString(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") &
                                       IIf(ConvertToString(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") &
                                       IIf(ConvertToString(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID:  " & txtcompany_taxno.Text, "") &
                                       IIf(ConvertToString(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")
                ElseIf Mid(txtdest_remark.Text, 1, 3) = "O/B" Then
                    StrtxtRerutn = IIf(ConvertToString(Check_NULLALL(txtcompany_email.Text)) <> "", "E-mail: " & txtcompany_email.Text & " ", "") &
                                        IIf(ConvertToString(Check_NULLALL(txtob_address.Text)) <> "", "ON BEHALF OF " & txtob_address.Text, "") & IIf(Check_NULLALL(txtNewEmail_ch01.Text) <> "", " E-mail: " & txtNewEmail_ch01.Text, "")
                ElseIf Mid(txtdest_remark.Text, 2, 1) <> "/" Then
                    StrtxtRerutn = ""
                    'StrtxtRerutn = txtcompany_name.Text & " " & txtcompany_address.Text & " " & txtcompany_province.Text _
                    '                    & " " & txtcompany_country.Text & IIf(ConvertToString(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") & _
                    '                    IIf(ConvertToString(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") _
                    '                    & IIf(ConvertToString(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID: " & txtcompany_taxno.Text, "") & _
                    '                    IIf(ConvertToString(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")

                ElseIf Mid(txtdest_remark.Text, 1, 5) <> "O/TOB" Then
                    StrtxtRerutn = IIf(ConvertToString(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text & " ", "") & txtob_address.Text

                ElseIf Mid(txtdest_remark.Text, 1, 5) <> "O/TCO" Then
                    StrtxtRerutn = txtob_address.Text & " " & txtcompany_name.Text & " " & txtcompany_address.Text & " " & txtcompany_province.Text _
                                    & " " & txtcompany_country.Text &
                                    IIf(ConvertToString(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") &
                                    IIf(ConvertToString(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") _
                                    & IIf(ConvertToString(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID: " & txtcompany_taxno.Text, "") &
                                    IIf(ConvertToString(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")

                End If
            Case "FORM4_5"
                If Mid(txtdest_remark.Text, 1, 3) = "C/O" Then
                    StrtxtRerutn = txtob_address.Text & IIf(Check_NULLALL(txtNewEmail_ch01.Text) <> "", " E-mail: " & txtNewEmail_ch01.Text, "") & " CARE OF " & txtcompany_name.Text & " " & txtcompany_address.Text _
                                       & " " & txtcompany_province.Text & " " & txtcompany_country.Text &
                                       IIf(ConvertToString(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") &
                                       IIf(ConvertToString(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") &
                                       IIf(ConvertToString(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID:  " & txtcompany_taxno.Text, "") &
                                       IIf(ConvertToString(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")
                ElseIf Mid(txtdest_remark.Text, 1, 3) = "O/B" Then
                    StrtxtRerutn = IIf(ConvertToString(Check_NULLALL(txtcompany_email.Text)) <> "", "E-mail: " & txtcompany_email.Text & " ", "") &
                                        IIf(ConvertToString(Check_NULLALL(txtob_address.Text)) <> "", "ON BEHALF OF " & txtob_address.Text, "") & IIf(Check_NULLALL(txtNewEmail_ch01.Text) <> "", " E-mail: " & txtNewEmail_ch01.Text, "")
                ElseIf Mid(txtdest_remark.Text, 2, 1) <> "/" Then
                    StrtxtRerutn = ""
                    'StrtxtRerutn = txtcompany_name.Text & " " & txtcompany_address.Text & " " & txtcompany_province.Text _
                    '                    & " " & txtcompany_country.Text & IIf(ConvertToString(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") & _
                    '                    IIf(ConvertToString(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") _
                    '                    & IIf(ConvertToString(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID: " & txtcompany_taxno.Text, "") & _
                    '                    IIf(ConvertToString(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")

                ElseIf Mid(txtdest_remark.Text, 1, 5) = "O/TOB" Then
                    StrtxtRerutn = IIf(ConvertToString(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text & " ", "") & txtob_address.Text

                ElseIf Mid(txtdest_remark.Text, 1, 5) = "O/TCO" Then
                    StrtxtRerutn = txtob_address.Text & " " & txtcompany_name.Text & " " & txtcompany_address.Text & " " & txtcompany_province.Text _
                                    & " " & txtcompany_country.Text &
                                    IIf(ConvertToString(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") &
                                    IIf(ConvertToString(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") _
                                    & IIf(ConvertToString(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID: " & txtcompany_taxno.Text, "") &
                                    IIf(ConvertToString(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")

                End If
            Case "FORM4_4"
                If Mid(txtdest_remark.Text, 1, 3) = "C/O" Then
                    StrtxtRerutn = txtob_address.Text & IIf(Check_NULLALL(txtNewEmail_ch01.Text) <> "", " E-mail: " & txtNewEmail_ch01.Text, "") & " CARE OF " & txtcompany_name.Text & " " & txtcompany_address.Text _
                                       & " " & txtcompany_province.Text & " " & txtcompany_country.Text &
                                       IIf(ConvertToString(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") &
                                       IIf(ConvertToString(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") &
                                       IIf(ConvertToString(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID:  " & txtcompany_taxno.Text, "") &
                                       IIf(ConvertToString(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")
                ElseIf Mid(txtdest_remark.Text, 1, 3) = "O/B" Then
                    StrtxtRerutn = IIf(ConvertToString(Check_NULLALL(txtcompany_email.Text)) <> "", "E-mail: " & txtcompany_email.Text & " ", "") &
                                        IIf(ConvertToString(Check_NULLALL(txtob_address.Text)) <> "", "ON BEHALF OF " & txtob_address.Text, "") & IIf(Check_NULLALL(txtNewEmail_ch01.Text) <> "", " E-mail: " & txtNewEmail_ch01.Text, "")
                ElseIf Mid(txtdest_remark.Text, 2, 1) <> "/" Then
                    StrtxtRerutn = ""
                    'StrtxtRerutn = txtcompany_name.Text & " " & txtcompany_address.Text & " " & txtcompany_province.Text _
                    '                    & " " & txtcompany_country.Text & IIf(ConvertToString(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") & _
                    '                    IIf(ConvertToString(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") _
                    '                    & IIf(ConvertToString(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID: " & txtcompany_taxno.Text, "") & _
                    '                    IIf(ConvertToString(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")

                ElseIf Mid(txtdest_remark.Text, 1, 5) <> "O/TOB" Then
                    StrtxtRerutn = txtcompany_name.Text & " " & txtcompany_address.Text & " " & txtcompany_province.Text _
                                    & " " & txtcompany_country.Text & " " & txtob_address.Text &
                                    IIf(ConvertToString(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") &
                                    IIf(ConvertToString(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") _
                                    & IIf(ConvertToString(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID: " & txtcompany_taxno.Text, "") &
                                    IIf(ConvertToString(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")

                ElseIf Mid(txtdest_remark.Text, 1, 5) <> "O/TCO" Then
                    StrtxtRerutn = txtob_address.Text & txtcompany_name.Text & " " & txtcompany_address.Text & " " & txtcompany_province.Text _
                                    & " " & txtcompany_country.Text &
                                    IIf(ConvertToString(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") &
                                    IIf(ConvertToString(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") _
                                    & IIf(ConvertToString(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID: " & txtcompany_taxno.Text, "") &
                                    IIf(ConvertToString(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")

                End If
            Case "FORM4_2"
                Select Case ConvertToString(txtTax_Status.Text)
                    Case 1 'ช่อง ให้แสดงหรือไม่แสดง Tax 1 คือ แสดง
                        If Mid(txtdest_remark.Text, 1, 3) = "C/O" Then
                            StrtxtRerutn = txtob_address.Text & IIf(Check_NULLALL(txtNewEmail_ch01.Text) <> "", " E-mail: " & txtNewEmail_ch01.Text, "") & " CARE OF " & txtcompany_name.Text & " " & txtcompany_address.Text _
                                               & " " & txtcompany_province.Text & " " & txtcompany_country.Text &
                                               IIf(ConvertToString(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") &
                                               IIf(ConvertToString(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") &
                                               IIf(ConvertToString(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID:  " & txtcompany_taxno.Text, "") &
                                               IIf(ConvertToString(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")
                        ElseIf Mid(txtdest_remark.Text, 1, 3) = "O/B" Then
                            StrtxtRerutn = IIf(ConvertToString(Check_NULLALL(txtcompany_email.Text)) <> "", "E-mail: " & txtcompany_email.Text & " ", "") &
                                                IIf(ConvertToString(Check_NULLALL(txtob_address.Text)) <> "", "ON BEHALF OF " & txtob_address.Text, "") & IIf(Check_NULLALL(txtNewEmail_ch01.Text) <> "", " E-mail: " & txtNewEmail_ch01.Text, "")
                        ElseIf Mid(txtdest_remark.Text, 2, 1) <> "/" Then
                            StrtxtRerutn = ""
                            'StrtxtRerutn = txtcompany_name.Text & " " & txtcompany_address.Text & " " & txtcompany_province.Text _
                            '                    & " " & txtcompany_country.Text & IIf(ConvertToString(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") & _
                            '                    IIf(ConvertToString(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") _
                            '                    & IIf(ConvertToString(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID: " & txtcompany_taxno.Text, "") & _
                            '                    IIf(ConvertToString(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")
                        End If
                    Case 0 'ช่อง ให้แสดงหรือไม่แสดง Tax 0 คือ ไม่แสดง
                        If Mid(txtdest_remark.Text, 1, 3) = "C/O" Then
                            StrtxtRerutn = txtob_address.Text & IIf(Check_NULLALL(txtNewEmail_ch01.Text) <> "", " E-mail: " & txtNewEmail_ch01.Text, "") & " CARE OF " & txtcompany_name.Text & " " & txtcompany_address.Text _
                                               & " " & txtcompany_province.Text & " " & txtcompany_country.Text &
                                               IIf(ConvertToString(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") &
                                               IIf(ConvertToString(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") &
                                               IIf(ConvertToString(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")
                        ElseIf Mid(txtdest_remark.Text, 1, 3) = "O/B" Then
                            StrtxtRerutn = IIf(ConvertToString(Check_NULLALL(txtcompany_email.Text)) <> "", "E-mail: " & txtcompany_email.Text & " ", "") &
                                                IIf(ConvertToString(Check_NULLALL(txtob_address.Text)) <> "", "ON BEHALF OF " & txtob_address.Text, "") & IIf(Check_NULLALL(txtNewEmail_ch01.Text) <> "", " E-mail: " & txtNewEmail_ch01.Text, "")
                        ElseIf Mid(txtdest_remark.Text, 2, 1) <> "/" Then
                            StrtxtRerutn = ""
                            'StrtxtRerutn = txtcompany_name.Text & " " & txtcompany_address.Text & " " & txtcompany_province.Text _
                            '                    & " " & txtcompany_country.Text & IIf(ConvertToString(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") & _
                            '                    IIf(ConvertToString(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") _
                            '                    & IIf(ConvertToString(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")
                        End If
                End Select
            Case "FORM4_1", "FORM44_41"
                If Mid(txtdest_remark.Text, 1, 3) = "C/O" Then
                    StrtxtRerutn = txtob_address.Text & IIf(Check_NULLALL(txtNewEmail_ch01.Text) <> "", " E-mail: " & txtNewEmail_ch01.Text, "") & " CARE OF " & txtcompany_name.Text & " " & txtcompany_address.Text _
                                       & " " & txtcompany_province.Text & " " & txtcompany_country.Text &
                                       IIf(ConvertToString(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") &
                                       IIf(ConvertToString(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") &
                                       IIf(ConvertToString(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID:  " & txtcompany_taxno.Text, "") &
                                       IIf(ConvertToString(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")
                ElseIf Mid(txtdest_remark.Text, 1, 3) = "O/B" Then
                    StrtxtRerutn = IIf(ConvertToString(Check_NULLALL(txtcompany_email.Text)) <> "", "E-mail: " & txtcompany_email.Text & " ", "") &
                                        IIf(ConvertToString(Check_NULLALL(txtob_address.Text)) <> "", "ON BEHALF OF " & txtob_address.Text, "") & IIf(Check_NULLALL(txtNewEmail_ch01.Text) <> "", " E-mail: " & txtNewEmail_ch01.Text, "")
                ElseIf Mid(txtdest_remark.Text, 2, 1) <> "/" Then
                    StrtxtRerutn = ""
                    'StrtxtRerutn = txtcompany_name.Text & " " & txtcompany_address.Text & " " & txtcompany_province.Text _
                    '                    & " " & txtcompany_country.Text & IIf(ConvertToString(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") & _
                    '                    IIf(ConvertToString(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") _
                    '                    & IIf(ConvertToString(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID: " & txtcompany_taxno.Text, "") & _
                    '                    IIf(ConvertToString(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")

                ElseIf Mid(txtdest_remark.Text, 1, 5) <> "O/TOB" Then
                    StrtxtRerutn = txtcompany_name.Text & " " & txtcompany_address.Text & " " & txtcompany_province.Text _
                                    & " " & txtcompany_country.Text & " " & txtob_address.Text &
                                    IIf(ConvertToString(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") &
                                    IIf(ConvertToString(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") _
                                    & IIf(ConvertToString(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID: " & txtcompany_taxno.Text, "") &
                                    IIf(ConvertToString(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")

                ElseIf Mid(txtdest_remark.Text, 1, 5) <> "O/TCO" Then
                    StrtxtRerutn = txtob_address.Text & txtcompany_name.Text & " " & txtcompany_address.Text & " " & txtcompany_province.Text _
                                    & " " & txtcompany_country.Text &
                                    IIf(ConvertToString(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") &
                                    IIf(ConvertToString(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") _
                                    & IIf(ConvertToString(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID: " & txtcompany_taxno.Text, "") &
                                    IIf(ConvertToString(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")

                End If
            Case "FORM3_1"
                StrtxtRerutn = str_01 & ConvertToString(txtByCom_CH01.Text)
            Case "FORM3"
                If Mid(txtdest_remark.Text, 1, 3) = "C/O" Then
                    StrtxtRerutn = str_01 & txtob_address.Text & IIf(Check_NULLALL(txtNewEmail_ch01.Text) <> "", " E-mail: " & txtNewEmail_ch01.Text, "") & " CARE OF " & txtcompany_name.Text & " " & txtcompany_address.Text _
                                       & " " & txtcompany_province.Text & " " & txtcompany_country.Text &
                                       IIf(ConvertToString(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") &
                                       IIf(ConvertToString(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") &
                                       IIf(ConvertToString(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID:  " & txtcompany_taxno.Text, "") &
                                       IIf(ConvertToString(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")
                ElseIf Mid(txtdest_remark.Text, 1, 3) = "O/B" Then
                    StrtxtRerutn = IIf(ConvertToString(Check_NULLALL(txtcompany_email.Text)) <> "", "E-mail: " & txtcompany_email.Text & " ", "") &
                                        IIf(ConvertToString(Check_NULLALL(txtob_address.Text)) <> "", "ON BEHALF OF " & txtob_address.Text, "") & IIf(Check_NULLALL(txtNewEmail_ch01.Text) <> "", " E-mail: " & txtNewEmail_ch01.Text, "")
                ElseIf Mid(txtdest_remark.Text, 2, 1) <> "/" Then
                    StrtxtRerutn = ""
                    'StrtxtRerutn = str_01 & txtcompany_name.Text & " " & txtcompany_address.Text & " " & txtcompany_province.Text _
                    '                    & " " & txtcompany_country.Text & IIf(ConvertToString(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") & _
                    '                    IIf(ConvertToString(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") _
                    '                    & IIf(ConvertToString(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID: " & txtcompany_taxno.Text, "") & _
                    '                    IIf(ConvertToString(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")

                End If
            Case "FORM2", "FORM2_ESS"
                If Mid(txtdest_remark.Text, 1, 3) = "C/O" Then
                    StrtxtRerutn = txtob_address.Text & IIf(Check_NULLALL(txtNewEmail_ch01.Text) <> "", " E-mail: " & txtNewEmail_ch01.Text, "") & " CARE OF " & txtcompany_name.Text & " " & txtcompany_address.Text _
                                       & " " & txtcompany_province.Text & " " & txtcompany_country.Text &
                                       IIf(ConvertToString(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") &
                                       IIf(ConvertToString(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") &
                                       IIf(ConvertToString(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID:  " & txtcompany_taxno.Text, "") &
                                       IIf(ConvertToString(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")
                ElseIf Mid(txtdest_remark.Text, 1, 3) = "O/B" Then
                    StrtxtRerutn = IIf(ConvertToString(Check_NULLALL(txtcompany_email.Text)) <> "", "E-mail: " & txtcompany_email.Text & " ", "") &
                                        IIf(ConvertToString(Check_NULLALL(txtob_address.Text)) <> "", "ON BEHALF OF " & txtob_address.Text, "") & IIf(Check_NULLALL(txtNewEmail_ch01.Text) <> "", " E-mail: " & txtNewEmail_ch01.Text, "")
                ElseIf Mid(txtdest_remark.Text, 2, 1) <> "/" Then
                    StrtxtRerutn = ""
                    'StrtxtRerutn = txtcompany_name.Text & " " & txtcompany_address.Text & " " & txtcompany_province.Text _
                    '                    & " " & txtcompany_country.Text & IIf(ConvertToString(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") & _
                    '                    IIf(ConvertToString(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") _
                    '                    & IIf(ConvertToString(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID: " & txtcompany_taxno.Text, "") & _
                    '                    IIf(ConvertToString(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")

                End If
            Case "FORM2_3"
                If Mid(txtdest_remark.Text, 1, 3) = "C/O" Then
                    StrtxtRerutn = txtob_address.Text & IIf(Check_NULLALL(txtNewEmail_ch01.Text) <> "", " E-mail: " & txtNewEmail_ch01.Text, "") & " CARE OF " & txtcompany_name.Text & " " & txtcompany_address.Text _
                                       & " " & txtcompany_province.Text & " " & txtcompany_country.Text &
                                       IIf(ConvertToString(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") &
                                       IIf(ConvertToString(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") &
                                       IIf(ConvertToString(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID:  " & txtcompany_taxno.Text, "") &
                                       IIf(ConvertToString(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")
                ElseIf Mid(txtdest_remark.Text, 1, 3) = "O/B" Then
                    StrtxtRerutn = IIf(ConvertToString(Check_NULLALL(txtcompany_email.Text)) <> "", "E-mail: " & txtcompany_email.Text & " ", "") &
                                        IIf(ConvertToString(Check_NULLALL(txtob_address.Text)) <> "", "ON BEHALF OF " & txtob_address.Text, "") & IIf(Check_NULLALL(txtNewEmail_ch01.Text) <> "", " E-mail: " & txtNewEmail_ch01.Text, "")
                ElseIf Mid(txtdest_remark.Text, 2, 1) <> "/" Then
                    StrtxtRerutn = ""
                    'StrtxtRerutn = txtcompany_name.Text & " " & txtcompany_address.Text & " " & txtcompany_province.Text _
                    '                    & " " & txtcompany_country.Text & IIf(ConvertToString(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") & _
                    '                    IIf(ConvertToString(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") _
                    '                    & IIf(ConvertToString(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID: " & txtcompany_taxno.Text, "") & _
                    '                    IIf(ConvertToString(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")

                End If
            Case "FORM2_2"
                If Mid(txtdest_remark.Text, 1, 3) = "C/O" Then
                    StrtxtRerutn = txtob_address.Text & IIf(Check_NULLALL(txtNewEmail_ch01.Text) <> "", " E-mail: " & txtNewEmail_ch01.Text, "") & " CARE OF " & txtcompany_name.Text & " " & txtcompany_address.Text _
                                       & " " & txtcompany_province.Text & " " & txtcompany_country.Text &
                                       IIf(ConvertToString(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") &
                                       IIf(ConvertToString(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") &
                                       IIf(ConvertToString(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID:  " & txtcompany_taxno.Text, "") &
                                       IIf(ConvertToString(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")
                ElseIf Mid(txtdest_remark.Text, 1, 3) = "O/B" Then
                    StrtxtRerutn = IIf(ConvertToString(Check_NULLALL(txtcompany_email.Text)) <> "", "E-mail: " & txtcompany_email.Text & " ", "") &
                                        IIf(ConvertToString(Check_NULLALL(txtob_address.Text)) <> "", "ON BEHALF OF " & txtob_address.Text, "") & IIf(Check_NULLALL(txtNewEmail_ch01.Text) <> "", " E-mail: " & txtNewEmail_ch01.Text, "")
                ElseIf Mid(txtdest_remark.Text, 2, 1) <> "/" Then
                    StrtxtRerutn = ""
                    'StrtxtRerutn = txtcompany_name.Text & " " & txtcompany_address.Text & " " & txtcompany_province.Text _
                    '                    & " " & txtcompany_country.Text & IIf(ConvertToString(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") & _
                    '                    IIf(ConvertToString(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") _
                    '                    & IIf(ConvertToString(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID: " & txtcompany_taxno.Text, "") & _
                    '                    IIf(ConvertToString(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")

                End If
            Case "FORMRUSSIA"
                If Mid(txtdest_remark.Text, 1, 3) = "C/O" Then
                    StrtxtRerutn = txtob_address.Text & IIf(Check_NULLALL(txtNewEmail_ch01.Text) <> "", " E-mail: " & txtNewEmail_ch01.Text, "") & " CARE OF " & txtcompany_name.Text & " " & txtcompany_address.Text _
                                       & " " & txtcompany_province.Text & " " & txtcompany_country.Text &
                                       IIf(ConvertToString(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") &
                                       IIf(ConvertToString(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") &
                                       IIf(ConvertToString(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID:  " & txtcompany_taxno.Text, "") &
                                       IIf(ConvertToString(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")
                ElseIf Mid(txtdest_remark.Text, 1, 3) = "O/B" Then
                    StrtxtRerutn = IIf(ConvertToString(Check_NULLALL(txtcompany_email.Text)) <> "", "E-mail: " & txtcompany_email.Text & " ", "") &
                                        IIf(ConvertToString(Check_NULLALL(txtob_address.Text)) <> "", "ON BEHALF OF " & txtob_address.Text, "") & IIf(Check_NULLALL(txtNewEmail_ch01.Text) <> "", " E-mail: " & txtNewEmail_ch01.Text, "")
                ElseIf Mid(txtdest_remark.Text, 2, 1) <> "/" Then
                    StrtxtRerutn = ""
                    'StrtxtRerutn = txtcompany_name.Text & " " & txtcompany_address.Text & " " & txtcompany_province.Text _
                    '                    & " " & txtcompany_country.Text & IIf(ConvertToString(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") & _
                    '                    IIf(ConvertToString(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") _
                    '                    & IIf(ConvertToString(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID: " & txtcompany_taxno.Text, "") & _
                    '                    IIf(ConvertToString(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")

                End If
            Case "FORM1"
                If Mid(txtdest_remark.Text, 1, 3) = "C/O" Then
                    StrtxtRerutn = txtob_address.Text & IIf(Check_NULLALL(txtNewEmail_ch01.Text) <> "", " E-mail: " & txtNewEmail_ch01.Text, "") & " CARE OF " & txtcompany_name.Text & " " & txtcompany_address.Text _
                                       & " " & txtcompany_province.Text & " " & txtcompany_country.Text &
                                       IIf(ConvertToString(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") &
                                       IIf(ConvertToString(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") &
                                       IIf(ConvertToString(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID:  " & txtcompany_taxno.Text, "") &
                                       IIf(ConvertToString(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")
                ElseIf Mid(txtdest_remark.Text, 1, 3) = "O/B" Then
                    StrtxtRerutn = IIf(ConvertToString(Check_NULLALL(txtcompany_email.Text)) <> "", "E-mail: " & txtcompany_email.Text & " ", "") &
                                        IIf(ConvertToString(Check_NULLALL(txtob_address.Text)) <> "", "ON BEHALF OF " & txtob_address.Text, "") & IIf(Check_NULLALL(txtNewEmail_ch01.Text) <> "", " E-mail: " & txtNewEmail_ch01.Text, "")
                ElseIf Mid(txtdest_remark.Text, 2, 1) <> "/" Then
                    StrtxtRerutn = ""
                    'StrtxtRerutn = txtcompany_name.Text & " " & txtcompany_address.Text & " " & txtcompany_province.Text _
                    '                    & " " & txtcompany_country.Text & IIf(ConvertToString(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") & _
                    '                    IIf(ConvertToString(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") _
                    '                    & IIf(ConvertToString(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID: " & txtcompany_taxno.Text, "") & _
                    '                    IIf(ConvertToString(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")

                End If
            Case "FORM1_4"
                If Mid(txtdest_remark.Text, 1, 3) = "C/O" Then
                    StrtxtRerutn = txtob_address.Text & IIf(Check_NULLALL(txtNewEmail_ch01.Text) <> "", " E-mail: " & txtNewEmail_ch01.Text, "") & " CARE OF " & txtcompany_name.Text & " " & txtcompany_address.Text _
                                       & " " & txtcompany_province.Text & " " & txtcompany_country.Text &
                                       IIf(ConvertToString(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") &
                                       IIf(ConvertToString(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") &
                                       IIf(ConvertToString(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID:  " & txtcompany_taxno.Text, "") &
                                       IIf(ConvertToString(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")
                ElseIf Mid(txtdest_remark.Text, 1, 3) = "O/B" Then
                    StrtxtRerutn = IIf(ConvertToString(Check_NULLALL(txtcompany_email.Text)) <> "", "E-mail: " & txtcompany_email.Text & " ", "") &
                                        IIf(ConvertToString(Check_NULLALL(txtob_address.Text)) <> "", "ON BEHALF OF " & txtob_address.Text, "") & IIf(Check_NULLALL(txtNewEmail_ch01.Text) <> "", " E-mail: " & txtNewEmail_ch01.Text, "")
                ElseIf Mid(txtdest_remark.Text, 2, 1) <> "/" Then
                    StrtxtRerutn = ""
                    'StrtxtRerutn = txtcompany_name.Text & " " & txtcompany_address.Text & " " & txtcompany_province.Text _
                    '                    & " " & txtcompany_country.Text & IIf(ConvertToString(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") & _
                    '                    IIf(ConvertToString(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") _
                    '                    & IIf(ConvertToString(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID: " & txtcompany_taxno.Text, "") & _
                    '                    IIf(ConvertToString(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")

                End If
            Case "FORM1_3"
                If Mid(txtdest_remark.Text, 1, 3) = "C/O" Then
                    StrtxtRerutn = txtob_address.Text & IIf(Check_NULLALL(txtNewEmail_ch01.Text) <> "", " E-mail: " & txtNewEmail_ch01.Text, "") & " CARE OF " & txtcompany_name.Text & " " & txtcompany_address.Text _
                                       & " " & txtcompany_province.Text & " " & txtcompany_country.Text &
                                       IIf(ConvertToString(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") &
                                       IIf(ConvertToString(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") &
                                       IIf(ConvertToString(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID:  " & txtcompany_taxno.Text, "") &
                                       IIf(ConvertToString(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")
                ElseIf Mid(txtdest_remark.Text, 1, 3) = "O/B" Then
                    StrtxtRerutn = IIf(ConvertToString(Check_NULLALL(txtcompany_email.Text)) <> "", "E-mail: " & txtcompany_email.Text & " ", "") &
                                        IIf(ConvertToString(Check_NULLALL(txtob_address.Text)) <> "", "ON BEHALF OF " & txtob_address.Text, "") & IIf(Check_NULLALL(txtNewEmail_ch01.Text) <> "", " E-mail: " & txtNewEmail_ch01.Text, "")
                ElseIf Mid(txtdest_remark.Text, 2, 1) <> "/" Then
                    StrtxtRerutn = ""
                    'StrtxtRerutn = txtcompany_name.Text & " " & txtcompany_address.Text & " " & txtcompany_province.Text _
                    '                    & " " & txtcompany_country.Text & IIf(ConvertToString(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") & _
                    '                    IIf(ConvertToString(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") _
                    '                    & IIf(ConvertToString(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID: " & txtcompany_taxno.Text, "") & _
                    '                    IIf(ConvertToString(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")

                End If
            Case "FORM1_2"
                If Mid(txtdest_remark.Text, 1, 3) = "C/O" Then
                    StrtxtRerutn = txtob_address.Text & IIf(Check_NULLALL(txtNewEmail_ch01.Text) <> "", " E-mail: " & txtNewEmail_ch01.Text, "") & " CARE OF " & txtcompany_name.Text & " " & txtcompany_address.Text _
                                       & " " & txtcompany_province.Text & " " & txtcompany_country.Text &
                                       IIf(ConvertToString(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") &
                                       IIf(ConvertToString(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") &
                                       IIf(ConvertToString(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID:  " & txtcompany_taxno.Text, "") &
                                       IIf(ConvertToString(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")
                ElseIf Mid(txtdest_remark.Text, 1, 3) = "O/B" Then
                    StrtxtRerutn = IIf(ConvertToString(Check_NULLALL(txtcompany_email.Text)) <> "", "E-mail: " & txtcompany_email.Text & " ", "") &
                                        IIf(ConvertToString(Check_NULLALL(txtob_address.Text)) <> "", "ON BEHALF OF " & txtob_address.Text, "") & IIf(Check_NULLALL(txtNewEmail_ch01.Text) <> "", " E-mail: " & txtNewEmail_ch01.Text, "")
                ElseIf Mid(txtdest_remark.Text, 2, 1) <> "/" Then
                    StrtxtRerutn = ""
                    'StrtxtRerutn = txtcompany_name.Text & " " & txtcompany_address.Text & " " & txtcompany_province.Text _
                    '                    & " " & txtcompany_country.Text & IIf(ConvertToString(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") & _
                    '                    IIf(ConvertToString(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") _
                    '                    & IIf(ConvertToString(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID: " & txtcompany_taxno.Text, "") & _
                    '                    IIf(ConvertToString(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")

                End If
            Case "FORM1_1"
                If Mid(txtdest_remark.Text, 1, 3) = "C/O" Then
                    StrtxtRerutn = txtob_address.Text & IIf(Check_NULLALL(txtNewEmail_ch01.Text) <> "", " E-mail: " & txtNewEmail_ch01.Text, "") & " CARE OF " & txtcompany_name.Text & " " & txtcompany_address.Text _
                                       & " " & txtcompany_province.Text & " " & txtcompany_country.Text &
                                       IIf(ConvertToString(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") &
                                       IIf(ConvertToString(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") &
                                       IIf(ConvertToString(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID:  " & txtcompany_taxno.Text, "") &
                                       IIf(ConvertToString(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")
                ElseIf Mid(txtdest_remark.Text, 1, 3) = "O/B" Then
                    StrtxtRerutn = IIf(ConvertToString(Check_NULLALL(txtcompany_email.Text)) <> "", "E-mail: " & txtcompany_email.Text & " ", "") &
                                        IIf(ConvertToString(Check_NULLALL(txtob_address.Text)) <> "", "ON BEHALF OF " & txtob_address.Text, "") & IIf(Check_NULLALL(txtNewEmail_ch01.Text) <> "", " E-mail: " & txtNewEmail_ch01.Text, "")
                ElseIf Mid(txtdest_remark.Text, 2, 1) <> "/" Then
                    StrtxtRerutn = ""
                    'StrtxtRerutn = txtcompany_name.Text & " " & txtcompany_address.Text & " " & txtcompany_province.Text _
                    '                    & " " & txtcompany_country.Text & IIf(ConvertToString(Check_NULLALL(txtcompany_phone.Text)) <> "", " TEL: " & txtcompany_phone.Text, "") & _
                    '                    IIf(ConvertToString(Check_NULLALL(txtcompany_fax.Text)) <> "", " FAX: " & txtcompany_fax.Text, "") _
                    '                    & IIf(ConvertToString(Check_NULLALL(txtcompany_taxno.Text)) <> "", " TAX ID: " & txtcompany_taxno.Text, "") & _
                    '                    IIf(ConvertToString(Check_NULLALL(txtcompany_email.Text)) <> "", " E-mail: " & txtcompany_email.Text, "")

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
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID:  " & txtdestination_taxid.Text, "") &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                        IIf(Check_NULLALL(txtplace_Exibition.Text) <> "", " " & txtplace_Exibition.Text, "")

                ElseIf Mid(txtdest_remark1.Text, 1, 3) = "O/B" Then
                    StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " &
                                            txtdestination_province.Text & " " & txtdest_receive_country.Text &
                                            IIf(ConvertToString(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                            IIf(ConvertToString(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                            & IIf(ConvertToString(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                            IIf(ConvertToString(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                            " ON BEHALF OF " & txtob_dest_address.Text & IIf(Check_NULLALL(txtNewEmail_ch02.Text) <> "", " E-mail: " & txtNewEmail_ch02.Text, "") & IIf(Check_NULLALL(txtplace_Exibition.Text) <> "", " " & txtplace_Exibition.Text, "")

                ElseIf Mid(txtdest_remark1.Text, 2, 1) <> "/" Then
                    StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " & txtdestination_province.Text _
                                        & " " & txtdest_receive_country.Text &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                        & IIf(ConvertToString(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                        IIf(Check_NULLALL(txtplace_Exibition.Text) <> "", " " & txtplace_Exibition.Text, "")

                End If
            Case "FORM4_3"
                If Mid(txtdest_remark1.Text, 1, 3) = "C/O" Then
                    StrtxtRerutn = txtob_dest_address.Text & " CARE OF " & txtdestination_company.Text & " " & txtdestination_address.Text _
                                        & " " & txtdestination_province.Text & " " & txtdest_receive_country.Text &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID:  " & txtdestination_taxid.Text, "") &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                        IIf(Check_NULLALL(txtplace_Exibition.Text) <> "", " " & txtplace_Exibition.Text, "")

                ElseIf Mid(txtdest_remark1.Text, 1, 3) = "O/B" Then
                    StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " &
                                            txtdestination_province.Text & " " & txtdest_receive_country.Text &
                                            IIf(ConvertToString(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                            IIf(ConvertToString(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                            & IIf(ConvertToString(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                            IIf(ConvertToString(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                            " ON BEHALF OF " & txtob_dest_address.Text & IIf(Check_NULLALL(txtplace_Exibition.Text) <> "", " " & txtplace_Exibition.Text, "")

                ElseIf Mid(txtdest_remark1.Text, 2, 1) <> "/" Then
                    StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " & txtdestination_province.Text _
                                        & " " & txtdest_receive_country.Text &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                        & IIf(ConvertToString(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                        IIf(Check_NULLALL(txtplace_Exibition.Text) <> "", " " & txtplace_Exibition.Text, "")

                ElseIf Mid(txtdest_remark1.Text, 1, 5) <> "O/TOB" Then
                    StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " & txtdestination_province.Text _
                                    & " " & txtdest_receive_country.Text & " " & txtob_dest_address.Text &
                                    IIf(ConvertToString(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                    IIf(ConvertToString(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                    & IIf(ConvertToString(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                    IIf(ConvertToString(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                    IIf(Check_NULLALL(txtplace_Exibition.Text) <> "", " " & txtplace_Exibition.Text, "")

                ElseIf Mid(txtdest_remark1.Text, 1, 5) <> "O/TCO" Then
                    StrtxtRerutn = txtob_dest_address.Text & " " & txtdestination_company.Text & " " & txtdestination_address.Text & " " & txtdestination_province.Text _
                                    & " " & txtdest_receive_country.Text &
                                    IIf(ConvertToString(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                    IIf(ConvertToString(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                    & IIf(ConvertToString(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                    IIf(ConvertToString(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                    IIf(Check_NULLALL(txtplace_Exibition.Text) <> "", " " & txtplace_Exibition.Text, "")

                End If
            Case "FORM9"
                If Mid(txtdest_remark1.Text, 1, 3) = "C/O" Then
                    StrtxtRerutn = txtob_dest_address.Text & IIf(Check_NULLALL(txtNewEmail_ch02.Text) <> "", " E-mail: " & txtNewEmail_ch02.Text, "") & " CARE OF " & txtdestination_company.Text & " " & txtdestination_address.Text _
                                        & " " & txtdestination_province.Text & " " & txtdest_receive_country.Text &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID:  " & txtdestination_taxid.Text, "") &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                        IIf(Check_NULLALL(txtplace_Exibition.Text) <> "", " " & txtplace_Exibition.Text, "")

                ElseIf Mid(txtdest_remark1.Text, 1, 3) = "O/B" Then
                    StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " &
                                            txtdestination_province.Text & " " & txtdest_receive_country.Text &
                                            IIf(ConvertToString(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                            IIf(ConvertToString(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                            & IIf(ConvertToString(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                            IIf(ConvertToString(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                            " ON BEHALF OF " & txtob_dest_address.Text & IIf(Check_NULLALL(txtNewEmail_ch02.Text) <> "", " E-mail: " & txtNewEmail_ch02.Text, "") & IIf(Check_NULLALL(txtplace_Exibition.Text) <> "", " " & txtplace_Exibition.Text, "")

                ElseIf Mid(txtdest_remark1.Text, 2, 1) <> "/" Then
                    StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " & txtdestination_province.Text _
                                        & " " & txtdest_receive_country.Text &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                        & IIf(ConvertToString(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                        IIf(Check_NULLALL(txtplace_Exibition.Text) <> "", " " & txtplace_Exibition.Text, "")

                End If
            Case "FORM8"
                If Mid(txtdest_remark1.Text, 1, 3) = "C/O" Then
                    StrtxtRerutn = txtob_dest_address.Text & IIf(Check_NULLALL(txtNewEmail_ch02.Text) <> "", " E-mail: " & txtNewEmail_ch02.Text, "") & " CARE OF " & txtdestination_company.Text & " " & txtdestination_address.Text _
                                        & " " & txtdestination_province.Text & " " & txtdest_receive_country.Text &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID:  " & txtdestination_taxid.Text, "") &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                        IIf(Check_NULLALL(txtplace_Exibition.Text) <> "", " " & txtplace_Exibition.Text, "")

                ElseIf Mid(txtdest_remark1.Text, 1, 3) = "O/B" Then
                    StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " &
                                            txtdestination_province.Text & " " & txtdest_receive_country.Text &
                                            IIf(ConvertToString(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                            IIf(ConvertToString(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                            & IIf(ConvertToString(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                            IIf(ConvertToString(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                            " ON BEHALF OF " & txtob_dest_address.Text & IIf(Check_NULLALL(txtNewEmail_ch02.Text) <> "", " E-mail: " & txtNewEmail_ch02.Text, "") & IIf(Check_NULLALL(txtplace_Exibition.Text) <> "", " " & txtplace_Exibition.Text, "")

                ElseIf Mid(txtdest_remark1.Text, 2, 1) <> "/" Then
                    StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " & txtdestination_province.Text _
                                        & " " & txtdest_receive_country.Text &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                        & IIf(ConvertToString(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                        IIf(Check_NULLALL(txtplace_Exibition.Text) <> "", " " & txtplace_Exibition.Text, "")

                End If
            Case "FORM7"
                If Mid(txtdest_remark1.Text, 1, 3) = "C/O" Then
                    StrtxtRerutn = txtob_dest_address.Text & IIf(Check_NULLALL(txtNewEmail_ch02.Text) <> "", " E-mail: " & txtNewEmail_ch02.Text, "") & " CARE OF " & txtdestination_company.Text & " " & txtdestination_address.Text _
                                        & " " & txtdestination_province.Text & " " & txtdest_receive_country.Text &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID:  " & txtdestination_taxid.Text, "") &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                        IIf(Check_NULLALL(txtplace_Exibition.Text) <> "", " " & txtplace_Exibition.Text, "")

                ElseIf Mid(txtdest_remark1.Text, 1, 3) = "O/B" Then
                    StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " &
                                            txtdestination_province.Text & " " & txtdest_receive_country.Text &
                                            IIf(ConvertToString(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                            IIf(ConvertToString(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                            & IIf(ConvertToString(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                            IIf(ConvertToString(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                            " ON BEHALF OF " & txtob_dest_address.Text & IIf(Check_NULLALL(txtNewEmail_ch02.Text) <> "", " E-mail: " & txtNewEmail_ch02.Text, "") & IIf(Check_NULLALL(txtplace_Exibition.Text) <> "", " " & txtplace_Exibition.Text, "")

                ElseIf Mid(txtdest_remark1.Text, 2, 1) <> "/" Then
                    StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " & txtdestination_province.Text _
                                        & " " & txtdest_receive_country.Text &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                        & IIf(ConvertToString(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                        IIf(Check_NULLALL(txtplace_Exibition.Text) <> "", " " & txtplace_Exibition.Text, "")

                End If
            Case "FORM6"
                If Mid(txtdest_remark1.Text, 1, 3) = "C/O" Then
                    StrtxtRerutn = txtob_dest_address.Text & IIf(txtNewEmail_ch02.Text <> "", " E-mail: " & txtNewEmail_ch02.Text, "") & " CARE OF " & txtdestination_company.Text & " " & txtdestination_address.Text _
                                        & " " & txtdestination_province.Text & " " & txtdest_receive_country.Text &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID:  " & txtdestination_taxid.Text, "") &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                        IIf(Check_NULLALL(txtplace_Exibition.Text) <> "", " " & txtplace_Exibition.Text, "")

                ElseIf Mid(txtdest_remark1.Text, 1, 3) = "O/B" Then
                    StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " &
                                            txtdestination_province.Text & " " & txtdest_receive_country.Text &
                                            IIf(ConvertToString(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                            IIf(ConvertToString(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                            & IIf(ConvertToString(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                            IIf(ConvertToString(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                            " ON BEHALF OF " & txtob_dest_address.Text & IIf(Check_NULLALL(txtNewEmail_ch02.Text) <> "", " E-mail: " & txtNewEmail_ch02.Text, "") & IIf(Check_NULLALL(txtplace_Exibition.Text) <> "", " " & txtplace_Exibition.Text, "")

                ElseIf Mid(txtdest_remark1.Text, 2, 1) <> "/" Then
                    StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " & txtdestination_province.Text _
                                        & " " & txtdest_receive_country.Text &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                        & IIf(ConvertToString(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                        IIf(Check_NULLALL(txtplace_Exibition.Text) <> "", " " & txtplace_Exibition.Text, "")

                End If
            Case "FORM5", "FORM5_ESS"
                If Mid(txtdest_remark1.Text, 1, 3) = "C/O" Then
                    StrtxtRerutn = txtob_dest_address.Text & " CARE OF " & txtdestination_company.Text & " " & txtdestination_address.Text _
                                        & " " & txtdestination_province.Text & " " & txtdest_receive_country.Text &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID:  " & txtdestination_taxid.Text, "") &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                        IIf(Check_NULLALL(txtplace_Exibition.Text) <> "", " " & txtplace_Exibition.Text, "")

                ElseIf Mid(txtdest_remark1.Text, 1, 3) = "O/B" Then
                    StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " &
                                            txtdestination_province.Text & " " & txtdest_receive_country.Text &
                                            IIf(ConvertToString(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                            IIf(ConvertToString(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                            & IIf(ConvertToString(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                            IIf(ConvertToString(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                            " ON BEHALF OF " & txtob_dest_address.Text & IIf(Check_NULLALL(txtplace_Exibition.Text) <> "", " " & txtplace_Exibition.Text, "")

                ElseIf Mid(txtdest_remark1.Text, 2, 1) <> "/" Then
                    StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " & txtdestination_province.Text _
                                        & " " & txtdest_receive_country.Text &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                        & IIf(ConvertToString(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                        IIf(Check_NULLALL(txtplace_Exibition.Text) <> "", " " & txtplace_Exibition.Text, "")

                End If
            Case "FORM5_1"
                If Mid(txtdest_remark1.Text, 1, 3) = "C/O" Then
                    StrtxtRerutn = txtob_dest_address.Text & " CARE OF " & txtdestination_company.Text & " " & txtdestination_address.Text _
                                        & " " & txtdestination_province.Text & " " & txtdest_receive_country.Text &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID:  " & txtdestination_taxid.Text, "") &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                        IIf(Check_NULLALL(txtplace_Exibition.Text) <> "", " " & txtplace_Exibition.Text, "")

                ElseIf Mid(txtdest_remark1.Text, 1, 3) = "O/B" Then
                    StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " &
                                            txtdestination_province.Text & " " & txtdest_receive_country.Text &
                                            IIf(ConvertToString(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                            IIf(ConvertToString(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                            & IIf(ConvertToString(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                            IIf(ConvertToString(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                            " ON BEHALF OF " & txtob_dest_address.Text & IIf(Check_NULLALL(txtplace_Exibition.Text) <> "", " " & txtplace_Exibition.Text, "")

                ElseIf Mid(txtdest_remark1.Text, 2, 1) <> "/" Then
                    StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " & txtdestination_province.Text _
                                        & " " & txtdest_receive_country.Text &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                        & IIf(ConvertToString(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                        IIf(Check_NULLALL(txtplace_Exibition.Text) <> "", " " & txtplace_Exibition.Text, "")

                ElseIf Mid(txtdest_remark1.Text, 1, 5) = "O/TOB" Then
                    StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " & txtdestination_province.Text _
                                    & " " & txtdest_receive_country.Text &
                                    IIf(ConvertToString(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                    IIf(ConvertToString(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                    & IIf(ConvertToString(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                    IIf(ConvertToString(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                    IIf(Check_NULLALL(txtplace_Exibition.Text) <> "", " " & txtplace_Exibition.Text, "") & " " & txtob_dest_address.Text

                ElseIf Mid(txtdest_remark1.Text, 1, 5) = "O/TCO" Then
                    StrtxtRerutn = txtob_dest_address.Text & " " & txtdestination_company.Text & " " & txtdestination_address.Text & " " & txtdestination_province.Text _
                                    & " " & txtdest_receive_country.Text &
                                    IIf(ConvertToString(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                    IIf(ConvertToString(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                    & IIf(ConvertToString(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                    IIf(ConvertToString(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                    IIf(Check_NULLALL(txtplace_Exibition.Text) <> "", " " & txtplace_Exibition.Text, "")

                End If
            Case "FORM5_2"
                If Mid(txtdest_remark1.Text, 1, 3) = "C/O" Then
                    StrtxtRerutn = txtob_dest_address.Text & " CARE OF " & txtdestination_company.Text & " " & txtdestination_address.Text _
                                        & " " & txtdestination_province.Text & " " & txtdest_receive_country.Text &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID:  " & txtdestination_taxid.Text, "") &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                        IIf(Check_NULLALL(txtplace_Exibition.Text) <> "", " " & txtplace_Exibition.Text, "")

                ElseIf Mid(txtdest_remark1.Text, 1, 3) = "O/B" Then
                    StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " &
                                            txtdestination_province.Text & " " & txtdest_receive_country.Text &
                                            IIf(ConvertToString(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                            IIf(ConvertToString(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                            & IIf(ConvertToString(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                            IIf(ConvertToString(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                            " ON BEHALF OF " & txtob_dest_address.Text & IIf(Check_NULLALL(txtplace_Exibition.Text) <> "", " " & txtplace_Exibition.Text, "")

                ElseIf Mid(txtdest_remark1.Text, 2, 1) <> "/" Then
                    StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " & txtdestination_province.Text _
                                        & " " & txtdest_receive_country.Text &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                        & IIf(ConvertToString(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                        IIf(Check_NULLALL(txtplace_Exibition.Text) <> "", " " & txtplace_Exibition.Text, "")

                ElseIf Mid(txtdest_remark1.Text, 1, 5) <> "O/TOB" Then
                    StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " & txtdestination_province.Text _
                                    & " " & txtdest_receive_country.Text & " " & txtob_dest_address.Text &
                                    IIf(ConvertToString(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                    IIf(ConvertToString(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                    & IIf(ConvertToString(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                    IIf(ConvertToString(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                    IIf(Check_NULLALL(txtplace_Exibition.Text) <> "", " " & txtplace_Exibition.Text, "")

                ElseIf Mid(txtdest_remark1.Text, 1, 5) <> "O/TCO" Then
                    StrtxtRerutn = txtob_dest_address.Text & " " & txtdestination_company.Text & " " & txtdestination_address.Text & " " & txtdestination_province.Text _
                                    & " " & txtdest_receive_country.Text &
                                    IIf(ConvertToString(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                    IIf(ConvertToString(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                    & IIf(ConvertToString(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                    IIf(ConvertToString(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                    IIf(Check_NULLALL(txtplace_Exibition.Text) <> "", " " & txtplace_Exibition.Text, "")

                End If
            Case "FORM441", "FORM441_4"
                If Mid(txtdest_remark1.Text, 1, 3) = "C/O" Then
                    StrtxtRerutn = txtob_dest_address.Text & " CARE OF " & txtdestination_company.Text & " " & txtdestination_address.Text _
                                        & " " & txtdestination_province.Text & " " & txtdest_receive_country.Text &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID:  " & txtdestination_taxid.Text, "") &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                        IIf(Check_NULLALL(txtplace_Exibition.Text) <> "", " " & txtplace_Exibition.Text, "")

                ElseIf Mid(txtdest_remark1.Text, 1, 3) = "O/B" Then
                    StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " &
                                            txtdestination_province.Text & " " & txtdest_receive_country.Text &
                                            IIf(ConvertToString(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                            IIf(ConvertToString(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                            & IIf(ConvertToString(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                            IIf(ConvertToString(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                            " ON BEHALF OF " & txtob_dest_address.Text & IIf(Check_NULLALL(txtplace_Exibition.Text) <> "", " " & txtplace_Exibition.Text, "")

                ElseIf Mid(txtdest_remark1.Text, 2, 1) <> "/" Then
                    StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " & txtdestination_province.Text _
                                        & " " & txtdest_receive_country.Text &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                        & IIf(ConvertToString(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                        IIf(Check_NULLALL(txtplace_Exibition.Text) <> "", " " & txtplace_Exibition.Text, "")

                ElseIf Mid(txtdest_remark1.Text, 1, 5) <> "O/TOB" Then
                    StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " & txtdestination_province.Text _
                                    & " " & txtdest_receive_country.Text & " " & txtob_dest_address.Text &
                                    IIf(ConvertToString(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                    IIf(ConvertToString(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                    & IIf(ConvertToString(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                    IIf(ConvertToString(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                    IIf(Check_NULLALL(txtplace_Exibition.Text) <> "", " " & txtplace_Exibition.Text, "")

                ElseIf Mid(txtdest_remark1.Text, 1, 5) <> "O/TCO" Then
                    StrtxtRerutn = txtob_dest_address.Text & " " & txtdestination_company.Text & " " & txtdestination_address.Text & " " & txtdestination_province.Text _
                                    & " " & txtdest_receive_country.Text &
                                    IIf(ConvertToString(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                    IIf(ConvertToString(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                    & IIf(ConvertToString(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                    IIf(ConvertToString(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                    IIf(Check_NULLALL(txtplace_Exibition.Text) <> "", " " & txtplace_Exibition.Text, "")

                End If
            Case "FORM44", "FORM44_44", "FORM44_02", "FORMD_ESS_", "FORMD_ESS_ATTS"
                If Mid(txtdest_remark1.Text, 1, 3) = "C/O" Then
                    StrtxtRerutn = txtob_dest_address.Text & " CARE OF " & txtdestination_company.Text & " " & txtdestination_address.Text _
                                        & " " & txtdestination_province.Text & " " & txtdest_receive_country.Text &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID:  " & txtdestination_taxid.Text, "") &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                        IIf(Check_NULLALL(txtplace_Exibition.Text) <> "", " " & txtplace_Exibition.Text, "")

                ElseIf Mid(txtdest_remark1.Text, 1, 3) = "O/B" Then
                    StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " &
                                            txtdestination_province.Text & " " & txtdest_receive_country.Text &
                                            IIf(ConvertToString(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                            IIf(ConvertToString(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                            & IIf(ConvertToString(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                            IIf(ConvertToString(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                            " ON BEHALF OF " & txtob_dest_address.Text & IIf(Check_NULLALL(txtplace_Exibition.Text) <> "", " " & txtplace_Exibition.Text, "")

                ElseIf Mid(txtdest_remark1.Text, 2, 1) <> "/" Then
                    StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " & txtdestination_province.Text _
                                        & " " & txtdest_receive_country.Text &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                        & IIf(ConvertToString(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                        IIf(Check_NULLALL(txtplace_Exibition.Text) <> "", " " & txtplace_Exibition.Text, "")

                ElseIf Mid(txtdest_remark1.Text, 1, 5) <> "O/TOB" Then
                    StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " & txtdestination_province.Text _
                                    & " " & txtdest_receive_country.Text & " " & txtob_dest_address.Text &
                                    IIf(ConvertToString(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                    IIf(ConvertToString(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                    & IIf(ConvertToString(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                    IIf(ConvertToString(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                    IIf(Check_NULLALL(txtplace_Exibition.Text) <> "", " " & txtplace_Exibition.Text, "")

                ElseIf Mid(txtdest_remark1.Text, 1, 5) <> "O/TCO" Then
                    StrtxtRerutn = txtob_dest_address.Text & " " & txtdestination_company.Text & " " & txtdestination_address.Text & " " & txtdestination_province.Text _
                                    & " " & txtdest_receive_country.Text &
                                    IIf(ConvertToString(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                    IIf(ConvertToString(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                    & IIf(ConvertToString(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                    IIf(ConvertToString(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                    IIf(Check_NULLALL(txtplace_Exibition.Text) <> "", " " & txtplace_Exibition.Text, "")

                End If
            Case "FORM4", "FORM44_4", "FORM44_01", "FORMRCEP", "FORMAHK", "FORME_01", "FORMD_ESS"
                If Mid(txtdest_remark1.Text, 1, 3) = "C/O" Then
                    StrtxtRerutn = txtob_dest_address.Text & " CARE OF " & txtdestination_company.Text & " " & txtdestination_address.Text _
                                        & " " & txtdestination_province.Text & " " & txtdest_receive_country.Text &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID:  " & txtdestination_taxid.Text, "") &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                        IIf(Check_NULLALL(txtplace_Exibition.Text) <> "", " " & txtplace_Exibition.Text, "")

                ElseIf Mid(txtdest_remark1.Text, 1, 3) = "O/B" Then
                    StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " &
                                            txtdestination_province.Text & " " & txtdest_receive_country.Text &
                                            IIf(ConvertToString(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                            IIf(ConvertToString(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                            & IIf(ConvertToString(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                            IIf(ConvertToString(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                            " ON BEHALF OF " & txtob_dest_address.Text & IIf(Check_NULLALL(txtplace_Exibition.Text) <> "", " " & txtplace_Exibition.Text, "")

                ElseIf Mid(txtdest_remark1.Text, 2, 1) <> "/" Then
                    StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " & txtdestination_province.Text _
                                        & " " & txtdest_receive_country.Text &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                        & IIf(ConvertToString(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                        IIf(Check_NULLALL(txtplace_Exibition.Text) <> "", " " & txtplace_Exibition.Text, "")

                ElseIf Mid(txtdest_remark1.Text, 1, 5) <> "O/TOB" Then
                    StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " & txtdestination_province.Text _
                                    & " " & txtdest_receive_country.Text & " " & txtob_dest_address.Text &
                                    IIf(ConvertToString(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                    IIf(ConvertToString(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                    & IIf(ConvertToString(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                    IIf(ConvertToString(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                    IIf(Check_NULLALL(txtplace_Exibition.Text) <> "", " " & txtplace_Exibition.Text, "")

                ElseIf Mid(txtdest_remark1.Text, 1, 5) <> "O/TCO" Then
                    StrtxtRerutn = txtob_dest_address.Text & " " & txtdestination_company.Text & " " & txtdestination_address.Text & " " & txtdestination_province.Text _
                                    & " " & txtdest_receive_country.Text &
                                    IIf(ConvertToString(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                    IIf(ConvertToString(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                    & IIf(ConvertToString(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                    IIf(ConvertToString(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                    IIf(Check_NULLALL(txtplace_Exibition.Text) <> "", " " & txtplace_Exibition.Text, "")

                End If
            Case "FORM4_91", "FORM4_911", "FORMAI_ESS"
                Select Case ConvertToString(txtCheck_StatusWeb.Text)
                    Case Is <> ""
                        If Mid(txtdest_remark1.Text, 1, 3) = "C/O" Then
                            StrtxtRerutn = txtob_dest_address.Text & " CARE OF " & txtdestination_company.Text & " " & txtdestination_address.Text _
                                                & " " & txtdestination_province.Text & " " & txtdest_receive_country.Text &
                                                IIf(ConvertToString(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                                IIf(ConvertToString(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") &
                                                IIf(ConvertToString(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID:  " & txtdestination_taxid.Text, "") &
                                                IIf(ConvertToString(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                                IIf(Check_NULLALL(txtplace_Exibition.Text) <> "", " " & txtplace_Exibition.Text, "")

                        ElseIf Mid(txtdest_remark1.Text, 1, 3) = "O/B" Then
                            StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " &
                                                    txtdestination_province.Text & " " & txtdest_receive_country.Text &
                                                    IIf(ConvertToString(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                                    IIf(ConvertToString(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                                    & IIf(ConvertToString(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                                    IIf(ConvertToString(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                                    " ON BEHALF OF " & txtob_dest_address.Text & IIf(Check_NULLALL(txtplace_Exibition.Text) <> "", " " & txtplace_Exibition.Text, "")

                        ElseIf Mid(txtdest_remark1.Text, 2, 1) <> "/" Then
                            StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " & txtdestination_province.Text _
                                                & " " & txtdest_receive_country.Text &
                                                IIf(ConvertToString(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                                IIf(ConvertToString(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                                & IIf(ConvertToString(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                                IIf(ConvertToString(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                                IIf(Check_NULLALL(txtplace_Exibition.Text) <> "", " " & txtplace_Exibition.Text, "")

                        ElseIf Mid(txtdest_remark1.Text, 1, 5) <> "O/TOB" Then
                            StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " & txtdestination_province.Text _
                                            & " " & txtdest_receive_country.Text & " " & txtob_dest_address.Text &
                                            IIf(ConvertToString(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                            IIf(ConvertToString(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                            & IIf(ConvertToString(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                            IIf(ConvertToString(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                            IIf(Check_NULLALL(txtplace_Exibition.Text) <> "", " " & txtplace_Exibition.Text, "")

                        ElseIf Mid(txtdest_remark1.Text, 1, 5) <> "O/TCO" Then
                            StrtxtRerutn = txtob_dest_address.Text & " " & txtdestination_company.Text & " " & txtdestination_address.Text & " " & txtdestination_province.Text _
                                            & " " & txtdest_receive_country.Text &
                                            IIf(ConvertToString(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                            IIf(ConvertToString(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                            & IIf(ConvertToString(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                            IIf(ConvertToString(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                            IIf(Check_NULLALL(txtplace_Exibition.Text) <> "", " " & txtplace_Exibition.Text, "")

                        End If
                    Case Is = "" 'ระบบเก่า
                        If Mid(txtdest_remark1.Text, 1, 3) = "C/O" Then
                            StrtxtRerutn = txtob_dest_address.Text & " CARE OF " & txtdestination_company.Text & " " & txtdestination_address.Text _
                                                & " " & txtdestination_province.Text & " " & txtdest_receive_country.Text &
                                                IIf(ConvertToString(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                                IIf(ConvertToString(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") &
                                                IIf(ConvertToString(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID:  " & txtdestination_taxid.Text, "") &
                                                IIf(ConvertToString(Check_NULLALL(txtdeclare_Remark.Text)) <> "", " E-mail: " & txtdeclare_Remark.Text, "") &
                                                IIf(Check_NULLALL(txtplace_Exibition.Text) <> "", " " & txtplace_Exibition.Text, "")

                        ElseIf Mid(txtdest_remark1.Text, 1, 3) = "O/B" Then
                            StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " &
                                                    txtdestination_province.Text & " " & txtdest_receive_country.Text &
                                                    IIf(ConvertToString(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                                    IIf(ConvertToString(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                                    & IIf(ConvertToString(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                                    IIf(ConvertToString(Check_NULLALL(txtdeclare_Remark.Text)) <> "", " E-mail: " & txtdeclare_Remark.Text, "") &
                                                    " ON BEHALF OF " & txtob_dest_address.Text & IIf(Check_NULLALL(txtplace_Exibition.Text) <> "", " " & txtplace_Exibition.Text, "")

                        ElseIf Mid(txtdest_remark1.Text, 2, 1) <> "/" Then
                            StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " & txtdestination_province.Text _
                                                & " " & txtdest_receive_country.Text &
                                                IIf(ConvertToString(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                                IIf(ConvertToString(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                                & IIf(ConvertToString(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                                IIf(ConvertToString(Check_NULLALL(txtdeclare_Remark.Text)) <> "", " E-mail: " & txtdeclare_Remark.Text, "") &
                                                IIf(Check_NULLALL(txtplace_Exibition.Text) <> "", " " & txtplace_Exibition.Text, "")

                        ElseIf Mid(txtdest_remark1.Text, 1, 5) <> "O/TOB" Then
                            StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " & txtdestination_province.Text _
                                            & " " & txtdest_receive_country.Text & " " & txtob_dest_address.Text &
                                            IIf(ConvertToString(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                            IIf(ConvertToString(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                            & IIf(ConvertToString(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                            IIf(ConvertToString(Check_NULLALL(txtdeclare_Remark.Text)) <> "", " E-mail: " & txtdeclare_Remark.Text, "") &
                                            IIf(Check_NULLALL(txtplace_Exibition.Text) <> "", " " & txtplace_Exibition.Text, "")

                        ElseIf Mid(txtdest_remark1.Text, 1, 5) <> "O/TCO" Then
                            StrtxtRerutn = txtob_dest_address.Text & " " & txtdestination_company.Text & " " & txtdestination_address.Text & " " & txtdestination_province.Text _
                                            & " " & txtdest_receive_country.Text &
                                            IIf(ConvertToString(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                            IIf(ConvertToString(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                            & IIf(ConvertToString(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                            IIf(ConvertToString(Check_NULLALL(txtdeclare_Remark.Text)) <> "", " E-mail: " & txtdeclare_Remark.Text, "") &
                                            IIf(Check_NULLALL(txtplace_Exibition.Text) <> "", " " & txtplace_Exibition.Text, "")

                        End If
                End Select
            Case "FORM4_9", "FORMAI_ESS"
                Select Case ConvertToString(txtCheck_StatusWeb.Text)
                    Case Is <> ""
                        If Mid(txtdest_remark1.Text, 1, 3) = "C/O" Then
                            StrtxtRerutn = txtob_dest_address.Text & " CARE OF " & txtdestination_company.Text & " " & txtdestination_address.Text _
                                                & " " & txtdestination_province.Text & " " & txtdest_receive_country.Text &
                                                IIf(ConvertToString(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                                IIf(ConvertToString(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") &
                                                IIf(ConvertToString(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID:  " & txtdestination_taxid.Text, "") &
                                                IIf(ConvertToString(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                                IIf(Check_NULLALL(txtplace_Exibition.Text) <> "", " " & txtplace_Exibition.Text, "")

                        ElseIf Mid(txtdest_remark1.Text, 1, 3) = "O/B" Then
                            StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " &
                                                    txtdestination_province.Text & " " & txtdest_receive_country.Text &
                                                    IIf(ConvertToString(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                                    IIf(ConvertToString(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                                    & IIf(ConvertToString(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                                    IIf(ConvertToString(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                                    " ON BEHALF OF " & txtob_dest_address.Text & IIf(Check_NULLALL(txtplace_Exibition.Text) <> "", " " & txtplace_Exibition.Text, "")

                        ElseIf Mid(txtdest_remark1.Text, 2, 1) <> "/" Then
                            StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " & txtdestination_province.Text _
                                                & " " & txtdest_receive_country.Text &
                                                IIf(ConvertToString(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                                IIf(ConvertToString(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                                & IIf(ConvertToString(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                                IIf(ConvertToString(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                                IIf(Check_NULLALL(txtplace_Exibition.Text) <> "", " " & txtplace_Exibition.Text, "")

                        ElseIf Mid(txtdest_remark1.Text, 1, 5) <> "O/TOB" Then
                            StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " & txtdestination_province.Text _
                                            & " " & txtdest_receive_country.Text & " " & txtob_dest_address.Text &
                                            IIf(ConvertToString(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                            IIf(ConvertToString(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                            & IIf(ConvertToString(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                            IIf(ConvertToString(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                            IIf(Check_NULLALL(txtplace_Exibition.Text) <> "", " " & txtplace_Exibition.Text, "")

                        ElseIf Mid(txtdest_remark1.Text, 1, 5) <> "O/TCO" Then
                            StrtxtRerutn = txtob_dest_address.Text & " " & txtdestination_company.Text & " " & txtdestination_address.Text & " " & txtdestination_province.Text _
                                            & " " & txtdest_receive_country.Text &
                                            IIf(ConvertToString(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                            IIf(ConvertToString(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                            & IIf(ConvertToString(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                            IIf(ConvertToString(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                            IIf(Check_NULLALL(txtplace_Exibition.Text) <> "", " " & txtplace_Exibition.Text, "")

                        End If
                    Case Is = "" 'ระบบเก่า
                        If Mid(txtdest_remark1.Text, 1, 3) = "C/O" Then
                            StrtxtRerutn = txtob_dest_address.Text & " CARE OF " & txtdestination_company.Text & " " & txtdestination_address.Text _
                                                & " " & txtdestination_province.Text & " " & txtdest_receive_country.Text &
                                                IIf(ConvertToString(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                                IIf(ConvertToString(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") &
                                                IIf(ConvertToString(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID:  " & txtdestination_taxid.Text, "") &
                                                IIf(ConvertToString(Check_NULLALL(txtdeclare_Remark.Text)) <> "", " E-mail: " & txtdeclare_Remark.Text, "") &
                                                IIf(Check_NULLALL(txtplace_Exibition.Text) <> "", " " & txtplace_Exibition.Text, "")

                        ElseIf Mid(txtdest_remark1.Text, 1, 3) = "O/B" Then
                            StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " &
                                                    txtdestination_province.Text & " " & txtdest_receive_country.Text &
                                                    IIf(ConvertToString(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                                    IIf(ConvertToString(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                                    & IIf(ConvertToString(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                                    IIf(ConvertToString(Check_NULLALL(txtdeclare_Remark.Text)) <> "", " E-mail: " & txtdeclare_Remark.Text, "") &
                                                    " ON BEHALF OF " & txtob_dest_address.Text & IIf(Check_NULLALL(txtplace_Exibition.Text) <> "", " " & txtplace_Exibition.Text, "")

                        ElseIf Mid(txtdest_remark1.Text, 2, 1) <> "/" Then
                            StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " & txtdestination_province.Text _
                                                & " " & txtdest_receive_country.Text &
                                                IIf(ConvertToString(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                                IIf(ConvertToString(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                                & IIf(ConvertToString(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                                IIf(ConvertToString(Check_NULLALL(txtdeclare_Remark.Text)) <> "", " E-mail: " & txtdeclare_Remark.Text, "") &
                                                IIf(Check_NULLALL(txtplace_Exibition.Text) <> "", " " & txtplace_Exibition.Text, "")

                        ElseIf Mid(txtdest_remark1.Text, 1, 5) <> "O/TOB" Then
                            StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " & txtdestination_province.Text _
                                            & " " & txtdest_receive_country.Text & " " & txtob_dest_address.Text &
                                            IIf(ConvertToString(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                            IIf(ConvertToString(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                            & IIf(ConvertToString(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                            IIf(ConvertToString(Check_NULLALL(txtdeclare_Remark.Text)) <> "", " E-mail: " & txtdeclare_Remark.Text, "") &
                                            IIf(Check_NULLALL(txtplace_Exibition.Text) <> "", " " & txtplace_Exibition.Text, "")

                        ElseIf Mid(txtdest_remark1.Text, 1, 5) <> "O/TCO" Then
                            StrtxtRerutn = txtob_dest_address.Text & " " & txtdestination_company.Text & " " & txtdestination_address.Text & " " & txtdestination_province.Text _
                                            & " " & txtdest_receive_country.Text &
                                            IIf(ConvertToString(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                            IIf(ConvertToString(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                            & IIf(ConvertToString(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                            IIf(ConvertToString(Check_NULLALL(txtdeclare_Remark.Text)) <> "", " E-mail: " & txtdeclare_Remark.Text, "") &
                                            IIf(Check_NULLALL(txtplace_Exibition.Text) <> "", " " & txtplace_Exibition.Text, "")

                        End If
                End Select
            Case "FORM4_8", "FORM4_81"
                If Mid(txtdest_remark1.Text, 1, 3) = "C/O" Then
                    StrtxtRerutn = txtob_dest_address.Text & " CARE OF " & txtdestination_company.Text & " " & txtdestination_address.Text _
                                        & " " & txtdestination_province.Text & " " & txtdest_receive_country.Text &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID:  " & txtdestination_taxid.Text, "") &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                        IIf(Check_NULLALL(txtplace_Exibition.Text) <> "", " " & txtplace_Exibition.Text, "")

                ElseIf Mid(txtdest_remark1.Text, 1, 3) = "O/B" Then
                    StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " &
                                            txtdestination_province.Text & " " & txtdest_receive_country.Text &
                                            IIf(ConvertToString(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                            IIf(ConvertToString(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                            & IIf(ConvertToString(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                            IIf(ConvertToString(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                            " ON BEHALF OF " & txtob_dest_address.Text & IIf(Check_NULLALL(txtplace_Exibition.Text) <> "", " " & txtplace_Exibition.Text, "")

                ElseIf Mid(txtdest_remark1.Text, 2, 1) <> "/" Then
                    StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " & txtdestination_province.Text _
                                        & " " & txtdest_receive_country.Text &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                        & IIf(ConvertToString(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                        IIf(Check_NULLALL(txtplace_Exibition.Text) <> "", " " & txtplace_Exibition.Text, "")

                ElseIf Mid(txtdest_remark1.Text, 1, 5) <> "O/TOB" Then
                    StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " & txtdestination_province.Text _
                                    & " " & txtdest_receive_country.Text & " " & txtob_dest_address.Text &
                                    IIf(ConvertToString(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                    IIf(ConvertToString(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                    & IIf(ConvertToString(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                    IIf(ConvertToString(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                    IIf(Check_NULLALL(txtplace_Exibition.Text) <> "", " " & txtplace_Exibition.Text, "")

                ElseIf Mid(txtdest_remark1.Text, 1, 5) <> "O/TCO" Then
                    StrtxtRerutn = txtob_dest_address.Text & " " & txtdestination_company.Text & " " & txtdestination_address.Text & " " & txtdestination_province.Text _
                                    & " " & txtdest_receive_country.Text &
                                    IIf(ConvertToString(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                    IIf(ConvertToString(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                    & IIf(ConvertToString(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                    IIf(ConvertToString(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                    IIf(Check_NULLALL(txtplace_Exibition.Text) <> "", " " & txtplace_Exibition.Text, "")

                End If
            Case "FORM4_6", "FORM4_61"
                If Mid(txtdest_remark1.Text, 1, 3) = "C/O" Then
                    StrtxtRerutn = txtob_dest_address.Text & " CARE OF " & txtdestination_company.Text & " " & txtdestination_address.Text _
                                        & " " & txtdestination_province.Text & " " & txtdest_receive_country.Text &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID:  " & txtdestination_taxid.Text, "") &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                        IIf(Check_NULLALL(txtplace_Exibition.Text) <> "", " " & txtplace_Exibition.Text, "")

                ElseIf Mid(txtdest_remark1.Text, 1, 3) = "O/B" Then
                    StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " &
                                            txtdestination_province.Text & " " & txtdest_receive_country.Text &
                                            IIf(ConvertToString(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                            IIf(ConvertToString(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                            & IIf(ConvertToString(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                            IIf(ConvertToString(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                            " ON BEHALF OF " & txtob_dest_address.Text & IIf(Check_NULLALL(txtplace_Exibition.Text) <> "", " " & txtplace_Exibition.Text, "")

                ElseIf Mid(txtdest_remark1.Text, 2, 1) <> "/" Then
                    StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " & txtdestination_province.Text _
                                        & " " & txtdest_receive_country.Text &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                        & IIf(ConvertToString(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                        IIf(Check_NULLALL(txtplace_Exibition.Text) <> "", " " & txtplace_Exibition.Text, "")

                ElseIf Mid(txtdest_remark1.Text, 1, 5) <> "O/TOB" Then
                    StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " & txtdestination_province.Text _
                                    & " " & txtdest_receive_country.Text &
                                    IIf(ConvertToString(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                    IIf(ConvertToString(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                    & IIf(ConvertToString(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                    IIf(ConvertToString(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                    IIf(Check_NULLALL(txtplace_Exibition.Text) <> "", " " & txtplace_Exibition.Text, "") & " " & txtob_dest_address.Text

                ElseIf Mid(txtdest_remark1.Text, 1, 5) <> "O/TCO" Then
                    StrtxtRerutn = txtob_dest_address.Text & " " & txtdestination_company.Text & " " & txtdestination_address.Text & " " & txtdestination_province.Text _
                                    & " " & txtdest_receive_country.Text &
                                    IIf(ConvertToString(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                    IIf(ConvertToString(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                    & IIf(ConvertToString(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                    IIf(ConvertToString(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                    IIf(Check_NULLALL(txtplace_Exibition.Text) <> "", " " & txtplace_Exibition.Text, "")

                End If
            Case "FORM4_5"
                If Mid(txtdest_remark1.Text, 1, 3) = "C/O" Then
                    StrtxtRerutn = txtob_dest_address.Text & " CARE OF " & txtdestination_company.Text & " " & txtdestination_address.Text _
                                        & " " & txtdestination_province.Text & " " & txtdest_receive_country.Text &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID:  " & txtdestination_taxid.Text, "") &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                        IIf(Check_NULLALL(txtplace_Exibition.Text) <> "", " " & txtplace_Exibition.Text, "")

                ElseIf Mid(txtdest_remark1.Text, 1, 3) = "O/B" Then
                    StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " &
                                            txtdestination_province.Text & " " & txtdest_receive_country.Text &
                                            IIf(ConvertToString(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                            IIf(ConvertToString(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                            & IIf(ConvertToString(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                            IIf(ConvertToString(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                            " ON BEHALF OF " & txtob_dest_address.Text & IIf(Check_NULLALL(txtplace_Exibition.Text) <> "", " " & txtplace_Exibition.Text, "")

                ElseIf Mid(txtdest_remark1.Text, 2, 1) <> "/" Then
                    StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " & txtdestination_province.Text _
                                        & " " & txtdest_receive_country.Text &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                        & IIf(ConvertToString(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                        IIf(Check_NULLALL(txtplace_Exibition.Text) <> "", " " & txtplace_Exibition.Text, "")

                ElseIf Mid(txtdest_remark1.Text, 1, 5) = "O/TOB" Then
                    StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " & txtdestination_province.Text _
                                    & " " & txtdest_receive_country.Text &
                                    IIf(ConvertToString(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                    IIf(ConvertToString(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                    & IIf(ConvertToString(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                    IIf(ConvertToString(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                    IIf(Check_NULLALL(txtplace_Exibition.Text) <> "", " " & txtplace_Exibition.Text, "") & " " & txtob_dest_address.Text

                ElseIf Mid(txtdest_remark1.Text, 1, 5) = "O/TCO" Then
                    StrtxtRerutn = txtob_dest_address.Text & " " & txtdestination_company.Text & " " & txtdestination_address.Text & " " & txtdestination_province.Text _
                                    & " " & txtdest_receive_country.Text &
                                    IIf(ConvertToString(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                    IIf(ConvertToString(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                    & IIf(ConvertToString(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                    IIf(ConvertToString(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                    IIf(Check_NULLALL(txtplace_Exibition.Text) <> "", " " & txtplace_Exibition.Text, "")

                End If
            Case "FORM4_4"
                If Mid(txtdest_remark1.Text, 1, 3) = "C/O" Then
                    StrtxtRerutn = txtob_dest_address.Text & " CARE OF " & txtdestination_company.Text & " " & txtdestination_address.Text _
                                        & " " & txtdestination_province.Text & " " & txtdest_receive_country.Text &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID:  " & txtdestination_taxid.Text, "") &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                        IIf(Check_NULLALL(txtplace_Exibition.Text) <> "", " " & txtplace_Exibition.Text, "")

                ElseIf Mid(txtdest_remark1.Text, 1, 3) = "O/B" Then
                    StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " &
                                            txtdestination_province.Text & " " & txtdest_receive_country.Text &
                                            IIf(ConvertToString(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                            IIf(ConvertToString(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                            & IIf(ConvertToString(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                            IIf(ConvertToString(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                            " ON BEHALF OF " & txtob_dest_address.Text & IIf(Check_NULLALL(txtplace_Exibition.Text) <> "", " " & txtplace_Exibition.Text, "")

                ElseIf Mid(txtdest_remark1.Text, 2, 1) <> "/" Then
                    StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " & txtdestination_province.Text _
                                        & " " & txtdest_receive_country.Text &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                        & IIf(ConvertToString(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                        IIf(Check_NULLALL(txtplace_Exibition.Text) <> "", " " & txtplace_Exibition.Text, "")

                ElseIf Mid(txtdest_remark1.Text, 1, 5) <> "O/TOB" Then
                    StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " & txtdestination_province.Text _
                                    & " " & txtdest_receive_country.Text & " " & txtob_dest_address.Text &
                                    IIf(ConvertToString(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                    IIf(ConvertToString(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                    & IIf(ConvertToString(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                    IIf(ConvertToString(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                    IIf(Check_NULLALL(txtplace_Exibition.Text) <> "", " " & txtplace_Exibition.Text, "")

                ElseIf Mid(txtdest_remark1.Text, 1, 5) <> "O/TCO" Then
                    StrtxtRerutn = txtob_dest_address.Text & " " & txtdestination_company.Text & " " & txtdestination_address.Text & " " & txtdestination_province.Text _
                                    & " " & txtdest_receive_country.Text &
                                    IIf(ConvertToString(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                    IIf(ConvertToString(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                    & IIf(ConvertToString(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                    IIf(ConvertToString(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                    IIf(Check_NULLALL(txtplace_Exibition.Text) <> "", " " & txtplace_Exibition.Text, "")

                End If
            Case "FORM4_2"
                StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " & txtdestination_province.Text _
                                & " " & txtdest_receive_country.Text &
                                IIf(ConvertToString(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                IIf(ConvertToString(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                & IIf(ConvertToString(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                IIf(ConvertToString(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                IIf(Check_NULLALL(txtplace_Exibition.Text) <> "", " " & txtplace_Exibition.Text, "")

            Case "FORM4_1", "FORM44_41"
                If Mid(txtdest_remark1.Text, 1, 3) = "C/O" Then
                    StrtxtRerutn = txtob_dest_address.Text & " CARE OF " & txtdestination_company.Text & " " & txtdestination_address.Text _
                                        & " " & txtdestination_province.Text & " " & txtdest_receive_country.Text &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID:  " & txtdestination_taxid.Text, "") &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                        IIf(Check_NULLALL(txtplace_Exibition.Text) <> "", " " & txtplace_Exibition.Text, "")

                ElseIf Mid(txtdest_remark1.Text, 1, 3) = "O/B" Then
                    StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " &
                                            txtdestination_province.Text & " " & txtdest_receive_country.Text &
                                            IIf(ConvertToString(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                            IIf(ConvertToString(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                            & IIf(ConvertToString(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                            IIf(ConvertToString(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                            " ON BEHALF OF " & txtob_dest_address.Text & IIf(Check_NULLALL(txtplace_Exibition.Text) <> "", " " & txtplace_Exibition.Text, "")

                ElseIf Mid(txtdest_remark1.Text, 2, 1) <> "/" Then
                    StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " & txtdestination_province.Text _
                                        & " " & txtdest_receive_country.Text &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                        & IIf(ConvertToString(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                        IIf(Check_NULLALL(txtplace_Exibition.Text) <> "", " " & txtplace_Exibition.Text, "")

                ElseIf Mid(txtdest_remark1.Text, 1, 5) <> "O/TOB" Then
                    StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " & txtdestination_province.Text _
                                    & " " & txtdest_receive_country.Text & " " & txtob_dest_address.Text &
                                    IIf(ConvertToString(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                    IIf(ConvertToString(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                    & IIf(ConvertToString(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                    IIf(ConvertToString(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                    IIf(Check_NULLALL(txtplace_Exibition.Text) <> "", " " & txtplace_Exibition.Text, "")

                ElseIf Mid(txtdest_remark1.Text, 1, 5) <> "O/TCO" Then
                    StrtxtRerutn = txtob_dest_address.Text & " " & txtdestination_company.Text & " " & txtdestination_address.Text & " " & txtdestination_province.Text _
                                    & " " & txtdest_receive_country.Text &
                                    IIf(ConvertToString(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                    IIf(ConvertToString(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                    & IIf(ConvertToString(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                    IIf(ConvertToString(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                    IIf(Check_NULLALL(txtplace_Exibition.Text) <> "", " " & txtplace_Exibition.Text, "")

                End If
            Case "FORM3_1"
                StrtxtRerutn = str_03 & ConvertToString(txtByCom_CH03.Text)
            Case "FORM3"
                If Mid(txtdest_remark1.Text, 1, 3) = "C/O" Then
                    StrtxtRerutn = str_02 & txtob_dest_address.Text & IIf(Check_NULLALL(txtNewEmail_ch02.Text) <> "", " E-mail: " & txtNewEmail_ch02.Text, "") & " CARE OF " & txtdestination_company.Text & " " & txtdestination_address.Text _
                                        & " " & txtdestination_province.Text & " " & txtdest_receive_country.Text &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID:  " & txtdestination_taxid.Text, "") &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                        IIf(Check_NULLALL(txtplace_Exibition.Text) <> "", " " & txtplace_Exibition.Text, "")

                ElseIf Mid(txtdest_remark1.Text, 1, 3) = "O/B" Then
                    StrtxtRerutn = str_02 & txtdestination_company.Text & " " & txtdestination_address.Text & " " &
                                            txtdestination_province.Text & " " & txtdest_receive_country.Text &
                                            IIf(ConvertToString(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                            IIf(ConvertToString(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                            & IIf(ConvertToString(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                            IIf(ConvertToString(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                            " ON BEHALF OF " & txtob_dest_address.Text & IIf(Check_NULLALL(txtNewEmail_ch02.Text) <> "", " E-mail: " & txtNewEmail_ch02.Text, "") & IIf(Check_NULLALL(txtplace_Exibition.Text) <> "", " " & txtplace_Exibition.Text, "")

                ElseIf Mid(txtdest_remark1.Text, 2, 1) <> "/" Then
                    StrtxtRerutn = str_02 & txtdestination_company.Text & " " & txtdestination_address.Text & " " & txtdestination_province.Text _
                                        & " " & txtdest_receive_country.Text &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                        & IIf(ConvertToString(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                        IIf(Check_NULLALL(txtplace_Exibition.Text) <> "", " " & txtplace_Exibition.Text, "")

                End If
            Case "FORM2", "FORM2_ESS"
                Select Case Check_RFC_Tax(ConvertToString(txtdestination_taxid.Text))
                    Case True 'rfc
                        If Mid(txtdest_remark1.Text, 1, 3) = "C/O" Then
                            StrtxtRerutn = txtob_dest_address.Text & IIf(Check_NULLALL(txtNewEmail_ch02.Text) <> "", " E-mail: " & txtNewEmail_ch02.Text, "") & " CARE OF " & txtdestination_company.Text & " " & txtdestination_address.Text _
                                                & " " & txtdestination_province.Text & " " & txtdest_receive_country.Text &
                                                IIf(ConvertToString(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                                IIf(ConvertToString(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") &
                                                IIf(ConvertToString(Check_NULLALL(txtdestination_taxid.Text)) <> "", " " & txtdestination_taxid.Text, "") &
                                                IIf(ConvertToString(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                                IIf(Check_NULLALL(txtplace_Exibition.Text) <> "", " " & txtplace_Exibition.Text, "")

                        ElseIf Mid(txtdest_remark1.Text, 1, 3) = "O/B" Then
                            StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " &
                                                    txtdestination_province.Text & " " & txtdest_receive_country.Text &
                                                    IIf(ConvertToString(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                                    IIf(ConvertToString(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                                    & IIf(ConvertToString(Check_NULLALL(txtdestination_taxid.Text)) <> "", " " & txtdestination_taxid.Text, "") &
                                                    IIf(ConvertToString(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                                    " ON BEHALF OF " & txtob_dest_address.Text & IIf(Check_NULLALL(txtNewEmail_ch02.Text) <> "", " E-mail: " & txtNewEmail_ch02.Text, "") & IIf(Check_NULLALL(txtplace_Exibition.Text) <> "", " " & txtplace_Exibition.Text, "")

                        ElseIf Mid(txtdest_remark1.Text, 2, 1) <> "/" Then
                            StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " & txtdestination_province.Text _
                                                & " " & txtdest_receive_country.Text &
                                                IIf(ConvertToString(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                                IIf(ConvertToString(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                                & IIf(ConvertToString(Check_NULLALL(txtdestination_taxid.Text)) <> "", " " & txtdestination_taxid.Text, "") &
                                                IIf(ConvertToString(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                                IIf(Check_NULLALL(txtplace_Exibition.Text) <> "", " " & txtplace_Exibition.Text, "")

                        End If
                    Case False 'ไม่มี rfc
                        If Mid(txtdest_remark1.Text, 1, 3) = "C/O" Then
                            StrtxtRerutn = txtob_dest_address.Text & IIf(Check_NULLALL(txtNewEmail_ch02.Text) <> "", " E-mail: " & txtNewEmail_ch02.Text, "") & " CARE OF " & txtdestination_company.Text & " " & txtdestination_address.Text _
                                                & " " & txtdestination_province.Text & " " & txtdest_receive_country.Text &
                                                IIf(ConvertToString(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                                IIf(ConvertToString(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") &
                                                IIf(ConvertToString(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                                IIf(ConvertToString(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                                IIf(Check_NULLALL(txtplace_Exibition.Text) <> "", " " & txtplace_Exibition.Text, "")

                        ElseIf Mid(txtdest_remark1.Text, 1, 3) = "O/B" Then
                            StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " &
                                                    txtdestination_province.Text & " " & txtdest_receive_country.Text &
                                                    IIf(ConvertToString(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                                    IIf(ConvertToString(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                                    & IIf(ConvertToString(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                                    IIf(ConvertToString(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                                    " ON BEHALF OF " & txtob_dest_address.Text & IIf(Check_NULLALL(txtNewEmail_ch02.Text) <> "", " E-mail: " & txtNewEmail_ch02.Text, "") & IIf(Check_NULLALL(txtplace_Exibition.Text) <> "", " " & txtplace_Exibition.Text, "")

                        ElseIf Mid(txtdest_remark1.Text, 2, 1) <> "/" Then
                            StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " & txtdestination_province.Text _
                                                & " " & txtdest_receive_country.Text &
                                                IIf(ConvertToString(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                                IIf(ConvertToString(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                                & IIf(ConvertToString(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                                IIf(ConvertToString(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                                IIf(Check_NULLALL(txtplace_Exibition.Text) <> "", " " & txtplace_Exibition.Text, "")

                        End If
                End Select
            Case "FORM2_3"
                If Mid(txtdest_remark1.Text, 1, 3) = "C/O" Then
                    StrtxtRerutn = txtob_dest_address.Text & IIf(Check_NULLALL(txtNewEmail_ch02.Text) <> "", " E-mail: " & txtNewEmail_ch02.Text, "") & " CARE OF " & txtdestination_company.Text & " " & txtdestination_address.Text _
                                        & " " & txtdestination_province.Text & " " & txtdest_receive_country.Text &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID:  " & txtdestination_taxid.Text, "") &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                        IIf(Check_NULLALL(txtplace_Exibition.Text) <> "", " " & txtplace_Exibition.Text, "")

                ElseIf Mid(txtdest_remark1.Text, 1, 3) = "O/B" Then
                    StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " &
                                            txtdestination_province.Text & " " & txtdest_receive_country.Text &
                                            IIf(ConvertToString(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                            IIf(ConvertToString(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                            & IIf(ConvertToString(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                            IIf(ConvertToString(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                            " ON BEHALF OF " & txtob_dest_address.Text & IIf(Check_NULLALL(txtNewEmail_ch02.Text) <> "", " E-mail: " & txtNewEmail_ch02.Text, "") & IIf(Check_NULLALL(txtplace_Exibition.Text) <> "", " " & txtplace_Exibition.Text, "")

                ElseIf Mid(txtdest_remark1.Text, 2, 1) <> "/" Then
                    StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " & txtdestination_province.Text _
                                        & " " & txtdest_receive_country.Text &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                        & IIf(ConvertToString(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                        IIf(Check_NULLALL(txtplace_Exibition.Text) <> "", " " & txtplace_Exibition.Text, "")

                End If
            Case "FORM2_2"
                If Mid(txtdest_remark1.Text, 1, 3) = "C/O" Then
                    StrtxtRerutn = txtob_dest_address.Text & IIf(Check_NULLALL(txtNewEmail_ch02.Text) <> "", " E-mail: " & txtNewEmail_ch02.Text, "") & " CARE OF " & txtdestination_company.Text & " " & txtdestination_address.Text _
                                        & " " & txtdestination_province.Text & " " & txtdest_receive_country.Text &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID:  " & txtdestination_taxid.Text, "") &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                        IIf(Check_NULLALL(txtplace_Exibition.Text) <> "", " " & txtplace_Exibition.Text, "")

                ElseIf Mid(txtdest_remark1.Text, 1, 3) = "O/B" Then
                    StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " &
                                            txtdestination_province.Text & " " & txtdest_receive_country.Text &
                                            IIf(ConvertToString(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                            IIf(ConvertToString(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                            & IIf(ConvertToString(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                            IIf(ConvertToString(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                            " ON BEHALF OF " & txtob_dest_address.Text & IIf(Check_NULLALL(txtNewEmail_ch02.Text) <> "", " E-mail: " & txtNewEmail_ch02.Text, "") & IIf(Check_NULLALL(txtplace_Exibition.Text) <> "", " " & txtplace_Exibition.Text, "")

                ElseIf Mid(txtdest_remark1.Text, 2, 1) <> "/" Then
                    StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " & txtdestination_province.Text _
                                        & " " & txtdest_receive_country.Text &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                        & IIf(ConvertToString(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                        IIf(Check_NULLALL(txtplace_Exibition.Text) <> "", " " & txtplace_Exibition.Text, "")

                End If
            Case "FORMRUSSIA"
                If Mid(txtdest_remark1.Text, 1, 3) = "C/O" Then
                    StrtxtRerutn = txtob_dest_address.Text & IIf(Check_NULLALL(txtNewEmail_ch02.Text) <> "", " E-mail: " & txtNewEmail_ch02.Text, "") & " CARE OF " & txtdestination_company.Text & " " & txtdestination_address.Text _
                                        & " " & txtdestination_province.Text & " " & txtdest_receive_country.Text &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID:  " & txtdestination_taxid.Text, "") &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                        IIf(Check_NULLALL(txtplace_Exibition.Text) <> "", " " & txtplace_Exibition.Text, "")

                ElseIf Mid(txtdest_remark1.Text, 1, 3) = "O/B" Then
                    StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " &
                                            txtdestination_province.Text & " " & txtdest_receive_country.Text &
                                            IIf(ConvertToString(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                            IIf(ConvertToString(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                            & IIf(ConvertToString(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                            IIf(ConvertToString(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                            " ON BEHALF OF " & txtob_dest_address.Text & IIf(Check_NULLALL(txtNewEmail_ch02.Text) <> "", " E-mail: " & txtNewEmail_ch02.Text, "") & IIf(Check_NULLALL(txtplace_Exibition.Text) <> "", " " & txtplace_Exibition.Text, "")

                ElseIf Mid(txtdest_remark1.Text, 2, 1) <> "/" Then
                    StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " & txtdestination_province.Text _
                                        & " " & txtdest_receive_country.Text &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                        & IIf(ConvertToString(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                        IIf(Check_NULLALL(txtplace_Exibition.Text) <> "", " " & txtplace_Exibition.Text, "")

                End If
            Case "FORM1"
                If Mid(txtdest_remark1.Text, 1, 3) = "C/O" Then
                    StrtxtRerutn = txtob_dest_address.Text & IIf(Check_NULLALL(txtNewEmail_ch02.Text) <> "", " E-mail: " & txtNewEmail_ch02.Text, "") & " CARE OF " & txtdestination_company.Text & " " & txtdestination_address.Text _
                                        & " " & txtdestination_province.Text & " " & txtdest_receive_country.Text &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID:  " & txtdestination_taxid.Text, "") &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                        IIf(Check_NULLALL(txtplace_Exibition.Text) <> "", " " & txtplace_Exibition.Text, "")

                ElseIf Mid(txtdest_remark1.Text, 1, 3) = "O/B" Then
                    StrtxtRerutn = txtdestination_company.Text & vbNewLine & txtdestination_address.Text & " " &
                                            txtdestination_province.Text & " " & txtdest_receive_country.Text &
                                            IIf(ConvertToString(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                            IIf(ConvertToString(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                            & IIf(ConvertToString(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                            IIf(ConvertToString(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                            " ON BEHALF OF " & txtob_dest_address.Text & IIf(Check_NULLALL(txtNewEmail_ch02.Text) <> "", " E-mail: " & txtNewEmail_ch02.Text, "") & IIf(Check_NULLALL(txtplace_Exibition.Text) <> "", " " & txtplace_Exibition.Text, "")

                ElseIf Mid(txtdest_remark1.Text, 2, 1) <> "/" Then
                    StrtxtRerutn = txtdestination_company.Text & vbNewLine & txtdestination_address.Text & " " & txtdestination_province.Text _
                                        & " " & txtdest_receive_country.Text &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                        & IIf(ConvertToString(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                        IIf(Check_NULLALL(txtplace_Exibition.Text) <> "", " " & txtplace_Exibition.Text, "")

                End If
            Case "FORM1_4"
                If Mid(txtdest_remark1.Text, 1, 3) = "C/O" Then
                    StrtxtRerutn = txtob_dest_address.Text & IIf(Check_NULLALL(txtNewEmail_ch02.Text) <> "", " E-mail: " & txtNewEmail_ch02.Text, "") & " CARE OF " & txtdestination_company.Text & " " & txtdestination_address.Text _
                                        & " " & txtdestination_province.Text & " " & txtdest_receive_country.Text &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID:  " & txtdestination_taxid.Text, "") &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                        IIf(Check_NULLALL(txtplace_Exibition.Text) <> "", " " & txtplace_Exibition.Text, "")

                ElseIf Mid(txtdest_remark1.Text, 1, 3) = "O/B" Then
                    StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " &
                                            txtdestination_province.Text & " " & txtdest_receive_country.Text &
                                            IIf(ConvertToString(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                            IIf(ConvertToString(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                            & IIf(ConvertToString(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                            IIf(ConvertToString(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                            " ON BEHALF OF " & txtob_dest_address.Text & IIf(Check_NULLALL(txtNewEmail_ch02.Text) <> "", " E-mail: " & txtNewEmail_ch02.Text, "") & IIf(Check_NULLALL(txtplace_Exibition.Text) <> "", " " & txtplace_Exibition.Text, "")

                ElseIf Mid(txtdest_remark1.Text, 2, 1) <> "/" Then
                    StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " & txtdestination_province.Text _
                                        & " " & txtdest_receive_country.Text &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                        & IIf(ConvertToString(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                        IIf(Check_NULLALL(txtplace_Exibition.Text) <> "", " " & txtplace_Exibition.Text, "")

                End If
            Case "FORM1_3"
                If Mid(txtdest_remark1.Text, 1, 3) = "C/O" Then
                    StrtxtRerutn = txtob_dest_address.Text & IIf(Check_NULLALL(txtNewEmail_ch02.Text) <> "", " E-mail: " & txtNewEmail_ch02.Text, "") & " CARE OF " & txtdestination_company.Text & " " & txtdestination_address.Text _
                                        & " " & txtdestination_province.Text & " " & txtdest_receive_country.Text &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID:  " & txtdestination_taxid.Text, "") &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                        IIf(Check_NULLALL(txtplace_Exibition.Text) <> "", " " & txtplace_Exibition.Text, "")

                ElseIf Mid(txtdest_remark1.Text, 1, 3) = "O/B" Then
                    StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " &
                                            txtdestination_province.Text & " " & txtdest_receive_country.Text &
                                            IIf(ConvertToString(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                            IIf(ConvertToString(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                            & IIf(ConvertToString(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                            IIf(ConvertToString(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                            " ON BEHALF OF " & txtob_dest_address.Text & IIf(Check_NULLALL(txtNewEmail_ch02.Text) <> "", " E-mail: " & txtNewEmail_ch02.Text, "") & IIf(Check_NULLALL(txtplace_Exibition.Text) <> "", " " & txtplace_Exibition.Text, "")

                ElseIf Mid(txtdest_remark1.Text, 2, 1) <> "/" Then
                    StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " & txtdestination_province.Text _
                                        & " " & txtdest_receive_country.Text &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                        & IIf(ConvertToString(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                        IIf(Check_NULLALL(txtplace_Exibition.Text) <> "", " " & txtplace_Exibition.Text, "")

                End If
            Case "FORM1_2"
                If Mid(txtdest_remark1.Text, 1, 3) = "C/O" Then
                    StrtxtRerutn = txtob_dest_address.Text & IIf(Check_NULLALL(txtNewEmail_ch02.Text) <> "", " E-mail: " & txtNewEmail_ch02.Text, "") & " CARE OF " & txtdestination_company.Text & " " & txtdestination_address.Text _
                                        & " " & txtdestination_province.Text & " " & txtdest_receive_country.Text &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID:  " & txtdestination_taxid.Text, "") &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                        IIf(Check_NULLALL(txtplace_Exibition.Text) <> "", " " & txtplace_Exibition.Text, "")

                ElseIf Mid(txtdest_remark1.Text, 1, 3) = "O/B" Then
                    StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " &
                                            txtdestination_province.Text & " " & txtdest_receive_country.Text &
                                            IIf(ConvertToString(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                            IIf(ConvertToString(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                            & IIf(ConvertToString(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                            IIf(ConvertToString(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                            " ON BEHALF OF " & txtob_dest_address.Text & IIf(Check_NULLALL(txtNewEmail_ch02.Text) <> "", " E-mail: " & txtNewEmail_ch02.Text, "") & IIf(Check_NULLALL(txtplace_Exibition.Text) <> "", " " & txtplace_Exibition.Text, "")

                ElseIf Mid(txtdest_remark1.Text, 2, 1) <> "/" Then
                    StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " & txtdestination_province.Text _
                                        & " " & txtdest_receive_country.Text &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                        & IIf(ConvertToString(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                        IIf(Check_NULLALL(txtplace_Exibition.Text) <> "", " " & txtplace_Exibition.Text, "")

                End If
            Case "FORM1_1"
                If Mid(txtdest_remark1.Text, 1, 3) = "C/O" Then
                    StrtxtRerutn = txtob_dest_address.Text & IIf(Check_NULLALL(txtNewEmail_ch02.Text) <> "", " E-mail: " & txtNewEmail_ch02.Text, "") & " CARE OF " & txtdestination_company.Text & " " & txtdestination_address.Text _
                                        & " " & txtdestination_province.Text & " " & txtdest_receive_country.Text &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID:  " & txtdestination_taxid.Text, "") &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                        IIf(Check_NULLALL(txtplace_Exibition.Text) <> "", " " & txtplace_Exibition.Text, "")

                ElseIf Mid(txtdest_remark1.Text, 1, 3) = "O/B" Then
                    StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " &
                                            txtdestination_province.Text & " " & txtdest_receive_country.Text &
                                            IIf(ConvertToString(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                            IIf(ConvertToString(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                            & IIf(ConvertToString(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                            IIf(ConvertToString(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                            " ON BEHALF OF " & txtob_dest_address.Text & IIf(Check_NULLALL(txtNewEmail_ch02.Text) <> "", " E-mail: " & txtNewEmail_ch02.Text, "") & IIf(Check_NULLALL(txtplace_Exibition.Text) <> "", " " & txtplace_Exibition.Text, "")

                ElseIf Mid(txtdest_remark1.Text, 2, 1) <> "/" Then
                    StrtxtRerutn = txtdestination_company.Text & " " & txtdestination_address.Text & " " & txtdestination_province.Text _
                                        & " " & txtdest_receive_country.Text &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_phone.Text)) <> "", " TEL: " & txtdestination_phone.Text, "") &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_fax.Text)) <> "", " FAX: " & txtdestination_fax.Text, "") _
                                        & IIf(ConvertToString(Check_NULLALL(txtdestination_taxid.Text)) <> "", " TAX ID: " & txtdestination_taxid.Text, "") &
                                        IIf(ConvertToString(Check_NULLALL(txtdestination_email.Text)) <> "", " E-mail: " & txtdestination_email.Text, "") &
                                        IIf(Check_NULLALL(txtplace_Exibition.Text) <> "", " " & txtplace_Exibition.Text, "")

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
    Private Sub PageHeader1_Format(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PageHeader1.Format
        txtALL3.Text = ConvertToString(txtport_discharge.Value) & vbNewLine & ConvertToString(txtvasel_name.Value)

        '===============txtForm_Name_Temp===================================
        txtForm_Name_Temp.Text = "ตัวอย่าง" & txtForm_Name.Text

        '===============txtREFERENCE_CODE2_Temp===================================
        'txtREFERENCE_CODE2_Temp.Text = "I" + txtREFERENCE_CODE2.Text

        '============txtcard_id_Temp=txtcompany_name=====================================
        txtcard_id_Temp.Text = v1_Head_Checkcompany(txtform_type.Text)
        ' ''txtcard_id_Temp.Text = "อำนาจฯเลขที่  " & txtcard_id.Text & "   ของบริษัท/ห้าง/ร้าน  " & txtcompany_name.Text

        '' ''===========txtcompany_taxno_Temp=======================================
        ' ''txtcompany_taxno_Temp.Text = "เลขประจำตัวผู้เสียภาษีอากร   " & txtcompany_taxno.Text

        '' ''===============txtcompany_address_Temp===================================
        ' ''txtcompany_address_Temp.Text = "ตั้งอยู่เลขที่  " & txtcompany_address.Text & " " & txtcompany_province.Text & " ประเทศ " & txtcompany_country.Text

        '' ''===============txtcompany_phone_fax_Temp===================================
        ' ''If ConvertToString(txtcompany_phone.Text) <> "" And ConvertToString(txtcompany_fax.Text) <> "" Then
        ' ''    txtcompany_phone_fax_Temp.Text = " โทรศัพท์  " & txtcompany_phone.Text & "   โทรสาร  " & txtcompany_fax.Text
        ' ''ElseIf ConvertToString(txtcompany_phone.Text) <> "" And ConvertToString(txtcompany_fax.Text) = "" Then
        ' ''    txtcompany_phone_fax_Temp.Text = " โทรศัพท์  " & txtcompany_phone.Text
        ' ''ElseIf ConvertToString(txtcompany_phone.Text) = "" And ConvertToString(txtcompany_fax.Text) <> "" Then
        ' ''    txtcompany_phone_fax_Temp.Text = " โทรสาร  " & txtcompany_fax.Text
        ' ''End If

        '================txtdestination_address_province_dest_receive_country==================================
        txtdestination_address_province_dest_receive_country.Text = v2_Head_Checkdestination(txtform_type.Text)
        ' ''If ConvertToString(txtdestination_phone.Text) <> "" And ConvertToString(txtdestination_fax.Text) <> "" Then
        ' ''    txtdestination_address_province_dest_receive_country.Text = txtdestination_address.Text & "  " & txtdestination_province.Text & "  " & txtdest_receive_country.Text _
        ' ''    & " โทรศัพท์ " & txtdestination_phone.Text & "  โทรสาร " & txtdestination_fax.Text

        ' ''ElseIf ConvertToString(txtdestination_phone.Text) <> "" And ConvertToString(txtdestination_fax.Text) = "" Then
        ' ''    txtdestination_address_province_dest_receive_country.Text = txtdestination_address.Text & "  " & txtdestination_province.Text & "  " & txtdest_receive_country.Text _
        ' ''                & " โทรศัพท์ " & txtdestination_phone.Text

        ' ''ElseIf ConvertToString(txtdestination_phone.Text) = "" And ConvertToString(txtdestination_fax.Text) <> "" Then
        ' ''    txtdestination_address_province_dest_receive_country.Text = txtdestination_address.Text & "  " & txtdestination_province.Text & "  " & txtdest_receive_country.Text _
        ' ''    & "  โทรสาร " & txtdestination_fax.Text

        ' ''ElseIf ConvertToString(txtdestination_phone.Text) = "" And ConvertToString(txtdestination_fax.Text) = "" Then
        ' ''    txtdestination_address_province_dest_receive_country.Text = txtdestination_address.Text & "  " & txtdestination_province.Text & "  " & txtdest_receive_country.Text '+ " โทรศัพท์ " + txtdestination_phone.Text + "  โทรสาร " + txtdestination_fax.Text

        ' ''Else
        ' ''    txtdestination_address_province_dest_receive_country.Text = txtdestination_address.Text & "  " & txtdestination_province.Text & "  " & txtdest_receive_country.Text & " โทรศัพท์ " & txtdestination_phone.Text & "  โทรสาร " & txtdestination_fax.Text
        ' ''End If

        '===============txtimport_country_Temp===================================
        txtimport_country_Temp.Text = "ประเทศปลายทาง  " & txtcountry_name.Text & "   ( " & txtdestination_country.Value & " )" 'txtimport_country.Text

        '==================txtship_by================================
        Select Case ConvertToString(txtship_by.Text)
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
    End Sub
    Dim TAttNetweightTotal, TAttUSDTotal As Decimal
    Private Sub Detail1_Format(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Detail1.Format
        C_num += 1
        txtNum_Product.Text = C_num & "."
        '===================================================
        txtTARIFF_CODE_Temp.Text = Mid(txtTARIFF_CODE.Text, 1, 4) & "." & Mid(txtTARIFF_CODE.Text, 5, 5)
        txtNET_WEIGHT_unit_code2.Text = Format(txtNET_WEIGHT.Value, "#,##0.00##") & " " & txtunit_code2.Text
        txtFOB_AMT_Temp.Text = Format(txtFOB_AMT.Value, "#,##0.00##")

        '===================================================
        C_NET_WEIGHT += CDec(txtNET_WEIGHT.Text)
        C_Fob_Amt += CDec(txtFOB_AMT.Text)

        txtTemp_PRODUCT_NAME.Text = txtproduct_description.Text & "****"

        'by rut new D att
        TAttNetweightTotal += Check_Null(ConvertToString(txtAttNetweightTotal.Value))
        TAttUSDTotal += Check_Null(ConvertToString(txtAttUSDTotal.Value))
    End Sub

    Private Sub PageFooter1_Format(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PageFooter1.Format
        txtQuantityAll.Text = "Quantity 1 : " & IIf(ConvertToString(txtquantity1.Text) <> "" And ConvertToDecimal(txtquantity1.Text) <> 0, Format(ConvertToDecimal(txtquantity1.Text), "#,###.####"), "") & "   " & ConvertToString(txtq_unit_code1.Value) & ", "
        txtQuantityAll.Text = txtQuantityAll.Text & " Quantity 2 : " & IIf(ConvertToString(txtquantity2.Text) <> "" And ConvertToDecimal(txtquantity2.Text) <> 0, Format(ConvertToDecimal(txtquantity2.Text), "#,###.####"), "") & "   " & ConvertToString(txtq_unit_code2.Value) & ", "
        txtQuantityAll.Text = txtQuantityAll.Text & " Quantity 3 : " & IIf(ConvertToString(txtquantity3.Text) <> "" And ConvertToDecimal(txtquantity3.Text) <> 0, Format(ConvertToDecimal(txtquantity3.Text), "#,###.####"), "") & "   " & ConvertToString(txtq_unit_code3.Value) & ", "
        txtQuantityAll.Text = txtQuantityAll.Text & " Quantity 4 : " & IIf(ConvertToString(txtquantity4.Text) <> "" And ConvertToDecimal(txtquantity4.Text) <> 0, Format(ConvertToDecimal(txtquantity4.Text), "#,###.####"), "") & "   " & ConvertToString(txtq_unit_code4.Value) & ", "
        txtQuantityAll.Text = txtQuantityAll.Text & " Quantity 5 : " & IIf(ConvertToString(txtquantity5.Text) <> "" And ConvertToDecimal(txtquantity5.Text) <> 0, Format(ConvertToDecimal(txtquantity5.Text), "#,###.####"), "") & "   " & ConvertToString(txtq_unit_code5.Value)

        If txtNumpageForm.Value = 1 Then
            txtTotal.Text = "ยอดรวม GROSS WEIGHT/NET WEIGHT/OTHER QUANTITY = " & GrossTxt() 'txtSum_Gross_Weight.Text
        Else
            txtTotal.Text = ""
        End If

        If ConvertToString(txtform_type.Value) <> "FORM2_1" Then
            txtTemp_sailing_date.Text = Format(Get_DateTime_rpt(txtsailing_date.Value), "dd/MM/yyy") 'Format(Get_DateTime_rpt(txtsailing_date.Value), "dd/MM/yyy")
        Else
            txtTemp_sailing_date.Text = ""
        End If

        txtTemp_edi_date.Text = Format(Get_DateTime_rpt(txtedi_date.Value), "dd/MM/yyy")

        Dim str_in As String = ""
        str_in = "เลขที่ " & IIf(ConvertToString(txtinvoice_no1.Text) <> "", ConvertToString(txtinvoice_no1.Text).Replace(vbCrLf, ""), "") & " " &
                                IIf(ConvertToString(txtinvoice_no2.Text) <> "", ConvertToString(txtinvoice_no2.Text).Replace(vbCrLf, ""), "") & " " &
                                IIf(ConvertToString(txtinvoice_no3.Text) <> "", ConvertToString(txtinvoice_no3.Text).Replace(vbCrLf, ""), "") & " " &
                                IIf(ConvertToString(txtinvoice_no4.Text) <> "", ConvertToString(txtinvoice_no4.Text).Replace(vbCrLf, ""), "") & " " &
                                IIf(ConvertToString(txtinvoice_no5.Text) <> "", ConvertToString(txtinvoice_no5.Text).Replace(vbCrLf, ""), "")
        Dim str_inDate As String = ""
        str_inDate = "ลงวันที่ " & IIf(ConvertToString(txtinvoice_no1.Text) <> "" And txtinvoice_date1.Text <> Nothing, Format(Get_DateTime_rpt(txtinvoice_date1.Text), "dd/MM/yyy"), "") & " " &
                                    IIf(ConvertToString(txtinvoice_no2.Text) <> "" And txtinvoice_date2.Text <> Nothing, Format(Get_DateTime_rpt(txtinvoice_date2.Text), "dd/MM/yyy"), "") & " " &
                                    IIf(ConvertToString(txtinvoice_no3.Text) <> "" And txtinvoice_date3.Text <> Nothing, Format(Get_DateTime_rpt(txtinvoice_date3.Text), "dd/MM/yyy"), "") & " " &
                                    IIf(ConvertToString(txtinvoice_no4.Text) <> "" And txtinvoice_date4.Text <> Nothing, Format(Get_DateTime_rpt(txtinvoice_date4.Text), "dd/MM/yyy"), "") & " " &
                                    IIf(ConvertToString(txtinvoice_no5.Text) <> "" And txtinvoice_date5.Text <> Nothing, Format(Get_DateTime_rpt(txtinvoice_date5.Text), "dd/MM/yyy"), "") & " "
        txtInvoince_Date.Text = str_in & " " & str_inDate

        ''txtSum_Net_Weight.Text = Format(CDec(txttotal_real_net_weight.Text), "#,##0.00##") & " " & txtunit_code2.Text
        ''txtSum_Fob_Amt.Text = Format(CDec(txttotalSum_fob_amt.Text), "#,##0.00##")
        'by rut d att
        Select Case ConvertToString(txtform_type.Value)
            Case Is = "FORM44_44", "FORM441_4", "FORM44_02", "FORMD_ESS_ATTS", "FORMD_ESS_"
                'ออกแบบใหม่ ดี att
                txtSum_Net_Weight.Text = Format(CDec(TAttNetweightTotal), "#,##0.00##") & " " & txtunit_code2.Text
                txtSum_Fob_Amt.Text = Format(CDec(TAttUSDTotal), "#,##0.00##")

            Case Else
                'ออกแบบเดิม
                txtSum_Net_Weight.Text = Format(CDec(txttotal_real_net_weight.Text), "#,##0.00##") & " " & txtunit_code2.Text 'Format(CDec(txttotal_real_net_weight.Text), "#,##0.00##") & " " & txtunit_code2.Text
                txtSum_Fob_Amt.Text = Format(CDec(txttotalSum_fob_amt.Text), "#,##0.00##") 'Format(CDec(txttotalSum_fob_amt.Text), "#,##0.00##")
        End Select

        'txtSum_Net_Weight.Text = Format(CDec(txtNET_WEIGHTPage.Text), "#,##0.00##") & " " & txtunit_code2.Text
        'txtSum_Fob_Amt.Text = Format(CDec(txtSumFOB_AMTPage.Text), "#,##0.00##")
        '==============txtrequest_person_2====================================
        txtrequest_person_2.Text = "( " + txtrequest_person.Text + " )    ผู้รับมอบ" '"( " + txtrequest_person.Text + " )    ผู้รับมอบ"

        Select Case ConvertToString(txtbill_type.Text)
            Case "0"
                txtbill_type0.Text = "X"
            Case "1"
                txtbill_type1.Text = "X"
            Case "2"
                txtbill_type2.Text = "X"
            Case "3"
                txtbill_type3.Text = "X"
        End Select
    End Sub

    Private Sub GroupFooter1_Format(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GroupFooter1.Format
        '==================================================
        ' txtTotal.Text = "ยอดรวม       GROSS WEIGHT = " & GrossTxt() 'txtSum_Gross_Weight.Text

        '===============txtbill_type===================================


    End Sub
    'เฉพาะ view font ใช้ gross_weight เป็น a_gross_weight
    Function GrossTxt() As String
        Dim Str_GrossTxt As String
        Dim Dec_Gross As Decimal
        Dec_Gross = Format(CDec(Check_Null(ConvertToString(txtgross_weightH.Value))), "#,##0.00##")
        Str_GrossTxt = Format(Dec_Gross, "#,##0.00##") & " " & ConvertToString(txtg_unit_code.Value)

        Return Str_GrossTxt
    End Function

    Private Sub rpt3_ReEdi_AView1_PageStart(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PageStart
        Me.CurrentPage.DrawLine(0.18, 5, 0.18, 9)
        Me.CurrentPage.DrawLine(8.07, 5, 8.07, 11)

        Me.CurrentPage.DrawLine(3.74, 5.5, 3.74, 7.17) 'mark
        Me.CurrentPage.DrawLine(4.707, 5.5, 4.707, 7.17)
        Me.CurrentPage.DrawLine(5.74, 5.5, 5.74, 7.2)
        Me.CurrentPage.DrawLine(7.113, 5.5, 7.113, 7.2)
    End Sub

    Function Get_DateTime_rpt(ByVal val As String) As DateTime
        If val Is Nothing Then
            Return Now
        Else
            val = val.ToLower().Trim()
            If (val.StartsWith("today")) Then

                If val = "today" Then
                    Return DateTime.Today
                End If
                Try
                    Dim htValues As Hashtable
                    htValues = GetAllValues_rpt(val.Substring(5))
                    Dim strTimeVal As String = htValues("TimeVal").ToString()
                    Select Case strTimeVal

                        Case "Days"
                            Dim days As Double = ConvertToDouble(htValues("Operator").ToString() + htValues("Number").ToString())
                            Return DateTime.Today.AddDays(days)
                        Case "Months"
                            Dim Months As Int32 = Convert.ToInt32(htValues("Operator").ToString() + htValues("Number").ToString())
                            Return DateTime.Today.AddMonths(Months)
                        Case "Years"
                            Dim years As Int32 = Convert.ToInt32(htValues("Operator").ToString() + htValues("Number").ToString())
                            Return DateTime.Today.AddYears(years)
                        Case Else
                            Return DateTime.Today
                    End Select
                Catch ex As Exception
                    Return DateTime.Today
                End Try

            End If
            If val <> "" Then
                Return Convert.ToDateTime(val)
            End If
            Return New DateTime
        End If

    End Function

    Function GetAllValues_rpt(ByVal val As String) As Hashtable

        val = val.Trim()
        Dim ht As Hashtable = New Hashtable(3)
        Dim strMember As String = ""
        strMember = val.Substring(0, 1)
        If strMember = "+" Or strMember = "-" Then
            ht.Add("Operator", strMember)
        End If

        Dim nEndChars As Int32 = 0
        strMember = val.Substring(1).Trim()

        If strMember.EndsWith("days") Or strMember.EndsWith("day") Then
            ht.Add("TimeVal", "Days")
            If strMember.EndsWith("days") Then
                nEndChars = 4
            Else
                nEndChars = 3
            End If
        ElseIf strMember.EndsWith("months") Or strMember.EndsWith("month") Then
            ht.Add("TimeVal", "Months")
            If strMember.EndsWith("months") Then
                nEndChars = 6
            Else
                nEndChars = 5
            End If

        ElseIf strMember.EndsWith("years") Or strMember.EndsWith("year") Then
            ht.Add("TimeVal", "Years")
            If strMember.EndsWith("years") Then
                nEndChars = 5
            Else
                nEndChars = 4
            End If
        Else
            ht.Add("TimeVal", "Days")
        End If

        strMember = strMember.Substring(0, (strMember.Length - nEndChars))
        strMember = strMember.Trim()
        ht.Add("Number", strMember)

        Return ht
    End Function

    'check and return "" เป็น 0.0
    Function Check_Null(ByVal V_txt As Object) As Object
        If V_txt = "" Then
            Return 0.0
        Else
            Return V_txt
        End If
    End Function

    Private Function ConvertToString(obj As Object) As String
        Dim result As String = ""
        Try
            result = Convert.ToString(obj)
        Catch ex As Exception

        End Try
        Return result
    End Function
    Private Function ConvertToDate(obj As Object) As Date
        Dim result As Object = DBNull.Value
        Try
            Dim culture As New System.Globalization.CultureInfo("en-US")
            result = Convert.ToDateTime(obj, culture)
        Catch ex As Exception

        End Try
        Return result
    End Function

    Private Function ConvertToBoolean(obj As Object) As Boolean
        Dim ret As Boolean = False
        Try
            ret = Convert.ToBoolean(obj)
        Catch ex As Exception

        End Try
        Return ret
    End Function

    Private Function ConvertToDouble(obj As Object) As Double
        Dim result As Double = 0
        Try
            result = Convert.ToDouble(obj)
        Catch ex As Exception

        End Try
        Return result
    End Function

    Private Function ConvertToDecimal(obj As Object) As Decimal
        Dim result As Decimal = 0
        Try
            result = Convert.ToDecimal(obj)
        Catch ex As Exception

        End Try
        Return result
    End Function


    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub
End Class
