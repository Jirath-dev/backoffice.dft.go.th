<%@ Control Language="vb" Inherits="NTi.Modules.DFT_EDI_Approved.ViewDFT_EDI_Approved"
    AutoEventWireup="false" Explicit="True" Codebehind="ViewDFT_EDI_Approved.ascx.vb" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="RadAjaxManager1">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="rgApprovedList" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="rgApprovedList">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="rgApprovedList" />
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
                                                            <font class="groupbox">&nbsp; บันทึกผลการตรวจสอบ (เฉพาะที่ผ่าน) &nbsp;</font></td>
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
                                                            <font class="FormLabel">หมายเลขอ้างอิง (Barcode) :
                                                                <asp:TextBox ID="txtSearch" runat="server" MaxLength="15"></asp:TextBox>
                                                                <asp:Button ID="btnApproved" runat="server" Text="บันทึกผล" ValidationGroup="approved"
                                                                    Width="150px" />
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtSearch"
                                                                    Display="Dynamic" ErrorMessage="กรุณาป้อนหมายเลขอ้างอิง" Font-Names="Tahoma"
                                                                    Font-Size="10pt" SetFocusOnError="True" ValidationGroup="approved"></asp:RequiredFieldValidator>
                                                                <asp:CheckBox ID="chkBranch" runat="server" Font-Names="Tahoma" Font-Size="10pt"
                                                                    Text="บันทึกผลพร้อมเอกสารแนบ" Checked="True" Enabled="False" /></font></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="font-size: 12pt; color: #000000; font-family: Times New Roman">
                                                            <font class="FormLabel">ดูสถานะที่ผ่านการตรวจสอบ :</font>
                                                            <telerik:RadDatePicker ID="rdpApprovedDate" runat="server" Culture="English (United Kingdom)"
                                                                Skin="Vista" Width="200px">
                                                                <Calendar Skin="Vista" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                                    ViewSelectorText="x">
                                                                </Calendar>
                                                                <DatePopupButton HoverImageUrl="" ImageUrl="" />
                                                            </telerik:RadDatePicker>
                                                            &nbsp;<asp:Button ID="btnSearch" runat="server" Text="ค้นหา" Width="150px" /></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="font-size: 12pt; color: #000000; font-family: Times New Roman">
                                                            <telerik:RadGrid ID="rgApprovedList" runat="server" AllowPaging="False" AllowSorting="True"
                                                                PageSize="20" GridLines="None" ShowStatusBar="True" Skin="Office2007" TabIndex="-1"
                                                                Width="100%" ShowGroupPanel="true">
                                                                <MasterTableView AutoGenerateColumns="False" CellSpacing="-1" ClientDataKeyNames="invh_run_auto"
                                                                    DataKeyNames="invh_run_auto" NoMasterRecordsText="ไม่มีรายการคำร้อง" Width="100%"
                                                                    GroupsDefaultExpanded="false">
                                                                    <%--<GroupByExpressions>
                                                                        <telerik:GridGroupByExpression>
                                                                            <SelectFields>
                                                                                <telerik:GridGroupByField FieldAlias="edi_status" FieldName="edi_status" HeaderText="สถานะ"
                                                                                    FormatString=""></telerik:GridGroupByField>
                                                                            </SelectFields>
                                                                            <GroupByFields>
                                                                                <telerik:GridGroupByField FieldName="edi_status" SortOrder="None" FieldAlias="edi_status"
                                                                                    FormatString=""></telerik:GridGroupByField>
                                                                            </GroupByFields>
                                                                        </telerik:GridGroupByExpression>
                                                                    </GroupByExpressions>--%>
                                                                    <Columns>
                                                                        <telerik:GridBoundColumn DataField="invh_run_auto" HeaderText="เลขที่อ้างอิง" ReadOnly="True"
                                                                            SortExpression="invh_run_auto" UniqueName="invh_run_auto">
                                                                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="reference_code2" HeaderText="เลขที่หนังสือรับรอง"
                                                                            ReadOnly="True" SortExpression="reference_code2" UniqueName="reference_code2">
                                                                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="edi_status" HeaderText="สถานะ" SortExpression="edi_status"
                                                                            UniqueName="edi_status">
                                                                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="form_name" HeaderText="ชื่อฟอร์ม" ReadOnly="True"
                                                                            SortExpression="form_name" UniqueName="form_name">
                                                                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="company_name" HeaderText="บริษัท" SortExpression="company_name"
                                                                            UniqueName="company_name">
                                                                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="approve_date" DataFormatString="{0:dd/MM/yyyy HH:mm}"
                                                                            HeaderText="วันอนุมัติ" SortExpression="approve_date" UniqueName="approve_date">
                                                                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="approval" HeaderText="ผู้อนุมัติ" ReadOnly="True"
                                                                            SortExpression="approval" UniqueName="approval">
                                                                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="approve_by" HeaderText="โดย" ReadOnly="True"
                                                                            SortExpression="approve_by" UniqueName="approve_by">
                                                                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="site_id" HeaderText="สถานที่" ReadOnly="True"
                                                                            SortExpression="site_id" UniqueName="site_id">
                                                                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                                                        </telerik:GridBoundColumn>
                                                                    </Columns>
                                                                </MasterTableView>
                                                                <HeaderStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                <ClientSettings EnableRowHoverStyle="True" ReorderColumnsOnClient="True" AllowDragToGroup="True"
                                                                    AllowColumnsReorder="True">
                                                                    <Selecting AllowRowSelect="True" />
                                                                </ClientSettings>
                                                                <GroupingSettings ShowUnGroupButton="true" />
                                                            </telerik:RadGrid></td>
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
<asp:Label ID="lblType" runat="server" Visible="False"></asp:Label>
