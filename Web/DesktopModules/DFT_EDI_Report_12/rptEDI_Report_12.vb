Imports DataDynamics.ActiveReports 
Imports DataDynamics.ActiveReports.Document 

Public Class rptEDI_Report_12 
    Dim index As Integer = 0

    Function SecondsToText(ByVal Seconds) As String
        Dim bAddComma As Boolean
        Dim Result As String
        Dim sTemp As String
        Dim days, hours, minutes

        If Seconds <= 0 Or Not IsNumeric(Seconds) Then
            SecondsToText = "00:00:00"
            Exit Function
        End If

        Seconds = Fix(Seconds)

        If Seconds >= 86400 Then
            days = Fix(Seconds / 86400)
        Else
            days = 0
        End If

        If Seconds - (days * 86400) >= 3600 Then
            hours = Fix((Seconds - (days * 86400)) / 3600)
        Else
            hours = 0
        End If

        If Seconds - (hours * 3600) - (days * 86400) >= 60 Then
            minutes = Fix((Seconds - (hours * 3600) - (days * 86400)) / 60)
        Else
            minutes = 0
        End If

        Seconds = Seconds - (minutes * 60) - (hours * 3600) - (days * 86400)

        If Seconds > 0 Then Result = Seconds '& ":"
        If Seconds = 0 Then Result = "00"

        If Result.Length = 1 Then
            Result = "0" & Result
        End If

        If minutes > 0 Then
            bAddComma = Result <> ""
            Dim a As String = "aaa"

            If CInt(minutes).ToString.Length = 1 Then
                sTemp = "0" & minutes & ":"

            Else
                sTemp = minutes & ":"
            End If

            If bAddComma Then sTemp = sTemp
            Result = sTemp & Result
        End If

        If minutes = 0 Then Result = "00:" & Result

        If hours > 0 Then
            bAddComma = Result <> ""

            If CInt(hours).ToString.Length = 1 Then
                sTemp = "0" & hours & ":"
            Else
                sTemp = hours & ":"
            End If

            If bAddComma Then sTemp = sTemp
            Result = sTemp & Result
        End If

        If hours = 0 Then Result = "00:" & Result

        SecondsToText = Result
    End Function

    Private Sub ReportHeader1_Format(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ReportHeader1.Format
        If Now.Month = 10 Or Now.Month = 11 Or Now.Month = 12 Then
            lblHeader.Text = Now.Year + 544
        Else
            lblHeader.Text = Now.Year + 543
        End If

    End Sub

    Private Sub Detail1_Format(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Detail1.Format
        index = index + 1
        lblIndex.Text = CType(index, String)

        Dim StartTime As Date = CDate(lblSTART_TIME.Text)
        Dim FinishTime As Date = CDate(lblFINISH_TIME.Text)

        Dim CAL_SEC As Decimal = CInt(DateDiff(DateInterval.Second, StartTime, FinishTime))
        Dim CAL_MIN As Decimal = CInt(DateDiff(DateInterval.Minute, StartTime, FinishTime))
        Dim FORM_COUNT As Decimal = CDec(lblFORM_COUNT.Text)
        lblAvgTime.Text = SecondsToText(CInt(CAL_SEC / FORM_COUNT))

        If CInt(CAL_MIN / FORM_COUNT) > 30 Then
            lblResult.Text = 0
        Else
            lblResult.Text = 1
        End If
        'แปลง Format เวลา
        'Dim dt As Date
        'dt = Convert.ToDateTime(lblSTART_TIME.Text)
        'lblSTART_TIME.Text = dt.ToString("T")

        'dt = Convert.ToDateTime(lblFINISH_TIME.Text)
        'lblFINISH_TIME.Text = dt.ToString("T")
    End Sub
End Class
