Imports System.Web.UI
Imports System.Collections.Generic
Imports System.Reflection

Imports DotNetNuke
Imports DotNetNuke.Services.Exceptions
Imports DotNetNuke.Services.Localization

Imports Microsoft.ApplicationBlocks.Data
Imports DFT.Dotnetnuke.ClassLibrary

Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI


Partial Public Class ctrlForm4_8
    Inherits Entities.Modules.PortalModuleBase

    Dim UpdateTargetFile = ""
    Dim g_sForm = "FORM4_8"
    Dim g_sAction As String = "คำขอหนังสือรับรองถิ่นกำเนิดสินค้าฟอร์ม ASEAN-KOREA"
    Dim strConn As String = ConfigurationManager.ConnectionStrings("OriginConnection").ConnectionString
    Dim reader As SqlDataReader = Nothing
    'Dim objConn As SqlConnection = Nothing
    Protected SiteUrl As String

    Private Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Try
            If CallSiteForm_DS.Tables(0).Rows.Count > 0 Then
                DDLSiteCase.DataSource = CallSiteForm_DS.Tables(0)
                DDLSiteCase.DataTextField = "site_name"
                DDLSiteCase.DataValueField = "site_id"
                DDLSiteCase.DataBind()
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SiteUrl = "http://" & DotNetNuke.Common.GetDomainName(Request)
        txtInvHRunAuto.Text = Request("InvHRunAuto")
        Session("ssInvHRunAuto") = txtInvHRunAuto.Text

        'by rut
        Select Case checkData_UrlCase(txtInvHRunAuto.Text, Request.QueryString("action"), Request("udftkey"))
            Case False
                GoTo urlcheckError1
        End Select

        'ทำเพื่อเปลี่ยนบัตร
        If Request("FormCardID") = "" Then
            txtCardID.Text = txtCardID.Text
        Else
            Session("ssFormCardID") = Request("FormCardID")
            txtCardID.Text = Session("ssFormCardID") 'Request("FormCardID")
        End If

        Session("ssFilePath") = Server.MapPath("")

        If Not IsPostBack Then
            txtOB_Address.Focus()
            If Request.QueryString("action") = "new" Then
                '=======================
                'by rut New FOB Other
                LoadCURRENCY_CODE_page()
                drpI_Currency_Code.Text = "USD"
                txtHideFOBOther.Text = drpI_Currency_Code.SelectedValue
                Session("CURRENCY_CODE") = drpI_Currency_Code.SelectedValue
                '=======================

                Call SetForm()
                btnSave.Visible = True

                '<-- DS2 edited -->
                If Request.QueryString("signed") = "true" Or Signed.Value = "true" Then
                    btnSave.Visible = False
                    btnSaveAndSigned.Visible = True
                    panelAttach.Visible = True

                    'by rut แสดง Site
                    PanelSiteCase.Visible = True
                Else
                    btnSave.Visible = True
                    btnSaveAndSigned.Visible = False
                    panelAttach.Visible = False

                    'by rut แสดง Site
                    PanelSiteCase.Visible = False
                End If
                '<-- end DS2 -->
            ElseIf Request.QueryString("action") = "view" Then
                '=======================
                'by rut New FOB Other
                LoadCURRENCY_CODE_page()
                drpI_Currency_Code.Enabled = False
                '=======================

                Call SetForm()
                btnSave.Visible = False
                btnInsertItem.Visible = False

                '<-- DS2 edited -->
                If Request.QueryString("signed") = "true" Or Signed.Value = "true" Then
                    panelAttach.Visible = True

                    'by rut แสดง Site
                    PanelSiteCase.Visible = True
                Else
                    panelAttach.Visible = False

                    'by rut แสดง Site
                    PanelSiteCase.Visible = False
                End If
                '<-- end DS2 -->
                '<-- DS2 edited -->
                btnSaveAndSigned.Visible = False
                btnAttach.Visible = False
                '<-- end DS2 -->
            ElseIf Request.QueryString("action") = "edit" Then
                '============================
                'by rut New FOB Other
                LoadCURRENCY_CODE_page()
                '============================

                'by rut new
                _TolValue()

                Call SetForm()
                btnSave.Visible = True

                '<-- DS2 edited -->
                If Request.QueryString("signed") = "true" Or Signed.Value = "true" Then
                    btnSave.Visible = False
                    btnSaveAndSigned.Visible = True
                    panelAttach.Visible = True

                    'by rut แสดง Site
                    PanelSiteCase.Visible = True
                Else
                    btnSave.Visible = True
                    btnSaveAndSigned.Visible = False
                    panelAttach.Visible = False

                    'by rut แสดง Site
                    PanelSiteCase.Visible = False
                End If
                '<-- end DS2 -->

            End If
        End If

        'by rut เฉพาะ Title แสดงเฉพาะรายการสินค้า
        Session("ssTaxCom") = txtCompanyTaxNo.Text

        '=========================
        'by rut 
        txtHideFOBOther.Text = drpI_Currency_Code.SelectedValue
        Session("CURRENCY_CODE") = drpI_Currency_Code.SelectedValue
        '=========================

        If Session("ssCompany_TaxNo") <> txtCompanyTaxNo.Text Then
urlcheckError1:
            singOuttoLogin()
        End If

    End Sub
    Sub singOuttoLogin()
        DotNetNuke.Security.PortalSecurity.ClearRoles()

        Session.Clear()

        Session.Abandon()

        Dim objPS As New PortalSecurity()

        objPS.SignOut()
        Response.Redirect("http://" & DotNetNuke.Common.GetDomainName(Request) & "/Home/UserLogIn/tabid/58/Default.aspx", True)

    End Sub

#Region "by rut FOB Other"
    'เลือกสกุลเงินอื่น
    Sub LoadCURRENCY_CODE_page()
        Try
            Dim ds As New DataSet
            ds = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, "sp_common_get_currency_NewDS")

            If ds.Tables(0).Rows.Count > 0 Then
                drpI_Currency_Code.DataSource = ds.Tables(0)
                drpI_Currency_Code.DataTextField = "DESCRIPTION"
                drpI_Currency_Code.DataValueField = "CODE"
                drpI_Currency_Code.DataBind()
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Function update_FobSelect(ByVal by_case As Integer, ByVal by_InvHRunAuto As String) As Integer
        Try
            If by_InvHRunAuto <> "" Then
                Dim sqlFOB As String = ""
                sqlFOB = "UPDATE    form_header_edi " & _
                       "SET              Currency_Code =@Currency_Code " & _
                       "WHERE     (invh_run_auto = @invh_run_auto)"

                Dim prm(1) As SqlClient.SqlParameter
                prm(0) = New SqlClient.SqlParameter("@invh_run_auto", by_InvHRunAuto)

                Select Case by_case
                    Case 1 'update USD อย่างเดียว
                        prm(1) = New SqlClient.SqlParameter("@Currency_Code", "USD")
                    Case 2 'update ตามสกุลที่เลือก
                        prm(1) = New SqlClient.SqlParameter("@Currency_Code", drpI_Currency_Code.SelectedValue)
                End Select

                SqlHelper.ExecuteNonQuery(strConn, CommandType.Text, sqlFOB, prm)
            Else
                lbl_ErrMSG.Focus()
                lbl_ErrMSG.Text = "กรุณาออกจากระบบ และทำการ Login เข้ามาใหม่"
            End If

        Catch ex As Exception
            lbl_ErrMSG.Focus()
            lbl_ErrMSG.Text = "กรุณาออกจากระบบ และทำการ Login เข้ามาใหม่"
        End Try

    End Function
