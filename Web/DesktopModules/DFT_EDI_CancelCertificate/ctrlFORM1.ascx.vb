Imports System.Web.UI
Imports System.Collections.Generic
Imports System.Reflection

Imports DotNetNuke
Imports DotNetNuke.Services.Exceptions
Imports DotNetNuke.Services.Localization

Imports Microsoft.ApplicationBlocks.Data
Imports DFT.Dotnetnuke.ClassLibrary
Imports System.Data.SqlClient
Imports Telerik.Web.UI

Partial Public Class ctrlFORM1
    Inherits Entities.Modules.PortalModuleBase
    Dim strConn As String = ConfigurationManager.ConnectionStrings("OriginConnection").ConnectionString
    Dim myReader As SqlDataReader = Nothing
    Dim objConn As SqlConnection = Nothing

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'SiteUrl = "http://" & DotNetNuke.Common.GetDomainName(Request)
        If Not Session("invh_run_auto") Is Nothing Then
            txtInvHRunAuto.Text = CommonUtility.Get_StringValue(Session("invh_run_auto"))
            Call SetForm(txtInvHRunAuto.Text)
            Call LoadGridDetail()
        End If
    End Sub

    Private Sub SetForm(ByVal InvHRunAuto As String)
        Try
            Dim m_objReader As SqlDataReader
            m_objReader = SqlHelper.ExecuteReader(strConn, CommandType.StoredProcedure, "sp_common_form_edi_getHeaderByDataKey", _
            New SqlParameter("@INVH_RUN_AUTO", InvHRunAuto))

            If m_objReader.Read() Then
                txtFormNo.Text = CommonUtility.Get_StringValue(m_objReader("Form_No"))
                txtRequestPerson.Text = CommonUtility.Get_StringValue(m_objReader("Request_Person"))
                txtCompanyName.Text = CommonUtility.Get_StringValue(m_objReader("Company_Name"))
                txtCompanyTaxNo.Text = CommonUtility.Get_StringValue(m_objReader("Company_TaxNo"))
                txtCompanyAddress.Text = CommonUtility.Get_StringValue(m_objReader("Company_Address"))
                txtCompanyProvince.Text = CommonUtility.Get_StringValue(m_objReader("Company_Province"))
                txtCompanyCountry.Text = CommonUtility.Get_StringValue(m_objReader("Company_Country"))
                txtCompanyPhone.Text = CommonUtility.Get_StringValue(m_objReader("Company_Phone"))
                txtCompanyFax.Text = CommonUtility.Get_StringValue(m_objReader("Fax_No"))
                txtCardID.Text = CommonUtility.Get_StringValue(m_objReader("Card_ID"))
                dropDestRemark.SelectedValue = CommonUtility.Get_StringValue(m_objReader("Dest_Remark"))
                txtOB_Address.Text = CommonUtility.Get_StringValue(m_objReader("OB_ADDRESS"))
                txtDestinationCompany.Text = CommonUtility.Get_StringValue(m_objReader("Destination_Company"))
                txtDestinationTaxID.Text = CommonUtility.Get_StringValue(m_objReader("Destination_TaxID"))
                txtDestinationAddress.Text = CommonUtility.Get_StringValue(m_objReader("Destination_Address"))
                txtDestinationProvince.Text = CommonUtility.Get_StringValue(m_objReader("Destination_Province"))
                txtDestReceiveCountry.Text = CommonUtility.Get_StringValue(m_objReader("Dest_Receive_Country"))
                dropDestinationCountry.SelectedValue = CommonUtility.Get_StringValue(m_objReader("Destination_Country"))
                txtDestinationPhone.Text = CommonUtility.Get_StringValue(m_objReader("Destination_Phone"))
                txtDestinationFax.Text = CommonUtility.Get_StringValue(m_objReader("Destination_Fax"))
                SetIndexOfRadio(CommonUtility.Get_StringValue(m_objReader("Ship_By")), radShipBy)
                txtTransportBy.Text = CommonUtility.Get_StringValue(m_objReader("Transport_By"))
                txtInvoiceNo1.Text = CommonUtility.Get_StringValue(m_objReader("invoice_no1"))
                txtInvoiceNo2.Text = CommonUtility.Get_StringValue(m_objReader("invoice_no2"))
                txtInvoiceNo3.Text = CommonUtility.Get_StringValue(m_objReader("invoice_no3"))
                txtInvoiceNo4.Text = CommonUtility.Get_StringValue(m_objReader("invoice_no4"))
                txtInvoiceNo5.Text = CommonUtility.Get_StringValue(m_objReader("invoice_no5"))

                If Not m_objReader("invoice_Date1").Equals(System.DBNull.Value) Then txtInvoiceDate1.SelectedDate = CommonUtility.Get_DateTime(m_objReader("invoice_Date1"))
                If Not m_objReader("invoice_Date2").Equals(System.DBNull.Value) Then txtInvoiceDate2.SelectedDate = CommonUtility.Get_DateTime(m_objReader("invoice_Date2"))
                If Not m_objReader("invoice_Date3").Equals(System.DBNull.Value) Then txtInvoiceDate3.SelectedDate = CommonUtility.Get_DateTime(m_objReader("invoice_Date3"))
                If Not m_objReader("invoice_Date4").Equals(System.DBNull.Value) Then txtInvoiceDate4.SelectedDate = CommonUtility.Get_DateTime(m_objReader("invoice_Date4"))
                If Not m_objReader("invoice_Date5").Equals(System.DBNull.Value) Then txtInvoiceDate5.SelectedDate = CommonUtility.Get_DateTime(m_objReader("invoice_Date5"))

                SetIndexOfRadio(CommonUtility.Get_StringValue(m_objReader("Bill_Type")), radBillType)
                txtBillTypeOther.Text = CommonUtility.Get_StringValue(m_objReader("Bill_Type_Other"))
                txtBlNo.Text = CommonUtility.Get_StringValue(m_objReader("Bl_No"))

                If Not m_objReader("sailing_date").Equals(System.DBNull.Value) Then txtSailingDate.SelectedDate = CommonUtility.Get_DateTime(m_objReader("sailing_date"))
                If Not m_objReader("EDI_Date").Equals(System.DBNull.Value) Then txtEdiDate.SelectedDate = CommonUtility.Get_DateTime(m_objReader("EDI_Date"))

                txtAttachFile.Text = CommonUtility.Get_StringValue(m_objReader("Attach_File"))
                txtFactory.Text = CommonUtility.Get_StringValue(m_objReader("Factory"))
                txtFactoryTaxID.Text = CommonUtility.Get_StringValue(m_objReader("Factory_TaxID"))
                txtFactoryAddress.Text = CommonUtility.Get_StringValue(m_objReader("Factory_Address"))
                txtFactoryProvince.Text = CommonUtility.Get_StringValue(m_objReader("Factory_Province"))
                txtFactoryCountry.Text = CommonUtility.Get_StringValue(m_objReader("Factory_Country"))
                txtFactoryPhone.Text = CommonUtility.Get_StringValue(m_objReader("Factory_Phone"))
                txtFactoryFax.Text = CommonUtility.Get_StringValue(m_objReader("Factory_Fax"))
                txtAuthorize.Text = CommonUtility.Get_StringValue(m_objReader("Authorize1"))
                If CommonUtility.Get_StringValue(m_objReader("Authorize2")) <> "" And Trim(txtAuthorize.Text) <> "" Then txtAuthorize.Text = txtAuthorize.Text & "  , "
                txtAuthorize.Text = txtAuthorize.Text & CommonUtility.Get_StringValue(m_objReader("Authorize2"))
                If CommonUtility.Get_StringValue(m_objReader("Authorize3")) <> "" And Trim(txtAuthorize.Text) <> "" Then txtAuthorize.Text = txtAuthorize.Text & "  , "
                txtAuthorize.Text = txtAuthorize.Text & CommonUtility.Get_StringValue(m_objReader("Authorize3"))
                If CommonUtility.Get_StringValue(m_objReader("Authorize4")) <> "" And Trim(txtAuthorize.Text) <> "" Then txtAuthorize.Text = txtAuthorize.Text & "  , "
                txtAuthorize.Text = txtAuthorize.Text & CommonUtility.Get_StringValue(m_objReader("Authorize4"))
                If CommonUtility.Get_StringValue(m_objReader("Authorize5")) <> "" And Trim(txtAuthorize.Text) <> "" Then txtAuthorize.Text = txtAuthorize.Text & "  , "
                txtAuthorize.Text = txtAuthorize.Text & CommonUtility.Get_StringValue(m_objReader("Authorize5"))
                If CommonUtility.Get_StringValue(m_objReader("Authorize6")) <> "" And Trim(txtAuthorize.Text) <> "" Then txtAuthorize.Text = txtAuthorize.Text & "  , "
                txtAuthorize.Text = txtAuthorize.Text & CommonUtility.Get_StringValue(m_objReader("Authorize6"))
                'txtGrossWeight.Text = CommonUtility.Get_StringValue(m_objReader("Gross_Weight"))
                'drpG_UNIT_CODE.SelectedValue = CommonUtility.Get_StringValue(m_objReader("G_UNIT_CODE"))
                'txtQuantity1.Text = CommonUtility.Get_StringValue(m_objReader("Quantity1"))
                'drpQ_UNIT_CODE1.SelectedValue = CommonUtility.Get_StringValue(m_objReader("Q_UNIT_CODE1"))
                'txtQuantity2.Text = CommonUtility.Get_StringValue(m_objReader("Quantity2"))
                'drpQ_UNIT_CODE2.SelectedValue = CommonUtility.Get_StringValue(m_objReader("Q_UNIT_CODE2"))
                'txtQuantity3.Text = CommonUtility.Get_StringValue(m_objReader("Quantity3"))
                'drpQ_UNIT_CODE3.SelectedValue = CommonUtility.Get_StringValue(m_objReader("Q_UNIT_CODE3"))
                'txtQuantity4.Text = CommonUtility.Get_StringValue(m_objReader("Quantity4"))
                'drpQ_UNIT_CODE4.SelectedValue = CommonUtility.Get_StringValue(m_objReader("Q_UNIT_CODE4"))
                'txtQuantity5.Text = CommonUtility.Get_StringValue(m_objReader("Quantity5"))
                'drpQ_UNIT_CODE5.SelectedValue = CommonUtility.Get_StringValue(m_objReader("Q_UNIT_CODE5"))
                dropDestRemark1.SelectedValue = CommonUtility.Get_StringValue(m_objReader("Dest_Remark1"))
                'dropWeightDisplay.SelectedValue = CommonUtility.Get_StringValue(m_objReader("WeightDisplayHeader"))
                txtOB_Dest_Address.Text = CommonUtility.Get_StringValue(m_objReader("OB_Dest_Address"))
                txtComapnyEmail.Text = CommonUtility.Get_StringValue(m_objReader("company_email"))
                txtDestinationEmail.Text = CommonUtility.Get_StringValue(m_objReader("destination_email"))

                m_objReader.Close()
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Sub SetIndexOfRadio(ByVal v_sIndex As String, ByRef v_objRadioButtonControl As RadioButtonList)
        v_objRadioButtonControl.SelectedIndex = v_objRadioButtonControl.Items.IndexOf(v_objRadioButtonControl.Items.FindByValue(v_sIndex))
    End Sub

    Function LoadDetailListAll() As SqlDataReader
        Try
            objConn = New SqlConnection(strConn)
            myReader = SqlHelper.ExecuteReader(objConn, CommandType.StoredProcedure, "sp_common_form_edi_getDetailListAll", _
            New SqlParameter("@INVH_RUN_AUTO", CommonUtility.Get_StringValue(Session("invh_run_auto"))))

            Return myReader
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Function

    Private Sub grdItemData_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdItemData.DataBound
        myReader.Close()
        objConn.Close()
    End Sub

    Private Sub grdItemData_ItemCreated(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles grdItemData.ItemCreated
        If TypeOf e.Item Is GridDataItem Then
            Dim viewLink As HyperLink = DirectCast(e.Item.FindControl("ViewLink"), HyperLink)
            viewLink.Attributes("href") = "#"
            viewLink.Attributes("onclick") = [String].Format("return ShowViewForm('{0}','{1}','{2}');", e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("invh_run_auto"), e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("invd_run_auto"), "view")
        End If
    End Sub

    Private Sub grdItemData_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles grdItemData.NeedDataSource
        grdItemData.DataSource = LoadGridDetail()
    End Sub

    Private Function LoadGridDetail() As SqlDataReader
        Try
            objConn = New SqlConnection(strConn)
            myReader = SqlHelper.ExecuteReader(objConn, CommandType.StoredProcedure, "sp_common_form_edi_getDetailListAll", _
            New SqlParameter("@INVH_RUN_AUTO", txtInvHRunAuto.Text))

            Return myReader
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Function
End Class