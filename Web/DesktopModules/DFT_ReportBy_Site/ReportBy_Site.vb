Imports DataDynamics.ActiveReports 
Imports DataDynamics.ActiveReports.Document 

Public Class ReportBy_Site 
    Dim Cpage As Integer
   Private Sub GroupHeader2_Format(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GroupHeader2.Format
        Cpage += 1
        txtNumRowCount.Text = Cpage & "."

    End Sub

    Private Sub GroupHeader1_Format(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GroupHeader1.Format

    End Sub

    Private Sub ReportBy_Site_PageStart(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PageStart
        'Me.CurrentPage.DrawLine(1, 1.49, 1, 9.85) 'เส้นซ้ายสุด
        'Me.CurrentPage.DrawLine(9.68, 1.49, 9.68, 9.85) 'เส้นขวาสุด
    End Sub
    'Sub _CheckNumType()
    '    Select Case txtform_type.Value
    '        Case "FORM1"
    '            Cpage = 0
    '        Case "FORM1_1"
    '            Cpage = 0
    '        Case "FORM1_2"
    '            Cpage = 0
    '        Case "FORM1_3"
    '            Cpage = 0
    '        Case "FORM1_4"
    '            Cpage = 0
    '        Case "FORMRussia"
    '            Cpage = 0
    '        Case "FORM2"
    '            Cpage = 0
    '        Case "FORM2_1"
    '            Cpage = 0
    '        Case "FORM2_2"
    '            Cpage = 0
    '        Case "FORM2_3"
    '            Cpage = 0
    '        Case "FORM2_4"
    '            Cpage = 0
    '        Case "FORM3", "FORM3_1"
    '            Cpage = 0
    '        Case "FORM4"
    '            Cpage = 0
    '        Case "FORM4_1"
    '            Cpage = 0
    '        Case "FORM4_2"
    '            Cpage = 0
    '        Case "FORM4_3"
    '            Cpage = 0
    '        Case "FORM4_4"
    '            Cpage = 0
    '        Case "FORM4_5"
    '            Cpage = 0
    '        Case "FORM4_6"
    '            Cpage = 0
    '        Case "FORM4_8"
    '            Cpage = 0
    '        Case "FORM4_9"
    '            Cpage = 0
    '        Case "FORM4_91"
    '            Cpage = 0
    '        Case "FORM5"
    '            Cpage = 0
    '        Case "FORM6"
    '            Cpage = 0
    '        Case "FORM7"
    '            Cpage = 0
    '        Case "FORM8"
    '            Cpage = 0
    '        Case "FORM9"
    '            Cpage = 0
    '    End Select
    'End Sub

    Private Sub ReportBy_Site_ReportStart(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.ReportStart
        Me.ReportInfo1.FormatString = "Page : {PageNumber} of {PageCount}"
    End Sub

End Class
