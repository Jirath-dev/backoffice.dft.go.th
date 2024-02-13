Imports DataDynamics.ActiveReports
Imports DataDynamics.ActiveReports.Document

Imports Microsoft.ApplicationBlocks.Data
Imports System.Data.SqlClient
Imports System.Data
Imports System.Text
Imports DFT.Dotnetnuke.ClassLibrary
Imports ReportPrintClass
Public Class rpt3_ReEdi_A 
    Dim C_num As Integer
    Dim i As Integer

    Dim C_NET_WEIGHT As Decimal
    Dim C_Fob_Amt As Decimal
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

        txtPRODUCT_NAME.Text = txtTemp_PRODUCT_NAME.Text & " " & txtRover.Text
    End Sub
    Dim ii As Integer
    Private Sub PageHeader1_Format(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PageHeader1.Format
        '===============txtForm_Name_Temp===================================
        Select Case CommonUtility.Get_StringValue(txtform_type.Text)
            Case "FORM2_2"
                txtForm_Name_Temp.Text = "คำขอหนังสือรับรองถิ่นกำเนิดสินค้าเกษตรเพื่อนำเข้าสหภาพยุโรป (ไก่)"
            Case "FORM2_3"
                txtForm_Name_Temp.Text = "คำขอหนังสือรับรองถิ่นกำเนิดสินค้าเกษตรเพื่อนำเข้าสหภาพยุโรป (มันสำปะหลัง)"
            Case Else
                txtForm_Name_Temp.Text = "คำขอหนังสือรับรองถิ่นกำเนิดสินค้าแบบ" & txtForm_Name.Text
        End Select

        ii += 1
        '===============txtREFERENCE_CODE2_Temp===================================
        txtREFERENCE_CODE2_Temp.Text = "I" & txtREFERENCE_CODE2.Text

        '=====================================================
        txtTemp_request_person.Text = txtrequest_person.Text & "    และ.......ผู้มีอำนาจกระทำการแทนนิติบุคคล"
        '============txtcard_id_Temp=txtcompany_name=====================================
        txtcard_id_Temp.Text = "อำนาจฯเลขที่  " & txtcard_id.Text & "   ของบริษัท/ห้าง/ร้าน  " & txtcompany_name.Text & _
        vbNewLine & "เลขประจำตัวผู้เสียภาษีอากร   " & txtcompany_taxno.Text & _
        vbNewLine & "ตั้งอยู่เลขที่  " & txtcompany_address.Text & " " & txtcompany_province.Text & " ประเทศ " & txtcompany_country.Text & vbNewLine & _
        IIf(CommonUtility.Get_StringValue(txtcompany_phone.Text) <> "", "โทรศัพท์  " & txtcompany_phone.Text, "โทรศัพท์    -") & _
        IIf(CommonUtility.Get_StringValue(txtcompany_fax.Text) <> "", "   โทรสาร  " & txtcompany_fax.Text, "   โทรสาร    -")

        '================txtdestination_address_province_dest_receive_country==================================
        If CommonUtility.Get_StringValue(txtdestination_phone.Text) <> "" And CommonUtility.Get_StringValue(txtdestination_fax.Text) <> "" Then
            txtdestination_address_province_dest_receive_country.Text = txtdestination_company.Text & vbNewLine & txtdestination_address.Text & "  " & txtdestination_province.Text & "  " & txtdest_receive_country.Text _
            & " โทรศัพท์ " & txtdestination_phone.Text & "  โทรสาร " & txtdestination_fax.Text

        ElseIf CommonUtility.Get_StringValue(txtdestination_phone.Text) <> "" And CommonUtility.Get_StringValue(txtdestination_fax.Text) = "" Then
            txtdestination_address_province_dest_receive_country.Text = txtdestination_company.Text & vbNewLine & txtdestination_address.Text & "  " & txtdestination_province.Text & "  " & txtdest_receive_country.Text _
                        & " โทรศัพท์ " & txtdestination_phone.Text

        ElseIf CommonUtility.Get_StringValue(txtdestination_phone.Text) = "" And CommonUtility.Get_StringValue(txtdestination_fax.Text) <> "" Then
            txtdestination_address_province_dest_receive_country.Text = txtdestination_company.Text & vbNewLine & txtdestination_address.Text & "  " & txtdestination_province.Text & "  " & txtdest_receive_country.Text _
            & "  โทรสาร " & txtdestination_fax.Text

        ElseIf CommonUtility.Get_StringValue(txtdestination_phone.Text) = "" And CommonUtility.Get_StringValue(txtdestination_fax.Text) = "" Then
            txtdestination_address_province_dest_receive_country.Text = txtdestination_company.Text & vbNewLine & txtdestination_address.Text & "  " & txtdestination_province.Text & "  " & txtdest_receive_country.Text '& " โทรศัพท์ " & txtdestination_phone.Text & "  โทรสาร " & txtdestination_fax.Text

        Else
            txtdestination_address_province_dest_receive_country.Text = txtdestination_company.Text & vbNewLine & txtdestination_address.Text & "  " & txtdestination_province.Text & "  " & txtdest_receive_country.Text & " โทรศัพท์ " & txtdestination_phone.Text & "  โทรสาร " & txtdestination_fax.Text
        End If

        '===============txtimport_country_Temp===================================
        txtimport_country_Temp.Text = "ประเทศปลายทาง  " & txtimport_country.Text

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
    End Sub

    Private Sub PageFooter1_Format(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PageFooter1.Format
        If txtNumpageForm.Value = 1 Then 'check เพื่อแสดงแค่หน้าแรกหน้าเดียว
            txtTotal.Text = "ยอดรวม       GROSS WEIGHT = " & GrossTxt() 'txtSum_Gross_Weight.Text
        Else
            txtTotal.Text = ""
        End If

        txtTemp_sailing_date.Text = Format(Get_DateTime_rpt(txtsailing_date.Value), "dd/MM/yyy") 'Format(Get_DateTime_rpt(txtsailing_date.Value), "dd/MM/yyy")
        txtTemp_edi_date.Text = Format(Get_DateTime_rpt(txtedi_date.Value), "dd/MM/yyy")

        'Dim str_in As String = "เลขที่ " & IIf(txtinvoice_no1.Text <> "", txtinvoice_no1.Text.Replace(vbCrLf, ""), "") & " " & _
        '                                IIf(txtinvoice_no2.Text <> "", txtinvoice_no2.Text.Replace(vbCrLf, ""), "")
        'mpReplace = tmpReplace.Replace(vbCrLf, "chr(13) + chr(10)")



        'Dim str_in As String = "เลขที่ " & IIf(txtinvoice_no1.Text <> "", txtinvoice_no1.Text.Replace(vbCrLf, ""), "") & " " & _
        '                        IIf(txtinvoice_no2.Text <> "", txtinvoice_no2.Text.Replace(vbCrLf, ""), "") & " " & _
        '                        IIf(txtinvoice_no3.Text <> "", txtinvoice_no3.Text.Replace(vbCrLf, ""), "") & " " & _
        '                        IIf(txtinvoice_no4.Text <> "", txtinvoice_no4.Text.Replace(vbCrLf, ""), "") & " " & _
        '                        IIf(txtinvoice_no5.Text <> "", txtinvoice_no5.Text.Replace(vbCrLf, ""), "")
        'Dim str_inDate As String = "ลงวันที่ " & IIf(txtinvoice_no1.Text <> "" And txtinvoice_date1.Text <> Nothing, Format(Get_DateTime_rpt(txtinvoice_date1.Text), "dd/MM/yyy"), "") & " " & _
        '                            IIf(txtinvoice_no2.Text <> "" And txtinvoice_date2.Text <> Nothing, Format(Get_DateTime_rpt(txtinvoice_date2.Text), "dd/MM/yyy"), "") & " " & _
        '                            IIf(txtinvoice_no3.Text <> "" And txtinvoice_date3.Text <> Nothing, Format(Get_DateTime_rpt(txtinvoice_date3.Text), "dd/MM/yyy"), "") & " " & _
        '                            IIf(txtinvoice_no4.Text <> "" And txtinvoice_date4.Text <> Nothing, Format(Get_DateTime_rpt(txtinvoice_date4.Text), "dd/MM/yyy"), "") & " " & _
        '                            IIf(txtinvoice_no5.Text <> "" And txtinvoice_date5.Text <> Nothing, Format(Get_DateTime_rpt(txtinvoice_date5.Text), "dd/MM/yyy"), "") & " "
        Dim str_in As String = ""
        str_in = "เลขที่ " & IIf(CommonUtility.Get_StringValue(txtinvoice_no1.Text) <> "", CommonUtility.Get_StringValue(txtinvoice_no1.Text).Replace(vbCrLf, ""), "") & " " & _
                                IIf(CommonUtility.Get_StringValue(txtinvoice_no2.Text) <> "", CommonUtility.Get_StringValue(txtinvoice_no2.Text).Replace(vbCrLf, ""), "") & " " & _
                                IIf(CommonUtility.Get_StringValue(txtinvoice_no3.Text) <> "", CommonUtility.Get_StringValue(txtinvoice_no3.Text).Replace(vbCrLf, ""), "") & " " & _
                                IIf(CommonUtility.Get_StringValue(txtinvoice_no4.Text) <> "", CommonUtility.Get_StringValue(txtinvoice_no4.Text).Replace(vbCrLf, ""), "") & " " & _
                                IIf(CommonUtility.Get_StringValue(txtinvoice_no5.Text) <> "", CommonUtility.Get_StringValue(txtinvoice_no5.Text).Replace(vbCrLf, ""), "")
        Dim str_inDate As String = ""
        str_inDate = "ลงวันที่ " & IIf(CommonUtility.Get_StringValue(txtinvoice_no1.Text) <> "" And txtinvoice_date1.Text <> Nothing, Format(Get_DateTime_rpt(txtinvoice_date1.Text), "dd/MM/yyy"), "") & " " & _
                                    IIf(CommonUtility.Get_StringValue(txtinvoice_no2.Text) <> "" And txtinvoice_date2.Text <> Nothing, Format(Get_DateTime_rpt(txtinvoice_date2.Text), "dd/MM/yyy"), "") & " " & _
                                    IIf(CommonUtility.Get_StringValue(txtinvoice_no3.Text) <> "" And txtinvoice_date3.Text <> Nothing, Format(Get_DateTime_rpt(txtinvoice_date3.Text), "dd/MM/yyy"), "") & " " & _
                                    IIf(CommonUtility.Get_StringValue(txtinvoice_no4.Text) <> "" And txtinvoice_date4.Text <> Nothing, Format(Get_DateTime_rpt(txtinvoice_date4.Text), "dd/MM/yyy"), "") & " " & _
                                    IIf(CommonUtility.Get_StringValue(txtinvoice_no5.Text) <> "" And txtinvoice_date5.Text <> Nothing, Format(Get_DateTime_rpt(txtinvoice_date5.Text), "dd/MM/yyy"), "") & " "

        txtInvoince_Date.Text = str_in.Replace(Chr(10), "") & " " & str_inDate

        txtSum_Net_Weight.Text = Format(CDec(txtNET_WEIGHTPage.Text), "#,##0.00##") & " " & txtunit_code2.Text
        txtSum_Fob_Amt.Text = Format(CDec(txtSumFOB_AMTPage.Text), "#,##0.00##")

        '==============txtrequest_person_2====================================
        txtrequest_person_2.Text = "( " + txtrequest_person.Text + " )    ผู้รับมอบ" '"( " + txtrequest_person.Text + " )    ผู้รับมอบ"

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
        txtfactory_Temp.Text = txtfactory.Text + " ตั้งอยู่ที่ " + txtfactory_address.Text
    End Sub


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

        Me.CurrentPage.DrawLine(0.31, 4.688, 0.31, 9) 'เส้นซ้ายสุด
        Me.CurrentPage.DrawLine(8, 4.688, 8, 9) 'เส้นขวาสุด

        Me.CurrentPage.DrawLine(4.625, 4.688, 4.625, 6.88)
        Me.CurrentPage.DrawLine(5.625, 4.688, 5.625, 7.19)
        Me.CurrentPage.DrawLine(7, 4.688, 7, 6.88)
    End Sub

    
    Private Sub ReportFooter1_Format(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ReportFooter1.Format

    End Sub

    
    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub
End Class
