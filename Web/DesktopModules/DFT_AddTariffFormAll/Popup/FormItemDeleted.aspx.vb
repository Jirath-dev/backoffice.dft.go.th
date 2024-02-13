Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient

Imports Telerik.Web.UI
Imports DFT.Dotnetnuke.ClassLibrary

Partial Public Class FormItemDeleted
    Inherits System.Web.UI.Page
    Dim strConn As String = ConfigurationManager.ConnectionStrings("OriginConnection").ConnectionString

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            btnDelete.Focus()
            If Request.QueryString("Itariff_code") <> "" And Request.QueryString("Icountry_code") <> "" And Not (Request.QueryString("Icat") Is Nothing) Then 'form2_1
                txtItariff_code.Text = Request.QueryString("Itariff_code")
                txtIcountry_code.Text = Request.QueryString("Icountry_code")
                txtCAT.Text = Request.QueryString("Icat")
                txtform.Text = Request.QueryString("Iform")
            Else 'form อื่น
                txtItariff_code.Text = Request.QueryString("Itariff_code")
                txtIcountry_code.Text = Request.QueryString("Icountry_code")
                txtCAT.Text = ""
                txtform.Text = Request.QueryString("Iform")
            End If
        End If
    End Sub

    Private Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim iReturn As Integer
        iReturn = SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, "vi_common_form_edi_deleteTariffByDataKey_NewDS", _
            New SqlParameter("@tariff_code", txtItariff_code.Text), New SqlParameter("@country_code", txtIcountry_code.Text), _
            New SqlParameter("@CAT", txtCAT.Text), New SqlParameter("@FORM_TYPE", txtform.Text))

        If iReturn = 1 Then
            ClientScript.RegisterStartupScript(Page.GetType(), "mykey", "CloseAndRebind();", True)
        Else
            Response.Write("<script type='text/javascript'> alert('เกิดข้อผิดพลาดไม่สามารถทำการลบข้อมูลได้  กรุณาติดต่อผู้ดูแลระบบ!!!')</script>")
        End If
    End Sub

    Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        ClientScript.RegisterStartupScript(Page.GetType(), "mykey", "CancelEdit();", True)

    End Sub
End Class