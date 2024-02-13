Imports System.Web.UI
Imports System.Collections.Generic
Imports System.Reflection

Imports DotNetNuke
Imports DotNetNuke.Services.Exceptions
Imports DotNetNuke.Services.Localization

Imports Microsoft.ApplicationBlocks.Data
Imports System.Data.SqlClient

Partial Public Class ChangeSite
    Inherits Entities.Modules.PortalModuleBase

    Dim strConn As String = ConfigurationManager.ConnectionStrings("Ds2OriginConnection").ConnectionString

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            LoadSite()
        End If
    End Sub

    Private Sub LoadSite()
        Try
            Dim ds As New DataSet
            Dim strCommand As String
            strCommand = "SELECT * FROM site_plus " & _
                         "WHERE (active_status = 'Y') AND (type_site = 'A') AND (ds_status='Y') " & _
                         "ORDER BY site_code"
            ds = SqlHelper.ExecuteDataset(strConn, CommandType.Text, strCommand)
            If ds.Tables(0).Rows.Count > 0 Then
                ddlSite.DataSource = ds.Tables(0)
                ddlSite.DataTextField = "site_name"
                ddlSite.DataValueField = "site_id"
                ddlSite.DataBind()
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub LoadData()
        Try
            Dim ds As New DataSet
            ds = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, "vi_form_edi_getForChangeSite_NewDS2", _
                                            New SqlParameter("@INVH_RUN_AUTO", txtRefNo.Text.Trim()))

            If ds.Tables(0).Rows.Count > 0 Then
                If ds.Tables(0).Rows(0)("print_flag").ToString().ToUpper() = "N" And ds.Tables(0).Rows(0)("receipt_flag").ToString().ToUpper() = "N" Then
                    btnSave.Enabled = True
                    lblMsg.Visible = False
                Else
                    btnSave.Enabled = False
                    lblMsg.Visible = True
                End If
            Else
                btnSave.Enabled = False
                lblMsg.Visible = False
            End If

            rgRequestForm.DataSource = ds.Tables(0)
            rgRequestForm.DataBind()

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
        rgRequestForm.Visible = True
    End Sub

    ''' <summary>
    ''' บันทึกการเปลี่ยนสาขาที่รับหนังสือรับรอง
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSave.Click
        Try
            Dim ret As Integer
            'update site id
            ret = SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, "vi_form_edi_updateForChangeSite_NewDS2", _
                                            New SqlParameter("@INVH_RUN_AUTO", txtRefNo.Text.Trim()), _
                                            New SqlParameter("@SITE_ID", ddlSite.SelectedValue))
            'show data
            If ret > 0 Then
                LoadData()
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "mykey", "alert('Completed - บันทึกข้อมูลการเปลี่ยนสถานที่ออกหนังสือรับรองฯ เรียบร้อยแล้ว');", True)
            Else
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "mykey", "alert('Error - ไม่สามารถบันทึกข้อมูลการเปลี่ยนสถานที่ออกหนังสือรับรองฯ ได้');", True)
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
        LoadData()
    End Sub
End Class