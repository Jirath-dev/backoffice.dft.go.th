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
Imports Sign_GovClass
Partial Public Class ctrl_AllEditSign
    Inherits Entities.Modules.PortalModuleBase

    Dim strEDIConn As String = ConfigurationManager.ConnectionStrings("OriginConnection").ConnectionString
    Dim PathName As String = ""

    Dim PathSiteAll_page As String = ""
    Protected SiteUrl As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        PathSiteAll_page = ConfigurationManager.AppSettings("pathWebconfigUppicGov").ToString()
        SiteUrl = "http://" & DotNetNuke.Common.GetDomainName(Request)
        txtTemp_Gov_action.Text = Request.QueryString("Gov_action")
        If Not Page.IsPostBack Then
            CallSiteName(Request.QueryString("Gov_action"), Request.QueryString("UserLog"), Request.QueryString("SiteidLog"))

            'check case new หรือ edit
            Select Case Request.QueryString("Gov_action")
                Case "GovNew"
                    chb_Noimage.Visible = False
                    BtnEdit.Visible = False
                    BtnDel.Visible = False

                    'ซ่อนไว้ตอนเพิ่มใหม่ไม่ใช้งานส่วนนี้
                    PanelStatus.Visible = False
                Case "GovEdit"
                    chb_Noimage.Visible = True
                    'Call_setControledit(RadCbbUser.Text, RadCbbSite.SelectedValue)
                    'LoadUserNameSign()
                    Call_user2(RadCbbUser.Text, RadCbbSite.SelectedValue, Request.QueryString("Gov_action"))
                    BtnAdd.Visible = False
                    If (RadCbbUser.Text <> "ไม่พบรายชื่อเจ้าหน้าที่ที่อยู่ในสาขาที่เลือก" And RadCbbUser.Text <> "กรุณาเลือกชื่อ User เจ้าหน้าที่" And RadCbbUser.Text <> "") = True Then
                        BtnEdit.Enabled = True
                        BtnDel.Enabled = True

                        Select Case Mid(Request.QueryString("UserLog"), 1, 5).ToUpper
                            Case "ADMIN"
                                PanelStatus.Visible = True
                            Case Else
                                PanelStatus.Visible = False
                        End Select
                    Else
                        BtnEdit.Enabled = False
                        BtnDel.Enabled = False

                        PanelStatus.Visible = False
                    End If
            End Select
            'รูป defalut
            Image1.ImageUrl = PathSiteAll_page & "no-image.png"

            _dataAllForm()
        End If
        enable_check(txtTemp_Gov_action.Text)
    End Sub

#Region "Form List เนื่องจากลายเซ็นต์ มีหลายแบบ แล้วแต่ฟอร์ม"
    Sub _dataAllForm()
        Dim ds As New DataSet
        Dim sql As String = ""
        sql = "SELECT     * FROM         form_type ORDER BY ShowOrder"

        ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.Text, sql)

        CheckBoxListFormType.DataSource = ds.Tables(0)
        CheckBoxListFormType.DataBind()
    End Sub

#End Region
#Region "Call Site edi"
    Sub CallSiteName(ByVal _QAction As String, ByVal _QUserid As String, ByVal _QSiteid As String)
        Dim ds As DataSet

        Select Case Mid(_QUserid, 1, 5).ToUpper
            Case "ADMIN"
                ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, "vi_GetSite_images_NewDS2")
            Case Else
                ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, "vi_GetSite_imagesBySiteid_NewDS2", _
                                    New SqlParameter("@site_id", _QSiteid))
        End Select

        If ds.Tables(0).Rows.Count > 0 Then
            RadCbbSite.DataSource = ds.Tables(0)
            RadCbbSite.DataBind()
            Select Case _QAction
                Case "GovNew"
                    getUserInfo(RadCbbSite.Items(0).Value)
                    RadComboBox_UserNameTemp.Visible = False
                    txtUserNameTemp.Visible = True
                    LabelDisplay.Visible = True
                Case "GovEdit"
                    LoadUserNameSign(RadCbbSite.Items(0).Value, Request.QueryString("UserLog"))
                    RadComboBox_UserNameTemp.Visible = False
                    txtUserNameTemp.Visible = False
                    LabelDisplay.Visible = False
            End Select
        End If
    End Sub
#End Region

#Region "get user ใน nuke ตาม site"
    Private Sub getUserInfo(ByVal bySites As String)
        Dim objRoleController As DotNetNuke.Security.Roles.RoleController = New DotNetNuke.Security.Roles.RoleController()
        Dim listroles As ArrayList = objRoleController.GetUsersByRoleName(PortalId, bySites)
        Dim liUserInfo As New List(Of Object)

        Dim char_arr As Array
        char_arr = CheckArrayuserBysite(bySites, Request.QueryString("UserLog")).Split(";")
        For Each i As Object In listroles
            Dim objuser As DotNetNuke.Entities.Users.UserInfo = CType(i, DotNetNuke.Entities.Users.UserInfo)
            Select Case objuser.Username
                Case "F04", "EDI.Admin", "edi.st001", "edi.st002", "edi.st001t"

                Case Else
                    ' ''check เปรียบเทียบกัน ระหว่าง user nuke กับ user ที่ upload ถ้ามี user ที่ upload แล้วไม่เอามาเพิ่ม
                    ''Dim inum_if As Integer = 0
                    ''For ii As Integer = 0 To char_arr.Length - 1
                    ''    If objuser.Username <> char_arr(ii).ToString() Then
                    ''        inum_if += 1
                    ''    End If
                    ''Next
                    ''If char_arr.Length = inum_if Then
                    ''    liUserInfo.Add(objuser)
                    ''End If
                    liUserInfo.Add(objuser)
            End Select

        Next i
        RadCbbUser.Items.Clear()
        Dim radComboBoxItem As New Telerik.Web.UI.RadComboBoxItem

        RadCbbUser.DataSource = liUserInfo
        RadCbbUser.DataTextField = "Username"
        RadCbbUser.DataValueField = "UserID"
        RadCbbUser.DataBind()
        If liUserInfo.Count = 0 Then
            radComboBoxItem.Text = "ไม่พบรายชื่อเจ้าหน้าที่ที่อยู่ในสาขาที่เลือก"
        Else
            radComboBoxItem.Text = "กรุณาเลือกชื่อ User เจ้าหน้าที่"
        End If

        RadCbbUser.Items.Insert(0, radComboBoxItem)

    End Sub
#End Region

