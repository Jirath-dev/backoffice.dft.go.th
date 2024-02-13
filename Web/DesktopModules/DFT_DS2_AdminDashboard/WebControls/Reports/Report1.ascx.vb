Public Partial Class Report1
    Inherits Entities.Modules.PortalModuleBase

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            txtFromDate.SelectedDate = Now
            txtToDate.SelectedDate = Now
            If ddlStatus.SelectedValue = "" Then
                ddlStatus.SelectedValue = "Q"
            End If
        End If
    End Sub

End Class