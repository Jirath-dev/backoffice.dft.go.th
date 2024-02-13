Imports System.Web.UI
Imports System.Collections.Generic
Imports System.Reflection

Imports DotNetNuke
Imports DotNetNuke.Services.Exceptions
Imports DotNetNuke.Services.Localization

Imports Microsoft.ApplicationBlocks.Data
Imports DFT.Dotnetnuke.ClassLibrary
Imports System.Data.SqlClient
Imports Telerik.Web.UI

Namespace NTi.Modules.DFT_EDI_F04_Report_05
    Partial Class ViewDFT_EDI_F04_Report_05
        Inherits Entities.Modules.PortalModuleBase
        Dim strConn As String = ConfigurationManager.ConnectionStrings("OriginConnection").ConnectionString
        Dim objConn As SqlConnection = Nothing
        Dim myReader As SqlDataReader

        Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            If Not Page.IsPostBack Then
                'rdpFromDate.SelectedDate = Today
                'rdpToDate.SelectedDate = Today
            End If

            'Check ว่า User ที่ Login เข้ามาอยู่ Site ไหน
            Dim myRoleControllers As New DotNetNuke.Security.Roles.RoleController()
            Dim i As Integer = 0
            For i = 0 To MyBase.UserInfo.Roles.Length - 1
                Dim myRoleInfo As DotNetNuke.Security.Roles.RoleInfo = myRoleControllers.GetRoleByName(0, MyBase.UserInfo.Roles(i))
                'by rut 7-09-2555 ใช้วันที่ 22-01-2556 ใช้แทนด้านล่าง ต้องเพิ่มใน Table "RoleList" แทน
                If Get_ListRoles(myRoleInfo.RoleID, myRoleInfo.RoleName) <> "" Then
                    Exit For
                End If
                'Select Case myRoleInfo.RoleID
                '    Case 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 26, 28
                '        lblRoleID.Text = myRoleInfo.RoleName
                '        Session("ssRoleName") = lblRoleID.Text
                '        Exit For
                'End Select
            Next i
        End Sub
#Region "by rut"
        Function Get_ListRoles(ByVal ByRoleID As Integer, ByVal ByRoleName As String) As String
            Dim DSRoles As SqlDataReader
            Dim strEDIConn As String = ConfigurationManager.ConnectionStrings("OriginConnection").ConnectionString

            DSRoles = SqlHelper.ExecuteReader(strEDIConn, CommandType.StoredProcedure, "RoleList_GetList", _
                            New SqlParameter("@ListRoleNameCase", ByRoleID))

            Dim strListRole As String = ""

            If DSRoles.HasRows Then
                strListRole = ByRoleName
                lblRoleID.Text = strListRole
                Session("ssRoleName") = lblRoleID.Text
            End If

            Return strListRole
        End Function
