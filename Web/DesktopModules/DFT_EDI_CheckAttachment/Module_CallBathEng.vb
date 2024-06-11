Imports Microsoft.VisualBasic
Imports Microsoft.ApplicationBlocks.Data
Imports System.Data.SqlClient
Imports System.Data
Imports System.Text
Imports DFT.Dotnetnuke.ClassLibrary
Module Module_CallBathEng
    '===========================================
    'Printer
    Function CallPrintConfig(ByVal MyPrinter_ As String) As String
        Select Case MyPrinter_
            Case "ST-001"
                Return ConfigurationManager.AppSettings("printName_dev").ToString()
            Case ""
                Return ConfigurationManager.AppSettings("printName_Pdf").ToString()
        End Select

    End Function

    '===========================================
    'Bath Eng
    Function BahtOnly(ByVal MyNumber, ByVal Myq_unit_code)
        Dim F_Mynumber As String = MyNumber
        Dim Baht, Temp
        Dim Satang As String = ""
        Dim DecimalPlace, Count

        Dim Place() As String
        ReDim Place(9)
        Place(2) = " THOUSAND "
        Place(3) = " MILLION "
        Place(4) = " BILLION "
        Place(5) = " TRILLION "

        ' String representation of amount
        MyNumber = Trim(Str(MyNumber))

        ' Position of decimal place 0 if none
        DecimalPlace = InStr(MyNumber, ".")
        'Convert Satang and set MyNumber to Baht amount
        If DecimalPlace > 0 Then
            Dim str_digi As String = Left(Mid(MyNumber, DecimalPlace + 1) & "0", 3)

            Dim str_digi1 As String = MyNumber 'Left(Mid(MyNumber, DecimalPlace + 1) & "00", 1)
            Dim str_digi2 As String = MyNumber 'Left(Mid(MyNumber, DecimalPlace + 1) & "00", 2)
            Dim str_digi3 As String = MyNumber 'Left(Mid(MyNumber, DecimalPlace + 1) & "00", 3)
            Dim str_digi4 As String = MyNumber 'Left(Mid(MyNumber, DecimalPlace + 1) & "00", 4)

            Dim str_digiSP As String = MyNumber
            Dim _str() As String
            _str = str_digiSP.Split(".")

            Select Case Len(_str(1))
                Case 1
                    Satang = GetDigitcheck(Mid(_str(1), 1, 1)) & " " & GetDigitcheck(Mid(_str(1), 2, 1))
                Case 2
                    Satang = GetDigitcheck(Mid(_str(1), 1, 1)) & " " & GetDigitcheck(Mid(_str(1), 2, 1))
                Case 3
                    Satang = GetDigitcheck(Mid(_str(1), 1, 1)) & " " & GetDigitcheck(Mid(_str(1), 2, 1)) & " " & GetDigitcheck(Mid(_str(1), 3, 1))
                Case 4
                    Satang = GetDigitcheck(Mid(_str(1), 1, 1)) & " " & GetDigitcheck(Mid(_str(1), 2, 1)) & " " & GetDigitcheck(Mid(_str(1), 3, 1)) & " " & GetDigitcheck(Mid(_str(1), 4, 1))
            End Select


            'Select Case Len(DecimalPlace)
            '    Case 1
            '        'Satang = GetDigitcheck(Mid(str_digi1, 1, 1)) & " "
            '    Case 2
            '        'If Right(str_digi2, 1) = "0" Then 'check เรื่อง ตัวสุดท้ายหลังจุดทดศนิยมเป็น ศูนย์ไม่เอา
            '        '    Satang = GetDigitcheck(Mid(str_digi2, 1, 1)) '& " " & GetDigitcheck(Mid(str_digi2, 2, 1))
            '        'Else
            '        '    Satang = GetDigitcheck(Mid(str_digi2, 1, 1)) & " " & GetDigitcheck(Mid(str_digi2, 2, 1))
            '        'End If

            '    Case 3
            '        'If Right(str_digi3, 1) = "0" Then 'check เรื่อง ตัวสุดท้ายหลังจุดทดศนิยมเป็น ศูนย์ไม่เอา
            '        '    Satang = GetDigitcheck(Mid(str_digi3, 1, 1)) & " " & GetDigitcheck(Mid(str_digi3, 2, 1)) '& " " & GetDigitcheck(Mid(str_digi3, 3, 1))
            '        'Else
            '        '    Satang = GetDigitcheck(Mid(str_digi3, 1, 1)) & " " & GetDigitcheck(Mid(str_digi3, 2, 1)) & " " & GetDigitcheck(Mid(str_digi3, 3, 1))
            '        'End If

            '    Case 4
            '        'If Right(str_digi4, 1) = "0" And Right(str_digi3, 1) = "0" And Right(str_digi2, 1) = "0" And Right(str_digi1, 1) <> "0" Then 'check เรื่อง ตัวสุดท้ายหลังจุดทดศนิยมเป็น ศูนย์ไม่เอา
            '        '    'display 1
            '        '    Satang = GetDigitcheck(Mid(str_digi4, 1, 1)) '& " " & GetDigitcheck(Mid(str_digi4, 2, 1)) & " " '& GetDigitcheck(Mid(str_digi4, 3, 1)) & " " & GetDigitcheck(Mid(str_digi4, 4, 1))
            '        'ElseIf Right(str_digi4, 1) = "0" And Right(str_digi3, 1) = "0" And Right(str_digi2, 1) <> "0" And Right(str_digi1, 1) <> "0" Then 'check เรื่อง ตัวสุดท้ายหลังจุดทดศนิยมเป็น ศูนย์ไม่เอา
            '        '    'display 2
            '        '    Satang = GetDigitcheck(Mid(str_digi4, 1, 1)) & " " & GetDigitcheck(Mid(str_digi4, 2, 1)) & " " '& GetDigitcheck(Mid(str_digi4, 3, 1)) & " " & GetDigitcheck(Mid(str_digi4, 4, 1))
            '        'ElseIf Right(str_digi4, 1) = "0" And Right(str_digi3, 1) <> "0" And Right(str_digi2, 1) <> "0" And Right(str_digi1, 1) <> "0" Then 'check เรื่อง ตัวสุดท้ายหลังจุดทดศนิยมเป็น ศูนย์ไม่เอา
            '        '    'display 3
            '        '    Satang = GetDigitcheck(Mid(str_digi4, 1, 1)) & " " & GetDigitcheck(Mid(str_digi4, 2, 1)) & " " & GetDigitcheck(Mid(str_digi4, 3, 1)) '& " " & GetDigitcheck(Mid(str_digi4, 4, 1))
            '        'ElseIf Right(str_digi4, 1) <> "0" And Right(str_digi3, 1) <> "0" And Right(str_digi2, 1) <> "0" And Right(str_digi1, 1) <> "0" Then 'check เรื่อง ตัวสุดท้ายหลังจุดทดศนิยมเป็น ศูนย์ไม่เอา
            '        '    'display 4
            '        'Satang = GetDigitcheck(Mid(str_digi4, 1, 1)) & " " & GetDigitcheck(Mid(str_digi4, 2, 1)) & " " & GetDigitcheck(Mid(str_digi4, 3, 1)) & " " & GetDigitcheck(Mid(str_digi4, 4, 1))
            '        'End If

            'End Select

            'Satang = GetTenscheck(Left(Mid(MyNumber, DecimalPlace + 1) & "00", 2)) 'check เรื่องหลังจุดมีกี่ตัว 'GetTens(Left(Mid(MyNumber, DecimalPlace + 1) & "00", 2)) 'เอาจุดทศนิยมมา
            MyNumber = Trim(Left(MyNumber, DecimalPlace - 1))
        End If

        Count = 1
        Do While MyNumber <> ""
            Temp = GetHundreds(Right(MyNumber, 3))
            If Temp <> "" Then Baht = Temp & Place(Count) & Baht
            If Len(MyNumber) > 3 Then
                MyNumber = Left(MyNumber, Len(MyNumber) - 3)
            Else
                MyNumber = ""
            End If
            Count = Count + 1
        Loop

        Select Case Baht
            Case ""
                Baht = "ZERO " '"No "
            Case "One"
                Baht = "One"
            Case Else
                Baht = Baht & ""
        End Select

        Select Case Satang
            Case ""
                Satang = " POINT ZERO ZERO"
            Case "One"
                Satang = " POINT One "
            Case Else
                Satang = " POINT " & Satang & ""
        End Select
        If Check_Point(F_Mynumber) = True Then
            BahtOnly = Baht & Satang + " (" + Format(CDec(F_Mynumber), "#,##0.00##") + ") " + Myq_unit_code + "****"
        Else
            ''ByTine 25-11-2558 แก้จาก CInt เป็น CLng เนื่องจากจำนวนมูลค่ามีเกิน 1 พันล้าน ใช้ CInt แล้ว Error
            BahtOnly = Baht + " (" + Format(CLng(F_Mynumber), "#,##0") + ") " + Myq_unit_code + "****"
            'BahtOnly = Baht + " (" + Format(CInt(F_Mynumber), "#,##0") + ") " + Myq_unit_code + "****"
            ' BahtOnly = Baht & Satang + " (" + Format(CInt(F_Mynumber), "#,##0.00") + ") " + Myq_unit_code + "****"

        End If
        '        BahtOnly = Baht & Satang
    End Function
    '*******************************************
    ' Converts a number from 100-999 into text *
    '*******************************************
    Private Function GetHundreds(ByVal MyNumber)
        Dim Result As String

        If Val(MyNumber) = 0 Then Exit Function
        MyNumber = Right("000" & MyNumber, 3)

        'Convert the hundreds place
        If Mid(MyNumber, 1, 1) <> "0" Then
            Result = GetDigit(Mid(MyNumber, 1, 1)) & " HUNDRED "
        End If

        'Convert the tens and ones place
        If Mid(MyNumber, 2, 1) <> "0" Then
            Result = Result & GetTens(Mid(MyNumber, 2))
        Else
            Result = Result & GetDigit(Mid(MyNumber, 3))
        End If

        GetHundreds = Result
    End Function
    '*********************************************
    ' Converts a number from 10 to 99 into text. *
    '*********************************************
    Private Function GetTens(ByVal TensText)
        Dim Result As String

        Result = "" 'null out the temporary function value
        If Val(Left(TensText, 1)) = 1 Then ' If value between 10-19
            Select Case Val(TensText)
                Case 10 : Result = "TEN"
                Case 11 : Result = "ELEVEN"
                Case 12 : Result = "TWELVE"
                Case 13 : Result = "THIRTEEN"
                Case 14 : Result = "FOURTEEN"
                Case 15 : Result = "FIFTEEN"
                Case 16 : Result = "SIXTEEN"
                Case 17 : Result = "SEVENTEEN"
                Case 18 : Result = "EIGHTEEN"
                Case 19 : Result = "NINETEEN"
                Case Else
            End Select
        Else ' If value between 20-99
            Select Case Val(Left(TensText, 1))
                Case 2 : Result = "TWENTY "
                Case 3 : Result = "THIRTY "
                Case 4 : Result = "FORTY "
                Case 5 : Result = "FIFTY "
                Case 6 : Result = "SIXTY "
                Case 7 : Result = "SEVENTY "
                Case 8 : Result = "EIGHTY "
                Case 9 : Result = "NINETY "
                Case Else
            End Select
            Result = Result & GetDigit _
            (Right(TensText, 1)) 'Retrieve ones place
        End If
        GetTens = Result
    End Function
    Private Function GetTenscheck(ByVal TensText)
        Dim Result As String

        Result = "" 'null out the temporary function value
        If Val(Left(TensText, 1)) = 1 Then ' If value between 10-19
            Select Case Val(TensText)
                Case 10 : Result = "ONE ZERO"
                Case 11 : Result = "ONE ONE"
                Case 12 : Result = "ONE TWO"
                Case 13 : Result = "ONE THREE"
                Case 14 : Result = "ONE FOUR"
                Case 15 : Result = "ONE FIVE"
                Case 16 : Result = "ONE SIX"
                Case 17 : Result = "ONE SEVEN"
                Case 18 : Result = "ONE EIGHT"
                Case 19 : Result = "ONE NINE"
                Case Else
            End Select
        Else ' If value between 20-99
            Select Case Val(Left(TensText, 1))
                Case 0 : Result = "ZERO "
                Case 2 : Result = "TWO "
                Case 3 : Result = "THREE "
                Case 4 : Result = "FOUR "
                Case 5 : Result = "FIVE "
                Case 6 : Result = "SIX "
                Case 7 : Result = "SEVEN "
                Case 8 : Result = "EIGHT "
                Case 9 : Result = "NINE "
                Case Else
            End Select
            Result = Result & GetDigitcheck(Right(TensText, 1)) 'Retrieve ones place

            'Select Case Val(TensText)
            '    Case 1

            '    Case 2

            '    Case 3

            '    Case 4

            'End Select
        End If
        GetTenscheck = Result
    End Function
    '*******************************************
    ' Converts a number from 1 to 9 into text. *
    '*******************************************
    Private Function GetDigit(ByVal Digit)
        Select Case Val(Digit)
            Case 1 : GetDigit = "ONE"
            Case 2 : GetDigit = "TWO"
            Case 3 : GetDigit = "THREE"
            Case 4 : GetDigit = "FOUR"
            Case 5 : GetDigit = "FIVE"
            Case 6 : GetDigit = "SIX"
            Case 7 : GetDigit = "SEVEN"
            Case 8 : GetDigit = "EIGHT"
            Case 9 : GetDigit = "NINE"
            Case Else : GetDigit = ""
        End Select
    End Function
    Private Function GetDigitcheck(ByVal Digit)
        Select Case Val(Digit)
            Case 0 : GetDigitcheck = "ZERO"
            Case 1 : GetDigitcheck = "ONE"
            Case 2 : GetDigitcheck = "TWO"
            Case 3 : GetDigitcheck = "THREE"
            Case 4 : GetDigitcheck = "FOUR"
            Case 5 : GetDigitcheck = "FIVE"
            Case 6 : GetDigitcheck = "SIX"
            Case 7 : GetDigitcheck = "SEVEN"
            Case 8 : GetDigitcheck = "EIGHT"
            Case 9 : GetDigitcheck = "NINE"
            Case Else : GetDigitcheck = ""
        End Select
    End Function
