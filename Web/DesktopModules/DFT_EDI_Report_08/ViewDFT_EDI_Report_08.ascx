<%@ Control language="vb" Inherits="NTi.Modules.DFT_EDI_Report_08.ViewDFT_EDI_Report_08" AutoEventWireup="false" Explicit="True" Codebehind="ViewDFT_EDI_Report_08.ascx.vb" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="rgCetificateList">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="rgCetificateList" />
            </UpdatedControls>
        </telerik:AjaxSetting>
    </AjaxSettings>
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
                                                            <font class="groupbox">&#160;&#160;รายงานตรวจสอบการออกเลขที่หนังสือรับรองถิ่นกำเนิดสินค้า&#160;&#160;</font></td>
                                                        <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
                                                            <td style="background-image: url(<%= ResolveUrl("images") %>/CORNER.gif); background-repeat: no-repeat;">
                                                                &#160;&#160;&#160;&#160;</td>
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
                                            <td style="width: 100%; text-align: left;">
                                                <table cellpadding="0" cellspacing="0" dwcopytype="CopyTableColumn">
                                                    <tr>
                                                        <td>
                                                            <font class="FormLabel">&nbsp;ตั้งแต่วันที่:&nbsp;</font></td>
                                                        <td>
                                                            <telerik:RadDatePicker ID="rdpFromDate" runat="server" Skin="Office2007" Culture="English (United Kingdom)" Width="170px" Font-Names="Tahoma" Font-Size="10pt">
                                                                <Calendar Skin="Office2007" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                                    ViewSelectorText="x" DayNameFormat="FirstTwoLetters" FirstDayOfWeek="Sunday" ShowRowHeaders="False">
                                                                </Calendar>
                                                                <DatePopupButton HoverImageUrl="" ImageUrl="" />
                                                            </telerik:RadDatePicker>
                                                        </td>
                                                        <td>
                                                            <font class="FormLabel">&nbsp;ถึงวันที่:&nbsp;</font>
                                                        </td>
                                                        <td>
                                                            <telerik:RadDatePicker ID="rdpToDate" runat="server" Skin="Office2007" Culture="English (United Kingdom)" Width="170px" Font-Names="Tahoma" Font-Size="10pt">
                                                                <Calendar Skin="Office2007" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                                    ViewSelectorText="x" DayNameFormat="FirstTwoLetters" FirstDayOfWeek="Sunday" ShowRowHeaders="False">
                                                                </Calendar>
                                                                <DatePopupButton HoverImageUrl="" ImageUrl="" />
                                                            </telerik:RadDatePicker>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100%; text-align: left;">
                                                <font class="FormLabel">ฟอร์ม :&nbsp;</font>
                                                <telerik:RadComboBox ID="rcbFormType" runat="server" DataTextField="form_name" DataValueField="form_type"
                                                    Filter="StartsWith" Font-Names="Tahoma" Font-Size="10pt" MarkFirstMatch="True"
                                                    Skin="Web20" Width="400px">
                                                </telerik:RadComboBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100%; text-align: left">
                                                <font class="FormLabel">
                                                    <asp:RadioButtonList ID="rblRPT_TYPE" runat="server">
                                                        <asp:ListItem Value="0">รายการที่ยังไม่ได้ดำเนินการ</asp:ListItem>
                                                        <asp:ListItem Value="1">รายการที่ไม่อนุมัติ</asp:ListItem>
                                                        <asp:ListItem Value="2" Selected="True">รายการที่อนุมัติเรียบร้อยแล้ว</asp:ListItem>
                                                        <asp:ListItem Value="3">รายการที่ยกเลิก</asp:ListItem>
                                                        <asp:ListItem Value="4">รายการที่อนุมัติ/ยกเลิก</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </font>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100%; text-align: center;">
                                                            <asp:Button ID="btnSearch" runat="server" Width="150px" Text="แสดงผล" /></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                        <table id="tblData" runat="server" cellpadding="2" cellspacing="2" style="width: 100%">
                            <tr>
                                <td style="width: 100%">
                                    <asp:Button ID="btnPrint" runat="server" Text="พิมพ์รายงาน" Visible="False" Width="150px" /></td>
                            </tr>
                            <tr>
                                <td style="width: 100%">
                                    <telerik:RadGrid ID="rgCetificateList" runat="server"
                                        CssClass="GRID-STYLE" GridLines="None" Skin="Office2007" Font-Names="Tahoma"
                                        Font-Size="X-Small" TabIndex="-1" PageSize="20" ShowFooter="True" AllowPaging="True">
                                        <MasterTableView ShowGroupFooter="True" AutoGenerateColumns="False" Width="100%" NoMasterRecordsText="ไม่พบรายการที่ทำการค้นหา">
                                            <Columns>
                                                <telerik:GridBoundColumn DataField="invh_run_auto" HeaderText="เลขที่อ้างอิง"
                                                    ReadOnly="True" SortExpression="invh_run_auto" UniqueName="invh_run_auto">
                                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt"/>
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="company_name" HeaderText="บริษัท"
                                                    ReadOnly="True" SortExpression="company_name" UniqueName="company_name">
                                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt"/>
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="site_id" HeaderText="สถานที่"
                                                    ReadOnly="True" SortExpression="site_id" UniqueName="site_id">
                                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt"/>
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="edi_status" HeaderText="สถานะ"
                                                    ReadOnly="True" SortExpression="edi_status" UniqueName="edi_status">
                                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt"/>
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="reference_code2" HeaderText="เลขที่หนังสือรับรอง"
                                                    ReadOnly="True" SortExpression="reference_code2" UniqueName="reference_code2">
                                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt"/>
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="reason" HeaderText="หมายเหตุ"
                                                    ReadOnly="True" SortExpression="reason" UniqueName="reason">
                                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt"/>
                                                </telerik:GridBoundColumn>
                                            </Columns>
                                        </MasterTableView>
                                        <HeaderStyle HorizontalAlign="Center" Font-Names="Tahoma" Font-Size="10pt" Font-Underline="False" />
                                        <ClientSettings EnableRowHoverStyle="True">
                                            <Selecting AllowRowSelect="True" />
                                        </ClientSettings>
                                    </telerik:RadGrid>
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