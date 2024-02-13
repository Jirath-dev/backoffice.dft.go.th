Imports DataDynamics.ActiveReports 
Imports DataDynamics.ActiveReports.Document 

Public Class rptEDI_Report_00 
    Dim index As Integer = 0
    Private Sub Detail1_Format(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Detail1.Format
        index = index + 1
        txtIndex.Text = CType(index, String)
        txtPrintDate.Text = "วันที่พิมพ์ : " & Convert.ToDateTime(Now).ToString("D", New System.Globalization.CultureInfo("th-TH"))
        txtApprove_Date.Text = Convert.ToDateTime(txtApprove_Date.Text).ToString("d", New System.Globalization.CultureInfo("en-GB"))
    End Sub

    Private Sub GroupHeader1_Format(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GroupHeader1.Format
        index = 0
    End Sub
End Class
