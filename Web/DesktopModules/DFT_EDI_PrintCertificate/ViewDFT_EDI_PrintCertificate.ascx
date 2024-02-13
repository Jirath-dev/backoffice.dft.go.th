<%@ Control Language="vb" Inherits="NTi.Modules.DFT_EDI_PrintCertificate.ViewDFT_EDI_PrintCertificate"
    AutoEventWireup="false" Explicit="True" CodeBehind="ViewDFT_EDI_PrintCertificate.ascx.vb" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<script src="/js/jquery/jquery.js"></script>
<script src="/js/jquery/jquery-ui.js"></script>
<link href="/js/jquery/jquery-ui2.css" rel="stylesheet" />
<script type="text/javascript">
    $.noConflict();
    jQuery(document).ready(function () {
        //jQuery('#div_loading').hide();

        jQuery("#div_loading").dialog({
            modal: true,
            autoOpen: false
        });

        jQuery('#<%=btnPrint.ClientID%>').click(function () {
            var items = $find("<%= rgRequestForm.MasterTableView.ClientID %>").get_selectedItems().length;
            if (items <= 0) {
                alert('กรุณาเลือกรายการหนังสือรับรองก่อนการสั่งพิมพ์');
                return false;
            } else {
                //jQuery('#div_loading').show();

                jQuery('#div_loading').dialog({
                    modal: true,
                    autoOpen: false,
                    title: "กำลังดำเนินการพิมพ์แบบคำขอ/หนังสือรับรองฯ",
                    width: 450
                });

                jQuery('#div_loading').dialog('open');

                return true;
            }

        });
    });

</script>
<style>
    .ui-dialog-titlebar-close {
        display: none;
    }
</style>
<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="RadAjaxManager1">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="rgCardList" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="rgCardList">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="rgCardList" />
            </UpdatedControls>
        </telerik:AjaxSetting>
    </AjaxSettings>
