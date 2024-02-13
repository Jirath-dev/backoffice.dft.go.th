Imports DFT.Dotnetnuke.ClassLibrary
Imports Microsoft.ApplicationBlocks.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI

Partial Public Class frmCancelInvoice
    Inherits System.Web.UI.Page
    Dim strEDIConn As String = ConfigurationManager.ConnectionStrings("OriginConnection").ConnectionString

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            lblCompany_Taxno.Text = CommonUtility.Get_StringValue(Session("ssCompany_Taxno"))
            lblInvoiceNo.Text = Request.QueryString("invoiceno")
            lblInvoiceYear.Text = Request.QueryString("year")
            lblFType.Text = CommonUtility.Get_StringValue(Session("ssSearchType"))

            btnOK.Focus()
        End If
    End Sub

    Protected Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        Try
            'sp_common_form_edi_cancelInvoice_NewDS '3191002037','  NACP08FLK0657','2008',0
            Dim ds As New DataSet
            ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, "sp_common_form_edi_cancelInvoice_NewDS", _
            New SqlParameter("@COMPANY_TAXNO", CommonUtility.Get_StringValue(lblCompany_Taxno.Text)), _
            New SqlParameter("@INVOICE_NO", CommonUtility.Get_StringValue(lblInvoiceNo.Text)), _
            New SqlParameter("@EDI_YEAR", CommonUtility.Get_StringValue(lblInvoiceYear.Text)), _
            New SqlParameter("@FTYPE", CommonUtility.Get_Int32(lblFType.Text)))

            If ds.Tables(0).Rows.Count > 0 Then
                If CommonUtility.Get_StringValue(ds.Tables(0).Rows(0).Item("retSTATUS")) = "0" Then
                    ClientScript.RegisterStartupScript(Page.GetType(), "mykey", "CloseAndRebind();", True)
                Else
                    Me.RadAjaxManager1.ResponseScripts.Add("window.alert('เกิดข้อผิดพลาด ไม่สามารถทำการลบ Invoice หมายเลข " & lblInvoiceNo.Text & " !!!');")
                End If
            Else
                Me.RadAjaxManager1.ResponseScripts.Add("window.alert('เกิดข้อผิดพลาด ไม่สามารถทำการลบ Invoice หมายเลข " & lblInvoiceNo.Text & " !!!');")
            End If

        Catch ex As Exception
            Me.RadAjaxManager1.ResponseScripts.Add("window.alert('" & ex.Message & " !!!');")
        End Try
    End Sub

    Protected Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        ClientScript.RegisterStartupScript(Page.GetType(), "mykey", "CancelEdit();", True)
    End Sub
End Class