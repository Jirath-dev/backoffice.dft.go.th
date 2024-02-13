Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports Telerik.Web.UI

Public Class frmBlackListV2
    Inherits System.Web.UI.Page
    Dim company_tax As String = ""
    Dim strRegConn As String = ConfigurationManager.ConnectionStrings("RegisBackConnection").ConnectionString
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Request.QueryString("comtax") Is Nothing Then
            company_tax = ConverttoString(Request.QueryString("comtax"))
        End If
    End Sub

    Public Function ConverttoString(obj As Object) As String
        Dim result As String = ""
        Try
            result = Convert.ToString(obj)
        Catch ex As Exception

        End Try
        Return result
    End Function

    Private Sub grdBlackList_NeedDataSource(source As Object, e As GridNeedDataSourceEventArgs) Handles grdBlackList.NeedDataSource
        '//grdBlackList.DataSource = getBlackList(company_tax)

        grdBlackList.DataSource = ReportPrintClass.GetOriginAlertDetail(company_tax)

    End Sub
    Private Function getBlackList(comtax As String) As DataSet
        Dim cmd As String = "viGetDetail_BlackListByCompany_NewDS"
        Dim ds As New DataSet
        Try
            ds = SqlHelper.ExecuteDataset(strRegConn, CommandType.StoredProcedure, cmd, New SqlParameter("@COMPANY_TAXNO", comtax.Trim()))
        Catch ex As Exception

        End Try
        Return ds
    End Function
    Function set_dateFormat(ByVal s_date As Object) As String

        Dim r_date As String = ""
        Try
            r_date = Convert.ToDateTime(s_date).ToString("d MMM yy", New System.Globalization.CultureInfo("th-TH"))
        Catch ex As Exception

        End Try
        Return r_date
    End Function
End Class