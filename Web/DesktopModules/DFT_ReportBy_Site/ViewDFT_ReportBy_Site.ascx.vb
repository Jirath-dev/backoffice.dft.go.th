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

Imports DataDynamics.ActiveReports.Export.Pdf
Imports DataDynamics.ActiveReports.Export.Xls
Imports System.IO

Imports System.Windows.Forms
Imports Microsoft.Office.Interop.Excel
Imports System.Object
Imports System.Runtime.InteropServices

Imports Microsoft.Office.Interop
Imports System.Runtime.InteropServices.Marshal
Namespace NTi.Modules.DFT_ReportBy_Site
    Partial Class ViewDFT_ReportBy_Site
        Inherits Entities.Modules.PortalModuleBase
        Dim strEDIConn As String = ConfigurationManager.ConnectionStrings("ediConReport").ConnectionString

        Dim objConn As SqlConnection = Nothing
#Region "Event Handlers"

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load, Me.Load
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

#End Region

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
        Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
            Me.RadAjaxManager1.ResponseScripts.Add("window.open('http://" & DotNetNuke.Common.GetDomainName(Request) & "/DesktopModules/DFT_ReportBy_Site/WebFormReportBySite.aspx?action=viewprint&sendRole=" & Session("ssRoleName") & "&SendFdate=" & _
                                        rdpFromDate.SelectedDate.Value & "&sendTdate=" & rdpToDate.SelectedDate.Value & "',null,'height=600, width=800,status=no, resizable= no,scrollbars=yes, toolbar=no,location=no,menubar=no,center=yes');")

            'Me.RadAjaxManager1.ResponseScripts.Add("window.open('http://" & DotNetNuke.Common.GetDomainName(Request) & "/DesktopModules/DFT_ReportBy_Site/WebFormReportBySite.aspx?action=viewprint&sendRole=" & Session("ssRoleName") & "&SendFdate=" & _
            '                            rdpFromDate.SelectedDate.Value & "&sendTdate=" & rdpToDate.SelectedDate.Value.AddDays(1) & "',null,'height=600, width=800,status=no, resizable= no,scrollbars=yes, toolbar=no,location=no,menubar=no,center=yes');")

        End Sub

        Function LoadDataForm_ds(ByVal sFromDate As String, ByVal sTodate As String, ByVal sRoleID As String) As DataSet
            Try
                Dim ds As New DataSet
                objConn = New SqlConnection(strEDIConn)
                ds = SqlHelper.ExecuteDataset(objConn, CommandType.StoredProcedure, "vi_getreportExcel_By_Site_NewDS", _
                New SqlParameter("@FORM_date", FunctionUtility.DMY2YMD(sFromDate)), _
                New SqlParameter("@FORM_todate", FunctionUtility.DMY2YMD(sTodate)), _
                New SqlParameter("@site_id", sRoleID))
                Return ds
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End Function

        Protected Sub btnExportEX_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExportEX.Click
            If LoadDataForm_ds(rdpFromDate.SelectedDate.Value, rdpToDate.SelectedDate.Value, Session("ssRoleName")).Tables(0).Rows.Count > 0 Then
                DataSetToExcel.Convert(LoadDataForm_ds(rdpFromDate.SelectedDate.Value, rdpToDate.SelectedDate.Value.AddDays(1), Session("ssRoleName")), Response)
            Else
                RadAjaxManager1.ResponseScripts.Add("window.alert('ไม่พบรายงาน');")
            End If
            'If LoadDataForm_ds(rdpFromDate.SelectedDate.Value, rdpToDate.SelectedDate.Value.AddDays(1), Session("ssRoleName")).Tables(0).Rows.Count > 0 Then
            '    DataSetToExcel.Convert(LoadDataForm_ds(rdpFromDate.SelectedDate.Value, rdpToDate.SelectedDate.Value.AddDays(1), Session("ssRoleName")), Response)
            'Else
            '    RadAjaxManager1.ResponseScripts.Add("window.alert('ไม่พบรายงาน');")
            'End If
        End Sub
    End Class

End Namespace
