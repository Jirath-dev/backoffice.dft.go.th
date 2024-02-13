Imports System.Web.UI
Imports System.Collections.Generic
Imports System.Reflection

Imports DotNetNuke
Imports DotNetNuke.Services.Exceptions
Imports DotNetNuke.Services.Localization

Imports Microsoft.ApplicationBlocks.Data
Imports DFT.Dotnetnuke.ClassLibrary
Imports System.Data.SqlClient
Imports Telerik.Web.UI

Namespace NTi.Modules.DFT_EDI_F04_Report_08
    Partial Class ViewDFT_EDI_F04_Report_08
        Inherits Entities.Modules.PortalModuleBase
        Dim strConn As String = ConfigurationManager.ConnectionStrings("OriginConnection").ConnectionString
        Dim objConn As SqlConnection = Nothing
        Dim myReader As SqlDataReader

        Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            If Not Page.IsPostBack Then
                Call LoadFormType()

                tblData.Visible = False
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
            Next
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
        Private Sub LoadFormType()
            Try
                Dim strCommand As String
                strCommand = "SELECT * FROM form_type WHERE form_type <> 'ALL' ORDER BY ShowOrder"
                Dim ds As New DataSet
                ds = SqlHelper.ExecuteDataset(strConn, CommandType.Text, strCommand)
                If ds.Tables(0).Rows.Count > 0 Then
                    rcbSFormType.DataSource = ds.Tables(0)
                    rcbSFormType.DataTextField = "form_name"
                    rcbSFormType.DataValueField = "form_type"
                    rcbSFormType.DataBind()
                End If

                rcbSFormType.Items.Insert(0, New RadComboBoxItem("กรุณาเลือกรายชื่อฟอร์ม", 0))
                rcbSFormType.SelectedValue = 0
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End Sub

        Protected Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
            If rcbSFormType.SelectedValue = "0" Then
                Me.RadAjaxManager1.ResponseScripts.Add("window.alert('กรุณาเลือกประเภทหนังสือรับรองฯ ก่อนทำการค้นหา');")
                tblData.Visible = False

                Call LoadFormType()
            Else
                rgAllRequestList.DataSource = LoadData()
                rgAllRequestList.DataBind()

                If rgAllRequestList.MasterTableView.Items.Count > 0 Then
                    tblData.Visible = True
                Else
                    Me.RadAjaxManager1.ResponseScripts.Add("window.alert('ไม่พบข้อมูลตามเงื่อนไขที่ทำการค้นหา');")
                End If
            End If
        End Sub

        Function LoadData() As DataTable
            Try
                Dim strCommand As String
                strCommand = " SELECT form_header_edi.*, (SELECT form_name FROM form_type WHERE (form_type = form_header_edi.form_type)) AS form_name FROM form_header_edi " & _
                             " WHERE (Rep_status = 'A' OR Rep_status = 'N' OR Rep_status = 'W') " & _
                             " AND (edi_status_id = 'N') AND (CONVERT(varchar(8), approve_date, 112) > '20100101') " & _
                             " AND (form_type = '" & rcbSFormType.SelectedValue & "') " & _
                             " AND (site_id LIKE '%" & lblRoleID.Text.Trim() & "%') ORDER BY reference_code2"
                Dim ds As New DataSet
                ds = SqlHelper.ExecuteDataset(strConn, CommandType.Text, strCommand)
                Return ds.Tables(0)
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End Function

        Private Sub rgAllRequestList_PageIndexChanged(ByVal source As Object, ByVal e As Telerik.Web.UI.GridPageChangedEventArgs) Handles rgAllRequestList.PageIndexChanged
            rgAllRequestList.CurrentPageIndex = e.NewPageIndex
            rgAllRequestList.DataSource = LoadData()
            rgAllRequestList.DataBind()
        End Sub

        Private Sub rgAllRequestList_PageSizeChanged(ByVal source As Object, ByVal e As Telerik.Web.UI.GridPageSizeChangedEventArgs) Handles rgAllRequestList.PageSizeChanged
            rgAllRequestList.PageSize = e.NewPageSize
            rgAllRequestList.DataSource = LoadData()
            rgAllRequestList.DataBind()
        End Sub

        Private Sub rgAllRequestList_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgAllRequestList.ItemDataBound
            If Not (e.Item.FindControl("lblIndex") Is Nothing) Then
                Dim lbl As Label = e.Item.FindControl("lblIndex")
                lbl.Text = (rgAllRequestList.MasterTableView.CurrentPageIndex * rgAllRequestList.MasterTableView.PageSize) + (e.Item.ItemIndex + 1)
            End If
        End Sub
    End Class

End Namespace
