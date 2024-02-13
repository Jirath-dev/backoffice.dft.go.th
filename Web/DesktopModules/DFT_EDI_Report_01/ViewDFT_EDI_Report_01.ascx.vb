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

Namespace NTi.Modules.DFT_EDI_Report_01
    Partial Class ViewDFT_EDI_Report_01
        Inherits Entities.Modules.PortalModuleBase
        Dim strConn As String = ConfigurationManager.ConnectionStrings("OriginConnection").ConnectionString
        Dim objConn As SqlConnection = Nothing

        Private Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
            Call LoadAllCompany()
        End Sub

        Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            If Not Page.IsPostBack Then
                rdpFromDate.SelectedDate = Today
                rdpToDate.SelectedDate = Today

                tblData.Visible = False
            End If

            'Check ��� User ��� Login ��������� Site �˹
            Dim myRoleControllers As New DotNetNuke.Security.Roles.RoleController()
            Dim i As Integer = 0
            For i = 0 To MyBase.UserInfo.Roles.Length - 1
                Dim myRoleInfo As DotNetNuke.Security.Roles.RoleInfo = myRoleControllers.GetRoleByName(0, MyBase.UserInfo.Roles(i))
                'by rut 7-09-2555 ���ѹ��� 22-01-2556 ��᷹��ҹ��ҧ ��ͧ����� Table "RoleList" ᷹
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
        Private Sub rgAllRequestList_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles rgAllRequestList.DataBound
            objConn.Close()
        End Sub

        Private Sub rgAllRequestList_PageIndexChanged(ByVal source As Object, ByVal e As Telerik.Web.UI.GridPageChangedEventArgs) Handles rgAllRequestList.PageIndexChanged
            rgAllRequestList.CurrentPageIndex = e.NewPageIndex
            rgAllRequestList.DataSource = LoadRequestList()
            rgAllRequestList.DataBind()
        End Sub

        Function LoadRequestList() As DataTable
            Try
                Dim ds As New DataSet
                objConn = New SqlConnection(strConn)
                ds = SqlHelper.ExecuteDataset(objConn, CommandType.StoredProcedure, "sp_Report_1", _
                New SqlParameter("@FROM_DATE", FunctionUtility.DMY2YMD(rdpFromDate.SelectedDate.Value)), _
                New SqlParameter("@TO_DATE", FunctionUtility.DMY2YMD(rdpToDate.SelectedDate.Value.AddDays(1))), _
                New SqlParameter("@COMPANY_TAXNO", "%"))
                Return ds.Tables(0)
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End Function

        Private Sub LoadAllCompany()
            Try
                Dim ds As New DataSet
                Dim strCommand As String
                strCommand = "select aa.* from (select COMPANY_TAXNO,COMPANY_ENG from COMPANY union select '%' as COMPANY_TAXNO,'�ء����ѷ' as COMPANY_ENG from COMPANY) aa order by aa.COMPANY_ENG"
                ds = SqlHelper.ExecuteDataset(strConn, CommandType.Text, strCommand)
                If ds.Tables(0).Rows.Count > 0 Then
                    rcbCompany.DataSource = ds.Tables(0)
                    rcbCompany.DataTextField = "COMPANY_ENG"
                    rcbCompany.DataValueField = "COMPANY_TAXNO"
                    rcbCompany.DataBind()

                    rcbCompany.SelectedValue = "%"
                End If
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End Sub

        Protected Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
            rgAllRequestList.DataSource = LoadRequestList()
            rgAllRequestList.DataBind()

            If rgAllRequestList.MasterTableView.Items.Count > 0 Then
                tblData.Visible = True
            Else
                tblData.Visible = False
                Me.RadAjaxManager1.ResponseScripts.Add("window.alert('��辺��¡�õ�����͹䢷��ӡ�ä���');")
            End If
        End Sub
    End Class

End Namespace
