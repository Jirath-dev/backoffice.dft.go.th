Imports System.Data.SqlClient
Imports DFT.Dotnetnuke.ClassLibrary
Imports Microsoft.ApplicationBlocks.Data
Imports Telerik.Web.UI

Partial Public Class frmChangeCompany
    Inherits System.Web.UI.Page
    Dim strRegConn As String = ConfigurationManager.ConnectionStrings("RegisBackConnection").ConnectionString
    Dim strEDIConn As String = ConfigurationManager.ConnectionStrings("OriginConnection").ConnectionString
    
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            lblSiteName.Text = CommonUtility.Get_StringValue(Session("ssRoleName"))
            lblInvh_Run_Auto.Text = CommonUtility.Get_StringValue(Session("ssInvh_Run_Auto"))
        End If
    End Sub

    Protected Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            Dim ds As New DataSet
            ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, "vi_manual_changeCompany", _
            New SqlParameter("@INVH_RUN_AUTO", CommonUtility.Get_StringValue(lblInvh_Run_Auto.Text)), _
            New SqlParameter("@SITE_ID", CommonUtility.Get_StringValue(lblSiteName.Text)), _
            New SqlParameter("@COMPANY_TAXNO", CommonUtility.Get_StringValue(txtCompany_Taxno.Text.Trim())), _
            New SqlParameter("@COMPANY_BRANCHNO", CommonUtility.Get_Int32(txtCompany_BranchNo.Text.Trim())))

            If ds.Tables(0).Rows.Count > 0 Then
                If CommonUtility.Get_Int32(ds.Tables(0).Rows(0).Item("retSTATUS")) = 0 Then
                    ClientScript.RegisterStartupScript(Page.GetType(), "mykey", "CloseAndRebind();", True)
                Else
                    Me.RadAjaxManager1.ResponseScripts.Add("window.alert('ไม่พบข้อมูลบริษัท เลขที่  " & txtCompany_Taxno.Text.Trim() & " !!!');")
                End If
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub
End Class