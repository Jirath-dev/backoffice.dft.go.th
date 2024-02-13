Public Class ConvertThaiBath
    Public Shared Function BahtText(ByVal InputCurrency As Decimal) As String
        Dim DigitSave, UnitSave, DigitName, DigitName1, UnitName, Satang, StrTmp, StrTmp1 As String
        Dim DecimalValue, CurrDigit, PrevDigit, StrLen, DigitBase, ScanDigit As Integer
        Dim IntegerValue As Double

        ' init variable
        DigitName = "ศูนย์หนึ่งสอง  สาม  สี่  ห้า  หก   เจ็ด แปด  เก้า "    ' name of digit number
        DigitName1 = "          ยี่  สาม  สี่  ห้า  หก   เจ็ด แปด  เก้า "        ' name of digit number in another call
        UnitName = "แสน  ล้าน สิบ  ร้อย พัน  หมื่น"                             ' name of digit base
        BahtText = ""
        Satang = ""

        ' check for negative val
        If InputCurrency < 0 Then
            InputCurrency = -InputCurrency
            BahtText = "ลบ"
        End If

        StrTmp1 = Format(InputCurrency, "0.00")             ' rounds up to 2 decimals
        InputCurrency = Val(StrTmp1)
        IntegerValue = Int(InputCurrency)                           ' get integer value
        DecimalValue = (InputCurrency - IntegerValue) * 100             ' get 2 decimal values

        ' check for zeto val
        If IntegerValue = 0 And DecimalValue = 0 Then
            Satang = "ศูนย์บาทถ้วน"
            GoTo locExit
        End If

        ' translate integer val to name if necesary
        If IntegerValue > 0 Then
            StrTmp = Left(StrTmp1, Len(StrTmp1) - 3)          ' get string of integer val
            StrLen = Len(StrTmp)                                  ' get string len
            CurrDigit = 0

            ' scan integer string and compute its name
            For ScanDigit = StrLen To 1 Step -1
                ' save previous digit
                PrevDigit = CurrDigit
                ' get digit base
                DigitBase = ScanDigit Mod 6
                ' convert digit character to numeric value
                CurrDigit = Asc(Mid(StrTmp, StrLen - ScanDigit + 1, 1)) - 48
                ' get unit name from its base
                UnitSave = RTrim(Mid(UnitName, DigitBase * 5 + 1, 5))
                ' get number name from Currdigit, depends on the digit base
                DigitSave = RTrim(Mid(IIf(DigitBase = 2, DigitName1, DigitName), CurrDigit * 5 + 1, 5))

                ' base ten and number 1
                If DigitBase = 1 And CurrDigit = 1 And PrevDigit <> 0 Then
                    DigitSave = "เอ็ด"
                End If

                ' first digit base may be base million or 1
                If DigitBase = 1 And ScanDigit < 6 Then
                    UnitSave = ""
                End If

                ' ignore add digit name in result string if it is zero
                If CurrDigit <> 0 Then
                    BahtText = BahtText + DigitSave + UnitSave
                ElseIf DigitBase = 1 Then
                    BahtText = BahtText + UnitSave
                End If
            Next ScanDigit

            BahtText = BahtText + "บาท"
        End If

        ' if no decimal value
        If DecimalValue = 0 Then
            Satang = "ถ้วน"
            ' compute decimal val to name, there are only 2 digit
        Else
            StrTmp = Right(StrTmp1, 2)

            ' name ot first digit
            CurrDigit = Asc(Left(StrTmp, 1)) - 48
            PrevDigit = CurrDigit

            If CurrDigit > 0 Then
                Satang = RTrim(Mid(DigitName1, CurrDigit * 5 + 1, 5)) + "สิบ"
            End If

            ' name of last digit
            CurrDigit = Asc(Right(StrTmp, 1)) - 48

            If CurrDigit > 0 Then
                Satang = Satang + IIf(CurrDigit = 1 And PrevDigit <> 0, "เอ็ด", RTrim(Mid(DigitName, CurrDigit * 5 + 1, 5)))
            End If

            ' store result and unit
            Satang = Satang + "สตางค์"
        End If

