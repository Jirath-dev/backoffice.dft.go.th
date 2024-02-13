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
Namespace NTi.Modules.DFT_EDI_ReceiptZero

    Partial Class ViewDFT_EDI_ReceiptZero
        Inherits Entities.Modules.PortalModuleBase
        Implements Entities.Modules.IActionable
        'เปลี่ยน strRegConn เป็น strEDIConn เพื่อไม่เป็น Manual
        Dim strRegConn As String = ConfigurationManager.ConnectionStrings("RegisBackConnection").ConnectionString
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
        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            txtSearch.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" + btnSearch.UniqueID + "').click();return false;}} else {return true}; ")
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


#End Region
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
                ''ByTine 22-07-2559 ใบเสร็จเขียว
                If txtSearchRe.Text.Trim <> "" And rblRecieptType.SelectedValue = 0 Then
                    GridZero.DataSource = SearchReceript()
                    GridZero.Rebind()

                    ''ByTine 22-07-2559 ใบเสร็จเหลือง
                ElseIf txtSearchRe.Text.Trim <> "" And rblRecieptType.SelectedValue = 1 Then
                    GridZero.DataSource = SearchReceript_v2()
                    GridZero.Rebind()
                Else
                    RadAjaxManager1.ResponseScripts.Add("window.alert('กรุณาระบุ เลขที่ใบเสร็จที่ออกใบเสร็จสมบูรณ์');")
                End If
            End If
           
        End Sub

        Function SearchReceript() As SqlDataReader
            Try
                objConn = New SqlConnection(strEDIConn)
                myReader = SqlHelper.ExecuteReader(objConn, CommandType.StoredProcedure, "sp_Zero_search_NewDS", _
                                        New SqlParameter("@bill_no", txtSearchRe.Text.Trim), _
                                        New SqlParameter("@site_id", Session("ssRoleName")))

                Return myReader
            Catch ex As Exception
                Return Nothing
            End Try
        End Function

        Function SearchReceriptDS() As DataSet
            Try
                Dim ds As New DataSet
                objConn = New SqlConnection(strEDIConn)
                ds = SqlHelper.ExecuteDataset(objConn, CommandType.StoredProcedure, "sp_Zero_search_NewDS", _
                                        New SqlParameter("@bill_no", txtSearch.Text.Trim), _
                                        New SqlParameter("@site_id", Session("ssRoleName")))

                Return ds
            Catch ex As Exception
                Return Nothing
            End Try
        End Function

