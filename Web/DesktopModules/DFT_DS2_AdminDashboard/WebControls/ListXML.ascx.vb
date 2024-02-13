Imports System.Web.UI
Imports System.Collections.Generic
Imports System.Reflection

Imports DotNetNuke
Imports DotNetNuke.Services.Exceptions
Imports DotNetNuke.Services.Localization

Imports DFT.Dotnetnuke.ClassLibrary
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports Telerik.Web.UI

Partial Public Class ListXML
    Inherits Entities.Modules.PortalModuleBase

    Dim strConn As String = ConfigurationManager.ConnectionStrings("Ds2OriginConnection").ConnectionString
    Dim reader As SqlDataReader = Nothing
    Dim objConn As SqlConnection = Nothing

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            txtFromDate.SelectedDate = Now
            txtToDate.SelectedDate = Now
        End If
    End Sub

    Protected Sub LinkButton1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles LinkButton1.Click
        Response.Redirect(EditUrl("ViewDFT_DS2_AdminDashboard"))
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
        grdMasterData.Rebind()
    End Sub

    Private Function LoadDataGrid() As SqlDataReader
        Try
            objConn = New SqlConnection(strConn)
            reader = SqlHelper.ExecuteReader(objConn, CommandType.Text, "SELECT X.Id, X.Invh_Run_Auto, X.XMLFileName, X.ReadXMLDate, F.form_name, C.company_eng, H.card_id, S.site_name " & _
                            "FROM tbl_XMLFile_Log AS X INNER JOIN " & _
                            "form_header_edi AS H ON X.Invh_Run_Auto = H.invh_run_auto INNER JOIN " & _
                            "form_type AS F ON H.form_type = F.form_type INNER JOIN " & _
                            "company AS C ON X.Company_TaxNo = C.company_taxno INNER JOIN " & _
                            "site_plus AS S ON H.site_id = S.site_id " & _
                            "WHERE (X.ReadXMLDate BETWEEN '" & FunctionUtility.DMY2YMD(txtFromDate.SelectedDate.Value) & "' AND '" & FunctionUtility.DMY2YMD(txtToDate.SelectedDate.Value) & "') ORDER BY ReadXMLDate DESC")
            Return reader
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Function

    Private Sub grdMasterData_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles grdMasterData.NeedDataSource
        grdMasterData.DataSource = LoadDataGrid()
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

End Class