
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


Namespace NTI.Modules.DS2COReportSummary

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' The ViewDS2COReportSummary class displays the content
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Partial Class ViewDS2COReportSummary
        Inherits Entities.Modules.PortalModuleBase

        Dim strEDIConn As String = ConfigurationManager.ConnectionStrings("OriginConnection").ConnectionString

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
            If Not Page.IsPostBack Then
                'Load year
                Dim y As Integer
                y = Date.Now.Year
                For c = y To 2010 Step -1
                    ddlYear.Items.Add(New RadComboBoxItem(c + 543, c))
                Next

                'Set year
                ddlYear.Items(0).Selected = True
                ddlMonth.SelectedIndex = Date.Today.Month - 1

                'Load Data
                GridData.DataSource = LoadData()
                GridData.DataBind()
                GridData.MasterTableView.Caption = "<b>สรุปรายงานจำนวนการขอหนังสือรับรองถิ่นกำเนิดสินค้า " & "เดือน" & ddlMonth.Items(ddlMonth.SelectedIndex).Text & "  &nbsp;&nbsp;พ.ศ. " & ddlYear.Items(ddlYear.SelectedIndex).Text & "</b>"

                lblTitle.Text = "เดือน" & ddlMonth.Items(ddlMonth.SelectedIndex).Text & "  &nbsp;&nbsp;พ.ศ. " & ddlYear.Items(ddlYear.SelectedIndex).Text

                'Load chart.
                RadChart1.DataSource = LoadChartData().Tables(0)
                RadChart1.DataBind()

                'Load chart2.
                RadChart2.DataSource = LoadChartData().Tables(1)
                RadChart2.DataBind()

            End If
        End Sub

        Protected Function LoadChartData() As DataSet
            Dim dS As DataSet
            dS = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, "sp_report_ds2_SummaryChart", _
                New SqlParameter("@Month", ddlMonth.SelectedValue), _
                New SqlParameter("@Year", ddlYear.SelectedValue))
            Return dS
        End Function

        Protected Function LoadData() As DataTable
            Try

                Dim ds As New DataSet
                ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, "sp_report_ds2_SummaryByMonth", _
                New SqlParameter("@Month", ddlMonth.SelectedValue), _
                New SqlParameter("@Year", ddlYear.SelectedValue))

                Return ds.Tables(0)

            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End Function

        Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
            GridData.DataSource = LoadData()
            GridData.DataBind()
            GridData.MasterTableView.Caption = "<b>สรุปรายงานจำนวนการขอหนังสือรับรองถิ่นกำเนิดสินค้า " & "เดือน" & ddlMonth.Items(ddlMonth.SelectedIndex).Text & "  &nbsp;&nbsp;พ.ศ. " & ddlYear.Items(ddlYear.SelectedIndex).Text & "</b>"
            lblTitle.Text = "เดือน" & ddlMonth.Items(ddlMonth.SelectedIndex).Text & "  &nbsp;&nbsp;พ.ศ. " & ddlYear.Items(ddlYear.SelectedIndex).Text
            'Load chart.
            Dim ds As DataSet
            ds = LoadChartData()
            If ds.Tables.Count > 0 Then
                RadChart1.DataSource = ds.Tables(0)
                RadChart1.DataBind()
                If ds.Tables.Count > 1 Then
                    'Load chart2.
                    RadChart2.DataSource = ds.Tables(1)
                    RadChart2.DataBind()
                End If
            End If
        End Sub

        Private Sub GridData_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles GridData.NeedDataSource
            GridData.DataSource = LoadData()
        End Sub

        Private Sub GridData_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles GridData.ItemDataBound
            If TypeOf e.Item Is GridDataItem Then
                Dim viewLink As HyperLink = DirectCast(e.Item.FindControl("linkView"), HyperLink)
                viewLink.NavigateUrl = [String].Format(GetEditUrl(PortalId, "DS2COReportSummary", "ctlReportByForm") & "&yy=" & ddlYear.SelectedValue & "&mm=" & ddlMonth.SelectedValue & "&site=" & e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("site_id"))
            End If
        End Sub

        Private Function GetEditUrl(ByVal PortalID As Integer, ByVal ModuleFriendlyName As String, ByVal ControlName As String)
            Dim _ModuleController As DotNetNuke.Entities.Modules.ModuleController = New DotNetNuke.Entities.Modules.ModuleController()
            Dim _ModuleInfo As DotNetNuke.Entities.Modules.ModuleInfo = _ModuleController.GetModuleByDefinition(PortalID, ModuleFriendlyName)
            Dim _TabController As DotNetNuke.Entities.Tabs.TabController = New DotNetNuke.Entities.Tabs.TabController
            Dim _tab As DotNetNuke.Entities.Tabs.TabInfo = _TabController.GetTab(_ModuleInfo.TabID)
            Return [String].Format("http://" & DotNetNuke.Common.GetDomainName(Request) & "/Default.aspx?tabid=" & _ModuleInfo.TabID & "&ctl=" & ControlName & "&mid=" & _ModuleInfo.ModuleID)

            'Return _tab.TabPath.Replace("//", "/") & "/" & _tab.TabName & "/tabid/" & _ModuleInfo.TabID & "/ctl/Edit/mid/" & _ModuleInfo.ModuleID & "/Default.aspx"

        End Function

        Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnExport.Click
            GridData.GridLines = GridLines.Both
            GridData.MasterTableView.Columns(0).Visible = False
            GridData.MasterTableView.Columns(1).Visible = False
            GridData.ExportSettings.FileName = "Report_DS2_by_month"
            GridData.ExportSettings.ExportOnlyData = True
            GridData.ExportSettings.IgnorePaging = True
            GridData.ExportSettings.OpenInNewWindow = True
            GridData.MasterTableView.ExportToExcel()
        End Sub
    End Class

End Namespace
