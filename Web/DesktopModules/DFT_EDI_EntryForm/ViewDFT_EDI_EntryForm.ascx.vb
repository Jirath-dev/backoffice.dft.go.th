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

Namespace NTi.Modules.DFT_EDI_EntryForm
    Partial Class ViewDFT_EDI_EntryForm
        Inherits Entities.Modules.PortalModuleBase
        Dim strRegConn As String = ConfigurationManager.ConnectionStrings("RegisBackConnection").ConnectionString
        Dim strEDIConn As String = ConfigurationManager.ConnectionStrings("OriginConnection").ConnectionString
        Dim objConn As SqlConnection = Nothing
        Protected _CompanyTaxno As String

        Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            txtInvh_Run_Auto2.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" + btnSearch.UniqueID + "').click();return false;}} else {return true}; ")

            If Not Page.IsPostBack Then
                txtInvh_Run_Auto1.Text = FunctionUtility.DMY2YMD(Today)
                txtInvh_Run_Auto2.Focus()

                drpQ_UNIT_CODE1.SelectedValue = "KGS"
                drpQ_UNIT_CODE2.SelectedValue = "KGS"

                tblForm.Visible = False

                trOriginCountry.Visible = False
                Session("ssUserName") = UserInfo.Username
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
            Call LoadRequest()
        End Sub

        Private Sub LoadRequest()
            Try
                Dim dr As SqlDataReader
                Dim strCommand As String
                Dim invh As String = (CInt(txtInvh_Run_Auto2.Text)).ToString("000000")
                txtInvh_Run_Auto2.Text = invh
                strCommand = "select a.*,b.FORM_NAME from FORM_HEADER_MANUAL a left outer join FORM_TYPE b on a.FORM_TYPE=b.FORM_TYPE where INVH_RUN_AUTO='" & txtInvh_Run_Auto1.Text & "-" & invh & "' and SITE_ID='" & lblRoleID.Text & "'"
                dr = SqlHelper.ExecuteReader(strEDIConn, CommandType.Text, strCommand)
                If dr.HasRows Then
                    dr.Read()
                    Call SetTable(CommonUtility.Get_StringValue(dr.Item("FORM_TYPE")))

                    btnSearch.Visible = False
                    txtInvh_Run_Auto1.ReadOnly = True
                    txtInvh_Run_Auto2.ReadOnly = True

                    lblCompany_Taxno.Text = CommonUtility.Get_StringValue(dr.Item("company_taxno"))
                    txtCompanyName.Text = CommonUtility.Get_StringValue(dr.Item("company_name"))
                    txtCardID.Text = CommonUtility.Get_StringValue(dr.Item("card_id"))
                    lblForm_Type.Text = CommonUtility.Get_StringValue(dr.Item("form_type"))
                    lblForm_Name.Text = CommonUtility.Get_StringValue(dr.Item("FORM_NAME"))

                    Call LoadDestinationCountry(CommonUtility.Get_StringValue(dr.Item("FORM_TYPE")))

                    If Not dr.Item("destination_country").Equals(System.DBNull.Value) Then ddlDestination_Country.SelectedValue = CommonUtility.Get_StringValue(dr.Item("destination_country"))
                    If Not dr.Item("gross_weight").Equals(System.DBNull.Value) Then txtQuantity1.Value = CommonUtility.Get_Decimal(dr.Item("gross_weight"))
                    If Not dr.Item("g_unit_code").Equals(System.DBNull.Value) Then drpQ_UNIT_CODE1.SelectedValue = CommonUtility.Get_StringValue(dr.Item("g_unit_code")) Else drpQ_UNIT_CODE1.SelectedValue = "KGS"
                    If Not dr.Item("quantity5").Equals(System.DBNull.Value) Then txtQuantity2.Value = CommonUtility.Get_Decimal(dr.Item("quantity5"))
                    If Not dr.Item("q_unit_code5").Equals(System.DBNull.Value) Then drpQ_UNIT_CODE2.SelectedValue = CommonUtility.Get_StringValue(dr.Item("q_unit_code5")) Else drpQ_UNIT_CODE2.SelectedValue = "KGS"

                    Session("ssCompanyTaxno") = CommonUtility.Get_StringValue(lblCompany_Taxno.Text)
                    Session("ssInvh_Run_Auto") = CommonUtility.Get_StringValue(txtInvh_Run_Auto1.Text & "-" & invh)
                    Session("ssForm_Type") = CommonUtility.Get_StringValue(lblForm_Type.Text)
                    Session("ssCard_id") = CommonUtility.Get_StringValue(txtCardID.Text)

                    lblEDI_Status_ID.Text = CommonUtility.Get_StringValue(dr.Item("edi_status_id"))
                    lblDistributeForm.Text = CommonUtility.Get_StringValue(dr.Item("distribute"))
                    lblReferenceCode2.Text = CommonUtility.Get_StringValue(dr.Item("reference_code2"))

                    Session("ssReferenceCode2") = lblReferenceCode2.Text
                    dr.Close()

                    rgItemDetail.DataSource = LoadDetail_Manual()
                    rgItemDetail.DataBind()
                    tblForm.Visible = True

                    Select Case lblEDI_Status_ID.Text
                        Case "A"
                            lblStatus_Name.Text = "อนุมัติ"
                            lblReferenceCode2.Visible = True
                            btnDistribute.Visible = True
                            lblSentForm.Visible = True
                        Case "N"
                            lblStatus_Name.Text = "ไม่อนุมัติ"
                            lblReferenceCode2.Visible = True
                            btnDistribute.Visible = False
                            lblSentForm.Visible = False
                        Case "C"
                            lblStatus_Name.Text = "ยกเลิก"
                            lblReferenceCode2.Visible = True
                            btnDistribute.Visible = False
                            lblSentForm.Visible = False
                        Case "E"
                            lblStatus_Name.Text = "อนุมัติ/แก้ไข"
                            lblReferenceCode2.Visible = True
                            btnDistribute.Visible = True
                            lblSentForm.Visible = True
                        Case "B"
                            lblReferenceCode2.Visible = False
                            lblStatus_Name.Visible = False
                            btnDistribute.Visible = False
                            lblSentForm.Visible = False
                    End Select
                    
                    Select Case lblDistributeForm.Text
                        Case "N"
                            lblSentForm.Text = "ยังไม่ได้จ่ายออก"
                        Case "Y"
                            lblSentForm.Text = "xxxxxx"
                    End Select
                Else
                    tblForm.Visible = False
                    Me.RadAjaxManager1.ResponseScripts.Add("window.alert('ไม่พบข้อมูลฟอร์มหมายเลข " & txtInvh_Run_Auto1.Text & "-" & invh & " !!!');")
                End If
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End Sub

        Private Sub LoadDestinationCountry(ByVal _FormType As String)
            Try
                Dim strcommand As String
                Select Case _FormType
                    Case "FORM1"
                        strcommand = "SELECT country_code, country_name FROM country " & _
                                     "WHERE (form_type = '" & _FormType & "') AND (country_name IN ('Austria', 'Belgium', 'Bulgaria', 'Cyprus', 'Czech Republic', 'Denmark', 'Estonia', 'Finland', 'France', 'Germany', 'Greece', " & _
                                     "'Hungary', 'Ireland', 'Italy', 'Latvia', 'Lithuania', 'Luxembourg', 'Malta', 'Netherlands', 'Poland', 'Portugal', 'Romania', 'Slovakia', 'Slovenia', 'Spain', 'Sweden', " & _
                                     "'United Kingdom', 'CANADA', 'TURKEY', 'JAPAN', 'NORWAY', 'RUSSIAN FEDERATION', 'SWITZERLAND')) " & _
                                     "ORDER BY country_name"
                    Case "FORM1_3"
                        strcommand = "SELECT country_code, country_name FROM country " & _
                                     "WHERE (form_type = 'FORM1_3') AND (country_name LIKE '%EU%') AND (NOT (country_code IN ('A0', 'A7'))) " & _
                                     "ORDER BY country_name"
                    Case Else
                        strcommand = "select COUNTRY_CODE,COUNTRY_NAME from COUNTRY where FORM_TYPE='" & _FormType & "' ORDER BY COUNTRY_NAME"
                End Select
                Dim ds As New DataSet
                ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.Text, strcommand)
                If ds.Tables(0).Rows.Count > 0 Then
                    ddlDestination_Country.DataSource = ds.Tables(0)
                    ddlDestination_Country.DataTextField = "COUNTRY_NAME"
                    ddlDestination_Country.DataValueField = "COUNTRY_CODE"
                    ddlDestination_Country.DataBind()
                    ddlDestination_Country.SelectedIndex = 0
                    Session("ssCountry_code") = CommonUtility.Get_StringValue(ddlDestination_Country.SelectedValue)
                End If
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End Sub

        Private Sub LoadOriginCountry(ByVal Form_Type As String)
            Try
                Dim strCommand As String
                Select Case Form_Type
                    Case "FORM1"
                        strCommand = "SELECT country_code, REPLACE(country_name, '/EU', '') AS country_name FROM country " & _
                                     "WHERE (form_type = 'FORM1') AND (country_name LIKE '%EU%') " & _
                                     "ORDER BY country_name"
                    Case "FORM1_3"
                        strCommand = "SELECT country_code, country_name FROM country " & _
                                     "WHERE (form_type = 'FORM1_3') AND (country_name LIKE '%EU%') AND (NOT (country_code IN ('A7'))) " & _
                                     "ORDER BY country_name"
                    Case Else
                        strCommand = "SELECT COUNTRY_CODE,COUNTRY_NAME from COUNTRY where FORM_TYPE='" & Form_Type & "' ORDER BY COUNTRY_NAME"
                End Select
                Dim ds As New DataSet
                ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.Text, strcommand)
                If ds.Tables(0).Rows.Count > 0 Then
                    ddlOrigin_Country.DataSource = ds.Tables(0)
                    ddlOrigin_Country.DataTextField = "COUNTRY_NAME"
                    ddlOrigin_Country.DataValueField = "COUNTRY_CODE"
                    ddlOrigin_Country.DataBind()
                    ddlOrigin_Country.SelectedValue = "A0"
                End If
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End Sub

        Function LoadDetail_Manual() As DataTable
            Try
                Dim invh As String = (CInt(txtInvh_Run_Auto2.Text)).ToString("000000")
                Dim strCommand As String = "select * from FORM_DETAIL_MANUAL where INVH_RUN_AUTO='" & txtInvh_Run_Auto1.Text & "-" & invh & "' and SITE_ID='" & lblRoleID.Text & "' order by INVD_RUN_AUTO desc"
                Dim ds As New DataSet
                ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.Text, strCommand)
                Return ds.Tables(0)
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End Function

        Private Sub RadAjaxManager1_AjaxRequest(ByVal sender As Object, ByVal e As Telerik.Web.UI.AjaxRequestEventArgs) Handles RadAjaxManager1.AjaxRequest
            Call LoadRequest()


        End Sub

        Private Sub rgItemDetail_ItemCreated(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgItemDetail.ItemCreated
            If TypeOf e.Item Is GridDataItem Then
                Dim editLink As HyperLink = DirectCast(e.Item.FindControl("EditLink"), HyperLink)
                editLink.Attributes("href") = "#"
                editLink.Attributes("onclick") = [String].Format("return ShowEditItem('{0}','{1}','{2}','{3}','{4}','{5}','{6}');", e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("invh_run_auto"), _
                e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("invd_run_auto"), _
                e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("tariff_code"), _
                e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("certoforigin_no"), _
                e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("product_description"), _
                e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("net_weight"), _
                e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("fob_amt"))

                Dim deleteLink As HyperLink = DirectCast(e.Item.FindControl("DeleteLink"), HyperLink)
                deleteLink.Attributes("href") = "#"
                deleteLink.Attributes("onclick") = [String].Format("return ShowDeleteItem('{0}','{1}');", e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("invh_run_auto"), e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("invd_run_auto"))
            End If
        End Sub

        Private Sub SetTable(ByVal FormType As String)
            Try
                Select Case FormType
                    Case "FORM1", "FORM1_1", "FORM1_2", "FORM1_3", "FORM1_4", "FORMRussia", "FORM4" 'Back
                        tblThird.Visible = False
                        tblBack.Visible = True

                        Select Case FormType
                            Case "FORM1"
                                tblBack.Visible = False
                                trBackReferenceNo.Visible = False
                                trBackQuantity.Visible = False

                                trOriginCountry.Visible = True 'ประเทศแหล่งกำเนิด
                                Call LoadOriginCountry(FormType)
                            Case "FORM1_3"
                                tblBack.Visible = False
                                trBackReferenceNo.Visible = False
                                trBackQuantity.Visible = False

                                trOriginCountry.Visible = True 'ประเทศแหล่งกำเนิด
                                Call LoadOriginCountry(FormType)
                            Case "FORM4"
                                trBackReferenceNo.Visible = True
                                trBackQuantity.Visible = True

                                trOriginCountry.Visible = False 'ประเทศแหล่งกำเนิด
                            Case Else
                                trBackReferenceNo.Visible = False
                                trBackQuantity.Visible = False
                                trOriginCountry.Visible = False 'ประเทศแหล่งกำเนิด
                        End Select

                    Case "FORM2", "FORM4_2", "FORM4_3", "FORM4_5" 'Third
                        tblThird.Visible = True
                        tblBack.Visible = False

                    Case "FORM4_1", "FORM4_6", "FORM4_8" 'มีทั้ง 2 อย่าง
                        tblThird.Visible = True
                        tblBack.Visible = True

                    Case "FORM3", "FORM2_1", "FORM2_2", "FORM2_3", "FORM4_4", "FORM5", "FORM6", "FORM7", "FORM8", "FORM9" 'ไม่มีทั้ง 2 อย่าง
                        tblThird.Visible = False
                        tblBack.Visible = False
                End Select
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End Sub
    End Class

End Namespace
