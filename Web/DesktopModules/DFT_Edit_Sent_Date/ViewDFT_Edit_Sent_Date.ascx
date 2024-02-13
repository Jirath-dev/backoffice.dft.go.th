<%@ Control Language="vb" Inherits="YourCompany.Modules.DFT_Edit_Sent_Date.ViewDFT_Edit_Sent_Date"
    AutoEventWireup="false" Explicit="True" Codebehind="ViewDFT_Edit_Sent_Date.ascx.vb" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
</telerik:RadAjaxManager>
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
                                                <table cols="2" border="0" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td nowrap class="groupboxheader">
                                                            <font class="groupbox">&#160;&#160;รายงานสรุปการออกใบเสร็จรับเงิน&#160;&#160;</font>
                                                        </td>
                                                        <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
                                                            <td style="background-image: url(<%= ResolveUrl("images") %>/CORNER.gif); background-repeat: no-repeat;">
                                                                &#160;&#160;&#160;&#160;
                                                            </td>
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
                            <tr class="m-frm-hdr">
                                <td>
                                    <table style="width: 100%">
                                        <tr>
                                            <td style="width: 100%">
                                                <table cellpadding="0" cellspacing="0" style="width: 100%;">
                                                    <tr>
                                                        <td style="width: 17%" align="right">
                                                            <font class="FormLabel">เลขที่อ้างอิง :&nbsp;</font>
                                                        </td>
                                                        <td style="width: 33%; height: 30px;" align="left">
                                                            <telerik:RadTextBox ID="txtInvRunAuto" runat="server" Width="200px" Font-Names="Tahoma"
                                                                Font-Size="12pt">
                                                            </telerik:RadTextBox>
                                                        </td>
                                                        <td style="width: 50%">
                                                            <asp:Button ID="btnSearch" runat="server" Width="150px" Text="ค้นหา"></asp:Button>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 17%" align="right">
                                                            <font class="FormLabel">เลขประจำตัวผู้เสียภาษี :&nbsp;</font>
                                                        </td>
                                                        <td style="width: 33%; height: 30px;" align="left">
                                                            <telerik:RadTextBox ID="txtCompanyTaxno" runat="server" Width="200px" Font-Names="Tahoma"
                                                                Font-Size="12pt" Enabled="false">
                                                            </telerik:RadTextBox>
                                                        </td>
                                                        <td style="width: 50%">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 17%" align="right">
                                                            <font class="FormLabel">ชื่อบริษัท :&nbsp;</font>
                                                        </td>
                                                        <td colspan="2" style="height: 30px;">
                                                            <telerik:RadTextBox ID="txtCompanyName" runat="server" Width="800px" Font-Names="Tahoma"
                                                                Font-Size="12pt" Enabled="false">
                                                            </telerik:RadTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 17%" align="right">
                                                            <font class="FormLabel">ประเภทฟอร์ม :&nbsp;</font>
                                                        </td>
                                                        <td style="width: 33%; height: 30px;" align="left">
                                                            <telerik:RadTextBox ID="txtFormName" runat="server" Width="500px" Font-Names="Tahoma"
                                                                Font-Size="12pt" Enabled="false">
                                                            </telerik:RadTextBox>
                                                        </td>
                                                        <td style="width: 50%">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 17%" align="right">
                                                            <font class="FormLabel">วันที่ส่ง :&nbsp;</font>
                                                        </td>
                                                        <td style="width: 33%; height: 30px;" align="left">
                                                            <telerik:RadDatePicker ID="rdpSentDate" runat="server" Culture="English (United Kingdom)"
                                                                Width="170px" Font-Names="Tahoma" Font-Size="10pt" Skin="Outlook">
                                                                <Calendar Skin="Outlook" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                                    ViewSelectorText="x" DayNameFormat="FirstTwoLetters" FirstDayOfWeek="Sunday"
                                                                    ShowRowHeaders="False">
                                                                </Calendar>
                                                                <DatePopupButton HoverImageUrl="" ImageUrl="" />
                                                            </telerik:RadDatePicker>
                                                        </td>
                                                        <td style="width: 50%">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 17%" align="left" colspan="2" >
                                                            <asp:Label ID="Label1" runat="server" Text="หมายเหตุ : วันที่ส่งฟอร์มต้องไม่เกิน 5 วันของวันที่ปัจจุบัน" ForeColor="red" Font-Bold="true"
                                                                Font-Names="Tahoma" Font-Size="10pt">
                                                            </asp:Label>
                                                        </td>
                                                        <td style="width: 50%">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 17%; height: 30px" align="right">
                                                        </td>
                                                        <td style="width: 33%" align="center">
                                                            <asp:Button ID="btnSave" runat="server" Text="บันทึก" Width="150px" Enabled="false"/>
                                                        </td>
                                                        <td style="width: 50%">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 17%" align="right">
                                                        </td>
                                                        <td style="width: 33%" align="center">
                                                            <asp:Label ID="lblErrors" runat="server" Text="" Font-Bold="true" Font-Names="Tahoma"
                                                                Font-Size="10pt">
                                                            </asp:Label>
                                                        </td>
                                                        <td style="width: 50%">
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
        </td>
    </tr>
</table>
