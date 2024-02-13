Imports System.Data.SqlClient
Imports DFT.Dotnetnuke.ClassLibrary
Imports Microsoft.ApplicationBlocks.Data
Imports Telerik.Web.UI

Partial Public Class frmApproved
    Inherits System.Web.UI.Page
    Dim strEDIConn As String = ConfigurationManager.ConnectionStrings("OriginConnection").ConnectionString

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            lblINVH_RUN_AUTO.Text = CommonUtility.Get_StringValue(Session("ssInvh_Run_Auto"))
            lblSite_ID.Text = CommonUtility.Get_StringValue(Session("ssRoleName"))
            lblUserName.Text = CommonUtility.Get_StringValue(Session("ssUserName"))

            If Not CheckHaveInvoice() Then
                Me.RadAjaxManager1.ResponseScripts.Add("window.alert('กรุณาทำการป้อนข้อมูล Invoice ก่อน !!!');")
                ClientScript.RegisterStartupScript(Page.GetType(), "mykey", "CancelEdit();", True)
            End If

            rdpApprovedDate.SelectedDate = Today
            txtReferenceCode2.Text = Session("ssReferenceCode2")
        End If
    End Sub

    Function CheckHaveInvoice() As Boolean
        Try
            'select * from INVOICE_MANUAL where INVH_RUN_AUTO='20091218-000001'
            Dim ds As New DataSet
            Dim strCommad As String
            strCommad = "select * from INVOICE_MANUAL where INVH_RUN_AUTO='" & lblINVH_RUN_AUTO.Text & "'"
            ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.Text, strCommad)
            If ds.Tables(0).Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Function

    Protected Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        ClientScript.RegisterStartupScript(Page.GetType(), "mykey", "CancelEdit();", True)
    End Sub

    Protected Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            Dim ds As New DataSet
            ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, "sp_common_form_manual_approve", _
            New SqlParameter("@INVH_RUN_AUTO", CommonUtility.Get_String(lblINVH_RUN_AUTO.Text)), _
            New SqlParameter("@SITE_ID", CommonUtility.Get_String(lblSite_ID.Text)), _
            New SqlParameter("@EDI_STATUS_ID", CommonUtility.Get_StringValue(rblType.SelectedValue)), _
            New SqlParameter("@APPROVE_DATE", CommonUtility.Get_DateTime(rdpApprovedDate.SelectedDate)), _
            New SqlParameter("@BOOK_ID", CommonUtility.Get_StringValue(txtReferenceCode2.Text.Trim())), _
            New SqlParameter("@REASON", ""), _
            New SqlParameter("@APPROVAL", CommonUtility.Get_String(lblUserName.Text)))

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
End Class