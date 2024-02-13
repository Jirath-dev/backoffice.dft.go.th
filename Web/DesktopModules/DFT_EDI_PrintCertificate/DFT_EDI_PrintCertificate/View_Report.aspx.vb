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

Imports System.Drawing

Imports System.Threading
Imports System.Globalization
Imports ReportPrintClass
Partial Public Class View_Report
    Inherits System.Web.UI.Page
    Dim strEDIConn As String = ConfigurationManager.ConnectionStrings("OriginConnection").ConnectionString
    Dim counter As Integer
    Friend WithEvents PdfExport1 As DataDynamics.ActiveReports.Export.Pdf.PdfExport

    Dim _ISB2B As Boolean
    Dim _CompanyTaxno, _FullFormName, _B2BRefNo, _B2BCountry As String

    Private dt As DataTable
#Region "   Code Test   "
    Function LoadRequestFormForPrint_Test() As DataTable
        Try
            'objConn = New SqlConnection(strEDIConn)
            Dim ds As New DataSet

            ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, "vi_form4_edi_printFormBar_NewDS", _
                                    New SqlParameter("@INVH_RUN_AUTO", Request.QueryString("SendCell")), _
                                    New SqlParameter("@SITE_ID", "ST-001"))


            If ds.Tables(0).Rows.Count > 0 Then
                Dim dr As DataRow
                dt.Columns.Add(New DataColumn("111", GetType(String)))
                dt.Columns.Add(New DataColumn("222", GetType(String)))
                dt.Columns.Add(New DataColumn("333", GetType(String)))
                dt.Columns.Add(New DataColumn("444", GetType(String)))

                dr = dt.NewRow()

                For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                    If i = 0 Then
                        dr("111") = CommonUtility.Get_String(ds.Tables(0).Rows(i).Item("PRODUCT_NAME"))
                        dr("222") = CommonUtility.Get_String(ds.Tables(0).Rows(i).Item("TARIFF_CODE"))
                        dr("333") = CommonUtility.Get_String(ds.Tables(0).Rows(i).Item("NET_WEIGHT"))
                        dr("444") = CommonUtility.Get_String(ds.Tables(0).Rows(i).Item("FOB_AMT"))
                    Else
                        dr("111") &= vbNewLine & CommonUtility.Get_String(ds.Tables(0).Rows(i).Item("PRODUCT_NAME"))
                        dr("222") &= vbNewLine & CommonUtility.Get_String(ds.Tables(0).Rows(i).Item("TARIFF_CODE"))
                        dr("333") &= vbNewLine & CommonUtility.Get_String(ds.Tables(0).Rows(i).Item("NET_WEIGHT"))
                        dr("444") &= vbNewLine & CommonUtility.Get_String(ds.Tables(0).Rows(i).Item("FOB_AMT"))

                    End If
                Next
                dt.Rows.Add(dr)

                Return dt
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Function

#End Region
    
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        '===========set Date thai==================================
        'Thread.CurrentThread.CurrentCulture = New CultureInfo("th-TH")
        'Thread.CurrentThread.CurrentUICulture = New CultureInfo("th-TH")
        '=============================================
        'If Request.QueryString("CheckUserPage") = "admin" Then
        Select Case LoadReportView(Request.QueryString("CellType"), Request.QueryString("SendSiteID"), CInt(Request.QueryString("radioForm")), Request.QueryString("PrintFlag"))
            Case 1
                Page.SetFocus(Page)
                Page.Title = "�ʴ���§ҹ"

                '0=�Ӣ�,1=˹ѧ����Ѻ�ͧ
                checkUpdate_user(Request.QueryString("SendCell"), Request.QueryString("CheckUserPage"), Request.QueryString("radioForm"))

                WebViewer_Report.Focus()
            Case 2
                If updatePrintTotal(Request.QueryString("SendCell"), CStr(1)) = True Then
                    '0=�Ӣ�,1=˹ѧ����Ѻ�ͧ
                    checkUpdate_user(Request.QueryString("SendCell"), Request.QueryString("CheckUserPage"), Request.QueryString("radioForm"))
                End If
                lblErrorReport.Text = "�������¡���Թ���"
            Case 3
                lblErrorReport.Text = "��辺��¡��"
        End Select


    End Sub

#Region "   Code ���¡ �Ӣ� Form2_1   "
    Sub LoadMulti_Re_Form2_1(ByVal Multi_Cardid As String, ByVal Muti_INVH_RUN_AUTO As String, ByVal Muti_SiteId As String)
        Dim rpt1 As rpt3_ReEdi_FormST6_1 = New rpt3_ReEdi_FormST6_1

        '�������Ѻ Export Pdf
        Dim oPDF As DataDynamics.ActiveReports.Export.Pdf.PdfExport = New DataDynamics.ActiveReports.Export.Pdf.PdfExport()
        '�������Ѻ ���¡ View ��Ẻ PDF
        Me.WebViewer_Report.ViewerType = DataDynamics.ActiveReports.Web.ViewerType.AcrobatReader

        rpt1.DataSource = GetDataLoad_EDI_ReForm2_1(Muti_INVH_RUN_AUTO, Muti_SiteId)
        '====================
        rpt1.Run(False)
        '=================

        '���¡�ٷ�� View Ẻ PDF
        WebViewer_Report.Height = 700
        WebViewer_Report.Report = rpt1

        '��Ҩ� Export �� PDF
        'oPDF.Export(rpt0.Document, "C:\CombinedPDF.PDF")
    End Sub
#End Region

#Region "   Code ���¡ Form44   "
    Function View_ReEdi_A44(ByVal dsRequestDetails_f) As Boolean
        Dim rpt = New rpt3_ReEdi_A44

        Me.WebViewer_Report.ViewerType = DataDynamics.ActiveReports.Web.ViewerType.AcrobatReader
        'Dim dataReadRfcard As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "vi_Nr_Rfcard_GetAll", New SqlParameter("@card_id ", CommonUtility.Get_String(dsRequestDetails_f.Tables(0).Rows(0).Item("card_id"))))

        'Dim dataRead_Sum As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "sp_getSummaryPrintForm", New SqlParameter("@invh_run_auto ", CommonUtility.Get_String(Request.QueryString("SendCell"))))

        '========================================
        If dsRequestDetails_f.Tables(0).Rows.Count > 0 Then
            'If dataRead_Sum.Read Then
            '    'rpt.txtSum_Gross_Weight.Text = dataRead_Sum.Item("Gross_Weight").ToString()
            '    'rpt.txtSum_g_Unit_Desc.Text = dataRead_Sum.Item("g_Unit_Desc").ToString()

            '    dataRead_Sum.Close()
            'End If
            'set �������Ѻ����ͧ Temp ���
            WebViewer_Report.ClearCachedReport()

            WebViewer_Report.Report = rpt
            WebViewer_Report.Height = 700

            WebViewer_Report.Report.DataSource = dsRequestDetails_f.Tables(0)

            Page.SetFocus(Page)
            WebViewer_Report.Focus()
            Return True
        Else
            Return False
        End If
    End Function
#End Region

#Region "   Code ���¡ Form441   "
    Function View_ReEdi_A441(ByVal dsRequestDetails) As Boolean
        'view

        Dim rpt = New rpt3_ediFORM441
        Me.WebViewer_Report.ViewerType = DataDynamics.ActiveReports.Web.ViewerType.AcrobatReader

        'Dim dataReadRfcard As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "vi_Nr_Rfcard_GetAll", New SqlParameter("@card_id ", CommonUtility.Get_String(dsRequestDetails.Tables(0).Rows(0).Item("card_id"))))

        ' Dim dataRead_Sum As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "sp_getSummaryPrintForm", New SqlParameter("@invh_run_auto ", CommonUtility.Get_String(Request.QueryString("SendCell"))))

        If dsRequestDetails.Tables(0).Rows.Count > 0 Then
            'If dataRead_Sum.Read Then
            '    'rpt.txtSum_Gross_Weight.Text = dataRead_Sum.Item("Gross_Weight").ToString()
            '    'rpt.txtSum_g_Unit_Desc.Text = dataRead_Sum.Item("g_Unit_Desc").ToString()

            '    dataRead_Sum.Close()
            'End If
            rpt.C_TotalRowDe.text = dsRequestDetails.Tables(0).Rows.Count
            WebViewer_Report.Report = rpt
            WebViewer_Report.Height = 700


            WebViewer_Report.Report.DataSource = dsRequestDetails.Tables(0) 'ReportPrintClass.GetDataLoadTrd("20090416-000001", "ST-001") 'dsRequestDetail.Tables(0)
            Page.SetFocus(Page)
            WebViewer_Report.Focus()
            Return 1
        Else
            Return 3
        End If
    End Function
#End Region

#Region "   Code View �Ӣ� ALL Form   "
    Function View_ReEdi_A(ByVal dsRequestDetails_f) As Boolean
        Dim m_stream As New System.IO.MemoryStream()
        Dim rpt = New rpt3_ReEdi_A
        Dim rptTemp As New rpt3_ReEdi_Temp

        If dsRequestDetails_f.Tables(0).Rows.Count > 0 Then
            Try
                rpt.Document.Printer.PrinterName = ""
                rpt.DataSource = dsRequestDetails_f.Tables(0)

                If _ISB2B = True Then
                    ''���͹� B2B ��ҵ��ҧ������ B2B �ҵ�͡Ѻ�Ӣ�
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

                    'Report ��ѡ
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
        ''Dim rpt = New rpt3_ReEdi_A

        ''Me.WebViewer_Report.ViewerType = DataDynamics.ActiveReports.Web.ViewerType.AcrobatReader
        ' ''Dim dataReadRfcard As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "vi_Nr_Rfcard_GetAll", New SqlParameter("@card_id ", CommonUtility.Get_String(dsRequestDetails_f.Tables(0).Rows(0).Item("card_id"))))

        ' ''Dim dataRead_Sum As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "sp_getSummaryPrintForm", New SqlParameter("@invh_run_auto ", CommonUtility.Get_String(Request.QueryString("SendCell"))))

        ' ''========================================
        ''If dsRequestDetails_f.Tables(0).Rows.Count > 0 Then
        ''    'If dataRead_Sum.Read Then
        ''    '    'rpt.txtSum_Gross_Weight.Text = dataRead_Sum.Item("Gross_Weight").ToString()
        ''    '    'rpt.txtSum_g_Unit_Desc.Text = dataRead_Sum.Item("g_Unit_Desc").ToString()

        ''    '    dataRead_Sum.Close()
        ''    'End If
        ''    'set �������Ѻ����ͧ Temp ���
        ''    WebViewer_Report.ClearCachedReport()

        ''    WebViewer_Report.Report = rpt
        ''    WebViewer_Report.Height = 700

        ''    WebViewer_Report.Report.DataSource = dsRequestDetails_f.Tables(0)

        ''    Page.SetFocus(Page)
        ''    WebViewer_Report.Focus()
        ''    Return True
        ''Else
        ''    Return False
        ''End If
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

                '��¡�� Invoice
                For j As Integer = 0 To rptInvoice.Document.Pages.Count - 1
                    rptTemp.Document.Pages.Add(rptInvoice.Document.Pages(j))
                Next

                If _ISB2B = True Then
                    ''���͹� B2B ��ҵ��ҧ������ B2B �ҵ�͡Ѻ�Ӣ�
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

                    'Report ��ѡ
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

