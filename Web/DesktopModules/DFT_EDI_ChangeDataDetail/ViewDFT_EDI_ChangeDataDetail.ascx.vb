'
' DotNetNukeÆ - http://www.dotnetnuke.com
' Copyright (c) 2002-2010
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

Imports System.Web.UI
Imports System.Collections.Generic
Imports System.Reflection

Imports DotNetNuke
Imports DotNetNuke.Services.Exceptions
Imports DotNetNuke.Services.Localization
Imports Microsoft.ApplicationBlocks.Data
Imports DFT.Dotnetnuke.ClassLibrary
Imports System.Data
Imports System.Data.SqlClient

Imports Telerik.Web.UI
Namespace YourCompany.Modules.DFT_EDI_ChangeDataDetail

    Partial Class ViewDFT_EDI_ChangeDataDetail
        Inherits Entities.Modules.PortalModuleBase
        Implements Entities.Modules.IActionable
        Dim strEDIConn As String = ConfigurationManager.ConnectionStrings("OriginConnection").ConnectionString
        Dim objConn As SqlConnection = Nothing
        Dim reader As SqlDataReader = Nothing

        Protected SiteUrl As String
#Region "Event Handlers"
        Function CheckFormValueDetail(ByVal _strForm As String) As String
            Dim str_form As String
            Dim ds As New DataSet
            ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.Text, "SELECT * FROM form_type WHERE     (form_type =" & "'" & _strForm & "')")
            If ds.Tables(0).Rows.Count > 0 Then
                str_form = ds.Tables(0).Rows(0).Item("form_name").ToString
            End If

            Return str_form
        End Function
#End Region

#Region "Optional Interfaces"

        ''' -----------------------------------------------------------------------------
        ''' <summary>
        ''' Registers the module actions required for interfacing with the portal framework
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' </history>
        ''' -----------------------------------------------------------------------------
        Public ReadOnly Property ModuleActions() As Entities.Modules.Actions.ModuleActionCollection Implements Entities.Modules.IActionable.ModuleActions
            Get
                Dim Actions As New Entities.Modules.Actions.ModuleActionCollection
                Actions.Add(GetNextActionID, Localization.GetString(Entities.Modules.Actions.ModuleActionType.AddContent, LocalResourceFile), Entities.Modules.Actions.ModuleActionType.AddContent, "", "", EditUrl(), False, Security.SecurityAccessLevel.Edit, True, False)
                Return Actions
            End Get
        End Property

#End Region

        
        Protected Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Me.Load
            If Not IsPostBack Then
                tblData1.Visible = False
                txtSearchValue.Focus()
            End If
            
        End Sub

        Protected Sub btnSearchForm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchForm.Click
            Try
                Dim TCAT As Integer
                If chkUseRef2.Checked Then TCAT = 2 Else TCAT = 1

                Dim ds As New DataSet
                ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, "vi_selectFormChangeDetail_NewDS", _
                New SqlParameter("@TCat", TCAT), _
                New SqlParameter("@invh_run_auto", CommonUtility.Get_StringValue(txtSearchValue.Text.Trim())))

                If ds.Tables(0).Rows.Count > 0 Then
                    txtform.Text = CheckFormValueDetail(ds.Tables(0).Rows(0).Item("form_type").ToString)
                    txtCard_id.Text = ds.Tables(0).Rows(0).Item("card_id").ToString
                    txtCom.Text = ds.Tables(0).Rows(0).Item("company_name").ToString

                    tblData1.Visible = True
                    grdItemData.Rebind()
                Else
                    tblData1.Visible = False
                    Me.RadAjaxManager1.ResponseScripts.Add("window.alert('‰¡Ëæ∫À¡“¬‡≈¢§”√ÈÕß∑’Ë∑”°“√§ÈπÀ“');")
                End If
            Catch ex As Exception
                Me.RadAjaxManager1.ResponseScripts.Add("window.alert('" & ex.Message & "');")
            End Try

        End Sub

        Private Sub grdItemData_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdItemData.DataBound
            reader.Close()
        End Sub

        Private Sub grdItemData_ItemCreated(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles grdItemData.ItemCreated
            If TypeOf e.Item Is GridDataItem Then
                Dim viewLink As HyperLink = DirectCast(e.Item.FindControl("ViewLink"), HyperLink)
                viewLink.Attributes("href") = "#"
                viewLink.Attributes("onclick") = [String].Format("return ShowViewForm('{0}','{1}','{2}');", e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("invh_run_auto"), e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("invd_run_auto"), "view")

                Dim editLink As HyperLink = DirectCast(e.Item.FindControl("EditLink"), HyperLink)
                editLink.Attributes("href") = "#"
                editLink.Attributes("onclick") = [String].Format("return ShowEditForm('{0}','{1}','{2}');", e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("invh_run_auto"), e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("invd_run_auto"), "edit")

                Dim deleteLink As HyperLink = DirectCast(e.Item.FindControl("DeleteLink"), HyperLink)
                deleteLink.Attributes("href") = "#"
                deleteLink.Attributes("onclick") = [String].Format("return ShowDeleteForm('{0}','{1}');", e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("invh_run_auto"), e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("invd_run_auto"))
            End If

            If Request.QueryString("action") = "view" Then
                grdItemData.MasterTableView.Columns(1).Visible = False
                grdItemData.MasterTableView.Columns(2).Visible = False
            End If
        End Sub

        Private Sub RadAjaxManager1_AjaxRequest(ByVal sender As Object, ByVal e As Telerik.Web.UI.AjaxRequestEventArgs) Handles RadAjaxManager1.AjaxRequest
            grdItemData.MasterTableView.SortExpressions.Clear()
            grdItemData.MasterTableView.GroupByExpressions.Clear()
            grdItemData.Rebind()
        End Sub

        Private Sub grdItemData_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles grdItemData.ItemDataBound
            If Not (e.Item.FindControl("lblIndex") Is Nothing) Then
                Dim lbl As Label = e.Item.FindControl("lblIndex")
                lbl.Text = (grdItemData.MasterTableView.CurrentPageIndex * grdItemData.MasterTableView.PageSize) + (e.Item.ItemIndex + 1)
            End If
        End Sub

        Private Sub grdItemData_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles grdItemData.NeedDataSource
            grdItemData.DataSource = LoadGridDetail()
        End Sub

        Private Function LoadGridDetail() As SqlDataReader
            Try
                Dim TCAT As Integer
                If chkUseRef2.Checked Then TCAT = 2 Else TCAT = 1

                'objConn = New SqlConnection(strConn)
                reader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "vi_selectFormChangeDetail_NewDS", _
                New SqlParameter("@TCat", TCAT), _
                New SqlParameter("@invh_run_auto", CommonUtility.Get_StringValue(txtSearchValue.Text.Trim())))

                'reader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "sp_common_form_edi_getDetailListAll_NewDS", New SqlParameter("@INVH_RUN_AUTO", txtInvHRunAuto.Text))
                Return reader
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End Function
    End Class
End Namespace
