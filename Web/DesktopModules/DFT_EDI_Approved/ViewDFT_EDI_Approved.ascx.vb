Imports System.Web.UI
Imports System.Collections.Generic
Imports System.Reflection

Imports DotNetNuke
Imports DotNetNuke.Services.Exceptions
Imports DotNetNuke.Services.Localization

Imports System.Data.SqlClient
Imports DFT.Dotnetnuke.ClassLibrary
Imports Microsoft.ApplicationBlocks.Data
Imports Telerik.Web.UI

Namespace NTi.Modules.DFT_EDI_Approved
    Partial Class ViewDFT_EDI_Approved
        Inherits Entities.Modules.PortalModuleBase
        Dim strEDIConn As String = ConfigurationManager.ConnectionStrings("OriginConnection").ConnectionString
        Dim myReader As SqlDataReader = Nothing
        Dim objConn As SqlConnection = Nothing
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
        Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            txtSearch.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" + btnApproved.UniqueID + "').click();return false;}} else {return true}; ")

            'Check ว่า User ที่ Login เข้ามาอยู่ Site ไหน
            Dim myRoleControllers As New DotNetNuke.Security.Roles.RoleController()
            Dim i As Integer = 0
            For i = 0 To MyBase.UserInfo.Roles.Length - 1
                Dim myRoleInfo As DotNetNuke.Security.Roles.RoleInfo = myRoleControllers.GetRoleByName(0, MyBase.UserInfo.Roles(i))

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
                lblType.Text = "LOAD"
                rdpApprovedDate.SelectedDate = Today
                txtSearch.Text = ""
                txtSearch.Focus()
            End If


        End Sub

        Private Sub rgApprovedList_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles rgApprovedList.DataBound
            myReader.Close()
            objConn.Close()
        End Sub

        Private Sub rgApprovedList_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgApprovedList.NeedDataSource
            rgApprovedList.DataSource = LoadApproved(lblType.Text)
        End Sub

        Private Function LoadApproved(ByVal _Type As String) As SqlDataReader
            Dim expression1 As GridGroupByExpression = GridGroupByExpression.Parse("edi_status [สถานะ], count(invh_run_auto) Items [จำนวน] Group By edi_status")
            Me.rgApprovedList.MasterTableView.GroupByExpressions.Add(expression1)

            'Dim expression2 As GridGroupByExpression = GridGroupByExpression.Parse("form_name [ชื่อฟอร์ม], count(invh_run_auto) Items [จำนวน] Group By form_name")
            'Me.rgApprovedList.MasterTableView.GroupByExpressions.Add(expression2)

            Select Case _Type
                Case "LOAD", "SEARCH"
                    objConn = New SqlConnection(strEDIConn)
                    myReader = SqlHelper.ExecuteReader(objConn, CommandType.StoredProcedure, "vi_select_approveByBar_NewDS", _
                    New SqlParameter("@TCat", 3), _
                    New SqlParameter("@invh_run_auto", ""), _
                    New SqlParameter("@FROM_DATE", FunctionUtility.DMY2YMD(rdpApprovedDate.SelectedDate.Value)), _
                    New SqlParameter("@site_id", lblRoleID.Text))

                Case "APPROVED"
                    objConn = New SqlConnection(strEDIConn)
                    myReader = SqlHelper.ExecuteReader(objConn, CommandType.StoredProcedure, "vi_select_approveByBar", _
                    New SqlParameter("@TCat", 1), _
                    New SqlParameter("@invh_run_auto", CommonUtility.Get_StringValue(txtSearch.Text.Trim())), _
                    New SqlParameter("@FROM_DATE", FunctionUtility.DMY2YMD(rdpApprovedDate.SelectedDate.Value.AddDays(1))), _
                    New SqlParameter("@site_id", lblRoleID.Text))

            End Select

            Return myReader
        End Function

        Protected Sub btnApproved_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnApproved.Click
            Try
                Dim isShowPrint As Boolean = IsShowReceive(Session("ssRoleName"))

                'เช็คเงื่อนไข เพิ่ม เพื่อป้องกัน งานหลุด คือ Apporve ก่อน Print ใบเสร็จ หรือ หนังสือรับรอง By Narit 09-03-60
                Dim cmd As String = " SELECT invh_run_auto, print_flag, receipt_flag, reference_code2 " &
                                    " FROM form_header_edi " &
                                    "  WHERE  (print_flag = 'Y') AND (reference_code2 IS NOT NULL) AND (invh_run_auto = '" & txtSearch.Text & "')"
                '// AND (receipt_flag = 'Y') 
                If isShowPrint = True Then



                    cmd = cmd & " AND (receipt_flag = 'Y')"
                End If
                '=======================================================================================================
                Dim ds As DataSet = SqlHelper.ExecuteDataset(strEDIConn, CommandType.Text, cmd)

                If ds.Tables(0).Rows.Count > 0 Then
                    Dim dr As SqlDataReader
                    dr = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "vi_form4_edi_approveFormBar_NewDS",
                    New SqlParameter("@TCat", IIf(chkBranch.Checked, 2, 1)),
                    New SqlParameter("@INVH_RUN_AUTO", CommonUtility.Get_StringValue(txtSearch.Text.Trim())),
                    New SqlParameter("@USER_ID", CommonUtility.Get_StringValue(UserInfo.Username)),
                    New SqlParameter("@site_id", lblRoleID.Text))

                    If dr.Read() Then
                        If CommonUtility.Get_StringValue(dr.Item("retSTATUS")) <> "0" Then
                            Me.RadAjaxManager1.ResponseScripts.Add("window.alert('" & CommonUtility.Get_StringValue(dr.Item("retMSG")) & "');")
                            txtSearch.Text = ""
                            txtSearch.Focus()
                        Else
                            dr.Close()
                            lblType.Text = "APPROVED"
                            rgApprovedList.Rebind()
                            txtSearch.Text = ""
                            txtSearch.Focus()
                        End If
                    End If

                Else
                    txtSearch.Text = ""
                    Page.ClientScript.RegisterStartupScript(Page.GetType, "ok", "javascript:alert('ไม่สามารถบันทึกข้อมูลได้เนื่องจากยังไม่ได้พิมพ์ฟอร์มหรือใบเสร็จ กรุณาตรวจสอบใหม่อีกครั้ง');", True)
                End If


            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End Sub

        Protected Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
            Try
                lblType.Text = "SEARCH"
                rgApprovedList.Rebind()
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End Sub

        Private Function IsShowReceive(site_id As String) As Boolean
            Dim ret As Boolean = True
            Try
                Dim Cmd As String = "Select s.is_print_receipt from site_plus s Where s.site_id = @site_id"
                Dim Prm As New SqlParameter("@site_id", site_id)
                Dim Ds As DataSet = SqlHelper.ExecuteDataset(strEDIConn, CommandType.Text, Cmd, Prm)

                If Ds.Tables(0).Rows.Count > 0 Then
                    ret = ConvertToBoolean(Ds.Tables(0).Rows(0).Item("is_print_receipt"))
                End If
            Catch ex As Exception

            End Try

            Return ret

        End Function

        Private Function ConvertToBoolean(obj As Object) As Boolean
            Dim ret As Boolean = False
            Try
                ret = Convert.ToBoolean(obj)
            Catch ex As Exception

            End Try

            Return ret

        End Function

    End Class

End Namespace
