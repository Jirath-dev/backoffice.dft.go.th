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

Partial Public Class ReStatus
    Inherits Entities.Modules.PortalModuleBase

    Dim strConn As String = ConfigurationManager.ConnectionStrings("Ds2OriginConnection").ConnectionString
    Dim reader As SqlDataReader = Nothing
    Dim objConn As SqlConnection = Nothing

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
        grdMasterData.DataSource = LoadDataGrid()
        grdMasterData.DataBind()

        If grdMasterData.Items.Count > 0 Then
            btnChangeStatus.Enabled = True
        Else
            btnChangeStatus.Enabled = False
        End If
    End Sub

    Private Function LoadDataGrid() As SqlDataReader
        'Try
        '    objConn = New SqlConnection(strConn)
        '    reader = SqlHelper.ExecuteReader(objConn, CommandType.Text, "" & _
        '                    "SELECT I.[edi_year],I.[company_taxno],I.[invoice_no],I.[active_flag],I.[cancel_by],(SELECT form_name FROM form_type WHERE [form_type]=I.form_type) AS form_name FROM [dbo].[invoice_EDI] As I " & _
        '                    "WHERE replace([invoice_no],' ','')='" & txtInvoiceNo.Text.Replace(" ", "") & "' ")
        '    Return reader
        'Catch ex As Exception
        '    Response.Write(ex.Message)
        'End Try
    End Function

End Class