<%@ Control Language="vb" Inherits="DFT_SearchPhone.ViewDFT_SearchPhone" AutoEventWireup="false"
    Explicit="True" Codebehind="ViewDFT_SearchPhone.ascx.vb" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
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
                                                            <font class="groupbox">&#160;&#160;ค้นหาหมายเลขโทรศัพท์ผู้ประกอบการ&#160;&#160;</font></td>
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
                                            <td width="15" style="height: 55px">
                                                &nbsp;</td>
                                            <td style="height: 55px">
                                                <table width="100%" border="0" cellspacing="2" cellpadding="0">
                                                    <tr>
                                                        <td>
                                                            <font class="FormLabel">หมายเลขบัตรประจำตัวผู้ส่งออก: </font>
                                                            <asp:TextBox ID="txtCardID" runat="server" Font-Names="Tahoma" Font-Size="12pt" MaxLength="15"
                                                                Width="150px"></asp:TextBox>&nbsp;
                                                            <asp:Button ID="btnSearch" runat="server" Text="ค้นหา" Width="150px" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td class="m-frm" valign="top" width="100%">
                                    <table width="100%" border="0" cellspacing="2" cellpadding="0">
                                        <tr>
                                            <td style="width: 100px; height: 19px;" align="right">
                                                <asp:Label ID="lblName_H" runat="server" Text="ชื่อ - นามสกุล : " Font-Names="tahoma"
                                                    Font-Size="10pt" Font-Bold="true"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:Label ID="lblName_D" runat="server" Text="" Font-Names="tahoma" Font-Size="12pt"
                                                Font-Bold="true" ForeColor="blue" ></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100px; height: 19px;" align="right">
                                                <asp:Label ID="lblPhoneNumber_H" runat="server" Text="เบอร์โทรศัพท์ : " Font-Names="tahoma"
                                                    Font-Size="10pt" Font-Bold="true"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:Label ID="lblPhoneNumber_Back" runat="server" Text="" Font-Names="tahoma" Font-Size="12pt"
                                                    Font-Bold="true" ForeColor="blue"></asp:Label>
                                                <asp:Label ID="lblPhoneNumber_Front" runat="server" Text="" Font-Names="tahoma" Font-Size="12pt"
                                                    Font-Bold="true" ForeColor="blue"></asp:Label></td>
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
<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
</telerik:RadAjaxManager>
