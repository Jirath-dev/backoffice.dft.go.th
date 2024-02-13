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

Namespace NTi.Modules.DFT_EDI_BookingForm_Summary
    Partial Class ViewDFT_EDI_BookingForm_Summary
        Inherits Entities.Modules.PortalModuleBase
        Dim strConn As String = ConfigurationManager.ConnectionStrings("OriginConnection").ConnectionString
        Dim objConn As SqlConnection = Nothing
        Dim myReader As SqlDataReader = Nothing

        Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            If Not Page.IsPostBack Then
                rdpFromDate.SelectedDate = Today
                rdpToDate.SelectedDate = Today
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
        Private Sub rgRequestList_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles rgRequestList.DataBound
            myReader.Close()
            objConn.Close()
        End Sub

        Private Sub rgRequestList_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgRequestList.NeedDataSource
            rgRequestList.DataSource = LoadRequestSummary()
        End Sub

        Function LoadRequestSummary() As SqlDataReader
            Try
                objConn = New SqlConnection(strConn)
                myReader = SqlHelper.ExecuteReader(objConn, CommandType.StoredProcedure, "sp_common_form_manual_countForm", _
                New SqlParameter("@SITE_ID", lblRoleID.Text), _
                New SqlParameter("@FROM_DATE", FunctionUtility.DMY2YMD(rdpFromDate.SelectedDate.Value)), _
                New SqlParameter("@TO_DATE", FunctionUtility.DMY2YMD(rdpToDate.SelectedDate.Value.AddDays(1))))
                Return myReader
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End Function
    End Class

End Namespace
