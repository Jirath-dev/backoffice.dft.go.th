Imports System.Web.UI
Imports System.Collections.Generic
Imports System.Reflection

Imports DotNetNuke
Imports DotNetNuke.Services.Exceptions
Imports DotNetNuke.Services.Localization

Imports Microsoft.ApplicationBlocks.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI

Namespace YourCompany.Modules.DFT_Report_Form_ByPro

    Partial Class ViewDFT_Report_Form_ByPro
        Inherits Entities.Modules.PortalModuleBase
        Dim strEDIConn As String = ConfigurationManager.ConnectionStrings("OriginConnection").ConnectionString
        Dim strTradingConn As String = ConfigurationManager.ConnectionStrings("tmpCardConn").ConnectionString

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            If Not Page.IsPostBack Then
                rdpFromDate.SelectedDate = Today
                rdpToDate.SelectedDate = Today
                LoadAllFormType()
                LoadAllSite()
            End If
        End Sub

        Sub LoadAllFormType()
            Try
                Dim Strcommand As String
                Dim ds As New DataSet
                ddlSelectForm.Items.Clear()

                If rblSystem.SelectedValue = 0 Then
                    Strcommand = " SELECT form_type, form_name FROM form_type WHERE (ShowForm = 1) ORDER BY form_type"
                    ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.Text, Strcommand)
                Else
                    Strcommand = " SELECT form_type, form_name FROM P_Form_type  WHERE (IsDisplay = 1) ORDER BY form_type"
                    ds = SqlHelper.ExecuteDataset(strTradingConn, CommandType.Text, Strcommand)
                End If

                If ds.Tables(0).Rows.Count > 0 Then
                    ddlSelectForm.DataSource = ds
                    ddlSelectForm.DataTextField = "form_name"
                    ddlSelectForm.DataValueField = "form_type"
                    ddlSelectForm.DataBind()
                End If
                ddlSelectForm.Items.Insert(0, New RadComboBoxItem("ทุกฟอร์ม", "ALL"))
                ddlSelectForm.SelectedIndex = 0
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End Sub

        Sub LoadAllSite()
            Try
                Dim Strcommand, TempTable, Strconn As String

                ddlSiteID.Items.Clear()

                If rblSystem.SelectedValue = 0 Then
                    TempTable = "site_plus"
                    Strconn = strEDIConn
                Else
                    TempTable = "P_site_plus"
                    Strconn = strTradingConn
                End If

                Strcommand = " SELECT site_id, site_name FROM " & TempTable & " WHERE(active_status = 'Y') ORDER BY site_id "
                Dim DS As DataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(Strconn, CommandType.Text, Strcommand)
                ddlSiteID.DataSource = DS
                ddlSiteID.DataTextField = "site_name"
                ddlSiteID.DataValueField = "site_id"
                ddlSiteID.DataBind()

                ddlSiteID.SelectedIndex = 0
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End Sub

        Function LoadData() As DataTable
            Try
                Dim npm(5) As SqlParameter
                npm(1) = New SqlParameter("@FROM_DATE", rdpFromDate.SelectedDate.Value.ToString("yyyyMMdd"))
                npm(2) = New SqlParameter("@TO_DATE", rdpToDate.SelectedDate.Value.ToString("yyyyMMdd"))
                npm(3) = New SqlParameter("@Site_ID", ddlSiteID.SelectedValue)
                npm(4) = New SqlParameter("@Form_Type", ddlSelectForm.SelectedValue)
                npm(5) = New SqlParameter("@edi_status_id", rdoStatus.SelectedValue)

                If CheckAllSite.Checked = True And ddlSelectForm.SelectedValue = "ALL" And rblReportSystem.SelectedValue = 1 Then
                    npm(0) = New SqlParameter("@TCAT", 0)
                ElseIf CheckAllSite.Checked = False And ddlSelectForm.SelectedValue = "ALL" And rblReportSystem.SelectedValue = 1 Then
                    npm(0) = New SqlParameter("@TCAT", 1)
                ElseIf CheckAllSite.Checked = True And ddlSelectForm.SelectedValue <> "ALL" And rblReportSystem.SelectedValue = 1 Then
                    npm(0) = New SqlParameter("@TCAT", 2)
                ElseIf CheckAllSite.Checked = False And ddlSelectForm.SelectedValue <> "ALL" And rblReportSystem.SelectedValue = 1 Then
                    npm(0) = New SqlParameter("@TCAT", 3)
                ElseIf CheckAllSite.Checked = True And ddlSelectForm.SelectedValue = "ALL" And rblReportSystem.SelectedValue = 0 Then
                    npm(0) = New SqlParameter("@TCAT", 4)
                ElseIf CheckAllSite.Checked = False And ddlSelectForm.SelectedValue = "ALL" And rblReportSystem.SelectedValue = 0 Then
                    npm(0) = New SqlParameter("@TCAT", 1)
                ElseIf CheckAllSite.Checked = True And ddlSelectForm.SelectedValue <> "ALL" And rblReportSystem.SelectedValue = 0 Then
                    npm(0) = New SqlParameter("@TCAT", 5)
                ElseIf CheckAllSite.Checked = False And ddlSelectForm.SelectedValue <> "ALL" And rblReportSystem.SelectedValue = 0 Then
                    npm(0) = New SqlParameter("@TCAT", 3)
                End If

                Dim ds As New DataSet
                Select Case rblSystem.SelectedValue
                    Case 0
                        ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, "SP_REPORT_Form_ByPro_EDI_v2", npm)
                    Case 1
                        ds = SqlHelper.ExecuteDataset(strTradingConn, CommandType.StoredProcedure, "SP_REPORT_Form_ByPro_Trading", npm)
                End Select

                Return ds.Tables(0)
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End Function

        Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
            If CheckAllSite.Checked = False And ddlSiteID.SelectedItem Is Nothing Then
                RadAjaxManager1.ResponseScripts.Add("window.alert('กรุณาเลือกสถานที่!!!');")
            Else
                If rblReportSystem.SelectedValue = 0 Then
                    rgEDI.Visible = True
                    rgEDIBySite.Visible = False
                    rgEDI.DataSource = LoadData()
                    rgEDI.DataBind()
                Else
                    rgEDI.Visible = False
                    rgEDIBySite.Visible = True
                    rgEDIBySite.DataSource = LoadData()
                    rgEDIBySite.DataBind()
                End If

            End If

            ''rgEDI.Rebind()

        End Sub

        Private Sub CheckAllSite_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckAllSite.CheckedChanged
            If CheckAllSite.Checked = True Then
                ddlSiteID.Items.Clear()
                ddlSiteID.Enabled = False
            Else
                ddlSiteID.Enabled = True
                LoadAllSite()
            End If
        End Sub

        Private Sub rgEDI_GridExporting(ByVal source As Object, ByVal e As Telerik.Web.UI.GridExportingArgs) Handles rgEDI.GridExporting
            If (e.ExportType = Telerik.Web.UI.ExportType.Excel) Then
                Dim Css As String = "<style> td { border:solid 0.1pt #000000; }</style>"
                e.ExportOutput = e.ExportOutput.Replace("</head>", Css + "</head>")
            End If
        End Sub

        Private Sub rgEDI_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgEDI.NeedDataSource
            rgEDI.DataSource = LoadData()
        End Sub

        Private Sub btnExcel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExcel.Click
            If rblReportSystem.SelectedValue = 0 Then
                If rblSystem.SelectedValue = 0 Then
                    rgEDI.MasterTableView.Caption = "รายงานการออกฟอร์ม ตั้งแต่วันที่  " & rdpFromDate.SelectedDate.Value.ToString("dd/MM/yyyy") & " ถึงวันที่  " & rdpToDate.SelectedDate.Value.ToString("dd/MM/yyyy")
                ElseIf rblSystem.SelectedValue = 1 Then
                    rgEDI.MasterTableView.Caption = "รายงานการออกใบอนุญาต ตั้งแต่วันที่  " & rdpFromDate.SelectedDate.Value.ToString("dd/MM/yyyy") & " ถึงวันที่  " & rdpToDate.SelectedDate.Value.ToString("dd/MM/yyyy")
                End If
                rgEDI.MasterTableView.GroupsDefaultExpanded = True
                rgEDI.ExportSettings.ExportOnlyData = True
                rgEDI.ExportSettings.IgnorePaging = True
                rgEDI.MasterTableView.ExportToExcel()
            Else
                If rblSystem.SelectedValue = 0 Then
                    rgEDIBySite.MasterTableView.Caption = "รายงานการออกฟอร์ม ตั้งแต่วันที่  " & rdpFromDate.SelectedDate.Value.ToString("dd/MM/yyyy") & " ถึงวันที่  " & rdpToDate.SelectedDate.Value.ToString("dd/MM/yyyy")
                ElseIf rblSystem.SelectedValue = 1 Then
                    rgEDIBySite.MasterTableView.Caption = "รายงานการออกใบอนุญาต ตั้งแต่วันที่  " & rdpFromDate.SelectedDate.Value.ToString("dd/MM/yyyy") & " ถึงวันที่  " & rdpToDate.SelectedDate.Value.ToString("dd/MM/yyyy")
                End If
                rgEDIBySite.MasterTableView.GroupsDefaultExpanded = True
                rgEDIBySite.ExportSettings.ExportOnlyData = True
                rgEDIBySite.ExportSettings.IgnorePaging = True
                rgEDIBySite.MasterTableView.ExportToExcel()
            End If
          
        End Sub

        Private Sub rblSystem_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rblSystem.SelectedIndexChanged
            LoadAllFormType()
            LoadAllSite()
        End Sub

        Private Sub rgEDIBySite_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgEDIBySite.NeedDataSource
            rgEDIBySite.DataSource = LoadData()
        End Sub
    End Class

End Namespace
