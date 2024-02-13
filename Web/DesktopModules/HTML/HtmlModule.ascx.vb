'
' DotNetNuke® - http://www.dotnetnuke.com
' Copyright (c) 2002-2007
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

Imports DotNetNuke
Imports System.Web.UI
Imports DotNetNuke.Entities.Modules

Namespace DotNetNuke.Modules.Html

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' The HtmlModule Class provides the UI for displaying the Html
    ''' </summary>
       ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Partial Class HtmlModule
		Inherits Entities.Modules.PortalModuleBase
		Implements Entities.Modules.IActionable

#Region "Event Handlers"

        ''' -----------------------------------------------------------------------------
        ''' <summary>
        ''' Page_Load runs when the control is loaded
        ''' </summary>
        ''' <remarks>
        ''' </remarks>
        ''' <history>
        ''' [sleupold] 08/20/2007   Use of TokenReplace added
        ''' </history>
        ''' -----------------------------------------------------------------------------
        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            Try

                ' edit in place
                lblContent.EditEnabled = Me.IsEditable

                ' get HtmlText object
                Dim objHTML As New HtmlTextController
                Dim objText As HtmlTextInfo = objHTML.GetHtmlText(ModuleId)

                ' get default content from resource file
                Dim strContent As String = Localization.GetString("AddContentFromToolBar.Text", LocalResourceFile)
                If Entities.Portals.PortalSettings.GetSiteSetting(PortalId, "InlineEditorEnabled") = "False" Then
                    lblContent.EditEnabled = False
                    strContent = Localization.GetString("AddContentFromActionMenu.Text", LocalResourceFile)
                End If

                ' get html
                If Not objText Is Nothing Then
                    strContent = Server.HtmlDecode(CType(objText.DeskTopHTML, String))
                End If

                ' token replace
                If CType(Settings("TEXTHTML_ReplaceTokens"), String) <> "" Then
                    If CType(Settings("TEXTHTML_ReplaceTokens"), Boolean) = True Then
                        Dim tr As New DotNetNuke.Services.Tokens.TokenReplace()
                        tr.AccessingUser = UserInfo
                        tr.DebugMessages = Not IsTabPreview()
                        strContent = tr.ReplaceEnvironmentTokens(strContent)
                        lblContent.EditEnabled = False
                    End If
                End If

                ' localize toolbar
                If lblContent.EditEnabled Then
                    For Each objButton As DotNetNuke.UI.WebControls.DNNToolBarButton In Me.tbEIPHTML.Buttons
                        objButton.ToolTip = Services.Localization.Localization.GetString("cmd" & objButton.ToolTip, LocalResourceFile)
                    Next
                Else
                    Me.tbEIPHTML.Visible = False
                End If

                ' add content to module
                lblContent.Controls.Add(New LiteralControl(DotNetNuke.Common.Globals.ManageUploadDirectory(strContent, PortalSettings.HomeDirectory)))

            Catch exc As Exception
                ProcessModuleLoadException(Me, exc)
            End Try
        End Sub

        ''' -----------------------------------------------------------------------------
        ''' <summary>
        ''' lblContent_UpdateLabel allows for inline editing of content
        ''' </summary>
        ''' <remarks>
        ''' </remarks>
        ''' <history>
        ''' </history>
        ''' -----------------------------------------------------------------------------
        Private Sub lblContent_UpdateLabel(ByVal source As Object, ByVal e As UI.WebControls.DNNLabelEditEventArgs) Handles lblContent.UpdateLabel

            ' verify security 
            If Me.IsEditable Then
                ' get HtmlText object
                Dim objHTML As HtmlTextController = New HtmlTextController
                Dim objText As HtmlTextInfo = objHTML.GetHtmlText(ModuleId)

                ' check if this is a new module instance
                Dim blnIsNew As Boolean = False
                If objText Is Nothing Then
                    objText = New HtmlTextInfo
                    blnIsNew = True
                End If

                ' set content values
                objText.ModuleId = ModuleId
                objText.DeskTopHTML = e.Text
                objText.CreatedByUser = Me.UserId

                ' save the content
                If blnIsNew Then
                    objHTML.AddHtmlText(objText)
                Else
                    objHTML.UpdateHtmlText(objText)
                End If

                ' refresh cache
                ModuleController.SynchronizeModule(ModuleId)
            End If

        End Sub

#End Region

#Region "Optional Interfaces"

        Public ReadOnly Property ModuleActions() As Entities.Modules.Actions.ModuleActionCollection Implements Entities.Modules.IActionable.ModuleActions
            Get
                Dim Actions As New Entities.Modules.Actions.ModuleActionCollection
                Actions.Add(GetNextActionID, Localization.GetString(Entities.Modules.Actions.ModuleActionType.AddContent, LocalResourceFile), Entities.Modules.Actions.ModuleActionType.AddContent, "", "", EditUrl(), False, Security.SecurityAccessLevel.Edit, True, False)
                Return Actions
            End Get
        End Property

#End Region

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub

        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

    End Class

End Namespace

