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

Namespace NTi.Modules.DFT_EDI_CancelCertificate
    Partial Class ViewDFT_EDI_CancelCertificate
        Inherits Entities.Modules.PortalModuleBase
        Dim strEDIConn As String = ConfigurationManager.ConnectionStrings("OriginConnection").ConnectionString

        'by rut
        Dim _UserName As String = CommonUtility.Get_StringValue(DotNetNuke.Entities.Users.UserController.GetUser(PortalId, UserId, False).Username)

        Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            txtReferenceCode2.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" + btnSearch.UniqueID + "').click();return false;}} else {return true}; ")

            If Not Page.IsPostBack Then
                tblHeader.Visible = False
                txtReferenceCode2.Focus()
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
            'by rut แก้เรื่องการ ยกเลิกข้ามสาขา
            Select Case Mid(_UserName, 1, 5)
                Case "admin"
                    Call LoadHeader(txtReferenceCode2.Text.Trim())
                Case Else
                    Select Case Check_Siteid(txtReferenceCode2.Text.Trim(), lblRoleID.Text)
                        Case True
                            Call LoadHeader(txtReferenceCode2.Text.Trim())
                        Case False
                            Me.RadAjaxManager1.ResponseScripts.Add("window.alert('" & "ไม่สามารถยกเลิกหนังสือรับรองเลขที่  " & txtReferenceCode2.Text.Trim() & " ได้เนื่องจาก สาขาที่พิมพ์หนังสือรับรอง กับ สาขาที่ยกเลิกคนละที่" & "');")
                            btnCencelCertificate.Enabled = False
                    End Select
            End Select
        End Sub
#Region "by rut แก้เรื่องยกเลิกข้ามสาขาได้"
        Function Check_Siteid(ByVal By_reference_code2 As String, ByVal By_site As String) As Boolean
            Dim Site_Check As Boolean = True

            Dim sql As String = "SELECT     invh_run_auto, form_no, company_taxno, card_id, edi_status_id, site_id, reference_code1, reference_code2 " & _
"FROM         form_header_edi " & _
"WHERE     (reference_code2 = @reference_code2)"

            Dim ds As DataSet
            Dim prm As SqlClient.SqlParameter = New SqlClient.SqlParameter("@reference_code2", By_reference_code2)
            ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.Text, sql, prm)

            If ds.Tables(0).Rows.Count > 0 Then
                If ds.Tables(0).Rows(0).Item("site_id") = By_site Then
                    Site_Check = True 'สาขาเดียวกัน
                Else
                    Site_Check = False 'ไม่ใช่สาขาเดียวกัน
                End If
            End If

            Return Site_Check
        End Function
