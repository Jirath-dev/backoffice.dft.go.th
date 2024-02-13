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

Namespace NTi.Modules.DFT_EDI_Report_04
    Partial Class ViewDFT_EDI_Report_04
        Inherits Entities.Modules.PortalModuleBase
        Dim strConn As String = ConfigurationManager.ConnectionStrings("OriginConnection").ConnectionString
        Dim objConn As SqlConnection = Nothing

        Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            If Not Page.IsPostBack Then
                rdpFromDate.SelectedDate = Today
                rdpToDate.SelectedDate = Today

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
        Protected Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
            rgCetificateList.DataSource = SumFORM_EDI(rblType.SelectedValue)
            rgCetificateList.DataBind()
        End Sub

        Function SumFORM_EDI(ByVal Type As String) As DataTable
            Try
                Dim ds As New DataSet
                objConn = New SqlConnection(strConn)

                Dim rpt_type As String = ""
                Select Case Type
                    Case "0"
                        rpt_type = "1"
                    Case "1"
                        rpt_type = "2"
                End Select

                ds = SqlHelper.ExecuteDataset(objConn, CommandType.StoredProcedure, "vi_EDI_REPORT_04_NewDS",
                        New SqlParameter("@FROM_DATE", FunctionUtility.DMY2YMD(rdpFromDate.SelectedDate.Value)),
                        New SqlParameter("@TO_DATE", FunctionUtility.DMY2YMD(rdpToDate.SelectedDate.Value)),
                        New SqlParameter("@SITE_ID", CommonUtility.Get_StringValue(lblRoleID.Text)),
                        New SqlParameter("@TYPE", rpt_type))

                Return ds.Tables(0)

            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End Function

        Private Sub rgCetificateList_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles rgCetificateList.DataBound
            objConn.Close()

            If rgCetificateList.MasterTableView.Items.Count > 0 Then
                btnPrint.Visible = True
            Else
                btnPrint.Visible = False
            End If

        End Sub

        Private Sub rgCetificateList_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgCetificateList.NeedDataSource
            rgCetificateList.DataSource = SumFORM_EDI(rblType.SelectedValue)
        End Sub

        Protected Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
            If rblType.SelectedValue = "0" Then
                Response.Write("<script language ='javascript'> window.open('http://" & DotNetNuke.Common.GetDomainName(Request) & "/DesktopModules/DFT_EDI_Report_04/frmEDI_Report_04.aspx?TCat=0&FROM_DATE=" & FunctionUtility.DMY2YMD(rdpFromDate.SelectedDate.Value) & "&TO_DATE=" & FunctionUtility.DMY2YMD(rdpToDate.SelectedDate.Value) & "&SITE_ID=" & lblRoleID.Text & "',null,'height=600, width=800,status=yes, resizable= yes,scrollbars=no, toolbar=no,location=no,menubar=no'); </script>")
            ElseIf rblType.SelectedValue = "1" Then
                Response.Write("<script language ='javascript'> window.open('http://" & DotNetNuke.Common.GetDomainName(Request) & "/DesktopModules/DFT_EDI_Report_04/frmEDI_Report_04.aspx?TCat=1&FROM_DATE=" & FunctionUtility.DMY2YMD(rdpFromDate.SelectedDate.Value) & "&TO_DATE=" & FunctionUtility.DMY2YMD(rdpToDate.SelectedDate.Value) & "&SITE_ID=" & lblRoleID.Text & "',null,'height=600, width=800,status=yes, resizable= yes,scrollbars=no, toolbar=no,location=no,menubar=no'); </script>")
            End If
        End Sub
    End Class

End Namespace
