<%@ Control Language="vb" Inherits="Nti.Modules.DFT_EDI_ResetForm.ViewDFT_EDI_ResetForm"
    AutoEventWireup="false" Explicit="True" Codebehind="ViewDFT_EDI_ResetForm.ascx.vb" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="dnn" TagName="Audit" Src="~/controls/ModuleAuditControl.ascx" %>
<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
</telerik:RadAjaxManager>
<br />
<table border="0" cellpadding="0" cellspacing="0" class="m-frm-bdr" width="100%">
    <tr class="m-frm-hdr">
        <td>
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr class="m-frm-hdr">
                    <td align="left" class="m-frm-hdr">
                        <table border="0" cellpadding="0" cellspacing="0" cols="2">
                            <tr>
                                <td class="groupboxheader" nowrap="nowrap">
                                    <font class="groupbox">&nbsp; ค้นหาข้อมูลฟอร์ม ที่จะคืนสถานะ &nbsp;</font></td>
                                <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
                                    <td style='background-image: url(<%= ResolveUrl("images") %>/CORNER.gif); background-repeat: no-repeat;'>
                                        &nbsp; &nbsp;&nbsp;</td>
                                </telerik:RadCodeBlock>
                            </tr>
                        </table>
                        <asp:Label ID="lblRoleID" runat="server" Text="lblRoleID" Visible="False"></asp:Label>
                        <asp:Label ID="lblstr_sentby" runat="server" Text="" Visible="False"></asp:Label>
                        <asp:Label ID="lblStatus" runat="server" Text="" Visible="False"></asp:Label>
                        </td>
                    <td align="right" class="m-frm-nav">
                    </td>
                </tr>
            </table>
</td> </tr>
<tr>
    <td class="m-frm" valign="top" width="100%">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td width="15">
                    &nbsp;</td>
                <td>
                    <table border="0" cellpadding="0" cellspacing="2" width="100%">
                        <tr>
                            <td style="font-size: 12pt; color: #000000; font-family: Times New Roman">
                                <font class="FormLabel">เลขที่อ้างอิงหรือเลขที่หนังสือรับรองที่ค้นหา :&nbsp;<telerik:RadTextBox
                                    ID="txtSearchRe" runat="server" Font-Bold="True" Font-Size="Large" ForeColor="Teal"
                                    Width="225px">
                                </telerik:RadTextBox>
                                    <asp:CheckBox ID="chkUseRef2" runat="server" Font-Names="Tahoma" Font-Size="10pt"
                                        Text="ใช้เลขที่หนังสือรับรอง" />
                                    <asp:Button ID="btnSearch" runat="server" Text="ค้นหา" ValidationGroup="search" Width="150px" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtSearchRe"
                                        Display="Dynamic" ErrorMessage="กรุณาป้อนเลขที่อ้างอิงหรือเลขที่หนังรับรอง" Font-Names="Tahoma"
                                        Font-Size="10pt" SetFocusOnError="True" ValidationGroup="search"></asp:RequiredFieldValidator></font></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td width="15">
                </td>
                <td>
                </td>
            </tr>
        </table>
    </td>
