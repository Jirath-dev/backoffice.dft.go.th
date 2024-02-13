

Imports System.Web.UI
Imports System.Collections.Generic
Imports System.Reflection

Imports DotNetNuke
Imports DotNetNuke.Services.Exceptions
Imports DotNetNuke.Services.Localization
Imports System.Data
Imports system.data.sqlclient
Imports Microsoft.ApplicationBlocks.Data
Imports Telerik.Web.UI

Namespace YourCompany.Modules.DFT_ReportEDIByCountryForm

    Partial Class ViewDFT_ReportEDIByCountryForm
        Inherits Entities.Modules.PortalModuleBase
        Implements Entities.Modules.IActionable
        Dim StrConnEDI As String = ConfigurationManager.ConnectionStrings("EDIConnection").ConnectionString

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            If Not Page.IsPostBack Then
                rdpFromDate.SelectedDate = Now
                rdpToDate.SelectedDate = Now
                LoadExportCompany()
                LoadAllCountry()
                LoadFormTypeForSearching()
                dropFormType.SelectedValue = "ALL"
            End If
        End Sub

#Region "Optional Interfaces"

        ''' -----------------------------------------------------------------------------
        ''' <summary>
        ''' Registers the module actions required for interfacing with the portal framework
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' </history>
        ''' -----------------------------------------------------------------------------
        Public ReadOnly Property ModuleActions() As Entities.Modules.Actions.ModuleActionCollection Implements Entities.Modules.IActionable.ModuleActions
            Get
                Dim Actions As New Entities.Modules.Actions.ModuleActionCollection
                Actions.Add(GetNextActionID, Localization.GetString(Entities.Modules.Actions.ModuleActionType.AddContent, LocalResourceFile), Entities.Modules.Actions.ModuleActionType.AddContent, "", "", EditUrl(), False, Security.SecurityAccessLevel.Edit, True, False)
                Return Actions
            End Get
        End Property

