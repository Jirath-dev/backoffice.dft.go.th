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

Namespace NTi.Modules.DFT_EDI_ReadCard
    Partial Class ViewDFT_EDI_ReadCard
        Inherits Entities.Modules.PortalModuleBase
        Dim strEDI2Conn As String = ConfigurationManager.ConnectionStrings("TradingConnection").ConnectionString
        Dim strEDIConn As String = ConfigurationManager.ConnectionStrings("OriginConnection").ConnectionString
        Dim objConn As SqlConnection = Nothing
        Dim imageUrl As String = ConfigurationManager.AppSettings("URL").ToString

        Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            If Not Page.IsPostBack Then
                txtSearch.Focus()
                tblCardDetail.Visible = False
            End If
        End Sub

        Protected Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
            Try
                Dim dr As SqlDataReader
                dr = SqlHelper.ExecuteReader(strEDI2Conn, CommandType.StoredProcedure, "pl_Select_CardForPreprint", _
                New SqlParameter("@TValue1", CommonUtility.Get_String(txtSearch.Text.Trim())), _
                New SqlParameter("@TValue2", ""), _
                New SqlParameter("@TCat", 1))

                If dr.Read() Then
                    tblCardDetail.Visible = True

                    txtCompanyName_Th.Text = CommonUtility.Get_StringValue(dr.Item("company_thai"))
                    txtCompanyName_Eng.Text = CommonUtility.Get_StringValue(dr.Item("company_eng"))
                    txtCompany_Taxno.Text = CommonUtility.Get_StringValue(dr.Item("company_taxno"))
                    txtCompany_BranchNo.Text = CommonUtility.Get_StringValue(dr.Item("company_BranchNo"))
                    txtCompany_Juristic.Text = CommonUtility.Get_StringValue(dr.Item("company_juristic"))
                    txtCompany_Address.Text = CommonUtility.Get_StringValue(dr.Item("address_thai")) & " " & CommonUtility.Get_StringValue(dr.Item("province_thai"))
                    txtCompany_Phone.Text = CommonUtility.Get_StringValue(dr.Item("phone_no"))
                    txtCompany_Fax.Text = CommonUtility.Get_StringValue(dr.Item("fax_no"))
                    txtAuthorize1.Text = CommonUtility.Get_StringValue(dr.Item("authorize1"))
                    txtAuthorize2.Text = CommonUtility.Get_StringValue(dr.Item("authorize2"))
                    txtAuthorize3.Text = CommonUtility.Get_StringValue(dr.Item("authorize3"))
                    txtAuthorize4.Text = CommonUtility.Get_StringValue(dr.Item("authorize4"))
                    txtAuthorize5.Text = CommonUtility.Get_StringValue(dr.Item("authorize5"))
                    txtAuthorize_Remark.Text = CommonUtility.Get_StringValue(dr.Item("authorize_Remark"))
                    txtAuthName.Text = CommonUtility.Get_StringValue(dr.Item("AuthName_Thai"))
                    txtCard_Level.Text = CommonUtility.Get_StringValue(dr.Item("card_level"))

                    Image1.ImageUrl = imageUrl & CommonUtility.Get_StringValue(dr.Item("AuthPersonID")) & ".JPG"
                    dr.Close()

                    dr = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "sp_check_All1", _
                    New SqlParameter("@card_id", CommonUtility.Get_StringValue(txtSearch.Text.Trim())))

                    If dr.Read() Then
                        txtReturnMsg.Text = CommonUtility.Get_StringValue(dr.Item("retMessage"))

                        dr.Close()
                    End If
                Else
                    txtSearch.Focus()
                    tblCardDetail.Visible = False
                    Me.RadAjaxManager1.ResponseScripts.Add("window.alert('ไม่พบข้อมูลบัตรผู้รับมอบที่ทำการค้นหา');")
                End If
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End Sub
    End Class

End Namespace