#End Region
        Private Sub LoadHeader(ByVal ReferenceCode2 As String)
            Try
                Dim dr As SqlDataReader
                Dim strCommand As String
                If check_YearsOle.Checked = True Then
                    Select Case DDLYears.SelectedValue
                        Case 0 '2009-2010
                            strCommand = "select a.*,b.DESCRIPTION,c.FORM_NAME,d.* from form_header_edi_2009_2010 a left outer join EDI_STATUS b on a.EDI_STATUS_ID=b.EDI_STATUS_ID left outer join FORM_TYPE c on a.FORM_TYPE=c.FORM_TYPE left outer join COMPANY d on a.COMPANY_TAXNO=d.COMPANY_TAXNO where  (a.EDI_STATUS_ID='P' or a.EDI_STATUS_ID='A' or a.EDI_STATUS_ID='D') and a.REFERENCE_CODE2='" & ReferenceCode2 & "'"
                        Case 1 '2011-2012
                            strCommand = "select a.*,b.DESCRIPTION,c.FORM_NAME,d.* from form_header_edi_2011_2012 a left outer join EDI_STATUS b on a.EDI_STATUS_ID=b.EDI_STATUS_ID left outer join FORM_TYPE c on a.FORM_TYPE=c.FORM_TYPE left outer join COMPANY d on a.COMPANY_TAXNO=d.COMPANY_TAXNO where  (a.EDI_STATUS_ID='P' or a.EDI_STATUS_ID='A' or a.EDI_STATUS_ID='D') and a.REFERENCE_CODE2='" & ReferenceCode2 & "'"
                        Case 2 '2013-2014
                            strCommand = "select a.*,b.DESCRIPTION,c.FORM_NAME,d.* from form_header_edi_2013_2014 a left outer join EDI_STATUS b on a.EDI_STATUS_ID=b.EDI_STATUS_ID left outer join FORM_TYPE c on a.FORM_TYPE=c.FORM_TYPE left outer join COMPANY d on a.COMPANY_TAXNO=d.COMPANY_TAXNO where  (a.EDI_STATUS_ID='P' or a.EDI_STATUS_ID='A' or a.EDI_STATUS_ID='D') and a.REFERENCE_CODE2='" & ReferenceCode2 & "'"
                        Case 3 '2015-2016
                            strCommand = "select a.*,b.DESCRIPTION,c.FORM_NAME,d.* from form_header_edi_2015_2016 a left outer join EDI_STATUS b on a.EDI_STATUS_ID=b.EDI_STATUS_ID left outer join FORM_TYPE c on a.FORM_TYPE=c.FORM_TYPE left outer join COMPANY d on a.COMPANY_TAXNO=d.COMPANY_TAXNO where  (a.EDI_STATUS_ID='P' or a.EDI_STATUS_ID='A' or a.EDI_STATUS_ID='D') and a.REFERENCE_CODE2='" & ReferenceCode2 & "'"
                        Case 4 '2017-2018
                            strCommand = "select a.*,b.DESCRIPTION,c.FORM_NAME,d.* from form_header_edi_2017_2018 a left outer join EDI_STATUS b on a.EDI_STATUS_ID=b.EDI_STATUS_ID left outer join FORM_TYPE c on a.FORM_TYPE=c.FORM_TYPE left outer join COMPANY d on a.COMPANY_TAXNO=d.COMPANY_TAXNO where  (a.EDI_STATUS_ID='P' or a.EDI_STATUS_ID='A' or a.EDI_STATUS_ID='D') and a.REFERENCE_CODE2='" & ReferenceCode2 & "'"
                        Case 5 '2019
                            strCommand = "select a.*,b.DESCRIPTION,c.FORM_NAME,d.* from form_header_edi_2019 a left outer join EDI_STATUS b on a.EDI_STATUS_ID=b.EDI_STATUS_ID left outer join FORM_TYPE c on a.FORM_TYPE=c.FORM_TYPE left outer join COMPANY d on a.COMPANY_TAXNO=d.COMPANY_TAXNO where  (a.EDI_STATUS_ID='P' or a.EDI_STATUS_ID='A' or a.EDI_STATUS_ID='D') and a.REFERENCE_CODE2='" & ReferenceCode2 & "'"
                    End Select
                Else
                    strCommand = "select a.*,b.DESCRIPTION,c.FORM_NAME,d.* from FORM_HEADER_EDI a left outer join EDI_STATUS b on a.EDI_STATUS_ID=b.EDI_STATUS_ID left outer join FORM_TYPE c on a.FORM_TYPE=c.FORM_TYPE left outer join COMPANY d on a.COMPANY_TAXNO=d.COMPANY_TAXNO where  (a.EDI_STATUS_ID='P' or a.EDI_STATUS_ID='A' or a.EDI_STATUS_ID='D') and a.REFERENCE_CODE2='" & ReferenceCode2 & "'"
                End If

                dr = SqlHelper.ExecuteReader(strEDIConn, CommandType.Text, strCommand)
                If dr.HasRows Then
                    tblHeader.Visible = True
                    dr.Read()

                    lblInvh_Run_Auto.Text = CommonUtility.Get_StringValue(CheckNullOrEmpty(dr.Item("invh_run_auto")))
                    lblReferenceCode2.Text = CommonUtility.Get_StringValue(CheckNullOrEmpty(dr.Item("reference_code2")))
                    lblRequestPerson.Text = CommonUtility.Get_StringValue(CheckNullOrEmpty(dr.Item("request_person")))
                    lblCompanyName.Text = CommonUtility.Get_StringValue(CheckNullOrEmpty(dr.Item("company_name")))
                    lblCompanyTaxNo.Text = CommonUtility.Get_StringValue(CheckNullOrEmpty(dr.Item("company_taxno")))
                    lblCompanyAddress.Text = CommonUtility.Get_StringValue(CheckNullOrEmpty(dr.Item("company_address")))
                    lblCompanyProvice.Text = CommonUtility.Get_StringValue(CheckNullOrEmpty(dr.Item("company_province")))
                    lblCompanyCountry.Text = CommonUtility.Get_StringValue(CheckNullOrEmpty(dr.Item("company_country")))
                    lblCompanyPhone.Text = CommonUtility.Get_StringValue(CheckNullOrEmpty(dr.Item("company_phone")))
                    lblCompanyFax.Text = CommonUtility.Get_StringValue(CheckNullOrEmpty(dr.Item("company_fax")))
                    lblCardID.Text = CommonUtility.Get_StringValue(CheckNullOrEmpty(dr.Item("card_id")))
                    lblsiteid.Text = CommonUtility.Get_StringValue(CheckNullOrEmpty(dr.Item("site_id")))

                    lblDestinationCompany.Text = CommonUtility.Get_StringValue(CheckNullOrEmpty(dr.Item("destination_company")))
                    lblDestinationAddress.Text = CommonUtility.Get_StringValue(CheckNullOrEmpty(dr.Item("destination_address")))
                    lblDestinationProvince.Text = CommonUtility.Get_StringValue(CheckNullOrEmpty(dr.Item("destination_province")))
                    lblDestReceiveCountry.Text = CommonUtility.Get_StringValue(CheckNullOrEmpty(dr.Item("destination_country")))

                    radShipBy.SelectedValue = CommonUtility.Get_StringValue(CheckNullOrEmpty(dr.Item("ship_by")))
                    lblTransportBy.Text = CommonUtility.Get_StringValue(CheckNullOrEmpty(dr.Item("transport_by")))
                    lblInvoiceBoard.Text = CommonUtility.Get_StringValue(CheckNullOrEmpty(dr.Item("invoice_board")))
                    lblInvoiceNo1.Text = CommonUtility.Get_StringValue(CheckNullOrEmpty(dr.Item("invoice_no1")))
                    If Not dr.Item("invoice_date1").Equals(System.DBNull.Value) Then txtInvoiceDate1.SelectedDate = CommonUtility.Get_DateTime(dr.Item("invoice_date1"))
                    lblInvoiceNo2.Text = CommonUtility.Get_StringValue(CheckNullOrEmpty(dr.Item("invoice_no2")))
                    If Not dr.Item("invoice_date2").Equals(System.DBNull.Value) Then txtInvoiceDate2.SelectedDate = CommonUtility.Get_DateTime(dr.Item("invoice_date2"))
                    lblInvoiceNo3.Text = CommonUtility.Get_StringValue(CheckNullOrEmpty(dr.Item("invoice_no3")))
                    If Not dr.Item("invoice_date3").Equals(System.DBNull.Value) Then txtInvoiceDate3.SelectedDate = CommonUtility.Get_DateTime(dr.Item("invoice_date3"))
                    lblInvoiceNo4.Text = CommonUtility.Get_StringValue(CheckNullOrEmpty(dr.Item("invoice_no4")))
                    If Not dr.Item("invoice_date4").Equals(System.DBNull.Value) Then txtInvoiceDate4.SelectedDate = CommonUtility.Get_DateTime(dr.Item("invoice_date4"))
                    lblInvoiceNo5.Text = CommonUtility.Get_StringValue(CheckNullOrEmpty(dr.Item("invoice_no5")))
                    If Not dr.Item("invoice_date5").Equals(System.DBNull.Value) Then txtInvoiceDate5.SelectedDate = CommonUtility.Get_DateTime(dr.Item("invoice_date5"))
                    radBillType.SelectedValue = CommonUtility.Get_StringValue(CheckNullOrEmpty(dr.Item("bill_type")))
                    lblBillTypeOther.Text = CommonUtility.Get_StringValue(CheckNullOrEmpty(dr.Item("bill_type_other")))
                    lblBlNo.Text = CommonUtility.Get_StringValue(CheckNullOrEmpty(dr.Item("bl_no")))
                    If Not dr.Item("sailing_date").Equals(System.DBNull.Value) Then txtSailingDate.SelectedDate = CommonUtility.Get_DateTime(dr.Item("sailing_date"))
                    If Not dr.Item("edi_date").Equals(System.DBNull.Value) Then txtEdiDate.SelectedDate = CommonUtility.Get_DateTime(dr.Item("edi_date"))

                    lblAttachFile.Text = CommonUtility.Get_StringValue(CheckNullOrEmpty(dr.Item("attach_file")))

                    lblFactory.Text = CommonUtility.Get_StringValue(CheckNullOrEmpty(dr.Item("factory")))
                    lblFactoryTaxID.Text = CommonUtility.Get_StringValue(CheckNullOrEmpty(dr.Item("factory_taxid")))
                    lblFactoryAddress.Text = CommonUtility.Get_StringValue(CheckNullOrEmpty(dr.Item("factory_address")))
                    lblFactoryProvince.Text = CommonUtility.Get_StringValue(CheckNullOrEmpty(dr.Item("factory_province")))
                    lblFactoryCountry.Text = CommonUtility.Get_StringValue(CheckNullOrEmpty(dr.Item("factory_country")))
                    lblFactoryPhone.Text = CommonUtility.Get_StringValue(CheckNullOrEmpty(dr.Item("factory_phone")))
                    lblFactoryFax.Text = CommonUtility.Get_StringValue(CheckNullOrEmpty(dr.Item("factory_fax")))

                    dr.Close()
                Else
                    tblHeader.Visible = False
                    Me.RadAjaxManager1.ResponseScripts.Add("window.alert('" & "ไม่พบหมายเลขหนังสือรับรองเลขที่  " & txtReferenceCode2.Text.Trim() & "');")
                    txtReferenceCode2.Text = ""
                    txtReferenceCode2.Focus()
                End If
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End Sub

        Function CheckNullOrEmpty(ByVal item As Object) As String
            Try
                If item.Equals(System.DBNull.Value) Or CommonUtility.Get_StringValue(item) = "" Then
                    Return "-"
                Else
                    Return item.ToString()
                End If
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End Function

        Protected Sub btnCencelCertificate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCencelCertificate.Click
            Try
                Dim ds As New DataSet
                Dim TmpStore As String = ""
                Dim strCommand As String = ""
                If check_YearsOle.Checked = True Then
                    Select Case DDLYears.SelectedValue
                        Case 0 '2009-2010
                            TmpStore = "sp_cancelForm_v2_2009_2010"
                            strCommand = " Select H.*, D.* FROM dbo.form_header_edi_2009_2010 H INNER JOIN dbo.form_detail_edi_2009_2010 D ON H.invh_run_auto = D.invh_run_auto
                                WHERE (H.invh_run_auto = '" & lblInvh_Run_Auto.Text.Trim() & "') "
                        Case 1 '2011-2012
                            TmpStore = "sp_cancelForm_v2_2011_2012"
                            strCommand = " Select H.*, D.* FROM dbo.form_header_edi_2011_2012 H INNER JOIN dbo.form_detail_edi_2011_2012 D ON H.invh_run_auto = D.invh_run_auto
                                WHERE (H.invh_run_auto = '" & lblInvh_Run_Auto.Text.Trim() & "') "
                        Case 2 '2013-2014
                            TmpStore = "sp_cancelForm_v2_2013_2014"
                            strCommand = " Select H.*, D.* FROM dbo.form_header_edi_2013_2014 H INNER JOIN dbo.form_detail_edi_2013_2014 D ON H.invh_run_auto = D.invh_run_auto
                                WHERE (H.invh_run_auto = '" & lblInvh_Run_Auto.Text.Trim() & "') "
                        Case 3 '2015-2016
                            TmpStore = "sp_cancelForm_v2_2015_2016"
                            strCommand = " Select H.*, D.* FROM dbo.form_header_edi_2015_2016 H INNER JOIN dbo.form_detail_edi_2015_2016 D ON H.invh_run_auto = D.invh_run_auto
                                WHERE (H.invh_run_auto = '" & lblInvh_Run_Auto.Text.Trim() & "') "
                        Case 4 '2017-2018
                            TmpStore = "sp_cancelForm_v2_2017_2018"
                            strCommand = " Select H.*, D.* FROM dbo.form_header_edi_2017_2018 H INNER JOIN dbo.form_detail_edi_2017_2018 D ON H.invh_run_auto = D.invh_run_auto
                                WHERE (H.invh_run_auto = '" & lblInvh_Run_Auto.Text.Trim() & "') "
                        Case 5 '2019
                            TmpStore = "sp_cancelForm_v2_2019"
                            strCommand = " Select H.*, D.* FROM dbo.form_header_edi_2019 H INNER JOIN dbo.form_detail_edi_2019 D ON H.invh_run_auto = D.invh_run_auto
                                WHERE (H.invh_run_auto = '" & lblInvh_Run_Auto.Text.Trim() & "') "
                    End Select
                Else
                    TmpStore = "sp_cancelForm_v2"
                    strCommand = " Select H.*, D.* FROM dbo.form_header_edi H INNER JOIN dbo.form_detail_edi D ON H.invh_run_auto = D.invh_run_auto
                                WHERE (H.invh_run_auto = '" & lblInvh_Run_Auto.Text.Trim() & "') "
                End If

                ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, TmpStore,
                    New SqlParameter("@INVH_RUN_AUTO", lblInvh_Run_Auto.Text.Trim),
                    New SqlParameter("@Cancel_Date", CommonUtility.Get_DateTime(Now)),
                    New SqlParameter("@Cancel_By", CommonUtility.Get_StringValue(UserInfo.Username)),
                    New SqlParameter("@remark", CommonUtility.Get_StringValue(txtRemark.Text)))


                If CommonUtility.Get_Int32(ds.Tables(0).Rows(0).Item("retSTATUS")) = 0 Then
                    'QUOTA START
                    'Dim strcommand As String = ""

                    'strcommand = "Select dbo.form_header_edi.*, dbo.form_detail_edi.* " &
                    '             "FROM dbo.form_header_edi INNER JOIN " &
                    '             "dbo.form_detail_edi ON dbo.form_header_edi.invh_run_auto = dbo.form_detail_edi.invh_run_auto " &
                    '             "WHERE (dbo.form_header_edi.invh_run_auto = '" & lblInvh_Run_Auto.Text.Trim() & "')"


                    ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.Text, strCommand)
                    If ds.Tables(0).Rows.Count > 0 Then
                        If ds.Tables(0).Rows(0).Item("form_type") = "FORM2_4" Then
                            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                                With ds.Tables(0).Rows(i)
                                    Dim dsStock As New DataSet
                                    dsStock = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, "s_Insert_TransStock_NewDS", _
                                    New SqlParameter("@TSK_YEAR", CommonUtility.Get_StringValue(Now.Year)), _
                                    New SqlParameter("@invh_run_auto", CommonUtility.Get_StringValue(.Item("invh_run_auto"))), _
                                    New SqlParameter("@invd_run_auto", CommonUtility.Get_StringValue(.Item("invd_run_auto"))), _
                                    New SqlParameter("@TSK_Date", CommonUtility.Get_DateTime(Now)), _
                                    New SqlParameter("@TSK_Type1", CommonUtility.Get_StringValue("D")), _
                                    New SqlParameter("@TTS_Code", CommonUtility.Get_StringValue("003")), _
                                    New SqlParameter("@TSK_Desc", CommonUtility.Get_StringValue("คำขอไม่ได้รับการอนุมัติ (ต้องแก้ไข)")), _
                                    New SqlParameter("@unit_code", CommonUtility.Get_StringValue("TON")), _
                                    New SqlParameter("@TSK_Debit", CommonUtility.Get_Decimal(0)), _
                                    New SqlParameter("@TSK_Credit", CommonUtility.Get_Decimal(.Item("product_descriptionMaxico"))), _
                                    New SqlParameter("@TSK_Amount", CommonUtility.Get_Decimal(0)), _
                                    New SqlParameter("@company_taxno", CommonUtility.Get_StringValue(.Item("company_taxno"))), _
                                    New SqlParameter("@CompanyName_En", CommonUtility.Get_StringValue(.Item("company_name"))), _
                                    New SqlParameter("@reference_code2", CommonUtility.Get_StringValue(.Item("reference_code2"))), _
                                    New SqlParameter("@Quantity", CommonUtility.Get_Decimal(0)), _
                                    New SqlParameter("@ReferenceDesc1", CommonUtility.Get_StringValue("&nbsp;")), _
                                    New SqlParameter("@ReferenceDesc2", CommonUtility.Get_StringValue("ยกเลิก")), _
                                    New SqlParameter("@user_id", CommonUtility.Get_StringValue(UserInfo.Username)))
                                End With
                            Next
                        End If
                    End If

                    'QUOTA END

                    Me.RadAjaxManager1.ResponseScripts.Add("window.alert('ทำการยกเลิกหนังสือรับรองเลขที่ " & lblReferenceCode2.Text & " เรียบร้อยแล้ว');")
                    tblHeader.Visible = False
                    btnSearch.Visible = True
                    txtReferenceCode2.Text = ""
                    txtReferenceCode2.Focus()
                Else
                    Me.RadAjaxManager1.ResponseScripts.Add("window.alert('เกิดความผิดพลาดไม่สามารถยกเลิกหนังสือรับรองได้');")
                End If
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End Sub

        Protected Sub check_YearsOle_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles check_YearsOle.CheckedChanged
            DDLYears.Enabled = check_YearsOle.Checked
        End Sub
    End Class

End Namespace
