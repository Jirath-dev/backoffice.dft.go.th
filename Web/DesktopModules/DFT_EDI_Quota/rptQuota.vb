Imports DataDynamics.ActiveReports 
Imports DataDynamics.ActiveReports.Document 

Public Class rptQuota 

    Private Sub PageHeader1_Format(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PageHeader1.Format
        lblQuotaYear.Text = "ประจำปี " & CType(Now.Year + 543, String)
    End Sub

    Private Sub Detail1_Format(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Detail1.Format
        If txtTSK_Date.Text = "&nbsp;" Then txtTSK_Date.Text = ""
        If txtTSK_Debit.Text = "&nbsp;" Then txtTSK_Debit.Text = ""
        If txtCompanyName_En.Text = "&nbsp;" Then txtCompanyName_En.Text = ""
        If txtTSK_Credit.Text = "&nbsp;" Then txtTSK_Credit.Text = ""
        If txtReference_code2.Text = "&nbsp;" Then txtReference_code2.Text = ""
        If txtTSK_Credit2.Text = "&nbsp;" Then txtTSK_Credit2.Text = ""
        If txtReferenceDesc1.Text = "&nbsp;" Then txtReferenceDesc1.Text = ""
        If txtReferenceDesc2.Text = "&nbsp;" Then txtReferenceDesc2.Text = ""
        If txtAmount.Text = "&nbsp;" Then txtAmount.Text = ""
    End Sub
End Class
