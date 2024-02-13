Public Class DevUtil
    Public Shared Function convertToInt(ByVal val As String) As Integer
        Dim retvl As Integer = 0
        Try
            retvl = Convert.ToInt32(val.Replace(",", ""))
        Catch ex As Exception
            retvl = 0
        End Try
        Return retvl
    End Function

    Public Shared Function decimalFormat(ByVal str As String) As String
        Dim retval As String = "0"
        Try
            retval = String.Format("{0:N0}", Double.Parse(str))
        Catch ex As Exception
            retval = "0"
        End Try
        Return retval
    End Function

    Public Shared Function DistinctArray(ByVal list As Array) As List(Of String)
        Dim retList As New List(Of String)

        For i As Integer = 0 To list.Length - 1
            If Not retList.Contains(list(i)) Then
                retList.Add(list(i))
            End If
        Next

        Return retList

    End Function
End Class
