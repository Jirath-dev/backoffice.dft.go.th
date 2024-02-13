Imports DataDynamics.ActiveReports 
Imports DataDynamics.ActiveReports.Document 

Public Class rptEDI_Report_02_Type2 
    Dim INVH_RUN_AUTO As String = ""

    Private Sub Detail1_Format(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Detail1.Format
        If INVH_RUN_AUTO = txtBill_No.Text Then
            txtBill_No.Text = ""
        Else
            INVH_RUN_AUTO = txtBill_No.Text
        End If

        txtBILL_DATE.Text = Convert.ToDateTime(txtBILL_DATE.Text).ToString("dd/MM/yyyy [HH:mm]")
        txtRECEIPT_NAME.Text = "∫√‘…—∑ : " & txtRECEIPT_NAME.Text
    End Sub
End Class