#Region "Back"
    ' ''===========================================
    ' ''Bath Eng
    ''Function BahtOnly(ByVal MyNumber, ByVal Myq_unit_code)
    ''    Dim F_Mynumber As String = MyNumber
    ''    Dim Baht, Satang, Temp
    ''    Dim DecimalPlace, Count

    ''    Dim Place() As String
    ''    ReDim Place(9)
    ''    Place(2) = " THOUSAND "
    ''    Place(3) = " MILLION "
    ''    Place(4) = " BILLION "
    ''    Place(5) = " TRILLION "

    ''    ' String representation of amount
    ''    MyNumber = Trim(Str(MyNumber))

    ''    ' Position of decimal place 0 if none
    ''    DecimalPlace = InStr(MyNumber, ".")
    ''    'Convert Satang and set MyNumber to Baht amount
    ''    If DecimalPlace > 0 Then
    ''        Dim str_digi As String = Left(Mid(MyNumber, DecimalPlace + 1) & "0", 3)

    ''        Dim str_digi1 As String = MyNumber 'Left(Mid(MyNumber, DecimalPlace + 1) & "00", 1)
    ''        Dim str_digi2 As String = MyNumber 'Left(Mid(MyNumber, DecimalPlace + 1) & "00", 2)
    ''        Dim str_digi3 As String = MyNumber 'Left(Mid(MyNumber, DecimalPlace + 1) & "00", 3)
    ''        Dim str_digi4 As String = MyNumber 'Left(Mid(MyNumber, DecimalPlace + 1) & "00", 4)

    ''        Dim str_digiSP As String = MyNumber
    ''        Dim _str() As String
    ''        _str = str_digiSP.Split(".")

    ''        Select Case Len(_str(1))
    ''            Case 1
    ''                Satang = GetDigitcheck(Mid(_str(1), 1, 1))
    ''            Case 2
    ''                Satang = GetDigitcheck(Mid(_str(1), 1, 1)) & " " & GetDigitcheck(Mid(_str(1), 2, 1))
    ''            Case 3
    ''                Satang = GetDigitcheck(Mid(_str(1), 1, 1)) & " " & GetDigitcheck(Mid(_str(1), 2, 1)) & " " & GetDigitcheck(Mid(_str(1), 3, 1))
    ''            Case 4
    ''                Satang = GetDigitcheck(Mid(_str(1), 1, 1)) & " " & GetDigitcheck(Mid(_str(1), 2, 1)) & " " & GetDigitcheck(Mid(_str(1), 3, 1)) & " " & GetDigitcheck(Mid(_str(1), 4, 1))
    ''        End Select


    ''        'Select Case Len(DecimalPlace)
    ''        '    Case 1
    ''        '        'Satang = GetDigitcheck(Mid(str_digi1, 1, 1)) & " "
    ''        '    Case 2
    ''        '        'If Right(str_digi2, 1) = "0" Then 'check เรื่อง ตัวสุดท้ายหลังจุดทดศนิยมเป็น ศูนย์ไม่เอา
    ''        '        '    Satang = GetDigitcheck(Mid(str_digi2, 1, 1)) '& " " & GetDigitcheck(Mid(str_digi2, 2, 1))
    ''        '        'Else
    ''        '        '    Satang = GetDigitcheck(Mid(str_digi2, 1, 1)) & " " & GetDigitcheck(Mid(str_digi2, 2, 1))
    ''        '        'End If

    ''        '    Case 3
    ''        '        'If Right(str_digi3, 1) = "0" Then 'check เรื่อง ตัวสุดท้ายหลังจุดทดศนิยมเป็น ศูนย์ไม่เอา
    ''        '        '    Satang = GetDigitcheck(Mid(str_digi3, 1, 1)) & " " & GetDigitcheck(Mid(str_digi3, 2, 1)) '& " " & GetDigitcheck(Mid(str_digi3, 3, 1))
    ''        '        'Else
    ''        '        '    Satang = GetDigitcheck(Mid(str_digi3, 1, 1)) & " " & GetDigitcheck(Mid(str_digi3, 2, 1)) & " " & GetDigitcheck(Mid(str_digi3, 3, 1))
    ''        '        'End If

    ''        '    Case 4
    ''        '        'If Right(str_digi4, 1) = "0" And Right(str_digi3, 1) = "0" And Right(str_digi2, 1) = "0" And Right(str_digi1, 1) <> "0" Then 'check เรื่อง ตัวสุดท้ายหลังจุดทดศนิยมเป็น ศูนย์ไม่เอา
    ''        '        '    'display 1
    ''        '        '    Satang = GetDigitcheck(Mid(str_digi4, 1, 1)) '& " " & GetDigitcheck(Mid(str_digi4, 2, 1)) & " " '& GetDigitcheck(Mid(str_digi4, 3, 1)) & " " & GetDigitcheck(Mid(str_digi4, 4, 1))
    ''        '        'ElseIf Right(str_digi4, 1) = "0" And Right(str_digi3, 1) = "0" And Right(str_digi2, 1) <> "0" And Right(str_digi1, 1) <> "0" Then 'check เรื่อง ตัวสุดท้ายหลังจุดทดศนิยมเป็น ศูนย์ไม่เอา
    ''        '        '    'display 2
    ''        '        '    Satang = GetDigitcheck(Mid(str_digi4, 1, 1)) & " " & GetDigitcheck(Mid(str_digi4, 2, 1)) & " " '& GetDigitcheck(Mid(str_digi4, 3, 1)) & " " & GetDigitcheck(Mid(str_digi4, 4, 1))
    ''        '        'ElseIf Right(str_digi4, 1) = "0" And Right(str_digi3, 1) <> "0" And Right(str_digi2, 1) <> "0" And Right(str_digi1, 1) <> "0" Then 'check เรื่อง ตัวสุดท้ายหลังจุดทดศนิยมเป็น ศูนย์ไม่เอา
    ''        '        '    'display 3
    ''        '        '    Satang = GetDigitcheck(Mid(str_digi4, 1, 1)) & " " & GetDigitcheck(Mid(str_digi4, 2, 1)) & " " & GetDigitcheck(Mid(str_digi4, 3, 1)) '& " " & GetDigitcheck(Mid(str_digi4, 4, 1))
    ''        '        'ElseIf Right(str_digi4, 1) <> "0" And Right(str_digi3, 1) <> "0" And Right(str_digi2, 1) <> "0" And Right(str_digi1, 1) <> "0" Then 'check เรื่อง ตัวสุดท้ายหลังจุดทดศนิยมเป็น ศูนย์ไม่เอา
    ''        '        '    'display 4
    ''        '        'Satang = GetDigitcheck(Mid(str_digi4, 1, 1)) & " " & GetDigitcheck(Mid(str_digi4, 2, 1)) & " " & GetDigitcheck(Mid(str_digi4, 3, 1)) & " " & GetDigitcheck(Mid(str_digi4, 4, 1))
    ''        '        'End If

    ''        'End Select

    ''        'Satang = GetTenscheck(Left(Mid(MyNumber, DecimalPlace + 1) & "00", 2)) 'check เรื่องหลังจุดมีกี่ตัว 'GetTens(Left(Mid(MyNumber, DecimalPlace + 1) & "00", 2)) 'เอาจุดทศนิยมมา
    ''        MyNumber = Trim(Left(MyNumber, DecimalPlace - 1))
    ''    End If

    ''    Count = 1
    ''    Do While MyNumber <> ""
    ''        Temp = GetHundreds(Right(MyNumber, 3))
    ''        If Temp <> "" Then Baht = Temp & Place(Count) & Baht
    ''        If Len(MyNumber) > 3 Then
    ''            MyNumber = Left(MyNumber, Len(MyNumber) - 3)
    ''        Else
    ''            MyNumber = ""
    ''        End If
    ''        Count = Count + 1
    ''    Loop

    ''    Select Case Baht
    ''        Case ""
    ''            Baht = "ZERO " '"No "
    ''        Case "One"
    ''            Baht = "One"
    ''        Case Else
    ''            Baht = Baht & ""
    ''    End Select

    ''    Select Case Satang
    ''        Case ""
    ''            Satang = " POINT ZERO ZERO"
    ''        Case "One"
    ''            Satang = " POINT One "
    ''        Case Else
    ''            Satang = " POINT " & Satang & ""
    ''    End Select
    ''    If Check_Point(F_Mynumber) = True Then
    ''        BahtOnly = Baht & Satang + " (" + Format(CDec(F_Mynumber), "#,##0.00##") + ") " + Myq_unit_code + "****"
    ''    Else
    ''        BahtOnly = Baht + " (" + Format(CInt(F_Mynumber), "#,##0") + ") " + Myq_unit_code + "****"
    ''        ' BahtOnly = Baht & Satang + " (" + Format(CInt(F_Mynumber), "#,##0.00") + ") " + Myq_unit_code + "****"

    ''    End If
    ''    '        BahtOnly = Baht & Satang
    ''End Function
    ' ''*******************************************
    ' '' Converts a number from 100-999 into text *
    ' ''*******************************************
    ''Private Function GetHundreds(ByVal MyNumber)
    ''    Dim Result As String

    ''    If Val(MyNumber) = 0 Then Exit Function
    ''    MyNumber = Right("000" & MyNumber, 3)

    ''    'Convert the hundreds place
    ''    If Mid(MyNumber, 1, 1) <> "0" Then
    ''        Result = GetDigit(Mid(MyNumber, 1, 1)) & " HUNDRED "
    ''    End If

    ''    'Convert the tens and ones place
    ''    If Mid(MyNumber, 2, 1) <> "0" Then
    ''        Result = Result & GetTens(Mid(MyNumber, 2))
    ''    Else
    ''        Result = Result & GetDigit(Mid(MyNumber, 3))
    ''    End If

    ''    GetHundreds = Result
    ''End Function
    ' ''*********************************************
    ' '' Converts a number from 10 to 99 into text. *
    ' ''*********************************************
    ''Private Function GetTens(ByVal TensText)
    ''    Dim Result As String

    ''    Result = "" 'null out the temporary function value
    ''    If Val(Left(TensText, 1)) = 1 Then ' If value between 10-19
    ''        Select Case Val(TensText)
    ''            Case 10 : Result = "TEN"
    ''            Case 11 : Result = "ELEVEN"
    ''            Case 12 : Result = "TWELVE"
    ''            Case 13 : Result = "THIRTEEN"
    ''            Case 14 : Result = "FOURTEEN"
    ''            Case 15 : Result = "FIFTEEN"
    ''            Case 16 : Result = "SIXTEEN"
    ''            Case 17 : Result = "SEVENTEEN"
    ''            Case 18 : Result = "EIGHTEEN"
    ''            Case 19 : Result = "NINETEEN"
    ''            Case Else
    ''        End Select
    ''    Else ' If value between 20-99
    ''        Select Case Val(Left(TensText, 1))
    ''            Case 2 : Result = "TWENTY "
    ''            Case 3 : Result = "THIRTY "
    ''            Case 4 : Result = "FORTY "
    ''            Case 5 : Result = "FIFTY "
    ''            Case 6 : Result = "SIXTY "
    ''            Case 7 : Result = "SEVENTY "
    ''            Case 8 : Result = "EIGHTY "
    ''            Case 9 : Result = "NINETY "
    ''            Case Else
    ''        End Select
    ''        Result = Result & GetDigit _
    ''        (Right(TensText, 1)) 'Retrieve ones place
    ''    End If
    ''    GetTens = Result
    ''End Function
    ''Private Function GetTenscheck(ByVal TensText)
    ''    Dim Result As String

    ''    Result = "" 'null out the temporary function value
    ''    If Val(Left(TensText, 1)) = 1 Then ' If value between 10-19
    ''        Select Case Val(TensText)
    ''            Case 10 : Result = "ONE ZERO"
    ''            Case 11 : Result = "ONE ONE"
    ''            Case 12 : Result = "ONE TWO"
    ''            Case 13 : Result = "ONE THREE"
    ''            Case 14 : Result = "ONE FOUR"
    ''            Case 15 : Result = "ONE FIVE"
    ''            Case 16 : Result = "ONE SIX"
    ''            Case 17 : Result = "ONE SEVEN"
    ''            Case 18 : Result = "ONE EIGHT"
    ''            Case 19 : Result = "ONE NINE"
    ''            Case Else
    ''        End Select
    ''    Else ' If value between 20-99
    ''        Select Case Val(Left(TensText, 1))
    ''            Case 0 : Result = "ZERO "
    ''            Case 2 : Result = "TWO "
    ''            Case 3 : Result = "THREE "
    ''            Case 4 : Result = "FOUR "
    ''            Case 5 : Result = "FIVE "
    ''            Case 6 : Result = "SIX "
    ''            Case 7 : Result = "SEVEN "
    ''            Case 8 : Result = "EIGHT "
    ''            Case 9 : Result = "NINE "
    ''            Case Else
    ''        End Select
    ''        Result = Result & GetDigitcheck(Right(TensText, 1)) 'Retrieve ones place

    ''        'Select Case Val(TensText)
    ''        '    Case 1

    ''        '    Case 2

    ''        '    Case 3

    ''        '    Case 4

    ''        'End Select
    ''    End If
    ''    GetTenscheck = Result
    ''End Function
    ' ''*******************************************
    ' '' Converts a number from 1 to 9 into text. *
    ' ''*******************************************
    ''Private Function GetDigit(ByVal Digit)
    ''    Select Case Val(Digit)
    ''        Case 1 : GetDigit = "ONE"
    ''        Case 2 : GetDigit = "TWO"
    ''        Case 3 : GetDigit = "THREE"
    ''        Case 4 : GetDigit = "FOUR"
    ''        Case 5 : GetDigit = "FIVE"
    ''        Case 6 : GetDigit = "SIX"
    ''        Case 7 : GetDigit = "SEVEN"
    ''        Case 8 : GetDigit = "EIGHT"
    ''        Case 9 : GetDigit = "NINE"
    ''        Case Else : GetDigit = ""
    ''    End Select
    ''End Function
    ''Private Function GetDigitcheck(ByVal Digit)
    ''    Select Case Val(Digit)
    ''        Case 0 : GetDigitcheck = "ZERO"
    ''        Case 1 : GetDigitcheck = "ONE"
    ''        Case 2 : GetDigitcheck = "TWO"
    ''        Case 3 : GetDigitcheck = "THREE"
    ''        Case 4 : GetDigitcheck = "FOUR"
    ''        Case 5 : GetDigitcheck = "FIVE"
    ''        Case 6 : GetDigitcheck = "SIX"
    ''        Case 7 : GetDigitcheck = "SEVEN"
    ''        Case 8 : GetDigitcheck = "EIGHT"
    ''        Case 9 : GetDigitcheck = "NINE"
    ''        Case Else : GetDigitcheck = ""
    ''    End Select
    ''End Function
