<%@ Control Language="vb" Inherits="NTi.Modules.DFT_EDI_ReceiptZero.ViewDFT_EDI_ReceiptZero"
    AutoEventWireup="false" Explicit="True" Codebehind="ViewDFT_EDI_ReceiptZero.ascx.vb" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="dnn" TagName="Audit" Src="~/controls/ModuleAuditControl.ascx" %>
<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
</telerik:RadAjaxManager>
&nbsp;&nbsp;&nbsp;&nbsp;
<%--<telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">

            <script type="text/javascript">
                function onFocus(sender, eventArgs){
                    $find("<%= txtSearch.ClientID %>").focus();
                }
        
            </script>

        </telerik:RadCodeBlock>
--%>
<table border="0" cellpadding="0" cellspacing="0" width="100%">
    <tr class="m-frm-hdr">
        <td>
            <table cellspacing="0" border="0" class="m-frm-bdr" cellpadding="1" width="100%">
                <tr>
                    <td>
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr class="m-frm-hdr">
                                <td align="left" class="m-frm-hdr">
                                    <table border="0" cellpadding="0" cellspacing="0" cols="2">
                                        <tr>
                                            <td class="groupboxheader" nowrap="nowrap">
                                                <font class="groupbox">&nbsp; ค้นหาข้อมูลใบเสร็จ &nbsp;</font></td>
                                            <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
                                                <td style='background-image: url(<%= ResolveUrl("images") %>/CORNER.gif); background-repeat: no-repeat;'>
                                                    &nbsp; &nbsp;&nbsp;</td>
                                            </telerik:RadCodeBlock>
                                        </tr>
                                    </table>
                                    <asp:Label ID="lblRoleID" runat="server" Text="lblRoleID" Visible="False"></asp:Label></td>
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
                                <td width="15">
                                    &nbsp;</td>
                                <td>
                                    <table width="100%">
                                        <tr>
                                            <td style="width: 130px; text-align: right">
                                                <font class="FormLabel">เลขที่ใบเสร็จที่ค้นหา :</font>
                                            </td>
                                            <td>
                                                <telerik:RadTextBox ID="txtSearchRe" runat="server" Width="225px" ForeColor="Teal"
                                                    Font-Bold="True" Font-Size="Large">
                                                </telerik:RadTextBox>
                                                <asp:Button ID="btnSearch" runat="server" Text="ค้นหา" ValidationGroup="search" Width="150px" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtSearchRe"
                                                    Display="Dynamic" ErrorMessage="กรุณาป้อนเลขใบเสร็จ" Font-Names="Tahoma" Font-Size="10pt"
                                                    SetFocusOnError="True" ValidationGroup="search"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 130px; text-align: right">
                                                <font class="FormLabel">ประเภทใบเสร็จรับเงิน :</font>
                                            </td>
                                            <td>
                                                <asp:RadioButtonList ID="rblRecieptType" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Text="ใบเสร็จสวัสดิการ (ใบเสร็จเขียว)" Value="0" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Text="ใบเสร็จกองทุน (ใบเสร็จเหลือง)" Value="1"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="m-frm" valign="top" width="100%">
                    </td>
                </tr>
                <tr>
                    <td class="m-frm" valign="top" width="100%">
                        <table cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td colspan="2" valign="top">
                                    <table cellpadding="0" cellspacing="2" width="50%">
                                        <tr>
                                            <td>
                                                <table cellpadding="0" cellspacing="0" width="100%" class="FormLabel">
                                                    <tr>
                                                        <td>
                                                            <asp:Panel ID="Panel1" runat="server" GroupingText="ส่วนเพิ่มรายการใบเสร็จ" Width="100%">
                                                                <table>
                                                                    <tr>
                                                                        <td style="width: 50%">
                                                                            เลขที่ใบเสร็จที่อ้างอิง <telerik:RadTextBox ID="txtSearch" runat="server" Width="225px"
                                                                                ForeColor="Teal" Font-Bold="True" Font-Size="Large">
                                                                            </telerik:RadTextBox>
                                                                            <br />
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                                                                ControlToValidate="txtSearch" Display="Dynamic" ErrorMessage="กรุณาป้อนเลขใบเสร็จ"
                                                                                Font-Names="Tahoma" Font-Size="10pt" SetFocusOnError="True" ValidationGroup="search2"></asp:RequiredFieldValidator></td>
                                                                        <td style="width: 50%">
                                                                            เลขที่ใบเสร็จที่ออกไม่ได้ <telerik:RadTextBox ID="txtsearch2" runat="server" Width="225px"
                                                                                Font-Bold="True" Font-Size="Large" ForeColor="Teal">
                                                                            </telerik:RadTextBox>
                                                                            <br />
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                                                                ControlToValidate="txtsearch2" Display="Dynamic" ErrorMessage="กรุณาป้อนเลขใบเสร็จ"
                                                                                Font-Names="Tahoma" Font-Size="10pt" SetFocusOnError="True" ValidationGroup="search2"></asp:RequiredFieldValidator></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="2" style="font-size: 12pt; color: #000000; font-family: Times New Roman; text-align: center">
                                                                            <asp:Label ID="lbl_ErrMSG" runat="server" ForeColor="Red"></asp:Label></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="2" style="font-size: 12pt; color: #000000; font-family: Times New Roman; text-align: center">
                                                                            <asp:Button ID="btnUpdate" runat="server" Text="เพิ่มใบเสร็จเป็นศูนย์" ValidationGroup="search2"
                                                                                Width="150px" /></td>
                                                                    </tr>
                                                                </table>
                                                            </asp:Panel>
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
                    <td class="m-frm" valign="top" width="100%">
                        <telerik:RadGrid ID="GridZero" runat="server" PageSize="40" Skin="Vista" TabIndex="-1">
                            <MasterTableView AutoGenerateColumns="False" ClientDataKeyNames="bill_no" DataKeyNames="bill_no,site_id"
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
                                    <telerik:GridBoundColumn DataField="bill_no" HeaderText="เลขที่ใบเสร็จ" ReadOnly="True"
                                        SortExpression="bill_no" UniqueName="bill_no">
                                        <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                        <HeaderStyle Width="12%" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="invh_run_auto" HeaderText="เลขที่อ้างอิง" ReadOnly="True"
                                        SortExpression="invh_run_auto" UniqueName="invh_run_auto" Visible="true">
                                        <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                        <HeaderStyle Width="15%" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="reference_code1" HeaderText="reference_code1"
                                        ReadOnly="True" SortExpression="reference_code1" UniqueName="reference_code1"
                                        Visible="False">
                                        <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="reference_code2" HeaderText="เลขหนังสือรับรอง"
                                        ReadOnly="True" SortExpression="reference_code2" UniqueName="reference_code2">
                                        <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                        <HeaderStyle Width="14%" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="total_set" HeaderText="จำนวนชุด" ReadOnly="True"
                                        SortExpression="total_set" UniqueName="total_set">
                                        <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                        <HeaderStyle Width="5%" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="set_price" HeaderText="ราคา(ต่อหน่วย)" ReadOnly="True"
                                        SortExpression="set_price" UniqueName="set_price">
                                        <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                        <HeaderStyle Width="12%" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="amt" HeaderText="ราคารวม" ReadOnly="True" SortExpression="amt"
                                        UniqueName="amt">
                                        <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                        <HeaderStyle Width="14%" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="amt_bahttext" HeaderText="ยอดรวม(ตัวหนังสือ)"
                                        ReadOnly="True" SortExpression="amt_bahttext" UniqueName="amt_bahttext">
                                        <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                        <HeaderStyle Width="14%" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="rpt_set" HeaderText="rpt_set" ReadOnly="True"
                                        SortExpression="rpt_set" UniqueName="rpt_set" Visible="false">
                                        <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                        <HeaderStyle Width="14%" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="site_id" HeaderText="สถานที่" ReadOnly="True"
                                        SortExpression="site_id" UniqueName="site_id">
                                        <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                        <HeaderStyle Width="7%" />
                                    </telerik:GridBoundColumn>
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
        </td>
    </tr>
</table>
