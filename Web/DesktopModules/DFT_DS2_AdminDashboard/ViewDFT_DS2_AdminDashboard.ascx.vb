
Imports System.Web.UI
Imports System.Collections.Generic
Imports System.Reflection

Imports DotNetNuke
Imports DotNetNuke.Services.Exceptions
Imports DotNetNuke.Services.Localization

Imports System.Management
Imports System.DirectoryServices


Namespace NTI.Modules.DFT_DS2_AdminDashboard

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' The ViewDFT_DS2_AdminDashboard class displays the content
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Partial Class ViewDFT_DS2_AdminDashboard
        Inherits Entities.Modules.PortalModuleBase

        Protected Sub btnReAppPool_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnReAppPool.Click
            Dim machine As String
            Dim AppPoolFullPath As String

            machine = System.Environment.MachineName
            AppPoolFullPath = "IIS://" + machine + "/W3SVC/AppPools/" + lstAppName.SelectedValue

            Try
                Dim w3svc As New DirectoryEntry(AppPoolFullPath)
                w3svc.Invoke("Recycle")
                lblRecycleAppMsg.Text = "Completed - เสร็จเรียบร้อยแล้ว"
            Catch ex As Exception
                lblRecycleAppMsg.Text = "<font style=""font-size:8pt;"" color=""#f00000"">Error - ไม่สามารถทำการ recycle application pool ได้<br/>" & ex.Message & "</font>"
            End Try

            lblRecycleAppMsg.Text &= "<br/>" & AppPoolFullPath

        End Sub

        Protected Sub LinkButton1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles LinkButton1.Click
            'LoadMyControl("ListXML")
            Response.Redirect(EditUrl("ListXML"))
        End Sub

        Protected Sub LinkButton6_Click(ByVal sender As Object, ByVal e As EventArgs) Handles LinkButton6.Click
            'LoadMyControl("ListXML")
            Response.Redirect(EditUrl("ListAttachSheet"))
        End Sub

        Protected Sub LoadMyControl(ByVal UserControlName As String)
            ' Load web user control
            Dim myUserControl As Control = Page.LoadControl("~/DesktopModules/DFT_DS2_AdminDashboard/WebControls/" & UserControlName & ".ascx")
            ' Place web user control to place holder control
            myUserControl.ID = "U_WEBCONTROL"
            phModule.Controls.Add(myUserControl)
        End Sub

        Protected Sub LinkButton2_Click(ByVal sender As Object, ByVal e As EventArgs) Handles LinkButton2.Click
            Response.Redirect(EditUrl("ListPDF"))
        End Sub

        Protected Sub LinkButton5_Click(ByVal sender As Object, ByVal e As EventArgs) Handles LinkButton5.Click
            Response.Redirect(EditUrl("ChangeSite"))
        End Sub

        Protected Sub LinkButton4_Click(ByVal sender As Object, ByVal e As EventArgs) Handles LinkButton4.Click
            Response.Redirect(EditUrl("ListSite"))
        End Sub
        Protected Sub LinkButton3_Click(ByVal sender As Object, ByVal e As EventArgs) Handles LinkButton3.Click
            Response.Redirect(EditUrl("ListFrom"))
        End Sub

        Protected Sub LinkPrinter_Click(ByVal sender As Object, ByVal e As EventArgs) Handles linkPrinter.Click
            Response.Redirect(EditUrl("Printer"))
        End Sub

        Protected Sub LinkRollver_Click(ByVal sender As Object, ByVal e As EventArgs) Handles LinkRollver.Click
            Response.Redirect(EditUrl("Rollver"))
        End Sub

        Protected Sub LinkReport_Click(ByVal sender As Object, ByVal e As EventArgs) Handles linkReport.Click
            Response.Redirect(EditUrl("Report"))
        End Sub

        Protected Sub linkInvoice_Click(ByVal sender As Object, ByVal e As EventArgs) Handles linkInvoice.Click
            Response.Redirect(EditUrl("InvoiceEDI"))
        End Sub

        Protected Sub linkRemoveName_Click(ByVal sender As Object, ByVal e As EventArgs) Handles linkRemoveName.Click
            Response.Redirect(EditUrl("RemoveName"))
        End Sub

        Protected Sub linkReStatus_Click(ByVal sender As Object, ByVal e As EventArgs) Handles linkReStatus.Click
            Response.Redirect(EditUrl("ReStatus"))
        End Sub

        Protected Sub linkOpenCard_Click(ByVal sender As Object, ByVal e As EventArgs) Handles LinkOpenCard.Click
            Response.Redirect(EditUrl("OpenCard"))
        End Sub


    End Class

End Namespace
