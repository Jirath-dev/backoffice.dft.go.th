<%@ Control language="vb" Inherits="NTi.Modules.DFT_ReportBy_Site.ViewDFT_ReportBy_Site" AutoEventWireup="false" Explicit="True" Codebehind="ViewDFT_ReportBy_Site.ascx.vb" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="dnn" TagName="Audit" Src="~/controls/ModuleAuditControl.ascx" %>
<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
</telerik:RadAjaxManager>
<br />
<table border="0" cellpadding="0" cellspacing="4" style="width: 100%">
    <tr>
        <td colspan="3">
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
                                            <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
                                                <td style='background-image: url(<%= ResolveUrl("images") %>/CORNER.gif); background-repeat: no-repeat;'>
                                                    &nbsp; &nbsp;&nbsp;</td>
                                            </telerik:RadCodeBlock>
                                        </tr>
                                    </table>
                                    <asp:Label ID="lblRoleID" runat="server" Text="lblRoleID" Visible="False"></asp:Label></td>
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
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="2">
                                    &nbsp;<table border="0" cellpadding="0" cellspacing="2" width="100%">
                                        <tr>
                                            <td style="font-size: 12pt; color: #000000; font-family: Times New Roman">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" class="FormLabel">
                                                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                                                    <tr>
                                                        <td style="width: 100px">
                                                        </td>
                                                        <td style="width: 100px">
                                                        </td>
                                                        <td style="width: 100px">
                                                        </td>
                                                        <td style="width: 100px">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" class="FormLabel" style="width: 250px">
                                                            ตั้งแต่วันที่ :</td>
                                                        <td align="left" class="FormLabel" style="width: 100px">
                                                            <telerik:RadDatePicker ID="rdpFromDate"
                                                    runat="server" Culture="English (United Kingdom)" Font-Names="Tahoma" Font-Size="10pt"
                                                    Skin="Office2007" Width="170px">
                                                    <Calendar DayNameFormat="FirstTwoLetters" FirstDayOfWeek="Sunday" ShowRowHeaders="False"
                                                        Skin="Office2007" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                        ViewSelectorText="x">
                                                    </Calendar>
                                                    <DatePopupButton HoverImageUrl="" ImageUrl="" />
                                                </telerik:RadDatePicker>
                                                        </td>
                                                        <td class="FormLabel" style="width: 8%">
                                                    ถึงวันที่ :</td>
                                                        <td class="FormLabel" style="width: 290px">
                                                    <telerik:RadDatePicker ID="rdpToDate" runat="server" Culture="English (United Kingdom)"
                                                        Font-Names="Tahoma" Font-Size="10pt" Skin="Office2007" Width="170px">
                                                        <Calendar DayNameFormat="FirstTwoLetters" FirstDayOfWeek="Sunday" ShowRowHeaders="False"
                                                            Skin="Office2007" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                            ViewSelectorText="x">
                                                        </Calendar>
                                                        <DatePopupButton HoverImageUrl="" ImageUrl="" />
                                                    </telerik:RadDatePicker>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 100px">
                                                        </td>
                                                        <td style="width: 100px">
                                                        </td>
                                                        <td style="width: 100px">
                                                        </td>
                                                        <td style="width: 100px">
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <table border="0" cellpadding="2" cellspacing="6" style="width: 100%">
                                        <tr>
                                            <td align="center" colspan="1">
                                                <asp:Button ID="btnSearch" runat="server" Text="เรียกรายงาน" ValidationGroup="search" Width="150px" />
                                                    <asp:Button ID="btnExportEX" runat="server" Text="Export Excel" Width="150px" /></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td width="15">
                                </td>
                                <td>
                                    &nbsp;</td>
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
        <td class="FormLabel" colspan="3">
            <span style="color: red">หมายเหตุ :
                <br />
                1. การเรียกรายงานจะไม่สามารถเรียกรายงานของวันนี้ได้ เนื่องจากระบบยังไม่ได้โอนถ่ายข้อมูล
                ถ้าจะเรียกรายงานของวันนี้ ต้องเรียกในวันพรุ่งนี้ครับ<br />
                2. การเรียกรายงานถ้าเลือกจำนวนวันมาก การออกรายงานจะช้าเนื่องจากปริมาณข้อมูลที่ฐานข้อมูลมาก</span></td>
    </tr>
    <tr>
        <td colspan="3">
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;</td>
        <td>
        </td>
        <td>
        </td>
    </tr>
</table>

