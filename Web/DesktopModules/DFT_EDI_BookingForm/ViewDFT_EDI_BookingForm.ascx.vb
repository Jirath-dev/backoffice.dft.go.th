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

Namespace NTi.Modules.DFT_EDI_BookingForm
    Partial Class ViewDFT_EDI_BookingForm
        Inherits Entities.Modules.PortalModuleBase
        Dim strRegConn As String = ConfigurationManager.ConnectionStrings("RegisBackConnection").ConnectionString
        Dim strEDIConn As String = ConfigurationManager.ConnectionStrings("OriginConnection").ConnectionString
        Dim objConn As SqlConnection = Nothing
        Private dt As DataTable

        Private Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
            Call LoadAllForm()
        End Sub

        Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            txtFormNum.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" + btnInsertItem.UniqueID + "').click();return false;}} else {return true}; ")

            If Not Page.IsPostBack Then
                Session("xtable") = Nothing
                tblCardDetail.Visible = False
                tblRefList.Visible = False
                txtSearch.Focus()
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

            If Me.Session("xtable") Is Nothing Then
                dt = New DataTable()
                dt.Columns.Add(New DataColumn("invh_run_auto", GetType(String)))
                dt.Columns.Add(New DataColumn("form_name", GetType(String)))
                dt.Columns.Add(New DataColumn("company_name", GetType(String)))
                dt.Columns.Add(New DataColumn("AuthName_Thai", GetType(String)))
                Me.Session("xtable") = dt
            Else
                dt = DirectCast(Me.Session("xtable"), DataTable)
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
                tblCardDetail.Visible = True
                tblRefList.Visible = True
                Dim dr As SqlDataReader
                dr = SqlHelper.ExecuteReader(strRegConn, CommandType.StoredProcedure, "sp_check_All", _
                New SqlParameter("@card_id", CommonUtility.Get_String(txtSearch.Text.Trim())))
                If dr.Read() Then
                    If CommonUtility.Get_StringValue(dr.Item("retStatus")) = "0" Then

                        txtReturnMsg.Text = "EDI" 'CommonUtility.Get_StringValue(dr.Item("retMessage"))
                        dr.Close()
                    Else
                        txtReturnMsg.Text = CommonUtility.Get_StringValue(dr.Item("retMessage"))
                    End If

                    'Load Profile
                    Dim strCommand As String
                    strCommand = "SELECT DISTINCT " & _
                                 " dbo.P_Rfcard.card_id, dbo.P_Rfcard.commit_name, dbo.P_Rfcard.company_taxno, dbo.P_Rfcard.company_branchNo, dbo.T_CompanyProfile.AddressNo, dbo.T_CompanyProfile.Building_Eng,  " & _
                                 " dbo.T_CompanyProfile.Soi_Eng, dbo.T_CompanyProfile.Road_Eng, dbo.T_CompanyProfile.Tumbol_Eng, dbo.T_CompanyProfile.Amphur_Eng,  " & _
                                 " dbo.T_CompanyProfile.Province_Eng, dbo.T_CompanyProfile.Zipcode, dbo.T_CompanyProfile.PhoneNo, dbo.T_CompanyProfile.FaxNo,  " & _
                                 " dbo.T_CompanyProfile.Authorize_Remark, dbo.P_Rfcard.card_level,  " & _
                                 " dbo.T_CompanyAuthorize.Title_Eng + ' ' + dbo.T_CompanyAuthorize.FirstName_Eng + ' ' + dbo.T_CompanyAuthorize.LastName_Eng AS FullName " & _
                                 " FROM dbo.P_Rfcard INNER JOIN " & _
                                 " dbo.T_CompanyProfile ON dbo.P_Rfcard.company_taxno = dbo.T_CompanyProfile.Company_Taxno LEFT OUTER JOIN " & _
                                 " dbo.T_CompanyAuthorize ON dbo.T_CompanyProfile.Company_Taxno = dbo.T_CompanyAuthorize.CompanyTaxNo " & _
                                 " WHERE (dbo.P_Rfcard.card_id = '" & txtSearch.Text.Trim() & "')"

                    Dim ds As New DataSet
                    ds = SqlHelper.ExecuteDataset(strRegConn, CommandType.Text, strCommand)
                    If ds.Tables(0).Rows.Count > 0 Then
                        For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                            txtCompanyName_Eng.Text = CommonUtility.Get_StringValue(ds.Tables(0).Rows(i).Item("commit_name"))
                            txtCompany_Taxno.Text = CommonUtility.Get_StringValue(ds.Tables(0).Rows(i).Item("company_taxno"))
                            txtCompany_BranchNo.Text = CommonUtility.Get_StringValue(ds.Tables(0).Rows(i).Item("company_branchNo"))
                            txtCompany_Address.Text = BuildingAddress(ds)
                            txtCompany_Phone.Text = CommonUtility.Get_StringValue(ds.Tables(0).Rows(i).Item("PhoneNo"))
                            txtCompany_Fax.Text = CommonUtility.Get_StringValue(ds.Tables(0).Rows(i).Item("FaxNo"))

                            If i = 0 Then txtAuthorize1.Text = CommonUtility.Get_StringValue(ds.Tables(0).Rows(i).Item("FullName"))
                            If i = 1 Then txtAuthorize2.Text = CommonUtility.Get_StringValue(ds.Tables(0).Rows(i).Item("FullName"))
                            If i = 2 Then txtAuthorize3.Text = CommonUtility.Get_StringValue(ds.Tables(0).Rows(i).Item("FullName"))
                            If i = 3 Then txtAuthorize4.Text = CommonUtility.Get_StringValue(ds.Tables(0).Rows(i).Item("FullName"))
                            If i = 4 Then txtAuthorize5.Text = CommonUtility.Get_StringValue(ds.Tables(0).Rows(i).Item("FullName"))
                            If i = 5 Then txtAuthorize6.Text = CommonUtility.Get_StringValue(ds.Tables(0).Rows(i).Item("FullName"))

                            txtAuthName_Eng.Text = CommonUtility.Get_StringValue(ds.Tables(0).Rows(i).Item("AuthName"))
                            txtCardLevel.Text = CommonUtility.Get_StringValue(ds.Tables(0).Rows(i).Item("card_level"))

                            txtCard_Id.Text = CommonUtility.Get_StringValue(ds.Tables(0).Rows(i).Item("card_id"))
                        Next

                        'Load Tab 2 รายการบัตร
                        rgCardList.DataSource = LoadCard()
                        rgCardList.DataBind()

                        'Load Tab 3 รายการเอกสารแนบ
                        rgDocAttach.DataSource = LoadDocAttach()
                        rgDocAttach.DataBind()

                        'Load Tab 4 บันทึกการกระทำความผิด
                        rgBackList.DataSource = LoadDocBackList()
                        rgBackList.DataBind()

                        'Load Tab 1 บันทึกการกระทำความผิด
                        grdBlackListTab1.DataSource = LoadDocBackList()
                        grdBlackListTab1.DataBind()
                        If grdBlackListTab1.MasterTableView.Items.Count = 0 Then
                            tblBlackList.Visible = False
                        End If

                        'Load รายการขอรับคำรับรอง
                        rgRequestForm.DataSource = LoadRequestForm("load")
                        rgRequestForm.DataBind()

                        txtFormNum.Focus()
                    Else
                        dr = SqlHelper.ExecuteReader(strRegConn, CommandType.StoredProcedure, "sp_check_All", _
                        New SqlParameter("@card_id", txtSearch.Text.Trim()))

                        If dr.Read() Then
                            If CommonUtility.Get_StringValue(dr.Item("retStatus")) = "0" Then

                                txtReturnMsg.Text = CommonUtility.Get_StringValue(dr.Item("retMessage"))
                                dr.Close()
                            Else
                                txtReturnMsg.Text = CommonUtility.Get_StringValue(dr.Item("retMessage"))
                            End If
                        End If

                        strCommand = "SELECT dbo.P_Company.*, dbo.P_Rfcard.* " & _
                                     "FROM dbo.P_Company INNER JOIN " & _
                                     "dbo.P_Rfcard ON dbo.P_Company.company_taxno = dbo.P_Rfcard.company_taxno " & _
                                     "WHERE card_id = '" & txtSearch.Text.Trim() & "'"
                        dr = SqlHelper.ExecuteReader(strRegConn, CommandType.Text, strCommand)
                        If dr.Read() Then
                            txtCompanyName_Eng.Text = CommonUtility.Get_StringValue(dr.Item("company_eng"))
                            txtCompany_Taxno.Text = CommonUtility.Get_StringValue(dr.Item("company_taxno"))
                            txtCompany_BranchNo.Text = CommonUtility.Get_StringValue(dr.Item("company_BranchNo"))
                            txtCompany_Address.Text = CommonUtility.Get_StringValue(dr.Item("address_eng"))
                            txtCompany_Phone.Text = CommonUtility.Get_StringValue(dr.Item("phone_no"))
                            txtCompany_Fax.Text = CommonUtility.Get_StringValue(dr.Item("fax_no"))
                            txtAuthorize1.Text = CommonUtility.Get_StringValue(dr.Item("authorize1"))
                            txtAuthorize2.Text = CommonUtility.Get_StringValue(dr.Item("authorize2"))
                            txtAuthorize3.Text = CommonUtility.Get_StringValue(dr.Item("authorize3"))
                            txtAuthorize4.Text = CommonUtility.Get_StringValue(dr.Item("authorize4"))
                            txtAuthorize5.Text = CommonUtility.Get_StringValue(dr.Item("authorize5"))
                            'txtAuthorize6.Text = CommonUtility.Get_StringValue(dr.Item("authorize3"))
                            txtAuthName_Eng.Text = CommonUtility.Get_StringValue(dr.Item("AuthName"))
                            txtCardLevel.Text = CommonUtility.Get_StringValue(dr.Item("card_level"))

                            txtCard_Id.Text = CommonUtility.Get_StringValue(dr.Item("card_id"))
                            dr.Close()
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

                        'Load Tab 1 บันทึกการกระทำความผิด
                        grdBlackListTab1.DataSource = LoadDocBackList()
                        grdBlackListTab1.DataBind()
                        If grdBlackListTab1.MasterTableView.Items.Count = 0 Then
                            tblBlackList.Visible = False
                        End If

                        'Load รายการคำร้อง
                        rgRequestForm.DataSource = LoadRequestForm("load")
                        rgRequestForm.DataBind()

                        txtFormNum.Focus()
                    End If
                End If
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End Sub

        Private Sub LoadAllForm()
            Try
                Dim ds As New DataSet
                ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, "vi_select_form", _
                New SqlParameter("@TCat", 4), _
                New SqlParameter("@form_type", ""))
                If ds.Tables(0).Rows.Count > 0 Then
                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                        rcbFormType.Items.Insert(i, New RadComboBoxItem("[" & ds.Tables(0).Rows(i).Item("form_code") & "] " & ds.Tables(0).Rows(i).Item("form_name"), ds.Tables(0).Rows(i).Item("form_type")))
                    Next
                    rcbFormType.SelectedIndex = 0
                End If
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End Sub

        Function BuildingAddress(ByVal ds As DataSet) As String
            Dim address As String = ""

            If Not (ds.Tables(0).Rows(0).Item("AddressNo").Equals(System.DBNull.Value)) And CommonUtility.Get_StringValue(ds.Tables(0).Rows(0).Item("AddressNo")) <> "" Then
                address = ds.Tables(0).Rows(0).Item("AddressNo") & " "
            End If

            If Not (ds.Tables(0).Rows(0).Item("Building_Eng").Equals(System.DBNull.Value)) And CommonUtility.Get_StringValue(ds.Tables(0).Rows(0).Item("Building_Eng")) <> "" Then
                address &= ds.Tables(0).Rows(0).Item("Building_Eng") & " "
            End If

            If Not (ds.Tables(0).Rows(0).Item("Soi_Eng").Equals(System.DBNull.Value)) And CommonUtility.Get_StringValue(ds.Tables(0).Rows(0).Item("Soi_Eng")) <> "" Then
                address &= "Soi " & ds.Tables(0).Rows(0).Item("Soi_Eng") & " "
            End If

            If Not (ds.Tables(0).Rows(0).Item("Road_Eng").Equals(System.DBNull.Value)) And CommonUtility.Get_StringValue(ds.Tables(0).Rows(0).Item("Road_Eng")) <> "" Then
                address &= ds.Tables(0).Rows(0).Item("Road_Eng") & " "
            End If

            If Not (ds.Tables(0).Rows(0).Item("Tumbol_Eng").Equals(System.DBNull.Value)) And CommonUtility.Get_StringValue(ds.Tables(0).Rows(0).Item("Tumbol_Eng")) <> "" Then
                address &= ds.Tables(0).Rows(0).Item("Tumbol_Eng") & " "
            End If

            If Not (ds.Tables(0).Rows(0).Item("Amphur_Eng").Equals(System.DBNull.Value)) And CommonUtility.Get_StringValue(ds.Tables(0).Rows(0).Item("Amphur_Eng")) <> "" Then
                address &= ds.Tables(0).Rows(0).Item("Amphur_Eng") & " "
            End If

            If Not (ds.Tables(0).Rows(0).Item("Province_Eng").Equals(System.DBNull.Value)) And CommonUtility.Get_StringValue(ds.Tables(0).Rows(0).Item("Province_Eng")) <> "" Then
                address &= ds.Tables(0).Rows(0).Item("Province_Eng") & " "
            End If

            If Not (ds.Tables(0).Rows(0).Item("Zipcode").Equals(System.DBNull.Value)) And CommonUtility.Get_StringValue(ds.Tables(0).Rows(0).Item("Zipcode")) <> "" Then
                address &= ds.Tables(0).Rows(0).Item("Zipcode")
            End If

            Return address
        End Function

        'Tab ที่ 1 แสดงรายการการขอรับหนังสือรับรอง
        Function LoadRequestForm(ByVal action As String) As DataTable
            Try
                Select Case action
                    Case "load"
                        Dim ds As New DataSet
                        objConn = New SqlConnection(strEDIConn)
                        Dim strCommand As String = "Select invh_run_auto, company_name, company_address as form_name, destination_address As AuthName_Thai From form_header_manual Where (1 = 2)"
                        ds = SqlHelper.ExecuteDataset(objConn, CommandType.Text, strCommand)
                        Return ds.Tables(0)
                    Case "page"
                        'ตรวจ Black List
                        Dim FormCode As String = CommonUtility.Get_StringValue(Left(txtFormNum.Text, 3))

                        Dim ds As New DataSet
                        objConn = New SqlConnection(strEDIConn)
                        ds = SqlHelper.ExecuteDataset(objConn, CommandType.StoredProcedure, "vi_gen_bookForm", _
                        New SqlParameter("@CARD_ID", CommonUtility.Get_StringValue(txtCard_Id.Text)), _
                        New SqlParameter("@SITE_ID", lblRoleID.Text), _
                        New SqlParameter("@form_code", CommonUtility.Get_StringValue(Left(txtFormNum.Text, 3))), _
                        New SqlParameter("@REQUEST_PERSON", CommonUtility.Get_StringValue(txtAuthName_Eng.Text)), _
                        New SqlParameter("@user_id", CommonUtility.Get_StringValue(UserInfo.Username)), _
                        New SqlParameter("@form_amount", CommonUtility.Get_Int32(Right(txtFormNum.Text, 2))))

                        If ds.Tables(0).Rows.Count > 0 Then
                            Dim dr As DataRow
                            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                                dr = dt.NewRow()
                                dr("invh_run_auto") = CommonUtility.Get_String(ds.Tables(0).Rows(i).Item("invh_run_auto"))
                                dr("form_name") = CommonUtility.Get_String(ds.Tables(0).Rows(i).Item("form_name"))
                                dr("company_name") = CommonUtility.Get_String(ds.Tables(0).Rows(i).Item("company_name"))
                                dr("AuthName_Thai") = CommonUtility.Get_String(ds.Tables(0).Rows(i).Item("AuthName_Thai"))

                                dt.Rows.Add(dr)
                            Next
                            Return dt
                        End If
                End Select
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End Function

        Private Sub rgRequestForm_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles rgRequestForm.DataBound
            objConn.Close()
        End Sub

        Private Sub rgRequestForm_PageIndexChanged(ByVal source As Object, ByVal e As Telerik.Web.UI.GridPageChangedEventArgs) Handles rgRequestForm.PageIndexChanged
            rgRequestForm.CurrentPageIndex = e.NewPageIndex
            rgRequestForm.DataSource = LoadRequestForm("page")
            rgRequestForm.DataBind()
        End Sub

        'Tab ที่ 2 รายการบัตร
        Private Sub rgCardList_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles rgCardList.DataBound
            objConn.Close()
        End Sub

        Function LoadCard() As DataTable
            Try
                Dim ds As New DataSet
                objConn = New SqlConnection(strRegConn)
                Dim strCommand As String = "SELECT * FROM P_rfcard Where company_taxno = '" & txtCompany_Taxno.Text.Trim() & "' Order By card_id"
                ds = SqlHelper.ExecuteDataset(objConn, CommandType.Text, strCommand)
                Return ds.Tables(0)
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End Function

        Private Sub rgCardList_PageIndexChanged(ByVal source As Object, ByVal e As Telerik.Web.UI.GridPageChangedEventArgs) Handles rgCardList.PageIndexChanged
            rgCardList.CurrentPageIndex = e.NewPageIndex
            rgCardList.DataSource = LoadCard()
            rgCardList.DataBind()
        End Sub

        'Load Tab 3 รายการเอกสารแนบ
        Private Sub rgDocAttach_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles rgDocAttach.DataBound
            objConn.Close()
        End Sub

        Function LoadDocAttach() As DataTable
            Try
                Dim ds As New DataSet
                objConn = New SqlConnection(strEDIConn)
                ds = SqlHelper.ExecuteDataset(objConn, CommandType.StoredProcedure, "vi_select_RepAtt1", _
                New SqlParameter("@TCat1", 2), _
                New SqlParameter("@TCat2", 3), _
                New SqlParameter("@dForm", ""), _
                New SqlParameter("@dTo", ""), _
                New SqlParameter("@total_day", 10), _
                New SqlParameter("@company_taxno", txtCompany_Taxno.Text), _
                New SqlParameter("@site_id", "ALL"), _
                New SqlParameter("@invh_run_auto", ""))
                Return ds.Tables(0)
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End Function

        Private Sub rgDocAttach_PageIndexChanged(ByVal source As Object, ByVal e As Telerik.Web.UI.GridPageChangedEventArgs) Handles rgDocAttach.PageIndexChanged
            rgDocAttach.CurrentPageIndex = e.NewPageIndex
            rgDocAttach.DataSource = LoadDocAttach()
            rgDocAttach.DataBind()
        End Sub

        'Load Tab 4 บันทึกการกระทำความผิด
        Private Sub rgBackList_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles rgBackList.DataBound
            objConn.Close()
        End Sub

        Function LoadDocBackList() As DataTable
            Try
                Dim ds As New DataSet
                objConn = New SqlConnection(strRegConn)
                Dim strCommand As String = ""
                strCommand = "SELECT dbo.BlackList.Auto_Run, dbo.BlackList.Company_Taxno, dbo.BlackList.CompanyName_Eng, dbo.BlackList.CompanyName_Th, dbo.BlackList.Type_Mistake, dbo.BlackList.Form_Mistake, dbo.BlackList.Country_Mistake, dbo.BlackList_Country.CountryName, dbo.BlackList_Form_Type.form_name, " & _
                             "dbo.BlackList.Product_Mistake, dbo.BlackList_Desc.BlackList_Desc, dbo.BlackList.Status_Mistake, dbo.BlackList.Date_Rec_Mistake,  " & _
                             "dbo.BlackList.User_Rec_Mistake, dbo.BlackList.Site_Rec_Mistake, dbo.BlackList.Sys_Date_Mistake, dbo.BlackList.Desc_Cancel_Mistake,  " & _
                             "dbo.BlackList.Date_Rec_Cancel_Mistake, dbo.BlackList.User_Rec_Cancel_Mistake, dbo.BlackList.Site_Rec_Cancel_Mistake,  " & _
                             "dbo.BlackList.Sys_Date_Cancel_Mistake " & _
                             "FROM dbo.BlackList INNER JOIN " & _
                             "dbo.BlackList_Country ON dbo.BlackList.Country_Mistake = dbo.BlackList_Country.CountryCode INNER JOIN " & _
                             "dbo.BlackList_Form_Type ON dbo.BlackList.Form_Mistake = dbo.BlackList_Form_Type.form_type INNER JOIN " & _
                             "dbo.BlackList_Desc ON dbo.BlackList.Desc_Mistake = dbo.BlackList_Desc.Desc_No " & _
                             "WHERE (dbo.BlackList.Company_Taxno = N'" & txtCompany_Taxno.Text.Trim() & "') and (dbo.BlackList.Status_Mistake <> 'D')"
                ds = SqlHelper.ExecuteDataset(objConn, CommandType.Text, strCommand)
                Return ds.Tables(0)
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End Function

        Private Sub rgBackList_PageIndexChanged(ByVal source As Object, ByVal e As Telerik.Web.UI.GridPageChangedEventArgs) Handles rgBackList.PageIndexChanged
            rgBackList.CurrentPageIndex = e.NewPageIndex
            rgBackList.DataSource = LoadDocBackList()
            rgBackList.DataBind()
        End Sub

        Protected Sub btnInsertItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnInsertItem.Click
            Try
                rgRequestForm.DataSource = LoadRequestForm("page")
                rgRequestForm.DataBind()
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End Sub

        Private Sub grdBlackListTab1_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles grdBlackListTab1.ItemDataBound
            If TypeOf e.Item Is GridDataItem Then
                Dim dataItem As GridDataItem = CType(e.Item, GridDataItem)
                If dataItem("Type_Mistake").Text = "B" Then
                    dataItem("Type_Mistake").Text = "Black List"
                ElseIf dataItem("Type_Mistake").Text = "W" Then
                    dataItem("Type_Mistake").Text = "Watch List"
                End If

                If dataItem("Country_Mistake").Text = "00" Or dataItem("Form_Mistake").Text = "ALL" Then
                    tblRefList.Visible = False
                End If

                If dataItem("Form_Mistake").Text <> "ALL" Then
                    rcbFormType.Items.Remove(rcbFormType.Items.FindItemByValue(dataItem("Form_Mistake").Text))
                End If
            End If
        End Sub

        Protected Sub btnPrintBarcode_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrintBarcode.Click
            Try
                'vi_select_form 5, '01'
                Dim _Number As String
                _Number = Right("00" & CommonUtility.Get_StringValue(txtNumber.Value), 2)
                Response.Write("<script language ='javascript'> window.open('http://" & DotNetNuke.Common.GetDomainName(Request) & "/DesktopModules/DFT_EDI_BookingForm/frmBarcode.aspx?Number=" & _Number & "',null,'height=600, width=800,status=yes, resizable= yes,scrollbars=no, toolbar=no,location=no,menubar=no'); </script>")
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End Sub
    End Class

End Namespace
