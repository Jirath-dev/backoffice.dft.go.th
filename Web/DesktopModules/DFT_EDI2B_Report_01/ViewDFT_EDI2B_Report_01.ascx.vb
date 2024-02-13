Imports System.Web.UI
Imports System.Collections.Generic
Imports System.Reflection

Imports DotNetNuke
Imports DotNetNuke.Services.Exceptions
Imports DotNetNuke.Services.Localization

Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports DFT.Dotnetnuke.ClassLibrary
Imports Telerik.Web.UI

Namespace NTi.Modules.DFT_EDI2B_Report_01
    Partial Class ViewDFT_EDI2B_Report_01
        Inherits Entities.Modules.PortalModuleBase
        Dim strConn As String = ConfigurationManager.ConnectionStrings("OriginConnection").ConnectionString

        Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            If Not Page.IsPostBack Then

                LoadSite()
                ddlSiteID.SelectedIndex = 0

                rdpFromDate.SelectedDate = Today
                rdpToDate.SelectedDate = Today
            End If

        End Sub

        Public Sub LoadSite()
            Dim cmd As String = "select * from site_plus p where p.active_status = 'Y' and p.site_id not in ('ST-001T','ST-001R') order by site_name asc"
            Try
                Dim ds As New DataSet
                ds = SqlHelper.ExecuteDataset(strConn, CommandType.Text, cmd)
                If ds.Tables(0).Rows.Count > 0 Then

                    ddlSiteID.DataSource = ds
                    ddlSiteID.DataTextField = "site_name"
                    ddlSiteID.DataValueField = "site_id"
                    ddlSiteID.DataBind()

                    ddlSiteID.Items.Insert(0, New ListItem("-- ทั้งหมด --", ""))
                End If
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End Sub




        Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
            grdData.DataSource = LoadData()
            grdData.DataBind()
        End Sub

        Function LoadData() As DataTable
            Try
                Dim ds As New DataSet

                ds = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, "SP_ASW_Report_PrintOption",
                New SqlParameter("@start_date", rdpFromDate.SelectedDate),
                New SqlParameter("@end_date", rdpToDate.SelectedDate),
                New SqlParameter("@site_id", ddlSiteID.SelectedValue))

                Return ds.Tables(0)
            Catch ex As Exception
            End Try
        End Function

        Private Sub btnExcel_Click(sender As Object, e As EventArgs) Handles btnExcel.Click
            Try
                For Each gi As GridItem In grdData.MasterTableView.GetItems(GridItemType.GroupHeader)
                    gi.Expanded = True
                Next
                grdData.ExportSettings.ExportOnlyData = True
                grdData.ExportSettings.IgnorePaging = True
                grdData.MasterTableView.ExportToExcel()

            Catch ex As Exception
                Response.Write(ex.Message)
            End Try

        End Sub
    End Class

End Namespace