#End Region
    Function Check_Point(ByVal MFunc_Quantity) As Boolean
        Dim Count_Quantity As Decimal
        Select Case MFunc_Quantity
            Case Is = ""
                Return False
            Case Is <> ""
                Count_Quantity = (CDec(MFunc_Quantity) * 100) Mod 100
                If Count_Quantity > 0 Then
                    Return True
                Else
                    Return False
                End If
        End Select

    End Function
    '===========================================
    'ใช้อันนี้
    Public Function confor_mat(ByVal d_date As Date) As String
        Dim str_for As String = Format(d_date, "d/MM/yyyy")
        Return str_for
    End Function

    Public Function Get_DateTime_rpt(ByVal val As String) As DateTime
        If val Is Nothing Then
            Return Now
        Else
            val = val.ToLower().Trim()
            If (val.StartsWith("today")) Then

                If val = "today" Then
                    Return DateTime.Today
                End If
                Try
                    Dim htValues As Hashtable
                    htValues = GetAllValues_rpt(val.Substring(5))
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
        End If

    End Function

    Public Function GetAllValues_rpt(ByVal val As String) As Hashtable

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

    'function check updatePrintTotal
    Public Function updatePrintTotal(ByVal Rpt_INVH_RUN_AUTO As String, ByVal Rpt_TOTAL As String) As Boolean
        Dim strEDIConn As String = ConfigurationManager.ConnectionStrings("OriginConnection").ConnectionString

        If Rpt_INVH_RUN_AUTO <> "" And Rpt_TOTAL <> "" Then
            Dim ds As New DataSet
            'ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, "sp_update_totalPrintPage", New SqlParameter("@INVH_RUN_AUTO", CommonUtility.Get_StringValue(Rpt_INVH_RUN_AUTO)), New SqlParameter("@TOTAL", Rpt_TOTAL))
            Return True
        Else
            Return False
        End If

    End Function

    Public Function UpdateTotalPDFPage(ByVal Rpt_INVH_RUN_AUTO As String, ByVal Rpt_TOTAL As String) As Boolean
        Dim strEDIConn As String = ConfigurationManager.ConnectionStrings("OriginConnection").ConnectionString

        If Rpt_INVH_RUN_AUTO <> "" And Rpt_TOTAL <> "" Then
            Dim ds As New DataSet
            ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, "sp_update_totalPDFPage", New SqlParameter("@invh_run_auto", CommonUtility.Get_StringValue(Rpt_INVH_RUN_AUTO)), New SqlParameter("@total_page", Rpt_TOTAL))
            Return True
        Else
            Return False
        End If

    End Function

    'function ตรวจเรื่องวัน DEPARTURE_DATE เกี่ยวกับ Issued ทั้ง 5 form
    Public Sub checkIssued_date(ByVal _numINVH_RUN_AUTO As String, ByVal _strIssued As String)
        Dim strEDIConn As String = ConfigurationManager.ConnectionStrings("OriginConnection").ConnectionString

        Dim _md As Integer

        If _numINVH_RUN_AUTO <> "" Then
            'Dim ds As New DataSet
            '_md = SqlHelper.ExecuteNonQuery(strEDIConn, CommandType.StoredProcedure, "viCheckIssuedDatePrint_NewDS", New SqlParameter("@INVH_RUN_AUTO", CommonUtility.Get_StringValue(_numINVH_RUN_AUTO)), New SqlParameter("@CheckIssued", _strIssued))

        End If
    End Sub

    Public Sub checkUpdate_user(ByVal _numINVH_RUN_AUTO As String, ByVal _strUser As String, ByVal _CheckAll As String)
        Dim strEDIConn As String = ConfigurationManager.ConnectionStrings("OriginConnection").ConnectionString

        Dim _md As Integer

        If _numINVH_RUN_AUTO <> "" Then
            'Dim ds As New DataSet
            '_md = SqlHelper.ExecuteNonQuery(strEDIConn, CommandType.StoredProcedure, "viCheckUpdateByUserPrint_NewDS", New SqlParameter("@INVH_RUN_AUTO", CommonUtility.Get_StringValue(_numINVH_RUN_AUTO)), New SqlParameter("@UserPrintForm", _strUser), New SqlParameter("@CheckAll", _CheckAll))

        End If
    End Sub
    ''function ตรวจเรื่องวัน DEPARTURE_DATE เกี่ยวกับ Issued ทั้ง 5 form
    'Public Function checkIssued_date(ByVal _numINVH_RUN_AUTO As String, ByVal _date As Date) As Integer
    '    Dim strEDIConn As String = ConfigurationManager.ConnectionStrings("OriginConnection").ConnectionString

    '    If _numINVH_RUN_AUTO <> "" Then
    '        Dim ds As New DataSet
    '        ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, "viCheckIssuedDatePrint_NewDS", New SqlParameter("@INVH_RUN_AUTO", CommonUtility.Get_StringValue(_numINVH_RUN_AUTO)), New SqlParameter("@DEPARTURE_DATE", _date))

    '        If ds.Tables(0).Rows.Count > 0 Then
    '            Return 1 'check issued
    '        Else
    '            Return 2
    '        End If
    '    Else
    '        Return 3
    '    End If
    'End Function

    Public Function Check_NULLALL(ByVal _strAll As Object) As String
        Dim _ReStr As String = ""
        If (Not _strAll = String.Empty = True) And CommonUtility.Get_StringValue(_strAll) <> "" Then
            If _strAll.ToString.Trim <> "" Then
                _ReStr = _strAll
                Return _ReStr
            Else
                Return _ReStr
            End If
        Else
            Return _ReStr
        End If
    End Function

    'check ค่าเพื่อ ใส่ข้อความไว้ก่อน HS.Code
    Public Function CarTxt(ByVal _SINGLE_COUNTRY_CONTENT As String, ByVal _InvoiceDetailTH As String) As String
        Dim str_txt As String = ""

        '_SINGLE_COUNTRY_CONTENT --1=บริษัท Car,0=ไม่ใช่ Car
        Select Case _SINGLE_COUNTRY_CONTENT
            Case "1"
                Select Case _InvoiceDetailTH.Trim
                    Case Is = ""
                        str_txt = ""
                    Case Else
                        str_txt = _InvoiceDetailTH & vbCrLf
                End Select

            Case Else
                str_txt = ""
        End Select
        Return str_txt
    End Function
    Public Function CheckIssuedDateAllForms(ByVal dateEdi As Date, ByVal Check_PrintDate As Date) As Boolean

        Dim returnCheckValue As Boolean = False

        Select Case Not IsDBNull(Check_PrintDate) 'check printFormDate

            Case False '

                Select Case DateDiff("d", dateEdi, Check_PrintDate)

                    Case Is >= 3

                        returnCheckValue = True

                    Case Else

                        returnCheckValue = False

                End Select

            Case True '

                Select Case DateDiff("d", dateEdi, Check_PrintDate)

                    Case Is >= 3

                        returnCheckValue = True

                    Case Else

                        returnCheckValue = False

                End Select

        End Select

        Return returnCheckValue

    End Function
    'check แยก invoice
    Public Function Check_numinvoiceAll(ByVal v0 As String, ByVal v1 As String, ByVal v2 As String, ByVal v3 As String, ByVal v4 As String) As Integer
        Dim iv As Integer
        Dim reInteger As Integer = 0
        Dim str_ As String = ""
        str_ = v0 & ";" & v1 & ";" & v2 & ";" & v3 & ";" & v4 & ";"
        Dim arr_str As Array = str_.Split(";")

        For iv = 0 To 4
            If arr_str(iv).ToString.Trim <> "" Then
                reInteger += 1
            End If
        Next
        Return reInteger
    End Function

    'check รายการถ้าไม่มีเลยไม่ต้องเข้า case
    Public Function CheckListDetail_ALL(ByVal By_invh_run_auto As String) As Boolean
        Dim strEDIConn As String = ConfigurationManager.ConnectionStrings("OriginConnection").ConnectionString
        Dim SQL As String = "SP_CheckListDetail_ALL"

        Dim temp_ As String = False

        Dim prm As SqlClient.SqlParameter = New SqlClient.SqlParameter("@invh_run_auto", By_invh_run_auto)
        Dim ds As DataSet
        ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, SQL, prm)

        If ds.Tables(0).Rows.Count > 0 Then
            temp_ = True
        End If

        Return temp_
    End Function
    'code check detail หน่วย
    Public Function CheckUnitDetail(ByVal By_invh_run_auto As String, Optional ByVal By_unit As String = "") As String
        Dim strEDIConn As String = ConfigurationManager.ConnectionStrings("OriginConnection").ConnectionString
        Dim SQL As String = "SP_CheckUnitDetail"

        Dim temp_ As String = ""

        Dim prm As SqlClient.SqlParameter = New SqlClient.SqlParameter("@invh_run_auto", By_invh_run_auto)
        Dim ds As DataSet
        ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, SQL, prm)

        If ds.Tables(0).Rows.Count > 0 Then
            If By_unit <> "" Then
                temp_ = By_unit
            Else
                temp_ = ds.Tables(0).Rows(0).Item("unit_code3")
            End If
        Else
            temp_ = By_unit
        End If

        Return temp_
    End Function

    Public Function CheckUnitDetail_ASW(ByVal By_invh_run_auto As String, Optional ByVal By_unit As String = "") As String
        Dim strEDIConn As String = ConfigurationManager.ConnectionStrings("OriginConnection").ConnectionString
        Dim SQL As String = "sp_asw_get_form_unitcode"

        Dim temp_ As String = ""

        Dim prm As SqlClient.SqlParameter = New SqlClient.SqlParameter("@invh_run_auto", By_invh_run_auto)
        Dim ds As DataSet
        ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, SQL, prm)

        If ds.Tables(0).Rows.Count > 0 Then
            If By_unit <> "" Then
                temp_ = By_unit
            Else
                If Not ds.Tables(0).Rows(0).Item("unit_code3").Equals(System.DBNull.Value) Then
                    temp_ = ds.Tables(0).Rows(0).Item("unit_code3")
                Else
                    temp_ = "KILOGRAM"
                End If
                ' temp_ = checkUnit(ds.Tables(0).Rows(0).Item("unit_code3"))
                ' temp_ = ds.Tables(0).Rows(0).Item("unit_code3")
            End If
        Else
            temp_ = By_unit
        End If

        Return temp_
    End Function

    Public Function CheckListIN_Detail(ByVal By_invh_run_auto As String, ByVal By_AutoDetail As String) As Boolean
        Dim strEDIConn As String = ConfigurationManager.ConnectionStrings("OriginConnection").ConnectionString
        Dim SQL As String = "SP_CheckListIN_Detail"

        Dim temp_ As String = False

        Dim prm As SqlClient.SqlParameter = New SqlClient.SqlParameter("@invh_run_auto", By_invh_run_auto)
        Dim ds As DataSet
        ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, SQL, prm)

        If ds.Tables(0).Rows.Count > 0 Then
            If ds.Tables(0).Rows(0).Item("invd_run_auto") = By_AutoDetail Then
                temp_ = True 'แสดงว่ารายการแรก
            End If
        End If

        Return temp_
    End Function

    'check count RVC Form4,Form4_1
    Public Function CheckCount_RVC(ByVal By_invh_run_auto As String, ByVal By_form As String) As Integer
        Dim strEDIConn As String = ConfigurationManager.ConnectionStrings("OriginConnection").ConnectionString
        Dim SQL As String = "SP_Checkcount_RVC"

        Dim Case_CheckReturn As Boolean = False
        Dim Check_Count As String = ""

        Dim prm As SqlClient.SqlParameter = New SqlClient.SqlParameter("@invh_run_auto", By_invh_run_auto)
        Dim ds As DataSet
        ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, SQL, prm)

        If ds.Tables(0).Rows.Count > 0 Then
            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                With ds.Tables(0).Rows(i)
                    If Check_Count = "" Then
                        Check_Count = .Item("letter")
                    Else
                        Check_Count = Check_Count & .Item("letter")
                    End If

                End With
            Next
            Select Case By_form.ToUpper
                '"FORM4", "FORM4_1", "FORM44", "FORM441", "FORM44_4", "FORM44_44", "FORM441_4"
                Case "FORM44_4", "FORM44_44", "FORM44_41", "FORM441_4", "FORM44_01", "FORM44_02", "FORMAHK", "FORMD_ESS", "FORMD_ESS_ATTS", "FORMD_ESS_"
                    Check_Count = Check_Count.Replace("2", "")
                    Check_Count = Check_Count.Replace("8", "")
                Case "FORM4_8" 'เนื่องจากเงื่อนไข rvc ไม่เหมือนกัน
                    Check_Count = Check_Count.Replace("3", "")
                Case "FORM4_911" 'สำหรับฟอร์ม AANZ ใหม่
                    Check_Count = Check_Count.Replace("4", "")
                Case "FORM4_2" 'สำหรับฟอร์ม E
                    Check_Count = ""
                Case "FORME_01", "FORME_ESS" 'สำหรับฟอร์ม E ใหม่
                    Check_Count = Check_Count.Replace("3", "")
                    Check_Count = Check_Count.Replace("6", "")
                    Check_Count = Check_Count.Replace("7", "")
                Case "FORMRCEP" 'เนื่องจากเงื่อนไข rvc ไม่เหมือนกัน
                    Check_Count = Check_Count.Replace("10", "")
            End Select
            'Check_Count = Check_Count.Replace("2", "")
            'Check_Count = Check_Count.Replace("8", "")

            Select Case Check_Count
                Case ""
                    Return 1
                Case Else
                    Return 9
            End Select
        End If
    End Function

    'form in ('FORM4','FORM4_1','FORM44','FORM44_4','FORM44_41','FORM44_44','FORM441','FORM441_4')
    '23/06/2558 เงื่อนไขใหม่ เหมือนฟอร์มอี แสดงช่อง 7 ก่อน Total
    Public Function Form_NewStr_AddressDetail(ByVal strOB_address As String) As String
        Dim Substr_OB_add As String = ""
        If strOB_address <> "" And strOB_address.Trim <> "" And Not strOB_address = String.Empty = True Then
            Substr_OB_add = strOB_address & vbNewLine & vbNewLine
        Else
            Substr_OB_add = ""
        End If
        Return Substr_OB_add
    End Function

