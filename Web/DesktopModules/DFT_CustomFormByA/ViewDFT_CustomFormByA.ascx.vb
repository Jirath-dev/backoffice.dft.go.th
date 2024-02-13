
Imports System.Web.UI
Imports System.Collections.Generic
Imports System.Reflection

Imports DotNetNuke
Imports DotNetNuke.Services.Exceptions
Imports DotNetNuke.Services.Localization
Imports System.Data
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data

Namespace YourCompany.Modules.DFT_CustomFormByA

    Partial Class ViewDFT_CustomFormByA
        Inherits Entities.Modules.PortalModuleBase
        Implements Entities.Modules.IActionable

        Dim Strconn As String = ConfigurationManager.ConnectionStrings("OriginConnection").ConnectionString
#Region "Optional Interfaces"

        ''' -----------------------------------------------------------------------------
        ''' <summary>
        ''' Registers the module actions required for interfacing with the portal framework
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' </history>
        ''' -----------------------------------------------------------------------------
        Public ReadOnly Property ModuleActions() As Entities.Modules.Actions.ModuleActionCollection Implements Entities.Modules.IActionable.ModuleActions
            Get
                Dim Actions As New Entities.Modules.Actions.ModuleActionCollection
                Actions.Add(GetNextActionID, Localization.GetString(Entities.Modules.Actions.ModuleActionType.AddContent, LocalResourceFile), Entities.Modules.Actions.ModuleActionType.AddContent, "", "", EditUrl(), False, Security.SecurityAccessLevel.Edit, True, False)
                Return Actions
            End Get
        End Property

