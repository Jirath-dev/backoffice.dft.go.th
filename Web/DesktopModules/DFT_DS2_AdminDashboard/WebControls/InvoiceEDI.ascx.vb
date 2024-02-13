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

Partial Public Class InvoiceEDI
    Inherits Entities.Modules.PortalModuleBase

    Dim strConn As String = ConfigurationManager.ConnectionStrings("Ds2OriginConnection").ConnectionString
    Dim reader As SqlDataReader = Nothing
    Dim objConn As SqlConnection = Nothing

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            'Load data.
            ' grdMasterData.DataSource = LoadDataGrid()
            ' grdMasterData.DataBind()
        End If
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
        Try
            objConn = New SqlConnection(strConn)
            reader = SqlHelper.ExecuteReader(objConn, CommandType.Text, "" & _
                            "SELECT I.[edi_year],I.[company_taxno],I.[invoice_no],I.[active_flag],I.[cancel_by],(SELECT form_name FROM form_type WHERE [form_type]=I.form_type) AS form_name FROM [dbo].[invoice_EDI] As I " & _
                            "WHERE replace([invoice_no],' ','')='" & txtInvoiceNo.Text.Replace(" ", "") & "' ")
            Return reader
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Function

    Private Sub grdMasterData_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles grdMasterData.NeedDataSource
        grdMasterData.DataSource = LoadDataGrid()
    End Sub

    Private Sub grdMasterData_PageIndexChanged(ByVal source As Object, ByVal e As Telerik.Web.UI.GridPageChangedEventArgs) Handles grdMasterData.PageIndexChanged
        grdMasterData.CurrentPageIndex = e.NewPageIndex
        grdMasterData.DataBind()
    End Sub

    Protected Sub btnChangeStatus_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnChangeStatus.Click
        Try
            Dim ret As Integer
            objConn = New SqlConnection(strConn)
            ret = SqlHelper.ExecuteNonQuery(objConn, CommandType.Text, "" & _
                            "UPDATE [Invoice_EDI] SET [active_flag]='" & ddlStatus.SelectedValue & "' " & _
                            "WHERE replace([invoice_no],' ','')='" & txtInvoiceNo.Text.Replace(" ", "") & "' ")

            grdMasterData.Rebind()

            Page.ClientScript.RegisterStartupScript(Page.GetType(), "mykey", "alert('Completed - บันทึกข้อมูลเรียบร้อยแล้ว" & ret.ToString() & "');", True)

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload
        objConn.Close()
    End Sub
End Class