#Region "check RVC box8 AJ"
    'check count RVC Form4_6 AJ
    Public Function CheckCount_RVCByAJ(ByVal By_invh_run_auto As String, ByVal By_form As String) As Integer
        Dim strEDIConn As String = ConfigurationManager.ConnectionStrings("OriginConnection").ConnectionString
        Dim SQL As String = "SP_CheckCount_RVCByAJ"

        Dim Case_CheckReturn As Boolean = False
        Dim Check_Count As String = ""

        Dim prm As SqlClient.SqlParameter = New SqlClient.SqlParameter("@invh_run_auto", By_invh_run_auto)
        Dim ds As DataSet
        ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, SQL, prm)

        If ds.Tables(0).Rows.Count > 0 Then
            'ต้องตรวจสอบ value กรณี 6 ว่าค่าใน box8 มี RVC สลับตำแหน่งหรือไม่
            Dim TempRvc_ As String = ""

            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                With ds.Tables(0).Rows(i)
                    If Check_Count = "" Then
                        Check_Count = .Item("letter")
                    Else
                        Check_Count = Check_Count & .Item("letter")
                    End If

                    If .Item("letter") = 6 Then
                        If TempRvc_ = "" Then
                            TempRvc_ = Check4_6RVC(.Item("box8"))
                        Else
                            TempRvc_ = TempRvc_ & Check4_6RVC(.Item("box8"))
                        End If
                    End If

                    'ต้องตรวจสอบ value กรณี 6 ว่าค่าใน box8 มี RVC สลับตำแหน่งหรือไม่
                    'If TempRvc_true = False Then
                    '    Select Case .Item("letter")
                    '        Case "6"
                    '            'True= มีเงื่อนไข RVC อยู่
                    '            TempRvc_true = Check4_6RVC(.Item("box8"))
                    '    End Select
                    'End If
                End With
            Next
            Select Case By_form.ToUpper
                Case "FORM4_61" 'เนื่องจากเงื่อนไข rvc ไม่เหมือนกัน
                    Check_Count = Check_Count.Replace("3", "")
                    Check_Count = Check_Count.Replace("6", "")

                    'ถ้าไม่เท่ากับค่าว่างแสดงว่า ใน box8 มีอย่างอื่นที่ไม่ใช่ rvc
                    TempRvc_ = TempRvc_.Replace("1", "")
            End Select

            Select Case Check_Count
                Case ""
                    Select Case TempRvc_
                        Case ""
                            Return 1
                        Case Else
                            '9 มีอย่างอื่นปน ไม่แสดงมูลค่ารวม
                            Return 9
                    End Select
                Case Else
                    '9 มีอย่างอื่นปน ไม่แสดงมูลค่ารวม
                    Return 9
            End Select
        End If
    End Function
    Function Check4_6RVC(ByVal By_RvcTemp As String) As String
        Dim str_1 As Array = By_RvcTemp.ToString.Split("+")
        Dim rvc_1 As String = """" & "RVC"
        Dim rvc_2 As String = "RVC" & """"
        Dim rvc_true As String = "9"
        If str_1.Length > 0 Then
            For i1 As Integer = 0 To str_1.Length - 1
                If str_1(i1).ToString.ToUpper = rvc_1 Or str_1(i1).ToString.ToUpper = rvc_2 Then
                    rvc_true = "1"
                End If
            Next
        End If

        Return rvc_true
    End Function
    Function Check4_6In_report(ByVal By_letter As String, ByVal By_box8 As String) As String
        Dim Temp_str As String = ""
        Select Case By_letter
            Case "6"
                Select Case Check4_6RVC(By_box8)
                    Case "1"
                        Temp_str = "3"
                End Select
            Case "3"
                Temp_str = "3"
        End Select

        Return Temp_str
    End Function
