Imports Microsoft.ApplicationBlocks.Data
Imports DFT.Dotnetnuke.ClassLibrary
Imports System.Data.SqlClient
Imports Telerik.Web.UI
Imports System.Threading

Partial Public Class frmReceipt
    Inherits System.Web.UI.Page
    Dim strConn As String = ConfigurationManager.ConnectionStrings("OriginConnection").ConnectionString
    Dim myReader As SqlDataReader = Nothing
    Dim objConn As SqlConnection = Nothing
    '    Dim reader_receipt As SqlDataReader = Nothing
    Dim DataSet_Userreceipt As DataSet = Nothing
    Protected SiteUrl As String
    Dim BType As String = "1"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        BType = Request.QueryString("BType")

        If Not IsPostBack Then
            txtReceiptDate.SelectedDate = Today
            Dim StrRev As String = Session("invh_run")
            Dim StrRev_v2 As String = Session("invh_run_v2")

            If BType = "1" Then
                Select Case Session("ssRoleName")
                    Case "ST-001"
                        rdblReceiptPrinter.Items.Add(New ListItem("เครื่องพิมพ์ใบเสร็จสวัสดิการ (ใบเสร็จเขียว)"))
                        rdblReceiptPrinter.Items(0).Selected = True
                    Case "ST-002", "ST-004N", "ST-004"
                        rdblReceiptPrinter.Items.Add(New ListItem("เครื่องพิมพ์ใบเสร็จสวัสดิการ (ใบเสร็จเขียว)"))
                        rdblReceiptPrinter.Items(0).Selected = True
                    Case Else
                        Call_printName()
                End Select
            ElseIf BType = "2" Then
                Select Case Session("ssRoleName")
                    Case "ST-001"
                        Call_printName()
                    Case "ST-002", "ST-004N", "ST-004"
                        Call_printName()
                    Case "ST-003"
                        rdblReceiptPrinter.Items.Add(New ListItem("เครื่องพิมพ์ใบเสร็จกองทุน (ใบเสร็จเหลือง) (10.14.1.24)"))
                        rdblReceiptPrinter.Items.Add(New ListItem("เครื่องพิมพ์ใบเสร็จกองทุน (ใบเสร็จเหลือง) (10.14.1.21)"))
                        rdblReceiptPrinter.Items.Add(New ListItem("เครื่องพิมพ์ใบเสร็จกองทุน (ใบเสร็จเหลือง) (10.14.1.51)"))
                        Select Case Session("UName").ToLower
                            'Case "wipaporn", "WIPAPORN", "naphat", "NAPHAT", "prapungkong", "PRAPUNGKONG"
                            Case "naphat", "NAPHAT", "prapungkong", "PRAPUNGKONG"
                                rdblReceiptPrinter.Items(1).Selected = True
                            Case "uraiwanl", "uraiwanlds"
                                rdblReceiptPrinter.Items(2).Selected = True
                            Case Else
                                rdblReceiptPrinter.Items(0).Selected = True
                        End Select
                    Case Else
                        rdblReceiptPrinter.Items.Add(New ListItem("เครื่องพิมพ์ใบเสร็จกองทุน (ใบเสร็จเหลือง)"))
                        rdblReceiptPrinter.Items(0).Selected = True
                End Select
            End If
        End If
    End Sub

    Sub Call_printName()
        Dim IPCheck_ As String = CallIP_user()
        If LoadPrint_receipt.HasRows = True Then
            rdblReceiptPrinter.DataSource = LoadPrint_receipt()
            rdblReceiptPrinter.DataTextField = "description_nameprinter"
            rdblReceiptPrinter.DataValueField = "value_Print"
            rdblReceiptPrinter.DataBind()
            If LoadPrint_SetUser_receipt.Tables(0).Rows.Count > 0 Then
                'check user  ก่อนเพื่อไม่ให้ ปรับที่ database ทุกครั้ง
                For iuser As Integer = 0 To LoadPrint_SetUser_receipt.Tables(0).Rows.Count - 1
                    If LoadPrint_SetUser_receipt.Tables(0).Rows(iuser).Item("site_id") = Session("ssRoleName") Then
                        If LoadPrint_SetUser_receipt.Tables(0).Rows(iuser).Item("UserName") = Session("UName") Then
                            If LoadPrint_SetUser_receipt.Tables(0).Rows(iuser).Item("IP_UserPrint") = IPCheck_ Then
                                If LoadPrint_SetUser_receipt.Tables(0).Rows.Count = 1 Then
                                    rdblReceiptPrinter.Items.Item(0).Selected = True
                                    Exit For
                                Else
                                    rdblReceiptPrinter.Items.Item(LoadPrint_SetUser_receipt.Tables(0).Rows(iuser).Item("IPFixPrint") - 1).Selected = True
                                    Exit For
                                End If

                            Else
                                If LoadPrint_SetUser_receipt.Tables(0).Rows.Count = 1 Then
                                    'rdblReceiptPrinter.Items.Item(LoadPrint_SetUser_receipt.Tables(0).Rows(iuser).Item("fix_printer")).Selected = True
                                    rdblReceiptPrinter.Items.Item(0).Selected = True
                                Else
                                    rdblReceiptPrinter.Items.Item(LoadPrint_SetUser_receipt.Tables(0).Rows(iuser).Item("fix_printer") - 1).Selected = True
                                End If
                            End If
                        End If
                    End If
                Next

            Else
                lbl_ErrMSG.Text = "ไม่สามารถพิมพ์ได้เนื่องจากหาเครื่องพิมพ์ไม่พบ"
            End If
        Else
            lbl_ErrMSG.Text = "ไม่สามารถพิมพ์ได้เนื่องจากหาเครื่องพิมพ์ไม่พบ"
        End If
    End Sub

    Function CallIP_user() As String
        Dim string_IP As String = Request.ServerVariables("REMOTE_ADDR")

        Dim ReuestIP As String = Request.UserHostAddress

        Return ReuestIP
    End Function

    Private Function LoadPrint_receipt() As SqlDataReader
        Try
            Dim reader_receipt As SqlDataReader = Nothing
            'objConn = New SqlConnection(strConn)
            reader_receipt = SqlHelper.ExecuteReader(strConn, CommandType.StoredProcedure, "vi_receiptCall_printBysite_id", New SqlParameter("@site_id", CommonUtility.Get_StringValue(Session("ssRoleName"))), New SqlParameter("@UserName", Session("UName")))
            Return reader_receipt

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Function

    Private Function LoadPrint_SetUser_receipt() As DataSet
        Try
            objConn = New SqlConnection(strConn)
            DataSet_Userreceipt = SqlHelper.ExecuteDataset(objConn, CommandType.StoredProcedure, "vi_receiptCall_printBysite_id", New SqlParameter("@site_id", CommonUtility.Get_StringValue(Session("ssRoleName"))), New SqlParameter("@UserName", Session("UName")))
            Return DataSet_Userreceipt
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Function

    Function LoadData(ByVal _inHd As String) As SqlDataReader
        Dim arrWhere() As String = Split(_inHd, ",")
        Dim strWhere As String = ""

        For i As Integer = 0 To arrWhere.Length - 1
            If strWhere = "" Then
                strWhere = "'" & arrWhere(i) & "'"
            Else
                strWhere &= ", '" & arrWhere(i) & "'"
            End If
        Next

        objConn = New SqlConnection(strConn)
        Dim strCommand As String
        strCommand = "SELECT dbo.form_header_edi.*, dbo.form_type.price, REPLACE(dbo.form_type.form_name, 'ฟอร์ม', '') AS form_name, " &
                     "CONVERT(int, dbo.form_header_edi.totalPrintPage * dbo.form_type.price) AS Amount " &
                     "FROM dbo.form_header_edi INNER JOIN " &
                     "dbo.form_type ON dbo.form_header_edi.form_type = dbo.form_type.form_type " &
                     "WHERE (dbo.form_header_edi.invh_run_auto IN (" & strWhere & "))"
        myReader = SqlHelper.ExecuteReader(objConn, CommandType.Text, strCommand)
        Return myReader
    End Function

    Protected Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            If CommonUtility.Get_StringValue(Session("ssRoleName")) = "" Or
            CommonUtility.Get_StringValue(Session("UName")) = "" Then
                Page.ClientScript.RegisterStartupScript(Page.GetType, "fail1", "alert('ไม่สามารถออกใบเสร็จได้ กรุณาเข้าสู่ระบบใหม่อีกครั้ง');", True)
            Else
                Dim ds As New DataSet
                Dim TotalAmount As Decimal = 0
                Dim All_invh_run_auto As String = ""
                If rgReceiptList.Items.Count > 0 Then
                    For i As Integer = 0 To rgReceiptList.Items.Count - 1
                        TotalAmount += CommonUtility.Get_Decimal(rgReceiptList.MasterTableView.Items(i).Cells(8).Text)
                        All_invh_run_auto &= "[" & rgReceiptList.MasterTableView.Items(i).Cells(3).Text & "]"
                    Next
                End If

                ''ByTine 06-01-2559 ปรับปรุงรองรับใบเสร็จใหม่
                Dim SpName As String = "vi_CreateReceipt"
                Dim SpName_v2 As String = "sp_get_receipt"
                Dim _PrinterName As String = PrintReceipt(CommonUtility.Get_StringValue(Session("ssRoleName")), CommonUtility.Get_StringValue(rdblReceiptPrinter.SelectedValue), CommonUtility.Get_StringValue(Session("UName")), Session("_RoleDS"))
                Dim rpt As Object = New receipt_rpt

                Dim isUseShortReport As Boolean = IsUseShortReceipt(CommonUtility.Get_StringValue(Session("ssRoleName")))

                If BType = "2" Then
                    SpName = "vi_CreateReceipt_v2"
                    SpName_v2 = "sp_get_receipt_v2"

                    _PrinterName = PrintReceipt_v2(CommonUtility.Get_StringValue(Session("ssRoleName")), CommonUtility.Get_StringValue(rdblReceiptPrinter.SelectedValue), Session("_RoleDS"), CommonUtility.Get_StringValue(Session("UName")))
                    'rpt = New rpt_Receipt_Yellow
                    If isUseShortReport = True Then
                        rpt = New rpt_Receipt_Short
                    Else
                        rpt = New rpt_Receipt_Yellow
                    End If

                End If

                ds = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, SpName,
                New SqlParameter("@receipt_by", Session("UName")),
                New SqlParameter("@remark", CommonUtility.Get_StringValue(txtRemark.Text)),
                New SqlParameter("@receipt_name", CommonUtility.Get_StringValue(Session("ssCompanyName_Eng"))),
                New SqlParameter("@site_id", CommonUtility.Get_StringValue(Session("ssRoleName"))),
                New SqlParameter("@amount", CommonUtility.Get_Decimal(TotalAmount)),
                New SqlParameter("@bath_text", ThaiBaht(CommonUtility.Get_Decimal(TotalAmount))),
                New SqlParameter("@All_invh_run_auto", CommonUtility.Get_StringValue(All_invh_run_auto)))

                If ds.Tables(0).Rows(0).Item("retStatus") <> 0 Then
                    lbl_ErrMSG.Text = "เกิดข้อผิดพลาดไม่สามารถทำรายการได้"

                Else
                    '=========== Print ผ่าน Report Viewer ===========
                    'Dim dr As SqlDataReader
                    'Dim rpt = New receipt_rpt
                    'Me.WebViewer1.ViewerType = DataDynamics.ActiveReports.Web.ViewerType.AcrobatReader
                    'Dim dataReadRfcard As SqlDataReader = SqlHelper.ExecuteReader(strConn, CommandType.StoredProcedure, "sp_get_receipt", _
                    'New SqlParameter("@B_NO", CommonUtility.Get_StringValue(ds.Tables(0).Rows(0).Item("retMessage"))), _
                    'New SqlParameter("@SITE_ID", CommonUtility.Get_StringValue(Session("ssRoleName"))))

                    'WebViewer1.Report = rpt
                    'WebViewer1.Height = 600

                    'WebViewer1.Report.DataSource = dataReadRfcard
                    'Page.SetFocus(Page)
                    'WebViewer1.Focus()

                    'btnCancel.Text = "ปิดหน้าต่าง"

                    '=========== Print ตรง ===========
                    'Dim dr As SqlDataReader

                    Me.WebViewer1.ViewerType = DataDynamics.ActiveReports.Web.ViewerType.AcrobatReader
                    Dim ds_v2 As DataSet = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, SpName_v2,
                    New SqlParameter("@B_NO", CommonUtility.Get_StringValue(ds.Tables(0).Rows(0).Item("retMessage"))),
                    New SqlParameter("@SITE_ID", CommonUtility.Get_StringValue(Session("ssRoleName"))))

                    'rpt.txtbText.text = ThaiBaht(CommonUtility.Get_Decimal(TotalAmount))
                    'rpt.txtSumamt.Text = CommonUtility.Get_Decimal(TotalAmount)

                    ''ByTine 07-01-2559 กรณีเป็นใบเสร็จใหม่
                    If BType = "2" Then
                        Dim CheckCulture As String = Thread.CurrentThread.CurrentCulture.ToString

                        rpt.lblTime.Text = Now.ToString("HH:mm:ss")
                        rpt.lblDate.Text = Now.Day
                        rpt.lblMonth.Text = ThaiMonth(Now.Month)

                        If CheckCulture <> "th-TH" Then
                            rpt.lblYear.Text = Now.Year + 543
                        Else
                            rpt.lblYear.Text = Now.Year
                        End If

                        'For i As Integer = 0 To ds_v2.Tables(0).Rows.Count - 1
                        '    Dim Total1 As Decimal
                        '    Dim Total2 As Decimal
                        '    Total1 = ds_v2.Tables(0).Rows(i).Item("amt")

                        '    Total2 = Total2 + Total1
                        '    rpt.lblTotalAmount.Text = Total2
                        'Next
                        'rpt.lblTextAmount.Text = ThaiBaht(rpt.lblTotalAmount.Text)

                        rpt.txtCompanyName.Text = CommonUtility.Get_StringValue(ds_v2.Tables(0).Rows(0).Item("receipt_name")) & "  (" & CommonUtility.Get_StringValue(ds_v2.Tables(0).Rows(0).Item("company_taxno")) & ")"

                    End If

                    rpt.Document.Printer.PrinterName = _PrinterName

                    If isUseShortReport = True Then
                        Dim p As New System.Drawing.Printing.PaperSize("Custom Paper Size", 825, 550)
                        rpt.Document.Printer.PaperKind = System.Drawing.Printing.PaperKind.Custom
                        rpt.Document.Printer.PaperSize = p
                    End If

                    rpt.DataSource = ds_v2.Tables(0)
                    btnCancel.Text = "ปิดหน้าต่าง"
                    rpt.Run()
                    rpt.Document.Print(False, False)

                    ClientScript.RegisterStartupScript(Page.GetType(), "mykey", "CloseAndRebind();", True)
                End If
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Function IsUseShortReceipt(ByVal SiteID As String) As Boolean
        Dim ret As Boolean = False
        Try
            Dim cmd As String = "select * from receipt_type_setting where site_id='" & SiteID & "' and use_short_receipt = 1"
            Dim ds As DataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(strConn, CommandType.Text, cmd)
            If ds.Tables(0).Rows.Count > 0 Then
                ret = True
            End If
        Catch ex As Exception

        End Try
        Return ret
    End Function

    Function PrintReceipt(ByVal sendSiteId As String, ByVal selectPrints As String, ByVal sendUser As String, ByVal _RoleID As Boolean) As String
        Dim Print_name As String
        Select Case sendSiteId
            Case "ST-001"
                ''ByTine 22-06-2559 ย้ายใบเสร็จสวัสดิการไปออกเครื่องใบเสร็จกองทุน (สลับเครื่องกัน)
                Print_name = ConfigurationManager.AppSettings("printNameReceiptST_001_01").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                Return Print_name
                ''Select Case sendUser
                ''    'Case "TIPPARPORN", "SURANG" 'set DS 2
                ''    '    Select Case selectPrints 'set เพื่อ เวลาใช้เครื่อง print หลายเครื่อง
                ''    '        Case "1"
                ''    '            Print_name = ConfigurationManager.AppSettings("printNameReceipt_GL03").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                ''    '            Return Print_name
                ''    '    End Select
                ''    Case Else
                ''        Select Case selectPrints 'set เพื่อ เวลาใช้เครื่อง print หลายเครื่อง
                ''            Case "1"
                ''                'Print_name = ConfigurationManager.AppSettings("printNameReceipt_GL").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                ''                Print_name = ConfigurationManager.AppSettings("printNameReceiptST_001_01").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                ''                Return Print_name
                ''            Case "2"
                ''                'Print_name = ConfigurationManager.AppSettings("printNameReceipt_GL02").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                ''                Print_name = ConfigurationManager.AppSettings("printNameReceiptST_001_02").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                ''                Return Print_name
                ''            Case "3"
                ''                Print_name = ConfigurationManager.AppSettings("printNameReceiptST_001_03").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                ''                Return Print_name
                ''            Case "4"
                ''                Print_name = ConfigurationManager.AppSettings("printNameReceiptST_001_04").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                ''                Return Print_name
                ''        End Select
                ''End Select

            Case "ST-001T"
                Select Case selectPrints 'set เพื่อ เวลาใช้เครื่อง print หลายเครื่อง
                    Case "1"
                        Print_name = ConfigurationManager.AppSettings("printNameReceipt_GLT01").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                        Return Print_name

                End Select
            Case "ST-002", "ST-004N", "ST-004" 'คลองเตย
                ''ByTine 22-06-2559 ย้ายใบเสร็จสวัสดิการไปออกเครื่องใบเสร็จกองทุน (สลับเครื่องกัน)
                If _RoleID = True Then
                    Print_name = ConfigurationManager.AppSettings("printNameReceiptST_002_01_DS").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                    Return Print_name
                Else
                    Print_name = ConfigurationManager.AppSettings("printNameReceiptST_002_01").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                    Return Print_name
                End If


                'Select Case selectPrints 'set เพื่อ เวลาใช้เครื่อง print หลายเครื่อง
                '    Case "1"
                '        Print_name = ConfigurationManager.AppSettings("printNameReceiptST_002_01").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                '        Return Print_name
                '    Case "2"
                '        Print_name = ConfigurationManager.AppSettings("printNameReceiptST_002_02").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                '        Return Print_name
                '    Case "3"
                '        Print_name = ConfigurationManager.AppSettings("printNameReceiptST_002_03").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                '        Return Print_name
                '    Case "4"
                '        Print_name = ConfigurationManager.AppSettings("printNameReceiptST_002_04").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                '        Return Print_name
                '    Case "5"
                '        Print_name = ConfigurationManager.AppSettings("printNameReceiptST_002_05").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                '        Return Print_name
                '    Case "6"
                '        Print_name = ConfigurationManager.AppSettings("printNameReceiptST_002_06").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                '        Return Print_name
                '    Case "7" 'by rut add เพิ่ม
                '        Print_name = ConfigurationManager.AppSettings("printNameReceiptST_002_07").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                '        Return Print_name
                '    Case "8" 'by rut add เพิ่ม DS2
                '        Print_name = ConfigurationManager.AppSettings("printNameReceiptST_002_08").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                '        Return Print_name
                'End Select
            Case "CB-003" 'ชลบุรี
                Select Case selectPrints 'set เพื่อ เวลาใช้เครื่อง print หลายเครื่อง
                    Case "1"
                        Print_name = ConfigurationManager.AppSettings("printNameReceiptCB_003_01").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                        Return Print_name
                    Case "2"
                        Print_name = ConfigurationManager.AppSettings("printNameReceiptCB_003_02").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                        Return Print_name
                End Select
            Case "HY-002" 'หาดใหญ่
                Select Case selectPrints 'set เพื่อ เวลาใช้เครื่อง print หลายเครื่อง
                    Case "1"
                        Print_name = ConfigurationManager.AppSettings("printNameReceiptHY_002_01").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                        Return Print_name

                End Select
            Case "CR-006" 'เชียงราย
                Select Case selectPrints 'set เพื่อ เวลาใช้เครื่อง print หลายเครื่อง
                    Case "1"
                        Print_name = ConfigurationManager.AppSettings("printNameReceiptCR_006_01").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                        Return Print_name

                End Select
            Case "CM-001" 'เชียงใหม่
                Select Case selectPrints 'set เพื่อ เวลาใช้เครื่อง print หลายเครื่อง
                    Case "1"
                        Print_name = ConfigurationManager.AppSettings("printNameReceiptCM_001_01").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                        Return Print_name
                    Case "2"
                        Print_name = ConfigurationManager.AppSettings("printNameReceiptCM_001_02").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                        Return Print_name
                End Select
            Case "ST-003" 'สุวรรณภูมิ
                Select Case selectPrints 'set เพื่อ เวลาใช้เครื่อง print หลายเครื่อง
                    Case "1"
                        Print_name = ConfigurationManager.AppSettings("printNameReceiptST_003_01").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                        Return Print_name
                    Case "2"
                        Print_name = ConfigurationManager.AppSettings("printNameReceiptST_003_02").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                        Return Print_name
                End Select
            Case "ST-004XXX" 'สอ
                Select Case selectPrints 'set เพื่อ เวลาใช้เครื่อง print หลายเครื่อง
                    Case "1"
                        Print_name = ConfigurationManager.AppSettings("printNameReceiptST_004_01").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                        Return Print_name
                    Case "2"
                        Print_name = ConfigurationManager.AppSettings("printNameReceiptST_004_02").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                        Return Print_name

                End Select
            Case "SK-004" 'สระแก้ว
                Select Case selectPrints 'set เพื่อ เวลาใช้เครื่อง print หลายเครื่อง
                    Case "1"
                        Print_name = ConfigurationManager.AppSettings("printNameReceiptSK_004_01").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                        Return Print_name

                End Select
            Case "NK-005" 'หนองคาย
                Select Case selectPrints 'set เพื่อ เวลาใช้เครื่อง print หลายเครื่อง
                    Case "1"
                        Print_name = ConfigurationManager.AppSettings("printNameReceiptNK_005_01").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                        Return Print_name

                End Select
            Case "SG-007" 'ศรีสะเกษ
                Select Case selectPrints 'set เพื่อ เวลาใช้เครื่อง print หลายเครื่อง
                    Case "1"
                        Print_name = ConfigurationManager.AppSettings("printNameReceiptSG_007_01").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                        Return Print_name

                End Select
            Case "TK-008" 'ตาก
                Select Case selectPrints 'set เพื่อ เวลาใช้เครื่อง print หลายเครื่อง
                    Case "1"
                        Print_name = ConfigurationManager.AppSettings("printNameReceiptTK_008_01").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                        Return Print_name

                End Select
            Case "MH-009" 'มุกดาหาร
                Select Case selectPrints 'set เพื่อ เวลาใช้เครื่อง print หลายเครื่อง
                    Case "1"
                        Print_name = ConfigurationManager.AppSettings("printNameReceiptMH_009_01").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                        Return Print_name

                End Select
            Case "KR-010" 'สคต.เขต 10 (กาญจนบุรี)
                Select Case selectPrints 'set เพื่อ เวลาใช้เครื่อง print หลายเครื่อง
                    Case "1"
                        Print_name = ConfigurationManager.AppSettings("printNameReceiptKR_010_01").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                        Return Print_name

                End Select
            Case "PN-83-001" 'สพจ.ภูเก็ต
                'Dim printer_key As String = "printNameReceiptPN_83_001_0" & selectPrints & "_v2"
                'Print_name = ConfigurationManager.AppSettings(printer_key).ToString()
                Print_name = ConfigurationManager.AppSettings("printNameReceiptPN_83_001_01_v2").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                Return Print_name
            Case "PN-95-001" 'สพจ.ยะลา
                'Dim printer_key As String = "printNameReceiptPN_95_001_0" & selectPrints & "_v2"
                'Print_name = ConfigurationManager.AppSettings(printer_key).ToString()
                Print_name = ConfigurationManager.AppSettings("printNameReceiptPN_95_001_01_v2").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                Return Print_name
            Case "PN-96-001" 'สพจ.นราธิวาส
                'Dim printer_key As String = "printNameReceiptPN_96_001_0" & selectPrints & "_v2"
                'Print_name = ConfigurationManager.AppSettings(printer_key).ToString()
                Print_name = ConfigurationManager.AppSettings("printNameReceiptPN_96_001_01_v2").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                Return Print_name
            Case "PN-91-001" 'สพจ.สตูล
                'Dim printer_key As String = "printNameReceiptPN_91_001_0" & selectPrints & "_v2"
                'Print_name = ConfigurationManager.AppSettings(printer_key).ToString()
                Print_name = ConfigurationManager.AppSettings("printNameReceiptPN_91_001_01_v2").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                Return Print_name
            Case "PN-48-001" 'สพจ.นครพนม
                'Dim printer_key As String = "printNameReceiptPN_48_001_0" & selectPrints & "_v2"
                'Print_name = ConfigurationManager.AppSettings(printer_key).ToString()
                Print_name = ConfigurationManager.AppSettings("printNameReceiptPN_48_001_01_v2").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                Return Print_name
            Case "PN-51-001" 'สพจ.ลำพูน
                'Dim printer_key As String = "printNameReceiptPN_48_001_0" & selectPrints & "_v2"
                'Print_name = ConfigurationManager.AppSettings(printer_key).ToString()
                Print_name = ConfigurationManager.AppSettings("printNameReceiptPN_51_001_01_v2").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                Return Print_name
        End Select
    End Function

    Function PrintReceipt_v2(ByVal sendSiteId As String, ByVal selectPrints As String, ByVal _RoleID As Boolean, ByVal sendUser As String) As String
        Dim Print_name As String
        Select Case sendSiteId
            Case "ST-001"
                'Print_name = ConfigurationManager.AppSettings("printNameReceiptST_001_01_v2").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                'Return Print_name
                Select Case sendUser
                    'Case "TIPPARPORN", "SURANG" 'set DS 2
                    '    Select Case selectPrints 'set เพื่อ เวลาใช้เครื่อง print หลายเครื่อง
                    '        Case "1"
                    '            Print_name = ConfigurationManager.AppSettings("printNameReceipt_GL03").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                    '            Return Print_name
                    '    End Select
                    Case Else
                        ''ByTine 22-06-2559 ย้ายใบเสร็จกองทุนไปออกเครื่องใบเสร็จสวัสดิการ (สลับเครื่องกัน)
                        Select Case selectPrints 'set เพื่อ เวลาใช้เครื่อง print หลายเครื่อง
                            Case "1"
                                'Print_name = ConfigurationManager.AppSettings("printNameReceipt_GL").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                                Print_name = ConfigurationManager.AppSettings("printNameReceiptST_001_01_v2").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                                Return Print_name
                            Case "2"
                                'Print_name = ConfigurationManager.AppSettings("printNameReceipt_GL02").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                                Print_name = ConfigurationManager.AppSettings("printNameReceiptST_001_02_v2").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                                Return Print_name
                            Case "3"
                                Print_name = ConfigurationManager.AppSettings("printNameReceiptST_001_03_v2").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                                Return Print_name
                            Case "4"
                                Print_name = ConfigurationManager.AppSettings("printNameReceiptST_001_04_v2").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                                Return Print_name
                        End Select
                End Select
            Case "ST-001T"
                Print_name = ConfigurationManager.AppSettings("printNameReceipt_GLT01_v2").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                Return Print_name
            Case "ST-002", "ST-004N", "ST-004" 'คลองเตย
                '//2017-10-07 ไม่ต้องตรวจเงื่อนไข ds แล้ว ออกเครื่องเดียวกัน เฉพาะคลองเคย
                _RoleID = False
                If _RoleID = True Then
                    Print_name = ConfigurationManager.AppSettings("printNameReceiptST_002_01DS_v2").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                Else
                    '  Print_name = ConfigurationManager.AppSettings("printNameReceiptST_002_01").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                    ''ByTine 22-06-2559 ย้ายใบเสร็จกองทุนไปออกเครื่องใบเสร็จสวัสดิการ (สลับเครื่องกัน)
                    Select Case selectPrints 'set เพื่อ เวลาใช้เครื่อง print หลายเครื่อง
                        Case "1"
                            Print_name = ConfigurationManager.AppSettings("printNameReceiptST_002_01_v2").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                            Return Print_name
                        Case "2"
                            Print_name = ConfigurationManager.AppSettings("printNameReceiptST_002_02_v2").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                            Return Print_name
                        Case "3"
                            Print_name = ConfigurationManager.AppSettings("printNameReceiptST_002_03_v2").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                            Return Print_name
                        Case "4"
                            Print_name = ConfigurationManager.AppSettings("printNameReceiptST_002_04_v2").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                            Return Print_name
                        Case "5"
                            Print_name = ConfigurationManager.AppSettings("printNameReceiptST_002_05_v2").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                            Return Print_name
                        Case "6"
                            Print_name = ConfigurationManager.AppSettings("printNameReceiptST_002_06_v2").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                            Return Print_name
                        Case "7" 'by rut add เพิ่ม
                            Print_name = ConfigurationManager.AppSettings("printNameReceiptST_002_07_v2").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                            Return Print_name
                            'Case "8" 'by rut add เพิ่ม DS2
                            '    Print_name = ConfigurationManager.AppSettings("printNameReceiptST_002_08_v2").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                            '    Return Print_name
                    End Select
                End If
                Return Print_name
            Case "CB-003" 'ชลบุรี
                Print_name = ConfigurationManager.AppSettings("printNameReceiptCB_003_01_v2").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                Return Print_name
            Case "HY-002" 'หาดใหญ่
                Print_name = ConfigurationManager.AppSettings("printNameReceiptHY_002_01_v2").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                Return Print_name
            Case "CR-006" 'เชียงราย
                Print_name = ConfigurationManager.AppSettings("printNameReceiptCR_006_01_v2").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                Return Print_name
            Case "CM-001" 'เชียงใหม่
                Print_name = ConfigurationManager.AppSettings("printNameReceiptCM_001_01_v2").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                Return Print_name
            Case "ST-003" 'สุวรรณภูมิ
                If rdblReceiptPrinter.Items(0).Selected = True Then
                    Print_name = ConfigurationManager.AppSettings("printNameReceiptST_003_01_v2").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                ElseIf rdblReceiptPrinter.Items(2).Selected = True Then
                    Print_name = ConfigurationManager.AppSettings("printNameReceiptST_003_03").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                ElseIf rdblReceiptPrinter.Items(1).Selected = True Then
                    Print_name = ConfigurationManager.AppSettings("printNameReceiptST_003_02_v2").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                End If
                Return Print_name
            Case "ST-004XXX" 'สอ
                Print_name = ConfigurationManager.AppSettings("printNameReceiptST_004_01_v2").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                Return Print_name
            Case "SK-004" 'สระแก้ว
                Print_name = ConfigurationManager.AppSettings("printNameReceiptSK_004_01_v2").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                Return Print_name
            Case "NK-005" 'หนองคาย
                Print_name = ConfigurationManager.AppSettings("printNameReceiptNK_005_01_v2").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                Return Print_name
            Case "SG-007" 'ศรีสะเกษ
                Print_name = ConfigurationManager.AppSettings("printNameReceiptSG_007_01_v2").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                Return Print_name
            Case "TK-008" 'ตาก
                Print_name = ConfigurationManager.AppSettings("printNameReceiptTK_008_01_v2").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                Return Print_name
            Case "MH-009" 'มุกดาหาร
                Print_name = ConfigurationManager.AppSettings("printNameReceiptMH_009_01_v2").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                Return Print_name
            Case "KR-010" 'สคต.เขต 10 (กาญจนบุรี)
                Print_name = ConfigurationManager.AppSettings("printNameReceiptKR_010_01_v2").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                Return Print_name
            Case "PN-83-001" 'สพจ.ภูเก็ต
                'Dim printer_key As String = "printNameReceiptPN_83_001_0" & selectPrints & "_v2"
                'Print_name = ConfigurationManager.AppSettings(printer_key).ToString()
                Print_name = ConfigurationManager.AppSettings("printNameReceiptPN_83_001_01_v2").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                Return Print_name
            Case "PN-95-001" 'สพจ.ยะลา
                'Dim printer_key As String = "printNameReceiptPN_95_001_0" & selectPrints & "_v2"
                'Print_name = ConfigurationManager.AppSettings(printer_key).ToString()
                Print_name = ConfigurationManager.AppSettings("printNameReceiptPN_95_001_01_v2").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                Return Print_name
            Case "PN-96-001" 'สพจ.นราธิวาส
                'Dim printer_key As String = "printNameReceiptPN_96_001_0" & selectPrints & "_v2"
                'Print_name = ConfigurationManager.AppSettings(printer_key).ToString()
                Print_name = ConfigurationManager.AppSettings("printNameReceiptPN_96_001_01_v2").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                Return Print_name
            Case "PN-91-001" 'สพจ.สตูล
                'Dim printer_key As String = "printNameReceiptPN_91_001_0" & selectPrints & "_v2"
                'Print_name = ConfigurationManager.AppSettings(printer_key).ToString()
                Print_name = ConfigurationManager.AppSettings("printNameReceiptPN_91_001_01_v2").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                Return Print_name
            Case "PN-48-001" 'สพจ.นครพนม
                'Dim printer_key As String = "printNameReceiptPN_48_001_0" & selectPrints & "_v2"
                'Print_name = ConfigurationManager.AppSettings(printer_key).ToString()
                Print_name = ConfigurationManager.AppSettings("printNameReceiptPN_48_001_01_v2").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                Return Print_name
            Case "PN-51-001" 'สพจ.ลำพูน
                'Dim printer_key As String = "printNameReceiptPN_48_001_0" & selectPrints & "_v2"
                'Print_name = ConfigurationManager.AppSettings(printer_key).ToString()
                Print_name = ConfigurationManager.AppSettings("printNameReceiptPN_51_001_01_v2").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                Return Print_name
        End Select
    End Function

    Protected Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        If btnCancel.Text = "ปิดหน้าต่าง" Then
            ClientScript.RegisterStartupScript(Page.GetType(), "mykey", "CloseAndRebind();", True)
        Else
            ClientScript.RegisterStartupScript(Page.GetType(), "mykey", "CancelEdit();", True)
        End If
    End Sub

    Function ThaiMonth(ByVal Month As String) As String
        Try
            Select Case Month
                Case "01" Or "1"
                    Month = "มกราคม"
                Case "02" Or "2"
                    Month = "กุมภาพันธ์"
                Case "03" Or "3"
                    Month = "มีนาคม"
                Case "04" Or "4"
                    Month = "เมษายน"
                Case "05" Or "5"
                    Month = "พฤษภาคม"
                Case "06" Or "6"
                    Month = "มิถุนายน"
                Case "07" Or "7"
                    Month = "กรกฏาคม"
                Case "08" Or "8"
                    Month = "สิงหาคม"
                Case "09" Or "9"
                    Month = "กันยายน"
                Case "10"
                    Month = "ตุลาคม"
                Case "11"
                    Month = "พฤศจิกายน"
                Case "12"
                    Month = "ธันวาคม"
            End Select

            Return Month

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Function

    Private Sub rgReceiptList_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles rgReceiptList.DataBound
        myReader.Close()
        objConn.Close()
    End Sub

    'Private Sub rgReceiptList_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgReceiptList.ItemDataBound
    '    If TypeOf e.Item Is GridDataItem Then
    '        Dim dataItem As GridDataItem = CType(e.Item, GridDataItem)
    '        If dataItem("totalPrintPage").Text.Trim() <> "&nbsp;" And dataItem("price").Text.Trim() <> "&nbsp;" Then
    '            dataItem("Amount").Text = CommonUtility.Get_StringValue(CType(dataItem("totalPrintPage").Text, Decimal) * CType(dataItem("price").Text, Decimal)) & ".00"
    '        End If
    '    End If
    'End Sub

    Private Sub rgReceiptList_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgReceiptList.NeedDataSource
        If BType = "1" Then
            rgReceiptList.DataSource = LoadData(Session("invh_run"))
        ElseIf BType = "2" Then
            rgReceiptList.DataSource = LoadData(Session("invh_run_v2"))
        End If

    End Sub

    Public Shared Function ThaiBaht(ByVal pAmount As Double) As String
        If pAmount = 0 Then
            Return "ศูนย์บาทถ้วน"
        End If
        Dim _integerValue As String ' จำนวนเต็ม    
        Dim _decimalValue As String ' ทศนิยม     
        Dim _integerTranslatedText As String ' จำนวนเต็ม ภาษาไทย     
        Dim _integerTranslatedText2 As String
        Dim _decimalTranslatedText As String ' ทศนิยมภาษาไทย    
        _integerValue = Format(pAmount, "####.00") ' จัด Format ค่าเงินเป็นตัวเลข 2 หลัก   
        _decimalValue = Mid(_integerValue, Len(_integerValue) - 1, 2) ' ทศนิยม    
        _integerValue = Mid(_integerValue, 1, Len(_integerValue) - 3) ' จำนวนเต็ม    
        ' แปลง จำนวนเต็ม เป็น ภาษาไทย    
        _integerTranslatedText = NumberToText(CDbl(_integerValue))
        ' แปลง ทศนิยม เป็น ภาษาไทย     
        If CDbl(_decimalValue) <> 0 Then
            _decimalTranslatedText = NumberToText(CDbl(_decimalValue))
        Else
            _decimalTranslatedText = ""
        End If
        ' ถ้าไม่มีทศนิม    
        If _decimalTranslatedText.Trim = "" Then
            _integerTranslatedText += "บาทถ้วน"
        Else
            _integerTranslatedText += "บาท" & _decimalTranslatedText & "สตางค์"
        End If
        'ใส่เพิ่มเพื่อมีวงเล็บ
        _integerTranslatedText2 = "(" & _integerTranslatedText & ")"
        Return _integerTranslatedText2
    End Function

    Private Shared Function NumberToText(ByVal pAmount As Double) As String
        ' ตัวอักษร   
        Dim _numberText() As String = {"", "หนึ่ง", "สอง", "สาม", "สี่", "ห้า", "หก", "เจ็ด", "แปด", "เก้า", "สิบ"}
        ' หลัก หน่วย สิบ ร้อย พัน ...   
        Dim _digit() As String = {"", "สิบ", "ร้อย", "พัน", "หมื่น", "แสน", "ล้าน"}
        Dim _value As String, _aWord As String, _text As String
        Dim _numberTranslatedText As String = ""
        Dim _length, _digitPosition As Integer
        _value = pAmount.ToString
        _length = Len(_value)
        ' ขนาดของ ข้อมูลที่ต้องการแปลง เช่น 122200 มีขนาด เท่ากับ 6   
        For i As Integer = 0 To _length - 1
            ' วนลูป เริ่มจาก 0 จนถึง (ขนาด - 1)       
            ' ตำแหน่งของ หลัก (digit) ของตัวเลข      
            ' เช่น       ' ตำแหน่งหลักที่0 (หลักหน่วย)      
            ' ตำแหน่งหลักที่1 (หลักสิบ)       
            ' ตำแหน่งหลักที่2 (หลักร้อย)      
            ' ถ้าเป็นข้อมูล i = 7 ตำแหน่งหลักจะเท่ากับ 1 (หลักสิบ)      
            ' ถ้าเป็นข้อมูล i = 9 ตำแหน่งหลักจะเท่ากับ 3 (หลักพัน)       
            ' ถ้าเป็นข้อมูล i = 13 ตำแหน่งหลักจะเท่ากับ 1 (หลักสิบ)      
            _digitPosition = i - (6 * ((i - 1) \ 6))
            _aWord = Mid(_value, Len(_value) - i, 1)
            _text = ""
            Select Case _digitPosition
                Case 0 ' หลักหน่วย               
                    If _aWord = "1" And _length > 1 Then
                        ' ถ้าเป็นเลข 1 และมีขนาดมากกว่า 1 ให้มีค่าเท่ากับ "เอ็ด"                  
                        _text = "เอ็ด"
                    ElseIf _aWord <> "0" Then
                        ' ถ้าไม่ใช่เลข 0 ให้หา ตัวอักษร ใน _numberText()                   
                        _text = _numberText(CInt(_aWord))
                    End If
                Case 1 ' หลักสิบ               
                    If _aWord = "1" Then
                        ' ถ้าเป็นเลข 1 ไม่ต้องมี ตัวอักษร อื่นอีก นอกจากคำว่า "สิบ"                  
                        '_numberTranslatedText = "สิบ" & _numberTranslatedText                  
                        _text = _digit(_digitPosition)
                    ElseIf _aWord = "2" Then
                        ' ถ้าเป็นเลข 2 ให้ตัวอักษรคือ "ยี่สิบ"                  
                        _text = "ยี่" & _digit(_digitPosition)
                    ElseIf _aWord <> "0" Then
                        ' ถ้าไม่ใช่เลข 0 ให้หา ตัวอักษร ใน _numberText() และหาหลัก(digit) ใน _digit()                 
                        _text = _numberText(CInt(_aWord)) & _digit(_digitPosition)
                    End If
                Case 2, 3, 4, 5 ' หลักร้อย ถึง แสน               
                    If _aWord <> "0" Then
                        _text = _numberText(CInt(_aWord)) & _digit(_digitPosition)
                    End If
                Case 6 ' หลักล้าน              
                    If _aWord = "0" Then
                        _text = "ล้าน"
                    ElseIf _aWord = "1" And _length - 1 > i Then
                        _text = "เอ็ดล้าน"
                    Else
                        _text = _numberText(CInt(_aWord)) & _digit(_digitPosition)
                    End If
            End Select
            _numberTranslatedText = _text & _numberTranslatedText
        Next
        Return _numberTranslatedText
    End Function

End Class