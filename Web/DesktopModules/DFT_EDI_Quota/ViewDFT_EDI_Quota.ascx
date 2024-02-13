<%@ Control Language="vb" Inherits="NTi.Modules.DFT_EDI_Quota.ViewDFT_EDI_Quota"
    AutoEventWireup="false" Explicit="True" Codebehind="ViewDFT_EDI_Quota.ascx.vb" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="RadAjaxManager1">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="lblTotalQuota" />
                <telerik:AjaxUpdatedControl ControlID="repTranStock" />
            </UpdatedControls>
        </telerik:AjaxSetting>
    </AjaxSettings>
</telerik:RadAjaxManager>
<telerik:RadCodeBlock ID="RadCodeBlockFirst" runat="server">

    <script type="text/javascript">
       function ShowQuotaDialog(TSK_ID) {
            window.radopen("/DesktopModules/DFT_EDI_Quota/frmManageQuota.aspx?action=edit&TSK_ID=" + TSK_ID, "QuotaDialog")
            return false;
       }
       
       function InsertQuotaDialog() {
            window.radopen("/DesktopModules/DFT_EDI_Quota/frmManageQuota.aspx?action=new", "QuotaDialog")
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
        <telerik:RadWindow ID="QuotaDialog" runat="server" ReloadOnShow="True" ShowContentDuringLoad="False"
            Modal="True" VisibleStatusbar="False" Width="700px" Height="200px" />
    </Windows>
</telerik:RadWindowManager>
<table cellspacing="5" width="100%">
    <tr>
        <td>
            <table cellspacing="0" border="0" class="m-frm-bdr" cellpadding="1" width="100%">
                <tr>
                    <td>
                        <table cellspacing="0" cellpadding="0" border="0" width="100%">
                            <tr class="m-frm-hdr">
                                <td>
                                    <table cellspacing="0" cellpadding="0" border="0" width="100%">
                                        <tr class="m-frm-hdr">
                                            <td align="left" class="m-frm-hdr">
                                                <table cols="2" border="0" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td nowrap class="groupboxheader">
                                                            <font class="groupbox">&#160;&#160;บริหารจัดการโควต้าสินค้าแป้งมันสำปะหลัง&#160;&#160;</font>
                                                        </td>
                                                        <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
                                                            <td style="background-image: url(<%= ResolveUrl("images") %>/CORNER.gif); background-repeat: no-repeat;">
                                                                &#160;&#160;&#160;&#160;</td>
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
                                    <table cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:Panel ID="Panel1" runat="server" BackColor="Transparent" Font-Names="Tahoma"
                                                                Font-Size="10pt" GroupingText="สืบค้น">
                                                                <table>
                                                                    <tr>
                                                                        <td>
                                                                            <table cellpadding="0" cellspacing="0" dwcopytype="CopyTableColumn">
                                                                                <tr>
                                                                                    <td>
                                                                                        <font class="FormLabel">&nbsp;ตั้งแต่:&nbsp;</font></td>
                                                                                    <td>
                                                                                        <telerik:RadDatePicker ID="rdpFromDate" runat="server" Skin="Office2007" Culture="English (United Kingdom)"
                                                                                            Width="170px" Font-Names="Tahoma" Font-Size="10pt">
                                                                                            <Calendar Skin="Office2007" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                                                                ViewSelectorText="x" DayNameFormat="FirstTwoLetters" FirstDayOfWeek="Sunday"
                                                                                                ShowRowHeaders="False">
                                                                                            </Calendar>
                                                                                            <DatePopupButton HoverImageUrl="" ImageUrl="" />
                                                                                        </telerik:RadDatePicker>
                                                                                    </td>
                                                                                    <td>
                                                                                        <font class="FormLabel">&nbsp;ถึงวัน:&nbsp;</font>
                                                                                    </td>
                                                                                    <td>
                                                                                        <telerik:RadDatePicker ID="rdpToDate" runat="server" Skin="Office2007" Culture="English (United Kingdom)"
                                                                                            Width="170px" Font-Names="Tahoma" Font-Size="10pt">
                                                                                            <Calendar Skin="Office2007" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                                                                ViewSelectorText="x" DayNameFormat="FirstTwoLetters" FirstDayOfWeek="Sunday"
                                                                                                ShowRowHeaders="False">
                                                                                            </Calendar>
                                                                                            <DatePopupButton HoverImageUrl="" ImageUrl="" />
                                                                                        </telerik:RadDatePicker>
                                                                                    </td>
                                                                                    <td>
                                                                                        &nbsp;<asp:Button ID="btnSearch" runat="server" Width="100px" Text="ค้นหา" />
                                                                                    </td>
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
                                            <td valign="top">
                                                <table id="tblQuota" runat="server">
                                                    <tr>
                                                        <td>
                                                            <asp:Panel ID="Panel2" runat="server" BackColor="Transparent" Font-Names="Tahoma"
                                                                Font-Size="10pt" GroupingText="ปริมาณโควต้าจัดสรร" Width="100%">
                                                                <table>
                                                                    <tr>
                                                                        <td valign="bottom">
                                                                            <font class="FormLabel">ปริมาณโควต้าจัดสรร:</font></td>
                                                                        <td valign="bottom">
                                                                            <asp:Label ID="lblTotalQuota" runat="server" Font-Size="14pt" ForeColor="#0000C0"></asp:Label>
                                                                        </td>
                                                                        <td valign="bottom">
                                                                            <font class="FormLabel">TONS</font>
                                                                        </td>
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
                            <tr class="m-frm-hdr">
                                <td>
                                    <table style="width: 100%" id="tblData" runat="server">
                                        <tr>
                                            <td style="width: 100%">
                                                <asp:Button ID="btnQuota" runat="server" Text="ทำรายการ" Width="150px" OnClientClick="return InsertQuotaDialog();" />
                                                <asp:Button ID="btnPrintReport" runat="server" Text="พิมพ์รายงาน" Width="150px" /></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100%">
                                                <asp:Repeater ID="repTranStock" runat="server">
                                                    <HeaderTemplate>
                                                        <table style="width: 100%" border="1px" cellpadding="1" cellspacing="1">
                                                            <tr>
                                                                <td rowspan="3" valign="middle" align="center" style="background-color: #C3D7F0;" >
                                                                    <font class="FormLabel">วันที่</font>
                                                                </td>
                                                                <td rowspan="3" valign="middle" align="center" style="background-color: #C3D7F0;" >
                                                                    <font class="FormLabel">ปริมาณจัดสรร</font>
                                                                </td>
                                                                <td colspan="2" valign="middle" align="center" style="background-color: #C3D7F0;" >
                                                                    <font class="FormLabel">คำขอหนังสือรับรองฯ</font>
                                                                </td>
                                                                <td colspan="4" valign="middle" align="center" style="background-color: #C3D7F0;" >
                                                                    <font class="FormLabel">การพิจารณาออกหนังสือรับรองฯ</font>
                                                                </td>
                                                                <td rowspan="3" valign="middle" align="center" style="background-color: #C3D7F0;" >
                                                                    <font class="FormLabel">ปริมาณคงเหลือ<br />(ยกไป)</font>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td rowspan="2" valign="middle" align="center" style="background-color: #C3D7F0;" >
                                                                    <font class="FormLabel">ชื่อผู้ส่งออก</font>
                                                                </td>
                                                                <td rowspan="2" valign="middle" align="center" style="background-color: #C3D7F0;" >
                                                                    <font class="FormLabel">ปริมาณที่ขอ<br />หนังสือรับรองฯ</font>
                                                                </td>
                                                                <td colspan="2" valign="middle" align="center" style="background-color: #C3D7F0;" >
                                                                    <font class="FormLabel">คำขอได้รับการอนุมัติ<br />หนังสือรับรองฯ</font>
                                                                </td>
                                                                <td colspan="2" valign="middle" align="center" style="background-color: #C3D7F0;" >
                                                                    <font class="FormLabel">คำขอไม่ได้รับการอนุมัติ</font>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td valign="middle" align="center" style="background-color: #C3D7F0;" >
                                                                    <font class="FormLabel">เลขที่หนังสือรับรองฯ</font>
                                                                </td>
                                                                <td valign="middle" align="center" style="background-color: #C3D7F0;" >
                                                                    <font class="FormLabel">ปริมาณ</font>
                                                                </td>
                                                                <td valign="middle" align="center" style="background-color: #C3D7F0;" >
                                                                    <font class="FormLabel">ต้องแก้ไข</font>
                                                                </td>
                                                                <td valign="middle" align="center" style="background-color: #C3D7F0;" >
                                                                    <font class="FormLabel">ถูกยกเลิก</font>
                                                                </td>
                                                            </tr>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td align="center">
                                                                <font class="FormLabel"><%#Eval("TSK_Date")%></font>
                                                            </td>
                                                            <td align="right">
                                                                <font class="FormLabel"><%# string.Format("{0:N5}", Eval("TSK_Debit"))%></font>
                                                            </td>
                                                            <td>
                                                                <font class="FormLabel"><%#Eval("CompanyName_En")%></font>
                                                            </td>
                                                            <td align="right">
                                                                <font class="FormLabel"><%#String.Format("{0:N5}", Eval("TSK_Credit"))%></font>
                                                            </td>
                                                            <td align="center">
                                                                <font class="FormLabel"><%#Eval("reference_code2")%></font>
                                                            </td>
                                                            <td align="right">
                                                                <font class="FormLabel"><%#String.Format("{0:N5}", Eval("TSK_Credit"))%></font>
                                                            </td>
                                                            <td>
                                                                <font class="FormLabel"><%#Eval("ReferenceDesc1")%></font>
                                                            </td>
                                                            <td>
                                                                <font class="FormLabel"><%#Eval("ReferenceDesc2")%></font>
                                                            </td>
                                                            <td align="right">
                                                                <font class="FormLabel"><%#String.Format("{0:N5}", Eval("Amount"))%></font>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        </table>
                                                    </FooterTemplate>
                                                </asp:Repeater>
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
<asp:Label ID="lblRoleID" runat="server" Visible="False"></asp:Label><br />
