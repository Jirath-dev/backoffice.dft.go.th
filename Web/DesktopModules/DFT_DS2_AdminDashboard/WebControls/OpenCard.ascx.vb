Imports System.Web.UI
Imports System.Collections.Generic
Imports System.Reflection

Imports DotNetNuke
Imports DotNetNuke.Services.Exceptions
Imports DotNetNuke.Services.Localization

Imports Microsoft.ApplicationBlocks.Data
Imports System.Data.SqlClient

Partial Public Class OpenCard
    Inherits Entities.Modules.PortalModuleBase

    Dim strConn As String = ConfigurationManager.ConnectionStrings("Ds2OriginConnection").ConnectionString
    Dim reader As SqlDataReader = Nothing
    Dim objConn As SqlConnection = Nothing

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
        grdMasterData.Rebind()

        If grdMasterData.Items.Count > 0 Then
            btnRegisCardId.Enabled = True
        Else
            btnRegisCardId.Enabled = False
        End If
    End Sub

    Private Function LoadDataGrid() As SqlDataReader
        Try
            objConn = New SqlConnection(strConn)
            reader = SqlHelper.ExecuteReader(objConn, CommandType.Text, "" & _
                            "SELECT R.[company_taxno],R.[card_id],R.[commit_name],R.[expire_date],R.[active_flag],R.[card_type],R.[AuthName],R.[AuthName_Thai],C.company_internet_ds_edi,C.company_internet_xml_edi FROM [dbo].[rfcard] AS R, [dbo].[company] AS C " & _
                            "WHERE (replace(R.[card_id],' ','')='" & txtCardId.Text.Replace(" ", "") & "') AND (R.[company_taxno]=C.[company_taxno])  ")
            Return reader
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Function

    Private Sub grdMasterData_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles grdMasterData.NeedDataSource
        If txtCardId.Text <> "" Then
            grdMasterData.DataSource = LoadDataGrid()
        End If
    End Sub
End Class