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

Namespace NTi.Modules.DFT_EDI_F04_EditAttachDoc
    Partial Class ViewDFT_EDI_F04_EditAttachDoc
        Inherits Entities.Modules.PortalModuleBase
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

                strCommand = "select a.*,b.DESCRIPTION,c.FORM_NAME,d.* from FORM_HEADER_EDI a left outer join EDI_STATUS b on a.EDI_STATUS_ID=b.EDI_STATUS_ID left outer join FORM_TYPE c on a.FORM_TYPE=c.FORM_TYPE left outer join COMPANY d on a.COMPANY_TAXNO=d.COMPANY_TAXNO where a.EDI_STATUS_ID='A' and PRINT_FLAG='Y' and RECEIPT_FLAG='Y' and a.REFERENCE_CODE2='" & ReferenceCode2 & "' and a.SITE_ID LIKE '%" & Site_ID & "%'"
                
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

                    Select Case CommonUtility.Get_StringValue(dr.Item("edi_status_id"))
                        Case "P"
                            lblStatus.Visible = False
                            btnEditDocAttach.Visible = False
                        Case "N"
                            lblStatus.Text = "ไม่ผ่านการอนุมัติ"
                            btnEditDocAttach.Visible = False
                        Case "A"
                            lblStatus.Text = "ผ่านการอนุมัติ"
                            btnEditDocAttach.Visible = True
                    End Select
                    dr.Close()

                Else
                    tblHeader.Visible = False
                    Me.RadAjaxManager1.ResponseScripts.Add("window.alert('" & "ไม่พบหมายเลขหนังสือรับรองเลขที่  " & txtReferenceCode2.Text.Trim() & "');")
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

        Protected Sub btnEditDocAttach_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEditDocAttach.Click
            Try
                Dim ds As New DataSet
                ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, "Ma_EditDocAtt_NewDS", _
                New SqlParameter("@TCat", 1), _
                New SqlParameter("@invh_run_auto", ""), _
                New SqlParameter("@reference_code2", CommonUtility.Get_StringValue(txtReferenceCode2.Text.Trim())), _
                New SqlParameter("@remark_docatt", CommonUtility.Get_StringValue(txtEditDetail.Text)), _
                New SqlParameter("@user_id", CommonUtility.Get_StringValue(UserInfo.Username)), _
                New SqlParameter("@site_id", CommonUtility.Get_StringValue(lblRoleID.Text)))

                If ds.Tables(0).Rows.Count > 0 Then
                    If CommonUtility.Get_Int32(ds.Tables(0).Rows(0).Item("retSTATUS")) = 0 Then
                        'QUOTA END
                        Me.RadAjaxManager1.ResponseScripts.Add("window.alert('ทำการแจ้งแก้ไขหนังสือรับรองเลขที่ " & lblReferenceCode2.Text & " เรียบร้อยแล้ว');")
                        tblHeader.Visible = False
                        txtReferenceCode2.Text = ""
                        txtReferenceCode2.Focus()
                    Else
                        Me.RadAjaxManager1.ResponseScripts.Add("window.alert('" & CommonUtility.Get_StringValue(ds.Tables(0).Rows(0).Item("retMessage")) & "');")
                    End If
                End If
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End Sub
    End Class

End Namespace
