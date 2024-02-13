<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class receipt_rpt 
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(receipt_rpt))
        Me.Label2 = New DataDynamics.ActiveReports.Label
        Me.Label1 = New DataDynamics.ActiveReports.Label
        Me.txtreceipt_name = New DataDynamics.ActiveReports.TextBox
        Me.txtbill_date = New DataDynamics.ActiveReports.TextBox
        Me.txtbill_no = New DataDynamics.ActiveReports.TextBox
        Me.txtCount_Tol = New DataDynamics.ActiveReports.TextBox
        Me.txtPage2 = New DataDynamics.ActiveReports.TextBox
        Me.txtTemp_reference_code2 = New DataDynamics.ActiveReports.TextBox
        Me.txtTempt_reference_code2 = New DataDynamics.ActiveReports.TextBox
        Me.txttotal_set = New DataDynamics.ActiveReports.TextBox
        Me.txtset_price = New DataDynamics.ActiveReports.TextBox
        Me.txtamt = New DataDynamics.ActiveReports.TextBox
        Me.txtreference_code2 = New DataDynamics.ActiveReports.TextBox
        Me.txtSumamt = New DataDynamics.ActiveReports.TextBox
        Me.txtbText = New DataDynamics.ActiveReports.TextBox
        Me.txtreceipt_by = New DataDynamics.ActiveReports.TextBox
        Me.TextBox1 = New DataDynamics.ActiveReports.TextBox
        Me.txtNumpageForm = New DataDynamics.ActiveReports.TextBox
        Me.txtnumTotalpage = New DataDynamics.ActiveReports.TextBox
        Me.PageHeader1 = New DataDynamics.ActiveReports.PageHeader
        Me.Detail1 = New DataDynamics.ActiveReports.Detail
        Me.PageFooter1 = New DataDynamics.ActiveReports.PageFooter
        Me.ReportHeader1 = New DataDynamics.ActiveReports.ReportHeader
        Me.ReportFooter1 = New DataDynamics.ActiveReports.ReportFooter
        CType(Me.Label2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtreceipt_name, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtbill_date, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtbill_no, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCount_Tol, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPage2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTemp_reference_code2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTempt_reference_code2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txttotal_set, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtset_price, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtamt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtreference_code2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSumamt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtbText, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtreceipt_by, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TextBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNumpageForm, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtnumTotalpage, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
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
        resources.ApplyResources(Me.Label2, "Label2")
        Me.Label2.Height = 0.2706693!
        Me.Label2.HyperLink = Nothing
        Me.Label2.Left = 6.496063!
        Me.Label2.Name = "Label2"
        Me.Label2.Top = 0.6643701!
        Me.Label2.Width = 1.008858!
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
        resources.ApplyResources(Me.Label1, "Label1")
        Me.Label1.Height = 0.25!
        Me.Label1.HyperLink = Nothing
        Me.Label1.Left = 6.200788!
        Me.Label1.Name = "Label1"
        Me.Label1.Top = 0.6643701!
        Me.Label1.Width = 0.5!
        '
        'txtreceipt_name
        '
        Me.txtreceipt_name.Border.BottomColor = System.Drawing.Color.Black
        Me.txtreceipt_name.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtreceipt_name.Border.LeftColor = System.Drawing.Color.Black
        Me.txtreceipt_name.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtreceipt_name.Border.RightColor = System.Drawing.Color.Black
        Me.txtreceipt_name.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtreceipt_name.Border.TopColor = System.Drawing.Color.Black
        Me.txtreceipt_name.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        resources.ApplyResources(Me.txtreceipt_name, "txtreceipt_name")
        Me.txtreceipt_name.DataField = "receipt_name"
        Me.txtreceipt_name.DistinctField = Nothing
        Me.txtreceipt_name.Height = 0.5625!
        Me.txtreceipt_name.Left = 1.624016!
        Me.txtreceipt_name.Name = "txtreceipt_name"
        Me.txtreceipt_name.OutputFormat = resources.GetString("txtreceipt_name.OutputFormat")
        Me.txtreceipt_name.SummaryGroup = Nothing
        Me.txtreceipt_name.Top = 2.411417!
        Me.txtreceipt_name.Width = 6.5!
        '
        'txtbill_date
        '
        Me.txtbill_date.Border.BottomColor = System.Drawing.Color.Black
        Me.txtbill_date.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtbill_date.Border.LeftColor = System.Drawing.Color.Black
        Me.txtbill_date.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtbill_date.Border.RightColor = System.Drawing.Color.Black
        Me.txtbill_date.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtbill_date.Border.TopColor = System.Drawing.Color.Black
        Me.txtbill_date.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        resources.ApplyResources(Me.txtbill_date, "txtbill_date")
        Me.txtbill_date.DistinctField = Nothing
        Me.txtbill_date.Height = 0.25!
        Me.txtbill_date.Left = 0.6643701!
        Me.txtbill_date.Name = "txtbill_date"
        Me.txtbill_date.SummaryGroup = Nothing
        Me.txtbill_date.Top = 1.237861!
        Me.txtbill_date.Width = 2.1875!
        '
        'txtbill_no
        '
        Me.txtbill_no.Border.BottomColor = System.Drawing.Color.Black
        Me.txtbill_no.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtbill_no.Border.LeftColor = System.Drawing.Color.Black
        Me.txtbill_no.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtbill_no.Border.RightColor = System.Drawing.Color.Black
        Me.txtbill_no.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtbill_no.Border.TopColor = System.Drawing.Color.Black
        Me.txtbill_no.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        resources.ApplyResources(Me.txtbill_no, "txtbill_no")
        Me.txtbill_no.DataField = "bill_no"
        Me.txtbill_no.DistinctField = Nothing
        Me.txtbill_no.Height = 0.25!
        Me.txtbill_no.Left = 6.4375!
        Me.txtbill_no.Name = "txtbill_no"
        Me.txtbill_no.OutputFormat = resources.GetString("txtbill_no.OutputFormat")
        Me.txtbill_no.SummaryGroup = Nothing
        Me.txtbill_no.Top = 1.25!
        Me.txtbill_no.Width = 1.4375!
        '
        'txtCount_Tol
        '
        Me.txtCount_Tol.Border.BottomColor = System.Drawing.Color.Black
        Me.txtCount_Tol.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtCount_Tol.Border.LeftColor = System.Drawing.Color.Black
        Me.txtCount_Tol.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtCount_Tol.Border.RightColor = System.Drawing.Color.Black
        Me.txtCount_Tol.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtCount_Tol.Border.TopColor = System.Drawing.Color.Black
        Me.txtCount_Tol.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        resources.ApplyResources(Me.txtCount_Tol, "txtCount_Tol")
        Me.txtCount_Tol.DistinctField = Nothing
        Me.txtCount_Tol.Height = 0.246063!
        Me.txtCount_Tol.Left = 7.037402!
        Me.txtCount_Tol.Name = "txtCount_Tol"
        Me.txtCount_Tol.SummaryGroup = Nothing
        Me.txtCount_Tol.SummaryType = DataDynamics.ActiveReports.SummaryType.PageCount
        Me.txtCount_Tol.Top = 0.6643701!
        Me.txtCount_Tol.Width = 1.353346!
        '
        'txtPage2
        '
        Me.txtPage2.Border.BottomColor = System.Drawing.Color.Black
        Me.txtPage2.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtPage2.Border.LeftColor = System.Drawing.Color.Black
        Me.txtPage2.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtPage2.Border.RightColor = System.Drawing.Color.Black
        Me.txtPage2.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtPage2.Border.TopColor = System.Drawing.Color.Black
        Me.txtPage2.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        resources.ApplyResources(Me.txtPage2, "txtPage2")
        Me.txtPage2.DistinctField = Nothing
        Me.txtPage2.Height = 0.246063!
        Me.txtPage2.Left = 6.594488!
        Me.txtPage2.Name = "txtPage2"
        Me.txtPage2.SummaryGroup = Nothing
        Me.txtPage2.Top = 0.6643701!
        Me.txtPage2.Width = 0.3690945!
        '
        'txtTemp_reference_code2
        '
        Me.txtTemp_reference_code2.Border.BottomColor = System.Drawing.Color.Black
        Me.txtTemp_reference_code2.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtTemp_reference_code2.Border.LeftColor = System.Drawing.Color.Black
        Me.txtTemp_reference_code2.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtTemp_reference_code2.Border.RightColor = System.Drawing.Color.Black
        Me.txtTemp_reference_code2.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtTemp_reference_code2.Border.TopColor = System.Drawing.Color.Black
        Me.txtTemp_reference_code2.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        resources.ApplyResources(Me.txtTemp_reference_code2, "txtTemp_reference_code2")
        Me.txtTemp_reference_code2.DistinctField = Nothing
        Me.txtTemp_reference_code2.Height = 0.246063!
        Me.txtTemp_reference_code2.Left = 0.25!
        Me.txtTemp_reference_code2.Name = "txtTemp_reference_code2"
        Me.txtTemp_reference_code2.OutputFormat = resources.GetString("txtTemp_reference_code2.OutputFormat")
        Me.txtTemp_reference_code2.SummaryGroup = Nothing
        Me.txtTemp_reference_code2.Text = Nothing
        Me.txtTemp_reference_code2.Top = 0.0!
        Me.txtTemp_reference_code2.Width = 3.395669!
        '
        'txtTempt_reference_code2
        '
        Me.txtTempt_reference_code2.Border.BottomColor = System.Drawing.Color.Black
        Me.txtTempt_reference_code2.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtTempt_reference_code2.Border.LeftColor = System.Drawing.Color.Black
        Me.txtTempt_reference_code2.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtTempt_reference_code2.Border.RightColor = System.Drawing.Color.Black
        Me.txtTempt_reference_code2.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtTempt_reference_code2.Border.TopColor = System.Drawing.Color.Black
        Me.txtTempt_reference_code2.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        resources.ApplyResources(Me.txtTempt_reference_code2, "txtTempt_reference_code2")
        Me.txtTempt_reference_code2.DistinctField = Nothing
        Me.txtTempt_reference_code2.Height = 0.246063!
        Me.txtTempt_reference_code2.Left = 3.838583!
        Me.txtTempt_reference_code2.Name = "txtTempt_reference_code2"
        Me.txtTempt_reference_code2.OutputFormat = resources.GetString("txtTempt_reference_code2.OutputFormat")
        Me.txtTempt_reference_code2.SummaryGroup = Nothing
        Me.txtTempt_reference_code2.Text = Nothing
        Me.txtTempt_reference_code2.Top = 0.0!
        Me.txtTempt_reference_code2.Width = 1.353347!
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
        resources.ApplyResources(Me.txttotal_set, "txttotal_set")
        Me.txttotal_set.DataField = "total_set"
        Me.txttotal_set.DistinctField = Nothing
        Me.txttotal_set.Height = 0.246063!
        Me.txttotal_set.Left = 5.265748!
        Me.txttotal_set.Name = "txttotal_set"
        Me.txttotal_set.OutputFormat = resources.GetString("txttotal_set.OutputFormat")
        Me.txttotal_set.SummaryGroup = Nothing
        Me.txttotal_set.Top = 0.0!
        Me.txttotal_set.Width = 0.738189!
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
        resources.ApplyResources(Me.txtset_price, "txtset_price")
        Me.txtset_price.DataField = "set_price"
        Me.txtset_price.DistinctField = Nothing
        Me.txtset_price.Height = 0.246063!
        Me.txtset_price.Left = 6.028543!
        Me.txtset_price.Name = "txtset_price"
        Me.txtset_price.OutputFormat = resources.GetString("txtset_price.OutputFormat")
        Me.txtset_price.SummaryGroup = Nothing
        Me.txtset_price.Top = 0.0!
        Me.txtset_price.Width = 0.7627954!
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
        resources.ApplyResources(Me.txtamt, "txtamt")
        Me.txtamt.DataField = "amt"
        Me.txtamt.DistinctField = Nothing
        Me.txtamt.Height = 0.246063!
        Me.txtamt.Left = 6.8125!
        Me.txtamt.Name = "txtamt"
        Me.txtamt.OutputFormat = resources.GetString("txtamt.OutputFormat")
        Me.txtamt.SummaryGroup = Nothing
        Me.txtamt.Top = 0.0!
        Me.txtamt.Width = 1.131889!
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
        resources.ApplyResources(Me.txtreference_code2, "txtreference_code2")
        Me.txtreference_code2.DataField = "reference_code2"
        Me.txtreference_code2.DistinctField = Nothing
        Me.txtreference_code2.Height = 0.246063!
        Me.txtreference_code2.Left = 0.3444882!
        Me.txtreference_code2.Name = "txtreference_code2"
        Me.txtreference_code2.OutputFormat = resources.GetString("txtreference_code2.OutputFormat")
        Me.txtreference_code2.SummaryGroup = Nothing
        Me.txtreference_code2.Top = 0.308563!
        Me.txtreference_code2.Visible = False
        Me.txtreference_code2.Width = 3.395669!
        '
        'txtSumamt
        '
        Me.txtSumamt.Border.BottomColor = System.Drawing.Color.Black
        Me.txtSumamt.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtSumamt.Border.LeftColor = System.Drawing.Color.Black
        Me.txtSumamt.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtSumamt.Border.RightColor = System.Drawing.Color.Black
        Me.txtSumamt.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtSumamt.Border.TopColor = System.Drawing.Color.Black
        Me.txtSumamt.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        resources.ApplyResources(Me.txtSumamt, "txtSumamt")
        Me.txtSumamt.DistinctField = Nothing
        Me.txtSumamt.Height = 0.246063!
        Me.txtSumamt.Left = 6.815945!
        Me.txtSumamt.Name = "txtSumamt"
        Me.txtSumamt.OutputFormat = resources.GetString("txtSumamt.OutputFormat")
        Me.txtSumamt.SummaryGroup = Nothing
        Me.txtSumamt.Top = 0.1476378!
        Me.txtSumamt.Width = 1.131889!
        '
        'txtbText
        '
        Me.txtbText.Border.BottomColor = System.Drawing.Color.Black
        Me.txtbText.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtbText.Border.LeftColor = System.Drawing.Color.Black
        Me.txtbText.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtbText.Border.RightColor = System.Drawing.Color.Black
        Me.txtbText.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtbText.Border.TopColor = System.Drawing.Color.Black
        Me.txtbText.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        resources.ApplyResources(Me.txtbText, "txtbText")
        Me.txtbText.DistinctField = Nothing
        Me.txtbText.Height = 0.25!
        Me.txtbText.Left = 3.051181!
        Me.txtbText.Name = "txtbText"
        Me.txtbText.OutputFormat = resources.GetString("txtbText.OutputFormat")
        Me.txtbText.SummaryGroup = Nothing
        Me.txtbText.Top = 0.1476378!
        Me.txtbText.Width = 3.3125!
        '
        'txtreceipt_by
        '
        Me.txtreceipt_by.Border.BottomColor = System.Drawing.Color.Black
        Me.txtreceipt_by.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtreceipt_by.Border.LeftColor = System.Drawing.Color.Black
        Me.txtreceipt_by.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtreceipt_by.Border.RightColor = System.Drawing.Color.Black
        Me.txtreceipt_by.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtreceipt_by.Border.TopColor = System.Drawing.Color.Black
        Me.txtreceipt_by.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        resources.ApplyResources(Me.txtreceipt_by, "txtreceipt_by")
        Me.txtreceipt_by.DataField = "receipt_by"
        Me.txtreceipt_by.DistinctField = Nothing
        Me.txtreceipt_by.Height = 0.2952756!
        Me.txtreceipt_by.Left = 4.798229!
        Me.txtreceipt_by.Name = "txtreceipt_by"
        Me.txtreceipt_by.OutputFormat = resources.GetString("txtreceipt_by.OutputFormat")
        Me.txtreceipt_by.SummaryGroup = Nothing
        Me.txtreceipt_by.Top = 0.7874016!
        Me.txtreceipt_by.Width = 3.100394!
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
        resources.ApplyResources(Me.TextBox1, "TextBox1")
        Me.TextBox1.DataField = "bill_date"
        Me.TextBox1.DistinctField = Nothing
        Me.TextBox1.Height = 0.25!
        Me.TextBox1.Left = 1.32874!
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.SummaryGroup = Nothing
        Me.TextBox1.Top = 0.2952756!
        Me.TextBox1.Visible = False
        Me.TextBox1.Width = 2.1875!
        '
        'txtNumpageForm
        '
        Me.txtNumpageForm.Border.BottomColor = System.Drawing.Color.Black
        Me.txtNumpageForm.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtNumpageForm.Border.LeftColor = System.Drawing.Color.Black
        Me.txtNumpageForm.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtNumpageForm.Border.RightColor = System.Drawing.Color.Black
        Me.txtNumpageForm.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtNumpageForm.Border.TopColor = System.Drawing.Color.Black
        Me.txtNumpageForm.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        resources.ApplyResources(Me.txtNumpageForm, "txtNumpageForm")
        Me.txtNumpageForm.DistinctField = Nothing
        Me.txtNumpageForm.Height = 0.1979167!
        Me.txtNumpageForm.Left = 4.749016!
        Me.txtNumpageForm.Name = "txtNumpageForm"
        Me.txtNumpageForm.OutputFormat = resources.GetString("txtNumpageForm.OutputFormat")
        Me.txtNumpageForm.SummaryGroup = Nothing
        Me.txtNumpageForm.SummaryRunning = DataDynamics.ActiveReports.SummaryRunning.All
        Me.txtNumpageForm.SummaryType = DataDynamics.ActiveReports.SummaryType.PageCount
        Me.txtNumpageForm.Text = Nothing
        Me.txtNumpageForm.Top = 0.2952756!
        Me.txtNumpageForm.Visible = False
        Me.txtNumpageForm.Width = 1.0!
        '
        'txtnumTotalpage
        '
        Me.txtnumTotalpage.Border.BottomColor = System.Drawing.Color.Black
        Me.txtnumTotalpage.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtnumTotalpage.Border.LeftColor = System.Drawing.Color.Black
        Me.txtnumTotalpage.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtnumTotalpage.Border.RightColor = System.Drawing.Color.Black
        Me.txtnumTotalpage.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtnumTotalpage.Border.TopColor = System.Drawing.Color.Black
        Me.txtnumTotalpage.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        resources.ApplyResources(Me.txtnumTotalpage, "txtnumTotalpage")
        Me.txtnumTotalpage.DistinctField = Nothing
        Me.txtnumTotalpage.Height = 0.1979167!
        Me.txtnumTotalpage.Left = 4.749016!
        Me.txtnumTotalpage.Name = "txtnumTotalpage"
        Me.txtnumTotalpage.OutputFormat = resources.GetString("txtnumTotalpage.OutputFormat")
        Me.txtnumTotalpage.SummaryGroup = Nothing
        Me.txtnumTotalpage.SummaryType = DataDynamics.ActiveReports.SummaryType.PageCount
        Me.txtnumTotalpage.Text = Nothing
        Me.txtnumTotalpage.Top = 0.5556923!
        Me.txtnumTotalpage.Visible = False
        Me.txtnumTotalpage.Width = 1.0!
        '
        'PageHeader1
        '
        Me.PageHeader1.CanGrow = False
        Me.PageHeader1.Controls.AddRange(New DataDynamics.ActiveReports.ARControl() {Me.txtCount_Tol, Me.Label2, Me.Label1, Me.txtreceipt_name, Me.txtbill_date, Me.txtbill_no, Me.txtPage2, Me.TextBox1, Me.txtNumpageForm, Me.txtnumTotalpage})
        Me.PageHeader1.Height = 3.65625!
        Me.PageHeader1.Name = "PageHeader1"
        '
        'Detail1
        '
        Me.Detail1.ColumnSpacing = 0.0!
        Me.Detail1.Controls.AddRange(New DataDynamics.ActiveReports.ARControl() {Me.txtTemp_reference_code2, Me.txtTempt_reference_code2, Me.txttotal_set, Me.txtset_price, Me.txtamt, Me.txtreference_code2})
        Me.Detail1.Height = 0.2480315!
        Me.Detail1.KeepTogether = True
        Me.Detail1.Name = "Detail1"
        '
        'PageFooter1
        '
        Me.PageFooter1.CanGrow = False
        Me.PageFooter1.Controls.AddRange(New DataDynamics.ActiveReports.ARControl() {Me.txtSumamt, Me.txtbText, Me.txtreceipt_by})
        Me.PageFooter1.Height = 1.417323!
        Me.PageFooter1.Name = "PageFooter1"
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
        'receipt_rpt
        '
        Me.MasterReport = False
        Me.PageSettings.DefaultPaperSize = False
        Me.PageSettings.DefaultPaperSource = False
        Me.PageSettings.Margins.Bottom = 0.0!
        Me.PageSettings.Margins.Left = 0.0!
        Me.PageSettings.Margins.Right = 0.0!
        Me.PageSettings.Margins.Top = 0.0!
        Me.PageSettings.PaperHeight = 11.0!
        Me.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.Custom
        Me.PageSettings.PaperName = "Letter Fanfold 8 1/2 x 11 in"
        Me.PageSettings.PaperSource = System.Drawing.Printing.PaperSourceKind.FormSource
        Me.PageSettings.PaperWidth = 8.268056!
        Me.PrintWidth = 8.267715!
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
        CType(Me.Label2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtreceipt_name, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtbill_date, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtbill_no, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCount_Tol, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPage2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTemp_reference_code2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTempt_reference_code2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txttotal_set, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtset_price, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtamt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtreference_code2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSumamt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtbText, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtreceipt_by, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TextBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNumpageForm, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtnumTotalpage, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub
    Friend WithEvents Label1 As DataDynamics.ActiveReports.Label
    Friend WithEvents txtTemp_reference_code2 As DataDynamics.ActiveReports.TextBox
    Friend WithEvents txtTempt_reference_code2 As DataDynamics.ActiveReports.TextBox
    Public WithEvents txtSumamt As DataDynamics.ActiveReports.TextBox
    Public WithEvents txtbText As DataDynamics.ActiveReports.TextBox
    Friend WithEvents txtCount_Tol As DataDynamics.ActiveReports.TextBox
    Friend WithEvents txtPage2 As DataDynamics.ActiveReports.TextBox
    Friend WithEvents Label2 As DataDynamics.ActiveReports.Label
    Public WithEvents txtreceipt_name As DataDynamics.ActiveReports.TextBox
    Public WithEvents txtbill_date As DataDynamics.ActiveReports.TextBox
    Public WithEvents txtbill_no As DataDynamics.ActiveReports.TextBox
    Public WithEvents txtreceipt_by As DataDynamics.ActiveReports.TextBox
    Public WithEvents txttotal_set As DataDynamics.ActiveReports.TextBox
    Public WithEvents txtset_price As DataDynamics.ActiveReports.TextBox
    Public WithEvents txtamt As DataDynamics.ActiveReports.TextBox
    Friend WithEvents txtreference_code2 As DataDynamics.ActiveReports.TextBox
    Public WithEvents TextBox1 As DataDynamics.ActiveReports.TextBox
    Friend WithEvents txtNumpageForm As DataDynamics.ActiveReports.TextBox
    Friend WithEvents txtnumTotalpage As DataDynamics.ActiveReports.TextBox
    Friend WithEvents ReportHeader1 As DataDynamics.ActiveReports.ReportHeader
    Friend WithEvents ReportFooter1 As DataDynamics.ActiveReports.ReportFooter
End Class