#Region "ByTine 22-07-2559 ใบเสร็จเหลือง"

        Function SearchReceript_V2() As SqlDataReader
            Try
                objConn = New SqlConnection(strEDIConn)
                myReader = SqlHelper.ExecuteReader(objConn, CommandType.StoredProcedure, "sp_Zero_search_NewDS_v2", _
                                        New SqlParameter("@bill_no", txtSearchRe.Text.Trim), _
                                        New SqlParameter("@site_id", Session("ssRoleName")))

                Return myReader
            Catch ex As Exception
                Return Nothing
            End Try
        End Function

        Function SearchReceriptDS_V2() As DataSet
            Try
                Dim ds As New DataSet
                objConn = New SqlConnection(strEDIConn)
                ds = SqlHelper.ExecuteDataset(objConn, CommandType.StoredProcedure, "sp_Zero_search_NewDS_v2", _
                                        New SqlParameter("@bill_no", txtSearch.Text.Trim), _
                                        New SqlParameter("@site_id", Session("ssRoleName")))

                Return ds
            Catch ex As Exception
                Return Nothing
            End Try
        End Function

        Function CheckSearchReceript_V2(ByVal _search As String) As Boolean
            Try
                objConn = New SqlConnection(strEDIConn)
                myReader = SqlHelper.ExecuteReader(objConn, CommandType.StoredProcedure, "sp_Zero_search_NewDS_v2", _
                                        New SqlParameter("@bill_no", CommonUtility.Get_StringValue(_search)), _
                                        New SqlParameter("@site_id", Session("ssRoleName")))
                If myReader.HasRows Then
                    Return True
                Else
                    Return False
                End If
            Catch ex As Exception
                Return Nothing
            End Try
        End Function

        Sub UpdateReceipt_V2()
            Try
                Dim ds As New DataSet

                Dim Fi As Integer
                If Session("ssRoleName") = "" Then
                    RadAjaxManager1.ResponseScripts.Add("window.alert('กรุณา Login เข้าสู่ระบบใหม่');")
                Else
                    If CommonUtility.Get_StringValue(txtsearch2.Text.Trim) <> "" Then
                        If CheckSearchReceript_V2(CommonUtility.Get_StringValue(txtsearch2.Text)) = True Then
                            RadAjaxManager1.ResponseScripts.Add("window.alert('รายการซ้ำ เนื่องจากมีรายการอยู่แล้ว');")
                        Else
                            If SearchReceriptDS_V2.Tables(0).Rows.Count > 0 Then
                                For Fi = 0 To SearchReceriptDS_V2.Tables(0).Rows.Count - 1
                                    ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, "sp_Zero_insert_NewDS_v2", _
                                                                                               New SqlParameter("@bill_no", CommonUtility.Get_StringValue(txtsearch2.Text.Trim)), _
                                                                                               New SqlParameter("@INVH_RUN_AUTO", SearchReceriptDS_V2.Tables(0).Rows(Fi).Item("INVH_RUN_AUTO").ToString), _
                                                                                               New SqlParameter("@reference_code1", SearchReceriptDS_V2.Tables(0).Rows(Fi).Item("reference_code1").ToString), _
                                                                                               New SqlParameter("@reference_code2", SearchReceriptDS_V2.Tables(0).Rows(Fi).Item("reference_code2").ToString), _
                                                                                               New SqlParameter("@total_set", "0"), _
                                                                                               New SqlParameter("@set_price", CommonUtility.Get_Decimal("30")), _
                                                                                               New SqlParameter("@amt", CommonUtility.Get_Decimal("0")), _
                                                                                               New SqlParameter("@amt_bahttext", CommonUtility.Get_StringValue("(ศูนย์บาทถ้วน) ระบบขัดข้อง")), _
                                                                                               New SqlParameter("@rpt_set", "1"), _
                                                                                               New SqlParameter("@site_id", Session("ssRoleName")))
                                Next

                                If ds.Tables(0).Rows(0).Item("retStatus") = 0 Then
                                    'ClientScript.RegisterStartupScript(Page.GetType(), "mykey", "CloseAndRebind('navigateToInserted');", True)
                                    RadAjaxManager1.ResponseScripts.Add("window.alert('บันทึกเพิ่มข้อมูลใบเสร็จเรียบร้อยแล้ว');")
                                Else
                                    lbl_ErrMSG.Text = "ไม่สามารถบันทึกได้"
                                End If
                            Else
                                RadAjaxManager1.ResponseScripts.Add("window.alert('กรุณาป้อน เลขที่ใบเสร็จที่ออกใบเสร็จสมบูรณ์ ด้วยเพื่อใช้ในการอ้างถึงข้อมูล');")
                            End If
                        End If

                    Else
                        RadAjaxManager1.ResponseScripts.Add("window.alert('กรุณาระบุ เลขที่ใบเสร็จที่ออกไม่ได้หรือไม่มีรายการในใบเสร็จ');")
                    End If
                End If

            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End Sub

