Imports System.Globalization
Imports System.Threading

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Public Class rpt_f04_report
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(rpt_f04_report))
        Me.Detail = New DataDynamics.ActiveReports.Detail()
        Me.TextBox6 = New DataDynamics.ActiveReports.TextBox()
        Me.TextBox7 = New DataDynamics.ActiveReports.TextBox()
        Me.TextBox8 = New DataDynamics.ActiveReports.TextBox()
        Me.TextBox9 = New DataDynamics.ActiveReports.TextBox()
        Me.Line3 = New DataDynamics.ActiveReports.Line()
        Me.TextBox11 = New DataDynamics.ActiveReports.TextBox()
        Me.Line10 = New DataDynamics.ActiveReports.Line()
        Me.Line11 = New DataDynamics.ActiveReports.Line()
        Me.Line12 = New DataDynamics.ActiveReports.Line()
        Me.Line13 = New DataDynamics.ActiveReports.Line()
        Me.Line14 = New DataDynamics.ActiveReports.Line()
        Me.PageHeader = New DataDynamics.ActiveReports.PageHeader()
        Me.TextBox1 = New DataDynamics.ActiveReports.TextBox()
        Me.ReportInfo1 = New DataDynamics.ActiveReports.ReportInfo()
        Me.PageFooter = New DataDynamics.ActiveReports.PageFooter()
        Me.GroupHeader1 = New DataDynamics.ActiveReports.GroupHeader()
        Me.GroupFooter1 = New DataDynamics.ActiveReports.GroupFooter()
        Me.TextBox12 = New DataDynamics.ActiveReports.TextBox()
        Me.TextBox13 = New DataDynamics.ActiveReports.TextBox()
        Me.Line15 = New DataDynamics.ActiveReports.Line()
        Me.Line16 = New DataDynamics.ActiveReports.Line()
        Me.TextBox14 = New DataDynamics.ActiveReports.TextBox()
        Me.Line17 = New DataDynamics.ActiveReports.Line()
        Me.TextBox15 = New DataDynamics.ActiveReports.TextBox()
        Me.TextBox16 = New DataDynamics.ActiveReports.TextBox()
        Me.TextBox17 = New DataDynamics.ActiveReports.TextBox()
        Me.TextBox18 = New DataDynamics.ActiveReports.TextBox()
        Me.Line18 = New DataDynamics.ActiveReports.Line()
        Me.Line19 = New DataDynamics.ActiveReports.Line()
        Me.Line20 = New DataDynamics.ActiveReports.Line()
        Me.Line21 = New DataDynamics.ActiveReports.Line()
        Me.Line22 = New DataDynamics.ActiveReports.Line()
        Me.Line23 = New DataDynamics.ActiveReports.Line()
        Me.Line24 = New DataDynamics.ActiveReports.Line()
        Me.Line25 = New DataDynamics.ActiveReports.Line()
        CType(Me.TextBox6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TextBox7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TextBox8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TextBox9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TextBox11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TextBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ReportInfo1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TextBox12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TextBox13, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TextBox14, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TextBox15, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TextBox16, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TextBox17, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TextBox18, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'Detail
        '
        Me.Detail.ColumnSpacing = 0!
        Me.Detail.Controls.AddRange(New DataDynamics.ActiveReports.ARControl() {Me.TextBox6, Me.TextBox7, Me.TextBox8, Me.TextBox9, Me.Line3, Me.TextBox11, Me.Line10, Me.Line11, Me.Line12, Me.Line13, Me.Line14})
        Me.Detail.Height = 0.3541667!
        Me.Detail.Name = "Detail"
        '
        'TextBox6
        '
        Me.TextBox6.Border.BottomColor = System.Drawing.Color.Black
        Me.TextBox6.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.TextBox6.Border.LeftColor = System.Drawing.Color.Black
        Me.TextBox6.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.TextBox6.Border.RightColor = System.Drawing.Color.Black
        Me.TextBox6.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.TextBox6.Border.TopColor = System.Drawing.Color.Black
        Me.TextBox6.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.TextBox6.DataField = "card_id"
        Me.TextBox6.Height = 0.25!
        Me.TextBox6.Left = 0.75!
        Me.TextBox6.Name = "TextBox6"
        Me.TextBox6.Style = "ddo-char-set: 1; text-align: center; font-weight: normal; font-size: 10pt; font-f" &
    "amily: TH SarabunPSK; vertical-align: middle; "
        Me.TextBox6.Text = "เลขบัตร"
        Me.TextBox6.Top = 0.0625!
        Me.TextBox6.Width = 1.25!
        '
        'TextBox7
        '
        Me.TextBox7.Border.BottomColor = System.Drawing.Color.Black
        Me.TextBox7.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.TextBox7.Border.LeftColor = System.Drawing.Color.Black
        Me.TextBox7.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.TextBox7.Border.RightColor = System.Drawing.Color.Black
        Me.TextBox7.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.TextBox7.Border.TopColor = System.Drawing.Color.Black
        Me.TextBox7.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.TextBox7.DataField = "AuthName_Thai"
        Me.TextBox7.Height = 0.25!
        Me.TextBox7.Left = 2.0625!
        Me.TextBox7.Name = "TextBox7"
        Me.TextBox7.Style = "ddo-char-set: 1; text-align: left; font-weight: normal; font-size: 10pt; font-fam" &
    "ily: TH SarabunPSK; vertical-align: middle; "
        Me.TextBox7.Text = "ชื่อผู้มาใช้บริการ"
        Me.TextBox7.Top = 0.0625!
        Me.TextBox7.Width = 3.0625!
        '
        'TextBox8
        '
        Me.TextBox8.Border.BottomColor = System.Drawing.Color.Black
        Me.TextBox8.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.TextBox8.Border.LeftColor = System.Drawing.Color.Black
        Me.TextBox8.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.TextBox8.Border.RightColor = System.Drawing.Color.Black
        Me.TextBox8.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.TextBox8.Border.TopColor = System.Drawing.Color.Black
        Me.TextBox8.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.TextBox8.DataField = "site_name"
        Me.TextBox8.Height = 0.25!
        Me.TextBox8.Left = 5.1875!
        Me.TextBox8.Name = "TextBox8"
        Me.TextBox8.Style = "ddo-char-set: 1; text-align: left; font-weight: normal; font-size: 10pt; font-fam" &
    "ily: TH SarabunPSK; vertical-align: middle; "
        Me.TextBox8.Text = "สาขา"
        Me.TextBox8.Top = 0.0625!
        Me.TextBox8.Width = 1.6875!
        '
        'TextBox9
        '
        Me.TextBox9.Border.BottomColor = System.Drawing.Color.Black
        Me.TextBox9.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.TextBox9.Border.LeftColor = System.Drawing.Color.Black
        Me.TextBox9.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.TextBox9.Border.RightColor = System.Drawing.Color.Black
        Me.TextBox9.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.TextBox9.Border.TopColor = System.Drawing.Color.Black
        Me.TextBox9.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.TextBox9.DataField = "active_date"
        Me.TextBox9.Height = 0.25!
        Me.TextBox9.Left = 6.875!
        Me.TextBox9.Name = "TextBox9"
        Me.TextBox9.OutputFormat = resources.GetString("TextBox9.OutputFormat")
        Me.TextBox9.Style = "ddo-char-set: 1; text-align: center; font-weight: normal; font-size: 10pt; font-f" &
    "amily: TH SarabunPSK; vertical-align: middle; "
        Me.TextBox9.Text = "วันที่มาใช้บริการ"
        Me.TextBox9.Top = 0.0625!
        Me.TextBox9.Width = 1.25!
        '
        'Line3
        '
        Me.Line3.AnchorBottom = True
        Me.Line3.Border.BottomColor = System.Drawing.Color.Black
        Me.Line3.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Line3.Border.LeftColor = System.Drawing.Color.Black
        Me.Line3.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Line3.Border.RightColor = System.Drawing.Color.Black
        Me.Line3.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Line3.Border.TopColor = System.Drawing.Color.Black
        Me.Line3.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Line3.Height = 0.375!
        Me.Line3.Left = 0.125!
        Me.Line3.LineWeight = 1.0!
        Me.Line3.Name = "Line3"
        Me.Line3.Top = 0!
        Me.Line3.Width = 0!
        Me.Line3.X1 = 0.125!
        Me.Line3.X2 = 0.125!
        Me.Line3.Y1 = 0!
        Me.Line3.Y2 = 0.375!
        '
        'TextBox11
        '
        Me.TextBox11.Border.BottomColor = System.Drawing.Color.Black
        Me.TextBox11.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.TextBox11.Border.LeftColor = System.Drawing.Color.Black
        Me.TextBox11.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.TextBox11.Border.RightColor = System.Drawing.Color.Black
        Me.TextBox11.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.TextBox11.Border.TopColor = System.Drawing.Color.Black
        Me.TextBox11.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.TextBox11.Height = 0.25!
        Me.TextBox11.Left = 0.1875!
        Me.TextBox11.Name = "TextBox11"
        Me.TextBox11.Style = "ddo-char-set: 1; text-align: center; font-weight: normal; font-size: 10pt; font-f" &
    "amily: TH SarabunPSK; vertical-align: middle; "
        Me.TextBox11.Text = "ลำดับ"
        Me.TextBox11.Top = 0.0625!
        Me.TextBox11.Width = 0.5!
        '
        'Line10
        '
        Me.Line10.AnchorBottom = True
        Me.Line10.Border.BottomColor = System.Drawing.Color.Black
        Me.Line10.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Line10.Border.LeftColor = System.Drawing.Color.Black
        Me.Line10.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Line10.Border.RightColor = System.Drawing.Color.Black
        Me.Line10.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Line10.Border.TopColor = System.Drawing.Color.Black
        Me.Line10.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Line10.Height = 0.375!
        Me.Line10.Left = 0.75!
        Me.Line10.LineWeight = 1.0!
        Me.Line10.Name = "Line10"
        Me.Line10.Top = 0!
        Me.Line10.Width = 0!
        Me.Line10.X1 = 0.75!
        Me.Line10.X2 = 0.75!
        Me.Line10.Y1 = 0!
        Me.Line10.Y2 = 0.375!
        '
        'Line11
        '
        Me.Line11.AnchorBottom = True
        Me.Line11.Border.BottomColor = System.Drawing.Color.Black
        Me.Line11.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Line11.Border.LeftColor = System.Drawing.Color.Black
        Me.Line11.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Line11.Border.RightColor = System.Drawing.Color.Black
        Me.Line11.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Line11.Border.TopColor = System.Drawing.Color.Black
        Me.Line11.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Line11.Height = 0.375!
        Me.Line11.Left = 2.0!
        Me.Line11.LineWeight = 1.0!
        Me.Line11.Name = "Line11"
        Me.Line11.Top = 0!
        Me.Line11.Width = 0!
        Me.Line11.X1 = 2.0!
        Me.Line11.X2 = 2.0!
        Me.Line11.Y1 = 0!
        Me.Line11.Y2 = 0.375!
        '
        'Line12
        '
        Me.Line12.AnchorBottom = True
        Me.Line12.Border.BottomColor = System.Drawing.Color.Black
        Me.Line12.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Line12.Border.LeftColor = System.Drawing.Color.Black
        Me.Line12.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Line12.Border.RightColor = System.Drawing.Color.Black
        Me.Line12.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Line12.Border.TopColor = System.Drawing.Color.Black
        Me.Line12.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Line12.Height = 0.375!
        Me.Line12.Left = 5.125!
        Me.Line12.LineWeight = 1.0!
        Me.Line12.Name = "Line12"
        Me.Line12.Top = 0!
        Me.Line12.Width = 0!
        Me.Line12.X1 = 5.125!
        Me.Line12.X2 = 5.125!
        Me.Line12.Y1 = 0!
        Me.Line12.Y2 = 0.375!
        '
        'Line13
        '
        Me.Line13.AnchorBottom = True
        Me.Line13.Border.BottomColor = System.Drawing.Color.Black
        Me.Line13.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Line13.Border.LeftColor = System.Drawing.Color.Black
        Me.Line13.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Line13.Border.RightColor = System.Drawing.Color.Black
        Me.Line13.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Line13.Border.TopColor = System.Drawing.Color.Black
        Me.Line13.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Line13.Height = 0.375!
        Me.Line13.Left = 6.875!
        Me.Line13.LineWeight = 1.0!
        Me.Line13.Name = "Line13"
        Me.Line13.Top = 0!
        Me.Line13.Width = 0!
        Me.Line13.X1 = 6.875!
        Me.Line13.X2 = 6.875!
        Me.Line13.Y1 = 0!
        Me.Line13.Y2 = 0.375!
        '
        'Line14
        '
        Me.Line14.AnchorBottom = True
        Me.Line14.Border.BottomColor = System.Drawing.Color.Black
        Me.Line14.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Line14.Border.LeftColor = System.Drawing.Color.Black
        Me.Line14.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Line14.Border.RightColor = System.Drawing.Color.Black
        Me.Line14.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Line14.Border.TopColor = System.Drawing.Color.Black
        Me.Line14.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Line14.Height = 0.375!
        Me.Line14.Left = 8.125!
        Me.Line14.LineWeight = 1.0!
        Me.Line14.Name = "Line14"
        Me.Line14.Top = 0!
        Me.Line14.Width = 0!
        Me.Line14.X1 = 8.125!
        Me.Line14.X2 = 8.125!
        Me.Line14.Y1 = 0!
        Me.Line14.Y2 = 0.375!
        '
        'PageHeader
        '
        Me.PageHeader.Controls.AddRange(New DataDynamics.ActiveReports.ARControl() {Me.TextBox1, Me.ReportInfo1, Me.TextBox14, Me.Line17, Me.TextBox15, Me.TextBox16, Me.TextBox17, Me.TextBox18, Me.Line18, Me.Line19, Me.Line20, Me.Line21, Me.Line22, Me.Line23, Me.Line24, Me.Line25})
        Me.PageHeader.Height = 0.75!
        Me.PageHeader.Name = "PageHeader"
        '
        'TextBox1
        '
        Me.TextBox1.Border.BottomColor = System.Drawing.Color.Black
        Me.TextBox1.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.TextBox1.Border.LeftColor = System.Drawing.Color.Black
        Me.TextBox1.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.TextBox1.Border.RightColor = System.Drawing.Color.Black
        Me.TextBox1.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.TextBox1.Border.TopColor = System.Drawing.Color.Black
        Me.TextBox1.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.TextBox1.Height = 0.3125!
        Me.TextBox1.Left = 2.375!
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Style = "ddo-char-set: 1; text-align: center; font-weight: bold; font-size: 14pt; font-fam" &
    "ily: TH SarabunPSK; vertical-align: top; "
        Me.TextBox1.Text = "รายงาน สถิติการใช้งานกรม"
        Me.TextBox1.Top = 0.0625!
        Me.TextBox1.Width = 3.3125!
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
        Me.ReportInfo1.FormatString = "หน้าที่ {PageNumber} จาก {PageCount}"
        Me.ReportInfo1.Height = 0.25!
        Me.ReportInfo1.Left = 5.5!
        Me.ReportInfo1.Name = "ReportInfo1"
        Me.ReportInfo1.Style = "ddo-char-set: 1; text-align: right; font-size: 10pt; font-family: TH SarabunPSK; " &
    ""
        Me.ReportInfo1.Top = 0.0625!
        Me.ReportInfo1.Width = 2.6875!
        '
        'PageFooter
        '
        Me.PageFooter.Controls.AddRange(New DataDynamics.ActiveReports.ARControl() {Me.TextBox13})
        Me.PageFooter.Height = 0.25!
        Me.PageFooter.Name = "PageFooter"
        '
        'GroupHeader1
        '
        Me.GroupHeader1.DataField = "invh_run_auto"
        Me.GroupHeader1.Height = 0!
        Me.GroupHeader1.KeepTogether = True
        Me.GroupHeader1.Name = "GroupHeader1"
        '
        'GroupFooter1
        '
        Me.GroupFooter1.Controls.AddRange(New DataDynamics.ActiveReports.ARControl() {Me.TextBox12, Me.Line15, Me.Line16})
        Me.GroupFooter1.Height = 0.4479167!
        Me.GroupFooter1.Name = "GroupFooter1"
        '
        'TextBox12
        '
        Me.TextBox12.Border.BottomColor = System.Drawing.Color.Black
        Me.TextBox12.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.TextBox12.Border.LeftColor = System.Drawing.Color.Black
        Me.TextBox12.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.TextBox12.Border.RightColor = System.Drawing.Color.Black
        Me.TextBox12.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.TextBox12.Border.TopColor = System.Drawing.Color.Black
        Me.TextBox12.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.TextBox12.Height = 0.25!
        Me.TextBox12.Left = 5.5625!
        Me.TextBox12.Name = "TextBox12"
        Me.TextBox12.Style = "ddo-char-set: 1; text-align: center; font-weight: bold; font-size: 12pt; font-fam" &
    "ily: TH SarabunPSK; vertical-align: middle; "
        Me.TextBox12.Text = "รวม : "
        Me.TextBox12.Top = 0.0625!
        Me.TextBox12.Visible = False
        Me.TextBox12.Width = 0.75!
        '
        'TextBox13
        '
        Me.TextBox13.Border.BottomColor = System.Drawing.Color.Black
        Me.TextBox13.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.TextBox13.Border.LeftColor = System.Drawing.Color.Black
        Me.TextBox13.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.TextBox13.Border.RightColor = System.Drawing.Color.Black
        Me.TextBox13.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.TextBox13.Border.TopColor = System.Drawing.Color.Black
        Me.TextBox13.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.TextBox13.DataField = "total"
        Me.TextBox13.Height = 0.25!
        Me.TextBox13.Left = 6.4375!
        Me.TextBox13.Name = "TextBox13"
        Me.TextBox13.Style = "ddo-char-set: 1; text-align: center; font-weight: bold; font-size: 12pt; font-fam" &
    "ily: TH SarabunPSK; vertical-align: middle; "
        Me.TextBox13.SummaryRunning = DataDynamics.ActiveReports.SummaryRunning.All
        Me.TextBox13.SummaryType = DataDynamics.ActiveReports.SummaryType.GrandTotal
        Me.TextBox13.Text = "รวม : "
        Me.TextBox13.Top = 0!
        Me.TextBox13.Visible = False
        Me.TextBox13.Width = 1.6875!
        '
        'Line15
        '
        Me.Line15.Border.BottomColor = System.Drawing.Color.Black
        Me.Line15.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Line15.Border.LeftColor = System.Drawing.Color.Black
        Me.Line15.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Line15.Border.RightColor = System.Drawing.Color.Black
        Me.Line15.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Line15.Border.TopColor = System.Drawing.Color.Black
        Me.Line15.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Line15.Height = 0!
        Me.Line15.Left = 0.125!
        Me.Line15.LineWeight = 1.0!
        Me.Line15.Name = "Line15"
        Me.Line15.Top = 0!
        Me.Line15.Width = 8.0!
        Me.Line15.X1 = 0.125!
        Me.Line15.X2 = 8.125!
        Me.Line15.Y1 = 0!
        Me.Line15.Y2 = 0!
        '
        'Line16
        '
        Me.Line16.Border.BottomColor = System.Drawing.Color.Black
        Me.Line16.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Line16.Border.LeftColor = System.Drawing.Color.Black
        Me.Line16.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Line16.Border.RightColor = System.Drawing.Color.Black
        Me.Line16.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Line16.Border.TopColor = System.Drawing.Color.Black
        Me.Line16.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Line16.Height = 0!
        Me.Line16.Left = 0.125!
        Me.Line16.LineWeight = 1.0!
        Me.Line16.Name = "Line16"
        Me.Line16.Top = 0.375!
        Me.Line16.Width = 8.0!
        Me.Line16.X1 = 0.125!
        Me.Line16.X2 = 8.125!
        Me.Line16.Y1 = 0.375!
        Me.Line16.Y2 = 0.375!
        '
        'TextBox14
        '
        Me.TextBox14.Border.BottomColor = System.Drawing.Color.Black
        Me.TextBox14.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.TextBox14.Border.LeftColor = System.Drawing.Color.Black
        Me.TextBox14.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.TextBox14.Border.RightColor = System.Drawing.Color.Black
        Me.TextBox14.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.TextBox14.Border.TopColor = System.Drawing.Color.Black
        Me.TextBox14.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.TextBox14.Height = 0.25!
        Me.TextBox14.Left = 0.1875!
        Me.TextBox14.Name = "TextBox14"
        Me.TextBox14.Style = "ddo-char-set: 1; text-align: center; font-weight: bold; font-size: 12pt; font-fam" &
    "ily: TH SarabunPSK; vertical-align: middle; "
        Me.TextBox14.Text = "ลำดับ"
        Me.TextBox14.Top = 0.5!
        Me.TextBox14.Width = 0.5!
        '
        'Line17
        '
        Me.Line17.Border.BottomColor = System.Drawing.Color.Black
        Me.Line17.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Line17.Border.LeftColor = System.Drawing.Color.Black
        Me.Line17.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Line17.Border.RightColor = System.Drawing.Color.Black
        Me.Line17.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Line17.Border.TopColor = System.Drawing.Color.Black
        Me.Line17.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Line17.Height = 0!
        Me.Line17.Left = 0.1875!
        Me.Line17.LineWeight = 1.0!
        Me.Line17.Name = "Line17"
        Me.Line17.Top = 11.375!
        Me.Line17.Width = 7.8125!
        Me.Line17.X1 = 0.1875!
        Me.Line17.X2 = 8.0!
        Me.Line17.Y1 = 11.375!
        Me.Line17.Y2 = 11.375!
        '
        'TextBox15
        '
        Me.TextBox15.Border.BottomColor = System.Drawing.Color.Black
        Me.TextBox15.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.TextBox15.Border.LeftColor = System.Drawing.Color.Black
        Me.TextBox15.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.TextBox15.Border.RightColor = System.Drawing.Color.Black
        Me.TextBox15.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.TextBox15.Border.TopColor = System.Drawing.Color.Black
        Me.TextBox15.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.TextBox15.Height = 0.25!
        Me.TextBox15.Left = 0.75!
        Me.TextBox15.Name = "TextBox15"
        Me.TextBox15.Style = "ddo-char-set: 1; text-align: center; font-weight: bold; font-size: 12pt; font-fam" &
    "ily: TH SarabunPSK; vertical-align: middle; "
        Me.TextBox15.Text = "เลขที่บัตร/เลขผู้เสียภาษี"
        Me.TextBox15.Top = 0.5!
        Me.TextBox15.Width = 1.25!
        '
        'TextBox16
        '
        Me.TextBox16.Border.BottomColor = System.Drawing.Color.Black
        Me.TextBox16.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.TextBox16.Border.LeftColor = System.Drawing.Color.Black
        Me.TextBox16.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.TextBox16.Border.RightColor = System.Drawing.Color.Black
        Me.TextBox16.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.TextBox16.Border.TopColor = System.Drawing.Color.Black
        Me.TextBox16.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.TextBox16.Height = 0.25!
        Me.TextBox16.Left = 2.0!
        Me.TextBox16.Name = "TextBox16"
        Me.TextBox16.Style = "ddo-char-set: 1; text-align: center; font-weight: bold; font-size: 12pt; font-fam" &
    "ily: TH SarabunPSK; vertical-align: middle; "
        Me.TextBox16.Text = "ชื่อผู้มาใช้บริการ"
        Me.TextBox16.Top = 0.5!
        Me.TextBox16.Width = 3.125!
        '
        'TextBox17
        '
        Me.TextBox17.Border.BottomColor = System.Drawing.Color.Black
        Me.TextBox17.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.TextBox17.Border.LeftColor = System.Drawing.Color.Black
        Me.TextBox17.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.TextBox17.Border.RightColor = System.Drawing.Color.Black
        Me.TextBox17.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.TextBox17.Border.TopColor = System.Drawing.Color.Black
        Me.TextBox17.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.TextBox17.Height = 0.25!
        Me.TextBox17.Left = 5.125!
        Me.TextBox17.Name = "TextBox17"
        Me.TextBox17.Style = "ddo-char-set: 1; text-align: center; font-weight: bold; font-size: 12pt; font-fam" &
    "ily: TH SarabunPSK; vertical-align: middle; "
        Me.TextBox17.Text = "สาขา"
        Me.TextBox17.Top = 0.5!
        Me.TextBox17.Width = 1.75!
        '
        'TextBox18
        '
        Me.TextBox18.Border.BottomColor = System.Drawing.Color.Black
        Me.TextBox18.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.TextBox18.Border.LeftColor = System.Drawing.Color.Black
        Me.TextBox18.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.TextBox18.Border.RightColor = System.Drawing.Color.Black
        Me.TextBox18.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.TextBox18.Border.TopColor = System.Drawing.Color.Black
        Me.TextBox18.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.TextBox18.Height = 0.25!
        Me.TextBox18.Left = 6.875!
        Me.TextBox18.Name = "TextBox18"
        Me.TextBox18.Style = "ddo-char-set: 1; text-align: center; font-weight: bold; font-size: 12pt; font-fam" &
    "ily: TH SarabunPSK; vertical-align: middle; "
        Me.TextBox18.Text = "วันที่มาใช้บริการ"
        Me.TextBox18.Top = 0.5!
        Me.TextBox18.Width = 1.25!
        '
        'Line18
        '
        Me.Line18.Border.BottomColor = System.Drawing.Color.Black
        Me.Line18.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Line18.Border.LeftColor = System.Drawing.Color.Black
        Me.Line18.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Line18.Border.RightColor = System.Drawing.Color.Black
        Me.Line18.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Line18.Border.TopColor = System.Drawing.Color.Black
        Me.Line18.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Line18.Height = 0!
        Me.Line18.Left = 0.125!
        Me.Line18.LineWeight = 1.0!
        Me.Line18.Name = "Line18"
        Me.Line18.Top = 0.75!
        Me.Line18.Width = 8.0!
        Me.Line18.X1 = 0.125!
        Me.Line18.X2 = 8.125!
        Me.Line18.Y1 = 0.75!
        Me.Line18.Y2 = 0.75!
        '
        'Line19
        '
        Me.Line19.Border.BottomColor = System.Drawing.Color.Black
        Me.Line19.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Line19.Border.LeftColor = System.Drawing.Color.Black
        Me.Line19.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Line19.Border.RightColor = System.Drawing.Color.Black
        Me.Line19.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Line19.Border.TopColor = System.Drawing.Color.Black
        Me.Line19.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Line19.Height = 0!
        Me.Line19.Left = 0.125!
        Me.Line19.LineWeight = 1.0!
        Me.Line19.Name = "Line19"
        Me.Line19.Top = 0.5!
        Me.Line19.Width = 8.0!
        Me.Line19.X1 = 0.125!
        Me.Line19.X2 = 8.125!
        Me.Line19.Y1 = 0.5!
        Me.Line19.Y2 = 0.5!
        '
        'Line20
        '
        Me.Line20.AnchorBottom = True
        Me.Line20.Border.BottomColor = System.Drawing.Color.Black
        Me.Line20.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Line20.Border.LeftColor = System.Drawing.Color.Black
        Me.Line20.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Line20.Border.RightColor = System.Drawing.Color.Black
        Me.Line20.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Line20.Border.TopColor = System.Drawing.Color.Black
        Me.Line20.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Line20.Height = 0.25!
        Me.Line20.Left = 0.125!
        Me.Line20.LineWeight = 1.0!
        Me.Line20.Name = "Line20"
        Me.Line20.Top = 0.5!
        Me.Line20.Width = 0!
        Me.Line20.X1 = 0.125!
        Me.Line20.X2 = 0.125!
        Me.Line20.Y1 = 0.5!
        Me.Line20.Y2 = 0.75!
        '
        'Line21
        '
        Me.Line21.AnchorBottom = True
        Me.Line21.Border.BottomColor = System.Drawing.Color.Black
        Me.Line21.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Line21.Border.LeftColor = System.Drawing.Color.Black
        Me.Line21.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Line21.Border.RightColor = System.Drawing.Color.Black
        Me.Line21.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Line21.Border.TopColor = System.Drawing.Color.Black
        Me.Line21.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Line21.Height = 0.25!
        Me.Line21.Left = 8.125!
        Me.Line21.LineWeight = 1.0!
        Me.Line21.Name = "Line21"
        Me.Line21.Top = 0.5!
        Me.Line21.Width = 0!
        Me.Line21.X1 = 8.125!
        Me.Line21.X2 = 8.125!
        Me.Line21.Y1 = 0.5!
        Me.Line21.Y2 = 0.75!
        '
        'Line22
        '
        Me.Line22.AnchorBottom = True
        Me.Line22.Border.BottomColor = System.Drawing.Color.Black
        Me.Line22.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Line22.Border.LeftColor = System.Drawing.Color.Black
        Me.Line22.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Line22.Border.RightColor = System.Drawing.Color.Black
        Me.Line22.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Line22.Border.TopColor = System.Drawing.Color.Black
        Me.Line22.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Line22.Height = 0.25!
        Me.Line22.Left = 0.75!
        Me.Line22.LineWeight = 1.0!
        Me.Line22.Name = "Line22"
        Me.Line22.Top = 0.5!
        Me.Line22.Width = 0!
        Me.Line22.X1 = 0.75!
        Me.Line22.X2 = 0.75!
        Me.Line22.Y1 = 0.5!
        Me.Line22.Y2 = 0.75!
        '
        'Line23
        '
        Me.Line23.AnchorBottom = True
        Me.Line23.Border.BottomColor = System.Drawing.Color.Black
        Me.Line23.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Line23.Border.LeftColor = System.Drawing.Color.Black
        Me.Line23.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Line23.Border.RightColor = System.Drawing.Color.Black
        Me.Line23.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Line23.Border.TopColor = System.Drawing.Color.Black
        Me.Line23.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Line23.Height = 0.25!
        Me.Line23.Left = 2.0!
        Me.Line23.LineWeight = 1.0!
        Me.Line23.Name = "Line23"
        Me.Line23.Top = 0.5!
        Me.Line23.Width = 0!
        Me.Line23.X1 = 2.0!
        Me.Line23.X2 = 2.0!
        Me.Line23.Y1 = 0.5!
        Me.Line23.Y2 = 0.75!
        '
        'Line24
        '
        Me.Line24.AnchorBottom = True
        Me.Line24.Border.BottomColor = System.Drawing.Color.Black
        Me.Line24.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Line24.Border.LeftColor = System.Drawing.Color.Black
        Me.Line24.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Line24.Border.RightColor = System.Drawing.Color.Black
        Me.Line24.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Line24.Border.TopColor = System.Drawing.Color.Black
        Me.Line24.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Line24.Height = 0.25!
        Me.Line24.Left = 5.125!
        Me.Line24.LineWeight = 1.0!
        Me.Line24.Name = "Line24"
        Me.Line24.Top = 0.5!
        Me.Line24.Width = 0!
        Me.Line24.X1 = 5.125!
        Me.Line24.X2 = 5.125!
        Me.Line24.Y1 = 0.5!
        Me.Line24.Y2 = 0.75!
        '
        'Line25
        '
        Me.Line25.AnchorBottom = True
        Me.Line25.Border.BottomColor = System.Drawing.Color.Black
        Me.Line25.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Line25.Border.LeftColor = System.Drawing.Color.Black
        Me.Line25.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Line25.Border.RightColor = System.Drawing.Color.Black
        Me.Line25.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Line25.Border.TopColor = System.Drawing.Color.Black
        Me.Line25.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Line25.Height = 0.25!
        Me.Line25.Left = 6.875!
        Me.Line25.LineWeight = 1.0!
        Me.Line25.Name = "Line25"
        Me.Line25.Top = 0.5!
        Me.Line25.Width = 0!
        Me.Line25.X1 = 6.875!
        Me.Line25.X2 = 6.875!
        Me.Line25.Y1 = 0.5!
        Me.Line25.Y2 = 0.75!
        '
        'rpt_f04_report
        '
        Me.MasterReport = False
        Me.PageSettings.DefaultPaperSize = False
        Me.PageSettings.Margins.Bottom = 0!
        Me.PageSettings.Margins.Left = 0!
        Me.PageSettings.Margins.Right = 0!
        Me.PageSettings.Margins.Top = 0!
        Me.PageSettings.PaperHeight = 11.69291!
        Me.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.A4
        Me.PageSettings.PaperWidth = 8.27!
        Me.PrintWidth = 8.26!
        Me.Sections.Add(Me.PageHeader)
        Me.Sections.Add(Me.GroupHeader1)
        Me.Sections.Add(Me.Detail)
        Me.Sections.Add(Me.GroupFooter1)
        Me.Sections.Add(Me.PageFooter)
        Me.StyleSheet.Add(New DDCssLib.StyleSheetRule(resources.GetString("$this.StyleSheet"), "Normal"))
        Me.StyleSheet.Add(New DDCssLib.StyleSheetRule("font-family: inherit; font-style: inherit; font-variant: inherit; font-weight: bo" &
            "ld; font-size: 16pt; font-size-adjust: inherit; font-stretch: inherit; ", "Heading1", "Normal"))
        Me.StyleSheet.Add(New DDCssLib.StyleSheetRule("font-family: Times New Roman; font-style: italic; font-variant: inherit; font-wei" &
            "ght: bold; font-size: 14pt; font-size-adjust: inherit; font-stretch: inherit; ", "Heading2", "Normal"))
        Me.StyleSheet.Add(New DDCssLib.StyleSheetRule("font-family: inherit; font-style: inherit; font-variant: inherit; font-weight: bo" &
            "ld; font-size: 13pt; font-size-adjust: inherit; font-stretch: inherit; ", "Heading3", "Normal"))
        CType(Me.TextBox6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TextBox7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TextBox8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TextBox9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TextBox11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TextBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ReportInfo1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TextBox12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TextBox13, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TextBox14, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TextBox15, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TextBox16, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TextBox17, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TextBox18, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub
    Friend WithEvents Detail As DataDynamics.ActiveReports.Detail
    Friend WithEvents PageHeader As DataDynamics.ActiveReports.PageHeader
    Friend WithEvents PageFooter As DataDynamics.ActiveReports.PageFooter
    Friend WithEvents GroupHeader1 As DataDynamics.ActiveReports.GroupHeader
    Friend WithEvents GroupFooter1 As DataDynamics.ActiveReports.GroupFooter
    Friend WithEvents TextBox1 As DataDynamics.ActiveReports.TextBox
    Friend WithEvents TextBox6 As DataDynamics.ActiveReports.TextBox
    Friend WithEvents TextBox7 As DataDynamics.ActiveReports.TextBox
    Friend WithEvents TextBox8 As DataDynamics.ActiveReports.TextBox
    Friend WithEvents TextBox9 As DataDynamics.ActiveReports.TextBox
    Friend WithEvents Line3 As DataDynamics.ActiveReports.Line
    Friend WithEvents TextBox11 As DataDynamics.ActiveReports.TextBox
    Friend WithEvents Line10 As DataDynamics.ActiveReports.Line
    Friend WithEvents Line11 As DataDynamics.ActiveReports.Line
    Friend WithEvents Line12 As DataDynamics.ActiveReports.Line
    Friend WithEvents Line13 As DataDynamics.ActiveReports.Line
    Friend WithEvents Line14 As DataDynamics.ActiveReports.Line
    Friend WithEvents ReportInfo1 As DataDynamics.ActiveReports.ReportInfo
    Friend WithEvents TextBox12 As DataDynamics.ActiveReports.TextBox
    Friend WithEvents TextBox13 As DataDynamics.ActiveReports.TextBox
    Friend WithEvents Line15 As DataDynamics.ActiveReports.Line
    Friend WithEvents Line16 As DataDynamics.ActiveReports.Line
    Friend WithEvents TextBox14 As DataDynamics.ActiveReports.TextBox
    Friend WithEvents Line17 As DataDynamics.ActiveReports.Line
    Friend WithEvents TextBox15 As DataDynamics.ActiveReports.TextBox
    Friend WithEvents TextBox16 As DataDynamics.ActiveReports.TextBox
    Friend WithEvents TextBox17 As DataDynamics.ActiveReports.TextBox
    Friend WithEvents TextBox18 As DataDynamics.ActiveReports.TextBox
    Friend WithEvents Line18 As DataDynamics.ActiveReports.Line
    Friend WithEvents Line19 As DataDynamics.ActiveReports.Line
    Friend WithEvents Line20 As DataDynamics.ActiveReports.Line
    Friend WithEvents Line21 As DataDynamics.ActiveReports.Line
    Friend WithEvents Line22 As DataDynamics.ActiveReports.Line
    Friend WithEvents Line23 As DataDynamics.ActiveReports.Line
    Friend WithEvents Line24 As DataDynamics.ActiveReports.Line
    Friend WithEvents Line25 As DataDynamics.ActiveReports.Line
End Class
