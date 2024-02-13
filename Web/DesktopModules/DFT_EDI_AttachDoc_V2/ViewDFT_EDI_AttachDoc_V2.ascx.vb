Imports System.Web.UI
Imports System.Collections.Generic
Imports System.Reflection

Imports DotNetNuke
Imports DotNetNuke.Services.Exceptions
Imports DotNetNuke.Services.Localization

Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports DFT.Dotnetnuke.ClassLibrary
Imports Telerik.Web.UI

Namespace NTi.Modules.DFT_EDI_AttachDoc_V2
    Partial Class ViewDFT_EDI_AttachDoc_V2
        Inherits Entities.Modules.PortalModuleBase
        Dim strEDIConn As String = ConfigurationManager.ConnectionStrings("EDIConnection").ConnectionString
        Dim objConn As SqlConnection = Nothing

        Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            txtSearch.Attributes.Add("onkeyup", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" + btnSearch.UniqueID + "').click();return false;}} else {return true}; ")

            If Not Page.IsPostBack Then
                txtSearch.Focus()
                tblCardDetail.Visible = False
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
                '    Case 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19
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
            Try
                Dim ds As New DataSet
                ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, "sp_check_All_NewDS", _
                New SqlParameter("@card_id", CommonUtility.Get_StringValue(txtSearch.Text.Trim())))

                If ds.Tables(0).Rows.Count > 0 Then
                    tblCardDetail.Visible = True
                    With ds.Tables(0).Rows(0)
                        txtCompanyName_Th.Text = CommonUtility.Get_StringValue(.Item("company_thai"))
                        txtCompanyName_Eng.Text = CommonUtility.Get_StringValue(.Item("company_eng"))
                        txtCompany_Taxno.Text = CommonUtility.Get_StringValue(.Item("company_taxno"))
                        txtCompany_Juristic.Text = CommonUtility.Get_StringValue(.Item("company_juristic"))
                        txtCompany_Address.Text = CommonUtility.Get_StringValue(.Item("address_eng")) & " " & CommonUtility.Get_StringValue(.Item("province_eng"))
                        txtCompany_Phone.Text = CommonUtility.Get_StringValue(.Item("phone_no"))
                        txtCompany_Fax.Text = CommonUtility.Get_StringValue(.Item("fax_no"))
                        txtAuthorize1.Text = CommonUtility.Get_StringValue(.Item("authorize1"))
                        txtAuthorize2.Text = CommonUtility.Get_StringValue(.Item("authorize2"))
                        txtAuthorize3.Text = CommonUtility.Get_StringValue(.Item("authorize3"))
                        txtAuthorize4.Text = CommonUtility.Get_StringValue(.Item("authorize4"))
                        txtAuthorize5.Text = CommonUtility.Get_StringValue(.Item("authorize5"))
                        txtAuthorize_Remark.Text = CommonUtility.Get_StringValue(.Item("authorize_Remark"))
                        txtAuthName.Text = CommonUtility.Get_StringValue(.Item("AuthName"))
                        txtCard_Level.Text = CommonUtility.Get_StringValue(.Item("card_level"))
                        txtReturnMsg.Text = CommonUtility.Get_StringValue(.Item("retMessage"))
                        txtCARD_ID.Text = CommonUtility.Get_StringValue(.Item("CARD_ID"))
                    End With

                    rgAttachDocList.DataSource = LoadReferenceNoAttach()
                    rgAttachDocList.DataBind()
                End If
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End Sub

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

        Function LoadReferenceNoAttach() As DataTable
            Try
                Dim ds As New DataSet
                ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, "vi_select_RepAtt1_NewDS", _
                New SqlParameter("@TCat1", 2), _
                New SqlParameter("@TCat2", 3), _
                New SqlParameter("@dForm", ""), _
                New SqlParameter("@dTo", ""), _
                New SqlParameter("@total_day", 10), _
                New SqlParameter("@company_taxno", CommonUtility.Get_StringValue(txtCompany_Taxno.Text)), _
                New SqlParameter("@site_id", CommonUtility.Get_StringValue(lblRoleID.Text)), _
                New SqlParameter("@invh_run_auto", ""))

                Return ds.Tables(0)
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End Function

        Private Sub rgAttachDocList_ItemCommand(ByVal source As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles rgAttachDocList.ItemCommand
            Dim INVH_RUN_AUTO As String
            If e.CommandName = "ATTACH" Then
                INVH_RUN_AUTO = e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("invh_run_auto")

                Dim ds As New DataSet
                ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, "vi_approve_repAtt_NewDS", _
                New SqlParameter("@TCat", CommonUtility.Get_Int32(1)), _
                New SqlParameter("@INVH_RUN_AUTO", CommonUtility.Get_StringValue(INVH_RUN_AUTO)), _
                New SqlParameter("@USER_ID", CommonUtility.Get_StringValue(UserInfo.Username)), _
                New SqlParameter("@site_id", lblRoleID.Text))

                If ds.Tables(0).Rows.Count > 0 Then
                    Select Case CommonUtility.Get_Int32(ds.Tables(0).Rows(0).Item("retSTATUS"))
                        Case 0
                            rgAttachDocList.DataSource = LoadReferenceNoAttach()
                            rgAttachDocList.DataBind()
                        Case -1, -2, -3, -4, -5
                            Me.RadAjaxManager1.ResponseScripts.Add("window.alert('" & CommonUtility.Get_StringValue(ds.Tables(0).Rows(0).Item("retMessage")) & "');")
                    End Select
                End If
            End If
        End Sub

        Private Sub rgAttachDocList_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgAttachDocList.ItemDataBound
            If Not (e.Item.FindControl("lbtReference_Code2") Is Nothing) Then
                CType(e.Item.FindControl("lbtReference_Code2"), LinkButton).Attributes("onmouseover") = "this.style.cursor='hand';"
            End If
        End Sub

        Private Sub rgAttachDocList_PageIndexChanged(ByVal source As Object, ByVal e As Telerik.Web.UI.GridPageChangedEventArgs) Handles rgAttachDocList.PageIndexChanged
            rgAttachDocList.CurrentPageIndex = e.NewPageIndex
            rgAttachDocList.DataSource = LoadReferenceNoAttach()
            rgAttachDocList.DataBind()
        End Sub
    End Class

End Namespace
