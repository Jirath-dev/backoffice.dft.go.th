Imports DFT.Dotnetnuke.ClassLibrary
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data

Partial Public Class ViewCertOfOrigin
    Inherits System.Web.UI.Page

    Dim invh_run_auto As String
    Dim item_no As String
    Dim strConn As String = ConfigurationManager.ConnectionStrings("EDIConnection").ConnectionString

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        invh_run_auto = Request.QueryString("invh_run_auto")
        item_no = Request.QueryString("item_no")
        If Not IsPostBack Then
            GridData.DataSource = LoadData()
            GridData.DataBind()

            'LoadcertOfOrigin()
        End If

    End Sub

    Protected Function LoadData() As DataTable
        Try
            Dim ds As DataSet
            ds = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, "sp_Get_ProductListByRefNo_NewDS2", _
                         New SqlParameter("@Ref_No", Request.QueryString("invh_run_auto")))

            Return ds.Tables(0)
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Function

    Private Sub GridData_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles GridData.NeedDataSource
        GridData.DataSource = LoadData()
    End Sub

    ''Protected Sub LoadcertOfOrigin()
    ''    Dim strConn As String = ConfigurationManager.ConnectionStrings("OriginConnection").ConnectionString
    ''    Dim ds As DataSet = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, "sp_form_edi_getArrayItemNo", _
    ''                            New SqlParameter("@invh_run_auto", invh_run_auto), _
    ''                            New SqlParameter("@item_no", item_no))

    ''    If ds.Tables(0).Rows.Count > 0 Then
    ''        If CommonUtility.Get_StringValue(ds.Tables(0).Rows(0)("check_asset")) <> "" Then
    ''            CheckRollver(ds.Tables(0).Rows(0)("check_asset"))
    ''            lblCertNo.Text = ds.Tables(0).Rows(0)("check_asset")
    ''        Else
    ''            CheckRollver(ds.Tables(0).Rows(0)("asset_shares"))
    ''            lblCertNo.Text = ds.Tables(0).Rows(0)("asset_shares")
    ''        End If
    ''    End If

    ''    ds.Dispose()
    ''End Sub

    ''Protected Sub CheckRollver(ByVal CerNo As String)
    ''    Try
    ''        Dim RollverConn As String = ConfigurationManager.ConnectionStrings("RollverConnection").ConnectionString

    ''        Dim strCommand As String
    ''        strCommand = "SELECT transfer_date, tax_id, country, harmonized_no, certoforigin_no, CONVERT(varchar(8), certoforigin_date, 112) AS certoforigin_date, company_name_th, " & _
    ''                     "company_name_en, goods_desc_th, goods_desc_en, models, country_code, CONVERT(varchar(8), DATEADD(yyyy, 2, certoforigin_date), 112) AS Cert_ExpiredDate " & _
    ''                     "FROM dbo.tbl_certoforigin " & _
    ''                     "WHERE (certoforigin_no = '" & CerNo & "')"
    ''        Dim ds As New DataSet
    ''        ds = SqlHelper.ExecuteDataset(RollverConn, CommandType.Text, strCommand)
    ''        If ds.Tables(0).Rows.Count > 0 Then
    ''            'ตรวจสอบว่าค่าที่ได้มานี้ หมดอายุ หรือว่า ยังไม่หมด
    ''            With ds.Tables(0).Rows(0)
    ''                If CommonUtility.Get_StringValue(.Item("Cert_ExpiredDate")) <= FunctionUtility.DMY2YMD(Today) Then
    ''                    ' lblMsg.Text = "certoforigin หมดอายุ"

    ''                Else
    ''                    'lblMsg.Text = "certoforigin ไม่หมดอายุ"
    ''                End If

    ''                lblCertOfOrigin_No.Text = .Item("certoforigin_no").ToString()
    ''                lblCertOfOrigin_date.Text = GetDateThai(.Item("certoforigin_date").ToString())
    ''                lblExpireDate.Text = GetDateThai(.Item("Cert_ExpiredDate").ToString())
    ''                lblCompany_name_thai.Text = .Item("company_name_th").ToString()
    ''                lblCompany_name_eng.Text = .Item("company_name_en").ToString()
    ''                lblTaxId.Text = .Item("tax_id").ToString()
    ''                lblGood_desc_th.Text = .Item("goods_desc_th").ToString()
    ''                lblGood_name_eng.Text = .Item("goods_desc_en").ToString()
    ''                lblCountry.Text = .Item("country").ToString()
    ''                lblHamonized_no.Text = .Item("harmonized_no").ToString()
    ''                lblModels.Text = .Item("models").ToString()
    ''                lblCompany_name_th2.Text = .Item("company_name_th").ToString().Replace("บริษัท", "")


    ''            End With

    ''        Else
    ''            lblMsg.Text = "ไม่พบข้อมูลต้นทุน"
    ''        End If
    ''    Catch ex As Exception
    ''        ''Return 3
    ''        lblMsg.Text = "ไม่พบข้อมูลต้นทุน"
    ''    End Try
    ''End Sub

    Function GetDateThai(ByVal yyyymmdd As String) As String
        Return Mid(yyyymmdd, 7, 2) & " " & ConvertMonthThai(CInt(Mid(yyyymmdd, 5, 2))) & " " & Mid(yyyymmdd, 1, 4) + 543
    End Function

    Function ConvertMonthThai(ByVal getIndexMonth As Integer) As String
        Select Case getIndexMonth
            Case 1
                Return "มกราคม "
            Case 2
                Return "กุมภาพันธ์ "
            Case 3
                Return "มีนาคม "
            Case 4
                Return "เมษายน "
            Case 5
                Return "พฤษภาคม "
            Case 6
                Return "มิถุนายน "
            Case 7
                Return "กรกฎาคม "
            Case 8
                Return "สิงหาคม "
            Case 9
                Return "กันยายน "
            Case 10
                Return "ตุลาคม "
            Case 11
                Return "พฤศจิกายน "
            Case 12
                Return "ธันวาคม "
        End Select
    End Function

End Class