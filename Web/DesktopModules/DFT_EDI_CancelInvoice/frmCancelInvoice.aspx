<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frmCancelInvoice.aspx.vb" Inherits=".frmCancelInvoice" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>ยืนยันการยกเลิก INVOICE</title>
    <link href="CSS/skin.css" rel="stylesheet" type="text/css" />
</head>
<body>
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
        <table border="0" cellpadding="0" cellspacing="5" width="100%">
            <tr>
                <td>
                    <table border="0" cellpadding="1" cellspacing="0" class="m-frm-bdr" width="100%">
                        <tr>
                            <td>
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td class="m-frm" valign="top" width="100%">
                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                <tr>
                                                    <td width="15">
                                                    </td>
                                                    <td style="text-align: left">
                                                        <asp:Label ID="lbl_ErrMSG" runat="server" Font-Names="Tahoma" Font-Size="10pt" ForeColor="Red"></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td width="15">
                                                        &nbsp;</td>
                                                    <td style="text-align: left">
                                                        <table border="0" cellpadding="0" cellspacing="2" width="100%">
                                                            <tr>
                                                                <td align="center">
                                                                    <table cellpadding="5" cellspacing="5">
                                                                        <tr>
                                                                            <td align="center">
                                                                                <font class="FormLabel"> ท่านต้องการยกเลิกหมายเลข Invoice ใช่หรือไม่ ?</font></td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="text-align: center">
                                                                    <asp:Button ID="btnOK" runat="server" Text="ตกลง" ValidationGroup="Insert"
                                                                        Width="170px" />
                                                                    <asp:Button ID="btnCancel" runat="server" Text="ยกเลิก" Width="170px" /></td>
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

        <asp:Label ID="lblCompany_Taxno" runat="server" ></asp:Label>
           <asp:Label ID="lblinvh_run_auto" runat="server" ></asp:Label>
        <asp:Label ID="lblInvoiceNo" runat="server" ></asp:Label>
        <asp:Label ID="lblInvoiceYear" runat="server" ></asp:Label>
        <asp:Label ID="lblFType" runat="server" ></asp:Label>
    </form>
</body>
</html>
