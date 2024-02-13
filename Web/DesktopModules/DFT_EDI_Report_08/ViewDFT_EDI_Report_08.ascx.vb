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

Namespace NTi.Modules.DFT_EDI_Report_08
    Partial Class ViewDFT_EDI_Report_08
        Inherits Entities.Modules.PortalModuleBase
        Dim strConn As String = ConfigurationManager.ConnectionStrings("OriginConnection").ConnectionString

        Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            If Not Page.IsPostBack Then
                rdpFromDate.SelectedDate = Today
                rdpToDate.SelectedDate = Today

                tblData.Visible = False

                Call LoadFormType()
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
        Private Sub LoadFormType()
            Try
                Dim ds As New DataSet
                Dim strCommand As String = "select * from FORM_TYPE where FORM_TYPE<>'ALL' order by FORM_TYPE"
                ds = SqlHelper.ExecuteDataset(strConn, CommandType.Text, strCommand)
                If ds.Tables(0).Rows.Count > 0 Then
                    rcbFormType.DataSource = ds.Tables(0)
                    rcbFormType.DataTextField = "form_name"
                    rcbFormType.DataValueField = "form_type"
                    rcbFormType.DataBind()

                    rcbFormType.SelectedValue = "FORM1"
                End If
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End Sub

        Protected Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
            rgCetificateList.DataSource = LoadData()
            rgCetificateList.DataBind()

            If rgCetificateList.MasterTableView.Items.Count > 0 Then
                btnPrint.Visible = False
                tblData.Visible = True
            Else
                btnPrint.Visible = False
                tblData.Visible = False
                Me.RadAjaxManager1.ResponseScripts.Add("window.alert('ไม่พบข้อมูลตามเงื่อนไขที่ทำการค้นหา !!!');")
            End If
        End Sub

        Function LoadData() As DataTable
            Try
                'sp_report_check_form_manual '20090303','20090303','FORM1',0
                Dim ds As New DataSet
                ds = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, "sp_report_check_form_manual", _
                New SqlParameter("@FROM_DATE", FunctionUtility.DMY2YMD(rdpFromDate.SelectedDate.Value)), _
                New SqlParameter("@TO_DATE", FunctionUtility.DMY2YMD(rdpToDate.SelectedDate.Value.AddDays(1))), _
                New SqlParameter("@FORM_TYPE", CommonUtility.Get_StringValue(rcbFormType.SelectedValue)), _
                New SqlParameter("@RPT_TYPE", CommonUtility.Get_Int32(rblRPT_TYPE.SelectedValue)))

                Return ds.Tables(0)
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End Function
    End Class

End Namespace
