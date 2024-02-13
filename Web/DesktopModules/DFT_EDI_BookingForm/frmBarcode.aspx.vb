Imports System.Data.SqlClient
Imports DFT.Dotnetnuke.ClassLibrary
Imports Microsoft.ApplicationBlocks.Data
Imports DataDynamics.ActiveReports.Export.Pdf

Partial Public Class frmBarcode
    Inherits System.Web.UI.Page
    Dim strEDIConn As String = ConfigurationManager.ConnectionStrings("OriginConnection").ConnectionString

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If Request.QueryString("Number") <> "" Then
                Dim ds As New DataSet
                ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, "vi_select_form", _
                New SqlParameter("@TCat", 5), _
                New SqlParameter("@form_type", CommonUtility.Get_StringValue(Request.QueryString("Number"))))

                If ds.Tables(0).Rows.Count > 0 Then
                    Dim rpt As New rptBarcode
                    Try
                        rpt.DataSource = ds.Tables(0)
                        rpt.Run(False)
                    Catch eRunReport As DataDynamics.ActiveReports.ReportException
                        Response.Clear()
                        Response.Write("<h1>Error running report:</h1>")
                        Response.Write(eRunReport.ToString())
                        Return
                    End Try
                    Response.ContentType = "application/pdf"

                    Response.AddHeader("content-disposition", "inline; filename=MyCardForm1.PDF")

                    Dim pdf As New PdfExport
                    Dim memStream As New System.IO.MemoryStream
                    pdf.Export(rpt.Document, memStream)
                    Response.BinaryWrite(memStream.ToArray())
                    Response.End()
                End If

            End If
        End If
    End Sub

End Class