</telerik:RadAjaxManager>
<br />
<table width="100%" border="0" cellpadding="0" cellspacing="5">
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
                                                            <font class="groupbox">&nbsp; ค้นหาข้อมูล &nbsp;</font></td>
                                                        <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
                                                            <td style='background-image: url(<%= ResolveUrl("images") %>/CORNER.gif); background-repeat: no-repeat;'>&nbsp; &nbsp;&nbsp;</td>
                                                        </telerik:RadCodeBlock>
                                                    </tr>
                                                </table>
                                                <asp:Label ID="lblRoleID" runat="server" Text="lblRoleID" Visible="False"></asp:Label>&nbsp;
                                                <asp:Label ID="lblDSRoleID" runat="server" Text="lblDSRoleID" Visible="False"></asp:Label></td>
                                            <td align="right" class="m-frm-nav"></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td class="m-frm" valign="top" width="100%">
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                        <tr>
                                            <td width="15">&nbsp;</td>
                                            <td>
                                                <table border="0" cellpadding="0" cellspacing="2" width="100%">
                                                    <tr>
                                                        <td style="font-size: 12pt; color: #000000; font-family: Times New Roman">
                                                            <font class="FormLabel">เลขที่บัตรประจำตัวผู้ส่งออก :
                                                                <asp:TextBox ID="txtSearch" runat="server" MaxLength="9"></asp:TextBox>
                                                                <asp:Button ID="btnSearch" runat="server" Text="ค้นหา" Width="150px" ValidationGroup="search" />
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtSearch"
                                                                    Display="Dynamic" ErrorMessage="กรุณาป้อนหมายเลขบัตร" Font-Names="Tahoma" Font-Size="10pt"
                                                                    SetFocusOnError="True" ValidationGroup="search"></asp:RequiredFieldValidator>
                                                                <asp:Button ID="Button1" runat="server" Text="Button" Visible="False" />
                                                                <asp:TextBox ID="txtTemp_user" runat="server" Visible="False"></asp:TextBox></font></td>
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
            <table id="tblCardDetail" runat="server" border="0" cellpadding="0" cellspacing="0"
                width="100%" class="m-frm-bdr">
                <tr>
                    <td>
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr class="m-frm-hdr">
                                <td>
                                    <telerik:RadTabStrip ID="RadTabStrip1" runat="server" Skin="Office2007" Width="100%"
                                        SelectedIndex="0" MultiPageID="RadMultiPage1" Font-Names="Tahoma" Font-Size="10pt">
                                        <Tabs>
                                            <telerik:RadTab runat="server" Text="ข้อมูลผู้ส่งออก" Font-Names="Tahoma"
                                                Font-Size="10pt">
                                            </telerik:RadTab>
                                            <telerik:RadTab runat="server" Text="ข้อมูลผู้รับมอบ" Font-Names="Tahoma" Font-Size="10pt">
                                            </telerik:RadTab>
                                            <telerik:RadTab runat="server" Text="รายการยื่นเอกสารแนบ" Font-Names="Tahoma" Font-Size="10pt" Selected="True">
                                            </telerik:RadTab>
                                            <telerik:RadTab runat="server" Text="บันทึกการกระทำความผิด" Font-Names="Tahoma" Font-Size="10pt">
                                            </telerik:RadTab>
                                        </Tabs>
                                    </telerik:RadTabStrip>
                                </td>
                            </tr>
                            <tr>
                                <td style="background-color: #eeeeee; border-width: 1px; border-style: solid; border-color: #CCCCCC;">
                                    <telerik:RadMultiPage ID="RadMultiPage1" runat="server" Width="100%" SelectedIndex="0"
                                        TabIndex="-1">
                                        <telerik:RadPageView ID="RadPageView1" runat="server" Width="100%" TabIndex="-1">
                                            <table width="100%" border="0" cellspacing="2" cellpadding="0">
                                                <tr>
                                                    <td>
                                                        <table border="0" cellpadding="0" cellspacing="0" class="m-frm-bdr" width="100%">
                                                            <tr class="m-frm-hdr">
                                                                <td align="left" class="m-frm-hdr">
                                                                    <table border="0" cellpadding="0" cellspacing="0">
                                                                        <tr>
                                                                            <td class="groupboxheader" nowrap="nowrap">
                                                                                <font class="groupbox">&nbsp; ผู้ขอ &nbsp;</font></td>
                                                                            <telerik:RadCodeBlock ID="RadCodeBlock3" runat="server">
                                                                                <td style='background-image: url(<%= ResolveUrl("images") %>/CORNER.gif); background-repeat: no-repeat'>&nbsp; &nbsp;&nbsp;</td>
                                                                            </telerik:RadCodeBlock>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr class="m-frm-hdr">
                                                                <td align="left">
                                                                    <table border="0" cellpadding="0" cellspacing="2" width="100%">
                                                                        <tr>
                                                                            <td style="font-size: 12pt; color: #000000; font-family: Times New Roman">
                                                                                <font class="FormLabel">บริษัท:&nbsp;</font>
                                                                                <telerik:RadTextBox ID="txtCompanyName_Eng"
                                                                                    runat="server" CssClass="FormFld" EnableEmbeddedSkins="False" TabIndex="-1" Width="315px"
                                                                                    ReadOnly="True">
                                                                                </telerik:RadTextBox>
                                                                                <font class="FormLabel">TaxID:</font>
                                                                                <telerik:RadTextBox ID="txtCompany_Taxno" runat="server" CssClass="FormFld" EnableEmbeddedSkins="False"
                                                                                    TabIndex="-1" Width="150px" ReadOnly="True">
                                                                                </telerik:RadTextBox>
                                                                                <telerik:RadTextBox ID="txtCompany_BranchNo" runat="server" CssClass="FormFld" EnableEmbeddedSkins="False"
                                                                                    TabIndex="-1" Width="150px" ReadOnly="True" Visible="False">
                                                                                </telerik:RadTextBox></td>
                                                                        </tr>
                                                                        <tr style="font-size: 12pt; font-family: Times New Roman">
                                                                            <td style="font-size: 12pt; font-family: Times New Roman">
                                                                                <font class="FormLabel">ที่อยู่:&nbsp;</font>
                                                                                <telerik:RadTextBox ID="txtCompany_Address" runat="server" CssClass="FormFld" EnableEmbeddedSkins="False"
                                                                                    ReadOnly="True" TabIndex="-1" Width="680px">
                                                                                </telerik:RadTextBox></td>
                                                                        </tr>
                                                                        <tr style="font-size: 12pt; font-family: Times New Roman">
                                                                            <td>
                                                                                <font class="FormLabel">โทรศัพท์:</font>
                                                                                <telerik:RadTextBox ID="txtCompany_Phone" runat="server" CssClass="FormFld" EnableEmbeddedSkins="False"
                                                                                    TabIndex="-1" Width="150px" ReadOnly="True">
                                                                                </telerik:RadTextBox>
                                                                                <font class="FormLabel">&nbsp;โทรสาร:</font>
                                                                                <telerik:RadTextBox ID="txtCompany_Fax" runat="server" CssClass="FormFld" EnableEmbeddedSkins="False"
                                                                                    TabIndex="-1" Width="150px" ReadOnly="True">
                                                                                </telerik:RadTextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr style="font-size: 12pt; font-family: Times New Roman">
                                                                            <td>
                                                                                <font class="FormLabel">กรรมการผู้มีอำนาจ 1.<telerik:RadTextBox ID="txtAuthorize1"
                                                                                    runat="server" CssClass="FormFld" EnableEmbeddedSkins="False" TabIndex="-1" Width="200px"
                                                                                    ReadOnly="True">
                                                                                </telerik:RadTextBox>
                                                                                    2.<telerik:RadTextBox ID="txtAuthorize2" runat="server" CssClass="FormFld" EnableEmbeddedSkins="False"
                                                                                        TabIndex="-1" Width="200px" ReadOnly="True">
                                                                                    </telerik:RadTextBox>
                                                                                    3.<telerik:RadTextBox ID="txtAuthorize3" runat="server" CssClass="FormFld" EnableEmbeddedSkins="False"
                                                                                        TabIndex="-1" Width="200px" ReadOnly="True">
                                                                                    </telerik:RadTextBox></font></td>
                                                                        </tr>
                                                                        <tr style="font-size: 12pt; font-family: Times New Roman">
                                                                            <td>
                                                                                <font class="FormLabel">กรรมการผู้รับมอบอำนาจ 1.<telerik:RadTextBox ID="txtAuthorize4"
                                                                                    runat="server" CssClass="FormFld" EnableEmbeddedSkins="False" TabIndex="-1" Width="200px"
                                                                                    ReadOnly="True">
                                                                                </telerik:RadTextBox>
                                                                                    2.<telerik:RadTextBox ID="txtAuthorize5" runat="server" CssClass="FormFld" EnableEmbeddedSkins="False"
                                                                                        TabIndex="-1" Width="200px" ReadOnly="True">
                                                                                    </telerik:RadTextBox>
                                                                                    3.<telerik:RadTextBox ID="txtAuthorize6" runat="server" CssClass="FormFld" EnableEmbeddedSkins="False"
                                                                                        TabIndex="-1" Width="200px" ReadOnly="True">
                                                                                    </telerik:RadTextBox></font></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <font class="FormLabel">ผู้ลงนาม:
                                                                                    <telerik:RadTextBox ID="txtAuthorize7" runat="server" CssClass="FormFld" EnableEmbeddedSkins="False"
                                                                                        TabIndex="-1" Width="400px" ReadOnly="True">
                                                                                    </telerik:RadTextBox>
                                                                                    ระดับบัตร
                                                                                    <telerik:RadTextBox ID="txtCardLevel" runat="server" CssClass="FormFld" EnableEmbeddedSkins="False"
                                                                                        TabIndex="-1" Width="200px" ReadOnly="True">
                                                                                    </telerik:RadTextBox></font>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>&nbsp; &nbsp; &nbsp; 
                                                                                <asp:TextBox ID="txtReturnMsg" runat="server" BackColor="Black" ForeColor="Yellow"
                                                                                    Height="100px" TextMode="MultiLine" Width="330px" Font-Bold="True" Font-Names="Tahoma"
                                                                                    Font-Size="10pt" ReadOnly="True"></asp:TextBox>
                                                                                <asp:TextBox ID="txtReturnMsgBlack" runat="server" BackColor="Black" Font-Bold="True" Font-Names="Tahoma"
                                                                                    Font-Size="10pt" ForeColor="Yellow" Height="100px" ReadOnly="True" TextMode="MultiLine"
                                                                                    Width="330px"></asp:TextBox></td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <table id="tblRefList" runat="server" border="0" cellpadding="1" cellspacing="0"
                                                            class="m-frm-bdr" width="100%">
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
                                                                                                        <font class="groupbox">&nbsp; ข้อมูลคำขอ &nbsp;</font></td>
                                                                                                    <telerik:RadCodeBlock ID="RadCodeBlock6" runat="server">
                                                                                                        <td style='background-image: url(<%= ResolveUrl("images") %>/CORNER.gif); background-repeat: no-repeat;'>&nbsp; &nbsp;&nbsp;</td>
                                                                                                    </telerik:RadCodeBlock>
                                                                                                </tr>
                                                                                            </table>
                                                                                            <asp:TextBox ID="TextBox1" runat="server" Visible="False"></asp:TextBox>&nbsp;
                                                                                            <asp:Label ID="lblRowCountCheck" runat="server" ForeColor="Red"></asp:Label></td>
                                                                                        <td align="right" class="m-frm-nav"></td>
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
                                                                                                        <table border="0" cellpadding="0" cellspacing="2">
                                                                                                            <tr>
                                                                                                                <td colspan="4">
                                                                                                                    <asp:Panel ID="PanelSelectPrint" runat="server" BackColor="#C0C0FF" CssClass="FormLabel"
                                                                                                                        GroupingText="เลือกหน้าพิมพ์" Width="50%">
                                                                                                                        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                                                                                                                            <tr>
                                                                                                                                <td align="right" class="FormLabel" style="width: 70px">หน้าแรก :</td>
                                                                                                                                <td align="left" class="FormLabel" style="width: 7%">
                                                                                                                                    <telerik:RadNumericTextBox ID="txtFirstpage" runat="server" Culture="Thai (Thailand)"
                                                                                                                                        DataType="System.Single" EnableEmbeddedSkins="False" ForeColor="Blue" MaxValue="1000"
                                                                                                                                        MinValue="0" ShowSpinButtons="False" TabIndex="-1" Width="54px">
                                                                                                                                        <NumberFormat AllowRounding="False" DecimalDigits="0" />
                                                                                                                                    </telerik:RadNumericTextBox>
                                                                                                                                </td>
                                                                                                                                <td align="right" class="FormLabel" style="width: 70px">หน้าสุดท้าย :</td>
                                                                                                                                <td align="left" class="FormLabel" style="width: 100px">
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
                                                                                                                <td></td>
                                                                                                                <td></td>
                                                                                                                <td></td>
                                                                                                                <td></td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td>
                                                                                                                    <asp:Button ID="btnPrint" runat="server" Text="พิมพ์" Width="100px" />
                                                                                                                    <asp:Button ID="btnRefresh" runat="server" Text="Refresh" Width="100px" />
                                                                                                                    <%--<asp:Button ID="btnVerifly" runat="server" Visible="false" Text="คลิกให้ผ่านการตรวจสอบ" Width="150px" />--%></td>
                                                                                                                <td>
                                                                                                                    <asp:Panel ID="Panel1" runat="server" BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px">
                                                                                                                        <asp:RadioButtonList ID="RBListPrinter" runat="server" Font-Names="Tahoma" Font-Size="10pt"
                                                                                                                            RepeatDirection="Horizontal" Width="200px">
                                                                                                                            <asp:ListItem Value="0">จอภาพ</asp:ListItem>
                                                                                                                            <asp:ListItem Value="1" Selected="True">เครื่องพิมพ์</asp:ListItem>
                                                                                                                        </asp:RadioButtonList>
                                                                                                                    </asp:Panel>
                                                                                                                </td>
                                                                                                                <td>
                                                                                                                    <asp:Panel ID="Panel2" runat="server" BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px">
                                                                                                                        <asp:RadioButtonList ID="RBListFormRequest" runat="server" Font-Names="Tahoma" Font-Size="10pt"
                                                                                                                            RepeatDirection="Horizontal" Width="200px">
                                                                                                                            <asp:ListItem Value="0">แบบคำขอ</asp:ListItem>
                                                                                                                            <asp:ListItem Selected="True" Value="1">หนังสือรับรอง</asp:ListItem>
                                                                                                                        </asp:RadioButtonList>
                                                                                                                    </asp:Panel>
                                                                                                                </td>
                                                                                                                <td>
                                                                                                                    <asp:CheckBox ID="chkRePrint" runat="server" Font-Names="Tahoma" Font-Size="10pt"
                                                                                                                        Text="พิมพ์ซ้ำ" /></td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td align="right" class="FormLabel">เครื่องพิมพ์คำขอ&nbsp; :</td>
                                                                                                                <td class="FormLabel" colspan="3">
                                                                                                                    <asp:RadioButtonList ID="rdblReceiptPrinter" runat="server" RepeatDirection="Horizontal" RepeatColumns="3">
                                                                                                                    </asp:RadioButtonList>
                                                                                                                    <asp:Label ID="lbl_ErrMSG" runat="server" Font-Names="Tahoma" Font-Size="10pt" ForeColor="Red"></asp:Label></td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td align="left" class="FormLabel">
                                                                                                                    <asp:Panel ID="Panel_A" runat="server" BackColor="#C0C0FF" CssClass="FormLabel" GroupingText="เครื่องฟอร์ม A"
                                                                                                                        Visible="False" Width="100%">
                                                                                                                        <asp:RadioButtonList ID="rdoA_ALL" runat="server" RepeatDirection="Horizontal" RepeatColumns="3" CssClass="FormLabel">
                                                                                                                            <asp:ListItem Selected="True" Value="A1">เครื่องที่ 1</asp:ListItem>
                                                                                                                            <asp:ListItem Value="A2">เครื่องที่ 2</asp:ListItem>
                                                                                                                        </asp:RadioButtonList>
                                                                                                                    </asp:Panel>
                                                                                                                </td>
                                                                                                                <td class="FormLabel" colspan="3">
                                                                                                                    <asp:Panel ID="Panel_D" runat="server" BackColor="#C0C0FF" CssClass="FormLabel" GroupingText="เครื่อง ฟอร์ม D"
                                                                                                                        Visible="False" Width="100%">
                                                                                                                        <asp:RadioButtonList ID="rdoD_ALL" runat="server" RepeatDirection="Horizontal" RepeatColumns="3" CssClass="FormLabel">
                                                                                                                            <asp:ListItem Selected="True" Value="D1">เครื่องที่ 1</asp:ListItem>
                                                                                                                            <asp:ListItem Value="D2">เครื่องที่ 2</asp:ListItem>
                                                                                                                        </asp:RadioButtonList>
                                                                                                                    </asp:Panel>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                        </table>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        <%--<div style="overflow:hidden; width:765px">--%>
                                                                                                        <div style="overflow: hidden; width: 100%">
                                                                                                            <telerik:RadGrid ID="rgRequestForm" runat="server" Width="100%" AllowSorting="True"
                                                                                                                GridLines="None" Skin="Office2007" TabIndex="-1" ShowStatusBar="True" AllowMultiRowSelection="True" ShowFooter="True">
                                                                                                                <MasterTableView AutoGenerateColumns="False" ClientDataKeyNames="invh_run_auto"
                                                                                                                    DataKeyNames="invh_run_auto" NoMasterRecordsText="ไม่มีรายการคำร้อง">
                                                                                                                    <Columns>
                                                                                                                        <telerik:GridTemplateColumn UniqueName="PrintTemplateColumn" Visible="False">
                                                                                                                            <HeaderStyle Width="15px" HorizontalAlign="Center" />
                                                                                                                            <ItemStyle Width="15px" HorizontalAlign="Center" />
                                                                                                                            <ItemTemplate>
                                                                                                                                <asp:ImageButton ID="imbPrint" ImageUrl="~/images/Printer.png" runat="server" AlternateText='<%#Eval("invh_run_auto")%>' ToolTip='<%#Eval("form_type")%>' OnClick="imbPrint_Click" />
                                                                                                                            </ItemTemplate>
                                                                                                                        </telerik:GridTemplateColumn>
                                                                                                                        <telerik:GridTemplateColumn HeaderText="ฟอร์ม" UniqueName="TemplateColumn">
                                                                                                                            <ItemTemplate>
                                                                                                                                <asp:Label ID="lblSent_FormName" runat="server" Text='<%#GetFormName_(Eval("form_type")) %>'></asp:Label>
                                                                                                                            </ItemTemplate>
                                                                                                                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Left" />
                                                                                                                        </telerik:GridTemplateColumn>
                                                                                                                        <telerik:GridBoundColumn DataField="CaseSign" HeaderText="CaseSign"
                                                                                                                            ReadOnly="True" SortExpression="CaseSign" UniqueName="CaseSign" Visible="false">
                                                                                                                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                                                                        </telerik:GridBoundColumn>
                                                                                                                        <telerik:GridTemplateColumn HeaderText="สถานะ" UniqueName="CaseSign1">
                                                                                                                            <ItemTemplate>
                                                                                                                                <asp:Label ID="lblCaseSign" runat="server" Text=""></asp:Label>
                                                                                                                            </ItemTemplate>
                                                                                                                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                                                                        </telerik:GridTemplateColumn>
                                                                                                                        <telerik:GridNumericColumn DataField="invh_run_auto" ReadOnly="True" HeaderText="เลขที่อ้างอิง"
                                                                                                                            SortExpression="invh_run_auto" UniqueName="invh_run_auto" Aggregate="Count" FooterText="รวม : ">
                                                                                                                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                                                                            <FooterStyle Font-Names="Tahoma" Font-Size="10pt" />
                                                                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                                                                        </telerik:GridNumericColumn>
                                                                                                                        <telerik:GridBoundColumn DataField="reference_code2" HeaderText="เลขที่หนังสือรับรอง"
                                                                                                                            ReadOnly="True" SortExpression="reference_code2" UniqueName="reference_code2">
                                                                                                                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                                                                        </telerik:GridBoundColumn>
                                                                                                                        <telerik:GridBoundColumn DataField="company_name" HeaderText="ผู้ขอ" SortExpression="company_name"
                                                                                                                            UniqueName="company_name" Visible="False">
                                                                                                                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
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
                                                                                                                        <telerik:GridBoundColumn DataField="print_no" HeaderText="ครั้งที่พิมพ์" ReadOnly="True"
                                                                                                                            SortExpression="print_no" UniqueName="print_no" Visible="False">
                                                                                                                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                                                                        </telerik:GridBoundColumn>
                                                                                                                        <telerik:GridBoundColumn DataField="print_flag" HeaderText="พิมพ์" ReadOnly="True"
                                                                                                                            SortExpression="print_flag" UniqueName="print_flag" Visible="False">
                                                                                                                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                                                                        </telerik:GridBoundColumn>
                                                                                                                        <telerik:GridBoundColumn DataField="request_person" HeaderText="ผู้รับมอบ"
                                                                                                                            ReadOnly="True" SortExpression="request_person" UniqueName="request_person">
                                                                                                                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                                                                        </telerik:GridBoundColumn>
                                                                                                                        <telerik:GridBoundColumn DataField="form_type" HeaderText="form_type" ReadOnly="True"
                                                                                                                            SortExpression="form_type" UniqueName="form_type" Visible="False">
                                                                                                                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                                                                        </telerik:GridBoundColumn>
                                                                                                                        <telerik:GridBoundColumn DataField="site_id" HeaderText="site_id" ReadOnly="True"
                                                                                                                            SortExpression="site_id" UniqueName="site_id" Visible="False">
                                                                                                                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                                                                        </telerik:GridBoundColumn>
                                                                                                                        <telerik:GridBoundColumn DataField="COMPANY_TAXNO" HeaderText="COMPANY_TAXNO" ReadOnly="True"
                                                                                                                            SortExpression="COMPANY_TAXNO" UniqueName="COMPANY_TAXNO" Visible="False">
                                                                                                                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                                                                        </telerik:GridBoundColumn>
                                                                                                                        <telerik:GridBoundColumn DataField="FROM_DATE" HeaderText="FROM_DATE" ReadOnly="True"
                                                                                                                            SortExpression="FROM_DATE" UniqueName="FROM_DATE" Visible="False">
                                                                                                                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                                                                        </telerik:GridBoundColumn>
                                                                                                                        <telerik:GridBoundColumn DataField="TO_DATE" HeaderText="TO_DATE" ReadOnly="True"
                                                                                                                            SortExpression="TO_DATE" UniqueName="TO_DATE" Visible="False">
                                                                                                                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                                                                        </telerik:GridBoundColumn>
                                                                                                                        <telerik:GridBoundColumn DataField="INVOICE_NO1" HeaderText="NO.INVOICE" ReadOnly="True"
                                                                                                                            SortExpression="INVOICE_NO1" UniqueName="INVOICE_NO1">
                                                                                                                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                                                                                                        </telerik:GridBoundColumn>
                                                                                                                        <telerik:GridBoundColumn DataField="reference_code1" HeaderText="เลขที่รับคำร้อง"
                                                                                                                            ReadOnly="True" SortExpression="reference_code1" UniqueName="reference_code1">
                                                                                                                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                                                                        </telerik:GridBoundColumn>
                                                                                                                        <telerik:GridBoundColumn DataField="sent_date" HeaderText="วันที่ส่ง"
                                                                                                                            ReadOnly="True" SortExpression="sent_date" UniqueName="sent_date">
                                                                                                                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                                                                        </telerik:GridBoundColumn>
                                                                                                                        <telerik:GridBoundColumn DataField="Check_StatusWeb" HeaderText="สถานะระบบใหม่-เก่า" ReadOnly="True"
                                                                                                                            SortExpression="Check_StatusWeb" UniqueName="Check_StatusWeb">
                                                                                                                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt" ForeColor="Red" />
                                                                                                                        </telerik:GridBoundColumn>
                                                                                                                        <telerik:GridBoundColumn DataField="DISPLAY_FLAG" HeaderText="DISPLAY_FLAG" ReadOnly="True"
                                                                                                                            SortExpression="DISPLAY_FLAG" UniqueName="DISPLAY_FLAG" Visible="False">
                                                                                                                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                                                                        </telerik:GridBoundColumn>
                                                                                                                    </Columns>
                                                                                                                </MasterTableView>
                                                                                                                <HeaderStyle HorizontalAlign="Center" Font-Names="Tahoma" Font-Size="10pt" />
                                                                                                                <ClientSettings AllowKeyboardNavigation="True">
                                                                                                                    <Selecting AllowRowSelect="True" />
                                                                                                                    <KeyboardNavigationSettings AllowActiveRowCycle="True" />
                                                                                                                    <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                                                                                                                </ClientSettings>
                                                                                                                <ItemStyle Wrap="False" />
                                                                                                                <SelectedItemStyle Wrap="True" BackColor="DarkOrange" />
                                                                                                            </telerik:RadGrid>
                                                                                                        </div>
                                                                                                        <div style="padding: 5px; display: none;">
                                                                                                            <asp:Button ID="btnVerifly" runat="server" Visible="True" Text="คลิกให้ผ่านการตรวจสอบ"
                                                                                                                Width="150px" />
                                                                                                            <div style="color: Red;">ปุ่มนี้ เปิดให้ใช้งานได้ ตั้งแต่เวลา 15.00 - 18.00 เท่านั้น</div>
                                                                                                        </div>

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
                                        <telerik:RadPageView ID="RadPageView2" runat="server" Width="100%" TabIndex="-1">
                                            <table width="100%" border="0" cellspacing="2" cellpadding="0">
                                                <tr>
                                                    <td>
                                                        <table border="0" cellpadding="0" cellspacing="0" class="m-frm-bdr" width="100%">
                                                            <tr class="m-frm-hdr">
                                                                <td align="left" class="m-frm-hdr">
                                                                    <table border="0" cellpadding="0" cellspacing="0">
                                                                        <tr>
                                                                            <td class="groupboxheader" nowrap="nowrap">
                                                                                <font class="groupbox">&nbsp; รายการบัตรผู้รับมอบ &nbsp;</font></td>
                                                                            <telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">
                                                                                <td style='background-image: url(<%= ResolveUrl("images") %>/CORNER.gif); background-repeat: no-repeat'>&nbsp; &nbsp;&nbsp;</td>
                                                                            </telerik:RadCodeBlock>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr class="m-frm-hdr">
                                                                <td align="left">
                                                                    <table border="0" cellpadding="0" cellspacing="2" width="100%">
                                                                        <tr>
                                                                            <td>

                                                                                <telerik:RadGrid ID="rgCardList" runat="server" AllowPaging="True" AllowSorting="True"
                                                                                    Font-Names="Tahoma" Font-Size="X-Small" GridLines="None" Skin="Office2007" TabIndex="-1"
                                                                                    Width="100%" ShowStatusBar="True" ShowFooter="True" PageSize="50">
                                                                                    <MasterTableView AutoGenerateColumns="False" ClientDataKeyNames="card_id"
                                                                                        DataKeyNames="card_id" NoMasterRecordsText="ไม่มีรายการบัตร" Width="100%">
                                                                                        <Columns>
                                                                                            <telerik:GridBoundColumn DataField="card_id" HeaderText="รหัสบัตร" ReadOnly="True"
                                                                                                SortExpression="card_id" UniqueName="card_id">
                                                                                                <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                                            </telerik:GridBoundColumn>
                                                                                            <telerik:GridBoundColumn DataField="active_flag" HeaderText="สถานะ" ReadOnly="True"
                                                                                                SortExpression="active_flag" UniqueName="active_flag">
                                                                                                <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                                            </telerik:GridBoundColumn>
                                                                                            <telerik:GridBoundColumn DataField="expire_date" HeaderText="วันหมดอายุ" ReadOnly="True"
                                                                                                SortExpression="expire_date" UniqueName="expire_date" DataFormatString="{0:dd/MM/yyyy}">
                                                                                                <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                                            </telerik:GridBoundColumn>
                                                                                            <telerik:GridBoundColumn DataField="card_level" HeaderText="ระดับ" SortExpression="card_level"
                                                                                                UniqueName="card_level">
                                                                                                <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                                                                            </telerik:GridBoundColumn>
                                                                                            <telerik:GridBoundColumn DataField="card_type" HeaderText="ประเภท" SortExpression="card_type"
                                                                                                UniqueName="card_type">
                                                                                                <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                                            </telerik:GridBoundColumn>
                                                                                            <telerik:GridBoundColumn DataField="AuthName" HeaderText="ชื่อ (อังกฤษ)" ReadOnly="True"
                                                                                                SortExpression="AuthName" UniqueName="AuthName">
                                                                                                <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                                                                            </telerik:GridBoundColumn>
                                                                                            <telerik:GridBoundColumn DataField="AuthName_Thai" HeaderText="ชื่อ (ไทย)" ReadOnly="True"
                                                                                                SortExpression="AuthName_Thai" UniqueName="AuthName_Thai">
                                                                                                <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                                                                            </telerik:GridBoundColumn>
                                                                                            <telerik:GridBoundColumn DataField="AuthAddress" HeaderText="ที่อยู่" ReadOnly="True"
                                                                                                SortExpression="AuthAddress" UniqueName="AuthAddress">
                                                                                                <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                                                                            </telerik:GridBoundColumn>
                                                                                        </Columns>
                                                                                    </MasterTableView>
                                                                                    <HeaderStyle HorizontalAlign="Center" Font-Names="Tahoma" Font-Size="10pt" />
                                                                                    <ClientSettings EnableRowHoverStyle="True">
                                                                                        <Selecting AllowRowSelect="True" />
                                                                                    </ClientSettings>
                                                                                    <PagerStyle Mode="NumericPages" />
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
                                        </telerik:RadPageView>
                                        <telerik:RadPageView ID="RadPageView3" runat="server" Width="100%" TabIndex="-1">
                                            <table width="100%" border="0" cellspacing="2" cellpadding="0">
                                                <tr>
                                                    <td>
                                                        <table border="0" cellpadding="0" cellspacing="0" class="m-frm-bdr" width="100%">
                                                            <tr class="m-frm-hdr">
                                                                <td align="left" class="m-frm-hdr">
                                                                    <table border="0" cellpadding="0" cellspacing="0">
                                                                        <tr>
                                                                            <td class="groupboxheader" nowrap="nowrap">
                                                                                <font class="groupbox">&nbsp; รายการยื่นเอกสารแนบ &nbsp;</font></td>
                                                                            <telerik:RadCodeBlock ID="RadCodeBlock4" runat="server">
                                                                                <td style='background-image: url(<%= ResolveUrl("images") %>/CORNER.gif); background-repeat: no-repeat'>&nbsp; &nbsp;&nbsp;</td>
                                                                            </telerik:RadCodeBlock>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr class="m-frm-hdr">
                                                                <td align="left">
                                                                    <table border="0" cellpadding="0" cellspacing="2" width="100%">
                                                                        <tr>
                                                                            <td>
                                                                                <div style="overflow: hidden; width: 765px">
                                                                                    <telerik:RadGrid ID="rgDocAttach" runat="server" AllowPaging="True" AllowSorting="True"
                                                                                        Font-Names="Tahoma" Font-Size="X-Small" GridLines="None" Skin="Office2007" TabIndex="-1"
                                                                                        Width="100%" ShowStatusBar="True">
                                                                                        <MasterTableView AutoGenerateColumns="False" ClientDataKeyNames="invh_run_auto"
                                                                                            DataKeyNames="invh_run_auto" NoMasterRecordsText="ไม่มีรายการเอกสารแนบ" Width="100%">
                                                                                            <Columns>
                                                                                                <telerik:GridBoundColumn DataField="invh_run_auto" HeaderText="เลขที่อ้างอิง" ReadOnly="True"
                                                                                                    SortExpression="invh_run_auto" UniqueName="invh_run_auto">
                                                                                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                                                </telerik:GridBoundColumn>
                                                                                                <telerik:GridBoundColumn DataField="reference_code2" HeaderText="เลขที่หนังสือ" ReadOnly="True"
                                                                                                    SortExpression="reference_code2" UniqueName="reference_code2">
                                                                                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                                                </telerik:GridBoundColumn>
                                                                                                <telerik:GridBoundColumn DataField="company_name" HeaderText="บริษัท" ReadOnly="True"
                                                                                                    SortExpression="company_name" UniqueName="company_name">
                                                                                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                                                                                </telerik:GridBoundColumn>
                                                                                                <telerik:GridBoundColumn DataField="form_name" HeaderText="ฟอร์ม" SortExpression="form_name"
                                                                                                    UniqueName="form_name">
                                                                                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                                                </telerik:GridBoundColumn>
                                                                                                <telerik:GridBoundColumn DataField="edi_status" HeaderText="การแนบ" SortExpression="edi_status"
                                                                                                    UniqueName="edi_status">
                                                                                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                                                                                </telerik:GridBoundColumn>
                                                                                                <telerik:GridBoundColumn DataField="OverDay" HeaderText="เกินกำหนด (วัน)" ReadOnly="True"
                                                                                                    SortExpression="OverDay" UniqueName="OverDay">
                                                                                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                                                </telerik:GridBoundColumn>
                                                                                                <telerik:GridBoundColumn DataField="rep_doc_date" HeaderText="วันที่บันทึกการแนบ"
                                                                                                    ReadOnly="True" SortExpression="rep_doc_date" UniqueName="rep_doc_date" DataFormatString="{0:dd/MM/yyyy}">
                                                                                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                                                </telerik:GridBoundColumn>
                                                                                                <telerik:GridBoundColumn DataField="rep_by" HeaderText="ผู้บันทึกการแนบ" ReadOnly="True"
                                                                                                    SortExpression="rep_by" UniqueName="rep_by">
                                                                                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                                                </telerik:GridBoundColumn>
                                                                                                <telerik:GridBoundColumn DataField="approve_date" HeaderText="วันอนุมัติ" ReadOnly="True"
                                                                                                    SortExpression="approve_date" UniqueName="approve_date" DataFormatString="{0:dd/MM/yyyy}">
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
                                                                                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                                                </telerik:GridBoundColumn>
                                                                                            </Columns>
                                                                                        </MasterTableView>
                                                                                        <HeaderStyle HorizontalAlign="Center" Font-Names="Tahoma" Font-Size="10pt" />
                                                                                        <ClientSettings AllowKeyboardNavigation="True">
                                                                                            <Selecting AllowRowSelect="True" />
                                                                                            <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                                                                                        </ClientSettings>
                                                                                    </telerik:RadGrid>
                                                                                </div>
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
                                        <telerik:RadPageView ID="RadPageView4" runat="server" Width="100%" TabIndex="-1">
                                            <table width="100%" border="0" cellspacing="2" cellpadding="0">
                                                <tr>
                                                    <td>
                                                        <table border="0" cellpadding="0" cellspacing="0" class="m-frm-bdr" width="100%">
                                                            <tr class="m-frm-hdr">
                                                                <td align="left" class="m-frm-hdr">
                                                                    <table border="0" cellpadding="0" cellspacing="0">
                                                                        <tr>
                                                                            <td class="groupboxheader" nowrap="nowrap">
                                                                                <font class="groupbox">&nbsp; รายการบันทึกการกระทำความผิด &nbsp;</font></td>
                                                                            <telerik:RadCodeBlock ID="RadCodeBlock5" runat="server">
                                                                                <td style='background-image: url(<%= ResolveUrl("images") %>/CORNER.gif); background-repeat: no-repeat'>&nbsp; &nbsp;&nbsp;</td>
                                                                            </telerik:RadCodeBlock>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr class="m-frm-hdr">
                                                                <td align="left">
                                                                    <table border="0" cellpadding="0" cellspacing="2" width="100%">
                                                                        <tr>
                                                                            <td>
                                                                                <%--<telerik:RadGrid ID="rgBackList" runat="server" AllowPaging="True" AllowSorting="True"
                                                                                    Font-Names="Tahoma" Font-Size="X-Small" GridLines="None" Skin="Office2007" TabIndex="-1"
                                                                                    Width="100%" ShowStatusBar="True">
                                                                                    <MasterTableView AutoGenerateColumns="False" NoMasterRecordsText="ไม่มีรายการบันทึกการกระทำความผิด" Width="100%">
                                                                                        <Columns>
                                                                                            <telerik:GridBoundColumn DataField="type_mistake" HeaderText="ประเภท" ReadOnly="True"
                                                                                                SortExpression="type_mistake" UniqueName="type_mistake">
                                                                                                <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                                            </telerik:GridBoundColumn>
                                                                                            <telerik:GridBoundColumn DataField="the_mistake" HeaderText="ความผิด" ReadOnly="True"
                                                                                                SortExpression="the_mistake" UniqueName="the_mistake">
                                                                                                <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                                            </telerik:GridBoundColumn>
                                                                                            <telerik:GridBoundColumn DataField="status_mistake" HeaderText="สถานะ" ReadOnly="True"
                                                                                                SortExpression="status_mistake" UniqueName="status_mistake">
                                                                                                <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                                            </telerik:GridBoundColumn>
                                                                                            <telerik:GridBoundColumn DataField="date_rec_mistake" HeaderText="วันบันทึกการกระทำความผิด" ReadOnly="True"
                                                                                                SortExpression="date_rec_mistake" UniqueName="date_rec_mistake" DataFormatString="{0:dd/MM/yyyy}">
                                                                                                <ItemStyle Font-Names="Tahoma" Font-Size="10pt"/>
                                                                                            </telerik:GridBoundColumn>
                                                                                            <telerik:GridBoundColumn DataField="Product_Mistake" HeaderText="คำอธิบาย" ReadOnly="True"
                                                                                                SortExpression="Product_Mistake" UniqueName="Product_Mistake" >
                                                                                                <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                                            </telerik:GridBoundColumn>
                                                                                            <telerik:GridBoundColumn DataField="site_rec_mistake" HeaderText="สถานที่กระทำความผิด" ReadOnly="True"
                                                                                                SortExpression="site_rec_mistake" UniqueName="site_rec_mistake">
                                                                                                <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                                            </telerik:GridBoundColumn>
                                                                                            <telerik:GridBoundColumn DataField="Sys_Date_Mistake" HeaderText="วันที่ผ่อนผัน" SortExpression="Sys_Date_Mistake"
                                                                                                UniqueName="Sys_Date_Mistake" DataFormatString="{0:dd/MM/yyyy}">
                                                                                                <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                                                                            </telerik:GridBoundColumn>
                                                                                            <telerik:GridBoundColumn DataField="desc_mistake" HeaderText="เหตุผลการผ่อนผัน" ReadOnly="True"
                                                                                                SortExpression="desc_mistake2" UniqueName="desc_mistake2">
                                                                                                <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                                                                            </telerik:GridBoundColumn>
                                                                                            <telerik:GridBoundColumn DataField="site_rec_mistake2" HeaderText="สถานที่ทำการผ่อนผัน" SortExpression="site_rec_mistake2"
                                                                                                UniqueName="site_rec_mistake2">
                                                                                                <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                                                                            </telerik:GridBoundColumn>
                                                                                        </Columns>
                                                                                    </MasterTableView>
                                                                                    <HeaderStyle HorizontalAlign="Center" Font-Names="Tahoma" Font-Size="10pt" />
                                                                                    <ClientSettings EnableRowHoverStyle="True">
                                                                                        <Selecting AllowRowSelect="True" />
                                                                                    </ClientSettings>
                                                                                </telerik:RadGrid>--%>
                                                                                <div style="display:none">
                                                                                <telerik:RadGrid ID="rgBackList" runat="server" AllowPaging="True" AllowSorting="True"
                                                                                    Font-Names="Tahoma" Font-Size="X-Small" GridLines="None" Skin="Office2007" TabIndex="-1"
                                                                                    Width="100%" ShowStatusBar="True">
                                                                                    <MasterTableView AutoGenerateColumns="False" NoMasterRecordsText="ไม่มีรายการบันทึกการกระทำความผิด" Width="100%">
                                                                                        <Columns>
                                                                                            <telerik:GridBoundColumn DataField="type_mistake" HeaderText="ประเภท" ReadOnly="True"
                                                                                                SortExpression="type_mistake" UniqueName="type_mistake">
                                                                                                <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                                            </telerik:GridBoundColumn>
                                                                                            <telerik:GridBoundColumn DataField="the_mistake" HeaderText="ความผิด" ReadOnly="True"
                                                                                                SortExpression="the_mistake" UniqueName="the_mistake">
                                                                                                <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                                            </telerik:GridBoundColumn>
                                                                                            <telerik:GridBoundColumn DataField="status_mistake" HeaderText="สถานะ" ReadOnly="True"
                                                                                                SortExpression="status_mistake" UniqueName="status_mistake">
                                                                                                <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                                            </telerik:GridBoundColumn>
                                                                                            <telerik:GridBoundColumn DataField="date_rec_mistake1" HeaderText="วันบันทึกการกระทำความผิด" ReadOnly="True"
                                                                                                SortExpression="date_rec_mistake1" UniqueName="date_rec_mistake1" DataFormatString="{0:dd/MM/yyyy}">
                                                                                                <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                                                                            </telerik:GridBoundColumn>
                                                                                            <telerik:GridBoundColumn DataField="desc_mistake1" HeaderText="คำอธิบาย" ReadOnly="True"
                                                                                                SortExpression="desc_mistake1" UniqueName="desc_mistake1">
                                                                                                <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                                            </telerik:GridBoundColumn>
                                                                                            <telerik:GridBoundColumn DataField="site_rec_mistake1" HeaderText="สถานที่กระทำความผิด" ReadOnly="True"
                                                                                                SortExpression="site_rec_mistake1" UniqueName="site_rec_mistake1">
                                                                                                <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                                            </telerik:GridBoundColumn>
                                                                                            <telerik:GridBoundColumn DataField="date_rec_mistake2" HeaderText="วันที่ผ่อนผัน" SortExpression="date_rec_mistake2"
                                                                                                UniqueName="date_rec_mistake2" DataFormatString="{0:dd/MM/yyyy}">
                                                                                                <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                                                                            </telerik:GridBoundColumn>
                                                                                            <telerik:GridBoundColumn DataField="desc_mistake2" HeaderText="เหตุผลการผ่อนผัน" ReadOnly="True"
                                                                                                SortExpression="desc_mistake2" UniqueName="desc_mistake2">
                                                                                                <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                                                                            </telerik:GridBoundColumn>
                                                                                            <telerik:GridBoundColumn DataField="site_rec_mistake2" HeaderText="สถานที่ทำการผ่อนผัน" SortExpression="site_rec_mistake2"
                                                                                                UniqueName="site_rec_mistake2">
                                                                                                <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                                                                            </telerik:GridBoundColumn>
                                                                                        </Columns>
                                                                                    </MasterTableView>
                                                                                    <HeaderStyle HorizontalAlign="Center" Font-Names="Tahoma" Font-Size="10pt" />
                                                                                    <ClientSettings EnableRowHoverStyle="True">
                                                                                        <Selecting AllowRowSelect="True" />
                                                                                    </ClientSettings>
                                                                                </telerik:RadGrid>
                                                                                </div>
                                                                                <div style="margin-top: 3px;">
                                                                                    <table>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <div style="padding: 10px;">หมายเหตุ : ข้อมูลจากระบบ origin-alert.dft.go.th </div>
                                                                                                <telerik:RadGrid ID="grdBlackListV2" runat="server" Font-Names="Tahoma" Font-Size="X-Small"
                                                                                                    GridLines="None" Skin="Office2007" TabIndex="-1">
                                                                                                    <MasterTableView AutoGenerateColumns="False" CellSpacing="-1"
                                                                                                        NoMasterRecordsText="ไม่มีข้อมูล BlackList/WatchList">
                                                                                                        <Columns>
                                                                                                            <telerik:GridBoundColumn DataField="CompanyNameThai" ReadOnly="True" SortExpression="CompanyName_Th"
                                                                                                                UniqueName="CompanyName_Th" HeaderText="ชื่อนิติบุคคล">
                                                                                                                <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                                                                                            </telerik:GridBoundColumn>
                                                                                                            <telerik:GridBoundColumn DataField="BlacklistType" ReadOnly="True" SortExpression="Type_Mistake"
                                                                                                                UniqueName="Type_Mistake" HeaderText="ประเภท">
                                                                                                                <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                                                                                            </telerik:GridBoundColumn>
                                                                                                            <telerik:GridBoundColumn DataField="Country" ReadOnly="True" SortExpression="CountryName"
                                                                                                                UniqueName="CountryName" HeaderText="ประเทศ">
                                                                                                                <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                                                                                            </telerik:GridBoundColumn>
                                                                                                            <telerik:GridBoundColumn DataField="Formtype" ReadOnly="True" SortExpression="form_name"
                                                                                                                UniqueName="form_name" HeaderText="ประเภทฟอร์ม">
                                                                                                                <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                                                                                            </telerik:GridBoundColumn>
                                                                                                            <telerik:GridBoundColumn DataField="Goods" ReadOnly="True" SortExpression="Product_Mistake"
                                                                                                                UniqueName="Product_Mistake" HeaderText="สินค้า">
                                                                                                                <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                                                                                            </telerik:GridBoundColumn>
                                                                                                            <telerik:GridBoundColumn DataField="Cause" ReadOnly="True" SortExpression="BlackList_Desc"
                                                                                                                UniqueName="BlackList_Desc" HeaderText="สาเหตุ">
                                                                                                                <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                                                                                            </telerik:GridBoundColumn>
                                                                                                            <telerik:GridBoundColumn DataField="Status" ReadOnly="True" SortExpression="Status"
                                                                                                                UniqueName="BStatus" HeaderText="สถานะ">
                                                                                                                <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                                                                                            </telerik:GridBoundColumn>
                                                                                                        </Columns>
                                                                                                    </MasterTableView>
                                                                                                    <HeaderStyle HorizontalAlign="Center" Font-Names="Tahoma" Font-Size="10pt" />
                                                                                                </telerik:RadGrid>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </div>
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
        </td>
    </tr>
</table>
<div id="div_loading" style="text-align: center; display: none;">
    <div>
        <img id="img_indicator" src="/images/indicator.gif" style='width: 350px;' />
        <div>
            กรุณารอสักครู่
        </div>
    </div>
</div>
