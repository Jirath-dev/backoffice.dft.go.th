Imports System.Web.UI
Imports System.Collections.Generic
Imports System.Reflection

Imports DotNetNuke
Imports DotNetNuke.Services.Exceptions
Imports DotNetNuke.Services.Localization

Namespace YourCompany.Modules.DFT_EDI_ChangePasswordCard

    Partial Class ViewDFT_EDI_ChangePasswordCard
        Inherits Entities.Modules.PortalModuleBase

        Dim connection As String = ""
        Dim ediConn As String = ConfigurationManager.ConnectionStrings("EDIConnection").ConnectionString
        Dim tradingConn As String = ConfigurationManager.ConnectionStrings("TradingConnection").ConnectionString

        Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
            SearchRfCardEDI(txtCardID.Text.Trim)
            SearchRfCardTrading(txtCardID.Text.Trim)

        End Sub

        Public Sub SearchRfCardEDI(ByVal cardID As String)
            Dim comm As String = "SELECT card_id, password FROM rfcard WHERE (card_id = @CardID)"
            Dim prm As New SqlClient.SqlParameter("@CardID", cardID)
            Dim ds As DataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ediConn, CommandType.Text, comm, prm)

            If ds.Tables(0).Rows.Count > 0 Then
                txtPassword_EDI.Text = ds.Tables(0).Rows(0).Item("password").ToString
                btnUpdatePassword.Enabled = True
                lblMSG.Visible = False
            Else
                txtPassword_EDI.Text = ""
                btnUpdatePassword.Enabled = False
                lblMSG.Text = "ไม่พบข้อมูลที่ค้นหา"
                lblMSG.ForeColor = Drawing.Color.Red
                lblMSG.Visible = True
            End If
        End Sub

        Public Sub SearchRfCardTrading(ByVal cardID As String)
            Dim comm As String = "SELECT card_id, password FROM P_Rfcard WHERE (card_id = @CardID)"
            Dim prm As New SqlClient.SqlParameter("@CardID", cardID)
            Dim ds As DataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(tradingConn, CommandType.Text, comm, prm)

            If ds.Tables(0).Rows.Count > 0 Then
                txtPassword_Trading.Text = ds.Tables(0).Rows(0).Item("password").ToString
                btnUpdatePassword.Enabled = True
                lblMSG.Visible = False
            Else
                txtPassword_Trading.Text = ""
                btnUpdatePassword.Enabled = False
                lblMSG.Text = "ไม่พบข้อมูลที่ค้นหา"
                lblMSG.ForeColor = Drawing.Color.Red
                lblMSG.Visible = True
            End If
        End Sub

        Public Function UpdatePasswordEDI(ByVal cardId As String, ByVal pass As String, ByVal conn As String) As Boolean

            Dim command As String = "UPDATE rfcard SET password = @password WHERE (card_id = @card_id)"
            Dim prm(2) As SqlClient.SqlParameter
            prm(0) = New SqlClient.SqlParameter("@card_id", cardId)
            prm(1) = New SqlClient.SqlParameter("@password", pass)
            Try
                Return Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(conn, CommandType.Text, command, prm)
            Catch ex As Exception
                Return False
            End Try

        End Function

        Public Function UpdatePasswordTrading(ByVal cardId As String, ByVal pass As String, ByVal conn As String) As Boolean

            Dim command As String = "UPDATE P_Rfcard SET password = @password WHERE (card_id = @card_id)"
            Dim prm(2) As SqlClient.SqlParameter
            prm(0) = New SqlClient.SqlParameter("@card_id", cardId)
            prm(1) = New SqlClient.SqlParameter("@password", pass)
            Try
                Return Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(conn, CommandType.Text, command, prm)
            Catch ex As Exception
                Return False
            End Try

        End Function

        Private Sub btnUpdatePassword_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdatePassword.Click
            Dim return_val As Boolean = False

            If rblSystemChange.SelectedValue = "EDI" Then
                return_val = UpdatePasswordEDI(txtCardID.Text.Trim, txtPassword_EDI.Text.Trim, ediConn)
            ElseIf rblSystemChange.SelectedValue = "TRADING" Then
                return_val = UpdatePasswordTrading(txtCardID.Text.Trim, txtPassword_Trading.Text.Trim, tradingConn)
            Else ''แก้ทั้ง 2 ระบบ
                return_val = UpdatePasswordEDI(txtCardID.Text.Trim, txtPassword_EDI.Text.Trim, ediConn) _
                             And _
                             UpdatePasswordTrading(txtCardID.Text.Trim, txtPassword_Trading.Text.Trim, tradingConn)
            End If

            If return_val Then
                lblMSG.Text = "บันทึกข้อมูลเรียบร้อยแล้ว"
                lblMSG.ForeColor = Drawing.Color.Blue
                lblMSG.Visible = True
            Else
                lblMSG.Text = "เกิดข้อผิดพลาด กรุณาติดต่อ โปรแกรมเมอร์"
                lblMSG.ForeColor = Drawing.Color.Red
                lblMSG.Visible = True
            End If
        End Sub
    End Class

End Namespace
