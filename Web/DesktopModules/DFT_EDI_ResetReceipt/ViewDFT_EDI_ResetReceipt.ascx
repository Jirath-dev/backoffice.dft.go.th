<%@ Control Language="vb" Inherits="Nti.Modules.DFT_EDI_ResetReceipt.ViewDFT_EDI_ResetReceipt"
    AutoEventWireup="false" Explicit="True" Codebehind="ViewDFT_EDI_ResetReceipt.ascx.vb" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="dnn" TagName="Audit" Src="~/controls/ModuleAuditControl.ascx" %>
<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="ChkFees">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="txtCompanyName" />
            </UpdatedControls>
        </telerik:AjaxSetting>
    </AjaxSettings>
</telerik:RadAjaxManager>
<br />
<table border="0" class="m-frm-bdr" cellpadding="0" cellspacing="0" width="100%">
    <tr class="m-frm-hdr">
        <td>
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr class="m-frm-hdr">
                    <td align="left" class="m-frm-hdr">
                        <table border="0" cellpadding="0" cellspacing="0" cols="2">
                            <tr>
                                <td class="groupboxheader" nowrap="nowrap">
                                    <font class="groupbox">&nbsp; ค้นหาข้อมูลใบเสร็จ ที่จะทำเป็นศูนย์ &nbsp;</font></td>
                                <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
                                    <td style='background-image: url(<%= ResolveUrl("images") %>/CORNER.gif); background-repeat: no-repeat;'>
                                        &nbsp; &nbsp;&nbsp;
                                    </td>
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
                        <table border="0" cellpadding="0" cellspacing="2" width="100%">
                            <tr>
                                <td style="font-size: 12pt; color: #000000; font-family: Times New Roman; width:140px;">
                                    <font class="FormLabel">ประเภทใบเสร็จที่ค้นหา :&nbsp;&nbsp; </font>
                                </td>
                                <td>
                                    <asp:RadioButtonList ID="rblBillType" runat="server" RepeatDirection="Horizontal"
                                        RepeatColumns="3">
                                        <asp:ListItem Text="ใบเสร็จสวัสดิการ (ใบเสร็จเขียว)" Value="1" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="ใบเสร็จกองทุน (ใบเสร็จเหลือง)" Value="2"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                        <table border="0" cellpadding="0" cellspacing="2" width="100%">
                            <tr>
                                <td style="font-size: 12pt; color: #000000; font-family: Times New Roman; width:140px;">
                                    <font class="FormLabel">เลขที่ใบเสร็จที่ค้นหา :&nbsp;&nbsp; </font>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="txtSearchRe" runat="server" Width="225px" ForeColor="Teal"
                                        Font-Bold="True" Font-Size="Large">
                                    </telerik:RadTextBox>
                                    <asp:Button ID="btnSearch" runat="server" Text="ค้นหา" ValidationGroup="search" Width="150px" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtSearchRe"
                                        Display="Dynamic" ErrorMessage="กรุณาป้อนเลขใบเสร็จ" Font-Names="Tahoma" Font-Size="10pt"
                                        SetFocusOnError="True" ValidationGroup="search">
                                    </asp:RequiredFieldValidator>
                                </td>
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
        <td class="m-frm" valign="top" width="100%">
            <asp:Panel ID="PanelReset" runat="server" Visible="False" Width="100%">
                <table border="0" cellpadding="0" cellspacing="4" style="width: 100%">
                    <tr>
                        <td align="right" class="FormLabel" style="width: 9%" valign="top">
                        </td>
                        <td class="FormLabel" style="width: 80%" valign="top">
                            <asp:CheckBox ID="ChkFees" runat="server" Text="กรณีได้รับการยกเว้นค่าธรรมเนียม" AutoPostBack="True" />
                            </td>
                    </tr>
                     <tr>
                        <td align="right" class="FormLabel" style="width: 9%" valign="top">
                        </td>
                        <td class="FormLabel" style="width: 80%" valign="top">
                            <asp:TextBox ID="txtCompanyName" runat="server" Width="700px" Visible="false"></asp:TextBox>
                            </td>
                    </tr>
                    <tr>
                        <td align="right" class="FormLabel" style="width: 9%" valign="top">
                            หมายเหตุ :</td>
                        <td class="FormLabel" style="width: 80%" valign="top">
                            <asp:TextBox ID="txtRemark" runat="server" Text="-" Height="60px" TextMode="MultiLine" Width="425px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtRemark"
                                Display="Dynamic" ErrorMessage="กรุณาป้อนเหตุผลการแก้ไข" Font-Names="Tahoma"
                                Font-Size="10pt" SetFocusOnError="True" ValidationGroup="Greset"></asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td align="right" class="FormLabel" style="width: 9%" valign="top">
                        </td>
                        <td class="FormLabel" style="width: 80%" valign="top">
                            <asp:Button ID="btnReset" runat="server" Text="บันทึกรายการใบเสร็จให้เป็นศูนย์" ValidationGroup="Greset"
                                Width="200px" /></td>
                    </tr>
                </table>
            </asp:Panel>
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
                        <telerik:GridBoundColumn DataField="receipt_name" HeaderText="ชื่อบริษัท" ReadOnly="True"
                            SortExpression="receipt_name" UniqueName="receipt_name">
                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                            <HeaderStyle Width="30%" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="bill_no" HeaderText="เลขที่ใบเสร็จ" ReadOnly="True"
                            SortExpression="bill_no" UniqueName="bill_no">
                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                            <HeaderStyle Width="8%" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="invh_run_auto" HeaderText="เลขที่อ้างอิง" ReadOnly="True"
                            SortExpression="invh_run_auto" UniqueName="invh_run_auto" Visible="true">
                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                            <HeaderStyle Width="10%" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="reference_code1" HeaderText="reference_code1"
                            ReadOnly="True" SortExpression="reference_code1" UniqueName="reference_code1"
                            Visible="False">
                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="reference_code2" HeaderText="เลขหนังสือรับรอง"
                            ReadOnly="True" SortExpression="reference_code2" UniqueName="reference_code2">
                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                            <HeaderStyle Width="10%" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="total_set" HeaderText="จำนวนชุด" ReadOnly="True"
                            SortExpression="total_set" UniqueName="total_set">
                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                            <HeaderStyle Width="5%" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="set_price" HeaderText="ราคา(ต่อหน่วย)" ReadOnly="True"
                            SortExpression="set_price" UniqueName="set_price">
                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                            <HeaderStyle Width="8%" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="amt" HeaderText="ราคารวม" ReadOnly="True" SortExpression="amt"
                            UniqueName="amt">
                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                            <HeaderStyle Width="8%" />
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
            </telerik:RadGrid></td>
    </tr>
</table>
