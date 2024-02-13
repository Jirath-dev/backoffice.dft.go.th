Imports DataDynamics.ActiveReports 
Imports DataDynamics.ActiveReports.Document 
Imports Microsoft.ApplicationBlocks.Data
Imports System.Data.SqlClient
Imports System.Data
Imports System.Text
Imports DFT.Dotnetnuke.ClassLibrary
Imports ReportPrintClass
Public Class rpt3_ReEdi_FormST6_1 

    Private Sub PageHeader1_Format(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PageHeader1.Format
        txtTemp_REFERENCE_CODE2.Text = "I" & txtREFERENCE_CODE2.Value

        txtcompany_nameth.Text = CommonUtility.Get_StringValue(txtcompany_name.Value)
        txtcompany_nameeng.Text = CommonUtility.Get_StringValue(txtcompany_name.Value)

        txtTemp_address.Text = CommonUtility.Get_StringValue(txtcompany_Address.Value) & " " & CommonUtility.Get_StringValue(txtcompany_Province.Value) & " " & CommonUtility.Get_StringValue(txtcompany_Country.Value)
        txtTemp_address1.Text = CommonUtility.Get_StringValue(txtcompany_Address.Value) & " " & CommonUtility.Get_StringValue(txtcompany_Province.Value) & " " & CommonUtility.Get_StringValue(txtcompany_Country.Value)

        txtTemp_company_phone.Text = CommonUtility.Get_StringValue(txtcompany_phone.Value)
        txtTemp_company_phone1.Text = CommonUtility.Get_StringValue(txtcompany_phone.Value)

        txtTemp_company_fax.Text = CommonUtility.Get_StringValue(txtcompany_fax.Value)
        txtTemp_company_fax1.Text = CommonUtility.Get_StringValue(txtcompany_fax.Value)

       

        txtTemp_invoice.Text = IIf(Not txtinvoice_no1.Text = String.Empty = True, txtinvoice_no1.Value, "") & _
                IIf(Not txtinvoice_no2.Text = String.Empty = True, " " & txtinvoice_no2.Value, "") & _
                IIf(Not txtinvoice_no3.Text = String.Empty = True, " " & txtinvoice_no3.Value, "") & _
                IIf(Not txtinvoice_no4.Text = String.Empty = True, " " & txtinvoice_no4.Value, "") & _
                IIf(Not txtinvoice_no5.Text = String.Empty = True, " " & txtinvoice_no5.Value, "")

        txtTemp_dest.Text = CommonUtility.Get_StringValue(txtdestination_address.Value) & " " & CommonUtility.Get_StringValue(txtdestination_province.Value) & " " & CommonUtility.Get_StringValue(txtdest_receive_country.Value)

        txtTemp_request_person1.Text = CommonUtility.Get_StringValue(txtrequest_person.Value)
        txtTemp_request_person2.Text = CommonUtility.Get_StringValue(txtrequest_person.Value)

        Q1Q5()
    End Sub

    Sub Q1Q5()
        'check เพื่อแสดงค่า Q1(USD) หรือ Q5(ไม่ใช่ USD)
        Select Case txtcurrency_code.Text
            Case "USD"
                txtTemp_Q1Q5.Text = Format(txtQuantity1.Value, "#,##0.00##")
            Case Else
                txtTemp_Q1Q5.Text = Format(txtQuantity5.Value, "#,##0.00##")
        End Select
    End Sub
    
    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

End Class
