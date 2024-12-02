Imports System.Web.UI
Imports System.Collections.Generic
Imports System.Reflection

Imports DotNetNuke
Imports DotNetNuke.Services.Exceptions
Imports DotNetNuke.Services.Localization

Public Class ctlSetUser
    Inherits Entities.Modules.PortalModuleBase

    Private conn As String = ConfigurationManager.ConnectionStrings("EDIConnection").ConnectionString
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

End Class