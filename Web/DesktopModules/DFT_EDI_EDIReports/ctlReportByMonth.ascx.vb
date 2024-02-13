Imports Telerik.Web.UI
Imports DevUtil

Partial Public Class ctlReportByMonth
    Inherits Entities.Modules.PortalModuleBase

    Dim conn As String = ConfigurationManager.ConnectionStrings("EDIConnection").ToString

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            '// Add month name to dropdown
            renderMonthDropdown()
            renderYearDropdown()
            'Response.Write(Convert.ToDateTime("2011/12/05").ToString("yyyy-MM-dd"))
            ddlSelectReport.SelectedValue = Request.QueryString("ctl")
        End If

    End Sub

    Public Sub renderMonthDropdown()
        'Dim _date As New Date(Date.Now.Year, 1, 1)
        'For i As Integer = 0 To 11
        '    Dim _item As New RadComboBoxItem(_date.AddMonths(i).ToString("MMMM", New System.Globalization.CultureInfo("th-Th")), i + 1)
        '    ddlMonth.Items.Add(_item)

        'Next
        'ddlMonth.Items.Insert(0, New RadComboBoxItem("--เลือกเดือน--", 0))

        Dim monthName() As String = {"--เลือกเดือน--", "มกราคม", "กุมภาพันธ์", "มีนาคม", "เมษายน", "พฤษภาคม", "มิถุนายน", "กรกฎาคม", "สิงหาคม", "กันยายน ", "ตุลาคม", "พฤศจิกายน", "ธันวาคม"}
        For i As Integer = 0 To monthName.Length - 1
            Dim _item As New RadComboBoxItem(monthName(i), i)
            ddlMonth.Items.Add(_item)
        Next
    End Sub

    Public Sub renderYearDropdown()
        For i As Integer = 0 To 9
            ddlYear.Items.Add(New RadComboBoxItem((Date.Now.Year - i).ToString, Date.Now.Year - i))
        Next
    End Sub

    Public Sub LoadReport(ByVal mouth As Integer, ByVal _year As String)
        Dim comm As String = "vi_report_edi_ReportByMonth_NewDS"

        Dim _eday As Integer = 0

        Dim _sdate As New Date(_year, mouth, 1)
        _eday = Date.DaysInMonth(_year, mouth)
        Dim _edate As New Date(_year, mouth, _eday)

        Dim prm(2) As SqlClient.SqlParameter
        prm(0) = New SqlClient.SqlParameter("@sdate", SqlDbType.DateTime)
        prm(0).Value = _sdate
        prm(1) = New SqlClient.SqlParameter("@edate", SqlDbType.DateTime)
        prm(1).Value = _edate

        Dim ds As DataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(conn, CommandType.StoredProcedure, comm, prm)

        If ds.Tables(0).Rows.Count > 0 Then

            RadGrid1.Visible = True
            lblMsg.Visible = True
            lblMsg.ForeColor = Drawing.Color.Blue
            lblMsg.Text = "สรุปจำนวนการขอฟอร์ม เดือน" & ddlMonth.SelectedItem.Text & " ปี " & ddlYear.SelectedValue + 543

            Dim form_type As New List(Of String)

            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                form_type.Add(ds.Tables(0).Rows(i).Item("form_name").ToString)
            Next

            form_type = DistinctArray(form_type.ToArray)

            Dim dt As New DataTable("ReportByMonth")
            dt.Columns.Add(New DataColumn("วันที่", GetType(String)))

            For i As Integer = 0 To form_type.Count - 1
                dt.Columns.Add(New DataColumn(form_type(i).ToString, GetType(String)))
            Next

            dt.Columns.Add(New DataColumn("รวม", GetType(String)))

            For i As Integer = 0 To _eday - 1

                Dim dr = dt.NewRow

                dr(0) = i + 1
                dt.Rows.Add(dr)
            Next

            Dim gridRow As Integer = 0
            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                For j As Integer = 0 To dt.Columns.Count - 1
                    If ds.Tables(0).Rows(i).Item("form_name") = dt.Columns(j).ColumnName Then
                        gridRow = Convert.ToInt32(ds.Tables(0).Rows(i).Item("sent_date").ToString.Substring(6, 2)) - 1
                        dt.Rows(gridRow).Item(ds.Tables(0).Rows(i).Item("form_name")) = decimalFormat(ds.Tables(0).Rows(i).Item("nCount"))
                    End If
                Next
            Next

            Dim sumCols(dt.Columns.Count - 2) As Integer
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim sumRow As Integer = 0

                For j As Integer = 1 To dt.Columns.Count - 1
                    sumCols(j - 1) += convertToInt(dt.Rows(i).Item(j).ToString)
                    If j <> dt.Columns.Count - 1 Then
                        sumRow += convertToInt(dt.Rows(i).Item(j).ToString)
                    End If

                    If sumRow <> 0 Then
                        dt.Rows(i).Item(dt.Columns(dt.Columns.Count - 1)) = decimalFormat(sumRow)
                    End If
                Next

            Next

            Dim dr2 = dt.NewRow
            dr2(0) = "<b>รวม</b>"
            For i As Integer = 0 To sumCols.Length - 1
                dr2(i + 1) = "<b>" & decimalFormat(sumCols(i)) & "</b>"
            Next
            dt.Rows.Add(dr2)

            RadGrid1.DataSource = dt
            RadGrid1.DataBind()

            RadGrid1.ItemStyle.HorizontalAlign = HorizontalAlign.Center
            RadGrid1.AlternatingItemStyle.HorizontalAlign = HorizontalAlign.Center
            RadGrid1.HeaderStyle.Font.Bold = True
            RadGrid1.HeaderStyle.HorizontalAlign = HorizontalAlign.Center

            ImageButton1.Visible = True

        Else
            RadGrid1.Visible = False
            lblMsg.Visible = True
            lblMsg.Text = "ไม่พบรายการข้อมูลที่ต้องการแสดง"
            lblMsg.ForeColor = Drawing.Color.Red
            ImageButton1.Visible = False
        End If


    End Sub

    Protected Sub btnViewReport_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnViewReport.Click
        LoadReport(ddlMonth.SelectedValue, ddlYear.SelectedValue)
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