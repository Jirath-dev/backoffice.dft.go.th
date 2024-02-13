Imports Microsoft.ApplicationBlocks.Data
Imports System.Data.SqlClient
Imports DFT.Dotnetnuke.ClassLibrary

Imports DotNetNuke.Entities.Users

Partial Public Class frmCancelForm
    Inherits System.Web.UI.Page

    Dim strEDIConn As String = ConfigurationManager.ConnectionStrings("OriginConnection").ConnectionString

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Check ว่า User ที่ Login เข้ามาอยู่ Site ไหน
        Dim myRoleControllers As New DotNetNuke.Security.Roles.RoleController()
        Dim i As Integer = 0
        Dim objUserInfo As UserInfo = UserController.GetCurrentUserInfo

        For i = 0 To objUserInfo.Roles.Length - 1
            Dim myRoleInfo As DotNetNuke.Security.Roles.RoleInfo = myRoleControllers.GetRoleByName(0, objUserInfo.Roles(i))

            'by rut 7-09-2555 ใช้วันที่ 22-01-2556 ใช้แทนด้านล่าง ต้องเพิ่มใน Table "RoleList" แทน
            If Get_ListRoles(myRoleInfo.RoleID, myRoleInfo.RoleName) <> "" Then
                Exit For
            End If
            'Select Case myRoleInfo.RoleID
            '    Case 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 26, 28
            '        lblRoleID.Text = myRoleInfo.RoleName
            '        Session("ssRoleName") = lblRoleID.Text
            '        Exit For
            'End Select
        Next i

        If Not Page.IsPostBack Then
            txtInvHRunAuto.Text = CommonUtility.Get_StringValue(Request.QueryString("RefNo"))
        End If
    End Sub
#Region "by rut"
    Function Get_ListRoles(ByVal ByRoleID As Integer, ByVal ByRoleName As String) As String
        Dim DSRoles As SqlDataReader
        Dim strEDIConn As String = ConfigurationManager.ConnectionStrings("OriginConnection").ConnectionString

        DSRoles = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "RoleList_GetList", _
                        New SqlParameter("@ListRoleNameCase", ByRoleID))

        Dim strListRole As String = ""

        If DSRoles.HasRows Then
            strListRole = ByRoleName
            lblRoleID.Text = strListRole
            Session("ssRoleName") = lblRoleID.Text
        End If

        Return strListRole
    End Function
#End Region
    Protected Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            Dim strEDIConn As String
            strEDIConn = ConfigurationManager.ConnectionStrings("EDIConnection").ConnectionString
            Dim objUserInfo As UserInfo = UserController.GetCurrentUserInfo

            Dim ds As New DataSet
            ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, "sp_form4_edi_notApproveForm", _
            New SqlParameter("@INVH_RUN_AUTO", CommonUtility.Get_StringValue(txtInvHRunAuto.Text)), _
            New SqlParameter("@USER_ID", CommonUtility.Get_StringValue(objUserInfo.Username.ToLower)), _
            New SqlParameter("@E01", CommonUtility.Get_String(txtE01.Text.Trim())), _
            New SqlParameter("@E02", CommonUtility.Get_String(txtE02.Text.Trim())), _
            New SqlParameter("@E03", CommonUtility.Get_String(txtE03.Text.Trim())), _
            New SqlParameter("@E04", ""), _
            New SqlParameter("@E05", CommonUtility.Get_String(txtE05.Text.Trim())), _
            New SqlParameter("@E06", CommonUtility.Get_String(txtE06.Text.Trim())), _
            New SqlParameter("@E07", CommonUtility.Get_String(txtE07.Text.Trim())), _
            New SqlParameter("@E08", CommonUtility.Get_String(txtE08.Text.Trim())), _
            New SqlParameter("@E09", CommonUtility.Get_String(txtE09.Text.Trim())), _
            New SqlParameter("@E10", CommonUtility.Get_String(txtE10.Text.Trim())), _
            New SqlParameter("@E11", CommonUtility.Get_String(txtE11.Text.Trim())), _
            New SqlParameter("@E12", CommonUtility.Get_String(txtE12.Text.Trim())), _
            New SqlParameter("@E99", CommonUtility.Get_String(txtE99.Text)), _
            New SqlParameter("@Cancel_Date", CommonUtility.Get_DateTime(Now)))

            If CommonUtility.Get_StringValue(ds.Tables(0).Rows(0).Item("retSTATUS")) = "0" Then
                'complete

                If CommonUtility.Get_StringValue(txtInvHRunAuto.Text) <> "" Then
                    'update CheckDoc_Date
                    SqlHelper.ExecuteNonQuery(strEDIConn, CommandType.Text, "update FORM_HEADER_EDI set CheckDoc_Date=getdate(),CheckDoc_By='" & CommonUtility.Get_StringValue(objUserInfo.Username.ToLower) & "' where INVH_RUN_AUTO='" & CommonUtility.Get_StringValue(txtInvHRunAuto.Text) & "' and (SentBy=1 OR SentBy=2)")
                End If


                ' ''Insert ActivityLog data

                ''SqlHelper.ExecuteNonQuery(strEDIConn, CommandType.StoredProcedure, "sp_common_edi_insertActivityLog_NewDS2", _
                ''                New SqlParameter("@user_id", CommonUtility.Get_StringValue(objUserInfo.Username)), _
                ''                New SqlParameter("@refno", CommonUtility.Get_StringValue(txtInvHRunAuto.Text)), _
                ''                New SqlParameter("@detail", "บันทึกผล [ไม่ผ่าน] การตรวจสอบแบบคำขอและเอกสารแนบโดยเจ้าหน้าที่"), _
                ''                New SqlParameter("@group_type", 2), _
                ''                New SqlParameter("@site_id", CommonUtility.Get_String(lblRoleID.Text)), _
                ''                New SqlParameter("@ip", CommonUtility.Get_String(Request.UserHostAddress)))
                ' ''end

                Dim strScript As String = "<script language='javascript'>"
                strScript += "try{ "
                strScript += "alert('แบบฟอร์มคำขอ เลขที่อ้างอิง " & txtInvHRunAuto.Text & " [ไม่ผ่าน] การตรวจสอบจากเจ้าหน้าที่! \nระบบได้ทำการบันทึกข้อมูลรายการนี้เรียบร้อยแล้ว'); window.parent.opener.refreshGrid(); window.parent.close();"
                strScript += "}catch(e){ }"
                strScript += "</script>"
                If (Not Page.ClientScript.IsStartupScriptRegistered("clientScript31")) Then
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript31", strScript, False)
                End If
            Else
                'error
                Me.RadAjaxManager1.ResponseScripts.Add("window.alert('" & CommonUtility.Get_StringValue(ds.Tables(0).Rows(0).Item("retSTATUS")) & "');")
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        'complete
        Dim strScript As String = "<script language='javascript'>"
        strScript += "try{ "
        strScript += "CancelEdit();"
        strScript += "}catch(e){ }"
        strScript += "</script>"
        If (Not Page.ClientScript.IsStartupScriptRegistered("clientScript33")) Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript33", strScript, False)
        End If
    End Sub
End Class