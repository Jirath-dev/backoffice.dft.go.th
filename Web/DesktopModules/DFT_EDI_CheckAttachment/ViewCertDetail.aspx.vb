Imports Microsoft.ApplicationBlocks.Data
Imports System.Data.SqlClient
Imports DFT.Dotnetnuke.ClassLibrary

Partial Public Class ViewCertDetail
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim CERTNO As String = Request.QueryString("certno")
        Dim TAXNO As String = Request.QueryString("TAXNO")
        Dim COUNTRY As String = Request.QueryString("COUNTRY")

        'If Not IsPostBack Then
        CheckRollver(certNo, TAXNO, COUNTRY)
        'End If
    End Sub

    ''' <summary>
    ''' ฟังก์ชั่นสำหรับตรวจสอบต้นทุน จาก Rollver
    ''' </summary>
    ''' <param name="CERTNO"></param>
    ''' <param name="TAXNO"></param>
    ''' <param name="COUNTRY"></param>
    ''' <remarks></remarks>
    Protected Sub CheckRollver(ByVal CERTNO As String, ByVal TAXNO As String, ByVal COUNTRY As String)
        Try
            Dim RollverConn As String = ConfigurationManager.ConnectionStrings("RollverConnection").ConnectionString

            Dim strCommand As String
            strCommand = "spLoadDataCertoforigin"
            Dim prm As New SqlParameter("@certno", CERTNO)
            Dim ds As New DataSet
            ds = SqlHelper.ExecuteDataset(RollverConn, CommandType.StoredProcedure, strCommand, prm)
            If ds.Tables(0).Rows.Count > 0 Then
                'ตรวจสอบว่าค่าที่ได้มานี้ หมดอายุ หรือว่า ยังไม่หมด
                With ds.Tables(0).Rows(0)
                    If CommonUtility.Get_StringValue(.Item("Cert_ExpiredDate")) < FunctionUtility.DMY2YMD(Today) Then
                        lblMsg.Text = "ผลการตรวจสอบคุณสมบัติทางด้านถิ่นกำเนิดของสินค้าหมดอายุ ไม่สามารถใช้งานได้!!"
                    End If

                    lblCertOfOrigin_No.Text = .Item("certoforigin_no").ToString()
                    lblCertOfOrigin_date.Text = GetDateThai(.Item("certoforigin_date").ToString())
                    lblExpireDate.Text = GetDateThai(.Item("Cert_ExpiredDate2").ToString())
                    lblCompany_name_thai.Text = .Item("company_name_th").ToString()
                    lblCompany_name_eng.Text = .Item("company_name_en").ToString()
                    lblTaxId.Text = .Item("tax_id").ToString()
                    lblGood_desc_th.Text = .Item("goods_desc_th").ToString()
                    lblGood_name_eng.Text = .Item("goods_desc_en").ToString()
                    lblCountry.Text = .Item("country").ToString()
                    lblHamonized_no.Text = .Item("harmonized_no").ToString()
                    lblModels.Text = .Item("models").ToString()
                    lblCompany_name_th2.Text = .Item("company_name_th").ToString().Replace("บริษัท", "")
                End With

            Else
                lblMsg.Text = "ไม่พบข้อมูลต้นทุน"
            End If
        Catch ex As Exception
            ''Return 3
            lblMsg.Text = "เกิดข้อผิดพลาด : " & ex.Message
        End Try
    End Sub

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