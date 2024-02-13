Imports System.Net
Imports System.Security.Cryptography.X509Certificates

Public Class MyPolicy
    Implements ICertificatePolicy

    Public Function CheckValidationResult(ByVal srvPoint As System.Net.ServicePoint, _
                                          ByVal certificate As System.Security.Cryptography.X509Certificates.X509Certificate, _
                                          ByVal request As System.Net.WebRequest, _
                                          ByVal certificateProblem As Integer) As Boolean Implements System.Net.ICertificatePolicy.CheckValidationResult
        Return True
    End Function
End Class
