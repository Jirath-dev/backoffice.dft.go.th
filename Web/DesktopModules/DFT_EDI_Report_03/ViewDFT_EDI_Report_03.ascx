<%@ Control language="vb" Inherits="NTi.Modules.DFT_EDI_Report_03.ViewDFT_EDI_Report_03" AutoEventWireup="false" Explicit="True" Codebehind="ViewDFT_EDI_Report_03.ascx.vb" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="rgExportSummary">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="rgExportSummary" />
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
                                                            <font class="groupbox">&#160;&#160;รายงานสถิติการส่งสินค้าออก&#160;&#160;</font></td>
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
                                            <td style="width: 100%">
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
                                                        <td>
                                                            &nbsp;</td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100%">
                                                <font class="FormLabel">ฟอร์ม:</font>
                                                <telerik:RadComboBox ID="rcbFormType" runat="server" DataTextField="form_name"
                                                    DataValueField="form_type" Filter="StartsWith" Font-Names="Tahoma" Font-Size="10pt"
                                                    MarkFirstMatch="True" Skin="Web20" Width="400px">
                                                </telerik:RadComboBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100%">
                                                <font class="FormLabel">พิกัดศุลกากร:</font>&nbsp;<telerik:RadNumericTextBox ID="txtTARIFF_CODE"
                                                    runat="server" Width="130px">
                                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                </telerik:RadNumericTextBox></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100%">
                                                <font class="FormLabel">ประเทศ:</font>
                                                <telerik:RadComboBox ID="rcbCountries" runat="server" DataTextField="country_name"
                                                    DataValueField="country_code" Filter="StartsWith" Font-Names="Tahoma" Font-Size="10pt"
                                                    MarkFirstMatch="True" Skin="Web20" Width="400px">
                                                </telerik:RadComboBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100%">
                                                <table cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td>
                                                            <asp:Panel ID="Panel1" runat="server" GroupingText="ระบบ" BackColor="Transparent" Font-Names="Tahoma" Font-Size="10pt">
                                                            <asp:RadioButtonList ID="rblType" runat="server" RepeatDirection="Horizontal">
                                                                <asp:ListItem Selected="True">EDI</asp:ListItem>
                                                                <asp:ListItem>MANUAL</asp:ListItem>
                                                            </asp:RadioButtonList></asp:Panel>
                                                        </td>
                                                        <td>
                                                            &nbsp;
                                                            <asp:Button ID="btnSearch" runat="server" Width="150px" Text="ค้นหา" />
                                                            <asp:Button ID="btnExportExcel" runat="server" Width="150px" Text="ส่งออกข้อมูล Excel" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                        <table cellpadding="2" cellspacing="2" style="width: 100%" id="tblData" runat="server">
                            <tr>
                                <td style="width: 100%">
                                    <asp:Button ID="btnPrint" runat="server" Text="พิมพ์รายงาน" Visible="False" Width="150px" /></td>
                            </tr>
                            <tr>
                                <td style="width: 100%">
                                    <telerik:RadGrid ID="rgExportSummary" runat="server" AllowPaging="True" AllowSorting="True"
                                        CssClass="GRID-STYLE" GridLines="None" Skin="Office2007" Font-Names="Tahoma"
                                        Font-Size="X-Small" TabIndex="-1" PageSize="10" ShowFooter="True">
                                        <MasterTableView AutoGenerateColumns="False" CellSpacing="-1" Width="100%" NoMasterRecordsText="ไม่พบรายการที่ทำการค้นหา">
                                            <GroupByExpressions>
                                                <telerik:GridGroupByExpression>
                                                    <SelectFields>
                                                        <telerik:GridGroupByField FieldAlias="" FieldName="tariff_code" HeaderText="HS."></telerik:GridGroupByField>
                                                    </SelectFields>
                                                    <GroupByFields>
                                                        <telerik:GridGroupByField FieldName="tariff_code" SortOrder="Descending" FieldAlias="tariff_code" HeaderText=""></telerik:GridGroupByField>
                                                    </GroupByFields>
                                                </telerik:GridGroupByExpression>
                                            </GroupByExpressions>
                                            <Columns>
                                                <telerik:GridBoundColumn DataField="destination_country" HeaderText="Country"
                                                    ReadOnly="True" SortExpression="destination_country" UniqueName="destination_country">
                                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt"/>
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="TARIFF_COUNT" HeaderText="Copy"
                                                    ReadOnly="True" SortExpression="TARIFF_COUNT" UniqueName="TARIFF_COUNT">
                                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center"/>
                                                </telerik:GridBoundColumn>
                                                <telerik:GridNumericColumn DataField="QTY_SUM" ReadOnly="True" HeaderText="Net weight"
                                                    SortExpression="QTY_SUM" UniqueName="QTY_SUM" Aggregate="Sum" FooterText="รวม : ">
                                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Right"/>
                                                    <FooterStyle Font-Names="Tahoma" HorizontalAlign="Right" Font-Size="10pt"/>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </telerik:GridNumericColumn>
                                                <telerik:GridNumericColumn DataField="FOB_SUM" ReadOnly="True" HeaderText="FOB Value (US$)"
                                                    SortExpression="FOB_SUM" UniqueName="FOB_SUM" Aggregate="Sum" FooterText="รวม : ">
                                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Right"/>
                                                    <FooterStyle Font-Names="Tahoma" HorizontalAlign="Right" Font-Size="10pt"/>
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
<asp:SqlDataSource ID="SqlCountry" runat="server" ConnectionString="<%$ ConnectionStrings:EDIConnection %>"
    SelectCommand="SELECT * FROM [vCountry] ORDER BY [country_name]"></asp:SqlDataSource>
<asp:SqlDataSource ID="SqlFormType" runat="server" ConnectionString="<%$ ConnectionStrings:EDIConnection %>"
    SelectCommand="select * from FORM_TYPE where FORM_TYPE<>'ALL' order by showorder">
</asp:SqlDataSource>
<asp:Label ID="lblRoleID" runat="server" Visible="False"></asp:Label>
