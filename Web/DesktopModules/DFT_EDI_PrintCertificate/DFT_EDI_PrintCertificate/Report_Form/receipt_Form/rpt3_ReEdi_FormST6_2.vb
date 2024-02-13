Imports DataDynamics.ActiveReports
Imports DataDynamics.ActiveReports.Document

Imports Microsoft.ApplicationBlocks.Data
Imports System.Data.SqlClient
Imports System.Data
Imports System.Text
Imports DFT.Dotnetnuke.ClassLibrary
Imports ReportPrintClass
Public Class rpt3_ReEdi_FormST6_2 


    Private Sub PageHeader1_Format(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PageHeader1.Format
        txtTemp_REFERENCE_CODE2.Text = "I" & txtREFERENCE_CODE2.Value

        txtcompany_nameth.Text = txtcompany_name.Value
        txtcompany_nameeng.Text = txtcompany_name.Value

        txtTemp_address.Text = txtcompany_Address.Value & " " & txtcompany_Province.Value & " " & txtcompany_Country.Value
        txtTemp_address1.Text = txtcompany_Address.Value & " " & txtcompany_Province.Value & " " & txtcompany_Country.Value

        txtTemp_company_phone.Text = txtcompany_phone.Value
        txtTemp_company_phone1.Text = txtcompany_phone.Value

        txtTemp_company_fax.Text = txtcompany_fax.Value
        txtTemp_company_fax1.Text = txtcompany_fax.Value

        txtTemp_invoice.Text = txtinvoice_no1.Value & " " & txtinvoice_no2.Value & " " & txtinvoice_no3.Value & " " & txtinvoice_no4.Value & " " & txtinvoice_no5.Value

        txtTemp_dest.Text = txtdestination_address.Value & " " & txtdestination_province.Value & " " & txtdest_receive_country.Value

        txtTemp_request_person1.Text = txtrequest_person.Value
        txtTemp_request_person2.Text = txtrequest_person.Value



    End Sub

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        Me.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.A4

        ' Add any initialization after the InitializeComponent() call.

    End Sub
End Class
