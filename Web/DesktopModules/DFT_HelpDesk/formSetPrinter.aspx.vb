Public Class formSetPrinter
    Inherits System.Web.UI.Page

    Private conn As String = ConfigurationManager.ConnectionStrings("EDIConnection").ConnectionString

    Private FormAction As String = ""
    Private PrinterID As String = "0"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        '    If Not Request.QueryString("act") Is Nothing Then
        '        FormAction = Request.QueryString("act")
        '    End If

        '    If Not Request.QueryString("printer_id") Is Nothing Then
        '        PrinterID = Request.QueryString("printer_id")
        '    End If

        '    If Not Page.IsPostBack Then
        '        LoadSite()
        '        If ddlSite.Items.Count > 0 Then
        '            ddlSite.SelectedIndex = 0
        '        End If

        '        If FormAction = "edit" Then
        '            LoadForm(PrinterID)
        '        End If
        '    End If
        'End Sub

        'Private Sub LoadForm(printer_id)
        '    ddlSite.Enabled = False
        '    Try
        '        Dim cmd As String = "SP_Printer_LoadPrinterByID"
        '        Dim prm As New SqlClient.SqlParameter("@printer_id", printer_id)

        '        Dim ds As DataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(conn, CommandType.StoredProcedure, cmd, prm)

        '        If ds.Tables(0).Rows.Count > 0 Then
        '            Dim dr As DataRow = ds.Tables(0).Rows(0)
        '            ddlSite.SelectedValue = dr.Item("site_id")
        '            txtPrinterName.Text = dr.Item("printer_name")
        '            txtDesc.Text = dr.Item("description")
        '            chkActive.Checked = dr.Item("active_flag")
        '        End If

        '    Catch ex As Exception

        '    End Try
        'End Sub

        'Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        '    If FormAction = "new" Then
        '        If AddPrinter() Then
        '            Page.ClientScript.RegisterStartupScript(Page.GetType, "Saved", "CloseAndRebind();", True)
        '        End If
        '    Else
        '        If EditPrinter(PrinterID) Then
        '            Page.ClientScript.RegisterStartupScript(Page.GetType, "Edited", "CloseAndRebind();", True)
        '        End If
        '    End If
        'End Sub

        'Private Sub LoadSite()
        '    Try
        '        Dim cmd As String = "select site_id, site_name from site_plus where (active_status = 'Y') order by site_name"
        '        Dim ds As DataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(conn, CommandType.Text, cmd)

        '        ddlSite.DataSource = ds
        '        ddlSite.DataValueField = "site_id"
        '        ddlSite.DataTextField = "site_name"
        '        ddlSite.DataBind()

        '    Catch ex As Exception

        '    End Try
        'End Sub

        'Private Function AddPrinter() As Boolean
        '    Dim ret As Boolean = False
        '    Try
        '        Dim cmd As String = "SP_Printer_AddPrinter"

        '        Dim prm(3) As SqlClient.SqlParameter
        '        prm(0) = New SqlClient.SqlParameter("@site_id", ddlSite.SelectedValue)
        '        prm(1) = New SqlClient.SqlParameter("@printer_name", txtPrinterName.Text.Trim)
        '        prm(2) = New SqlClient.SqlParameter("@description", txtDesc.Text.Trim)
        '        prm(3) = New SqlClient.SqlParameter("@active_flag", chkActive.Checked)

        '        Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(conn, CommandType.StoredProcedure, cmd, prm)

        '        ret = True
        '    Catch ex As Exception

        '    End Try
        '    Return ret
        'End Function

        'Private Function EditPrinter(printer_id As String) As Boolean
        '    Dim ret As Boolean = False
        '    Try
        '        Dim cmd As String = "SP_Printer_EditPrinter"

        '        Dim prm(4) As SqlClient.SqlParameter
        '        prm(0) = New SqlClient.SqlParameter("@site_id", ddlSite.SelectedValue)
        '        prm(1) = New SqlClient.SqlParameter("@printer_name", txtPrinterName.Text.Trim)
        '        prm(2) = New SqlClient.SqlParameter("@description", txtDesc.Text.Trim)
        '        prm(3) = New SqlClient.SqlParameter("@active_flag", chkActive.Checked)
        '        prm(4) = New SqlClient.SqlParameter("@printer_id", printer_id)

        '        Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(conn, CommandType.StoredProcedure, cmd, prm)

        '        ret = True
        '    Catch ex As Exception

        '    End Try
        '    Return ret
        'End Function
    End Sub
End Class