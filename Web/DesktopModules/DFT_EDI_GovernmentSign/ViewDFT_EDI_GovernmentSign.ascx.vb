'
' DotNetNukeฎ - http://www.dotnetnuke.com
' Copyright (c) 2002-2012
' by DotNetNuke Corporation
'
' Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated 
' documentation files (the "Software"), to deal in the Software without restriction, including without limitation 
' the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and 
' to permit persons to whom the Software is furnished to do so, subject to the following conditions:
'
' The above copyright notice and this permission notice shall be included in all copies or substantial portions 
' of the Software.
'
' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED 
' TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL 
' THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF 
' CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
' DEALINGS IN THE SOFTWARE.
'

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

Imports System.IO
Namespace NTi.Modules.DFT_EDI_GovernmentSign

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' The ViewDFT_EDI_GovernmentSign class displays the content
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' </history>
    ''' -----------------------------------------------------------------------------
    ''' 
    Partial Class ViewDFT_EDI_GovernmentSign
        Inherits Entities.Modules.PortalModuleBase

        Dim strEDIConn As String = ConfigurationManager.ConnectionStrings("OriginConnection").ConnectionString
        Dim PathName As String = ""

        Dim PathSiteAll_page As String = ""
        Protected SiteUrl As String
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
        

#End Region

        Protected Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Me.Load
            PathSiteAll_page = ConfigurationManager.AppSettings("pathWebconfigUppicGov").ToString()
            SiteUrl = "http://" & DotNetNuke.Common.GetDomainName(Request)

            'Check ว่า User ที่ Login เข้ามาอยู่ Site ไหน
            Dim myRoleControllers As New DotNetNuke.Security.Roles.RoleController()
            Dim i As Integer = 0
            For i = 0 To MyBase.UserInfo.Roles.Length - 1
                Dim myRoleInfo As DotNetNuke.Security.Roles.RoleInfo = myRoleControllers.GetRoleByName(0, MyBase.UserInfo.Roles(i))
                Select Case myRoleInfo.RoleID
                    Case 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19
                        lblRoleID.Text = myRoleInfo.RoleName
                        Session("ssRoleName") = lblRoleID.Text
                        Exit For
                End Select
            Next i

            If Not Page.IsPostBack Then
                Session("UName") = UserInfo.Username

                CallSiteName(Session("UName"), Session("ssRoleName"))

                'รูป defalut
                Image1.ImageUrl = PathSiteAll_page & "no-image.png"
            End If

            'rgRequestUser.DataSource = LoadUserNameSignDataTable(RadCbbSite.SelectedValue, Session("UName"))
            'rgRequestUser.DataBind()
           
        End Sub

        Protected Sub RadCbbSite_SelectedIndexChanged(ByVal o As System.Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles RadCbbSite.SelectedIndexChanged
            lbl_ErrMSG.Text = ""
            getUserInfo(RadCbbSite.SelectedValue)

            If RadCbbUser.Text <> "" Then
                Call_setControl(RadCbbUser.Text, RadCbbSite.SelectedValue)
            End If


            With LoadUserNameSignDataTable(RadCbbSite.SelectedValue, Session("UName"))
                If .Tables(0).Rows.Count = 0 Then
                    lbl_ErrMSG.Text = "ไม่พบข้อมูลการ Upload ลายเซ็นเจ้าหน้าที่"
                    Image1.ImageUrl = PathSiteAll_page & "no-image.png"
                End If
                rgRequestUser.DataSource = .Tables(0)
                rgRequestUser.DataBind()
            End With
        End Sub

#Region "LoadGrid"
        Function LoadUserNameSignDataTable(ByVal _SiteName As String, ByVal _ByChange As String) As DataSet
            Try
                Dim ds As DataSet
                objConn = New SqlConnection(strEDIConn)
                Select Case Mid(_ByChange, 1, 5).ToUpper
                    Case "ADMIN"
                        ds = SqlHelper.ExecuteDataset(objConn, CommandType.StoredProcedure, "sp_common_getGovSign_NewDS2", _
                                                New SqlParameter("@SiteUser", _SiteName), _
                                                New SqlParameter("@ChangeUser", 1))
                    Case Else
                        ds = SqlHelper.ExecuteDataset(objConn, CommandType.StoredProcedure, "sp_common_getGovSign_NewDS2", _
                                                New SqlParameter("@SiteUser", _SiteName), _
                                                New SqlParameter("@ChangeUser", 0))
                End Select


                Return ds
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End Function
#End Region

#Region "Get Grid"
        Function GetSiteName_(ByVal _siteid As Object) As String
            Dim siteNameShow As String = ""

            If Not _siteid.Equals(System.DBNull.Value) = True Then
                siteNameShow = CallGet_SiteName(_siteid.ToString)
            End If

            Return siteNameShow
        End Function
        Function CallGet_SiteName(ByVal strSiteid As String) As String
            Dim ds As DataSet
            Dim reStr_site As String = ""

            ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, "vi_getSiteName_NewDS2", _
            New SqlParameter("@site_id", strSiteid))

            If ds.Tables(0).Rows.Count > 0 Then
                reStr_site = ds.Tables(0).Rows(0).Item("site_name").ToString
            End If

            Return reStr_site
        End Function

        Function GetStatus_(ByVal _Status As Object) As String
            Dim StatusShow As String = ""

            If Not _Status.Equals(System.DBNull.Value) = True Then
                Select Case _Status.ToString
                    Case "1"
                        StatusShow = "ใช้งานปรกติ"
                    Case "2"
                        StatusShow = "ถูกลบข้อมูลไว้"
                    Case "3"
                        StatusShow = "พักการใช้งาน"
                End Select
            End If

            Return StatusShow
        End Function
