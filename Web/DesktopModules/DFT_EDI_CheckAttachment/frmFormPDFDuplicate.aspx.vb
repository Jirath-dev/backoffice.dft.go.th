Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports DFT.Dotnetnuke.ClassLibrary
Imports System.IO
Imports ReportPrintClass

Partial Public Class frmFormPDFDuplicate
    Inherits System.Web.UI.Page
    Dim strEDIConn As String = ConfigurationManager.ConnectionStrings("OriginConnection").ConnectionString
    Friend WithEvents PdfExport1 As DataDynamics.ActiveReports.Export.Pdf.PdfExport

    Private YearID As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Request.QueryString("yearid") Is Nothing Then
            YearID = Request.QueryString("yearid")
        End If

        If Not Page.IsPostBack Then
            Try
                Dim form_type As String = ""
                If Not Request.QueryString("form_type") Is Nothing Then
                    form_type = Request.QueryString("form_type")
                End If

                Dim ds As New DataSet

                Select Case form_type
                    Case "FORM3_1"
                        ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, "vi_form4_edi_printPDF_NewDS31",
                                        New SqlParameter("@INVH_RUN_AUTO", Request.QueryString("INVH_RUN_AUTO")))
                    Case "FORM44_01", "FORM44_02", "FORMAHK", "FORMD_ESS", "FORMD_ESS_", "FORMRCEP", "FORME_01", "FORME_ESS"
                        ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, "vi_form4_edi_printPDF_NewDS_ASW",
                                        New SqlParameter("@INVH_RUN_AUTO", Request.QueryString("INVH_RUN_AUTO")))
                        'Case "FORM4_9"
                        '    ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, "vi_form4_edi_printPDF_NewDS_Duplicate",
                        '                    New SqlParameter("@INVH_RUN_AUTO", Request.QueryString("INVH_RUN_AUTO")))
                    Case Else
                        Select Case YearID
                            Case ""
                                ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, "vi_form4_edi_printPDF_NewDS_test",
                                       New SqlParameter("@INVH_RUN_AUTO", Request.QueryString("INVH_RUN_AUTO")))
                            Case "1"
                                ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, "vi_form4_edi_RePrintFormBar_NewDS_Ole01",
                                                    New SqlParameter("@INVH_RUN_AUTO", Request.QueryString("INVH_RUN_AUTO")),
                                                    New SqlParameter("@SITE_ID", ""))
                            Case "2"
                                ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, "vi_form4_edi_RePrintFormBar_NewDS_Ole02",
                                                                               New SqlParameter("@INVH_RUN_AUTO", Request.QueryString("INVH_RUN_AUTO")),
                                                                               New SqlParameter("@SITE_ID", ""))
                            Case "3"
                                ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, "vi_form4_edi_RePrintFormBar_NewDS_Ole03",
                                                                               New SqlParameter("@INVH_RUN_AUTO", Request.QueryString("INVH_RUN_AUTO")),
                                                                               New SqlParameter("@SITE_ID", ""))
                            Case "4"
                                ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, "vi_form4_edi_RePrintFormBar_NewDS_Ole04",
                                                                               New SqlParameter("@INVH_RUN_AUTO", Request.QueryString("INVH_RUN_AUTO")),
                                                                               New SqlParameter("@SITE_ID", ""))
                            Case "5"
                                ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, "vi_form4_edi_RePrintFormBar_NewDS_Ole05",
                                                                               New SqlParameter("@INVH_RUN_AUTO", Request.QueryString("INVH_RUN_AUTO")),
                                                                               New SqlParameter("@SITE_ID", ""))
                            Case "6"
                                ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, "vi_form4_edi_RePrintFormBar_NewDS_Ole06",
                                                                               New SqlParameter("@INVH_RUN_AUTO", Request.QueryString("INVH_RUN_AUTO")),
                                                                               New SqlParameter("@SITE_ID", ""))
                            Case "7"
                                ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, "vi_form4_edi_RePrintFormBar_NewDS_Ole07",
                                                                               New SqlParameter("@INVH_RUN_AUTO", Request.QueryString("INVH_RUN_AUTO")),
                                                                               New SqlParameter("@SITE_ID", ""))
                            Case "8"
                                ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, "vi_form4_edi_RePrintFormBar_NewDS_Ole08",
                                                                               New SqlParameter("@INVH_RUN_AUTO", Request.QueryString("INVH_RUN_AUTO")),
                                                                               New SqlParameter("@SITE_ID", ""))
                        End Select

                End Select

                'ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, "vi_form4_edi_printPDF_NewDS",
                'New SqlParameter("@INVH_RUN_AUTO", Request.QueryString("INVH_RUN_AUTO")))

                If ds.Tables(0).Rows.Count > 0 Then
                    Dim m_stream As New System.IO.MemoryStream()
                    Dim rpt As Object = Nothing
                    Dim FormType_name As String = CommonUtility.Get_StringValue(ds.Tables(0).Rows(0).Item("form_type")).ToUpper()
                    Select Case FormType_name
                        Case "FORMAHK"
                            rpt = New rpt3_ediFORMAHK_A4
                            'เงื่อนไขใหม่ ปรับแก้ วันที่ 03-12-2012 เกี่ยวกับ RVC สองอัน Case 2,8 ต้องแสดงมูลค่า ที่เหลือไม่แสดง
                            'begin RVC
                            rpt.txtCheck_CaseRVCCount.text = CheckCount_RVC(Request.QueryString("INVH_RUN_AUTO"), FormType_name)
                            rpt.txtDisplayUDS7.text = Check_DisplayUSD7(Request.QueryString("INVH_RUN_AUTO"))
                            'end RVC  
                            '============

                            rpt._TempB2B = ""

                            ''ByTine 24-01-2560 Invoice นายหน้า
                            ''=======================================================
                            If DBNull.Value.Equals(ds.Tables(0).Rows(0).Item("IsAgent")) = False Then
                                If ds.Tables(0).Rows(0).Item("IsAgent") = True Then
                                    rpt._IsAgent = ds.Tables(0).Rows(0).Item("IsAgent")
                                    rpt._InvAgentType = ds.Tables(0).Rows(0).Item("InvAgentType")
                                    rpt._CompanyName_Agent = ds.Tables(0).Rows(0).Item("CompanyName_Agent")
                                    rpt._Invoice_Agent = ds.Tables(0).Rows(0).Item("Invoice_Agent")
                                End If
                            End If
                            ''=======================================================
                        Case "FORMRCEP"
                            rpt = New rpt3_ediFORMRCEP
                            'เงื่อนไขใหม่ ปรับแก้ วันที่ 03-12-2012 เกี่ยวกับ RVC สองอัน Case 2,8 ต้องแสดงมูลค่า ที่เหลือไม่แสดง
                            'begin RVC
                            rpt.txtCheck_CaseRVCCount.text = CheckCount_RVC(Request.QueryString("INVH_RUN_AUTO"), FormType_name)
                            rpt.txtDisplayUDS7.text = Check_DisplayUSD7(Request.QueryString("INVH_RUN_AUTO"))
                            rpt._TempB2B = LoadDataForB2B_v2(Request.QueryString("INVH_RUN_AUTO"))
                            'end RVC  
                            '============

                            '//rpt._TempB2B = LoadDataForB2B(Request.QueryString("INVH_RUN_AUTO"))

                            ''ByTine 24-01-2560 Invoice นายหน้า
                            ''=======================================================
                            If DBNull.Value.Equals(ds.Tables(0).Rows(0).Item("IsAgent")) = False Then
                                If ds.Tables(0).Rows(0).Item("IsAgent") = True Then
                                    rpt._IsAgent = ds.Tables(0).Rows(0).Item("IsAgent")
                                    rpt._InvAgentType = ds.Tables(0).Rows(0).Item("InvAgentType")
                                    rpt._CompanyName_Agent = ds.Tables(0).Rows(0).Item("CompanyName_Agent")
                                    rpt._Invoice_Agent = ds.Tables(0).Rows(0).Item("Invoice_Agent")
                                End If
                            End If
                            ''=======================================================
                        Case "FORME_01"
                            rpt = New rpt3_ediFORME_01_DC
                            'เงื่อนไขใหม่ ปรับแก้ วันที่ 03-12-2012 เกี่ยวกับ RVC สองอัน Case 2,8 ต้องแสดงมูลค่า ที่เหลือไม่แสดง
                            'begin RVC
                            rpt.txtCheck_CaseRVCCount.text = CheckCount_RVC(Request.QueryString("INVH_RUN_AUTO"), FormType_name)
                            rpt.txtDisplayUDS7.text = Check_DisplayUSD7(Request.QueryString("INVH_RUN_AUTO"))
                            rpt._TempB2B = LoadDataForB2B(Request.QueryString("INVH_RUN_AUTO"))
                            'end RVC  
                            '============

                            '//rpt._TempB2B = ""
                        Case "FORME_ESS"
                            rpt = New rpt3_ediFORME_ESS_DC
                            'เงื่อนไขใหม่ ปรับแก้ วันที่ 03-12-2012 เกี่ยวกับ RVC สองอัน Case 2,8 ต้องแสดงมูลค่า ที่เหลือไม่แสดง
                            'begin RVC
                            rpt.txtCheck_CaseRVCCount.text = CheckCount_RVC(Request.QueryString("INVH_RUN_AUTO"), FormType_name)
                            rpt.txtDisplayUDS7.text = Check_DisplayUSD7(Request.QueryString("INVH_RUN_AUTO"))
                            rpt._TempB2B = LoadDataForB2B(Request.QueryString("INVH_RUN_AUTO"))
                             'end RVC  
                            '============

                            '//rpt._TempB2B = ""
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
                        Case "FORM2_ESS"
                            rpt = New rpt3_ediFORM2_ESS_pr
                        Case "FORM2_1"
                            rpt = New rpt3_ediFORM2_1_pr
                        Case "FORM2_2"
                            rpt = New rpt3_ediFORM2_2_pr
                        Case "FORM2_3"
                            rpt = New rpt3_ediFORM2_3_pr
                        Case "FORM2_4"
                            rpt = New rpt3_ediFORM2_4_pr
                        Case "FORM2_5" ''ByTine 30-11-2559 ฟอร์มใหม่ CO ปลา
                            rpt = New rpt3_ediFORM2_5_pr
                        Case "FORM2_6" ''ByTine 30-11-2559 ฟอร์มใหม่ CO ข้าว
                            rpt = New rpt3_ediFORM2_6_pr
                        Case "FORM3", "FORM3_1"
                            rpt = New rpt3_ediFORM3_pr
                        Case "FORM4"
                            rpt = New rpt3_ediFORM4_pr
                        Case "FORM4_1" '//Last update by Madnattz 20/04/55
                            rpt = New rpt3_ediFORM4_1_pr
                        Case "FORM44"
                            rpt = New rpt3_ediFORM44_pr
                            'by rut D new
                        Case "FORM44_4", "FORM44_44", "FORM44_41", "FORM441_4", "FORM44_01", "FORM44_02", "FORMD_ESS", "FORMD_ESS_"
                            Select Case FormType_name
                                Case "FORM44_4"
                                    rpt = New rpt3_ediFORM44_4_pr
                                Case "FORM44_44"
                                    rpt = New rpt3_ediFORM44_44_pr
                                Case "FORM44_41"
                                    rpt = New rpt3_ediFORM44_41_pr
                                Case "FORM441_4"
                                    rpt = New rpt3_ediFORM441_4_pr
                                Case "FORM44_01"
                                    rpt = New rpt3_ediFORM44_01_DC
                                Case "FORMD_ESS"
                                    rpt = New rpt3_ediFORMD_ESS_DC
                                Case "FORM44_02"
                                    rpt = New rpt3_ediFORM44_02_DC
                                Case "FORMD_ESS_"
                                    rpt = New rpt3_ediFORMD_ESS_
                            End Select
                            'เงื่อนไขใหม่ ปรับแก้ วันที่ 03-12-2012 เกี่ยวกับ RVC สองอัน Case 2,8 ต้องแสดงมูลค่า ที่เหลือไม่แสดง
                            'begin RVC
                            rpt.txtCheck_CaseRVCCount.text = CheckCount_RVC(Request.QueryString("INVH_RUN_AUTO"), FormType_name)
                            rpt.txtDisplayUDS7.text = Check_DisplayUSD7(Request.QueryString("INVH_RUN_AUTO"))
                            rpt._TempB2B = LoadDataForB2B(Request.QueryString("INVH_RUN_AUTO"))
                            'end RVC  
                            '============

                            '//rpt._TempB2B = LoadDataForB2B(Request.QueryString("INVH_RUN_AUTO"))

                            ''ByTine 24-01-2560 Invoice นายหน้า
                            ''=======================================================
                            If DBNull.Value.Equals(ds.Tables(0).Rows(0).Item("IsAgent")) = False Then
                                If ds.Tables(0).Rows(0).Item("IsAgent") = True Then
                                    rpt._IsAgent = ds.Tables(0).Rows(0).Item("IsAgent")
                                    rpt._InvAgentType = ds.Tables(0).Rows(0).Item("InvAgentType")
                                    rpt._CompanyName_Agent = ds.Tables(0).Rows(0).Item("CompanyName_Agent")
                                    rpt._Invoice_Agent = ds.Tables(0).Rows(0).Item("Invoice_Agent")
                                End If
                            End If
                            ''=======================================================

                        Case "FORM441"
                            rpt = New rpt3_ediFORM441_pr
                        Case "FORM4_2"
                            rpt = New rpt3_ediFORM4_2_pr_New

                            ''ByTine 12-10-2559 B2B
                            rpt._TempB2B = ReportPrintClass.LoadDataForB2B(Request.QueryString("INVH_RUN_AUTO"))
                            rpt.txtCheck_CaseRVCCount.text = CheckCount_RVC(Request.QueryString("INVH_RUN_AUTO"), FormType_name)

                        Case "FORM4_3"
                            rpt = New rpt3_ediFORMs4_3_pr
                        Case "FORMFTA_IN"
                            rpt = New rpt3_ediFORMsFTA_IN_pr
                        Case "FORM4_4"
                            rpt = New rpt3_ediFORM4_4_pr
                        Case "FORM4_5"
                            rpt = New rpt3_ediFORM4_5_pr

                        Case "FORM4_6" 'เก่า
                            rpt = New rpt3_ediFORM4_6_pr
                        Case "FORM4_61" 'ใหม่
                            rpt = New rpt3_ediFORM4_61_pr
                            'เงื่อนไขใหม่ ปรับแก้ วันที่ 03-12-2012 เกี่ยวกับ RVC สองอัน Case 2,8 ต้องแสดงมูลค่า ที่เหลือไม่แสดง
                            'begin RVC
                            rpt.txtCheck_CaseRVCCount.text = CheckCount_RVCByAJ(Request.QueryString("INVH_RUN_AUTO"), FormType_name)
                            rpt.txtDisplayUDS7.text = Check_DisplayUSD7(Request.QueryString("INVH_RUN_AUTO"))
                                'end RVC 

                                ''ByTine 12-10-2559 B2B
                                'rpt._TempB2B = ReportPrintClass.LoadDataForB2B(Request.QueryString("INVH_RUN_AUTO"))

                        Case "FORM4_81" 'เก่า
                            rpt = New rpt3_ediFORM4_8_pr
                        Case "FORM4_8" 'ใหม่
                            Select Case Check_Form4_8(Request.QueryString("INVH_RUN_AUTO"))
                                Case True
                                    rpt = New rpt3_ediFORM4_8_New_pr
                                    'เงื่อนไขใหม่ ปรับแก้ วันที่ 03-12-2012 เกี่ยวกับ RVC สองอัน Case 2,8 ต้องแสดงมูลค่า ที่เหลือไม่แสดง
                                    'begin RVC
                                    rpt.txtCheck_CaseRVCCount.text = CheckCount_RVC(Request.QueryString("INVH_RUN_AUTO"), FormType_name)
                                    rpt.txtDisplayUDS7.text = Check_DisplayUSD7(Request.QueryString("INVH_RUN_AUTO"))
                                        'end RVC  
                                        '============

                                        ''ByTine 12-10-2559 B2B
                                        'rpt._TempB2B = ReportPrintClass.LoadDataForB2B(Request.QueryString("INVH_RUN_AUTO"))

                                Case False
                                    rpt = New rpt3_ediFORM4_8_pr
                            End Select

                        Case "FORM4_9"
                            rpt = New rpt3_ediFORM4_9_DC
                            rpt._TempB2B = ReportPrintClass.LoadDataForB2B(Request.QueryString("INVH_RUN_AUTO"))
                        Case "FORMAI_ESS"
                            rpt = New rpt3_ediFORMAI_ESS_PR
                            rpt._TempB2B = ReportPrintClass.LoadDataForB2B(Request.QueryString("INVH_RUN_AUTO"))
                        Case "FORM4_91"
                            rpt = New rpt3_ediFORM4_91_pr

                                ''ByTine 22-03-2559 ฟอร์ม AANZ
                        Case "FORM4_911"
                            rpt = New rpt3_ediFORM4_911_pr
                            'เงื่อนไขใหม่ ปรับแก้ วันที่ 22-03-2559 เกี่ยวกับ RVC สองอัน Case 4 ต้องแสดงมูลค่า ที่เหลือไม่แสดง
                            'begin RVC
                            rpt.txtCheck_CaseRVCCount.text = CheckCount_RVC(Request.QueryString("INVH_RUN_AUTO"), FormType_name)
                                'end RVC  
                                '============

                        Case "FORM5"
                            rpt = New rpt3_ediFORM5_pr
                        Case "FORM5_ESS"
                            rpt = New rpt3_ediFORM5_pr
                        Case "FORM5_1"
                            rpt = New rpt3_ediFORM5_1_pr
                        Case "FORMTP_ESS"
                            rpt = New rpt3_ediFORMTP_ESS_pr
                                ''ByTine 15-10-2558 เพิ่มฟอร์มใหม่ ไทย-ชิลี
                        Case "FORM5_2"
                            rpt = New rpt3_ediFORM5_2_pr
                            'เงื่อนไขใหม่ ปรับแก้ วันที่ 15-10-2015 เกี่ยวกับ RVC สองอัน Case 2 ต้องแสดงมูลค่า ที่เหลือไม่แสดง
                            'begin RVC
                            rpt.txtCheck_CaseRVCCount.text = CheckCount_RVC(Request.QueryString("INVH_RUN_AUTO"), FormType_name)
                            rpt.txtDisplayUDS7.text = Check_DisplayUSD7(Request.QueryString("INVH_RUN_AUTO"))
                            'end RVC  
                            '============

                            ''ByTine 04-05-2559 เพิ่มเติมกรณีไม่ต้องการแสดง Inv ต่างประเทศ
                            If DBNull.Value.Equals(ds.Tables(0).Rows(0).Item("NoneDisplayInv10")) = True Then
                                rpt.ChkNoneDisplayInv10.Checked = False
                            Else
                                rpt.ChkNoneDisplayInv10.Checked = ds.Tables(0).Rows(0).Item("NoneDisplayInv10")
                            End If
                        Case "FORMTC_ESS"
                            rpt = New rpt3_ediFORMTC_ESS_pr
                            'เงื่อนไขใหม่ ปรับแก้ วันที่ 15-10-2015 เกี่ยวกับ RVC สองอัน Case 2 ต้องแสดงมูลค่า ที่เหลือไม่แสดง
                            'begin RVC
                            rpt.txtCheck_CaseRVCCount.text = CheckCount_RVC(Request.QueryString("INVH_RUN_AUTO"), FormType_name)
                            rpt.txtDisplayUDS7.text = Check_DisplayUSD7(Request.QueryString("INVH_RUN_AUTO"))
                            'end RVC  
                            '============

                            ''ByTine 04-05-2559 เพิ่มเติมกรณีไม่ต้องการแสดง Inv ต่างประเทศ
                            If DBNull.Value.Equals(ds.Tables(0).Rows(0).Item("NoneDisplayInv10")) = True Then
                                rpt.ChkNoneDisplayInv10.Checked = False
                            Else
                                rpt.ChkNoneDisplayInv10.Checked = ds.Tables(0).Rows(0).Item("NoneDisplayInv10")
                            End If
                        Case "FORM6"
                            rpt = New rpt3_ediFORM6_pr
                        Case "FORM7"
                            rpt = New rpt3_ediFORM8_7_pr
                        Case "FORM8"
                            rpt = New rpt3_ediFORM8
                        Case "FORM9"
                            rpt = New rpt3_ediFORM9
                    End Select

                    If Not rpt Is Nothing Then
                        'Select Case FormType_name.ToUpper
                        '    Case "FORM1", "FORM1_1", "FORM1_2", "FORM1_3", "FORM2", "FORMRussia".ToUpper, "FORM4_2", "FORM4_3", "FORM4_4", "FORM4_5", "FORM4_8", "FORM5_1"
                        '        rpt.PageSettings.PaperKind = Drawing.Printing.PaperKind.A4
                        '        rpt.Document.Printer.PrinterName = ""

                        '    Case Else
                        '        rpt.PageSettings.PaperKind = Drawing.Printing.PaperKind.Custom
                        '        rpt.Document.Printer.PrinterName = ""
                        'End Select

                        Select Case FormType_name.ToUpper
                            Case "FORM44_02", "FORM44_44"
                                rpt.PageSettings.PaperKind = Drawing.Printing.PaperKind.Custom
                                rpt.Document.Printer.PrinterName = ""

                            Case Else
                                rpt.PageSettings.PaperKind = Drawing.Printing.PaperKind.A4
                                rpt.Document.Printer.PrinterName = ""
                        End Select

                        'rpt.PageSettings.PaperKind = Drawing.Printing.PaperKind.A4
                        'rpt.Document.Printer.PrinterName = ""

                        rpt.C_TotalRowDe.Text = ds.Tables(0).Rows.Count

                        '//Issue Date depend on Print from Date 
                        Select Case FormType_name
                            Case "FORM2_2", "FORM2_3", "FORM2_6", "FORM2_4", "FORM2_5"
                                rpt.txtTemp_ProvinceName.Text = DataUtil.ConvertToString(ds.Tables(0).Rows(0).Item("company_province")) & "  " &
                                                            GetIssueDate(ds.Tables(0).Rows(0).Item("approve_date"), ds.Tables(0).Rows(0).Item("printFormDate"))

                            Case "FORM6", "FORM7", "FORM8", "FORM9"
                            Case "FORM3", "FORM3_1"
                                rpt.txtTemp_Company_province.Text = DataUtil.ConvertToString(ds.Tables(0).Rows(0).Item("company_province")) & "  " &
                                                            GetIssueDate(ds.Tables(0).Rows(0).Item("approve_date"), ds.Tables(0).Rows(0).Item("printFormDate"))

                            Case Else
                                rpt.txtcompany_provincefoot.Text = DataUtil.ConvertToString(ds.Tables(0).Rows(0).Item("company_province")) & "  " &
                                                                GetIssueDate(ds.Tables(0).Rows(0).Item("approve_date"), ds.Tables(0).Rows(0).Item("printFormDate"))
                        End Select

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

    Public Function GetIssueDate(approve_date As Object, print_date As Object) As String
        Dim ret As String = Date.Now.ToString("dd/MM/yyyy")
        Try
            If DataUtil.ConvertToString(approve_date) <> "" Then
                ret = Convert.ToDateTime(approve_date).ToString("dd/MM/yyyy")
            ElseIf DataUtil.ConvertToString(print_date) <> "" Then
                ret = Convert.ToDateTime(print_date).ToString("dd/MM/yyyy")
            End If
        Catch ex As Exception

        End Try

        Return ret
    End Function

End Class