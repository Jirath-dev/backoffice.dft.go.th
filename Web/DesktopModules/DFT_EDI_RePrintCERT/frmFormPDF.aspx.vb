Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports DFT.Dotnetnuke.ClassLibrary
Imports System.IO
Imports ReportPrintClass

Partial Public Class frmFormPDF
    Inherits System.Web.UI.Page
    Dim strEDIConn As String = ConfigurationManager.ConnectionStrings("OriginConnection").ConnectionString
    Friend WithEvents PdfExport1 As DataDynamics.ActiveReports.Export.Pdf.PdfExport

    Private INVH_RUN_AUTO As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Request.QueryString("INVH_RUN_AUTO") Is Nothing Then
            INVH_RUN_AUTO = Request.QueryString("INVH_RUN_AUTO")
        End If

        If Not Page.IsPostBack Then
            Try
                Dim cmd As String = "select h.reference_code2,year(h.approve_date) as approve_year,upper(h.form_type) form_type from form_header_edi h where h.invh_run_auto=@invh_run_auto"

                Dim ds As New DataSet
                ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.Text, cmd, New SqlParameter("@invh_run_auto", INVH_RUN_AUTO))

                'If ds.Tables(0).Rows.Count > 0 Then
                '    PDF_FRAME.Attributes("src") = String.Format("/CertificateCopy/{0}/{1}/{2}.pdf?t={3}", ds.Tables(0).Rows(0).Item("approve_year"), ds.Tables(0).Rows(0).Item("form_type"), ds.Tables(0).Rows(0).Item("reference_code2"), Date.Now.ToString("ddMMyyyymmss"))
                'End If


            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End If
    End Sub


End Class