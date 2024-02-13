Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports DFT.Dotnetnuke.ClassLibrary

Partial Public Class frmF04_Report_09
    Inherits System.Web.UI.Page
    Friend WithEvents PdfExport1 As DataDynamics.ActiveReports.Export.Pdf.PdfExport
    Dim strConn As String = ConfigurationManager.ConnectionStrings("OriginConnection").ConnectionString

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Try
                Dim ds As New DataSet
                ds = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, "vi_select_RepAtt2_Summary_NewDS", _
                New SqlParameter("@FROM_DATE", CommonUtility.Get_StringValue(Request.QueryString("FROM_DATE"))), _
                New SqlParameter("@TO_DATE", CommonUtility.Get_StringValue(Request.QueryString("TO_DATE"))), _
                New SqlParameter("@SITE_ID", CommonUtility.Get_StringValue(Request.QueryString("SITE_ID"))))

                If ds.Tables(0).Rows.Count > 0 Then
                    Dim StartDate, EndDate As String
                    Dim date1, date2 As String
                    Dim month1, month2 As String
                    Dim year1, year2 As String

                    Dim m_stream As New System.IO.MemoryStream()
                    Dim rpt As New rptF04_Report_09
                    rpt.DataSource = ds.Tables(0)

                    StartDate = Request.QueryString("FROM_DATE")
                    date1 = StartDate.Substring(6, 2)
                    month1 = StartDate.Substring(4, 2)
                    year1 = StartDate.Substring(0, 4)

                    EndDate = Request.QueryString("TO_DATE")
                    date2 = EndDate.Substring(6, 2)
                    month2 = EndDate.Substring(4, 2)
                    year2 = EndDate.Substring(0, 4)

                    rpt.lblSite_ID.Text = "สถานที่ : " & Request.QueryString("SITE_ID")
                    rpt.txtFrom.Text = "ตั้งแต่วันที่ : " & date1 & "/" & month1 & "/" & year1 & "  ถึงวันที่ : " & date2 & "/" & month2 & "/" & year2
                    rpt.txtPrintDate.Text = "วันที่พิมพ์ " & Convert.ToDateTime(Now).ToString("dd/MM/yyyy")

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
        End If
    End Sub

    Private Sub InitializeComponent()
        Me.PdfExport1 = New DataDynamics.ActiveReports.Export.Pdf.PdfExport

    End Sub
End Class