#End Region

        Private Sub GridZero_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles GridZero.NeedDataSource
            ''ByTine 22-07-2559 0 = ใบเสร็จเขียว / 1= ใบเสร็จเหลือง
            If rblRecieptType.SelectedValue = 0 Then
                GridZero.DataSource = SearchReceript()
            ElseIf rblRecieptType.SelectedValue = 1 Then
                GridZero.DataSource = SearchReceript_V2()
            End If
        End Sub

        Function CheckSearchReceript(ByVal _search As String) As Boolean
            Try
                objConn = New SqlConnection(strEDIConn)
                myReader = SqlHelper.ExecuteReader(objConn, CommandType.StoredProcedure, "sp_Zero_search_NewDS", _
                                        New SqlParameter("@bill_no", CommonUtility.Get_StringValue(_search)), _
                                        New SqlParameter("@site_id", Session("ssRoleName")))
                If myReader.HasRows Then
                    Return True
                Else
                    Return False
                End If
            Catch ex As Exception
                Return Nothing
            End Try
        End Function

        Sub UpdateReceipt()
            Try
                Dim ds As New DataSet

                Dim Fi As Integer
                If Session("ssRoleName") = "" Then
                    RadAjaxManager1.ResponseScripts.Add("window.alert('กรุณา Login เข้าสู่ระบบใหม่');")
                Else
                    If CommonUtility.Get_StringValue(txtsearch2.Text.Trim) <> "" Then
                        If CheckSearchReceript(CommonUtility.Get_StringValue(txtsearch2.Text)) = True Then
                            RadAjaxManager1.ResponseScripts.Add("window.alert('รายการซ้ำ เนื่องจากมีรายการอยู่แล้ว');")
                        Else
                            If SearchReceriptDS.Tables(0).Rows.Count > 0 Then
                                For Fi = 0 To SearchReceriptDS.Tables(0).Rows.Count - 1
                                    ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, "sp_Zero_insert_NewDS", _
                                                                                               New SqlParameter("@bill_no", CommonUtility.Get_StringValue(txtsearch2.Text.Trim)), _
                                                                                               New SqlParameter("@INVH_RUN_AUTO", SearchReceriptDS.Tables(0).Rows(Fi).Item("INVH_RUN_AUTO").ToString), _
                                                                                               New SqlParameter("@reference_code1", SearchReceriptDS.Tables(0).Rows(Fi).Item("reference_code1").ToString), _
                                                                                               New SqlParameter("@reference_code2", SearchReceriptDS.Tables(0).Rows(Fi).Item("reference_code2").ToString), _
                                                                                               New SqlParameter("@total_set", "0"), _
                                                                                               New SqlParameter("@set_price", CommonUtility.Get_Decimal("30")), _
                                                                                               New SqlParameter("@amt", CommonUtility.Get_Decimal("0")), _
                                                                                               New SqlParameter("@amt_bahttext", CommonUtility.Get_StringValue("(ศูนย์บาทถ้วน) ระบบขัดข้อง")), _
                                                                                               New SqlParameter("@rpt_set", "1"), _
                                                                                               New SqlParameter("@site_id", Session("ssRoleName")))
                                Next

                                If ds.Tables(0).Rows(0).Item("retStatus") = 0 Then
                                    'ClientScript.RegisterStartupScript(Page.GetType(), "mykey", "CloseAndRebind('navigateToInserted');", True)
                                    RadAjaxManager1.ResponseScripts.Add("window.alert('บันทึกเพิ่มข้อมูลใบเสร็จเรียบร้อยแล้ว');")
                                Else
                                    lbl_ErrMSG.Text = "ไม่สามารถบันทึกได้"
                                End If
                            Else
                                RadAjaxManager1.ResponseScripts.Add("window.alert('กรุณาป้อน เลขที่ใบเสร็จที่ออกใบเสร็จสมบูรณ์ ด้วยเพื่อใช้ในการอ้างถึงข้อมูล');")
                            End If
                        End If

                    Else
                        RadAjaxManager1.ResponseScripts.Add("window.alert('กรุณาระบุ เลขที่ใบเสร็จที่ออกไม่ได้หรือไม่มีรายการในใบเสร็จ');")
                    End If
                End If

            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End Sub

        Protected Sub btnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
            ''ByTine 22-07-2559 0 = ใบเสร็จเขียว / 1= ใบเสร็จเหลือง
            If rblRecieptType.SelectedValue = 0 Then
                UpdateReceipt()
            ElseIf rblRecieptType.SelectedValue = 1 Then
                UpdateReceipt_V2()
            End If

            GridZero.Rebind()
        End Sub

    End Class

End Namespace
