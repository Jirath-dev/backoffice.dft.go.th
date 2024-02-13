Imports Microsoft.ApplicationBlocks.Data
Imports System.Data.SqlClient
Imports DFT.Dotnetnuke.ClassLibrary

Imports DotNetNuke.Entities.Users

Partial Public Class frmCheckSummary
    Inherits System.Web.UI.Page

    Dim strEDIConn As String = ConfigurationManager.ConnectionStrings("OriginConnection").ConnectionString
    Private RoleID As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        LoadFormTypeForSearching()

        If Not Page.IsPostBack Then
            rdpFromDate.SelectedDate = Now.Month & "/" & Now.Day & "/" & Now.Year 'Now.Month & "/01/" & Now.Year
            rdpToDate.SelectedDate = Now.Month & "/" & Now.Day & "/" & Now.Year

            dropFormType.SelectedValue = "ALL"

        End If

        Dim myRoleControllers As New DotNetNuke.Security.Roles.RoleController()
        Dim i As Integer = 0
        Dim objUserInfo As UserInfo = UserController.GetCurrentUserInfo

        For i = 0 To objUserInfo.Roles.Length - 1
            Dim myRoleInfo As DotNetNuke.Security.Roles.RoleInfo = myRoleControllers.GetRoleByName(0, objUserInfo.Roles(i))

            'by rut 7-09-2555 ใช้วันที่ 22-01-2556 ใช้แทนด้านล่าง ต้องเพิ่มใน Table "RoleList" แทน
            If Get_ListRoles(myRoleInfo.RoleID, myRoleInfo.RoleName) <> "" Then
                Exit For
            End If

        Next i

        If Not Page.IsPostBack Then
            CheckSummary()
        End If

    End Sub

    Private Sub LoadFormTypeForSearching()
        Try
            Dim ds As New DataSet
            Dim strCommand As String
            strCommand = "SELECT form_type AS CODE, form_nameUsd AS DESCRIPTION " &
                         "FROM form_type ORDER BY ShowOrder"
            ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.Text, strCommand)

            If ds.Tables(0).Rows.Count > 0 Then
                dropFormType.DataSource = ds.Tables(0)
                dropFormType.DataTextField = "DESCRIPTION"
                dropFormType.DataValueField = "CODE"
                dropFormType.DataBind()
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Function Get_ListRoles(ByVal ByRoleID As Integer, ByVal ByRoleName As String) As String
        Dim DSRoles As SqlDataReader
        Dim strEDIConn As String = ConfigurationManager.ConnectionStrings("OriginConnection").ConnectionString

        DSRoles = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "RoleList_GetList",
                        New SqlParameter("@ListRoleNameCase", ByRoleID))

        Dim strListRole As String = ""

        If DSRoles.HasRows Then
            strListRole = ByRoleName
            RoleID = strListRole
        End If

        Return strListRole
    End Function

    Protected Sub CheckSummary()
        Dim strhtml As String = "<table class=""tb-data"" style=""font-size:10pt;"" cellpadding=""0"" cellspacing=""0""><tr><td class=""head"">ชื่อฟอร์ม</td><td class=""head"">จำนวนทั้งหมด</td><td class=""head"">ผ่าน</td><td class=""head"">ไม่ผ่าน</td></tr>"
        Try
            Dim Total As Long = 0
            Dim totalErr As Long = 0
            Dim totalOk As Long = 0

            Dim prm(6) As SqlClient.SqlParameter
            prm(0) = New SqlClient.SqlParameter("@FORM_TYPE", DataUtil.ConvertToString(dropFormType.SelectedValue))
            prm(1) = New SqlClient.SqlParameter("@FROM_DATE", FunctionUtility.DMY2YMD(rdpFromDate.SelectedDate.Value))
            prm(2) = New SqlClient.SqlParameter("@TO_DATE", FunctionUtility.DMY2YMD(rdpToDate.SelectedDate.Value))
            prm(3) = New SqlClient.SqlParameter("@DISPLAY_FLAG", "0")
            prm(4) = New SqlClient.SqlParameter("@SITE_ID", DataUtil.ConvertToString(RoleID))

            Dim ds As New DataSet
            ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, "vi_form_edi_getForCheckAtt_Summary_NewDS", prm)

            If ds.Tables(0).Rows.Count > 0 Then
                For Each row As DataRow In ds.Tables(0).Rows
                    Total += row(1)
                    totalOk += row(2)
                    totalErr += row(3)
                    strhtml &= "<tr><td>" & row(0).ToString() & "</td><td align=""center"">" & row(1).ToString() & "</td><td align=""center"">" & row(2).ToString() & "</td><td align=""center"">" & row(3).ToString() & "</td></tr>"
                Next
                strhtml &= "<tr><td align=""center""><b>รวม</b></td><td align=""center""><b>" & Total.ToString() & "</b></td><td align=""center""><b>" & totalOk.ToString() & "</b></td><td align=""center""><b>" & totalErr.ToString() & "</b></td></tr>"
            Else

            End If

            lblSummary.Text = strhtml & "</table>"

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        CheckSummary()
    End Sub


End Class