Imports DataDynamics.ActiveReports 
Imports DataDynamics.ActiveReports.Document
Imports DFT.Dotnetnuke.ClassLibrary

Public Class rptB_Report_01 
    Dim Receitp_Y As Integer = 0
    Dim Receitp_N As Integer = 0
    Dim Receipt_Amount_Y As Decimal = 0.0
    Dim Receipt_Amount_N As Decimal = 0.0
    Dim Cancel_Num As Integer = 0
    Dim Cancel_Value As Decimal = 0.0

    Private Sub Detail1_Format(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Detail1.Format
        Try
            If txtActive_status.Text.Trim() = "N" Then
                txtReceipt_No.Text = "* " & txtReceipt_No.Text
                Receitp_N += CommonUtility.Get_Int32(txtReceipt_Amount.Text.Replace(",", ""))
                Receipt_Amount_N += CommonUtility.Get_Decimal(txtReceipt_Total.Text)

                Cancel_Num += CommonUtility.Get_Int32(txtReceipt_Amount.Text)
                Cancel_Value += CommonUtility.Get_Decimal(txtReceipt_Total.Text)
            ElseIf txtActive_status.Text.Trim() = "Y" Then
                Receitp_Y += CommonUtility.Get_Int32(txtReceipt_Amount.Text.Replace(",", ""))
                Receipt_Amount_Y += CommonUtility.Get_Decimal(txtReceipt_Total.Text)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ReportFooter1_Format(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ReportFooter1.Format
        Try
            txtReceipt_Y.Text = CommonUtility.Get_StringValue(Receitp_Y)
            txtReceipt_Amount_Y.Text = CommonUtility.Get_StringValue(Receipt_Amount_Y)
            txtReceipt_Amount_Y.Text = CDec(txtReceipt_Amount_Y.Text).ToString("#,###0.00")

            txtReceipt_N.Text = CommonUtility.Get_StringValue(Receitp_N)
            txtReceipt_Amount_N.Text = CommonUtility.Get_StringValue(Receipt_Amount_N)
            txtReceipt_Amount_N.Text = CDec(txtReceipt_Amount_N.Text).ToString("#,###0.00")
        Catch ex As Exception

        End Try
    End Sub

    Private Sub GroupFooter1_Format(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GroupFooter1.Format
        Try
            txtSumReceipt_Amount.Text = CInt(CInt(txtSumReceipt_Amount.Text) - CInt(Cancel_Num)).ToString("#,###0.00")
            txtSumReceipt_Total.Text = CInt(CInt(txtSumReceipt_Total.Text) - CInt(Cancel_Value)).ToString("#,###0.00")

            Cancel_Num = 0
            Cancel_Value = 0.0
        Catch ex As Exception

        End Try
    End Sub
End Class
