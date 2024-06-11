Imports DFT.Dotnetnuke.ClassLibrary
Imports Microsoft.ApplicationBlocks.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI

Partial Public Class frmSelectInvh
    Inherits System.Web.UI.Page
    Dim strEDIConn As String = ConfigurationManager.ConnectionStrings("OriginConnection").ConnectionString
    Dim strTradingConn As String = ConfigurationManager.ConnectionStrings("tmpCardConn").ConnectionString
    Dim objConn As SqlConnection = Nothing

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'txtCompany_Search.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" + btnSearch.UniqueID + "').click();return false;}} else {return true}; ")
        If Not Page.IsPostBack Then
            txtCompany_Search.Focus()
            tblCompanyList.Visible = False
        End If
    End Sub

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        rgCompanyList.DataSource = LoadCompany()
        rgCompanyList.DataBind()
        tblCompanyList.Visible = True

        If rgCompanyList.MasterTableView.Items.Count <= 0 Then
            Me.RadAjaxManager1.ResponseScripts.Add("window.alert('��辺�����ŷ��ӡ�ä��� !!!');")
        End If
    End Sub

    Private Sub rgCompanyList_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles rgCompanyList.DataBound
        objConn.Close()
    End Sub

    Private Sub rgCompanyList_ItemCreated(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgCompanyList.ItemCreated
        If TypeOf e.Item Is GridDataItem Then
            Dim editLink As HyperLink = DirectCast(e.Item.FindControl("SelectLink"), HyperLink)
            editLink.Attributes("href") = "#"
            'editLink.Attributes("onclick") = [String].Format("return returnToParent('{0}');", e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("invh_run_auto"))
            editLink.Attributes("onclick") = [String].Format("return returnToParent('{0}','{1}','{2}','{3}');", e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("Company_Taxno"), e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("company_name"), e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("invoice_no1"), e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("invh_run_auto"))

        End If
    End Sub

    Private Sub rgCompanyList_PageIndexChanged(ByVal source As Object, ByVal e As Telerik.Web.UI.GridPageChangedEventArgs) Handles rgCompanyList.PageIndexChanged
        rgCompanyList.CurrentPageIndex = e.NewPageIndex
        rgCompanyList.DataSource = LoadCompany()
        rgCompanyList.DataBind()
    End Sub

    Function LoadCompany() As DataTable
        Try
            objConn = New SqlConnection(strEDIConn)
            Dim ds As New DataSet
            Dim strCommand As String
            '���Ҩҡ P_Company ��͹ 
            strCommand = "SELECT company_taxno,company_name,invoice_no1,invh_run_auto 
                                FROM  form_header_edi " &
                         "WHERE (invh_run_auto = '" & txtCompany_Search.Text.Trim() & "')  "
            ds = SqlHelper.ExecuteDataset(objConn, CommandType.Text, strCommand)
            If ds.Tables(0).Rows.Count > 0 Then
                Return ds.Tables(0)
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Function
End Class