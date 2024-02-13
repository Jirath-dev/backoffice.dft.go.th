Option Explicit On
Imports System
Imports System.Collections

Public Class FunctionUtility

    Public Sub New()
        ' MyBase
    End Sub

    Public Shared Function Check_For_Currency_Format(ByVal C_S) As Boolean
        Dim Var_Loop, Var_Len

        If C_S = "" Then
            Return False
        End If

        Var_Len = Len(C_S)
        C_S = LCase(C_S)

        For Var_Loop = 1 To Var_Len
            If Mid(C_S, Var_Loop, 1) < Chr(48) Or Mid(C_S, Var_Loop, 1) > Chr(57) And (Mid(C_S, Var_Loop, 1) <> ".") Then
                Return False
            End If
        Next

        Return True
    End Function

    Public Shared Function DMY2YMD(ByVal v_sDMY As String) As String
        'Sompol Edit 03/04/2009

        If IsDBNull(v_sDMY) Or (v_sDMY = "") Then
            DMY2YMD = ""
        Else
            DMY2YMD = CopyElements(3, v_sDMY, "/") 'year

            If Len(CopyElements(1, v_sDMY, "/")) = 1 Then
                DMY2YMD = DMY2YMD & "0"
            End If
            DMY2YMD = DMY2YMD & CopyElements(1, v_sDMY, "/") 'month

            If Len(CopyElements(2, v_sDMY, "/")) = 1 Then
                DMY2YMD = DMY2YMD & "0"
            End If
            DMY2YMD = DMY2YMD & CopyElements(2, v_sDMY, "/") 'day
        End If
    End Function

    Public Shared Function DAddDMY(ByVal v_iDate As Integer, ByVal v_sDMY As String) As String
        Dim m_sDMY As String
        Dim m_sD As String
        Dim m_sM As String
        If IsDBNull(v_sDMY) Or (v_sDMY = "") Then
            DAddDMY = ""
        Else
            m_sDMY = MDY2DMY(DateAdd("d", v_iDate, (DMY2MDY(v_sDMY))))
            m_sD = CopyElements(1, m_sDMY, "/")
            If Len(m_sD) = 1 Then
                m_sD = "0" & m_sD
            End If
            m_sM = CopyElements(2, m_sDMY, "/")
            If Len(m_sM) = 1 Then
                m_sM = "0" & m_sM
            End If
            DAddDMY = m_sD & "/" & m_sM & "/" & CopyElements(3, m_sDMY, "/")
        End If
    End Function

    Private Shared Function CopyElements(ByVal v_iNumber As Integer, ByVal v_sData As String, ByVal v_cMark As String) As String
        Dim m_aTemp() As String = Split(v_sData, v_cMark)
        Try
            CopyElements = m_aTemp(v_iNumber - 1)
        Catch
            CopyElements = ""
        End Try
    End Function

    Private Shared Function MDY2DMY(ByVal v_sMDY As String) As String
        If IsDBNull(v_sMDY) Or (v_sMDY = "") Then
            MDY2DMY = ""
        Else
            MDY2DMY = CopyElements(2, v_sMDY, "/") & "/" & CopyElements(1, v_sMDY, "/") & "/" & CopyElements(3, v_sMDY, "/")
        End If
    End Function

    Private Shared Function DMY2MDY(ByVal v_sDMY As String) As String
        If IsDBNull(v_sDMY) Or (v_sDMY = "") Then
            DMY2MDY = ""
        Else
            DMY2MDY = CopyElements(2, v_sDMY, "/") & "/" & CopyElements(1, v_sDMY, "/") & "/" & CopyElements(3, v_sDMY, "/")
        End If
    End Function
End Class
