Imports DFT.Dotnetnuke.ClassLibrary
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports Telerik.Web.UI

Partial Public Class frmConfirmDialog
    Inherits System.Web.UI.Page
    Dim strEDIConn As String = ConfigurationManager.ConnectionStrings("OriginConnection").ConnectionString

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Select Case CommonUtility.Get_Int32(Request.QueryString("TCat"))
                Case 1
                    lblMsg.Text = "ท่านต้องการแจ้งแก้ไขเอกสาร" & "<br />" & "หมายเลขอ้างอิงเลขที่ " & CommonUtility.Get_StringValue(Request.QueryString("Invh")) & " ใช่หรือไม่ ?"
                Case 2
                    lblMsg.Text = "ท่านต้องการส่งเอกสาร" & "<br />" & "หมายเลขอ้างอิงเลขที่ " & CommonUtility.Get_StringValue(Request.QueryString("Invh")) & " ใช่หรือไม่ ?"
                Case 3
                    lblMsg.Text = "ท่านต้องการรับคืนเอกสาร" & "<br />" & "หมายเลขอ้างอิงเลขที่ " & CommonUtility.Get_StringValue(Request.QueryString("Invh")) & " ใช่หรือไม่ ?"
            End Select

            lblTCat.Text = CommonUtility.Get_StringValue(Request.QueryString("TCat"))
            lblInvh_run_auto.Text = CommonUtility.Get_StringValue(Request.QueryString("Invh"))
            lblRemark_docatt.Text = CommonUtility.Get_StringValue(Request.QueryString("Remark"))
            lblUser_ID.Text = CommonUtility.Get_StringValue(Request.QueryString("UserID"))
            lblSite_ID.Text = CommonUtility.Get_StringValue(Request.QueryString("SiteID"))

        End If
    End Sub

    Protected Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            Dim ds As New DataSet
            ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, "Ma_EditDocAtt", _
            New SqlParameter("@TCat", CommonUtility.Get_Int32(lblTCat.Text)), _
            New SqlParameter("@invh_run_auto", CommonUtility.Get_String(lblInvh_run_auto.Text)), _
            New SqlParameter("@remark_docatt", CommonUtility.Get_String(lblRemark_docatt.Text)), _
            New SqlParameter("@user_id", CommonUtility.Get_StringValue(lblUser_ID.Text)), _
            New SqlParameter("@site_id", lblSite_ID.Text))

            If ds.Tables(0).Rows.Count > 0 Then
                Select Case CommonUtility.Get_Int32(ds.Tables(0).Rows(0).Item("retSTATUS"))
                    Case 0
                        ClientScript.RegisterStartupScript(Page.GetType(), "mykey", "CloseAndRebind();", True)
                    Case -1, -2, -3, -4, -5, -6
                        'ClientScript.RegisterStartupScript(Page.GetType(), "mykey", "CancelEdit();", True)
                        'Me.RadAjaxManager1.ResponseScripts.Add("window.alert('" & CommonUtility.Get_StringValue(ds.Tables(0).Rows(0).Item("retMessage")) & "');")
                        lblMsg.Text = CommonUtility.Get_StringValue(ds.Tables(0).Rows(0).Item("retMessage"))
                        lblMsg.ForeColor = Drawing.Color.Red
                        btnSave.Visible = False
                End Select
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        ClientScript.RegisterStartupScript(Page.GetType(), "mykey", "CancelEdit();", True)
    End Sub
End Class