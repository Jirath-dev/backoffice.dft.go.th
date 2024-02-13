Imports DFT.Dotnetnuke.ClassLibrary
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports Telerik.Web.UI

Partial Public Class Rollver
    Inherits Entities.Modules.PortalModuleBase

    Dim strConn As String = ConfigurationManager.ConnectionStrings("Ds2OriginConnection").ConnectionString
    Dim strRollver As String = ConfigurationManager.ConnectionStrings("RollverConnection").ConnectionString

    Dim reader As SqlDataReader = Nothing
    Dim objConn As SqlConnection = Nothing

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub LinkButton1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles LinkButton1.Click
        Response.Redirect(EditUrl("ViewDFT_DS2_AdminDashboard"))
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
        grdMasterData.DataSource = LoadDataGrid()
        grdMasterData.DataBind()
        PageLoadState.Value = 1
    End Sub

    Private Function LoadDataGrid() As SqlDataReader
        Try
            If optServerType.SelectedValue = 1 Then
                objConn = New SqlConnection(strRollver)

                reader = SqlHelper.ExecuteReader(objConn, CommandType.Text, "SELECT *,country+'('+country_code+')' as country_name, DATEADD(yyyy, 2, certoforigin_date) AS Cert_ExpiredDate FROM  tbl_certoforigin " & _
                "WHERE certoforigin_no='" & txtCertNo.Text & "'")
            Else
                objConn = New SqlConnection(strConn)

                reader = SqlHelper.ExecuteReader(objConn, CommandType.Text, "SELECT *,tax_ref as company_tax,country+'('+country_code+')' as country_name, DATEADD(yyyy, 2, certoforigin_date) AS Cert_ExpiredDate FROM  certoforigin_file " & _
                "WHERE certoforigin_no='" & txtCertNo.Text & "'")

            End If

            Return reader
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Function

    Private Sub grdMasterData_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles grdMasterData.NeedDataSource
        If PageLoadState.Value = "1" Then
            grdMasterData.DataSource = LoadDataGrid()
        End If
    End Sub

    Private Sub rgRequestForm_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles grdMasterData.ItemDataBound
        If TypeOf e.Item Is GridDataItem Then
            Dim openLink As HyperLink = DirectCast(e.Item.FindControl("lnkXml"), HyperLink)
            openLink.Attributes("onclick") = [String].Format("javascript:return openXml('{0}');", GetDateFilename(e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("ReadXMLDate")) & "-" & e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("XMLFileName"))
        End If
    End Sub

    Private Function GetDateFilename(ByVal d As Date) As String
        Return d.Year.ToString() & d.ToString("MMddHHmmss")
    End Function


    Private Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload
        'If objConn.State = ConnectionState.Open Then
        '    objConn.Close()
        'End If
    End Sub
End Class