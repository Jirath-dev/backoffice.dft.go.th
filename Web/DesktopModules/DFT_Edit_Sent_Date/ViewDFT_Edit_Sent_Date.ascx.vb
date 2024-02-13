Imports System.Web.UI
Imports System.Collections.Generic
Imports System.Reflection

Imports DotNetNuke
Imports DotNetNuke.Services.Exceptions
Imports DotNetNuke.Services.Localization

Imports Microsoft.ApplicationBlocks.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI

Namespace YourCompany.Modules.DFT_Edit_Sent_Date

    Partial Class ViewDFT_Edit_Sent_Date
        Inherits Entities.Modules.PortalModuleBase

        Dim strEDIConn As String = ConfigurationManager.ConnectionStrings("OriginConnection").ConnectionString

        Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            If Not Page.IsPostBack Then

            End If
        End Sub

        Protected Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
            If txtInvRunAuto.Text = "" Then
                lblErrors.Text = "กรุณาป้อนเลขที่อ้างอิง"
                lblErrors.ForeColor = Drawing.Color.Red
                txtInvRunAuto.Focus()
                btnSave.Enabled = False
            Else
                LoadData()
                btnSave.Enabled = True
            End If
        End Sub

        Sub LoadData()
            Try
                Dim Strcommand As String
                Dim npm As New SqlParameter("@INVHRUNAUTO", txtInvRunAuto.Text.Trim)
                Strcommand = " SELECT h.invh_run_auto,h.company_taxno,h.company_name,h.sent_date,f.form_name " & _
                " FROM dbo.form_header_edi as H INNER JOIN dbo.form_type as F ON H.form_type = F.form_type " & _
                " WHERE (h.invh_run_auto = @INVHRUNAUTO) AND (reference_code2 IS NULL) AND (edi_status_id IN ('P', 'D'))  "

                Dim ds As New DataSet
                ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.Text, Strcommand, npm)
                If ds.Tables(0).Rows.Count > 0 Then
                    txtCompanyTaxno.Text = ds.Tables(0).Rows(0).Item("company_taxno")
                    txtCompanyName.Text = ds.Tables(0).Rows(0).Item("company_name")
                    txtFormName.Text = ds.Tables(0).Rows(0).Item("form_name")
                    rdpSentDate.SelectedDate = ds.Tables(0).Rows(0).Item("sent_date")
                Else
                    txtCompanyName.Text = ""
                    txtCompanyTaxno.Text = ""
                    txtFormName.Text = ""
                    lblErrors.Text = ""
                    rdpSentDate.SelectedDate = Nothing
                    btnSave.Enabled = False
                End If
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End Sub

        Function UpdateData()
            Try
                Dim Strcommand As String
                Strcommand = " UPDATE form_header_edi SET sent_date = @sent_date WHERE (invh_run_auto = @invh_run_auto) "

                Dim npm(1) As SqlParameter
                npm(0) = New SqlParameter("@invh_run_auto", txtInvRunAuto.Text.Trim)
                npm(1) = New SqlParameter("@sent_date", rdpSentDate.SelectedDate)

                SqlHelper.ExecuteNonQuery(strEDIConn, CommandType.Text, Strcommand, npm)
                Return True

            Catch ex As Exception
                Return False
                Response.Write(ex.Message)
            End Try
        End Function

        Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
            If UpdateData() = True Then
                lblErrors.Text = "บันทึกข้อมูลเรียบร้อย"
                lblErrors.ForeColor = Drawing.Color.Green
            Else
                lblErrors.Text = "เกิดข้อผิดพลาดไม่สามารถบันทึกข้อมูลได้"
                lblErrors.ForeColor = Drawing.Color.Red
            End If
        End Sub
    End Class

End Namespace
