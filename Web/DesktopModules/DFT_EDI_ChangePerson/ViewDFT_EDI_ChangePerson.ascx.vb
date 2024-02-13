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

Namespace YourCompany.Modules.DFT_EDI_ChangePerson

    Partial Class ViewDFT_EDI_ChangePerson
        Inherits Entities.Modules.PortalModuleBase
        Implements Entities.Modules.IActionable
        'เปลี่ยน strRegConn เป็น strEDIConn เพื่อไม่เป็น Manual
        Dim strRegConn As String = ConfigurationManager.ConnectionStrings("RegisBackConnection").ConnectionString
        Dim strEDIConn As String = ConfigurationManager.ConnectionStrings("OriginConnection").ConnectionString
        Dim myReader As SqlDataReader = Nothing
        Dim objConn As SqlConnection = Nothing
        Dim _UserName As String = CommonUtility.Get_StringValue(DotNetNuke.Entities.Users.UserController.GetUser(PortalId, UserId, False).Username)

#Region "Event Handlers"



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

        Protected Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Me.Load
            txtSearch.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" + btnSearch.UniqueID + "').click();return false;}} else {return true}; ")
            If Not Page.IsPostBack Then
                Session("UName") = UserInfo.DisplayName
                tblCardDetail.Visible = False
                tblRefList.Visible = False
                txtSearch.Focus()

                TBLPerson.Visible = False
                tblremark.Visible = False
            End If

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
                dr = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "sp_check_All_NewDS", _
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

                            txtReturnMsg.Text = CommonUtility.Get_StringValue(dr.Item("retMessage"))

                            dr.Close()
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
                Dim objConnBackList = New SqlConnection(strEDIConn)
                Dim myReadBackList As SqlDataReader = Nothing
                myReadBackList = SqlHelper.ExecuteReader(objConnBackList, CommandType.StoredProcedure, "viGet_BlackListByCompany_NewDS", New SqlParameter("@COMPANY_TAXNO", txtCompany_Taxno.Text.Trim))


                If myReadBackList.Read Then 'Type_Mistake
                    Select Case CommonUtility.Get_StringValue(myReadBackList.Item("active_flag"))
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
                If chkRePrint.Checked = False Then
                    rgRequestForm.DataSource = LoadRequestFormList("0")
                Else
                    rgRequestForm.DataSource = LoadRequestFormList("1")
                End If

                rgRequestForm.DataBind()


                LoadFormAuthorize()
                txtChangeRemark.Visible = True
                tblremark.Visible = True
                '        End If
                '    End If
                rgRequestForm.Focus()
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End Sub
        'Tab ที่ 2 รายการบัตร
        Private Sub rgCardList_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles rgCardList.DataBound
            myReader.Close()
            objConn.Close()
        End Sub

        Function LoadCard() As SqlDataReader
            'by rut 01
            Try
                objConn = New SqlConnection(strRegConn)

                myReader = SqlHelper.ExecuteReader(objConn, CommandType.StoredProcedure, "viGet_company_taxnoByP_rfcard_NewDS", New SqlParameter("@COMPANY_TAXNO", txtCompany_Taxno.Text.Trim))
                Return myReader
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try

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
                myReader = SqlHelper.ExecuteReader(objConn, CommandType.StoredProcedure, "vi_select_RepAtt1_NewDS", _
                New SqlParameter("@TCat1", 2), _
                New SqlParameter("@TCat2", 3), _
                New SqlParameter("@dForm", ""), _
                New SqlParameter("@dTo", ""), _
                New SqlParameter("@total_day", 10), _
                New SqlParameter("@company_taxno", txtCompany_Taxno.Text), _
                New SqlParameter("@site_id", CommonUtility.Get_StringValue(lblRoleID.Text)), _
                New SqlParameter("@invh_run_auto", ""))
                Return myReader
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End Function

        Private Sub rgRequestForm_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles rgRequestForm.DataBound
            objConn.Close()
        End Sub

        Private Sub rgRequestForm_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgRequestForm.ItemDataBound
            If Not (e.Item.FindControl("imbPrint") Is Nothing) Then
                CType(e.Item.FindControl("imbPrint"), ImageButton).Attributes("onmouseover") = "this.style.cursor='hand';"
            End If
        End Sub

        Private Sub rgRequestForm_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgRequestForm.NeedDataSource
            If chkRePrint.Checked = False Then
                LoadRequestFormList2("0")
            Else
                LoadRequestFormList2("1")
            End If

        End Sub

        Private Sub rgRequestForm_PageIndexChanged(ByVal source As Object, ByVal e As Telerik.Web.UI.GridPageChangedEventArgs) Handles rgRequestForm.PageIndexChanged
            rgRequestForm.CurrentPageIndex = e.NewPageIndex
            If chkRePrint.Checked = False Then
                rgRequestForm.DataSource = LoadRequestFormList("0")
            Else
                rgRequestForm.DataSource = LoadRequestFormList("1")
            End If

            rgRequestForm.DataBind()

        End Sub

        Private Sub rgBackList_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles rgBackList.DataBound
            myReader.Close()
            objConn.Close()
        End Sub
        Function LoadDocBackList() As SqlDataReader
            'by rut 02
            Try
                objConn = New SqlConnection(strEDIConn)

                myReader = SqlHelper.ExecuteReader(objConn, CommandType.StoredProcedure, "viGet_BlackListByCompany_NewDS", New SqlParameter("@COMPANY_TAXNO", txtCompany_Taxno.Text.Trim))
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
                objConn = New SqlConnection(strEDIConn)
                Dim ds As New DataSet
                ds = SqlHelper.ExecuteDataset(objConn, CommandType.StoredProcedure, "vi_form_edi_getForPrintByCardId_NewDS", _
                New SqlParameter("@COMPANY_TAXNO", txtCompany_Taxno.Text), _
                New SqlParameter("@card_id", txtSearch.Text), _
                New SqlParameter("@FORM_TYPE", "ALL"), _
                New SqlParameter("@FROM_DATE", "20000101"), _
                New SqlParameter("@TO_DATE", "25001231"), _
                New SqlParameter("@INVOICE_NO", ""), _
                New SqlParameter("@DISPLAY_FLAG", chk), _
                New SqlParameter("@SITE_ID", lblRoleID.Text))


                Return ds.Tables(0)
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End Function

        Sub LoadRequestFormList2(ByVal chk As String)
            Try
                objConn = New SqlConnection(strEDIConn)
                Dim ds As New DataSet
                ds = SqlHelper.ExecuteDataset(objConn, CommandType.StoredProcedure, "vi_form_edi_getForPrintByCardId_NewDS", _
               New SqlParameter("@COMPANY_TAXNO", txtCompany_Taxno.Text), _
               New SqlParameter("@card_id", txtSearch.Text), _
               New SqlParameter("@FORM_TYPE", "ALL"), _
               New SqlParameter("@FROM_DATE", "20000101"), _
               New SqlParameter("@TO_DATE", "25001231"), _
               New SqlParameter("@INVOICE_NO", ""), _
               New SqlParameter("@DISPLAY_FLAG", chk), _
               New SqlParameter("@SITE_ID", lblRoleID.Text))

                rgRequestForm.DataSource = ds.Tables(0)
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End Sub
        Protected Sub ToggleRowSelection(ByVal sender As Object, ByVal e As EventArgs)
            Dim runAuto As String = ""
            Session("invh_run") = Nothing

            Dim runCheckweb As String = ""
            Session("Checkweb") = Nothing

            CType(CType(sender, CheckBox).Parent.Parent, GridItem).Selected = CType(sender, CheckBox).Checked
            For Each dataItem As GridDataItem In rgRequestForm.MasterTableView.Items
                If CType(dataItem.FindControl("chkSelect"), CheckBox).Checked = True Then
                    If runAuto = "" Then
                        runAuto = dataItem.OwnerTableView.DataKeyValues(dataItem.ItemIndex)("invh_run_auto")
                        runCheckweb = dataItem.OwnerTableView.DataKeyValues(dataItem.ItemIndex)("Check_StatusWeb").ToString
                    Else
                        runAuto = runAuto & "," & dataItem.OwnerTableView.DataKeyValues(dataItem.ItemIndex)("invh_run_auto")
                        runCheckweb = runCheckweb & "," & dataItem.OwnerTableView.DataKeyValues(dataItem.ItemIndex)("Check_StatusWeb").ToString
                    End If

                End If
            Next
            Session("invh_run") = runAuto
            Session("Checkweb") = runCheckweb
        End Sub

        Protected Sub ToggleSelectedState(ByVal sender As Object, ByVal e As EventArgs)
            Dim runAuto As String = ""
            Session("invh_run") = Nothing

            Dim runCheckweb As String = ""
            Session("Checkweb") = Nothing

            If (CType(sender, CheckBox)).Checked Then
                For Each dataItem As GridDataItem In rgRequestForm.MasterTableView.Items
                    CType(dataItem.FindControl("chkSelect"), CheckBox).Checked = True
                    dataItem.Selected = True

                    If CType(dataItem.FindControl("chkSelect"), CheckBox).Checked = True Then
                        If runAuto = "" Then
                            runAuto = dataItem.OwnerTableView.DataKeyValues(dataItem.ItemIndex)("invh_run_auto")
                            runCheckweb = dataItem.OwnerTableView.DataKeyValues(dataItem.ItemIndex)("Check_StatusWeb").ToString
                        Else
                            runAuto = runAuto & "," & dataItem.OwnerTableView.DataKeyValues(dataItem.ItemIndex)("invh_run_auto")
                            runCheckweb = runCheckweb & "," & dataItem.OwnerTableView.DataKeyValues(dataItem.ItemIndex)("Check_StatusWeb").ToString
                        End If
                    End If
                Next
            Else
                For Each dataItem As GridDataItem In rgRequestForm.MasterTableView.Items
                    CType(dataItem.FindControl("chkSelect"), CheckBox).Checked = False
                    dataItem.Selected = False

                    If CType(dataItem.FindControl("chkSelect"), CheckBox).Checked = True Then
                        If runAuto = "" Then
                            runAuto = dataItem.OwnerTableView.DataKeyValues(dataItem.ItemIndex)("invh_run_auto")
                            runCheckweb = dataItem.OwnerTableView.DataKeyValues(dataItem.ItemIndex)("Check_StatusWeb")
                        Else
                            runAuto = runAuto & "," & dataItem.OwnerTableView.DataKeyValues(dataItem.ItemIndex)("invh_run_auto")
                            runCheckweb = runCheckweb & "," & dataItem.OwnerTableView.DataKeyValues(dataItem.ItemIndex)("Check_StatusWeb")
                        End If
                    End If
                Next
            End If

            Session("invh_run") = runAuto
            Session("Checkweb") = runCheckweb
        End Sub
        Function GetDate(ByVal sent_date As Date) As String
            Return Convert.ToDateTime(sent_date).ToString("d", New System.Globalization.CultureInfo("en-GB"))
        End Function

        Private Sub LoadFormAuthorize()
            Try
                Dim ds As New DataSet
                ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, "sp_common_getRequestPerson_NewDS", _
                New SqlParameter("@COMPANY_TAXNO", CommonUtility.Get_StringValue(txtCompany_Taxno.Text)))
                If ds.Tables(0).Rows.Count > 0 Then
                    
                    RCBRequestPerson.DataSource = ds.Tables(0)
                    RCBRequestPerson.DataTextField = "DESCRIPTION"
                    RCBRequestPerson.DataValueField = "CODE"
                    RCBRequestPerson.DataBind()
                   
                    RCBRequestPerson.Items.Insert(0, New Telerik.Web.UI.RadComboBoxItem("กรุณาเลือกผู้รับมอบ", "0"))

                    TBLPerson.Visible = True
                End If
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End Sub
        
        Protected Sub btnUpdatePerson_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdatePerson.Click
            Dim arr_invh_run As Array
            Dim arr_Checkweb As Array
            If Session("invh_run") <> "" Then
                arr_invh_run = Session("invh_run").ToString.Split(",")
                arr_Checkweb = Session("Checkweb").ToString.Split(",")

                Dim i As Integer
                Try
                    If RCBRequestPerson.SelectedValue = "0" Then
                        lbl_ErrMSG.Visible = True
                        lbl_ErrMSG.Text = "ไม่สามารถบันทึกได้เนื่องจากยังไม่ได้เลือกผู้รับมอบ"
                    Else
                        For i = 0 To arr_invh_run.Length - 1
                            Dim ds As New DataSet

                            ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, "vi_updateChangeRequestPerson_NewDS", _
                            New SqlParameter("@INVH_RUN_AUTO", CommonUtility.Get_StringValue(arr_invh_run(i))), _
                            New SqlParameter("@Check_StatusWeb", CommonUtility.Get_StringValue(arr_Checkweb(i))), _
                            New SqlParameter("@card_id", CommonUtility.Get_StringValue(RCBRequestPerson.SelectedValue)), _
                            New SqlParameter("@authorize2", CommonUtility.Get_StringValue(RCBRequestPerson.SelectedItem.Text)), _
                            New SqlParameter("@ChangeByuser", CommonUtility.Get_StringValue(Session("UName"))), _
                            New SqlParameter("@ChangeRemark", CommonUtility.Get_StringValue(txtChangeRemark.Text)), _
                            New SqlParameter("@ChangeSite", CommonUtility.Get_StringValue(Session("ssRoleName"))))

                            If ds.Tables(0).Rows(0).Item("retStatus") = 0 Then
                                'Page.ClientScript.RegisterStartupScript(Page.GetType(), "mykey", "Rebind();", True)
                                'Load รายการคำร้อง
                                If chkRePrint.Checked = False Then
                                    rgRequestForm.DataSource = LoadRequestFormList("0")
                                Else
                                    rgRequestForm.DataSource = LoadRequestFormList("1")
                                End If

                                rgRequestForm.DataBind()
                                txtChangeRemark.Text = ""
                                Page.ClientScript.RegisterStartupScript(Page.GetType(), "mykey", "Rebind();", True)

                                lbl_ErrMSG.Visible = False

                                'เคลียร์ข้อมูล
                                Session("invh_run") = ""
                                Session("Checkweb") = ""
                            Else
                                lbl_ErrMSG.Visible = True
                                lbl_ErrMSG.Text = "ไม่สามารถบันทึกได้"
                            End If
                        Next
                    End If
                Catch ex As Exception
                    Response.Write(ex.Message)
                End Try
            Else
                lbl_ErrMSG.Visible = True
                lbl_ErrMSG.Text = "กรุณาเลือกรายการที่จะเปลี่ยนผู้รับมอบ"
            End If
           
        End Sub

        Protected Sub btnSearch2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch2.Click
             Select Case Mid(CommonUtility.Get_StringValue(txtSearchNum.Text.Trim), 1, 1)
                Case "0", "1", "2", "3", "4", "5", "6", "7", "8", "9"
                    Try
                        rgRequestForm.DataSource = LoadFormList1("1")
                        rgRequestForm.DataBind()

                        Dim dr As SqlDataReader
                        dr = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "sp_check_All_NewDS", _
                        New SqlParameter("@card_id", LoadFormList1("1").Rows(0).Item("card_id")))
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

                                    txtReturnMsg.Text = CommonUtility.Get_StringValue(dr.Item("retMessage"))

                                    dr.Close()
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
                        Dim objConnBackList = New SqlConnection(strEDIConn)
                        Dim myReadBackList As SqlDataReader = Nothing
                        myReadBackList = SqlHelper.ExecuteReader(objConnBackList, CommandType.StoredProcedure, "viGet_BlackListByCompany_NewDS", New SqlParameter("@COMPANY_TAXNO", txtCompany_Taxno.Text.Trim))


                        If myReadBackList.Read Then 'Type_Mistake
                            Select Case CommonUtility.Get_StringValue(myReadBackList.Item("active_flag"))
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


                        LoadFormAuthorize()
                        txtChangeRemark.Visible = True
                        tblremark.Visible = True
                        '        End If
                        '    End If
                        rgRequestForm.Focus()
                    Catch ex As Exception
                        Response.Write(ex.Message)
                    End Try
                Case Else
                    Try
                        rgRequestForm.DataSource = LoadFormList1("2")
                        rgRequestForm.DataBind()

                        Dim dr As SqlDataReader
                        dr = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "sp_check_All_NewDS", _
                        New SqlParameter("@card_id", LoadFormList1("2").Rows(0).Item("card_id")))
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

                                    txtReturnMsg.Text = CommonUtility.Get_StringValue(dr.Item("retMessage"))

                                    dr.Close()
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
                        Dim objConnBackList = New SqlConnection(strEDIConn)
                        Dim myReadBackList As SqlDataReader = Nothing
                        myReadBackList = SqlHelper.ExecuteReader(objConnBackList, CommandType.StoredProcedure, "viGet_BlackListByCompany_NewDS", New SqlParameter("@COMPANY_TAXNO", txtCompany_Taxno.Text.Trim))


                        If myReadBackList.Read Then 'Type_Mistake
                            Select Case CommonUtility.Get_StringValue(myReadBackList.Item("active_flag"))
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

                        LoadFormAuthorize()
                        txtChangeRemark.Visible = True
                        tblremark.Visible = True
                        '        End If
                        '    End If
                        rgRequestForm.Focus()
                    Catch ex As Exception
                        Response.Write(ex.Message)
                    End Try
            End Select
        End Sub

        'Load รายการ แบบ เลข
        Function LoadFormList1(ByVal chkBY As String) As DataTable
            Try
                objConn = New SqlConnection(strEDIConn)
                Dim ds As New DataSet

                Select Case CommonUtility.Get_StringValue(chkBY)
                    Case "1"
                        ds = SqlHelper.ExecuteDataset(objConn, CommandType.StoredProcedure, "vi_Search_By_invh_run_auto", _
                                        New SqlParameter("@invh_run_auto", txtSearchNum.Text.Trim))
                    Case "2"
                        ds = SqlHelper.ExecuteDataset(objConn, CommandType.StoredProcedure, "vi_Search_By_reference_code2", _
                                        New SqlParameter("@reference_code2", txtSearchNum.Text.Trim))
                End Select
                


                Return ds.Tables(0)
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End Function
    End Class

End Namespace
