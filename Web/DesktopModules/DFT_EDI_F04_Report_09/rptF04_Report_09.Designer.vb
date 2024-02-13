<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class rptF04_Report_09 
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
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(rptF04_Report_09))
        Me.Detail1 = New DataDynamics.ActiveReports.Detail
        Me.txtBill_No = New DataDynamics.ActiveReports.TextBox
        Me.txtRECEIPT_NAME = New DataDynamics.ActiveReports.TextBox
        Me.ReportHeader1 = New DataDynamics.ActiveReports.ReportHeader
        Me.ReportFooter1 = New DataDynamics.ActiveReports.ReportFooter
        Me.PageHeader1 = New DataDynamics.ActiveReports.PageHeader
        Me.Label8 = New DataDynamics.ActiveReports.Label
        Me.Label4 = New DataDynamics.ActiveReports.Label
        Me.Label1 = New DataDynamics.ActiveReports.Label
        Me.txtFrom = New DataDynamics.ActiveReports.Label
        Me.lblSite_ID = New DataDynamics.ActiveReports.Label
        Me.txtPrintDate = New DataDynamics.ActiveReports.Label
        Me.Line1 = New DataDynamics.ActiveReports.Line
        Me.ReportInfo1 = New DataDynamics.ActiveReports.ReportInfo
        Me.PageFooter1 = New DataDynamics.ActiveReports.PageFooter
        Me.GroupHeader1 = New DataDynamics.ActiveReports.GroupHeader
        Me.Line3 = New DataDynamics.ActiveReports.Line
        Me.Label9 = New DataDynamics.ActiveReports.Label
        Me.Label10 = New DataDynamics.ActiveReports.Label
        Me.GroupFooter1 = New DataDynamics.ActiveReports.GroupFooter
        Me.Label2 = New DataDynamics.ActiveReports.Label
        Me.Label3 = New DataDynamics.ActiveReports.Label
        Me.txtSummary = New DataDynamics.ActiveReports.TextBox
        Me.Line2 = New DataDynamics.ActiveReports.Line
        Me.Line4 = New DataDynamics.ActiveReports.Line
        CType(Me.txtBill_No, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRECEIPT_NAME, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFrom, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblSite_ID, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPrintDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ReportInfo1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSummary, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'Detail1
        '
        Me.Detail1.ColumnSpacing = 0.0!
        Me.Detail1.Controls.AddRange(New DataDynamics.ActiveReports.ARControl() {Me.txtBill_No, Me.txtRECEIPT_NAME})
        Me.Detail1.Height = 0.25!
        Me.Detail1.Name = "Detail1"
        '
        'txtBill_No
        '
        Me.txtBill_No.Border.BottomColor = System.Drawing.Color.Black
        Me.txtBill_No.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtBill_No.Border.LeftColor = System.Drawing.Color.Black
        Me.txtBill_No.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtBill_No.Border.RightColor = System.Drawing.Color.Black
        Me.txtBill_No.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtBill_No.Border.TopColor = System.Drawing.Color.Black
        Me.txtBill_No.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtBill_No.DataField = "REP_BY"
        Me.txtBill_No.Height = 0.25!
        Me.txtBill_No.Left = 0.0625!
        Me.txtBill_No.Name = "txtBill_No"
        Me.txtBill_No.Style = "ddo-char-set: 0; text-align: left; font-weight: normal; font-size: 14.25pt; font-" & _
            "family: AngsanaUPC; "
        Me.txtBill_No.Text = Nothing
        Me.txtBill_No.Top = 0.0!
        Me.txtBill_No.Width = 6.9375!
        '
        'txtRECEIPT_NAME
        '
        Me.txtRECEIPT_NAME.Border.BottomColor = System.Drawing.Color.Black
        Me.txtRECEIPT_NAME.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtRECEIPT_NAME.Border.LeftColor = System.Drawing.Color.Black
        Me.txtRECEIPT_NAME.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtRECEIPT_NAME.Border.RightColor = System.Drawing.Color.Black
        Me.txtRECEIPT_NAME.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtRECEIPT_NAME.Border.TopColor = System.Drawing.Color.Black
        Me.txtRECEIPT_NAME.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtRECEIPT_NAME.DataField = "FORM_COUNT"
        Me.txtRECEIPT_NAME.Height = 0.25!
        Me.txtRECEIPT_NAME.Left = 7.0!
        Me.txtRECEIPT_NAME.Name = "txtRECEIPT_NAME"
        Me.txtRECEIPT_NAME.OutputFormat = resources.GetString("txtRECEIPT_NAME.OutputFormat")
        Me.txtRECEIPT_NAME.Style = "ddo-char-set: 0; text-align: left; font-weight: normal; font-size: 14.25pt; font-" & _
            "family: AngsanaUPC; "
        Me.txtRECEIPT_NAME.Text = Nothing
        Me.txtRECEIPT_NAME.Top = 0.0!
        Me.txtRECEIPT_NAME.Width = 1.4375!
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
        Me.PageHeader1.Controls.AddRange(New DataDynamics.ActiveReports.ARControl() {Me.Label8, Me.Label4, Me.Label1, Me.txtFrom, Me.lblSite_ID, Me.txtPrintDate, Me.Line1, Me.ReportInfo1})
        Me.PageHeader1.Height = 1.379861!
        Me.PageHeader1.Name = "PageHeader1"
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
        Me.Label8.Height = 0.25!
        Me.Label8.HyperLink = Nothing
        Me.Label8.Left = 7.0!
        Me.Label8.Name = "Label8"
        Me.Label8.Style = "ddo-char-set: 222; text-align: center; font-size: 14.25pt; font-family: Angsana N" & _
            "ew; "
        Me.Label8.Text = "จำนวนฉบับ"
        Me.Label8.Top = 1.125!
        Me.Label8.Width = 1.4375!
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
        Me.Label4.Style = "ddo-char-set: 222; text-align: center; font-size: 14.25pt; font-family: Angsana N" & _
            "ew; "
        Me.Label4.Text = "ผู้บันทึกการแนบเอกสาร"
        Me.Label4.Top = 1.125!
        Me.Label4.Width = 6.9375!
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
        Me.Label1.Style = "text-align: left; font-weight: bold; font-size: 16pt; font-family: AngsanaUPC; "
        Me.Label1.Text = "รายงานสรุปการแนบเอกสาร"
        Me.Label1.Top = 0.0625!
        Me.Label1.Width = 8.375!
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
        Me.txtFrom.Style = "ddo-char-set: 222; text-align: left; font-size: 14.25pt; font-family: Angsana New" & _
            "; "
        Me.txtFrom.Text = "ประจำวันที่"
        Me.txtFrom.Top = 0.5!
        Me.txtFrom.Width = 2.9375!
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
        Me.lblSite_ID.Height = 0.25!
        Me.lblSite_ID.HyperLink = Nothing
        Me.lblSite_ID.Left = 5.5!
        Me.lblSite_ID.Name = "lblSite_ID"
        Me.lblSite_ID.Style = "ddo-char-set: 222; text-align: right; font-size: 14.25pt; font-family: Angsana Ne" & _
            "w; "
        Me.lblSite_ID.Text = "สถานที่ : ST-001"
        Me.lblSite_ID.Top = 0.8125!
        Me.lblSite_ID.Width = 2.9375!
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
        Me.txtPrintDate.Left = 0.0625!
        Me.txtPrintDate.Name = "txtPrintDate"
        Me.txtPrintDate.Style = "ddo-char-set: 222; text-align: left; font-size: 14.25pt; font-family: Angsana New" & _
            "; "
        Me.txtPrintDate.Text = "วันที่พิมพ์"
        Me.txtPrintDate.Top = 0.8125!
        Me.txtPrintDate.Width = 2.9375!
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
        Me.Line1.Width = 8.4375!
        Me.Line1.X1 = 0.0!
        Me.Line1.X2 = 8.4375!
        Me.Line1.Y1 = 1.125!
        Me.Line1.Y2 = 1.125!
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
        Me.ReportInfo1.Left = 5.9375!
        Me.ReportInfo1.Name = "ReportInfo1"
        Me.ReportInfo1.Style = "ddo-char-set: 222; text-align: right; font-size: 14.25pt; font-family: Angsana Ne" & _
            "w; "
        Me.ReportInfo1.Top = 0.5!
        Me.ReportInfo1.Width = 2.5!
        '
        'PageFooter1
        '
        Me.PageFooter1.Height = 0.0!
        Me.PageFooter1.Name = "PageFooter1"
        '
        'GroupHeader1
        '
        Me.GroupHeader1.Controls.AddRange(New DataDynamics.ActiveReports.ARControl() {Me.Line3, Me.Label9, Me.Label10})
        Me.GroupHeader1.DataField = "FORM_NAME"
        Me.GroupHeader1.Height = 0.3541667!
        Me.GroupHeader1.Name = "GroupHeader1"
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
        Me.Line3.Left = 0.0625!
        Me.Line3.LineWeight = 1.0!
        Me.Line3.Name = "Line3"
        Me.Line3.Top = 0.0!
        Me.Line3.Width = 8.375!
        Me.Line3.X1 = 0.0625!
        Me.Line3.X2 = 8.4375!
        Me.Line3.Y1 = 0.0!
        Me.Line3.Y2 = 0.0!
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
        Me.Label9.Height = 0.25!
        Me.Label9.HyperLink = Nothing
        Me.Label9.Left = 0.0625!
        Me.Label9.Name = "Label9"
        Me.Label9.Style = "ddo-char-set: 0; text-align: left; font-size: 14.25pt; font-family: Angsana New; " & _
            ""
        Me.Label9.Text = "ฟอร์ม : "
        Me.Label9.Top = 0.0625!
        Me.Label9.Width = 0.4375!
        '
        'Label10
        '
        Me.Label10.Border.BottomColor = System.Drawing.Color.Black
        Me.Label10.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Label10.Border.LeftColor = System.Drawing.Color.Black
        Me.Label10.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Label10.Border.RightColor = System.Drawing.Color.Black
        Me.Label10.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Label10.Border.TopColor = System.Drawing.Color.Black
        Me.Label10.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Label10.DataField = "FORM_NAME"
        Me.Label10.Height = 0.25!
        Me.Label10.HyperLink = Nothing
        Me.Label10.Left = 0.5!
        Me.Label10.Name = "Label10"
        Me.Label10.Style = "ddo-char-set: 0; text-align: left; font-size: 14.25pt; font-family: Angsana New; " & _
            ""
        Me.Label10.Text = ""
        Me.Label10.Top = 0.0625!
        Me.Label10.Width = 7.9375!
        '
        'GroupFooter1
        '
        Me.GroupFooter1.Controls.AddRange(New DataDynamics.ActiveReports.ARControl() {Me.Label2, Me.Label3, Me.txtSummary, Me.Line2, Me.Line4})
        Me.GroupFooter1.Height = 0.59375!
        Me.GroupFooter1.Name = "GroupFooter1"
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
        Me.Label2.Left = 0.0625!
        Me.Label2.Name = "Label2"
        Me.Label2.Style = "text-align: left; font-size: 14pt; font-family: AngsanaUPC; "
        Me.Label2.Text = "รวมทั้งสิ้น"
        Me.Label2.Top = 0.1875!
        Me.Label2.Width = 1.0!
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
        Me.Label3.Left = 3.125!
        Me.Label3.Name = "Label3"
        Me.Label3.Style = "text-align: left; font-size: 14pt; font-family: AngsanaUPC; "
        Me.Label3.Text = "ฉบับ"
        Me.Label3.Top = 0.1875!
        Me.Label3.Width = 0.3125!
        '
        'txtSummary
        '
        Me.txtSummary.Border.BottomColor = System.Drawing.Color.Black
        Me.txtSummary.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtSummary.Border.LeftColor = System.Drawing.Color.Black
        Me.txtSummary.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtSummary.Border.RightColor = System.Drawing.Color.Black
        Me.txtSummary.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtSummary.Border.TopColor = System.Drawing.Color.Black
        Me.txtSummary.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtSummary.DataField = "FORM_COUNT"
        Me.txtSummary.Height = 0.25!
        Me.txtSummary.Left = 1.125!
        Me.txtSummary.Name = "txtSummary"
        Me.txtSummary.OutputFormat = resources.GetString("txtSummary.OutputFormat")
        Me.txtSummary.Style = "ddo-char-set: 0; text-align: right; font-weight: normal; font-size: 14.25pt; font" & _
            "-family: AngsanaUPC; "
        Me.txtSummary.SummaryGroup = "GroupHeader1"
        Me.txtSummary.SummaryRunning = DataDynamics.ActiveReports.SummaryRunning.Group
        Me.txtSummary.SummaryType = DataDynamics.ActiveReports.SummaryType.SubTotal
        Me.txtSummary.Text = Nothing
        Me.txtSummary.Top = 0.1875!
        Me.txtSummary.Width = 1.9375!
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
        Me.Line2.Left = 1.125!
        Me.Line2.LineWeight = 3.0!
        Me.Line2.Name = "Line2"
        Me.Line2.Top = 0.5!
        Me.Line2.Width = 1.9375!
        Me.Line2.X1 = 1.125!
        Me.Line2.X2 = 3.0625!
        Me.Line2.Y1 = 0.5!
        Me.Line2.Y2 = 0.5!
        '
        'Line4
        '
        Me.Line4.Border.BottomColor = System.Drawing.Color.Black
        Me.Line4.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Line4.Border.LeftColor = System.Drawing.Color.Black
        Me.Line4.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Line4.Border.RightColor = System.Drawing.Color.Black
        Me.Line4.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Line4.Border.TopColor = System.Drawing.Color.Black
        Me.Line4.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Line4.Height = 0.0!
        Me.Line4.Left = 0.0!
        Me.Line4.LineWeight = 1.0!
        Me.Line4.Name = "Line4"
        Me.Line4.Top = 0.0625!
        Me.Line4.Width = 8.4375!
        Me.Line4.X1 = 0.0!
        Me.Line4.X2 = 8.4375!
        Me.Line4.Y1 = 0.0625!
        Me.Line4.Y2 = 0.0625!
        '
        'rptF04_Report_09
        '
        Me.MasterReport = False
        Me.PageSettings.DefaultPaperSize = False
        Me.PageSettings.DefaultPaperSource = False
        Me.PageSettings.Margins.Bottom = 0.0!
        Me.PageSettings.Margins.Left = 0.0!
        Me.PageSettings.Margins.Right = 0.0!
        Me.PageSettings.Margins.Top = 0.0!
        Me.PageSettings.PaperHeight = 11.0!
        Me.PageSettings.PaperWidth = 8.5!
        Me.PrintWidth = 8.5!
        Me.Sections.Add(Me.ReportHeader1)
        Me.Sections.Add(Me.PageHeader1)
        Me.Sections.Add(Me.GroupHeader1)
        Me.Sections.Add(Me.Detail1)
        Me.Sections.Add(Me.GroupFooter1)
        Me.Sections.Add(Me.PageFooter1)
        Me.Sections.Add(Me.ReportFooter1)
        Me.StyleSheet.Add(New DDCssLib.StyleSheetRule("font-family: Arial; font-style: normal; text-decoration: none; font-weight: norma" & _
                    "l; font-size: 10pt; color: Black; ", "Normal"))
        Me.StyleSheet.Add(New DDCssLib.StyleSheetRule("font-size: 16pt; font-weight: bold; ", "Heading1", "Normal"))
        Me.StyleSheet.Add(New DDCssLib.StyleSheetRule("font-family: Times New Roman; font-size: 14pt; font-weight: bold; font-style: ita" & _
                    "lic; ", "Heading2", "Normal"))
        Me.StyleSheet.Add(New DDCssLib.StyleSheetRule("font-size: 13pt; font-weight: bold; ", "Heading3", "Normal"))
        CType(Me.txtBill_No, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRECEIPT_NAME, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFrom, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblSite_ID, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPrintDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ReportInfo1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSummary, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub
    Friend WithEvents Detail1 As DataDynamics.ActiveReports.Detail
    Friend WithEvents txtBill_No As DataDynamics.ActiveReports.TextBox
    Friend WithEvents txtRECEIPT_NAME As DataDynamics.ActiveReports.TextBox
    Friend WithEvents ReportHeader1 As DataDynamics.ActiveReports.ReportHeader
    Friend WithEvents ReportFooter1 As DataDynamics.ActiveReports.ReportFooter
    Friend WithEvents PageHeader1 As DataDynamics.ActiveReports.PageHeader
    Friend WithEvents Label8 As DataDynamics.ActiveReports.Label
    Friend WithEvents Label4 As DataDynamics.ActiveReports.Label
    Friend WithEvents Label1 As DataDynamics.ActiveReports.Label
    Friend WithEvents txtFrom As DataDynamics.ActiveReports.Label
    Friend WithEvents lblSite_ID As DataDynamics.ActiveReports.Label
    Friend WithEvents txtPrintDate As DataDynamics.ActiveReports.Label
    Friend WithEvents Line1 As DataDynamics.ActiveReports.Line
    Friend WithEvents ReportInfo1 As DataDynamics.ActiveReports.ReportInfo
    Friend WithEvents PageFooter1 As DataDynamics.ActiveReports.PageFooter
    Friend WithEvents GroupHeader1 As DataDynamics.ActiveReports.GroupHeader
    Friend WithEvents Line3 As DataDynamics.ActiveReports.Line
    Friend WithEvents Label9 As DataDynamics.ActiveReports.Label
    Friend WithEvents Label10 As DataDynamics.ActiveReports.Label
    Friend WithEvents GroupFooter1 As DataDynamics.ActiveReports.GroupFooter
    Friend WithEvents Label2 As DataDynamics.ActiveReports.Label
    Friend WithEvents Label3 As DataDynamics.ActiveReports.Label
    Friend WithEvents txtSummary As DataDynamics.ActiveReports.TextBox
    Friend WithEvents Line2 As DataDynamics.ActiveReports.Line
    Friend WithEvents Line4 As DataDynamics.ActiveReports.Line
End Class 
