Imports DotNetNuke.Entities.Users
Imports Microsoft.ApplicationBlocks.Data
Imports System.Data.SqlClient

Partial Public Class ViewError
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim userInfo = UserController.GetCurrentUserInfo()
        'If (userInfo.IsInRole("Administrators")) Then
        If userInfo.DisplayName = "" Then
            Response.Redirect("NoAccess.aspx")
        Else
            If Not Page.IsPostBack Then

                Dim strConn As String = ConfigurationManager.ConnectionStrings("Ds2OriginConnection").ConnectionString

                Dim reader As SqlDataReader
                txtInvHRunAuto.Text = Request.QueryString("InvHRunAuto")

                reader = SqlHelper.ExecuteReader(strConn, CommandType.StoredProcedure, "sp_common_form_edi_viewErrorMessage", New SqlParameter("@INVH_RUN_AUTO", txtInvHRunAuto.Text))

                grdErrorsData.DataSource = reader
                grdErrorsData.DataBind()

                If grdErrorsData.Items.Count < 1 Then
                    lblMsg.Visible = True
                End If

                reader.Close()

            End If
        End If

    End Sub

End Class