#End Region

#Region "Call Site edi"
        Sub CallSiteName(ByVal byUserid As String, ByVal bySiteid As String)
            Dim ds As DataSet

            'เพื่อเช็คสิทธิ์ เฉพาะ admin เท่านั้น ถึงจะเห็น ทุก site
            Select Case Mid(byUserid, 1, 5).ToUpper
                Case "ADMIN"
                    ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, "vi_GetSite_images_NewDS2")
                Case Else
                    ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, "vi_GetSite_imagesBySiteid_NewDS2", _
                    New SqlParameter("@site_id", bySiteid))
            End Select

            If ds.Tables(0).Rows.Count > 0 Then
                RadCbbSite.DataSource = ds.Tables(0)
                RadCbbSite.DataBind()

                getUserInfo(RadCbbSite.Items(0).Value)
            End If
        End Sub
#End Region

#Region "get user ใน nuke ตาม site"
        Private Sub getUserInfo(ByVal bySites As String)
            Dim ds As New DataSet
            Dim sql As String = ""
            sql = "SELECT     * FROM         imGovernmentSign WHERE     (SiteUser = @SiteUser) AND (StatusUse = '1')"
            Dim prm(0) As SqlClient.SqlParameter
            prm(0) = New SqlClient.SqlParameter("@SiteUser", bySites)
            ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.Text, sql, prm)

            Dim radComboBoxItem As New Telerik.Web.UI.RadComboBoxItem
            RadCbbUser.Items.Clear()
            RadCbbUser.ClearSelection()
            RadCbbUser.Text = String.Empty

            radComboBoxItem.Text = String.Empty
            If ds.Tables(0).Rows.Count > 0 Then
                RadCbbUser.Items.Clear()
                RadCbbUser.DataSource = ds.Tables(0)
                RadCbbUser.DataTextField = "UserNameTemp"
                RadCbbUser.DataValueField = "AutoRunid"
                RadCbbUser.DataBind()
                radComboBoxItem.Text = "กรุณาเลือกชื่อ User เจ้าหน้าที่"
            Else
                radComboBoxItem.Text = "ไม่พบรายชื่อเจ้าหน้าที่ที่อยู่ในสาขาที่เลือก"
            End If

            RadCbbUser.Items.Insert(0, radComboBoxItem)

            'Dim objRoleController As DotNetNuke.Security.Roles.RoleController = New DotNetNuke.Security.Roles.RoleController()
            'Dim listroles As ArrayList = objRoleController.GetUsersByRoleName(PortalId, bySites)
            'Dim liUserInfo As New List(Of Object)

            'For Each i As Object In listroles
            '    Dim objuser As DotNetNuke.Entities.Users.UserInfo = CType(i, DotNetNuke.Entities.Users.UserInfo)
            '    Select Case objuser.Username
            '        Case "F04", "EDI.Admin", "edi.st001", "edi.st002", "edi.st001t"

            '        Case Else
            '            liUserInfo.Add(objuser)
            '    End Select

            'Next i
        End Sub

        'Private Sub getUserInfo(ByVal bySites As String)
        '    Dim objRoleController As DotNetNuke.Security.Roles.RoleController = New DotNetNuke.Security.Roles.RoleController()
        '    Dim listroles As ArrayList = objRoleController.GetUsersByRoleName(PortalId, bySites)
        '    Dim liUserInfo As New List(Of Object)

        '    For Each i As Object In listroles
        '        Dim objuser As DotNetNuke.Entities.Users.UserInfo = CType(i, DotNetNuke.Entities.Users.UserInfo)
        '        Select Case objuser.Username
        '            Case "F04", "EDI.Admin", "edi.st001", "edi.st002", "edi.st001t"

        '            Case Else
        '                liUserInfo.Add(objuser)
        '        End Select

        '    Next i
        '    Dim radComboBoxItem As New Telerik.Web.UI.RadComboBoxItem

        '    RadCbbUser.DataSource = liUserInfo
        '    RadCbbUser.DataTextField = "Username"
        '    RadCbbUser.DataValueField = "UserID"
        '    RadCbbUser.DataBind()
        '    If liUserInfo.Count = 0 Then
        '        radComboBoxItem.Text = "ไม่พบรายชื่อเจ้าหน้าที่ที่อยู่ในสาขาที่เลือก"
        '    Else
        '        radComboBoxItem.Text = "กรุณาเลือกชื่อ User เจ้าหน้าที่"
        '    End If

        '    RadCbbUser.Items.Insert(0, radComboBoxItem)

        'End Sub

        'Sub getportaluser()
        '    Dim objUserController As DotNetNuke.Entities.Users.UserController = New DotNetNuke.Entities.Users.UserController()
        '    Dim listroles As ArrayList = objUserController.GetUsers(PortalId, True, True)
        '    Dim liUserInfo As List = New List()
        '    For Each i As Object In listroles
        '        Dim objuser As DotNetNuke.Entities.Users.UserInfo = CType(i, DotNetNuke.Entities.Users.UserInfo)
        '        liUserInfo.Add(objuser)
        '    Next i
        '    drpuserlist.DataSource = liUserInfo
        '    drpuserlist.DataTextField = "Username"
        '    drpuserlist.DataValueField = "UserID"
        '    drpuserlist.DataBind()
        'End Sub
        'Sub roleByname()
        '    Dim ObjRoleController As RoleController = New RoleController()
        '    Dim OBjRoleInfo As RoleInfo = ObjRoleController.GetRoleByName(Me.PortalId, drproledropdown.SelectedValue.ToString())
        '    ObjRoleController.AddUserRole(Me.PortalId, Me.UserId, OBjRoleInfo.RoleID, DateTime.Now, DateTime.Now.AddYears(100))
        '    Dim flag As String = drproledropdown.SelectedValue.ToString()
        '    Select Case flag
        '        Case "Practice_Admin"
        '            SessionManager.eH_ControlName = "SigningUpPractice.ascx"
        '        Case "Patients"
        '            SessionManager.eH_ControlName = "PatientRegistration.ascx"

        '    End Select


        'End Sub
