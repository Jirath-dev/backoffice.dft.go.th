Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports DFT.Dotnetnuke.ClassLibrary

Partial Public Class frmPrintReceipt
    Inherits System.Web.UI.Page
    Dim strEDIConn As String = ConfigurationManager.ConnectionStrings("OriginConnection").ConnectionString

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If Request.QueryString("bill_no") <> "" And Request.QueryString("site") <> "" Then
                txtBill_no.Text = Request.QueryString("bill_no")
                txtSite_ID.Text = Request.QueryString("site")

                Dim dr As SqlDataReader
                dr = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "sp_get_receipt_new", _
                New SqlParameter("@B_NO", txtBill_no.Text), _
                New SqlParameter("@SITE_ID", txtSite_ID.Text))

                Dim rpt = New receipt_rpt
                Me.WebViewer1.ViewerType = DataDynamics.ActiveReports.Web.ViewerType.HtmlViewer

                WebViewer1.Report = rpt
                WebViewer1.Height = 600

                WebViewer1.Report.DataSource = dr
                Page.SetFocus(Page)
                WebViewer1.Focus()
            End If
        End If
    End Sub

End Class