Imports System.IO
Public Class Sign_GovClass
    Public Shared Function Upload_image(ByVal GetPath As String, ByVal FolderName As String) As String
        Dim dir As DirectoryInfo
        Try
            dir = Directory.CreateDirectory(Path.Combine(GetPath, FolderName))
            Return dir.FullName
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
End Class
