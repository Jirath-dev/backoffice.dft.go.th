Imports System.Web.UI
Imports System.Collections.Generic
Imports System.Reflection

Imports DotNetNuke
Imports DotNetNuke.Services.Exceptions
Imports DotNetNuke.Services.Localization

Imports DFT.Dotnetnuke.ClassLibrary
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports Telerik.Web.UI

Partial Public Class RemoveName
    Inherits Entities.Modules.PortalModuleBase

    Dim strConn As String = ConfigurationManager.ConnectionStrings("Ds2OriginConnection").ConnectionString
    Dim reader As SqlDataReader = Nothing
    Dim objConn As SqlConnection = Nothing

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            txtFromDate.SelectedDate = Now

            LoadSite()
        End If
    End Sub

    Private Sub LoadSite()
        Try
            Dim ds As New DataSet
            Dim strCommand As String
            strCommand = "SELECT site_name,site_id FROM site_plus " & _
                         "WHERE (active_status = 'Y') AND (type_site = 'A') AND (ds_status='Y') " & _
                         "ORDER BY site_code"
            ds = SqlHelper.ExecuteDataset(strConn, CommandType.Text, strCommand)
            If ds.Tables(0).Rows.Count > 0 Then
                ddlSite.DataSource = ds.Tables(0)
                ddlSite.DataTextField = "site_name"
                ddlSite.DataValueField = "site_id"
                ddlSite.DataBind()
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
        grdMasterData.DataSource = LoadDataGrid()
        grdMasterData.DataBind()
        PageLoadState.Value = 1
    End Sub

    Private Function LoadDataGrid() As SqlDataReader
        Try
            objConn = New SqlConnection(strConn)
            reader = SqlHelper.ExecuteReader(strConn, CommandType.StoredProcedure, "vi_form_edi_getCheckDocName_NewDS2", _
                        New SqlParameter("@SITE_ID", ddlSite.SelectedValue), _
                        New SqlParameter("@FROM_DATE", FunctionUtility.DMY2YMD(txtFromDate.SelectedDate.Value)))

            Return reader
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Function

    Private Sub grdMasterData_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles grdMasterData.NeedDataSource
        If PageLoadState.Value = "1" Then
            grdMasterData.DataSource = LoadDataGrid()
        End If
    End Sub

    Private Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload
        If Not objConn Is Nothing Then
            If objConn.State = ConnectionState.Open Then
                objConn.Close()
            End If
        End If
    End Sub

    Protected Sub grdMasterData_ItemCommand(ByVal source As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles grdMasterData.ItemCommand
        If e.CommandName = "DeleteSelected" Then
            If grdMasterData.SelectedIndexes.Count = 0 Then
                Return
            End If

            Dim ret As Integer = 0
            Dim totaldo As Integer = 0

            For Each item As GridDataItem In grdMasterData.SelectedItems

                Dim refid As String
                refid = item.GetDataKeyValue("invh_run_auto").ToString()

                ret = SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, "sp_edi_removeCheckDocUser_NewDS2", _
                                                New SqlParameter("@INVH_RUN_AUTO", refid), _
                                                New SqlParameter("@USERNAME", ""), _
                                                New SqlParameter("@ACTION", 1))

                totaldo = totaldo + ret

            Next

            'show data
            If totaldo > 0 Then
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "mykey", "alert('Completed - บันทึกข้อมูลเรียบร้อยแล้ว (" & totaldo.ToString() & ")');", True)
            Else
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "mykey1", "alert('Error - ไม่สามารถบันทึกข้อมูลได้');", True)
            End If

            e.Item.OwnerTableView.Rebind()
            Return
        End If

    End Sub

    Protected Sub btnRemove_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnRemove.Click
        Try
            Dim ret As Integer
            'update site id
            ret = SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, "sp_edi_removeCheckDocUser_NewDS2", _
                                            New SqlParameter("@INVH_RUN_AUTO", txtRefNo.Text.Trim()), _
                                            New SqlParameter("@USERNAME", ""), _
                                            New SqlParameter("@ACTION", 1))
            'show data
            If ret > 0 Then
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "mykey", "alert('Completed - บันทึกข้อมูลเรียบร้อยแล้ว');", True)
            Else
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "mykey", "alert('Error - ไม่สามารถบันทึกข้อมูลได้');", True)
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub
End Class