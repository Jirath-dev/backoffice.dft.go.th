Imports Microsoft.VisualBasic
Imports Microsoft.ApplicationBlocks.Data
Imports System.Data.SqlClient
Imports System.Data
Imports System.Text
Imports DataDynamics.ActiveReports.Export.Pdf
Imports DataDynamics.ActiveReports.Viewer.Viewer
Imports DFT.Dotnetnuke.ClassLibrary

Imports System.Drawing

Imports System.Threading
Imports System.Globalization
Imports ReportPrintClass

Partial Public Class View_ReportDUP
    Inherits System.Web.UI.Page
    Dim strEDIConn As String = ConfigurationManager.ConnectionStrings("OriginConnection").ConnectionString
    Dim counter As Integer
    Friend WithEvents PdfExport1 As DataDynamics.ActiveReports.Export.Pdf.PdfExport
    Dim _ISB2B As Boolean
    Dim _CompanyTaxno, _FullFormName, _B2BRefNo, _B2BCountry As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Select Case LoadReportView(Request.QueryString("CellType"), Request.QueryString("SendSiteID"), CInt(Request.QueryString("radioForm")), Request.QueryString("SendSeleteDATE"), Request.QueryString("SendCHDATE"), Request.QueryString("YearsOle"), Request.QueryString("YearsID"), Request.QueryString("SendTrueCopy"))
                Case 1
                    Page.SetFocus(Page)
                    Page.Title = "แสดงรายงาน"

                    '0=คำขอ,1=หนังสือรับรอง
                    checkUpdate_user(Request.QueryString("SendCell"), Request.QueryString("CheckUserPage"), Request.QueryString("radioForm"), Request.QueryString("YearsOle"), Request.QueryString("YearsID"))

                Case 2
                    If updatePrintTotal(Request.QueryString("SendCell"), CStr(1)) = True Then
                        '0=คำขอ,1=หนังสือรับรอง
                        checkUpdate_user(Request.QueryString("SendCell"), Request.QueryString("CheckUserPage"), Request.QueryString("radioForm"), Request.QueryString("YearsOle"), Request.QueryString("YearsID"))
                    End If
                    Page.Title = "ไม่มีรายการสินค้า"
                Case 3
                    Page.Title = "ไม่พบรายงาน"
            End Select
            'If LoadReportView(Request.QueryString("CellType"), Request.QueryString("SendSiteID"), CInt(Request.QueryString("radioForm"))) = True Then
            '    Page.SetFocus(Page)
            '    Page.Title = "แสดงรายงาน"
            'Else
            '    Page.Title = "ไม่พบรายงาน"
            'End If
        End If
    End Sub

    Function LoadReportView(ByVal SendFormTypes As String, ByVal SiteID As String, ByVal radioForms As Integer, ByVal strDate As String, ByVal _SendCHDATE As Boolean, ByVal By_checkCase As Boolean, ByVal By_ID As String, ByVal _ChkTrueCopy As Boolean) As Integer
        Dim dsRequestDetails As New DataSet

        If SiteID = "" Then
            Select Case By_checkCase
                Case True
                    Select Case By_ID
                        Case "1"
                            dsRequestDetails = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, "vi_form4_edi_RePrintFormBar_NewDS_Ole01",
                                                    New SqlParameter("@INVH_RUN_AUTO", Request.QueryString("SendCell")),
                                                    New SqlParameter("@SITE_ID", SiteID))
                        Case "2"
                            dsRequestDetails = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, "vi_form4_edi_RePrintFormBar_NewDS_Ole02",
                                                                               New SqlParameter("@INVH_RUN_AUTO", Request.QueryString("SendCell")),
                                                                               New SqlParameter("@SITE_ID", SiteID))
                        Case "3"
                            dsRequestDetails = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, "vi_form4_edi_RePrintFormBar_NewDS_Ole03",
                                                                               New SqlParameter("@INVH_RUN_AUTO", Request.QueryString("SendCell")),
                                                                               New SqlParameter("@SITE_ID", SiteID))
                        Case "4"
                            dsRequestDetails = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, "vi_form4_edi_RePrintFormBar_NewDS_Ole04",
                                                                               New SqlParameter("@INVH_RUN_AUTO", Request.QueryString("SendCell")),
                                                                               New SqlParameter("@SITE_ID", SiteID))
                        Case "5"
                            dsRequestDetails = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, "vi_form4_edi_RePrintFormBar_NewDS_Ole05",
                                                                               New SqlParameter("@INVH_RUN_AUTO", Request.QueryString("SendCell")),
                                                                               New SqlParameter("@SITE_ID", SiteID))
                        Case "6"
                            dsRequestDetails = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, "vi_form4_edi_RePrintFormBar_NewDS_Ole06",
                                                                               New SqlParameter("@INVH_RUN_AUTO", Request.QueryString("SendCell")),
                                                                               New SqlParameter("@SITE_ID", SiteID))
                        Case "7"
                            dsRequestDetails = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, "vi_form4_edi_RePrintFormBar_NewDS_Ole07",
                                                                               New SqlParameter("@INVH_RUN_AUTO", Request.QueryString("SendCell")),
                                                                               New SqlParameter("@SITE_ID", SiteID))
                    End Select
                Case False
                    Dim cmd As String = "vi_form4_edi_printFormBar_NewDS_test"

                    Select Case SendFormTypes
                        Case "FORM44_01", "FORM44_02", "FORMAHK", "FORME_01", "FORMD_ESS", "FORMD_ESS_", "FORMTC_ESS", "FORME_ESS"
                            cmd = "vi_form4_edi_printFormBar_NewDS_ASW"
                            'Case "FORM4_9"
                            '    cmd = "vi_form4_edi_printFormBar_NewDS_test"
                    End Select
                    dsRequestDetails = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, cmd,
                                            New SqlParameter("@INVH_RUN_AUTO", Request.QueryString("SendCell")),
                                            New SqlParameter("@SITE_ID", SiteID))
            End Select

        Else
            Select Case By_checkCase
                Case True
                    Select Case By_ID
                        Case "1"
                            dsRequestDetails = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, "vi_form4_edi_RePrintFormBar_NewDS_Ole01",
                                                   New SqlParameter("@INVH_RUN_AUTO", Request.QueryString("SendCell")),
                                                   New SqlParameter("@SITE_ID", SiteID))
                        Case "2"
                            dsRequestDetails = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, "vi_form4_edi_RePrintFormBar_NewDS_Ole02",
                                                   New SqlParameter("@INVH_RUN_AUTO", Request.QueryString("SendCell")),
                                                   New SqlParameter("@SITE_ID", SiteID))
                        Case "3"
                            dsRequestDetails = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, "vi_form4_edi_RePrintFormBar_NewDS_Ole03",
                                                   New SqlParameter("@INVH_RUN_AUTO", Request.QueryString("SendCell")),
                                                   New SqlParameter("@SITE_ID", SiteID))
                        Case "4"
                            dsRequestDetails = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, "vi_form4_edi_RePrintFormBar_NewDS_Ole04",
                                                   New SqlParameter("@INVH_RUN_AUTO", Request.QueryString("SendCell")),
                                                   New SqlParameter("@SITE_ID", SiteID))
                        Case "5"
                            dsRequestDetails = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, "vi_form4_edi_RePrintFormBar_NewDS_Ole05",
                                                   New SqlParameter("@INVH_RUN_AUTO", Request.QueryString("SendCell")),
                                                   New SqlParameter("@SITE_ID", SiteID))
                        Case "6"
                            dsRequestDetails = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, "vi_form4_edi_RePrintFormBar_NewDS_Ole06",
                                                   New SqlParameter("@INVH_RUN_AUTO", Request.QueryString("SendCell")),
                                                   New SqlParameter("@SITE_ID", SiteID))
                        Case "7"
                            dsRequestDetails = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, "vi_form4_edi_RePrintFormBar_NewDS_Ole07",
                                                   New SqlParameter("@INVH_RUN_AUTO", Request.QueryString("SendCell")),
                                                   New SqlParameter("@SITE_ID", SiteID))
                    End Select
                Case False
                    dsRequestDetails = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, "vi_form4_edi_RePrintFormBar_NewDS",
                                           New SqlParameter("@INVH_RUN_AUTO", Request.QueryString("SendCell")),
                                           New SqlParameter("@SITE_ID", SiteID))
            End Select

        End If

        If dsRequestDetails.Tables(0).Rows.Count > 0 Then

            ''=======================================
            ''ByTine 12-09-2559 สำหรับ B2B
            If DBNull.Value.Equals(dsRequestDetails.Tables(0).Rows(0).Item("IsBackToBack")) = True Then
                _ISB2B = False
            ElseIf dsRequestDetails.Tables(0).Rows(0).Item("IsBackToBack") = False Then
                _ISB2B = False
            Else
                _ISB2B = dsRequestDetails.Tables(0).Rows(0).Item("IsBackToBack")
                _CompanyTaxno = CommonUtility.Get_String(dsRequestDetails.Tables(0).Rows(0).Item("company_taxno"))
                _B2BCountry = CommonUtility.Get_String(dsRequestDetails.Tables(0).Rows(0).Item("ImportCountry"))
                _B2BRefNo = CommonUtility.Get_String(dsRequestDetails.Tables(0).Rows(0).Item("B2BReferenceCode"))
                _FullFormName = CommonUtility.Get_String(dsRequestDetails.Tables(0).Rows(0).Item("b2b_form_name"))
            End If


            Dim IsNewFormE As Boolean = CommonUtility.Get_Boolean(dsRequestDetails.Tables(0).Rows(0).Item("NewFormE"))

            ''=======================================

            Try
                If radioForms = 0 Then
                    Select Case SendFormTypes
                        Case "FORM1"
                            If View_ReEdi_A(dsRequestDetails) = True Then
                                Return 1
                            Else
                                Return 3
                            End If
                        Case "FORMRussia"
                            If View_ReEdi_A(dsRequestDetails) = True Then
                                Return 1
                            Else
                                Return 3
                            End If
                        Case "FORM1_1"
                            If View_ReEdi_A(dsRequestDetails) = True Then
                                Return 1
                            Else
                                Return 3
                            End If
                        Case "FORM1_2"
                            If View_ReEdi_A(dsRequestDetails) = True Then
                                Return 1
                            Else
                                Return 3
                            End If
                        Case "FORM1_3"
                            If View_ReEdi_A(dsRequestDetails) = True Then
                                Return 1
                            Else
                                Return 3
                            End If
                        Case "FORM1_4"
                            If View_ReEdi_A(dsRequestDetails) = True Then
                                Return 1
                            Else
                                Return 3
                            End If
                        Case "FORM2", "FORM2_ESS"
                            If View_ReEdi_CO(dsRequestDetails) = True Then
                                Return 1
                            Else
                                Return 3
                            End If
                        Case "FORM2_1"
                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then
                                LoadMulti_Re_Form2_1(CommonUtility.Get_StringValue(dsRequestDetails.Tables(0).Rows(0).Item("card_id")), Request.QueryString("SendCell"), SiteID)
                                Return 1
                            Else
                                Return 3
                            End If
                        Case "FORM2_2", "FORM2_5", "FORM2_6"
                            If View_ReEdi_A(dsRequestDetails) = True Then
                                Return 1
                            Else
                                Return 3
                            End If
                        Case "FORM2_3"
                            If View_ReEdi_A(dsRequestDetails) = True Then
                                Return 1
                            Else
                                Return 3
                            End If
                        Case "FORM2_4"
                            If View_ReEdi_A_24(dsRequestDetails) = True Then
                                Return 1
                            Else
                                Return 3
                            End If
                        Case "FORM3"
                            If View_ReEdi_A(dsRequestDetails) = True Then
                                Return 1
                            Else
                                Return 3
                            End If
                        Case "FORM3_1"
                            If View_ReEdi_A(dsRequestDetails) = True Then
                                Return 1
                            Else
                                Return 3
                            End If
                            'by rut D att
                            '==================================
                            'FORM4		ฟอร์ม ดี (ATIGA) เก่า (2 ประเทศ)
                            'FORM44_4	ฟอร์ม ดี (ATIGA) ใหม่ (7 ประเทศ)
                            'FORM44_44	ฟอร์ม ดี (ATIGA) Attach Sheet รถยนต์ ใหม่ (7 ประเทศ)
                            'FORM441_4	ฟอร์ม ดี AICO Attach Sheet รถยนต์ ใหม่ (7 ประเทศ)
                        Case "FORM4", "FORM44_4", "FORM44_44", "FORM441_4"
                            If View_ReEdi_A(dsRequestDetails) = True Then
                                Return 1
                            Else
                                Return 3
                            End If
                        Case "FORM44_01", "FORM44_02", "FORMAHK", "FORME_01", "FORMD_ESS", "FORMD_ESS_", "FORME_ESS"
                            If View_ReEdi_ASW(dsRequestDetails) = True Then
                                Return 1
                            Else
                                Return 3
                            End If

                            'FORM44		ฟอร์ม ดี (ATIGA) Attach Sheet รถยนต์ เก่า (2 ประเทศ)

                        Case "FORM44"
                            If View_ReEdi_A44(dsRequestDetails) = True Then
                                Return 1
                            Else
                                Return 3
                            End If
                            'FORM4_1		ฟอร์ม ดี AICO เก่า (2 ประเทศ)
                            'FORM44_41	ฟอร์ม ดี AICO ใหม่ (7 ประเทศ)
                        Case "FORM4_1", "FORM44_41"
                            If View_ReEdi_A(dsRequestDetails) = True Then
                                Return 1
                            Else
                                Return 3
                            End If
                            'FORM441		ฟอร์ม ดี AICO Attach Sheet รถยนต์ เก่า (2 ประเทศ)
                        Case "FORM441"
                            If View_ReEdi_A44(dsRequestDetails) = True Then
                                Return 1
                            Else
                                Return 3
                            End If
                            '==================================
                        Case "FORM4_2"
                            If View_ReEdi_A(dsRequestDetails) = True Then
                                Return 1
                            Else
                                Return 3
                            End If
                        Case "FORM4_3"
                            If View_ReEdi_A(dsRequestDetails) = True Then
                                Return 1
                            Else
                                Return 3
                            End If
                        Case "FORM4_4"
                            If View_ReEdi_A(dsRequestDetails) = True Then
                                Return 1
                            Else
                                Return 3
                            End If
                        Case "FORM4_5"
                            If View_ReEdi_A(dsRequestDetails) = True Then
                                Return 1
                            Else
                                Return 3
                            End If
                        Case "FORM4_6" 'เก่า
                            If View_ReEdi_A(dsRequestDetails) = True Then
                                Return 1
                            Else
                                Return 3
                            End If
                        Case "FORM4_6" 'ใหม่
                            If View_ReEdi_A(dsRequestDetails) = True Then
                                Return 1
                            Else
                                Return 3
                            End If
                        Case "FORM4_81" 'เก่า
                            If View_ReEdi_A(dsRequestDetails) = True Then
                                Return 1
                            Else
                                Return 3
                            End If
                        Case "FORM4_8" 'ใหม่
                            If View_ReEdi_A(dsRequestDetails) = True Then
                                Return 1
                            Else
                                Return 3
                            End If
                        Case "FORM4_9"
                            If View_ReEdi_A(dsRequestDetails) = True Then
                                Return 1
                            Else
                                Return 3
                            End If

                            ''ByTine 23-03-2559 เพิ่มฟอร์ม AANZ ใหม่ FORM4_911
                        Case "FORM4_91", "FORM4_911"
                            If View_ReEdi_A(dsRequestDetails) = True Then
                                Return 1
                            Else
                                Return 3
                            End If

                            ''ByTine 15-10-2558 เพิ่มฟอร์มใหม่ไทย-ชิลี FORM5_2
                        Case "FORM5", "FORM5_1", "FORMTP_ESS", "FORM5_2"
                            If View_ReEdi_A(dsRequestDetails) = True Then
                                Return 1
                            Else
                                Return 3
                            End If
                        Case "FORM6"
                            If View_ReEdi_A(dsRequestDetails) = True Then
                                Return 1
                            Else
                                Return 3
                            End If
                        Case "FORM7"
                            If View_ReEdi_A(dsRequestDetails) = True Then
                                Return 1
                            Else
                                Return 3
                            End If
                        Case "FORM8"
                            If View_ReEdi_A(dsRequestDetails) = True Then
                                Return 1
                            Else
                                Return 3
                            End If
                        Case "FORM9"
                            If View_ReEdi_A(dsRequestDetails) = True Then
                                Return 1
                            Else
                                Return 3
                            End If
                        Case Else
                            Return 3
                    End Select
                Else
                    'view หนังสือรับรอง
                    Select Case SendFormTypes
                        Case "FORM1"
                            Dim rpt = New rpt3_ediFORM1
                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then
                                'POL
                                Dim m_stream As New System.IO.MemoryStream()
                                rpt.C_TotalRowDe.text = dsRequestDetails.Tables(0).Rows.Count

                                ReportPrintClass.RPT_select(rpt, strDate, _SendCHDATE)

                                rpt.DataSource = dsRequestDetails.Tables(0)
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
                                Return 1
                            Else
                                Return 3
                            End If
                        Case "FORMRussia"
                            Dim rpt = New rpt3_ediFORM1RU_VR
                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then
                                'POL
                                Dim m_stream As New System.IO.MemoryStream()
                                rpt.C_TotalRowDe.text = dsRequestDetails.Tables(0).Rows.Count
                                ReportPrintClass.RPT_select(rpt, strDate, _SendCHDATE)

                                rpt.DataSource = dsRequestDetails.Tables(0)
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
                                Return 1
                            Else
                                Return 3
                            End If
                        Case "FORM1_1"
                            Dim rpt = New rpt3_ediFORM1_1
                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then
                                'POL
                                Dim m_stream As New System.IO.MemoryStream()
                                rpt.C_TotalRowDe.text = dsRequestDetails.Tables(0).Rows.Count
                                ReportPrintClass.RPT_select(rpt, strDate, _SendCHDATE)

                                rpt.DataSource = dsRequestDetails.Tables(0)
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
                                Return 1
                            Else
                                Return 3
                            End If
                        Case "FORM1_2"
                            Dim rpt = New rpt3_ediFORM1_2
                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then
                                'POL
                                Dim m_stream As New System.IO.MemoryStream()
                                rpt.C_TotalRowDe.text = dsRequestDetails.Tables(0).Rows.Count
                                ReportPrintClass.RPT_select(rpt, strDate, _SendCHDATE)

                                rpt.DataSource = dsRequestDetails.Tables(0)
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
                                Return 1
                            Else
                                Return 3
                            End If
                        Case "FORM1_3"
                            'view

                            Dim rpt = New rpt3_ediFORM1_3
                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then
                                'POL
                                Dim m_stream As New System.IO.MemoryStream()
                                rpt.C_TotalRowDe.text = dsRequestDetails.Tables(0).Rows.Count
                                ReportPrintClass.RPT_select(rpt, strDate, _SendCHDATE)

                                rpt.DataSource = dsRequestDetails.Tables(0)
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
                                Return 1
                            Else
                                Return 3
                            End If
                        Case "FORM1_4"
                            Dim rpt = New rpt3_ediFORM1_4
                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then
                                'POL
                                Dim m_stream As New System.IO.MemoryStream()
                                rpt.C_TotalRowDe.text = dsRequestDetails.Tables(0).Rows.Count
                                ReportPrintClass.RPT_select(rpt, strDate, _SendCHDATE)

                                rpt.DataSource = dsRequestDetails.Tables(0)
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
                                Return 1
                            Else
                                Return 3
                            End If
                        Case "FORM2"
                            Dim rpt = New rpt3_ediFORM2
                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then
                                'POL
                                Dim m_stream As New System.IO.MemoryStream()
                                rpt.C_TotalRowDe.text = dsRequestDetails.Tables(0).Rows.Count
                                ReportPrintClass.RPT_select(rpt, strDate, _SendCHDATE)

                                rpt.DataSource = dsRequestDetails.Tables(0)
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
                                Return 1
                            Else
                                Return 3
                            End If
                        Case "FORM2_ESS"
                            Dim rpt = New rpt3_ediFORM2_ESS_VR
                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then
                                'POL
                                Dim m_stream As New System.IO.MemoryStream()
                                rpt.C_TotalRowDe.text = dsRequestDetails.Tables(0).Rows.Count
                                ReportPrintClass.RPT_select(rpt, strDate, _SendCHDATE)

                                rpt.DataSource = dsRequestDetails.Tables(0)
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
                                Return 1
                            Else
                                Return 3
                            End If
                        Case "FORM2_1"
                            Dim rpt = New rpt3_ediFORM2_1
                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then
                                'POL
                                Dim m_stream As New System.IO.MemoryStream()
                                rpt.C_TotalRowDe.text = dsRequestDetails.Tables(0).Rows.Count
                                ReportPrintClass.RPT_select(rpt, strDate, _SendCHDATE)

                                rpt.DataSource = dsRequestDetails.Tables(0)
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

                                Return 1
                            Else
                                Return 3
                            End If
                        Case "FORM2_2"
                            Dim rpt = New rpt3_ediFORM2_2_VR
                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then
                                'POL
                                Dim m_stream As New System.IO.MemoryStream()
                                rpt.C_TotalRowDe.text = dsRequestDetails.Tables(0).Rows.Count
                                ReportPrintClass.RPT_select(rpt, strDate, _SendCHDATE)

                                rpt.DataSource = dsRequestDetails.Tables(0)
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
                                Return 1
                            Else
                                Return 3
                            End If
                        Case "FORM2_3"
                            Dim rpt = New rpt3_ediFORM2_3
                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then
                                'POL
                                Dim m_stream As New System.IO.MemoryStream()
                                rpt.C_TotalRowDe.text = dsRequestDetails.Tables(0).Rows.Count
                                ReportPrintClass.RPT_select(rpt, strDate, _SendCHDATE)

                                rpt.DataSource = dsRequestDetails.Tables(0)
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
                                Return 1
                            Else
                                Return 3
                            End If
                        Case "FORM2_4"
                            Dim rpt = New rpt3_ediFORM2_4
                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then
                                'POL
                                Dim m_stream As New System.IO.MemoryStream()
                                rpt.C_TotalRowDe.text = dsRequestDetails.Tables(0).Rows.Count
                                ReportPrintClass.RPT_select(rpt, strDate, _SendCHDATE)

                                rpt.DataSource = dsRequestDetails.Tables(0)
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
                                Return 1
                            Else
                                Return 3
                            End If

                            ''ByTine 11-12-2559 ฟอร์มใฟม่ COปลา /COข้าว
                        Case "FORM2_5"
                            Dim rpt = New rpt3_ediFORM2_5
                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then
                                'POL
                                Dim m_stream As New System.IO.MemoryStream()
                                rpt.C_TotalRowDe.text = dsRequestDetails.Tables(0).Rows.Count
                                ReportPrintClass.RPT_select(rpt, strDate, _SendCHDATE)

                                rpt.DataSource = dsRequestDetails.Tables(0)
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
                                Return 1
                            Else
                                Return 3
                            End If
                            ''ByTine 11-12-2559 ฟอร์มใฟม่ COปลา /COข้าว
                        Case "FORM2_6"
                            Dim rpt = New rpt3_ediFORM2_6
                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then
                                'POL
                                Dim m_stream As New System.IO.MemoryStream()
                                rpt.C_TotalRowDe.text = dsRequestDetails.Tables(0).Rows.Count
                                ReportPrintClass.RPT_select(rpt, strDate, _SendCHDATE)

                                rpt.DataSource = dsRequestDetails.Tables(0)
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
                                Return 1
                            Else
                                Return 3
                            End If

                        Case "FORM3"
                            Dim rpt = New rpt3_ediFORM3
                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then
                                'POL
                                Dim m_stream As New System.IO.MemoryStream()
                                rpt.C_TotalRowDe.text = dsRequestDetails.Tables(0).Rows.Count
                                ReportPrintClass.RPT_select(rpt, strDate, _SendCHDATE)

                                rpt.DataSource = dsRequestDetails.Tables(0)
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
                                Return 1
                            Else
                                Return 3
                            End If
                        Case "FORM3_1"
                            Dim rpt = New rpt3_ediFORM3_1
                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then
                                'POL
                                Dim m_stream As New System.IO.MemoryStream()
                                rpt.C_TotalRowDe.text = dsRequestDetails.Tables(0).Rows.Count
                                ReportPrintClass.RPT_select(rpt, strDate, _SendCHDATE)

                                rpt.DataSource = dsRequestDetails.Tables(0)
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
                                Return 1
                            Else
                                Return 3
                            End If
                        Case "FORM4"
                            Dim rpt = New rpt3_ediFORM4
                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then
                                'POL
                                Dim m_stream As New System.IO.MemoryStream()
                                rpt.C_TotalRowDe.text = dsRequestDetails.Tables(0).Rows.Count
                                ReportPrintClass.RPT_select(rpt, strDate, _SendCHDATE)

                                rpt.DataSource = dsRequestDetails.Tables(0)

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
                                Return 1
                            Else
                                Return 3
                            End If
                            'by rut D new
                            'FORM44_4	ฟอร์ม ดี (ATIGA) ใหม่ (7 ประเทศ)
                            'FORM44_44	ฟอร์ม ดี (ATIGA) Attach Sheet รถยนต์ ใหม่ (7 ประเทศ)
                            'FORM44_41	ฟอร์ม ดี AICO ใหม่ (7 ประเทศ)
                            'FORM441_4	ฟอร์ม ดี AICO Attach Sheet รถยนต์ ใหม่ (7 ประเทศ)
                        Case "FORM44_4", "FORM44_44", "FORM44_41", "FORM441_4"
                            Dim rpt As Object
                            Select Case SendFormTypes
                                Case "FORM44_4"
                                    rpt = New rpt3_ediFORM44_4
                                Case "FORM44_44"
                                    rpt = New rpt3_ediFORM44_44
                                Case "FORM44_41"
                                    rpt = New rpt3_ediFORM44_41
                                Case "FORM441_4"
                                    rpt = New rpt3_ediFORM441_4
                            End Select
                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then
                                'POL
                                Dim m_stream As New System.IO.MemoryStream()
                                rpt.txtCheck_CaseRVCCount.text = CheckCount_RVC(Request.QueryString("SendCell"), SendFormTypes)
                                rpt.txtDisplayUDS7.text = Check_DisplayUSD7(Request.QueryString("SendCell"))
                                rpt.C_TotalRowDe.text = dsRequestDetails.Tables(0).Rows.Count
                                ReportPrintClass.RPT_select(rpt, strDate, _SendCHDATE)

                                ''ByTine 24-01-2560 Invoice นายหน้า
                                ''=======================================================
                                If DBNull.Value.Equals(dsRequestDetails.Tables(0).Rows(0).Item("IsAgent")) = False Then
                                    If dsRequestDetails.Tables(0).Rows(0).Item("IsAgent") = True Then
                                        rpt._IsAgent = dsRequestDetails.Tables(0).Rows(0).Item("IsAgent")
                                        rpt._InvAgentType = dsRequestDetails.Tables(0).Rows(0).Item("InvAgentType")
                                        rpt._CompanyName_Agent = dsRequestDetails.Tables(0).Rows(0).Item("CompanyName_Agent")
                                        rpt._Invoice_Agent = dsRequestDetails.Tables(0).Rows(0).Item("Invoice_Agent")
                                    End If
                                End If
                                ''=======================================================

                                rpt.DataSource = dsRequestDetails.Tables(0)

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
                                Return 1
                            Else
                                Return 3
                            End If
                        Case "FORM44"
                            Dim rpt = New rpt3_ediFORM44
                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then
                                'POL
                                Dim m_stream As New System.IO.MemoryStream()
                                rpt.C_TotalRowDe.text = dsRequestDetails.Tables(0).Rows.Count
                                ReportPrintClass.RPT_select(rpt, strDate, _SendCHDATE)

                                rpt.DataSource = dsRequestDetails.Tables(0)

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
                                Return 1
                            Else
                                Return 3
                            End If
                        Case "FORM4_1"
                            Dim rpt = New rpt3_ediFORM4_1
                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then
                                'POL
                                Dim m_stream As New System.IO.MemoryStream()
                                rpt.C_TotalRowDe.text = dsRequestDetails.Tables(0).Rows.Count
                                ReportPrintClass.RPT_select(rpt, strDate, _SendCHDATE)

                                rpt.DataSource = dsRequestDetails.Tables(0)
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
                                Return 1
                            Else
                                Return 3
                            End If
                        Case "FORM441"
                            Dim rpt = New rpt3_ediFORM441
                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then
                                'POL
                                Dim m_stream As New System.IO.MemoryStream()
                                rpt.C_TotalRowDe.text = dsRequestDetails.Tables(0).Rows.Count
                                ReportPrintClass.RPT_select(rpt, strDate, _SendCHDATE)

                                rpt.DataSource = dsRequestDetails.Tables(0)
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
                                Return 1
                            Else
                                Return 3
                            End If

                        Case "FORME_01"

                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then

                                Dim rpt As Object
                                If IsNewFormE = True Then
                                    rpt = New rpt3_ediFORME_01_VR
                                    ''ByTine 14-2-2561 แยก GW
                                    rpt.txtCheck_CaseRVCCount.text = CheckCount_RVC(Request.QueryString("SendCell"), SendFormTypes)
                                Else
                                    rpt = New rpt3_ediFORME_01_VR
                                End If

                                'POL
                                Dim m_stream As New System.IO.MemoryStream()
                                rpt.C_TotalRowDe.text = dsRequestDetails.Tables(0).Rows.Count
                                ReportPrintClass.RPT_select(rpt, strDate, _SendCHDATE)

                                'ByTine 12-10-2559 B2B
                                rpt._TempB2B = LoadDataForB2B(Request.QueryString("SendCell"))

                                rpt.DataSource = dsRequestDetails.Tables(0)
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
                                Return 1
                            Else
                                Return 3
                            End If
                        Case "FORME_ESS"

                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then

                                Dim rpt As Object
                                If IsNewFormE = True Then
                                    rpt = New rpt3_ediFORME_VR
                                    ''ByTine 14-2-2561 แยก GW
                                    rpt.txtCheck_CaseRVCCount.text = CheckCount_RVC(Request.QueryString("SendCell"), SendFormTypes)
                                Else
                                    rpt = New rpt3_ediFORME_VR
                                End If

                                'POL
                                Dim m_stream As New System.IO.MemoryStream()
                                rpt.C_TotalRowDe.text = dsRequestDetails.Tables(0).Rows.Count
                                ReportPrintClass.RPT_select(rpt, strDate, _SendCHDATE)

                                'ByTine 12-10-2559 B2B
                                rpt._TempB2B = LoadDataForB2B(Request.QueryString("SendCell"))

                                rpt.DataSource = dsRequestDetails.Tables(0)
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
                                Return 1
                            Else
                                Return 3
                            End If
                        Case "FORM4_3"
                            Dim rpt = New rpt3_ediFORMs4_3_VR
                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then
                                'POL
                                Dim m_stream As New System.IO.MemoryStream()
                                rpt.C_TotalRowDe.text = dsRequestDetails.Tables(0).Rows.Count
                                ReportPrintClass.RPT_select(rpt, strDate, _SendCHDATE)

                                rpt.DataSource = dsRequestDetails.Tables(0)
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
                                Return 1
                            Else
                                Return 3
                            End If
                        Case "FORM4_4"
                            Dim rpt = New rpt3_ediFORM4_4
                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then
                                'POL
                                Dim m_stream As New System.IO.MemoryStream()
                                rpt.txtTemp_SiteSend.text = Request.QueryString("SendSiteID")
                                rpt.C_TotalRowDe.text = dsRequestDetails.Tables(0).Rows.Count
                                ReportPrintClass.RPT_select(rpt, strDate, _SendCHDATE)

                                rpt.DataSource = dsRequestDetails.Tables(0)
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
                                Return 1
                            Else
                                Return 3
                            End If
                        Case "FORM44_01"
                            'Dim string_IP As String = Request.ServerVariables("REMOTE_ADDR")
                            'Label1.Text = string_IP
                            Dim rpt = New rpt3_ediFORM44_01
                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then
                                'POL
                                Dim m_stream As New System.IO.MemoryStream()
                                rpt.txtTemp_SiteSend.text = Request.QueryString("SendSiteID")
                                rpt.C_TotalRowDe.text = dsRequestDetails.Tables(0).Rows.Count
                                ReportPrintClass.RPT_select(rpt, strDate, _SendCHDATE)

                                rpt.DataSource = dsRequestDetails.Tables(0)
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
                                Return 1
                            Else
                                Return 3
                            End If
                        Case "FORMD_ESS"
                            'Dim string_IP As String = Request.ServerVariables("REMOTE_ADDR")
                            'Label1.Text = string_IP
                            Dim rpt = New rpt3_ediFORMD_ESS
                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then
                                'POL
                                Dim m_stream As New System.IO.MemoryStream()
                                rpt.txtTemp_SiteSend.text = Request.QueryString("SendSiteID")
                                rpt.C_TotalRowDe.text = dsRequestDetails.Tables(0).Rows.Count
                                ReportPrintClass.RPT_select(rpt, strDate, _SendCHDATE)

                                rpt.DataSource = dsRequestDetails.Tables(0)
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
                                Return 1
                            Else
                                Return 3
                            End If

                        Case "FORMAHK"
                            'Dim string_IP As String = Request.ServerVariables("REMOTE_ADDR")
                            'Label1.Text = string_IP
                            Dim rpt = New rpt3_ediFORMAHK_VR
                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then
                                'POL
                                Dim m_stream As New System.IO.MemoryStream()
                                rpt.txtTemp_SiteSend.text = Request.QueryString("SendSiteID")
                                rpt.C_TotalRowDe.text = dsRequestDetails.Tables(0).Rows.Count
                                ReportPrintClass.RPT_select(rpt, strDate, _SendCHDATE)

                                rpt.DataSource = dsRequestDetails.Tables(0)
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
                                Return 1
                            Else
                                Return 3
                            End If

                        Case "FORMTC_ESS"
                            'Dim string_IP As String = Request.ServerVariables("REMOTE_ADDR")
                            'Label1.Text = string_IP
                            Dim rpt = New rpt3_ediFORMTC_ESS_VR
                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then
                                'POL
                                Dim m_stream As New System.IO.MemoryStream()
                                rpt.txtTemp_SiteSend.text = Request.QueryString("SendSiteID")
                                rpt.C_TotalRowDe.text = dsRequestDetails.Tables(0).Rows.Count
                                ReportPrintClass.RPT_select(rpt, strDate, _SendCHDATE)

                                rpt.DataSource = dsRequestDetails.Tables(0)
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
                                Return 1
                            Else
                                Return 3
                            End If

                        Case "FORM4_5"
                            'Dim string_IP As String = Request.ServerVariables("REMOTE_ADDR")
                            'Label1.Text = string_IP
                            Dim rpt = New rpt3_ediFORM4_5_VR
                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then
                                'POL
                                Dim m_stream As New System.IO.MemoryStream()
                                rpt.txtTemp_SiteSend.text = Request.QueryString("SendSiteID")
                                rpt.C_TotalRowDe.text = dsRequestDetails.Tables(0).Rows.Count
                                ReportPrintClass.RPT_select(rpt, strDate, _SendCHDATE)

                                rpt.DataSource = dsRequestDetails.Tables(0)
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
                                Return 1
                            Else
                                Return 3
                            End If
                        Case "FORM4_6" 'เก่า
                            Dim rpt = New rpt3_ediFORM4_6
                            Dim dataReadRfcard As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "vi_Nr_Rfcard_GetAll", New SqlParameter("@card_id ", CommonUtility.Get_String(dsRequestDetails.Tables(0).Rows(0).Item("card_id"))))
                            Dim dataRead_Sum As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "sp_getSummaryPrintForm", New SqlParameter("@invh_run_auto ", CommonUtility.Get_String(Request.QueryString("SendCell"))))

                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then
                                If dataRead_Sum.Read Then
                                    rpt.txtSum_Gross_Weight.Text = dataRead_Sum.Item("Gross_Weight").ToString()
                                    rpt.txtSum_g_Unit_Desc.Text = dataRead_Sum.Item("g_Unit_Desc").ToString()

                                    dataRead_Sum.Close()
                                End If
                                'POL
                                Dim m_stream As New System.IO.MemoryStream()
                                rpt.txtTemp_SiteSend.text = Request.QueryString("SendSiteID")
                                rpt.C_TotalRowDe.text = dsRequestDetails.Tables(0).Rows.Count
                                ReportPrintClass.RPT_select(rpt, strDate, _SendCHDATE)

                                rpt.DataSource = dsRequestDetails.Tables(0)
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
                                Return 1
                            Else
                                Return 3
                            End If
                        Case "FORM4_61" 'ใหม่
                            Dim rpt = New rpt3_ediFORM4_61_VR
                            Dim dataReadRfcard As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "vi_Nr_Rfcard_GetAll", New SqlParameter("@card_id ", CommonUtility.Get_String(dsRequestDetails.Tables(0).Rows(0).Item("card_id"))))
                            Dim dataRead_Sum As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "sp_getSummaryPrintForm", New SqlParameter("@invh_run_auto ", CommonUtility.Get_String(Request.QueryString("SendCell"))))

                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then
                                'POL
                                Dim m_stream As New System.IO.MemoryStream()
                                'เงื่อนไขใหม่ ปรับแก้ วันที่ 03-12-2012 เกี่ยวกับ RVC สองอัน Case 2,8 ต้องแสดงมูลค่า ที่เหลือไม่แสดง
                                'begin RVC
                                rpt.txtCheck_CaseRVCCount.text = CheckCount_RVCByAJ(Request.QueryString("SendCell"), SendFormTypes)
                                rpt.txtDisplayUDS7.text = Check_DisplayUSD7(Request.QueryString("SendCell"))
                                'end RVC 
                                rpt.txtTemp_SiteSend.text = Request.QueryString("SendSiteID")
                                rpt.C_TotalRowDe.text = dsRequestDetails.Tables(0).Rows.Count
                                ReportPrintClass.RPT_select(rpt, strDate, _SendCHDATE)

                                'ByTine 12-10-2559 B2B
                                'rpt._TempB2B = LoadDataForB2B(Request.QueryString("SendCell"))

                                rpt.DataSource = dsRequestDetails.Tables(0)

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
                                Return 1
                            Else
                                Return 3
                            End If
                        Case "FORM4_81" 'เก่า
                            Dim rpt = New rpt3_ediFORM4_8
                            Dim dataReadRfcard As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "vi_Nr_Rfcard_GetAll", New SqlParameter("@card_id ", CommonUtility.Get_String(dsRequestDetails.Tables(0).Rows(0).Item("card_id"))))
                            Dim dataRead_Sum As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "sp_getSummaryPrintForm", New SqlParameter("@invh_run_auto ", CommonUtility.Get_String(Request.QueryString("SendCell"))))

                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then
                                If dataRead_Sum.Read Then
                                    rpt.txtSum_Gross_Weight.Text = dataRead_Sum.Item("Gross_Weight").ToString()
                                    rpt.txtSum_g_Unit_Desc.Text = dataRead_Sum.Item("g_Unit_Desc").ToString()

                                    dataRead_Sum.Close()
                                End If
                                'POL
                                Dim m_stream As New System.IO.MemoryStream()
                                'rpt.txtTemp_SiteSend.text = Request.QueryString("SendSiteID")
                                rpt.txtTemp_SiteSend.text = Request.QueryString("SendSiteID")
                                rpt.C_TotalRowDe.text = dsRequestDetails.Tables(0).Rows.Count
                                ReportPrintClass.RPT_select(rpt, strDate, _SendCHDATE)

                                rpt.DataSource = dsRequestDetails.Tables(0)
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
                                Return 1
                            Else
                                Return 3
                            End If
                        Case "FORM4_8" 'ใหม่
                            Dim rpt As Object

                            Dim dataReadRfcard As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "vi_Nr_Rfcard_GetAll", New SqlParameter("@card_id ", CommonUtility.Get_String(dsRequestDetails.Tables(0).Rows(0).Item("card_id"))))

                            Dim dataRead_Sum As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "sp_getSummaryPrintForm", New SqlParameter("@invh_run_auto ", CommonUtility.Get_String(Request.QueryString("SendCell"))))

                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then
                                If dataRead_Sum.Read Then
                                    Select Case Check_Form4_8(Request.QueryString("SendCell"))
                                        Case True 'ใหม่
                                            rpt = New rpt3_ediFORM4_8_VR
                                            'เงื่อนไขใหม่ ปรับแก้ วันที่ 03-12-2012 เกี่ยวกับ RVC สองอัน Case 2,8 ต้องแสดงมูลค่า ที่เหลือไม่แสดง
                                            'begin RVC
                                            rpt.txtCheck_CaseRVCCount.text = CheckCount_RVC(Request.QueryString("SendCell"), SendFormTypes)
                                            rpt.txtDisplayUDS7.text = Check_DisplayUSD7(Request.QueryString("SendCell"))
                                                'end RVC  

                                                'ByTine 12-10-2559 B2B
                                                'rpt._TempB2B = LoadDataForB2B(Request.QueryString("SendCell"))

                                        Case False 'เก่า
                                            rpt = New rpt3_ediFORM4_8_VR
                                    End Select

                                    rpt.txtSum_Gross_Weight.Text = dataRead_Sum.Item("Gross_Weight").ToString()
                                    rpt.txtSum_g_Unit_Desc.Text = dataRead_Sum.Item("g_Unit_Desc").ToString()

                                    dataRead_Sum.Close()
                                End If
                                'POL
                                Dim m_stream As New System.IO.MemoryStream()
                                rpt.txtTemp_SiteSend.text = Request.QueryString("SendSiteID")
                                rpt.C_TotalRowDe.text = dsRequestDetails.Tables(0).Rows.Count
                                ReportPrintClass.RPT_select(rpt, strDate, _SendCHDATE)

                                rpt.DataSource = dsRequestDetails.Tables(0)
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
                                Return 1
                            Else
                                Return 3
                            End If
                        Case "FORM4_9"
                            Dim rpt = New rpt3_ediFORM4_9_VR

                            Dim dataReadRfcard As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "vi_Nr_Rfcard_GetAll", New SqlParameter("@card_id ", CommonUtility.Get_String(dsRequestDetails.Tables(0).Rows(0).Item("card_id"))))

                            Dim dataRead_Sum As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "sp_getSummaryPrintForm", New SqlParameter("@invh_run_auto ", CommonUtility.Get_String(Request.QueryString("SendCell"))))

                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then
                                If dataRead_Sum.Read Then
                                    rpt.txtSum_Gross_Weight.Text = dataRead_Sum.Item("Gross_Weight").ToString()
                                    rpt.txtSum_g_Unit_Desc.Text = dataRead_Sum.Item("g_Unit_Desc").ToString()

                                    dataRead_Sum.Close()

                                    'ByTine B2B 17-03-2560
                                    rpt._TempB2B = LoadDataForB2B(Request.QueryString("SendCell"))

                                End If
                                'POL
                                Dim m_stream As New System.IO.MemoryStream()
                                rpt.C_TotalRowDe.text = dsRequestDetails.Tables(0).Rows.Count
                                ReportPrintClass.RPT_select(rpt, strDate, _SendCHDATE)

                                rpt.DataSource = dsRequestDetails.Tables(0)
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
                                Return 1
                            Else
                                Return 3
                            End If
                        Case "FORM4_91"
                            Dim rpt = New rpt3_ediFORM4_91

                            Dim dataReadRfcard As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "vi_Nr_Rfcard_GetAll", New SqlParameter("@card_id ", CommonUtility.Get_String(dsRequestDetails.Tables(0).Rows(0).Item("card_id"))))

                            Dim dataRead_Sum As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "sp_getSummaryPrintForm", New SqlParameter("@invh_run_auto ", CommonUtility.Get_String(Request.QueryString("SendCell"))))

                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then
                                If dataRead_Sum.Read Then
                                    rpt.txtSum_Gross_Weight.Text = dataRead_Sum.Item("Gross_Weight").ToString()
                                    rpt.txtSum_g_Unit_Desc.Text = dataRead_Sum.Item("g_Unit_Desc").ToString()

                                    dataRead_Sum.Close()
                                End If
                                'POL
                                Dim m_stream As New System.IO.MemoryStream()
                                rpt.txtTemp_SiteSend.text = Request.QueryString("SendSiteID")
                                rpt.C_TotalRowDe.text = dsRequestDetails.Tables(0).Rows.Count
                                ReportPrintClass.RPT_select(rpt, strDate, _SendCHDATE)

                                rpt.DataSource = dsRequestDetails.Tables(0)
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
                                Return 1
                            Else
                                Return 3
                            End If

                                ''ByTine 23-03-2559 AANZ ใหม่
                        Case "FORM4_911"
                            Dim rpt = New rpt3_ediFORM4_911_VR

                            Dim dataReadRfcard As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "vi_Nr_Rfcard_GetAll", New SqlParameter("@card_id ", CommonUtility.Get_String(dsRequestDetails.Tables(0).Rows(0).Item("card_id"))))

                            Dim dataRead_Sum As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "sp_getSummaryPrintForm", New SqlParameter("@invh_run_auto ", CommonUtility.Get_String(Request.QueryString("SendCell"))))

                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then
                                rpt.txtCheck_CaseRVCCount.text = CheckCount_RVC(Request.QueryString("SendCell"), SendFormTypes)
                                If dataRead_Sum.Read Then
                                    rpt.txtSum_Gross_Weight.Text = dataRead_Sum.Item("Gross_Weight").ToString()
                                    rpt.txtSum_g_Unit_Desc.Text = dataRead_Sum.Item("g_Unit_Desc").ToString()

                                    dataRead_Sum.Close()
                                End If
                                'POL
                                Dim m_stream As New System.IO.MemoryStream()
                                rpt.txtTemp_SiteSend.text = Request.QueryString("SendSiteID")
                                rpt.C_TotalRowDe.text = dsRequestDetails.Tables(0).Rows.Count
                                ReportPrintClass.RPT_select(rpt, strDate, _SendCHDATE)

                                rpt.DataSource = dsRequestDetails.Tables(0)
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
                                Return 1
                            Else
                                Return 3
                            End If

                        Case "FORM5"
                            Dim rpt = New rpt3_ediFORM5_VR

                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then
                                'POL
                                Dim m_stream As New System.IO.MemoryStream()
                                rpt.C_TotalRowDe.text = dsRequestDetails.Tables(0).Rows.Count
                                ReportPrintClass.RPT_select(rpt, strDate, _SendCHDATE)

                                rpt.DataSource = dsRequestDetails.Tables(0)
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
                                Return 1
                            Else
                                Return 3
                            End If
                        Case "FORM5_1", "FORMTP_ESS"
                            Dim rpt = New rpt3_ediFORM5_1_pr

                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then
                                'POL
                                Dim m_stream As New System.IO.MemoryStream()
                                rpt.C_TotalRowDe.text = dsRequestDetails.Tables(0).Rows.Count
                                ReportPrintClass.RPT_select(rpt, strDate, _SendCHDATE)

                                rpt.DataSource = dsRequestDetails.Tables(0)
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
                                Return 1
                            Else
                                Return 3
                            End If

                                ''ByTine 15-10-2558 เพิ่มฟอร์มใหม่ไทย-ชิลี
                        Case "FORM5_2"
                            Dim rpt = New rpt3_ediFORM5_2

                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then
                                'POL
                                Dim m_stream As New System.IO.MemoryStream()
                                rpt.C_TotalRowDe.text = dsRequestDetails.Tables(0).Rows.Count

                                ''ByTine 04-05-2559 เพิ่มเติมกรณีไม่ต้องการแสดง Inv ต่างประเทศ
                                If DBNull.Value.Equals(dsRequestDetails.Tables(0).Rows(0).Item("NoneDisplayInv10")) = True Then
                                    rpt.ChkNoneDisplayInv10.Checked = False
                                Else
                                    rpt.ChkNoneDisplayInv10.Checked = dsRequestDetails.Tables(0).Rows(0).Item("NoneDisplayInv10")
                                End If

                                ReportPrintClass.RPT_select(rpt, strDate, _SendCHDATE)

                                If _ChkTrueCopy = True Then
                                    rpt.PCheck_TrueCopy.visible = True
                                Else
                                    rpt.PCheck_TrueCopy.visible = False
                                End If

                                rpt.DataSource = dsRequestDetails.Tables(0)
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
                                Return 1
                            Else
                                Return 3
                            End If

                        Case "FORM6"
                            Dim rpt = New rpt3_ediFORM6_VR

                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then
                                'POL
                                Dim m_stream As New System.IO.MemoryStream()
                                rpt.C_TotalRowDe.text = dsRequestDetails.Tables(0).Rows.Count
                                ReportPrintClass.RPT_select(rpt, strDate, _SendCHDATE)

                                rpt.DataSource = dsRequestDetails.Tables(0)
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
                                Return 1
                            Else
                                Return 3
                            End If
                        Case "FORM7"
                            Dim rpt = New rpt3_ediFORM8_7
                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then
                                'POL
                                Dim m_stream As New System.IO.MemoryStream()
                                rpt.C_TotalRowDe.text = dsRequestDetails.Tables(0).Rows.Count
                                ReportPrintClass.RPT_select(rpt, strDate, _SendCHDATE)

                                rpt.DataSource = dsRequestDetails.Tables(0)
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
                                Return 1
                            Else
                                Return 3
                            End If
                        Case "FORM8"
                            Dim rpt = New rpt3_ediFORM8
                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then
                                'POL
                                Dim m_stream As New System.IO.MemoryStream()
                                rpt.C_TotalRowDe.text = dsRequestDetails.Tables(0).Rows.Count
                                ReportPrintClass.RPT_select(rpt, strDate, _SendCHDATE)

                                rpt.DataSource = dsRequestDetails.Tables(0)
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
                                Return 1
                            Else
                                Return 3
                            End If
                        Case "FORM9"
                            Dim rpt = New rpt3_ediFORM9 'rpt3_ediFORM7 'rpt3_ediFORM9
                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then
                                'POL
                                Dim m_stream As New System.IO.MemoryStream()
                                rpt.C_TotalRowDe.text = dsRequestDetails.Tables(0).Rows.Count
                                ReportPrintClass.RPT_select(rpt, strDate, _SendCHDATE)

                                rpt.DataSource = dsRequestDetails.Tables(0)
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
                                Return 1
                            Else
                                Return 3
                            End If
                        Case Else
                            Return 3
                    End Select
                End If
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        Else
            Return 2
        End If
    End Function

    Function View_ReEdi_A(ByVal dsRequestDetails_f) As Boolean
        Dim m_stream As New System.IO.MemoryStream()
        Dim rpt = New rpt3_ReEdi_A
        Dim rptTemp As New rpt3_ReEdi_Temp

        '========================================
        If dsRequestDetails_f.Tables(0).Rows.Count > 0 Then
            'POL
            rpt.Document.Printer.PrinterName = ""
            rpt.DataSource = dsRequestDetails_f.Tables(0)

            If _ISB2B = True Then
                ''เงื่อนไข B2B เอาตารางข้อมูล B2B มาต่อกับคำขอ
                ''===============================================================
                Dim _FormType As String = Module_CallBathEng.ConvertFormTypeForB2B(Request.QueryString("CellType"))

                rpt.Run(False)

                Dim npm As New SqlParameter("@invh_run_auto", Request.QueryString("SendCell"))
                Dim ds As New DataSet
                ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, "sp_b2b_Load_BalanceB2BItem", npm)

                If ds.Tables(0).Rows.Count > 0 Then
                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                        Dim rpt_B2B As New rpt3_ReEdi_B2B
                        rpt_B2B = Print_B2BData(_CompanyTaxno, ds.Tables(0).Rows(i).Item("B2BReferenceCode"), _FormType, _B2BCountry, Request.QueryString("SendCell"))
                        rpt_B2B.Document.Printer.PrinterName = ""
                        rpt_B2B.PageSettings.PaperKind = Printing.PaperKind.A4
                        rpt_B2B.Run(False)

                        For j As Integer = 0 To rpt_B2B.Document.Pages.Count - 1
                            rptTemp.Document.Pages.Add(rpt_B2B.Document.Pages(j))
                        Next
                    Next
                End If

                '========================

                'Report หลัก
                Dim a As Integer = 0
                Do While a <= rpt.Document.Pages.Count - 1
                    Dim Numpage0 As Integer = (rpt.Document.Pages.Count - 1) - a
                    rptTemp.Document.Pages.Insert(0, rpt.Document.Pages(Numpage0))
                    a += 1
                Loop
                ''===============================================================
            Else
                rpt.Run()
            End If

            If Me.PdfExport1 Is Nothing Then
                Me.PdfExport1 = New DataDynamics.ActiveReports.Export.Pdf.PdfExport
            End If

            If _ISB2B = True Then
                Me.PdfExport1.Export(rptTemp.Document, m_stream)
            Else
                Me.PdfExport1.Export(rpt.Document, m_stream)
            End If

            'Me.PdfExport1.Export(rpt.Document, m_stream)
            m_stream.Position = 0
            Response.ContentType = "application/pdf"
            Response.AddHeader("content-disposition", "inline; filename=MyExport.pdf")
            Response.BinaryWrite(m_stream.ToArray())
            Response.End()

            Return True
        Else
            Return False
        End If
    End Function

    Function View_ReEdi_ASW(ByVal dsRequestDetails_f) As Boolean
        Dim m_stream As New System.IO.MemoryStream()
        Dim rpt = New rpt3_ReEdi_A
        Dim rptInvoice As New rpt3_ReEdi_InvoiceList
        Dim rptTemp As New rpt3_ReEdi_Temp

        If dsRequestDetails_f.Tables(0).Rows.Count > 0 Then
            Try
                rpt.Document.Printer.PrinterName = ""
                rpt.DataSource = dsRequestDetails_f.Tables(0)

                Dim invoiceDatasource As DataSet = LoadASWInvoice(Request.QueryString("SendCell"))
                rptInvoice.DataSource = invoiceDatasource.Tables(0)
                rptInvoice.Document.Printer.PrinterName = ""
                rptInvoice.PageSettings.PaperKind = Printing.PaperKind.A4
                rptInvoice.Run(False)

                'รายการ Invoice
                For j As Integer = 0 To rptInvoice.Document.Pages.Count - 1
                    rptTemp.Document.Pages.Add(rptInvoice.Document.Pages(j))
                Next

                If _ISB2B = True Then
                    ''เงื่อนไข B2B เอาตารางข้อมูล B2B มาต่อกับคำขอ
                    ''===============================================================
                    Dim _FormType As String = Module_CallBathEng.ConvertFormTypeForB2B(Request.QueryString("CellType"))

                    rpt.Run(False)

                    Dim npm As New SqlParameter("@invh_run_auto", Request.QueryString("SendCell"))
                    Dim ds As New DataSet
                    ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, "sp_b2b_Load_BalanceB2BItem", npm)

                    If ds.Tables(0).Rows.Count > 0 Then
                        For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                            Dim rpt_B2B As New rpt3_ReEdi_B2B
                            rpt_B2B = Print_B2BData(_CompanyTaxno, ds.Tables(0).Rows(i).Item("B2BReferenceCode"), _FormType, _B2BCountry, Request.QueryString("SendCell"))
                            rpt_B2B.Document.Printer.PrinterName = ""
                            rpt_B2B.PageSettings.PaperKind = Printing.PaperKind.A4
                            rpt_B2B.Run(False)

                            For j As Integer = 0 To rpt_B2B.Document.Pages.Count - 1
                                rptTemp.Document.Pages.Add(rpt_B2B.Document.Pages(j))
                            Next
                        Next
                    End If

                    '========================

                    'Report หลัก
                    Dim a As Integer = 0
                    Do While a <= rpt.Document.Pages.Count - 1
                        Dim Numpage0 As Integer = (rpt.Document.Pages.Count - 1) - a
                        rptTemp.Document.Pages.Insert(0, rpt.Document.Pages(Numpage0))
                        a += 1
                    Loop
                    ''===============================================================
                Else
                    rpt.Run()
                    Dim a As Integer = 0
                    Do While a <= rpt.Document.Pages.Count - 1
                        Dim Numpage0 As Integer = (rpt.Document.Pages.Count - 1) - a
                        rptTemp.Document.Pages.Insert(0, rpt.Document.Pages(Numpage0))
                        a += 1
                    Loop
                End If


                If Me.PdfExport1 Is Nothing Then
                    Me.PdfExport1 = New DataDynamics.ActiveReports.Export.Pdf.PdfExport
                End If

                Me.PdfExport1.Export(rptTemp.Document, m_stream)
                'If _ISB2B = True Then
                '    Me.PdfExport1.Export(rptTemp.Document, m_stream)
                'Else
                '    Me.PdfExport1.Export(rpt.Document, m_stream)
                'End If

                m_stream.Position = 0
                Response.ContentType = "application/pdf"
                Response.AddHeader("content-disposition", "inline; filename=MyExport.pdf")
                Response.BinaryWrite(m_stream.ToArray())
                Response.End()
            Catch eRunReport As DataDynamics.ActiveReports.ReportException
                ' Failure running report, just report the error to the user:
                Response.Clear()
                Response.Write("<h1>Error running report:</h1>")
                Response.Write(eRunReport.ToString())
                'Return
            End Try
            Return True
        Else
            Return False
        End If

    End Function
    Function View_ReEdi_A_24(ByVal dsRequestDetails_f) As Boolean
        Dim m_stream As New System.IO.MemoryStream()
        Dim rpt = New rpt3_ReEdi_A_24

        '========================================
        If dsRequestDetails_f.Tables(0).Rows.Count > 0 Then

            'POL
            rpt.DataSource = dsRequestDetails_f.Tables(0)
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

            Return True
        Else
            Return False
        End If
    End Function

    Function View_ReEdi_CO(ByVal dsRequestDetails_f) As Boolean
        Dim m_stream As New System.IO.MemoryStream()
        Dim rpt = New rpt3_ReEdi_CO
        Dim rptTemp As New rpt3_ReEdi_Temp

        '========================================
        If dsRequestDetails_f.Tables(0).Rows.Count > 0 Then
            'POL
            rpt.Document.Printer.PrinterName = ""
            rpt.DataSource = dsRequestDetails_f.Tables(0)

            If _ISB2B = True Then
                ''เงื่อนไข B2B เอาตารางข้อมูล B2B มาต่อกับคำขอ
                ''===============================================================
                Dim _FormType As String = Module_CallBathEng.ConvertFormTypeForB2B(Request.QueryString("CellType"))

                rpt.Run(False)

                Dim npm As New SqlParameter("@invh_run_auto", Request.QueryString("SendCell"))
                Dim ds As New DataSet
                ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, "sp_b2b_Load_BalanceB2BItem", npm)

                If ds.Tables(0).Rows.Count > 0 Then
                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                        Dim rpt_B2B As New rpt3_ReEdi_B2B
                        rpt_B2B = Print_B2BData(_CompanyTaxno, ds.Tables(0).Rows(i).Item("B2BReferenceCode"), _FormType, _B2BCountry, Request.QueryString("SendCell"))
                        rpt_B2B.Document.Printer.PrinterName = ""
                        rpt_B2B.PageSettings.PaperKind = Printing.PaperKind.A4
                        rpt_B2B.Run(False)

                        For j As Integer = 0 To rpt_B2B.Document.Pages.Count - 1
                            rptTemp.Document.Pages.Add(rpt_B2B.Document.Pages(j))
                        Next
                    Next
                End If

                '========================

                'Report หลัก
                Dim a As Integer = 0
                Do While a <= rpt.Document.Pages.Count - 1
                    Dim Numpage0 As Integer = (rpt.Document.Pages.Count - 1) - a
                    rptTemp.Document.Pages.Insert(0, rpt.Document.Pages(Numpage0))
                    a += 1
                Loop
                ''===============================================================
            Else
                rpt.Run()
            End If

            If Me.PdfExport1 Is Nothing Then
                Me.PdfExport1 = New DataDynamics.ActiveReports.Export.Pdf.PdfExport
            End If

            If _ISB2B = True Then
                Me.PdfExport1.Export(rptTemp.Document, m_stream)
            Else
                Me.PdfExport1.Export(rpt.Document, m_stream)
            End If

            'Me.PdfExport1.Export(rpt.Document, m_stream)
            m_stream.Position = 0
            Response.ContentType = "application/pdf"
            Response.AddHeader("content-disposition", "inline; filename=MyExport.pdf")
            Response.BinaryWrite(m_stream.ToArray())
            Response.End()

            Return True
        Else
            Return False
        End If
    End Function
    Sub LoadMulti_Re_Form2_1(ByVal Multi_Cardid As String, ByVal Muti_INVH_RUN_AUTO As String, ByVal Muti_SiteId As String)
        Dim m_stream As New System.IO.MemoryStream()

        Dim rpt1 As rpt3_ReEdi_FormST6_1 = New rpt3_ReEdi_FormST6_1
        rpt1.DataSource = GetDataLoad_EDI_ReForm2_1(Muti_INVH_RUN_AUTO, Muti_SiteId)
        '====================
        rpt1.Run(False)
        If Me.PdfExport1 Is Nothing Then
            Me.PdfExport1 = New DataDynamics.ActiveReports.Export.Pdf.PdfExport
        End If
        Me.PdfExport1.Export(rpt1.Document, m_stream)
        m_stream.Position = 0
        Response.ContentType = "application/pdf"
        Response.AddHeader("content-disposition", "inline; filename=MyExport.pdf")
        Response.BinaryWrite(m_stream.ToArray())
        Response.End()
    End Sub

    Private Sub InitializeComponent()
        Me.PdfExport1 = New DataDynamics.ActiveReports.Export.Pdf.PdfExport

    End Sub

