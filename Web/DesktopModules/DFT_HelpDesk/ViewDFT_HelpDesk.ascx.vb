
Imports System.Web.UI
Imports System.Collections.Generic
Imports System.Reflection

Imports DotNetNuke
Imports DotNetNuke.Services.Exceptions
Imports DotNetNuke.Services.Localization
Imports Telerik.Web.UI

Namespace Modules.DFT_HelpDesk

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' The ViewDFT_HelpDesk class displays the content
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Partial Class ViewDFT_HelpDesk
        Inherits Entities.Modules.PortalModuleBase

        Private conn As String = ConfigurationManager.ConnectionStrings("EDIConnection").ConnectionString

        Private Sub ViewDFT_HelpDesk_Load(sender As Object, e As EventArgs) Handles Me.Load
            If Not Page.IsPostBack Then
                LoadSite()
                If ddlSite.Items.Count > 0 Then
                    ddlSite.SelectedIndex = 0
                End If
                LoadPrinters(ddlSite.SelectedValue)
            End If
        End Sub

        Private Sub LoadSite()
            Try
                Dim cmd As String = "select Information_system, Information_name from Information where (active = '1') "
                Dim ds As DataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(conn, CommandType.Text, cmd)

                ddlSite.DataSource = ds
                ddlSite.DataValueField = "Information_system"
                ddlSite.DataTextField = "Information_name"
                ddlSite.DataBind()

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

        Private Sub ddlSite_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSite.SelectedIndexChanged
            LoadPrinters(ddlSite.SelectedValue)
        End Sub

        Private Sub RadAjaxManager1_AjaxRequest(sender As Object, e As AjaxRequestEventArgs) Handles RadAjaxManager1.AjaxRequest
            LoadPrinters(ddlSite.SelectedValue)
        End Sub

    End Class

End Namespace
