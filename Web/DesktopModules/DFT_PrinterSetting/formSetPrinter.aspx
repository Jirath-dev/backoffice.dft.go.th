<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="formSetPrinter.aspx.vb" Inherits=".formSetPrinter" %>

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
                                <td style="width:200px">สาขา:
                                </td>
                                <td>
                                    <asp:DropDownList runat="server" ID="ddlSite" AutoPostBack="true"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td >ชื่อเครื่องพิมพ์:
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtPrinterName" Width="300px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td >รายละเอียด:
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtDesc" Width="300px" TextMode="MultiLine" Rows="3"></asp:TextBox>
                                </td>
                            </tr>
                             <tr>
                                <td >Active:
                                </td>
                                <td>
                                    <asp:CheckBox runat="server" ID="chkActive" Text="เปิด/ปิด การใช้งาน" Checked="True" />
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
