Imports System.Web.UI
Imports System.Collections.Generic
Imports System.Reflection

Imports DotNetNuke
Imports DotNetNuke.Services.Exceptions
Imports DotNetNuke.Services.Localization

Imports Microsoft.ApplicationBlocks.Data
Imports DFT.Dotnetnuke.ClassLibrary
Imports System.Data.SqlClient
Imports Telerik.Web.UI

Namespace NTi.Modules.DFT_EDI_Report_12
    Partial Class ViewDFT_EDI_Report_12
        Inherits Entities.Modules.PortalModuleBase
        Dim strConn As String = ConfigurationManager.ConnectionStrings("EDIConnection").ConnectionString
        Dim objConn As SqlConnection = Nothing
        Dim myReader As SqlDataReader = Nothing

        Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            If Not Page.IsPostBack Then
                rdpFromDate.SelectedDate = Today
                rdpToDate.SelectedDate = Today
                trData.Visible = False

                Call LoadSitePlus()
            End If

            'Check ว่า User ที่ Login เข้ามาอยู่ Site ไหน
            'Dim myRoleControllers As New DotNetNuke.Security.Roles.RoleController()
            'Dim i As Integer = 0
            'For i = 0 To MyBase.UserInfo.Roles.Length - 1
            '    Dim myRoleInfo As DotNetNuke.Security.Roles.RoleInfo = myRoleControllers.GetRoleByName(0, MyBase.UserInfo.Roles(i))

            '    Select Case myRoleInfo.RoleID
            '        Case 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19
            '            lblRoleID.Text = myRoleInfo.RoleName
            '            Exit For
            '    End Select
            'Next i
        End Sub

        Private Sub LoadSitePlus()
            Try
                Dim ds As New DataSet
                Dim strCommand As String
                strCommand = " SELECT site_id, site_name FROM site " &
                            "   WHERE site_id in('ST-001','ST-003','ST-004') order by site_id "

                ds = SqlHelper.ExecuteDataset(strConn, CommandType.Text, strCommand)
                If ds.Tables(0).Rows.Count > 0 Then
                    ddlSite.DataSource = ds.Tables(0)
                    ddlSite.DataTextField = "site_name"
                    ddlSite.DataValueField = "site_id"
                    ddlSite.DataBind()

                    ddlSite.SelectedValue = "ST-001"

                    ddlSite.Items.Add(New ListItem("ทั้งหมด", "-1"))

                End If
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End Sub

        Private Sub rgReceiptList_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles rgReceiptList.DataBound
            objConn.Close()

            If rgReceiptList.MasterTableView.Items.Count > 0 Then
                trData.Visible = True
            Else
                trData.Visible = False
                Me.RadAjaxManager1.ResponseScripts.Add("window.alert('ไม่พบข้อมูลตามเงื่อนไขที่ทำการค้นหา');")
            End If
        End Sub

        Private Sub rgReceiptList_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgReceiptList.ItemDataBound
            If Not (e.Item.FindControl("lblIndex") Is Nothing) Then
                Dim lbl As Label = e.Item.FindControl("lblIndex")
                lbl.Text = (rgReceiptList.MasterTableView.CurrentPageIndex * rgReceiptList.MasterTableView.PageSize) + (e.Item.ItemIndex + 1)
            End If
        End Sub

        Private Sub rgReceiptList_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgReceiptList.NeedDataSource
            rgReceiptList.DataSource = LoadData()
        End Sub

        Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
            rgReceiptList.DataSource = LoadData()
            rgReceiptList.DataBind()

            If rgReceiptList.MasterTableView.Items.Count > 0 Then
                trData.Visible = True
            Else
                trData.Visible = False
                Me.RadAjaxManager1.ResponseScripts.Add("window.alert('ไม่พบข้อมูลตามเงื่อนไขที่ทำการค้นหา');")
            End If
        End Sub

        Function LoadData() As DataTable
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

            objConn = New SqlConnection(strConn)

            Dim cmd As String = "SP_KPI_REPORT_NewDS"
            If rdoType.SelectedValue = "ds" Then
                cmd = "SP_KPI_DS_REPORT_NewDS_V1"
                If rdoDSPrintOrCheck.SelectedValue = "checkdoc" Then
                    cmd = "SP_KPI_DS_CHECKDOC_REPORT_NewDS_V2"
                End If
            End If

            'Dim npm(3) As SqlParameter
            'npm(0) = New SqlParameter("@TCAT", rdlReceiptType.SelectedValue)
            'npm(1) = New SqlParameter("@FROM_DATE", FunctionUtility.DMY2YMD(rdpFromDate.SelectedDate.Value))
            'npm(2) = New SqlParameter("@TO_DATE", FunctionUtility.DMY2YMD(rdpToDate.SelectedDate.Value))
            'npm(3) = New SqlParameter("@SITE_ID", CommonUtility.Get_StringValue(ddlSite.SelectedValue))

            Dim npm(4) As SqlParameter
            npm(0) = New SqlParameter("@TCAT", rdlReceiptType.SelectedValue)
            npm(1) = New SqlParameter("@FROM_DATE", FunctionUtility.DMY2YMD(rdpFromDate.SelectedDate.Value))
            npm(2) = New SqlParameter("@TO_DATE", FunctionUtility.DMY2YMD(rdpToDate.SelectedDate.Value))
            npm(3) = New SqlParameter("@SITE_ID", CommonUtility.Get_StringValue(ddlSite.SelectedValue))
            npm(4) = New SqlParameter("@request_type", rblType.SelectedValue)

            Dim ds As New DataSet
            ds = SqlHelper.ExecuteDataset(objConn, CommandType.StoredProcedure, cmd, npm)

            '<DS I Edit 18/08/2011>
            Dim TOTAL_USE_TIME As Integer = 0 'เวลารวมทั้งหมดที่ใช้ในการปฏิบัติงาน (หน่วยเป็นนาที)
            Dim TOTAL_RECORD As Integer = 0 'จำนวนงานทั้งหมด
            Dim MIN_VALUE As Integer = 0 'จำนวนเวลาที่ใช้ในการทำงานที่น้อยที่สุด (หน่วยเป็นนาที)
            Dim MAX_VALUE As Integer = 0 'จำนวนเวลาที่ใช้ในการทำงานมากที่สุด (หน่วยเป็นนาที)
            Dim AVG_VALUE As Integer = 0 'จำนวนเวลาที่ใช้ในการทำงานเฉลี่ย (หน่วยเป็นนาที)

            Dim MAX_TIME As Integer = 1800

            If rdoType.SelectedValue = "ds" Then
                MAX_TIME = 900
            End If

            If rblType.SelectedValue = "A" Then
                If ds.Tables(0).Rows.Count > 0 Then
                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                        Dim aRow As DataRow = table1.NewRow()
                        With ds.Tables(0).Rows(i)
                            Dim date1 As Date = CommonUtility.Get_DateTime(.Item("START_TIME"))
                            Dim date2 As Date = CommonUtility.Get_DateTime(.Item("FINISH_TIME"))

                            Dim TOTAL_MINTUE As Integer = DateDiff(DateInterval.Minute, date1, date2)
                            'เก็บค่าเวลารวมทั้งหมดที่ใช้ในการปฏิบัติงาน
                            TOTAL_USE_TIME = TOTAL_USE_TIME + TOTAL_MINTUE

                            'เก็บค่าจำนวนงานทั้งหมด
                            TOTAL_RECORD = TOTAL_RECORD + CommonUtility.Get_Int32(.Item("FORM_COUNT"))

                            Dim AVG_MINTUE As Integer
                            Dim TOTAL_TIME As String
                            Dim AVG_TIME As String

                            TOTAL_TIME = SecondsToText(DateDiff(DateInterval.Second, date1, date2))
                            AVG_MINTUE = DateDiff(DateInterval.Second, date1, date2) / CommonUtility.Get_Int32(.Item("FORM_COUNT"))
                            AVG_TIME = SecondsToText(AVG_MINTUE)

                            'เก็บค่าจำนวนเวลาที่ใช้ในการทำงานที่น้อยที่สุด และ จำนวนเวลาที่ใช้ในการทำงานมากที่สุด
                            If i = 0 Then
                                MIN_VALUE = AVG_MINTUE
                                MAX_VALUE = AVG_MINTUE
                            Else
                                If AVG_MINTUE < MIN_VALUE Then
                                    MIN_VALUE = AVG_MINTUE
                                End If

                                If AVG_MINTUE > MAX_VALUE Then
                                    MAX_VALUE = AVG_MINTUE
                                End If
                            End If

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

                    'แสดงค่า MAX, MIN, และ AVG ออกทางหน้าจอ
                    lblMinValue.Text = "MIN VALUE : " & SecondsToText(MIN_VALUE)
                    lblMaxValue.Text = "MAX VALUE : " & SecondsToText(MAX_VALUE)
                    lblAvgValue.Text = "AVERAGE VALUE : " & SecondsToText(CommonUtility.Get_Int32((TOTAL_USE_TIME * 60) \ TOTAL_RECORD))

                    Return table1
                End If
            ElseIf rblType.SelectedValue = "W" Then
                If ds.Tables(0).Rows.Count > 0 Then
                    Dim index As Integer = 0
                    Dim _INDEX As Integer = 0
                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                        Dim aRow As DataRow = table1.NewRow()
                        With ds.Tables(0).Rows(i)
                            Dim date1 As Date = CommonUtility.Get_DateTime(.Item("START_TIME"))
                            Dim date2 As Date = CommonUtility.Get_DateTime(.Item("FINISH_TIME"))

                            Dim TOTAL_MINTUE As Integer = DateDiff(DateInterval.Minute, date1, date2)
                            Dim AVG_MINTUE As Integer = DateDiff(DateInterval.Second, date1, date2) / CommonUtility.Get_Int32(.Item("FORM_COUNT"))

                            If AVG_MINTUE < MAX_TIME Then
                                _INDEX = _INDEX + 1
                                'เก็บค่าจำนวนงานทั้งหมด
                                TOTAL_RECORD = TOTAL_RECORD + CommonUtility.Get_Int32(.Item("FORM_COUNT"))

                                'เก็บค่าเวลารวมทั้งหมดที่ใช้ในการปฏิบัติงาน
                                TOTAL_USE_TIME = TOTAL_USE_TIME + TOTAL_MINTUE

                                Dim TOTAL_TIME As String
                                Dim AVG_TIME As String
                                index = index + 1
                                TOTAL_TIME = SecondsToText(DateDiff(DateInterval.Second, date1, date2))
                                'AVG_MINTUE = DateDiff(DateInterval.Second, date1, date2) / CommonUtility.Get_Int32(.Item("FORM_COUNT"))
                                AVG_TIME = SecondsToText(AVG_MINTUE)

                                'เก็บค่าจำนวนเวลาที่ใช้ในการทำงานที่น้อยที่สุด และ จำนวนเวลาที่ใช้ในการทำงานมากที่สุด
                                If _INDEX = 1 Then
                                    MIN_VALUE = AVG_MINTUE
                                    MAX_VALUE = AVG_MINTUE
                                Else
                                    If AVG_MINTUE < MIN_VALUE Then
                                        MIN_VALUE = AVG_MINTUE
                                    End If

                                    If AVG_MINTUE > MAX_VALUE Then
                                        MAX_VALUE = AVG_MINTUE
                                    End If
                                End If

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

                    'แสดงค่า MAX, MIN, และ AVG ออกทางหน้าจอ
                    lblMinValue.Text = "MIN VALUE : " & SecondsToText(MIN_VALUE)
                    lblMaxValue.Text = "MAX VALUE : " & SecondsToText(MAX_VALUE)
                    lblAvgValue.Text = "AVERAGE VALUE : " & SecondsToText(CommonUtility.Get_Int32((TOTAL_USE_TIME * 60) \ TOTAL_RECORD))

                    Return table1
                End If
            End If
        End Function

        Protected Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
            Me.RadAjaxManager1.ResponseScripts.Add("window.open('http://" & DotNetNuke.Common.GetDomainName(Request) & "/DesktopModules/DFT_EDI_Report_12/frmEDI_Report_12.aspx" &
            "?FROM_DATE=" & FunctionUtility.DMY2YMD(rdpFromDate.SelectedDate.Value) &
            "&TO_DATE=" & FunctionUtility.DMY2YMD(rdpToDate.SelectedDate.Value) &
            "&SITE_ID=" & CommonUtility.Get_StringValue(ddlSite.SelectedValue) &
            "&TYPE=" & CommonUtility.Get_StringValue(rblType.SelectedValue) &
            "&rTYPE=" & CommonUtility.Get_StringValue(rdoType.SelectedValue) &
            "&IsPRINT=" & CommonUtility.Get_StringValue(rdoDSPrintOrCheck.SelectedValue) &
            "&ReceiptType=" & CommonUtility.Get_StringValue(rdlReceiptType.SelectedValue) &
            "&request_type=" & CommonUtility.Get_StringValue(rblType.SelectedValue) &
            "',null,'height=600, width=800,status=no, resizable= no,scrollbars=yes, toolbar=no,location=no,menubar=no,center=yes');")
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

        Private Sub rdoType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rdoType.SelectedIndexChanged
            If rdoType.SelectedIndex = 0 Then
                rdoDSPrintOrCheck.Visible = False
            Else
                rdoDSPrintOrCheck.Visible = True
            End If
        End Sub
    End Class

End Namespace
