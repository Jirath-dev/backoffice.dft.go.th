Imports System.Data.SqlClient
Imports DFT.Dotnetnuke.ClassLibrary
Imports Microsoft.ApplicationBlocks.Data
Imports Telerik.Web.UI

Partial Public Class frmInvoice
    Inherits System.Web.UI.Page
    Dim strRegConn As String = ConfigurationManager.ConnectionStrings("RegisBackConnection").ConnectionString
    Dim strEDIConn As String = ConfigurationManager.ConnectionStrings("OriginConnection").ConnectionString
    Dim objConn As SqlConnection = Nothing
    Dim myReader As SqlDataReader = Nothing

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        txtInvoice_No.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" + btnSave.UniqueID + "').click();return false;}} else {return true}; ")
        txtTariff_Code.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" + imbSearch.UniqueID + "').click();return false;}} else {return true}; ")

        If Not Page.IsPostBack Then
            lblCOUNTRY_CODE.Text = CommonUtility.Get_StringValue(Request.QueryString("country_code"))

            lblINVH_RUN_AUTO.Text = CommonUtility.Get_StringValue(Session("ssInvh_Run_Auto"))
            lblCompany_Taxno.Text = CommonUtility.Get_StringValue(Session("ssCompanyTaxno"))
            lblSite_ID.Text = CommonUtility.Get_StringValue(Session("ssRoleName"))
            lblFormType.Text = CommonUtility.Get_StringValue(Session("ssForm_Type"))

            Call InitialDDL()

            txtInvoice_No.Focus()
            tblData.Visible = False
        End If
    End Sub

    Function LoadInvoice() As SqlDataReader
        Try
            Dim strCommand As String
            objConn = New SqlConnection(strEDIConn)
            strCommand = "select * from INVOICE_MANUAL where COMPANY_TAXNO='" & lblCompany_Taxno.Text & "' and SITE_ID='" & lblSite_ID.Text & "' and INVH_RUN_AUTO='" & lblINVH_RUN_AUTO.Text & "'"
            myReader = SqlHelper.ExecuteReader(objConn, CommandType.Text, strCommand)
            Return myReader
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Function

    Private Sub InitialDDL()
        Try
            For i As Integer = 0 To Now.Year - 2005
                ddlYear.Items.Insert(i, New RadComboBoxItem(Now.Year - i, Now.Year - i))
            Next
            ddlYear.SelectedValue = Now.Year
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub rgInvoiceList_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles rgInvoiceList.DataBound
        myReader.Close()
        objConn.Close()
    End Sub

    Private Sub rgInvoiceList_ItemCommand(ByVal source As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles rgInvoiceList.ItemCommand
        'sp_common_form_manual_deleteInvoice '2009','3101030426','ST-001','20091218-000001','456POL','10.3.16.4 (EXP-IMP110)'
        Dim Invoice_no As String
        Dim Invoice_Year As String
        If e.CommandName = "ImageDelete" Then
            Invoice_no = e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("invoice_no")
            Invoice_Year = e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("edi_year")

            Dim ClientIP As String = GetClientIP()
            Dim ComputerName As String = GetComputerName()
            Dim ds As New DataSet
            ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, "sp_common_form_manual_deleteInvoice", _
            New SqlParameter("@EDI_YEAR", CommonUtility.Get_String(Invoice_Year)), _
            New SqlParameter("@COMPANY_TAXNO", CommonUtility.Get_StringValue(lblCompany_Taxno.Text)), _
            New SqlParameter("@SITE_ID", CommonUtility.Get_StringValue(lblSite_ID.Text)), _
            New SqlParameter("@INVH_RUN_AUTO", CommonUtility.Get_StringValue(lblINVH_RUN_AUTO.Text)), _
            New SqlParameter("@INVOICE_NO", CommonUtility.Get_String(Invoice_no)), _
            New SqlParameter("@CANCEL_BY", CommonUtility.Get_StringValue(ClientIP & " [" & ComputerName & "]")))

            If ds.Tables(0).Rows.Count > 0 Then
                If CommonUtility.Get_Int32(ds.Tables(0).Rows(0).Item("retSTATUS")) = 0 Then
                    rgInvoiceList.DataSource = LoadInvoice()
                    rgInvoiceList.DataBind()
                Else
                    Me.RadAjaxManager1.ResponseScripts.Add("window.alert('เกิดข้อผิดพลาด กรุณาติดต่อผู้ดูแลระบบ !!!');")
                End If
            End If
        End If
    End Sub

    Private Sub rgInvoiceList_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgInvoiceList.ItemDataBound
        If Not (e.Item.FindControl("imbDelete") Is Nothing) Then
            CType(e.Item.FindControl("imbDelete"), ImageButton).Attributes("onmouseover") = "this.style.cursor='hand';"
            CType(e.Item.FindControl("imbDelete"), ImageButton).Attributes.Add("onClick", "return confirm('ต้องการลบ Invoice เลขที่นี้หรือไม่');")
        End If
    End Sub

    Private Sub rgInvoiceList_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgInvoiceList.NeedDataSource
        rgInvoiceList.DataSource = LoadInvoice()
    End Sub

    Protected Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            Dim ds As New DataSet
            ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, "sp_common_form_manual_addInvoice", _
            New SqlParameter("@COMPANY_TAXNO", CommonUtility.Get_StringValue(lblCompany_Taxno.Text)), _
            New SqlParameter("@SITE_ID", CommonUtility.Get_StringValue(lblSite_ID.Text)), _
            New SqlParameter("@INVH_RUN_AUTO", CommonUtility.Get_StringValue(lblINVH_RUN_AUTO.Text)), _
            New SqlParameter("@INVOICE_NO", CommonUtility.Get_StringValue(txtInvoice_No.Text)), _
            New SqlParameter("@INVOICE_YEAR", CommonUtility.Get_StringValue(ddlYear.SelectedValue)))

            If ds.Tables(0).Rows.Count > 0 Then
                If CommonUtility.Get_Int32(ds.Tables(0).Rows(0).Item("retSTATUS")) = -1 Then
                    Me.RadAjaxManager1.ResponseScripts.Add("window.alert('" & CommonUtility.Get_StringValue(ds.Tables(0).Rows(0).Item("retMsg")) & "');")
                Else
                    rgInvoiceList.DataSource = LoadInvoice()
                    rgInvoiceList.DataBind()
                End If
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Function GetClientIP() As String
        Return CommonUtility.Get_StringValue(Request.UserHostName)
    End Function

    Function GetComputerName() As String
        Try
            Return CommonUtility.Get_StringValue(System.Net.Dns.GetHostByAddress(GetClientIP).HostName)
        Catch ex As Exception
            Return CommonUtility.Get_StringValue(Request.ServerVariables("REMOTE_HOST"))
        End Try
    End Function

    Protected Sub imbSearch_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imbSearch.Click
        Try
            'sp_common_form_manual_CheckRover '3101030426','FORM1','NO','6402'
            Dim ds As New DataSet
            ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, "sp_common_form_manual_CheckRover", _
            New SqlParameter("@COMPANY_TAXNO", CommonUtility.Get_StringValue(lblCompany_Taxno.Text)), _
            New SqlParameter("@form_type", CommonUtility.Get_StringValue(lblFormType.Text)), _
            New SqlParameter("@country_code", CommonUtility.Get_String(lblCOUNTRY_CODE.Text)), _
            New SqlParameter("@TARIFF_CODE", CommonUtility.Get_StringValue(txtTariff_Code.Text.Trim())))

            rgCerOfOrigin.DataSource = ds.Tables(0)
            rgCerOfOrigin.DataBind()

            If rgCerOfOrigin.MasterTableView.Items.Count = 0 Then
                Me.RadAjaxManager1.ResponseScripts.Add("window.alert('ไม่พบข้อมูลต้นทุนของพิกัดที่ทำการค้นหา !!!');")
            Else
                tblData.Visible = True
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub
End Class