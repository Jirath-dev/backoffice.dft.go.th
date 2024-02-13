Imports System.Data.SqlClient
Imports DFT.Dotnetnuke.ClassLibrary
Imports Microsoft.ApplicationBlocks.Data
Imports Telerik.Web.UI

Partial Public Class frmChangePersonRequest
    Inherits System.Web.UI.Page
    Dim strRegConn As String = ConfigurationManager.ConnectionStrings("RegisBackConnection").ConnectionString
    Dim strEDIConn As String = ConfigurationManager.ConnectionStrings("OriginConnection").ConnectionString
    Dim objConn As SqlConnection = Nothing
    Dim myReader As SqlDataReader = Nothing

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            lblCompany_Taxno.Text = CommonUtility.Get_StringValue(Session("ssCompanyTaxno"))
        End If
    End Sub

    Function LoadCardList() As SqlDataReader
        Try
            Dim strCommand As String
            strCommand = "select * From rfcard where active_flag='Z' And company_taxno = '" & lblCompany_Taxno.Text & "' Order by card_id desc"
            objConn = New SqlConnection(strEDIConn)
            myReader = SqlHelper.ExecuteReader(objConn, CommandType.Text, strCommand)
            Return myReader
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Function

    Private Sub rgCardList_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles rgCardList.DataBound
        myReader.Close()
        objConn.Close()
    End Sub

    Private Sub rgCardList_ItemCreated(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgCardList.ItemCreated
        If TypeOf e.Item Is GridDataItem Then
            Dim selectLink As HyperLink = DirectCast(e.Item.FindControl("SelectLink"), HyperLink)
            selectLink.Attributes("href") = "#"
            selectLink.Attributes("onclick") = [String].Format("return returnToParent('{0}');", e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("card_id"))
        End If
    End Sub

    Private Sub rgCardList_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgCardList.NeedDataSource
        rgCardList.DataSource = LoadCardList()
    End Sub
End Class