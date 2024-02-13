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

Namespace NTi.Modules.DFT_EDI_NotApproved
    Partial Class ViewDFT_EDI_NotApproved
        Inherits Entities.Modules.PortalModuleBase
        'บันทึกสถานะว่าไม่ผ่านการอนุมัติ
        Dim strEDIConn As String = ConfigurationManager.ConnectionStrings("OriginConnection").ConnectionString

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
            Call LoadHeader(txtReferenceCode2.Text.Trim(), lblRoleID.Text)

        End Sub

        Private Sub LoadHeader(ByVal ReferenceCode2 As String, ByVal Site_ID As String)
            Try
                Dim dr As SqlDataReader
                Dim strCommand As String
                If chkUseRef2.Checked = True Then
                    strCommand = "select a.*,b.DESCRIPTION,c.FORM_NAME,d.* from FORM_HEADER_EDI a left outer join EDI_STATUS b on a.EDI_STATUS_ID=b.EDI_STATUS_ID left outer join FORM_TYPE c on a.FORM_TYPE=c.FORM_TYPE left outer join COMPANY d on a.COMPANY_TAXNO=d.COMPANY_TAXNO where  (a.EDI_STATUS_ID='D' or a.EDI_STATUS_ID='P' or a.EDI_STATUS_ID='A' or a.EDI_STATUS_ID='N') and PRINT_FLAG='Y' and RECEIPT_FLAG='Y' and a.REFERENCE_CODE2='" & ReferenceCode2 & "' and a.SITE_ID='" & Site_ID & "'"
                Else
                    strCommand = "select a.*,b.DESCRIPTION,c.FORM_NAME,d.* from FORM_HEADER_EDI a left outer join EDI_STATUS b on a.EDI_STATUS_ID=b.EDI_STATUS_ID left outer join FORM_TYPE c on a.FORM_TYPE=c.FORM_TYPE left outer join COMPANY d on a.COMPANY_TAXNO=d.COMPANY_TAXNO where  (a.EDI_STATUS_ID='D' or a.EDI_STATUS_ID='P' or a.EDI_STATUS_ID='A' or a.EDI_STATUS_ID='N') and PRINT_FLAG='Y' and RECEIPT_FLAG='Y' and a.INVH_RUN_AUTO='" & ReferenceCode2 & "' and a.SITE_ID='" & Site_ID & "'"
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

                    txtE00.Text = ""
                    txtE01.Text = ""
                    txtE02.Text = ""
                    txtE03.Text = ""
                    txtE04.Text = ""
                    txtE05.Text = ""
                    txtE06.Text = ""
                    txtE07.Text = ""
                    txtE08.Text = ""
                    txtE09.Text = ""
                    txtE10.Text = ""
                    txtE11.Text = ""
                    txtE12.Text = ""
                    txtE99.Text = ""

                    Call Get_RESULT_CHECKING_MESSAGE(lblInvh_Run_Auto.Text)

                    Select Case CommonUtility.Get_StringValue(dr.Item("edi_status_id"))
                        Case "P", "D"
                            lblStatus.Visible = False
                            btnNotApproved.Visible = True
                        Case "N"
                            lblStatus.Text = "ไม่ผ่านการอนุมัติ"
                            lblStatus.Visible = True
                            btnNotApproved.Visible = True

                            'Call Get_RESULT_CHECKING_MESSAGE(lblInvh_Run_Auto.Text)
                        Case "A"
                            lblStatus.Text = "ผ่านการอนุมัติ"
                            lblStatus.Visible = True
                            btnNotApproved.Visible = True
                    End Select
                    dr.Close()

                    If txtE00.Text = "" Then
                        txtE00.Enabled = True
                    Else
                        txtE00.Enabled = False
                    End If

                    If txtE01.Text = "" Then
                        txtE01.Enabled = True
                    Else
                        txtE01.Enabled = False
                    End If

                    If txtE02.Text = "" Then
                        txtE02.Enabled = True
                    Else
                        txtE02.Enabled = False
                    End If

                    If txtE03.Text = "" Then
                        txtE03.Enabled = True
                    Else
                        txtE03.Enabled = False
                    End If

                    If txtE04.Text = "" Then
                        txtE04.Enabled = True
                    Else
                        txtE04.Enabled = False
                    End If

                    If txtE05.Text = "" Then
                        txtE05.Enabled = True
                    Else
                        txtE05.Enabled = False
                    End If

                    If txtE06.Text = "" Then
                        txtE06.Enabled = True
                    Else
                        txtE06.Enabled = False
                    End If

                    If txtE07.Text = "" Then
                        txtE07.Enabled = True
                    Else
                        txtE07.Enabled = False
                    End If

                    If txtE08.Text = "" Then
                        txtE08.Enabled = True
                    Else
                        txtE08.Enabled = False
                    End If

                    If txtE09.Text = "" Then
                        txtE09.Enabled = True
                    Else
                        txtE09.Enabled = False
                    End If

                    If txtE10.Text = "" Then
                        txtE10.Enabled = True
                    Else
                        txtE10.Enabled = False
                    End If

                    If txtE11.Text = "" Then
                        txtE11.Enabled = True
                    Else
                        txtE11.Enabled = False
                    End If

                    If txtE12.Text = "" Then
                        txtE12.Enabled = True
                    Else
                        txtE12.Enabled = False
                    End If

                    If txtE99.Text = "" Then
                        txtE99.Enabled = True
                    Else
                        txtE99.Enabled = False
                    End If



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

        Private Sub Get_RESULT_CHECKING_MESSAGE(ByVal Invh_Run_Auto As String)
            Try
                Dim strCommand As String
                strCommand = "SELECT * FROM result_checking_message WHERE (invh_run_auto = '" & Invh_Run_Auto & "')"
                Dim ds As New DataSet
                ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.Text, strCommand)
                If ds.Tables(0).Rows.Count > 0 Then
                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                        With ds.Tables(0).Rows(i)
                            Select Case CommonUtility.Get_StringValue(.Item("error_code"))
                                Case "E01"
                                    txtE01.Text = CommonUtility.Get_StringValue(.Item("error_information"))
                                Case "E02"
                                    txtE02.Text = CommonUtility.Get_StringValue(.Item("error_information"))
                                Case "E03"
                                    txtE03.Text = CommonUtility.Get_StringValue(.Item("error_information"))
                                Case "E04"
                                    txtE04.Text = CommonUtility.Get_StringValue(.Item("error_information"))
                                Case "E05"
                                    txtE05.Text = CommonUtility.Get_StringValue(.Item("error_information"))
                                Case "E06"
                                    txtE06.Text = CommonUtility.Get_StringValue(.Item("error_information"))
                                Case "E07"
                                    txtE07.Text = CommonUtility.Get_StringValue(.Item("error_information"))
                                Case "E08"
                                    txtE08.Text = CommonUtility.Get_StringValue(.Item("error_information"))
                                Case "E09"
                                    txtE09.Text = CommonUtility.Get_StringValue(.Item("error_information"))
                                Case "E10"
                                    txtE10.Text = CommonUtility.Get_StringValue(.Item("error_information"))
                                Case "E11"
                                    txtE11.Text = CommonUtility.Get_StringValue(.Item("error_information"))
                                Case "E12"
                                    txtE12.Text = CommonUtility.Get_StringValue(.Item("error_information"))
                                Case "E99"
                                    txtE99.Text = CommonUtility.Get_StringValue(.Item("error_information"))
                            End Select
                        End With
                    Next
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

        Protected Sub btnNotApproved_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNotApproved.Click
            Try
                Dim ds As New DataSet
                ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, "sp_form4_edi_notApproveForm", _
                New SqlParameter("@INVH_RUN_AUTO", CommonUtility.Get_StringValue(lblInvh_Run_Auto.Text)), _
                New SqlParameter("@USER_ID", CommonUtility.Get_StringValue(UserInfo.Username)), _
                New SqlParameter("@E01", CommonUtility.Get_String(txtE01.Text.Trim())), _
                New SqlParameter("@E02", CommonUtility.Get_String(txtE02.Text.Trim())), _
                New SqlParameter("@E03", CommonUtility.Get_String(txtE03.Text.Trim())), _
                New SqlParameter("@E04", CommonUtility.Get_String(txtE04.Text.Trim())), _
                New SqlParameter("@E05", CommonUtility.Get_String(txtE05.Text.Trim())), _
                New SqlParameter("@E06", CommonUtility.Get_String(txtE06.Text.Trim())), _
                New SqlParameter("@E07", CommonUtility.Get_String(txtE07.Text.Trim())), _
                New SqlParameter("@E08", CommonUtility.Get_String(txtE08.Text.Trim())), _
                New SqlParameter("@E09", CommonUtility.Get_String(txtE09.Text.Trim())), _
                New SqlParameter("@E10", CommonUtility.Get_String(txtE10.Text.Trim())), _
                New SqlParameter("@E11", CommonUtility.Get_String(txtE11.Text.Trim())), _
                New SqlParameter("@E12", CommonUtility.Get_String(txtE12.Text.Trim())), _
                New SqlParameter("@E99", CommonUtility.Get_String(txtE99.Text.Trim())), _
                New SqlParameter("@Cancel_Date", CommonUtility.Get_DateTime(Now)))

                If CommonUtility.Get_StringValue(ds.Tables(0).Rows(0).Item("retSTATUS")) = "0" Then
                    'QUOTA START
                    Dim Edit_Text As String = Generate_EditText()
                    Dim strCommand As String = ""
                    If chkUseRef2.Checked = True Then
                        strCommand = "SELECT dbo.form_header_edi.*, dbo.form_detail_edi.* " & _
                                     "FROM dbo.form_header_edi INNER JOIN " & _
                                     "dbo.form_detail_edi ON dbo.form_header_edi.invh_run_auto = dbo.form_detail_edi.invh_run_auto " & _
                                     "WHERE (dbo.form_header_edi.reference_code2 = '" & txtReferenceCode2.Text.Trim() & "')"
                    Else
                        strCommand = "SELECT dbo.form_header_edi.*, dbo.form_detail_edi.* " & _
                                     "FROM dbo.form_header_edi INNER JOIN " & _
                                     "dbo.form_detail_edi ON dbo.form_header_edi.invh_run_auto = dbo.form_detail_edi.invh_run_auto " & _
                                     "WHERE (dbo.form_header_edi.invh_run_auto = '" & txtReferenceCode2.Text.Trim() & "')"
                    End If

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
                                    New SqlParameter("@ReferenceDesc1", CommonUtility.Get_StringValue(Edit_Text)), _
                                    New SqlParameter("@ReferenceDesc2", CommonUtility.Get_StringValue("&nbsp;")), _
                                    New SqlParameter("@user_id", CommonUtility.Get_StringValue(UserInfo.Username)))
                                End With
                            Next
                        End If
                    End If

                    'QUOTA END
                    Me.RadAjaxManager1.ResponseScripts.Add("window.alert('ทำการบันทึกหนังสือรับรองเลขที่ " & lblReferenceCode2.Text & " เรียบร้อยแล้ว  (ไม่ผ่านการอนุมัติ)');")
                    tblHeader.Visible = False
                    txtReferenceCode2.Text = ""
                    txtReferenceCode2.Focus()
                Else
                    Me.RadAjaxManager1.ResponseScripts.Add("window.alert('" & CommonUtility.Get_StringValue(ds.Tables(0).Rows(0).Item("retSTATUS")) & "');")
                End If
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End Sub

        Function Generate_EditText()
            Dim Edit_Text As String = ""
            If txtE01.Text.Trim() <> "" Then
                If Edit_Text = "" Then
                    Edit_Text = txtE01.Text
                Else
                    Edit_Text &= ", " & txtE01.Text
                End If
            End If

            If txtE02.Text.Trim() <> "" Then
                If Edit_Text = "" Then
                    Edit_Text = txtE02.Text
                Else
                    Edit_Text &= ", " & txtE02.Text
                End If
            End If

            If txtE03.Text.Trim() <> "" Then
                If Edit_Text = "" Then
                    Edit_Text = txtE03.Text
                Else
                    Edit_Text &= ", " & txtE03.Text
                End If
            End If

            If txtE04.Text.Trim() <> "" Then
                If Edit_Text = "" Then
                    Edit_Text = txtE04.Text
                Else
                    Edit_Text &= ", " & txtE04.Text
                End If
            End If

            If txtE05.Text.Trim() <> "" Then
                If Edit_Text = "" Then
                    Edit_Text = txtE05.Text
                Else
                    Edit_Text &= ", " & txtE05.Text
                End If
            End If

            If txtE06.Text.Trim() <> "" Then
                If Edit_Text = "" Then
                    Edit_Text = txtE06.Text
                Else
                    Edit_Text &= ", " & txtE06.Text
                End If
            End If

            If txtE07.Text.Trim() <> "" Then
                If Edit_Text = "" Then
                    Edit_Text = txtE07.Text
                Else
                    Edit_Text &= ", " & txtE07.Text
                End If
            End If

            If txtE08.Text.Trim() <> "" Then
                If Edit_Text = "" Then
                    Edit_Text = txtE08.Text
                Else
                    Edit_Text &= ", " & txtE08.Text
                End If
            End If

            If txtE09.Text.Trim() <> "" Then
                If Edit_Text = "" Then
                    Edit_Text = txtE09.Text
                Else
                    Edit_Text &= ", " & txtE09.Text
                End If
            End If

            If txtE10.Text.Trim() <> "" Then
                If Edit_Text = "" Then
                    Edit_Text = txtE10.Text
                Else
                    Edit_Text &= ", " & txtE10.Text
                End If
            End If

            If txtE11.Text.Trim() <> "" Then
                If Edit_Text = "" Then
                    Edit_Text = txtE11.Text
                Else
                    Edit_Text &= ", " & txtE11.Text
                End If
            End If

            If txtE12.Text.Trim() <> "" Then
                If Edit_Text = "" Then
                    Edit_Text = txtE12.Text
                Else
                    Edit_Text &= ", " & txtE12.Text
                End If
            End If

            If txtE99.Text.Trim() <> "" Then
                If Edit_Text = "" Then
                    Edit_Text = txtE99.Text
                Else
                    Edit_Text &= ", " & txtE99.Text
                End If
            End If

            Return Edit_Text
        End Function
    End Class

End Namespace
