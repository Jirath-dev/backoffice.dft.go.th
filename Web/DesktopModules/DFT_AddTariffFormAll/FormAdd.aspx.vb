Imports System.Web.UI
Imports System.Collections.Generic
Imports System.Reflection

Imports DotNetNuke
Imports DotNetNuke.Services.Exceptions
Imports DotNetNuke.Services.Localization

Imports Microsoft.ApplicationBlocks.Data
Imports System.Data.SqlClient
Imports DFT.Dotnetnuke.ClassLibrary
Imports Telerik.Web.UI

Partial Public Class FormAdd
    'Inherits Entities.Modules.PortalModuleBase
    Inherits System.Web.UI.Page
    Dim strConn As String = ConfigurationManager.ConnectionStrings("OriginConnection").ConnectionString
    Dim objConn As SqlConnection = Nothing
    Dim reader As SqlDataReader = Nothing
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        txtNumTariff.Focus()
        If Not Page.IsPostBack Then
            'lblFormHeader.Text = "ค้นหาพิกัดศุลกากร"

            txtTemp_tariff_code.Text = CommonUtility.Get_StringValue(Request.QueryString("Itariff_code"))
            txtTemp_country_code.Text = CommonUtility.Get_StringValue(Request.QueryString("Icountry_code"))
            txtTemp_form.Text = CommonUtility.Get_StringValue(Request.QueryString("selectform"))
            txtTemp_CAT.Text = CommonUtility.Get_StringValue(Request.QueryString("Icat"))

            objConn = New SqlConnection(strConn)
            Dim dr As SqlDataReader
            dr = SqlHelper.ExecuteReader(strConn, CommandType.StoredProcedure, "sp_common_get_countryByFormTypeForm1_NewDS", _
             New SqlParameter("@FORM_TYPE", CommonUtility.Get_StringValue("FORM1")))
            If dr.HasRows() Then
                DDLcountry.DataSource = dr
                DDLcountry.DataBind()

                txtCodecountry.Text = DDLcountry.SelectedValue
            End If
            If Request.QueryString("action") = "new" Then
                Page.Title = "เพิ่มพิกัดศุลกากร"
                lblHeader.Text = "เพิ่มพิกัดศุลกากร"

                btnSave.Visible = False
                ChecktariffBox(CommonUtility.Get_StringValue(DDLListForm.SelectedValue))

            ElseIf Request.QueryString("action") = "view" Then

                SetForm(txtTemp_tariff_code.Text, txtTemp_country_code.Text, CommonUtility.Get_StringValue(txtTemp_form.Text), CommonUtility.Get_StringValue(txtTemp_CAT.Text))

                Page.Title = "แสดงพิกัดศุลกากร"
                lblHeader.Text = "แสดงพิกัดศุลกากร"

                btnAdd.Visible = False
                btnSave.Visible = False
                ChecktariffBox(CommonUtility.Get_StringValue(txtTemp_form.Text))
                txtCAT.Enabled = False
                txtCAT.BackColor = Drawing.Color.Gainsboro

            ElseIf Request.QueryString("action") = "edit" Then
                SetForm(txtTemp_tariff_code.Text, txtTemp_country_code.Text, CommonUtility.Get_StringValue(txtTemp_form.Text), CommonUtility.Get_StringValue(txtTemp_CAT.Text))

                Page.Title = "แก้ไขพิกัดศุลกากร"
                lblHeader.Text = "แก้ไขพิกัดศุลกากร"

                btnAdd.Visible = False

                txtNumTariff.Enabled = False
                txtNumTariff.BackColor = Drawing.Color.Gainsboro
                ChecktariffBox(CommonUtility.Get_StringValue(txtTemp_form.Text))
                txtCAT.Enabled = False
                txtCAT.BackColor = Drawing.Color.Gainsboro
                DDLcountry.Enabled = False
                DDLListForm.Enabled = False
            End If
        End If
        pageTitle()
    End Sub
    Sub pageTitle()
        If Request.QueryString("action") = "new" Then
            Page.Title = "เพิ่มพิกัดศุลกากร"
            lblHeader.Text = "เพิ่มพิกัดศุลกากร"
        ElseIf Request.QueryString("action") = "view" Then
            Page.Title = "แสดงพิกัดศุลกากร"
            lblHeader.Text = "แสดงพิกัดศุลกากร"
        ElseIf Request.QueryString("action") = "edit" Then
            Page.Title = "แก้ไขพิกัดศุลกากร"
            lblHeader.Text = "แก้ไขพิกัดศุลกากร"
        End If
    End Sub

    Sub ChecktariffBox(ByVal _strForm As String)
        Select Case _strForm
            Case "FORM2_1"
                txtCAT.Enabled = True
                txtCAT.BackColor = Drawing.Color.White
                txtrate_desc.Enabled = False
                txtrate_desc.BackColor = Drawing.Color.Gainsboro

                DDLcountry.Enabled = False
            Case "FORM3", "FORM3_1", "FORM4", "FORM4_2", "FORM4_3", "FORM4_4", "FORM4_5", "FORM4_6", "FORM4_8", "FORM4_9", "FORM5", "FORM5_1", "FORM5_2", _
            "FORM6", "FORM7", "FORM8", "FORM9", "FORM44_4", "FORM44_41", "FORM44_44", "FORM44", "FORM441_4", "FORM441", "FORM4_61", "FORM4_81"
                txtCAT.Enabled = False
                txtCAT.BackColor = Drawing.Color.Gainsboro
                txtrate_desc.Enabled = False
                txtrate_desc.BackColor = Drawing.Color.Gainsboro

                DDLcountry.Enabled = True
            Case Else
                txtCAT.Enabled = False
                txtCAT.BackColor = Drawing.Color.Gainsboro
                txtrate_desc.Enabled = True
                txtrate_desc.BackColor = Drawing.Color.White

                DDLcountry.Enabled = True
        End Select
    End Sub

    Protected Sub DDLListForm_SelectedIndexChanged(ByVal o As System.Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles DDLListForm.SelectedIndexChanged
        txtNumTariff.Text = ""
        objConn = New SqlConnection(strConn)
        Dim dr As SqlDataReader

        dr = SqlHelper.ExecuteReader(strConn, CommandType.StoredProcedure, Form_selectTariff(CommonUtility.Get_StringValue(DDLListForm.SelectedItem.Value)), _
         New SqlParameter("@FORM_TYPE", CommonUtility.Get_StringValue(DDLListForm.SelectedItem.Value)))

        If dr.HasRows() Then
            DDLcountry.DataSource = dr
            DDLcountry.DataBind()

            txtCodecountry.Text = DDLcountry.SelectedValue
        End If
        ChecktariffBox(CommonUtility.Get_StringValue(DDLListForm.SelectedValue))

    End Sub

    Protected Sub DDLcountry_SelectedIndexChanged(ByVal o As System.Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles DDLcountry.SelectedIndexChanged
        txtCodecountry.Text = DDLcountry.SelectedValue
    End Sub
    'หาประเทศของแต่ละฟอร์ม
    Function Form_selectTariff(ByVal Form_) As String
        Dim str_store As String
        Select Case Form_
            Case "FORM1"
                str_store = "sp_common_get_countryByFormTypeForm1_NewDS"
            Case "FORM1_1"
                str_store = "sp_common_get_countryByFormTypeFORM1_1_NewDS"
            Case "FORM1_2"
                str_store = "sp_common_get_countryByFormTypeFORM1_1_NewDS"
            Case "FORM1_3"
                str_store = "sp_common_get_countryByFormTypeFORM1_3_NewDS"
            Case "FORM1_4"
                str_store = "sp_common_get_countryByFormTypeFORM1_4_NewDS"
            Case "FORM2"
                str_store = "sp_common_get_countryByFormTypeFORM2_NewDS"
            Case "FORM2_1"
                str_store = "sp_common_get_countryByFormTypeFORM2_1_NewDS"
            Case "FORM2_2"
                str_store = "sp_common_get_countryByFormTypeFORM2_2_NewDS"
            Case "FORM2_3"
                str_store = "sp_common_get_countryByFormTypeFORM2_3_NewDS"
            Case "FORM2_4"
                str_store = "sp_common_get_countryByFormTypeFORM2_4_NewDS"
            Case "FORM3"
                str_store = "sp_common_get_countryByFormType_NewDS"
            Case "FORM3_1"
                str_store = "sp_common_get_countryByFormType_NewDS"

                'By Tine
            Case "FORM44_4"
                str_store = "sp_common_get_countryByFormTypeFORM44_4_NewDS"
            Case "FORM44_41"
                str_store = "sp_common_get_countryByFormTypeFORM44_41_NewDS"
            Case "FORM44_44"
                str_store = "sp_common_get_countryByFormTypeForm44_44_NewDS"
            Case "FORM44"
                str_store = "sp_common_get_countryByFormTypeForm44_NewDS"
            Case "FORM441_4"
                str_store = "sp_common_get_countryByFormTypeFORM441_4_NewDS"
            Case "FORM441"
                str_store = "sp_common_get_countryByFormTypeForm441_NewDS"

            Case "FORM4"
                str_store = "sp_common_get_countryByFormTypeFORM4_NewDS"
            Case "FORM4_1"
                str_store = "sp_common_get_countryByFormTypeForm4_1_NewDS"
            Case "FORM4_2"
                str_store = "sp_common_get_countryByFormTypeForm4_2_NewDS"
            Case "FORM4_3"
                str_store = "sp_common_get_countryByFormType_NewDS"
            Case "FORM4_4"
                str_store = "sp_common_get_countryByFormType_NewDS"
            Case "FORM4_5"
                str_store = "sp_common_get_countryByFormType_NewDS"
            Case "FORM4_6"
                str_store = "sp_common_get_countryByFormTypeForm4_6_NewDS"

                'By Tine
            Case "FORM4_61"
                str_store = "sp_common_get_countryByFormTypeForm4_61_NewDS"

            Case "FORM4_8"
                str_store = "sp_common_get_countryByFormTypeForm4_8_NewDS"

                'By Tine
            Case "FORM4_81"
                str_store = "sp_common_get_countryByFormTypeForm4_81_NewDS"

            Case "FORM4_9"
                str_store = "sp_common_get_countryByFormTypeForm4_9_NewDS"
            Case "FORM4_91"
                str_store = "sp_common_get_countryByFormTypeForm4_91_NewDS"
            Case "FORM5"
                str_store = "sp_common_get_countryByFormTypeFORM5_NewDS"
                'by rut
            Case "FORM5_1"
                str_store = "sp_common_get_countryByFormType_NewDS"

                ''ByTine 28-10-2558 เพิ่มฟอร์มใหม่ไทย-ชิลี
            Case "FORM5_2"
                str_store = "sp_common_get_countryByFormTypeForm5_2_NewDS"

            Case "FORM6"
                str_store = "sp_common_get_countryByFormTypeFORM6_NewDS"
            Case "FORM7"
                str_store = "sp_common_get_countryByFormTypeFORM7_NewDS"
            Case "FORM8"
                str_store = "sp_common_get_countryByFormTypeFORM8_NewDS"
            Case "FORM9"
                str_store = "sp_common_get_countryByFormTypeFORM9_NewDS"
            Case "FORMRussia"
                str_store = "sp_common_get_countryByFormTypeFORMRussia_NewDS"
        End Select
        Return str_store
    End Function

    Sub setCountry(ByVal _strForm As String)
        Dim dr As SqlDataReader
        dr = SqlHelper.ExecuteReader(strConn, CommandType.StoredProcedure, Form_selectTariff(CommonUtility.Get_StringValue(_strForm)), _
         New SqlParameter("@FORM_TYPE", _strForm))
        If dr.HasRows() Then
            DDLcountry.DataSource = dr
            DDLcountry.DataBind()


        End If
    End Sub

    Private Sub SetForm(ByVal _tariff_code As String, ByVal _country_code As String, ByVal _form As String, ByVal _cat As String)
        Try
            Dim m_objReader As SqlDataReader
            Dim store_form As String = ""

            'store ใช้เรียกทุกฟอร์ม
            m_objReader = SqlHelper.ExecuteReader(strConn, CommandType.StoredProcedure, "vi_common_form_edi_CheckAllTariff_NewDS", _
            New SqlParameter("@tariff_code", _tariff_code), _
            New SqlParameter("@country_code", _country_code), _
            New SqlParameter("@FORM_TYPE", _form), New SqlParameter("@CAT", _cat))

            If m_objReader.Read() Then
                setCountry(CommonUtility.Get_StringValue(_form))
                DDLListForm.SelectedValue = CommonUtility.Get_StringValue(_form)
                DDLcountry.SelectedValue = CommonUtility.Get_StringValue(m_objReader("country_code"))
                txtCodecountry.Text = CommonUtility.Get_StringValue(m_objReader("country_code"))

                txtNumTariff.Text = CommonUtility.Get_StringValue(m_objReader("tariff_code"))

                txtcheck_digit.Text = CommonUtility.Get_StringValue(m_objReader("check_digit"))

                'กำหนดให้แสดงเฉพาะฟอร์ม FORM2_1
                Select Case CommonUtility.Get_StringValue(_form)
                    Case "FORM2_1"
                        txtCAT.Text = CommonUtility.Get_StringValue(m_objReader("CAT"))
                        txttariff_name.Text = CommonUtility.Get_StringValue(m_objReader("tariff_name"))
                    Case "FORM3", "FORM3_1", "FORM4", "FORM4_2", "FORM4_3", "FORM4_4", "FORM4_5", "FORM4_6", "FORM4_8", "FORM4_9", "FORM5", "FORM5_1", "FORM5_2", _
            "FORM6", "FORM7", "FORM8", "FORM9", "FORM44_4", "FORM44_41", "FORM44_44", "FORM44", "FORM441_4", "FORM441", "FORM4_61", "FORM4_81"
                        txttariff_name.Text = CommonUtility.Get_StringValue(m_objReader("tariff_name"))
                    Case Else
                        txtrate_desc.Text = CommonUtility.Get_StringValue(m_objReader("rate_desc"))
                        txttariff_name.Text = CommonUtility.Get_StringValue(m_objReader("tariff_name"))
                End Select

            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        ClientScript.RegisterStartupScript(Page.GetType(), "mykey", "CancelEdit();", True)
    End Sub

    Function selectAddStoreForm() As String
        Dim _strFormStore As String
        Select Case CommonUtility.Get_StringValue(DDLListForm.SelectedValue)
            Case "FORM1"
                _strFormStore = "vi_form1_edi_AddTariff_NewDS"
            Case "FORM1_1"
                _strFormStore = "vi_form1_edi_AddTariff_NewDS"
            Case "FORM1_2"
                _strFormStore = "vi_form1_edi_AddTariff_NewDS"
            Case "FORM1_3"
                _strFormStore = "vi_form1_edi_AddTariff_NewDS"
            Case "FORM1_4"
                _strFormStore = "vi_form1_edi_AddTariff_NewDS"
            Case "FORM1_5"
                _strFormStore = "vi_form1_edi_AddTariff_NewDS"
            Case "FORM2"
                _strFormStore = "vi_form2_edi_AddTariff_NewDS"
            Case "FORM2_1"
                _strFormStore = "vi_form2_1_edi_AddTariff_NewDS"
            Case "FORM2_2"
                _strFormStore = "vi_form2_2_edi_AddTariff_NewDS"
            Case "FORM2_3"
                _strFormStore = "vi_form2_3_edi_AddTariff_NewDS"
            Case "FORM2_4"
                _strFormStore = "vi_form2_4_edi_AddTariff_NewDS"
            Case "FORM3"
                _strFormStore = "vi_form3_edi_AddTariff_NewDS"

                'By Tine
            Case "FORM44_4"
                _strFormStore = "vi_form44_4_edi_AddTariff_NewDS"
            Case "FORM44_41"
                _strFormStore = "vi_form44_41_edi_AddTariff_NewDS"
            Case "FORM44_44"
                _strFormStore = "vi_form44_44_edi_AddTariff_NewDS"
            Case "FORM44"
                _strFormStore = "vi_form44_edi_AddTariff_NewDS"
            Case "FORM441_4"
                _strFormStore = "vi_form441_4_edi_AddTariff_NewDS"
            Case "FORM441"
                _strFormStore = "vi_form441_edi_AddTariff_NewDS"

            Case "FORM4"
                _strFormStore = "vi_form4_edi_AddTariff_NewDS"
            Case "FORM4_2"
                _strFormStore = "vi_form4_2_edi_AddTariff_NewDS"
            Case "FORM4_3"
                _strFormStore = "vi_form4_3_edi_AddTariff_NewDS"
            Case "FORM4_4"
                _strFormStore = "vi_form4_4_edi_AddTariff_NewDS"
            Case "FORM4_5"
                _strFormStore = "vi_form4_5_edi_AddTariff_NewDS"
            Case "FORM4_6"
                _strFormStore = "vi_form4_6_edi_AddTariff_NewDS"

                'By Tine
            Case "FORM4_61"
                _strFormStore = "vi_form4_61_edi_AddTariff_NewDS"

            Case "FORM4_8"
                _strFormStore = "vi_form4_8_edi_AddTariff_NewDS"

                'By Tine
            Case "FORM4_81"
                _strFormStore = "vi_form4_81_edi_AddTariff_NewDS"

            Case "FORM4_9"
                _strFormStore = "vi_form4_9_edi_AddTariff_NewDS"
            Case "FORM4_91"
                _strFormStore = "vi_form4_91_edi_AddTariff_NewDS"
            Case "FORM5"
                _strFormStore = "vi_form5_edi_AddTariff_NewDS"
                'by rut
            Case "FORM5_1"
                _strFormStore = "vi_form5_1_edi_AddTariff_NewDS"
                ''ByTine เพิ่มฟอร์มใหม่ ไทย-ชิลี
            Case "FORM5_2"
                _strFormStore = "vi_form5_2_edi_AddTariff_NewDS"

            Case "FORM6"
                _strFormStore = "vi_form6_edi_AddTariff_NewDS"
            Case "FORM7"
                _strFormStore = "vi_form7_edi_AddTariff_NewDS"
            Case "FORM8"
                _strFormStore = "vi_form8_edi_AddTariff_NewDS"
            Case "FORM9"
                _strFormStore = "vi_form9_edi_AddTariff_NewDS"
            Case "FORMRussia"
                _strFormStore = "vi_form1_edi_AddTariff_NewDS"
        End Select

        Return _strFormStore
    End Function

    Function selectUpdateStoreForm() As String
        Dim _strFormStore As String
        Select Case CommonUtility.Get_StringValue(DDLListForm.SelectedValue)
            Case "FORM1"
                _strFormStore = "vi_form1_edi_updateTariff_NewDS"
            Case "FORM1_1"
                _strFormStore = "vi_form1_edi_updateTariff_NewDS"
            Case "FORM1_2"
                _strFormStore = "vi_form1_edi_updateTariff_NewDS"
            Case "FORM1_3"
                _strFormStore = "vi_form1_edi_updateTariff_NewDS"
            Case "FORM1_4"
                _strFormStore = "vi_form1_edi_updateTariff_NewDS"
            Case "FORM1_5"
                _strFormStore = "vi_form1_edi_updateTariff_NewDS"
            Case "FORM2"
                _strFormStore = "vi_form2_edi_updateTariff_NewDS"
            Case "FORM2_1"
                _strFormStore = "vi_form2_1_edi_updateTariff_NewDS"
            Case "FORM2_2"
                _strFormStore = "vi_form2_2_edi_updateTariff_NewDS"
            Case "FORM2_3"
                _strFormStore = "vi_form2_3_edi_updateTariff_NewDS"
            Case "FORM2_4"
                _strFormStore = "vi_form2_4_edi_updateTariff_NewDS"
            Case "FORM3"
                _strFormStore = "vi_form3_edi_updateTariff_NewDS"

                'By Tine
            Case "FORM44_4"
                _strFormStore = "vi_form44_4_edi_updateTariff_NewDS"
            Case "FORM44_41"
                _strFormStore = "vi_form44_41_edi_updateTariff_NewDS"
            Case "FORM44_44"
                _strFormStore = "vi_form44_44_edi_updateTariff_NewDS"
            Case "FORM44"
                _strFormStore = "vi_form44_edi_updateTariff_NewDS"
            Case "FORM441_4"
                _strFormStore = "vi_form441_4_edi_updateTariff_NewDS"
            Case "FORM441"
                _strFormStore = "vi_form441_edi_updateTariff_NewDS"


            Case "FORM4"
                _strFormStore = "vi_form4_edi_updateTariff_NewDS"
            Case "FORM4_2"
                _strFormStore = "vi_form4_2_edi_updateTariff_NewDS"
            Case "FORM4_3"
                _strFormStore = "vi_form4_3_edi_updateTariff_NewDS"
            Case "FORM4_4"
                _strFormStore = "vi_form4_4_edi_updateTariff_NewDS"
            Case "FORM4_5"
                _strFormStore = "vi_form4_5_edi_updateTariff_NewDS"
            Case "FORM4_6"
                _strFormStore = "vi_form4_6_edi_updateTariff_NewDS"

                'By Tine
            Case "FORM4_61"
                _strFormStore = "vi_form4_61_edi_updateTariff_NewDS"

            Case "FORM4_8"
                _strFormStore = "vi_form4_8_edi_updateTariff_NewDS"

                'By Tine
            Case "FORM4_81"
                _strFormStore = "vi_form4_81_edi_updateTariff_NewDS"

            Case "FORM4_9"
                _strFormStore = "vi_form4_9_edi_updateTariff_NewDS"
            Case "FORM4_91"
                _strFormStore = "vi_form4_91_edi_updateTariff_NewDS"
            Case "FORM5"
                _strFormStore = "vi_form5_edi_updateTariff_NewDS"
                'by rut
            Case "FORM5_1"
                _strFormStore = "vi_form5_1_edi_updateTariff_NewDS"

                ''ByTine 28-10-2558 เพิ่มฟอร์มใหม่ ไทย-ชิลี
            Case "FORM5_2"
                _strFormStore = "vi_form5_2_edi_updateTariff_NewDS"

            Case "FORM6"
                _strFormStore = "vi_form6_edi_updateTariff_NewDS"
            Case "FORM7"
                _strFormStore = "vi_form7_edi_updateTariff_NewDS"
            Case "FORM8"
                _strFormStore = "vi_form8_edi_updateTariff_NewDS"
            Case "FORM9"
                _strFormStore = "vi_form9_edi_updateTariff_NewDS"
            Case "FORMRussia"
                _strFormStore = "vi_form1_edi_updateTariff_NewDS"
        End Select

        Return _strFormStore
    End Function

    Protected Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Try
            Dim ds As New DataSet
            Dim _intValue As Integer = 0 'ไม่มีรายการ

            Select Case CommonUtility.Get_StringValue(DDLListForm.SelectedValue)
                Case "FORM2_1" 'ฟิวล์ ไม่เหมือนอันอื่น
                    If CheckPriKeyTariff(CommonUtility.Get_StringValue(txtNumTariff.Text), "ZZ", _
                        "FORM2_1", CommonUtility.Get_StringValue(txtCAT.Text)) = False Then
                        ds = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, selectAddStoreForm, _
                                                        New SqlParameter("@tariff_code", CommonUtility.Get_StringValue(txtNumTariff.Text)), _
                                                        New SqlParameter("@cat", CommonUtility.Get_StringValue(txtCAT.Text)), _
                                                        New SqlParameter("@country_code", "ZZ"), _
                                                        New SqlParameter("@tariff_name", CommonUtility.Get_StringValue(txttariff_name.Text)), _
                                                        New SqlParameter("@check_digit", CommonUtility.Get_StringValue(txtcheck_digit.Text)), _
                                                        New SqlParameter("@status_flag", ""), _
                                                        New SqlParameter("@reason", ""), _
                                                        New SqlParameter("@update_by", CommonUtility.Get_StringValue(Session("UName"))), _
                                                        New SqlParameter("@update_datetime", CommonUtility.Get_DateTime(Date.Now)), _
                                                        New SqlParameter("@warning_msg", ""))
                    Else
                        _intValue = 1 'มีรายการ
                    End If
                Case "FORM3", "FORM3_1", "FORM4", "FORM4_2", "FORM4_3", "FORM4_4", "FORM4_5", "FORM4_6", "FORM4_8", "FORM4_9", "FORM5", "FORM5_1", "FORM5_2", _
                    "FORM6", "FORM7", "FORM8", "FORM9", "FORM44_4", "FORM44_41", "FORM44_44", "FORM44", "FORM441_4", "FORM441", "FORM4_61", "FORM4_81" 'ฟิวล์ ไม่เหมือนอันอื่น
                    If CheckPriKeyTariff(CommonUtility.Get_StringValue(txtNumTariff.Text), CommonUtility.Get_StringValue(txtCodecountry.Text), _
                                            CommonUtility.Get_StringValue(DDLListForm.SelectedValue), "") = False Then
                        ds = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, selectAddStoreForm, _
                                                        New SqlParameter("@tariff_code", CommonUtility.Get_StringValue(txtNumTariff.Text)), _
                                                        New SqlParameter("@country_code", CommonUtility.Get_StringValue(txtCodecountry.Text)), _
                                                        New SqlParameter("@tariff_name", CommonUtility.Get_StringValue(txttariff_name.Text)), _
                                                        New SqlParameter("@check_digit", CommonUtility.Get_StringValue(txtcheck_digit.Text)), _
                                                        New SqlParameter("@status_flag", ""), _
                                                        New SqlParameter("@reason", ""), _
                                                        New SqlParameter("@update_by", CommonUtility.Get_StringValue(Session("UName"))), _
                                                        New SqlParameter("@update_datetime", CommonUtility.Get_DateTime(Date.Now)), _
                                                        New SqlParameter("@warning_msg", ""))
                    Else
                        _intValue = 1 'มีรายการ
                    End If
                Case Else
                    If CheckPriKeyTariff(CommonUtility.Get_StringValue(txtNumTariff.Text), CommonUtility.Get_StringValue(txtCodecountry.Text), _
                        CommonUtility.Get_StringValue(DDLListForm.SelectedValue), "") = False Then
                        ds = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, selectAddStoreForm, _
                                                        New SqlParameter("@tariff_code", CommonUtility.Get_StringValue(txtNumTariff.Text)), _
                                                        New SqlParameter("@country_code", CommonUtility.Get_StringValue(txtCodecountry.Text)), _
                                                        New SqlParameter("@tariff_name", CommonUtility.Get_StringValue(txttariff_name.Text)), _
                                                        New SqlParameter("@check_digit", CommonUtility.Get_StringValue(txtcheck_digit.Text)), _
                                                        New SqlParameter("@status_flag", ""), _
                                                        New SqlParameter("@reason", ""), _
                                                        New SqlParameter("@update_by", CommonUtility.Get_StringValue(Session("UName"))), _
                                                        New SqlParameter("@update_datetime", CommonUtility.Get_DateTime(Date.Now)), _
                                                        New SqlParameter("@warning_msg", ""), _
                                                        New SqlParameter("@rate_desc", CommonUtility.Get_StringValue(txtrate_desc.Text)))
                    Else
                        _intValue = 1 'มีรายการ
                    End If

            End Select
            Select Case _intValue
                Case 1
                    Page.Title = "เพิ่มพิกัดศุลกากร"
                    lbl_ErrMSG.Text = "ไม่สามารถเพิ่มพิกัดศุลกากรได้ เนื่องจาก มีข้อมูลซ้ำ"
                Case 0
                    If ds.Tables(0).Rows(0).Item("retStatus") = 0 Then
                        ClientScript.RegisterStartupScript(Page.GetType(), "mykey", "CloseAndRebind('navigateToInserted');", True)
                    Else
                        lbl_ErrMSG.Text = "ไม่สามารถเพิ่มพิกัดศุลกากรได้ เนื่องจาก มีข้อมูลซ้ำ"
                    End If
            End Select

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub
    'check พิกัดซ้ำ
    Function CheckPriKeyTariff(ByVal _tariff As String, ByVal _country As String, ByVal _form As String, ByVal _Cat As String) As Boolean
        Dim m_objReader As SqlDataReader

        m_objReader = SqlHelper.ExecuteReader(strConn, CommandType.StoredProcedure, "vi_common_form_edi_CheckAllTariff_NewDS", _
        New SqlParameter("@tariff_code", _tariff), _
        New SqlParameter("@country_code", _country), _
        New SqlParameter("@FORM_TYPE", _form), _
        New SqlParameter("@CAT", _Cat))

        If m_objReader.Read() Then
            Return True
        Else
            Return False
        End If

    End Function

    Protected Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            Dim ds As New DataSet
            Select Case CommonUtility.Get_StringValue(DDLListForm.SelectedValue)
                Case "FORM2_1" 'ฟิวล์ ไม่เหมือนอันอื่น
                    ds = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, selectUpdateStoreForm, _
                                New SqlParameter("@tariff_code", CommonUtility.Get_StringValue(txtNumTariff.Text)), _
                                New SqlParameter("@cat", CommonUtility.Get_StringValue(txtCAT.Text)), _
                                New SqlParameter("@tariff_name", CommonUtility.Get_StringValue(txttariff_name.Text)), _
                                New SqlParameter("@check_digit", CommonUtility.Get_StringValue(txtcheck_digit.Text)), _
                                New SqlParameter("@status_flag", ""), _
                                New SqlParameter("@reason", ""), _
                                New SqlParameter("@update_by", CommonUtility.Get_StringValue(Session("UName"))), _
                                New SqlParameter("@warning_msg", ""))
                Case "FORM3", "FORM3_1", "FORM4", "FORM4_2", "FORM4_3", "FORM4_4", "FORM4_5", "FORM4_6", "FORM4_8", "FORM4_9", "FORM5", "FORM5_1", "FORM5_2", _
                     "FORM6", "FORM7", "FORM8", "FORM9", "FORM44_4", "FORM44_41", "FORM44_44", "FORM44", "FORM441_4", "FORM441", "FORM4_61", "FORM4_81" 'ฟิวล์ ไม่เหมือนอันอื่น
                    ds = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, selectUpdateStoreForm, _
                                                    New SqlParameter("@tariff_code", CommonUtility.Get_StringValue(txtNumTariff.Text)), _
                                                    New SqlParameter("@country_code", CommonUtility.Get_StringValue(txtCodecountry.Text)), _
                                                    New SqlParameter("@tariff_name", CommonUtility.Get_StringValue(txttariff_name.Text)), _
                                                    New SqlParameter("@check_digit", CommonUtility.Get_StringValue(txtcheck_digit.Text)), _
                                                    New SqlParameter("@status_flag", ""), _
                                                    New SqlParameter("@reason", ""), _
                                                    New SqlParameter("@update_by", CommonUtility.Get_StringValue(Session("UName"))), _
                                                    New SqlParameter("@update_datetime", CommonUtility.Get_DateTime(Date.Now)), _
                                                    New SqlParameter("@warning_msg", ""))

                Case Else
                    ds = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, selectUpdateStoreForm, _
                                New SqlParameter("@tariff_code", CommonUtility.Get_StringValue(txtNumTariff.Text)), _
                                New SqlParameter("@country_code", CommonUtility.Get_StringValue(txtCodecountry.Text)), _
                                New SqlParameter("@tariff_name", CommonUtility.Get_StringValue(txttariff_name.Text)), _
                                New SqlParameter("@check_digit", CommonUtility.Get_StringValue(txtcheck_digit.Text)), _
                                New SqlParameter("@status_flag", ""), _
                                New SqlParameter("@reason", ""), _
                                New SqlParameter("@update_by", CommonUtility.Get_StringValue(Session("UName"))), _
                                New SqlParameter("@update_datetime", CommonUtility.Get_DateTime(Date.Now)), _
                                New SqlParameter("@warning_msg", ""), _
                                New SqlParameter("@rate_desc", CommonUtility.Get_StringValue(txtrate_desc.Text)))
            End Select

            If ds.Tables(0).Rows(0).Item("retStatus") = 0 Then
                ClientScript.RegisterStartupScript(Page.GetType(), "mykey", "CloseAndRebind();", True)
            Else
                lbl_ErrMSG.Text = "ไม่สามารถบันทึกพิกัดศุลกากรได้"
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub


End Class