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

Namespace NTi.Modules.DFT_EDI_Report_02
    Partial Class ViewDFT_EDI_Report_02
        Inherits Entities.Modules.PortalModuleBase
        Dim strConn As String = ConfigurationManager.ConnectionStrings("OriginConnection").ConnectionString
        Dim objConn As SqlConnection = Nothing

        Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            If Not Page.IsPostBack Then
                rdpFromDate.SelectedDate = Today
                rdpToDate.SelectedDate = Today
                trType1.Visible = False
                trType2.Visible = False

                rdpFromDate_v2.SelectedDate = Today
                rdpToDate_v2.SelectedDate = Today
                trType1_v2.Visible = False
                trType2_v2.Visible = False
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
        Function ckeck_total(ByVal By_form As String, ByVal By_num As String) As String
            Dim temp_str As String = ""
            Select Case By_form.ToUpper
                Case "FORM2_1"
                    temp_str = CStr(CInt(By_num) / 60)
                Case Else
                    temp_str = CStr(CInt(By_num) / 30)
            End Select
            Return temp_str
        End Function
        Protected Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
            Select Case rblReportType.SelectedValue
                Case 0
                    trType1.Visible = True
                    trType2.Visible = False
                    rgReceiptSummary.DataSource = LoadReceiptSummary()
                    rgReceiptSummary.DataBind()

                    If rgReceiptSummary.MasterTableView.Items.Count > 0 Then
                        btnPrint.Visible = True
                    Else
                        btnPrint.Visible = False
                    End If
                Case 1
                    trType1.Visible = False
                    trType2.Visible = True
                    rgReceiptDetailSummary.DataSource = LoadReceiptDetailSUmmary()
                    rgReceiptDetailSummary.DataBind()

                    If rgReceiptDetailSummary.MasterTableView.Items.Count > 0 Then
                        btnPrint.Visible = True
                    Else
                        btnPrint.Visible = False
                    End If
            End Select
        End Sub

        ''ByTine 07-01-2559 ปรับปรุงแก้ไขใบเสร็จใหม่
        Protected Sub btnSearch_v2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch_v2.Click
            Select Case rblReportType_v2.SelectedValue
                Case 0
                    trType1_v2.Visible = True
                    trType2_v2.Visible = False
                    rgReceiptSummary_v2.DataSource = LoadReceiptSummary_v2()
                    rgReceiptSummary_v2.DataBind()

                    If rgReceiptSummary_v2.MasterTableView.Items.Count > 0 Then
                        btnPrint_v2.Visible = True
                    Else
                        btnPrint_v2.Visible = False
                    End If
                Case 1
                    trType1_v2.Visible = False
                    trType2_v2.Visible = True
                    rgReceiptDetailSummary_v2.DataSource = LoadReceiptDetailSUmmary_v2()
                    rgReceiptDetailSummary_v2.DataBind()

                    If rgReceiptDetailSummary_v2.MasterTableView.Items.Count > 0 Then
                        btnPrint_v2.Visible = True
                    Else
                        btnPrint_v2.Visible = False
                    End If
            End Select
        End Sub

        Private Sub rgReceiptSummary_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles rgReceiptSummary.DataBound
            objConn.Close()
        End Sub

        ''ByTine 07-01-2559 ปรับปรุงแก้ไขใบเสร็จใหม่
        Private Sub rgReceiptSummary_v2_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles rgReceiptSummary_v2.DataBound
            objConn.Close()
        End Sub

        Function LoadReceiptSummary() As DataTable
            Try
                Dim ds As New DataSet
                objConn = New SqlConnection(strConn)

                ds = SqlHelper.ExecuteDataset(objConn, CommandType.StoredProcedure, "sp_report_5_NewDS", _
                New SqlParameter("@FROM_DATE", rdpFromDate.SelectedDate.Value.ToString("yyyyMMdd")), _
                New SqlParameter("@TO_DATE", rdpToDate.SelectedDate.Value.ToString("yyyyMMdd")), _
                New SqlParameter("@SITE_ID", lblRoleID.Text))

                Return ds.Tables(0)
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End Function

        Function LoadReceiptDetailSUmmary() As DataTable
            Try
                Dim ds As New DataSet
                objConn = New SqlConnection(strConn)

                ds = SqlHelper.ExecuteDataset(objConn, CommandType.StoredProcedure, "sp_report_4_NewDS", _
                New SqlParameter("@FROM_DATE", rdpFromDate.SelectedDate.Value.ToString("yyyyMMdd")), _
                New SqlParameter("@TO_DATE", rdpToDate.SelectedDate.Value.ToString("yyyyMMdd")), _
                New SqlParameter("@SITE_ID", lblRoleID.Text))

                Return ds.Tables(0)
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End Function

        ''ByTine 07-01-2559 ปรับปรุงแก้ไขใบเสร็จใหม่
        Function LoadReceiptSummary_v2() As DataTable
            Try
                Dim ds As New DataSet
                objConn = New SqlConnection(strConn)

                ds = SqlHelper.ExecuteDataset(objConn, CommandType.StoredProcedure, "sp_report_5_NewDS_v2", _
                New SqlParameter("@FROM_DATE", rdpFromDate_v2.SelectedDate.Value.ToString("yyyyMMdd")), _
                New SqlParameter("@TO_DATE", rdpToDate_v2.SelectedDate.Value.ToString("yyyyMMdd")), _
                New SqlParameter("@SITE_ID", lblRoleID.Text))

                Return ds.Tables(0)
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End Function

        ''ByTine 07-01-2559 ปรับปรุงแก้ไขใบเสร็จใหม่
        Function LoadReceiptDetailSUmmary_v2() As DataTable
            Try
                Dim ds As New DataSet
                objConn = New SqlConnection(strConn)

                ds = SqlHelper.ExecuteDataset(objConn, CommandType.StoredProcedure, "sp_report_4_NewDS_v2", _
                New SqlParameter("@FROM_DATE", rdpFromDate_v2.SelectedDate.Value.ToString("yyyyMMdd")), _
                New SqlParameter("@TO_DATE", rdpToDate_v2.SelectedDate.Value.ToString("yyyyMMdd")), _
                New SqlParameter("@SITE_ID", lblRoleID.Text))

                Return ds.Tables(0)
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End Function

        Private Sub rgReceiptDetailSummary_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles rgReceiptDetailSummary.DataBound
            objConn.Close()
        End Sub

        ''ByTine 07-01-2559 ปรับปรุงแก้ไขใบเสร็จใหม่
        Private Sub rgReceiptDetailSummary_v2_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles rgReceiptDetailSummary_v2.DataBound
            objConn.Close()
        End Sub

        Private Sub rgReceiptDetailSummary_PageIndexChanged(ByVal source As Object, ByVal e As Telerik.Web.UI.GridPageChangedEventArgs) Handles rgReceiptDetailSummary.PageIndexChanged
            rgReceiptDetailSummary.CurrentPageIndex = e.NewPageIndex
            rgReceiptDetailSummary.DataSource = LoadReceiptDetailSUmmary()
            rgReceiptDetailSummary.DataBind()
        End Sub

        ''ByTine 07-01-2559 ปรับปรุงแก้ไขใบเสร็จใหม่
        Private Sub rgReceiptDetailSummary_v2_PageIndexChanged(ByVal source As Object, ByVal e As Telerik.Web.UI.GridPageChangedEventArgs) Handles rgReceiptDetailSummary_v2.PageIndexChanged
            rgReceiptDetailSummary_v2.CurrentPageIndex = e.NewPageIndex
            rgReceiptDetailSummary_v2.DataSource = LoadReceiptDetailSUmmary_v2()
            rgReceiptDetailSummary_v2.DataBind()
        End Sub

        Protected Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
            Me.RadAjaxManager1.ResponseScripts.Add("window.open('http://" & DotNetNuke.Common.GetDomainName(Request) & "/DesktopModules/DFT_EDI_Report_02/frmEDI_Report_02.aspx" & _
            "?TYPE=" & rblReportType.SelectedValue & _
            "&FROM_DATE=" & rdpFromDate.SelectedDate.Value.ToString("yyyyMMdd") & _
            "&TO_DATE=" & rdpToDate.SelectedDate.Value.ToString("yyyyMMdd") & _
            "&SITE_ID=" & lblRoleID.Text & _
            "&BillType=1" & _
            "',null,'height=600, width=800,status=no, resizable= no,scrollbars=yes, toolbar=no,location=no,menubar=no,center=yes');")

        End Sub

        Protected Sub btnPrint_v2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint_v2.Click
            Me.RadAjaxManager1.ResponseScripts.Add("window.open('http://" & DotNetNuke.Common.GetDomainName(Request) & "/DesktopModules/DFT_EDI_Report_02/frmEDI_Report_02.aspx" & _
            "?TYPE=" & rblReportType_v2.SelectedValue & _
            "&FROM_DATE=" & rdpFromDate_v2.SelectedDate.Value.ToString("yyyyMMdd") & _
            "&TO_DATE=" & rdpToDate_v2.SelectedDate.Value.ToString("yyyyMMdd") & _
            "&SITE_ID=" & lblRoleID.Text & _
            "&BillType=2" & _
            "',null,'height=600, width=800,status=no, resizable= no,scrollbars=yes, toolbar=no,location=no,menubar=no,center=yes');")

        End Sub
    End Class

End Namespace
