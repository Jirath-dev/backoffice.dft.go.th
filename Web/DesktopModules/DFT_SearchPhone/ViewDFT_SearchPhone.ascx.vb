
Imports System.Web.UI
Imports System.Collections.Generic
Imports System.Reflection

Imports DotNetNuke
Imports DotNetNuke.Services.Exceptions
Imports DotNetNuke.Services.Localization
Imports System.Data
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports Telerik.Web.UI

Namespace DFT_SearchPhone

    Partial Class ViewDFT_SearchPhone
        Inherits Entities.Modules.PortalModuleBase
        Dim StrconnEDI As String = ConfigurationManager.ConnectionStrings("EDIConnection").ConnectionString
        Dim StrconnRegBack As String = ConfigurationManager.ConnectionStrings("RegisBackConnection").ConnectionString
        Dim StrconnReg As String = ConfigurationManager.ConnectionStrings("RegisConnection").ConnectionString
        'Dim PersonID As String

        Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            If Not Page.IsPostBack Then

            End If
        End Sub

        Sub SearchPhoneNumber()
            Try
                Dim Strcommand As String
                Strcommand = " SELECT card_id, AuthPersonID FROM rfcard WHERE (card_id = @CardID) "
                Dim npm As New SqlClient.SqlParameter("@CardID", txtCardID.Text.Trim)
                Dim ds As New DataSet
                ds = SqlHelper.ExecuteDataset(StrconnEDI, CommandType.Text, Strcommand, npm)
                If ds.Tables(0).Rows.Count > 0 Then
                    LoadRegBack(ds.Tables(0).Rows(0).Item("AuthPersonID"))
                    LoadRegFront(ds.Tables(0).Rows(0).Item("AuthPersonID"))
                Else
                    RadAjaxManager1.ResponseScripts.Add("window.alert('ไม่พบข้อมูลผู้ประกอบการ')")
                End If

            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End Sub

        Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
            If txtCardID.Text.Trim <> "" Then
                SearchPhoneNumber()
            Else
                txtCardID.Focus()
                RadAjaxManager1.ResponseScripts.Add("window.alert('กรุณาป้อนเลขบัตร')")
            End If

        End Sub

        Sub LoadRegBack(ByVal PersonID As String)
            Try
                Dim Strcomm As String
                Strcomm = " SELECT FirstName_Th + N'  ' + LastName_Th AS Name, PhoneNo , FaxNo, Person_CardNo " & _
                          " FROM T_PersonProfile " & _
                          " WHERE (Person_CardNo = '" & PersonID & "') "
                Dim ds As New DataSet
                ds = SqlHelper.ExecuteDataset(StrconnRegBack, CommandType.Text, Strcomm)
                If ds.Tables(0).Rows.Count > 0 Then
                    lblName_D.Text = ds.Tables(0).Rows(0).Item("Name")

                    If DBNull.Value.Equals(ds.Tables(0).Rows(0).Item("PhoneNo")) = True Then
                        If ds.Tables(0).Rows(0).Item("PhoneNo") = "" Or ds.Tables(0).Rows(0).Item("PhoneNo") Is Nothing Then
                            lblPhoneNumber_Back.Text = ds.Tables(0).Rows(0).Item("FaxNo") & " , "
                        End If
                    ElseIf DBNull.Value.Equals(ds.Tables(0).Rows(0).Item("FaxNo")) = True Then
                        If ds.Tables(0).Rows(0).Item("FaxNo") = "" Or ds.Tables(0).Rows(0).Item("FaxNo") Is Nothing Then
                            lblPhoneNumber_Back.Text = ds.Tables(0).Rows(0).Item("PhoneNo") & " , "
                        End If
                    Else
                        lblPhoneNumber_Back.Text = ds.Tables(0).Rows(0).Item("PhoneNo") & " , " & ds.Tables(0).Rows(0).Item("FaxNo") & " , "
                    End If
                Else
                    lblName_D.Text = ""
                    lblPhoneNumber_Back.Text = ""
                End If

            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End Sub

        Sub LoadRegFront(ByVal PersonID As String)
            Try
                Dim Strcomm As String
                Strcomm = " SELECT Person_CardNo, MobileNo, PhoneNo, FaxNo " & _
                          " FROM R_PersonProfile " & _
                          " WHERE (Person_CardNo = '" & PersonID & "')"
                Dim ds As New DataSet
                ds = SqlHelper.ExecuteDataset(StrconnReg, CommandType.Text, Strcomm)
                If ds.Tables(0).Rows.Count > 0 Then
                    If DBNull.Value.Equals(ds.Tables(0).Rows(0).Item("MobileNo")) = True Then
                        If ds.Tables(0).Rows(0).Item("MobileNo") = "" Or ds.Tables(0).Rows(0).Item("MobileNo") Is Nothing Then
                            lblPhoneNumber_Front.Text = " , " & ds.Tables(0).Rows(0).Item("PhoneNo") & " , " & ds.Tables(0).Rows(0).Item("FaxNo")
                        End If
                    ElseIf DBNull.Value.Equals(ds.Tables(0).Rows(0).Item("PhoneNo")) = True Then
                        If ds.Tables(0).Rows(0).Item("PhoneNo") = "" Or ds.Tables(0).Rows(0).Item("PhoneNo") Is Nothing Then
                            lblPhoneNumber_Front.Text = " , " & ds.Tables(0).Rows(0).Item("MobileNo") & " , " & ds.Tables(0).Rows(0).Item("FaxNo")
                        End If
                    ElseIf DBNull.Value.Equals(ds.Tables(0).Rows(0).Item("FaxNo")) = True Then
                        If ds.Tables(0).Rows(0).Item("FaxNo") = "" Or ds.Tables(0).Rows(0).Item("FaxNo") Is Nothing Then
                            lblPhoneNumber_Front.Text = " , " & ds.Tables(0).Rows(0).Item("MobileNo") & " , " & ds.Tables(0).Rows(0).Item("PhoneNo")
                        End If
                    Else
                        lblPhoneNumber_Front.Text = " , " & ds.Tables(0).Rows(0).Item("MobileNo") & " , " & ds.Tables(0).Rows(0).Item("PhoneNo") & " , " & ds.Tables(0).Rows(0).Item("FaxNo")
                    End If
                Else
                    lblPhoneNumber_Front.Text = ""
                End If
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End Sub
    End Class

End Namespace
