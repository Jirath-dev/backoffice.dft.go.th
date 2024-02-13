
Imports System.Web.UI
Imports System.Collections.Generic
Imports System.Reflection

Imports DotNetNuke
Imports DotNetNuke.Services.Exceptions
Imports DotNetNuke.Services.Localization
Imports Telerik.Web.UI
Imports Microsoft.ApplicationBlocks.Data
Imports System.Data.SqlClient

Namespace DFT_EDI_F04_Report_10

    Partial Class ViewDFT_EDI_F04_Report_10
        Inherits Entities.Modules.PortalModuleBase
        Dim strEDIConn As String = ConfigurationManager.ConnectionStrings("OriginConnection").ConnectionString
        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


            If Not Page.IsPostBack Then
                rdpFromDate.SelectedDate = Today
                rdpToDate.SelectedDate = Today

                getSiteID()
            End If


        End Sub
        Private Function getData() As DataSet
            Dim cmd As String = "sp_get_report_f04_10_V2"
            Dim ds As New DataSet
            Try
                Dim prm(3) As SqlParameter
                prm(0) = New SqlParameter("@FROM_DATE", rdpFromDate.SelectedDate)
                prm(1) = New SqlParameter("@TO_DATE", rdpToDate.SelectedDate)
                prm(2) = New SqlParameter("@SITE_ID", ddlSiteID.SelectedValue)
                prm(3) = New SqlParameter("@USER_TYPE", rdoUserType.SelectedValue)
                ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, cmd, prm)
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
            Return ds
        End Function
        Private Sub rgRequestForm_NeedDataSource(source As Object, e As GridNeedDataSourceEventArgs) Handles rgRequestForm.NeedDataSource
            rgRequestForm.DataSource = getData()
        End Sub

        Public Sub getSiteID()
            Dim cmd As String = "select * from site_plus p where p.active_status = 'Y' and p.site_id not in ('ST-001T','ST-001R') order by site_name asc"
            Try
                Dim ds As New DataSet
                ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.Text, cmd)
                If ds.Tables(0).Rows.Count > 0 Then

                    ddlSiteID.DataSource = ds
                    ddlSiteID.DataTextField = "site_name"
                    ddlSiteID.DataValueField = "site_id"
                    ddlSiteID.DataBind()

                    ddlSiteID.Items.Insert(0, New ListItem("-- กรุณาเลือก --", "-1"))
                End If
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End Sub
        Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click

            If ddlSiteID.SelectedValue = "-1" Then
                lbl_error_msg.Text = "เกิดข้อผิดพลาด กรุณาเลือกสาขา"
                lbl_error_msg.Visible = True
                lbl_error_msg.ForeColor = Drawing.Color.Red
                rgRequestForm.Rebind()
                Exit Sub
            End If

            rgRequestForm.Rebind()
        End Sub

        Private Sub btnExcel_Click(sender As Object, e As EventArgs) Handles btnExcel.Click
            Try
                For Each gi As GridItem In rgRequestForm.MasterTableView.GetItems(GridItemType.GroupHeader)
                    gi.Expanded = True
                Next
                rgRequestForm.ExportSettings.ExportOnlyData = True
                rgRequestForm.ExportSettings.IgnorePaging = True
                rgRequestForm.MasterTableView.ExportToExcel()

            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End Sub

        Private Sub rgRequestForm_GridExporting(source As Object, e As GridExportingArgs) Handles rgRequestForm.GridExporting
            If (e.ExportType = Telerik.Web.UI.ExportType.Excel) Then
                Dim Css As String = "<style> td { border:solid 0.1pt #000000; }</style>"
                e.ExportOutput = e.ExportOutput.Replace("</head>", Css + "</head>")
            End If
        End Sub

        Private Sub btnPDF_Click(sender As Object, e As EventArgs) Handles btnPDF.Click
            Dim FORM_DATE As String = rdpFromDate.SelectedDate.Value.ToString("yyyyMMdd")
            Dim TO_DATE As String = rdpToDate.SelectedDate.Value.ToString("yyyyMMdd")
            Dim SITE_ID As String = ddlSiteID.SelectedValue
            Response.Write("<script language ='javascript'> window.open('http://" & DotNetNuke.Common.GetDomainName(Request) & "/DesktopModules/DFT_EDI_F04_Report_10/frm_report_f04.aspx?FROM_DATE=" & FORM_DATE & "&TO_DATE=" & TO_DATE & "&SITE_ID=" & SITE_ID & "&UTYPE=" & rdoUserType.SelectedValue & "',null,'height=600, width=800,status=yes, resizable= yes,scrollbars=no, toolbar=no,location=no,menubar=no'); </script>")
        End Sub

        Public Function ConvertToThaiDate(obj As Object) As String
            Dim ret As String = ""
            Try
                ret = Convert.ToDateTime(obj).ToString("dd MMMM yyyy H:mm น.", New System.Globalization.CultureInfo("th-TH"))
            Catch ex As Exception
            End Try
            Return ret
        End Function

    End Class

End Namespace
