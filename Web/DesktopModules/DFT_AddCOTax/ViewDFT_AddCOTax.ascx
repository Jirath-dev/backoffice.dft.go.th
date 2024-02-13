<%@ Control language="vb" Inherits="YourCompany.Modules.DFT_AddCOTax.ViewDFT_AddCOTax" AutoEventWireup="false" Explicit="True" Codebehind="ViewDFT_AddCOTax.ascx.vb" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="dnn" TagName="Audit" Src="~/controls/ModuleAuditControl.ascx" %>
<table border="0" cellpadding="0" cellspacing="4" style="width: 100%">
    <tr>
        <td style="width: 100%;">
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
                                                            <font class="groupbox">&nbsp; ค้นหาข้อมูล &nbsp;</font></td>
                                                        </tr>
                                                </table>
                                                <asp:Label ID="lblRoleID" runat="server" Text="lblRoleID" Visible="False"></asp:Label>&nbsp;
                                                <asp:Label ID="lblDSRoleID" runat="server" Text="lblDSRoleID" Visible="False"></asp:Label></td>
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
                                                        <td style="font-size: 12pt; color: #000000; font-family: Times New Roman">
                                                            <font class="FormLabel">ค้นหาเลขที่ภาษีว่าได้สมัครใจ :
                                                                <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>
                                                                <asp:Button ID="btnSearch" runat="server" Text="ค้นหา" Width="150px" ValidationGroup="search" />
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtSearch"
                                                                    Display="Dynamic" ErrorMessage="กรุณาป้อนเลขที่ภาษี" Font-Names="Tahoma" Font-Size="10pt"
                                                                    SetFocusOnError="True" ValidationGroup="search"></asp:RequiredFieldValidator></font></td>
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
    <tr>
        <td align="center">
            <asp:Label ID="lblError" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label></td>
    </tr>
    <tr>
        <td style="width: 100%">
            <asp:Panel ID="Panel_Company" runat="server" Width="100%" Visible="False">
                <table style="width: 100%" border="0" cellpadding="0" cellspacing="2" class="m-frm-bdr">
                    <tr>
                        <td colspan="3" class="m-frm-hdr" nowrap="nowrap"><table border="0" cellpadding="0" cellspacing="0" cols="2">
                            <tr>
                                <td class="groupboxheader" nowrap="nowrap">
                                    <font class="groupbox">&nbsp; รายละเอียด &nbsp;</font></td>
                            </tr>
                        </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 200px" class="FormLabel">
                            ชื่อบริษัท (ไทย)</td>
                        <td>
                            <asp:Label ID="lblcompany_thai" runat="server" Font-Bold="True" ForeColor="#0000C0" CssClass="FormLabel"></asp:Label></td>
                        <td style="width: 40px">
                            </td>
                    </tr>
                    <tr>
                        <td class="FormLabel">
                            ชื่อบริษัท (อังกฤษ)</td>
                        <td>
                            <asp:Label ID="lblcompany_eng" runat="server" Font-Bold="True" ForeColor="#0000C0" CssClass="FormLabel"></asp:Label></td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="FormLabel">
                            เลขที่ภาษี</td>
                        <td>
                            <asp:Label ID="lblcompany_taxno" runat="server" Font-Bold="True" ForeColor="#0000C0" CssClass="FormLabel"></asp:Label></td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="FormLabel">
                            สถานะการสมัครใจ</td>
                        <td>
                            <asp:Label ID="lblStatus" runat="server" Font-Bold="True" ForeColor="Red" CssClass="FormLabel"></asp:Label></td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <asp:Button ID="btnAddCo" runat="server" Text="เพิ่ม สมัครใจ" Width="150px" ValidationGroup="search" /></td>
                        <td>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </td>
    </tr>
    <tr>
        <td style="width: 100%">
        </td>
    </tr>
</table>

