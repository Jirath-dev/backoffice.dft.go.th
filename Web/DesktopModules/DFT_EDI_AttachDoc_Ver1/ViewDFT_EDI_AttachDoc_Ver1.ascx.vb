Imports System.Web.UI
Imports System.Collections.Generic
Imports System.Reflection

Imports DotNetNuke
Imports DotNetNuke.Services.Exceptions
Imports DotNetNuke.Services.Localization

Imports DFT.Dotnetnuke.ClassLibrary
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports Telerik.Web.UI

Namespace NTi.Modules.DFT_EDI_AttachDoc_Ver1
    Partial Class ViewDFT_EDI_AttachDoc_Ver1
        Inherits Entities.Modules.PortalModuleBase
        Dim strEDIConn As String = ConfigurationManager.ConnectionStrings("OriginConnection").ConnectionString
        Dim myReader As SqlDataReader = Nothing
        Dim objConn As SqlConnection = Nothing

        Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            txtINVH_RUN_AUTO.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" + btnSearch.UniqueID + "').click();return false;}} else {return true}; ")

            If Not Page.IsPostBack Then
                rdpAttachDate.SelectedDate = Today
                txtINVH_RUN_AUTO.Focus()
            End If

            'Check ว่า User ที่ Login เข้ามาอยู่ Site ไหน
            Dim myRoleControllers As New DotNetNuke.Security.Roles.RoleController()
            Dim i As Integer = 0
            For i = 0 To MyBase.UserInfo.Roles.Length - 1
                Dim myRoleInfo As DotNetNuke.Security.Roles.RoleInfo = myRoleControllers.GetRoleByName(0, MyBase.UserInfo.Roles(i))

                Select Case myRoleInfo.RoleID
                    Case 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19
                        lblRoleID.Text = myRoleInfo.RoleName
                        Exit For
                End Select
            Next i
        End Sub

        Private Sub rgAttachDocList_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles rgAttachDocList.DataBound
            myReader.Close()
            objConn.Close()
        End Sub

        Private Sub rgAttachDocList_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgAttachDocList.NeedDataSource
            rgAttachDocList.DataSource = LoadAttachDoc()
        End Sub

        Function LoadAttachDoc() As SqlDataReader
            Try
                objConn = New SqlConnection(strEDIConn)
                myReader = SqlHelper.ExecuteReader(objConn, CommandType.StoredProcedure, "vi_RepAtt1_NewDS", _
                New SqlParameter("@TCat1", 1), _
                New SqlParameter("@TCat2", 1), _
                New SqlParameter("@dForm", FunctionUtility.DMY2YMD(rdpAttachDate.SelectedDate.Value)), _
                New SqlParameter("@dTo", FunctionUtility.DMY2YMD(rdpAttachDate.SelectedDate.Value)), _
                New SqlParameter("@total_day", CommonUtility.Get_Int32(10)), _
                New SqlParameter("@company_taxno", ""), _
                New SqlParameter("@site_id", CommonUtility.Get_String(lblRoleID.Text)), _
                New SqlParameter("@invh_run_auto", CommonUtility.Get_String(txtINVH_RUN_AUTO.Text.Trim())))

                Return myReader
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End Function

        Protected Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
            Try
                Dim ds As New DataSet
                ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, "vi_approve_repAtt_NewDS_V1", _
                New SqlParameter("@TCat", CommonUtility.Get_Int32(rblTypeRepAtt.SelectedValue)), _
                New SqlParameter("@INVH_RUN_AUTO", CommonUtility.Get_StringValue(txtINVH_RUN_AUTO.Text)), _
                New SqlParameter("@USER_ID", CommonUtility.Get_StringValue(UserInfo.Username)), _
                New SqlParameter("@site_id", lblRoleID.Text))

                If ds.Tables(0).Rows.Count > 0 Then
                    Select Case CommonUtility.Get_Int32(ds.Tables(0).Rows(0).Item("retSTATUS"))
                        Case 0
                            rgAttachDocList.DataSource = LoadAttachDoc()
                            rgAttachDocList.DataBind()
                        Case -1, -2, -3, -4, -5
                            Me.RadAjaxManager1.ResponseScripts.Add("window.alert('" & CommonUtility.Get_StringValue(ds.Tables(0).Rows(0).Item("retMessage")) & "');")
                    End Select
                End If

                txtINVH_RUN_AUTO.Text = ""
                txtINVH_RUN_AUTO.Focus()
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End Sub

        Function GetInvoice_No(ByVal _invh_run_auto As String) As String
            Try
                Dim isFirst As Boolean = True
                Dim strInvoice As String = ""
                Dim strCommand As String
                strCommand = "Select * From form_header_edi Where invh_run_auto = '" & _invh_run_auto & "'"
                Dim dr As SqlDataReader
                dr = SqlHelper.ExecuteReader(strEDIConn, CommandType.Text, strCommand)
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

        Protected Sub btnSearch2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch2.Click
            rgAttachDocList.DataSource = LoadAttachDoc()
            rgAttachDocList.DataBind()
        End Sub

        Protected Sub btnReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReport.Click
            Me.RadAjaxManager1.ResponseScripts.Add("window.open('http://" & DotNetNuke.Common.GetDomainName(Request) & "/DesktopModules/DFT_EDI_AttachDoc_Ver1/frmAttachDoc_Ver1.aspx" & _
            "?FROM_DATE=" & FunctionUtility.DMY2YMD(rdpAttachDate.SelectedDate.Value) & _
            "&TO_DATE=" & FunctionUtility.DMY2YMD(rdpAttachDate.SelectedDate.Value) & _
            "&SITE_ID=" & CommonUtility.Get_StringValue(lblRoleID.Text) & _
            "',null,'height=600, width=800,status=no, resizable=yes,scrollbars=yes, toolbar=no,location=no,menubar=no,center=yes');")
        End Sub
    End Class

End Namespace
