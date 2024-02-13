Option Explicit On 
Imports System
Imports System.Collections

Public Class CommonUtility

    Public Sub New()
        ' MyBase
    End Sub


    Public Shared Function Is_Base64CharArray(ByVal val As String) As Boolean

        'No implementation provided yet
        Return False
    End Function

    Public Shared Function Is_Base64String(ByVal val As String) As Boolean

        'No implementation provided yet
        Return False
    End Function

    Public Shared Function Is_Boolean(ByVal val As String) As Boolean

        Try
            Dim temp As Boolean = Convert.ToBoolean(val)
            Return True

        Catch
            Return False
        End Try
    End Function



    Public Shared Function Is_Byte(ByVal val As String) As Boolean

        Try
            Dim temp As Byte = Convert.ToByte(val)
            Return True

        Catch
            Return False
        End Try
    End Function

    Public Shared Function Is_Char(ByVal val As String) As Boolean

        Try
            Dim temp As Char = Convert.ToChar(val)
            Return True

        Catch
            Return False
        End Try
    End Function

    Public Shared Function Is_DateTime(ByVal val As String) As Boolean

        Try
            Dim temp As DateTime = Convert.ToDateTime(val)
            Return True

        Catch
            Return False
        End Try
    End Function

    Public Shared Function Is_Decimal(ByVal val As String) As Boolean

        Try
            Dim temp As Decimal = Convert.ToDecimal(val)
            Return True

        Catch
            Return False
        End Try
    End Function

    Public Shared Function Is_Double(ByVal val As String) As Boolean

        Try
            Dim temp As Double = Convert.ToDouble(val)
            Return True

        Catch
            Return False
        End Try
    End Function

    Public Shared Function Is_Int16(ByVal val As String) As Boolean

        Try
            Dim temp As Int16 = Convert.ToInt16(val)
            Return True

        Catch
            Return False
        End Try
    End Function

    Public Shared Function Is_Int32(ByVal val As String) As Boolean

        Try
            Dim temp As Int32 = Convert.ToInt32(val)
            Return True

        Catch
            Return False
        End Try
    End Function

    Public Shared Function Is_Int64(ByVal val As String) As Boolean

        Try
            Dim temp As Int64 = Convert.ToInt64(val)
            Return True

        Catch
            Return False
        End Try
    End Function

    Public Shared Function Is_SByte(ByVal val As String) As Boolean

        Try
            Dim temp As Byte = Convert.ToByte(val)
            Return True

        Catch
            Return False
        End Try
    End Function

    Public Shared Function Is_Single(ByVal val As String) As Boolean

        Try
            Dim temp As Single = Convert.ToSingle(val)
            Return True

        Catch
            Return False
        End Try
    End Function

    Public Shared Function Is_String(ByVal val As String) As Boolean

        Try
            Dim temp As String = Convert.ToString(val)
            Return True

        Catch
            Return False
        End Try
    End Function

    Public Shared Function Is_UInt16(ByVal val As String) As Boolean

        Try
            Dim temp As UInt16 = Convert.ToUInt16(val)
            Return True

        Catch
            Return False
        End Try
    End Function

    Public Shared Function Is_UInt32(ByVal val As String) As Boolean

        Try
            Dim temp As UInt32 = Convert.ToUInt32(val)
            Return True

        Catch
            Return False
        End Try
    End Function

    Public Shared Function Is_UInt64(ByVal val As String) As Boolean

        Try
            Dim temp As UInt32 = Convert.ToUInt32(val)
            Return True

        Catch
            Return False
        End Try
    End Function

    Public Shared Function Is_Guid(ByVal val As String) As Boolean
        Try
            Dim szGuid As New Guid(val)
            Return True
        Catch
            Return False
        End Try
    End Function

    Public Shared Function Is_ByteArray(ByVal val As String) As Boolean
        Return True
    End Function

    Public Shared Sub Get_Base64CharArray(ByVal val As String)

        'No implementation provided yet
    End Sub

    Public Shared Sub Get_Base64String(ByVal val As String)

        'No implementation provided yet
    End Sub

    Public Shared Function Get_Boolean(ByVal val As String) As Boolean

        If val <> "" Then
            Return Convert.ToBoolean(val)
        End If
        Return False
    End Function

    Public Shared Function Get_Byte(ByVal val As String) As Byte

        If val <> "" Then
            Return Convert.ToByte(val)
        End If
        Return 0
    End Function

    Public Shared Function Get_Char(ByVal val As String) As Char

        Return Convert.ToChar(val)
    End Function

    Public Shared Function Get_DateTime(ByVal val As String) As DateTime

        val = val.ToLower().Trim()
        If (val.StartsWith("today")) Then

            If val = "today" Then
                Return DateTime.Today
            End If
            Try
                Dim htValues As Hashtable
                htValues = GetAllValues(val.Substring(5))
                Dim strTimeVal As String = htValues("TimeVal").ToString()
                Select Case strTimeVal

                    Case "Days"
                        Dim days As Double = Convert.ToDouble(htValues("Operator").ToString() + htValues("Number").ToString())
                        Return DateTime.Today.AddDays(days)
                    Case "Months"
                        Dim Months As Int32 = Convert.ToInt32(htValues("Operator").ToString() + htValues("Number").ToString())
                        Return DateTime.Today.AddMonths(Months)
                    Case "Years"
                        Dim years As Int32 = Convert.ToInt32(htValues("Operator").ToString() + htValues("Number").ToString())
                        Return DateTime.Today.AddYears(years)
                    Case Else
                        Return DateTime.Today
                End Select
            Catch ex As Exception
                Return DateTime.Today
            End Try

        End If
        If val <> "" Then
            Return Convert.ToDateTime(val)
        End If
        Return New DateTime
    End Function

    Public Shared Function GetAllValues(ByVal val As String) As Hashtable

        val = val.Trim()
        Dim ht As Hashtable = New Hashtable(3)
        Dim strMember As String = ""
        strMember = val.Substring(0, 1)
        If strMember = "+" Or strMember = "-" Then
            ht.Add("Operator", strMember)
        End If

        Dim nEndChars As Int32 = 0
        strMember = val.Substring(1).Trim()

        If strMember.EndsWith("days") Or strMember.EndsWith("day") Then
            ht.Add("TimeVal", "Days")
            If strMember.EndsWith("days") Then
                nEndChars = 4
            Else
                nEndChars = 3
            End If
        ElseIf strMember.EndsWith("months") Or strMember.EndsWith("month") Then
            ht.Add("TimeVal", "Months")
            If strMember.EndsWith("months") Then
                nEndChars = 6
            Else
                nEndChars = 5
            End If

        ElseIf strMember.EndsWith("years") Or strMember.EndsWith("year") Then
            ht.Add("TimeVal", "Years")
            If strMember.EndsWith("years") Then
                nEndChars = 5
            Else
                nEndChars = 4
            End If
        Else
            ht.Add("TimeVal", "Days")
        End If

        strMember = strMember.Substring(0, (strMember.Length - nEndChars))
        strMember = strMember.Trim()
        ht.Add("Number", strMember)

        Return ht
    End Function

    Public Shared Function Get_Decimal(ByVal val As String) As Decimal

        If val <> "" Then
            Return Convert.ToDecimal(val)
        End If
        Return 0.0
    End Function

    Public Shared Function Get_Double(ByVal val As String) As Double

        If val <> "" Then
            Return Convert.ToDouble(val)
        End If
        Return 0.0
    End Function

    Public Shared Function Get_Int16(ByVal val As String) As Int16

        If val <> "" Then
            Return Convert.ToInt16(val)
        End If
        Return 0
    End Function

    Public Shared Function Get_Int32(ByVal val As String) As Int32

        If val <> "" Then
            Return Convert.ToInt32(val)
        End If
        Return 0
    End Function

    Public Shared Function Get_Int64(ByVal val As String) As Int64

        If val <> "" Then
            Return Convert.ToInt64(val)
        End If
        Return 0
    End Function

    <CLSCompliant(False)> _
           Public Shared Function Get_SByte(ByVal val As String) As [SByte]

        Return Convert.ToSByte(val)
    End Function

    Public Shared Function Get_Single(ByVal val As String) As Single

        If val <> "" Then
            Return Convert.ToSingle(val)
        End If
        Return 0.0
    End Function

    Public Shared Function Get_String(ByVal val As String) As String

        Return Convert.ToString(val)
    End Function

    <CLSCompliant(False)> _
           Public Shared Function Get_UInt16(ByVal val As String) As UInt16

        Return Convert.ToUInt16(val)
    End Function

    <CLSCompliant(False)> _
           Public Shared Function Get_UInt32(ByVal val As String) As UInt32

        Return Convert.ToUInt32(val)
    End Function

    <CLSCompliant(False)> _
           Public Shared Function Get_UInt64(ByVal val As String) As UInt64

        Return Convert.ToUInt64(val)
    End Function

    Public Shared Function Get_Object(ByVal val As String) As String
        Return val
    End Function

    Public Shared Function Get_Guid(ByVal val As String) As Guid
        Dim szGuid As New Guid(val)
        Return szGuid
    End Function

    Public Shared Function Get_ByteArray(ByVal val As String) As Byte()

        Return System.Text.Encoding.Default.GetBytes(val)

    End Function

    Public Shared Function Validate_Base64CharArray(ByVal val As String, ByVal fieldName As String, ByVal isRequired As Boolean) As String

        'No implementation provided yet
        Return ""
    End Function

    Public Shared Function Validate_Base64String(ByVal val As String, ByVal fieldName As String, ByVal isRequired As Boolean) As String

        'No implementation provided yet
        Return ""
    End Function

    Public Shared Function Validate_Boolean(ByVal val As String, ByVal fieldName As String, ByVal isRequired As Boolean, ByVal maxRange As String, ByVal minRange As String) As String

        val = val.Trim()
        If val = "" Then

            If isRequired Then
                Return "Field " + fieldName + " cannot be left empty"
            Else
                Return ""
            End If
        End If

        If Is_Boolean(val) Then
            Return ""
        Else
            Return "Field " + fieldName + " should have some valid value."
        End If
    End Function

    Public Shared Function Validate_Byte(ByVal val As String, ByVal fieldName As String, ByVal isRequired As Boolean, ByVal maxRange As String, ByVal minRange As String) As String

        val = val.Trim()
        If val = "" Then

            If isRequired Then
                Return "Field " + fieldName + " cannot be left empty"
            Else
                Return ""
            End If
        End If

        If Is_Byte(val) Then
            If maxRange.Equals("0") And minRange.Equals("0") Then
                Return ""
            Else
                Dim temp As Byte = Get_Byte(val)
                If Not maxRange.Equals("0") Then
                    If temp.CompareTo(Get_Byte(maxRange)) > 0 Then
                        Return "Field " + fieldName + " cannot be greater than " + maxRange
                    End If
                End If
                If Not minRange.Equals("0") Then
                    If temp.CompareTo(Get_Byte(minRange)) < 0 Then
                        Return "Field " + fieldName + " cannot be less than " + minRange
                    End If
                End If
            End If
            Return ""
        Else
            Return "Field " + fieldName + " should have some valid value."
        End If

    End Function

    Public Shared Function Validate_Char(ByVal val As String, ByVal fieldName As String, ByVal isRequired As Boolean) As String

        val = val.Trim()
        If val = "" Then

            If isRequired Then
                Return "Field " + fieldName + " cannot be left empty"
            Else
                Return ""
            End If
        End If

        If Is_Char(val) Then
            Return ""
        Else
            Return "Field " + fieldName + " should have some valid value."
        End If

    End Function

    Public Shared Function Validate_DateTime(ByVal val As String, ByVal fieldName As String, ByVal isRequired As Boolean, ByVal maxRange As String, ByVal minRange As String) As String

        val = val.Trim()
        If val = "" Then

            If isRequired Then
                Return "Field " + fieldName + " cannot be left empty"
            Else
                Return ""
            End If
        End If

        If Is_DateTime(val) Then
            Return ""
        Else
            Return "Field " + fieldName + " should have some valid value."
        End If
    End Function

    Public Shared Function Validate_Decimal(ByVal val As String, ByVal fieldName As String, ByVal isRequired As Boolean, ByVal maxRange As String, ByVal minRange As String) As String

        val = val.Trim()
        If val = "" Then

            If isRequired Then
                Return "Field " + fieldName + " cannot be left empty"
            Else
                Return ""
            End If
        End If
        If Is_Decimal(val) Then
            If maxRange.Equals("0") And minRange.Equals("0") Then
                Return ""
            Else
                Dim temp As Decimal = Get_Decimal(val)
                If Not maxRange.Equals("0") Then
                    If temp.CompareTo(Get_Decimal(maxRange)) > 0 Then
                        Return "Field " + fieldName + " cannot be greater than " + maxRange
                    End If
                End If
                If Not minRange.Equals("0") Then
                    If temp.CompareTo(Get_Decimal(minRange)) < 0 Then
                        Return "Field " + fieldName + " cannot be less than " + minRange
                    End If
                End If
            End If
            Return ""
        Else
            Return "Field " + fieldName + " should have some valid value."
        End If

    End Function

    Public Shared Function Validate_Double(ByVal val As String, ByVal fieldName As String, ByVal isRequired As Boolean, ByVal maxRange As String, ByVal minRange As String) As String

        val = val.Trim()
        If val = "" Then

            If isRequired Then
                Return "Field " + fieldName + " cannot be left empty"
            Else
                Return ""
            End If
        End If
        If Is_Double(val) Then
            If maxRange.Equals("0") And minRange.Equals("0") Then
                Return ""
            Else
                Dim temp As Double = Get_Double(val)
                If Not maxRange.Equals("0") Then
                    If temp.CompareTo(Get_Double(maxRange)) > 0 Then
                        Return "Field " + fieldName + " cannot be greater than " + maxRange
                    End If
                End If
                If Not minRange.Equals("0") Then
                    If temp.CompareTo(Get_Double(minRange)) < 0 Then
                        Return "Field " + fieldName + " cannot be less than " + minRange
                    End If
                End If
            End If
            Return ""
        Else
            Return "Field " + fieldName + " should have some valid value."
        End If
    End Function

    Public Shared Function Validate_Int16(ByVal val As String, ByVal fieldName As String, ByVal isRequired As Boolean, ByVal maxRange As String, ByVal minRange As String) As String

        val = val.Trim()
        If val = "" Then

            If isRequired Then
                Return "Field " + fieldName + " cannot be left empty"
            Else
                Return ""
            End If
        End If
        If Is_Int16(val) Then
            If maxRange.Equals("0") And minRange.Equals("0") Then
                Return ""
            Else
                Dim temp As Int16 = Get_Int16(val)
                If Not maxRange.Equals("0") Then
                    If temp.CompareTo(Get_Int16(maxRange)) > 0 Then
                        Return "Field " + fieldName + " cannot be greater than " + maxRange
                    End If
                End If
                If Not minRange.Equals("0") Then
                    If temp.CompareTo(Get_Int16(minRange)) < 0 Then
                        Return "Field " + fieldName + " cannot be less than " + minRange
                    End If
                End If
            End If
            Return ""
        Else
            Return "Field " + fieldName + " should have some valid value."
        End If
    End Function

    Public Shared Function Validate_Int32(ByVal val As String, ByVal fieldName As String, ByVal isRequired As Boolean, ByVal maxRange As String, ByVal minRange As String) As String

        val = val.Trim()
        If val = "" Then

            If isRequired Then
                Return "Field " + fieldName + " cannot be left empty"
            Else
                Return ""
            End If
        End If
        If Is_Int32(val) Then
            If maxRange.Equals("0") And minRange.Equals("0") Then
                Return ""
            Else
                Dim temp As Int32 = Get_Int32(val)
                If Not maxRange.Equals("0") Then
                    If temp.CompareTo(Get_Int32(maxRange)) > 0 Then
                        Return "Field " + fieldName + " cannot be greater than " + maxRange
                    End If
                End If
                If Not minRange.Equals("0") Then
                    If temp.CompareTo(Get_Int32(minRange)) < 0 Then
                        Return "Field " + fieldName + " cannot be less than " + minRange
                    End If
                End If
            End If
            Return ""
        Else
            Return "Field " + fieldName + " should have some valid value."
        End If

    End Function

    Public Shared Function Validate_Int64(ByVal val As String, ByVal fieldName As String, ByVal isRequired As Boolean, ByVal maxRange As String, ByVal minRange As String) As String

        val = val.Trim()
        If val = "" Then

            If isRequired Then
                Return "Field " + fieldName + " cannot be left empty"
            Else
                Return ""
            End If
        End If
        If Is_Int64(val) Then
            If maxRange.Equals("0") And minRange.Equals("0") Then
                Return ""
            Else
                Dim temp As Int64 = Get_Int64(val)
                If Not maxRange.Equals("0") Then
                    If temp.CompareTo(Get_Int64(maxRange)) > 0 Then
                        Return "Field " + fieldName + " cannot be greater than " + maxRange
                    End If
                End If
                If Not minRange.Equals("0") Then
                    If temp.CompareTo(Get_Int64(minRange)) < 0 Then
                        Return "Field " + fieldName + " cannot be less than " + minRange
                    End If
                End If
            End If
            Return ""
        Else
            Return "Field " + fieldName + " should have some valid value."
        End If
    End Function

    Public Shared Function Validate_SByte(ByVal val As String, ByVal fieldName As String, ByVal isRequired As Boolean) As String

        val = val.Trim()
        If val = "" Then

            If isRequired Then
                Return "Field " + fieldName + " cannot be left empty"
            Else
                Return ""
            End If
        End If

        If Is_SByte(val) Then
            Return ""
        Else
            Return "Field " + fieldName + " should have some valid value."
        End If

    End Function

    Public Shared Function Validate_Single(ByVal val As String, ByVal fieldName As String, ByVal isRequired As Boolean, ByVal maxRange As String, ByVal minRange As String) As String

        val = val.Trim()
        If val = "" Then

            If isRequired Then
                Return "Field " + fieldName + " cannot be left empty"
            Else
                Return ""
            End If
        End If

        If Is_Single(val) Then
            If maxRange.Equals("0") And minRange.Equals("0") Then
                Return ""
            Else
                Dim temp As Single = Get_Single(val)
                If Not maxRange.Equals("0") Then
                    If temp.CompareTo(Get_Single(maxRange)) > 0 Then
                        Return "Field " + fieldName + " cannot be greater than " + maxRange
                    End If
                End If
                If Not minRange.Equals("0") Then
                    If temp.CompareTo(Get_Single(minRange)) < 0 Then
                        Return "Field " + fieldName + " cannot be less than " + minRange
                    End If
                End If
            End If
            Return ""
        Else
            Return "Field " + fieldName + " should have some valid value."
        End If
    End Function

    Public Shared Function Validate_String(ByVal val As String, ByVal fieldName As String, ByVal isRequired As Boolean, ByVal maxRange As String, ByVal minRange As String) As String

        val = val.Trim()
        If val = "" Then

            If isRequired Then
                Return "Field " + fieldName + " cannot be left empty"
            Else
                Return ""
            End If
        End If
        If maxRange.Equals("0") And minRange.Equals("0") Then
            Return ""
        ElseIf val.Length > maxRange.Length Then
            Return "Field " + fieldName + " cannot be greater than " + maxRange
        End If
        Return ""

    End Function

    Public Shared Function Validate_UInt16(ByVal val As String, ByVal fieldName As String, ByVal isRequired As Boolean) As String

        val = val.Trim()
        If val = "" Then

            If isRequired Then
                Return "Field " + fieldName + " cannot be left empty"
            Else
                Return ""
            End If
        End If

        If Is_UInt16(val) Then
            Return ""
        Else
            Return "Field " + fieldName + " should have some valid value."
        End If

    End Function

    Public Shared Function Validate_UInt32(ByVal val As String, ByVal fieldName As String, ByVal isRequired As Boolean) As String

        val = val.Trim()

        If val = "" Then
            If isRequired Then
                Return "Field " + fieldName + " cannot be left empty"
            Else
                Return ""
            End If
        End If

        If Is_UInt32(val) Then
            Return ""
        Else
            Return "Field " + fieldName + " should have some valid value."
        End If

    End Function

    Public Shared Function Validate_UInt64(ByVal val As String, ByVal fieldName As String, ByVal isRequired As Boolean) As String

        val = val.Trim()
        If val = "" Then

            If isRequired Then
                Return "Field " + fieldName + " cannot be left empty"
            Else
                Return ""
            End If
        End If

        If Is_UInt32(val) Then
            Return ""
        Else
            Return "Field " + fieldName + " should have some valid value."
        End If
    End Function

    Public Shared Function Validate_Guid(ByVal val As String, ByVal fieldName As String, ByVal isRequired As Boolean, ByVal maxRange As String, ByVal minRange As String) As String
        val = val.Trim()
        If val = "" Then
            If isRequired Then
                Return "Field " + fieldName + " should have some valid value."
            Else
                Return ""
            End If
        End If

        If Is_Guid(val) Then
            Return ""
        Else
            Return "Field " + fieldName + " should have some valid value."
        End If
    End Function

    Public Shared Function Get_StringValue(ByVal Obj As Object) As String
        Dim typ As String
        typ = Convert.ToString(Obj)
        If typ.Equals("System.Byte[]") Then
            Dim byt() As Byte = Obj
            If byt.Length > 0 Then
                Return "(Binary Data)"
            Else
                Return ""
            End If
        Else
            Return typ
        End If
    End Function

    Public Shared Function Validate_ByteArray(ByVal val As String, ByVal fieldName As String, ByVal isRequired As Boolean, ByVal maxRange As String, ByVal minRange As String) As String
        val = val.Trim()
        If val = "" Then
            If isRequired Then
                Return "Field " + fieldName + " should have some valid value."
            Else
                Return ""
            End If
        Else
            If Is_ByteArray(val) Then
                Return ""
            Else
                Return "Field " + fieldName + " should have some valid value."
            End If
        End If
    End Function

    Public Shared Function Validate_EmptyValue(ByVal fVal As String, ByVal fFieldName As String, ByVal sVal As String, ByVal sFieldName As String) As String

        fVal = fVal.Trim()
        sVal = sVal.Trim()
        If fVal <> "" Or sVal <> "" Then
            If fVal = "" Then
                Return "Field " + fFieldName + " should have some valid value."
            ElseIf sVal = "" Then
                Return "Field " + sFieldName + " should have some valid value."
            Else
                Return ""
            End If
        Else
            Return ""
        End If
    End Function

End Class
Public Class ComboBoxItem

    Public Value As Object
    Public DisplayText As String

    Public Sub New(ByVal NewValue As Object, ByVal NewDisplayText As String)
        Value = NewValue
        DisplayText = NewDisplayText
    End Sub

    Public Overrides Function ToString() As String
        Return DisplayText
    End Function

End Class