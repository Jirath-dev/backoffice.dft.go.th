Imports DataDynamics.ActiveReports 
Imports DataDynamics.ActiveReports.Document 

Public Class rptEDI_Report_02_Type1 

    Private Sub Detail1_Format(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Detail1.Format
        Dim T_check As String = txtRep_by.Text
        Select Case txtTemp_formtype.Text.ToUpper
            Case "FORM2_1"
                If T_check <> "" And T_check <> "0" Then
                    txtTemptotal_set.Text = CInt(txtRep_by.Text) / 60
                End If
            Case Else
                If T_check <> "" And T_check <> "0" Then
                    txtTemptotal_set.Text = CInt(txtRep_by.Text) / 30
                End If
        End Select

    End Sub
End Class
