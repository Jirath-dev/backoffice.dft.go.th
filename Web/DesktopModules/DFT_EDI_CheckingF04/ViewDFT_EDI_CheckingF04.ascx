<%@ Control language="vb" Inherits="NTi.Modules.DFT_EDI_CheckingF04.ViewDFT_EDI_CheckingF04" AutoEventWireup="false" Explicit="True" Codebehind="ViewDFT_EDI_CheckingF04.ascx.vb" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
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
                                                            <font class="groupbox">&nbsp; ตรวจสอบ/ติดตาม ใบอนุญาต &nbsp;</font></td>
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
                                                            <asp:Label ID="lblReturnMsg" runat="server" Font-Names="Tahoma" Font-Size="10pt" ForeColor="#0000C0"></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="font-size: 12pt; color: #000000; font-family: Times New Roman">
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <font class="FormLabel">เงื่อนไขการค้นหา &nbsp;:</font></td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtSearch" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="12pt" ForeColor="Blue"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:CheckBox ID="chkType" runat="server" Checked="True" Font-Names="Tahoma" Font-Size="10pt" Text="ใช้เลขที่หนังสือรับรอง ฯ" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:Button ID="btnSearch" runat="server" Text="ค้นหา" Width="150px" /></td>
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
                                                                        <telerik:GridBoundColumn DataField="reference_code2" HeaderText="เลขที่หนังสือ"
                                                                            SortExpression="reference_code2" UniqueName="reference_code2">
                                                                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="edi_status" HeaderText="สถานะ" 
                                                                            SortExpression="edi_status" UniqueName="edi_status">
                                                                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="form_name" HeaderText="ฟอร์ม" ReadOnly="True"
                                                                            SortExpression="form_name" UniqueName="form_name">
                                                                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="site_id" HeaderText="สถานที่" ReadOnly="True"
                                                                            SortExpression="site_id" UniqueName="site_id">
                                                                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="SUM_FOB" HeaderText="มูลค่า FOB" ReadOnly="True"
                                                                            SortExpression="SUM_FOB" UniqueName="SUM_FOB" DataFormatString="{0:N2}">
                                                                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Right" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="company_name" HeaderText="บริษัท" ReadOnly="True"
                                                                            SortExpression="company_name" UniqueName="company_name">
                                                                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="approve_date" HeaderText="เวลาอนุมัติ" ReadOnly="True"
                                                                            SortExpression="approve_date" UniqueName="approve_date" DataFormatString="{0:dd/MM/yyyy}">
                                                                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="Rep_status" HeaderText="การแนบฯ" ReadOnly="True"
                                                                            SortExpression="Rep_status" UniqueName="Rep_status">
                                                                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="Rep_Doc_date" HeaderText="วันที่บันทึกการแนบฯ" ReadOnly="True"
                                                                            SortExpression="Rep_Doc_date" UniqueName="Rep_Doc_date" DataFormatString="{0:dd/MM/yyyy}">
                                                                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="Rep_by" HeaderText="ผู้บันทึกการแนบ" ReadOnly="True"
                                                                            SortExpression="Rep_by" UniqueName="Rep_by">
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