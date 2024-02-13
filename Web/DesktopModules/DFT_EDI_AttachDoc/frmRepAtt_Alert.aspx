<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frmRepAtt_Alert.aspx.vb" Inherits=".frmRepAtt_Alert" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>
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
        <table style="width: 100%">
            <tr>
                <td style="width: 100%; text-align: center">
                    <asp:Label ID="lblReturnMsg" runat="server" Font-Names="Tahoma" Font-Size="10pt"
                        ForeColor="#0000C0"></asp:Label></td>
            </tr>
            <tr>
                <td style="width: 100%; text-align: center">
                    <asp:Button ID="btnClose" runat="server" Text="ปิดหน้าต่าง" Width="200px" /></td>
            </tr>
        </table>
    </form>
</body>
</html>
