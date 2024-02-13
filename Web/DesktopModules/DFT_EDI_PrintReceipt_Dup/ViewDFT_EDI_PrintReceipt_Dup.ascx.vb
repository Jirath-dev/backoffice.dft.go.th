Imports System.Web.UI
Imports System.Collections.Generic
Imports System.Reflection

Imports DotNetNuke
Imports DotNetNuke.Services.Exceptions
Imports DotNetNuke.Services.Localization

Imports System.Data.SqlClient
Imports DFT.Dotnetnuke.ClassLibrary
Imports Microsoft.ApplicationBlocks.Data

Imports Telerik.Web.UI
Imports Receipt_Print
Imports System.Threading

Imports System.Web.HttpRequest
Namespace NTi.Modules.DFT_EDI_PrintReceipt_Dup
    Partial Class ViewDFT_EDI_PrintReceipt_Dup
        Inherits Entities.Modules.PortalModuleBase
        Dim strEDIConn As String = ConfigurationManager.ConnectionStrings("OriginConnection").ConnectionString
        Dim myReader As SqlDataReader = Nothing
        Dim objConn As SqlConnection = Nothing
        Dim DataSet_Userreceipt As DataSet = Nothing
        Dim Receipt_Count As Integer = 0
        Dim Receipt_Amount As Decimal = 0.0

        Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            'สร้าง Label ชื่อ lblRoleID แล้ว set Visible = false ด้วย
            'Check ว่า User ที่ Login เข้ามาอยู่ Site ไหน
            Dim myRoleControllers As New DotNetNuke.Security.Roles.RoleController()
            Dim i As Integer = 0
            For i = 0 To MyBase.UserInfo.Roles.Length - 1
                Dim myRoleInfo As DotNetNuke.Security.Roles.RoleInfo = myRoleControllers.GetRoleByName(0, MyBase.UserInfo.Roles(i))
                'by rut 7-09-2555 ใช้วันที่ 22-01-2556 ใช้แทนด้านล่าง ต้องเพิ่มใน Table "RoleList" แทน
                If Get_ListRoles(myRoleInfo.RoleID, myRoleInfo.RoleName) <> "" Then
                    Exit For
                End If
                'Select Case myRoleInfo.RoleID
                '    Case 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 26, 28
                '        lblRoleID.Text = myRoleInfo.RoleName
                '        Session("ssRoleName") = lblRoleID.Text
                '        Exit For
                'End Select
            Next i

            If Not Page.IsPostBack Then
                Session("UName") = UserInfo.Username
                rdpReceiptDate.SelectedDate = Today
                rdpReceiptDate_v2.SelectedDate = Today

                Select Case Session("ssRoleName")
                    Case "ST-001", "ST-002", "ST-004N", "ST-004"
                        Call_printName()
                        rdblReceiptPrinter.Items.Add(New ListItem("เครื่องพิมพ์ใบเสร็จสวัสดิการ (ใบเสร็จเขียว)"))
                        rdblReceiptPrinter.Items(0).Selected = True
                    Case "ST-003"

                        rdblReceiptPrinter_v2.Items.Add(New ListItem("เครื่องพิมพ์ใบเสร็จกองทุน (ใบเสร็จเหลือง) (10.14.1.24)", 0))
                        rdblReceiptPrinter_v2.Items.Add(New ListItem("เครื่องพิมพ์ใบเสร็จกองทุน (ใบเสร็จเหลือง) (10.14.1.21)", 1))
                        rdblReceiptPrinter_v2.Items.Add(New ListItem("เครื่องพิมพ์ใบเสร็จกองทุน (ใบเสร็จเหลือง) (10.14.1.51)", 2))
                        Call_printName()
                        Select Case Session("UName").ToLower
                            'Case "wipaporn", "WIPAPORN", "naphat", "NAPHAT", "prapungkong", "PRAPUNGKONG"
                            Case "naphat", "NAPHAT", "prapungkong", "PRAPUNGKONG"
                                rdblReceiptPrinter_v2.Items(1).Selected = True
                            Case "uraiwanl", "uraiwanlds"
                                rdblReceiptPrinter_v2.Items(2).Selected = True
                            Case Else
                                rdblReceiptPrinter_v2.Items(0).Selected = True
                        End Select

                    Case Else

                        rdblReceiptPrinter_v2.Items.Add(New ListItem("เครื่องพิมพ์ใบเสร็จกองทุน (ใบเสร็จเหลือง)"))
                        rdblReceiptPrinter_v2.Items(0).Selected = True
                        Call_printName()
                End Select

                'Call_printName()

                'If Session("ssRoleName") = "ST-003" Then
                '    rdblReceiptPrinter_v2.Items.Add(New ListItem("เครื่องพิมพ์ใบเสร็จกองทุน (ใบเสร็จเหลือง) (10.14.1.24)"))
                '    rdblReceiptPrinter_v2.Items.Add(New ListItem("เครื่องพิมพ์ใบเสร็จกองทุน (ใบเสร็จเหลือง) (10.14.1.21)"))
                '    Select Case UserInfo.Username.ToLower
                '        Case "wipaporn", "WIPAPORN", "naphat", "NAPHAT"
                '            rdblReceiptPrinter_v2.Items(1).Selected = True
                '        Case Else
                '            rdblReceiptPrinter_v2.Items(0).Selected = True
                '    End Select
                'Else
                '    rdblReceiptPrinter_v2.Items.Add(New ListItem("เครื่องพิมพ์ใบเสร็จกองทุน (ใบเสร็จเหลือง)"))
                '    rdblReceiptPrinter_v2.Items(0).Selected = True
                'End If

            End If

            Label1.Text = CallIP_user()
            Label2.Text = LoadPrint_SetUser_receipt.Tables(0).Rows(0).Item("IP_UserPrint") 'LoadPrint_receipt.Item("IP_UserPrint")
        End Sub
