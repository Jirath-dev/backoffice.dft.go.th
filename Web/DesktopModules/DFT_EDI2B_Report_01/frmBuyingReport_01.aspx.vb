Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports DFT.Dotnetnuke.ClassLibrary

Partial Public Class frmBuyingReport_01
    Inherits System.Web.UI.Page
    Friend WithEvents PdfExport1 As DataDynamics.ActiveReports.Export.Pdf.PdfExport
    Dim strTradingConn As String = ConfigurationManager.ConnectionStrings("TradingConnection").ConnectionString

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If Request.QueryString("FROM_DATE") <> "" And Request.QueryString("TO_DATE") <> "" Then
                Try
                    Dim ds As New DataSet
                    ds = SqlHelper.ExecuteDataset(strTradingConn, CommandType.StoredProcedure, "pl_Preprint_report1_FL3_NewDS", _
                    New SqlParameter("@site_id", CommonUtility.Get_StringValue(Session("ssRoleName"))), _
                    New SqlParameter("@active_status", "Y"), _
                    New SqlParameter("@FROM_DATE", Request.QueryString("FROM_DATE")), _
                    New SqlParameter("@TO_DATE", Request.QueryString("TO_DATE")), _
                    New SqlParameter("@type_report", 0))

                    If ds.Tables(0).Rows.Count > 0 Then
                        Dim FROMDATE, TODATE As String
                        Dim date1, date2 As String
                        Dim month1, month2 As String
                        Dim year1, year2 As String

                        Dim m_stream As New System.IO.MemoryStream()
                        Dim rpt As New rptB_Report_01()
                        rpt.DataSource = ds.Tables(0)
                        FROMDATE = Request.QueryString("FROM_DATE")
                        date1 = FROMDATE.Substring(6, 2)
                        month1 = FROMDATE.Substring(4, 2)
                        year1 = FROMDATE.Substring(0, 4)

                        TODATE = Request.QueryString("TO_DATE")
                        date2 = TODATE.Substring(6, 2)
                        month2 = TODATE.Substring(4, 2)
                        year2 = TODATE.Substring(0, 4)

                        rpt.lblFromDate.Text = date1 & "/" & month1 & "/" & year1
                        rpt.lblToDate.Text = date2 & "/" & month2 & "/" & year2
                        rpt.lblPrintDate.Text = Convert.ToDateTime(Now).ToString("G", New System.Globalization.CultureInfo("en-GB"))
                        rpt.lblPrintTime.Text = Convert.ToDateTime(Now).ToString("HH:mm:ss")

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
        End If
    End Sub

    Private Sub InitializeComponent()
        Me.PdfExport1 = New DataDynamics.ActiveReports.Export.Pdf.PdfExport

    End Sub
End Class