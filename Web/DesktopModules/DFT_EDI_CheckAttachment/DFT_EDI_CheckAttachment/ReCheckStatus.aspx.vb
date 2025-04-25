Imports Microsoft.ApplicationBlocks.Data
Imports System.Data.SqlClient
Imports DFT.Dotnetnuke.ClassLibrary

Imports DotNetNuke.Entities.Users

Partial Public Class ReCheckStatus
    Inherits System.Web.UI.Page

    Dim objUserInfo As UserInfo = UserController.GetCurrentUserInfo

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            lblUserName.Text = objUserInfo.Username
        End If
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSave.Click
        Dim strEDIConn As String
        strEDIConn = ConfigurationManager.ConnectionStrings("EDIConnection").ConnectionString
        SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, "sp_EDI3_ReCheckStatus", _
            New SqlParameter("@INVH_RUN_AUTO", CommonUtility.Get_StringValue(Request.QueryString("Id"))), _
            New SqlParameter("@USERNAME", CommonUtility.Get_StringValue(objUserInfo.Username.ToLower)), _
            New SqlParameter("@MESSAGE", CommonUtility.Get_String(txtResult.Text.Trim())))

        Dim strScript As String = "<script language='javascript'>"
        strScript += "try{ "
        strScript += "alert('บันทึกข้อมูลเสร็จเรียบร้อยแล้ว');CloseMyWin();"
        strScript += "}catch(e){ }"
        strScript += "</script>"

        If (Not Page.ClientScript.IsStartupScriptRegistered("clientScript343")) Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript343", strScript, False)
        End If

    End Sub
End Class