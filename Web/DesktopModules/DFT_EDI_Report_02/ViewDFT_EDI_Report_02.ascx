<%@ Control Language="vb" Inherits="NTi.Modules.DFT_EDI_Report_02.ViewDFT_EDI_Report_02"
    AutoEventWireup="false" Explicit="True" Codebehind="ViewDFT_EDI_Report_02.ascx.vb" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="rgReceiptSummary">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="rgReceiptSummary" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="rgReceiptDetailSummary">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="rgReceiptDetailSummary" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="rgReceiptSummary_v2">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="rgReceiptSummary_v2" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="rgReceiptDetailSummary_v2">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="rgReceiptDetailSummary_v2" />
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
                                                            <font class="groupbox">&#160;&#160;รายงานตรวจสอบการออกใบเสร็จรับเงิน&#160;&#160;</font></td>
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
                            <tr>
                                <td>
                                    <telerik:RadTabStrip ID="RadTabStrip2" runat="server" Skin="Office2007" Width="100%"
                                        SelectedIndex="1" MultiPageID="RadMultiPage2">
                                        <Tabs>
                                            <telerik:RadTab TabIndex="0" runat="Server" Text="ใบเสร็จสวัสดิการ (ใบเสร็จเขียว)" Selected="True">
                                            </telerik:RadTab>
                                            <telerik:RadTab TabIndex="1" runat="Server" Text="ใบเสร็จกองทุน (ใบเสร็จเหลือง)">
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
                                                                                <asp:Button ID="btnSearch" runat="server" Width="150px" Text="ค้นหา" /></td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 100%">
                                                                    <asp:RadioButtonList ID="rblReportType" runat="server">
                                                                        <asp:ListItem Value="0" Selected="True">สรุปยอดใบเสร็จแยกตามฟอร์ม</asp:ListItem>
                                                                        <asp:ListItem Value="1">สรุปรายละเอียดการออกใบเสร็จรับเงิน</asp:ListItem>
                                                                    </asp:RadioButtonList></td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                            <table cellpadding="2" cellspacing="2" style="width: 100%">
                                                <tr>
                                                    <td style="width: 100%">
                                                        <asp:Button ID="btnPrint" runat="server" Text="พิมพ์รายงาน" Visible="False" Width="150px" /></td>
                                                </tr>
                                                <tr id="trType1" runat="server">
                                                    <td style="width: 100%">
                                                        <telerik:RadGrid ID="rgReceiptSummary" runat="server" AllowPaging="True" AllowSorting="True"
                                                            CssClass="GRID-STYLE" GridLines="None" Skin="Office2007" Font-Names="Tahoma"
                                                            Font-Size="X-Small" TabIndex="-1" PageSize="23" ShowFooter="True">
                                                            <MasterTableView AutoGenerateColumns="False" CellSpacing="-1" Width="100%" NoMasterRecordsText="ไม่พบรายการที่ทำการค้นหา">
                                                                <Columns>
                                                                    <telerik:GridBoundColumn DataField="FORM_NAME" HeaderText="ประเภทฟอร์ม" ReadOnly="True"
                                                                        SortExpression="FORM_NAME" UniqueName="FORM_NAME">
                                                                        <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                                                    </telerik:GridBoundColumn>
                                                                    <telerik:GridTemplateColumn HeaderText="จำนวน (ชุด)" UniqueName="TemplateColumn">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblSent_FormName" runat="server" Text='<%#ckeck_total(Eval("FORM_TYPE"),Eval("AMT")) %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="center" />
                                                                    </telerik:GridTemplateColumn>
                                                                    <telerik:GridNumericColumn DataField="AMT" ReadOnly="True" HeaderText="จำนวนเงิน"
                                                                        SortExpression="AMT" UniqueName="AMT" Aggregate="Sum" FooterText="รวม : ">
                                                                        <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Right" Width="150px" />
                                                                        <FooterStyle Font-Names="Tahoma" Width="150px" HorizontalAlign="Right" Font-Size="10pt" />
                                                                        <HeaderStyle Width="150px" HorizontalAlign="Center" />
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
                                                <tr id="trType2" runat="server">
                                                    <td style="width: 100%">
                                                        <telerik:RadGrid ID="rgReceiptDetailSummary" runat="server" AllowPaging="True" AllowSorting="True"
                                                            CssClass="GRID-STYLE" GridLines="None" Skin="Office2007" Font-Names="Tahoma"
                                                            Font-Size="X-Small" TabIndex="-1" ShowFooter="True">
                                                            <MasterTableView AutoGenerateColumns="False" CellSpacing="-1" Width="100%" NoMasterRecordsText="ไม่พบรายการที่ทำการค้นหา">
                                                                <GroupByExpressions>
                                                                    <telerik:GridGroupByExpression>
                                                                        <SelectFields>
                                                                            <telerik:GridGroupByField FieldAlias="FORM_NAME" FieldName="FORM_NAME" HeaderText="ประเภทฟอร์ม"
                                                                                FormatString=""></telerik:GridGroupByField>
                                                                        </SelectFields>
                                                                        <GroupByFields>
                                                                            <telerik:GridGroupByField FieldName="FORM_NAME" SortOrder="Descending" FieldAlias="FORM_NAME"
                                                                                FormatString=""></telerik:GridGroupByField>
                                                                        </GroupByFields>
                                                                    </telerik:GridGroupByExpression>
                                                                </GroupByExpressions>
                                                                <Columns>
                                                                    <telerik:GridNumericColumn DataField="bill_no" HeaderText="เลขที่ใบเสร็จ" ReadOnly="True"
                                                                        SortExpression="bill_no" UniqueName="bill_no" Aggregate="Count" FooterText="รวม : ">
                                                                        <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                        <FooterStyle Font-Names="Tahoma" Font-Size="10pt" />
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                    </telerik:GridNumericColumn>
                                                                    <telerik:GridBoundColumn DataField="BILL_DATE" HeaderText="วันที่ออกใบเสร็จ" ReadOnly="True"
                                                                        SortExpression="BILL_DATE" UniqueName="BILL_DATE" DataFormatString="{0:d}">
                                                                        <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                    </telerik:GridBoundColumn>
                                                                    <telerik:GridBoundColumn DataField="reference_code2" HeaderText="เลขที่หนังสือรับรอง"
                                                                        ReadOnly="True" SortExpression="reference_code2" UniqueName="reference_code2">
                                                                        <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                    </telerik:GridBoundColumn>
                                                                    <telerik:GridNumericColumn DataField="amt" ReadOnly="True" HeaderText="จำนวนเงิน"
                                                                        SortExpression="amt" UniqueName="amt" Aggregate="Sum" FooterText="รวม : ">
                                                                        <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Right" />
                                                                        <FooterStyle Font-Names="Tahoma" HorizontalAlign="Right" Font-Size="10pt" />
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                    </telerik:GridNumericColumn>
                                                                    <telerik:GridBoundColumn DataField="RECEIPT_NAME" HeaderText="ชื่อนิติบุคคล" ReadOnly="True"
                                                                        SortExpression="RECEIPT_NAME" UniqueName="RECEIPT_NAME">
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
                                        </telerik:RadPageView>
                                        <telerik:RadPageView ID="RadPageView3" runat="server" Width="100%" TabIndex="-1">
                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
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
                                                                                <telerik:RadDatePicker ID="rdpFromDate_v2" runat="server" Skin="Office2007" Culture="English (United Kingdom)"
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
                                                                                <telerik:RadDatePicker ID="rdpToDate_v2" runat="server" Skin="Office2007" Culture="English (United Kingdom)"
                                                                                    Width="170px" Font-Names="Tahoma" Font-Size="10pt">
                                                                                    <Calendar Skin="Office2007" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                                                        ViewSelectorText="x" DayNameFormat="FirstTwoLetters" FirstDayOfWeek="Sunday"
                                                                                        ShowRowHeaders="False">
                                                                                    </Calendar>
                                                                                    <DatePopupButton HoverImageUrl="" ImageUrl="" />
                                                                                </telerik:RadDatePicker>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Button ID="btnSearch_v2" runat="server" Width="150px" Text="ค้นหา" /></td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 100%">
                                                                    <asp:RadioButtonList ID="rblReportType_v2" runat="server">
                                                                        <asp:ListItem Value="0" Selected="True">สรุปยอดใบเสร็จแยกตามฟอร์ม</asp:ListItem>
                                                                        <asp:ListItem Value="1">สรุปรายละเอียดการออกใบเสร็จรับเงิน</asp:ListItem>
                                                                    </asp:RadioButtonList>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <table cellpadding="2" cellspacing="2" style="width: 100%">
                                                            <tr>
                                                                <td style="width: 100%">
                                                                    <asp:Button ID="btnPrint_v2" runat="server" Text="พิมพ์รายงาน" Visible="False" Width="150px" /></td>
                                                            </tr>
                                                            <tr id="trType1_v2" runat="server">
                                                                <td style="width: 100%">
                                                                    <telerik:RadGrid ID="rgReceiptSummary_v2" runat="server" AllowPaging="True" AllowSorting="True"
                                                                        CssClass="GRID-STYLE" GridLines="None" Skin="Office2007" Font-Names="Tahoma"
                                                                        Font-Size="X-Small" TabIndex="-1" PageSize="23" ShowFooter="True">
                                                                        <MasterTableView AutoGenerateColumns="False" CellSpacing="-1" Width="100%" NoMasterRecordsText="ไม่พบรายการที่ทำการค้นหา">
                                                                            <Columns>
                                                                                <telerik:GridBoundColumn DataField="FORM_NAME" HeaderText="ประเภทฟอร์ม" ReadOnly="True"
                                                                                    SortExpression="FORM_NAME" UniqueName="FORM_NAME">
                                                                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                                                                </telerik:GridBoundColumn>
                                                                                <telerik:GridTemplateColumn HeaderText="จำนวน (ชุด)" UniqueName="TemplateColumn">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblSent_FormName" runat="server" Text='<%#ckeck_total(Eval("FORM_TYPE"),Eval("AMT")) %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="center" />
                                                                                </telerik:GridTemplateColumn>
                                                                                <telerik:GridNumericColumn DataField="AMT" ReadOnly="True" HeaderText="จำนวนเงิน"
                                                                                    SortExpression="AMT" UniqueName="AMT" Aggregate="Sum" FooterText="รวม : ">
                                                                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Right" Width="150px" />
                                                                                    <FooterStyle Font-Names="Tahoma" Width="150px" HorizontalAlign="Right" Font-Size="10pt" />
                                                                                    <HeaderStyle Width="150px" HorizontalAlign="Center" />
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
                                                            <tr id="trType2_v2" runat="server">
                                                                <td style="width: 100%">
                                                                    <telerik:RadGrid ID="rgReceiptDetailSummary_v2" runat="server" AllowPaging="True" AllowSorting="True"
                                                                        CssClass="GRID-STYLE" GridLines="None" Skin="Office2007" Font-Names="Tahoma"
                                                                        Font-Size="X-Small" TabIndex="-1" ShowFooter="True">
                                                                        <MasterTableView AutoGenerateColumns="False" CellSpacing="-1" Width="100%" NoMasterRecordsText="ไม่พบรายการที่ทำการค้นหา">
                                                                            <GroupByExpressions>
                                                                                <telerik:GridGroupByExpression>
                                                                                    <SelectFields>
                                                                                        <telerik:GridGroupByField FieldAlias="FORM_NAME" FieldName="FORM_NAME" HeaderText="ประเภทฟอร์ม"
                                                                                            FormatString=""></telerik:GridGroupByField>
                                                                                    </SelectFields>
                                                                                    <GroupByFields>
                                                                                        <telerik:GridGroupByField FieldName="FORM_NAME" SortOrder="Descending" FieldAlias="FORM_NAME"
                                                                                            FormatString=""></telerik:GridGroupByField>
                                                                                    </GroupByFields>
                                                                                </telerik:GridGroupByExpression>
                                                                            </GroupByExpressions>
                                                                            <Columns>
                                                                                <telerik:GridNumericColumn DataField="bill_no" HeaderText="เลขที่ใบเสร็จ" ReadOnly="True"
                                                                                    SortExpression="bill_no" UniqueName="bill_no" Aggregate="Count" FooterText="รวม : ">
                                                                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                                    <FooterStyle Font-Names="Tahoma" Font-Size="10pt" />
                                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                                </telerik:GridNumericColumn>
                                                                                <telerik:GridBoundColumn DataField="BILL_DATE" HeaderText="วันที่ออกใบเสร็จ" ReadOnly="True"
                                                                                    SortExpression="BILL_DATE" UniqueName="BILL_DATE" DataFormatString="{0:d}">
                                                                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                                </telerik:GridBoundColumn>
                                                                                <telerik:GridBoundColumn DataField="reference_code2" HeaderText="เลขที่หนังสือรับรอง"
                                                                                    ReadOnly="True" SortExpression="reference_code2" UniqueName="reference_code2">
                                                                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                                </telerik:GridBoundColumn>
                                                                                <telerik:GridNumericColumn DataField="amt" ReadOnly="True" HeaderText="จำนวนเงิน"
                                                                                    SortExpression="amt" UniqueName="amt" Aggregate="Sum" FooterText="รวม : ">
                                                                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Right" />
                                                                                    <FooterStyle Font-Names="Tahoma" HorizontalAlign="Right" Font-Size="10pt" />
                                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                                </telerik:GridNumericColumn>
                                                                                <telerik:GridBoundColumn DataField="RECEIPT_NAME" HeaderText="ชื่อนิติบุคคล" ReadOnly="True"
                                                                                    SortExpression="RECEIPT_NAME" UniqueName="RECEIPT_NAME">
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
<asp:Label ID="lblRoleID" runat="server" Visible="False"></asp:Label>