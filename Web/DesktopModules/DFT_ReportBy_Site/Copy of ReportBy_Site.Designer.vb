<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class ReportBy_Site 
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
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(ReportBy_Site))
        Me.PageHeader1 = New DataDynamics.ActiveReports.PageHeader
        Me.Label1 = New DataDynamics.ActiveReports.Label
        Me.Detail1 = New DataDynamics.ActiveReports.Detail
        Me.txtnet_weight = New DataDynamics.ActiveReports.TextBox
        Me.txttariff_code = New DataDynamics.ActiveReports.TextBox
        Me.txtreference_code2 = New DataDynamics.ActiveReports.TextBox
        Me.txtDecountry_name = New DataDynamics.ActiveReports.TextBox
        Me.Line2 = New DataDynamics.ActiveReports.Line
        Me.Line4 = New DataDynamics.ActiveReports.Line
        Me.Line6 = New DataDynamics.ActiveReports.Line
        Me.txtinvh_run_auto = New DataDynamics.ActiveReports.TextBox
        Me.PageFooter1 = New DataDynamics.ActiveReports.PageFooter
        Me.ReportInfo1 = New DataDynamics.ActiveReports.ReportInfo
        Me.GroupHeader1 = New DataDynamics.ActiveReports.GroupHeader
        Me.txtform_name = New DataDynamics.ActiveReports.TextBox
        Me.txtform_type = New DataDynamics.ActiveReports.TextBox
        Me.Line8 = New DataDynamics.ActiveReports.Line
        Me.Line9 = New DataDynamics.ActiveReports.Line
        Me.GroupFooter1 = New DataDynamics.ActiveReports.GroupFooter
        Me.GroupHeader2 = New DataDynamics.ActiveReports.GroupHeader
        Me.txtcompany_taxno = New DataDynamics.ActiveReports.TextBox
        Me.txtcompany_name = New DataDynamics.ActiveReports.TextBox
        Me.txtNumRowCount = New DataDynamics.ActiveReports.TextBox
        Me.Line7 = New DataDynamics.ActiveReports.Line
        Me.GroupFooter2 = New DataDynamics.ActiveReports.GroupFooter
        Me.GroupHeader3 = New DataDynamics.ActiveReports.GroupHeader
        Me.TextBox1 = New DataDynamics.ActiveReports.TextBox
        Me.Line5 = New DataDynamics.ActiveReports.Line
        Me.GroupFooter3 = New DataDynamics.ActiveReports.GroupFooter
        Me.GroupHeader4 = New DataDynamics.ActiveReports.GroupHeader
        Me.Line3 = New DataDynamics.ActiveReports.Line
        Me.GroupFooter4 = New DataDynamics.ActiveReports.GroupFooter
        Me.Line1 = New DataDynamics.ActiveReports.Line
        CType(Me.Label1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtnet_weight, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txttariff_code, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtreference_code2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDecountry_name, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtinvh_run_auto, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ReportInfo1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtform_name, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtform_type, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtcompany_taxno, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtcompany_name, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNumRowCount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TextBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'PageHeader1
        '
        Me.PageHeader1.Controls.AddRange(New DataDynamics.ActiveReports.ARControl() {Me.Label1})
        Me.PageHeader1.Height = 0.46875!
        Me.PageHeader1.Name = "PageHeader1"
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
        Me.Label1.Height = 0.3937008!
        Me.Label1.HyperLink = Nothing
        Me.Label1.Left = 0.0!
        Me.Label1.Name = "Label1"
        Me.Label1.Style = "ddo-char-set: 222; text-align: center; font-weight: bold; font-size: 20.25pt; fon" & _
            "t-family: AngsanaUPC; vertical-align: middle; "
        Me.Label1.Text = "รายงาน"
        Me.Label1.Top = 0.0!
        Me.Label1.Width = 8.636811!
        '
        'Detail1
        '
        Me.Detail1.ColumnSpacing = 0.0!
        Me.Detail1.Controls.AddRange(New DataDynamics.ActiveReports.ARControl() {Me.txtnet_weight, Me.txttariff_code, Me.txtreference_code2, Me.txtDecountry_name, Me.Line2, Me.Line4, Me.Line6, Me.Line1})
        Me.Detail1.Height = 0.2952756!
        Me.Detail1.Name = "Detail1"
        '
        'txtnet_weight
        '
        Me.txtnet_weight.Border.BottomColor = System.Drawing.Color.Black
        Me.txtnet_weight.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtnet_weight.Border.LeftColor = System.Drawing.Color.Black
        Me.txtnet_weight.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtnet_weight.Border.RightColor = System.Drawing.Color.Black
        Me.txtnet_weight.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtnet_weight.Border.TopColor = System.Drawing.Color.Black
        Me.txtnet_weight.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtnet_weight.DataField = "net_weight"
        Me.txtnet_weight.Height = 0.2706693!
        Me.txtnet_weight.Left = 6.766733!
        Me.txtnet_weight.Name = "txtnet_weight"
        Me.txtnet_weight.OutputFormat = resources.GetString("txtnet_weight.OutputFormat")
        Me.txtnet_weight.Style = "ddo-char-set: 222; text-align: right; font-size: 14.25pt; font-family: AngsanaUPC" & _
            "; vertical-align: middle; "
        Me.txtnet_weight.Text = "net_weight"
        Me.txtnet_weight.Top = 0.0!
        Me.txtnet_weight.Width = 1.500984!
        '
        'txttariff_code
        '
        Me.txttariff_code.Border.BottomColor = System.Drawing.Color.Black
        Me.txttariff_code.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txttariff_code.Border.LeftColor = System.Drawing.Color.Black
        Me.txttariff_code.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txttariff_code.Border.RightColor = System.Drawing.Color.Black
        Me.txttariff_code.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txttariff_code.Border.TopColor = System.Drawing.Color.Black
        Me.txttariff_code.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txttariff_code.DataField = "tariff_code"
        Me.txttariff_code.Height = 0.2706693!
        Me.txttariff_code.Left = 4.72441!
        Me.txttariff_code.Name = "txttariff_code"
        Me.txttariff_code.Style = "ddo-char-set: 222; font-size: 14.25pt; font-family: AngsanaUPC; vertical-align: m" & _
            "iddle; "
        Me.txttariff_code.Text = "tariff_code"
        Me.txttariff_code.Top = 0.0!
        Me.txttariff_code.Width = 1.968504!
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
        Me.txtreference_code2.Height = 0.2706693!
        Me.txtreference_code2.Left = 0.7874016!
        Me.txtreference_code2.Name = "txtreference_code2"
        Me.txtreference_code2.Style = "ddo-char-set: 222; font-size: 14.25pt; font-family: AngsanaUPC; vertical-align: m" & _
            "iddle; "
        Me.txtreference_code2.Text = "reference_code2"
        Me.txtreference_code2.Top = 0.0!
        Me.txtreference_code2.Width = 1.599409!
        '
        'txtDecountry_name
        '
        Me.txtDecountry_name.Border.BottomColor = System.Drawing.Color.Black
        Me.txtDecountry_name.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtDecountry_name.Border.LeftColor = System.Drawing.Color.Black
        Me.txtDecountry_name.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtDecountry_name.Border.RightColor = System.Drawing.Color.Black
        Me.txtDecountry_name.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtDecountry_name.Border.TopColor = System.Drawing.Color.Black
        Me.txtDecountry_name.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtDecountry_name.DataField = "country_name"
        Me.txtDecountry_name.Height = 0.2706693!
        Me.txtDecountry_name.Left = 2.485236!
        Me.txtDecountry_name.Name = "txtDecountry_name"
        Me.txtDecountry_name.Style = "ddo-char-set: 222; font-size: 14.25pt; font-family: AngsanaUPC; vertical-align: m" & _
            "iddle; "
        Me.txtDecountry_name.Text = "country_name"
        Me.txtDecountry_name.Top = 0.0!
        Me.txtDecountry_name.Width = 2.165354!
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
        Me.Line2.Height = 0.2706693!
        Me.Line2.Left = 2.436024!
        Me.Line2.LineWeight = 1.0!
        Me.Line2.Name = "Line2"
        Me.Line2.Top = 0.0!
        Me.Line2.Width = 0.0!
        Me.Line2.X1 = 2.436024!
        Me.Line2.X2 = 2.436024!
        Me.Line2.Y1 = 0.0!
        Me.Line2.Y2 = 0.2706693!
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
        Me.Line4.Height = 0.2706693!
        Me.Line4.Left = 4.699803!
        Me.Line4.LineWeight = 1.0!
        Me.Line4.Name = "Line4"
        Me.Line4.Top = 0.0!
        Me.Line4.Width = 0.0!
        Me.Line4.X1 = 4.699803!
        Me.Line4.X2 = 4.699803!
        Me.Line4.Y1 = 0.0!
        Me.Line4.Y2 = 0.2706693!
        '
        'Line6
        '
        Me.Line6.Border.BottomColor = System.Drawing.Color.Black
        Me.Line6.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Line6.Border.LeftColor = System.Drawing.Color.Black
        Me.Line6.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Line6.Border.RightColor = System.Drawing.Color.Black
        Me.Line6.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Line6.Border.TopColor = System.Drawing.Color.Black
        Me.Line6.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.Line6.Height = 0.2706693!
        Me.Line6.Left = 6.742126!
        Me.Line6.LineWeight = 1.0!
        Me.Line6.Name = "Line6"
        Me.Line6.Top = 0.0!
        Me.Line6.Width = 0.0!
        Me.Line6.X1 = 6.742126!
        Me.Line6.X2 = 6.742126!
        Me.Line6.Y1 = 0.0!
        Me.Line6.Y2 = 0.2706693!
        '
        'txtinvh_run_auto
        '
        Me.txtinvh_run_auto.Border.BottomColor = System.Drawing.Color.Black
        Me.txtinvh_run_auto.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtinvh_run_auto.Border.LeftColor = System.Drawing.Color.Black
        Me.txtinvh_run_auto.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtinvh_run_auto.Border.RightColor = System.Drawing.Color.Black
        Me.txtinvh_run_auto.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtinvh_run_auto.Border.TopColor = System.Drawing.Color.Black
        Me.txtinvh_run_auto.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtinvh_run_auto.DataField = "invh_run_auto"
        Me.txtinvh_run_auto.Height = 0.2706693!
        Me.txtinvh_run_auto.Left = 0.6397638!
        Me.txtinvh_run_auto.Name = "txtinvh_run_auto"
        Me.txtinvh_run_auto.Style = "ddo-char-set: 222; font-size: 14.25pt; font-family: AngsanaUPC; vertical-align: m" & _
            "iddle; "
        Me.txtinvh_run_auto.Text = "invh_run_auto"
        Me.txtinvh_run_auto.Top = 0.0!
        Me.txtinvh_run_auto.Width = 7.677166!
        '
        'PageFooter1
        '
        Me.PageFooter1.Controls.AddRange(New DataDynamics.ActiveReports.ARControl() {Me.ReportInfo1})
        Me.PageFooter1.Height = 0.3198819!
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
        Me.ReportInfo1.FormatString = Nothing
        Me.ReportInfo1.Height = 0.3198819!
        Me.ReportInfo1.Left = 7.480315!
        Me.ReportInfo1.Name = "ReportInfo1"
        Me.ReportInfo1.Style = "ddo-char-set: 222; text-align: center; font-weight: bold; font-size: 14.25pt; fon" & _
            "t-family: AngsanaUPC; vertical-align: middle; "
        Me.ReportInfo1.Top = 0.0!
        Me.ReportInfo1.Width = 1.082677!
        '
        'GroupHeader1
        '
        Me.GroupHeader1.Controls.AddRange(New DataDynamics.ActiveReports.ARControl() {Me.txtform_name, Me.txtform_type, Me.Line8, Me.Line9})
        Me.GroupHeader1.DataField = "form_type"
        Me.GroupHeader1.Height = 0.2913386!
        Me.GroupHeader1.Name = "GroupHeader1"
        '
        'txtform_name
        '
        Me.txtform_name.Border.BottomColor = System.Drawing.Color.Black
        Me.txtform_name.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtform_name.Border.LeftColor = System.Drawing.Color.Black
        Me.txtform_name.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtform_name.Border.RightColor = System.Drawing.Color.Black
        Me.txtform_name.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtform_name.Border.TopColor = System.Drawing.Color.Black
        Me.txtform_name.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtform_name.DataField = "form_name"
        Me.txtform_name.Height = 0.2706693!
        Me.txtform_name.Left = 0.0246063!
        Me.txtform_name.Name = "txtform_name"
        Me.txtform_name.Style = "ddo-char-set: 222; font-size: 14.25pt; font-family: AngsanaUPC; vertical-align: m" & _
            "iddle; "
        Me.txtform_name.Text = "form_name"
        Me.txtform_name.Top = 0.0!
        Me.txtform_name.Width = 6.471457!
        '
        'txtform_type
        '
        Me.txtform_type.Border.BottomColor = System.Drawing.Color.Black
        Me.txtform_type.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtform_type.Border.LeftColor = System.Drawing.Color.Black
        Me.txtform_type.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtform_type.Border.RightColor = System.Drawing.Color.Black
        Me.txtform_type.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtform_type.Border.TopColor = System.Drawing.Color.Black
        Me.txtform_type.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtform_type.DataField = "form_type"
        Me.txtform_type.Height = 0.2706693!
        Me.txtform_type.Left = 3.863189!
        Me.txtform_type.Name = "txtform_type"
        Me.txtform_type.Style = "color: Red; ddo-char-set: 222; font-size: 14.25pt; font-family: AngsanaUPC; verti" & _
            "cal-align: middle; "
        Me.txtform_type.Text = "form_type"
        Me.txtform_type.Top = 0.3444882!
        Me.txtform_type.Visible = False
        Me.txtform_type.Width = 2.632874!
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
        Me.Line8.Left = 0.0!
        Me.Line8.LineWeight = 1.0!
        Me.Line8.Name = "Line8"
        Me.Line8.Top = 0.0!
        Me.Line8.Width = 8.661417!
        Me.Line8.X1 = 0.0!
        Me.Line8.X2 = 8.661417!
        Me.Line8.Y1 = 0.0!
        Me.Line8.Y2 = 0.0!
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
        Me.Line9.Height = 0.0!
        Me.Line9.Left = 0.0!
        Me.Line9.LineWeight = 1.0!
        Me.Line9.Name = "Line9"
        Me.Line9.Top = 0.2706693!
        Me.Line9.Width = 8.661418!
        Me.Line9.X1 = 0.0!
        Me.Line9.X2 = 8.661418!
        Me.Line9.Y1 = 0.2706693!
        Me.Line9.Y2 = 0.2706693!
        '
        'GroupFooter1
        '
        Me.GroupFooter1.Height = 0.0!
        Me.GroupFooter1.Name = "GroupFooter1"
        '
        'GroupHeader2
        '
        Me.GroupHeader2.Controls.AddRange(New DataDynamics.ActiveReports.ARControl() {Me.txtcompany_taxno, Me.txtcompany_name, Me.txtNumRowCount, Me.Line7})
        Me.GroupHeader2.DataField = "company_taxno"
        Me.GroupHeader2.Height = 0.2913386!
        Me.GroupHeader2.Name = "GroupHeader2"
        '
        'txtcompany_taxno
        '
        Me.txtcompany_taxno.Border.BottomColor = System.Drawing.Color.Black
        Me.txtcompany_taxno.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtcompany_taxno.Border.LeftColor = System.Drawing.Color.Black
        Me.txtcompany_taxno.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtcompany_taxno.Border.RightColor = System.Drawing.Color.Black
        Me.txtcompany_taxno.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtcompany_taxno.Border.TopColor = System.Drawing.Color.Black
        Me.txtcompany_taxno.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtcompany_taxno.DataField = "company_taxno"
        Me.txtcompany_taxno.Height = 0.2755905!
        Me.txtcompany_taxno.Left = 0.6889763!
        Me.txtcompany_taxno.Name = "txtcompany_taxno"
        Me.txtcompany_taxno.Style = "ddo-char-set: 222; font-size: 14.25pt; font-family: AngsanaUPC; vertical-align: m" & _
            "iddle; "
        Me.txtcompany_taxno.Text = "company_taxno"
        Me.txtcompany_taxno.Top = 0.2706693!
        Me.txtcompany_taxno.Visible = False
        Me.txtcompany_taxno.Width = 1.0!
        '
        'txtcompany_name
        '
        Me.txtcompany_name.Border.BottomColor = System.Drawing.Color.Black
        Me.txtcompany_name.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtcompany_name.Border.LeftColor = System.Drawing.Color.Black
        Me.txtcompany_name.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtcompany_name.Border.RightColor = System.Drawing.Color.Black
        Me.txtcompany_name.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtcompany_name.Border.TopColor = System.Drawing.Color.Black
        Me.txtcompany_name.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtcompany_name.DataField = "company_name"
        Me.txtcompany_name.Height = 0.2706693!
        Me.txtcompany_name.Left = 0.4675197!
        Me.txtcompany_name.Name = "txtcompany_name"
        Me.txtcompany_name.Style = "color: Red; ddo-char-set: 222; font-size: 14.25pt; font-family: AngsanaUPC; verti" & _
            "cal-align: middle; "
        Me.txtcompany_name.Text = "company_name"
        Me.txtcompany_name.Top = 0.0!
        Me.txtcompany_name.Width = 8.095473!
        '
        'txtNumRowCount
        '
        Me.txtNumRowCount.Border.BottomColor = System.Drawing.Color.Black
        Me.txtNumRowCount.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtNumRowCount.Border.LeftColor = System.Drawing.Color.Black
        Me.txtNumRowCount.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtNumRowCount.Border.RightColor = System.Drawing.Color.Black
        Me.txtNumRowCount.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtNumRowCount.Border.TopColor = System.Drawing.Color.Black
        Me.txtNumRowCount.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtNumRowCount.Height = 0.2716535!
        Me.txtNumRowCount.Left = 0.0246063!
        Me.txtNumRowCount.Name = "txtNumRowCount"
        Me.txtNumRowCount.Style = "ddo-char-set: 222; text-align: center; font-size: 14.25pt; font-family: AngsanaUP" & _
            "C; vertical-align: middle; "
        Me.txtNumRowCount.Text = Nothing
        Me.txtNumRowCount.Top = 0.0!
        Me.txtNumRowCount.Width = 0.4429134!
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
        Me.Line7.Height = 0.0!
        Me.Line7.Left = 0.0!
        Me.Line7.LineWeight = 1.0!
        Me.Line7.Name = "Line7"
        Me.Line7.Top = 0.2706693!
        Me.Line7.Width = 8.661418!
        Me.Line7.X1 = 0.0!
        Me.Line7.X2 = 8.661418!
        Me.Line7.Y1 = 0.2706693!
        Me.Line7.Y2 = 0.2706693!
        '
        'GroupFooter2
        '
        Me.GroupFooter2.Height = 0.0!
        Me.GroupFooter2.Name = "GroupFooter2"
        '
        'GroupHeader3
        '
        Me.GroupHeader3.Controls.AddRange(New DataDynamics.ActiveReports.ARControl() {Me.TextBox1, Me.Line5})
        Me.GroupHeader3.DataField = "country_name"
        Me.GroupHeader3.Height = 0.2913386!
        Me.GroupHeader3.Name = "GroupHeader3"
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
        Me.TextBox1.DataField = "country_name"
        Me.TextBox1.Height = 0.2706693!
        Me.TextBox1.Left = 0.1722441!
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Style = "ddo-char-set: 222; font-size: 14.25pt; font-family: AngsanaUPC; vertical-align: m" & _
            "iddle; "
        Me.TextBox1.Text = "country_name"
        Me.TextBox1.Top = 0.0!
        Me.TextBox1.Width = 7.997047!
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
        Me.Line5.Height = 0.0!
        Me.Line5.Left = 0.0!
        Me.Line5.LineWeight = 1.0!
        Me.Line5.Name = "Line5"
        Me.Line5.Top = 0.2706693!
        Me.Line5.Width = 8.661418!
        Me.Line5.X1 = 0.0!
        Me.Line5.X2 = 8.661418!
        Me.Line5.Y1 = 0.2706693!
        Me.Line5.Y2 = 0.2706693!
        '
        'GroupFooter3
        '
        Me.GroupFooter3.Height = 0.0!
        Me.GroupFooter3.Name = "GroupFooter3"
        '
        'GroupHeader4
        '
        Me.GroupHeader4.Controls.AddRange(New DataDynamics.ActiveReports.ARControl() {Me.txtinvh_run_auto, Me.Line3})
        Me.GroupHeader4.DataField = "invh_run_auto"
        Me.GroupHeader4.Height = 0.2913386!
        Me.GroupHeader4.Name = "GroupHeader4"
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
        Me.Line3.Top = 0.2706693!
        Me.Line3.Width = 8.661418!
        Me.Line3.X1 = 0.0!
        Me.Line3.X2 = 8.661418!
        Me.Line3.Y1 = 0.2706693!
        Me.Line3.Y2 = 0.2706693!
        '
        'GroupFooter4
        '
        Me.GroupFooter4.Height = 0.0!
        Me.GroupFooter4.Name = "GroupFooter4"
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
        Me.Line1.Top = 0.281086!
        Me.Line1.Width = 8.661418!
        Me.Line1.X1 = 0.0!
        Me.Line1.X2 = 8.661418!
        Me.Line1.Y1 = 0.281086!
        Me.Line1.Y2 = 0.281086!
        '
        'ReportBy_Site
        '
        Me.MasterReport = False
        Me.PageSettings.PaperHeight = 11.0!
        Me.PageSettings.PaperWidth = 8.5!
        Me.PrintWidth = 8.661417!
        Me.Sections.Add(Me.PageHeader1)
        Me.Sections.Add(Me.GroupHeader1)
        Me.Sections.Add(Me.GroupHeader2)
        Me.Sections.Add(Me.GroupHeader3)
        Me.Sections.Add(Me.GroupHeader4)
        Me.Sections.Add(Me.Detail1)
        Me.Sections.Add(Me.GroupFooter4)
        Me.Sections.Add(Me.GroupFooter3)
        Me.Sections.Add(Me.GroupFooter2)
        Me.Sections.Add(Me.GroupFooter1)
        Me.Sections.Add(Me.PageFooter1)
        Me.StyleSheet.Add(New DDCssLib.StyleSheetRule("font-family: Arial; font-style: normal; text-decoration: none; font-weight: norma" & _
                    "l; font-size: 10pt; color: Black; ddo-char-set: 204; ", "Normal"))
        Me.StyleSheet.Add(New DDCssLib.StyleSheetRule("font-size: 16pt; font-weight: bold; ", "Heading1", "Normal"))
        Me.StyleSheet.Add(New DDCssLib.StyleSheetRule("font-family: Times New Roman; font-size: 14pt; font-weight: bold; font-style: ita" & _
                    "lic; ", "Heading2", "Normal"))
        Me.StyleSheet.Add(New DDCssLib.StyleSheetRule("font-size: 13pt; font-weight: bold; ", "Heading3", "Normal"))
        CType(Me.Label1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtnet_weight, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txttariff_code, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtreference_code2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDecountry_name, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtinvh_run_auto, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ReportInfo1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtform_name, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtform_type, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtcompany_taxno, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtcompany_name, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNumRowCount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TextBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub
    Friend WithEvents txtinvh_run_auto As DataDynamics.ActiveReports.TextBox
    Friend WithEvents txtnet_weight As DataDynamics.ActiveReports.TextBox
    Friend WithEvents txttariff_code As DataDynamics.ActiveReports.TextBox
    Friend WithEvents GroupHeader1 As DataDynamics.ActiveReports.GroupHeader
    Friend WithEvents txtform_name As DataDynamics.ActiveReports.TextBox
    Friend WithEvents GroupFooter1 As DataDynamics.ActiveReports.GroupFooter
    Friend WithEvents GroupHeader2 As DataDynamics.ActiveReports.GroupHeader
    Friend WithEvents txtcompany_taxno As DataDynamics.ActiveReports.TextBox
    Friend WithEvents GroupFooter2 As DataDynamics.ActiveReports.GroupFooter
    Friend WithEvents GroupHeader3 As DataDynamics.ActiveReports.GroupHeader
    Friend WithEvents TextBox1 As DataDynamics.ActiveReports.TextBox
    Friend WithEvents GroupFooter3 As DataDynamics.ActiveReports.GroupFooter
    Friend WithEvents GroupHeader4 As DataDynamics.ActiveReports.GroupHeader
    Friend WithEvents GroupFooter4 As DataDynamics.ActiveReports.GroupFooter
    Friend WithEvents txtreference_code2 As DataDynamics.ActiveReports.TextBox
    Friend WithEvents txtcompany_name As DataDynamics.ActiveReports.TextBox
    Friend WithEvents Label1 As DataDynamics.ActiveReports.Label
    Friend WithEvents txtNumRowCount As DataDynamics.ActiveReports.TextBox
    Friend WithEvents txtform_type As DataDynamics.ActiveReports.TextBox
    Friend WithEvents txtDecountry_name As DataDynamics.ActiveReports.TextBox
    Friend WithEvents ReportInfo1 As DataDynamics.ActiveReports.ReportInfo
    Friend WithEvents Line8 As DataDynamics.ActiveReports.Line
    Friend WithEvents Line9 As DataDynamics.ActiveReports.Line
    Friend WithEvents Line7 As DataDynamics.ActiveReports.Line
    Friend WithEvents Line5 As DataDynamics.ActiveReports.Line
    Friend WithEvents Line3 As DataDynamics.ActiveReports.Line
    Friend WithEvents Line2 As DataDynamics.ActiveReports.Line
    Friend WithEvents Line4 As DataDynamics.ActiveReports.Line
    Friend WithEvents Line6 As DataDynamics.ActiveReports.Line
    Friend WithEvents Line1 As DataDynamics.ActiveReports.Line
End Class
