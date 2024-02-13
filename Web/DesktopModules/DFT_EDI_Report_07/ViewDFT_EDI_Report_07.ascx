<%@ Control Language="vb" Inherits="NTi.Modules.DFT_EDI_Report_07.ViewDFT_EDI_Report_07"
    AutoEventWireup="false" Explicit="True" Codebehind="ViewDFT_EDI_Report_07.ascx.vb" %>
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
                                                            <font class="groupbox">&#160;&#160;รายงานตรวจสอบการขอหนังสือรับรอง&#160;&#160;</font></td>
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
                                            <td style="width: 100%; text-align: center;">
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
                                                            <font class="FormLabel">&nbsp;สถานที่ออกหนังสือ:&nbsp;</font>
                                                        </td>
                                                        <td>
                                                            <telerik:RadComboBox ID="rcbSitePlus" runat="server" Filter="StartsWith" Font-Names="Tahoma"
                                                                Font-Size="10pt" MarkFirstMatch="True" Skin="Web20" Width="250px">
                                                            </telerik:RadComboBox>
                                                        
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100%; text-align: center;">
                                                    <asp:RadioButtonList ID="rblType" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="0">จำนวนฉบับทั้งหมด</asp:ListItem>
                                                        <asp:ListItem Selected="True" Value="1">จำนวนฉบับ (ไม่รวมการยกเลิกและอื่นๆ)</asp:ListItem>
                                                    </asp:RadioButtonList>
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
                                    <telerik:RadGrid ID="rgCetificateList" runat="server" AllowSorting="True"
                                        CssClass="GRID-STYLE" GridLines="None" Skin="Office2007" Font-Names="Tahoma"
                                        Font-Size="X-Small" TabIndex="-1" PageSize="10" ShowFooter="True">
                                        <MasterTableView ShowGroupFooter="true" AutoGenerateColumns="False" CellSpacing="-1" Width="100%" NoMasterRecordsText="ไม่พบรายการที่ทำการค้นหา">
                                            <GroupByExpressions>
                                                <telerik:GridGroupByExpression>
                                                    <SelectFields>
                                                        <telerik:GridGroupByField FieldAlias="SITE_NAME" FieldName="SITE_NAME" HeaderText="หน่วยงาน  " FormatString=""></telerik:GridGroupByField>
                                                    </SelectFields>
                                                    <GroupByFields>
                                                        <telerik:GridGroupByField FieldName="SITE_NAME" FieldAlias="SITE_NAME" FormatString=""></telerik:GridGroupByField>
                                                    </GroupByFields>
                                                </telerik:GridGroupByExpression>
                                            </GroupByExpressions>
                                            <Columns>
                                                <telerik:GridBoundColumn DataField="FORM_NAME" HeaderText="ชื่อฟอร์ม"
                                                    ReadOnly="True" SortExpression="FORM_NAME" UniqueName="FORM_NAME">
                                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt"/>
                                                </telerik:GridBoundColumn>
                                                <telerik:GridNumericColumn DataField="FORM_COUNT" ReadOnly="True" HeaderText="จำนวน (ฉบับ)"
                                                    SortExpression="FORM_COUNT" UniqueName="FORM_COUNT" Aggregate="Sum" FooterText="รวม : ">
                                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center"/>
                                                    <FooterStyle Font-Names="Tahoma" HorizontalAlign="Center" Font-Size="10pt"/>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </telerik:GridNumericColumn>
                                                <telerik:GridNumericColumn DataField="SUM_FOB" ReadOnly="True" HeaderText="มูลค่า (USD)"
                                                    SortExpression="SUM_FOB" UniqueName="SUM_FOB" Aggregate="Sum" FooterText="รวม : ">
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
<asp:Label ID="lblRoleID" runat="server" Visible="False"></asp:Label>