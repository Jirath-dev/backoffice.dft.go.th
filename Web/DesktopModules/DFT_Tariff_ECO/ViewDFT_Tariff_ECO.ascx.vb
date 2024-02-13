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

Namespace NTi.Modules.DFT_Tariff_ECO
    Partial Class ViewDFT_Tariff_ECO
        Inherits Entities.Modules.PortalModuleBase
        Dim strConn As String = ConfigurationManager.ConnectionStrings("OriginConnection").ConnectionString
        Dim objConn As SqlConnection = Nothing
        Dim reader As SqlDataReader = Nothing

        Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            If Not Page.IsPostBack Then
                lblFormHeader.Text = "ค้นหาพิกัดศุลกากร"

                objConn = New SqlConnection(strConn)
                Dim dr As SqlDataReader
                dr = SqlHelper.ExecuteReader(strConn, CommandType.StoredProcedure, "sp_common_get_countryByFormTypeFORM2_2_NewDS",
                 New SqlParameter("@FORM_TYPE", CommonUtility.Get_StringValue("FORM2_2")))
                If dr.HasRows() Then
                    dropCountry.DataSource = dr
                    dropCountry.DataBind()
                End If
            End If
            ''back
            'If Not Page.IsPostBack Then
            '    lblFormHeader.Text = "ค้นหาพิกัดศุลกากร"

            '    objConn = New SqlConnection(strConn)
            '    Dim dr As SqlDataReader
            '    dr = SqlHelper.ExecuteReader(strConn, CommandType.StoredProcedure, "sp_common_get_countryByFormType", _
            '     New SqlParameter("@FORM_TYPE", CommonUtility.Get_StringValue("FORM1")))
            '    If dr.HasRows() Then
            '        dropCountry.DataSource = dr
            '        dropCountry.DataBind()
            '    End If
            'End If
        End Sub

        Protected Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
            'If Search.Read Then
            grdMasterData.DataSource = Search()
            grdMasterData.Rebind()
            'End If
        End Sub

        Private Function Search() As SqlDataReader
            Try
                If txtTariff.Text.Length < 4 And txtTariff.Text <> "" Then
                    lblMsg.Text = "กรุณากรอกพิกัดอย่างน้อย 4 ตัว"
                Else
                    lblMsg.Text = ""
                End If
                objConn = New SqlConnection(strConn)
                reader = SqlHelper.ExecuteReader(objConn, CommandType.StoredProcedure, "sp_common_getTariffNew_NewDS",
                New SqlParameter("@TARIFF_CODE", txtTariff.Text),
                New SqlParameter("@COUNTRY_CODE", dropCountry.SelectedItem.Value),
                New SqlParameter("@FORM_TYPE", dropForm.SelectedItem.Value))

                Return reader
            Catch ex As Exception
                Return Nothing
            End Try
        End Function

        Private Sub grdMasterData_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdMasterData.DataBound
            reader.Close()
            objConn.Close()
        End Sub

        Private Sub grdMasterData_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles grdMasterData.NeedDataSource
            grdMasterData.DataSource = Search()
        End Sub

        Protected Sub dropForm_SelectedIndexChanged(ByVal o As System.Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles dropForm.SelectedIndexChanged
            ''back
            'txtTariff.Text = ""
            'objConn = New SqlConnection(strConn)
            'Dim dr As SqlDataReader
            'dr = SqlHelper.ExecuteReader(strConn, CommandType.StoredProcedure, "sp_common_get_countryByFormType", _
            ' New SqlParameter("@FORM_TYPE", CommonUtility.Get_StringValue(dropForm.SelectedItem.Value)))
            'If dr.HasRows() Then
            '    dropCountry.DataSource = dr
            '    dropCountry.DataBind()
            'End If

            txtTariff.Text = ""
            objConn = New SqlConnection(strConn)
            Dim dr As SqlDataReader

            dr = SqlHelper.ExecuteReader(strConn, CommandType.StoredProcedure, Form_selectTariff(CommonUtility.Get_StringValue(dropForm.SelectedItem.Value)),
             New SqlParameter("@FORM_TYPE", CommonUtility.Get_StringValue(dropForm.SelectedItem.Value)))

            If dr.HasRows() Then
                dropCountry.DataSource = dr
                dropCountry.DataBind()
            End If
        End Sub
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
                Case "FORM2_ESS"
                    str_store = "sp_common_get_countryByFormTypeFORM2_NewDS"
                Case "FORM2_1"
                    str_store = "sp_common_get_countryByFormTypeFORM2_1_NewDS"
                Case "FORM2_2"
                    str_store = "sp_common_get_countryByFormTypeFORM2_2_NewDS"
                Case "FORM2_3"
                    str_store = "sp_common_get_countryByFormTypeFORM2_3_NewDS"
                Case "FORM2_4"
                    str_store = "sp_common_get_countryByFormTypeFORM2_4_NewDS"
                Case "FORM2_5"
                    str_store = "sp_common_get_countryByFormTypeFORM2_5_NewDS"
                Case "FORM2_6"
                    str_store = "sp_common_get_countryByFormTypeFORM2_6_NewDS"
                Case "FORM3"
                    str_store = "sp_common_get_countryByFormType_NewDS"
                Case "FORM3_1"
                    str_store = "sp_common_get_countryByFormType_NewDS"
                Case "FORM4"
                    str_store = "sp_common_get_countryByFormTypeFORM4_NewDS"
                Case "FORM4_1"
                    str_store = "sp_common_get_countryByFormTypeForm4_1_NewDS"
                Case "FORM4_2", "FORME_01", "FORME_ESS"
                    str_store = "sp_common_get_countryByFormTypeForm4_2_NewDS"
                Case "FORM4_3"
                    str_store = "sp_common_get_countryByFormType_NewDS"
                Case "FORM4_4"
                    str_store = "sp_common_get_countryByFormType_NewDS"
                Case "FORM4_5", "FORM44_01", "FORM44_02", "FORMAHK", "FORMD_ESS", "FORMD_ESS_", "FORMRCEP"
                    str_store = "sp_common_get_countryByFormType_NewDS"
                Case "FORM4_6"
                    str_store = "sp_common_get_countryByFormTypeForm4_6_NewDS"
                Case "FORM4_61"
                    str_store = "sp_common_get_countryByFormTypeForm4_61_NewDS"
                Case "FORM4_8"
                    str_store = "sp_common_get_countryByFormTypeForm4_8_NewDS"
                Case "FORM4_81"
                    str_store = "sp_common_get_countryByFormTypeForm4_81_NewDS"
                Case "FORM4_9"
                    str_store = "sp_common_get_countryByFormTypeForm4_9_NewDS"
                Case "FORM4_91"
                    str_store = "sp_common_get_countryByFormTypeForm4_91_NewDS"

                    ''By Tine 30-03-59
                Case "FORM4_911"
                    str_store = "sp_common_get_countryByFormTypeForm4_911_NewDS"
                    ''By Tine 24-6-57
                Case "FORM44"
                    str_store = "sp_common_get_countryByFormTypeForm44_NewDS"
                Case "FORM44_4"
                    str_store = "sp_common_get_countryByFormTypeForm44_4_NewDS"
                Case "FORM44_41"
                    str_store = "sp_common_get_countryByFormTypeForm44_41_NewDS"
                Case "FORM44_44"
                    str_store = "sp_common_get_countryByFormTypeForm44_44_NewDS"
                Case "FORM441"
                    str_store = "sp_common_get_countryByFormTypeForm441_NewDS"
                Case "FORM441_4"
                    str_store = "sp_common_get_countryByFormTypeForm441_4_NewDS"
                Case "FORM5"
                    str_store = "sp_common_get_countryByFormTypeFORM5_NewDS"
                    'by rut 19-12-54
                Case "FORM5_1", "FORMTP_ESS"
                    str_store = "sp_common_get_countryByFormType_NewDS"
                    ''ByTine 28-10-2558 เพิ่มฟอร์มใหม่ไทย-ชิลี
                Case "FORM5_2", "FORMTC_ESS"
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
    End Class
End Namespace
