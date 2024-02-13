<%@ Control language="vb" Inherits="NTi.Modules.DFT_EDI_F04_Report_01.ViewDFT_EDI_F04_Report_01" AutoEventWireup="false" Explicit="True" Codebehind="ViewDFT_EDI_F04_Report_01.ascx.vb" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="rgAllRequestList">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="rgAllRequestList" />
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
                                                            <font class="groupbox">&#160;&#160;รายงานการอนุมัติหนังสือรับรอง&#160;&#160;</font></td>
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
                                                            &nbsp;<asp:Button ID="btnSearch" runat="server" Width="150px" Text="ค้นหา" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                        <table cellpadding="2" cellspacing="2" style="width: 100%">
                            <tr>
                                <td style="width: 100%">
                                    <asp:Button ID="btnPrint" runat="server" Text="พิมพ์รายงาน" Visible="False" Width="150px" />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 100%">
                                    <telerik:RadGrid ID="rgAllRequestList" runat="server" AllowPaging="True" AllowSorting="True"
                                        CssClass="GRID-STYLE" GridLines="None" Skin="Office2007" Font-Names="Tahoma"
                                        Font-Size="X-Small" TabIndex="-1" PageSize="20" ShowFooter="True">
                                        <MasterTableView AutoGenerateColumns="False" CellSpacing="-1" Width="100%" NoMasterRecordsText="ไม่พบรายการที่ทำการค้นหา">
                                            <GroupByExpressions>
                                                <telerik:GridGroupByExpression>
                                                    <SelectFields>
                                                        <telerik:GridGroupByField FieldAlias="form_name" FieldName="form_name" HeaderText=" " FormatString=""></telerik:GridGroupByField>
                                                    </SelectFields>
                                                    <GroupByFields>
                                                        <telerik:GridGroupByField FieldName="form_name" SortOrder="None" FieldAlias="form_name" FormatString=""></telerik:GridGroupByField>
                                                    </GroupByFields>
                                                </telerik:GridGroupByExpression>
                                            </GroupByExpressions>
                                            <Columns>
                                                <telerik:GridTemplateColumn HeaderText="ลำดับ" UniqueName="TemplateColumn">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblIndex" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center"/>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridNumericColumn DataField="company_name" HeaderText="บริษัท"
                                                    ReadOnly="True" SortExpression="company_name" Aggregate="Count" UniqueName="company_name" FooterText="รวม : ">
                                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt"/>
                                                </telerik:GridNumericColumn>
                                                <telerik:GridBoundColumn DataField="reference_code2" HeaderText="เลขที่หนังสือรับรอง"
                                                    ReadOnly="True" SortExpression="reference_code2" UniqueName="reference_code2">
                                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center"/>
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="invh_run_auto" HeaderText="เลขที่คำร้อง"
                                                    ReadOnly="True" SortExpression="invh_run_auto" UniqueName="invh_run_auto">
                                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center"/>
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="approve_date" HeaderText="วันอนุมัติ"
                                                    ReadOnly="True" SortExpression="approve_date" UniqueName="approve_date" DataFormatString="{0:dd/MM/yyyy}">
                                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center"/>
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