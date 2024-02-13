Imports System.Web.UI
Imports System.Collections.Generic
Imports System.Reflection

Imports DotNetNuke
Imports DotNetNuke.Services.Exceptions
Imports DotNetNuke.Services.Localization

Imports DFT.Dotnetnuke.ClassLibrary
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports Telerik.Web.UI

Namespace NTi.Modules.DFT_EDI_AttachDoc
    Partial Class ViewDFT_EDI_AttachDoc
        Inherits Entities.Modules.PortalModuleBase
        Dim strEDIConn As String = ConfigurationManager.ConnectionStrings("OriginConnection").ConnectionString
        Dim myReader As SqlDataReader = Nothing
        Dim objConn As SqlConnection = Nothing

        Private Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
            Call LoadSitePlus()
        End Sub

        Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            txtINVH_RUN_AUTO.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" + btnSearch.UniqueID + "').click();return false;}} else {return true}; ")
            txtINVH_RUN_AUTO2.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" + btnEditDoc.UniqueID + "').click();return false;}} else {return true}; ")
            txtSearchCancel.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" + btnSearchTab3.UniqueID + "').click();return false;}} else {return true}; ")

            If Not Page.IsPostBack Then
                lblState.Text = "load"
                'Tab 1 การแนบเอกสาร
                rdpFromDate.SelectedDate = Today
                rdpToDate.SelectedDate = Today

                rdpFromDateTab2.SelectedDate = Today
                rdpToDateTab2.SelectedDate = Today

                'rdpStartApproved.SelectedDate = Today
                'rdpEndApproved.SelectedDate = Today

                rgCancelCertificate.Visible = False

                txtINVH_RUN_AUTO.Focus()
            Else
                lblState.Text = "unload"
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
        Private Sub rgAttachDocList_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles rgAttachDocList.DataBound
            myReader.Close()
            objConn.Close()
        End Sub

        Private Sub rgAttachDocList_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgAttachDocList.NeedDataSource
            rgAttachDocList.DataSource = LoadAttachDocOnLoad()
        End Sub

        Function LoadAttachDocOnLoad() As SqlDataReader
            Try
                objConn = New SqlConnection(strEDIConn)
                myReader = SqlHelper.ExecuteReader(objConn, CommandType.StoredProcedure, "vi_select_RepAtt1", _
                New SqlParameter("@TCat1", 9), _
                New SqlParameter("@TCat2", 9), _
                New SqlParameter("@dForm", FunctionUtility.DMY2YMD(rdpFromDate.SelectedDate.Value)), _
                New SqlParameter("@dTo", FunctionUtility.DMY2YMD(rdpToDate.SelectedDate.Value)), _
                New SqlParameter("@total_day", CommonUtility.Get_Int32(txtTotalDay.Text)), _
                New SqlParameter("@company_taxno", CommonUtility.Get_String(txtCompany_Taxno.Text)), _
                New SqlParameter("@site_id", lblRoleID.Text), _
                New SqlParameter("@invh_run_auto", CommonUtility.Get_String(txtINVH_RUN_AUTO.Text.Trim())))

                Return myReader
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End Function

        Function LoadAttachDoc() As SqlDataReader
            Try
                Dim TCat2 As Integer = 0
                If rbType1.Checked = True Then
                    TCat2 = 1
                ElseIf rbType2.Checked = True Then
                    TCat2 = 2
                ElseIf rbType3.Checked = True Then
                    TCat2 = 3
                End If

                objConn = New SqlConnection(strEDIConn)
                myReader = SqlHelper.ExecuteReader(objConn, CommandType.StoredProcedure, "vi_select_RepAtt1", _
                New SqlParameter("@TCat1", 1), _
                New SqlParameter("@TCat2", TCat2), _
                New SqlParameter("@dForm", FunctionUtility.DMY2YMD(rdpFromDate.SelectedDate.Value)), _
                New SqlParameter("@dTo", FunctionUtility.DMY2YMD(rdpToDate.SelectedDate.Value)), _
                New SqlParameter("@total_day", CommonUtility.Get_Int32(txtTotalDay.Text)), _
                New SqlParameter("@company_taxno", CommonUtility.Get_String(txtCompany_Taxno.Text)), _
                New SqlParameter("@site_id", CommonUtility.Get_String(rcbSitePlus.SelectedValue)), _
                New SqlParameter("@invh_run_auto", CommonUtility.Get_String(txtINVH_RUN_AUTO.Text.Trim())))

                Return myReader
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End Function

        Protected Sub ibSearch_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ibSearch.Click
            rgAttachDocList.DataSource = LoadAttachDoc()
            rgAttachDocList.DataBind()
        End Sub

        Private Sub LoadSitePlus()
            Try
                Dim ds As New DataSet
                ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, "vi_select_SitePlus", _
                New SqlParameter("@TCat", 4), _
                New SqlParameter("@Para1", ""))

                If ds.Tables(0).Rows.Count > 0 Then
                    rcbSitePlus.DataSource = ds.Tables(0)
                    rcbSitePlus.DataTextField = "site_name"
                    rcbSitePlus.DataValueField = "site_id"
                    rcbSitePlus.DataBind()
                End If
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End Sub

        Protected Sub chkTaxno_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTaxno.CheckedChanged
            If chkTaxno.Checked = True Then
                txtCompany_Taxno.Enabled = True
                txtCompany_Taxno.BackColor = Drawing.Color.White
                txtCompany_Taxno.Focus()
            Else
                txtCompany_Taxno.Enabled = False
                txtCompany_Taxno.BackColor = Drawing.Color.LightGray
            End If
        End Sub

        Protected Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
            Try
                Dim ds As New DataSet
                ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, "vi_approve_repAtt_NewDS", _
                New SqlParameter("@TCat", CommonUtility.Get_Int32(rblTypeRepAtt.SelectedValue)), _
                New SqlParameter("@INVH_RUN_AUTO", CommonUtility.Get_StringValue(txtINVH_RUN_AUTO.Text)), _
                New SqlParameter("@USER_ID", CommonUtility.Get_StringValue(UserInfo.Username)), _
                New SqlParameter("@site_id", lblRoleID.Text))

                If ds.Tables(0).Rows.Count > 0 Then
                    Select Case CommonUtility.Get_Int32(ds.Tables(0).Rows(0).Item("retSTATUS"))
                        Case 0
                            rgAttachDocList.DataSource = LoadRepAtt()
                            rgAttachDocList.DataBind()
                        Case -1, -2, -3, -4, -5
                            Me.RadAjaxManager1.ResponseScripts.Add("window.alert('" & CommonUtility.Get_StringValue(ds.Tables(0).Rows(0).Item("retMessage")) & "');")
                    End Select
                End If

                txtINVH_RUN_AUTO.Text = ""
                txtINVH_RUN_AUTO.Focus()
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End Sub

        Function LoadRepAtt() As SqlDataReader
            Try
                objConn = New SqlConnection(strEDIConn)
                myReader = SqlHelper.ExecuteReader(objConn, CommandType.StoredProcedure, "vi_select_RepAtt1", _
                New SqlParameter("@TCat1", 3), _
                New SqlParameter("@TCat2", 1), _
                New SqlParameter("@dForm", FunctionUtility.DMY2YMD(rdpFromDate.SelectedDate.Value)), _
                New SqlParameter("@dTo", FunctionUtility.DMY2YMD(rdpToDate.SelectedDate.Value)), _
                New SqlParameter("@total_day", 0), _
                New SqlParameter("@company_taxno", CommonUtility.Get_String(txtCompany_Taxno.Text)), _
                New SqlParameter("@site_id", lblRoleID.Text), _
                New SqlParameter("@invh_run_auto", CommonUtility.Get_String(txtINVH_RUN_AUTO.Text.Trim())))

                Return myReader
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End Function

        Function LoadEditDocAtt() As SqlDataReader
            Try
                objConn = New SqlConnection(strEDIConn)
                myReader = SqlHelper.ExecuteReader(objConn, CommandType.StoredProcedure, "Re_EditDocAtt_NewDS", _
                New SqlParameter("@TCat", CommonUtility.Get_Int32(rblTypeSearch.SelectedValue)), _
                New SqlParameter("@site_id", lblRoleID.Text), _
                New SqlParameter("@invh_run_auto", CommonUtility.Get_StringValue(txtINVH_RUN_AUTO2.Text.Trim())), _
                New SqlParameter("@dForm", FunctionUtility.DMY2YMD(rdpFromDateTab2.SelectedDate.Value)), _
                New SqlParameter("@dTo", FunctionUtility.DMY2YMD(rdpToDateTab2.SelectedDate.Value)))

                Return myReader
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End Function

        Protected Sub btnSearchAtt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchAtt.Click
            rgEditAttDoc.DataSource = LoadEditDocAtt()
            rgEditAttDoc.DataBind()

            If rgEditAttDoc.MasterTableView.Items.Count = 0 Then
                Me.RadAjaxManager1.ResponseScripts.Add("window.alert('ไม่พบรายการตามเงื่อนไขที่ทำการค้นหา !!!');")
            End If

            Select Case rblTypeSearch.SelectedValue
                Case "1"
                    rgEditAttDoc.MasterTableView.Columns(8).Visible = False
                    rgEditAttDoc.MasterTableView.Columns(9).Visible = False
                    rgEditAttDoc.MasterTableView.Columns(10).Visible = False
                    rgEditAttDoc.MasterTableView.Columns(11).Visible = False
                Case "2", "3"
                    rgEditAttDoc.MasterTableView.Columns(8).Visible = True
                    rgEditAttDoc.MasterTableView.Columns(9).Visible = True
                    rgEditAttDoc.MasterTableView.Columns(10).Visible = False
                    rgEditAttDoc.MasterTableView.Columns(11).Visible = False
                Case "4"
                    rgEditAttDoc.MasterTableView.Columns(8).Visible = True
                    rgEditAttDoc.MasterTableView.Columns(9).Visible = True
                    rgEditAttDoc.MasterTableView.Columns(10).Visible = True
                    rgEditAttDoc.MasterTableView.Columns(11).Visible = True
            End Select
        End Sub

        Private Sub rgEditAttDoc_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles rgEditAttDoc.DataBound
            myReader.Close()
            objConn.Close()

            Select Case rblTypeSearch.SelectedValue
                Case "1"
                    rgEditAttDoc.MasterTableView.Columns(8).Visible = False
                    rgEditAttDoc.MasterTableView.Columns(9).Visible = False
                    rgEditAttDoc.MasterTableView.Columns(10).Visible = False
                    rgEditAttDoc.MasterTableView.Columns(11).Visible = False
                Case "2", "3"
                    rgEditAttDoc.MasterTableView.Columns(10).Visible = False
                    rgEditAttDoc.MasterTableView.Columns(11).Visible = False
            End Select
        End Sub

        Private Sub rgEditAttDoc_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgEditAttDoc.NeedDataSource
            rgEditAttDoc.DataSource = LoadEditDocAtt()
        End Sub

        Protected Sub rblEditType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rblEditType.SelectedIndexChanged
            btnEditDoc.Text = "ดำเนินการ " & rblEditType.SelectedItem.Text
        End Sub

        Protected Sub btnEditDoc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEditDoc.Click
            Try
                If txtINVH_RUN_AUTO2.Text.Trim() = "" Then
                    Me.RadAjaxManager1.ResponseScripts.Add("window.alert('กรุณาป้อนหมายเลขคำร้องที่ต้องการแจ้งแก้ไขเอกสาร !!!');")
                    txtINVH_RUN_AUTO2.Text = ""
                    txtINVH_RUN_AUTO2.Focus()
                    Exit Sub
                End If

                If rblEditType.SelectedValue = "1" Then
                    If txtRemark_docatt.Text.Trim() = "" Then
                        Me.RadAjaxManager1.ResponseScripts.Add("window.alert('กรุณาระบุเหตุผลการแจ้งแก้ไขเอกสาร !!!');")
                        txtRemark_docatt.Text = ""
                        txtRemark_docatt.Focus()
                        Exit Sub
                    End If
                End If

                Me.RadAjaxManager1.ResponseScripts.Add("return ShowConfirmDialog('" & rblEditType.SelectedValue & "','" & txtINVH_RUN_AUTO2.Text.Trim & "','" & txtRemark_docatt.Text & "','" & UserInfo.Username & "','" & lblRoleID.Text & "');")

                txtINVH_RUN_AUTO2.Text = ""
                txtRemark_docatt.Text = ""
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End Sub

        Function LoadEditDocAttForClick() As SqlDataReader
            Try
                objConn = New SqlConnection(strEDIConn)
                myReader = SqlHelper.ExecuteReader(objConn, CommandType.StoredProcedure, "Re_EditDocAtt", _
                New SqlParameter("@TCat", 4), _
                New SqlParameter("@site_id", lblRoleID.Text), _
                New SqlParameter("@invh_run_auto", CommonUtility.Get_StringValue(txtINVH_RUN_AUTO2.Text.Trim())), _
                New SqlParameter("@dForm", FunctionUtility.DMY2YMD(rdpFromDateTab2.SelectedDate.Value)), _
                New SqlParameter("@dTo", FunctionUtility.DMY2YMD(rdpToDateTab2.SelectedDate.Value)))

                Return myReader
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End Function

        Function GetInvoice_No(ByVal _invh_run_auto As String) As String
            Try
                Dim isFirst As Boolean = True
                Dim strInvoice As String = ""
                Dim strCommand As String
                strCommand = "Select * From form_header_edi Where invh_run_auto = '" & _invh_run_auto & "'"
                Dim dr As SqlDataReader
                dr = SqlHelper.ExecuteReader(strEDIConn, CommandType.Text, strCommand)
                If dr.HasRows() Then
                    dr.Read()

                    If Not (dr.Item("invoice_no1").Equals(System.DBNull.Value) Or CommonUtility.Get_StringValue(dr.Item("invoice_no1")) = "") Then
                        If isFirst Then
                            strInvoice = CommonUtility.Get_StringValue(dr.Item("invoice_no1"))
                            isFirst = False
                        Else
                            strInvoice &= "," & "<br />" & CommonUtility.Get_StringValue(dr.Item("invoice_no1"))
                        End If
                    End If

                    If Not (dr.Item("invoice_no2").Equals(System.DBNull.Value) Or CommonUtility.Get_StringValue(dr.Item("invoice_no2")) = "") Then
                        If isFirst Then
                            strInvoice = CommonUtility.Get_StringValue(dr.Item("invoice_no2"))
                            isFirst = False
                        Else
                            strInvoice &= "," & "<br />" & CommonUtility.Get_StringValue(dr.Item("invoice_no2"))
                        End If
                    End If

                    If Not (dr.Item("invoice_no3").Equals(System.DBNull.Value) Or CommonUtility.Get_StringValue(dr.Item("invoice_no3")) = "") Then
                        If isFirst Then
                            strInvoice = CommonUtility.Get_StringValue(dr.Item("invoice_no3"))
                            isFirst = False
                        Else
                            strInvoice &= "," & "<br />" & CommonUtility.Get_StringValue(dr.Item("invoice_no3"))
                        End If
                    End If

                    If Not (dr.Item("invoice_no4").Equals(System.DBNull.Value) Or CommonUtility.Get_StringValue(dr.Item("invoice_no4")) = "") Then
                        If isFirst Then
                            strInvoice = CommonUtility.Get_StringValue(dr.Item("invoice_no4"))
                            isFirst = False
                        Else
                            strInvoice &= "," & "<br />" & CommonUtility.Get_StringValue(dr.Item("invoice_no4"))
                        End If
                    End If

                    If Not (dr.Item("invoice_no5").Equals(System.DBNull.Value) Or CommonUtility.Get_StringValue(dr.Item("invoice_no5")) = "") Then
                        If isFirst Then
                            strInvoice = CommonUtility.Get_StringValue(dr.Item("invoice_no5"))
                            isFirst = False
                        Else
                            strInvoice &= "," & "<br />" & CommonUtility.Get_StringValue(dr.Item("invoice_no5"))
                        End If
                    End If

                    Return strInvoice
                Else
                    Return ""
                End If
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End Function

        Private Sub RadAjaxManager1_AjaxRequest(ByVal sender As Object, ByVal e As Telerik.Web.UI.AjaxRequestEventArgs) Handles RadAjaxManager1.AjaxRequest
            rgEditAttDoc.DataSource = LoadEditDocAtt()
            rgEditAttDoc.DataBind()

            Select Case rblEditType.SelectedValue
                Case "1"
                    rgEditAttDoc.MasterTableView.Columns(8).Visible = False
                    rgEditAttDoc.MasterTableView.Columns(9).Visible = False
                    rgEditAttDoc.MasterTableView.Columns(10).Visible = False
                    rgEditAttDoc.MasterTableView.Columns(11).Visible = False
                Case "2"
                    rgEditAttDoc.MasterTableView.Columns(10).Visible = False
                    rgEditAttDoc.MasterTableView.Columns(11).Visible = False
            End Select
        End Sub

        Protected Sub btnSearchTab3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchTab3.Click
            rgCancelCertificate.DataSource = SerachCancelCert()
            rgCancelCertificate.DataBind()

            If rgCancelCertificate.MasterTableView.Items.Count > 0 Then
                rgCancelCertificate.Visible = True
            Else
                Me.RadAjaxManager1.ResponseScripts.Add("window.alert('ไม่พบข้อมูลตามเงื่อนไขที่ทำการค้นหา !!!');")
            End If
        End Sub

        Function SerachCancelCert() As DataTable
            Try
                Dim strCommand As String
                Dim ds As New DataSet
                strCommand = "SELECT dbo.form_header_edi.invh_run_auto, dbo.form_header_edi.company_taxno, dbo.form_header_edi.company_name, dbo.form_header_edi.reference_code2, " & _
                             "dbo.form_header_edi.approve_date, dbo.form_type.form_name, dbo.form_header_edi.site_id, dbo.form_header_edi.edi_status_id, dbo.form_header_edi.invoice_no1,  " & _
                             "dbo.form_header_edi.invoice_no2, dbo.form_header_edi.invoice_no3, dbo.form_header_edi.invoice_no4, dbo.form_header_edi.invoice_no5 " & _
                             "FROM dbo.form_header_edi INNER JOIN " & _
                             "dbo.form_type ON dbo.form_header_edi.form_type = dbo.form_type.form_type " & _
                             "WHERE (dbo.form_header_edi.edi_status_id = 'N') AND (dbo.form_header_edi.reference_code2 LIKE '%_C%') AND (dbo.form_header_edi.invh_run_auto = '" & txtSearchCancel.Text.Trim() & "')"

                ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.Text, strCommand)
                Return ds.Tables(0)
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End Function

        Private Sub rgCancelCertificate_PageIndexChanged(ByVal source As Object, ByVal e As Telerik.Web.UI.GridPageChangedEventArgs) Handles rgCancelCertificate.PageIndexChanged
            rgCancelCertificate.CurrentPageIndex = e.NewPageIndex
            rgCancelCertificate.DataSource = SerachCancelCert()
            rgCancelCertificate.DataBind()
        End Sub
    End Class

End Namespace
