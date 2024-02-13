<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frmChangeCompany.aspx.vb" Inherits=".frmChangeCompany" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>เปลี่ยนบริษัทฯ</title>
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

                function GetRadWindow() {
                    var oWindow = null;
                    if (window.radWindow) oWindow = window.radWindow; //Will work in Moz in all cases, including clasic dialog
                    else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow; //IE (and Moz as well)

                    return oWindow;
                }

                function CancelEdit() {
                    GetRadWindow().Close();
                }
                
                function returnToParent(){
                    var oWnd = GetRadWindow();
                    oWnd.close();
                }
            </script>
        </telerik:RadCodeBlock>
        <table width="100%" border="0" cellpadding="0" cellspacing="5">
            <tr>
                <td>
                    <table border="0" cellpadding="1" cellspacing="0" class="m-frm-bdr" width="100%">
                        <tr>
                            <td>
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tr class="m-frm-hdr">
                                        <td>
                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                <tr class="m-frm-hdr">
                                                    <td align="left" class="m-frm-hdr">
                                                        <table border="0" cellpadding="0" cellspacing="0" cols="2">
                                                            <tr>
                                                                <td class="groupboxheader" nowrap="nowrap">
                                                                    <font class="groupbox">&nbsp; เปลี่ยนบริษัท &nbsp;</font></td>
                                                                <telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">
                                                                    <td style='background-image: url(<%= ResolveUrl("images") %>/CORNER.gif); background-repeat: no-repeat;'>
                                                                        &nbsp; &nbsp;&nbsp;</td>
                                                                </telerik:RadCodeBlock>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td align="right" class="m-frm-nav">
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="m-frm" valign="top" width="100%">
                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                <tr>
                                                    <td width="15">
                                                        &nbsp;</td>
                                                    <td>
                                                        <table border="0" cellpadding="0" cellspacing="2" width="100%">
                                                            <tr>
                                                                <td align="left">
                                                                    <font class="FormLabel">เลขประจำตัวผู้เสีภาษี :</font>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtCompany_Taxno"
                                                                        Display="Dynamic" ErrorMessage="*" Font-Names="Tahoma" Font-Size="10pt" SetFocusOnError="True"
                                                                        ValidationGroup="search"></asp:RequiredFieldValidator></td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txtCompany_Taxno" runat="server" MaxLength="10" Font-Bold="True"
                                                                        Font-Size="14pt" Width="100%"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left">
                                                                    <font class="FormLabel">สาขา :</font>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txtCompany_BranchNo" runat="server" Font-Bold="True" Font-Size="16pt"
                                                                        MaxLength="1" Width="100%" Text="1"></asp:TextBox></td>
                                                            </tr>
                                                            <tr>
                                                                <td style="text-align: right">
                                                                    <asp:Button ID="btnSave" runat="server" Text="ตกลง" Width="150px" ValidationGroup="search" /></td>
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
        <asp:Label ID="lblSiteName" runat="server" Visible="False"></asp:Label>
        <asp:Label ID="lblInvh_Run_Auto" runat="server" Visible="False"></asp:Label>
    </form>
</body>
</html>
