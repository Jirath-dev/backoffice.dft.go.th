Imports System.Web.UI
Imports System.Collections.Generic
Imports System.Reflection

Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports DotNetNuke
Imports DotNetNuke.Services.Exceptions
Imports DotNetNuke.Services.Localization
Imports DotNetNuke.Entities.Users

Namespace NTi.Modules.DFT_EDI_F03_Normal
    Partial Class ViewDFT_EDI_F03_Normal
        Inherits Entities.Modules.PortalModuleBase
        Dim strRegConn As String = ConfigurationManager.ConnectionStrings("RegisBackConnection").ConnectionString
        Dim strEDIConn As String = ConfigurationManager.ConnectionStrings("OriginConnection").ConnectionString
        Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

            Dim myRoleControllers As New DotNetNuke.Security.Roles.RoleController()
            Dim i As Integer = 0
            For i = 0 To MyBase.UserInfo.Roles.Length - 1
                Dim myRoleInfo As DotNetNuke.Security.Roles.RoleInfo = myRoleControllers.GetRoleByName(0, MyBase.UserInfo.Roles(i))

                If Get_ListRoles(myRoleInfo.RoleID, myRoleInfo.RoleName) <> "" Then
                    Exit For
                End If
            Next i

            If Not Page.IsPostBack Then
                Dim isShowPrint As Boolean = IsShowReceive(Session("ssRoleName"))

                If isShowPrint Then
                    trprintrecive.Style.Item("display") = ""
                    trprintrepeat.Style.Item("display") = ""
                    trprintrecive_edi.Style.Item("display") = ""
                    trprintrepeat_edi.Style.Item("display") = ""
                Else
                    trprintrecive.Style.Item("display") = "none"
                    trprintrepeat.Style.Item("display") = "none"
                    trprintrecive_edi.Style.Item("display") = "none"
                    trprintrepeat_edi.Style.Item("display") = "none"
                End If

                If IsRoleName("EDI_DS") Then
                    panel_ds1.Visible = False
                    panel_ds2.Visible = True
                Else
                    panel_ds1.Visible = True
                    panel_ds2.Visible = False
                End If

            End If
        End Sub

        Function Get_ListRoles(ByVal ByRoleID As Integer, ByVal ByRoleName As String) As String
            Dim DSRoles As SqlDataReader
            Dim strEDIConn As String = ConfigurationManager.ConnectionStrings("OriginConnection").ConnectionString

            DSRoles = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "RoleList_GetList",
                            New SqlParameter("@ListRoleNameCase", ByRoleID))

            Dim strListRole As String = ""

            If DSRoles.HasRows Then
                strListRole = ByRoleName
                Session("ssRoleName") = strListRole
            End If

            Return strListRole
        End Function

        Function IsShowReceive(site_id As String) As Boolean
            Dim ret As Boolean = True
            Try
                Dim Cmd As String = "Select s.is_print_receipt from site_plus s Where s.site_id = @site_id"
                Dim Prm As New SqlParameter("@site_id", site_id)
                Dim Ds As DataSet = SqlHelper.ExecuteDataset(strEDIConn, CommandType.Text, Cmd, Prm)

                If Ds.Tables(0).Rows.Count > 0 Then
                    ret = ConvertToBoolean(Ds.Tables(0).Rows(0).Item("is_print_receipt"))
                End If
            Catch ex As Exception

            End Try

            Return ret

        End Function

        Function ConvertToBoolean(obj As Object) As Boolean
            Dim ret As Boolean = False
            Try
                ret = Convert.ToBoolean(obj)
            Catch ex As Exception

            End Try

            Return ret

        End Function

        Protected Function IsRoleName(ByVal str_rolename As String) As Boolean
            Dim ret As Boolean = False
            Dim myRoleControllers As New DotNetNuke.Security.Roles.RoleController()
            Dim i As Integer = 0
            For i = 0 To MyBase.UserInfo.Roles.Length - 1
                Dim myRoleInfo As DotNetNuke.Security.Roles.RoleInfo = myRoleControllers.GetRoleByName(0, MyBase.UserInfo.Roles(i))

                If myRoleInfo.RoleName = str_rolename Then
                    ret = True
                    Exit For
                End If
            Next i
            Return ret
        End Function

    End Class

End Namespace
