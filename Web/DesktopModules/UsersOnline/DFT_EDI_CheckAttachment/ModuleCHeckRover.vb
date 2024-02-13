Imports Microsoft.ApplicationBlocks.Data
Imports DFT.Dotnetnuke.ClassLibrary
Imports System.Data.SqlClient

Module ModuleCHeckRover
    Dim RollverConn As String = ConfigurationManager.ConnectionStrings("RollverConnection").ConnectionString

    Function CheckRollver(ByVal Company_Taxno As String, ByVal Conutry As String, ByVal Harmonized_no As String, ByVal CerNo As String) As Boolean
        Try
            Dim strCommand As String
            strCommand = "SELECT tax_id, country, harmonized_no, certoforigin_no FROM dbo.tbl_certoforigin WHERE (tax_id ='" & Company_Taxno & "')" & " AND (country = '" & Conutry & "')" & " AND (harmonized_no like '" & Left(Harmonized_no, 4) & "%')" & " AND (certoforigin_no ='" & CerNo & "')" & " AND (certoforigin_date > DATEADD(year, - 2, GETDATE()))"
            '(certoforigin_date > CONVERT(DATETIME, '2007-06-01 00:00:00', 102)) 
            Dim ds As New DataSet
            ds = SqlHelper.ExecuteDataset(RollverConn, CommandType.Text, strCommand)
            If ds.Tables(0).Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If
            
        Catch ex As Exception
            Return False
        End Try
    End Function
End Module
