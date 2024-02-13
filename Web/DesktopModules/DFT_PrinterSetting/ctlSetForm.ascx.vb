Imports System.Web.UI
Imports System.Collections.Generic
Imports System.Reflection

Imports DotNetNuke
Imports DotNetNuke.Services.Exceptions
Imports DotNetNuke.Services.Localization
Imports Telerik.Web.UI

Public Class ctlSetForm
    Inherits Entities.Modules.PortalModuleBase

    Private conn As String = ConfigurationManager.ConnectionStrings("EDIConnection").ConnectionString

    Private FormAction As String = ""
    Public PrinterID As String = "0"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Request.QueryString("act") Is Nothing Then
            FormAction = Request.QueryString("act")
        End If

        If Not Request.QueryString("printer_id") Is Nothing Then
            PrinterID = Request.QueryString("printer_id")
        End If

        btnNew.Attributes.Add("onclick", "OpenSetPrinterFormtype(" & PrinterID & ");")

        If Not Page.IsPostBack Then
            LoadSite()
            LoadForm(PrinterID)
            LoadPrinterFormType(PrinterID)

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

    Private Sub LoadForm(printer_id)
        ddlSite.Enabled = False
        Try
            Dim cmd As String = "SP_Printer_LoadPrinterByID"
            Dim prm As New SqlClient.SqlParameter("@printer_id", printer_id)

            Dim ds As DataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(conn, CommandType.StoredProcedure, cmd, prm)

            If ds.Tables(0).Rows.Count > 0 Then
                Dim dr As DataRow = ds.Tables(0).Rows(0)
                ddlSite.SelectedValue = dr.Item("site_id")
                txtPrinterName.Text = dr.Item("printer_name")
                txtDesc.Text = dr.Item("description")
                chkActive.Checked = dr.Item("active_flag")
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub LoadPrinterFormType(printer_id As String)
        Try
            Dim cmd As String = "SP_Printer_LoadFormTypeByPrinterID"
            Dim prm As New SqlClient.SqlParameter("@printer_id", printer_id)

            Dim ds As DataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(conn, CommandType.StoredProcedure, cmd, prm)

            grdForms.DataSource = ds
            grdForms.DataBind()

        Catch ex As Exception

        End Try
    End Sub

    Private Sub RadAjaxManager1_AjaxRequest(sender As Object, e As AjaxRequestEventArgs) Handles RadAjaxManager1.AjaxRequest
        LoadPrinterFormType(PrinterID)
    End Sub

    Public Sub ibtnDelete_Click(sender As Object, e As ImageClickEventArgs)
        Dim del_id As String = CType(sender, ImageButton).CommandArgument
        Try
            Dim cmd As String = "delete from Printer_FormType where printer_formtype_id = " & del_id
            Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(conn, CommandType.Text, cmd)

            LoadPrinterFormType(PrinterID)

        Catch ex As Exception

        End Try
    End Sub
End Class