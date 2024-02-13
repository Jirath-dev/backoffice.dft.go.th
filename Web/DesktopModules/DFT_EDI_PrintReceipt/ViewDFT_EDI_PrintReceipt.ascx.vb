Imports System.Web.UI
Imports System.Collections.Generic
Imports System.Reflection

Imports DotNetNuke
Imports DotNetNuke.Services.Exceptions
Imports DotNetNuke.Services.Localization

Imports System.Data.SqlClient
Imports DFT.Dotnetnuke.ClassLibrary
Imports Microsoft.ApplicationBlocks.Data
Imports Telerik.Web.ui

Namespace NTi.Modules.DFT_EDI_PrintReceipt
    Partial Class ViewDFT_EDI_PrintReceipt
        Inherits Entities.Modules.PortalModuleBase
        Dim strRegConn As String = ConfigurationManager.ConnectionStrings("RegisBackConnection").ConnectionString
        Dim strEDIConn As String = ConfigurationManager.ConnectionStrings("OriginConnection").ConnectionString
        Dim myReader As SqlDataReader = Nothing
        Dim objConn As SqlConnection = Nothing

        Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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

                Dim ISRoleDS As Boolean = UserInfo.IsInRole("EDI_DS")
                Session("_RoleDS") = ISRoleDS
                tblCardDetail.Visible = False
                tblRefList.Visible = False
                txtSearch.Focus()
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
        Protected Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
            Try
                Dim dr As SqlDataReader
                dr = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "sp_check_All", _
                New SqlParameter("@card_id", txtSearch.Text.Trim()))
                If dr.Read() Then
                    Select Case CommonUtility.Get_Int32(dr.Item("retStatus"))
                        Case 0, -2, -3, -4, -5, -6
                            tblCardDetail.Visible = True
                            tblRefList.Visible = True

                            txtCompanyName_Eng.Text = CommonUtility.Get_StringValue(dr.Item("company_eng"))
                            txtCompany_Taxno.Text = CommonUtility.Get_StringValue(dr.Item("company_taxno"))
                            txtCompany_BranchNo.Text = CommonUtility.Get_StringValue(dr.Item("company_BranchNo"))
                            txtCompany_Address.Text = CommonUtility.Get_StringValue(dr.Item("address_eng"))
                            txtCompany_Phone.Text = CommonUtility.Get_StringValue(dr.Item("phone_no"))
                            txtCompany_Fax.Text = CommonUtility.Get_StringValue(dr.Item("fax_no"))

                            txtAuthorize1.Text = CommonUtility.Get_StringValue(dr.Item("authorize1"))
                            txtAuthorize4.Text = CommonUtility.Get_StringValue(dr.Item("authorize1"))

                            txtAuthorize2.Text = CommonUtility.Get_StringValue(dr.Item("authorize2"))
                            txtAuthorize5.Text = CommonUtility.Get_StringValue(dr.Item("authorize2"))

                            txtAuthorize3.Text = CommonUtility.Get_StringValue(dr.Item("authorize3"))
                            txtAuthorize6.Text = CommonUtility.Get_StringValue(dr.Item("authorize3"))

                            dr.Close()

                            rgReceiptList.Rebind()
                            rgReceiptListNew.Rebind()
                            Session("ssCompanyName_Eng") = txtCompanyName_Eng.Text

                            Dim _Count As Integer = 0
                            Dim myDataGridItem As GridDataItem
                            Dim runAuto, runAuto_v2 As String
                            For Each myDataGridItem In rgReceiptList.MasterTableView.Items
                                If myDataGridItem.Item("totalPrintPage").Text <> "0" Then
                                    _Count = _Count + 1
                                    CType(myDataGridItem.FindControl("chkSelect"), CheckBox).Checked = True
                                    If runAuto = "" Then
                                        runAuto = myDataGridItem.Item("invh_run_auto").Text
                                    Else
                                        runAuto &= "," & myDataGridItem.Item("invh_run_auto").Text
                                    End If
                                End If
                            Next

                            For Each myDataGridItem In rgReceiptListNew.MasterTableView.Items
                                If myDataGridItem.Item("totalPrintPage").Text <> "0" Then
                                    _Count = _Count + 1
                                    CType(myDataGridItem.FindControl("chkSelect"), CheckBox).Checked = True
                                    If runAuto_v2 = "" Then
                                        runAuto_v2 = myDataGridItem.Item("invh_run_auto").Text
                                    Else
                                        runAuto_v2 &= "," & myDataGridItem.Item("invh_run_auto").Text
                                    End If
                                End If
                            Next

                            Session("invh_run") = runAuto
                            Session("invh_run_v2") = runAuto_v2

                            lblRecords_Count.Text = "จำนวนรายการทั้งหมด : " & CType(_Count, String) & "  รายการ"

                            ''ByTine 05-01-2559 ปรับใบเสร็จแบบใหม่
                            RadTabStrip2.Tabs.Item(0).Text = "ใบเสร็จสวัสดิการ (ใบเสร็จเขียว)" & " (" & rgReceiptList.MasterTableView.Items.Count & ") "
                            RadTabStrip2.Tabs.Item(1).Text = "ใบเสร็จกองทุน (ใบเสร็จเหลือง)" & " (" & rgReceiptListNew.MasterTableView.Items.Count & ") "
                            ''RadTabStrip2.Tabs.Item(1).Selected = True
                        Case -1
                            tblCardDetail.Visible = False
                            tblRefList.Visible = False

                            Me.RadAjaxManager1.ResponseScripts.Add("window.alert('" & CommonUtility.Get_StringValue(dr.Item("retMessage")) & "');")
                        Case Else
                            tblCardDetail.Visible = False
                            tblRefList.Visible = False

                            Me.RadAjaxManager1.ResponseScripts.Add("window.alert('" & CommonUtility.Get_StringValue(dr.Item("retMessage")) & "');")
                    End Select
                End If
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End Sub

        'Load รายการคำร้อง
        Function LoadRequestFormForPrint() As SqlDataReader
            objConn = New SqlConnection(strEDIConn)
            myReader = SqlHelper.ExecuteReader(objConn, CommandType.StoredProcedure, "sp_common_form_edi_getForReceipt_NewDS2_V3",
                    New SqlParameter("@CARD_ID", txtSearch.Text.Trim.Replace(" ", "")),
                    New SqlParameter("@FORM_TYPE", "ALL"),
                    New SqlParameter("@FROM_DATE", "20000101"),
                    New SqlParameter("@TO_DATE", "25001231"),
                    New SqlParameter("@INVOICE_NO", ""),
                    New SqlParameter("@SITE_ID", lblRoleID.Text))

            Return myReader
        End Function

        'ByTine 06-01-2558 Load รายการคำร้องสำหรับใบเสร็จใหม่
        Function LoadRequestFormForPrint_NewReceipt() As SqlDataReader
            objConn = New SqlConnection(strEDIConn)
            myReader = SqlHelper.ExecuteReader(objConn, CommandType.StoredProcedure, "sp_common_form_edi_getForReceipt_NewDS2_V4",
                    New SqlParameter("@CARD_ID", txtSearch.Text.Trim.Replace(" ", "")),
                    New SqlParameter("@FORM_TYPE", "ALL"),
                    New SqlParameter("@FROM_DATE", "20000101"),
                    New SqlParameter("@TO_DATE", "25001231"),
                    New SqlParameter("@INVOICE_NO", ""),
                    New SqlParameter("@SITE_ID", lblRoleID.Text))

            Return myReader
        End Function

        Protected Function IsRoleName(ByVal str_rolename As String) As Boolean
            Dim ret As Boolean = False
            Dim myRoleControllers As New DotNetNuke.Security.Roles.RoleController()
            Dim i As Integer = 0
            Dim _UserInfo As New Users.UserInfo
            For i = 0 To UserInfo.Roles.Length - 1
                Dim myRoleInfo As DotNetNuke.Security.Roles.RoleInfo = myRoleControllers.GetRoleByName(0, UserInfo.Roles(i))

                If myRoleInfo.RoleName = str_rolename Then
                    ret = True
                    Exit For
                End If
            Next i
            Return ret
        End Function

        Protected Sub ToggleRowSelection(ByVal sender As Object, ByVal e As EventArgs)
            Dim runAuto, runAuto_v2 As String
            Session("invh_run") = Nothing
            CType(CType(sender, CheckBox).Parent.Parent, GridItem).Selected = CType(sender, CheckBox).Checked
            For Each dataItem As GridDataItem In rgReceiptList.MasterTableView.Items
                If CType(dataItem.FindControl("chkSelect"), CheckBox).Checked = True Then
                    If runAuto = "" Then
                        runAuto = dataItem.OwnerTableView.DataKeyValues(dataItem.ItemIndex)("invh_run_auto")
                    Else
                        runAuto = runAuto & "," & dataItem.OwnerTableView.DataKeyValues(dataItem.ItemIndex)("invh_run_auto")
                    End If
                End If
            Next
            Session("invh_run") = runAuto

            Session("invh_run_v2") = Nothing
            CType(CType(sender, CheckBox).Parent.Parent, GridItem).Selected = CType(sender, CheckBox).Checked
            For Each dataItem As GridDataItem In rgReceiptListNew.MasterTableView.Items
                If CType(dataItem.FindControl("chkSelect"), CheckBox).Checked = True Then
                    If runAuto_v2 = "" Then
                        runAuto_v2 = dataItem.OwnerTableView.DataKeyValues(dataItem.ItemIndex)("invh_run_auto")
                    Else
                        runAuto_v2 = runAuto_v2 & "," & dataItem.OwnerTableView.DataKeyValues(dataItem.ItemIndex)("invh_run_auto")
                    End If
                End If
            Next
            Session("invh_run_v2") = runAuto_v2

        End Sub

        Private Sub rgReceiptList_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles rgReceiptList.DataBound
            myReader.Close()
            objConn.Close()
        End Sub

        Private Sub RadAjaxManager1_AjaxRequest(ByVal sender As Object, ByVal e As Telerik.Web.UI.AjaxRequestEventArgs) Handles RadAjaxManager1.AjaxRequest
            rgReceiptList.Rebind()
            rgReceiptListNew.Rebind()
        End Sub

        Private Sub rgReceiptList_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgReceiptList.NeedDataSource
            rgReceiptList.DataSource = LoadRequestFormForPrint()
        End Sub


        Private Sub rgReceiptListNew_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgReceiptListNew.NeedDataSource
            rgReceiptListNew.DataSource = LoadRequestFormForPrint_NewReceipt()
        End Sub
    End Class

End Namespace
