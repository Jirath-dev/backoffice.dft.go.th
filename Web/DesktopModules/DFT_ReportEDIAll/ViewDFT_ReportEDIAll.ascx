<%@ Control Language="vb" Inherits="YourCompany.Modules.DFT_ReportEDIAll.ViewDFT_ReportEDIAll"
    AutoEventWireup="false" Explicit="True" Codebehind="ViewDFT_ReportEDIAll.ascx.vb" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<script language="javascript" type="text/javascript">
// <!CDATA[

function TABLE1_onclick() {

}

// ]]>
</script>

<style type="text/css">
    .RadPicker_Office2007
    {
        vertical-align: middle;
    }
    .RadPicker_Office2007 .RadInput
    {
        vertical-align: baseline;
    }
    .RadInput_Default
    {
        vertical-align: middle;
        font: 12px "segoe ui" ,arial,sans-serif;
    }
    .RadPicker_Office2007 .rcCalPopup
    {
        background-position: 0 0;
    }
    .RadPicker_Office2007 .rcCalPopup
    {
        display: block;
        overflow: hidden;
        width: 22px;
        height: 22px;
        background: url('mvwres://Telerik.Web.UI, Version=2009.2.701.20, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Office2007.Calendar.sprite.gif') no-repeat;
        text-indent: -2222px;
        text-align: center;
    }
