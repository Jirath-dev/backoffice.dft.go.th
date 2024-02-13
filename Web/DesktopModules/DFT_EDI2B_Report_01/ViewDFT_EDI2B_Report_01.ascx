<%@ Control Language="vb" Inherits="NTi.Modules.DFT_EDI2B_Report_01.ViewDFT_EDI2B_Report_01"
    AutoEventWireup="false" Explicit="True" CodeBehind="ViewDFT_EDI2B_Report_01.ascx.vb" %>
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
                                                            <font class="groupbox">&#160;&#160;รายงานสถิติการออก e-FORM D / FORM D / Re-print e - FORM D&#160;&#160;</font></td>
                                                        <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
                                                            <td style="background-image: url(<%= ResolveUrl("images") %>/CORNER.gif); background-repeat: no-repeat;">&#160;&#160;&#160;&#160;</td>
                                                        </telerik:RadCodeBlock>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td align="right" class="m-frm-nav"></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr class="m-frm-hdr">
                                <td>
                                    <table style="width: 100%" class="FormLabel">
                                        <tr>
                                            <td style="width: 10%">ตั้งแต่วันที่:</td>
                                            <td style="text-align: center;">
                                                <table cellpadding="0" cellspacing="0" dwcopytype="CopyTableColumn">
                                                    <tr>

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
                                            <td>สาขา:</td>
                                            <td>
                                                <asp:DropDownList ID="ddlSiteID" runat="server"></asp:DropDownList>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td></td>
                                            <td>
                                                <asp:Button ID="btnSearch" runat="server" Width="150px" Text="แสดงผล" Font-Names="Tahoma" Font-Size="10pt" />
                                                <asp:Button ID="btnExcel" runat="server" Text="Export (Excel)" Visible="true" Width="150px" Font-Names="Tahoma" Font-Size="10pt" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                        <table cellpadding="2" cellspacing="2" style="width: 100%">
                    
                            <tr>
                                <td style="width: 100%">
                                    <telerik:RadGrid ID="grdData" runat="server" AllowSorting="True"
                                        CssClass="GRID-STYLE" GridLines="None" Skin="Office2007" Font-Names="Tahoma"
                                        Font-Size="X-Small" TabIndex="-1" PageSize="10" ShowFooter="True">
                                        <MasterTableView AutoGenerateColumns="False" CellSpacing="-1" Width="100%" NoMasterRecordsText="ไม่พบรายการที่ทำการค้นหา">

                                            <Columns>
                                                <telerik:GridBoundColumn DataField="country_name" HeaderText="ประเทศปลายทาง"
                                                    ReadOnly="True" SortExpression="country_name" UniqueName="country_name">
                                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridNumericColumn DataField="Printed" ReadOnly="True" HeaderText="FORM D (ฉบับ)" HeaderStyle-Width="20%"
                                                    SortExpression="Printed" UniqueName="Printed" Aggregate="Sum" FooterText="รวม : " DataFormatString="{0:N0}">
                                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Right" />
                                                    <FooterStyle Font-Names="Tahoma" HorizontalAlign="Right" Font-Size="10pt" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </telerik:GridNumericColumn>
                                                <telerik:GridNumericColumn DataField="NotPrint" ReadOnly="True" HeaderText="e-FORM D (ฉบับ)" HeaderStyle-Width="20%"
                                                    SortExpression="NotPrint" UniqueName="NotPrint" Aggregate="Sum" FooterText="รวม : " DataFormatString="{0:N0}">
                                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Right" />
                                                    <FooterStyle Font-Names="Tahoma" HorizontalAlign="Right" Font-Size="10pt" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </telerik:GridNumericColumn>
                                                <telerik:GridNumericColumn DataField="RepPrint" ReadOnly="True" HeaderText="e-FORM D (Reprint) (ฉบับ)" HeaderStyle-Width="20%"
                                                    SortExpression="RepPrint" UniqueName="RepPrint" Aggregate="Sum" FooterText="รวม : " DataFormatString="{0:N0}">
                                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Right" />
                                                    <FooterStyle Font-Names="Tahoma" HorizontalAlign="Right" Font-Size="10pt" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </telerik:GridNumericColumn>
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