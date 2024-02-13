Imports System.Web.UI
Imports System.Collections.Generic
Imports System.Reflection

Imports DotNetNuke
Imports DotNetNuke.Services.Exceptions
Imports DotNetNuke.Services.Localization

Imports Microsoft.ApplicationBlocks.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI


Namespace YourCompany.Modules.DFT_ReportEDIAll

 
    Partial Class ViewDFT_ReportEDIAll
        Inherits Entities.Modules.PortalModuleBase
        Implements Entities.Modules.IActionable
        Dim strConn As String = ConfigurationManager.ConnectionStrings("OriginConnection").ConnectionString
        Dim objConn As SqlConnection = Nothing

        Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            If Not Page.IsPostBack Then
                rdpFromDate.SelectedDate = Today
                rdpToDate.SelectedDate = Today
                LoadExportCompany()
                LoadAllFormType()
            End If
        End Sub

        Private Sub rgEDI_GridExporting(ByVal source As Object, ByVal e As Telerik.Web.UI.GridExportingArgs) Handles rgEDI.GridExporting
            If (e.ExportType = Telerik.Web.UI.ExportType.Excel) Then
                Dim Css As String = "<style> td { border:solid 0.1pt #000000; }</style>"
                e.ExportOutput = e.ExportOutput.Replace("</head>", Css + "</head>")
            End If
        End Sub

        Private Sub rgEDI_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgEDI.NeedDataSource
            rgEDI.DataSource = LoadTrans()
        End Sub

        Function LoadTrans() As DataTable
            Try
                Dim Strcommand As String = ""
                Dim TCAT As String = ""

                If CheckAllSite.Checked = True Then ''∑ÿ° “¢“
                    If rdbSelectSys.SelectedValue = "0" Then
                        ''∑ÿ°√–∫∫ ByTine 19-05-2558
                        TCAT = "6"
                        Strcommand = " SELECT SUM(Form_Count) AS Form_Count, form_name, site_name, site_id, SUM(fob_amt) AS fob_amt, SUM(totalPrintPage) AS totalPrintPage , " & _
                        " CASE WHEN SentBy = '0' THEN '√–∫∫ª°µ‘' WHEN SentBy = '1' THEN '√–∫∫Digital (ºË“π‡«Á∫)' WHEN SentBy = '2' THEN '√–∫∫Digital (XML)' " & _
                        " WHEN SentBy = '3' THEN 'Digital & SealSign (ºË“π‡«Á∫)' WHEN SentBy = '4' THEN 'Digital & SealSign (XML)' END AS SentBy " & _
                        " FROM dbo.REPORT_EDIByCountryForm " & _
                        " WHERE (approve_date BETWEEN CONVERT(VARCHAR, @FROM_DATE, 112) AND CONVERT(VARCHAR, @TO_DATE, 112)) "

                    ElseIf rdbSelectSys.SelectedValue = "1" Then
                        ''√–∫∫ª°µ‘
                        TCAT = "5"
                        Strcommand = " SELECT SUM(Form_Count) AS Form_Count, form_name, site_name, site_id, SUM(fob_amt) AS fob_amt, SUM(totalPrintPage) AS totalPrintPage , " & _
                        " CASE WHEN SentBy = '0' THEN '√–∫∫ª°µ‘' END AS SentBy FROM dbo.REPORT_EDIByCountryForm " & _
                        " WHERE (approve_date BETWEEN CONVERT(VARCHAR, @FROM_DATE, 112) AND CONVERT(VARCHAR, @TO_DATE, 112))" & _
                        " GROUP BY form_name, site_name, site_id, SentBy " & _
                        " HAVING (SentBy = 0) "

                    ElseIf rdbSelectSys.SelectedValue = "2" Then
                        ''√–∫∫ Digital
                        TCAT = "3"
                        Strcommand = " SELECT SUM(Form_Count) AS Form_Count, form_name, site_name, site_id, SUM(fob_amt) AS fob_amt, SUM(totalPrintPage) AS totalPrintPage , " & _
                        " CASE WHEN SentBy = '1' THEN '√–∫∫Digital (ºË“π‡«Á∫)' WHEN SentBy = '2' THEN '√–∫∫Digital (XML)' END AS SentBy " & _
                        " FROM dbo.REPORT_EDIByCountryForm " & _
                        " WHERE (approve_date BETWEEN CONVERT(VARCHAR, @FROM_DATE, 112) AND CONVERT(VARCHAR, @TO_DATE, 112)) " & _
                        " GROUP BY form_name, site_name, site_id, SentBy " & _
                        " HAVING (SentBy = 1 OR SentBy = 2) "

                    ElseIf rdbSelectSys.SelectedValue = "3" Then
                        ''√–∫∫Digital & SealSign
                        TCAT = "4"
                        Strcommand = " SELECT SUM(Form_Count) AS Form_Count, form_name, site_name, site_id, SUM(fob_amt) AS fob_amt, SUM(totalPrintPage) AS totalPrintPage , " & _
                        " CASE WHEN SentBy = '3' THEN 'Digital & SealSign (ºË“π‡«Á∫)' WHEN SentBy = '4' THEN 'Digital & SealSign (XML)' END AS SentBy " & _
                        " FROM dbo.REPORT_EDIByCountryForm " & _
                        " WHERE (approve_date BETWEEN CONVERT(VARCHAR, @FROM_DATE, 112) AND CONVERT(VARCHAR, @TO_DATE, 112)) " & _
                        " GROUP BY form_name, site_name, site_id, SentBy " & _
                        " HAVING (SentBy = 3 OR SentBy = 4)"
                    End If

                Else ''‡≈◊Õ° “¢“
                    If rdbSelectSys.SelectedValue = "0" Then
                        ''∑ÿ°√–∫∫ ByTine 19-05-2558
                        TCAT = "7"
                        Strcommand = " SELECT SUM(Form_Count) AS Form_Count, form_name, site_name, site_id, SUM(fob_amt) AS fob_amt, SUM(totalPrintPage) AS totalPrintPage , " & _
                         " CASE WHEN SentBy = '0' THEN '√–∫∫ª°µ‘' WHEN SentBy = '1' THEN '√–∫∫Digital (ºË“π‡«Á∫)' WHEN SentBy = '2' THEN '√–∫∫Digital (XML)' " & _
                         " WHEN SentBy = '3' THEN '√–∫∫Digital & SealSign (ºË“π‡«Á∫)' WHEN SentBy = '4' THEN '√–∫∫Digital & SealSign (XML)' END AS SentBy " & _
                         " FROM dbo.REPORT_EDIByCountryForm " & _
                         " WHERE (approve_date BETWEEN CONVERT(VARCHAR, @FROM_DATE, 112) AND CONVERT(VARCHAR, @TO_DATE, 112)) " & _
                         " GROUP BY form_name, site_name, site_id, SentBy " & _
                         " HAVING (site_id = @SiteID) "

                    ElseIf rdbSelectSys.SelectedValue = "1" Then
                        ''√–∫∫ª°µ‘
                        TCAT = "2"
                        Strcommand = " SELECT SUM(Form_Count) AS Form_Count, form_name, site_name, site_id, SUM(fob_amt) AS fob_amt, SUM(totalPrintPage) AS totalPrintPage , " & _
                        " CASE WHEN SentBy = '0' THEN '√–∫∫ª°µ‘' END AS SentBy " & _
                        " FROM dbo.REPORT_EDIByCountryForm " & _
                        " WHERE (approve_date BETWEEN CONVERT(VARCHAR, @FROM_DATE, 112) AND CONVERT(VARCHAR, @TO_DATE, 112)) " & _
                        " GROUP BY form_name, site_name, site_id, SentBy " & _
                        " HAVING (SentBy = 0) AND (site_id = @SiteID) "

                    ElseIf rdbSelectSys.SelectedValue = "2" Then
                        ''√–∫∫ Digital
                        TCAT = "0"
                        Strcommand = " SELECT SUM(Form_Count) AS Form_Count, form_name, site_name, site_id, SUM(fob_amt) AS fob_amt, SUM(totalPrintPage) AS totalPrintPage , " & _
                        " CASE WHEN SentBy = '1' THEN '√–∫∫Digital (ºË“π‡«Á∫)' WHEN SentBy = '2' THEN '√–∫∫Digital (XML)' END AS SentBy " & _
                        " FROM dbo.REPORT_EDIByCountryForm " & _
                        " WHERE (approve_date BETWEEN CONVERT(VARCHAR, @FROM_DATE, 112) AND CONVERT(VARCHAR, @TO_DATE, 112)) " & _
                        " GROUP BY form_name, site_name, site_id, SentBy " & _
                        " HAVING (SentBy = 1 OR SentBy = 2) AND (site_id = @SiteID) "

                    ElseIf rdbSelectSys.SelectedValue = "3" Then
                        ''√–∫∫Digital & SealSign
                        TCAT = "1"
                        Strcommand = " SELECT SUM(Form_Count) AS Form_Count, form_name, site_name, site_id, SUM(fob_amt) AS fob_amt, SUM(totalPrintPage) AS totalPrintPage , " & _
                        " CASE WHEN SentBy = '3' THEN '√–∫∫Digital & SealSign (ºË“π‡«Á∫)' WHEN SentBy = '4' THEN '√–∫∫Digital & SealSign (XML)' END AS SentBy " & _
                        " FROM dbo.REPORT_EDIByCountryForm " & _
                        " WHERE (approve_date BETWEEN CONVERT(VARCHAR, @FROM_DATE, 112) AND CONVERT(VARCHAR, @TO_DATE, 112)) " & _
                        " GROUP BY form_name, site_name, site_id, SentBy " & _
                        " HAVING (SentBy = 3 OR SentBy = 4) AND (site_id = @SiteID) "
                    End If
                End If

                If ddlSelectForm.SelectedValue.ToUpper <> "ALL" And TCAT = "6" Then
                    Strcommand = Strcommand & " AND (form_name = '" & ddlSelectForm.Text & "') GROUP BY form_name, site_name, site_id, SentBy ORDER BY site_name,form_name"
                ElseIf ddlSelectForm.SelectedValue.ToUpper = "ALL" And TCAT = "6" Then
                    Strcommand = Strcommand & " GROUP BY form_name, site_name, site_id, SentBy ORDER BY site_name,form_name "
                ElseIf ddlSelectForm.SelectedValue.ToUpper <> "ALL" Then
                    Strcommand = Strcommand & " AND (form_name = '" & ddlSelectForm.Text & "') ORDER BY site_name,form_name "
                ElseIf ddlSelectForm.SelectedValue.ToUpper = "ALL" Then
                    Strcommand = Strcommand & " ORDER BY site_name,form_name "
                End If

                Dim ds As New DataSet
                Dim npm(2) As SqlClient.SqlParameter
                npm(0) = New SqlClient.SqlParameter("@FROM_DATE", rdpFromDate.SelectedDate.Value.ToString("yyyyMMdd"))
                npm(1) = New SqlClient.SqlParameter("@TO_DATE", rdpToDate.SelectedDate.Value.ToString("yyyyMMdd"))
                npm(2) = New SqlClient.SqlParameter("@SiteID", ddlSiteID.SelectedValue)

                'ds = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, "SP_REPORT_EDI_ALL", npm)
                ds = SqlHelper.ExecuteDataset(strConn, CommandType.Text, Strcommand, npm)
                Return ds.Tables(0)
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End Function

        Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
            If CheckAllSite.Checked = False And ddlSiteID.SelectedItem Is Nothing Then
                RadAjaxManager1.ResponseScripts.Add("window.alert('°√ÿ≥“‡≈◊Õ° ∂“π∑’Ë !!!');")
            Else
                rgEDI.DataSource = LoadTrans()
                rgEDI.DataBind()
            End If
        End Sub

        Sub LoadExportCompany()
            Try
                ddlSiteID.Items.Clear()
                Dim comm As String = " SELECT site_id, site_name, center, active_status, type_site, site_code, ds_status, image_status " & _
                                     " FROM site_plus " & _
                                     " WHERE (active_status = 'Y') " & _
                                     " ORDER BY site_name "
                Dim DS As DataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(strConn, CommandType.Text, comm)
                ddlSiteID.DataSource = DS
                ddlSiteID.DataTextField = "site_name"
                ddlSiteID.DataValueField = "site_id"
                ddlSiteID.DataBind()
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End Sub

        Sub LoadAllFormType()
            Try
                ddlSelectForm.Items.Clear()
                Dim Strcommand As String = " SELECT form_type, form_name FROM form_type "
                Dim ds As New DataSet
                ds = SqlHelper.ExecuteDataset(strConn, CommandType.Text, Strcommand)
                If ds.Tables(0).Rows.Count > 0 Then
                    ddlSelectForm.DataSource = ds
                    ddlSelectForm.DataTextField = "form_name"
                    ddlSelectForm.DataValueField = "form_type"
                    ddlSelectForm.DataBind()
                End If
                ddlSelectForm.SelectedIndex = 0
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End Sub

        Protected Sub btnExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcel.Click
            rgEDI.MasterTableView.GroupsDefaultExpanded = True
            rgEDI.ExportSettings.ExportOnlyData = True
            rgEDI.ExportSettings.IgnorePaging = True
            rgEDI.MasterTableView.ExportToExcel()
        End Sub

#Region "Optional Interfaces"

        Public ReadOnly Property ModuleActions() As Entities.Modules.Actions.ModuleActionCollection Implements Entities.Modules.IActionable.ModuleActions
            Get
                Dim Actions As New Entities.Modules.Actions.ModuleActionCollection
                Actions.Add(GetNextActionID, Localization.GetString(Entities.Modules.Actions.ModuleActionType.AddContent, LocalResourceFile), Entities.Modules.Actions.ModuleActionType.AddContent, "", "", EditUrl(), False, Security.SecurityAccessLevel.Edit, True, False)
                Return Actions
            End Get
        End Property

#End Region

        Private Sub CheckAllSite_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckAllSite.CheckedChanged
            If CheckAllSite.Checked = True Then
                ddlSiteID.Items.Clear()
                ddlSiteID.Enabled = False
            Else
                ddlSiteID.Enabled = True
                LoadExportCompany()
            End If
        End Sub

    End Class

End Namespace
