Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports DFT.Dotnetnuke.ClassLibrary

Partial Public Class frmF04_Report_04
    Inherits System.Web.UI.Page
    Dim strConn As String = ConfigurationManager.ConnectionStrings("OriginConnection").ConnectionString
    Friend WithEvents PdfExport1 As DataDynamics.ActiveReports.Export.Pdf.PdfExport

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim ds As New DataSet
            ds = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, "vi_select_RepAtt2", _
                New SqlParameter("@TCat1", 4), _
                New SqlParameter("@dForm", "20000101"), _
                New SqlParameter("@dTo", "25001231"), _
                New SqlParameter("@total_day", 10), _
                New SqlParameter("@site_id", CommonUtility.Get_StringValue(Session("ssRoleName"))))

            If ds.Tables(0).Rows.Count > 0 Then
                Dim m_stream As New System.IO.MemoryStream()
                Dim rpt As New rptF04_Report_04()
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