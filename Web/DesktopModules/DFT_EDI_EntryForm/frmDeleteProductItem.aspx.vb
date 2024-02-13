Imports System.Data.SqlClient
Imports DFT.Dotnetnuke.ClassLibrary
Imports Microsoft.ApplicationBlocks.Data
Imports Telerik.Web.UI

Partial Public Class frmDeleteProductItem
    Inherits System.Web.UI.Page
    Dim strRegConn As String = ConfigurationManager.ConnectionStrings("RegisBackConnection").ConnectionString
    Dim strEDIConn As String = ConfigurationManager.ConnectionStrings("OriginConnection").ConnectionString

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            lblinvh_run_auto.Text = CommonUtility.Get_StringValue(Request.QueryString("h_run_auto"))
            lblinvd_run_auto.Text = CommonUtility.Get_StringValue(Request.QueryString("d_run_auto"))
            lblSite_ID.Text = CommonUtility.Get_StringValue(Session("ssRoleName"))
        End If
    End Sub

    Protected Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Try
            'sp_common_form_manual_deleteDetail '20100212-000002','ST-001','0001'
            Dim ds As New DataSet
            ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, "sp_common_form_manual_deleteDetail", _
            New SqlParameter("@INVH_RUN_AUTO", CommonUtility.Get_StringValue(lblinvh_run_auto.Text)), _
            New SqlParameter("@SITE_ID", CommonUtility.Get_StringValue(lblSite_ID.Text)), _
            New SqlParameter("@INVD_RUN_AUTO", CommonUtility.Get_StringValue(lblinvd_run_auto.Text)))

            If ds.Tables(0).Rows.Count > 0 Then
                If CommonUtility.Get_Int32(ds.Tables(0).Rows(0).Item("retSTATUS")) = 0 Then
                    ClientScript.RegisterStartupScript(Page.GetType(), "mykey", "CloseAndRebind();", True)
                Else
                    Me.RadAjaxManager1.ResponseScripts.Add("window.alert('เกิดความผิดพลาด กรุณาติดต่อผู้ดูแลระบบ !!!');")
                End If
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        ClientScript.RegisterStartupScript(Page.GetType(), "mykey", "CancelEdit();", True)
    End Sub
End Class