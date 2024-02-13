Imports Microsoft.ApplicationBlocks.Data
Imports DFT.Dotnetnuke.ClassLibrary
Imports System.Data.SqlClient
Imports Telerik.Web.UI

Partial Public Class FormALLDetail
    Inherits System.Web.UI.Page
    Dim g_sWindowClose = False
    Dim strConn As String = ConfigurationManager.ConnectionStrings("OriginConnection").ConnectionString

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Request.QueryString("action") = "edit" Then
                txtInvHRunAuto.Text = CommonUtility.Get_StringValue(Request("InvHRunAuto"))
                txtInvDRunAuto.Text = CommonUtility.Get_StringValue(Request("InvDRunAuto"))
                lblHeader.Text = "แก้ไขรายการสินค้า"
                btnSave.Visible = True
                Call SetForm()
            End If
        End If
    End Sub
    Sub SetForm()
        Dim m_objReader As SqlDataReader
        m_objReader = SqlHelper.ExecuteReader(strConn, CommandType.StoredProcedure, "vi_form_edi_getDetailByDataKey_NewDS", _
        New SqlParameter("@INVH_RUN_AUTO", txtInvHRunAuto.Text), _
        New SqlParameter("@INVD_RUN_AUTO", txtInvDRunAuto.Text))
        If m_objReader.Read() Then
            txtInvHRunAuto.Text = CommonUtility.Get_StringValue(m_objReader("InvH_Run_Auto"))
            txtInvDRunAuto.Text = CommonUtility.Get_StringValue(m_objReader("InvD_Run_Auto"))
            txtTariffCode.Text = CommonUtility.Get_StringValue(m_objReader("Tariff_Code"))
            txtProductName.Text = CommonUtility.Get_StringValue(m_objReader("Product_Name"))
            txtProductDescription.Text = CommonUtility.Get_StringValue(m_objReader("Product_Description"))
            
            m_objReader.Close()
        End If
    End Sub
    Protected Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim ds_save As DataSet
        
        ds_save = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, "viSaveDetailAll_NewDS", _
                                      New SqlParameter("@INVH_RUN_AUTO", CommonUtility.Get_StringValue(txtInvHRunAuto.Text.Trim())), _
                                      New SqlParameter("@INVD_RUN_AUTO", CommonUtility.Get_StringValue(txtInvDRunAuto.Text)), _
                                      New SqlParameter("@PRODUCT_DESCRIPTION", CommonUtility.Get_StringValue(txtProductDescription.Text)))

        If ds_save.Tables(0).Rows(0).Item("retStatus") = 0 Then
            ClientScript.RegisterStartupScript(Page.GetType(), "mykey", "CloseAndRebind('navigateToInserted');", True)
        Else
            Me.RadAjaxManager1.ResponseScripts.Add("window.alert('ไม่สามารถบันทึกรายการได้');")
        End If
    End Sub

    Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        ClientScript.RegisterStartupScript(Page.GetType(), "mykey", "CancelEdit();", True)
    End Sub
End Class