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

Namespace YourCompany.Modules.DFT_ChangeApproveDate
    Partial Class ViewDFT_ChangeApproveDate
        Inherits Entities.Modules.PortalModuleBase
        Implements Entities.Modules.IActionable
        Dim StrEDIConn As String = ConfigurationManager.ConnectionStrings("OriginConnection").ConnectionString

        Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            If Not Page.IsPostBack Then

            End If
        End Sub

        Function LoadDetail()
            Try
                Dim Strcommand As String
                If chkUseRef2.Checked = False Then
                    Strcommand = " SELECT A.invh_run_auto, A.reference_code2, A.company_name, C.form_name, B.site_name, A.approve_date " & _
                                 " FROM dbo.form_header_edi AS A INNER JOIN dbo.site AS B ON A.site_id = B.site_id INNER JOIN " & _
                                 " dbo.form_type AS C ON A.form_type = C.form_type " & _
                                 " WHERE (A.invh_run_auto = '" & txtSearch.Text.Trim & "') "
                    Dim ds As New DataSet
                    ds = SqlHelper.ExecuteDataset(StrEDIConn, CommandType.Text, Strcommand)
                    If ds.Tables(0).Rows.Count > 0 Then
                        PanelDetail.Visible = True
                        lblinvRun.Text = ds.Tables(0).Rows(0).Item("invh_run_auto")
                        lblReferenceCode2.Text = ds.Tables(0).Rows(0).Item("reference_code2")
                        lblCompanyname.Text = ds.Tables(0).Rows(0).Item("company_name")
                        lblForm_Type.Text = ds.Tables(0).Rows(0).Item("form_name")
                        lblSiteName.Text = ds.Tables(0).Rows(0).Item("site_name")
                        rdpApproveDate.SelectedDate = ds.Tables(0).Rows(0).Item("approve_date")
                        Return ds
                    Else
                        PanelDetail.Visible = False
                        RadAjaxManager1.ResponseScripts.Add("window.alert('ไม่พบข้อมูลคำร้อง')")
                    End If
                Else
                    Strcommand = " SELECT A.invh_run_auto, A.reference_code2, A.company_name, C.form_name, B.site_name, A.approve_date " & _
                                 " FROM dbo.form_header_edi AS A INNER JOIN dbo.site AS B ON A.site_id = B.site_id INNER JOIN " & _
                                 " dbo.form_type AS C ON A.form_type = C.form_type " & _
                                 " WHERE(A.reference_code2 = '" & txtSearch.Text.Trim & "') "
                    Dim ds As New DataSet
                    ds = SqlHelper.ExecuteDataset(StrEDIConn, CommandType.Text, Strcommand)
                    If ds.Tables(0).Rows.Count > 0 Then
                        PanelDetail.Visible = True
                        lblinvRun.Text = ds.Tables(0).Rows(0).Item("invh_run_auto")
                        lblReferenceCode2.Text = ds.Tables(0).Rows(0).Item("reference_code2")
                        lblCompanyname.Text = ds.Tables(0).Rows(0).Item("company_name")
                        lblForm_Type.Text = ds.Tables(0).Rows(0).Item("form_name")
                        lblSiteName.Text = ds.Tables(0).Rows(0).Item("site_name")
                        rdpApproveDate.SelectedDate = ds.Tables(0).Rows(0).Item("approve_date")
                        Return ds
                    Else
                        PanelDetail.Visible = False
                        RadAjaxManager1.ResponseScripts.Add("window.alert('ไม่พบข้อมูลคำร้อง')")
                    End If
                End If
            Catch ex As Exception
                Return False
                Response.Write(ex.Message)
            End Try
        End Function

        Function UpdateApprovedate()
            Try
                Dim _Time As DateTime = Now.ToString("HH:mm:ss tt")
                Dim Strcommand As String
                If chkUseRef2.Checked = False Then
                    Strcommand = " UPDATE form_header_edi " & _
                                 " SET approve_date = '" & rdpApproveDate.SelectedDate.Value & " " & _Time & "' " & _
                                 " WHERE (invh_run_auto = '" & txtSearch.Text.Trim & "') "
                    SqlHelper.ExecuteNonQuery(StrEDIConn, CommandType.Text, Strcommand)
                    Return True
                ElseIf chkUseRef2.Checked = True Then
                    Strcommand = " UPDATE form_header_edi " & _
                                 " SET approve_date = '" & rdpApproveDate.SelectedDate.Value & " " & _Time & "' " & _
                                 " WHERE (reference_code2 = '" & txtSearch.Text.Trim & "') "
                    SqlHelper.ExecuteNonQuery(StrEDIConn, CommandType.Text, Strcommand)
                    Return True
                End If

            Catch ex As Exception
                Return False
                Response.Write(ex.Message)
            End Try
        End Function

        Private Sub btnSearchForm_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearchForm.Click
            LoadDetail()
        End Sub

        Private Sub btnChangeApprove_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnChangeApprove.Click
            If UpdateApprovedate() = True Then
                RadAjaxManager1.ResponseScripts.Add("window.alert('บันทึกการแก้ไขข้อมูลเรียบร้อย')")
            Else
                RadAjaxManager1.ResponseScripts.Add("window.alert('เกิดข้อผิดพลาดไม่สามารถบันทึกข้อมูลได้')")
            End If
        End Sub
    
#Region "Optional Interfaces"
        Public ReadOnly Property ModuleActions() As Entities.Modules.Actions.ModuleActionCollection Implements Entities.Modules.IActionable.ModuleActions
            Get
                Dim Actions As New Entities.Modules.Actions.ModuleActionCollection
                Actions.Add(GetNextActionID, Localization.GetString(Entities.Modules.Actions.ModuleActionType.AddContent, LocalResourceFile), Entities.Modules.Actions.ModuleActionType.AddContent, "", "", EditUrl(), False, Security.SecurityAccessLevel.Edit, True, False)
                Return Actions
            End Get
        End Property

#End Region
    End Class

End Namespace
