<%@ Control language="vb" Inherits="NTi.Modules.DFT_EDI_Report_10.ViewDFT_EDI_Report_10" AutoEventWireup="false" Explicit="True" Codebehind="ViewDFT_EDI_Report_10.ascx.vb" %>
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
                                                            <font class="groupbox">&#160;&#160;รายงานข้อมูล Not-Found และมูลค่าเกิน 600,000.00&#160;&#160;</font></td>
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
                                            <td style="width: 100%;">
                                                <table cellpadding="0" cellspacing="0" dwcopytype="CopyTableColumn">
                                                    <tr>
                                                        <td>
                                                            <font class="FormLabel">&nbsp;ประจำวันที่:&nbsp;</font></td>
                                                        <td>
                                                            <telerik:RadDatePicker ID="rdpFromDate" runat="server" Skin="Office2007" Culture="English (United Kingdom)" Width="170px" Font-Names="Tahoma" Font-Size="10pt">
                                                                <Calendar Skin="Office2007" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                                    ViewSelectorText="x" DayNameFormat="FirstTwoLetters" FirstDayOfWeek="Sunday" ShowRowHeaders="False">
                                                                </Calendar>
                                                                <DatePopupButton HoverImageUrl="" ImageUrl="" />
                                                            </telerik:RadDatePicker>
                                                        </td>
                                                        <td>
                                                            <font class="FormLabel">&nbsp;<asp:Button ID="btnSearch" runat="server" Width="150px" Text="แสดงผล" /></font>
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
                                    <telerik:RadGrid ID="rgCetificateList" runat="server" AllowSorting="True"
                                        CssClass="GRID-STYLE" GridLines="None" Skin="Office2007" Font-Names="Tahoma"
                                        Font-Size="X-Small" TabIndex="-1" PageSize="20" ShowFooter="True" AllowPaging="True">
                                        <MasterTableView AutoGenerateColumns="False" Width="100%" NoMasterRecordsText="ไม่พบรายการที่ทำการค้นหา">
                                            <Columns>
                                                <telerik:GridNumericColumn DataField="invh_run_auto" ReadOnly="True" HeaderText="เลขที่อ้างอิง"
                                                    SortExpression="invh_run_auto" UniqueName="invh_run_auto" Aggregate="Count" FooterText="รวม : ">
                                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center"/>
                                                    <FooterStyle Font-Names="Tahoma" Font-Size="10pt"/>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </telerik:GridNumericColumn>
                                                <telerik:GridBoundColumn DataField="invd_run_auto" HeaderText="ลำดับที่"
                                                    ReadOnly="True" SortExpression="invd_run_auto" UniqueName="invd_run_auto">
                                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center"/>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="site_id" HeaderText="สถานที่ออกหนังสือ"
                                                    ReadOnly="True" SortExpression="site_id" UniqueName="site_id">
                                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt"/>
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Tariff_code" HeaderText="พิกัด"
                                                    ReadOnly="True" SortExpression="Tariff_code" UniqueName="Tariff_code">
                                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt"/>
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Product_description" HeaderText="รายละเอียด"
                                                    ReadOnly="True" SortExpression="Product_description" UniqueName="Product_description">
                                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt"/>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridNumericColumn DataField="fob_amt" ReadOnly="True" HeaderText="มูลค่า"
                                                    SortExpression="fob_amt" UniqueName="fob_amt" Aggregate="Sum" FooterText="รวม : ">
                                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Right"/>
                                                    <FooterStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Right"/>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </telerik:GridNumericColumn>
                                                <telerik:GridNumericColumn DataField="net_weight" ReadOnly="True" HeaderText="น้ำหนักสุทธิ"
                                                    SortExpression="net_weight" UniqueName="net_weight" Aggregate="Sum" FooterText="รวม : ">
                                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Right"/>
                                                    <FooterStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Right"/>
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
<asp:SqlDataSource ID="SqlForm_Type" runat="server" ConnectionString="<%$ ConnectionStrings:OriginConnection %>"
    SelectCommand="SELECT [form_type], [form_name] FROM [form_type] WHERE ([form_type] <> @form_type) ORDER BY [ShowOrder]">
    <SelectParameters>
        <asp:Parameter DefaultValue="ALL" Name="form_type" Type="String" />
    </SelectParameters>
</asp:SqlDataSource>
<asp:Label ID="lblRoleID" runat="server" Visible="False"></asp:Label>