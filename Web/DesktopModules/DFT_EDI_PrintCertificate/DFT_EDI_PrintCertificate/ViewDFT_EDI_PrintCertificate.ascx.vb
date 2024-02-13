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

Namespace NTi.Modules.DFT_EDI_PrintCertificate
    Partial Class ViewDFT_EDI_PrintCertificate
        Inherits Entities.Modules.PortalModuleBase
        'เปลี่ยน strRegConn เป็น strEDIConn เพื่อไม่เป็น Manual
        Dim strRegConn As String = ConfigurationManager.ConnectionStrings("RegisBackConnection").ConnectionString
        Dim strEDIConn As String = ConfigurationManager.ConnectionStrings("OriginConnection").ConnectionString

        'เปลี่ยนตอน refresh
        Dim strEDIConnFre As String = ConfigurationManager.ConnectionStrings("ediConReFre").ConnectionString

        Dim myReader As SqlDataReader = Nothing
        Dim objConn As SqlConnection = Nothing
        Dim _UserName As String = CommonUtility.Get_StringValue(DotNetNuke.Entities.Users.UserController.GetUser(PortalId, UserId, False).Username)

        'Dim reader_receipt As SqlDataReader = Nothing
        Dim DataSet_Userreceipt As DataSet = Nothing
#Region "by rut"
        Function Get_ListRoles(ByVal ByRoleID As Integer, ByVal ByRoleName As String) As String
            Dim DSRoles As SqlDataReader
            Dim strEDIConn As String = ConfigurationManager.ConnectionStrings("OriginConnection").ConnectionString

            DSRoles = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "RoleList_GetList",
                            New SqlParameter("@ListRoleNameCase", ByRoleID))

            Dim strListRole As String = ""

            If DSRoles.HasRows Then
                strListRole = ByRoleName
                lblRoleID.Text = strListRole
                Session("ssRoleName") = lblRoleID.Text
            End If

            'Dim DSRoles As SqlDataReader
            'Dim strEDIConn As String = ConfigurationManager.ConnectionStrings("OriginConnection").ConnectionString

            'DSRoles = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "RoleList_GetList", _
            '                New SqlParameter("@AutoRoleID", "1"))

            'Dim strListRole As String = ""
            'Dim strTempList As String = ""

            'If DSRoles.HasRows Then
            '    DSRoles.Read()
            '    Dim arrRoles As Array
            '    strTempList = DSRoles.Item("ListRoleNameCase").ToString

            '    arrRoles = strTempList.Split(",")
            '    For iarr As Integer = 0 To arrRoles.Length - 1
            '        If CStr(ByRoleID) = arrRoles(iarr).ToString Then
            '            strListRole = ByRoleName
            '            lblRoleID.Text = strListRole
            '            Session("ssRoleName") = lblRoleID.Text

            '            DSRoles.Close()
            '            Exit For
            '        End If
            '    Next iarr

            'End If

            'Dim DSRoles As DataSet
            'Dim strEDIConn As String = ConfigurationManager.ConnectionStrings("OriginConnection").ConnectionString

            'DSRoles = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, "RoleList_GetList", _
            '                New SqlParameter("@AutoRoleID", "1"))

            'Dim strListRole As String = ""
            'Dim strTempList As String = ""

            'If DSRoles.Tables(0).Rows.Count > 0 Then
            '    Dim arrRoles As Array
            '    strTempList = DSRoles.Tables(0).Rows(0).Item("ListRoleNameCase").ToString

            '    arrRoles = strTempList.Split(",")
            '    For iarr As Integer = 0 To arrRoles.Length - 1
            '        If CStr(ByRoleID) = arrRoles(iarr).ToString Then
            '            strListRole = ByRoleName
            '            lblRoleID.Text = strListRole
            '            Session("ssRoleName") = lblRoleID.Text
            '            Exit For
            '        End If
            '    Next iarr

            'End If

            Return strListRole
        End Function
