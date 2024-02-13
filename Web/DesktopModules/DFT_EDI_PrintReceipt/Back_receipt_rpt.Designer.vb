<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class Back_receipt_rpt
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
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(Back_receipt_rpt))
        Me.PageHeader1 = New DataDynamics.ActiveReports.PageHeader
        Me.Label1 = New DataDynamics.ActiveReports.Label
        Me.txtreceipt_name = New DataDynamics.ActiveReports.TextBox
        Me.txtbill_date = New DataDynamics.ActiveReports.TextBox
        Me.txtbill_no = New DataDynamics.ActiveReports.TextBox
        Me.Detail1 = New DataDynamics.ActiveReports.Detail
        Me.txtreference_code2 = New DataDynamics.ActiveReports.TextBox
        Me.txt_reference_code2 = New DataDynamics.ActiveReports.TextBox
        Me.txttotal_set = New DataDynamics.ActiveReports.TextBox
        Me.txtset_price = New DataDynamics.ActiveReports.TextBox
        Me.txtamt = New DataDynamics.ActiveReports.TextBox
        Me.PageFooter1 = New DataDynamics.ActiveReports.PageFooter
        Me.txtSumamt = New DataDynamics.ActiveReports.TextBox
        Me.txtbText = New DataDynamics.ActiveReports.TextBox
        Me.txtreceipt_by = New DataDynamics.ActiveReports.TextBox
        Me.txtCount_Tol = New DataDynamics.ActiveReports.TextBox
        Me.txtPage2 = New DataDynamics.ActiveReports.TextBox
        Me.Label2 = New DataDynamics.ActiveReports.Label
        CType(Me.Label1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtreceipt_name, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtbill_date, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtbill_no, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtreference_code2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_reference_code2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txttotal_set, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtset_price, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtamt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSumamt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtbText, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtreceipt_by, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCount_Tol, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPage2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'PageHeader1
        '
        Me.PageHeader1.Controls.AddRange(New DataDynamics.ActiveReports.ARControl() {Me.Label1, Me.txtreceipt_name, Me.txtbill_date, Me.txtbill_no, Me.txtCount_Tol, Me.txtPage2, Me.Label2})
        Me.PageHeader1.Height = 3.03125!
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
        Me.Label1.Height = 0.25!
        Me.Label1.HyperLink = Nothing
        Me.Label1.Left = 5.625!
        Me.Label1.Name = "Label1"
        Me.Label1.Style = ""
        Me.Label1.Text = "แผ่นที่"
        Me.Label1.Top = 0.4375!
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
        Me.txtreceipt_name.DataField = "receipt_name"
        Me.txtreceipt_name.Height = 0.25!
        Me.txtreceipt_name.Left = 2.0!
        Me.txtreceipt_name.Name = "txtreceipt_name"
        Me.txtreceipt_name.Style = ""
        Me.txtreceipt_name.Text = "txtreceipt_name"
        Me.txtreceipt_name.Top = 2.0!
        Me.txtreceipt_name.Width = 4.0!
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
        Me.txtbill_date.DataField = "bill_date"
        Me.txtbill_date.Height = 0.25!
        Me.txtbill_date.Left = 0.8125!
        Me.txtbill_date.Name = "txtbill_date"
        Me.txtbill_date.Style = ""
        Me.txtbill_date.Text = "txtbill_date"
        Me.txtbill_date.Top = 1.25!
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
        Me.txtbill_no.DataField = "bill_no"
        Me.txtbill_no.Height = 0.25!
        Me.txtbill_no.Left = 5.75!
        Me.txtbill_no.Name = "txtbill_no"
        Me.txtbill_no.Style = ""
        Me.txtbill_no.Text = "txtbill_no"
        Me.txtbill_no.Top = 1.0!
        Me.txtbill_no.Width = 1.625!
        '
        'Detail1
        '
        Me.Detail1.ColumnSpacing = 0.0!
        Me.Detail1.Controls.AddRange(New DataDynamics.ActiveReports.ARControl() {Me.txtreference_code2, Me.txt_reference_code2, Me.txttotal_set, Me.txtset_price, Me.txtamt})
        Me.Detail1.Height = 6.6875!
        Me.Detail1.Name = "Detail1"
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
        Me.txtreference_code2.Left = 0.1875!
        Me.txtreference_code2.Name = "txtreference_code2"
        Me.txtreference_code2.Style = ""
        Me.txtreference_code2.Text = "txtreference_code2"
        Me.txtreference_code2.Top = 0.0625!
        Me.txtreference_code2.Width = 1.8125!
        '
        'txt_reference_code2
        '
        Me.txt_reference_code2.Border.BottomColor = System.Drawing.Color.Black
        Me.txt_reference_code2.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txt_reference_code2.Border.LeftColor = System.Drawing.Color.Black
        Me.txt_reference_code2.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txt_reference_code2.Border.RightColor = System.Drawing.Color.Black
        Me.txt_reference_code2.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txt_reference_code2.Border.TopColor = System.Drawing.Color.Black
        Me.txt_reference_code2.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txt_reference_code2.DataField = "reference_code2"
        Me.txt_reference_code2.Height = 0.25!
        Me.txt_reference_code2.Left = 2.1875!
        Me.txt_reference_code2.Name = "txt_reference_code2"
        Me.txt_reference_code2.Style = ""
        Me.txt_reference_code2.Text = "txt_reference_code2"
        Me.txt_reference_code2.Top = 0.0625!
        Me.txt_reference_code2.Width = 2.0!
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
        Me.txttotal_set.Left = 4.5!
        Me.txttotal_set.Name = "txttotal_set"
        Me.txttotal_set.Style = ""
        Me.txttotal_set.Text = "txttotal_set"
        Me.txttotal_set.Top = 0.0625!
        Me.txttotal_set.Width = 1.0625!
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
        Me.txtset_price.Left = 5.6875!
        Me.txtset_price.Name = "txtset_price"
        Me.txtset_price.Style = ""
        Me.txtset_price.Text = "txtset_price"
        Me.txtset_price.Top = 0.0625!
        Me.txtset_price.Width = 1.0!
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
        Me.txtamt.Left = 6.875!
        Me.txtamt.Name = "txtamt"
        Me.txtamt.Style = ""
        Me.txtamt.Text = "txtamt"
        Me.txtamt.Top = 0.0625!
        Me.txtamt.Width = 1.0625!
        '
        'PageFooter1
        '
        Me.PageFooter1.Controls.AddRange(New DataDynamics.ActiveReports.ARControl() {Me.txtSumamt, Me.txtbText, Me.txtreceipt_by})
        Me.PageFooter1.Height = 1.916667!
        Me.PageFooter1.Name = "PageFooter1"
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
        Me.txtSumamt.Height = 0.25!
        Me.txtSumamt.Left = 6.5!
        Me.txtSumamt.Name = "txtSumamt"
        Me.txtSumamt.Style = ""
        Me.txtSumamt.Text = "txtSum_amt"
        Me.txtSumamt.Top = 0.25!
        Me.txtSumamt.Width = 1.5!
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
        Me.txtbText.Height = 0.25!
        Me.txtbText.Left = 4.125!
        Me.txtbText.Name = "txtbText"
        Me.txtbText.Style = ""
        Me.txtbText.Text = "txtbText"
        Me.txtbText.Top = 0.25!
        Me.txtbText.Width = 1.8125!
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
        Me.txtreceipt_by.DataField = "receipt_by"
        Me.txtreceipt_by.Height = 0.25!
        Me.txtreceipt_by.Left = 5.25!
        Me.txtreceipt_by.Name = "txtreceipt_by"
        Me.txtreceipt_by.Style = ""
        Me.txtreceipt_by.Text = "txtreceipt_by"
        Me.txtreceipt_by.Top = 0.75!
        Me.txtreceipt_by.Width = 2.25!
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
        Me.txtCount_Tol.Height = 0.246063!
        Me.txtCount_Tol.Left = 6.668307!
        Me.txtCount_Tol.Name = "txtCount_Tol"
        Me.txtCount_Tol.OutputFormat = resources.GetString("txtCount_Tol.OutputFormat")
        Me.txtCount_Tol.Style = "color: Black; "
        Me.txtCount_Tol.SummaryType = DataDynamics.ActiveReports.SummaryType.PageCount
        Me.txtCount_Tol.Text = "txtCount_Tol"
        Me.txtCount_Tol.Top = 0.4429134!
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
        Me.txtPage2.Height = 0.246063!
        Me.txtPage2.Left = 6.225394!
        Me.txtPage2.Name = "txtPage2"
        Me.txtPage2.OutputFormat = resources.GetString("txtPage2.OutputFormat")
        Me.txtPage2.Style = "color: Black; text-align: right; "
        Me.txtPage2.Text = "txtPage2"
        Me.txtPage2.Top = 0.4429134!
        Me.txtPage2.Width = 0.3690945!
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
        Me.Label2.Height = 0.2706693!
        Me.Label2.HyperLink = Nothing
        Me.Label2.Left = 6.126968!
        Me.Label2.Name = "Label2"
        Me.Label2.Style = "text-align: center; "
        Me.Label2.Text = "/"
        Me.Label2.Top = 0.4370079!
        Me.Label2.Width = 1.008858!
        '
        'receipt_rpt
        '
        Me.MasterReport = False
        Me.PageSettings.Margins.Bottom = 0.1!
        Me.PageSettings.Margins.Top = 0.1!
        Me.PageSettings.PaperHeight = 11.69!
        Me.PageSettings.PaperWidth = 8.27!
        Me.PrintWidth = 8.5!
        Me.Sections.Add(Me.PageHeader1)
        Me.Sections.Add(Me.Detail1)
        Me.Sections.Add(Me.PageFooter1)
        Me.StyleSheet.Add(New DDCssLib.StyleSheetRule("font-family: Arial; font-style: normal; text-decoration: none; font-weight: norma" & _
                    "l; font-size: 10pt; color: Black; ", "Normal"))
        Me.StyleSheet.Add(New DDCssLib.StyleSheetRule("font-size: 16pt; font-weight: bold; ", "Heading1", "Normal"))
        Me.StyleSheet.Add(New DDCssLib.StyleSheetRule("font-family: Times New Roman; font-size: 14pt; font-weight: bold; font-style: ita" & _
                    "lic; ", "Heading2", "Normal"))
        Me.StyleSheet.Add(New DDCssLib.StyleSheetRule("font-size: 13pt; font-weight: bold; ", "Heading3", "Normal"))
        CType(Me.Label1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtreceipt_name, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtbill_date, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtbill_no, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtreference_code2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_reference_code2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txttotal_set, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtset_price, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtamt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSumamt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtbText, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtreceipt_by, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCount_Tol, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPage2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub
    Friend WithEvents Label1 As DataDynamics.ActiveReports.Label
    Friend WithEvents txtreceipt_name As DataDynamics.ActiveReports.TextBox
    Friend WithEvents txtbill_date As DataDynamics.ActiveReports.TextBox
    Friend WithEvents txtbill_no As DataDynamics.ActiveReports.TextBox
    Friend WithEvents txtreference_code2 As DataDynamics.ActiveReports.TextBox
    Friend WithEvents txt_reference_code2 As DataDynamics.ActiveReports.TextBox
    Friend WithEvents txttotal_set As DataDynamics.ActiveReports.TextBox
    Friend WithEvents txtset_price As DataDynamics.ActiveReports.TextBox
    Friend WithEvents txtamt As DataDynamics.ActiveReports.TextBox
    Friend WithEvents txtreceipt_by As DataDynamics.ActiveReports.TextBox
    Public WithEvents txtSumamt As DataDynamics.ActiveReports.TextBox
    Public WithEvents txtbText As DataDynamics.ActiveReports.TextBox
    Friend WithEvents txtCount_Tol As DataDynamics.ActiveReports.TextBox
    Friend WithEvents txtPage2 As DataDynamics.ActiveReports.TextBox
    Friend WithEvents Label2 As DataDynamics.ActiveReports.Label
End Class