#Region "   Code เรียก Form44   "
    Function View_ReEdi_A44(ByVal dsRequestDetails_f) As Boolean
        Dim m_stream As New System.IO.MemoryStream()
        Dim rpt = New rpt3_ReEdi_A44

        '========================================
        If dsRequestDetails_f.Tables(0).Rows.Count > 0 Then
            'POL
            rpt.DataSource = dsRequestDetails_f.Tables(0)
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

            Return True
        Else
            Return False
        End If
    End Function
#End Region

#Region "   Code เรียก Form441   "
    Function View_ReEdi_A441(ByVal dsRequestDetails_f) As Boolean
        Dim m_stream As New System.IO.MemoryStream()
        Dim rpt = New rpt3_ediFORM441

        '========================================
        If dsRequestDetails_f.Tables(0).Rows.Count > 0 Then
            'POL
            rpt.DataSource = dsRequestDetails_f.Tables(0)
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

            Return True
        Else
            Return False
        End If
    End Function
#End Region

#Region "B2B"
#Region "BackToBack"
    Function Print_B2BData(ByVal _ComTax As String, ByVal _b2bRefno As String, ByVal _formtype As String, ByVal _b2bCountry As String, ByVal invh_run_auto As String)
        Try
            Dim rpt_B2BData As New rpt3_ReEdi_B2B
            Dim npm As SqlParameter
            'npm(0) = New SqlParameter("@CompanyTaxno", _ComTax.Trim)
            'npm(1) = New SqlParameter("@reference_code", _b2bRefno.Trim)
            'npm(2) = New SqlParameter("@form_type", _formtype.Trim)
            'npm(3) = New SqlParameter("@import_country", _b2bCountry.Trim)
            npm = New SqlParameter("@invh_run_auto", invh_run_auto)

            Dim ds As New DataSet
            'ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, "sp_b2b_import_product_Load_For_PrintRequest_Back", npm)
            ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, "sp_b2b_import_product_Load_For_Request_Back", npm)

            'If ds.Tables(0).Rows.Count > 0 Then
            'rpt_B2BData.txtCompanyName.Text = ds.Tables(0).Rows(0).Item("CompanyName") & " " & "(" & ds.Tables(0).Rows(0).Item("Company_tax_id") & ")"
            'rpt_B2BData.txtFormName.Text = _FullFormName
            'rpt_B2BData.txtImportCountry.Text = ds.Tables(0).Rows(0).Item("country_name")
            'rpt_B2BData.txtRefNo.Text = ds.Tables(0).Rows(0).Item("reference_code")
            'rpt_B2BData.txtIssueDate.Text = ds.Tables(0).Rows(0).Item("import_date")
            'rpt_B2BData.txtInvHrunauto.Text = "เลขที่อ้างอิง : " & invh_run_auto


            'End If

            rpt_B2BData.DataSource = ds.Tables(0)

            Return rpt_B2BData

        Catch ex As Exception
            Return Nothing
        End Try
    End Function

#End Region
#End Region

    Private Function LoadASWInvoice(invh_run_auto As String) As DataSet
        Dim ds As New DataSet

        Try
            Dim cmd As String = "sp_ASW2_get_FormInvoice_Select"
            Dim prm As New SqlParameter("@invh_run_auto", invh_run_auto)
            ds = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, cmd, prm)

        Catch ex As Exception

        End Try

        Return ds
    End Function
End Class