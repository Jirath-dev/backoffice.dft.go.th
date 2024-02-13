<%@ Control language="vb" Inherits="NTi.Modules.DFT_EDI_Rollver.ViewDFT_EDI_Rollver" AutoEventWireup="false" Explicit="True" Codebehind="ViewDFT_EDI_Rollver.ascx.vb" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
    <AjaxSettings>
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
                                                            <font class="FormLabel">Certificate of Origin No. :
                                                                <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>
                                                                <asp:Button ID="btnSearch" runat="server" Text="ค้นหา"
                                                                    Width="150px" /></font></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="font-size: 12pt; color: #000000; font-family: Times New Roman">
                                                            <telerik:RadGrid ID="rgApprovedList" runat="server" AllowPaging="True" AllowSorting="True"
                                                                GridLines="None" ShowStatusBar="True" Skin="Office2007" TabIndex="-1" Width="100%">
                                                                <MasterTableView AutoGenerateColumns="False" CellSpacing="-1" ClientDataKeyNames=""
                                                                    DataKeyNames="" NoMasterRecordsText="ไม่พบรายการ Cerificate of Origin" Width="100%">
                                                                    <Columns>
                                                                        <telerik:GridBoundColumn DataField="tax_id" HeaderText="หมายเลขประจำตัวผู้เสียภาษี" ReadOnly="True"
                                                                            SortExpression="tax_id" UniqueName="tax_id">
                                                                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="center" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="company_name_th" HeaderText="ชื่อนิติบุคคล (ไทย)" ReadOnly="True"
                                                                            SortExpression="company_name_th" UniqueName="company_name_th">
                                                                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="company_name_en" HeaderText="ชื่อนิติบุคคล (อังกฤษ)" 
                                                                            SortExpression="company_name_en" UniqueName="company_name_en">
                                                                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="country" HeaderText="ประเทศ" ReadOnly="True"
                                                                            SortExpression="country" UniqueName="country">
                                                                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="harmonized_no" HeaderText="พิกัด"
                                                                            ReadOnly="True" SortExpression="harmonized_no" UniqueName="harmonized_no">
                                                                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="center" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="certoforigin_no" HeaderText="Certificate of Origin No." 
                                                                            SortExpression="certoforigin_no" UniqueName="certoforigin_no">
                                                                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="center" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="certoforigin_date" HeaderText="วันออกเอกสาร" 
                                                                            SortExpression="certoforigin_date" UniqueName="certoforigin_date" DataFormatString="{0:d}">
                                                                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="goods_desc_th" HeaderText="รายละเอียดสินค้า (ไทย)"
                                                                            SortExpression="goods_desc_th" UniqueName="goods_desc_th">
                                                                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="goods_desc_en" HeaderText="รายละเอียดสินค้า (อังกฤษ)" ReadOnly="True"
                                                                            SortExpression="goods_desc_en" UniqueName="goods_desc_en">
                                                                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
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
    <tr>
        <td>
            <asp:Button ID="Button1" runat="server" Text="ค้นหา" Width="150px" />
            <asp:Button ID="Button2" runat="server" Text="ค้นหา" Width="150px" /></td>
    </tr>
    <tr>
        <td>
            <div id="bodyRowTwo" runat="server"></div>
        </td>
    </tr>
</table>