#End Region
        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            If Not Page.IsPostBack Then
                lblMessage.Visible = False
            End If
            txtInvRunAuto.Focus()
        End Sub

        Public Sub SearchInvRunAuto()
            Try
                If txtInvRunAuto.Text.Trim = "" Then
                    RadAjaxManager1.Alert("กรุณาป้อนเลขที่อ้างอิง!!!")
                    txtInvRunAuto.Focus()
                    Exit Sub
                Else
                    lblMessage.Visible = False
                End If

                Dim Strcommand As String = ""
                Strcommand = " SELECT invh_run_auto, edi_status_id, company_name, destination_country, form_type, reference_code2, approve_date " & _
                             " FROM form_header_edi " & _
                             " WHERE (invh_run_auto = '" & txtInvRunAuto.Text.Trim & "') AND (NOT (edi_status_id IN ('N', 'C'))) AND " & _
                             " (form_type IN ('FORM1', 'FORM1_1', 'FORM1_2', 'FORM1_3', 'FORM1_4', 'FORM1_5', 'FORMRussia'))"
                Dim ds As New DataSet
                ds = SqlHelper.ExecuteDataset(Strconn, CommandType.Text, Strcommand)
                If ds.Tables(0).Rows.Count > 0 Then
                    lblCompanyName.Text = ds.Tables(0).Rows(0).Item("company_name")
                    lblOldCountry.Text = CheckOldCountry(ds.Tables(0).Rows(0).Item("destination_country"), ds.Tables(0).Rows(0).Item("form_type"))
                    LoadCountry(ds.Tables(0).Rows(0).Item("form_type"))
                    LoadRunningMAXRefCode2(ds.Tables(0).Rows(0).Item("form_type"))

                    If DBNull.Value.Equals(ds.Tables(0).Rows(0).Item("reference_code2")) Then
                        lblReferenceNo.Text = ""
                    Else
                        lblReferenceNo.Text = ds.Tables(0).Rows(0).Item("reference_code2")
                    End If

                    If txtRunningRefCode2.Text.Length = 1 Then
                        txtRefCode2.Text = CheckRefByForm(ds.Tables(0).Rows(0).Item("form_type")) & "000000" & txtRunningRefCode2.Text + 1
                    ElseIf txtRunningRefCode2.Text.Length = 2 Then
                        txtRefCode2.Text = CheckRefByForm(ds.Tables(0).Rows(0).Item("form_type")) & "00000" & txtRunningRefCode2.Text + 1
                    ElseIf txtRunningRefCode2.Text.Length = 3 Then
                        txtRefCode2.Text = CheckRefByForm(ds.Tables(0).Rows(0).Item("form_type")) & "0000" & txtRunningRefCode2.Text + 1
                    ElseIf txtRunningRefCode2.Text.Length = 4 Then
                        txtRefCode2.Text = CheckRefByForm(ds.Tables(0).Rows(0).Item("form_type")) & "000" & txtRunningRefCode2.Text + 1
                    ElseIf txtRunningRefCode2.Text.Length = 5 Then
                        txtRefCode2.Text = CheckRefByForm(ds.Tables(0).Rows(0).Item("form_type")) & "00" & txtRunningRefCode2.Text + 1
                    ElseIf txtRunningRefCode2.Text.Length = 6 Then
                        txtRefCode2.Text = CheckRefByForm(ds.Tables(0).Rows(0).Item("form_type")) & "0" & txtRunningRefCode2.Text + 1
                    ElseIf txtRunningRefCode2.Text.Length = 7 Then
                        txtRefCode2.Text = CheckRefByForm(ds.Tables(0).Rows(0).Item("form_type")) & txtRunningRefCode2.Text + 1
                    End If

                    If ds.Tables(0).Rows(0).Item("edi_status_id") = "A" Then
                        btnSaveCountry.Enabled = False
                        btnSaveRefCode2.Enabled = False
                        btnSaveApproveDate.Enabled = True
                    Else
                        btnSaveCountry.Enabled = True
                        btnSaveRefCode2.Enabled = True
                        btnSaveApproveDate.Enabled = False
                    End If

                Else
                    lblCompanyName.Text = ""
                    lblOldCountry.Text = ""
                    rcbChangeCountry.Items.Clear()
                    txtRefCode2.Text = ""
                    txtRunningRefCode2.Text = ""
                    RadAjaxManager1.ResponseScripts.Add("window.alert('ไม่พบข้อมูลเลขที่อ้างอิง')")
                End If

            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End Sub

        Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
            SearchInvRunAuto
        End Sub

        Function CheckRefByForm(ByVal FormType As String)
            Try
                Dim RefCode2 As String = ""
                Select Case FormType
                    Case "FORM1"
                        RefCode2 = "A2014-"
                    Case "FORM1_1"
                        RefCode2 = "A2014-"
                    Case "FORM1_2"
                        RefCode2 = "A2014-"
                    Case "FORMRussia"
                        RefCode2 = "A2014-"
                    Case "FORM1_3"
                        RefCode2 = "E2014-"
                    Case "FORM1_4"
                        RefCode2 = "L2014-"
                    Case "FORM1_5"
                        RefCode2 = "E2014-"
                End Select
                Return RefCode2
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End Function

        Function CheckOldCountry(ByVal CountryCode As String, ByVal FormType As String)
            Try
                Dim CountryName As String = ""
                Dim npm(1) As SqlClient.SqlParameter
                npm(0) = New SqlClient.SqlParameter("@CountryCode", CountryCode)
                npm(1) = New SqlClient.SqlParameter("@FormType", FormType)

                Dim Strcommand As String = ""
                Strcommand = " SELECT country_name FROM country WHERE (country_code = @CountryCode) AND (form_type = @FormType)"

                Dim ds As New DataSet
                ds = SqlHelper.ExecuteDataset(Strconn, CommandType.Text, Strcommand, npm)
                If ds.Tables(0).Rows.Count > 0 Then
                    CountryName = ds.Tables(0).Rows(0).Item("country_name")
                Else
                    CountryName = ""
                End If

                Return CountryName
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End Function

        Sub LoadCountry(ByVal FormType As String)
            Try
                Dim Strcommand As String = ""
                Strcommand = " SELECT country_code, form_type, country_name FROM dbo.country" & _
                             " WHERE (form_type = '" & FormType & "')" & _
                             " ORDER BY country_name"
                Dim ds As New DataSet
                ds = SqlHelper.ExecuteDataset(Strconn, CommandType.Text, Strcommand)
                If ds.Tables(0).Rows.Count > 0 Then
                    rcbChangeCountry.DataSource = ds
                    rcbChangeCountry.DataValueField = "country_code"
                    rcbChangeCountry.DataTextField = "country_name"
                    rcbChangeCountry.DataBind()
                End If

            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End Sub

        Sub UpdateChangeCountry()
            Try
                Dim Strcommand As String = ""
                Strcommand = " UPDATE form_header_edi SET destination_country = @CountryCode" & _
                             " WHERE (invh_run_auto = @invh_run_auto)"

                Dim npm(2) As SqlClient.SqlParameter
                npm(0) = New SqlClient.SqlParameter("@CountryCode", rcbChangeCountry.SelectedValue.ToString)
                npm(1) = New SqlClient.SqlParameter("@invh_run_auto", txtInvRunAuto.Text.Trim)

                SqlHelper.ExecuteNonQuery(Strconn, CommandType.Text, Strcommand, npm)
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End Sub

        Sub UpdateRefCode2()
            Try
                Dim Strcommand As String = ""
                Strcommand = " UPDATE form_header_edi SET reference_code2 = @RefCode2, site_id = @site_id " & _
                             " WHERE (invh_run_auto = @invh_run_auto) "

                Dim npm(2) As SqlClient.SqlParameter
                npm(0) = New SqlClient.SqlParameter("@RefCode2", txtRefCode2.Text)
                npm(1) = New SqlClient.SqlParameter("@invh_run_auto", txtInvRunAuto.Text.Trim)
                npm(2) = New SqlClient.SqlParameter("@site_id", "ST-001")

                SqlHelper.ExecuteNonQuery(Strconn, CommandType.Text, Strcommand, npm)
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End Sub

        Sub UpdateApproveDate()
            Try
                Dim Strcommand As String = ""
                Strcommand = " UPDATE form_header_edi SET approve_date = @approve_date WHERE (invh_run_auto = @invh_run_auto)"

                Dim npm(1) As SqlClient.SqlParameter
                npm(0) = New SqlClient.SqlParameter("@approve_date", rdpApproveDate.SelectedDate.Value)
                npm(1) = New SqlClient.SqlParameter("@invh_run_auto", txtInvRunAuto.Text.Trim)

                SqlHelper.ExecuteNonQuery(Strconn, CommandType.Text, Strcommand, npm)
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End Sub

        Private Sub btnSaveCountry_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSaveCountry.Click
            UpdateChangeCountry()
            lblMessage.Visible = True
            lblMessage.Text = "บันทึกการเปลี่ยนแปลงประเทศเรียบร้อย"
            lblMessage.ForeColor = Drawing.Color.Green
            'SearchInvRunAuto()
        End Sub

        Private Sub btnSaveRefCode2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSaveRefCode2.Click
            If CheckRefCode2BeforeUpdate() = True Then
                UpdateRefCode2()
                UpdateRunningMAXRefCode2()
                lblMessage.Visible = True
                lblMessage.Text = "บันทึกเลขที่หนังสือรับรองเรียบร้อย"
                lblMessage.ForeColor = Drawing.Color.Green
                'SearchInvRunAuto()
            Else
                lblMessage.Visible = True
                lblMessage.Text = "ไม่สามารถบันทึกเลขที่หนังสือรับรองได้เนื่องจากมีข้อมูลแล้ว"
                lblMessage.ForeColor = Drawing.Color.Red
            End If
        End Sub

        Private Sub btnSaveApproveDate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSaveApproveDate.Click
            UpdateApproveDate()
            lblMessage.Visible = True
            lblMessage.Text = "บันทึกวันที่อนุมัติเรียบร้อย"
            lblMessage.ForeColor = Drawing.Color.Green
            'SearchInvRunAuto()
        End Sub

        Sub LoadRunningMAXRefCode2(ByVal FormType As String)
            Try
                Dim Strcommand As String = ""
                Strcommand = " SELECT running_max_number FROM book_edi_running_number" & _
                             " WHERE (running_year = '2014') AND (form_type = '" & FormType & "' )"
                Dim ds As New DataSet
                ds = SqlHelper.ExecuteDataset(Strconn, CommandType.Text, Strcommand)
                If ds.Tables(0).Rows.Count > 0 Then
                    txtRunningRefCode2.Text = ds.Tables(0).Rows(0).Item("running_max_number")
                End If
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End Sub

        Sub UpdateRunningMAXRefCode2()
            Try
                Dim Strcommand_FormType As String = ""
                Strcommand_FormType = " SELECT form_type FROM dbo.form_header_edi" & _
                             " WHERE (invh_run_auto = '" & txtInvRunAuto.Text.Trim & "')"
                Dim ds As New DataSet
                ds = SqlHelper.ExecuteDataset(Strconn, CommandType.Text, Strcommand_FormType)
                If ds.Tables(0).Rows.Count > 0 Then
                    Dim Strcommand As String = ""
                    Dim npm(1) As SqlClient.SqlParameter
                    npm(0) = New SqlClient.SqlParameter("@RunningMax", txtRunningRefCode2.Text + 1)
                    npm(1) = New SqlClient.SqlParameter("@FormType", ds.Tables(0).Rows(0).Item("form_type"))

                    Strcommand = " UPDATE book_edi_running_number SET running_max_number = @RunningMax" & _
                                 " WHERE (running_year = '2014') AND (form_type = @FormType)"
                    SqlHelper.ExecuteNonQuery(Strconn, CommandType.Text, Strcommand, npm)
                Else
                    RadAjaxManager1.Alert("ไม่พบข้อมูลกรุณาตรวจสอบ!!!")
                    Exit Sub
                End If
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End Sub

        Function CheckRefCode2BeforeUpdate()
            Try
                Dim Strcommand_FormType As String = ""
                Strcommand_FormType = " SELECT form_type FROM dbo.form_header_edi" & _
                             " WHERE (invh_run_auto = '" & txtInvRunAuto.Text.Trim & "')"
                Dim ds As New DataSet
                ds = SqlHelper.ExecuteDataset(Strconn, CommandType.Text, Strcommand_FormType)
                If ds.Tables(0).Rows.Count > 0 Then
                    Dim Strcommand As String = ""
                    Strcommand = " SELECT reference_code2 FROM form_header_edi" & _
                                 " WHERE (form_type = @FormType) AND (invh_run_auto = @InvRunAuto)"

                    Dim npm(1) As SqlClient.SqlParameter
                    npm(0) = New SqlClient.SqlParameter("@InvRunAuto", txtInvRunAuto.Text.Trim)
                    npm(1) = New SqlClient.SqlParameter("@FormType", ds.Tables(0).Rows(0).Item("form_type"))

                    Dim ds_ref2 As New DataSet
                    ds_ref2 = SqlHelper.ExecuteDataset(Strconn, CommandType.Text, Strcommand, npm)
                    If DBNull.Value.Equals(ds_ref2.Tables(0).Rows(0).Item("reference_code2")) Then
                        Return True
                    Else
                        Return False
                    End If
                Else
                    Return False
                End If
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End Function
    End Class
End Namespace
