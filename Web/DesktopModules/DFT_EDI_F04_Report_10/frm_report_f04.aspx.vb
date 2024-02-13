Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Globalization
Imports System.Threading
Imports DataDynamics.ActiveReports.Export.Pdf
Imports Microsoft.ApplicationBlocks.Data

Public Class frm_report_f04
    Inherits System.Web.UI.Page
    Dim FROM_DATE As String = ""
    Dim TO_DATE As String = ""
    Dim SITE_ID As String = ""
    Dim UTYPE As String = "0"
    Dim strEDIConn As String = ConfigurationManager.ConnectionStrings("OriginConnection").ConnectionString
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        '===========set Date thai==================================
        Thread.CurrentThread.CurrentCulture = New CultureInfo("th-TH")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("th-TH")
        '=============================================

        If Not Request.QueryString("FROM_DATE") Is Nothing Then
            FROM_DATE = Request.QueryString("FROM_DATE")
        End If
        If Not Request.QueryString("TO_DATE") Is Nothing Then
            TO_DATE = Request.QueryString("TO_DATE")
        End If
        If Not Request.QueryString("SITE_ID") Is Nothing Then
            SITE_ID = Request.QueryString("SITE_ID")
        End If

        If Not Request.QueryString("UTYPE") Is Nothing Then
            UTYPE = Request.QueryString("UTYPE")
        End If

        If GetData(FROM_DATE, TO_DATE, SITE_ID, UTYPE) = False Then
            Page.ClientScript.RegisterStartupScript(Page.GetType, "nook", "javascript:alert('เกิดข้อผิดพลาด ไม่สามารถเรียกดูข้อมูลได้')", True)
        End If

    End Sub

    Private Function GetData(fromdate As String, todate As String, siteid As String, user_type As String) As Boolean
        Dim result As Boolean = False
        Try
            Dim cmd As String = "sp_get_report_f04_10_v2"
            Dim ds As New DataSet

            Dim prm(3) As SqlParameter
            prm(0) = New SqlParameter("@FROM_DATE", fromdate)
            prm(1) = New SqlParameter("@TO_DATE", todate)
            prm(2) = New SqlParameter("@SITE_ID", siteid)
            prm(3) = New SqlParameter("@USER_TYPE", user_type)
            ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, cmd, prm)
            If ds.Tables(0).Rows.Count > 0 Then
                Dim rpt As New rpt_f04_report

                rpt.PageSettings.PaperKind = Printing.PaperKind.A4
                rpt.Document.Printer.PrinterName = ""
                rpt.DataSource = ds.Tables(0)
                rpt.Run(False)

                Response.ContentType = "application/pdf"
                Response.AddHeader("content-disposition", "inline; filename=MyForm1.PDF")
                Dim pdf As New PdfExport
                Dim memStream As New System.IO.MemoryStream
                pdf.Export(rpt.Document, memStream)
                Response.BinaryWrite(memStream.ToArray())
                Response.End()

                Return True
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function
End Class