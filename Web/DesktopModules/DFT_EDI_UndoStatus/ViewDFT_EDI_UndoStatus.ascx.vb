Imports System.Web.UI
Imports System.Collections.Generic
Imports System.Reflection

Imports DotNetNuke
Imports DotNetNuke.Services.Exceptions
Imports DotNetNuke.Services.Localization

Imports System.Data.SqlClient
Imports DFT.Dotnetnuke.ClassLibrary
Imports Microsoft.ApplicationBlocks.Data
Imports Telerik.Web.UI

Namespace NTi.Modules.DFT_EDI_UndoStatus
    Partial Class ViewDFT_EDI_UndoStatus
        Inherits Entities.Modules.PortalModuleBase
        Dim strEDIConn As String = ConfigurationManager.ConnectionStrings("OriginConnection").ConnectionString

        Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            txtReferenceCode2.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" + btnSearch.UniqueID + "').click();return false;}} else {return true}; ")

            If Not Page.IsPostBack Then
                tblHeader.Visible = False
                txtReferenceCode2.Focus()
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
        Protected Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
            Call LoadHeader(txtReferenceCode2.Text.Trim())
        End Sub

        Private Sub LoadHeader(ByVal ReferenceCode2 As String)
            Try
                Dim dr As SqlDataReader
                Dim strCommand As String
                strCommand = "select * from vEdiForm_For_Undo where REPLACE(REFERENCE_CODE2,'_C','')='" & ReferenceCode2 & "'"
                'strCommand = "select * from vEdiForm_For_Undo where substring(reference_code2,1,14)='" & ReferenceCode2 & "'"
                dr = SqlHelper.ExecuteReader(strEDIConn, CommandType.Text, strCommand)
                If dr.HasRows Then
                    tblHeader.Visible = True
                    dr.Read()

                    lblInv_Auto_Run.Text = CommonUtility.Get_StringValue(dr.Item("invh_run_auto"))
                    lblReferenceCode2.Text = CommonUtility.Get_StringValue(dr.Item("reference_code2"))
                    lblForm_Name.Text = CommonUtility.Get_StringValue(dr.Item("form_name"))
                    lblCompany_Name.Text = CommonUtility.Get_StringValue(dr.Item("company_name"))
                    lblEdi_Status.Text = CommonUtility.Get_StringValue(EDI_Status_Name(dr.Item("edi_status_id")))

                    dr.Close()

                    txtReferenceCode2.Enabled = False
                    btnSearch.Visible = False
                Else
                    tblHeader.Visible = False
                    Me.RadAjaxManager1.ResponseScripts.Add("window.alert('" & "ไม่พบหมายเลขหนังสือรับรองเลขที่  " & txtReferenceCode2.Text.Trim() & "');")
                    txtReferenceCode2.Text = ""
                    txtReferenceCode2.Focus()
                End If
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End Sub

        Function EDI_Status_Name(ByVal edi_status_id As String) As String
            Try
                Dim _return As String
                Dim strCommand As String
                Dim dr As SqlDataReader
                strCommand = "SELECT * FROM edi_status WHERE (edi_status_id = '" & edi_status_id & "')"
                dr = SqlHelper.ExecuteReader(strEDIConn, CommandType.Text, strCommand)
                If dr.HasRows Then
                    dr.Read()
                    _return = CommonUtility.Get_StringValue(dr.Item("description"))
                    dr.Close()

                    Return _return
                End If
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End Function

        Protected Sub btnUndoStatus_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUndoStatus.Click
            Try
                Dim ds As New DataSet
                ds = SqlHelper.ExecuteDataset(strEDIConn, CommandType.StoredProcedure, "sp_form_edi_undoStatus_NewDS", New SqlParameter("@INVH_RUN_AUTO", lblInv_Auto_Run.Text.Trim()))
                If CommonUtility.Get_Int32(ds.Tables(0).Rows(0).Item("retSTATUS")) = 0 Then
                    tblHeader.Visible = False
                    btnSearch.Visible = True
                    txtReferenceCode2.Enabled = True
                    txtReferenceCode2.Text = ""
                    txtReferenceCode2.Focus()
                Else
                    Me.RadAjaxManager1.ResponseScripts.Add("window.alert('เกิดความผิดพลาดไม่สามารถทำการย้อนสถานะได้ !!!');")
                End If
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End Sub
    End Class

End Namespace
