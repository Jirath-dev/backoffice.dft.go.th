<%@ Control Language="vb" Inherits="YourCompany.Modules.DFT_ChangeApproveDate.ViewDFT_ChangeApproveDate"
    AutoEventWireup="false" Explicit="True" Codebehind="ViewDFT_ChangeApproveDate.ascx.vb" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="dnn" TagName="Audit" Src="~/controls/ModuleAuditControl.ascx" %>
<%--<table width="100%" border="0" cellspacing="3" cellpadding="0">
    <tr>
        <td>
            <table cellspacing="0" border="0" class="m-frm-bdr" cellpadding="1" width="100%">
                <tr>
                    <td>--%>
                        <table cellspacing="1" cellpadding="0" class="m-frm-bdr" width="100%" border="0">
                            <tr class="m-frm-hdr">
                                <td>
                                    <table cellspacing="0" cellpadding="0" border="0" width="100%">
                                        <tr class="m-frm-hdr">
                                            <td width="88%" align="left" class="m-frm-hdr">
                                                <table cols="2" border="0" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td nowrap class="groupboxheader">
                                                            <font class="groupbox">&#160;&#160;แก้ไขวันอนุมัติ&#160;&#160;</font></td>
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
                                            <td>
                                                <table border="0" cellpadding="0" cellspacing="2">
                                                    <tr>
                                                        <td>
                                                            <font class="FormLabel">หมายเลขคำร้อง :</font>&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtSearch"
                                                                runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="12pt" ForeColor="Blue"></asp:TextBox></td>
                                                        <td>
                                                            <font class="FormLabel"></font>
                                                            <asp:CheckBox ID="chkUseRef2" runat="server" Font-Names="Tahoma" Font-Size="10pt"
                                                                Text="ใช้เลขที่หนังสือรับรอง" /></td>
                                                        <td>
                                                            &nbsp;
                                                            <asp:Button ID="btnSearchForm" runat="server" Text="ค้นหา" Width="150px" />
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
                                    <asp:Panel ID="PanelDetail" runat="server" Width="100%" Visible="false">
                                    <table id="Table1" runat="server" border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                                        <tr>
                                        <td width="15" style="height: 55px" rowspan="7">
                                        </td>
                                            <td class="FormLabel" style="width: 136px; height: 20px;">
                                                เลขที่อ้างอิง</td>
                                            <td class="FormLabel" style="height: 20px">
                                                <asp:Label ID="lblinvRun" runat="server" Font-Bold="True" ForeColor="Blue"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td class="FormLabel" style="width: 136px; height: 20px;">
                                                เลขที่หนังสือรับรอง</td>
                                            <td class="FormLabel" style="height: 20px">
                                                <asp:Label ID="lblReferenceCode2" runat="server" Font-Bold="True" ForeColor="Blue"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td class="FormLabel" style="width: 136px; height: 20px;">
                                                ชื่อบริษัท</td>
                                            <td class="FormLabel" style="height: 20px">
                                                <asp:Label ID="lblCompanyname" runat="server" Font-Bold="True" ForeColor="Blue"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td class="FormLabel" style="width: 136px">
                                                ฟอร์ม</td>
                                            <td class="FormLabel">
                                                <asp:Label ID="lblForm_Type" runat="server" Font-Bold="True" ForeColor="Blue"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td class="FormLabel" style="width: 136px; height: 24px">
                                                สาขา</td>
                                            <td class="FormLabel" style="height: 24px">
                                                <asp:Label ID="lblSiteName" runat="server" Font-Bold="True" ForeColor="Blue"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td class="FormLabel" style="width: 136px; height: 23px;">
                                                วันที่อนุมัติ</td>
                                            <td class="FormLabel" colspan="2" style="height: 23px">
                                                <telerik:RadDatePicker ID="rdpApproveDate" runat="server" Font-Bold="True" ForeColor="Blue">
                                                    <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                                                    </Calendar>
                                                    <DatePopupButton HoverImageUrl="" ImageUrl="" />
                                                    <DateInput DateFormat="dd/MM/yyyy">
                                                    </DateInput>
                                                </telerik:RadDatePicker>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="FormLabel" style="width: 136px; height: 24px;">
                                            </td>
                                            <td class="FormLabel" style="height: 50px">
                                                <asp:Button ID="btnChangeApprove" runat="server" Text="บันทึกการแก้ไขวันอนุมัติ" Width="150px" /></td>
                                        </tr>
                                    </table>
                                    </asp:Panel>
                                    
                                </td>
                            </tr>
                        </table>
<%--                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>--%>
<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
</telerik:RadAjaxManager>
