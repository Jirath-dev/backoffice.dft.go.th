Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports DFT.Dotnetnuke.ClassLibrary

Partial Public Class frmF04_Report_06
    Inherits System.Web.UI.Page
    Dim strConn As String = ConfigurationManager.ConnectionStrings("OriginConnection").ConnectionString
    Friend WithEvents PdfExport1 As DataDynamics.ActiveReports.Export.Pdf.PdfExport

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim ds As New DataSet
            Dim strCommand As String
            strCommand = "SELECT A.reference_code2, A.company_name, A.approve_date, " & _
                         "(SELECT form_name FROM dbo.form_type WHERE (form_type = A.form_type)) AS form_name,  " & _
                         "B.invh_run_auto, B.alert_date, B.alert_user, B.alert_site, B.remark_docatt, B.date_edit, B.user_edit,  " & _
                         "B.site_edit, B.date_finish, B.user_finish, B.site_finish " & _
                         "FROM dbo.form_header_edi AS A INNER JOIN " & _
                         "dbo.edit_doc_att AS B ON A.invh_run_auto = B.invh_run_auto " & _
                         "WHERE (A.edi_status_id = 'A') AND (A.Rep_status = 'W') AND (B.site_edit = '" & Request.QueryString("SITEID") & "') " & _
                         "AND (CONVERT(varchar(8), B.date_edit, 112) BETWEEN " & Request.QueryString("FROM") & "" & _
                         "AND " & Request.QueryString("TO") & ")"
            ds = SqlHelper.ExecuteDataset(strConn, CommandType.Text, strCommand)

            If ds.Tables(0).Rows.Count > 0 Then
                Dim m_stream As New System.IO.MemoryStream()
                Dim rpt As New rptF04_Report_06
                rpt.DataSource = ds.Tables(0)
                rpt.Run()
                If Me.PdfExport1 Is Nothing Then
                    Me.PdfExport1 = New DataDynamics.ActiveReports.Export.Pdf.PdfExport
                End If
                Me.PdfExport1.Export(rpt.Document, m_stream)
                m_stream.Position = 0
                Response.ContentType = "application/pdf"
                Response.AddHeader("content-disposition", "inline; filename=MyExport.pdf")
                Response.BinaryWrite(m_stream.ToArray())
                Response.End()

            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub InitializeComponent()
        Me.PdfExport1 = New DataDynamics.ActiveReports.Export.Pdf.PdfExport

    End Sub
End Class