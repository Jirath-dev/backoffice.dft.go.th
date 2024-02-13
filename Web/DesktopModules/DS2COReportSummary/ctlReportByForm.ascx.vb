Imports Microsoft.ApplicationBlocks.Data
Imports Telerik.Web.UI
Imports System.Data.SqlClient

Partial Public Class ctlReportByForm
    Inherits Entities.Modules.PortalModuleBase

    Dim strEDIConn As String = ConfigurationManager.ConnectionStrings("OriginConnection").ConnectionString

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then

            'Load site
            Try
                Dim ds As New DataSet
                Dim sql As String
                sql = "SELECT B.site_id,B.site_name " & _
                           "FROM [dbo].[EDI_REPORT_04] A " & _
                           "LEFT JOIN [dbo].[site] B " & _
                           "ON A.Site_ID=B.site_id " & _
                           "WHERE typeDescription IN(4,5) " & _
                           "GROUP BY B.Site_ID,B.site_name,B.ShowOrder_Report " & _
                           "ORDER BY B.ShowOrder_Report "
                ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.Text, sql)

                ddlSite.DataSource = ds
                ddlSite.DataValueField = "site_id"
                ddlSite.DataTextField = "site_name"
                ddlSite.DataBind()

                'Load year
                Dim y As Integer
                y = Date.Now.Year
                For c = y To 2010 Step -1
                    ddlYear.Items.Add(New RadComboBoxItem(c + 543, c))
                Next

                'Set site
                If Request.QueryString("site") <> "" Then
                    ddlSite.Items.FindItemByValue(Request.QueryString("site")).Selected = True
                Else
                    ddlSite.SelectedIndex = 0
                End If

                If Request.QueryString("yy") <> "" Then
                    ddlYear.Items.FindItemByValue(Request.QueryString("yy")).Selected = True
                Else
                    ddlYear.Items.FindItemByValue(Date.Today.Year.ToString()).Selected = True
                End If

                If Request.QueryString("mm") <> "" Then
                    ddlMonth.Items.FindItemByValue(Request.QueryString("mm")).Selected = True
                Else
                    ddlMonth.SelectedIndex = Date.Today.Month - 1
                End If

            Catch ex As Exception
                Response.Write(ex.Message)
            End Try

        End If

    End Sub

    Protected Function LoadData() As DataTable
        Try
            Dim strEDIConn As String = ConfigurationManager.ConnectionStrings("OriginConnection").ConnectionString

            Dim ds As New DataSet
            ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, "sp_report_ds2_SummaryBySite", _
            New SqlParameter("@Month", ddlMonth.SelectedValue), _
            New SqlParameter("@Year", ddlYear.SelectedValue), _
            New SqlParameter("@SiteId", ddlSite.SelectedValue))

            ' lblTitle.Text = "เดือน" & ddlMonth.Text & "  &nbsp;&nbsp;พ.ศ. " & ddlYear.Text

            Return ds.Tables(0)

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Function

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
        GridData.DataSource = LoadData()
        GridData.DataBind()
    End Sub

    Private Sub GridData_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles GridData.NeedDataSource
        GridData.DataSource = LoadData()
    End Sub

End Class