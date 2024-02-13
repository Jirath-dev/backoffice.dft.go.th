<%@ Control language="vb" Inherits="Nti.Modules.DFT_IISPoolManage.ViewDFT_IISPoolManage" AutoEventWireup="false" Explicit="True" Codebehind="ViewDFT_IISPoolManage.ascx.vb" %>
<%@ Register TagPrefix="dnn" TagName="Audit" Src="~/controls/ModuleAuditControl.ascx" %>
<asp:TextBox ID="txtpool" runat="server" Width="325px"></asp:TextBox><br />
<asp:TextBox ID="txtStatus" runat="server" Height="101px" TextMode="MultiLine" Width="345px"></asp:TextBox>
<asp:Button ID="btnCheckStatus" runat="server" Text="ตรวจสถานะ" />
<asp:Button ID="Button1" runat="server" Text="check status" />
<asp:DropDownList ID="DropDownList1" runat="server">
</asp:DropDownList><br />
<asp:Button ID="Button2" runat="server" Text="Start Pool" />
<asp:Button ID="Button3" runat="server" Text="Stop Pool" />
<asp:Button ID="btnRe" runat="server" Text="Re Pool" />
<asp:Button ID="btnMachineName" runat="server" Text="MachineName" />
<asp:Button ID="Button4" runat="server" Text="recycle pool in list" />
<asp:Button ID="Button5" runat="server" Text="recycle pool in list" />
<asp:Button ID="Button6" runat="server" Text="recycle in file XML" />
<asp:Button ID="Button7" runat="server" Text="Web Administ" />
<br />
<br />
<table border="0" cellpadding="0" cellspacing="0" width="100%">
    <tr class="m-frm-hdr">
        <td>
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr class="m-frm-hdr">
                    <td align="left" class="m-frm-hdr" width="12%">
                        <table border="0" cellpadding="0" cellspacing="0" cols="2">
                            <tr>
                                <td class="groupboxheader" nowrap="nowrap">
                                    <font class="groupbox">&nbsp; หน้าจัดการ Pool &nbsp;</font></td>
                            </tr>
                        </table>
                    </td>
                    <td align="left" class="m-frm-nav" width="88%">
                        &nbsp;&nbsp;
                        <asp:TextBox ID="txtTempNamePool" runat="server"></asp:TextBox>
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
                                <td>
                                </td>
                                <td style="color: #000000">
                                    <font class="FormLabel">List Pool:</font></td>
                                <td class="FormLabel" style="color: #000000">
                                    <asp:DropDownList ID="DDL_ListPool" runat="server" AutoPostBack="True" Width="278px">
                                    </asp:DropDownList>
                                    <asp:Button ID="btnCheckListPoolAgain" runat="server" Text="ตรวจสอบ List Pool อีกครั้ง" /></td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                    <font class="FormLabel"></font>
                                </td>
                                <td class="FormLabel">
                                    &nbsp;<asp:RadioButtonList ID="RDO_ListStatus" runat="server" Width="192px">
                                        <asp:ListItem Selected="True" Value="0">None (ไม่เลือก)</asp:ListItem>
                                        <asp:ListItem Value="1">Start Pool</asp:ListItem>
                                        <asp:ListItem Value="2">Stop Pool</asp:ListItem>
                                        <asp:ListItem Value="3">Recycle Pool</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                    <font class="FormLabel"></font>
                                </td>
                                <td class="FormLabel">
                                    &nbsp;<asp:Button ID="btnResetPoolStatus" runat="server" Text="Reset Status Pool ใหม่" /></td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td class="FormLabel">
                                    &nbsp;<asp:Label ID="lblstatuspool" runat="server" ForeColor="Red"></asp:Label>&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td class="FormLabel">
                                    <asp:Button ID="btnCheckStatusPool" runat="server" Text="ตรวจสอบ Status Pool ตามที่เลือก" /></td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td class="FormLabel">
                                    &nbsp;<asp:Label ID="lblError" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label></td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
