Imports System.Data.SqlClient
Imports DFT.Dotnetnuke.ClassLibrary
Imports Microsoft.ApplicationBlocks.Data
Imports Telerik.Web.UI

Partial Public Class frmProductItem
    Inherits System.Web.UI.Page
    Dim strRegConn As String = ConfigurationManager.ConnectionStrings("RegisBackConnection").ConnectionString
    Dim strEDIConn As String = ConfigurationManager.ConnectionStrings("OriginConnection").ConnectionString

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        txtTariffCode.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" + imbSearch.UniqueID + "').click();return false;}} else {return true}; ")

        If Not Page.IsPostBack Then
            txtTariffCode.Focus()
            lblINVH_RUN_AUTO.Text = CommonUtility.Get_StringValue(Session("ssInvh_Run_Auto"))
            lblSite_ID.Text = CommonUtility.Get_StringValue(Session("ssRoleName"))
            lblCompany_Taxno.Text = CommonUtility.Get_StringValue(Session("ssCompanyTaxno"))
            lblFormType.Text = CommonUtility.Get_StringValue(Session("ssForm_Type"))
            lblCard_id.Text = CommonUtility.Get_StringValue(Session("ssCard_id"))

            lblCountry_Code.Text = CommonUtility.Get_StringValue(Request.QueryString("country"))
            lblGROSS_WEIGHT.Text = CommonUtility.Get_String(Request.QueryString("g_weight"))
            lblG_UNIT_CODE.Text = CommonUtility.Get_String(Request.QueryString("g_unit"))
            lblquantity5.Text = CommonUtility.Get_String(Request.QueryString("quan5"))
            lblQ_UNIT_CODE5.Text = CommonUtility.Get_String(Request.QueryString("q_unit"))
        End If
    End Sub

   
    Protected Sub imbSearch_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imbSearch.Click
        Try
            Dim dr As SqlDataReader
            dr = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "sp_common_form_manual_checkTariff_Rover", _
            New SqlParameter("@FORM_TYPE", CommonUtility.Get_String(lblFormType.Text)), _
            New SqlParameter("@COUNTRY_CODE", CommonUtility.Get_String(lblCountry_Code.Text)), _
            New SqlParameter("@COMPANY_TAXNO", CommonUtility.Get_String(lblCompany_Taxno.Text)), _
            New SqlParameter("@TARIFF_CODE", CommonUtility.Get_StringValue(txtTariffCode.Text.Trim())))

            If dr.Read() Then
                If CommonUtility.Get_StringValue(dr.Item("retStatus")) = "Y" Then
                    txtTariffName.Text = CommonUtility.Get_StringValue(dr.Item("retTariffName"))
                    txtCertificateNo.Text = CommonUtility.Get_StringValue(dr.Item("certoforigin_no"))
                ElseIf CommonUtility.Get_StringValue(dr.Item("retStatus")) = "N" Then
                    txtTariffName.Text = "NOT-FOUND"
                    txtCertificateNo.Text = "ไม่พบข้อมูลต้นทุน"
                End If
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            Dim ds As New DataSet
            ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, "vi_manual_update_k_Rover", _
            New SqlParameter("@INVH_RUN_AUTO", CommonUtility.Get_StringValue(lblINVH_RUN_AUTO.Text)), _
            New SqlParameter("@SITE_ID", CommonUtility.Get_StringValue(lblSite_ID.Text)), _
            New SqlParameter("@INVD_RUN_AUTO", "AUTO"), _
            New SqlParameter("@DESTINATION_COUNTRY", CommonUtility.Get_StringValue(lblCountry_Code.Text)), _
            New SqlParameter("@GROSS_WEIGHT", CommonUtility.Get_Decimal(lblGROSS_WEIGHT.Text)), _
            New SqlParameter("@TARIFF_CODE", CommonUtility.Get_StringValue(txtTariffCode.Text.Trim())), _
            New SqlParameter("@PRODUCT_DESCRIPTION", CommonUtility.Get_StringValue(txtTariffName.Text)), _
            New SqlParameter("@NET_WEIGHT", CommonUtility.Get_Decimal(txtNetWeight.Text)), _
            New SqlParameter("@FOB_AMT", CommonUtility.Get_Decimal(txtFOB.Text)), _
            New SqlParameter("@CAT", ""), _
            New SqlParameter("@ship_by", "0"), _
            New SqlParameter("@DEPARTURE_DATE", Nothing), _
            New SqlParameter("@G_UNIT_CODE", CommonUtility.Get_String(lblG_UNIT_CODE.Text)), _
            New SqlParameter("@Q_UNIT_CODE1", ""), _
            New SqlParameter("@certoforigin_no", CommonUtility.Get_String(txtCertificateNo.Text)), _
            New SqlParameter("@GROSS_WEIGHT_Detail", CommonUtility.Get_Decimal(0)), _
            New SqlParameter("@destination_company", ""), _
            New SqlParameter("@destination_address", ""), _
            New SqlParameter("@quantity5", CommonUtility.Get_Decimal(lblquantity5.Text)), _
            New SqlParameter("@Q_UNIT_CODE5", CommonUtility.Get_String(lblQ_UNIT_CODE5.Text)), _
            New SqlParameter("@card_id", CommonUtility.Get_String(lblCard_id.Text)))

            If ds.Tables(0).Rows.Count > 0 Then
                If CommonUtility.Get_Int32(ds.Tables(0).Rows(0).Item("retStatus")) = 0 Then
                    ClientScript.RegisterStartupScript(Page.GetType(), "mykey", "CloseAndRebind();", True)
                Else
                    Me.RadAjaxManager1.ResponseScripts.Add("window.alert('เกิดความผิดพลาด กรุณาติดต่อผู้ดูแลระบบ !!!');")
                End If
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        ClientScript.RegisterStartupScript(Page.GetType(), "mykey", "CancelEdit();", True)
    End Sub
End Class