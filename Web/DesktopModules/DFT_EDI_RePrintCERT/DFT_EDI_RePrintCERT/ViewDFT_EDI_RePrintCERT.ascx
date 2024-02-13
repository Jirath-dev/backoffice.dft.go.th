<%@ Control language="vb" Inherits="NTi.Modules.DFT_EDI_RePrintCERT.ViewDFT_EDI_RePrintCERT" AutoEventWireup="false" Explicit="True" Codebehind="ViewDFT_EDI_RePrintCERT.ascx.vb" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="CheckSelectDate">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="CheckSelectDate" />
                <telerik:AjaxUpdatedControl ControlID="rdpSelectDatePrint" />
                <telerik:AjaxUpdatedControl ControlID="rfvSelectDatePrint" />
            </UpdatedControls>
        </telerik:AjaxSetting>
    </AjaxSettings>
</telerik:RadAjaxManager>
<table border="0" cellpadding="0" cellspacing="3" width="100%">
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
                                                            <font class="groupbox">&nbsp; ค้นหาคำร้องที่ต้องการพิมพ์หนังสือรับรองฯ (ย้อนหลัง) &nbsp;</font></td>
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
                                            <td width="15">
                                                &nbsp;</td>
                                            <td>
                                                <table border="0" cellpadding="0" cellspacing="2">
                                                    <tr>
                                                        <td>
                                                            <font class="FormLabel">หมายเลขคำร้อง :</font>
                                                            <asp:TextBox ID="txtSearchValue" runat="server" Font-Bold="True" Font-Names="Tahoma"
                                                                Font-Size="12pt" ForeColor="Blue" Width="170px">20081230-001028</asp:TextBox>&nbsp;
                                                        </td>
                                                        <td>
                                                            <font class="FormLabel"></font>
                                                            <asp:CheckBox ID="chkUseRef2" runat="server" Font-Names="Tahoma" Font-Size="10pt"
                                                                Text="ใช้เลขที่หนังสือรับรอง" /></td>
                                                        <td>
                                                            <asp:CheckBox ID="check_YearsOle" runat="server" Font-Names="Tahoma" Font-Size="10pt"
                                                                Text="กรณีหาข้อมูลจากปีเก่า (2008 ลงมา)" AutoPostBack="True" /></td>
                                                        <td>
                                                            <asp:DropDownList ID="DDLYears" runat="server" Enabled="False" Width="120px">
                                                            </asp:DropDownList></td>
                                                        <td>
                                                            &nbsp;
                                                            <asp:Button ID="btnSearch" runat="server" Text="ค้นหา" Width="150px" />
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
<table id="tblData1" runat="server" border="0" cellpadding="0" cellspacing="3" width="100%">
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
                                                            <font class="groupbox">&nbsp; รายการคำร้อง &nbsp;</font></td>
                                                        <telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">
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
                            <tr class="m-frm-hdr">
                                <td>
                                    <asp:Panel ID="PanelSelectPrint" runat="server" BackColor="#C0C0FF" CssClass="FormLabel"
                                        GroupingText="เลือกหน้าพิมพ์" Width="30%">
                                        <table border="0" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td align="right" class="FormLabel">
                                                    หน้าแรก :</td>
                                                <td align="left" class="FormLabel">
                                                    <telerik:RadNumericTextBox ID="txtFirstpage" runat="server" Culture="Thai (Thailand)"
                                                        DataType="System.Single" EnableEmbeddedSkins="False" ForeColor="Blue" MaxValue="1000"
                                                        MinValue="0" ShowSpinButtons="False" TabIndex="-1" Width="54px">
                                                        <NumberFormat AllowRounding="False" DecimalDigits="0" />
                                                    </telerik:RadNumericTextBox>
                                                </td>
                                                <td align="right" class="FormLabel">
                                                    หน้าสุดท้าย :</td>
                                                <td align="left" class="FormLabel">
                                                    <telerik:RadNumericTextBox ID="txtLastpage" runat="server" Culture="Thai (Thailand)"
                                                        DataType="System.Single" EnableEmbeddedSkins="False" ForeColor="Blue" MaxValue="1000"
                                                        MinValue="0" ShowSpinButtons="False" TabIndex="-1" Width="54px">
                                                        <NumberFormat AllowRounding="False" DecimalDigits="0" />
                                                    </telerik:RadNumericTextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td class="m-frm" valign="top" width="100%">
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                        <tr>
                                            <td>
                                                <table style="width: 100%">
                                                    <tr>
                                                        <td>
                                                            <table border="0" cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <td style="width: 194px">
                                                                        <asp:Button ID="btnPrint" runat="server" Text="พิมพ์" Width="150px" ValidationGroup="Print" /></td>
                                                                    <td>
                                                                        <asp:RadioButtonList ID="RBListPrinter" runat="server" Font-Names="Tahoma" Font-Size="10pt"
                                                                            RepeatDirection="Horizontal" Width="100%">
                                                                            <asp:ListItem Value="0" Selected="True">จอภาพ</asp:ListItem>
                                                                            <asp:ListItem Value="1">เครื่องพิมพ์</asp:ListItem>
                                                                        </asp:RadioButtonList></td>
                                                                    <td>
                                                                        <asp:RadioButtonList ID="RBListFormRequest" runat="server" Font-Names="Tahoma" Font-Size="10pt"
                                                                            RepeatDirection="Horizontal" Width="100%">
                                                                            <asp:ListItem Value="0">แบบคำขอ</asp:ListItem>
                                                                            <asp:ListItem Selected="True" Value="1">หนังสือรับรองฯ</asp:ListItem>
                                                                        </asp:RadioButtonList></td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="right" class="FormLabel" style="width: 194px">
                                                                        เครื่องพิมพ์คำขอ :</td>
                                                                    <td class="FormLabel" colspan="2">
                                                                        <asp:RadioButtonList ID="rdblReceiptPrinter" runat="server" RepeatDirection="Horizontal">
                                                                        </asp:RadioButtonList>
                                                                        <asp:Label ID="lbl_ErrMSG" runat="server" Font-Names="Tahoma" Font-Size="10pt" ForeColor="Red"></asp:Label></td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" class="FormLabel" style="width: 194px">
                                                                        <asp:Panel ID="Panel_A" runat="server" BackColor="#C0C0FF" CssClass="FormLabel" GroupingText="เครื่องฟอร์ม A"
                                                                            Visible="False" Width="100%">
                                                                            <asp:RadioButtonList ID="rdoA_ALL" runat="server" CssClass="FormLabel" RepeatColumns="3"
                                                                                RepeatDirection="Horizontal">
                                                                                <asp:ListItem Selected="True" Value="A1">เครื่องที่ 1</asp:ListItem>
                                                                                <asp:ListItem Value="A2">เครื่องที่ 2</asp:ListItem>
                                                                            </asp:RadioButtonList></asp:Panel>
                                                                    </td>
                                                                    <td class="FormLabel" colspan="2">
                                                                        <asp:Panel ID="Panel_D" runat="server" BackColor="#C0C0FF" CssClass="FormLabel" GroupingText="เครื่อง ฟอร์ม D"
                                                                            Visible="False" Width="100%">
                                                                            <asp:RadioButtonList ID="rdoD_ALL" runat="server" CssClass="FormLabel" RepeatColumns="3"
                                                                                RepeatDirection="Horizontal">
                                                                                <asp:ListItem Selected="True" Value="D1">เครื่องที่ 1</asp:ListItem>
                                                                                <asp:ListItem Value="D2">เครื่องที่ 2</asp:ListItem>
                                                                            </asp:RadioButtonList></asp:Panel>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" class="FormLabel" style="width: 194px">
                                                                    </td>
                                                                    <td class="FormLabel" colspan="2">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" class="FormLabel" style="width: 194px">
                                                                        <asp:CheckBox ID="CheckSelectDate" runat="server" AutoPostBack="True" CssClass="FormLabel"
                                                                            ForeColor="Red" Text="กรณีเลือกวันที่พิมพ์เอง" /></td>
                                                                    <td class="FormLabel" colspan="2">
                                                                        <telerik:RadDatePicker ID="rdpSelectDatePrint" runat="server" Culture="English (United Kingdom)"
                                                                            Enabled="False" Skin="Vista" Width="200px">
                                                                            <Calendar Skin="Vista" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                                                ViewSelectorText="x">
                                                                            </Calendar>
                                                                            <DatePopupButton HoverImageUrl="" ImageUrl="" />
                                                                        </telerik:RadDatePicker>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="FormLabel" colspan="3" style="height: 21px">
                                                                        <asp:CheckBox ID="ChkTrueCopy" runat="server" CssClass="FormLabel" ForeColor="Red"
                                                                            Text="Certified True Copy (กรณีพิมพ์ฟอร์มซ้ำเนื่องจากฟอร์มสูญหาย)" Visible="False" /></td>
                                                                </tr>
                                                                <tr>
                                                                    <td id="TDChkTrueCopy" align="left" class="FormLabel" visible="False" style="width: 194px">
                                                                        &nbsp;</td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 100%">
                                                            <telerik:RadGrid ID="rgRequestList" runat="server" AllowSorting="True" Font-Names="Tahoma"
                                                                Font-Size="X-Small" GridLines="None" Skin="Office2007" TabIndex="-1">
                                                                <MasterTableView AutoGenerateColumns="False" DataKeyNames="invh_run_auto,form_name"
                                                                    NoMasterRecordsText="ไม่มีคำร้องขอออกหนังสือรับรองถิ่นกำเนิดสินค้า" Width="100%">
                                                                    <Columns>
                                                                        <telerik:GridBoundColumn DataField="form_name" HeaderText="ชื่อฟอร์ม" ReadOnly="True"
                                                                            SortExpression="form_name" UniqueName="form_name">
                                                                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt"/>
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="form_type" HeaderText="ชื่อฟอร์ม" ReadOnly="True"
                                                                            SortExpression="form_type" UniqueName="form_type" Visible="false">
                                                                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt"/>
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="invh_run_auto" HeaderText="เลขที่อ้างอิง" ReadOnly="True"
                                                                            SortExpression="invh_run_auto" UniqueName="invh_run_auto">
                                                                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="reference_code2" HeaderText="เลขที่หนังสือรับรองฯ"
                                                                            ReadOnly="True" SortExpression="reference_code2" UniqueName="reference_code2">
                                                                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="edi_status" HeaderText="สถานะ" ReadOnly="True"
                                                                            SortExpression="edi_status" UniqueName="edi_status">
                                                                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt"/>
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="request_person" HeaderText="ผู้ขอ" ReadOnly="True"
                                                                            SortExpression="request_person" UniqueName="request_person">
                                                                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="approve_date" HeaderText="วันที่อนุมัติ" ReadOnly="True" SortExpression="approve_date"
                                                                            UniqueName="approve_date" DataFormatString="{0:dd/MM/yyyy HH:MM:ss}">
                                                                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="print_no" HeaderText="ครั้งที่พิมพ์"
                                                                            ReadOnly="True" SortExpression="print_no" UniqueName="print_no">
                                                                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center"/>
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="print_flag" HeaderText="พิมพ์" ReadOnly="True"
                                                                            SortExpression="print_flag" UniqueName="print_flag">
                                                                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="totalPrintPage" HeaderText="จำนวนหน้า" ReadOnly="True"
                                                                            SortExpression="totalPrintPage" UniqueName="totalPrintPage">
                                                                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                        </telerik:GridBoundColumn>
                                                                    </Columns>
                                                                </MasterTableView>
                                                                <HeaderStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                <ClientSettings EnableRowHoverStyle="True">
                                                                    <Selecting AllowRowSelect="True" />
                                                                </ClientSettings>
                                                            </telerik:RadGrid></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 100%">
                                                            <font class="FormLabel">ระบุเหตุผลการพิมพ์หนังสือรับรอง&nbsp;&nbsp;</font>
                                                            <asp:TextBox ID="txtRePrint_Remark" runat="server" Height="100px" TextMode="MultiLine" Width="400px"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtRePrint_Remark"
                                                                ErrorMessage="กรุณาระบุเหตุผลการพิมพ์หนังสือรับรอง" Font-Names="Tahoma" Font-Size="10pt"
                                                                SetFocusOnError="True" ValidationGroup="Print"></asp:RequiredFieldValidator></td>
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
<asp:Label ID="lblDSRoleID" runat="server" Visible="False"></asp:Label>