#End Region

        Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
            If CheckAllSite.Checked = False And ddlSiteID.SelectedItem Is Nothing Then
                RadAjaxManager1.ResponseScripts.Add("window.alert('กรุณาเลือกสถานที่!!!');")
            Else
                rgEDI.DataSource = LoadDetail()
                rgEDI.DataBind()
            End If
        End Sub

        Function LoadDetail() As DataTable
            Try

                Dim Strcommand as String
                Dim TCAT As String = ""

                Dim form_name As String = dropFormType.SelectedItem.Text.Trim
                Dim form_name_cuase As String = " and (1=1) "
                If form_name <> "ทุกฟอร์ม" Then
                    form_name_cuase = " and form_name = '" & form_name & "' "
                End If


                If CheckAllSite.Checked = True Then ''ทุกสาขา
                    If rdbSelectSys.SelectedValue = "0" Then
                        ''ทุกระบบ ByTine 19-05-2558
                        TCAT = "6"
                        Strcommand = " SELECT SUM(Form_Count) AS Form_Count, country_name, form_name, site_name, site_id, SUM(fob_amt) as fob_amt, SUM(totalPrintPage) AS totalPrintPage, " &
                       " CASE WHEN SentBy = '0' THEN 'ระบบปกติ' WHEN SentBy = '1' THEN 'ระบบDigital (ผ่านเว็บ)' WHEN SentBy = '2' THEN 'ระบบDigital (XML)' " &
                       " WHEN SentBy = '3' THEN 'Digital & SealSign (ผ่านเว็บ)' WHEN SentBy = '4' THEN 'Digital & SealSign (XML)' END AS SentBy, country_code " &
                       " FROM dbo.REPORT_EDIByCountryForm " &
                       " WHERE (approve_date BETWEEN CONVERT(VARCHAR, @FROM_DATE, 112) AND CONVERT(VARCHAR, @TO_DATE, 112)) " & form_name_cuase


                    ElseIf rdbSelectSys.SelectedValue = "1" Then
                        'ระบบปกติ
                        Strcommand = " SELECT SUM(Form_Count) AS Form_Count, country_name, form_name, site_name, site_id, SUM(fob_amt) as fob_amt, SUM(totalPrintPage) AS totalPrintPage, " &
                       " CASE WHEN SentBy = '0' THEN 'ระบบปกติ' END as SentBy, country_code " &
                       " FROM dbo.REPORT_EDIByCountryForm " &
                       " WHERE (approve_date BETWEEN CONVERT(VARCHAR, @FROM_DATE, 112) AND CONVERT(VARCHAR, @TO_DATE, 112))" & form_name_cuase &
                       " GROUP BY country_name, form_name, site_name, site_id, SentBy, country_code " &
                       " HAVING (SentBy = 0) "
                    ElseIf rdbSelectSys.SelectedValue = "2" Then
                        ''ระบบ Digital
                        TCAT = "2"
                        Strcommand = " SELECT SUM(Form_Count) AS Form_Count, country_name, form_name, site_name, site_id, SUM(fob_amt) As fob_amt, SUM(totalPrintPage) AS totalPrintPage, " &
                       " CASE WHEN SentBy = '1' THEN 'ระบบDigital (ผ่านเว็บ)' WHEN SentBy = '2' THEN 'ระบบDigital (XML)' END AS SentBy, country_code " &
                       " FROM dbo.REPORT_EDIByCountryForm " &
                       " WHERE (approve_date BETWEEN CONVERT(VARCHAR, @FROM_DATE, 112) AND CONVERT(VARCHAR, @TO_DATE, 112)) " & form_name_cuase &
                       " GROUP BY country_name, form_name, site_name, site_id, SentBy, country_code " &
                       " HAVING (SentBy = 1 OR SentBy = 2) "
                    ElseIf rdbSelectSys.SelectedValue = "3" Then
                        ''ระบบDigital & SealSign
                        TCAT = "4"
                        Strcommand = " SELECT SUM(Form_Count) AS Form_Count, country_name, form_name, site_name, site_id, SUM(fob_amt) AS fob_amt, SUM(totalPrintPage) AS totalPrintPage, " &
                       " CASE WHEN SentBy = '3' THEN 'Digital & SealSign (ผ่านเว็บ)' WHEN SentBy = '4' THEN 'Digital & SealSign (XML)' END AS SentBy, country_code " &
                       " FROM dbo.REPORT_EDIByCountryForm " &
                       " WHERE (approve_date BETWEEN CONVERT(VARCHAR, @FROM_DATE, 112) AND CONVERT(VARCHAR, @TO_DATE, 112)) " & form_name_cuase &
                       " GROUP BY country_name, form_name, site_name, site_id, SentBy, country_code " &
                       " HAVING (SentBy = 3 OR SentBy = 4) "
                    End If

                Else ''เลือกสาขา

                    If rdbSelectSys.SelectedValue = "0" Then
                        ''ทุกระบบ ByTine 19-05-2558
                        TCAT = "7"
                        Strcommand = " SELECT SUM(Form_Count) AS Form_Count, country_name, form_name, site_name, site_id, SUM(fob_amt) as fob_amt, SUM(totalPrintPage) AS totalPrintPage, " &
                      " CASE WHEN SentBy = '0' THEN 'ระบบปกติ' WHEN SentBy = '1' THEN 'ระบบDigital (ผ่านเว็บ)' WHEN SentBy = '2' THEN 'ระบบDigital (XML)' " &
                      " WHEN SentBy = '3' THEN 'Digital & SealSign (ผ่านเว็บ)' WHEN SentBy = '4' THEN 'Digital & SealSign (XML)' END AS SentBy, country_code " &
                      " FROM dbo.REPORT_EDIByCountryForm " &
                      " WHERE (approve_date BETWEEN CONVERT(VARCHAR, @FROM_DATE, 112) AND CONVERT(VARCHAR, @TO_DATE, 112)) " & form_name_cuase &
                      " GROUP BY country_name, form_name, site_name, site_id, SentBy, country_code " &
                      " HAVING (site_id = @SiteID) "
                    ElseIf rdbSelectSys.SelectedValue = "1" Then
                        ''ระบบปกติ
                        TCAT = "1"
                        Strcommand = " SELECT SUM(Form_Count) AS Form_Count, country_name, form_name, site_name, site_id, SUM(fob_amt) as fob_amt, SUM(totalPrintPage) AS totalPrintPage, " &
                       " CASE WHEN SentBy = '0' THEN 'ระบบปกติ' END as SentBy, country_code " &
                       " FROM dbo.REPORT_EDIByCountryForm " &
                       " WHERE (approve_date BETWEEN CONVERT(VARCHAR, @FROM_DATE, 112) AND CONVERT(VARCHAR, @TO_DATE, 112)) " & form_name_cuase &
                       " GROUP BY country_name, form_name, site_name, site_id, SentBy, country_code " &
                       " HAVING (SentBy = 0) AND (site_id = @SiteID) "
                    ElseIf rdbSelectSys.SelectedValue = "2" Then
                        ''ระบบ Digital
                        TCAT = "3"
                        Strcommand = " SELECT SUM(Form_Count) AS Form_Count, country_name, form_name, site_name, site_id, SUM(fob_amt) As fob_amt, SUM(totalPrintPage) AS totalPrintPage, " &
                       " CASE WHEN SentBy = '1' THEN 'ระบบDigital (ผ่านเว็บ)' WHEN SentBy = '2' THEN 'ระบบDigital (XML)' END AS SentBy, country_code " &
                       " FROM dbo.REPORT_EDIByCountryForm " &
                       " WHERE (approve_date BETWEEN CONVERT(VARCHAR, @FROM_DATE, 112) AND CONVERT(VARCHAR, @TO_DATE, 112)) " & form_name_cuase &
                       " GROUP BY country_name, form_name, site_name, site_id, SentBy, country_code " &
                       " HAVING (SentBy = 1 OR SentBy = 2) AND (site_id = @SiteID) "
                    ElseIf rdbSelectSys.SelectedValue = "3" Then
                        ''ระบบDigital & SealSign
                        TCAT = "5"
                        Strcommand = " SELECT SUM(Form_Count) AS Form_Count, country_name, form_name, site_name, site_id, SUM(fob_amt) AS fob_amt, SUM(totalPrintPage) AS totalPrintPage, " &
                       " CASE WHEN SentBy = '3' THEN 'Digital & SealSign (ผ่านเว็บ)' WHEN SentBy = '4' THEN 'Digital & SealSign (XML)' END AS SentBy, country_code " &
                       " FROM dbo.REPORT_EDIByCountryForm " &
                       " WHERE (approve_date BETWEEN CONVERT(VARCHAR, @FROM_DATE, 112) AND CONVERT(VARCHAR, @TO_DATE, 112)) " & form_name_cuase &
                       " GROUP BY country_name, form_name, site_name, site_id, SentBy, country_code " &
                       " HAVING (SentBy = 3 OR SentBy = 4) AND (site_id = @SiteID) "
                    End If
                End If

                If ddlSelectCountry.SelectedValue <> "ทุกประเทศ" And TCAT = "6" Then
                    Strcommand = Strcommand & " GROUP BY country_code, country_name, form_name, site_name, site_id, SentBy HAVING (country_code = '" & ddlSelectCountry.SelectedValue & "') ORDER BY country_name, site_name "
                ElseIf ddlSelectCountry.SelectedValue = "ทุกประเทศ" And TCAT = "6" Then
                    Strcommand = Strcommand & " GROUP BY country_code, country_name, form_name, site_name, site_id, SentBy "
                ElseIf ddlSelectCountry.SelectedValue <> "ทุกประเทศ" Then
                    Strcommand = Strcommand & " AND (country_code = '" & ddlSelectCountry.SelectedValue & "') ORDER BY country_name, site_name "
                ElseIf ddlSelectCountry.SelectedValue = "ทุกประเทศ" Then
                    Strcommand = Strcommand & " ORDER BY country_name, site_name "
                End If

                Dim ds As New DataSet
                Dim npm(2) As SqlClient.SqlParameter
                npm(0) = New SqlClient.SqlParameter("@FROM_DATE", rdpFromDate.SelectedDate.Value.ToString("yyyyMMdd"))
                npm(1) = New SqlClient.SqlParameter("@TO_DATE", rdpToDate.SelectedDate.Value.ToString("yyyyMMdd"))
                npm(2) = New SqlClient.SqlParameter("@SiteID", ddlSiteID.SelectedValue)

                'ds = SqlHelper.ExecuteDataset(StrConnEDI, CommandType.StoredProcedure, "SP_REPORT_EDIByCountryForm", npm)
                ds = SqlHelper.ExecuteDataset(StrConnEDI, CommandType.Text, Strcommand, npm)
                Return ds.Tables(0)
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End Function

        Sub LoadExportCompany()
            Try
                ddlSiteID.Items.Clear()
                Dim comm As String = " SELECT site_id, site_name, center, active_status, type_site, site_code, ds_status, image_status " & _
                                     " FROM site_plus " & _
                                     " WHERE (active_status = 'Y') " & _
                                     " ORDER BY site_name "
                Dim DS As DataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(StrConnEDI, CommandType.Text, comm)
                ddlSiteID.DataSource = DS
                ddlSiteID.DataTextField = "site_name"
                ddlSiteID.DataValueField = "site_id"
                ddlSiteID.DataBind()
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End Sub

        Sub LoadAllCountry()
            Try
                ddlSelectCountry.Items.Clear()
                Dim Strcommand As String = " SELECT country_code, country_name FROM country GROUP BY country_code, country_name ORDER BY country_name "
                Dim ds As New DataSet
                ds = SqlHelper.ExecuteDataset(StrConnEDI, CommandType.Text, Strcommand)
                If ds.Tables(0).Rows.Count > 0 Then
                    ddlSelectCountry.DataSource = ds
                    ddlSelectCountry.DataTextField = "country_name"
                    ddlSelectCountry.DataValueField = "country_code"
                    ddlSelectCountry.DataBind()
                End If
                ddlSelectCountry.Items.Insert(0, New RadComboBoxItem("ทุกประเทศ", "ทุกประเทศ"))
                ddlSelectCountry.SelectedIndex = 0
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End Sub

        Private Sub LoadFormTypeForSearching()
            Try
                Dim ds As New DataSet
                Dim strCommand As String
                strCommand = "SELECT form_type AS CODE, form_nameUsd AS DESCRIPTION " &
                             "FROM form_type ORDER BY ShowOrder"
                ds = SqlHelper.ExecuteDataset(StrConnEDI, CommandType.Text, strCommand)

                If ds.Tables(0).Rows.Count > 0 Then
                    dropFormType.DataSource = ds.Tables(0)
                    dropFormType.DataTextField = "DESCRIPTION"
                    dropFormType.DataValueField = "CODE"
                    dropFormType.DataBind()
                End If
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

        Private Sub rgEDI_GridExporting(ByVal source As Object, ByVal e As Telerik.Web.UI.GridExportingArgs) Handles rgEDI.GridExporting
            If (e.ExportType = Telerik.Web.UI.ExportType.Excel) Then
                Dim Css As String = "<style> td { border:solid 0.1pt #000000; }</style>"
                e.ExportOutput = e.ExportOutput.Replace("</head>", Css + "</head>")
            End If
        End Sub

        Private Sub rgEDI_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgEDI.NeedDataSource
            rgEDI.DataSource = LoadDetail()
        End Sub

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
