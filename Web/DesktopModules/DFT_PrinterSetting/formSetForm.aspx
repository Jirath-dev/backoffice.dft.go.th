<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="formSetForm.aspx.vb" Inherits=".formSetForm" %>

<!DOCTYPE html>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="/js/jquery/jquery.js"></script>
    <link href="css/skin.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function GetRadWindow() {
            var oWindow = null;
            if (window.radWindow) oWindow = window.radWindow; //Will work in Moz in all cases, including clasic dialog
            else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow; //IE (and Moz as well)

            return oWindow;
        }
        function CloseAndRebind(args) {
            GetRadWindow().Close();
            GetRadWindow().BrowserWindow.refreshGrid(args);
        }
        function Close() {
            GetRadWindow().Close();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <telerik:RadScriptManager runat="server" ID="RadScriptManager1"></telerik:RadScriptManager>
        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        </telerik:RadAjaxManager>
        <div>
            <table cellspacing="1" cellpadding="0" class="m-frm-bdr" width="100%" border="0">
                <tr>
                    <td class="m-frm" valign="top" width="100%">
                        <table border="0" cellpadding="0" cellspacing="2" class="FormLabel">
                            <tr>
                                <td style="width: 200px">ฟอร์ม:
                                </td>
                                <td>
                                    <asp:DropDownList runat="server" ID="ddlForm" AutoPostBack="true"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>ประเภท:
                                </td>
                                <td>
                                    <asp:RadioButtonList runat="server" ID="rdoPrintType" RepeatDirection="Horizontal">
                                        <asp:ListItem Text="รวม" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="EDI" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="DS" Value="2"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: center">
                        <div style="padding: 8px">
                            <asp:Button runat="server" ID="btnSave" Text="Save" />
                            <input type="button" value="Cancel" onclick="Close();" />
                        </div>
                    </td>
                </tr>
            </table>

        </div>
    </form>
</body>
</html>
