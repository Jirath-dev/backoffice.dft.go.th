<%@ Control Language="vb" Inherits="NTi.Modules.DFT_EDI_AttachDoc_Ver1.ViewDFT_EDI_AttachDoc_Ver1" AutoEventWireup="false" Explicit="True" Codebehind="ViewDFT_EDI_AttachDoc_Ver1.ascx.vb" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
</telerik:RadAjaxManager>
<table border="0" cellpadding="0" cellspacing="5" width="100%">
    <tr>
        <td>
            <table cellpadding="0" cellspacing="0" style="width: 100%">
                <tr>
                    <td style="width: 100%">
                        <telerik:RadTabStrip ID="RadTabStrip1" runat="server" Font-Names="Tahoma" Font-Size="10pt"
                            MultiPageID="RadMultiPage1" SelectedIndex="0" Skin="Office2007" Width="100%">
                            <Tabs>
                                <telerik:RadTab runat="server" Font-Names="Tahoma" Font-Size="10pt" Selected="True"
                                    Text="รายงานเอกสารแนบ">
                                </telerik:RadTab>
                            </Tabs>
                        </telerik:RadTabStrip>
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%">
                        <telerik:RadMultiPage ID="RadMultiPage1" runat="server" TabIndex="-1" Width="100%"
                            SelectedIndex="0">
                            <telerik:RadPageView ID="RadPageView1" runat="server" Selected="True" TabIndex="-1"
                                Width="100%">
                                <table border="0" cellpadding="1" cellspacing="0" class="m-frm-bdr" width="100%">
                                    <tr>
                                        <td>
                                            <table style="width: 100%">
                                                <tr>
                                                    <td style="width: 100%">
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
                                                                                                        <font class="groupbox">&nbsp; รายงานเอกสารแนบ &nbsp;</font></td>
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
                                                                                                    <td>
                                                                                                        <table>
                                                                                                            <tr>
                                                                                                                <td>
                                                                                                                    <font class="FormLabel">หมายเลขอ้างอิง (Barcode) :</font>
                                                                                                                    <asp:TextBox ID="txtINVH_RUN_AUTO" runat="server" MaxLength="15"></asp:TextBox>
                                                                                                                </td>
                                                                                                                <td>
                                                                                                                        <asp:RadioButtonList ID="rblTypeRepAtt" runat="server" RepeatDirection="Horizontal">
                                                                                                                            <asp:ListItem Selected="True" Value="1">แนบเอกสาร</asp:ListItem>
                                                                                                                            <asp:ListItem Value="2">ยกเลิกการแนบฯ</asp:ListItem>
                                                                                                                        </asp:RadioButtonList></td>
                                                                                                                <td>
                                                                                                                    &nbsp;<asp:Button ID="btnSearch" runat="server" Text="ยืนยัน" ValidationGroup="approved"
                                                                                                                        Width="150px" />
                                                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtINVH_RUN_AUTO"
                                                                                                                        Display="Dynamic" ErrorMessage="กรุณาป้อนหมายเลขอ้างอิง" Font-Names="Tahoma"
                                                                                                                        Font-Size="10pt" SetFocusOnError="True" ValidationGroup="approved"></asp:RequiredFieldValidator>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                        </table>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        <table>
                                                                                                            <tr>
                                                                                                                <td>
                                                                                                                    <font class="FormLabel">ดูสถานะที่ผ่านการแนบเอกสาร :</font>
                                                                                                                    <telerik:RadDatePicker ID="rdpAttachDate" runat="server" Culture="English (United Kingdom)"
                                                                                                                        Skin="Vista" Width="200px">
                                                                                                                        <Calendar Skin="Vista" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                                                                                            ViewSelectorText="x">
                                                                                                                        </Calendar>
                                                                                                                        <DatePopupButton HoverImageUrl="" ImageUrl="" />
                                                                                                                    </telerik:RadDatePicker>
                                                                                                                    &nbsp;<asp:Button ID="btnSearch2" runat="server" Text="ค้นหา" Width="150px" />
                                                                                                                    <asp:Button ID="btnReport" runat="server" Text="พิมพ์รายงาน" Width="150px" />
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
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table style="width: 100%">
                                                <tr>
                                                    <td style="width: 100%">
                                            <telerik:RadGrid ID="rgAttachDocList" runat="server" AllowPaging="True" AllowSorting="True"
                                                GridLines="None" ShowStatusBar="True" Skin="Office2007" TabIndex="-1" Width="100%"
                                                ShowFooter="True" ShowGroupPanel="true">
                                                <MasterTableView AutoGenerateColumns="False" CellSpacing="-1" NoMasterRecordsText="ไม่มีรายการเอกสารแนบตามเงื่อนไขที่ทำการค้นหา"
                                                    Width="100%">
                                                    <Columns>
                                                        <telerik:GridNumericColumn DataField="invh_run_auto" ReadOnly="True" HeaderText="เลขที่อ้างอิง"
                                                            SortExpression="invh_run_auto" UniqueName="invh_run_auto" Aggregate="Count" FooterText="รวม : ">
                                                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                            <FooterStyle Font-Names="Tahoma" Font-Size="10pt" />
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                        </telerik:GridNumericColumn>
                                                        <telerik:GridBoundColumn DataField="reference_code2" HeaderText="เลขที่หนังสือ" SortExpression="reference_code2"
                                                            UniqueName="reference_code2">
                                                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="approve_date" DataFormatString="{0:dd/MM/yyyy}"
                                                            HeaderText="วันอนุมัติ" ReadOnly="True" SortExpression="approve_date" UniqueName="approve_date">
                                                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridTemplateColumn HeaderText="หมายเลข Invoice">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblInvoice_No" runat="server" Text='<%#GetInvoice_No(Eval("invh_run_auto")) %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridBoundColumn DataField="invoice_no1" HeaderText="Invoice1" ReadOnly="True"
                                                            SortExpression="invoice_no1" UniqueName="invoice_no1" Visible="false">
                                                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="invoice_no2" HeaderText="Invoice2" ReadOnly="True"
                                                            SortExpression="invoice_no2" UniqueName="invoice_no2" Visible="false">
                                                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="invoice_no3" HeaderText="Invoice3" ReadOnly="True"
                                                            SortExpression="invoice_no3" UniqueName="invoice_no3" Visible="false">
                                                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="invoice_no4" HeaderText="Invoice4" ReadOnly="True"
                                                            SortExpression="invoice_no4" UniqueName="invoice_no4" Visible="false">
                                                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="invoice_no5" HeaderText="Invoice5" ReadOnly="True"
                                                            SortExpression="invoice_no5" UniqueName="invoice_no5" Visible="false">
                                                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="company_name" HeaderText="บริษัท" ReadOnly="True"
                                                            SortExpression="company_name" UniqueName="company_name">
                                                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="form_name" HeaderText="ฟอร์ม" ReadOnly="True"
                                                            SortExpression="form_name" UniqueName="form_name">
                                                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="rep_status" HeaderText="การแนบ" SortExpression="rep_status"
                                                            UniqueName="rep_status">
                                                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="OverDay" HeaderText="เกินกำหนด(วัน)" ReadOnly="True"
                                                            SortExpression="OverDay" UniqueName="OverDay">
                                                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Right" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="rep_doc_date" DataFormatString="{0:dd/MM/yyyy}"
                                                            HeaderText="วันที่บันทึกการแนบ ฯ" ReadOnly="True" SortExpression="rep_doc_date"
                                                            UniqueName="rep_doc_date">
                                                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="rep_by" HeaderText="ผู้บันทึกการแนบ ฯ" ReadOnly="True"
                                                            SortExpression="rep_by" UniqueName="rep_by">
                                                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                        </telerik:GridBoundColumn>
                                                        <%--<telerik:GridBoundColumn DataField="approve_by" HeaderText="โดย" ReadOnly="True"
                                                            SortExpression="approve_by" UniqueName="approve_by">
                                                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="site_id" HeaderText="สถานที่" ReadOnly="True"
                                                            SortExpression="site_id" UniqueName="site_id">
                                                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                        </telerik:GridBoundColumn>--%>
                                                    </Columns>
                                                </MasterTableView>
                                                <HeaderStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                <ClientSettings EnableRowHoverStyle="True" ReorderColumnsOnClient="True" AllowDragToGroup="True" AllowColumnsReorder="True">
                                                    <Selecting AllowRowSelect="True" />
                                                </ClientSettings>
                                                <GroupingSettings ShowUnGroupButton="true" />
                                            </telerik:RadGrid></td>
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
<asp:Label ID="lblRoleID" runat="server" Visible="False"></asp:Label>
<asp:Label ID="lblState" runat="server" Visible="False"></asp:Label>