#End Region

#Region "Price Other Total"
    Public Function String_Total_USDOnly(ByVal By_Currency_Code As String, ByVal By_TFOB As String) As String
        Dim temp_str As String = ""
        Select Case By_Currency_Code
            Case Is <> "USD"
                'temp_str = "TOTAL: " & BahtOnly(By_TFOB, By_Currency_Code) & vbNewLine
                temp_str = "TOTAL: " & BahtOnly(By_TFOB, "USD") & vbNewLine
        End Select

        Return temp_str
    End Function

    Public Function String_Total_USDOnly_New_ForAgent(ByVal By_CaseDisplayUSD7 As String, ByVal By_Currency_Code As String, ByVal By_TFOB As String, ByVal By_third As String, ByVal By_usdThird As Decimal, ByVal By_UsdOther As Decimal, ByVal Isagent As Boolean, ByVal By_Usdagent As Decimal, ByVal InvAgentType As String) As String
        Dim temp_str As String = ""
        Select Case By_CaseDisplayUSD7
            Case Is <> "1" 'แสดงมูลค่าที่ช่องที่ 7
                Select Case Mid(By_third, 1, 1)
                    Case "1" 'use third
                        Select Case By_Currency_Code
                            Case Is <> "USD"
                                temp_str = "TOTAL: " & BahtOnly(Format(By_UsdOther, "#,##0.00##"), "USD") & vbNewLine
                            Case Else
                                temp_str = "TOTAL: " & BahtOnly(Format(By_usdThird, "#,##0.00##"), By_Currency_Code) & vbNewLine
                        End Select
                    Case Else 'not third
                        ''Inv นายหน้า
                        If Isagent = True Then
                            Select Case By_Currency_Code
                                Case Is <> "USD"
                                    '' ไม่เท่ากับ USD = ใช้สกุลเงินอื่นๆ
                                    If InvAgentType = 0 Then
                                        ''0 = แสดงมูลค่า Inv นายหน้า
                                        temp_str = "TOTAL: " & BahtOnly(Format(By_Usdagent, "#,##0.00##"), By_Currency_Code) & vbNewLine
                                    Else
                                        temp_str = "TOTAL: " & BahtOnly(Format(By_UsdOther, "#,##0.00##"), By_Currency_Code) & vbNewLine
                                        'temp_str = "TOTAL: " & BahtOnly(Format(By_UsdOther, "#,##0.00##"), "USD") & vbNewLine
                                    End If
                                Case Else
                                    '' ใช้สกุลเงิน USD
                                    If InvAgentType = 0 Then
                                        ''0 = แสดงมูลค่า Inv นายหน้า
                                        temp_str = "TOTAL: " & BahtOnly(Format(By_Usdagent, "#,##0.00##"), By_Currency_Code) & vbNewLine
                                    Else
                                        temp_str = "TOTAL: " & BahtOnly(By_TFOB, "USD") & vbNewLine
                                    End If
                            End Select
                        Else
                            Select Case By_Currency_Code
                                Case Is <> "USD" 'กรณีใช้สกุลเงินอื่น แต่ต้องเอาค่าจาก usd หลักและหน่วยต้องเป็น USD เท่านั้น ไม่เอาหน่วยจาก Currency_Code
                                    temp_str = "TOTAL: " & BahtOnly(By_TFOB, "USD") & vbNewLine
                                Case Else
                                    temp_str = "TOTAL: " & BahtOnly(By_TFOB, By_Currency_Code) & vbNewLine
                            End Select
                        End If
                End Select
        End Select

        Return temp_str
    End Function

    Public Function String_Total_USDOnly_New_FORME(ByVal By_CaseDisplayUSD7 As String, ByVal By_Currency_Code As String, ByVal By_TFOB As String, ByVal By_third As String, ByVal By_usdThird As Decimal, ByVal By_UsdOther As Decimal) As String
        Dim temp_str As String = ""
        Dim temp_value As Decimal = 0
        temp_value = By_TFOB
        If By_CaseDisplayUSD7 <> "1" Then
            Select Case Mid(By_third, 1, 1)
                Case "1" 'use third
                    temp_value = By_usdThird
                Case Else 'not third
                    temp_value = By_TFOB

            End Select

            Dim temp_data As String = temp_value.ToString("N2")

            '//temp_str = "TOTAL: " & BahtOnly(temp_value, By_Currency_Code)
            temp_str = "TOTAL: " & BahtOnly(temp_data, "USD") '//& vbCrLf
        End If

        If temp_str <> "" Then
            temp_str &= vbCrLf
        End If

        Return temp_str
    End Function

    Public Function String_Total_USDOnly_New_Attach_ForAgent(ByVal By_CaseDisplayUSD7 As String, ByVal By_Currency_Code As String, ByVal By_TFOB As String, ByVal By_third As String, ByVal By_usdThird As Decimal, ByVal By_UsdOther As Decimal, ByVal By_grossTotal As Decimal, ByVal By_G_uniGross As String, ByVal Isagent As Boolean, ByVal By_Usdagent As Decimal, ByVal InvAgentType As String, Optional ByVal By_invh_run_auto As String = "") As String
        Dim temp_str As String = ""
        Dim temp_Gross As String = ""
        Dim temp_ReturnAll As String = ""
        'เพิ่ม Gross weight ให้แสดง Total รวมด้วย TOTAL G.W.:
        'เฉพาะ D att 
        If By_invh_run_auto <> "" Then
            Select Case CheckUnitDetail(By_invh_run_auto).ToLower
                Case "KGM", "KGS", "kgm", "kgs"
                    temp_Gross = "TOTAL G.W.: " & BahtOnly(Format(By_grossTotal, "#,##0.00##"), By_G_uniGross) & vbNewLine
                    'temp_Gross = "TOTAL : " & BahtOnly(Format(By_grossTotal, "#,##0.00##"), By_G_uniGross) & vbNewLine
                Case Else
                    temp_Gross = "TOTAL : " & BahtOnly(Format(By_grossTotal, "#,##0.00##"), By_G_uniGross) & vbNewLine
                    'temp_Gross = "TOTAL G.W.: " & BahtOnly(Format(By_grossTotal, "#,##0.00##"), By_G_uniGross) & vbNewLine
            End Select
        Else
            temp_Gross = "TOTAL G.W.: " & BahtOnly(Format(By_grossTotal, "#,##0.00##"), By_G_uniGross) & vbNewLine
        End If

        '        temp_Gross = "TOTAL G.W.: " & BahtOnly(Format(By_grossTotal, "#,##0.00##"), By_G_uniGross) & vbNewLine
        Select Case By_CaseDisplayUSD7
            Case Is <> "1" 'แสดงมูลค่าที่ช่องที่ 7
                Select Case Mid(By_third, 1, 1)
                    Case "1" 'use third
                        Select Case By_Currency_Code
                            Case Is <> "USD"
                                temp_str = "TOTAL: " & BahtOnly(Format(By_UsdOther, "#,##0.00##"), "USD") & vbNewLine
                                'temp_str = "TOTAL: " & BahtOnly(By_UsdOther, By_Currency_Code) & vbNewLine
                            Case Else
                                temp_str = "TOTAL: " & BahtOnly(Format(By_usdThird, "#,##0.00##"), By_Currency_Code) & vbNewLine
                        End Select
                    Case Else 'not third
                        ''Inv นายหน้า
                        If Isagent = True Then
                            Select Case By_Currency_Code
                                Case Is <> "USD" 'กรณีใช้สกุลเงินอื่น แต่ต้องเอาค่าจาก usd หลักและหน่วยต้องเป็น USD เท่านั้น ไม่เอาหน่วยจาก Currency_Code
                                    '' ไม่เท่ากับ USD = ใช้สกุลเงินอื่นๆ
                                    If InvAgentType = 0 Then
                                        ''0 = แสดงมูลค่า Inv นายหน้า
                                        temp_str = "TOTAL: " & BahtOnly(By_Usdagent, By_Currency_Code) & vbNewLine
                                    Else
                                        temp_str = "TOTAL: " & BahtOnly(By_UsdOther, "USD") & vbNewLine
                                    End If
                                Case Else
                                    '' ใช้สกุลเงิน USD
                                    If InvAgentType = 0 Then
                                        temp_str = "TOTAL: " & BahtOnly(By_Usdagent, By_Currency_Code) & vbNewLine
                                    Else
                                        temp_str = "TOTAL: " & BahtOnly(By_TFOB, "USD") & vbNewLine
                                    End If
                            End Select
                        Else
                            ''ไม่ใช่ Inv นายหน้า
                            Select Case By_Currency_Code
                                Case Is <> "USD" 'กรณีใช้สกุลเงินอื่น แต่ต้องเอาค่าจาก usd หลักและหน่วยต้องเป็น USD เท่านั้น ไม่เอาหน่วยจาก Currency_Code
                                    temp_str = "TOTAL: " & BahtOnly(By_TFOB, "USD") & vbNewLine
                                Case Else
                                    temp_str = "TOTAL: " & BahtOnly(By_TFOB, By_Currency_Code) & vbNewLine
                            End Select
                        End If
                End Select
        End Select

        temp_ReturnAll = temp_Gross & temp_str
        Return temp_ReturnAll
    End Function

    Public Function String_Total_USDOnly_New(ByVal By_CaseDisplayUSD7 As String, ByVal By_Currency_Code As String, ByVal By_TFOB As String, ByVal By_third As String, ByVal By_usdThird As Decimal, ByVal By_UsdOther As Decimal) As String
        Dim temp_str As String = ""
        Select Case By_CaseDisplayUSD7
            Case Is <> "1" 'แสดงมูลค่าที่ช่องที่ 7
                Select Case Mid(By_third, 1, 1)
                    Case "1" 'use third
                        Select Case By_Currency_Code
                            Case Is <> "USD"
                                temp_str = "TOTAL: " & BahtOnly(Format(By_UsdOther, "#,##0.00##"), "USD") & vbNewLine
                                'temp_str = "TOTAL: " & BahtOnly(Format(By_UsdOther, "#,##0.00##"), "USD") & vbNewLine & "        "
                                'temp_str = "TOTAL: " & BahtOnly(By_UsdOther, By_Currency_Code) & vbNewLine
                            Case Else
                                temp_str = "TOTAL: " & BahtOnly(Format(By_usdThird, "#,##0.00##"), By_Currency_Code) & vbNewLine
                                'temp_str = "TOTAL: " & BahtOnly(Format(By_usdThird, "#,##0.00##"), By_Currency_Code) & vbNewLine & "        "
                        End Select
                    Case Else 'not third
                        Select Case By_Currency_Code
                            Case Is <> "USD" 'กรณีใช้สกุลเงินอื่น แต่ต้องเอาค่าจาก usd หลักและหน่วยต้องเป็น USD เท่านั้น ไม่เอาหน่วยจาก Currency_Code
                                temp_str = "TOTAL: " & BahtOnly(By_TFOB, "USD") & vbNewLine
                                'temp_str = "TOTAL: " & BahtOnly(By_TFOB, "USD") & vbNewLine & "        "
                            Case Else
                                temp_str = "TOTAL: " & BahtOnly(By_TFOB, By_Currency_Code) & vbNewLine
                                'temp_str = "TOTAL: " & BahtOnly(By_TFOB, By_Currency_Code) & vbNewLine & "        "
                        End Select

                End Select
        End Select

        Return temp_str
    End Function
    Public Function String_Total_USDOnly_New_Attach(ByVal By_CaseDisplayUSD7 As String, ByVal By_Currency_Code As String, ByVal By_TFOB As String, ByVal By_third As String, ByVal By_usdThird As Decimal, ByVal By_UsdOther As Decimal, ByVal By_grossTotal As Decimal, ByVal By_G_uniGross As String, Optional ByVal By_invh_run_auto As String = "") As String
        Dim temp_str As String = ""
        Dim temp_Gross As String = ""
        Dim temp_ReturnAll As String = ""
        'เพิ่ม Gross weight ให้แสดง Total รวมด้วย TOTAL G.W.:
        'เฉพาะ D att 
        If By_invh_run_auto <> "" Then
            Select Case CheckUnitDetail(By_invh_run_auto).ToLower
                Case "KGM", "KGS", "kgm", "kgs"
                    temp_Gross = "TOTAL G.W.: " & BahtOnly(Format(By_grossTotal, "#,##0.00##"), By_G_uniGross) & vbNewLine
                    'temp_Gross = "TOTAL : " & BahtOnly(Format(By_grossTotal, "#,##0.00##"), By_G_uniGross) & vbNewLine
                Case Else
                    temp_Gross = "TOTAL : " & BahtOnly(Format(By_grossTotal, "#,##0.00##"), By_G_uniGross) & vbNewLine
                    'temp_Gross = "TOTAL G.W.: " & BahtOnly(Format(By_grossTotal, "#,##0.00##"), By_G_uniGross) & vbNewLine
            End Select
        Else
            temp_Gross = "TOTAL G.W.: " & BahtOnly(Format(By_grossTotal, "#,##0.00##"), By_G_uniGross) & vbNewLine
        End If

        '        temp_Gross = "TOTAL G.W.: " & BahtOnly(Format(By_grossTotal, "#,##0.00##"), By_G_uniGross) & vbNewLine
        Select Case By_CaseDisplayUSD7
            Case Is <> "1" 'แสดงมูลค่าที่ช่องที่ 7
                Select Case Mid(By_third, 1, 1)
                    Case "1" 'use third
                        Select Case By_Currency_Code
                            Case Is <> "USD"
                                temp_str = "TOTAL: " & BahtOnly(Format(By_UsdOther, "#,##0.00##"), "USD") & vbNewLine
                                'temp_str = "TOTAL: " & BahtOnly(By_UsdOther, By_Currency_Code) & vbNewLine
                            Case Else
                                temp_str = "TOTAL: " & BahtOnly(Format(By_usdThird, "#,##0.00##"), By_Currency_Code) & vbNewLine
                        End Select
                    Case Else 'not third
                        Select Case By_Currency_Code
                            Case Is <> "USD" 'กรณีใช้สกุลเงินอื่น แต่ต้องเอาค่าจาก usd หลักและหน่วยต้องเป็น USD เท่านั้น ไม่เอาหน่วยจาก Currency_Code
                                temp_str = "TOTAL: " & BahtOnly(By_TFOB, "USD") & vbNewLine
                            Case Else
                                temp_str = "TOTAL: " & BahtOnly(By_TFOB, By_Currency_Code) & vbNewLine
                        End Select

                End Select
        End Select

        temp_ReturnAll = temp_Gross & temp_str
        Return temp_ReturnAll
    End Function
    'Public Function String_Total_USDOnly_New_Attach(ByVal By_CaseDisplayUSD7 As String, ByVal By_Currency_Code As String, ByVal By_TFOB As String, ByVal By_third As String, ByVal By_usdThird As Decimal, ByVal By_UsdOther As Decimal, ByVal By_grossTotal As Decimal, ByVal By_G_uniGross As String) As String
    '    Dim temp_str As String = ""
    '    Dim temp_Gross As String = ""
    '    Dim temp_ReturnAll As String = ""
    '    'เพิ่ม Gross weight ให้แสดง Total รวมด้วย TOTAL G.W.:
    '    temp_Gross = "TOTAL G.W.: " & BahtOnly(Format(By_grossTotal, "#,##0.00##"), By_G_uniGross) & vbNewLine
    '    Select Case By_CaseDisplayUSD7
    '        Case Is <> "1" 'แสดงมูลค่าที่ช่องที่ 7
    '            Select Case Mid(By_third, 1, 1)
    '                Case "1" 'use third
    '                    Select Case By_Currency_Code
    '                        Case Is <> "USD"
    '                            temp_str = "TOTAL: " & BahtOnly(Format(By_UsdOther, "#,##0.00##"), "USD") & vbNewLine
    '                            'temp_str = "TOTAL: " & BahtOnly(By_UsdOther, By_Currency_Code) & vbNewLine
    '                        Case Else
    '                            temp_str = "TOTAL: " & BahtOnly(Format(By_usdThird, "#,##0.00##"), By_Currency_Code) & vbNewLine
    '                    End Select
    '                Case Else 'not third
    '                    Select Case By_Currency_Code
    '                        Case Is <> "USD" 'กรณีใช้สกุลเงินอื่น แต่ต้องเอาค่าจาก usd หลักและหน่วยต้องเป็น USD เท่านั้น ไม่เอาหน่วยจาก Currency_Code
    '                            temp_str = "TOTAL: " & BahtOnly(By_TFOB, "USD") & vbNewLine
    '                        Case Else
    '                            temp_str = "TOTAL: " & BahtOnly(By_TFOB, By_Currency_Code) & vbNewLine
    '                    End Select

    '            End Select
    '    End Select

    '    temp_ReturnAll = temp_Gross & temp_str
    '    Return temp_ReturnAll
    'End Function
