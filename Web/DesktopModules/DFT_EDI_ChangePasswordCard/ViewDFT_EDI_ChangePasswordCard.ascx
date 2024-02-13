<%@ Control Language="vb" Inherits="YourCompany.Modules.DFT_EDI_ChangePasswordCard.ViewDFT_EDI_ChangePasswordCard"
    AutoEventWireup="false" Explicit="True" CodeBehind="ViewDFT_EDI_ChangePasswordCard.ascx.vb" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<table cellspacing="5" width="100%">
    <tr>
        <td>
            <table cellspacing="0" border="0" class="m-frm-bdr" cellpadding="1" width="100%">
                <tr>
                    <td>
                        <table cellspacing="0" cellpadding="0" border="0" width="100%">
                            <tr class="m-frm-hdr">
                                <td>
                                    <table cellspacing="0" cellpadding="0" border="0" width="100%">
                                        <tr class="m-frm-hdr">
                                            <td align="left" class="m-frm-hdr">
                                                <table border="0" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td class="groupboxheader">
                                                            <font class="groupbox">&#160;เปลี่ยน Password จาก CardID&nbsp;
                                                               <%-- <asp:Label runat="server" ID="lblType" ForeColor="Yellow"></asp:Label>--%>
                                                            </font>
                                                        </td>
                                                        <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
                                                            <td style="background-image: url(<%= ResolveUrl("images") %>/CORNER.gif); background-repeat: no-repeat;">
                                                                &#160;&#160;&#160;&#160;
                                                            </td>
                                                        </telerik:RadCodeBlock></tr>
                                                </table>
                                            </td>
                                            <td align="right" class="m-frm-nav">
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr class="m-frm-hdr">
                                <td>
                                    <table style="width: 100%">
                                        <tr>
                                            <td style="width: 100%">
                                                <table cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td colspan="4">
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <font class="FormLabel">Card ID : &nbsp;</font>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox runat="server" ID="txtCardID"></asp:TextBox></td>
                                                        <td  colspan="4">
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="RequiredFieldValidator"
                                                                ControlToValidate="txtCardID" ValidationGroup="update">*</asp:RequiredFieldValidator>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="RequiredFieldValidator"
                                                                ControlToValidate="txtCardID" ValidationGroup="search">*</asp:RequiredFieldValidator>
                                                                <asp:Button ID="btnSearch" runat="Server" Text="ค้นหา" ValidationGroup="search" />
                                                        </td>
                                                        <td>
                                                            
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <font class="FormLabel">Password หนังสือรับรอง : &nbsp;</font>
                                                        </td>
                                                        <td  colspan="4">
                                                            <asp:TextBox runat="server" ID="txtPassword_EDI" MaxLength="4"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="RequiredFieldValidator"
                                                                ControlToValidate="txtPassword_EDI" ValidationGroup="update">*</asp:RequiredFieldValidator>&nbsp;
                                                        </td>
                                                       
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <font class="FormLabel">Password ใบอนุญาต : &nbsp;</font>
                                                        </td>
                                                        <td  colspan="4">
                                                            <asp:TextBox runat="server" ID="txtPassword_Trading" MaxLength="4"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="RequiredFieldValidator"
                                                                ControlToValidate="txtPassword_Trading" ValidationGroup="update">*</asp:RequiredFieldValidator>&nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                        </td>
                                                        <table>
                                                            <tr>
                                                                <td align="right" style="width:135px">
                                                                </td>
                                                                <td>
                                                                    <asp:RadioButtonList ID="rblSystemChange" runat="server">
                                                                        <asp:ListItem Text="แก้ไขรหัสผ่านทั้งสองระบบ (EDI และ Trading)" Value="ALL" Selected="True"></asp:ListItem>
                                                                        <asp:ListItem Text="แก้ไขรหัสผ่านระบบหนังสือรับรอง (EDI)" Value="EDI"></asp:ListItem>
                                                                        <asp:ListItem Text="แก้ไขรหัสผ่านระบบใบอนุญาต (Trading)" Value="TRADING"></asp:ListItem>
                                                                    </asp:RadioButtonList>
                                                                </td>
                                                            </tr>
                                                             <tr>
                                                        <td colspan="4" align="center" >
                                                            <div style="margin: 7px;">
                                                                <asp:Label runat="server" ID="lblMSG" ForeColor="Red" Visible="false">ไม่พบข้อมูลที่ค้นหา</asp:Label></div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4" align="center">
                                                            <div style="margin: 7px;">
                                                                <asp:Button ID="btnUpdatePassword" runat="Server" Text="เปลี่ยนรหัสผ่าน" Enabled="false"
                                                                    ValidationGroup="update" />
                                                            </div>
                                                        </td>
                                                    </tr>
                                                        </table>
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
