Imports Microsoft.VisualBasic
Imports Microsoft.ApplicationBlocks.Data
Imports System.Data.SqlClient
Imports System.Data
Imports System.Text

Imports System.Drawing

Imports DFT.Dotnetnuke.ClassLibrary
Public Class ReportPrintClass
    'Public Shared Function GetDataLoadTrd(ByVal INVH_RUN_AUTO As String, ByVal SITE_ID As String) As SqlDataReader
    '    Dim conn As String = ConfigurationManager.ConnectionStrings("OriginConnection").ConnectionString

    '    Dim dr As SqlDataReader = SqlHelper.ExecuteReader(conn, CommandType.StoredProcedure, "vi_form4_edi_printFormBar_NewDS", New SqlParameter("@INVH_RUN_AUTO", INVH_RUN_AUTO), New SqlParameter("@SITE_ID", SITE_ID))

    '    If dr.HasRows Then
    '        Return dr
    '    Else
    '        Return dr
    '    End If
    'End Function
    ''--------------------------------------------------------------------------------------------------

    Public Shared Function GetDataLoad_EDI_ReForm2_1(ByVal Send_INVH_RUN_AUTO As String, ByVal Send_SITE_ID As String) As SqlDataReader
        Dim strEDIConn As String = ConfigurationManager.ConnectionStrings("EDIConnection").ConnectionString
        Dim dr As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "vi_form4_edi_printPDF_NewDS", New SqlParameter("@INVH_RUN_AUTO", Send_INVH_RUN_AUTO))

        Return dr

    End Function

    Sub ClearReport(ByVal nameReport)
        nameReport.ducument.clear()

    End Sub
    ''--------------------------------------------------------------------------------------------------
    Public Shared Function printMultireport(ByVal sendFormType As String, ByVal Cells As String, ByVal SiteID As String, ByVal sendRadio_Form As String, ByVal sendUser As String, ByVal sendRequest_print As String, ByVal PrintFlag As String) As Boolean
        Dim strEDIConn As String = ConfigurationManager.ConnectionStrings("EDIConnection").ConnectionString

        Dim dsRequestDetails As New DataSet

        '============== Part Report ==============
        dsRequestDetails = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, "vi_form4_edi_printFormBar_NewDS", _
                        New SqlParameter("@INVH_RUN_AUTO", Cells), _
                        New SqlParameter("@SITE_ID", SiteID))
        'checkUpdate_user(CommonUtility.Get_StringValue(Cells), CommonUtility.Get_StringValue(sendUser), sendRadio_Form)
        'sompol
        ''If PrintFlag = "N" And sendFormType = "FORM2_4" Then
        ''    For i As Integer = 0 To dsRequestDetails.Tables(0).Rows.Count - 1
        ''        With dsRequestDetails.Tables(0).Rows(i)
        ''            Dim dsStock As New DataSet
        ''            dsStock = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, "s_Insert_TransStock_NewDS", _
        ''            New SqlParameter("@TSK_YEAR", CommonUtility.Get_StringValue(Now.Year)), _
        ''            New SqlParameter("@invh_run_auto", CommonUtility.Get_StringValue(.Item("invh_run_auto"))), _
        ''            New SqlParameter("@invd_run_auto", CommonUtility.Get_StringValue(.Item("invd_run_auto"))), _
        ''            New SqlParameter("@TSK_Date", CommonUtility.Get_DateTime(Now)), _
        ''            New SqlParameter("@TSK_Type1", CommonUtility.Get_StringValue("C")), _
        ''            New SqlParameter("@TTS_Code", CommonUtility.Get_StringValue("002")), _
        ''            New SqlParameter("@TSK_Desc", CommonUtility.Get_StringValue("�����͡")), _
        ''            New SqlParameter("@unit_code", CommonUtility.Get_StringValue("KGM")), _
        ''            New SqlParameter("@TSK_Debit", CommonUtility.Get_Decimal(0)), _
        ''            New SqlParameter("@TSK_Credit", CommonUtility.Get_Decimal(.Item("net_weight"))), _
        ''            New SqlParameter("@TSK_Amount", CommonUtility.Get_Decimal(0)), _
        ''            New SqlParameter("@company_taxno", CommonUtility.Get_StringValue(.Item("company_taxno"))), _
        ''            New SqlParameter("@CompanyName_En", CommonUtility.Get_StringValue(.Item("company_name"))), _
        ''            New SqlParameter("@reference_code2", CommonUtility.Get_StringValue(.Item("reference_code2"))), _
        ''            New SqlParameter("@Quantity", CommonUtility.Get_Decimal(0)), _
        ''            New SqlParameter("@ReferenceDesc1", CommonUtility.Get_StringValue("&nbsp;")), _
        ''            New SqlParameter("@ReferenceDesc2", CommonUtility.Get_StringValue("&nbsp;")), _
        ''            New SqlParameter("@user_id", CommonUtility.Get_StringValue(sendUser)))
        ''        End With
        ''    Next
        ''End If


        If dsRequestDetails.Tables(0).Rows.Count > 0 Then

            '�������
            'Dim dsRFCard As New DataSet
            'Dim strCommand As String = "SELECT * FROM rfcard Where card_id = '" & CommonUtility.Get_String(dsRequestDetails.Tables(0).Rows(0).Item("card_id")) & "'"
            'dsRFCard = SqlHelper.ExecuteDataset(strEDIConn, CommandType.Text, strCommand)
            '�������
            'Dim dsRequestSummary As New DataSet
            'dsRequestSummary = SqlHelper.ExecuteDataset(strEDIConn, CommandType.Text, "SELECT SUM(gross_weight) As gross_weight FROM form_detail_edi Where invh_run_auto = '" & Cells & "'")

            '========================================
            'Dim PrintRpt As Object

            Try

                If sendRadio_Form = "0" Then
                    ''Print �Ӣ�
                    Select Case sendFormType
                        Case "FORM1"
                            Dim rpt = New rpt3_ReEdi_A
                            rpt.Document.Printer.PrinterName = PrintReceipt(sendFormType, SiteID, sendRequest_print, sendUser) 'ConfigurationManager.AppSettings("printNameCall").ToString()

                            'Dim dataReadRfcard As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "vi_Nr_Rfcard_GetAll", New SqlParameter("@card_id ", CommonUtility.Get_String(dsRequestDetails.Tables(0).Rows(0).Item("card_id"))))

                            'Dim dataRead_Sum As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "sp_getSummaryPrintForm", New SqlParameter("@invh_run_auto ", CommonUtility.Get_String(Cells)))

                            '========================================
                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then
                                'If dataRead_Sum.Read Then
                                '    'rpt.txtSum_Gross_Weight.Text = dataRead_Sum.Item("Gross_Weight").ToString()
                                '    'rpt.txtSum_g_Unit_Desc.Text = dataRead_Sum.Item("g_Unit_Desc").ToString()

                                '    dataRead_Sum.Close()
                                'End If
                                'rpt.Document.clear()�������

                                rpt.DataSource = dsRequestDetails.Tables(0)

                                'rpt.PageSettings.PaperKind = Printing.PaperKind.Custom
                                'rpt.PageSettings.PaperHeight = 30.5
                                'rpt.PageSettings.PaperWidth = 21

                                rpt.Run()
                                rpt.Document.Print(False, False) 'set false ������������˹�����͡����ͧ printer

                                Return True
                            Else
                                Return False
                            End If

                        Case "FORMRussia"

                            Dim rpt = New rpt3_ReEdi_A
                            rpt.Document.Printer.PrinterName = PrintReceipt(sendFormType, SiteID, sendRequest_print, sendUser) 'ConfigurationManager.AppSettings("printNameCall").ToString()

                            'Dim dataReadRfcard As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "vi_Nr_Rfcard_GetAll", New SqlParameter("@card_id ", CommonUtility.Get_String(dsRequestDetails.Tables(0).Rows(0).Item("card_id"))))

                            'Dim dataRead_Sum As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "sp_getSummaryPrintForm", New SqlParameter("@invh_run_auto ", CommonUtility.Get_String(Cells)))

                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then
                                'If dataRead_Sum.Read Then
                                '    'rpt.txtSum_Gross_Weight.Text = dataRead_Sum.Item("Gross_Weight").ToString()
                                '    'rpt.txtSum_g_Unit_Desc.Text = dataRead_Sum.Item("g_Unit_Desc").ToString()

                                '    dataRead_Sum.Close()
                                'End If

                                rpt.DataSource = dsRequestDetails.Tables(0)
                                'rForm1.PageSettings.PaperKind = Printing.PaperKind.Custom
                                'rForm1.PageSettings.PaperHeight = 30.5
                                'rForm1.PageSettings.PaperWidth = 21

                                rpt.Run()
                                rpt.Document.Print(False, False) 'set false ������������˹�����͡����ͧ printer

                                Return True
                            Else
                                Return False
                            End If
                        Case "FORM1_1"

                            Dim rpt = New rpt3_ReEdi_A
                            rpt.Document.Printer.PrinterName = PrintReceipt(sendFormType, SiteID, sendRequest_print, sendUser) 'ConfigurationManager.AppSettings("printNameCall").ToString()

                            'Dim dataReadRfcard As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "vi_Nr_Rfcard_GetAll", New SqlParameter("@card_id ", CommonUtility.Get_String(dsRequestDetails.Tables(0).Rows(0).Item("card_id"))))

                            'Dim dataRead_Sum As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "sp_getSummaryPrintForm", New SqlParameter("@invh_run_auto ", CommonUtility.Get_String(Cells)))

                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then
                                'If dataRead_Sum.Read Then
                                '    'rpt.txtSum_Gross_Weight.Text = dataRead_Sum.Item("Gross_Weight").ToString()
                                '    'rpt.txtSum_g_Unit_Desc.Text = dataRead_Sum.Item("g_Unit_Desc").ToString()

                                '    dataRead_Sum.Close()
                                'End If
                                rpt.DataSource = dsRequestDetails.Tables(0)
                                'rForm1.PageSettings.PaperKind = Printing.PaperKind.Custom
                                'rForm1.PageSettings.PaperHeight = 30.5
                                'rForm1.PageSettings.PaperWidth = 21

                                rpt.Run()
                                rpt.Document.Print(False, False) 'set false ������������˹�����͡����ͧ printer
                                Return True
                            Else
                                Return False
                            End If
                        Case "FORM1_2"

                            Dim rpt = New rpt3_ReEdi_A
                            rpt.Document.Printer.PrinterName = PrintReceipt(sendFormType, SiteID, sendRequest_print, sendUser) 'ConfigurationManager.AppSettings("printNameCall").ToString()

                            'Dim dataReadRfcard As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "vi_Nr_Rfcard_GetAll", New SqlParameter("@card_id ", CommonUtility.Get_String(dsRequestDetails.Tables(0).Rows(0).Item("card_id"))))

                            'Dim dataRead_Sum As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "sp_getSummaryPrintForm", New SqlParameter("@invh_run_auto ", CommonUtility.Get_String(Cells)))

                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then
                                'If dataRead_Sum.Read Then
                                '    'rpt.txtSum_Gross_Weight.Text = dataRead_Sum.Item("Gross_Weight").ToString()
                                '    'rpt.txtSum_g_Unit_Desc.Text = dataRead_Sum.Item("g_Unit_Desc").ToString()

                                '    dataRead_Sum.Close()
                                'End If
                                rpt.DataSource = dsRequestDetails.Tables(0)
                                'rForm1.PageSettings.PaperKind = Printing.PaperKind.Custom
                                'rForm1.PageSettings.PaperHeight = 30.5
                                'rForm1.PageSettings.PaperWidth = 21

                                rpt.Run()
                                rpt.Document.Print(False, False) 'set false ������������˹�����͡����ͧ printer
                                Return True
                            Else
                                Return False
                            End If
                        Case "FORM1_3"

                            Dim rpt = New rpt3_ReEdi_A
                            rpt.Document.Printer.PrinterName = PrintReceipt(sendFormType, SiteID, sendRequest_print, sendUser) 'ConfigurationManager.AppSettings("printNameCall").ToString()

                            'Dim dataReadRfcard As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "vi_Nr_Rfcard_GetAll", New SqlParameter("@card_id ", CommonUtility.Get_String(dsRequestDetails.Tables(0).Rows(0).Item("card_id"))))

                            'Dim dataRead_Sum As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "sp_getSummaryPrintForm", New SqlParameter("@invh_run_auto ", CommonUtility.Get_String(Cells)))

                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then
                                'If dataRead_Sum.Read Then
                                '    'rpt.txtSum_Gross_Weight.Text = dataRead_Sum.Item("Gross_Weight").ToString()
                                '    'rpt.txtSum_g_Unit_Desc.Text = dataRead_Sum.Item("g_Unit_Desc").ToString()

                                '    dataRead_Sum.Close()
                                'End If
                                rpt.DataSource = dsRequestDetails.Tables(0)
                                'rForm1.PageSettings.PaperKind = Printing.PaperKind.Custom
                                'rForm1.PageSettings.PaperHeight = 30.5
                                'rForm1.PageSettings.PaperWidth = 21

                                rpt.Run()
                                rpt.Document.Print(False, False) 'set false ������������˹�����͡����ͧ printer
                                Return True
                            Else
                                Return False
                            End If
                        Case "FORM1_4"

                            Dim rpt = New rpt3_ReEdi_A
                            rpt.Document.Printer.PrinterName = PrintReceipt(sendFormType, SiteID, sendRequest_print, sendUser) 'ConfigurationManager.AppSettings("printNameCall").ToString()

                            'Dim dataReadRfcard As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "vi_Nr_Rfcard_GetAll", New SqlParameter("@card_id ", CommonUtility.Get_String(dsRequestDetails.Tables(0).Rows(0).Item("card_id"))))

                            'Dim dataRead_Sum As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "sp_getSummaryPrintForm", New SqlParameter("@invh_run_auto ", CommonUtility.Get_String(Cells)))

                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then
                                'If dataRead_Sum.Read Then
                                '    'rpt.txtSum_Gross_Weight.Text = dataRead_Sum.Item("Gross_Weight").ToString()
                                '    'rpt.txtSum_g_Unit_Desc.Text = dataRead_Sum.Item("g_Unit_Desc").ToString()

                                '    dataRead_Sum.Close()
                                'End If
                                rpt.DataSource = dsRequestDetails.Tables(0)
                                'rForm1.PageSettings.PaperKind = Printing.PaperKind.Custom
                                'rForm1.PageSettings.PaperHeight = 30.5
                                'rForm1.PageSettings.PaperWidth = 21

                                rpt.Run()
                                rpt.Document.Print(False, False) 'set false ������������˹�����͡����ͧ printer
                                Return True
                            Else
                                Return False
                            End If
                        Case "FORM2"

                            Dim rpt = New rpt3_ReEdi_A
                            rpt.Document.Printer.PrinterName = PrintReceipt(sendFormType, SiteID, sendRequest_print, sendUser) 'ConfigurationManager.AppSettings("printNameCall").ToString()

                            'Dim dataReadRfcard As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "vi_Nr_Rfcard_GetAll", New SqlParameter("@card_id ", CommonUtility.Get_String(dsRequestDetails.Tables(0).Rows(0).Item("card_id"))))

                            'Dim dataRead_Sum As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "sp_getSummaryPrintForm", New SqlParameter("@invh_run_auto ", CommonUtility.Get_String(Cells)))

                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then
                                'If dataRead_Sum.Read Then
                                '    'rpt.txtSum_Gross_Weight.Text = dataRead_Sum.Item("Gross_Weight").ToString()
                                '    'rpt.txtSum_g_Unit_Desc.Text = dataRead_Sum.Item("g_Unit_Desc").ToString()

                                '    dataRead_Sum.Close()
                                'End If
                                rpt.DataSource = dsRequestDetails.Tables(0)
                                'rForm1.PageSettings.PaperKind = Printing.PaperKind.Custom
                                'rForm1.PageSettings.PaperHeight = 30.5
                                'rForm1.PageSettings.PaperWidth = 21

                                rpt.Run()
                                rpt.Document.Print(False, False) 'set false ������������˹�����͡����ͧ printer
                                Return True
                            Else
                                Return False
                            End If
                        Case "FORM2_1"
                            Dim rpt1 As rpt3_ReEdi_FormST6_1 = New rpt3_ReEdi_FormST6_1
                            rpt1.Document.Printer.PrinterName = PrintReceipt(sendFormType, SiteID, sendRequest_print, sendUser) 'ConfigurationManager.AppSettings("printNameCall").ToString()
                            'Dim dataReadRfcard As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "vi_Nr_Rfcard_GetAll", New SqlParameter("@card_id ", CommonUtility.Get_String(dsRequestDetails.Tables(0).Rows(0).Item("card_id"))))

                            'Dim dataRead_Sum As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "sp_getSummaryPrintForm", New SqlParameter("@invh_run_auto ", CommonUtility.Get_String(Cells)))

                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then
                                rpt1.DataSource = GetDataLoad_EDI_ReForm2_1(Cells, SiteID)
                                '====================
                                rpt1.Run(False)
                                '=================
                                rpt1.Document.Print(False, False)
                                Return True
                            Else
                                Return False
                            End If
                            'Dim counter As Integer
                            'Dim rpt1 As rpt3_ReEdi_FormST6_1 = New rpt3_ReEdi_FormST6_1
                            'Dim rpt2 As rpt3_ReEdi_FormST6_2 = New rpt3_ReEdi_FormST6_2
                            'Dim rpt0 As rpt3_ReEdi_FormST6 = New rpt3_ReEdi_FormST6

                            'rpt0.Document.Printer.PrinterName = PrintReceipt(sendFormType, SiteID) 'ConfigurationManager.AppSettings("printNameCall").ToString()


                            'rpt1.DataSource = GetDataLoad_EDI_ReForm2_1(Cells, SiteID)
                            ''====================
                            'rpt1.Run(False)
                            ''=================


                            'rpt2.DataSource = GetDataLoad_EDI_ReForm2_1(Cells, SiteID)
                            ''=================
                            'rpt2.Run(False)



                            'counter = 0

                            'Dim b As Integer = 0
                            'Do While b <= rpt2.Document.Pages.Count - 1
                            '    Dim Numpage As Integer = (rpt2.Document.Pages.Count - 1) - b
                            '    rpt0.Document.Pages.Insert(counter, rpt2.Document.Pages(Numpage))
                            '    b += 1
                            'Loop

                            'counter = 0

                            'Dim a As Integer = 0
                            'Do While a <= rpt1.Document.Pages.Count - 1
                            '    rpt0.Document.Pages.Insert(counter, rpt1.Document.Pages(a))
                            '    a += 1
                            'Loop

                            'rpt0.Run()
                            'rpt0.Document.Print(False, False)


                            'Dim ds As New DataSet
                            'ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, "sp_update_totalPrintPage", New SqlParameter("@INVH_RUN_AUTO", CommonUtility.Get_StringValue(Cells)), New SqlParameter("@TOTAL", 1))

                            'Dim rpt = New rpt3_ReEdi_A
                            'rpt.Document.Printer.PrinterName = PrintComplete(sendFormType, SiteID) 'ConfigurationManager.AppSettings("printNameCall").ToString()

                            'Dim dataReadRfcard As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "vi_Nr_Rfcard_GetAll", New SqlParameter("@card_id ", CommonUtility.Get_String(dsRequestDetails.Tables(0).Rows(0).Item("card_id"))))

                            'Dim dataRead_Sum As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "sp_getSummaryPrintForm", New SqlParameter("@invh_run_auto ", CommonUtility.Get_String(Cells)))

                            'If dsRequestDetails.Tables(0).Rows.Count > 0 Then
                            '    If dataRead_Sum.Read Then
                            '        rpt.txtSum_Gross_Weight.Text = dataRead_Sum.Item("Gross_Weight").ToString()
                            '        'rpt.txtSum_g_Unit_Desc.Text = dataRead_Sum.Item("g_Unit_Desc").ToString()

                            '        dataRead_Sum.Close()
                            '    End If
                            '    rpt.DataSource = dsRequestDetails.Tables(0)
                            '    'rForm1.PageSettings.PaperKind = Printing.PaperKind.Custom
                            '    'rForm1.PageSettings.PaperHeight = 30.5
                            '    'rForm1.PageSettings.PaperWidth = 21

                            '    rpt.Run()
                            '    rpt.Document.Print(False, False) 'set false ������������˹�����͡����ͧ printer
                            '    Return True
                            'Else
                            '    Return False
                            'End If
                        Case "FORM2_2"

                            Dim rpt = New rpt3_ReEdi_A
                            rpt.Document.Printer.PrinterName = PrintReceipt(sendFormType, SiteID, sendRequest_print, sendUser) 'ConfigurationManager.AppSettings("printNameCall").ToString()

                            'Dim dataReadRfcard As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "vi_Nr_Rfcard_GetAll", New SqlParameter("@card_id ", CommonUtility.Get_String(dsRequestDetails.Tables(0).Rows(0).Item("card_id"))))

                            'Dim dataRead_Sum As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "sp_getSummaryPrintForm", New SqlParameter("@invh_run_auto ", CommonUtility.Get_String(Cells)))

                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then
                                'If dataRead_Sum.Read Then
                                '    'rpt.txtSum_Gross_Weight.Text = dataRead_Sum.Item("Gross_Weight").ToString()
                                '    'rpt.txtSum_g_Unit_Desc.Text = dataRead_Sum.Item("g_Unit_Desc").ToString()

                                '    dataRead_Sum.Close()
                                'End If
                                rpt.DataSource = dsRequestDetails.Tables(0)
                                'rForm1.PageSettings.PaperKind = Printing.PaperKind.Custom
                                'rForm1.PageSettings.PaperHeight = 30.5
                                'rForm1.PageSettings.PaperWidth = 21

                                rpt.Run()
                                rpt.Document.Print(False, False) 'set false ������������˹�����͡����ͧ printer
                                Return True
                            Else
                                Return False
                            End If
                        Case "FORM2_3"

                            Dim rpt = New rpt3_ReEdi_A
                            rpt.Document.Printer.PrinterName = PrintReceipt(sendFormType, SiteID, sendRequest_print, sendUser) 'ConfigurationManager.AppSettings("printNameCall").ToString()

                            'Dim dataReadRfcard As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "vi_Nr_Rfcard_GetAll", New SqlParameter("@card_id ", CommonUtility.Get_String(dsRequestDetails.Tables(0).Rows(0).Item("card_id"))))

                            'Dim dataRead_Sum As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "sp_getSummaryPrintForm", New SqlParameter("@invh_run_auto ", CommonUtility.Get_String(Cells)))

                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then
                                'If dataRead_Sum.Read Then
                                '    'rpt.txtSum_Gross_Weight.Text = dataRead_Sum.Item("Gross_Weight").ToString()
                                '    'rpt.txtSum_g_Unit_Desc.Text = dataRead_Sum.Item("g_Unit_Desc").ToString()

                                '    dataRead_Sum.Close()
                                'End If
                                rpt.DataSource = dsRequestDetails.Tables(0)
                                'rForm1.PageSettings.PaperKind = Printing.PaperKind.Custom
                                'rForm1.PageSettings.PaperHeight = 30.5
                                'rForm1.PageSettings.PaperWidth = 21

                                rpt.Run()
                                rpt.Document.Print(False, False) 'set false ������������˹�����͡����ͧ printer
                                Return True
                            Else
                                Return False
                            End If
                        Case "FORM2_4" '���������

                            Dim rpt = New rpt3_ReEdi_A_24
                            rpt.Document.Printer.PrinterName = PrintReceipt(sendFormType, SiteID, sendRequest_print, sendUser) 'ConfigurationManager.AppSettings("printNameCall").ToString()

                            'Dim dataReadRfcard As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "vi_Nr_Rfcard_GetAll", New SqlParameter("@card_id ", CommonUtility.Get_String(dsRequestDetails.Tables(0).Rows(0).Item("card_id"))))

                            'Dim dataRead_Sum As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "sp_getSummaryPrintForm", New SqlParameter("@invh_run_auto ", CommonUtility.Get_String(Cells)))

                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then
                                'If dataRead_Sum.Read Then
                                '    'rpt.txtSum_Gross_Weight.Text = dataRead_Sum.Item("Gross_Weight").ToString()
                                '    'rpt.txtSum_g_Unit_Desc.Text = dataRead_Sum.Item("g_Unit_Desc").ToString()

                                '    dataRead_Sum.Close()
                                'End If
                                rpt.DataSource = dsRequestDetails.Tables(0)
                                'rForm1.PageSettings.PaperKind = Printing.PaperKind.Custom
                                'rForm1.PageSettings.PaperHeight = 30.5
                                'rForm1.PageSettings.PaperWidth = 21

                                rpt.Run()
                                rpt.Document.Print(False, False) 'set false ������������˹�����͡����ͧ printer
                                Return True
                            Else
                                Return False
                            End If
                        Case "FORM3"

                            Dim rpt = New rpt3_ReEdi_A
                            rpt.Document.Printer.PrinterName = PrintReceipt(sendFormType, SiteID, sendRequest_print, sendUser) 'ConfigurationManager.AppSettings("printNameCall").ToString()

                            'Dim dataReadRfcard As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "vi_Nr_Rfcard_GetAll", New SqlParameter("@card_id ", CommonUtility.Get_String(dsRequestDetails.Tables(0).Rows(0).Item("card_id"))))

                            'Dim dataRead_Sum As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "sp_getSummaryPrintForm", New SqlParameter("@invh_run_auto ", CommonUtility.Get_String(Cells)))

                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then
                                'If dataRead_Sum.Read Then
                                '    'rpt.txtSum_Gross_Weight.Text = dataRead_Sum.Item("Gross_Weight").ToString()
                                '    'rpt.txtSum_g_Unit_Desc.Text = dataRead_Sum.Item("g_Unit_Desc").ToString()

                                '    dataRead_Sum.Close()
                                'End If
                                rpt.DataSource = dsRequestDetails.Tables(0)
                                'rForm1.PageSettings.PaperKind = Printing.PaperKind.Custom
                                'rForm1.PageSettings.PaperHeight = 30.5
                                'rForm1.PageSettings.PaperWidth = 21

                                rpt.Run()
                                rpt.Document.Print(False, False) 'set false ������������˹�����͡����ͧ printer
                                Return True
                            Else
                                Return False
                            End If
                        Case "FORM3_1"

                            Dim rpt = New rpt3_ReEdi_A
                            rpt.Document.Printer.PrinterName = PrintReceipt(sendFormType, SiteID, sendRequest_print, sendUser) 'ConfigurationManager.AppSettings("printNameCall").ToString()

                            'Dim dataReadRfcard As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "vi_Nr_Rfcard_GetAll", New SqlParameter("@card_id ", CommonUtility.Get_String(dsRequestDetails.Tables(0).Rows(0).Item("card_id"))))

                            'Dim dataRead_Sum As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "sp_getSummaryPrintForm", New SqlParameter("@invh_run_auto ", CommonUtility.Get_String(Cells)))

                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then
                                'If dataRead_Sum.Read Then
                                '    'rpt.txtSum_Gross_Weight.Text = dataRead_Sum.Item("Gross_Weight").ToString()
                                '    'rpt.txtSum_g_Unit_Desc.Text = dataRead_Sum.Item("g_Unit_Desc").ToString()

                                '    dataRead_Sum.Close()
                                'End If
                                rpt.DataSource = dsRequestDetails.Tables(0)
                                'rForm1.PageSettings.PaperKind = Printing.PaperKind.Custom
                                'rForm1.PageSettings.PaperHeight = 30.5
                                'rForm1.PageSettings.PaperWidth = 21

                                rpt.Run()
                                rpt.Document.Print(False, False) 'set false ������������˹�����͡����ͧ printer
                                Return True
                            Else
                                Return False
                            End If
                        Case "FORM4"

                            Dim rpt = New rpt3_ReEdi_A
                            rpt.Document.Printer.PrinterName = PrintReceipt(sendFormType, SiteID, sendRequest_print, sendUser) 'ConfigurationManager.AppSettings("printNameCall").ToString()

                            ' Dim dataReadRfcard As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "vi_Nr_Rfcard_GetAll", New SqlParameter("@card_id ", CommonUtility.Get_String(dsRequestDetails.Tables(0).Rows(0).Item("card_id"))))

                            'Dim dataRead_Sum As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "sp_getSummaryPrintForm", New SqlParameter("@invh_run_auto ", CommonUtility.Get_String(Cells)))

                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then
                                'If dataRead_Sum.Read Then
                                '    'rpt.txtSum_Gross_Weight.Text = dataRead_Sum.Item("Gross_Weight").ToString()
                                '    'rpt.txtSum_g_Unit_Desc.Text = dataRead_Sum.Item("g_Unit_Desc").ToString()

                                '    dataRead_Sum.Close()
                                'End If
                                rpt.DataSource = dsRequestDetails.Tables(0)
                                'rForm1.PageSettings.PaperKind = Printing.PaperKind.Custom
                                'rForm1.PageSettings.PaperHeight = 30.5
                                'rForm1.PageSettings.PaperWidth = 21

                                rpt.Run()
                                rpt.Document.Print(False, False) 'set false ������������˹�����͡����ͧ printer
                                Return True
                            Else
                                Return False
                            End If
                        Case "FORM4_1"

                            Dim rpt = New rpt3_ReEdi_A
                            rpt.Document.Printer.PrinterName = PrintReceipt(sendFormType, SiteID, sendRequest_print, sendUser) 'ConfigurationManager.AppSettings("printNameCall").ToString()

                            ' Dim dataReadRfcard As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "vi_Nr_Rfcard_GetAll", New SqlParameter("@card_id ", CommonUtility.Get_String(dsRequestDetails.Tables(0).Rows(0).Item("card_id"))))

                            ' Dim dataRead_Sum As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "sp_getSummaryPrintForm", New SqlParameter("@invh_run_auto ", CommonUtility.Get_String(Cells)))

                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then
                                'If dataRead_Sum.Read Then
                                '    'rpt.txtSum_Gross_Weight.Text = dataRead_Sum.Item("Gross_Weight").ToString()
                                '    'rpt.txtSum_g_Unit_Desc.Text = dataRead_Sum.Item("g_Unit_Desc").ToString()

                                '    dataRead_Sum.Close()
                                'End If
                                rpt.DataSource = dsRequestDetails.Tables(0)
                                'rForm1.PageSettings.PaperKind = Printing.PaperKind.Custom
                                'rForm1.PageSettings.PaperHeight = 30.5
                                'rForm1.PageSettings.PaperWidth = 21

                                rpt.Run()
                                rpt.Document.Print(False, False) 'set false ������������˹�����͡����ͧ printer
                                Return True
                            Else
                                Return False
                            End If
                        Case "FORM4_2"

                            Dim rpt = New rpt3_ReEdi_A
                            rpt.Document.Printer.PrinterName = PrintReceipt(sendFormType, SiteID, sendRequest_print, sendUser) 'ConfigurationManager.AppSettings("printNameCall").ToString()

                            'Dim dataReadRfcard As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "vi_Nr_Rfcard_GetAll", New SqlParameter("@card_id ", CommonUtility.Get_String(dsRequestDetails.Tables(0).Rows(0).Item("card_id"))))

                            'Dim dataRead_Sum As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "sp_getSummaryPrintForm", New SqlParameter("@invh_run_auto ", CommonUtility.Get_String(Cells)))

                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then
                                'If dataRead_Sum.Read Then
                                '    'rpt.txtSum_Gross_Weight.Text = dataRead_Sum.Item("Gross_Weight").ToString()
                                '    'rpt.txtSum_g_Unit_Desc.Text = dataRead_Sum.Item("g_Unit_Desc").ToString()

                                '    dataRead_Sum.Close()
                                'End If
                                rpt.DataSource = dsRequestDetails.Tables(0)
                                'rForm1.PageSettings.PaperKind = Printing.PaperKind.Custom
                                'rForm1.PageSettings.PaperHeight = 30.5
                                'rForm1.PageSettings.PaperWidth = 21

                                rpt.Run()
                                rpt.Document.Print(False, False) 'set false ������������˹�����͡����ͧ printer
                                Return True
                            Else
                                Return False
                            End If
                        Case "FORM4_3"

                            Dim rpt = New rpt3_ReEdi_A
                            rpt.Document.Printer.PrinterName = PrintReceipt(sendFormType, SiteID, sendRequest_print, sendUser) 'ConfigurationManager.AppSettings("printNameCall").ToString()

                            ' Dim dataReadRfcard As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "vi_Nr_Rfcard_GetAll", New SqlParameter("@card_id ", CommonUtility.Get_String(dsRequestDetails.Tables(0).Rows(0).Item("card_id"))))

                            'Dim dataRead_Sum As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "sp_getSummaryPrintForm", New SqlParameter("@invh_run_auto ", CommonUtility.Get_String(Cells)))

                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then
                                'If dataRead_Sum.Read Then
                                '    'rpt.txtSum_Gross_Weight.Text = dataRead_Sum.Item("Gross_Weight").ToString()
                                '    'rpt.txtSum_g_Unit_Desc.Text = dataRead_Sum.Item("g_Unit_Desc").ToString()

                                '    dataRead_Sum.Close()
                                'End If
                                rpt.DataSource = dsRequestDetails.Tables(0)
                                'rForm1.PageSettings.PaperKind = Printing.PaperKind.Custom
                                'rForm1.PageSettings.PaperHeight = 30.5
                                'rForm1.PageSettings.PaperWidth = 21

                                rpt.Run()
                                rpt.Document.Print(False, False) 'set false ������������˹�����͡����ͧ printer
                                Return True
                            Else
                                Return False
                            End If
                        Case "FORM4_4"

                            Dim rpt = New rpt3_ReEdi_A
                            rpt.Document.Printer.PrinterName = PrintReceipt(sendFormType, SiteID, sendRequest_print, sendUser) 'ConfigurationManager.AppSettings("printNameCall").ToString()

                            'Dim dataReadRfcard As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "vi_Nr_Rfcard_GetAll", New SqlParameter("@card_id ", CommonUtility.Get_String(dsRequestDetails.Tables(0).Rows(0).Item("card_id"))))

                            'Dim dataRead_Sum As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "sp_getSummaryPrintForm", New SqlParameter("@invh_run_auto ", CommonUtility.Get_String(Cells)))

                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then
                                'If dataRead_Sum.Read Then
                                '    'rpt.txtSum_Gross_Weight.Text = dataRead_Sum.Item("Gross_Weight").ToString()
                                '    'rpt.txtSum_g_Unit_Desc.Text = dataRead_Sum.Item("g_Unit_Desc").ToString()

                                '    dataRead_Sum.Close()
                                'End If
                                rpt.DataSource = dsRequestDetails.Tables(0)
                                'rForm1.PageSettings.PaperKind = Printing.PaperKind.Custom
                                'rForm1.PageSettings.PaperHeight = 30.5
                                'rForm1.PageSettings.PaperWidth = 21

                                rpt.Run()
                                rpt.Document.Print(False, False) 'set false ������������˹�����͡����ͧ printer
                                Return True
                            Else
                                Return False
                            End If
                        Case "FORM4_5"

                            Dim rpt = New rpt3_ReEdi_A
                            rpt.Document.Printer.PrinterName = PrintReceipt(sendFormType, SiteID, sendRequest_print, sendUser) 'ConfigurationManager.AppSettings("printNameCall").ToString()

                            'Dim dataReadRfcard As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "vi_Nr_Rfcard_GetAll", New SqlParameter("@card_id ", CommonUtility.Get_String(dsRequestDetails.Tables(0).Rows(0).Item("card_id"))))

                            'Dim dataRead_Sum As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "sp_getSummaryPrintForm", New SqlParameter("@invh_run_auto ", CommonUtility.Get_String(Cells)))

                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then
                                'If dataRead_Sum.Read Then
                                '    'rpt.txtSum_Gross_Weight.Text = dataRead_Sum.Item("Gross_Weight").ToString()
                                '    'rpt.txtSum_g_Unit_Desc.Text = dataRead_Sum.Item("g_Unit_Desc").ToString()

                                '    dataRead_Sum.Close()
                                'End If
                                rpt.DataSource = dsRequestDetails.Tables(0)
                                'rForm1.PageSettings.PaperKind = Printing.PaperKind.Custom
                                'rForm1.PageSettings.PaperHeight = 30.5
                                'rForm1.PageSettings.PaperWidth = 21

                                rpt.Run()
                                rpt.Document.Print(False, False) 'set false ������������˹�����͡����ͧ printer
                                Return True
                            Else
                                Return False
                            End If
                        Case "FORM4_6"

                            Dim rpt = New rpt3_ReEdi_A
                            rpt.Document.Printer.PrinterName = PrintReceipt(sendFormType, SiteID, sendRequest_print, sendUser) 'ConfigurationManager.AppSettings("printNameCall").ToString()

                            'Dim dataReadRfcard As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "vi_Nr_Rfcard_GetAll", New SqlParameter("@card_id ", CommonUtility.Get_String(dsRequestDetails.Tables(0).Rows(0).Item("card_id"))))

                            'Dim dataRead_Sum As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "sp_getSummaryPrintForm", New SqlParameter("@invh_run_auto ", CommonUtility.Get_String(Cells)))

                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then
                                'If dataRead_Sum.Read Then
                                '    'rpt.txtSum_Gross_Weight.Text = dataRead_Sum.Item("Gross_Weight").ToString()
                                '    'rpt.txtSum_g_Unit_Desc.Text = dataRead_Sum.Item("g_Unit_Desc").ToString()

                                '    dataRead_Sum.Close()
                                'End If
                                rpt.DataSource = dsRequestDetails.Tables(0)
                                'rForm1.PageSettings.PaperKind = Printing.PaperKind.Custom
                                'rForm1.PageSettings.PaperHeight = 30.5
                                'rForm1.PageSettings.PaperWidth = 21

                                rpt.Run()
                                rpt.Document.Print(False, False) 'set false ������������˹�����͡����ͧ printer
                                Return True
                            Else
                                Return False
                            End If
                        Case "FORM4_8"

                            Dim rpt = New rpt3_ReEdi_A
                            rpt.Document.Printer.PrinterName = PrintReceipt(sendFormType, SiteID, sendRequest_print, sendUser) 'ConfigurationManager.AppSettings("printNameCall").ToString()

                            'Dim dataReadRfcard As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "vi_Nr_Rfcard_GetAll", New SqlParameter("@card_id ", CommonUtility.Get_String(dsRequestDetails.Tables(0).Rows(0).Item("card_id"))))

                            'Dim dataRead_Sum As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "sp_getSummaryPrintForm", New SqlParameter("@invh_run_auto ", CommonUtility.Get_String(Cells)))

                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then
                                'If dataRead_Sum.Read Then
                                '    'rpt.txtSum_Gross_Weight.Text = dataRead_Sum.Item("Gross_Weight").ToString()
                                '    'rpt.txtSum_g_Unit_Desc.Text = dataRead_Sum.Item("g_Unit_Desc").ToString()

                                '    dataRead_Sum.Close()
                                'End If
                                'set �������Ѻ����ͧ Temp ���
                                'rpt.Document.Pages.Clear()

                                rpt.DataSource = dsRequestDetails.Tables(0)
                                'rForm1.PageSettings.PaperKind = Printing.PaperKind.Custom
                                'rForm1.PageSettings.PaperHeight = 30.5
                                'rForm1.PageSettings.PaperWidth = 21

                                rpt.Run()
                                rpt.Document.Print(False, False) 'set false ������������˹�����͡����ͧ printer
                                Return True
                            Else
                                Return False
                            End If
                        Case "FORM4_9"

                            Dim rpt = New rpt3_ReEdi_A
                            rpt.Document.Printer.PrinterName = PrintReceipt(sendFormType, SiteID, sendRequest_print, sendUser) 'ConfigurationManager.AppSettings("printNameCall").ToString()

                            'Dim dataReadRfcard As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "vi_Nr_Rfcard_GetAll", New SqlParameter("@card_id ", CommonUtility.Get_String(dsRequestDetails.Tables(0).Rows(0).Item("card_id"))))

                            'Dim dataRead_Sum As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "sp_getSummaryPrintForm", New SqlParameter("@invh_run_auto ", CommonUtility.Get_String(Cells)))

                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then
                                'If dataRead_Sum.Read Then
                                '    'rpt.txtSum_Gross_Weight.Text = dataRead_Sum.Item("Gross_Weight").ToString()
                                '    'rpt.txtSum_g_Unit_Desc.Text = dataRead_Sum.Item("g_Unit_Desc").ToString()

                                '    dataRead_Sum.Close()
                                'End If
                                rpt.DataSource = dsRequestDetails.Tables(0)
                                'rForm1.PageSettings.PaperKind = Printing.PaperKind.Custom
                                'rForm1.PageSettings.PaperHeight = 30.5
                                'rForm1.PageSettings.PaperWidth = 21

                                rpt.Run()
                                rpt.Document.Print(False, False) 'set false ������������˹�����͡����ͧ printer
                                Return True
                            Else
                                Return False
                            End If
                        Case "FORM4_91"

                            Dim rpt = New rpt3_ReEdi_A
                            rpt.Document.Printer.PrinterName = PrintReceipt(sendFormType, SiteID, sendRequest_print, sendUser) 'ConfigurationManager.AppSettings("printNameCall").ToString()

                            'Dim dataReadRfcard As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "vi_Nr_Rfcard_GetAll", New SqlParameter("@card_id ", CommonUtility.Get_String(dsRequestDetails.Tables(0).Rows(0).Item("card_id"))))

                            'Dim dataRead_Sum As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "sp_getSummaryPrintForm", New SqlParameter("@invh_run_auto ", CommonUtility.Get_String(Cells)))

                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then
                                'If dataRead_Sum.Read Then
                                '    'rpt.txtSum_Gross_Weight.Text = dataRead_Sum.Item("Gross_Weight").ToString()
                                '    'rpt.txtSum_g_Unit_Desc.Text = dataRead_Sum.Item("g_Unit_Desc").ToString()

                                '    dataRead_Sum.Close()
                                'End If
                                rpt.DataSource = dsRequestDetails.Tables(0)
                                'rForm1.PageSettings.PaperKind = Printing.PaperKind.Custom
                                'rForm1.PageSettings.PaperHeight = 30.5
                                'rForm1.PageSettings.PaperWidth = 21

                                rpt.Run()
                                rpt.Document.Print(False, False) 'set false ������������˹�����͡����ͧ printer
                                Return True
                            Else
                                Return False
                            End If
                        Case "FORM5"

                            Dim rpt = New rpt3_ReEdi_A
                            rpt.Document.Printer.PrinterName = PrintReceipt(sendFormType, SiteID, sendRequest_print, sendUser) 'ConfigurationManager.AppSettings("printNameCall").ToString()

                            'Dim dataReadRfcard As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "vi_Nr_Rfcard_GetAll", New SqlParameter("@card_id ", CommonUtility.Get_String(dsRequestDetails.Tables(0).Rows(0).Item("card_id"))))

                            'Dim dataRead_Sum As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "sp_getSummaryPrintForm", New SqlParameter("@invh_run_auto ", CommonUtility.Get_String(Cells)))

                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then
                                'If dataRead_Sum.Read Then
                                '    'rpt.txtSum_Gross_Weight.Text = dataRead_Sum.Item("Gross_Weight").ToString()
                                '    'rpt.txtSum_g_Unit_Desc.Text = dataRead_Sum.Item("g_Unit_Desc").ToString()

                                '    dataRead_Sum.Close()
                                'End If
                                rpt.DataSource = dsRequestDetails.Tables(0)
                                'rForm1.PageSettings.PaperKind = Printing.PaperKind.Custom
                                'rForm1.PageSettings.PaperHeight = 30.5
                                'rForm1.PageSettings.PaperWidth = 21

                                rpt.Run()
                                rpt.Document.Print(False, False) 'set false ������������˹�����͡����ͧ printer
                                Return True
                            Else
                                Return False
                            End If
                        Case "FORM6"

                            Dim rpt = New rpt3_ReEdi_A
                            rpt.Document.Printer.PrinterName = PrintReceipt(sendFormType, SiteID, sendRequest_print, sendUser) 'ConfigurationManager.AppSettings("printNameCall").ToString()

                            'Dim dataReadRfcard As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "vi_Nr_Rfcard_GetAll", New SqlParameter("@card_id ", CommonUtility.Get_String(dsRequestDetails.Tables(0).Rows(0).Item("card_id"))))

                            'Dim dataRead_Sum As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "sp_getSummaryPrintForm", New SqlParameter("@invh_run_auto ", CommonUtility.Get_String(Cells)))

                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then
                                'If dataRead_Sum.Read Then
                                '    'rpt.txtSum_Gross_Weight.Text = dataRead_Sum.Item("Gross_Weight").ToString()
                                '    'rpt.txtSum_g_Unit_Desc.Text = dataRead_Sum.Item("g_Unit_Desc").ToString()

                                '    dataRead_Sum.Close()
                                'End If
                                rpt.DataSource = dsRequestDetails.Tables(0)
                                'rForm1.PageSettings.PaperKind = Printing.PaperKind.Custom
                                'rForm1.PageSettings.PaperHeight = 30.5
                                'rForm1.PageSettings.PaperWidth = 21

                                rpt.Run()
                                rpt.Document.Print(False, False) 'set false ������������˹�����͡����ͧ printer
                                Return True
                            Else
                                Return False
                            End If
                        Case "FORM7"

                            Dim rpt = New rpt3_ReEdi_A
                            rpt.Document.Printer.PrinterName = PrintReceipt(sendFormType, SiteID, sendRequest_print, sendUser) 'ConfigurationManager.AppSettings("printNameCall").ToString()

                            'Dim dataReadRfcard As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "vi_Nr_Rfcard_GetAll", New SqlParameter("@card_id ", CommonUtility.Get_String(dsRequestDetails.Tables(0).Rows(0).Item("card_id"))))

                            ' Dim dataRead_Sum As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "sp_getSummaryPrintForm", New SqlParameter("@invh_run_auto ", CommonUtility.Get_String(Cells)))

                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then
                                'If dataRead_Sum.Read Then
                                '    'rpt.txtSum_Gross_Weight.Text = dataRead_Sum.Item("Gross_Weight").ToString()
                                '    'rpt.txtSum_g_Unit_Desc.Text = dataRead_Sum.Item("g_Unit_Desc").ToString()

                                '    dataRead_Sum.Close()
                                'End If
                                rpt.DataSource = dsRequestDetails.Tables(0)
                                'rForm1.PageSettings.PaperKind = Printing.PaperKind.Custom
                                'rForm1.PageSettings.PaperHeight = 30.5
                                'rForm1.PageSettings.PaperWidth = 21

                                rpt.Run()
                                rpt.Document.Print(False, False) 'set false ������������˹�����͡����ͧ printer
                                Return True
                            Else
                                Return False
                            End If
                        Case "FORM8"

                            Dim rpt = New rpt3_ReEdi_A
                            rpt.Document.Printer.PrinterName = PrintReceipt(sendFormType, SiteID, sendRequest_print, sendUser) 'ConfigurationManager.AppSettings("printNameCall").ToString()

                            'Dim dataReadRfcard As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "vi_Nr_Rfcard_GetAll", New SqlParameter("@card_id ", CommonUtility.Get_String(dsRequestDetails.Tables(0).Rows(0).Item("card_id"))))

                            'Dim dataRead_Sum As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "sp_getSummaryPrintForm", New SqlParameter("@invh_run_auto ", CommonUtility.Get_String(Cells)))

                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then
                                'If dataRead_Sum.Read Then
                                '    'rpt.txtSum_Gross_Weight.Text = dataRead_Sum.Item("Gross_Weight").ToString()
                                '    'rpt.txtSum_g_Unit_Desc.Text = dataRead_Sum.Item("g_Unit_Desc").ToString()

                                '    dataRead_Sum.Close()
                                'End If
                                rpt.DataSource = dsRequestDetails.Tables(0)
                                'rForm1.PageSettings.PaperKind = Printing.PaperKind.Custom
                                'rForm1.PageSettings.PaperHeight = 30.5
                                'rForm1.PageSettings.PaperWidth = 21

                                rpt.Run()
                                rpt.Document.Print(False, False) 'set false ������������˹�����͡����ͧ printer
                                Return True
                            Else
                                Return False
                            End If
                        Case "FORM9"

                            Dim rpt = New rpt3_ReEdi_A
                            rpt.Document.Printer.PrinterName = PrintReceipt(sendFormType, SiteID, sendRequest_print, sendUser) 'ConfigurationManager.AppSettings("printNameCall").ToString()

                            'Dim dataReadRfcard As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "vi_Nr_Rfcard_GetAll", New SqlParameter("@card_id ", CommonUtility.Get_String(dsRequestDetails.Tables(0).Rows(0).Item("card_id"))))

                            'Dim dataRead_Sum As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "sp_getSummaryPrintForm", New SqlParameter("@invh_run_auto ", CommonUtility.Get_String(Cells)))

                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then
                                'If dataRead_Sum.Read Then
                                '    'rpt.txtSum_Gross_Weight.Text = dataRead_Sum.Item("Gross_Weight").ToString()
                                '    'rpt.txtSum_g_Unit_Desc.Text = dataRead_Sum.Item("g_Unit_Desc").ToString()

                                '    dataRead_Sum.Close()
                                'End If
                                rpt.DataSource = dsRequestDetails.Tables(0)
                                'rForm1.PageSettings.PaperKind = Printing.PaperKind.Custom
                                'rForm1.PageSettings.PaperHeight = 30.5
                                'rForm1.PageSettings.PaperWidth = 21

                                rpt.Run()
                                rpt.Document.Print(False, False) 'set false ������������˹�����͡����ͧ printer
                                Return True
                            Else
                                Return False
                            End If
                        Case Else
                            Return False
                    End Select

                Else
                    'Print ˹ѧ����Ѻ�ͧ
                    Select Case sendFormType
                        Case "FORM1"
                            Dim rpt = New rpt3_ediFORM1_pr
                            rpt.Document.Printer.PrinterName = PrintComplete(sendFormType, SiteID, sendUser) 'ConfigurationManager.AppSettings("printNameCall").ToString()

                            'Dim dataReadRfcard As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "vi_Nr_Rfcard_GetAll", New SqlParameter("@card_id ", CommonUtility.Get_String(dsRequestDetails.Tables(0).Rows(0).Item("card_id"))))

                            'Dim dataRead_Sum As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "sp_getSummaryPrintForm", New SqlParameter("@invh_run_auto ", CommonUtility.Get_String(Cells)))

                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then
                                'If dataRead_Sum.Read Then
                                '    'rpt.txtSum_Gross_Weight.Text = dataRead_Sum.Item("Gross_Weight").ToString()
                                '    'rpt.txtSum_g_Unit_Desc.Text = dataRead_Sum.Item("g_Unit_Desc").ToString()

                                '    dataRead_Sum.Close()
                                'End If
                                rpt.C_TotalRowDe.text = dsRequestDetails.Tables(0).Rows.Count
                                rpt.DataSource = dsRequestDetails.Tables(0) 'ReportPrintClass.GetDataLoadTrd("20090416-000001", "ST-001") 'dsRequestDetail.Tables(0)
                                'rForm1.PageSettings.PaperKind = Printing.PaperKind.Custom
                                'rForm1.PageSettings.PaperHeight = 30.5
                                'rForm1.PageSettings.PaperWidth = 21

                                rpt.Run()
                                rpt.Document.Print(False, False) 'set false ������������˹�����͡����ͧ printer
                                Return True
                            Else
                                Return False
                            End If
                        Case "FORMRussia"
                            'view
                            Dim rpt = New rpt3_ediFORM1RU_pr
                            rpt.Document.Printer.PrinterName = PrintComplete(sendFormType, SiteID, sendUser) 'ConfigurationManager.AppSettings("printNameCall").ToString()

                            'Dim dataReadRfcard As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "vi_Nr_Rfcard_GetAll", New SqlParameter("@card_id ", CommonUtility.Get_String(dsRequestDetails.Tables(0).Rows(0).Item("card_id"))))

                            'Dim dataRead_Sum As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "sp_getSummaryPrintForm", New SqlParameter("@invh_run_auto ", CommonUtility.Get_String(Cells)))

                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then
                                'If dataRead_Sum.Read Then
                                '    'rpt.txtSum_Gross_Weight.Text = dataRead_Sum.Item("Gross_Weight").ToString()
                                '    'rpt.txtSum_g_Unit_Desc.Text = dataRead_Sum.Item("g_Unit_Desc").ToString()

                                '    dataRead_Sum.Close()
                                'End If
                                rpt.C_TotalRowDe.text = dsRequestDetails.Tables(0).Rows.Count
                                rpt.DataSource = dsRequestDetails.Tables(0) 'ReportPrintClass.GetDataLoadTrd("20090416-000001", "ST-001") 'dsRequestDetail.Tables(0)
                                'rForm1.PageSettings.PaperKind = Printing.PaperKind.Custom
                                'rForm1.PageSettings.PaperHeight = 30.5
                                'rForm1.PageSettings.PaperWidth = 21

                                rpt.Run()
                                rpt.Document.Print(False, False) 'set false ������������˹�����͡����ͧ printer
                                Return True
                            Else
                                Return False
                            End If
                        Case "FORM1_1"
                            'view
                            Dim rpt = New rpt3_ediFORM1_1_pr
                            rpt.Document.Printer.PrinterName = PrintComplete(sendFormType, SiteID, sendUser) 'ConfigurationManager.AppSettings("printNameCall").ToString()

                            'Dim dataReadRfcard As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "vi_Nr_Rfcard_GetAll", New SqlParameter("@card_id ", CommonUtility.Get_String(dsRequestDetails.Tables(0).Rows(0).Item("card_id"))))

                            'Dim dataRead_Sum As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "sp_getSummaryPrintForm", New SqlParameter("@invh_run_auto ", CommonUtility.Get_String(Cells)))

                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then
                                'If dataRead_Sum.Read Then
                                '    'rpt.txtSum_Gross_Weight.Text = dataRead_Sum.Item("Gross_Weight").ToString()
                                '    'rpt.txtSum_g_Unit_Desc.Text = dataRead_Sum.Item("g_Unit_Desc").ToString()

                                '    dataRead_Sum.Close()
                                'End If
                                rpt.C_TotalRowDe.text = dsRequestDetails.Tables(0).Rows.Count
                                rpt.DataSource = dsRequestDetails.Tables(0) 'ReportPrintClass.GetDataLoadTrd("20090416-000001", "ST-001") 'dsRequestDetail.Tables(0)
                                'rForm1.PageSettings.PaperKind = Printing.PaperKind.Custom
                                'rForm1.PageSettings.PaperHeight = 30.5
                                'rForm1.PageSettings.PaperWidth = 21

                                rpt.Run()
                                rpt.Document.Print(False, False) 'set false ������������˹�����͡����ͧ printer
                                Return True
                            Else
                                Return False
                            End If
                        Case "FORM1_2"
                            'view
                            Dim rpt = New rpt3_ediFORM1_2_pr
                            rpt.Document.Printer.PrinterName = PrintComplete(sendFormType, SiteID, sendUser) 'ConfigurationManager.AppSettings("printNameCall").ToString()

                            'Dim dataReadRfcard As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "vi_Nr_Rfcard_GetAll", New SqlParameter("@card_id ", CommonUtility.Get_String(dsRequestDetails.Tables(0).Rows(0).Item("card_id"))))

                            'Dim dataRead_Sum As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "sp_getSummaryPrintForm", New SqlParameter("@invh_run_auto ", CommonUtility.Get_String(Cells)))

                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then
                                'If dataRead_Sum.Read Then
                                '    'rpt.txtSum_Gross_Weight.Text = dataRead_Sum.Item("Gross_Weight").ToString()
                                '    'rpt.txtSum_g_Unit_Desc.Text = dataRead_Sum.Item("g_Unit_Desc").ToString()

                                '    dataRead_Sum.Close()
                                'End If
                                rpt.C_TotalRowDe.text = dsRequestDetails.Tables(0).Rows.Count
                                rpt.DataSource = dsRequestDetails.Tables(0) 'ReportPrintClass.GetDataLoadTrd("20090416-000001", "ST-001") 'dsRequestDetail.Tables(0)
                                'rForm1.PageSettings.PaperKind = Printing.PaperKind.Custom
                                'rForm1.PageSettings.PaperHeight = 30.5
                                'rForm1.PageSettings.PaperWidth = 21

                                rpt.Run()
                                rpt.Document.Print(False, False) 'set false ������������˹�����͡����ͧ printer
                                Return True
                            Else
                                Return False
                            End If
                        Case "FORM1_3"
                            'view
                            Dim rpt = New rpt3_ediFORM1_3_pr
                            rpt.Document.Printer.PrinterName = PrintComplete(sendFormType, SiteID, sendUser) 'ConfigurationManager.AppSettings("printNameCall").ToString()

                            'Dim dataReadRfcard As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "vi_Nr_Rfcard_GetAll", New SqlParameter("@card_id ", CommonUtility.Get_String(dsRequestDetails.Tables(0).Rows(0).Item("card_id"))))

                            'Dim dataRead_Sum As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "sp_getSummaryPrintForm", New SqlParameter("@invh_run_auto ", CommonUtility.Get_String(Cells)))

                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then
                                'If dataRead_Sum.Read Then
                                '    'rpt.txtSum_Gross_Weight.Text = dataRead_Sum.Item("Gross_Weight").ToString()
                                '    'rpt.txtSum_g_Unit_Desc.Text = dataRead_Sum.Item("g_Unit_Desc").ToString()

                                '    dataRead_Sum.Close()
                                'End If
                                rpt.C_TotalRowDe.text = dsRequestDetails.Tables(0).Rows.Count
                                rpt.DataSource = dsRequestDetails.Tables(0) 'ReportPrintClass.GetDataLoadTrd("20090416-000001", "ST-001") 'dsRequestDetail.Tables(0)
                                'rForm1.PageSettings.PaperKind = Printing.PaperKind.Custom
                                'rForm1.PageSettings.PaperHeight = 30.5
                                'rForm1.PageSettings.PaperWidth = 21

                                rpt.Run()
                                rpt.Document.Print(False, False) 'set false ������������˹�����͡����ͧ printer
                                Return True
                            Else
                                Return False
                            End If
                        Case "FORM1_4"
                            'view
                            Dim rpt = New rpt3_ediFORM1_4_pr
                            rpt.Document.Printer.PrinterName = PrintComplete(sendFormType, SiteID, sendUser) 'ConfigurationManager.AppSettings("printNameCall").ToString()

                            'Dim dataReadRfcard As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "vi_Nr_Rfcard_GetAll", New SqlParameter("@card_id ", CommonUtility.Get_String(dsRequestDetails.Tables(0).Rows(0).Item("card_id"))))

                            'Dim dataRead_Sum As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "sp_getSummaryPrintForm", New SqlParameter("@invh_run_auto ", CommonUtility.Get_String(Cells)))

                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then
                                'If dataRead_Sum.Read Then
                                '    'rpt.txtSum_Gross_Weight.Text = dataRead_Sum.Item("Gross_Weight").ToString()
                                '    'rpt.txtSum_g_Unit_Desc.Text = dataRead_Sum.Item("g_Unit_Desc").ToString()

                                '    dataRead_Sum.Close()
                                'End If
                                rpt.C_TotalRowDe.text = dsRequestDetails.Tables(0).Rows.Count
                                rpt.DataSource = dsRequestDetails.Tables(0) 'ReportPrintClass.GetDataLoadTrd("20090416-000001", "ST-001") 'dsRequestDetail.Tables(0)
                                'rForm1.PageSettings.PaperKind = Printing.PaperKind.Custom
                                'rForm1.PageSettings.PaperHeight = 30.5
                                'rForm1.PageSettings.PaperWidth = 21

                                rpt.Run()
                                rpt.Document.Print(False, False) 'set false ������������˹�����͡����ͧ printer
                                Return True
                            Else
                                Return False
                            End If
                        Case "FORM2"
                            'view
                            Dim rpt = New rpt3_ediFORM2_pr
                            rpt.Document.Printer.PrinterName = PrintComplete(sendFormType, SiteID, sendUser) 'ConfigurationManager.AppSettings("printNameCall").ToString()

                            'Dim dataReadRfcard As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "vi_Nr_Rfcard_GetAll", New SqlParameter("@card_id ", CommonUtility.Get_String(dsRequestDetails.Tables(0).Rows(0).Item("card_id"))))

                            'Dim dataRead_Sum As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "sp_getSummaryPrintForm", New SqlParameter("@invh_run_auto ", CommonUtility.Get_String(Cells)))

                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then
                                'If dataRead_Sum.Read Then
                                '    'rpt.txtSum_Gross_Weight.Text = dataRead_Sum.Item("Gross_Weight").ToString()
                                '    'rpt.txtSum_g_Unit_Desc.Text = dataRead_Sum.Item("g_Unit_Desc").ToString()

                                '    dataRead_Sum.Close()
                                'End If
                                rpt.C_TotalRowDe.text = dsRequestDetails.Tables(0).Rows.Count
                                rpt.DataSource = dsRequestDetails.Tables(0) 'ReportPrintClass.GetDataLoadTrd("20090416-000001", "ST-001") 'dsRequestDetail.Tables(0)
                                'rForm1.PageSettings.PaperKind = Printing.PaperKind.Custom
                                'rForm1.PageSettings.PaperHeight = 30.5
                                'rForm1.PageSettings.PaperWidth = 21

                                rpt.Run()
                                rpt.Document.Print(False, False) 'set false ������������˹�����͡����ͧ printer
                                Return True
                            Else
                                Return False
                            End If
                        Case "FORM2_1"
                            'view
                            Dim rpt = New rpt3_ediFORM2_1_pr
                            rpt.Document.Printer.PrinterName = PrintComplete(sendFormType, SiteID, sendUser) 'ConfigurationManager.AppSettings("printNameCall").ToString()

                            'Dim dataReadRfcard As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "vi_Nr_Rfcard_GetAll", New SqlParameter("@card_id ", CommonUtility.Get_String(dsRequestDetails.Tables(0).Rows(0).Item("card_id"))))

                            'Dim dataRead_Sum As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "sp_getSummaryPrintForm", New SqlParameter("@invh_run_auto ", CommonUtility.Get_String(Cells)))

                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then
                                'If dataRead_Sum.Read Then
                                '    'rpt.txtSum_Gross_Weight.Text = dataRead_Sum.Item("Gross_Weight").ToString()
                                '    'rpt.txtSum_g_Unit_Desc.Text = dataRead_Sum.Item("g_Unit_Desc").ToString()

                                '    dataRead_Sum.Close()
                                'End If
                                rpt.C_TotalRowDe.text = dsRequestDetails.Tables(0).Rows.Count
                                rpt.DataSource = dsRequestDetails.Tables(0) 'ReportPrintClass.GetDataLoadTrd("20090416-000001", "ST-001") 'dsRequestDetail.Tables(0)
                                'rForm1.PageSettings.PaperKind = Printing.PaperKind.Custom
                                'rForm1.PageSettings.PaperHeight = 30.5
                                'rForm1.PageSettings.PaperWidth = 21

                                rpt.Run()
                                rpt.Document.Print(False, False) 'set false ������������˹�����͡����ͧ printer
                                Return True
                            Else
                                Return False
                            End If
                        Case "FORM2_2"
                            'view
                            Dim rpt = New rpt3_ediFORM2_2_pr
                            rpt.Document.Printer.PrinterName = PrintComplete(sendFormType, SiteID, sendUser) 'ConfigurationManager.AppSettings("printNameCall").ToString()

                            'Dim dataReadRfcard As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "vi_Nr_Rfcard_GetAll", New SqlParameter("@card_id ", CommonUtility.Get_String(dsRequestDetails.Tables(0).Rows(0).Item("card_id"))))

                            'Dim dataRead_Sum As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "sp_getSummaryPrintForm", New SqlParameter("@invh_run_auto ", CommonUtility.Get_String(Cells)))

                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then
                                'If dataRead_Sum.Read Then
                                '    'rpt.txtSum_Gross_Weight.Text = dataRead_Sum.Item("Gross_Weight").ToString()
                                '    'rpt.txtSum_g_Unit_Desc.Text = dataRead_Sum.Item("g_Unit_Desc").ToString()

                                '    dataRead_Sum.Close()
                                'End If
                                rpt.C_TotalRowDe.text = dsRequestDetails.Tables(0).Rows.Count
                                rpt.DataSource = dsRequestDetails.Tables(0) 'ReportPrintClass.GetDataLoadTrd("20090416-000001", "ST-001") 'dsRequestDetail.Tables(0)
                                'rForm1.PageSettings.PaperKind = Printing.PaperKind.Custom
                                'rForm1.PageSettings.PaperHeight = 30.5
                                'rForm1.PageSettings.PaperWidth = 21

                                rpt.Run()
                                rpt.Document.Print(False, False) 'set false ������������˹�����͡����ͧ printer
                                Return True
                            Else
                                Return False
                            End If
                        Case "FORM2_3"
                            'view
                            Dim rpt = New rpt3_ediFORM2_3_pr
                            rpt.Document.Printer.PrinterName = PrintComplete(sendFormType, SiteID, sendUser) 'ConfigurationManager.AppSettings("printNameCall").ToString()

                            'Dim dataReadRfcard As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "vi_Nr_Rfcard_GetAll", New SqlParameter("@card_id ", CommonUtility.Get_String(dsRequestDetails.Tables(0).Rows(0).Item("card_id"))))

                            'Dim dataRead_Sum As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "sp_getSummaryPrintForm", New SqlParameter("@invh_run_auto ", CommonUtility.Get_String(Cells)))

                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then
                                'If dataRead_Sum.Read Then
                                '    'rpt.txtSum_Gross_Weight.Text = dataRead_Sum.Item("Gross_Weight").ToString()
                                '    'rpt.txtSum_g_Unit_Desc.Text = dataRead_Sum.Item("g_Unit_Desc").ToString()

                                '    dataRead_Sum.Close()
                                'End If
                                rpt.C_TotalRowDe.text = dsRequestDetails.Tables(0).Rows.Count
                                rpt.DataSource = dsRequestDetails.Tables(0) 'ReportPrintClass.GetDataLoadTrd("20090416-000001", "ST-001") 'dsRequestDetail.Tables(0)
                                'rForm1.PageSettings.PaperKind = Printing.PaperKind.Custom
                                'rForm1.PageSettings.PaperHeight = 30.5
                                'rForm1.PageSettings.PaperWidth = 21

                                rpt.Run()
                                rpt.Document.Print(False, False) 'set false ������������˹�����͡����ͧ printer
                                Return True
                            Else
                                Return False
                            End If
                        Case "FORM2_4" '���������
                            '' ''view
                            ' ''Dim rpt = New rpt3_ediFORM2_4_pr
                            ' ''rpt.Document.Printer.PrinterName = PrintComplete(sendFormType, SiteID, sendUser) 'ConfigurationManager.AppSettings("printNameCall").ToString()

                            '' ''Dim dataReadRfcard As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "vi_Nr_Rfcard_GetAll", New SqlParameter("@card_id ", CommonUtility.Get_String(dsRequestDetails.Tables(0).Rows(0).Item("card_id"))))

                            '' ''Dim dataRead_Sum As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "sp_getSummaryPrintForm", New SqlParameter("@invh_run_auto ", CommonUtility.Get_String(Cells)))

                            ' ''If dsRequestDetails.Tables(0).Rows.Count > 0 Then
                            ' ''    'If dataRead_Sum.Read Then
                            ' ''    '    'rpt.txtSum_Gross_Weight.Text = dataRead_Sum.Item("Gross_Weight").ToString()
                            ' ''    '    'rpt.txtSum_g_Unit_Desc.Text = dataRead_Sum.Item("g_Unit_Desc").ToString()

                            ' ''    '    dataRead_Sum.Close()
                            ' ''    'End If
                            ' ''    rpt.C_TotalRowDe.text = dsRequestDetails.Tables(0).Rows.Count
                            ' ''    rpt.DataSource = dsRequestDetails.Tables(0) 'ReportPrintClass.GetDataLoadTrd("20090416-000001", "ST-001") 'dsRequestDetail.Tables(0)
                            ' ''    'rForm1.PageSettings.PaperKind = Printing.PaperKind.Custom
                            ' ''    'rForm1.PageSettings.PaperHeight = 30.5
                            ' ''    'rForm1.PageSettings.PaperWidth = 21

                            ' ''    rpt.Run()
                            ' ''    rpt.Document.Print(False, False) 'set false ������������˹�����͡����ͧ printer
                            ' ''    Return True
                            ' ''Else
                            ' ''    Return False
                            ' ''End If
                        Case "FORM3"
                            'view
                            Dim rpt = New rpt3_ediFORM3_pr
                            rpt.Document.Printer.PrinterName = PrintComplete(sendFormType, SiteID, sendUser) 'ConfigurationManager.AppSettings("printNameCall").ToString()

                            'Dim dataReadRfcard As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "vi_Nr_Rfcard_GetAll", New SqlParameter("@card_id ", CommonUtility.Get_String(dsRequestDetails.Tables(0).Rows(0).Item("card_id"))))

                            'Dim dataRead_Sum As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "sp_getSummaryPrintForm", New SqlParameter("@invh_run_auto ", CommonUtility.Get_String(Cells)))

                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then
                                'If dataRead_Sum.Read Then
                                '    'rpt.txtSum_Gross_Weight.Text = dataRead_Sum.Item("Gross_Weight").ToString()
                                '    'rpt.txtSum_g_Unit_Desc.Text = dataRead_Sum.Item("g_Unit_Desc").ToString()

                                '    dataRead_Sum.Close()
                                'End If
                                rpt.C_TotalRowDe.text = dsRequestDetails.Tables(0).Rows.Count
                                rpt.DataSource = dsRequestDetails.Tables(0) 'ReportPrintClass.GetDataLoadTrd("20090416-000001", "ST-001") 'dsRequestDetail.Tables(0)
                                'rForm1.PageSettings.PaperKind = Printing.PaperKind.Custom
                                'rForm1.PageSettings.PaperHeight = 30.5
                                'rForm1.PageSettings.PaperWidth = 21

                                rpt.Run()
                                rpt.Document.Print(False, False) 'set false ������������˹�����͡����ͧ printer
                                Return True
                            Else
                                Return False
                            End If
                        Case "FORM3_1"
                            'view
                            Dim rpt = New rpt3_ediFORM3_1_pr
                            rpt.Document.Printer.PrinterName = PrintComplete(sendFormType, SiteID, sendUser) 'ConfigurationManager.AppSettings("printNameCall").ToString()

                            'Dim dataReadRfcard As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "vi_Nr_Rfcard_GetAll", New SqlParameter("@card_id ", CommonUtility.Get_String(dsRequestDetails.Tables(0).Rows(0).Item("card_id"))))

                            'Dim dataRead_Sum As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "sp_getSummaryPrintForm", New SqlParameter("@invh_run_auto ", CommonUtility.Get_String(Cells)))

                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then
                                'If dataRead_Sum.Read Then
                                '    'rpt.txtSum_Gross_Weight.Text = dataRead_Sum.Item("Gross_Weight").ToString()
                                '    'rpt.txtSum_g_Unit_Desc.Text = dataRead_Sum.Item("g_Unit_Desc").ToString()

                                '    dataRead_Sum.Close()
                                'End If
                                rpt.C_TotalRowDe.text = dsRequestDetails.Tables(0).Rows.Count
                                rpt.DataSource = dsRequestDetails.Tables(0) 'ReportPrintClass.GetDataLoadTrd("20090416-000001", "ST-001") 'dsRequestDetail.Tables(0)
                                'rForm1.PageSettings.PaperKind = Printing.PaperKind.Custom
                                'rForm1.PageSettings.PaperHeight = 30.5
                                'rForm1.PageSettings.PaperWidth = 21

                                rpt.Run()
                                rpt.Document.Print(False, False) 'set false ������������˹�����͡����ͧ printer
                                Return True
                            Else
                                Return False
                            End If
                        Case "FORM4"
                            'view
                            Dim rpt = New rpt3_ediFORM4_pr
                            rpt.Document.Printer.PrinterName = PrintComplete(sendFormType, SiteID, sendUser) 'ConfigurationManager.AppSettings("printNameCall").ToString()

                            'Dim dataReadRfcard As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "vi_Nr_Rfcard_GetAll", New SqlParameter("@card_id ", CommonUtility.Get_String(dsRequestDetails.Tables(0).Rows(0).Item("card_id"))))

                            'Dim dataRead_Sum As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "sp_getSummaryPrintForm", New SqlParameter("@invh_run_auto ", CommonUtility.Get_String(Cells)))

                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then
                                'If dataRead_Sum.Read Then
                                '    'rpt.txtSum_Gross_Weight.Text = dataRead_Sum.Item("Gross_Weight").ToString()
                                '    'rpt.txtSum_g_Unit_Desc.Text = dataRead_Sum.Item("g_Unit_Desc").ToString()

                                '    dataRead_Sum.Close()
                                'End If
                                rpt.C_TotalRowDe.text = dsRequestDetails.Tables(0).Rows.Count
                                rpt.DataSource = dsRequestDetails.Tables(0) 'ReportPrintClass.GetDataLoadTrd("20090416-000001", "ST-001") 'dsRequestDetail.Tables(0)
                                'rForm1.PageSettings.PaperKind = Printing.PaperKind.Custom
                                'rForm1.PageSettings.PaperHeight = 30.5
                                'rForm1.PageSettings.PaperWidth = 21

                                rpt.Run()
                                rpt.Document.Print(False, False) 'set false ������������˹�����͡����ͧ printer
                                Return True
                            Else
                                Return False
                            End If
                        Case "FORM4_1"
                            'view
                            Dim rpt = New rpt3_ediFORM4_1_pr
                            rpt.Document.Printer.PrinterName = PrintComplete(sendFormType, SiteID, sendUser) 'ConfigurationManager.AppSettings("printNameCall").ToString()

                            'Dim dataReadRfcard As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "vi_Nr_Rfcard_GetAll", New SqlParameter("@card_id ", CommonUtility.Get_String(dsRequestDetails.Tables(0).Rows(0).Item("card_id"))))

                            'Dim dataRead_Sum As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "sp_getSummaryPrintForm", New SqlParameter("@invh_run_auto ", CommonUtility.Get_String(Cells)))

                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then
                                'If dataRead_Sum.Read Then
                                '    'rpt.txtSum_Gross_Weight.Text = dataRead_Sum.Item("Gross_Weight").ToString()
                                '    'rpt.txtSum_g_Unit_Desc.Text = dataRead_Sum.Item("g_Unit_Desc").ToString()

                                '    dataRead_Sum.Close()
                                'End If
                                rpt.C_TotalRowDe.text = dsRequestDetails.Tables(0).Rows.Count
                                rpt.DataSource = dsRequestDetails.Tables(0) 'ReportPrintClass.GetDataLoadTrd("20090416-000001", "ST-001") 'dsRequestDetail.Tables(0)
                                'rForm1.PageSettings.PaperKind = Printing.PaperKind.Custom
                                'rForm1.PageSettings.PaperHeight = 30.5
                                'rForm1.PageSettings.PaperWidth = 21

                                rpt.Run()
                                rpt.Document.Print(False, False) 'set false ������������˹�����͡����ͧ printer
                                Return True
                            Else
                                Return False
                            End If
                        Case "FORM4_2"
                            'view
                            Dim rpt = New rpt3_ediFORM4_2_pr
                            rpt.Document.Printer.PrinterName = PrintComplete(sendFormType, SiteID, sendUser) 'ConfigurationManager.AppSettings("printNameCall").ToString()

                            'Dim dataReadRfcard As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "vi_Nr_Rfcard_GetAll", New SqlParameter("@card_id ", CommonUtility.Get_String(dsRequestDetails.Tables(0).Rows(0).Item("card_id"))))

                            'Dim dataRead_Sum As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "sp_getSummaryPrintForm", New SqlParameter("@invh_run_auto ", CommonUtility.Get_String(Cells)))

                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then
                                'If dataRead_Sum.Read Then
                                '    'rpt.txtSum_Gross_Weight.Text = dataRead_Sum.Item("Gross_Weight").ToString()
                                '    'rpt.txtSum_g_Unit_Desc.Text = dataRead_Sum.Item("g_Unit_Desc").ToString()

                                '    dataRead_Sum.Close()
                                'End If
                                rpt.C_TotalRowDe.text = dsRequestDetails.Tables(0).Rows.Count
                                rpt.DataSource = dsRequestDetails.Tables(0) 'ReportPrintClass.GetDataLoadTrd("20090416-000001", "ST-001") 'dsRequestDetail.Tables(0)
                                'rForm1.PageSettings.PaperKind = Printing.PaperKind.Custom
                                'rForm1.PageSettings.PaperHeight = 30.5
                                'rForm1.PageSettings.PaperWidth = 21

                                rpt.Run()
                                rpt.Document.Print(False, False) 'set false ������������˹�����͡����ͧ printer
                                Return True
                            Else
                                Return False
                            End If
                        Case "FORM4_3"
                            'view
                            Dim rpt = New rpt3_ediFORMs4_3_pr
                            rpt.Document.Printer.PrinterName = PrintComplete(sendFormType, SiteID, sendUser) 'ConfigurationManager.AppSettings("printNameCall").ToString()

                            'Dim dataReadRfcard As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "vi_Nr_Rfcard_GetAll", New SqlParameter("@card_id ", CommonUtility.Get_String(dsRequestDetails.Tables(0).Rows(0).Item("card_id"))))

                            'Dim dataRead_Sum As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "sp_getSummaryPrintForm", New SqlParameter("@invh_run_auto ", CommonUtility.Get_String(Cells)))

                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then
                                'If dataRead_Sum.Read Then
                                '    'rpt.txtSum_Gross_Weight.Text = dataRead_Sum.Item("Gross_Weight").ToString()
                                '    'rpt.txtSum_g_Unit_Desc.Text = dataRead_Sum.Item("g_Unit_Desc").ToString()

                                '    dataRead_Sum.Close()
                                'End If
                                rpt.C_TotalRowDe.text = dsRequestDetails.Tables(0).Rows.Count
                                rpt.DataSource = dsRequestDetails.Tables(0) 'ReportPrintClass.GetDataLoadTrd("20090416-000001", "ST-001") 'dsRequestDetail.Tables(0)
                                'rForm1.PageSettings.PaperKind = Printing.PaperKind.Custom
                                'rForm1.PageSettings.PaperHeight = 30.5
                                'rForm1.PageSettings.PaperWidth = 21

                                rpt.Run()
                                rpt.Document.Print(False, False) 'set false ������������˹�����͡����ͧ printer
                                Return True
                            Else
                                Return False
                            End If
                        Case "FORM4_4"
                            'view
                            Dim rpt = New rpt3_ediFORM4_4_pr
                            rpt.Document.Printer.PrinterName = PrintComplete(sendFormType, SiteID, sendUser) 'ConfigurationManager.AppSettings("printNameCall").ToString()

                            ' Dim dataReadRfcard As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "vi_Nr_Rfcard_GetAll", New SqlParameter("@card_id ", CommonUtility.Get_String(dsRequestDetails.Tables(0).Rows(0).Item("card_id"))))

                            'Dim dataRead_Sum As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "sp_getSummaryPrintForm", New SqlParameter("@invh_run_auto ", CommonUtility.Get_String(Cells)))

                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then
                                'If dataRead_Sum.Read Then
                                '    'rpt.txtSum_Gross_Weight.Text = dataRead_Sum.Item("Gross_Weight").ToString()
                                '    'rpt.txtSum_g_Unit_Desc.Text = dataRead_Sum.Item("g_Unit_Desc").ToString()

                                '    dataRead_Sum.Close()
                                'End If
                                rpt.C_TotalRowDe.text = dsRequestDetails.Tables(0).Rows.Count
                                rpt.DataSource = dsRequestDetails.Tables(0) 'ReportPrintClass.GetDataLoadTrd("20090416-000001", "ST-001") 'dsRequestDetail.Tables(0)
                                'rForm1.PageSettings.PaperKind = Printing.PaperKind.Custom
                                'rForm1.PageSettings.PaperHeight = 30.5
                                'rForm1.PageSettings.PaperWidth = 21

                                rpt.Run()
                                rpt.Document.Print(False, False) 'set false ������������˹�����͡����ͧ printer
                                Return True
                            Else
                                Return False
                            End If
                        Case "FORM4_5"
                            'view
                            Dim rpt = New rpt3_ediFORM4_5_pr
                            rpt.Document.Printer.PrinterName = PrintComplete(sendFormType, SiteID, sendUser) 'ConfigurationManager.AppSettings("printNameCall").ToString()

                            'Dim dataReadRfcard As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "vi_Nr_Rfcard_GetAll", New SqlParameter("@card_id ", CommonUtility.Get_String(dsRequestDetails.Tables(0).Rows(0).Item("card_id"))))

                            'Dim dataRead_Sum As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "sp_getSummaryPrintForm", New SqlParameter("@invh_run_auto ", CommonUtility.Get_String(Cells)))

                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then
                                'If dataRead_Sum.Read Then
                                '    'rpt.txtSum_Gross_Weight.Text = dataRead_Sum.Item("Gross_Weight").ToString()
                                '    'rpt.txtSum_g_Unit_Desc.Text = dataRead_Sum.Item("g_Unit_Desc").ToString()

                                '    dataRead_Sum.Close()
                                'End If
                                rpt.C_TotalRowDe.text = dsRequestDetails.Tables(0).Rows.Count
                                rpt.DataSource = dsRequestDetails.Tables(0) 'ReportPrintClass.GetDataLoadTrd("20090416-000001", "ST-001") 'dsRequestDetail.Tables(0)
                                'rForm1.PageSettings.PaperKind = Printing.PaperKind.Custom
                                'rForm1.PageSettings.PaperHeight = 30.5
                                'rForm1.PageSettings.PaperWidth = 21

                                rpt.Run()
                                rpt.Document.Print(False, False) 'set false ������������˹�����͡����ͧ printer
                                Return True
                            Else
                                Return False
                            End If
                        Case "FORM4_6"
                            'view
                            Dim rpt = New rpt3_ediFORM4_6_pr
                            rpt.Document.Printer.PrinterName = PrintComplete(sendFormType, SiteID, sendUser) 'ConfigurationManager.AppSettings("printNameCall").ToString()

                            ' Dim dataReadRfcard As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "vi_Nr_Rfcard_GetAll", New SqlParameter("@card_id ", CommonUtility.Get_String(dsRequestDetails.Tables(0).Rows(0).Item("card_id"))))

                            ' Dim dataRead_Sum As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "sp_getSummaryPrintForm", New SqlParameter("@invh_run_auto ", CommonUtility.Get_String(Cells)))

                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then
                                'If dataRead_Sum.Read Then
                                '    rpt.txtSum_Gross_Weight.Text = dataRead_Sum.Item("Gross_Weight").ToString()
                                '    rpt.txtSum_g_Unit_Desc.Text = dataRead_Sum.Item("g_Unit_Desc").ToString()

                                '    dataRead_Sum.Close()
                                'End If
                                rpt.C_TotalRowDe.text = dsRequestDetails.Tables(0).Rows.Count
                                rpt.DataSource = dsRequestDetails.Tables(0) 'ReportPrintClass.GetDataLoadTrd("20090416-000001", "ST-001") 'dsRequestDetail.Tables(0)
                                rpt.PageSettings.PaperKind = Printing.PaperKind.Custom
                                rpt.PageSettings.PaperHeight = 30.5
                                rpt.PageSettings.PaperWidth = 21

                                rpt.Run()
                                rpt.Document.Print(False, False) 'set false ������������˹�����͡����ͧ printer
                                Return True
                            Else
                                Return False
                            End If
                        Case "FORM4_8"
                            'view
                            Dim rpt = New rpt3_ediFORM4_8_pr
                            rpt.Document.Printer.PrinterName = PrintComplete(sendFormType, SiteID, sendUser) 'ConfigurationManager.AppSettings("printNameCall").ToString()

                            'Dim dataReadRfcard As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "vi_Nr_Rfcard_GetAll", New SqlParameter("@card_id ", CommonUtility.Get_String(dsRequestDetails.Tables(0).Rows(0).Item("card_id"))))

                            'Dim dataRead_Sum As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "sp_getSummaryPrintForm", New SqlParameter("@invh_run_auto ", CommonUtility.Get_String(Cells)))

                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then
                                'If dataRead_Sum.Read Then
                                '    rpt.txtSum_Gross_Weight.Text = dataRead_Sum.Item("Gross_Weight").ToString()
                                '    rpt.txtSum_g_Unit_Desc.Text = dataRead_Sum.Item("g_Unit_Desc").ToString()

                                '    dataRead_Sum.Close()
                                'End If
                                rpt.C_TotalRowDe.text = dsRequestDetails.Tables(0).Rows.Count
                                rpt.DataSource = dsRequestDetails.Tables(0) 'ReportPrintClass.GetDataLoadTrd("20090416-000001", "ST-001") 'dsRequestDetail.Tables(0)
                                'rpt.PageSettings.PaperKind = Printing.PaperKind.Custom
                                'rpt.PageSettings.PaperHeight = 30.5
                                'rpt.PageSettings.PaperWidth = 21

                                rpt.Run()
                                rpt.Document.Print(False, False) 'set false ������������˹�����͡����ͧ printer
                                Return True
                            Else
                                Return False
                            End If
                        Case "FORM4_9"
                            'view
                            Dim rpt = New rpt3_ediFORM4_9_pr
                            rpt.Document.Printer.PrinterName = PrintComplete(sendFormType, SiteID, sendUser) 'ConfigurationManager.AppSettings("printNameCall").ToString()

                            'Dim dataReadRfcard As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "vi_Nr_Rfcard_GetAll", New SqlParameter("@card_id ", CommonUtility.Get_String(dsRequestDetails.Tables(0).Rows(0).Item("card_id"))))

                            'Dim dataRead_Sum As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "sp_getSummaryPrintForm", New SqlParameter("@invh_run_auto ", CommonUtility.Get_String(Cells)))

                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then
                                'If dataRead_Sum.Read Then
                                '    rpt.txtSum_Gross_Weight.Text = dataRead_Sum.Item("Gross_Weight").ToString()
                                '    rpt.txtSum_g_Unit_Desc.Text = dataRead_Sum.Item("g_Unit_Desc").ToString()

                                '    dataRead_Sum.Close()
                                'End If
                                rpt.C_TotalRowDe.text = dsRequestDetails.Tables(0).Rows.Count
                                rpt.DataSource = dsRequestDetails.Tables(0) 'ReportPrintClass.GetDataLoadTrd("20090416-000001", "ST-001") 'dsRequestDetail.Tables(0)
                                'rpt.PageSettings.PaperKind = Printing.PaperKind.Custom
                                'rpt.PageSettings.PaperHeight = 30.5
                                'rpt.PageSettings.PaperWidth = 21

                                rpt.Run()
                                rpt.Document.Print(False, False) 'set false ������������˹�����͡����ͧ printer
                                Return True
                            Else
                                Return False
                            End If
                        Case "FORM4_91"
                            'view
                            Dim rpt = New rpt3_ediFORM4_91_pr
                            rpt.Document.Printer.PrinterName = PrintComplete(sendFormType, SiteID, sendUser) 'ConfigurationManager.AppSettings("printNameCall").ToString()

                            'Dim dataReadRfcard As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "vi_Nr_Rfcard_GetAll", New SqlParameter("@card_id ", CommonUtility.Get_String(dsRequestDetails.Tables(0).Rows(0).Item("card_id"))))

                            'Dim dataRead_Sum As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "sp_getSummaryPrintForm", New SqlParameter("@invh_run_auto ", CommonUtility.Get_String(Cells)))

                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then
                                'If dataRead_Sum.Read Then
                                '    rpt.txtSum_Gross_Weight.Text = dataRead_Sum.Item("Gross_Weight").ToString()
                                '    rpt.txtSum_g_Unit_Desc.Text = dataRead_Sum.Item("g_Unit_Desc").ToString()

                                '    dataRead_Sum.Close()
                                'End If
                                rpt.C_TotalRowDe.text = dsRequestDetails.Tables(0).Rows.Count
                                rpt.DataSource = dsRequestDetails.Tables(0) 'ReportPrintClass.GetDataLoadTrd("20090416-000001", "ST-001") 'dsRequestDetail.Tables(0)
                                'rpt.PageSettings.PaperKind = Printing.PaperKind.Custom
                                'rpt.PageSettings.PaperHeight = 30.5
                                'rpt.PageSettings.PaperWidth = 21

                                rpt.Run()
                                rpt.Document.Print(False, False) 'set false ������������˹�����͡����ͧ printer
                                Return True
                            Else
                                Return False
                            End If
                        Case "FORM5"
                            'view
                            Dim rpt = New rpt3_ediFORM5_pr
                            rpt.Document.Printer.PrinterName = PrintComplete(sendFormType, SiteID, sendUser) 'ConfigurationManager.AppSettings("printNameCall").ToString()

                            'Dim dataReadRfcard As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "vi_Nr_Rfcard_GetAll", New SqlParameter("@card_id ", CommonUtility.Get_String(dsRequestDetails.Tables(0).Rows(0).Item("card_id"))))

                            'Dim dataRead_Sum As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "sp_getSummaryPrintForm", New SqlParameter("@invh_run_auto ", CommonUtility.Get_String(Cells)))

                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then
                                'If dataRead_Sum.Read Then
                                '    'rpt.txtSum_Gross_Weight.Text = dataRead_Sum.Item("Gross_Weight").ToString()
                                '    'rpt.txtSum_g_Unit_Desc.Text = dataRead_Sum.Item("g_Unit_Desc").ToString()

                                '    dataRead_Sum.Close()
                                'End If
                                rpt.C_TotalRowDe.text = dsRequestDetails.Tables(0).Rows.Count
                                rpt.DataSource = dsRequestDetails.Tables(0) 'ReportPrintClass.GetDataLoadTrd("20090416-000001", "ST-001") 'dsRequestDetail.Tables(0)
                                'rForm1.PageSettings.PaperKind = Printing.PaperKind.Custom
                                'rForm1.PageSettings.PaperHeight = 30.5
                                'rForm1.PageSettings.PaperWidth = 21

                                rpt.Run()
                                rpt.Document.Print(False, False) 'set false ������������˹�����͡����ͧ printer
                                Return True
                            Else
                                Return False
                            End If
                        Case "FORM6"
                            'view
                            Dim rpt = New rpt3_ediFORM6_pr
                            rpt.Document.Printer.PrinterName = PrintComplete(sendFormType, SiteID, sendUser) 'ConfigurationManager.AppSettings("printNameCall").ToString()

                            'Dim dataReadRfcard As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "vi_Nr_Rfcard_GetAll", New SqlParameter("@card_id ", CommonUtility.Get_String(dsRequestDetails.Tables(0).Rows(0).Item("card_id"))))

                            'Dim dataRead_Sum As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "sp_getSummaryPrintForm", New SqlParameter("@invh_run_auto ", CommonUtility.Get_String(Cells)))

                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then
                                'If dataRead_Sum.Read Then
                                '    'rpt.txtSum_Gross_Weight.Text = dataRead_Sum.Item("Gross_Weight").ToString()
                                '    'rpt.txtSum_g_Unit_Desc.Text = dataRead_Sum.Item("g_Unit_Desc").ToString()

                                '    dataRead_Sum.Close()
                                'End If
                                rpt.C_TotalRowDe.text = dsRequestDetails.Tables(0).Rows.Count
                                rpt.DataSource = dsRequestDetails.Tables(0) 'ReportPrintClass.GetDataLoadTrd("20090416-000001", "ST-001") 'dsRequestDetail.Tables(0)
                                'rForm1.PageSettings.PaperKind = Printing.PaperKind.Custom
                                'rForm1.PageSettings.PaperHeight = 30.5
                                'rForm1.PageSettings.PaperWidth = 21

                                rpt.Run()
                                rpt.Document.Print(False, False) 'set false ������������˹�����͡����ͧ printer
                                Return True
                            Else
                                Return False
                            End If
                        Case "FORM7"
                            'view
                            Dim rpt = New rpt3_ediFORM7_pr
                            rpt.Document.Printer.PrinterName = PrintComplete(sendFormType, SiteID, sendUser) 'ConfigurationManager.AppSettings("printNameCall").ToString()

                            'Dim dataReadRfcard As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "vi_Nr_Rfcard_GetAll", New SqlParameter("@card_id ", CommonUtility.Get_String(dsRequestDetails.Tables(0).Rows(0).Item("card_id"))))

                            'Dim dataRead_Sum As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "sp_getSummaryPrintForm", New SqlParameter("@invh_run_auto ", CommonUtility.Get_String(Cells)))

                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then
                                'If dataRead_Sum.Read Then
                                '    'rpt.txtSum_Gross_Weight.Text = dataRead_Sum.Item("Gross_Weight").ToString()
                                '    'rpt.txtSum_g_Unit_Desc.Text = dataRead_Sum.Item("g_Unit_Desc").ToString()

                                '    dataRead_Sum.Close()
                                'End If
                                rpt.C_TotalRowDe.text = dsRequestDetails.Tables(0).Rows.Count
                                rpt.DataSource = dsRequestDetails.Tables(0) 'ReportPrintClass.GetDataLoadTrd("20090416-000001", "ST-001") 'dsRequestDetail.Tables(0)
                                'rForm1.PageSettings.PaperKind = Printing.PaperKind.Custom
                                'rForm1.PageSettings.PaperHeight = 30.5
                                'rForm1.PageSettings.PaperWidth = 21

                                rpt.Run()
                                rpt.Document.Print(False, False) 'set false ������������˹�����͡����ͧ printer
                                Return True
                            Else
                                Return False
                            End If
                        Case "FORM8"
                            'view
                            Dim rpt = New rpt3_ediFORM8_pr
                            rpt.Document.Printer.PrinterName = PrintComplete(sendFormType, SiteID, sendUser) 'ConfigurationManager.AppSettings("printNameCall").ToString()

                            'Dim dataReadRfcard As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "vi_Nr_Rfcard_GetAll", New SqlParameter("@card_id ", CommonUtility.Get_String(dsRequestDetails.Tables(0).Rows(0).Item("card_id"))))

                            'Dim dataRead_Sum As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "sp_getSummaryPrintForm", New SqlParameter("@invh_run_auto ", CommonUtility.Get_String(Cells)))

                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then
                                'If dataRead_Sum.Read Then
                                '    'rpt.txtSum_Gross_Weight.Text = dataRead_Sum.Item("Gross_Weight").ToString()
                                '    'rpt.txtSum_g_Unit_Desc.Text = dataRead_Sum.Item("g_Unit_Desc").ToString()

                                '    dataRead_Sum.Close()
                                'End If
                                rpt.C_TotalRowDe.text = dsRequestDetails.Tables(0).Rows.Count
                                rpt.DataSource = dsRequestDetails.Tables(0) 'ReportPrintClass.GetDataLoadTrd("20090416-000001", "ST-001") 'dsRequestDetail.Tables(0)
                                'rForm1.PageSettings.PaperKind = Printing.PaperKind.Custom
                                'rForm1.PageSettings.PaperHeight = 30.5
                                'rForm1.PageSettings.PaperWidth = 21

                                rpt.Run()
                                rpt.Document.Print(False, False) 'set false ������������˹�����͡����ͧ printer
                                Return True
                            Else
                                Return False
                            End If
                        Case "FORM9"
                            'view
                            Dim rpt = New rpt3_ediFORM9_pr
                            rpt.Document.Printer.PrinterName = PrintComplete(sendFormType, SiteID, sendUser) 'ConfigurationManager.AppSettings("printNameCall").ToString()

                            'Dim dataReadRfcard As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "vi_Nr_Rfcard_GetAll", New SqlParameter("@card_id ", CommonUtility.Get_String(dsRequestDetails.Tables(0).Rows(0).Item("card_id"))))

                            'Dim dataRead_Sum As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "sp_getSummaryPrintForm", New SqlParameter("@invh_run_auto ", CommonUtility.Get_String(Cells)))

                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then
                                'If dataRead_Sum.Read Then
                                '    'rpt.txtSum_Gross_Weight.Text = dataRead_Sum.Item("Gross_Weight").ToString()
                                '    'rpt.txtSum_g_Unit_Desc.Text = dataRead_Sum.Item("g_Unit_Desc").ToString()

                                '    dataRead_Sum.Close()
                                'End If
                                rpt.C_TotalRowDe.text = dsRequestDetails.Tables(0).Rows.Count
                                rpt.DataSource = dsRequestDetails.Tables(0) 'ReportPrintClass.GetDataLoadTrd("20090416-000001", "ST-001") 'dsRequestDetail.Tables(0)
                                'rForm1.PageSettings.PaperKind = Printing.PaperKind.Custom
                                'rForm1.PageSettings.PaperHeight = 30.5
                                'rForm1.PageSettings.PaperWidth = 21

                                rpt.Run()
                                rpt.Document.Print(False, False) 'set false ������������˹�����͡����ͧ printer
                                Return True
                            Else
                                Return False
                            End If

                        Case Else
                            Return False
                    End Select
                End If
            Catch ex As Exception
                Return False
            End Try
        Else
            Return False
        End If
    End Function
    '������¡��������ͧ printer ˹ѧ����Ѻ�ͧ���Ϳ����
    Public Shared Function PrintComplete(ByVal sendFormTypePrinter As String, ByVal sendSiteId As String, ByVal sendUser_ As String) As String
        Dim Print_name As String
        Select Case sendSiteId
            Case "ST-001"
                Select Case sendUser_
                    Case "TIPPARPORN", "SURANG"
                        Print_name = Call_SitePrints_ST_001_01(sendFormTypePrinter)
                        Return Print_name
                    Case Else
                        Print_name = Call_SitePrints_ST_001(sendFormTypePrinter)
                        Return Print_name
                End Select
                
            Case "ST-001T" '��� �� form2_1
                Print_name = Call_SitePrints_ST_001(sendFormTypePrinter)
                Return Print_name
            Case "ST-002" '��ͧ��
                Print_name = Call_SitePrints_ST_002(sendFormTypePrinter)
                Return Print_name
            Case "ST-003" '����ó����
                Print_name = Call_SitePrints_ST_003(sendFormTypePrinter)
                Return Print_name
            Case "ST-004" '�����ä�ҵ�ҧ����� (��)
                Print_name = Call_SitePrints_ST_004(sendFormTypePrinter)
                Return Print_name
            Case "ST-005" '�����ä�ҵ�ҧ����� (��Ҵ�)
                Print_name = Call_SitePrints_ST_005(sendFormTypePrinter)
                Return Print_name
            Case "CB-003" 'ʤ�.ࢵ3 (�ź���)
                Print_name = Call_SitePrints_CB_003(sendFormTypePrinter)
                Return Print_name
        End Select
    End Function

    '������¡��������ͧ printer new �ҡ Table (�ѧ����鷴�ͺ���)
    Public Shared Function PrinterNameSite(ByVal sendFormTypePrinter As String, ByVal sendSiteId As String) As String
        Dim Print_name As String
        Dim strEDIConn As String = ConfigurationManager.ConnectionStrings("EDIConnection").ConnectionString
        Dim dr As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "vi_Call_getPrinterSite_NewDS", New SqlParameter("@SITE_ID", sendSiteId), New SqlParameter("@FORM_TYPE", sendFormTypePrinter), New SqlParameter("@Check_Dot_LaserPrinter", 0))

        If dr.Read() Then
            Print_name = CommonUtility.Get_StringValue(dr.Item("IPNameClient"))
        Else
            Print_name = ""
        End If

        Return Print_name
    End Function
    '������¡��������ͧ printer new �ҡ Table (�ѧ����鷴�ͺ���)
    Public Shared Function PrintReceiptNameSite(ByVal sendFormTypePrinter As String, ByVal sendSiteId As String) As String
        Dim Print_name As String
        Dim strEDIConn As String = ConfigurationManager.ConnectionStrings("EDIConnection").ConnectionString
        Dim dr As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "vi_Call_getPrinterSite_NewDS", New SqlParameter("@SITE_ID", sendSiteId), New SqlParameter("@FORM_TYPE", sendFormTypePrinter), New SqlParameter("@Check_Dot_LaserPrinter", 1))

        If dr.Read() Then
            Print_name = CommonUtility.Get_StringValue(dr.Item("IPNameClient"))
        Else
            Print_name = ""
        End If

        Return Print_name
    End Function

    '������¡��������ͧ �Ӣ�-----------------------------------------------
    Public Shared Function PrintReceipt(ByVal sendFormTypePrinter As String, ByVal sendSiteId As String, ByVal sendRequest As String, ByVal sendUser_ As String) As String
        Dim Print_name As String
        Select Case sendSiteId
            Case "ST-001"
                Select Case sendUser_
                    Case "TIPPARPORN", "SURANG" 'set DS 2
                        Select Case sendRequest
                            Case "1"
                                Print_name = Call_SiteReceipt_ST_001_03(sendFormTypePrinter)
                                Return Print_name
                        End Select
                    Case Else
                        Select Case sendRequest
                            Case "1"
                                Print_name = Call_SiteReceipt_ST_001_01(sendFormTypePrinter)
                                Return Print_name
                            Case "2"
                                Print_name = Call_SiteReceipt_ST_001_02(sendFormTypePrinter)
                                Return Print_name
                        End Select
                End Select
            Case "ST-001T" '��Ѻ Site ST-001 ������ ���������ǡѹ �� ST-001 �������ҹ ST-001T
                Print_name = Call_SiteReceipt_ST_001_01(sendFormTypePrinter)
                Return Print_name
            Case "ST-002" '��ͧ��
                Select Case sendRequest
                    Case "1"
                        Print_name = Call_SiteReceipt_ST_002_01(sendFormTypePrinter)
                        Return Print_name
                    Case "2"
                        Print_name = Call_SiteReceipt_ST_002_02(sendFormTypePrinter)
                        Return Print_name
                End Select
            Case "ST-003" '�����ä�ҵ�ҧ����� (����ó����)
                Print_name = Call_SiteReceipt_ST_003_01(sendFormTypePrinter)
                Return Print_name
            Case "ST-004" '�����ä�ҵ�ҧ����� (��)
                Print_name = Call_SiteReceipt_ST_004_01(sendFormTypePrinter)
                Return Print_name
            Case "ST-005" '�����ä�ҵ�ҧ����� (��Ҵ�)
                Print_name = Call_SiteReceipt_ST_005_01(sendFormTypePrinter)
                Return Print_name
            Case "CB-003" 'ʤ�.ࢵ3 (�ź���)
                Print_name = Call_SiteReceipt_CB_003_01(sendFormTypePrinter)
                Return Print_name
        End Select
    End Function
    '============================Form===========================================
    '˹ѧ����Ѻ�ͧ ST-001
    Public Shared Function Call_SitePrints_ST_001(ByVal Form_ID As String) As String
        Dim SendPrinterName As String
        Select Case Form_ID
            Case "FORM1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001Edi_FORM1").ToString()
                Return SendPrinterName
            Case "FORM1_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001Edi_FORM1_1").ToString()
                Return SendPrinterName
            Case "FORM1_2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001Edi_FORM1_2").ToString()
                Return SendPrinterName
            Case "FORM1_3"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001Edi_FORM1_3").ToString()
                Return SendPrinterName
            Case "FORM1_4"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001Edi_FORM1_4").ToString()
                Return SendPrinterName
            Case "FORM2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001Edi_FORM2").ToString()
                Return SendPrinterName
            Case "FORM2_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001Edi_FORM2_1").ToString()
                Return SendPrinterName
            Case "FORM2_2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001Edi_FORM2_2").ToString()
                Return SendPrinterName
            Case "FORM2_3"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001Edi_FORM2_3").ToString()
                Return SendPrinterName
            Case "FORM2_4"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001Edi_FORM2_4").ToString()
                Return SendPrinterName
            Case "FORM3"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001Edi_FORM3").ToString()
                Return SendPrinterName
            Case "FORM3_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001Edi_FORM3_1").ToString()
                Return SendPrinterName
            Case "FORM4"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001Edi_FORM4").ToString()
                Return SendPrinterName
            Case "FORM4_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001Edi_FORM4_1").ToString()
                Return SendPrinterName
            Case "FORM4_2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001Edi_FORM4_2").ToString()
                Return SendPrinterName
            Case "FORM4_3"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001Edi_FORM4_3").ToString()
                Return SendPrinterName
            Case "FORM4_4"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001Edi_FORM4_4").ToString()
                Return SendPrinterName
            Case "FORM4_5"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001Edi_FORM4_5").ToString()
                Return SendPrinterName
            Case "FORM4_6"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001Edi_FORM4_6").ToString()
                Return SendPrinterName
            Case "FORM4_8"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001Edi_FORM4_8").ToString()
                Return SendPrinterName
            Case "FORM4_9"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001Edi_FORM4_9").ToString()
                Return SendPrinterName
            Case "FORM4_91"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001Edi_FORM4_91").ToString()
                Return SendPrinterName
            Case "FORM5"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001Edi_FORM5").ToString()
                Return SendPrinterName
            Case "FORM6"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001Edi_FORM6").ToString()
                Return SendPrinterName
            Case "FORM7"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001Edi_FORM7").ToString()
                Return SendPrinterName
            Case "FORM8"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001Edi_FORM8").ToString()
                Return SendPrinterName
            Case "FORM9"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001Edi_FORM9").ToString()
                Return SendPrinterName
            Case "FORMRussia"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001Edi_FORMRussia").ToString()
                Return SendPrinterName
        End Select
    End Function
    '˹ѧ����Ѻ�ͧ DS 2
    Public Shared Function Call_SitePrints_ST_001_01(ByVal Form_ID As String) As String
        Dim SendPrinterName As String
        Select Case Form_ID
            Case "FORM1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_01Edi_FORM1").ToString()
                Return SendPrinterName
            Case "FORM1_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_01Edi_FORM1_1").ToString()
                Return SendPrinterName
            Case "FORM1_2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_01Edi_FORM1_2").ToString()
                Return SendPrinterName
            Case "FORM1_3"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_01Edi_FORM1_3").ToString()
                Return SendPrinterName
            Case "FORM1_4"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_01Edi_FORM1_4").ToString()
                Return SendPrinterName
            Case "FORM2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_01Edi_FORM2").ToString()
                Return SendPrinterName
            Case "FORM2_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_01Edi_FORM2_1").ToString()
                Return SendPrinterName
            Case "FORM2_2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_01Edi_FORM2_2").ToString()
                Return SendPrinterName
            Case "FORM2_3"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_01Edi_FORM2_3").ToString()
                Return SendPrinterName
            Case "FORM2_4"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_01Edi_FORM2_4").ToString()
                Return SendPrinterName
            Case "FORM3"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_01Edi_FORM3").ToString()
                Return SendPrinterName
            Case "FORM3_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_01Edi_FORM3_1").ToString()
                Return SendPrinterName
            Case "FORM4"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_01Edi_FORM4").ToString()
                Return SendPrinterName
            Case "FORM4_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_01Edi_FORM4_1").ToString()
                Return SendPrinterName
            Case "FORM4_2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_01Edi_FORM4_2").ToString()
                Return SendPrinterName
            Case "FORM4_3"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_01Edi_FORM4_3").ToString()
                Return SendPrinterName
            Case "FORM4_4"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_01Edi_FORM4_4").ToString()
                Return SendPrinterName
            Case "FORM4_5"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_01Edi_FORM4_5").ToString()
                Return SendPrinterName
            Case "FORM4_6"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_01Edi_FORM4_6").ToString()
                Return SendPrinterName
            Case "FORM4_8"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_01Edi_FORM4_8").ToString()
                Return SendPrinterName
            Case "FORM4_9"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_01Edi_FORM4_9").ToString()
                Return SendPrinterName
            Case "FORM4_91"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_01Edi_FORM4_91").ToString()
                Return SendPrinterName
            Case "FORM5"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_01Edi_FORM5").ToString()
                Return SendPrinterName
            Case "FORM6"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_01Edi_FORM6").ToString()
                Return SendPrinterName
            Case "FORM7"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_01Edi_FORM7").ToString()
                Return SendPrinterName
            Case "FORM8"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_01Edi_FORM8").ToString()
                Return SendPrinterName
            Case "FORM9"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_01Edi_FORM9").ToString()
                Return SendPrinterName
            Case "FORMRussia"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_01Edi_FORMRussia").ToString()
                Return SendPrinterName
        End Select
    End Function
    'set print ST-002 �����ä�ҵ�ҧ����� (�������) ��ͧ��
    Public Shared Function Call_SitePrints_ST_002(ByVal Form_ID As String) As String
        Dim SendPrinterName As String
        Select Case Form_ID
            Case "FORM1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002Edi_FORM1").ToString()
                Return SendPrinterName
            Case "FORM1_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002Edi_FORM1_1").ToString()
                Return SendPrinterName
            Case "FORM1_2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002Edi_FORM1_2").ToString()
                Return SendPrinterName
            Case "FORM1_3"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002Edi_FORM1_3").ToString()
                Return SendPrinterName
            Case "FORM1_4"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002Edi_FORM1_4").ToString()
                Return SendPrinterName
            Case "FORM2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002Edi_FORM2").ToString()
                Return SendPrinterName
            Case "FORM2_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002Edi_FORM2_1").ToString()
                Return SendPrinterName
            Case "FORM2_2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002Edi_FORM2_2").ToString()
                Return SendPrinterName
            Case "FORM2_3"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002Edi_FORM2_3").ToString()
                Return SendPrinterName
            Case "FORM2_4"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002Edi_FORM2_4").ToString()
                Return SendPrinterName
            Case "FORM3"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002Edi_FORM3").ToString()
                Return SendPrinterName
            Case "FORM3_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002Edi_FORM3_1").ToString()
                Return SendPrinterName
            Case "FORM4"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002Edi_FORM4").ToString()
                Return SendPrinterName
            Case "FORM4_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002Edi_FORM4_1").ToString()
                Return SendPrinterName
            Case "FORM4_2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002Edi_FORM4_2").ToString()
                Return SendPrinterName
            Case "FORM4_3"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002Edi_FORM4_3").ToString()
                Return SendPrinterName
            Case "FORM4_4"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002Edi_FORM4_4").ToString()
                Return SendPrinterName
            Case "FORM4_5"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002Edi_FORM4_5").ToString()
                Return SendPrinterName
            Case "FORM4_6"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002Edi_FORM4_6").ToString()
                Return SendPrinterName
            Case "FORM4_8"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002Edi_FORM4_8").ToString()
                Return SendPrinterName
            Case "FORM4_9"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002Edi_FORM4_9").ToString()
                Return SendPrinterName
            Case "FORM4_91"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002Edi_FORM4_91").ToString()
                Return SendPrinterName
            Case "FORM5"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002Edi_FORM5").ToString()
                Return SendPrinterName
            Case "FORM6"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002Edi_FORM6").ToString()
                Return SendPrinterName
            Case "FORM7"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002Edi_FORM7").ToString()
                Return SendPrinterName
            Case "FORM8"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002Edi_FORM8").ToString()
                Return SendPrinterName
            Case "FORM9"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002Edi_FORM9").ToString()
                Return SendPrinterName
            Case "FORMRussia"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002Edi_FORMRussia").ToString()
                Return SendPrinterName

        End Select
    End Function

    '˹ѧ����Ѻ�ͧ ST-003 '����ó����
    Public Shared Function Call_SitePrints_ST_003(ByVal Form_ID As String) As String
        Dim SendPrinterName As String
        Select Case Form_ID
            Case "FORM1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003Edi_FORM1").ToString()
                Return SendPrinterName
            Case "FORM1_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003Edi_FORM1_1").ToString()
                Return SendPrinterName
            Case "FORM1_2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003Edi_FORM1_2").ToString()
                Return SendPrinterName
            Case "FORM1_3"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003Edi_FORM1_3").ToString()
                Return SendPrinterName
            Case "FORM1_4"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003Edi_FORM1_4").ToString()
                Return SendPrinterName
            Case "FORM2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003Edi_FORM2").ToString()
                Return SendPrinterName
            Case "FORM2_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003Edi_FORM2_1").ToString()
                Return SendPrinterName
            Case "FORM2_2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003Edi_FORM2_2").ToString()
                Return SendPrinterName
            Case "FORM2_3"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003Edi_FORM2_3").ToString()
                Return SendPrinterName
            Case "FORM2_4"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003Edi_FORM2_4").ToString()
                Return SendPrinterName
            Case "FORM3"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003Edi_FORM3").ToString()
                Return SendPrinterName
            Case "FORM3_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003Edi_FORM3_1").ToString()
                Return SendPrinterName
            Case "FORM4"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003Edi_FORM4").ToString()
                Return SendPrinterName
            Case "FORM4_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003Edi_FORM4_1").ToString()
                Return SendPrinterName
            Case "FORM4_2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003Edi_FORM4_2").ToString()
                Return SendPrinterName
            Case "FORM4_3"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003Edi_FORM4_3").ToString()
                Return SendPrinterName
            Case "FORM4_4"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003Edi_FORM4_4").ToString()
                Return SendPrinterName
            Case "FORM4_5"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003Edi_FORM4_5").ToString()
                Return SendPrinterName
            Case "FORM4_6"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003Edi_FORM4_6").ToString()
                Return SendPrinterName
            Case "FORM4_8"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003Edi_FORM4_8").ToString()
                Return SendPrinterName
            Case "FORM4_9"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003Edi_FORM4_9").ToString()
                Return SendPrinterName
            Case "FORM4_91"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003Edi_FORM4_91").ToString()
                Return SendPrinterName
            Case "FORM5"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003Edi_FORM5").ToString()
                Return SendPrinterName
            Case "FORM6"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003Edi_FORM6").ToString()
                Return SendPrinterName
            Case "FORM7"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003Edi_FORM7").ToString()
                Return SendPrinterName
            Case "FORM8"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003Edi_FORM8").ToString()
                Return SendPrinterName
            Case "FORM9"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003Edi_FORM9").ToString()
                Return SendPrinterName
            Case "FORMRussia"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003Edi_FORMRussia").ToString()
                Return SendPrinterName

        End Select
    End Function

    '˹ѧ����Ѻ�ͧ ST-004 '�����ä�ҵ�ҧ����� (��)
    Public Shared Function Call_SitePrints_ST_004(ByVal Form_ID As String) As String
        Dim SendPrinterName As String
        Select Case Form_ID
            Case "FORM1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004Edi_FORM1").ToString()
                Return SendPrinterName
            Case "FORM1_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004Edi_FORM1_1").ToString()
                Return SendPrinterName
            Case "FORM1_2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004Edi_FORM1_2").ToString()
                Return SendPrinterName
            Case "FORM1_3"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004Edi_FORM1_3").ToString()
                Return SendPrinterName
            Case "FORM1_4"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004Edi_FORM1_4").ToString()
                Return SendPrinterName
            Case "FORM2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004Edi_FORM2").ToString()
                Return SendPrinterName
            Case "FORM2_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004Edi_FORM2_1").ToString()
                Return SendPrinterName
            Case "FORM2_2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004Edi_FORM2_2").ToString()
                Return SendPrinterName
            Case "FORM2_3"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004Edi_FORM2_3").ToString()
                Return SendPrinterName
            Case "FORM2_4"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004Edi_FORM2_4").ToString()
                Return SendPrinterName
            Case "FORM3"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004Edi_FORM3").ToString()
                Return SendPrinterName
            Case "FORM3_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004Edi_FORM3_1").ToString()
                Return SendPrinterName
            Case "FORM4"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004Edi_FORM4").ToString()
                Return SendPrinterName
            Case "FORM4_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004Edi_FORM4_1").ToString()
                Return SendPrinterName
            Case "FORM4_2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004Edi_FORM4_2").ToString()
                Return SendPrinterName
            Case "FORM4_3"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004Edi_FORM4_3").ToString()
                Return SendPrinterName
            Case "FORM4_4"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004Edi_FORM4_4").ToString()
                Return SendPrinterName
            Case "FORM4_5"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004Edi_FORM4_5").ToString()
                Return SendPrinterName
            Case "FORM4_6"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004Edi_FORM4_6").ToString()
                Return SendPrinterName
            Case "FORM4_8"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004Edi_FORM4_8").ToString()
                Return SendPrinterName
            Case "FORM4_9"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004Edi_FORM4_9").ToString()
                Return SendPrinterName
            Case "FORM4_91"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004Edi_FORM4_91").ToString()
                Return SendPrinterName
            Case "FORM5"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004Edi_FORM5").ToString()
                Return SendPrinterName
            Case "FORM6"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004Edi_FORM6").ToString()
                Return SendPrinterName
            Case "FORM7"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004Edi_FORM7").ToString()
                Return SendPrinterName
            Case "FORM8"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004Edi_FORM8").ToString()
                Return SendPrinterName
            Case "FORM9"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004Edi_FORM9").ToString()
                Return SendPrinterName
            Case "FORMRussia"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004Edi_FORMRussia").ToString()
                Return SendPrinterName

        End Select
    End Function

    '˹ѧ����Ѻ�ͧ ST-005 '�����ä�ҵ�ҧ����� (��Ҵ�)
    Public Shared Function Call_SitePrints_ST_005(ByVal Form_ID As String) As String
        Dim SendPrinterName As String
        Select Case Form_ID
            Case "FORM1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_005Edi_FORM1").ToString()
                Return SendPrinterName
            Case "FORM1_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_005Edi_FORM1_1").ToString()
                Return SendPrinterName
            Case "FORM1_2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_005Edi_FORM1_2").ToString()
                Return SendPrinterName
            Case "FORM1_3"
                SendPrinterName = ConfigurationManager.AppSettings("ST_005Edi_FORM1_3").ToString()
                Return SendPrinterName
            Case "FORM1_4"
                SendPrinterName = ConfigurationManager.AppSettings("ST_005Edi_FORM1_4").ToString()
                Return SendPrinterName
            Case "FORM2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_005Edi_FORM2").ToString()
                Return SendPrinterName
            Case "FORM2_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_005Edi_FORM2_1").ToString()
                Return SendPrinterName
            Case "FORM2_2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_005Edi_FORM2_2").ToString()
                Return SendPrinterName
            Case "FORM2_3"
                SendPrinterName = ConfigurationManager.AppSettings("ST_005Edi_FORM2_3").ToString()
                Return SendPrinterName
            Case "FORM2_4"
                SendPrinterName = ConfigurationManager.AppSettings("ST_005Edi_FORM2_4").ToString()
                Return SendPrinterName
            Case "FORM3"
                SendPrinterName = ConfigurationManager.AppSettings("ST_005Edi_FORM3").ToString()
                Return SendPrinterName
            Case "FORM3_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_005Edi_FORM3_1").ToString()
                Return SendPrinterName
            Case "FORM4"
                SendPrinterName = ConfigurationManager.AppSettings("ST_005Edi_FORM4").ToString()
                Return SendPrinterName
            Case "FORM4_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_005Edi_FORM4_1").ToString()
                Return SendPrinterName
            Case "FORM4_2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_005Edi_FORM4_2").ToString()
                Return SendPrinterName
            Case "FORM4_3"
                SendPrinterName = ConfigurationManager.AppSettings("ST_005Edi_FORM4_3").ToString()
                Return SendPrinterName
            Case "FORM4_4"
                SendPrinterName = ConfigurationManager.AppSettings("ST_005Edi_FORM4_4").ToString()
                Return SendPrinterName
            Case "FORM4_5"
                SendPrinterName = ConfigurationManager.AppSettings("ST_005Edi_FORM4_5").ToString()
                Return SendPrinterName
            Case "FORM4_6"
                SendPrinterName = ConfigurationManager.AppSettings("ST_005Edi_FORM4_6").ToString()
                Return SendPrinterName
            Case "FORM4_8"
                SendPrinterName = ConfigurationManager.AppSettings("ST_005Edi_FORM4_8").ToString()
                Return SendPrinterName
            Case "FORM4_9"
                SendPrinterName = ConfigurationManager.AppSettings("ST_005Edi_FORM4_9").ToString()
                Return SendPrinterName
            Case "FORM4_91"
                SendPrinterName = ConfigurationManager.AppSettings("ST_005Edi_FORM4_91").ToString()
                Return SendPrinterName
            Case "FORM5"
                SendPrinterName = ConfigurationManager.AppSettings("ST_005Edi_FORM5").ToString()
                Return SendPrinterName
            Case "FORM6"
                SendPrinterName = ConfigurationManager.AppSettings("ST_005Edi_FORM6").ToString()
                Return SendPrinterName
            Case "FORM7"
                SendPrinterName = ConfigurationManager.AppSettings("ST_005Edi_FORM7").ToString()
                Return SendPrinterName
            Case "FORM8"
                SendPrinterName = ConfigurationManager.AppSettings("ST_005Edi_FORM8").ToString()
                Return SendPrinterName
            Case "FORM9"
                SendPrinterName = ConfigurationManager.AppSettings("ST_005Edi_FORM9").ToString()
                Return SendPrinterName
            Case "FORMRussia"
                SendPrinterName = ConfigurationManager.AppSettings("ST_005Edi_FORMRussia").ToString()
                Return SendPrinterName

        End Select
    End Function

    '˹ѧ����Ѻ�ͧ CB-003 'ʤ�.ࢵ3 (�ź���)
    Public Shared Function Call_SitePrints_CB_003(ByVal Form_ID As String) As String
        Dim SendPrinterName As String
        Select Case Form_ID
            Case "FORM1"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003Edi_FORM1").ToString()
                Return SendPrinterName
            Case "FORM1_1"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003Edi_FORM1_1").ToString()
                Return SendPrinterName
            Case "FORM1_2"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003Edi_FORM1_2").ToString()
                Return SendPrinterName
            Case "FORM1_3"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003Edi_FORM1_3").ToString()
                Return SendPrinterName
            Case "FORM1_4"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003Edi_FORM1_4").ToString()
                Return SendPrinterName
            Case "FORM2"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003Edi_FORM2").ToString()
                Return SendPrinterName
            Case "FORM2_1"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003Edi_FORM2_1").ToString()
                Return SendPrinterName
            Case "FORM2_2"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003Edi_FORM2_2").ToString()
                Return SendPrinterName
            Case "FORM2_3"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003Edi_FORM2_3").ToString()
                Return SendPrinterName
            Case "FORM2_4"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003Edi_FORM2_4").ToString()
                Return SendPrinterName
            Case "FORM3"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003Edi_FORM3").ToString()
                Return SendPrinterName
            Case "FORM3_1"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003Edi_FORM3_1").ToString()
                Return SendPrinterName
            Case "FORM4"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003Edi_FORM4").ToString()
                Return SendPrinterName
            Case "FORM4_1"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003Edi_FORM4_1").ToString()
                Return SendPrinterName
            Case "FORM4_2"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003Edi_FORM4_2").ToString()
                Return SendPrinterName
            Case "FORM4_3"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003Edi_FORM4_3").ToString()
                Return SendPrinterName
            Case "FORM4_4"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003Edi_FORM4_4").ToString()
                Return SendPrinterName
            Case "FORM4_5"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003Edi_FORM4_5").ToString()
                Return SendPrinterName
            Case "FORM4_6"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003Edi_FORM4_6").ToString()
                Return SendPrinterName
            Case "FORM4_8"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003Edi_FORM4_8").ToString()
                Return SendPrinterName
            Case "FORM4_9"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003Edi_FORM4_9").ToString()
                Return SendPrinterName
            Case "FORM4_91"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003Edi_FORM4_91").ToString()
                Return SendPrinterName
            Case "FORM5"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003Edi_FORM5").ToString()
                Return SendPrinterName
            Case "FORM6"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003Edi_FORM6").ToString()
                Return SendPrinterName
            Case "FORM7"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003Edi_FORM7").ToString()
                Return SendPrinterName
            Case "FORM8"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003Edi_FORM8").ToString()
                Return SendPrinterName
            Case "FORM9"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003Edi_FORM9").ToString()
                Return SendPrinterName
            Case "FORMRussia"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003Edi_FORMRussia").ToString()
                Return SendPrinterName

        End Select
    End Function
    '============================�Ӣ�===========================================
    '����ͧ�����Ӣ� ��. ST-001
    Public Shared Function Call_SiteReceipt_ST_001_01(ByVal Form_ID As String) As String
        Dim SendPrinterName As String
        Select Case Form_ID
            Case "FORM1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_01Edi_receiptFORM1").ToString()
                Return SendPrinterName
            Case "FORM1_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_01Edi_receiptFORM1_1").ToString()
                Return SendPrinterName
            Case "FORM1_2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_01Edi_receiptFORM1_2").ToString()
                Return SendPrinterName
            Case "FORM1_3"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_01Edi_receiptFORM1_3").ToString()
                Return SendPrinterName
            Case "FORM1_4"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_01Edi_receiptFORM1_4").ToString()
                Return SendPrinterName
            Case "FORM2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_01Edi_receiptFORM2").ToString()
                Return SendPrinterName
            Case "FORM2_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_01Edi_receiptFORM2_1").ToString()
                Return SendPrinterName
            Case "FORM2_2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_01Edi_receiptFORM2_2").ToString()
                Return SendPrinterName
            Case "FORM2_3"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_01Edi_receiptFORM2_3").ToString()
                Return SendPrinterName
            Case "FORM2_4"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_01Edi_receiptFORM2_4").ToString()
                Return SendPrinterName
            Case "FORM3"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_01Edi_receiptFORM3").ToString()
                Return SendPrinterName
            Case "FORM3_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_01Edi_receiptFORM3_1").ToString()
                Return SendPrinterName
            Case "FORM4"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_01Edi_receiptFORM4").ToString()
                Return SendPrinterName
            Case "FORM4_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_01Edi_receiptFORM4_1").ToString()
                Return SendPrinterName
            Case "FORM4_2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_01Edi_receiptFORM4_2").ToString()
                Return SendPrinterName
            Case "FORM4_3"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_01Edi_receiptFORM4_3").ToString()
                Return SendPrinterName
            Case "FORM4_4"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_01Edi_receiptFORM4_4").ToString()
                Return SendPrinterName
            Case "FORM4_5"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_01Edi_receiptFORM4_5").ToString()
                Return SendPrinterName
            Case "FORM4_6"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_01Edi_receiptFORM4_6").ToString()
                Return SendPrinterName
            Case "FORM4_8"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_01Edi_receiptFORM4_8").ToString()
                Return SendPrinterName
            Case "FORM4_9"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_01Edi_receiptFORM4_9").ToString()
                Return SendPrinterName
            Case "FORM4_91"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_01Edi_receiptFORM4_91").ToString()
                Return SendPrinterName
            Case "FORM5"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_01Edi_receiptFORM5").ToString()
                Return SendPrinterName
            Case "FORM6"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_01Edi_receiptFORM6").ToString()
                Return SendPrinterName
            Case "FORM7"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_01Edi_receiptFORM7").ToString()
                Return SendPrinterName
            Case "FORM8"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_01Edi_receiptFORM8").ToString()
                Return SendPrinterName
            Case "FORM9"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_01Edi_receiptFORM9").ToString()
                Return SendPrinterName
            Case "FORMRussia"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_01Edi_receiptFORMRussia").ToString()
                Return SendPrinterName
        End Select
    End Function
    Public Shared Function Call_SiteReceipt_ST_001_02(ByVal Form_ID As String) As String
        Dim SendPrinterName As String
        Select Case Form_ID
            Case "FORM1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_02Edi_receiptFORM1").ToString()
                Return SendPrinterName
            Case "FORM1_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_02Edi_receiptFORM1_1").ToString()
                Return SendPrinterName
            Case "FORM1_2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_02Edi_receiptFORM1_2").ToString()
                Return SendPrinterName
            Case "FORM1_3"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_02Edi_receiptFORM1_3").ToString()
                Return SendPrinterName
            Case "FORM1_4"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_02Edi_receiptFORM1_4").ToString()
                Return SendPrinterName
            Case "FORM2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_02Edi_receiptFORM2").ToString()
                Return SendPrinterName
            Case "FORM2_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_02Edi_receiptFORM2_1").ToString()
                Return SendPrinterName
            Case "FORM2_2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_02Edi_receiptFORM2_2").ToString()
                Return SendPrinterName
            Case "FORM2_3"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_02Edi_receiptFORM2_3").ToString()
                Return SendPrinterName
            Case "FORM2_4"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_02Edi_receiptFORM2_4").ToString()
                Return SendPrinterName
            Case "FORM3"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_02Edi_receiptFORM3").ToString()
                Return SendPrinterName
            Case "FORM3_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_02Edi_receiptFORM3_1").ToString()
                Return SendPrinterName
            Case "FORM4"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_02Edi_receiptFORM4").ToString()
                Return SendPrinterName
            Case "FORM4_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_02Edi_receiptFORM4_1").ToString()
                Return SendPrinterName
            Case "FORM4_2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_02Edi_receiptFORM4_2").ToString()
                Return SendPrinterName
            Case "FORM4_3"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_02Edi_receiptFORM4_3").ToString()
                Return SendPrinterName
            Case "FORM4_4"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_02Edi_receiptFORM4_4").ToString()
                Return SendPrinterName
            Case "FORM4_5"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_02Edi_receiptFORM4_5").ToString()
                Return SendPrinterName
            Case "FORM4_6"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_02Edi_receiptFORM4_6").ToString()
                Return SendPrinterName
            Case "FORM4_8"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_02Edi_receiptFORM4_8").ToString()
                Return SendPrinterName
            Case "FORM4_9"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_02Edi_receiptFORM4_9").ToString()
                Return SendPrinterName
            Case "FORM4_91"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_02Edi_receiptFORM4_91").ToString()
                Return SendPrinterName
            Case "FORM5"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_02Edi_receiptFORM5").ToString()
                Return SendPrinterName
            Case "FORM6"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_02Edi_receiptFORM6").ToString()
                Return SendPrinterName
            Case "FORM7"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_02Edi_receiptFORM7").ToString()
                Return SendPrinterName
            Case "FORM8"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_02Edi_receiptFORM8").ToString()
                Return SendPrinterName
            Case "FORM9"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_02Edi_receiptFORM9").ToString()
                Return SendPrinterName
            Case "FORMRussia"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_02Edi_receiptFORMRussia").ToString()
                Return SendPrinterName
        End Select
    End Function
    '����ͧ�����Ӣ� DS 2
    Public Shared Function Call_SiteReceipt_ST_001_03(ByVal Form_ID As String) As String
        Dim SendPrinterName As String
        Select Case Form_ID
            Case "FORM1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_03Edi_receiptFORM1").ToString()
                Return SendPrinterName
            Case "FORM1_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_03Edi_receiptFORM1_1").ToString()
                Return SendPrinterName
            Case "FORM1_2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_03Edi_receiptFORM1_2").ToString()
                Return SendPrinterName
            Case "FORM1_3"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_03Edi_receiptFORM1_3").ToString()
                Return SendPrinterName
            Case "FORM1_4"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_03Edi_receiptFORM1_4").ToString()
                Return SendPrinterName
            Case "FORM2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_03Edi_receiptFORM2").ToString()
                Return SendPrinterName
            Case "FORM2_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_03Edi_receiptFORM2_1").ToString()
                Return SendPrinterName
            Case "FORM2_2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_03Edi_receiptFORM2_2").ToString()
                Return SendPrinterName
            Case "FORM2_3"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_03Edi_receiptFORM2_3").ToString()
                Return SendPrinterName
            Case "FORM2_4"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_03Edi_receiptFORM2_4").ToString()
                Return SendPrinterName
            Case "FORM3"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_03Edi_receiptFORM3").ToString()
                Return SendPrinterName
            Case "FORM3_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_03Edi_receiptFORM3_1").ToString()
                Return SendPrinterName
            Case "FORM4"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_03Edi_receiptFORM4").ToString()
                Return SendPrinterName
            Case "FORM4_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_03Edi_receiptFORM4_1").ToString()
                Return SendPrinterName
            Case "FORM4_2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_03Edi_receiptFORM4_2").ToString()
                Return SendPrinterName
            Case "FORM4_3"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_03Edi_receiptFORM4_3").ToString()
                Return SendPrinterName
            Case "FORM4_4"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_03Edi_receiptFORM4_4").ToString()
                Return SendPrinterName
            Case "FORM4_5"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_03Edi_receiptFORM4_5").ToString()
                Return SendPrinterName
            Case "FORM4_6"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_03Edi_receiptFORM4_6").ToString()
                Return SendPrinterName
            Case "FORM4_8"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_03Edi_receiptFORM4_8").ToString()
                Return SendPrinterName
            Case "FORM4_9"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_03Edi_receiptFORM4_9").ToString()
                Return SendPrinterName
            Case "FORM4_91"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_03Edi_receiptFORM4_91").ToString()
                Return SendPrinterName
            Case "FORM5"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_03Edi_receiptFORM5").ToString()
                Return SendPrinterName
            Case "FORM6"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_03Edi_receiptFORM6").ToString()
                Return SendPrinterName
            Case "FORM7"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_03Edi_receiptFORM7").ToString()
                Return SendPrinterName
            Case "FORM8"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_03Edi_receiptFORM8").ToString()
                Return SendPrinterName
            Case "FORM9"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_03Edi_receiptFORM9").ToString()
                Return SendPrinterName
            Case "FORMRussia"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_03Edi_receiptFORMRussia").ToString()
                Return SendPrinterName
        End Select
    End Function

    '����ͧ�����Ӣ� ��ͧ�� ST-002 ����ͧ 1
    Public Shared Function Call_SiteReceipt_ST_002_01(ByVal Form_ID As String) As String
        Dim SendPrinterName As String
        Select Case Form_ID
            Case "FORM1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_01Edi_receiptFORM1").ToString()
                Return SendPrinterName
            Case "FORM1_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_01Edi_receiptFORM1_1").ToString()
                Return SendPrinterName
            Case "FORM1_2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_01Edi_receiptFORM1_2").ToString()
                Return SendPrinterName
            Case "FORM1_3"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_01Edi_receiptFORM1_3").ToString()
                Return SendPrinterName
            Case "FORM1_4"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_01Edi_receiptFORM1_4").ToString()
                Return SendPrinterName
            Case "FORM2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_01Edi_receiptFORM2").ToString()
                Return SendPrinterName
            Case "FORM2_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_01Edi_receiptFORM2_1").ToString()
                Return SendPrinterName
            Case "FORM2_2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_01Edi_receiptFORM2_2").ToString()
                Return SendPrinterName
            Case "FORM2_3"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_01Edi_receiptFORM2_3").ToString()
                Return SendPrinterName
            Case "FORM2_4"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_01Edi_receiptFORM2_4").ToString()
                Return SendPrinterName
            Case "FORM3"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_01Edi_receiptFORM3").ToString()
                Return SendPrinterName
            Case "FORM3_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_01Edi_receiptFORM3_1").ToString()
                Return SendPrinterName
            Case "FORM4"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_01Edi_receiptFORM4").ToString()
                Return SendPrinterName
            Case "FORM4_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_01Edi_receiptFORM4_1").ToString()
                Return SendPrinterName
            Case "FORM4_2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_01Edi_receiptFORM4_2").ToString()
                Return SendPrinterName
            Case "FORM4_3"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_01Edi_receiptFORM4_3").ToString()
                Return SendPrinterName
            Case "FORM4_4"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_01Edi_receiptFORM4_4").ToString()
                Return SendPrinterName
            Case "FORM4_5"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_01Edi_receiptFORM4_5").ToString()
                Return SendPrinterName
            Case "FORM4_6"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_01Edi_receiptFORM4_6").ToString()
                Return SendPrinterName
            Case "FORM4_8"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_01Edi_receiptFORM4_8").ToString()
                Return SendPrinterName
            Case "FORM4_9"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_01Edi_receiptFORM4_9").ToString()
                Return SendPrinterName
            Case "FORM4_91"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_01Edi_receiptFORM4_91").ToString()
                Return SendPrinterName
            Case "FORM5"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_01Edi_receiptFORM5").ToString()
                Return SendPrinterName
            Case "FORM6"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_01Edi_receiptFORM6").ToString()
                Return SendPrinterName
            Case "FORM7"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_01Edi_receiptFORM7").ToString()
                Return SendPrinterName
            Case "FORM8"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_01Edi_receiptFORM8").ToString()
                Return SendPrinterName
            Case "FORM9"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_01Edi_receiptFORM9").ToString()
                Return SendPrinterName
            Case "FORMRussia"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_01Edi_receiptFORMRussia").ToString()
                Return SendPrinterName
        End Select
    End Function
    '����ͧ�����Ӣ� ��ͧ�� ST-002 ����ͧ 2
    Public Shared Function Call_SiteReceipt_ST_002_02(ByVal Form_ID As String) As String
        Dim SendPrinterName As String
        Select Case Form_ID
            Case "FORM1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_02Edi_receiptFORM1").ToString()
                Return SendPrinterName
            Case "FORM1_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_02Edi_receiptFORM1_1").ToString()
                Return SendPrinterName
            Case "FORM1_2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_02Edi_receiptFORM1_2").ToString()
                Return SendPrinterName
            Case "FORM1_3"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_02Edi_receiptFORM1_3").ToString()
                Return SendPrinterName
            Case "FORM1_4"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_02Edi_receiptFORM1_4").ToString()
                Return SendPrinterName
            Case "FORM2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_02Edi_receiptFORM2").ToString()
                Return SendPrinterName
            Case "FORM2_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_02Edi_receiptFORM2_1").ToString()
                Return SendPrinterName
            Case "FORM2_2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_02Edi_receiptFORM2_2").ToString()
                Return SendPrinterName
            Case "FORM2_3"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_02Edi_receiptFORM2_3").ToString()
                Return SendPrinterName
            Case "FORM2_4"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_02Edi_receiptFORM2_4").ToString()
                Return SendPrinterName
            Case "FORM3"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_02Edi_receiptFORM3").ToString()
                Return SendPrinterName
            Case "FORM3_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_02Edi_receiptFORM3_1").ToString()
                Return SendPrinterName
            Case "FORM4"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_02Edi_receiptFORM4").ToString()
                Return SendPrinterName
            Case "FORM4_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_02Edi_receiptFORM4_1").ToString()
                Return SendPrinterName
            Case "FORM4_2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_02Edi_receiptFORM4_2").ToString()
                Return SendPrinterName
            Case "FORM4_3"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_02Edi_receiptFORM4_3").ToString()
                Return SendPrinterName
            Case "FORM4_4"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_02Edi_receiptFORM4_4").ToString()
                Return SendPrinterName
            Case "FORM4_5"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_02Edi_receiptFORM4_5").ToString()
                Return SendPrinterName
            Case "FORM4_6"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_02Edi_receiptFORM4_6").ToString()
                Return SendPrinterName
            Case "FORM4_8"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_02Edi_receiptFORM4_8").ToString()
                Return SendPrinterName
            Case "FORM4_9"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_02Edi_receiptFORM4_9").ToString()
                Return SendPrinterName
            Case "FORM4_91"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_02Edi_receiptFORM4_91").ToString()
                Return SendPrinterName
            Case "FORM5"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_02Edi_receiptFORM5").ToString()
                Return SendPrinterName
            Case "FORM6"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_02Edi_receiptFORM6").ToString()
                Return SendPrinterName
            Case "FORM7"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_02Edi_receiptFORM7").ToString()
                Return SendPrinterName
            Case "FORM8"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_02Edi_receiptFORM8").ToString()
                Return SendPrinterName
            Case "FORM9"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_02Edi_receiptFORM9").ToString()
                Return SendPrinterName
            Case "FORMRussia"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_02Edi_receiptFORMRussia").ToString()
                Return SendPrinterName
        End Select
    End Function

    '����ͧ�����Ӣ� ����ó���� ST-003
    Public Shared Function Call_SiteReceipt_ST_003_01(ByVal Form_ID As String) As String
        Dim SendPrinterName As String
        Select Case Form_ID
            Case "FORM1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003_01Edi_receiptFORM1").ToString()
                Return SendPrinterName
            Case "FORM1_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003_01Edi_receiptFORM1_1").ToString()
                Return SendPrinterName
            Case "FORM1_2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003_01Edi_receiptFORM1_2").ToString()
                Return SendPrinterName
            Case "FORM1_3"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003_01Edi_receiptFORM1_3").ToString()
                Return SendPrinterName
            Case "FORM1_4"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003_01Edi_receiptFORM1_4").ToString()
                Return SendPrinterName
            Case "FORM2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003_01Edi_receiptFORM2").ToString()
                Return SendPrinterName
            Case "FORM2_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003_01Edi_receiptFORM2_1").ToString()
                Return SendPrinterName
            Case "FORM2_2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003_01Edi_receiptFORM2_2").ToString()
                Return SendPrinterName
            Case "FORM2_3"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003_01Edi_receiptFORM2_3").ToString()
                Return SendPrinterName
            Case "FORM2_4"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003_01Edi_receiptFORM2_4").ToString()
                Return SendPrinterName
            Case "FORM3"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003_01Edi_receiptFORM3").ToString()
                Return SendPrinterName
            Case "FORM3_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003_01Edi_receiptFORM3_1").ToString()
                Return SendPrinterName
            Case "FORM4"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003_01Edi_receiptFORM4").ToString()
                Return SendPrinterName
            Case "FORM4_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003_01Edi_receiptFORM4_1").ToString()
                Return SendPrinterName
            Case "FORM4_2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003_01Edi_receiptFORM4_2").ToString()
                Return SendPrinterName
            Case "FORM4_3"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003_01Edi_receiptFORM4_3").ToString()
                Return SendPrinterName
            Case "FORM4_4"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003_01Edi_receiptFORM4_4").ToString()
                Return SendPrinterName
            Case "FORM4_5"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003_01Edi_receiptFORM4_5").ToString()
                Return SendPrinterName
            Case "FORM4_6"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003_01Edi_receiptFORM4_6").ToString()
                Return SendPrinterName
            Case "FORM4_8"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003_01Edi_receiptFORM4_8").ToString()
                Return SendPrinterName
            Case "FORM4_9"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003_01Edi_receiptFORM4_9").ToString()
                Return SendPrinterName
            Case "FORM4_91"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003_01Edi_receiptFORM4_91").ToString()
                Return SendPrinterName
            Case "FORM5"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003_01Edi_receiptFORM5").ToString()
                Return SendPrinterName
            Case "FORM6"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003_01Edi_receiptFORM6").ToString()
                Return SendPrinterName
            Case "FORM7"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003_01Edi_receiptFORM7").ToString()
                Return SendPrinterName
            Case "FORM8"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003_01Edi_receiptFORM8").ToString()
                Return SendPrinterName
            Case "FORM9"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003_01Edi_receiptFORM9").ToString()
                Return SendPrinterName
            Case "FORMRussia"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003_01Edi_receiptFORMRussia").ToString()
                Return SendPrinterName
        End Select
    End Function

    '����ͧ�����Ӣ� �����ä�ҵ�ҧ����� (��) ST-004
    Public Shared Function Call_SiteReceipt_ST_004_01(ByVal Form_ID As String) As String
        Dim SendPrinterName As String
        Select Case Form_ID
            Case "FORM1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004_01Edi_receiptFORM1").ToString()
                Return SendPrinterName
            Case "FORM1_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004_01Edi_receiptFORM1_1").ToString()
                Return SendPrinterName
            Case "FORM1_2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004_01Edi_receiptFORM1_2").ToString()
                Return SendPrinterName
            Case "FORM1_3"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004_01Edi_receiptFORM1_3").ToString()
                Return SendPrinterName
            Case "FORM1_4"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004_01Edi_receiptFORM1_4").ToString()
                Return SendPrinterName
            Case "FORM2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004_01Edi_receiptFORM2").ToString()
                Return SendPrinterName
            Case "FORM2_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004_01Edi_receiptFORM2_1").ToString()
                Return SendPrinterName
            Case "FORM2_2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004_01Edi_receiptFORM2_2").ToString()
                Return SendPrinterName
            Case "FORM2_3"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004_01Edi_receiptFORM2_3").ToString()
                Return SendPrinterName
            Case "FORM2_4"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004_01Edi_receiptFORM2_4").ToString()
                Return SendPrinterName
            Case "FORM3"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004_01Edi_receiptFORM3").ToString()
                Return SendPrinterName
            Case "FORM3_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004_01Edi_receiptFORM3_1").ToString()
                Return SendPrinterName
            Case "FORM4"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004_01Edi_receiptFORM4").ToString()
                Return SendPrinterName
            Case "FORM4_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004_01Edi_receiptFORM4_1").ToString()
                Return SendPrinterName
            Case "FORM4_2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004_01Edi_receiptFORM4_2").ToString()
                Return SendPrinterName
            Case "FORM4_3"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004_01Edi_receiptFORM4_3").ToString()
                Return SendPrinterName
            Case "FORM4_4"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004_01Edi_receiptFORM4_4").ToString()
                Return SendPrinterName
            Case "FORM4_5"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004_01Edi_receiptFORM4_5").ToString()
                Return SendPrinterName
            Case "FORM4_6"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004_01Edi_receiptFORM4_6").ToString()
                Return SendPrinterName
            Case "FORM4_8"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004_01Edi_receiptFORM4_8").ToString()
                Return SendPrinterName
            Case "FORM4_9"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004_01Edi_receiptFORM4_9").ToString()
                Return SendPrinterName
            Case "FORM4_91"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004_01Edi_receiptFORM4_91").ToString()
                Return SendPrinterName
            Case "FORM5"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004_01Edi_receiptFORM5").ToString()
                Return SendPrinterName
            Case "FORM6"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004_01Edi_receiptFORM6").ToString()
                Return SendPrinterName
            Case "FORM7"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004_01Edi_receiptFORM7").ToString()
                Return SendPrinterName
            Case "FORM8"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004_01Edi_receiptFORM8").ToString()
                Return SendPrinterName
            Case "FORM9"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004_01Edi_receiptFORM9").ToString()
                Return SendPrinterName
            Case "FORMRussia"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004_01Edi_receiptFORMRussia").ToString()
                Return SendPrinterName
        End Select
    End Function

    '����ͧ�����Ӣ� �����ä�ҵ�ҧ����� (��Ҵ�) ST-005
    Public Shared Function Call_SiteReceipt_ST_005_01(ByVal Form_ID As String) As String
        Dim SendPrinterName As String
        Select Case Form_ID
            Case "FORM1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_005_01Edi_receiptFORM1").ToString()
                Return SendPrinterName
            Case "FORM1_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_005_01Edi_receiptFORM1_1").ToString()
                Return SendPrinterName
            Case "FORM1_2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_005_01Edi_receiptFORM1_2").ToString()
                Return SendPrinterName
            Case "FORM1_3"
                SendPrinterName = ConfigurationManager.AppSettings("ST_005_01Edi_receiptFORM1_3").ToString()
                Return SendPrinterName
            Case "FORM1_4"
                SendPrinterName = ConfigurationManager.AppSettings("ST_005_01Edi_receiptFORM1_4").ToString()
                Return SendPrinterName
            Case "FORM2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_005_01Edi_receiptFORM2").ToString()
                Return SendPrinterName
            Case "FORM2_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_005_01Edi_receiptFORM2_1").ToString()
                Return SendPrinterName
            Case "FORM2_2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_005_01Edi_receiptFORM2_2").ToString()
                Return SendPrinterName
            Case "FORM2_3"
                SendPrinterName = ConfigurationManager.AppSettings("ST_005_01Edi_receiptFORM2_3").ToString()
                Return SendPrinterName
            Case "FORM2_4"
                SendPrinterName = ConfigurationManager.AppSettings("ST_005_01Edi_receiptFORM2_4").ToString()
                Return SendPrinterName
            Case "FORM3"
                SendPrinterName = ConfigurationManager.AppSettings("ST_005_01Edi_receiptFORM3").ToString()
                Return SendPrinterName
            Case "FORM3_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_005_01Edi_receiptFORM3_1").ToString()
                Return SendPrinterName
            Case "FORM4"
                SendPrinterName = ConfigurationManager.AppSettings("ST_005_01Edi_receiptFORM4").ToString()
                Return SendPrinterName
            Case "FORM4_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_005_01Edi_receiptFORM4_1").ToString()
                Return SendPrinterName
            Case "FORM4_2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_005_01Edi_receiptFORM4_2").ToString()
                Return SendPrinterName
            Case "FORM4_3"
                SendPrinterName = ConfigurationManager.AppSettings("ST_005_01Edi_receiptFORM4_3").ToString()
                Return SendPrinterName
            Case "FORM4_4"
                SendPrinterName = ConfigurationManager.AppSettings("ST_005_01Edi_receiptFORM4_4").ToString()
                Return SendPrinterName
            Case "FORM4_5"
                SendPrinterName = ConfigurationManager.AppSettings("ST_005_01Edi_receiptFORM4_5").ToString()
                Return SendPrinterName
            Case "FORM4_6"
                SendPrinterName = ConfigurationManager.AppSettings("ST_005_01Edi_receiptFORM4_6").ToString()
                Return SendPrinterName
            Case "FORM4_8"
                SendPrinterName = ConfigurationManager.AppSettings("ST_005_01Edi_receiptFORM4_8").ToString()
                Return SendPrinterName
            Case "FORM4_9"
                SendPrinterName = ConfigurationManager.AppSettings("ST_005_01Edi_receiptFORM4_9").ToString()
                Return SendPrinterName
            Case "FORM4_91"
                SendPrinterName = ConfigurationManager.AppSettings("ST_005_01Edi_receiptFORM4_91").ToString()
                Return SendPrinterName
            Case "FORM5"
                SendPrinterName = ConfigurationManager.AppSettings("ST_005_01Edi_receiptFORM5").ToString()
                Return SendPrinterName
            Case "FORM6"
                SendPrinterName = ConfigurationManager.AppSettings("ST_005_01Edi_receiptFORM6").ToString()
                Return SendPrinterName
            Case "FORM7"
                SendPrinterName = ConfigurationManager.AppSettings("ST_005_01Edi_receiptFORM7").ToString()
                Return SendPrinterName
            Case "FORM8"
                SendPrinterName = ConfigurationManager.AppSettings("ST_005_01Edi_receiptFORM8").ToString()
                Return SendPrinterName
            Case "FORM9"
                SendPrinterName = ConfigurationManager.AppSettings("ST_005_01Edi_receiptFORM9").ToString()
                Return SendPrinterName
            Case "FORMRussia"
                SendPrinterName = ConfigurationManager.AppSettings("ST_005_01Edi_receiptFORMRussia").ToString()
                Return SendPrinterName
        End Select
    End Function

    '����ͧ�����Ӣ� ʤ�.ࢵ3 (�ź���) CB-003
    Public Shared Function Call_SiteReceipt_CB_003_01(ByVal Form_ID As String) As String
        Dim SendPrinterName As String
        Select Case Form_ID
            Case "FORM1"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003_01Edi_receiptFORM1").ToString()
                Return SendPrinterName
            Case "FORM1_1"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003_01Edi_receiptFORM1_1").ToString()
                Return SendPrinterName
            Case "FORM1_2"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003_01Edi_receiptFORM1_2").ToString()
                Return SendPrinterName
            Case "FORM1_3"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003_01Edi_receiptFORM1_3").ToString()
                Return SendPrinterName
            Case "FORM1_4"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003_01Edi_receiptFORM1_4").ToString()
                Return SendPrinterName
            Case "FORM2"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003_01Edi_receiptFORM2").ToString()
                Return SendPrinterName
            Case "FORM2_1"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003_01Edi_receiptFORM2_1").ToString()
                Return SendPrinterName
            Case "FORM2_2"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003_01Edi_receiptFORM2_2").ToString()
                Return SendPrinterName
            Case "FORM2_3"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003_01Edi_receiptFORM2_3").ToString()
                Return SendPrinterName
            Case "FORM2_4"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003_01Edi_receiptFORM2_4").ToString()
                Return SendPrinterName
            Case "FORM3"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003_01Edi_receiptFORM3").ToString()
                Return SendPrinterName
            Case "FORM3_1"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003_01Edi_receiptFORM3_1").ToString()
                Return SendPrinterName
            Case "FORM4"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003_01Edi_receiptFORM4").ToString()
                Return SendPrinterName
            Case "FORM4_1"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003_01Edi_receiptFORM4_1").ToString()
                Return SendPrinterName
            Case "FORM4_2"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003_01Edi_receiptFORM4_2").ToString()
                Return SendPrinterName
            Case "FORM4_3"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003_01Edi_receiptFORM4_3").ToString()
                Return SendPrinterName
            Case "FORM4_4"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003_01Edi_receiptFORM4_4").ToString()
                Return SendPrinterName
            Case "FORM4_5"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003_01Edi_receiptFORM4_5").ToString()
                Return SendPrinterName
            Case "FORM4_6"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003_01Edi_receiptFORM4_6").ToString()
                Return SendPrinterName
            Case "FORM4_8"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003_01Edi_receiptFORM4_8").ToString()
                Return SendPrinterName
            Case "FORM4_9"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003_01Edi_receiptFORM4_9").ToString()
                Return SendPrinterName
            Case "FORM4_91"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003_01Edi_receiptFORM4_91").ToString()
                Return SendPrinterName
            Case "FORM5"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003_01Edi_receiptFORM5").ToString()
                Return SendPrinterName
            Case "FORM6"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003_01Edi_receiptFORM6").ToString()
                Return SendPrinterName
            Case "FORM7"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003_01Edi_receiptFORM7").ToString()
                Return SendPrinterName
            Case "FORM8"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003_01Edi_receiptFORM8").ToString()
                Return SendPrinterName
            Case "FORM9"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003_01Edi_receiptFORM9").ToString()
                Return SendPrinterName
            Case "FORMRussia"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003_01Edi_receiptFORMRussia").ToString()
                Return SendPrinterName
        End Select
    End Function
    '=======================================================================
    '������Ѻ ������ѹ��� 04/09/2010
    Public Shared Function format_dateSelect() As String
        Dim _date As Date
        _date = Date.Today

        Return Format(_date, "dd/MM/yyy")
    End Function
    ''������Ѻ���� �ʴ� 08:05:12
    'Function SelectdateTime(ByVal _billdate As Date) As String
    '    Dim _strBilldate As String
    '    _strBilldate = Format(_billdate, "hh:mm:ss")

    '    Return _strBilldate
    'End Function

    'check and return "" �� 0.0
    Public Shared Function Check_Null(ByVal V_txt As Object) As Object
        If String.IsNullOrEmpty(Trim(V_txt.ToString)) = True Then
            Return 0.0
        Else
            Return V_txt
        End If
    End Function

    'set print ST-001 �����ä�ҵ�ҧ����� (��ǹ��ҧ)
    'Public Shared Function Call_SitePrints_ST_001(ByVal Form_ID As String) As String
    '    Dim SendPrinterName As String
    '    Select Case Form_ID
    '        Case "FORM1"
    '            SendPrinterName = ConfigurationManager.AppSettings("ST_001Edi_FORM1").ToString()
    '            Return SendPrinterName
    '        Case "FORM1_1"
    '            SendPrinterName = ConfigurationManager.AppSettings("ST_001Edi_FORM1_1").ToString()
    '            Return SendPrinterName
    '        Case "FORM1_2"
    '            SendPrinterName = ConfigurationManager.AppSettings("ST_001Edi_FORM1_2").ToString()
    '            Return SendPrinterName
    '        Case "FORM1_3"
    '            SendPrinterName = ConfigurationManager.AppSettings("ST_001Edi_FORM1_3").ToString()
    '            Return SendPrinterName
    '        Case "FORM1_4"
    '            SendPrinterName = ConfigurationManager.AppSettings("ST_001Edi_FORM1_4").ToString()
    '            Return SendPrinterName
    '        Case "FORM2"
    '            SendPrinterName = ConfigurationManager.AppSettings("ST_001Edi_FORM2").ToString()
    '            Return SendPrinterName
    '        Case "FORM2_1"
    '            SendPrinterName = ConfigurationManager.AppSettings("ST_001Edi_FORM2_1").ToString()
    '            Return SendPrinterName
    '        Case "FORM2_2"
    '            SendPrinterName = ConfigurationManager.AppSettings("ST_001Edi_FORM2_2").ToString()
    '            Return SendPrinterName
    '        Case "FORM2_3"
    '            SendPrinterName = ConfigurationManager.AppSettings("ST_001Edi_FORM2_3").ToString()
    '            Return SendPrinterName
    '        Case "FORM3"
    '            SendPrinterName = ConfigurationManager.AppSettings("ST_001Edi_FORM3").ToString()
    '            Return SendPrinterName
    '        Case "FORM4"
    '            SendPrinterName = ConfigurationManager.AppSettings("ST_001Edi_FORM4").ToString()
    '            Return SendPrinterName
    '        Case "FORM4_1"
    '            SendPrinterName = ConfigurationManager.AppSettings("ST_001Edi_FORM4_1").ToString()
    '            Return SendPrinterName
    '        Case "FORM4_2"
    '            SendPrinterName = ConfigurationManager.AppSettings("ST_001Edi_FORM4_2").ToString()
    '            Return SendPrinterName
    '        Case "FORM4_3"
    '            SendPrinterName = ConfigurationManager.AppSettings("ST_001Edi_FORM4_3").ToString()
    '            Return SendPrinterName
    '        Case "FORM4_4"
    '            SendPrinterName = ConfigurationManager.AppSettings("ST_001Edi_FORM4_4").ToString()
    '            Return SendPrinterName
    '        Case "FORM4_5"
    '            SendPrinterName = ConfigurationManager.AppSettings("ST_001Edi_FORM4_5").ToString()
    '            Return SendPrinterName
    '        Case "FORM4_6"
    '            SendPrinterName = ConfigurationManager.AppSettings("ST_001Edi_FORM4_6").ToString()
    '            Return SendPrinterName
    '        Case "FORM4_8"
    '            SendPrinterName = ConfigurationManager.AppSettings("ST_001Edi_FORM4_8").ToString()
    '            Return SendPrinterName
    '        Case "FORM5"
    '            SendPrinterName = ConfigurationManager.AppSettings("ST_001Edi_FORM5").ToString()
    '            Return SendPrinterName
    '        Case "FORM6"
    '            SendPrinterName = ConfigurationManager.AppSettings("ST_001Edi_FORM6").ToString()
    '            Return SendPrinterName
    '        Case "FORM7"
    '            SendPrinterName = ConfigurationManager.AppSettings("ST_001Edi_FORM7").ToString()
    '            Return SendPrinterName
    '        Case "FORM8"
    '            SendPrinterName = ConfigurationManager.AppSettings("ST_001Edi_FORM8").ToString()
    '            Return SendPrinterName
    '        Case "FORM9"
    '            SendPrinterName = ConfigurationManager.AppSettings("ST_001Edi_FORM9").ToString()
    '            Return SendPrinterName
    '        Case "FORMRussia"
    '            SendPrinterName = ConfigurationManager.AppSettings("ST_001Edi_FORMRussia").ToString()
    '            Return SendPrinterName

    '    End Select
    'End Function



    'PrintRpt = New rptprFormE_a4_g2
    '                PrintRpt.Document.Printer.PrinterName = ConfigurationManager.AppSettings("printNameCall").ToString()

    '                PrintRpt.DataSource = dsRequestDetail.Tables(0)
    '                If dsRFCard.Tables(0).Rows.Count > 0 Then
    '                    PrintRpt.txtAuthName_Thai.Text = dsRFCard.Tables(0).Rows(0).Item("AuthName_Thai").ToString()
    '                    PrintRpt.txtAuthAddress.Text = dsRFCard.Tables(0).Rows(0).Item("AuthAddress").ToString()
    '                    PrintRpt.txtAuthTel.Text = dsRFCard.Tables(0).Rows(0).Item("AuthTel").ToString()
    '                End If

    '                PrintRpt.Run()
    '                PrintRpt.Document.Print(False, False) 'set false ������������˹�����͡����ͧ printer
    '                Return True
End Class