#End Region
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
                tblCardDetail.Visible = False
                tblRefList.Visible = False
                txtSearch.Focus()

                Call_printName()
                'lblDSRoleID.Text = IsRoleName2("EDI_DS")
            End If
            ShowForm_AD(Session("ssRoleName"))
            lblDSRoleID.Text = IsRoleName2("EDI_DS")
            txtTemp_user.Text = UserInfo.Username
        End Sub

        'แสดง เครื่องเพื่อให้แสดงเครื่องที่พิมพ์ฟอร์ม A and D ที่มีหลายเครื่อง
        Sub ShowForm_AD(ByVal _siteID As String)
            Select Case _siteID
                Case "ST-001"
                    Panel_A.Visible = True
                    Panel_D.Visible = True
                Case Else
                    Panel_A.Visible = False
                    Panel_D.Visible = False
            End Select
        End Sub
        'เรียกเครื่องพิมพ์คำขอ
        Sub Call_printName()
            If LoadPrint_receipt.HasRows = True Then
                rdblReceiptPrinter.DataSource = LoadPrint_receipt()
                rdblReceiptPrinter.DataTextField = "description_nameprinter"
                rdblReceiptPrinter.DataValueField = "value_Print"
                rdblReceiptPrinter.DataBind()
                If LoadPrint_SetUser_receipt.Tables(0).Rows.Count > 0 Then
                    For irdb As Integer = 0 To LoadPrint_SetUser_receipt.Tables(0).Rows.Count - 1
                        If LoadPrint_SetUser_receipt.Tables(0).Rows.Count = 1 Then
                            rdblReceiptPrinter.Items.Item(LoadPrint_SetUser_receipt.Tables(0).Rows(irdb).Item("fix_printer") - 1).Selected = True
                        Else
                            rdblReceiptPrinter.Items.Item(LoadPrint_SetUser_receipt.Tables(0).Rows(irdb).Item("fix_printer") - 1).Selected = True
                        End If
                    Next
                Else
                    lbl_ErrMSG.Text = "ไม่สามารถพิมพ์ได้เนื่องจากหาเครื่องพิมพ์ไม่พบ"
                End If
            Else
                lbl_ErrMSG.Text = "ไม่สามารถพิมพ์ได้เนื่องจากหาเครื่องพิมพ์ไม่พบ"
            End If
        End Sub

        Private Function LoadPrint_receipt() As SqlDataReader
            Try
                Dim reader_receipt As SqlDataReader = Nothing

                'objConn = New SqlConnection(strEDIConn)
                reader_receipt = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "vi_requestCall_printBysite_id", New SqlParameter("@site_id", CommonUtility.Get_StringValue(Session("ssRoleName"))), New SqlParameter("@UserName", UserInfo.Username))
                Return reader_receipt
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End Function
        Private Function LoadPrint_SetUser_receipt() As DataSet
            Try
                objConn = New SqlConnection(strEDIConn)
                DataSet_Userreceipt = SqlHelper.ExecuteDataset(objConn, CommandType.StoredProcedure, "vi_requestCall_printBysite_id", New SqlParameter("@site_id", CommonUtility.Get_StringValue(Session("ssRoleName"))), New SqlParameter("@UserName", UserInfo.Username))
                Return DataSet_Userreceipt
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End Function

        Protected Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
            Try
                txtReturnMsgBlack.Text = ""
                Dim dr As SqlDataReader
                'dr = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "sp_check_All_NewDS", _
                'New SqlParameter("@card_id", txtSearch.Text.Trim()))
                dr = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "sp_check_All_NewDS_V2",
                New SqlParameter("@card_id", txtSearch.Text.Trim()), New SqlParameter("@site", lblRoleID.Text))
                If dr.Read() Then
                    Select Case CommonUtility.Get_Int32(dr.Item("retStatus"))
                        Case 0
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

                            txtReturnMsg.Text = CommonUtility.Get_StringValue(dr.Item("retMessage"))
                            txtAuthorize7.Text = CommonUtility.Get_StringValue(dr.Item("authorize_Remark"))
                            txtCardLevel.Text = CommonUtility.Get_StringValue(dr.Item("card_level"))
                            dr.Close()

                        Case -1, -2, -3, -4, -5, -6
                            tblCardDetail.Visible = False
                            tblRefList.Visible = False

                            Me.RadAjaxManager1.ResponseScripts.Add("window.alert('" & CommonUtility.Get_StringValue(dr.Item("retMessage")) & "');")
                        Case Else
                            tblCardDetail.Visible = False
                            tblRefList.Visible = False

                            Me.RadAjaxManager1.ResponseScripts.Add("window.alert('" & CommonUtility.Get_StringValue(dr.Item("retMessage")) & "');")
                    End Select
                End If

                'Load Tab 2 รายการบัตร
                rgCardList.DataSource = LoadCard()
                rgCardList.DataBind()

                'Load Tab 3 รายการเอกสารแนบ
                rgDocAttach.DataSource = LoadDocAttach()
                rgDocAttach.DataBind()

                'Load Tab 4 บันทึกการกระทำความผิด
                rgBackList.DataSource = LoadDocBackList()
                rgBackList.DataBind()
                '=================================================
                Dim objConnBackList = New SqlConnection(strRegConn)
                'Dim objConnBackList = New SqlConnection(strEDIConn)
                Dim myReadBackList As SqlDataReader = Nothing
                myReadBackList = SqlHelper.ExecuteReader(objConnBackList, CommandType.StoredProcedure, "viGet_BlackListByCompany_NewDS", New SqlParameter("@COMPANY_TAXNO", txtCompany_Taxno.Text.Trim))


                If myReadBackList.Read Then 'Type_Mistake
                    Select Case CommonUtility.Get_StringValue(myReadBackList.Item("type_mistake"))
                        Case "B"
                            txtReturnMsgBlack.ForeColor = Drawing.Color.Red
                            txtReturnMsgBlack.Text = "*** บริษัทของท่าน ติด Black List !!! กรุณาติดต่อเจ้าหน้าที่"
                        Case "W"
                            txtReturnMsgBlack.ForeColor = Drawing.Color.Orange
                            txtReturnMsgBlack.Text = "*** บริษัทของท่าน ติด Whatch List !!! กรุณาติดต่อเจ้าหน้าที่"
                        Case Else
                            txtReturnMsgBlack.Text = ""
                    End Select
                End If
                LoadDocBackList.Close()
                '=================================================
                'Load รายการคำร้อง
                rgRequestForm.DataSource = LoadRequestFormList("0")
                rgRequestForm.DataBind()

                lblRowCountCheck.Text = "รายการทั้งหมด  " & rgRequestForm.Items.Count & "  รายการ"
                Call_printName()
                '        End If
                '    End If
                rgRequestForm.Focus()
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End Sub

        'Tab ที่ 2 รายการบัตร
        'Private Sub rgCardList_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles rgCardList.DataBound
        '    myReader.Close()
        '    objConn.Close()
        'End Sub
        Private Sub rgCardList_PageIndexChanged(ByVal source As Object, ByVal e As Telerik.Web.UI.GridPageChangedEventArgs) Handles rgCardList.PageIndexChanged
            rgRequestForm.CurrentPageIndex = e.NewPageIndex
            rgCardList.DataSource = LoadCard()
            rgCardList.DataBind()
        End Sub
        Function LoadCard() As DataTable
            Try
                Dim ds As New DataSet
                objConn = New SqlConnection(strEDIConn)

                ds = SqlHelper.ExecuteDataset(objConn, CommandType.StoredProcedure, "viGet_company_taxnoBy_rfcard_NewDS", New SqlParameter("@COMPANY_TAXNO", txtCompany_Taxno.Text.Trim))
                Return ds.Tables(0)
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try

            'by rut 01
            'Try
            '    objConn = New SqlConnection(strRegConn)

            '    myReader = SqlHelper.ExecuteReader(objConn, CommandType.StoredProcedure, "viGet_company_taxnoByP_rfcard_NewDS", New SqlParameter("@COMPANY_TAXNO", txtCompany_Taxno.Text.Trim))
            '    Return myReader
            'Catch ex As Exception
            '    Response.Write(ex.Message)
            'End Try

            'Try
            '    objConn = New SqlConnection(strRegConn)

            '    myReader = SqlHelper.ExecuteReader(objConn, CommandType.StoredProcedure, "viGet_company_taxnoByP_rfcard_NewDS", New SqlParameter("@COMPANY_TAXNO", txtCompany_Taxno.Text.Trim))
            '    Return myReader
            'Catch ex As Exception
            '    Response.Write(ex.Message)
            'End Try

            'Try
            '    objConn = New SqlConnection(strRegConn)
            '    Dim strCommand As String = "SELECT * FROM P_rfcard Where company_taxno = '" & txtCompany_Taxno.Text.Trim() & "' Order By card_id"
            '    myReader = SqlHelper.ExecuteReader(objConn, CommandType.Text, strCommand)
            '    Return myReader
            'Catch ex As Exception
            '    Response.Write(ex.Message)
            'End Try
        End Function

        'Load Tab 3 รายการเอกสารแนบ
        Private Sub rgDocAttach_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles rgDocAttach.DataBound
            myReader.Close()
            objConn.Close()
        End Sub

        Function LoadDocAttach() As SqlDataReader
            Try
                objConn = New SqlConnection(strEDIConn)
                myReader = SqlHelper.ExecuteReader(objConn, CommandType.StoredProcedure, "vi_select_RepAtt1_NewDS",
                New SqlParameter("@TCat1", 2),
                New SqlParameter("@TCat2", 3),
                New SqlParameter("@dForm", ""),
                New SqlParameter("@dTo", ""),
                New SqlParameter("@total_day", 10),
                New SqlParameter("@company_taxno", txtCompany_Taxno.Text),
                New SqlParameter("@site_id", "ALL"),
                New SqlParameter("@invh_run_auto", ""))
                Return myReader
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End Function

        Private Sub rgRequestForm_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles rgRequestForm.DataBound
            objConn.Close()
        End Sub

        Private Sub rgRequestForm_PageIndexChanged(ByVal source As Object, ByVal e As Telerik.Web.UI.GridPageChangedEventArgs) Handles rgRequestForm.PageIndexChanged
            rgRequestForm.CurrentPageIndex = e.NewPageIndex
            If chkRePrint.Checked = True Then
                rgRequestForm.DataSource = LoadRequestFormList("1")
                rgRequestForm.DataBind()
            Else
                rgRequestForm.DataSource = LoadRequestFormList("0")
                rgRequestForm.DataBind()
            End If
        End Sub

        Private Sub rgRequestForm_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgRequestForm.ItemDataBound
            If Not (e.Item.FindControl("imbPrint") Is Nothing) Then
                CType(e.Item.FindControl("imbPrint"), ImageButton).Attributes("onmouseover") = "this.style.cursor='hand';"
            End If

            'by rut visible กรณีไม่ใช่ role EDI_DS
            If TypeOf e.Item Is GridDataItem Then
                Dim lblColor_ As Label = DirectCast(e.Item.FindControl("lblCaseSign"), Label)
                Dim item As GridDataItem = DirectCast(e.Item, GridDataItem)
                If lblDSRoleID.Text = "EDI_DS" Then
                    rgRequestForm.MasterTableView.GetColumn("CaseSign1").Display = True
                    lblColor_.Text = item("CaseSign").Text
                    Select Case lblColor_.Text.ToLower
                        Case "sealsign"
                            lblColor_.ForeColor = Drawing.Color.Green
                            lblColor_.Font.Bold = True
                        Case Else
                            lblColor_.ForeColor = Drawing.Color.Red
                            lblColor_.Font.Bold = True
                    End Select
                Else
                    rgRequestForm.MasterTableView.GetColumn("CaseSign1").Display = False
                End If
            End If
        End Sub
        Function ReCheck_Value(ByVal By_Seal As String) As String
            Select Case CommonUtility.Get_StringValue(By_Seal)
                Case ""
                    Return ""
                Case Else
                    Return CommonUtility.Get_StringValue(By_Seal)
            End Select
        End Function
        'Load Tab 4 บันทึกการกระทำความผิด
        Private Sub rgBackList_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles rgBackList.DataBound
            myReader.Close()
            objConn.Close()
        End Sub

        Function LoadDocBackList() As SqlDataReader
            'by rut 02
            Try
                objConn = New SqlConnection(strRegConn)

                myReader = SqlHelper.ExecuteReader(objConn, CommandType.StoredProcedure, "viGet_BlackListByCompany_NewDSNew", New SqlParameter("@COMPANY_TAXNO", txtCompany_Taxno.Text.Trim))
                Return myReader
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try

            'Try
            '    objConn = New SqlConnection(strEDIConn)
            '    Dim strCommand As String = "SELECT * FROM BlackList Where Company_Taxno = '" & txtCompany_Taxno.Text & "'"
            '    myReader = SqlHelper.ExecuteReader(objConn, CommandType.Text, strCommand)
            '    Return myReader
            'Catch ex As Exception
            '    Response.Write(ex.Message)
            'End Try
        End Function

        'Load รายการคำร้อง
        Function LoadRequestFormList(ByVal chk As String) As DataTable
            Try
                objConn = New SqlConnection(strEDIConnFre)
                Dim ds As New DataSet
                Dim cmd As String = "vi_form_edi_getForPrint_NewDS_V2"
                If IsRoleName("EDI_DS") Then
                    cmd = "vi_form_edi_getForPrint_NewDS2_V2"
                End If

                ds = SqlHelper.ExecuteDataset(objConn, CommandType.StoredProcedure, cmd,
                        New SqlParameter("@CARD_ID", txtSearch.Text.Trim.Replace(" ", "")),
                        New SqlParameter("@FORM_TYPE", "ALL"),
                        New SqlParameter("@FROM_DATE", "20000101"),
                        New SqlParameter("@TO_DATE", "25001231"),
                        New SqlParameter("@INVOICE_NO", ""),
                        New SqlParameter("@DISPLAY_FLAG", chk),
                        New SqlParameter("@SITE_ID", lblRoleID.Text))

                Return ds.Tables(0)
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End Function


        Protected Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
            If chkRePrint.Checked = True Then
                rgRequestForm.DataSource = LoadRequestFormList("1")
                rgRequestForm.DataBind()
                lblRowCountCheck.Text = "รายการทั้งหมด  " & rgRequestForm.Items.Count & "  รายการ"
            Else
                rgRequestForm.DataSource = LoadRequestFormList("0")
                rgRequestForm.DataBind()
                lblRowCountCheck.Text = "รายการทั้งหมด  " & rgRequestForm.Items.Count & "  รายการ"
            End If
            Call_printName()
            rgRequestForm.Focus()
        End Sub

        'เก็บไว้ก่อน
        Function GetSelectedItems() As Integer
            Dim in_ As Integer

            in_ = rgRequestForm.SelectedItems.Count

            Return in_
        End Function
        'ใช้
        Protected Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
            Try
                'TextBox1.Text = GetSelectedItems()
                'Dim row_count As Integer = GetSelectedItems()

                Dim run_site_id As String = Session("ssRoleName") ' "ST-001"

                Dim myDataGridItem As GridDataItem
                Dim runAuto As String = ""
                Dim run_form_type As String = ""

                Dim run_COMPANY_TAXNO As String = ""
                Dim run_FROM_DATE As String = ""
                Dim run_TO_DATE As String = ""
                Dim run_INVOICE_NO As String = ""
                Dim run_DISPLAY_FLAG As String = ""

                'sompol
                Dim run_print_flag As String
                'Dim m_iReturn As Integer

                Dim str_runAutos As String = ""
                Dim str_runForms As String = ""
                'sompol
                Dim str_print_flag As String = ""


                For Each myDataGridItem In rgRequestForm.SelectedItems
                    ''If myDataGridItem.Selected = True Then

                    runAuto = myDataGridItem.Item("invh_run_auto").Text
                    str_runAutos += runAuto & ";"

                    'run_COMPANY_TAXNO = myDataGridItem.Item("COMPANY_TAXNO").Text
                    'run_FROM_DATE = myDataGridItem.Item("FROM_DATE").Text
                    'run_TO_DATE = myDataGridItem.Item("TO_DATE").Text
                    'run_INVOICE_NO = myDataGridItem.Item("INVOICE_NO").Text
                    'run_DISPLAY_FLAG = myDataGridItem.Item("DISPLAY_FLAG").Text

                    run_form_type = myDataGridItem.Item("form_type").Text
                    str_runForms += run_form_type & ";"

                    'sompol
                    run_print_flag = myDataGridItem.Item("print_flag").Text
                    str_print_flag += run_print_flag & ";"
                    ''End If
                Next
                Session("CheckUser") = _UserName

                'For i_row = 0 To row_count - 1
                Dim spl_runAuto As Array
                Dim spl_runForms As Array
                'sompol
                Dim sql_print_flag As Array

                spl_runAuto = str_runAutos.Split(";")
                spl_runForms = str_runForms.Split(";")
                'sompol
                sql_print_flag = str_print_flag.Split(";")
                Dim i As Integer

                '========================================
                If RBListPrinter.Items.Item(0).Selected = True And RBListFormRequest.Items.Item(0).Selected = True Then
                    If spl_runAuto(i) = "" Or spl_runForms(i) = "" Then
                        RadAjaxManager1.ResponseScripts.Add("window.alert('ไม่สามารถ พิมพ์คำขอได้เนื่องจาก ยังไม่ได้เลือกรายการ');")
                    Else
                        'จอภาพและแบบคำขอ
                        Me.RadAjaxManager1.ResponseScripts.Add("window.open('http://" & DotNetNuke.Common.GetDomainName(Request) & "/DesktopModules/DFT_EDI_PrintCertificate/View_Report.aspx?action=viewprint&CheckUserPage=" & Session("CheckUser") & "&SendCell=" &
                                        runAuto & "&CellType=" & run_form_type &
                                        "&SendSiteID=" & run_site_id & "&PrintFlag=" & sql_print_flag(i) & "&radioForm=0" & "',null,'height=600, width=800,status=no, resizable= no,scrollbars=yes, toolbar=no,location=no,menubar=no,center=yes');")

                        txtFirstpage.Text = ""
                        txtLastpage.Text = ""
                        'Me.RadAjaxManager1.ResponseScripts.Add("window.open('http://" & DotNetNuke.Common.GetDomainName(Request) & "/DesktopModules/DFT_EDI_PrintCertificate/View_Report.aspx?action=viewprint&CheckUserPage=" & Session("CheckUser") & "&SendCell=" & _
                        '                                    runAuto & "&CellType=" & run_form_type & _
                        '                                    "&SendSiteID=" & run_site_id & "&radioForm=0" & "',null,'height=650, width=860,status=yes, resizable= no,scrollbars=yes, toolbar=no,location=no,menubar=no,center=yes');")
                        '============
                        'Me.RadAjaxManager1.ResponseScripts.Add("window.open('http://" & DotNetNuke.Common.GetDomainName(Request) & "/DesktopModules/DFT_EDI_PrintCertificate/View_Report.aspx?action=viewprint&CheckUserPage=" & Session("CheckUser") & "&SendCell=" & _
                        '                runAuto & "&CellType=" & run_form_type & _
                        '                "&SendSiteID=" & run_site_id & "&radioForm=0" & "',null,'fullscreen=yes,status=no, resizable= no,scrollbars=yes, toolbar=no,location=no,menubar=no,center=yes');")

                    End If

                ElseIf RBListPrinter.Items.Item(0).Selected = True And RBListFormRequest.Items.Item(0).Selected = False Then
                    If spl_runAuto(i) = "" Or spl_runForms(i) = "" Then
                        RadAjaxManager1.ResponseScripts.Add("window.alert('ไม่สามารถ พิมพ์หนังสือรับรองได้ ยังไม่ได้เลือกรายการ');")
                    Else
                        'จอภาพและหนังสือรับรอง
                        Me.RadAjaxManager1.ResponseScripts.Add("window.open('http://" & DotNetNuke.Common.GetDomainName(Request) & "/DesktopModules/DFT_EDI_PrintCertificate/View_Report.aspx?action=viewprint&CheckUserPage=" & Session("CheckUser") & "&SendCell=" &
                                                            runAuto & "&CellType=" & run_form_type &
                                                            "&SendSiteID=" & run_site_id & "&PrintFlag=" & sql_print_flag(i) & "&radioForm=1" & "',null,'height=600, width=800, status=yes, resizable= no,scrollbars=yes, toolbar=no,location=no,menubar=no,center=yes');")

                        txtFirstpage.Text = ""
                        txtLastpage.Text = ""
                        'Me.RadAjaxManager1.ResponseScripts.Add("window.open('http://" & DotNetNuke.Common.GetDomainName(Request) & "/DesktopModules/DFT_EDI_PrintCertificate/View_Report.aspx?action=viewprint&CheckUserPage=" & Session("CheckUser") & "&SendCell=" & _
                        '                                                        runAuto & "&CellType=" & run_form_type & _
                        '                                                        "&SendSiteID=" & run_site_id & "&radioForm=1" & "',null,'height=650, width=860,status=yes, resizable= no,scrollbars=yes, toolbar=no,location=no,menubar=no,center=yes');")

                    End If
                    ''Refresh(Grid)
                    'rgRequestForm.DataSource = LoadRequestFormList("0")
                    'rgRequestForm.DataBind()

                    'check เรื่อง ไม่ให้ refresh ตอน พิมพ์ซ้ำ
                    ''If chkRePrint.Checked = True Then

                    ''Else
                    ''    rgRequestForm.DataSource = LoadRequestFormList("0")
                    ''    rgRequestForm.DataBind()

                    ''    lblRowCountCheck.Text = "รายการทั้งหมด  " & LoadRequestFormList("0").Rows.Count & "  รายการ"
                    ''End If
                ElseIf RBListPrinter.Items.Item(0).Selected = False And RBListFormRequest.Items.Item(0).Selected = True Then
                    For i = 0 To spl_runAuto.Length - 1

                        Dim RBList_Form As String = "0" 'check เป็น คำขอ
                        'เครื่องพิมพ์และแบบคำขอ
                        If spl_runAuto(i) = "" Or spl_runForms(i) = "" Then
                            Exit For
                        Else
                            Select Case ReportPrintClass.printMultireport(spl_runForms(i),
                                                    spl_runAuto(i), run_site_id, RBList_Form, lblDSRoleID.Text, CommonUtility.Get_StringValue(rdblReceiptPrinter.SelectedValue), sql_print_flag(i), txtFirstpage.Text.Trim, txtLastpage.Text.Trim, "", "", txtTemp_user.Text)
                                Case 1 'print ได้ ปกติ
                                    txtFirstpage.Text = ""
                                    txtLastpage.Text = ""
                                    checkUpdate_user(CommonUtility.Get_StringValue(spl_runAuto(i)), CommonUtility.Get_StringValue(Session("CheckUser")), RBList_Form)
                                Case 2 'พิมพ์ไม่ได้ เงื่อนไข ไม่มีรายการสินค้า
                                    If updatePrintTotal(spl_runAuto(i), CStr(1)) = True Then

                                    End If
                                    txtFirstpage.Text = ""
                                    txtLastpage.Text = ""
                                    checkUpdate_user(CommonUtility.Get_StringValue(spl_runAuto(i)), CommonUtility.Get_StringValue(Session("CheckUser")), RBList_Form)
                                    RadAjaxManager1.ResponseScripts.Add("window.alert('ไม่สามารถพิมพ์แบบคำขอเลขที่อ้างอิง " & spl_runAuto(i) & " ได้ เนื่องจากรายการสินค้าไม่มี');")
                                Case 3 'พิมพ์ไม่ได้เงื่อนไข อื่น
                                    txtFirstpage.Text = ""
                                    txtLastpage.Text = ""
                                    RadAjaxManager1.ResponseScripts.Add("window.alert('ไม่สามารถพิมพ์แบบคำขอเลขที่อ้างอิง " & spl_runAuto(i) & " ได้');")
                            End Select

                        End If

                        'If ReportPrintClass.printMultireport(run_form_type, _
                        '                        runAuto, run_site_id, RBList_Form) = True Then
                        'Else
                        '    RadAjaxManager1.ResponseScripts.Add("window.alert('ไม่สามารถพิมพ์แบบคำขอได้');")
                        'End If
                    Next
                    spl_runForms(i) = ""
                    spl_runAuto(i) = ""
                ElseIf RBListPrinter.Items.Item(0).Selected = False And RBListFormRequest.Items.Item(0).Selected = False Then
                    For i = 0 To spl_runAuto.Length - 1
                        Dim RBList_Form As String = "1" 'check เป็น หนังสือรับรอง
                        'เครื่องพิมพ์และหนังสือรับรอง
                        If spl_runAuto(i) = "" Or spl_runForms(i) = "" Then
                            Exit For
                        Else
                            'ปรับเรื่องพิมพ์ เลือกหน้าได้
                            If (txtFirstpage.Text = "" And txtLastpage.Text = "") = True Then 'เพื่อ print แบบปกติ
                                Select Case ReportPrintClass.printMultireport(spl_runForms(i),
                                                                                    spl_runAuto(i), run_site_id, RBList_Form, lblDSRoleID.Text, "", sql_print_flag(i), txtFirstpage.Text.Trim, txtLastpage.Text.Trim, rdoA_ALL.SelectedValue, rdoD_ALL.SelectedValue, txtTemp_user.Text)
                                    Case 1 'print ได้
                                        txtFirstpage.Text = ""
                                        txtLastpage.Text = ""
                                        checkUpdate_user(CommonUtility.Get_StringValue(spl_runAuto(i)), CommonUtility.Get_StringValue(Session("CheckUser")), RBList_Form)
                                    Case 2 'พิมพ์ไม่ได้ไม่มีรายการสินค้า
                                        If updatePrintTotal(spl_runAuto(i), CStr(1)) = True Then

                                        End If
                                        txtFirstpage.Text = ""
                                        txtLastpage.Text = ""
                                        checkUpdate_user(CommonUtility.Get_StringValue(spl_runAuto(i)), CommonUtility.Get_StringValue(Session("CheckUser")), RBList_Form)
                                        RadAjaxManager1.ResponseScripts.Add("window.alert('ไม่สามารถพิมพ์หนังสือรับรองเลขที่อ้างอิง " & spl_runAuto(i) & " ได้ เนื่องจากรายการสินค้าไม่มี');")
                                    Case 3 'พิมพ์ไม่ได้เงื่อนไขอื่น
                                        txtFirstpage.Text = ""
                                        txtLastpage.Text = ""
                                        RadAjaxManager1.ResponseScripts.Add("window.alert('ไม่สามารถพิมพ์หนังสือรับรองเลขที่อ้างอิง " & spl_runAuto(i) & " ได้');")
                                End Select

                            Else 'print แบบ เลือกหน้า
                                If (txtFirstpage.Text = "0" Or txtLastpage.Text = "0") = True Then
                                    RadAjaxManager1.ResponseScripts.Add("window.alert('ไม่สามารถป้อนเลขหน้าเป็น ศูนย์ ได้');")
                                ElseIf CInt(txtFirstpage.Value) > CInt(txtLastpage.Value) = True Then
                                    RadAjaxManager1.ResponseScripts.Add("window.alert('เลขหน้าแรกไม่สามารถ มากกว่าหน้าสุดท้ายได้');")
                                Else
                                    If spl_runAuto.Length - 1 > 1 Then 'check เพื่อให้เลือกรายการ 1 รายการ
                                        RadAjaxManager1.ResponseScripts.Add("window.alert('กรุณาเลือกรายการ เพียง 1 รายการ');")
                                    Else
                                        Select Case ReportPrintClass.printMultireport(spl_runForms(i),
                                                                                            spl_runAuto(i), run_site_id, RBList_Form, lblDSRoleID.Text, "", sql_print_flag(i), txtFirstpage.Text.Trim, txtLastpage.Text.Trim, rdoA_ALL.SelectedValue, rdoD_ALL.SelectedValue, txtTemp_user.Text)
                                            Case 1 'print ได้
                                                txtFirstpage.Text = ""
                                                txtLastpage.Text = ""
                                                checkUpdate_user(CommonUtility.Get_StringValue(spl_runAuto(i)), CommonUtility.Get_StringValue(Session("CheckUser")), RBList_Form)
                                            Case 2 'พิมพ์ไม่ได้ไม่มีรายการสินค้า
                                                If updatePrintTotal(spl_runAuto(i), CStr(1)) = True Then

                                                End If
                                                txtFirstpage.Text = ""
                                                txtLastpage.Text = ""
                                                checkUpdate_user(CommonUtility.Get_StringValue(spl_runAuto(i)), CommonUtility.Get_StringValue(Session("CheckUser")), RBList_Form)
                                                RadAjaxManager1.ResponseScripts.Add("window.alert('ไม่สามารถพิมพ์หนังสือรับรองเลขที่อ้างอิง " & spl_runAuto(i) & " ได้ เนื่องจากรายการสินค้าไม่มี');")
                                            Case 3 'พิมพ์ไม่ได้เงื่อนไขอื่น
                                                txtFirstpage.Text = ""
                                                txtLastpage.Text = ""
                                                RadAjaxManager1.ResponseScripts.Add("window.alert('ไม่สามารถพิมพ์หนังสือรับรองเลขที่อ้างอิง " & spl_runAuto(i) & " ได้');")
                                        End Select

                                    End If
                                End If

                            End If
                        End If

                    Next
                    spl_runForms(i) = ""
                    spl_runAuto(i) = ""
                    'Refresh(Grid)
                    'check เรื่อง ไม่ให้ refresh ตอน พิมพ์ซ้ำ
                    ''If chkRePrint.Checked = True Then

                    ''Else
                    ''    rgRequestForm.DataSource = LoadRequestFormList("0")
                    ''    rgRequestForm.DataBind()

                    ''    lblRowCountCheck.Text = "รายการทั้งหมด  " & LoadRequestFormList("0").Rows.Count & "  รายการ"
                    ''End If

                End If
                'Next
                'check เรื่อง ไม่ให้ refresh ตอน พิมพ์ซ้ำ
                ''If chkRePrint.Checked = True Then

                ''Else
                ''    If RBListFormRequest.Items.Item(0).Selected = False Then
                ''        rgRequestForm.DataSource = LoadRequestFormList("0")
                ''        rgRequestForm.DataBind()

                ''        lblRowCountCheck.Text = "รายการทั้งหมด  " & LoadRequestFormList("0").Rows.Count & "  รายการ"
                ''    End If
                ''End If

                'Refresh(Grid)
                rgRequestForm.DataSource = LoadRequestFormList("0")
                rgRequestForm.DataBind()

                lblRowCountCheck.Text = "รายการทั้งหมด  " & rgRequestForm.Items.Count & "  รายการ"
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try

            'code เดิม
            'Try
            '    Dim myDataGridItem As GridDataItem
            '    Dim runAuto As String = ""
            '    Dim run_form_type As String = ""
            '    Dim run_site_id As String = ""
            '    Dim run_COMPANY_TAXNO As String = ""
            '    Dim run_FROM_DATE As String = ""
            '    Dim run_TO_DATE As String = ""
            '    Dim run_INVOICE_NO As String = ""
            '    Dim run_DISPLAY_FLAG As String = ""

            '    Dim m_iReturn As Integer
            '    For Each myDataGridItem In rgRequestForm.MasterTableView.Items
            '        If CType(myDataGridItem.FindControl("chkSelect"), CheckBox).Checked = True Then
            '            runAuto = myDataGridItem.Item("invh_run_auto").Text
            '            run_COMPANY_TAXNO = myDataGridItem.Item("COMPANY_TAXNO").Text
            '            run_FROM_DATE = myDataGridItem.Item("FROM_DATE").Text
            '            run_TO_DATE = myDataGridItem.Item("TO_DATE").Text
            '            run_INVOICE_NO = myDataGridItem.Item("INVOICE_NO").Text
            '            run_DISPLAY_FLAG = myDataGridItem.Item("DISPLAY_FLAG").Text

            '            run_form_type = myDataGridItem.Item("form_type").Text
            '            If myDataGridItem.Item("site_id").Text <> "&nbsp;" Then
            '                run_site_id = Nothing
            '            Else
            '                run_site_id = myDataGridItem.Item("site_id").Text
            '            End If


            '            m_iReturn = SqlHelper.ExecuteNonQuery(strEDIConn, CommandType.StoredProcedure, "vi_form_edi_getForPrint", New SqlParameter("@COMPANY_TAXNO", run_COMPANY_TAXNO), New SqlParameter("@FORM_TYPE", run_form_type), New SqlParameter("@FROM_DATE", run_FROM_DATE), New SqlParameter("@TO_DATE", run_TO_DATE), New SqlParameter("@INVOICE_NO", run_INVOICE_NO), New SqlParameter("@DISPLAY_FLAG", run_DISPLAY_FLAG), New SqlParameter("@SITE_ID", run_site_id))
            '        End If
            '    Next

            '    Session("CheckUser") = _UserName
            '    '========================================
            '    If RBListPrinter.Items.Item(0).Selected = True And RBListFormRequest.Items.Item(0).Selected = True Then
            '        'จอภาพและแบบคำขอ
            '        Response.Write("<script language ='javascript'> window.open('http://" & DotNetNuke.Common.GetDomainName(Request) & "/DesktopModules/DFT_EDI_PrintCertificate/View_Report.aspx?action=viewprint&CheckUserPage=" & Session("CheckUser") & "&SendCell=" & _
            '                        runAuto & "&CellType=" & run_form_type & _
            '                        "&SendSiteID=" & run_site_id & "&SendCOMPANY_TAXNO=" & run_COMPANY_TAXNO & "&SendFROM_DATE=" & run_FROM_DATE & "&SendTO_DATE=" & run_TO_DATE & "&SendINVOICE_NO=" & run_INVOICE_NO & "&SendDISPLAY_FLAG=" & run_DISPLAY_FLAG & _
            '                        "&radioForm=0" & "',null,'height=650, width=860,status=yes, resizable= no,scrollbars=yes, toolbar=no,location=no,menubar=no,center=yes'); </script>")
            '    ElseIf RBListPrinter.Items.Item(0).Selected = True And RBListFormRequest.Items.Item(0).Selected = False Then
            '        'จอภาพและหนังสือรับรอง
            '        Response.Write("<script language ='javascript'> window.open('http://" & DotNetNuke.Common.GetDomainName(Request) & "/DesktopModules/DFT_EDI_PrintCertificate/View_Report.aspx?action=viewprint&CheckUserPage=" & Session("CheckUser") & "&SendCell=" & _
            '                        runAuto & "&CellType=" & run_form_type & _
            '                        "&SendSiteID=" & run_site_id & "&SendCOMPANY_TAXNO=" & run_COMPANY_TAXNO & "&SendFROM_DATE=" & run_FROM_DATE & "&SendTO_DATE=" & run_TO_DATE & "&SendINVOICE_NO=" & run_INVOICE_NO & "&SendDISPLAY_FLAG=" & run_DISPLAY_FLAG & _
            '                        "&radioForm=1" & "',null,'height=650, width=860,status=yes, resizable= no,scrollbars=yes, toolbar=no,location=no,menubar=no,center=yes'); </script>")

            '    ElseIf RBListPrinter.Items.Item(0).Selected = False And RBListFormRequest.Items.Item(0).Selected = True Then
            '        Dim RBList_Form As String = "0"
            '        'เครื่องพิมพ์และแบบคำขอ
            '        If ReportPrintClass.printMultireport(run_form_type, _
            '        runAuto, run_site_id, RBList_Form) = True Then
            '        Else
            '            Dim errorMSReport As String = "ไม่สามารถ print ได้"
            '            Page.RegisterStartupScript("script", "<script language ='javascript'> alert('" & errorMSReport & "')</script>")
            '        End If
            '    ElseIf RBListPrinter.Items.Item(0).Selected = False And RBListFormRequest.Items.Item(0).Selected = False Then
            '        Dim RBList_Form As String = "1"
            '        'เครื่องพิมพ์และหนังสือรับรอง

            '        If ReportPrintClass.printMultireport(run_form_type, _
            '        runAuto, run_site_id, RBList_Form) = True Then
            '        Else
            '            Dim errorMSReport As String = "ไม่สามารถ print ได้"
            '            Page.RegisterStartupScript("script", "<script language ='javascript'> alert('" & errorMSReport & "')</script>")
            '        End If
            '    End If
            'Catch ex As Exception
            '    Response.Write(ex.Message)
            'End Try
        End Sub

        Protected Sub ToggleRowSelection(ByVal sender As Object, ByVal e As EventArgs)
            CType(CType(sender, CheckBox).Parent.Parent, GridItem).Selected = CType(sender, CheckBox).Checked
        End Sub

        Protected Sub ToggleSelectedState(ByVal sender As Object, ByVal e As EventArgs)
            If (CType(sender, CheckBox)).Checked Then
                For Each dataItem As GridDataItem In rgRequestForm.MasterTableView.Items
                    CType(dataItem.FindControl("chkSelect"), CheckBox).Checked = True
                    dataItem.Selected = True
                Next
            Else
                For Each dataItem As GridDataItem In rgRequestForm.MasterTableView.Items
                    CType(dataItem.FindControl("chkSelect"), CheckBox).Checked = False
                    dataItem.Selected = False
                Next
            End If
        End Sub
        'Unuse print
        Protected Sub imbPrint_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs)
            'Try
            '    Dim runAuto As String = CType(sender.AlternateText, String)
            '    Dim run_form_type As String = CType(sender.ToolTip, String)
            '    Dim run_site_id As String = Session("ssRoleName") ' "ST-001"
            '    Dim m_iReturn As Integer

            '    'Refresh Grid
            '    'rgRequestForm.DataSource = LoadRequestFormList("0")
            '    'rgRequestForm.DataBind()


            '    Session("CheckUser") = _UserName
            '    '========================================
            '    If RBListPrinter.Items.Item(0).Selected = True And RBListFormRequest.Items.Item(0).Selected = True Then
            '        'จอภาพและแบบคำขอ
            '        Me.RadAjaxManager1.ResponseScripts.Add("window.open('http://" & DotNetNuke.Common.GetDomainName(Request) & "/DesktopModules/DFT_EDI_PrintCertificate/View_Report.aspx?action=viewprint&CheckUserPage=" & Session("CheckUser") & "&SendCell=" & _
            '                        runAuto & "&CellType=" & run_form_type & _
            '                        "&SendSiteID=" & run_site_id & "&radioForm=0" & "',null,'fullscreen=yes,status=no, resizable= no,scrollbars=yes, toolbar=no,location=no,menubar=no,center=yes');")
            '        'Me.RadAjaxManager1.ResponseScripts.Add("window.open('http://" & DotNetNuke.Common.GetDomainName(Request) & "/DesktopModules/DFT_EDI_PrintCertificate/View_Report.aspx?action=viewprint&CheckUserPage=" & Session("CheckUser") & "&SendCell=" & _
            '        '                                    runAuto & "&CellType=" & run_form_type & _
            '        '                                    "&SendSiteID=" & run_site_id & "&radioForm=0" & "',null,'height=650, width=860,status=yes, resizable= no,scrollbars=yes, toolbar=no,location=no,menubar=no,center=yes');")

            '    ElseIf RBListPrinter.Items.Item(0).Selected = True And RBListFormRequest.Items.Item(0).Selected = False Then
            '        'จอภาพและหนังสือรับรอง
            '        Me.RadAjaxManager1.ResponseScripts.Add("window.open('http://" & DotNetNuke.Common.GetDomainName(Request) & "/DesktopModules/DFT_EDI_PrintCertificate/View_Report.aspx?action=viewprint&CheckUserPage=" & Session("CheckUser") & "&SendCell=" & _
            '                                            runAuto & "&CellType=" & run_form_type & _
            '                                            "&SendSiteID=" & run_site_id & "&radioForm=1" & "',null,'fullscreen=yes, status=yes, resizable= no,scrollbars=yes, toolbar=no,location=no,menubar=no,center=yes');")
            '        'Me.RadAjaxManager1.ResponseScripts.Add("window.open('http://" & DotNetNuke.Common.GetDomainName(Request) & "/DesktopModules/DFT_EDI_PrintCertificate/View_Report.aspx?action=viewprint&CheckUserPage=" & Session("CheckUser") & "&SendCell=" & _
            '        '                                                        runAuto & "&CellType=" & run_form_type & _
            '        '                                                        "&SendSiteID=" & run_site_id & "&radioForm=1" & "',null,'height=650, width=860,status=yes, resizable= no,scrollbars=yes, toolbar=no,location=no,menubar=no,center=yes');")

            '    ElseIf RBListPrinter.Items.Item(0).Selected = False And RBListFormRequest.Items.Item(0).Selected = True Then
            '        Dim RBList_Form As String = "0" 'check เป็น คำขอ
            '        'เครื่องพิมพ์และแบบคำขอ
            '        If ReportPrintClass.printMultireport(run_form_type, _
            '        runAuto, run_site_id, RBList_Form, Session("CheckUser"), CommonUtility.Get_StringValue(rdblReceiptPrinter.SelectedValue), sql_print_flag(i)) = True Then
            '        Else
            '            RadAjaxManager1.ResponseScripts.Add("window.alert('ไม่สามารถพิมพ์แบบคำขอได้');")
            '        End If
            '    ElseIf RBListPrinter.Items.Item(0).Selected = False And RBListFormRequest.Items.Item(0).Selected = False Then
            '        Dim RBList_Form As String = "1" 'check เป็น หนังสือรับรอง
            '        'เครื่องพิมพ์และหนังสือรับรอง

            '        If ReportPrintClass.printMultireport(run_form_type, _
            '        runAuto, run_site_id, RBList_Form, Session("CheckUser"), "", sql_print_flag(i)) = True Then
            '        Else
            '            RadAjaxManager1.ResponseScripts.Add("window.alert('ไม่สามารถพิมพ์หนังสือรับรองได้');")

            '            'Dim errorMSReport As String = "ไม่สามารถ print ได้"
            '            'Page.RegisterStartupScript("script", "<script language ='javascript'> alert('" & errorMSReport & "')</script>")
            '        End If
            '    End If
            'Catch ex As Exception
            '    Response.Write(ex.Message)
            'End Try
        End Sub


        Protected Sub btnVerifly_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnVerifly.Click
            'Dim ds As New DataSet
            'ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, "sp_common_edi_verifyMsg_byComputer")
            Dim ds As New DataSet
            Dim ds2 As New DataSet

            If Not Date.Now.Hour >= 15 And Date.Now.Hour <= 18 Then
                Page.ClientScript.RegisterStartupScript(Page.GetType, "error1", "alert('เปิดให้ใช้ หลังเวลา 15.00 น. เท่านั้น!!');", True)
            Else
                ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, "sp_common_edi_verifyMsg_byComputer")
                ds2 = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, "sp_common_edi_verifyMsg_byComputer_2Ser24")
                Page.ClientScript.RegisterStartupScript(Page.GetType, "alert1", "alert('ระบบทำการตรวจสอบคำขอเรียบร้อยแล้ว');", True)
            End If
        End Sub

        Function GetDate(ByVal sent_date As Date) As String
            Return Convert.ToDateTime(sent_date).ToString("d", New System.Globalization.CultureInfo("en-GB"))
        End Function
        Function GetStatus_(ByVal sent_check As Object) As String
            Dim S_check As String

            'If Not sent_check.Equals(System.DBNull.Value) Then txtInvoiceDate1.SelectedDate = CommonUtility.Get_DateTime(m_objReader("invoice_Date1"))
            If Not sent_check.Equals(System.DBNull.Value) = True Then
                S_check = sent_check
            Else
                S_check = "N"
            End If
            Return S_check
        End Function
        Function GetFormName_(ByVal sent_check As Object) As String
            Dim S_check As String

            'If Not sent_check.Equals(System.DBNull.Value) Then txtInvoiceDate1.SelectedDate = CommonUtility.Get_DateTime(m_objReader("invoice_Date1"))
            If Not sent_check.Equals(System.DBNull.Value) = True Then
                Select Case sent_check
                    Case "FORM1"
                        S_check = "เอ"
                    Case "FORM1_1"
                        S_check = "เอ Certificate"
                    Case "FORM1_2"
                        S_check = "เอ Cumulative"
                    Case "FORM1_3"
                        S_check = "เอ Cumulation"
                    Case "FORM1_4"
                        S_check = "เอ EC Cumulation"
                    Case "FORM2"
                        S_check = "ซีโอ(ทั่วไป)"
                    Case "FORM2_1"
                        S_check = "ซีโอ(Textile)"
                    Case "FORM2_2"
                        S_check = "ซีโอ(ไก่)"
                    Case "FORM2_3"
                        S_check = "ซีโอ(มันสำปะหลัง)"
                    Case "FORM2_4" 'ฟอร์มใหม่
                        S_check = "ซีโอ(แป้งมันสำปะหลัง)"

                        ''ByTine 28-11-2559 เพิ่มฟอร์มใหม่ ฟอร์ม CO (ปลา)
                    Case "FORM2_5"
                        S_check = "ซีโอ(ปลา)"
                        ''ByTine 28-11-2559 เพิ่มฟอร์มใหม่ ฟอร์ม CO (ข้าว)
                    Case "FORM2_6"
                        S_check = "ซีโอ(ข้าว)"

                    Case "FORM3"
                        S_check = "ซีโอ(เม็กซิโก)"
                    Case "FORM3_1"
                        S_check = "ซีโอ(เม็กซิโก)"
                        'by rut D new
                    Case "FORM4"
                        S_check = "ดี"
                    Case "FORM44_4"
                        S_check = "ดี ใหม่"
                    Case "FORM44"
                        S_check = "ดี Car"
                    Case "FORM44_44"
                        S_check = "ดี ใหม่ Car"
                    Case "FORM4_1"
                        S_check = "ดี AIGO"
                    Case "FORM44_41"
                        S_check = "ดี AIGO ใหม่"
                    Case "FORM441"
                        S_check = "ดี AIGO Car"
                    Case "FORM441_4"
                        S_check = "ดี AIGO ใหม่ Car"
                        '=================
                    Case "FORM4_2"
                        S_check = "อี"
                    Case "FORM4_3"
                        S_check = "FTA(ไทย - อินเดีย)"
                    Case "FORM4_4"
                        S_check = "FTA(ไทย - ออสเตรเลีย)"
                    Case "FORM4_5"
                        S_check = "JTEPA"
                    Case "FORM4_6"
                        S_check = "AJCEP"

                        'by rut 26-09-2014
                    Case "FORM4_61"
                        S_check = "AJCEP"

                    Case "FORM4_8"
                        S_check = "ASEAN-KOREA"

                        'by rut 26-09-2014
                    Case "FORM4_81"
                        S_check = "ASEAN-KOREA"

                    Case "FORM4_9"
                        S_check = "ASEAN-INDIA"
                    Case "FORM4_91"
                        S_check = "AANZ(อาเซียน)"

                        ''ByTine 23-03-2559 AANZ ใหม่
                    Case "FORM4_911"
                        S_check = "AANZ(อาเซียน) ใหม่"

                    Case "FORM5"
                        S_check = "จี.เอส.ที.พี"
                    Case "FORM5_1" 'by rut ฟอร์มใหม่ 20-12-54
                        S_check = "ไทย-เปรู"

                        'byTine เพิ่มฟอร์มใหม่ 09-10-2558
                    Case "FORM5_2"
                        S_check = "ไทย-ชิลี"

                    Case "FORM6"
                        S_check = "ใบยาสูบ"
                    Case "FORM7"
                        S_check = "สินค้าหัตถกรรมทั่วไป"
                    Case "FORM8"
                        S_check = "สินค้าหัตถกรรมหรือผ้าทอด้วยมือ"
                    Case "FORM9"
                        S_check = "สินค้าผ้าไหมและผ้าฝ้ายทอด้วยมือ"
                    Case "FORMRussia"
                        S_check = "เอ RUSSIAN"
                    Case "FORM44_01", "FORM44_02"
                        S_check = "ดี ASW"

                End Select
            End If
            Return S_check
        End Function
        Protected Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
            'Dim rpt = New AAA

            'rpt.Run()
            'rpt.Document.Print(True, True, False)
        End Sub

        Private Sub rgRequestForm_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgRequestForm.NeedDataSource
            If chkRePrint.Checked = True Then
                rgRequestForm.DataSource = LoadRequestFormList("1")
            Else
                rgRequestForm.DataSource = LoadRequestFormList("0")
            End If

        End Sub

        'Check role for DS2
        Protected Function IsRoleName(ByVal str_rolename As String) As Boolean
            Dim ret As Boolean = False
            Dim myRoleControllers As New DotNetNuke.Security.Roles.RoleController()
            Dim i As Integer = 0
            For i = 0 To MyBase.UserInfo.Roles.Length - 1
                Dim myRoleInfo As DotNetNuke.Security.Roles.RoleInfo = myRoleControllers.GetRoleByName(0, MyBase.UserInfo.Roles(i))

                If myRoleInfo.RoleName = str_rolename Then
                    ret = True
                    Exit For
                End If
            Next i
            Return ret
        End Function

        'Check role str for DS2
        Protected Function IsRoleName2(ByVal str_rolename As String) As String
            Dim ret As String = ""
            Dim myRoleControllers As New DotNetNuke.Security.Roles.RoleController()
            Dim i As Integer = 0
            For i = 0 To MyBase.UserInfo.Roles.Length - 1
                Dim myRoleInfo As DotNetNuke.Security.Roles.RoleInfo = myRoleControllers.GetRoleByName(0, MyBase.UserInfo.Roles(i))

                If myRoleInfo.RoleName = str_rolename Then
                    ret = "EDI_DS"
                    Exit For
                End If
            Next i
            Return ret
        End Function

    End Class

End Namespace
