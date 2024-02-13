<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frmDeleteProductItem.aspx.vb" Inherits=".frmDeleteProductItem" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>ลบรายการสินค้า</title>
    <link href="css/skin.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>
        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        </telerik:RadAjaxManager>
        <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
            <script type="text/javascript">
                function CloseAndRebind(args) {
                    GetRadWindow().Close();
                    GetRadWindow().BrowserWindow.refreshGrid(args);
                }

                function GetRadWindow() 
                {
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
        <table width="100%" border="0" cellpadding="0" cellspacing="5">
            <tr>
                <td>
                    <%--<div id="divForm" runat="server"></div>--%>
                    <table border="0" cellpadding="1" cellspacing="0" class="m-frm-bdr" width="100%">
                        <tr>
                            <td>
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td class="m-frm" valign="top" width="100%">
                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                <tr>
                                                    <td width="15">
                                                        &nbsp;</td>
                                                    <td>
                                                        <table style="width: 100%">
                                                            <tr>
                                                                <td style="text-align: center">
                                                                    <font class="FormLabel">ท่านต้องการลบรายการสินค้าใช่หรือไม่ ?</font></td>
                                                            </tr>
                                                            <tr>
                                                                <td style="text-align: center">
                                                                    &nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td style="text-align: center">
                                                                    <asp:Button ID="btnDelete" runat="server" Text="ลบ" UseSubmitBehavior="False" Width="150px" />
                                                                    <asp:Button ID="btnCancel" runat="server" Text="ยกเลิก" UseSubmitBehavior="False"
                                                                        Width="150px" /></td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <asp:Label ID="lblinvh_run_auto" runat="server" Visible="False"></asp:Label>
        <asp:Label ID="lblinvd_run_auto" runat="server" Visible="False"></asp:Label>
        <asp:Label ID="lblSite_ID" runat="server" Visible="False"></asp:Label>
    </form>
</body>
</html>
