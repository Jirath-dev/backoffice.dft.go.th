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
Namespace Nti.Modules.DFT_EDI_ResetReceipt
    Partial Class ViewDFT_EDI_ResetReceipt
        Inherits Entities.Modules.PortalModuleBase
        Implements Entities.Modules.IActionable

        Dim strEDIConn As String = ConfigurationManager.ConnectionStrings("OriginConnection").ConnectionString
        Dim myReader As SqlDataReader = Nothing
        Dim objConn As SqlConnection = Nothing
        Dim _UserName As String = CommonUtility.Get_StringValue(DotNetNuke.Entities.Users.UserController.GetUser(PortalId, UserId, False).Username)

        Dim reader_receipt As SqlDataReader = Nothing
        Dim DataSet_Userreceipt As DataSet = Nothing
#Region "Event Handlers"

        ''' -----------------------------------------------------------------------------
        ''' <summary>
        ''' Page_Load runs when the control is loaded
        ''' </summary>
        ''' <remarks>
        ''' </remarks>
        ''' <history>
        ''' </history>
        ''' -----------------------------------------------------------------------------

#End Region

#Region "Optional Interfaces"

        ''' -----------------------------------------------------------------------------
        ''' <summary>
        ''' Registers the module actions required for interfacing with the portal framework
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' </history>
        ''' -----------------------------------------------------------------------------
        Public ReadOnly Property ModuleActions() As Entities.Modules.Actions.ModuleActionCollection Implements Entities.Modules.IActionable.ModuleActions
            Get
                Dim Actions As New Entities.Modules.Actions.ModuleActionCollection
                Actions.Add(GetNextActionID, Localization.GetString(Entities.Modules.Actions.ModuleActionType.AddContent, LocalResourceFile), Entities.Modules.Actions.ModuleActionType.AddContent, "", "", EditUrl(), False, Security.SecurityAccessLevel.Edit, True, False)
                Return Actions
            End Get
        End Property

#End Region

        Protected Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
            If Session("ssRoleName") = "" Then
                RadAjaxManager1.ResponseScripts.Add("window.alert('กรุณา Login เข้าสู่ระบบใหม่');")
            Else
                If txtSearchRe.Text.Trim <> "" Then
                    SearchReceiptCompanyName()
                    GridZero.DataSource = SearchReceript()
                    GridZero.Rebind()
                    PanelReset.Visible = True
                    btnReset.Visible = True
                Else
                    RadAjaxManager1.ResponseScripts.Add("window.alert('กรุณาระบุ เลขที่ใบเสร็จที่ออกใบเสร็จสมบูรณ์');")
                End If
            End If
        End Sub
        'by rut หาเลขใบเสร็จ
        Function SearchReceript() As SqlDataReader
            Try
                objConn = New SqlConnection(strEDIConn)

                Dim SpName As String = "sp_Zero_search_NewDS"

                ''ByTine 07-01-2559 ปรับปรุงสำหรับใบเสร็จใหม่ (ใบเสร็จเหลือง)
                If rblBillType.SelectedValue = "2" Then
                    SpName = "sp_Zero_search_NewDS_v2"
                End If

                myReader = SqlHelper.ExecuteReader(objConn, CommandType.StoredProcedure, SpName, _
                                        New SqlParameter("@bill_no", txtSearchRe.Text.Trim), _
                                        New SqlParameter("@site_id", Session("ssRoleName")))

                Return myReader
            Catch ex As Exception
                Return Nothing
            End Try
        End Function

        Function SearchReceiptCompanyName()
            Try
                Dim strcommand As String = ""
                strcommand = "SELECT * FROM dbo.receipt_bill_header WHERE (bill_no = @bill_no) AND (site_id = @site_id)"

                ''ByTine 07-01-2559 ปรับปรุงสำหรับใบเสร็จใหม่ (ใบเสร็จเหลือง)
                If rblBillType.SelectedValue = "2" Then
                    strcommand = "SELECT * FROM dbo.receipt_bill_header_v2 WHERE (bill_no = @bill_no) AND (site_id = @site_id)"
                End If

                Dim ds As New DataSet
                ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.Text, strcommand, _
                             New SqlParameter("@bill_no", txtSearchRe.Text.Trim), _
                             New SqlParameter("@site_id", Session("ssRoleName")))

                If ds.Tables(0).Rows.Count > 0 Then
                    txtCompanyName.Text = ds.Tables(0).Rows(0).Item("receipt_name")
                    Return ds
                End If

            Catch ex As Exception
                Response.Write(ex.Message)
            End Try

        End Function

        Function Reset_Receript() As Boolean
            Dim m_iReturn As Integer
            Dim Str_Remark, SpName_Free, SpName As String
            Str_Remark = Session("UName") & " - " & txtRemark.Text

            objConn = New SqlConnection(strEDIConn)

            SpName_Free = "sp_ResetReceipt_NewDS_Fees_No"
            SpName = "sp_ResetReceipt_NewDS"

            ''ByTine 07-01-2559 ปรับปรุงสำหรับใบเสร็จใหม่ (ใบเสร็จเหลือง)
            If rblBillType.SelectedValue = "2" Then
                SpName_Free = "sp_ResetReceipt_NewDS_Fees_No_v2"
                SpName = "sp_ResetReceipt_NewDS_v2"
            End If

            If ChkFees.Checked = True Then
                m_iReturn = SqlHelper.ExecuteNonQuery(objConn, CommandType.StoredProcedure, SpName_Free, _
                            New SqlParameter("@bill_no", txtSearchRe.Text.Trim), _
                            New SqlParameter("@site_id", Session("ssRoleName")), _
                            New SqlParameter("@remark", Session("UName") & " - " & "ได้รับการยกเว้นค่าธรรมเนียม"), _
                            New SqlParameter("@companyname", txtCompanyName.Text))
            Else
                m_iReturn = SqlHelper.ExecuteNonQuery(objConn, CommandType.StoredProcedure, SpName, _
                            New SqlParameter("@bill_no", txtSearchRe.Text.Trim), _
                            New SqlParameter("@site_id", Session("ssRoleName")), _
                            New SqlParameter("@remark", Str_Remark))
            End If

            If m_iReturn = 0 Then
                Return False
            Else
                Return True
            End If
        End Function
        Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            txtSearchRe.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" + btnSearch.UniqueID + "').click();return false;}} else {return true}; ")
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
                Session("UName") = UserInfo.Username
                txtSearchRe.Focus()

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
        Protected Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
            If Session("ssRoleName") = "" Then
                RadAjaxManager1.ResponseScripts.Add("window.alert('กรุณา Login เข้าสู่ระบบใหม่');")
            Else
                If Reset_Receript() = True Then
                    RadAjaxManager1.ResponseScripts.Add("window.alert('บันทึกข้อมูลใบเสร็จเป็นศูนย์เรียบร้อยแล้ว');")
                Else
                    RadAjaxManager1.ResponseScripts.Add("window.alert('ไม่สามารถบันทึกข้อมูลใบเสร็จเป็นศูนย์ได้');")
                End If
            End If
            
        End Sub

        Private Sub ChkFees_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkFees.CheckedChanged
            If ChkFees.Checked = True Then
                txtCompanyName.Visible = True
            Else
                txtCompanyName.Visible = False
            End If
        End Sub
    End Class

End Namespace