#Region "set control"
    Sub Call_setControl(ByVal _UserID As String, ByVal _SiteID As String, ByVal _ChangeStatus As String, ByVal _UserRun As String)
        Dim ds As DataSet

        Select Case Mid(_ChangeStatus, 1, 5).ToUpper
            Case "ADMIN"
                ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, "vi_GetByChange_ID_GovernmentSign_NewDS2", _
                                       New SqlParameter("@SiteUser", _SiteID), _
                                       New SqlParameter("@AutoRunid", _UserRun))
                'ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, "vi_GetByChange_ID_GovernmentSign_NewDS2", _
                '                       New SqlParameter("@UserName", _UserID), _
                '                       New SqlParameter("@SiteUser", _SiteID), _
                '                       New SqlParameter("@AutoRunid", _UserRun))
            Case Else
                ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, "vi_GetBy_ID_GovernmentSign_NewDS2", _
                                       New SqlParameter("@UserNameTemp", _UserID), _
                                       New SqlParameter("@SiteUser", _SiteID))
        End Select


        If ds.Tables(0).Rows.Count > 0 Then
            RadCbbSite.Text = CommonUtility.Get_StringValue(ds.Tables(0).Rows.Item(0).Item("SiteUser"))
            RadCbbUser.Text = CommonUtility.Get_StringValue(ds.Tables(0).Rows.Item(0).Item("UserName"))
            txtRemark.Text = CommonUtility.Get_StringValue(ds.Tables(0).Rows.Item(0).Item("Remark"))

            rdolistStatus.SelectedValue = CommonUtility.Get_StringValue(ds.Tables(0).Rows.Item(0).Item("StatusUse"))

            Image1.ImageUrl = PathSiteAll_page & ds.Tables(0).Rows(0).Item("SiteUser") & "\" & ds.Tables(0).Rows(0).Item("UserName") & ds.Tables(0).Rows(0).Item("FileStr") & IIf(CInt(ds.Tables(0).Rows(0).Item("FileNum")) = 1, "", ds.Tables(0).Rows(0).Item("FileNum")) & ds.Tables(0).Rows(0).Item("FileNameGov")
            lbl_ErrMSG.Text = ""
        Else
            lbl_ErrMSG.Text = "ไม่พบข้อมูลการ Upload ลายเซ็นเจ้าหน้าที่"
            Image1.ImageUrl = PathSiteAll_page & "no-image.png"
        End If
    End Sub
    Sub Call_setControledit(ByVal _UserID As String, ByVal _SiteID As String, ByVal _ChangeStatus As String, ByVal _UserRun As String)
        Dim ds As DataSet

        Select Case Mid(_ChangeStatus, 1, 5).ToUpper
            Case "ADMIN"
                ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, "vi_GetByChange_ID_GovernmentSign_NewDS2", _
                                       New SqlParameter("@SiteUser", _SiteID), _
                                       New SqlParameter("@AutoRunid", _UserRun))
                'ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, "vi_GetByChange_ID_GovernmentSign_NewDS2", _
                '                       New SqlParameter("@UserName", _UserID), _
                '                       New SqlParameter("@SiteUser", _SiteID), _
                '                       New SqlParameter("@AutoRunid", _UserRun))
            Case Else
                ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, "vi_GetBy_ID_GovernmentSign_NewDS2", _
                                       New SqlParameter("@UserNameTemp", _UserID), _
                                       New SqlParameter("@SiteUser", _SiteID))
        End Select


        If ds.Tables(0).Rows.Count > 0 Then
            RadCbbSite.Text = CommonUtility.Get_StringValue(ds.Tables(0).Rows.Item(0).Item("SiteUser"))
            RadCbbUser.Text = CommonUtility.Get_StringValue(ds.Tables(0).Rows.Item(0).Item("UserName"))
            txtRemark.Text = CommonUtility.Get_StringValue(ds.Tables(0).Rows.Item(0).Item("Remark"))

            txtFullName.Text = CommonUtility.Get_StringValue(ds.Tables(0).Rows.Item(0).Item("FullName"))
            txtTAXID.Text = CommonUtility.Get_StringValue(ds.Tables(0).Rows.Item(0).Item("TAXID"))

            txtUserNameTemp.Text = CommonUtility.Get_StringValue(ds.Tables(0).Rows.Item(0).Item("UserNameTemp"))

            rdolistStatus.SelectedValue = CommonUtility.Get_StringValue(ds.Tables(0).Rows.Item(0).Item("StatusUse"))

            Image1.ImageUrl = PathSiteAll_page & ds.Tables(0).Rows(0).Item("SiteUser") & "\" & ds.Tables(0).Rows(0).Item("UserNameTemp") & "\" & ds.Tables(0).Rows(0).Item("UserName") & ds.Tables(0).Rows(0).Item("FileStr") & IIf(CInt(ds.Tables(0).Rows(0).Item("FileNum")) = 1, "", ds.Tables(0).Rows(0).Item("FileNum")) & ds.Tables(0).Rows(0).Item("FileNameGov")
            lbl_ErrMSG.Text = ""

            '================

            Dim arr_str As Array
            Dim str_temp As String = ""
            str_temp = CommonUtility.Get_StringValue(ds.Tables(0).Rows.Item(0).Item("FormTypeCase"))
            arr_str = str_temp.Split(";")
            _dataAllForm()
            For iarr As Integer = 0 To arr_str.Length - 1
                For iL As Integer = 0 To CheckBoxListFormType.Items.Count - 1
                    If CheckBoxListFormType.Items.Item(iL).Value = arr_str(iarr) Then
                        CheckBoxListFormType.Items.Item(iL).Selected = True
                        Exit For
                    End If
                Next
            Next

            'For iarr As Integer = 0 To arr_str.Length - 1
            '    For iL As Integer = 0 To CheckBoxListFormType.Items.Count - 1
            '        If arr_str(iarr).ToString = CheckBoxListFormType.Items.Item(iL).Value Then
            '            CheckBoxListFormType.Items.Item(iL).Selected = True
            '        End If
            '    Next
            'Next
            '================
        Else
            'lbl_ErrMSG.Text = "ไม่พบข้อมูลการ Upload ลายเซ็นเจ้าหน้าที่ ต้องทำการ Upload ก่อน"
            Image1.ImageUrl = PathSiteAll_page & "no-image.png"
            BtnEdit.Enabled = False
            BtnDel.Enabled = False

        End If
    End Sub
#End Region