#End Region

#Region "Check Form4_8 เก่าหรือไม่"
    Public Function Check_Form4_8(ByVal By_invh_run_auto As String) As Boolean
        Dim strEDIConn As String = ConfigurationManager.ConnectionStrings("OriginConnection").ConnectionString
        Dim SQLForm4_8 As String = "SP_Check_Form4_8"

        Dim Form4_8Detail As Boolean = False

        Dim prm As SqlClient.SqlParameter = New SqlClient.SqlParameter("@invh_run_auto", By_invh_run_auto)
        Dim ds As DataSet
        ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, SQLForm4_8, prm)

        If ds.Tables(0).Rows.Count > 0 Then
            Return True
        End If

        Return Form4_8Detail
    End Function

    'by rut เพิ่มให้เลือกไม่แสดง USD ช่องที่ 7
    Public Function Check_DisplayUSD7(ByVal By_invh_run_auto As String) As String
        Dim strEDIConn As String = ConfigurationManager.ConnectionStrings("OriginConnection").ConnectionString
        Dim SQL As String = "SP_Check_DisplayUSD7"

        Dim temp_ As String = ""

        Dim prm As SqlClient.SqlParameter = New SqlClient.SqlParameter("@invh_run_auto", By_invh_run_auto)
        Dim ds As DataSet
        ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, SQL, prm)

        If ds.Tables(0).Rows.Count > 0 Then
            temp_ = CommonUtility.Get_StringValue(ds.Tables(0).Rows(0).Item("DisplayUSD7"))
        End If

        Return temp_
    End Function
