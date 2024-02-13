Imports DataDynamics.ActiveReports 
Imports DataDynamics.ActiveReports.Document 

Imports System.Threading
Imports System.Globalization
Public Class receipt_rpt 
    Dim Cpage As Integer
    Dim sum_all As Double
    Function SelectdateTime(ByVal _billdate As Date) As String
        Dim _strBilldate As String
        _strBilldate = Format(_billdate, "hh:mm:ss")

        Return _strBilldate
    End Function
    Function setDateTime(ByVal dateValue As Date) As String
        Dim str_date As String

        Dim CI As System.Globalization.CultureInfo
        CI = System.Globalization.CultureInfo.GetCultureInfo("en-US")
        Dim dt As DateTime = dateValue
        str_date = dt.ToString("dd/MM/yyyy HH:mm:ss", CI)

        Return str_date
    End Function
    Private Sub PageHeader1_Format(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PageHeader1.Format
        'Thread.CurrentThread.CurrentCulture = New CultureInfo("th-TH")
        'Thread.CurrentThread.CurrentUICulture = New CultureInfo("th-TH")

        Cpage += 1
        txtPage2.Text = Cpage

        txtbill_date.Text = setDateTime(CDate(TextBox1.Value))
        'txtbill_date.Text = Format(CDate(TextBox1.Value), "dd/MM/yyy") & " " & SelectdateTime(CDate(TextBox1.Value))
        ' txtbill_date.Text = Format(CDate(TextBox1.Value), "d/MM/yyy") & " " & CDate(TextBox1.Value).TimeOfDay.Hours & ":" & CDate(TextBox1.Value).TimeOfDay.Minutes & ":" & CDate(TextBox1.Value).TimeOfDay.Seconds 'CDate(TextBox1.Value).Day & "/" & CDate(TextBox1.Value).Month & "/" & CDate(TextBox1.Value).Year & " " & CDate(TextBox1.Value).TimeOfDay.Hours & ":" & CDate(TextBox1.Value).TimeOfDay.Minutes & ":" & CDate(TextBox1.Value).TimeOfDay.Seconds

    End Sub

    Private Sub PageFooter1_Format(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PageFooter1.Format
        'If page_star = txtNumpageForm.Value Then
        '    txtSumamt.Text = Format(sum_all, "#,##0.00")
        '    txtbText.Text = "( " & ThaiBaht(sum_all) & " )"
        'End If
    End Sub
    Private Sub Detail1_Format(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Detail1.Format
        sum_all = sum_all + txtamt.Value
        'txtTemp_reference_code2.Text = "I" & txtreference_code2.Text
        'txtTempt_reference_code2.Text = "I" & txtreference_code2.Text

        ''ByTine �Ѵ I �͡�ҡ����� 20-01-2560
        txtTemp_reference_code2.Text = txtreference_code2.Text
        txtTempt_reference_code2.Text = txtreference_code2.Text
    End Sub

    Public Shared Function ThaiBaht(ByVal pAmount As Double) As String
        If pAmount = 0 Then
            Return "�ٹ��ҷ��ǹ"
        End If
        Dim _integerValue As String ' �ӹǹ���    
        Dim _decimalValue As String ' �ȹ���     
        Dim _integerTranslatedText As String ' �ӹǹ��� ������     
        Dim _decimalTranslatedText As String ' �ȹ���������    
        _integerValue = Format(pAmount, "####.00") ' �Ѵ Format ����Թ�繵���Ţ 2 ��ѡ   
        _decimalValue = Mid(_integerValue, Len(_integerValue) - 1, 2) ' �ȹ���    
        _integerValue = Mid(_integerValue, 1, Len(_integerValue) - 3) ' �ӹǹ���    
        ' �ŧ �ӹǹ��� �� ������    
        _integerTranslatedText = NumberToText(CDbl(_integerValue))
        ' �ŧ �ȹ��� �� ������     
        If CDbl(_decimalValue) <> 0 Then
            _decimalTranslatedText = NumberToText(CDbl(_decimalValue))
        Else
            _decimalTranslatedText = ""
        End If
        ' �������շȹ��    
        If _decimalTranslatedText.Trim = "" Then
            _integerTranslatedText += "�ҷ��ǹ"
        Else
            _integerTranslatedText += "�ҷ" & _decimalTranslatedText & "ʵҧ��"
        End If
        Return _integerTranslatedText
    End Function

    Private Shared Function NumberToText(ByVal pAmount As Double) As String
        ' ����ѡ��   
        Dim _numberText() As String = {"", "˹��", "�ͧ", "���", "���", "���", "ˡ", "��", "Ỵ", "���", "�Ժ"}
        ' ��ѡ ˹��� �Ժ ���� �ѹ ...   
        Dim _digit() As String = {"", "�Ժ", "����", "�ѹ", "����", "�ʹ", "��ҹ"}
        Dim _value As String, _aWord As String, _text As String
        Dim _numberTranslatedText As String = ""
        Dim _length, _digitPosition As Integer
        _value = pAmount.ToString
        _length = Len(_value)
        ' ��Ҵ�ͧ �����ŷ���ͧ����ŧ �� 122200 �բ�Ҵ ��ҡѺ 6   
        For i As Integer = 0 To _length - 1
            ' ǹ�ٻ ������ҡ 0 ���֧ (��Ҵ - 1)       
            ' ���˹觢ͧ ��ѡ (digit) �ͧ����Ţ      
            ' ��       ' ���˹���ѡ���0 (��ѡ˹���)      
            ' ���˹���ѡ���1 (��ѡ�Ժ)       
            ' ���˹���ѡ���2 (��ѡ����)      
            ' ����繢����� i = 7 ���˹���ѡ����ҡѺ 1 (��ѡ�Ժ)      
            ' ����繢����� i = 9 ���˹���ѡ����ҡѺ 3 (��ѡ�ѹ)       
            ' ����繢����� i = 13 ���˹���ѡ����ҡѺ 1 (��ѡ�Ժ)      
            _digitPosition = i - (6 * ((i - 1) \ 6))
            _aWord = Mid(_value, Len(_value) - i, 1)
            _text = ""
            Select Case _digitPosition
                Case 0 ' ��ѡ˹���               
                    If _aWord = "1" And _length > 1 Then
                        ' ������Ţ 1 ����բ�Ҵ�ҡ���� 1 ����դ����ҡѺ "���"                  
                        _text = "���"
                    ElseIf _aWord <> "0" Then
                        ' ���������Ţ 0 ����� ����ѡ�� � _numberText()                   
                        _text = _numberText(CInt(_aWord))
                    End If
                Case 1 ' ��ѡ�Ժ               
                    If _aWord = "1" Then
                        ' ������Ţ 1 ����ͧ�� ����ѡ�� ����ա �͡�ҡ����� "�Ժ"                  
                        '_numberTranslatedText = "�Ժ" & _numberTranslatedText                  
                        _text = _digit(_digitPosition)
                    ElseIf _aWord = "2" Then
                        ' ������Ţ 2 ������ѡ�ä�� "����Ժ"                  
                        _text = "���" & _digit(_digitPosition)
                    ElseIf _aWord <> "0" Then
                        ' ���������Ţ 0 ����� ����ѡ�� � _numberText() �������ѡ(digit) � _digit()                 
                        _text = _numberText(CInt(_aWord)) & _digit(_digitPosition)
                    End If
                Case 2, 3, 4, 5 ' ��ѡ���� �֧ �ʹ               
                    If _aWord <> "0" Then
                        _text = _numberText(CInt(_aWord)) & _digit(_digitPosition)
                    End If
                Case 6 ' ��ѡ��ҹ              
                    If _aWord = "0" Then
                        _text = "��ҹ"
                    ElseIf _aWord = "1" And _length - 1 > i Then
                        _text = "�����ҹ"
                    Else
                        _text = _numberText(CInt(_aWord)) & _digit(_digitPosition)
                    End If
            End Select
            _numberTranslatedText = _text & _numberTranslatedText
        Next
        Return _numberTranslatedText
    End Function

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        Me.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.Custom
        Me.PageSettings.PaperName = "Letter Fanfold 8 1/2 x 11 in"
        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub ReportFooter1_Format(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ReportFooter1.Format
        txtSumamt.Text = Format(sum_all, "#,##0.00")
        'txtbText.Text = "( " & ThaiBaht(sum_all) & " )"
    End Sub
End Class