#End Region

#Region "   Code ���¡ View Form2_4   "
    '��������� form2_4
    Function View_ReEdi_A_24(ByVal dsRequestDetails_f) As Boolean
        Dim rpt = New rpt3_ReEdi_A_24

        Me.WebViewer_Report.ViewerType = DataDynamics.ActiveReports.Web.ViewerType.AcrobatReader
        'Dim dataReadRfcard As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "vi_Nr_Rfcard_GetAll", New SqlParameter("@card_id ", CommonUtility.Get_String(dsRequestDetails_f.Tables(0).Rows(0).Item("card_id"))))

        'Dim dataRead_Sum As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "sp_getSummaryPrintForm", New SqlParameter("@invh_run_auto ", CommonUtility.Get_String(Request.QueryString("SendCell"))))

        '========================================
        If dsRequestDetails_f.Tables(0).Rows.Count > 0 Then
            'If dataRead_Sum.Read Then
            '    'rpt.txtSum_Gross_Weight.Text = dataRead_Sum.Item("Gross_Weight").ToString()
            '    'rpt.txtSum_g_Unit_Desc.Text = dataRead_Sum.Item("g_Unit_Desc").ToString()

            '    dataRead_Sum.Close()
            'End If
            'set �������Ѻ����ͧ Temp ���
            WebViewer_Report.ClearCachedReport()

            WebViewer_Report.Report = rpt
            WebViewer_Report.Height = 700

            WebViewer_Report.Report.DataSource = dsRequestDetails_f.Tables(0)

            Page.SetFocus(Page)
            WebViewer_Report.Focus()
            Return True
        Else
            Return False
        End If
    End Function
#End Region