#End Region

    ''ByTine 20-10-2558 ใช้ดึงข้อมูลวันที่พิมพ์ฟอร์มของเลขที่อ้างอิงชุดเดิมที่มีการขอแก้ไข
    Public Function CheckPrintDate(ByVal _RefCode2 As String)
        Try
            Dim StrConn As String = ConfigurationManager.ConnectionStrings("OriginConnection").ConnectionString
            Dim Strcommand As String
            Dim _PrintDate As DateTime
            Strcommand = " SELECT printFormDate FROM form_header_edi WHERE (reference_code2 = '" & _RefCode2 & "' OR reference_code2 = '" & _RefCode2 & "_C') "
            Dim dr As SqlDataReader
            dr = SqlHelper.ExecuteReader(StrConn, CommandType.Text, Strcommand)
            dr = SqlHelper.ExecuteReader(StrConn, CommandType.Text, Strcommand)
            dr.Read()
            If dr.HasRows Then
                _PrintDate = dr.Item("printFormDate")
            Else
                _PrintDate = ""
            End If
            Return _PrintDate.ToString("dd/MM/yyyy")
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Public Function CheckInvAgent(ByVal _IsAgent As Boolean, ByVal _InvAgent As String, ByVal _CompanyAgentName As String)
        Try
            Dim ret As String = ""
            If _IsAgent = True Then
                ret = _InvAgent & vbNewLine & _CompanyAgentName & "  " & "THAILAND" & vbNewLine
            End If

            Return ret

        Catch ex As Exception
            Return ""
        End Try
    End Function

    Public Function CheckInvAgent_AHK(ByVal _IsAgent As Boolean, ByVal _InvAgent As String, ByVal _CompanyAgentName As String)
        Try
            Dim ret As String = ""
            If _IsAgent = True Then
                ret = _CompanyAgentName & "  " & "THAILAND" & vbNewLine
            End If

            Return ret

        Catch ex As Exception
            Return ""
        End Try
    End Function
#Region "Check ค่าว่าง สำหรับช่อง 7 บรรทัดสุดท้าย (Total)"
    ''ByTine 20-03-2560
    Public Function CheckIsZeroOrNothing(ByVal _Value As Object) As Boolean
        Try
            Dim ret As Boolean = False

            ''Check เป็น Null หรือเปล่า
            If DBNull.Value.Equals(_Value) = False Then
                If IsNumeric(_Value) = True Then
                    If _Value <> 0 Then
                        ret = True
                    Else
                        ret = False
                    End If
                Else
                    If _Value = " " Then
                        ret = False
                    ElseIf _Value <> "" Or _Value IsNot Nothing Then
                        ret = True
                    Else
                        ret = False
                    End If
                End If
            Else
                ret = False
            End If

            Return ret

        Catch ex As Exception
            Return False
        End Try
    End Function

#End Region

    ''ByTine 13-2-2561 Form E แยก GW
    Public Function CheckUnitDetail_FormE(ByVal By_invh_run_auto As String, Optional ByVal By_unit As String = "") As String
        Dim strEDIConn As String = ConfigurationManager.ConnectionStrings("OriginConnection").ConnectionString
        Dim SQL As String = "SP_CheckUnitDetail_FormE"

        Dim temp_ As String = ""

        Dim prm As SqlClient.SqlParameter = New SqlClient.SqlParameter("@invh_run_auto", By_invh_run_auto)
        Dim ds As DataSet
        ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, SQL, prm)

        If ds.Tables(0).Rows.Count > 0 Then
            If By_unit <> "" Then
                temp_ = By_unit
            Else
                If DBNull.Value.Equals(ds.Tables(0).Rows(0).Item("unit_code3")) = False Then
                    temp_ = ds.Tables(0).Rows(0).Item("unit_code3")
                Else
                    temp_ = ds.Tables(0).Rows(0).Item("unit_code2")
                End If
            End If
        Else
            temp_ = By_unit
        End If

        Return temp_
    End Function

    Public Function CheckIssuedDate_Forms_AHK(ByVal dateEdi As Date, ByVal Check_PrintDate As Date) As Boolean

        Dim returnCheckValue As Boolean = False

        Select Case Not IsDBNull(Check_PrintDate) 'check printFormDate

            Case False '

                Select Case WorkingDaysElapsed(dateEdi, Now)
                    Case Is > 4
                        returnCheckValue = True
                    Case Else
                        returnCheckValue = False
                End Select
            Case True '
                Select Case WorkingDaysElapsed(dateEdi, Check_PrintDate)
                    Case Is > 4
                        returnCheckValue = True
                    Case Else
                        returnCheckValue = False
                End Select

        End Select

        Return returnCheckValue

    End Function
    Public Function CheckIssuedDate_Forms_RCEP(ByVal dateEdi As Date, ByVal Check_PrintDate As Date) As Boolean

        Dim returnCheckValue As Boolean = False

        Select Case Not IsDBNull(Check_PrintDate) 'check printFormDate

            Case False '

                Select Case WorkingDaysElapsed(dateEdi, Now)
                    Case Is > 1
                        returnCheckValue = True
                    Case Else
                        returnCheckValue = False
                End Select
            Case True '
                Select Case WorkingDaysElapsed(dateEdi, Check_PrintDate)
                    Case Is > 1
                        returnCheckValue = True
                    Case Else
                        returnCheckValue = False
                End Select

        End Select

        Return returnCheckValue

    End Function
    Public Function WorkingDaysElapsed(StartDate As Date, EndDate As Date)
        Dim count = 0
        Dim totalDays = (EndDate - StartDate).Days

        For i As Integer = 0 To totalDays
            Dim weekday As DayOfWeek = StartDate.AddDays(i).DayOfWeek
            If weekday <> DayOfWeek.Saturday AndAlso weekday <> DayOfWeek.Sunday Then
                count += 1
            End If
        Next
        Return count
    End Function

#Region "Seal Sign"
    'code get ค่า path report
    Public Function reports_CardID(ByVal _strCardID As String) As String
        'Dim PathFull_SiteMain As String = ConfigurationManager.AppSettings("pathWebconfigUppic_main")

        Dim PathFull_SiteMain As String = ConfigurationManager.AppSettings("pathWebconfigUppic_mainUrl")
        Dim PathFull_Site As String = ConfigurationManager.AppSettings("pathWebconfigUppic")

        Dim strEDIConn As String = ConfigurationManager.ConnectionStrings("OriginConnection").ConnectionString

        Dim m_iReturnCheck As New DataSet
        Dim send_Check As String = ""
        'check
        m_iReturnCheck = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, "vi_GetBy_AutoID_ImagesSign_NewDS2",
                   New SqlParameter("@im_AutoID", _strCardID))

        If m_iReturnCheck.Tables(0).Rows.Count > 0 Then
            With m_iReturnCheck.Tables(0).Rows(0)
                send_Check = PathFull_SiteMain & "/" & PathFull_Site & .Item("im_Taxid") & "/" & .Item("im_years") & IIf(.Item("im_StatusType").ToUpper = "C", "/SealAuthorize", "/SealPerson") & "/" & .Item("im_AuthPersonID") & "/" & .Item("im_AuthPersonID") & .Item("im_strFile") & IIf(CInt(.Item("im_FileNum")) = 1, "", .Item("im_FileNum")) & .Item("im_FilesName")

                'send_Check = PathFull_SiteMain & "/" & PathFull_Site & .Item("im_Taxid") & "\" & .Item("im_years") & IIf(.Item("im_StatusType").ToUpper = "C", "/SealAuthorize", "/SealPerson") & "\" & .Item("im_AuthPersonID") & "\" & .Item("im_AuthPersonID") & .Item("im_strFile") & IIf(CInt(.Item("im_FileNum")) = 1, "", .Item("im_FileNum")) & .Item("im_FilesName")
                ''send_Check = PathFull_SiteMain & PathFull_Site & m_iReturnCheck.Tables(0).Rows(0).Item("im_Taxid") & "\" & m_iReturnCheck.Tables(0).Rows(0).Item("im_AuthPersonID") & m_iReturnCheck.Tables(0).Rows(0).Item("im_strFile") & IIf(CInt(m_iReturnCheck.Tables(0).Rows(0).Item("im_FileNum")) = 1, "", m_iReturnCheck.Tables(0).Rows(0).Item("im_FileNum")) & m_iReturnCheck.Tables(0).Rows(0).Item("im_FilesName")
                'send_Check = HttpContext.Current.Server.MapPath("") & _strCardID & "_New_" & m_iReturnCheck.Tables(0).Rows(0).Item("Im_FilesName").ToString

            End With
        Else
            send_Check = "" 'ยังไม่มีข้อมูล
        End If
        Return send_Check
    End Function

    'by rut search form image หาฟอร์มที่มีข้อมูล sign imang
    Public Function search_imageForm(ByVal Func_runForm As String) As DataSet
        Dim dsFormImage As DataSet
        Dim strEDIConn As String = ConfigurationManager.ConnectionStrings("OriginConnection").ConnectionString

        dsFormImage = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, "vi_Search_FormSign_image_NewDS2",
                        New SqlParameter("@im_invh_run_auto", Func_runForm))

        Return dsFormImage
    End Function

    'เอา card id หา เลขบัตรประชาชน เพื่อพิมพ์ลงในคำขอ
    Public Function Get_Cardid_Num(ByVal By_Cardid As String) As DataSet
        Dim ds As DataSet

        Dim strEDIConn As String = ConfigurationManager.ConnectionStrings("OriginConnection").ConnectionString
        Dim sql As String = ""
        sql = "SELECT     company_taxno, company_branchNo, card_id, commit_name, expire_date, password, active_flag, AuthName, AuthName_Thai,  AuthPersonID, card_type " &
"FROM         rfcard " &
"WHERE     (card_id = @card_id) AND (active_flag <> 'C')"
        Dim prm(0) As SqlClient.SqlParameter
        prm(0) = New SqlClient.SqlParameter("@card_id", By_Cardid)

        ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.Text, sql, prm)

        Return ds
    End Function
    'เอาเลขบัตรมาหา เพื่อให้ได้ลายเซ็นในคำขอ
    Public Function Get_DataSealSignidby_cardid(ByVal By_Cardid As String, ByVal By_Tax As String, ByVal By_branchNo As String) As String
        Dim ds As DataSet
        Dim Temp_ As String = ""
        Dim strEDIConn As String = ConfigurationManager.ConnectionStrings("OriginConnection").ConnectionString
        Dim sql As String = ""
        sql = "SELECT     * FROM         im_SignImages " &
