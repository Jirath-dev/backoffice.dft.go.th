Imports System.Web.UI
Imports System.Collections.Generic
Imports System.Reflection

Imports DotNetNuke
Imports DotNetNuke.Services.Exceptions
Imports DotNetNuke.Services.Localization

Imports DFT.Dotnetnuke.ClassLibrary
Imports Microsoft.ApplicationBlocks.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI

Namespace NTi.Modules.DFT_EDI_AUsers
    Partial Class ViewDFT_EDI_AUsers
        Inherits Entities.Modules.PortalModuleBase
        Dim strConn As String = ConfigurationManager.ConnectionStrings("OriginConnection").ConnectionString
        Dim objConn As sqlconnection = Nothing
        Dim myReader As SqlDataReader = Nothing

        Private Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
            Call LoadSite()
        End Sub

        Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            If Not Page.IsPostBack Then

            End If
        End Sub

        Private Sub rgUserList_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles rgUserList.DataBound
            myReader.Close()
            objConn.Close()
        End Sub

        Private Sub rgUserList_ItemCreated(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgUserList.ItemCreated
            If TypeOf e.Item Is GridDataItem Then
                Dim viewLink As HyperLink = DirectCast(e.Item.FindControl("ViewLink"), HyperLink)
                viewLink.Attributes("href") = "#"
                viewLink.Attributes("onclick") = [String].Format("return ShowViewForm('{0}','{1}');", e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("user_id"), "view")

                Dim editLink As HyperLink = DirectCast(e.Item.FindControl("EditLink"), HyperLink)
                editLink.Attributes("href") = "#"
                editLink.Attributes("onclick") = [String].Format("return ShowEditForm('{0}','{1}');", e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("user_id"), "edit")

                Dim deleteLink As HyperLink = DirectCast(e.Item.FindControl("DeleteLink"), HyperLink)
                deleteLink.Attributes("href") = "#"
                deleteLink.Attributes("onclick") = [String].Format("return ShowDeleteForm('{0}');", e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("user_id"))
            End If
        End Sub

        Private Sub rgUserList_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgUserList.NeedDataSource
            rgUserList.DataSource = LoadUsers()
        End Sub

        Function LoadUsers() As SqlDataReader
            Try
                Dim strCommand As String
                strCommand = "Select * From users WHERE site_id = '" & ddlSite.SelectedItem.Value & "'"
                objConn = New SqlConnection(strConn)
                myReader = SqlHelper.ExecuteReader(objConn, CommandType.Text, strCommand)

                Return myReader
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End Function

        Protected Sub imbSearch_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imbSearch.Click
            rgUserList.DataSource = LoadUsers()
            rgUserList.DataBind()
        End Sub

        Private Sub LoadSite()
            Try
                Dim ds As New DataSet
                ds = SqlHelper.ExecuteDataset(strConn, CommandType.Text, "Select * From Site Order by Site_Name")
                If ds.Tables(0).Rows.Count > 0 Then
                    ddlSite.DataSource = ds.Tables(0)
                    ddlSite.DataTextField = "site_name"
                    ddlSite.DataValueField = "site_id"
                    ddlSite.DataBind()
                End If

                ddlSite.Items.Insert(0, New ListItem("กรุณาเลือกรายการ", 0))
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End Sub
    End Class
End Namespace