#End Region

#Region "set control"
        Sub Call_setControl(ByVal _UserID As String, ByVal _SiteID As String)
            Dim ds As DataSet

            'check
            ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, "vi_GetBy_ID_GovernmentSign_NewDS2", _
                       New SqlParameter("@UserNameTemp", _UserID), _
                       New SqlParameter("@SiteUser", _SiteID))
            lbl_ErrMSG.Text = ""
            If ds.Tables(0).Rows.Count > 0 Then
                RadCbbSite.Text = CommonUtility.Get_StringValue(ds.Tables(0).Rows.Item(0).Item("SiteUser"))
                RadCbbUser.Text = CommonUtility.Get_StringValue(ds.Tables(0).Rows.Item(0).Item("UserName"))

                Image1.ImageUrl = PathSiteAll_page & ds.Tables(0).Rows(0).Item("SiteUser") & "\" & ds.Tables(0).Rows(0).Item("UserNameTemp") & "\" & ds.Tables(0).Rows(0).Item("UserName") & ds.Tables(0).Rows(0).Item("FileStr") & IIf(CInt(ds.Tables(0).Rows(0).Item("FileNum")) = 1, "", ds.Tables(0).Rows(0).Item("FileNum")) & ds.Tables(0).Rows(0).Item("FileNameGov")
                lbl_ErrMSG.Text = ""
            Else
                'lbl_ErrMSG.Text = "ไม่พบข้อมูลการ Upload ลายเซ็นเจ้าหน้าที่"
                Image1.ImageUrl = PathSiteAll_page & "no-image.png"
            End If
        End Sub
        Function Re_status(ByVal By_status_id) As String
            Dim temp_ As String = ""
            Select Case By_status_id
                Case "1"
                    temp_ = "เปิดใช้งาน"
                Case "2"
                    temp_ = "ระงับใช้งาน"
                Case "3"
                    temp_ = "ลบข้อมูล"
            End Select

            Return temp_
        End Function
