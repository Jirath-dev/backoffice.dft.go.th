Imports System.Web.UI
Imports System.Collections.Generic
Imports System.Reflection

Imports DotNetNuke
Imports DotNetNuke.Services.Exceptions
Imports DotNetNuke.Services.Localization

Imports System.Data.SqlClient
Imports DFT.Dotnetnuke.ClassLibrary
Imports Microsoft.ApplicationBlocks.Data

Namespace NTi.Modules.DFT_EDI_Rollver
    Partial Class ViewDFT_EDI_Rollver
        Inherits Entities.Modules.PortalModuleBase
        Dim strConn As String = ConfigurationManager.ConnectionStrings("COConnection").ConnectionString
        Dim myReader As SqlDataReader = Nothing
        Dim objConn As SqlConnection = Nothing

        Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            If Not Page.IsPostBack Then
                txtSearch.Focus()
            End If
        End Sub

        Private Sub rgApprovedList_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles rgApprovedList.DataBound
            myReader.Close()
            objConn.Close()
        End Sub

        Private Sub rgApprovedList_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgApprovedList.NeedDataSource
            'rgApprovedList.DataSource = LoadCetigicate()
        End Sub

        Function LoadCetigicate() As SqlDataReader
            Try
                objConn = New SqlConnection(strConn)
                Dim strCommand As String
                strCommand = "SELECT * FROM tbl_certoforigin WHERE certoforigin_no like '%" & txtSearch.Text.Trim() & "%'"
                myReader = SqlHelper.ExecuteReader(objConn, CommandType.Text, strCommand)
                Return myReader
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End Function

        Protected Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
            rgApprovedList.DataSource = LoadCetigicate()
            rgApprovedList.Rebind()
        End Sub

        Protected Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
            LoadUserControl("WebUserControl1.ascx")
        End Sub

        Protected Sub LoadUserControl(ByVal pathUserControl As String)
            Try
                Dim controlToLoad As Control
                controlToLoad = Me.LoadControl(pathUserControl)
                Me.bodyRowTwo.Controls.Add(controlToLoad)
            Catch ex As Exception
                Response.Write("Exception: " & ex.Message & "." & vbCr & "Could not load a DemoUserControl.")
            End Try
        End Sub

        Protected Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
            Response.Redirect(EditUrl("WebUserControl2"))
        End Sub
    End Class

End Namespace
