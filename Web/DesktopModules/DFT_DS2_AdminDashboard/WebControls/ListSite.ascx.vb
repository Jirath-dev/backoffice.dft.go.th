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
            strCommand = "SELECT site_id,site_name+' : '+site_id AS site_name  FROM site_plus " & _
                         "WHERE (active_status = 'Y') AND (type_site = 'A') AND (ds_status='N') " & _
                         "ORDER BY site_code"
            ds = SqlHelper.ExecuteDataset(strConn, CommandType.Text, strCommand)
            lstSiteClose.DataSource = ds.Tables(0)
            lstSiteClose.DataTextField = "site_name"
            lstSiteClose.DataValueField = "site_id"
            lstSiteClose.DataBind()

            strCommand = "SELECT site_id,site_name+' : '+site_id AS site_name FROM site_plus " & _
                         "WHERE (active_status = 'Y') AND (type_site = 'A') AND (ds_status='Y') " & _
                         "ORDER BY site_code"
            ds = SqlHelper.ExecuteDataset(strConn, CommandType.Text, strCommand)
            lstSiteOpen.DataSource = ds.Tables(0)
            lstSiteOpen.DataTextField = "site_name"
            lstSiteOpen.DataValueField = "site_id"
            lstSiteOpen.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub btnAdd_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAdd.Click
        SiteDo("Y")
    End Sub

    Private Function SiteDo(ByVal status As Char) As String
        Try
            Dim str_err As String = ""
            Dim strCommand As String = ""

            If status = "N" Then
                If lstSiteOpen.SelectedValue <> "" Then
                    strCommand = "UPDATE site_plus SET ds_status='" & status & "' " & _
                                    "WHERE (site_id='" & lstSiteOpen.SelectedValue & "')"
                Else
                    str_err = "โปรดเลือกชื่อสาขาจากช่อง สาขาที่เปิดใช้งานเรียบร้อยแล้ว ทางด้านขวาให้เรียบร้อยก่อน"
                End If
            Else
                If lstSiteClose.SelectedValue <> "" Then
                    strCommand = "UPDATE site_plus SET ds_status='" & status & "' " & _
                                    "WHERE (site_id='" & lstSiteClose.SelectedValue & "')"
                Else
                    str_err = "โปรดเลือกชื่อสาขาจากช่อง สาขา ทางด้านซ้ายให้เรียบร้อยก่อน"
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
        SiteDo("N")
    End Sub

    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        LoadSite()
    End Sub
End Class