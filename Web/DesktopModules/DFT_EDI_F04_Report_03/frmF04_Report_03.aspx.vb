Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports DFT.Dotnetnuke.ClassLibrary

Partial Public Class frmF04_Report_03
    Inherits System.Web.UI.Page
    Dim strConn As String = ConfigurationManager.ConnectionStrings("OriginConnection").ConnectionString
    Friend WithEvents PdfExport1 As DataDynamics.ActiveReports.Export.Pdf.PdfExport

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim ds As New DataSet
            ds = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, "vi_select_RepAtt2", _
            New SqlParameter("@TCat1", 3), _
            New SqlParameter("@dForm", CommonUtility.Get_StringValue(Request.QueryString("From"))), _
            New SqlParameter("@dTo", CommonUtility.Get_StringValue(Request.QueryString("To"))), _
            New SqlParameter("@total_day", 10), _
            New SqlParameter("@site_id", CommonUtility.Get_StringValue(Session("ssRoleName"))))

            If ds.Tables(0).Rows.Count > 0 Then
                Dim StartDate, EndDate As String
                Dim date1, date2 As String
                Dim month1, month2 As String
                Dim year1, year2 As String

                Dim m_stream As New System.IO.MemoryStream()
                Dim rpt As New rptF04_Report_03()
                rpt.DataSource = ds.Tables(0)

                StartDate = Request.QueryString("From")
                date1 = StartDate.Substring(6, 2)
                month1 = StartDate.Substring(4, 2)
                year1 = StartDate.Substring(0, 4)

                EndDate = Request.QueryString("To")
                date2 = EndDate.Substring(6, 2)
                month2 = EndDate.Substring(4, 2)
                year2 = EndDate.Substring(0, 4)

                rpt.txtFrom.Text = "ตั้งแต่วันที่ : " & date1 & "/" & month1 & "/" & year1 & "  ถึงวันที่ : " & date2 & "/" & month2 & "/" & year2
                rpt.lblSite_ID.Text = "สถานที่ : " & CommonUtility.Get_StringValue(Session("ssRoleName"))
                rpt.lblCompanyTotal.Text = "รวมจำนวนผู้ประกอบการทั้งสิ้น  " & GetCompanyTotal(CommonUtility.Get_StringValue(Request.QueryString("From")), CommonUtility.Get_StringValue(Request.QueryString("To")), CommonUtility.Get_StringValue(Session("ssRoleName"))) & "  ราย"

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

    Function GetCompanyTotal(ByVal _FROM As String, ByVal _TO As String, ByVal _SITE_ID As String) As String
        Try
            Dim strCommand As String
            strCommand = "SELECT DISTINCT company_name, site_id " & _
                         "FROM dbo.form_header_edi " & _
                         "WHERE (edi_status_id = 'A') AND (Rep_status = 'A' OR " & _
                         "Rep_status = 'N' OR " & _
                         "Rep_status = 'W') AND (site_id like '%" & _SITE_ID & "%') AND (CONVERT(varchar(8), approve_date, 112) > '20081001') AND (CONVERT(varchar(8), Rep_Doc_date,  " & _
                         "112) <= '" & _FROM & "') AND (CONVERT(varchar(8), Rep_Doc_date, 112) >= '" & _TO & "')"
            Dim ds As New DataSet
            ds = SqlHelper.ExecuteDataset(strConn, CommandType.Text, strCommand)
            If ds.Tables(0).Rows.Count > 0 Then
                Return CommonUtility.Get_StringValue(ds.Tables(0).Rows.Count)
            Else
                Return "0"
            End If
        Catch ex As Exception
            Return ex.Message
        End Try
    End Function

End Class