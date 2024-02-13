
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports Telerik.Web.UI

Namespace YourCompany.Modules.DFT_EDI_Report_15

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' The ViewDFT_EDI_Report_15 class displays the content
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Partial Class ViewDFT_EDI_Report_15
        Inherits Entities.Modules.PortalModuleBase
        Dim strConn As String = ConfigurationManager.ConnectionStrings("OriginConnection").ConnectionString
        Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
            If Not Page.IsPostBack Then
                rdpFromDate.SelectedDate = Today
                rdpToDate.SelectedDate = Today
                Call LoadAllSite()
                'ddlSiteID.Items.FindItemByValue("ST-001").Selected = True
            End If

        End Sub

        Public Sub LoadAllSite()

            Try
                Dim Strcommand As String
                Strcommand = " SELECT site_id, site_name FROM site_plus WHERE        (active_status = 'Y') ORDER BY site_name Asc "
                Dim DS As DataSet = SqlHelper.ExecuteDataset(strConn, CommandType.Text, Strcommand)
                Dim item As New RadComboBoxItem("--ทั้งหมด--", "-1")
                ddlSiteID.Items.Add(item)

                For i As Integer = 0 To DS.Tables(0).Rows.Count - 1
                    item = New RadComboBoxItem(ConvertToString(DS.Tables(0).Rows(i).Item("site_name")), ConvertToString(DS.Tables(0).Rows(i).Item("site_id")))
                    ddlSiteID.Items.Add(item)
                Next

                ddlSiteID.SelectedIndex = 0


                'ddlSiteID.DataSource = DS
                'ddlSiteID.DataTextField = "site_name"
                'ddlSiteID.DataValueField = "site_id"
                'ddlSiteID.DataBind()

            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End Sub
        Private Function ConvertToString(obj As Object) As String
            Dim result As String = ""
            Try
                result = Convert.ToString(obj)

            Catch ex As Exception

            End Try
            Return result
        End Function

        Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click

            rgCetificateList.Rebind()


        End Sub

        Private Function rgReportListItems() As DataSet
            Dim cmd As String = "sp_report_EDI_DS_15_NewDS"
            Dim ds As New DataSet
            Try
                ds = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, cmd,
                New SqlParameter("@FROM_DATE", rdpFromDate.SelectedDate.Value.ToString("yyyyMMdd")),
                New SqlParameter("@TO_DATE", rdpToDate.SelectedDate.Value.ToString("yyyyMMdd")),
                New SqlParameter("@SENT_BY", rblType.SelectedValue),
                New SqlParameter("@SITE_ID", ddlSiteID.SelectedValue))

            Catch ex As Exception

            End Try
            Return ds
        End Function

        Private Sub rgCetificateList_NeedDataSource(source As Object, e As GridNeedDataSourceEventArgs) Handles rgCetificateList.NeedDataSource

            rgCetificateList.DataSource = rgReportListItems()
        End Sub

        Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
            Response.Write("<script language ='javascript'> window.open('/DesktopModules/DFT_EDI_Report_15/frmEDI_Report_15.aspx?SentBy=" & rblType.SelectedValue & "&FROM_DATE=" & rdpFromDate.SelectedDate.Value.ToString("yyyyMMdd") & "&TO_DATE=" & rdpToDate.SelectedDate.Value.ToString("yyyyMMdd") & "&SITE_ID=" & ddlSiteID.SelectedValue & "',null,'height=600, width=800,status=yes, resizable= yes,scrollbars=no, toolbar=no,location=no,menubar=no'); </script>")
        End Sub

        Private Sub rgCetificateList_ItemDataBound(sender As Object, e As GridItemEventArgs) Handles rgCetificateList.ItemDataBound

            If rgCetificateList.MasterTableView.Items.Count > 0 Then
                btnPrint.Visible = True
            Else
                btnPrint.Visible = False
            End If
        End Sub
    End Class

End Namespace
