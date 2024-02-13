<%@ Control language="vb" Inherits="NTi.Modules.DFT_EDI_Report_13.ViewDFT_EDI_Report_13" AutoEventWireup="false" Explicit="True" Codebehind="ViewDFT_EDI_Report_13.ascx.vb" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="RadAjaxManager1">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="rgReceiptList" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="rgReceiptList">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="rgReceiptList" />
            </UpdatedControls>
        </telerik:AjaxSetting>
    </AjaxSettings>
</telerik:RadAjaxManager>
<table cellspacing="5" width="100%">
    <tr>
        <td>
            <table style="width: 100%">
                <tr>
                    <td style="width: 100%">
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
                                                                        <font class="groupbox">&#160;&#160;รายงานการเข้าใช้งานระบบ&#160;&#160;</font></td>
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
                                                                        <asp:RadioButtonList ID="rblType" runat="server" RepeatDirection="Horizontal">
                                                                            <asp:ListItem Selected="True" Value="A">ALL</asp:ListItem>
                                                                            <asp:ListItem Value="INSERT">INSERT</asp:ListItem>
                                                                            <asp:ListItem>UPDATE</asp:ListItem>
                                                                            <asp:ListItem>DELETE</asp:ListItem>
                                                                        </asp:RadioButtonList></td>
                                                                    <td>
                                                                        &nbsp;<asp:Button ID="btnSearch" runat="server" Width="150px" Text="ค้นหา" /></td>
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
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr id="trData" runat="server">
        <td>
            <table style="width: 100%">
                <tr>
                    <td style="width: 100%">
                        <telerik:RadGrid ID="rgReceiptList" runat="server" AllowSorting="True" CssClass="GRID-STYLE"
                            GridLines="None" Skin="Office2007" Font-Names="Tahoma" Font-Size="X-Small" TabIndex="-1"
                            PageSize="30" AllowPaging="True">
                            <MasterTableView ShowGroupFooter="True" AutoGenerateColumns="False"
                                Width="100%" NoMasterRecordsText="ไม่พบรายการที่ทำการค้นหา">
                                <Columns>
                                    <telerik:GridTemplateColumn HeaderText="ลำดับ" UniqueName="TemplateColumn">
                                        <ItemTemplate>
                                            <asp:Label ID="lblIndex" runat="server"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn DataField="LogStatus" HeaderText="ประเภทการเข้าถึงข้อมูล" ReadOnly="True"
                                        SortExpression="LogStatus" UniqueName="LogStatus">
                                        <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="LogDate" HeaderText="เวลาเข้าถึงข้อมูล" ReadOnly="True"
                                        SortExpression="LogDate" UniqueName="LogDate">
                                        <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="card_id" HeaderText="หมายเลขบัตร" ReadOnly="True"
                                        SortExpression="card_id" UniqueName="card_id">
                                        <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="request_person" HeaderText="ผู้รับมอบอำนาจ" ReadOnly="True"
                                        SortExpression="request_person" UniqueName="request_person">
                                        <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="company_name" HeaderText="บริษัท" ReadOnly="True"
                                        SortExpression="company_name" UniqueName="company_name">
                                        <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="invh_run_auto" HeaderText="เลขที่อ้างอิง" ReadOnly="True"
                                        SortExpression="invh_run_auto" UniqueName="invh_run_auto">
                                        <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="form_name" HeaderText="ฟอร์ม" ReadOnly="True"
                                        SortExpression="form_name" UniqueName="form_name">
                                        <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
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
<asp:Label ID="lblRoleID" runat="server" Visible="False"></asp:Label>