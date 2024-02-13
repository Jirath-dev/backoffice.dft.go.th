<%@ Control Language="vb" Inherits="YourCompany.Modules.DFT_ADDCardid_DS.ViewDFT_ADDCardid_DS"
    AutoEventWireup="false" Explicit="True" Codebehind="ViewDFT_ADDCardid_DS.ascx.vb" %>
<%@ Register TagPrefix="dnn" TagName="Audit" Src="~/controls/ModuleAuditControl.ascx" %>
<table border="0" cellpadding="0" cellspacing="4" style="width: 100%">
    <tr>
        <td style="width: 100%">
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
                                                            <font class="FormLabel">เลขที่ภาษี :
                                                                <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>
                                                                <asp:Button ID="btnSearch" runat="server" Text="ค้นหา" ValidationGroup="search" Width="150px" />
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
            <asp:Panel ID="Panel_Company" runat="server" Visible="False" Width="100%">
                <table border="0" cellpadding="0" cellspacing="2" class="m-frm-bdr" style="width: 100%">
                    <tr>
                        <td class="m-frm-hdr" colspan="3" nowrap="nowrap">
                            <table border="0" cellpadding="0" cellspacing="0" cols="2">
                                <tr>
                                    <td class="groupboxheader" nowrap="nowrap">
                                        <font class="groupbox">&nbsp; รายละเอียด &nbsp;</font></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="FormLabel" style="width: 270px">
                            ชื่อบริษัท (ไทย)</td>
                        <td>
                            <asp:Label ID="lblcompany_thai" runat="server" CssClass="FormLabel" Font-Bold="True"
                                ForeColor="#0000C0"></asp:Label></td>
                        <td style="width: 40px">
                        </td>
                    </tr>
                    <tr>
                        <td class="FormLabel">
                            ชื่อบริษัท (อังกฤษ)</td>
                        <td>
                            <asp:Label ID="lblcompany_eng" runat="server" CssClass="FormLabel" Font-Bold="True"
                                ForeColor="#0000C0"></asp:Label></td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="FormLabel">
                            เลขที่ภาษี</td>
                        <td>
                            <asp:Label ID="lblcompany_taxno" runat="server" CssClass="FormLabel" Font-Bold="True"
                                ForeColor="#0000C0"></asp:Label></td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="FormLabel">
                            สถานะการสมัคร
                        </td>
                        <td>
                            <asp:Label ID="lblStatus" runat="server" CssClass="FormLabel" Font-Bold="True" ForeColor="Red"></asp:Label></td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="FormLabel" valign="top">
                            เลขบัตรประจำตัวผู้นำเข้าส่งออกทั้งหมด</td>
                        <td>
                            <asp:Label ID="lblListCard" runat="server" CssClass="FormLabel" Font-Bold="True"
                                ForeColor="#00C000"></asp:Label></td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="FormLabel" valign="top">
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <asp:Button ID="btnAddCo" runat="server" Text="เพิ่มเลขบัตรเพื่อใช้งาน Digital" ValidationGroup="search" /></td>
                        <td>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </td>
    </tr>
    <tr>
        <td style="width: 100%">
            <asp:TextBox ID="txtTempCardAll_inDatabase" runat="server" TextMode="MultiLine" Visible="False"></asp:TextBox>
            <asp:TextBox ID="txtTempCardAll" runat="server" TextMode="MultiLine" Visible="False"></asp:TextBox></td>
    </tr>
</table>
