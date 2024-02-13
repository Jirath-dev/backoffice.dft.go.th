<%@ Control Language="vb" Inherits="NTi.Modules.DFT_EDI_Report_14.ViewDFT_EDI_Report_14"
    AutoEventWireup="false" Explicit="True" CodeBehind="ViewDFT_EDI_Report_14.ascx.vb" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="btnSearch">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="rgCetificateList" />
            </UpdatedControls>
        </telerik:AjaxSetting>
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
                                                            <font class="groupbox">&#160;&#160;รายงานการออกหนังสือรับรองถิ่นกำเนิดสินค้า&#160;&#160;</font>
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
                                                <table cellpadding="0" cellspacing="0" dwcopytype="CopyTableColumn">
                                                    <tr>
                                                        <td>
                                                            <font class="FormLabel">&nbsp;บริษัท :&nbsp;</font>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtCompany_Name" runat="server" Font-Names="Tahoma" 
                                                                Font-Size="10pt" Width="300px"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <font class="FormLabel">&nbsp;ประเภทฟอร์ม :&nbsp;</font>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlFormType" runat="server" Font-Names="Tahoma" 
                                                                Font-Size="10pt">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100%">
                                                <table cellpadding="0" cellspacing="0" dwcopytype="CopyTableColumn">
                                                    <tr>
                                                        <td>
                                                            <font class="FormLabel">วันอนุมัติตั้งแต่ :&nbsp;</font>
                                                        </td>
                                                        <td>
                                                            <telerik:RadDatePicker ID="rdpFromDate" runat="server" Skin="Office2007" Culture="English (United Kingdom)"
                                                                Width="170px" Font-Names="Tahoma" Font-Size="10pt">
                                                                <Calendar Skin="Office2007" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                                    ViewSelectorText="x" DayNameFormat="FirstTwoLetters" FirstDayOfWeek="Sunday"
                                                                    ShowRowHeaders="False">
                                                                </Calendar>
                                                                <DatePopupButton HoverImageUrl="" ImageUrl="" />
                                                            </telerik:RadDatePicker>
                                                        </td>
                                                        <td>
                                                            <font class="FormLabel">&nbsp;ถึงวันที่:&nbsp;</font>
                                                        </td>
                                                        <td>
                                                            <telerik:RadDatePicker ID="rdpToDate" runat="server" Skin="Office2007" Culture="English (United Kingdom)"
                                                                Width="170px" Font-Names="Tahoma" Font-Size="10pt">
                                                                <Calendar Skin="Office2007" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                                    ViewSelectorText="x" DayNameFormat="FirstTwoLetters" FirstDayOfWeek="Sunday"
                                                                    ShowRowHeaders="False">
                                                                </Calendar>
                                                                <DatePopupButton HoverImageUrl="" ImageUrl="" />
                                                            </telerik:RadDatePicker>
                                                        </td>
                                                        <td>
                                                            <font class="FormLabel">&nbsp;พิกัด :&nbsp;</font>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtTariff_Code" runat="server" Font-Names="Tahoma" 
                                                                Font-Size="10pt"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100%; text-align: center;">
                                                <asp:Button ID="btnSearch" runat="server" Width="150px" Text="แสดงผล" Font-Names="Tahoma"
                                                    Font-Size="10pt" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                        <table cellpadding="2" cellspacing="2" style="width: 100%">
                            <tr>
                                <td style="width: 100%">
                                    <asp:Button ID="btnPrint" runat="server" Text="พิมพ์รายงาน" Visible="False" Width="150px"
                                        Font-Names="Tahoma" Font-Size="10pt" />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 100%">
                                    <telerik:RadGrid ID="rgCetificateList" runat="server" AllowSorting="True" CssClass="GRID-STYLE"
                                        GridLines="None" Skin="Office2007" Font-Names="Tahoma" Font-Size="X-Small" TabIndex="-1"
                                        PageSize="10" ShowFooter="True">
                                        <MasterTableView AutoGenerateColumns="False" CellSpacing="-1" Width="100%" NoMasterRecordsText="ไม่พบรายการที่ทำการค้นหา">
                                            <GroupByExpressions>
                                                <telerik:GridGroupByExpression>
                                                    <SelectFields>
                                                        <telerik:GridGroupByField FieldAlias="form_name" FieldName="form_name" HeaderText="ประเภทหนังสือรับรอง :  " FormatString=""></telerik:GridGroupByField>
                                                    </SelectFields>
                                                    <GroupByFields>
                                                        <telerik:GridGroupByField FieldName="form_name" FieldAlias="form_name" FormatString=""></telerik:GridGroupByField>
                                                    </GroupByFields>
                                                </telerik:GridGroupByExpression>
                                            </GroupByExpressions>
                                            <Columns>
                                                <telerik:GridBoundColumn DataField="company_name" HeaderText="บริษัท" ReadOnly="True"
                                                    SortExpression="company_name" UniqueName="company_name">
                                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="country_name" HeaderText="ประเทศปลายทาง" ReadOnly="True"
                                                    SortExpression="country_name" UniqueName="country_name">
                                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="reference_code2" HeaderText="เลขที่หนังสือรับรอง" ReadOnly="True"
                                                    SortExpression="reference_code2" UniqueName="reference_code2">
                                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="approve_date" HeaderText="วันที่อนุมัติ" ReadOnly="True"
                                                    SortExpression="approve_date" UniqueName="approve_date">
                                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center"/>
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="tariff_code" HeaderText="พิกัด" ReadOnly="True"
                                                    SortExpression="tariff_code" UniqueName="tariff_code">
                                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="box8" HeaderText="ช่อง 8" ReadOnly="True"
                                                    SortExpression="box8" UniqueName="box8">
                                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridNumericColumn DataField="net_weight" ReadOnly="True" HeaderText="NET WEIGHT"
                                                    SortExpression="net_weight" UniqueName="net_weight" Aggregate="Sum" FooterText="รวม : "
                                                    DataFormatString="{0:N4}">
                                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Right" />
                                                    <FooterStyle Font-Names="Tahoma" HorizontalAlign="Right" Font-Size="10pt" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </telerik:GridNumericColumn>
                                                <telerik:GridNumericColumn DataField="fob_amt" ReadOnly="True" HeaderText="มูลค่า (USD)"
                                                    SortExpression="fob_amt" UniqueName="fob_amt" Aggregate="Sum" FooterText="รวม : "
                                                    DataFormatString="{0:N4}">
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
