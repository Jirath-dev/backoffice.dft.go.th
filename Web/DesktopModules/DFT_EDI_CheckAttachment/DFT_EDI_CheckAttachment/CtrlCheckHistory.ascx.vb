Imports System.Web.UI
Imports System.Collections.Generic
Imports System.Reflection

Imports DotNetNuke
Imports DotNetNuke.Services.Exceptions
Imports DotNetNuke.Services.Localization

Imports DFT.Dotnetnuke.ClassLibrary
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports Telerik.Web.UI

Partial Public Class CtrlCheckHistory
    Inherits Entities.Modules.PortalModuleBase

    Dim strEDIConn As String = ConfigurationManager.ConnectionStrings("OriginConnection").ConnectionString

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        LoadFormTypeForSearching()

        If Not Page.IsPostBack Then
            rdpFromDate.SelectedDate = Now.Month & "/" & Now.Day & "/" & Now.Year 'Now.Month & "/01/" & Now.Year
            rdpToDate.SelectedDate = Now.Month & "/" & Now.Day & "/" & Now.Year

            dropFormType.SelectedValue = "ALL"
            rgRequestForm.Visible = False

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

        If Not Page.IsPostBack Then
            '<!-- DS2 edited -->
            LoadData()
            CheckSummary()
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
    Protected Sub CheckSummary()
        Dim strhtml As String = "<table class=""tb-data"" style=""font-size:10pt;"" cellpadding=""0"" cellspacing=""0""><tr><td class=""head"">ชื่อฟอร์ม</td><td class=""head"">จำนวนทั้งหมด</td><td class=""head"">ผ่าน</td><td class=""head"">ไม่ผ่าน</td></tr>"
        Try
            Dim Total As Long = 0
            Dim totalErr As Long = 0
            Dim totalOk As Long = 0

            Dim ds As New DataSet
            ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, "vi_form_edi_getForCheckAtt_Summary_NewDS", _
            New SqlParameter("@FORM_TYPE", CommonUtility.Get_StringValue(dropFormType.SelectedValue)), _
            New SqlParameter("@FROM_DATE", FunctionUtility.DMY2YMD(rdpFromDate.SelectedDate.Value)), _
            New SqlParameter("@TO_DATE", FunctionUtility.DMY2YMD(rdpToDate.SelectedDate.Value)), _
            New SqlParameter("@DISPLAY_FLAG", "0"), _
            New SqlParameter("@SITE_ID", CommonUtility.Get_StringValue(lblRoleID.Text)))

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

            lblTotalForm.Text = totalOk + totalErr
            lblOK.Text = totalOk.ToString()
            lblNO.Text = totalErr.ToString()

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    '<!-- DS2 edited -->
    Protected Sub LoadData()
        rgRequestForm.DataSource = LoadRequestFormList()
        rgRequestForm.Rebind()
        rgRequestForm.Visible = True
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        '<!-- DS2 edited -->
        rgRequestForm.CurrentPageIndex = 0
        LoadData()
        CheckSummary()

        ChangeCollaps()

    End Sub

    Public Sub ChangeCollaps()
        If txtTempCollaps.Text = 1 Then

            collapsdiv.Style.Item("display") = ""
            collaps_div_main.Style.Item("border-left") = "1px solid #cccccc"
            collaps_div_main.Style.Item("border-bottom") = "1px solid #cccccc"
            collaps_div_main.Style.Item("border-right") = "1px solid #cccccc"
            colIcon.InnerHtml = "(-)"
            ' txtTempCollaps.value = '1';
        Else
            collapsdiv.Style.Item("display") = "none"
            collaps_div_main.Style.Item("border-left") = ""
            collaps_div_main.Style.Item("border-bottom") = ""
            collaps_div_main.Style.Item("border-right") = ""
            colIcon.InnerHtml = "(+)"
        End If
    End Sub

    Function LoadRequestFormList() As DataTable
        Try
            Dim expression1 As GridGroupByExpression = GridGroupByExpression.Parse("CheckDoc_By [ผู้ตรวจ], count(invh_run_auto) Items [จำนวน] Group By CheckDoc_By")
            Me.rgRequestForm.MasterTableView.GroupByExpressions.Add(expression1)

            Dim ds As New DataSet

            Dim prm(6) As SqlClient.SqlParameter
            prm(0) = New SqlClient.SqlParameter("@FORM_TYPE", CommonUtility.Get_StringValue(dropFormType.SelectedValue))
            prm(1) = New SqlClient.SqlParameter("@FROM_DATE", FunctionUtility.DMY2YMD(rdpFromDate.SelectedDate.Value))
            prm(2) = New SqlClient.SqlParameter("@TO_DATE", FunctionUtility.DMY2YMD(rdpToDate.SelectedDate.Value))
            prm(3) = New SqlClient.SqlParameter("@DISPLAY_FLAG", "0")
            prm(4) = New SqlClient.SqlParameter("@SITE_ID", CommonUtility.Get_StringValue(lblRoleID.Text))
            prm(5) = New SqlClient.SqlParameter("@REF_NO", SqlDbType.VarChar)
            If txtRefNo.Text <> "" Then
                prm(5).Value = CommonUtility.Get_StringValue("%" & txtRefNo.Text & "%")
            Else
                prm(5).Value = "0"
            End If
            prm(6) = New SqlClient.SqlParameter("@INVOICE_NO", SqlDbType.VarChar)
            If txtInvoiceNo.Text <> "" Then
                prm(6).Value = CommonUtility.Get_StringValue("%" & txtInvoiceNo.Text & "%")
            Else
                prm(6).Value = "0"
            End If

            ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, "vi_form_edi_getForCheckAtt_History_NewDS", prm)
            Return ds.Tables(0)
        Catch ex As Exception
            Response.Write(ex.Message)
            'Return Nothing
        End Try
    End Function

    Private Sub LoadFormTypeForSearching()
        Try
            Dim ds As New DataSet
            Dim strCommand As String
            strCommand = "SELECT form_type AS CODE, form_nameUsd AS DESCRIPTION " & _
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

    Private Sub rgRequestForm_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgRequestForm.ItemDataBound
        If TypeOf e.Item Is GridDataItem Then
            Dim viewLink As HyperLink = DirectCast(e.Item.FindControl("ViewLink"), HyperLink)

            viewLink.Attributes("href") = "#"
            If e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("form_type").ToString().ToUpper() = "FORM44" _
                Or e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("form_type").ToString().ToUpper() = "FORM441" _
                Or e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("form_type").ToString().ToUpper() = "FORM441_4" _
                Or e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("form_type").ToString().ToUpper() = "FORM44_44" _
                Or e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("form_type").ToString().ToUpper() = "FORM44_02" Then
                viewLink.Attributes("onclick") = [String].Format("return popup('{0}');", "http://" & DotNetNuke.Common.GetDomainName(Request) & "/DesktopModules/DFT_EDI_CheckAttachment/frmCheckForm3.aspx?mode=view&TaxNo=" + e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("company_taxno") + "&RefNo=" + e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("invh_run_auto") & "&form_type=" & e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("form_type"))
            Else
                If e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("SentBy") = "1" Then
                    viewLink.Attributes("onclick") = [String].Format("return popup('{0}');", "http://" & DotNetNuke.Common.GetDomainName(Request) & "/DesktopModules/DFT_EDI_CheckAttachment/frmCheckForm1.aspx?mode=view&TaxNo=" + e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("company_taxno") + "&RefNo=" + e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("invh_run_auto") & "&form_type=" & e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("form_type"))
                ElseIf e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("SentBy") = "2" Then
                    viewLink.Attributes("onclick") = [String].Format("return popup('{0}');", "http://" & DotNetNuke.Common.GetDomainName(Request) & "/DesktopModules/DFT_EDI_CheckAttachment/frmCheckForm2.aspx?mode=view&TaxNo=" + e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("company_taxno") + "&RefNo=" + e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("invh_run_auto") & "&form_type=" & e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("form_type"))
                End If
            End If

            If e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("edi_status_id").ToString.ToUpper() = "N" Then
                e.Item.ForeColor = Drawing.Color.Red
                e.Item.Cells(11).Text = "<a onmouseover=""this.style.cursor='hand';"" onClick=""javascript:return ShowError('" & e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("invh_run_auto") & "');"" target=""_blank"" style=""color:Red;"">ไม่ผ่าน</a>"
            Else
                e.Item.Cells(11).Text = "ผ่าน"
            End If

            If CommonUtility.Get_StringValue(e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("SignImageID").ToString()) <> "" Then
                e.Item.Cells(13).Text = e.Item.Cells(13).Text & " +<span class='sealsign'>SEAL&SIGN</span>"
            End If

        End If
    End Sub

    Private Sub rgRequestForm_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgRequestForm.NeedDataSource
        rgRequestForm.DataSource = LoadRequestFormList()
    End Sub

    Private Sub rgRequestForm_PageIndexChanged(ByVal source As Object, ByVal e As Telerik.Web.UI.GridPageChangedEventArgs) Handles rgRequestForm.PageIndexChanged
        rgRequestForm.CurrentPageIndex = e.NewPageIndex
        rgRequestForm.DataSource = LoadRequestFormList()
        rgRequestForm.DataBind()
    End Sub

    Protected Sub ImageButton1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click
        rgRequestForm.ExportSettings.ExportOnlyData = True
        rgRequestForm.ExportSettings.IgnorePaging = True
        rgRequestForm.ExportSettings.OpenInNewWindow = True

        rgRequestForm.ExportSettings.Pdf.PageTopMargin = Unit.Pixel(10)
        rgRequestForm.ExportSettings.Pdf.PageLeftMargin = Unit.Pixel(10)
        rgRequestForm.ExportSettings.Pdf.PageRightMargin = Unit.Pixel(10)
        rgRequestForm.ExportSettings.Pdf.PageBottomMargin = Unit.Pixel(10)

        rgRequestForm.Columns(0).Visible = False
        rgRequestForm.MasterTableView.GridLines = GridLines.Both

        rgRequestForm.MasterTableView.Font.Name = "Tahoma"
        rgRequestForm.MasterTableView.Font.Size = FontUnit.Point(6)
        rgRequestForm.MasterTableView.ExportToExcel()
        ''rgRequestForm.MasterTableView.ExportToPdf()

    End Sub

    Private Sub RadAjaxManager1_AjaxRequest(ByVal sender As Object, ByVal e As Telerik.Web.UI.AjaxRequestEventArgs) Handles RadAjaxManager1.AjaxRequest
        rgRequestForm.Rebind()
    End Sub

End Class