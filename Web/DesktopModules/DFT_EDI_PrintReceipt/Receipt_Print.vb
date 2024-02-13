Imports Microsoft.VisualBasic
Imports Microsoft.ApplicationBlocks.Data
Imports System.Data.SqlClient
Imports System.Data
Imports System.Text

Imports System.Drawing

Imports DFT.Dotnetnuke.ClassLibrary

Public Class Receipt_Print
    'ไว้เรียกชื่อเครื่อง printer
    Public Shared Function PrintReceipt_(ByVal sendSiteId As String) As String
        Dim Print_name As String
        Select Case sendSiteId
            Case "ST-001"
                Print_name = ConfigurationManager.AppSettings("printNameCallEPTest").ToString() 'Call_SitePrints_ST_001(sendFormTypePrinter)
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
