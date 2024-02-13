Imports System.Web.UI
Imports System.Collections.Generic
Imports System.Reflection

Imports DotNetNuke
Imports DotNetNuke.Services.Exceptions
Imports DotNetNuke.Services.Localization
Imports System.Data
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports DFT.Dotnetnuke.ClassLibrary
Imports Telerik.Web.UI


Namespace YourCompany.Modules.DFT_EDI_Report_Chapter
    Partial Class ViewDFT_EDI_Report_Chapter
        Inherits Entities.Modules.PortalModuleBase
        Dim StrConn As String = ConfigurationManager.ConnectionStrings("OriginConnection").ConnectionString
        Dim myconn As SqlConnection = Nothing
        Dim dr As SqlDataReader = Nothing

        Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            If Not Page.IsPostBack Then
                ddlMonthFrom.SelectedValue = "1"
                ddlMonthTo.SelectedValue = "1"
            End If
        End Sub

        Function LoadAll() As SqlDataReader
            Try
                myconn = New SqlConnection(StrConn)
                Dim StrSQL As String = "SELECT     CHAPTER, YEAR, " &
                         "CASE WHEN MONTH = '1' THEN 'มกราคม' " &
                         "WHEN MONTH = '2' THEN 'กุมภาพันธ์'  " &
                         "WHEN MONTH = '3' THEN 'มีนาคม'  " &
                         "WHEN MONTH = '4' THEN 'เมษายน'  " &
                         "WHEN MONTH = '5' THEN 'พฤษภาคม'  " &
                         "WHEN MONTH = '6' THEN 'มิถุนายน'  " &
                         "WHEN MONTH = '7' THEN 'กรกฎาคม'  " &
                         "WHEN MONTH = '8' THEN 'สิงหาคม'  " &
                         "WHEN MONTH = '9' THEN 'กันยายน'  " &
                         "WHEN MONTH = '10' THEN 'ตุลาคม'  " &
                         "WHEN MONTH = '11' THEN 'พฤศจิกายน'  " &
                         "WHEN MONTH = '12' THEN 'ธันวาคม'  " &
                         "END AS MONTH,  " &
                        "FORM_NAME, SUM(FORM_COUNT)  AS FORM_COUNT, SUM(FOB) AS FOB, COUNTRY FROM SSIS_REPORT_BY_CHAPTER " &
                        "GROUP BY CHAPTER, YEAR, MONTH, FORM_NAME, COUNTRY " &
                        "HAVING      (CHAPTER = '" & txtChapter.Text & "') AND (YEAR = '" & txtYear.Text & "') AND (MONTH BETWEEN '" & ddlMonthFrom.SelectedValue & "' AND '" & ddlMonthTo.SelectedValue & "')  "
                dr = SqlHelper.ExecuteReader(myconn, CommandType.Text, StrSQL)
                Return dr

            Catch ex As Exception
                Response.Write(ex.Message)
            End Try

        End Function

        Private Sub rgShow_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles rgShow.DataBound
            dr.Close()
            myconn.Close()
        End Sub

        'Private Sub rgShow_ItemCommand(ByVal source As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles rgShow.ItemCommand
        '    If e.CommandName = Telerik.Web.UI.RadGrid.ExportToExcelCommandName Then
        '        ExportExcel()
        '    End If
        'End Sub

        Private Sub rgShow_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgShow.NeedDataSource
            rgShow.DataSource = LoadAll()
        End Sub

        Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
            rgShow.DataSource = LoadAll()
            rgShow.Rebind()
        End Sub

        Protected Sub btnPrint_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnPrint.Click
            ExportExcel()
            rgShow.MasterTableView.ExportToExcel()
        End Sub

        Sub ExportExcel()
            rgShow.ExportSettings.ExportOnlyData = True
            rgShow.ExportSettings.IgnorePaging = True
            rgShow.ExportSettings.OpenInNewWindow = True
        End Sub

    End Class

End Namespace
