﻿<%@ Control Language="vb" Inherits="NTi.Modules.DFT_EDI_CheckingF04_Manual.ViewDFT_EDI_CheckingF04_Manual"
    AutoEventWireup="false" Explicit="True" Codebehind="ViewDFT_EDI_CheckingF04_Manual.ascx.vb" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="RadAjaxManager1">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="rgCertificateList" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="rgCertificateList">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="rgCertificateList" />
            </UpdatedControls>
        </telerik:AjaxSetting>
    </AjaxSettings>
</telerik:RadAjaxManager>
<table border="0" cellpadding="0" cellspacing="5" width="100%">
    <tr>
        <td>
            <table border="0" cellpadding="1" cellspacing="0" class="m-frm-bdr" width="100%">
                <tr>
                    <td>
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr class="m-frm-hdr">
                                <td>
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                        <tr class="m-frm-hdr">
                                            <td align="left" class="m-frm-hdr">
                                                <table border="0" cellpadding="0" cellspacing="0" cols="2">
                                                    <tr>
                                                        <td class="groupboxheader" nowrap="nowrap">
                                                            <font class="groupbox">&nbsp; ตรวจสอบ/ติดตาม ใบอนุญาต (MANUAL) &nbsp;</font></td>
                                                        <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
                                                            <td style='background-image: url(<%= ResolveUrl("images") %>/CORNER.gif); background-repeat: no-repeat;'>
                                                                &nbsp; &nbsp;&nbsp;</td>
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
                            <tr>
                                <td class="m-frm" valign="top" width="100%">
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                        <tr>
                                            <td>
                                                <table border="0" cellpadding="0" cellspacing="2" width="100%">
                                                    <tr>
                                                        <td style="font-size: 12pt; color: #000000; font-family: Times New Roman">
                                                            <asp:Label ID="lblReturnMsg" runat="server" Font-Names="Tahoma" Font-Size="10pt"
                                                                ForeColor="#0000C0"></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="font-size: 12pt; color: #000000; font-family: Times New Roman">
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <font class="FormLabel">เลขที่หนังสือรับรอง &nbsp;:</font></td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtSearch" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="12pt"
                                                                            ForeColor="Blue" Width="150px"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <font class="FormLabel">สถานที่ออกหนังสือรับรอง &nbsp;:</font>
                                                                    </td>
                                                                    <td>
                                                                        <telerik:RadComboBox ID="cboSite" runat="server" Skin="Web20" Width="250px" Filter="StartsWith"
                                                                            MarkFirstMatch="True">
                                                                        </telerik:RadComboBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Button ID="btnSearch" runat="server" Text="ค้นหา" Width="150px" /></td>
                                                                </tr>
                                                            </table>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <font class="FormLabel">ตั้งแต่วันที่ &nbsp;:</font></td>
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
                                                                        <font class="FormLabel">ถึงวันที่ &nbsp;:</font>
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
                                                                </tr>
                                                            </table>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <font class="FormLabel">ประเภทฟอร์ม &nbsp;:</font></td>
                                                                    <td>
                                                                        <telerik:RadComboBox ID="dropFormType" runat="server" Skin="Web20" Width="500px"
                                                                            Filter="StartsWith" MarkFirstMatch="True">
                                                                        </telerik:RadComboBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="font-size: 12pt; color: #000000; font-family: Times New Roman">
                                                            <telerik:RadGrid ID="rgCertificateList" runat="server" AllowPaging="True" AllowSorting="True"
                                                                GridLines="None" ShowStatusBar="True" Skin="Office2007" TabIndex="-1" Width="100%">
                                                                <MasterTableView AutoGenerateColumns="False" CellSpacing="-1" ClientDataKeyNames=""
                                                                    DataKeyNames="" NoMasterRecordsText="ไม่มีรายการที่ทำการค้นหา" Width="100%">
                                                                    <Columns>
                                                                        <telerik:GridBoundColumn DataField="invh_run_auto" HeaderText="เลขที่อ้างอิง" ReadOnly="True"
                                                                            SortExpression="invh_run_auto" UniqueName="invh_run_auto">
                                                                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="reference_code2" HeaderText="เลขที่หนังสือ" SortExpression="reference_code2"
                                                                            UniqueName="reference_code2">
                                                                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="description" HeaderText="สถานะ" SortExpression="description"
                                                                            UniqueName="description">
                                                                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="form_name" HeaderText="ฟอร์ม" ReadOnly="True"
                                                                            SortExpression="form_name" UniqueName="form_name">
                                                                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="SUMFOB" HeaderText="มูลค่า FOB" ReadOnly="True"
                                                                            SortExpression="SUMFOB" UniqueName="SUMFOB" DataFormatString="{0:N2}">
                                                                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Right" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="site_name" HeaderText="สถานที่" ReadOnly="True"
                                                                            SortExpression="site_name" UniqueName="site_name">
                                                                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="company_name" HeaderText="บริษัท" ReadOnly="True"
                                                                            SortExpression="company_name" UniqueName="company_name">
                                                                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="approve_date" HeaderText="เวลาอนุมัติ" ReadOnly="True"
                                                                            SortExpression="approve_date" UniqueName="approve_date" DataFormatString="{0:dd/MM/yyyy}">
                                                                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="approval" HeaderText="ผู้อนุมัติ" ReadOnly="True"
                                                                            SortExpression="approval" UniqueName="approval">
                                                                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                        </telerik:GridBoundColumn>
                                                                    </Columns>
                                                                </MasterTableView>
                                                                <HeaderStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
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
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
<asp:Label ID="lblRoleID" runat="server" Visible="False"></asp:Label>
