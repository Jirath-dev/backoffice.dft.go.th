Imports DataDynamics.ActiveReports 
Imports DataDynamics.ActiveReports.Document 

Imports Microsoft.ApplicationBlocks.Data
Imports System.Data.SqlClient
Imports System.Data
Imports System.Text
Public Class rpt3_ediFORM1 

    Private Sub PageFooter1_Format(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PageFooter1.Format
        txtcompany_provincefoot.Text = txtcompany_provincefoot1.Text & " " & Date.Today

    End Sub

    Private Sub Detail1_AfterPrint(ByVal sender As Object, ByVal e As System.EventArgs) Handles Detail1.AfterPrint
        'Me.Detail1.KeepTogether = True
    End Sub
    Private Sub Detail1_Format(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Detail1.Format
        '===============================================================
        Count_RowToPage_DetailData()
        '===============================================================
    End Sub

    'Sub Calldetail_Before()
    '    ''============================================================
    '    'If txtNumRowCount.Text = 7 Then
    '    '    txtNumRowCount.Text += 1
    '    '    'Me.Detail1.KeepTogether = True
    '    '    Me.Detail1.NewPage = NewPage.Before
    '    'ElseIf txtNumRowCount.Text = 14 Then
    '    '    txtNumRowCount.Text += 1
    '    '    Me.Detail1.NewPage = NewPage.Before
    '    'ElseIf txtNumRowCount.Text = 21 Then
    '    '    txtNumRowCount.Text += 1
    '    '    Me.Detail1.NewPage = NewPage.Before
    '    'ElseIf txtNumRowCount.Text = 28 Then
    '    '    txtNumRowCount.Text += 1
    '    '    Me.Detail1.NewPage = NewPage.Before
    '    'ElseIf txtNumRowCount.Text = 35 Then
    '    '    txtNumRowCount.Text += 1
    '    '    Me.Detail1.NewPage = NewPage.Before
    '    'ElseIf txtNumRowCount.Text = 42 Then
    '    '    txtNumRowCount.Text += 1
    '    '    Me.Detail1.NewPage = NewPage.Before
    '    'ElseIf txtNumRowCount.Text = 49 Then
    '    '    txtNumRowCount.Text += 1
    '    '    Me.Detail1.NewPage = NewPage.Before
    '    'ElseIf txtNumRowCount.Text = 56 Then
    '    '    txtNumRowCount.Text += 1
    '    '    Me.Detail1.NewPage = NewPage.Before
    '    'Else
    '    '    txtNumRowCount.Text += 1
    '    '    '===============================================================
    '    '    If txtgross_weight.Text <> "" Then
    '    '        txtgross_weight1.Text = txtgross_weight.Text
    '    '    Else
    '    '        txtgross_weight1.Text = ""
    '    '    End If
    '    '    '============================================================
    '    '    CallInvoiceCheck()
    '    '    '============================================================
    '    '    txtT_product.Text = txtproduct_n1.Text + txtproduct_n2.Text + "****"
    '    '    '============================================================
    '    '    Me.Detail1.NewPage = NewPage.None
    '    'End If
    'End Sub
    Dim C_TO As Integer = GetData_EdiDS("20080109-001010", "ST-001").Tables(0).Rows.Count 'GetData_EdiDS("20080109-001010", "ST-001").Tables(0).Rows.Count
    
    Sub Count_RowToPage_DetailData()
        Dim CountData_T, CountData_T2 As Integer
        CountData_T = C_TO '29 'CInt(TextBox2.Text)
        CountData_T2 = CountData_T / 7

        Dim i As Integer
        txtNumRowCount.Text += 1
        For i = 0 To CountData_T2
            Dim arrnum(i) As Integer

            arrnum(i) = i * 7 'ทำเพื่อเช็คจำนวนเปรียบเทียบ
            'TextBox2.Text = TextBox2.Text & "=" & CStr(arrnum(i))

            If (txtNumRowCount.Text = arrnum(i)) = True Then
                Me.Detail1.NewPage = NewPage.Before
                txtmarks.Visible = True
                txtgross_weight1.Visible = True
                txtTolInvoice.Visible = True
                Exit For
            Else
                '===============================================================
                If txtgross_weight.Text <> "" Then
                    txtgross_weight1.Text = txtgross_weight.Text + " " + txtg_unit_code.Text
                Else
                    txtgross_weight1.Text = ""
                End If
                '============================================================
                CallInvoiceCheck()
                '============================================================
                If txtNumRowCount.Text = C_TO Then
                    If txtquantity1.Text <> "" And txtq_unit_code1.Text <> "" Then
                        txtT_product.Text = txtproduct_n1.Text + txtproduct_n2.Text + "****" + vbCrLf + _
                        "TOTAL: " + BahtOnly(txtquantity1.Text, txtq_unit_code1.Text)
                    ElseIf txtquantity2.Text <> "" And txtq_unit_code2.Text <> "" Then
                        txtT_product.Text = txtproduct_n1.Text + txtproduct_n2.Text + "****" + vbCrLf + _
                                                "TOTAL: " + BahtOnly(txtquantity2.Text, txtq_unit_code2.Text)
                    ElseIf txtquantity3.Text <> "" And txtq_unit_code3.Text <> "" Then
                        txtT_product.Text = txtproduct_n1.Text + txtproduct_n2.Text + "****" + vbCrLf + _
                                                "TOTAL: " + BahtOnly(txtquantity3.Text, txtq_unit_code3.Text)
                    ElseIf txtquantity4.Text <> "" And txtq_unit_code4.Text <> "" Then
                        txtT_product.Text = txtproduct_n1.Text + txtproduct_n2.Text + "****" + vbCrLf + _
                                                "TOTAL: " + BahtOnly(txtquantity4.Text, txtq_unit_code4.Text)
                    ElseIf txtquantity5.Text <> "" And txtq_unit_code5.Text <> "" Then
                        txtT_product.Text = txtproduct_n1.Text + txtproduct_n2.Text + "****" + vbCrLf + _
                                                "TOTAL: " + BahtOnly(txtquantity5.Text, txtq_unit_code5.Text)
                    End If
                Else
                    txtT_product.Text = txtproduct_n1.Text + txtproduct_n2.Text + "****"
                End If

                '============================================================
                If txtNumRowCount.Text = "1" Then
                    txtmarks.Visible = True
                    txtgross_weight1.Visible = True
                    txtTolInvoice.Visible = True
                Else
                    txtmarks.Visible = False
                    txtgross_weight1.Visible = False
                    txtTolInvoice.Visible = False
                End If

                Me.Detail1.NewPage = NewPage.None
            End If

        Next

        
    End Sub
    
    Sub CallInvoiceCheck() 'Check Invoince
        If txtinvoice_no1.Text <> "" And txtinvoice_date1.Text <> "" Then
            txtTolInvoice.Text = txtinvoice_no1.Text & vbNewLine & CDate(txtinvoice_date1.Text).Date
        ElseIf txtinvoice_no2.Text <> "" And txtinvoice_date2.Text <> "" Then
            txtTolInvoice.Text = txtinvoice_no1.Text & vbNewLine & CDate(txtinvoice_date1.Text).Date
        ElseIf txtinvoice_no3.Text <> "" And txtinvoice_date3.Text <> "" Then
            txtTolInvoice.Text = txtinvoice_no1.Text & vbNewLine & CDate(txtinvoice_date1.Text).Date
        ElseIf txtinvoice_no4.Text <> "" And txtinvoice_date4.Text <> "" Then
            txtTolInvoice.Text = txtinvoice_no1.Text & vbNewLine & CDate(txtinvoice_date1.Text).Date
        ElseIf txtinvoice_no5.Text <> "" And txtinvoice_date5.Text <> "" Then
            txtTolInvoice.Text = txtinvoice_no1.Text & vbNewLine & CDate(txtinvoice_date1.Text).Date
        End If

    End Sub

    Dim Cpage As Integer
    Private Sub PageHeader1_Format(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PageHeader1.Format
        '===============================================================
        Cpage += 1
        txtPage2.Text = Cpage
        '===============================================================
        If (Len(txtcompany_phone.Text) <> 0) And (Len(txtcompany_fax.Text) <> 0) And (Mid(txtdest_remark.Text, 1, 3) = "C/O") Then
            txtCompany_Check_1.Text = txtob_address.Text(+" CARE OF " + txtcompany_name.Text + " " + txtcompany_address.Text + " " + txtcompany_province.Text + " " + txtcompany_country.Text + " TEL: " + txtcompany_phone.Text + " FAX: " + txtcompany_fax.Text + " TAX ID:  " + txtcompany_taxno.Text)
        ElseIf (Len(txtcompany_phone.Text) <> 0) And (Len(txtcompany_fax.Text) <> 0) And (Mid(txtdest_remark.Text, 1, 3) = "O/B") Then
            txtCompany_Check_1.Text = txtcompany_name.Text + " " + txtcompany_address.Text + " " + txtcompany_province.Text + " " + txtcompany_country.Text + " TEL: " + txtcompany_phone.Text + " FAX: " + txtcompany_fax.Text + " TAX ID: " + txtcompany_taxno.Text + " ON BEHALF OF " + txtob_address.Text
        ElseIf (Len(txtcompany_phone.Text) <> 0) And (Len(txtcompany_fax.Text) <> 0) And (Mid(txtdest_remark.Text, 2, 1) <> "/") Then
            txtCompany_Check_1.Text = txtcompany_name.Text + " " + txtcompany_address.Text + " " + txtcompany_province.Text + " " + txtcompany_country.Text + " TEL: " + txtcompany_phone.Text + " FAX: " + txtcompany_fax.Text + " TAX ID: " + txtcompany_taxno.Text
        ElseIf (Len(txtcompany_phone.Text) <> 0) And (Len(txtcompany_fax.Text) = 0) And (Mid(txtdest_remark.Text, 1, 3) = "C/O") Then
            txtCompany_Check_1.Text = txtob_address.Text(+" CARE OF " + txtcompany_name.Text + " " + txtcompany_address.Text + " " + txtcompany_province.Text + " " + txtcompany_country.Text + " TEL: " + txtcompany_phone.Text + " TAX ID: " + txtcompany_taxno.Text)
        ElseIf (Len(txtcompany_phone.Text) <> 0) And (Len(txtcompany_fax.Text) = 0) And (Mid(txtdest_remark.Text, 1, 3) = "O/B") Then
            txtCompany_Check_1.Text = txtcompany_name.Text + " " + txtcompany_address.Text + " " + txtcompany_province.Text + " " + txtcompany_country.Text + " TEL: " + txtcompany_phone.Text + " TAX ID: " + txtcompany_taxno.Text + " ON BEHALF OF " + txtob_address.Text
        ElseIf (Len(txtcompany_phone.Text) <> 0) And (Len(txtcompany_fax.Text) = 0) And (Mid(txtdest_remark.Text, 2, 1) <> "/") Then
            txtCompany_Check_1.Text = txtcompany_name.Text + " " + txtcompany_address.Text + " " + txtcompany_province.Text + " " + txtcompany_country.Text + " TEL: " + txtcompany_phone.Text + " TAX ID: " + txtcompany_taxno.Text
        ElseIf (Len(txtcompany_phone.Text) = 0) And (Len(txtcompany_fax.Text) = 0) And (Mid(txtdest_remark.Text, 1, 3) = "C/O") Then
            txtCompany_Check_1.Text = txtob_address.Text + " CARE OF " + txtcompany_name.Text + " " + txtcompany_address.Text + " " + txtcompany_province.Text + " " + txtcompany_country.Text + " TEL: " + txtcompany_phone.Text + " TAX ID: " + txtcompany_taxno.Text
        ElseIf (Len(txtcompany_phone.Text) = 0) And (Len(txtcompany_fax.Text) = 0) And (Mid(txtdest_remark.Text, 1, 3) = "O/B") Then
            txtCompany_Check_1.Text = txtcompany_name.Text + " " + txtcompany_address.Text + " " + txtcompany_province.Text + " " + txtcompany_country.Text + " TEL: " + txtcompany_phone.Text + " TAX ID: " + txtcompany_taxno.Text + " ON BEHALF OF " + txtob_address.Text
        ElseIf (Len(txtcompany_phone.Text) = 0) And (Len(txtcompany_fax.Text) = 0) And (Mid(txtdest_remark.Text, 2, 1) <> "/") Then
            txtCompany_Check_1.Text = txtcompany_name.Text + " " + txtcompany_address.Text + " " + txtcompany_province.Text + " " + txtcompany_country.Text + " TEL: " + txtcompany_phone.Text + " TAX ID: " + txtcompany_taxno.Text
        End If
        '===============================================================
        If (txtdestination_phone.Text <> "") And (txtdestination_fax.Text <> "") Then
            txtdestination_Check2.Text = txtdestination_company.Text + " " + txtdestination_address.Text + " " + txtdestination_province.Text + " " + txtdest_Receive_country.Text + " TEL: " + txtdestination_phone.Text + " FAX: " + txtdestination_fax.Text
        ElseIf (txtdestination_phone.Text = "") And (txtdestination_fax.Text <> "") Then
            txtdestination_Check2.Text = txtdestination_company.Text + " " + txtdestination_address.Text + " " + txtdestination_province.Text + " " + txtdest_Receive_country.Text + " FAX: " + txtdestination_fax.Text
        ElseIf (txtdestination_phone.Text <> "") And (txtdestination_fax.Text = "") Then
            txtdestination_Check2.Text = txtdestination_company.Text + " " + txtdestination_address.Text + " " + txtdestination_province.Text + " " + txtdest_Receive_country.Text + " TEL: " + txtdestination_phone.Text
        ElseIf (txtdestination_phone.Text = "") And (txtdestination_fax.Text = "") Then
            txtdestination_Check2.Text = txtdestination_company.Text + " " + txtdestination_address.Text + " " + txtdestination_province.Text + " " + txtdest_Receive_country.Text
        End If
    End Sub


    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.Custom
        Me.PageSettings.PaperName = "Fanfold 210 x 305 mm"
    End Sub
    Function GetData_EdiDS(ByVal INVH_RUN_AUTO As String, ByVal SITE_ID As String) As DataSet
        Dim conn As String = ConfigurationManager.ConnectionStrings("EDIConnectionString").ConnectionString

        Dim dr As DataSet = SqlHelper.ExecuteDataset(conn, CommandType.StoredProcedure, "sp_form4_edi_printForm", New SqlParameter("@INVH_RUN_AUTO", INVH_RUN_AUTO), New SqlParameter("@SITE_ID", SITE_ID))
        Return dr
    End Function

   
End Class
