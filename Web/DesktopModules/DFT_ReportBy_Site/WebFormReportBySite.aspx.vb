Imports DotNetNuke
Imports DotNetNuke.Services.Exceptions
Imports DotNetNuke.Services.Localization

Imports Microsoft.VisualBasic
Imports Microsoft.ApplicationBlocks.Data
Imports System.Data.SqlClient
Imports System.Data
Imports System.Text
Imports DataDynamics.ActiveReports.Export.Pdf
Imports DataDynamics.ActiveReports.Viewer.Viewer
Imports DFT.Dotnetnuke.ClassLibrary

Imports System.IO
Imports DataDynamics.ActiveReports.Export.Xls
Partial Public Class WebFormReportBySite
    Inherits System.Web.UI.Page
    Dim strEDIConn As String = ConfigurationManager.ConnectionStrings("ediConReport").ConnectionString
    Dim objConn As SqlConnection = Nothing
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If Not Page.IsPostBack Then

        Session("F1") = Request.QueryString("SendFdate")
        Session("F2") = Request.QueryString("sendTdate")
        Session("R1") = Request.QueryString("sendRole")
        call_report()
        'End If
    End Sub

    Sub call_report()
        'If LoadDataForm_DS(Request.QueryString("SendFdate"), Request.QueryString("sendTdate"), Request.QueryString("sendRole")).Tables(0).Rows.Count > 0 Then
        If LoadDataForm_DS(Session("F1"), Session("F2"), Session("R1")).Tables(0).Rows.Count > 0 Then
            Dim rpt As New ReportBy_Site
            Try
                rpt.DataSource = LoadDataForm_DS(Session("F1"), Session("F2"), Session("R1")).Tables(0)
                rpt.Run(False)
            Catch eRunReport As DataDynamics.ActiveReports.ReportException
                Response.Clear()
                Response.Write("<h1>Error running report:</h1>")
                Response.Write(eRunReport.ToString())
                Return
            End Try
            Response.ContentType = "application/pdf"

            Response.AddHeader("content-disposition", "inline; filename=ReportBy_Site.PDF")

            Dim pdf As New PdfExport
            Dim memStream As New System.IO.MemoryStream
            pdf.Export(rpt.Document, memStream)
            Response.BinaryWrite(memStream.ToArray())
            Response.End()

        Else
            lblError.Text = "ไม่พบรายงาน"
        End If
    End Sub

#Region "function data"
    Function LoadDataForm_DS(ByVal sFromDate As String, ByVal sTodate As String, ByVal sRoleID As String) As DataSet
        Try
            Dim ds As New DataSet
            objConn = New SqlConnection(strEDIConn)
            ds = SqlHelper.ExecuteDataset(objConn, CommandType.StoredProcedure, "vi_getreportReport_By_Site_NewDS", _
            New SqlParameter("@FORM_date", FunctionUtility.DMY2YMD(sFromDate)), _
            New SqlParameter("@FORM_todate", FunctionUtility.DMY2YMD(sTodate)), _
            New SqlParameter("@site_id", sRoleID))
            Return ds
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Function
    
#End Region

End Class