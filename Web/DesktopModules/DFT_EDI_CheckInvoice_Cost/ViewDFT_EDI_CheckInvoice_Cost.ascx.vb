

Imports System.Web.UI
Imports System.Collections.Generic
Imports System.Reflection

Imports DotNetNuke
Imports DotNetNuke.Services.Exceptions
Imports DotNetNuke.Services.Localization
Imports System.Data
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data


Namespace YourCompany.Modules.DFT_EDI_CheckInvoice_Cost

    Partial Class ViewDFT_EDI_CheckInvoice_Cost
        Inherits Entities.Modules.PortalModuleBase
        Implements Entities.Modules.IActionable

        Dim StrconnEDI As String = ConfigurationManager.ConnectionStrings("EDIConnection").ConnectionString
        Dim StrconnRollver As String = ConfigurationManager.ConnectionStrings("RollverConnection").ConnectionString

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            If Not Page.IsPostBack Then
                LoadInvoiceYear()
                rcbInvoiceYear.SelectedValue = Now.Year
            End If
        End Sub

        Function SearchCost()
            Try
                Dim Strcommand As String = ""
                Dim ds As New DataSet
                Strcommand = " SELECT transfer_date, tax_id, country, harmonized_no, certoforigin_no, Convert(Varchar(10),certoforigin_date,103) AS certoforigin_date," & _
                " company_name_th, company_name_en, goods_desc_th, goods_desc_en, models, country_code " & _
                " FROM tbl_certoforigin WHERE (tax_id = '" & txtCompanyTaxNo_Cost.Text.Trim & "') AND (harmonized_no LIKE '" & txtTariff.Text.Trim & "' + '%') "

                ds = SqlHelper.ExecuteDataset(StrconnRollver, CommandType.Text, Strcommand)

                Return ds.Tables(0)

            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End Function

        Function SearchInvoice()
            Try
                Dim ds As New DataSet
                Dim Strcommand As String = ""
                Strcommand = " SELECT C.company_eng, INV.company_taxno, INV.invoice_no, INV.edi_year, H.invh_run_auto " & _
                " FROM dbo.company AS C INNER JOIN dbo.invoice_EDI AS INV ON C.company_taxno = INV.company_taxno INNER JOIN " & _
                " dbo.form_header_edi AS H ON INV.company_taxno = H.company_taxno AND INV.invoice_no = H.invoice_no1 " & _
                " WHERE (INV.active_flag = 'Y') AND (INV.edi_year = @edi_year) AND (INV.company_taxno = @company_taxno) AND (INV.invoice_no LIKE @invoice_no + '%') "

                Dim npm(2) As SqlClient.SqlParameter
                npm(0) = New SqlClient.SqlParameter("@company_taxno", txtCompanyTaxNo_Invoice.Text.Trim)
                npm(1) = New SqlClient.SqlParameter("@invoice_no", txtInvoice_no.Text)
                npm(2) = New SqlClient.SqlParameter("@edi_year", rcbInvoiceYear.SelectedValue.ToString)

                ds = SqlHelper.ExecuteDataset(StrconnEDI, CommandType.Text, Strcommand, npm)
                Return ds.Tables(0)

            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End Function

        Sub LoadInvoiceYear()
            Try
                Dim ds As New DataSet
                Dim Strcommand As String = ""
                Strcommand = " SELECT DISTINCT edi_year FROM invoice_EDI "
                ds = SqlHelper.ExecuteDataset(StrconnEDI, CommandType.Text, Strcommand)

                rcbInvoiceYear.DataSource = ds.Tables(0)
                rcbInvoiceYear.DataTextField = "edi_year"
                rcbInvoiceYear.DataValueField = "edi_year"
                rcbInvoiceYear.DataBind()
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End Sub
#Region "Optional Interfaces"

        ''' -----------------------------------------------------------------------------
        ''' <summary>
        ''' Registers the module actions required for interfacing with the portal framework
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' </history>
        ''' -----------------------------------------------------------------------------
        Public ReadOnly Property ModuleActions() As Entities.Modules.Actions.ModuleActionCollection Implements Entities.Modules.IActionable.ModuleActions
            Get
                Dim Actions As New Entities.Modules.Actions.ModuleActionCollection
                Actions.Add(GetNextActionID, Localization.GetString(Entities.Modules.Actions.ModuleActionType.AddContent, LocalResourceFile), Entities.Modules.Actions.ModuleActionType.AddContent, "", "", EditUrl(), False, Security.SecurityAccessLevel.Edit, True, False)
                Return Actions
            End Get
        End Property

#End Region

        Private Sub rgCost_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgCost.NeedDataSource
            rgCost.DataSource = SearchCost()
        End Sub

        Private Sub rgInvoice_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgInvoice.NeedDataSource
            rgInvoice.DataSource = SearchInvoice()
        End Sub

        Private Sub btnSearchCost_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearchCost.Click
            If txtCompanyTaxNo_Cost.Text = "" Or txtTariff.Text = "" Then
                RadAjaxManager1.Alert("กรุณาป้อนเลขประจำตัวผู้เสียภาษีและพิกัดสินค้า !!!")
                Exit Sub
            Else
                rgCost.DataSource = SearchCost()
                rgCost.Rebind()
            End If
        End Sub

        Private Sub btnSearchInvoice_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearchInvoice.Click
            If txtCompanyTaxNo_Invoice.Text = "" Or txtInvoice_no.Text = "" Or rcbInvoiceYear.SelectedValue = Nothing Or rcbInvoiceYear.SelectedValue = "" Then
                RadAjaxManager1.Alert("กรุณาป้อนเลขประจำตัวผู้เสียภาษี , Invoice , ปี !!!")
                Exit Sub
            Else
                rgInvoice.DataSource = SearchInvoice()
                rgInvoice.Rebind()
            End If
        End Sub
    End Class

End Namespace
