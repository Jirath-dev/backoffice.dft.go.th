Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports DFT.Dotnetnuke.ClassLibrary

Partial Public Class frmAttachDoc_Ver1
    Inherits System.Web.UI.Page
    Friend WithEvents PdfExport1 As DataDynamics.ActiveReports.Export.Pdf.PdfExport
    Dim strConn As String = ConfigurationManager.ConnectionStrings("OriginConnection").ConnectionString

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Try
                Dim ds As New DataSet
                ds = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, "vi_select_RepAtt2_NewDS_V1", _
                New SqlParameter("@TCat1", 3), _
                New SqlParameter("@dForm", CommonUtility.Get_StringValue(Request.QueryString("FROM_DATE"))), _
                New SqlParameter("@dTo", CommonUtility.Get_StringValue(Request.QueryString("TO_DATE"))), _
                New SqlParameter("@total_day", 10), _
                New SqlParameter("@site_id", CommonUtility.Get_StringValue(Request.QueryString("SITE_ID"))))


                If ds.Tables(0).Rows.Count > 0 Then
                    Dim StartDate, EndDate As String
                    Dim date1, date2 As String
                    Dim month1, month2 As String
                    Dim year1, year2 As String

                    Dim m_stream As New System.IO.MemoryStream()
                    Dim rpt As New rptAttachDoc_Ver1()
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
                    rpt.txtPrintDate.Text = "วันที่พิมพ์ " & Convert.ToDateTime(Now).ToString("G", New System.Globalization.CultureInfo("en-GB"))
                    'rpt.lblCompanyTotal.Text = "รวมจำนวนผู้ประกอบการทั้งสิ้น  " & GetCompanyTotal(CommonUtility.Get_StringValue(Request.QueryString("From")), CommonUtility.Get_StringValue(Request.QueryString("To")), CommonUtility.Get_StringValue(Session("ssRoleName"))) & "  ราย"

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