Imports Microsoft.VisualBasic
Imports Microsoft.ApplicationBlocks.Data
Imports System.Data.SqlClient
Imports System.Data
Imports System.Text

Imports System.Drawing

Imports DFT.Dotnetnuke.ClassLibrary

Public Class Receipt_Print
    '������¡��������ͧ printer
    'Public Shared Function PrintReceipt_(ByVal sendSiteId As String) As String
    '    Dim Print_name As String
    '    Select Case sendSiteId
    '        Case "ST-001"
    '            Print_name = ConfigurationManager.AppSettings("ST_001Edi_Receipt").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
    '            Return Print_name

    '    End Select
    'End Function

    Public Shared Function PrintReceipt_(ByVal sendSiteId As String, ByVal selectPrints As String, ByVal sendUser As String, ByVal _RoleID As Boolean) As String
        Dim Print_name As String
        Select Case sendSiteId
            Case "ST-001"
                ''ByTine 22-06-2559 ������������ʴԡ����͡����ͧ����稡ͧ�ع (��Ѻ����ͧ�ѹ)
                Print_name = ConfigurationManager.AppSettings("printNameReceiptST_001_01").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                Return Print_name
                'Select Case sendUser
                '    'Case "TIPPARPORN", "SURANG" 'set DS 2
                '    '    Select Case selectPrints 'set ���� ����������ͧ print ��������ͧ
                '    '        Case "1"
                '    '            Print_name = ConfigurationManager.AppSettings("printNameReceipt_GL03").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                '    '            Return Print_name
                '    '    End Select
                '    Case Else
                '        Select Case selectPrints 'set ���� ����������ͧ print ��������ͧ
                '            Case "1"
                '                'Print_name = ConfigurationManager.AppSettings("printNameReceipt_GL").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                '                Print_name = ConfigurationManager.AppSettings("printNameReceiptST_001_01").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                '                Return Print_name
                '            Case "2"
                '                'Print_name = ConfigurationManager.AppSettings("printNameReceipt_GL02").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                '                Print_name = ConfigurationManager.AppSettings("printNameReceiptST_001_02").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                '                Return Print_name
                '            Case "3"
                '                Print_name = ConfigurationManager.AppSettings("printNameReceiptST_001_03").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                '                Return Print_name
                '            Case "4"
                '                Print_name = ConfigurationManager.AppSettings("printNameReceiptST_001_04").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                '                Return Print_name
                '        End Select
                'End Select

            Case "ST-001T"
                Select Case selectPrints 'set ���� ����������ͧ print ��������ͧ
                    Case "1"
                        Print_name = ConfigurationManager.AppSettings("printNameReceipt_GLT01").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                        Return Print_name

                End Select
            Case "ST-002", "ST-004N", "ST-004" '��ͧ��
                ''ByTine 22-06-2559 ������������ʴԡ����͡����ͧ����稡ͧ�ع (��Ѻ����ͧ�ѹ)
                If _RoleID = True Then
                    ''DS ��ҹ��
                    Print_name = ConfigurationManager.AppSettings("printNameReceiptST_002_01_DS").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                    Return Print_name
                Else
                    Print_name = ConfigurationManager.AppSettings("printNameReceiptST_002_01").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                    Return Print_name
                End If

                'Select Case selectPrints 'set ���� ����������ͧ print ��������ͧ
                '    Case "1"
                '        Print_name = ConfigurationManager.AppSettings("printNameReceiptST_002_01").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                '        Return Print_name
                '    Case "2"
                '        Print_name = ConfigurationManager.AppSettings("printNameReceiptST_002_02").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                '        Return Print_name
                '    Case "3"
                '        Print_name = ConfigurationManager.AppSettings("printNameReceiptST_002_03").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                '        Return Print_name
                '    Case "4"
                '        Print_name = ConfigurationManager.AppSettings("printNameReceiptST_002_04").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                '        Return Print_name
                '    Case "5"
                '        Print_name = ConfigurationManager.AppSettings("printNameReceiptST_002_05").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                '        Return Print_name
                '    Case "6"
                '        Print_name = ConfigurationManager.AppSettings("printNameReceiptST_002_06").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                '        Return Print_name
                '    Case "7" 'by rut add ����
                '        Print_name = ConfigurationManager.AppSettings("printNameReceiptST_002_07").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                '        Return Print_name
                '    Case "8" 'by rut add ���� DS2
                '        Print_name = ConfigurationManager.AppSettings("printNameReceiptST_002_08").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                '        Return Print_name
                'End Select
            Case "CB-003" '�ź���
                Select Case selectPrints 'set ���� ����������ͧ print ��������ͧ
                    Case "1"
                        Print_name = ConfigurationManager.AppSettings("printNameReceiptCB_003_01").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                        Return Print_name
                    Case "2"
                        Print_name = ConfigurationManager.AppSettings("printNameReceiptCB_003_02").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                        Return Print_name
                End Select
            Case "HY-002" '�Ҵ�˭�
                Select Case selectPrints 'set ���� ����������ͧ print ��������ͧ
                    Case "1"
                        Print_name = ConfigurationManager.AppSettings("printNameReceiptHY_002_01").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                        Return Print_name

                End Select
            Case "CR-006" '��§���
                Select Case selectPrints 'set ���� ����������ͧ print ��������ͧ
                    Case "1"
                        Print_name = ConfigurationManager.AppSettings("printNameReceiptCR_006_01").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                        Return Print_name

                End Select
            Case "CM-001" '��§����
                Select Case selectPrints 'set ���� ����������ͧ print ��������ͧ
                    Case "1"
                        Print_name = ConfigurationManager.AppSettings("printNameReceiptCM_001_01").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                        Return Print_name
                    Case "2"
                        Print_name = ConfigurationManager.AppSettings("printNameReceiptCM_001_02").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                        Return Print_name
                End Select
            Case "ST-003" '����ó����
                Select Case selectPrints 'set ���� ����������ͧ print ��������ͧ
                    Case "1"
                        Print_name = ConfigurationManager.AppSettings("printNameReceiptST_003_01").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                        Return Print_name
                    Case "2"
                        Print_name = ConfigurationManager.AppSettings("printNameReceiptST_003_02").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                        Return Print_name
                End Select
            Case "ST-004XXX"
                Select Case selectPrints 'set ���� ����������ͧ print ��������ͧ
                    Case "1"
                        Print_name = ConfigurationManager.AppSettings("printNameReceiptST_004_01").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                        Return Print_name
                    Case "2"
                        Print_name = ConfigurationManager.AppSettings("printNameReceiptST_004_02").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                        Return Print_name
                End Select
            Case "SK-004" '������
                Select Case selectPrints 'set ���� ����������ͧ print ��������ͧ
                    Case "1"
                        Print_name = ConfigurationManager.AppSettings("printNameReceiptSK_004_01").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                        Return Print_name

                End Select
            Case "NK-005" '˹ͧ���
                Select Case selectPrints 'set ���� ����������ͧ print ��������ͧ
                    Case "1"
                        Print_name = ConfigurationManager.AppSettings("printNameReceiptNK_005_01").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                        Return Print_name

                End Select
            Case "SG-007" '�������
                Select Case selectPrints 'set ���� ����������ͧ print ��������ͧ
                    Case "1"
                        Print_name = ConfigurationManager.AppSettings("printNameReceiptSG_007_01").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                        Return Print_name

                End Select
            Case "TK-008" '�ҡ
                Select Case selectPrints 'set ���� ����������ͧ print ��������ͧ
                    Case "1"
                        Print_name = ConfigurationManager.AppSettings("printNameReceiptTK_008_01").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                        Return Print_name

                End Select
            Case "MH-009" '�ء�����
                Select Case selectPrints 'set ���� ����������ͧ print ��������ͧ
                    Case "1"
                        Print_name = ConfigurationManager.AppSettings("printNameReceiptMH_009_01").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                        Return Print_name

                End Select
            Case "KR-010" 'ʤ�.ࢵ 10 (�ҭ������)
                Select Case selectPrints 'set ���� ����������ͧ print ��������ͧ
                    Case "1"
                        Print_name = ConfigurationManager.AppSettings("printNameReceiptKR_010_01").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                        Return Print_name

                End Select
            Case "PN-83-001" 'ʾ�.����
                'Dim printer_key As String = "printNameReceiptPN_83_001_0" & selectPrints & "_v2"
                'Print_name = ConfigurationManager.AppSettings(printer_key).ToString()
                Print_name = ConfigurationManager.AppSettings("printNameReceiptPN_83_001_01_v2").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                Return Print_name
            Case "PN-95-001" 'ʾ�.����
                'Dim printer_key As String = "printNameReceiptPN_95_001_0" & selectPrints & "_v2"
                'Print_name = ConfigurationManager.AppSettings(printer_key).ToString()
                Print_name = ConfigurationManager.AppSettings("printNameReceiptPN_95_001_01_v2").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                Return Print_name
            Case "PN-96-001" 'ʾ�.��Ҹ����
                'Dim printer_key As String = "printNameReceiptPN_96_001_0" & selectPrints & "_v2"
                'Print_name = ConfigurationManager.AppSettings(printer_key).ToString()
                Print_name = ConfigurationManager.AppSettings("printNameReceiptPN_96_001_01_v2").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                Return Print_name
            Case "PN-91-001" 'ʾ�.ʵ��
                'Dim printer_key As String = "printNameReceiptPN_91_001_0" & selectPrints & "_v2"
                'Print_name = ConfigurationManager.AppSettings(printer_key).ToString()
                Print_name = ConfigurationManager.AppSettings("printNameReceiptPN_91_001_01_v2").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                Return Print_name
            Case "PN-48-001" 'ʾ�.��þ��
                'Dim printer_key As String = "printNameReceiptPN_48_001_0" & selectPrints & "_v2"
                'Print_name = ConfigurationManager.AppSettings(printer_key).ToString()
                Print_name = ConfigurationManager.AppSettings("printNameReceiptPN_48_001_01_v2").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                Return Print_name
            Case "PN-51-001" 'ʾ�.�Ӿٹ
                'Dim printer_key As String = "printNameReceiptPN_48_001_0" & selectPrints & "_v2"
                'Print_name = ConfigurationManager.AppSettings(printer_key).ToString()
                Print_name = ConfigurationManager.AppSettings("printNameReceiptPN_51_001_01_v2").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                Return Print_name
        End Select
    End Function

    Public Shared Function PrintReceipt_v2(ByVal sendSiteId As String, ByVal selectPrints As String, ByVal _RoleID As Boolean, ByVal sendUser As String) As String
        Dim Print_name As String
        Select Case sendSiteId
            Case "ST-001"
                'Print_name = ConfigurationManager.AppSettings("printNameReceiptST_001_01_v2").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                'Return Print_name
                Select Case sendUser
                    'Case "TIPPARPORN", "SURANG" 'set DS 2
                    '    Select Case selectPrints 'set ���� ����������ͧ print ��������ͧ
                    '        Case "1"
                    '            Print_name = ConfigurationManager.AppSettings("printNameReceipt_GL03").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                    '            Return Print_name
                    '    End Select
                    Case Else
                        ''ByTine 22-06-2559 ��������稡ͧ�ع��͡����ͧ��������ʴԡ�� (��Ѻ����ͧ�ѹ)
                        Select Case selectPrints 'set ���� ����������ͧ print ��������ͧ
                            Case "1"
                                'Print_name = ConfigurationManager.AppSettings("printNameReceipt_GL").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                                Print_name = ConfigurationManager.AppSettings("printNameReceiptST_001_01_v2").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                                Return Print_name
                            Case "2"
                                'Print_name = ConfigurationManager.AppSettings("printNameReceipt_GL02").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                                Print_name = ConfigurationManager.AppSettings("printNameReceiptST_001_02_v2").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                                Return Print_name
                            Case "3"
                                Print_name = ConfigurationManager.AppSettings("printNameReceiptST_001_03_v2").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                                Return Print_name
                            Case "4"
                                Print_name = ConfigurationManager.AppSettings("printNameReceiptST_001_04_v2").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                                Return Print_name
                        End Select
                End Select
            Case "ST-001T"
                Print_name = ConfigurationManager.AppSettings("printNameReceipt_GLT01_v2").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                Return Print_name
            Case "ST-002", "ST-004N", "ST-004" '��ͧ��
                '//2017-10-07 ����ͧ��Ǩ���͹� ds ���� �͡����ͧ���ǡѹ ੾�Ф�ͧ��
                _RoleID = False
                If _RoleID = True Then
                    Print_name = ConfigurationManager.AppSettings("printNameReceiptST_002_01DS_v2").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                    Return Print_name
                Else
                    '  Print_name = ConfigurationManager.AppSettings("printNameReceiptST_002_01").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                    ''ByTine 22-06-2559 ��������稡ͧ�ع��͡����ͧ��������ʴԡ�� (��Ѻ����ͧ�ѹ)
                    Select Case selectPrints 'set ���� ����������ͧ print ��������ͧ
                        Case "1"
                            Print_name = ConfigurationManager.AppSettings("printNameReceiptST_002_01_v2").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                            Return Print_name
                        Case "2"
                            Print_name = ConfigurationManager.AppSettings("printNameReceiptST_002_02_v2").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                            Return Print_name
                        Case "3"
                            Print_name = ConfigurationManager.AppSettings("printNameReceiptST_002_03_v2").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                            Return Print_name
                        Case "4"
                            Print_name = ConfigurationManager.AppSettings("printNameReceiptST_002_04_v2").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                            Return Print_name
                        Case "5"
                            Print_name = ConfigurationManager.AppSettings("printNameReceiptST_002_05_v2").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                            Return Print_name
                        Case "6"
                            Print_name = ConfigurationManager.AppSettings("printNameReceiptST_002_06_v2").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                            Return Print_name
                        Case "7" 'by rut add ����
                            Print_name = ConfigurationManager.AppSettings("printNameReceiptST_002_07_v2").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                            Return Print_name
                            'Case "8" 'by rut add ���� DS2
                            '    Print_name = ConfigurationManager.AppSettings("printNameReceiptST_002_08_v2").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                            '    Return Print_name
                    End Select
                End If
                'Case "ST-002" '��ͧ��
                '    Print_name = ConfigurationManager.AppSettings("printNameReceiptST_002_01_v2").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                '    Return Print_name
            Case "CB-003" '�ź���
                Print_name = ConfigurationManager.AppSettings("printNameReceiptCB_003_01_v2").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                Return Print_name
            Case "HY-002" '�Ҵ�˭�
                Print_name = ConfigurationManager.AppSettings("printNameReceiptHY_002_01_v2").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                Return Print_name
            Case "CR-006" '��§���
                Print_name = ConfigurationManager.AppSettings("printNameReceiptCR_006_01_v2").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                Return Print_name
            Case "CM-001" '��§����
                Print_name = ConfigurationManager.AppSettings("printNameReceiptCM_001_01_v2").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                Return Print_name
            Case "ST-003" '����ó����
                Select Case selectPrints
                    Case "0"
                        Print_name = ConfigurationManager.AppSettings("printNameReceiptST_003_01_v2").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                    Case "2"
                        Print_name = ConfigurationManager.AppSettings("printNameReceiptST_003_03").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                    Case "1"
                        Print_name = ConfigurationManager.AppSettings("printNameReceiptST_003_02_v2").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                End Select
                Return Print_name
            Case "ST-004XXX" '��
                Print_name = ConfigurationManager.AppSettings("printNameReceiptST_004_01_v2").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                Return Print_name
            Case "SK-004" '������
                Print_name = ConfigurationManager.AppSettings("printNameReceiptSK_004_01_v2").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                Return Print_name
            Case "NK-005" '˹ͧ���
                Print_name = ConfigurationManager.AppSettings("printNameReceiptNK_005_01_v2").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                Return Print_name
            Case "SG-007" '�������
                Print_name = ConfigurationManager.AppSettings("printNameReceiptSG_007_01_v2").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                Return Print_name
            Case "TK-008" '�ҡ
                Print_name = ConfigurationManager.AppSettings("printNameReceiptTK_008_01_v2").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                Return Print_name
            Case "MH-009" '�ء�����
                Print_name = ConfigurationManager.AppSettings("printNameReceiptMH_009_01_v2").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                Return Print_name
            Case "KR-010" 'ʤ�.ࢵ 10 (�ҭ������)
                Print_name = ConfigurationManager.AppSettings("printNameReceiptKR_010_01_v2").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                Return Print_name
            Case "PN-83-001" 'ʾ�.����
                'Dim printer_key As String = "printNameReceiptPN_83_001_0" & selectPrints & "_v2"
                'Print_name = ConfigurationManager.AppSettings(printer_key).ToString()
                Print_name = ConfigurationManager.AppSettings("printNameReceiptPN_83_001_01_v2").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                Return Print_name
            Case "PN-95-001" 'ʾ�.����
                'Dim printer_key As String = "printNameReceiptPN_95_001_0" & selectPrints & "_v2"
                'Print_name = ConfigurationManager.AppSettings(printer_key).ToString()
                Print_name = ConfigurationManager.AppSettings("printNameReceiptPN_95_001_01_v2").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                Return Print_name
            Case "PN-96-001" 'ʾ�.��Ҹ����
                'Dim printer_key As String = "printNameReceiptPN_96_001_0" & selectPrints & "_v2"
                'Print_name = ConfigurationManager.AppSettings(printer_key).ToString()
                Print_name = ConfigurationManager.AppSettings("printNameReceiptPN_96_001_01_v2").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                Return Print_name
            Case "PN-91-001" 'ʾ�.ʵ��
                'Dim printer_key As String = "printNameReceiptPN_91_001_0" & selectPrints & "_v2"
                'Print_name = ConfigurationManager.AppSettings(printer_key).ToString()
                Print_name = ConfigurationManager.AppSettings("printNameReceiptPN_91_001_01_v2").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                Return Print_name
            Case "PN-48-001" 'ʾ�.��þ��
                'Dim printer_key As String = "printNameReceiptPN_48_001_0" & selectPrints & "_v2"
                'Print_name = ConfigurationManager.AppSettings(printer_key).ToString()
                Print_name = ConfigurationManager.AppSettings("printNameReceiptPN_48_001_01_v2").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                Return Print_name
            Case "PN-51-001" 'ʾ�.�Ӿٹ
                'Dim printer_key As String = "printNameReceiptPN_48_001_0" & selectPrints & "_v2"
                'Print_name = ConfigurationManager.AppSettings(printer_key).ToString()
                Print_name = ConfigurationManager.AppSettings("printNameReceiptPN_51_001_01_v2").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
                Return Print_name
        End Select
    End Function

    'Public Shared Function Call_SitePrints_ST_001(ByVal Form_ID As String) As String
    '    Dim SendPrinterName As String
    '    Select Case Form_ID
    '        Case "ST-001"
    '            SendPrinterName = ConfigurationManager.AppSettings("printNameCallEPTest").ToString()
    '            Return SendPrinterName
    '    End Select
    'End Function
End Class
