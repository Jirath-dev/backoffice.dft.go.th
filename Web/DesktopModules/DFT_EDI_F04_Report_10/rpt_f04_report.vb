Imports DataDynamics.ActiveReports
Imports DataDynamics.ActiveReports.Document

Imports Microsoft.ApplicationBlocks.Data
Imports System.Data.SqlClient
Imports System.Data
Imports System.Text
Imports System.Threading
Imports System.Globalization

Public Class rpt_f04_report
    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        '===========set Date thai==================================
        Thread.CurrentThread.CurrentCulture = New CultureInfo("th-TH")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("th-TH")
        '=============================================
    End Sub

    Private Sub PageHeader_Format(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PageHeader.Format

    End Sub
    Dim item_count As Integer = 0
    Private Sub Detail_Format(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Detail.Format
        item_count += 1
        TextBox11.Text = item_count & "."
    End Sub

    Private Sub GroupFooter1_Format(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GroupFooter1.Format
    End Sub


    Private Sub GroupHeader1_Format(sender As Object, e As EventArgs) Handles GroupHeader1.Format
        item_count = 0
    End Sub
End Class
