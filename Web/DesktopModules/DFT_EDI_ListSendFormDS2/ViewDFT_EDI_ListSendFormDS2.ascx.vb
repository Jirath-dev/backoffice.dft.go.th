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

Namespace NTI.Modules.DFT_EDI_ListSendFormDS2

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' The ViewDFT_EDI_ListSendFormDS2 class displays the content
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Partial Class ViewDFT_EDI_ListSendFormDS2
        Inherits Entities.Modules.PortalModuleBase

        Dim strEDIConn As String = ConfigurationManager.ConnectionStrings("OriginConnection").ConnectionString

        Private Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
            LoadFormTypeForSearching()
            LoadSite()
        End Sub


        Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            If Not Page.IsPostBack Then
                rdpFromDate.SelectedDate = Now.Month - 1 & "/01/" & Now.Year
                rdpToDate.SelectedDate = Now.Month & "/" & Date.DaysInMonth(Now.Year, Now.Month) & "/" & Now.Year

                rgRequestForm.Visible = False

                dropFormType.SelectedValue = "ALL"
            End If

            LoadData()

        End Sub

        Private Sub LoadFormTypeForSearching()
            Try
                Dim ds As New DataSet
                Dim strCommand As String
                strCommand = "SELECT form_type AS CODE, form_name AS DESCRIPTION " & _
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

        Private Sub LoadSite()
            Try
                Dim ds As New DataSet
                Dim strCommand As String
                strCommand = "SELECT * FROM site_plus " & _
                             "WHERE (active_status = 'Y') AND (type_site = 'A') AND (ds_status='Y') " & _
                             "ORDER BY site_code"
                ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.Text, strCommand)
                If ds.Tables(0).Rows.Count > 0 Then
                    ddlSite.DataSource = ds.Tables(0)
                    ddlSite.DataTextField = "site_name"
                    ddlSite.DataValueField = "site_id"
                    ddlSite.DataBind()

                    ddlSite.Items.Insert(0, New RadComboBoxItem("---ทั้งหมด---", "ALL"))
                End If
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End Sub

        Protected Sub LoadData()
            rgRequestForm.DataSource = LoadRequestFormList()
            rgRequestForm.DataBind()
            rgRequestForm.Visible = True
        End Sub

        Function LoadRequestFormList() As DataTable
            Try
                Dim ds As New DataSet
                ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, "vi_form_edi_getListFormSend_NewDS2",
                New SqlParameter("@FORM_TYPE", CommonUtility.Get_StringValue(dropFormType.SelectedValue)),
                New SqlParameter("@FROM_DATE", FunctionUtility.DMY2YMD(rdpFromDate.SelectedDate.Value)),
                New SqlParameter("@TO_DATE", FunctionUtility.DMY2YMD(rdpToDate.SelectedDate.Value)),
                New SqlParameter("@DISPLAY_FLAG", "0"),
                New SqlParameter("@REF_NO", txtRefNo.Text),
                New SqlParameter("@INVOICE_NO", txtInvoiceNo.Text),
                New SqlParameter("@SENTBY", CommonUtility.Get_StringValue(ddlSentBy.SelectedValue)),
                New SqlParameter("@STATUS", CommonUtility.Get_StringValue(ddlStatus.SelectedValue)),
                New SqlParameter("@SITE_ID", CommonUtility.Get_StringValue(ddlSite.SelectedValue)))

                Return ds.Tables(0)
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End Function

        Protected Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
            '<!-- DS2 edited -->
            LoadData()
        End Sub

        Private Sub rgRequestForm_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgRequestForm.ItemDataBound
            If TypeOf e.Item Is GridDataItem Then
                Dim viewLink As HyperLink = DirectCast(e.Item.FindControl("ViewLink"), HyperLink)

                viewLink.Attributes("href") = "#"
                If e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("form_type").ToString().ToUpper() = "FORM44" Or e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("form_type").ToString().ToUpper() = "FORM441" Then
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
                    e.Item.Cells(10).Text = "<a onmouseover=""this.style.cursor='hand';"" onClick=""javascript:return ShowError('" & e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("invh_run_auto") & "');"" target=""_blank"" style=""color:Red;"">ไม่ผ่านการตรวจสอบจากเจ้าหน้าที่</a>"
                ElseIf e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("edi_status_id").ToString.ToUpper() = "R" Then
                    e.Item.ForeColor = Drawing.Color.Red
                    e.Item.Cells(10).Text = "<a onmouseover=""this.style.cursor='hand';"" onClick=""javascript:return ShowError('" & e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("invh_run_auto") & "');"" target=""_blank"" style=""color:Red;"">ไม่ผ่านการตรวจสอบจากคอมพิวเตอร์</a>"
                ElseIf e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("edi_status_id").ToString.ToUpper() = "C" Then
                    e.Item.ForeColor = Drawing.Color.Red
                    e.Item.Cells(10).Text = "<a onmouseover=""this.style.cursor='hand';"" onClick=""javascript:return ShowError('" & e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("invh_run_auto") & "');"" target=""_blank"" style=""color:Red;"">ยกเลิก</a>"
                End If

            End If
        End Sub

    End Class

End Namespace
