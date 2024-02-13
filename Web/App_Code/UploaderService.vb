Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols

Imports System
Imports System.Data
Imports System.Collections
Imports System.ComponentModel
Imports System.IO

Imports Microsoft.ApplicationBlocks.Data
Imports System.Configuration

<WebService(Namespace:="http://tempuri.org/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Class UploaderService
     Inherits System.Web.Services.WebService

    <WebMethod()> _
    Public Function UploadFile(ByVal f As Byte(), ByVal fileName As String, ByVal Company_Taxno As String) As String
        ' the byte array argument contains the content of the file
        ' the string argument contains the name and extension
        ' of the file passed in the byte array
        Try
            ' instance a memory stream and pass the
            ' byte array to its constructor
            Dim ms As New MemoryStream(f)

            ' instance a filestream pointing to the
            ' storage folder, use the original file name
            ' to name the resulting file
            Dim fs As New FileStream(System.Web.Hosting.HostingEnvironment.MapPath("~/XMLFile/") + Company_Taxno & "-" & fileName, FileMode.Create)

            ' write the memory stream containing the original 
            ' file as a byte array to the filestream
            ms.WriteTo(fs)

            ' clean up
            ms.Close()
            fs.Close()
            fs.Dispose()

            ' return OK if we made it this far 
            Return "OK"
        Catch ex As Exception
            ' return the error message if the operation fails 
            Return ex.Message.ToString()
        End Try
    End Function

    <WebMethod()> _
    Public Function LoadHeader(ByVal Comapny_TaxNo As String) As DataSet
        Try
            Dim strConn As String = ConfigurationManager.ConnectionStrings("EDIConnection").ConnectionString
            Dim strCommand As String
            strCommand = "SELECT dbo.form_header_edi.* " & _
                         "FROM dbo.tbl_XMLFile_Log INNER JOIN " & _
                         "dbo.form_header_edi ON dbo.tbl_XMLFile_Log.Invh_Run_Auto = dbo.form_header_edi.invh_run_auto " & _
                         "WHERE (dbo.tbl_XMLFile_Log.Company_TaxNo = N'" & Comapny_TaxNo & "')"
            Dim ds As New DataSet
            ds = SqlHelper.ExecuteDataset(strConn, CommandType.Text, strCommand)
            Return ds
        Catch ex As Exception

        End Try
    End Function
End Class