#End Region
        Function LoadReport() As SqlDataReader
            Try
                objConn = New SqlConnection(strConn)
                myReader = SqlHelper.ExecuteReader(objConn, CommandType.StoredProcedure, "Re_EditDocAtt_NewDS", _
                New SqlParameter("@TCat", 5), _
                New SqlParameter("@dForm", FunctionUtility.DMY2YMD(Today)), _
                New SqlParameter("@dTo", FunctionUtility.DMY2YMD(Today)), _
                New SqlParameter("@invh_run_auto", ""), _
                New SqlParameter("@site_id", lblRoleID.Text))

                Return myReader
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End Function

        Private Sub rgAllRequestList_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles rgAllRequestList.DataBound
            myReader.Close()
            objConn.Close()

            If rgAllRequestList.MasterTableView.Items.Count > 0 Then
                btnPrint.Visible = True
            Else
                btnPrint.Visible = False
            End If
        End Sub

        Private Sub rgAllRequestList_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgAllRequestList.ItemDataBound
            If Not (e.Item.FindControl("lblIndex") Is Nothing) Then
                Dim lbl As Label = e.Item.FindControl("lblIndex")
                lbl.Text = (rgAllRequestList.MasterTableView.CurrentPageIndex * rgAllRequestList.MasterTableView.PageSize) + (e.Item.ItemIndex + 1)
            End If
        End Sub

        Private Sub rgAllRequestList_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgAllRequestList.NeedDataSource
            rgAllRequestList.DataSource = LoadReport()
        End Sub

        'Protected Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        '    rgAllRequestList.DataSource = LoadReport()
        '    rgAllRequestList.DataBind()

        '    If rgAllRequestList.MasterTableView.Items.Count > 0 Then
        '        btnPrint.Visible = True
        '    Else
        '        btnPrint.Visible = False
        '    End If
        'End Sub

        Function GetInvoice_No(ByVal _invh_run_auto As String) As String
            Try
                Dim isFirst As Boolean = True
                Dim strInvoice As String = ""
                Dim strCommand As String
                strCommand = "Select * From form_header_edi Where invh_run_auto = '" & _invh_run_auto & "'"
                Dim dr As SqlDataReader
                dr = SqlHelper.ExecuteReader(strConn, CommandType.Text, strCommand)
                If dr.HasRows() Then
                    dr.Read()

                    If Not (dr.Item("invoice_no1").Equals(System.DBNull.Value) Or CommonUtility.Get_StringValue(dr.Item("invoice_no1")) = "") Then
                        If isFirst Then
                            strInvoice = CommonUtility.Get_StringValue(dr.Item("invoice_no1"))
                            isFirst = False
                        Else
                            strInvoice &= "," & "<br />" & CommonUtility.Get_StringValue(dr.Item("invoice_no1"))
                        End If
                    End If

                    If Not (dr.Item("invoice_no2").Equals(System.DBNull.Value) Or CommonUtility.Get_StringValue(dr.Item("invoice_no2")) = "") Then
                        If isFirst Then
                            strInvoice = CommonUtility.Get_StringValue(dr.Item("invoice_no2"))
                            isFirst = False
                        Else
                            strInvoice &= "," & "<br />" & CommonUtility.Get_StringValue(dr.Item("invoice_no2"))
                        End If
                    End If

                    If Not (dr.Item("invoice_no3").Equals(System.DBNull.Value) Or CommonUtility.Get_StringValue(dr.Item("invoice_no3")) = "") Then
                        If isFirst Then
                            strInvoice = CommonUtility.Get_StringValue(dr.Item("invoice_no3"))
                            isFirst = False
                        Else
                            strInvoice &= "," & "<br />" & CommonUtility.Get_StringValue(dr.Item("invoice_no3"))
                        End If
                    End If

                    If Not (dr.Item("invoice_no4").Equals(System.DBNull.Value) Or CommonUtility.Get_StringValue(dr.Item("invoice_no4")) = "") Then
                        If isFirst Then
                            strInvoice = CommonUtility.Get_StringValue(dr.Item("invoice_no4"))
                            isFirst = False
                        Else
                            strInvoice &= "," & "<br />" & CommonUtility.Get_StringValue(dr.Item("invoice_no4"))
                        End If
                    End If

                    If Not (dr.Item("invoice_no5").Equals(System.DBNull.Value) Or CommonUtility.Get_StringValue(dr.Item("invoice_no5")) = "") Then
                        If isFirst Then
                            strInvoice = CommonUtility.Get_StringValue(dr.Item("invoice_no5"))
                            isFirst = False
                        Else
                            strInvoice &= "," & "<br />" & CommonUtility.Get_StringValue(dr.Item("invoice_no5"))
                        End If
                    End If

                    Return strInvoice
                Else
                    Return ""
                End If
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End Function

        Private Sub btnPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrint.Click
            Response.Write("<script language ='javascript'> window.open('http://" & DotNetNuke.Common.GetDomainName(Request) & "/DesktopModules/DFT_EDI_F04_Report_05/frmF04_Report_05.aspx',null,'height=768, width=1024,status=yes, resizable= yes,scrollbars=no, toolbar=no,location=no,menubar=no'); </script>")
        End Sub
    End Class

End Namespace
