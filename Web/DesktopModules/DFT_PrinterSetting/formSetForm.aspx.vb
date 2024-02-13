Public Class formSetForm
    Inherits System.Web.UI.Page

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

        If Not Page.IsPostBack Then
            LoadForm()

        End If
    End Sub

    'Private Sub LoadPrinterFormtype(printer_id)
    '    Try
    '        Dim cmd As String = "SP_Printer_LoadPriinterByID"
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

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If AddForm(PrinterID) Then
            Page.ClientScript.RegisterStartupScript(Page.GetType, "Saved", "CloseAndRebind();", True)
        End If
    End Sub

    Private Sub LoadForm()
        Try
            Dim cmd As String = "select * from form_type t" ' where t.ShowForm = 1
            Dim ds As DataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(conn, CommandType.Text, cmd)

            ddlForm.DataSource = ds
            ddlForm.DataValueField = "form_type"
            ddlForm.DataTextField = "form_name"
            ddlForm.DataBind()

        Catch ex As Exception

        End Try
    End Sub

    Private Function AddForm(printer_id) As Boolean
        Dim ret As Boolean = False
        Try
            Dim cmd As String = "SP_Printer_AddPrinterFormType"

            Dim prm(2) As SqlClient.SqlParameter
            prm(0) = New SqlClient.SqlParameter("@printer_id", printer_id)
            prm(1) = New SqlClient.SqlParameter("@form_type", ddlForm.SelectedValue)
            prm(2) = New SqlClient.SqlParameter("@print_type", rdoPrintType.SelectedValue)

            Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(conn, CommandType.StoredProcedure, cmd, prm)

            ret = True
        Catch ex As Exception

        End Try
        Return ret
    End Function

End Class