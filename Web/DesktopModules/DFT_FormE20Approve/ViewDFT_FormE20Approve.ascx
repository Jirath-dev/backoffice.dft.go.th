<%@ Control language="vb" Inherits="NTi.Modules.DFT_FormE20Approve.ViewDFT_FormE20Approve" AutoEventWireup="false" Explicit="True" Codebehind="ViewDFT_FormE20Approve.ascx.vb" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="dnn" TagName="Audit" Src="~/controls/ModuleAuditControl.ascx" %>
<table border="0" cellpadding="0" cellspacing="4" style="width: 100%">
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
                                                            <font class="groupbox">&nbsp; ค้นหาข้อมูล &nbsp;</font></td>
                                                    </tr>
                                                </table>
                                                <asp:Label ID="lblRoleID" runat="server" Text="lblRoleID" Visible="False"></asp:Label>&nbsp;
                                                <asp:Label ID="lblDSRoleID" runat="server" Text="lblDSRoleID" Visible="False"></asp:Label></td>
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
                                                <table border="0" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td class="FormLabel">
                                                            ค้นหาเลขที่ภาษี :</td>
                                                        <td class="FormLabel">
                                                                <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox></td>
                                                        <td class="FormLabel">
                                                                <asp:Button ID="btnSearch" runat="server" Text="ค้นหา" ValidationGroup="search" Width="150px" /></td>
                                                        <td class="FormLabel">
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtSearch"
                                                                    Display="Dynamic" ErrorMessage="กรุณาป้อนเลขที่ภาษี" Font-Names="Tahoma" Font-Size="10pt"
                                                                    SetFocusOnError="True" ValidationGroup="search"></asp:RequiredFieldValidator></td>
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
        <td align="center">
            <asp:Label ID="lblError" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label></td>
    </tr>
    <tr>
        <td style="width: 100%">
            <asp:Panel ID="Panel_Company" runat="server" Visible="False" Width="100%">
                <table border="0" cellpadding="0" cellspacing="2" class="m-frm-bdr" style="width: 100%">
                    <tr>
                        <td class="m-frm-hdr" colspan="3" nowrap="nowrap">
                            <table border="0" cellpadding="0" cellspacing="0" cols="2">
                                <tr>
                                    <td class="groupboxheader" nowrap="nowrap">
                                        <font class="groupbox">&nbsp; รายละเอียด &nbsp;</font></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="FormLabel" style="width: 200px">
                            ชื่อบริษัท (ไทย)</td>
                        <td>
                            <asp:Label ID="lblcompany_thai" runat="server" CssClass="FormLabel" Font-Bold="True"
                                ForeColor="#0000C0"></asp:Label></td>
                        <td style="width: 40px">
                        </td>
                    </tr>
                    <tr>
                        <td class="FormLabel">
                            ชื่อบริษัท (อังกฤษ)</td>
                        <td>
                            <asp:Label ID="lblcompany_eng" runat="server" CssClass="FormLabel" Font-Bold="True"
                                ForeColor="#0000C0"></asp:Label></td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="FormLabel">
                            เลขที่ภาษี</td>
                        <td>
                            <asp:Label ID="lblcompany_taxno" runat="server" CssClass="FormLabel" Font-Bold="True"
                                ForeColor="#0000C0"></asp:Label></td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="FormLabel" valign="top">
                            สถานะการเปิดใช้งาน</td>
                        <td valign="top">
                            <table border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td valign="top">
                            <asp:RadioButtonList ID="rdoStatus" runat="server" AutoPostBack="True" CssClass="FormLabel"
                                RepeatDirection="Horizontal">
                                <asp:ListItem Value="1">เปิดการใช้งาน</asp:ListItem>
                                <asp:ListItem Value="0">ปิดการใช้งาน</asp:ListItem>
                            </asp:RadioButtonList></td>
                                    <td valign="middle">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="rdoStatus"
                                            CssClass="FormLabel" ErrorMessage="กรุณาเลือก" ValidationGroup="AddData"></asp:RequiredFieldValidator></td>
                                </tr>
                            </table>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="FormLabel" valign="top">
                            หมายเหตุ</td>
                        <td valign="top">
                            <asp:TextBox ID="txtRemarks" runat="server" Height="100px" TextMode="MultiLine" Width="400px"></asp:TextBox></td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <asp:Button ID="btnAddCo" runat="server" Text="บันทึกข้อมูล" ValidationGroup="AddData"
                                Width="150px" /></td>
                        <td>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </td>
    </tr>
    <tr>
        <td style="width: 100%">
            <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server"><AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="btnAddCo">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="lblError" />
                        <telerik:AjaxUpdatedControl ControlID="lblcompany_thai" />
                        <telerik:AjaxUpdatedControl ControlID="lblcompany_eng" />
                        <telerik:AjaxUpdatedControl ControlID="lblcompany_taxno" />
                        <telerik:AjaxUpdatedControl ControlID="txtRemarks" />
                        <telerik:AjaxUpdatedControl ControlID="btnAddCo" />
                        <telerik:AjaxUpdatedControl ControlID="rdoStatus" />
                        <telerik:AjaxUpdatedControl ControlID="RequiredFieldValidator2" />
                        <telerik:AjaxUpdatedControl ControlID="txtSearch" />
                        <telerik:AjaxUpdatedControl ControlID="btnSearch" />
                        <telerik:AjaxUpdatedControl ControlID="RequiredFieldValidator1" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="rdoStatus">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="rdoStatus" />
                        <telerik:AjaxUpdatedControl ControlID="RequiredFieldValidator2" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="RadAjaxManager1">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="Panel_Company" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="btnSearch">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="lblError" />
                        <telerik:AjaxUpdatedControl ControlID="Panel_Company" />
                        <telerik:AjaxUpdatedControl ControlID="txtSearch" />
                        <telerik:AjaxUpdatedControl ControlID="btnSearch" />
                        <telerik:AjaxUpdatedControl ControlID="RequiredFieldValidator1" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
</AjaxSettings>
</telerik:RadAjaxManager>
        </td>
    </tr>
</table>

