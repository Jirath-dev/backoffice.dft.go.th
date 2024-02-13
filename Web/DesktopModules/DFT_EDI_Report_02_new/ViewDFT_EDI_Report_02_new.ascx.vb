
Imports System.Web.UI
Imports System.Collections.Generic
Imports System.Reflection
Imports DotNetNuke
Imports DotNetNuke.Services.Exceptions
Imports DotNetNuke.Services.Localization
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports Telerik.Web.UI

Namespace YourCompany.Modules.DFT_EDI_Report_02_new


    Partial Class ViewDFT_EDI_Report_02_new
        Inherits Entities.Modules.PortalModuleBase

        Dim strConn As String = ConfigurationManager.ConnectionStrings("OriginConnection").ConnectionString
        Dim strTrading As String = ConfigurationManager.ConnectionStrings("tmpCardConn").ConnectionString
        Dim strRegisConn As String = ConfigurationManager.ConnectionStrings("RegisBackConnection").ConnectionString
        Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
            If Not Page.IsPostBack Then
                rdpFromDate.SelectedDate = Today
                rdpToDate.SelectedDate = Today

                Call LoadAllSite()
                'ddlSiteID.SelectedValue = ("ST-001")
                ddlSiteID.Items.FindItemByValue("-1").Selected = True

            End If


        End Sub

        Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click



            '��������������
            If rblReceiptType.SelectedValue = 0 Then
                '��Դ�ͧ Report
                Select Case rblReportType.SelectedValue
                    Case 0
                        rgReceiptSummary.Rebind()

                        If rgReceiptSummary.MasterTableView.Items.Count > 0 Then
                            btnExcel.Visible = True
                            rgReceiptSummary.Visible = True
                            lbltotalRecipt_c.Visible = False
                            lbltotalRecipt_z.Visible = False
                        Else
                            btnExcel.Visible = False
                            rgReceiptSummary.Visible = False
                        End If
                    Case 1
                        rgReceiptDetailSummary.Rebind()

                        If rgReceiptDetailSummary.MasterTableView.Items.Count > 0 Then
                            btnExcel.Visible = True
                            rgReceiptDetailSummary.Visible = True
                            lbltotalRecipt_c.Visible = False
                            lbltotalRecipt_z.Visible = False
                        Else
                            btnExcel.Visible = False
                            rgReceiptDetailSummary.Visible = False
                        End If



                End Select

                '��������������ͧ
            Else

                '��Դ�ͧ Report
                Select Case rblReportType.SelectedValue

                    Case 0
                        rgReceiptSummary.Rebind()
                        If rgReceiptSummary.MasterTableView.Items.Count > 0 Then
                            btnExcel.Visible = True
                            rgReceiptSummary.Visible = True
                            lbltotalRecipt_c.Visible = False
                            lbltotalRecipt_z.Visible = False
                        Else
                            btnExcel.Visible = False
                            rgReceiptSummary.Visible = False

                        End If
                    Case 1
                        rgReceiptDetailSummary.Rebind()
                        If rgReceiptDetailSummary.MasterTableView.Items.Count > 0 Then
                            btnExcel.Visible = True
                            rgReceiptDetailSummary.Visible = True
                            lbltotalRecipt_c.Visible = False
                            lbltotalRecipt_z.Visible = False
                        Else
                            btnExcel.Visible = False
                            rgReceiptDetailSummary.Visible = False


                        End If
                        '===================== ��� ��� ���͡��Դ�ͧ Report ��� �ӡ�ä��Ңͧ �к� �ѵ�
                    Case Else
                        rgReportList.Rebind()
                        If rgReportList.MasterTableView.Items.Count > 0 Then
                            btnExcel.Visible = True
                            rgReportList.Visible = True
                        Else
                            btnExcel.Visible = False
                            rgReportList.Visible = False
                        End If

                      
                End Select

            End If

            ' Call LoadAllSite()
        End Sub
        '�к���� �ѵ� / �ժ���
        Private Function rgReportListItems() As DataSet
            Dim ds As New DataSet
            Dim cmd As String = "sp_ReceiptReportListAll_ReportF04"

            Try

                ds = SqlHelper.ExecuteDataset(strRegisConn, CommandType.StoredProcedure, cmd, _
                  New SqlParameter("@FROM_DATE", rdpFromDate.SelectedDate.Value.ToString("yyyyMMdd")), _
                  New SqlParameter("@TO_DATE", rdpToDate.SelectedDate.Value.ToString("yyyyMMdd")), _
                  New SqlParameter("@Site_ID", ddlSiteID.SelectedValue))

                If ds.Tables(0).Rows.Count > 0 Then

                    If ConvetToString(ds.Tables(0).Rows(0).Item("CountReceipt_C")) <> "" Then
                        lbltotalRecipt_c.Visible = True
                        lbltotalRecipt_c.Text = "�ӹǹ����稷��¡��ԡ :" + "  " + ConvetToString(ds.Tables(0).Rows(0).Item("CountReceipt_C")) + "  " + "��Ѻ"
                        lbltotalRecipt_z.Visible = True
                        lbltotalRecipt_z.Text = "�ӹǹ����稷����ҹ :" + "  " + ConvetToString(ds.Tables(0).Rows(0).Item("CountReceipt_Z")) + "  " + "��Ѻ"


                    End If
                    
                End If
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
            Return ds

        End Function

        '��ػ�ʹ����� �¡�������� ����� ����
        Function LoadReceiptSummary() As DataSet
            Dim ds As New DataSet
            Dim type As String = ""
            Dim Conn As String = ""
            Try
                If rblSystemType.SelectedValue = 0 Then
                    type = "sp_report_5_NewDS"
                    Conn = strConn
                Else
                    type = "sp_report_5_edi2_NewDS"
                    Conn = strTrading
                End If

                ds = SqlHelper.ExecuteDataset(Conn, CommandType.StoredProcedure, type, _
                New SqlParameter("@FROM_DATE", rdpFromDate.SelectedDate.Value.ToString("yyyyMMdd")), _
                New SqlParameter("@TO_DATE", rdpToDate.SelectedDate.Value.ToString("yyyyMMdd")), _
                New SqlParameter("@SITE_ID", ddlSiteID.SelectedValue))


            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
            Return ds
        End Function

        ' ��ػ�ʹ����� �¡�������� ����� ����ͧ
        Function LoadReceiptSummary_v2() As DataSet
            Dim ds As New DataSet
            Dim type As String = ""
            Dim Conn As String = ""
            Try
                If rblSystemType.SelectedValue = 0 Then
                    type = "sp_report_5_NewDS_v2"
                    Conn = strConn
                Else
                    type = "sp_report_5_edi2_NewDS_V2"
                    Conn = strTrading
                End If

                ds = SqlHelper.ExecuteDataset(Conn, CommandType.StoredProcedure, type, _
                New SqlParameter("@FROM_DATE", rdpFromDate.SelectedDate.Value.ToString("yyyyMMdd")), _
                New SqlParameter("@TO_DATE", rdpToDate.SelectedDate.Value.ToString("yyyyMMdd")), _
                New SqlParameter("@SITE_ID", ddlSiteID.SelectedValue))


            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
            Return ds
        End Function

        '��ػ��������´����͡������Ѻ�Թ ���������
        Function LoadReceiptDetailSUmmary() As DataSet
            Dim ds As New DataSet
            Dim type As String = ""
            Dim Conn As String = ""
            Try
                If rblSystemType.SelectedValue = 0 Then
                    type = "sp_report_4_NewDS"
                    Conn = strConn
                Else
                    type = "sp_report_4_edi2_NewDS"
                    Conn = strTrading
                End If

                ds = SqlHelper.ExecuteDataset(Conn, CommandType.StoredProcedure, type, _
                New SqlParameter("@FROM_DATE", rdpFromDate.SelectedDate.Value.ToString("yyyyMMdd")), _
                New SqlParameter("@TO_DATE", rdpToDate.SelectedDate.Value.ToString("yyyyMMdd")), _
                New SqlParameter("@SITE_ID", ddlSiteID.SelectedValue))


            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
            Return ds
        End Function

        '��ػ��������´����͡������Ѻ�Թ ���������ͧ
        Function LoadReceiptDetailSUmmary_v2() As DataSet
            Dim ds As New DataSet
            Dim type As String = ""
            Dim Conn As String = ""
            Try

                If rblSystemType.SelectedValue = 0 Then
                    type = "sp_report_4_NewDS_v2"
                    Conn = strConn
                Else
                    type = "sp_report_4_edi2_NewDS_v2"
                    Conn = strTrading
                End If

                ds = SqlHelper.ExecuteDataset(Conn, CommandType.StoredProcedure, type, _
                New SqlParameter("@FROM_DATE", rdpFromDate.SelectedDate.Value.ToString("yyyyMMdd")), _
                New SqlParameter("@TO_DATE", rdpToDate.SelectedDate.Value.ToString("yyyyMMdd")), _
                New SqlParameter("@SITE_ID", ddlSiteID.SelectedValue))


            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
            Return ds
        End Function

        Public Sub LoadAllSite()
            Try
                Dim Strcommand As String

                ' ddlSiteID.Items.Clear()

                Strcommand = " SELECT site_id, site_name FROM site_plus WHERE        (active_status = 'Y') ORDER BY site_name Asc "
                Dim DS As DataSet = SqlHelper.ExecuteDataset(strConn, CommandType.Text, Strcommand)


                ddlSiteID.DataSource = DS
                ddlSiteID.DataTextField = "site_name"
                ddlSiteID.DataValueField = "site_id"
                ddlSiteID.DataBind()

                ddlSiteID.Items.Insert(0, New RadComboBoxItem("-- ������ --", "-1"))
                ddlSiteID.SelectedIndex = 0


                ' ddlSiteID.SelectedValue = "ST-001"
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End Sub
        Public Function ConvertToString(obj As String) As String
            Dim result As String = ""
            Try
                result = Convert.ToString(obj)

            Catch ex As Exception

            End Try
            Return result
        End Function
        Function ckeck_total(ByVal By_form As String, ByVal By_num As String, ByVal NUM As String) As String
            Dim temp_str As String = ""
            If rblSystemType.SelectedValue = 0 Then
                Select Case By_form.ToUpper
                    Case "FORM2_1"
                        temp_str = CStr(CInt(By_num) / 60)
                    Case Else
                        temp_str = CStr(CInt(By_num) / 30)
                End Select
            Else
                temp_str = CStr(NUM)
            End If

            Return temp_str
        End Function

        'Private Sub CheckAllSite_CheckedChanged(sender As Object, e As EventArgs) Handles CheckAllSite.CheckedChanged
        '    If CheckAllSite.Checked = True Then
        '        ddlSiteID.Enabled = False
        '    Else
        '        ddlSiteID.Enabled = True
        '    End If
        'End Sub

        Protected Sub btnExcel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnExcel.Click

            If rblSystemType.SelectedValue = "0" Then  'rblReportType
                'ǹ㹡Ե ���� �������ѹ ���µ͹��� export �͡��
                If rblReportType.SelectedValue = "0" Then

                    For Each gi As GridItem In rgReceiptSummary.MasterTableView.GetItems(GridItemType.GroupHeader)
                        gi.Expanded = True
                    Next

                    'rgEDI.MasterTableView.HierarchyDefaultExpanded = True

                    '������ export �͡�� excel ��Ѻ
                    rgReceiptSummary.ExportSettings.ExportOnlyData = True
                    rgReceiptSummary.ExportSettings.IgnorePaging = True
                    rgReceiptSummary.MasterTableView.ExportToExcel()
                Else

                    For Each gi As GridItem In rgReceiptDetailSummary.MasterTableView.GetItems(GridItemType.GroupHeader)
                        gi.Expanded = True
                    Next
                    'RgEdiInput.MasterTableView.HierarchyDefaultExpanded = True
                    rgReceiptDetailSummary.ExportSettings.ExportOnlyData = True
                    rgReceiptDetailSummary.ExportSettings.IgnorePaging = True
                    rgReceiptDetailSummary.MasterTableView.ExportToExcel()


                End If


            ElseIf rblSystemType.SelectedValue = "1" Then

                If rblReportType.SelectedValue = "0" Then

                    For Each gi As GridItem In rgReceiptSummary.MasterTableView.GetItems(GridItemType.GroupHeader)
                        gi.Expanded = True
                    Next

                    'rgEDI.MasterTableView.HierarchyDefaultExpanded = True

                    '������ export �͡�� excel ��Ѻ
                    rgReceiptSummary.ExportSettings.ExportOnlyData = True
                    rgReceiptSummary.ExportSettings.IgnorePaging = True
                    rgReceiptSummary.MasterTableView.ExportToExcel()
                Else

                    For Each gi As GridItem In rgReceiptDetailSummary.MasterTableView.GetItems(GridItemType.GroupHeader)
                        gi.Expanded = True
                    Next
                    'RgEdiInput.MasterTableView.HierarchyDefaultExpanded = True
                    rgReceiptDetailSummary.ExportSettings.ExportOnlyData = True
                    rgReceiptDetailSummary.ExportSettings.IgnorePaging = True
                    rgReceiptDetailSummary.MasterTableView.ExportToExcel()


                End If

            ElseIf rblSystemType.SelectedValue = "2" Then

                For Each gi As GridItem In rgReportList.MasterTableView.GetItems(GridItemType.GroupHeader)
                    gi.Expanded = True
                Next
                'RgEdiInput.MasterTableView.HierarchyDefaultExpanded = True
                rgReportList.ExportSettings.ExportOnlyData = True
                rgReportList.ExportSettings.IgnorePaging = True
                rgReportList.MasterTableView.ExportToExcel()

            End If




        End Sub

        Private Sub rgReceiptSummary_GridExporting(source As Object, e As GridExportingArgs) Handles rgReceiptSummary.GridExporting
            If (e.ExportType = Telerik.Web.UI.ExportType.Excel) Then
                Dim Css As String = "<style> td { border:solid 0.1pt #000000; }</style>"
                e.ExportOutput = e.ExportOutput.Replace("</head>", Css + "</head>")
            End If
        End Sub

        Private Sub rgReceiptDetailSummary_GridExporting(source As Object, e As GridExportingArgs) Handles rgReceiptDetailSummary.GridExporting
            If (e.ExportType = Telerik.Web.UI.ExportType.Excel) Then
                Dim Css As String = "<style> td { border:solid 0.1pt #000000; }</style>"
                e.ExportOutput = e.ExportOutput.Replace("</head>", Css + "</head>")
            End If
        End Sub

        Private Sub rgReportList_GridExporting(source As Object, e As GridExportingArgs) Handles rgReportList.GridExporting
            If (e.ExportType = Telerik.Web.UI.ExportType.Excel) Then
                Dim Css As String = "<style> td { border:solid 0.1pt #000000; }</style>"
                e.ExportOutput = e.ExportOutput.Replace("</head>", Css + "</head>")
            End If
        End Sub
        Private Sub rgReceiptSummary_NeedDataSource(source As Object, e As GridNeedDataSourceEventArgs) Handles rgReceiptSummary.NeedDataSource
            If rblReceiptType.SelectedValue = 0 Then
                If rblReportType.SelectedValue = 0 Then

                    trType3.Visible = False
                    trType2.Visible = False
                    trType1.Visible = True

                    rgReceiptSummary.DataSource = LoadReceiptSummary()

                End If
            Else
                If rblReportType.SelectedValue = 0 Then
                    trType3.Visible = False
                    trType2.Visible = False
                    trType1.Visible = True

                    rgReceiptSummary.DataSource = LoadReceiptSummary_v2()

                End If

            End If
        End Sub

        Private Sub rgReceiptDetailSummary_NeedDataSource(source As Object, e As GridNeedDataSourceEventArgs) Handles rgReceiptDetailSummary.NeedDataSource
            If rblReceiptType.SelectedValue = 0 Then
                If rblReportType.SelectedValue = 1 Then
                    trType1.Visible = False
                    trType3.Visible = False
                    trType2.Visible = True

                    rgReceiptDetailSummary.DataSource = LoadReceiptDetailSUmmary()
                 
                   
                End If
            Else
                If rblReportType.SelectedValue = 1 Then

                    trType1.Visible = False
                    trType3.Visible = False
                    trType2.Visible = True
                    rgReceiptDetailSummary.DataSource = LoadReceiptDetailSUmmary_v2()

                   
                End If
            End If
        End Sub

       


        Private Sub rgReportList_NeedDataSource(source As Object, e As GridNeedDataSourceEventArgs) Handles rgReportList.NeedDataSource
            If rblSystemType.SelectedValue = 2 Then
                trType1.Visible = False
                trType2.Visible = False
                trType3.Visible = True
                rgReportList.DataSource = rgReportListItems()
            End If

        End Sub

        Private Sub rblSystemType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rblSystemType.SelectedIndexChanged

            If rblSystemType.SelectedValue = "2" Then

                rblReceiptType.Enabled = False
                rblReceiptType.SelectedValue = 1
                rblReportType.ClearSelection()
                rblReportType.Enabled = False
            Else

                rblReceiptType.Enabled = True

                rblReportType.Enabled = True


            End If

        End Sub

        Public Function ConvetToString(obj As Object) As String
            Dim ret As String = ""
            Try
                ret = Convert.ToString(obj)
            Catch ex As Exception

            End Try
            Return ret
        End Function

    End Class

End Namespace
