Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports DFT.Dotnetnuke.ClassLibrary
Imports System.IO

Partial Public Class frmFormPDFPage
    Inherits System.Web.UI.Page
    Dim strEDIConn As String = ConfigurationManager.ConnectionStrings("OriginConnection").ConnectionString
    Friend WithEvents PdfExport1 As DataDynamics.ActiveReports.Export.Pdf.PdfExport

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Try
                Dim ds As New DataSet
                ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, "vi_form4_edi_printPDF_NewDS", _
                New SqlParameter("@INVH_RUN_AUTO", Request.QueryString("INVH_RUN_AUTO")))

                If ds.Tables(0).Rows.Count > 0 Then
                    Dim m_stream As New System.IO.MemoryStream()
                    Dim rpt As Object = Nothing
                    Select Case CommonUtility.Get_StringValue(ds.Tables(0).Rows(0).Item("form_type")).ToUpper()
                        Case "FORM1"
                            rpt = New rpt3_ediFORM1_pr
                        Case "FORM1_1"
                            rpt = New rpt3_ediFORM1_1_pr
                        Case "FORM1_2"
                            rpt = New rpt3_ediFORM1_2_pr
                        Case "FORM1_3"
                            rpt = New rpt3_ediFORM1_3_pr
                        Case "FORM1_4"
                            rpt = New rpt3_ediFORM1_4_pr
                        Case "FORMRUSSIA"
                            rpt = New rpt3_ediFORM1RU_pr
                        Case "FORM2"
                            rpt = New rpt3_ediFORM2_pr
                        Case "FORM2_1"
                            rpt = New rpt3_ediFORM2_1_pr
                        Case "FORM2_2"
                            rpt = New rpt3_ediFORM2_2_pr
                        Case "FORM2_3"
                            rpt = New rpt3_ediFORM2_3_pr
                        Case "FORM2_4"
                            rpt = New rpt3_ediFORM2_4_pr
                        Case "FORM3"
                            rpt = New rpt3_ediFORM3_pr
                        Case "FORM4"
                            rpt = New rpt3_ediFORM4_pr
                        Case "FORM4_1" '//Last update by Madnattz 20/04/55
                            rpt = New rpt3_ediFORM4_1_pr
                        Case "FORM44"
                            rpt = New rpt3_ediFORM44_pr
                        Case "FORM441"
                            rpt = New rpt3_ediFORM441_pr
                        Case "FORM4_2"
                            rpt = New rpt3_ediFORM4_2_pr_New
                        Case "FORM4_3"
                            rpt = New rpt3_ediFORMs4_3_pr
                        Case "FORM4_4"
                            rpt = New rpt3_ediFORM4_4_pr
                        Case "FORM4_5"
                            rpt = New rpt3_ediFORM4_5_pr
                        Case "FORM4_6"
                            rpt = New rpt3_ediFORM4_6_pr
                        Case "FORM4_8"
                            rpt = New rpt3_ediFORM4_8_pr
                        Case "FORM4_9"
                            rpt = New rpt3_ediFORM4_9_pr
                        Case "FORM4_91"
                            rpt = New rpt3_ediFORM4_91_pr
                        Case "FORM5"
                            rpt = New rpt3_ediFORM5_pr
                        Case "FORM5_1"
                            rpt = New rpt3_ediFORM5_1_pr
                        Case "FORM6"
                            rpt = New rpt3_ediFORM6_pr
                        Case "FORM7"
                            rpt = New rpt3_ediFORM7_pr
                        Case "FORM8"
                            rpt = New rpt3_ediFORM8_pr
                        Case "FORM9"
                            rpt = New rpt3_ediFORM9_pr
                    End Select

                    If Not rpt Is Nothing Then
                        rpt.C_TotalRowDe.Text = ds.Tables(0).Rows.Count
                        rpt.DataSource = ds.Tables(0)
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