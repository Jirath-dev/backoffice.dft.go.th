Imports Telerik.Web.UI

Partial Public Class ctlReportCheckBySentBy
    Inherits Entities.Modules.PortalModuleBase

    Dim conn As String = ConfigurationManager.ConnectionStrings("EDIConnection").ToString

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            LoadReport()
            ddlSelectReport.SelectedValue = Request.QueryString("ctl")
        End If
    End Sub

    Public Sub LoadReport()
        Dim comm As String = "vi_report_edi_ReportCheckBySentBy_NewDS"

        Dim ds As DataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(conn, CommandType.StoredProcedure, comm)

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
            lblMsg.Text = "สรุปจำนวนฟอร์มทั้งหมดของทุกบริษัท ที่ผ่านและไม่ผ่านการตรวจสอบโดยเจ้าหน้าที่"
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
            footerItem("sent_by").Controls.Add(New LiteralControl("<div style='color: black; font-weight: bold;text-align:center;'>รวม</div> "))
            footerItem("total").Style.Add("text-align", "right")
            footerItem("total_err").Style.Add("text-align", "right")
            footerItem("total_ok").Style.Add("text-align", "right")
        End If
    End Sub

    Private Sub ddlSelectReport_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlSelectReport.SelectedIndexChanged
        Response.Redirect(EditUrl(ddlSelectReport.SelectedValue))
    End Sub

End Class