#End Region

        Protected Sub RadCbbUser_SelectedIndexChanged(ByVal o As System.Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles RadCbbUser.SelectedIndexChanged
            Call_setControl(RadCbbUser.Text, RadCbbSite.SelectedValue)
        End Sub

        Protected Sub BtnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnAdd.Click
            Response.Redirect(EditUrl("ctrl_AllEditSign") & "&Gov_action=GovNew" & "&UserLog=" & Session("UName") & "&SiteidLog=" & Session("ssRoleName"))
        End Sub

        Protected Sub BtnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnEdit.Click
            Response.Redirect(EditUrl("ctrl_AllEditSign") & "&Gov_action=GovEdit" & "&UserLog=" & Session("UName") & "&SiteidLog=" & Session("ssRoleName"))
        End Sub

        Private Sub rgRequestUser_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles rgRequestUser.DataBound
            objConn.Close()
        End Sub

        Private Sub rgRequestUser_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgRequestUser.ItemDataBound
            If Not (e.Item.FindControl("lblIndex") Is Nothing) Then
                Dim lbl As Label = e.Item.FindControl("lblIndex")
                lbl.Text = (rgRequestUser.MasterTableView.CurrentPageIndex * rgRequestUser.MasterTableView.PageSize) + (e.Item.ItemIndex + 1)
            End If
        End Sub

        Private Sub rgRequestUser_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgRequestUser.NeedDataSource
            rgRequestUser.DataSource = LoadUserNameSignDataTable(RadCbbSite.SelectedValue, Session("UName"))
        End Sub

        Private Sub rgRequestUser_PageIndexChanged(ByVal source As Object, ByVal e As Telerik.Web.UI.GridPageChangedEventArgs) Handles rgRequestUser.PageIndexChanged
            rgRequestUser.CurrentPageIndex = e.NewPageIndex
            rgRequestUser.DataSource = LoadUserNameSignDataTable(RadCbbSite.SelectedValue, Session("UName"))
            rgRequestUser.DataBind()
        End Sub
    End Class

End Namespace
