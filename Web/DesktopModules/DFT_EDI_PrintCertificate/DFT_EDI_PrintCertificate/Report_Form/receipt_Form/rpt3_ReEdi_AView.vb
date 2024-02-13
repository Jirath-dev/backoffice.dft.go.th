Imports DataDynamics.ActiveReports
Imports DataDynamics.ActiveReports.Document

Imports Microsoft.ApplicationBlocks.Data
Imports System.Data.SqlClient
Imports System.Data
Imports System.Text
Imports DFT.Dotnetnuke.ClassLibrary
Public Class rpt3_ReEdi_AView
    Dim C_num As Integer
    Dim i As Integer
    Private Sub Detail1_Format(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Detail1.Format
        C_num += 1
        txtNum_Product.Text = C_num
        '===================================================
        txtTARIFF_CODE_Temp.Text = Mid(txtTARIFF_CODE.Text, 1, 4) + "." + Mid(txtTARIFF_CODE.Text, 5, 2)
        txtNET_WEIGHT_unit_code2.Text = txtNET_WEIGHT.Text + " " + txtunit_code2.Text
        txtFOB_AMT_Temp.Text = txtFOB_AMT.Text

        '===================================================
    End Sub

    Private Sub PageHeader1_Format(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PageHeader1.Format
        '===============txtForm_Name_Temp===================================
        txtForm_Name_Temp.Text = "������ҧ�Ӣ�˹ѧ����Ѻ�ͧ��蹡��Դ�Թ���Ẻ" + txtForm_Name.Text

        '===============txtREFERENCE_CODE2_Temp===================================
        txtREFERENCE_CODE2_Temp.Text = "I" + txtREFERENCE_CODE2.Text

        '============txtcard_id_Temp=txtcompany_name=====================================
        txtcard_id_Temp.Text = "�ӹҨ��Ţ���  " + txtcard_id.Text + "   �ͧ����ѷ/��ҧ/��ҹ  " + txtcompany_name.Text

        '===========txtcompany_taxno_Temp=======================================
        txtcompany_taxno_Temp.Text = "�Ţ��Шӵ�Ǽ�����������ҡ�   " + txtcompany_taxno.Text

        '===============txtcompany_address_Temp===================================
        txtcompany_address_Temp.Text = "��������Ţ���  " + txtcompany_address.Text

        '===============txtcompany_phone_fax_Temp===================================
        If txtcompany_phone.Text <> "" And txtcompany_fax.Text <> "" Then
            txtcompany_phone_fax_Temp.Text = "���Ѿ��  " + txtcompany_phone.Text + "   �����  " + txtcompany_fax.Text
        ElseIf txtcompany_phone.Text <> "" And txtcompany_fax.Text = "" Then
            txtcompany_phone_fax_Temp.Text = "���Ѿ��  " + txtcompany_phone.Text
        ElseIf txtcompany_phone.Text = "" And txtcompany_fax.Text <> "" Then
            txtcompany_phone_fax_Temp.Text = "�����  " + txtcompany_fax.Text
        End If

        '================txtdestination_address_province_dest_receive_country==================================
        If CommonUtility.Get_StringValue(txtdestination_phone.Text) <> "" And CommonUtility.Get_StringValue(txtdestination_fax.Text) <> "" Then
            txtdestination_address_province_dest_receive_country.Text = txtdestination_address.Text + "  " + txtdestination_province.Text + "  " + txtdest_receive_country.Text _
            + "���Ѿ�� " + txtdestination_phone.Text + "  ����� " + txtdestination_fax.Text

        ElseIf CommonUtility.Get_StringValue(txtdestination_phone.Text) <> "" And CommonUtility.Get_StringValue(txtdestination_fax.Text) = "" Then
            txtdestination_address_province_dest_receive_country.Text = txtdestination_address.Text + "  " + txtdestination_province.Text + "  " + txtdest_receive_country.Text _
                        + "���Ѿ�� " + txtdestination_phone.Text

        ElseIf CommonUtility.Get_StringValue(txtdestination_phone.Text) = "" And CommonUtility.Get_StringValue(txtdestination_fax.Text) <> "" Then
            txtdestination_address_province_dest_receive_country.Text = txtdestination_address.Text + "  " + txtdestination_province.Text + "  " + txtdest_receive_country.Text _
            + "  ����� " + txtdestination_fax.Text

        ElseIf CommonUtility.Get_StringValue(txtdestination_phone.Text) = "" And CommonUtility.Get_StringValue(txtdestination_fax.Text) = "" Then
            txtdestination_address_province_dest_receive_country.Text = txtdestination_address.Text + "  " + txtdestination_province.Text + "  " + txtdest_receive_country.Text + "���Ѿ�� " + txtdestination_phone.Text + "  ����� " + txtdestination_fax.Text

        Else
            txtdestination_address_province_dest_receive_country.Text = txtdestination_address.Text + "  " + txtdestination_province.Text + "  " + txtdest_receive_country.Text + "���Ѿ�� " + txtdestination_phone.Text + "  ����� " + txtdestination_fax.Text
        End If

        '===============txtimport_country_Temp===================================
        txtimport_country_Temp.Text = "����Ȼ��·ҧ  " + txtimport_country.Text

        '==================txtship_by================================
        Select Case txtship_by.Text
            Case "0"
                txtship_by0.Text = "X"
            Case "1"
                txtship_by1.Text = "X"
            Case "2"
                txtship_by2.Text = "X"
            Case "3"
                txtship_by3.Text = "X"
            Case "4"
                txtship_by4.Text = "X"
        End Select
        '==================================================
    End Sub

    Private Sub PageFooter1_Format(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PageFooter1.Format

    End Sub

    Private Sub GroupFooter1_Format(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GroupFooter1.Format

        '==================================================
        txtTotal.Text = "�ʹ���       GROSS WEIGHT = " + txtSum_Gross_Weight.Text

        '===============txtbill_type===================================
        Select Case txtbill_type.Text
            Case "0"
                txtbill_type0.Text = "X"
            Case "1"
                txtbill_type1.Text = "X"
            Case "2"
                txtbill_type2.Text = "X"
            Case "3"
                txtbill_type3.Text = "X"
        End Select
        '==============txtfactory_Temp====================================
        'txtfactory_Temp.Text = txtfactory.Text + " ��������� " + txtfactory_address.Text
        '==============txtrequest_person_2====================================
        txtrequest_person_2.Text = "( " + txtrequest_person.Text + " )    ����Ѻ�ͺ"
    End Sub
End Class
