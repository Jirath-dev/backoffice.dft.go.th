Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports DFT.Dotnetnuke.ClassLibrary

Partial Public Class frmQuotaReport
    Inherits System.Web.UI.Page
    Friend WithEvents PdfExport1 As DataDynamics.ActiveReports.Export.Pdf.PdfExport
    Dim strEDIConn As String = ConfigurationManager.ConnectionStrings("OriginConnection").ConnectionString

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Try
                Dim ds As New DataSet
                Dim strCommand As String
                strCommand = "SELECT * FROM TransStock WHERE CONVERT(varchar(8),TSK_Date,112) BETWEEN " & CommonUtility.Get_StringValue(Request.QueryString("FROM_DATE")) & " AND " & CommonUtility.Get_StringValue(Request.QueryString("TO_DATE"))
                ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.Text, strCommand)

                If ds.Tables(0).Rows.Count > 0 Then
                    Dim dt As New DataTable("xTable")
                    dt.Columns.Add("TSK_Date", Type.GetType("System.String"))
                    dt.Columns.Add("TSK_Debit", Type.GetType("System.Decimal"))
                    dt.Columns.Add("CompanyName_En", Type.GetType("System.String"))
                    dt.Columns.Add("TSK_Credit", Type.GetType("System.Decimal"))
                    dt.Columns.Add("reference_code2", Type.GetType("System.String"))
                    dt.Columns.Add("ReferenceDesc1", Type.GetType("System.String"))
                    dt.Columns.Add("ReferenceDesc2", Type.GetType("System.String"))
                    dt.Columns.Add("Amount", Type.GetType("System.Decimal"))

                    Dim TSK_Date As String
                    Dim TSK_Debit As Decimal
                    Dim CompanyName_En As String
                    Dim TSK_Credit As Decimal
                    Dim reference_code2 As String
                    Dim ReferenceDesc1 As String
                    Dim ReferenceDesc2 As String
                    Dim Amount As Decimal
                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                        With ds.Tables(0).Rows(i)
                            If i = 0 Then
                                'lblTotalQuota.Text = CommonUtility.Get_StringValue(.Item("TSK_Debit"))
                                TSK_Date = CommonUtility.Get_StringValue(Convert.ToDateTime(.Item("TSK_Date")).ToString("dd/MM/yyyy"))
                                TSK_Debit = CommonUtility.Get_Decimal(.Item("TSK_Debit"))
                                CompanyName_En = CommonUtility.Get_StringValue(.Item("CompanyName_En"))
                                TSK_Credit = CommonUtility.Get_Decimal(.Item("TSK_Credit"))
                                reference_code2 = CommonUtility.Get_StringValue(.Item("reference_code2"))
                                ReferenceDesc1 = CommonUtility.Get_StringValue(.Item("ReferenceDesc1"))
                                ReferenceDesc2 = CommonUtility.Get_StringValue(.Item("ReferenceDesc2"))
                                If CommonUtility.Get_StringValue(.Item("TSK_Type1")) = "C" Then
                                    Amount = CommonUtility.Get_Decimal(TSK_Debit - TSK_Credit)
                                Else
                                    Amount = CommonUtility.Get_Decimal(TSK_Debit + TSK_Credit)
                                End If

                                dt.Rows.Add(New Object() {TSK_Date, TSK_Debit, CompanyName_En, TSK_Credit, reference_code2, ReferenceDesc1, ReferenceDesc2, Amount})
                            Else
                                TSK_Date = CommonUtility.Get_StringValue(Convert.ToDateTime(.Item("TSK_Date")).ToString("dd/MM/yyyy"))
                                TSK_Debit = CommonUtility.Get_Decimal(Amount)
                                CompanyName_En = CommonUtility.Get_StringValue(.Item("CompanyName_En"))
                                TSK_Credit = CommonUtility.Get_Decimal(.Item("TSK_Credit"))
                                reference_code2 = CommonUtility.Get_StringValue(.Item("reference_code2"))
                                ReferenceDesc1 = CommonUtility.Get_StringValue(.Item("ReferenceDesc1"))
                                ReferenceDesc2 = CommonUtility.Get_StringValue(.Item("ReferenceDesc2"))
                                If CommonUtility.Get_StringValue(.Item("TSK_Type1")) = "C" Then
                                    Amount = CommonUtility.Get_Decimal(TSK_Debit - TSK_Credit)
                                Else
                                    Amount = CommonUtility.Get_Decimal(TSK_Debit + TSK_Credit)
                                End If

                                dt.Rows.Add(New Object() {TSK_Date, TSK_Debit, CompanyName_En, TSK_Credit, reference_code2, ReferenceDesc1, ReferenceDesc2, Amount})
                            End If
                        End With
                    Next

                    Dim m_stream As New System.IO.MemoryStream()
                    Dim rpt As New rptQuota
                    rpt.DataSource = dt
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