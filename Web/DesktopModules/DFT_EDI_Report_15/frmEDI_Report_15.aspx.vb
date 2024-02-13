Imports System.Reflection

Imports DotNetNuke
Imports DotNetNuke.Services.Exceptions
Imports DotNetNuke.Services.Localization

Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports DFT.Dotnetnuke.ClassLibrary

Imports DataDynamics.ActiveReports.Export.Pdf

Partial Public Class frmEDI_Report_15
    Inherits System.Web.UI.Page
    Friend WithEvents PdfExport1 As DataDynamics.ActiveReports.Export.Pdf.PdfExport
    Dim strConn As String = ConfigurationManager.ConnectionStrings("OriginConnection").ConnectionString
    Dim fromdate As String = ""
    Dim todate As String = ""
    Dim siteId As String = ""
    Dim sentby As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        fromdate = Request.QueryString("FROM_DATE")
        todate = Request.QueryString("TO_DATE")
        siteId = Request.QueryString("SITE_ID")
        sentby = Request.QueryString("SentBy")
        If Not Page.IsPostBack Then
            Try

                Dim ds As New DataSet
                ds = rgReportListItems()

                If ds.Tables(0).Rows.Count > 0 Then
                    Dim StartDate, EndDate As String
                    Dim date1, date2 As String
                    Dim month1, month2 As String
                    Dim year1, year2 As String

                    Dim m_stream As New System.IO.MemoryStream()
                    Dim rpt As New rptEDI_Report_15

                    rpt.DataSource = ds.Tables(0)

                    Select Case sentby
                        Case 0
                            rpt.Label1.Text = "รายงานการออกหนังสือรับรองถิ่นกำเนิดสินค้า  ระบบ EDI"
                        Case 1
                            rpt.Label1.Text = "รายงานการออกหนังสือรับรองถิ่นกำเนิดสินค้า  ระบบ DS"
                        Case 2
                            rpt.Label1.Text = "รายงานการออกหนังสือรับรองถิ่นกำเนิดสินค้า  ระบบ XML"
                        Case Else
                            rpt.Label1.Text = "รายงานการออกหนังสือรับรองถิ่นกำเนิดสินค้า  (ทั้งหมด)"

                    End Select
                    Select Case siteId
                        Case -1
                            rpt.Label2.Text = "หน่วยงาน : ทั้งหมด"
                        Case Else
                            rpt.Label2.Text = "หน่วยงาน :" & Sitename(siteId)
                    End Select


                    StartDate = fromdate
                    date1 = StartDate.Substring(6, 2)
                    month1 = StartDate.Substring(4, 2)
                    year1 = StartDate.Substring(0, 4)

                    EndDate = todate
                    date2 = CStr(CInt(EndDate.Substring(6, 2)))
                    month2 = EndDate.Substring(4, 2)
                    year2 = EndDate.Substring(0, 4)

                    rpt.lblDateLenght.Text = date1 & " / " & month1 & " / " & year1 & "  ถึงวันที่ : " & date2 & "/" & month2 & "/" & year2

                    rpt.Run(False)

                    Response.ContentType = "application/pdf"
                    Response.AddHeader("content-disposition", "inline; filename=MyCardForm1.PDF")
                    Dim pdf As New PdfExport
                    Dim memStream As New System.IO.MemoryStream
                    pdf.Export(rpt.Document, memStream)
                    Response.BinaryWrite(memStream.ToArray())
                    Response.End()
                End If
            Catch eRunReport As DataDynamics.ActiveReports.ReportException
                Response.Clear()
                Response.Write("<h1>Error running report:</h1>")
                Response.Write(eRunReport.ToString())
                Return
            End Try
        End If
    End Sub
    Private Function rgReportListItems() As DataSet
        Dim cmd As String = "sp_report_EDI_DS_15_NewDS"
        Dim ds As New DataSet
        Try
            ds = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, cmd,
                New SqlParameter("@FROM_DATE", fromdate),
                New SqlParameter("@TO_DATE", todate),
                New SqlParameter("@SENT_BY", sentby),
                New SqlParameter("@SITE_ID", siteId))

        Catch ex As Exception

        End Try
        Return ds
    End Function
    Private Function Sitename(site_id As String) As String
        Dim result As String = ""
        Dim cmd As String = "SELECT site_name FROM site_plus WHERE  (active_status = 'Y') and (site_id= @site) ORDER BY site_name Asc"
        Dim ds As New DataSet
        Try
            ds = SqlHelper.ExecuteDataset(strConn, CommandType.Text, cmd, New SqlParameter("@site", site_id))

            If ds.Tables(0).Rows.Count > 0 Then
                result = ds.Tables(0).Rows(0).Item("site_name")
            End If
        Catch ex As Exception

        End Try
        Return result
    End Function
    Private Sub InitializeComponent()
        Me.PdfExport1 = New DataDynamics.ActiveReports.Export.Pdf.PdfExport

    End Sub
End Class