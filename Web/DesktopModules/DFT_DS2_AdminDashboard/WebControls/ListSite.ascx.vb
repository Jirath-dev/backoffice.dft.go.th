Imports System.Web.UI
Imports System.Collections.Generic
Imports System.Reflection

Imports DotNetNuke
Imports DotNetNuke.Services.Exceptions
Imports DotNetNuke.Services.Localization

Imports Microsoft.ApplicationBlocks.Data
Imports System.Data.SqlClient

Partial Public Class ListSite
    Inherits Entities.Modules.PortalModuleBase

    Dim strConn As String = ConfigurationManager.ConnectionStrings("EDIConnection").ConnectionString

    Private Sub LoadSite()
        Try
            Dim ds As New DataSet
            Dim strCommand As String
            strCommand = "SELECT form_type,form_name+' : '+form_type AS form_name  
                          FROM form_type " &
                         "WHERE (ShowForm  = '0')  " &
                         "ORDER BY form_type"
            ds = SqlHelper.ExecuteDataset(strConn, CommandType.Text, strCommand)
            lstSiteClose.DataSource = ds.Tables(0)
            lstSiteClose.DataTextField = "form_name"
            lstSiteClose.DataValueField = "form_type"
            lstSiteClose.DataBind()

            strCommand = "SELECT form_type,form_name+' : '+form_type AS form_name
                          FROM form_type " &
                         "WHERE (ShowForm = '1')  " &
                         "ORDER BY form_type"
            ds = SqlHelper.ExecuteDataset(strConn, CommandType.Text, strCommand)
            lstSiteOpen.DataSource = ds.Tables(0)
            lstSiteOpen.DataTextField = "form_name"
            lstSiteOpen.DataValueField = "form_type"
            lstSiteOpen.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub btnAdd_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAdd.Click
        SiteDo("1")
    End Sub

    Private Function SiteDo(ByVal status As Char) As String
        Try
            Dim str_err As String = ""
            Dim strCommand As String = ""

            If status = "1" Then
                If lstSiteOpen.SelectedValue <> "" Then
                    strCommand = "UPDATE form_type 
                                  SET ShowForm='" & status & "' " &
                                  "WHERE (form_type='" & lstSiteOpen.SelectedValue & "')"
                Else
                    str_err = "โปรดเลือกฟอร์ม เปิดใช้งานเรียบร้อยแล้ว ทางด้านขวาให้เรียบร้อยก่อน"
                End If
            Else
                If lstSiteClose.SelectedValue <> "" Then
                    strCommand = "UPDATE form_type 
                                  SET ShowForm='" & status & "' " &
                                  "WHERE (form_type='" & lstSiteClose.SelectedValue & "')"
                Else
                    str_err = "โปรดเลือกโปรดเลือกฟอร์ม จากช่อง ฟอร์ม ทางด้านซ้ายให้เรียบร้อยก่อน"
                End If
            End If

            If str_err = "" Then
                'run sql update database
                SqlHelper.ExecuteNonQuery(strConn, CommandType.Text, strCommand)

                'refresh listbox
                LoadSite()

                lblMsg.ForeColor = Drawing.Color.Green
                lblMsg.Text = "บันทึกข้อมูลเสร็จเรียบร้อยแล้ว"
            Else
                lblMsg.ForeColor = Drawing.Color.Red
                lblMsg.Text = str_err
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Function

    Protected Sub btnRemove_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnRemove.Click
        SiteDo("0")
    End Sub

    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        LoadSite()
    End Sub
End Class