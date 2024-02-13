Imports DFT.Dotnetnuke.ClassLibrary
Imports Microsoft.ApplicationBlocks.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI

Partial Public Class frmDeleteBookingForm
    Inherits System.Web.UI.Page
    Dim strConn As String = ConfigurationManager.ConnectionStrings("OriginConnection").ConnectionString

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If Request.QueryString("InvHRunAuto") <> "" Then
                lblInvHRunAuto.Text = Request.QueryString("InvHRunAuto")
            End If
        End If
    End Sub

    Protected Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Try
            Dim dr As SqlDataReader
            dr = SqlHelper.ExecuteReader(strConn, CommandType.StoredProcedure, "spFormManual_delete", _
            New SqlParameter("@SiteID", CommonUtility.Get_StringValue(Session("ssRoleName"))), _
            New SqlParameter("@InvhRunAuto", CommonUtility.Get_StringValue(lblInvHRunAuto.Text)))

            If dr.Read() Then
                If CommonUtility.Get_Int32(dr.Item("retStatus")) = 0 Then
                    dr.Close()
                    ClientScript.RegisterStartupScript(Page.GetType(), "mykey", "CloseAndRebind();", True)
                Else
                    lbl_ErrMSG.Text = "ไม่สามารถยกเลิกการรับหนังสือรับรองได้"
                    dr.Close()
                End If
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        ClientScript.RegisterStartupScript(Page.GetType(), "mykey", "CancelEdit();", True)
    End Sub
End Class