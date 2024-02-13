Public Partial Class Report
    Inherits Entities.Modules.PortalModuleBase

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load



    End Sub

    Protected Sub linkReport1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles linkReport1.Click
        Response.Redirect(EditUrl("Report1"))
    End Sub
End Class