<%@ Control Language="vb" Inherits="NTi.Modules.DFT_EDI_AttachDoc.ViewDFT_EDI_AttachDoc" AutoEventWireup="false" Explicit="True" Codebehind="ViewDFT_EDI_AttachDoc.ascx.vb" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="RadAjaxManager1">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="rgAttachDocList" />
                <telerik:AjaxUpdatedControl ControlID="rgEditAttDoc" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="chkTaxno">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="txtCompany_Taxno" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="rgAttachDocList">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="rgAttachDocList" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="rgEditAttDoc">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="rgEditAttDoc" />
            </UpdatedControls>
        </telerik:AjaxSetting>
    </AjaxSettings>
</telerik:RadAjaxManager>
<telerik:RadCodeBlock ID="RadCodeBlockFirst" runat="server">

    <script type="text/javascript">
       function ShowConfirmDialog(tcat, invh, remark, user, site) {
           window.radopen("/DesktopModules/DFT_EDI_AttachDoc/frmConfirmDialog.aspx?TCat=" + tcat + "&Invh=" + invh + "&Remark=" + remark + "&UserID=" + user + "&SiteID=" + site, "ConfirmDialog")
           return false;
       }
       
       function refreshGrid(arg)
       {
            if(!arg)
            {
                $find("<%= RadAjaxManager1.ClientID %>").ajaxRequest("Rebind");			        
            }
            else
            {
                $find("<%= RadAjaxManager1.ClientID %>").ajaxRequest("RebindAndNavigate");			        
            }
        }
    </script>

</telerik:RadCodeBlock>
<telerik:RadWindowManager ID="RadWindowManager1" Behavior="Close" runat="server"
    Skin="Office2007" TabIndex="-1">
    <Windows>
        <telerik:RadWindow ID="ConfirmDialog" Animation="Slide" runat="server" ReloadOnShow="True"
            ShowContentDuringLoad="False" Modal="True" Width="450px" Height="160px" VisibleStatusbar="False" />
    </Windows>