</tr>
    <tr>
        <td class="m-frm" valign="top" width="100%" style="height: 30px">
            <asp:Panel ID="PanelReset" runat="server" Visible="False" Width="100%">
                <asp:Button ID="btnResetForm" runat="server" Text="คืนสถานะฟอร์ม [เป็นยังไม่ได้พิมพ์]"
                    ValidationGroup="search" Width="253px" />
                <br />
                <br />
                <asp:Label ID="lblReceipt" runat="Server" ForeColor="Red" Font-Names="tahoma" Font-Size="10pt" Font-Bold="True"></asp:Label>
            </asp:Panel>
        </td>
    </tr>
    <tr>
        <td class="m-frm" valign="top" width="100%">
        <telerik:RadGrid ID="GridZero" runat="server" PageSize="40" Skin="Vista" TabIndex="-1">
            <MasterTableView AutoGenerateColumns="False" ClientDataKeyNames="invh_run_auto" DataKeyNames="invh_run_auto,site_id"
                PageSize="10" Width="100%">
                <Columns>
                    <telerik:GridTemplateColumn UniqueName="TemplateViewColumn" Visible="false">
                        <ItemStyle HorizontalAlign="Center" Width="20px" />
                        <ItemTemplate>
                            <asp:HyperLink ID="ViewLink" runat="server" ImageUrl="~/images/view.gif" TabIndex="-1"
                                Text="View"></asp:HyperLink>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn UniqueName="TemplateEditColumn" Visible="false">
                        <ItemStyle HorizontalAlign="Center" Width="20px" />
                        <ItemTemplate>
                            <asp:HyperLink ID="EditLink" runat="server" ImageUrl="~/images/edit.gif" TabIndex="-1"
                                Text="Edit"></asp:HyperLink>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn UniqueName="TemplateDeleteColumn" Visible="false">
                        <ItemStyle HorizontalAlign="Center" Width="20px" />
                        <ItemTemplate>
                            <asp:HyperLink ID="DeleteLink" runat="server" ImageUrl="~/images/delete.gif" TabIndex="-1"
                                Text="Delete"></asp:HyperLink>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridBoundColumn DataField="invh_run_auto" HeaderText="เลขอ้างอิง" ReadOnly="True"
                        SortExpression="invh_run_auto" UniqueName="invh_run_auto">
                        <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                        <HeaderStyle Width="12%" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="form_type" HeaderText="form_type" ReadOnly="True"
                        SortExpression="form_type" UniqueName="form_type" Visible="false">
                        <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                        <HeaderStyle Width="15%" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="form_name" HeaderText="ชื่อฟอร์ม" ReadOnly="True"
                        SortExpression="form_name" UniqueName="form_name" Visible="true">
                        <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                        <HeaderStyle Width="15%" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="edi_status_id" HeaderText="สถานะ" ReadOnly="True"
                        SortExpression="edi_status_id" UniqueName="edi_status_id" Visible="False">
                        <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="reference_code2" HeaderText="เลขหนังสือรับรอง"
                        ReadOnly="True" SortExpression="reference_code2" UniqueName="reference_code2">
                        <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                        <HeaderStyle Width="14%" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="print_flag" HeaderText="พิมพ์" ReadOnly="True"
                        SortExpression="print_flag" UniqueName="print_flag">
                        <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                        <HeaderStyle Width="5%" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="print_no" HeaderText="ครั้งที่พิมพ์" ReadOnly="True"
                        SortExpression="print_no" UniqueName="print_no">
                        <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                        <HeaderStyle Width="7%" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="receipt_flag" HeaderText="พิมพ์ใบเสร็จ" ReadOnly="True"
                        SortExpression="receipt_flag" UniqueName="receipt_flag">
                        <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                        <HeaderStyle Width="7%" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="site_id" HeaderText="สาขาที่รับ" ReadOnly="True"
                        SortExpression="site_id" UniqueName="site_id">
                        <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                        <HeaderStyle Width="10%" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="UserPrintForm" HeaderText="ผู้พิมพ์" ReadOnly="True"
                        SortExpression="UserPrintForm" UniqueName="UserPrintForm" Visible="true">
                        <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                        <HeaderStyle Width="14%" />
                    </telerik:GridBoundColumn>
                    <telerik:GridTemplateColumn HeaderText="คำขอ" UniqueName="TemplateColumn">
                        <ItemTemplate>
                            <asp:Label ID="lblSent_check_request" runat="server" Text='<%#GetStatus_(Eval("check_request")) %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderText="พิมพ์ฟอร์ม" UniqueName="TemplateColumn">
                        <ItemTemplate>
                            <asp:Label ID="lblSent_check_form" runat="server" Text='<%#GetStatus_(Eval("check_form")) %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                    </telerik:GridTemplateColumn>
                    <telerik:GridBoundColumn DataField="totalPrintPage" HeaderText="จำนวนหน้า" ReadOnly="True"
                        SortExpression="totalPrintPage" UniqueName="totalPrintPage">
                        <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                        <HeaderStyle Width="7%" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="printFormDate" HeaderText="วันที่พิมพ์" ReadOnly="True"
                        SortExpression="printFormDate" UniqueName="printFormDate">
                        <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                        <HeaderStyle Width="7%" />
                    </telerik:GridBoundColumn>
                    <telerik:GridTemplateColumn HeaderText="ระบบ" UniqueName="TemplateColumn">
                        <ItemTemplate>
                            <asp:Label ID="lblSent_check_SendBy" runat="server" Text='<%#GetProGram_(Eval("SentBy")) %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                    </telerik:GridTemplateColumn>
                </Columns>
            </MasterTableView>
            <HeaderStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
            <ClientSettings EnableRowHoverStyle="True">
                <Selecting AllowRowSelect="True" />
                <Scrolling AllowScroll="True" />
            </ClientSettings>
        </telerik:RadGrid>
    </td>
</tr>
</table> 