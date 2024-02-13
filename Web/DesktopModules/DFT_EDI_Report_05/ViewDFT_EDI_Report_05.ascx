<%@ Control language="vb" Inherits="NTi.Modules.DFT_EDI_Report_05.ViewDFT_EDI_Report_05" AutoEventWireup="false" Explicit="True" Codebehind="ViewDFT_EDI_Report_05.ascx.vb" %>
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
                                                            <font class="groupbox">&#160;&#160;รายงานตรวจสอบการจ่ายออกหนังสือรับรอง&#160;&#160;</font></td>
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
                                            <td style="width: 100%">
                                                <font class="FormLabel">&nbsp;ฟอร์ม:</font>
                                                <telerik:RadComboBox ID="rcbForm_Type" runat="server" DataSourceID="SqlForm_Type"
                                                    DataTextField="form_name" DataValueField="form_type" Filter="StartsWith" MarkFirstMatch="True"
                                                    Skin="Web20" Width="400px">
                                                </telerik:RadComboBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100%;">
                                                <asp:Panel ID="Panel1" runat="server" BackColor="Transparent" Font-Names="Tahoma" Font-Size="10pt" GroupingText="สถานะ" Width="350px">
                                                    <asp:RadioButtonList ID="rblType" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="0">ยังไม่ได้จ่ายออก</asp:ListItem>
                                                        <asp:ListItem Selected="True" Value="1">จ่ายออกเรียบร้อยแล้ว</asp:ListItem>
                                                    </asp:RadioButtonList></asp:Panel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100%;">
                                                <asp:Button ID="btnSearch" runat="server" Width="150px" Text="แสดงผล" /></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                        <table cellpadding="2" cellspacing="2" style="width: 100%">
                            <tr>
                                <td style="width: 100%">
                                    <asp:Button ID="btnPrint" runat="server" Text="พิมพ์รายงาน" Visible="False" Width="150px" /></td>
                            </tr>
                            <tr>
                                <td style="width: 100%">
                                    <telerik:RadGrid ID="rgCetificateList" runat="server" AllowSorting="True"
                                        CssClass="GRID-STYLE" GridLines="None" Skin="Office2007" Font-Names="Tahoma"
                                        Font-Size="X-Small" TabIndex="-1" PageSize="10" ShowFooter="True">
                                        <MasterTableView AutoGenerateColumns="False" CellSpacing="-1" Width="100%" NoMasterRecordsText="ไม่พบรายการที่ทำการค้นหา">
                                            <Columns>
                                                <telerik:GridNumericColumn DataField="invh_run_auto" ReadOnly="True" HeaderText="เลขที่อ้างอิง"
                                                    SortExpression="invh_run_auto" UniqueName="invh_run_auto" Aggregate="Count" FooterText="รวม : ">
                                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center"/>
                                                    <FooterStyle Font-Names="Tahoma" Font-Size="10pt"/>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </telerik:GridNumericColumn>
                                                <telerik:GridBoundColumn DataField="company_name" HeaderText="บริษัท"
                                                    ReadOnly="True" SortExpression="company_name" UniqueName="company_name">
                                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt"/>
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="distribute_by" HeaderText="ผู้จ่าย"
                                                    ReadOnly="True" SortExpression="distribute_by" UniqueName="distribute_by">
                                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt"/>
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="reference_code2" HeaderText="เลขที่หนังสือรับรองฯ"
                                                    ReadOnly="True" SortExpression="reference_code2" UniqueName="reference_code2">
                                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center"/>
                                                    <HeaderStyle HorizontalAlign="Center" />
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
<asp:SqlDataSource ID="SqlForm_Type" runat="server" ConnectionString="<%$ ConnectionStrings:OriginConnection %>"
    SelectCommand="SELECT [form_type], [form_name] FROM [form_type] WHERE ([form_type] <> @form_type) ORDER BY [ShowOrder]">
    <SelectParameters>
        <asp:Parameter DefaultValue="ALL" Name="form_type" Type="String" />
    </SelectParameters>
</asp:SqlDataSource>
<asp:Label ID="lblRoleID" runat="server" Visible="False"></asp:Label>
