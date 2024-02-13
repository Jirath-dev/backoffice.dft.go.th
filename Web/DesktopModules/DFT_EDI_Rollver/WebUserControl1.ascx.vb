Public Partial Class WebUserControl1
    Inherits Entities.Modules.PortalModuleBase

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Response.Redirect("http://mysite.backoffice.dft.go.th/ระบบงานออกหนงสอรบรองถนกำเนดสนคา/ระบบงานรบหนงสอรบรองEDI/Test/tabid/83/ctl/WebUserControl2/mid/384/Default.aspx")
        'Response.Redirect(EditUrl("WebUserControl2"))
        Response.Write("dddddddddddddddddddddddd")
    End Sub

    Protected Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Response.Write("dddddddddddddddddddddddd")
        'Response.Redirect("http://mysite.backoffice.dft.go.th/ระบบงานออกหนงสอรบรองถนกำเนดสนคา/ระบบงานรบหนงสอรบรองEDI/Test/tabid/83/ctl/WebUserControl2/mid/384/Default.aspx")
        'http://mysite.backoffice.dft.go.th/ระบบงานออกหนงสอรบรองถนกำเนดสนคา/ระบบงานรบหนงสอรบรองEDI/Test/tabid/83/ctl/WebUserControl2/mid/384/Default.aspx
    End Sub
End Class