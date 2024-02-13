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

    ''ByTine 28-07-2558 CheckBlackList / WatchList
    Public Shared Function CheckBlackList(ByVal _CompanyTaxno As String) As String
        Try
            Dim Result As String = ""
            Dim strRegConn As String = ConfigurationManager.ConnectionStrings("RegisBackConnection").ConnectionString
            Dim ds As New DataSet
            ds = SqlHelper.ExecuteDataset(strRegConn, CommandType.StoredProcedure, "viGet_BlackListByCompany_NewDS", New SqlParameter("@COMPANY_TAXNO", _CompanyTaxno.Trim))
            If ds.Tables(0).Rows.Count > 0 Then
                Select Case CommonUtility.Get_StringValue(ds.Tables(0).Rows(0).Item("type_mistake"))
                    Case "B"
                        Result = "*** บริษัท ติด Black List !!! ***"
                    Case "W"
                        Result = "*** บริษัท ติด Watch List !!! ***"
                    Case Else
                        Result = ""
                End Select
            End If

            Return Result

        Catch ex As Exception
            Return ""
        End Try
    End Function

    Public Shared Function CheckOrginAlert(ByVal CompanyTaxno As String) As String
        Try
            Dim Result As String = ""

            Dim objService As New DFTOriginAlert.ws_blacklistwatchlist
            Dim objBlacklist() As DFTOriginAlert.clsExporter
            objBlacklist = objService.getExporterBlacklistWatchlist(CompanyTaxno)

            If objBlacklist.Length > 0 And objBlacklist(0).Msg <> "ไม่พบข้อมูล" Then
                Result = "*** " & objBlacklist(0).BlacklistType & " *** (คลิกเพื่อแสดงรายละเอียด)"
            End If

            Return Result

        Catch ex As Exception
            Return ""
        End Try
    End Function

    Public Shared Function GetOriginAlertDetail(ByVal CompanyTaxno As String) As DFTOriginAlert.clsExporter()

        Dim ret() As DFTOriginAlert.clsExporter

        Try
            Dim objService As New DFTOriginAlert.ws_blacklistwatchlist
            ret = objService.getExporterBlacklistWatchlist(CompanyTaxno)

        Catch ex As Exception
            ret = Nothing
        End Try
        Return ret

    End Function

    ''12-10-2559
    ''ByTine แสดงวันที่และเลขของ B2B ในฟอร์มช่อง7
    Public Shared Function LoadDataForB2B(ByVal _InvhRunAuto As String)
        Try
            Dim strEDIConn As String = ConfigurationManager.ConnectionStrings("EDIConnection").ConnectionString
            Dim _TempData As String = ""
            Dim npm As New SqlParameter("@invh_run_auto", _InvhRunAuto)
            Dim ds As New DataSet
            ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, "sp_b2b_Load_For_PrintForm", npm)
            If ds.Tables(0).Rows.Count > 0 Then
                For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                    If i = ds.Tables(0).Rows.Count - 1 Then
                        _TempData += "        " & ds.Tables(0).Rows(i).Item("B2BData")
                        'ElseIf i = 0 Then
                        '    _TempData += ds.Tables(0).Rows(i).Item("B2BData") & vbNewLine
                    Else
                        _TempData += "        " & ds.Tables(0).Rows(i).Item("B2BData") & vbNewLine
                    End If
                Next

                Return _TempData

            End If
        Catch ex As Exception

        End Try
    End Function
    Public Shared Function LoadDataForB2B_v2(ByVal _InvhRunAuto As String)
        Try
            Dim strEDIConn As String = ConfigurationManager.ConnectionStrings("EDIConnection").ConnectionString
            Dim _TempData As String = ""
            Dim npm As New SqlParameter("@invh_run_auto", _InvhRunAuto)
            Dim ds As New DataSet
            ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, "sp_b2b_Load_For_PrintForm_V2", npm)
            If ds.Tables(0).Rows.Count > 0 Then
                For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                    If i = ds.Tables(0).Rows.Count - 1 Then
                        _TempData += "        " & ds.Tables(0).Rows(i).Item("B2BData")
                        'ElseIf i = 0 Then
                        '    _TempData += ds.Tables(0).Rows(i).Item("B2BData") & vbNewLine
                    Else
                        _TempData += "        " & ds.Tables(0).Rows(i).Item("B2BData") & vbNewLine
                    End If
                Next

                Return _TempData

            End If
        Catch ex As Exception

        End Try
    End Function
#Region "   function เอาวันที่ที่เลือกเพื่อ พิมพ์ย้อนหลังแบบเลือกวันเอง กรณี อนุมัติคนละวันกับที่พิมพ์   "
    Public Shared Sub RPT_select(ByVal objRPTPrint As Object, ByVal _date As DateTime, ByVal _ValueCheck As Boolean)
        Dim str_date As String = ""
        Dim str_ValueCheck As String = ""

        Select Case _ValueCheck
            Case True 'เลือกวันพิมพ์เอง
                str_date = CommonUtility.Get_DateTime(_date).ToString("dd/MM/yyyy")
                str_ValueCheck = _ValueCheck
            Case False 'เอาตามวันที่อนุมัติ
                'str_date = CommonUtility.Get_DateTime(_date).ToString("dd/MM/yyyy")
                str_ValueCheck = _ValueCheck
        End Select
        objRPTPrint.txtdateSelectRPT.text = str_date
        objRPTPrint.txtSendCheckSeletedate.text = str_ValueCheck
    End Sub
#End Region

    ''ByTine 5-1-2564 Set เปิด/ปิด Ws Rover
    Public Shared Function CheckSetActiveWsRover() As Boolean
        Dim strEDIConn As String = ConfigurationManager.ConnectionStrings("EDIConnection").ConnectionString
        Dim ret As Boolean = False
        Try
            Dim strcommand As String = "SELECT ISNULL(IsOpen, 0) AS IsOpen FROM CO.WebserviceSetActive WHERE (WebserviceName = N'ServiceRollver')"
            Dim Ds As DataSet = SqlHelper.ExecuteDataset(strEDIConn, CommandType.Text, strcommand)
            ret = Ds.Tables(0).Rows(0).Item("IsOpen")
        Catch ex As Exception
            ret = False
        End Try
        Return ret
    End Function

End Class
