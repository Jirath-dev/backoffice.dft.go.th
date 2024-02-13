Imports System.Data.SqlClient
Imports DFT.Dotnetnuke.ClassLibrary
Imports Microsoft.ApplicationBlocks.Data
Imports Telerik.Web.UI

Partial Public Class frmChangeForm
    Inherits System.Web.UI.Page
    Dim strEDIConn As String = ConfigurationManager.ConnectionStrings("OriginConnection").ConnectionString
    Dim objConn As SqlConnection = Nothing
    Dim myReader As SqlDataReader = Nothing

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            lblSiteName.Text = CommonUtility.Get_StringValue(Session("ssRoleName"))
            lblInvh_Run_Auto.Text = CommonUtility.Get_StringValue(Session("ssInvh_Run_Auto"))
        End If
    End Sub

    Function LoadAllForm() As sqldatareader
        Try
            Dim strCommand As String = "select * from FORM_TYPE where FORM_TYPE<>'ALL' order by FORM_TYPE"
            objConn = New SqlConnection(strEDIConn)
            myReader = SqlHelper.ExecuteReader(strEDIConn, CommandType.Text, strCommand)
            Return myReader
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Function

    Private Sub rgFormList_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles rgFormList.DataBound
        myReader.Close()
        objConn.Close()
    End Sub

    Private Sub rgFormList_ItemCommand(ByVal source As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles rgFormList.ItemCommand
        Dim FormType As String
        If e.CommandName = "selected" Then
            FormType = e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("form_type")
            Dim ds As New DataSet
            ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, "sp_common_form_manual_changeForm", _
            New SqlParameter("@INVH_RUN_AUTO", CommonUtility.Get_StringValue(lblInvh_Run_Auto.Text)), _
            New SqlParameter("@SITE_ID", CommonUtility.Get_StringValue(lblSiteName.Text)), _
            New SqlParameter("@FORM_TYPE", CommonUtility.Get_StringValue(FormType)))

            If ds.Tables(0).Rows.Count > 0 Then
                If CommonUtility.Get_Int32(ds.Tables(0).Rows(0).Item("retSTATUS")) = 0 Then
                    'ClientScript.RegisterStartupScript(Page.GetType(), "mykey1", "CloseAndRebind();", True)
                    Me.RadAjaxManager1.ResponseScripts.Add("CloseAndRebind();")
                Else
                    Me.RadAjaxManager1.ResponseScripts.Add("window.alert('เกิดความผิดพลาด กรุณาติดต่อผู้ดูแลระบบ !!!');")
                End If
            End If
        End If
    End Sub

    Private Sub rgFormList_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgFormList.ItemDataBound
        If Not (e.Item.FindControl("SelectLink") Is Nothing) Then
            CType(e.Item.FindControl("SelectLink"), ImageButton).Attributes("onmouseover") = "this.style.cursor='hand';"
        End If
    End Sub

    Private Sub rgFormList_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgFormList.NeedDataSource
        rgFormList.DataSource = LoadAllForm()
    End Sub
End Class