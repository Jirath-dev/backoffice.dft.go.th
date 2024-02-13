Imports DevUtil
Imports System.Collections.Generic

Partial Public Class ctlReportSumaryMonth
    Inherits Entities.Modules.PortalModuleBase

    Dim conn As String = ConfigurationManager.ConnectionStrings("EDIConnection").ToString

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            ddlSelectReport.SelectedValue = Request.QueryString("ctl")
        End If
    End Sub

    Public Sub LoadReport(ByVal sdate As DateTime, ByVal edate As DateTime)
        Dim comm As String = "vi_report_edi_ReportSumaryMonth_NewDS"

        Dim _sdate = sdate
        Dim _edate = edate

        Dim prm(2) As SqlClient.SqlParameter
        prm(0) = New SqlClient.SqlParameter("@sdate", SqlDbType.DateTime)
        prm(0).Value = _sdate
        prm(1) = New SqlClient.SqlParameter("@edate", SqlDbType.DateTime)
        prm(1).Value = _edate

        Dim ds As DataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(conn, CommandType.StoredProcedure, comm, prm)

        If ds.Tables(0).Rows.Count > 0 Then

            Dim form_type As New List(Of String) '//สำหรับเก็บประเภทของฟอร์ม
            Dim month As New List(Of String) '//สำหรับเก็บรายการเดือน

            '//เอาค่าเดือนที่ซ้ำๆ ในข้อมูลที่ Query มาได้  ใส่มใน Array
            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                form_type.Add(ds.Tables(0).Rows(i).Item("form_name").ToString)
            Next

            '//หาผลต่างของเดือน
            Dim mont_diff As Integer = DateDiff(DateInterval.Month, _sdate, _edate) + 1

            For i As Integer = 0 To mont_diff - 1
                month.Add(_sdate.AddMonths(i).ToString("MM/dd/yyyy"))
            Next

            '//ตัดค่าที่ซ้ำๆใน Array ให้เหลืออย่างละค่าเดียว
            form_type = DistinctArray(form_type.ToArray)
            'month = DistinctArray(month.ToArray)

            Dim dt As New DataTable("ReportByMonth")
            dt.Columns.Add(New DataColumn("เดือน", GetType(String)))

            '//สร้าง Collumn ตามประเภทฟอร์ม
            For i As Integer = 0 To form_type.Count - 1
                dt.Columns.Add(New DataColumn(form_type(i).ToString, GetType(String)))
            Next

            '//เพิ่ม Collumn ผลรวมที่ Collumn ขวาสุด
            dt.Columns.Add(New DataColumn("รวม", GetType(String)))

            '//วนลูปสร้าง Record ตามเดือนที่ระบุในการค้นหา จากเดือนเริ่มต้น ถึงสิ้นสุด
            For i As Integer = 0 To month.Count - 1

                Dim dr = dt.NewRow

                Dim str_date As String = month(i)
                Dim _date As Date = Convert.ToDateTime(str_date)

                dr(0) = _date.ToString("MMMM yyyy", New System.Globalization.CultureInfo("th-Th"))
                dt.Rows.Add(dr)
            Next

            '// ใส่ค่าให้ DataTable ในตำแหน่ง Row และ Collumn ตามรูปแบบ
            '//มี สาม Loop i,j,k ไล่ให้ดีๆ
            Dim gridRow As Integer = 0
            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1 '//วนทุกรายการ ข้อมูลมาจก การ Query (dataset)

                Dim temp_str_date As String = ds.Tables(0).Rows(i).Item("_month") & "/1/" & ds.Tables(0).Rows(i).Item("_year")
                Dim temp_date As String = Convert.ToDateTime(temp_str_date).ToString("MMMM yyyy", New System.Globalization.CultureInfo("th-Th"))

                For k As Integer = 0 To dt.Rows.Count - 1 '//วน เพื่อหาตำแหน่งของ Row ใน DataTable

                    If dt.Rows(k).Item(0).ToString = temp_date Then '//เปรียบเทียบ เดือนใน DataTable กับ DataSet

                        gridRow = k '//ตำแหน่ง Row = k

                        For j As Integer = 0 To dt.Columns.Count - 1 '//วนเพื่อหาตำแหน่งของ Collumn
                            If ds.Tables(0).Rows(i).Item("form_name") = dt.Columns(j).ColumnName Then '//เปรียบเทียบ หัว Collume กับ ข้อมูลในแต่ละรายการ
                                '//เพื่อหาตำแหน่งของการใส่ข้อมูล
                                dt.Rows(gridRow).Item(ds.Tables(0).Rows(i).Item("form_name")) = decimalFormat(ds.Tables(0).Rows(i).Item("nCount"))
                            End If
                        Next

                    End If
                Next

            Next
            '//=================================================================

            '//หาค่า sumary ทั้งด้านข้าง และ footer  ====================================
            Dim sumCols(dt.Columns.Count - 2) As Integer
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim sumRow As Integer = 0

                For j As Integer = 1 To dt.Columns.Count - 1
                    sumCols(j - 1) += convertToInt(dt.Rows(i).Item(j).ToString) '//Sum ในแต่ละ Collumn
                    If j <> dt.Columns.Count - 1 Then '//ไม่เอาค่า Collumn สุดท้ายของแต่ละ Row มารวม
                        sumRow += convertToInt(dt.Rows(i).Item(j).ToString) '//Sum ในแต่ละ Row
                    End If

                    If sumRow <> 0 Then
                        dt.Rows(i).Item(dt.Columns(dt.Columns.Count - 1)) = decimalFormat(sumRow)
                    End If
                Next
            Next
            '//==================================================================

            '//เพิ่ม Row ให้เสมือนเป็น Footer ===================
            Dim dr2 = dt.NewRow
            dr2(0) = "<b>รวม</b>"
            For i As Integer = 0 To sumCols.Length - 1
                dr2(i + 1) = "<b>" & decimalFormat(sumCols(i)) & "</b>"
            Next
            dt.Rows.Add(dr2)
            '//===========================================

            '//Bind Grid==================================
            RadGrid1.Visible = True
            lblMsg.Visible = True
            lblMsg.ForeColor = Drawing.Color.Blue
            lblMsg.Text = "สรุปจำนวนการขอฟอร์มทั้งหมด ตั้งแต่ " & _
                        _sdate.ToString("d MMMM yyyy", New System.Globalization.CultureInfo("th-Th")) & _
                        " ถึง " & _
                        _edate.ToString("d MMMM yyyy", New System.Globalization.CultureInfo("th-Th")) & _
                        " แยกตามเดือน"

            RadGrid1.DataSource = dt
            RadGrid1.DataBind()

            RadGrid1.ItemStyle.HorizontalAlign = HorizontalAlign.Center
            RadGrid1.AlternatingItemStyle.HorizontalAlign = HorizontalAlign.Center
            RadGrid1.HeaderStyle.Font.Bold = True
            RadGrid1.HeaderStyle.HorizontalAlign = HorizontalAlign.Center

            ImageButton1.Visible = True '//แสดงปุ่มให้ Export

            '// ===============================================

        Else
            RadGrid1.Visible = False
            lblMsg.Visible = True
            lblMsg.Text = "ไม่พบรายการข้อมูลที่ต้องการแสดง"
            lblMsg.ForeColor = Drawing.Color.Red

            ImageButton1.Visible = False '//ปิดปุ่ม Export

        End If

    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
        LoadReport(rdpFromDate.SelectedDate, rdpToDate.SelectedDate)
    End Sub

    Protected Sub ImageButton1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click
        RadGrid1.ExportSettings.ExportOnlyData = True
        RadGrid1.ExportSettings.OpenInNewWindow = True

        RadGrid1.MasterTableView.GridLines = GridLines.Both

        RadGrid1.MasterTableView.Font.Name = "Tahoma"
        RadGrid1.MasterTableView.Font.Size = FontUnit.Point(10)
        RadGrid1.MasterTableView.ExportToExcel()
    End Sub

    Private Sub ddlSelectReport_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlSelectReport.SelectedIndexChanged
        Response.Redirect(EditUrl(ddlSelectReport.SelectedValue))
    End Sub

End Class