Imports System.Web.UI
Imports System.Collections.Generic
Imports System.Reflection

Imports DotNetNuke

Imports DFT.Dotnetnuke.ClassLibrary
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports Telerik.Web.UI

Partial Public Class ListEDIStatus
    Inherits Entities.Modules.PortalModuleBase

    Dim strConn As String = ConfigurationManager.ConnectionStrings("Ds2OriginConnection").ConnectionString

    Dim reader As SqlDataReader = Nothing
    Dim objConn As SqlConnection = New SqlConnection

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            txtFromDate.SelectedDate = Now
            txtToDate.SelectedDate = Now
        End If
    End Sub

    Private Sub grdMasterData_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles grdMasterData.NeedDataSource
        grdMasterData.DataSource = LoadDataGrid()
    End Sub

    Private Function LoadDataGrid() As SqlDataReader
        Try
            'objConn = New SqlConnection(strConn)
            reader = SqlHelper.ExecuteReader(strConn, CommandType.StoredProcedure, "vi_form_edi_getListEDIStatus_NewDS2", _
                New SqlParameter("@FROM_DATE", FunctionUtility.DMY2YMD(txtFromDate.SelectedDate.Value)), _
                New SqlParameter("@TO_DATE", FunctionUtility.DMY2YMD(txtToDate.SelectedDate.Value)))
            Return reader
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Function

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
        grdMasterData.CurrentPageIndex = 0
        grdMasterData.DataSource = LoadDataGrid()
        grdMasterData.DataBind()
    End Sub

End Class