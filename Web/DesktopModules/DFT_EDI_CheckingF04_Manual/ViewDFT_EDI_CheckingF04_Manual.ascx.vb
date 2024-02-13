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

Namespace NTi.Modules.DFT_EDI_CheckingF04_Manual
    Partial Class ViewDFT_EDI_CheckingF04_Manual
        Inherits Entities.Modules.PortalModuleBase
        Dim strEDIConn As String = ConfigurationManager.ConnectionStrings("OriginConnection").ConnectionString
        Dim myReader As SqlDataReader = Nothing
        Dim objConn As SqlConnection = Nothing

        Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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

            If Not Page.IsPostBack Then
                txtSearch.Focus()
                rdpFromDate.SelectedDate = Today
                rdpToDate.SelectedDate = Today
                Call LoadSite()
                Call LoadFormType()
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
        Private Sub LoadSite()
            Try
                Dim ds As New DataSet
                Dim strCommand As String
                strCommand = "SELECT  site_id, site_name FROM site_plus WHERE (active_status = 'Y') ORDER BY site_code"
                ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.Text, strCommand)
                If ds.Tables(0).Rows.Count > 0 Then
                    cboSite.DataSource = ds.Tables(0)
                    cboSite.DataTextField = "site_name"
                    cboSite.DataValueField = "site_id"
                    cboSite.DataBind()
                End If

                cboSite.SelectedValue = lblRoleID.Text
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End Sub

        Private Sub LoadFormType()
            Try
                Dim ds As New DataSet
                Dim strCommand As String
                strCommand = "SELECT form_type, form_name FROM form_type WHERE (form_type <> 'ALL')"
                ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.Text, strCommand)
                If ds.Tables(0).Rows.Count > 0 Then
                    dropFormType.DataSource = ds.Tables(0)
                    dropFormType.DataTextField = "form_name"
                    dropFormType.DataValueField = "form_type"
                    dropFormType.DataBind()
                End If
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End Sub

        Private Sub rgCertificateList_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles rgCertificateList.DataBound
            myReader.Close()
            objConn.Close()
        End Sub

        Private Sub rgCertificateList_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgCertificateList.NeedDataSource
            rgCertificateList.DataSource = LoadCO()
        End Sub

        Function LoadCO() As SqlDataReader
            objConn = New SqlConnection(strEDIConn)
            myReader = SqlHelper.ExecuteReader(objConn, CommandType.StoredProcedure, "vi_selectCheckingFomr_Manual_NewDS", _
            New SqlParameter("@REFERENCE_CODE2", CommonUtility.Get_StringValue(txtSearch.Text.Trim())), _
            New SqlParameter("@SITE_ID", CommonUtility.Get_StringValue(cboSite.SelectedValue)), _
            New SqlParameter("@FORM_TYPE", CommonUtility.Get_StringValue(dropFormType.SelectedValue)), _
            New SqlParameter("@FROM_DATE", FunctionUtility.DMY2YMD(rdpFromDate.SelectedDate.Value)), _
            New SqlParameter("@TO_DATE", FunctionUtility.DMY2YMD(rdpToDate.SelectedDate.Value)))

            Return myReader
        End Function

        Protected Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
            rgCertificateList.Rebind()
        End Sub
    End Class

End Namespace