#Region "   Code Funcion Main ��ѡ   "
    Function LoadReportView(ByVal SendFormTypes As String, ByVal SiteID As String, ByVal radioForms As Integer, ByVal PrintFlag As String) As Integer
        Dim dsRequestDetails As New DataSet

        '============== Part Report ==============
        Dim cmd As String = "vi_form4_edi_printFormBar_NewDS"
        Select Case SendFormTypes
            Case "FORM44_01", "FORM44_02"
                cmd = "vi_form4_edi_printFormBar_NewDS_ASW"
        End Select

        dsRequestDetails = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, cmd,
                        New SqlParameter("@INVH_RUN_AUTO", Request.QueryString("SendCell")),
                        New SqlParameter("@SITE_ID", SiteID))

        If dsRequestDetails.Tables(0).Rows.Count > 0 Then

            ''=======================================
            ''ByTine 12-09-2559 ����Ѻ B2B
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

            ''=======================================

            '========================================
            Try
                If radioForms = 0 Then
                    'view 
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
                        Case "FORM2"
                            If View_ReEdi_A(dsRequestDetails) = True Then
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
                        Case "FORM2_2"
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

                            ''ByTine 28-11-2559 ������������  FORM2_5 CO (���) / FORM2_6 CO (����)
                        Case "FORM2_5", "FORM2_6"
                            If View_ReEdi_A(dsRequestDetails) = True Then
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
                            'FORM4		����� �� (ATIGA) ��� (2 �����)
                            'FORM44_4	����� �� (ATIGA) ���� (7 �����)
                            'FORM44_44	����� �� (ATIGA) Attach Sheet ö¹�� ���� (7 �����)
                            'FORM441_4	����� �� AICO Attach Sheet ö¹�� ���� (7 �����)
                        Case "FORM4", "FORM44_4", "FORM44_44", "FORM441_4"
                            If View_ReEdi_A(dsRequestDetails) = True Then
                                Return 1
                            Else
                                Return 3
                            End If
                        Case "FORM44_01", "FORM44_02"
                            If View_ReEdi_ASW(dsRequestDetails) = True Then
                                Return 1
                            Else
                                Return 3
                            End If
                            'FORM44		����� �� (ATIGA) Attach Sheet ö¹�� ��� (2 �����)
                        Case "FORM44"
                            If View_ReEdi_A44(dsRequestDetails) = True Then
                                Return 1
                            Else
                                Return 3
                            End If
                            'FORM4_1		����� �� AICO ��� (2 �����)
                            'FORM44_41	����� �� AICO ���� (7 �����)
                        Case "FORM4_1", "FORM44_41"
                            If View_ReEdi_A(dsRequestDetails) = True Then
                                Return 1
                            Else
                                Return 3
                            End If
                            'FORM441		����� �� AICO Attach Sheet ö¹�� ��� (2 �����)
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
                        Case "FORM4_6" '���
                            If View_ReEdi_A(dsRequestDetails) = True Then
                                Return 1
                            Else
                                Return 3
                            End If
                        Case "FORM4_61" '����
                            If View_ReEdi_A(dsRequestDetails) = True Then
                                Return 1
                            Else
                                Return 3
                            End If
                        Case "FORM4_81" '���
                            If View_ReEdi_A(dsRequestDetails) = True Then
                                Return 1
                            Else
                                Return 3
                            End If
                        Case "FORM4_8" '����
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
                        Case "FORM4_91", "FORM4_911"
                            If View_ReEdi_A(dsRequestDetails) = True Then
                                Return 1
                            Else
                                Return 3
                            End If

                            ''ByTine 08-10-2558 �����������-���� (FORM5_2)
                        Case "FORM5", "FORM5_1", "FORM5_2"
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
                    'view ˹ѧ����Ѻ�ͧ
                    Select Case SendFormTypes
                        Case "FORM1"
                            'view

                            Dim rpt = New rpt3_ediFORM1
                            Me.WebViewer_Report.ViewerType = DataDynamics.ActiveReports.Web.ViewerType.AcrobatReader

                            'Dim dataReadRfcard As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "vi_Nr_Rfcard_GetAll", New SqlParameter("@card_id ", CommonUtility.Get_String(dsRequestDetails.Tables(0).Rows(0).Item("card_id"))))

                            'Dim dataRead_Sum As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "sp_getSummaryPrintForm", New SqlParameter("@invh_run_auto ", CommonUtility.Get_String(Request.QueryString("SendCell"))))

                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then
                                'If dataRead_Sum.Read Then
                                '    'rpt.txtSum_Gross_Weight.Text = dataRead_Sum.Item("Gross_Weight").ToString()
                                '    'rpt.txtSum_g_Unit_Desc.Text = dataRead_Sum.Item("g_Unit_Desc").ToString()

                                '    dataRead_Sum.Close()
                                'End If
                                rpt.C_TotalRowDe.text = dsRequestDetails.Tables(0).Rows.Count
                                WebViewer_Report.Report = rpt
                                WebViewer_Report.Height = 700


                                WebViewer_Report.Report.DataSource = dsRequestDetails.Tables(0) 'ReportPrintClass.GetDataLoadTrd("20090416-000001", "ST-001") 'dsRequestDetail.Tables(0)
                                Page.SetFocus(Page)
                                WebViewer_Report.Focus()
                                Return 1
                            Else
                                Return 3
                            End If

                        Case "FORMRussia"
                            'view

                            Dim rpt = New rpt3_ediFORM1RU
                            Me.WebViewer_Report.ViewerType = DataDynamics.ActiveReports.Web.ViewerType.AcrobatReader

                            'Dim dataReadRfcard As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "vi_Nr_Rfcard_GetAll", New SqlParameter("@card_id ", CommonUtility.Get_String(dsRequestDetails.Tables(0).Rows(0).Item("card_id"))))

                            'Dim dataRead_Sum As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "sp_getSummaryPrintForm", New SqlParameter("@invh_run_auto ", CommonUtility.Get_String(Request.QueryString("SendCell"))))

                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then
                                'If dataRead_Sum.Read Then
                                '    'rpt.txtSum_Gross_Weight.Text = dataRead_Sum.Item("Gross_Weight").ToString()
                                '    'rpt.txtSum_g_Unit_Desc.Text = dataRead_Sum.Item("g_Unit_Desc").ToString()

                                '    dataRead_Sum.Close()
                                'End If
                                rpt.C_TotalRowDe.text = dsRequestDetails.Tables(0).Rows.Count
                                WebViewer_Report.Report = rpt
                                WebViewer_Report.Height = 700


                                WebViewer_Report.Report.DataSource = dsRequestDetails.Tables(0) 'ReportPrintClass.GetDataLoadTrd("20090416-000001", "ST-001") 'dsRequestDetail.Tables(0)
                                Page.SetFocus(Page)
                                WebViewer_Report.Focus()
                                Return 1
                            Else
                                Return 3
                            End If

                        Case "FORM1_1"
                            'view

                            Dim rpt = New rpt3_ediFORM1_1
                            Me.WebViewer_Report.ViewerType = DataDynamics.ActiveReports.Web.ViewerType.AcrobatReader

                            'Dim dataReadRfcard As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "vi_Nr_Rfcard_GetAll", New SqlParameter("@card_id ", CommonUtility.Get_String(dsRequestDetails.Tables(0).Rows(0).Item("card_id"))))

                            'Dim dataRead_Sum As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "sp_getSummaryPrintForm", New SqlParameter("@invh_run_auto ", CommonUtility.Get_String(Request.QueryString("SendCell"))))

                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then
                                'If dataRead_Sum.Read Then
                                '    'rpt.txtSum_Gross_Weight.Text = dataRead_Sum.Item("Gross_Weight").ToString()
                                '    'rpt.txtSum_g_Unit_Desc.Text = dataRead_Sum.Item("g_Unit_Desc").ToString()

                                '    dataRead_Sum.Close()
                                'End If
                                rpt.C_TotalRowDe.text = dsRequestDetails.Tables(0).Rows.Count
                                WebViewer_Report.Report = rpt
                                WebViewer_Report.Height = 700


                                WebViewer_Report.Report.DataSource = dsRequestDetails.Tables(0) 'ReportPrintClass.GetDataLoadTrd("20090416-000001", "ST-001") 'dsRequestDetail.Tables(0)
                                Page.SetFocus(Page)
                                WebViewer_Report.Focus()
                                Return 1
                            Else
                                Return 3
                            End If
                        Case "FORM1_2"
                            'view

                            Dim rpt = New rpt3_ediFORM1_2
                            Me.WebViewer_Report.ViewerType = DataDynamics.ActiveReports.Web.ViewerType.AcrobatReader

                            'Dim dataReadRfcard As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "vi_Nr_Rfcard_GetAll", New SqlParameter("@card_id ", CommonUtility.Get_String(dsRequestDetails.Tables(0).Rows(0).Item("card_id"))))

                            'Dim dataRead_Sum As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "sp_getSummaryPrintForm", New SqlParameter("@invh_run_auto ", CommonUtility.Get_String(Request.QueryString("SendCell"))))

                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then
                                'If dataRead_Sum.Read Then
                                '    'rpt.txtSum_Gross_Weight.Text = dataRead_Sum.Item("Gross_Weight").ToString()
                                '    'rpt.txtSum_g_Unit_Desc.Text = dataRead_Sum.Item("g_Unit_Desc").ToString()

                                '    dataRead_Sum.Close()
                                'End If
                                rpt.C_TotalRowDe.text = dsRequestDetails.Tables(0).Rows.Count
                                WebViewer_Report.Report = rpt
                                WebViewer_Report.Height = 700


                                WebViewer_Report.Report.DataSource = dsRequestDetails.Tables(0) 'ReportPrintClass.GetDataLoadTrd("20090416-000001", "ST-001") 'dsRequestDetail.Tables(0)
                                Page.SetFocus(Page)
                                WebViewer_Report.Focus()
                                Return 1
                            Else
                                Return 3
                            End If
                        Case "FORM1_3"
                            'view

                            Dim rpt = New rpt3_ediFORM1_3
                            Me.WebViewer_Report.ViewerType = DataDynamics.ActiveReports.Web.ViewerType.AcrobatReader

                            'Dim dataReadRfcard As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "vi_Nr_Rfcard_GetAll", New SqlParameter("@card_id ", CommonUtility.Get_String(dsRequestDetails.Tables(0).Rows(0).Item("card_id"))))

                            'Dim dataRead_Sum As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "sp_getSummaryPrintForm", New SqlParameter("@invh_run_auto ", CommonUtility.Get_String(Request.QueryString("SendCell"))))

                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then
                                'If dataRead_Sum.Read Then
                                '    'rpt.txtSum_Gross_Weight.Text = dataRead_Sum.Item("Gross_Weight").ToString()
                                '    'rpt.txtSum_g_Unit_Desc.Text = dataRead_Sum.Item("g_Unit_Desc").ToString()

                                '    dataRead_Sum.Close()
                                'End If
                                rpt.C_TotalRowDe.text = dsRequestDetails.Tables(0).Rows.Count
                                WebViewer_Report.Report = rpt
                                WebViewer_Report.Height = 700


                                WebViewer_Report.Report.DataSource = dsRequestDetails.Tables(0) 'ReportPrintClass.GetDataLoadTrd("20090416-000001", "ST-001") 'dsRequestDetail.Tables(0)
                                Page.SetFocus(Page)
                                WebViewer_Report.Focus()
                                Return 1
                            Else
                                Return 3
                            End If
                        Case "FORM1_4"
                            'view

                            Dim rpt = New rpt3_ediFORM1_4
                            Me.WebViewer_Report.ViewerType = DataDynamics.ActiveReports.Web.ViewerType.AcrobatReader

                            'Dim dataReadRfcard As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "vi_Nr_Rfcard_GetAll", New SqlParameter("@card_id ", CommonUtility.Get_String(dsRequestDetails.Tables(0).Rows(0).Item("card_id"))))

                            'Dim dataRead_Sum As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "sp_getSummaryPrintForm", New SqlParameter("@invh_run_auto ", CommonUtility.Get_String(Request.QueryString("SendCell"))))

                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then
                                'If dataRead_Sum.Read Then
                                '    'rpt.txtSum_Gross_Weight.Text = dataRead_Sum.Item("Gross_Weight").ToString()
                                '    'rpt.txtSum_g_Unit_Desc.Text = dataRead_Sum.Item("g_Unit_Desc").ToString()

                                '    dataRead_Sum.Close()
                                'End If
                                rpt.C_TotalRowDe.text = dsRequestDetails.Tables(0).Rows.Count
                                WebViewer_Report.Report = rpt
                                WebViewer_Report.Height = 700


                                WebViewer_Report.Report.DataSource = dsRequestDetails.Tables(0) 'ReportPrintClass.GetDataLoadTrd("20090416-000001", "ST-001") 'dsRequestDetail.Tables(0)
                                Page.SetFocus(Page)
                                WebViewer_Report.Focus()
                                Return 1
                            Else
                                Return 3
                            End If
                        Case "FORM2"
                            'view

                            Dim rpt = New rpt3_ediFORM2
                            Me.WebViewer_Report.ViewerType = DataDynamics.ActiveReports.Web.ViewerType.AcrobatReader

                            'Dim dataReadRfcard As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "vi_Nr_Rfcard_GetAll", New SqlParameter("@card_id ", CommonUtility.Get_String(dsRequestDetails.Tables(0).Rows(0).Item("card_id"))))

                            'Dim dataRead_Sum As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "sp_getSummaryPrintForm", New SqlParameter("@invh_run_auto ", CommonUtility.Get_String(Request.QueryString("SendCell"))))

                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then
                                'If dataRead_Sum.Read Then
                                '    'rpt.txtSum_Gross_Weight.Text = dataRead_Sum.Item("Gross_Weight").ToString()
                                '    'rpt.txtSum_g_Unit_Desc.Text = dataRead_Sum.Item("g_Unit_Desc").ToString()

                                '    dataRead_Sum.Close()
                                'End If
                                rpt.C_TotalRowDe.text = dsRequestDetails.Tables(0).Rows.Count
                                WebViewer_Report.Report = rpt
                                WebViewer_Report.Height = 700


                                WebViewer_Report.Report.DataSource = dsRequestDetails.Tables(0) 'ReportPrintClass.GetDataLoadTrd("20090416-000001", "ST-001") 'dsRequestDetail.Tables(0)
                                Page.SetFocus(Page)
                                WebViewer_Report.Focus()
                                Return 1
                            Else
                                Return 3
                            End If

                        Case "FORM2_1"
                            'view

                            Dim rpt = New rpt3_ediFORM2_1
                            Me.WebViewer_Report.ViewerType = DataDynamics.ActiveReports.Web.ViewerType.AcrobatReader

                            'Dim dataReadRfcard As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "vi_Nr_Rfcard_GetAll", New SqlParameter("@card_id ", CommonUtility.Get_String(dsRequestDetails.Tables(0).Rows(0).Item("card_id"))))

                            'Dim dataRead_Sum As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "sp_getSummaryPrintForm", New SqlParameter("@invh_run_auto ", CommonUtility.Get_String(Request.QueryString("SendCell"))))

                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then
                                'If dataRead_Sum.Read Then
                                '    'rpt.txtSum_Gross_Weight.Text = dataRead_Sum.Item("Gross_Weight").ToString()
                                '    'rpt.txtSum_g_Unit_Desc.Text = dataRead_Sum.Item("g_Unit_Desc").ToString()

                                '    dataRead_Sum.Close()
                                'End If
                                Select Case SiteID
                                    Case "CM-001"
                                        rpt.txtBANGKOK.text = "CHIANGMAI"
                                    Case Else
                                        rpt.txtBANGKOK.text = "BANGKOK"
                                End Select
                                rpt.C_TotalRowDe.text = dsRequestDetails.Tables(0).Rows.Count
                                WebViewer_Report.Report = rpt
                                WebViewer_Report.Height = 700


                                WebViewer_Report.Report.DataSource = dsRequestDetails.Tables(0) 'ReportPrintClass.GetDataLoadTrd("20090416-000001", "ST-001") 'dsRequestDetail.Tables(0)
                                Page.SetFocus(Page)
                                WebViewer_Report.Focus()
                                Return 1
                            Else
                                Return 3
                            End If
                        Case "FORM2_2"
                            'view

                            Dim rpt = New rpt3_ediFORM2_2
                            Me.WebViewer_Report.ViewerType = DataDynamics.ActiveReports.Web.ViewerType.AcrobatReader

                            'Dim dataReadRfcard As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "vi_Nr_Rfcard_GetAll", New SqlParameter("@card_id ", CommonUtility.Get_String(dsRequestDetails.Tables(0).Rows(0).Item("card_id"))))

                            'Dim dataRead_Sum As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "sp_getSummaryPrintForm", New SqlParameter("@invh_run_auto ", CommonUtility.Get_String(Request.QueryString("SendCell"))))

                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then
                                'If dataRead_Sum.Read Then
                                '    'rpt.txtSum_Gross_Weight.Text = dataRead_Sum.Item("Gross_Weight").ToString()
                                '    'rpt.txtSum_g_Unit_Desc.Text = dataRead_Sum.Item("g_Unit_Desc").ToString()

                                '    dataRead_Sum.Close()
                                'End If
                                rpt.C_TotalRowDe.text = dsRequestDetails.Tables(0).Rows.Count
                                WebViewer_Report.Report = rpt
                                WebViewer_Report.Height = 700


                                WebViewer_Report.Report.DataSource = dsRequestDetails.Tables(0) 'ReportPrintClass.GetDataLoadTrd("20090416-000001", "ST-001") 'dsRequestDetail.Tables(0)
                                Page.SetFocus(Page)
                                WebViewer_Report.Focus()
                                Return 1
                            Else
                                Return 3
                            End If
                        Case "FORM2_3"
                            'view

                            Dim rpt = New rpt3_ediFORM2_3
                            Me.WebViewer_Report.ViewerType = DataDynamics.ActiveReports.Web.ViewerType.AcrobatReader

                            'Dim dataReadRfcard As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "vi_Nr_Rfcard_GetAll", New SqlParameter("@card_id ", CommonUtility.Get_String(dsRequestDetails.Tables(0).Rows(0).Item("card_id"))))

                            ' Dim dataRead_Sum As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "sp_getSummaryPrintForm", New SqlParameter("@invh_run_auto ", CommonUtility.Get_String(Request.QueryString("SendCell"))))

                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then
                                'If dataRead_Sum.Read Then
                                '    'rpt.txtSum_Gross_Weight.Text = dataRead_Sum.Item("Gross_Weight").ToString()
                                '    'rpt.txtSum_g_Unit_Desc.Text = dataRead_Sum.Item("g_Unit_Desc").ToString()

                                '    dataRead_Sum.Close()
                                'End If
                                rpt.C_TotalRowDe.text = dsRequestDetails.Tables(0).Rows.Count
                                WebViewer_Report.Report = rpt
                                WebViewer_Report.Height = 700


                                WebViewer_Report.Report.DataSource = dsRequestDetails.Tables(0) 'ReportPrintClass.GetDataLoadTrd("20090416-000001", "ST-001") 'dsRequestDetail.Tables(0)
                                Page.SetFocus(Page)
                                WebViewer_Report.Focus()
                                Return 1
                            Else
                                Return 3
                            End If
                        Case "FORM2_4" '���������
                            'view

                            Dim rpt = New rpt3_ediFORM2_4
                            Me.WebViewer_Report.ViewerType = DataDynamics.ActiveReports.Web.ViewerType.AcrobatReader

                            'Dim dataReadRfcard As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "vi_Nr_Rfcard_GetAll", New SqlParameter("@card_id ", CommonUtility.Get_String(dsRequestDetails.Tables(0).Rows(0).Item("card_id"))))

                            'Dim dataRead_Sum As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "sp_getSummaryPrintForm", New SqlParameter("@invh_run_auto ", CommonUtility.Get_String(Request.QueryString("SendCell"))))

                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then
                                'If dataRead_Sum.Read Then
                                '    'rpt.txtSum_Gross_Weight.Text = dataRead_Sum.Item("Gross_Weight").ToString()
                                '    'rpt.txtSum_g_Unit_Desc.Text = dataRead_Sum.Item("g_Unit_Desc").ToString()

                                '    dataRead_Sum.Close()
                                'End If
                                rpt.C_TotalRowDe.text = dsRequestDetails.Tables(0).Rows.Count
                                WebViewer_Report.Report = rpt
                                WebViewer_Report.Height = 700


                                WebViewer_Report.Report.DataSource = dsRequestDetails.Tables(0) 'ReportPrintClass.GetDataLoadTrd("20090416-000001", "ST-001") 'dsRequestDetail.Tables(0)
                                Page.SetFocus(Page)
                                WebViewer_Report.Focus()
                                Return 1
                            Else
                                Return 3
                            End If


                            ''ByTine 28-11-2559 ������������� CO (���)
                        Case "FORM2_5"
                            'view

                            Dim rpt = New rpt3_ediFORM2_5
                            Me.WebViewer_Report.ViewerType = DataDynamics.ActiveReports.Web.ViewerType.AcrobatReader

                            'Dim dataReadRfcard As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "vi_Nr_Rfcard_GetAll", New SqlParameter("@card_id ", CommonUtility.Get_String(dsRequestDetails.Tables(0).Rows(0).Item("card_id"))))

                            'Dim dataRead_Sum As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "sp_getSummaryPrintForm", New SqlParameter("@invh_run_auto ", CommonUtility.Get_String(Request.QueryString("SendCell"))))

                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then
                                'If dataRead_Sum.Read Then
                                '    'rpt.txtSum_Gross_Weight.Text = dataRead_Sum.Item("Gross_Weight").ToString()
                                '    'rpt.txtSum_g_Unit_Desc.Text = dataRead_Sum.Item("g_Unit_Desc").ToString()

                                '    dataRead_Sum.Close()
                                'End If
                                rpt.C_TotalRowDe.text = dsRequestDetails.Tables(0).Rows.Count
                                WebViewer_Report.Report = rpt
                                WebViewer_Report.Height = 700


                                WebViewer_Report.Report.DataSource = dsRequestDetails.Tables(0) 'ReportPrintClass.GetDataLoadTrd("20090416-000001", "ST-001") 'dsRequestDetail.Tables(0)
                                Page.SetFocus(Page)
                                WebViewer_Report.Focus()
                                Return 1
                            Else
                                Return 3
                            End If

                            ''ByTine 28-11-2559 ������������� CO (����)
                        Case "FORM2_6"
                            'view

                            Dim rpt = New rpt3_ediFORM2_6
                            Me.WebViewer_Report.ViewerType = DataDynamics.ActiveReports.Web.ViewerType.AcrobatReader

                            'Dim dataReadRfcard As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "vi_Nr_Rfcard_GetAll", New SqlParameter("@card_id ", CommonUtility.Get_String(dsRequestDetails.Tables(0).Rows(0).Item("card_id"))))

                            'Dim dataRead_Sum As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "sp_getSummaryPrintForm", New SqlParameter("@invh_run_auto ", CommonUtility.Get_String(Request.QueryString("SendCell"))))

                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then
                                'If dataRead_Sum.Read Then
                                '    'rpt.txtSum_Gross_Weight.Text = dataRead_Sum.Item("Gross_Weight").ToString()
                                '    'rpt.txtSum_g_Unit_Desc.Text = dataRead_Sum.Item("g_Unit_Desc").ToString()

                                '    dataRead_Sum.Close()
                                'End If
                                rpt.C_TotalRowDe.text = dsRequestDetails.Tables(0).Rows.Count
                                WebViewer_Report.Report = rpt
                                WebViewer_Report.Height = 700


                                WebViewer_Report.Report.DataSource = dsRequestDetails.Tables(0) 'ReportPrintClass.GetDataLoadTrd("20090416-000001", "ST-001") 'dsRequestDetail.Tables(0)
                                Page.SetFocus(Page)
                                WebViewer_Report.Focus()
                                Return 1
                            Else
                                Return 3
                            End If


                        Case "FORM3"
                            'view

                            Dim rpt = New rpt3_ediFORM3
                            Me.WebViewer_Report.ViewerType = DataDynamics.ActiveReports.Web.ViewerType.AcrobatReader

                            'Dim dataReadRfcard As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "vi_Nr_Rfcard_GetAll", New SqlParameter("@card_id ", CommonUtility.Get_String(dsRequestDetails.Tables(0).Rows(0).Item("card_id"))))

                            'Dim dataRead_Sum As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "sp_getSummaryPrintForm", New SqlParameter("@invh_run_auto ", CommonUtility.Get_String(Request.QueryString("SendCell"))))

                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then
                                'If dataRead_Sum.Read Then
                                '    'rpt.txtSum_Gross_Weight.Text = dataRead_Sum.Item("Gross_Weight").ToString()
                                '    'rpt.txtSum_g_Unit_Desc.Text = dataRead_Sum.Item("g_Unit_Desc").ToString()

                                '    dataRead_Sum.Close()
                                'End If
                                rpt.C_TotalRowDe.text = dsRequestDetails.Tables(0).Rows.Count
                                WebViewer_Report.Report = rpt
                                WebViewer_Report.Height = 700


                                WebViewer_Report.Report.DataSource = dsRequestDetails.Tables(0) 'ReportPrintClass.GetDataLoadTrd("20090416-000001", "ST-001") 'dsRequestDetail.Tables(0)
                                Page.SetFocus(Page)
                                WebViewer_Report.Focus()
                                Return 1
                            Else
                                Return 3
                            End If
                        Case "FORM3_1"
                            'view

                            Dim rpt = New rpt3_ediFORM3_1
                            Me.WebViewer_Report.ViewerType = DataDynamics.ActiveReports.Web.ViewerType.AcrobatReader

                            'Dim dataReadRfcard As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "vi_Nr_Rfcard_GetAll", New SqlParameter("@card_id ", CommonUtility.Get_String(dsRequestDetails.Tables(0).Rows(0).Item("card_id"))))

                            'Dim dataRead_Sum As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "sp_getSummaryPrintForm", New SqlParameter("@invh_run_auto ", CommonUtility.Get_String(Request.QueryString("SendCell"))))

                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then
                                'If dataRead_Sum.Read Then
                                '    'rpt.txtSum_Gross_Weight.Text = dataRead_Sum.Item("Gross_Weight").ToString()
                                '    'rpt.txtSum_g_Unit_Desc.Text = dataRead_Sum.Item("g_Unit_Desc").ToString()

                                '    dataRead_Sum.Close()
                                'End If
                                rpt.C_TotalRowDe.text = dsRequestDetails.Tables(0).Rows.Count
                                WebViewer_Report.Report = rpt
                                WebViewer_Report.Height = 700


                                WebViewer_Report.Report.DataSource = dsRequestDetails.Tables(0) 'ReportPrintClass.GetDataLoadTrd("20090416-000001", "ST-001") 'dsRequestDetail.Tables(0)
                                Page.SetFocus(Page)
                                WebViewer_Report.Focus()
                                Return 1
                            Else
                                Return 3
                            End If

                            'FORM4		����� �� (ATIGA) ��� (2 �����)
                        Case "FORM4"
                            'view

                            Dim rpt = New rpt3_ediFORM4
                            Me.WebViewer_Report.ViewerType = DataDynamics.ActiveReports.Web.ViewerType.AcrobatReader

                            'Dim dataReadRfcard As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "vi_Nr_Rfcard_GetAll", New SqlParameter("@card_id ", CommonUtility.Get_String(dsRequestDetails.Tables(0).Rows(0).Item("card_id"))))

                            ' Dim dataRead_Sum As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "sp_getSummaryPrintForm", New SqlParameter("@invh_run_auto ", CommonUtility.Get_String(Request.QueryString("SendCell"))))

                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then
                                'If dataRead_Sum.Read Then
                                '    'rpt.txtSum_Gross_Weight.Text = dataRead_Sum.Item("Gross_Weight").ToString()
                                '    'rpt.txtSum_g_Unit_Desc.Text = dataRead_Sum.Item("g_Unit_Desc").ToString()

                                '    dataRead_Sum.Close()
                                'End If
                                rpt.C_TotalRowDe.text = dsRequestDetails.Tables(0).Rows.Count
                                WebViewer_Report.Report = rpt
                                WebViewer_Report.Height = 700


                                WebViewer_Report.Report.DataSource = dsRequestDetails.Tables(0) 'ReportPrintClass.GetDataLoadTrd("20090416-000001", "ST-001") 'dsRequestDetail.Tables(0)
                                Page.SetFocus(Page)
                                WebViewer_Report.Focus()
                                Return 1
                            Else
                                Return 3
                            End If

                            'by rut D new
                            'FORM44_4	����� �� (ATIGA) ���� (7 �����)
                            'FORM44_44	����� �� (ATIGA) Attach Sheet ö¹�� ���� (7 �����)
                            'FORM44_41	����� �� AICO ���� (7 �����)
                            'FORM441_4	����� �� AICO Attach Sheet ö¹�� ���� (7 �����)
                        Case "FORM44_4", "FORM44_44", "FORM44_41", "FORM441_4", "FORM44_01", "FORM44_02"
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
                                Case "FORM44_01"
                                    rpt = New rpt3_ediFORM44_01
                                Case "FORM44_02"
                                    rpt = New rpt3_ediFORM44_02
                            End Select
                            Me.WebViewer_Report.ViewerType = DataDynamics.ActiveReports.Web.ViewerType.AcrobatReader

                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then
                                'by rut D New
                                '���͹����� ��Ѻ�� �ѹ��� 03-12-2012 ����ǡѺ RVC �ͧ�ѹ Case 2,8 ��ͧ�ʴ���Ť�� ������������ʴ�
                                'begin RVC
                                rpt.txtCheck_CaseRVCCount.text = CheckCount_RVC(Request.QueryString("SendCell"), SendFormTypes)
                                rpt.txtDisplayUDS7.text = Check_DisplayUSD7(Request.QueryString("SendCell"))
                                'end RVC  

                                ''ByTine 24-01-2560 Invoice ���˹��
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

                                rpt.C_TotalRowDe.text = dsRequestDetails.Tables(0).Rows.Count
                                WebViewer_Report.Report = rpt
                                WebViewer_Report.Height = 700


                                WebViewer_Report.Report.DataSource = dsRequestDetails.Tables(0)
                                Page.SetFocus(Page)
                                WebViewer_Report.Focus()
                                Return 1
                            Else
                                Return 3
                            End If
                            'FORM4_1		����� �� AICO ��� (2 �����)
                        Case "FORM4_1"
                                'view

                                Dim rpt = New rpt3_ediFORM4_1
                                Me.WebViewer_Report.ViewerType = DataDynamics.ActiveReports.Web.ViewerType.AcrobatReader

                                'Dim dataReadRfcard As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "vi_Nr_Rfcard_GetAll", New SqlParameter("@card_id ", CommonUtility.Get_String(dsRequestDetails.Tables(0).Rows(0).Item("card_id"))))

                                'Dim dataRead_Sum As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "sp_getSummaryPrintForm", New SqlParameter("@invh_run_auto ", CommonUtility.Get_String(Request.QueryString("SendCell"))))

                                If dsRequestDetails.Tables(0).Rows.Count > 0 Then
                                    'If dataRead_Sum.Read Then
                                    '    'rpt.txtSum_Gross_Weight.Text = dataRead_Sum.Item("Gross_Weight").ToString()
                                    '    'rpt.txtSum_g_Unit_Desc.Text = dataRead_Sum.Item("g_Unit_Desc").ToString()

                                    '    dataRead_Sum.Close()
                                    'End If
                                    rpt.C_TotalRowDe.text = dsRequestDetails.Tables(0).Rows.Count
                                    WebViewer_Report.Report = rpt
                                    WebViewer_Report.Height = 700


                                    WebViewer_Report.Report.DataSource = dsRequestDetails.Tables(0) 'ReportPrintClass.GetDataLoadTrd("20090416-000001", "ST-001") 'dsRequestDetail.Tables(0)
                                    Page.SetFocus(Page)
                                    WebViewer_Report.Focus()
                                    Return 1
                                Else
                                    Return 3
                                End If

                                'FORM44		����� �� (ATIGA) Attach Sheet ö¹�� ��� (2 �����)
                        Case "FORM44"
                                'view

                                Dim rpt = New rpt3_ediFORM44
                                Me.WebViewer_Report.ViewerType = DataDynamics.ActiveReports.Web.ViewerType.AcrobatReader

                                'Dim dataReadRfcard As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "vi_Nr_Rfcard_GetAll", New SqlParameter("@card_id ", CommonUtility.Get_String(dsRequestDetails.Tables(0).Rows(0).Item("card_id"))))

                                ' Dim dataRead_Sum As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "sp_getSummaryPrintForm", New SqlParameter("@invh_run_auto ", CommonUtility.Get_String(Request.QueryString("SendCell"))))

                                If dsRequestDetails.Tables(0).Rows.Count > 0 Then
                                    'If dataRead_Sum.Read Then
                                    '    'rpt.txtSum_Gross_Weight.Text = dataRead_Sum.Item("Gross_Weight").ToString()
                                    '    'rpt.txtSum_g_Unit_Desc.Text = dataRead_Sum.Item("g_Unit_Desc").ToString()

                                    '    dataRead_Sum.Close()
                                    'End If
                                    rpt.C_TotalRowDe.text = dsRequestDetails.Tables(0).Rows.Count
                                    WebViewer_Report.Report = rpt
                                    WebViewer_Report.Height = 700


                                    WebViewer_Report.Report.DataSource = dsRequestDetails.Tables(0) 'ReportPrintClass.GetDataLoadTrd("20090416-000001", "ST-001") 'dsRequestDetail.Tables(0)
                                    Page.SetFocus(Page)
                                    WebViewer_Report.Focus()
                                    Return 1
                                Else
                                    Return 3
                                End If
                                'FORM441		����� �� AICO Attach Sheet ö¹�� ��� (2 �����)
                        Case "FORM441"
                                'view

                                Dim rpt = New rpt3_ediFORM441
                                Me.WebViewer_Report.ViewerType = DataDynamics.ActiveReports.Web.ViewerType.AcrobatReader

                                'Dim dataReadRfcard As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "vi_Nr_Rfcard_GetAll", New SqlParameter("@card_id ", CommonUtility.Get_String(dsRequestDetails.Tables(0).Rows(0).Item("card_id"))))

                                ' Dim dataRead_Sum As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "sp_getSummaryPrintForm", New SqlParameter("@invh_run_auto ", CommonUtility.Get_String(Request.QueryString("SendCell"))))

                                If dsRequestDetails.Tables(0).Rows.Count > 0 Then
                                    'If dataRead_Sum.Read Then
                                    '    'rpt.txtSum_Gross_Weight.Text = dataRead_Sum.Item("Gross_Weight").ToString()
                                    '    'rpt.txtSum_g_Unit_Desc.Text = dataRead_Sum.Item("g_Unit_Desc").ToString()

                                    '    dataRead_Sum.Close()
                                    'End If
                                    rpt.C_TotalRowDe.text = dsRequestDetails.Tables(0).Rows.Count
                                    WebViewer_Report.Report = rpt
                                    WebViewer_Report.Height = 700


                                    WebViewer_Report.Report.DataSource = dsRequestDetails.Tables(0) 'ReportPrintClass.GetDataLoadTrd("20090416-000001", "ST-001") 'dsRequestDetail.Tables(0)
                                    Page.SetFocus(Page)
                                    WebViewer_Report.Focus()
                                    Return 1
                                Else
                                    Return 3
                                End If
                        Case "FORM4_2"
                                'view

                                Dim rpt As Object
                                Select Case SiteID
                                    'Case "ST-003"
                                    '    rpt = New rpt3_ediFORM4_2_ST003
                                    'Case "ST-001"
                                    '    rpt = New rpt3_ediFORM4_2_New
                                    Case Else
                                        rpt = New rpt3_ediFORM4_2_New
                                End Select

                                Me.WebViewer_Report.ViewerType = DataDynamics.ActiveReports.Web.ViewerType.AcrobatReader

                                ' Dim dataReadRfcard As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "vi_Nr_Rfcard_GetAll", New SqlParameter("@card_id ", CommonUtility.Get_String(dsRequestDetails.Tables(0).Rows(0).Item("card_id"))))

                                ' Dim dataRead_Sum As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "sp_getSummaryPrintForm", New SqlParameter("@invh_run_auto ", CommonUtility.Get_String(Request.QueryString("SendCell"))))

                                If dsRequestDetails.Tables(0).Rows.Count > 0 Then
                                'If dataRead_Sum.Read Then
                                '    'rpt.txtSum_Gross_Weight.Text = dataRead_Sum.Item("Gross_Weight").ToString()
                                '    'rpt.txtSum_g_Unit_Desc.Text = dataRead_Sum.Item("g_Unit_Desc").ToString()

                                '    dataRead_Sum.Close()
                                'End If

                                'ByTine 12-10-2559 B2B
                                rpt._TempB2B = ReportPrintClass.LoadDataForB2B(Request.QueryString("SendCell"))

                                ''ByTine 14-2-2561 �¡ GW
                                rpt.txtCheck_CaseRVCCount.text = CheckCount_RVC(Request.QueryString("SendCell"), SendFormTypes)

                                rpt.C_TotalRowDe.text = dsRequestDetails.Tables(0).Rows.Count
                                    WebViewer_Report.Report = rpt
                                    WebViewer_Report.Height = 700


                                    WebViewer_Report.Report.DataSource = dsRequestDetails.Tables(0) 'ReportPrintClass.GetDataLoadTrd("20090416-000001", "ST-001") 'dsRequestDetail.Tables(0)
                                    Page.SetFocus(Page)
                                    WebViewer_Report.Focus()
                                    Return 1
                                Else
                                    Return 3
                                End If

                        Case "FORM4_3"
                                'view

                                Dim rpt = New rpt3_ediFORMs4_3
                                Me.WebViewer_Report.ViewerType = DataDynamics.ActiveReports.Web.ViewerType.AcrobatReader

                                ' Dim dataReadRfcard As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "vi_Nr_Rfcard_GetAll", New SqlParameter("@card_id ", CommonUtility.Get_String(dsRequestDetails.Tables(0).Rows(0).Item("card_id"))))

                                ' Dim dataRead_Sum As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "sp_getSummaryPrintForm", New SqlParameter("@invh_run_auto ", CommonUtility.Get_String(Request.QueryString("SendCell"))))

                                If dsRequestDetails.Tables(0).Rows.Count > 0 Then
                                    'If dataRead_Sum.Read Then
                                    '    'rpt.txtSum_Gross_Weight.Text = dataRead_Sum.Item("Gross_Weight").ToString()
                                    '    'rpt.txtSum_g_Unit_Desc.Text = dataRead_Sum.Item("g_Unit_Desc").ToString()

                                    '    dataRead_Sum.Close()
                                    'End If
                                    rpt.C_TotalRowDe.text = dsRequestDetails.Tables(0).Rows.Count
                                    WebViewer_Report.Report = rpt
                                    WebViewer_Report.Height = 700


                                    WebViewer_Report.Report.DataSource = dsRequestDetails.Tables(0) 'ReportPrintClass.GetDataLoadTrd("20090416-000001", "ST-001") 'dsRequestDetail.Tables(0)
                                    Page.SetFocus(Page)
                                    WebViewer_Report.Focus()
                                    Return 1
                                Else
                                    Return 3
                                End If
                        Case "FORM4_4"
                                'view

                                Dim rpt = New rpt3_ediFORM4_4
                                Me.WebViewer_Report.ViewerType = DataDynamics.ActiveReports.Web.ViewerType.AcrobatReader

                                'Dim dataReadRfcard As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "vi_Nr_Rfcard_GetAll", New SqlParameter("@card_id ", CommonUtility.Get_String(dsRequestDetails.Tables(0).Rows(0).Item("card_id"))))

                                'Dim dataRead_Sum As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "sp_getSummaryPrintForm", New SqlParameter("@invh_run_auto ", CommonUtility.Get_String(Request.QueryString("SendCell"))))

                                If dsRequestDetails.Tables(0).Rows.Count > 0 Then
                                    'If dataRead_Sum.Read Then
                                    '    'rpt.txtSum_Gross_Weight.Text = dataRead_Sum.Item("Gross_Weight").ToString()
                                    '    'rpt.txtSum_g_Unit_Desc.Text = dataRead_Sum.Item("g_Unit_Desc").ToString()

                                    '    dataRead_Sum.Close()
                                    'End If
                                    rpt.txtTemp_SiteSend.text = Request.QueryString("SendSiteID")
                                    rpt.C_TotalRowDe.text = dsRequestDetails.Tables(0).Rows.Count
                                    WebViewer_Report.Report = rpt
                                    WebViewer_Report.Height = 700


                                    WebViewer_Report.Report.DataSource = dsRequestDetails.Tables(0) 'ReportPrintClass.GetDataLoadTrd("20090416-000001", "ST-001") 'dsRequestDetail.Tables(0)
                                    Page.SetFocus(Page)
                                    WebViewer_Report.Focus()
                                    Return 1
                                Else
                                    Return 3
                                End If
                        Case "FORM4_5"
                                'view
                            Dim string_IP As String = Request.ServerVariables("REMOTE_ADDR")
                            Label1.Text = string_IP
                                Dim rpt = New rpt3_ediFORM4_5
                                Me.WebViewer_Report.ViewerType = DataDynamics.ActiveReports.Web.ViewerType.AcrobatReader

                                'Dim dataReadRfcard As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "vi_Nr_Rfcard_GetAll", New SqlParameter("@card_id ", CommonUtility.Get_String(dsRequestDetails.Tables(0).Rows(0).Item("card_id"))))

                                'Dim dataRead_Sum As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "sp_getSummaryPrintForm", New SqlParameter("@invh_run_auto ", CommonUtility.Get_String(Request.QueryString("SendCell"))))

                                If dsRequestDetails.Tables(0).Rows.Count > 0 Then
                                    'If dataRead_Sum.Read Then
                                    '    'rpt.txtSum_Gross_Weight.Text = dataRead_Sum.Item("Gross_Weight").ToString()
                                    '    'rpt.txtSum_g_Unit_Desc.Text = dataRead_Sum.Item("g_Unit_Desc").ToString()

                                    '    dataRead_Sum.Close()
                                    'End If
                                    rpt.txtTemp_SiteSend.text = Request.QueryString("SendSiteID")
                                    rpt.C_TotalRowDe.text = dsRequestDetails.Tables(0).Rows.Count
                                    WebViewer_Report.Report = rpt
                                    WebViewer_Report.Height = 700


                                    WebViewer_Report.Report.DataSource = dsRequestDetails.Tables(0) 'ReportPrintClass.GetDataLoadTrd("20090416-000001", "ST-001") 'dsRequestDetail.Tables(0)
                                    Page.SetFocus(Page)
                                    WebViewer_Report.Focus()
                                    Return 1
                                Else
                                    Return 3
                                End If
                        Case "FORM4_6" '���
                                Dim rpt = New rpt3_ediFORM4_6
                                Me.WebViewer_Report.ViewerType = DataDynamics.ActiveReports.Web.ViewerType.AcrobatReader

                                If dsRequestDetails.Tables(0).Rows.Count > 0 Then
                                    rpt.txtTemp_SiteSend.text = Request.QueryString("SendSiteID")
                                    rpt.C_TotalRowDe.text = dsRequestDetails.Tables(0).Rows.Count
                                    WebViewer_Report.Report = rpt
                                    WebViewer_Report.Height = 700


                                    WebViewer_Report.Report.DataSource = dsRequestDetails.Tables(0) 'ReportPrintClass.GetDataLoadTrd("20090416-000001", "ST-001") 'dsRequestDetail.Tables(0)
                                    Page.SetFocus(Page)
                                    WebViewer_Report.Focus()
                                    Return 1
                                Else
                                    Return 3
                                End If
                        Case "FORM4_61" '����
                                Dim rpt = New rpt3_ediFORM4_61
                                Me.WebViewer_Report.ViewerType = DataDynamics.ActiveReports.Web.ViewerType.AcrobatReader

                                If dsRequestDetails.Tables(0).Rows.Count > 0 Then
                                    'by rut D New
                                    '���͹����� ��Ѻ�� �ѹ��� 03-12-2012 ����ǡѺ RVC �ͧ�ѹ Case 2,8 ��ͧ�ʴ���Ť�� ������������ʴ�
                                    'begin RVC
                                    rpt.txtCheck_CaseRVCCount.text = CheckCount_RVCByAJ(Request.QueryString("SendCell"), SendFormTypes)
                                    rpt.txtDisplayUDS7.text = Check_DisplayUSD7(Request.QueryString("SendCell"))
                                    'end RVC 

                                    'ByTine 12-10-2559 B2B
                                    'rpt._TempB2B = ReportPrintClass.LoadDataForB2B(Request.QueryString("SendCell"))

                                    rpt.txtTemp_SiteSend.text = Request.QueryString("SendSiteID")
                                    rpt.C_TotalRowDe.text = dsRequestDetails.Tables(0).Rows.Count
                                    WebViewer_Report.Report = rpt
                                    WebViewer_Report.Height = 700


                                    WebViewer_Report.Report.DataSource = dsRequestDetails.Tables(0)
                                    Page.SetFocus(Page)
                                    WebViewer_Report.Focus()
                                    Return 1
                                Else
                                    Return 3
                                End If
                        Case "FORM4_81" '���
                                'view
                                Dim rpt = New rpt3_ediFORM4_8
                                Me.WebViewer_Report.ViewerType = DataDynamics.ActiveReports.Web.ViewerType.AcrobatReader

                                If dsRequestDetails.Tables(0).Rows.Count > 0 Then
                                    rpt.txtTemp_SiteSend.text = Request.QueryString("SendSiteID")
                                    rpt.C_TotalRowDe.text = dsRequestDetails.Tables(0).Rows.Count
                                    WebViewer_Report.Report = rpt
                                    WebViewer_Report.Height = 700


                                    WebViewer_Report.Report.DataSource = dsRequestDetails.Tables(0) 'ReportPrintClass.GetDataLoadTrd("20090416-000001", "ST-001") 'dsRequestDetail.Tables(0)
                                    Page.SetFocus(Page)
                                    WebViewer_Report.Focus()
                                    Return 1
                                Else
                                    Return 3
                                End If
                        Case "FORM4_8" '����

                                'view
                                'by rut New AK 30-06-2014
                                Dim rpt As Object
                                Me.WebViewer_Report.ViewerType = DataDynamics.ActiveReports.Web.ViewerType.AcrobatReader

                                If dsRequestDetails.Tables(0).Rows.Count > 0 Then
                                    Select Case Check_Form4_8(Request.QueryString("SendCell"))
                                        Case True '����
                                            rpt = New rpt3_ediFORM4_8_New
                                            '���͹����� ��Ѻ�� �ѹ��� 03-12-2012 ����ǡѺ RVC �ͧ�ѹ Case 2,8 ��ͧ�ʴ���Ť�� ������������ʴ�
                                            'begin RVC
                                            rpt.txtCheck_CaseRVCCount.text = CheckCount_RVC(Request.QueryString("SendCell"), SendFormTypes)
                                            rpt.txtDisplayUDS7.text = Check_DisplayUSD7(Request.QueryString("SendCell"))
                                            'end RVC  

                                            'ByTine 12-10-2559 B2B
                                            'rpt._TempB2B = ReportPrintClass.LoadDataForB2B(Request.QueryString("SendCell"))

                                        Case False '���
                                            rpt = New rpt3_ediFORM4_8
                                    End Select

                                    rpt.txtTemp_SiteSend.text = Request.QueryString("SendSiteID")
                                    rpt.C_TotalRowDe.text = dsRequestDetails.Tables(0).Rows.Count
                                    WebViewer_Report.Report = rpt
                                    WebViewer_Report.Height = 700


                                    WebViewer_Report.Report.DataSource = dsRequestDetails.Tables(0) 'ReportPrintClass.GetDataLoadTrd("20090416-000001", "ST-001") 'dsRequestDetail.Tables(0)
                                    Page.SetFocus(Page)
                                    WebViewer_Report.Focus()
                                    Return 1
                                Else
                                    Return 3
                                End If
                        Case "FORM4_9"
                                'view

                                Dim rpt = New rpt3_ediFORM4_9
                                Me.WebViewer_Report.ViewerType = DataDynamics.ActiveReports.Web.ViewerType.AcrobatReader

                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then
                                ''ByTine B2B 17-03-2560
                                rpt._TempB2B = ReportPrintClass.LoadDataForB2B(Request.QueryString("SendCell"))
                                rpt.C_TotalRowDe.text = dsRequestDetails.Tables(0).Rows.Count
                                WebViewer_Report.Report = rpt
                                WebViewer_Report.Height = 700


                                WebViewer_Report.Report.DataSource = dsRequestDetails.Tables(0) 'ReportPrintClass.GetDataLoadTrd("20090416-000001", "ST-001") 'dsRequestDetail.Tables(0)
                                Page.SetFocus(Page)
                                WebViewer_Report.Focus()
                                Return 1
                            Else
                                Return 3
                            End If
                        Case "FORM4_91"
                                'view
                                Dim rpt = New rpt3_ediFORM4_91
                                Me.WebViewer_Report.ViewerType = DataDynamics.ActiveReports.Web.ViewerType.AcrobatReader

                                If dsRequestDetails.Tables(0).Rows.Count > 0 Then

                                    rpt.txtTemp_SiteSend.text = Request.QueryString("SendSiteID")
                                    rpt.C_TotalRowDe.text = dsRequestDetails.Tables(0).Rows.Count
                                    WebViewer_Report.Report = rpt
                                    WebViewer_Report.Height = 700


                                    WebViewer_Report.Report.DataSource = dsRequestDetails.Tables(0) 'ReportPrintClass.GetDataLoadTrd("20090416-000001", "ST-001") 'dsRequestDetail.Tables(0)
                                    Page.SetFocus(Page)
                                    WebViewer_Report.Focus()
                                    Return 1
                                Else
                                    Return 3
                                End If

                                ''ByTine 23-03-2559 AANZ ����
                        Case "FORM4_911"
                                'view
                                Dim rpt = New rpt3_ediFORM4_911
                                Me.WebViewer_Report.ViewerType = DataDynamics.ActiveReports.Web.ViewerType.AcrobatReader

                                If dsRequestDetails.Tables(0).Rows.Count > 0 Then
                                    rpt.txtTemp_SiteSend.text = Request.QueryString("SendSiteID")
                                    rpt.C_TotalRowDe.text = dsRequestDetails.Tables(0).Rows.Count
                                    rpt.txtCheck_CaseRVCCount.text = CheckCount_RVC(Request.QueryString("SendCell"), SendFormTypes)
                                    WebViewer_Report.Report = rpt
                                    WebViewer_Report.Height = 700


                                    WebViewer_Report.Report.DataSource = dsRequestDetails.Tables(0) 'ReportPrintClass.GetDataLoadTrd("20090416-000001", "ST-001") 'dsRequestDetail.Tables(0)
                                    Page.SetFocus(Page)
                                    WebViewer_Report.Focus()
                                    Return 1
                                Else
                                    Return 3
                                End If

                        Case "FORM5"
                                'view

                                Dim rpt = New rpt3_ediFORM5
                                Me.WebViewer_Report.ViewerType = DataDynamics.ActiveReports.Web.ViewerType.AcrobatReader

                                ' Dim dataReadRfcard As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "vi_Nr_Rfcard_GetAll", New SqlParameter("@card_id ", CommonUtility.Get_String(dsRequestDetails.Tables(0).Rows(0).Item("card_id"))))

                                ' Dim dataRead_Sum As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "sp_getSummaryPrintForm", New SqlParameter("@invh_run_auto ", CommonUtility.Get_String(Request.QueryString("SendCell"))))

                                If dsRequestDetails.Tables(0).Rows.Count > 0 Then
                                    'If dataRead_Sum.Read Then
                                    '    'rpt.txtSum_Gross_Weight.Text = dataRead_Sum.Item("Gross_Weight").ToString()
                                    '    'rpt.txtSum_g_Unit_Desc.Text = dataRead_Sum.Item("g_Unit_Desc").ToString()

                                    '    dataRead_Sum.Close()
                                    'End If
                                    rpt.C_TotalRowDe.text = dsRequestDetails.Tables(0).Rows.Count
                                    WebViewer_Report.Report = rpt
                                    WebViewer_Report.Height = 700


                                    WebViewer_Report.Report.DataSource = dsRequestDetails.Tables(0) 'ReportPrintClass.GetDataLoadTrd("20090416-000001", "ST-001") 'dsRequestDetail.Tables(0)
                                    Page.SetFocus(Page)
                                    WebViewer_Report.Focus()
                                    Return 1
                                Else
                                    Return 3
                                End If
                                'by rut
                        Case "FORM5_1"
                                'View()
                                Dim rpt = New rpt3_ediFORM5_1
                                Me.WebViewer_Report.ViewerType = DataDynamics.ActiveReports.Web.ViewerType.AcrobatReader

                                ' Dim dataReadRfcard As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "vi_Nr_Rfcard_GetAll", New SqlParameter("@card_id ", CommonUtility.Get_String(dsRequestDetails.Tables(0).Rows(0).Item("card_id"))))

                                ' Dim dataRead_Sum As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "sp_getSummaryPrintForm", New SqlParameter("@invh_run_auto ", CommonUtility.Get_String(Request.QueryString("SendCell"))))

                                If dsRequestDetails.Tables(0).Rows.Count > 0 Then
                                    'If dataRead_Sum.Read Then
                                    '    'rpt.txtSum_Gross_Weight.Text = dataRead_Sum.Item("Gross_Weight").ToString()
                                    '    'rpt.txtSum_g_Unit_Desc.Text = dataRead_Sum.Item("g_Unit_Desc").ToString()

                                    '    dataRead_Sum.Close()
                                    'End If
                                    rpt.C_TotalRowDe.text = dsRequestDetails.Tables(0).Rows.Count
                                    WebViewer_Report.Report = rpt
                                    WebViewer_Report.Height = 700


                                    WebViewer_Report.Report.DataSource = dsRequestDetails.Tables(0) 'ReportPrintClass.GetDataLoadTrd("20090416-000001", "ST-001") 'dsRequestDetail.Tables(0)
                                    Page.SetFocus(Page)
                                    WebViewer_Report.Focus()
                                    Return 1
                                Else
                                    Return 3
                                End If
                                ''Dim m_stream As New System.IO.MemoryStream()
                                ''Dim rpt = New rpt3_ediFORM5_1
                                ''If dsRequestDetails.Tables(0).Rows.Count > 0 Then
                                ''    Try
                                ''        rpt.C_TotalRowDe.text = dsRequestDetails.Tables(0).Rows.Count
                                ''        rpt.DataSource = dsRequestDetails.Tables(0)
                                ''        rpt.Run()

                                ''        If Me.PdfExport1 Is Nothing Then
                                ''            Me.PdfExport1 = New DataDynamics.ActiveReports.Export.Pdf.PdfExport
                                ''        End If

                                ''        Me.PdfExport1.Export(rpt.Document, m_stream)
                                ''        m_stream.Position = 0
                                ''        Response.ContentType = "application/pdf"
                                ''        Response.AddHeader("content-disposition", "inline; filename=MyExport.pdf")
                                ''        Response.BinaryWrite(m_stream.ToArray())
                                ''        Response.End()
                                ''    Catch eRunReport As DataDynamics.ActiveReports.ReportException
                                ''        ' Failure running report, just report the error to the user:
                                ''        Response.Clear()
                                ''        Response.Write("<h1>Error running report:</h1>")
                                ''        Response.Write(eRunReport.ToString())
                                ''        'Return
                                ''    End Try
                                ''    Return 1
                                ''Else
                                ''    Return 3
                                ''End If

                                ''Begin ��-����
                                ''=========================================================================
                                'ByTine 08-10-2558 ���������������-����
                        Case "FORM5_2"
                                'View()

                                Dim rpt = New rpt3_ediFORM5_2
                                Me.WebViewer_Report.ViewerType = DataDynamics.ActiveReports.Web.ViewerType.AcrobatReader

                                ' Dim dataReadRfcard As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "vi_Nr_Rfcard_GetAll", New SqlParameter("@card_id ", CommonUtility.Get_String(dsRequestDetails.Tables(0).Rows(0).Item("card_id"))))

                                ' Dim dataRead_Sum As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "sp_getSummaryPrintForm", New SqlParameter("@invh_run_auto ", CommonUtility.Get_String(Request.QueryString("SendCell"))))

                                If dsRequestDetails.Tables(0).Rows.Count > 0 Then

                                    rpt.C_TotalRowDe.text = dsRequestDetails.Tables(0).Rows.Count
                                    rpt.txtCheck_CaseRVCCount.text = CheckCount_RVC(Request.QueryString("SendCell"), SendFormTypes)

                                    ''ByTine 04-05-2559 ��������ó�����ͧ����ʴ� Inv ��ҧ�����
                                    If DBNull.Value.Equals(dsRequestDetails.Tables(0).Rows(0).Item("NoneDisplayInv10")) = True Then
                                        rpt.ChkNoneDisplayInv10.Checked = False
                                    Else
                                        rpt.ChkNoneDisplayInv10.Checked = dsRequestDetails.Tables(0).Rows(0).Item("NoneDisplayInv10")
                                    End If

                                    WebViewer_Report.Report = rpt
                                    WebViewer_Report.Height = 700


                                    WebViewer_Report.Report.DataSource = dsRequestDetails.Tables(0) 'ReportPrintClass.GetDataLoadTrd("20090416-000001", "ST-001") 'dsRequestDetail.Tables(0)
                                    Page.SetFocus(Page)
                                    WebViewer_Report.Focus()
                                    Return 1
                                Else
                                    Return 3
                                End If

                                ''ByTine �� ���ͺ���¡��੾�������ͧ����ͧ
                                'Dim m_stream As New System.IO.MemoryStream()
                                'Dim rpt = New rpt3_ediFORM5_2
                                'If dsRequestDetails.Tables(0).Rows.Count > 0 Then
                                '    Try
                                '        rpt.Document.Printer.PrinterName = ""
                                '        rpt.DataSource = dsRequestDetails.Tables(0)

                                '        rpt.txtCheck_CaseRVCCount.text = CheckCount_RVC(Request.QueryString("SendCell"), SendFormTypes)
                                '        rpt.C_TotalRowDe.text = dsRequestDetails.Tables(0).Rows.Count
                                '        rpt.Run()

                                '        If Me.PdfExport1 Is Nothing Then
                                '            Me.PdfExport1 = New DataDynamics.ActiveReports.Export.Pdf.PdfExport
                                '        End If

                                '        Me.PdfExport1.Export(rpt.Document, m_stream)
                                '        m_stream.Position = 0
                                '        Response.ContentType = "application/pdf"
                                '        Response.AddHeader("content-disposition", "inline; filename=MyExport.pdf")
                                '        Response.BinaryWrite(m_stream.ToArray())
                                '        Response.End()
                                '    Catch eRunReport As DataDynamics.ActiveReports.ReportException
                                '        ' Failure running report, just report the error to the user:
                                '        Response.Clear()
                                '        Response.Write("<h1>Error running report:</h1>")
                                '        Response.Write(eRunReport.ToString())
                                '        'Return
                                '    End Try
                                '    Return 1
                                'Else
                                '    Return 3
                                'End If
                                ''End ��-����
                                ''=========================================================================

                        Case "FORM6"
                                'view

                                Dim rpt = New rpt3_ediFORM6
                                Me.WebViewer_Report.ViewerType = DataDynamics.ActiveReports.Web.ViewerType.AcrobatReader

                                'Dim dataReadRfcard As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "vi_Nr_Rfcard_GetAll", New SqlParameter("@card_id ", CommonUtility.Get_String(dsRequestDetails.Tables(0).Rows(0).Item("card_id"))))

                                'Dim dataRead_Sum As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "sp_getSummaryPrintForm", New SqlParameter("@invh_run_auto ", CommonUtility.Get_String(Request.QueryString("SendCell"))))

                                If dsRequestDetails.Tables(0).Rows.Count > 0 Then
                                    'If dataRead_Sum.Read Then
                                    '    'rpt.txtSum_Gross_Weight.Text = dataRead_Sum.Item("Gross_Weight").ToString()
                                    '    'rpt.txtSum_g_Unit_Desc.Text = dataRead_Sum.Item("g_Unit_Desc").ToString()

                                    '    dataRead_Sum.Close()
                                    'End If
                                    rpt.C_TotalRowDe.text = dsRequestDetails.Tables(0).Rows.Count
                                    WebViewer_Report.Report = rpt
                                    WebViewer_Report.Height = 700


                                    WebViewer_Report.Report.DataSource = dsRequestDetails.Tables(0) 'ReportPrintClass.GetDataLoadTrd("20090416-000001", "ST-001") 'dsRequestDetail.Tables(0)
                                    Page.SetFocus(Page)
                                    WebViewer_Report.Focus()
                                    Return 1
                                Else
                                    Return 3
                                End If
                        Case "FORM7"
                                'view

                                Dim rpt = New rpt3_ediFORM8_7 'rpt3_ediFORM7
                                Me.WebViewer_Report.ViewerType = DataDynamics.ActiveReports.Web.ViewerType.AcrobatReader

                                'Dim dataReadRfcard As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "vi_Nr_Rfcard_GetAll", New SqlParameter("@card_id ", CommonUtility.Get_String(dsRequestDetails.Tables(0).Rows(0).Item("card_id"))))

                                'Dim dataRead_Sum As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "sp_getSummaryPrintForm", New SqlParameter("@invh_run_auto ", CommonUtility.Get_String(Request.QueryString("SendCell"))))

                                If dsRequestDetails.Tables(0).Rows.Count > 0 Then
                                    'If dataRead_Sum.Read Then
                                    '    'rpt.txtSum_Gross_Weight.Text = dataRead_Sum.Item("Gross_Weight").ToString()
                                    '    'rpt.txtSum_g_Unit_Desc.Text = dataRead_Sum.Item("g_Unit_Desc").ToString()

                                    '    dataRead_Sum.Close()
                                    'End If
                                    rpt.C_TotalRowDe.text = dsRequestDetails.Tables(0).Rows.Count
                                    WebViewer_Report.Report = rpt
                                    WebViewer_Report.Height = 700


                                    WebViewer_Report.Report.DataSource = dsRequestDetails.Tables(0) 'ReportPrintClass.GetDataLoadTrd("20090416-000001", "ST-001") 'dsRequestDetail.Tables(0)
                                    Page.SetFocus(Page)
                                    WebViewer_Report.Focus()
                                    Return 1
                                Else
                                    Return 3
                                End If
                        Case "FORM8"
                                'view

                                Dim rpt = New rpt3_ediFORM8
                                Me.WebViewer_Report.ViewerType = DataDynamics.ActiveReports.Web.ViewerType.AcrobatReader

                                'Dim dataReadRfcard As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "vi_Nr_Rfcard_GetAll", New SqlParameter("@card_id ", CommonUtility.Get_String(dsRequestDetails.Tables(0).Rows(0).Item("card_id"))))

                                'Dim dataRead_Sum As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "sp_getSummaryPrintForm", New SqlParameter("@invh_run_auto ", CommonUtility.Get_String(Request.QueryString("SendCell"))))

                                If dsRequestDetails.Tables(0).Rows.Count > 0 Then
                                    'If dataRead_Sum.Read Then
                                    '    'rpt.txtSum_Gross_Weight.Text = dataRead_Sum.Item("Gross_Weight").ToString()
                                    '    'rpt.txtSum_g_Unit_Desc.Text = dataRead_Sum.Item("g_Unit_Desc").ToString()

                                    '    dataRead_Sum.Close()
                                    'End If
                                    rpt.C_TotalRowDe.text = dsRequestDetails.Tables(0).Rows.Count
                                    WebViewer_Report.Report = rpt
                                    WebViewer_Report.Height = 700


                                    WebViewer_Report.Report.DataSource = dsRequestDetails.Tables(0) 'ReportPrintClass.GetDataLoadTrd("20090416-000001", "ST-001") 'dsRequestDetail.Tables(0)
                                    Page.SetFocus(Page)
                                    WebViewer_Report.Focus()
                                    Return 1
                                Else
                                    Return 3
                                End If
                        Case "FORM9"
                                'view

                                Dim rpt = New rpt3_ediFORM9 'rpt3_ediFORM7 'rpt3_ediFORM9
                                Me.WebViewer_Report.ViewerType = DataDynamics.ActiveReports.Web.ViewerType.AcrobatReader

                                'Dim dataReadRfcard As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "vi_Nr_Rfcard_GetAll", New SqlParameter("@card_id ", CommonUtility.Get_String(dsRequestDetails.Tables(0).Rows(0).Item("card_id"))))

                                'Dim dataRead_Sum As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "sp_getSummaryPrintForm", New SqlParameter("@invh_run_auto ", CommonUtility.Get_String(Request.QueryString("SendCell"))))

                                If dsRequestDetails.Tables(0).Rows.Count > 0 Then
                                    'If dataRead_Sum.Read Then
                                    '    'rpt.txtSum_Gross_Weight.Text = dataRead_Sum.Item("Gross_Weight").ToString()
                                    '    'rpt.txtSum_g_Unit_Desc.Text = dataRead_Sum.Item("g_Unit_Desc").ToString()

                                    '    dataRead_Sum.Close()
                                    'End If
                                    rpt.C_TotalRowDe.text = dsRequestDetails.Tables(0).Rows.Count
                                    WebViewer_Report.Report = rpt
                                    WebViewer_Report.Height = 700


                                    WebViewer_Report.Report.DataSource = dsRequestDetails.Tables(0) 'ReportPrintClass.GetDataLoadTrd("20090416-000001", "ST-001") 'dsRequestDetail.Tables(0)
                                    Page.SetFocus(Page)
                                    WebViewer_Report.Focus()
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
#End Region

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
            '    rpt_B2BData.txtCompanyName.Text = ds.Tables(0).Rows(0).Item("CompanyName") & " " & "(" & ds.Tables(0).Rows(0).Item("Company_tax_id") & ")"
            '    rpt_B2BData.txtFormName.Text = _FullFormName
            '    rpt_B2BData.txtImportCountry.Text = ds.Tables(0).Rows(0).Item("country_name")
            '    rpt_B2BData.txtRefNo.Text = ds.Tables(0).Rows(0).Item("reference_code")
            '    rpt_B2BData.txtIssueDate.Text = ds.Tables(0).Rows(0).Item("import_date")
            '    rpt_B2BData.txtInvHrunauto.Text = "�Ţ�����ҧ�ԧ : " & invh_run_auto

            '    rpt_B2BData.DataSource = ds.Tables(0)
            'End If
            rpt_B2BData.DataSource = ds.Tables(0)
            Return rpt_B2BData

        Catch ex As Exception
            Return Nothing
        End Try
    End Function

#End Region

#Region "Invoice"
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
#End Region

End Class