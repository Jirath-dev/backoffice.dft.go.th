Imports DataDynamics.ActiveReports
Imports DataDynamics.ActiveReports.Document

Imports Microsoft.ApplicationBlocks.Data
Imports System.Data.SqlClient
Imports System.Data
Imports System.Text
Imports DFT.Dotnetnuke.ClassLibrary
Imports ReportPrintClass
Public Class rpt3_ReEdi_A_V0
    Dim C_num As Integer
    Dim i As Integer

    Dim C_NET_WEIGHT As Decimal
    Dim C_Fob_Amt As Decimal

    Dim str_arr As Array
    Dim str_str As String = ""
    Dim ii As Integer
    Private Sub Detail1_Format(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Detail1.Format
        C_num += 1
        txtNum_Product.Text = C_num
        '===================================================
        txtTARIFF_CODE_Temp.Text = Mid(txtTARIFF_CODE.Text, 1, 4) + "." + Mid(txtTARIFF_CODE.Text, 5, 2)
        txtNET_WEIGHT_unit_code2.Text = Format(txtNET_WEIGHT.Value, "#,##0.00##") + " " + txtunit_code2.Text
        txtFOB_AMT_Temp.Text = Format(txtFOB_AMT.Value, "#,##0.00##")

        '===================================================
        C_NET_WEIGHT += CDec(txtNET_WEIGHT.Text)
        C_Fob_Amt += CDec(txtFOB_AMT.Text)

        str_str += txtPRODUCT_NAME.Value & ";"

        If C_num > 9 And C_num <= 10 Then
            'Dim _new As New PageBreak
            '_new.Visible = True
            'PageBreak1.Visible = True
            'Me.Detail1.NewPage = NewPage.After
        End If
        Me.Detail1.NewPage = NewPage.None
        'str_x += txtPRODUCT_NAME.Value
    End Sub

    Private Sub PageHeader1_Format(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PageHeader1.Format
        '===============txtForm_Name_Temp===================================
        txtForm_Name_Temp.Text = "คำขอหนังสือรับรองแหล่งกำเนิดสินค้าแบบ" + txtForm_Name.Text

        '===============txtREFERENCE_CODE2_Temp===================================
        txtREFERENCE_CODE2_Temp.Text = "I" + txtREFERENCE_CODE2.Text

        '============txtcard_id_Temp=txtcompany_name=====================================
        txtcard_id_Temp.Text = "อำนาจฯเลขที่  " + txtcard_id.Text + "   ของบริษัท/ห้าง/ร้าน  " + txtcompany_name.Text

        '===========txtcompany_taxno_Temp=======================================
        txtcompany_taxno_Temp.Text = "เลขประจำตัวผู้เสียภาษีอากร   " + txtcompany_taxno.Text

        '===============txtcompany_address_Temp===================================
        txtcompany_address_Temp.Text = "ตั้งอยู่เลขที่  " + txtcompany_address.Text

        '===============txtcompany_phone_fax_Temp===================================
        If CommonUtility.Get_StringValue(txtcompany_phone.Text) <> "" And CommonUtility.Get_StringValue(txtcompany_fax.Text) <> "" Then
            txtcompany_phone_fax_Temp.Text = " โทรศัพท์  " + txtcompany_phone.Text + "   โทรสาร  " + txtcompany_fax.Text
        ElseIf CommonUtility.Get_StringValue(txtcompany_phone.Text) <> "" And CommonUtility.Get_StringValue(txtcompany_fax.Text) = "" Then
            txtcompany_phone_fax_Temp.Text = " โทรศัพท์  " + txtcompany_phone.Text
        ElseIf CommonUtility.Get_StringValue(txtcompany_phone.Text) = "" And CommonUtility.Get_StringValue(txtcompany_fax.Text) <> "" Then
            txtcompany_phone_fax_Temp.Text = " โทรสาร  " + txtcompany_fax.Text
        End If

        '================txtdestination_address_province_dest_receive_country==================================
        If CommonUtility.Get_StringValue(txtdestination_phone.Text) <> "" And CommonUtility.Get_StringValue(txtdestination_fax.Text) <> "" Then
            txtdestination_address_province_dest_receive_country.Text = txtdestination_address.Text + "  " + txtdestination_province.Text + "  " + txtdest_receive_country.Text _
            + " โทรศัพท์ " + txtdestination_phone.Text + "  โทรสาร " + txtdestination_fax.Text

        ElseIf CommonUtility.Get_StringValue(txtdestination_phone.Text) <> "" And CommonUtility.Get_StringValue(txtdestination_fax.Text) = "" Then
            txtdestination_address_province_dest_receive_country.Text = txtdestination_address.Text + "  " + txtdestination_province.Text + "  " + txtdest_receive_country.Text _
                        + " โทรศัพท์ " + txtdestination_phone.Text

        ElseIf CommonUtility.Get_StringValue(txtdestination_phone.Text) = "" And CommonUtility.Get_StringValue(txtdestination_fax.Text) <> "" Then
            txtdestination_address_province_dest_receive_country.Text = txtdestination_address.Text + "  " + txtdestination_province.Text + "  " + txtdest_receive_country.Text _
            + "  โทรสาร " + txtdestination_fax.Text

        ElseIf CommonUtility.Get_StringValue(txtdestination_phone.Text) = "" And CommonUtility.Get_StringValue(txtdestination_fax.Text) = "" Then
            txtdestination_address_province_dest_receive_country.Text = txtdestination_address.Text + "  " + txtdestination_province.Text + "  " + txtdest_receive_country.Text '+ " โทรศัพท์ " + txtdestination_phone.Text + "  โทรสาร " + txtdestination_fax.Text

        Else
            txtdestination_address_province_dest_receive_country.Text = txtdestination_address.Text + "  " + txtdestination_province.Text + "  " + txtdest_receive_country.Text + " โทรศัพท์ " + txtdestination_phone.Text + "  โทรสาร " + txtdestination_fax.Text
        End If

        '===============txtimport_country_Temp===================================
        txtimport_country_Temp.Text = "ประเทศปลายทาง  " + txtimport_country.Text

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
    Dim jj As Integer
    Private Sub PageFooter1_Format(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PageFooter1.Format
        str_arr = str_str.Split(";")

        For jj = 0 To C_num - 1
            If jj < 9 Then
                TextBox47_man.Text += str_arr(jj) & vbNewLine
            ElseIf jj > 9 Then
                If jj < 10 Then
                    'Me.Detail1.NewPage = NewPage.Before
                    TextBox47_man.Text += str_arr(jj) & vbNewLine
                Else
                    'Me.Detail1.NewPage = NewPage.Before
                    TextBox47_man.Text += str_arr(jj) & vbNewLine
                End If
                
            End If
        Next
        

        txtTemp_sailing_date.Text = Format(CommonUtility.Get_DateTime(txtsailing_date.Value), "dd/MM/yyy")
        txtTemp_edi_date.Text = Format(CommonUtility.Get_DateTime(txtedi_date.Value), "dd/MM/yyy")

        Dim str_in As String = "เลขที่ " & IIf(txtinvoice_no1.Text <> "", txtinvoice_no1.Text, "") & " " & _
                                IIf(txtinvoice_no2.Text <> "", txtinvoice_no2.Text, "") & " " & _
                                IIf(txtinvoice_no3.Text <> "", txtinvoice_no3.Text, "") & " " & _
                                IIf(txtinvoice_no4.Text <> "", txtinvoice_no4.Text, "") & " " & _
                                IIf(txtinvoice_no5.Text <> "", txtinvoice_no5.Text, "")
        Dim str_inDate As String = "ลงวันที่ " & IIf(txtinvoice_no1.Text <> "" And txtinvoice_date1.Text <> Nothing, Format(CommonUtility.Get_DateTime(txtinvoice_date1.Text), "dd/MM/yyy"), "") & " " & _
                                    IIf(txtinvoice_no2.Text <> "" And txtinvoice_date2.Text <> Nothing, Format(CommonUtility.Get_DateTime(txtinvoice_date2.Text), "dd/MM/yyy"), "") & " " & _
                                    IIf(txtinvoice_no3.Text <> "" And txtinvoice_date3.Text <> Nothing, Format(CommonUtility.Get_DateTime(txtinvoice_date3.Text), "dd/MM/yyy"), "") & " " & _
                                    IIf(txtinvoice_no4.Text <> "" And txtinvoice_date4.Text <> Nothing, Format(CommonUtility.Get_DateTime(txtinvoice_date4.Text), "dd/MM/yyy"), "") & " " & _
                                    IIf(txtinvoice_no5.Text <> "" And txtinvoice_date5.Text <> Nothing, Format(CommonUtility.Get_DateTime(txtinvoice_date5.Text), "dd/MM/yyy"), "") & " "
        txtInvoince_Date.Text = str_in & " " & str_inDate

        txtSum_Net_Weight.Text = Format(C_NET_WEIGHT, "#,##0.00##")
        txtSum_Fob_Amt.Text = Format(C_Fob_Amt, "#,##0.00##")
    End Sub


    Private Sub GroupFooter1_Format(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GroupFooter1.Format

        '==================================================
        txtTotal.Text = "ยอดรวม       GROSS WEIGHT = " + GrossTxt() 'txtSum_Gross_Weight.Text

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
        '==============txtrequest_person_2====================================
        txtrequest_person_2.Text = "( " + txtauthorize2.Text + " )    ผู้รับมอบ" '"( " + txtrequest_person.Text + " )    ผู้รับมอบ"
    End Sub

    Function GrossTxt() As String
        Dim Str_GrossTxt As String
        Dim Dec_Gross As Decimal
        Dec_Gross = Format(CDec(Check_Null(CommonUtility.Get_StringValue(txtgross_weightH.Value))), "#,##0.00##")
        Str_GrossTxt = CStr(Dec_Gross) & " " & CommonUtility.Get_StringValue(txtg_unit_code.Value)

        Return Str_GrossTxt
    End Function

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        Me.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.A4
        'Me.PageSettings.PaperName = "A4"
        ' Add any initialization after the InitializeComponent() call.

    End Sub
End Class
