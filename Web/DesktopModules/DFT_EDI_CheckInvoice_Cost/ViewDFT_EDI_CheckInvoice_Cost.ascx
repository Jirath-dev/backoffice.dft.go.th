<%@ Control Language="vb" Inherits="YourCompany.Modules.DFT_EDI_CheckInvoice_Cost.ViewDFT_EDI_CheckInvoice_Cost"
    AutoEventWireup="false" Explicit="True" Codebehind="ViewDFT_EDI_CheckInvoice_Cost.ascx.vb" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
</telerik:RadAjaxManager>
<table cellspacing="5" width="100%">
    <tr>
        <td>
            <table cellspacing="0" border="0" class="m-frm-bdr" cellpadding="1" width="100%">
                <tr>
                    <td style="width: 100%">
                        <table cellspacing="0" cellpadding="0" border="0" width="100%">
                            <tr class="m-frm-hdr">
                                <td>
                                    <table cellspacing="0" cellpadding="0" border="0" width="100%">
                                        <tr class="m-frm-hdr">
                                            <td align="left" class="m-frm-hdr">
                                                <table cols="2" border="0" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td nowrap class="groupboxheader">
                                                            <font class="groupbox">&#160;&#160;ตรวจสอบ Invoices / ต้นทุน&#160;&#160;</font>
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
                                <td style="height: 90px; width:100%">
                                    <telerik:RadTabStrip ID="RadTabStrip1" runat="server" Skin="Office2007" Width="100%"
                                        SelectedIndex="1" MultiPageID="RadMultiPage1">
                                        <Tabs>
                                            <telerik:RadTab runat="server" Text="ข้อมูลต้นทุน" Selected="True">
                                            </telerik:RadTab>
                                            <telerik:RadTab runat="server" Text="ข้อมูล Invoice">
                                            </telerik:RadTab>
                                        </Tabs>
                                    </telerik:RadTabStrip>
                                    <table id="tblData" runat="server" cellpadding="0" cellspacing="0" style="width: 100%">
                                        <tr>
                                            <td style="width: 100%">
                                                <telerik:RadMultiPage ID="RadMultiPage1" runat="server" Width="100%" SelectedIndex="0"
                                                    TabIndex="-1">
                                                    <telerik:RadPageView ID="RadPageView1" runat="server" Width="100%" TabIndex="-1">
                                                        <table style="width: 100%">
                                                            <tr>
                                                                <td align="right" style="width: 161px;">
                                                                    <font class="FormLabel">เลขประจำตัวผู้เสียภาษี :&nbsp;</font>
                                                                </td>
                                                                <td style="width: 210px">
                                                                    <asp:TextBox ID="txtCompanyTaxNo_Cost" runat="server" Width="200px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right" style="width: 161px;">
                                                                    <font class="FormLabel">ระบุพิกัดสินค้า :&nbsp;</font>
                                                                </td>
                                                                <td style="width: 210px">
                                                                    <asp:TextBox ID="txtTariff" runat="server" Width="200px"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                </td>
                                                                <td align="left">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right" style="width: 161px;">
                                                                </td>
                                                                <td>
                                                                    <asp:Button ID="btnSearchCost" runat="server" Text="ค้นหา" Width="150px" />
                                                                </td>
                                                                <td>
                                                                </td>
                                                                <td>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <table style="width: 100%">
                                                            <tr>
                                                                <td style="width: 100%">
                                                                    <telerik:RadGrid ID="rgCost" runat="server" AllowSorting="True" GridLines="None"
                                                                        ShowStatusBar="True" Skin="Office2007" TabIndex="-1" Width="100%" ShowFooter="True"
                                                                        AllowPaging="True" PageSize="50">
                                                                        <FooterStyle HorizontalAlign="Right" Font-Names="Tahoma" Font-Size="10pt"></FooterStyle>
                                                                        <MasterTableView NoMasterRecordsText="ไม่พบข้อมูลต้นทุน" AutoGenerateColumns="False"
                                                                            Width="100%">
                                                                            <Columns>
                                                                                <telerik:GridBoundColumn DataField="company_name_en" HeaderText="ชื่อบริษัท(ภาษาอังกฤษ)"
                                                                                    ReadOnly="True" SortExpression="company_name_en" UniqueName="company_name_en">
                                                                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Left" />
                                                                                </telerik:GridBoundColumn>
                                                                                <telerik:GridBoundColumn DataField="harmonized_no" HeaderText="พิกัด" ReadOnly="True"
                                                                                    SortExpression="harmonized_no" UniqueName="harmonized_no">
                                                                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Left" />
                                                                                </telerik:GridBoundColumn>
                                                                                <telerik:GridBoundColumn DataField="certoforigin_no" HeaderText="เลขที่ต้นทุน" ReadOnly="True"
                                                                                    SortExpression="certoforigin_no" UniqueName="certoforigin_no">
                                                                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Left" />
                                                                                </telerik:GridBoundColumn>
                                                                                <telerik:GridBoundColumn DataField="certoforigin_date" HeaderText="วันที่ต้นทุน" ReadOnly="True"
                                                                                    SortExpression="certoforigin_date" UniqueName="certoforigin_date" >
                                                                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Left" />
                                                                                </telerik:GridBoundColumn>
                                                                                <telerik:GridBoundColumn DataField="country" HeaderText="ประเทศ" ReadOnly="True"
                                                                                    SortExpression="country" UniqueName="country">
                                                                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Left" />
                                                                                </telerik:GridBoundColumn>
                                                                                <telerik:GridBoundColumn DataField="models" HeaderText="รุ่น" ReadOnly="True" SortExpression="form_nmodelse"
                                                                                    UniqueName="models">
                                                                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Left" />
                                                                                </telerik:GridBoundColumn>
                                                                                <telerik:GridBoundColumn DataField="goods_desc_en" HeaderText="รายละเอียดสินค้า(ภาษาอังกฤษ)"
                                                                                    ReadOnly="True" SortExpression="goods_desc_en" UniqueName="goods_desc_en">
                                                                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Left" />
                                                                                </telerik:GridBoundColumn>
                                                                                <telerik:GridBoundColumn DataField="goods_desc_th" HeaderText="รายละเอียดสินค้า(ภาษาไทย)"
                                                                                    ReadOnly="True" SortExpression="goods_desc_th" UniqueName="goods_desc_th">
                                                                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Left" />
                                                                                </telerik:GridBoundColumn>
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
                                                    </telerik:RadPageView>
                                                    <telerik:RadPageView ID="RadPageView2" runat="server" Width="100%" TabIndex="-1">
                                                        <table style="width: 100%">
                                                            <tr>
                                                                <td align="right" style="width: 161px;">
                                                                    <font class="FormLabel">เลขประจำตัวผู้เสียภาษี :&nbsp;</font>
                                                                </td>
                                                                <td style="width: 210px">
                                                                    <asp:TextBox ID="txtCompanyTaxNo_Invoice" runat="server" Width="200px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right" style="width: 161px;">
                                                                    <font class="FormLabel">ระบุ Invoice :&nbsp;</font>
                                                                </td>
                                                                <td style="width: 210px">
                                                                    <asp:TextBox ID="txtInvoice_no" runat="server" Width="200px"></asp:TextBox>
                                                                </td>
                                                                <td align="right" style="width: 40px">
                                                                    <font class="FormLabel">ปี :&nbsp;</font>
                                                                </td>
                                                                <td>
                                                                    <telerik:RadComboBox ID="rcbInvoiceYear" runat="server">
                                                                    </telerik:RadComboBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right" style="width: 161px;">
                                                                </td>
                                                                <td>
                                                                    <asp:Button ID="btnSearchInvoice" runat="server" Text="ค้นหา" Width="150px" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <table style="width: 100%">
                                                            <tr>
                                                                <td style="width: 100%">
                                                                    <telerik:RadGrid ID="rgInvoice" runat="server" AllowSorting="True" GridLines="None"
                                                                        ShowStatusBar="True" Skin="Office2007" TabIndex="-1" Width="100%" ShowFooter="true"
                                                                        AllowPaging="True" PageSize="50">
                                                                        <FooterStyle HorizontalAlign="Right" Font-Names="Tahoma" Font-Size="10pt"></FooterStyle>
                                                                        <MasterTableView NoMasterRecordsText="ไม่พบข้อมูล Invoice" AutoGenerateColumns="False"
                                                                            Width="100%">
                                                                             <Columns>
                                                                                <telerik:GridBoundColumn DataField="invh_run_auto" HeaderText="เลขที่อ้างอิง"
                                                                                    ReadOnly="True" SortExpression="invh_run_auto" UniqueName="invh_run_auto">
                                                                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="center" />
                                                                                </telerik:GridBoundColumn>
                                                                            </Columns>
                                                                            <Columns>
                                                                                <telerik:GridBoundColumn DataField="company_eng" HeaderText="ชื่อบริษัท(ภาษาอังกฤษ)"
                                                                                    ReadOnly="True" SortExpression="company_eng" UniqueName="company_eng">
                                                                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Left" />
                                                                                </telerik:GridBoundColumn>
                                                                            </Columns>
                                                                            <Columns>
                                                                                <telerik:GridBoundColumn DataField="invoice_no" HeaderText="Invoice" ReadOnly="True"
                                                                                    SortExpression="invoice_no" UniqueName="invoice_no">
                                                                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Left" />
                                                                                </telerik:GridBoundColumn>
                                                                            </Columns>
                                                                            <Columns>
                                                                                <telerik:GridBoundColumn DataField="edi_year" HeaderText="ปี" ReadOnly="True" SortExpression="edi_year"
                                                                                    UniqueName="edi_year">
                                                                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Left" />
                                                                                </telerik:GridBoundColumn>
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
