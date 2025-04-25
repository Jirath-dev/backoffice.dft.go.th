Public Class DataUtil
    Public Shared Function ConvertToString(obj As Object) As String
        Dim ret As String = ""
        Try
            ret = Convert.ToString(obj)
        Catch ex As Exception

        End Try

        Return ret

    End Function

    Public Shared Function ConvertToInteger(obj As Object) As Integer
        Dim ret As Integer = 0
        Try
            ret = Convert.ToInt32(obj)
        Catch ex As Exception

        End Try

        Return ret

    End Function

    Public Shared Function ConvertToDouble(obj As Object) As Double
        Dim ret As Double = 0
        Try
            ret = Convert.ToDouble(obj)
        Catch ex As Exception

        End Try

        Return ret

    End Function

    Public Shared Function ConvertToBoolean(obj As Object) As Boolean
        Dim ret As Boolean = False
        Try
            ret = Convert.ToBoolean(obj)
        Catch ex As Exception

        End Try

        Return ret

    End Function

End Class