locExit:
        ' store result to BahtText
        BahtText = BahtText + Satang
    End Function

    '----------------------------------------------------------------------------------------------------------------------------------
    ' Function: TLeft
    ' Purpose: Returns a specified number of Thai cells from the left side of a string
    '----------------------------------------------------------------------------------------------------------------------------------
    Public Shared Function TLeft(ByVal InputString As String, ByVal CellLength As Integer) As Object
        TLeft = Left(InputString, TCell2Count(InputString, CellLength, 1, False))
    End Function

    '---------------------------------------------------------------------------------------------------------------------------------
    ' Function: TRight
    ' Purpose: Returns a specified number of Thai cells from the right side of a string
    '----------------------------------------------------------------------------------------------------------------------------------
    Public Shared Function TRight(ByVal InputString As String, ByVal CellLength As Integer) As Object
        TRight = Right(InputString, TCell2Count(InputString, CellLength, TCell2Count(InputString, TLen(InputString) - CellLength, 1, False) + 1, False))
    End Function

    '---------------------------------------------------------------------------------------------------------------------------------
    ' Function: TMid
    ' Purpose: Returns a specified number of Thai cells from a string.
    '----------------------------------------------------------------------------------------------------------------------------------
    Public Shared Function TMid(ByVal InputString As String, ByVal ChopCell As Integer, ByVal ChopLength As Integer) As Object
        Dim StartChopByte As Integer

        ' validate parameter range
        ChopCell = max(ChopCell, 0)
        ChopLength = max(ChopLength, 0)

        ' get start byte form requested cell
        StartChopByte = TCell2Count(InputString, ChopCell, 1, True)

        ' get sub-string by Mid()
        TMid = Mid(InputString, StartChopByte, TCell2Count(InputString, ChopLength, StartChopByte, False))
    End Function
    '----------------------------------------------------------------------------------------------------------------------
    ' Function: TStr
    ' Purpose: Returns a Thai string representation of a number.
    '----------------------------------------------------------------------------------------------------------------------
    Public Shared Function TStr(ByVal InputNumber As Object) As Object
        Dim StringLength, StringScan As Integer
        Dim ch As String

        TStr = Str(InputNumber)
        StringLength = Len(TStr)

        For StringScan = 1 To StringLength
            ch = Mid(TStr, StringScan, 1)
            If (ch >= "0" And ch <= "9") Then
                Mid$(TStr, StringScan, 1) = Chr(Asc(ch) + 192)
            End If
        Next StringScan
    End Function

    '---------------------------------------------------------------------------------------
    ' Function: TLen
    ' Purpose: Returns the number of Thai cells in a string
    '---------------------------------------------------------------------------------------
    Public Shared Function TLen(ByVal InputString As String) As Integer
        Dim StringLength, StringScan As Integer

        StringLength = Len(InputString)
        TLen = 0

        For StringScan = 1 To StringLength
            If Not IsZeroWidthChar(Asc(Mid(InputString, StringScan, 1))) Then
                TLen = TLen + 1
            End If
        Next StringScan
    End Function

    '----------------------------------------------------------------------------------------------------------------
    ' Internal function: computes the maximum value between p1 and p2
    '----------------------------------------------------------------------------------------------------------------
    Public Shared Function max(ByVal p1 As Object, ByVal p2 As Object) As Object
        If p1 > p2 Then
            max = p1
        Else
            max = p2
        End If
    End Function
    '------------------------------------------------------------------------
    ' Internal function: IsZeroWidthChar
    '------------------------------------------------------------------------
    Public Shared Function IsZeroWidthChar(ByVal ch As Integer) As Integer
        IsZeroWidthChar = (ch = 209) Or (ch > 211 And ch < 219) Or (ch > 230 And ch < 239)
    End Function
    '-----------------------------------------------------------------------------------------------------------------
    ' Internal function: TCell2Count counts the number of bytes in Thai cells
    '-----------------------------------------------------------------------------------------------------------------
    Public Shared Function TCell2Count(ByVal StringInput As String, ByVal CellLength As Integer, ByVal StartScan As Integer, ByVal SWMode As Integer) As Integer
        Dim StringInputLength, CellCount As Integer

        StringInputLength = Len(StringInput)
        CellCount = 0
        TCell2Count = 0
        StartScan = max(StartScan, 1)
        CellLength = max(CellLength, 0)

        While TCell2Count + StartScan <= StringInputLength And CellCount <> CellLength
            If Not IsZeroWidthChar(Asc(Mid(StringInput, TCell2Count + StartScan, 1))) Then
                CellCount = CellCount + 1
            End If
            TCell2Count = TCell2Count + 1
        End While

        If Not SWMode Then
            While TCell2Count + StartScan <= StringInputLength
                If IsZeroWidthChar(Asc(Mid(StringInput, TCell2Count + StartScan, 1))) Then
                    TCell2Count = TCell2Count + 1
                Else
                    GoTo locExit
                End If
            End While
        End If
locExit:
    End Function
End Class
