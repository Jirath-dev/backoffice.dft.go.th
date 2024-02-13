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

Namespace NTi.Modules.DFT_EDI_Report_03
    Partial Class ViewDFT_EDI_Report_03
        Inherits Entities.Modules.PortalModuleBase
        Dim strConn As String = ConfigurationManager.ConnectionStrings("OriginConnection").ConnectionString
        Dim objConn As SqlConnection = Nothing

        Private Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
            Call LoadFormType()
            Call LoadCountry()
        End Sub

        Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            If Not Page.IsPostBack Then
                rdpFromDate.SelectedDate = Today
                rdpToDate.SelectedDate = Today

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
        Protected Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
            Select Case rblType.SelectedValue
                Case "EDI"
                    rgExportSummary.DataSource = LoadExportSummary("EDI")
                    rgExportSummary.DataBind()
                Case "MANUAL"
                    rgExportSummary.DataSource = LoadExportSummary("MANUAL")
                    rgExportSummary.DataBind()
            End Select

            If rgExportSummary.MasterTableView.Items.Count > 0 Then
                tblData.Visible = True
            Else
                tblData.Visible = False
                Me.RadAjaxManager1.ResponseScripts.Add("window.alert('ไม่พบรายการตามเงื่อนไขที่ทำการค้นหา');")
            End If
        End Sub

        Function LoadExportSummary(ByVal Type As String) As DataTable
            Try
                Select Case Type
                    Case "EDI"
                        Dim ds As New DataSet
                        objConn = New SqlConnection(strConn)
                        ds = SqlHelper.ExecuteDataset(objConn, CommandType.StoredProcedure, "sp_Report_3", _
                        New SqlParameter("@FROM_DATE", FunctionUtility.DMY2YMD(rdpFromDate.SelectedDate.Value)), _
                        New SqlParameter("@TO_DATE", FunctionUtility.DMY2YMD(rdpToDate.SelectedDate.Value.AddDays(1))), _
                        New SqlParameter("@FORM_TYPE", CommonUtility.Get_StringValue(rcbFormType.SelectedValue)), _
                        New SqlParameter("@COUNTRY_CODE", CommonUtility.Get_StringValue(rcbCountries.SelectedValue)), _
                        New SqlParameter("@TARIFF_CODE", CommonUtility.Get_StringValue(txtTARIFF_CODE.Value)))

                        Return ds.Tables(0)
                    Case "MANUAL"
                        Dim ds As New DataSet
                        objConn = New SqlConnection(strConn)
                        ds = SqlHelper.ExecuteDataset(objConn, CommandType.StoredProcedure, "sp_report_3_manual", _
                        New SqlParameter("@FROM_DATE", FunctionUtility.DMY2YMD(rdpFromDate.SelectedDate.Value)), _
                        New SqlParameter("@TO_DATE", FunctionUtility.DMY2YMD(rdpToDate.SelectedDate.Value.AddDays(1))), _
                        New SqlParameter("@FORM_TYPE", CommonUtility.Get_StringValue(rcbFormType.SelectedValue)), _
                        New SqlParameter("@COUNTRY_CODE", CommonUtility.Get_StringValue(rcbCountries.SelectedValue)), _
                        New SqlParameter("@TARIFF_CODE", CommonUtility.Get_StringValue(txtTARIFF_CODE.Value)))

                        Return ds.Tables(0)
                End Select
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End Function

        Private Sub rgExportSummary_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles rgExportSummary.DataBound
            objConn.Close()

            If rgExportSummary.MasterTableView.Items.Count > 0 Then
                btnPrint.Visible = True
            Else
                btnPrint.Visible = False
            End If
        End Sub

        Private Sub rgExportSummary_PageIndexChanged(ByVal source As Object, ByVal e As Telerik.Web.UI.GridPageChangedEventArgs) Handles rgExportSummary.PageIndexChanged
            rgExportSummary.CurrentPageIndex = e.NewPageIndex
            Select Case rblType.SelectedValue
                Case "EDI"
                    rgExportSummary.DataSource = LoadExportSummary("EDI")
                    rgExportSummary.DataBind()
                Case "MANUAL"
                    rgExportSummary.DataSource = LoadExportSummary("MANUAL")
                    rgExportSummary.DataBind()
            End Select
        End Sub

        Protected Sub btnExportExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExportExcel.Click
            Select Case rblType.SelectedValue
                Case "EDI"
                    Call ExportSummaryExcel("EDI")
                Case "MANUAL"
                    Call ExportSummaryExcel("MANUAL")
            End Select
        End Sub

        Sub ExportSummaryExcel(ByVal Type As String)
            Try
                Dim ds As New DataSet
                objConn = New SqlConnection(strConn)

                Select Case Type
                    Case "EDI"
                        ds = SqlHelper.ExecuteDataset(objConn, CommandType.StoredProcedure, "sp_Report_3_Export", _
                        New SqlParameter("@FROM_DATE", FunctionUtility.DMY2YMD(rdpFromDate.SelectedDate.Value)), _
                        New SqlParameter("@TO_DATE", FunctionUtility.DMY2YMD(rdpToDate.SelectedDate.Value.AddDays(1))), _
                        New SqlParameter("@FORM_TYPE", CommonUtility.Get_StringValue(rcbFormType.SelectedValue)), _
                        New SqlParameter("@COUNTRY_CODE", CommonUtility.Get_StringValue(rcbCountries.SelectedValue)), _
                        New SqlParameter("@TARIFF_CODE", CommonUtility.Get_StringValue(txtTARIFF_CODE.Value)))

                    Case "MANUAL"
                        ds = SqlHelper.ExecuteDataset(objConn, CommandType.StoredProcedure, "sp_report_3_manual_export", _
                        New SqlParameter("@FROM_DATE", FunctionUtility.DMY2YMD(rdpFromDate.SelectedDate.Value)), _
                        New SqlParameter("@TO_DATE", FunctionUtility.DMY2YMD(rdpToDate.SelectedDate.Value.AddDays(1))), _
                        New SqlParameter("@FORM_TYPE", CommonUtility.Get_StringValue(rcbFormType.SelectedValue)), _
                        New SqlParameter("@COUNTRY_CODE", CommonUtility.Get_StringValue(rcbCountries.SelectedValue)), _
                        New SqlParameter("@TARIFF_CODE", CommonUtility.Get_StringValue(txtTARIFF_CODE.Value)))

                End Select

                If ds.Tables(0).Rows.Count > 0 Then
                    Dim grd1 As New GridView()
                    grd1.DataSource = ds.Tables(0)
                    grd1.DataBind()

                    Response.ContentType = "application/vnd.ms-excel"

                    Response.AddHeader("Content-Disposition", "attachment;filename=MyExportFile.xls")
                    Response.Charset = ""

                    Response.ContentEncoding = Encoding.Unicode
                    Response.BinaryWrite(Encoding.Unicode.GetPreamble())

                    Me.EnableViewState = False
                    Dim tw As New System.IO.StringWriter()
                    Dim hw As New System.Web.UI.HtmlTextWriter(tw)
                    grd1.RenderControl(hw)
                    Response.Write(tw.ToString())
                    Response.End()
                Else
                    Me.RadAjaxManager1.ResponseScripts.Add("window.alert('ไม่พบข้อมูลสถิติการส่งออกตามเงื่อนไขที่ทำการค้นหา !!!');")
                End If
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End Sub

        Private Sub LoadFormType()
            Try
                Dim strCommand As String
                Dim ds As New DataSet
                strCommand = "select * from FORM_TYPE where FORM_TYPE <> 'ALL'"
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

        Private Sub LoadCountry()
            Try
                Dim strCommand As String
                Dim ds As New DataSet
                strCommand = "select * from vCOUNTRY order by COUNTRY_NAME"
                ds = SqlHelper.ExecuteDataset(strConn, CommandType.Text, strCommand)
                If ds.Tables(0).Rows.Count > 0 Then
                    rcbCountries.DataSource = ds.Tables(0)
                    rcbCountries.DataTextField = "country_name"
                    rcbCountries.DataValueField = "country_code"
                    rcbCountries.DataBind()

                    rcbCountries.Items.Insert(0, New RadComboBoxItem("--- ALL COUNTRY ---", "%"))
                    rcbCountries.SelectedValue = "%"
                End If
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End Sub
    End Class

End Namespace