#Region "check userid"
    Function Check_UserIDAll(ByVal By_Tconn As SqlTransaction, ByVal _UserID As String, ByVal _SiteID As String, ByVal _ChangeStatus As String, ByVal _UserRun As String) As DataSet
        Dim m_iReturnCheckAll As New DataSet
        Dim send_Check As Integer = 0

        Select Case Mid(_ChangeStatus, 1, 5).ToUpper
            Case "ADMIN"
                m_iReturnCheckAll = SqlHelper.ExecuteDataset(By_Tconn, CommandType.StoredProcedure, "vi_GetByChange_ID_GovernmentSign_NewDS2", _
                                       New SqlParameter("@SiteUser", _SiteID), _
                                       New SqlParameter("@AutoRunid", _UserRun))
                'm_iReturnCheckAll = SqlHelper.ExecuteDataset(By_Tconn, CommandType.StoredProcedure, "vi_GetByChange_ID_GovernmentSign_NewDS2", _
                '                      New SqlParameter("@UserName", _UserID), _
                '                      New SqlParameter("@SiteUser", _SiteID), _
                '                      New SqlParameter("@AutoRunid", _UserRun))
            Case Else
                m_iReturnCheckAll = SqlHelper.ExecuteDataset(By_Tconn, CommandType.StoredProcedure, "vi_GetBy_ID_GovernmentSign_NewDS2", _
                                       New SqlParameter("@UserNameTemp", _UserID), _
                                       New SqlParameter("@SiteUser", _SiteID))
        End Select

        ''check
        'm_iReturnCheckAll = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, "vi_GetBy_ID_GovernmentSign_NewDS2", _
        '           New SqlParameter("@UserName", RadCbbUser.Text), _
        '           New SqlParameter("@SiteUser", RadCbbSite.SelectedValue))

        If m_iReturnCheckAll.Tables(0).Rows.Count > 0 Then
            Return m_iReturnCheckAll
        Else
            Return Nothing
        End If
    End Function
#End Region

#Region "Call Load UserName"
    Private Sub LoadUserNameSign(ByVal _SiteName As String, ByVal _ChangeStatus As String)
        Try
            RadCbbUser.Items.Clear()
            RadCbbUser.Text = ""
            Dim ds As New DataSet

            Select Case Mid(_ChangeStatus, 1, 5).ToUpper
                Case "ADMIN"
                    'เฉพาะ admin
                    ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, "sp_common_getGovSign_NewDS2_V2", _
                                        New SqlParameter("@SiteUser", _SiteName), _
                                        New SqlParameter("@ChangeUser", 1))
                Case Else
                    ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, "sp_common_getGovSign_NewDS2_V2", _
                                        New SqlParameter("@SiteUser", _SiteName), _
                                        New SqlParameter("@ChangeUser", 0))
            End Select
            
            Dim radComboBoxItem As New Telerik.Web.UI.RadComboBoxItem
            radComboBoxItem.Text = ""
            If ds.Tables(0).Rows.Count > 0 Then
                RadCbbUser.Items.Clear()
                RadCbbUser.DataSource = ds.Tables(0)
                RadCbbUser.DataTextField = "UserName"
                RadCbbUser.DataValueField = "AutoRunid"
                RadCbbUser.DataBind()

                radComboBoxItem.Text = "กรุณาเลือกชื่อ User เจ้าหน้าที่"
                RadCbbUser.Items.Insert(0, radComboBoxItem)
            Else
                RadCbbUser.Items.Clear()
                radComboBoxItem.Value = ""
                radComboBoxItem.Text = "ไม่พบรายชื่อเจ้าหน้าที่ที่อยู่ในสาขาที่เลือก"

                Image1.ImageUrl = PathSiteAll_page & "no-image.png"

                BtnEdit.Enabled = False
                BtnDel.Enabled = False
                btnChangeStatus.Enabled = False
                RadCbbUser.Items.Insert(0, radComboBoxItem)
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub
    Function CheckArrayuserBysite(ByVal bySite As String, ByVal byChange As String) As String
        Dim _char As String = ""
        Dim ds As DataSet

        Select Case Mid(byChange, 1, 5).ToUpper
            Case "ADMIN"
                ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, "sp_common_getGovSign_NewDS2_V2", _
                                New SqlParameter("@SiteUser", bySite), _
                                New SqlParameter("@ChangeUser", 1))
            Case Else
                ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, "sp_common_getGovSign_NewDS2_V2", _
                                New SqlParameter("@SiteUser", bySite), _
                                New SqlParameter("@ChangeUser", 0))
        End Select
        

        If ds.Tables(0).Rows.Count > 0 Then
            For ii As Integer = 0 To ds.Tables(0).Rows.Count - 1
                _char &= ds.Tables(0).Rows(ii).Item("UserName").ToString & ";"

            Next
        End If

        Return _char
    End Function
