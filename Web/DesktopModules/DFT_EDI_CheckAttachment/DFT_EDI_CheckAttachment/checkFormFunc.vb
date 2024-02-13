Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data

Module checkFormFunc
    Dim strConn As String = ConfigurationManager.ConnectionStrings("OriginConnection").ConnectionString
    Dim myReader As SqlDataReader = Nothing
    Dim objConn As SqlConnection = Nothing

    Public Function GetResultCheckedInfo(ByVal RefNo As String) As String
        Dim strResult As String = ""
        Try
            myReader = SqlHelper.ExecuteReader(strConn, CommandType.StoredProcedure, "sp_edi_getFormCheckedInfo_DS2", New SqlParameter("@REF_No", RefNo))
            myReader.Read()
            If myReader.HasRows Then
                strResult = "สถานะ: " & myReader("Check_Result") & _
                            "<br/>วันที่ส่งคำขอ: " & myReader("Sent_Date") & "&nbsp;&nbsp;วันที่ตรวจสอบ: " & myReader("CheckDoc_Date") & " &nbsp;&nbsp;โดย: " & myReader("CheckDoc_By").ToString() & _
                            "<br/>วันที่พิมพ์ฟอร์ม: " & myReader("printFormDate") & "&nbsp;&nbsp;โดย: " & myReader("UserPrintForm")
            End If
            myReader.Close()
        Catch ex As Exception
            strResult = "" & ex.Message
        End Try
        Return strResult
    End Function
End Module
