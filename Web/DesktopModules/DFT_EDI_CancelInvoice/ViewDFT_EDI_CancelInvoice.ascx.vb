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

Namespace NTi.Modules.DFT_EDI_CancelInvoice
    Partial Class ViewDFT_EDI_CancelInvoice
        Inherits Entities.Modules.PortalModuleBase
        Dim strEDIConn As String = ConfigurationManager.ConnectionStrings("OriginConnection").ConnectionString

        Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            If Not Page.IsPostBack Then
                rgInvoiceList.DataSource = LoadInvoice(txtCompany_Taxno.Text.Trim(), txtInvoiceNo.Text, rblSearchType.SelectedValue)
                rgInvoiceList.DataBind()

                txtInvoiceNo.Focus()
            End If
        End Sub

        Protected Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
            txtCompany_Taxno.Text = ""
            txtCompanyName_Eng.Text = ""
            txtInvoiceNo.Text = ""

            rgInvoiceList.DataSource = LoadInvoice(txtCompany_Taxno.Text.Trim(), txtInvoiceNo.Text, rblSearchType.SelectedValue)
            rgInvoiceList.DataBind()

            txtInvoiceNo.Focus()
        End Sub

        Protected Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
            If txtCompany_Taxno.Text.Trim() <> "" Then
                rgInvoiceList.DataSource = LoadInvoice(txtCompany_Taxno.Text.Trim(), txtInvoiceNo.Text, rblSearchType.SelectedValue)
                rgInvoiceList.DataBind()
            End If

            txtInvoiceNo.Focus()
        End Sub

        Function LoadInvoice(ByVal Company_Taxno As String, ByVal InvoiceNo As String, ByVal FType As String) As DataTable
            Try
                Session("ssSearchType") = FType
                Session("ssCompany_Taxno") = Company_Taxno

                'sp_common_form_edi_getInvoiceByCompany '3191002037','',0
                Dim ds As New DataSet
                ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, "sp_common_form_edi_getInvoiceByCompany_NewDS", _
                New SqlParameter("@COMPANY_TAXNO", CommonUtility.Get_StringValue(Company_Taxno)), _
                New SqlParameter("@INVOICE_NO", CommonUtility.Get_StringValue(InvoiceNo)), _
                New SqlParameter("@FTYPE", CommonUtility.Get_Int32(FType)))

                Return ds.Tables(0)
            Catch ex As Exception

            End Try
        End Function

        Private Sub RadAjaxManager1_AjaxRequest(ByVal sender As Object, ByVal e As Telerik.Web.UI.AjaxRequestEventArgs) Handles RadAjaxManager1.AjaxRequest
            rgInvoiceList.DataSource = LoadInvoice(txtCompany_Taxno.Text.Trim(), txtInvoiceNo.Text, rblSearchType.SelectedValue)
            rgInvoiceList.DataBind()
        End Sub

        Private Sub rgInvoiceList_ItemCreated(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgInvoiceList.ItemCreated
            If TypeOf e.Item Is GridDataItem Then
                Dim cancelLink As ImageButton = DirectCast(e.Item.FindControl("btnCancelInvoice"), ImageButton)
                cancelLink.Attributes("href") = "#"
                cancelLink.Attributes("onclick") = [String].Format("return ShowDeleteForm('{0}','{1}');", e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("invoice_no"), e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("edi_year"))
            End If
        End Sub

        Private Sub rgInvoiceList_PageIndexChanged(ByVal source As Object, ByVal e As Telerik.Web.UI.GridPageChangedEventArgs) Handles rgInvoiceList.PageIndexChanged
            rgInvoiceList.CurrentPageIndex = e.NewPageIndex
            rgInvoiceList.DataSource = LoadInvoice(txtCompany_Taxno.Text.Trim(), txtInvoiceNo.Text, rblSearchType.SelectedValue)
            rgInvoiceList.DataBind()
        End Sub

        Protected Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
            Dim myDataGridItem As GridDataItem
            Dim _InvoiceNo As String = ""
            Dim _InvoiceYear As String = ""

            For Each myDataGridItem In rgInvoiceList.MasterTableView.Items
                If myDataGridItem.Selected = True Then
                    _InvoiceNo = myDataGridItem.Item("invoice_no").Text
                    _InvoiceYear = myDataGridItem.Item("edi_year").Text
                End If
            Next

            If _InvoiceNo = "" And _InvoiceYear = "" Then
                Me.RadAjaxManager1.ResponseScripts.Add("window.alert('กรุณาเลือก Invoice ที่ต้องการทำการยกเลิก !!!');")
            Else
                Me.RadAjaxManager1.ResponseScripts.Add("ShowDeleteForm('" & _InvoiceNo & "','" & _InvoiceYear & "');")
                'Page.ClientScript.RegisterStartupScript(Page.GetType(), "mykey", "return ShowDeleteForm('" & _InvoiceNo & "','" & _InvoiceYear & "');")
            End If

        End Sub
    End Class

End Namespace
