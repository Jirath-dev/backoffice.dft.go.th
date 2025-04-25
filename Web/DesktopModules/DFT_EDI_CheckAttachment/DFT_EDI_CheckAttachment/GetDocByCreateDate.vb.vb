Imports System.Data
Imports System.Data.SqlClient
Imports DFT.Dotnetnuke.ClassLibrary
Imports Microsoft.ApplicationBlocks.Data

Public Class GetDocByCreateDate
    Shared StrEDIConn As String = ConfigurationManager.ConnectionStrings("OriginConnection").ConnectionString

    Public Shared Function GetUploadDate(ByVal lblInvh_Run_Auto As String) As String
        Dim CreateDate As String = ""
        Dim Year As String = ""
        Dim Month As String = ""
        Dim Strcommand As String
        Strcommand = " SELECT DISTINCT YEAR(CreateDate) AS Year, MONTH(CreateDate) AS Month FROM DocumentFile_edi " & _
       " WHERE (invh_run_auto = '" & CommonUtility.Get_String(lblInvh_Run_Auto) & "')"
        Dim ds As New DataSet
        ds = SqlHelper.ExecuteDataset(StrEDIConn, CommandType.Text, Strcommand)
        If ds.Tables(0).Rows.Count > 0 Then
            Year = ds.Tables(0).Rows(0).Item("Year")
            Month = ds.Tables(0).Rows(0).Item("Month")

            Select Case Month
                Case "1"
                    CreateDate = Year & "-01"
                Case "2"
                    CreateDate = Year & "-02"
                Case "3"
                    CreateDate = Year & "-03"
                Case "4"
                    CreateDate = Year & "-04"
                Case "5"
                    CreateDate = Year & "-05"
                Case "6"
                    CreateDate = Year & "-06"
                Case "7"
                    CreateDate = Year & "-07"
                Case "8"
                    CreateDate = Year & "-08"
                Case "9"
                    CreateDate = Year & "-09"
                Case "10"
                    CreateDate = Year & "-10"
                Case "11"
                    CreateDate = Year & "-11"
                Case "12"
                    CreateDate = Year & "-12"
            End Select
            Return CreateDate
        End If
    End Function
End Class
