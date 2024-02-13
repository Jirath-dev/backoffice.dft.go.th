Imports DataDynamics.ActiveReports
Imports DataDynamics.ActiveReports.Document

Imports Microsoft.ApplicationBlocks.Data
Imports System.Data.SqlClient
Imports System.Data
Imports System.Text
Imports DFT.Dotnetnuke.ClassLibrary
Imports ReportPrintClass
Public Class rpt3_ReEdi_A_VLine
   
    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub Detail1_BeforePrint(ByVal sender As Object, ByVal e As System.EventArgs) Handles Detail1.BeforePrint
        'PageDetail1Height = PageDetail1Height + Detail1.Height

    End Sub

    Private Sub Detail1_Format(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Detail1.Format
        'txtByLine.Text = "1" & vbNewLine & "1" & vbNewLine & "1" & vbNewLine & "1" & vbNewLine & "1" & vbNewLine & "1" & vbNewLine & "1"

        Me.CurrentPage.DrawLine(0.19, 0, 0.19, 8)
    End Sub

    Private Sub rpt3_ReEdi_A_VLine_PageEnd(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PageEnd
        'Me.CurrentPage.DrawLine(Me.PageSettings.Margins.Left, Me.PageSettings.Margins.Top, Me.PageSettings.Margins.Left + Me.PrintWidth, Me.PageSettings.Margins.Top)
        'Me.CurrentPage.DrawLine(Me.PageSettings.Margins.Left, Me.PageSettings.Margins.Top + Me.Detail1.Height, Me.PageSettings.Margins.Left + Me.PrintWidth, Me.PageSettings.Margins.Top + Me.Detail1.Height)
        'Me.CurrentPage.DrawLine()
        Me.Line1.AnchorBottom = True
        Me.Line2.AnchorBottom = True
    End Sub

    Private Sub rpt3_ReEdi_A_VLine_PageStart(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PageStart
        'PageDetailHeight = 0
        Me.Line1.AnchorBottom = True
        Me.Line2.AnchorBottom = True
    End Sub
End Class
