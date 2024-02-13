<%@ Control Language="vb" Inherits="NTi.Modules.DFT_EDI_Report_12.ViewDFT_EDI_Report_12"
    AutoEventWireup="false" Explicit="True" CodeBehind="ViewDFT_EDI_Report_12.ascx.vb" %>
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
                                                                        <font class="groupbox">&#160;&#160;รายงานบันทึกผลการให้บริการ&#160;&#160;</font>
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
                                                            <table cellpadding="0" cellspacing="0" dwcopytype="CopyTableColumn">
                                                                <tr>
                                                                  <td style="text-align: right; width: 120px; height:30px;">
                                                                        <font class="FormLabel"> ระบบ : </font>
                                                                    </td>
                                                                    <td>
                                                                        <asp:RadioButtonList ID="rdoType" runat="server" RepeatDirection="Horizontal" AutoPostBack="true">
                                                                            <asp:ListItem Selected="True" Value="edi">EDI ปกติ</asp:ListItem>
                                                                            <asp:ListItem Value="ds">Digital Signature</asp:ListItem>
                                                                        </asp:RadioButtonList>
                                                                    </td>
                                                                    <td></td>
                                                                    <td colspan="3">
                                                                        <asp:RadioButtonList ID="rdoDSPrintOrCheck" runat="server" RepeatDirection="Horizontal" Visible="false">
                                                                            <asp:ListItem Selected="True" Value="print">กระบวนการพิมพ์งาน (DS)</asp:ListItem>
                                                                            <asp:ListItem Value="checkdoc">กระบวนการตรวจเอกสาร (DS)</asp:ListItem>
                                                                        </asp:RadioButtonList>

                                                                    </td>
                                                                   
                                                                </tr>
                                                                <tr>
                                                                     <td style="text-align: right; width: 120px; height:30px;">
                                                                        <font class="FormLabel">ตั้งแต่วันที่ :</font>
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
                                                                        <font class="FormLabel">ถึงวันที่ :</font>
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
                                                                            <asp:ListItem Selected="True" Value="A">ทั้งหมด</asp:ListItem>
                                                                            <asp:ListItem Value="W">ต้องการ</asp:ListItem>
                                                                        </asp:RadioButtonList>
                                                                    </td>
                                                                    <td>&nbsp;<asp:Button ID="btnSearch" runat="server" Width="150px" Text="ค้นหา" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                      <td style="text-align: right; width: 120px; height:30px;">
                                                                        <font class="FormLabel"> สถานที่ :</font></td>
                                                                    <td colspan="5">
                                                                        <asp:DropDownList ID="ddlSite" runat="server" Font-Names="Tahoma" Font-Size="10pt">
                                                                        </asp:DropDownList></td>
                                                                </tr>
                                                                <tr>
                                                                     <td style="text-align: right; width: 120px; height:30px;">
                                                                        <font class="FormLabel">ประเภทใบเสร็จ :</font></td>
                                                                    <td colspan="5">
                                                                        <asp:RadioButtonList ID="rdlReceiptType" runat="Server" Font-Names="Tahoma" Font-Size="10pt" RepeatDirection="Horizontal">
                                                                            <asp:ListItem Text="ใบเสร็จสวัสดิการ (ใบเสร็จเขียว)" Value="1"></asp:ListItem>
                                                                            <asp:ListItem Text="ใบเสร็จกองทุน (ใบเสร็จเหลือง)" Value="2" Selected="True"></asp:ListItem>
                                                                        </asp:RadioButtonList>
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
                        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                            <tr>
                                <td>
                                    <asp:Button ID="btnPrint" runat="server" Text="พิมพ์รายงาน" Width="150px" />
                                </td>
                                <td style="text-align: right">
                                    <asp:Label ID="lblMinValue" runat="server" ForeColor="Blue" Font-Names="Tahoma" Font-Size="10pt"></asp:Label>
                                    <asp:Label ID="lblMaxValue" runat="server" ForeColor="Red" Font-Names="Tahoma" Font-Size="10pt"></asp:Label>
                                    <asp:Label ID="lblAvgValue" runat="server" ForeColor="Green" Font-Names="Tahoma"
                                        Font-Size="10pt"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%">
                        <telerik:RadGrid ID="rgReceiptList" runat="server" AllowSorting="True" CssClass="GRID-STYLE"
                            GridLines="None" Skin="Office2007" Font-Names="Tahoma" Font-Size="X-Small" TabIndex="-1"
                            PageSize="30">
                            <MasterTableView ShowFooter="true" ShowGroupFooter="True" AutoGenerateColumns="False"
                                CellSpacing="-1" Width="100%" NoMasterRecordsText="ไม่พบรายการที่ทำการค้นหา">
                                <Columns>
                                    <%--<telerik:GridTemplateColumn HeaderText="" UniqueName="TemplateColumn">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkSelect" runat="server" />
                                        </ItemTemplate>
                                        <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                    </telerik:GridTemplateColumn>--%>
                                    <telerik:GridBoundColumn DataField="INDEX" HeaderText="ลำดับ" ReadOnly="True" SortExpression="INDEX"
                                        UniqueName="INDEX">
                                        <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="COMPANY_NAME" HeaderText="ชื่อผู้ส่งออก" ReadOnly="True"
                                        SortExpression="COMPANY_NAME" UniqueName="COMPANY_NAME">
                                        <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="BILL_NO" HeaderText="เลขที่ใบเสร็จรับเงิน" ReadOnly="True"
                                        SortExpression="BILL_NO" UniqueName="BILL_NO">
                                        <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridNumericColumn DataField="FORM_COUNT" ReadOnly="True" HeaderText="จำนวน (ฉบับ)"
                                        SortExpression="FORM_COUNT" UniqueName="FORM_COUNT" Aggregate="Sum" FooterText="รวม : "
                                        DataFormatString="{0:N0}">
                                        <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Right" />
                                        <FooterStyle Font-Names="Tahoma" HorizontalAlign="Right" Font-Size="10pt" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </telerik:GridNumericColumn>
                                    <telerik:GridBoundColumn DataField="START_TIME" HeaderText="เริ่มต้น" ReadOnly="True"
                                        SortExpression="START_TIME" UniqueName="START_TIME" DataFormatString="{0:HH:mm:ss}">
                                        <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="FINISH_TIME" HeaderText="สิ้นสุด" ReadOnly="True"
                                        SortExpression="FINISH_TIME" UniqueName="FINISH_TIME" DataFormatString="{0:HH:mm:ss}">
                                        <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="TOTAL_TIME" HeaderText="ระยะเวลาการให้บริการ"
                                        ReadOnly="True" SortExpression="TOTAL_TIME" UniqueName="TOTAL_TIME">
                                        <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="AVG_TIME" HeaderText="ระยะเวลาการให้บริการ/ฉบับ"
                                        ReadOnly="True" SortExpression="AVG_TIME" UniqueName="AVG_TIME">
                                        <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                    </telerik:GridBoundColumn>
                                    <%--<telerik:GridTemplateColumn HeaderText="ระยะเวลาการให้บริการ" UniqueName="TemplateColumn">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTotalTime" runat="server"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn HeaderText="ระยะเวลาการให้บริการ/ชุด" UniqueName="TemplateColumn">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAvgTime" runat="server"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                    </telerik:GridTemplateColumn>--%>
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