</style>
<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="CheckAllSite">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="CheckAllSite" />
                <telerik:AjaxUpdatedControl ControlID="ddlSiteID" />
                <telerik:AjaxUpdatedControl ControlID="CheckNormal" />
                <telerik:AjaxUpdatedControl ControlID="CheckDigital" />
                <telerik:AjaxUpdatedControl ControlID="CheckDigitalAndSeal" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="CheckNormal">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="CheckAllSite" />
                <telerik:AjaxUpdatedControl ControlID="CheckNormal" />
                <telerik:AjaxUpdatedControl ControlID="CheckDigital" />
                <telerik:AjaxUpdatedControl ControlID="CheckDigitalAndSeal" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="CheckDigital">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="CheckAllSite" />
                <telerik:AjaxUpdatedControl ControlID="CheckNormal" />
                <telerik:AjaxUpdatedControl ControlID="CheckDigital" />
                <telerik:AjaxUpdatedControl ControlID="CheckDigitalAndSeal" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="CheckDigitalAndSeal">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="CheckAllSite" />
                <telerik:AjaxUpdatedControl ControlID="CheckNormal" />
                <telerik:AjaxUpdatedControl ControlID="CheckDigital" />
                <telerik:AjaxUpdatedControl ControlID="CheckDigitalAndSeal" />
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
                                                            <font class="groupbox">&#160;&#160;รายงานสรุปการออกหนังสือรับรอง&#160;&#160;</font>
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
                                                <table cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td align="right" style="height: 30px; width: 100px;" valign="top">
                                                            <font class="FormLabel">สถานที่ :&nbsp;</font>
                                                        </td>
                                                        <td align="left" style="height: 50px" valign="top">
                                                            <asp:CheckBox ID="CheckAllSite" runat="server" AutoPostBack="True" Font-Names="Tahoma"
                                                                Font-Size="10pt" Text="ทุกสาขา" Height="25px" />
                                                            <telerik:RadComboBox ID="ddlSiteID" runat="server" AllowCustomText="True" DropDownWidth="300px"
                                                                EmptyMessage="-----กรุณาลือกสถานที่-----" MaxHeight="200px" Width="220px" Filter="Contains">
                                                            </telerik:RadComboBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" style="height: 30px; width: 100px;" valign="top">
                                                            <font class="FormLabel">ระบบ :&nbsp;</font>
                                                        </td>
                                                        <td valign="top" align="left" style="height: 30px">
                                                            <asp:RadioButtonList ID="rdbSelectSys" runat="server" RepeatDirection="Horizontal">
                                                                <asp:ListItem Text="ทุกระบบ" Value="0" Selected="true"></asp:ListItem>
                                                                <asp:ListItem Text="ระบบปกติ" Value="1"></asp:ListItem>
                                                                <asp:ListItem Text="Digital" Value="2"></asp:ListItem>
                                                                <asp:ListItem Text="Digital &amp; SealSign" Value="3"></asp:ListItem>
                                                            </asp:RadioButtonList></td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" style="height: 30px; width: 100px;" valign="top">
                                                            <font class="FormLabel">ประเภทฟอร์ม :&nbsp;</font>
                                                        </td>
                                                        <td valign="top" align="left" style="height: 30px">
                                                            &nbsp;<telerik:RadComboBox ID="ddlSelectForm" runat="server" AllowCustomText="True"
                                                                DropDownWidth="300px" EmptyMessage="-----กรุณาเลือกฟอร์ม-----" MaxHeight="200px"
                                                                Width="330px" Filter="Contains">
                                                            </telerik:RadComboBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" style="height: 30px; width: 100px;" valign="top">
                                                            <font class="FormLabel">ตั้งแต่วันที่ :&nbsp;</font>
                                                        </td>
                                                        <td valign="top" align="left" style="height: 30px">
                                                            <telerik:RadDatePicker ID="rdpFromDate" runat="server" Culture="English (United Kingdom)"
                                                                Width="170px" Font-Names="Tahoma" Font-Size="10pt" Skin="Outlook">
                                                                <Calendar Skin="Outlook" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                                    ViewSelectorText="x" DayNameFormat="FirstTwoLetters" FirstDayOfWeek="Sunday"
                                                                    ShowRowHeaders="False">
                                                                </Calendar>
                                                                <DatePopupButton HoverImageUrl="" ImageUrl="" />
                                                            </telerik:RadDatePicker>
                                                            &nbsp;&nbsp;<font class="FormLabel">ถึง&nbsp;&nbsp;</font>
                                                            <telerik:RadDatePicker ID="rdpToDate" runat="server" Culture="English (United Kingdom)"
                                                                Width="170px" Font-Names="Tahoma" Font-Size="10pt" Skin="Outlook">
                                                                <Calendar Skin="Outlook" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                                    ViewSelectorText="x" DayNameFormat="FirstTwoLetters" FirstDayOfWeek="Sunday"
                                                                    ShowRowHeaders="False">
                                                                </Calendar>
                                                                <DatePopupButton HoverImageUrl="" ImageUrl="" />
                                                            </telerik:RadDatePicker>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" style="width: 100px; height: 30px" valign="top">
                                                        </td>
                                                        <td align="left" style="height: 40px" valign="middle">
                                                            <asp:Button ID="btnSearch" runat="server" Width="150px" Text="ค้นหา"></asp:Button>&nbsp;
                                                            &nbsp;<asp:Button ID="btnExcel" runat="server" Text="Excel" Width="100px" />
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
                                        PageSize="50">
                                        <FooterStyle HorizontalAlign="Right" Font-Names="Tahoma" Font-Size="10pt"></FooterStyle>
                                        <MasterTableView NoMasterRecordsText="ไม่พบข้อมูลหนังสือรับรอง" AutoGenerateColumns="False"
                                            Width="100%" GroupLoadMode="Client" TableLayout="Fixed" ShowGroupFooter="True"
                                            NoDetailRecordsText="ไม่พบข้อมูลหนังสือรับรอง" GroupsDefaultExpanded="false">
                                            <GroupByExpressions>
                                                <telerik:GridGroupByExpression>
                                                    <SelectFields>
                                                        <telerik:GridGroupByField FieldName="site_name" HeaderText="สถานที่" FieldAlias="site_name"
                                                            FormatString="" />
                                                        <telerik:GridGroupByField FieldName="sentBy" HeaderText="ส่ง" FieldAlias="sentBy"
                                                            FormatString="" />
                                                       </SelectFields>
                                                    <GroupByFields>
                                                        <telerik:GridGroupByField FieldName="site_name" FieldAlias="site_name" FormatString=""
                                                            HeaderText="" />
                                                        <telerik:GridGroupByField FieldName="sentBy" FieldAlias="sentBy" FormatString=""
                                                            HeaderText="" />
                                                    </GroupByFields>
                                                </telerik:GridGroupByExpression>
                                            </GroupByExpressions>
                                            <Columns>
                                                <telerik:GridBoundColumn DataField="form_name" HeaderText="ประเภทฟอร์ม" ReadOnly="True"
                                                    SortExpression="form_name" UniqueName="form_name">
                                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Left" />
                                                </telerik:GridBoundColumn>
                                                <%--<telerik:GridNumericColumn DataField="net_weight" Aggregate="Sum" GroupByExpression="site_name"
                                                    ReadOnly="True" FooterText="รวม : " HeaderText="น้ำหนัก (ตัน)" SortExpression="net_weight"
                                                    UniqueName="net_weight">
                                                    <FooterStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Right" />
                                                    <ItemStyle HorizontalAlign="Right" Font-Names="Tahoma" Font-Size="10pt"></ItemStyle>
                                                </telerik:GridNumericColumn>--%>
                                                <telerik:GridNumericColumn DataField="fob_amt" Aggregate="Sum" GroupByExpression="site_name"
                                                    ReadOnly="True" FooterText="รวม : " HeaderText="มูลค่า (FOB)" SortExpression="fob_amt"
                                                    UniqueName="fob_amt">
                                                    <FooterStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Right" />
                                                    <ItemStyle HorizontalAlign="Right" Font-Names="Tahoma" Font-Size="10pt"></ItemStyle>
                                                </telerik:GridNumericColumn>
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