#Region "by rut"
        Function Get_ListRoles(ByVal ByRoleID As Integer, ByVal ByRoleName As String) As String
            Dim DSRoles As SqlDataReader
            Dim strEDIConn As String = ConfigurationManager.ConnectionStrings("OriginConnection").ConnectionString

            DSRoles = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "RoleList_GetList", _
                            New SqlParameter("@ListRoleNameCase", ByRoleID))

            Dim strListRole As String = ""

            If DSRoles.HasRows Then
                strListRole = ByRoleName
                lblRoleID.Text = strListRole
                Session("ssRoleName") = lblRoleID.Text
            End If

            Return strListRole
        End Function
#End Region
        Function CallIP_user() As String
            Dim string_IP As String = Request.ServerVariables("REMOTE_ADDR")

            Dim ReuestIP As String = Request.UserHostAddress

            Return ReuestIP
        End Function

        Sub Call_printName()
            Dim IPCheck_ As String = CallIP_user()
            If LoadPrint_receipt.HasRows = True Then
                Select Case Session("ssRoleName")
                    Case "ST-001", "ST-002", "ST-004N", "ST-004"
                        rdblReceiptPrinter_v2.DataSource = LoadPrint_receipt()
                        rdblReceiptPrinter_v2.DataTextField = "description_nameprinter"
                        rdblReceiptPrinter_v2.DataValueField = "value_Print"
                        rdblReceiptPrinter_v2.DataBind()
                    Case Else
                        rdblReceiptPrinter.DataSource = LoadPrint_receipt()
                        rdblReceiptPrinter.DataTextField = "description_nameprinter"
                        rdblReceiptPrinter.DataValueField = "value_Print"
                        rdblReceiptPrinter.DataBind()
                End Select
               

                ' ''ByTine 06-01-2559 ปรรับปรุงใบเสร็จใหม่
                'rdblReceiptPrinter_v2.DataSource = LoadPrint_receipt()
                'rdblReceiptPrinter_v2.DataTextField = "description_nameprinter"
                'rdblReceiptPrinter_v2.DataValueField = "value_Print"
                'rdblReceiptPrinter_v2.DataBind()

                If LoadPrint_SetUser_receipt.Tables(0).Rows.Count > 0 Then
                    'check user  ก่อนเพื่อไม่ให้ ปรับที่ database ทุกครั้ง
                    For iuser As Integer = 0 To LoadPrint_SetUser_receipt.Tables(0).Rows.Count - 1
                        If LoadPrint_SetUser_receipt.Tables(0).Rows(iuser).Item("site_id") = Session("ssRoleName") Then
                            If LoadPrint_SetUser_receipt.Tables(0).Rows(iuser).Item("UserName") = Session("UName") Then
                                If LoadPrint_SetUser_receipt.Tables(0).Rows(iuser).Item("IP_UserPrint") = IPCheck_ Then
                                    If LoadPrint_SetUser_receipt.Tables(0).Rows.Count = 1 Then
                                        rdblReceiptPrinter.Items.Item(0).Selected = True
                                        rdblReceiptPrinter_v2.Items.Item(0).Selected = True
                                        Exit For
                                    Else
                                        Select Case Session("ssRoleName")
                                            Case "ST-001", "ST-002", "ST-004N", "ST-004"
                                                'rdblReceiptPrinter.Items.Item(LoadPrint_SetUser_receipt.Tables(0).Rows(iuser).Item("IPFixPrint") - 1).Selected = True
                                                rdblReceiptPrinter_v2.Items.Item(LoadPrint_SetUser_receipt.Tables(0).Rows(iuser).Item("IPFixPrint") - 1).Selected = True
                                            Case Else
                                                rdblReceiptPrinter.Items.Item(LoadPrint_SetUser_receipt.Tables(0).Rows(iuser).Item("IPFixPrint") - 1).Selected = True
                                                'rdblReceiptPrinter_v2.Items.Item(LoadPrint_SetUser_receipt.Tables(0).Rows(iuser).Item("IPFixPrint") - 1).Selected = True
                                        End Select
                                        
                                        Exit For
                                    End If

                                Else
                                    If LoadPrint_SetUser_receipt.Tables(0).Rows.Count = 1 Then
                                        Select Case Session("ssRoleName")
                                            Case "ST-001", "ST-002", "ST-004N", "ST-004"
                                                'rdblReceiptPrinter_v2.Items.Item(LoadPrint_SetUser_receipt.Tables(0).Rows(iuser).Item("fix_printer")).Selected = True
                                                rdblReceiptPrinter_v2.Items.Item(0).Selected = True
                                            Case Else
                                                'rdblReceiptPrinter.Items.Item(LoadPrint_SetUser_receipt.Tables(0).Rows(iuser).Item("fix_printer")).Selected = True
                                                rdblReceiptPrinter.Items.Item(0).Selected = True
                                                'rdblReceiptPrinter_v2.Items.Item(0).Selected = True
                                        End Select
                                    Else
                                        Select Case Session("ssRoleName")
                                            Case "ST-001", "ST-002", "ST-004N", "ST-004"
                                                'rdblReceiptPrinter.Items.Item(LoadPrint_SetUser_receipt.Tables(0).Rows(iuser).Item("fix_printer") - 1).Selected = True
                                                rdblReceiptPrinter_v2.Items.Item(LoadPrint_SetUser_receipt.Tables(0).Rows(iuser).Item("fix_printer") - 1).Selected = True
                                            Case Else
                                                rdblReceiptPrinter.Items.Item(LoadPrint_SetUser_receipt.Tables(0).Rows(iuser).Item("fix_printer") - 1).Selected = True
                                                'rdblReceiptPrinter_v2.Items.Item(LoadPrint_SetUser_receipt.Tables(0).Rows(iuser).Item("fix_printer") - 1).Selected = True
                                        End Select
                                        
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

        Private Function LoadPrint_receipt() As SqlDataReader
            Try
                Dim reader_receipt As SqlDataReader = Nothing
                reader_receipt = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "vi_receiptCall_printBysite_id", New SqlParameter("@site_id", CommonUtility.Get_StringValue(Session("ssRoleName"))), New SqlParameter("@UserName", Session("UName")))
                Return reader_receipt
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End Function

        Private Function LoadPrint_SetUser_receipt() As DataSet
            Try
                objConn = New SqlConnection(strEDIConn)
                DataSet_Userreceipt = SqlHelper.ExecuteDataset(objConn, CommandType.StoredProcedure, "vi_receiptCall_printBysite_id", New SqlParameter("@site_id", CommonUtility.Get_StringValue(Session("ssRoleName"))), New SqlParameter("@UserName", Session("UName")))
                Return DataSet_Userreceipt
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End Function

        Private Sub rgReceiptList_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles rgReceiptList.DataBound
            myReader.Close()
            objConn.Close()
        End Sub

        Private Sub rgReceiptList_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgReceiptList.ItemDataBound
            If Not (e.Item.FindControl("imbPrint") Is Nothing) Then
                CType(e.Item.FindControl("imbPrint"), ImageButton).Attributes("onmouseover") = "this.style.cursor='hand';"

                Dim dataItem As GridDataItem = CType(e.Item, GridDataItem)
                If dataItem("bill_no").Text.Trim() <> "&nbsp;" Then
                    Receipt_Count = Receipt_Count + 1
                End If

                If dataItem("amount").Text.Trim() <> "&nbsp;" Then
                    Receipt_Amount = Receipt_Amount + CType(dataItem("amount").Text, Decimal)
                End If
            End If

            ''ByTine 05-01-2559 ปรับใบเสร็จแบบใหม่ (Tab ใบเสร็จเก่า)
            RadTabStrip2.Tabs.Item(0).Text = "ใบเสร็จสวัสดิการ (ใบเสร็จเขียว)" & " (" & rgReceiptList.MasterTableView.Items.Count & ") "
        End Sub


        Private Sub rgReceiptListNew_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgReceiptListNew.ItemDataBound
            If Not (e.Item.FindControl("imbPrint_v2") Is Nothing) Then
                CType(e.Item.FindControl("imbPrint_v2"), ImageButton).Attributes("onmouseover") = "this.style.cursor='hand';"

                Dim dataItem As GridDataItem = CType(e.Item, GridDataItem)
                If dataItem("bill_no").Text.Trim() <> "&nbsp;" Then
                    Receipt_Count = Receipt_Count + 1
                End If

                If dataItem("amount").Text.Trim() <> "&nbsp;" Then
                    Receipt_Amount = Receipt_Amount + CType(dataItem("amount").Text, Decimal)
                End If
            End If

            ''ByTine 05-01-2559 ปรับใบเสร็จแบบใหม่ (Tab ใบเสร็จใหม่)
            RadTabStrip2.Tabs.Item(1).Text = "ใบเสร็จกองทุน (ใบเสร็จเหลือง)" & " (" & rgReceiptListNew.MasterTableView.Items.Count & ") "
            'RadTabStrip2.Tabs.Item(1).Selected = True
        End Sub

        Function LoadReceiptList() As SqlDataReader
            Try
                objConn = New SqlConnection(strEDIConn)
                Dim strCommand As String
                strCommand = "select * from RECEIPT_BILL_HEADER where convert(varchar(8),BILL_DATE,112)='" & FunctionUtility.DMY2YMD(rdpReceiptDate.SelectedDate.Value) & "' and SITE_ID='" & Session("ssRoleName") & "' order by BILL_NO desc"
                myReader = SqlHelper.ExecuteReader(objConn, CommandType.Text, strCommand)
                Return myReader
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End Function

        ''ByTine 06-01-2559 ปรับปรุงใบเสร็จใหม่
        Function LoadReceiptListNew() As SqlDataReader
            Try
                objConn = New SqlConnection(strEDIConn)
                Dim strCommand As String
                strCommand = "select * from receipt_bill_header_v2 where convert(varchar(8),BILL_DATE,112)='" & FunctionUtility.DMY2YMD(rdpReceiptDate_v2.SelectedDate.Value) & "' and SITE_ID='" & Session("ssRoleName") & "' order by BILL_NO desc"
                myReader = SqlHelper.ExecuteReader(objConn, CommandType.Text, strCommand)
                Return myReader
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End Function

        Protected Sub rdpReceiptDate_SelectedDateChanged(ByVal sender As System.Object, ByVal e As Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs) Handles rdpReceiptDate.SelectedDateChanged
            rgReceiptList.DataSource = LoadReceiptList()
            rgReceiptList.Rebind()
        End Sub

        ''ByTine 06-01-2559 ปรับปรุงใช้ใบเสร็จใหม่
        Protected Sub rdpReceiptDate_v2_SelectedDateChanged(ByVal sender As System.Object, ByVal e As Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs) Handles rdpReceiptDate_v2.SelectedDateChanged
            rgReceiptListNew.DataSource = LoadReceiptListNew()
            rgReceiptListNew.Rebind()
        End Sub

        Protected Sub imbPrint_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs)
            If RadioButtonList1.SelectedItem.Value = 0 Then
                Dim id As String = CType(sender.AlternateText, String)
                If id <> "" And Session("ssRoleName") <> "" Then
                    Me.RadAjaxManager1.ResponseScripts.Add("window.open('http://" & DotNetNuke.Common.GetDomainName(Request) & "/DesktopModules/DFT_EDI_PrintReceipt_Dup/frmPrintReceipt_Dup.aspx?bill_no=" & id & "&site=" & Session("ssRoleName") & "&BillType=1" & "',null,'fullscreen=yes,status=yes, resizable= yes,scrollbars=no, toolbar=no,location=no,menubar=no');")
                Else
                    Me.RadAjaxManager1.ResponseScripts.Add("window.alert('ไม่สามารถพิมพ์ใบเสร็จได้');")
                End If

            Else
                'print ตรง
                Dim str_bill As String = CType(sender.AlternateText, String)
                Dim str_siteid As String = Session("ssRoleName")

                'RadAjaxManager1.ResponseScripts.Add("window.alert('print');")
                If str_bill <> "" And str_siteid <> "" Then
                    Dim ds As DataSet
                    ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, "sp_get_receipt_new", _
                    New SqlParameter("@B_NO", str_bill), _
                    New SqlParameter("@SITE_ID", str_siteid))

                    'Dim rpt = New receipt_rpt
                    Dim rpt As Object
                    'Dim IsShortReport As Boolean = IsUseShortReceipt(str_siteid)
                    'If IsShortReport = True Then
                    '    rpt = New rpt_Receipt_Short
                    'Else
                    '    rpt = New receipt_rpt
                    'End If
                    rpt = New receipt_rpt

                    'set ให้สามารถทำได้ทุกสาขาด้วย
                    Dim ISRoleDS As Boolean = UserInfo.IsInRole("EDI_DS")
                    ISRoleDS = False
                    rpt.Document.Printer.PrinterName = PrintReceipt_(str_siteid, CommonUtility.Get_StringValue(rdblReceiptPrinter.SelectedValue), CommonUtility.Get_StringValue(Session("UName")), ISRoleDS) 'ConfigurationManager.AppSettings("printNameCall").ToString()

                    If ds.Tables(0).Rows.Count > 0 Then
                        rpt.DataSource = ds.Tables(0)

                        rpt.Run()
                        rpt.Document.Print(False, False) 'set false เพื่อไม่ให้ขึ้นหน้าเลือกเครื่อง printer
                    End If
                Else
                    RadAjaxManager1.ResponseScripts.Add("window.alert('ไม่สามารถพิมพ์ใบเสร็จได้');")
                End If
            End If
        End Sub

        ''ByTine 06-01-2559 ปรับปรุงใช้ใบเสร็จใหม่
        Protected Sub imbPrint_v2_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs)
            If RadioButtonList2.SelectedItem.Value = 0 Then
                Dim id As String = CType(sender.AlternateText, String)
                If id <> "" And Session("ssRoleName") <> "" Then
                    Me.RadAjaxManager1.ResponseScripts.Add("window.open('http://" & DotNetNuke.Common.GetDomainName(Request) & "/DesktopModules/DFT_EDI_PrintReceipt_Dup/frmPrintReceipt_Dup.aspx?bill_no=" & id & "&site=" & Session("ssRoleName") & "&BillType=2" & "',null,'fullscreen=yes,status=yes, resizable= yes,scrollbars=no, toolbar=no,location=no,menubar=no');")
                Else
                    Me.RadAjaxManager1.ResponseScripts.Add("window.alert('ไม่สามารถพิมพ์ใบเสร็จได้');")
                End If

            Else
                'print ตรง
                Dim str_bill As String = CType(sender.AlternateText, String)
                Dim str_siteid As String = Session("ssRoleName")

                If str_bill <> "" And str_siteid <> "" Then
                    Dim ds As DataSet
                    ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, "sp_get_receipt_new_v2", _
                    New SqlParameter("@B_NO", str_bill), _
                    New SqlParameter("@SITE_ID", str_siteid))

                    'Dim rpt = New rpt_Receipt_Yellow
                    Dim rpt As Object
                    Dim IsShortReport As Boolean = IsUseShortReceipt(str_siteid)
                    If IsShortReport = True Then
                        rpt = New rpt_Receipt_Short
                    Else
                        rpt = New rpt_Receipt_Yellow
                    End If

                    Dim CheckCulture As String = Thread.CurrentThread.CurrentCulture.ToString

                    rpt.lblTime.Text = ds.Tables(0).Rows(0).Item("B_TIME")
                    rpt.lblDate.Text = ds.Tables(0).Rows(0).Item("B_Date")
                    rpt.lblMonth.Text = ThaiMonth(ds.Tables(0).Rows(0).Item("B_Month"))

                    If CheckCulture <> "th-TH" Then
                        rpt.lblYear.Text = ds.Tables(0).Rows(0).Item("B_Year") + 543
                    Else
                        rpt.lblYear.Text = ds.Tables(0).Rows(0).Item("B_Year")
                    End If

                    rpt.txtCompanyName.Text = CommonUtility.Get_StringValue(ds.Tables(0).Rows(0).Item("receipt_name")) & "  (" & CommonUtility.Get_StringValue(ds.Tables(0).Rows(0).Item("company_taxno")) & ")"

                    Dim ISRoleDS As Boolean = UserInfo.IsInRole("EDI_DS")
                    rpt.Document.Printer.PrinterName = PrintReceipt_v2(str_siteid, CommonUtility.Get_StringValue(rdblReceiptPrinter_v2.SelectedValue), ISRoleDS, CommonUtility.Get_StringValue(Session("UName")))

                    If IsShortReport = True Then
                        rpt.Document.Printer.PaperKind = System.Drawing.Printing.PaperKind.Custom
                        Dim p As New System.Drawing.Printing.PaperSize("Custom Paper Size", 825, 550)
                        rpt.Document.Printer.PaperSize = p
                    End If

                    If ds.Tables(0).Rows.Count > 0 Then
                        rpt.DataSource = ds.Tables(0)

                        rpt.Run()
                        rpt.Document.Print(False, False) 'set false เพื่อไม่ให้ขึ้นหน้าเลือกเครื่อง printer
                    End If
                Else
                    RadAjaxManager1.ResponseScripts.Add("window.alert('ไม่สามารถพิมพ์ใบเสร็จได้');")
                End If
            End If
        End Sub

        Private Function IsUseShortReceipt(ByVal SiteID As String) As Boolean
            Dim ret As Boolean = False
            Try
                Dim cmd As String = "select * from receipt_type_setting where site_id='" & SiteID & "' and use_short_receipt = 1"
                Dim ds As DataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(strEDIConn, CommandType.Text, cmd)
                If ds.Tables(0).Rows.Count > 0 Then
                    ret = True
                End If
            Catch ex As Exception

            End Try
            Return ret
        End Function

        Protected Sub btnGroupBy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGroupBy.Click
            Dim expression1 As GridGroupByExpression = GridGroupByExpression.Parse("receipt_by [ผู้ออกใบเสร็จ], sum(amount) Items [จำนวน] Group By receipt_by")
            Me.rgReceiptList.MasterTableView.GroupByExpressions.Add(expression1)

            rgReceiptList.Rebind()
        End Sub

        ''ByTine 07-01-2559 ปรับเรื่องใบเสร็จใหม่
        Protected Sub btnGroupBy_v2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGroupBy_v2.Click
            ''ByTine 06-01-2559 ปรับเรื่องใบเสร็จใหม่
            Dim expression2 As GridGroupByExpression = GridGroupByExpression.Parse("receipt_by [ผู้ออกใบเสร็จ], sum(amount) Items [จำนวน] Group By receipt_by")
            Me.rgReceiptListNew.MasterTableView.GroupByExpressions.Add(expression2)

            rgReceiptListNew.Rebind()
        End Sub

        Private Sub rgReceiptList_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgReceiptList.NeedDataSource
            rgReceiptList.DataSource = LoadReceiptList()
        End Sub

        Private Sub rgReceiptListNew_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgReceiptListNew.NeedDataSource
            rgReceiptListNew.DataSource = LoadReceiptListNew()
        End Sub

        ''ByTine 07-01-2559 ปรับเรื่องใบเสร็จใหม่
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

        Function ThaiMonth(ByVal Month As String) As String
            Try
                Select Case Month
                    Case "01", "1"
                        Month = "มกราคม"
                    Case "02", "2"
                        Month = "กุมภาพันธ์"
                    Case "03", "3"
                        Month = "มีนาคม"
                    Case "04", "4"
                        Month = "เมษายน"
                    Case "05", "5"
                        Month = "พฤษภาคม"
                    Case "06", "6"
                        Month = "มิถุนายน"
                    Case "07", "7"
                        Month = "กรกฏาคม"
                    Case "08", "8"
                        Month = "สิงหาคม"
                    Case "09", "9"
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
    End Class

End Namespace
