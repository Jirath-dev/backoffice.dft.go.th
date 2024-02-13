Imports DotNetNuke.Security.Membership
Imports DotNetNuke.Entities.Users
Imports DotNetNuke.Services.Authentication
Imports Microsoft.ApplicationBlocks.Data
Imports System.Data.SqlClient

Partial Class admin_Authentication_ssologin
    Inherits System.Web.UI.Page

    Private _PortalName As String = "edi-back.me"

    Private Sub admin_Authentication_ssologin_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim _username As String = Request.Form("username")
            Dim _password As String = Request.Form("password")
            Dim _appname As String = Request.Form("appname")
            If _appname = "ssomoc" Then
                Login(_username, _password)
            End If

        End If
    End Sub

    Private Sub Login(usr As String, pass As String)
        Dim AuthType As String = "DNN"

        Dim status As DotNetNuke.Security.Membership.UserLoginStatus = New DotNetNuke.Security.Membership.UserLoginStatus()
        Dim userInfo As DotNetNuke.Entities.Users.UserInfo = DotNetNuke.Entities.Users.UserController.ValidateUser(0, usr, pass, AuthType, "", _PortalName, DotNetNuke.Services.Authentication.AuthenticationLoginBase.GetIPAddress(), status)

        Select Case status
            Case UserLoginStatus.LOGIN_SUCCESS
                ValidateUser(userInfo, False)
            Case UserLoginStatus.LOGIN_SUPERUSER
                ValidateUser(userInfo, False)
            Case UserLoginStatus.LOGIN_FAILURE
                Response.Write("Login Failed.")
        End Select
    End Sub

    Private Sub ValidateUser(ByVal objUser As UserInfo, ByVal ignoreExpiring As Boolean)
        Dim AuthType As String = "DNN"
        Dim RedirectURL As String = ""
        If Not Request.QueryString("returnurl") Is Nothing Then
            RedirectURL = Request.QueryString("returnurl").ToString
        End If

        ' Response.Redirect(NavigateURL(PortalSettings.HomeTabId, "Home"), True)

        Dim validStatus As UserValidStatus = UserValidStatus.VALID
        Dim strMessage As String = Null.NullString
        Dim expiryDate As DateTime = Null.NullDate

        If Not objUser.IsSuperUser Then
            validStatus = UserController.ValidateUser(objUser, 0, ignoreExpiring)
        End If

        'Check if the User has valid Password/Profile
        Select Case validStatus
            Case UserValidStatus.VALID
                'Set the Page Culture(Language) based on the Users Preferred Locale
                If (Not objUser.Profile Is Nothing) AndAlso (Not objUser.Profile.PreferredLocale Is Nothing) Then
                    Localization.SetLanguage(objUser.Profile.PreferredLocale)
                Else
                    Localization.SetLanguage("th-TH")
                End If

                'Set the Authentication Type used 
                AuthenticationController.SetAuthenticationType(AuthType)

                'Complete Login
                UserController.UserLogin(0, objUser, _PortalName, AuthenticationLoginBase.GetIPAddress(), False)

                ' redirect browser
                Response.Redirect("http://edi-back.me", True)

        End Select

    End Sub

End Class
