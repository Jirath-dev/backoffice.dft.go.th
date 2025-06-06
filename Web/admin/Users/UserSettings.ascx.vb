'
' DotNetNukeŽ - http://www.dotnetnuke.com
' Copyright (c) 2002-2009
' by DotNetNuke Corporation
'
' Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated 
' documentation files (the "Software"), to deal in the Software without restriction, including without limitation 
' the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and 
' to permit persons to whom the Software is furnished to do so, subject to the following conditions:
'
' The above copyright notice and this permission notice shall be included in all copies or substantial portions 
' of the Software.
'
' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED 
' TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL 
' THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF 
' CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
' DEALINGS IN THE SOFTWARE.
'

Imports DotNetNuke.Entities.Modules
Imports DotNetNuke.Security.Membership
Imports DotNetNuke.UI.Utilities
Imports DotNetNuke.UI.WebControls

Imports System.Threading

Namespace DotNetNuke.Modules.Admin.Users

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' The UserSettings PortalModuleBase is used to manage User Settings for the portal
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[cnurse]	03/02/2006
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Partial Class UserSettings
        Inherits UserModuleBase

#Region "Event Handlers"

        ''' -----------------------------------------------------------------------------
        ''' <summary>
        ''' Page_Load runs when the control is loaded
        ''' </summary>
        ''' <remarks>
        ''' </remarks>
        ''' <history>
        ''' 	[cnurse]	03/02/2006  Created
        ''' </history>
        ''' -----------------------------------------------------------------------------
        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

            'Bind User Controller to PropertySettings
            Dim config As New MembershipProviderConfig
            If MembershipProviderConfig.CanEditProviderProperties Then
                ProviderSettings.EditMode = PropertyEditorMode.Edit
            Else
                ProviderSettings.EditMode = PropertyEditorMode.View
            End If
            ProviderSettings.LocalResourceFile = Me.LocalResourceFile
            ProviderSettings.DataSource = config
            ProviderSettings.DataBind()

            If UserInfo.IsSuperUser Then
                PasswordSettings.EditMode = PropertyEditorMode.Edit
            Else
                PasswordSettings.EditMode = PropertyEditorMode.View
            End If
            PasswordSettings.LocalResourceFile = Me.LocalResourceFile
            PasswordSettings.DataSource = New PasswordConfig
            PasswordSettings.DataBind()

            'Create a hashtable for the custom editors being used, using the same keys
            'as in the settings hashtable
            Dim editors As New Hashtable
            editors("Redirect_AfterLogin") = EditorInfo.GetEditor("Page")
            editors("Redirect_AfterLogout") = EditorInfo.GetEditor("Page")
            editors("Redirect_AfterRegistration") = EditorInfo.GetEditor("Page")
            'editors("Security_EmailValidation") = "DotNetNuke.UI.WebControls.MultiLineTextEditControl, DotNetNuke"

            'Create a Hashtable for the custom Visibility options
            Dim visibility As New Hashtable
            If Me.IsHostMenu Then
                visibility("Profile_DefaultVisibility") = False
                visibility("Profile_DisplayVisibility") = False
                visibility("Profile_ManageServices") = False
                visibility("Redirect_AfterLogin") = False
                visibility("Redirect_AfterRegistration") = False
                visibility("Redirect_AfterLogout") = False
                visibility("Security_CaptchaLogin") = False
                visibility("Security_CaptchaRegister") = False
                visibility("Security_EmailValidation") = False
                visibility("Security_RequireValidProfile") = False
                visibility("Security_RequireValidProfileAtLogin") = False
                visibility("Security_UsersControl") = False
            End If

            UserSettings.LocalResourceFile = Me.LocalResourceFile

            'Get the settings
            Dim settings As New Hashtable
            Dim settingsEnumerator As IDictionaryEnumerator = GetSettings(UserController.GetUserSettings(UserPortalID)).GetEnumerator()
            Dim key As String
            Dim prefix As String
            Dim index As Integer
            While settingsEnumerator.MoveNext()
                key = CType(settingsEnumerator.Key, String)
                index = key.IndexOf("_")
                If index > 0 Then
                    'Get the prefix
                    prefix = key.Substring(0, index + 1)
                    Select Case prefix
                        Case "Column_", "Display_", "Profile_", "Records_", "Redirect_", "Security_"
                            settings.Add(key, settingsEnumerator.Value)
                    End Select
                End If
            End While

            UserSettings.DataSource = settings
            UserSettings.CustomEditors = editors
            UserSettings.Visibility = visibility
            UserSettings.DataBind()

        End Sub

        ''' -----------------------------------------------------------------------------
        ''' <summary>
        ''' cmdCancel_Click runs when the Cancel Button is clicked
        ''' </summary>
        ''' <remarks>
        ''' </remarks>
        ''' <history>
        ''' 	[cnurse]	03/02/2006
        ''' </history>
        ''' -----------------------------------------------------------------------------
        Private Sub cmdCancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdCancel.Click
            Dim FilterParams(IIf(Request.QueryString("filterproperty") = "", 1, 2)) As String

            If (Request.QueryString("filterProperty") = "") Then
                FilterParams.SetValue("filter=" & Request.QueryString("filter"), 0)
            Else
                FilterParams.SetValue("filter=" & Request.QueryString("filter"), 0)
                FilterParams.SetValue("filterProperty=" & Request.QueryString("filterProperty"), 1)
            End If
            Response.Redirect(NavigateURL(TabId, "", FilterParams), True)

        End Sub

        ''' -----------------------------------------------------------------------------
        ''' <summary>
        ''' cmdUpdate_Click runs when the Update Button is clicked
        ''' </summary>
        ''' <remarks>
        ''' </remarks>
        ''' <history>
        ''' 	[cnurse]	03/02/2006
        ''' </history>
        ''' -----------------------------------------------------------------------------
        Private Sub cmdUpdate_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdUpdate.Click

            Dim objModuleController As New ModuleController
            Dim key As String = Null.NullString
            Dim setting As String = Null.NullString

            Dim objModule As ModuleInfo = objModuleController.GetModuleByDefinition(UserPortalID, "User Accounts")
            If Not objModule Is Nothing Then
                For Each settingsEditor As FieldEditorControl In UserSettings.Fields
                    If settingsEditor.IsDirty Then
                        key = settingsEditor.Editor.Name
                        setting = Convert.ToString(settingsEditor.Editor.Value)
                        If key = "Security_DisplayNameFormat" Then
                            'Update the DisplayName of all Users in the portal
                            Dim objUserController As New UserController
                            objUserController.PortalId = UserPortalID
                            objUserController.DisplayFormat = setting
                            Dim objThread As New Thread(AddressOf objUserController.UpdateDisplayNames)
                            objThread.Start()
                        End If
                        objModuleController.UpdateModuleSetting(objModule.ModuleID, key, setting)
                    End If
                Next
            End If

            'Clear the UserSettings Cache
            DataCache.RemoveCache(UserController.SettingsKey(UserPortalID))

            Response.Redirect(NavigateURL(TabId, "", IIf(Request.QueryString("filter") <> "", "filter=" & Request.QueryString("filter"), "").ToString), True)
        End Sub

#End Region

    End Class

End Namespace