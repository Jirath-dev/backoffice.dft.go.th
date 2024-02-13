<%@ Control Language="vb" Inherits="YourCompany.Modules.DFT_Report_Form_ByPro.ViewDFT_Report_Form_ByPro"
    AutoEventWireup="false" Explicit="True" CodeBehind="ViewDFT_Report_Form_ByPro.ascx.vb" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="cbbSystem">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="CheckAllSite" />
                <telerik:AjaxUpdatedControl ControlID="ddlSiteID" />
                <telerik:AjaxUpdatedControl ControlID="ddlSelectForm" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="CheckAllSite">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="ddlSiteID" />
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
                                                            <font class="groupbox">&#160;&#160;รายงานสรุปการออกฟอร์ม&#160;&#160;</font>
                                                        </td>
                                                        <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
                                                            <td style="background-image: url(<%= ResolveUrl("images") %>/CORNER.gif); background-repeat: no-repeat;">&#160;&#160;&#160;&#160;
                                                            </td>
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
                                    <table style="width: 100%">
                                        <tr>
                                            <td style="width: 100%">
                                                <table cellpadding="0" cellspacing="0" style="width: 100%;">
                                                    <tr>
                                                        <td align="right" style="width: 150px;" valign="top">
                                                            <font class="FormLabel">ระบบ :&nbsp;</font>
                                                        </td>
                                                        <td align="left" valign="top">
                                                            <asp:RadioButtonList ID="rblSystem" runat="server" AutoPostBack="True" RepeatDirection="Horizontal">
                                                                <asp:ListItem Text="หนังสือรับรอง" Value="0" Selected="True"></asp:ListItem>
                                                                <asp:ListItem Text="ใบอนุญาต" Value="1"></asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" style="height: 50px; " valign="top">
                                                            <font class="FormLabel">สถานที่ :&nbsp;</font>
                                                        </td>
                                                        <td align="left" valign="top">
                                                            <asp:CheckBox ID="CheckAllSite" runat="server" AutoPostBack="True" Font-Names="Tahoma"
                                                                Font-Size="10pt" Text="ทุกสาขา" Height="25px" />
                                                            <telerik:RadComboBox ID="ddlSiteID" runat="server" AllowCustomText="True" DropDownWidth="300px"
                                                                EmptyMessage="-----กรุณาลือกสถานที่-----" MaxHeight="200px" Width="220px" Filter="Contains">
                                                            </telerik:RadComboBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" style="height: 50px;" valign="top">
                                                            <font class="FormLabel">ประเภทของรายงาน :&nbsp;</font>
                                                        </td>
                                                        <td align="left" valign="top">
                                                            <asp:RadioButtonList ID="rblReportSystem" runat="server" AutoPostBack="False" RepeatDirection="Horizontal">
                                                                <asp:ListItem Text="รวมทุกสาขา" Value="0" Selected="True"></asp:ListItem>
                                                                <asp:ListItem Text="แยกตามแต่ละสาขา" Value="1"></asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" style="height: 30px;" valign="top">
                                                            <font class="FormLabel">ประเภทฟอร์ม :&nbsp;</font>
                                                        </td>
                                                        <td valign="top" align="left" style="height: 30px">&nbsp;<telerik:RadComboBox ID="ddlSelectForm" runat="server" AllowCustomText="True"
                                                            DropDownWidth="400px" EmptyMessage="-----กรุณาเลือกฟอร์ม-----" MaxHeight="200px"
                                                            Width="330px" Filter="Contains">
                                                        </telerik:RadComboBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" style="height: 30px;" valign="top">
                                                            <font class="FormLabel">สถานะฟอร์ม :<br/> (สำหรับหนังสือรับรอง)&nbsp;</font>
                                                        </td>
                                                        <td valign="top" align="left" style="height: 30px">
                                                            <asp:RadioButtonList ID="rdoStatus" runat="server" AutoPostBack="False" RepeatDirection="Horizontal">
                                                                <asp:ListItem Text="ผ่านการอนุมัติ" Value="A" Selected="True"></asp:ListItem>
                                                                <asp:ListItem Text="ไม่อนุมัติ (ไม่ผ่านการตรวจสอบ และ ยกเลิก)" Value="NC"></asp:ListItem>
                                                                <asp:ListItem Text="ทั้งหมด" Value="ANC"></asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" style="height: 30px;" valign="top">
                                                            <font class="FormLabel">ตั้งแต่วันที่ :&nbsp;</font>
                                                        </td>
                                                        <td align="left" style="height: 40px;" valign="middle">
                                                            <table width="100%">
                                                                <tr>
                                                                    <td valign="top" align="left" style="height: 30px; width: 15%;">
                                                                        <telerik:RadDatePicker ID="rdpFromDate" runat="server" Culture="English (United Kingdom)"
                                                                            Width="170px" Font-Names="Tahoma" Font-Size="10pt" Skin="Outlook">
                                                                            <Calendar Skin="Outlook" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                                                ViewSelectorText="x" DayNameFormat="FirstTwoLetters" FirstDayOfWeek="Sunday"
                                                                                ShowRowHeaders="False">
                                                                            </Calendar>
                                                                            <DatePopupButton HoverImageUrl="" ImageUrl="" />
                                                                        </telerik:RadDatePicker>
                                                                    </td>
                                                                    <td valign="top" align="left" style="height: 30px; width: 3%;">
                                                                        <font class="FormLabel">ถึง&nbsp;&nbsp;</font>
                                                                    </td>
                                                                    <td valign="top" align="left" style="height: 30px; width: 15%;">
                                                                        <telerik:RadDatePicker ID="rdpToDate" runat="server" Culture="English (United Kingdom)"
                                                                            Width="170px" Font-Names="Tahoma" Font-Size="10pt" Skin="Outlook">
                                                                            <Calendar Skin="Outlook" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                                                ViewSelectorText="x" DayNameFormat="FirstTwoLetters" FirstDayOfWeek="Sunday"
                                                                                ShowRowHeaders="False">
                                                                            </Calendar>
                                                                            <DatePopupButton HoverImageUrl="" ImageUrl="" />
                                                                        </telerik:RadDatePicker>
                                                                    </td>
                                                                    <td valign="top" align="left" style="height: 30px; width: 67%;"></td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" style="height: 30px" valign="top"></td>
                                                        <td align="left" style="height: 40px;" valign="middle">
                                                            <table width="100%">
                                                                <tr>
                                                                    <td style="width: 15%">
                                                                        <asp:Button ID="btnSearch" runat="server" Width="150px" Text="ค้นหา"></asp:Button>
                                                                    </td>
                                                                    <td style="width: 15%">
                                                                        <asp:Button ID="btnExcel" runat="server" Text="Excel" Width="100px" />
                                                                    </td>
                                                                    <td style="width: 70%"></td>
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
                        <table id="tblData" runat="server" cellpadding="2" cellspacing="2" style="width: 100%">
                            <tr>
                                <td style="width: 100%">
                                    <telerik:RadGrid ID="rgEDI" runat="server" AllowSorting="True" GridLines="None" ShowStatusBar="True"
                                        Skin="Office2007" TabIndex="-1" Width="100%" ShowFooter="true" AllowPaging="True"
                                        PageSize="50" Visible="False">
                                        <FooterStyle HorizontalAlign="Right" Font-Names="Tahoma" Font-Size="10pt"></FooterStyle>
                                        <MasterTableView NoMasterRecordsText="ไม่พบข้อมูล" AutoGenerateColumns="False"
                                            Width="100%" GroupLoadMode="Client" TableLayout="Fixed" ShowGroupFooter="True"
                                            NoDetailRecordsText="ไม่พบข้อมูล" GroupsDefaultExpanded="false">
                                            <Columns>
                                                <telerik:GridBoundColumn DataField="form_name" HeaderText="ประเภทฟอร์ม" ReadOnly="True"
                                                    SortExpression="form_name" UniqueName="form_name">
                                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Left" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridNumericColumn DataField="FORM_COUNT" Aggregate="Sum" ReadOnly="True"
                                                    FooterText="รวม : " HeaderText="จำนวนฉบับ (ตามเลขที่อ้างอิง)" SortExpression="FORM_COUNT"
                                                    GroupByExpression="site_name" UniqueName="FORM_COUNT">
                                                    <FooterStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Right" />
                                                    <ItemStyle HorizontalAlign="Right" Font-Names="Tahoma" Font-Size="10pt"></ItemStyle>
                                                </telerik:GridNumericColumn>
                                                <telerik:GridNumericColumn DataField="totalPrintPage" Aggregate="Sum" ReadOnly="True"
                                                    FooterText="รวม : " HeaderText="จำนวนฉบับ (ตามจำนวนหน้าที่พิมพ์)" SortExpression="totalPrintPage"
                                                    GroupByExpression="site_name" UniqueName="totalPrintPage">
                                                    <FooterStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Right" />
                                                    <ItemStyle HorizontalAlign="Right" Font-Names="Tahoma" Font-Size="10pt"></ItemStyle>
                                                </telerik:GridNumericColumn>
                                            </Columns>
                                        </MasterTableView>
                                        <HeaderStyle HorizontalAlign="Center" Font-Names="Tahoma" Font-Size="10pt"></HeaderStyle>
                                        <ClientSettings EnableRowHoverStyle="True">
                                            <Selecting AllowRowSelect="True"></Selecting>
                                        </ClientSettings>
                                    </telerik:RadGrid>
                                    <telerik:RadGrid ID="rgEDIBySite" runat="server" AllowSorting="True" GridLines="None" ShowStatusBar="True"
                                        Skin="Office2007" TabIndex="-1" Width="100%" ShowFooter="true" AllowPaging="True"
                                        PageSize="50" Visible="False">
                                        <FooterStyle HorizontalAlign="Right" Font-Names="Tahoma" Font-Size="10pt"></FooterStyle>
                                        <MasterTableView NoMasterRecordsText="ไม่พบข้อมูล" AutoGenerateColumns="False"
                                            Width="100%" GroupLoadMode="Client" TableLayout="Fixed" ShowGroupFooter="True"
                                            NoDetailRecordsText="ไม่พบข้อมูล" GroupsDefaultExpanded="false">
                                            <GroupByExpressions>
                                                <telerik:GridGroupByExpression>
                                                    <SelectFields>
                                                        <telerik:GridGroupByField FieldName="site_name" HeaderText="สถานที่" FieldAlias="site_name"
                                                            FormatString="" />
                                                    </SelectFields>
                                                    <GroupByFields>
                                                        <telerik:GridGroupByField FieldName="site_name" FieldAlias="site_name" FormatString=""
                                                            HeaderText="" />
                                                    </GroupByFields>
                                                </telerik:GridGroupByExpression>
                                            </GroupByExpressions>
                                            <Columns>
                                                <telerik:GridBoundColumn DataField="form_name" HeaderText="ประเภทฟอร์ม" ReadOnly="True"
                                                    SortExpression="form_name" UniqueName="form_name">
                                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Left" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridNumericColumn DataField="FORM_COUNT" Aggregate="Sum" ReadOnly="True"
                                                    FooterText="รวม : " HeaderText="จำนวนฉบับ (ตามเลขที่อ้างอิง)" SortExpression="FORM_COUNT"
                                                    GroupByExpression="site_name" UniqueName="FORM_COUNT">
                                                    <FooterStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Right" />
                                                    <ItemStyle HorizontalAlign="Right" Font-Names="Tahoma" Font-Size="10pt"></ItemStyle>
                                                </telerik:GridNumericColumn>
                                                <telerik:GridNumericColumn DataField="totalPrintPage" Aggregate="Sum" ReadOnly="True"
                                                    FooterText="รวม : " HeaderText="จำนวนฉบับ (ตามจำนวนหน้าที่พิมพ์)" SortExpression="totalPrintPage"
                                                    GroupByExpression="site_name" UniqueName="totalPrintPage">
                                                    <FooterStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Right" />
                                                    <ItemStyle HorizontalAlign="Right" Font-Names="Tahoma" Font-Size="10pt"></ItemStyle>
                                                </telerik:GridNumericColumn>
                                            </Columns>
                                        </MasterTableView>
                                        <HeaderStyle HorizontalAlign="Center" Font-Names="Tahoma" Font-Size="10pt"></HeaderStyle>
                                        <ClientSettings EnableRowHoverStyle="True">
                                            <Selecting AllowRowSelect="True"></Selecting>
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
