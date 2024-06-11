Imports DFT.Dotnetnuke.ClassLibrary
Imports Microsoft.ApplicationBlocks.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI

Partial Public Class frmSelectCompany
    Inherits System.Web.UI.Page
    Dim strTradingConn As String = ConfigurationManager.ConnectionStrings("tmpCardConn").ConnectionString
    Dim strEDIConn As String = ConfigurationManager.ConnectionStrings("OriginConnection").ConnectionString
    Dim objConn As SqlConnection = Nothing

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        txtCompany_Search.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" + btnSearch.UniqueID + "').click();return false;}} else {return true}; ")
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
            Me.RadAjaxManager1.ResponseScripts.Add("window.alert('ไม่พบข้อมูลที่ทำการค้นหา !!!');")
        End If
    End Sub

    Private Sub rgCompanyList_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles rgCompanyList.DataBound
        objConn.Close()
    End Sub

    Private Sub rgCompanyList_ItemCreated(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgCompanyList.ItemCreated
        If TypeOf e.Item Is GridDataItem Then
            Dim editLink As HyperLink = DirectCast(e.Item.FindControl("SelectLink"), HyperLink)
            editLink.Attributes("href") = "#"
            editLink.Attributes("onclick") = [String].Format("return returnToParent('{0}','{1}','{2}','{3}');", e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("Company_Taxno"), e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("CompanyName_Eng"), e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("invh_run_auto"), e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("invoice_no1"))
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
            'ค้นหาจาก P_Company ก่อน
            strCommand = "SELECT company_taxno AS Company_Taxno, company_eng AS CompanyName_Eng, company_thai AS CompanyName_Th 
                          FROM Company " &
                         "WHERE (company_taxno = '" & txtCompany_Search.Text.Trim() & "') OR (company_eng LIKE '%" & txtCompany_Search.Text & "%') OR (company_thai like '%" & txtCompany_Search.Text & "%') ORDER BY company_eng"
            ds = SqlHelper.ExecuteDataset(objConn, CommandType.Text, strCommand)
            If ds.Tables(0).Rows.Count > 0 Then
                Return ds.Tables(0)
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Function
End Class