"WHERE     (im_Taxid = @im_Taxid) AND (im_AuthPersonID = @im_AuthPersonID) and (im_CheckStatus='1')"

        Dim prm(2) As SqlClient.SqlParameter
        prm(0) = New SqlClient.SqlParameter("@im_Taxid", By_Tax)
        prm(1) = New SqlClient.SqlParameter("@im_Branchno", By_branchNo)
        prm(2) = New SqlClient.SqlParameter("@im_AuthPersonID", By_Cardid)

        ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.Text, sql, prm)
        If ds.Tables(0).Rows.Count > 0 Then
            Temp_ = ds.Tables(0).Rows(0).Item("im_AutoID").ToString
        End If
        Return Temp_
    End Function
    'check รายการว่าเป็น Seal Sign หรือไม่
    Public Function Get_CheckDataBy_Auto(ByVal By_im_invh_run_auto As String) As Boolean
        Dim ds As DataSet
        Dim re_check As Boolean = False
        Dim strEDIConn As String = ConfigurationManager.ConnectionStrings("OriginConnection").ConnectionString
        Dim sql As String = ""
        sql = "SELECT      im_invh_run_auto, SignImageID, SignImage_ApproveID, SignImage_ApproveName, im_create, im_update, im_form_type, im_CheckCaseSave " &
"FROM         FormSign_image " &
"WHERE     (im_invh_run_auto = @im_invh_run_auto) AND (im_CheckCaseSave = '1')"

        Dim prm(0) As SqlClient.SqlParameter
        prm(0) = New SqlClient.SqlParameter("@im_invh_run_auto", By_im_invh_run_auto)

        ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.Text, sql, prm)
        If ds.Tables(0).Rows.Count > 0 Then
            re_check = True
        End If
        Return re_check
    End Function

    'คำขอ
    Public Function Req_IDSeal(ByVal By_inv As String) As String
        Dim re_id As String = ""
        Dim strEDIConn As String = ConfigurationManager.ConnectionStrings("OriginConnection").ConnectionString
        Dim sql_ As String = "vi_GetBy_AutoID_ImagesSign_ESS"
        ' sql_ = "SELECT     * FROM         FormSign_image WHERE     (im_invh_run_auto = @im_invh_run_auto)"
        Dim dsSeal As New DataSet
        Dim prmSeal(0) As SqlClient.SqlParameter
        prmSeal(0) = New SqlClient.SqlParameter("@im_invh_run_auto", By_inv)

        dsSeal = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, sql_, prmSeal)

        If dsSeal.Tables(0).Rows.Count > 0 Then
            Dim str_temp As String = dsSeal.Tables(0).Rows(0).Item("im_AutoID")
            If str_temp <> "" Then
                'Dim arr_ As Array
                'arr_ = str_temp.Split(";")
                re_id = str_temp
                're_id = arr_(0).ToString
            End If
        End If

        Return re_id
    End Function
    'get ค่า
    Public Function reports_Approveid(ByVal _strAppID As String) As String
        'Dim PathFull_SiteMain As String = ConfigurationManager.AppSettings("pathWebconfigUppic_main")
        'Dim PathFull_Site As String = ConfigurationManager.AppSettings("pathWebconfigUppic")

        Dim PathSiteAll_page As String = ConfigurationManager.AppSettings("pathWebconfigUppicGov").ToString()

        Dim PathSiteAll_pageMainBack As String = ConfigurationManager.AppSettings("pathWebconfigUppic_mainBack").ToString()

        Dim strEDIConn As String = ConfigurationManager.ConnectionStrings("OriginConnection").ConnectionString

        Dim m_iReturnCheck As New DataSet
        Dim send_Check As String = ""
        'check
        Dim sqlapp As String = ""
        sqlapp = "SELECT     * FROM         imGovernmentSign WHERE     (AutoRunid = @AutoRunid)"
        Dim prm(0) As SqlClient.SqlParameter
        prm(0) = New SqlClient.SqlParameter("@AutoRunid", _strAppID)

        m_iReturnCheck = SqlHelper.ExecuteDataset(strEDIConn, CommandType.Text, sqlapp, prm)

        If m_iReturnCheck.Tables(0).Rows.Count > 0 Then
            With m_iReturnCheck.Tables(0).Rows(0)
                'send_Check = PathSiteAll_pageMainBack & "\" & PathSiteAll_page & .Item("SiteUser") & "\" & .Item("UserNameTemp") & "\" & .Item("UserName") & .Item("FileStr") & IIf(CInt(.Item("FileNum")) = 1, "", .Item("FileNum")) & .Item("FileNameGov")
                send_Check = PathSiteAll_pageMainBack & PathSiteAll_page & .Item("SiteUser") & "\" & .Item("UserNameTemp") & "\" & .Item("UserName") & .Item("FileStr") & IIf(CInt(.Item("FileNum")) = 1, "", .Item("FileNum")) & .Item("FileNameGov")

            End With
        Else
            send_Check = "" 'ยังไม่มีข้อมูล
        End If
        Return send_Check
    End Function

    'get string ชื่อ ตามสาขา
    Function String_SiteNameReport01(ByVal By_Site As String) As String
        Dim strEDIConn As String = ConfigurationManager.ConnectionStrings("OriginConnection").ConnectionString
        Dim str_txt As String = ""
        'Select Case By_Site.ToUpper
        '    Case "ST-001" 'ส่วนกลาง สนามบินน้ำ
        '        str_txt = "BANGkok"
        '    Case "ST-002" 'คลองเตย
        '        str_txt = "BANGKOK PORT"
        '    Case "ST-003" 'สุวรรณภูมิ
        '        str_txt = "SUVARNABHUMI" '& vbNewLine & "AIRPORT"
        '    Case "ST-004" 'สอ
        '        str_txt = "BANGKOK"
        '    Case "CB-003" 'ชลบุรี
        '        str_txt = "Chonburi"
        '    Case "CM-001" 'สคต.เขต1 (เชียงใหม่)
        '        str_txt = "Chiang Mai"
        '    Case "CR-006" 'สคต.เขต6 (เชียงราย)
        '        str_txt = "Chiangrai"
        '    Case "MH-009"  'สคต มุกดาหาร'
        '        str_txt = "Mukdahan"
        '    Case "SK-004"  'สคต สระแก้ว'
        '        str_txt = "Srakaeo"
        '    Case "TK-008"  'สคต ตาก'
        '        str_txt = "Tak"
        '    Case "NK-005"  'สคต หนองคาย'
        '        str_txt = "Nong Khai"
        '    Case "HY-002"  'สคต สงขลา'
        '        str_txt = "Songkhla"

        'End Select

        Dim cmd As String = "SP_GetPortName_ESS"
        Dim ds As New DataSet
        ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, cmd, New SqlParameter("@siteid", By_Site.ToUpper))
        If ds.Tables(0).Rows.Count > 0 Then
            str_txt = ds.Tables(0).Rows(0).Item("port_name")
        End If

        Return str_txt
    End Function
    'get string ชื่อ ตามสาขา
    Function String_SiteNameReport02(ByVal By_Site As String) As String
        Dim strEDIConn As String = ConfigurationManager.ConnectionStrings("OriginConnection").ConnectionString
        Dim str_txt As String = ""
        'Select Case By_Site.ToUpper
        '    Case "ST-003" 'สุวรรณภูมิ
        '        str_txt = "AIRPORT"
        'End Select
        Dim cmd As String = "SP_GetPortName_ESS"
        Dim ds As New DataSet
        ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, cmd, New SqlParameter("@siteid", By_Site.ToUpper))
        If ds.Tables(0).Rows.Count > 0 Then
            str_txt = ds.Tables(0).Rows(0).Item("port_name2")
        End If

        Return str_txt
    End Function
    Function String_SiteNameReport03(ByVal By_Site As String) As String
        Dim strEDIConn As String = ConfigurationManager.ConnectionStrings("OriginConnection").ConnectionString
        Dim str_txt As String = ""

        Dim cmd As String = "SP_GetPortName_ESS"
        Dim ds As New DataSet
        ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, cmd, New SqlParameter("@siteid", By_Site.ToUpper))
        If ds.Tables(0).Rows.Count > 0 Then
            str_txt = ds.Tables(0).Rows(0).Item("port_name3")
        End If

        Return str_txt
    End Function
    'get string วันที่ตามตราครุฑ
    Function String_DateSiteReport(ByVal By_datePrint As Object) As String
        Dim date_tempStr As String = ""
        Dim D_str As String = CStr(CDate(IIf(CommonUtility.Get_StringValue(By_datePrint) <> "", By_datePrint, Date.Now)).Day)
        Dim M_str As String = CStr(CDate(IIf(CommonUtility.Get_StringValue(By_datePrint) <> "", By_datePrint, Date.Now)).Month)
        Dim Y_str As String = CDate(IIf(CommonUtility.Get_StringValue(By_datePrint) <> "", By_datePrint, Date.Now))

        date_tempStr = IIf(D_str.Length = 1, "-" & D_str, D_str) & " " & MonthName(CInt(M_str), True) & " " & Year(CDate(Y_str))

        Return date_tempStr
    End Function
    'get string วันที่ตามตราครุฑ สำหรับ reprint
    Function String_DateSiteReport_Reprint(ByVal By_datePrintSelect As String, ByVal By_datePrintApprove As Object, ByVal By_caseReprint As Boolean) As String
        Dim date_tempStr As String = ""
        Select Case By_caseReprint
            Case True 'ใช้วันที่เลือกเอง
                Dim arr_date As Array = By_datePrintSelect.Split("/")
                Dim D_str As String = CInt(arr_date(0))
                Dim M_str As String = arr_date(1)
                Dim Y_str As String = arr_date(2)

                date_tempStr = IIf(D_str.Length = 1, "-" & D_str, D_str) & " " & MonthName(CInt(M_str), True) & " " & Y_str

            Case False 'ใช้ วันอนุมัติ
                Dim D_str As String = CStr(CDate(CommonUtility.Get_StringValue(By_datePrintApprove)).Day)
                Dim M_str As String = CStr(CDate(CommonUtility.Get_StringValue(By_datePrintApprove)).Month)
                Dim Y_str As String = CDate(CommonUtility.Get_StringValue(By_datePrintApprove))

                date_tempStr = IIf(D_str.Length = 1, "-" & D_str, D_str) & " " & MonthName(CInt(M_str), True) & " " & Year(CDate(Y_str))

        End Select

        Return date_tempStr
    End Function
    'หาเลข บัตรประชาชน
    Public Function Req_CheckIDSealSign_perID(ByVal By_perID As String) As String
        Dim re_id As String = ""
        Dim strEDIConn As String = ConfigurationManager.ConnectionStrings("OriginConnection").ConnectionString
        Dim dsSeal As New DataSet

        dsSeal = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, "vi_GetBy_AutoID_ImagesSign_NewDS2",
                           New SqlParameter("@im_AutoID", By_perID))

        If dsSeal.Tables(0).Rows.Count > 0 Then
            Dim str_temp As String = dsSeal.Tables(0).Rows(0).Item("im_AuthPersonID")
            re_id = str_temp
        End If

        Return re_id
    End Function
#End Region

End Module
