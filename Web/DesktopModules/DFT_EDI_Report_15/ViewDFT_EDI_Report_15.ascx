<%@ Control Language="vb" Inherits="YourCompany.Modules.DFT_EDI_Report_15.ViewDFT_EDI_Report_15" AutoEventWireup="false" Explicit="True" CodeBehind="ViewDFT_EDI_Report_15.ascx.vb" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%--<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="rgCetificateList">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="rgCetificateList" />
            </UpdatedControls>
        </telerik:AjaxSetting>
    </AjaxSettings>
</telerik:RadAjaxManager>--%>
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
                                                            <font class="groupbox">&#160;&#160;รายงานการออกหนังสือรับรองถิ่นกำเนิดสินค้า&#160;&#160;ระบบ EDI DS & XML</font></td>
                                                        <telerik:radcodeblock id="RadCodeBlock1" runat="server">
                                                            <td style="background-image: url(<%= ResolveUrl("images") %>/CORNER.gif); background-repeat: no-repeat;">
                                                                &#160;&#160;&#160;&#160;</td>
                                                        </telerik:radcodeblock>
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
                                    <table style="width: 100%">
                                        <tr>
                                            <td style="width: 100%; text-align: center;">
                                                <table cellpadding="0" cellspacing="0" dwcopytype="CopyTableColumn">
                                                    <tr>
                                                        <td>
                                                            <font class="FormLabel">&nbsp;ตั้งแต่วันที่:&nbsp;</font></td>
                                                        <td>
                                                            <telerik:raddatepicker id="rdpFromDate" runat="server" skin="Office2007" culture="English (United Kingdom)" width="170px" font-names="Tahoma" font-size="10pt">
                                                                <Calendar Skin="Office2007" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                                    ViewSelectorText="x" DayNameFormat="FirstTwoLetters" FirstDayOfWeek="Sunday" ShowRowHeaders="False">
                                                                </Calendar>
                                                                <DatePopupButton HoverImageUrl="" ImageUrl="" />
                                                            </telerik:raddatepicker>
                                                        </td>
                                                        <td>
                                                            <font class="FormLabel">&nbsp;ถึงวันที่:&nbsp;</font>
                                                        </td>
                                                        <td>
                                                            <telerik:raddatepicker id="rdpToDate" runat="server" skin="Office2007" culture="English (United Kingdom)" width="170px" font-names="Tahoma" font-size="10pt">
                                                                <Calendar Skin="Office2007" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                                    ViewSelectorText="x" DayNameFormat="FirstTwoLetters" FirstDayOfWeek="Sunday" ShowRowHeaders="False">
                                                                </Calendar>
                                                                <DatePopupButton HoverImageUrl="" ImageUrl="" />
                                                            </telerik:raddatepicker>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>

                                    </table>
                                </td>
                            </tr>
                        </table>
                        <table>
                            <tr class="m-frm-hdr">
                                <td>
                                    <font class="FormLabel">&nbsp;ระบบงาน :&nbsp;</font>
                                </td>
                                <td style="text-align: center;">
                                    <asp:RadioButtonList ID="rblType" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem Value="0">EDI</asp:ListItem>
                                        <asp:ListItem Selected="True" Value="1">DS</asp:ListItem>
                                        <asp:ListItem Value="2">XML</asp:ListItem>
                                        <asp:ListItem Value="3">ทั้งหมด</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td>
                                    <font class="FormLabel">&nbsp;หน่วยงาน :&nbsp;</font>
                                </td>
                                <td>
                                    <telerik:radcombobox id="ddlSiteID" runat="server" allowcustomtext="True" dropdownwidth="300px"
                                        emptymessage="-----กรุณาลือกสถานที่-----" maxheight="200px" width="220px" filter="Contains">
                                                </telerik:radcombobox>
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td></td>
                                <td style="text-align: left;">
                                    <asp:Button ID="btnSearch" runat="server" Width="150px" Text="แสดงผล" Font-Names="Tahoma" Font-Size="10pt" />
                                </td>
                                <td>
                                    <asp:Button ID="btnPrint" runat="server" Text="พิมพ์รายงาน" Visible="False" Width="150px" Font-Names="Tahoma" Font-Size="10pt" />
                                </td>
                            </tr>
                        </table>
                        <table cellpadding="2" cellspacing="2" style="width: 100%">

                            <tr>
                                <td style="width: 100%">
                                    <telerik:radgrid id="rgCetificateList" runat="server" allowsorting="True"
                                        cssclass="GRID-STYLE" gridlines="None" skin="Office2007" font-names="Tahoma"
                                        font-size="X-Small" tabindex="-1" pagesize="10" showfooter="True">
                                        <MasterTableView AutoGenerateColumns="False" CellSpacing="-1" Width="100%" NoMasterRecordsText="ไม่พบรายการที่ทำการค้นหา">
                                            
                                            <Columns>
                                                <telerik:GridBoundColumn DataField="form_name" HeaderText="ชื่อฟอร์ม"
                                                    ReadOnly="True" SortExpression="form_name" UniqueName="form_name">
                                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt"/>
                                                </telerik:GridBoundColumn>
                                                <telerik:GridNumericColumn DataField="form_count" ReadOnly="True" HeaderText="จำนวน (ฉบับ)"
                                                    SortExpression="form_count" UniqueName="form_count" Aggregate="Sum" FooterText="รวม : ">
                                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Right"/>
                                                    <FooterStyle Font-Names="Tahoma" HorizontalAlign="Right" Font-Size="10pt"/>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </telerik:GridNumericColumn>
                                                <telerik:GridNumericColumn DataField="fob_total" ReadOnly="True" HeaderText="มูลค่า (USD)"
                                                    SortExpression="fob_total" UniqueName="fob_total" Aggregate="Sum" FooterText="รวม : " DataFormatString="{0:N4}">
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
                                    </telerik:radgrid>
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