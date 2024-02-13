Imports DFT.Dotnetnuke.ClassLibrary
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports Telerik.Web.UI
Imports System.IO

Partial Public Class ListForm
    Inherits Entities.Modules.PortalModuleBase

    Dim strConn As String = ConfigurationManager.ConnectionStrings("Ds2OriginConnection").ConnectionString

    Dim reader As SqlDataReader = Nothing
    Dim objConn As SqlConnection = New SqlConnection

    Dim company_taxno As String
    Dim invh_run_auto As String
    Dim edi_status As String
    Dim form_type As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            LoadFormTypeForSearching()
            LoadSite()

            rdpFromDate.SelectedDate = DateAdd(DateInterval.Month, -1, Now)
            rdpToDate.SelectedDate = Now
        End If
    End Sub

    Private Sub LoadFormTypeForSearching()
        Try
            Dim ds As New DataSet
            Dim strCommand As String
            strCommand = "SELECT form_type AS CODE, form_name AS DESCRIPTION " & _
                         "FROM form_type ORDER BY ShowOrder"
            ds = SqlHelper.ExecuteDataset(strConn, CommandType.Text, strCommand)

            If ds.Tables(0).Rows.Count > 0 Then
                ddlFormType.DataSource = ds.Tables(0)
                ddlFormType.DataTextField = "DESCRIPTION"
                ddlFormType.DataValueField = "CODE"
                ddlFormType.DataBind()
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub LoadSite()
        Try
            Dim ds As New DataSet
            Dim strCommand As String
            strCommand = "SELECT * FROM site_plus " & _
                         "WHERE (active_status = 'Y') AND (type_site = 'A') AND (ds_status='Y') " & _
                         "ORDER BY site_code"
            ds = SqlHelper.ExecuteDataset(strConn, CommandType.Text, strCommand)
            If ds.Tables(0).Rows.Count > 0 Then
                ddlSite.DataSource = ds.Tables(0)
                ddlSite.DataTextField = "site_name"
                ddlSite.DataValueField = "site_id"
                ddlSite.DataBind()

                ddlSite.Items.Insert(0, New ListItem("---ทั้งหมด---", "ALL"))
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Function LoadRequestFormList() As SqlDataReader
        Try
            reader = SqlHelper.ExecuteReader(strConn, CommandType.StoredProcedure, "vi_form_edi_getListFormSend_NewDS2", _
            New SqlParameter("@FORM_TYPE", CommonUtility.Get_StringValue(ddlFormType.SelectedValue)), _
            New SqlParameter("@FROM_DATE", FunctionUtility.DMY2YMD(rdpFromDate.SelectedDate.Value)), _
            New SqlParameter("@TO_DATE", FunctionUtility.DMY2YMD(rdpToDate.SelectedDate.Value)), _
            New SqlParameter("@DISPLAY_FLAG", "0"), _
            New SqlParameter("@REF_NO", CommonUtility.Get_StringValue(txtRefNo.Text)), _
            New SqlParameter("@INVOICE_NO", CommonUtility.Get_StringValue(txtInvoiceNo.Text)), _
            New SqlParameter("@SENTBY", CommonUtility.Get_StringValue(ddlSentBy.SelectedValue)), _
            New SqlParameter("@STATUS", CommonUtility.Get_StringValue(ddlStatus.SelectedValue)), _
            New SqlParameter("@SITE_ID", CommonUtility.Get_StringValue(ddlSite.SelectedValue)))

            Return reader
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Function

    Private Sub grdMasterData_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles grdMasterData.ItemDataBound
        If TypeOf e.Item Is GridDataItem Then

            invh_run_auto = e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("invh_run_auto")
            company_taxno = e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("company_taxno")

            Dim viewLink As HyperLink = DirectCast(e.Item.FindControl("ViewLink"), HyperLink)
            viewLink.Attributes("href") = "#"
            form_type = e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("form_type").ToString().ToUpper()

            If form_type = "FORM44" Or form_type = "FORM441" Then
                viewLink.Attributes("onclick") = [String].Format("return popup('{0}');", "http://" & DotNetNuke.Common.GetDomainName(Request) & "/DesktopModules/DFT_EDI_CheckAttachment/frmCheckForm3.aspx?mode=view&TaxNo=" + company_taxno + "&RefNo=" + invh_run_auto & "&form_type=" & form_type)
            Else
                If e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("SentBy") = "1" Then
                    viewLink.Attributes("onclick") = [String].Format("return popup('{0}');", "http://" & DotNetNuke.Common.GetDomainName(Request) & "/DesktopModules/DFT_EDI_CheckAttachment/frmCheckForm1.aspx?mode=view&TaxNo=" + company_taxno + "&RefNo=" + invh_run_auto & "&form_type=" & form_type)
                ElseIf e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("SentBy") = "2" Then
                    viewLink.Attributes("onclick") = [String].Format("return popup('{0}');", "http://" & DotNetNuke.Common.GetDomainName(Request) & "/DesktopModules/DFT_EDI_CheckAttachment/frmCheckForm2.aspx?mode=view&TaxNo=" + company_taxno + "&RefNo=" + invh_run_auto & "&form_type=" & form_type)
                End If
            End If

            edi_status = e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("edi_status_id").ToString.ToUpper()
            Dim ViewStatus As HyperLink = DirectCast(e.Item.FindControl("ViewStatus"), HyperLink)

            If edi_status = "N" Or edi_status = "R" Or edi_status = "C" Then
                ViewStatus.Attributes("href") = "javascript:return false;void(0);"
                ViewStatus.Attributes("onclick") = [String].Format("javascript:return ShowError('{0}');", invh_run_auto)

                If edi_status = "N" Then
                    e.Item.ForeColor = Drawing.Color.Red
                    ViewStatus.ForeColor = Drawing.Color.Red
                    ViewStatus.Text = "ไม่ผ่านการตรวจสอบจากเจ้าหน้าที่"
                ElseIf edi_status = "R" Then
                    e.Item.ForeColor = Drawing.Color.Orange
                    ViewStatus.ForeColor = Drawing.Color.Orange
                    ViewStatus.Text = "ไม่ผ่านการตรวจสอบจากคอมพิวเตอร์"
                ElseIf edi_status = "C" Then
                    e.Item.ForeColor = Drawing.Color.Yellow
                    ViewStatus.ForeColor = Drawing.Color.Yellow
                    ViewStatus.Text = "ยกเลิก"
                End If
            Else
                ViewStatus.Text = e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("description").ToString()
            End If


        End If
    End Sub

    Private Sub grdMasterData_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles grdMasterData.NeedDataSource
        grdMasterData.DataSource = LoadRequestFormList()
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
        'กำหนดให้แสดงหน้า 1 ทุกครั้งที่ทำการค้นหา
        grdMasterData.CurrentPageIndex = 0
        'ทำการ load ข้อมูล
        grdMasterData.DataSource = LoadRequestFormList()
        grdMasterData.DataBind()
    End Sub

    Private Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload
        Try
            If objConn.State = ConnectionState.Open Then
                reader.Close()
                objConn.Close()
            End If
        Catch ex As Exception
            '
        End Try
        
    End Sub

End Class