</telerik:RadWindowManager>
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
                                    Text="การแนบเอกสาร">
                                </telerik:RadTab>
                                <telerik:RadTab runat="server" Font-Names="Tahoma" Font-Size="10pt" Text="จัดการเอกสารแนบ" Visible="false">
                                </telerik:RadTab>
                                <telerik:RadTab runat="server" Font-Names="Tahoma" Font-Size="10pt" Text="ใบอนุญาตที่ถูกยกเลิก" Visible="false">
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
                                                                                                                    <asp:Panel ID="Panel4" runat="server" BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px">
                                                                                                                        <asp:RadioButtonList ID="rblTypeRepAtt" runat="server" RepeatDirection="Horizontal">
                                                                                                                            <asp:ListItem Selected="True" Value="1">แนบเอกสาร</asp:ListItem>
                                                                                                                            <asp:ListItem Value="2">ยกเลิกการแนบฯ</asp:ListItem>
                                                                                                                        </asp:RadioButtonList></asp:Panel>
                                                                                                                </td>
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
                                    <tr id="xxxx" runat="server" visible="false">
                                        <td>
                                            <table style="width: 100%">
                                                <tr>
                                                    <td valign="top" class="FormLabel">
                                                        <asp:Panel ID="Panel1" runat="server" BackColor="Transparent" BorderStyle="None"
                                                            GroupingText="สืบค้นการบันทึกเอกสารแนบ">
                                                            <table style="width: 100%">
                                                                <tr>
                                                                    <td style="width: 100%">
                                                                        <font class="FormLabel">ตั้งแต่</font>
                                                                        <telerik:RadDatePicker ID="rdpFromDate" runat="server" Culture="English (United Kingdom)"
                                                                            Skin="Vista">
                                                                            <Calendar DayNameFormat="FirstTwoLetters" ShowRowHeaders="False" Skin="Vista" UseColumnHeadersAsSelectors="False"
                                                                                UseRowHeadersAsSelectors="False" ViewSelectorText="x" FirstDayOfWeek="Sunday">
                                                                            </Calendar>
                                                                            <DatePopupButton HoverImageUrl="" ImageUrl="" />
                                                                        </telerik:RadDatePicker>
                                                                        <font class="FormLabel">&nbsp;ถึง</font>
                                                                        <telerik:RadDatePicker ID="rdpToDate" runat="server" Culture="English (United Kingdom)"
                                                                            Skin="Vista">
                                                                            <Calendar DayNameFormat="FirstTwoLetters" ShowRowHeaders="False" Skin="Vista" UseColumnHeadersAsSelectors="False"
                                                                                UseRowHeadersAsSelectors="False" ViewSelectorText="x" FirstDayOfWeek="Sunday">
                                                                            </Calendar>
                                                                            <DatePopupButton HoverImageUrl="" ImageUrl="" />
                                                                        </telerik:RadDatePicker>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                    </td>
                                                    <td valign="top" class="FormLabel">
                                                        <asp:Panel ID="Panel2" runat="server" BackColor="Transparent" BorderStyle="None"
                                                            GroupingText="รูปแบบการสืบค้น">
                                                            <table style="width: 100%">
                                                                <tr>
                                                                    <td>
                                                                        <asp:RadioButton ID="rbType1" runat="server" GroupName="TypeSearch" Text="อนุมัติผ่านเอกสารแนบ"
                                                                            Checked="True" />
                                                                        <asp:RadioButton ID="rbType2" runat="server" GroupName="TypeSearch" Text="รอเอกสารภายใน" />
                                                                        <asp:TextBox ID="txtTotalDay" runat="server" Width="25px">10</asp:TextBox>
                                                                        <font class="FormLabel">วัน</font>
                                                                        <asp:RadioButton ID="rbType3" runat="server" GroupName="TypeSearch" Text="เลยกำหนดเอกสารแนบ" /></td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="FormLabel" valign="top">
                                                    </td>
                                                    <td class="FormLabel" valign="top">
                                                        <asp:Panel ID="Panel3" runat="server" BackColor="Transparent" BorderStyle="None"
                                                            GroupingText="สถานที่ออกหนังสือ">
                                                            <table style="width: 100%">
                                                                <tr>
                                                                    <td>
                                                                        <telerik:RadComboBox ID="rcbSitePlus" runat="server" Skin="Web20" Filter="StartsWith"
                                                                            Font-Names="Tahoma" Font-Size="10pt" MarkFirstMatch="True" Width="280px">
                                                                        </telerik:RadComboBox>
                                                                        &nbsp;
                                                                        <asp:ImageButton ID="ibSearch" runat="server" ImageUrl="~/images/search.gif" /></td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <table border="0" cellpadding="0" cellspacing="0">
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:CheckBox ID="chkTaxno" runat="server" Text="ระบุบริษัทฯ (หมายเลขประจำตัวผู้เสียภาษี)"
                                                                                        AutoPostBack="True" />&nbsp;</td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtCompany_Taxno" runat="server" Width="150px" Enabled="false" BackColor="LightGray"></asp:TextBox></td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
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
                                                ShowFooter="True">
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
                                                        <telerik:GridBoundColumn DataField="approve_by" HeaderText="โดย" ReadOnly="True"
                                                            SortExpression="approve_by" UniqueName="approve_by">
                                                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="site_id" HeaderText="สถานที่" ReadOnly="True"
                                                            SortExpression="site_id" UniqueName="site_id">
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
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </telerik:RadPageView>
                            <telerik:RadPageView ID="RadPageView2" runat="server" TabIndex="-1" Width="100%">
                                <table border="0" cellpadding="1" cellspacing="0" class="m-frm-bdr" width="100%">
                                    <tr>
                                        <td class="FormLabel">
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
                                                                                                        <font class="groupbox">&nbsp; การส่ง/รับคืนเอกสาร &nbsp;</font></td>
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
                                                                        <tr>
                                                                            <td class="m-frm" valign="top" width="100%">
                                                                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                                    <tr>
                                                                                        <td>
                                                                                            <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="100%">
                                                                                                <table style="width: 100%" cellpadding="3" cellspacing="3">
                                                                                                    <tr>
                                                                                                        <td valign="top">
                                                                                                            <asp:Panel ID="Panel5" runat="server" BackColor="Transparent" BorderStyle="None"
                                                                                                                GroupingText="เอกสาร" Width="100%">
                                                                                                                <asp:RadioButtonList ID="rblEditType" runat="server" RepeatDirection="Horizontal"
                                                                                                                    AutoPostBack="True">
                                                                                                                    <asp:ListItem Selected="True" Value="1">แจ้งแก้ไข</asp:ListItem>
                                                                                                                    <asp:ListItem Value="2">ส่งแก้ไข</asp:ListItem>
                                                                                                                    <asp:ListItem Value="3">รับคืน</asp:ListItem>
                                                                                                                </asp:RadioButtonList></asp:Panel>
                                                                                                        </td>
                                                                                                        <td valign="top">
                                                                                                            <asp:Panel ID="Panel6" runat="server" BackColor="Transparent" BorderStyle="None"
                                                                                                                GroupingText="หมายเลขอ้างอิง (Barcode)" Width="100%">
                                                                                                                <table style="width: 100%">
                                                                                                                    <tr>
                                                                                                                        <td style="width: 100%; text-align: center;">
                                                                                                                            <asp:TextBox ID="txtINVH_RUN_AUTO2" runat="server" Width="195px" MaxLength="15"></asp:TextBox></td>
                                                                                                                    </tr>
                                                                                                                    <tr>
                                                                                                                        <td style="width: 100%; text-align: center;">
                                                                                                                            <asp:Button ID="btnEditDoc" runat="server" Text="ดำนินการ  แจ้งแก้ไข" Width="200px" /></td>
                                                                                                                    </tr>
                                                                                                                </table>
                                                                                                            </asp:Panel>
                                                                                                        </td>
                                                                                                        <td valign="top">
                                                                                                            <asp:Panel ID="Panel7" runat="server" BackColor="Transparent" BorderStyle="None"
                                                                                                                GroupingText="เหตุผลการส่งคืนเอกสาร">
                                                                                                                <table style="width: 100%">
                                                                                                                    <tr>
                                                                                                                        <td style="width: 100%">
                                                                                                                            <asp:TextBox ID="txtRemark_docatt" runat="server" Height="70px" TextMode="MultiLine"
                                                                                                                                Width="400px"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                </table>
                                                                                                            </asp:Panel>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                            </telerik:RadAjaxPanel>
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
                                        <td class="FormLabel">
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
                                                                                                        <font class="groupbox">&nbsp; การสืบค้นการส่งเอกสารคืน &nbsp;</font></td>
                                                                                                    <telerik:RadCodeBlock ID="RadCodeBlock3" runat="server">
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
                                                                                            <table style="width: 100%">
                                                                                                <tr>
                                                                                                    <td style="width: 100%">
                                                                                                        <asp:Panel ID="Panel8" runat="server" BackColor="Transparent" BorderStyle="None"
                                                                                                            GroupingText="กำหนดเวลา" Width="100%">
                                                                                                            <table>
                                                                                                                <tr>
                                                                                                                    <td>
                                                                                                                        <table style="width: 100%">
                                                                                                                            <tr>
                                                                                                                                <td style="width: 100%">
                                                                                                                                    <font class="FormLabel">ตั้งแต่</font>
                                                                                                                                    <telerik:RadDatePicker ID="rdpFromDateTab2" runat="server" Culture="English (United Kingdom)"
                                                                                                                                        Skin="Vista">
                                                                                                                                        <Calendar DayNameFormat="FirstTwoLetters" ShowRowHeaders="False" Skin="Vista" UseColumnHeadersAsSelectors="False"
                                                                                                                                            UseRowHeadersAsSelectors="False" ViewSelectorText="x" FirstDayOfWeek="Sunday">
                                                                                                                                        </Calendar>
                                                                                                                                        <DatePopupButton HoverImageUrl="" ImageUrl="" />
                                                                                                                                    </telerik:RadDatePicker>
                                                                                                                                    <font class="FormLabel">&nbsp;ถึง</font>
                                                                                                                                    <telerik:RadDatePicker ID="rdpToDateTab2" runat="server" Culture="English (United Kingdom)"
                                                                                                                                        Skin="Vista">
                                                                                                                                        <Calendar DayNameFormat="FirstTwoLetters" ShowRowHeaders="False" Skin="Vista" UseColumnHeadersAsSelectors="False"
                                                                                                                                            UseRowHeadersAsSelectors="False" ViewSelectorText="x" FirstDayOfWeek="Sunday">
                                                                                                                                        </Calendar>
                                                                                                                                        <DatePopupButton HoverImageUrl="" ImageUrl="" />
                                                                                                                                    </telerik:RadDatePicker>
                                                                                                                                </td>
                                                                                                                            </tr>
                                                                                                                        </table>
                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <td>
                                                                                                                        <asp:RadioButtonList ID="rblTypeSearch" runat="server" RepeatDirection="Horizontal">
                                                                                                                            <asp:ListItem Selected="True" Value="1">แจ้งแก้ไข (ยังไม่รับเอกสาร)</asp:ListItem>
                                                                                                                            <asp:ListItem Value="2">อยู่ในกำหนดเวลา (ส่งแก้ไขแล้ว)</asp:ListItem>
                                                                                                                            <asp:ListItem Value="3">เลยกำหนดส่งคืน 3 วัน</asp:ListItem>
                                                                                                                            <asp:ListItem Value="4">รับคืนเอกสาร</asp:ListItem>
                                                                                                                        </asp:RadioButtonList></td>
                                                                                                                    <td>
                                                                                                                        <asp:Button ID="btnSearchAtt" runat="server" Text="สืบค้น" Width="100px" /></td>
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
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="FormLabel">
                                            <table style="width: 100%">
                                                <tr>
                                                    <td style="width: 100%">
                                                        <telerik:RadGrid ID="rgEditAttDoc" runat="server" AllowPaging="True" AllowSorting="True"
                                                            GridLines="None" ShowStatusBar="True" Skin="Office2007" TabIndex="-1" Width="100%"
                                                            ShowFooter="True">
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
                                                                        HeaderText="วันอนุมัติ" SortExpression="approve_date" UniqueName="approve_date">
                                                                        <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                    </telerik:GridBoundColumn>
                                                                    <telerik:GridTemplateColumn HeaderText="หมายเลข Invoice">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblInvoice_No" runat="server" Text='<%#GetInvoice_No(Eval("invh_run_auto")) %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                                                    </telerik:GridTemplateColumn>
                                                                    <telerik:GridBoundColumn DataField="remark_docatt" HeaderText="เหตุผลการแจ้งแก้ไขเอกสาร ฯ"
                                                                        ReadOnly="True" SortExpression="remark_docatt" UniqueName="remark_docatt">
                                                                        <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                                                    </telerik:GridBoundColumn>
                                                                    <telerik:GridBoundColumn DataField="company_name" HeaderText="บริษัท" ReadOnly="True"
                                                                        SortExpression="company_name" UniqueName="company_name">
                                                                        <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                                                    </telerik:GridBoundColumn>
                                                                    <telerik:GridBoundColumn DataField="form_name" HeaderText="ฟอร์ม" ReadOnly="True"
                                                                        SortExpression="form_name" UniqueName="form_name">
                                                                        <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                                                    </telerik:GridBoundColumn>
                                                                    <telerik:GridBoundColumn DataField="alert_user" HeaderText="ผู้แจ้งแก้ไข" ReadOnly="True"
                                                                        SortExpression="alert_user" UniqueName="alert_user">
                                                                        <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                    </telerik:GridBoundColumn>
                                                                    <%--<telerik:GridBoundColumn DataField="alert_date" DataFormatString="{0:dd/MM/yyyy}"
                                                                        HeaderText="วันที่แจ้งแก้ไข" SortExpression="alert_date" UniqueName="alert_date">
                                                                        <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                    </telerik:GridBoundColumn>
                                                                    <telerik:GridBoundColumn DataField="OverDay" HeaderText="เลยกำหนดวันส่งคืน (วัน)"
                                                                        ReadOnly="True" SortExpression="OverDay" UniqueName="OverDay">
                                                                        <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Right" />
                                                                    </telerik:GridBoundColumn>--%>
                                                                    <telerik:GridBoundColumn DataField="date_edit" DataFormatString="{0:dd/MM/yyyy}"
                                                                        HeaderText="วันที่ส่งแก้ไขเอกสาร ฯ" ReadOnly="True" SortExpression="date_edit"
                                                                        UniqueName="date_edit">
                                                                        <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                    </telerik:GridBoundColumn>
                                                                    <telerik:GridBoundColumn DataField="user_edit" HeaderText="ผู้บันทึกการส่งแก้ไข ฯ"
                                                                        ReadOnly="True" SortExpression="user_edit" UniqueName="user_edit">
                                                                        <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                    </telerik:GridBoundColumn>
                                                                    <telerik:GridBoundColumn DataField="date_finish" DataFormatString="{0:dd/MM/yyyy}"
                                                                        HeaderText="วันที่รับคืนเอกสาร ฯ" ReadOnly="True" SortExpression="date_finish"
                                                                        UniqueName="date_finish">
                                                                        <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                    </telerik:GridBoundColumn>
                                                                    <telerik:GridBoundColumn DataField="user_finish" HeaderText="ผู้บันทึกรับคืนเอกสาร ฯ"
                                                                        ReadOnly="True" SortExpression="user_finish" UniqueName="user_finish">
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
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </telerik:RadPageView>
                            <telerik:RadPageView ID="RadPageView3" runat="server" Width="100%">
                                <table border="0" cellpadding="1" cellspacing="0" class="m-frm-bdr" width="100%">
                                    <tr>
                                        <td class="FormLabel">
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
                                                                                                        <font class="groupbox">&nbsp; หนังสือรับรองที่ถูกยกเลิก &nbsp;</font></td>
                                                                                                    <telerik:RadCodeBlock ID="RadCodeBlock4" runat="server">
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
                                                                                            <table style="width: 100%">
                                                                                                <tr>
                                                                                                    <td style="width: 100%">
                                                                                                        <table style="width: 100%">
                                                                                                            <tr>
                                                                                                                <td style="width: 100%">
                                                                                                                    <font class="FormLabel">เลขที่อ้างอิง (Barcode) :</font>
                                                                                                                    <asp:TextBox ID="txtSearchCancel" runat="server" MaxLength="15" Width="200px"></asp:TextBox>
                                                                                                                    <asp:Button ID="btnSearchTab3" runat="server" Text="สืบค้น" Width="100px" /></td>
                                                                                                            </tr>
                                                                                                            <%--<tr>
                                                                                                                <td style="width: 100%">
                                                                                                                    <font class="FormLabel">วันอนุมัติตั้งแต่</font>
                                                                                                                    <telerik:RadDatePicker ID="rdpStartApproved" runat="server" Culture="English (United Kingdom)"
                                                                                                                                        Skin="Vista">
                                                                                                                        <Calendar DayNameFormat="FirstTwoLetters" ShowRowHeaders="False" Skin="Vista" UseColumnHeadersAsSelectors="False"
                                                                                                                                            UseRowHeadersAsSelectors="False" ViewSelectorText="x" FirstDayOfWeek="Sunday">
                                                                                                                        </Calendar>
                                                                                                                        <DatePopupButton HoverImageUrl="" ImageUrl="" />
                                                                                                                    </telerik:RadDatePicker>
                                                                                                                    <font class="FormLabel">&nbsp;ถึง</font>
                                                                                                                    <telerik:RadDatePicker ID="rdpEndApproved" runat="server" Culture="English (United Kingdom)"
                                                                                                                                        Skin="Vista">
                                                                                                                        <Calendar DayNameFormat="FirstTwoLetters" ShowRowHeaders="False" Skin="Vista" UseColumnHeadersAsSelectors="False"
                                                                                                                                            UseRowHeadersAsSelectors="False" ViewSelectorText="x" FirstDayOfWeek="Sunday">
                                                                                                                        </Calendar>
                                                                                                                        <DatePopupButton HoverImageUrl="" ImageUrl="" />
                                                                                                                    </telerik:RadDatePicker>
                                                                                                                    </td>
                                                                                                            </tr>--%>
                                                                                                        </table>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td style="width: 100%">
                                                                                                        <telerik:RadGrid ID="rgCancelCertificate" runat="server" AllowPaging="True" AllowSorting="True"
                                                                                                            GridLines="None" ShowStatusBar="True" Skin="Office2007" TabIndex="-1" Width="100%"
                                                                                                            ShowFooter="True">
                                                                                                            <MasterTableView AutoGenerateColumns="False" CellSpacing="-1" NoMasterRecordsText="ไม่มีรายการตามเงื่อนไขที่ทำการค้นหา"
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
                                                                                                                        HeaderText="วันอนุมัติ" SortExpression="approve_date" UniqueName="approve_date">
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
                                                                                                                    <telerik:GridBoundColumn DataField="invoice_no1" HeaderText="Invoice No.1" ReadOnly="True"
                                                                                                                        SortExpression="invoice_no1" UniqueName="invoice_no1">
                                                                                                                        <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                                                                                                    </telerik:GridBoundColumn>
                                                                                                                    <telerik:GridBoundColumn DataField="invoice_no2" HeaderText="Invoice No.2" ReadOnly="True"
                                                                                                                        SortExpression="invoice_no2" UniqueName="invoice_no2">
                                                                                                                        <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                                                                                                    </telerik:GridBoundColumn>
                                                                                                                    <telerik:GridBoundColumn DataField="invoice_no3" HeaderText="Invoice No.3" ReadOnly="True"
                                                                                                                        SortExpression="invoice_no3" UniqueName="invoice_no3">
                                                                                                                        <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                                                                                                    </telerik:GridBoundColumn>
                                                                                                                    <telerik:GridBoundColumn DataField="invoice_no4" HeaderText="Invoice No.4" ReadOnly="True"
                                                                                                                        SortExpression="invoice_no4" UniqueName="invoice_no4">
                                                                                                                        <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                                                                                                    </telerik:GridBoundColumn>
                                                                                                                    <telerik:GridBoundColumn DataField="invoice_no5" HeaderText="Invoice No.5" ReadOnly="True"
                                                                                                                        SortExpression="invoice_no5" UniqueName="invoice_no5">
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
