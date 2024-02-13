<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class rpt_Receipt_Short
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(rpt_Receipt_Short))
        Me.Detail1 = New DataDynamics.ActiveReports.Detail
        Me.txtNo = New DataDynamics.ActiveReports.TextBox
        Me.txttotal_set = New DataDynamics.ActiveReports.TextBox
        Me.txtamt = New DataDynamics.ActiveReports.TextBox
        Me.txtset_price = New DataDynamics.ActiveReports.TextBox
        Me.txtreference_code2 = New DataDynamics.ActiveReports.TextBox
        Me.ReportHeader1 = New DataDynamics.ActiveReports.ReportHeader
        Me.ReportFooter1 = New DataDynamics.ActiveReports.ReportFooter
        Me.PageHeader1 = New DataDynamics.ActiveReports.PageHeader
        Me.lblDate = New DataDynamics.ActiveReports.Label
        Me.lblMonth = New DataDynamics.ActiveReports.Label
        Me.lblYear = New DataDynamics.ActiveReports.Label
        Me.lblTime = New DataDynamics.ActiveReports.Label
        Me.lblReceiptNo = New DataDynamics.ActiveReports.Label
        Me.txtCompanyName = New DataDynamics.ActiveReports.TextBox
        Me.ReportInfo1 = New DataDynamics.ActiveReports.ReportInfo
        Me.PageFooter1 = New DataDynamics.ActiveReports.PageFooter
        Me.txtDisplayName = New DataDynamics.ActiveReports.Label
        Me.lblTextAmount = New DataDynamics.ActiveReports.Label
        Me.lblTotalAmount = New DataDynamics.ActiveReports.TextBox
        CType(Me.txtNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txttotal_set, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtamt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtset_price, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtreference_code2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblMonth, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblYear, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblReceiptNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCompanyName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ReportInfo1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDisplayName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTextAmount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTotalAmount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'Detail1
        '
        Me.Detail1.ColumnSpacing = 0.0!
        Me.Detail1.Controls.AddRange(New DataDynamics.ActiveReports.ARControl() {Me.txtNo, Me.txttotal_set, Me.txtamt, Me.txtset_price, Me.txtreference_code2})
        Me.Detail1.Height = 0.25!
        Me.Detail1.KeepTogether = True
        Me.Detail1.Name = "Detail1"
        '
        'txtNo
        '
        Me.txtNo.Border.BottomColor = System.Drawing.Color.Black
        Me.txtNo.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtNo.Border.LeftColor = System.Drawing.Color.Black
        Me.txtNo.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtNo.Border.RightColor = System.Drawing.Color.Black
        Me.txtNo.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtNo.Border.TopColor = System.Drawing.Color.Black
        Me.txtNo.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtNo.Height = 0.25!
        Me.txtNo.Left = 0.1!
        Me.txtNo.Name = "txtNo"
        Me.txtNo.Style = "ddo-char-set: 1; text-align: center; font-size: 14pt; font-family: AngsanaUPC; ve" & _
            "rtical-align: middle; "
        Me.txtNo.Text = Nothing
        Me.txtNo.Top = 0.0!
        Me.txtNo.Width = 0.5!
        '
        'txttotal_set
        '
        Me.txttotal_set.Border.BottomColor = System.Drawing.Color.Black
        Me.txttotal_set.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txttotal_set.Border.LeftColor = System.Drawing.Color.Black
        Me.txttotal_set.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txttotal_set.Border.RightColor = System.Drawing.Color.Black
        Me.txttotal_set.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txttotal_set.Border.TopColor = System.Drawing.Color.Black
        Me.txttotal_set.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txttotal_set.DataField = "total_set"
        Me.txttotal_set.Height = 0.25!
        Me.txttotal_set.Left = 4.625!
        Me.txttotal_set.Name = "txttotal_set"
        Me.txttotal_set.Style = "ddo-char-set: 1; text-align: center; font-size: 14pt; font-family: AngsanaUPC; ve" & _
            "rtical-align: middle; "
        Me.txttotal_set.Text = Nothing
        Me.txttotal_set.Top = 0.0!
        Me.txttotal_set.Width = 0.8125!
        '
        'txtamt
        '
        Me.txtamt.Border.BottomColor = System.Drawing.Color.Black
        Me.txtamt.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtamt.Border.LeftColor = System.Drawing.Color.Black
        Me.txtamt.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtamt.Border.RightColor = System.Drawing.Color.Black
        Me.txtamt.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtamt.Border.TopColor = System.Drawing.Color.Black
        Me.txtamt.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtamt.DataField = "amt"
        Me.txtamt.Height = 0.25!
        Me.txtamt.Left = 6.5!
        Me.txtamt.Name = "txtamt"
        Me.txtamt.Style = "ddo-char-set: 1; text-align: right; font-size: 14pt; font-family: AngsanaUPC; ver" & _
            "tical-align: middle; "
        Me.txtamt.Text = Nothing
        Me.txtamt.Top = 0.0!
        Me.txtamt.Width = 1.1875!
        '
        'txtset_price
        '
        Me.txtset_price.Border.BottomColor = System.Drawing.Color.Black
        Me.txtset_price.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtset_price.Border.LeftColor = System.Drawing.Color.Black
        Me.txtset_price.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtset_price.Border.RightColor = System.Drawing.Color.Black
        Me.txtset_price.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtset_price.Border.TopColor = System.Drawing.Color.Black
        Me.txtset_price.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtset_price.DataField = "set_price"
        Me.txtset_price.Height = 0.25!
        Me.txtset_price.Left = 5.5625!
        Me.txtset_price.Name = "txtset_price"
        Me.txtset_price.Style = "ddo-char-set: 1; text-align: right; font-size: 14pt; font-family: AngsanaUPC; ver" & _
            "tical-align: middle; "
        Me.txtset_price.Text = Nothing
        Me.txtset_price.Top = 0.0!
        Me.txtset_price.Width = 0.6875!
        '
        'txtreference_code2
        '
        Me.txtreference_code2.Border.BottomColor = System.Drawing.Color.Black
        Me.txtreference_code2.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtreference_code2.Border.LeftColor = System.Drawing.Color.Black
        Me.txtreference_code2.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtreference_code2.Border.RightColor = System.Drawing.Color.Black
        Me.txtreference_code2.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtreference_code2.Border.TopColor = System.Drawing.Color.Black
        Me.txtreference_code2.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtreference_code2.DataField = "reference_code2"
        Me.txtreference_code2.Height = 0.25!
        Me.txtreference_code2.Left = 0.875!
        Me.txtreference_code2.Name = "txtreference_code2"
        Me.txtreference_code2.Style = "ddo-char-set: 1; font-size: 14pt; font-family: AngsanaUPC; vertical-align: middle" & _
            "; "
        Me.txtreference_code2.Text = Nothing
        Me.txtreference_code2.Top = 0.0!
        Me.txtreference_code2.Width = 3.75!
        '
        'ReportHeader1
        '
        Me.ReportHeader1.Height = 0.0!
        Me.ReportHeader1.Name = "ReportHeader1"
        '
        'ReportFooter1
        '
        Me.ReportFooter1.Height = 0.0!
        Me.ReportFooter1.Name = "ReportFooter1"
        '
        'PageHeader1
        '
        Me.PageHeader1.CanGrow = False
        Me.PageHeader1.Controls.AddRange(New DataDynamics.ActiveReports.ARControl() {Me.lblDate, Me.lblMonth, Me.lblYear, Me.lblTime, Me.lblReceiptNo, Me.txtCompanyName, Me.ReportInfo1})
        Me.PageHeader1.Height = 1.854167!
        Me.PageHeader1.Name = "PageHeader1"
        '
        'lblDate
        '
        Me.lblDate.Border.BottomColor = System.Drawing.Color.Black
        Me.lblDate.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.lblDate.Border.LeftColor = System.Drawing.Color.Black
        Me.lblDate.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.lblDate.Border.RightColor = System.Drawing.Color.Black
        Me.lblDate.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.lblDate.Border.TopColor = System.Drawing.Color.Black
        Me.lblDate.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.lblDate.Height = 0.25!
        Me.lblDate.HyperLink = Nothing
        Me.lblDate.Left = 4.6875!
        Me.lblDate.Name = "lblDate"
        Me.lblDate.Style = "ddo-char-set: 1; text-align: center; font-size: 14pt; font-family: AngsanaUPC; ve" & _
            "rtical-align: top; "
        Me.lblDate.Text = "lblDate"
        Me.lblDate.Top = 1.0!
        Me.lblDate.Width = 0.5625!
        '
        'lblMonth
        '
        Me.lblMonth.Border.BottomColor = System.Drawing.Color.Black
        Me.lblMonth.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.lblMonth.Border.LeftColor = System.Drawing.Color.Black
        Me.lblMonth.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.lblMonth.Border.RightColor = System.Drawing.Color.Black
        Me.lblMonth.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.lblMonth.Border.TopColor = System.Drawing.Color.Black
        Me.lblMonth.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.lblMonth.Height = 0.25!
        Me.lblMonth.HyperLink = Nothing
        Me.lblMonth.Left = 5.6875!
        Me.lblMonth.Name = "lblMonth"
        Me.lblMonth.Style = "ddo-char-set: 1; text-align: center; font-size: 14pt; font-family: AngsanaUPC; ve" & _
            "rtical-align: top; "
        Me.lblMonth.Text = "lblMonth"
        Me.lblMonth.Top = 1.0!
        Me.lblMonth.Width = 1.0!
        '
        'lblYear
        '
        Me.lblYear.Border.BottomColor = System.Drawing.Color.Black
        Me.lblYear.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.lblYear.Border.LeftColor = System.Drawing.Color.Black
        Me.lblYear.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.lblYear.Border.RightColor = System.Drawing.Color.Black
        Me.lblYear.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.lblYear.Border.TopColor = System.Drawing.Color.Black
        Me.lblYear.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.lblYear.Height = 0.25!
        Me.lblYear.HyperLink = Nothing
        Me.lblYear.Left = 7.25!
        Me.lblYear.Name = "lblYear"
        Me.lblYear.Style = "ddo-char-set: 1; text-align: left; font-size: 14pt; font-family: AngsanaUPC; vert" & _
            "ical-align: top; "
        Me.lblYear.Text = "lblYear"
        Me.lblYear.Top = 1.0!
        Me.lblYear.Width = 0.5!
        '
        'lblTime
        '
        Me.lblTime.Border.BottomColor = System.Drawing.Color.Black
        Me.lblTime.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.lblTime.Border.LeftColor = System.Drawing.Color.Black
        Me.lblTime.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.lblTime.Border.RightColor = System.Drawing.Color.Black
        Me.lblTime.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.lblTime.Border.TopColor = System.Drawing.Color.Black
        Me.lblTime.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.lblTime.Height = 0.25!
        Me.lblTime.HyperLink = Nothing
        Me.lblTime.Left = 7.1875!
        Me.lblTime.Name = "lblTime"
        Me.lblTime.Style = "ddo-char-set: 1; text-align: left; font-size: 14pt; font-family: AngsanaUPC; vert" & _
            "ical-align: top; "
        Me.lblTime.Text = "lblTime"
        Me.lblTime.Top = 0.625!
        Me.lblTime.Width = 0.875!
        '
        'lblReceiptNo
        '
        Me.lblReceiptNo.Border.BottomColor = System.Drawing.Color.Black
        Me.lblReceiptNo.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.lblReceiptNo.Border.LeftColor = System.Drawing.Color.Black
        Me.lblReceiptNo.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.lblReceiptNo.Border.RightColor = System.Drawing.Color.Black
        Me.lblReceiptNo.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.lblReceiptNo.Border.TopColor = System.Drawing.Color.Black
        Me.lblReceiptNo.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.lblReceiptNo.DataField = "bill_no"
        Me.lblReceiptNo.Height = 0.25!
        Me.lblReceiptNo.HyperLink = Nothing
        Me.lblReceiptNo.Left = 6.75!
        Me.lblReceiptNo.Name = "lblReceiptNo"
        Me.lblReceiptNo.Style = "ddo-char-set: 1; text-align: left; font-size: 14pt; font-family: AngsanaUPC; vert" & _
            "ical-align: top; "
        Me.lblReceiptNo.Text = "lblReceiptNo"
        Me.lblReceiptNo.Top = 0.3333333!
        Me.lblReceiptNo.Width = 1.3125!
        '
        'txtCompanyName
        '
        Me.txtCompanyName.Border.BottomColor = System.Drawing.Color.Black
        Me.txtCompanyName.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtCompanyName.Border.LeftColor = System.Drawing.Color.Black
        Me.txtCompanyName.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtCompanyName.Border.RightColor = System.Drawing.Color.Black
        Me.txtCompanyName.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtCompanyName.Border.TopColor = System.Drawing.Color.Black
        Me.txtCompanyName.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtCompanyName.Height = 0.3125!
        Me.txtCompanyName.Left = 1.0!
        Me.txtCompanyName.Name = "txtCompanyName"
        Me.txtCompanyName.Style = "ddo-char-set: 1; font-weight: bold; font-size: 14pt; font-family: AngsanaUPC; "
        Me.txtCompanyName.Text = Nothing
        Me.txtCompanyName.Top = 1.291667!
        Me.txtCompanyName.Width = 7.0!
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
        Me.ReportInfo1.CanGrow = False
        Me.ReportInfo1.FormatString = "แผ่นที่ {PageNumber} / {PageCount}"
        Me.ReportInfo1.Height = 0.25!
        Me.ReportInfo1.Left = 6.75!
        Me.ReportInfo1.Name = "ReportInfo1"
        Me.ReportInfo1.Style = "ddo-char-set: 1; text-align: right; font-size: 12pt; font-family: BrowalliaUPC; "
        Me.ReportInfo1.Top = 0.125!
        Me.ReportInfo1.Width = 1.3125!
        '
        'PageFooter1
        '
        Me.PageFooter1.CanGrow = False
        Me.PageFooter1.Controls.AddRange(New DataDynamics.ActiveReports.ARControl() {Me.txtDisplayName, Me.lblTextAmount, Me.lblTotalAmount})
        Me.PageFooter1.Height = 1.25!
        Me.PageFooter1.Name = "PageFooter1"
        '
        'txtDisplayName
        '
        Me.txtDisplayName.Border.BottomColor = System.Drawing.Color.Black
        Me.txtDisplayName.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtDisplayName.Border.LeftColor = System.Drawing.Color.Black
        Me.txtDisplayName.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtDisplayName.Border.RightColor = System.Drawing.Color.Black
        Me.txtDisplayName.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtDisplayName.Border.TopColor = System.Drawing.Color.Black
        Me.txtDisplayName.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtDisplayName.DataField = "receipt_by"
        Me.txtDisplayName.Height = 0.313!
        Me.txtDisplayName.HyperLink = Nothing
        Me.txtDisplayName.Left = 5.0625!
        Me.txtDisplayName.Name = "txtDisplayName"
        Me.txtDisplayName.Style = "ddo-char-set: 1; text-align: center; font-size: 14pt; font-family: AngsanaUPC; ve" & _
            "rtical-align: bottom; "
        Me.txtDisplayName.Text = ""
        Me.txtDisplayName.Top = 0.84375!
        Me.txtDisplayName.Width = 2.2!
        '
        'lblTextAmount
        '
        Me.lblTextAmount.Border.BottomColor = System.Drawing.Color.Black
        Me.lblTextAmount.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.lblTextAmount.Border.LeftColor = System.Drawing.Color.Black
        Me.lblTextAmount.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.lblTextAmount.Border.RightColor = System.Drawing.Color.Black
        Me.lblTextAmount.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.lblTextAmount.Border.TopColor = System.Drawing.Color.Black
        Me.lblTextAmount.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.lblTextAmount.Height = 0.3125!
        Me.lblTextAmount.HyperLink = Nothing
        Me.lblTextAmount.Left = 1.583!
        Me.lblTextAmount.Name = "lblTextAmount"
        Me.lblTextAmount.Style = "ddo-char-set: 1; font-size: 14pt; font-family: AngsanaUPC; vertical-align: middle" & _
            "; "
        Me.lblTextAmount.Text = ""
        Me.lblTextAmount.Top = 0.1248333!
        Me.lblTextAmount.Width = 4.125!
        '
        'lblTotalAmount
        '
        Me.lblTotalAmount.Border.BottomColor = System.Drawing.Color.Black
        Me.lblTotalAmount.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.lblTotalAmount.Border.LeftColor = System.Drawing.Color.Black
        Me.lblTotalAmount.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.lblTotalAmount.Border.RightColor = System.Drawing.Color.Black
        Me.lblTotalAmount.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.lblTotalAmount.Border.TopColor = System.Drawing.Color.Black
        Me.lblTotalAmount.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.lblTotalAmount.Height = 0.3125!
        Me.lblTotalAmount.Left = 6.5!
        Me.lblTotalAmount.Name = "lblTotalAmount"
        Me.lblTotalAmount.Style = "ddo-char-set: 1; text-align: right; font-weight: bold; font-size: 14pt; font-fami" & _
            "ly: AngsanaUPC; vertical-align: middle; "
        Me.lblTotalAmount.Text = Nothing
        Me.lblTotalAmount.Top = 0.0625!
        Me.lblTotalAmount.Width = 1.1875!
        '
        'rpt_Receipt_Short
        '
        Me.MasterReport = False
        Me.PageSettings.DefaultPaperSize = False
        Me.PageSettings.DefaultPaperSource = False
        Me.PageSettings.Margins.Bottom = 0.0!
        Me.PageSettings.Margins.Left = 0.0!
        Me.PageSettings.Margins.Right = 0.0!
        Me.PageSettings.Margins.Top = 0.0!
        Me.PageSettings.PaperHeight = 5.5!
        Me.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.Custom
        Me.PageSettings.PaperName = "Half Letter 8 1/2 x 5 1/2 in"
        Me.PageSettings.PaperWidth = 8.25!
        Me.PrintWidth = 8.25!
        Me.Sections.Add(Me.ReportHeader1)
        Me.Sections.Add(Me.PageHeader1)
        Me.Sections.Add(Me.Detail1)
        Me.Sections.Add(Me.PageFooter1)
        Me.Sections.Add(Me.ReportFooter1)
        Me.StyleSheet.Add(New DDCssLib.StyleSheetRule("font-family: Arial; font-style: normal; text-decoration: none; font-weight: norma" & _
                    "l; font-size: 10pt; color: Black; ddo-char-set: 204; ", "Normal"))
        Me.StyleSheet.Add(New DDCssLib.StyleSheetRule("font-size: 16pt; font-weight: bold; ", "Heading1", "Normal"))
        Me.StyleSheet.Add(New DDCssLib.StyleSheetRule("font-family: Times New Roman; font-size: 14pt; font-weight: bold; font-style: ita" & _
                    "lic; ", "Heading2", "Normal"))
        Me.StyleSheet.Add(New DDCssLib.StyleSheetRule("font-size: 13pt; font-weight: bold; ", "Heading3", "Normal"))
        CType(Me.txtNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txttotal_set, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtamt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtset_price, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtreference_code2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblMonth, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblYear, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblReceiptNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCompanyName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ReportInfo1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDisplayName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTextAmount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTotalAmount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub
    Public WithEvents txtNo As DataDynamics.ActiveReports.TextBox
    Public WithEvents txttotal_set As DataDynamics.ActiveReports.TextBox
    Public WithEvents txtamt As DataDynamics.ActiveReports.TextBox
    Public WithEvents txtset_price As DataDynamics.ActiveReports.TextBox
    Public WithEvents txtreference_code2 As DataDynamics.ActiveReports.TextBox
    Friend WithEvents ReportHeader1 As DataDynamics.ActiveReports.ReportHeader
    Friend WithEvents ReportFooter1 As DataDynamics.ActiveReports.ReportFooter
    Private WithEvents Detail1 As DataDynamics.ActiveReports.Detail
    Private WithEvents PageHeader1 As DataDynamics.ActiveReports.PageHeader
    Public WithEvents lblDate As DataDynamics.ActiveReports.Label
    Public WithEvents lblMonth As DataDynamics.ActiveReports.Label
    Public WithEvents lblYear As DataDynamics.ActiveReports.Label
    Public WithEvents lblTime As DataDynamics.ActiveReports.Label
    Public WithEvents lblReceiptNo As DataDynamics.ActiveReports.Label
    Public WithEvents txtCompanyName As DataDynamics.ActiveReports.TextBox
    Friend WithEvents PageFooter1 As DataDynamics.ActiveReports.PageFooter
    Public WithEvents txtDisplayName As DataDynamics.ActiveReports.Label
    Public WithEvents lblTextAmount As DataDynamics.ActiveReports.Label
    Public WithEvents lblTotalAmount As DataDynamics.ActiveReports.TextBox
    Friend WithEvents ReportInfo1 As DataDynamics.ActiveReports.ReportInfo
End Class
