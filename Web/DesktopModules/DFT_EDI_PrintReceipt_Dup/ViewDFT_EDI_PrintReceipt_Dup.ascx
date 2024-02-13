<%@ Control Language="vb" Inherits="NTi.Modules.DFT_EDI_PrintReceipt_Dup.ViewDFT_EDI_PrintReceipt_Dup"
    AutoEventWireup="false" Explicit="True" Codebehind="ViewDFT_EDI_PrintReceipt_Dup.ascx.vb" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="rgReceiptApprovedList">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="rgReceiptApprovedList" />
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
                                                            <font class="groupbox">&nbsp; พิมพ์ใบเสร็จ (พิมพ์ซ้ำ)</font></td>
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
                                    <asp:Label ID="lblRoleID" runat="server" ForeColor="Red" Visible="False"></asp:Label>
                                    <asp:Label ID="Label1" runat="server" Text="Label" Visible="False"></asp:Label>
                                    <asp:Label ID="Label2" runat="server" Text="Label" Visible="False"></asp:Label></td>
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
                                                        <td>
                                                            <telerik:RadTabStrip ID="RadTabStrip2" runat="server" Skin="Office2007" Width="100%"
                                                                SelectedIndex="1" MultiPageID="RadMultiPage2">
                                                                <Tabs>
                                                                    <telerik:RadTab TabIndex="0" runat="Server" Text="ใบเสร็จสวัสดิการ (ใบเสร็จเขียว)" Font-Bold="true">
                                                                    </telerik:RadTab>
                                                                    <telerik:RadTab TabIndex="1" runat="Server" Text="ใบเสร็จกองทุน (ใบเสร็จเหลือง)" Selected="true" Font-Bold="true">
                                                                    </telerik:RadTab>
                                                                </Tabs>
                                                            </telerik:RadTabStrip>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 100%">
                                                            <telerik:RadMultiPage ID="RadMultiPage2" runat="server" Width="100%" SelectedIndex="0"
                                                                TabIndex="-1">
                                                                <telerik:RadPageView ID="RadPageView2" runat="server" Width="100%" TabIndex="-1">
                                                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                        <tr>
                                                                            <td style="font-size: 12pt; color: #000000; font-family: Times New Roman">
                                                                                <table>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <font class="FormLabel">วันที่ออกใบเสร็จ :</font></td>
                                                                                        <td>
                                                                                            <telerik:RadDatePicker ID="rdpReceiptDate" runat="server" Culture="English (United Kingdom)"
                                                                                                Skin="Vista" Width="200px" AutoPostBack="True">
                                                                                                <Calendar Skin="Vista" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                                                                    ViewSelectorText="x" DayNameFormat="FirstTwoLetters" FirstDayOfWeek="Sunday"
                                                                                                    ShowRowHeaders="False">
                                                                                                </Calendar>
                                                                                                <DatePopupButton HoverImageUrl="" ImageUrl="" />
                                                                                                <DateInput AutoPostBack="True">
                                                                                                </DateInput>
                                                                                            </telerik:RadDatePicker>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:RadioButtonList ID="RadioButtonList1" runat="server" Font-Names="Tahoma" Font-Size="10pt"
                                                                                                RepeatDirection="Horizontal" Width="200px" BorderColor="#CCCCCC" BorderStyle="Solid"
                                                                                                BorderWidth="1px">
                                                                                                <asp:ListItem Value="0">จอภาพ</asp:ListItem>
                                                                                                <asp:ListItem Selected="True" Value="1">เครื่องพิมพ์</asp:ListItem>
                                                                                            </asp:RadioButtonList></td>
                                                                                        <td class="FormLabel">
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td class="FormLabel">
                                                                                        </td>
                                                                                        <td align="left" class="FormLabel">
                                                                                        </td>
                                                                                        <td class="FormLabel" colspan="2">
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td class="FormLabel">
                                                                                        </td>
                                                                                        <td align="left" class="FormLabel" colspan="3">
                                                                                            <asp:RadioButtonList ID="rdblReceiptPrinter" runat="server" RepeatDirection="Horizontal"
                                                                                                RepeatColumns="3">
                                                                                            </asp:RadioButtonList></td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td class="FormLabel">
                                                                                        </td>
                                                                                        <td align="right" class="FormLabel">
                                                                                        </td>
                                                                                        <td class="FormLabel" colspan="2">
                                                                                            <asp:Label ID="lbl_ErrMSG" runat="server" Font-Names="Tahoma" Font-Size="10pt" ForeColor="Red"></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td class="FormLabel" colspan="4">
                                                                                            <asp:Button ID="btnGroupBy" runat="server" Text="Gruop by Expression" /></td>
                                                                                    </tr>
                                                                                </table>
                                                                                <telerik:RadGrid ID="rgReceiptList" runat="server" AllowSorting="True" GridLines="None"
                                                                                    ShowStatusBar="True" Skin="Office2007" TabIndex="-1" Width="100%" PageSize="50"
                                                                                    ShowFooter="True">
                                                                                    <MasterTableView AutoGenerateColumns="False" ClientDataKeyNames="bill_no" DataKeyNames="bill_no"
                                                                                        NoMasterRecordsText="ไม่มีรายการใบเสร็จที่ผ่านการชำระแล้ว" Width="100%" GroupsDefaultExpanded="False">
                                                                                        <Columns>
                                                                                            <telerik:GridTemplateColumn UniqueName="CheckBoxTemplateColumn">
                                                                                                <HeaderStyle Width="30px" HorizontalAlign="Center" />
                                                                                                <ItemStyle Width="30px" HorizontalAlign="Center" />
                                                                                                <ItemTemplate>
                                                                                                    <asp:ImageButton ID="imbPrint" ImageUrl="~/images/Printer.png" runat="server" AlternateText='<%#Eval("bill_no")%>'
                                                                                                        OnClick="imbPrint_Click" />
                                                                                                </ItemTemplate>
                                                                                            </telerik:GridTemplateColumn>
                                                                                            <telerik:GridNumericColumn DataField="bill_no" HeaderText="เลขที่ใบเสร็จ" ReadOnly="True"
                                                                                                SortExpression="bill_no" UniqueName="bill_no" Aggregate="Count" FooterText="รวม : ">
                                                                                                <ItemStyle Width="100px" Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                                                <HeaderStyle Width="100px" HorizontalAlign="Center" />
                                                                                            </telerik:GridNumericColumn>
                                                                                            <telerik:GridBoundColumn DataField="bill_date" DataFormatString="{0:dd/MM/yyyy}"
                                                                                                HeaderText="วันที่ใบเสร็จ" SortExpression="bill_date" UniqueName="bill_date">
                                                                                                <ItemStyle Width="80px" Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                                                <HeaderStyle Width="80px" HorizontalAlign="Center" />
                                                                                            </telerik:GridBoundColumn>
                                                                                            <telerik:GridBoundColumn DataField="receipt_name" HeaderText="บริษัท" SortExpression="receipt_name"
                                                                                                UniqueName="receipt_name">
                                                                                                <ItemStyle Width="270px" Font-Names="Tahoma" Font-Size="10pt" />
                                                                                                <HeaderStyle Width="270px" HorizontalAlign="Center" />
                                                                                            </telerik:GridBoundColumn>
                                                                                            <telerik:GridNumericColumn DataField="amount" HeaderText="จำนวนเงิน" ReadOnly="True"
                                                                                                SortExpression="amount" UniqueName="amount" Aggregate="Sum" DataFormatString="{0:N2}">
                                                                                                <ItemStyle Width="50px" Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Right" />
                                                                                                <HeaderStyle Width="50px" HorizontalAlign="Center" />
                                                                                                <FooterStyle Width="50px" Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Right" />
                                                                                            </telerik:GridNumericColumn>
                                                                                            <telerik:GridBoundColumn DataField="receipt_by" HeaderText="ผู้ออกใบเสร็จ" ReadOnly="True"
                                                                                                SortExpression="receipt_by" UniqueName="receipt_by">
                                                                                                <ItemStyle Width="70px" Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                                                <HeaderStyle Width="70px" HorizontalAlign="Center" />
                                                                                            </telerik:GridBoundColumn>
                                                                                            <telerik:GridBoundColumn DataField="site_id" HeaderText="สถานที่ออก" ReadOnly="True"
                                                                                                SortExpression="site_id" UniqueName="site_id">
                                                                                                <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                                            </telerik:GridBoundColumn>
                                                                                        </Columns>
                                                                                    </MasterTableView>
                                                                                    <HeaderStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                                    <ClientSettings EnableRowHoverStyle="True">
                                                                                        <Selecting AllowRowSelect="True" />
                                                                                        <Scrolling AllowScroll="True" FrozenColumnsCount="10" UseStaticHeaders="True" />
                                                                                    </ClientSettings>
                                                                                    <GroupingSettings ShowUnGroupButton="True" />
                                                                                </telerik:RadGrid>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </telerik:RadPageView>
                                                                <telerik:RadPageView ID="RadPageView3" runat="server" Width="100%" TabIndex="-1" Selected="true">
                                                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                        <tr>
                                                                            <td style="font-size: 12pt; color: #000000; font-family: Times New Roman">
                                                                                <table>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <font class="FormLabel">วันที่ออกใบเสร็จ :</font></td>
                                                                                        <td>
                                                                                            <telerik:RadDatePicker ID="rdpReceiptDate_v2" runat="server" Culture="English (United Kingdom)"
                                                                                                Skin="Vista" Width="200px" AutoPostBack="True">
                                                                                                <Calendar Skin="Vista" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                                                                    ViewSelectorText="x" DayNameFormat="FirstTwoLetters" FirstDayOfWeek="Sunday"
                                                                                                    ShowRowHeaders="False">
                                                                                                </Calendar>
                                                                                                <DatePopupButton HoverImageUrl="" ImageUrl="" />
                                                                                                <DateInput AutoPostBack="True">
                                                                                                </DateInput>
                                                                                            </telerik:RadDatePicker>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:RadioButtonList ID="RadioButtonList2" runat="server" Font-Names="Tahoma" Font-Size="10pt"
                                                                                                RepeatDirection="Horizontal" Width="200px" BorderColor="#CCCCCC" BorderStyle="Solid"
                                                                                                BorderWidth="1px">
                                                                                                <asp:ListItem Value="0">จอภาพ</asp:ListItem>
                                                                                                <asp:ListItem Selected="True" Value="1">เครื่องพิมพ์</asp:ListItem>
                                                                                            </asp:RadioButtonList></td>
                                                                                        <td class="FormLabel">
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td class="FormLabel">
                                                                                        </td>
                                                                                        <td align="left" class="FormLabel">
                                                                                        </td>
                                                                                        <td class="FormLabel" colspan="2">
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td class="FormLabel">
                                                                                        </td>
                                                                                        <td align="left" class="FormLabel" colspan="3">
                                                                                            <asp:RadioButtonList ID="rdblReceiptPrinter_v2" runat="server" RepeatDirection="Horizontal"
                                                                                                RepeatColumns="1">
                                                                                               <%-- <asp:ListItem Text="เครื่องพิมพ์ใบเสร็จกองทุน (ใบเสร็จเหลือง) (10.14.1.24)" Value="0" Selected="True"></asp:ListItem>
                                                                                                <asp:ListItem Text="เครื่องพิมพ์ใบเสร็จกองทุน (ใบเสร็จเหลือง) (10.14.1.21)" Value="1" ></asp:ListItem>--%>
                                                                                            </asp:RadioButtonList></td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td class="FormLabel">
                                                                                        </td>
                                                                                        <td align="right" class="FormLabel">
                                                                                        </td>
                                                                                        <td class="FormLabel" colspan="2">
                                                                                            <asp:Label ID="Label3" runat="server" Font-Names="Tahoma" Font-Size="10pt" ForeColor="Red"></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td class="FormLabel" colspan="4">
                                                                                            <asp:Button ID="btnGroupBy_v2" runat="server" Text="Gruop by Expression" /></td>
                                                                                    </tr>
                                                                                </table>
                                                                                <telerik:RadGrid ID="rgReceiptListNew" runat="server" AllowSorting="True" GridLines="None"
                                                                                    ShowStatusBar="True" Skin="Office2007" TabIndex="-1" Width="100%" PageSize="50"
                                                                                    ShowFooter="True">
                                                                                    <MasterTableView AutoGenerateColumns="False" ClientDataKeyNames="bill_no" DataKeyNames="bill_no"
                                                                                        NoMasterRecordsText="ไม่มีรายการใบเสร็จที่ผ่านการชำระแล้ว" Width="100%" GroupsDefaultExpanded="False">
                                                                                        <Columns>
                                                                                            <telerik:GridTemplateColumn UniqueName="CheckBoxTemplateColumn">
                                                                                                <HeaderStyle Width="30px" HorizontalAlign="Center" />
                                                                                                <ItemStyle Width="30px" HorizontalAlign="Center" />
                                                                                                <ItemTemplate>
                                                                                                    <asp:ImageButton ID="imbPrint_v2" ImageUrl="~/images/Printer.png" runat="server" AlternateText='<%#Eval("bill_no")%>'
                                                                                                        OnClick="imbPrint_v2_Click" />
                                                                                                </ItemTemplate>
                                                                                            </telerik:GridTemplateColumn>
                                                                                            <telerik:GridNumericColumn DataField="bill_no" HeaderText="เลขที่ใบเสร็จ" ReadOnly="True"
                                                                                                SortExpression="bill_no" UniqueName="bill_no" Aggregate="Count" FooterText="รวม : ">
                                                                                                <ItemStyle Width="100px" Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                                                <HeaderStyle Width="100px" HorizontalAlign="Center" />
                                                                                            </telerik:GridNumericColumn>
                                                                                            <telerik:GridBoundColumn DataField="bill_date" DataFormatString="{0:dd/MM/yyyy}"
                                                                                                HeaderText="วันที่ใบเสร็จ" SortExpression="bill_date" UniqueName="bill_date">
                                                                                                <ItemStyle Width="80px" Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                                                <HeaderStyle Width="80px" HorizontalAlign="Center" />
                                                                                            </telerik:GridBoundColumn>
                                                                                            <telerik:GridBoundColumn DataField="receipt_name" HeaderText="บริษัท" SortExpression="receipt_name"
                                                                                                UniqueName="receipt_name">
                                                                                                <ItemStyle Width="370px" Font-Names="Tahoma" Font-Size="10pt" />
                                                                                                <HeaderStyle Width="370px" HorizontalAlign="Center" />
                                                                                            </telerik:GridBoundColumn>
                                                                                            <telerik:GridNumericColumn DataField="amount" HeaderText="จำนวนเงิน" ReadOnly="True"
                                                                                                SortExpression="amount" UniqueName="amount" Aggregate="Sum" DataFormatString="{0:N2}">
                                                                                                <ItemStyle Width="80px" Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Right" />
                                                                                                <HeaderStyle Width="80px" HorizontalAlign="Center" />
                                                                                                <FooterStyle Width="80px" Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Right" />
                                                                                            </telerik:GridNumericColumn>
                                                                                            <telerik:GridBoundColumn DataField="receipt_by" HeaderText="ผู้ออกใบเสร็จ" ReadOnly="True"
                                                                                                SortExpression="receipt_by" UniqueName="receipt_by">
                                                                                                <ItemStyle Width="90px" Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                                                <HeaderStyle Width="90px" HorizontalAlign="Center" />
                                                                                            </telerik:GridBoundColumn>
                                                                                            <telerik:GridBoundColumn DataField="site_id" HeaderText="สถานที่ออก" ReadOnly="True"
                                                                                                SortExpression="site_id" UniqueName="site_id">
                                                                                                <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                                            </telerik:GridBoundColumn>
                                                                                        </Columns>
                                                                                    </MasterTableView>
                                                                                    <HeaderStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                                    <ClientSettings EnableRowHoverStyle="True">
                                                                                        <Selecting AllowRowSelect="True" />
                                                                                        <Scrolling AllowScroll="True" FrozenColumnsCount="10" UseStaticHeaders="True" />
                                                                                    </ClientSettings>
                                                                                    <GroupingSettings ShowUnGroupButton="True" />
                                                                                </telerik:RadGrid>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </telerik:RadPageView>
                                                            </telerik:RadMultiPage>
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
