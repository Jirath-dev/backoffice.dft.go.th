Imports System.Reflection

Imports DotNetNuke
Imports DotNetNuke.Services.Exceptions
Imports DotNetNuke.Services.Localization

Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports DFT.Dotnetnuke.ClassLibrary

Imports DataDynamics.ActiveReports.Export.Pdf

Partial Public Class frmEDI_Report_04
    Inherits System.Web.UI.Page
    Friend WithEvents PdfExport1 As DataDynamics.ActiveReports.Export.Pdf.PdfExport
    Dim strEDIConn As String = ConfigurationManager.ConnectionStrings("OriginConnection").ConnectionString

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Try
                Dim ds As New DataSet
                If CommonUtility.Get_StringValue(Request.QueryString("TCat")) = "0" Then
                    ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, "vi_EDI_REPORT_04_NewDS", _
                    New SqlParameter("@FROM_DATE", CommonUtility.Get_StringValue(Request.QueryString("FROM_DATE"))), _
                    New SqlParameter("@TO_DATE", CommonUtility.Get_StringValue(Request.QueryString("TO_DATE"))), _
                    New SqlParameter("@SITE_ID", CommonUtility.Get_StringValue(Request.QueryString("SITE_ID"))), _
                    New SqlParameter("@TYPE", "1"))

                ElseIf CommonUtility.Get_StringValue(Request.QueryString("TCat")) = "1" Then
                    ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, "vi_EDI_REPORT_04_NewDS", _
                    New SqlParameter("@FROM_DATE", CommonUtility.Get_StringValue(Request.QueryString("FROM_DATE"))), _
                    New SqlParameter("@TO_DATE", CommonUtility.Get_StringValue(Request.QueryString("TO_DATE"))), _
                    New SqlParameter("@SITE_ID", CommonUtility.Get_StringValue(Request.QueryString("SITE_ID"))), _
                    New SqlParameter("@TYPE", "2"))
                End If

                If ds.Tables(0).Rows.Count > 0 Then
                    Dim StartDate, EndDate As String
                    Dim date1, date2 As String
                    Dim month1, month2 As String
                    Dim year1, year2 As String

                    Dim m_stream As New System.IO.MemoryStream()
                    Dim rpt As New rptEDI_Report_04

                    rpt.DataSource = ds.Tables(0)
                    StartDate = CommonUtility.Get_StringValue(Request.QueryString("FROM_DATE"))
                    date1 = StartDate.Substring(6, 2)
                    month1 = StartDate.Substring(4, 2)
                    year1 = StartDate.Substring(0, 4)

                    EndDate = CommonUtility.Get_StringValue(Request.QueryString("TO_DATE"))
                    date2 = CStr(CInt(EndDate.Substring(6, 2)))
                    month2 = EndDate.Substring(4, 2)
                    year2 = EndDate.Substring(0, 4)

                    rpt.lblDateLenght.Text = date1 & "/" & month1 & "/" & year1 & "  ถึงวันที่ : " & date2 & "/" & month2 & "/" & year2

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

    Private Sub InitializeComponent()
        Me.PdfExport1 = New DataDynamics.ActiveReports.Export.Pdf.PdfExport

    End Sub
End Class