#End Region

    Protected Sub RadCbbSite_SelectedIndexChanged(ByVal o As System.Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles RadCbbSite.SelectedIndexChanged
        txtFullName.Text = ""
        txtTAXID.Text = ""
        txtUserNameTemp.Text = ""

        Select Case Request.QueryString("Gov_action")
            Case "GovNew"
                getUserInfo(RadCbbSite.SelectedValue)
            Case "GovEdit"
                chb_Noimage.Visible = True
                LoadUserNameSign(RadCbbSite.SelectedValue, Request.QueryString("UserLog"))
        End Select
        _dataAllForm()
        'getUserInfo(RadCbbSite.SelectedValue)
        '        LoadUserNameSign(RadCbbSite.SelectedValue)
        'Call_setControledit(RadCbbUser.Text, RadCbbSite.SelectedValue)
    End Sub

    Protected Sub BtnCancle_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnCancle.Click
        Response.Redirect(NavigateURL)
    End Sub

    Protected Sub BtnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnAdd.Click
        Try
            lbl_ErrMSG.Text = ""
            Dim Attachfile As String = ""
            Dim m_iReturn As Integer
            Dim str_FormTemp As String = ""
            Dim AllCase As Integer = 0
            Dim AllNumType As Integer = 0
            For itype As Integer = 0 To CheckBoxListFormType.Items.Count - 1
                If CheckBoxListFormType.Items.Item(itype).Selected = True Then
                    AllNumType += 1
                    If CheckBoxListFormType.Items(itype).Value = "ALL" Then
                        AllCase += 1
                        str_FormTemp = CheckBoxListFormType.Items(itype).Value & ";" & str_FormTemp
                    Else
                        str_FormTemp = CheckBoxListFormType.Items(itype).Value & ";" & str_FormTemp
                    End If
                End If
            Next
            'AllCase =1 แสดงว่ามีการเลือก ALL และ AllNumType > 1 แสดงว่ามีการเลือกฟอร์มอื่นด้วย
            If AllCase = 1 And AllNumType > 1 Then
                lbl_ErrMSG.Text = "ถ้าเลือกทุกฟอร์มแล้วไม่ต้องเลือกฟอร์มอื่น ให้เลือกทุกฟอร์มเพียงอย่างเดียว"
                GoTo NoSave
            End If

            If FileUploadDoc.HasFile Then
                Dim _path As String
                '_path = Server.MapPath("") & "\Portals\0\DocumentFiles\"

                _path = PathName & Server.MapPath("") & ConfigurationManager.AppSettings("pathWebconfigUppicGov").ToString()

                Dim FolderSite As String = RadCbbSite.SelectedValue & "\" & txtUserNameTemp.Text.Trim

                'สร้างตัวแปร มารับ ชื่อ
                Dim Str_UserAll As String
                If (RadCbbUser.Text <> "ไม่พบรายชื่อเจ้าหน้าที่ที่อยู่ในสาขาที่เลือก" And RadCbbUser.Text <> "กรุณาเลือกชื่อ User เจ้าหน้าที่") = True Then
                    Str_UserAll = RadCbbUser.Text
                Else
                    lbl_ErrMSG.Text = "กรุณาเลือกชื่อ User เจ้าหน้าที่"
                    GoTo NoSave
                End If
                'สร้าง Folder
                Dim _pathNew As String = Upload_image(_path, FolderSite)

                'FileUploadDoc.SaveAs(_path & FolderSite & "\" & Str_UserAll & "_" & FileUploadDoc.FileName)
                Dim PathServerFullName As String = ""
                Attachfile = FileUploadDoc.FileName

                If Not (My.Computer.FileSystem.DirectoryExists(_path & FolderSite & "\" & Str_UserAll & "_" & FileUploadDoc.FileName) = True) Then
                    '-------------------------------------------------------------------
                    m_iReturn = SqlHelper.ExecuteNonQuery(strEDIConn, CommandType.StoredProcedure, "vi_Insert_Gov_ImagesSign_NewDS2", _
                                        New SqlParameter("@UserName", RadCbbUser.Text), _
                                        New SqlParameter("@SiteUser", RadCbbSite.SelectedValue), _
                                        New SqlParameter("@CheckDate", "0"), _
                                        New SqlParameter("@Remark", txtRemark.Text), _
                                        New SqlParameter("@PathFileimage", _pathNew), _
                                        New SqlParameter("@StatusUse", "1"), _
                                        New SqlParameter("@FileNameGov", Attachfile), _
                                        New SqlParameter("@FileNum", "1"), _
                                        New SqlParameter("@FileStr", "_New_"), _
                                        New SqlParameter("@FormTypeCase", str_FormTemp), _
                                        New SqlParameter("@UserNameTemp", txtUserNameTemp.Text), _
                                        New SqlParameter("@FullName", txtFullName.Text), _
                                        New SqlParameter("@TAXID", txtTAXID.Text.Trim), _
                                        New SqlParameter("@UserBy", Request.QueryString("UserLog")))

                    If m_iReturn = 1 Then
                        PathServerFullName = _path & FolderSite & "\" & Str_UserAll & "_" & FileUploadDoc.FileName

                        FileUploadDoc.SaveAs(_path & FolderSite & "\" & Str_UserAll & "_New_" & FileUploadDoc.FileName)
                        System.IO.File.Delete(PathServerFullName)
                        Response.Redirect(NavigateURL)
                    Else
                        lbl_ErrMSG.Text = "ไม่สามารถบันทึกข้อมูลได้"
                    End If
                    'Select Case Check_Sizeimage(PathServerFullName)
                    '    Case 1 'ขนาดภาพเกิน
                    '        System.IO.File.Delete(PathServerFullName)
                    '        'DownloadFile(PathServerFullName, True)
                    '        lbl_ErrMSG.Text = "ขนาดของรูปภาพ ไม่ควรเกิน xxx KB"
                    '    Case 2 'ความกว้างและสูงไม่ได้ตามกำหนด
                    '        System.IO.File.Delete(PathServerFullName)
                    '        'DownloadFile(PathServerFullName, True)
                    '        lbl_ErrMSG.Text = "ขนาดกว้าง และสูง ไม่ควรเกิน 150x150 "
                    '    Case 3

                    '    Case Else 'เงื่อนไขอื่น ต้องดูทีหลัง
                    '        lbl_ErrMSG.Text = "เกิดข้อผิดพลาด กรุณาติดต่อเจ้าหน้าที่"
                    'End Select

                Else
                    lbl_ErrMSG.Text = "ชื่อไฟล์ซ้ำ ไม่สามารถบันทึกข้อมูลได้กรุณาเปลี่ยนชื่อไฟล์"
                End If

            Else
                lbl_ErrMSG.Text = "กรุณาเลือกไฟล์รูปภาพ"
            End If
NoSave:

        Catch ex As Exception
            lbl_ErrMSG.Text = ex.Message
        End Try
    End Sub

    Protected Sub BtnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnEdit.Click
        Dim conn As SqlConnection = New SqlConnection(strEDIConn)
        Dim Trans As SqlTransaction
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            Trans = conn.BeginTransaction()

            lbl_ErrMSG.Text = ""
            Dim str_FormTemp As String = ""
            Dim AllCase As Integer = 0
            Dim AllNumType As Integer = 0
            For itype As Integer = 0 To CheckBoxListFormType.Items.Count - 1
                If CheckBoxListFormType.Items.Item(itype).Selected = True Then
                    AllNumType += 1
                    If CheckBoxListFormType.Items(itype).Value = "ALL" Then
                        AllCase += 1
                        str_FormTemp = CheckBoxListFormType.Items(itype).Value & ";" & str_FormTemp
                    Else
                        str_FormTemp = CheckBoxListFormType.Items(itype).Value & ";" & str_FormTemp
                    End If
                End If
            Next
            'AllCase =1 แสดงว่ามีการเลือก ALL และ AllNumType > 1 แสดงว่ามีการเลือกฟอร์มอื่นด้วย
            If AllCase = 1 And AllNumType > 1 Then
                lbl_ErrMSG.Text = "ถ้าเลือกทุกฟอร์มแล้วไม่ต้องเลือกฟอร์มอื่น ให้เลือกทุกฟอร์มเพียงอย่างเดียว"
                GoTo NoSave
            End If

            Dim Attachfile As String = ""
            Dim m_iReturn As Integer
            Dim m_iReturnStatus As Integer

            'กรณีบันทึกเพื่อเพิ่มฟอร์มแก้ไขส่วนอื่นไม่เกี่ยวกับรูปลายเซ็น
            Select Case chb_Noimage.Checked
                Case False
                    If FileUploadDoc.HasFile Then
                        Dim _path As String
                        '_path = Server.MapPath("") & "\Portals\0\DocumentFiles\"

                        _path = PathName & Server.MapPath("") & ConfigurationManager.AppSettings("pathWebconfigUppicGov").ToString()

                        Dim FolderSite As String = RadCbbSite.SelectedValue & "\" & RadComboBox_UserNameTemp.Text

                        'สร้างตัวแปร มารับ ชื่อ
                        Dim Str_UserAll As String
                        If (RadCbbUser.Text <> "ไม่พบรายชื่อเจ้าหน้าที่ที่อยู่ในสาขาที่เลือก" And RadCbbUser.Text <> "กรุณาเลือกชื่อ User เจ้าหน้าที่") = True Then
                            Dim arr_t As Array = RadCbbUser.Text.Split("[")
                            Str_UserAll = arr_t(0)
                            'Str_UserAll = RadCbbUser.Text
                        Else
                            lbl_ErrMSG.Text = "กรุณาเลือกชื่อ User เจ้าหน้าที่"
                            GoTo NoSave
                        End If
                        'สร้าง Folder
                        Dim _pathNew As String = Upload_image(_path, FolderSite)

                        FileUploadDoc.SaveAs(_path & FolderSite & "\" & Str_UserAll & "_" & FileUploadDoc.FileName)
                        Dim PathServerFullName As String = ""
                        Attachfile = FileUploadDoc.FileName

                        If Not (My.Computer.FileSystem.DirectoryExists(_path & FolderSite & "\" & Str_UserAll & "_" & FileUploadDoc.FileName) = True) Then
                            '-------------------------------------------------------------------

                            'check ก่อนลบรูปภาพ
                            Dim Name_OLE As String = ""
                            Name_OLE = Str_UserAll & "_" & Attachfile
                            'If By_user <> "" Then
                            '    strnn = RadComboBox_UserNameTemp.Items.Item(1).Value
                            'Else
                            '    strnn = RadComboBox_UserNameTemp.Items.Item(0).Value
                            'End If
                            'Insert
                            m_iReturn = SqlHelper.ExecuteNonQuery(Trans, CommandType.StoredProcedure, "vi_Insert_Gov_ImagesSign_NewDS2", _
                                                New SqlParameter("@UserName", Str_UserAll), _
                                                New SqlParameter("@SiteUser", RadCbbSite.SelectedValue), _
                                                New SqlParameter("@CheckDate", "1"), _
                                                New SqlParameter("@Remark", txtRemark.Text), _
                                                New SqlParameter("@PathFileimage", _pathNew), _
                                                New SqlParameter("@StatusUse", "1"), _
                                                New SqlParameter("@FileNameGov", Attachfile), _
                                                New SqlParameter("@FileNum", CInt(Check_UserIDAll(Trans, RadComboBox_UserNameTemp.Text, RadCbbSite.SelectedValue, Request.QueryString("UserLog"), RadCbbUser.SelectedValue).Tables(0).Rows(0).Item("FileNum")) + 1), _
                                                New SqlParameter("@FileStr", "_NewUP_"), _
                                                New SqlParameter("@FormTypeCase", str_FormTemp), _
                                                New SqlParameter("@UserNameTemp", IIf(txtTemp_Gov_action.Text = "GovEdit", RadComboBox_UserNameTemp.Text, txtUserNameTemp.Text)), _
                                                New SqlParameter("@FullName", txtFullName.Text), _
                                                New SqlParameter("@TAXID", txtTAXID.Text.Trim), _
                                                New SqlParameter("@UserBy", Request.QueryString("UserLog")))

                            'update ข้อมูลเก่าแล้วเปลี่ยนสถานะ ให้เป็นลบข้อมูล
                            m_iReturnStatus = SqlHelper.ExecuteNonQuery(Trans, CommandType.StoredProcedure, "vi_UpdateStatus_GovSign_NewDS2", _
                                        New SqlParameter("@AutoRunid", RadCbbUser.SelectedValue), _
                                        New SqlParameter("@StatusUse", "3"))



                            If m_iReturn = 1 And m_iReturnStatus = 1 Then
                                PathServerFullName = _path & FolderSite & "\" & Str_UserAll & "_" & FileUploadDoc.FileName
                                Dim Num_pic As Integer = CInt(Check_UserIDAll(Trans, RadComboBox_UserNameTemp.Text, RadCbbSite.SelectedValue, Request.QueryString("UserLog"), RadCbbUser.SelectedValue).Tables(0).Rows(0).Item("FileNum"))

                                Select Case Mid(Request.QueryString("UserLog"), 1, 5).ToUpper
                                    Case "ADMIN"
                                        'ทำเพื่อไม่ลบรูปเก่าทิ้งเก็บไว้กรณี พิมพ์ย้อนหลังให้รูปเป็นรูปเดิม
                                        FileUploadDoc.SaveAs(_path & FolderSite & "\" & Str_UserAll & "_NewUP_" & Num_pic + 1 & FileUploadDoc.FileName)

                                    Case Else
                                        'ทำเพื่อไม่ลบรูปเก่าทิ้งเก็บไว้กรณี พิมพ์ย้อนหลังให้รูปเป็นรูปเดิม
                                        FileUploadDoc.SaveAs(_path & FolderSite & "\" & Str_UserAll & "_NewUP_" & Num_pic & FileUploadDoc.FileName)
                                End Select
                                ''ทำเพื่อไม่ลบรูปเก่าทิ้งเก็บไว้กรณี พิมพ์ย้อนหลังให้รูปเป็นรูปเดิม
                                'objBitmap.Save(_path & FolderSite & "\" & Str_UserAll & "_NewUP_" & IIf(Mid(Request.QueryString("UserLog"), 1, 5) = "ADMIN", Num_pic + 1, Num_pic) & FileUploadDoc.FileName, objGraphic.RawFormat)

                                System.IO.File.Delete(PathServerFullName)

                                Trans.Commit()
                                conn.Close()

                                Response.Redirect(NavigateURL)
                                Exit Sub
                            Else
                                lbl_ErrMSG.Text = "ไม่สามารถบันทึกข้อมูลได้"
                                Trans.Rollback()
                                conn.Close()
                            End If
                            'Select Case Check_Sizeimage(PathServerFullName)
                            '    Case 1 'ขนาดภาพเกิน
                            '        System.IO.File.Delete(PathServerFullName)
                            '        'DownloadFile(PathServerFullName, True)
                            '        lbl_ErrMSG.Text = "ขนาดของรูปภาพ ไม่ควรเกิน xxx KB"
                            '    Case 2 'ความกว้างและสูงไม่ได้ตามกำหนด
                            '        System.IO.File.Delete(PathServerFullName)
                            '        'DownloadFile(PathServerFullName, True)
                            '        lbl_ErrMSG.Text = "ขนาดกว้าง และสูง ไม่ควรเกิน 150x150 "
                            '    Case 3

                            '    Case Else 'เงื่อนไขอื่น ต้องดูทีหลัง
                            '        lbl_ErrMSG.Text = "เกิดข้อผิดพลาด กรุณาติดต่อเจ้าหน้าที่"
                            'End Select

                        Else
                            lbl_ErrMSG.Text = "ชื่อไฟล์ซ้ำ ไม่สามารถบันทึกข้อมูลได้กรุณาเปลี่ยนชื่อไฟล์"
                            Trans.Rollback()
                            conn.Close()
                        End If

                    Else
                        lbl_ErrMSG.Text = "กรุณาเลือกไฟล์รูปภาพ"
                    End If
                Case True

                    m_iReturn = SqlHelper.ExecuteNonQuery(Trans, CommandType.StoredProcedure, "vi_Update_Gov_NoImagesSign_NewDS2", _
                                                                    New SqlParameter("@AutoRunid", RadCbbUser.SelectedValue), _
                                                                    New SqlParameter("@SiteUser", RadCbbSite.SelectedValue), _
                                                                    New SqlParameter("@Remark", txtRemark.Text), _
                                                                    New SqlParameter("@FormTypeCase", str_FormTemp), _
                                                                    New SqlParameter("@FullName", txtFullName.Text), _
                                                                    New SqlParameter("@TAXID", txtTAXID.Text.Trim), _
                                                                    New SqlParameter("@UserBy", Request.QueryString("UserLog")))
                    If m_iReturn = 1 Then
                        Trans.Commit()
                        conn.Close()

                        Response.Redirect(NavigateURL)
                        Exit Sub
                    Else
                        lbl_ErrMSG.Text = "ไม่สามารถบันทึกข้อมูลได้"
                        Trans.Rollback()
                        conn.Close()
                    End If
            End Select

NoSave:
            Trans.Rollback()
            conn.Close()
        Catch ex As Exception
            lbl_ErrMSG.Text = ex.Message
        End Try
    End Sub
#Region "back code"
    '    Sub ccc()
    '        Dim conn As SqlConnection = New SqlConnection(strEDIConn)
    '        Dim Trans As SqlTransaction
    '        Try
    '            If conn.State = ConnectionState.Closed Then
    '                conn.Open()
    '            End If
    '            Trans = conn.BeginTransaction()

    '            lbl_ErrMSG.Text = ""
    '            Dim str_FormTemp As String = ""
    '            Dim AllCase As Integer = 0
    '            Dim AllNumType As Integer = 0
    '            For itype As Integer = 0 To CheckBoxListFormType.Items.Count - 1
    '                If CheckBoxListFormType.Items.Item(itype).Selected = True Then
    '                    AllNumType += 1
    '                    If CheckBoxListFormType.Items(itype).Value = "ALL" Then
    '                        AllCase += 1
    '                        str_FormTemp = CheckBoxListFormType.Items(itype).Value & ";" & str_FormTemp
    '                    Else
    '                        str_FormTemp = CheckBoxListFormType.Items(itype).Value & ";" & str_FormTemp
    '                    End If
    '                End If
    '            Next
    '            'AllCase =1 แสดงว่ามีการเลือก ALL และ AllNumType > 1 แสดงว่ามีการเลือกฟอร์มอื่นด้วย
    '            If AllCase = 1 And AllNumType > 1 Then
    '                lbl_ErrMSG.Text = "ถ้าเลือกทุกฟอร์มแล้วไม่ต้องเลือกฟอร์มอื่น ให้เลือกทุกฟอร์มเพียงอย่างเดียว"
    '                GoTo NoSave
    '            End If

    '            Dim Attachfile As String = ""
    '            Dim m_iReturn As Integer
    '            Dim m_iReturnStatus As Integer

    '            If FileUploadDoc.HasFile Then
    '                Dim _path As String
    '                '_path = Server.MapPath("") & "\Portals\0\DocumentFiles\"

    '                _path = PathName & Server.MapPath("") & ConfigurationManager.AppSettings("pathWebconfigUppicGov").ToString()

    '                Dim FolderSite As String = RadCbbSite.SelectedValue & "\" & RadComboBox_UserNameTemp.Text

    '                'สร้างตัวแปร มารับ ชื่อ
    '                Dim Str_UserAll As String
    '                If (RadCbbUser.Text <> "ไม่พบรายชื่อเจ้าหน้าที่ที่อยู่ในสาขาที่เลือก" And RadCbbUser.Text <> "กรุณาเลือกชื่อ User เจ้าหน้าที่") = True Then
    '                    Dim arr_t As Array = RadCbbUser.Text.Split("[")
    '                    Str_UserAll = arr_t(0)
    '                    'Str_UserAll = RadCbbUser.Text
    '                Else
    '                    lbl_ErrMSG.Text = "กรุณาเลือกชื่อ User เจ้าหน้าที่"
    '                    GoTo NoSave
    '                End If
    '                'สร้าง Folder
    '                Dim _pathNew As String = Upload_image(_path, FolderSite)

    '                FileUploadDoc.SaveAs(_path & FolderSite & "\" & Str_UserAll & "_" & FileUploadDoc.FileName)
    '                Dim PathServerFullName As String = ""
    '                Attachfile = FileUploadDoc.FileName

    '                If Not (My.Computer.FileSystem.DirectoryExists(_path & FolderSite & "\" & Str_UserAll & "_" & FileUploadDoc.FileName) = True) Then
    '                    '-------------------------------------------------------------------
    '                    'check ก่อนลบรูปภาพ
    '                    Dim Name_OLE As String = ""
    '                    Name_OLE = Str_UserAll & "_" & Attachfile

    '                    'Insert
    '                    m_iReturn = SqlHelper.ExecuteNonQuery(Trans, CommandType.StoredProcedure, "vi_Insert_Gov_ImagesSign_NewDS2", _
    '                                        New SqlParameter("@UserName", Str_UserAll), _
    '                                        New SqlParameter("@SiteUser", RadCbbSite.SelectedValue), _
    '                                        New SqlParameter("@CheckDate", "1"), _
    '                                        New SqlParameter("@Remark", txtRemark.Text), _
    '                                        New SqlParameter("@PathFileimage", _pathNew), _
    '                                        New SqlParameter("@StatusUse", "1"), _
    '                                        New SqlParameter("@FileNameGov", Attachfile), _
    '                                        New SqlParameter("@FileNum", CInt(Check_UserIDAll(Trans, RadComboBox_UserNameTemp.Text, RadCbbSite.SelectedValue, Request.QueryString("UserLog"), RadComboBox_UserNameTemp.SelectedValue).Tables(0).Rows(0).Item("FileNum")) + 1), _
    '                                        New SqlParameter("@FileStr", "_NewUP_"), _
    '                                        New SqlParameter("@FormTypeCase", str_FormTemp), _
    '                                        New SqlParameter("@UserNameTemp", IIf(txtTemp_Gov_action.Text = "GovEdit", RadComboBox_UserNameTemp.Text, txtUserNameTemp.Text)), _
    '                                        New SqlParameter("@UserBy", Request.QueryString("UserLog")))

    '                    'update ข้อมูลเก่าแล้วเปลี่ยนสถานะ ให้เป็นลบข้อมูล
    '                    m_iReturnStatus = SqlHelper.ExecuteNonQuery(Trans, CommandType.StoredProcedure, "vi_UpdateStatus_GovSign_NewDS2", _
    '                                New SqlParameter("@AutoRunid", RadCbbUser.SelectedValue), _
    '                                New SqlParameter("@StatusUse", "3"))



    '                    If m_iReturn = 1 And m_iReturnStatus = 1 Then
    '                        PathServerFullName = _path & FolderSite & "\" & Str_UserAll & "_" & FileUploadDoc.FileName

    '                        FileUploadDoc.SaveAs(_path & FolderSite & "\" & Str_UserAll & "_New_" & FileUploadDoc.FileName)

    '                        Response.Redirect(NavigateURL)

    '                        'resize=============================
    '                        Dim intWidth, intHeight As Integer

    '                        intWidth = 340 '*** Fix Width ***'
    '                        'intHeight = 0   '*** If = 0 Auto Re-Cal Size ***'
    '                        intHeight = 189

    '                        PathServerFullName = _path & FolderSite & "\" & Str_UserAll & "_" & FileUploadDoc.FileName
    '                        Dim objGraphic As System.Drawing.Image = System.Drawing.Image.FromFile(PathServerFullName)

    '                        Dim objBitmap As Drawing.Bitmap

    '                        '*** Calculate Height ***'
    '                        If intHeight > 0 Then
    '                            objBitmap = New Drawing.Bitmap(objGraphic, intWidth, intHeight)
    '                        Else
    '                            If objGraphic.Width > intWidth Then
    '                                Dim ratio As Double = objGraphic.Height / objGraphic.Width

    '                                intHeight = ratio * intWidth

    '                                objBitmap = New Drawing.Bitmap(objGraphic, intWidth, intHeight)
    '                            Else
    '                                objBitmap = New Drawing.Bitmap(objGraphic)
    '                            End If
    '                        End If

    '                        '===================================
    '                        FileUploadDoc.Dispose()
    '                        Dim Num_pic As Integer = CInt(Check_UserIDAll(Trans, RadComboBox_UserNameTemp.Text, RadCbbSite.SelectedValue, Request.QueryString("UserLog"), RadComboBox_UserNameTemp.SelectedValue).Tables(0).Rows(0).Item("FileNum"))

    '                        Select Case Mid(Request.QueryString("UserLog"), 1, 5).ToUpper
    '                            Case "ADMIN"
    '                                'ทำเพื่อไม่ลบรูปเก่าทิ้งเก็บไว้กรณี พิมพ์ย้อนหลังให้รูปเป็นรูปเดิม
    '                                objBitmap.Save(_path & FolderSite & "\" & Str_UserAll & "_NewUP_" & Num_pic + 1 & FileUploadDoc.FileName, objGraphic.RawFormat)

    '                            Case Else
    '                                'ทำเพื่อไม่ลบรูปเก่าทิ้งเก็บไว้กรณี พิมพ์ย้อนหลังให้รูปเป็นรูปเดิม
    '                                objBitmap.Save(_path & FolderSite & "\" & Str_UserAll & "_NewUP_" & Num_pic & FileUploadDoc.FileName, objGraphic.RawFormat)

    '                        End Select
    '                        ''ทำเพื่อไม่ลบรูปเก่าทิ้งเก็บไว้กรณี พิมพ์ย้อนหลังให้รูปเป็นรูปเดิม
    '                        'objBitmap.Save(_path & FolderSite & "\" & Str_UserAll & "_NewUP_" & IIf(Mid(Request.QueryString("UserLog"), 1, 5) = "ADMIN", Num_pic + 1, Num_pic) & FileUploadDoc.FileName, objGraphic.RawFormat)

    '                        objBitmap.Dispose()
    '                        objGraphic.Dispose()

    '                        System.IO.File.Delete(PathServerFullName)

    '                        Trans.Commit()
    '                        conn.Close()

    '                        Response.Redirect(NavigateURL)
    '                        Exit Sub
    '                    Else
    '                        lbl_ErrMSG.Text = "ไม่สามารถบันทึกข้อมูลได้"
    '                        Trans.Rollback()
    '                        conn.Close()
    '                    End If
    '                    'Select Case Check_Sizeimage(PathServerFullName)
    '                    '    Case 1 'ขนาดภาพเกิน
    '                    '        System.IO.File.Delete(PathServerFullName)
    '                    '        'DownloadFile(PathServerFullName, True)
    '                    '        lbl_ErrMSG.Text = "ขนาดของรูปภาพ ไม่ควรเกิน xxx KB"
    '                    '    Case 2 'ความกว้างและสูงไม่ได้ตามกำหนด
    '                    '        System.IO.File.Delete(PathServerFullName)
    '                    '        'DownloadFile(PathServerFullName, True)
    '                    '        lbl_ErrMSG.Text = "ขนาดกว้าง และสูง ไม่ควรเกิน 150x150 "
    '                    '    Case 3

    '                    '    Case Else 'เงื่อนไขอื่น ต้องดูทีหลัง
    '                    '        lbl_ErrMSG.Text = "เกิดข้อผิดพลาด กรุณาติดต่อเจ้าหน้าที่"
    '                    'End Select

    '                Else
    '                    lbl_ErrMSG.Text = "ชื่อไฟล์ซ้ำ ไม่สามารถบันทึกข้อมูลได้กรุณาเปลี่ยนชื่อไฟล์"
    '                    Trans.Rollback()
    '                    conn.Close()
    '                End If

    '            Else
    '                lbl_ErrMSG.Text = "กรุณาเลือกไฟล์รูปภาพ"
    '            End If
    'NoSave:
    '            Trans.Rollback()
    '            conn.Close()
    '        Catch ex As Exception
    '            lbl_ErrMSG.Text = ex.Message
    '        End Try
    '    End Sub
#End Region
    Protected Sub BtnDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDel.Click
        Dim m_iReturnDel As Integer
        If (RadCbbUser.Text <> "ไม่พบรายชื่อเจ้าหน้าที่ที่อยู่ในสาขาที่เลือก" And RadCbbUser.Text <> "กรุณาเลือกชื่อ User เจ้าหน้าที่") = True Then
            'update ข้อมูลเก่าแล้วเปลี่ยนสถานะ ให้เป็นลบข้อมูล
            m_iReturnDel = SqlHelper.ExecuteNonQuery(strEDIConn, CommandType.StoredProcedure, "vi_ChangeStatus_GovSign_NewDS2", _
                                    New SqlParameter("@AutoRunid", RadCbbUser.SelectedValue), _
                                    New SqlParameter("@StatusUse", "2"), _
                                    New SqlParameter("@Remark", txtRemark.Text), _
                                    New SqlParameter("@UserBy", Request.QueryString("UserLog")))

            If m_iReturnDel = 1 Then
                Response.Redirect(NavigateURL)
            Else
                Response.Write("<script type='text/javascript'> alert('เกิดข้อผิดพลาดไม่สามารถทำการลบข้อมูลได้  กรุณาติดต่อผู้ดูแลระบบ!!!')</script>")
            End If
        Else
            lbl_ErrMSG.Text = "กรุณาเลือกชื่อ User ที่ต้องการลบข้อมูล"
        End If
        
    End Sub

    Protected Sub RadCbbUser_SelectedIndexChanged(ByVal o As System.Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles RadCbbUser.SelectedIndexChanged
        If (RadCbbUser.Text <> "ไม่พบรายชื่อเจ้าหน้าที่ที่อยู่ในสาขาที่เลือก" And RadCbbUser.Text <> "กรุณาเลือกชื่อ User เจ้าหน้าที่") = True Then
            BtnEdit.Enabled = True
            BtnDel.Enabled = True
            btnChangeStatus.Enabled = True
            Select Case Mid(Request.QueryString("UserLog"), 1, 5).ToUpper
                Case "ADMIN"
                    Select Case Request.QueryString("Gov_action")
                        Case "GovNew"
                            PanelStatus.Visible = False
                            RadComboBox_UserNameTemp.Visible = False
                            txtUserNameTemp.Visible = True
                            LabelDisplay.Visible = True
                        Case Else
                            PanelStatus.Visible = True

                            txtUserNameTemp.Visible = False
                            LabelDisplay.Visible = False
                            RadComboBox_UserNameTemp.Visible = False
                    End Select

                Case Else
                    PanelStatus.Visible = False
            End Select
        Else
            BtnEdit.Enabled = False
            BtnDel.Enabled = False
            PanelStatus.Visible = False
        End If
        ''Call_setControledit(RadCbbUser.Text, RadCbbSite.SelectedValue, Request.QueryString("UserLog"), RadCbbUser.SelectedValue)
        Select Case txtTemp_Gov_action.Text
            Case "GovEdit"

                chb_Noimage.Visible = True
                Select Case RadCbbUser.Text
                    Case Is <> "ไม่พบรายชื่อเจ้าหน้าที่ที่อยู่ในสาขาที่เลือก", "กรุณาเลือกชื่อ User เจ้าหน้าที่"
                        Dim arr_ As Array = RadCbbUser.Text.Split("[")
                        Call_user2(arr_(1).ToString.Replace("]", ""), RadCbbSite.SelectedValue, txtTemp_Gov_action.Text)
                End Select
            Case Else
                Call_user2(RadCbbUser.Text, RadCbbSite.SelectedValue, txtTemp_Gov_action.Text)
        End Select
       
    End Sub
    Sub enable_check(ByVal by_case As String)
        Select Case by_case
            Case "GovEdit"
                BtnAdd.Enabled = False
                BtnEdit.Enabled = True
                BtnCancle.Enabled = True
                BtnDel.Enabled = True
            Case Else
                BtnAdd.Enabled = True
                BtnEdit.Enabled = False
                BtnCancle.Enabled = True
                BtnDel.Enabled = False
        End Select
        
    End Sub

    Sub Call_user2(ByVal By_user As String, ByVal By_Site As String, ByVal By_NewOrEdit As String)
        RadComboBox_UserNameTemp.Items.Clear()
        Dim ds As New DataSet
        Dim sql As String
        sql = "SELECT     * FROM         imGovernmentSign WHERE     (SiteUser = @SiteUser) AND (StatusUse in ('1','2')) AND (UserNameTemp = @UserNameTemp)"
        'sql = "SELECT     * FROM         imGovernmentSign WHERE     (SiteUser = @SiteUser) AND (StatusUse = '1') AND (UserNameTemp = @UserNameTemp)"
        Dim prm(1) As SqlClient.SqlParameter
        prm(0) = New SqlClient.SqlParameter("@SiteUser", By_Site)
        prm(1) = New SqlClient.SqlParameter("@UserNameTemp", By_user)
        ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.Text, sql, prm)

        Dim radComboBoxItem As New Telerik.Web.UI.RadComboBoxItem
        If ds.Tables(0).Rows.Count > 0 Then
            RadComboBox_UserNameTemp.DataSource = ds.Tables(0)
            RadComboBox_UserNameTemp.DataTextField = "UserNameTemp"
            RadComboBox_UserNameTemp.DataValueField = "AutoRunid"
            RadComboBox_UserNameTemp.DataBind()
            radComboBoxItem.Text = "กรุณาเลือกชื่อ User เจ้าหน้าที่"
        Else
            radComboBoxItem.Text = "ไม่พบรายชื่อเจ้าหน้าที่ที่อยู่ในสาขาที่เลือก"
        End If
        RadComboBox_UserNameTemp.Items.Insert(0, radComboBoxItem)

        RadComboBox_UserNameTemp.Text = By_user
        Dim strnn As String = ""
        Select Case By_NewOrEdit
            Case "GovEdit"
                If By_user <> "" Then
                    strnn = RadComboBox_UserNameTemp.Items.Item(1).Value
                Else
                    strnn = RadComboBox_UserNameTemp.Items.Item(0).Value
                End If
            Case Else
                strnn = RadComboBox_UserNameTemp.Items.Item(0).Value
        End Select

        Call_setControledit(RadComboBox_UserNameTemp.Text, RadCbbSite.SelectedValue, Request.QueryString("UserLog"), strnn)
    End Sub
    Protected Sub btnChangeStatus_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnChangeStatus.Click
        Dim m_ChangeStatus As Integer
        'update ข้อมูลเก่าแล้วเปลี่ยนสถานะ ให้เป็นลบข้อมูล
        m_ChangeStatus = SqlHelper.ExecuteNonQuery(strEDIConn, CommandType.StoredProcedure, "vi_ChangeStatus_GovSign_NewDS2", _
                    New SqlParameter("@AutoRunid", RadCbbUser.SelectedValue), _
                    New SqlParameter("@StatusUse", rdolistStatus.SelectedValue), _
                    New SqlParameter("@Remark", txtRemark.Text), _
                    New SqlParameter("@UserBy", Request.QueryString("UserLog")))

        If m_ChangeStatus = 1 Then
            Response.Redirect(NavigateURL)
        Else
            lbl_ErrMSG.Text = "ไม่สามารถเปลี่ยนสถานะได้ กรูณาติดต่อเจ้าหน้าที่"
        End If
    End Sub

    Protected Sub RadComboBox_UserNameTemp_SelectedIndexChanged(ByVal o As System.Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles RadComboBox_UserNameTemp.SelectedIndexChanged
        '        Call_setControledit(RadComboBox_UserNameTemp.Text, RadCbbSite.SelectedValue, Request.QueryString("UserLog"), RadComboBox_UserNameTemp.SelectedValue)
    End Sub

    Private Sub RadAjaxManager1_AjaxRequest(ByVal sender As Object, ByVal e As Telerik.Web.UI.AjaxRequestEventArgs) Handles RadAjaxManager1.AjaxRequest
        'Select Case Request.QueryString("Gov_action")
        '    Case "GovNew"
        '        getUserInfo(RadCbbSite.SelectedValue)
        '    Case "GovEdit"
        '        LoadUserNameSign(RadCbbSite.SelectedValue, Request.QueryString("UserLog"))
        'End Select
        '_dataAllForm()
    End Sub
End Class