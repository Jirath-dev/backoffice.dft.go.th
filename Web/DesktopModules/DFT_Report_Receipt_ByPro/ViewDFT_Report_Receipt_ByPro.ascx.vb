Imports System.Web.UI
Imports System.Collections.Generic
Imports System.Reflection

Imports DotNetNuke
Imports DotNetNuke.Services.Exceptions
Imports DotNetNuke.Services.Localization

Imports Microsoft.ApplicationBlocks.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI

Namespace YourCompany.Modules.DFT_Report_Receipt_ByPro

    Partial Class ViewDFT_Report_Receipt_ByPro
        Inherits Entities.Modules.PortalModuleBase
        Dim strEDIConn As String = ConfigurationManager.ConnectionStrings("OriginConnection").ConnectionString
        Dim strTradingConn As String = ConfigurationManager.ConnectionStrings("tmpCardConn").ConnectionString
        Dim StrRegBackConn As String = ConfigurationManager.ConnectionStrings("RegisBackConnection").ConnectionString

        Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            If Not Page.IsPostBack Then
                rdpFromDate.SelectedDate = Today
                rdpToDate.SelectedDate = Today
                LoadAllSite()
            End If
        End Sub

        Sub LoadAllSite()
            Try
                Dim Strcommand, TempTable, Strconn As String

                ddlSiteID.Items.Clear()

                If rblSystem.SelectedValue = 0 Then
                    TempTable = "site_plus"
                    Strconn = strEDIConn
                ElseIf rblSystem.SelectedValue = 1 Then
                    TempTable = "P_site_plus"
                    Strconn = strTradingConn
                Else
                    TempTable = "P_Site_Plus"
                    Strconn = StrRegBackConn
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

        Private Sub rblSystem_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rblSystem.SelectedIndexChanged
            LoadAllSite()

            If rblSystem.SelectedValue = 2 Then
                rblReceiptType.SelectedValue = 2
                rblReceiptType.Enabled = False
            ElseIf rblSystem.SelectedValue = 1 Then
                rblReceiptType.SelectedValue = 1
                rblReceiptType.Enabled = False
            Else
                rblReceiptType.SelectedValue = 0
                rblReceiptType.Enabled = True
            End If
        End Sub

        Private Sub CheckAllSite_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckAllSite.CheckedChanged
            If CheckAllSite.Checked = True Then
                ddlSiteID.Enabled = False
            Else
                ddlSiteID.Enabled = True
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
            If rgEDI.MasterTableView.Items.Count > 0 Then
                rgEDI.MasterTableView.GroupsDefaultExpanded = True
                rgEDI.ExportSettings.ExportOnlyData = True
                rgEDI.ExportSettings.IgnorePaging = True
                rgEDI.MasterTableView.ExportToExcel()
            End If
        End Sub

        Function LoadData() As DataTable
            Try
                Dim npm(3) As SqlParameter
                npm(0) = New SqlParameter("@TCAT", _CheckAllCase)
                npm(1) = New SqlParameter("@FROM_DATE", rdpFromDate.SelectedDate.Value.ToString("yyyyMMdd"))
                npm(2) = New SqlParameter("@TO_DATE", rdpToDate.SelectedDate.Value.ToString("yyyyMMdd"))
                npm(3) = New SqlParameter("@Site_ID", ddlSiteID.SelectedValue)

                Dim ds As New DataSet
                Select Case rblSystem.SelectedValue
                    Case 0
                        ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, "SP_REPORT_Receipt_ByPro_EDI", npm)
                    Case 1
                        ds = SqlHelper.ExecuteDataset(strTradingConn, CommandType.StoredProcedure, "SP_REPORT_Receipt_ByPro_Trading", npm)
                    Case 2
                        ds = SqlHelper.ExecuteDataset(StrRegBackConn, CommandType.StoredProcedure, "SP_REPORT_Receipt_ByPro_Reg", npm)
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
                rgEDI.DataSource = LoadData()
                rgEDI.DataBind()
            End If
        End Sub

        Function _CheckAllCase() As Integer
            Try
                Dim Result As Integer = Nothing

                ''=== หนังสือรับรอง   ===================================
                '' หนังสือรับรอง ทุกสาขา ใบเสร็จทุกแบบ
                If rblSystem.SelectedValue = 0 And CheckAllSite.Checked = True And rblReceiptType.SelectedValue = 0 Then
                    Result = 0

                    '' หนังสือรับรอง ทุกสาขา ใบเสร็จเขียว
                ElseIf rblSystem.SelectedValue = 0 And CheckAllSite.Checked = True And rblReceiptType.SelectedValue = 1 Then
                    Result = 1

                    '' หนังสือรับรอง ทุกสาขา ใบเสร็จเหลือง
                ElseIf rblSystem.SelectedValue = 0 And CheckAllSite.Checked = True And rblReceiptType.SelectedValue = 2 Then
                    Result = 2

                    '' หนังสือรับรอง เลือกสาขา ใบเสร็จทุกแบบ
                ElseIf rblSystem.SelectedValue = 0 And CheckAllSite.Checked = False And rblReceiptType.SelectedValue = 0 Then
                    Result = 3

                    '' หนังสือรับรอง เลือกสาขา ใบเสร็จเขียว
                ElseIf rblSystem.SelectedValue = 0 And CheckAllSite.Checked = False And rblReceiptType.SelectedValue = 1 Then
                    Result = 4

                    '' หนังสือรับรอง เลือกสาขา ใบเสร็จเหลือง
                ElseIf rblSystem.SelectedValue = 0 And CheckAllSite.Checked = False And rblReceiptType.SelectedValue = 2 Then
                    Result = 5


                    ''=== ใบอนุญาต   ===================================
                    '' ใบอนุญาต ทุกสาขา ใบเสร็จเขียวและทั้งหมด (ฝั่งบัตรมีแต่ใบเสร็จเขียว)
                ElseIf rblSystem.SelectedValue = 1 And CheckAllSite.Checked = True And rblReceiptType.SelectedValue = 1 Then
                    Result = 0
                    '' ใบอนุญาต เลือกสาขา ใบเสร็จเขียวและทั้งหมด  (ฝั่งบัตรมีแต่ใบเสร็จเขียว)
                ElseIf rblSystem.SelectedValue = 1 And CheckAllSite.Checked = False And rblReceiptType.SelectedValue = 1 Then
                    Result = 1


                    ''=== ระบบบัตร/ขึ้นทะเบียน   =============================
                    '' ระบบบัตร/ขึ้นทะเบียน ทุกสาขา ใบเสร็จเหลืองและทั้งหมด (ฝั่งบัตรมีแต่ใบเสร็จเหลือง)
                ElseIf rblSystem.SelectedValue = 2 And CheckAllSite.Checked = True And rblReceiptType.SelectedValue = 2 Then
                    Result = 0
                    '' ระบบบัตร/ขึ้นทะเบียน เลือกสาขา ใบเสร็จเหลืองและทั้งหมด (ฝั่งบัตรมีแต่ใบเสร็จเหลือง)
                ElseIf rblSystem.SelectedValue = 2 And CheckAllSite.Checked = False And rblReceiptType.SelectedValue = 2 Then
                    Result = 1
                End If

                Return Result

            Catch ex As Exception
                Response.Write(ex.Message)
                Return Nothing
            End Try
        End Function
    End Class

End Namespace
