Imports Microsoft.ApplicationBlocks.Data
Imports DFT.Dotnetnuke.ClassLibrary
Imports System.Data.SqlClient

Partial Public Class ViewErrors
    Inherits System.Web.UI.Page
    Dim strConn As String = ConfigurationManager.ConnectionStrings("OriginConnection").ConnectionString

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            lblHeader.Text = "รายการที่ต้องแก้ไข"
            txtInvHRunAuto.Text = CommonUtility.Get_StringValue(Request.QueryString("InvHRunAuto"))

            Dim ds As New DataSet
            ds = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, "sp_common_form_edi_viewErrorMessage", New SqlParameter("@INVH_RUN_AUTO", txtInvHRunAuto.Text))

            If ds.Tables(0).Rows.Count > 0 Then
                grdErrorsData.DataSource = ds.Tables(0)
                grdErrorsData.DataBind()
                lblMessage.Text = ""
            Else
                lblMessage.Text = "ไม่มีประวัติการตรวจคำขอและเอกสารแนบ"
            End If
        End If
    End Sub

End Class