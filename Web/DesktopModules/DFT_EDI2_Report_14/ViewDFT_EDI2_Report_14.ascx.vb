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

Namespace NTi.Modules.DFT_EDI2_Report_14
    Partial Class ViewDFT_EDI2_Report_14
        Inherits Entities.Modules.PortalModuleBase
        Dim strTradingConn As String = ConfigurationManager.ConnectionStrings("TradingConnection").ConnectionString
        Dim objConn As SqlConnection = Nothing
        Dim myReader As SqlDataReader = Nothing

        Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            If Not Page.IsPostBack Then
                rdpFromDate.SelectedDate = Today
                rdpToDate.SelectedDate = Today
                trData.Visible = False
            End If

            ''Check ว่า User ที่ Login เข้ามาอยู่ Site ไหน
            'Dim myRoleControllers As New DotNetNuke.Security.Roles.RoleController()
            'Dim i As Integer = 0
            'For i = 0 To MyBase.UserInfo.Roles.Length - 1
            '    Dim myRoleInfo As DotNetNuke.Security.Roles.RoleInfo = myRoleControllers.GetRoleByName(0, MyBase.UserInfo.Roles(i))

            '    Select Case myRoleInfo.RoleID
            '        Case 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21
            '            lblRoleID.Text = myRoleInfo.RoleName
            '            Exit For
            '    End Select
            'Next i

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
                '        Exit For
                'End Select
            Next i
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
        Private Sub rgReceiptList_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles rgReceiptList.DataBound
            objConn.Close()

            If rgReceiptList.MasterTableView.Items.Count > 0 Then
                trData.Visible = True
            Else
                trData.Visible = False
                Me.RadAjaxManager1.ResponseScripts.Add("window.alert('ไม่พบข้อมูลตามเงื่อนไขที่ทำการค้นหา');")
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

            objConn = New SqlConnection(strTradingConn)
            Dim ds As New DataSet
            ds = SqlHelper.ExecuteDataset(objConn, CommandType.StoredProcedure, "vi_KPI_REPORT_NewDS", _
            New SqlParameter("@FROM_DATE", FunctionUtility.DMY2YMD(rdpFromDate.SelectedDate.Value)), _
            New SqlParameter("@TO_DATE", FunctionUtility.DMY2YMD(rdpToDate.SelectedDate.Value)), _
            New SqlParameter("@SITE_ID", CommonUtility.Get_StringValue(lblRoleID.Text)), _
            New SqlParameter("@TYPE", CommonUtility.Get_StringValue("A")))

            If rblType.SelectedValue = "A" Then
                If ds.Tables(0).Rows.Count > 0 Then
                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                        Dim aRow As DataRow = table1.NewRow()
                        With ds.Tables(0).Rows(i)
                            Dim date1 As Date = CommonUtility.Get_DateTime(.Item("START_TIME"))
                            Dim date2 As Date = CommonUtility.Get_DateTime(.Item("FINISH_TIME"))

                            Dim TOTAL_MINTUE As Integer = DateDiff(DateInterval.Minute, date1, date2)
                            Dim AVG_MINTUE As Integer

                            Dim TOTAL_TIME As String
                            Dim AVG_TIME As String

                            TOTAL_TIME = SecondsToText(DateDiff(DateInterval.Second, date1, date2))
                            AVG_MINTUE = DateDiff(DateInterval.Second, date1, date2) / CommonUtility.Get_Int32(.Item("FORM_COUNT"))
                            AVG_TIME = SecondsToText(AVG_MINTUE)

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
                    Return table1
                End If
            ElseIf rblType.SelectedValue = "W" Then
                If ds.Tables(0).Rows.Count > 0 Then
                    Dim index As Integer = 0
                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                        Dim aRow As DataRow = table1.NewRow()
                        With ds.Tables(0).Rows(i)
                            Dim date1 As Date = CommonUtility.Get_DateTime(.Item("START_TIME"))
                            Dim date2 As Date = CommonUtility.Get_DateTime(.Item("FINISH_TIME"))

                            Dim TOTAL_MINTUE As Integer = DateDiff(DateInterval.Minute, date1, date2)
                            Dim AVG_MINTUE As Integer

                            If TOTAL_MINTUE < 30 Then
                                Dim TOTAL_TIME As String
                                Dim AVG_TIME As String
                                index = index + 1
                                TOTAL_TIME = SecondsToText(DateDiff(DateInterval.Second, date1, date2))
                                AVG_MINTUE = DateDiff(DateInterval.Second, date1, date2) / CommonUtility.Get_Int32(.Item("FORM_COUNT"))
                                AVG_TIME = SecondsToText(AVG_MINTUE)

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
                    Return table1
                End If
            End If
        End Function

        Protected Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
            Me.RadAjaxManager1.ResponseScripts.Add("window.open('http://" & DotNetNuke.Common.GetDomainName(Request) & "/DesktopModules/DFT_EDI2_Report_14/frmEDI2_Report_14.aspx" & _
            "?FROM_DATE=" & FunctionUtility.DMY2YMD(rdpFromDate.SelectedDate.Value) & _
            "&TO_DATE=" & FunctionUtility.DMY2YMD(rdpToDate.SelectedDate.Value) & _
            "&SITE_ID=" & lblRoleID.Text & _
            "&TYPE=" & CommonUtility.Get_StringValue(rblType.SelectedValue) & _
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
    End Class

End Namespace
