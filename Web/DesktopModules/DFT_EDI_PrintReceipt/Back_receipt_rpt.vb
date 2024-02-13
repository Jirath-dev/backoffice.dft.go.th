Imports DataDynamics.ActiveReports 
Imports DataDynamics.ActiveReports.Document 

Public Class Back_receipt_rpt
    Dim Cpage As Integer
    Private Sub PageHeader1_Format(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PageHeader1.Format
        Cpage += 1
        txtPage2.Text = Cpage


    End Sub

    Private Sub PageFooter1_Format(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PageFooter1.Format

    End Sub
End Class
