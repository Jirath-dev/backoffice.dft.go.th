Imports System.Web.UI
Imports System.Collections.Generic
Imports System.Reflection

Imports DotNetNuke
Imports DotNetNuke.Services.Exceptions
Imports DotNetNuke.Services.Localization
Imports DotNetNuke.Entities.Users
''Imports DotNetNuke.Entities.Modules.PortalModuleBase

Imports DFT.Dotnetnuke.ClassLibrary
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports Telerik.Web.UI


Partial Public Class frmCheckForm3
    Inherits System.Web.UI.Page

    Dim strEDIConn As String = ConfigurationManager.ConnectionStrings("OriginConnection").ConnectionString
    Dim myReader As SqlDataReader = Nothing
    Dim objConn As SqlConnection = Nothing
    Dim myReader1 As SqlDataReader = Nothing
    Dim objConn1 As SqlConnection = Nothing

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
            lblInvh_Run_Auto.Text = CommonUtility.Get_StringValue(Request.QueryString("RefNo"))
            lblUser_ID.Text = objUserInfo.Username

            If Request.QueryString("mode") = "view" Then
                panelResult.Visible = False
                lblCheckResult.Text = "<div class=""check-result"">*** คำขอหนังสือรับรองรายการนี้ตรวจสอบเรียบร้อยแล้ว ***</div>"
            End If
            Call LoadAttachFile(lblInvh_Run_Auto.Text)

            '//Set Session For Checkform KPI
            START_CHCEK_TIME.Value = Date.Now

            ''ตรวจสอบ BlackList / WatchList
            Dim blacklist As String = ReportPrintClass.CheckBlackList(Request.QueryString("TaxNo"))
            If blacklist = "" Or blacklist Is Nothing Then
                ''ถ้าเป็นค่าว่าง หรือ Nothing ให้ซ่อน Label ไว้
                lblMsgBlackList.Visible = False
            Else
                lblMsgBlackList.Visible = True
                lblMsgBlackList.Text = "<a href=# onclick=ShowBlackList();><u>" & blacklist & "</u></a>"
            End If

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
    Private Sub LoadAttachFile(ByVal RefNo As String)
        Try
            objConn1 = New SqlConnection(strEDIConn)

            Dim objUserInfo As UserInfo = UserController.GetCurrentUserInfo

            If Request.QueryString("mode") <> "view" Then

                myReader1 = SqlHelper.ExecuteReader(objConn1, CommandType.StoredProcedure, "sp_Form_EDI_GetUserCheckAttach", New SqlParameter("@INVH_RUN_AUTO", RefNo))
                myReader1.Read()

                Dim userview As String = ""
                If myReader1.HasRows Then
                    userview = myReader1("CheckDoc_By").ToString()

                    If DataUtil.ConvertToBoolean(myReader1("ASW_Support")) _
                       And DataUtil.ConvertToBoolean(myReader1("ASW_CountryAllow")) _
                       And Not DataUtil.ConvertToBoolean(myReader1("IsPrintForm")) Then
                        btnApprove.Visible = True
                        Button1.Visible = False
                        div_asw.Style.Item("display") = ""
                    Else
                        btnApprove.Visible = False
                        Button1.Visible = True
                        div_asw.Style.Item("display") = "none"
                    End If

                End If

                myReader1.Close()

                If userview <> "" Then
                    'btnNotApproved.Visible = False
                    'Button1.Visible = False
                    Panel1.Visible = True
                    lblMsg.Text = "เลขที่ใบคำร้องนี้ กำลังถูกตรวจสอบโดย " & userview & vbCrLf '& " ดังนั้นจึงดูได้อย่างเดียว ไม่สามารถบันทึกข้อมูลใดๆ ได้"
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript3", "<script language='javascript'>alert('เลขที่ใบคำร้องนี้ กำลังถูกตรวจสอบโดย " & userview & "');</script>", False)
                ElseIf Not objUserInfo.Username.ToLower.Contains("adminds2") Then
                    Dim sql As String
                    Panel1.Visible = False
                    sql = "UPDATE form_header_edi SET CheckDoc_By='" & objUserInfo.Username.ToLower & "' WHERE invh_run_auto='" & RefNo & "' AND CheckDoc_Date is null AND edi_status_id='Q'"
                    SqlHelper.ExecuteNonQuery(objConn1, CommandType.Text, sql)

                    ' ''Insert ActivityLog data
                    ''SqlHelper.ExecuteNonQuery(objConn1, CommandType.StoredProcedure, "sp_common_edi_insertActivityLog_NewDS2", _
                    ''                New SqlParameter("@user_id", CommonUtility.Get_StringValue(objUserInfo.Username)), _
                    ''                New SqlParameter("@refno", CommonUtility.Get_StringValue(RefNo)), _
                    ''                New SqlParameter("@detail", "เข้าตรวจสอบแบบคำขอและเอกสารแนบโดยเจ้าหน้าที่"), _
                    ''                New SqlParameter("@group_type", 2), _
                    ''                New SqlParameter("@site_id", CommonUtility.Get_String(lblRoleID.Text)), _
                    ''                New SqlParameter("@ip", CommonUtility.Get_String(Request.UserHostAddress)))
                    ' ''end
                End If

            Else

                Dim strResult As String = ""
                Try
                    myReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "sp_edi_getFormCheckedInfo_DS2", New SqlParameter("@REF_No", RefNo))
                    myReader.Read()
                    If myReader.HasRows Then
                        strResult = "สถานะ: " & myReader("Check_Result") & _
                                    "<br/>วันที่ส่งคำขอ: " & myReader("Sent_Date") & "&nbsp;&nbsp;วันที่ตรวจสอบ: " & myReader("CheckDoc_Date") & " &nbsp;&nbsp;โดย: " & myReader("CheckDoc_By").ToString() & _
                                    "<br/>วันที่พิมพ์ฟอร์ม: " & myReader("printFormDate") & "&nbsp;&nbsp;โดย: " & myReader("UserPrintForm")

                        'If myReader("CheckDoc_By").ToString.ToLower.Equals(objUserInfo.Username.ToLower) And myReader("edi_status_id") <> "A" And myReader("print_flag") <> "Y" Then
                        '    btnReStatus.Visible = True
                        'End If
                        If objUserInfo.IsInRole("Department_Leader") And myReader("edi_status_id") <> "A" And myReader("print_flag") <> "Y" Then
                            btnReStatus.Visible = True
                        End If

                    End If
                    myReader.Close()
                Catch ex As Exception
                    strResult = "" & ex.Message
                End Try
                lblResultCheck.Text = strResult

            End If

            myReader1 = SqlHelper.ExecuteReader(objConn1, CommandType.StoredProcedure, "sp_Form_EDI_SelectAttachFile_NewDS", New SqlParameter("@INVH_RUN_AUTO", RefNo))
            grdAttachFile.DataSource = myReader1
            grdAttachFile.DataBind()

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            Dim result As Integer
            Dim RefNo As String = ""
            RefNo = lblInvh_Run_Auto.Text
            objConn1 = New SqlConnection(strEDIConn)

            Dim prm(3) As SqlClient.SqlParameter
            prm(0) = New SqlParameter("@INVH_RUN_AUTO", RefNo)
            prm(1) = New SqlParameter("@START_CHCEK_TIME", START_CHCEK_TIME.Value)
            prm(2) = New SqlParameter("@USER_ID", CommonUtility.Get_StringValue(lblUser_ID.Text.ToLower))

            result = SqlHelper.ExecuteNonQuery(objConn1, CommandType.StoredProcedure, "sp_common_form_edi_attachdoc_NewDS", prm)

            If result > 0 Then

                ' ''Insert ActivityLog data
                ''Dim objUserInfo As UserInfo = UserController.GetCurrentUserInfo
                ''SqlHelper.ExecuteNonQuery(objConn1, CommandType.StoredProcedure, "sp_common_edi_insertActivityLog_NewDS2", _
                ''                New SqlParameter("@user_id", CommonUtility.Get_StringValue(objUserInfo.Username)), _
                ''                New SqlParameter("@refno", CommonUtility.Get_StringValue(RefNo)), _
                ''                New SqlParameter("@detail", "บันทึกผล [ผ่าน] การตรวจสอบแบบคำขอและเอกสารแนบโดยเจ้าหน้าที่"), _
                ''                New SqlParameter("@group_type", 2), _
                ''                New SqlParameter("@site_id", CommonUtility.Get_String(lblRoleID.Text)), _
                ''                New SqlParameter("@ip", CommonUtility.Get_String(Request.UserHostAddress)))
                ' ''end

                '======================================
                Dim strScript As String = "<script language='javascript'>"
                strScript += "try{ "
                strScript += "alert('แบบฟอร์มคำร้อง เลขที่อ้างอิง " & RefNo & " [ผ่าน] การตรวจสอบจากเจ้าหน้าที่! \nระบบทำการบันทึกผลการตรวจสอบเอกสารเสร็จเรียบร้อยแล้ว'); CloseMyWin();"
                strScript += "}catch(e){ }"
                strScript += "</script>"
                If (Not Page.ClientScript.IsStartupScriptRegistered("clientScript3")) Then
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript3", strScript, False)
                End If

                '//Remove Session
                'Session.Remove("START_CHCEK_TIME")

                '======================================
            Else
                '======================================
                Dim strScript As String = "<script language='javascript'>"
                strScript += "try{ "
                strScript += "alert('เกิดข้อผิดพลาด!\nไม่สามารถบันทึกผลการตรวจสอบเอกสารได้!');"
                strScript += "}catch(e){ }"
                strScript += "</script>"
                If (Not Page.ClientScript.IsStartupScriptRegistered("clientScript4")) Then
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript4", strScript, False)
                End If
                '======================================
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub btnApprove_Click(sender As Object, e As EventArgs) Handles btnApprove.Click
        Try
            Dim result As Integer
            Dim RefNo As String = ""
            RefNo = lblInvh_Run_Auto.Text
            objConn1 = New SqlConnection(strEDIConn)

            Dim prm(3) As SqlClient.SqlParameter
            prm(0) = New SqlParameter("@INVH_RUN_AUTO", RefNo)
            prm(1) = New SqlParameter("@START_CHCEK_TIME", START_CHCEK_TIME.Value)
            prm(2) = New SqlParameter("@USER_ID", CommonUtility.Get_StringValue(lblUser_ID.Text.ToLower))

            '//กดแล้วอนุมัติทันที
            '//1. ผ่านการตรวจ
            '//2. พิมพ์หนังสือรับรอง
            '//3. อนุมัติ
            '//step ทั้งหมด รวมที่ sp_common_form_edi_attachdoc_NewDS_ASW
            result = SqlHelper.ExecuteNonQuery(objConn1, CommandType.StoredProcedure, "sp_common_form_edi_attachdoc_NewDS_ASW", prm)

            If result > 0 Then

                '======================================
                Dim strScript As String = "<script language='javascript'>"
                strScript += "try{ "
                strScript += "alert('แบบฟอร์มคำร้อง เลขที่อ้างอิง " & RefNo & " [ผ่าน] การตรวจสอบจากเจ้าหน้าที่! \nระบบทำการบันทึกผลการตรวจสอบเอกสารเสร็จเรียบร้อยแล้ว'); CloseMyWin();"
                strScript += "}catch(e){ }"
                strScript += "</script>"
                If (Not Page.ClientScript.IsStartupScriptRegistered("clientScript3")) Then
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript3", strScript, False)
                End If

                '//Remove Session
                'Session.Remove("START_CHCEK_TIME")

                '======================================
            Else
                '======================================
                Dim strScript As String = "<script language='javascript'>"
                strScript += "try{ "
                strScript += "alert('เกิดข้อผิดพลาด!\nไม่สามารถบันทึกผลการตรวจสอบเอกสารได้!');"
                strScript += "}catch(e){ }"
                strScript += "</script>"
                If (Not Page.ClientScript.IsStartupScriptRegistered("clientScript4")) Then
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript4", strScript, False)
                End If
                '======================================
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

End Class