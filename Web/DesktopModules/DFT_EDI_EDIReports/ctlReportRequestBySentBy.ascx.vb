Imports Telerik.Web.UI

Partial Public Class ctlReportRequestBySentBy
    Inherits Entities.Modules.PortalModuleBase

    Dim conn As String = ConfigurationManager.ConnectionStrings("EDIConnection").ToString

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
           ddlSelectReport.SelectedValue = Request.QueryString("ctl")
        End If
    End Sub

    Public Sub LoadReport(ByVal sdate As DateTime, ByVal edate As DateTime)
        Dim comm As String = "vi_report_edi_ReportSentBy_NewDS"

        Dim _sdate = sdate
        Dim _edate = edate

        Dim prm(2) As SqlClient.SqlParameter
        prm(0) = New SqlClient.SqlParameter("@sdate", SqlDbType.DateTime)
        prm(0).Value = _sdate
        prm(1) = New SqlClient.SqlParameter("@edate", SqlDbType.DateTime)
        prm(1).Value = _edate

        Dim ds As DataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(conn, CommandType.StoredProcedure, comm, prm)

        If ds.Tables(0).Rows.Count > 0 Then

            RadGrid1.DataSource = ds
            RadGrid1.DataBind()

            RadGrid1.Visible = True

            'RadGrid1.ItemStyle.HorizontalAlign = HorizontalAlign.Center
            'RadGrid1.AlternatingItemStyle.HorizontalAlign = HorizontalAlign.Center
            RadGrid1.HeaderStyle.Font.Bold = True
            RadGrid1.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
            RadGrid1.FooterStyle.HorizontalAlign = HorizontalAlign.Left

            ImageButton1.Visible = True '//แสดงปุ่มให้ Export

            lblMsg.Visible = True
            lblMsg.Text = "สรุปจำนวนการขอฟอร์มทั้งหมดของทุกบริษัท  </br> ตั้งแต่  " & _
                        _sdate.ToString("d MMMM yyyy", New System.Globalization.CultureInfo("th-Th")) & _
                        " ถึง " & _
                        _edate.ToString("d MMMM yyyy", New System.Globalization.CultureInfo("th-Th")) & _
                        " แยกรูปแบบการขอฟอร์ม"

            lblMsg.ForeColor = Drawing.Color.Blue

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

    Public Function GetDateFormat(ByVal d) As String
        Dim retVal As String = "-"
        Try
            retVal = Convert.ToDateTime(d).ToString("dd/MM/yyyy", New System.Globalization.CultureInfo("th-TH"))
        Catch ex As Exception
            retVal = "-"
        End Try
        Return retVal
    End Function

    Private Sub RadGrid1_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles RadGrid1.ItemDataBound
        If TypeOf e.Item Is GridFooterItem Then
            Dim footerItem As GridFooterItem = e.Item

            'footerItem.Cells(1).Text = ""
            footerItem("SentBy").Controls.Add(New LiteralControl("<div style='color: black; font-weight: bold;text-align:center;'>รวม</div> "))
            footerItem("nCount").Style.Add("text-align", "right")
            'ElseIf TypeOf e.Item Is GridDataItem Then
            'Dim lbl As Label = DirectCast(e.Item.FindControl("lblItemNo"), Label)
            'lbl.Text = e.Item.ItemIndex + 1
        End If
    End Sub

    Private Sub ddlSelectReport_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlSelectReport.SelectedIndexChanged
        Response.Redirect(EditUrl(ddlSelectReport.SelectedValue))
    End Sub

End Class