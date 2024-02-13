<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class NewActiveReport2 
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
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(NewActiveReport2))
        Me.PageHeader1 = New DataDynamics.ActiveReports.PageHeader
        Me.Detail1 = New DataDynamics.ActiveReports.Detail
        Me.PageFooter1 = New DataDynamics.ActiveReports.PageFooter
        Me.txtdest_remark = New DataDynamics.ActiveReports.TextBox
        Me.txtob_address = New DataDynamics.ActiveReports.TextBox
        Me.txtNewEmail_ch01 = New DataDynamics.ActiveReports.TextBox
        Me.txtcompany_email = New DataDynamics.ActiveReports.TextBox
        Me.txtdest_remark1 = New DataDynamics.ActiveReports.TextBox
        Me.txtob_dest_address = New DataDynamics.ActiveReports.TextBox
        Me.txtNewEmail_ch02 = New DataDynamics.ActiveReports.TextBox
        Me.txtdestination_taxid = New DataDynamics.ActiveReports.TextBox
        Me.txtdestination_email = New DataDynamics.ActiveReports.TextBox
        Me.txtplace_exibition = New DataDynamics.ActiveReports.TextBox
        CType(Me.txtdest_remark, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtob_address, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNewEmail_ch01, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtcompany_email, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtdest_remark1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtob_dest_address, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNewEmail_ch02, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtdestination_taxid, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtdestination_email, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtplace_exibition, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'PageHeader1
        '
        Me.PageHeader1.Controls.AddRange(New DataDynamics.ActiveReports.ARControl() {Me.txtdest_remark, Me.txtob_address, Me.txtNewEmail_ch01, Me.txtcompany_email, Me.txtdest_remark1, Me.txtob_dest_address, Me.txtNewEmail_ch02, Me.txtdestination_taxid, Me.txtdestination_email, Me.txtplace_exibition})
        Me.PageHeader1.Height = 2.541667!
        Me.PageHeader1.Name = "PageHeader1"
        '
        'Detail1
        '
        Me.Detail1.ColumnSpacing = 0.0!
        Me.Detail1.Height = 2.0!
        Me.Detail1.Name = "Detail1"
        '
        'PageFooter1
        '
        Me.PageFooter1.Height = 0.25!
        Me.PageFooter1.Name = "PageFooter1"
        '
        'txtdest_remark
        '
        Me.txtdest_remark.Border.BottomColor = System.Drawing.Color.Black
        Me.txtdest_remark.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtdest_remark.Border.LeftColor = System.Drawing.Color.Black
        Me.txtdest_remark.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtdest_remark.Border.RightColor = System.Drawing.Color.Black
        Me.txtdest_remark.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtdest_remark.Border.TopColor = System.Drawing.Color.Black
        Me.txtdest_remark.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtdest_remark.DataField = "dest_remark"
        Me.txtdest_remark.Height = 0.1979167!
        Me.txtdest_remark.Left = 0.0!
        Me.txtdest_remark.Name = "txtdest_remark"
        Me.txtdest_remark.Style = "color: Red; "
        Me.txtdest_remark.Text = "dest_remark"
        Me.txtdest_remark.Top = 0.0!
        Me.txtdest_remark.Visible = False
        Me.txtdest_remark.Width = 1.0!
        '
        'txtob_address
        '
        Me.txtob_address.Border.BottomColor = System.Drawing.Color.Black
        Me.txtob_address.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtob_address.Border.LeftColor = System.Drawing.Color.Black
        Me.txtob_address.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtob_address.Border.RightColor = System.Drawing.Color.Black
        Me.txtob_address.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtob_address.Border.TopColor = System.Drawing.Color.Black
        Me.txtob_address.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtob_address.DataField = "ob_address"
        Me.txtob_address.Height = 0.1979167!
        Me.txtob_address.Left = 0.0!
        Me.txtob_address.Name = "txtob_address"
        Me.txtob_address.Style = "color: Red; "
        Me.txtob_address.Text = "ob_address"
        Me.txtob_address.Top = 0.2604167!
        Me.txtob_address.Visible = False
        Me.txtob_address.Width = 1.0!
        '
        'txtNewEmail_ch01
        '
        Me.txtNewEmail_ch01.Border.BottomColor = System.Drawing.Color.Black
        Me.txtNewEmail_ch01.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtNewEmail_ch01.Border.LeftColor = System.Drawing.Color.Black
        Me.txtNewEmail_ch01.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtNewEmail_ch01.Border.RightColor = System.Drawing.Color.Black
        Me.txtNewEmail_ch01.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtNewEmail_ch01.Border.TopColor = System.Drawing.Color.Black
        Me.txtNewEmail_ch01.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtNewEmail_ch01.DataField = "NewEmail_ch01"
        Me.txtNewEmail_ch01.Height = 0.1979167!
        Me.txtNewEmail_ch01.Left = 0.0!
        Me.txtNewEmail_ch01.Name = "txtNewEmail_ch01"
        Me.txtNewEmail_ch01.Style = "color: Red; "
        Me.txtNewEmail_ch01.Text = "NewEmail_ch01"
        Me.txtNewEmail_ch01.Top = 0.5208334!
        Me.txtNewEmail_ch01.Visible = False
        Me.txtNewEmail_ch01.Width = 1.0!
        '
        'txtcompany_email
        '
        Me.txtcompany_email.Border.BottomColor = System.Drawing.Color.Black
        Me.txtcompany_email.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtcompany_email.Border.LeftColor = System.Drawing.Color.Black
        Me.txtcompany_email.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtcompany_email.Border.RightColor = System.Drawing.Color.Black
        Me.txtcompany_email.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtcompany_email.Border.TopColor = System.Drawing.Color.Black
        Me.txtcompany_email.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtcompany_email.DataField = "company_email"
        Me.txtcompany_email.Height = 0.1979167!
        Me.txtcompany_email.Left = 0.0!
        Me.txtcompany_email.Name = "txtcompany_email"
        Me.txtcompany_email.Style = "color: Red; "
        Me.txtcompany_email.Text = "company_email"
        Me.txtcompany_email.Top = 0.7812501!
        Me.txtcompany_email.Visible = False
        Me.txtcompany_email.Width = 1.0!
        '
        'txtdest_remark1
        '
        Me.txtdest_remark1.Border.BottomColor = System.Drawing.Color.Black
        Me.txtdest_remark1.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtdest_remark1.Border.LeftColor = System.Drawing.Color.Black
        Me.txtdest_remark1.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtdest_remark1.Border.RightColor = System.Drawing.Color.Black
        Me.txtdest_remark1.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtdest_remark1.Border.TopColor = System.Drawing.Color.Black
        Me.txtdest_remark1.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtdest_remark1.DataField = "dest_remark1"
        Me.txtdest_remark1.Height = 0.1979167!
        Me.txtdest_remark1.Left = 0.0!
        Me.txtdest_remark1.Name = "txtdest_remark1"
        Me.txtdest_remark1.Style = "color: Red; "
        Me.txtdest_remark1.Text = "dest_remark1"
        Me.txtdest_remark1.Top = 1.041667!
        Me.txtdest_remark1.Visible = False
        Me.txtdest_remark1.Width = 1.0!
        '
        'txtob_dest_address
        '
        Me.txtob_dest_address.Border.BottomColor = System.Drawing.Color.Black
        Me.txtob_dest_address.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtob_dest_address.Border.LeftColor = System.Drawing.Color.Black
        Me.txtob_dest_address.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtob_dest_address.Border.RightColor = System.Drawing.Color.Black
        Me.txtob_dest_address.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtob_dest_address.Border.TopColor = System.Drawing.Color.Black
        Me.txtob_dest_address.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtob_dest_address.DataField = "ob_dest_address"
        Me.txtob_dest_address.Height = 0.1979167!
        Me.txtob_dest_address.Left = 0.0!
        Me.txtob_dest_address.Name = "txtob_dest_address"
        Me.txtob_dest_address.Style = "color: Red; "
        Me.txtob_dest_address.Text = "ob_dest_address"
        Me.txtob_dest_address.Top = 1.302083!
        Me.txtob_dest_address.Visible = False
        Me.txtob_dest_address.Width = 1.0!
        '
        'txtNewEmail_ch02
        '
        Me.txtNewEmail_ch02.Border.BottomColor = System.Drawing.Color.Black
        Me.txtNewEmail_ch02.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtNewEmail_ch02.Border.LeftColor = System.Drawing.Color.Black
        Me.txtNewEmail_ch02.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtNewEmail_ch02.Border.RightColor = System.Drawing.Color.Black
        Me.txtNewEmail_ch02.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtNewEmail_ch02.Border.TopColor = System.Drawing.Color.Black
        Me.txtNewEmail_ch02.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtNewEmail_ch02.DataField = "NewEmail_ch02"
        Me.txtNewEmail_ch02.Height = 0.1979167!
        Me.txtNewEmail_ch02.Left = 0.0!
        Me.txtNewEmail_ch02.Name = "txtNewEmail_ch02"
        Me.txtNewEmail_ch02.Style = "color: Red; "
        Me.txtNewEmail_ch02.Text = "NewEmail_ch02"
        Me.txtNewEmail_ch02.Top = 1.5625!
        Me.txtNewEmail_ch02.Visible = False
        Me.txtNewEmail_ch02.Width = 1.0!
        '
        'txtdestination_taxid
        '
        Me.txtdestination_taxid.Border.BottomColor = System.Drawing.Color.Black
        Me.txtdestination_taxid.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtdestination_taxid.Border.LeftColor = System.Drawing.Color.Black
        Me.txtdestination_taxid.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtdestination_taxid.Border.RightColor = System.Drawing.Color.Black
        Me.txtdestination_taxid.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtdestination_taxid.Border.TopColor = System.Drawing.Color.Black
        Me.txtdestination_taxid.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtdestination_taxid.DataField = "destination_taxid"
        Me.txtdestination_taxid.Height = 0.1979167!
        Me.txtdestination_taxid.Left = 0.0!
        Me.txtdestination_taxid.Name = "txtdestination_taxid"
        Me.txtdestination_taxid.Style = "color: Red; "
        Me.txtdestination_taxid.Text = "destination_taxid"
        Me.txtdestination_taxid.Top = 1.822917!
        Me.txtdestination_taxid.Visible = False
        Me.txtdestination_taxid.Width = 1.0!
        '
        'txtdestination_email
        '
        Me.txtdestination_email.Border.BottomColor = System.Drawing.Color.Black
        Me.txtdestination_email.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtdestination_email.Border.LeftColor = System.Drawing.Color.Black
        Me.txtdestination_email.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtdestination_email.Border.RightColor = System.Drawing.Color.Black
        Me.txtdestination_email.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtdestination_email.Border.TopColor = System.Drawing.Color.Black
        Me.txtdestination_email.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtdestination_email.DataField = "destination_email"
        Me.txtdestination_email.Height = 0.1979167!
        Me.txtdestination_email.Left = 0.0!
        Me.txtdestination_email.Name = "txtdestination_email"
        Me.txtdestination_email.Style = "color: Red; "
        Me.txtdestination_email.Text = "destination_email"
        Me.txtdestination_email.Top = 2.083334!
        Me.txtdestination_email.Visible = False
        Me.txtdestination_email.Width = 1.0!
        '
        'txtplace_exibition
        '
        Me.txtplace_exibition.Border.BottomColor = System.Drawing.Color.Black
        Me.txtplace_exibition.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtplace_exibition.Border.LeftColor = System.Drawing.Color.Black
        Me.txtplace_exibition.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtplace_exibition.Border.RightColor = System.Drawing.Color.Black
        Me.txtplace_exibition.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtplace_exibition.Border.TopColor = System.Drawing.Color.Black
        Me.txtplace_exibition.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.None
        Me.txtplace_exibition.DataField = "place_exibition"
        Me.txtplace_exibition.Height = 0.1979167!
        Me.txtplace_exibition.Left = 0.0!
        Me.txtplace_exibition.Name = "txtplace_exibition"
        Me.txtplace_exibition.Style = "color: Red; "
        Me.txtplace_exibition.Text = "place_exibition"
        Me.txtplace_exibition.Top = 2.34375!
        Me.txtplace_exibition.Visible = False
        Me.txtplace_exibition.Width = 1.0!
        '
        'NewActiveReport2
        '
        Me.MasterReport = False
        Me.PageSettings.PaperHeight = 11.0!
        Me.PageSettings.PaperWidth = 8.5!
        Me.Sections.Add(Me.PageHeader1)
        Me.Sections.Add(Me.Detail1)
        Me.Sections.Add(Me.PageFooter1)
        Me.StyleSheet.Add(New DDCssLib.StyleSheetRule("font-family: Arial; font-style: normal; text-decoration: none; font-weight: norma" & _
                    "l; font-size: 10pt; color: Black; ", "Normal"))
        Me.StyleSheet.Add(New DDCssLib.StyleSheetRule("font-size: 16pt; font-weight: bold; ", "Heading1", "Normal"))
        Me.StyleSheet.Add(New DDCssLib.StyleSheetRule("font-family: Times New Roman; font-size: 14pt; font-weight: bold; font-style: ita" & _
                    "lic; ", "Heading2", "Normal"))
        Me.StyleSheet.Add(New DDCssLib.StyleSheetRule("font-size: 13pt; font-weight: bold; ", "Heading3", "Normal"))
        CType(Me.txtdest_remark, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtob_address, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNewEmail_ch01, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtcompany_email, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtdest_remark1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtob_dest_address, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNewEmail_ch02, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtdestination_taxid, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtdestination_email, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtplace_exibition, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub
    Friend WithEvents txtdest_remark As DataDynamics.ActiveReports.TextBox
    Friend WithEvents txtob_address As DataDynamics.ActiveReports.TextBox
    Friend WithEvents txtNewEmail_ch01 As DataDynamics.ActiveReports.TextBox
    Friend WithEvents txtcompany_email As DataDynamics.ActiveReports.TextBox
    Friend WithEvents txtdest_remark1 As DataDynamics.ActiveReports.TextBox
    Friend WithEvents txtob_dest_address As DataDynamics.ActiveReports.TextBox
    Friend WithEvents txtNewEmail_ch02 As DataDynamics.ActiveReports.TextBox
    Friend WithEvents txtdestination_taxid As DataDynamics.ActiveReports.TextBox
    Friend WithEvents txtdestination_email As DataDynamics.ActiveReports.TextBox
    Friend WithEvents txtplace_exibition As DataDynamics.ActiveReports.TextBox
End Class 
