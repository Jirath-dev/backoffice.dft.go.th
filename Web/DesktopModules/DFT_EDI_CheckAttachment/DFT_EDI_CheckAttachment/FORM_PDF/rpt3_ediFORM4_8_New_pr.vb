Imports DataDynamics.ActiveReports
Imports DataDynamics.ActiveReports.Document

Imports Microsoft.ApplicationBlocks.Data
Imports System.Data.SqlClient
Imports System.Data
Imports System.Text
Imports DFT.Dotnetnuke.ClassLibrary
Imports ReportPrintClass
Public Class rpt3_ediFORM4_8_New_pr
    Dim Cpage As Integer
    Dim CpageNum As Integer
    Public _TempB2B As String

    'by rut gross new
    Dim TGross, TUSD, TUSDOther As Decimal
    Dim Str_USDinvoiceDetail As String
    'เงื่อนไขใหม่ ปรับแก้ วันที่ 11-3-2014
    'begin RVC
    Dim Check_CaseRVCCount As Integer
    'end RVC

    'by rut Title New Begin-----------------
    Dim Str_TitlePage As String
    'by rut Title New End-------------------
    'by rut ใช้กับ page end เพื่อกำหนด
    Dim head_height As String
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

    'Sub All_CheckThird_5(ByVal count_data As Integer)
    '    Dim All_str As String
    '    Select Case Check_Third(CommonUtility.Get_StringValue(txtthird_country.Value), _
    '                            CommonUtility.Get_StringValue(txtback_country.Value), _
    '                            CommonUtility.Get_StringValue(txtplace_exibition.Value))
    '        Case 1
    '            All_str = "HS. CODE. " + tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) + vbCrLf _
    '                                        + txtproduct_n1.Text + txtproduct_n2.Text + " ****" + vbCrLf

    '            If C_TotalRowDe.Text = count_data Then
    '                txtT_product.Text = All_str & _
    '                "TOTAL: " + BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) + vbCrLf + "        " + _
    '                                                            BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) + vbCrLf + "        " + _
    '                                                            BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) + vbCrLf + "        " + _
    '                                                            BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) + vbCrLf + "        " + _
    '                                                            BahtOnly(txtquantity5.Text, txtq_unit_code5.Text) + vbCrLf + txtthird_country.Text + vbCrLf + _
    '                                                            txtback_country.Text + vbCrLf + _
    '                                                            txtplace_exibition.Text + vbCrLf + _
    '                                                            "       _____________________________"
    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 2 'T2_back_country,T3_place_exibition
    '            All_str = "HS. CODE. " + tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) + vbCrLf _
    '                                                        + txtproduct_n1.Text + txtproduct_n2.Text + " ****" + vbCrLf

    '            If C_TotalRowDe.Text = count_data Then
    '                txtT_product.Text = All_str & _
    '                 "TOTAL: " + BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) + vbCrLf + "        " + _
    '                                                                            BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) + vbCrLf + "        " + _
    '                                                                            BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) + vbCrLf + "        " + _
    '                                                                            BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) + vbCrLf + "        " + _
    '                                                                            BahtOnly(txtquantity5.Text, txtq_unit_code5.Text) + vbCrLf + _
    '                                                                            txtback_country.Text + vbCrLf + _
    '                                                                            txtplace_exibition.Text + vbCrLf + _
    '                                                                            "       _____________________________"
    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 3 'T3_place_exibition
    '            All_str = "HS. CODE. " + tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) + vbCrLf _
    '                                            + txtproduct_n1.Text + txtproduct_n2.Text + " ****" + vbCrLf

    '            If C_TotalRowDe.Text = count_data Then
    '                txtT_product.Text = All_str & _
    '                 "TOTAL: " + BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) + vbCrLf + "        " + _
    '                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) + vbCrLf + "        " + _
    '                                                                BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) + vbCrLf + "        " + _
    '                                                                BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) + vbCrLf + "        " + _
    '                                                                BahtOnly(txtquantity5.Text, txtq_unit_code5.Text) + vbCrLf + + _
    '                                                                txtplace_exibition.Text + vbCrLf + _
    '                                                                "       _____________________________"
    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 4 'ไม่มีเลย
    '            All_str = "HS. CODE. " + tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) + vbCrLf _
    '                                            + txtproduct_n1.Text + txtproduct_n2.Text + " ****" + vbCrLf

    '            If C_TotalRowDe.Text = count_data Then
    '                txtT_product.Text = All_str & _
    '                "TOTAL: " + BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) + vbCrLf + "        " + _
    '                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) + vbCrLf + "        " + _
    '                                                                BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) + vbCrLf + "        " + _
    '                                                                BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) + vbCrLf + "        " + _
    '                                                                BahtOnly(txtquantity5.Text, txtq_unit_code5.Text) + vbCrLf + _
    '                                                                "       _____________________________"
    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 5 'T2_back_country
    '            All_str = "HS. CODE. " + tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) + vbCrLf _
    '                                            + txtproduct_n1.Text + txtproduct_n2.Text + " ****" + vbCrLf

    '            If C_TotalRowDe.Text = count_data Then
    '                txtT_product.Text = All_str & _
    '                "TOTAL: " + BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) + vbCrLf + "        " + _
    '                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) + vbCrLf + "        " + _
    '                                                                BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) + vbCrLf + "        " + _
    '                                                                BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) + vbCrLf + "        " + _
    '                                                                BahtOnly(txtquantity5.Text, txtq_unit_code5.Text) + vbCrLf + _
    '                                                                "       _____________________________"
    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 6 'T3_place_exibition
    '            All_str = "HS. CODE. " + tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) + vbCrLf _
    '                                            + txtproduct_n1.Text + txtproduct_n2.Text + " ****" + vbCrLf

    '            If C_TotalRowDe.Text = count_data Then
    '                txtT_product.Text = All_str & _
    '                 "TOTAL: " + BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) + vbCrLf + "        " + _
    '                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) + vbCrLf + "        " + _
    '                                                                BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) + vbCrLf + "        " + _
    '                                                                BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) + vbCrLf + "        " + _
    '                                                                BahtOnly(txtquantity5.Text, txtq_unit_code5.Text) + vbCrLf + + _
    '                                                                txtplace_exibition.Text + vbCrLf + _
    '                                                                "       _____________________________"
    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 7 'T1_third_country,T2_back_country
    '            All_str = "HS. CODE. " + tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) + vbCrLf _
    '                                            + txtproduct_n1.Text + txtproduct_n2.Text + " ****" + vbCrLf

    '            If C_TotalRowDe.Text = count_data Then
    '                txtT_product.Text = All_str & _
    '                "TOTAL: " + BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) + vbCrLf + "        " + _
    '                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) + vbCrLf + "        " + _
    '                                                                BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) + vbCrLf + "        " + _
    '                                                                BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) + vbCrLf + "        " + _
    '                                                                BahtOnly(txtquantity5.Text, txtq_unit_code5.Text) + vbCrLf + txtthird_country.Text + vbCrLf + _
    '                                                                txtback_country.Text + vbCrLf + _
    '                                                                "       _____________________________"
    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 8 'T1_third_country,T3_place_exibition
    '            All_str = "HS. CODE. " + tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) + vbCrLf _
    '                                            + txtproduct_n1.Text + txtproduct_n2.Text + " ****" + vbCrLf

    '            If C_TotalRowDe.Text = count_data Then
    '                txtT_product.Text = All_str & _
    '                 "TOTAL: " + BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) + vbCrLf + "        " + _
    '                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) + vbCrLf + "        " + _
    '                                                                BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) + vbCrLf + "        " + _
    '                                                                BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) + vbCrLf + "        " + _
    '                                                                BahtOnly(txtquantity5.Text, txtq_unit_code5.Text) + vbCrLf + txtthird_country.Text + vbCrLf + _
    '                                                                txtplace_exibition.Text + vbCrLf + _
    '                                                                "       _____________________________"
    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 9 'T1_third_country
    '            All_str = "HS. CODE. " + tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) + vbCrLf _
    '                                        + txtproduct_n1.Text + txtproduct_n2.Text + " ****" + vbCrLf

    '            If C_TotalRowDe.Text = count_data Then
    '                txtT_product.Text = All_str & _
    '                "TOTAL: " + BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) + vbCrLf + "        " + _
    '                                                            BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) + vbCrLf + "        " + _
    '                                                            BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) + vbCrLf + "        " + _
    '                                                            BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) + vbCrLf + "        " + _
    '                                                            BahtOnly(txtquantity5.Text, txtq_unit_code5.Text) + vbCrLf + txtthird_country.Text + vbCrLf + _
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
    '            All_str = "HS. CODE. " + tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) + vbCrLf _
    '                                        + txtproduct_n1.Text + txtproduct_n2.Text + " ****" + vbCrLf

    '            If C_TotalRowDe.Text = count_data Then
    '                txtT_product.Text = All_str & _
    '                "TOTAL: " + BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) + vbCrLf + "        " + _
    '                                        BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) + vbCrLf + "        " + _
    '                                        BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) + vbCrLf + "        " + _
    '                                        BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) + vbCrLf + "        " + _
    '                                        txtthird_country.Text + vbCrLf + _
    '                                        txtback_country.Text + vbCrLf + _
    '                                        txtplace_exibition.Text + vbCrLf + _
    '                                        "       _____________________________"
    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 2 'T2_back_country,T3_place_exibition
    '            All_str = "HS. CODE. " + tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) + vbCrLf _
    '                                                        + txtproduct_n1.Text + txtproduct_n2.Text + " ****" + vbCrLf

    '            If C_TotalRowDe.Text = count_data Then
    '                txtT_product.Text = All_str & _
    '                "TOTAL: " + BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) + vbCrLf + "        " + _
    '                                                        BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) + vbCrLf + "        " + _
    '                                                        BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) + vbCrLf + "        " + _
    '                                                        BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) + vbCrLf + "        " + _
    '                                                        txtback_country.Text + vbCrLf + _
    '                                                        txtplace_exibition.Text + vbCrLf + _
    '                                                        "       _____________________________"
    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 3 'T3_place_exibition
    '            All_str = "HS. CODE. " + tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) + vbCrLf _
    '                                        + txtproduct_n1.Text + txtproduct_n2.Text + " ****" + vbCrLf

    '            If C_TotalRowDe.Text = count_data Then
    '                txtT_product.Text = All_str & _
    '                "TOTAL: " + BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) + vbCrLf + "        " + _
    '                                                            BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) + vbCrLf + "        " + _
    '                                                            BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) + vbCrLf + "        " + _
    '                                                            BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) + vbCrLf + "        " + _
    '                                                            txtplace_exibition.Text + vbCrLf + _
    '                                                            "       _____________________________"
    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 4 'ไม่มีเลย
    '            All_str = "HS. CODE. " + tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) + vbCrLf _
    '                                        + txtproduct_n1.Text + txtproduct_n2.Text + " ****" + vbCrLf

    '            If C_TotalRowDe.Text = count_data Then
    '                txtT_product.Text = All_str & _
    '                "TOTAL: " + BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) + vbCrLf + "        " + _
    '                                                            BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) + vbCrLf + "        " + _
    '                                                            BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) + vbCrLf + "        " + _
    '                                                            BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) + vbCrLf + "        " + _
    '                                                            "       _____________________________"
    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 5 'T2_back_country
    '            All_str = "HS. CODE. " + tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) + vbCrLf _
    '                                        + txtproduct_n1.Text + txtproduct_n2.Text + " ****" + vbCrLf

    '            If C_TotalRowDe.Text = count_data Then
    '                txtT_product.Text = All_str & _
    '                "TOTAL: " + BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) + vbCrLf + "        " + _
    '                                                            BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) + vbCrLf + "        " + _
    '                                                            BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) + vbCrLf + "        " + _
    '                                                            BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) + vbCrLf + "        " + _
    '                                                            "       _____________________________"
    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 6 'T3_place_exibition
    '            All_str = "HS. CODE. " + tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) + vbCrLf _
    '                                        + txtproduct_n1.Text + txtproduct_n2.Text + " ****" + vbCrLf

    '            If C_TotalRowDe.Text = count_data Then
    '                txtT_product.Text = All_str & _
    '                "TOTAL: " + BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) + vbCrLf + "        " + _
    '                                                            BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) + vbCrLf + "        " + _
    '                                                            BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) + vbCrLf + "        " + _
    '                                                            BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) + vbCrLf + "        " + _
    '                                                            txtplace_exibition.Text + vbCrLf + _
    '                                                            "       _____________________________"
    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 7 'T1_third_country,T2_back_country
    '            All_str = "HS. CODE. " + tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) + vbCrLf _
    '                                            + txtproduct_n1.Text + txtproduct_n2.Text + " ****" + vbCrLf

    '            If C_TotalRowDe.Text = count_data Then
    '                txtT_product.Text = All_str & _
    '                "TOTAL: " + BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) + vbCrLf + "        " + _
    '                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) + vbCrLf + "        " + _
    '                                                                BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) + vbCrLf + "        " + _
    '                                                                BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) + vbCrLf + "        " + _
    '                                                                txtthird_country.Text + vbCrLf + _
    '                                                                txtback_country.Text + vbCrLf + _
    '                                                                "       _____________________________"
    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 8 'T1_third_country,T3_place_exibition
    '            All_str = "HS. CODE. " + tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) + vbCrLf _
    '                                            + txtproduct_n1.Text + txtproduct_n2.Text + " ****" + vbCrLf

    '            If C_TotalRowDe.Text = count_data Then
    '                txtT_product.Text = All_str & _
    '                "TOTAL: " + BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) + vbCrLf + "        " + _
    '                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) + vbCrLf + "        " + _
    '                                                                BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) + vbCrLf + "        " + _
    '                                                                BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) + vbCrLf + "        " + _
    '                                                                txtthird_country.Text + vbCrLf + _
    '                                                                txtplace_exibition.Text + vbCrLf + _
    '                                                                "       _____________________________"
    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 9 'T1_third_country
    '            All_str = "HS. CODE. " + tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) + vbCrLf _
    '                                            + txtproduct_n1.Text + txtproduct_n2.Text + " ****" + vbCrLf

    '            If C_TotalRowDe.Text = count_data Then
    '                txtT_product.Text = All_str & _
    '                "TOTAL: " + BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) + vbCrLf + "        " + _
    '                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) + vbCrLf + "        " + _
    '                                                                BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) + vbCrLf + "        " + _
    '                                                                BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) + vbCrLf + "        " + _
    '                                                                txtthird_country.Text + vbCrLf + _
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
    '            All_str = "HS. CODE. " + tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) + vbCrLf _
    '                                        + txtproduct_n1.Text + txtproduct_n2.Text + " ****" + vbCrLf

    '            If C_TotalRowDe.Text = count_data Then
    '                txtT_product.Text = All_str & _
    '                 "TOTAL: " + BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) + vbCrLf + "        " + _
    '                                                            BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) + vbCrLf + "        " + _
    '                                                            BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) + vbCrLf + "        " + _
    '                                                            txtthird_country.Text + vbCrLf + _
    '                                                            txtback_country.Text + vbCrLf + _
    '                                                            txtplace_exibition.Text + vbCrLf + _
    '                                                            "       _____________________________"
    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 2 'T2_back_country,T3_place_exibition
    '            All_str = "HS. CODE. " + tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) + vbCrLf _
    '                                                        + txtproduct_n1.Text + txtproduct_n2.Text + " ****" + vbCrLf

    '            If C_TotalRowDe.Text = count_data Then
    '                txtT_product.Text = All_str & _
    '                "TOTAL: " + BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) + vbCrLf + "        " + _
    '                                                                            BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) + vbCrLf + "        " + _
    '                                                                            BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) + vbCrLf + "        " + _
    '                                                                            txtback_country.Text + vbCrLf + _
    '                                                                            txtplace_exibition.Text + vbCrLf + _
    '                                                                            "       _____________________________"
    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 3 'T3_place_exibition
    '            All_str = "HS. CODE. " + tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) + vbCrLf _
    '                                        + txtproduct_n1.Text + txtproduct_n2.Text + " ****" + vbCrLf

    '            If C_TotalRowDe.Text = count_data Then
    '                txtT_product.Text = All_str & _
    '                "TOTAL: " + BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) + vbCrLf + "        " + _
    '                                                            BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) + vbCrLf + "        " + _
    '                                                            BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) + vbCrLf + "        " + _
    '                                                            txtplace_exibition.Text + vbCrLf + _
    '                                                            "       _____________________________"
    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 4 'ไม่มีเลย
    '            All_str = "HS. CODE. " + tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) + vbCrLf _
    '                                        + txtproduct_n1.Text + txtproduct_n2.Text + " ****" + vbCrLf

    '            If C_TotalRowDe.Text = count_data Then
    '                txtT_product.Text = All_str & _
    '                 "TOTAL: " + BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) + vbCrLf + "        " + _
    '                                                            BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) + vbCrLf + "        " + _
    '                                                            BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) + vbCrLf + "        " + _
    '                                                            "       _____________________________"
    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 5 'T2_back_country
    '            All_str = "HS. CODE. " + tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) + vbCrLf _
    '                                        + txtproduct_n1.Text + txtproduct_n2.Text + " ****" + vbCrLf

    '            If C_TotalRowDe.Text = count_data Then
    '                txtT_product.Text = All_str & _
    '                "TOTAL: " + BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) + vbCrLf + "        " + _
    '                                                            BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) + vbCrLf + "        " + _
    '                                                            BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) + vbCrLf + "        " + _
    '                                                            "       _____________________________"
    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 6 'T3_place_exibition
    '            All_str = "HS. CODE. " + tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) + vbCrLf _
    '                                        + txtproduct_n1.Text + txtproduct_n2.Text + " ****" + vbCrLf

    '            If C_TotalRowDe.Text = count_data Then
    '                txtT_product.Text = All_str & _
    '                "TOTAL: " + BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) + vbCrLf + "        " + _
    '                                                            BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) + vbCrLf + "        " + _
    '                                                            BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) + vbCrLf + "        " + _
    '                                                            txtplace_exibition.Text + vbCrLf + _
    '                                                            "       _____________________________"
    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 7 'T1_third_country,T2_back_country
    '            All_str = "HS. CODE. " + tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) + vbCrLf _
    '                                        + txtproduct_n1.Text + txtproduct_n2.Text + " ****" + vbCrLf

    '            If C_TotalRowDe.Text = count_data Then
    '                txtT_product.Text = All_str & _
    '                "TOTAL: " + BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) + vbCrLf + "        " + _
    '                                                            BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) + vbCrLf + "        " + _
    '                                                            BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) + vbCrLf + "        " + _
    '                                                            txtthird_country.Text + vbCrLf + _
    '                                                            txtback_country.Text + vbCrLf + _
    '                                                            "       _____________________________"
    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 8 'T1_third_country,T3_place_exibition
    '            All_str = "HS. CODE. " + tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) + vbCrLf _
    '                                        + txtproduct_n1.Text + txtproduct_n2.Text + " ****" + vbCrLf

    '            If C_TotalRowDe.Text = count_data Then
    '                txtT_product.Text = All_str & _
    '                "TOTAL: " + BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) + vbCrLf + "        " + _
    '                                                            BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) + vbCrLf + "        " + _
    '                                                            BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) + vbCrLf + "        " + _
    '                                                            txtthird_country.Text + vbCrLf + _
    '                                                            txtplace_exibition.Text + vbCrLf + _
    '                                                            "       _____________________________"
    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 9 'T1_third_country
    '            All_str = "HS. CODE. " + tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) + vbCrLf _
    '                                        + txtproduct_n1.Text + txtproduct_n2.Text + " ****" + vbCrLf

    '            If C_TotalRowDe.Text = count_data Then
    '                txtT_product.Text = All_str & _
    '                 "TOTAL: " + BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) + vbCrLf + "        " + _
    '                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) + vbCrLf + "        " + _
    '                                                                BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) + vbCrLf + "        " + _
    '                                                                txtthird_country.Text + vbCrLf + _
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
    '            All_str = "HS. CODE. " + tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) + vbCrLf _
    '                                        + txtproduct_n1.Text + txtproduct_n2.Text + " ****" + vbCrLf

    '            If C_TotalRowDe.Text = count_data Then
    '                txtT_product.Text = All_str & _
    '                 "TOTAL: " + BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) + vbCrLf + "        " + _
    '                                                            BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) + vbCrLf + "        " + _
    '                                                            txtthird_country.Text + vbCrLf + _
    '                                                            txtback_country.Text + vbCrLf + _
    '                                                            txtplace_exibition.Text + vbCrLf + _
    '                                                            "       _____________________________"
    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 2 'T2_back_country,T3_place_exibition
    '            All_str = "HS. CODE. " + tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) + vbCrLf _
    '                                                        + txtproduct_n1.Text + txtproduct_n2.Text + " ****" + vbCrLf

    '            If C_TotalRowDe.Text = count_data Then
    '                txtT_product.Text = All_str & _
    '                "TOTAL: " + BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) + vbCrLf + "        " + _
    '                                                                            BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) + vbCrLf + "        " + _
    '                                                                            txtback_country.Text + vbCrLf + _
    '                                                                            txtplace_exibition.Text + vbCrLf + _
    '                                                                            "       _____________________________"
    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 3 'T3_place_exibition
    '            All_str = "HS. CODE. " + tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) + vbCrLf _
    '                                        + txtproduct_n1.Text + txtproduct_n2.Text + " ****" + vbCrLf

    '            If C_TotalRowDe.Text = count_data Then
    '                txtT_product.Text = All_str & _
    '                "TOTAL: " + BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) + vbCrLf + "        " + _
    '                                                            BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) + vbCrLf + "        " + _
    '                                                            txtplace_exibition.Text + vbCrLf + _
    '                                                            "       _____________________________"
    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 4 'ไม่มีเลย
    '            All_str = "HS. CODE. " + tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) + vbCrLf _
    '                                        + txtproduct_n1.Text + txtproduct_n2.Text + " ****" + vbCrLf

    '            If C_TotalRowDe.Text = count_data Then
    '                txtT_product.Text = All_str & _
    '                "TOTAL: " + BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) + vbCrLf + "        " + _
    '                                                            BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) + vbCrLf + "        " + _
    '                                                            "       _____________________________"
    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 5 'T2_back_country
    '            All_str = "HS. CODE. " + tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) + vbCrLf _
    '                                        + txtproduct_n1.Text + txtproduct_n2.Text + " ****" + vbCrLf

    '            If C_TotalRowDe.Text = count_data Then
    '                txtT_product.Text = All_str & _
    '                "TOTAL: " + BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) + vbCrLf + "        " + _
    '                                                            BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) + vbCrLf + "        " + _
    '                                                            "       _____________________________"
    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 6 'T3_place_exibition
    '            All_str = "HS. CODE. " + tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) + vbCrLf _
    '                                        + txtproduct_n1.Text + txtproduct_n2.Text + " ****" + vbCrLf

    '            If C_TotalRowDe.Text = count_data Then
    '                txtT_product.Text = All_str & _
    '                "TOTAL: " + BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) + vbCrLf + "        " + _
    '                                                            BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) + vbCrLf + "        " + _
    '                                                            txtplace_exibition.Text + vbCrLf + _
    '                                                            "       _____________________________"
    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 7 'T1_third_country,T2_back_country
    '            All_str = "HS. CODE. " + tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) + vbCrLf _
    '                                        + txtproduct_n1.Text + txtproduct_n2.Text + " ****" + vbCrLf

    '            If C_TotalRowDe.Text = count_data Then
    '                txtT_product.Text = All_str & _
    '                "TOTAL: " + BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) + vbCrLf + "        " + _
    '                                                            BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) + vbCrLf + "        " + _
    '                                                            txtthird_country.Text + vbCrLf + _
    '                                                            txtback_country.Text + vbCrLf + _
    '                                                            "       _____________________________"
    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 8 'T1_third_country,T3_place_exibition
    '            All_str = "HS. CODE. " + tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) + vbCrLf _
    '                                        + txtproduct_n1.Text + txtproduct_n2.Text + " ****" + vbCrLf

    '            If C_TotalRowDe.Text = count_data Then
    '                txtT_product.Text = All_str & _
    '                "TOTAL: " + BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) + vbCrLf + "        " + _
    '                                                            BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) + vbCrLf + "        " + _
    '                                                            txtthird_country.Text + vbCrLf + _
    '                                                            txtplace_exibition.Text + vbCrLf + _
    '                                                            "       _____________________________"
    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 9 'T1_third_country
    '            All_str = "HS. CODE. " + tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) + vbCrLf _
    '                                        + txtproduct_n1.Text + txtproduct_n2.Text + " ****" + vbCrLf

    '            If C_TotalRowDe.Text = count_data Then
    '                txtT_product.Text = All_str & _
    '                "TOTAL: " + BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) + vbCrLf + "        " + _
    '                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) + vbCrLf + "        " + _
    '                                                                txtthird_country.Text + vbCrLf + _
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
    '            All_str = "HS. CODE. " + tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) + vbCrLf _
    '                                        + txtproduct_n1.Text + txtproduct_n2.Text + " ****" + vbCrLf

    '            If C_TotalRowDe.Text = count_data Then
    '                txtT_product.Text = All_str & _
    '                "TOTAL: " + BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) + vbCrLf + "        " + _
    '                                                            txtthird_country.Text + vbCrLf + _
    '                                                            txtback_country.Text + vbCrLf + _
    '                                                            txtplace_exibition.Text + vbCrLf + _
    '                                                            "       _____________________________"
    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 2 'T2_back_country,T3_place_exibition
    '            All_str = "HS. CODE. " + tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) + vbCrLf _
    '                                                        + txtproduct_n1.Text + txtproduct_n2.Text + " ****" + vbCrLf

    '            If C_TotalRowDe.Text = count_data Then
    '                txtT_product.Text = All_str & _
    '                "TOTAL: " + BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) + vbCrLf + "        " + _
    '                                                                            txtback_country.Text + vbCrLf + _
    '                                                                            txtplace_exibition.Text + vbCrLf + _
    '                                                                            "       _____________________________"
    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 3 'T3_place_exibition
    '            All_str = "HS. CODE. " + tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) + vbCrLf _
    '                                        + txtproduct_n1.Text + txtproduct_n2.Text + " ****" + vbCrLf

    '            If C_TotalRowDe.Text = count_data Then
    '                txtT_product.Text = All_str & _
    '                "TOTAL: " + BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) + vbCrLf + "        " + _
    '                                                            txtplace_exibition.Text + vbCrLf + _
    '                                                            "       _____________________________"
    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 4 'ไม่มีเลย
    '            All_str = "HS. CODE. " + tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) + vbCrLf _
    '                                        + txtproduct_n1.Text + txtproduct_n2.Text + " ****" + vbCrLf

    '            If C_TotalRowDe.Text = count_data Then
    '                txtT_product.Text = All_str & _
    '                "TOTAL: " + BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) + vbCrLf + _
    '                                                            "       _____________________________"
    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 5 'T2_back_country
    '            All_str = "HS. CODE. " + tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) + vbCrLf _
    '                                        + txtproduct_n1.Text + txtproduct_n2.Text + " ****" + vbCrLf

    '            If C_TotalRowDe.Text = count_data Then
    '                txtT_product.Text = All_str & _
    '                "TOTAL: " + BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) + vbCrLf + _
    '                                                            "       _____________________________"
    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 6 'T3_place_exibition
    '            All_str = "HS. CODE. " + tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) + vbCrLf _
    '                                        + txtproduct_n1.Text + txtproduct_n2.Text + " ****" + vbCrLf

    '            If C_TotalRowDe.Text = count_data Then
    '                txtT_product.Text = All_str & _
    '                "TOTAL: " + BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) + vbCrLf + "        " + _
    '                                                            txtplace_exibition.Text + vbCrLf + _
    '                                                            "       _____________________________"
    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 7 'T1_third_country,T2_back_country
    '            All_str = "HS. CODE. " + tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) + vbCrLf _
    '                                        + txtproduct_n1.Text + txtproduct_n2.Text + " ****" + vbCrLf

    '            If C_TotalRowDe.Text = count_data Then
    '                txtT_product.Text = All_str & _
    '                "TOTAL: " + BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) + vbCrLf + "        " + _
    '                                                            txtthird_country.Text + vbCrLf + _
    '                                                            txtback_country.Text + vbCrLf + _
    '                                                            "       _____________________________"
    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 8 'T1_third_country,T3_place_exibition
    '            All_str = "HS. CODE. " + tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) + vbCrLf _
    '                                        + txtproduct_n1.Text + txtproduct_n2.Text + " ****" + vbCrLf

    '            If C_TotalRowDe.Text = count_data Then
    '                txtT_product.Text = All_str & _
    '                "TOTAL: " + BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) + vbCrLf + "        " + _
    '                                                            txtthird_country.Text + vbCrLf + _
    '                                                            txtplace_exibition.Text + vbCrLf + _
    '                                                            "       _____________________________"
    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 9 'T1_third_country
    '            All_str = "HS. CODE. " + tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) + vbCrLf _
    '                                        + txtproduct_n1.Text + txtproduct_n2.Text + " ****" + vbCrLf

    '            If C_TotalRowDe.Text = count_data Then
    '                txtT_product.Text = All_str & _
    '                "TOTAL: " + BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) + vbCrLf + "        " + _
    '                                                                txtthird_country.Text + vbCrLf + _
    '                                                                "       _____________________________"
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
    '            All_str = "HS. CODE. " + tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) + vbCrLf _
    '                                        + txtproduct_n1.Text + txtproduct_n2.Text + " ****" + vbCrLf

    '            If C_TotalRowDe.Text = count_data Then
    '                txtT_product.Text = All_str + "        " & _
    '                txtthird_country.Text + vbCrLf + _
    '                                                            txtback_country.Text + vbCrLf + _
    '                                                            txtplace_exibition.Text + vbCrLf + _
    '                                                            "       _____________________________"
    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 2 'T2_back_country,T3_place_exibition
    '            All_str = "HS. CODE. " + tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) + vbCrLf _
    '                                                        + txtproduct_n1.Text + txtproduct_n2.Text + " ****" + vbCrLf

    '            If C_TotalRowDe.Text = count_data Then
    '                txtT_product.Text = All_str & _
    '                txtback_country.Text + vbCrLf + _
    '                                                                            txtplace_exibition.Text + vbCrLf + _
    '                                                                            "       _____________________________"
    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 3 'T3_place_exibition
    '            All_str = "HS. CODE. " + tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) + vbCrLf _
    '                                        + txtproduct_n1.Text + txtproduct_n2.Text + " ****" + vbCrLf

    '            If C_TotalRowDe.Text = count_data Then
    '                txtT_product.Text = All_str & _
    '                 txtplace_exibition.Text + vbCrLf + _
    '                                                            "       _____________________________"
    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 4 'ไม่มีเลย
    '            All_str = "HS. CODE. " + tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) + vbCrLf _
    '                                        + txtproduct_n1.Text + txtproduct_n2.Text + " ****" + vbCrLf

    '            If C_TotalRowDe.Text = count_data Then
    '                txtT_product.Text = All_str & _
    '                 "       _____________________________"
    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 5 'T2_back_country
    '            All_str = "HS. CODE. " + tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) + vbCrLf _
    '                                        + txtproduct_n1.Text + txtproduct_n2.Text + " ****" + vbCrLf

    '            If C_TotalRowDe.Text = count_data Then
    '                txtT_product.Text = All_str & _
    '                vbCrLf + "       _____________________________"
    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 6 'T3_place_exibition
    '            All_str = "HS. CODE. " + tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) + vbCrLf _
    '                                        + txtproduct_n1.Text + txtproduct_n2.Text + " ****" + vbCrLf

    '            If C_TotalRowDe.Text = count_data Then
    '                txtT_product.Text = All_str & _
    '                 txtplace_exibition.Text + vbCrLf + _
    '                                                            "       _____________________________"
    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 7 'T1_third_country,T2_back_country
    '            All_str = "HS. CODE. " + tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) + vbCrLf _
    '                                        + txtproduct_n1.Text + txtproduct_n2.Text + " ****" + vbCrLf

    '            If C_TotalRowDe.Text = count_data Then
    '                txtT_product.Text = All_str & _
    '                txtthird_country.Text + vbCrLf + _
    '                                                            txtback_country.Text + vbCrLf + _
    '                                                            "       _____________________________"
    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 8 'T1_third_country,T3_place_exibition
    '            All_str = "HS. CODE. " + tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) + vbCrLf _
    '                                        + txtproduct_n1.Text + txtproduct_n2.Text + " ****" + vbCrLf

    '            If C_TotalRowDe.Text = count_data Then
    '                txtT_product.Text = All_str & _
    '                 txtthird_country.Text + vbCrLf + _
    '                                                            txtplace_exibition.Text + vbCrLf + _
    '                                                            "       _____________________________"
    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 9 'T1_third_country
    '            All_str = "HS. CODE. " + tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) + vbCrLf _
    '                                        + txtproduct_n1.Text + txtproduct_n2.Text + " ****" + vbCrLf

    '            If C_TotalRowDe.Text = count_data Then
    '                txtT_product.Text = All_str & _
    '                txtthird_country.Text + vbCrLf + _
    '                                                                "       _____________________________"
    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '    End Select
    'End Sub

    'Sub Count_RowToPage_DetailData()
    '    Dim CountData_T, CountData_T2 As Integer
    '    CountData_T = C_TotalRowDe.Text
    '    CountData_T2 = CountData_T / 7

    '    Dim i As Integer
    '    txtNumRowCount.Text += 1
    '    For i = 0 To CountData_T2
    '        Dim arrnum(i) As Integer

    '        arrnum(i) = i * 7 'ทำเพื่อเช็คจำนวนเปรียบเทียบ

    '        If (txtNumRowCount.Text = arrnum(i)) = True Then
    '            Me.Detail1.NewPage = NewPage.Before
    '            txtmarks.Visible = True
    '            'txtgross_weight1.Visible = True
    '            txtTolInvoice.Visible = True
    '            Exit For
    '        Else
    '            '===============================================================
    '            'If CommonUtility.Get_StringValue(txtgross_weight.Text) <> "" Then
    '            '    txtgross_weight1.Text = txtTemp_Gross_Weight.Text + " " + txtg_Unit_Desc.Text
    '            'Else
    '            '    txtgross_weight1.Text = ""
    '            'End If
    '            '============================================================
    '            CallInvoiceCheck()

    '            CHeck_DisPlay()
    '            '============================================================
    '            If txtNumRowCount.Text = C_TotalRowDe.Text Then

    '                If (CommonUtility.Get_StringValue(txtquantity1.Text) <> "" And CommonUtility.Get_StringValue(txtquantity1.Text) <> "0.0000" And CommonUtility.Get_StringValue(txtq_unit_code1.Text) <> "") = True And _
    '                (CommonUtility.Get_StringValue(txtquantity2.Text) <> "" And CommonUtility.Get_StringValue(txtquantity2.Text) <> "0.0000" And CommonUtility.Get_StringValue(txtq_unit_code2.Text) <> "") = True And _
    '                (CommonUtility.Get_StringValue(txtquantity3.Text) <> "" And CommonUtility.Get_StringValue(txtquantity3.Text) <> "0.0000" And CommonUtility.Get_StringValue(txtq_unit_code3.Text) <> "") = True And _
    '                (CommonUtility.Get_StringValue(txtquantity4.Text) <> "" And CommonUtility.Get_StringValue(txtquantity4.Text) <> "0.0000" And CommonUtility.Get_StringValue(txtq_unit_code4.Text) <> "") = True And _
    '                (CommonUtility.Get_StringValue(txtquantity5.Text) <> "" And CommonUtility.Get_StringValue(txtquantity5.Text) <> "0.0000" And CommonUtility.Get_StringValue(txtq_unit_code5.Text) <> "") = True Then
    '                    'มีหมด
    '                    All_CheckThird_5()
    '                    'If CommonUtility.Get_StringValue(txtthird_country.Value) <> "" Then
    '                    '    txtT_product.Text = "HS. CODE. " + Mid(CommonUtility.Get_StringValue(txttariff_code.Text), 1, 6) _
    '                    '                            + txtproduct_n1.Text + txtproduct_n2.Text + "****" + vbCrLf + _
    '                    '                            "TOTAL: " + BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) + vbCrLf + "        " + _
    '                    '                            BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) + vbCrLf + "        " + _
    '                    '                            BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) + vbCrLf + "        " + _
    '                    '                            BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) + vbCrLf + "        " + _
    '                    '                            BahtOnly(txtquantity5.Text, txtq_unit_code5.Text) + vbCrLf + txtthird_country.Text + vbCrLf + _
    '                    '                            "_____________________________"
    '                    'Else
    '                    '    txtT_product.Text = "HS. CODE. " + Mid(CommonUtility.Get_StringValue(txttariff_code.Text), 1, 6) _
    '                    '                            + txtproduct_n1.Text + txtproduct_n2.Text + "****" + vbCrLf + _
    '                    '                            "TOTAL: " + BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) + vbCrLf + "        " + _
    '                    '                            BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) + vbCrLf + "        " + _
    '                    '                            BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) + vbCrLf + "        " + _
    '                    '                            BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) + vbCrLf + "        " + _
    '                    '                            BahtOnly(txtquantity5.Text, txtq_unit_code5.Text) + vbCrLf + _
    '                    '                            "_____________________________"
    '                    'End If
    '                ElseIf (CommonUtility.Get_StringValue(txtquantity1.Text) <> "" And CommonUtility.Get_StringValue(txtquantity1.Text) <> "0.0000" And CommonUtility.Get_StringValue(txtq_unit_code1.Text) <> "") = True And _
    '                (CommonUtility.Get_StringValue(txtquantity2.Text) <> "" And CommonUtility.Get_StringValue(txtquantity2.Text) <> "0.0000" And CommonUtility.Get_StringValue(txtq_unit_code2.Text) <> "") = True And _
    '                (CommonUtility.Get_StringValue(txtquantity3.Text) <> "" And CommonUtility.Get_StringValue(txtquantity3.Text) <> "0.0000" And CommonUtility.Get_StringValue(txtq_unit_code3.Text) <> "") = True And _
    '                (CommonUtility.Get_StringValue(txtquantity4.Text) <> "" And CommonUtility.Get_StringValue(txtquantity4.Text) <> "0.0000" And CommonUtility.Get_StringValue(txtq_unit_code4.Text) <> "") = True And _
    '                (CommonUtility.Get_StringValue(txtquantity5.Text) <> "" And CommonUtility.Get_StringValue(txtquantity5.Text) <> "0.0000" And CommonUtility.Get_StringValue(txtq_unit_code5.Text) <> "") = False Then
    '                    'ขาด 5
    '                    All_CheckThird_4()
    '                    'If CommonUtility.Get_StringValue(txtthird_country.Value) <> "" Then
    '                    '    txtT_product.Text = "HS. CODE. " + Mid(CommonUtility.Get_StringValue(txttariff_code.Text), 1, 6) _
    '                    '                            + txtproduct_n1.Text + txtproduct_n2.Text + "****" + vbCrLf + _
    '                    '                            "TOTAL: " + BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) + vbCrLf + "        " + _
    '                    '                            BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) + vbCrLf + "        " + _
    '                    '                            BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) + vbCrLf + "        " + _
    '                    '                            BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) + vbCrLf + txtthird_country.Text + vbCrLf + _
    '                    '                            "_____________________________"
    '                    'Else
    '                    '    txtT_product.Text = "HS. CODE. " + Mid(CommonUtility.Get_StringValue(txttariff_code.Text), 1, 6) _
    '                    '                            + txtproduct_n1.Text + txtproduct_n2.Text + "****" + vbCrLf + _
    '                    '                            "TOTAL: " + BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) + vbCrLf + "        " + _
    '                    '                            BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) + vbCrLf + "        " + _
    '                    '                            BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) + vbCrLf + "        " + _
    '                    '                            BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) + vbCrLf + _
    '                    '                            "_____________________________"
    '                    'End If


    '                ElseIf (CommonUtility.Get_StringValue(txtquantity1.Text) <> "" And CommonUtility.Get_StringValue(txtquantity1.Text) <> "0.0000" And CommonUtility.Get_StringValue(txtq_unit_code1.Text) <> "") = True And _
    '                 (CommonUtility.Get_StringValue(txtquantity2.Text) <> "" And CommonUtility.Get_StringValue(txtquantity2.Text) <> "0.0000" And CommonUtility.Get_StringValue(txtq_unit_code2.Text) <> "") = True And _
    '                 (CommonUtility.Get_StringValue(txtquantity3.Text) <> "" And CommonUtility.Get_StringValue(txtquantity3.Text) <> "0.0000" And CommonUtility.Get_StringValue(txtq_unit_code3.Text) <> "") = True And _
    '                 (CommonUtility.Get_StringValue(txtquantity4.Text) <> "" And CommonUtility.Get_StringValue(txtquantity4.Text) <> "0.0000" And CommonUtility.Get_StringValue(txtq_unit_code4.Text) <> "") = False And _
    '                 (CommonUtility.Get_StringValue(txtquantity5.Text) <> "" And CommonUtility.Get_StringValue(txtquantity5.Text) <> "0.0000" And CommonUtility.Get_StringValue(txtq_unit_code5.Text) <> "") = False Then
    '                    'ขาด 4,5
    '                    All_CheckThird_3()
    '                    'If CommonUtility.Get_StringValue(txtthird_country.Value) <> "" Then
    '                    '    txtT_product.Text = "HS. CODE. " + Mid(CommonUtility.Get_StringValue(txttariff_code.Text), 1, 6) _
    '                    '                            + txtproduct_n1.Text + txtproduct_n2.Text + "****" + vbCrLf + _
    '                    '                            "TOTAL: " + BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) + vbCrLf + "        " + _
    '                    '                            BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) + vbCrLf + "        " + _
    '                    '                            BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) + vbCrLf + txtthird_country.Text + vbCrLf + _
    '                    '                            "_____________________________"
    '                    'Else
    '                    '    txtT_product.Text = "HS. CODE. " + Mid(CommonUtility.Get_StringValue(txttariff_code.Text), 1, 6) _
    '                    '                            + txtproduct_n1.Text + txtproduct_n2.Text + "****" + vbCrLf + _
    '                    '                            "TOTAL: " + BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) + vbCrLf + "        " + _
    '                    '                            BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) + vbCrLf + "        " + _
    '                    '                            BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) + vbCrLf + _
    '                    '                            "_____________________________"
    '                    'End If

    '                ElseIf (CommonUtility.Get_StringValue(txtquantity1.Text) <> "" And CommonUtility.Get_StringValue(txtquantity1.Text) <> "0.0000" And CommonUtility.Get_StringValue(txtq_unit_code1.Text) <> "") = True And _
    '                  (CommonUtility.Get_StringValue(txtquantity2.Text) <> "" And CommonUtility.Get_StringValue(txtquantity2.Text) <> "0.0000" And CommonUtility.Get_StringValue(txtq_unit_code2.Text) <> "") = True And _
    '                  (CommonUtility.Get_StringValue(txtquantity3.Text) <> "" And CommonUtility.Get_StringValue(txtquantity3.Text) <> "0.0000" And CommonUtility.Get_StringValue(txtq_unit_code3.Text) <> "") = False And _
    '                  (CommonUtility.Get_StringValue(txtquantity4.Text) <> "" And CommonUtility.Get_StringValue(txtquantity4.Text) <> "0.0000" And CommonUtility.Get_StringValue(txtq_unit_code4.Text) <> "") = False And _
    '                  (CommonUtility.Get_StringValue(txtquantity5.Text) <> "" And CommonUtility.Get_StringValue(txtquantity5.Text) <> "0.0000" And CommonUtility.Get_StringValue(txtq_unit_code5.Text) <> "") = False Then
    '                    'ขาด 3,4,5
    '                    All_CheckThird_2()
    '                    'If CommonUtility.Get_StringValue(txtthird_country.Value) <> "" Then
    '                    '    txtT_product.Text = "HS. CODE. " + Mid(CommonUtility.Get_StringValue(txttariff_code.Text), 1, 6) _
    '                    '                            + txtproduct_n1.Text + txtproduct_n2.Text + "****" + vbCrLf + _
    '                    '                            "TOTAL: " + BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) + vbCrLf + "        " + _
    '                    '                            BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) + vbCrLf + txtthird_country.Text + vbCrLf + _
    '                    '                            "_____________________________"

    '                    'Else
    '                    '    txtT_product.Text = "HS. CODE. " + Mid(CommonUtility.Get_StringValue(txttariff_code.Text), 1, 6) _
    '                    '                            + txtproduct_n1.Text + txtproduct_n2.Text + "****" + vbCrLf + _
    '                    '                            "TOTAL: " + BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) + vbCrLf + "        " + _
    '                    '                            BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) + vbCrLf + _
    '                    '                            "_____________________________"

    '                    'End If

    '                ElseIf (CommonUtility.Get_StringValue(txtquantity1.Text) <> "" And CommonUtility.Get_StringValue(txtquantity1.Text) <> "0.0000" And CommonUtility.Get_StringValue(txtq_unit_code1.Text) <> "") = True And _
    '                  (CommonUtility.Get_StringValue(txtquantity2.Text) <> "" And CommonUtility.Get_StringValue(txtquantity2.Text) <> "0.0000" And CommonUtility.Get_StringValue(txtq_unit_code2.Text) <> "") = False And _
    '                  (CommonUtility.Get_StringValue(txtquantity3.Text) <> "" And CommonUtility.Get_StringValue(txtquantity3.Text) <> "0.0000" And CommonUtility.Get_StringValue(txtq_unit_code3.Text) <> "") = False And _
    '                  (CommonUtility.Get_StringValue(txtquantity4.Text) <> "" And CommonUtility.Get_StringValue(txtquantity4.Text) <> "0.0000" And CommonUtility.Get_StringValue(txtq_unit_code4.Text) <> "") = False And _
    '                  (CommonUtility.Get_StringValue(txtquantity5.Text) <> "" And CommonUtility.Get_StringValue(txtquantity5.Text) <> "0.0000" And CommonUtility.Get_StringValue(txtq_unit_code5.Text) <> "") = False Then
    '                    'ขาด 2,3,4,5
    '                    All_CheckThird_1()
    '                    'If CommonUtility.Get_StringValue(txtthird_country.Value) <> "" Then
    '                    '    txtT_product.Text = "HS. CODE. " + Mid(CommonUtility.Get_StringValue(txttariff_code.Text), 1, 6) _
    '                    '                            + txtproduct_n1.Text + txtproduct_n2.Text + "****" + vbCrLf + _
    '                    '                            "TOTAL: " + BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) + vbCrLf + txtthird_country.Text + vbCrLf + _
    '                    '                            "_____________________________"
    '                    'Else
    '                    '    txtT_product.Text = "HS. CODE. " + Mid(CommonUtility.Get_StringValue(txttariff_code.Text), 1, 6) _
    '                    '                            + txtproduct_n1.Text + txtproduct_n2.Text + "****" + vbCrLf + _
    '                    '                            "TOTAL: " + BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) + vbCrLf + _
    '                    '                            "_____________________________"
    '                    'End If

    '                Else
    '                    All_CheckThird_0()
    '                    'If CommonUtility.Get_StringValue(txtthird_country.Value) <> "" Then
    '                    '    txtT_product.Text = "HS. CODE. " + Mid(CommonUtility.Get_StringValue(txttariff_code.Text), 1, 6) _
    '                    '                                                    + txtproduct_n1.Text + txtproduct_n2.Text + "****" + vbCrLf + txtthird_country.Text + vbCrLf + _
    '                    '                            "_____________________________"
    '                    'Else
    '                    '    txtT_product.Text = "HS. CODE. " + Mid(CommonUtility.Get_StringValue(txttariff_code.Text), 1, 6) _
    '                    '                                                    + txtproduct_n1.Text + txtproduct_n2.Text + "****" + vbCrLf + _
    '                    '                            "_____________________________"
    '                    'End If

    '                End If

    '            Else
    '                txtT_product.Text = "HS. CODE. " + Mid(CommonUtility.Get_StringValue(txttariff_code.Text), 1, 6) + " " _
    '                + txtproduct_n1.Text + txtproduct_n2.Text + "****"
    '            End If

    '            '============================================================
    '            If CommonUtility.Get_StringValue(txtNumRowCount.Text) = "1" Then
    '                txtmarks.Visible = True
    '                'txtgross_weight1.Visible = True
    '                txtTolInvoice.Visible = True
    '            Else
    '                txtmarks.Visible = False
    '                'txtgross_weight1.Visible = False
    '                txtTolInvoice.Visible = False
    '            End If

    '            Me.Detail1.NewPage = NewPage.None
    '        End If

    '    Next


    'End Sub
    Sub check_Issued_()
        Dim date_now As Date = Now.Date
        Dim set_issued As Integer

        Dim str_1, str_2, str_3 As String
        str_1 = CommonUtility.Get_DateTime(txtdeparture_date.Value).Day
        str_2 = CInt(str_1) + 1
        str_3 = CInt(str_1) + 2

        'เฉพาะในนี้ไม่เกี่ยวกับตอน print ย้อน ถ้าย้อน issued มีก็ต้องมี
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
        txtcompany_provincefoot.Text = txtcompany_province.Value & " " & format_dateSelect()

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

        'by rut rvc new
        TGross += Check_Null(txtgross_weightD.Value)
        TUSD += Check_Null(txtUSDInvoiceDetail.Value)
        'by rut FOB Other
        TUSDOther += Check_Null(txtPriceOtherDetail.Value)
        'by rut FOBOther
        If Cpage = 1 Then 'invoice ต่างประเทศ แสดงมูลค่า รายการแรกรายการเดียว
            Str_USDinvoiceDetail = Format(CommonUtility.Get_Decimal(txtUSDInvoiceDetail.Text), "#,##0.00##") & " " & txtCurrency_Code.Text
        End If

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

        'by rut rvc new
        'by rut rvc
        Check_CaseRVCCount = CInt(txtCheck_CaseRVCCount.Text)
    End Sub

    Private Sub rpt3_ediFORM4_8_pr_PageEnd(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PageEnd
        head_height = PageHeader1.Height

        Dim f As New System.Drawing.Font("BrowalliaUPC", 11)

        Dim xm As Single = 6.94
        Dim ym As Single = CSng(head_height) '4.875
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
        'by rut rvc new
        Select Case CommonUtility.Get_StringValue(txtCheckGrossDetail.Text)
            Case Is <> "GWDetail"
                Dim x_txtGrossTxt As Single = 6
                Dim y_txtGrossTxt As Single = CSng(head_height) '4.875
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
                    .DrawText(CHeck_DisPlayF(CommonUtility.Get_StringValue(C_TotalRowDe.Text), CommonUtility.Get_StringValue(txtCheckGrossDetail.Text)), drawRect_txtGrossTxt)
                End With
        End Select
        'Dim x_txtGrossTxt As Single = 6
        'Dim y_txtGrossTxt As Single = CSng(head_height) '4.875
        ''Dim width As Single = 6.0F
        'Dim width_txtGrossTxt As Single = 0.94
        'Dim height_txtGrossTxt As Single = 4
        'Dim drawRect_txtGrossTxt As New Drawing.RectangleF(x_txtGrossTxt, y_txtGrossTxt, width_txtGrossTxt, height_txtGrossTxt)

        'With Me.CurrentPage
        '    .Font = f
        '    .ForeColor = System.Drawing.Color.Blue
        '    .TextAlignment = DataDynamics.ActiveReports.TextAlignment.Center
        '    .VerticalTextAlignment = VerticalTextAlignment.Top
        '    '.BackColor = Drawing.Color.Green
        '    '.TextAngle = 900
        '    '.VerticalTextAlignment = DataDynamics.ActiveReports.VerticalTextAlignment.Middle
        '    .DrawText(CHeck_DisPlayF, drawRect_txtGrossTxt)
        'End With
        '============================
        Dim x_txtmarks As Single = 0.83
        Dim y_txtmarks As Single = CSng(head_height) '4.875
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

    Private Sub rpt3_ediFORM4_8_pr_PageStart(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PageStart
        Me.CurrentPage.DrawLine(0.06, 4.25, 0.06, 8.2)
        Me.CurrentPage.DrawLine(0.75, 4.25, 0.75, 8.2)
        Me.CurrentPage.DrawLine(1.8, 4.25, 1.8, 8.2)
        Me.CurrentPage.DrawLine(5.1, 4.25, 5.1, 8.2)
        Me.CurrentPage.DrawLine(6, 4.25, 6, 8.2)
        Me.CurrentPage.DrawLine(6.9, 4.25, 6.9, 8.2)
        Me.CurrentPage.DrawLine(8.19, 4.25, 8.19, 8.2)
    End Sub

    Private Sub rpt3_ediFORM4_8_pr_ReportEnd(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.ReportEnd
        ''If updatePrintTotal(txtinvh_run_auto.Value, CStr(Me.Document.Pages.Count)) = True Then

        ''End If
    End Sub

    Private Sub rpt3_ediFORM4_8_pr_ReportStart(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.ReportStart
        'Me.ReportInfo1.FormatString = "Page {PageNumber} of {PageCount} on {RunDateTime}"
        Me.ReportInfo1.FormatString = "Page : {PageNumber} of {PageCount}"

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

    Sub comCheck()
        If (Len(CommonUtility.Get_StringValue(txtcompany_phone.Value)) <> 0) And (Len(CommonUtility.Get_StringValue(txtcompany_fax.Value)) <> 0) And (Len(CommonUtility.Get_StringValue(txtcompany_taxno.Value)) <> 0) And (Mid(CommonUtility.Get_StringValue(txtdest_remark.Value), 1, 3)) = "C/O" Then

            txtCompany_Check_1.Text = (Mid(CommonUtility.Get_StringValue(txtob_address.Value), 1, 255)) + " CARE OF " + CommonUtility.Get_StringValue(txtcompany_name.Value) + " " + CommonUtility.Get_StringValue(txtcompany_address.Value) + " " + CommonUtility.Get_StringValue(txtcompany_province.Value) + " " + CommonUtility.Get_StringValue(txtcompany_country.Value) + " TEL: " + CommonUtility.Get_StringValue(txtcompany_phone.Value) + " FAX: " + CommonUtility.Get_StringValue(txtcompany_fax.Value) + " TAX ID:  " + CommonUtility.Get_StringValue(txtcompany_taxno.Value)

        ElseIf (Len(CommonUtility.Get_StringValue(txtcompany_phone.Value)) <> 0) And (Len(CommonUtility.Get_StringValue(txtcompany_fax.Value)) <> 0) And (Len(CommonUtility.Get_StringValue(txtcompany_taxno.Value)) <> 0) And (Mid(CommonUtility.Get_StringValue(txtdest_remark.Value), 1, 3)) = "O/B" Then

            txtCompany_Check_1.Text = (CommonUtility.Get_StringValue(txtcompany_name.Value) + "  " + CommonUtility.Get_StringValue(txtcompany_address.Value) + " " + CommonUtility.Get_StringValue(txtcompany_province.Value) + " " + CommonUtility.Get_StringValue(txtcompany_country.Value) + " TEL: " + CommonUtility.Get_StringValue(txtcompany_phone.Value) + " FAX: " + CommonUtility.Get_StringValue(txtcompany_fax.Value) + " TAX ID:  " + CommonUtility.Get_StringValue(txtcompany_taxno.Value) + " ON BEHALF OF " + Mid(CommonUtility.Get_StringValue(txtob_address.Value), 1, 255))

        ElseIf (Len(CommonUtility.Get_StringValue(txtcompany_phone.Value)) <> 0) And (Len(CommonUtility.Get_StringValue(txtcompany_fax.Value)) <> 0) And (Len(CommonUtility.Get_StringValue(txtcompany_taxno.Value)) <> 0) And (Mid(CommonUtility.Get_StringValue(txtdest_remark.Value), 2, 1)) <> "/" Then

            txtCompany_Check_1.Text = (CommonUtility.Get_StringValue(txtcompany_name.Value) + "  " + CommonUtility.Get_StringValue(txtcompany_address.Value) + " " + CommonUtility.Get_StringValue(txtcompany_province.Value) + " " + CommonUtility.Get_StringValue(txtcompany_country.Value) + " TEL: " + CommonUtility.Get_StringValue(txtcompany_phone.Value) + " FAX: " + CommonUtility.Get_StringValue(txtcompany_fax.Value) + " TAX ID:  " + CommonUtility.Get_StringValue(txtcompany_taxno.Value))

        ElseIf (Len(CommonUtility.Get_StringValue(txtcompany_phone.Value)) <> 0) And (Len(CommonUtility.Get_StringValue(txtcompany_fax.Value)) = 0) And (Len(CommonUtility.Get_StringValue(txtcompany_taxno.Value)) <> 0) And (Mid(CommonUtility.Get_StringValue(txtdest_remark.Value), 1, 3)) = "C/O" Then

            txtCompany_Check_1.Text = (Mid(CommonUtility.Get_StringValue(txtob_address.Value), 1, 255) + " CARE OF " + CommonUtility.Get_StringValue(txtcompany_name.Value) + " " + CommonUtility.Get_StringValue(txtcompany_address.Value) + " " + CommonUtility.Get_StringValue(txtcompany_province.Value) + " " + CommonUtility.Get_StringValue(txtcompany_country.Value) + " TEL: " + CommonUtility.Get_StringValue(txtcompany_phone.Value) + " TAX ID:  " + CommonUtility.Get_StringValue(txtcompany_taxno.Value))

        ElseIf (Len(CommonUtility.Get_StringValue(txtcompany_phone.Value)) <> 0) And (Len(CommonUtility.Get_StringValue(txtcompany_fax.Value)) = 0) And (Len(CommonUtility.Get_StringValue(txtcompany_taxno.Value)) = 0) And (Mid(CommonUtility.Get_StringValue(txtdest_remark.Value), 1, 3)) = "C/O" Then

            txtCompany_Check_1.Text = (Mid(CommonUtility.Get_StringValue(txtob_address.Value), 1, 255) + " CARE OF " + CommonUtility.Get_StringValue(txtcompany_name.Value) + " " + CommonUtility.Get_StringValue(txtcompany_address.Value) + " " + CommonUtility.Get_StringValue(txtcompany_province.Value) + " " + CommonUtility.Get_StringValue(txtcompany_country.Value) + " TEL: " + CommonUtility.Get_StringValue(txtcompany_phone.Value))

        ElseIf (Len(CommonUtility.Get_StringValue(txtcompany_phone.Value)) = 0) And (Len(CommonUtility.Get_StringValue(txtcompany_fax.Value)) <> 0) And (Len(CommonUtility.Get_StringValue(txtcompany_taxno.Value)) = 0) And (Mid(CommonUtility.Get_StringValue(txtdest_remark.Value), 1, 3)) = "C/O" Then

            txtCompany_Check_1.Text = (Mid(CommonUtility.Get_StringValue(txtob_address.Value), 1, 255) + " CARE OF " + CommonUtility.Get_StringValue(txtcompany_name.Value) + " " + CommonUtility.Get_StringValue(txtcompany_address.Value) + " " + CommonUtility.Get_StringValue(txtcompany_province.Value) + " " + CommonUtility.Get_StringValue(txtcompany_country.Value) + " FAX: " + CommonUtility.Get_StringValue(txtcompany_fax.Value))

        ElseIf (Len(CommonUtility.Get_StringValue(txtcompany_phone.Value)) = 0) And (Len(CommonUtility.Get_StringValue(txtcompany_fax.Value)) <> 0) And (Len(CommonUtility.Get_StringValue(txtcompany_taxno.Value)) <> 0) And (Mid(CommonUtility.Get_StringValue(txtdest_remark.Value), 1, 3)) = "C/O" Then

            txtCompany_Check_1.Text = (Mid(CommonUtility.Get_StringValue(txtob_address.Value), 1, 255) + " CARE OF " + CommonUtility.Get_StringValue(txtcompany_name.Value) + " " + CommonUtility.Get_StringValue(txtcompany_address.Value) + " " + CommonUtility.Get_StringValue(txtcompany_province.Value) + " " + CommonUtility.Get_StringValue(txtcompany_country.Value) + " FAX: " + CommonUtility.Get_StringValue(txtcompany_fax.Value) + " TAX ID:  " + CommonUtility.Get_StringValue(txtcompany_taxno.Value))

        ElseIf (Len(CommonUtility.Get_StringValue(txtcompany_phone.Value)) <> 0) And (Len(CommonUtility.Get_StringValue(txtcompany_fax.Value)) = 0) And (Len(CommonUtility.Get_StringValue(txtcompany_taxno.Value)) <> 0) And (Mid(CommonUtility.Get_StringValue(txtdest_remark.Value), 1, 3)) = "O/B" Then

            txtCompany_Check_1.Text = (CommonUtility.Get_StringValue(txtcompany_name.Value) + "  " + CommonUtility.Get_StringValue(txtcompany_address.Value) + " " + CommonUtility.Get_StringValue(txtcompany_province.Value) + " " + CommonUtility.Get_StringValue(txtcompany_country.Value) + " TEL: " + CommonUtility.Get_StringValue(txtcompany_phone.Value) + " TAX ID:  " + CommonUtility.Get_StringValue(txtcompany_taxno.Value) + " ON BEHALF OF " + Mid(CommonUtility.Get_StringValue(txtob_address.Value), 1, 255))

        ElseIf (Len(CommonUtility.Get_StringValue(txtcompany_phone.Value)) <> 0) And (Len(CommonUtility.Get_StringValue(txtcompany_fax.Value)) = 0) And (Len(CommonUtility.Get_StringValue(txtcompany_taxno.Value)) = 0) And (Mid(CommonUtility.Get_StringValue(txtdest_remark.Value), 1, 3)) = "O/B" Then

            txtCompany_Check_1.Text = (CommonUtility.Get_StringValue(txtcompany_name.Value) + "  " + CommonUtility.Get_StringValue(txtcompany_address.Value) + " " + CommonUtility.Get_StringValue(txtcompany_province.Value) + " " + CommonUtility.Get_StringValue(txtcompany_country.Value) + " TEL: " + CommonUtility.Get_StringValue(txtcompany_phone.Value) + " ON BEHALF OF " + Mid(CommonUtility.Get_StringValue(txtob_address.Value), 1, 255))

        ElseIf (Len(CommonUtility.Get_StringValue(txtcompany_phone.Value)) = 0) And (Len(CommonUtility.Get_StringValue(txtcompany_fax.Value)) <> 0) And (Len(CommonUtility.Get_StringValue(txtcompany_taxno.Value)) = 0) And (Mid(CommonUtility.Get_StringValue(txtdest_remark.Value), 1, 3)) = "O/B" Then

            txtCompany_Check_1.Text = (CommonUtility.Get_StringValue(txtcompany_name.Value) + "  " + CommonUtility.Get_StringValue(txtcompany_address.Value) + " " + CommonUtility.Get_StringValue(txtcompany_province.Value) + " " + CommonUtility.Get_StringValue(txtcompany_country.Value) + " FAX: " + CommonUtility.Get_StringValue(txtcompany_fax.Value) + " ON BEHALF OF " + Mid(CommonUtility.Get_StringValue(txtob_address.Value), 1, 255))

        ElseIf (Len(CommonUtility.Get_StringValue(txtcompany_phone.Value)) = 0) And (Len(CommonUtility.Get_StringValue(txtcompany_fax.Value)) <> 0) And (Len(CommonUtility.Get_StringValue(txtcompany_taxno.Value)) <> 0) And (Mid(CommonUtility.Get_StringValue(txtdest_remark.Value), 1, 3)) = "O/B" Then

            txtCompany_Check_1.Text = (CommonUtility.Get_StringValue(txtcompany_name.Value) + "  " + CommonUtility.Get_StringValue(txtcompany_address.Value) + " " + CommonUtility.Get_StringValue(txtcompany_province.Value) + " " + CommonUtility.Get_StringValue(txtcompany_country.Value) + " FAX: " + CommonUtility.Get_StringValue(txtcompany_fax.Value) + " TAX ID:  " + CommonUtility.Get_StringValue(txtcompany_taxno.Value) + " ON BEHALF OF " + Mid(CommonUtility.Get_StringValue(txtob_address.Value), 1, 255))

        ElseIf (Len(CommonUtility.Get_StringValue(txtcompany_phone.Value)) <> 0) And (Len(CommonUtility.Get_StringValue(txtcompany_fax.Value)) = 0) And (Len(CommonUtility.Get_StringValue(txtcompany_taxno.Value)) <> 0) And (Mid(CommonUtility.Get_StringValue(txtdest_remark.Value), 2, 1)) <> "/" Then

            txtCompany_Check_1.Text = (CommonUtility.Get_StringValue(txtcompany_name.Value) + "  " + CommonUtility.Get_StringValue(txtcompany_address.Value) + " " + CommonUtility.Get_StringValue(txtcompany_province.Value) + " " + CommonUtility.Get_StringValue(txtcompany_country.Value) + " TEL: " + CommonUtility.Get_StringValue(txtcompany_phone.Value) + " TAX ID:  " + CommonUtility.Get_StringValue(txtcompany_taxno.Value))

        ElseIf (Len(CommonUtility.Get_StringValue(txtcompany_phone.Value)) <> 0) And (Len(CommonUtility.Get_StringValue(txtcompany_fax.Value)) = 0) And (Len(CommonUtility.Get_StringValue(txtcompany_taxno.Value)) = 0) And (Mid(CommonUtility.Get_StringValue(txtdest_remark.Value), 2, 1)) <> "/" Then

            txtCompany_Check_1.Text = (CommonUtility.Get_StringValue(txtcompany_name.Value) + "  " + CommonUtility.Get_StringValue(txtcompany_address.Value) + " " + CommonUtility.Get_StringValue(txtcompany_province.Value) + " " + CommonUtility.Get_StringValue(txtcompany_country.Value) + " TEL: " + CommonUtility.Get_StringValue(txtcompany_phone.Value))

        ElseIf (Len(CommonUtility.Get_StringValue(txtcompany_phone.Value)) = 0) And (Len(CommonUtility.Get_StringValue(txtcompany_fax.Value)) <> 0) And (Len(CommonUtility.Get_StringValue(txtcompany_taxno.Value)) = 0) And (Mid(CommonUtility.Get_StringValue(txtdest_remark.Value), 2, 1)) <> "/" Then

            txtCompany_Check_1.Text = (CommonUtility.Get_StringValue(txtcompany_name.Value) + "  " + CommonUtility.Get_StringValue(txtcompany_address.Value) + " " + CommonUtility.Get_StringValue(txtcompany_province.Value) + " " + CommonUtility.Get_StringValue(txtcompany_country.Value) + " FAX: " + CommonUtility.Get_StringValue(txtcompany_fax.Value))

        ElseIf (Len(CommonUtility.Get_StringValue(txtcompany_phone.Value)) = 0) And (Len(CommonUtility.Get_StringValue(txtcompany_fax.Value)) <> 0) And (Len(CommonUtility.Get_StringValue(txtcompany_taxno.Value)) <> 0) And (Mid(CommonUtility.Get_StringValue(txtdest_remark.Value), 2, 1)) <> "/" Then

            txtCompany_Check_1.Text = (CommonUtility.Get_StringValue(txtcompany_name.Value) + "  " + CommonUtility.Get_StringValue(txtcompany_address.Value) + " " + CommonUtility.Get_StringValue(txtcompany_province.Value) + " " + CommonUtility.Get_StringValue(txtcompany_country.Value) + " FAX: " + CommonUtility.Get_StringValue(txtcompany_fax.Value) + " TAX ID:  " + CommonUtility.Get_StringValue(txtcompany_taxno.Value))

        ElseIf (Len(CommonUtility.Get_StringValue(txtcompany_phone.Value)) = 0) And (Len(CommonUtility.Get_StringValue(txtcompany_fax.Value)) = 0) And (Len(CommonUtility.Get_StringValue(txtcompany_taxno.Value)) <> 0) And (Mid(CommonUtility.Get_StringValue(txtdest_remark.Value), 1, 3)) = "C/O" Then

            txtCompany_Check_1.Text = (Mid(CommonUtility.Get_StringValue(txtob_address.Value), 1, 255) + " CARE OF " + CommonUtility.Get_StringValue(txtcompany_name.Value) + " " + CommonUtility.Get_StringValue(txtcompany_address.Value) + " " + CommonUtility.Get_StringValue(txtcompany_province.Value) + " " + CommonUtility.Get_StringValue(txtcompany_country.Value) + " TAX ID:  " + CommonUtility.Get_StringValue(txtcompany_taxno.Value))

        ElseIf (Len(CommonUtility.Get_StringValue(txtcompany_phone.Value)) = 0) And (Len(CommonUtility.Get_StringValue(txtcompany_fax.Value)) = 0) And (Len(CommonUtility.Get_StringValue(txtcompany_taxno.Value)) = 0) And (Mid(CommonUtility.Get_StringValue(txtdest_remark.Value), 1, 3)) = "C/O" Then

            txtCompany_Check_1.Text = (Mid(CommonUtility.Get_StringValue(txtob_address.Value), 1, 255) + " CARE OF " + CommonUtility.Get_StringValue(txtcompany_name.Value) + " " + CommonUtility.Get_StringValue(txtcompany_address.Value) + " " + CommonUtility.Get_StringValue(txtcompany_province.Value) + " " + CommonUtility.Get_StringValue(txtcompany_country.Value))

        ElseIf (Len(CommonUtility.Get_StringValue(txtcompany_phone.Value)) = 0) And (Len(CommonUtility.Get_StringValue(txtcompany_fax.Value)) = 0) And (Len(CommonUtility.Get_StringValue(txtcompany_taxno.Value)) <> 0) And (Mid(CommonUtility.Get_StringValue(txtdest_remark.Value), 1, 3)) = "O/B" Then

            txtCompany_Check_1.Text = (CommonUtility.Get_StringValue(txtcompany_name.Value) + "  " + CommonUtility.Get_StringValue(txtcompany_address.Value) + " " + CommonUtility.Get_StringValue(txtcompany_province.Value) + " " + CommonUtility.Get_StringValue(txtcompany_country.Value) + " TAX ID:  " + CommonUtility.Get_StringValue(txtcompany_taxno.Value) + " ON BEHALF OF " + Mid(CommonUtility.Get_StringValue(txtob_address.Value), 1, 255))

        ElseIf (Len(CommonUtility.Get_StringValue(txtcompany_phone.Value)) = 0) And (Len(CommonUtility.Get_StringValue(txtcompany_fax.Value)) = 0) And (Len(CommonUtility.Get_StringValue(txtcompany_taxno.Value)) = 0) And (Mid(CommonUtility.Get_StringValue(txtdest_remark.Value), 1, 3)) = "O/B" Then

            txtCompany_Check_1.Text = (CommonUtility.Get_StringValue(txtcompany_name.Value) + "  " + CommonUtility.Get_StringValue(txtcompany_address.Value) + " " + CommonUtility.Get_StringValue(txtcompany_province.Value) + " " + CommonUtility.Get_StringValue(txtcompany_country.Value) + " ON BEHALF OF " + Mid(CommonUtility.Get_StringValue(txtob_address.Value), 1, 255))

        ElseIf (Len(CommonUtility.Get_StringValue(txtcompany_phone.Value)) = 0) And (Len(CommonUtility.Get_StringValue(txtcompany_fax.Value)) = 0) And (Len(CommonUtility.Get_StringValue(txtcompany_taxno.Value)) <> 0) And (Mid(CommonUtility.Get_StringValue(txtdest_remark.Value), 2, 1)) <> "/" Then

            txtCompany_Check_1.Text = (CommonUtility.Get_StringValue(txtcompany_name.Value) + "  " + CommonUtility.Get_StringValue(txtcompany_address.Value) + " " + CommonUtility.Get_StringValue(txtcompany_province.Value) + " " + CommonUtility.Get_StringValue(txtcompany_country.Value) + " TAX ID:  " + CommonUtility.Get_StringValue(txtcompany_taxno.Value))

        ElseIf (Len(CommonUtility.Get_StringValue(txtcompany_phone.Value)) = 0) And (Len(CommonUtility.Get_StringValue(txtcompany_fax.Value)) = 0) And (Len(CommonUtility.Get_StringValue(txtcompany_taxno.Value)) = 0) And (Mid(CommonUtility.Get_StringValue(txtdest_remark.Value), 2, 1)) <> "/" Then

            txtCompany_Check_1.Text = (CommonUtility.Get_StringValue(txtcompany_name.Value) + "  " + CommonUtility.Get_StringValue(txtcompany_address.Value) + " " + CommonUtility.Get_StringValue(txtcompany_province.Value) + " " + CommonUtility.Get_StringValue(txtcompany_country.Value))

        ElseIf (Len(CommonUtility.Get_StringValue(txtcompany_phone.Value)) <> 0) And (Len(CommonUtility.Get_StringValue(txtcompany_fax.Value)) <> 0) And (Len(CommonUtility.Get_StringValue(txtcompany_taxno.Value)) <> 0) And (Mid(CommonUtility.Get_StringValue(txtdest_remark.Value), 1, 5) = "O/TCO") Then

            txtCompany_Check_1.Text = (Mid(CommonUtility.Get_StringValue(txtob_address.Value), 1, 255) + " " + CommonUtility.Get_StringValue(txtcompany_name.Value) + " " + CommonUtility.Get_StringValue(txtcompany_address.Value) + " " + CommonUtility.Get_StringValue(txtcompany_province.Value) + " " + CommonUtility.Get_StringValue(txtcompany_country.Value) + " TEL: " + CommonUtility.Get_StringValue(txtcompany_phone.Value) + " FAX: " + CommonUtility.Get_StringValue(txtcompany_fax.Value) + " TAX ID:  " + CommonUtility.Get_StringValue(txtcompany_taxno.Value))

        ElseIf (Len(CommonUtility.Get_StringValue(txtcompany_phone.Value)) <> 0) And (Len(CommonUtility.Get_StringValue(txtcompany_fax.Value)) <> 0) And (Len(CommonUtility.Get_StringValue(txtcompany_taxno.Value)) <> 0) And (Mid(CommonUtility.Get_StringValue(txtdest_remark.Value), 1, 5) = "O/TOB") Then

            txtCompany_Check_1.Text = (CommonUtility.Get_StringValue(txtcompany_name.Value) + " " + CommonUtility.Get_StringValue(txtcompany_address.Value) + " " + CommonUtility.Get_StringValue(txtcompany_province.Value) + " " + CommonUtility.Get_StringValue(txtcompany_country.Value) + " " + Mid(CommonUtility.Get_StringValue(txtob_address.Value), 1, 255) + " TEL: " + CommonUtility.Get_StringValue(txtcompany_phone.Value) + " FAX: " + CommonUtility.Get_StringValue(txtcompany_fax.Value) + " TAX ID:  " + CommonUtility.Get_StringValue(txtcompany_taxno.Value))

        ElseIf (Len(CommonUtility.Get_StringValue(txtcompany_phone.Value)) <> 0) And (Len(CommonUtility.Get_StringValue(txtcompany_fax.Value)) = 0) And (Len(CommonUtility.Get_StringValue(txtcompany_taxno.Value)) <> 0) And (Mid(CommonUtility.Get_StringValue(txtdest_remark.Value), 1, 5) = "O/TCO") Then

            txtCompany_Check_1.Text = (Mid(CommonUtility.Get_StringValue(txtob_address.Value), 1, 255) + " " + CommonUtility.Get_StringValue(txtcompany_name.Value) + " " + CommonUtility.Get_StringValue(txtcompany_address.Value) + " " + CommonUtility.Get_StringValue(txtcompany_province.Value) + " " + CommonUtility.Get_StringValue(txtcompany_country.Value) + " TEL: " + CommonUtility.Get_StringValue(txtcompany_phone.Value) + " TAX ID:  " + CommonUtility.Get_StringValue(txtcompany_taxno.Value))

        ElseIf (Len(CommonUtility.Get_StringValue(txtcompany_phone.Value)) <> 0) And (Len(CommonUtility.Get_StringValue(txtcompany_fax.Value)) = 0) And (Len(CommonUtility.Get_StringValue(txtcompany_taxno.Value)) = 0) And (Mid(CommonUtility.Get_StringValue(txtdest_remark.Value), 1, 5) = "O/TCO") Then

            txtCompany_Check_1.Text = (Mid(CommonUtility.Get_StringValue(txtob_address.Value), 1, 255) + " " + CommonUtility.Get_StringValue(txtcompany_name.Value) + " " + CommonUtility.Get_StringValue(txtcompany_address.Value) + " " + CommonUtility.Get_StringValue(txtcompany_province.Value) + " " + CommonUtility.Get_StringValue(txtcompany_country.Value) + " TEL: " + CommonUtility.Get_StringValue(txtcompany_phone.Value))

        ElseIf (Len(CommonUtility.Get_StringValue(txtcompany_phone.Value)) = 0) And (Len(CommonUtility.Get_StringValue(txtcompany_fax.Value)) <> 0) And (Len(CommonUtility.Get_StringValue(txtcompany_taxno.Value)) = 0) And (Mid(CommonUtility.Get_StringValue(txtdest_remark.Value), 1, 5) = "O/TCO") Then

            txtCompany_Check_1.Text = (Mid(CommonUtility.Get_StringValue(txtob_address.Value), 1, 255) + " " + CommonUtility.Get_StringValue(txtcompany_name.Value) + " " + CommonUtility.Get_StringValue(txtcompany_address.Value) + " " + CommonUtility.Get_StringValue(txtcompany_province.Value) + " " + CommonUtility.Get_StringValue(txtcompany_country.Value) + " FAX: " + CommonUtility.Get_StringValue(txtcompany_fax.Value))

        ElseIf (Len(CommonUtility.Get_StringValue(txtcompany_phone.Value)) = 0) And (Len(CommonUtility.Get_StringValue(txtcompany_fax.Value)) <> 0) And (Len(CommonUtility.Get_StringValue(txtcompany_taxno.Value)) <> 0) And (Mid(CommonUtility.Get_StringValue(txtdest_remark.Value), 1, 5) = "O/TCO") Then

            txtCompany_Check_1.Text = (Mid(CommonUtility.Get_StringValue(txtob_address.Value), 1, 255) + " " + CommonUtility.Get_StringValue(txtcompany_name.Value) + " " + CommonUtility.Get_StringValue(txtcompany_address.Value) + " " + CommonUtility.Get_StringValue(txtcompany_province.Value) + " " + CommonUtility.Get_StringValue(txtcompany_country.Value) + " FAX: " + CommonUtility.Get_StringValue(txtcompany_fax.Value) + " TAX ID:  " + CommonUtility.Get_StringValue(txtcompany_taxno.Value))

        ElseIf (Len(CommonUtility.Get_StringValue(txtcompany_phone.Value)) <> 0) And (Len(CommonUtility.Get_StringValue(txtcompany_fax.Value)) = 0) And (Len(CommonUtility.Get_StringValue(txtcompany_taxno.Value)) <> 0) And (Mid(CommonUtility.Get_StringValue(txtdest_remark.Value), 1, 5) = "O/TOB") Then

            txtCompany_Check_1.Text = (CommonUtility.Get_StringValue(txtcompany_name.Value) + " " + CommonUtility.Get_StringValue(txtcompany_address.Value) + " " + CommonUtility.Get_StringValue(txtcompany_province.Value) + " " + CommonUtility.Get_StringValue(txtcompany_country.Value) + " " + Mid(CommonUtility.Get_StringValue(txtob_address.Value), 1, 255) + " TEL: " + CommonUtility.Get_StringValue(txtcompany_phone.Value) + " TAX ID:  " + CommonUtility.Get_StringValue(txtcompany_taxno.Value))

        ElseIf (Len(CommonUtility.Get_StringValue(txtcompany_phone.Value)) <> 0) And (Len(CommonUtility.Get_StringValue(txtcompany_fax.Value)) = 0) And (Len(CommonUtility.Get_StringValue(txtcompany_taxno.Value)) = 0) And (Mid(CommonUtility.Get_StringValue(txtdest_remark.Value), 1, 5) = "O/TOB") Then

            txtCompany_Check_1.Text = (CommonUtility.Get_StringValue(txtcompany_name.Value) + " " + CommonUtility.Get_StringValue(txtcompany_address.Value) + " " + CommonUtility.Get_StringValue(txtcompany_province.Value) + " " + CommonUtility.Get_StringValue(txtcompany_country.Value) + " " + Mid(CommonUtility.Get_StringValue(txtob_address.Value), 1, 255) + " TEL: " + CommonUtility.Get_StringValue(txtcompany_phone.Value))

        ElseIf (Len(CommonUtility.Get_StringValue(txtcompany_phone.Value)) = 0) And (Len(CommonUtility.Get_StringValue(txtcompany_fax.Value)) <> 0) And (Len(CommonUtility.Get_StringValue(txtcompany_taxno.Value)) = 0) And (Mid(CommonUtility.Get_StringValue(txtdest_remark.Value), 1, 5) = "O/TOB") Then

            txtCompany_Check_1.Text = (CommonUtility.Get_StringValue(txtcompany_name.Value) + " " + CommonUtility.Get_StringValue(txtcompany_address.Value) + " " + CommonUtility.Get_StringValue(txtcompany_province.Value) + " " + CommonUtility.Get_StringValue(txtcompany_country.Value) + " " + Mid(CommonUtility.Get_StringValue(txtob_address.Value), 1, 255) + " FAX: " + CommonUtility.Get_StringValue(txtcompany_fax.Value))

        ElseIf (Len(CommonUtility.Get_StringValue(txtcompany_phone.Value)) = 0) And (Len(CommonUtility.Get_StringValue(txtcompany_fax.Value)) <> 0) And (Len(CommonUtility.Get_StringValue(txtcompany_taxno.Value)) <> 0) And (Mid(CommonUtility.Get_StringValue(txtdest_remark.Value), 1, 5) = "O/TOB") Then

            txtCompany_Check_1.Text = (CommonUtility.Get_StringValue(txtcompany_name.Value) + " " + CommonUtility.Get_StringValue(txtcompany_address.Value) + " " + CommonUtility.Get_StringValue(txtcompany_province.Value) + " " + CommonUtility.Get_StringValue(txtcompany_country.Value) + " " + Mid(CommonUtility.Get_StringValue(txtob_address.Value), 1, 255) + " FAX: " + CommonUtility.Get_StringValue(txtcompany_fax.Value) + " TAX ID:  " + CommonUtility.Get_StringValue(txtcompany_taxno.Value))

        ElseIf (Len(CommonUtility.Get_StringValue(txtcompany_phone.Value)) = 0) And (Len(CommonUtility.Get_StringValue(txtcompany_fax.Value)) = 0) And (Len(CommonUtility.Get_StringValue(txtcompany_taxno.Value)) <> 0) And (Mid(CommonUtility.Get_StringValue(txtdest_remark.Value), 1, 5) = "O/TCO") Then

            txtCompany_Check_1.Text = (Mid(CommonUtility.Get_StringValue(txtob_address.Value), 1, 255) + " " + CommonUtility.Get_StringValue(txtcompany_name.Value) + " " + CommonUtility.Get_StringValue(txtcompany_address.Value) + " " + CommonUtility.Get_StringValue(txtcompany_province.Value) + " " + CommonUtility.Get_StringValue(txtcompany_country.Value) + " TAX ID:  " + CommonUtility.Get_StringValue(txtcompany_taxno.Value))

        ElseIf (Len(CommonUtility.Get_StringValue(txtcompany_phone.Value)) = 0) And (Len(CommonUtility.Get_StringValue(txtcompany_fax.Value)) = 0) And (Len(CommonUtility.Get_StringValue(txtcompany_taxno.Value)) = 0) And (Mid(CommonUtility.Get_StringValue(txtdest_remark.Value), 1, 5) = "O/TCO") Then

            txtCompany_Check_1.Text = (Mid(CommonUtility.Get_StringValue(txtob_address.Value), 1, 255) + " " + CommonUtility.Get_StringValue(txtcompany_name.Value) + " " + CommonUtility.Get_StringValue(txtcompany_address.Value) + " " + CommonUtility.Get_StringValue(txtcompany_province.Value) + " " + CommonUtility.Get_StringValue(txtcompany_country.Value))

        ElseIf (Len(CommonUtility.Get_StringValue(txtcompany_phone.Value)) = 0) And (Len(CommonUtility.Get_StringValue(txtcompany_fax.Value)) = 0) And (Len(CommonUtility.Get_StringValue(txtcompany_taxno.Value)) <> 0) And (Mid(CommonUtility.Get_StringValue(txtdest_remark.Value), 1, 5) = "O/TOB") Then

            txtCompany_Check_1.Text = (CommonUtility.Get_StringValue(txtcompany_name.Value) + " " + CommonUtility.Get_StringValue(txtcompany_address.Value) + " " + CommonUtility.Get_StringValue(txtcompany_province.Value) + " " + CommonUtility.Get_StringValue(txtcompany_country.Value) + " " + Mid(CommonUtility.Get_StringValue(txtob_address.Value), 1, 255) + " TAX ID:  " + CommonUtility.Get_StringValue(txtcompany_taxno.Value))

        ElseIf (Len(CommonUtility.Get_StringValue(txtcompany_phone.Value)) = 0) And (Len(CommonUtility.Get_StringValue(txtcompany_fax.Value)) = 0) And (Len(CommonUtility.Get_StringValue(txtcompany_taxno.Value)) = 0) And (Mid(CommonUtility.Get_StringValue(txtdest_remark.Value), 1, 5) = "O/TOB") Then

            txtCompany_Check_1.Text = (CommonUtility.Get_StringValue(txtcompany_name.Value) + " " + CommonUtility.Get_StringValue(txtcompany_address.Value) + " " + CommonUtility.Get_StringValue(txtcompany_province.Value) + " " + CommonUtility.Get_StringValue(txtcompany_country.Value) + " " + Mid(CommonUtility.Get_StringValue(txtob_address.Value), 1, 255))
        End If
    End Sub

    Sub destination_check()
        If (Len(CommonUtility.Get_StringValue(txtdestination_phone.Value) <> 0) And (Len(CommonUtility.Get_StringValue(txtdestination_fax.Value)) <> 0) And (Len(CommonUtility.Get_StringValue(txtdestination_taxid.Value)) <> 0) And (Mid(CommonUtility.Get_StringValue(txtdest_remark1.Value), 1, 3)) = "C/O") Then

            txtdestination_Check2.Text = Mid(CommonUtility.Get_StringValue(txtob_dest_address.Value), 1, 400) + " CARE OF " + CommonUtility.Get_StringValue(txtdestination_company.Value) + " " + CommonUtility.Get_StringValue(txtdestination_address.Value) + " " + CommonUtility.Get_StringValue(txtdestination_province.Value) + " " + CommonUtility.Get_StringValue(txtdest_Receive_country.Value) + " TEL: " + CommonUtility.Get_StringValue(txtdestination_phone.Value) + " FAX: " + CommonUtility.Get_StringValue(txtdestination_fax.Value) + " TAX ID:  " + CommonUtility.Get_StringValue(txtdestination_taxid.Value)

        ElseIf (Len(CommonUtility.Get_StringValue(txtdestination_phone.Value) <> 0) And (Len(CommonUtility.Get_StringValue(txtdestination_fax.Value)) <> 0) And (Len(CommonUtility.Get_StringValue(txtdestination_taxid.Value)) <> 0) And (Mid(CommonUtility.Get_StringValue(txtdest_remark1.Value), 1, 3) = "O/B")) Then

            txtdestination_Check2.Text = (CommonUtility.Get_StringValue(txtdestination_company.Value) + "  " + CommonUtility.Get_StringValue(txtdestination_address.Value) + " " + CommonUtility.Get_StringValue(txtdestination_province.Value) + " " + CommonUtility.Get_StringValue(txtdest_Receive_country.Value) + " TEL: " + CommonUtility.Get_StringValue(txtdestination_phone.Value) + " FAX: " + CommonUtility.Get_StringValue(txtdestination_fax.Value) + " TAX ID:  " + CommonUtility.Get_StringValue(txtdestination_taxid.Value) + " ON BEHALF OF " + Mid(CommonUtility.Get_StringValue(txtob_dest_address.Value), 1, 400))

        ElseIf (Len(CommonUtility.Get_StringValue(txtdestination_phone.Value) <> 0) And (Len(CommonUtility.Get_StringValue(txtdestination_fax.Value)) <> 0) And (Len(CommonUtility.Get_StringValue(txtdestination_taxid.Value)) <> 0) And (Mid(CommonUtility.Get_StringValue(txtdest_remark1.Value), 2, 1) <> "/")) Then

            txtdestination_Check2.Text = (CommonUtility.Get_StringValue(txtdestination_company.Value) + "  " + CommonUtility.Get_StringValue(txtdestination_address.Value) + " " + CommonUtility.Get_StringValue(txtdestination_province.Value) + " " + CommonUtility.Get_StringValue(txtdest_Receive_country.Value) + " TEL: " + CommonUtility.Get_StringValue(txtdestination_phone.Value) + " FAX: " + CommonUtility.Get_StringValue(txtdestination_fax.Value) + " TAX ID:  " + CommonUtility.Get_StringValue(txtdestination_taxid.Value))

        ElseIf (Len(CommonUtility.Get_StringValue(txtdestination_phone.Value) <> 0) And (Len(CommonUtility.Get_StringValue(txtdestination_fax.Value)) = 0) And (Len(CommonUtility.Get_StringValue(txtdestination_taxid.Value)) <> 0) And (Mid(CommonUtility.Get_StringValue(txtdest_remark1.Value), 1, 3)) = "C/O") Then

            txtdestination_Check2.Text = Mid(CommonUtility.Get_StringValue(txtob_dest_address.Value), 1, 400) + " CARE OF " + CommonUtility.Get_StringValue(txtdestination_company.Value) + " " + CommonUtility.Get_StringValue(txtdestination_address.Value) + " " + CommonUtility.Get_StringValue(txtdestination_province.Value) + " " + CommonUtility.Get_StringValue(txtdest_Receive_country.Value) + " TEL: " + CommonUtility.Get_StringValue(txtdestination_phone.Value) + " TAX ID:  " + CommonUtility.Get_StringValue(txtdestination_taxid.Value)

        ElseIf (Len(CommonUtility.Get_StringValue(txtdestination_phone.Value) <> 0) And (Len(CommonUtility.Get_StringValue(txtdestination_fax.Value)) = 0) And (Len(CommonUtility.Get_StringValue(txtdestination_taxid.Value)) = 0) And (Mid(CommonUtility.Get_StringValue(txtdest_remark1.Value), 1, 3)) = "C/O") Then

            txtdestination_Check2.Text = Mid(CommonUtility.Get_StringValue(txtob_dest_address.Value), 1, 400) + " CARE OF " + CommonUtility.Get_StringValue(txtdestination_company.Value) + " " + CommonUtility.Get_StringValue(txtdestination_address.Value) + " " + CommonUtility.Get_StringValue(txtdestination_province.Value) + " " + CommonUtility.Get_StringValue(txtdest_Receive_country.Value) + " TEL: " + CommonUtility.Get_StringValue(txtdestination_phone.Value)

        ElseIf (Len(CommonUtility.Get_StringValue(txtdestination_phone.Value) = 0) And (Len(CommonUtility.Get_StringValue(txtdestination_fax.Value)) <> 0) And (Len(CommonUtility.Get_StringValue(txtdestination_taxid.Value)) = 0) And (Mid(CommonUtility.Get_StringValue(txtdest_remark1.Value), 1, 3)) = "C/O") Then

            txtdestination_Check2.Text = Mid(CommonUtility.Get_StringValue(txtob_dest_address.Value), 1, 400) + " CARE OF " + CommonUtility.Get_StringValue(txtdestination_company.Value) + " " + CommonUtility.Get_StringValue(txtdestination_address.Value) + " " + CommonUtility.Get_StringValue(txtdestination_province.Value) + " " + CommonUtility.Get_StringValue(txtdest_Receive_country.Value) + " FAX: " + CommonUtility.Get_StringValue(txtdestination_fax.Value)

        ElseIf (Len(CommonUtility.Get_StringValue(txtdestination_phone.Value) = 0) And (Len(CommonUtility.Get_StringValue(txtdestination_fax.Value)) <> 0) And (Len(CommonUtility.Get_StringValue(txtdestination_taxid.Value)) <> 0) And (Mid(CommonUtility.Get_StringValue(txtdest_remark1.Value), 1, 3)) = "C/O") Then

            txtdestination_Check2.Text = Mid(CommonUtility.Get_StringValue(txtob_dest_address.Value), 1, 400) + " CARE OF " + CommonUtility.Get_StringValue(txtdestination_company.Value) + " " + CommonUtility.Get_StringValue(txtdestination_address.Value) + " " + CommonUtility.Get_StringValue(txtdestination_province.Value) + " " + CommonUtility.Get_StringValue(txtdest_Receive_country.Value) + " FAX: " + CommonUtility.Get_StringValue(txtdestination_fax.Value) + " TAX ID:  " + CommonUtility.Get_StringValue(txtdestination_taxid.Value)

        ElseIf (Len(CommonUtility.Get_StringValue(txtdestination_phone.Value) <> 0) And (Len(CommonUtility.Get_StringValue(txtdestination_fax.Value)) = 0) And (Len(CommonUtility.Get_StringValue(txtdestination_taxid.Value)) <> 0) And (Mid(CommonUtility.Get_StringValue(txtdest_remark1.Value), 1, 3) = "O/B")) Then

            txtdestination_Check2.Text = (CommonUtility.Get_StringValue(txtdestination_company.Value) + "  " + CommonUtility.Get_StringValue(txtdestination_address.Value) + " " + CommonUtility.Get_StringValue(txtdestination_province.Value) + " " + CommonUtility.Get_StringValue(txtdest_Receive_country.Value) + " TEL: " + CommonUtility.Get_StringValue(txtdestination_phone.Value) + " TAX ID:  " + CommonUtility.Get_StringValue(txtdestination_taxid.Value) + " ON BEHALF OF " + Mid(CommonUtility.Get_StringValue(txtob_dest_address.Value), 1, 400))

        ElseIf (Len(CommonUtility.Get_StringValue(txtdestination_phone.Value) <> 0) And (Len(CommonUtility.Get_StringValue(txtdestination_fax.Value)) = 0) And (Len(CommonUtility.Get_StringValue(txtdestination_taxid.Value)) = 0) And (Mid(CommonUtility.Get_StringValue(txtdest_remark1.Value), 1, 3) = "O/B")) Then

            txtdestination_Check2.Text = (CommonUtility.Get_StringValue(txtdestination_company.Value) + "  " + CommonUtility.Get_StringValue(txtdestination_address.Value) + " " + CommonUtility.Get_StringValue(txtdestination_province.Value) + " " + CommonUtility.Get_StringValue(txtdest_Receive_country.Value) + " TEL: " + CommonUtility.Get_StringValue(txtdestination_phone.Value) + " ON BEHALF OF " + Mid(CommonUtility.Get_StringValue(txtob_dest_address.Value), 1, 400))

        ElseIf (Len(CommonUtility.Get_StringValue(txtdestination_phone.Value) = 0) And (Len(CommonUtility.Get_StringValue(txtdestination_fax.Value)) <> 0) And (Len(CommonUtility.Get_StringValue(txtdestination_taxid.Value)) = 0) And (Mid(CommonUtility.Get_StringValue(txtdest_remark1.Value), 1, 3) = "O/B")) Then

            txtdestination_Check2.Text = (CommonUtility.Get_StringValue(txtdestination_company.Value) + "  " + CommonUtility.Get_StringValue(txtdestination_address.Value) + " " + CommonUtility.Get_StringValue(txtdestination_province.Value) + " " + CommonUtility.Get_StringValue(txtdest_Receive_country.Value) + " FAX: " + CommonUtility.Get_StringValue(txtdestination_fax.Value) + " ON BEHALF OF " + Mid(CommonUtility.Get_StringValue(txtob_dest_address.Value), 1, 400))

        ElseIf (Len(CommonUtility.Get_StringValue(txtdestination_phone.Value) = 0) And (Len(CommonUtility.Get_StringValue(txtdestination_fax.Value)) <> 0) And (Len(CommonUtility.Get_StringValue(txtdestination_taxid.Value)) <> 0) And (Mid(CommonUtility.Get_StringValue(txtdest_remark1.Value), 1, 3) = "O/B")) Then

            txtdestination_Check2.Text = (CommonUtility.Get_StringValue(txtdestination_company.Value) + "  " + CommonUtility.Get_StringValue(txtdestination_address.Value) + " " + CommonUtility.Get_StringValue(txtdestination_province.Value) + " " + CommonUtility.Get_StringValue(txtdest_Receive_country.Value) + " FAX: " + CommonUtility.Get_StringValue(txtdestination_fax.Value) + " TAX ID:  " + CommonUtility.Get_StringValue(txtdestination_taxid.Value) + " ON BEHALF OF " + Mid(CommonUtility.Get_StringValue(txtob_dest_address.Value), 1, 400))

        ElseIf (Len(CommonUtility.Get_StringValue(txtdestination_phone.Value) <> 0) And (Len(CommonUtility.Get_StringValue(txtdestination_fax.Value)) = 0) And (Len(CommonUtility.Get_StringValue(txtdestination_taxid.Value)) <> 0) And (Mid(CommonUtility.Get_StringValue(txtdest_remark1.Value), 2, 1) <> "/")) Then

            txtdestination_Check2.Text = (CommonUtility.Get_StringValue(txtdestination_company.Value) + "  " + CommonUtility.Get_StringValue(txtdestination_address.Value) + " " + CommonUtility.Get_StringValue(txtdestination_province.Value) + " " + CommonUtility.Get_StringValue(txtdest_Receive_country.Value) + " TEL: " + CommonUtility.Get_StringValue(txtdestination_phone.Value) + " TAX ID:  " + CommonUtility.Get_StringValue(txtdestination_taxid.Value))

        ElseIf (Len(CommonUtility.Get_StringValue(txtdestination_phone.Value) <> 0) And (Len(CommonUtility.Get_StringValue(txtdestination_fax.Value)) = 0) And (Len(CommonUtility.Get_StringValue(txtdestination_taxid.Value)) = 0) And (Mid(CommonUtility.Get_StringValue(txtdest_remark1.Value), 2, 1) <> "/")) Then

            txtdestination_Check2.Text = (CommonUtility.Get_StringValue(txtdestination_company.Value) + "  " + CommonUtility.Get_StringValue(txtdestination_address.Value) + " " + CommonUtility.Get_StringValue(txtdestination_province.Value) + " " + CommonUtility.Get_StringValue(txtdest_Receive_country.Value) + " TEL: " + CommonUtility.Get_StringValue(txtdestination_phone.Value))

        ElseIf (Len(CommonUtility.Get_StringValue(txtdestination_phone.Value) = 0) And (Len(CommonUtility.Get_StringValue(txtdestination_fax.Value)) <> 0) And (Len(CommonUtility.Get_StringValue(txtdestination_taxid.Value)) = 0) And (Mid(CommonUtility.Get_StringValue(txtdest_remark1.Value), 2, 1) <> "/")) Then

            txtdestination_Check2.Text = (CommonUtility.Get_StringValue(txtdestination_company.Value) + "  " + CommonUtility.Get_StringValue(txtdestination_address.Value) + " " + CommonUtility.Get_StringValue(txtdestination_province.Value) + " " + CommonUtility.Get_StringValue(txtdest_Receive_country.Value) + " FAX: " + CommonUtility.Get_StringValue(txtdestination_fax.Value))

        ElseIf (Len(CommonUtility.Get_StringValue(txtdestination_phone.Value) = 0) And (Len(CommonUtility.Get_StringValue(txtdestination_fax.Value)) <> 0) And (Len(CommonUtility.Get_StringValue(txtdestination_taxid.Value)) <> 0) And (Mid(CommonUtility.Get_StringValue(txtdest_remark1.Value), 2, 1) <> "/")) Then

            txtdestination_Check2.Text = (CommonUtility.Get_StringValue(txtdestination_company.Value) + "  " + CommonUtility.Get_StringValue(txtdestination_address.Value) + " " + CommonUtility.Get_StringValue(txtdestination_province.Value) + " " + CommonUtility.Get_StringValue(txtdest_Receive_country.Value) + " FAX: " + CommonUtility.Get_StringValue(txtdestination_fax.Value) + " TAX ID:  " + CommonUtility.Get_StringValue(txtdestination_taxid.Value))

        ElseIf (Len(CommonUtility.Get_StringValue(txtdestination_phone.Value) <> 0) And (Len(CommonUtility.Get_StringValue(txtdestination_fax.Value)) <> 0) And (Len(CommonUtility.Get_StringValue(txtdestination_taxid.Value)) = 0) And (Mid(CommonUtility.Get_StringValue(txtdest_remark1.Value), 2, 1) <> "/")) Then

            txtdestination_Check2.Text = (CommonUtility.Get_StringValue(txtdestination_company.Value) + "  " + CommonUtility.Get_StringValue(txtdestination_address.Value) + " " + CommonUtility.Get_StringValue(txtdestination_province.Value) + " " + CommonUtility.Get_StringValue(txtdest_Receive_country.Value) + " TEL: " + CommonUtility.Get_StringValue(txtdestination_phone.Value) + " FAX: " + CommonUtility.Get_StringValue(txtdestination_fax.Value))

        ElseIf (Len(CommonUtility.Get_StringValue(txtdestination_phone.Value) = 0) And (Len(CommonUtility.Get_StringValue(txtdestination_fax.Value)) = 0) And (Len(CommonUtility.Get_StringValue(txtdestination_taxid.Value)) <> 0) And (Mid(CommonUtility.Get_StringValue(txtdest_remark1.Value), 1, 3)) = "C/O") Then

            txtdestination_Check2.Text = Mid(CommonUtility.Get_StringValue(txtob_dest_address.Value), 1, 400) + " CARE OF " + CommonUtility.Get_StringValue(txtdestination_company.Value) + " " + CommonUtility.Get_StringValue(txtdestination_address.Value) + " " + CommonUtility.Get_StringValue(txtdestination_province.Value) + " " + CommonUtility.Get_StringValue(txtdest_Receive_country.Value) + " TAX ID:  " + CommonUtility.Get_StringValue(txtdestination_taxid.Value)

        ElseIf (Len(CommonUtility.Get_StringValue(txtdestination_phone.Value) = 0) And (Len(CommonUtility.Get_StringValue(txtdestination_fax.Value)) = 0) And (Len(CommonUtility.Get_StringValue(txtdestination_taxid.Value)) = 0) And (Mid(CommonUtility.Get_StringValue(txtdest_remark1.Value), 1, 3)) = "C/O") Then

            txtdestination_Check2.Text = Mid(CommonUtility.Get_StringValue(txtob_dest_address.Value), 1, 400) + " CARE OF " + CommonUtility.Get_StringValue(txtdestination_company.Value) + " " + CommonUtility.Get_StringValue(txtdestination_address.Value) + " " + CommonUtility.Get_StringValue(txtdestination_province.Value) + " " + CommonUtility.Get_StringValue(txtdest_Receive_country.Value)

        ElseIf (Len(CommonUtility.Get_StringValue(txtdestination_phone.Value) = 0) And (Len(CommonUtility.Get_StringValue(txtdestination_fax.Value)) = 0) And (Len(CommonUtility.Get_StringValue(txtdestination_taxid.Value)) <> 0) And (Mid(CommonUtility.Get_StringValue(txtdest_remark1.Value), 1, 3) = "O/B")) Then

            txtdestination_Check2.Text = (CommonUtility.Get_StringValue(txtdestination_company.Value) + "  " + CommonUtility.Get_StringValue(txtdestination_address.Value) + " " + CommonUtility.Get_StringValue(txtdestination_province.Value) + " " + CommonUtility.Get_StringValue(txtdest_Receive_country.Value) + " TAX ID:  " + CommonUtility.Get_StringValue(txtdestination_taxid.Value) + " ON BEHALF OF " + Mid(CommonUtility.Get_StringValue(txtob_dest_address.Value), 1, 400))

        ElseIf (Len(CommonUtility.Get_StringValue(txtdestination_phone.Value) = 0) And (Len(CommonUtility.Get_StringValue(txtdestination_fax.Value)) = 0) And (Len(CommonUtility.Get_StringValue(txtdestination_taxid.Value)) = 0) And (Mid(CommonUtility.Get_StringValue(txtdest_remark1.Value), 1, 3) = "O/B")) Then

            txtdestination_Check2.Text = (CommonUtility.Get_StringValue(txtdestination_company.Value) + "  " + CommonUtility.Get_StringValue(txtdestination_address.Value) + " " + CommonUtility.Get_StringValue(txtdestination_province.Value) + " " + CommonUtility.Get_StringValue(txtdest_Receive_country.Value) + " ON BEHALF OF " + Mid(CommonUtility.Get_StringValue(txtob_dest_address.Value), 1, 400))

        ElseIf (Len(CommonUtility.Get_StringValue(txtdestination_phone.Value) = 0) And (Len(CommonUtility.Get_StringValue(txtdestination_fax.Value)) = 0) And (Len(CommonUtility.Get_StringValue(txtdestination_taxid.Value)) <> 0) And (Mid(CommonUtility.Get_StringValue(txtdest_remark1.Value), 2, 1) <> "/")) Then

            txtdestination_Check2.Text = (CommonUtility.Get_StringValue(txtdestination_company.Value) + "  " + CommonUtility.Get_StringValue(txtdestination_address.Value) + " " + CommonUtility.Get_StringValue(txtdestination_province.Value) + " " + CommonUtility.Get_StringValue(txtdest_Receive_country.Value) + " TAX ID:  " + CommonUtility.Get_StringValue(txtdestination_taxid.Value))

        ElseIf (Len(CommonUtility.Get_StringValue(txtdestination_phone.Value) = 0) And (Len(CommonUtility.Get_StringValue(txtdestination_fax.Value)) = 0) And (Len(CommonUtility.Get_StringValue(txtdestination_taxid.Value)) = 0) And (Mid(CommonUtility.Get_StringValue(txtdest_remark1.Value), 2, 1) <> "/")) Then

            txtdestination_Check2.Text = (CommonUtility.Get_StringValue(txtdestination_company.Value) + "  " + CommonUtility.Get_StringValue(txtdestination_address.Value) + " " + CommonUtility.Get_StringValue(txtdestination_province.Value) + " " + CommonUtility.Get_StringValue(txtdest_Receive_country.Value))

        ElseIf (Len(CommonUtility.Get_StringValue(txtdestination_phone.Value) <> 0) And (Len(CommonUtility.Get_StringValue(txtdestination_fax.Value)) <> 0) And (Len(CommonUtility.Get_StringValue(txtdestination_taxid.Value)) <> 0) And (Mid(CommonUtility.Get_StringValue(txtdest_remark1.Value), 1, 5)) = "O/TCO") Then

            txtdestination_Check2.Text = Mid(CommonUtility.Get_StringValue(txtob_dest_address.Value), 1, 400) + " " + CommonUtility.Get_StringValue(txtdestination_company.Value) + " " + CommonUtility.Get_StringValue(txtdestination_address.Value) + " " + CommonUtility.Get_StringValue(txtdestination_province.Value) + " " + CommonUtility.Get_StringValue(txtdest_Receive_country.Value) + " TEL: " + CommonUtility.Get_StringValue(txtdestination_phone.Value) + " FAX: " + CommonUtility.Get_StringValue(txtdestination_fax.Value) + " TAX ID:  " + CommonUtility.Get_StringValue(txtdestination_taxid.Value)

        ElseIf (Len(CommonUtility.Get_StringValue(txtdestination_phone.Value) <> 0) And (Len(CommonUtility.Get_StringValue(txtdestination_fax.Value)) <> 0) And (Len(CommonUtility.Get_StringValue(txtdestination_taxid.Value)) = 0) And (Mid(CommonUtility.Get_StringValue(txtdest_remark1.Value), 1, 5)) = "O/TCO") Then

            txtdestination_Check2.Text = Mid(CommonUtility.Get_StringValue(txtob_dest_address.Value), 1, 400) + " " + CommonUtility.Get_StringValue(txtdestination_company.Value) + " " + CommonUtility.Get_StringValue(txtdestination_address.Value) + " " + CommonUtility.Get_StringValue(txtdestination_province.Value) + " " + CommonUtility.Get_StringValue(txtdest_Receive_country.Value) + " TEL: " + CommonUtility.Get_StringValue(txtdestination_phone.Value) + " FAX: " + CommonUtility.Get_StringValue(txtdestination_fax.Value)

        ElseIf (Len(CommonUtility.Get_StringValue(txtdestination_phone.Value) <> 0) And (Len(CommonUtility.Get_StringValue(txtdestination_fax.Value)) <> 0) And (Len(CommonUtility.Get_StringValue(txtdestination_taxid.Value)) <> 0) And (Mid(CommonUtility.Get_StringValue(txtdest_remark1.Value), 1, 5)) = "O/TOB") Then

            txtdestination_Check2.Text = (CommonUtility.Get_StringValue(txtdestination_company.Value) + " " + CommonUtility.Get_StringValue(txtdestination_address.Value) + " " + CommonUtility.Get_StringValue(txtdestination_province.Value) + " " + CommonUtility.Get_StringValue(txtdest_Receive_country.Value) + "  " + Mid(CommonUtility.Get_StringValue(txtob_dest_address.Value), 1, 400) + " TEL: " + CommonUtility.Get_StringValue(txtdestination_phone.Value) + " FAX: " + CommonUtility.Get_StringValue(txtdestination_fax.Value) + " TAX ID:  " + CommonUtility.Get_StringValue(txtdestination_taxid.Value))

        ElseIf (Len(CommonUtility.Get_StringValue(txtdestination_phone.Value) <> 0) And (Len(CommonUtility.Get_StringValue(txtdestination_fax.Value)) <> 0) And (Len(CommonUtility.Get_StringValue(txtdestination_taxid.Value)) = 0) And (Mid(CommonUtility.Get_StringValue(txtdest_remark1.Value), 1, 5)) = "O/TOB") Then

            txtdestination_Check2.Text = (CommonUtility.Get_StringValue(txtdestination_company.Value) + " " + CommonUtility.Get_StringValue(txtdestination_address.Value) + " " + CommonUtility.Get_StringValue(txtdestination_province.Value) + " " + CommonUtility.Get_StringValue(txtdest_Receive_country.Value) + "  " + Mid(CommonUtility.Get_StringValue(txtob_dest_address.Value), 1, 400) + " TEL: " + CommonUtility.Get_StringValue(txtdestination_phone.Value) + " FAX: " + CommonUtility.Get_StringValue(txtdestination_fax.Value))

        ElseIf (Len(CommonUtility.Get_StringValue(txtdestination_phone.Value) <> 0) And (Len(CommonUtility.Get_StringValue(txtdestination_fax.Value)) = 0) And (Len(CommonUtility.Get_StringValue(txtdestination_taxid.Value)) <> 0) And (Mid(CommonUtility.Get_StringValue(txtdest_remark1.Value), 1, 5)) = "O/TCO") Then

            txtdestination_Check2.Text = Mid(CommonUtility.Get_StringValue(txtob_dest_address.Value), 1, 400) + " " + CommonUtility.Get_StringValue(txtdestination_company.Value) + " " + CommonUtility.Get_StringValue(txtdestination_address.Value) + " " + CommonUtility.Get_StringValue(txtdestination_province.Value) + " " + CommonUtility.Get_StringValue(txtdest_Receive_country.Value) + " TEL: " + CommonUtility.Get_StringValue(txtdestination_phone.Value) + " TAX ID:  " + CommonUtility.Get_StringValue(txtdestination_taxid.Value)

        ElseIf (Len(CommonUtility.Get_StringValue(txtdestination_phone.Value) <> 0) And (Len(CommonUtility.Get_StringValue(txtdestination_fax.Value)) = 0) And (Len(CommonUtility.Get_StringValue(txtdestination_taxid.Value)) = 0) And (Mid(CommonUtility.Get_StringValue(txtdest_remark1.Value), 1, 5)) = "O/TCO") Then

            txtdestination_Check2.Text = Mid(CommonUtility.Get_StringValue(txtob_dest_address.Value), 1, 400) + " " + CommonUtility.Get_StringValue(txtdestination_company.Value) + " " + CommonUtility.Get_StringValue(txtdestination_address.Value) + " " + CommonUtility.Get_StringValue(txtdestination_province.Value) + " " + CommonUtility.Get_StringValue(txtdest_Receive_country.Value) + " TEL: " + CommonUtility.Get_StringValue(txtdestination_phone.Value)

        ElseIf (Len(CommonUtility.Get_StringValue(txtdestination_phone.Value) = 0) And (Len(CommonUtility.Get_StringValue(txtdestination_fax.Value)) <> 0) And (Len(CommonUtility.Get_StringValue(txtdestination_taxid.Value)) = 0) And (Mid(CommonUtility.Get_StringValue(txtdest_remark1.Value), 1, 5)) = "O/TCO") Then

            txtdestination_Check2.Text = Mid(CommonUtility.Get_StringValue(txtob_dest_address.Value), 1, 400) + " " + CommonUtility.Get_StringValue(txtdestination_company.Value) + " " + CommonUtility.Get_StringValue(txtdestination_address.Value) + " " + CommonUtility.Get_StringValue(txtdestination_province.Value) + " " + CommonUtility.Get_StringValue(txtdest_Receive_country.Value) + " FAX: " + CommonUtility.Get_StringValue(txtdestination_fax.Value)

        ElseIf (Len(CommonUtility.Get_StringValue(txtdestination_phone.Value) = 0) And (Len(CommonUtility.Get_StringValue(txtdestination_fax.Value)) <> 0) And (Len(CommonUtility.Get_StringValue(txtdestination_taxid.Value)) <> 0) And (Mid(CommonUtility.Get_StringValue(txtdest_remark1.Value), 1, 5)) = "O/TCO") Then

            txtdestination_Check2.Text = Mid(CommonUtility.Get_StringValue(txtob_dest_address.Value), 1, 400) + " " + CommonUtility.Get_StringValue(txtdestination_company.Value) + " " + CommonUtility.Get_StringValue(txtdestination_address.Value) + " " + CommonUtility.Get_StringValue(txtdestination_province.Value) + " " + CommonUtility.Get_StringValue(txtdest_Receive_country.Value) + " FAX: " + CommonUtility.Get_StringValue(txtdestination_fax.Value) + " TAX ID:  " + CommonUtility.Get_StringValue(txtdestination_taxid.Value)

        ElseIf (Len(CommonUtility.Get_StringValue(txtdestination_phone.Value) <> 0) And (Len(CommonUtility.Get_StringValue(txtdestination_fax.Value)) = 0) And (Len(CommonUtility.Get_StringValue(txtdestination_taxid.Value)) <> 0) And (Mid(CommonUtility.Get_StringValue(txtdest_remark1.Value), 1, 5)) = "O/TOB") Then

            txtdestination_Check2.Text = (CommonUtility.Get_StringValue(txtdestination_company.Value) + " " + CommonUtility.Get_StringValue(txtdestination_address.Value) + " " + CommonUtility.Get_StringValue(txtdestination_province.Value) + " " + CommonUtility.Get_StringValue(txtdest_Receive_country.Value) + "  " + Mid(CommonUtility.Get_StringValue(txtob_dest_address.Value), 1, 400) + " TEL: " + CommonUtility.Get_StringValue(txtdestination_phone.Value) + " TAX ID:  " + CommonUtility.Get_StringValue(txtdestination_taxid.Value))

        ElseIf (Len(CommonUtility.Get_StringValue(txtdestination_phone.Value) <> 0) And (Len(CommonUtility.Get_StringValue(txtdestination_fax.Value)) = 0) And (Len(CommonUtility.Get_StringValue(txtdestination_taxid.Value)) = 0) And (Mid(CommonUtility.Get_StringValue(txtdest_remark1.Value), 1, 5)) = "O/TOB") Then

            txtdestination_Check2.Text = (CommonUtility.Get_StringValue(txtdestination_company.Value) + " " + CommonUtility.Get_StringValue(txtdestination_address.Value) + " " + CommonUtility.Get_StringValue(txtdestination_province.Value) + " " + CommonUtility.Get_StringValue(txtdest_Receive_country.Value) + "  " + Mid(CommonUtility.Get_StringValue(txtob_dest_address.Value), 1, 400) + " TEL: " + CommonUtility.Get_StringValue(txtdestination_phone.Value))

        ElseIf (Len(CommonUtility.Get_StringValue(txtdestination_phone.Value) = 0) And (Len(CommonUtility.Get_StringValue(txtdestination_fax.Value)) <> 0) And (Len(CommonUtility.Get_StringValue(txtdestination_taxid.Value)) = 0) And (Mid(CommonUtility.Get_StringValue(txtdest_remark1.Value), 1, 5)) = "O/TOB") Then

            txtdestination_Check2.Text = (CommonUtility.Get_StringValue(txtdestination_company.Value) + " " + CommonUtility.Get_StringValue(txtdestination_address.Value) + " " + CommonUtility.Get_StringValue(txtdestination_province.Value) + " " + CommonUtility.Get_StringValue(txtdest_Receive_country.Value) + "  " + Mid(CommonUtility.Get_StringValue(txtob_dest_address.Value), 1, 400) + " FAX: " + CommonUtility.Get_StringValue(txtdestination_fax.Value))

        ElseIf (Len(CommonUtility.Get_StringValue(txtdestination_phone.Value) = 0) And (Len(CommonUtility.Get_StringValue(txtdestination_fax.Value)) <> 0) And (Len(CommonUtility.Get_StringValue(txtdestination_taxid.Value)) <> 0) And (Mid(CommonUtility.Get_StringValue(txtdest_remark1.Value), 1, 5)) = "O/TOB") Then

            txtdestination_Check2.Text = (CommonUtility.Get_StringValue(txtdestination_company.Value) + " " + CommonUtility.Get_StringValue(txtdestination_address.Value) + " " + CommonUtility.Get_StringValue(txtdestination_province.Value) + " " + CommonUtility.Get_StringValue(txtdest_Receive_country.Value) + "  " + Mid(CommonUtility.Get_StringValue(txtob_dest_address.Value), 1, 400) + " FAX: " + CommonUtility.Get_StringValue(txtdestination_fax.Value) + " TAX ID:  " + CommonUtility.Get_StringValue(txtdestination_taxid.Value))

        ElseIf (Len(CommonUtility.Get_StringValue(txtdestination_phone.Value) = 0) And (Len(CommonUtility.Get_StringValue(txtdestination_fax.Value)) = 0) And (Len(CommonUtility.Get_StringValue(txtdestination_taxid.Value)) <> 0) And (Mid(CommonUtility.Get_StringValue(txtdest_remark1.Value), 1, 5)) = "O/TCO") Then

            txtdestination_Check2.Text = Mid(CommonUtility.Get_StringValue(txtob_dest_address.Value), 1, 400) + " " + CommonUtility.Get_StringValue(txtdestination_company.Value) + " " + CommonUtility.Get_StringValue(txtdestination_address.Value) + " " + CommonUtility.Get_StringValue(txtdestination_province.Value) + " " + CommonUtility.Get_StringValue(txtdest_Receive_country.Value) + " TAX ID:  " + CommonUtility.Get_StringValue(txtdestination_taxid.Value)

        ElseIf (Len(CommonUtility.Get_StringValue(txtdestination_phone.Value) = 0) And (Len(CommonUtility.Get_StringValue(txtdestination_fax.Value)) = 0) And (Len(CommonUtility.Get_StringValue(txtdestination_taxid.Value)) = 0) And (Mid(CommonUtility.Get_StringValue(txtdest_remark1.Value), 1, 5)) = "O/TCO") Then

            txtdestination_Check2.Text = Mid(CommonUtility.Get_StringValue(txtob_dest_address.Value), 1, 400) + " " + CommonUtility.Get_StringValue(txtdestination_company.Value) + " " + CommonUtility.Get_StringValue(txtdestination_address.Value) + " " + CommonUtility.Get_StringValue(txtdestination_province.Value) + " " + CommonUtility.Get_StringValue(txtdest_Receive_country.Value)

        ElseIf (Len(CommonUtility.Get_StringValue(txtdestination_phone.Value) = 0) And (Len(CommonUtility.Get_StringValue(txtdestination_fax.Value)) = 0) And (Len(CommonUtility.Get_StringValue(txtdestination_taxid.Value)) <> 0) And (Mid(CommonUtility.Get_StringValue(txtdest_remark1.Value), 1, 5)) = "O/TOB") Then

            txtdestination_Check2.Text = (CommonUtility.Get_StringValue(txtdestination_company.Value) + " " + CommonUtility.Get_StringValue(txtdestination_address.Value) + " " + CommonUtility.Get_StringValue(txtdestination_province.Value) + " " + CommonUtility.Get_StringValue(txtdest_Receive_country.Value) + "  " + Mid(CommonUtility.Get_StringValue(txtob_dest_address.Value), 1, 400) + " TAX ID:  " + CommonUtility.Get_StringValue(txtdestination_taxid.Value))

        ElseIf (Len(CommonUtility.Get_StringValue(txtdestination_phone.Value) = 0) And (Len(CommonUtility.Get_StringValue(txtdestination_fax.Value)) = 0) And (Len(CommonUtility.Get_StringValue(txtdestination_taxid.Value)) = 0) And (Mid(CommonUtility.Get_StringValue(txtdest_remark1.Value), 1, 5)) = "O/TOB") Then

            txtdestination_Check2.Text = (CommonUtility.Get_StringValue(txtdestination_company.Value) + " " + CommonUtility.Get_StringValue(txtdestination_address.Value) + " " + CommonUtility.Get_StringValue(txtdestination_province.Value) + " " + CommonUtility.Get_StringValue(txtdest_Receive_country.Value) + "  " + Mid(CommonUtility.Get_StringValue(txtob_dest_address.Value), 1, 400))
        End If
    End Sub

    Function confor_mat(ByVal d_date As Date) As String
        Dim str_for As String = Format(d_date, "d/MM/yyyy")
        Return str_for
    End Function

    Sub CallInvoiceCheck() 'Check Invoince
        Select Case txtshow_check.Text
            Case "100" 'invoice ต่างประเทศ
                txtTolInvoice.Text = txtNumInvoice.Text
            Case Else 'ไม่ใช่ invoice ต่างประเทศ
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
    'ใช้อันนี้ เพื่อใช้รวมกับระบบเก่า
    Function CallInvoice_() As String 'Check Invoince
        Dim str_invoice As String
        Select Case txtshow_check.Text
            Case "100" 'invoice ต่างประเทศ
                Select Case CommonUtility.Get_StringValue(Check_NULLALL(txtNumInvoice.Text))
                    Case ""
                        str_invoice = Callinvoice_board(0)
                    Case Is <> ""
                        str_invoice = txtNumInvoice.Text
                End Select
                'str_invoice = txtNumInvoice.Text
            Case Else 'ไม่ใช่ invoice ต่างประเทศ
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

    'tariff ไม่เหมือน form az
    Function tariff_All(ByVal _tariff As String) As String
        Dim str_tariff As String
        'str_tariff = Mid(_tariff, 1, 4) & "." & Mid(_tariff, 5, 2)

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
    '        Case 4 'ไม่มีเลย
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
    '        Case 4 'ไม่มีเลย
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
    '                                                            "       _____________________________"
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
    '        Case 4 'ไม่มีเลย
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
    '                                                            "       _____________________________"
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
    '        Case 4 'ไม่มีเลย
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
    '                                                            "       _____________________________"
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
    '                                                                                    txtthird_country.Text & vbCrLf & _
    '                                                                                    txtback_country.Text & vbCrLf & _
    '                                                                                    txtplace_exibition.Text & vbCrLf & _
    '                                                                                    "       _____________________________"
    '                Else
    '                    txtT_product.Text = All_str & _
    '                                        "        " & _
    '                                                                                    txtthird_country.Text & vbCrLf & _
    '                                                                                    txtback_country.Text & vbCrLf & _
    '                                                                                    txtplace_exibition.Text & vbCrLf & _
    '                                                                                    "       _____________________________"
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
    '                                        "        " & _
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
    '        Case 4 'ไม่มีเลย
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
    '                                                                                   "       _____________________________"
    '                Else
    '                    txtT_product.Text = All_str & _
    '                                       "       _____________________________"
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
    '                                                                                   "       _____________________________"
    '                Else
    '                    txtT_product.Text = All_str & _
    '                                       "       _____________________________"
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
    '        Case 4 'ไม่มีเลย
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
    '                txtT_product.Text = All_str & _
    '                vbCrLf & "       _____________________________"
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
    Sub All_CheckThird_5(ByVal count_data As Integer)
        Title_StrDetail()
        Select Case Check_Third(CommonUtility.Get_StringValue(txtthird_country.Value), _
                                CommonUtility.Get_StringValue(txtback_country.Value), _
                                CommonUtility.Get_StringValue(txtplace_exibition.Value))
            Case 1
                'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity5.Text, txtq_unit_code5.Text) & vbCrLf & _
                                                                String_Total_USDOnly_New(txtDisplayUDS7.Text, txtCurrency_Code.Text, TFOB, CommonUtility.Get_StringValue(txtshow_check.Text), TUSD, TUSDOther) & "        " & _
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
                    txtT_product.Text = All_str
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
                                                                                BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " & _
                                                                                BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf & "        " & _
                                                                                BahtOnly(txtquantity5.Text, txtq_unit_code5.Text) & vbCrLf & _
                                                                                String_Total_USDOnly_New(txtDisplayUDS7.Text, txtCurrency_Code.Text, TFOB, CommonUtility.Get_StringValue(txtshow_check.Text), TUSD, TUSDOther) & "        " & _
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
                    txtT_product.Text = All_str
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                    BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
                                                                    BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " & _
                                                                    BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf & "        " & _
                                                                    BahtOnly(txtquantity5.Text, txtq_unit_code5.Text) & vbCrLf & String_Total_USDOnly_New(txtDisplayUDS7.Text, txtCurrency_Code.Text, TFOB, CommonUtility.Get_StringValue(txtshow_check.Text), TUSD, TUSDOther) & _
                                                                    "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 4 'ไม่มีเลย
                'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                    BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
                                                                    BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " & _
                                                                    BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf & "        " & _
                                                                    BahtOnly(txtquantity5.Text, txtq_unit_code5.Text) & vbCrLf & String_Total_USDOnly_New(txtDisplayUDS7.Text, txtCurrency_Code.Text, TFOB, CommonUtility.Get_StringValue(txtshow_check.Text), TUSD, TUSDOther) & _
                                                                    "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 5 'T2_back_country
                'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                    BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
                                                                    BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " & _
                                                                    BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf & "        " & _
                                                                    BahtOnly(txtquantity5.Text, txtq_unit_code5.Text) & vbCrLf & String_Total_USDOnly_New(txtDisplayUDS7.Text, txtCurrency_Code.Text, TFOB, CommonUtility.Get_StringValue(txtshow_check.Text), TUSD, TUSDOther) & vbNewLine & _
                                                                    _TempB2B & vbNewLine & _
                                                                    "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 6 'T3_place_exibition
                'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                    BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
                                                                    BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " & _
                                                                    BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf & "        " & _
                                                                    BahtOnly(txtquantity5.Text, txtq_unit_code5.Text) & vbCrLf & String_Total_USDOnly_New(txtDisplayUDS7.Text, txtCurrency_Code.Text, TFOB, CommonUtility.Get_StringValue(txtshow_check.Text), TUSD, TUSDOther) & _
                                                                    "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 7 'T1_third_country,T2_back_country
                'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                    BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
                                                                    BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " & _
                                                                    BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf & "        " & _
                                                                    BahtOnly(txtquantity5.Text, txtq_unit_code5.Text) & vbCrLf & _
                                                                    String_Total_USDOnly_New(txtDisplayUDS7.Text, txtCurrency_Code.Text, TFOB, CommonUtility.Get_StringValue(txtshow_check.Text), TUSD, TUSDOther) & "        " & _
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
                    txtT_product.Text = All_str
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                    BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
                                                                    BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " & _
                                                                    BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf & "        " & _
                                                                    BahtOnly(txtquantity5.Text, txtq_unit_code5.Text) & vbCrLf & _
                                                                    String_Total_USDOnly_New(txtDisplayUDS7.Text, txtCurrency_Code.Text, TFOB, CommonUtility.Get_StringValue(txtshow_check.Text), TUSD, TUSDOther) & "        " & _
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
                    txtT_product.Text = All_str
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity5.Text, txtq_unit_code5.Text) & vbCrLf & _
                                                                String_Total_USDOnly_New(txtDisplayUDS7.Text, txtCurrency_Code.Text, TFOB, CommonUtility.Get_StringValue(txtshow_check.Text), TUSD, TUSDOther) & "        " & _
                                                                txtthird_country.Text & vbCrLf & _
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
                    txtT_product.Text = All_str
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                            BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
                                            BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " & _
                                            BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf & _
                                            String_Total_USDOnly_New(txtDisplayUDS7.Text, txtCurrency_Code.Text, TFOB, CommonUtility.Get_StringValue(txtshow_check.Text), TUSD, TUSDOther) & "        " & _
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
                    txtT_product.Text = All_str
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                            BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
                                                            BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " & _
                                                            BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf & _
                                                            String_Total_USDOnly_New(txtDisplayUDS7.Text, txtCurrency_Code.Text, TFOB, CommonUtility.Get_StringValue(txtshow_check.Text), TUSD, TUSDOther) & "        " & _
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
                    txtT_product.Text = All_str
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf & String_Total_USDOnly_New(txtDisplayUDS7.Text, txtCurrency_Code.Text, TFOB, CommonUtility.Get_StringValue(txtshow_check.Text), TUSD, TUSDOther) & _
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 4 'ไม่มีเลย
                'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf & String_Total_USDOnly_New(txtDisplayUDS7.Text, txtCurrency_Code.Text, TFOB, CommonUtility.Get_StringValue(txtshow_check.Text), TUSD, TUSDOther) & _
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 5 'T2_back_country
                'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf & String_Total_USDOnly_New(txtDisplayUDS7.Text, txtCurrency_Code.Text, TFOB, CommonUtility.Get_StringValue(txtshow_check.Text), TUSD, TUSDOther) & vbNewLine & _
                                                                    _TempB2B & vbNewLine & _
                                                                    "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 6 'T3_place_exibition
                'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf & String_Total_USDOnly_New(txtDisplayUDS7.Text, txtCurrency_Code.Text, TFOB, CommonUtility.Get_StringValue(txtshow_check.Text), TUSD, TUSDOther) & _
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 7 'T1_third_country,T2_back_country
                'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                    BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
                                                                    BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " & _
                                                                    BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf & _
                                                                    String_Total_USDOnly_New(txtDisplayUDS7.Text, txtCurrency_Code.Text, TFOB, CommonUtility.Get_StringValue(txtshow_check.Text), TUSD, TUSDOther) & "        " & _
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
                    txtT_product.Text = All_str
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                    BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
                                                                    BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " & _
                                                                    BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf & _
                                                                    String_Total_USDOnly_New(txtDisplayUDS7.Text, txtCurrency_Code.Text, TFOB, CommonUtility.Get_StringValue(txtshow_check.Text), TUSD, TUSDOther) & "        " & _
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
                    txtT_product.Text = All_str
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                    BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
                                                                    BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " & _
                                                                    BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf & _
                                                                    String_Total_USDOnly_New(txtDisplayUDS7.Text, txtCurrency_Code.Text, TFOB, CommonUtility.Get_StringValue(txtshow_check.Text), TUSD, TUSDOther) & "        " & _
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
                    txtT_product.Text = All_str
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & _
                                                                String_Total_USDOnly_New(txtDisplayUDS7.Text, txtCurrency_Code.Text, TFOB, CommonUtility.Get_StringValue(txtshow_check.Text), TUSD, TUSDOther) & "        " & _
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
                    txtT_product.Text = All_str
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
                                                                                BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & _
                                                                                String_Total_USDOnly_New(txtDisplayUDS7.Text, txtCurrency_Code.Text, TFOB, CommonUtility.Get_StringValue(txtshow_check.Text), TUSD, TUSDOther) & "        " & _
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
                    txtT_product.Text = All_str
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & String_Total_USDOnly_New(txtDisplayUDS7.Text, txtCurrency_Code.Text, TFOB, CommonUtility.Get_StringValue(txtshow_check.Text), TUSD, TUSDOther) & _
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 4 'ไม่มีเลย
                'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & String_Total_USDOnly_New(txtDisplayUDS7.Text, txtCurrency_Code.Text, TFOB, CommonUtility.Get_StringValue(txtshow_check.Text), TUSD, TUSDOther) & _
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 5 'T2_back_country
                'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & String_Total_USDOnly_New(txtDisplayUDS7.Text, txtCurrency_Code.Text, TFOB, CommonUtility.Get_StringValue(txtshow_check.Text), TUSD, TUSDOther) & vbNewLine & _
                                                                    _TempB2B & vbNewLine & _
                                                                    "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 6 'T3_place_exibition
                'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & String_Total_USDOnly_New(txtDisplayUDS7.Text, txtCurrency_Code.Text, TFOB, CommonUtility.Get_StringValue(txtshow_check.Text), TUSD, TUSDOther) & _
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 7 'T1_third_country,T2_back_country
                'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & _
                                                                String_Total_USDOnly_New(txtDisplayUDS7.Text, txtCurrency_Code.Text, TFOB, CommonUtility.Get_StringValue(txtshow_check.Text), TUSD, TUSDOther) & "        " & _
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
                    txtT_product.Text = All_str
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & _
                                                                String_Total_USDOnly_New(txtDisplayUDS7.Text, txtCurrency_Code.Text, TFOB, CommonUtility.Get_StringValue(txtshow_check.Text), TUSD, TUSDOther) & "        " & _
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
                    txtT_product.Text = All_str
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                    BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
                                                                    BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & _
                                                                    String_Total_USDOnly_New(txtDisplayUDS7.Text, txtCurrency_Code.Text, TFOB, CommonUtility.Get_StringValue(txtshow_check.Text), TUSD, TUSDOther) & "        " & _
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
                    txtT_product.Text = All_str
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & _
                                                                String_Total_USDOnly_New(txtDisplayUDS7.Text, txtCurrency_Code.Text, TFOB, CommonUtility.Get_StringValue(txtshow_check.Text), TUSD, TUSDOther) & "        " & _
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
                    txtT_product.Text = All_str
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & _
                                                                                String_Total_USDOnly_New(txtDisplayUDS7.Text, txtCurrency_Code.Text, TFOB, CommonUtility.Get_StringValue(txtshow_check.Text), TUSD, TUSDOther) & "        " & _
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
                    txtT_product.Text = All_str
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & String_Total_USDOnly_New(txtDisplayUDS7.Text, txtCurrency_Code.Text, TFOB, CommonUtility.Get_StringValue(txtshow_check.Text), TUSD, TUSDOther) & _
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 4 'ไม่มีเลย
                'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & String_Total_USDOnly_New(txtDisplayUDS7.Text, txtCurrency_Code.Text, TFOB, CommonUtility.Get_StringValue(txtshow_check.Text), TUSD, TUSDOther) & _
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 5 'T2_back_country
                'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & String_Total_USDOnly_New(txtDisplayUDS7.Text, txtCurrency_Code.Text, TFOB, CommonUtility.Get_StringValue(txtshow_check.Text), TUSD, TUSDOther) & vbNewLine & _
                                                                    _TempB2B & vbNewLine & _
                                                                    "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 6 'T3_place_exibition
                'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & String_Total_USDOnly_New(txtDisplayUDS7.Text, txtCurrency_Code.Text, TFOB, CommonUtility.Get_StringValue(txtshow_check.Text), TUSD, TUSDOther) & _
                                                                "       _____________________________"
                Else
                    txtT_product.Text = All_str
                End If
            Case 7 'T1_third_country,T2_back_country
                'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    txtT_product.Text = All_str
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & _
                                                                String_Total_USDOnly_New(txtDisplayUDS7.Text, txtCurrency_Code.Text, TFOB, CommonUtility.Get_StringValue(txtshow_check.Text), TUSD, TUSDOther) & "        " & _
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
                    txtT_product.Text = All_str
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & _
                                                                String_Total_USDOnly_New(txtDisplayUDS7.Text, txtCurrency_Code.Text, TFOB, CommonUtility.Get_StringValue(txtshow_check.Text), TUSD, TUSDOther) & "        " & _
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
                    txtT_product.Text = All_str
                    GroupFooter1.Visible = True
                    txtTotalAll.Visible = True
                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
                                                                    BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & _
                                                                    String_Total_USDOnly_New(txtDisplayUDS7.Text, txtCurrency_Code.Text, TFOB, CommonUtility.Get_StringValue(txtshow_check.Text), TUSD, TUSDOther) & "        " & _
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
                        txtT_product.Text = All_str
                        GroupFooter1.Visible = True
                        txtTotalAll.Visible = True
                        txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & _
                                            String_Total_USDOnly_New(txtDisplayUDS7.Text, txtCurrency_Code.Text, TFOB, CommonUtility.Get_StringValue(txtshow_check.Text), TUSD, TUSDOther) & "        " & _
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
                        txtT_product.Text = All_str
                        GroupFooter1.Visible = True
                        txtTotalAll.Visible = True
                        txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & _
                                            String_Total_USDOnly_New(txtDisplayUDS7.Text, txtCurrency_Code.Text, TFOB, CommonUtility.Get_StringValue(txtshow_check.Text), TUSD, TUSDOther) & "        " & _
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
                        txtT_product.Text = All_str
                        GroupFooter1.Visible = True
                        txtTotalAll.Visible = True
                        txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & String_Total_USDOnly_New(txtDisplayUDS7.Text, txtCurrency_Code.Text, TFOB, CommonUtility.Get_StringValue(txtshow_check.Text), TUSD, TUSDOther) & _
                                                                                        "       _____________________________"
                    Else
                        txtT_product.Text = All_str & _
                                            "       _____________________________"
                    End If

                Else
                    txtT_product.Text = All_str
                End If
            Case 4 'ไม่มีเลย
                'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    If (Not txtquantity1.Text = String.Empty = True) And CommonUtility.Get_StringValue(txtquantity1.Text) <> "" And CommonUtility.Get_Decimal(IIf(Not (txtquantity1.Text = String.Empty) = False, 0, txtquantity1.Value)) <> 0 Then
                        txtT_product.Text = All_str
                        GroupFooter1.Visible = True
                        txtTotalAll.Visible = True
                        txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & String_Total_USDOnly_New(txtDisplayUDS7.Text, txtCurrency_Code.Text, TFOB, CommonUtility.Get_StringValue(txtshow_check.Text), TUSD, TUSDOther) & _
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
                        txtT_product.Text = All_str
                        GroupFooter1.Visible = True
                        txtTotalAll.Visible = True
                        txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & String_Total_USDOnly_New(txtDisplayUDS7.Text, txtCurrency_Code.Text, TFOB, CommonUtility.Get_StringValue(txtshow_check.Text), TUSD, TUSDOther) & vbNewLine & _
                                                                    _TempB2B & vbNewLine & _
                                                                    "       _____________________________"
                    Else
                        txtT_product.Text = All_str & _
                                                                    _TempB2B & vbNewLine & _
                                                                    "       _____________________________"
                    End If

                Else
                    txtT_product.Text = All_str
                End If
            Case 6 'T3_place_exibition
                'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
                '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

                If C_TotalRowDe.Text = count_data Then
                    If (Not txtquantity1.Text = String.Empty = True) And CommonUtility.Get_StringValue(txtquantity1.Text) <> "" And CommonUtility.Get_Decimal(IIf(Not (txtquantity1.Text = String.Empty) = False, 0, txtquantity1.Value)) <> 0 Then
                        txtT_product.Text = All_str
                        GroupFooter1.Visible = True
                        txtTotalAll.Visible = True
                        txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & String_Total_USDOnly_New(txtDisplayUDS7.Text, txtCurrency_Code.Text, TFOB, CommonUtility.Get_StringValue(txtshow_check.Text), TUSD, TUSDOther) & _
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
                        txtT_product.Text = All_str
                        GroupFooter1.Visible = True
                        txtTotalAll.Visible = True
                        txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & _
                                            String_Total_USDOnly_New(txtDisplayUDS7.Text, txtCurrency_Code.Text, TFOB, CommonUtility.Get_StringValue(txtshow_check.Text), TUSD, TUSDOther) & "        " & _
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
                        txtT_product.Text = All_str
                        GroupFooter1.Visible = True
                        txtTotalAll.Visible = True
                        txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & _
                                            String_Total_USDOnly_New(txtDisplayUDS7.Text, txtCurrency_Code.Text, TFOB, CommonUtility.Get_StringValue(txtshow_check.Text), TUSD, TUSDOther) & "        " & _
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
                        txtT_product.Text = All_str
                        GroupFooter1.Visible = True
                        txtTotalAll.Visible = True
                        txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & _
                                            String_Total_USDOnly_New(txtDisplayUDS7.Text, txtCurrency_Code.Text, TFOB, CommonUtility.Get_StringValue(txtshow_check.Text), TUSD, TUSDOther) & "        " & _
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
            Case 4 'ไม่มีเลย
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
                    txtT_product.Text = All_str & vbNewLine & _
                                                                    _TempB2B & vbNewLine & _
                                                                    "       _____________________________"
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

#Region "back"
    'Sub All_CheckThird_5(ByVal count_data As Integer)
    '    Title_StrDetail()
    '    Select Case Check_Third(CommonUtility.Get_StringValue(txtthird_country.Value), _
    '                            CommonUtility.Get_StringValue(txtback_country.Value), _
    '                            CommonUtility.Get_StringValue(txtplace_exibition.Value))
    '        Case 1
    '            'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '            '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

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
    '            'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '            '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

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
    '            'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '            '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

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
    '        Case 4 'ไม่มีเลย
    '            'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '            '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

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
    '            'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '            '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

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
    '        Case 6 'T3_place_exibition
    '            'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '            '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

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
    '            'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '            '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

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
    '            'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '            '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

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
    '            'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '            '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

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
    '    Title_StrDetail()

    '    Select Case Check_Third(CommonUtility.Get_StringValue(txtthird_country.Value), _
    '                            CommonUtility.Get_StringValue(txtback_country.Value), _
    '                            CommonUtility.Get_StringValue(txtplace_exibition.Value))
    '        Case 1
    '            'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '            '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

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
    '            'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '            '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

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
    '            'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '            '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

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
    '        Case 4 'ไม่มีเลย
    '            'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '            '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

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
    '            'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '            '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

    '            If C_TotalRowDe.Text = count_data Then
    '                txtT_product.Text = All_str
    '                GroupFooter1.Visible = True
    '                txtTotalAll.Visible = True
    '                txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
    '                                                            BahtOnly(txtquantity2.Text, txtq_unit_code2.Text) & vbCrLf & "        " & _
    '                                                            BahtOnly(txtquantity3.Text, txtq_unit_code3.Text) & vbCrLf & "        " & _
    '                                                            BahtOnly(txtquantity4.Text, txtq_unit_code4.Text) & vbCrLf & "        " & _
    '                                                            "       _____________________________"
    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 6 'T3_place_exibition
    '            'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '            '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

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
    '            'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '            '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

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
    '            'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '            '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

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
    '            'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '            '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

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
    '    Title_StrDetail()
    '    Select Case Check_Third(CommonUtility.Get_StringValue(txtthird_country.Value), _
    '                            CommonUtility.Get_StringValue(txtback_country.Value), _
    '                            CommonUtility.Get_StringValue(txtplace_exibition.Value))
    '        Case 1
    '            'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '            '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

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
    '            'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '            '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

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
    '            'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '            '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

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
    '        Case 4 'ไม่มีเลย
    '            'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '            '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

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
    '            'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '            '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

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
    '        Case 6 'T3_place_exibition
    '            'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '            '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

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
    '            'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '            '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

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
    '            'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '            '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

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
    '            'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '            '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

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
    '    Title_StrDetail()
    '    Select Case Check_Third(CommonUtility.Get_StringValue(txtthird_country.Value), _
    '                            CommonUtility.Get_StringValue(txtback_country.Value), _
    '                            CommonUtility.Get_StringValue(txtplace_exibition.Value))
    '        Case 1
    '            'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '            '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

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
    '            'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '            '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

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
    '            'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '            '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

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
    '        Case 4 'ไม่มีเลย
    '            'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '            '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

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
    '            'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '            '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

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
    '        Case 6 'T3_place_exibition
    '            'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '            '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

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
    '            'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '            '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

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
    '            'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '            '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

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
    '            'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '            '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

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
    '    Title_StrDetail()
    '    Select Case Check_Third(CommonUtility.Get_StringValue(txtthird_country.Value), _
    '                            CommonUtility.Get_StringValue(txtback_country.Value), _
    '                            CommonUtility.Get_StringValue(txtplace_exibition.Value))
    '        Case 1
    '            'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '            '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

    '            If C_TotalRowDe.Text = count_data Then
    '                If (Not txtquantity1.Text = String.Empty = True) And CommonUtility.Get_StringValue(txtquantity1.Text) <> "" And CommonUtility.Get_Decimal(IIf(Not (txtquantity1.Text = String.Empty) = False, 0, txtquantity1.Value)) <> 0 Then
    '                    txtT_product.Text = All_str
    '                    GroupFooter1.Visible = True
    '                    txtTotalAll.Visible = True
    '                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & "        " & _
    '                                                                                    txtthird_country.Text & vbCrLf & _
    '                                                                                    txtback_country.Text & vbCrLf & _
    '                                                                                    txtplace_exibition.Text & vbCrLf & _
    '                                                                                    "       _____________________________"
    '                Else
    '                    txtT_product.Text = All_str & _
    '                                        "        " & _
    '                                                                                    txtthird_country.Text & vbCrLf & _
    '                                                                                    txtback_country.Text & vbCrLf & _
    '                                                                                    txtplace_exibition.Text & vbCrLf & _
    '                                                                                    "       _____________________________"
    '                End If

    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 2 'T2_back_country,T3_place_exibition
    '            'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '            '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

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
    '                                        "        " & _
    '                                                                                                    txtback_country.Text & vbCrLf & _
    '                                                                                                    txtplace_exibition.Text & vbCrLf & _
    '                                                                                                    "       _____________________________"
    '                End If

    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 3 'T3_place_exibition
    '            'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '            '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

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
    '        Case 4 'ไม่มีเลย
    '            'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '            '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

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
    '            'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '            '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

    '            If C_TotalRowDe.Text = count_data Then
    '                If (Not txtquantity1.Text = String.Empty = True) And CommonUtility.Get_StringValue(txtquantity1.Text) <> "" And CommonUtility.Get_Decimal(IIf(Not (txtquantity1.Text = String.Empty) = False, 0, txtquantity1.Value)) <> 0 Then
    '                    txtT_product.Text = All_str
    '                    GroupFooter1.Visible = True
    '                    txtTotalAll.Visible = True
    '                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & _
    '                                                                                   "       _____________________________"
    '                Else
    '                    txtT_product.Text = All_str & _
    '                                       "       _____________________________"
    '                End If

    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 6 'T3_place_exibition
    '            'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '            '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

    '            If C_TotalRowDe.Text = count_data Then
    '                If (Not txtquantity1.Text = String.Empty = True) And CommonUtility.Get_StringValue(txtquantity1.Text) <> "" And CommonUtility.Get_Decimal(IIf(Not (txtquantity1.Text = String.Empty) = False, 0, txtquantity1.Value)) <> 0 Then
    '                    txtT_product.Text = All_str
    '                    GroupFooter1.Visible = True
    '                    txtTotalAll.Visible = True
    '                    txtTotalAll.Text = "TOTAL: " & BahtOnly(txtquantity1.Text, txtq_unit_code1.Text) & vbCrLf & _
    '                                                                                   "       _____________________________"
    '                Else
    '                    txtT_product.Text = All_str & _
    '                                       "       _____________________________"
    '                End If

    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 7 'T1_third_country,T2_back_country
    '            'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '            '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

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
    '            'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '            '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

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
    '            'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '            '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

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
    '    Title_StrDetail()
    '    Select Case Check_Third(CommonUtility.Get_StringValue(txtthird_country.Value), _
    '                            CommonUtility.Get_StringValue(txtback_country.Value), _
    '                            CommonUtility.Get_StringValue(txtplace_exibition.Value))
    '        Case 1
    '            'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '            '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

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
    '            'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '            '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

    '            If C_TotalRowDe.Text = count_data Then
    '                txtT_product.Text = All_str & _
    '                txtback_country.Text & vbCrLf & _
    '                                                                            txtplace_exibition.Text & vbCrLf & _
    '                                                                            "       _____________________________"
    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 3 'T3_place_exibition
    '            'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '            '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

    '            If C_TotalRowDe.Text = count_data Then
    '                txtT_product.Text = All_str & _
    '                 "       _____________________________"
    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 4 'ไม่มีเลย
    '            'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '            '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

    '            If C_TotalRowDe.Text = count_data Then
    '                txtT_product.Text = All_str & _
    '                 "       _____________________________"
    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 5 'T2_back_country
    '            'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '            '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

    '            If C_TotalRowDe.Text = count_data Then
    '                txtT_product.Text = All_str & _
    '                vbCrLf & "       _____________________________"
    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 6 'T3_place_exibition
    '            'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '            '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

    '            If C_TotalRowDe.Text = count_data Then
    '                txtT_product.Text = All_str & _
    '                 "       _____________________________"
    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 7 'T1_third_country,T2_back_country
    '            'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '            '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

    '            If C_TotalRowDe.Text = count_data Then
    '                txtT_product.Text = All_str & _
    '                txtthird_country.Text & vbCrLf & _
    '                                                            txtback_country.Text & vbCrLf & _
    '                                                            "       _____________________________"
    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 8 'T1_third_country,T3_place_exibition
    '            'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '            '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

    '            If C_TotalRowDe.Text = count_data Then
    '                txtT_product.Text = All_str & _
    '                 txtthird_country.Text & vbCrLf & _
    '                                                            txtplace_exibition.Text & vbCrLf & _
    '                                                            "       _____________________________"
    '            Else
    '                txtT_product.Text = All_str
    '            End If
    '        Case 9 'T1_third_country
    '            'All_str = "HS. CODE. " & tariff_All(CommonUtility.Get_StringValue(txttariff_code.Text)) & vbCrLf _
    '            '& txtproduct_n1.Text & txtproduct_n2.Text & " ****" & vbCrLf

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

        'by rut rvc new
        Select Case CommonUtility.Get_StringValue(txtCheckGrossDetail.Text)
            Case "GWDetail"
                txtGrossTxt.Text = CHeck_DisPlayF(CommonUtility.Get_StringValue(C_TotalRowDe.Text), CommonUtility.Get_StringValue(txtCheckGrossDetail.Text))
        End Select

        'CHeck_DisPlay()
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


        ElseIf (CommonUtility.Get_StringValue(txtquantity1.Text) <> "" And CommonUtility.Get_Decimal(txtquantity1.Text) <> 0 And CommonUtility.Get_StringValue(txtq_unit_code1.Text) <> "") = True And _
         (CommonUtility.Get_StringValue(txtquantity2.Text) <> "" And CommonUtility.Get_Decimal(txtquantity2.Text) <> 0 And CommonUtility.Get_StringValue(txtq_unit_code2.Text) <> "") = True And _
         (CommonUtility.Get_StringValue(txtquantity3.Text) <> "" And CommonUtility.Get_Decimal(txtquantity3.Text) <> 0 And CommonUtility.Get_StringValue(txtq_unit_code3.Text) <> "") = True And _
         (CommonUtility.Get_StringValue(txtquantity4.Text) <> "" And CommonUtility.Get_Decimal(txtquantity4.Text) <> 0 And CommonUtility.Get_StringValue(txtq_unit_code4.Text) <> "") = False And _
         (CommonUtility.Get_StringValue(txtquantity5.Text) <> "" And CommonUtility.Get_Decimal(txtquantity5.Text) <> 0 And CommonUtility.Get_StringValue(txtq_unit_code5.Text) <> "") = False Then
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

        ElseIf (CommonUtility.Get_StringValue(txtquantity1.Text) <> "" And CommonUtility.Get_Decimal(txtquantity1.Text) <> 0 And CommonUtility.Get_StringValue(txtq_unit_code1.Text) <> "") = True And _
          (CommonUtility.Get_StringValue(txtquantity2.Text) <> "" And CommonUtility.Get_Decimal(txtquantity2.Text) <> 0 And CommonUtility.Get_StringValue(txtq_unit_code2.Text) <> "") = True And _
          (CommonUtility.Get_StringValue(txtquantity3.Text) <> "" And CommonUtility.Get_Decimal(txtquantity3.Text) <> 0 And CommonUtility.Get_StringValue(txtq_unit_code3.Text) <> "") = False And _
          (CommonUtility.Get_StringValue(txtquantity4.Text) <> "" And CommonUtility.Get_Decimal(txtquantity4.Text) <> 0 And CommonUtility.Get_StringValue(txtq_unit_code4.Text) <> "") = False And _
          (CommonUtility.Get_StringValue(txtquantity5.Text) <> "" And CommonUtility.Get_Decimal(txtquantity5.Text) <> 0 And CommonUtility.Get_StringValue(txtq_unit_code5.Text) <> "") = False Then
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

        ElseIf (CommonUtility.Get_StringValue(txtquantity1.Text) <> "" And CommonUtility.Get_Decimal(txtquantity1.Text) <> 0 And CommonUtility.Get_StringValue(txtq_unit_code1.Text) <> "") = True And _
          (CommonUtility.Get_StringValue(txtquantity2.Text) <> "" And CommonUtility.Get_Decimal(txtquantity2.Text) <> 0 And CommonUtility.Get_StringValue(txtq_unit_code2.Text) <> "") = False And _
          (CommonUtility.Get_StringValue(txtquantity3.Text) <> "" And CommonUtility.Get_Decimal(txtquantity3.Text) <> 0 And CommonUtility.Get_StringValue(txtq_unit_code3.Text) <> "") = False And _
          (CommonUtility.Get_StringValue(txtquantity4.Text) <> "" And CommonUtility.Get_Decimal(txtquantity4.Text) <> 0 And CommonUtility.Get_StringValue(txtq_unit_code4.Text) <> "") = False And _
          (CommonUtility.Get_StringValue(txtquantity5.Text) <> "" And CommonUtility.Get_Decimal(txtquantity5.Text) <> 0 And CommonUtility.Get_StringValue(txtq_unit_code5.Text) <> "") = False Then
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

    'ไม่ได้ใช้
    Sub CHeck_DisPlay()


        Dim sss As String = txtgross_weight.Text

        Dim str_gross As String

        'str_gross = txtgross_weight.Text

        'เรื่อง invoice ต่างประเทศ
        Dim str_NumInvoice As String = CommonUtility.Get_StringValue(txtNumInvoice.Text)
        'เรื่อง มูลค่า ต่างประเทศ
        Dim str_USDInvoice As String = CommonUtility.Get_StringValue(txtUSDInvoice.Text)

        Select Case txtshow_check.Text
            Case "100" ' เรื่อง invoice ต่างประเทศ
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

            Case Else 'ไม่ใช่ invoice ต่างประเทศ
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

    'ไม่ได้ใช้
    'Function CHeck_DisPlayF() As String
    '    CallInvoice_()
    '    Dim str_GrossF As String

    '    Dim sss As String = txtgross_weight.Text

    '    Dim str_gross As String

    '    'str_gross = txtgross_weight.Text

    '    'เรื่อง invoice ต่างประเทศ
    '    Dim str_NumInvoice As String = CommonUtility.Get_StringValue(txtNumInvoice.Text)
    '    'เรื่อง มูลค่า ต่างประเทศ
    '    Dim str_USDInvoice As String
    '    If CommonUtility.Get_StringValue(txtUSDInvoice.Value) <> "" Then
    '        str_USDInvoice = Format(CommonUtility.Get_Decimal(txtUSDInvoice.Value), "#,##0.00") & " USD"
    '    Else
    '        str_USDInvoice = ""
    '    End If

    '    Select Case txtshow_check.Text
    '        Case "100" ' เรื่อง invoice ต่างประเทศ
    '            If Mid(CommonUtility.Get_StringValue(txtFOBDisplay.Value), 1, 50) = "Display" Then

    '                If Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "NET WEIGHT" Then

    '                    str_gross = NetWeight_(TNet, txtunit_code2.Value) 'Mid(Tnet, 1, 9) & " " & Mid(txtunit_code2.Value, 1, 10) '& vbNewLine & FobTxtAll()
    '                    str_GrossF = str_gross & vbNewLine & str_USDInvoice

    '                ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "GROSS WEIGHT - NET WEIGHT" Then
    '                    str_gross = GrossTxt() & vbNewLine & NetWeight_(TNet, txtunit_code2.Value) 'Mid(Tnet, 1, 9) & " " & Mid(txtunit_code2.Value, 1, 10) '& vbNewLine & FobTxtAll()
    '                    str_GrossF = str_gross & vbNewLine & str_USDInvoice
    '                ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "GROSS WEIGHT" Then
    '                    str_gross = GrossTxt() '& vbNewLine & FobTxtAll()
    '                    str_GrossF = str_gross & vbNewLine & str_USDInvoice
    '                ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "GW" Then 'ระบบเก่า มีแต่ GW
    '                    str_gross = GrossTxt() '& vbNewLine & FobTxtAll()
    '                    str_GrossF = str_gross & vbNewLine & Callinvoice_board(1)
    '                ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "" Then
    '                    str_gross = GrossTxt() '& vbNewLine & FobTxtAll()
    '                    str_GrossF = str_gross & vbNewLine & str_USDInvoice
    '                End If
    '            ElseIf Mid(CommonUtility.Get_StringValue(txtFOBDisplay.Value), 1, 50) = "NoDisplay" Then
    '                If Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "NET WEIGHT" Then
    '                    str_gross = NetWeight_(TNet, txtunit_code2.Value) 'Mid(Tnet, 1, 9) & " " & Mid(txtunit_code2.Value, 1, 10)
    '                    str_GrossF = str_gross & vbNewLine & str_USDInvoice
    '                ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "GROSS WEIGHT - NET WEIGHT" Then
    '                    str_gross = GrossTxt() & vbNewLine & NetWeight_(TNet, txtunit_code2.Value) 'Mid(Tnet, 1, 9) & " " & Mid(txtunit_code2.Value, 1, 10)
    '                    str_GrossF = str_gross & vbNewLine & str_USDInvoice
    '                ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "GROSS WEIGHT" Then
    '                    str_gross = GrossTxt()
    '                    str_GrossF = str_gross & vbNewLine & str_USDInvoice
    '                ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "" Then
    '                    str_gross = GrossTxt()
    '                    str_GrossF = str_gross & vbNewLine & str_USDInvoice
    '                End If

    '            ElseIf Mid(CommonUtility.Get_StringValue(txtFOBDisplay.Value), 1, 50) = "" Then

    '                If Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "NET WEIGHT" Then
    '                    str_gross = NetWeight_(TNet, txtunit_code2.Value) 'Mid(Tnet, 1, 9) & " " & Mid(txtunit_code2.Value, 1, 10) '& vbNewLine & FobTxtAll()
    '                    str_GrossF = str_gross & vbNewLine & str_USDInvoice
    '                ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "GROSS WEIGHT - NET WEIGHT" Then
    '                    str_gross = GrossTxt() & vbNewLine & NetWeight_(TNet, txtunit_code2.Value) 'Mid(Tnet, 1, 9) & " " & Mid(txtunit_code2.Value, 1, 10) '& vbNewLine & FobTxtAll()
    '                    str_GrossF = str_gross & vbNewLine & str_USDInvoice
    '                ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "GROSS WEIGHT" Then
    '                    str_gross = GrossTxt() '& vbNewLine & FobTxtAll()
    '                    str_GrossF = str_gross & vbNewLine & str_USDInvoice
    '                ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "" Then
    '                    str_gross = GrossTxt() '& vbNewLine & FobTxtAll()
    '                    str_GrossF = str_gross & vbNewLine & str_USDInvoice
    '                End If
    '            End If

    '        Case Else 'ไม่ใช่ invoice ต่างประเทศ
    '            If Mid(CommonUtility.Get_StringValue(txtFOBDisplay.Value), 1, 50) = "Display" Then

    '                If Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "NET WEIGHT" Then

    '                    str_gross = NetWeight_(TNet, txtunit_code2.Value) & vbNewLine & FobTxtAllSum() 'Mid(Tnet, 1, 9) & " " & Mid(txtunit_code2.Value, 1, 10) '& vbNewLine & FobTxtAll()
    '                    str_GrossF = str_gross

    '                ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "GROSS WEIGHT - NET WEIGHT" Then
    '                    str_gross = GrossTxt() & vbNewLine & NetWeight_(TNet, txtunit_code2.Value) & vbNewLine & FobTxtAllSum() 'Mid(Tnet, 1, 9) & " " & Mid(txtunit_code2.Value, 1, 10) '& vbNewLine & FobTxtAll()
    '                    str_GrossF = str_gross
    '                ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "GROSS WEIGHT" Then
    '                    str_gross = GrossTxt() & vbNewLine & FobTxtAllSum()
    '                    str_GrossF = str_gross
    '                ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "GW" Then 'ระบบเก่า
    '                    str_gross = GrossTxt() & vbNewLine & FobTxtAllSum()
    '                    str_GrossF = str_gross
    '                ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "" Then
    '                    str_gross = GrossTxt() & vbNewLine & FobTxtAllSum()
    '                    str_GrossF = str_gross
    '                End If
    '            ElseIf Mid(CommonUtility.Get_StringValue(txtFOBDisplay.Value), 1, 50) = "NoDisplay" Then
    '                If Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "NET WEIGHT" Then
    '                    str_gross = NetWeight_(TNet, txtunit_code2.Value) 'Mid(Tnet, 1, 9) & " " & Mid(txtunit_code2.Value, 1, 10)
    '                    str_GrossF = str_gross
    '                ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "GROSS WEIGHT - NET WEIGHT" Then
    '                    str_gross = GrossTxt() & vbNewLine & NetWeight_(TNet, txtunit_code2.Value) 'Mid(Tnet, 1, 9) & " " & Mid(txtunit_code2.Value, 1, 10)
    '                    str_GrossF = str_gross
    '                ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "GROSS WEIGHT" Then
    '                    str_gross = GrossTxt()
    '                    str_GrossF = str_gross
    '                ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "" Then
    '                    str_gross = GrossTxt()
    '                    str_GrossF = str_gross
    '                End If

    '            ElseIf Mid(CommonUtility.Get_StringValue(txtFOBDisplay.Value), 1, 50) = "" Then

    '                If Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "NET WEIGHT" Then
    '                    str_gross = NetWeight_(TNet, txtunit_code2.Value) 'Mid(Tnet, 1, 9) & " " & Mid(txtunit_code2.Value, 1, 10) '& vbNewLine & FobTxtAll()
    '                    str_GrossF = str_gross
    '                ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "GROSS WEIGHT - NET WEIGHT" Then
    '                    str_gross = GrossTxt() & vbNewLine & NetWeight_(TNet, txtunit_code2.Value) 'Mid(Tnet, 1, 9) & " " & Mid(txtunit_code2.Value, 1, 10) '& vbNewLine & FobTxtAll()
    '                    str_GrossF = str_gross
    '                ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "GROSS WEIGHT" Then
    '                    str_gross = GrossTxt()
    '                    str_GrossF = str_gross
    '                ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "" Then
    '                    str_gross = GrossTxt()
    '                    str_GrossF = str_gross
    '                End If
    '            End If

    '    End Select
    '    Return str_GrossF
    'End Function

    Function check_returnBox8(ByVal Temp_box8 As String) As String
        Dim str_case As String = "0"
        Select Case Temp_box8
            Case """RVC""", """2RVC""", """2RV""", """CC+RVC""", """CTH+RVC""", """CTSH+RVC"""
                str_case = "1"
        End Select

        Return str_case
    End Function
    Function CHeck_DisPlayF(ByVal CountAllDetail As String, ByVal CheckDetailGross As String) As String
        'กันไว้เนื่องจาก ฟอร์มใหม่ตรวจสอบหน่วย grossweight ที่รายการแรกรายการเดียว ไม่สนรายการอื่น
        Dim TempUnit3 As String = ""
        TempUnit3 = CheckUnitDetail(txtinvh_run_auto.Value)

        CallInvoice_()
        Dim str_GrossF As String

        Dim sss As String = txtgross_weight.Text

        Dim str_gross As String

        'str_gross = txtgross_weight.Text

        'เรื่อง invoice ต่างประเทศ
        Dim str_NumInvoice As String = CommonUtility.Get_StringValue(txtNumInvoice.Text)
        'เรื่อง มูลค่า ต่างประเทศ
        Dim str_USDInvoice As String
        If CommonUtility.Get_StringValue(txtCheckGrossDetail.Text) = "GWDetail" Then
            'by rut FOBOther
            str_USDInvoice = Format(CommonUtility.Get_Decimal(txtUSDInvoiceDetail.Text), "#,##0.00##") & " " & txtCurrency_Code.Text '& " USD" 'invoice ต่างประเทศ แสดงมูลค่า รายการทุกรายการ
        Else
            str_USDInvoice = Str_USDinvoiceDetail 'invoice ต่างประเทศ แสดงมูลค่า รายการแรกรายการเดียว
        End If

        Select Case txtshow_check.Text
            Case "100" ' เรื่อง invoice ต่างประเทศ
                Select Case CheckDetailGross
                    Case "GWDetail" 'แยกรายการ ส่วน third
                        Select Case Mid(CommonUtility.Get_StringValue(txtletter.Value), 1, 50)
                            Case "3" 'แสดงมูลค่า Detail ส่วน third [ต้องแสดงมูลค่า แต่ละรายการสินค้าหมดที่เป็น case 3 ยกเว้นยอดรวม]
                                If Mid(CommonUtility.Get_StringValue(txtFOBDisplay.Value), 1, 50) = "Display" Then
                                    If Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "NET WEIGHT" Then
                                        str_gross = NetWeight_(txtnet_weight.Value, txtunit_code2.Value) 'Mid(Tnet, 1, 9) & " " & Mid(txtunit_code2.Value, 1, 10) '& vbNewLine & FobTxtAll()
                                        Select Case C_TotalRowDe.Text
                                            Case 1 'รายการแรก
                                                str_GrossF = str_gross & vbNewLine & str_USDInvoice
                                            Case Else 'รายการที่ไม่ใช่รายการที่ 1
                                                If Cpage = CountAllDetail Then 'check เงื่อนไขรายการสุดท้าย
                                                    Select Case Check_CaseRVCCount
                                                        Case 9 'มีอย่างอื่นปน ไม่แสดงมูลค่ารวม
                                                            str_GrossF = str_gross & vbNewLine & str_USDInvoice & vbNewLine & _
                                                                      "_______" & _
                                                                      vbNewLine & Format(TNet, "#,##0.00##") & " " & CommonUtility.Get_StringValue(txtunit_code2.Value)

                                                        Case Else 'มี RVC อย่างเดียว แสดงมูลค่ารวม
                                                            str_GrossF = str_gross & vbNewLine & str_USDInvoice & vbNewLine & _
                                                                       "_______" & _
                                                                       vbNewLine & Format(TNet, "#,##0.00##") & " " & CommonUtility.Get_StringValue(txtunit_code2.Value) & _
                                                                       vbNewLine & Format(TUSD, "#,##0.00##") & " " & txtCurrency_Code.Text

                                                    End Select
                                                Else
                                                    str_GrossF = str_gross & vbNewLine & str_USDInvoice
                                                End If
                                        End Select

                                    ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "GROSS WEIGHT - NET WEIGHT" Then
                                        str_gross = GrossTxtDetail() & vbNewLine & NetWeight_(txtnet_weight.Value, txtunit_code2.Value) 'Mid(Tnet, 1, 9) & " " & Mid(txtunit_code2.Value, 1, 10) '& vbNewLine & FobTxtAll()
                                        Select Case C_TotalRowDe.Text
                                            Case 1 'รายการแรก
                                                str_GrossF = str_gross & vbNewLine & str_USDInvoice
                                            Case Else 'รายการที่ไม่ใช่รายการที่ 1
                                                If Cpage = CountAllDetail Then 'check เงื่อนไขรายการสุดท้าย
                                                    Select Case Check_CaseRVCCount
                                                        Case 9 'มีอย่างอื่นปน ไม่แสดงมูลค่ารวม
                                                            str_GrossF = str_gross & vbNewLine & str_USDInvoice & vbNewLine & _
                                                                       "_______" & vbNewLine & Format(TGross, "#,##0.00##") & " " & TempUnit3 & _
                                                                       vbNewLine & Format(TNet, "#,##0.00##") & " " & CommonUtility.Get_StringValue(txtunit_code2.Value)

                                                        Case Else 'มี RVC อย่างเดียว แสดงมูลค่ารวม
                                                            str_GrossF = str_gross & vbNewLine & str_USDInvoice & vbNewLine & _
                                                                       "_______" & vbNewLine & Format(TGross, "#,##0.00##") & " " & TempUnit3 & _
                                                                       vbNewLine & Format(TNet, "#,##0.00##") & " " & CommonUtility.Get_StringValue(txtunit_code2.Value) & _
                                                                       vbNewLine & Format(TUSD, "#,##0.00##") & " " & txtCurrency_Code.Text

                                                    End Select
                                                Else
                                                    str_GrossF = str_gross & vbNewLine & str_USDInvoice
                                                End If
                                        End Select

                                    ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "GROSS WEIGHT" Then
                                        str_gross = GrossTxtDetail() '& vbNewLine & FobTxtAll()
                                        Select Case C_TotalRowDe.Text
                                            Case 1 'รายการแรก
                                                str_GrossF = str_gross & vbNewLine & str_USDInvoice
                                            Case Else 'รายการที่ไม่ใช่รายการที่ 1
                                                If Cpage = CountAllDetail Then 'check เงื่อนไขรายการสุดท้าย
                                                    Select Case Check_CaseRVCCount
                                                        Case 9 'มีอย่างอื่นปน ไม่แสดงมูลค่ารวม
                                                            str_GrossF = str_gross & vbNewLine & str_USDInvoice & vbNewLine & _
                                                                       "_______" & vbNewLine & Format(TGross, "#,##0.00##") & " " & TempUnit3

                                                        Case Else 'มี RVC อย่างเดียว แสดงมูลค่ารวม
                                                            str_GrossF = str_gross & vbNewLine & str_USDInvoice & vbNewLine & _
                                                                       "_______" & vbNewLine & Format(TGross, "#,##0.00##") & " " & TempUnit3 & _
                                                                       vbNewLine & Format(TUSD, "#,##0.00##") & " " & txtCurrency_Code.Text

                                                    End Select
                                                Else
                                                    str_GrossF = str_gross & vbNewLine & str_USDInvoice
                                                End If
                                        End Select

                                    ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "GW" Then 'ระบบเก่า มีแต่ GW
                                        str_gross = GrossTxtDetail() '& vbNewLine & FobTxtAll()
                                        Select Case C_TotalRowDe.Text
                                            Case 1 'รายการแรก
                                                str_GrossF = str_gross & vbNewLine & Callinvoice_board(1)
                                            Case Else 'รายการที่ไม่ใช่รายการที่ 1
                                                If Cpage = CountAllDetail Then 'check เงื่อนไขรายการสุดท้าย
                                                    Select Case Check_CaseRVCCount
                                                        Case 9 'มีอย่างอื่นปน ไม่แสดงมูลค่ารวม
                                                            str_GrossF = str_gross & vbNewLine & Callinvoice_board(1) & vbNewLine & _
                                                                        "_______" & vbNewLine & Format(TGross, "#,##0.00##") & " " & TempUnit3

                                                        Case Else 'มี RVC อย่างเดียว แสดงมูลค่ารวม
                                                            str_GrossF = str_gross & vbNewLine & Callinvoice_board(1) & vbNewLine & _
                                                                       "_______" & vbNewLine & Format(TGross, "#,##0.00##") & " " & TempUnit3 & _
                                                                       vbNewLine & Format(TUSD, "#,##0.00##") & " " & txtCurrency_Code.Text

                                                    End Select
                                                Else
                                                    str_GrossF = str_gross & vbNewLine & Callinvoice_board(1)
                                                End If
                                        End Select

                                    ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "" Then
                                        str_gross = GrossTxtDetail() '& vbNewLine & FobTxtAll()
                                        Select Case C_TotalRowDe.Text
                                            Case 1 'รายการแรก
                                                str_GrossF = str_gross & vbNewLine & str_USDInvoice
                                            Case Else 'รายการที่ไม่ใช่รายการที่ 1
                                                If Cpage = CountAllDetail Then 'check เงื่อนไขรายการสุดท้าย
                                                    Select Case Check_CaseRVCCount
                                                        Case 9 'มีอย่างอื่นปน ไม่แสดงมูลค่ารวม
                                                            str_GrossF = str_gross & vbNewLine & str_USDInvoice & vbNewLine & _
                                                                       "_______" & vbNewLine & Format(TGross, "#,##0.00##") & " " & TempUnit3

                                                        Case Else 'มี RVC อย่างเดียว แสดงมูลค่ารวม
                                                            str_GrossF = str_gross & vbNewLine & str_USDInvoice & vbNewLine & _
                                                                       "_______" & vbNewLine & Format(TGross, "#,##0.00##") & " " & TempUnit3 & _
                                                                       vbNewLine & Format(TUSD, "#,##0.00##") & " " & txtCurrency_Code.Text

                                                    End Select
                                                Else
                                                    str_GrossF = str_gross & vbNewLine & str_USDInvoice
                                                End If
                                        End Select

                                    End If
                                ElseIf Mid(CommonUtility.Get_StringValue(txtFOBDisplay.Value), 1, 50) = "NoDisplay" Then 'ถึงจะอยู่ใน case 3 ที่ต้องแสดงมูลค่าแต่ถ้าเข้าเงื่อนไขนี้ มูลค่าไม่แสดง [เคสนี้ไม่มี เผื่อไว้ ในอนาคต]
                                    If Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "NET WEIGHT" Then
                                        str_gross = NetWeight_(txtnet_weight.Value, txtunit_code2.Value) 'Mid(Tnet, 1, 9) & " " & Mid(txtunit_code2.Value, 1, 10)
                                        Select Case C_TotalRowDe.Text
                                            Case 1 'รายการแรก
                                                str_GrossF = str_gross
                                            Case Else 'รายการที่ไม่ใช่รายการที่ 1
                                                If Cpage = CountAllDetail Then 'check เงื่อนไขรายการสุดท้าย
                                                    Select Case Check_CaseRVCCount
                                                        Case 9 'มีอย่างอื่นปน ไม่แสดงมูลค่ารวม
                                                            str_GrossF = str_gross & vbNewLine & _
                                                                       "_______" & _
                                                                       vbNewLine & Format(TNet, "#,##0.00##") & " " & CommonUtility.Get_StringValue(txtunit_code2.Value)

                                                        Case Else 'มี RVC อย่างเดียว แสดงมูลค่ารวม
                                                            str_GrossF = str_gross & vbNewLine & _
                                                                       "_______" & _
                                                                       vbNewLine & Format(TNet, "#,##0.00##") & " " & CommonUtility.Get_StringValue(txtunit_code2.Value)

                                                    End Select
                                                Else
                                                    str_GrossF = str_gross
                                                End If
                                        End Select

                                    ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "GROSS WEIGHT - NET WEIGHT" Then
                                        str_gross = GrossTxtDetail() & vbNewLine & NetWeight_(txtnet_weight.Value, txtunit_code2.Value) 'Mid(Tnet, 1, 9) & " " & Mid(txtunit_code2.Value, 1, 10)
                                        Select Case C_TotalRowDe.Text
                                            Case 1 'รายการแรก
                                                str_GrossF = str_gross
                                            Case Else 'รายการที่ไม่ใช่รายการที่ 1
                                                If Cpage = CountAllDetail Then 'check เงื่อนไขรายการสุดท้าย
                                                    Select Case Check_CaseRVCCount
                                                        Case 9 'มีอย่างอื่นปน ไม่แสดงมูลค่ารวม
                                                            str_GrossF = str_gross & vbNewLine & _
                                                                       "_______" & vbNewLine & Format(TGross, "#,##0.00##") & " " & TempUnit3 & _
                                                                       vbNewLine & Format(TNet, "#,##0.00##") & " " & CommonUtility.Get_StringValue(txtunit_code2.Value)

                                                        Case Else 'มี RVC อย่างเดียว แสดงมูลค่ารวม
                                                            str_GrossF = str_gross & vbNewLine & _
                                                                        "_______" & vbNewLine & Format(TGross, "#,##0.00##") & " " & TempUnit3 & _
                                                                        vbNewLine & Format(TNet, "#,##0.00##") & " " & CommonUtility.Get_StringValue(txtunit_code2.Value)

                                                    End Select
                                                Else
                                                    str_GrossF = str_gross
                                                End If
                                        End Select

                                    ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "GROSS WEIGHT" Then
                                        str_gross = GrossTxtDetail()
                                        Select Case C_TotalRowDe.Text
                                            Case 1 'รายการแรก
                                                str_GrossF = str_gross
                                            Case Else 'รายการที่ไม่ใช่รายการที่ 1
                                                If Cpage = CountAllDetail Then 'check เงื่อนไขรายการสุดท้าย
                                                    Select Case Check_CaseRVCCount
                                                        Case 9 'มีอย่างอื่นปน ไม่แสดงมูลค่ารวม
                                                            str_GrossF = str_gross & vbNewLine & _
                                                                   "_______" & vbNewLine & Format(TGross, "#,##0.00##") & " " & TempUnit3

                                                        Case Else 'มี RVC อย่างเดียว แสดงมูลค่ารวม
                                                            str_GrossF = str_gross & vbNewLine & _
                                                                   "_______" & vbNewLine & Format(TGross, "#,##0.00##") & " " & TempUnit3

                                                    End Select
                                                Else
                                                    str_GrossF = str_gross
                                                End If
                                        End Select

                                    ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "" Then
                                        str_gross = GrossTxtDetail()
                                        Select Case C_TotalRowDe.Text
                                            Case 1 'รายการแรก
                                                str_GrossF = str_gross
                                            Case Else 'รายการที่ไม่ใช่รายการที่ 1
                                                If Cpage = CountAllDetail Then 'check เงื่อนไขรายการสุดท้าย
                                                    Select Case Check_CaseRVCCount
                                                        Case 9 'มีอย่างอื่นปน ไม่แสดงมูลค่ารวม
                                                            str_GrossF = str_gross & vbNewLine & _
                                                                   "_______" & vbNewLine & Format(TGross, "#,##0.00##") & " " & TempUnit3

                                                        Case Else 'มี RVC อย่างเดียว แสดงมูลค่ารวม
                                                            str_GrossF = str_gross & vbNewLine & _
                                                                    "_______" & vbNewLine & Format(TGross, "#,##0.00##") & " " & TempUnit3

                                                    End Select
                                                Else
                                                    str_GrossF = str_gross
                                                End If
                                        End Select

                                    End If

                                ElseIf Mid(CommonUtility.Get_StringValue(txtFOBDisplay.Value), 1, 50) = "" Then 'กรณี ไม่มีค่าในช่องนี้ [เผื่อไว้]

                                    If Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "NET WEIGHT" Then
                                        str_gross = NetWeight_(txtnet_weight.Value, txtunit_code2.Value) 'Mid(Tnet, 1, 9) & " " & Mid(txtunit_code2.Value, 1, 10) '& vbNewLine & FobTxtAll()
                                        Select Case C_TotalRowDe.Text
                                            Case 1 'รายการแรก
                                                str_GrossF = str_gross
                                            Case Else 'รายการที่ไม่ใช่รายการที่ 1
                                                If Cpage = CountAllDetail Then 'check เงื่อนไขรายการสุดท้าย
                                                    Select Case Check_CaseRVCCount
                                                        Case 9 'มีอย่างอื่นปน ไม่แสดงมูลค่ารวม
                                                            str_GrossF = str_gross & vbNewLine & str_USDInvoice & vbNewLine & _
                                                                   "_______" & _
                                                                   vbNewLine & Format(TNet, "#,##0.00##") & " " & CommonUtility.Get_StringValue(txtunit_code2.Value)

                                                        Case Else 'มี RVC อย่างเดียว แสดงมูลค่ารวม
                                                            str_GrossF = str_gross & vbNewLine & str_USDInvoice & vbNewLine & _
                                                                   "_______" & _
                                                                   vbNewLine & Format(TNet, "#,##0.00##") & " " & CommonUtility.Get_StringValue(txtunit_code2.Value)

                                                    End Select
                                                Else
                                                    str_GrossF = str_gross
                                                End If
                                        End Select

                                    ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "GROSS WEIGHT - NET WEIGHT" Then
                                        str_gross = GrossTxtDetail() & vbNewLine & NetWeight_(txtnet_weight.Value, txtunit_code2.Value) 'Mid(Tnet, 1, 9) & " " & Mid(txtunit_code2.Value, 1, 10) '& vbNewLine & FobTxtAll()
                                        Select Case C_TotalRowDe.Text
                                            Case 1 'รายการแรก
                                                str_GrossF = str_gross
                                            Case Else 'รายการที่ไม่ใช่รายการที่ 1
                                                If Cpage = CountAllDetail Then 'check เงื่อนไขรายการสุดท้าย
                                                    Select Case Check_CaseRVCCount
                                                        Case 9 'มีอย่างอื่นปน ไม่แสดงมูลค่ารวม
                                                            str_GrossF = str_gross & vbNewLine & _
                                                                   "_______" & vbNewLine & Format(TGross, "#,##0.00##") & " " & TempUnit3 & _
                                                                   vbNewLine & Format(TNet, "#,##0.00##") & " " & CommonUtility.Get_StringValue(txtunit_code2.Value)

                                                        Case Else 'มี RVC อย่างเดียว แสดงมูลค่ารวม
                                                            str_GrossF = str_gross & vbNewLine & _
                                                                   "_______" & vbNewLine & Format(TGross, "#,##0.00##") & " " & TempUnit3 & _
                                                                   vbNewLine & Format(TNet, "#,##0.00##") & " " & CommonUtility.Get_StringValue(txtunit_code2.Value)

                                                    End Select
                                                Else
                                                    str_GrossF = str_gross
                                                End If
                                        End Select

                                    ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "GROSS WEIGHT" Then
                                        str_gross = GrossTxtDetail() '& vbNewLine & FobTxtAll()
                                        Select Case C_TotalRowDe.Text
                                            Case 1 'รายการแรก
                                                str_GrossF = str_gross
                                            Case Else 'รายการที่ไม่ใช่รายการที่ 1
                                                If Cpage = CountAllDetail Then 'check เงื่อนไขรายการสุดท้าย
                                                    Select Case Check_CaseRVCCount
                                                        Case 9 'มีอย่างอื่นปน ไม่แสดงมูลค่ารวม
                                                            str_GrossF = str_gross & vbNewLine & _
                                                                   "_______" & vbNewLine & Format(TGross, "#,##0.00##") & " " & TempUnit3

                                                        Case Else 'มี RVC อย่างเดียว แสดงมูลค่ารวม
                                                            str_GrossF = str_gross & vbNewLine & _
                                                                   "_______" & vbNewLine & Format(TGross, "#,##0.00##") & " " & TempUnit3

                                                    End Select
                                                Else
                                                    str_GrossF = str_gross
                                                End If
                                        End Select

                                    ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "" Then
                                        str_gross = GrossTxtDetail() '& vbNewLine & FobTxtAll()
                                        Select Case C_TotalRowDe.Text
                                            Case 1 'รายการแรก
                                                str_GrossF = str_gross
                                            Case Else 'รายการที่ไม่ใช่รายการที่ 1
                                                If Cpage = CountAllDetail Then 'check เงื่อนไขรายการสุดท้าย
                                                    Select Case Check_CaseRVCCount
                                                        Case 9 'มีอย่างอื่นปน ไม่แสดงมูลค่ารวม
                                                            str_GrossF = str_gross & vbNewLine & _
                                                                   "_______" & vbNewLine & Format(TGross, "#,##0.00##") & " " & TempUnit3

                                                        Case Else 'มี RVC อย่างเดียว แสดงมูลค่ารวม
                                                            str_GrossF = str_gross & vbNewLine & _
                                                                    "_______" & vbNewLine & Format(TGross, "#,##0.00##") & " " & TempUnit3

                                                    End Select
                                                Else
                                                    str_GrossF = str_gross
                                                End If
                                        End Select

                                    End If
                                End If
                            Case Else ' ไม่แสดงมูลค่า Detail ส่วน third letter ไม่เท่ากับ 3
                                If Mid(CommonUtility.Get_StringValue(txtFOBDisplay.Value), 1, 50) = "Display" Then
                                    If Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "NET WEIGHT" Then
                                        str_gross = NetWeight_(txtnet_weight.Value, txtunit_code2.Value) 'Mid(Tnet, 1, 9) & " " & Mid(txtunit_code2.Value, 1, 10) '& vbNewLine & FobTxtAll()
                                        Select Case C_TotalRowDe.Text
                                            Case 1 'รายการแรก
                                                str_GrossF = str_gross
                                            Case Else 'รายการที่ไม่ใช่รายการที่ 1
                                                If Cpage = CountAllDetail Then 'check เงื่อนไขรายการสุดท้าย
                                                    Select Case Check_CaseRVCCount
                                                        Case 9 'มีอย่างอื่นปน ไม่แสดงมูลค่ารวม
                                                            str_GrossF = str_gross & vbNewLine & _
                                                                   "_______" & _
                                                                   vbNewLine & Format(TNet, "#,##0.00##") & " " & CommonUtility.Get_StringValue(txtunit_code2.Value)

                                                        Case Else 'มี RVC อย่างเดียว แสดงมูลค่ารวม
                                                            str_GrossF = str_gross & vbNewLine & _
                                                                   "_______" & _
                                                                   vbNewLine & Format(TNet, "#,##0.00##") & " " & CommonUtility.Get_StringValue(txtunit_code2.Value)

                                                    End Select
                                                Else
                                                    str_GrossF = str_gross
                                                End If
                                        End Select

                                    ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "GROSS WEIGHT - NET WEIGHT" Then
                                        str_gross = GrossTxtDetail() & vbNewLine & NetWeight_(txtnet_weight.Value, txtunit_code2.Value) 'Mid(Tnet, 1, 9) & " " & Mid(txtunit_code2.Value, 1, 10) '& vbNewLine & FobTxtAll()
                                        Select Case C_TotalRowDe.Text
                                            Case 1 'รายการแรก
                                                str_GrossF = str_gross
                                            Case Else 'รายการที่ไม่ใช่รายการที่ 1
                                                If Cpage = CountAllDetail Then 'check เงื่อนไขรายการสุดท้าย
                                                    Select Case Check_CaseRVCCount
                                                        Case 9 'มีอย่างอื่นปน ไม่แสดงมูลค่ารวม
                                                            str_GrossF = str_gross & vbNewLine & _
                                                                   "_______" & vbNewLine & Format(TGross, "#,##0.00##") & " " & TempUnit3 & _
                                                                   vbNewLine & Format(TNet, "#,##0.00##") & " " & CommonUtility.Get_StringValue(txtunit_code2.Value)

                                                        Case Else 'มี RVC อย่างเดียว แสดงมูลค่ารวม
                                                            str_GrossF = str_gross & vbNewLine & _
                                                                   "_______" & vbNewLine & Format(TGross, "#,##0.00##") & " " & TempUnit3 & _
                                                                   vbNewLine & Format(TNet, "#,##0.00##") & " " & CommonUtility.Get_StringValue(txtunit_code2.Value)

                                                    End Select
                                                Else
                                                    str_GrossF = str_gross
                                                End If
                                        End Select

                                    ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "GROSS WEIGHT" Then
                                        str_gross = GrossTxtDetail() '& vbNewLine & FobTxtAll()
                                        Select Case C_TotalRowDe.Text
                                            Case 1 'รายการแรก
                                                str_GrossF = str_gross
                                            Case Else 'รายการที่ไม่ใช่รายการที่ 1
                                                If Cpage = CountAllDetail Then 'check เงื่อนไขรายการสุดท้าย
                                                    Select Case Check_CaseRVCCount
                                                        Case 9 'มีอย่างอื่นปน ไม่แสดงมูลค่ารวม
                                                            str_GrossF = str_gross & vbNewLine & _
                                                                   "_______" & vbNewLine & Format(TGross, "#,##0.00##") & " " & TempUnit3

                                                        Case Else 'มี RVC อย่างเดียว แสดงมูลค่ารวม
                                                            str_GrossF = str_gross & vbNewLine & _
                                                                   "_______" & vbNewLine & Format(TGross, "#,##0.00##") & " " & TempUnit3

                                                    End Select
                                                Else
                                                    str_GrossF = str_gross
                                                End If
                                        End Select

                                    ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "GW" Then 'ระบบเก่า มีแต่ GW
                                        str_gross = GrossTxtDetail() '& vbNewLine & FobTxtAll()
                                        Select Case C_TotalRowDe.Text
                                            Case 1 'รายการแรก
                                                str_GrossF = str_gross
                                            Case Else 'รายการที่ไม่ใช่รายการที่ 1
                                                If Cpage = CountAllDetail Then 'check เงื่อนไขรายการสุดท้าย
                                                    Select Case Check_CaseRVCCount
                                                        Case 9 'มีอย่างอื่นปน ไม่แสดงมูลค่ารวม
                                                            str_GrossF = str_gross & vbNewLine & _
                                                                    "_______" & vbNewLine & Format(TGross, "#,##0.00##") & " " & TempUnit3

                                                        Case Else 'มี RVC อย่างเดียว แสดงมูลค่ารวม
                                                            str_GrossF = str_gross & vbNewLine & _
                                                                    "_______" & vbNewLine & Format(TGross, "#,##0.00##") & " " & TempUnit3

                                                    End Select
                                                Else
                                                    str_GrossF = str_gross
                                                End If
                                        End Select

                                    ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "" Then
                                        str_gross = GrossTxtDetail() '& vbNewLine & FobTxtAll()
                                        Select Case C_TotalRowDe.Text
                                            Case 1 'รายการแรก
                                                str_GrossF = str_gross
                                            Case Else 'รายการที่ไม่ใช่รายการที่ 1
                                                If Cpage = CountAllDetail Then 'check เงื่อนไขรายการสุดท้าย
                                                    Select Case Check_CaseRVCCount
                                                        Case 9 'มีอย่างอื่นปน ไม่แสดงมูลค่ารวม
                                                            str_GrossF = str_gross & vbNewLine & _
                                                                       "_______" & vbNewLine & Format(TGross, "#,##0.00##") & " " & TempUnit3

                                                        Case Else 'มี RVC อย่างเดียว แสดงมูลค่ารวม
                                                            str_GrossF = str_gross & vbNewLine & _
                                                                       "_______" & vbNewLine & Format(TGross, "#,##0.00##") & " " & TempUnit3
                                                    End Select
                                                Else
                                                    str_GrossF = str_gross
                                                End If
                                        End Select

                                    End If
                                ElseIf Mid(CommonUtility.Get_StringValue(txtFOBDisplay.Value), 1, 50) = "NoDisplay" Then
                                    If Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "NET WEIGHT" Then
                                        str_gross = NetWeight_(txtnet_weight.Value, txtunit_code2.Value) 'Mid(Tnet, 1, 9) & " " & Mid(txtunit_code2.Value, 1, 10)
                                        Select Case C_TotalRowDe.Text
                                            Case 1 'รายการแรก
                                                str_GrossF = str_gross
                                            Case Else 'รายการที่ไม่ใช่รายการที่ 1
                                                If Cpage = CountAllDetail Then 'check เงื่อนไขรายการสุดท้าย
                                                    Select Case Check_CaseRVCCount
                                                        Case 9 'มีอย่างอื่นปน ไม่แสดงมูลค่ารวม
                                                            str_GrossF = str_gross & vbNewLine & _
                                                                   "_______" & _
                                                                   vbNewLine & Format(TNet, "#,##0.00##") & " " & CommonUtility.Get_StringValue(txtunit_code2.Value)

                                                        Case Else 'มี RVC อย่างเดียว แสดงมูลค่ารวม
                                                            str_GrossF = str_gross & vbNewLine & _
                                                                    "_______" & _
                                                                    vbNewLine & Format(TNet, "#,##0.00##") & " " & CommonUtility.Get_StringValue(txtunit_code2.Value)

                                                    End Select
                                                Else
                                                    str_GrossF = str_gross
                                                End If
                                        End Select

                                    ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "GROSS WEIGHT - NET WEIGHT" Then
                                        str_gross = GrossTxtDetail() & vbNewLine & NetWeight_(txtnet_weight.Value, txtunit_code2.Value) 'Mid(Tnet, 1, 9) & " " & Mid(txtunit_code2.Value, 1, 10)
                                        Select Case C_TotalRowDe.Text
                                            Case 1 'รายการแรก
                                                str_GrossF = str_gross
                                            Case Else 'รายการที่ไม่ใช่รายการที่ 1
                                                If Cpage = CountAllDetail Then 'check เงื่อนไขรายการสุดท้าย
                                                    Select Case Check_CaseRVCCount
                                                        Case 9 'มีอย่างอื่นปน ไม่แสดงมูลค่ารวม
                                                            str_GrossF = str_gross & vbNewLine & _
                                                                    "_______" & vbNewLine & Format(TGross, "#,##0.00##") & " " & TempUnit3 & _
                                                                    vbNewLine & Format(TNet, "#,##0.00##") & " " & CommonUtility.Get_StringValue(txtunit_code2.Value)

                                                        Case Else 'มี RVC อย่างเดียว แสดงมูลค่ารวม
                                                            str_GrossF = str_gross & vbNewLine & _
                                                                       "_______" & vbNewLine & Format(TGross, "#,##0.00##") & " " & TempUnit3 & _
                                                                       vbNewLine & Format(TNet, "#,##0.00##") & " " & CommonUtility.Get_StringValue(txtunit_code2.Value)

                                                    End Select
                                                Else
                                                    str_GrossF = str_gross
                                                End If
                                        End Select

                                    ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "GROSS WEIGHT" Then
                                        str_gross = GrossTxtDetail()
                                        Select Case C_TotalRowDe.Text
                                            Case 1 'รายการแรก
                                                str_GrossF = str_gross
                                            Case Else 'รายการที่ไม่ใช่รายการที่ 1
                                                If Cpage = CountAllDetail Then 'check เงื่อนไขรายการสุดท้าย
                                                    Select Case Check_CaseRVCCount
                                                        Case 9 'มีอย่างอื่นปน ไม่แสดงมูลค่ารวม
                                                            str_GrossF = str_gross & vbNewLine & _
                                                                   "_______" & vbNewLine & Format(TGross, "#,##0.00##") & " " & TempUnit3

                                                        Case Else 'มี RVC อย่างเดียว แสดงมูลค่ารวม
                                                            str_GrossF = str_gross & vbNewLine & _
                                                                   "_______" & vbNewLine & Format(TGross, "#,##0.00##") & " " & TempUnit3

                                                    End Select
                                                Else
                                                    str_GrossF = str_gross
                                                End If
                                        End Select

                                    ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "" Then
                                        str_gross = GrossTxtDetail()
                                        Select Case C_TotalRowDe.Text
                                            Case 1 'รายการแรก
                                                str_GrossF = str_gross
                                            Case Else 'รายการที่ไม่ใช่รายการที่ 1
                                                If Cpage = CountAllDetail Then 'check เงื่อนไขรายการสุดท้าย
                                                    Select Case Check_CaseRVCCount
                                                        Case 9 'มีอย่างอื่นปน ไม่แสดงมูลค่ารวม
                                                            str_GrossF = str_gross & vbNewLine & _
                                                                   "_______" & vbNewLine & Format(TGross, "#,##0.00##") & " " & TempUnit3

                                                        Case Else 'มี RVC อย่างเดียว แสดงมูลค่ารวม
                                                            str_GrossF = str_gross & vbNewLine & _
                                                                   "_______" & vbNewLine & Format(TGross, "#,##0.00##") & " " & TempUnit3

                                                    End Select
                                                Else
                                                    str_GrossF = str_gross
                                                End If
                                        End Select

                                    End If

                                ElseIf Mid(CommonUtility.Get_StringValue(txtFOBDisplay.Value), 1, 50) = "" Then

                                    If Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "NET WEIGHT" Then
                                        str_gross = NetWeight_(txtnet_weight.Value, txtunit_code2.Value) 'Mid(Tnet, 1, 9) & " " & Mid(txtunit_code2.Value, 1, 10) '& vbNewLine & FobTxtAll()
                                        Select Case C_TotalRowDe.Text
                                            Case 1 'รายการแรก
                                                str_GrossF = str_gross
                                            Case Else 'รายการที่ไม่ใช่รายการที่ 1
                                                If Cpage = CountAllDetail Then 'check เงื่อนไขรายการสุดท้าย
                                                    Select Case Check_CaseRVCCount
                                                        Case 9 'มีอย่างอื่นปน ไม่แสดงมูลค่ารวม
                                                            str_GrossF = str_gross & vbNewLine & _
                                                                   "_______" & _
                                                                   vbNewLine & Format(TNet, "#,##0.00##") & " " & CommonUtility.Get_StringValue(txtunit_code2.Value)

                                                        Case Else 'มี RVC อย่างเดียว แสดงมูลค่ารวม
                                                            str_GrossF = str_gross & vbNewLine & _
                                                                   "_______" & _
                                                                   vbNewLine & Format(TNet, "#,##0.00##") & " " & CommonUtility.Get_StringValue(txtunit_code2.Value)

                                                    End Select
                                                Else
                                                    str_GrossF = str_gross
                                                End If
                                        End Select

                                    ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "GROSS WEIGHT - NET WEIGHT" Then
                                        str_gross = GrossTxtDetail() & vbNewLine & NetWeight_(txtnet_weight.Value, txtunit_code2.Value) 'Mid(Tnet, 1, 9) & " " & Mid(txtunit_code2.Value, 1, 10) '& vbNewLine & FobTxtAll()
                                        Select Case C_TotalRowDe.Text
                                            Case 1 'รายการแรก
                                                str_GrossF = str_gross
                                            Case Else 'รายการที่ไม่ใช่รายการที่ 1
                                                If Cpage = CountAllDetail Then 'check เงื่อนไขรายการสุดท้าย
                                                    Select Case Check_CaseRVCCount
                                                        Case 9 'มีอย่างอื่นปน ไม่แสดงมูลค่ารวม
                                                            str_GrossF = str_gross & vbNewLine & _
                                                                       "_______" & vbNewLine & Format(TGross, "#,##0.00##") & " " & TempUnit3 & _
                                                                       vbNewLine & Format(TNet, "#,##0.00##") & " " & CommonUtility.Get_StringValue(txtunit_code2.Value)

                                                        Case Else 'มี RVC อย่างเดียว แสดงมูลค่ารวม
                                                            str_GrossF = str_gross & vbNewLine & _
                                                                        "_______" & vbNewLine & Format(TGross, "#,##0.00##") & " " & TempUnit3 & _
                                                                        vbNewLine & Format(TNet, "#,##0.00##") & " " & CommonUtility.Get_StringValue(txtunit_code2.Value)

                                                    End Select
                                                Else
                                                    str_GrossF = str_gross
                                                End If
                                        End Select

                                    ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "GROSS WEIGHT" Then
                                        str_gross = GrossTxtDetail() '& vbNewLine & FobTxtAll()
                                        Select Case C_TotalRowDe.Text
                                            Case 1 'รายการแรก
                                                str_GrossF = str_gross
                                            Case Else 'รายการที่ไม่ใช่รายการที่ 1
                                                If Cpage = CountAllDetail Then 'check เงื่อนไขรายการสุดท้าย
                                                    Select Case Check_CaseRVCCount
                                                        Case 9 'มีอย่างอื่นปน ไม่แสดงมูลค่ารวม
                                                            str_GrossF = str_gross & vbNewLine & _
                                                                   "_______" & vbNewLine & Format(TGross, "#,##0.00##") & " " & TempUnit3

                                                        Case Else 'มี RVC อย่างเดียว แสดงมูลค่ารวม
                                                            str_GrossF = str_gross & vbNewLine & _
                                                                   "_______" & vbNewLine & Format(TGross, "#,##0.00##") & " " & TempUnit3

                                                    End Select
                                                Else
                                                    str_GrossF = str_gross
                                                End If
                                        End Select

                                    ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "" Then
                                        str_gross = GrossTxtDetail() '& vbNewLine & FobTxtAll()
                                        Select Case C_TotalRowDe.Text
                                            Case 1 'รายการแรก
                                                str_GrossF = str_gross
                                            Case Else 'รายการที่ไม่ใช่รายการที่ 1
                                                If Cpage = CountAllDetail Then 'check เงื่อนไขรายการสุดท้าย
                                                    Select Case Check_CaseRVCCount
                                                        Case 9 'มีอย่างอื่นปน ไม่แสดงมูลค่ารวม
                                                            str_GrossF = str_gross & vbNewLine & _
                                                                   "_______" & vbNewLine & Format(TGross, "#,##0.00##") & " " & TempUnit3

                                                        Case Else 'มี RVC อย่างเดียว แสดงมูลค่ารวม
                                                            str_GrossF = str_gross & vbNewLine & _
                                                                    "_______" & vbNewLine & Format(TGross, "#,##0.00##") & " " & TempUnit3

                                                    End Select
                                                Else
                                                    str_GrossF = str_gross
                                                End If
                                        End Select

                                    End If
                                End If
                        End Select
                    Case Else '[รายการแบบเดิม เพื่อให้เงื่อนไขเดิม print ได้]ไม่แยกรายการ Gross ส่วน third
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
                            ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "GW" Then 'ระบบเก่า มีแต่ GW
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
                End Select

                '---------------------
            Case Else 'ไม่ใช่ invoice ต่างประเทศ
                Select Case CheckDetailGross
                    Case "GWDetail" 'แยกรายการ ไม่ใช่ invoice ต่างประเทศ
                        Select Case Mid(CommonUtility.Get_StringValue(txtletter.Value), 1, 50)
                            Case "3" 'แสดงมูลค่า Detail ไม่ใช่ invoice ต่างประเทศ
                                If Mid(CommonUtility.Get_StringValue(txtFOBDisplay.Value), 1, 50) = "Display" Then
                                    If Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "NET WEIGHT" Then
                                        str_gross = NetWeight_(txtnet_weight.Value, txtunit_code2.Value) & vbNewLine & FobTxtAllSumNew() 'Mid(Tnet, 1, 9) & " " & Mid(txtunit_code2.Value, 1, 10) '& vbNewLine & FobTxtAll()
                                        Select Case C_TotalRowDe.Text
                                            Case 1 'รายการแรก
                                                str_GrossF = str_gross
                                            Case Else 'รายการที่ไม่ใช่รายการที่ 1
                                                If Cpage = CountAllDetail Then 'check เงื่อนไขรายการสุดท้าย
                                                    Select Case Check_CaseRVCCount
                                                        Case 9 'มีอย่างอื่นปน ไม่แสดงมูลค่ารวม
                                                            str_GrossF = str_gross & vbNewLine & _
                                                                       "_______" & _
                                                                       vbNewLine & Format(TNet, "#,##0.00##") & " " & CommonUtility.Get_StringValue(txtunit_code2.Value)

                                                        Case Else 'มี RVC อย่างเดียว แสดงมูลค่ารวม
                                                            str_GrossF = str_gross & vbNewLine & _
                                                                       "_______" & _
                                                                       vbNewLine & Format(TNet, "#,##0.00##") & " " & CommonUtility.Get_StringValue(txtunit_code2.Value) & _
                                                                       vbNewLine & FobTxtAll()

                                                    End Select
                                                Else
                                                    str_GrossF = str_gross
                                                End If
                                        End Select

                                    ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "GROSS WEIGHT - NET WEIGHT" Then
                                        str_gross = GrossTxtDetail() & vbNewLine & NetWeight_(txtnet_weight.Value, txtunit_code2.Value) & vbNewLine & FobTxtAllSumNew() 'Mid(Tnet, 1, 9) & " " & Mid(txtunit_code2.Value, 1, 10) '& vbNewLine & FobTxtAll()
                                        Select Case C_TotalRowDe.Text
                                            Case 1 'รายการแรก
                                                str_GrossF = str_gross
                                            Case Else 'รายการที่ไม่ใช่รายการที่ 1
                                                If Cpage = CountAllDetail Then 'check เงื่อนไขรายการสุดท้าย
                                                    Select Case Check_CaseRVCCount
                                                        Case 9 'มีอย่างอื่นปน ไม่แสดงมูลค่ารวม
                                                            str_GrossF = str_gross & vbNewLine & _
                                                                       "_______" & vbNewLine & Format(TGross, "#,##0.00##") & " " & TempUnit3 & _
                                                                       vbNewLine & Format(TNet, "#,##0.00##") & " " & CommonUtility.Get_StringValue(txtunit_code2.Value)

                                                        Case Else 'มี RVC อย่างเดียว แสดงมูลค่ารวม
                                                            str_GrossF = str_gross & vbNewLine & _
                                                                       "_______" & vbNewLine & Format(TGross, "#,##0.00##") & " " & TempUnit3 & _
                                                                       vbNewLine & FobTxtAll() & _
                                                                       vbNewLine & Format(TNet, "#,##0.00##") & " " & CommonUtility.Get_StringValue(txtunit_code2.Value)


                                                    End Select
                                                Else
                                                    str_GrossF = str_gross
                                                End If
                                        End Select

                                    ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "GROSS WEIGHT" Then
                                        str_gross = GrossTxtDetail() & vbNewLine & FobTxtAllSumNew()
                                        Select Case C_TotalRowDe.Text
                                            Case 1 'รายการแรก
                                                str_GrossF = str_gross
                                            Case Else 'รายการที่ไม่ใช่รายการที่ 1
                                                If Cpage = CountAllDetail Then 'check เงื่อนไขรายการสุดท้าย
                                                    Select Case Check_CaseRVCCount
                                                        Case 9 'มีอย่างอื่นปน ไม่แสดงมูลค่ารวม
                                                            str_GrossF = str_gross & vbNewLine & _
                                                                        "_______" & vbNewLine & Format(TGross, "#,##0.00##") & " " & TempUnit3

                                                        Case Else 'มี RVC อย่างเดียว แสดงมูลค่ารวม
                                                            str_GrossF = str_gross & vbNewLine & _
                                                                       "_______" & vbNewLine & Format(TGross, "#,##0.00##") & " " & TempUnit3 & _
                                                                       vbNewLine & FobTxtAll()

                                                    End Select
                                                Else
                                                    str_GrossF = str_gross
                                                End If
                                        End Select

                                    ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "GW" Then 'ระบบเก่า
                                        str_gross = GrossTxtDetail() & vbNewLine & FobTxtAllSumNew()
                                        Select Case C_TotalRowDe.Text
                                            Case 1 'รายการแรก
                                                str_GrossF = str_gross
                                            Case Else 'รายการที่ไม่ใช่รายการที่ 1
                                                If Cpage = CountAllDetail Then 'check เงื่อนไขรายการสุดท้าย
                                                    Select Case Check_CaseRVCCount
                                                        Case 9 'มีอย่างอื่นปน ไม่แสดงมูลค่ารวม
                                                            str_GrossF = str_gross & vbNewLine & _
                                                                       "_______" & vbNewLine & Format(TGross, "#,##0.00##") & " " & TempUnit3

                                                        Case Else 'มี RVC อย่างเดียว แสดงมูลค่ารวม
                                                            str_GrossF = str_gross & vbNewLine & _
                                                                       "_______" & vbNewLine & Format(TGross, "#,##0.00##") & " " & TempUnit3 & _
                                                                       vbNewLine & FobTxtAll()

                                                    End Select
                                                Else
                                                    str_GrossF = str_gross
                                                End If
                                        End Select

                                    ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "" Then
                                        str_gross = GrossTxtDetail() & vbNewLine & FobTxtAllSumNew()
                                        Select Case C_TotalRowDe.Text
                                            Case 1 'รายการแรก
                                                str_GrossF = str_gross
                                            Case Else 'รายการที่ไม่ใช่รายการที่ 1
                                                If Cpage = CountAllDetail Then 'check เงื่อนไขรายการสุดท้าย
                                                    Select Case Check_CaseRVCCount
                                                        Case 9 'มีอย่างอื่นปน ไม่แสดงมูลค่ารวม
                                                            str_GrossF = str_gross & vbNewLine & _
                                                                       "_______" & vbNewLine & Format(TGross, "#,##0.00##") & " " & TempUnit3

                                                        Case Else 'มี RVC อย่างเดียว แสดงมูลค่ารวม
                                                            str_GrossF = str_gross & vbNewLine & _
                                                                       "_______" & vbNewLine & Format(TGross, "#,##0.00##") & " " & TempUnit3 & _
                                                                       vbNewLine & FobTxtAll()

                                                    End Select
                                                Else
                                                    str_GrossF = str_gross
                                                End If
                                        End Select
                                    End If
                                ElseIf Mid(CommonUtility.Get_StringValue(txtFOBDisplay.Value), 1, 50) = "NoDisplay" Then
                                    If Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "NET WEIGHT" Then
                                        str_gross = NetWeight_(txtnet_weight.Value, txtunit_code2.Value) 'Mid(Tnet, 1, 9) & " " & Mid(txtunit_code2.Value, 1, 10)
                                        Select Case C_TotalRowDe.Text
                                            Case 1 'รายการแรก
                                                str_GrossF = str_gross
                                            Case Else 'รายการที่ไม่ใช่รายการที่ 1
                                                If Cpage = CountAllDetail Then 'check เงื่อนไขรายการสุดท้าย
                                                    Select Case Check_CaseRVCCount
                                                        Case 9 'มีอย่างอื่นปน ไม่แสดงมูลค่ารวม
                                                            str_GrossF = str_gross & vbNewLine & _
                                                                       "_______" & _
                                                                       vbNewLine & Format(TNet, "#,##0.00##") & " " & CommonUtility.Get_StringValue(txtunit_code2.Value)

                                                        Case Else 'มี RVC อย่างเดียว แสดงมูลค่ารวม
                                                            str_GrossF = str_gross & vbNewLine & _
                                                                       "_______" & _
                                                                       vbNewLine & Format(TNet, "#,##0.00##") & " " & CommonUtility.Get_StringValue(txtunit_code2.Value)

                                                    End Select
                                                Else
                                                    str_GrossF = str_gross
                                                End If
                                        End Select

                                    ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "GROSS WEIGHT - NET WEIGHT" Then
                                        str_gross = GrossTxtDetail() & vbNewLine & NetWeight_(txtnet_weight.Value, txtunit_code2.Value) 'Mid(Tnet, 1, 9) & " " & Mid(txtunit_code2.Value, 1, 10)
                                        Select Case C_TotalRowDe.Text
                                            Case 1 'รายการแรก
                                                str_GrossF = str_gross
                                            Case Else 'รายการที่ไม่ใช่รายการที่ 1
                                                If Cpage = CountAllDetail Then 'check เงื่อนไขรายการสุดท้าย
                                                    Select Case Check_CaseRVCCount
                                                        Case 9 'มีอย่างอื่นปน ไม่แสดงมูลค่ารวม
                                                            str_GrossF = str_gross & vbNewLine & _
                                                                       "_______" & vbNewLine & Format(TGross, "#,##0.00##") & " " & TempUnit3 & _
                                                                       vbNewLine & Format(TNet, "#,##0.00##") & " " & CommonUtility.Get_StringValue(txtunit_code2.Value)

                                                        Case Else 'มี RVC อย่างเดียว แสดงมูลค่ารวม
                                                            str_GrossF = str_gross & vbNewLine & _
                                                                       "_______" & vbNewLine & Format(TGross, "#,##0.00##") & " " & TempUnit3 & _
                                                                       vbNewLine & Format(TNet, "#,##0.00##") & " " & CommonUtility.Get_StringValue(txtunit_code2.Value)

                                                    End Select
                                                Else
                                                    str_GrossF = str_gross
                                                End If
                                        End Select

                                    ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "GROSS WEIGHT" Then
                                        str_gross = GrossTxtDetail()
                                        Select Case C_TotalRowDe.Text
                                            Case 1 'รายการแรก
                                                str_GrossF = str_gross
                                            Case Else 'รายการที่ไม่ใช่รายการที่ 1
                                                If Cpage = CountAllDetail Then 'check เงื่อนไขรายการสุดท้าย
                                                    Select Case Check_CaseRVCCount
                                                        Case 9 'มีอย่างอื่นปน ไม่แสดงมูลค่ารวม
                                                            str_GrossF = str_gross & vbNewLine & _
                                                                       "_______" & vbNewLine & Format(TGross, "#,##0.00##") & " " & TempUnit3

                                                        Case Else 'มี RVC อย่างเดียว แสดงมูลค่ารวม
                                                            str_GrossF = str_gross & vbNewLine & _
                                                                       "_______" & vbNewLine & Format(TGross, "#,##0.00##") & " " & TempUnit3

                                                    End Select
                                                Else
                                                    str_GrossF = str_gross
                                                End If
                                        End Select

                                    ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "" Then
                                        str_gross = GrossTxtDetail()
                                        Select Case C_TotalRowDe.Text
                                            Case 1 'รายการแรก
                                                str_GrossF = str_gross
                                            Case Else 'รายการที่ไม่ใช่รายการที่ 1
                                                If Cpage = CountAllDetail Then 'check เงื่อนไขรายการสุดท้าย
                                                    Select Case Check_CaseRVCCount
                                                        Case 9 'มีอย่างอื่นปน ไม่แสดงมูลค่ารวม
                                                            str_GrossF = str_gross & vbNewLine & _
                                                                       "_______" & vbNewLine & Format(TGross, "#,##0.00##") & " " & TempUnit3

                                                        Case Else 'มี RVC อย่างเดียว แสดงมูลค่ารวม
                                                            str_GrossF = str_gross & vbNewLine & _
                                                                       "_______" & vbNewLine & Format(TGross, "#,##0.00##") & " " & TempUnit3

                                                    End Select
                                                Else
                                                    str_GrossF = str_gross
                                                End If
                                        End Select
                                    End If

                                ElseIf Mid(CommonUtility.Get_StringValue(txtFOBDisplay.Value), 1, 50) = "" Then

                                    If Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "NET WEIGHT" Then
                                        str_gross = NetWeight_(txtnet_weight.Value, txtunit_code2.Value) 'Mid(Tnet, 1, 9) & " " & Mid(txtunit_code2.Value, 1, 10) '& vbNewLine & FobTxtAll()
                                        Select Case C_TotalRowDe.Text
                                            Case 1 'รายการแรก
                                                str_GrossF = str_gross
                                            Case Else 'รายการที่ไม่ใช่รายการที่ 1
                                                If Cpage = CountAllDetail Then 'check เงื่อนไขรายการสุดท้าย
                                                    Select Case Check_CaseRVCCount
                                                        Case 9 'มีอย่างอื่นปน ไม่แสดงมูลค่ารวม
                                                            str_GrossF = str_gross & vbNewLine & _
                                                                       "_______" & _
                                                                       vbNewLine & Format(TNet, "#,##0.00##") & " " & CommonUtility.Get_StringValue(txtunit_code2.Value)

                                                        Case Else 'มี RVC อย่างเดียว แสดงมูลค่ารวม
                                                            str_GrossF = str_gross & vbNewLine & _
                                                                       "_______" & _
                                                                       vbNewLine & Format(TNet, "#,##0.00##") & " " & CommonUtility.Get_StringValue(txtunit_code2.Value)

                                                    End Select
                                                Else
                                                    str_GrossF = str_gross
                                                End If
                                        End Select

                                    ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "GROSS WEIGHT - NET WEIGHT" Then
                                        str_gross = GrossTxtDetail() & vbNewLine & NetWeight_(txtnet_weight.Value, txtunit_code2.Value) 'Mid(Tnet, 1, 9) & " " & Mid(txtunit_code2.Value, 1, 10) '& vbNewLine & FobTxtAll()
                                        Select Case C_TotalRowDe.Text
                                            Case 1 'รายการแรก
                                                str_GrossF = str_gross
                                            Case Else 'รายการที่ไม่ใช่รายการที่ 1
                                                If Cpage = CountAllDetail Then 'check เงื่อนไขรายการสุดท้าย
                                                    Select Case Check_CaseRVCCount
                                                        Case 9 'มีอย่างอื่นปน ไม่แสดงมูลค่ารวม
                                                            str_GrossF = str_gross & vbNewLine & _
                                                                       "_______" & vbNewLine & Format(TGross, "#,##0.00##") & " " & TempUnit3 & _
                                                                       vbNewLine & Format(TNet, "#,##0.00##") & " " & CommonUtility.Get_StringValue(txtunit_code2.Value)

                                                        Case Else 'มี RVC อย่างเดียว แสดงมูลค่ารวม
                                                            str_GrossF = str_gross & vbNewLine & _
                                                                       "_______" & vbNewLine & Format(TGross, "#,##0.00##") & " " & TempUnit3 & _
                                                                       vbNewLine & Format(TNet, "#,##0.00##") & " " & CommonUtility.Get_StringValue(txtunit_code2.Value)

                                                    End Select
                                                Else
                                                    str_GrossF = str_gross
                                                End If
                                        End Select

                                    ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "GROSS WEIGHT" Then
                                        str_gross = GrossTxtDetail()
                                        Select Case C_TotalRowDe.Text
                                            Case 1 'รายการแรก
                                                str_GrossF = str_gross
                                            Case Else 'รายการที่ไม่ใช่รายการที่ 1
                                                If Cpage = CountAllDetail Then 'check เงื่อนไขรายการสุดท้าย
                                                    Select Case Check_CaseRVCCount
                                                        Case 9 'มีอย่างอื่นปน ไม่แสดงมูลค่ารวม
                                                            str_GrossF = str_gross & vbNewLine & _
                                                                       "_______" & vbNewLine & Format(TGross, "#,##0.00##") & " " & TempUnit3

                                                        Case Else 'มี RVC อย่างเดียว แสดงมูลค่ารวม
                                                            str_GrossF = str_gross & vbNewLine & _
                                                                       "_______" & vbNewLine & Format(TGross, "#,##0.00##") & " " & TempUnit3

                                                    End Select
                                                Else
                                                    str_GrossF = str_gross
                                                End If
                                        End Select

                                    ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "" Then
                                        str_gross = GrossTxtDetail()
                                        Select Case C_TotalRowDe.Text
                                            Case 1 'รายการแรก
                                                str_GrossF = str_gross
                                            Case Else 'รายการที่ไม่ใช่รายการที่ 1
                                                If Cpage = CountAllDetail Then 'check เงื่อนไขรายการสุดท้าย
                                                    Select Case Check_CaseRVCCount
                                                        Case 9 'มีอย่างอื่นปน ไม่แสดงมูลค่ารวม
                                                            str_GrossF = str_gross & vbNewLine & _
                                                                       "_______" & vbNewLine & Format(TGross, "#,##0.00##") & " " & TempUnit3

                                                        Case Else 'มี RVC อย่างเดียว แสดงมูลค่ารวม
                                                            str_GrossF = str_gross & vbNewLine & _
                                                                       "_______" & vbNewLine & Format(TGross, "#,##0.00##") & " " & TempUnit3

                                                    End Select
                                                Else
                                                    str_GrossF = str_gross
                                                End If
                                        End Select
                                    End If
                                End If
                            Case Else ' ไม่แสดงมูลค่า Detail ไม่ใช่ invoice ต่างประเทศ check letter ไม่เท่า 3 ไม่ต้องแสดงมูลค่าเลย
                                If Mid(CommonUtility.Get_StringValue(txtFOBDisplay.Value), 1, 50) = "Display" Then

                                    If Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "NET WEIGHT" Then
                                        str_gross = NetWeight_(txtnet_weight.Value, txtunit_code2.Value) '& vbNewLine & FobTxtAllSumNew() 'Mid(Tnet, 1, 9) & " " & Mid(txtunit_code2.Value, 1, 10) '& vbNewLine & FobTxtAll()
                                        Select Case C_TotalRowDe.Text
                                            Case 1 'รายการแรก
                                                str_GrossF = str_gross
                                            Case Else 'รายการที่ไม่ใช่รายการที่ 1
                                                If Cpage = CountAllDetail Then 'check เงื่อนไขรายการสุดท้าย
                                                    Select Case Check_CaseRVCCount
                                                        Case 9 'มีอย่างอื่นปน ไม่แสดงมูลค่ารวม
                                                            str_GrossF = str_gross & vbNewLine & _
                                                                       "_______" & _
                                                                       vbNewLine & Format(TNet, "#,##0.00##") & " " & CommonUtility.Get_StringValue(txtunit_code2.Value)

                                                        Case Else 'มี RVC อย่างเดียว แสดงมูลค่ารวม
                                                            str_GrossF = str_gross & vbNewLine & _
                                                                        "_______" & _
                                                                        vbNewLine & Format(TNet, "#,##0.00##") & " " & CommonUtility.Get_StringValue(txtunit_code2.Value)

                                                    End Select
                                                Else
                                                    str_GrossF = str_gross
                                                End If
                                        End Select

                                    ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "GROSS WEIGHT - NET WEIGHT" Then
                                        str_gross = GrossTxtDetail() & vbNewLine & NetWeight_(txtnet_weight.Value, txtunit_code2.Value) '& vbNewLine & FobTxtAllSumNew() 'Mid(Tnet, 1, 9) & " " & Mid(txtunit_code2.Value, 1, 10) '& vbNewLine & FobTxtAll()
                                        Select Case C_TotalRowDe.Text
                                            Case 1 'รายการแรก
                                                str_GrossF = str_gross
                                            Case Else 'รายการที่ไม่ใช่รายการที่ 1
                                                If Cpage = CountAllDetail Then 'check เงื่อนไขรายการสุดท้าย
                                                    Select Case Check_CaseRVCCount
                                                        Case 9 'มีอย่างอื่นปน ไม่แสดงมูลค่ารวม
                                                            str_GrossF = str_gross & vbNewLine & _
                                                                       "_______" & vbNewLine & Format(TGross, "#,##0.00##") & " " & TempUnit3 & _
                                                                       vbNewLine & Format(TNet, "#,##0.00##") & " " & CommonUtility.Get_StringValue(txtunit_code2.Value)

                                                        Case Else 'มี RVC อย่างเดียว แสดงมูลค่ารวม
                                                            str_GrossF = str_gross & vbNewLine & _
                                                                       "_______" & vbNewLine & Format(TGross, "#,##0.00##") & " " & TempUnit3 & _
                                                                       vbNewLine & Format(TNet, "#,##0.00##") & " " & CommonUtility.Get_StringValue(txtunit_code2.Value)

                                                    End Select
                                                Else
                                                    str_GrossF = str_gross
                                                End If
                                        End Select

                                    ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "GROSS WEIGHT" Then
                                        str_gross = GrossTxtDetail() '& vbNewLine & FobTxtAllSumNew()
                                        Select Case C_TotalRowDe.Text
                                            Case 1 'รายการแรก
                                                str_GrossF = str_gross
                                            Case Else 'รายการที่ไม่ใช่รายการที่ 1
                                                If Cpage = CountAllDetail Then 'check เงื่อนไขรายการสุดท้าย
                                                    Select Case Check_CaseRVCCount
                                                        Case 9 'มีอย่างอื่นปน ไม่แสดงมูลค่ารวม
                                                            str_GrossF = str_gross & vbNewLine & _
                                                                       "_______" & vbNewLine & Format(TGross, "#,##0.00##") & " " & TempUnit3

                                                        Case Else 'มี RVC อย่างเดียว แสดงมูลค่ารวม
                                                            str_GrossF = str_gross & vbNewLine & _
                                                                       "_______" & vbNewLine & Format(TGross, "#,##0.00##") & " " & TempUnit3

                                                    End Select
                                                Else
                                                    str_GrossF = str_gross
                                                End If
                                        End Select

                                    ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "GW" Then 'ระบบเก่า
                                        str_gross = GrossTxtDetail() '& vbNewLine & FobTxtAllSumNew()
                                        Select Case C_TotalRowDe.Text
                                            Case 1 'รายการแรก
                                                str_GrossF = str_gross
                                            Case Else 'รายการที่ไม่ใช่รายการที่ 1
                                                If Cpage = CountAllDetail Then 'check เงื่อนไขรายการสุดท้าย
                                                    Select Case Check_CaseRVCCount
                                                        Case 9 'มีอย่างอื่นปน ไม่แสดงมูลค่ารวม
                                                            str_GrossF = str_gross & vbNewLine & _
                                                                       "_______" & vbNewLine & Format(TGross, "#,##0.00##") & " " & TempUnit3

                                                        Case Else 'มี RVC อย่างเดียว แสดงมูลค่ารวม
                                                            str_GrossF = str_gross & vbNewLine & _
                                                                       "_______" & vbNewLine & Format(TGross, "#,##0.00##") & " " & TempUnit3

                                                    End Select
                                                Else
                                                    str_GrossF = str_gross
                                                End If
                                        End Select

                                    ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "" Then
                                        str_gross = GrossTxtDetail() '& vbNewLine & FobTxtAllSumNew()
                                        Select Case C_TotalRowDe.Text
                                            Case 1 'รายการแรก
                                                str_GrossF = str_gross
                                            Case Else 'รายการที่ไม่ใช่รายการที่ 1
                                                If Cpage = CountAllDetail Then 'check เงื่อนไขรายการสุดท้าย
                                                    Select Case Check_CaseRVCCount
                                                        Case 9 'มีอย่างอื่นปน ไม่แสดงมูลค่ารวม
                                                            str_GrossF = str_gross & vbNewLine & _
                                                                       "_______" & vbNewLine & Format(TGross, "#,##0.00##") & " " & TempUnit3

                                                        Case Else 'มี RVC อย่างเดียว แสดงมูลค่ารวม
                                                            str_GrossF = str_gross & vbNewLine & _
                                                                       "_______" & vbNewLine & Format(TGross, "#,##0.00##") & " " & TempUnit3

                                                    End Select
                                                Else
                                                    str_GrossF = str_gross
                                                End If
                                        End Select
                                    End If
                                ElseIf Mid(CommonUtility.Get_StringValue(txtFOBDisplay.Value), 1, 50) = "NoDisplay" Then
                                    If Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "NET WEIGHT" Then
                                        str_gross = NetWeight_(txtnet_weight.Value, txtunit_code2.Value) 'Mid(Tnet, 1, 9) & " " & Mid(txtunit_code2.Value, 1, 10)
                                        Select Case C_TotalRowDe.Text
                                            Case 1 'รายการแรก
                                                str_GrossF = str_gross
                                            Case Else 'รายการที่ไม่ใช่รายการที่ 1
                                                If Cpage = CountAllDetail Then 'check เงื่อนไขรายการสุดท้าย
                                                    Select Case Check_CaseRVCCount
                                                        Case 9 'มีอย่างอื่นปน ไม่แสดงมูลค่ารวม
                                                            str_GrossF = str_gross & vbNewLine & _
                                                                       "_______" & _
                                                                       vbNewLine & Format(TNet, "#,##0.00##") & " " & CommonUtility.Get_StringValue(txtunit_code2.Value)

                                                        Case Else 'มี RVC อย่างเดียว แสดงมูลค่ารวม
                                                            str_GrossF = str_gross & vbNewLine & _
                                                                       "_______" & _
                                                                       vbNewLine & Format(TNet, "#,##0.00##") & " " & CommonUtility.Get_StringValue(txtunit_code2.Value)

                                                    End Select
                                                Else
                                                    str_GrossF = str_gross
                                                End If
                                        End Select

                                    ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "GROSS WEIGHT - NET WEIGHT" Then
                                        str_gross = GrossTxtDetail() & vbNewLine & NetWeight_(txtnet_weight.Value, txtunit_code2.Value) 'Mid(Tnet, 1, 9) & " " & Mid(txtunit_code2.Value, 1, 10)
                                        Select Case C_TotalRowDe.Text
                                            Case 1 'รายการแรก
                                                str_GrossF = str_gross
                                            Case Else 'รายการที่ไม่ใช่รายการที่ 1
                                                If Cpage = CountAllDetail Then 'check เงื่อนไขรายการสุดท้าย
                                                    Select Case Check_CaseRVCCount
                                                        Case 9 'มีอย่างอื่นปน ไม่แสดงมูลค่ารวม
                                                            str_GrossF = str_gross & vbNewLine & _
                                                                       "_______" & vbNewLine & Format(TGross, "#,##0.00##") & " " & TempUnit3 & _
                                                                       vbNewLine & Format(TNet, "#,##0.00##") & " " & CommonUtility.Get_StringValue(txtunit_code2.Value)

                                                        Case Else 'มี RVC อย่างเดียว แสดงมูลค่ารวม
                                                            str_GrossF = str_gross & vbNewLine & _
                                                                       "_______" & vbNewLine & Format(TGross, "#,##0.00##") & " " & TempUnit3 & _
                                                                       vbNewLine & Format(TNet, "#,##0.00##") & " " & CommonUtility.Get_StringValue(txtunit_code2.Value)

                                                    End Select
                                                Else
                                                    str_GrossF = str_gross
                                                End If
                                        End Select

                                    ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "GROSS WEIGHT" Then
                                        str_gross = GrossTxtDetail()
                                        Select Case C_TotalRowDe.Text
                                            Case 1 'รายการแรก
                                                str_GrossF = str_gross
                                            Case Else 'รายการที่ไม่ใช่รายการที่ 1
                                                If Cpage = CountAllDetail Then 'check เงื่อนไขรายการสุดท้าย
                                                    Select Case Check_CaseRVCCount
                                                        Case 9 'มีอย่างอื่นปน ไม่แสดงมูลค่ารวม
                                                            str_GrossF = str_gross & vbNewLine & _
                                                                       "_______" & vbNewLine & Format(TGross, "#,##0.00##") & " " & TempUnit3

                                                        Case Else 'มี RVC อย่างเดียว แสดงมูลค่ารวม
                                                            str_GrossF = str_gross & vbNewLine & _
                                                                       "_______" & vbNewLine & Format(TGross, "#,##0.00##") & " " & TempUnit3

                                                    End Select
                                                Else
                                                    str_GrossF = str_gross
                                                End If
                                        End Select

                                    ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "" Then
                                        str_gross = GrossTxtDetail()
                                        Select Case C_TotalRowDe.Text
                                            Case 1 'รายการแรก
                                                str_GrossF = str_gross
                                            Case Else 'รายการที่ไม่ใช่รายการที่ 1
                                                If Cpage = CountAllDetail Then 'check เงื่อนไขรายการสุดท้าย
                                                    Select Case Check_CaseRVCCount
                                                        Case 9 'มีอย่างอื่นปน ไม่แสดงมูลค่ารวม
                                                            str_GrossF = str_gross & vbNewLine & _
                                                                       "_______" & vbNewLine & Format(TGross, "#,##0.00##") & " " & TempUnit3

                                                        Case Else 'มี RVC อย่างเดียว แสดงมูลค่ารวม
                                                            str_GrossF = str_gross & vbNewLine & _
                                                                       "_______" & vbNewLine & Format(TGross, "#,##0.00##") & " " & TempUnit3

                                                    End Select
                                                Else
                                                    str_GrossF = str_gross
                                                End If
                                        End Select
                                    End If

                                ElseIf Mid(CommonUtility.Get_StringValue(txtFOBDisplay.Value), 1, 50) = "" Then

                                    If Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "NET WEIGHT" Then
                                        str_gross = NetWeight_(txtnet_weight.Value, txtunit_code2.Value) 'Mid(Tnet, 1, 9) & " " & Mid(txtunit_code2.Value, 1, 10) '& vbNewLine & FobTxtAll()
                                        Select Case C_TotalRowDe.Text
                                            Case 1 'รายการแรก
                                                str_GrossF = str_gross
                                            Case Else 'รายการที่ไม่ใช่รายการที่ 1
                                                If Cpage = CountAllDetail Then 'check เงื่อนไขรายการสุดท้าย
                                                    Select Case Check_CaseRVCCount
                                                        Case 9 'มีอย่างอื่นปน ไม่แสดงมูลค่ารวม
                                                            str_GrossF = str_gross & vbNewLine & _
                                                                       "_______" & _
                                                                       vbNewLine & Format(TNet, "#,##0.00##") & " " & CommonUtility.Get_StringValue(txtunit_code2.Value)

                                                        Case Else 'มี RVC อย่างเดียว แสดงมูลค่ารวม
                                                            str_GrossF = str_gross & vbNewLine & _
                                                                       "_______" & _
                                                                       vbNewLine & Format(TNet, "#,##0.00##") & " " & CommonUtility.Get_StringValue(txtunit_code2.Value)

                                                    End Select
                                                Else
                                                    str_GrossF = str_gross
                                                End If
                                        End Select

                                    ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "GROSS WEIGHT - NET WEIGHT" Then
                                        str_gross = GrossTxtDetail() & vbNewLine & NetWeight_(txtnet_weight.Value, txtunit_code2.Value) 'Mid(Tnet, 1, 9) & " " & Mid(txtunit_code2.Value, 1, 10) '& vbNewLine & FobTxtAll()
                                        Select Case C_TotalRowDe.Text
                                            Case 1 'รายการแรก
                                                str_GrossF = str_gross
                                            Case Else 'รายการที่ไม่ใช่รายการที่ 1
                                                If Cpage = CountAllDetail Then 'check เงื่อนไขรายการสุดท้าย
                                                    Select Case Check_CaseRVCCount
                                                        Case 9 'มีอย่างอื่นปน ไม่แสดงมูลค่ารวม
                                                            str_GrossF = str_gross & vbNewLine & _
                                                                       "_______" & vbNewLine & Format(TGross, "#,##0.00##") & " " & TempUnit3 & _
                                                                       vbNewLine & Format(TNet, "#,##0.00##") & " " & CommonUtility.Get_StringValue(txtunit_code2.Value)

                                                        Case Else 'มี RVC อย่างเดียว แสดงมูลค่ารวม
                                                            str_GrossF = str_gross & vbNewLine & _
                                                                       "_______" & vbNewLine & Format(TGross, "#,##0.00##") & " " & TempUnit3 & _
                                                                       vbNewLine & Format(TNet, "#,##0.00##") & " " & CommonUtility.Get_StringValue(txtunit_code2.Value)

                                                    End Select
                                                Else
                                                    str_GrossF = str_gross
                                                End If
                                        End Select

                                    ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "GROSS WEIGHT" Then
                                        str_gross = GrossTxtDetail()
                                        Select Case C_TotalRowDe.Text
                                            Case 1 'รายการแรก
                                                str_GrossF = str_gross
                                            Case Else 'รายการที่ไม่ใช่รายการที่ 1
                                                If Cpage = CountAllDetail Then 'check เงื่อนไขรายการสุดท้าย
                                                    Select Case Check_CaseRVCCount
                                                        Case 9 'มีอย่างอื่นปน ไม่แสดงมูลค่ารวม
                                                            str_GrossF = str_gross & vbNewLine & _
                                                                       "_______" & vbNewLine & Format(TGross, "#,##0.00##") & " " & TempUnit3

                                                        Case Else 'มี RVC อย่างเดียว แสดงมูลค่ารวม
                                                            str_GrossF = str_gross & vbNewLine & _
                                                                       "_______" & vbNewLine & Format(TGross, "#,##0.00##") & " " & TempUnit3

                                                    End Select
                                                Else
                                                    str_GrossF = str_gross
                                                End If
                                        End Select

                                    ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "" Then
                                        str_gross = GrossTxtDetail()
                                        Select Case C_TotalRowDe.Text
                                            Case 1 'รายการแรก
                                                str_GrossF = str_gross
                                            Case Else 'รายการที่ไม่ใช่รายการที่ 1
                                                If Cpage = CountAllDetail Then 'check เงื่อนไขรายการสุดท้าย
                                                    Select Case Check_CaseRVCCount
                                                        Case 9 'มีอย่างอื่นปน ไม่แสดงมูลค่ารวม
                                                            str_GrossF = str_gross & vbNewLine & _
                                                                       "_______" & vbNewLine & Format(TGross, "#,##0.00##") & " " & TempUnit3

                                                        Case Else 'มี RVC อย่างเดียว แสดงมูลค่ารวม
                                                            str_GrossF = str_gross & vbNewLine & _
                                                                       "_______" & vbNewLine & Format(TGross, "#,##0.00##") & " " & TempUnit3

                                                    End Select
                                                Else
                                                    str_GrossF = str_gross
                                                End If
                                        End Select
                                    End If
                                End If
                        End Select
                    Case Else '[รายการแบบเดิม เพื่อให้เงื่อนไขเดิม print ได้]ไม่แยกรายการ Gross ไม่ใช่ invoice ต่างประเทศ
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
                            ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "GW" Then 'ระบบเก่า
                                str_gross = GrossTxt() & vbNewLine & FobTxtAllSum()
                                str_GrossF = str_gross
                            ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "" Then
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
                                str_gross = GrossTxt()
                                str_GrossF = str_gross
                            ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "" Then
                                str_gross = GrossTxt()
                                str_GrossF = str_gross
                            End If
                        End If
                End Select

        End Select
        Return str_GrossF
    End Function

    'Function CHeck_DisPlayF() As String
    '    Dim str_GrossF As String

    '    Dim sss As String = txtgross_weight.Text

    '    Dim str_gross As String

    '    'str_gross = txtgross_weight.Text

    '    'เรื่อง invoice ต่างประเทศ
    '    Dim str_NumInvoice As String = CommonUtility.Get_StringValue(txtNumInvoice.Text)
    '    'เรื่อง มูลค่า ต่างประเทศ
    '    Dim str_USDInvoice As String = CommonUtility.Get_StringValue(txtUSDInvoice.Text)

    '    Select Case txtshow_check.Text
    '        Case "100" ' เรื่อง invoice ต่างประเทศ
    '            If Mid(CommonUtility.Get_StringValue(txtFOBDisplay.Value), 1, 50) = "Display" Then

    '                If Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "NET WEIGHT" Then

    '                    str_gross = NetWeight_(TNet, txtunit_code2.Value) 'Mid(Tnet, 1, 9) & " " & Mid(txtunit_code2.Value, 1, 10) '& vbNewLine & FobTxtAll()
    '                    str_GrossF = str_gross & vbNewLine & str_USDInvoice

    '                ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "GROSS WEIGHT - NET WEIGHT" Then
    '                    str_gross = GrossTxt() & vbNewLine & NetWeight_(TNet, txtunit_code2.Value) 'Mid(Tnet, 1, 9) & " " & Mid(txtunit_code2.Value, 1, 10) '& vbNewLine & FobTxtAll()
    '                    str_GrossF = str_gross & vbNewLine & str_USDInvoice
    '                ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "GROSS WEIGHT" Then
    '                    str_gross = GrossTxt() '& vbNewLine & FobTxtAll()
    '                    str_GrossF = str_gross & vbNewLine & str_USDInvoice
    '                ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "" Then
    '                    str_gross = GrossTxt() '& vbNewLine & FobTxtAll()
    '                    str_GrossF = str_gross & vbNewLine & str_USDInvoice
    '                End If
    '            ElseIf Mid(CommonUtility.Get_StringValue(txtFOBDisplay.Value), 1, 50) = "NoDisplay" Then
    '                If Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "NET WEIGHT" Then
    '                    str_gross = NetWeight_(TNet, txtunit_code2.Value) 'Mid(Tnet, 1, 9) & " " & Mid(txtunit_code2.Value, 1, 10)
    '                    str_GrossF = str_gross & vbNewLine & str_USDInvoice
    '                ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "GROSS WEIGHT - NET WEIGHT" Then
    '                    str_gross = GrossTxt() & vbNewLine & NetWeight_(TNet, txtunit_code2.Value) 'Mid(Tnet, 1, 9) & " " & Mid(txtunit_code2.Value, 1, 10)
    '                    str_GrossF = str_gross & vbNewLine & str_USDInvoice
    '                ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "GROSS WEIGHT" Then
    '                    str_gross = GrossTxt()
    '                    str_GrossF = str_gross & vbNewLine & str_USDInvoice
    '                ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "" Then
    '                    str_gross = GrossTxt()
    '                    str_GrossF = str_gross & vbNewLine & str_USDInvoice
    '                End If

    '            ElseIf Mid(CommonUtility.Get_StringValue(txtFOBDisplay.Value), 1, 50) = "" Then

    '                If Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "NET WEIGHT" Then
    '                    str_gross = NetWeight_(TNet, txtunit_code2.Value) 'Mid(Tnet, 1, 9) & " " & Mid(txtunit_code2.Value, 1, 10) '& vbNewLine & FobTxtAll()
    '                    str_GrossF = str_gross & vbNewLine & str_USDInvoice
    '                ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "GROSS WEIGHT - NET WEIGHT" Then
    '                    str_gross = GrossTxt() & vbNewLine & NetWeight_(TNet, txtunit_code2.Value) 'Mid(Tnet, 1, 9) & " " & Mid(txtunit_code2.Value, 1, 10) '& vbNewLine & FobTxtAll()
    '                    str_GrossF = str_gross & vbNewLine & str_USDInvoice
    '                ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "GROSS WEIGHT" Then
    '                    str_gross = GrossTxt() '& vbNewLine & FobTxtAll()
    '                    str_GrossF = str_gross & vbNewLine & str_USDInvoice
    '                ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "" Then
    '                    str_gross = GrossTxt() '& vbNewLine & FobTxtAll()
    '                    str_GrossF = str_gross & vbNewLine & str_USDInvoice
    '                End If
    '            End If

    '        Case Else 'ไม่ใช่ invoice ต่างประเทศ
    '            If Mid(CommonUtility.Get_StringValue(txtFOBDisplay.Value), 1, 50) = "Display" Then

    '                If Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "NET WEIGHT" Then

    '                    str_gross = NetWeight_(TNet, txtunit_code2.Value) & vbNewLine & FobTxtAll() 'Mid(Tnet, 1, 9) & " " & Mid(txtunit_code2.Value, 1, 10) '& vbNewLine & FobTxtAll()
    '                    str_GrossF = str_gross

    '                ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "GROSS WEIGHT - NET WEIGHT" Then
    '                    str_gross = GrossTxt() & vbNewLine & NetWeight_(TNet, txtunit_code2.Value) & vbNewLine & FobTxtAll() 'Mid(Tnet, 1, 9) & " " & Mid(txtunit_code2.Value, 1, 10) '& vbNewLine & FobTxtAll()
    '                    str_GrossF = str_gross
    '                ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "GROSS WEIGHT" Then
    '                    str_gross = GrossTxt() & vbNewLine & FobTxtAll()
    '                    str_GrossF = str_gross
    '                ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "" Then
    '                    str_gross = GrossTxt() & vbNewLine & FobTxtAll()
    '                    str_GrossF = str_gross
    '                End If
    '            ElseIf Mid(CommonUtility.Get_StringValue(txtFOBDisplay.Value), 1, 50) = "NoDisplay" Then
    '                If Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "NET WEIGHT" Then
    '                    str_gross = NetWeight_(TNet, txtunit_code2.Value) 'Mid(Tnet, 1, 9) & " " & Mid(txtunit_code2.Value, 1, 10)
    '                    str_GrossF = str_gross
    '                ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "GROSS WEIGHT - NET WEIGHT" Then
    '                    str_gross = GrossTxt() & vbNewLine & NetWeight_(TNet, txtunit_code2.Value) 'Mid(Tnet, 1, 9) & " " & Mid(txtunit_code2.Value, 1, 10)
    '                    str_GrossF = str_gross
    '                ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "GROSS WEIGHT" Then
    '                    str_gross = GrossTxt()
    '                    str_GrossF = str_gross
    '                ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "" Then
    '                    str_gross = GrossTxt()
    '                    str_GrossF = str_gross
    '                End If

    '            ElseIf Mid(CommonUtility.Get_StringValue(txtFOBDisplay.Value), 1, 50) = "" Then

    '                If Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "NET WEIGHT" Then
    '                    str_gross = NetWeight_(TNet, txtunit_code2.Value) 'Mid(Tnet, 1, 9) & " " & Mid(txtunit_code2.Value, 1, 10) '& vbNewLine & FobTxtAll()
    '                    str_GrossF = str_gross
    '                ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "GROSS WEIGHT - NET WEIGHT" Then
    '                    str_gross = GrossTxt() & vbNewLine & NetWeight_(TNet, txtunit_code2.Value) 'Mid(Tnet, 1, 9) & " " & Mid(txtunit_code2.Value, 1, 10) '& vbNewLine & FobTxtAll()
    '                    str_GrossF = str_gross
    '                ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "GROSS WEIGHT" Then
    '                    str_gross = GrossTxt()
    '                    str_GrossF = str_gross
    '                ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "" Then
    '                    str_gross = GrossTxt()
    '                    str_GrossF = str_gross
    '                End If
    '            End If

    '    End Select
    '    Return str_GrossF
    'End Function

    'Sub CHeck_DisPlay()


    '    Dim sss As String = txtgross_weight.Text

    '    Dim str_gross As String

    '    'str_gross = txtgross_weight.Text

    '    'เรื่อง invoice ต่างประเทศ
    '    Dim str_NumInvoice As String = CommonUtility.Get_StringValue(txtNumInvoice.Text)
    '    'เรื่อง มูลค่า ต่างประเทศ
    '    Dim str_USDInvoice As String = CommonUtility.Get_StringValue(txtUSDInvoice.Text)

    '    Select Case txtshow_check.Text
    '        Case "100" ' เรื่อง invoice ต่างประเทศ
    '            If Mid(CommonUtility.Get_StringValue(txtFOBDisplay.Value), 1, 50) = "Display" Then

    '                If Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "NET WEIGHT" Then

    '                    str_gross = Mid(txtnet_weight.Value, 1, 9) & " " & Mid(txtunit_code2.Value, 1, 10) '& vbNewLine & FobTxt()
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

    '                ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "GROSS WEIGHT - NET WEIGHT" Then
    '                    str_gross = GrossTxt() & vbNewLine & Mid(txtnet_weight.Value, 1, 9) & " " & Mid(txtunit_code2.Value, 1, 10) '& vbNewLine & FobTxt()
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
    '                ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "GROSS WEIGHT" Then
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
    '                ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "" Then
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
    '                If Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "NET WEIGHT" Then
    '                    str_gross = Mid(txtnet_weight.Value, 1, 9) & " " & Mid(txtunit_code2.Value, 1, 10)
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
    '                ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "GROSS WEIGHT - NET WEIGHT" Then
    '                    str_gross = GrossTxt() & vbNewLine & Mid(txtnet_weight.Value, 1, 9) & " " & Mid(txtunit_code2.Value, 1, 10)
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
    '                ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "GROSS WEIGHT" Then
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
    '                ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "" Then
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

    '                If Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "NET WEIGHT" Then
    '                    str_gross = Mid(txtnet_weight.Value, 1, 9) & " " & Mid(txtunit_code2.Value, 1, 10) '& vbNewLine & FobTxt()
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
    '                ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "GROSS WEIGHT - NET WEIGHT" Then
    '                    str_gross = GrossTxt() & vbNewLine & Mid(txtnet_weight.Value, 1, 9) & " " & Mid(txtunit_code2.Value, 1, 10) '& vbNewLine & FobTxt()
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
    '                ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "GROSS WEIGHT" Then
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
    '                ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "" Then
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

    '        Case Else 'ไม่ใช่ invoice ต่างประเทศ
    '            If Mid(CommonUtility.Get_StringValue(txtFOBDisplay.Value), 1, 50) = "Display" Then

    '                If Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "NET WEIGHT" Then

    '                    str_gross = Mid(txtnet_weight.Value, 1, 9) & " " & Mid(txtunit_code2.Value, 1, 10) '& vbNewLine & FobTxt()
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

    '                ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "GROSS WEIGHT - NET WEIGHT" Then
    '                    str_gross = GrossTxt() & vbNewLine & Mid(txtnet_weight.Value, 1, 9) & " " & Mid(txtunit_code2.Value, 1, 10) '& vbNewLine & FobTxt()
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
    '                ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "GROSS WEIGHT" Then
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
    '                ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "" Then
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
    '                If Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "NET WEIGHT" Then
    '                    str_gross = Mid(txtnet_weight.Value, 1, 9) & " " & Mid(txtunit_code2.Value, 1, 10)
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
    '                ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "GROSS WEIGHT - NET WEIGHT" Then
    '                    str_gross = GrossTxt() & vbNewLine & Mid(txtnet_weight.Value, 1, 9) & " " & Mid(txtunit_code2.Value, 1, 10)
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
    '                ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "GROSS WEIGHT" Then
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
    '                ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "" Then
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

    '                If Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "NET WEIGHT" Then
    '                    str_gross = Mid(txtnet_weight.Value, 1, 9) & " " & Mid(txtunit_code2.Value, 1, 10) '& vbNewLine & FobTxt()
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
    '                ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "GROSS WEIGHT - NET WEIGHT" Then
    '                    str_gross = GrossTxt() & vbNewLine & Mid(txtnet_weight.Value, 1, 9) & " " & Mid(txtunit_code2.Value, 1, 10) '& vbNewLine & FobTxt()
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
    '                ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "GROSS WEIGHT" Then
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
    '                ElseIf Mid(CommonUtility.Get_StringValue(txtWeightDisplayHeaderH.Value), 1, 50) = "" Then
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
    'by rut rvc new
    Function GrossTxtDetail() As String
        Dim Str_GrossTxt As String
        Dim Dec_Gross As Decimal
        Dec_Gross = CDec(Check_Null(CommonUtility.Get_StringValue(txtgross_weightD.Value))) 'Format(CDec(Check_Null(CommonUtility.Get_StringValue(txtgross_weightH.Value))), "#,##0.00##")
        Str_GrossTxt = Format(Dec_Gross, "#,##0.00##") & " " & CheckUnitDetail(txtinvh_run_auto.Value) 'CommonUtility.Get_StringValue(txtunit_code3.Value) 'CStr(Dec_Gross) & " " & CommonUtility.Get_StringValue(txtg_unit_code.Value)

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

    '    If txtFOB_AMT.Value > 0 Then
    '        Dec_Fob = CDec(Check_Null(CommonUtility.Get_StringValue(TFOB))) 'Format(CDec(Check_Null(CommonUtility.Get_StringValue(txtFOB_AMT.Value))), "#,##0.0###")
    '        Str_FobTxt = Format(Dec_Fob, "#,##0.00##") & " USD"
    '    Else
    '        Str_FobTxt = ""
    '    End If
    '    Return Str_FobTxt
    'End Function
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

    'by rut rvc
    Function FobTxtAllSumNew() As String
        Dim Str_FobTxt As String
        Dim Dec_Fob As Decimal
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

        Return Str_FobTxt
    End Function

    Function FobTxtAll() As String
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
                    Dec_Fob = CDec(Check_Null(CommonUtility.Get_StringValue(TFOB))) 'Format(CDec(Check_Null(CommonUtility.Get_StringValue(txtFOB_AMT.Value))), "#,##0.0###")
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
        Select Case txtshow_check.value
            Case "100" 'third
                If txtthird_country.Value <> "" Then
                    PCheck_third_country.Visible = True
                    txtTemp_back_country.Text = "THAILAND"
                Else
                    PCheck_third_country.Visible = False
                    txtTemp_back_country.Text = "THAILAND"
                End If
            Case "010" 'place_exibition
                PCheck_place_exibition.Visible = True
                txtTemp_back_country.Text = "THAILAND"
            Case "001" 'back to back
                If txtback_country.Value <> "" Then
                    PCheck_back_country.Visible = True

                    txtTemp_back_country.Text = txtback_country.Value

                Else
                    PCheck_back_country.Visible = False

                    txtTemp_back_country.Text = "THAILAND"
                End If
            Case "000" 'none
                txtTemp_back_country.Text = "THAILAND"
        End Select
    End Sub
    Sub Check_box8()
        Select Case CommonUtility.Get_StringValue(txtbox8.Value)
            Case """RVC"""
                txtTemp_box8.Text = """RVC" & vbNewLine & txtprofit_per_unit.Value & " %" & """"
            Case """2RVC"""
                txtTemp_box8.Text = """RVC" & vbNewLine & txtprofit_per_unit.Value & " %" & """"
            Case """2RV"""
                txtTemp_box8.Text = """RVC" & vbNewLine & txtprofit_per_unit.Value & " %" & """"
            Case """CC+RVC"""
                txtTemp_box8.Text = Mid(CommonUtility.Get_StringValue(txtbox8.Value), 1, 7) & vbNewLine & txtprofit_per_unit.Value & " %" & Mid(CommonUtility.Get_StringValue(txtbox8.Value), 8, 1)
            Case """CTH+RVC"""
                txtTemp_box8.Text = Mid(CommonUtility.Get_StringValue(txtbox8.Value), 1, 8) & vbNewLine & txtprofit_per_unit.Value & " %" & Mid(CommonUtility.Get_StringValue(txtbox8.Value), 9, 1)
            Case """CTSH+RVC"""
                txtTemp_box8.Text = Mid(CommonUtility.Get_StringValue(txtbox8.Value), 1, 9) & vbNewLine & txtprofit_per_unit.Value & " %" & Mid(CommonUtility.Get_StringValue(txtbox8.Value), 10, 1)
            Case """SP"""
                txtTemp_box8.Text = "Specific Processes"  '& txtprofit_per_unit.Value & " %" & Mid(CommonUtility.Get_StringValue(txtbox8.Value), 10, 1)

            Case """C2SP""" 'by rut ปรับเพิ่มเงื่อนไขใหม่
                txtTemp_box8.Text = """CC+" & vbNewLine & "Specific Processes"""
            Case """CTSH""" 'by rut "CTC+Specific Processes" กฎการเปลี่ยนพิกัดบวกกระบวนผลิตที่เฉพาะเจาะจง
                txtTemp_box8.Text = """CTC+" & vbNewLine & "Specific Processes"""
            Case Else
                txtTemp_box8.Text = txtbox8.Value

        End Select
    End Sub

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        Me.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.Custom
        Me.PageSettings.PaperName = "Fanfold 210 x 305 mm"
        ' Add any initialization after the InitializeComponent() call.

    End Sub
End Class
