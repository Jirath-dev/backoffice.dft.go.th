Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports DFT.Dotnetnuke.ClassLibrary

Partial Public Class frmEDI_Report_121
    Inherits System.Web.UI.Page
    Friend WithEvents PdfExport1 As DataDynamics.ActiveReports.Export.Pdf.PdfExport
    Dim strConn As String = ConfigurationManager.ConnectionStrings("EDIConnection").ConnectionString

    Private ReportType As String = "edi"
    Private DSPrintOrCheck As String = "print"
    Private FROM_DATE As String = ""
    Private TO_DATE As String = ""
    Private SITE_ID As String = ""
    Private ReceiptType As String = ""
    Private request_type As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then

            If Not Request.QueryString("rTYPE") Is Nothing Then
                ReportType = CommonUtility.Get_StringValue(Request.QueryString("rTYPE"))
            End If
            If Not Request.QueryString("IsPRINT") Is Nothing Then
                DSPrintOrCheck = CommonUtility.Get_StringValue(Request.QueryString("IsPRINT"))
            End If
            If Not Request.QueryString("FROM_DATE") Is Nothing Then
                FROM_DATE = CommonUtility.Get_StringValue(Request.QueryString("FROM_DATE"))
            End If
            If Not Request.QueryString("TO_DATE") Is Nothing Then
                TO_DATE = CommonUtility.Get_StringValue(Request.QueryString("TO_DATE"))
            End If
            If Not Request.QueryString("SITE_ID") Is Nothing Then
                SITE_ID = CommonUtility.Get_StringValue(Request.QueryString("SITE_ID"))
            End If
            If Not Request.QueryString("ReceiptType") Is Nothing Then
                ReceiptType = CommonUtility.Get_StringValue(Request.QueryString("ReceiptType"))
            End If
            If Not Request.QueryString("request_type") Is Nothing Then
                request_type = CommonUtility.Get_StringValue(Request.QueryString("request_type"))
            End If

            Try
                Dim table1 As DataTable = New DataTable("Data")
                Dim Col0 As DataColumn = New DataColumn("INDEX", Type.GetType("System.String"))
                Dim Col1 As DataColumn = New DataColumn("COMPANY_NAME", Type.GetType("System.String"))
                Dim Col2 As DataColumn = New DataColumn("BILL_NO", Type.GetType("System.String"))
                Dim Col3 As DataColumn = New DataColumn("FORM_COUNT", Type.GetType("System.Int32"))
                Dim Col4 As DataColumn = New DataColumn("START_TIME", Type.GetType("System.DateTime"))
                Dim Col5 As DataColumn = New DataColumn("FINISH_TIME", Type.GetType("System.DateTime"))
                Dim Col6 As DataColumn = New DataColumn("TOTAL_TIME", Type.GetType("System.String"))
                Dim Col7 As DataColumn = New DataColumn("AVG_TIME", Type.GetType("System.String"))

                table1.Columns.Add(Col0)
                table1.Columns.Add(Col1)
                table1.Columns.Add(Col2)
                table1.Columns.Add(Col3)
                table1.Columns.Add(Col4)
                table1.Columns.Add(Col5)
                table1.Columns.Add(Col6)
                table1.Columns.Add(Col7)

                Dim ds As New DataSet
                Dim cmd As String = "SP_KPI_REPORT_NewDS"
                Dim MAX_TIME As Integer = 1800

                If ReportType = "ds" Then
                    cmd = "SP_KPI_DS_REPORT_NewDS_V1"
                    MAX_TIME = 900
                    If DSPrintOrCheck = "checkdoc" Then
                        cmd = "SP_KPI_DS_CHECKDOC_REPORT_NewDS_V2"
                    End If
                End If

                Dim npm(4) As SqlParameter
                npm(0) = New SqlParameter("@TCAT", ReceiptType)
                npm(1) = New SqlParameter("@FROM_DATE", FROM_DATE)
                npm(2) = New SqlParameter("@TO_DATE", TO_DATE)
                npm(3) = New SqlParameter("@SITE_ID", SITE_ID)
                npm(4) = New SqlParameter("@request_type", request_type)

                ds = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, cmd, npm)


                If ds.Tables(0).Rows.Count > 0 Then
                    Dim m_stream As New System.IO.MemoryStream()
                    Dim rpt As New rptEDI_Report_12

                    If Request.QueryString("TYPE") = "A" Then
                        For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                            Dim aRow As DataRow = table1.NewRow()
                            With ds.Tables(0).Rows(i)
                                Dim date1 As Date = CommonUtility.Get_DateTime(.Item("START_TIME"))
                                Dim date2 As Date = CommonUtility.Get_DateTime(.Item("FINISH_TIME"))

                                Dim TOTAL_MINTUE As Integer = DateDiff(DateInterval.Minute, date1, date2)
                                Dim AVG_MINTUE As Integer

                                Dim TOTAL_TIME As String
                                Dim AVG_TIME As String

                                TOTAL_TIME = SecondsToText(DateDiff(DateInterval.Second, date1, date2))
                                AVG_MINTUE = DateDiff(DateInterval.Second, date1, date2) / CommonUtility.Get_Int32(.Item("FORM_COUNT"))
                                AVG_TIME = SecondsToText(AVG_MINTUE)

                                aRow(0) = CommonUtility.Get_StringValue(i + 1)
                                aRow(1) = CommonUtility.Get_StringValue(.Item("COMPANY_NAME"))
                                aRow(2) = CommonUtility.Get_StringValue(.Item("BILL_NO"))
                                aRow(3) = CommonUtility.Get_StringValue(.Item("FORM_COUNT"))
                                aRow(4) = CommonUtility.Get_DateTime(.Item("START_TIME"))
                                aRow(5) = CommonUtility.Get_DateTime(.Item("FINISH_TIME"))
                                aRow(6) = CommonUtility.Get_StringValue(TOTAL_TIME)
                                aRow(7) = CommonUtility.Get_StringValue(AVG_TIME)

                            End With
                            table1.Rows.Add(aRow)
                        Next
                    Else
                        Dim index As Integer = 0
                        For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                            Dim aRow As DataRow = table1.NewRow()
                            With ds.Tables(0).Rows(i)
                                Dim date1 As Date = CommonUtility.Get_DateTime(.Item("START_TIME"))
                                Dim date2 As Date = CommonUtility.Get_DateTime(.Item("FINISH_TIME"))

                                Dim TOTAL_MINTUE As Integer = DateDiff(DateInterval.Minute, date1, date2)
                                Dim AVG_MINTUE As Integer = DateDiff(DateInterval.Second, date1, date2) / CommonUtility.Get_Int32(.Item("FORM_COUNT"))

                                If AVG_MINTUE < MAX_TIME Then
                                    Dim TOTAL_TIME As String
                                    Dim AVG_TIME As String
                                    index = index + 1
                                    TOTAL_TIME = SecondsToText(DateDiff(DateInterval.Second, date1, date2))
                                    'AVG_MINTUE = DateDiff(DateInterval.Second, date1, date2) / CommonUtility.Get_Int32(.Item("FORM_COUNT"))
                                    AVG_TIME = SecondsToText(AVG_MINTUE)

                                    aRow(0) = CommonUtility.Get_StringValue(index)
                                    aRow(1) = CommonUtility.Get_StringValue(.Item("COMPANY_NAME"))
                                    aRow(2) = CommonUtility.Get_StringValue(.Item("BILL_NO"))
                                    aRow(3) = CommonUtility.Get_StringValue(.Item("FORM_COUNT"))
                                    aRow(4) = CommonUtility.Get_DateTime(.Item("START_TIME"))
                                    aRow(5) = CommonUtility.Get_DateTime(.Item("FINISH_TIME"))
                                    aRow(6) = CommonUtility.Get_StringValue(TOTAL_TIME)
                                    aRow(7) = CommonUtility.Get_StringValue(AVG_TIME)
                                    table1.Rows.Add(aRow)
                                End If
                            End With
                        Next
                    End If

                    rpt.DataSource = table1

                    If ReportType = "ds" Then
                        rpt.Label3.Text = "ชื่อกระบวนงาน การให้บริการออกหนังสือรับรองถิ่นกำเนิดสินค้า ด้วยระบบลงลายมือชื่ออิเล็คทรอนิกส์ (Digital Signature)"
                        rpt.Label10.Text = "รอบระยะเวลามาตรฐานที่ให้บริการ  คือ  15  นาที"
                        rpt.Label8.Text = "จำนวนขั้นตอนให้บริการทั้งหมด 4 ขั้นตอน"
                        rpt.Label26.Text = "กระบวนการพิมพ์เอกสาร ระบบ Digital Signature"
                        If DSPrintOrCheck = "checkdoc" Then
                            rpt.Label26.Text = "กระบวนการตรวจสอบเอกสาร ระบบ Digital Signature"
                            rpt.Label8.Text = "จำนวนขั้นตอนให้บริการทั้งหมด 1 ขั้นตอน"
                        End If
                    End If

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

    Function SecondsToText(ByVal Seconds) As String
        Dim bAddComma As Boolean
        Dim Result As String
        Dim sTemp As String
        Dim days, hours, minutes

        If Seconds <= 0 Or Not IsNumeric(Seconds) Then
            SecondsToText = "00:00:00"
            Exit Function
        End If

        Seconds = Fix(Seconds)

        If Seconds >= 86400 Then
            days = Fix(Seconds / 86400)
        Else
            days = 0
        End If

        If Seconds - (days * 86400) >= 3600 Then
            hours = Fix((Seconds - (days * 86400)) / 3600)
        Else
            hours = 0
        End If

        If Seconds - (hours * 3600) - (days * 86400) >= 60 Then
            minutes = Fix((Seconds - (hours * 3600) - (days * 86400)) / 60)
        Else
            minutes = 0
        End If

        Seconds = Seconds - (minutes * 60) - (hours * 3600) - (days * 86400)

        If Seconds > 0 Then Result = Seconds '& ":"
        If Seconds = 0 Then Result = "00"

        If Result.Length = 1 Then
            Result = "0" & Result
        End If

        If minutes > 0 Then
            bAddComma = Result <> ""
            Dim a As String = "aaa"

            If CInt(minutes).ToString.Length = 1 Then
                sTemp = "0" & minutes & ":"

            Else
                sTemp = minutes & ":"
            End If

            If bAddComma Then sTemp = sTemp
            Result = sTemp & Result
        End If

        If minutes = 0 Then Result = "00:" & Result

        If hours > 0 Then
            bAddComma = Result <> ""

            If CInt(hours).ToString.Length = 1 Then
                sTemp = "0" & hours & ":"
            Else
                sTemp = hours & ":"
            End If

            If bAddComma Then sTemp = sTemp
            Result = sTemp & Result
        End If

        If hours = 0 Then Result = "00:" & Result

        SecondsToText = Result
    End Function
End Class