Imports DataDynamics.ActiveReports 
Imports DataDynamics.ActiveReports.Document 

Public Class rptAttachDoc_Ver1 
    Dim index As Integer = 0

    Private Sub Detail1_Format(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Detail1.Format
        index = index + 1
        txtIndex.Text = CStr(index)
        txtApprove_Date.Text = Convert.ToDateTime(txtApprove_Date.Text).ToString("G", New System.Globalization.CultureInfo("en-GB"))
    End Sub

    Private Sub GroupHeader1_Format(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GroupHeader1.Format
        index = 0
    End Sub
End Class
