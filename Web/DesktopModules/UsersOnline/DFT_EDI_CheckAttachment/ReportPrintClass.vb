Imports Microsoft.VisualBasic
Imports Microsoft.ApplicationBlocks.Data
Imports System.Data.SqlClient
Imports System.Data
Imports System.Text

Imports System.Drawing

Imports DFT.Dotnetnuke.ClassLibrary
Public Class ReportPrintClass
   
#Region "   Function Other   "
    '=======================================================================
    'ใช้สำหรับ พิมพ์วันที่ 04/09/2010
    Public Shared Function format_dateSelect() As String
        Dim _date As Date
        _date = Date.Today

        Return Format(_date, "dd/MM/yyy")
    End Function
    ''ใช้สำหรับเวลา แสดง 08:05:12
    'Function SelectdateTime(ByVal _billdate As Date) As String
    '    Dim _strBilldate As String
    '    _strBilldate = Format(_billdate, "hh:mm:ss")

    '    Return _strBilldate
    'End Function
    'check and return "" เป็น 0.0
    Public Shared Function Check_Null(ByVal V_txt As Object) As Object
        If String.IsNullOrEmpty(Trim(V_txt.ToString)) = True Then
            Return 0.0
        Else
            Return V_txt
        End If
    End Function
#End Region

End Class
