<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class rptF04_Report_01 
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
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(rptF04_Report_01))
        Me.PageHeader1 = New DataDynamics.ActiveReports.PageHeader
        Me.Label2 = New DataDynamics.ActiveReports.Label
        Me.Label1 = New DataDynamics.ActiveReports.Label
        Me.txtPrintDate = New DataDynamics.ActiveReports.Label
        Me.Label3 = New DataDynamics.ActiveReports.Label
        Me.Label4 = New DataDynamics.ActiveReports.Label
        Me.Label5 = New DataDynamics.ActiveReports.Label
        Me.Label6 = New DataDynamics.ActiveReports.Label
        Me.Label7 = New DataDynamics.ActiveReports.Label
        Me.Line1 = New DataDynamics.ActiveReports.Line
        Me.txtFrom = New DataDynamics.ActiveReports.Label
        Me.Detail1 = New DataDynamics.ActiveReports.Detail
        Me.txtIndex = New DataDynamics.ActiveReports.TextBox
        Me.txtComapny_Name = New DataDynamics.ActiveReports.TextBox
        Me.txtReference_Code2 = New DataDynamics.ActiveReports.TextBox
        Me.txtApprove_Date = New DataDynamics.ActiveReports.TextBox
        Me.Barcode1 = New DataDynamics.ActiveReports.Barcode
        Me.PageFooter1 = New DataDynamics.ActiveReports.PageFooter
        Me.ReportInfo1 = New DataDynamics.ActiveReports.ReportInfo
        Me.GroupHeader1 = New DataDynamics.ActiveReports.GroupHeader
        Me.txtForm_Name = New DataDynamics.ActiveReports.TextBox
        Me.Line3 = New DataDynamics.ActiveReports.Line
        Me.GroupFooter1 = New DataDynamics.ActiveReports.GroupFooter
        CType(Me.Label2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPrintDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFrom, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtIndex, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtComapny_Name, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtReference_Code2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtApprove_Date, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ReportInfo1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtForm_Name, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'PageHeader1
        '
        Me.PageHeader1.Controls.AddRange(New DataDynamics.ActiveReports.ARControl() {Me.Label2, Me.Label1, Me.txtPrintDate, Me.Label3, Me.Label4, Me.Label5, Me.Label6, Me.Label7, Me.Line1, Me.txtFrom})
        Me.PageHeader1.Height = 1.385417!
        Me.PageHeader1.Name = "PageHeader1"
        '
        'Label2
        '
        Me.Label2.Border.BottomColor = System.Drawing.Color.Black
        Me.Label2.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Label2.Border.LeftColor = System.Drawing.Color.Black
        Me.Label2.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Label2.Border.RightColor = System.Drawing.Color.Black
        Me.Label2.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Label2.Border.TopColor = System.Drawing.Color.Black
        Me.Label2.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Label2.Height = 0.25!
        Me.Label2.HyperLink = Nothing
        Me.Label2.Left = 0.4375!
        Me.Label2.Name = "Label2"
        Me.Label2.Style = "text-align: center; font-size: 14pt; font-family: AngsanaUPC; "
        Me.Label2.Text = "เลขที่คำร้อง"
        Me.Label2.Top = 1.125!
        Me.Label2.Width = 2.5625!
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
        Me.Label1.Style = "text-align: center; font-weight: bold; font-size: 16pt; font-family: AngsanaUPC; " & _
            ""
        Me.Label1.Text = "รายงานการอนุมัติหนังสือรับรองแหล่งกำเนิดสินค้าระบบ EDI"
        Me.Label1.Top = 0.0625!
        Me.Label1.Width = 7.875!
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
        Me.txtPrintDate.Height = 0.25!
        Me.txtPrintDate.HyperLink = Nothing
        Me.txtPrintDate.Left = 5.0!
        Me.txtPrintDate.Name = "txtPrintDate"
        Me.txtPrintDate.Style = "text-align: right; font-size: 14pt; font-family: AngsanaUPC; "
        Me.txtPrintDate.Text = "วันที่พิมพ์"
        Me.txtPrintDate.Top = 0.8125!
        Me.txtPrintDate.Width = 2.9375!
        '
        'Label3
        '
        Me.Label3.Border.BottomColor = System.Drawing.Color.Black
        Me.Label3.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Label3.Border.LeftColor = System.Drawing.Color.Black
        Me.Label3.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Label3.Border.RightColor = System.Drawing.Color.Black
        Me.Label3.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Label3.Border.TopColor = System.Drawing.Color.Black
        Me.Label3.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Label3.Height = 0.25!
        Me.Label3.HyperLink = Nothing
        Me.Label3.Left = 5.0!
        Me.Label3.Name = "Label3"
        Me.Label3.Style = "text-align: right; font-size: 14pt; font-family: AngsanaUPC; "
        Me.Label3.Text = "สถานที่ : ST-001"
        Me.Label3.Top = 0.5!
        Me.Label3.Width = 2.9375!
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
        Me.Label4.Height = 0.25!
        Me.Label4.HyperLink = Nothing
        Me.Label4.Left = 0.0625!
        Me.Label4.Name = "Label4"
        Me.Label4.Style = "text-align: center; font-size: 14pt; font-family: AngsanaUPC; "
        Me.Label4.Text = "ลำดับ"
        Me.Label4.Top = 1.125!
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
        Me.Label5.Height = 0.25!
        Me.Label5.HyperLink = Nothing
        Me.Label5.Left = 3.0!
        Me.Label5.Name = "Label5"
        Me.Label5.Style = "text-align: center; font-size: 14pt; font-family: AngsanaUPC; "
        Me.Label5.Text = "บริษัท"
        Me.Label5.Top = 1.125!
        Me.Label5.Width = 3.25!
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
        Me.Label6.Height = 0.25!
        Me.Label6.HyperLink = Nothing
        Me.Label6.Left = 6.25!
        Me.Label6.Name = "Label6"
        Me.Label6.Style = "text-align: center; font-size: 14pt; font-family: AngsanaUPC; "
        Me.Label6.Text = "เลขที่หนังสือ"
        Me.Label6.Top = 1.125!
        Me.Label6.Width = 0.875!
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
        Me.Label7.Height = 0.25!
        Me.Label7.HyperLink = Nothing
        Me.Label7.Left = 7.125!
        Me.Label7.Name = "Label7"
        Me.Label7.Style = "text-align: center; font-size: 14pt; font-family: AngsanaUPC; "
        Me.Label7.Text = "วันที่อนุมัติ"
        Me.Label7.Top = 1.125!
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
        Me.Line1.Left = 0.0!
        Me.Line1.LineWeight = 1.0!
        Me.Line1.Name = "Line1"
        Me.Line1.Top = 1.125!
        Me.Line1.Width = 8.0!
        Me.Line1.X1 = 0.0!
        Me.Line1.X2 = 8.0!
        Me.Line1.Y1 = 1.125!
        Me.Line1.Y2 = 1.125!
        '
        'txtFrom
        '
        Me.txtFrom.Border.BottomColor = System.Drawing.Color.Black
        Me.txtFrom.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtFrom.Border.LeftColor = System.Drawing.Color.Black
        Me.txtFrom.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtFrom.Border.RightColor = System.Drawing.Color.Black
        Me.txtFrom.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtFrom.Border.TopColor = System.Drawing.Color.Black
        Me.txtFrom.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtFrom.Height = 0.25!
        Me.txtFrom.HyperLink = Nothing
        Me.txtFrom.Left = 0.0625!
        Me.txtFrom.Name = "txtFrom"
        Me.txtFrom.Style = "text-align: left; font-size: 14pt; font-family: AngsanaUPC; "
        Me.txtFrom.Text = "วันที่พิมพ์"
        Me.txtFrom.Top = 0.8125!
        Me.txtFrom.Width = 2.9375!
        '
        'Detail1
        '
        Me.Detail1.ColumnSpacing = 0.0!
        Me.Detail1.Controls.AddRange(New DataDynamics.ActiveReports.ARControl() {Me.txtIndex, Me.txtComapny_Name, Me.txtReference_Code2, Me.txtApprove_Date, Me.Barcode1})
        Me.Detail1.Height = 0.5!
        Me.Detail1.Name = "Detail1"
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
        Me.txtIndex.Height = 0.25!
        Me.txtIndex.Left = 0.0!
        Me.txtIndex.Name = "txtIndex"
        Me.txtIndex.Style = "text-align: right; font-weight: normal; font-size: 12pt; font-family: AngsanaUPC;" & _
            " "
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
        Me.txtComapny_Name.Height = 0.25!
        Me.txtComapny_Name.Left = 3.0!
        Me.txtComapny_Name.Name = "txtComapny_Name"
        Me.txtComapny_Name.Style = "text-align: left; font-weight: normal; font-size: 12pt; font-family: AngsanaUPC; " & _
            ""
        Me.txtComapny_Name.Text = Nothing
        Me.txtComapny_Name.Top = 0.0!
        Me.txtComapny_Name.Width = 3.1875!
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
        Me.txtReference_Code2.Height = 0.25!
        Me.txtReference_Code2.Left = 6.25!
        Me.txtReference_Code2.Name = "txtReference_Code2"
        Me.txtReference_Code2.Style = "text-align: center; font-weight: normal; font-size: 12pt; font-family: AngsanaUPC" & _
            "; "
        Me.txtReference_Code2.Text = Nothing
        Me.txtReference_Code2.Top = 0.0!
        Me.txtReference_Code2.Width = 0.875!
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
        Me.txtApprove_Date.Height = 0.25!
        Me.txtApprove_Date.Left = 7.125!
        Me.txtApprove_Date.Name = "txtApprove_Date"
        Me.txtApprove_Date.Style = "text-align: center; font-weight: normal; font-size: 12pt; font-family: AngsanaUPC" & _
            "; "
        Me.txtApprove_Date.Text = Nothing
        Me.txtApprove_Date.Top = 0.0!
        Me.txtApprove_Date.Width = 0.8125!
        '
        'Barcode1
        '
        Me.Barcode1.Border.BottomColor = System.Drawing.Color.Black
        Me.Barcode1.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Barcode1.Border.LeftColor = System.Drawing.Color.Black
        Me.Barcode1.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Barcode1.Border.RightColor = System.Drawing.Color.Black
        Me.Barcode1.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Barcode1.Border.TopColor = System.Drawing.Color.Black
        Me.Barcode1.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Barcode1.CaptionPosition = DataDynamics.ActiveReports.BarCodeCaptionPosition.Below
        Me.Barcode1.DataField = "invh_run_auto"
        Me.Barcode1.Font = New System.Drawing.Font("AngsanaUPC", 8.0!)
        Me.Barcode1.Height = 0.5!
        Me.Barcode1.Left = 0.5!
        Me.Barcode1.Name = "Barcode1"
        Me.Barcode1.Top = 0.0!
        Me.Barcode1.Width = 2.375!
        '
        'PageFooter1
        '
        Me.PageFooter1.Controls.AddRange(New DataDynamics.ActiveReports.ARControl() {Me.ReportInfo1})
        Me.PageFooter1.Height = 0.25!
        Me.PageFooter1.Name = "PageFooter1"
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
        Me.ReportInfo1.Height = 0.25!
        Me.ReportInfo1.Left = 6.0!
        Me.ReportInfo1.Name = "ReportInfo1"
        Me.ReportInfo1.Style = "text-align: right; font-size: 12pt; font-family: AngsanaUPC; "
        Me.ReportInfo1.Top = 0.0!
        Me.ReportInfo1.Width = 1.9375!
        '
        'GroupHeader1
        '
        Me.GroupHeader1.Controls.AddRange(New DataDynamics.ActiveReports.ARControl() {Me.txtForm_Name, Me.Line3})
        Me.GroupHeader1.DataField = "form_name"
        Me.GroupHeader1.Height = 0.25!
        Me.GroupHeader1.KeepTogether = True
        Me.GroupHeader1.Name = "GroupHeader1"
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
        Me.txtForm_Name.Height = 0.25!
        Me.txtForm_Name.Left = 0.0625!
        Me.txtForm_Name.Name = "txtForm_Name"
        Me.txtForm_Name.Style = "text-decoration: underline; font-weight: normal; font-size: 12pt; font-family: An" & _
            "gsanaUPC; "
        Me.txtForm_Name.Text = Nothing
        Me.txtForm_Name.Top = 0.0!
        Me.txtForm_Name.Width = 2.9375!
        '
        'Line3
        '
        Me.Line3.Border.BottomColor = System.Drawing.Color.Black
        Me.Line3.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Line3.Border.LeftColor = System.Drawing.Color.Black
        Me.Line3.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Line3.Border.RightColor = System.Drawing.Color.Black
        Me.Line3.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Line3.Border.TopColor = System.Drawing.Color.Black
        Me.Line3.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Line3.Height = 0.0!
        Me.Line3.Left = 0.0!
        Me.Line3.LineWeight = 1.0!
        Me.Line3.Name = "Line3"
        Me.Line3.Top = 0.0!
        Me.Line3.Width = 8.0!
        Me.Line3.X1 = 0.0!
        Me.Line3.X2 = 8.0!
        Me.Line3.Y1 = 0.0!
        Me.Line3.Y2 = 0.0!
        '
        'GroupFooter1
        '
        Me.GroupFooter1.Height = 0.0!
        Me.GroupFooter1.Name = "GroupFooter1"
        '
        'rptF04_Report_01
        '
        Me.MasterReport = False
        Me.PageSettings.Margins.Bottom = 0.1!
        Me.PageSettings.Margins.Left = 0.1!
        Me.PageSettings.Margins.Right = 0.1!
        Me.PageSettings.Margins.Top = 0.1!
        Me.PageSettings.PaperHeight = 11.69!
        Me.PageSettings.PaperWidth = 8.27!
        Me.PrintWidth = 8.0!
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
        CType(Me.Label2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPrintDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFrom, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtIndex, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtComapny_Name, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtReference_Code2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtApprove_Date, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ReportInfo1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtForm_Name, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub
    Friend WithEvents GroupHeader1 As DataDynamics.ActiveReports.GroupHeader
    Friend WithEvents GroupFooter1 As DataDynamics.ActiveReports.GroupFooter
    Friend WithEvents Label1 As DataDynamics.ActiveReports.Label
    Friend WithEvents txtPrintDate As DataDynamics.ActiveReports.Label
    Friend WithEvents Label3 As DataDynamics.ActiveReports.Label
    Friend WithEvents Label4 As DataDynamics.ActiveReports.Label
    Friend WithEvents Label5 As DataDynamics.ActiveReports.Label
    Friend WithEvents Label6 As DataDynamics.ActiveReports.Label
    Friend WithEvents Label7 As DataDynamics.ActiveReports.Label
    Friend WithEvents Line1 As DataDynamics.ActiveReports.Line
    Friend WithEvents Label2 As DataDynamics.ActiveReports.Label
    Friend WithEvents txtForm_Name As DataDynamics.ActiveReports.TextBox
    Friend WithEvents Line3 As DataDynamics.ActiveReports.Line
    Friend WithEvents txtIndex As DataDynamics.ActiveReports.TextBox
    Friend WithEvents txtComapny_Name As DataDynamics.ActiveReports.TextBox
    Friend WithEvents txtReference_Code2 As DataDynamics.ActiveReports.TextBox
    Friend WithEvents txtApprove_Date As DataDynamics.ActiveReports.TextBox
    Friend WithEvents Barcode1 As DataDynamics.ActiveReports.Barcode
    Friend WithEvents ReportInfo1 As DataDynamics.ActiveReports.ReportInfo
    Public WithEvents txtFrom As DataDynamics.ActiveReports.Label
End Class
