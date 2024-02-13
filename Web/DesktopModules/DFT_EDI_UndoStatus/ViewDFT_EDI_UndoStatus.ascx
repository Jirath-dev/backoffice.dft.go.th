<%@ Control language="vb" Inherits="NTi.Modules.DFT_EDI_UndoStatus.ViewDFT_EDI_UndoStatus" AutoEventWireup="false" Explicit="True" Codebehind="ViewDFT_EDI_UndoStatus.ascx.vb" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
</telerik:RadAjaxManager>
<table width="100%" border="0" cellspacing="3" cellpadding="0">
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
                                            <td width="88%" align="left" class="m-frm-hdr">
                                                <table cols="2" border="0" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td nowrap class="groupboxheader">
                                                            <font class="groupbox">&#160;&#160;ค้นหา&#160;&#160;</font></td>
                                                        <telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">
                                                            <td style="background-image: url(<%= ResolveUrl("images") %>/CORNER.gif); background-repeat: no-repeat;">
                                                                &#160;&#160;&#160;&#160;</td>
                                                        </telerik:RadCodeBlock>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td width="12%" align="left" class="m-frm-nav">
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td class="m-frm" valign="top" width="100%">
                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td width="15">
                                                &nbsp;</td>
                                            <td>
                                                <table width="100%" border="0" cellspacing="2" cellpadding="0">
                                                    <tr>
                                                        <td>
                                                            <font class="FormLabel">เลขที่หนังสือรับรอง :</font>
                                                            <asp:TextBox ID="txtReferenceCode2" runat="server" Font-Names="Tahoma" Font-Size="14pt"
                                                                MaxLength="15" Width="150px"></asp:TextBox>
                                                            <asp:Button ID="btnSearch" runat="server" Text="ค้นหา" ValidationGroup="search" Width="150px" />
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtReferenceCode2"
                                                                ErrorMessage="กรุณาป้อนเลขที่หนังสือรับรอง" Font-Names="Tahoma" Font-Size="10pt"
                                                                ValidationGroup="search"></asp:RequiredFieldValidator></td>
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
<table id="tblHeader" runat="server" border="0" cellpadding="0" cellspacing="3" width="100%">
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
                                            <td align="left" class="m-frm-hdr" width="88%">
                                                <table border="0" cellpadding="0" cellspacing="0" cols="2">
                                                    <tr>
                                                        <td class="groupboxheader" nowrap="nowrap">
                                                            <font class="groupbox">&nbsp;&nbsp; รายละเอียดหนังสือรับรอง &nbsp;</font></td>
                                                        <telerik:RadCodeBlock ID="RadCodeBlock3" runat="server">
                                                            <td style='background-image: url(<%= ResolveUrl("images") %>/CORNER.gif); background-repeat: no-repeat;'>
                                                                &nbsp; &nbsp;&nbsp;</td>
                                                        </telerik:RadCodeBlock>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td align="left" class="m-frm-nav" width="12%">
                                                &nbsp;</td>
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
                                                        <td>
                                                            <asp:Button ID="btnUndoStatus" runat="server" Text="ย้อนสถานะ" ValidationGroup="search" Width="150px" /></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <font class="FormLabel">เลขที่คำร้อง :</font>
                                                            <asp:Label ID="lblInv_Auto_Run" runat="server" CssClass="FormFld"></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <font class="FormLabel">เลขที่หนังสือรับรอง :</font>
                                                            <asp:Label ID="lblReferenceCode2" runat="server" CssClass="FormFld"></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <font class="FormLabel">ชื่อฟอร์ม :</font>
                                                            <asp:Label ID="lblForm_Name" runat="server" CssClass="FormFld"></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <font class="FormLabel">บริษัท :</font>
                                                            <asp:Label ID="lblCompany_Name" runat="server" CssClass="FormFld"></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <font class="FormLabel">สถานะ :</font>
                                                            <asp:Label ID="lblEdi_Status" runat="server" CssClass="FormFld"></asp:Label></td>
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
<asp:Label ID="lblRoleID" runat="server" Visible="False"></asp:Label>
