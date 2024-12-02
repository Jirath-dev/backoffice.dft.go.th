
Imports System.Web.UI
Imports System.Collections.Generic
Imports System.Reflection

Imports DotNetNuke
Imports DotNetNuke.Services.Exceptions
Imports DotNetNuke.Services.Localization
Imports Telerik.Web.UI

Namespace Modules.DFT_PrinterSetting

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' The ViewDFT_PrinterSetting class displays the content
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Partial Class ViewDFT_PrinterSetting
        Inherits Entities.Modules.PortalModuleBase

        Private conn As String = ConfigurationManager.ConnectionStrings("EDIConnection").ConnectionString

        Private Sub ViewDFT_PrinterSetting_Load(sender As Object, e As EventArgs) Handles Me.Load
            If Not Page.IsPostBack Then
                LoadSite()
                If ddlSite.Items.Count > 0 Then
                    ddlSite.SelectedIndex = 0
                End If
                LoadPrinters(ddlSite.SelectedValue)
            End If
            If Not Page.IsPostBack Then
                LoadIE()
                If ddlIE.Items.Count > 0 Then
                    ddlIE.SelectedIndex = 0
                End If
                LoadLog(ddlIE.SelectedValue)
            End If
        End Sub

        Private Sub LoadSite()
            Try
                Dim cmd As String = "select site_id, site_name from site_plus where (active_status = 'Y') order by site_name"
                Dim ds As DataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(conn, CommandType.Text, cmd)

                ddlSite.DataSource = ds
                ddlSite.DataValueField = "site_id"
                ddlSite.DataTextField = "site_name"
                ddlSite.DataBind()

            Catch ex As Exception

            End Try
        End Sub
        Private Sub LoadIE()
            Try
                Dim cmd As String = "select Information_system, Information_name from Information where (active = '1') "
                Dim ds As DataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(conn, CommandType.Text, cmd)

                ddlIE.DataSource = ds
                ddlIE.DataValueField = "Information_system"
                ddlIE.DataTextField = "Information_name"
                ddlIE.DataBind()

            Catch ex As Exception

            End Try
        End Sub
        Private Sub LoadPrinters(site_id As String)
            Try
                Dim cmd As String = "SP_Printer_LoadPrinterBySite"
                Dim prm As New SqlClient.SqlParameter("@site_id", site_id)

                Dim ds As DataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(conn, CommandType.StoredProcedure, cmd, prm)

                grdPrinters.DataSource = ds
                grdPrinters.DataBind()

            Catch ex As Exception

            End Try
        End Sub
        Private Sub LoadLog(Information_system As String)
            Try
                Dim cmd As String = "SP_Information_LoadLog"
                Dim prm As New SqlClient.SqlParameter("@Information_system", Information_system)

                Dim ds As DataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(conn, CommandType.StoredProcedure, cmd, prm)

                grdlog.DataSource = ds
                grdlog.DataBind()

            Catch ex As Exception

            End Try
        End Sub

        Private Sub ddlSite_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSite.SelectedIndexChanged
            LoadPrinters(ddlSite.SelectedValue)
        End Sub

        Private Sub RadAjaxManager1_AjaxRequest(sender As Object, e As AjaxRequestEventArgs) Handles RadAjaxManager1.AjaxRequest
            LoadPrinters(ddlSite.SelectedValue)
        End Sub
        Private Sub ddlIE_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlIE.SelectedIndexChanged
            LoadLog(ddlIE.SelectedValue)
        End Sub

    End Class

End Namespace
