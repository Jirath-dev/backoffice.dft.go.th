<%@ Control Language="vb" Inherits="YourCompany.Modules.DFT_CustomFormByA.ViewDFT_CustomFormByA"
    AutoEventWireup="false" Explicit="True" Codebehind="ViewDFT_CustomFormByA.ascx.vb" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
</telerik:RadAjaxManager>
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
                                            <td align="left" class="m-frm-hdr" style="height: 20px">
                                                <table cols="2" border="0" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td nowrap class="groupboxheader">
                                                            <font class="groupbox">&#160;&#160;แก้ไขหนังสือรับรอง&#160;&#160;</font>
                                                        </td>
                                                        <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
                                                            <td style="background-image: url(<%= ResolveUrl("images") %>/CORNER.gif); background-repeat: no-repeat;">
                                                                &#160;&#160;&#160;&#160;
                                                            </td>
                                                        </telerik:RadCodeBlock>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td align="right" class="m-frm-nav">
                                            </td>
                                        </tr>
                                    </table>
                                    <table style="width: 100%">
                                        <tr>
                                            <td style="width: 100%">
                                                <table cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td align="right" style="width: 179px; height:25px">
                                                            <font class="FormLabel">เลขที่อ้างอิง :&nbsp;</font>
                                                        </td>
                                                        <td style="width: 441px; height:25px">
                                                            <asp:TextBox ID="txtInvRunAuto" runat="server" Width="200px" Font-Bold="true" Font-Names="Tahoma"
                                                                Font-Size="12pt" ForeColor="blue"></asp:TextBox>
                                                            &nbsp;
                                                            <asp:Button ID="btnSearch" runat="server" Text="ค้นหา" Width="150px" /></td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" style="width: 179px; height: 20px">
                                                            <font class="FormLabel">เลขที่หนังสือรับรองที่ได้ :&nbsp;</font>
                                                        </td>
                                                        <td style="width: 441px; height: 20px">
                                                            <asp:Label ID="lblReferenceNo" runat="server" Text="" Font-Names="Tahoma" Font-Size="10pt"
                                                                Font-Bold="true" Height="20px" ForeColor="blue"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" style="width: 179px; height: 20px">
                                                            <font class="FormLabel">ชื่อบริษัท :&nbsp;</font>
                                                        </td>
                                                        <td style="width: 441px; height:20px">
                                                            <asp:Label ID="lblCompanyName" runat="server" Text="" Font-Names="Tahoma" Font-Size="10pt"
                                                                Font-Bold="true" Height="20px" ForeColor="blue"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" style="width: 179px; height:20px">
                                                            <font class="FormLabel">ประเทศเดิม :&nbsp;</font>
                                                        </td>
                                                        <td style="width: 441px; height:20px">
                                                            <asp:Label ID="lblOldCountry" runat="server" Text="" Font-Names="Tahoma" Font-Size="10pt"
                                                                Font-Bold="true" Height="20px" ForeColor="blue"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" style="width: 179px; height:25px">
                                                            <font class="FormLabel">ประเทศที่เปลี่ยน :&nbsp;</font>
                                                        </td>
                                                        <td style="height: 25px; width: 441px;">
                                                            <telerik:RadComboBox ID="rcbChangeCountry" runat="server" Width="200px">
                                                            </telerik:RadComboBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" style="width: 179px; height: 25px">
                                                            <font class="FormLabel">เลขที่หนังสือรับรอง :&nbsp;</font>
                                                        </td>
                                                        <td style="width: 441px; height: 25px;">
                                                            <asp:TextBox ID="txtRefCode2" runat="server" Width="158px" Text="" Font-Names="Tahoma"
                                                                Font-Size="10pt" Font-Bold="true" ForeColor="blue" Enabled="False"></asp:TextBox>
                                                            &nbsp;&nbsp;<asp:TextBox ID="txtRunningRefCode2" runat="server" Enabled="False" Width="70px" Visible="False"></asp:TextBox></td>
                                                        <td style="height: 24px; width: 382px;">
                                                    </tr>
                                                    <tr>
                                                        <td align="right" style="width: 179px; height:25px">
                                                            <font class="FormLabel">วันที่อนุมัติ :&nbsp;</font>
                                                        </td>
                                                        <td style="height: 25px; width: 441px;">
                                                            <telerik:RadDatePicker ID="rdpApproveDate" runat="server" Width="200px" SelectedDate="2014-12-30" Enabled="False">
                                                                <DateInput DisplayDateFormat="dd/MM/yyyy" SelectedDate="2014-12-30">
                                                                </DateInput>
                                                                <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                                                                </Calendar>
                                                                <DatePopupButton HoverImageUrl="" ImageUrl="" CssClass="rcCalPopup rcDisabled" />
                                                            </telerik:RadDatePicker>
                                                    </tr>
                                                </table>
                                                <table>
                                                    <tr>
                                                        <td style="width: 150px">
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblMessage" runat="server" Text="" Font-Bold="true" Font-Names="Tahoma"
                                                                Font-Size="10pt" ForeColor="Green" Visible="false"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <table>
                                                    <tr>
                                                        <td>
                                                        </td>
                                                        <td style="width: 210px">
                                                            <asp:Button ID="btnSaveCountry" runat="server" Text="1.บันทึกการเปลี่ยนแปลงประเทศ"
                                                                Width="200px" />
                                                        </td>
                                                        <td style="width: 210px">
                                                            <asp:Button ID="btnSaveRefCode2" runat="server" Text="2.บันทึกเลขที่หนังสือรับรอง"
                                                                Width="200px" />
                                                        </td>
                                                        <td style="width: 210px">
                                                            <asp:Button ID="btnSaveApproveDate" runat="server" Text="3.บันทึกแก้ไขวันที่อนุมัติ"
                                                                Width="200px" />
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
