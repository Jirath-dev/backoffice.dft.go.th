Imports System.Web.UI
Imports System.Collections.Generic
Imports System.Reflection

Imports DotNetNuke
Imports DotNetNuke.Services.Exceptions
Imports DotNetNuke.Services.Localization

Imports DFT.Dotnetnuke.ClassLibrary
Imports Microsoft.ApplicationBlocks.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI

Namespace NTi.Modules.DFT_RFCard_PreCheck
    Partial Class ViewDFT_RFCard_PreCheck
        Inherits Entities.Modules.PortalModuleBase
        Dim strConn As String = ConfigurationManager.ConnectionStrings("RegisBackConnection").ConnectionString
        Dim strTMPConn As String = ConfigurationManager.ConnectionStrings("tmpCardConn").ConnectionString
        Dim objConn As SqlConnection = Nothing
        Dim myReader As SqlDataReader = Nothing

        Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            If Not Page.IsPostBack Then
                txtCompanySearch.Focus()
            End If
        End Sub

        Private Sub rgCompanyUserList_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles rgCompanyUserList.DataBound
            objConn.Close()
        End Sub

        Private Sub rgCompanyUserList_ItemCommand(ByVal source As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles rgCompanyUserList.ItemCommand
            Dim Company_TaxNo As String
            Dim Company_BranchNo As String
            If e.CommandName = "Select" Then
                Company_TaxNo = e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("Company_Taxno")
                Company_BranchNo = e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("company_BranchNo")
                Response.Redirect(EditUrl("ctrlCompanyDetailPreCheck") & "&Company_Taxno=" & Company_TaxNo & "&Company_BranchNo=" & Company_BranchNo)
            End If
        End Sub

        Private Sub rgCompanyUserList_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgCompanyUserList.ItemDataBound
            If Not (e.Item.FindControl("btnSelect") Is Nothing) Then
                CType(e.Item.FindControl("btnSelect"), ImageButton).Attributes("onmouseover") = "this.style.cursor='hand';"
            End If
        End Sub

        Private Function SearchCompany() As DataTable
            Try
                Dim ds As New DataSet
                Dim strCommand As String
                strCommand = "SELECT company_taxno AS Company_Taxno, company_juristic AS Company_Juristic, company_BranchNo, company_thai AS CompanyName_Th, company_eng AS CompanyName_Eng " & _
                                 "FROM P_Company " & _
                                 "WHERE (company_taxno = '" & txtCompanySearch.Text & "') OR (company_thai LIKE '%" & txtCompanySearch.Text & "%') OR (company_eng LIKE '%" & txtCompanySearch.Text & "%')"
                objConn = New SqlConnection(strTMPConn)
                ds = SqlHelper.ExecuteDataset(objConn, CommandType.Text, strCommand)
                If ds.Tables(0).Rows.Count > 0 Then
                    Return ds.Tables(0)
                End If
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End Function

        Protected Sub btnSearchCompany_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchCompany.Click
            rgCompanyUserList.DataSource = SearchCompany()
            rgCompanyUserList.DataBind()

            If rgCompanyUserList.MasterTableView.Items.Count <= 0 Then
                Me.RadAjaxManager1.ResponseScripts.Add("window.alert('ไม่พบข้อมูลตามเงื่อนไขที่ทำการค้นหา !!!');")
            End If
        End Sub

        Private Sub rgCompanyUserList_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgCompanyUserList.NeedDataSource
            rgCompanyUserList.DataSource = SearchCompany()
        End Sub
    End Class

End Namespace
