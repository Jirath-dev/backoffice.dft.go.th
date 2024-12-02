Public Class formrequired
    Inherits System.Web.UI.Page

    Private conn As String = ConfigurationManager.ConnectionStrings("EDIConnection").ConnectionString

    Private FormAction As String = ""
    Private Information_id As String = "0"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Request.QueryString("act") Is Nothing Then
            FormAction = Request.QueryString("act")
        End If

        If Not Request.QueryString("Information_id") Is Nothing Then
            Information_id = Request.QueryString("Information_id")
        End If

        If Not Page.IsPostBack Then
            Loadrequired()
            If ddlIE.Items.Count > 0 Then
                ddlIE.SelectedIndex = 0
            End If

            If FormAction = "edit" Then
                LoadForm(Information_id)
            End If
        End If
    End Sub

    Private Sub LoadForm(Information_id)
        ddlIE.Enabled = False
        Try
            Dim cmd As String = "SP_Information_LoadByID"
            Dim prm As New SqlClient.SqlParameter("@Information_id", Information_id)

            Dim ds As DataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(conn, CommandType.StoredProcedure, cmd, prm)

            If ds.Tables(0).Rows.Count > 0 Then
                Dim dr As DataRow = ds.Tables(0).Rows(0)
                ddlIE.SelectedValue = dr.Item("Information_system")
                txtUser_case.Text = dr.Item("User_case")
                txtDesc.Text = dr.Item("description")
                chkActive.Checked = dr.Item("active_flag")
                txtdescription_fix.Text = dr.Item("description_fix")
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If FormAction = "new" Then
            If AddCase() Then
                Page.ClientScript.RegisterStartupScript(Page.GetType, "Saved", "CloseAndRebind();", True)
            End If
        Else
            If Editcase(Information_id) Then
                Page.ClientScript.RegisterStartupScript(Page.GetType, "Edited", "CloseAndRebind();", True)
            End If
        End If
    End Sub

    Private Sub Loadrequired()
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

    Private Function AddCase() As Boolean
        Dim ret As Boolean = False
        Try
            Dim cmd As String = "SP_Information_AddCase"

            Dim prm(5) As SqlClient.SqlParameter
            prm(0) = New SqlClient.SqlParameter("@Information_system", ddlIE.SelectedValue)
            prm(1) = New SqlClient.SqlParameter("@User_case", txtUser_case.Text.Trim)
            prm(2) = New SqlClient.SqlParameter("@description", txtDesc.Text.Trim)
            prm(3) = New SqlClient.SqlParameter("@active_flag", chkActive.Checked)
            prm(4) = New SqlClient.SqlParameter("@description_fix", txtdescription_fix.Text.Trim)
            Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(conn, CommandType.StoredProcedure, cmd, prm)

            ret = True
        Catch ex As Exception

        End Try
        Return ret
    End Function

    Private Function Editcase(Information_id As String) As Boolean
        Dim ret As Boolean = False
        Try
            Dim cmd As String = "SP_Information_Editlog"

            Dim prm(6) As SqlClient.SqlParameter
            prm(0) = New SqlClient.SqlParameter("@Information_system", ddlIE.SelectedValue)
            prm(1) = New SqlClient.SqlParameter("@User_case", txtUser_case.Text.Trim)
            prm(2) = New SqlClient.SqlParameter("@description", txtDesc.Text.Trim)
            prm(3) = New SqlClient.SqlParameter("@active_flag", chkActive.Checked)
            prm(4) = New SqlClient.SqlParameter("@Information_id", Information_id)
            prm(5) = New SqlClient.SqlParameter("@description_fix", txtdescription_fix.Text.Trim)
            prm(6) = New SqlClient.SqlParameter("@LastUpdatedBy", txtdescription_fix.Text.Trim)
            Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(conn, CommandType.StoredProcedure, cmd, prm)

            ret = True
        Catch ex As Exception

        End Try
        Return ret
    End Function

End Class