Imports Microsoft.VisualBasic
Imports Microsoft.ApplicationBlocks.Data
Imports System.Data.SqlClient
Imports System.Data
Imports System.Text

Imports System.Drawing

Imports DFT.Dotnetnuke.ClassLibrary
Public Class ReportPrintClass
    Shared _ISB2B_1 As Boolean
    Shared _CompanyTaxno_1, _FullFormName_1, _B2BRefNo_1, _B2BCountry_1, _InvRunAuto_1 As String

#Region "   Code Main Print   "
    '1=print ได้,2=ไม่มีรายการสินค้า,3=ไม่สามารถพิมพ์ได้เงื่อนไขอื่น
    ''--------------------------------------------------------------------------------------------------
    Public Shared Function printMultireport(ByVal sendFormType As String, ByVal Cells As String, ByVal SiteID As String, ByVal sendRadio_Form As String, ByVal sendUser As String, ByVal sendRequest_print As String, ByVal PrintFlag As String,
        ByVal sendFirstpage As String, ByVal sendLastpage As String, ByVal NamePrint_A As String, ByVal NamePrint_D As String, ByVal By_TempUser As String) As Integer

        Dim strEDIConn As String = ConfigurationManager.ConnectionStrings("EDIConnection").ConnectionString

        Dim dsRequestDetails As New DataSet

        Dim cmd As String = "vi_form4_edi_printFormBar_NewDS"
        Select Case sendFormType
            Case "FORM44_01", "FORM44_02"
                cmd = "vi_form4_edi_printFormBar_NewDS_ASW"
        End Select

        '============== Part Report ==============
        dsRequestDetails = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, cmd,
                        New SqlParameter("@INVH_RUN_AUTO", Cells),
                       New SqlParameter("@SITE_ID", SiteID))

        'ปรับออก
        'checkUpdate_user(CommonUtility.Get_StringValue(Cells), CommonUtility.Get_StringValue(sendUser), sendRadio_Form)
        'sompol
        If PrintFlag = "N" And sendFormType = "FORM2_4" Then
            For i As Integer = 0 To dsRequestDetails.Tables(0).Rows.Count - 1
                With dsRequestDetails.Tables(0).Rows(i)
                    Dim dsStock As New DataSet
                    dsStock = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, "s_Insert_TransStock_NewDS",
                    New SqlParameter("@TSK_YEAR", CommonUtility.Get_StringValue(Now.Year)),
                    New SqlParameter("@invh_run_auto", CommonUtility.Get_StringValue(.Item("invh_run_auto"))),
                    New SqlParameter("@invd_run_auto", CommonUtility.Get_StringValue(.Item("invd_run_auto"))),
                    New SqlParameter("@TSK_Date", CommonUtility.Get_DateTime(Now)),
                    New SqlParameter("@TSK_Type1", CommonUtility.Get_StringValue("C")),
                    New SqlParameter("@TTS_Code", CommonUtility.Get_StringValue("002")),
                    New SqlParameter("@TSK_Desc", CommonUtility.Get_StringValue("ขอส่งออก")),
                    New SqlParameter("@unit_code", CommonUtility.Get_StringValue("KGM")),
                    New SqlParameter("@TSK_Debit", CommonUtility.Get_Decimal(0)),
                    New SqlParameter("@TSK_Credit", CommonUtility.Get_Decimal(.Item("product_descriptionMaxico"))),
                    New SqlParameter("@TSK_Amount", CommonUtility.Get_Decimal(0)),
                    New SqlParameter("@company_taxno", CommonUtility.Get_StringValue(.Item("company_taxno"))),
                    New SqlParameter("@CompanyName_En", CommonUtility.Get_StringValue(.Item("company_name"))),
                    New SqlParameter("@reference_code2", CommonUtility.Get_StringValue(.Item("reference_code2"))),
                    New SqlParameter("@Quantity", CommonUtility.Get_Decimal(0)),
                    New SqlParameter("@ReferenceDesc1", CommonUtility.Get_StringValue("&nbsp;")),
                    New SqlParameter("@ReferenceDesc2", CommonUtility.Get_StringValue("&nbsp;")),
                    New SqlParameter("@user_id", CommonUtility.Get_StringValue(sendUser)))
                End With
            Next
        End If

        If dsRequestDetails.Tables(0).Rows.Count > 0 Then
            Try
                ''=======================================
                ''ByTine 12-09-2559 สำหรับ B2B
                If DBNull.Value.Equals(dsRequestDetails.Tables(0).Rows(0).Item("IsBackToBack")) = True Then
                    _ISB2B_1 = False
                ElseIf dsRequestDetails.Tables(0).Rows(0).Item("IsBackToBack") = False Then
                    _ISB2B_1 = False
                Else
                    _ISB2B_1 = dsRequestDetails.Tables(0).Rows(0).Item("IsBackToBack")
                    _CompanyTaxno_1 = CommonUtility.Get_String(dsRequestDetails.Tables(0).Rows(0).Item("company_taxno"))
                    _B2BCountry_1 = CommonUtility.Get_String(dsRequestDetails.Tables(0).Rows(0).Item("ImportCountry"))
                    _B2BRefNo_1 = CommonUtility.Get_String(dsRequestDetails.Tables(0).Rows(0).Item("B2BReferenceCode"))
                    _FullFormName_1 = CommonUtility.Get_String(dsRequestDetails.Tables(0).Rows(0).Item("b2b_form_name"))
                    _InvRunAuto_1 = Cells
                End If

                ''=======================================
                '//Check Site IsDBPrintSetting = Y

                If sendRadio_Form = "0" Then
                    ''Print คำขอ         
                    Select Case sendFormType
                        Case "FORM1"
                            Dim rpt = New rpt3_ReEdi_A
                            rpt.Document.Printer.PrinterName = PrintReceipt(sendFormType, SiteID, sendRequest_print, sendUser) 'ConfigurationManager.AppSettings("printNameCall").ToString()

                            '========================================
                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then

                                rpt.DataSource = dsRequestDetails.Tables(0)

                                rpt.Run()
                                rpt.Document.Print(False, False) 'set false เพื่อไม่ให้ขึ้นหน้าเลือกเครื่อง printer

                                Return 1
                            Else
                                Return 3
                            End If

                        Case "FORMRussia"

                            Dim rpt = New rpt3_ReEdi_A
                            rpt.Document.Printer.PrinterName = PrintReceipt(sendFormType, SiteID, sendRequest_print, sendUser) 'ConfigurationManager.AppSettings("printNameCall").ToString()

                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then

                                rpt.DataSource = dsRequestDetails.Tables(0)

                                rpt.Run()
                                rpt.Document.Print(False, False) 'set false เพื่อไม่ให้ขึ้นหน้าเลือกเครื่อง printer

                                Return 1
                            Else
                                Return 3
                            End If
                        Case "FORM1_1"

                            Dim rpt = New rpt3_ReEdi_A
                            rpt.Document.Printer.PrinterName = PrintReceipt(sendFormType, SiteID, sendRequest_print, sendUser) 'ConfigurationManager.AppSettings("printNameCall").ToString()

                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then

                                rpt.DataSource = dsRequestDetails.Tables(0)

                                rpt.Run()
                                rpt.Document.Print(False, False) 'set false เพื่อไม่ให้ขึ้นหน้าเลือกเครื่อง printer
                                Return 1
                            Else
                                Return 3
                            End If
                        Case "FORM1_2"

                            Dim rpt = New rpt3_ReEdi_A
                            rpt.Document.Printer.PrinterName = PrintReceipt(sendFormType, SiteID, sendRequest_print, sendUser) 'ConfigurationManager.AppSettings("printNameCall").ToString()

                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then

                                rpt.DataSource = dsRequestDetails.Tables(0)

                                rpt.Run()
                                rpt.Document.Print(False, False) 'set false เพื่อไม่ให้ขึ้นหน้าเลือกเครื่อง printer
                                Return 1
                            Else
                                Return 3
                            End If
                        Case "FORM1_3"

                            Dim rpt = New rpt3_ReEdi_A
                            rpt.Document.Printer.PrinterName = PrintReceipt(sendFormType, SiteID, sendRequest_print, sendUser) 'ConfigurationManager.AppSettings("printNameCall").ToString()

                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then

                                rpt.DataSource = dsRequestDetails.Tables(0)

                                rpt.Run()
                                rpt.Document.Print(False, False) 'set false เพื่อไม่ให้ขึ้นหน้าเลือกเครื่อง printer
                                Return 1
                            Else
                                Return 3
                            End If
                        Case "FORM1_4"

                            Dim rpt = New rpt3_ReEdi_A
                            rpt.Document.Printer.PrinterName = PrintReceipt(sendFormType, SiteID, sendRequest_print, sendUser) 'ConfigurationManager.AppSettings("printNameCall").ToString()

                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then

                                rpt.DataSource = dsRequestDetails.Tables(0)

                                rpt.Run()
                                rpt.Document.Print(False, False) 'set false เพื่อไม่ให้ขึ้นหน้าเลือกเครื่อง printer
                                Return 1
                            Else
                                Return 3
                            End If
                        Case "FORM2"

                            Dim rpt = New rpt3_ReEdi_A
                            rpt.Document.Printer.PrinterName = PrintReceipt(sendFormType, SiteID, sendRequest_print, sendUser) 'ConfigurationManager.AppSettings("printNameCall").ToString()


                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then

                                rpt.DataSource = dsRequestDetails.Tables(0)

                                rpt.Run()
                                rpt.Document.Print(False, False) 'set false เพื่อไม่ให้ขึ้นหน้าเลือกเครื่อง printer
                                Return 1
                            Else
                                Return 3
                            End If
                        Case "FORM2_1"
                            Dim rpt1 As rpt3_ReEdi_FormST6_1 = New rpt3_ReEdi_FormST6_1
                            rpt1.Document.Printer.PrinterName = PrintReceipt(sendFormType, SiteID, sendRequest_print, sendUser) 'ConfigurationManager.AppSettings("printNameCall").ToString()

                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then
                                rpt1.DataSource = GetDataLoad_EDI_ReForm2_1(Cells, SiteID)
                                '====================
                                rpt1.Run(False)
                                '=================
                                rpt1.Document.Print(False, False)
                                Return 1
                            Else
                                Return 3
                            End If

                        Case "FORM2_2"

                            Dim rpt = New rpt3_ReEdi_A
                            rpt.Document.Printer.PrinterName = PrintReceipt(sendFormType, SiteID, sendRequest_print, sendUser) 'ConfigurationManager.AppSettings("printNameCall").ToString()

                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then

                                rpt.DataSource = dsRequestDetails.Tables(0)

                                rpt.Run()
                                rpt.Document.Print(False, False) 'set false เพื่อไม่ให้ขึ้นหน้าเลือกเครื่อง printer
                                Return 1
                            Else
                                Return 3
                            End If
                        Case "FORM2_3"

                            Dim rpt = New rpt3_ReEdi_A
                            rpt.Document.Printer.PrinterName = PrintReceipt(sendFormType, SiteID, sendRequest_print, sendUser) 'ConfigurationManager.AppSettings("printNameCall").ToString()

                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then

                                rpt.DataSource = dsRequestDetails.Tables(0)

                                rpt.Run()
                                rpt.Document.Print(False, False) 'set false เพื่อไม่ให้ขึ้นหน้าเลือกเครื่อง printer
                                Return 1
                            Else
                                Return 3
                            End If
                        Case "FORM2_4" 'ฟอร์มใหม่

                            Dim rpt = New rpt3_ReEdi_A_24
                            rpt.Document.Printer.PrinterName = PrintReceipt(sendFormType, SiteID, sendRequest_print, sendUser) 'ConfigurationManager.AppSettings("printNameCall").ToString()

                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then

                                rpt.DataSource = dsRequestDetails.Tables(0)

                                rpt.Run()
                                rpt.Document.Print(False, False) 'set false เพื่อไม่ให้ขึ้นหน้าเลือกเครื่อง printer
                                Return 1
                            Else
                                Return 3
                            End If

                            ''ByTine 28-11-2559 เพิ่มฟอร์มใหม่ CO (ปลา)
                        Case "FORM2_5"

                            Dim rpt = New rpt3_ReEdi_A
                            rpt.Document.Printer.PrinterName = PrintReceipt(sendFormType, SiteID, sendRequest_print, sendUser) 'ConfigurationManager.AppSettings("printNameCall").ToString()

                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then

                                rpt.DataSource = dsRequestDetails.Tables(0)

                                rpt.Run()
                                rpt.Document.Print(False, False) 'set false เพื่อไม่ให้ขึ้นหน้าเลือกเครื่อง printer
                                Return 1
                            Else
                                Return 3
                            End If
                            ''ByTine 28-11-2559 เพิ่มฟอร์มใหม่ CO (ข้าว)
                        Case "FORM2_6"

                            Dim rpt = New rpt3_ReEdi_A
                            rpt.Document.Printer.PrinterName = PrintReceipt(sendFormType, SiteID, sendRequest_print, sendUser) 'ConfigurationManager.AppSettings("printNameCall").ToString()

                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then

                                rpt.DataSource = dsRequestDetails.Tables(0)

                                rpt.Run()
                                rpt.Document.Print(False, False) 'set false เพื่อไม่ให้ขึ้นหน้าเลือกเครื่อง printer
                                Return 1
                            Else
                                Return 3
                            End If

                        Case "FORM3"

                            Dim rpt = New rpt3_ReEdi_A
                            rpt.Document.Printer.PrinterName = PrintReceipt(sendFormType, SiteID, sendRequest_print, sendUser) 'ConfigurationManager.AppSettings("printNameCall").ToString()

                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then

                                rpt.DataSource = dsRequestDetails.Tables(0)

                                rpt.Run()
                                rpt.Document.Print(False, False) 'set false เพื่อไม่ให้ขึ้นหน้าเลือกเครื่อง printer
                                Return 1
                            Else
                                Return 3
                            End If
                        Case "FORM3_1"

                            Dim rpt = New rpt3_ReEdi_A
                            rpt.Document.Printer.PrinterName = PrintReceipt(sendFormType, SiteID, sendRequest_print, sendUser) 'ConfigurationManager.AppSettings("printNameCall").ToString()

                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then

                                rpt.DataSource = dsRequestDetails.Tables(0)

                                rpt.Run()
                                rpt.Document.Print(False, False) 'set false เพื่อไม่ให้ขึ้นหน้าเลือกเครื่อง printer
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

                            '===================================
                            Dim rpt = New rpt3_ReEdi_A
                            rpt.Document.Printer.PrinterName = PrintReceipt(sendFormType, SiteID, sendRequest_print, sendUser) 'ConfigurationManager.AppSettings("printNameCall").ToString()

                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then

                                rpt.DataSource = dsRequestDetails.Tables(0)

                                rpt.Run()
                                rpt.Document.Print(False, False) 'set false เพื่อไม่ให้ขึ้นหน้าเลือกเครื่อง printer

                                ''Print Back To Back
                                PrintBackToBackForm(PrintReceipt(sendFormType, SiteID, sendRequest_print, sendUser), sendFormType)

                                Return 1
                            Else
                                Return 3
                            End If
                            '===================================

                            'FORM4_1		ฟอร์ม ดี AICO เก่า (2 ประเทศ)
                            'FORM44_41	ฟอร์ม ดี AICO ใหม่ (7 ประเทศ)

                        Case "FORM44_01", "FORM44_02"
                            '===================================
                            Dim rpt = New rpt3_ReEdi_A
                            rpt.Document.Printer.PrinterName = PrintReceipt(sendFormType, SiteID, sendRequest_print, sendUser) 'ConfigurationManager.AppSettings("printNameCall").ToString()

                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then

                                rpt.DataSource = dsRequestDetails.Tables(0)

                                rpt.Run()
                                rpt.Document.Print(False, False) 'set false เพื่อไม่ให้ขึ้นหน้าเลือกเครื่อง printer

                                '//Print Multi Invoice
                                PrintInvoice(PrintReceipt(sendFormType, SiteID, sendRequest_print, sendUser), Cells)

                                ''Print Back To Back
                                PrintBackToBackForm(PrintReceipt(sendFormType, SiteID, sendRequest_print, sendUser), sendFormType)

                                Return 1
                            Else
                                Return 3
                            End If

                        Case "FORM4_1", "FORM44_41"
                            '===================================
                            Dim rpt = New rpt3_ReEdi_A
                            rpt.Document.Printer.PrinterName = PrintReceipt(sendFormType, SiteID, sendRequest_print, sendUser) 'ConfigurationManager.AppSettings("printNameCall").ToString()

                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then

                                rpt.DataSource = dsRequestDetails.Tables(0)

                                rpt.Run()
                                rpt.Document.Print(False, False) 'set false เพื่อไม่ให้ขึ้นหน้าเลือกเครื่อง printer

                                ''Print Back To Back
                                PrintBackToBackForm(PrintReceipt(sendFormType, SiteID, sendRequest_print, sendUser), sendFormType)

                                Return 1
                            Else
                                Return 3
                            End If
                            '===================================
                            'FORM44		ฟอร์ม ดี (ATIGA) Attach Sheet รถยนต์ เก่า (2 ประเทศ)
                        Case "FORM44"

                            Dim rpt = New rpt3_ReEdi_A44
                            rpt.Document.Printer.PrinterName = PrintReceipt(sendFormType, SiteID, sendRequest_print, sendUser) 'ConfigurationManager.AppSettings("printNameCall").ToString()

                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then

                                rpt.DataSource = dsRequestDetails.Tables(0)

                                rpt.Run()
                                rpt.Document.Print(False, False) 'set false เพื่อไม่ให้ขึ้นหน้าเลือกเครื่อง printer
                                Return 1
                            Else
                                Return 3
                            End If
                            'FORM441		ฟอร์ม ดี AICO Attach Sheet รถยนต์ เก่า (2 ประเทศ)
                        Case "FORM441"

                            Dim rpt = New rpt3_ReEdi_A44
                            rpt.Document.Printer.PrinterName = PrintReceipt(sendFormType, SiteID, sendRequest_print, sendUser) 'ConfigurationManager.AppSettings("printNameCall").ToString()

                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then

                                rpt.DataSource = dsRequestDetails.Tables(0)

                                rpt.Run()
                                rpt.Document.Print(False, False) 'set false เพื่อไม่ให้ขึ้นหน้าเลือกเครื่อง printer
                                Return 1
                            Else
                                Return 3
                            End If
                            '==============================
                        Case "FORM4_2"
                            '===================================
                            Dim rpt = New rpt3_ReEdi_A
                            rpt.Document.Printer.PrinterName = PrintReceipt(sendFormType, SiteID, sendRequest_print, sendUser) 'ConfigurationManager.AppSettings("printNameCall").ToString()

                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then

                                rpt.DataSource = dsRequestDetails.Tables(0)

                                rpt.Run()
                                rpt.Document.Print(False, False) 'set false เพื่อไม่ให้ขึ้นหน้าเลือกเครื่อง printer

                                ''Print Back To Back
                                PrintBackToBackForm(PrintReceipt(sendFormType, SiteID, sendRequest_print, sendUser), sendFormType)

                                Return 1
                            Else
                                Return 3
                            End If

                            '===================================
                        Case "FORM4_3"

                            Dim rpt = New rpt3_ReEdi_A
                            rpt.Document.Printer.PrinterName = PrintReceipt(sendFormType, SiteID, sendRequest_print, sendUser) 'ConfigurationManager.AppSettings("printNameCall").ToString()

                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then

                                rpt.DataSource = dsRequestDetails.Tables(0)

                                rpt.Run()
                                rpt.Document.Print(False, False) 'set false เพื่อไม่ให้ขึ้นหน้าเลือกเครื่อง printer
                                Return 1
                            Else
                                Return 3
                            End If
                        Case "FORM4_4"

                            Dim rpt = New rpt3_ReEdi_A
                            rpt.Document.Printer.PrinterName = PrintReceipt(sendFormType, SiteID, sendRequest_print, sendUser) 'ConfigurationManager.AppSettings("printNameCall").ToString()

                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then

                                rpt.DataSource = dsRequestDetails.Tables(0)

                                rpt.Run()
                                rpt.Document.Print(False, False) 'set false เพื่อไม่ให้ขึ้นหน้าเลือกเครื่อง printer
                                Return 1
                            Else
                                Return 3
                            End If
                        Case "FORM4_5"

                            Dim rpt = New rpt3_ReEdi_A
                            rpt.Document.Printer.PrinterName = PrintReceipt(sendFormType, SiteID, sendRequest_print, sendUser) 'ConfigurationManager.AppSettings("printNameCall").ToString()

                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then

                                rpt.DataSource = dsRequestDetails.Tables(0)

                                rpt.Run()
                                rpt.Document.Print(False, False) 'set false เพื่อไม่ให้ขึ้นหน้าเลือกเครื่อง printer
                                Return 1
                            Else
                                Return 3
                            End If
                        Case "FORM4_6", "FORM4_61"
                            '===============================
                            Dim rpt = New rpt3_ReEdi_A
                            rpt.Document.Printer.PrinterName = PrintReceipt(sendFormType, SiteID, sendRequest_print, sendUser) 'ConfigurationManager.AppSettings("printNameCall").ToString()

                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then

                                rpt.DataSource = dsRequestDetails.Tables(0)

                                rpt.Run()
                                rpt.Document.Print(False, False) 'set false เพื่อไม่ให้ขึ้นหน้าเลือกเครื่อง printer

                                ''Print Back To Back
                                PrintBackToBackForm(PrintReceipt(sendFormType, SiteID, sendRequest_print, sendUser), sendFormType)

                                Return 1
                            Else
                                Return 3
                            End If
                            '===================================
                        Case "FORM4_8", "FORM4_81"

                            '===================================
                            Dim rpt = New rpt3_ReEdi_A
                            rpt.Document.Printer.PrinterName = PrintReceipt(sendFormType, SiteID, sendRequest_print, sendUser) 'ConfigurationManager.AppSettings("printNameCall").ToString()

                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then

                                rpt.DataSource = dsRequestDetails.Tables(0)


                                rpt.Run()
                                rpt.Document.Print(False, False) 'set false เพื่อไม่ให้ขึ้นหน้าเลือกเครื่อง printer

                                ''Print Back To Back
                                PrintBackToBackForm(PrintReceipt(sendFormType, SiteID, sendRequest_print, sendUser), sendFormType)

                                Return 1
                            Else
                                Return 3
                            End If
                            '===================================
                        Case "FORM4_9"

                            Dim rpt = New rpt3_ReEdi_A
                            rpt.Document.Printer.PrinterName = PrintReceipt(sendFormType, SiteID, sendRequest_print, sendUser) 'ConfigurationManager.AppSettings("printNameCall").ToString()

                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then

                                rpt.DataSource = dsRequestDetails.Tables(0)

                                rpt.Run()
                                rpt.Document.Print(False, False) 'set false เพื่อไม่ให้ขึ้นหน้าเลือกเครื่อง printer

                                ''Print Back To Back
                                PrintBackToBackForm(PrintReceipt(sendFormType, SiteID, sendRequest_print, sendUser), sendFormType)

                                Return 1
                            Else
                                Return 3
                            End If

                            ''ByTine 23-03-2559 เพิ่มเติม AANZ ใหม่ FORM4_911
                        Case "FORM4_91", "FORM4_911"

                            Dim rpt = New rpt3_ReEdi_A
                            rpt.Document.Printer.PrinterName = PrintReceipt(sendFormType, SiteID, sendRequest_print, sendUser) 'ConfigurationManager.AppSettings("printNameCall").ToString()

                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then

                                rpt.DataSource = dsRequestDetails.Tables(0)

                                rpt.Run()
                                rpt.Document.Print(False, False) 'set false เพื่อไม่ให้ขึ้นหน้าเลือกเครื่อง printer

                                ''Print Back To Back
                                PrintBackToBackForm(PrintReceipt(sendFormType, SiteID, sendRequest_print, sendUser), sendFormType)

                                Return 1
                            Else
                                Return 3
                            End If

                            ''ByTine 8-10-2558 เพิ่มคำขอฟอร์มไทย-ชิลี (FORM5_2)
                        Case "FORM5", "FORM5_1", "FORM5_2"

                            Dim rpt = New rpt3_ReEdi_A
                            rpt.Document.Printer.PrinterName = PrintReceipt(sendFormType, SiteID, sendRequest_print, sendUser) 'ConfigurationManager.AppSettings("printNameCall").ToString()

                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then

                                rpt.DataSource = dsRequestDetails.Tables(0)

                                rpt.Run()
                                rpt.Document.Print(False, False) 'set false เพื่อไม่ให้ขึ้นหน้าเลือกเครื่อง printer
                                Return 1
                            Else
                                Return 3
                            End If
                        Case "FORM6"

                            Dim rpt = New rpt3_ReEdi_A
                            rpt.Document.Printer.PrinterName = PrintReceipt(sendFormType, SiteID, sendRequest_print, sendUser) 'ConfigurationManager.AppSettings("printNameCall").ToString()

                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then

                                rpt.DataSource = dsRequestDetails.Tables(0)

                                rpt.Run()
                                rpt.Document.Print(False, False) 'set false เพื่อไม่ให้ขึ้นหน้าเลือกเครื่อง printer
                                Return 1
                            Else
                                Return 3
                            End If
                        Case "FORM7"

                            Dim rpt = New rpt3_ReEdi_A
                            rpt.Document.Printer.PrinterName = PrintReceipt(sendFormType, SiteID, sendRequest_print, sendUser) 'ConfigurationManager.AppSettings("printNameCall").ToString()

                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then

                                rpt.DataSource = dsRequestDetails.Tables(0)

                                rpt.Run()
                                rpt.Document.Print(False, False) 'set false เพื่อไม่ให้ขึ้นหน้าเลือกเครื่อง printer
                                Return 1
                            Else
                                Return 3
                            End If
                        Case "FORM8"

                            Dim rpt = New rpt3_ReEdi_A
                            rpt.Document.Printer.PrinterName = PrintReceipt(sendFormType, SiteID, sendRequest_print, sendUser) 'ConfigurationManager.AppSettings("printNameCall").ToString()

                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then

                                rpt.DataSource = dsRequestDetails.Tables(0)

                                rpt.Run()
                                rpt.Document.Print(False, False) 'set false เพื่อไม่ให้ขึ้นหน้าเลือกเครื่อง printer
                                Return 1
                            Else
                                Return 3
                            End If
                        Case "FORM9"

                            Dim rpt = New rpt3_ReEdi_A
                            rpt.Document.Printer.PrinterName = PrintReceipt(sendFormType, SiteID, sendRequest_print, sendUser) 'ConfigurationManager.AppSettings("printNameCall").ToString()

                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then

                                rpt.DataSource = dsRequestDetails.Tables(0)

                                rpt.Run()
                                rpt.Document.Print(False, False) 'set false เพื่อไม่ให้ขึ้นหน้าเลือกเครื่อง printer
                                Return 1
                            Else
                                Return 3
                            End If
                        Case Else
                            Return 3
                    End Select

                Else
                    'Print หนังสือรับรอง

                    Select Case sendFormType
                        Case "FORM1"
                            Dim rpt = New rpt3_ediFORM1_pr
                            rpt.Document.Printer.PrinterName = PrintComplete(sendFormType, SiteID, sendUser, NamePrint_A, NamePrint_D, By_TempUser) 'ConfigurationManager.AppSettings("printNameCall").ToString()

                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then

                                rpt.C_TotalRowDe.text = dsRequestDetails.Tables(0).Rows.Count
                                rpt.DataSource = dsRequestDetails.Tables(0) 'ReportPrintClass.GetDataLoadTrd("20090416-000001", "ST-001") 'dsRequestDetail.Tables(0)

                                If sendFirstpage.Trim <> "" And sendLastpage.Trim <> "" Then 'แสดงว่าเป็นแบบเลือกหน้าพิมพ์
                                    selectPrintForm_(rpt, sendFirstpage, sendLastpage)
                                    rpt.Run()
                                    rpt.Document.Print(False, False) 'set false เพื่อไม่ให้ขึ้นหน้าเลือกเครื่อง printer
                                Else 'แสดงว่าเป็นพิมพ์ปกติ
                                    rpt.Run()
                                    rpt.Document.Print(False, False) 'set false เพื่อไม่ให้ขึ้นหน้าเลือกเครื่อง printer
                                End If
                                Return 1
                            Else
                                Return 3
                            End If
                        Case "FORMRussia"
                            'view
                            Dim rpt = New rpt3_ediFORM1RU_pr
                            rpt.Document.Printer.PrinterName = PrintComplete(sendFormType, SiteID, sendUser, NamePrint_A, NamePrint_D, By_TempUser) 'ConfigurationManager.AppSettings("printNameCall").ToString()

                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then

                                rpt.C_TotalRowDe.text = dsRequestDetails.Tables(0).Rows.Count
                                rpt.DataSource = dsRequestDetails.Tables(0) 'ReportPrintClass.GetDataLoadTrd("20090416-000001", "ST-001") 'dsRequestDetail.Tables(0)

                                If sendFirstpage.Trim <> "" And sendLastpage.Trim <> "" Then 'แสดงว่าเป็นแบบเลือกหน้าพิมพ์
                                    selectPrintForm_(rpt, sendFirstpage, sendLastpage)
                                    rpt.Run()
                                    rpt.Document.Print(False, False) 'set false เพื่อไม่ให้ขึ้นหน้าเลือกเครื่อง printer
                                Else 'แสดงว่าเป็นพิมพ์ปกติ
                                    rpt.Run()
                                    rpt.Document.Print(False, False) 'set false เพื่อไม่ให้ขึ้นหน้าเลือกเครื่อง printer
                                End If
                                Return 1
                            Else
                                Return 3
                            End If
                        Case "FORM1_1"
                            'view
                            Dim rpt = New rpt3_ediFORM1_1_pr
                            rpt.Document.Printer.PrinterName = PrintComplete(sendFormType, SiteID, sendUser, NamePrint_A, NamePrint_D, By_TempUser) 'ConfigurationManager.AppSettings("printNameCall").ToString()

                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then

                                rpt.C_TotalRowDe.text = dsRequestDetails.Tables(0).Rows.Count
                                rpt.DataSource = dsRequestDetails.Tables(0) 'ReportPrintClass.GetDataLoadTrd("20090416-000001", "ST-001") 'dsRequestDetail.Tables(0)

                                If sendFirstpage.Trim <> "" And sendLastpage.Trim <> "" Then 'แสดงว่าเป็นแบบเลือกหน้าพิมพ์
                                    selectPrintForm_(rpt, sendFirstpage, sendLastpage)
                                    rpt.Run()
                                    rpt.Document.Print(False, False) 'set false เพื่อไม่ให้ขึ้นหน้าเลือกเครื่อง printer
                                Else 'แสดงว่าเป็นพิมพ์ปกติ
                                    rpt.Run()
                                    rpt.Document.Print(False, False) 'set false เพื่อไม่ให้ขึ้นหน้าเลือกเครื่อง printer
                                End If
                                Return 1
                            Else
                                Return 3
                            End If
                        Case "FORM1_2"
                            'view
                            Dim rpt = New rpt3_ediFORM1_2_pr
                            rpt.Document.Printer.PrinterName = PrintComplete(sendFormType, SiteID, sendUser, NamePrint_A, NamePrint_D, By_TempUser) 'ConfigurationManager.AppSettings("printNameCall").ToString()

                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then

                                rpt.C_TotalRowDe.text = dsRequestDetails.Tables(0).Rows.Count
                                rpt.DataSource = dsRequestDetails.Tables(0) 'ReportPrintClass.GetDataLoadTrd("20090416-000001", "ST-001") 'dsRequestDetail.Tables(0)

                                If sendFirstpage.Trim <> "" And sendLastpage.Trim <> "" Then 'แสดงว่าเป็นแบบเลือกหน้าพิมพ์
                                    selectPrintForm_(rpt, sendFirstpage, sendLastpage)
                                    rpt.Run()
                                    rpt.Document.Print(False, False) 'set false เพื่อไม่ให้ขึ้นหน้าเลือกเครื่อง printer
                                Else 'แสดงว่าเป็นพิมพ์ปกติ
                                    rpt.Run()
                                    rpt.Document.Print(False, False) 'set false เพื่อไม่ให้ขึ้นหน้าเลือกเครื่อง printer
                                End If
                                Return 1
                            Else
                                Return 3
                            End If
                        Case "FORM1_3"
                            'view
                            Dim rpt = New rpt3_ediFORM1_3_pr
                            rpt.Document.Printer.PrinterName = PrintComplete(sendFormType, SiteID, sendUser, NamePrint_A, NamePrint_D, By_TempUser) 'ConfigurationManager.AppSettings("printNameCall").ToString()

                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then

                                rpt.C_TotalRowDe.text = dsRequestDetails.Tables(0).Rows.Count
                                rpt.DataSource = dsRequestDetails.Tables(0) 'ReportPrintClass.GetDataLoadTrd("20090416-000001", "ST-001") 'dsRequestDetail.Tables(0)

                                If sendFirstpage.Trim <> "" And sendLastpage.Trim <> "" Then 'แสดงว่าเป็นแบบเลือกหน้าพิมพ์
                                    selectPrintForm_(rpt, sendFirstpage, sendLastpage)
                                    rpt.Run()
                                    rpt.Document.Print(False, False) 'set false เพื่อไม่ให้ขึ้นหน้าเลือกเครื่อง printer
                                Else 'แสดงว่าเป็นพิมพ์ปกติ
                                    rpt.Run()
                                    rpt.Document.Print(False, False) 'set false เพื่อไม่ให้ขึ้นหน้าเลือกเครื่อง printer
                                End If
                                Return 1
                            Else
                                Return 3
                            End If
                        Case "FORM1_4"
                            'view
                            Dim rpt = New rpt3_ediFORM1_4_pr
                            rpt.Document.Printer.PrinterName = PrintComplete(sendFormType, SiteID, sendUser, NamePrint_A, NamePrint_D, By_TempUser) 'ConfigurationManager.AppSettings("printNameCall").ToString()

                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then

                                rpt.C_TotalRowDe.text = dsRequestDetails.Tables(0).Rows.Count
                                rpt.DataSource = dsRequestDetails.Tables(0) 'ReportPrintClass.GetDataLoadTrd("20090416-000001", "ST-001") 'dsRequestDetail.Tables(0)

                                If sendFirstpage.Trim <> "" And sendLastpage.Trim <> "" Then 'แสดงว่าเป็นแบบเลือกหน้าพิมพ์
                                    selectPrintForm_(rpt, sendFirstpage, sendLastpage)
                                    rpt.Run()
                                    rpt.Document.Print(False, False) 'set false เพื่อไม่ให้ขึ้นหน้าเลือกเครื่อง printer
                                Else 'แสดงว่าเป็นพิมพ์ปกติ
                                    rpt.Run()
                                    rpt.Document.Print(False, False) 'set false เพื่อไม่ให้ขึ้นหน้าเลือกเครื่อง printer
                                End If
                                Return 1
                            Else
                                Return 3
                            End If
                        Case "FORM2"
                            'view
                            Dim rpt = New rpt3_ediFORM2_pr
                            rpt.Document.Printer.PrinterName = PrintComplete(sendFormType, SiteID, sendUser, NamePrint_A, NamePrint_D) 'ConfigurationManager.AppSettings("printNameCall").ToString()

                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then

                                rpt.C_TotalRowDe.text = dsRequestDetails.Tables(0).Rows.Count
                                rpt.DataSource = dsRequestDetails.Tables(0) 'ReportPrintClass.GetDataLoadTrd("20090416-000001", "ST-001") 'dsRequestDetail.Tables(0)

                                If sendFirstpage.Trim <> "" And sendLastpage.Trim <> "" Then 'แสดงว่าเป็นแบบเลือกหน้าพิมพ์
                                    selectPrintForm_(rpt, sendFirstpage, sendLastpage)
                                    rpt.Run()
                                    rpt.Document.Print(False, False) 'set false เพื่อไม่ให้ขึ้นหน้าเลือกเครื่อง printer
                                Else 'แสดงว่าเป็นพิมพ์ปกติ
                                    rpt.Run()
                                    rpt.Document.Print(False, False) 'set false เพื่อไม่ให้ขึ้นหน้าเลือกเครื่อง printer
                                End If
                                Return 1
                            Else
                                Return 3
                            End If
                        Case "FORM2_1"
                            'view
                            Dim rpt As Object
                            Select Case SiteID
                                Case "CM-001", "ST-002", "ST-001", "ST-003", "ST-004N", "ST-004"
                                    rpt = New rpt3_ediFORM2_1CM001_pr
                                Case Else
                                    rpt = New rpt3_ediFORM2_1_pr
                            End Select
                            rpt.Document.Printer.PrinterName = PrintComplete(sendFormType, SiteID, sendUser, NamePrint_A, NamePrint_D) 'ConfigurationManager.AppSettings("printNameCall").ToString()

                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then

                                Select Case SiteID
                                    Case "CM-001"
                                        rpt.txtBANGKOK.text = "CHIANGMAI"
                                    Case Else
                                        rpt.txtBANGKOK.text = "BANGKOK"
                                End Select
                                rpt.C_TotalRowDe.text = dsRequestDetails.Tables(0).Rows.Count
                                rpt.DataSource = dsRequestDetails.Tables(0) 'ReportPrintClass.GetDataLoadTrd("20090416-000001", "ST-001") 'dsRequestDetail.Tables(0)

                                If sendFirstpage.Trim <> "" And sendLastpage.Trim <> "" Then 'แสดงว่าเป็นแบบเลือกหน้าพิมพ์
                                    selectPrintForm_(rpt, sendFirstpage, sendLastpage)
                                    rpt.Run()
                                    rpt.Document.Print(False, False) 'set false เพื่อไม่ให้ขึ้นหน้าเลือกเครื่อง printer
                                Else 'แสดงว่าเป็นพิมพ์ปกติ
                                    rpt.Run()
                                    rpt.Document.Print(False, False) 'set false เพื่อไม่ให้ขึ้นหน้าเลือกเครื่อง printer
                                End If
                                Return 1
                            Else
                                Return 3
                            End If
                        Case "FORM2_2"
                            'view
                            Dim rpt = New rpt3_ediFORM2_2_pr
                            rpt.Document.Printer.PrinterName = PrintComplete(sendFormType, SiteID, sendUser, NamePrint_A, NamePrint_D) 'ConfigurationManager.AppSettings("printNameCall").ToString()

                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then

                                rpt.C_TotalRowDe.text = dsRequestDetails.Tables(0).Rows.Count
                                rpt.DataSource = dsRequestDetails.Tables(0) 'ReportPrintClass.GetDataLoadTrd("20090416-000001", "ST-001") 'dsRequestDetail.Tables(0)

                                If sendFirstpage.Trim <> "" And sendLastpage.Trim <> "" Then 'แสดงว่าเป็นแบบเลือกหน้าพิมพ์
                                    selectPrintForm_(rpt, sendFirstpage, sendLastpage)
                                    rpt.Run()
                                    rpt.Document.Print(False, False) 'set false เพื่อไม่ให้ขึ้นหน้าเลือกเครื่อง printer
                                Else 'แสดงว่าเป็นพิมพ์ปกติ
                                    rpt.Run()
                                    rpt.Document.Print(False, False) 'set false เพื่อไม่ให้ขึ้นหน้าเลือกเครื่อง printer
                                End If
                                Return 1
                            Else
                                Return 3
                            End If
                        Case "FORM2_3"
                            'view
                            Dim rpt = New rpt3_ediFORM2_3_pr
                            rpt.Document.Printer.PrinterName = PrintComplete(sendFormType, SiteID, sendUser, NamePrint_A, NamePrint_D) 'ConfigurationManager.AppSettings("printNameCall").ToString()

                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then

                                rpt.C_TotalRowDe.text = dsRequestDetails.Tables(0).Rows.Count
                                rpt.DataSource = dsRequestDetails.Tables(0) 'ReportPrintClass.GetDataLoadTrd("20090416-000001", "ST-001") 'dsRequestDetail.Tables(0)

                                If sendFirstpage.Trim <> "" And sendLastpage.Trim <> "" Then 'แสดงว่าเป็นแบบเลือกหน้าพิมพ์
                                    selectPrintForm_(rpt, sendFirstpage, sendLastpage)
                                    rpt.Run()
                                    rpt.Document.Print(False, False) 'set false เพื่อไม่ให้ขึ้นหน้าเลือกเครื่อง printer
                                Else 'แสดงว่าเป็นพิมพ์ปกติ
                                    rpt.Run()
                                    rpt.Document.Print(False, False) 'set false เพื่อไม่ให้ขึ้นหน้าเลือกเครื่อง printer
                                End If
                                Return 1
                            Else
                                Return 3
                            End If
                        Case "FORM2_4" 'ฟอร์มใหม่
                            'view
                            Dim rpt = New rpt3_ediFORM2_4_pr
                            rpt.Document.Printer.PrinterName = PrintComplete(sendFormType, SiteID, sendUser, NamePrint_A, NamePrint_D) 'ConfigurationManager.AppSettings("printNameCall").ToString()

                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then

                                rpt.C_TotalRowDe.text = dsRequestDetails.Tables(0).Rows.Count
                                rpt.DataSource = dsRequestDetails.Tables(0) 'ReportPrintClass.GetDataLoadTrd("20090416-000001", "ST-001") 'dsRequestDetail.Tables(0)

                                If sendFirstpage.Trim <> "" And sendLastpage.Trim <> "" Then 'แสดงว่าเป็นแบบเลือกหน้าพิมพ์
                                    selectPrintForm_(rpt, sendFirstpage, sendLastpage)
                                    rpt.Run()
                                    rpt.Document.Print(False, False) 'set false เพื่อไม่ให้ขึ้นหน้าเลือกเครื่อง printer
                                Else 'แสดงว่าเป็นพิมพ์ปกติ
                                    rpt.Run()
                                    rpt.Document.Print(False, False) 'set false เพื่อไม่ให้ขึ้นหน้าเลือกเครื่อง printer
                                End If
                                Return 1
                            Else
                                Return 3
                            End If

                            ''ByTine 28-11-2559 เพิ่มฟอร์มใหม่ CO (ปลา)
                        Case "FORM2_5"
                            'view
                            Dim rpt = New rpt3_ediFORM2_5_pr
                            rpt.Document.Printer.PrinterName = PrintComplete(sendFormType, SiteID, sendUser, NamePrint_A, NamePrint_D) 'ConfigurationManager.AppSettings("printNameCall").ToString()

                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then

                                rpt.C_TotalRowDe.text = dsRequestDetails.Tables(0).Rows.Count
                                rpt.DataSource = dsRequestDetails.Tables(0) 'ReportPrintClass.GetDataLoadTrd("20090416-000001", "ST-001") 'dsRequestDetail.Tables(0)

                                If sendFirstpage.Trim <> "" And sendLastpage.Trim <> "" Then 'แสดงว่าเป็นแบบเลือกหน้าพิมพ์
                                    selectPrintForm_(rpt, sendFirstpage, sendLastpage)
                                    rpt.Run()
                                    rpt.Document.Print(False, False) 'set false เพื่อไม่ให้ขึ้นหน้าเลือกเครื่อง printer
                                Else 'แสดงว่าเป็นพิมพ์ปกติ
                                    rpt.Run()
                                    rpt.Document.Print(False, False) 'set false เพื่อไม่ให้ขึ้นหน้าเลือกเครื่อง printer
                                End If
                                Return 1
                            Else
                                Return 3
                            End If

                            ''ByTine 28-11-2559 เพิ่มฟอร์มใหม่ CO (ข้าว)
                        Case "FORM2_6"
                            'view
                            Dim rpt = New rpt3_ediFORM2_6_pr
                            rpt.Document.Printer.PrinterName = PrintComplete(sendFormType, SiteID, sendUser, NamePrint_A, NamePrint_D) 'ConfigurationManager.AppSettings("printNameCall").ToString()

                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then

                                rpt.C_TotalRowDe.text = dsRequestDetails.Tables(0).Rows.Count
                                rpt.DataSource = dsRequestDetails.Tables(0) 'ReportPrintClass.GetDataLoadTrd("20090416-000001", "ST-001") 'dsRequestDetail.Tables(0)

                                If sendFirstpage.Trim <> "" And sendLastpage.Trim <> "" Then 'แสดงว่าเป็นแบบเลือกหน้าพิมพ์
                                    selectPrintForm_(rpt, sendFirstpage, sendLastpage)
                                    rpt.Run()
                                    rpt.Document.Print(False, False) 'set false เพื่อไม่ให้ขึ้นหน้าเลือกเครื่อง printer
                                Else 'แสดงว่าเป็นพิมพ์ปกติ
                                    rpt.Run()
                                    rpt.Document.Print(False, False) 'set false เพื่อไม่ให้ขึ้นหน้าเลือกเครื่อง printer
                                End If
                                Return 1
                            Else
                                Return 3
                            End If

                        Case "FORM3"
                            'view
                            Dim rpt = New rpt3_ediFORM3_pr
                            rpt.Document.Printer.PrinterName = PrintComplete(sendFormType, SiteID, sendUser, NamePrint_A, NamePrint_D) 'ConfigurationManager.AppSettings("printNameCall").ToString()

                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then

                                rpt.C_TotalRowDe.text = dsRequestDetails.Tables(0).Rows.Count
                                rpt.DataSource = dsRequestDetails.Tables(0) 'ReportPrintClass.GetDataLoadTrd("20090416-000001", "ST-001") 'dsRequestDetail.Tables(0)

                                If sendFirstpage.Trim <> "" And sendLastpage.Trim <> "" Then 'แสดงว่าเป็นแบบเลือกหน้าพิมพ์
                                    selectPrintForm_(rpt, sendFirstpage, sendLastpage)
                                    rpt.Run()
                                    rpt.Document.Print(False, False) 'set false เพื่อไม่ให้ขึ้นหน้าเลือกเครื่อง printer
                                Else 'แสดงว่าเป็นพิมพ์ปกติ
                                    rpt.Run()
                                    rpt.Document.Print(False, False) 'set false เพื่อไม่ให้ขึ้นหน้าเลือกเครื่อง printer
                                End If
                                Return 1
                            Else
                                Return 3
                            End If
                        Case "FORM3_1"
                            'view
                            Dim rpt = New rpt3_ediFORM3_1_pr
                            rpt.Document.Printer.PrinterName = PrintComplete(sendFormType, SiteID, sendUser, NamePrint_A, NamePrint_D) 'ConfigurationManager.AppSettings("printNameCall").ToString()

                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then

                                rpt.C_TotalRowDe.text = dsRequestDetails.Tables(0).Rows.Count
                                rpt.DataSource = dsRequestDetails.Tables(0) 'ReportPrintClass.GetDataLoadTrd("20090416-000001", "ST-001") 'dsRequestDetail.Tables(0)

                                If sendFirstpage.Trim <> "" And sendLastpage.Trim <> "" Then 'แสดงว่าเป็นแบบเลือกหน้าพิมพ์
                                    selectPrintForm_(rpt, sendFirstpage, sendLastpage)
                                    rpt.Run()
                                    rpt.Document.Print(False, False) 'set false เพื่อไม่ให้ขึ้นหน้าเลือกเครื่อง printer
                                Else 'แสดงว่าเป็นพิมพ์ปกติ
                                    rpt.Run()
                                    rpt.Document.Print(False, False) 'set false เพื่อไม่ให้ขึ้นหน้าเลือกเครื่อง printer
                                End If
                                Return 1
                            Else
                                Return 3
                            End If
                        Case "FORM4"
                            'view
                            Dim rpt = New rpt3_ediFORM4_pr
                            rpt.Document.Printer.PrinterName = PrintComplete(sendFormType, SiteID, sendUser, NamePrint_A, NamePrint_D, By_TempUser) 'ConfigurationManager.AppSettings("printNameCall").ToString()

                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then

                                rpt.C_TotalRowDe.text = dsRequestDetails.Tables(0).Rows.Count
                                rpt.DataSource = dsRequestDetails.Tables(0) 'ReportPrintClass.GetDataLoadTrd("20090416-000001", "ST-001") 'dsRequestDetail.Tables(0)

                                If sendFirstpage.Trim <> "" And sendLastpage.Trim <> "" Then 'แสดงว่าเป็นแบบเลือกหน้าพิมพ์
                                    selectPrintForm_(rpt, sendFirstpage, sendLastpage)
                                    rpt.Run()
                                    rpt.Document.Print(False, False) 'set false เพื่อไม่ให้ขึ้นหน้าเลือกเครื่อง printer
                                Else 'แสดงว่าเป็นพิมพ์ปกติ
                                    rpt.Run()
                                    rpt.Document.Print(False, False) 'set false เพื่อไม่ให้ขึ้นหน้าเลือกเครื่อง printer
                                End If
                                Return 1
                            Else
                                Return 3
                            End If
                            'by rut D new
                            'FORM44_4	ฟอร์ม ดี (ATIGA) ใหม่ (7 ประเทศ)
                            'FORM44_44	ฟอร์ม ดี (ATIGA) Attach Sheet รถยนต์ ใหม่ (7 ประเทศ)
                            'FORM44_41	ฟอร์ม ดี AICO ใหม่ (7 ประเทศ)
                            'FORM441_4	ฟอร์ม ดี AICO Attach Sheet รถยนต์ ใหม่ (7 ประเทศ)
                        Case "FORM44_4", "FORM44_44", "FORM44_41", "FORM441_4", "FORM44_01", "FORM44_02"
                            'view
                            Dim rpt As Object
                            Select Case sendFormType
                                Case "FORM44_4"
                                    rpt = New rpt3_ediFORM44_4_pr
                                Case "FORM44_44"
                                    rpt = New rpt3_ediFORM44_44_pr
                                Case "FORM44_41"
                                    rpt = New rpt3_ediFORM44_41_pr
                                Case "FORM441_4"
                                    rpt = New rpt3_ediFORM441_4_pr
                                Case "FORM44_01"
                                    rpt = New rpt3_ediFORM44_01
                                Case "FORM44_02"
                                    rpt = New rpt3_ediFORM44_02
                            End Select
                            rpt.Document.Printer.PrinterName = PrintComplete(sendFormType, SiteID, sendUser, NamePrint_A, NamePrint_D, By_TempUser) 'ConfigurationManager.AppSettings("printNameCall").ToString()

                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then
                                rpt.txtCheck_CaseRVCCount.text = CheckCount_RVC(Cells, sendFormType)
                                rpt.txtDisplayUDS7.text = Check_DisplayUSD7(Cells)
                                rpt.C_TotalRowDe.text = dsRequestDetails.Tables(0).Rows.Count

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

                                rpt.DataSource = dsRequestDetails.Tables(0) 'ReportPrintClass.GetDataLoadTrd("20090416-000001", "ST-001") 'dsRequestDetail.Tables(0)


                                If sendFirstpage.Trim <> "" And sendLastpage.Trim <> "" Then 'แสดงว่าเป็นแบบเลือกหน้าพิมพ์
                                    selectPrintForm_(rpt, sendFirstpage, sendLastpage)
                                    rpt.Run()
                                    rpt.Document.Print(False, False) 'set false เพื่อไม่ให้ขึ้นหน้าเลือกเครื่อง printer
                                Else 'แสดงว่าเป็นพิมพ์ปกติ
                                    rpt.Run()
                                    rpt.Document.Print(False, False) 'set false เพื่อไม่ให้ขึ้นหน้าเลือกเครื่อง printer
                                End If
                                Return 1
                            Else
                                Return 3
                            End If

                        Case "FORM4_1"
                            'view
                            Dim rpt = New rpt3_ediFORM4_1_pr
                            rpt.Document.Printer.PrinterName = PrintComplete(sendFormType, SiteID, sendUser, NamePrint_A, NamePrint_D, By_TempUser) 'ConfigurationManager.AppSettings("printNameCall").ToString()

                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then

                                rpt.C_TotalRowDe.text = dsRequestDetails.Tables(0).Rows.Count
                                rpt.DataSource = dsRequestDetails.Tables(0) 'ReportPrintClass.GetDataLoadTrd("20090416-000001", "ST-001") 'dsRequestDetail.Tables(0)

                                If sendFirstpage.Trim <> "" And sendLastpage.Trim <> "" Then 'แสดงว่าเป็นแบบเลือกหน้าพิมพ์
                                    selectPrintForm_(rpt, sendFirstpage, sendLastpage)
                                    rpt.Run()
                                    rpt.Document.Print(False, False) 'set false เพื่อไม่ให้ขึ้นหน้าเลือกเครื่อง printer
                                Else 'แสดงว่าเป็นพิมพ์ปกติ
                                    rpt.Run()
                                    rpt.Document.Print(False, False) 'set false เพื่อไม่ให้ขึ้นหน้าเลือกเครื่อง printer
                                End If

                                Return 1
                            Else
                                Return 3
                            End If
                        Case "FORM44"
                            'view
                            Dim rpt = New rpt3_ediFORM44_pr
                            rpt.Document.Printer.PrinterName = PrintComplete(sendFormType, SiteID, sendUser, NamePrint_A, NamePrint_D, By_TempUser) 'ConfigurationManager.AppSettings("printNameCall").ToString()

                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then

                                rpt.C_TotalRowDe.text = dsRequestDetails.Tables(0).Rows.Count
                                rpt.DataSource = dsRequestDetails.Tables(0) 'ReportPrintClass.GetDataLoadTrd("20090416-000001", "ST-001") 'dsRequestDetail.Tables(0)

                                If sendFirstpage.Trim <> "" And sendLastpage.Trim <> "" Then 'แสดงว่าเป็นแบบเลือกหน้าพิมพ์
                                    selectPrintForm_(rpt, sendFirstpage, sendLastpage)
                                    rpt.Run()
                                    rpt.Document.Print(False, False) 'set false เพื่อไม่ให้ขึ้นหน้าเลือกเครื่อง printer
                                Else 'แสดงว่าเป็นพิมพ์ปกติ
                                    rpt.Run()
                                    rpt.Document.Print(False, False) 'set false เพื่อไม่ให้ขึ้นหน้าเลือกเครื่อง printer
                                End If
                                Return 1
                            Else
                                Return 3
                            End If
                        Case "FORM441"
                            'view
                            Dim rpt = New rpt3_ediFORM441_pr
                            rpt.Document.Printer.PrinterName = PrintComplete(sendFormType, SiteID, sendUser, NamePrint_A, NamePrint_D, By_TempUser) 'ConfigurationManager.AppSettings("printNameCall").ToString()

                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then

                                rpt.C_TotalRowDe.text = dsRequestDetails.Tables(0).Rows.Count
                                rpt.DataSource = dsRequestDetails.Tables(0) 'ReportPrintClass.GetDataLoadTrd("20090416-000001", "ST-001") 'dsRequestDetail.Tables(0)

                                If sendFirstpage.Trim <> "" And sendLastpage.Trim <> "" Then 'แสดงว่าเป็นแบบเลือกหน้าพิมพ์
                                    selectPrintForm_(rpt, sendFirstpage, sendLastpage)
                                    rpt.Run()
                                    rpt.Document.Print(False, False) 'set false เพื่อไม่ให้ขึ้นหน้าเลือกเครื่อง printer
                                Else 'แสดงว่าเป็นพิมพ์ปกติ
                                    rpt.Run()
                                    rpt.Document.Print(False, False) 'set false เพื่อไม่ให้ขึ้นหน้าเลือกเครื่อง printer
                                End If
                                Return 1
                            Else
                                Return 3
                            End If
                        Case "FORM4_2"
                            'view
                            Dim rpt As Object
                            Select Case SiteID
                                Case Else
                                    rpt = New rpt3_ediFORM4_2_pr_New
                            End Select

                            rpt.Document.Printer.PrinterName = PrintComplete(sendFormType, SiteID, sendUser, NamePrint_A, NamePrint_D, By_TempUser) 'ConfigurationManager.AppSettings("printNameCall").ToString()

                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then
                                'ByTine 12-10-2559 B2B
                                rpt._TempB2B = LoadDataForB2B(Cells)

                                rpt.C_TotalRowDe.text = dsRequestDetails.Tables(0).Rows.Count
                                rpt.DataSource = dsRequestDetails.Tables(0) 'ReportPrintClass.GetDataLoadTrd("20090416-000001", "ST-001") 'dsRequestDetail.Tables(0)

                                If sendFirstpage.Trim <> "" And sendLastpage.Trim <> "" Then 'แสดงว่าเป็นแบบเลือกหน้าพิมพ์
                                    selectPrintForm_(rpt, sendFirstpage, sendLastpage)
                                    rpt.Run()
                                    rpt.Document.Print(False, False) 'set false เพื่อไม่ให้ขึ้นหน้าเลือกเครื่อง printer
                                Else 'แสดงว่าเป็นพิมพ์ปกติ
                                    rpt.Run()
                                    rpt.Document.Print(False, False) 'set false เพื่อไม่ให้ขึ้นหน้าเลือกเครื่อง printer
                                End If
                                Return 1
                            Else
                                Return 3
                            End If
                        Case "FORM4_3"
                            'view
                            Dim rpt = New rpt3_ediFORMs4_3_pr
                            rpt.Document.Printer.PrinterName = PrintComplete(sendFormType, SiteID, sendUser, NamePrint_A, NamePrint_D) 'ConfigurationManager.AppSettings("printNameCall").ToString()

                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then

                                rpt.C_TotalRowDe.text = dsRequestDetails.Tables(0).Rows.Count
                                rpt.DataSource = dsRequestDetails.Tables(0) 'ReportPrintClass.GetDataLoadTrd("20090416-000001", "ST-001") 'dsRequestDetail.Tables(0)

                                If sendFirstpage.Trim <> "" And sendLastpage.Trim <> "" Then 'แสดงว่าเป็นแบบเลือกหน้าพิมพ์
                                    selectPrintForm_(rpt, sendFirstpage, sendLastpage)
                                    rpt.Run()
                                    rpt.Document.Print(False, False) 'set false เพื่อไม่ให้ขึ้นหน้าเลือกเครื่อง printer
                                Else 'แสดงว่าเป็นพิมพ์ปกติ
                                    rpt.Run()
                                    rpt.Document.Print(False, False) 'set false เพื่อไม่ให้ขึ้นหน้าเลือกเครื่อง printer
                                End If
                                Return 1
                            Else
                                Return 3
                            End If
                        Case "FORM4_4"
                            'view
                            Dim rpt = New rpt3_ediFORM4_4_pr
                            '//rpt.Document.Printer.PrinterName = PrintComplete(sendFormType, SiteID, sendUser, NamePrint_A, NamePrint_D) 'ConfigurationManager.AppSettings("printNameCall").ToString()
                            rpt.Document.Printer.PrinterName = PrintComplete(sendFormType, SiteID, sendUser, NamePrint_A, NamePrint_D, By_TempUser)
                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then
                                rpt.txtTemp_SiteSend.text = SiteID
                                rpt.C_TotalRowDe.text = dsRequestDetails.Tables(0).Rows.Count
                                rpt.DataSource = dsRequestDetails.Tables(0) 'ReportPrintClass.GetDataLoadTrd("20090416-000001", "ST-001") 'dsRequestDetail.Tables(0)

                                If sendFirstpage.Trim <> "" And sendLastpage.Trim <> "" Then 'แสดงว่าเป็นแบบเลือกหน้าพิมพ์
                                    selectPrintForm_(rpt, sendFirstpage, sendLastpage)
                                    rpt.Run()
                                    rpt.Document.Print(False, False) 'set false เพื่อไม่ให้ขึ้นหน้าเลือกเครื่อง printer
                                Else 'แสดงว่าเป็นพิมพ์ปกติ
                                    rpt.Run()
                                    rpt.Document.Print(False, False) 'set false เพื่อไม่ให้ขึ้นหน้าเลือกเครื่อง printer
                                End If
                                Return 1
                            Else
                                Return 3
                            End If
                        Case "FORM4_5"
                            'view
                            Dim rpt = New rpt3_ediFORM4_5_pr
                            rpt.Document.Printer.PrinterName = PrintComplete(sendFormType, SiteID, sendUser, NamePrint_A, NamePrint_D, By_TempUser) 'ConfigurationManager.AppSettings("printNameCall").ToString()

                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then

                                rpt.txtTemp_SiteSend.text = SiteID
                                rpt.C_TotalRowDe.text = dsRequestDetails.Tables(0).Rows.Count
                                rpt.DataSource = dsRequestDetails.Tables(0) 'ReportPrintClass.GetDataLoadTrd("20090416-000001", "ST-001") 'dsRequestDetail.Tables(0)

                                If sendFirstpage.Trim <> "" And sendLastpage.Trim <> "" Then 'แสดงว่าเป็นแบบเลือกหน้าพิมพ์
                                    selectPrintForm_(rpt, sendFirstpage, sendLastpage)
                                    rpt.Run()
                                    rpt.Document.Print(False, False) 'set false เพื่อไม่ให้ขึ้นหน้าเลือกเครื่อง printer
                                Else 'แสดงว่าเป็นพิมพ์ปกติ
                                    rpt.Run()
                                    rpt.Document.Print(False, False) 'set false เพื่อไม่ให้ขึ้นหน้าเลือกเครื่อง printer
                                End If
                                Return 1
                            Else
                                Return 3
                            End If
                        Case "FORM4_6"
                            'view
                            Dim rpt = New rpt3_ediFORM4_6_pr
                            rpt.Document.Printer.PrinterName = PrintComplete(sendFormType, SiteID, sendUser, NamePrint_A, NamePrint_D) 'ConfigurationManager.AppSettings("printNameCall").ToString()

                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then

                                rpt.txtTemp_SiteSend.text = SiteID
                                rpt.C_TotalRowDe.text = dsRequestDetails.Tables(0).Rows.Count
                                rpt.DataSource = dsRequestDetails.Tables(0) 'ReportPrintClass.GetDataLoadTrd("20090416-000001", "ST-001") 'dsRequestDetail.Tables(0)
                                rpt.PageSettings.PaperKind = Printing.PaperKind.Custom
                                rpt.PageSettings.PaperHeight = 30.5
                                rpt.PageSettings.PaperWidth = 21

                                If sendFirstpage.Trim <> "" And sendLastpage.Trim <> "" Then 'แสดงว่าเป็นแบบเลือกหน้าพิมพ์
                                    selectPrintForm_(rpt, sendFirstpage, sendLastpage)
                                    rpt.Run()
                                    rpt.Document.Print(False, False) 'set false เพื่อไม่ให้ขึ้นหน้าเลือกเครื่อง printer
                                Else 'แสดงว่าเป็นพิมพ์ปกติ
                                    rpt.Run()
                                    rpt.Document.Print(False, False) 'set false เพื่อไม่ให้ขึ้นหน้าเลือกเครื่อง printer
                                End If
                                Return 1
                            Else
                                Return 3
                            End If
                        Case "FORM4_61" 'ใหม่
                            'view
                            Dim rpt As Object
                            rpt = New rpt3_ediFORM4_61_pr

                            rpt.Document.Printer.PrinterName = PrintComplete(sendFormType, SiteID, sendUser, NamePrint_A, NamePrint_D) 'ConfigurationManager.AppSettings("printNameCall").ToString()

                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then
                                'เงื่อนไขใหม่ ปรับแก้ วันที่ 03-12-2012 เกี่ยวกับ RVC สองอัน Case 2,8 ต้องแสดงมูลค่า ที่เหลือไม่แสดง
                                'begin RVC
                                rpt.txtCheck_CaseRVCCount.text = CheckCount_RVCByAJ(Cells, sendFormType)
                                rpt.txtDisplayUDS7.text = Check_DisplayUSD7(Cells)
                                'end RVC 

                                'ByTine 12-10-2559 B2B
                                'rpt._TempB2B = LoadDataForB2B(Cells)

                                rpt.txtTemp_SiteSend.text = SiteID
                                rpt.C_TotalRowDe.text = dsRequestDetails.Tables(0).Rows.Count
                                rpt.DataSource = dsRequestDetails.Tables(0) 'ReportPrintClass.GetDataLoadTrd("20090416-000001", "ST-001") 'dsRequestDetail.Tables(0)

                                If sendFirstpage.Trim <> "" And sendLastpage.Trim <> "" Then 'แสดงว่าเป็นแบบเลือกหน้าพิมพ์
                                    selectPrintForm_(rpt, sendFirstpage, sendLastpage)
                                    rpt.Run()
                                    rpt.Document.Print(False, False) 'set false เพื่อไม่ให้ขึ้นหน้าเลือกเครื่อง printer
                                Else 'แสดงว่าเป็นพิมพ์ปกติ
                                    rpt.Run()
                                    rpt.Document.Print(False, False) 'set false เพื่อไม่ให้ขึ้นหน้าเลือกเครื่อง printer
                                End If
                                Return 1
                            Else
                                Return 3
                            End If
                        Case "FORM4_8" 'ใหม่
                            'view
                            Dim rpt As Object
                            Select Case Check_Form4_8(Cells)
                                Case True 'ใหม่
                                    rpt = New rpt3_ediFORM4_8_New_pr
                                    'เงื่อนไขใหม่ ปรับแก้ วันที่ 03-12-2012 เกี่ยวกับ RVC สองอัน Case 2,8 ต้องแสดงมูลค่า ที่เหลือไม่แสดง
                                    'begin RVC
                                    rpt.txtCheck_CaseRVCCount.text = CheckCount_RVC(Cells, sendFormType)
                                    rpt.txtDisplayUDS7.text = Check_DisplayUSD7(Cells)
                                        'end RVC 

                                        'ByTine 12-10-2559 B2B
                                        'rpt._TempB2B = LoadDataForB2B(Cells)

                                Case False 'เก่า
                                    rpt = New rpt3_ediFORM4_8_pr
                            End Select
                            rpt.Document.Printer.PrinterName = PrintComplete(sendFormType, SiteID, sendUser, NamePrint_A, NamePrint_D) 'ConfigurationManager.AppSettings("printNameCall").ToString()

                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then
                                rpt.txtTemp_SiteSend.text = SiteID
                                rpt.C_TotalRowDe.text = dsRequestDetails.Tables(0).Rows.Count
                                rpt.DataSource = dsRequestDetails.Tables(0) 'ReportPrintClass.GetDataLoadTrd("20090416-000001", "ST-001") 'dsRequestDetail.Tables(0)

                                If sendFirstpage.Trim <> "" And sendLastpage.Trim <> "" Then 'แสดงว่าเป็นแบบเลือกหน้าพิมพ์
                                    selectPrintForm_(rpt, sendFirstpage, sendLastpage)
                                    rpt.Run()
                                    rpt.Document.Print(False, False) 'set false เพื่อไม่ให้ขึ้นหน้าเลือกเครื่อง printer
                                Else 'แสดงว่าเป็นพิมพ์ปกติ
                                    rpt.Run()
                                    rpt.Document.Print(False, False) 'set false เพื่อไม่ให้ขึ้นหน้าเลือกเครื่อง printer
                                End If
                                Return 1
                            Else
                                Return 3
                            End If
                        Case "FORM4_81" 'เก่า
                            'viw
                            Dim rpt = New rpt3_ediFORM4_8_pr
                            rpt.Document.Printer.PrinterName = PrintComplete(sendFormType, SiteID, sendUser, NamePrint_A, NamePrint_D) 'ConfigurationManager.AppSettings("printNameCall").ToString()

                            ' Dim dataReadRfcard As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "vi_Nr_Rfcard_GetAll", New SqlParameter("@card_id ", CommonUtility.Get_String(dsRequestDetails.Tables(0).Rows(0).Item("card_id"))))

                            ' Dim dataRead_Sum As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "sp_getSummaryPrintForm", New SqlParameter("@invh_run_auto ", CommonUtility.Get_String(Cells)))

                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then
                                'If dataRead_Sum.Read Then
                                '    rpt.txtSum_Gross_Weight.Text = dataRead_Sum.Item("Gross_Weight").ToString()
                                '    rpt.txtSum_g_Unit_Desc.Text = dataRead_Sum.Item("g_Unit_Desc").ToString()

                                '    dataRead_Sum.Close()
                                'End If
                                rpt.txtTemp_SiteSend.text = SiteID
                                rpt.C_TotalRowDe.text = dsRequestDetails.Tables(0).Rows.Count
                                rpt.DataSource = dsRequestDetails.Tables(0) 'ReportPrintClass.GetDataLoadTrd("20090416-000001", "ST-001") 'dsRequestDetail.Tables(0)
                                rpt.PageSettings.PaperKind = Printing.PaperKind.Custom
                                rpt.PageSettings.PaperHeight = 30.5
                                rpt.PageSettings.PaperWidth = 21

                                If sendFirstpage.Trim <> "" And sendLastpage.Trim <> "" Then 'แสดงว่าเป็นแบบเลือกหน้าพิมพ์
                                    selectPrintForm_(rpt, sendFirstpage, sendLastpage)
                                    rpt.Run()
                                    rpt.Document.Print(False, False) 'set false เพื่อไม่ให้ขึ้นหน้าเลือกเครื่อง printer
                                Else 'แสดงว่าเป็นพิมพ์ปกติ
                                    rpt.Run()
                                    rpt.Document.Print(False, False) 'set false เพื่อไม่ให้ขึ้นหน้าเลือกเครื่อง printer
                                End If
                                Return 1
                            Else
                                Return 3
                            End If
                        Case "FORM4_9"
                            'view
                            Dim rpt = New rpt3_ediFORM4_9_pr
                            rpt.Document.Printer.PrinterName = PrintComplete(sendFormType, SiteID, sendUser, NamePrint_A, NamePrint_D, By_TempUser) 'ConfigurationManager.AppSettings("printNameCall").ToString()

                            'Dim dataReadRfcard As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "vi_Nr_Rfcard_GetAll", New SqlParameter("@card_id ", CommonUtility.Get_String(dsRequestDetails.Tables(0).Rows(0).Item("card_id"))))

                            'Dim dataRead_Sum As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "sp_getSummaryPrintForm", New SqlParameter("@invh_run_auto ", CommonUtility.Get_String(Cells)))

                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then
                                'If dataRead_Sum.Read Then
                                '    rpt.txtSum_Gross_Weight.Text = dataRead_Sum.Item("Gross_Weight").ToString()
                                '    rpt.txtSum_g_Unit_Desc.Text = dataRead_Sum.Item("g_Unit_Desc").ToString()

                                '    dataRead_Sum.Close()
                                'End If
                                ''ByTine B2B 17-03-2560
                                rpt._TempB2B = LoadDataForB2B(Cells)

                                rpt.C_TotalRowDe.text = dsRequestDetails.Tables(0).Rows.Count
                                rpt.DataSource = dsRequestDetails.Tables(0) 'ReportPrintClass.GetDataLoadTrd("20090416-000001", "ST-001") 'dsRequestDetail.Tables(0)
                                'rpt.PageSettings.PaperKind = Printing.PaperKind.Custom
                                'rpt.PageSettings.PaperHeight = 30.5
                                'rpt.PageSettings.PaperWidth = 21

                                If sendFirstpage.Trim <> "" And sendLastpage.Trim <> "" Then 'แสดงว่าเป็นแบบเลือกหน้าพิมพ์
                                    selectPrintForm_(rpt, sendFirstpage, sendLastpage)
                                    rpt.Run()
                                    rpt.Document.Print(False, False) 'set false เพื่อไม่ให้ขึ้นหน้าเลือกเครื่อง printer
                                Else 'แสดงว่าเป็นพิมพ์ปกติ
                                    rpt.Run()
                                    rpt.Document.Print(False, False) 'set false เพื่อไม่ให้ขึ้นหน้าเลือกเครื่อง printer
                                End If
                                Return 1
                            Else
                                Return 3
                            End If
                        Case "FORM4_91"
                            'view
                            Dim rpt = New rpt3_ediFORM4_91_pr

                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then
                                rpt.txtTemp_SiteSend.text = SiteID
                                rpt.C_TotalRowDe.text = dsRequestDetails.Tables(0).Rows.Count
                                rpt.DataSource = dsRequestDetails.Tables(0) 'ReportPrintClass.GetDataLoadTrd("20090416-000001", "ST-001") 'dsRequestDetail.Tables(0)

                                If sendFirstpage.Trim <> "" And sendLastpage.Trim <> "" Then 'แสดงว่าเป็นแบบเลือกหน้าพิมพ์
                                    selectPrintForm_(rpt, sendFirstpage, sendLastpage)
                                    rpt.Run()
                                    rpt.Document.Print(False, False) 'set false เพื่อไม่ให้ขึ้นหน้าเลือกเครื่อง printer
                                Else 'แสดงว่าเป็นพิมพ์ปกติ
                                    rpt.Run()
                                    rpt.Document.Print(False, False) 'set false เพื่อไม่ให้ขึ้นหน้าเลือกเครื่อง printer
                                End If
                                Return 1
                            Else
                                Return 3
                            End If

                                ''ByTine 23-03-2559 AANZ ใหม่
                        Case "FORM4_911"
                            'view
                            Dim rpt = New rpt3_ediFORM4_911_pr
                            rpt.Document.Printer.PrinterName = PrintComplete(sendFormType, SiteID, sendUser, NamePrint_A, NamePrint_D) 'ConfigurationManager.AppSettings("printNameCall").ToString()

                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then
                                rpt.txtTemp_SiteSend.text = SiteID
                                rpt.txtCheck_CaseRVCCount.text = CheckCount_RVC(Cells, sendFormType)
                                rpt.C_TotalRowDe.text = dsRequestDetails.Tables(0).Rows.Count
                                rpt.DataSource = dsRequestDetails.Tables(0) 'ReportPrintClass.GetDataLoadTrd("20090416-000001", "ST-001") 'dsRequestDetail.Tables(0)

                                If sendFirstpage.Trim <> "" And sendLastpage.Trim <> "" Then 'แสดงว่าเป็นแบบเลือกหน้าพิมพ์
                                    selectPrintForm_(rpt, sendFirstpage, sendLastpage)
                                    rpt.Run()
                                    rpt.Document.Print(False, False) 'set false เพื่อไม่ให้ขึ้นหน้าเลือกเครื่อง printer
                                Else 'แสดงว่าเป็นพิมพ์ปกติ
                                    rpt.Run()
                                    rpt.Document.Print(False, False) 'set false เพื่อไม่ให้ขึ้นหน้าเลือกเครื่อง printer
                                End If
                                Return 1
                            Else
                                Return 3
                            End If

                        Case "FORM5"
                            'view
                            Dim rpt = New rpt3_ediFORM5_pr
                            rpt.Document.Printer.PrinterName = PrintComplete(sendFormType, SiteID, sendUser, NamePrint_A, NamePrint_D) 'ConfigurationManager.AppSettings("printNameCall").ToString()

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

                                If sendFirstpage.Trim <> "" And sendLastpage.Trim <> "" Then 'แสดงว่าเป็นแบบเลือกหน้าพิมพ์
                                    selectPrintForm_(rpt, sendFirstpage, sendLastpage)
                                    rpt.Run()
                                    rpt.Document.Print(False, False) 'set false เพื่อไม่ให้ขึ้นหน้าเลือกเครื่อง printer
                                Else 'แสดงว่าเป็นพิมพ์ปกติ
                                    rpt.Run()
                                    rpt.Document.Print(False, False) 'set false เพื่อไม่ให้ขึ้นหน้าเลือกเครื่อง printer
                                End If
                                Return 1
                            Else
                                Return 3
                            End If
                        Case "FORM5_1"
                            'view
                            Dim rpt = New rpt3_ediFORM5_1_pr
                            rpt.Document.Printer.PrinterName = PrintComplete(sendFormType, SiteID, sendUser, NamePrint_A, NamePrint_D) 'ConfigurationManager.AppSettings("printNameCall").ToString()

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

                                If sendFirstpage.Trim <> "" And sendLastpage.Trim <> "" Then 'แสดงว่าเป็นแบบเลือกหน้าพิมพ์
                                    selectPrintForm_(rpt, sendFirstpage, sendLastpage)
                                    rpt.Run()
                                    rpt.Document.Print(False, False) 'set false เพื่อไม่ให้ขึ้นหน้าเลือกเครื่อง printer
                                Else 'แสดงว่าเป็นพิมพ์ปกติ
                                    rpt.Run()
                                    rpt.Document.Print(False, False) 'set false เพื่อไม่ให้ขึ้นหน้าเลือกเครื่อง printer
                                End If
                                Return 1
                            Else
                                Return 3
                            End If

                                ''''''''''''''''''''''''''''''''''
                                ''''''''''''''''''''''''''''''''''
                                ''ByTine 06-10-2558 เพิ่มฟอร์มไทย-ชิลี (FORM5_2)
                        Case "FORM5_2"
                            'view
                            Dim rpt = New rpt3_ediFORM5_2_pr
                            rpt.Document.Printer.PrinterName = PrintComplete(sendFormType, SiteID, sendUser, NamePrint_A, NamePrint_D) 'ConfigurationManager.AppSettings("printNameCall").ToString()
                            If dsRequestDetails.Tables(0).Rows.Count > 0 Then
                                rpt.C_TotalRowDe.text = dsRequestDetails.Tables(0).Rows.Count
                                rpt.DataSource = dsRequestDetails.Tables(0) 'ReportPrintClass.GetDataLoadTrd("20090416-000001", "ST-001") 'dsRequestDetail.Tables(0)

                                ''ByTine 04-05-2559 เพิ่มเติมกรณีไม่ต้องการแสดง Inv ต่างประเทศ
                                If DBNull.Value.Equals(dsRequestDetails.Tables(0).Rows(0).Item("NoneDisplayInv10")) = True Then
                                    rpt.ChkNoneDisplayInv10.Checked = False
                                Else
                                    rpt.ChkNoneDisplayInv10.Checked = dsRequestDetails.Tables(0).Rows(0).Item("NoneDisplayInv10")
                                End If

                                If sendFirstpage.Trim <> "" And sendLastpage.Trim <> "" Then 'แสดงว่าเป็นแบบเลือกหน้าพิมพ์
                                    selectPrintForm_(rpt, sendFirstpage, sendLastpage)
                                    rpt.Run()
                                    rpt.Document.Print(False, False) 'set false เพื่อไม่ให้ขึ้นหน้าเลือกเครื่อง printer
                                Else 'แสดงว่าเป็นพิมพ์ปกติ
                                    rpt.Run()
                                    rpt.Document.Print(False, False) 'set false เพื่อไม่ให้ขึ้นหน้าเลือกเครื่อง printer
                                End If
                                Return 1
                            Else
                                Return 3
                            End If

                        Case "FORM6"
                            'view
                            Dim rpt = New rpt3_ediFORM6_pr
                            rpt.Document.Printer.PrinterName = PrintComplete(sendFormType, SiteID, sendUser, NamePrint_A, NamePrint_D) 'ConfigurationManager.AppSettings("printNameCall").ToString()

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

                                If sendFirstpage.Trim <> "" And sendLastpage.Trim <> "" Then 'แสดงว่าเป็นแบบเลือกหน้าพิมพ์
                                    selectPrintForm_(rpt, sendFirstpage, sendLastpage)
                                    rpt.Run()
                                    rpt.Document.Print(False, False) 'set false เพื่อไม่ให้ขึ้นหน้าเลือกเครื่อง printer
                                Else 'แสดงว่าเป็นพิมพ์ปกติ
                                    rpt.Run()
                                    rpt.Document.Print(False, False) 'set false เพื่อไม่ให้ขึ้นหน้าเลือกเครื่อง printer
                                End If
                                Return 1
                            Else
                                Return 3
                            End If
                        Case "FORM7"
                            'view
                            Dim rpt = New rpt3_ediFORM8_7_pr 'rpt3_ediFORM7_pr
                            rpt.Document.Printer.PrinterName = PrintComplete(sendFormType, SiteID, sendUser, NamePrint_A, NamePrint_D) 'ConfigurationManager.AppSettings("printNameCall").ToString()

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

                                If sendFirstpage.Trim <> "" And sendLastpage.Trim <> "" Then 'แสดงว่าเป็นแบบเลือกหน้าพิมพ์
                                    selectPrintForm_(rpt, sendFirstpage, sendLastpage)
                                    rpt.Run()
                                    rpt.Document.Print(False, False) 'set false เพื่อไม่ให้ขึ้นหน้าเลือกเครื่อง printer
                                Else 'แสดงว่าเป็นพิมพ์ปกติ
                                    rpt.Run()
                                    rpt.Document.Print(False, False) 'set false เพื่อไม่ให้ขึ้นหน้าเลือกเครื่อง printer
                                End If
                                Return 1
                            Else
                                Return 3
                            End If
                        Case "FORM8"
                            'view
                            Dim rpt = New rpt3_ediFORM8_pr
                            rpt.Document.Printer.PrinterName = PrintComplete(sendFormType, SiteID, sendUser, NamePrint_A, NamePrint_D) 'ConfigurationManager.AppSettings("printNameCall").ToString()

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

                                If sendFirstpage.Trim <> "" And sendLastpage.Trim <> "" Then 'แสดงว่าเป็นแบบเลือกหน้าพิมพ์
                                    selectPrintForm_(rpt, sendFirstpage, sendLastpage)
                                    rpt.Run()
                                    rpt.Document.Print(False, False) 'set false เพื่อไม่ให้ขึ้นหน้าเลือกเครื่อง printer
                                Else 'แสดงว่าเป็นพิมพ์ปกติ
                                    rpt.Run()
                                    rpt.Document.Print(False, False) 'set false เพื่อไม่ให้ขึ้นหน้าเลือกเครื่อง printer
                                End If
                                Return 1
                            Else
                                Return 3
                            End If
                        Case "FORM9"
                            'view
                            Dim rpt = New rpt3_ediFORM9_pr 'rpt3_ediFORM7_pr 'rpt3_ediFORM9_pr
                            rpt.Document.Printer.PrinterName = PrintComplete(sendFormType, SiteID, sendUser, NamePrint_A, NamePrint_D) 'ConfigurationManager.AppSettings("printNameCall").ToString()

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

                                If sendFirstpage.Trim <> "" And sendLastpage.Trim <> "" Then 'แสดงว่าเป็นแบบเลือกหน้าพิมพ์
                                    selectPrintForm_(rpt, sendFirstpage, sendLastpage)
                                    rpt.Run()
                                    rpt.Document.Print(False, False) 'set false เพื่อไม่ให้ขึ้นหน้าเลือกเครื่อง printer
                                Else 'แสดงว่าเป็นพิมพ์ปกติ
                                    rpt.Run()
                                    rpt.Document.Print(False, False) 'set false เพื่อไม่ให้ขึ้นหน้าเลือกเครื่อง printer
                                End If
                                Return 1
                            Else
                                Return 3
                            End If

                        Case Else
                            Return 3
                    End Select
                End If
            Catch ex As Exception
                Return 3
            End Try
        Else 'xxxx
            Return 2
        End If
    End Function
#End Region

#Region "   ใช้เรียกชื่อ เครื่องคำขอ site ต่างๆ   "
    'ไว้เรียกชื่อเครื่อง คำขอ-----------------------------------------------
    Public Shared Function PrintReceipt(ByVal sendFormTypePrinter As String, ByVal sendSiteId As String, ByVal sendRequest As String, ByVal sendUser_ As String) As String
        Dim Print_name As String
        Select Case sendSiteId
            Case "ST-001"
                Select Case sendUser_
                    '    Case "TIPPARPORN", "SURANG" 'set DS 2
                    '        Select Case sendRequest
                    '            Case "1"
                    '                Print_name = Call_SiteReceipt_ST_001_03(sendFormTypePrinter)
                    '                Return Print_name
                    '        End Select
                    Case "nook" 'set DS 2
                        Select Case sendRequest
                            Case "1"
                                Print_name = Call_SiteReceiptTest_ST_001_01(sendFormTypePrinter)
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
                            Case "3"
                                Print_name = Call_SiteReceipt_ST_001_02(sendFormTypePrinter)
                                Return Print_name
                        End Select
                End Select
            Case "ST-001T" 'ใช้กับ Site ST-001 ได้เพราะ อยู่ที่เดียวกัน แต่ ST-001 ไม่พิมพ์งาน ST-001T
                Print_name = Call_SiteReceipt_ST_001_01(sendFormTypePrinter)
                Return Print_name
            Case "ST-002", "ST-004N", "ST-004"  'คลองเตย
                'ปรับเพื่อ pinter เฉพาะ DS ไปเครื่องอื่น

                '//คำขอ EDI,DS ออกเครื่องเดียวกัน 2017-10-07
                Select Case sendRequest
                    Case "1"
                        Print_name = Call_SiteReceipt_ST_002_01(sendFormTypePrinter)
                        Return Print_name
                    Case "2"
                        Print_name = Call_SiteReceipt_ST_002_02(sendFormTypePrinter)
                        Return Print_name
                    Case "3"
                        Print_name = Call_SiteReceipt_ST_002_03(sendFormTypePrinter)
                        Return Print_name
                    Case "4"
                        Print_name = Call_SiteReceipt_ST_002_04(sendFormTypePrinter)
                        Return Print_name
                    Case "5"
                        Print_name = Call_SiteReceipt_ST_002_05(sendFormTypePrinter)
                        Return Print_name
                    Case "6"
                        Print_name = Call_SiteReceipt_ST_002_06(sendFormTypePrinter)
                        Return Print_name
                    Case "9" 'by rut add เพิ่ม 12-04-2555
                        Print_name = Call_SiteReceipt_ST_002_09(sendFormTypePrinter)
                        Return Print_name
                    Case "10" 'by rut add เพิ่ม 12-04-2555
                        Print_name = Call_SiteReceipt_ST_002_10(sendFormTypePrinter)
                        Return Print_name
                End Select

                'Select Case sendUser_
                '    Case "EDI_DS"
                '        'by rut
                '        Select Case sendRequest
                '            Case "11"
                '                Print_name = Call_SiteReceipt_ST_002_11(sendFormTypePrinter)
                '                Return Print_name

                '                'Case "7"
                '                '    Print_name = Call_SiteReceipt_ST_002_07(sendFormTypePrinter)
                '                '    Return Print_name
                '                'Case "8"
                '                '    Print_name = Call_SiteReceipt_ST_002_08(sendFormTypePrinter)
                '                '    Return Print_name
                '        End Select
                '        'Print_name = Call_SiteReceipt_ST_002_07(sendFormTypePrinter)
                '        'Return Print_name
                '    Case Else
                '        Select Case sendRequest
                '            Case "1"
                '                Print_name = Call_SiteReceipt_ST_002_01(sendFormTypePrinter)
                '                Return Print_name
                '            Case "2"
                '                Print_name = Call_SiteReceipt_ST_002_02(sendFormTypePrinter)
                '                Return Print_name
                '            Case "3"
                '                Print_name = Call_SiteReceipt_ST_002_03(sendFormTypePrinter)
                '                Return Print_name
                '            Case "4"
                '                Print_name = Call_SiteReceipt_ST_002_04(sendFormTypePrinter)
                '                Return Print_name
                '            Case "5"
                '                Print_name = Call_SiteReceipt_ST_002_05(sendFormTypePrinter)
                '                Return Print_name
                '            Case "6"
                '                Print_name = Call_SiteReceipt_ST_002_06(sendFormTypePrinter)
                '                Return Print_name
                '            Case "9" 'by rut add เพิ่ม 12-04-2555
                '                Print_name = Call_SiteReceipt_ST_002_09(sendFormTypePrinter)
                '                Return Print_name
                '            Case "10" 'by rut add เพิ่ม 12-04-2555
                '                Print_name = Call_SiteReceipt_ST_002_10(sendFormTypePrinter)
                '                Return Print_name
                '        End Select
                'End Select

            Case "ST-003" 'กรมการค้าต่างประเทศ (สุวรรณภูมิ)
                Select Case sendRequest
                    Case "1"
                        Print_name = Call_SiteReceipt_ST_003_01(sendFormTypePrinter)
                        Return Print_name
                    Case "2"
                        Print_name = Call_SiteReceipt_ST_003_02(sendFormTypePrinter)
                        Return Print_name
                    Case "3"
                        Print_name = Call_SiteReceipt_ST_003_03(sendFormTypePrinter)
                        Return Print_name
                End Select

            Case "ST-004XXX" 'กรมการค้าต่างประเทศ (สอ)
                Select Case sendUser_
                    Case "EDI_DS"
                        Print_name = Call_SiteReceipt_ST_004_01DS(sendFormTypePrinter)
                        Return Print_name
                    Case Else
                        'Print_name = Call_SiteReceipt_ST_004_01(sendFormTypePrinter)
                        'Return Print_name
                        Select Case sendRequest
                            Case "1"
                                Print_name = Call_SiteReceipt_ST_004_01(sendFormTypePrinter)
                                Return Print_name
                            Case "2"
                                Print_name = Call_SiteReceipt_ST_004_02(sendFormTypePrinter)
                                Return Print_name
                        End Select
                End Select

            Case "ST-005" 'กรมการค้าต่างประเทศ (ตลาดไท)
                Print_name = Call_SiteReceipt_ST_005_01(sendFormTypePrinter)
                Return Print_name
            Case "CB-003" 'สคต.เขต3 (ชลบุรี)
                'ปรับเพื่อ pinter เฉพาะ DS ไปเครื่องอื่น
                Select Case sendUser_
                    Case "EDI_DS"
                        Select Case sendRequest
                            Case "3"
                                Print_name = Call_SiteReceipt_CB_003_03(sendFormTypePrinter)
                                Return Print_name
                            Case "1"
                                Print_name = Call_SiteReceipt_CB_003_01(sendFormTypePrinter)
                                Return Print_name
                        End Select

                    Case Else
                        Select Case sendRequest
                            Case "1"
                                Print_name = Call_SiteReceipt_CB_003_01(sendFormTypePrinter)
                                Return Print_name
                            Case "2"
                                Print_name = Call_SiteReceipt_CB_003_02(sendFormTypePrinter)
                                Return Print_name
                        End Select
                End Select

            Case "HY-002" 'สงขลา
                'by rut set DS
                Select Case sendUser_
                    Case "EDI_DS"
                        Select Case sendRequest
                            Case "1"
                                Print_name = Call_SiteReceipt_HY_002_01(sendFormTypePrinter)
                                Return Print_name
                            Case "2"
                                Print_name = Call_SiteReceipt_HY_002_02(sendFormTypePrinter)
                                Return Print_name
                        End Select
                    Case Else
                        Select Case sendRequest
                            Case "1"
                                Print_name = Call_SiteReceipt_HY_002_01(sendFormTypePrinter)
                                Return Print_name
                            Case "2"
                                Print_name = Call_SiteReceipt_HY_002_02(sendFormTypePrinter)
                                Return Print_name
                        End Select
                End Select
                'Select Case sendRequest
                '    Case "1"
                '        Print_name = Call_SiteReceipt_HY_002_01(sendFormTypePrinter)
                '        Return Print_name
                '    Case "2"
                '        Print_name = Call_SiteReceipt_HY_002_02(sendFormTypePrinter)
                '        Return Print_name
                'End Select

            Case "CR-006" 'เชียงราย
                Print_name = Call_SiteReceipt_CR_006_01(sendFormTypePrinter)
                Return Print_name
            Case "CM-001" 'เชียงใหม่
                Print_name = Call_SiteReceipt_CM_001_01(sendFormTypePrinter)
                Return Print_name
            Case "SK-004" 'สระแก้ว
                Print_name = Call_SiteReceipt_SK_004_01(sendFormTypePrinter)
                Return Print_name
            Case "NK-005" 'หนองคาย
                Print_name = Call_SiteReceipt_NK_005_01(sendFormTypePrinter)
                Return Print_name
            Case "SG-007" 'ศรีสะเกษ
                Select Case sendRequest
                    Case "1"
                        Print_name = Call_SiteReceipt_SG_007_01(sendFormTypePrinter)
                        Return Print_name
                        'Case "2"
                        '    Print_name = Call_SiteReceipt_ST_003_02(sendFormTypePrinter)
                        '    Return Print_name
                End Select
            Case "TK-008" 'ตาก
                Select Case sendRequest
                    Case "1"
                        Print_name = Call_SiteReceipt_TK_008_01(sendFormTypePrinter)
                        Return Print_name
                        'Case "2"
                        '    Print_name = Call_SiteReceipt_ST_003_02(sendFormTypePrinter)
                        '    Return Print_name
                End Select
            Case "MH-009" 'มุกดาหาร 12-01-2556
                Select Case sendRequest
                    Case "1"
                        Print_name = Call_SiteReceipt_MH_009_01(sendFormTypePrinter)
                        Return Print_name
                        'Case "2"
                        '    Print_name = Call_SiteReceipt_ST_003_02(sendFormTypePrinter)
                        '    Return Print_name
                End Select
            Case "KR-010" 'สคต.เขต 10 (กาญจนบุรี)
                Print_name = Call_SiteReceipt_KR_010_01(sendFormTypePrinter)
                Return Print_name
            Case "PN-83-001" 'สพจ.ภูเก็ต PN-83-001 17-10-31
                Print_name = Call_SiteReceipt_PN_83_001(sendFormTypePrinter, sendRequest)
                Return Print_name
            Case "PN-95-001" 'สพจ.ยะลา PN-95-001 17-10-31
                Print_name = Call_SiteReceipt_PN_95_001(sendFormTypePrinter, sendRequest)
                Return Print_name
            Case "PN-96-001" 'สพจ.นราธิวาส PN-96-001 17-10-31
                Print_name = Call_SiteReceipt_PN_96_001(sendFormTypePrinter, sendRequest)
                Return Print_name
            Case "PN-91-001" 'สพจ.สตูล PN-91-001 17-10-31
                Print_name = Call_SiteReceipt_PN_91_001(sendFormTypePrinter, sendRequest)
                Return Print_name
            Case "PN-48-001" 'สพจ.นครพนม PN_48_001 17-10-31
                Print_name = Call_SiteReceipt_PN_48_001(sendFormTypePrinter, sendRequest)
                Return Print_name
            Case "PN-51-001" 'สพจ.ลำพูด PN-51-001 02-11-2018
                Print_name = Call_SiteReceipt_PN_51_001(sendFormTypePrinter, sendRequest)
                Return Print_name
        End Select
    End Function
#End Region

#Region "   ใช้เรียกชื่อ เครื่องหนังสือรับรอง site ต่างๆ   "
    'ไว้เรียกชื่อเครื่อง printer หนังสือรับรองหรือฟอร์ม
    Public Shared Function PrintComplete(ByVal sendFormTypePrinter As String, ByVal sendSiteId As String, ByVal sendUser_ As String, ByVal Com_NamePrint_A As String, ByVal Com_NamePrint_D As String, Optional ByVal By_CheckUser As String = "") As String
        Dim Print_name As String

        Select Case sendSiteId
            Case "ST-001"
                Select Case sendUser_
                    'Case "TIPPARPORN", "SURANG"
                    '    Print_name = Call_SitePrints_ST_001_01(sendFormTypePrinter)
                    '    Return Print_name
                    Case "nook"
                        Print_name = Call_SitePrintsTest_ST_001(sendFormTypePrinter)
                        Return Print_name
                    Case "EDI_DS"
                        Print_name = Call_SitePrints_ST_001DS(sendFormTypePrinter, Com_NamePrint_A, Com_NamePrint_D)
                        Return Print_name
                    Case Else
                        Print_name = Call_SitePrints_ST_001(sendFormTypePrinter, Com_NamePrint_A, Com_NamePrint_D)
                        Return Print_name
                End Select

            Case "ST-001T" 'ที่ คต form2_1
                Print_name = Call_SitePrints_ST_001(sendFormTypePrinter, Com_NamePrint_A, Com_NamePrint_D)
                Return Print_name
            Case "ST-002", "ST-004N", "ST-004"  'คลองเตย
                'set ไว้ออกเฉพาะ DS

                Select Case sendUser_
                    Case "EDI_DS"
                        'by phat กรณีมีเครื่อง Print หลายเครื่อง แยก ตามชื่อ จนท  เครื่องแรก 
                        Select Case By_CheckUser.ToLower
                            Case "piyawatrds", "phornthepkds", "prapapornkds", "admin002", "adminst004"
                                Print_name = Call_SitePrints_ST_002DS(sendFormTypePrinter)
                                Return Print_name
                            Case "yingsikanpds", "chaveewands2"
                                Print_name = Call_SitePrints_ST_002DS_V03(sendFormTypePrinter)
                                Return Print_name
                            Case Else
                                ' เครื่อง 2
                                Print_name = Call_SitePrints_ST_002DS_V02(sendFormTypePrinter)
                                Return Print_name
                        End Select

                    Case Else
                        'by rut เนื่องจากคลองเตยมีเครื่องพิมพ์ฟอร์มเดียวกันหลายเครื่อง ตอนพิมพ์หนังสือรับรองย้อนหลัง ไม่ได้แก้ เนื่องจากให้ออกเครื่องปกติเลย
                        Select Case By_CheckUser.ToLower
                            Case "wasana", "srisuda", "chaveewan", "chaweewan", "CHITRAK".ToLower, "pichayas", "chitrak"
                                Print_name = Call_SitePrints_ST_002_V02(sendFormTypePrinter)
                                Return Print_name
                            Case Else
                                Print_name = Call_SitePrints_ST_002(sendFormTypePrinter)
                                Return Print_name
                        End Select
                End Select

            Case "ST-003" 'สุวรรณภูมิ
                Select Case sendUser_
                    Case "EDI_DS"
                        Select Case By_CheckUser.ToLower
                            Case "naphato"
                                '//เครื่องใหม่
                                Print_name = Call_SitePrintsDS_ST_003_V02(sendFormTypePrinter)
                                Return Print_name
                            Case Else
                                '//เครื่องเก่า
                                Print_name = Call_SitePrintsDS_ST_003(sendFormTypePrinter)
                                Return Print_name
                        End Select

                        '//Backup เครื่องเก่า
                        'Print_name = Call_SitePrintsDS_ST_003(sendFormTypePrinter)
                        'Return Print_name
                    Case Else
                        Print_name = Call_SitePrints_ST_003(sendFormTypePrinter)
                        Return Print_name
                End Select

            Case "ST-004XXX" 'กรมการค้าต่างประเทศ (สอ)
                'set ไว้ออกเฉพาะ DS
                Select Case sendUser_
                    Case "EDI_DS"
                        Print_name = Call_SitePrintsDS_ST_004(sendFormTypePrinter)
                        Return Print_name
                    Case Else
                        Print_name = Call_SitePrints_ST_004(sendFormTypePrinter)
                        Return Print_name
                End Select
                'Print_name = Call_SitePrints_ST_004(sendFormTypePrinter)
                'Return Print_name
            Case "ST-005" 'กรมการค้าต่างประเทศ (ตลาดไท)
                Print_name = Call_SitePrints_ST_005(sendFormTypePrinter)
                Return Print_name
            Case "CB-003" 'สคต.เขต3 (ชลบุรี)
                'set ไว้ออกเฉพาะ DS
                Select Case sendUser_
                    Case "EDI_DS"
                        Print_name = Call_SitePrints_CB_003DS(sendFormTypePrinter)
                        Return Print_name
                    Case Else
                        Print_name = Call_SitePrints_CB_003(sendFormTypePrinter)
                        Return Print_name
                End Select

            Case "HY-002" 'สงขลา
                'by rut set DS
                Select Case sendUser_
                    Case "EDI_DS"
                        Print_name = Call_SitePrints_HY_002(sendFormTypePrinter)
                        Return Print_name
                    Case Else
                        Print_name = Call_SitePrints_HY_002(sendFormTypePrinter)
                        Return Print_name
                End Select
                'Print_name = Call_SitePrints_HY_002(sendFormTypePrinter)
                'Return Print_name
            Case "CR-006" 'เชียงราย
                Print_name = Call_SitePrints_CR_006(sendFormTypePrinter)
                Return Print_name
            Case "CM-001" 'เชียงใหม่
                Print_name = Call_SitePrints_CM_001(sendFormTypePrinter)
                Return Print_name
            Case "SK-004" 'สระแก้ว
                Print_name = Call_SitePrints_SK_004(sendFormTypePrinter)
                Return Print_name
            Case "NK-005" 'สระแก้ว
                Print_name = Call_SitePrints_NK_005(sendFormTypePrinter)
                Return Print_name
            Case "SG-007" 'ศรีสะเกษ
                Print_name = Call_SitePrints_SG_007(sendFormTypePrinter)
                Return Print_name
            Case "TK-008" 'ตาก 22-01-2556
                Print_name = Call_SitePrints_TK_008(sendFormTypePrinter)
                Return Print_name
            Case "MH-009" 'มุกดาหาร 12-02-2556
                Print_name = Call_SitePrints_MH_009(sendFormTypePrinter)
                Return Print_name
            Case "KR-010" 'สคต.เขต 10 (กาญจนบุรี) KR-010 12-09-2556
                Print_name = Call_SitePrints_KR_010(sendFormTypePrinter)
                Return Print_name
            Case "PN-83-001" 'สพจ.ภูเก็ต PN-83-001 17-10-31
                Print_name = Call_SitePrints_PN_83_001(sendFormTypePrinter)
                Return Print_name
            Case "PN-95-001" 'สพจ.ยะลา PN-95-001 17-10-31
                Print_name = Call_SitePrints_PN_95_001(sendFormTypePrinter)
                Return Print_name
            Case "PN-96-001" 'สพจ.นราธิวาส PN-96-001 17-10-31
                Print_name = Call_SitePrints_PN_96_001(sendFormTypePrinter)
                Return Print_name
            Case "PN-91-001" 'สพจ.สตูล PN-91-001 17-10-31
                Print_name = Call_SitePrints_PN_91_001(sendFormTypePrinter)
                Return Print_name
            Case "PN-48-001" 'สพจ.นครพนม PN_48_001 17-10-31
                Print_name = Call_SitePrints_PN_48_001(sendFormTypePrinter)
                Return Print_name
            Case "PN-51-001" 'สพจ.ลำพูน PN-51-001 02-01-2018
                Print_name = Call_SitePrints_PN_51_001(sendFormTypePrinter)
                Return Print_name
        End Select
    End Function
#End Region

#Region "   Code Print Select Page   "
    'code select print ใช้สำหรับเลือกหน้า print เอาหน้าเดียว
    Public Shared Sub selectPrintForm_(ByVal rpt_ As Object, ByVal firstpage_ As String, ByVal lastpage_ As String)
        'rpt_.txtCheckSelectFormPrint.value = ""
        rpt_.Document.Printer.PrinterSettings.PrintRange = System.Drawing.Printing.PrintRange.SomePages 'set page เพื่อ print หน้าเดียว
        rpt_.Document.Printer.PrinterSettings.FromPage = CInt(firstpage_) 'first page
        rpt_.Document.Printer.PrinterSettings.ToPage = CInt(lastpage_) 'rpt_.Document.Pages.Count 'last page
        'rpt_.txtCheckSelectFormPrint.value = "1"
        rpt_.Document.Printer.PrinterSettings()
    End Sub
    'code select print ใช้สำหรับเลือกหน้า print เอาตั้งแต่หน้าที่เลือก ถึง หน้าสุดท้าย ไม่ได้ใช้
    Public Shared Sub selectPrint_FormPageToPage_(ByVal rpt_ As Object, ByVal firstpage_ As String, ByVal lastpage_ As String)
        If firstpage_.Trim = "" Or lastpage_.Trim = "" Then
            'Return False
        Else
            rpt_.txtCheckSelectFormPrint.value = ""
            rpt_.Document.Printer.PrinterSettings.FromPage = CInt(firstpage_) 'first page
            rpt_.Document.Printer.PrinterSettings.ToPage = CInt(lastpage_) 'rpt_.Document.Pages.Count 'last page
            rpt_.txtCheckSelectFormPrint.value = "1"
            rpt_.Document.Printer.PrinterSettings()
            'Return True txtCheckSelectFormPrint
        End If
    End Sub
#End Region

#Region "   Site ST-001 Config Printer หนังสือรับรอง-คำขอ   "

#Region "   หนังสือรับรอง    "
    'หนังสือรับรอง ST-001
    Public Shared Function Call_SitePrints_ST_001(ByVal Form_ID As String, ByVal send_Aprint As String, ByVal send_Dprint As String) As String
        Dim SendPrinterName As String
        Select Case Form_ID
            Case "FORM1"
                Select Case send_Aprint
                    Case "A1"
                        SendPrinterName = ConfigurationManager.AppSettings("ST_001_01Edi_FORM1").ToString()
                    Case "A2"
                        SendPrinterName = ConfigurationManager.AppSettings("ST_001_02Edi_FORM1").ToString()
                    Case Else
                        'check เพื่อ 
                        SendPrinterName = ConfigurationManager.AppSettings("ST_001_01Edi_FORM1").ToString()
                End Select
                Return SendPrinterName
            Case "FORM1_1"
                Select Case send_Aprint
                    Case "A1"
                        SendPrinterName = ConfigurationManager.AppSettings("ST_001_01Edi_FORM1_1").ToString()
                    Case "A2"
                        SendPrinterName = ConfigurationManager.AppSettings("ST_001_02Edi_FORM1_1").ToString()
                    Case Else
                        'check เพื่อ 
                        SendPrinterName = ConfigurationManager.AppSettings("ST_001_01Edi_FORM1_1").ToString()
                End Select
                Return SendPrinterName
            Case "FORM1_2"
                Select Case send_Aprint
                    Case "A1"
                        SendPrinterName = ConfigurationManager.AppSettings("ST_001_01Edi_FORM1_2").ToString()
                    Case "A2"
                        SendPrinterName = ConfigurationManager.AppSettings("ST_001_02Edi_FORM1_2").ToString()
                    Case Else
                        'check เพื่อ 
                        SendPrinterName = ConfigurationManager.AppSettings("ST_001_01Edi_FORM1_2").ToString()
                End Select
                Return SendPrinterName
            Case "FORM1_3"
                Select Case send_Aprint
                    Case "A1"
                        SendPrinterName = ConfigurationManager.AppSettings("ST_001_01Edi_FORM1_3").ToString()
                    Case "A2"
                        SendPrinterName = ConfigurationManager.AppSettings("ST_001_02Edi_FORM1_3").ToString()
                    Case Else
                        'check เพื่อ 
                        SendPrinterName = ConfigurationManager.AppSettings("ST_001_01Edi_FORM1_3").ToString()
                End Select
                Return SendPrinterName
            Case "FORM1_4"
                Select Case send_Aprint
                    Case "A1"
                        SendPrinterName = ConfigurationManager.AppSettings("ST_001_01Edi_FORM1_4").ToString()
                    Case "A2"
                        SendPrinterName = ConfigurationManager.AppSettings("ST_001_02Edi_FORM1_4").ToString()
                    Case Else
                        'check เพื่อ 
                        SendPrinterName = ConfigurationManager.AppSettings("ST_001_01Edi_FORM1_4").ToString()
                End Select
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
            Case "FORM2_5"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001Edi_FORM2_5").ToString()
                Return SendPrinterName
            Case "FORM2_6"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001Edi_FORM2_6").ToString()
                Return SendPrinterName
            Case "FORM3"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001Edi_FORM3").ToString()
                Return SendPrinterName
            Case "FORM3_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001Edi_FORM3_1").ToString()
                Return SendPrinterName
                'by rut D new
                'FORM44_4	ฟอร์ม ดี (ATIGA) ใหม่ (7 ประเทศ)
                'FORM44_44	ฟอร์ม ดี (ATIGA) Attach Sheet รถยนต์ ใหม่ (7 ประเทศ)
                'FORM44_41	ฟอร์ม ดี AICO ใหม่ (7 ประเทศ)
                'FORM441_4	ฟอร์ม ดี AICO Attach Sheet รถยนต์ ใหม่ (7 ประเทศ)
            Case "FORM44_4", "FORM44_44", "FORM44_41", "FORM441_4", "FORM44_01", "FORM44_02"
                Select Case send_Dprint
                    Case "D1"
                        SendPrinterName = ConfigurationManager.AppSettings("ST_001_01Edi_FORM44ALL").ToString()
                    Case "D2"
                        SendPrinterName = ConfigurationManager.AppSettings("ST_001_01Edi_FORM44ALL").ToString()
                    Case Else
                        SendPrinterName = ConfigurationManager.AppSettings("ST_001_01Edi_FORM44ALL").ToString()
                End Select
                Return SendPrinterName
            Case "FORM4"
                Select Case send_Dprint
                    Case "D1"
                        SendPrinterName = ConfigurationManager.AppSettings("ST_001_01Edi_FORM4").ToString()
                    Case "D2"
                        SendPrinterName = ConfigurationManager.AppSettings("ST_001_02Edi_FORM4").ToString()
                    Case Else
                        SendPrinterName = ConfigurationManager.AppSettings("ST_001_01Edi_FORM4").ToString()
                End Select
                Return SendPrinterName
            Case "FORM44"
                Select Case send_Dprint
                    Case "D1"
                        SendPrinterName = ConfigurationManager.AppSettings("ST_001_01Edi_FORM44").ToString()
                    Case "D2"
                        SendPrinterName = ConfigurationManager.AppSettings("ST_001_02Edi_FORM44").ToString()
                    Case Else
                        SendPrinterName = ConfigurationManager.AppSettings("ST_001_01Edi_FORM44").ToString()
                End Select
                Return SendPrinterName
            Case "FORM4_1"
                Select Case send_Dprint
                    Case "D1"
                        SendPrinterName = ConfigurationManager.AppSettings("ST_001_01Edi_FORM4_1").ToString()
                    Case "D2"
                        SendPrinterName = ConfigurationManager.AppSettings("ST_001_02Edi_FORM4_1").ToString()
                    Case Else
                        SendPrinterName = ConfigurationManager.AppSettings("ST_001_01Edi_FORM4_1").ToString()
                End Select
                Return SendPrinterName
            Case "FORM441"
                Select Case send_Dprint
                    Case "D1"
                        SendPrinterName = ConfigurationManager.AppSettings("ST_001_01Edi_FORM441").ToString()
                    Case "D2"
                        SendPrinterName = ConfigurationManager.AppSettings("ST_001_02Edi_FORM441").ToString()
                    Case Else
                        SendPrinterName = ConfigurationManager.AppSettings("ST_001_01Edi_FORM441").ToString()
                End Select
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
            Case "FORM4_6", "FORM4_61"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001Edi_FORM4_6").ToString()
                Return SendPrinterName
            Case "FORM4_8", "FORM4_81"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001Edi_FORM4_8").ToString()
                Return SendPrinterName
            Case "FORM4_9"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001Edi_FORM4_9").ToString()
                Return SendPrinterName
            Case "FORM4_91", "FORM4_911"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001Edi_FORM4_91").ToString()
                Return SendPrinterName
            Case "FORM5"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001Edi_FORM5").ToString()
                Return SendPrinterName
            Case "FORM5_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001Edi_FORM5_1").ToString()
                Return SendPrinterName

                ''ByTine 14-10-2558 ฟอร์มไทย-ชิลี FORm5_2 
            Case "FORM5_2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001Edi_FORM5_2").ToString()
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
                Select Case send_Aprint
                    Case "A1"
                        SendPrinterName = ConfigurationManager.AppSettings("ST_001_01Edi_FORMRussia").ToString()
                    Case "A2"
                        SendPrinterName = ConfigurationManager.AppSettings("ST_001_02Edi_FORMRussia").ToString()
                    Case Else
                        'check เพื่อ 
                        SendPrinterName = ConfigurationManager.AppSettings("ST_001_01Edi_FORMRussia").ToString()
                End Select
                Return SendPrinterName
        End Select
    End Function

    'DS ST-001
    Public Shared Function Call_SitePrints_ST_001DS(ByVal Form_ID As String, ByVal send_Aprint As String, ByVal send_Dprint As String) As String
        Dim SendPrinterName As String
        Select Case Form_ID
            Case "FORM1"
                Select Case send_Aprint
                    Case "A1"
                        SendPrinterName = ConfigurationManager.AppSettings("ST_001_01Edi_FORM1").ToString()
                    Case "A2"
                        SendPrinterName = ConfigurationManager.AppSettings("ST_001_02Edi_FORM1").ToString()
                    Case Else
                        'check เพื่อ 
                        SendPrinterName = ConfigurationManager.AppSettings("ST_001_01Edi_FORM1").ToString()
                End Select
                Return SendPrinterName
            Case "FORM1_1"
                Select Case send_Aprint
                    Case "A1"
                        SendPrinterName = ConfigurationManager.AppSettings("ST_001_01Edi_FORM1_1").ToString()
                    Case "A2"
                        SendPrinterName = ConfigurationManager.AppSettings("ST_001_02Edi_FORM1_1").ToString()
                    Case Else
                        'check เพื่อ 
                        SendPrinterName = ConfigurationManager.AppSettings("ST_001_01Edi_FORM1_1").ToString()
                End Select
                Return SendPrinterName
            Case "FORM1_2"
                Select Case send_Aprint
                    Case "A1"
                        SendPrinterName = ConfigurationManager.AppSettings("ST_001_01Edi_FORM1_2").ToString()
                    Case "A2"
                        SendPrinterName = ConfigurationManager.AppSettings("ST_001_02Edi_FORM1_2").ToString()
                    Case Else
                        'check เพื่อ 
                        SendPrinterName = ConfigurationManager.AppSettings("ST_001_01Edi_FORM1_2").ToString()
                End Select
                Return SendPrinterName
            Case "FORM1_3"
                Select Case send_Aprint
                    Case "A1"
                        SendPrinterName = ConfigurationManager.AppSettings("ST_001_01Edi_FORM1_3").ToString()
                    Case "A2"
                        SendPrinterName = ConfigurationManager.AppSettings("ST_001_02Edi_FORM1_3").ToString()
                    Case Else
                        'check เพื่อ 
                        SendPrinterName = ConfigurationManager.AppSettings("ST_001_01Edi_FORM1_3").ToString()
                End Select
                Return SendPrinterName
            Case "FORM1_4"
                Select Case send_Aprint
                    Case "A1"
                        SendPrinterName = ConfigurationManager.AppSettings("ST_001_01Edi_FORM1_4").ToString()
                    Case "A2"
                        SendPrinterName = ConfigurationManager.AppSettings("ST_001_02Edi_FORM1_4").ToString()
                    Case Else
                        'check เพื่อ 
                        SendPrinterName = ConfigurationManager.AppSettings("ST_001_01Edi_FORM1_4").ToString()
                End Select
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
            Case "FORM2_5"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001Edi_FORM2_5").ToString()
                Return SendPrinterName
            Case "FORM2_6"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001Edi_FORM2_6").ToString()
                Return SendPrinterName
            Case "FORM3"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001Edi_FORM3").ToString()
                Return SendPrinterName
            Case "FORM3_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001Edi_FORM3_1").ToString()
                Return SendPrinterName
                'by rut D new
                'FORM44_4	ฟอร์ม ดี (ATIGA) ใหม่ (7 ประเทศ)
                'FORM44_44	ฟอร์ม ดี (ATIGA) Attach Sheet รถยนต์ ใหม่ (7 ประเทศ)
                'FORM44_41	ฟอร์ม ดี AICO ใหม่ (7 ประเทศ)
                'FORM441_4	ฟอร์ม ดี AICO Attach Sheet รถยนต์ ใหม่ (7 ประเทศ)
            Case "FORM44_4", "FORM44_44", "FORM44_41", "FORM441_4", "FORM44_01", "FORM44_02"
                Select Case send_Dprint
                    Case "D1"
                        SendPrinterName = ConfigurationManager.AppSettings("ST_001_01DSEdi_FORM44ALL").ToString()
                    Case "D2"
                        SendPrinterName = ConfigurationManager.AppSettings("ST_001_01DSEdi_FORM44ALL").ToString()
                    Case Else
                        SendPrinterName = ConfigurationManager.AppSettings("ST_001_01DSEdi_FORM44ALL").ToString()
                End Select
                Return SendPrinterName
            Case "FORM4"
                Select Case send_Dprint
                    Case "D1"
                        SendPrinterName = ConfigurationManager.AppSettings("ST_001_01DSEdi_FORM4").ToString()
                    Case "D2"
                        SendPrinterName = ConfigurationManager.AppSettings("ST_001_01DSEdi_FORM4").ToString()
                    Case Else
                        SendPrinterName = ConfigurationManager.AppSettings("ST_001_01DSEdi_FORM4").ToString()
                End Select
                Return SendPrinterName
            Case "FORM44"
                Select Case send_Dprint
                    Case "D1"
                        SendPrinterName = ConfigurationManager.AppSettings("ST_001_01DSEdi_FORM4").ToString()
                    Case "D2"
                        SendPrinterName = ConfigurationManager.AppSettings("ST_001_01DSEdi_FORM4").ToString()
                    Case Else
                        SendPrinterName = ConfigurationManager.AppSettings("ST_001_01DSEdi_FORM4").ToString()
                End Select
                Return SendPrinterName
            Case "FORM4_1"
                Select Case send_Dprint
                    Case "D1"
                        SendPrinterName = ConfigurationManager.AppSettings("ST_001_01DSEdi_FORM4").ToString()
                    Case "D2"
                        SendPrinterName = ConfigurationManager.AppSettings("ST_001_01DSEdi_FORM4").ToString()
                    Case Else
                        SendPrinterName = ConfigurationManager.AppSettings("ST_001_01DSEdi_FORM4").ToString()
                End Select
                Return SendPrinterName
            Case "FORM441"
                Select Case send_Dprint
                    Case "D1"
                        SendPrinterName = ConfigurationManager.AppSettings("ST_001_01DSEdi_FORM4").ToString()
                    Case "D2"
                        SendPrinterName = ConfigurationManager.AppSettings("ST_001_01DSEdi_FORM4").ToString()
                    Case Else
                        SendPrinterName = ConfigurationManager.AppSettings("ST_001_01DSEdi_FORM4").ToString()
                End Select
                Return SendPrinterName

            Case "FORM4_2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001Edi_FORM4_2").ToString()
                Return SendPrinterName
            Case "FORM4_3"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001Edi_FORM4_3").ToString()
                Return SendPrinterName
            Case "FORM4_4"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001DSEdi_FORM4_4").ToString()
                Return SendPrinterName
            Case "FORM4_5"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001Edi_FORM4_5").ToString()
                Return SendPrinterName
            Case "FORM4_6", "FORM4_61"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001Edi_FORM4_6").ToString()
                Return SendPrinterName
            Case "FORM4_8", "FORM4_81"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001Edi_FORM4_8").ToString()
                Return SendPrinterName
            Case "FORM4_9"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001DSEdi_FORM4_9").ToString()
                Return SendPrinterName
            Case "FORM4_91", "FORM4_911"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001Edi_FORM4_91").ToString()
                Return SendPrinterName
            Case "FORM5"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001Edi_FORM5").ToString()
                Return SendPrinterName
            Case "FORM5_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001Edi_FORM5_1").ToString()
                Return SendPrinterName

                ''ByTine 14-10-2558 ฟอร์มไทย-ชิลี FORm5_2 
            Case "FORM5_2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001Edi_FORM5_2").ToString()
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
                Select Case send_Aprint
                    Case "A1"
                        SendPrinterName = ConfigurationManager.AppSettings("ST_001_01Edi_FORMRussia").ToString()
                    Case "A2"
                        SendPrinterName = ConfigurationManager.AppSettings("ST_001_02Edi_FORMRussia").ToString()
                    Case Else
                        'check เพื่อ 
                        SendPrinterName = ConfigurationManager.AppSettings("ST_001_01Edi_FORMRussia").ToString()
                End Select
                Return SendPrinterName
        End Select
    End Function
#End Region

#Region "   test form   "
    Public Shared Function Call_SitePrintsTest_ST_001(ByVal Form_ID As String) As String
        Dim SendPrinterName As String
        Select Case Form_ID
            Case "FORM1"
                SendPrinterName = ConfigurationManager.AppSettings("TestST_001Edi_FORM1").ToString()
                Return SendPrinterName
            Case "FORM1_1"
                SendPrinterName = ConfigurationManager.AppSettings("TestST_001Edi_FORM1_1").ToString()
                Return SendPrinterName
            Case "FORM1_2"
                SendPrinterName = ConfigurationManager.AppSettings("TestST_001Edi_FORM1_2").ToString()
                Return SendPrinterName
            Case "FORM1_3"
                SendPrinterName = ConfigurationManager.AppSettings("TestST_001Edi_FORM1_3").ToString()
                Return SendPrinterName
            Case "FORM1_4"
                SendPrinterName = ConfigurationManager.AppSettings("TestST_001Edi_FORM1_4").ToString()
                Return SendPrinterName
            Case "FORM2"
                SendPrinterName = ConfigurationManager.AppSettings("TestST_001Edi_FORM2").ToString()
                Return SendPrinterName
            Case "FORM2_1"
                SendPrinterName = ConfigurationManager.AppSettings("TestST_001Edi_FORM2_1").ToString()
                Return SendPrinterName
            Case "FORM2_2"
                SendPrinterName = ConfigurationManager.AppSettings("TestST_001Edi_FORM2_2").ToString()
                Return SendPrinterName
            Case "FORM2_3"
                SendPrinterName = ConfigurationManager.AppSettings("TestST_001Edi_FORM2_3").ToString()
                Return SendPrinterName
            Case "FORM2_4"
                SendPrinterName = ConfigurationManager.AppSettings("TestST_001Edi_FORM2_4").ToString()
                Return SendPrinterName
            Case "FORM3"
                SendPrinterName = ConfigurationManager.AppSettings("TestST_001Edi_FORM3").ToString()
                Return SendPrinterName
            Case "FORM3_1"
                SendPrinterName = ConfigurationManager.AppSettings("TestST_001Edi_FORM3_1").ToString()
                Return SendPrinterName
            Case "FORM4"
                SendPrinterName = ConfigurationManager.AppSettings("TestST_001Edi_FORM4").ToString()
                Return SendPrinterName
            Case "FORM4_1"
                SendPrinterName = ConfigurationManager.AppSettings("TestST_001Edi_FORM4_1").ToString()
                Return SendPrinterName
            Case "FORM4_2"
                SendPrinterName = ConfigurationManager.AppSettings("TestST_001Edi_FORM4_2").ToString()
                Return SendPrinterName
            Case "FORM4_3"
                SendPrinterName = ConfigurationManager.AppSettings("TestST_001Edi_FORM4_3").ToString()
                Return SendPrinterName
            Case "FORM4_4"
                SendPrinterName = ConfigurationManager.AppSettings("TestST_001Edi_FORM4_4").ToString()
                Return SendPrinterName
            Case "FORM4_5"
                SendPrinterName = ConfigurationManager.AppSettings("TestST_001Edi_FORM4_5").ToString()
                Return SendPrinterName
            Case "FORM4_6"
                SendPrinterName = ConfigurationManager.AppSettings("TestST_001Edi_FORM4_6").ToString()
                Return SendPrinterName
            Case "FORM4_8"
                SendPrinterName = ConfigurationManager.AppSettings("TestST_001Edi_FORM4_8").ToString()
                Return SendPrinterName
            Case "FORM4_9"
                SendPrinterName = ConfigurationManager.AppSettings("TestST_001Edi_FORM4_9").ToString()
                Return SendPrinterName
            Case "FORM4_91", "FORM4_911"
                SendPrinterName = ConfigurationManager.AppSettings("TestST_001Edi_FORM4_91").ToString()
                Return SendPrinterName
            Case "FORM5"
                SendPrinterName = ConfigurationManager.AppSettings("TestST_001Edi_FORM5").ToString()
                Return SendPrinterName
            Case "FORM5_1"
                SendPrinterName = ConfigurationManager.AppSettings("TestST_001Edi_FORM5_1").ToString()
                Return SendPrinterName

                ''ByTine 14-10-2558 ฟอร์มไทย-ชิลี FORm5_2 
            Case "FORM5_2"
                SendPrinterName = ConfigurationManager.AppSettings("TestST_001Edi_FORM5_2").ToString()
                Return SendPrinterName

            Case "FORM6"
                SendPrinterName = ConfigurationManager.AppSettings("TestST_001Edi_FORM6").ToString()
                Return SendPrinterName
            Case "FORM7"
                SendPrinterName = ConfigurationManager.AppSettings("TestST_001Edi_FORM7").ToString()
                Return SendPrinterName
            Case "FORM8"
                SendPrinterName = ConfigurationManager.AppSettings("TestST_001Edi_FORM8").ToString()
                Return SendPrinterName
            Case "FORM9"
                SendPrinterName = ConfigurationManager.AppSettings("TestST_001Edi_FORM9").ToString()
                Return SendPrinterName
            Case "FORMRussia"
                SendPrinterName = ConfigurationManager.AppSettings("TestST_001Edi_FORMRussia").ToString()
                Return SendPrinterName
        End Select
    End Function
#End Region

#Region "   test   "
    Public Shared Function Call_SiteReceiptTest_ST_001_01(ByVal Form_ID As String) As String
        Dim SendPrinterName As String
        Select Case Form_ID
            Case "FORM1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_01TestEdi_receiptFORM1").ToString()
                Return SendPrinterName
            Case "FORM1_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_01TestEdi_receiptFORM1_1").ToString()
                Return SendPrinterName
            Case "FORM1_2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_01TestEdi_receiptFORM1_2").ToString()
                Return SendPrinterName
            Case "FORM1_3"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_01TestEdi_receiptFORM1_3").ToString()
                Return SendPrinterName
            Case "FORM1_4"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_01TestEdi_receiptFORM1_4").ToString()
                Return SendPrinterName
            Case "FORM2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_01TestEdi_receiptFORM2").ToString()
                Return SendPrinterName
            Case "FORM2_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_01TestEdi_receiptFORM2_1").ToString()
                Return SendPrinterName
            Case "FORM2_2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_01TestEdi_receiptFORM2_2").ToString()
                Return SendPrinterName
            Case "FORM2_3"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_01TestEdi_receiptFORM2_3").ToString()
                Return SendPrinterName
            Case "FORM2_4"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_01TestEdi_receiptFORM2_4").ToString()
                Return SendPrinterName
            Case "FORM3"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_01TestEdi_receiptFORM3").ToString()
                Return SendPrinterName
            Case "FORM3_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_01TestEdi_receiptFORM3_1").ToString()
                Return SendPrinterName
                'by rut D new
                'FORM44_4	ฟอร์ม ดี (ATIGA) ใหม่ (7 ประเทศ)
                'FORM44_44	ฟอร์ม ดี (ATIGA) Attach Sheet รถยนต์ ใหม่ (7 ประเทศ)
                'FORM44_41	ฟอร์ม ดี AICO ใหม่ (7 ประเทศ)
                'FORM441_4	ฟอร์ม ดี AICO Attach Sheet รถยนต์ ใหม่ (7 ประเทศ)
            Case "FORM44_4", "FORM44_44", "FORM44_41", "FORM441_4", "FORM44_01", "FORM44_02"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_01TestEdi_receiptFORM44_ALL").ToString()
                Return SendPrinterName
                '=============================
            Case "FORM4"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_01TestEdi_receiptFORM4").ToString()
                Return SendPrinterName
            Case "FORM4_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_01TestEdi_receiptFORM4_1").ToString()
                Return SendPrinterName
            Case "FORM4_2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_01TestEdi_receiptFORM4_2").ToString()
                Return SendPrinterName
            Case "FORM4_3"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_01TestEdi_receiptFORM4_3").ToString()
                Return SendPrinterName
            Case "FORM4_4"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_01TestEdi_receiptFORM4_4").ToString()
                Return SendPrinterName
            Case "FORM4_5"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_01TestEdi_receiptFORM4_5").ToString()
                Return SendPrinterName
            Case "FORM4_6"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_01TestEdi_receiptFORM4_6").ToString()
                Return SendPrinterName
            Case "FORM4_8"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_01TestEdi_receiptFORM4_8").ToString()
                Return SendPrinterName
            Case "FORM4_9"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_01TestEdi_receiptFORM4_9").ToString()
                Return SendPrinterName
            Case "FORM4_91", "FORM4_911"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_01TestEdi_receiptFORM4_91").ToString()
                Return SendPrinterName
            Case "FORM5"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_01TestEdi_receiptFORM5").ToString()
                Return SendPrinterName
            Case "FORM5_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_01TestEdi_receiptFORM5_1").ToString()
                Return SendPrinterName

                ''ByTine 14-10-2558 ฟอร์มไทย-ชิลี FORm5_2 
            Case "FORM5_2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_01TestEdi_receiptFORM5_2").ToString()
                Return SendPrinterName

            Case "FORM6"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_01TestEdi_receiptFORM6").ToString()
                Return SendPrinterName
            Case "FORM7"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_01TestEdi_receiptFORM7").ToString()
                Return SendPrinterName
            Case "FORM8"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_01TestEdi_receiptFORM8").ToString()
                Return SendPrinterName
            Case "FORM9"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_01TestEdi_receiptFORM9").ToString()
                Return SendPrinterName
            Case "FORMRussia"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_01TestEdi_receiptFORMRussia").ToString()
                Return SendPrinterName
        End Select
    End Function
#End Region


#Region "   คำขอ เครื่อง 1   "
    'เครื่องพิมพ์คำขอ คต. ST-001
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
            Case "FORM2_5"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_01Edi_receiptFORM2_5").ToString()
                Return SendPrinterName
            Case "FORM2_6"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_01Edi_receiptFORM2_6").ToString()
                Return SendPrinterName
            Case "FORM3"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_01Edi_receiptFORM3").ToString()
                Return SendPrinterName
            Case "FORM3_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_01Edi_receiptFORM3_1").ToString()
                Return SendPrinterName
                'by rut D new
                'FORM44_4	ฟอร์ม ดี (ATIGA) ใหม่ (7 ประเทศ)
                'FORM44_44	ฟอร์ม ดี (ATIGA) Attach Sheet รถยนต์ ใหม่ (7 ประเทศ)
                'FORM44_41	ฟอร์ม ดี AICO ใหม่ (7 ประเทศ)
                'FORM441_4	ฟอร์ม ดี AICO Attach Sheet รถยนต์ ใหม่ (7 ประเทศ)
            Case "FORM44_4", "FORM44_44", "FORM44_41", "FORM441_4", "FORM44_01", "FORM44_02"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_01Edi_receiptFORM4").ToString()
                Return SendPrinterName
            Case "FORM4", "FORM44"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_01Edi_receiptFORM4").ToString()
                Return SendPrinterName
            Case "FORM4_1", "FORM441"
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
            Case "FORM4_6", "FORM4_61"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_01Edi_receiptFORM4_6").ToString()
                Return SendPrinterName
            Case "FORM4_8", "FORM4_81"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_01Edi_receiptFORM4_8").ToString()
                Return SendPrinterName
            Case "FORM4_9"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_01Edi_receiptFORM4_9").ToString()
                Return SendPrinterName
            Case "FORM4_91", "FORM4_911"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_01Edi_receiptFORM4_91").ToString()
                Return SendPrinterName
            Case "FORM5"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_01Edi_receiptFORM5").ToString()
                Return SendPrinterName
            Case "FORM5_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_01Edi_receiptFORM5_1").ToString()
                Return SendPrinterName

                ''ByTine 14-10-2558 ฟอร์มไทย-ชิลี FORm5_2 
            Case "FORM5_2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_01Edi_receiptFORM5_2").ToString()
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

#End Region

#Region "   คำขอ เครื่อง 2   "
    'เครื่องพิมพ์คำขอ คต. ST-001
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
            Case "FORM2_5"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_02Edi_receiptFORM2_5").ToString()
                Return SendPrinterName
            Case "FORM2_6"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_02Edi_receiptFORM2_6").ToString()
                Return SendPrinterName
            Case "FORM3"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_02Edi_receiptFORM3").ToString()
                Return SendPrinterName
            Case "FORM3_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_02Edi_receiptFORM3_1").ToString()
                Return SendPrinterName
                'by rut D new
                'FORM44_4	ฟอร์ม ดี (ATIGA) ใหม่ (7 ประเทศ)
                'FORM44_44	ฟอร์ม ดี (ATIGA) Attach Sheet รถยนต์ ใหม่ (7 ประเทศ)
                'FORM44_41	ฟอร์ม ดี AICO ใหม่ (7 ประเทศ)
                'FORM441_4	ฟอร์ม ดี AICO Attach Sheet รถยนต์ ใหม่ (7 ประเทศ)
            Case "FORM44_4", "FORM44_44", "FORM44_41", "FORM441_4", "FORM44_01", "FORM44_02"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_02Edi_receiptFORM4").ToString()
                Return SendPrinterName
            Case "FORM4"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_02Edi_receiptFORM4").ToString()
                Return SendPrinterName
            Case "FORM44"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_03Edi_receiptFORM4").ToString()
                Return SendPrinterName
            Case "FORM441"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_03Edi_receiptFORM4").ToString()
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
            Case "FORM4_6", "FORM4_61"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_02Edi_receiptFORM4_6").ToString()
                Return SendPrinterName
            Case "FORM4_8", "FORM4_81"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_02Edi_receiptFORM4_8").ToString()
                Return SendPrinterName
            Case "FORM4_9"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_02Edi_receiptFORM4_9").ToString()
                Return SendPrinterName
            Case "FORM4_91", "FORM4_911"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_02Edi_receiptFORM4_91").ToString()
                Return SendPrinterName
            Case "FORM5"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_02Edi_receiptFORM5").ToString()
                Return SendPrinterName
            Case "FORM5_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_02Edi_receiptFORM5_1").ToString()
                Return SendPrinterName

                ''ByTine 14-10-2558 ฟอร์มไทย-ชิลี FORm5_2 
            Case "FORM5_2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_02Edi_receiptFORM5_2").ToString()
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
#End Region

#End Region

#Region "   Site ST-002 กรมการค้าต่างประเทศ (ท่าเรือ) คลองเตย Config Printer หนังสือรับรอง-คำขอ    "

#Region "   หนังสือรับรอง   "
    'set print ST-002 กรมการค้าต่างประเทศ (ท่าเรือ) คลองเตย a,d,e,jtepa 01
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
            Case "FORM2_5"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002Edi_FORM2_5").ToString()
                Return SendPrinterName
            Case "FORM2_6"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002Edi_FORM2_6").ToString()
                Return SendPrinterName
            Case "FORM3"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002Edi_FORM3").ToString()
                Return SendPrinterName
            Case "FORM3_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002Edi_FORM3_1").ToString()
                Return SendPrinterName
                'by rut D new
                'FORM44_4	ฟอร์ม ดี (ATIGA) ใหม่ (7 ประเทศ)
                'FORM44_44	ฟอร์ม ดี (ATIGA) Attach Sheet รถยนต์ ใหม่ (7 ประเทศ)
                'FORM44_41	ฟอร์ม ดี AICO ใหม่ (7 ประเทศ)
                'FORM441_4	ฟอร์ม ดี AICO Attach Sheet รถยนต์ ใหม่ (7 ประเทศ)
            Case "FORM44_4", "FORM44_44", "FORM44_41", "FORM441_4", "FORM44_01", "FORM44_02"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002Edi_FORM4").ToString()
                Return SendPrinterName

            Case "FORM4"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_02Edi_FORM4").ToString()
                Return SendPrinterName
            Case "FORM44"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_02Edi_FORM4").ToString()
                Return SendPrinterName
            Case "FORM441"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_02Edi_FORM4").ToString()
                Return SendPrinterName
            Case "FORM4_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_02Edi_FORM4").ToString()
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
            Case "FORM4_6", "FORM4_61"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002Edi_FORM4_6").ToString()
                Return SendPrinterName
            Case "FORM4_8", "FORM4_81"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002Edi_FORM4_8").ToString()
                Return SendPrinterName
            Case "FORM4_9"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002Edi_FORM4_9").ToString()
                Return SendPrinterName
            Case "FORM4_91", "FORM4_911"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002Edi_FORM4_91").ToString()
                Return SendPrinterName
            Case "FORM5"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002Edi_FORM5").ToString()
                Return SendPrinterName
            Case "FORM5_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002Edi_FORM5_1").ToString()
                Return SendPrinterName

                ''ByTine 14-10-2558 ฟอร์มไทย-ชิลี FORm5_2 
            Case "FORM5_2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002Edi_FORM5_2").ToString()
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
    'a,d,e,jtepa 02
    Public Shared Function Call_SitePrints_ST_002_V02(ByVal Form_ID As String) As String
        Dim SendPrinterName As String
        Select Case Form_ID
            Case "FORM1" '
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_02Edi_FORM1").ToString()
                Return SendPrinterName
            Case "FORM1_1" '
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_02Edi_FORM1_1").ToString()
                Return SendPrinterName
            Case "FORM1_2" '
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_02Edi_FORM1_2").ToString()
                Return SendPrinterName
            Case "FORM1_3" '
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_02Edi_FORM1_3").ToString()
                Return SendPrinterName
            Case "FORM1_4" '
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_02Edi_FORM1_4").ToString()
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
            Case "FORM2_5"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002Edi_FORM2_5").ToString()
                Return SendPrinterName
            Case "FORM2_6"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002Edi_FORM2_6").ToString()
                Return SendPrinterName
            Case "FORM3"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002Edi_FORM3").ToString()
                Return SendPrinterName
            Case "FORM3_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002Edi_FORM3_1").ToString()
                Return SendPrinterName
                'by rut D new
                'FORM44_4	ฟอร์ม ดี (ATIGA) ใหม่ (7 ประเทศ)
                'FORM44_44	ฟอร์ม ดี (ATIGA) Attach Sheet รถยนต์ ใหม่ (7 ประเทศ)
                'FORM44_41	ฟอร์ม ดี AICO ใหม่ (7 ประเทศ)
                'FORM441_4	ฟอร์ม ดี AICO Attach Sheet รถยนต์ ใหม่ (7 ประเทศ)
            Case "FORM44_4", "FORM44_44", "FORM44_41", "FORM441_4", "FORM44_01", "FORM44_02"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_02Edi_FORM44All").ToString()
                Return SendPrinterName

            Case "FORM4" '
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_02Edi_FORM4").ToString()
                Return SendPrinterName
            Case "FORM44" '
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_02Edi_FORM4").ToString()
                Return SendPrinterName
            Case "FORM441" '
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_02Edi_FORM4_1").ToString()
                Return SendPrinterName
            Case "FORM4_1" '
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_02Edi_FORM4_1").ToString()
                Return SendPrinterName

            Case "FORM4_2" '
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_02Edi_FORM4_2").ToString()
                Return SendPrinterName
            Case "FORM4_3"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002Edi_FORM4_3").ToString()
                Return SendPrinterName
            Case "FORM4_4"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002Edi_FORM4_4").ToString()
                Return SendPrinterName
            Case "FORM4_5" '
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_02Edi_FORM4_5").ToString()
                Return SendPrinterName
            Case "FORM4_6", "FORM4_61"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002Edi_FORM4_6").ToString()
                Return SendPrinterName
            Case "FORM4_8", "FORM4_81"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002Edi_FORM4_8").ToString()
                Return SendPrinterName
            Case "FORM4_9"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002Edi_FORM4_9").ToString()
                Return SendPrinterName
            Case "FORM4_91", "FORM4_911"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002Edi_FORM4_91").ToString()
                Return SendPrinterName
            Case "FORM5"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002Edi_FORM5").ToString()
                Return SendPrinterName
            Case "FORM5_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002Edi_FORM5_1").ToString()
                Return SendPrinterName

                ''ByTine 14-10-2558 ฟอร์มไทย-ชิลี FORm5_2 
            Case "FORM5_2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002Edi_FORM5_2").ToString()
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


    Public Shared Function Call_SitePrints_ST_002_V03(ByVal Form_ID As String) As String
        Dim SendPrinterName As String
        Select Case Form_ID
            Case "FORM1" '
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_02Edi_FORM1").ToString()
                Return SendPrinterName
            Case "FORM1_1" '
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_02Edi_FORM1_1").ToString()
                Return SendPrinterName
            Case "FORM1_2" '
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_02Edi_FORM1_2").ToString()
                Return SendPrinterName
            Case "FORM1_3" '
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_02Edi_FORM1_3").ToString()
                Return SendPrinterName
            Case "FORM1_4" '
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_02Edi_FORM1_4").ToString()
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
            Case "FORM2_5"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002Edi_FORM2_5").ToString()
                Return SendPrinterName
            Case "FORM2_6"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002Edi_FORM2_6").ToString()
                Return SendPrinterName
            Case "FORM3"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002Edi_FORM3").ToString()
                Return SendPrinterName
            Case "FORM3_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002Edi_FORM3_1").ToString()
                Return SendPrinterName
                'by rut D new
                'FORM44_4	ฟอร์ม ดี (ATIGA) ใหม่ (7 ประเทศ)
                'FORM44_44	ฟอร์ม ดี (ATIGA) Attach Sheet รถยนต์ ใหม่ (7 ประเทศ)
                'FORM44_41	ฟอร์ม ดี AICO ใหม่ (7 ประเทศ)
                'FORM441_4	ฟอร์ม ดี AICO Attach Sheet รถยนต์ ใหม่ (7 ประเทศ)
            Case "FORM44_4", "FORM44_44", "FORM44_41", "FORM441_4", "FORM44_01", "FORM44_02"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_02Edi_FORM44All").ToString()
                Return SendPrinterName

            Case "FORM4" '
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_02Edi_FORM4").ToString()
                Return SendPrinterName
            Case "FORM44" '
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_02Edi_FORM4").ToString()
                Return SendPrinterName
            Case "FORM441" '
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_02Edi_FORM4_1").ToString()
                Return SendPrinterName
            Case "FORM4_1" '
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_02Edi_FORM4_1").ToString()
                Return SendPrinterName

            Case "FORM4_2" '
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_02Edi_FORM4_2").ToString()
                Return SendPrinterName
            Case "FORM4_3"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002Edi_FORM4_3").ToString()
                Return SendPrinterName
            Case "FORM4_4"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002Edi_FORM4_4").ToString()
                Return SendPrinterName
            Case "FORM4_5" '
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_02Edi_FORM4_5").ToString()
                Return SendPrinterName
            Case "FORM4_6", "FORM4_61"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002Edi_FORM4_6").ToString()
                Return SendPrinterName
            Case "FORM4_8", "FORM4_81"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002Edi_FORM4_8").ToString()
                Return SendPrinterName
            Case "FORM4_9"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002Edi_FORM4_9").ToString()
                Return SendPrinterName
            Case "FORM4_91", "FORM4_911"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002Edi_FORM4_91").ToString()
                Return SendPrinterName
            Case "FORM5"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002Edi_FORM5").ToString()
                Return SendPrinterName
            Case "FORM5_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002Edi_FORM5_1").ToString()
                Return SendPrinterName

                ''ByTine 14-10-2558 ฟอร์มไทย-ชิลี FORm5_2 
            Case "FORM5_2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002Edi_FORM5_2").ToString()
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

#Region "   หนังสือรับรอง DS   "
    'set print ST-002 กรมการค้าต่างประเทศ (ท่าเรือ) คลองเตย
    Public Shared Function Call_SitePrints_ST_002DS(ByVal Form_ID As String) As String
        Dim SendPrinterName As String
        Select Case Form_ID
            Case "FORM1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002DSEdi_FORM1").ToString()
                Return SendPrinterName
            Case "FORM1_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002DSEdi_FORM1_1").ToString()
                Return SendPrinterName
            Case "FORM1_2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002DSEdi_FORM1_2").ToString()
                Return SendPrinterName
            Case "FORM1_3"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002DSEdi_FORM1_3").ToString()
                Return SendPrinterName
            Case "FORM1_4"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002DSEdi_FORM1_4").ToString()
                Return SendPrinterName
            Case "FORM2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002DSEdi_FORM2").ToString()
                Return SendPrinterName
            Case "FORM2_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002DSEdi_FORM2_1").ToString()
                Return SendPrinterName
            Case "FORM2_2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002DSEdi_FORM2_2").ToString()
                Return SendPrinterName
            Case "FORM2_3"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002DSEdi_FORM2_3").ToString()
                Return SendPrinterName
            Case "FORM2_4"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002DSEdi_FORM2_4").ToString()
                Return SendPrinterName
            Case "FORM2_5"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002DSEdi_FORM2_5").ToString()
                Return SendPrinterName
            Case "FORM2_6"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002DSEdi_FORM2_6").ToString()
                Return SendPrinterName
            Case "FORM3"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002DSEdi_FORM3").ToString()
                Return SendPrinterName
            Case "FORM3_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002DSEdi_FORM3_1").ToString()
                Return SendPrinterName
                'by rut D new
                'FORM44_4	ฟอร์ม ดี (ATIGA) ใหม่ (7 ประเทศ)
                'FORM44_44	ฟอร์ม ดี (ATIGA) Attach Sheet รถยนต์ ใหม่ (7 ประเทศ)
                'FORM44_41	ฟอร์ม ดี AICO ใหม่ (7 ประเทศ)
                'FORM441_4	ฟอร์ม ดี AICO Attach Sheet รถยนต์ ใหม่ (7 ประเทศ)
            Case "FORM44_4", "FORM44_44", "FORM44_41", "FORM441_4", "FORM44_01", "FORM44_02"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002DSEdi_FORM44ALL").ToString()
                Return SendPrinterName
            Case "FORM4"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002DSEdi_FORM4").ToString()
                Return SendPrinterName
            Case "FORM44"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002DSEdi_FORM4").ToString()
                Return SendPrinterName
            Case "FORM441"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002DSEdi_FORM4_1").ToString()
                Return SendPrinterName
            Case "FORM4_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002DSEdi_FORM4_1").ToString()
                Return SendPrinterName

            Case "FORM4_2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002DSEdi_FORM4_2").ToString()
                Return SendPrinterName
            Case "FORM4_3"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002DSEdi_FORM4_3").ToString()
                Return SendPrinterName
            Case "FORM4_4"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002DSEdi_FORM4_4").ToString()
                Return SendPrinterName
            Case "FORM4_5"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002DSEdi_FORM4_5").ToString()
                Return SendPrinterName
            Case "FORM4_6", "FORM4_61"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002DSEdi_FORM4_6").ToString()
                Return SendPrinterName
            Case "FORM4_8", "FORM4_81"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002DSEdi_FORM4_8").ToString()
                Return SendPrinterName
            Case "FORM4_9"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002DSEdi_FORM4_9").ToString()
                Return SendPrinterName
            Case "FORM4_91", "FORM4_911"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002DSEdi_FORM4_91").ToString()
                Return SendPrinterName
            Case "FORM5"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002DSEdi_FORM5").ToString()
                Return SendPrinterName
            Case "FORM5_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002DSEdi_FORM5_1").ToString()
                Return SendPrinterName

                ''ByTine 14-10-2558 ฟอร์มไทย-ชิลี FORm5_2 
            Case "FORM5_2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002DSEdi_FORM5_2").ToString()
                Return SendPrinterName

            Case "FORM6"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002DSEdi_FORM6").ToString()
                Return SendPrinterName
            Case "FORM7"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002DSEdi_FORM7").ToString()
                Return SendPrinterName
            Case "FORM8"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002DSEdi_FORM8").ToString()
                Return SendPrinterName
            Case "FORM9"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002DSEdi_FORM9").ToString()
                Return SendPrinterName
            Case "FORMRussia"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002DSEdi_FORMRussia").ToString()
                Return SendPrinterName

        End Select
    End Function

    'เครื่องพิมพ์ DS เครื่องที่ 2
    Public Shared Function Call_SitePrints_ST_002DS_V02(ByVal Form_ID As String) As String
        Dim SendPrinterName As String
        Select Case Form_ID
            Case "FORM1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002DSEdi_FORM1").ToString()
                Return SendPrinterName
            Case "FORM1_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002DSEdi_FORM1_1").ToString()
                Return SendPrinterName
            Case "FORM1_2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002DSEdi_FORM1_2").ToString()
                Return SendPrinterName
            Case "FORM1_3"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002DSEdi_FORM1_3").ToString()
                Return SendPrinterName
            Case "FORM1_4"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002DSEdi_FORM1_4").ToString()
                Return SendPrinterName
            Case "FORM2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002DSEdi_FORM2").ToString()
                Return SendPrinterName
            Case "FORM2_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002DSEdi_FORM2_1").ToString()
                Return SendPrinterName
            Case "FORM2_2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002DSEdi_FORM2_2").ToString()
                Return SendPrinterName
            Case "FORM2_3"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002DSEdi_FORM2_3").ToString()
                Return SendPrinterName
            Case "FORM2_4"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002DSEdi_FORM2_4").ToString()
                Return SendPrinterName
            Case "FORM2_5"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002DSEdi_FORM2_5").ToString()
                Return SendPrinterName
            Case "FORM2_6"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002DSEdi_FORM2_6").ToString()
                Return SendPrinterName
            Case "FORM3"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002DSEdi_FORM3").ToString()
                Return SendPrinterName
            Case "FORM3_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002DSEdi_FORM3_1").ToString()
                Return SendPrinterName
                'by rut D new
                'FORM44_4	ฟอร์ม ดี (ATIGA) ใหม่ (7 ประเทศ)
                'FORM44_44	ฟอร์ม ดี (ATIGA) Attach Sheet รถยนต์ ใหม่ (7 ประเทศ)
                'FORM44_41	ฟอร์ม ดี AICO ใหม่ (7 ประเทศ)
                'FORM441_4	ฟอร์ม ดี AICO Attach Sheet รถยนต์ ใหม่ (7 ประเทศ)
            Case "FORM44_4", "FORM44_44", "FORM44_41", "FORM441_4", "FORM44_01", "FORM44_02"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_02DSEdi_FORM44ALL").ToString()
                Return SendPrinterName
            Case "FORM4"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_02DSEdi_FORM4").ToString()
                Return SendPrinterName
            Case "FORM44"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002DSEdi_FORM4").ToString()
                Return SendPrinterName
            Case "FORM441"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002DSEdi_FORM4_1").ToString()
                Return SendPrinterName
            Case "FORM4_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002DSEdi_FORM4_1").ToString()
                Return SendPrinterName

            Case "FORM4_2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002DSEdi_FORM4_2").ToString()
                Return SendPrinterName
            Case "FORM4_3"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002DSEdi_FORM4_3").ToString()
                Return SendPrinterName
            Case "FORM4_4"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002DSEdi_FORM4_4_NO2").ToString()
                Return SendPrinterName
            Case "FORM4_5"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002DSEdi_FORM4_5").ToString()
                Return SendPrinterName
            Case "FORM4_6", "FORM4_61"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002DSEdi_FORM4_6").ToString()
                Return SendPrinterName
            Case "FORM4_8", "FORM4_81"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002DSEdi_FORM4_8").ToString()
                Return SendPrinterName
            Case "FORM4_9"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002DSEdi_FORM4_9_NO2").ToString()
                Return SendPrinterName
            Case "FORM4_91", "FORM4_911"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002DSEdi_FORM4_91").ToString()
                Return SendPrinterName
            Case "FORM5"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002DSEdi_FORM5").ToString()
                Return SendPrinterName
            Case "FORM5_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002DSEdi_FORM5_1").ToString()
                Return SendPrinterName

                ''ByTine 14-10-2558 ฟอร์มไทย-ชิลี FORm5_2 
            Case "FORM5_2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002DSEdi_FORM5_2").ToString()
                Return SendPrinterName

            Case "FORM6"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002DSEdi_FORM6").ToString()
                Return SendPrinterName
            Case "FORM7"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002DSEdi_FORM7").ToString()
                Return SendPrinterName
            Case "FORM8"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002DSEdi_FORM8").ToString()
                Return SendPrinterName
            Case "FORM9"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002DSEdi_FORM9").ToString()
                Return SendPrinterName
            Case "FORMRussia"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002DSEdi_FORMRussia").ToString()
                Return SendPrinterName

        End Select
    End Function

    'เครื่องพิมพ์ DS เครื่องที่ 3
    Public Shared Function Call_SitePrints_ST_002DS_V03(ByVal Form_ID As String) As String
        Dim SendPrinterName As String
        Select Case Form_ID
            Case "FORM1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002DSEdi_FORM1").ToString()
                Return SendPrinterName
            Case "FORM1_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002DSEdi_FORM1_1").ToString()
                Return SendPrinterName
            Case "FORM1_2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002DSEdi_FORM1_2").ToString()
                Return SendPrinterName
            Case "FORM1_3"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002DSEdi_FORM1_3").ToString()
                Return SendPrinterName
            Case "FORM1_4"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002DSEdi_FORM1_4").ToString()
                Return SendPrinterName
            Case "FORM2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002DSEdi_FORM2").ToString()
                Return SendPrinterName
            Case "FORM2_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002DSEdi_FORM2_1").ToString()
                Return SendPrinterName
            Case "FORM2_2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002DSEdi_FORM2_2").ToString()
                Return SendPrinterName
            Case "FORM2_3"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002DSEdi_FORM2_3").ToString()
                Return SendPrinterName
            Case "FORM2_4"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002DSEdi_FORM2_4").ToString()
                Return SendPrinterName
            Case "FORM2_5"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002DSEdi_FORM2_5").ToString()
                Return SendPrinterName
            Case "FORM2_6"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002DSEdi_FORM2_6").ToString()
                Return SendPrinterName
            Case "FORM3"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002DSEdi_FORM3").ToString()
                Return SendPrinterName
            Case "FORM3_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002DSEdi_FORM3_1").ToString()
                Return SendPrinterName
                'by rut D new
                'FORM44_4	ฟอร์ม ดี (ATIGA) ใหม่ (7 ประเทศ)
                'FORM44_44	ฟอร์ม ดี (ATIGA) Attach Sheet รถยนต์ ใหม่ (7 ประเทศ)
                'FORM44_41	ฟอร์ม ดี AICO ใหม่ (7 ประเทศ)
                'FORM441_4	ฟอร์ม ดี AICO Attach Sheet รถยนต์ ใหม่ (7 ประเทศ)
            Case "FORM44_4", "FORM44_44", "FORM44_41", "FORM441_4", "FORM44_01", "FORM44_02"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_02DSEdi_FORM44_No3").ToString()
                Return SendPrinterName
            Case "FORM4"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_02DSEdi_FORM4").ToString()
                Return SendPrinterName
            Case "FORM44"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002DSEdi_FORM4").ToString()
                Return SendPrinterName
            Case "FORM441"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002DSEdi_FORM4_1").ToString()
                Return SendPrinterName
            Case "FORM4_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002DSEdi_FORM4_1").ToString()
                Return SendPrinterName

            Case "FORM4_2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002DSEdi_FORM4_2").ToString()
                Return SendPrinterName
            Case "FORM4_3"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002DSEdi_FORM4_3").ToString()
                Return SendPrinterName
            Case "FORM4_4"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002DSEdi_FORM4_4_NO2").ToString()
                Return SendPrinterName
            Case "FORM4_5"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002DSEdi_FORM4_5").ToString()
                Return SendPrinterName
            Case "FORM4_6", "FORM4_61"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002DSEdi_FORM4_6").ToString()
                Return SendPrinterName
            Case "FORM4_8", "FORM4_81"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002DSEdi_FORM4_8").ToString()
                Return SendPrinterName
            Case "FORM4_9"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002DSEdi_FORM4_9_NO2").ToString()
                Return SendPrinterName
            Case "FORM4_91", "FORM4_911"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002DSEdi_FORM4_91").ToString()
                Return SendPrinterName
            Case "FORM5"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002DSEdi_FORM5").ToString()
                Return SendPrinterName
            Case "FORM5_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002DSEdi_FORM5_1").ToString()
                Return SendPrinterName

                ''ByTine 14-10-2558 ฟอร์มไทย-ชิลี FORm5_2 
            Case "FORM5_2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002DSEdi_FORM5_2").ToString()
                Return SendPrinterName

            Case "FORM6"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002DSEdi_FORM6").ToString()
                Return SendPrinterName
            Case "FORM7"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002DSEdi_FORM7").ToString()
                Return SendPrinterName
            Case "FORM8"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002DSEdi_FORM8").ToString()
                Return SendPrinterName
            Case "FORM9"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002DSEdi_FORM9").ToString()
                Return SendPrinterName
            Case "FORMRussia"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002DSEdi_FORMRussia").ToString()
                Return SendPrinterName

        End Select
    End Function
#End Region
#End Region

#Region "   คำขอ เครื่อง 1   "
    'เครื่องพิมพ์คำขอ คลองเตย ST-002 เครื่อง 1
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
            Case "FORM2_5"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_01Edi_receiptFORM2_5").ToString()
                Return SendPrinterName
            Case "FORM2_6"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_01Edi_receiptFORM2_6").ToString()
                Return SendPrinterName
            Case "FORM3"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_01Edi_receiptFORM3").ToString()
                Return SendPrinterName
            Case "FORM3_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_01Edi_receiptFORM3_1").ToString()
                Return SendPrinterName
                'by rut D new
                'FORM44_4	ฟอร์ม ดี (ATIGA) ใหม่ (7 ประเทศ)
                'FORM44_44	ฟอร์ม ดี (ATIGA) Attach Sheet รถยนต์ ใหม่ (7 ประเทศ)
                'FORM44_41	ฟอร์ม ดี AICO ใหม่ (7 ประเทศ)
                'FORM441_4	ฟอร์ม ดี AICO Attach Sheet รถยนต์ ใหม่ (7 ประเทศ)
            Case "FORM44_4", "FORM44_44", "FORM44_41", "FORM441_4", "FORM44_01", "FORM44_02"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_01Edi_receiptFORM4").ToString()
                Return SendPrinterName
            Case "FORM4"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_01Edi_receiptFORM4").ToString()
                Return SendPrinterName
            Case "FORM44"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_01Edi_receiptFORM4").ToString()
                Return SendPrinterName
            Case "FORM441"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_01Edi_receiptFORM4_1").ToString()
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
            Case "FORM4_6", "FORM4_61"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_01Edi_receiptFORM4_6").ToString()
                Return SendPrinterName
            Case "FORM4_8", "FORM4_81"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_01Edi_receiptFORM4_8").ToString()
                Return SendPrinterName
            Case "FORM4_9"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_01Edi_receiptFORM4_9").ToString()
                Return SendPrinterName
            Case "FORM4_91", "FORM4_911"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_01Edi_receiptFORM4_91").ToString()
                Return SendPrinterName
            Case "FORM5"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_01Edi_receiptFORM5").ToString()
                Return SendPrinterName
            Case "FORM5_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_01Edi_receiptFORM5_1").ToString()
                Return SendPrinterName

                ''ByTine 14-10-2558 ฟอร์มไทย-ชิลี FORm5_2 
            Case "FORM5_2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_01Edi_receiptFORM5_2").ToString()
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

#End Region

#Region "   คำขอ เครื่อง 2   "
    'เครื่องพิมพ์คำขอ คลองเตย ST-002 เครื่อง 2
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
            Case "FORM2_5"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_02Edi_receiptFORM2_5").ToString()
                Return SendPrinterName
            Case "FORM2_6"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_02Edi_receiptFORM2_6").ToString()
                Return SendPrinterName
            Case "FORM3"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_02Edi_receiptFORM3").ToString()
                Return SendPrinterName
            Case "FORM3_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_02Edi_receiptFORM3_1").ToString()
                Return SendPrinterName
                'by rut D new
                'FORM44_4	ฟอร์ม ดี (ATIGA) ใหม่ (7 ประเทศ)
                'FORM44_44	ฟอร์ม ดี (ATIGA) Attach Sheet รถยนต์ ใหม่ (7 ประเทศ)
                'FORM44_41	ฟอร์ม ดี AICO ใหม่ (7 ประเทศ)
                'FORM441_4	ฟอร์ม ดี AICO Attach Sheet รถยนต์ ใหม่ (7 ประเทศ)
            Case "FORM44_4", "FORM44_44", "FORM44_41", "FORM441_4", "FORM44_01", "FORM44_02"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_02Edi_receiptFORM4").ToString()
                Return SendPrinterName
            Case "FORM4"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_02Edi_receiptFORM4").ToString()
                Return SendPrinterName
            Case "FORM44"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_02Edi_receiptFORM4").ToString()
                Return SendPrinterName
            Case "FORM441"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_02Edi_receiptFORM4_1").ToString()
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
            Case "FORM4_6", "FORM4_61"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_02Edi_receiptFORM4_6").ToString()
                Return SendPrinterName
            Case "FORM4_8", "FORM4_81"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_02Edi_receiptFORM4_8").ToString()
                Return SendPrinterName
            Case "FORM4_9"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_02Edi_receiptFORM4_9").ToString()
                Return SendPrinterName
            Case "FORM4_91", "FORM4_911"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_02Edi_receiptFORM4_91").ToString()
                Return SendPrinterName
            Case "FORM5"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_02Edi_receiptFORM5").ToString()
                Return SendPrinterName
            Case "FORM5_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_02Edi_receiptFORM5_1").ToString()
                Return SendPrinterName

                ''ByTine 14-10-2558 ฟอร์มไทย-ชิลี FORm5_2 
            Case "FORM5_2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_02Edi_receiptFORM5_2").ToString()
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
#End Region

#Region "   คำขอ เครื่อง 3   "
    'เครื่องพิมพ์คำขอ คลองเตย ST-002 เครื่อง 3
    Public Shared Function Call_SiteReceipt_ST_002_03(ByVal Form_ID As String) As String
        Dim SendPrinterName As String
        Select Case Form_ID
            Case "FORM1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_03Edi_receiptFORM1").ToString()
                Return SendPrinterName
            Case "FORM1_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_03Edi_receiptFORM1_1").ToString()
                Return SendPrinterName
            Case "FORM1_2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_03Edi_receiptFORM1_2").ToString()
                Return SendPrinterName
            Case "FORM1_3"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_03Edi_receiptFORM1_3").ToString()
                Return SendPrinterName
            Case "FORM1_4"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_03Edi_receiptFORM1_4").ToString()
                Return SendPrinterName
            Case "FORM2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_03Edi_receiptFORM2").ToString()
                Return SendPrinterName
            Case "FORM2_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_03Edi_receiptFORM2_1").ToString()
                Return SendPrinterName
            Case "FORM2_2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_03Edi_receiptFORM2_2").ToString()
                Return SendPrinterName
            Case "FORM2_3"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_03Edi_receiptFORM2_3").ToString()
                Return SendPrinterName
            Case "FORM2_4"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_03Edi_receiptFORM2_4").ToString()
                Return SendPrinterName
            Case "FORM2_5"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_03Edi_receiptFORM2_5").ToString()
                Return SendPrinterName
            Case "FORM2_6"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_03Edi_receiptFORM2_6").ToString()
                Return SendPrinterName
            Case "FORM3"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_03Edi_receiptFORM3").ToString()
                Return SendPrinterName
            Case "FORM3_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_03Edi_receiptFORM3_1").ToString()
                Return SendPrinterName
                'by rut D new
                'FORM44_4	ฟอร์ม ดี (ATIGA) ใหม่ (7 ประเทศ)
                'FORM44_44	ฟอร์ม ดี (ATIGA) Attach Sheet รถยนต์ ใหม่ (7 ประเทศ)
                'FORM44_41	ฟอร์ม ดี AICO ใหม่ (7 ประเทศ)
                'FORM441_4	ฟอร์ม ดี AICO Attach Sheet รถยนต์ ใหม่ (7 ประเทศ)
            Case "FORM44_4", "FORM44_44", "FORM44_41", "FORM441_4", "FORM44_01", "FORM44_02"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_03Edi_receiptFORM4").ToString()
                Return SendPrinterName
            Case "FORM4"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_03Edi_receiptFORM4").ToString()
                Return SendPrinterName
            Case "FORM44"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_03Edi_receiptFORM4").ToString()
                Return SendPrinterName
            Case "FORM441"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_03Edi_receiptFORM4_1").ToString()
                Return SendPrinterName
            Case "FORM4_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_03Edi_receiptFORM4_1").ToString()
                Return SendPrinterName
            Case "FORM4_2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_03Edi_receiptFORM4_2").ToString()
                Return SendPrinterName
            Case "FORM4_3"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_03Edi_receiptFORM4_3").ToString()
                Return SendPrinterName
            Case "FORM4_4"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_03Edi_receiptFORM4_4").ToString()
                Return SendPrinterName
            Case "FORM4_5"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_03Edi_receiptFORM4_5").ToString()
                Return SendPrinterName
            Case "FORM4_6", "FORM4_61"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_03Edi_receiptFORM4_6").ToString()
                Return SendPrinterName
            Case "FORM4_8", "FORM4_81"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_03Edi_receiptFORM4_8").ToString()
                Return SendPrinterName
            Case "FORM4_9"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_03Edi_receiptFORM4_9").ToString()
                Return SendPrinterName
            Case "FORM4_91", "FORM4_911"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_03Edi_receiptFORM4_91").ToString()
                Return SendPrinterName
            Case "FORM5"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_03Edi_receiptFORM5").ToString()
                Return SendPrinterName
            Case "FORM5_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_03Edi_receiptFORM5_1").ToString()
                Return SendPrinterName

                ''ByTine 14-10-2558 ฟอร์มไทย-ชิลี FORm5_2 
            Case "FORM5_2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_03Edi_receiptFORM5_2").ToString()
                Return SendPrinterName

            Case "FORM6"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_03Edi_receiptFORM6").ToString()
                Return SendPrinterName
            Case "FORM7"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_03Edi_receiptFORM7").ToString()
                Return SendPrinterName
            Case "FORM8"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_03Edi_receiptFORM8").ToString()
                Return SendPrinterName
            Case "FORM9"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_03Edi_receiptFORM9").ToString()
                Return SendPrinterName
            Case "FORMRussia"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_03Edi_receiptFORMRussia").ToString()
                Return SendPrinterName
        End Select
    End Function
#End Region

#Region "   คำขอ เครื่อง 4   "
    'เครื่องพิมพ์คำขอ คลองเตย ST-002 เครื่อง 4
    Public Shared Function Call_SiteReceipt_ST_002_04(ByVal Form_ID As String) As String
        Dim SendPrinterName As String
        Select Case Form_ID
            Case "FORM1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_04Edi_receiptFORM1").ToString()
                Return SendPrinterName
            Case "FORM1_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_04Edi_receiptFORM1_1").ToString()
                Return SendPrinterName
            Case "FORM1_2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_04Edi_receiptFORM1_2").ToString()
                Return SendPrinterName
            Case "FORM1_3"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_04Edi_receiptFORM1_3").ToString()
                Return SendPrinterName
            Case "FORM1_4"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_04Edi_receiptFORM1_4").ToString()
                Return SendPrinterName
            Case "FORM2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_04Edi_receiptFORM2").ToString()
                Return SendPrinterName
            Case "FORM2_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_04Edi_receiptFORM2_1").ToString()
                Return SendPrinterName
            Case "FORM2_2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_04Edi_receiptFORM2_2").ToString()
                Return SendPrinterName
            Case "FORM2_3"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_04Edi_receiptFORM2_3").ToString()
                Return SendPrinterName
            Case "FORM2_4"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_04Edi_receiptFORM2_4").ToString()
                Return SendPrinterName
            Case "FORM2_5"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_04Edi_receiptFORM2_5").ToString()
                Return SendPrinterName
            Case "FORM2_6"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_04Edi_receiptFORM2_6").ToString()
                Return SendPrinterName
            Case "FORM3"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_04Edi_receiptFORM3").ToString()
                Return SendPrinterName
            Case "FORM3_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_04Edi_receiptFORM3_1").ToString()
                Return SendPrinterName
                'by rut D new
                'FORM44_4	ฟอร์ม ดี (ATIGA) ใหม่ (7 ประเทศ)
                'FORM44_44	ฟอร์ม ดี (ATIGA) Attach Sheet รถยนต์ ใหม่ (7 ประเทศ)
                'FORM44_41	ฟอร์ม ดี AICO ใหม่ (7 ประเทศ)
                'FORM441_4	ฟอร์ม ดี AICO Attach Sheet รถยนต์ ใหม่ (7 ประเทศ)
            Case "FORM44_4", "FORM44_44", "FORM44_41", "FORM441_4", "FORM44_01", "FORM44_02"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_04Edi_receiptFORM4").ToString()
                Return SendPrinterName
            Case "FORM4"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_04Edi_receiptFORM4").ToString()
                Return SendPrinterName
            Case "FORM44"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_04Edi_receiptFORM4").ToString()
                Return SendPrinterName
            Case "FORM441"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_04Edi_receiptFORM4_1").ToString()
                Return SendPrinterName
            Case "FORM4_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_04Edi_receiptFORM4_1").ToString()
                Return SendPrinterName
            Case "FORM4_2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_04Edi_receiptFORM4_2").ToString()
                Return SendPrinterName
            Case "FORM4_3"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_04Edi_receiptFORM4_3").ToString()
                Return SendPrinterName
            Case "FORM4_4"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_04Edi_receiptFORM4_4").ToString()
                Return SendPrinterName
            Case "FORM4_5"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_04Edi_receiptFORM4_5").ToString()
                Return SendPrinterName
            Case "FORM4_6", "FORM4_61"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_04Edi_receiptFORM4_6").ToString()
                Return SendPrinterName
            Case "FORM4_8", "FORM4_81"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_04Edi_receiptFORM4_8").ToString()
                Return SendPrinterName
            Case "FORM4_9"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_04Edi_receiptFORM4_9").ToString()
                Return SendPrinterName
            Case "FORM4_91", "FORM4_911"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_04Edi_receiptFORM4_91").ToString()
                Return SendPrinterName
            Case "FORM5"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_04Edi_receiptFORM5").ToString()
                Return SendPrinterName
            Case "FORM5_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_04Edi_receiptFORM5_1").ToString()
                Return SendPrinterName

                ''ByTine 14-10-2558 ฟอร์มไทย-ชิลี FORm5_2 
            Case "FORM5_2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_04Edi_receiptFORM5_2").ToString()
                Return SendPrinterName

            Case "FORM6"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_04Edi_receiptFORM6").ToString()
                Return SendPrinterName
            Case "FORM7"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_04Edi_receiptFORM7").ToString()
                Return SendPrinterName
            Case "FORM8"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_04Edi_receiptFORM8").ToString()
                Return SendPrinterName
            Case "FORM9"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_04Edi_receiptFORM9").ToString()
                Return SendPrinterName
            Case "FORMRussia"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_04Edi_receiptFORMRussia").ToString()
                Return SendPrinterName
        End Select
    End Function
#End Region

#Region "   คำขอ เครื่อง 5   "
    'เครื่องพิมพ์คำขอ คลองเตย ST-002 เครื่อง 5
    Public Shared Function Call_SiteReceipt_ST_002_05(ByVal Form_ID As String) As String
        Dim SendPrinterName As String
        Select Case Form_ID
            Case "FORM1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_05Edi_receiptFORM1").ToString()
                Return SendPrinterName
            Case "FORM1_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_05Edi_receiptFORM1_1").ToString()
                Return SendPrinterName
            Case "FORM1_2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_05Edi_receiptFORM1_2").ToString()
                Return SendPrinterName
            Case "FORM1_3"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_05Edi_receiptFORM1_3").ToString()
                Return SendPrinterName
            Case "FORM1_4"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_05Edi_receiptFORM1_4").ToString()
                Return SendPrinterName
            Case "FORM2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_05Edi_receiptFORM2").ToString()
                Return SendPrinterName
            Case "FORM2_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_05Edi_receiptFORM2_1").ToString()
                Return SendPrinterName
            Case "FORM2_2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_05Edi_receiptFORM2_2").ToString()
                Return SendPrinterName
            Case "FORM2_3"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_05Edi_receiptFORM2_3").ToString()
                Return SendPrinterName
            Case "FORM2_4"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_05Edi_receiptFORM2_4").ToString()
                Return SendPrinterName
            Case "FORM2_5"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_05Edi_receiptFORM2_5").ToString()
                Return SendPrinterName
            Case "FORM2_6"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_05Edi_receiptFORM2_6").ToString()
                Return SendPrinterName
            Case "FORM3"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_05Edi_receiptFORM3").ToString()
                Return SendPrinterName
            Case "FORM3_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_05Edi_receiptFORM3_1").ToString()
                Return SendPrinterName
                'by rut D new
                'FORM44_4	ฟอร์ม ดี (ATIGA) ใหม่ (7 ประเทศ)
                'FORM44_44	ฟอร์ม ดี (ATIGA) Attach Sheet รถยนต์ ใหม่ (7 ประเทศ)
                'FORM44_41	ฟอร์ม ดี AICO ใหม่ (7 ประเทศ)
                'FORM441_4	ฟอร์ม ดี AICO Attach Sheet รถยนต์ ใหม่ (7 ประเทศ)
            Case "FORM44_4", "FORM44_44", "FORM44_41", "FORM441_4", "FORM44_01", "FORM44_02"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_05Edi_receiptFORM4").ToString()
                Return SendPrinterName
            Case "FORM4"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_05Edi_receiptFORM4").ToString()
                Return SendPrinterName
            Case "FORM44"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_05Edi_receiptFORM4").ToString()
                Return SendPrinterName
            Case "FORM441"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_05Edi_receiptFORM4_1").ToString()
                Return SendPrinterName
            Case "FORM4_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_05Edi_receiptFORM4_1").ToString()
                Return SendPrinterName
            Case "FORM4_2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_05Edi_receiptFORM4_2").ToString()
                Return SendPrinterName
            Case "FORM4_3"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_05Edi_receiptFORM4_3").ToString()
                Return SendPrinterName
            Case "FORM4_4"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_05Edi_receiptFORM4_4").ToString()
                Return SendPrinterName
            Case "FORM4_5"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_05Edi_receiptFORM4_5").ToString()
                Return SendPrinterName
            Case "FORM4_6", "FORM4_61"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_05Edi_receiptFORM4_6").ToString()
                Return SendPrinterName
            Case "FORM4_8", "FORM4_81"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_05Edi_receiptFORM4_8").ToString()
                Return SendPrinterName
            Case "FORM4_9"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_05Edi_receiptFORM4_9").ToString()
                Return SendPrinterName
            Case "FORM4_91", "FORM4_911"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_05Edi_receiptFORM4_91").ToString()
                Return SendPrinterName
            Case "FORM5"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_05Edi_receiptFORM5").ToString()
                Return SendPrinterName
            Case "FORM5_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_05Edi_receiptFORM5_1").ToString()
                Return SendPrinterName

                ''ByTine 14-10-2558 ฟอร์มไทย-ชิลี FORm5_2 
            Case "FORM5_2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_05Edi_receiptFORM5_2").ToString()
                Return SendPrinterName

            Case "FORM6"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_05Edi_receiptFORM6").ToString()
                Return SendPrinterName
            Case "FORM7"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_05Edi_receiptFORM7").ToString()
                Return SendPrinterName
            Case "FORM8"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_05Edi_receiptFORM8").ToString()
                Return SendPrinterName
            Case "FORM9"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_05Edi_receiptFORM9").ToString()
                Return SendPrinterName
            Case "FORMRussia"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_05Edi_receiptFORMRussia").ToString()
                Return SendPrinterName
        End Select
    End Function
#End Region

#Region "   คำขอ เครื่อง 6   "
    'เครื่องพิมพ์คำขอ คลองเตย ST-002 เครื่อง 6
    Public Shared Function Call_SiteReceipt_ST_002_06(ByVal Form_ID As String) As String
        Dim SendPrinterName As String
        Select Case Form_ID
            Case "FORM1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_06Edi_receiptFORM1").ToString()
                Return SendPrinterName
            Case "FORM1_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_06Edi_receiptFORM1_1").ToString()
                Return SendPrinterName
            Case "FORM1_2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_06Edi_receiptFORM1_2").ToString()
                Return SendPrinterName
            Case "FORM1_3"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_06Edi_receiptFORM1_3").ToString()
                Return SendPrinterName
            Case "FORM1_4"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_06Edi_receiptFORM1_4").ToString()
                Return SendPrinterName
            Case "FORM2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_06Edi_receiptFORM2").ToString()
                Return SendPrinterName
            Case "FORM2_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_06Edi_receiptFORM2_1").ToString()
                Return SendPrinterName
            Case "FORM2_2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_06Edi_receiptFORM2_2").ToString()
                Return SendPrinterName
            Case "FORM2_3"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_06Edi_receiptFORM2_3").ToString()
                Return SendPrinterName
            Case "FORM2_4"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_06Edi_receiptFORM2_4").ToString()
                Return SendPrinterName
            Case "FORM2_5"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_06Edi_receiptFORM2_5").ToString()
                Return SendPrinterName
            Case "FORM2_6"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_06Edi_receiptFORM2_6").ToString()
                Return SendPrinterName
            Case "FORM3"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_06Edi_receiptFORM3").ToString()
                Return SendPrinterName
            Case "FORM3_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_06Edi_receiptFORM3_1").ToString()
                Return SendPrinterName
                'by rut D new
                'FORM44_4	ฟอร์ม ดี (ATIGA) ใหม่ (7 ประเทศ)
                'FORM44_44	ฟอร์ม ดี (ATIGA) Attach Sheet รถยนต์ ใหม่ (7 ประเทศ)
                'FORM44_41	ฟอร์ม ดี AICO ใหม่ (7 ประเทศ)
                'FORM441_4	ฟอร์ม ดี AICO Attach Sheet รถยนต์ ใหม่ (7 ประเทศ)
            Case "FORM44_4", "FORM44_44", "FORM44_41", "FORM441_4", "FORM44_01", "FORM44_02"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_06Edi_receiptFORM4").ToString()
                Return SendPrinterName
            Case "FORM4"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_06Edi_receiptFORM4").ToString()
                Return SendPrinterName
            Case "FORM44"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_06Edi_receiptFORM4").ToString()
                Return SendPrinterName
            Case "FORM441"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_06Edi_receiptFORM4_1").ToString()
                Return SendPrinterName
            Case "FORM4_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_06Edi_receiptFORM4_1").ToString()
                Return SendPrinterName
            Case "FORM4_2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_06Edi_receiptFORM4_2").ToString()
                Return SendPrinterName
            Case "FORM4_3"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_06Edi_receiptFORM4_3").ToString()
                Return SendPrinterName
            Case "FORM4_4"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_06Edi_receiptFORM4_4").ToString()
                Return SendPrinterName
            Case "FORM4_5"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_06Edi_receiptFORM4_5").ToString()
                Return SendPrinterName
            Case "FORM4_6", "FORM4_61"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_06Edi_receiptFORM4_6").ToString()
                Return SendPrinterName
            Case "FORM4_8", "FORM4_81"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_06Edi_receiptFORM4_8").ToString()
                Return SendPrinterName
            Case "FORM4_9"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_06Edi_receiptFORM4_9").ToString()
                Return SendPrinterName
            Case "FORM4_91", "FORM4_911"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_06Edi_receiptFORM4_91").ToString()
                Return SendPrinterName
            Case "FORM5"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_06Edi_receiptFORM5").ToString()
                Return SendPrinterName
            Case "FORM5_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_06Edi_receiptFORM5_2").ToString()
                Return SendPrinterName

                ''ByTine 14-10-2558 ฟอร์มไทย-ชิลี FORm5_2 
            Case "FORM5_2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_06Edi_receiptFORM5_2").ToString()
                Return SendPrinterName

            Case "FORM6"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_06Edi_receiptFORM6").ToString()
                Return SendPrinterName
            Case "FORM7"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_06Edi_receiptFORM7").ToString()
                Return SendPrinterName
            Case "FORM8"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_06Edi_receiptFORM8").ToString()
                Return SendPrinterName
            Case "FORM9"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_06Edi_receiptFORM9").ToString()
                Return SendPrinterName
            Case "FORMRussia"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_06Edi_receiptFORMRussia").ToString()
                Return SendPrinterName
        End Select
    End Function
#End Region

#Region "   คำขอ เครื่อง 9   "
    'เครื่องพิมพ์คำขอ คลองเตย ST-002 เครื่อง 9
    Public Shared Function Call_SiteReceipt_ST_002_09(ByVal Form_ID As String) As String
        Dim SendPrinterName As String
        Select Case Form_ID
            Case "FORM1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_09Edi_receiptFORM1").ToString()
                Return SendPrinterName
            Case "FORM1_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_09Edi_receiptFORM1_1").ToString()
                Return SendPrinterName
            Case "FORM1_2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_09Edi_receiptFORM1_2").ToString()
                Return SendPrinterName
            Case "FORM1_3"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_09Edi_receiptFORM1_3").ToString()
                Return SendPrinterName
            Case "FORM1_4"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_09Edi_receiptFORM1_4").ToString()
                Return SendPrinterName
            Case "FORM2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_09Edi_receiptFORM2").ToString()
                Return SendPrinterName
            Case "FORM2_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_09Edi_receiptFORM2_1").ToString()
                Return SendPrinterName
            Case "FORM2_2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_09Edi_receiptFORM2_2").ToString()
                Return SendPrinterName
            Case "FORM2_3"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_09Edi_receiptFORM2_3").ToString()
                Return SendPrinterName
            Case "FORM2_4"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_09Edi_receiptFORM2_4").ToString()
                Return SendPrinterName
            Case "FORM2_5"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_09Edi_receiptFORM2_5").ToString()
                Return SendPrinterName
            Case "FORM2_6"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_09Edi_receiptFORM2_6").ToString()
                Return SendPrinterName
            Case "FORM3"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_09Edi_receiptFORM3").ToString()
                Return SendPrinterName
            Case "FORM3_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_09Edi_receiptFORM3_1").ToString()
                Return SendPrinterName
                'by rut D new
                'FORM44_4	ฟอร์ม ดี (ATIGA) ใหม่ (7 ประเทศ)
                'FORM44_44	ฟอร์ม ดี (ATIGA) Attach Sheet รถยนต์ ใหม่ (7 ประเทศ)
                'FORM44_41	ฟอร์ม ดี AICO ใหม่ (7 ประเทศ)
                'FORM441_4	ฟอร์ม ดี AICO Attach Sheet รถยนต์ ใหม่ (7 ประเทศ)
            Case "FORM44_4", "FORM44_44", "FORM44_41", "FORM441_4", "FORM44_01", "FORM44_02"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_09Edi_receiptFORM4").ToString()
                Return SendPrinterName
            Case "FORM4"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_09Edi_receiptFORM4").ToString()
                Return SendPrinterName
            Case "FORM44"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_09Edi_receiptFORM4").ToString()
                Return SendPrinterName
            Case "FORM441"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_09Edi_receiptFORM4_1").ToString()
                Return SendPrinterName
            Case "FORM4_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_09Edi_receiptFORM4_1").ToString()
                Return SendPrinterName
            Case "FORM4_2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_09Edi_receiptFORM4_2").ToString()
                Return SendPrinterName
            Case "FORM4_3"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_09Edi_receiptFORM4_3").ToString()
                Return SendPrinterName
            Case "FORM4_4"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_09Edi_receiptFORM4_4").ToString()
                Return SendPrinterName
            Case "FORM4_5"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_09Edi_receiptFORM4_5").ToString()
                Return SendPrinterName
            Case "FORM4_6", "FORM4_61"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_09Edi_receiptFORM4_6").ToString()
                Return SendPrinterName
            Case "FORM4_8", "FORM4_81"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_09Edi_receiptFORM4_8").ToString()
                Return SendPrinterName
            Case "FORM4_9"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_09Edi_receiptFORM4_9").ToString()
                Return SendPrinterName
            Case "FORM4_91", "FORM4_911"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_09Edi_receiptFORM4_91").ToString()
                Return SendPrinterName
            Case "FORM5"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_09Edi_receiptFORM5").ToString()
                Return SendPrinterName
            Case "FORM5_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_09Edi_receiptFORM5_1").ToString()
                Return SendPrinterName

                ''ByTine 14-10-2558 ฟอร์มไทย-ชิลี FORm5_2 
            Case "FORM5_2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_09Edi_receiptFORM5_2").ToString()
                Return SendPrinterName

            Case "FORM6"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_09Edi_receiptFORM6").ToString()
                Return SendPrinterName
            Case "FORM7"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_09Edi_receiptFORM7").ToString()
                Return SendPrinterName
            Case "FORM8"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_09Edi_receiptFORM8").ToString()
                Return SendPrinterName
            Case "FORM9"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_09Edi_receiptFORM9").ToString()
                Return SendPrinterName
            Case "FORMRussia"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_09Edi_receiptFORMRussia").ToString()
                Return SendPrinterName
        End Select
    End Function
#End Region

#Region "   คำขอ เครื่อง 10   "
    'เครื่องพิมพ์คำขอ คลองเตย ST-002 เครื่อง 10
    Public Shared Function Call_SiteReceipt_ST_002_10(ByVal Form_ID As String) As String
        Dim SendPrinterName As String
        Select Case Form_ID
            Case "FORM1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_10Edi_receiptFORM1").ToString()
                Return SendPrinterName
            Case "FORM1_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_10Edi_receiptFORM1_1").ToString()
                Return SendPrinterName
            Case "FORM1_2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_10Edi_receiptFORM1_2").ToString()
                Return SendPrinterName
            Case "FORM1_3"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_10Edi_receiptFORM1_3").ToString()
                Return SendPrinterName
            Case "FORM1_4"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_10Edi_receiptFORM1_4").ToString()
                Return SendPrinterName
            Case "FORM2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_10Edi_receiptFORM2").ToString()
                Return SendPrinterName
            Case "FORM2_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_10Edi_receiptFORM2_1").ToString()
                Return SendPrinterName
            Case "FORM2_2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_10Edi_receiptFORM2_2").ToString()
                Return SendPrinterName
            Case "FORM2_3"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_10Edi_receiptFORM2_3").ToString()
                Return SendPrinterName
            Case "FORM2_4"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_10Edi_receiptFORM2_4").ToString()
                Return SendPrinterName
            Case "FORM2_5"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_10Edi_receiptFORM2_5").ToString()
                Return SendPrinterName
            Case "FORM2_6"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_10Edi_receiptFORM2_6").ToString()
                Return SendPrinterName
            Case "FORM3"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_10Edi_receiptFORM3").ToString()
                Return SendPrinterName
            Case "FORM3_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_10Edi_receiptFORM3_1").ToString()
                Return SendPrinterName
                'by rut D new
                'FORM44_4	ฟอร์ม ดี (ATIGA) ใหม่ (7 ประเทศ)
                'FORM44_44	ฟอร์ม ดี (ATIGA) Attach Sheet รถยนต์ ใหม่ (7 ประเทศ)
                'FORM44_41	ฟอร์ม ดี AICO ใหม่ (7 ประเทศ)
                'FORM441_4	ฟอร์ม ดี AICO Attach Sheet รถยนต์ ใหม่ (7 ประเทศ)
            Case "FORM44_4", "FORM44_44", "FORM44_41", "FORM441_4", "FORM44_01", "FORM44_02"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_10Edi_receiptFORM4").ToString()
                Return SendPrinterName
            Case "FORM4"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_10Edi_receiptFORM4").ToString()
                Return SendPrinterName
            Case "FORM44"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_10Edi_receiptFORM4").ToString()
                Return SendPrinterName
            Case "FORM441"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_10Edi_receiptFORM4_1").ToString()
                Return SendPrinterName
            Case "FORM4_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_10Edi_receiptFORM4_1").ToString()
                Return SendPrinterName
            Case "FORM4_2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_10Edi_receiptFORM4_2").ToString()
                Return SendPrinterName
            Case "FORM4_3"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_10Edi_receiptFORM4_3").ToString()
                Return SendPrinterName
            Case "FORM4_4"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_10Edi_receiptFORM4_4").ToString()
                Return SendPrinterName
            Case "FORM4_5"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_10Edi_receiptFORM4_5").ToString()
                Return SendPrinterName
            Case "FORM4_6", "FORM4_61"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_10Edi_receiptFORM4_6").ToString()
                Return SendPrinterName
            Case "FORM4_8", "FORM4_81"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_10Edi_receiptFORM4_8").ToString()
                Return SendPrinterName
            Case "FORM4_9"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_10Edi_receiptFORM4_9").ToString()
                Return SendPrinterName
            Case "FORM4_91", "FORM4_911"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_10Edi_receiptFORM4_91").ToString()
                Return SendPrinterName
            Case "FORM5"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_10Edi_receiptFORM5").ToString()
                Return SendPrinterName
            Case "FORM5_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_10Edi_receiptFORM5_1").ToString()
                Return SendPrinterName

                ''ByTine 14-10-2558 ฟอร์มไทย-ชิลี FORm5_2 
            Case "FORM5_2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_10Edi_receiptFORM5_2").ToString()
                Return SendPrinterName

            Case "FORM6"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_10Edi_receiptFORM6").ToString()
                Return SendPrinterName
            Case "FORM7"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_10Edi_receiptFORM7").ToString()
                Return SendPrinterName
            Case "FORM8"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_10Edi_receiptFORM8").ToString()
                Return SendPrinterName
            Case "FORM9"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_10Edi_receiptFORM9").ToString()
                Return SendPrinterName
            Case "FORMRussia"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_10Edi_receiptFORMRussia").ToString()
                Return SendPrinterName
        End Select
    End Function
#End Region

#Region "by rut ไม่ได้ใช้แล้ว 27-07-2556"
#Region "   คำขอ เครื่อง 7 DS   "
    'เครื่องพิมพ์คำขอ คลองเตย ST-002 เครื่อง 7 DS
    Public Shared Function Call_SiteReceipt_ST_002_07(ByVal Form_ID As String) As String
        Dim SendPrinterName As String
        Select Case Form_ID
            Case "FORM1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_07Edi_receiptFORM1").ToString()
                Return SendPrinterName
            Case "FORM1_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_07Edi_receiptFORM1_1").ToString()
                Return SendPrinterName
            Case "FORM1_2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_07Edi_receiptFORM1_2").ToString()
                Return SendPrinterName
            Case "FORM1_3"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_07Edi_receiptFORM1_3").ToString()
                Return SendPrinterName
            Case "FORM1_4"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_07Edi_receiptFORM1_4").ToString()
                Return SendPrinterName
            Case "FORM2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_07Edi_receiptFORM2").ToString()
                Return SendPrinterName
            Case "FORM2_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_07Edi_receiptFORM2_1").ToString()
                Return SendPrinterName
            Case "FORM2_2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_07Edi_receiptFORM2_2").ToString()
                Return SendPrinterName
            Case "FORM2_3"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_07Edi_receiptFORM2_3").ToString()
                Return SendPrinterName
            Case "FORM2_4"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_07Edi_receiptFORM2_4").ToString()
                Return SendPrinterName
            Case "FORM3"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_07Edi_receiptFORM3").ToString()
                Return SendPrinterName
            Case "FORM3_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_07Edi_receiptFORM3_1").ToString()
                Return SendPrinterName
            Case "FORM4"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_07Edi_receiptFORM4").ToString()
                Return SendPrinterName
            Case "FORM44"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_07Edi_receiptFORM4").ToString()
                Return SendPrinterName
            Case "FORM441"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_07Edi_receiptFORM4_1").ToString()
                Return SendPrinterName
            Case "FORM4_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_07Edi_receiptFORM4_1").ToString()
                Return SendPrinterName
            Case "FORM4_2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_07Edi_receiptFORM4_2").ToString()
                Return SendPrinterName
            Case "FORM4_3"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_07Edi_receiptFORM4_3").ToString()
                Return SendPrinterName
            Case "FORM4_4"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_07Edi_receiptFORM4_4").ToString()
                Return SendPrinterName
            Case "FORM4_5"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_07Edi_receiptFORM4_5").ToString()
                Return SendPrinterName
            Case "FORM4_6"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_07Edi_receiptFORM4_6").ToString()
                Return SendPrinterName
            Case "FORM4_8"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_07Edi_receiptFORM4_8").ToString()
                Return SendPrinterName
            Case "FORM4_9"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_07Edi_receiptFORM4_9").ToString()
                Return SendPrinterName
            Case "FORM4_91", "FORM4_911"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_07Edi_receiptFORM4_91").ToString()
                Return SendPrinterName
            Case "FORM5"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_07Edi_receiptFORM5").ToString()
                Return SendPrinterName
            Case "FORM5_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_07Edi_receiptFORM5_1").ToString()
                Return SendPrinterName

                ''ByTine 14-10-2558 ฟอร์มไทย-ชิลี FORm5_2 
            Case "FORM5_2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_07Edi_receiptFORM5_2").ToString()
                Return SendPrinterName

            Case "FORM6"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_07Edi_receiptFORM6").ToString()
                Return SendPrinterName
            Case "FORM7"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_07Edi_receiptFORM7").ToString()
                Return SendPrinterName
            Case "FORM8"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_07Edi_receiptFORM8").ToString()
                Return SendPrinterName
            Case "FORM9"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_07Edi_receiptFORM9").ToString()
                Return SendPrinterName
            Case "FORMRussia"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_07Edi_receiptFORMRussia").ToString()
                Return SendPrinterName
        End Select
    End Function
#End Region

#Region "   คำขอ เครื่อง 8 DS   "
    'เครื่องพิมพ์คำขอ คลองเตย ST-002 เครื่อง 8 DS
    Public Shared Function Call_SiteReceipt_ST_002_08(ByVal Form_ID As String) As String
        Dim SendPrinterName As String
        Select Case Form_ID
            Case "FORM1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_08Edi_receiptFORM1").ToString()
                Return SendPrinterName
            Case "FORM1_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_08Edi_receiptFORM1_1").ToString()
                Return SendPrinterName
            Case "FORM1_2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_08Edi_receiptFORM1_2").ToString()
                Return SendPrinterName
            Case "FORM1_3"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_08Edi_receiptFORM1_3").ToString()
                Return SendPrinterName
            Case "FORM1_4"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_08Edi_receiptFORM1_4").ToString()
                Return SendPrinterName
            Case "FORM2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_08Edi_receiptFORM2").ToString()
                Return SendPrinterName
            Case "FORM2_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_08Edi_receiptFORM2_1").ToString()
                Return SendPrinterName
            Case "FORM2_2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_08Edi_receiptFORM2_2").ToString()
                Return SendPrinterName
            Case "FORM2_3"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_08Edi_receiptFORM2_3").ToString()
                Return SendPrinterName
            Case "FORM2_4"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_08Edi_receiptFORM2_4").ToString()
                Return SendPrinterName
            Case "FORM3"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_08Edi_receiptFORM3").ToString()
                Return SendPrinterName
            Case "FORM3_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_08Edi_receiptFORM3_1").ToString()
                Return SendPrinterName
            Case "FORM4"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_08Edi_receiptFORM4").ToString()
                Return SendPrinterName
            Case "FORM44"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_08Edi_receiptFORM4").ToString()
                Return SendPrinterName
            Case "FORM441"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_08Edi_receiptFORM4_1").ToString()
                Return SendPrinterName
            Case "FORM4_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_08Edi_receiptFORM4_1").ToString()
                Return SendPrinterName
            Case "FORM4_2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_08Edi_receiptFORM4_2").ToString()
                Return SendPrinterName
            Case "FORM4_3"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_08Edi_receiptFORM4_3").ToString()
                Return SendPrinterName
            Case "FORM4_4"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_08Edi_receiptFORM4_4").ToString()
                Return SendPrinterName
            Case "FORM4_5"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_08Edi_receiptFORM4_5").ToString()
                Return SendPrinterName
            Case "FORM4_6"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_08Edi_receiptFORM4_6").ToString()
                Return SendPrinterName
            Case "FORM4_8"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_08Edi_receiptFORM4_8").ToString()
                Return SendPrinterName
            Case "FORM4_9"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_08Edi_receiptFORM4_9").ToString()
                Return SendPrinterName
            Case "FORM4_91", "FORM4_911"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_08Edi_receiptFORM4_91").ToString()
                Return SendPrinterName
            Case "FORM5"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_08Edi_receiptFORM5").ToString()
                Return SendPrinterName
            Case "FORM5_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_08Edi_receiptFORM5_1").ToString()
                Return SendPrinterName

                ''ByTine 14-10-2558 ฟอร์มไทย-ชิลี FORm5_2 
            Case "FORM5_2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_08Edi_receiptFORM5_2").ToString()
                Return SendPrinterName

            Case "FORM6"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_08Edi_receiptFORM6").ToString()
                Return SendPrinterName
            Case "FORM7"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_08Edi_receiptFORM7").ToString()
                Return SendPrinterName
            Case "FORM8"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_08Edi_receiptFORM8").ToString()
                Return SendPrinterName
            Case "FORM9"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_08Edi_receiptFORM9").ToString()
                Return SendPrinterName
            Case "FORMRussia"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_08Edi_receiptFORMRussia").ToString()
                Return SendPrinterName
        End Select
    End Function
#End Region
#End Region

#Region "   คำขอ เครื่อง 11 DS   "
    'เครื่องพิมพ์คำขอ คลองเตย ST-002 เครื่อง 11 DS
    Public Shared Function Call_SiteReceipt_ST_002_11(ByVal Form_ID As String) As String
        Dim SendPrinterName As String
        Select Case Form_ID
            Case "FORM1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_11Edi_receiptFORM1").ToString()
                Return SendPrinterName
            Case "FORM1_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_11Edi_receiptFORM1_1").ToString()
                Return SendPrinterName
            Case "FORM1_2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_11Edi_receiptFORM1_2").ToString()
                Return SendPrinterName
            Case "FORM1_3"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_11Edi_receiptFORM1_3").ToString()
                Return SendPrinterName
            Case "FORM1_4"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_11Edi_receiptFORM1_4").ToString()
                Return SendPrinterName
            Case "FORM2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_11Edi_receiptFORM2").ToString()
                Return SendPrinterName
            Case "FORM2_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_11Edi_receiptFORM2_1").ToString()
                Return SendPrinterName
            Case "FORM2_2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_11Edi_receiptFORM2_2").ToString()
                Return SendPrinterName
            Case "FORM2_3"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_11Edi_receiptFORM2_3").ToString()
                Return SendPrinterName
            Case "FORM2_4"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_11Edi_receiptFORM2_4").ToString()
                Return SendPrinterName
            Case "FORM2_5"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_11Edi_receiptFORM2_5").ToString()
                Return SendPrinterName
            Case "FORM2_6"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_11Edi_receiptFORM2_6").ToString()
                Return SendPrinterName
            Case "FORM3"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_11Edi_receiptFORM3").ToString()
                Return SendPrinterName
            Case "FORM3_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_11Edi_receiptFORM3_1").ToString()
                Return SendPrinterName
                'by rut D new
                'FORM44_4	ฟอร์ม ดี (ATIGA) ใหม่ (7 ประเทศ)
                'FORM44_44	ฟอร์ม ดี (ATIGA) Attach Sheet รถยนต์ ใหม่ (7 ประเทศ)
                'FORM44_41	ฟอร์ม ดี AICO ใหม่ (7 ประเทศ)
                'FORM441_4	ฟอร์ม ดี AICO Attach Sheet รถยนต์ ใหม่ (7 ประเทศ)
            Case "FORM44_4", "FORM44_44", "FORM44_41", "FORM441_4", "FORM44_01", "FORM44_02"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_11Edi_receiptFORM4").ToString()
                Return SendPrinterName
            Case "FORM4"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_11Edi_receiptFORM4").ToString()
                Return SendPrinterName
            Case "FORM44"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_11Edi_receiptFORM4").ToString()
                Return SendPrinterName
            Case "FORM441"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_11Edi_receiptFORM4_1").ToString()
                Return SendPrinterName
            Case "FORM4_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_11Edi_receiptFORM4_1").ToString()
                Return SendPrinterName
            Case "FORM4_2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_11Edi_receiptFORM4_2").ToString()
                Return SendPrinterName
            Case "FORM4_3"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_11Edi_receiptFORM4_3").ToString()
                Return SendPrinterName
            Case "FORM4_4"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_11Edi_receiptFORM4_4").ToString()
                Return SendPrinterName
            Case "FORM4_5"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_11Edi_receiptFORM4_5").ToString()
                Return SendPrinterName
            Case "FORM4_6", "FORM4_61"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_11Edi_receiptFORM4_6").ToString()
                Return SendPrinterName
            Case "FORM4_8", "FORM4_81"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_11Edi_receiptFORM4_8").ToString()
                Return SendPrinterName
            Case "FORM4_9"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_11Edi_receiptFORM4_9").ToString()
                Return SendPrinterName
            Case "FORM4_91", "FORM4_911"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_11Edi_receiptFORM4_91").ToString()
                Return SendPrinterName
            Case "FORM5"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_11Edi_receiptFORM5").ToString()
                Return SendPrinterName
            Case "FORM5_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_11Edi_receiptFORM5_1").ToString()
                Return SendPrinterName

                ''ByTine 14-10-2558 ฟอร์มไทย-ชิลี FORm5_2 
            Case "FORM5_2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_11Edi_receiptFORM5_2").ToString()
                Return SendPrinterName

            Case "FORM6"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_11Edi_receiptFORM6").ToString()
                Return SendPrinterName
            Case "FORM7"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_11Edi_receiptFORM7").ToString()
                Return SendPrinterName
            Case "FORM8"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_11Edi_receiptFORM8").ToString()
                Return SendPrinterName
            Case "FORM9"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_11Edi_receiptFORM9").ToString()
                Return SendPrinterName
            Case "FORMRussia"
                SendPrinterName = ConfigurationManager.AppSettings("ST_002_11Edi_receiptFORMRussia").ToString()
                Return SendPrinterName
        End Select
    End Function
#End Region
#End Region

#Region "   Site ST-003 สุวรรณภูมิ Config Printer หนังสือรับรอง-คำขอ    "

#Region "   หนังสือรับรอง   "
    'หนังสือรับรอง ST-003 'สุวรรณภูมิ
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
            Case "FORM2_5"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003Edi_FORM2_5").ToString()
                Return SendPrinterName
            Case "FORM2_6"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003Edi_FORM2_6").ToString()
                Return SendPrinterName
            Case "FORM3"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003Edi_FORM3").ToString()
                Return SendPrinterName
            Case "FORM3_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003Edi_FORM3_1").ToString()
                Return SendPrinterName
                'by rut D new
                'FORM44_4	ฟอร์ม ดี (ATIGA) ใหม่ (7 ประเทศ)
                'FORM44_44	ฟอร์ม ดี (ATIGA) Attach Sheet รถยนต์ ใหม่ (7 ประเทศ)
                'FORM44_41	ฟอร์ม ดี AICO ใหม่ (7 ประเทศ)
                'FORM441_4	ฟอร์ม ดี AICO Attach Sheet รถยนต์ ใหม่ (7 ประเทศ)
            Case "FORM44_4", "FORM44_44", "FORM44_41", "FORM441_4", "FORM44_01", "FORM44_02"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003Edi_FORM44ALL").ToString()
                Return SendPrinterName
            Case "FORM4", "FORM44"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003Edi_FORM4").ToString()
                Return SendPrinterName
            Case "FORM4_1", "FORM441"
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
            Case "FORM4_6", "FORM4_61"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003Edi_FORM4_6").ToString()
                Return SendPrinterName
            Case "FORM4_8", "FORM4_81"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003Edi_FORM4_8").ToString()
                Return SendPrinterName
            Case "FORM4_9"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003Edi_FORM4_9").ToString()
                Return SendPrinterName
            Case "FORM4_91", "FORM4_911"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003Edi_FORM4_91").ToString()
                Return SendPrinterName
            Case "FORM5"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003Edi_FORM5").ToString()
                Return SendPrinterName
            Case "FORM5_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003Edi_FORM5_1").ToString()
                Return SendPrinterName

                ''ByTine 14-10-2558 ฟอร์มไทย-ชิลี FORm5_2 
            Case "FORM5_2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003Edi_FORM5_2").ToString()
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
    'DS
    Public Shared Function Call_SitePrintsDS_ST_003(ByVal Form_ID As String) As String
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
            Case "FORM2_5"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003Edi_FORM2_5").ToString()
                Return SendPrinterName
            Case "FORM2_6"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003Edi_FORM2_6").ToString()
                Return SendPrinterName
            Case "FORM3"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003Edi_FORM3").ToString()
                Return SendPrinterName
            Case "FORM3_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003Edi_FORM3_1").ToString()
                Return SendPrinterName
                'by rut D new
                'FORM44_4	ฟอร์ม ดี (ATIGA) ใหม่ (7 ประเทศ)
                'FORM44_44	ฟอร์ม ดี (ATIGA) Attach Sheet รถยนต์ ใหม่ (7 ประเทศ)
                'FORM44_41	ฟอร์ม ดี AICO ใหม่ (7 ประเทศ)
                'FORM441_4	ฟอร์ม ดี AICO Attach Sheet รถยนต์ ใหม่ (7 ประเทศ)
            Case "FORM44_4", "FORM44_44", "FORM44_41", "FORM441_4", "FORM44_01", "FORM44_02"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003DSEdi_FORM44ALL").ToString()
                Return SendPrinterName
            Case "FORM4", "FORM44"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003DSEdi_FORM4").ToString()
                Return SendPrinterName
            Case "FORM4_1", "FORM441"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003DSEdi_FORM4").ToString()
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
            Case "FORM4_6", "FORM4_61"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003Edi_FORM4_6").ToString()
                Return SendPrinterName
            Case "FORM4_8", "FORM4_81"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003Edi_FORM4_8").ToString()
                Return SendPrinterName
            Case "FORM4_9"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003Edi_FORM4_9").ToString()
                Return SendPrinterName
            Case "FORM4_91", "FORM4_911"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003Edi_FORM4_91").ToString()
                Return SendPrinterName
            Case "FORM5"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003Edi_FORM5").ToString()
                Return SendPrinterName
            Case "FORM5_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003Edi_FORM5_1").ToString()
                Return SendPrinterName

                ''ByTine 14-10-2558 ฟอร์มไทย-ชิลี FORm5_2 
            Case "FORM5_2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003Edi_FORM5_2").ToString()
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

    Public Shared Function Call_SitePrintsDS_ST_003_V02(ByVal Form_ID As String) As String
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
            Case "FORM2_5"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003Edi_FORM2_5").ToString()
                Return SendPrinterName
            Case "FORM2_6"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003Edi_FORM2_6").ToString()
                Return SendPrinterName
            Case "FORM3"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003Edi_FORM3").ToString()
                Return SendPrinterName
            Case "FORM3_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003Edi_FORM3_1").ToString()
                Return SendPrinterName
                'by rut D new
                'FORM44_4	ฟอร์ม ดี (ATIGA) ใหม่ (7 ประเทศ)
                'FORM44_44	ฟอร์ม ดี (ATIGA) Attach Sheet รถยนต์ ใหม่ (7 ประเทศ)
                'FORM44_41	ฟอร์ม ดี AICO ใหม่ (7 ประเทศ)
                'FORM441_4	ฟอร์ม ดี AICO Attach Sheet รถยนต์ ใหม่ (7 ประเทศ)
            Case "FORM44_4", "FORM44_44", "FORM44_41", "FORM441_4", "FORM44_01", "FORM44_02"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003DSEdi_FORM44ALL_2").ToString()
                Return SendPrinterName
            Case "FORM4", "FORM44"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003DSEdi_FORM4").ToString()
                Return SendPrinterName
            Case "FORM4_1", "FORM441"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003DSEdi_FORM4").ToString()
                Return SendPrinterName

            Case "FORM4_2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003Edi_FORM4_2_2").ToString()
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
            Case "FORM4_6", "FORM4_61"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003Edi_FORM4_6").ToString()
                Return SendPrinterName
            Case "FORM4_8", "FORM4_81"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003Edi_FORM4_8").ToString()
                Return SendPrinterName
            Case "FORM4_9"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003Edi_FORM4_9").ToString()
                Return SendPrinterName
            Case "FORM4_91", "FORM4_911"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003Edi_FORM4_91").ToString()
                Return SendPrinterName
            Case "FORM5"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003Edi_FORM5").ToString()
                Return SendPrinterName
            Case "FORM5_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003Edi_FORM5_1").ToString()
                Return SendPrinterName

                ''ByTine 14-10-2558 ฟอร์มไทย-ชิลี FORm5_2 
            Case "FORM5_2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003Edi_FORM5_2").ToString()
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

#End Region

#Region "   คำขอ เครื่อง 1   "
    'เครื่องพิมพ์คำขอ สุวรรณภูมิ ST-003
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
            Case "FORM2_5"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003_01Edi_receiptFORM2_5").ToString()
                Return SendPrinterName
            Case "FORM2_6"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003_01Edi_receiptFORM2_6").ToString()
                Return SendPrinterName
            Case "FORM3"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003_01Edi_receiptFORM3").ToString()
                Return SendPrinterName
            Case "FORM3_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003_01Edi_receiptFORM3_1").ToString()
                Return SendPrinterName
                'by rut D new
                'FORM44_4	ฟอร์ม ดี (ATIGA) ใหม่ (7 ประเทศ)
                'FORM44_44	ฟอร์ม ดี (ATIGA) Attach Sheet รถยนต์ ใหม่ (7 ประเทศ)
                'FORM44_41	ฟอร์ม ดี AICO ใหม่ (7 ประเทศ)
                'FORM441_4	ฟอร์ม ดี AICO Attach Sheet รถยนต์ ใหม่ (7 ประเทศ)
            Case "FORM44_4", "FORM44_44", "FORM44_41", "FORM441_4", "FORM44_01", "FORM44_02"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003_01Edi_receiptFORM4").ToString()
                Return SendPrinterName
            Case "FORM4", "FORM44"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003_01Edi_receiptFORM4").ToString()
                Return SendPrinterName
            Case "FORM4_1", "FORM441"
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
            Case "FORM4_6", "FORM4_61"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003_01Edi_receiptFORM4_6").ToString()
                Return SendPrinterName
            Case "FORM4_8", "FORM4_81"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003_01Edi_receiptFORM4_8").ToString()
                Return SendPrinterName
            Case "FORM4_9"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003_01Edi_receiptFORM4_9").ToString()
                Return SendPrinterName
            Case "FORM4_91", "FORM4_911"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003_01Edi_receiptFORM4_91").ToString()
                Return SendPrinterName
            Case "FORM5"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003_01Edi_receiptFORM5").ToString()
                Return SendPrinterName
            Case "FORM5_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003_01Edi_receiptFORM5_1").ToString()
                Return SendPrinterName

                ''ByTine 14-10-2558 ฟอร์มไทย-ชิลี FORm5_2 
            Case "FORM5_2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003_01Edi_receiptFORM5_2").ToString()
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
#End Region

#Region "   คำขอ เครื่อง 2   "
    'เครื่องพิมพ์คำขอ สุวรรณภูมิ ST-003
    Public Shared Function Call_SiteReceipt_ST_003_02(ByVal Form_ID As String) As String
        Dim SendPrinterName As String
        Select Case Form_ID
            Case "FORM1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003_02Edi_receiptFORM1").ToString()
                Return SendPrinterName
            Case "FORM1_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003_02Edi_receiptFORM1_1").ToString()
                Return SendPrinterName
            Case "FORM1_2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003_02Edi_receiptFORM1_2").ToString()
                Return SendPrinterName
            Case "FORM1_3"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003_02Edi_receiptFORM1_3").ToString()
                Return SendPrinterName
            Case "FORM1_4"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003_02Edi_receiptFORM1_4").ToString()
                Return SendPrinterName
            Case "FORM2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003_02Edi_receiptFORM2").ToString()
                Return SendPrinterName
            Case "FORM2_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003_02Edi_receiptFORM2_1").ToString()
                Return SendPrinterName
            Case "FORM2_2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003_02Edi_receiptFORM2_2").ToString()
                Return SendPrinterName
            Case "FORM2_3"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003_02Edi_receiptFORM2_3").ToString()
                Return SendPrinterName
            Case "FORM2_4"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003_02Edi_receiptFORM2_4").ToString()
                Return SendPrinterName
            Case "FORM2_5"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003_02Edi_receiptFORM2_5").ToString()
                Return SendPrinterName
            Case "FORM2_6"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003_02Edi_receiptFORM2_6").ToString()
                Return SendPrinterName
            Case "FORM3"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003_02Edi_receiptFORM3").ToString()
                Return SendPrinterName
            Case "FORM3_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003_02Edi_receiptFORM3_1").ToString()
                Return SendPrinterName
                'by rut D new
                'FORM44_4	ฟอร์ม ดี (ATIGA) ใหม่ (7 ประเทศ)
                'FORM44_44	ฟอร์ม ดี (ATIGA) Attach Sheet รถยนต์ ใหม่ (7 ประเทศ)
                'FORM44_41	ฟอร์ม ดี AICO ใหม่ (7 ประเทศ)
                'FORM441_4	ฟอร์ม ดี AICO Attach Sheet รถยนต์ ใหม่ (7 ประเทศ)
            Case "FORM44_4", "FORM44_44", "FORM44_41", "FORM441_4", "FORM44_01", "FORM44_02"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003_02Edi_receiptFORM4").ToString()
                Return SendPrinterName
            Case "FORM4", "FORM44"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003_02Edi_receiptFORM4").ToString()
                Return SendPrinterName
            Case "FORM4_1", "FORM441"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003_02Edi_receiptFORM4_1").ToString()
                Return SendPrinterName
            Case "FORM4_2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003_02Edi_receiptFORM4_2").ToString()
                Return SendPrinterName
            Case "FORM4_3"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003_02Edi_receiptFORM4_3").ToString()
                Return SendPrinterName
            Case "FORM4_4"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003_02Edi_receiptFORM4_4").ToString()
                Return SendPrinterName
            Case "FORM4_5"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003_02Edi_receiptFORM4_5").ToString()
                Return SendPrinterName
            Case "FORM4_6", "FORM4_61"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003_02Edi_receiptFORM4_6").ToString()
                Return SendPrinterName
            Case "FORM4_8", "FORM4_81"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003_02Edi_receiptFORM4_8").ToString()
                Return SendPrinterName
            Case "FORM4_9"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003_02Edi_receiptFORM4_9").ToString()
                Return SendPrinterName
            Case "FORM4_91", "FORM4_911"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003_02Edi_receiptFORM4_91").ToString()
                Return SendPrinterName
            Case "FORM5"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003_02Edi_receiptFORM5").ToString()
                Return SendPrinterName
            Case "FORM5_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003_02Edi_receiptFORM5_1").ToString()
                Return SendPrinterName

                ''ByTine 14-10-2558 ฟอร์มไทย-ชิลี FORm5_2 
            Case "FORM5_2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003_02Edi_receiptFORM5_2").ToString()
                Return SendPrinterName

            Case "FORM6"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003_02Edi_receiptFORM6").ToString()
                Return SendPrinterName
            Case "FORM7"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003_02Edi_receiptFORM7").ToString()
                Return SendPrinterName
            Case "FORM8"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003_02Edi_receiptFORM8").ToString()
                Return SendPrinterName
            Case "FORM9"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003_02Edi_receiptFORM9").ToString()
                Return SendPrinterName
            Case "FORMRussia"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003_02Edi_receiptFORMRussia").ToString()
                Return SendPrinterName
        End Select
    End Function
#End Region

#Region "   คำขอ เครื่อง 3   "
    'เครื่องพิมพ์คำขอ สุวรรณภูมิ ST-003
    Public Shared Function Call_SiteReceipt_ST_003_03(ByVal Form_ID As String) As String
        Dim SendPrinterName As String
        Select Case Form_ID
            Case "FORM1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003_03Edi_receiptFORM1").ToString()
                Return SendPrinterName
            Case "FORM1_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003_03Edi_receiptFORM1_1").ToString()
                Return SendPrinterName
            Case "FORM1_2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003_03Edi_receiptFORM1_2").ToString()
                Return SendPrinterName
            Case "FORM1_3"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003_03Edi_receiptFORM1_3").ToString()
                Return SendPrinterName
            Case "FORM1_4"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003_03Edi_receiptFORM1_4").ToString()
                Return SendPrinterName
            Case "FORM2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003_03Edi_receiptFORM2").ToString()
                Return SendPrinterName
            Case "FORM2_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003_03Edi_receiptFORM2_1").ToString()
                Return SendPrinterName
            Case "FORM2_2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003_03Edi_receiptFORM2_2").ToString()
                Return SendPrinterName
            Case "FORM2_3"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003_03Edi_receiptFORM2_3").ToString()
                Return SendPrinterName
            Case "FORM2_4"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003_03Edi_receiptFORM2_4").ToString()
                Return SendPrinterName
            Case "FORM2_5"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003_03Edi_receiptFORM2_5").ToString()
                Return SendPrinterName
            Case "FORM2_6"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003_03Edi_receiptFORM2_6").ToString()
                Return SendPrinterName
            Case "FORM3"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003_03Edi_receiptFORM3").ToString()
                Return SendPrinterName
            Case "FORM3_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003_03Edi_receiptFORM3_1").ToString()
                Return SendPrinterName
                'by rut D new
                'FORM44_4	ฟอร์ม ดี (ATIGA) ใหม่ (7 ประเทศ)
                'FORM44_44	ฟอร์ม ดี (ATIGA) Attach Sheet รถยนต์ ใหม่ (7 ประเทศ)
                'FORM44_41	ฟอร์ม ดี AICO ใหม่ (7 ประเทศ)
                'FORM441_4	ฟอร์ม ดี AICO Attach Sheet รถยนต์ ใหม่ (7 ประเทศ)
            Case "FORM44_4", "FORM44_44", "FORM44_41", "FORM441_4", "FORM44_01", "FORM44_02"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003_03Edi_receiptFORM4").ToString()
                Return SendPrinterName
            Case "FORM4", "FORM44"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003_03Edi_receiptFORM4").ToString()
                Return SendPrinterName
            Case "FORM4_1", "FORM441"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003_03Edi_receiptFORM4_1").ToString()
                Return SendPrinterName
            Case "FORM4_2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003_03Edi_receiptFORM4_2").ToString()
                Return SendPrinterName
            Case "FORM4_3"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003_03Edi_receiptFORM4_3").ToString()
                Return SendPrinterName
            Case "FORM4_4"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003_03Edi_receiptFORM4_4").ToString()
                Return SendPrinterName
            Case "FORM4_5"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003_03Edi_receiptFORM4_5").ToString()
                Return SendPrinterName
            Case "FORM4_6", "FORM4_61"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003_03Edi_receiptFORM4_6").ToString()
                Return SendPrinterName
            Case "FORM4_8", "FORM4_81"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003_03Edi_receiptFORM4_8").ToString()
                Return SendPrinterName
            Case "FORM4_9"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003_03Edi_receiptFORM4_9").ToString()
                Return SendPrinterName
            Case "FORM4_91", "FORM4_911"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003_03Edi_receiptFORM4_91").ToString()
                Return SendPrinterName
            Case "FORM5"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003_03Edi_receiptFORM5").ToString()
                Return SendPrinterName
            Case "FORM5_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003_03Edi_receiptFORM5_1").ToString()
                Return SendPrinterName

                ''ByTine 14-10-2558 ฟอร์มไทย-ชิลี FORm5_2 
            Case "FORM5_2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003_03Edi_receiptFORM5_2").ToString()
                Return SendPrinterName

            Case "FORM6"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003_03Edi_receiptFORM6").ToString()
                Return SendPrinterName
            Case "FORM7"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003_03Edi_receiptFORM7").ToString()
                Return SendPrinterName
            Case "FORM8"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003_03Edi_receiptFORM8").ToString()
                Return SendPrinterName
            Case "FORM9"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003_03Edi_receiptFORM9").ToString()
                Return SendPrinterName
            Case "FORMRussia"
                SendPrinterName = ConfigurationManager.AppSettings("ST_003_03Edi_receiptFORMRussia").ToString()
                Return SendPrinterName
        End Select
    End Function
#End Region
#End Region

#Region "   Site ST-004 'กรมการค้าต่างประเทศ (สอ) Config Printer หนังสือรับรอง-คำขอ    "

#Region "   หนังสือรับรอง   "
    'หนังสือรับรอง ST-004 'กรมการค้าต่างประเทศ (สอ)
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
            Case "FORM2_5"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004Edi_FORM2_5").ToString()
                Return SendPrinterName
            Case "FORM2_6"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004Edi_FORM2_6").ToString()
                Return SendPrinterName
            Case "FORM3"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004Edi_FORM3").ToString()
                Return SendPrinterName
            Case "FORM3_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004Edi_FORM3_1").ToString()
                Return SendPrinterName
                'by rut D new
                'FORM44_4	ฟอร์ม ดี (ATIGA) ใหม่ (7 ประเทศ)
                'FORM44_44	ฟอร์ม ดี (ATIGA) Attach Sheet รถยนต์ ใหม่ (7 ประเทศ)
                'FORM44_41	ฟอร์ม ดี AICO ใหม่ (7 ประเทศ)
                'FORM441_4	ฟอร์ม ดี AICO Attach Sheet รถยนต์ ใหม่ (7 ประเทศ)
            Case "FORM44_4", "FORM44_44", "FORM44_41", "FORM441_4", "FORM44_01", "FORM44_02"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004Edi_FORM44ALL").ToString()
                Return SendPrinterName
            Case "FORM4"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004Edi_FORM4").ToString()
                Return SendPrinterName
            Case "FORM44"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004Edi_FORM44").ToString()
                Return SendPrinterName
            Case "FORM441"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004Edi_FORM441").ToString()
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
            Case "FORM4_6", "FORM4_61"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004Edi_FORM4_6").ToString()
                Return SendPrinterName
            Case "FORM4_8", "FORM4_81"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004Edi_FORM4_8").ToString()
                Return SendPrinterName
            Case "FORM4_9"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004Edi_FORM4_9").ToString()
                Return SendPrinterName
            Case "FORM4_91", "FORM4_911"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004Edi_FORM4_91").ToString()
                Return SendPrinterName
            Case "FORM5"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004Edi_FORM5").ToString()
                Return SendPrinterName
            Case "FORM5_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004Edi_FORM5_1").ToString()
                Return SendPrinterName

                ''ByTine 14-10-2558 ฟอร์มไทย-ชิลี FORm5_2 
            Case "FORM5_2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004Edi_FORM5_2").ToString()
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

    'DS
    Public Shared Function Call_SitePrintsDS_ST_004(ByVal Form_ID As String) As String
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
            Case "FORM2_5"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004Edi_FORM2_5").ToString()
                Return SendPrinterName
            Case "FORM2_6"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004Edi_FORM2_6").ToString()
                Return SendPrinterName
            Case "FORM3"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004Edi_FORM3").ToString()
                Return SendPrinterName
            Case "FORM3_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004Edi_FORM3_1").ToString()
                Return SendPrinterName
                'by rut D new
                'FORM44_4	ฟอร์ม ดี (ATIGA) ใหม่ (7 ประเทศ)
                'FORM44_44	ฟอร์ม ดี (ATIGA) Attach Sheet รถยนต์ ใหม่ (7 ประเทศ)
                'FORM44_41	ฟอร์ม ดี AICO ใหม่ (7 ประเทศ)
                'FORM441_4	ฟอร์ม ดี AICO Attach Sheet รถยนต์ ใหม่ (7 ประเทศ)
            Case "FORM44_4", "FORM44_44", "FORM44_41", "FORM441_4", "FORM44_01", "FORM44_02"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004DSEdi_FORM44ALL").ToString()
                Return SendPrinterName
            Case "FORM4"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004DSEdi_FORM4").ToString()
                Return SendPrinterName
            Case "FORM44"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004DSEdi_FORM44").ToString()
                Return SendPrinterName
            Case "FORM441"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004DSEdi_FORM441").ToString()
                Return SendPrinterName
            Case "FORM4_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004DSEdi_FORM4_1").ToString()
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
            Case "FORM4_6", "FORM4_61"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004Edi_FORM4_6").ToString()
                Return SendPrinterName
            Case "FORM4_8", "FORM4_81"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004Edi_FORM4_8").ToString()
                Return SendPrinterName
            Case "FORM4_9"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004Edi_FORM4_9").ToString()
                Return SendPrinterName
            Case "FORM4_91", "FORM4_911"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004DSEdi_FORM4_91").ToString()
                'SendPrinterName = ConfigurationManager.AppSettings("ST_004Edi_FORM4_91").ToString()
                Return SendPrinterName
            Case "FORM5"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004Edi_FORM5").ToString()
                Return SendPrinterName
            Case "FORM5_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004Edi_FORM5_1").ToString()
                Return SendPrinterName

                ''ByTine 14-10-2558 ฟอร์มไทย-ชิลี FORm5_2 
            Case "FORM5_2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004Edi_FORM5_2").ToString()
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
#End Region

#Region "   คำขอ เครื่อง 1   "
    'เครื่องพิมพ์คำขอ กรมการค้าต่างประเทศ (สอ) ST-004
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
            Case "FORM2_5"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004_01Edi_receiptFORM2_5").ToString()
                Return SendPrinterName
            Case "FORM2_6"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004_01Edi_receiptFORM2_6").ToString()
                Return SendPrinterName
            Case "FORM3"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004_01Edi_receiptFORM3").ToString()
                Return SendPrinterName
            Case "FORM3_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004_01Edi_receiptFORM3_1").ToString()
                Return SendPrinterName
                'by rut D new
                'FORM44_4	ฟอร์ม ดี (ATIGA) ใหม่ (7 ประเทศ)
                'FORM44_44	ฟอร์ม ดี (ATIGA) Attach Sheet รถยนต์ ใหม่ (7 ประเทศ)
                'FORM44_41	ฟอร์ม ดี AICO ใหม่ (7 ประเทศ)
                'FORM441_4	ฟอร์ม ดี AICO Attach Sheet รถยนต์ ใหม่ (7 ประเทศ)
            Case "FORM44_4", "FORM44_44", "FORM44_41", "FORM441_4", "FORM44_01", "FORM44_02"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004_01Edi_receiptFORM4").ToString()
                Return SendPrinterName
            Case "FORM4"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004_01Edi_receiptFORM4").ToString()
                Return SendPrinterName
            Case "FORM44"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004_01Edi_receiptFORM44").ToString()
                Return SendPrinterName
            Case "FORM441"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004_01Edi_receiptFORM441").ToString()
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
            Case "FORM4_6", "FORM4_61"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004_01Edi_receiptFORM4_6").ToString()
                Return SendPrinterName
            Case "FORM4_8", "FORM4_81"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004_01Edi_receiptFORM4_8").ToString()
                Return SendPrinterName
            Case "FORM4_9"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004_01Edi_receiptFORM4_9").ToString()
                Return SendPrinterName
            Case "FORM4_91", "FORM4_911"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004_01Edi_receiptFORM4_91").ToString()
                Return SendPrinterName
            Case "FORM5"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004_01Edi_receiptFORM5").ToString()
                Return SendPrinterName
            Case "FORM5_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004_01Edi_receiptFORM5_1").ToString()
                Return SendPrinterName

                ''ByTine 14-10-2558 ฟอร์มไทย-ชิลี FORm5_2 
            Case "FORM5_2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004_01Edi_receiptFORM5_2").ToString()
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

    'เครื่องพิมพ์คำขอ DS กรมการค้าต่างประเทศ (สอ) ST-004
    Public Shared Function Call_SiteReceipt_ST_004_01DS(ByVal Form_ID As String) As String
        Dim SendPrinterName As String
        Select Case Form_ID
            Case "FORM1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004_01DSEdi_receiptFORM1").ToString()
                Return SendPrinterName
            Case "FORM1_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004_01DSEdi_receiptFORM1_1").ToString()
                Return SendPrinterName
            Case "FORM1_2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004_01DSEdi_receiptFORM1_2").ToString()
                Return SendPrinterName
            Case "FORM1_3"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004_01DSEdi_receiptFORM1_3").ToString()
                Return SendPrinterName
            Case "FORM1_4"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004_01DSEdi_receiptFORM1_4").ToString()
                Return SendPrinterName
            Case "FORM2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004_01DSEdi_receiptFORM2").ToString()
                Return SendPrinterName
            Case "FORM2_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004_01DSEdi_receiptFORM2_1").ToString()
                Return SendPrinterName
            Case "FORM2_2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004_01DSEdi_receiptFORM2_2").ToString()
                Return SendPrinterName
            Case "FORM2_3"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004_01DSEdi_receiptFORM2_3").ToString()
                Return SendPrinterName
            Case "FORM2_4"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004_01DSEdi_receiptFORM2_4").ToString()
                Return SendPrinterName
            Case "FORM3"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004_01DSEdi_receiptFORM3").ToString()
                Return SendPrinterName
            Case "FORM3_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004_01DSEdi_receiptFORM3_1").ToString()
                Return SendPrinterName
                'by rut D new
                'FORM44_4	ฟอร์ม ดี (ATIGA) ใหม่ (7 ประเทศ)
                'FORM44_44	ฟอร์ม ดี (ATIGA) Attach Sheet รถยนต์ ใหม่ (7 ประเทศ)
                'FORM44_41	ฟอร์ม ดี AICO ใหม่ (7 ประเทศ)
                'FORM441_4	ฟอร์ม ดี AICO Attach Sheet รถยนต์ ใหม่ (7 ประเทศ)
            Case "FORM44_4", "FORM44_44", "FORM44_41", "FORM441_4", "FORM44_01", "FORM44_02"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004_01DSEdi_receiptFORM4").ToString()
                Return SendPrinterName
            Case "FORM4"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004_01DSEdi_receiptFORM4").ToString()
                Return SendPrinterName
            Case "FORM44"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004_01DSEdi_receiptFORM44").ToString()
                Return SendPrinterName
            Case "FORM441"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004_01DSEdi_receiptFORM441").ToString()
                Return SendPrinterName
            Case "FORM4_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004_01DSEdi_receiptFORM4_1").ToString()
                Return SendPrinterName

            Case "FORM4_2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004_01DSEdi_receiptFORM4_2").ToString()
                Return SendPrinterName
            Case "FORM4_3"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004_01DSEdi_receiptFORM4_3").ToString()
                Return SendPrinterName
            Case "FORM4_4"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004_01DSEdi_receiptFORM4_4").ToString()
                Return SendPrinterName
            Case "FORM4_5"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004_01DSEdi_receiptFORM4_5").ToString()
                Return SendPrinterName
            Case "FORM4_6", "FORM4_61"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004_01DSEdi_receiptFORM4_6").ToString()
                Return SendPrinterName
            Case "FORM4_8", "FORM4_81"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004_01DSEdi_receiptFORM4_8").ToString()
                Return SendPrinterName
            Case "FORM4_9"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004_01DSEdi_receiptFORM4_9").ToString()
                Return SendPrinterName
            Case "FORM4_91", "FORM4_911"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004_01DSEdi_receiptFORM4_91").ToString()
                Return SendPrinterName
            Case "FORM5"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004_01DSEdi_receiptFORM5").ToString()
                Return SendPrinterName
            Case "FORM5_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004_01DSEdi_receiptFORM5_1").ToString()
                Return SendPrinterName

                ''ByTine 14-10-2558 ฟอร์มไทย-ชิลี FORm5_2 
            Case "FORM5_2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004_01DSEdi_receiptFORM5_2").ToString()
                Return SendPrinterName

            Case "FORM6"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004_01DSEdi_receiptFORM6").ToString()
                Return SendPrinterName
            Case "FORM7"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004_01DSEdi_receiptFORM7").ToString()
                Return SendPrinterName
            Case "FORM8"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004_01DSEdi_receiptFORM8").ToString()
                Return SendPrinterName
            Case "FORM9"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004_01DSEdi_receiptFORM9").ToString()
                Return SendPrinterName
            Case "FORMRussia"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004_01DSEdi_receiptFORMRussia").ToString()
                Return SendPrinterName
        End Select
    End Function
#End Region

    'by rut 04-08-2558
#Region "   คำขอ เครื่อง 2   "
    'เครื่องพิมพ์คำขอ กรมการค้าต่างประเทศ (สอ) ST-004
    Public Shared Function Call_SiteReceipt_ST_004_02(ByVal Form_ID As String) As String
        Dim SendPrinterName As String
        Select Case Form_ID
            Case "FORM1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004_02Edi_receiptFORM1").ToString()
                Return SendPrinterName
            Case "FORM1_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004_02Edi_receiptFORM1_1").ToString()
                Return SendPrinterName
            Case "FORM1_2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004_02Edi_receiptFORM1_2").ToString()
                Return SendPrinterName
            Case "FORM1_3"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004_02Edi_receiptFORM1_3").ToString()
                Return SendPrinterName
            Case "FORM1_4"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004_02Edi_receiptFORM1_4").ToString()
                Return SendPrinterName
            Case "FORM2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004_02Edi_receiptFORM2").ToString()
                Return SendPrinterName
            Case "FORM2_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004_02Edi_receiptFORM2_1").ToString()
                Return SendPrinterName
            Case "FORM2_2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004_02Edi_receiptFORM2_2").ToString()
                Return SendPrinterName
            Case "FORM2_3"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004_02Edi_receiptFORM2_3").ToString()
                Return SendPrinterName
            Case "FORM2_4"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004_02Edi_receiptFORM2_4").ToString()
                Return SendPrinterName
            Case "FORM2_5"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004_02Edi_receiptFORM2_5").ToString()
                Return SendPrinterName
            Case "FORM2_6"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004_02Edi_receiptFORM2_6").ToString()
                Return SendPrinterName
            Case "FORM3"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004_02Edi_receiptFORM3").ToString()
                Return SendPrinterName
            Case "FORM3_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004_02Edi_receiptFORM3_1").ToString()
                Return SendPrinterName
                'by rut D new
                'FORM44_4	ฟอร์ม ดี (ATIGA) ใหม่ (7 ประเทศ)
                'FORM44_44	ฟอร์ม ดี (ATIGA) Attach Sheet รถยนต์ ใหม่ (7 ประเทศ)
                'FORM44_41	ฟอร์ม ดี AICO ใหม่ (7 ประเทศ)
                'FORM441_4	ฟอร์ม ดี AICO Attach Sheet รถยนต์ ใหม่ (7 ประเทศ)
            Case "FORM44_4", "FORM44_44", "FORM44_41", "FORM441_4", "FORM44_01", "FORM44_02"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004_02Edi_receiptFORM4").ToString()
                Return SendPrinterName
            Case "FORM4"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004_02Edi_receiptFORM4").ToString()
                Return SendPrinterName
            Case "FORM44"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004_02Edi_receiptFORM44").ToString()
                Return SendPrinterName
            Case "FORM441"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004_02Edi_receiptFORM441").ToString()
                Return SendPrinterName
            Case "FORM4_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004_02Edi_receiptFORM4_1").ToString()
                Return SendPrinterName

            Case "FORM4_2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004_02Edi_receiptFORM4_2").ToString()
                Return SendPrinterName
            Case "FORM4_3"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004_02Edi_receiptFORM4_3").ToString()
                Return SendPrinterName
            Case "FORM4_4"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004_02Edi_receiptFORM4_4").ToString()
                Return SendPrinterName
            Case "FORM4_5"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004_02Edi_receiptFORM4_5").ToString()
                Return SendPrinterName
            Case "FORM4_6", "FORM4_61"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004_02Edi_receiptFORM4_6").ToString()
                Return SendPrinterName
            Case "FORM4_8", "FORM4_81"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004_02Edi_receiptFORM4_8").ToString()
                Return SendPrinterName
            Case "FORM4_9"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004_02Edi_receiptFORM4_9").ToString()
                Return SendPrinterName
            Case "FORM4_91", "FORM4_911"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004_02Edi_receiptFORM4_91").ToString()
                Return SendPrinterName
            Case "FORM5"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004_02Edi_receiptFORM5").ToString()
                Return SendPrinterName
            Case "FORM5_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004_02Edi_receiptFORM5_1").ToString()
                Return SendPrinterName

                ''ByTine 14-10-2558 ฟอร์มไทย-ชิลี FORm5_2 
            Case "FORM5_2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004_02Edi_receiptFORM5_2").ToString()
                Return SendPrinterName

            Case "FORM6"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004_02Edi_receiptFORM6").ToString()
                Return SendPrinterName
            Case "FORM7"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004_02Edi_receiptFORM7").ToString()
                Return SendPrinterName
            Case "FORM8"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004_02Edi_receiptFORM8").ToString()
                Return SendPrinterName
            Case "FORM9"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004_02Edi_receiptFORM9").ToString()
                Return SendPrinterName
            Case "FORMRussia"
                SendPrinterName = ConfigurationManager.AppSettings("ST_004_02Edi_receiptFORMRussia").ToString()
                Return SendPrinterName
        End Select
    End Function
#End Region
#End Region

#Region "   Site ST-005 'กรมการค้าต่างประเทศ (ตลาดไท) Config Printer หนังสือรับรอง-คำขอ    "

#Region "   หนังสือรับรอง   "
    'หนังสือรับรอง ST-005 'กรมการค้าต่างประเทศ (ตลาดไท)
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
            Case "FORM2_5"
                SendPrinterName = ConfigurationManager.AppSettings("ST_005Edi_FORM2_5").ToString()
                Return SendPrinterName
            Case "FORM2_6"
                SendPrinterName = ConfigurationManager.AppSettings("ST_005Edi_FORM2_6").ToString()
                Return SendPrinterName
            Case "FORM3"
                SendPrinterName = ConfigurationManager.AppSettings("ST_005Edi_FORM3").ToString()
                Return SendPrinterName
            Case "FORM3_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_005Edi_FORM3_1").ToString()
                Return SendPrinterName

                'by rut D new
                'FORM44_4	ฟอร์ม ดี (ATIGA) ใหม่ (7 ประเทศ)
                'FORM44_44	ฟอร์ม ดี (ATIGA) Attach Sheet รถยนต์ ใหม่ (7 ประเทศ)
                'FORM44_41	ฟอร์ม ดี AICO ใหม่ (7 ประเทศ)
                'FORM441_4	ฟอร์ม ดี AICO Attach Sheet รถยนต์ ใหม่ (7 ประเทศ)
            Case "FORM44_4", "FORM44_44", "FORM44_41", "FORM441_4", "FORM44_01", "FORM44_02"
                SendPrinterName = ConfigurationManager.AppSettings("ST_005Edi_FORM4").ToString()
                Return SendPrinterName
            Case "FORM4", "FORM44"
                SendPrinterName = ConfigurationManager.AppSettings("ST_005Edi_FORM4").ToString()
                Return SendPrinterName
            Case "FORM4_1", "FORM441"
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
            Case "FORM4_6", "FORM4_61"
                SendPrinterName = ConfigurationManager.AppSettings("ST_005Edi_FORM4_6").ToString()
                Return SendPrinterName
            Case "FORM4_8", "FORM4_81"
                SendPrinterName = ConfigurationManager.AppSettings("ST_005Edi_FORM4_8").ToString()
                Return SendPrinterName
            Case "FORM4_9"
                SendPrinterName = ConfigurationManager.AppSettings("ST_005Edi_FORM4_9").ToString()
                Return SendPrinterName
            Case "FORM4_91", "FORM4_911"
                SendPrinterName = ConfigurationManager.AppSettings("ST_005Edi_FORM4_91").ToString()
                Return SendPrinterName
            Case "FORM5"
                SendPrinterName = ConfigurationManager.AppSettings("ST_005Edi_FORM5").ToString()
                Return SendPrinterName
            Case "FORM5_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_005Edi_FORM5_1").ToString()
                Return SendPrinterName

                ''ByTine 14-10-2558 ฟอร์มไทย-ชิลี FORm5_2 
            Case "FORM5_2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_005Edi_FORM5_2").ToString()
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
#End Region

#Region "   คำขอ เครื่อง 1   "
    'เครื่องพิมพ์คำขอ กรมการค้าต่างประเทศ (ตลาดไท) ST-005
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
            Case "FORM2_5"
                SendPrinterName = ConfigurationManager.AppSettings("ST_005_01Edi_receiptFORM2_5").ToString()
                Return SendPrinterName
            Case "FORM2_6"
                SendPrinterName = ConfigurationManager.AppSettings("ST_005_01Edi_receiptFORM2_6").ToString()
                Return SendPrinterName
            Case "FORM3"
                SendPrinterName = ConfigurationManager.AppSettings("ST_005_01Edi_receiptFORM3").ToString()
                Return SendPrinterName
            Case "FORM3_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_005_01Edi_receiptFORM3_1").ToString()
                Return SendPrinterName
                'by rut D new
                'FORM44_4	ฟอร์ม ดี (ATIGA) ใหม่ (7 ประเทศ)
                'FORM44_44	ฟอร์ม ดี (ATIGA) Attach Sheet รถยนต์ ใหม่ (7 ประเทศ)
                'FORM44_41	ฟอร์ม ดี AICO ใหม่ (7 ประเทศ)
                'FORM441_4	ฟอร์ม ดี AICO Attach Sheet รถยนต์ ใหม่ (7 ประเทศ)
            Case "FORM44_4", "FORM44_44", "FORM44_41", "FORM441_4", "FORM44_01", "FORM44_02"
                SendPrinterName = ConfigurationManager.AppSettings("ST_005_01Edi_receiptFORM4").ToString()
                Return SendPrinterName
            Case "FORM4", "FORM44"
                SendPrinterName = ConfigurationManager.AppSettings("ST_005_01Edi_receiptFORM4").ToString()
                Return SendPrinterName
            Case "FORM4_1", "FORM441"
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
            Case "FORM4_6", "FORM4_61"
                SendPrinterName = ConfigurationManager.AppSettings("ST_005_01Edi_receiptFORM4_6").ToString()
                Return SendPrinterName
            Case "FORM4_8", "FORM4_81"
                SendPrinterName = ConfigurationManager.AppSettings("ST_005_01Edi_receiptFORM4_8").ToString()
                Return SendPrinterName
            Case "FORM4_9"
                SendPrinterName = ConfigurationManager.AppSettings("ST_005_01Edi_receiptFORM4_9").ToString()
                Return SendPrinterName
            Case "FORM4_91", "FORM4_911"
                SendPrinterName = ConfigurationManager.AppSettings("ST_005_01Edi_receiptFORM4_91").ToString()
                Return SendPrinterName
            Case "FORM5"
                SendPrinterName = ConfigurationManager.AppSettings("ST_005_01Edi_receiptFORM5").ToString()
                Return SendPrinterName
            Case "FORM5_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_005_01Edi_receiptFORM5_1").ToString()
                Return SendPrinterName

                ''ByTine 14-10-2558 ฟอร์มไทย-ชิลี FORm5_2 
            Case "FORM5_2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_005_01Edi_receiptFORM5_2").ToString()
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
#End Region

#End Region

#Region "   Site CB-003 'สคต.เขต3 (ชลบุรี) Config Printer หนังสือรับรอง-คำขอ    "

#Region "   หนังสือรับรอง   "
    'หนังสือรับรอง CB-003 'สคต.เขต3 (ชลบุรี)
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
            Case "FORM2_5"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003Edi_FORM2_5").ToString()
                Return SendPrinterName
            Case "FORM2_6"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003Edi_FORM2_6").ToString()
                Return SendPrinterName
            Case "FORM3"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003Edi_FORM3").ToString()
                Return SendPrinterName
            Case "FORM3_1"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003Edi_FORM3_1").ToString()
                Return SendPrinterName
                'by rut D new
                'FORM44_4	ฟอร์ม ดี (ATIGA) ใหม่ (7 ประเทศ)
                'FORM44_44	ฟอร์ม ดี (ATIGA) Attach Sheet รถยนต์ ใหม่ (7 ประเทศ)
                'FORM44_41	ฟอร์ม ดี AICO ใหม่ (7 ประเทศ)
                'FORM441_4	ฟอร์ม ดี AICO Attach Sheet รถยนต์ ใหม่ (7 ประเทศ)
            Case "FORM44_4", "FORM44_44", "FORM44_41", "FORM441_4", "FORM44_01", "FORM44_02"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003Edi_FORM44ALL").ToString()
                Return SendPrinterName
            Case "FORM4", "FORM44"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003Edi_FORM4").ToString()
                Return SendPrinterName
            Case "FORM4_1", "FORM441"
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
            Case "FORM4_6", "FORM4_61"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003Edi_FORM4_6").ToString()
                Return SendPrinterName
            Case "FORM4_8", "FORM4_81"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003Edi_FORM4_8").ToString()
                Return SendPrinterName
            Case "FORM4_9"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003Edi_FORM4_9").ToString()
                Return SendPrinterName
            Case "FORM4_91", "FORM4_911"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003Edi_FORM4_91").ToString()
                Return SendPrinterName
            Case "FORM5"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003Edi_FORM5").ToString()
                Return SendPrinterName
            Case "FORM5_1"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003Edi_FORM5_1").ToString()
                Return SendPrinterName

                ''ByTine 14-10-2558 ฟอร์มไทย-ชิลี FORm5_2 
            Case "FORM5_2"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003Edi_FORM5_2").ToString()
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

    Public Shared Function Call_SitePrints_CB_003DS(ByVal Form_ID As String) As String
        Dim SendPrinterName As String
        Select Case Form_ID
            Case "FORM1"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003DSEdi_FORM1").ToString()
                Return SendPrinterName
            Case "FORM1_1"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003DSEdi_FORM1_1").ToString()
                Return SendPrinterName
            Case "FORM1_2"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003DSEdi_FORM1_2").ToString()
                Return SendPrinterName
            Case "FORM1_3"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003DSEdi_FORM1_3").ToString()
                Return SendPrinterName
            Case "FORM1_4"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003DSEdi_FORM1_4").ToString()
                Return SendPrinterName
            Case "FORM2"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003DSEdi_FORM2").ToString()
                Return SendPrinterName
            Case "FORM2_1"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003DSEdi_FORM2_1").ToString()
                Return SendPrinterName
            Case "FORM2_2"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003DSEdi_FORM2_2").ToString()
                Return SendPrinterName
            Case "FORM2_3"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003DSEdi_FORM2_3").ToString()
                Return SendPrinterName
            Case "FORM2_4"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003DSEdi_FORM2_4").ToString()
                Return SendPrinterName
            Case "FORM2_5"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003DSEdi_FORM2_5").ToString()
                Return SendPrinterName
            Case "FORM2_6"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003DSEdi_FORM2_6").ToString()
                Return SendPrinterName
            Case "FORM3"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003DSEdi_FORM3").ToString()
                Return SendPrinterName
            Case "FORM3_1"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003DSEdi_FORM3_1").ToString()
                Return SendPrinterName
                'by rut D new
                'FORM44_4	ฟอร์ม ดี (ATIGA) ใหม่ (7 ประเทศ)
                'FORM44_44	ฟอร์ม ดี (ATIGA) Attach Sheet รถยนต์ ใหม่ (7 ประเทศ)
                'FORM44_41	ฟอร์ม ดี AICO ใหม่ (7 ประเทศ)
                'FORM441_4	ฟอร์ม ดี AICO Attach Sheet รถยนต์ ใหม่ (7 ประเทศ)
            Case "FORM44_4", "FORM44_44", "FORM44_41", "FORM441_4", "FORM44_01", "FORM44_02"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003DSEdi_FORM44ALL").ToString()
                Return SendPrinterName
            Case "FORM4", "FORM44"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003DSEdi_FORM4").ToString()
                Return SendPrinterName
            Case "FORM4_1", "FORM441"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003DSEdi_FORM4_1").ToString()
                Return SendPrinterName

            Case "FORM4_2"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003DSEdi_FORM4_2").ToString()
                Return SendPrinterName
            Case "FORM4_3"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003DSEdi_FORM4_3").ToString()
                Return SendPrinterName
            Case "FORM4_4"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003DSEdi_FORM4_4").ToString()
                Return SendPrinterName
            Case "FORM4_5"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003DSEdi_FORM4_5").ToString()
                Return SendPrinterName
            Case "FORM4_6", "FORM4_61"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003DSEdi_FORM4_6").ToString()
                Return SendPrinterName
            Case "FORM4_8", "FORM4_81"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003DSEdi_FORM4_8").ToString()
                Return SendPrinterName
            Case "FORM4_9"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003DSEdi_FORM4_9").ToString()
                Return SendPrinterName
            Case "FORM4_91", "FORM4_911"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003DSEdi_FORM4_91").ToString()
                Return SendPrinterName
            Case "FORM5"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003DSEdi_FORM5").ToString()
                Return SendPrinterName
            Case "FORM5_1"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003DSEdi_FORM5_1").ToString()
                Return SendPrinterName

                ''ByTine 14-10-2558 ฟอร์มไทย-ชิลี FORm5_2 
            Case "FORM5_2"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003DSEdi_FORM5_2").ToString()
                Return SendPrinterName

            Case "FORM6"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003DSEdi_FORM6").ToString()
                Return SendPrinterName
            Case "FORM7"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003DSEdi_FORM7").ToString()
                Return SendPrinterName
            Case "FORM8"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003DSEdi_FORM8").ToString()
                Return SendPrinterName
            Case "FORM9"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003DSEdi_FORM9").ToString()
                Return SendPrinterName
            Case "FORMRussia"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003DSEdi_FORMRussia").ToString()
                Return SendPrinterName

        End Select
    End Function
#End Region

#Region "   คำขอ เครื่อง 1   "
    'เครื่องพิมพ์คำขอ สคต.เขต3 (ชลบุรี) CB-003
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
            Case "FORM2_5"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003_01Edi_receiptFORM2_5").ToString()
                Return SendPrinterName
            Case "FORM2_6"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003_01Edi_receiptFORM2_6").ToString()
                Return SendPrinterName
            Case "FORM3"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003_01Edi_receiptFORM3").ToString()
                Return SendPrinterName
            Case "FORM3_1"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003_01Edi_receiptFORM3_1").ToString()
                Return SendPrinterName
                'by rut D new
                'FORM44_4	ฟอร์ม ดี (ATIGA) ใหม่ (7 ประเทศ)
                'FORM44_44	ฟอร์ม ดี (ATIGA) Attach Sheet รถยนต์ ใหม่ (7 ประเทศ)
                'FORM44_41	ฟอร์ม ดี AICO ใหม่ (7 ประเทศ)
                'FORM441_4	ฟอร์ม ดี AICO Attach Sheet รถยนต์ ใหม่ (7 ประเทศ)
            Case "FORM44_4", "FORM44_44", "FORM44_41", "FORM441_4", "FORM44_01", "FORM44_02"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003_01Edi_receiptFORM4").ToString()
                Return SendPrinterName
            Case "FORM4", "FORM44"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003_01Edi_receiptFORM4").ToString()
                Return SendPrinterName
            Case "FORM4_1", "FORM441"
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
            Case "FORM4_6", "FORM4_61"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003_01Edi_receiptFORM4_6").ToString()
                Return SendPrinterName
            Case "FORM4_8", "FORM4_81"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003_01Edi_receiptFORM4_8").ToString()
                Return SendPrinterName
            Case "FORM4_9"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003_01Edi_receiptFORM4_9").ToString()
                Return SendPrinterName
            Case "FORM4_91", "FORM4_911"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003_01Edi_receiptFORM4_91").ToString()
                Return SendPrinterName
            Case "FORM5"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003_01Edi_receiptFORM5").ToString()
                Return SendPrinterName
            Case "FORM5_1"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003_01Edi_receiptFORM5_1").ToString()
                Return SendPrinterName

                ''ByTine 14-10-2558 ฟอร์มไทย-ชิลี FORm5_2 
            Case "FORM5_2"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003_01Edi_receiptFORM5_2").ToString()
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

#End Region

#Region "   คำขอ เครื่อง 2   "
    'เครื่องพิมพ์คำขอ สคต.เขต3 (ชลบุรี) CB-003
    Public Shared Function Call_SiteReceipt_CB_003_02(ByVal Form_ID As String) As String
        Dim SendPrinterName As String
        Select Case Form_ID
            Case "FORM1"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003_02Edi_receiptFORM1").ToString()
                Return SendPrinterName
            Case "FORM1_1"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003_02Edi_receiptFORM1_1").ToString()
                Return SendPrinterName
            Case "FORM1_2"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003_02Edi_receiptFORM1_2").ToString()
                Return SendPrinterName
            Case "FORM1_3"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003_02Edi_receiptFORM1_3").ToString()
                Return SendPrinterName
            Case "FORM1_4"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003_02Edi_receiptFORM1_4").ToString()
                Return SendPrinterName
            Case "FORM2"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003_02Edi_receiptFORM2").ToString()
                Return SendPrinterName
            Case "FORM2_1"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003_02Edi_receiptFORM2_1").ToString()
                Return SendPrinterName
            Case "FORM2_2"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003_02Edi_receiptFORM2_2").ToString()
                Return SendPrinterName
            Case "FORM2_3"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003_02Edi_receiptFORM2_3").ToString()
                Return SendPrinterName
            Case "FORM2_4"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003_02Edi_receiptFORM2_4").ToString()
                Return SendPrinterName
            Case "FORM2_5"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003_02Edi_receiptFORM2_5").ToString()
                Return SendPrinterName
            Case "FORM2_6"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003_02Edi_receiptFORM2_6").ToString()
                Return SendPrinterName
            Case "FORM3"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003_02Edi_receiptFORM3").ToString()
                Return SendPrinterName
            Case "FORM3_1"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003_02Edi_receiptFORM3_1").ToString()
                Return SendPrinterName
                'by rut D new
                'FORM44_4	ฟอร์ม ดี (ATIGA) ใหม่ (7 ประเทศ)
                'FORM44_44	ฟอร์ม ดี (ATIGA) Attach Sheet รถยนต์ ใหม่ (7 ประเทศ)
                'FORM44_41	ฟอร์ม ดี AICO ใหม่ (7 ประเทศ)
                'FORM441_4	ฟอร์ม ดี AICO Attach Sheet รถยนต์ ใหม่ (7 ประเทศ)
            Case "FORM44_4", "FORM44_44", "FORM44_41", "FORM441_4"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003_02Edi_receiptFORM4").ToString()
                Return SendPrinterName
            Case "FORM4", "FORM44"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003_02Edi_receiptFORM4").ToString()
                Return SendPrinterName
            Case "FORM4_1", "FORM441"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003_02Edi_receiptFORM4_1").ToString()
                Return SendPrinterName

            Case "FORM4_2"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003_02Edi_receiptFORM4_2").ToString()
                Return SendPrinterName
            Case "FORM4_3"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003_02Edi_receiptFORM4_3").ToString()
                Return SendPrinterName
            Case "FORM4_4"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003_02Edi_receiptFORM4_4").ToString()
                Return SendPrinterName
            Case "FORM4_5"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003_02Edi_receiptFORM4_5").ToString()
                Return SendPrinterName
            Case "FORM4_6", "FORM4_61"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003_02Edi_receiptFORM4_6").ToString()
                Return SendPrinterName
            Case "FORM4_8", "FORM4_81"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003_02Edi_receiptFORM4_8").ToString()
                Return SendPrinterName
            Case "FORM4_9"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003_02Edi_receiptFORM4_9").ToString()
                Return SendPrinterName
            Case "FORM4_91", "FORM4_911"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003_02Edi_receiptFORM4_91").ToString()
                Return SendPrinterName
            Case "FORM5"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003_02Edi_receiptFORM5").ToString()
                Return SendPrinterName
            Case "FORM5_1"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003_02Edi_receiptFORM5_1").ToString()
                Return SendPrinterName

                ''ByTine 14-10-2558 ฟอร์มไทย-ชิลี FORm5_2 
            Case "FORM5_2"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003_02Edi_receiptFORM5_2").ToString()
                Return SendPrinterName

            Case "FORM6"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003_02Edi_receiptFORM6").ToString()
                Return SendPrinterName
            Case "FORM7"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003_02Edi_receiptFORM7").ToString()
                Return SendPrinterName
            Case "FORM8"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003_02Edi_receiptFORM8").ToString()
                Return SendPrinterName
            Case "FORM9"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003_02Edi_receiptFORM9").ToString()
                Return SendPrinterName
            Case "FORMRussia"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003_02Edi_receiptFORMRussia").ToString()
                Return SendPrinterName
        End Select
    End Function

#End Region

#Region "   คำขอ เครื่อง 3 DS   "
    'เครื่องพิมพ์คำขอ สคต.เขต3 (ชลบุรี) CB-003
    Public Shared Function Call_SiteReceipt_CB_003_03(ByVal Form_ID As String) As String
        Dim SendPrinterName As String
        Select Case Form_ID
            Case "FORM1"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003_03DSEdi_receiptFORM1").ToString()
                Return SendPrinterName
            Case "FORM1_1"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003_03DSEdi_receiptFORM1_1").ToString()
                Return SendPrinterName
            Case "FORM1_2"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003_03DSEdi_receiptFORM1_2").ToString()
                Return SendPrinterName
            Case "FORM1_3"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003_03DSEdi_receiptFORM1_3").ToString()
                Return SendPrinterName
            Case "FORM1_4"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003_03DSEdi_receiptFORM1_4").ToString()
                Return SendPrinterName
            Case "FORM2"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003_03DSEdi_receiptFORM2").ToString()
                Return SendPrinterName
            Case "FORM2_1"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003_03DSEdi_receiptFORM2_1").ToString()
                Return SendPrinterName
            Case "FORM2_2"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003_03DSEdi_receiptFORM2_2").ToString()
                Return SendPrinterName
            Case "FORM2_3"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003_03DSEdi_receiptFORM2_3").ToString()
                Return SendPrinterName
            Case "FORM2_4"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003_03DSEdi_receiptFORM2_4").ToString()
                Return SendPrinterName
            Case "FORM2_5"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003_03DSEdi_receiptFORM2_5").ToString()
                Return SendPrinterName
            Case "FORM2_6"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003_03DSEdi_receiptFORM2_6").ToString()
                Return SendPrinterName
            Case "FORM3"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003_03DSEdi_receiptFORM3").ToString()
                Return SendPrinterName
            Case "FORM3_1"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003_03DSEdi_receiptFORM3_1").ToString()
                Return SendPrinterName
                'by rut D new
                'FORM44_4	ฟอร์ม ดี (ATIGA) ใหม่ (7 ประเทศ)
                'FORM44_44	ฟอร์ม ดี (ATIGA) Attach Sheet รถยนต์ ใหม่ (7 ประเทศ)
                'FORM44_41	ฟอร์ม ดี AICO ใหม่ (7 ประเทศ)
                'FORM441_4	ฟอร์ม ดี AICO Attach Sheet รถยนต์ ใหม่ (7 ประเทศ)
            Case "FORM44_4", "FORM44_44", "FORM44_41", "FORM441_4", "FORM44_01", "FORM44_02"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003_03DSEdi_receiptFORM4").ToString()
                Return SendPrinterName
            Case "FORM4", "FORM44"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003_03DSEdi_receiptFORM4").ToString()
                Return SendPrinterName
            Case "FORM4_1", "FORM441"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003_03DSEdi_receiptFORM4_1").ToString()
                Return SendPrinterName

            Case "FORM4_2"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003_03DSEdi_receiptFORM4_2").ToString()
                Return SendPrinterName
            Case "FORM4_3"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003_03DSEdi_receiptFORM4_3").ToString()
                Return SendPrinterName
            Case "FORM4_4"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003_03DSEdi_receiptFORM4_4").ToString()
                Return SendPrinterName
            Case "FORM4_5"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003_03DSEdi_receiptFORM4_5").ToString()
                Return SendPrinterName
            Case "FORM4_6", "FORM4_61"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003_03DSEdi_receiptFORM4_6").ToString()
                Return SendPrinterName
            Case "FORM4_8", "FORM4_81"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003_03DSEdi_receiptFORM4_8").ToString()
                Return SendPrinterName
            Case "FORM4_9"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003_03DSEdi_receiptFORM4_9").ToString()
                Return SendPrinterName
            Case "FORM4_91", "FORM4_911"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003_03DSEdi_receiptFORM4_91").ToString()
                Return SendPrinterName
            Case "FORM5"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003_03DSEdi_receiptFORM5").ToString()
                Return SendPrinterName
            Case "FORM5_1"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003_03DSEdi_receiptFORM5_1").ToString()
                Return SendPrinterName

                ''ByTine 14-10-2558 ฟอร์มไทย-ชิลี FORm5_2 
            Case "FORM5_2"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003_03DSEdi_receiptFORM5_2").ToString()
                Return SendPrinterName

            Case "FORM6"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003_03DSEdi_receiptFORM6").ToString()
                Return SendPrinterName
            Case "FORM7"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003_03DSEdi_receiptFORM7").ToString()
                Return SendPrinterName
            Case "FORM8"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003_03DSEdi_receiptFORM8").ToString()
                Return SendPrinterName
            Case "FORM9"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003_03DSEdi_receiptFORM9").ToString()
                Return SendPrinterName
            Case "FORMRussia"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003_03DSEdi_receiptFORMRussia").ToString()
                Return SendPrinterName
        End Select
    End Function

#End Region

#Region "   คำขอ เครื่อง 4 DS   "
    'เครื่องพิมพ์คำขอ สคต.เขต3 (ชลบุรี) CB-003
    Public Shared Function Call_SiteReceipt_CB_003_04(ByVal Form_ID As String) As String
        Dim SendPrinterName As String
        Select Case Form_ID
            Case "FORM1"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003_04DSEdi_receiptFORM1").ToString()
                Return SendPrinterName
            Case "FORM1_1"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003_04DSEdi_receiptFORM1_1").ToString()
                Return SendPrinterName
            Case "FORM1_2"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003_04DSEdi_receiptFORM1_2").ToString()
                Return SendPrinterName
            Case "FORM1_3"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003_04DSEdi_receiptFORM1_3").ToString()
                Return SendPrinterName
            Case "FORM1_4"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003_04DSEdi_receiptFORM1_4").ToString()
                Return SendPrinterName
            Case "FORM2"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003_04DSEdi_receiptFORM2").ToString()
                Return SendPrinterName
            Case "FORM2_1"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003_04DSEdi_receiptFORM2_1").ToString()
                Return SendPrinterName
            Case "FORM2_2"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003_04DSEdi_receiptFORM2_2").ToString()
                Return SendPrinterName
            Case "FORM2_3"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003_04DSEdi_receiptFORM2_3").ToString()
                Return SendPrinterName
            Case "FORM2_4"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003_04DSEdi_receiptFORM2_4").ToString()
                Return SendPrinterName
            Case "FORM2_5"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003_04DSEdi_receiptFORM2_5").ToString()
                Return SendPrinterName
            Case "FORM2_6"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003_04DSEdi_receiptFORM2_6").ToString()
                Return SendPrinterName
            Case "FORM3"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003_04DSEdi_receiptFORM3").ToString()
                Return SendPrinterName
            Case "FORM3_1"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003_04DSEdi_receiptFORM3_1").ToString()
                Return SendPrinterName
                'by rut D new
                'FORM44_4	ฟอร์ม ดี (ATIGA) ใหม่ (7 ประเทศ)
                'FORM44_44	ฟอร์ม ดี (ATIGA) Attach Sheet รถยนต์ ใหม่ (7 ประเทศ)
                'FORM44_41	ฟอร์ม ดี AICO ใหม่ (7 ประเทศ)
                'FORM441_4	ฟอร์ม ดี AICO Attach Sheet รถยนต์ ใหม่ (7 ประเทศ)
            Case "FORM44_4", "FORM44_44", "FORM44_41", "FORM441_4", "FORM44_01", "FORM44_02"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003_04DSEdi_receiptFORM4").ToString()
                Return SendPrinterName
            Case "FORM4", "FORM44"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003_04DSEdi_receiptFORM4").ToString()
                Return SendPrinterName
            Case "FORM4_1", "FORM441"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003_04DSEdi_receiptFORM4_1").ToString()
                Return SendPrinterName

            Case "FORM4_2"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003_04DSEdi_receiptFORM4_2").ToString()
                Return SendPrinterName
            Case "FORM4_3"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003_04DSEdi_receiptFORM4_3").ToString()
                Return SendPrinterName
            Case "FORM4_4"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003_04DSEdi_receiptFORM4_4").ToString()
                Return SendPrinterName
            Case "FORM4_5"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003_04DSEdi_receiptFORM4_5").ToString()
                Return SendPrinterName
            Case "FORM4_6", "FORM4_61"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003_04DSEdi_receiptFORM4_6").ToString()
                Return SendPrinterName
            Case "FORM4_8", "FORM4_81"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003_04DSEdi_receiptFORM4_8").ToString()
                Return SendPrinterName
            Case "FORM4_9"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003_04DSEdi_receiptFORM4_9").ToString()
                Return SendPrinterName
            Case "FORM4_91", "FORM4_911"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003_04DSEdi_receiptFORM4_91").ToString()
                Return SendPrinterName
            Case "FORM5"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003_04DSEdi_receiptFORM5").ToString()
                Return SendPrinterName
            Case "FORM5_1"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003_04DSEdi_receiptFORM5_1").ToString()
                Return SendPrinterName

                ''ByTine 14-10-2558 ฟอร์มไทย-ชิลี FORm5_2 
            Case "FORM5_2"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003_04DSEdi_receiptFORM5_2").ToString()
                Return SendPrinterName

            Case "FORM6"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003_04DSEdi_receiptFORM6").ToString()
                Return SendPrinterName
            Case "FORM7"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003_04DSEdi_receiptFORM7").ToString()
                Return SendPrinterName
            Case "FORM8"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003_04DSEdi_receiptFORM8").ToString()
                Return SendPrinterName
            Case "FORM9"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003_04DSEdi_receiptFORM9").ToString()
                Return SendPrinterName
            Case "FORMRussia"
                SendPrinterName = ConfigurationManager.AppSettings("CB_003_04DSEdi_receiptFORMRussia").ToString()
                Return SendPrinterName
        End Select
    End Function

#End Region
#End Region

#Region "   Site CR-006 'สคต.เขต6 (เชียงราย) Config Printer หนังสือรับรอง-คำขอ    "

#Region "   หนังสือรับรอง   "
    'หนังสือรับรอง CR-006 'สคต.เขต6 (เชียงราย)
    Public Shared Function Call_SitePrints_CR_006(ByVal Form_ID As String) As String
        Dim SendPrinterName As String
        Select Case Form_ID
            Case "FORM1"
                SendPrinterName = ConfigurationManager.AppSettings("CR_006Edi_FORM1").ToString()
                Return SendPrinterName
            Case "FORM1_1"
                SendPrinterName = ConfigurationManager.AppSettings("CR_006Edi_FORM1_1").ToString()
                Return SendPrinterName
            Case "FORM1_2"
                SendPrinterName = ConfigurationManager.AppSettings("CR_006Edi_FORM1_2").ToString()
                Return SendPrinterName
            Case "FORM1_3"
                SendPrinterName = ConfigurationManager.AppSettings("CR_006Edi_FORM1_3").ToString()
                Return SendPrinterName
            Case "FORM1_4"
                SendPrinterName = ConfigurationManager.AppSettings("CR_006Edi_FORM1_4").ToString()
                Return SendPrinterName
            Case "FORM2"
                SendPrinterName = ConfigurationManager.AppSettings("CR_006Edi_FORM2").ToString()
                Return SendPrinterName
            Case "FORM2_1"
                SendPrinterName = ConfigurationManager.AppSettings("CR_006Edi_FORM2_1").ToString()
                Return SendPrinterName
            Case "FORM2_2"
                SendPrinterName = ConfigurationManager.AppSettings("CR_006Edi_FORM2_2").ToString()
                Return SendPrinterName
            Case "FORM2_3"
                SendPrinterName = ConfigurationManager.AppSettings("CR_006Edi_FORM2_3").ToString()
                Return SendPrinterName
            Case "FORM2_4"
                SendPrinterName = ConfigurationManager.AppSettings("CR_006Edi_FORM2_4").ToString()
                Return SendPrinterName
            Case "FORM2_5"
                SendPrinterName = ConfigurationManager.AppSettings("CR_006Edi_FORM2_5").ToString()
                Return SendPrinterName
            Case "FORM2_6"
                SendPrinterName = ConfigurationManager.AppSettings("CR_006Edi_FORM2_6").ToString()
                Return SendPrinterName
            Case "FORM3"
                SendPrinterName = ConfigurationManager.AppSettings("CR_006Edi_FORM3").ToString()
                Return SendPrinterName
            Case "FORM3_1"
                SendPrinterName = ConfigurationManager.AppSettings("CR_006Edi_FORM3_1").ToString()
                Return SendPrinterName
                'by rut D new
                'FORM44_4	ฟอร์ม ดี (ATIGA) ใหม่ (7 ประเทศ)
                'FORM44_44	ฟอร์ม ดี (ATIGA) Attach Sheet รถยนต์ ใหม่ (7 ประเทศ)
                'FORM44_41	ฟอร์ม ดี AICO ใหม่ (7 ประเทศ)
                'FORM441_4	ฟอร์ม ดี AICO Attach Sheet รถยนต์ ใหม่ (7 ประเทศ)
            Case "FORM44_4", "FORM44_44", "FORM44_41", "FORM441_4", "FORM44_01", "FORM44_02"
                SendPrinterName = ConfigurationManager.AppSettings("CR_006Edi_FORM44ALL").ToString()
                Return SendPrinterName
            Case "FORM4", "FORM44"
                SendPrinterName = ConfigurationManager.AppSettings("CR_006Edi_FORM4").ToString()
                Return SendPrinterName
            Case "FORM4_1", "FORM441"
                SendPrinterName = ConfigurationManager.AppSettings("CR_006Edi_FORM4_1").ToString()
                Return SendPrinterName

            Case "FORM4_2"
                SendPrinterName = ConfigurationManager.AppSettings("CR_006Edi_FORM4_2").ToString()
                Return SendPrinterName
            Case "FORM4_3"
                SendPrinterName = ConfigurationManager.AppSettings("CR_006Edi_FORM4_3").ToString()
                Return SendPrinterName
            Case "FORM4_4"
                SendPrinterName = ConfigurationManager.AppSettings("CR_006Edi_FORM4_4").ToString()
                Return SendPrinterName
            Case "FORM4_5"
                SendPrinterName = ConfigurationManager.AppSettings("CR_006Edi_FORM4_5").ToString()
                Return SendPrinterName
            Case "FORM4_6", "FORM4_61"
                SendPrinterName = ConfigurationManager.AppSettings("CR_006Edi_FORM4_6").ToString()
                Return SendPrinterName
            Case "FORM4_8", "FORM4_81"
                SendPrinterName = ConfigurationManager.AppSettings("CR_006Edi_FORM4_8").ToString()
                Return SendPrinterName
            Case "FORM4_9"
                SendPrinterName = ConfigurationManager.AppSettings("CR_006Edi_FORM4_9").ToString()
                Return SendPrinterName
            Case "FORM4_91", "FORM4_911"
                SendPrinterName = ConfigurationManager.AppSettings("CR_006Edi_FORM4_91").ToString()
                Return SendPrinterName
            Case "FORM5"
                SendPrinterName = ConfigurationManager.AppSettings("CR_006Edi_FORM5").ToString()
                Return SendPrinterName
            Case "FORM5_1"
                SendPrinterName = ConfigurationManager.AppSettings("CR_006Edi_FORM5_1").ToString()
                Return SendPrinterName

                ''ByTine 14-10-2558 ฟอร์มไทย-ชิลี FORm5_2 
            Case "FORM5_2"
                SendPrinterName = ConfigurationManager.AppSettings("CR_006Edi_FORM5_2").ToString()
                Return SendPrinterName

            Case "FORM6"
                SendPrinterName = ConfigurationManager.AppSettings("CR_006Edi_FORM6").ToString()
                Return SendPrinterName
            Case "FORM7"
                SendPrinterName = ConfigurationManager.AppSettings("CR_006Edi_FORM7").ToString()
                Return SendPrinterName
            Case "FORM8"
                SendPrinterName = ConfigurationManager.AppSettings("CR_006Edi_FORM8").ToString()
                Return SendPrinterName
            Case "FORM9"
                SendPrinterName = ConfigurationManager.AppSettings("CR_006Edi_FORM9").ToString()
                Return SendPrinterName
            Case "FORMRussia"
                SendPrinterName = ConfigurationManager.AppSettings("CR_006Edi_FORMRussia").ToString()
                Return SendPrinterName

        End Select
    End Function
#End Region

#Region "   คำขอ เครื่อง 1   "
    'เครื่องพิมพ์คำขอ CR-006 'สคต.เขต6 (เชียงราย)
    Public Shared Function Call_SiteReceipt_CR_006_01(ByVal Form_ID As String) As String
        Dim SendPrinterName As String
        Select Case Form_ID
            Case "FORM1"
                SendPrinterName = ConfigurationManager.AppSettings("CR_006_01Edi_receiptFORM1").ToString()
                Return SendPrinterName
            Case "FORM1_1"
                SendPrinterName = ConfigurationManager.AppSettings("CR_006_01Edi_receiptFORM1_1").ToString()
                Return SendPrinterName
            Case "FORM1_2"
                SendPrinterName = ConfigurationManager.AppSettings("CR_006_01Edi_receiptFORM1_2").ToString()
                Return SendPrinterName
            Case "FORM1_3"
                SendPrinterName = ConfigurationManager.AppSettings("CR_006_01Edi_receiptFORM1_3").ToString()
                Return SendPrinterName
            Case "FORM1_4"
                SendPrinterName = ConfigurationManager.AppSettings("CR_006_01Edi_receiptFORM1_4").ToString()
                Return SendPrinterName
            Case "FORM2"
                SendPrinterName = ConfigurationManager.AppSettings("CR_006_01Edi_receiptFORM2").ToString()
                Return SendPrinterName
            Case "FORM2_1"
                SendPrinterName = ConfigurationManager.AppSettings("CR_006_01Edi_receiptFORM2_1").ToString()
                Return SendPrinterName
            Case "FORM2_2"
                SendPrinterName = ConfigurationManager.AppSettings("CR_006_01Edi_receiptFORM2_2").ToString()
                Return SendPrinterName
            Case "FORM2_3"
                SendPrinterName = ConfigurationManager.AppSettings("CR_006_01Edi_receiptFORM2_3").ToString()
                Return SendPrinterName
            Case "FORM2_4"
                SendPrinterName = ConfigurationManager.AppSettings("CR_006_01Edi_receiptFORM2_4").ToString()
                Return SendPrinterName
            Case "FORM2_5"
                SendPrinterName = ConfigurationManager.AppSettings("CR_006_01Edi_receiptFORM2_5").ToString()
                Return SendPrinterName
            Case "FORM2_6"
                SendPrinterName = ConfigurationManager.AppSettings("CR_006_01Edi_receiptFORM2_6").ToString()
                Return SendPrinterName
            Case "FORM3"
                SendPrinterName = ConfigurationManager.AppSettings("CR_006_01Edi_receiptFORM3").ToString()
                Return SendPrinterName
            Case "FORM3_1"
                SendPrinterName = ConfigurationManager.AppSettings("CR_006_01Edi_receiptFORM3_1").ToString()
                Return SendPrinterName
                'by rut D new
                'FORM44_4	ฟอร์ม ดี (ATIGA) ใหม่ (7 ประเทศ)
                'FORM44_44	ฟอร์ม ดี (ATIGA) Attach Sheet รถยนต์ ใหม่ (7 ประเทศ)
                'FORM44_41	ฟอร์ม ดี AICO ใหม่ (7 ประเทศ)
                'FORM441_4	ฟอร์ม ดี AICO Attach Sheet รถยนต์ ใหม่ (7 ประเทศ)
            Case "FORM44_4", "FORM44_44", "FORM44_41", "FORM441_4", "FORM44_01", "FORM44_02"
                SendPrinterName = ConfigurationManager.AppSettings("CR_006_01Edi_receiptFORM4").ToString()
                Return SendPrinterName
            Case "FORM4", "FORM44"
                SendPrinterName = ConfigurationManager.AppSettings("CR_006_01Edi_receiptFORM4").ToString()
                Return SendPrinterName
            Case "FORM4_1", "FORM441"
                SendPrinterName = ConfigurationManager.AppSettings("CR_006_01Edi_receiptFORM4_1").ToString()
                Return SendPrinterName

            Case "FORM4_2"
                SendPrinterName = ConfigurationManager.AppSettings("CR_006_01Edi_receiptFORM4_2").ToString()
                Return SendPrinterName
            Case "FORM4_3"
                SendPrinterName = ConfigurationManager.AppSettings("CR_006_01Edi_receiptFORM4_3").ToString()
                Return SendPrinterName
            Case "FORM4_4"
                SendPrinterName = ConfigurationManager.AppSettings("CR_006_01Edi_receiptFORM4_4").ToString()
                Return SendPrinterName
            Case "FORM4_5"
                SendPrinterName = ConfigurationManager.AppSettings("CR_006_01Edi_receiptFORM4_5").ToString()
                Return SendPrinterName
            Case "FORM4_6", "FORM4_61"
                SendPrinterName = ConfigurationManager.AppSettings("CR_006_01Edi_receiptFORM4_6").ToString()
                Return SendPrinterName
            Case "FORM4_8", "FORM4_81"
                SendPrinterName = ConfigurationManager.AppSettings("CR_006_01Edi_receiptFORM4_8").ToString()
                Return SendPrinterName
            Case "FORM4_9"
                SendPrinterName = ConfigurationManager.AppSettings("CR_006_01Edi_receiptFORM4_9").ToString()
                Return SendPrinterName
            Case "FORM4_91", "FORM4_911"
                SendPrinterName = ConfigurationManager.AppSettings("CR_006_01Edi_receiptFORM4_91").ToString()
                Return SendPrinterName
            Case "FORM5"
                SendPrinterName = ConfigurationManager.AppSettings("CR_006_01Edi_receiptFORM5").ToString()
                Return SendPrinterName
            Case "FORM5_1"
                SendPrinterName = ConfigurationManager.AppSettings("CR_006_01Edi_receiptFORM5_1").ToString()
                Return SendPrinterName

                ''ByTine 14-10-2558 ฟอร์มไทย-ชิลี FORm5_2 
            Case "FORM5_2"
                SendPrinterName = ConfigurationManager.AppSettings("CR_006_01Edi_receiptFORM5_2").ToString()
                Return SendPrinterName

            Case "FORM6"
                SendPrinterName = ConfigurationManager.AppSettings("CR_006_01Edi_receiptFORM6").ToString()
                Return SendPrinterName
            Case "FORM7"
                SendPrinterName = ConfigurationManager.AppSettings("CR_006_01Edi_receiptFORM7").ToString()
                Return SendPrinterName
            Case "FORM8"
                SendPrinterName = ConfigurationManager.AppSettings("CR_006_01Edi_receiptFORM8").ToString()
                Return SendPrinterName
            Case "FORM9"
                SendPrinterName = ConfigurationManager.AppSettings("CR_006_01Edi_receiptFORM9").ToString()
                Return SendPrinterName
            Case "FORMRussia"
                SendPrinterName = ConfigurationManager.AppSettings("CR_006_01Edi_receiptFORMRussia").ToString()
                Return SendPrinterName
        End Select
    End Function

#End Region

#End Region

#Region "   Site CM-001 'สคต.เชียงใหม่ Config Printer หนังสือรับรอง-คำขอ    "

#Region "   หนังสือรับรอง   "
    'หนังสือรับรอง CM-001 'สคต.เชียงใหม่
    Public Shared Function Call_SitePrints_CM_001(ByVal Form_ID As String) As String
        Dim SendPrinterName As String
        Select Case Form_ID
            Case "FORM1"
                SendPrinterName = ConfigurationManager.AppSettings("CM_001Edi_FORM1").ToString()
                Return SendPrinterName
            Case "FORM1_1"
                SendPrinterName = ConfigurationManager.AppSettings("CM_001Edi_FORM1_1").ToString()
                Return SendPrinterName
            Case "FORM1_2"
                SendPrinterName = ConfigurationManager.AppSettings("CM_001Edi_FORM1_2").ToString()
                Return SendPrinterName
            Case "FORM1_3"
                SendPrinterName = ConfigurationManager.AppSettings("CM_001Edi_FORM1_3").ToString()
                Return SendPrinterName
            Case "FORM1_4"
                SendPrinterName = ConfigurationManager.AppSettings("CM_001Edi_FORM1_4").ToString()
                Return SendPrinterName
            Case "FORM2"
                SendPrinterName = ConfigurationManager.AppSettings("CM_001Edi_FORM2").ToString()
                Return SendPrinterName
            Case "FORM2_1"
                SendPrinterName = ConfigurationManager.AppSettings("CM_001Edi_FORM2_1").ToString()
                Return SendPrinterName
            Case "FORM2_2"
                SendPrinterName = ConfigurationManager.AppSettings("CM_001Edi_FORM2_2").ToString()
                Return SendPrinterName
            Case "FORM2_3"
                SendPrinterName = ConfigurationManager.AppSettings("CM_001Edi_FORM2_3").ToString()
                Return SendPrinterName
            Case "FORM2_4"
                SendPrinterName = ConfigurationManager.AppSettings("CM_001Edi_FORM2_4").ToString()
                Return SendPrinterName
            Case "FORM2_5"
                SendPrinterName = ConfigurationManager.AppSettings("CM_001Edi_FORM2_5").ToString()
                Return SendPrinterName
            Case "FORM2_6"
                SendPrinterName = ConfigurationManager.AppSettings("CM_001Edi_FORM2_6").ToString()
                Return SendPrinterName
            Case "FORM3"
                SendPrinterName = ConfigurationManager.AppSettings("CM_001Edi_FORM3").ToString()
                Return SendPrinterName
            Case "FORM3_1"
                SendPrinterName = ConfigurationManager.AppSettings("CM_001Edi_FORM3_1").ToString()
                Return SendPrinterName
                'by rut D new
                'FORM44_4	ฟอร์ม ดี (ATIGA) ใหม่ (7 ประเทศ)
                'FORM44_44	ฟอร์ม ดี (ATIGA) Attach Sheet รถยนต์ ใหม่ (7 ประเทศ)
                'FORM44_41	ฟอร์ม ดี AICO ใหม่ (7 ประเทศ)
                'FORM441_4	ฟอร์ม ดี AICO Attach Sheet รถยนต์ ใหม่ (7 ประเทศ)
            Case "FORM44_4", "FORM44_44", "FORM44_41", "FORM441_4", "FORM44_01", "FORM44_02"
                SendPrinterName = ConfigurationManager.AppSettings("CM_001Edi_FORM44ALL").ToString()
                Return SendPrinterName
            Case "FORM4", "FORM44"
                SendPrinterName = ConfigurationManager.AppSettings("CM_001Edi_FORM4").ToString()
                Return SendPrinterName
            Case "FORM4_1", "FORM441"
                SendPrinterName = ConfigurationManager.AppSettings("CM_001Edi_FORM4_1").ToString()
                Return SendPrinterName

            Case "FORM4_2"
                SendPrinterName = ConfigurationManager.AppSettings("CM_001Edi_FORM4_2").ToString()
                Return SendPrinterName
            Case "FORM4_3"
                SendPrinterName = ConfigurationManager.AppSettings("CM_001Edi_FORM4_3").ToString()
                Return SendPrinterName
            Case "FORM4_4"
                SendPrinterName = ConfigurationManager.AppSettings("CM_001Edi_FORM4_4").ToString()
                Return SendPrinterName
            Case "FORM4_5"
                SendPrinterName = ConfigurationManager.AppSettings("CM_001Edi_FORM4_5").ToString()
                Return SendPrinterName
            Case "FORM4_6", "FORM4_61"
                SendPrinterName = ConfigurationManager.AppSettings("CM_001Edi_FORM4_6").ToString()
                Return SendPrinterName
            Case "FORM4_8", "FORM4_81"
                SendPrinterName = ConfigurationManager.AppSettings("CM_001Edi_FORM4_8").ToString()
                Return SendPrinterName
            Case "FORM4_9"
                SendPrinterName = ConfigurationManager.AppSettings("CM_001Edi_FORM4_9").ToString()
                Return SendPrinterName
            Case "FORM4_91", "FORM4_911"
                SendPrinterName = ConfigurationManager.AppSettings("CM_001Edi_FORM4_91").ToString()
                Return SendPrinterName
            Case "FORM5"
                SendPrinterName = ConfigurationManager.AppSettings("CM_001Edi_FORM5").ToString()
                Return SendPrinterName
            Case "FORM5_1"
                SendPrinterName = ConfigurationManager.AppSettings("CM_001Edi_FORM5_1").ToString()
                Return SendPrinterName

                ''ByTine 14-10-2558 ฟอร์มไทย-ชิลี FORm5_2 
            Case "FORM5_2"
                SendPrinterName = ConfigurationManager.AppSettings("CM_001Edi_FORM5_2").ToString()
                Return SendPrinterName

            Case "FORM6"
                SendPrinterName = ConfigurationManager.AppSettings("CM_001Edi_FORM6").ToString()
                Return SendPrinterName
            Case "FORM7"
                SendPrinterName = ConfigurationManager.AppSettings("CM_001Edi_FORM7").ToString()
                Return SendPrinterName
            Case "FORM8"
                SendPrinterName = ConfigurationManager.AppSettings("CM_001Edi_FORM8").ToString()
                Return SendPrinterName
            Case "FORM9"
                SendPrinterName = ConfigurationManager.AppSettings("CM_001Edi_FORM9").ToString()
                Return SendPrinterName
            Case "FORMRussia"
                SendPrinterName = ConfigurationManager.AppSettings("CM_001Edi_FORMRussia").ToString()
                Return SendPrinterName

        End Select
    End Function
#End Region

#Region "   คำขอ เครื่อง 1   "
    'เครื่องพิมพ์คำขอ CM-001 'สคต.เชียงใหม่
    Public Shared Function Call_SiteReceipt_CM_001_01(ByVal Form_ID As String) As String
        Dim SendPrinterName As String
        Select Case Form_ID
            Case "FORM1"
                SendPrinterName = ConfigurationManager.AppSettings("CM_001_01Edi_receiptFORM1").ToString()
                Return SendPrinterName
            Case "FORM1_1"
                SendPrinterName = ConfigurationManager.AppSettings("CM_001_01Edi_receiptFORM1_1").ToString()
                Return SendPrinterName
            Case "FORM1_2"
                SendPrinterName = ConfigurationManager.AppSettings("CM_001_01Edi_receiptFORM1_2").ToString()
                Return SendPrinterName
            Case "FORM1_3"
                SendPrinterName = ConfigurationManager.AppSettings("CM_001_01Edi_receiptFORM1_3").ToString()
                Return SendPrinterName
            Case "FORM1_4"
                SendPrinterName = ConfigurationManager.AppSettings("CM_001_01Edi_receiptFORM1_4").ToString()
                Return SendPrinterName
            Case "FORM2"
                SendPrinterName = ConfigurationManager.AppSettings("CM_001_01Edi_receiptFORM2").ToString()
                Return SendPrinterName
            Case "FORM2_1"
                SendPrinterName = ConfigurationManager.AppSettings("CM_001_01Edi_receiptFORM2_1").ToString()
                Return SendPrinterName
            Case "FORM2_2"
                SendPrinterName = ConfigurationManager.AppSettings("CM_001_01Edi_receiptFORM2_2").ToString()
                Return SendPrinterName
            Case "FORM2_3"
                SendPrinterName = ConfigurationManager.AppSettings("CM_001_01Edi_receiptFORM2_3").ToString()
                Return SendPrinterName
            Case "FORM2_4"
                SendPrinterName = ConfigurationManager.AppSettings("CM_001_01Edi_receiptFORM2_4").ToString()
                Return SendPrinterName
            Case "FORM2_5"
                SendPrinterName = ConfigurationManager.AppSettings("CM_001_01Edi_receiptFORM2_5").ToString()
                Return SendPrinterName
            Case "FORM2_6"
                SendPrinterName = ConfigurationManager.AppSettings("CM_001_01Edi_receiptFORM2_6").ToString()
                Return SendPrinterName
            Case "FORM3"
                SendPrinterName = ConfigurationManager.AppSettings("CM_001_01Edi_receiptFORM3").ToString()
                Return SendPrinterName
            Case "FORM3_1"
                SendPrinterName = ConfigurationManager.AppSettings("CM_001_01Edi_receiptFORM3_1").ToString()
                Return SendPrinterName
                'by rut D new
                'FORM44_4	ฟอร์ม ดี (ATIGA) ใหม่ (7 ประเทศ)
                'FORM44_44	ฟอร์ม ดี (ATIGA) Attach Sheet รถยนต์ ใหม่ (7 ประเทศ)
                'FORM44_41	ฟอร์ม ดี AICO ใหม่ (7 ประเทศ)
                'FORM441_4	ฟอร์ม ดี AICO Attach Sheet รถยนต์ ใหม่ (7 ประเทศ)
            Case "FORM44_4", "FORM44_44", "FORM44_41", "FORM441_4", "FORM44_01", "FORM44_02"
                SendPrinterName = ConfigurationManager.AppSettings("CM_001_01Edi_receiptFORM4").ToString()
                Return SendPrinterName
            Case "FORM4", "FORM44"
                SendPrinterName = ConfigurationManager.AppSettings("CM_001_01Edi_receiptFORM4").ToString()
                Return SendPrinterName
            Case "FORM4_1", "FORM441"
                SendPrinterName = ConfigurationManager.AppSettings("CM_001_01Edi_receiptFORM4_1").ToString()
                Return SendPrinterName

            Case "FORM4_2"
                SendPrinterName = ConfigurationManager.AppSettings("CM_001_01Edi_receiptFORM4_2").ToString()
                Return SendPrinterName
            Case "FORM4_3"
                SendPrinterName = ConfigurationManager.AppSettings("CM_001_01Edi_receiptFORM4_3").ToString()
                Return SendPrinterName
            Case "FORM4_4"
                SendPrinterName = ConfigurationManager.AppSettings("CM_001_01Edi_receiptFORM4_4").ToString()
                Return SendPrinterName
            Case "FORM4_5"
                SendPrinterName = ConfigurationManager.AppSettings("CM_001_01Edi_receiptFORM4_5").ToString()
                Return SendPrinterName
            Case "FORM4_6", "FORM4_61"
                SendPrinterName = ConfigurationManager.AppSettings("CM_001_01Edi_receiptFORM4_6").ToString()
                Return SendPrinterName
            Case "FORM4_8", "FORM4_81"
                SendPrinterName = ConfigurationManager.AppSettings("CM_001_01Edi_receiptFORM4_8").ToString()
                Return SendPrinterName
            Case "FORM4_9"
                SendPrinterName = ConfigurationManager.AppSettings("CM_001_01Edi_receiptFORM4_9").ToString()
                Return SendPrinterName
            Case "FORM4_91", "FORM4_911"
                SendPrinterName = ConfigurationManager.AppSettings("CM_001_01Edi_receiptFORM4_91").ToString()
                Return SendPrinterName
            Case "FORM5"
                SendPrinterName = ConfigurationManager.AppSettings("CM_001_01Edi_receiptFORM5").ToString()
                Return SendPrinterName
            Case "FORM5_1"
                SendPrinterName = ConfigurationManager.AppSettings("CM_001_01Edi_receiptFORM5_1").ToString()
                Return SendPrinterName

                ''ByTine 14-10-2558 ฟอร์มไทย-ชิลี FORm5_2 
            Case "FORM5_2"
                SendPrinterName = ConfigurationManager.AppSettings("CM_001_01Edi_receiptFORM5_2").ToString()
                Return SendPrinterName

            Case "FORM6"
                SendPrinterName = ConfigurationManager.AppSettings("CM_001_01Edi_receiptFORM6").ToString()
                Return SendPrinterName
            Case "FORM7"
                SendPrinterName = ConfigurationManager.AppSettings("CM_001_01Edi_receiptFORM7").ToString()
                Return SendPrinterName
            Case "FORM8"
                SendPrinterName = ConfigurationManager.AppSettings("CM_001_01Edi_receiptFORM8").ToString()
                Return SendPrinterName
            Case "FORM9"
                SendPrinterName = ConfigurationManager.AppSettings("CM_001_01Edi_receiptFORM9").ToString()
                Return SendPrinterName
            Case "FORMRussia"
                SendPrinterName = ConfigurationManager.AppSettings("CM_001_01Edi_receiptFORMRussia").ToString()
                Return SendPrinterName
        End Select
    End Function
#End Region

#End Region

#Region "   Site HY-002 'สคต.สงขลา Config Printer หนังสือรับรอง-คำขอ    "

#Region "   หนังสือรับรอง   "
    'หนังสือรับรอง HY-002 'สคต.สงขลา
    Public Shared Function Call_SitePrints_HY_002(ByVal Form_ID As String) As String
        Dim SendPrinterName As String
        Select Case Form_ID
            Case "FORM1"
                SendPrinterName = ConfigurationManager.AppSettings("HY_002Edi_FORM1").ToString()
                Return SendPrinterName
            Case "FORM1_1"
                SendPrinterName = ConfigurationManager.AppSettings("HY_002Edi_FORM1_1").ToString()
                Return SendPrinterName
            Case "FORM1_2"
                SendPrinterName = ConfigurationManager.AppSettings("HY_002Edi_FORM1_2").ToString()
                Return SendPrinterName
            Case "FORM1_3"
                SendPrinterName = ConfigurationManager.AppSettings("HY_002Edi_FORM1_3").ToString()
                Return SendPrinterName
            Case "FORM1_4"
                SendPrinterName = ConfigurationManager.AppSettings("HY_002Edi_FORM1_4").ToString()
                Return SendPrinterName
            Case "FORM2"
                SendPrinterName = ConfigurationManager.AppSettings("HY_002Edi_FORM2").ToString()
                Return SendPrinterName
            Case "FORM2_1"
                SendPrinterName = ConfigurationManager.AppSettings("HY_002Edi_FORM2_1").ToString()
                Return SendPrinterName
            Case "FORM2_2"
                SendPrinterName = ConfigurationManager.AppSettings("HY_002Edi_FORM2_2").ToString()
                Return SendPrinterName
            Case "FORM2_3"
                SendPrinterName = ConfigurationManager.AppSettings("HY_002Edi_FORM2_3").ToString()
                Return SendPrinterName
            Case "FORM2_4"
                SendPrinterName = ConfigurationManager.AppSettings("HY_002Edi_FORM2_4").ToString()
                Return SendPrinterName
            Case "FORM2_5"
                SendPrinterName = ConfigurationManager.AppSettings("HY_002Edi_FORM2_5").ToString()
                Return SendPrinterName
            Case "FORM2_6"
                SendPrinterName = ConfigurationManager.AppSettings("HY_002Edi_FORM2_6").ToString()
                Return SendPrinterName
            Case "FORM3"
                SendPrinterName = ConfigurationManager.AppSettings("HY_002Edi_FORM3").ToString()
                Return SendPrinterName
            Case "FORM3_1"
                SendPrinterName = ConfigurationManager.AppSettings("HY_002Edi_FORM3_1").ToString()
                Return SendPrinterName
                'by rut D new
                'FORM44_4	ฟอร์ม ดี (ATIGA) ใหม่ (7 ประเทศ)
                'FORM44_44	ฟอร์ม ดี (ATIGA) Attach Sheet รถยนต์ ใหม่ (7 ประเทศ)
                'FORM44_41	ฟอร์ม ดี AICO ใหม่ (7 ประเทศ)
                'FORM441_4	ฟอร์ม ดี AICO Attach Sheet รถยนต์ ใหม่ (7 ประเทศ)
            Case "FORM44_4", "FORM44_44", "FORM44_41", "FORM441_4", "FORM44_01", "FORM44_02"
                SendPrinterName = ConfigurationManager.AppSettings("HY_002Edi_FORM44ALL").ToString()
                Return SendPrinterName
            Case "FORM4", "FORM44"
                SendPrinterName = ConfigurationManager.AppSettings("HY_002Edi_FORM4").ToString()
                Return SendPrinterName
            Case "FORM4_1", "FORM441"
                SendPrinterName = ConfigurationManager.AppSettings("HY_002Edi_FORM4_1").ToString()
                Return SendPrinterName

            Case "FORM4_2"
                SendPrinterName = ConfigurationManager.AppSettings("HY_002Edi_FORM4_2").ToString()
                Return SendPrinterName
            Case "FORM4_3"
                SendPrinterName = ConfigurationManager.AppSettings("HY_002Edi_FORM4_3").ToString()
                Return SendPrinterName
            Case "FORM4_4"
                SendPrinterName = ConfigurationManager.AppSettings("HY_002Edi_FORM4_4").ToString()
                Return SendPrinterName
            Case "FORM4_5"
                SendPrinterName = ConfigurationManager.AppSettings("HY_002Edi_FORM4_5").ToString()
                Return SendPrinterName
            Case "FORM4_6", "FORM4_61"
                SendPrinterName = ConfigurationManager.AppSettings("HY_002Edi_FORM4_6").ToString()
                Return SendPrinterName
            Case "FORM4_8", "FORM4_81"
                SendPrinterName = ConfigurationManager.AppSettings("HY_002Edi_FORM4_8").ToString()
                Return SendPrinterName
            Case "FORM4_9"
                SendPrinterName = ConfigurationManager.AppSettings("HY_002Edi_FORM4_9").ToString()
                Return SendPrinterName
            Case "FORM4_91", "FORM4_911"
                SendPrinterName = ConfigurationManager.AppSettings("HY_002Edi_FORM4_91").ToString()
                Return SendPrinterName
            Case "FORM5"
                SendPrinterName = ConfigurationManager.AppSettings("HY_002Edi_FORM5").ToString()
                Return SendPrinterName
            Case "FORM5_1"
                SendPrinterName = ConfigurationManager.AppSettings("HY_002Edi_FORM5_1").ToString()
                Return SendPrinterName

                ''ByTine 14-10-2558 ฟอร์มไทย-ชิลี FORm5_2 
            Case "FORM5_2"
                SendPrinterName = ConfigurationManager.AppSettings("HY_002Edi_FORM5_2").ToString()
                Return SendPrinterName

            Case "FORM6"
                SendPrinterName = ConfigurationManager.AppSettings("HY_002Edi_FORM6").ToString()
                Return SendPrinterName
            Case "FORM7"
                SendPrinterName = ConfigurationManager.AppSettings("HY_002Edi_FORM7").ToString()
                Return SendPrinterName
            Case "FORM8"
                SendPrinterName = ConfigurationManager.AppSettings("HY_002Edi_FORM8").ToString()
                Return SendPrinterName
            Case "FORM9"
                SendPrinterName = ConfigurationManager.AppSettings("HY_002Edi_FORM9").ToString()
                Return SendPrinterName
            Case "FORMRussia"
                SendPrinterName = ConfigurationManager.AppSettings("HY_002Edi_FORMRussia").ToString()
                Return SendPrinterName

        End Select
    End Function
#End Region

#Region "   คำขอ เครื่อง 1   "
    'เครื่องพิมพ์คำขอ สคต.สงขลา
    Public Shared Function Call_SiteReceipt_HY_002_01(ByVal Form_ID As String) As String
        Dim SendPrinterName As String
        Select Case Form_ID
            Case "FORM1"
                SendPrinterName = ConfigurationManager.AppSettings("HY_002_01Edi_receiptFORM1").ToString()
                Return SendPrinterName
            Case "FORM1_1"
                SendPrinterName = ConfigurationManager.AppSettings("HY_002_01Edi_receiptFORM1_1").ToString()
                Return SendPrinterName
            Case "FORM1_2"
                SendPrinterName = ConfigurationManager.AppSettings("HY_002_01Edi_receiptFORM1_2").ToString()
                Return SendPrinterName
            Case "FORM1_3"
                SendPrinterName = ConfigurationManager.AppSettings("HY_002_01Edi_receiptFORM1_3").ToString()
                Return SendPrinterName
            Case "FORM1_4"
                SendPrinterName = ConfigurationManager.AppSettings("HY_002_01Edi_receiptFORM1_4").ToString()
                Return SendPrinterName
            Case "FORM2"
                SendPrinterName = ConfigurationManager.AppSettings("HY_002_01Edi_receiptFORM2").ToString()
                Return SendPrinterName
            Case "FORM2_1"
                SendPrinterName = ConfigurationManager.AppSettings("HY_002_01Edi_receiptFORM2_1").ToString()
                Return SendPrinterName
            Case "FORM2_2"
                SendPrinterName = ConfigurationManager.AppSettings("HY_002_01Edi_receiptFORM2_2").ToString()
                Return SendPrinterName
            Case "FORM2_3"
                SendPrinterName = ConfigurationManager.AppSettings("HY_002_01Edi_receiptFORM2_3").ToString()
                Return SendPrinterName
            Case "FORM2_4"
                SendPrinterName = ConfigurationManager.AppSettings("HY_002_01Edi_receiptFORM2_4").ToString()
                Return SendPrinterName
            Case "FORM2_5"
                SendPrinterName = ConfigurationManager.AppSettings("HY_002_01Edi_receiptFORM2_5").ToString()
                Return SendPrinterName
            Case "FORM2_6"
                SendPrinterName = ConfigurationManager.AppSettings("HY_002_01Edi_receiptFORM2_6").ToString()
                Return SendPrinterName
            Case "FORM3"
                SendPrinterName = ConfigurationManager.AppSettings("HY_002_01Edi_receiptFORM3").ToString()
                Return SendPrinterName
            Case "FORM3_1"
                SendPrinterName = ConfigurationManager.AppSettings("HY_002_01Edi_receiptFORM3_1").ToString()
                Return SendPrinterName
                'by rut D new
                'FORM44_4	ฟอร์ม ดี (ATIGA) ใหม่ (7 ประเทศ)
                'FORM44_44	ฟอร์ม ดี (ATIGA) Attach Sheet รถยนต์ ใหม่ (7 ประเทศ)
                'FORM44_41	ฟอร์ม ดี AICO ใหม่ (7 ประเทศ)
                'FORM441_4	ฟอร์ม ดี AICO Attach Sheet รถยนต์ ใหม่ (7 ประเทศ)
            Case "FORM44_4", "FORM44_44", "FORM44_41", "FORM441_4", "FORM44_01", "FORM44_02"
                SendPrinterName = ConfigurationManager.AppSettings("HY_002_01Edi_receiptFORM4").ToString()
                Return SendPrinterName
            Case "FORM4", "FORM44"
                SendPrinterName = ConfigurationManager.AppSettings("HY_002_01Edi_receiptFORM4").ToString()
                Return SendPrinterName
            Case "FORM4_1", "FORM441"
                SendPrinterName = ConfigurationManager.AppSettings("HY_002_01Edi_receiptFORM4_1").ToString()
                Return SendPrinterName

            Case "FORM4_2"
                SendPrinterName = ConfigurationManager.AppSettings("HY_002_01Edi_receiptFORM4_2").ToString()
                Return SendPrinterName
            Case "FORM4_3"
                SendPrinterName = ConfigurationManager.AppSettings("HY_002_01Edi_receiptFORM4_3").ToString()
                Return SendPrinterName
            Case "FORM4_4"
                SendPrinterName = ConfigurationManager.AppSettings("HY_002_01Edi_receiptFORM4_4").ToString()
                Return SendPrinterName
            Case "FORM4_5"
                SendPrinterName = ConfigurationManager.AppSettings("HY_002_01Edi_receiptFORM4_5").ToString()
                Return SendPrinterName
            Case "FORM4_6", "FORM4_61"
                SendPrinterName = ConfigurationManager.AppSettings("HY_002_01Edi_receiptFORM4_6").ToString()
                Return SendPrinterName
            Case "FORM4_8", "FORM4_81"
                SendPrinterName = ConfigurationManager.AppSettings("HY_002_01Edi_receiptFORM4_8").ToString()
                Return SendPrinterName
            Case "FORM4_9"
                SendPrinterName = ConfigurationManager.AppSettings("HY_002_01Edi_receiptFORM4_9").ToString()
                Return SendPrinterName
            Case "FORM4_91", "FORM4_911"
                SendPrinterName = ConfigurationManager.AppSettings("HY_002_01Edi_receiptFORM4_91").ToString()
                Return SendPrinterName
            Case "FORM5"
                SendPrinterName = ConfigurationManager.AppSettings("HY_002_01Edi_receiptFORM5").ToString()
                Return SendPrinterName
            Case "FORM5_1"
                SendPrinterName = ConfigurationManager.AppSettings("HY_002_01Edi_receiptFORM5_1").ToString()
                Return SendPrinterName

                ''ByTine 14-10-2558 ฟอร์มไทย-ชิลี FORm5_2 
            Case "FORM5_2"
                SendPrinterName = ConfigurationManager.AppSettings("HY_002_01Edi_receiptFORM5_2").ToString()
                Return SendPrinterName

            Case "FORM6"
                SendPrinterName = ConfigurationManager.AppSettings("HY_002_01Edi_receiptFORM6").ToString()
                Return SendPrinterName
            Case "FORM7"
                SendPrinterName = ConfigurationManager.AppSettings("HY_002_01Edi_receiptFORM7").ToString()
                Return SendPrinterName
            Case "FORM8"
                SendPrinterName = ConfigurationManager.AppSettings("HY_002_01Edi_receiptFORM8").ToString()
                Return SendPrinterName
            Case "FORM9"
                SendPrinterName = ConfigurationManager.AppSettings("HY_002_01Edi_receiptFORM9").ToString()
                Return SendPrinterName
            Case "FORMRussia"
                SendPrinterName = ConfigurationManager.AppSettings("HY_002_01Edi_receiptFORMRussia").ToString()
                Return SendPrinterName
        End Select
    End Function

#End Region

#Region "   คำขอ เครื่อง 2   "
    'เครื่องพิมพ์คำขอ สคต.สงขลา
    Public Shared Function Call_SiteReceipt_HY_002_02(ByVal Form_ID As String) As String
        Dim SendPrinterName As String
        Select Case Form_ID
            Case "FORM1"
                SendPrinterName = ConfigurationManager.AppSettings("HY_002_02Edi_receiptFORM1").ToString()
                Return SendPrinterName
            Case "FORM1_1"
                SendPrinterName = ConfigurationManager.AppSettings("HY_002_02Edi_receiptFORM1_1").ToString()
                Return SendPrinterName
            Case "FORM1_2"
                SendPrinterName = ConfigurationManager.AppSettings("HY_002_02Edi_receiptFORM1_2").ToString()
                Return SendPrinterName
            Case "FORM1_3"
                SendPrinterName = ConfigurationManager.AppSettings("HY_002_02Edi_receiptFORM1_3").ToString()
                Return SendPrinterName
            Case "FORM1_4"
                SendPrinterName = ConfigurationManager.AppSettings("HY_002_02Edi_receiptFORM1_4").ToString()
                Return SendPrinterName
            Case "FORM2"
                SendPrinterName = ConfigurationManager.AppSettings("HY_002_02Edi_receiptFORM2").ToString()
                Return SendPrinterName
            Case "FORM2_1"
                SendPrinterName = ConfigurationManager.AppSettings("HY_002_02Edi_receiptFORM2_1").ToString()
                Return SendPrinterName
            Case "FORM2_2"
                SendPrinterName = ConfigurationManager.AppSettings("HY_002_02Edi_receiptFORM2_2").ToString()
                Return SendPrinterName
            Case "FORM2_3"
                SendPrinterName = ConfigurationManager.AppSettings("HY_002_02Edi_receiptFORM2_3").ToString()
                Return SendPrinterName
            Case "FORM2_4"
                SendPrinterName = ConfigurationManager.AppSettings("HY_002_02Edi_receiptFORM2_4").ToString()
                Return SendPrinterName
            Case "FORM2_5"
                SendPrinterName = ConfigurationManager.AppSettings("HY_002_02Edi_receiptFORM2_5").ToString()
                Return SendPrinterName
            Case "FORM2_6"
                SendPrinterName = ConfigurationManager.AppSettings("HY_002_02Edi_receiptFORM2_6").ToString()
                Return SendPrinterName
            Case "FORM3"
                SendPrinterName = ConfigurationManager.AppSettings("HY_002_02Edi_receiptFORM3").ToString()
                Return SendPrinterName
            Case "FORM3_1"
                SendPrinterName = ConfigurationManager.AppSettings("HY_002_02Edi_receiptFORM3_1").ToString()
                Return SendPrinterName
                'by rut D new
                'FORM44_4	ฟอร์ม ดี (ATIGA) ใหม่ (7 ประเทศ)
                'FORM44_44	ฟอร์ม ดี (ATIGA) Attach Sheet รถยนต์ ใหม่ (7 ประเทศ)
                'FORM44_41	ฟอร์ม ดี AICO ใหม่ (7 ประเทศ)
                'FORM441_4	ฟอร์ม ดี AICO Attach Sheet รถยนต์ ใหม่ (7 ประเทศ)
            Case "FORM44_4", "FORM44_44", "FORM44_41", "FORM441_4", "FORM44_01", "FORM44_02"
                SendPrinterName = ConfigurationManager.AppSettings("HY_002_02Edi_receiptFORM4").ToString()
                Return SendPrinterName
            Case "FORM4", "FORM44"
                SendPrinterName = ConfigurationManager.AppSettings("HY_002_02Edi_receiptFORM4").ToString()
                Return SendPrinterName
            Case "FORM4_1", "FORM441"
                SendPrinterName = ConfigurationManager.AppSettings("HY_002_02Edi_receiptFORM4_1").ToString()
                Return SendPrinterName

            Case "FORM4_2"
                SendPrinterName = ConfigurationManager.AppSettings("HY_002_02Edi_receiptFORM4_2").ToString()
                Return SendPrinterName
            Case "FORM4_3"
                SendPrinterName = ConfigurationManager.AppSettings("HY_002_02Edi_receiptFORM4_3").ToString()
                Return SendPrinterName
            Case "FORM4_4"
                SendPrinterName = ConfigurationManager.AppSettings("HY_002_02Edi_receiptFORM4_4").ToString()
                Return SendPrinterName
            Case "FORM4_5"
                SendPrinterName = ConfigurationManager.AppSettings("HY_002_02Edi_receiptFORM4_5").ToString()
                Return SendPrinterName
            Case "FORM4_6", "FORM4_61"
                SendPrinterName = ConfigurationManager.AppSettings("HY_002_02Edi_receiptFORM4_6").ToString()
                Return SendPrinterName
            Case "FORM4_8", "FORM4_81"
                SendPrinterName = ConfigurationManager.AppSettings("HY_002_02Edi_receiptFORM4_8").ToString()
                Return SendPrinterName
            Case "FORM4_9"
                SendPrinterName = ConfigurationManager.AppSettings("HY_002_02Edi_receiptFORM4_9").ToString()
                Return SendPrinterName
            Case "FORM4_91", "FORM4_911"
                SendPrinterName = ConfigurationManager.AppSettings("HY_002_02Edi_receiptFORM4_91").ToString()
                Return SendPrinterName
            Case "FORM5"
                SendPrinterName = ConfigurationManager.AppSettings("HY_002_02Edi_receiptFORM5").ToString()
                Return SendPrinterName
            Case "FORM5_1"
                SendPrinterName = ConfigurationManager.AppSettings("HY_002_02Edi_receiptFORM5_1").ToString()
                Return SendPrinterName

                ''ByTine 14-10-2558 ฟอร์มไทย-ชิลี FORm5_2 
            Case "FORM5_2"
                SendPrinterName = ConfigurationManager.AppSettings("HY_002_02Edi_receiptFORM5_2").ToString()
                Return SendPrinterName

            Case "FORM6"
                SendPrinterName = ConfigurationManager.AppSettings("HY_002_02Edi_receiptFORM6").ToString()
                Return SendPrinterName
            Case "FORM7"
                SendPrinterName = ConfigurationManager.AppSettings("HY_002_02Edi_receiptFORM7").ToString()
                Return SendPrinterName
            Case "FORM8"
                SendPrinterName = ConfigurationManager.AppSettings("HY_002_02Edi_receiptFORM8").ToString()
                Return SendPrinterName
            Case "FORM9"
                SendPrinterName = ConfigurationManager.AppSettings("HY_002_02Edi_receiptFORM9").ToString()
                Return SendPrinterName
            Case "FORMRussia"
                SendPrinterName = ConfigurationManager.AppSettings("HY_002_02Edi_receiptFORMRussia").ToString()
                Return SendPrinterName
        End Select
    End Function
#End Region

#End Region

#Region "   Site SK-004 'สคต.สระแก้ว Config Printer หนังสือรับรอง-คำขอ    "

#Region "   หนังสือรับรอง   "
    'หนังสือรับรอง SK-004 'สคต.สระแก้ว
    Public Shared Function Call_SitePrints_SK_004(ByVal Form_ID As String) As String
        Dim SendPrinterName As String
        Select Case Form_ID
            Case "FORM1"
                SendPrinterName = ConfigurationManager.AppSettings("SK_004Edi_FORM1").ToString()
                Return SendPrinterName
            Case "FORM1_1"
                SendPrinterName = ConfigurationManager.AppSettings("SK_004Edi_FORM1_1").ToString()
                Return SendPrinterName
            Case "FORM1_2"
                SendPrinterName = ConfigurationManager.AppSettings("SK_004Edi_FORM1_2").ToString()
                Return SendPrinterName
            Case "FORM1_3"
                SendPrinterName = ConfigurationManager.AppSettings("SK_004Edi_FORM1_3").ToString()
                Return SendPrinterName
            Case "FORM1_4"
                SendPrinterName = ConfigurationManager.AppSettings("SK_004Edi_FORM1_4").ToString()
                Return SendPrinterName
            Case "FORM2"
                SendPrinterName = ConfigurationManager.AppSettings("SK_004Edi_FORM2").ToString()
                Return SendPrinterName
            Case "FORM2_1"
                SendPrinterName = ConfigurationManager.AppSettings("SK_004Edi_FORM2_1").ToString()
                Return SendPrinterName
            Case "FORM2_2"
                SendPrinterName = ConfigurationManager.AppSettings("SK_004Edi_FORM2_2").ToString()
                Return SendPrinterName
            Case "FORM2_3"
                SendPrinterName = ConfigurationManager.AppSettings("SK_004Edi_FORM2_3").ToString()
                Return SendPrinterName
            Case "FORM2_4"
                SendPrinterName = ConfigurationManager.AppSettings("SK_004Edi_FORM2_4").ToString()
                Return SendPrinterName
            Case "FORM2_5"
                SendPrinterName = ConfigurationManager.AppSettings("SK_004Edi_FORM2_5").ToString()
                Return SendPrinterName
            Case "FORM2_6"
                SendPrinterName = ConfigurationManager.AppSettings("SK_004Edi_FORM2_6").ToString()
                Return SendPrinterName
            Case "FORM3"
                SendPrinterName = ConfigurationManager.AppSettings("SK_004Edi_FORM3").ToString()
                Return SendPrinterName
            Case "FORM3_1"
                SendPrinterName = ConfigurationManager.AppSettings("SK_004Edi_FORM3_1").ToString()
                Return SendPrinterName
                'by rut D new
                'FORM44_4	ฟอร์ม ดี (ATIGA) ใหม่ (7 ประเทศ)
                'FORM44_44	ฟอร์ม ดี (ATIGA) Attach Sheet รถยนต์ ใหม่ (7 ประเทศ)
                'FORM44_41	ฟอร์ม ดี AICO ใหม่ (7 ประเทศ)
                'FORM441_4	ฟอร์ม ดี AICO Attach Sheet รถยนต์ ใหม่ (7 ประเทศ)
            Case "FORM44_4", "FORM44_44", "FORM44_41", "FORM441_4", "FORM44_01", "FORM44_02"
                SendPrinterName = ConfigurationManager.AppSettings("SK_004Edi_FORM44ALL").ToString()
                Return SendPrinterName
            Case "FORM4", "FORM44"
                SendPrinterName = ConfigurationManager.AppSettings("SK_004Edi_FORM4").ToString()
                Return SendPrinterName
            Case "FORM4_1", "FORM441"
                SendPrinterName = ConfigurationManager.AppSettings("SK_004Edi_FORM4_1").ToString()
                Return SendPrinterName

            Case "FORM4_2"
                SendPrinterName = ConfigurationManager.AppSettings("SK_004Edi_FORM4_2").ToString()
                Return SendPrinterName
            Case "FORM4_3"
                SendPrinterName = ConfigurationManager.AppSettings("SK_004Edi_FORM4_3").ToString()
                Return SendPrinterName
            Case "FORM4_4"
                SendPrinterName = ConfigurationManager.AppSettings("SK_004Edi_FORM4_4").ToString()
                Return SendPrinterName
            Case "FORM4_5"
                SendPrinterName = ConfigurationManager.AppSettings("SK_004Edi_FORM4_5").ToString()
                Return SendPrinterName
            Case "FORM4_6", "FORM4_61"
                SendPrinterName = ConfigurationManager.AppSettings("SK_004Edi_FORM4_6").ToString()
                Return SendPrinterName
            Case "FORM4_8", "FORM4_81"
                SendPrinterName = ConfigurationManager.AppSettings("SK_004Edi_FORM4_8").ToString()
                Return SendPrinterName
            Case "FORM4_9"
                SendPrinterName = ConfigurationManager.AppSettings("SK_004Edi_FORM4_9").ToString()
                Return SendPrinterName
            Case "FORM4_91", "FORM4_911"
                SendPrinterName = ConfigurationManager.AppSettings("SK_004Edi_FORM4_91").ToString()
                Return SendPrinterName
            Case "FORM5"
                SendPrinterName = ConfigurationManager.AppSettings("SK_004Edi_FORM5").ToString()
                Return SendPrinterName
            Case "FORM5_1"
                SendPrinterName = ConfigurationManager.AppSettings("SK_004Edi_FORM5_1").ToString()
                Return SendPrinterName

                ''ByTine 14-10-2558 ฟอร์มไทย-ชิลี FORm5_2 
            Case "FORM5_2"
                SendPrinterName = ConfigurationManager.AppSettings("SK_004Edi_FORM5_2").ToString()
                Return SendPrinterName

            Case "FORM6"
                SendPrinterName = ConfigurationManager.AppSettings("SK_004Edi_FORM6").ToString()
                Return SendPrinterName
            Case "FORM7"
                SendPrinterName = ConfigurationManager.AppSettings("SK_004Edi_FORM7").ToString()
                Return SendPrinterName
            Case "FORM8"
                SendPrinterName = ConfigurationManager.AppSettings("SK_004Edi_FORM8").ToString()
                Return SendPrinterName
            Case "FORM9"
                SendPrinterName = ConfigurationManager.AppSettings("SK_004Edi_FORM9").ToString()
                Return SendPrinterName
            Case "FORMRussia"
                SendPrinterName = ConfigurationManager.AppSettings("SK_004Edi_FORMRussia").ToString()
                Return SendPrinterName

        End Select
    End Function
#End Region

#Region "   คำขอ เครื่อง 1   "
    'เครื่องพิมพ์คำขอ SK-004 'สคต.สระแก้ว
    Public Shared Function Call_SiteReceipt_SK_004_01(ByVal Form_ID As String) As String
        Dim SendPrinterName As String
        Select Case Form_ID
            Case "FORM1"
                SendPrinterName = ConfigurationManager.AppSettings("SK_004_01Edi_receiptFORM1").ToString()
                Return SendPrinterName
            Case "FORM1_1"
                SendPrinterName = ConfigurationManager.AppSettings("SK_004_01Edi_receiptFORM1_1").ToString()
                Return SendPrinterName
            Case "FORM1_2"
                SendPrinterName = ConfigurationManager.AppSettings("SK_004_01Edi_receiptFORM1_2").ToString()
                Return SendPrinterName
            Case "FORM1_3"
                SendPrinterName = ConfigurationManager.AppSettings("SK_004_01Edi_receiptFORM1_3").ToString()
                Return SendPrinterName
            Case "FORM1_4"
                SendPrinterName = ConfigurationManager.AppSettings("SK_004_01Edi_receiptFORM1_4").ToString()
                Return SendPrinterName
            Case "FORM2"
                SendPrinterName = ConfigurationManager.AppSettings("SK_004_01Edi_receiptFORM2").ToString()
                Return SendPrinterName
            Case "FORM2_1"
                SendPrinterName = ConfigurationManager.AppSettings("SK_004_01Edi_receiptFORM2_1").ToString()
                Return SendPrinterName
            Case "FORM2_2"
                SendPrinterName = ConfigurationManager.AppSettings("SK_004_01Edi_receiptFORM2_2").ToString()
                Return SendPrinterName
            Case "FORM2_3"
                SendPrinterName = ConfigurationManager.AppSettings("SK_004_01Edi_receiptFORM2_3").ToString()
                Return SendPrinterName
            Case "FORM2_4"
                SendPrinterName = ConfigurationManager.AppSettings("SK_004_01Edi_receiptFORM2_4").ToString()
                Return SendPrinterName
            Case "FORM2_5"
                SendPrinterName = ConfigurationManager.AppSettings("SK_004_01Edi_receiptFORM2_5").ToString()
                Return SendPrinterName
            Case "FORM2_6"
                SendPrinterName = ConfigurationManager.AppSettings("SK_004_01Edi_receiptFORM2_6").ToString()
                Return SendPrinterName
            Case "FORM3"
                SendPrinterName = ConfigurationManager.AppSettings("SK_004_01Edi_receiptFORM3").ToString()
                Return SendPrinterName
            Case "FORM3_1"
                SendPrinterName = ConfigurationManager.AppSettings("SK_004_01Edi_receiptFORM3_1").ToString()
                Return SendPrinterName
                'by rut D new
                'FORM44_4	ฟอร์ม ดี (ATIGA) ใหม่ (7 ประเทศ)
                'FORM44_44	ฟอร์ม ดี (ATIGA) Attach Sheet รถยนต์ ใหม่ (7 ประเทศ)
                'FORM44_41	ฟอร์ม ดี AICO ใหม่ (7 ประเทศ)
                'FORM441_4	ฟอร์ม ดี AICO Attach Sheet รถยนต์ ใหม่ (7 ประเทศ)
            Case "FORM44_4", "FORM44_44", "FORM44_41", "FORM441_4", "FORM44_01", "FORM44_02"
                SendPrinterName = ConfigurationManager.AppSettings("SK_004_01Edi_receiptFORM4").ToString()
                Return SendPrinterName
            Case "FORM4", "FORM44"
                SendPrinterName = ConfigurationManager.AppSettings("SK_004_01Edi_receiptFORM4").ToString()
                Return SendPrinterName
            Case "FORM4_1", "FORM441"
                SendPrinterName = ConfigurationManager.AppSettings("SK_004_01Edi_receiptFORM4_1").ToString()
                Return SendPrinterName

            Case "FORM4_2"
                SendPrinterName = ConfigurationManager.AppSettings("SK_004_01Edi_receiptFORM4_2").ToString()
                Return SendPrinterName
            Case "FORM4_3"
                SendPrinterName = ConfigurationManager.AppSettings("SK_004_01Edi_receiptFORM4_3").ToString()
                Return SendPrinterName
            Case "FORM4_4"
                SendPrinterName = ConfigurationManager.AppSettings("SK_004_01Edi_receiptFORM4_4").ToString()
                Return SendPrinterName
            Case "FORM4_5"
                SendPrinterName = ConfigurationManager.AppSettings("SK_004_01Edi_receiptFORM4_5").ToString()
                Return SendPrinterName
            Case "FORM4_6", "FORM4_61"
                SendPrinterName = ConfigurationManager.AppSettings("SK_004_01Edi_receiptFORM4_6").ToString()
                Return SendPrinterName
            Case "FORM4_8", "FORM4_81"
                SendPrinterName = ConfigurationManager.AppSettings("SK_004_01Edi_receiptFORM4_8").ToString()
                Return SendPrinterName
            Case "FORM4_9"
                SendPrinterName = ConfigurationManager.AppSettings("SK_004_01Edi_receiptFORM4_9").ToString()
                Return SendPrinterName
            Case "FORM4_91", "FORM4_911"
                SendPrinterName = ConfigurationManager.AppSettings("SK_004_01Edi_receiptFORM4_91").ToString()
                Return SendPrinterName
            Case "FORM5"
                SendPrinterName = ConfigurationManager.AppSettings("SK_004_01Edi_receiptFORM5").ToString()
                Return SendPrinterName
            Case "FORM5_1"
                SendPrinterName = ConfigurationManager.AppSettings("SK_004_01Edi_receiptFORM5_1").ToString()
                Return SendPrinterName

                ''ByTine 14-10-2558 ฟอร์มไทย-ชิลี FORm5_2 
            Case "FORM5_2"
                SendPrinterName = ConfigurationManager.AppSettings("SK_004_01Edi_receiptFORM5_2").ToString()
                Return SendPrinterName

            Case "FORM6"
                SendPrinterName = ConfigurationManager.AppSettings("SK_004_01Edi_receiptFORM6").ToString()
                Return SendPrinterName
            Case "FORM7"
                SendPrinterName = ConfigurationManager.AppSettings("SK_004_01Edi_receiptFORM7").ToString()
                Return SendPrinterName
            Case "FORM8"
                SendPrinterName = ConfigurationManager.AppSettings("SK_004_01Edi_receiptFORM8").ToString()
                Return SendPrinterName
            Case "FORM9"
                SendPrinterName = ConfigurationManager.AppSettings("SK_004_01Edi_receiptFORM9").ToString()
                Return SendPrinterName
            Case "FORMRussia"
                SendPrinterName = ConfigurationManager.AppSettings("SK_004_01Edi_receiptFORMRussia").ToString()
                Return SendPrinterName
        End Select
    End Function

#End Region

#End Region

#Region "   Site NK-005 'สคต.หนองคาย Config Printer หนังสือรับรอง-คำขอ    "

#Region "   หนังสือรับรอง   "
    'หนังสือรับรอง NK-005 'สคต.หนองคาย
    Public Shared Function Call_SitePrints_NK_005(ByVal Form_ID As String) As String
        Dim SendPrinterName As String
        Select Case Form_ID
            Case "FORM1"
                SendPrinterName = ConfigurationManager.AppSettings("NK_005Edi_FORM1").ToString()
                Return SendPrinterName
            Case "FORM1_1"
                SendPrinterName = ConfigurationManager.AppSettings("NK_005Edi_FORM1_1").ToString()
                Return SendPrinterName
            Case "FORM1_2"
                SendPrinterName = ConfigurationManager.AppSettings("NK_005Edi_FORM1_2").ToString()
                Return SendPrinterName
            Case "FORM1_3"
                SendPrinterName = ConfigurationManager.AppSettings("NK_005Edi_FORM1_3").ToString()
                Return SendPrinterName
            Case "FORM1_4"
                SendPrinterName = ConfigurationManager.AppSettings("NK_005Edi_FORM1_4").ToString()
                Return SendPrinterName
            Case "FORM2"
                SendPrinterName = ConfigurationManager.AppSettings("NK_005Edi_FORM2").ToString()
                Return SendPrinterName
            Case "FORM2_1"
                SendPrinterName = ConfigurationManager.AppSettings("NK_005Edi_FORM2_1").ToString()
                Return SendPrinterName
            Case "FORM2_2"
                SendPrinterName = ConfigurationManager.AppSettings("NK_005Edi_FORM2_2").ToString()
                Return SendPrinterName
            Case "FORM2_3"
                SendPrinterName = ConfigurationManager.AppSettings("NK_005Edi_FORM2_3").ToString()
                Return SendPrinterName
            Case "FORM2_4"
                SendPrinterName = ConfigurationManager.AppSettings("NK_005Edi_FORM2_4").ToString()
                Return SendPrinterName
            Case "FORM2_5"
                SendPrinterName = ConfigurationManager.AppSettings("NK_005Edi_FORM2_5").ToString()
                Return SendPrinterName
            Case "FORM2_6"
                SendPrinterName = ConfigurationManager.AppSettings("NK_005Edi_FORM2_6").ToString()
                Return SendPrinterName
            Case "FORM3"
                SendPrinterName = ConfigurationManager.AppSettings("NK_005Edi_FORM3").ToString()
                Return SendPrinterName
            Case "FORM3_1"
                SendPrinterName = ConfigurationManager.AppSettings("NK_005Edi_FORM3_1").ToString()
                Return SendPrinterName
                'by rut D new
                'FORM44_4	ฟอร์ม ดี (ATIGA) ใหม่ (7 ประเทศ)
                'FORM44_44	ฟอร์ม ดี (ATIGA) Attach Sheet รถยนต์ ใหม่ (7 ประเทศ)
                'FORM44_41	ฟอร์ม ดี AICO ใหม่ (7 ประเทศ)
                'FORM441_4	ฟอร์ม ดี AICO Attach Sheet รถยนต์ ใหม่ (7 ประเทศ)
            Case "FORM44_4", "FORM44_44", "FORM44_41", "FORM441_4", "FORM44_01", "FORM44_02"
                SendPrinterName = ConfigurationManager.AppSettings("NK_005Edi_FORM44ALL").ToString()
                Return SendPrinterName
            Case "FORM4", "FORM44"
                SendPrinterName = ConfigurationManager.AppSettings("NK_005Edi_FORM4").ToString()
                Return SendPrinterName
            Case "FORM4_1", "FORM441"
                SendPrinterName = ConfigurationManager.AppSettings("NK_005Edi_FORM4_1").ToString()
                Return SendPrinterName

            Case "FORM4_2"
                SendPrinterName = ConfigurationManager.AppSettings("NK_005Edi_FORM4_2").ToString()
                Return SendPrinterName
            Case "FORM4_3"
                SendPrinterName = ConfigurationManager.AppSettings("NK_005Edi_FORM4_3").ToString()
                Return SendPrinterName
            Case "FORM4_4"
                SendPrinterName = ConfigurationManager.AppSettings("NK_005Edi_FORM4_4").ToString()
                Return SendPrinterName
            Case "FORM4_5"
                SendPrinterName = ConfigurationManager.AppSettings("NK_005Edi_FORM4_5").ToString()
                Return SendPrinterName
            Case "FORM4_6", "FORM4_61"
                SendPrinterName = ConfigurationManager.AppSettings("NK_005Edi_FORM4_6").ToString()
                Return SendPrinterName
            Case "FORM4_8", "FORM4_81"
                SendPrinterName = ConfigurationManager.AppSettings("NK_005Edi_FORM4_8").ToString()
                Return SendPrinterName
            Case "FORM4_9"
                SendPrinterName = ConfigurationManager.AppSettings("NK_005Edi_FORM4_9").ToString()
                Return SendPrinterName
            Case "FORM4_91", "FORM4_911"
                SendPrinterName = ConfigurationManager.AppSettings("NK_005Edi_FORM4_91").ToString()
                Return SendPrinterName
            Case "FORM5"
                SendPrinterName = ConfigurationManager.AppSettings("NK_005Edi_FORM5").ToString()
                Return SendPrinterName
            Case "FORM5_1"
                SendPrinterName = ConfigurationManager.AppSettings("NK_005Edi_FORM5_1").ToString()
                Return SendPrinterName

                ''ByTine 14-10-2558 ฟอร์มไทย-ชิลี FORm5_2 
            Case "FORM5_2"
                SendPrinterName = ConfigurationManager.AppSettings("NK_005Edi_FORM5_2").ToString()
                Return SendPrinterName

            Case "FORM6"
                SendPrinterName = ConfigurationManager.AppSettings("NK_005Edi_FORM6").ToString()
                Return SendPrinterName
            Case "FORM7"
                SendPrinterName = ConfigurationManager.AppSettings("NK_005Edi_FORM7").ToString()
                Return SendPrinterName
            Case "FORM8"
                SendPrinterName = ConfigurationManager.AppSettings("NK_005Edi_FORM8").ToString()
                Return SendPrinterName
            Case "FORM9"
                SendPrinterName = ConfigurationManager.AppSettings("NK_005Edi_FORM9").ToString()
                Return SendPrinterName
            Case "FORMRussia"
                SendPrinterName = ConfigurationManager.AppSettings("NK_005Edi_FORMRussia").ToString()
                Return SendPrinterName

        End Select
    End Function
#End Region

#Region "   คำขอ เครื่อง 1   "
    'เครื่องพิมพ์คำขอ NK-005 'สคต.หนองคาย
    Public Shared Function Call_SiteReceipt_NK_005_01(ByVal Form_ID As String) As String
        Dim SendPrinterName As String
        Select Case Form_ID
            Case "FORM1"
                SendPrinterName = ConfigurationManager.AppSettings("NK_005_01Edi_receiptFORM1").ToString()
                Return SendPrinterName
            Case "FORM1_1"
                SendPrinterName = ConfigurationManager.AppSettings("NK_005_01Edi_receiptFORM1_1").ToString()
                Return SendPrinterName
            Case "FORM1_2"
                SendPrinterName = ConfigurationManager.AppSettings("NK_005_01Edi_receiptFORM1_2").ToString()
                Return SendPrinterName
            Case "FORM1_3"
                SendPrinterName = ConfigurationManager.AppSettings("NK_005_01Edi_receiptFORM1_3").ToString()
                Return SendPrinterName
            Case "FORM1_4"
                SendPrinterName = ConfigurationManager.AppSettings("NK_005_01Edi_receiptFORM1_4").ToString()
                Return SendPrinterName
            Case "FORM2"
                SendPrinterName = ConfigurationManager.AppSettings("NK_005_01Edi_receiptFORM2").ToString()
                Return SendPrinterName
            Case "FORM2_1"
                SendPrinterName = ConfigurationManager.AppSettings("NK_005_01Edi_receiptFORM2_1").ToString()
                Return SendPrinterName
            Case "FORM2_2"
                SendPrinterName = ConfigurationManager.AppSettings("NK_005_01Edi_receiptFORM2_2").ToString()
                Return SendPrinterName
            Case "FORM2_3"
                SendPrinterName = ConfigurationManager.AppSettings("NK_005_01Edi_receiptFORM2_3").ToString()
                Return SendPrinterName
            Case "FORM2_4"
                SendPrinterName = ConfigurationManager.AppSettings("NK_005_01Edi_receiptFORM2_4").ToString()
                Return SendPrinterName
            Case "FORM2_5"
                SendPrinterName = ConfigurationManager.AppSettings("NK_005_01Edi_receiptFORM2_5").ToString()
                Return SendPrinterName
            Case "FORM2_6"
                SendPrinterName = ConfigurationManager.AppSettings("NK_005_01Edi_receiptFORM2_6").ToString()
                Return SendPrinterName
            Case "FORM3"
                SendPrinterName = ConfigurationManager.AppSettings("NK_005_01Edi_receiptFORM3").ToString()
                Return SendPrinterName
            Case "FORM3_1"
                SendPrinterName = ConfigurationManager.AppSettings("NK_005_01Edi_receiptFORM3_1").ToString()
                Return SendPrinterName
                'by rut D new
                'FORM44_4	ฟอร์ม ดี (ATIGA) ใหม่ (7 ประเทศ)
                'FORM44_44	ฟอร์ม ดี (ATIGA) Attach Sheet รถยนต์ ใหม่ (7 ประเทศ)
                'FORM44_41	ฟอร์ม ดี AICO ใหม่ (7 ประเทศ)
                'FORM441_4	ฟอร์ม ดี AICO Attach Sheet รถยนต์ ใหม่ (7 ประเทศ)
            Case "FORM44_4", "FORM44_44", "FORM44_41", "FORM441_4", "FORM44_01", "FORM44_02"
                SendPrinterName = ConfigurationManager.AppSettings("NK_005_01Edi_receiptFORM4").ToString()
                Return SendPrinterName
            Case "FORM4", "FORM44"
                SendPrinterName = ConfigurationManager.AppSettings("NK_005_01Edi_receiptFORM4").ToString()
                Return SendPrinterName
            Case "FORM4_1", "FORM441"
                SendPrinterName = ConfigurationManager.AppSettings("NK_005_01Edi_receiptFORM4_1").ToString()
                Return SendPrinterName

            Case "FORM4_2"
                SendPrinterName = ConfigurationManager.AppSettings("NK_005_01Edi_receiptFORM4_2").ToString()
                Return SendPrinterName
            Case "FORM4_3"
                SendPrinterName = ConfigurationManager.AppSettings("NK_005_01Edi_receiptFORM4_3").ToString()
                Return SendPrinterName
            Case "FORM4_4"
                SendPrinterName = ConfigurationManager.AppSettings("NK_005_01Edi_receiptFORM4_4").ToString()
                Return SendPrinterName
            Case "FORM4_5"
                SendPrinterName = ConfigurationManager.AppSettings("NK_005_01Edi_receiptFORM4_5").ToString()
                Return SendPrinterName
            Case "FORM4_6", "FORM4_61"
                SendPrinterName = ConfigurationManager.AppSettings("NK_005_01Edi_receiptFORM4_6").ToString()
                Return SendPrinterName
            Case "FORM4_8", "FORM4_81"
                SendPrinterName = ConfigurationManager.AppSettings("NK_005_01Edi_receiptFORM4_8").ToString()
                Return SendPrinterName
            Case "FORM4_9"
                SendPrinterName = ConfigurationManager.AppSettings("NK_005_01Edi_receiptFORM4_9").ToString()
                Return SendPrinterName
            Case "FORM4_91", "FORM4_911"
                SendPrinterName = ConfigurationManager.AppSettings("NK_005_01Edi_receiptFORM4_91").ToString()
                Return SendPrinterName
            Case "FORM5"
                SendPrinterName = ConfigurationManager.AppSettings("NK_005_01Edi_receiptFORM5").ToString()
                Return SendPrinterName
            Case "FORM5_1"
                SendPrinterName = ConfigurationManager.AppSettings("NK_005_01Edi_receiptFORM5_1").ToString()
                Return SendPrinterName

                ''ByTine 14-10-2558 ฟอร์มไทย-ชิลี FORm5_2 
            Case "FORM5_2"
                SendPrinterName = ConfigurationManager.AppSettings("NK_005_01Edi_receiptFORM5_2").ToString()
                Return SendPrinterName

            Case "FORM6"
                SendPrinterName = ConfigurationManager.AppSettings("NK_005_01Edi_receiptFORM6").ToString()
                Return SendPrinterName
            Case "FORM7"
                SendPrinterName = ConfigurationManager.AppSettings("NK_005_01Edi_receiptFORM7").ToString()
                Return SendPrinterName
            Case "FORM8"
                SendPrinterName = ConfigurationManager.AppSettings("NK_005_01Edi_receiptFORM8").ToString()
                Return SendPrinterName
            Case "FORM9"
                SendPrinterName = ConfigurationManager.AppSettings("NK_005_01Edi_receiptFORM9").ToString()
                Return SendPrinterName
            Case "FORMRussia"
                SendPrinterName = ConfigurationManager.AppSettings("NK_005_01Edi_receiptFORMRussia").ToString()
                Return SendPrinterName
        End Select
    End Function

#End Region

#End Region

#Region "   Site SG-007 สคต.ศรีสะเกษ Config Printer หนังสือรับรอง-คำขอ    "

#Region "   หนังสือรับรอง   "
    'หนังสือรับรอง SG-007 'สคต.ศรีสะเกษ
    Public Shared Function Call_SitePrints_SG_007(ByVal Form_ID As String) As String
        Dim SendPrinterName As String
        Select Case Form_ID
            Case "FORM1"
                SendPrinterName = ConfigurationManager.AppSettings("SG_007Edi_FORM1").ToString()
                Return SendPrinterName
            Case "FORM1_1"
                SendPrinterName = ConfigurationManager.AppSettings("SG_007Edi_FORM1_1").ToString()
                Return SendPrinterName
            Case "FORM1_2"
                SendPrinterName = ConfigurationManager.AppSettings("SG_007Edi_FORM1_2").ToString()
                Return SendPrinterName
            Case "FORM1_3"
                SendPrinterName = ConfigurationManager.AppSettings("SG_007Edi_FORM1_3").ToString()
                Return SendPrinterName
            Case "FORM1_4"
                SendPrinterName = ConfigurationManager.AppSettings("SG_007Edi_FORM1_4").ToString()
                Return SendPrinterName
            Case "FORM2"
                SendPrinterName = ConfigurationManager.AppSettings("SG_007Edi_FORM2").ToString()
                Return SendPrinterName
            Case "FORM2_1"
                SendPrinterName = ConfigurationManager.AppSettings("SG_007Edi_FORM2_1").ToString()
                Return SendPrinterName
            Case "FORM2_2"
                SendPrinterName = ConfigurationManager.AppSettings("SG_007Edi_FORM2_2").ToString()
                Return SendPrinterName
            Case "FORM2_3"
                SendPrinterName = ConfigurationManager.AppSettings("SG_007Edi_FORM2_3").ToString()
                Return SendPrinterName
            Case "FORM2_4"
                SendPrinterName = ConfigurationManager.AppSettings("SG_007Edi_FORM2_4").ToString()
                Return SendPrinterName
            Case "FORM2_5"
                SendPrinterName = ConfigurationManager.AppSettings("SG_007Edi_FORM2_5").ToString()
                Return SendPrinterName
            Case "FORM2_6"
                SendPrinterName = ConfigurationManager.AppSettings("SG_007Edi_FORM2_6").ToString()
                Return SendPrinterName
            Case "FORM3"
                SendPrinterName = ConfigurationManager.AppSettings("SG_007Edi_FORM3").ToString()
                Return SendPrinterName
            Case "FORM3_1"
                SendPrinterName = ConfigurationManager.AppSettings("SG_007Edi_FORM3_1").ToString()
                Return SendPrinterName
                'by rut D new
                'FORM44_4	ฟอร์ม ดี (ATIGA) ใหม่ (7 ประเทศ)
                'FORM44_44	ฟอร์ม ดี (ATIGA) Attach Sheet รถยนต์ ใหม่ (7 ประเทศ)
                'FORM44_41	ฟอร์ม ดี AICO ใหม่ (7 ประเทศ)
                'FORM441_4	ฟอร์ม ดี AICO Attach Sheet รถยนต์ ใหม่ (7 ประเทศ)
            Case "FORM44_4", "FORM44_44", "FORM44_41", "FORM441_4", "FORM44_01", "FORM44_02"
                SendPrinterName = ConfigurationManager.AppSettings("SG_007Edi_FORM44ALL").ToString()
                Return SendPrinterName
            Case "FORM4", "FORM44"
                SendPrinterName = ConfigurationManager.AppSettings("SG_007Edi_FORM4").ToString()
                Return SendPrinterName
            Case "FORM4_1", "FORM441"
                SendPrinterName = ConfigurationManager.AppSettings("SG_007Edi_FORM4_1").ToString()
                Return SendPrinterName

            Case "FORM4_2"
                SendPrinterName = ConfigurationManager.AppSettings("SG_007Edi_FORM4_2").ToString()
                Return SendPrinterName
            Case "FORM4_3"
                SendPrinterName = ConfigurationManager.AppSettings("SG_007Edi_FORM4_3").ToString()
                Return SendPrinterName
            Case "FORM4_4"
                SendPrinterName = ConfigurationManager.AppSettings("SG_007Edi_FORM4_4").ToString()
                Return SendPrinterName
            Case "FORM4_5"
                SendPrinterName = ConfigurationManager.AppSettings("SG_007Edi_FORM4_5").ToString()
                Return SendPrinterName
            Case "FORM4_6", "FORM4_61"
                SendPrinterName = ConfigurationManager.AppSettings("SG_007Edi_FORM4_6").ToString()
                Return SendPrinterName
            Case "FORM4_8", "FORM4_81"
                SendPrinterName = ConfigurationManager.AppSettings("SG_007Edi_FORM4_8").ToString()
                Return SendPrinterName
            Case "FORM4_9"
                SendPrinterName = ConfigurationManager.AppSettings("SG_007Edi_FORM4_9").ToString()
                Return SendPrinterName
            Case "FORM4_91", "FORM4_911"
                SendPrinterName = ConfigurationManager.AppSettings("SG_007Edi_FORM4_91").ToString()
                Return SendPrinterName
            Case "FORM5"
                SendPrinterName = ConfigurationManager.AppSettings("SG_007Edi_FORM5").ToString()
                Return SendPrinterName
            Case "FORM5_1"
                SendPrinterName = ConfigurationManager.AppSettings("SG_007Edi_FORM5_1").ToString()
                Return SendPrinterName

                ''ByTine 14-10-2558 ฟอร์มไทย-ชิลี FORm5_2 
            Case "FORM5_2"
                SendPrinterName = ConfigurationManager.AppSettings("SG_007Edi_FORM5_2").ToString()
                Return SendPrinterName

            Case "FORM6"
                SendPrinterName = ConfigurationManager.AppSettings("SG_007Edi_FORM6").ToString()
                Return SendPrinterName
            Case "FORM7"
                SendPrinterName = ConfigurationManager.AppSettings("SG_007Edi_FORM7").ToString()
                Return SendPrinterName
            Case "FORM8"
                SendPrinterName = ConfigurationManager.AppSettings("SG_007Edi_FORM8").ToString()
                Return SendPrinterName
            Case "FORM9"
                SendPrinterName = ConfigurationManager.AppSettings("SG_007Edi_FORM9").ToString()
                Return SendPrinterName
            Case "FORMRussia"
                SendPrinterName = ConfigurationManager.AppSettings("SG_007Edi_FORMRussia").ToString()
                Return SendPrinterName

        End Select
    End Function
#End Region

#Region "   คำขอ เครื่อง 1   "
    'เครื่องพิมพ์คำขอ สคต.ศรีสะเกษ SG-007
    Public Shared Function Call_SiteReceipt_SG_007_01(ByVal Form_ID As String) As String
        Dim SendPrinterName As String
        Select Case Form_ID
            Case "FORM1"
                SendPrinterName = ConfigurationManager.AppSettings("SG_007_01Edi_receiptFORM1").ToString()
                Return SendPrinterName
            Case "FORM1_1"
                SendPrinterName = ConfigurationManager.AppSettings("SG_007_01Edi_receiptFORM1_1").ToString()
                Return SendPrinterName
            Case "FORM1_2"
                SendPrinterName = ConfigurationManager.AppSettings("SG_007_01Edi_receiptFORM1_2").ToString()
                Return SendPrinterName
            Case "FORM1_3"
                SendPrinterName = ConfigurationManager.AppSettings("SG_007_01Edi_receiptFORM1_3").ToString()
                Return SendPrinterName
            Case "FORM1_4"
                SendPrinterName = ConfigurationManager.AppSettings("SG_007_01Edi_receiptFORM1_4").ToString()
                Return SendPrinterName
            Case "FORM2"
                SendPrinterName = ConfigurationManager.AppSettings("SG_007_01Edi_receiptFORM2").ToString()
                Return SendPrinterName
            Case "FORM2_1"
                SendPrinterName = ConfigurationManager.AppSettings("SG_007_01Edi_receiptFORM2_1").ToString()
                Return SendPrinterName
            Case "FORM2_2"
                SendPrinterName = ConfigurationManager.AppSettings("SG_007_01Edi_receiptFORM2_2").ToString()
                Return SendPrinterName
            Case "FORM2_3"
                SendPrinterName = ConfigurationManager.AppSettings("SG_007_01Edi_receiptFORM2_3").ToString()
                Return SendPrinterName
            Case "FORM2_4"
                SendPrinterName = ConfigurationManager.AppSettings("SG_007_01Edi_receiptFORM2_4").ToString()
                Return SendPrinterName
            Case "FORM2_5"
                SendPrinterName = ConfigurationManager.AppSettings("SG_007_01Edi_receiptFORM2_5").ToString()
                Return SendPrinterName
            Case "FORM2_6"
                SendPrinterName = ConfigurationManager.AppSettings("SG_007_01Edi_receiptFORM2_6").ToString()
                Return SendPrinterName
            Case "FORM3"
                SendPrinterName = ConfigurationManager.AppSettings("SG_007_01Edi_receiptFORM3").ToString()
                Return SendPrinterName
            Case "FORM3_1"
                SendPrinterName = ConfigurationManager.AppSettings("SG_007_01Edi_receiptFORM3_1").ToString()
                Return SendPrinterName
                'by rut D new
                'FORM44_4	ฟอร์ม ดี (ATIGA) ใหม่ (7 ประเทศ)
                'FORM44_44	ฟอร์ม ดี (ATIGA) Attach Sheet รถยนต์ ใหม่ (7 ประเทศ)
                'FORM44_41	ฟอร์ม ดี AICO ใหม่ (7 ประเทศ)
                'FORM441_4	ฟอร์ม ดี AICO Attach Sheet รถยนต์ ใหม่ (7 ประเทศ)
            Case "FORM44_4", "FORM44_44", "FORM44_41", "FORM441_4", "FORM44_01", "FORM44_02"
                SendPrinterName = ConfigurationManager.AppSettings("SG_007_01Edi_receiptFORM4").ToString()
                Return SendPrinterName
            Case "FORM4", "FORM44"
                SendPrinterName = ConfigurationManager.AppSettings("SG_007_01Edi_receiptFORM4").ToString()
                Return SendPrinterName
            Case "FORM4_1", "FORM441"
                SendPrinterName = ConfigurationManager.AppSettings("SG_007_01Edi_receiptFORM4_1").ToString()
                Return SendPrinterName

            Case "FORM4_2"
                SendPrinterName = ConfigurationManager.AppSettings("SG_007_01Edi_receiptFORM4_2").ToString()
                Return SendPrinterName
            Case "FORM4_3"
                SendPrinterName = ConfigurationManager.AppSettings("SG_007_01Edi_receiptFORM4_3").ToString()
                Return SendPrinterName
            Case "FORM4_4"
                SendPrinterName = ConfigurationManager.AppSettings("SG_007_01Edi_receiptFORM4_4").ToString()
                Return SendPrinterName
            Case "FORM4_5"
                SendPrinterName = ConfigurationManager.AppSettings("SG_007_01Edi_receiptFORM4_5").ToString()
                Return SendPrinterName
            Case "FORM4_6", "FORM4_61"
                SendPrinterName = ConfigurationManager.AppSettings("SG_007_01Edi_receiptFORM4_6").ToString()
                Return SendPrinterName
            Case "FORM4_8", "FORM4_81"
                SendPrinterName = ConfigurationManager.AppSettings("SG_007_01Edi_receiptFORM4_8").ToString()
                Return SendPrinterName
            Case "FORM4_9"
                SendPrinterName = ConfigurationManager.AppSettings("SG_007_01Edi_receiptFORM4_9").ToString()
                Return SendPrinterName
            Case "FORM4_91", "FORM4_911"
                SendPrinterName = ConfigurationManager.AppSettings("SG_007_01Edi_receiptFORM4_91").ToString()
                Return SendPrinterName
            Case "FORM5"
                SendPrinterName = ConfigurationManager.AppSettings("SG_007_01Edi_receiptFORM5").ToString()
                Return SendPrinterName
            Case "FORM5_1"
                SendPrinterName = ConfigurationManager.AppSettings("SG_007_01Edi_receiptFORM5_1").ToString()
                Return SendPrinterName

                ''ByTine 14-10-2558 ฟอร์มไทย-ชิลี FORm5_2 
            Case "FORM5_2"
                SendPrinterName = ConfigurationManager.AppSettings("SG_007_01Edi_receiptFORM5_2").ToString()
                Return SendPrinterName

            Case "FORM6"
                SendPrinterName = ConfigurationManager.AppSettings("SG_007_01Edi_receiptFORM6").ToString()
                Return SendPrinterName
            Case "FORM7"
                SendPrinterName = ConfigurationManager.AppSettings("SG_007_01Edi_receiptFORM7").ToString()
                Return SendPrinterName
            Case "FORM8"
                SendPrinterName = ConfigurationManager.AppSettings("SG_007_01Edi_receiptFORM8").ToString()
                Return SendPrinterName
            Case "FORM9"
                SendPrinterName = ConfigurationManager.AppSettings("SG_007_01Edi_receiptFORM9").ToString()
                Return SendPrinterName
            Case "FORMRussia"
                SendPrinterName = ConfigurationManager.AppSettings("SG_007_01Edi_receiptFORMRussia").ToString()
                Return SendPrinterName
        End Select
    End Function
#End Region

#Region "   คำขอ เครื่อง 2   "
    'เครื่องพิมพ์คำขอ สคต.ศรีสะเกษ SG-007
    Public Shared Function Call_SiteReceipt_SG_007_02(ByVal Form_ID As String) As String
        Dim SendPrinterName As String
        Select Case Form_ID
            Case "FORM1"
                SendPrinterName = ConfigurationManager.AppSettings("SG_007_02Edi_receiptFORM1").ToString()
                Return SendPrinterName
            Case "FORM1_1"
                SendPrinterName = ConfigurationManager.AppSettings("SG_007_02Edi_receiptFORM1_1").ToString()
                Return SendPrinterName
            Case "FORM1_2"
                SendPrinterName = ConfigurationManager.AppSettings("SG_007_02Edi_receiptFORM1_2").ToString()
                Return SendPrinterName
            Case "FORM1_3"
                SendPrinterName = ConfigurationManager.AppSettings("SG_007_02Edi_receiptFORM1_3").ToString()
                Return SendPrinterName
            Case "FORM1_4"
                SendPrinterName = ConfigurationManager.AppSettings("SG_007_02Edi_receiptFORM1_4").ToString()
                Return SendPrinterName
            Case "FORM2"
                SendPrinterName = ConfigurationManager.AppSettings("SG_007_02Edi_receiptFORM2").ToString()
                Return SendPrinterName
            Case "FORM2_1"
                SendPrinterName = ConfigurationManager.AppSettings("SG_007_02Edi_receiptFORM2_1").ToString()
                Return SendPrinterName
            Case "FORM2_2"
                SendPrinterName = ConfigurationManager.AppSettings("SG_007_02Edi_receiptFORM2_2").ToString()
                Return SendPrinterName
            Case "FORM2_3"
                SendPrinterName = ConfigurationManager.AppSettings("SG_007_02Edi_receiptFORM2_3").ToString()
                Return SendPrinterName
            Case "FORM2_4"
                SendPrinterName = ConfigurationManager.AppSettings("SG_007_02Edi_receiptFORM2_4").ToString()
                Return SendPrinterName
            Case "FORM2_5"
                SendPrinterName = ConfigurationManager.AppSettings("SG_007_02Edi_receiptFORM2_5").ToString()
                Return SendPrinterName
            Case "FORM2_6"
                SendPrinterName = ConfigurationManager.AppSettings("SG_007_02Edi_receiptFORM2_6").ToString()
                Return SendPrinterName
            Case "FORM3"
                SendPrinterName = ConfigurationManager.AppSettings("SG_007_02Edi_receiptFORM3").ToString()
                Return SendPrinterName
            Case "FORM3_1"
                SendPrinterName = ConfigurationManager.AppSettings("SG_007_02Edi_receiptFORM3_1").ToString()
                Return SendPrinterName
                'by rut D new
                'FORM44_4	ฟอร์ม ดี (ATIGA) ใหม่ (7 ประเทศ)
                'FORM44_44	ฟอร์ม ดี (ATIGA) Attach Sheet รถยนต์ ใหม่ (7 ประเทศ)
                'FORM44_41	ฟอร์ม ดี AICO ใหม่ (7 ประเทศ)
                'FORM441_4	ฟอร์ม ดี AICO Attach Sheet รถยนต์ ใหม่ (7 ประเทศ)
            Case "FORM44_4", "FORM44_44", "FORM44_41", "FORM441_4", "FORM44_01", "FORM44_02"
                SendPrinterName = ConfigurationManager.AppSettings("SG_007_02Edi_receiptFORM4").ToString()
                Return SendPrinterName
            Case "FORM4", "FORM44"
                SendPrinterName = ConfigurationManager.AppSettings("SG_007_02Edi_receiptFORM4").ToString()
                Return SendPrinterName
            Case "FORM4_1", "FORM441"
                SendPrinterName = ConfigurationManager.AppSettings("SG_007_02Edi_receiptFORM4_1").ToString()
                Return SendPrinterName

            Case "FORM4_2"
                SendPrinterName = ConfigurationManager.AppSettings("SG_007_02Edi_receiptFORM4_2").ToString()
                Return SendPrinterName
            Case "FORM4_3"
                SendPrinterName = ConfigurationManager.AppSettings("SG_007_02Edi_receiptFORM4_3").ToString()
                Return SendPrinterName
            Case "FORM4_4"
                SendPrinterName = ConfigurationManager.AppSettings("SG_007_02Edi_receiptFORM4_4").ToString()
                Return SendPrinterName
            Case "FORM4_5"
                SendPrinterName = ConfigurationManager.AppSettings("SG_007_02Edi_receiptFORM4_5").ToString()
                Return SendPrinterName
            Case "FORM4_6", "FORM4_61"
                SendPrinterName = ConfigurationManager.AppSettings("SG_007_02Edi_receiptFORM4_6").ToString()
                Return SendPrinterName
            Case "FORM4_8", "FORM4_81"
                SendPrinterName = ConfigurationManager.AppSettings("SG_007_02Edi_receiptFORM4_8").ToString()
                Return SendPrinterName
            Case "FORM4_9"
                SendPrinterName = ConfigurationManager.AppSettings("SG_007_02Edi_receiptFORM4_9").ToString()
                Return SendPrinterName
            Case "FORM4_91", "FORM4_911"
                SendPrinterName = ConfigurationManager.AppSettings("SG_007_02Edi_receiptFORM4_91").ToString()
                Return SendPrinterName
            Case "FORM5"
                SendPrinterName = ConfigurationManager.AppSettings("SG_007_02Edi_receiptFORM5").ToString()
                Return SendPrinterName
            Case "FORM5_1"
                SendPrinterName = ConfigurationManager.AppSettings("SG_007_02Edi_receiptFORM5_1").ToString()
                Return SendPrinterName

                ''ByTine 14-10-2558 ฟอร์มไทย-ชิลี FORm5_2 
            Case "FORM5_2"
                SendPrinterName = ConfigurationManager.AppSettings("SG_007_02Edi_receiptFORM5_2").ToString()
                Return SendPrinterName

            Case "FORM6"
                SendPrinterName = ConfigurationManager.AppSettings("SG_007_02Edi_receiptFORM6").ToString()
                Return SendPrinterName
            Case "FORM7"
                SendPrinterName = ConfigurationManager.AppSettings("SG_007_02Edi_receiptFORM7").ToString()
                Return SendPrinterName
            Case "FORM8"
                SendPrinterName = ConfigurationManager.AppSettings("SG_007_02Edi_receiptFORM8").ToString()
                Return SendPrinterName
            Case "FORM9"
                SendPrinterName = ConfigurationManager.AppSettings("SG_007_02Edi_receiptFORM9").ToString()
                Return SendPrinterName
            Case "FORMRussia"
                SendPrinterName = ConfigurationManager.AppSettings("SG_007_02Edi_receiptFORMRussia").ToString()
                Return SendPrinterName
        End Select
    End Function
#End Region
#End Region

#Region "   Site TK-008 สคต.ตาก Config Printer หนังสือรับรอง-คำขอ    "

#Region "   หนังสือรับรอง   "
    'หนังสือรับรอง TK-008 'สคต.ตาก
    Public Shared Function Call_SitePrints_TK_008(ByVal Form_ID As String) As String
        Dim SendPrinterName As String
        Select Case Form_ID
            Case "FORM1"
                SendPrinterName = ConfigurationManager.AppSettings("TK_008Edi_FORM1").ToString()
                Return SendPrinterName
            Case "FORM1_1"
                SendPrinterName = ConfigurationManager.AppSettings("TK_008Edi_FORM1_1").ToString()
                Return SendPrinterName
            Case "FORM1_2"
                SendPrinterName = ConfigurationManager.AppSettings("TK_008Edi_FORM1_2").ToString()
                Return SendPrinterName
            Case "FORM1_3"
                SendPrinterName = ConfigurationManager.AppSettings("TK_008Edi_FORM1_3").ToString()
                Return SendPrinterName
            Case "FORM1_4"
                SendPrinterName = ConfigurationManager.AppSettings("TK_008Edi_FORM1_4").ToString()
                Return SendPrinterName
            Case "FORM2"
                SendPrinterName = ConfigurationManager.AppSettings("TK_008Edi_FORM2").ToString()
                Return SendPrinterName
            Case "FORM2_1"
                SendPrinterName = ConfigurationManager.AppSettings("TK_008Edi_FORM2_1").ToString()
                Return SendPrinterName
            Case "FORM2_2"
                SendPrinterName = ConfigurationManager.AppSettings("TK_008Edi_FORM2_2").ToString()
                Return SendPrinterName
            Case "FORM2_3"
                SendPrinterName = ConfigurationManager.AppSettings("TK_008Edi_FORM2_3").ToString()
                Return SendPrinterName
            Case "FORM2_4"
                SendPrinterName = ConfigurationManager.AppSettings("TK_008Edi_FORM2_4").ToString()
                Return SendPrinterName
            Case "FORM2_5"
                SendPrinterName = ConfigurationManager.AppSettings("TK_008Edi_FORM2_5").ToString()
                Return SendPrinterName
            Case "FORM2_6"
                SendPrinterName = ConfigurationManager.AppSettings("TK_008Edi_FORM2_6").ToString()
                Return SendPrinterName
            Case "FORM3"
                SendPrinterName = ConfigurationManager.AppSettings("TK_008Edi_FORM3").ToString()
                Return SendPrinterName
            Case "FORM3_1"
                SendPrinterName = ConfigurationManager.AppSettings("TK_008Edi_FORM3_1").ToString()
                Return SendPrinterName
                'by rut D new
                'FORM44_4	ฟอร์ม ดี (ATIGA) ใหม่ (7 ประเทศ)
                'FORM44_44	ฟอร์ม ดี (ATIGA) Attach Sheet รถยนต์ ใหม่ (7 ประเทศ)
                'FORM44_41	ฟอร์ม ดี AICO ใหม่ (7 ประเทศ)
                'FORM441_4	ฟอร์ม ดี AICO Attach Sheet รถยนต์ ใหม่ (7 ประเทศ)
            Case "FORM44_4", "FORM44_44", "FORM44_41", "FORM441_4", "FORM44_01", "FORM44_02"
                SendPrinterName = ConfigurationManager.AppSettings("TK_008Edi_FORM44ALL").ToString()
                Return SendPrinterName
            Case "FORM4", "FORM44"
                SendPrinterName = ConfigurationManager.AppSettings("TK_008Edi_FORM4").ToString()
                Return SendPrinterName
            Case "FORM4_1", "FORM441"
                SendPrinterName = ConfigurationManager.AppSettings("TK_008Edi_FORM4_1").ToString()
                Return SendPrinterName

            Case "FORM4_2"
                SendPrinterName = ConfigurationManager.AppSettings("TK_008Edi_FORM4_2").ToString()
                Return SendPrinterName
            Case "FORM4_3"
                SendPrinterName = ConfigurationManager.AppSettings("TK_008Edi_FORM4_3").ToString()
                Return SendPrinterName
            Case "FORM4_4"
                SendPrinterName = ConfigurationManager.AppSettings("TK_008Edi_FORM4_4").ToString()
                Return SendPrinterName
            Case "FORM4_5"
                SendPrinterName = ConfigurationManager.AppSettings("TK_008Edi_FORM4_5").ToString()
                Return SendPrinterName
            Case "FORM4_6", "FORM4_61"
                SendPrinterName = ConfigurationManager.AppSettings("TK_008Edi_FORM4_6").ToString()
                Return SendPrinterName
            Case "FORM4_8", "FORM4_81"
                SendPrinterName = ConfigurationManager.AppSettings("TK_008Edi_FORM4_8").ToString()
                Return SendPrinterName
            Case "FORM4_9"
                SendPrinterName = ConfigurationManager.AppSettings("TK_008Edi_FORM4_9").ToString()
                Return SendPrinterName
            Case "FORM4_91", "FORM4_911"
                SendPrinterName = ConfigurationManager.AppSettings("TK_008Edi_FORM4_91").ToString()
                Return SendPrinterName
            Case "FORM5"
                SendPrinterName = ConfigurationManager.AppSettings("TK_008Edi_FORM5").ToString()
                Return SendPrinterName
            Case "FORM5_1"
                SendPrinterName = ConfigurationManager.AppSettings("TK_008Edi_FORM5_1").ToString()
                Return SendPrinterName

                ''ByTine 14-10-2558 ฟอร์มไทย-ชิลี FORm5_2 
            Case "FORM5_2"
                SendPrinterName = ConfigurationManager.AppSettings("TK_008Edi_FORM5_2").ToString()
                Return SendPrinterName

            Case "FORM6"
                SendPrinterName = ConfigurationManager.AppSettings("TK_008Edi_FORM6").ToString()
                Return SendPrinterName
            Case "FORM7"
                SendPrinterName = ConfigurationManager.AppSettings("TK_008Edi_FORM7").ToString()
                Return SendPrinterName
            Case "FORM8"
                SendPrinterName = ConfigurationManager.AppSettings("TK_008Edi_FORM8").ToString()
                Return SendPrinterName
            Case "FORM9"
                SendPrinterName = ConfigurationManager.AppSettings("TK_008Edi_FORM9").ToString()
                Return SendPrinterName
            Case "FORMRussia"
                SendPrinterName = ConfigurationManager.AppSettings("TK_008Edi_FORMRussia").ToString()
                Return SendPrinterName

        End Select
    End Function
#End Region

#Region "   คำขอ เครื่อง 1   "
    'เครื่องพิมพ์คำขอ สคต.ตาก TK-008
    Public Shared Function Call_SiteReceipt_TK_008_01(ByVal Form_ID As String) As String
        Dim SendPrinterName As String
        Select Case Form_ID
            Case "FORM1"
                SendPrinterName = ConfigurationManager.AppSettings("TK_008_01Edi_receiptFORM1").ToString()
                Return SendPrinterName
            Case "FORM1_1"
                SendPrinterName = ConfigurationManager.AppSettings("TK_008_01Edi_receiptFORM1_1").ToString()
                Return SendPrinterName
            Case "FORM1_2"
                SendPrinterName = ConfigurationManager.AppSettings("TK_008_01Edi_receiptFORM1_2").ToString()
                Return SendPrinterName
            Case "FORM1_3"
                SendPrinterName = ConfigurationManager.AppSettings("TK_008_01Edi_receiptFORM1_3").ToString()
                Return SendPrinterName
            Case "FORM1_4"
                SendPrinterName = ConfigurationManager.AppSettings("TK_008_01Edi_receiptFORM1_4").ToString()
                Return SendPrinterName
            Case "FORM2"
                SendPrinterName = ConfigurationManager.AppSettings("TK_008_01Edi_receiptFORM2").ToString()
                Return SendPrinterName
            Case "FORM2_1"
                SendPrinterName = ConfigurationManager.AppSettings("TK_008_01Edi_receiptFORM2_1").ToString()
                Return SendPrinterName
            Case "FORM2_2"
                SendPrinterName = ConfigurationManager.AppSettings("TK_008_01Edi_receiptFORM2_2").ToString()
                Return SendPrinterName
            Case "FORM2_3"
                SendPrinterName = ConfigurationManager.AppSettings("TK_008_01Edi_receiptFORM2_3").ToString()
                Return SendPrinterName
            Case "FORM2_4"
                SendPrinterName = ConfigurationManager.AppSettings("TK_008_01Edi_receiptFORM2_4").ToString()
                Return SendPrinterName
            Case "FORM2_5"
                SendPrinterName = ConfigurationManager.AppSettings("TK_008_01Edi_receiptFORM2_5").ToString()
                Return SendPrinterName
            Case "FORM2_6"
                SendPrinterName = ConfigurationManager.AppSettings("TK_008_01Edi_receiptFORM2_6").ToString()
                Return SendPrinterName
            Case "FORM3"
                SendPrinterName = ConfigurationManager.AppSettings("TK_008_01Edi_receiptFORM3").ToString()
                Return SendPrinterName
            Case "FORM3_1"
                SendPrinterName = ConfigurationManager.AppSettings("TK_008_01Edi_receiptFORM3_1").ToString()
                Return SendPrinterName
                'by rut D new
                'FORM44_4	ฟอร์ม ดี (ATIGA) ใหม่ (7 ประเทศ)
                'FORM44_44	ฟอร์ม ดี (ATIGA) Attach Sheet รถยนต์ ใหม่ (7 ประเทศ)
                'FORM44_41	ฟอร์ม ดี AICO ใหม่ (7 ประเทศ)
                'FORM441_4	ฟอร์ม ดี AICO Attach Sheet รถยนต์ ใหม่ (7 ประเทศ)
            Case "FORM44_4", "FORM44_44", "FORM44_41", "FORM441_4", "FORM44_01", "FORM44_02"
                SendPrinterName = ConfigurationManager.AppSettings("TK_008_01Edi_receiptFORM4").ToString()
                Return SendPrinterName
            Case "FORM4", "FORM44"
                SendPrinterName = ConfigurationManager.AppSettings("TK_008_01Edi_receiptFORM4").ToString()
                Return SendPrinterName
            Case "FORM4_1", "FORM441"
                SendPrinterName = ConfigurationManager.AppSettings("TK_008_01Edi_receiptFORM4_1").ToString()
                Return SendPrinterName

            Case "FORM4_2"
                SendPrinterName = ConfigurationManager.AppSettings("TK_008_01Edi_receiptFORM4_2").ToString()
                Return SendPrinterName
            Case "FORM4_3"
                SendPrinterName = ConfigurationManager.AppSettings("TK_008_01Edi_receiptFORM4_3").ToString()
                Return SendPrinterName
            Case "FORM4_4"
                SendPrinterName = ConfigurationManager.AppSettings("TK_008_01Edi_receiptFORM4_4").ToString()
                Return SendPrinterName
            Case "FORM4_5"
                SendPrinterName = ConfigurationManager.AppSettings("TK_008_01Edi_receiptFORM4_5").ToString()
                Return SendPrinterName
            Case "FORM4_6", "FORM4_61"
                SendPrinterName = ConfigurationManager.AppSettings("TK_008_01Edi_receiptFORM4_6").ToString()
                Return SendPrinterName
            Case "FORM4_8", "FORM4_81"
                SendPrinterName = ConfigurationManager.AppSettings("TK_008_01Edi_receiptFORM4_8").ToString()
                Return SendPrinterName
            Case "FORM4_9"
                SendPrinterName = ConfigurationManager.AppSettings("TK_008_01Edi_receiptFORM4_9").ToString()
                Return SendPrinterName
            Case "FORM4_91", "FORM4_911"
                SendPrinterName = ConfigurationManager.AppSettings("TK_008_01Edi_receiptFORM4_91").ToString()
                Return SendPrinterName
            Case "FORM5"
                SendPrinterName = ConfigurationManager.AppSettings("TK_008_01Edi_receiptFORM5").ToString()
                Return SendPrinterName
            Case "FORM5_1"
                SendPrinterName = ConfigurationManager.AppSettings("TK_008_01Edi_receiptFORM5_1").ToString()
                Return SendPrinterName

                ''ByTine 14-10-2558 ฟอร์มไทย-ชิลี FORm5_2 
            Case "FORM5_2"
                SendPrinterName = ConfigurationManager.AppSettings("TK_008_01Edi_receiptFORM5_2").ToString()
                Return SendPrinterName

            Case "FORM6"
                SendPrinterName = ConfigurationManager.AppSettings("TK_008_01Edi_receiptFORM6").ToString()
                Return SendPrinterName
            Case "FORM7"
                SendPrinterName = ConfigurationManager.AppSettings("TK_008_01Edi_receiptFORM7").ToString()
                Return SendPrinterName
            Case "FORM8"
                SendPrinterName = ConfigurationManager.AppSettings("TK_008_01Edi_receiptFORM8").ToString()
                Return SendPrinterName
            Case "FORM9"
                SendPrinterName = ConfigurationManager.AppSettings("TK_008_01Edi_receiptFORM9").ToString()
                Return SendPrinterName
            Case "FORMRussia"
                SendPrinterName = ConfigurationManager.AppSettings("TK_008_01Edi_receiptFORMRussia").ToString()
                Return SendPrinterName
        End Select
    End Function
#End Region

#Region "   คำขอ เครื่อง 2   "
    'เครื่องพิมพ์คำขอ สคต.ตาก TK-008
    Public Shared Function Call_SiteReceipt_TK_008_02(ByVal Form_ID As String) As String
        Dim SendPrinterName As String
        Select Case Form_ID
            Case "FORM1"
                SendPrinterName = ConfigurationManager.AppSettings("TK_008_02Edi_receiptFORM1").ToString()
                Return SendPrinterName
            Case "FORM1_1"
                SendPrinterName = ConfigurationManager.AppSettings("TK_008_02Edi_receiptFORM1_1").ToString()
                Return SendPrinterName
            Case "FORM1_2"
                SendPrinterName = ConfigurationManager.AppSettings("TK_008_02Edi_receiptFORM1_2").ToString()
                Return SendPrinterName
            Case "FORM1_3"
                SendPrinterName = ConfigurationManager.AppSettings("TK_008_02Edi_receiptFORM1_3").ToString()
                Return SendPrinterName
            Case "FORM1_4"
                SendPrinterName = ConfigurationManager.AppSettings("TK_008_02Edi_receiptFORM1_4").ToString()
                Return SendPrinterName
            Case "FORM2"
                SendPrinterName = ConfigurationManager.AppSettings("TK_008_02Edi_receiptFORM2").ToString()
                Return SendPrinterName
            Case "FORM2_1"
                SendPrinterName = ConfigurationManager.AppSettings("TK_008_02Edi_receiptFORM2_1").ToString()
                Return SendPrinterName
            Case "FORM2_2"
                SendPrinterName = ConfigurationManager.AppSettings("TK_008_02Edi_receiptFORM2_2").ToString()
                Return SendPrinterName
            Case "FORM2_3"
                SendPrinterName = ConfigurationManager.AppSettings("TK_008_02Edi_receiptFORM2_3").ToString()
                Return SendPrinterName
            Case "FORM2_4"
                SendPrinterName = ConfigurationManager.AppSettings("TK_008_02Edi_receiptFORM2_4").ToString()
                Return SendPrinterName
            Case "FORM2_5"
                SendPrinterName = ConfigurationManager.AppSettings("TK_008_02Edi_receiptFORM2_5").ToString()
                Return SendPrinterName
            Case "FORM2_6"
                SendPrinterName = ConfigurationManager.AppSettings("TK_008_02Edi_receiptFORM2_6").ToString()
                Return SendPrinterName
            Case "FORM3"
                SendPrinterName = ConfigurationManager.AppSettings("TK_008_02Edi_receiptFORM3").ToString()
                Return SendPrinterName
            Case "FORM3_1"
                SendPrinterName = ConfigurationManager.AppSettings("TK_008_02Edi_receiptFORM3_1").ToString()
                Return SendPrinterName
                'by rut D new
                'FORM44_4	ฟอร์ม ดี (ATIGA) ใหม่ (7 ประเทศ)
                'FORM44_44	ฟอร์ม ดี (ATIGA) Attach Sheet รถยนต์ ใหม่ (7 ประเทศ)
                'FORM44_41	ฟอร์ม ดี AICO ใหม่ (7 ประเทศ)
                'FORM441_4	ฟอร์ม ดี AICO Attach Sheet รถยนต์ ใหม่ (7 ประเทศ)
            Case "FORM44_4", "FORM44_44", "FORM44_41", "FORM441_4", "FORM44_01", "FORM44_02"
                SendPrinterName = ConfigurationManager.AppSettings("TK_008_02Edi_receiptFORM4").ToString()
                Return SendPrinterName
            Case "FORM4", "FORM44"
                SendPrinterName = ConfigurationManager.AppSettings("TK_008_02Edi_receiptFORM4").ToString()
                Return SendPrinterName
            Case "FORM4_1", "FORM441"
                SendPrinterName = ConfigurationManager.AppSettings("TK_008_02Edi_receiptFORM4_1").ToString()
                Return SendPrinterName
            Case "FORM4_2"
                SendPrinterName = ConfigurationManager.AppSettings("TK_008_02Edi_receiptFORM4_2").ToString()
                Return SendPrinterName
            Case "FORM4_3"
                SendPrinterName = ConfigurationManager.AppSettings("TK_008_02Edi_receiptFORM4_3").ToString()
                Return SendPrinterName
            Case "FORM4_4"
                SendPrinterName = ConfigurationManager.AppSettings("TK_008_02Edi_receiptFORM4_4").ToString()
                Return SendPrinterName
            Case "FORM4_5"
                SendPrinterName = ConfigurationManager.AppSettings("TK_008_02Edi_receiptFORM4_5").ToString()
                Return SendPrinterName
            Case "FORM4_6", "FORM4_61"
                SendPrinterName = ConfigurationManager.AppSettings("TK_008_02Edi_receiptFORM4_6").ToString()
                Return SendPrinterName
            Case "FORM4_8", "FORM4_81"
                SendPrinterName = ConfigurationManager.AppSettings("TK_008_02Edi_receiptFORM4_8").ToString()
                Return SendPrinterName
            Case "FORM4_9"
                SendPrinterName = ConfigurationManager.AppSettings("TK_008_02Edi_receiptFORM4_9").ToString()
                Return SendPrinterName
            Case "FORM4_91", "FORM4_911"
                SendPrinterName = ConfigurationManager.AppSettings("TK_008_02Edi_receiptFORM4_91").ToString()
                Return SendPrinterName
            Case "FORM5"
                SendPrinterName = ConfigurationManager.AppSettings("TK_008_02Edi_receiptFORM5").ToString()
                Return SendPrinterName
            Case "FORM5_1"
                SendPrinterName = ConfigurationManager.AppSettings("TK_008_02Edi_receiptFORM5_1").ToString()
                Return SendPrinterName

                ''ByTine 14-10-2558 ฟอร์มไทย-ชิลี FORm5_2 
            Case "FORM5_2"
                SendPrinterName = ConfigurationManager.AppSettings("TK_008_02Edi_receiptFORM5_2").ToString()
                Return SendPrinterName

            Case "FORM6"
                SendPrinterName = ConfigurationManager.AppSettings("TK_008_02Edi_receiptFORM6").ToString()
                Return SendPrinterName
            Case "FORM7"
                SendPrinterName = ConfigurationManager.AppSettings("TK_008_02Edi_receiptFORM7").ToString()
                Return SendPrinterName
            Case "FORM8"
                SendPrinterName = ConfigurationManager.AppSettings("TK_008_02Edi_receiptFORM8").ToString()
                Return SendPrinterName
            Case "FORM9"
                SendPrinterName = ConfigurationManager.AppSettings("TK_008_02Edi_receiptFORM9").ToString()
                Return SendPrinterName
            Case "FORMRussia"
                SendPrinterName = ConfigurationManager.AppSettings("TK_008_02Edi_receiptFORMRussia").ToString()
                Return SendPrinterName
        End Select
    End Function
#End Region
#End Region

#Region "   Site MH-009 สคต.มุกดาหาร Config Printer หนังสือรับรอง-คำขอ    "

#Region "   หนังสือรับรอง   "
    'หนังสือรับรอง MH-009 'สคต.มุกดาหาร MH-009
    Public Shared Function Call_SitePrints_MH_009(ByVal Form_ID As String) As String
        Dim SendPrinterName As String
        Select Case Form_ID
            Case "FORM1"
                SendPrinterName = ConfigurationManager.AppSettings("MH_009Edi_FORM1").ToString()
                Return SendPrinterName
            Case "FORM1_1"
                SendPrinterName = ConfigurationManager.AppSettings("MH_009Edi_FORM1_1").ToString()
                Return SendPrinterName
            Case "FORM1_2"
                SendPrinterName = ConfigurationManager.AppSettings("MH_009Edi_FORM1_2").ToString()
                Return SendPrinterName
            Case "FORM1_3"
                SendPrinterName = ConfigurationManager.AppSettings("MH_009Edi_FORM1_3").ToString()
                Return SendPrinterName
            Case "FORM1_4"
                SendPrinterName = ConfigurationManager.AppSettings("MH_009Edi_FORM1_4").ToString()
                Return SendPrinterName
            Case "FORM2"
                SendPrinterName = ConfigurationManager.AppSettings("MH_009Edi_FORM2").ToString()
                Return SendPrinterName
            Case "FORM2_1"
                SendPrinterName = ConfigurationManager.AppSettings("MH_009Edi_FORM2_1").ToString()
                Return SendPrinterName
            Case "FORM2_2"
                SendPrinterName = ConfigurationManager.AppSettings("MH_009Edi_FORM2_2").ToString()
                Return SendPrinterName
            Case "FORM2_3"
                SendPrinterName = ConfigurationManager.AppSettings("MH_009Edi_FORM2_3").ToString()
                Return SendPrinterName
            Case "FORM2_4"
                SendPrinterName = ConfigurationManager.AppSettings("MH_009Edi_FORM2_4").ToString()
                Return SendPrinterName
            Case "FORM2_5"
                SendPrinterName = ConfigurationManager.AppSettings("MH_009Edi_FORM2_5").ToString()
                Return SendPrinterName
            Case "FORM2_6"
                SendPrinterName = ConfigurationManager.AppSettings("MH_009Edi_FORM2_6").ToString()
                Return SendPrinterName
            Case "FORM3"
                SendPrinterName = ConfigurationManager.AppSettings("MH_009Edi_FORM3").ToString()
                Return SendPrinterName
            Case "FORM3_1"
                SendPrinterName = ConfigurationManager.AppSettings("MH_009Edi_FORM3_1").ToString()
                Return SendPrinterName
                'by rut D new
                'FORM44_4	ฟอร์ม ดี (ATIGA) ใหม่ (7 ประเทศ)
                'FORM44_44	ฟอร์ม ดี (ATIGA) Attach Sheet รถยนต์ ใหม่ (7 ประเทศ)
                'FORM44_41	ฟอร์ม ดี AICO ใหม่ (7 ประเทศ)
                'FORM441_4	ฟอร์ม ดี AICO Attach Sheet รถยนต์ ใหม่ (7 ประเทศ)
            Case "FORM44_4", "FORM44_44", "FORM44_41", "FORM441_4", "FORM44_01", "FORM44_02"
                SendPrinterName = ConfigurationManager.AppSettings("MH_009Edi_FORM44ALL").ToString()
                Return SendPrinterName
            Case "FORM4", "FORM44"
                SendPrinterName = ConfigurationManager.AppSettings("MH_009Edi_FORM4").ToString()
                Return SendPrinterName
            Case "FORM4_1", "FORM441"
                SendPrinterName = ConfigurationManager.AppSettings("MH_009Edi_FORM4_1").ToString()
                Return SendPrinterName

            Case "FORM4_2"
                SendPrinterName = ConfigurationManager.AppSettings("MH_009Edi_FORM4_2").ToString()
                Return SendPrinterName
            Case "FORM4_3"
                SendPrinterName = ConfigurationManager.AppSettings("MH_009Edi_FORM4_3").ToString()
                Return SendPrinterName
            Case "FORM4_4"
                SendPrinterName = ConfigurationManager.AppSettings("MH_009Edi_FORM4_4").ToString()
                Return SendPrinterName
            Case "FORM4_5"
                SendPrinterName = ConfigurationManager.AppSettings("MH_009Edi_FORM4_5").ToString()
                Return SendPrinterName
            Case "FORM4_6", "FORM4_61"
                SendPrinterName = ConfigurationManager.AppSettings("MH_009Edi_FORM4_6").ToString()
                Return SendPrinterName
            Case "FORM4_8", "FORM4_81"
                SendPrinterName = ConfigurationManager.AppSettings("MH_009Edi_FORM4_8").ToString()
                Return SendPrinterName
            Case "FORM4_9"
                SendPrinterName = ConfigurationManager.AppSettings("MH_009Edi_FORM4_9").ToString()
                Return SendPrinterName
            Case "FORM4_91", "FORM4_911"
                SendPrinterName = ConfigurationManager.AppSettings("MH_009Edi_FORM4_91").ToString()
                Return SendPrinterName
            Case "FORM5"
                SendPrinterName = ConfigurationManager.AppSettings("MH_009Edi_FORM5").ToString()
                Return SendPrinterName
            Case "FORM5_1"
                SendPrinterName = ConfigurationManager.AppSettings("MH_009Edi_FORM5_1").ToString()
                Return SendPrinterName

                ''ByTine 14-10-2558 ฟอร์มไทย-ชิลี FORm5_2 
            Case "FORM5_2"
                SendPrinterName = ConfigurationManager.AppSettings("MH_009Edi_FORM5_2").ToString()
                Return SendPrinterName

            Case "FORM6"
                SendPrinterName = ConfigurationManager.AppSettings("MH_009Edi_FORM6").ToString()
                Return SendPrinterName
            Case "FORM7"
                SendPrinterName = ConfigurationManager.AppSettings("MH_009Edi_FORM7").ToString()
                Return SendPrinterName
            Case "FORM8"
                SendPrinterName = ConfigurationManager.AppSettings("MH_009Edi_FORM8").ToString()
                Return SendPrinterName
            Case "FORM9"
                SendPrinterName = ConfigurationManager.AppSettings("MH_009Edi_FORM9").ToString()
                Return SendPrinterName
            Case "FORMRussia"
                SendPrinterName = ConfigurationManager.AppSettings("MH_009Edi_FORMRussia").ToString()
                Return SendPrinterName

        End Select
    End Function
#End Region

#Region "   คำขอ เครื่อง 1   "
    'เครื่องพิมพ์คำขอ สคต.มุกดาหาร MH-009
    Public Shared Function Call_SiteReceipt_MH_009_01(ByVal Form_ID As String) As String
        Dim SendPrinterName As String
        Select Case Form_ID
            Case "FORM1"
                SendPrinterName = ConfigurationManager.AppSettings("MH_009_01Edi_receiptFORM1").ToString()
                Return SendPrinterName
            Case "FORM1_1"
                SendPrinterName = ConfigurationManager.AppSettings("MH_009_01Edi_receiptFORM1_1").ToString()
                Return SendPrinterName
            Case "FORM1_2"
                SendPrinterName = ConfigurationManager.AppSettings("MH_009_01Edi_receiptFORM1_2").ToString()
                Return SendPrinterName
            Case "FORM1_3"
                SendPrinterName = ConfigurationManager.AppSettings("MH_009_01Edi_receiptFORM1_3").ToString()
                Return SendPrinterName
            Case "FORM1_4"
                SendPrinterName = ConfigurationManager.AppSettings("MH_009_01Edi_receiptFORM1_4").ToString()
                Return SendPrinterName
            Case "FORM2"
                SendPrinterName = ConfigurationManager.AppSettings("MH_009_01Edi_receiptFORM2").ToString()
                Return SendPrinterName
            Case "FORM2_1"
                SendPrinterName = ConfigurationManager.AppSettings("MH_009_01Edi_receiptFORM2_1").ToString()
                Return SendPrinterName
            Case "FORM2_2"
                SendPrinterName = ConfigurationManager.AppSettings("MH_009_01Edi_receiptFORM2_2").ToString()
                Return SendPrinterName
            Case "FORM2_3"
                SendPrinterName = ConfigurationManager.AppSettings("MH_009_01Edi_receiptFORM2_3").ToString()
                Return SendPrinterName
            Case "FORM2_4"
                SendPrinterName = ConfigurationManager.AppSettings("MH_009_01Edi_receiptFORM2_4").ToString()
                Return SendPrinterName
            Case "FORM2_5"
                SendPrinterName = ConfigurationManager.AppSettings("MH_009_01Edi_receiptFORM2_5").ToString()
                Return SendPrinterName
            Case "FORM2_6"
                SendPrinterName = ConfigurationManager.AppSettings("MH_009_01Edi_receiptFORM2_6").ToString()
                Return SendPrinterName
            Case "FORM3"
                SendPrinterName = ConfigurationManager.AppSettings("MH_009_01Edi_receiptFORM3").ToString()
                Return SendPrinterName
            Case "FORM3_1"
                SendPrinterName = ConfigurationManager.AppSettings("MH_009_01Edi_receiptFORM3_1").ToString()
                Return SendPrinterName
                'by rut D new
                'FORM44_4	ฟอร์ม ดี (ATIGA) ใหม่ (7 ประเทศ)
                'FORM44_44	ฟอร์ม ดี (ATIGA) Attach Sheet รถยนต์ ใหม่ (7 ประเทศ)
                'FORM44_41	ฟอร์ม ดี AICO ใหม่ (7 ประเทศ)
                'FORM441_4	ฟอร์ม ดี AICO Attach Sheet รถยนต์ ใหม่ (7 ประเทศ)
            Case "FORM44_4", "FORM44_44", "FORM44_41", "FORM441_4", "FORM44_01", "FORM44_02"
                SendPrinterName = ConfigurationManager.AppSettings("MH_009_01Edi_receiptFORM4").ToString()
                Return SendPrinterName
            Case "FORM4", "FORM44"
                SendPrinterName = ConfigurationManager.AppSettings("MH_009_01Edi_receiptFORM4").ToString()
                Return SendPrinterName
            Case "FORM4_1", "FORM441"
                SendPrinterName = ConfigurationManager.AppSettings("MH_009_01Edi_receiptFORM4_1").ToString()
                Return SendPrinterName

            Case "FORM4_2"
                SendPrinterName = ConfigurationManager.AppSettings("MH_009_01Edi_receiptFORM4_2").ToString()
                Return SendPrinterName
            Case "FORM4_3"
                SendPrinterName = ConfigurationManager.AppSettings("MH_009_01Edi_receiptFORM4_3").ToString()
                Return SendPrinterName
            Case "FORM4_4"
                SendPrinterName = ConfigurationManager.AppSettings("MH_009_01Edi_receiptFORM4_4").ToString()
                Return SendPrinterName
            Case "FORM4_5"
                SendPrinterName = ConfigurationManager.AppSettings("MH_009_01Edi_receiptFORM4_5").ToString()
                Return SendPrinterName
            Case "FORM4_6", "FORM4_61"
                SendPrinterName = ConfigurationManager.AppSettings("MH_009_01Edi_receiptFORM4_6").ToString()
                Return SendPrinterName
            Case "FORM4_8", "FORM4_81"
                SendPrinterName = ConfigurationManager.AppSettings("MH_009_01Edi_receiptFORM4_8").ToString()
                Return SendPrinterName
            Case "FORM4_9"
                SendPrinterName = ConfigurationManager.AppSettings("MH_009_01Edi_receiptFORM4_9").ToString()
                Return SendPrinterName
            Case "FORM4_91", "FORM4_911"
                SendPrinterName = ConfigurationManager.AppSettings("MH_009_01Edi_receiptFORM4_91").ToString()
                Return SendPrinterName
            Case "FORM5"
                SendPrinterName = ConfigurationManager.AppSettings("MH_009_01Edi_receiptFORM5").ToString()
                Return SendPrinterName
            Case "FORM5_1"
                SendPrinterName = ConfigurationManager.AppSettings("MH_009_01Edi_receiptFORM5_1").ToString()
                Return SendPrinterName

                ''ByTine 14-10-2558 ฟอร์มไทย-ชิลี FORm5_2 
            Case "FORM5_2"
                SendPrinterName = ConfigurationManager.AppSettings("MH_009_01Edi_receiptFORM5_2").ToString()
                Return SendPrinterName

            Case "FORM6"
                SendPrinterName = ConfigurationManager.AppSettings("MH_009_01Edi_receiptFORM6").ToString()
                Return SendPrinterName
            Case "FORM7"
                SendPrinterName = ConfigurationManager.AppSettings("MH_009_01Edi_receiptFORM7").ToString()
                Return SendPrinterName
            Case "FORM8"
                SendPrinterName = ConfigurationManager.AppSettings("MH_009_01Edi_receiptFORM8").ToString()
                Return SendPrinterName
            Case "FORM9"
                SendPrinterName = ConfigurationManager.AppSettings("MH_009_01Edi_receiptFORM9").ToString()
                Return SendPrinterName
            Case "FORMRussia"
                SendPrinterName = ConfigurationManager.AppSettings("MH_009_01Edi_receiptFORMRussia").ToString()
                Return SendPrinterName
        End Select
    End Function
#End Region

#Region "   คำขอ เครื่อง 2   "
    'เครื่องพิมพ์คำขอ สคต.มุกดาหาร MH-009
    Public Shared Function Call_SiteReceipt_MH_009_02(ByVal Form_ID As String) As String
        Dim SendPrinterName As String
        Select Case Form_ID
            Case "FORM1"
                SendPrinterName = ConfigurationManager.AppSettings("MH_009_02Edi_receiptFORM1").ToString()
                Return SendPrinterName
            Case "FORM1_1"
                SendPrinterName = ConfigurationManager.AppSettings("MH_009_02Edi_receiptFORM1_1").ToString()
                Return SendPrinterName
            Case "FORM1_2"
                SendPrinterName = ConfigurationManager.AppSettings("MH_009_02Edi_receiptFORM1_2").ToString()
                Return SendPrinterName
            Case "FORM1_3"
                SendPrinterName = ConfigurationManager.AppSettings("MH_009_02Edi_receiptFORM1_3").ToString()
                Return SendPrinterName
            Case "FORM1_4"
                SendPrinterName = ConfigurationManager.AppSettings("MH_009_02Edi_receiptFORM1_4").ToString()
                Return SendPrinterName
            Case "FORM2"
                SendPrinterName = ConfigurationManager.AppSettings("MH_009_02Edi_receiptFORM2").ToString()
                Return SendPrinterName
            Case "FORM2_1"
                SendPrinterName = ConfigurationManager.AppSettings("MH_009_02Edi_receiptFORM2_1").ToString()
                Return SendPrinterName
            Case "FORM2_2"
                SendPrinterName = ConfigurationManager.AppSettings("MH_009_02Edi_receiptFORM2_2").ToString()
                Return SendPrinterName
            Case "FORM2_3"
                SendPrinterName = ConfigurationManager.AppSettings("MH_009_02Edi_receiptFORM2_3").ToString()
                Return SendPrinterName
            Case "FORM2_4"
                SendPrinterName = ConfigurationManager.AppSettings("MH_009_02Edi_receiptFORM2_4").ToString()
                Return SendPrinterName
            Case "FORM2_5"
                SendPrinterName = ConfigurationManager.AppSettings("MH_009_02Edi_receiptFORM2_5").ToString()
                Return SendPrinterName
            Case "FORM2_6"
                SendPrinterName = ConfigurationManager.AppSettings("MH_009_02Edi_receiptFORM2_6").ToString()
                Return SendPrinterName
            Case "FORM3"
                SendPrinterName = ConfigurationManager.AppSettings("MH_009_02Edi_receiptFORM3").ToString()
                Return SendPrinterName
            Case "FORM3_1"
                SendPrinterName = ConfigurationManager.AppSettings("MH_009_02Edi_receiptFORM3_1").ToString()
                Return SendPrinterName
                'by rut D new
                'FORM44_4	ฟอร์ม ดี (ATIGA) ใหม่ (7 ประเทศ)
                'FORM44_44	ฟอร์ม ดี (ATIGA) Attach Sheet รถยนต์ ใหม่ (7 ประเทศ)
                'FORM44_41	ฟอร์ม ดี AICO ใหม่ (7 ประเทศ)
                'FORM441_4	ฟอร์ม ดี AICO Attach Sheet รถยนต์ ใหม่ (7 ประเทศ)
            Case "FORM44_4", "FORM44_44", "FORM44_41", "FORM441_4", "FORM44_01", "FORM44_02"
                SendPrinterName = ConfigurationManager.AppSettings("MH_009_02Edi_receiptFORM4").ToString()
                Return SendPrinterName
            Case "FORM4", "FORM44"
                SendPrinterName = ConfigurationManager.AppSettings("MH_009_02Edi_receiptFORM4").ToString()
                Return SendPrinterName
            Case "FORM4_1", "FORM441"
                SendPrinterName = ConfigurationManager.AppSettings("MH_009_02Edi_receiptFORM4_1").ToString()
                Return SendPrinterName

            Case "FORM4_2"
                SendPrinterName = ConfigurationManager.AppSettings("MH_009_02Edi_receiptFORM4_2").ToString()
                Return SendPrinterName
            Case "FORM4_3"
                SendPrinterName = ConfigurationManager.AppSettings("MH_009_02Edi_receiptFORM4_3").ToString()
                Return SendPrinterName
            Case "FORM4_4"
                SendPrinterName = ConfigurationManager.AppSettings("MH_009_02Edi_receiptFORM4_4").ToString()
                Return SendPrinterName
            Case "FORM4_5"
                SendPrinterName = ConfigurationManager.AppSettings("MH_009_02Edi_receiptFORM4_5").ToString()
                Return SendPrinterName
            Case "FORM4_6", "FORM4_61"
                SendPrinterName = ConfigurationManager.AppSettings("MH_009_02Edi_receiptFORM4_6").ToString()
                Return SendPrinterName
            Case "FORM4_8", "FORM4_81"
                SendPrinterName = ConfigurationManager.AppSettings("MH_009_02Edi_receiptFORM4_8").ToString()
                Return SendPrinterName
            Case "FORM4_9"
                SendPrinterName = ConfigurationManager.AppSettings("MH_009_02Edi_receiptFORM4_9").ToString()
                Return SendPrinterName
            Case "FORM4_91", "FORM4_911"
                SendPrinterName = ConfigurationManager.AppSettings("MH_009_02Edi_receiptFORM4_91").ToString()
                Return SendPrinterName
            Case "FORM5"
                SendPrinterName = ConfigurationManager.AppSettings("MH_009_02Edi_receiptFORM5").ToString()
                Return SendPrinterName
            Case "FORM5_1"
                SendPrinterName = ConfigurationManager.AppSettings("MH_009_02Edi_receiptFORM5_1").ToString()
                Return SendPrinterName

                ''ByTine 14-10-2558 ฟอร์มไทย-ชิลี FORm5_2 
            Case "FORM5_2"
                SendPrinterName = ConfigurationManager.AppSettings("MH_009_02Edi_receiptFORM5_2").ToString()
                Return SendPrinterName

            Case "FORM6"
                SendPrinterName = ConfigurationManager.AppSettings("MH_009_02Edi_receiptFORM6").ToString()
                Return SendPrinterName
            Case "FORM7"
                SendPrinterName = ConfigurationManager.AppSettings("MH_009_02Edi_receiptFORM7").ToString()
                Return SendPrinterName
            Case "FORM8"
                SendPrinterName = ConfigurationManager.AppSettings("MH_009_02Edi_receiptFORM8").ToString()
                Return SendPrinterName
            Case "FORM9"
                SendPrinterName = ConfigurationManager.AppSettings("MH_009_02Edi_receiptFORM9").ToString()
                Return SendPrinterName
            Case "FORMRussia"
                SendPrinterName = ConfigurationManager.AppSettings("MH_009_02Edi_receiptFORMRussia").ToString()
                Return SendPrinterName
        End Select
    End Function
#End Region
#End Region

    'by rut 12-09-2556
#Region "   Site KR-010 สคต.เขต 10 (กาญจนบุรี) Config Printer หนังสือรับรอง-คำขอ    "

#Region "   หนังสือรับรอง   "
    'หนังสือรับรอง สคต.เขต 10 (กาญจนบุรี) KR-010
    Public Shared Function Call_SitePrints_KR_010(ByVal Form_ID As String) As String
        Dim SendPrinterName As String
        Select Case Form_ID
            Case "FORM1"
                SendPrinterName = ConfigurationManager.AppSettings("KR_010Edi_FORM1").ToString()
                Return SendPrinterName
            Case "FORM1_1"
                SendPrinterName = ConfigurationManager.AppSettings("KR_010Edi_FORM1_1").ToString()
                Return SendPrinterName
            Case "FORM1_2"
                SendPrinterName = ConfigurationManager.AppSettings("KR_010Edi_FORM1_2").ToString()
                Return SendPrinterName
            Case "FORM1_3"
                SendPrinterName = ConfigurationManager.AppSettings("KR_010Edi_FORM1_3").ToString()
                Return SendPrinterName
            Case "FORM1_4"
                SendPrinterName = ConfigurationManager.AppSettings("KR_010Edi_FORM1_4").ToString()
                Return SendPrinterName
            Case "FORM2"
                SendPrinterName = ConfigurationManager.AppSettings("KR_010Edi_FORM2").ToString()
                Return SendPrinterName
            Case "FORM2_1"
                SendPrinterName = ConfigurationManager.AppSettings("KR_010Edi_FORM2_1").ToString()
                Return SendPrinterName
            Case "FORM2_2"
                SendPrinterName = ConfigurationManager.AppSettings("KR_010Edi_FORM2_2").ToString()
                Return SendPrinterName
            Case "FORM2_3"
                SendPrinterName = ConfigurationManager.AppSettings("KR_010Edi_FORM2_3").ToString()
                Return SendPrinterName
            Case "FORM2_4"
                SendPrinterName = ConfigurationManager.AppSettings("KR_010Edi_FORM2_4").ToString()
                Return SendPrinterName
            Case "FORM2_5"
                SendPrinterName = ConfigurationManager.AppSettings("KR_010Edi_FORM2_5").ToString()
                Return SendPrinterName
            Case "FORM2_6"
                SendPrinterName = ConfigurationManager.AppSettings("KR_010Edi_FORM2_6").ToString()
                Return SendPrinterName
            Case "FORM3"
                SendPrinterName = ConfigurationManager.AppSettings("KR_010Edi_FORM3").ToString()
                Return SendPrinterName
            Case "FORM3_1"
                SendPrinterName = ConfigurationManager.AppSettings("KR_010Edi_FORM3_1").ToString()
                Return SendPrinterName
                'by rut D new
                'FORM44_4	ฟอร์ม ดี (ATIGA) ใหม่ (7 ประเทศ)
                'FORM44_44	ฟอร์ม ดี (ATIGA) Attach Sheet รถยนต์ ใหม่ (7 ประเทศ)
                'FORM44_41	ฟอร์ม ดี AICO ใหม่ (7 ประเทศ)
                'FORM441_4	ฟอร์ม ดี AICO Attach Sheet รถยนต์ ใหม่ (7 ประเทศ)
            Case "FORM44_4", "FORM44_44", "FORM44_41", "FORM441_4", "FORM44_01", "FORM44_02"
                SendPrinterName = ConfigurationManager.AppSettings("KR_010Edi_FORM44ALL").ToString()
                Return SendPrinterName
            Case "FORM4", "FORM44"
                SendPrinterName = ConfigurationManager.AppSettings("KR_010Edi_FORM4").ToString()
                Return SendPrinterName
            Case "FORM4_1", "FORM441"
                SendPrinterName = ConfigurationManager.AppSettings("KR_010Edi_FORM4_1").ToString()
                Return SendPrinterName

            Case "FORM4_2"
                SendPrinterName = ConfigurationManager.AppSettings("KR_010Edi_FORM4_2").ToString()
                Return SendPrinterName
            Case "FORM4_3"
                SendPrinterName = ConfigurationManager.AppSettings("KR_010Edi_FORM4_3").ToString()
                Return SendPrinterName
            Case "FORM4_4"
                SendPrinterName = ConfigurationManager.AppSettings("KR_010Edi_FORM4_4").ToString()
                Return SendPrinterName
            Case "FORM4_5"
                SendPrinterName = ConfigurationManager.AppSettings("KR_010Edi_FORM4_5").ToString()
                Return SendPrinterName
            Case "FORM4_6", "FORM4_61"
                SendPrinterName = ConfigurationManager.AppSettings("KR_010Edi_FORM4_6").ToString()
                Return SendPrinterName
            Case "FORM4_8", "FORM4_81"
                SendPrinterName = ConfigurationManager.AppSettings("KR_010Edi_FORM4_8").ToString()
                Return SendPrinterName
            Case "FORM4_9"
                SendPrinterName = ConfigurationManager.AppSettings("KR_010Edi_FORM4_9").ToString()
                Return SendPrinterName
            Case "FORM4_91", "FORM4_911"
                SendPrinterName = ConfigurationManager.AppSettings("KR_010Edi_FORM4_91").ToString()
                Return SendPrinterName
            Case "FORM5"
                SendPrinterName = ConfigurationManager.AppSettings("KR_010Edi_FORM5").ToString()
                Return SendPrinterName
            Case "FORM5_1"
                SendPrinterName = ConfigurationManager.AppSettings("KR_010Edi_FORM5_1").ToString()
                Return SendPrinterName

                ''ByTine 14-10-2558 ฟอร์มไทย-ชิลี FORm5_2 
            Case "FORM5_2"
                SendPrinterName = ConfigurationManager.AppSettings("KR_010Edi_FORM5_2").ToString()
                Return SendPrinterName

            Case "FORM6"
                SendPrinterName = ConfigurationManager.AppSettings("KR_010Edi_FORM6").ToString()
                Return SendPrinterName
            Case "FORM7"
                SendPrinterName = ConfigurationManager.AppSettings("KR_010Edi_FORM7").ToString()
                Return SendPrinterName
            Case "FORM8"
                SendPrinterName = ConfigurationManager.AppSettings("KR_010Edi_FORM8").ToString()
                Return SendPrinterName
            Case "FORM9"
                SendPrinterName = ConfigurationManager.AppSettings("KR_010Edi_FORM9").ToString()
                Return SendPrinterName
            Case "FORMRussia"
                SendPrinterName = ConfigurationManager.AppSettings("KR_010Edi_FORMRussia").ToString()
                Return SendPrinterName

        End Select
    End Function
#End Region

#Region "   คำขอ เครื่อง 1   "
    'เครื่องพิมพ์คำขอ สคต.เขต 10 (กาญจนบุรี) KR-010
    Public Shared Function Call_SiteReceipt_KR_010_01(ByVal Form_ID As String) As String
        Dim SendPrinterName As String
        Select Case Form_ID
            Case "FORM1"
                SendPrinterName = ConfigurationManager.AppSettings("KR_010_01Edi_receiptFORM1").ToString()
                Return SendPrinterName
            Case "FORM1_1"
                SendPrinterName = ConfigurationManager.AppSettings("KR_010_01Edi_receiptFORM1_1").ToString()
                Return SendPrinterName
            Case "FORM1_2"
                SendPrinterName = ConfigurationManager.AppSettings("KR_010_01Edi_receiptFORM1_2").ToString()
                Return SendPrinterName
            Case "FORM1_3"
                SendPrinterName = ConfigurationManager.AppSettings("KR_010_01Edi_receiptFORM1_3").ToString()
                Return SendPrinterName
            Case "FORM1_4"
                SendPrinterName = ConfigurationManager.AppSettings("KR_010_01Edi_receiptFORM1_4").ToString()
                Return SendPrinterName
            Case "FORM2"
                SendPrinterName = ConfigurationManager.AppSettings("KR_010_01Edi_receiptFORM2").ToString()
                Return SendPrinterName
            Case "FORM2_1"
                SendPrinterName = ConfigurationManager.AppSettings("KR_010_01Edi_receiptFORM2_1").ToString()
                Return SendPrinterName
            Case "FORM2_2"
                SendPrinterName = ConfigurationManager.AppSettings("KR_010_01Edi_receiptFORM2_2").ToString()
                Return SendPrinterName
            Case "FORM2_3"
                SendPrinterName = ConfigurationManager.AppSettings("KR_010_01Edi_receiptFORM2_3").ToString()
                Return SendPrinterName
            Case "FORM2_4"
                SendPrinterName = ConfigurationManager.AppSettings("KR_010_01Edi_receiptFORM2_4").ToString()
                Return SendPrinterName
            Case "FORM3"
                SendPrinterName = ConfigurationManager.AppSettings("KR_010_01Edi_receiptFORM3").ToString()
                Return SendPrinterName
            Case "FORM3_1"
                SendPrinterName = ConfigurationManager.AppSettings("KR_010_01Edi_receiptFORM3_1").ToString()
                Return SendPrinterName
                'by rut D new
                'FORM44_4	ฟอร์ม ดี (ATIGA) ใหม่ (7 ประเทศ)
                'FORM44_44	ฟอร์ม ดี (ATIGA) Attach Sheet รถยนต์ ใหม่ (7 ประเทศ)
                'FORM44_41	ฟอร์ม ดี AICO ใหม่ (7 ประเทศ)
                'FORM441_4	ฟอร์ม ดี AICO Attach Sheet รถยนต์ ใหม่ (7 ประเทศ)
            Case "FORM44_4", "FORM44_44", "FORM44_41", "FORM441_4", "FORM44_01", "FORM44_02"
                SendPrinterName = ConfigurationManager.AppSettings("KR_010_01Edi_receiptFORM4").ToString()
                Return SendPrinterName
            Case "FORM4", "FORM44"
                SendPrinterName = ConfigurationManager.AppSettings("KR_010_01Edi_receiptFORM4").ToString()
                Return SendPrinterName
            Case "FORM4_1", "FORM441"
                SendPrinterName = ConfigurationManager.AppSettings("KR_010_01Edi_receiptFORM4_1").ToString()
                Return SendPrinterName

            Case "FORM4_2"
                SendPrinterName = ConfigurationManager.AppSettings("KR_010_01Edi_receiptFORM4_2").ToString()
                Return SendPrinterName
            Case "FORM4_3"
                SendPrinterName = ConfigurationManager.AppSettings("KR_010_01Edi_receiptFORM4_3").ToString()
                Return SendPrinterName
            Case "FORM4_4"
                SendPrinterName = ConfigurationManager.AppSettings("KR_010_01Edi_receiptFORM4_4").ToString()
                Return SendPrinterName
            Case "FORM4_5"
                SendPrinterName = ConfigurationManager.AppSettings("KR_010_01Edi_receiptFORM4_5").ToString()
                Return SendPrinterName
            Case "FORM4_6", "FORM4_61"
                SendPrinterName = ConfigurationManager.AppSettings("KR_010_01Edi_receiptFORM4_6").ToString()
                Return SendPrinterName
            Case "FORM4_8", "FORM4_81"
                SendPrinterName = ConfigurationManager.AppSettings("KR_010_01Edi_receiptFORM4_8").ToString()
                Return SendPrinterName
            Case "FORM4_9"
                SendPrinterName = ConfigurationManager.AppSettings("KR_010_01Edi_receiptFORM4_9").ToString()
                Return SendPrinterName
            Case "FORM4_91", "FORM4_911"
                SendPrinterName = ConfigurationManager.AppSettings("KR_010_01Edi_receiptFORM4_91").ToString()
                Return SendPrinterName
            Case "FORM5"
                SendPrinterName = ConfigurationManager.AppSettings("KR_010_01Edi_receiptFORM5").ToString()
                Return SendPrinterName
            Case "FORM5_1"
                SendPrinterName = ConfigurationManager.AppSettings("KR_010_01Edi_receiptFORM5_1").ToString()
                Return SendPrinterName

                ''ByTine 14-10-2558 ฟอร์มไทย-ชิลี FORm5_2 
            Case "FORM5_2"
                SendPrinterName = ConfigurationManager.AppSettings("KR_010_01Edi_receiptFORM5_2").ToString()
                Return SendPrinterName

            Case "FORM6"
                SendPrinterName = ConfigurationManager.AppSettings("KR_010_01Edi_receiptFORM6").ToString()
                Return SendPrinterName
            Case "FORM7"
                SendPrinterName = ConfigurationManager.AppSettings("KR_010_01Edi_receiptFORM7").ToString()
                Return SendPrinterName
            Case "FORM8"
                SendPrinterName = ConfigurationManager.AppSettings("KR_010_01Edi_receiptFORM8").ToString()
                Return SendPrinterName
            Case "FORM9"
                SendPrinterName = ConfigurationManager.AppSettings("KR_010_01Edi_receiptFORM9").ToString()
                Return SendPrinterName
            Case "FORMRussia"
                SendPrinterName = ConfigurationManager.AppSettings("KR_010_01Edi_receiptFORMRussia").ToString()
                Return SendPrinterName
        End Select
    End Function
#End Region

#End Region

#Region "   DS 2 Site ST-001 Config Printer หนังสือรับรอง-คำขอ   "

#Region "   หนังสือรับรอง   "
    'หนังสือรับรอง DS 2
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
            Case "FORM2_5"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_01Edi_FORM2_5").ToString()
                Return SendPrinterName
            Case "FORM2_6"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_01Edi_FORM2_6").ToString()
                Return SendPrinterName
            Case "FORM3"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_01Edi_FORM3").ToString()
                Return SendPrinterName
            Case "FORM3_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_01Edi_FORM3_1").ToString()
                Return SendPrinterName
            Case "FORM4", "FORM44"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_01Edi_FORM4").ToString()
                Return SendPrinterName
            Case "FORM4_1", "FORM441"
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
            Case "FORM4_6", "FORM4_61"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_01Edi_FORM4_6").ToString()
                Return SendPrinterName
            Case "FORM4_8", "FORM4_81"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_01Edi_FORM4_8").ToString()
                Return SendPrinterName
            Case "FORM4_9"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_01Edi_FORM4_9").ToString()
                Return SendPrinterName
            Case "FORM4_91", "FORM4_911"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_01Edi_FORM4_91").ToString()
                Return SendPrinterName
            Case "FORM5"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_01Edi_FORM5").ToString()
                Return SendPrinterName
            Case "FORM5_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_01Edi_FORM5_1").ToString()
                Return SendPrinterName

                ''ByTine 14-10-2558 ฟอร์มไทย-ชิลี FORm5_2 
            Case "FORM5_2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_01Edi_FORM5_2").ToString()
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
#End Region

#Region "   คำขอ เครื่อง 03   "
    'เครื่องพิมพ์คำขอ DS 2
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
            Case "FORM2_5"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_03Edi_receiptFORM2_5").ToString()
                Return SendPrinterName
            Case "FORM2_6"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_03Edi_receiptFORM2_6").ToString()
                Return SendPrinterName
            Case "FORM3"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_03Edi_receiptFORM3").ToString()
                Return SendPrinterName
            Case "FORM3_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_03Edi_receiptFORM3_1").ToString()
                Return SendPrinterName
            Case "FORM4", "FORM44"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_03Edi_receiptFORM4").ToString()
                Return SendPrinterName
            Case "FORM4_1", "FORM441"
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
            Case "FORM4_6", "FORM4_61"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_03Edi_receiptFORM4_6").ToString()
                Return SendPrinterName
            Case "FORM4_8", "FORM4_81"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_03Edi_receiptFORM4_8").ToString()
                Return SendPrinterName
            Case "FORM4_9"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_03Edi_receiptFORM4_9").ToString()
                Return SendPrinterName
            Case "FORM4_91", "FORM4_911"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_03Edi_receiptFORM4_91").ToString()
                Return SendPrinterName
            Case "FORM5"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_03Edi_receiptFORM5").ToString()
                Return SendPrinterName
            Case "FORM5_1"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_03Edi_receiptFORM5_1").ToString()
                Return SendPrinterName

                ''ByTine 14-10-2558 ฟอร์มไทย-ชิลี FORm5_2 
            Case "FORM5_2"
                SendPrinterName = ConfigurationManager.AppSettings("ST_001_03Edi_receiptFORM5_2").ToString()
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
#End Region

#End Region

#Region "   Site PN-83-001 สพจ.ภูเก็ต Config Printer หนังสือรับรอง-คำขอ    "
#Region "   หนังสือรับรอง   "
    'หนังสือรับรอง PN-83-001 'สพจ.ภูเก็ต PN-83-001
    Public Shared Function Call_SitePrints_PN_83_001(ByVal Form_ID As String) As String

        Dim SendPrinterName As String = ""
        Dim printer_key As String = "PN_83_001Edi_" & Form_ID
        SendPrinterName = ConfigurationManager.AppSettings(printer_key).ToString()
        Return SendPrinterName

        'backup 17-10-31
        'Select Case Form_ID
        '    Case "FORM1"
        '        SendPrinterName = ConfigurationManager.AppSettings("PN_83_001Edi_FORM1").ToString()
        '        Return SendPrinterName
        '    Case "FORM1_1"
        '        SendPrinterName = ConfigurationManager.AppSettings("PN_83_001Edi_FORM1_1").ToString()
        '        Return SendPrinterName
        '    Case "FORM1_2"
        '        SendPrinterName = ConfigurationManager.AppSettings("PN_83_001Edi_FORM1_2").ToString()
        '        Return SendPrinterName
        '    Case "FORM1_3"
        '        SendPrinterName = ConfigurationManager.AppSettings("PN_83_001Edi_FORM1_3").ToString()
        '        Return SendPrinterName
        '    Case "FORM1_4"
        '        SendPrinterName = ConfigurationManager.AppSettings("PN_83_001Edi_FORM1_4").ToString()
        '        Return SendPrinterName
        '    Case "FORM2"
        '        SendPrinterName = ConfigurationManager.AppSettings("PN_83_001Edi_FORM2").ToString()
        '        Return SendPrinterName
        '    Case "FORM2_1"
        '        SendPrinterName = ConfigurationManager.AppSettings("PN_83_001Edi_FORM2_1").ToString()
        '        Return SendPrinterName
        '    Case "FORM2_2"
        '        SendPrinterName = ConfigurationManager.AppSettings("PN_83_001Edi_FORM2_2").ToString()
        '        Return SendPrinterName
        '    Case "FORM2_3"
        '        SendPrinterName = ConfigurationManager.AppSettings("PN_83_001Edi_FORM2_3").ToString()
        '        Return SendPrinterName
        '    Case "FORM2_4"
        '        SendPrinterName = ConfigurationManager.AppSettings("PN_83_001Edi_FORM2_4").ToString()
        '        Return SendPrinterName
        '    Case "FORM2_5"
        '        SendPrinterName = ConfigurationManager.AppSettings("PN_83_001Edi_FORM2_5").ToString()
        '        Return SendPrinterName
        '    Case "FORM2_6"
        '        SendPrinterName = ConfigurationManager.AppSettings("PN_83_001Edi_FORM2_6").ToString()
        '        Return SendPrinterName
        '    Case "FORM3"
        '        SendPrinterName = ConfigurationManager.AppSettings("PN_83_001Edi_FORM3").ToString()
        '        Return SendPrinterName
        '    Case "FORM3_1"
        '        SendPrinterName = ConfigurationManager.AppSettings("PN_83_001Edi_FORM3_1").ToString()
        '        Return SendPrinterName
        '        'by rut D new
        '        'FORM44_4	ฟอร์ม ดี (ATIGA) ใหม่ (7 ประเทศ)
        '        'FORM44_44	ฟอร์ม ดี (ATIGA) Attach Sheet รถยนต์ ใหม่ (7 ประเทศ)
        '        'FORM44_41	ฟอร์ม ดี AICO ใหม่ (7 ประเทศ)
        '        'FORM441_4	ฟอร์ม ดี AICO Attach Sheet รถยนต์ ใหม่ (7 ประเทศ)
        '    Case "FORM44_4", "FORM44_44", "FORM44_41", "FORM441_4"
        '        SendPrinterName = ConfigurationManager.AppSettings("PN_83_001Edi_FORM44ALL").ToString()
        '        Return SendPrinterName
        '    Case "FORM4", "FORM44"
        '        SendPrinterName = ConfigurationManager.AppSettings("PN_83_001Edi_FORM4").ToString()
        '        Return SendPrinterName
        '    Case "FORM4_1", "FORM441"
        '        SendPrinterName = ConfigurationManager.AppSettings("PN_83_001Edi_FORM4_1").ToString()
        '        Return SendPrinterName

        '    Case "FORM4_2"
        '        SendPrinterName = ConfigurationManager.AppSettings("PN_83_001Edi_FORM4_2").ToString()
        '        Return SendPrinterName
        '    Case "FORM4_3"
        '        SendPrinterName = ConfigurationManager.AppSettings("PN_83_001Edi_FORM4_3").ToString()
        '        Return SendPrinterName
        '    Case "FORM4_4"
        '        SendPrinterName = ConfigurationManager.AppSettings("PN_83_001Edi_FORM4_4").ToString()
        '        Return SendPrinterName
        '    Case "FORM4_5"
        '        SendPrinterName = ConfigurationManager.AppSettings("PN_83_001Edi_FORM4_5").ToString()
        '        Return SendPrinterName
        '    Case "FORM4_6", "FORM4_61"
        '        SendPrinterName = ConfigurationManager.AppSettings("PN_83_001Edi_FORM4_6").ToString()
        '        Return SendPrinterName
        '    Case "FORM4_8", "FORM4_81"
        '        SendPrinterName = ConfigurationManager.AppSettings("PN_83_001Edi_FORM4_8").ToString()
        '        Return SendPrinterName
        '    Case "FORM4_9"
        '        SendPrinterName = ConfigurationManager.AppSettings("PN_83_001Edi_FORM4_9").ToString()
        '        Return SendPrinterName
        '    Case "FORM4_91", "FORM4_911"
        '        SendPrinterName = ConfigurationManager.AppSettings("PN_83_001Edi_FORM4_91").ToString()
        '        Return SendPrinterName
        '    Case "FORM5"
        '        SendPrinterName = ConfigurationManager.AppSettings("PN_83_001Edi_FORM5").ToString()
        '        Return SendPrinterName
        '    Case "FORM5_1"
        '        SendPrinterName = ConfigurationManager.AppSettings("PN_83_001Edi_FORM5_1").ToString()
        '        Return SendPrinterName

        '        ''ByTine 14-10-2558 ฟอร์มไทย-ชิลี FORm5_2 
        '    Case "FORM5_2"
        '        SendPrinterName = ConfigurationManager.AppSettings("PN_83_001Edi_FORM5_2").ToString()
        '        Return SendPrinterName

        '    Case "FORM6"
        '        SendPrinterName = ConfigurationManager.AppSettings("PN_83_001Edi_FORM6").ToString()
        '        Return SendPrinterName
        '    Case "FORM7"
        '        SendPrinterName = ConfigurationManager.AppSettings("PN_83_001Edi_FORM7").ToString()
        '        Return SendPrinterName
        '    Case "FORM8"
        '        SendPrinterName = ConfigurationManager.AppSettings("PN_83_001Edi_FORM8").ToString()
        '        Return SendPrinterName
        '    Case "FORM9"
        '        SendPrinterName = ConfigurationManager.AppSettings("PN_83_001Edi_FORM9").ToString()
        '        Return SendPrinterName
        '    Case "FORMRussia"
        '        SendPrinterName = ConfigurationManager.AppSettings("PN_83_001Edi_FORMRussia").ToString()
        '        Return SendPrinterName

        'End Select
    End Function
#End Region

#Region "   คำขอ เครื่อง   "
    'เครื่องพิมพ์คำขอ PN-83-001 'สพจ.ภูเก็ต PN-83-001
    Public Shared Function Call_SiteReceipt_PN_83_001(ByVal Form_ID As String, request_type As String) As String
        Dim SendPrinterName As String = ""
        Dim printer_key As String = "PN_83_001_0" & request_type & "Edi_receipt" & Form_ID
        SendPrinterName = ConfigurationManager.AppSettings(printer_key).ToString()

        Return SendPrinterName
        'backup 17-10-31
        'Select Case Form_ID
        '    Case "FORM1"
        '        SendPrinterName = ConfigurationManager.AppSettings("PN_83_001_01Edi_receiptFORM1").ToString()
        '        Return SendPrinterName
        '    Case "FORM1_1"
        '        SendPrinterName = ConfigurationManager.AppSettings("PN_83_001_01Edi_receiptFORM1_1").ToString()
        '        Return SendPrinterName
        '    Case "FORM1_2"
        '        SendPrinterName = ConfigurationManager.AppSettings("PN_83_001_01Edi_receiptFORM1_2").ToString()
        '        Return SendPrinterName
        '    Case "FORM1_3"
        '        SendPrinterName = ConfigurationManager.AppSettings("PN_83_001_01Edi_receiptFORM1_3").ToString()
        '        Return SendPrinterName
        '    Case "FORM1_4"
        '        SendPrinterName = ConfigurationManager.AppSettings("PN_83_001_01Edi_receiptFORM1_4").ToString()
        '        Return SendPrinterName
        '    Case "FORM2"
        '        SendPrinterName = ConfigurationManager.AppSettings("PN_83_001_01Edi_receiptFORM2").ToString()
        '        Return SendPrinterName
        '    Case "FORM2_1"
        '        SendPrinterName = ConfigurationManager.AppSettings("PN_83_001_01Edi_receiptFORM2_1").ToString()
        '        Return SendPrinterName
        '    Case "FORM2_2"
        '        SendPrinterName = ConfigurationManager.AppSettings("PN_83_001_01Edi_receiptFORM2_2").ToString()
        '        Return SendPrinterName
        '    Case "FORM2_3"
        '        SendPrinterName = ConfigurationManager.AppSettings("PN_83_001_01Edi_receiptFORM2_3").ToString()
        '        Return SendPrinterName
        '    Case "FORM2_4"
        '        SendPrinterName = ConfigurationManager.AppSettings("PN_83_001_01Edi_receiptFORM2_4").ToString()
        '        Return SendPrinterName
        '    Case "FORM2_5"
        '        SendPrinterName = ConfigurationManager.AppSettings("PN_83_001_01Edi_receiptFORM2_5").ToString()
        '        Return SendPrinterName
        '    Case "FORM2_6"
        '        SendPrinterName = ConfigurationManager.AppSettings("PN_83_001_01Edi_receiptFORM2_6").ToString()
        '        Return SendPrinterName
        '    Case "FORM3"
        '        SendPrinterName = ConfigurationManager.AppSettings("PN_83_001_01Edi_receiptFORM3").ToString()
        '        Return SendPrinterName
        '    Case "FORM3_1"
        '        SendPrinterName = ConfigurationManager.AppSettings("PN_83_001_01Edi_receiptFORM3_1").ToString()
        '        Return SendPrinterName
        '        'by rut D new
        '        'FORM44_4	ฟอร์ม ดี (ATIGA) ใหม่ (7 ประเทศ)
        '        'FORM44_44	ฟอร์ม ดี (ATIGA) Attach Sheet รถยนต์ ใหม่ (7 ประเทศ)
        '        'FORM44_41	ฟอร์ม ดี AICO ใหม่ (7 ประเทศ)
        '        'FORM441_4	ฟอร์ม ดี AICO Attach Sheet รถยนต์ ใหม่ (7 ประเทศ)
        '    Case "FORM44_4", "FORM44_44", "FORM44_41", "FORM441_4"
        '        SendPrinterName = ConfigurationManager.AppSettings("PN_83_001_01Edi_receiptFORM4").ToString()
        '        Return SendPrinterName
        '    Case "FORM4", "FORM44"
        '        SendPrinterName = ConfigurationManager.AppSettings("PN_83_001_01Edi_receiptFORM4").ToString()
        '        Return SendPrinterName
        '    Case "FORM4_1", "FORM441"
        '        SendPrinterName = ConfigurationManager.AppSettings("PN_83_001_01Edi_receiptFORM4_1").ToString()
        '        Return SendPrinterName

        '    Case "FORM4_2"
        '        SendPrinterName = ConfigurationManager.AppSettings("PN_83_001_01Edi_receiptFORM4_2").ToString()
        '        Return SendPrinterName
        '    Case "FORM4_3"
        '        SendPrinterName = ConfigurationManager.AppSettings("PN_83_001_01Edi_receiptFORM4_3").ToString()
        '        Return SendPrinterName
        '    Case "FORM4_4"
        '        SendPrinterName = ConfigurationManager.AppSettings("PN_83_001_01Edi_receiptFORM4_4").ToString()
        '        Return SendPrinterName
        '    Case "FORM4_5"
        '        SendPrinterName = ConfigurationManager.AppSettings("PN_83_001_01Edi_receiptFORM4_5").ToString()
        '        Return SendPrinterName
        '    Case "FORM4_6", "FORM4_61"
        '        SendPrinterName = ConfigurationManager.AppSettings("PN_83_001_01Edi_receiptFORM4_6").ToString()
        '        Return SendPrinterName
        '    Case "FORM4_8", "FORM4_81"
        '        SendPrinterName = ConfigurationManager.AppSettings("PN_83_001_01Edi_receiptFORM4_8").ToString()
        '        Return SendPrinterName
        '    Case "FORM4_9"
        '        SendPrinterName = ConfigurationManager.AppSettings("PN_83_001_01Edi_receiptFORM4_9").ToString()
        '        Return SendPrinterName
        '    Case "FORM4_91", "FORM4_911"
        '        SendPrinterName = ConfigurationManager.AppSettings("PN_83_001_01Edi_receiptFORM4_91").ToString()
        '        Return SendPrinterName
        '    Case "FORM5"
        '        SendPrinterName = ConfigurationManager.AppSettings("PN_83_001_01Edi_receiptFORM5").ToString()
        '        Return SendPrinterName
        '    Case "FORM5_1"
        '        SendPrinterName = ConfigurationManager.AppSettings("PN_83_001_01Edi_receiptFORM5_1").ToString()
        '        Return SendPrinterName

        '        ''ByTine 14-10-2558 ฟอร์มไทย-ชิลี FORm5_2 
        '    Case "FORM5_2"
        '        SendPrinterName = ConfigurationManager.AppSettings("PN_83_001_01Edi_receiptFORM5_2").ToString()
        '        Return SendPrinterName

        '    Case "FORM6"
        '        SendPrinterName = ConfigurationManager.AppSettings("PN_83_001_01Edi_receiptFORM6").ToString()
        '        Return SendPrinterName
        '    Case "FORM7"
        '        SendPrinterName = ConfigurationManager.AppSettings("PN_83_001_01Edi_receiptFORM7").ToString()
        '        Return SendPrinterName
        '    Case "FORM8"
        '        SendPrinterName = ConfigurationManager.AppSettings("PN_83_001_01Edi_receiptFORM8").ToString()
        '        Return SendPrinterName
        '    Case "FORM9"
        '        SendPrinterName = ConfigurationManager.AppSettings("PN_83_001_01Edi_receiptFORM9").ToString()
        '        Return SendPrinterName
        '    Case "FORMRussia"
        '        SendPrinterName = ConfigurationManager.AppSettings("PN_83_001_01Edi_receiptFORMRussia").ToString()
        '        Return SendPrinterName
        'End Select
    End Function
#End Region

#End Region

#Region "   Site PN-95-001 สพจ.ยะลา Config Printer หนังสือรับรอง-คำขอ    "
#Region "   หนังสือรับรอง   "
    'หนังสือรับรอง PN-95-001 สพจ.ยะลา PN-95-001
    Public Shared Function Call_SitePrints_PN_95_001(ByVal Form_ID As String) As String

        Dim SendPrinterName As String = ""
        Dim printer_key As String = "PN_95_001Edi_" & Form_ID
        SendPrinterName = ConfigurationManager.AppSettings(printer_key).ToString()
        Return SendPrinterName

    End Function
#End Region

#Region "   คำขอ เครื่อง   "
    'เครื่องพิมพ์คำขอ PN-95-001 สพจ.ยะลา PN-95-001
    Public Shared Function Call_SiteReceipt_PN_95_001(ByVal Form_ID As String, request_type As String) As String
        Dim SendPrinterName As String = ""
        Dim printer_key As String = "PN_95_001_0" & request_type & "Edi_receipt" & Form_ID
        SendPrinterName = ConfigurationManager.AppSettings(printer_key).ToString()

        Return SendPrinterName
    End Function
#End Region
#End Region

#Region "   Site PN-96-001 สพจ.นราธิวาส Config Printer หนังสือรับรอง-คำขอ    "
#Region "   หนังสือรับรอง   "
    'หนังสือรับรอง PN-96-001 สพจ.นราธิวาส PN-96-001
    Public Shared Function Call_SitePrints_PN_96_001(ByVal Form_ID As String) As String

        Dim SendPrinterName As String = ""
        Dim printer_key As String = "PN_96_001Edi_" & Form_ID
        SendPrinterName = ConfigurationManager.AppSettings(printer_key).ToString()
        Return SendPrinterName

    End Function
#End Region

#Region "   คำขอ เครื่อง   "
    'เครื่องพิมพ์คำขอ PN-96-001 สพจ.นราธิวาส PN-96-001
    Public Shared Function Call_SiteReceipt_PN_96_001(ByVal Form_ID As String, request_type As String) As String
        Dim SendPrinterName As String = ""
        Dim printer_key As String = "PN_96_001_0" & request_type & "Edi_receipt" & Form_ID
        SendPrinterName = ConfigurationManager.AppSettings(printer_key).ToString()

        Return SendPrinterName
    End Function
#End Region
#End Region

#Region "   Site PN-91-001 สพจ.สตูล Config Printer หนังสือรับรอง-คำขอ    "
#Region "   หนังสือรับรอง   "
    'หนังสือรับรอง PN-91-001 สพจ.สตูล PN-91-001
    Public Shared Function Call_SitePrints_PN_91_001(ByVal Form_ID As String) As String

        Dim SendPrinterName As String = ""
        Dim printer_key As String = "PN_91_001Edi_" & Form_ID
        SendPrinterName = ConfigurationManager.AppSettings(printer_key).ToString()
        Return SendPrinterName

    End Function
#End Region

#Region "   คำขอ เครื่อง   "
    'เครื่องพิมพ์คำขอ PN-91-001 สพจ.สตูล PN-91-001
    Public Shared Function Call_SiteReceipt_PN_91_001(ByVal Form_ID As String, request_type As String) As String
        Dim SendPrinterName As String = ""
        Dim printer_key As String = "PN_91_001_0" & request_type & "Edi_receipt" & Form_ID
        SendPrinterName = ConfigurationManager.AppSettings(printer_key).ToString()

        Return SendPrinterName
    End Function
#End Region
#End Region

#Region "   Site PN-48-001 สพจ.นครพนม Config Printer หนังสือรับรอง-คำขอ    "
#Region "   หนังสือรับรอง   "
    'หนังสือรับรอง PN-48-001 สพจ.นครพนม PN-48-001
    Public Shared Function Call_SitePrints_PN_48_001(ByVal Form_ID As String) As String

        Dim SendPrinterName As String = ""
        Dim printer_key As String = "PN_48_001Edi_" & Form_ID
        SendPrinterName = ConfigurationManager.AppSettings(printer_key).ToString()
        Return SendPrinterName

    End Function
#End Region

#Region "   คำขอ เครื่อง   "
    'เครื่องพิมพ์คำขอ PN-48-001 สพจ.นครพนม PN-48-001
    Public Shared Function Call_SiteReceipt_PN_48_001(ByVal Form_ID As String, request_type As String) As String
        Dim SendPrinterName As String = ""
        Dim printer_key As String = "PN_48_001_0" & request_type & "Edi_receipt" & Form_ID
        SendPrinterName = ConfigurationManager.AppSettings(printer_key).ToString()

        Return SendPrinterName
    End Function
#End Region
#End Region

#Region "   Site PN-51-001 สพจ ลำพูน Config Printer หนังสือรับรอง-คำขอ    "
#Region "   หนังสือรับรอง   "
    'หนังสือรับรอง PN-51-001 สพจ.ลำพูน PN-51-001
    Public Shared Function Call_SitePrints_PN_51_001(ByVal Form_ID As String) As String

        Dim SendPrinterName As String = ""
        Dim printer_key As String = "PN_51_001Edi_" & Form_ID
        SendPrinterName = ConfigurationManager.AppSettings(printer_key).ToString()
        Return SendPrinterName

    End Function
#End Region

#Region "   คำขอ เครื่อง   "
    'เครื่องพิมพ์คำขอ PN-51-001 สพจ.ลำพูน PN-51-001
    Public Shared Function Call_SiteReceipt_PN_51_001(ByVal Form_ID As String, request_type As String) As String
        Dim SendPrinterName As String = ""
        Dim printer_key As String = "PN_51_001_0" & request_type & "Edi_receipt" & Form_ID
        SendPrinterName = ConfigurationManager.AppSettings(printer_key).ToString()

        Return SendPrinterName
    End Function
#End Region
#End Region

#Region "   Function Form2_1   "
    Public Shared Function GetDataLoad_EDI_ReForm2_1(ByVal Send_INVH_RUN_AUTO As String, ByVal Send_SITE_ID As String) As SqlDataReader
        Dim strEDIConn As String = ConfigurationManager.ConnectionStrings("EDIConnection").ConnectionString
        Dim dr As SqlDataReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "vi_form4_edi_printFormBar_NewDS", New SqlParameter("@INVH_RUN_AUTO", Send_INVH_RUN_AUTO), New SqlParameter("@SITE_ID", Send_SITE_ID))

        Return dr

    End Function
#End Region

#Region "   Function Other   "
    '=======================================================================
    'ใช้สำหรับ พิมพ์วันที่ 04/09/2010
    Public Shared Function format_dateSelect() As String
        Dim _date As Date
        _date = Date.Today

        Return Format(_date, "dd/MM/yyy")
    End Function
    ''ใช้สำหรับเวลา แสดง 08:05:12
    'Function SelectdateTime(ByVal _billdate As Date) As String
    '    Dim _strBilldate As String
    '    _strBilldate = Format(_billdate, "hh:mm:ss")

    '    Return _strBilldate
    'End Function
    'check and return "" เป็น 0.0
    Public Shared Function Check_Null(ByVal V_txt As Object) As Object
        If String.IsNullOrEmpty(Trim(V_txt.ToString)) = True Then
            Return 0.0
        Else
            Return V_txt
        End If
    End Function
#End Region

#Region "   ยังไม่ได้ใช้ ทดสอบ   "
    'ไว้เรียกชื่อเครื่อง printer new จาก Table (ยังไม่ใช้ทดสอบเฉยๆ)
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
    'ไว้เรียกชื่อเครื่อง printer new จาก Table (ยังไม่ใช้ทดสอบเฉยๆ)
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

    Sub ClearReport(ByVal nameReport)
        nameReport.ducument.clear()

    End Sub

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
    'set print ST-001 กรมการค้าต่างประเทศ (ส่วนกลาง)
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
    '                PrintRpt.Document.Print(False, False) 'set false เพื่อไม่ให้ขึ้นหน้าเลือกเครื่อง printer
    '                Return True
#End Region

#Region "B2B"
    ''12-10-2559
    ''ByTine แสดงวันที่และเลขของ B2B ในฟอร์มช่อง7
    Public Shared Function LoadDataForB2B(ByVal _InvhRunAuto As String)
        Try
            Dim strEDIConn As String = ConfigurationManager.ConnectionStrings("EDIConnection").ConnectionString
            Dim _TempData As String = ""
            Dim npm As New SqlParameter("@invh_run_auto", _InvhRunAuto)
            Dim ds As New DataSet
            ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, "sp_b2b_Load_For_PrintForm", npm)
            If ds.Tables(0).Rows.Count > 0 Then
                For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                    If i = ds.Tables(0).Rows.Count - 1 Then
                        _TempData += "        " & ds.Tables(0).Rows(i).Item("B2BData")
                        'ElseIf i = 0 Then
                        '    _TempData += ds.Tables(0).Rows(i).Item("B2BData") & vbNewLine
                    Else
                        _TempData += "        " & ds.Tables(0).Rows(i).Item("B2BData") & vbNewLine
                    End If
                Next

                Return _TempData

            End If
        Catch ex As Exception

        End Try
    End Function

    Shared Function PrintBackToBackForm(ByVal PrinterName, ByVal _FormType_1) As Boolean
        Dim strEDIConn As String = ConfigurationManager.ConnectionStrings("EDIConnection").ConnectionString

        If _ISB2B_1 = True Then
            ''เงื่อนไข B2B เอาตารางข้อมูล B2B มาต่อกับคำขอ
            ''===============================================================
            Dim _FormType As String = Module_CallBathEng.ConvertFormTypeForB2B(_FormType_1)

            Dim npm As New SqlParameter("@invh_run_auto", _InvRunAuto_1)
            Dim ds As New DataSet
            ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, "sp_b2b_Load_BalanceB2BItem", npm)

            If ds.Tables(0).Rows.Count > 0 Then
                For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                    Dim rpt_B2B As New rpt3_ReEdi_B2B
                    rpt_B2B = Print_B2BData(_CompanyTaxno_1, ds.Tables(0).Rows(i).Item("B2BReferenceCode"), _FormType, _B2BCountry_1, _InvRunAuto_1)
                    'rpt_B2B.Document.Printer.PrinterName = "\\10.3.220.129\request_texttile"
                    rpt_B2B.Document.Printer.PrinterName = PrinterName
                    rpt_B2B.PageSettings.PaperKind = Printing.PaperKind.A4
                    rpt_B2B.Run(False)
                    rpt_B2B.Document.Print(False, False)

                Next
            End If
        End If
    End Function

    Shared Sub PrintInvoice(PrinterName As String, invh_run_auto As String)
        Dim strEDIConn As String = ConfigurationManager.ConnectionStrings("EDIConnection").ConnectionString
        Try
            Dim rpt As New rpt3_ReEdi_InvoiceList

            Dim cmd As String = "sp_ASW2_get_FormInvoice_Select"
            Dim prm As New SqlParameter("@invh_run_auto", invh_run_auto)
            Dim ds As DataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, cmd, prm)

            rpt.DataSource = ds.Tables(0)
            rpt.Document.Printer.PrinterName = PrinterName
            rpt.PageSettings.PaperKind = Printing.PaperKind.A4
            rpt.Run(False)
            rpt.Document.Print(False, False)

        Catch ex As Exception

        End Try
    End Sub


    'Shared Function PrintBackToBack(ByVal dsRequestDetails_f, ByVal PrinterName, ByVal _FormType_1) As Boolean
    '    Dim strEDIConn As String = ConfigurationManager.ConnectionStrings("EDIConnection").ConnectionString
    '    Dim rpt = New rpt3_ReEdi_A
    '    'Dim rptTemp As New rpt3_ReEdi_Temp

    '    If dsRequestDetails_f.Tables(0).Rows.Count > 0 Then
    '        'rpt.Document.Printer.PrinterName = ""
    '        'rptTemp.Document.Printer.PrinterName = ""

    '        'rpt.PageSettings.PaperKind = Printing.PaperKind.A4
    '        'rpt.PageSettings.PaperName = "Fanfold 210 x 305 mm"

    '        'rptTemp.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.Custom
    '        'rptTemp.PageSettings.PaperName = "Fanfold 210 x 305 mm"

    '        rpt.DataSource = dsRequestDetails_f.Tables(0)
    '        rpt.Document.Printer.PrinterName = "\\10.3.220.129\request_texttile"
    '        rpt.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.Custom
    '        rpt.PageSettings.PaperName = "Fanfold 210 x 305 mm"
    '        'rpt.Document.Printer.PrinterName = PrinterName
    '        rpt.Run()
    '        rpt.Document.Print(False, False)

    '        If _ISB2B_1 = True Then
    '            ''เงื่อนไข B2B เอาตารางข้อมูล B2B มาต่อกับคำขอ
    '            ''===============================================================
    '            Dim _FormType As String = Module_CallBathEng.ConvertFormTypeForB2B(_FormType_1)

    '            'rpt.Run(False)

    '            Dim npm As New SqlParameter("@invh_run_auto", _InvRunAuto_1)
    '            Dim ds As New DataSet
    '            ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, "sp_b2b_Load_BalanceB2BItem", npm)

    '            If ds.Tables(0).Rows.Count > 0 Then
    '                'rpt.Document.Printer.PrinterName = "\\10.3.220.129\request_texttile"
    '                ''rpt.Document.Printer.PrinterName = PrinterName
    '                'rpt.Run()
    '                'rpt.Document.Print(False, False)

    '                For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
    '                    Dim rpt_B2B As New rpt3_ReEdi_B2B
    '                    rpt_B2B = Print_B2BData(_CompanyTaxno_1, ds.Tables(0).Rows(i).Item("B2BReferenceCode"), _FormType, _B2BCountry_1, _InvRunAuto_1)
    '                    rpt.Document.Printer.PrinterName = "\\10.3.220.129\request_texttile"
    '                    'rpt_B2B.Document.Printer.PrinterName = PrinterName
    '                    rpt_B2B.PageSettings.PaperKind = Printing.PaperKind.A4
    '                    rpt_B2B.Run(False)
    '                    rpt_B2B.Document.Print(False, False)

    '                    'For j As Integer = 0 To rpt_B2B.Document.Pages.Count - 1
    '                    '    rptTemp.Document.Pages.Add(rpt_B2B.Document.Pages(j))
    '                    'Next
    '                Next
    '            End If

    '            '========================

    '            ''Report หลัก
    '            'Dim a As Integer = 0
    '            'Do While a <= rpt.Document.Pages.Count - 1
    '            '    Dim Numpage0 As Integer = (rpt.Document.Pages.Count - 1) - a
    '            '    rptTemp.Document.Pages.Insert(0, rpt.Document.Pages(Numpage0))
    '            '    a += 1
    '            'Loop
    '            ''===============================================================
    '            'rptTemp.Document.Printer.PrinterName = "\\10.3.220.129\request_texttile"
    '            'rptTemp.Run()
    '            'rptTemp.Document.Print(False, False)
    '            'Else
    '            '    rpt.Document.Printer.PrinterName = "\\10.3.220.129\request_texttile"
    '            '    'rpt.Document.Printer.PrinterName = PrinterName()
    '            '    rpt.Run()
    '            '    rpt.Document.Print(False, False)
    '        End If

    '        Return True

    '    Else
    '        Return False
    '    End If

    'End Function

    Shared Function Print_B2BData(ByVal _ComTax As String, ByVal _b2bRefno As String, ByVal _formtype As String, ByVal _b2bCountry As String, ByVal invh_run_auto As String)
        Try
            Dim strEDIConn As String = ConfigurationManager.ConnectionStrings("EDIConnection").ConnectionString
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
            '    'rpt_B2BData.txtCompanyName.Text = ds.Tables(0).Rows(0).Item("CompanyName") & " " & "(" & ds.Tables(0).Rows(0).Item("Company_tax_id") & ")"
            '    'rpt_B2BData.txtFormName.Text = _FullFormName_1
            '    'rpt_B2BData.txtImportCountry.Text = ds.Tables(0).Rows(0).Item("country_name")
            '    'rpt_B2BData.txtRefNo.Text = ds.Tables(0).Rows(0).Item("reference_code")
            '    'rpt_B2BData.txtIssueDate.Text = ds.Tables(0).Rows(0).Item("import_date")
            '    'rpt_B2BData.txtInvHrunauto.Text = "เลขที่อ้างอิง : " & invh_run_auto


            'End If
            rpt_B2BData.DataSource = ds.Tables(0)
            Return rpt_B2BData

        Catch ex As Exception
            Return Nothing
        End Try
    End Function
#End Region

End Class
