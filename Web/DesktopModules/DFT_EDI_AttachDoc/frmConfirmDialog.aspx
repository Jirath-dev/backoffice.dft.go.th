<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frmConfirmDialog.aspx.vb" Inherits=".frmConfirmDialog" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>บันทึกข้อมูล</title>
    <link href="css/skin.css" rel="stylesheet" type="text/css" />
</head>
<body class="m-frm-hdr">
    <form id="form1" runat="server">
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>
        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        </telerik:RadAjaxManager>
        <telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">
            <script type="text/javascript">
                function CloseAndRebind(args) {
                GetRadWindow().Close();
                GetRadWindow().BrowserWindow.refreshGrid(args);
                }

                function GetRadWindow() {
                    var oWindow = null;
                    if (window.radWindow) oWindow = window.radWindow; //Will work in Moz in all cases, including clasic dialog
                    else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow; //IE (and Moz as well)

                    return oWindow;
                }

                function CancelEdit() {
                    GetRadWindow().Close();
                }
            </script>
        </telerik:RadCodeBlock>
        <center>
            <br />
            <font class="FormLabel"><asp:Label ID="lblMsg" runat="server" ></asp:Label></font>
            <br />
            <br />
            <asp:Button ID="btnSave" runat="server" Text="ใช่" Width="150px" />
            <asp:Button ID="btnClose" runat="server" Text="ยกเลิก" Width="150px" />
        </center>
        <asp:Label ID="lblTCat" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="lblInvh_run_auto" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="lblRemark_docatt" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="lblUser_ID" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="lblSite_ID" runat="server" Visible="false"></asp:Label>
    </form>
</body>
</html>
