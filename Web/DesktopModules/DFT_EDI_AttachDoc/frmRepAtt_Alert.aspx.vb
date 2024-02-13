Public Partial Class frmRepAtt_Alert
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If Request.QueryString("retMsg") <> "" Then
                lblReturnMsg.Text = Request.QueryString("retMsg")
            End If
        End If
    End Sub

    Private Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.Click
        ClientScript.RegisterStartupScript(Page.GetType(), "mykey", "CancelEdit();", True)
    End Sub
End Class