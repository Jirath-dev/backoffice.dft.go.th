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

Imports DotNetNuke
Imports DotNetNuke.Common
Imports DotNetNuke.Services.Exceptions
Imports DotNetNuke.Services.Localization

Namespace NTi.Modules.DFT_EDI_EntryForm

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' The EditDFT_EDI_EntryForm class is used to manage content
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Partial Class EditDFT_EDI_EntryForm
        Inherits Entities.Modules.PortalModuleBase

#Region "Private Members"

        Private ItemId As Integer = Common.Utilities.Null.NullInteger

#End Region

#Region "Event Handlers"

        ''' -----------------------------------------------------------------------------
        ''' <summary>
        ''' Page_Load runs when the control is loaded
        ''' </summary>
        ''' <remarks>
        ''' </remarks>
        ''' <history>
        ''' </history>
        ''' -----------------------------------------------------------------------------
        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            Try

                ' Determine ItemId of DFT_EDI_EntryForm to Update
                If Not (Request.QueryString("ItemId") Is Nothing) Then
                    ItemId = Int32.Parse(Request.QueryString("ItemId"))
                End If

                ' If this is the first visit to the page, bind the role data to the datalist
                If Page.IsPostBack = False Then

                    cmdDelete.Attributes.Add("onClick", "javascript:return confirm('" & Localization.GetString("DeleteItem") & "');")

                    If Not Common.Utilities.Null.IsNull(ItemId) Then
                        ' get content
                        Dim objDFT_EDI_EntryForms As New DFT_EDI_EntryFormController
                        Dim objDFT_EDI_EntryForm As DFT_EDI_EntryFormInfo = objDFT_EDI_EntryForms.GetDFT_EDI_EntryForm(ModuleId,ItemId)
                        If Not objDFT_EDI_EntryForm Is Nothing Then
                            txtContent.Text = objDFT_EDI_EntryForm.Content
                            ctlAudit.CreatedByUser = objDFT_EDI_EntryForm.CreatedByUserName
                            ctlAudit.CreatedDate = objDFT_EDI_EntryForm.CreatedDate.ToString
                        Else ' security violation attempt to access item not related to this Module
                            Response.Redirect(NavigateURL(), True)
                        End If
                    Else
                        cmdDelete.Visible = False
                        ctlAudit.Visible = False
                    End If
                End If

            Catch exc As Exception    'Module failed to load
                ProcessModuleLoadException(Me, exc)
            End Try
        End Sub

        ''' -----------------------------------------------------------------------------
        ''' <summary>
        ''' cmdCancel_Click runs when the cancel button is clicked
        ''' </summary>
        ''' <remarks>
        ''' </remarks>
        ''' <history>
        ''' </history>
        ''' -----------------------------------------------------------------------------
        Private Sub cmdCancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdCancel.Click
            Try
                Response.Redirect(NavigateURL(), True)
            Catch exc As Exception    'Module failed to load
                ProcessModuleLoadException(Me, exc)
            End Try
        End Sub

        ''' -----------------------------------------------------------------------------
        ''' <summary>
        ''' cmdUpdate_Click runs when the update button is clicked
        ''' </summary>
        ''' <remarks>
        ''' </remarks>
        ''' <history>
        ''' </history>
        ''' -----------------------------------------------------------------------------
        Private Sub cmdUpdate_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdUpdate.Click
            Try
                Dim objDFT_EDI_EntryForms As New DFT_EDI_EntryFormController

                Dim objDFT_EDI_EntryForm As DFT_EDI_EntryFormInfo = New DFT_EDI_EntryFormInfo

                objDFT_EDI_EntryForm.ModuleId = ModuleId
                objDFT_EDI_EntryForm.ItemId = ItemId
                objDFT_EDI_EntryForm.Content = txtContent.Text
                objDFT_EDI_EntryForm.CreatedByUser = Me.UserId

                If Common.Utilities.Null.IsNull(ItemId) Then
                    ' add the content within the DFT_EDI_EntryForm table
                    objDFT_EDI_EntryForms.AddDFT_EDI_EntryForm(objDFT_EDI_EntryForm)
                Else
                    ' update the content within the DFT_EDI_EntryForm table
                    objDFT_EDI_EntryForms.UpdateDFT_EDI_EntryForm(objDFT_EDI_EntryForm)
                End If

                ' Redirect back to the portal home page
                Response.Redirect(NavigateURL(), True)
            Catch exc As Exception    'Module failed to load
                ProcessModuleLoadException(Me, exc)
            End Try
        End Sub

        ''' -----------------------------------------------------------------------------
        ''' <summary>
        ''' cmdDelete_Click runs when the delete button is clicked
        ''' </summary>
        ''' <remarks>
        ''' </remarks>
        ''' <history>
        ''' </history>
        ''' -----------------------------------------------------------------------------
        Private Sub cmdDelete_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdDelete.Click
            Try
                ' Only attempt to delete the item if it exists already
                If Not Common.Utilities.Null.IsNull(ItemId) Then

                    Dim objDFT_EDI_EntryForms As New DFT_EDI_EntryFormController
                    objDFT_EDI_EntryForms.DeleteDFT_EDI_EntryForm(ModuleId,ItemId)

                End If

                ' Redirect back to the portal home page
                Response.Redirect(NavigateURL(), True)
            Catch exc As Exception    'Module failed to load
                ProcessModuleLoadException(Me, exc)
            End Try
        End Sub

#End Region

    End Class

End Namespace