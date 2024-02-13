Imports System.Web.UI
Imports System.Collections.Generic
Imports System.Reflection

Imports DotNetNuke
Imports DotNetNuke.Services.Exceptions
Imports DotNetNuke.Services.Localization

Imports Microsoft.ApplicationBlocks.Data
Imports DFT.Dotnetnuke.ClassLibrary
Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI
Namespace Nti.Modules.DFT_EDI_ResetForm

    Partial Class ViewDFT_EDI_ResetForm
        Inherits Entities.Modules.PortalModuleBase
        Implements Entities.Modules.IActionable

        Dim strEDIConn As String = ConfigurationManager.ConnectionStrings("OriginConnection").ConnectionString
        Dim objConn As SqlConnection = Nothing
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

        Dim str_sendBy As String
        Protected Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
            Try
                Dim TCAT As Integer
                If chkUseRef2.Checked Then TCAT = 2 Else TCAT = 1

                Dim ds As New DataSet
                ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, "vi_select_FormReset_NewDS", _
                New SqlParameter("@TCat", TCAT), _
                New SqlParameter("@site_id", Session("ssRoleName")), _
                New SqlParameter("@invh_run_auto", CommonUtility.Get_StringValue(txtSearchRe.Text.Trim())))

                If ds.Tables(0).Rows.Count > 0 Then
                    GridZero.DataSource = ds.Tables(0)
                    GridZero.DataBind()

                    If GridZero.MasterTableView.Items.Count > 0 Then
                        PanelReset.Visible = True
                        str_sendBy = ""
                        str_sendBy = ds.Tables(0).Rows(0).Item("SentBy").ToString
                        lblstr_sentby.Text = str_sendBy
                        lblStatus.Text = ds.Tables(0).Rows(0).Item("edi_status_id").ToString
                        lblReceipt.Text = "สถานะการพิมพ์ใบเสร็จ : " & ds.Tables(0).Rows(0).Item("receipt_flag").ToString
                    Else
                        Me.RadAjaxManager1.ResponseScripts.Add("window.alert('ไม่พบหมายเลขคำร้องที่ทำการค้นหา');")
                    End If
                Else
                    Me.RadAjaxManager1.ResponseScripts.Add("window.alert('ไม่พบหมายเลขคำร้องที่ทำการค้นหา');")
                End If

            Catch ex As Exception
                Me.RadAjaxManager1.ResponseScripts.Add("window.alert('" & ex.Message & "');")
            End Try
        End Sub

        Function GetStatus_(ByVal sent_check As Object) As String
            Dim S_check As String

            If Not sent_check.Equals(System.DBNull.Value) = True Then
                S_check = sent_check
            Else
                S_check = "N"
            End If
            Return S_check
        End Function

        Function GetProGram_(ByVal sent_SendBy As Object) As String
            Dim S_check As String
            Select Case sent_SendBy.ToString
                Case "0"
                    S_check = "ปกติ"
                Case "1"
                    S_check = "Digital Web"
                Case "2"
                    S_check = "XML"
                Case Else
                    S_check = "ปกติ"
            End Select
            
            Return S_check
        End Function

        Function Reset_Form() As Boolean
            Dim m_iReturn As Integer
            Dim TCAT As Integer
            If chkUseRef2.Checked Then TCAT = 2 Else TCAT = 1

            objConn = New SqlConnection(strEDIConn)
            m_iReturn = SqlHelper.ExecuteNonQuery(objConn, CommandType.StoredProcedure, "sp_ResetForm_NewDS", _
                                    New SqlParameter("@TCat", TCAT), _
                                    New SqlParameter("@invh_run_auto", CommonUtility.Get_StringValue(txtSearchRe.Text.Trim())), _
                                    New SqlParameter("@site_id", Session("ssRoleName")), _
                                    New SqlParameter("@SentBy", lblstr_sentby.Text))
            If m_iReturn = 0 Then
                Return False
            Else
                Return True
            End If
        End Function
        Protected Sub btnResetForm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnResetForm.Click
            If Session("ssRoleName") = "" Then
                RadAjaxManager1.ResponseScripts.Add("window.alert('กรุณา Login เข้าสู่ระบบใหม่');")
            Else
                If lblStatus.Text = "A" Then
                    RadAjaxManager1.ResponseScripts.Add("window.alert('ไม่สามารถย้อนสถานะฟอร์มได้ เนื่องจากฟอร์มได้ผ่านการอนุมัติแล้ว!!!')")
                Else
                    If Reset_Form() = True Then
                        RadAjaxManager1.ResponseScripts.Add("window.alert('คืนสถานะฟอร์มเรียบร้อยแล้ว');")
                    Else
                        RadAjaxManager1.ResponseScripts.Add("window.alert('ไม่สามารถคืนสถานะฟอร์มได้');")
                    End If
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
        Protected Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Me.Load
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
    End Class
End Namespace
