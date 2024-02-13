<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class rptEDI_Report_04 
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
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(rptEDI_Report_04))
        Me.Detail1 = New DataDynamics.ActiveReports.Detail
        Me.txtDescription = New DataDynamics.ActiveReports.TextBox
        Me.txtFORM_COUNT = New DataDynamics.ActiveReports.TextBox
        Me.txtSUM_FOB = New DataDynamics.ActiveReports.TextBox
        Me.Line9 = New DataDynamics.ActiveReports.Line
        Me.Line10 = New DataDynamics.ActiveReports.Line
        Me.Line11 = New DataDynamics.ActiveReports.Line
        Me.Line12 = New DataDynamics.ActiveReports.Line
        Me.ReportHeader1 = New DataDynamics.ActiveReports.ReportHeader
        Me.Label1 = New DataDynamics.ActiveReports.Label
        Me.ReportFooter1 = New DataDynamics.ActiveReports.ReportFooter
        Me.PageHeader1 = New DataDynamics.ActiveReports.PageHeader
        Me.Label4 = New DataDynamics.ActiveReports.Label
        Me.Label5 = New DataDynamics.ActiveReports.Label
        Me.Label7 = New DataDynamics.ActiveReports.Label
        Me.Line1 = New DataDynamics.ActiveReports.Line
        Me.Line2 = New DataDynamics.ActiveReports.Line
        Me.Line3 = New DataDynamics.ActiveReports.Line
        Me.Line4 = New DataDynamics.ActiveReports.Line
        Me.Line5 = New DataDynamics.ActiveReports.Line
        Me.Line7 = New DataDynamics.ActiveReports.Line
        Me.Label14 = New DataDynamics.ActiveReports.Label
        Me.lblDateLenght = New DataDynamics.ActiveReports.Label
        Me.PageFooter1 = New DataDynamics.ActiveReports.PageFooter
        Me.GroupHeader1 = New DataDynamics.ActiveReports.GroupHeader
        Me.GroupFooter1 = New DataDynamics.ActiveReports.GroupFooter
        Me.txtTOTAL_SUM_FOB = New DataDynamics.ActiveReports.TextBox
        Me.Label8 = New DataDynamics.ActiveReports.Label
        Me.txtTOTAL_FORM_COUNT = New DataDynamics.ActiveReports.TextBox
        Me.Line13 = New DataDynamics.ActiveReports.Line
        Me.Line8 = New DataDynamics.ActiveReports.Line
        Me.Line14 = New DataDynamics.ActiveReports.Line
        Me.Line15 = New DataDynamics.ActiveReports.Line
        Me.Line16 = New DataDynamics.ActiveReports.Line
        Me.Line17 = New DataDynamics.ActiveReports.Line
        CType(Me.txtDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFORM_COUNT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSUM_FOB, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label14, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDateLenght, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTOTAL_SUM_FOB, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTOTAL_FORM_COUNT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'Detail1
        '
        Me.Detail1.ColumnSpacing = 0.0!
        Me.Detail1.Controls.AddRange(New DataDynamics.ActiveReports.ARControl() {Me.txtDescription, Me.txtFORM_COUNT, Me.txtSUM_FOB, Me.Line9, Me.Line10, Me.Line11, Me.Line12})
        Me.Detail1.Height = 0.3125!
        Me.Detail1.Name = "Detail1"
        '
        'txtDescription
        '
        Me.txtDescription.Border.BottomColor = System.Drawing.Color.Black
        Me.txtDescription.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtDescription.Border.LeftColor = System.Drawing.Color.Black
        Me.txtDescription.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtDescription.Border.RightColor = System.Drawing.Color.Black
        Me.txtDescription.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtDescription.Border.TopColor = System.Drawing.Color.Black
        Me.txtDescription.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtDescription.DataField = "Description"
        Me.txtDescription.Height = 0.3125!
        Me.txtDescription.Left = 0.125!
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.Style = "ddo-char-set: 0; font-size: 15.75pt; font-family: AngsanaUPC; "
        Me.txtDescription.Text = "txtDescription"
        Me.txtDescription.Top = 0.0!
        Me.txtDescription.Width = 4.8125!
        '
        'txtFORM_COUNT
        '
        Me.txtFORM_COUNT.Border.BottomColor = System.Drawing.Color.Black
        Me.txtFORM_COUNT.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtFORM_COUNT.Border.LeftColor = System.Drawing.Color.Black
        Me.txtFORM_COUNT.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtFORM_COUNT.Border.RightColor = System.Drawing.Color.Black
        Me.txtFORM_COUNT.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtFORM_COUNT.Border.TopColor = System.Drawing.Color.Black
        Me.txtFORM_COUNT.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtFORM_COUNT.DataField = "FORM_COUNT"
        Me.txtFORM_COUNT.Height = 0.3125!
        Me.txtFORM_COUNT.Left = 5.0625!
        Me.txtFORM_COUNT.Name = "txtFORM_COUNT"
        Me.txtFORM_COUNT.OutputFormat = resources.GetString("txtFORM_COUNT.OutputFormat")
        Me.txtFORM_COUNT.Style = "ddo-char-set: 0; text-align: right; font-size: 15.75pt; font-family: AngsanaUPC; " & _
            ""
        Me.txtFORM_COUNT.Text = "txtFORM_COUNT"
        Me.txtFORM_COUNT.Top = 0.0!
        Me.txtFORM_COUNT.Width = 1.5!
        '
        'txtSUM_FOB
        '
        Me.txtSUM_FOB.Border.BottomColor = System.Drawing.Color.Black
        Me.txtSUM_FOB.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtSUM_FOB.Border.LeftColor = System.Drawing.Color.Black
        Me.txtSUM_FOB.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtSUM_FOB.Border.RightColor = System.Drawing.Color.Black
        Me.txtSUM_FOB.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtSUM_FOB.Border.TopColor = System.Drawing.Color.Black
        Me.txtSUM_FOB.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtSUM_FOB.DataField = "SUM_FOB"
        Me.txtSUM_FOB.Height = 0.3125!
        Me.txtSUM_FOB.Left = 6.6875!
        Me.txtSUM_FOB.Name = "txtSUM_FOB"
        Me.txtSUM_FOB.OutputFormat = resources.GetString("txtSUM_FOB.OutputFormat")
        Me.txtSUM_FOB.Style = "ddo-char-set: 0; text-align: right; font-size: 15.75pt; font-family: AngsanaUPC; " & _
            ""
        Me.txtSUM_FOB.Text = "txtSUM_FOB"
        Me.txtSUM_FOB.Top = 0.0!
        Me.txtSUM_FOB.Width = 1.6875!
        '
        'Line9
        '
        Me.Line9.Border.BottomColor = System.Drawing.Color.Black
        Me.Line9.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Line9.Border.LeftColor = System.Drawing.Color.Black
        Me.Line9.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Line9.Border.RightColor = System.Drawing.Color.Black
        Me.Line9.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Line9.Border.TopColor = System.Drawing.Color.Black
        Me.Line9.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Line9.Height = 0.3125!
        Me.Line9.Left = 0.0625!
        Me.Line9.LineWeight = 1.0!
        Me.Line9.Name = "Line9"
        Me.Line9.Top = 0.0!
        Me.Line9.Width = 0.0!
        Me.Line9.X1 = 0.0625!
        Me.Line9.X2 = 0.0625!
        Me.Line9.Y1 = 0.0!
        Me.Line9.Y2 = 0.3125!
        '
        'Line10
        '
        Me.Line10.Border.BottomColor = System.Drawing.Color.Black
        Me.Line10.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Line10.Border.LeftColor = System.Drawing.Color.Black
        Me.Line10.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Line10.Border.RightColor = System.Drawing.Color.Black
        Me.Line10.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Line10.Border.TopColor = System.Drawing.Color.Black
        Me.Line10.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Line10.Height = 0.3125!
        Me.Line10.Left = 5.0!
        Me.Line10.LineWeight = 1.0!
        Me.Line10.Name = "Line10"
        Me.Line10.Top = 0.0!
        Me.Line10.Width = 0.0!
        Me.Line10.X1 = 5.0!
        Me.Line10.X2 = 5.0!
        Me.Line10.Y1 = 0.0!
        Me.Line10.Y2 = 0.3125!
        '
        'Line11
        '
        Me.Line11.Border.BottomColor = System.Drawing.Color.Black
        Me.Line11.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Line11.Border.LeftColor = System.Drawing.Color.Black
        Me.Line11.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Line11.Border.RightColor = System.Drawing.Color.Black
        Me.Line11.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Line11.Border.TopColor = System.Drawing.Color.Black
        Me.Line11.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Line11.Height = 0.3125!
        Me.Line11.Left = 6.625!
        Me.Line11.LineWeight = 1.0!
        Me.Line11.Name = "Line11"
        Me.Line11.Top = 0.0!
        Me.Line11.Width = 0.0!
        Me.Line11.X1 = 6.625!
        Me.Line11.X2 = 6.625!
        Me.Line11.Y1 = 0.0!
        Me.Line11.Y2 = 0.3125!
        '
        'Line12
        '
        Me.Line12.Border.BottomColor = System.Drawing.Color.Black
        Me.Line12.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Line12.Border.LeftColor = System.Drawing.Color.Black
        Me.Line12.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Line12.Border.RightColor = System.Drawing.Color.Black
        Me.Line12.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Line12.Border.TopColor = System.Drawing.Color.Black
        Me.Line12.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Line12.Height = 0.3125!
        Me.Line12.Left = 8.4375!
        Me.Line12.LineWeight = 1.0!
        Me.Line12.Name = "Line12"
        Me.Line12.Top = 0.0!
        Me.Line12.Width = 0.0!
        Me.Line12.X1 = 8.4375!
        Me.Line12.X2 = 8.4375!
        Me.Line12.Y1 = 0.0!
        Me.Line12.Y2 = 0.3125!
        '
        'ReportHeader1
        '
        Me.ReportHeader1.Controls.AddRange(New DataDynamics.ActiveReports.ARControl() {Me.Label1})
        Me.ReportHeader1.Height = 0.4444444!
        Me.ReportHeader1.Name = "ReportHeader1"
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
        Me.Label1.Text = "การออกหนังสือรับรองถิ่นกำเนิดสินค้า (ระบบ EDI)"
        Me.Label1.Top = 0.0!
        Me.Label1.Width = 8.375!
        '
        'ReportFooter1
        '
        Me.ReportFooter1.Height = 0.0!
        Me.ReportFooter1.Name = "ReportFooter1"
        '
        'PageHeader1
        '
        Me.PageHeader1.Controls.AddRange(New DataDynamics.ActiveReports.ARControl() {Me.Label4, Me.Label5, Me.Label7, Me.Line1, Me.Line2, Me.Line3, Me.Line4, Me.Line5, Me.Line7, Me.Label14, Me.lblDateLenght})
        Me.PageHeader1.Height = 0.75!
        Me.PageHeader1.Name = "PageHeader1"
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
        Me.Label4.Left = 6.6875!
        Me.Label4.Name = "Label4"
        Me.Label4.Style = "ddo-char-set: 0; text-align: center; font-size: 15.75pt; font-family: AngsanaUPC;" & _
            " "
        Me.Label4.Text = "มูลค่า (USD)"
        Me.Label4.Top = 0.4375!
        Me.Label4.Width = 1.75!
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
        Me.Label5.Left = 5.0625!
        Me.Label5.Name = "Label5"
        Me.Label5.Style = "ddo-char-set: 0; text-align: center; font-size: 15.75pt; font-family: AngsanaUPC;" & _
            " "
        Me.Label5.Text = "ฉบับ"
        Me.Label5.Top = 0.4375!
        Me.Label5.Width = 1.5!
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
        Me.Label7.Left = 0.0625!
        Me.Label7.Name = "Label7"
        Me.Label7.Style = "ddo-char-set: 0; text-align: center; font-size: 15.75pt; font-family: AngsanaUPC;" & _
            " vertical-align: middle; "
        Me.Label7.Text = "ประเภทหนังสือรับรองถิ่นกำเนิดสินค้า"
        Me.Label7.Top = 0.4375!
        Me.Label7.Width = 4.875!
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
        Me.Line2.Top = 0.4375!
        Me.Line2.Width = 8.375!
        Me.Line2.X1 = 0.0625!
        Me.Line2.X2 = 8.4375!
        Me.Line2.Y1 = 0.4375!
        Me.Line2.Y2 = 0.4375!
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
        Me.Line3.Height = 0.3125!
        Me.Line3.Left = 5.0!
        Me.Line3.LineWeight = 1.0!
        Me.Line3.Name = "Line3"
        Me.Line3.Top = 0.4375!
        Me.Line3.Width = 0.0!
        Me.Line3.X1 = 5.0!
        Me.Line3.X2 = 5.0!
        Me.Line3.Y1 = 0.4375!
        Me.Line3.Y2 = 0.75!
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
        Me.Line4.Height = 0.3125!
        Me.Line4.Left = 8.4375!
        Me.Line4.LineWeight = 1.0!
        Me.Line4.Name = "Line4"
        Me.Line4.Top = 0.4375!
        Me.Line4.Width = 0.0!
        Me.Line4.X1 = 8.4375!
        Me.Line4.X2 = 8.4375!
        Me.Line4.Y1 = 0.4375!
        Me.Line4.Y2 = 0.75!
        '
        'Line5
        '
        Me.Line5.Border.BottomColor = System.Drawing.Color.Black
        Me.Line5.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Line5.Border.LeftColor = System.Drawing.Color.Black
        Me.Line5.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Line5.Border.RightColor = System.Drawing.Color.Black
        Me.Line5.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Line5.Border.TopColor = System.Drawing.Color.Black
        Me.Line5.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Line5.Height = 0.3125!
        Me.Line5.Left = 0.0625!
        Me.Line5.LineWeight = 1.0!
        Me.Line5.Name = "Line5"
        Me.Line5.Top = 0.4375!
        Me.Line5.Width = 0.0!
        Me.Line5.X1 = 0.0625!
        Me.Line5.X2 = 0.0625!
        Me.Line5.Y1 = 0.4375!
        Me.Line5.Y2 = 0.75!
        '
        'Line7
        '
        Me.Line7.Border.BottomColor = System.Drawing.Color.Black
        Me.Line7.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Line7.Border.LeftColor = System.Drawing.Color.Black
        Me.Line7.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Line7.Border.RightColor = System.Drawing.Color.Black
        Me.Line7.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Line7.Border.TopColor = System.Drawing.Color.Black
        Me.Line7.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Line7.Height = 0.3125!
        Me.Line7.Left = 6.625!
        Me.Line7.LineWeight = 1.0!
        Me.Line7.Name = "Line7"
        Me.Line7.Top = 0.4375!
        Me.Line7.Width = 0.0!
        Me.Line7.X1 = 6.625!
        Me.Line7.X2 = 6.625!
        Me.Line7.Y1 = 0.4375!
        Me.Line7.Y2 = 0.75!
        '
        'Label14
        '
        Me.Label14.Border.BottomColor = System.Drawing.Color.Black
        Me.Label14.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Label14.Border.LeftColor = System.Drawing.Color.Black
        Me.Label14.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Label14.Border.RightColor = System.Drawing.Color.Black
        Me.Label14.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Label14.Border.TopColor = System.Drawing.Color.Black
        Me.Label14.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Label14.Height = 0.3125!
        Me.Label14.HyperLink = Nothing
        Me.Label14.Left = 0.0625!
        Me.Label14.Name = "Label14"
        Me.Label14.Style = "font-size: 16pt; font-family: AngsanaUPC; "
        Me.Label14.Text = "ตั้งแต่วันที่ :"
        Me.Label14.Top = 0.0625!
        Me.Label14.Width = 0.75!
        '
        'lblDateLenght
        '
        Me.lblDateLenght.Border.BottomColor = System.Drawing.Color.Black
        Me.lblDateLenght.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.lblDateLenght.Border.LeftColor = System.Drawing.Color.Black
        Me.lblDateLenght.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.lblDateLenght.Border.RightColor = System.Drawing.Color.Black
        Me.lblDateLenght.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.lblDateLenght.Border.TopColor = System.Drawing.Color.Black
        Me.lblDateLenght.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.lblDateLenght.Height = 0.3125!
        Me.lblDateLenght.HyperLink = Nothing
        Me.lblDateLenght.Left = 0.8125!
        Me.lblDateLenght.Name = "lblDateLenght"
        Me.lblDateLenght.Style = "font-size: 16pt; font-family: AngsanaUPC; "
        Me.lblDateLenght.Text = ""
        Me.lblDateLenght.Top = 0.0625!
        Me.lblDateLenght.Width = 2.625!
        '
        'PageFooter1
        '
        Me.PageFooter1.Height = 0.0!
        Me.PageFooter1.Name = "PageFooter1"
        '
        'GroupHeader1
        '
        Me.GroupHeader1.Height = 0.0!
        Me.GroupHeader1.Name = "GroupHeader1"
        '
        'GroupFooter1
        '
        Me.GroupFooter1.Controls.AddRange(New DataDynamics.ActiveReports.ARControl() {Me.txtTOTAL_SUM_FOB, Me.Label8, Me.txtTOTAL_FORM_COUNT, Me.Line13, Me.Line8, Me.Line14, Me.Line15, Me.Line16, Me.Line17})
        Me.GroupFooter1.Height = 0.3125!
        Me.GroupFooter1.Name = "GroupFooter1"
        '
        'txtTOTAL_SUM_FOB
        '
        Me.txtTOTAL_SUM_FOB.Border.BottomColor = System.Drawing.Color.Black
        Me.txtTOTAL_SUM_FOB.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtTOTAL_SUM_FOB.Border.LeftColor = System.Drawing.Color.Black
        Me.txtTOTAL_SUM_FOB.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtTOTAL_SUM_FOB.Border.RightColor = System.Drawing.Color.Black
        Me.txtTOTAL_SUM_FOB.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtTOTAL_SUM_FOB.Border.TopColor = System.Drawing.Color.Black
        Me.txtTOTAL_SUM_FOB.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtTOTAL_SUM_FOB.DataField = "SUM_FOB"
        Me.txtTOTAL_SUM_FOB.Height = 0.3125!
        Me.txtTOTAL_SUM_FOB.Left = 6.6875!
        Me.txtTOTAL_SUM_FOB.Name = "txtTOTAL_SUM_FOB"
        Me.txtTOTAL_SUM_FOB.OutputFormat = resources.GetString("txtTOTAL_SUM_FOB.OutputFormat")
        Me.txtTOTAL_SUM_FOB.Style = "ddo-char-set: 0; text-align: right; font-size: 15.75pt; font-family: AngsanaUPC; " & _
            ""
        Me.txtTOTAL_SUM_FOB.SummaryRunning = DataDynamics.ActiveReports.SummaryRunning.All
        Me.txtTOTAL_SUM_FOB.SummaryType = DataDynamics.ActiveReports.SummaryType.GrandTotal
        Me.txtTOTAL_SUM_FOB.Text = "txtTOTAL_SUM_FOB"
        Me.txtTOTAL_SUM_FOB.Top = 0.0!
        Me.txtTOTAL_SUM_FOB.Width = 1.6875!
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
        Me.Label8.Left = 3.4375!
        Me.Label8.Name = "Label8"
        Me.Label8.Style = "ddo-char-set: 0; text-align: center; font-size: 15.75pt; font-family: AngsanaUPC;" & _
            " "
        Me.Label8.Text = "รวม"
        Me.Label8.Top = 0.0!
        Me.Label8.Width = 1.5!
        '
        'txtTOTAL_FORM_COUNT
        '
        Me.txtTOTAL_FORM_COUNT.Border.BottomColor = System.Drawing.Color.Black
        Me.txtTOTAL_FORM_COUNT.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtTOTAL_FORM_COUNT.Border.LeftColor = System.Drawing.Color.Black
        Me.txtTOTAL_FORM_COUNT.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtTOTAL_FORM_COUNT.Border.RightColor = System.Drawing.Color.Black
        Me.txtTOTAL_FORM_COUNT.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtTOTAL_FORM_COUNT.Border.TopColor = System.Drawing.Color.Black
        Me.txtTOTAL_FORM_COUNT.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtTOTAL_FORM_COUNT.DataField = "FORM_COUNT"
        Me.txtTOTAL_FORM_COUNT.Height = 0.3125!
        Me.txtTOTAL_FORM_COUNT.Left = 5.0625!
        Me.txtTOTAL_FORM_COUNT.Name = "txtTOTAL_FORM_COUNT"
        Me.txtTOTAL_FORM_COUNT.OutputFormat = resources.GetString("txtTOTAL_FORM_COUNT.OutputFormat")
        Me.txtTOTAL_FORM_COUNT.Style = "ddo-char-set: 0; text-align: right; font-size: 15.75pt; font-family: AngsanaUPC; " & _
            ""
        Me.txtTOTAL_FORM_COUNT.SummaryRunning = DataDynamics.ActiveReports.SummaryRunning.All
        Me.txtTOTAL_FORM_COUNT.SummaryType = DataDynamics.ActiveReports.SummaryType.GrandTotal
        Me.txtTOTAL_FORM_COUNT.Text = "txtTOTAL_FORM_COUNT"
        Me.txtTOTAL_FORM_COUNT.Top = 0.0!
        Me.txtTOTAL_FORM_COUNT.Width = 1.5!
        '
        'Line13
        '
        Me.Line13.Border.BottomColor = System.Drawing.Color.Black
        Me.Line13.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Line13.Border.LeftColor = System.Drawing.Color.Black
        Me.Line13.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Line13.Border.RightColor = System.Drawing.Color.Black
        Me.Line13.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Line13.Border.TopColor = System.Drawing.Color.Black
        Me.Line13.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Line13.Height = 0.0!
        Me.Line13.Left = 0.0625!
        Me.Line13.LineWeight = 1.0!
        Me.Line13.Name = "Line13"
        Me.Line13.Top = 0.0!
        Me.Line13.Width = 8.375!
        Me.Line13.X1 = 0.0625!
        Me.Line13.X2 = 8.4375!
        Me.Line13.Y1 = 0.0!
        Me.Line13.Y2 = 0.0!
        '
        'Line8
        '
        Me.Line8.Border.BottomColor = System.Drawing.Color.Black
        Me.Line8.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Line8.Border.LeftColor = System.Drawing.Color.Black
        Me.Line8.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Line8.Border.RightColor = System.Drawing.Color.Black
        Me.Line8.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Line8.Border.TopColor = System.Drawing.Color.Black
        Me.Line8.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Line8.Height = 0.0!
        Me.Line8.Left = 0.0625!
        Me.Line8.LineWeight = 1.0!
        Me.Line8.Name = "Line8"
        Me.Line8.Top = 0.3125!
        Me.Line8.Width = 8.375!
        Me.Line8.X1 = 0.0625!
        Me.Line8.X2 = 8.4375!
        Me.Line8.Y1 = 0.3125!
        Me.Line8.Y2 = 0.3125!
        '
        'Line14
        '
        Me.Line14.Border.BottomColor = System.Drawing.Color.Black
        Me.Line14.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Line14.Border.LeftColor = System.Drawing.Color.Black
        Me.Line14.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Line14.Border.RightColor = System.Drawing.Color.Black
        Me.Line14.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Line14.Border.TopColor = System.Drawing.Color.Black
        Me.Line14.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Line14.Height = 0.3125!
        Me.Line14.Left = 0.0625!
        Me.Line14.LineWeight = 1.0!
        Me.Line14.Name = "Line14"
        Me.Line14.Top = 0.0!
        Me.Line14.Width = 0.0!
        Me.Line14.X1 = 0.0625!
        Me.Line14.X2 = 0.0625!
        Me.Line14.Y1 = 0.0!
        Me.Line14.Y2 = 0.3125!
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
        Me.Line15.Height = 0.3125!
        Me.Line15.Left = 5.0!
        Me.Line15.LineWeight = 1.0!
        Me.Line15.Name = "Line15"
        Me.Line15.Top = 0.0!
        Me.Line15.Width = 0.0!
        Me.Line15.X1 = 5.0!
        Me.Line15.X2 = 5.0!
        Me.Line15.Y1 = 0.0!
        Me.Line15.Y2 = 0.3125!
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
        Me.Line16.Height = 0.3125!
        Me.Line16.Left = 6.625!
        Me.Line16.LineWeight = 1.0!
        Me.Line16.Name = "Line16"
        Me.Line16.Top = 0.0!
        Me.Line16.Width = 0.0!
        Me.Line16.X1 = 6.625!
        Me.Line16.X2 = 6.625!
        Me.Line16.Y1 = 0.0!
        Me.Line16.Y2 = 0.3125!
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
        Me.Line17.Height = 0.3125!
        Me.Line17.Left = 8.4375!
        Me.Line17.LineWeight = 1.0!
        Me.Line17.Name = "Line17"
        Me.Line17.Top = 0.0!
        Me.Line17.Width = 0.0!
        Me.Line17.X1 = 8.4375!
        Me.Line17.X2 = 8.4375!
        Me.Line17.Y1 = 0.0!
        Me.Line17.Y2 = 0.3125!
        '
        'rptEDI_Report_04
        '
        Me.MasterReport = False
        Me.PageSettings.Margins.Bottom = 0.1!
        Me.PageSettings.Margins.Left = 0.1!
        Me.PageSettings.Margins.Right = 0.1!
        Me.PageSettings.Margins.Top = 0.1!
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
        CType(Me.txtDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFORM_COUNT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSUM_FOB, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label14, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDateLenght, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTOTAL_SUM_FOB, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTOTAL_FORM_COUNT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub
    Friend WithEvents Detail1 As DataDynamics.ActiveReports.Detail
    Friend WithEvents txtDescription As DataDynamics.ActiveReports.TextBox
    Friend WithEvents txtFORM_COUNT As DataDynamics.ActiveReports.TextBox
    Friend WithEvents txtSUM_FOB As DataDynamics.ActiveReports.TextBox
    Friend WithEvents Line9 As DataDynamics.ActiveReports.Line
    Friend WithEvents Line10 As DataDynamics.ActiveReports.Line
    Friend WithEvents Line11 As DataDynamics.ActiveReports.Line
    Friend WithEvents Line12 As DataDynamics.ActiveReports.Line
    Friend WithEvents ReportHeader1 As DataDynamics.ActiveReports.ReportHeader
    Friend WithEvents Label1 As DataDynamics.ActiveReports.Label
    Friend WithEvents ReportFooter1 As DataDynamics.ActiveReports.ReportFooter
    Friend WithEvents PageHeader1 As DataDynamics.ActiveReports.PageHeader
    Friend WithEvents Label4 As DataDynamics.ActiveReports.Label
    Friend WithEvents Label5 As DataDynamics.ActiveReports.Label
    Friend WithEvents Label7 As DataDynamics.ActiveReports.Label
    Friend WithEvents Line1 As DataDynamics.ActiveReports.Line
    Friend WithEvents Line2 As DataDynamics.ActiveReports.Line
    Friend WithEvents Line3 As DataDynamics.ActiveReports.Line
    Friend WithEvents Line4 As DataDynamics.ActiveReports.Line
    Friend WithEvents Line5 As DataDynamics.ActiveReports.Line
    Friend WithEvents Line7 As DataDynamics.ActiveReports.Line
    Friend WithEvents Label14 As DataDynamics.ActiveReports.Label
    Friend WithEvents lblDateLenght As DataDynamics.ActiveReports.Label
    Friend WithEvents PageFooter1 As DataDynamics.ActiveReports.PageFooter
    Friend WithEvents GroupHeader1 As DataDynamics.ActiveReports.GroupHeader
    Friend WithEvents GroupFooter1 As DataDynamics.ActiveReports.GroupFooter
    Friend WithEvents txtTOTAL_SUM_FOB As DataDynamics.ActiveReports.TextBox
    Friend WithEvents Label8 As DataDynamics.ActiveReports.Label
    Friend WithEvents txtTOTAL_FORM_COUNT As DataDynamics.ActiveReports.TextBox
    Friend WithEvents Line13 As DataDynamics.ActiveReports.Line
    Friend WithEvents Line8 As DataDynamics.ActiveReports.Line
    Friend WithEvents Line14 As DataDynamics.ActiveReports.Line
    Friend WithEvents Line15 As DataDynamics.ActiveReports.Line
    Friend WithEvents Line16 As DataDynamics.ActiveReports.Line
    Friend WithEvents Line17 As DataDynamics.ActiveReports.Line
End Class 