#End Region
    Sub SetForm()
        Dim m_objReader As SqlDataReader
        m_objReader = SqlHelper.ExecuteReader(strConn, CommandType.StoredProcedure, "sp_common_form_edi_getHeaderByDataKey_NewDS", _
        New SqlParameter("@INVH_RUN_AUTO", txtInvHRunAuto.Text))
        Dim i As Integer = 0

        If m_objReader.Read() Then
            txtRequestPerson.Text = CommonUtility.Get_StringValue(m_objReader("Authorize2"))
            txtCompanyName.Text = CommonUtility.Get_StringValue(m_objReader("Company_Name"))
            txtCompanyTaxNo.Text = CommonUtility.Get_StringValue(m_objReader("Company_TaxNo"))
            txtCompanyAddress.Text = CommonUtility.Get_StringValue(m_objReader("Company_Address"))
            txtCompanyProvince.Text = CommonUtility.Get_StringValue(m_objReader("Company_Province"))
            txtCompanyCountry.Text = CommonUtility.Get_StringValue(m_objReader("Company_Country"))
            txtCompanyPhone.Text = CommonUtility.Get_StringValue(m_objReader("Company_Phone"))
            txtCompanyFax.Text = CommonUtility.Get_StringValue(m_objReader("Company_Fax"))
            If Request.QueryString("action") = "new" Then
                txtCardID.Text = txtCardID.Text
            Else
                txtCardID.Text = CommonUtility.Get_StringValue(m_objReader("Card_ID"))
            End If
            dropDestRemark.SelectedValue = CommonUtility.Get_StringValue(m_objReader("Dest_Remark"))
            txtOB_Address.Text = CommonUtility.Get_StringValue(m_objReader("OB_ADDRESS"))
            txtDestinationCompany.Text = CommonUtility.Get_StringValue(m_objReader("Destination_Company"))
            txtDestinationTaxID.Text = CommonUtility.Get_StringValue(m_objReader("Destination_TaxID"))
            txtDestinationAddress.Text = CommonUtility.Get_StringValue(m_objReader("Destination_Address"))
            txtDestinationProvince.Text = CommonUtility.Get_StringValue(m_objReader("Destination_Province"))
            txtDestReceiveCountry.Text = CommonUtility.Get_StringValue(m_objReader("Dest_Receive_Country"))

            If Not m_objReader("Destination_Country").Equals(System.DBNull.Value) Then
                dropDestinationCountry.SelectedValue = CommonUtility.Get_StringValue(m_objReader("Destination_Country"))
            End If

            txtDestinationPhone.Text = CommonUtility.Get_StringValue(m_objReader("Destination_Phone"))
            txtDestinationFax.Text = CommonUtility.Get_StringValue(m_objReader("Destination_Fax"))
            dropDestRemark1.SelectedValue = CommonUtility.Get_StringValue(m_objReader("dest_remark1"))
            txtob_dest_address.Text = CommonUtility.Get_StringValue(m_objReader("ob_dest_address"))
            SetIndexOfRadio(CommonUtility.Get_StringValue(m_objReader("Ship_By")), radShipBy)
            txtTransportBy.Text = CommonUtility.Get_StringValue(m_objReader("Transport_By"))

            If Not m_objReader("Departure_Date").Equals(System.DBNull.Value) Then txtDepartureDate.SelectedDate = CommonUtility.Get_DateTime(m_objReader("Departure_Date"))

            txtPortDischarge.Text = CommonUtility.Get_StringValue(m_objReader("Port_Discharge"))
            txtVaselName.Text = CommonUtility.Get_StringValue(m_objReader("Vasel_Name"))
            txtInvoiceBoard.Text = CommonUtility.Get_StringValue(m_objReader("invoice_board"))

            txtNumInvoice.Text = CommonUtility.Get_StringValue(m_objReader("NumInvoice"))
            txtUSDInvoice.Text = CommonUtility.Get_StringValue(m_objReader("USDInvoice"))


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

            If Not IsDBNull(m_objReader("show_check")) Then
                For i = 1 To Len(m_objReader("show_check"))
                    If i > chkShowCheck.Items.Count Then Exit For
                    If Mid(m_objReader("show_check"), i, 1) = "1" Then
                        chkShowCheck.Items(i - 1).Selected = True
                    Else
                        chkShowCheck.Items(i - 1).Selected = False
                    End If
                Next
            End If

            txtThirdCountry.Text = CommonUtility.Get_StringValue(m_objReader("third_country"))
            txtBackCountry.Text = CommonUtility.Get_StringValue(m_objReader("back_country"))
            txtPlaceExibition.Text = CommonUtility.Get_StringValue(m_objReader("place_exibition"))
            txtFactory.Text = CommonUtility.Get_StringValue(m_objReader("Factory"))
            txtFactoryTaxID.Text = CommonUtility.Get_StringValue(m_objReader("Factory_TaxID"))
            txtFactoryAddress.Text = CommonUtility.Get_StringValue(m_objReader("Factory_Address"))
            txtFactoryProvince.Text = CommonUtility.Get_StringValue(m_objReader("Factory_Province"))
            txtFactoryCountry.Text = CommonUtility.Get_StringValue(m_objReader("Factory_Country"))
            txtFactoryPhone.Text = CommonUtility.Get_StringValue(m_objReader("Factory_Phone"))
            txtFactoryFax.Text = CommonUtility.Get_StringValue(m_objReader("Factory_Fax"))
            txtAuthorize.Text = CommonUtility.Get_StringValue(m_objReader("Authorize2"))
            txtGrossWeight.Text = CommonUtility.Get_StringValue(m_objReader("Gross_Weight"))
            If Not m_objReader("G_UNIT_CODE").Equals(System.DBNull.Value) Then
                drpG_UNIT_CODE.SelectedValue = CommonUtility.Get_StringValue(m_objReader("G_UNIT_CODE"))
            End If

            txtQuantity1.Text = CommonUtility.Get_StringValue(m_objReader("Quantity1"))
            If Not m_objReader("Q_UNIT_CODE1").Equals(System.DBNull.Value) Then
                drpQ_UNIT_CODE1.SelectedValue = CommonUtility.Get_StringValue(m_objReader("Q_UNIT_CODE1"))
            End If

            txtQuantity2.Text = CommonUtility.Get_StringValue(m_objReader("Quantity2"))
            If Not m_objReader("Q_UNIT_CODE2").Equals(System.DBNull.Value) Then
                drpQ_UNIT_CODE2.SelectedValue = CommonUtility.Get_StringValue(m_objReader("Q_UNIT_CODE2"))
            End If

            txtQuantity3.Text = CommonUtility.Get_StringValue(m_objReader("Quantity3"))
            If Not m_objReader("Q_UNIT_CODE3").Equals(System.DBNull.Value) Then
                drpQ_UNIT_CODE3.SelectedValue = CommonUtility.Get_StringValue(m_objReader("Q_UNIT_CODE3"))
            End If

            txtQuantity4.Text = CommonUtility.Get_StringValue(m_objReader("Quantity4"))
            If Not m_objReader("Q_UNIT_CODE4").Equals(System.DBNull.Value) Then
                drpQ_UNIT_CODE4.SelectedValue = CommonUtility.Get_StringValue(m_objReader("Q_UNIT_CODE4"))
            End If

            txtQuantity5.Text = CommonUtility.Get_StringValue(m_objReader("Quantity5"))
            If Not m_objReader("Q_UNIT_CODE5").Equals(System.DBNull.Value) Then
                drpQ_UNIT_CODE5.SelectedValue = CommonUtility.Get_StringValue(m_objReader("Q_UNIT_CODE5"))
            End If
            'dropWeightDisplay.SelectedValue = CommonUtility.Get_StringValue(m_objReader("WeightDisplayHeader"))
            txtCompanyEmail.Text = CommonUtility.Get_StringValue(m_objReader("Company_Email"))
            txtDestinationEmail.Text = CommonUtility.Get_StringValue(m_objReader("Destination_Email"))

            txtNewEmail_ch01.Text = CommonUtility.Get_StringValue(m_objReader("NewEmail_ch01"))
            'txtNewEmail_ch02.Text = CommonUtility.Get_StringValue(m_objReader("NewEmail_ch02"))
            '<-- DS2 edited -->
            If Not m_objReader("SentBy").Equals(System.DBNull.Value) Then
                If CommonUtility.Get_StringValue(m_objReader("SentBy")) = "1" Then
                    Signed.Value = "true"
                    'by rut
                    Session("SendSign") = "true"
                Else
                    Signed.Value = "false"
                End If
            Else
                Signed.Value = "false"
            End If
            '<-- end DS2 -->

            'by rut Site
            DDLSiteCase.Text = CommonUtility.Get_StringValue(m_objReader("site_id"))

            '===================================
            'by rut FOBOther
            If Not m_objReader("Currency_Code").Equals(System.DBNull.Value) Then
                drpI_Currency_Code.SelectedValue = CommonUtility.Get_StringValue(m_objReader("Currency_Code"))
            Else
                drpI_Currency_Code.Text = "U.S. DOLLAR"
            End If
            '===================================

            m_objReader.Close()
        End If
    End Sub

    'check เพื่อ ใส่ค่า รวมเมื่อเวลาใช้เงื่อนไข แยกรายการถ้าไม่ใช้ต้องป้อนเอง
    Sub _TolValue()
        Dim DS_checkDetail As DataSet
        Dim _grossSum As Decimal

        'check เงื่อนไขที่รายการ detail ว่าเป็นแยกรายการหรือไม่
        DS_checkDetail = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, "sp_common_form_edi_getDetailListAll_NewDS", _
        New SqlParameter("@INVH_RUN_AUTO", txtInvHRunAuto.Text))

        If DS_checkDetail.Tables(0).Rows.Count > 0 Then
            Dim i As Integer
            Dim icount As Integer = 0
            For i = 0 To DS_checkDetail.Tables(0).Rows.Count - 1
                If CommonUtility.Get_StringValue(DS_checkDetail.Tables.Item(0).Rows(i).Item("CheckGrossDetail")) <> "" Then
                    _grossSum += CommonUtility.Get_Decimal(DS_checkDetail.Tables.Item(0).Rows(i).Item("Gross_Weight"))
                    icount += 1
                End If
                'm_objReader_checkDetail.Item("CheckGrossDetail")
            Next

            Select Case icount
                Case Is > 0
                    txtGrossWeight.Text = 0
                    txtGrossWeight.Value = _grossSum
                    txtGrossWeight.ReadOnly = True
                Case Else
                    'txtGrossWeight.Text = 0
                    txtGrossWeight.ReadOnly = False
            End Select
        Else
            txtGrossWeight.Text = 0
            txtGrossWeight.ReadOnly = False
        End If
    End Sub

    Sub SetIndexOfRadio(ByVal v_sIndex As String, ByRef v_objRadioButtonControl As RadioButtonList)
        v_objRadioButtonControl.SelectedIndex = v_objRadioButtonControl.Items.IndexOf(v_objRadioButtonControl.Items.FindByValue(v_sIndex))
    End Sub

    Protected Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            If txtCompanyTaxNo.Text.Trim = "" Then
                lblErrMsgAttach.Text = "ไม่สามารถบันทึกข้อมูลได้ เนื่องจากขาดการเชื่อมต่อกับระบบ กรุณาเข้าสู่ระบบใหม่อีกครั้ง"
                Exit Sub
            End If
            If CommonUtility.Get_Decimal(txtGrossWeight.Value) = 0 Then
                lbl_ErrMSG.Text = "น้ำหนักรวม (Gross Weight) เป็น ศูนย์ กรุณาป้อนค่าด้วย"
                Exit Sub
            End If

            Dim t_ShowCheck As String
            Dim i As Integer

            If (chkShowCheck.Items(0).Selected = True) And txtThirdCountry.Text = "" Then
                RadAjaxManager1.ResponseScripts.Add("window.alert('กรุณาระบุชื่อ+ประเทศผู้ออก Invoice');")
            ElseIf (chkShowCheck.Items(0).Selected = True) And txtNumInvoice.Text = "" Then
                RadAjaxManager1.ResponseScripts.Add("window.alert('กรุณาระบุ Invoice ต่างประเทศ');")
                'ElseIf (chkShowCheck.Items(0).Selected = True) And txtUSDInvoice.Text.Trim = "" Then
                '    RadAjaxManager1.ResponseScripts.Add("window.alert('กรุณาระบุมูลค่า USD ต่างประเทศ');")
            Else
                For i = 0 To chkShowCheck.Items.Count - 1
                    If chkShowCheck.Items(i).Selected = True Then
                        t_ShowCheck = t_ShowCheck & "1"
                    Else
                        t_ShowCheck = t_ShowCheck & "0"
                    End If
                Next

                'Dim date2, date3, date4, date5, dateDeparture As Object
                Dim date2, date3, date4, date5, dateDeparture As Nullable(Of DateTime)


                If Not txtInvoiceDate2.SelectedDate.HasValue = True Then date2 = Nothing Else date2 = txtInvoiceDate2.SelectedDate.Value
                If Not txtInvoiceDate3.SelectedDate.HasValue = True Then date3 = Nothing Else date3 = txtInvoiceDate3.SelectedDate.Value
                If Not txtInvoiceDate4.SelectedDate.HasValue = True Then date4 = Nothing Else date4 = txtInvoiceDate4.SelectedDate.Value
                If Not txtInvoiceDate5.SelectedDate.HasValue = True Then date5 = Nothing Else date5 = txtInvoiceDate5.SelectedDate.Value
                If Not txtDepartureDate.SelectedDate.HasValue = True Then dateDeparture = Nothing Else dateDeparture = txtDepartureDate.SelectedDate.Value
                Dim str_allInvoiceBoard As String

                If txtNumInvoice.Text = "" Or txtNumInvoice.Text = "" Then
                    str_allInvoiceBoard = ""
                Else
                    str_allInvoiceBoard = txtNumInvoice.Text & vbNewLine & "%" & txtUSDInvoice.Text & "%"
                End If
                Dim m_iReturn As Integer
                m_iReturn = SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, "sp_form4_8_edi_updateHeader_R_NewDS", _
                New SqlParameter("@INVH_RUN_AUTO", CommonUtility.Get_StringValue(txtInvHRunAuto.Text)), _
                New SqlParameter("@COMPANY_TAXNO", CommonUtility.Get_StringValue(txtCompanyTaxNo.Text)), _
                New SqlParameter("@COMPANY_NAME", CommonUtility.Get_StringValue(txtCompanyName.Text)), _
                New SqlParameter("@COMPANY_ADDRESS", CommonUtility.Get_StringValue(txtCompanyAddress.Text)), _
                New SqlParameter("@COMPANY_PROVINCE", CommonUtility.Get_StringValue(txtCompanyProvince.Text)), _
                New SqlParameter("@COMPANY_COUNTRY", CommonUtility.Get_StringValue(txtCompanyCountry.Text)), _
                New SqlParameter("@COMPANY_PHONE", CommonUtility.Get_StringValue(txtCompanyPhone.Text)), _
                New SqlParameter("@COMPANY_FAX", CommonUtility.Get_StringValue(txtCompanyFax.Text)), _
                New SqlParameter("@REQUEST_PERSON", CommonUtility.Get_StringValue(txtRequestPerson.Text)), _
                New SqlParameter("@CARD_ID", CommonUtility.Get_StringValue(txtCardID.Text)), _
                New SqlParameter("@DESTINATION_COMPANY", CommonUtility.Get_StringValue(txtDestinationCompany.Text)), _
                New SqlParameter("@DESTINATION_ADDRESS", CommonUtility.Get_StringValue(txtDestinationAddress.Text)), _
                New SqlParameter("@DESTINATION_PROVINCE", CommonUtility.Get_StringValue(txtDestinationProvince.Text)), _
                New SqlParameter("@DESTINATION_COUNTRY", CommonUtility.Get_StringValue(dropDestinationCountry.SelectedItem.Value)), _
                New SqlParameter("@DESTINATION_PHONE", CommonUtility.Get_StringValue(txtDestinationPhone.Text)), _
                New SqlParameter("@DESTINATION_FAX", CommonUtility.Get_StringValue(txtDestinationFax.Text)), _
                New SqlParameter("@DESTINATION_TAXID", CommonUtility.Get_StringValue(txtDestinationTaxID.Text)), _
                New SqlParameter("@FACTORY", CommonUtility.Get_StringValue(txtFactory.Text)), _
                New SqlParameter("@FACTORY_ADDRESS", CommonUtility.Get_StringValue(txtFactoryAddress.Text)), _
                New SqlParameter("@FACTORY_PROVINCE", CommonUtility.Get_StringValue(txtFactoryProvince.Text)), _
                New SqlParameter("@FACTORY_COUNTRY", CommonUtility.Get_StringValue(txtFactoryCountry.Text)), _
                New SqlParameter("@FACTORY_PHONE", CommonUtility.Get_StringValue(txtFactoryPhone.Text)), _
                New SqlParameter("@FACTORY_FAX", CommonUtility.Get_StringValue(txtFactoryFax.Text)), _
                New SqlParameter("@FACTORY_TAXID", CommonUtility.Get_StringValue(txtFactoryTaxID.Text)), _
                New SqlParameter("@SHIP_BY", CommonUtility.Get_StringValue(radShipBy.SelectedItem.Value)), _
                New SqlParameter("@TRANSPORT_BY", CommonUtility.Get_StringValue(txtTransportBy.Text)), _
                New SqlParameter("@DEPARTURE_DATE", (dateDeparture)), _
                New SqlParameter("@PORT_DISCHARGE", CommonUtility.Get_StringValue(txtPortDischarge.Text)), _
                New SqlParameter("@VASEL_NAME", CommonUtility.Get_StringValue(txtVaselName.Text)), _
                New SqlParameter("@invoice_board", CommonUtility.Get_StringValue(str_allInvoiceBoard)), _
                New SqlParameter("@INVOICE_NO1", CommonUtility.Get_StringValue(txtInvoiceNo1.Text)), _
                New SqlParameter("@INVOICE_DATE1", CommonUtility.Get_DateTime(txtInvoiceDate1.SelectedDate.Value)), _
                New SqlParameter("@INVOICE_NO2", CommonUtility.Get_StringValue(txtInvoiceNo2.Text)), _
                New SqlParameter("@INVOICE_DATE2", (date2)), _
                New SqlParameter("@INVOICE_NO3", CommonUtility.Get_StringValue(txtInvoiceNo3.Text)), _
                New SqlParameter("@INVOICE_DATE3", (date3)), _
                New SqlParameter("@INVOICE_NO4", CommonUtility.Get_StringValue(txtInvoiceNo4.Text)), _
                New SqlParameter("@INVOICE_DATE4", (date4)), _
                New SqlParameter("@INVOICE_NO5", CommonUtility.Get_StringValue(txtInvoiceNo5.Text)), _
                New SqlParameter("@INVOICE_DATE5", (date5)), _
                New SqlParameter("@BILL_TYPE", CommonUtility.Get_StringValue(radBillType.SelectedItem.Value)), _
                New SqlParameter("@BILL_TYPE_OTHER", CommonUtility.Get_StringValue(txtBillTypeOther.Text)), _
                New SqlParameter("@BL_NO", CommonUtility.Get_StringValue(txtBlNo.Text)), _
                New SqlParameter("@SAILING_DATE", CommonUtility.Get_DateTime(txtSailingDate.SelectedDate.Value)), _
                New SqlParameter("@EDI_DATE", (dateDeparture)), _
                New SqlParameter("@ATTACH_FILE", CommonUtility.Get_StringValue(txtAttachFile.Text)), _
                New SqlParameter("@DEST_RECEIVE_COUNTRY", CommonUtility.Get_StringValue(txtDestReceiveCountry.Text)), _
                New SqlParameter("@DEST_REMARK", CommonUtility.Get_StringValue(dropDestRemark.SelectedItem.Value)), _
                New SqlParameter("@GROSS_WEIGHT", CommonUtility.Get_Decimal(txtGrossWeight.Text)), _
                New SqlParameter("@G_UNIT_CODE", CommonUtility.Get_StringValue(drpG_UNIT_CODE.SelectedItem.Value)), _
                New SqlParameter("@QUANTITY1", CommonUtility.Get_Decimal(txtQuantity1.Text)), _
                New SqlParameter("@Q_UNIT_CODE1", CommonUtility.Get_StringValue(drpQ_UNIT_CODE1.SelectedItem.Value)), _
                New SqlParameter("@QUANTITY2", CommonUtility.Get_Decimal(txtQuantity2.Text)), _
                New SqlParameter("@Q_UNIT_CODE2", CommonUtility.Get_StringValue(drpQ_UNIT_CODE2.SelectedItem.Value)), _
                New SqlParameter("@QUANTITY3", CommonUtility.Get_Decimal(txtQuantity3.Text)), _
                New SqlParameter("@Q_UNIT_CODE3", CommonUtility.Get_StringValue(drpQ_UNIT_CODE3.SelectedItem.Value)), _
                New SqlParameter("@QUANTITY4", CommonUtility.Get_Decimal(txtQuantity4.Text)), _
                New SqlParameter("@Q_UNIT_CODE4", CommonUtility.Get_StringValue(drpQ_UNIT_CODE4.SelectedItem.Value)), _
                New SqlParameter("@QUANTITY5", CommonUtility.Get_Decimal(txtQuantity5.Text)), _
                New SqlParameter("@Q_UNIT_CODE5", CommonUtility.Get_StringValue(drpQ_UNIT_CODE5.SelectedItem.Value)), _
                New SqlParameter("@DEST_REMARK1", CommonUtility.Get_StringValue(dropDestRemark1.SelectedItem.Value)), _
                New SqlParameter("@WEIGHTDISPLAYHEADER", CommonUtility.Get_StringValue("")), _
                New SqlParameter("@Company_email", CommonUtility.Get_StringValue(txtCompanyEmail.Text)), _
                New SqlParameter("@Destination_email", CommonUtility.Get_StringValue(txtDestinationEmail.Text)), _
                New SqlParameter("@show_check", CommonUtility.Get_StringValue(t_ShowCheck)), _
                New SqlParameter("@third_country", CommonUtility.Get_StringValue(txtThirdCountry.Text)), _
                New SqlParameter("@back_country", CommonUtility.Get_StringValue(txtBackCountry.Text)), _
                New SqlParameter("@place_exibition", CommonUtility.Get_StringValue(txtPlaceExibition.Text)), _
                New SqlParameter("@ob_dest_address", CommonUtility.Get_StringValue(txtob_dest_address.Text)), _
                New SqlParameter("@USDInvoice", CommonUtility.Get_StringValue(txtUSDInvoice.Text)), _
                New SqlParameter("@NumInvoice", CommonUtility.Get_StringValue(txtNumInvoice.Text)), _
                New SqlParameter("@Check_StatusWeb", "1"), _
                New SqlParameter("@NewEmail_ch01", CommonUtility.Get_StringValue(txtNewEmail_ch01.Text)), _
                New SqlParameter("@NewEmail_ch02", ""), _
                New SqlParameter("@site_id", Nothing), _
                New SqlParameter("@Currency_Code", CommonUtility.Get_StringValue(drpI_Currency_Code.SelectedItem.Value)), _
                New SqlParameter("@OB_ADDRESS", CommonUtility.Get_StringValue(txtOB_Address.Text)))

                If m_iReturn = 0 Then
                    lbl_ErrMSG.Text = "ไม่สามารถบันทึกได้  ต้องกรอกข้อมูลให้ครบตามที่มีเครื่องหมาย * ข้างหน้า"
                Else
                    Response.Redirect(EditUrl("ctrlCO_Drafts") & "&COMPANY_TAXNO=" & txtCompanyTaxNo.Text)
                End If
            End If


        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub grdItemData_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdItemData.DataBound
        reader.Close()
        'objConn.Close()
    End Sub

    Private Sub grdItemData_ItemCreated(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles grdItemData.ItemCreated
        If TypeOf e.Item Is GridDataItem Then
            Dim viewLink As HyperLink = DirectCast(e.Item.FindControl("ViewLink"), HyperLink)
            viewLink.Attributes("href") = "#"
            viewLink.Attributes("onclick") = [String].Format("return ShowViewForm('{0}','{1}','{2}');", e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("invh_run_auto"), e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("invd_run_auto"), "view")

            Dim editLink As HyperLink = DirectCast(e.Item.FindControl("EditLink"), HyperLink)
            editLink.Attributes("href") = "#"
            editLink.Attributes("onclick") = [String].Format("return ShowEditForm('{0}','{1}','{2}');", e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("invh_run_auto"), e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("invd_run_auto"), "edit")

            Dim deleteLink As HyperLink = DirectCast(e.Item.FindControl("DeleteLink"), HyperLink)
            deleteLink.Attributes("href") = "#"
            deleteLink.Attributes("onclick") = [String].Format("return ShowDeleteForm('{0}','{1}');", e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("invh_run_auto"), e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("invd_run_auto"))
        End If

        If Request.QueryString("action") = "view" Then
            grdItemData.MasterTableView.Columns(1).Visible = False
            grdItemData.MasterTableView.Columns(2).Visible = False
        End If
    End Sub

    Private Sub RadAjaxManager1_AjaxRequest(ByVal sender As Object, ByVal e As Telerik.Web.UI.AjaxRequestEventArgs) Handles RadAjaxManager1.AjaxRequest
        grdItemData.MasterTableView.SortExpressions.Clear()
        grdItemData.MasterTableView.GroupByExpressions.Clear()
        grdItemData.Rebind()

        GridUploadFile.MasterTableView.SortExpressions.Clear()
        GridUploadFile.MasterTableView.GroupByExpressions.Clear()
        GridUploadFile.Rebind()

        _TolValue()
    End Sub

    Private Sub grdItemData_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles grdItemData.ItemDataBound
        If Not (e.Item.FindControl("lblIndex") Is Nothing) Then
            Dim lbl As Label = e.Item.FindControl("lblIndex")
            lbl.Text = (grdItemData.MasterTableView.CurrentPageIndex * grdItemData.MasterTableView.PageSize) + (e.Item.ItemIndex + 1)
        End If
        Try
            Dim Total As Decimal = 0
            For Each row As DataRow In LoadGridDetail_DS.Tables(0).Rows
                Total += Convert.ToDouble(GetDecemalCheck_2(row("USDInvoiceDetail").ToString()))
            Next row

            If Not (e.Item.FindControl("Label2Foot") Is Nothing) Then
                Dim lblx As Label = e.Item.FindControl("Label2Foot")
                lblx.Text = Format(CDec(Total.ToString("N")), "#,##0.0000")
            End If
        Catch ex As Exception

        End Try

    End Sub

#Region "func convert"
    Function GetDecemalCheck_(ByVal By_value As String) As String
        Dim temp_ As String = 0

        Select Case CommonUtility.Get_StringValue(By_value)
            Case Is <> ""
                temp_ = CommonUtility.Get_StringValue(By_value)
                Return Format(CDec(temp_), "#,##0.0000")
            Case Else
                Return Format(CDec(temp_), "#,##0.0000")
        End Select
    End Function

    Function GetDecemalCheck_2(ByVal By_value As String) As String
        Dim temp_ As String = 0

        Select Case CommonUtility.Get_StringValue(By_value)
            Case Is <> ""
                temp_ = CommonUtility.Get_StringValue(By_value)
                Return temp_
            Case Else
                Return temp_
        End Select
    End Function

    Private Function LoadGridDetail_DS() As DataSet
        Try
            Dim ds As New DataSet

            'objConn = New SqlConnection(strConn)
            ds = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, "sp_common_form_edi_getDetailListAll_NewDS", New SqlParameter("@INVH_RUN_AUTO", txtInvHRunAuto.Text))
            Return ds
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Function
#End Region
    Private Sub grdItemData_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles grdItemData.NeedDataSource
        grdItemData.DataSource = LoadGridDetail()
    End Sub

    Private Function LoadGridDetail() As SqlDataReader
        Try
            'objConn = New SqlConnection(strConn)
            reader = SqlHelper.ExecuteReader(strConn, CommandType.StoredProcedure, "sp_common_form_edi_getDetailListAll_NewDS", New SqlParameter("@INVH_RUN_AUTO", txtInvHRunAuto.Text))
            Return reader
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Function

    Protected Sub btnHome_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHome.Click
        Response.Redirect(EditUrl("ctrlCO_Main") & "&CardID=" & Session("ssCard_ID"))
    End Sub

    Private Sub GridUploadFile_ItemCreated(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles GridUploadFile.ItemCreated
        If TypeOf e.Item Is GridDataItem Then
            Dim deleteLink As HyperLink = DirectCast(e.Item.FindControl("DeleteFile"), HyperLink)
            deleteLink.Attributes("href") = "#"
            deleteLink.Attributes("onclick") = [String].Format("return ShowDeleteFormFile('{0}','{1}','{2}');", e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("InvH_Run_Auto"), e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("FileID"), e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("FilesName"))

            '<-- DS2 edited -->
            If Request.QueryString("action") = "view" Then
                GridUploadFile.MasterTableView.Columns(1).Visible = False
                GridUploadFile.MasterTableView.Columns(2).Visible = False
            End If
            '<-- end DS2 -->

        End If
    End Sub

    Private Sub GridUploadFile_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles GridUploadFile.ItemDataBound
        If (TypeOf e.Item Is GridDataItem) Then
            Dim dataItem As GridDataItem = CType(e.Item, GridDataItem)
            Dim link As HyperLink = DirectCast(e.Item.FindControl("ViewFile"), HyperLink)
            link.NavigateUrl = SiteUrl & "/Portals/0/DocumentFiles/" & dataItem("FilesName").Text
            link.Target = "_blank"
        End If
    End Sub

    Private Sub GridUploadFile_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles GridUploadFile.NeedDataSource
        GridUploadFile.DataSource = LoadGridDetailDocument()
    End Sub

    Private Function LoadGridDetailDocument() As SqlDataReader
        Try
            'objConn = New SqlConnection(strConn)
            Dim dr As SqlClient.SqlDataReader = SqlHelper.ExecuteReader(strConn, CommandType.StoredProcedure, "sp_Form_EDI_SelectAttachFile_NewDS", New SqlParameter("@INVH_RUN_AUTO", txtInvHRunAuto.Text))

            Return dr
        Catch ex As Exception
            Response.Write(ex.Message)
            Return Nothing
        End Try
    End Function


    Protected Sub txtDepartureDate_SelectedDateChanged(ByVal sender As System.Object, ByVal e As Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs) Handles txtDepartureDate.SelectedDateChanged
        Dim dateChange As Nullable(Of DateTime)

        If Not txtDepartureDate.SelectedDate.HasValue = True Then dateChange = Nothing Else dateChange = txtDepartureDate.SelectedDate.Value

        txtEdiDate.SelectedDate = dateChange
    End Sub

    Protected Sub btnSaveAndSigned_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveAndSigned.Click
        Try
            If txtCompanyTaxNo.Text.Trim = "" Then
                lblErrMsgAttach.Text = "ไม่สามารถบันทึกข้อมูลได้ เนื่องจากขาดการเชื่อมต่อกับระบบ กรุณาเข้าสู่ระบบใหม่อีกครั้ง"
                Exit Sub
            End If
            If CommonUtility.Get_Decimal(txtGrossWeight.Value) = 0 Then
                lbl_ErrMSG.Text = "น้ำหนักรวม (Gross Weight) เป็น ศูนย์ กรุณาป้อนค่าด้วย"
                Exit Sub
            End If

            Dim t_ShowCheck As String
            Dim i As Integer
            If (chkShowCheck.Items(0).Selected = True) And txtThirdCountry.Text = "" Then
                RadAjaxManager1.ResponseScripts.Add("window.alert('กรุณาระบุชื่อ+ประเทศผู้ออก Invoice');")
            ElseIf (chkShowCheck.Items(0).Selected = True) And txtNumInvoice.Text = "" Then
                RadAjaxManager1.ResponseScripts.Add("window.alert('กรุณาระบุ Invoice ต่างประเทศ');")
            ElseIf (chkShowCheck.Items(0).Selected = True) And txtUSDInvoice.Text.Trim = "" Then
                RadAjaxManager1.ResponseScripts.Add("window.alert('กรุณาระบุมูลค่า USD ต่างประเทศ');")
            Else
                For i = 0 To chkShowCheck.Items.Count - 1
                    If chkShowCheck.Items(i).Selected = True Then
                        t_ShowCheck = t_ShowCheck & "1"
                    Else
                        t_ShowCheck = t_ShowCheck & "0"
                    End If
                Next

                'Dim date2, date3, date4, date5, dateDeparture As Object
                Dim date2, date3, date4, date5, dateDeparture As Nullable(Of DateTime)


                If Not txtInvoiceDate2.SelectedDate.HasValue = True Then date2 = Nothing Else date2 = txtInvoiceDate2.SelectedDate.Value
                If Not txtInvoiceDate3.SelectedDate.HasValue = True Then date3 = Nothing Else date3 = txtInvoiceDate3.SelectedDate.Value
                If Not txtInvoiceDate4.SelectedDate.HasValue = True Then date4 = Nothing Else date4 = txtInvoiceDate4.SelectedDate.Value
                If Not txtInvoiceDate5.SelectedDate.HasValue = True Then date5 = Nothing Else date5 = txtInvoiceDate5.SelectedDate.Value
                If Not txtDepartureDate.SelectedDate.HasValue = True Then dateDeparture = Nothing Else dateDeparture = txtDepartureDate.SelectedDate.Value
                Dim str_allInvoiceBoard As String

                If txtNumInvoice.Text = "" Or txtNumInvoice.Text = "" Then
                    str_allInvoiceBoard = ""
                Else
                    str_allInvoiceBoard = txtNumInvoice.Text & vbNewLine & "%" & txtUSDInvoice.Text & "%"
                End If
                Dim m_iReturn As Integer
                m_iReturn = SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, "sp_form4_8_edi_updateHeader_R_NewDS", _
                New SqlParameter("@INVH_RUN_AUTO", CommonUtility.Get_StringValue(txtInvHRunAuto.Text)), _
                New SqlParameter("@COMPANY_TAXNO", CommonUtility.Get_StringValue(txtCompanyTaxNo.Text)), _
                New SqlParameter("@COMPANY_NAME", CommonUtility.Get_StringValue(txtCompanyName.Text)), _
                New SqlParameter("@COMPANY_ADDRESS", CommonUtility.Get_StringValue(txtCompanyAddress.Text)), _
                New SqlParameter("@COMPANY_PROVINCE", CommonUtility.Get_StringValue(txtCompanyProvince.Text)), _
                New SqlParameter("@COMPANY_COUNTRY", CommonUtility.Get_StringValue(txtCompanyCountry.Text)), _
                New SqlParameter("@COMPANY_PHONE", CommonUtility.Get_StringValue(txtCompanyPhone.Text)), _
                New SqlParameter("@COMPANY_FAX", CommonUtility.Get_StringValue(txtCompanyFax.Text)), _
                New SqlParameter("@REQUEST_PERSON", CommonUtility.Get_StringValue(txtRequestPerson.Text)), _
                New SqlParameter("@CARD_ID", CommonUtility.Get_StringValue(txtCardID.Text)), _
                New SqlParameter("@DESTINATION_COMPANY", CommonUtility.Get_StringValue(txtDestinationCompany.Text)), _
                New SqlParameter("@DESTINATION_ADDRESS", CommonUtility.Get_StringValue(txtDestinationAddress.Text)), _
                New SqlParameter("@DESTINATION_PROVINCE", CommonUtility.Get_StringValue(txtDestinationProvince.Text)), _
                New SqlParameter("@DESTINATION_COUNTRY", CommonUtility.Get_StringValue(dropDestinationCountry.SelectedItem.Value)), _
                New SqlParameter("@DESTINATION_PHONE", CommonUtility.Get_StringValue(txtDestinationPhone.Text)), _
                New SqlParameter("@DESTINATION_FAX", CommonUtility.Get_StringValue(txtDestinationFax.Text)), _
                New SqlParameter("@DESTINATION_TAXID", CommonUtility.Get_StringValue(txtDestinationTaxID.Text)), _
                New SqlParameter("@FACTORY", CommonUtility.Get_StringValue(txtFactory.Text)), _
                New SqlParameter("@FACTORY_ADDRESS", CommonUtility.Get_StringValue(txtFactoryAddress.Text)), _
                New SqlParameter("@FACTORY_PROVINCE", CommonUtility.Get_StringValue(txtFactoryProvince.Text)), _
                New SqlParameter("@FACTORY_COUNTRY", CommonUtility.Get_StringValue(txtFactoryCountry.Text)), _
                New SqlParameter("@FACTORY_PHONE", CommonUtility.Get_StringValue(txtFactoryPhone.Text)), _
                New SqlParameter("@FACTORY_FAX", CommonUtility.Get_StringValue(txtFactoryFax.Text)), _
                New SqlParameter("@FACTORY_TAXID", CommonUtility.Get_StringValue(txtFactoryTaxID.Text)), _
                New SqlParameter("@SHIP_BY", CommonUtility.Get_StringValue(radShipBy.SelectedItem.Value)), _
                New SqlParameter("@TRANSPORT_BY", CommonUtility.Get_StringValue(txtTransportBy.Text)), _
                New SqlParameter("@DEPARTURE_DATE", (dateDeparture)), _
                New SqlParameter("@PORT_DISCHARGE", CommonUtility.Get_StringValue(txtPortDischarge.Text)), _
                New SqlParameter("@VASEL_NAME", CommonUtility.Get_StringValue(txtVaselName.Text)), _
                New SqlParameter("@invoice_board", CommonUtility.Get_StringValue(str_allInvoiceBoard)), _
                New SqlParameter("@INVOICE_NO1", CommonUtility.Get_StringValue(txtInvoiceNo1.Text)), _
                New SqlParameter("@INVOICE_DATE1", CommonUtility.Get_DateTime(txtInvoiceDate1.SelectedDate.Value)), _
                New SqlParameter("@INVOICE_NO2", CommonUtility.Get_StringValue(txtInvoiceNo2.Text)), _
                New SqlParameter("@INVOICE_DATE2", (date2)), _
                New SqlParameter("@INVOICE_NO3", CommonUtility.Get_StringValue(txtInvoiceNo3.Text)), _
                New SqlParameter("@INVOICE_DATE3", (date3)), _
                New SqlParameter("@INVOICE_NO4", CommonUtility.Get_StringValue(txtInvoiceNo4.Text)), _
                New SqlParameter("@INVOICE_DATE4", (date4)), _
                New SqlParameter("@INVOICE_NO5", CommonUtility.Get_StringValue(txtInvoiceNo5.Text)), _
                New SqlParameter("@INVOICE_DATE5", (date5)), _
                New SqlParameter("@BILL_TYPE", CommonUtility.Get_StringValue(radBillType.SelectedItem.Value)), _
                New SqlParameter("@BILL_TYPE_OTHER", CommonUtility.Get_StringValue(txtBillTypeOther.Text)), _
                New SqlParameter("@BL_NO", CommonUtility.Get_StringValue(txtBlNo.Text)), _
                New SqlParameter("@SAILING_DATE", CommonUtility.Get_DateTime(txtSailingDate.SelectedDate.Value)), _
                New SqlParameter("@EDI_DATE", (dateDeparture)), _
                New SqlParameter("@ATTACH_FILE", CommonUtility.Get_StringValue(txtAttachFile.Text)), _
                New SqlParameter("@DEST_RECEIVE_COUNTRY", CommonUtility.Get_StringValue(txtDestReceiveCountry.Text)), _
                New SqlParameter("@DEST_REMARK", CommonUtility.Get_StringValue(dropDestRemark.SelectedItem.Value)), _
                New SqlParameter("@GROSS_WEIGHT", CommonUtility.Get_Decimal(txtGrossWeight.Text)), _
                New SqlParameter("@G_UNIT_CODE", CommonUtility.Get_StringValue(drpG_UNIT_CODE.SelectedItem.Value)), _
                New SqlParameter("@QUANTITY1", CommonUtility.Get_Decimal(txtQuantity1.Text)), _
                New SqlParameter("@Q_UNIT_CODE1", CommonUtility.Get_StringValue(drpQ_UNIT_CODE1.SelectedItem.Value)), _
                New SqlParameter("@QUANTITY2", CommonUtility.Get_Decimal(txtQuantity2.Text)), _
                New SqlParameter("@Q_UNIT_CODE2", CommonUtility.Get_StringValue(drpQ_UNIT_CODE2.SelectedItem.Value)), _
                New SqlParameter("@QUANTITY3", CommonUtility.Get_Decimal(txtQuantity3.Text)), _
                New SqlParameter("@Q_UNIT_CODE3", CommonUtility.Get_StringValue(drpQ_UNIT_CODE3.SelectedItem.Value)), _
                New SqlParameter("@QUANTITY4", CommonUtility.Get_Decimal(txtQuantity4.Text)), _
                New SqlParameter("@Q_UNIT_CODE4", CommonUtility.Get_StringValue(drpQ_UNIT_CODE4.SelectedItem.Value)), _
                New SqlParameter("@QUANTITY5", CommonUtility.Get_Decimal(txtQuantity5.Text)), _
                New SqlParameter("@Q_UNIT_CODE5", CommonUtility.Get_StringValue(drpQ_UNIT_CODE5.SelectedItem.Value)), _
                New SqlParameter("@DEST_REMARK1", CommonUtility.Get_StringValue(dropDestRemark1.SelectedItem.Value)), _
                New SqlParameter("@WEIGHTDISPLAYHEADER", CommonUtility.Get_StringValue("")), _
                New SqlParameter("@Company_email", CommonUtility.Get_StringValue(txtCompanyEmail.Text)), _
                New SqlParameter("@Destination_email", CommonUtility.Get_StringValue(txtDestinationEmail.Text)), _
                New SqlParameter("@show_check", CommonUtility.Get_StringValue(t_ShowCheck)), _
                New SqlParameter("@third_country", CommonUtility.Get_StringValue(txtThirdCountry.Text)), _
                New SqlParameter("@back_country", CommonUtility.Get_StringValue(txtBackCountry.Text)), _
                New SqlParameter("@place_exibition", CommonUtility.Get_StringValue(txtPlaceExibition.Text)), _
                New SqlParameter("@ob_dest_address", CommonUtility.Get_StringValue(txtob_dest_address.Text)), _
                New SqlParameter("@USDInvoice", CommonUtility.Get_StringValue(txtUSDInvoice.Text)), _
                New SqlParameter("@NumInvoice", CommonUtility.Get_StringValue(txtNumInvoice.Text)), _
                New SqlParameter("@Check_StatusWeb", "1"), _
                New SqlParameter("@NewEmail_ch01", CommonUtility.Get_StringValue(txtNewEmail_ch01.Text)), _
                New SqlParameter("@NewEmail_ch02", ""), _
                New SqlParameter("@site_id", CommonUtility.Get_StringValue(DDLSiteCase.SelectedValue)), _
                New SqlParameter("@Currency_Code", CommonUtility.Get_StringValue(drpI_Currency_Code.SelectedItem.Value)), _
                New SqlParameter("@OB_ADDRESS", CommonUtility.Get_StringValue(txtOB_Address.Text)))

                If m_iReturn = 0 Then
                    lbl_ErrMSG.Text = "ไม่สามารถบันทึกได้  ต้องกรอกข้อมูลให้ครบตามที่มีเครื่องหมาย * ข้างหน้า"
                Else
                    'Saved
                    '<!-- DS2 edited -->
                    Dim strCommand As String = "UPDATE form_header_edi SET SentBy=1 WHERE invh_run_auto = '" & txtInvHRunAuto.Text & "'"
                    SqlHelper.ExecuteNonQuery(strConn, CommandType.Text, strCommand)

                    'get data from database
                    Dim strEdiHeaderData As String = ""

                    '=== 20100925 edited ===
                    Dim ds As DataSet = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, "sp_common_form_edi_getDataForSign_NewDS", _
                                New SqlParameter("@INVH_RUN_AUTO", CommonUtility.Get_StringValue(txtInvHRunAuto.Text)), _
                                New SqlParameter("@FORM_NO", CommonUtility.Get_StringValue(g_sForm).ToUpper()))

                    strEdiHeaderData = CommonUtility.Get_StringValue(ds.Tables(1).Rows(0).Item("HEADER_DATA")) + CommonUtility.Get_StringValue(ds.Tables(1).Rows(0).Item("DETAIL_DATA")) + CommonUtility.Get_StringValue(ds.Tables(1).Rows(0).Item("DETAIL_DATA2")) + CommonUtility.Get_StringValue(ds.Tables(1).Rows(0).Item("DETAIL_DATA3"))

                    'set string data for signature
                    DataText.Value = strEdiHeaderData

                    Dim strScript As String = "<script language='javascript'>"
                    strScript += "try{ var objSign; objSign = new ActiveXObject('NTISignLib.Signature'); var sdataforsign = document.getElementById('" & DataText.ClientID & "').value; "
                    strScript += "var sSign = objSign.SignString(GetTaxId('" & Session("ssCompany_TaxNo") & "'), sdataforsign);"
                    strScript += "document.getElementById('" & DataSigned.ClientID & "').value = sSign;"
                    strScript += "}catch(e){ alert('Error: ไม่สามารถลงลายมือชื่ออิเล็กทรอนิกส์ได้ \nกรุณาตรวจสอบ Certificate ให้พร้อมใช้งานให้เรียบร้อยก่อน');}"
                    strScript += "document.getElementById('" & btnSignData.ClientID & "').click();"
                    strScript += "</script>"
                    If (Not Page.ClientScript.IsStartupScriptRegistered("clientScript3")) Then
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript3", strScript, False)
                    End If
                    '<!-- end DS2 -->

                    'Response.Redirect(EditUrl("ctrlCO_Drafts") & "?COMPANY_TAXNO=" & txtCompanyTaxNo.Text)
                End If
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub btnSignData_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSignData.Click
        '<!-- DS2 edited -->
        Dim strCommand As String = "UPDATE form_header_edi SET SentBy=1,SignData = '" & DataSigned.Value & "' WHERE invh_run_auto = '" & txtInvHRunAuto.Text & "'"
        Dim m_iReturn As Integer = SqlHelper.ExecuteNonQuery(strConn, CommandType.Text, strCommand)

        If m_iReturn = 1 Then
            Response.Redirect(EditUrl("ctrlCO_Drafts") & "&COMPANY_TAXNO=" & txtCompanyTaxNo.Text)
        Else
            lbl_ErrMSG.Text = "ไม่สามารถบันทึกข้อมูลได้ กรุณาตรวจสอบความถูกต้องของข้อมูล และ Certificate ให้เรียบร้อยก่อน"
        End If
    End Sub

    Protected Sub chkShowCheck_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkShowCheck.SelectedIndexChanged
        'Dim Fi As Integer
        'Dim str_num As String = ""
        'Dim astr_ As String = ""

        'For Fi = 0 To 2
        '    If chkShowCheck.Items(Fi).Selected = True Then
        '        astr_ = "1"
        '        str_num += astr_ & ";"
        '    Else
        '        astr_ = "0"
        '        str_num += astr_ & ";"
        '    End If
        'Next

        If chkShowCheck.Items(0).Selected = True Then
            'txtThirdCountry.Enabled = True
            'txtPlaceExibition.Enabled = False
            'txtBackCountry.Enabled = False

            txtThirdCountry.Focus()
            'txtThirdCountry.Text = ""
            txtPlaceExibition.Text = ""
            txtBackCountry.Text = ""

            chkShowCheck.Items(1).Selected = False
            chkShowCheck.Items(2).Selected = False
            'ElseIf chkShowCheck.Items(0).Selected = False Then
            '    txtThirdCountry.Enabled = False
            '    txtPlaceExibition.Enabled = False
            '    txtBackCountry.Enabled = False

            '    txtThirdCountry.Text = ""
            '    txtPlaceExibition.Text = ""
            '    txtBackCountry.Text = ""
        ElseIf chkShowCheck.Items(1).Selected = True Then
            'txtPlaceExibition.Enabled = True
            'txtThirdCountry.Enabled = False
            'txtBackCountry.Enabled = False

            txtPlaceExibition.Focus()
            txtThirdCountry.Text = ""
            'txtPlaceExibition.Text = ""
            txtBackCountry.Text = ""

            chkShowCheck.Items(0).Selected = False
            chkShowCheck.Items(2).Selected = False
            'ElseIf chkShowCheck.Items(1).Selected = False Then
            '    txtPlaceExibition.Enabled = False
            '    txtThirdCountry.Enabled = False
            '    txtBackCountry.Enabled = False

            '    txtThirdCountry.Text = ""
            '    txtPlaceExibition.Text = ""
            '    txtBackCountry.Text = ""
        ElseIf chkShowCheck.Items(2).Selected = True Then
            'txtThirdCountry.Enabled = False
            'txtPlaceExibition.Enabled = False
            'txtBackCountry.Enabled = True

            txtBackCountry.Focus()
            txtThirdCountry.Text = ""
            txtPlaceExibition.Text = ""
            'txtBackCountry.Text = ""
            chkShowCheck.Items(0).Selected = False
            chkShowCheck.Items(1).Selected = False
            'ElseIf chkShowCheck.Items(2).Selected = True Then
            '    txtThirdCountry.Enabled = False
            '    txtPlaceExibition.Enabled = False
            '    txtBackCountry.Enabled = False

            '    txtThirdCountry.Text = ""
            '    txtPlaceExibition.Text = ""
            '    txtBackCountry.Text = ""
        End If
    End Sub
End Class