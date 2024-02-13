Imports System.Web.UI
Imports System.Collections.Generic
Imports System.Reflection

Imports DotNetNuke
Imports DotNetNuke.Services.Exceptions
Imports DotNetNuke.Services.Localization

Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports DFT.Dotnetnuke.ClassLibrary
Imports Telerik.Web.UI

Partial Public Class ctrlCompanyDetailPreCheck
    Inherits Entities.Modules.PortalModuleBase
    Dim strConn As String = ConfigurationManager.ConnectionStrings("RegisBackConnection").ConnectionString
    Dim strTMPConn As String = ConfigurationManager.ConnectionStrings("tmpCardConn").ConnectionString
    Dim objConn As SqlConnection = Nothing
    Dim myReader As SqlDataReader = Nothing

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If Request.QueryString("Company_Taxno") <> "" And Request.QueryString("Company_BranchNo") <> "" Then
                txtCompany_Taxno.Text = CommonUtility.Get_StringValue(Request.QueryString("Company_Taxno"))
                txtCompany_BranchNo.Text = CommonUtility.Get_StringValue(Request.QueryString("Company_BranchNo"))

                Call LoadCompany()

            End If
        End If
    End Sub

    Private Sub LoadCompany()
        Try
            Dim ds As New DataSet
            ds = SqlHelper.ExecuteDataset(strTMPConn, CommandType.StoredProcedure, "vi_select_company", _
            New SqlParameter("@TCat", 1), _
            New SqlParameter("@company_taxno", CommonUtility.Get_StringValue(txtCompany_Taxno.Text)), _
            New SqlParameter("@company_BranchNo", CommonUtility.Get_Int32(txtCompany_BranchNo.Text)))

            If ds.Tables(0).Rows.Count > 0 Then
                With ds.Tables(0).Rows(0)
                    txtCompanyName_Th.Text = CommonUtility.Get_StringValue(.Item("company_thai"))
                    txtCompanyName_Eng.Text = CommonUtility.Get_StringValue(.Item("company_eng"))
                    txtCompany_Taxno.Text = CommonUtility.Get_StringValue(.Item("company_taxno"))
                    txtCompany_Juristic.Text = CommonUtility.Get_StringValue(.Item("company_juristic"))
                    txtCompany_AddressTh.Text = CommonUtility.Get_StringValue(.Item("address_thai"))
                    txtCompany_Province_Thai.Text = CommonUtility.Get_StringValue(.Item("province_thai"))
                    txtCompany_AddressEng.Text = CommonUtility.Get_StringValue(.Item("address_eng"))
                    txtCompany_Province_Eng.Text = CommonUtility.Get_StringValue(.Item("province_eng"))
                    txtCompany_Phone.Text = CommonUtility.Get_StringValue(.Item("phone_no"))
                    txtCompany_Fax.Text = CommonUtility.Get_StringValue(.Item("fax_no"))
                    txtAuthorize1.Text = CommonUtility.Get_StringValue(.Item("authorize1"))
                    txtAuthorize2.Text = CommonUtility.Get_StringValue(.Item("authorize2"))
                    txtAuthorize3.Text = CommonUtility.Get_StringValue(.Item("authorize3"))
                    txtAuthorize4.Text = CommonUtility.Get_StringValue(.Item("authorize4"))
                    txtAuthorize5.Text = CommonUtility.Get_StringValue(.Item("authorize5"))
                    txtAuthorize_Remark.Text = CommonUtility.Get_StringValue(.Item("authorize_Remark"))
                    txtCompany_Internet.Text = CommonUtility.Get_StringValue(.Item("company_internet"))
                End With
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Function LoadCardNoExpired() As SqlDataReader
        Try
            Dim strCommand As String
            strCommand = "SELECT * FROM P_Rfcard WHERE company_taxno = '" & txtCompany_Taxno.Text & "' AND company_branchNo = " & CommonUtility.Get_Int32(txtCompany_BranchNo.Text) & " AND active_flag = 'Z' ORDER BY card_id ASC"
            'objConn = New SqlConnection(strConn)
            objConn = New SqlConnection(strTMPConn)
            myReader = SqlHelper.ExecuteReader(objConn, CommandType.Text, strCommand)
            Return myReader
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Function

    Private Sub rgNoExpiredCardList_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles rgNoExpiredCardList.DataBound
        myReader.Close()
        objConn.Close()
    End Sub

    Private Sub rgNoExpiredCardList_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgNoExpiredCardList.ItemDataBound
        If TypeOf e.Item Is GridDataItem Then
            Dim dataItem As GridDataItem = CType(e.Item, GridDataItem)
            If dataItem("card_type").Text.Trim() = "P" Then
                Dim viewLink As HyperLink = DirectCast(e.Item.FindControl("ViewLink"), HyperLink)
                viewLink.Attributes("href") = "#"
                viewLink.Attributes("onclick") = [String].Format("return ShowEditCardForm('{0}','{1}');", e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("card_id"), e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("company_taxno"))
            ElseIf dataItem("card_type").Text = "C" Then
                Dim viewLink As HyperLink = DirectCast(e.Item.FindControl("ViewLink"), HyperLink)
                viewLink.Attributes("href") = "#"
                viewLink.Attributes("onclick") = [String].Format("return ShowEditCompanyCardForm('{0}','{1}');", e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("card_id"), e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("company_taxno"))
            End If
        End If
    End Sub

    Private Sub rgNoExpiredCardList_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgNoExpiredCardList.NeedDataSource
        rgNoExpiredCardList.DataSource = LoadCardNoExpired()
    End Sub

    Function LoadCardExpired() As SqlDataReader
        Try
            Dim strCommand As String
            strCommand = "SELECT * FROM P_Rfcard WHERE company_taxno = '" & txtCompany_Taxno.Text & "' AND company_branchNo = " & CommonUtility.Get_Int32(txtCompany_BranchNo.Text) & " AND active_flag = 'C' ORDER BY card_id ASC"
            'objConn = New SqlConnection(strConn)
            objConn = New SqlConnection(strTMPConn)
            myReader = SqlHelper.ExecuteReader(objConn, CommandType.Text, strCommand)
            Return myReader
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Function

    Private Sub rgExpiredCardList_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles rgExpiredCardList.DataBound
        myReader.Close()
        objConn.Close()
    End Sub

    Private Sub rgExpiredCardList_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgExpiredCardList.ItemDataBound
        If TypeOf e.Item Is GridDataItem Then
            Dim dataItem As GridDataItem = CType(e.Item, GridDataItem)
            If dataItem("card_type").Text.Trim() = "P" Then
                Dim viewLink As HyperLink = DirectCast(e.Item.FindControl("ViewLink"), HyperLink)
                viewLink.Attributes("href") = "#"
                viewLink.Attributes("onclick") = [String].Format("return ShowEditCardForm('{0}','{1}');", e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("card_id"), e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("company_taxno"))
            ElseIf dataItem("card_type").Text = "C" Then
                Dim viewLink As HyperLink = DirectCast(e.Item.FindControl("ViewLink"), HyperLink)
                viewLink.Attributes("href") = "#"
                viewLink.Attributes("onclick") = [String].Format("return ShowEditCompanyCardForm('{0}','{1}');", e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("card_id"), e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("company_taxno"))
            End If
        End If
    End Sub

    Private Sub rgExpiredCardList_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgExpiredCardList.NeedDataSource
        rgExpiredCardList.DataSource = LoadCardExpired()
    End Sub

    Private Sub RadAjaxManager1_AjaxRequest(ByVal sender As Object, ByVal e As Telerik.Web.UI.AjaxRequestEventArgs) Handles RadAjaxManager1.AjaxRequest
        rgNoExpiredCardList.Rebind()
        rgExpiredCardList.Rebind()
    End Sub
End Class