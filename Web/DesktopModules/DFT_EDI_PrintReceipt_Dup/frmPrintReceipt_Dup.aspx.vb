Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports DFT.Dotnetnuke.ClassLibrary
Imports System.Threading

Partial Public Class frmPrintReceipt_Dup
    Inherits System.Web.UI.Page
    Dim strEDIConn As String = ConfigurationManager.ConnectionStrings("OriginConnection").ConnectionString
    Dim _BillType As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        _BillType = Request.QueryString("BillType")

        If Not Page.IsPostBack Then
            If Request.QueryString("bill_no") <> "" And Request.QueryString("site") <> "" Then
                Dim rpt As Object = Nothing
                txtBill_no.Text = Request.QueryString("bill_no")
                txtSite_ID.Text = Request.QueryString("site")

                Dim ds As New DataSet
                If _BillType = "1" Then
                    ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, "sp_get_receipt_new", _
                    New SqlParameter("@B_NO", txtBill_no.Text), _
                    New SqlParameter("@SITE_ID", txtSite_ID.Text))

                    rpt = New receipt_rpt
                    Me.WebViewer1.ViewerType = DataDynamics.ActiveReports.Web.ViewerType.AcrobatReader

                ElseIf _BillType = "2" Then
                    ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, "sp_get_receipt_new_v2", _
                    New SqlParameter("@B_NO", txtBill_no.Text), _
                    New SqlParameter("@SITE_ID", txtSite_ID.Text))

                    rpt = New rpt_Receipt_Yellow
                    Me.WebViewer1.ViewerType = DataDynamics.ActiveReports.Web.ViewerType.AcrobatReader

                    Dim CheckCulture As String = Thread.CurrentThread.CurrentCulture.ToString
                    rpt.datasource = ds.Tables(0)

                    rpt.lblTime.Text = ds.Tables(0).Rows(0).Item("B_TIME")
                    rpt.lblDate.Text = ds.Tables(0).Rows(0).Item("B_Date")
                    rpt.lblMonth.Text = ThaiMonth(ds.Tables(0).Rows(0).Item("B_Month"))

                    If CheckCulture <> "th-TH" Then
                        rpt.lblYear.Text = ds.Tables(0).Rows(0).Item("B_Year") + 543
                    Else
                        rpt.lblYear.Text = ds.Tables(0).Rows(0).Item("B_Year")
                    End If

                    'rpt.lblTime.Text = Now.ToString("HH:mm:ss")
                    'rpt.lblDate.Text = Now.Day
                    'rpt.lblMonth.Text = ThaiMonth(Now.Month)

                    'If CheckCulture <> "th-TH" Then
                    '    rpt.lblYear.Text = Now.Year + 543
                    'Else
                    '    rpt.lblYear.Text = Now.Year
                    'End If

                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                        Dim Total1 As Decimal
                        Dim Total2 As Decimal
                        Total1 = ds.Tables(0).Rows(i).Item("amt")

                        Total2 = Total2 + Total1
                        rpt.lblTotalAmount.Text = Total2
                    Next
                    'rpt.lblTextAmount.Text = ThaiBaht(rpt.lblTotalAmount.Text)

                    rpt.txtCompanyName.Text = CommonUtility.Get_StringValue(ds.Tables(0).Rows(0).Item("receipt_name")) & "  (" & CommonUtility.Get_StringValue(ds.Tables(0).Rows(0).Item("company_taxno")) & ")"

                End If

                WebViewer1.Report = rpt
                WebViewer1.Height = 700

                Page.Title = "�ʴ������"
                WebViewer1.Report.DataSource = ds.Tables(0)
                Page.SetFocus(Page)
                WebViewer1.Focus()

            End If
        End If
    End Sub

    ''ByTine 07-01-2559 ��Ѻ����ͧ���������
    Function ThaiMonth(ByVal Month As String) As String
        Try
            Select Case Month
                Case "01", "1"
                    Month = "���Ҥ�"
                Case "02", "2"
                    Month = "����Ҿѹ��"
                Case "03", "3"
                    Month = "�չҤ�"
                Case "04", "4"
                    Month = "����¹"
                Case "05", "5"
                    Month = "����Ҥ�"
                Case "06", "6"
                    Month = "�Զع�¹"
                Case "07", "7"
                    Month = "�á�Ҥ�"
                Case "08", "8"
                    Month = "�ԧ�Ҥ�"
                Case "09", "9"
                    Month = "�ѹ��¹"
                Case "10"
                    Month = "���Ҥ�"
                Case "11"
                    Month = "��Ȩԡ�¹"
                Case "12"
                    Month = "�ѹ�Ҥ�"
            End Select

            Return Month

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Function

    Function ThaiBaht(ByVal pAmount As Double) As String
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
        ' �������շȹ��    z
        If _decimalTranslatedText.Trim = "" Then
            _integerTranslatedText += "�ҷ��ǹ"
        Else
            _integerTranslatedText += "�ҷ" & _decimalTranslatedText & "ʵҧ��"
        End If
        Return _integerTranslatedText
    End Function

    Function NumberToText(ByVal pAmount As Double) As String
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

End Class