<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class rptF04_Report_07 
    Inherits DataDynamics.ActiveReports.ActiveReport3 

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
        End If
        MyBase.Dispose(disposing)
    End Sub
    
    'NOTE: The following procedure is required by the ActiveReports Designer
    'It can be modified using the ActiveReports Designer.
    'Do not modify it using the code editor.
    Private WithEvents PageHeader1 As DataDynamics.ActiveReports.PageHeader
    Private WithEvents Detail1 As DataDynamics.ActiveReports.Detail
    Private WithEvents PageFooter1 As DataDynamics.ActiveReports.PageFooter
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(rptF04_Report_07))
        Me.PageHeader1 = New DataDynamics.ActiveReports.PageHeader
        Me.Detail1 = New DataDynamics.ActiveReports.Detail
        Me.PageFooter1 = New DataDynamics.ActiveReports.PageFooter
        Me.GroupHeader1 = New DataDynamics.ActiveReports.GroupHeader
        Me.GroupFooter1 = New DataDynamics.ActiveReports.GroupFooter
        Me.Label1 = New DataDynamics.ActiveReports.Label
        Me.txtPrintDate = New DataDynamics.ActiveReports.Label
        Me.lblSite_ID = New DataDynamics.ActiveReports.Label
        Me.Label4 = New DataDynamics.ActiveReports.Label
        Me.Label5 = New DataDynamics.ActiveReports.Label
        Me.Label6 = New DataDynamics.ActiveReports.Label
        Me.Label7 = New DataDynamics.ActiveReports.Label
        Me.Line1 = New DataDynamics.ActiveReports.Line
        Me.Label9 = New DataDynamics.ActiveReports.Label
        Me.Label8 = New DataDynamics.ActiveReports.Label
        Me.txtForm_Name = New DataDynamics.ActiveReports.TextBox
        Me.Line2 = New DataDynamics.ActiveReports.Line
        Me.txtRemark_docatt = New DataDynamics.ActiveReports.TextBox
        Me.txtInvoice_No = New DataDynamics.ActiveReports.TextBox
        Me.txtApprove_Date = New DataDynamics.ActiveReports.TextBox
        Me.txtReference_Code2 = New DataDynamics.ActiveReports.TextBox
        Me.txtIndex = New DataDynamics.ActiveReports.TextBox
        Me.txtComapny_Name = New DataDynamics.ActiveReports.TextBox
        Me.ReportInfo1 = New DataDynamics.ActiveReports.ReportInfo
        CType(Me.Label1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPrintDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblSite_ID, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtForm_Name, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRemark_docatt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtInvoice_No, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtApprove_Date, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtReference_Code2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtIndex, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtComapny_Name, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ReportInfo1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'PageHeader1
        '
        Me.PageHeader1.Controls.AddRange(New DataDynamics.ActiveReports.ARControl() {Me.Label1, Me.txtPrintDate, Me.lblSite_ID, Me.Label4, Me.Label5, Me.Label6, Me.Label7, Me.Line1, Me.Label9, Me.Label8})
        Me.PageHeader1.Height = 1.0625!
        Me.PageHeader1.Name = "PageHeader1"
        '
        'Detail1
        '
        Me.Detail1.ColumnSpacing = 0.0!
        Me.Detail1.Controls.AddRange(New DataDynamics.ActiveReports.ARControl() {Me.txtRemark_docatt, Me.txtInvoice_No, Me.txtApprove_Date, Me.txtReference_Code2, Me.txtIndex, Me.txtComapny_Name})
        Me.Detail1.Height = 0.3125!
        Me.Detail1.Name = "Detail1"
        '
        'PageFooter1
        '
        Me.PageFooter1.Controls.AddRange(New DataDynamics.ActiveReports.ARControl() {Me.ReportInfo1})
        Me.PageFooter1.Height = 0.3125!
        Me.PageFooter1.Name = "PageFooter1"
        '
        'GroupHeader1
        '
        Me.GroupHeader1.Controls.AddRange(New DataDynamics.ActiveReports.ARControl() {Me.txtForm_Name, Me.Line2})
        Me.GroupHeader1.DataField = "form_name"
        Me.GroupHeader1.Height = 0.313!
        Me.GroupHeader1.Name = "GroupHeader1"
        '
        'GroupFooter1
        '
        Me.GroupFooter1.Height = 0.0!
        Me.GroupFooter1.Name = "GroupFooter1"
        '
        'Label1
        '
        Me.Label1.Border.BottomColor = System.Drawing.Color.Black
        Me.Label1.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Label1.Border.LeftColor = System.Drawing.Color.Black
        Me.Label1.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Label1.Border.RightColor = System.Drawing.Color.Black
        Me.Label1.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Label1.Border.TopColor = System.Drawing.Color.Black
        Me.Label1.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Label1.Height = 0.3125!
        Me.Label1.HyperLink = Nothing
        Me.Label1.Left = 0.0625!
        Me.Label1.Name = "Label1"
        Me.Label1.Style = "ddo-char-set: 0; text-align: center; font-weight: bold; font-size: 18pt; font-fam" & _
            "ily: AngsanaUPC; "
        Me.Label1.Text = "รายงานการรับเอกสารประจำวัน"
        Me.Label1.Top = 0.0!
        Me.Label1.Width = 8.375!
        '
        'txtPrintDate
        '
        Me.txtPrintDate.Border.BottomColor = System.Drawing.Color.Black
        Me.txtPrintDate.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtPrintDate.Border.LeftColor = System.Drawing.Color.Black
        Me.txtPrintDate.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtPrintDate.Border.RightColor = System.Drawing.Color.Black
        Me.txtPrintDate.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtPrintDate.Border.TopColor = System.Drawing.Color.Black
        Me.txtPrintDate.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtPrintDate.Height = 0.3125!
        Me.txtPrintDate.HyperLink = Nothing
        Me.txtPrintDate.Left = 0.0625!
        Me.txtPrintDate.Name = "txtPrintDate"
        Me.txtPrintDate.Style = "ddo-char-set: 0; font-size: 15.75pt; font-family: AngsanaUPC; "
        Me.txtPrintDate.Text = "วันที่พิมพ์"
        Me.txtPrintDate.Top = 0.375!
        Me.txtPrintDate.Width = 2.9375!
        '
        'lblSite_ID
        '
        Me.lblSite_ID.Border.BottomColor = System.Drawing.Color.Black
        Me.lblSite_ID.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.lblSite_ID.Border.LeftColor = System.Drawing.Color.Black
        Me.lblSite_ID.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.lblSite_ID.Border.RightColor = System.Drawing.Color.Black
        Me.lblSite_ID.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.lblSite_ID.Border.TopColor = System.Drawing.Color.Black
        Me.lblSite_ID.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.lblSite_ID.Height = 0.3125!
        Me.lblSite_ID.HyperLink = Nothing
        Me.lblSite_ID.Left = 5.5!
        Me.lblSite_ID.Name = "lblSite_ID"
        Me.lblSite_ID.Style = "ddo-char-set: 0; text-align: right; font-size: 15.75pt; font-family: AngsanaUPC; " & _
            ""
        Me.lblSite_ID.Text = "สถานที่ : ST-001"
        Me.lblSite_ID.Top = 0.375!
        Me.lblSite_ID.Width = 2.9375!
        '
        'Label4
        '
        Me.Label4.Border.BottomColor = System.Drawing.Color.Black
        Me.Label4.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Label4.Border.LeftColor = System.Drawing.Color.Black
        Me.Label4.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Label4.Border.RightColor = System.Drawing.Color.Black
        Me.Label4.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Label4.Border.TopColor = System.Drawing.Color.Black
        Me.Label4.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Label4.Height = 0.3125!
        Me.Label4.HyperLink = Nothing
        Me.Label4.Left = 0.0625!
        Me.Label4.Name = "Label4"
        Me.Label4.Style = "ddo-char-set: 0; text-align: center; font-size: 15.75pt; font-family: AngsanaUPC;" & _
            " "
        Me.Label4.Text = "ลำดับ"
        Me.Label4.Top = 0.75!
        Me.Label4.Width = 0.375!
        '
        'Label5
        '
        Me.Label5.Border.BottomColor = System.Drawing.Color.Black
        Me.Label5.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Label5.Border.LeftColor = System.Drawing.Color.Black
        Me.Label5.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Label5.Border.RightColor = System.Drawing.Color.Black
        Me.Label5.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Label5.Border.TopColor = System.Drawing.Color.Black
        Me.Label5.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Label5.Height = 0.3125!
        Me.Label5.HyperLink = Nothing
        Me.Label5.Left = 0.4375!
        Me.Label5.Name = "Label5"
        Me.Label5.Style = "ddo-char-set: 0; text-align: center; font-size: 15.75pt; font-family: AngsanaUPC;" & _
            " "
        Me.Label5.Text = "บริษัท"
        Me.Label5.Top = 0.75!
        Me.Label5.Width = 3.0625!
        '
        'Label6
        '
        Me.Label6.Border.BottomColor = System.Drawing.Color.Black
        Me.Label6.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Label6.Border.LeftColor = System.Drawing.Color.Black
        Me.Label6.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Label6.Border.RightColor = System.Drawing.Color.Black
        Me.Label6.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Label6.Border.TopColor = System.Drawing.Color.Black
        Me.Label6.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Label6.Height = 0.3125!
        Me.Label6.HyperLink = Nothing
        Me.Label6.Left = 3.5!
        Me.Label6.Name = "Label6"
        Me.Label6.Style = "ddo-char-set: 0; text-align: center; font-size: 15.75pt; font-family: AngsanaUPC;" & _
            " "
        Me.Label6.Text = "เลขที่หนังสือ"
        Me.Label6.Top = 0.75!
        Me.Label6.Width = 1.0!
        '
        'Label7
        '
        Me.Label7.Border.BottomColor = System.Drawing.Color.Black
        Me.Label7.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Label7.Border.LeftColor = System.Drawing.Color.Black
        Me.Label7.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Label7.Border.RightColor = System.Drawing.Color.Black
        Me.Label7.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Label7.Border.TopColor = System.Drawing.Color.Black
        Me.Label7.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Label7.Height = 0.3125!
        Me.Label7.HyperLink = Nothing
        Me.Label7.Left = 4.5!
        Me.Label7.Name = "Label7"
        Me.Label7.Style = "ddo-char-set: 0; text-align: center; font-size: 15.75pt; font-family: AngsanaUPC;" & _
            " "
        Me.Label7.Text = "วันที่อนุมัติ"
        Me.Label7.Top = 0.75!
        Me.Label7.Width = 0.8125!
        '
        'Line1
        '
        Me.Line1.Border.BottomColor = System.Drawing.Color.Black
        Me.Line1.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Line1.Border.LeftColor = System.Drawing.Color.Black
        Me.Line1.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Line1.Border.RightColor = System.Drawing.Color.Black
        Me.Line1.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Line1.Border.TopColor = System.Drawing.Color.Black
        Me.Line1.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Line1.Height = 0.0!
        Me.Line1.Left = 0.0625!
        Me.Line1.LineWeight = 1.0!
        Me.Line1.Name = "Line1"
        Me.Line1.Top = 0.75!
        Me.Line1.Width = 8.375!
        Me.Line1.X1 = 0.0625!
        Me.Line1.X2 = 8.4375!
        Me.Line1.Y1 = 0.75!
        Me.Line1.Y2 = 0.75!
        '
        'Label9
        '
        Me.Label9.Border.BottomColor = System.Drawing.Color.Black
        Me.Label9.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Label9.Border.LeftColor = System.Drawing.Color.Black
        Me.Label9.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Label9.Border.RightColor = System.Drawing.Color.Black
        Me.Label9.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Label9.Border.TopColor = System.Drawing.Color.Black
        Me.Label9.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Label9.Height = 0.3125!
        Me.Label9.HyperLink = Nothing
        Me.Label9.Left = 6.5!
        Me.Label9.Name = "Label9"
        Me.Label9.Style = "ddo-char-set: 0; text-align: center; font-size: 15.75pt; font-family: AngsanaUPC;" & _
            " "
        Me.Label9.Text = "เหตุผลการส่งคืน"
        Me.Label9.Top = 0.75!
        Me.Label9.Width = 1.9375!
        '
        'Label8
        '
        Me.Label8.Border.BottomColor = System.Drawing.Color.Black
        Me.Label8.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Label8.Border.LeftColor = System.Drawing.Color.Black
        Me.Label8.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Label8.Border.RightColor = System.Drawing.Color.Black
        Me.Label8.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Label8.Border.TopColor = System.Drawing.Color.Black
        Me.Label8.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Label8.Height = 0.3125!
        Me.Label8.HyperLink = Nothing
        Me.Label8.Left = 5.3125!
        Me.Label8.Name = "Label8"
        Me.Label8.Style = "ddo-char-set: 0; text-align: center; font-size: 15.75pt; font-family: AngsanaUPC;" & _
            " "
        Me.Label8.Text = "Invoice No."
        Me.Label8.Top = 0.75!
        Me.Label8.Width = 1.1875!
        '
        'txtForm_Name
        '
        Me.txtForm_Name.Border.BottomColor = System.Drawing.Color.Black
        Me.txtForm_Name.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtForm_Name.Border.LeftColor = System.Drawing.Color.Black
        Me.txtForm_Name.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtForm_Name.Border.RightColor = System.Drawing.Color.Black
        Me.txtForm_Name.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtForm_Name.Border.TopColor = System.Drawing.Color.Black
        Me.txtForm_Name.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtForm_Name.DataField = "form_name"
        Me.txtForm_Name.Height = 0.3125!
        Me.txtForm_Name.Left = 0.0625!
        Me.txtForm_Name.Name = "txtForm_Name"
        Me.txtForm_Name.Style = "text-decoration: underline; ddo-char-set: 0; font-weight: normal; font-size: 15.7" & _
            "5pt; font-family: AngsanaUPC; "
        Me.txtForm_Name.Text = Nothing
        Me.txtForm_Name.Top = 0.0!
        Me.txtForm_Name.Width = 8.375!
        '
        'Line2
        '
        Me.Line2.Border.BottomColor = System.Drawing.Color.Black
        Me.Line2.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Line2.Border.LeftColor = System.Drawing.Color.Black
        Me.Line2.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Line2.Border.RightColor = System.Drawing.Color.Black
        Me.Line2.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Line2.Border.TopColor = System.Drawing.Color.Black
        Me.Line2.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Line2.Height = 0.0!
        Me.Line2.Left = 0.0625!
        Me.Line2.LineWeight = 1.0!
        Me.Line2.Name = "Line2"
        Me.Line2.Top = 0.0!
        Me.Line2.Width = 8.375!
        Me.Line2.X1 = 0.0625!
        Me.Line2.X2 = 8.4375!
        Me.Line2.Y1 = 0.0!
        Me.Line2.Y2 = 0.0!
        '
        'txtRemark_docatt
        '
        Me.txtRemark_docatt.Border.BottomColor = System.Drawing.Color.Black
        Me.txtRemark_docatt.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtRemark_docatt.Border.LeftColor = System.Drawing.Color.Black
        Me.txtRemark_docatt.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtRemark_docatt.Border.RightColor = System.Drawing.Color.Black
        Me.txtRemark_docatt.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtRemark_docatt.Border.TopColor = System.Drawing.Color.Black
        Me.txtRemark_docatt.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtRemark_docatt.DataField = "remark_docatt"
        Me.txtRemark_docatt.Height = 0.3125!
        Me.txtRemark_docatt.Left = 6.5!
        Me.txtRemark_docatt.Name = "txtRemark_docatt"
        Me.txtRemark_docatt.Style = "text-align: left; font-weight: normal; font-size: 12pt; font-family: AngsanaUPC; " & _
            ""
        Me.txtRemark_docatt.Text = Nothing
        Me.txtRemark_docatt.Top = 0.0!
        Me.txtRemark_docatt.Width = 1.9375!
        '
        'txtInvoice_No
        '
        Me.txtInvoice_No.Border.BottomColor = System.Drawing.Color.Black
        Me.txtInvoice_No.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtInvoice_No.Border.LeftColor = System.Drawing.Color.Black
        Me.txtInvoice_No.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtInvoice_No.Border.RightColor = System.Drawing.Color.Black
        Me.txtInvoice_No.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtInvoice_No.Border.TopColor = System.Drawing.Color.Black
        Me.txtInvoice_No.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtInvoice_No.DataField = "invh_run_auto"
        Me.txtInvoice_No.Height = 0.3125!
        Me.txtInvoice_No.Left = 5.3125!
        Me.txtInvoice_No.Name = "txtInvoice_No"
        Me.txtInvoice_No.Style = "text-align: left; font-weight: normal; font-size: 12pt; font-family: AngsanaUPC; " & _
            ""
        Me.txtInvoice_No.Text = Nothing
        Me.txtInvoice_No.Top = 0.0!
        Me.txtInvoice_No.Width = 1.1875!
        '
        'txtApprove_Date
        '
        Me.txtApprove_Date.Border.BottomColor = System.Drawing.Color.Black
        Me.txtApprove_Date.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtApprove_Date.Border.LeftColor = System.Drawing.Color.Black
        Me.txtApprove_Date.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtApprove_Date.Border.RightColor = System.Drawing.Color.Black
        Me.txtApprove_Date.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtApprove_Date.Border.TopColor = System.Drawing.Color.Black
        Me.txtApprove_Date.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtApprove_Date.DataField = "approve_date"
        Me.txtApprove_Date.Height = 0.3125!
        Me.txtApprove_Date.Left = 4.5!
        Me.txtApprove_Date.Name = "txtApprove_Date"
        Me.txtApprove_Date.Style = "text-align: center; font-weight: normal; font-size: 12pt; font-family: AngsanaUPC" & _
            "; "
        Me.txtApprove_Date.Text = Nothing
        Me.txtApprove_Date.Top = 0.0!
        Me.txtApprove_Date.Width = 0.8125!
        '
        'txtReference_Code2
        '
        Me.txtReference_Code2.Border.BottomColor = System.Drawing.Color.Black
        Me.txtReference_Code2.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtReference_Code2.Border.LeftColor = System.Drawing.Color.Black
        Me.txtReference_Code2.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtReference_Code2.Border.RightColor = System.Drawing.Color.Black
        Me.txtReference_Code2.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtReference_Code2.Border.TopColor = System.Drawing.Color.Black
        Me.txtReference_Code2.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtReference_Code2.DataField = "reference_code2"
        Me.txtReference_Code2.Height = 0.3125!
        Me.txtReference_Code2.Left = 3.5!
        Me.txtReference_Code2.Name = "txtReference_Code2"
        Me.txtReference_Code2.Style = "text-align: center; font-weight: normal; font-size: 12pt; font-family: AngsanaUPC" & _
            "; "
        Me.txtReference_Code2.Text = Nothing
        Me.txtReference_Code2.Top = 0.0!
        Me.txtReference_Code2.Width = 1.0!
        '
        'txtIndex
        '
        Me.txtIndex.Border.BottomColor = System.Drawing.Color.Black
        Me.txtIndex.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtIndex.Border.LeftColor = System.Drawing.Color.Black
        Me.txtIndex.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtIndex.Border.RightColor = System.Drawing.Color.Black
        Me.txtIndex.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtIndex.Border.TopColor = System.Drawing.Color.Black
        Me.txtIndex.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtIndex.Height = 0.3125!
        Me.txtIndex.Left = 0.0625!
        Me.txtIndex.Name = "txtIndex"
        Me.txtIndex.Style = "text-align: center; font-weight: normal; font-size: 12pt; font-family: AngsanaUPC" & _
            "; "
        Me.txtIndex.Text = Nothing
        Me.txtIndex.Top = 0.0!
        Me.txtIndex.Width = 0.375!
        '
        'txtComapny_Name
        '
        Me.txtComapny_Name.Border.BottomColor = System.Drawing.Color.Black
        Me.txtComapny_Name.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtComapny_Name.Border.LeftColor = System.Drawing.Color.Black
        Me.txtComapny_Name.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtComapny_Name.Border.RightColor = System.Drawing.Color.Black
        Me.txtComapny_Name.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtComapny_Name.Border.TopColor = System.Drawing.Color.Black
        Me.txtComapny_Name.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtComapny_Name.DataField = "company_name"
        Me.txtComapny_Name.Height = 0.3125!
        Me.txtComapny_Name.Left = 0.4375!
        Me.txtComapny_Name.Name = "txtComapny_Name"
        Me.txtComapny_Name.Style = "text-align: left; font-weight: normal; font-size: 12pt; font-family: AngsanaUPC; " & _
            ""
        Me.txtComapny_Name.Text = Nothing
        Me.txtComapny_Name.Top = 0.0!
        Me.txtComapny_Name.Width = 3.0625!
        '
        'ReportInfo1
        '
        Me.ReportInfo1.Border.BottomColor = System.Drawing.Color.Black
        Me.ReportInfo1.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.ReportInfo1.Border.LeftColor = System.Drawing.Color.Black
        Me.ReportInfo1.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.ReportInfo1.Border.RightColor = System.Drawing.Color.Black
        Me.ReportInfo1.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.ReportInfo1.Border.TopColor = System.Drawing.Color.Black
        Me.ReportInfo1.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.ReportInfo1.FormatString = "Page {PageNumber} of {PageCount}"
        Me.ReportInfo1.Height = 0.3125!
        Me.ReportInfo1.Left = 4.0!
        Me.ReportInfo1.Name = "ReportInfo1"
        Me.ReportInfo1.Style = "ddo-char-set: 0; text-align: right; font-size: 15.75pt; font-family: AngsanaUPC; " & _
            ""
        Me.ReportInfo1.SummaryGroup = "GroupHeader1"
        Me.ReportInfo1.SummaryRunning = DataDynamics.ActiveReports.SummaryRunning.Group
        Me.ReportInfo1.Top = 0.0!
        Me.ReportInfo1.Width = 4.4375!
        '
        'rptF04_Report_07
        '
        Me.MasterReport = False
        Me.PageSettings.Margins.Bottom = 0.1!
        Me.PageSettings.Margins.Left = 0.1!
        Me.PageSettings.Margins.Right = 0.1!
        Me.PageSettings.Margins.Top = 0.1!
        Me.PageSettings.PaperHeight = 11.69!
        Me.PageSettings.PaperWidth = 8.27!
        Me.PrintWidth = 8.5!
        Me.Sections.Add(Me.PageHeader1)
        Me.Sections.Add(Me.GroupHeader1)
        Me.Sections.Add(Me.Detail1)
        Me.Sections.Add(Me.GroupFooter1)
        Me.Sections.Add(Me.PageFooter1)
        Me.StyleSheet.Add(New DDCssLib.StyleSheetRule("font-family: Arial; font-style: normal; text-decoration: none; font-weight: norma" & _
                    "l; font-size: 10pt; color: Black; ", "Normal"))
        Me.StyleSheet.Add(New DDCssLib.StyleSheetRule("font-size: 16pt; font-weight: bold; ", "Heading1", "Normal"))
        Me.StyleSheet.Add(New DDCssLib.StyleSheetRule("font-family: Times New Roman; font-size: 14pt; font-weight: bold; font-style: ita" & _
                    "lic; ", "Heading2", "Normal"))
        Me.StyleSheet.Add(New DDCssLib.StyleSheetRule("font-size: 13pt; font-weight: bold; ", "Heading3", "Normal"))
        CType(Me.Label1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPrintDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblSite_ID, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtForm_Name, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRemark_docatt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtInvoice_No, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtApprove_Date, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtReference_Code2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtIndex, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtComapny_Name, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ReportInfo1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub
    Friend WithEvents GroupHeader1 As DataDynamics.ActiveReports.GroupHeader
    Friend WithEvents GroupFooter1 As DataDynamics.ActiveReports.GroupFooter
    Friend WithEvents Label1 As DataDynamics.ActiveReports.Label
    Friend WithEvents txtPrintDate As DataDynamics.ActiveReports.Label
    Friend WithEvents lblSite_ID As DataDynamics.ActiveReports.Label
    Friend WithEvents Label4 As DataDynamics.ActiveReports.Label
    Friend WithEvents Label5 As DataDynamics.ActiveReports.Label
    Friend WithEvents Label6 As DataDynamics.ActiveReports.Label
    Friend WithEvents Label7 As DataDynamics.ActiveReports.Label
    Friend WithEvents Line1 As DataDynamics.ActiveReports.Line
    Friend WithEvents Label9 As DataDynamics.ActiveReports.Label
    Friend WithEvents Label8 As DataDynamics.ActiveReports.Label
    Friend WithEvents txtForm_Name As DataDynamics.ActiveReports.TextBox
    Friend WithEvents Line2 As DataDynamics.ActiveReports.Line
    Friend WithEvents txtRemark_docatt As DataDynamics.ActiveReports.TextBox
    Friend WithEvents txtInvoice_No As DataDynamics.ActiveReports.TextBox
    Friend WithEvents txtApprove_Date As DataDynamics.ActiveReports.TextBox
    Friend WithEvents txtReference_Code2 As DataDynamics.ActiveReports.TextBox
    Friend WithEvents txtIndex As DataDynamics.ActiveReports.TextBox
    Friend WithEvents txtComapny_Name As DataDynamics.ActiveReports.TextBox
    Friend WithEvents ReportInfo1 As DataDynamics.ActiveReports.ReportInfo
End Class 
