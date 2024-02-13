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

Namespace NTi.Modules.DFT_EDI_Report_14
    Partial Class ViewDFT_EDI_Report_14
        Inherits Entities.Modules.PortalModuleBase
        Dim strConn As String = ConfigurationManager.ConnectionStrings("OriginConnection").ConnectionString
        Dim objConn As SqlConnection = Nothing
        Dim objReader As SqlDataReader = Nothing

        Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            If Not Page.IsPostBack Then
                rdpFromDate.SelectedDate = Today '//"01/01/" & Now.Year
                rdpToDate.SelectedDate = Today
                Call LoadForm()
                txtCompany_Name.Text = String.Empty
                txtTariff_Code.Text = String.Empty
            End If
        End Sub

        Sub LoadForm()
            Try
                Dim ds As New DataSet
                Dim strCommand As String = String.Empty
                strCommand = "SELECT form_type, form_name FROM form_type"
                ds = SqlHelper.ExecuteDataset(strConn, CommandType.Text, strCommand)
                If ds.Tables(0).Rows.Count > 0 Then
                    ddlFormType.DataSource = ds.Tables(0)
                    ddlFormType.DataTextField = "form_name"
                    ddlFormType.DataValueField = "form_type"
                    ddlFormType.DataBind()
                End If
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End Sub

        Private Sub rgCetificateList_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles rgCetificateList.DataBound
            objReader.Close()
            objConn.Close()
        End Sub

        'Private Sub rgCetificateList_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgCetificateList.NeedDataSource
        '    rgCetificateList.DataSource = LoadReports()
        'End Sub

        Function LoadReports() As SqlDataReader
            objConn = New SqlConnection(strConn)
            objReader = SqlHelper.ExecuteReader(objConn, CommandType.StoredProcedure, "vi_Report_Advance_DS2", _
                                                New SqlParameter("@COMPANY_NAME", CommonUtility.Get_String(txtCompany_Name.Text)), _
                                                New SqlParameter("@FROM_DATE", FunctionUtility.DMY2YMD(rdpFromDate.SelectedDate.Value)), _
                                                New SqlParameter("@TO_DATE", FunctionUtility.DMY2YMD(rdpToDate.SelectedDate.Value)), _
                                                New SqlParameter("@TARIFF_CODE", CommonUtility.Get_String(txtTariff_Code.Text)), _
                                                New SqlParameter("@FORM_TYPE", CommonUtility.Get_String(ddlFormType.SelectedValue)))

            Return objReader
        End Function

        Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
            rgCetificateList.DataSource = LoadReports()
            rgCetificateList.DataBind()
        End Sub
    End Class

End Namespace
