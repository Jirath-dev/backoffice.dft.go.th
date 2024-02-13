<%@ Control Language="vb" Inherits="NTi.Modules.DFT_EDI_GovernmentSign.ViewDFT_EDI_GovernmentSign"
    AutoEventWireup="false" Explicit="True" Codebehind="ViewDFT_EDI_GovernmentSign.ascx.vb" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="RadCbbSite">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="RadCbbSite" />
                <telerik:AjaxUpdatedControl ControlID="RadCbbUser" />
                <telerik:AjaxUpdatedControl ControlID="Panel1" />
                <telerik:AjaxUpdatedControl ControlID="lbl_ErrMSG" />
                <telerik:AjaxUpdatedControl ControlID="rgRequestUser" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="RadCbbUser">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="RadCbbSite" />
                <telerik:AjaxUpdatedControl ControlID="RadCbbUser" />
                <telerik:AjaxUpdatedControl ControlID="Panel1" />
                <telerik:AjaxUpdatedControl ControlID="lbl_ErrMSG" />
                <telerik:AjaxUpdatedControl ControlID="rgRequestUser" />
            </UpdatedControls>
        </telerik:AjaxSetting>
    </AjaxSettings>
</telerik:RadAjaxManager>
<br />
<table border="0" cellpadding="0" cellspacing="5" width="100%">
    <tr align="center">
        <td>
            <font class="FormHeader"></font>
        </td>
    </tr>
    <tr>
        <td>
            <table border="0" cellpadding="1" cellspacing="0" class="m-frm-bdr" width="100%">
                <tr>
                    <td>
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr class="m-frm-hdr">
                                <td style="width: 896px">
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                        <tr class="m-frm-hdr">
                                            <td align="left" class="m-frm-hdr" width="12%">
                                                <table border="0" cellpadding="0" cellspacing="0" cols="2">
                                                    <tr>
                                                        <td class="groupboxheader" nowrap="nowrap">
                                                            <font class="groupbox">&nbsp;<asp:Label ID="lblHeader" runat="server" CssClass="groupbox">รายละเอียดข้อมูล&nbsp;&nbsp;</asp:Label></font></td>
                                                        <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
                                                            <td style='background-image: url(<%= ResolveUrl("../images") %>/groupbox/CORNER.gif);
                                                                background-repeat: no-repeat;'>
                                                                &nbsp; &nbsp;&nbsp;</td>
                                                        </telerik:RadCodeBlock>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td align="left" class="m-frm-nav" width="88%">
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr class="m-frm-hdr">
                                <td style="width: 896px">
                                    <telerik:RadTabStrip ID="RadTabStrip1" runat="server" Font-Names="Tahoma" Font-Size="10pt"
                                        MultiPageID="RadMultiPage1" SelectedIndex="0" Skin="Office2007" Width="100%">
                                        <Tabs>
                                            <telerik:RadTab runat="server" Font-Names="Tahoma" Font-Size="10pt" Selected="True"
                                                Text="ข้อมูลรายเซ็นเจ้าหน้าที่">
                                            </telerik:RadTab>
                                        </Tabs>
                                    </telerik:RadTabStrip>
                                    <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0" TabIndex="-1"
                                        Width="100%">
                                        <telerik:RadPageView ID="RadPageView1" runat="server" TabIndex="-1" Width="100%">
                                            <table border="0" cellpadding="0" cellspacing="4" class="FormLabel" style="width: 100%">
                                                <tr>
                                                    <td colspan="2" valign="top">
                                                        <table border="0" cellpadding="2" cellspacing="2">
                                                            <tr>
                                                                <td>
                                                                    สาขาของกรมฯ :.</td>
                                                                <td>
                                                        <telerik:RadComboBox ID="RadCbbSite" runat="server" AutoPostBack="True" DataTextField="site_name"
                                                            DataValueField="site_id" Width="350px" ForeColor="Blue" Skin="WebBlue">
                                                        </telerik:RadComboBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                        ชื่อ User :.</td>
                                                                <td>
                                                        <telerik:RadComboBox ID="RadCbbUser" runat="server" AutoPostBack="True" Width="300px"
                                                            ForeColor="Blue" Skin="WebBlue" Filter="StartsWith">
                                                        </telerik:RadComboBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td style="width: 45%" rowspan="2" valign="top">
                                                        <asp:Panel ID="Panel1" runat="server" CssClass="FormLabel" ForeColor="Blue" GroupingText="รูปภาพ"
                                                            HorizontalAlign="Center" Width="100%">
                                                            <asp:Image ID="Image1" runat="server" Height="189px" Width="340px" /><br />
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 15%" valign="top">
                                                    </td>
                                                    <td style="width: 40%" valign="top">
                                                        <asp:TextBox ID="lblRoleID" runat="server" Width="223px" Visible="False"></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td valign="top" align="center" colspan="3">
            <asp:Label ID="lbl_ErrMSG" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 15%" valign="top">
                                                    </td>
                                                    <td style="width: 40%" valign="top">
                                                    </td>
                                                    <td style="width: 45%" valign="top">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3" valign="top">
                                                        <telerik:RadGrid ID="rgRequestUser" runat="server" AllowPaging="True" AllowSorting="True"
                                        Font-Names="Tahoma" Font-Size="X-Small" ShowFooter="True" GridLines="None" Skin="Office2007" TabIndex="-1" PageSize="40">
                                        <MasterTableView AutoGenerateColumns="False" CellSpacing="-1" ClientDataKeyNames="AutoRunid"
                                            DataKeyNames="AutoRunid" NoMasterRecordsText="ไม่มีรายการ"
                                            Width="100%">
                                            <Columns>
                                               <telerik:GridTemplateColumn HeaderText="ลำดับ" HeaderStyle-Width="30px" UniqueName="TemplateColumn">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblIndex" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center"/>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridBoundColumn DataField="UserNameTemp" ReadOnly="True" SortExpression="UserNameTemp"
                                                    UniqueName="UserNameTemp" Visible="True" HeaderStyle-Width="300px" HeaderText="ชื่อ User ของเจ้าหน้าที่ที่ได้ทำการ Upload ลายเซ็น">
                                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="FullName" ReadOnly="True" SortExpression="FullName"
                                                    UniqueName="FullName" Visible="True" HeaderText="ชื่อ-นามสกุล">
                                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="SiteUser" ReadOnly="True" SortExpression="SiteUser"
                                                    UniqueName="SiteUser" Visible="True" HeaderText="สาขา" HeaderStyle-Width="100px">
                                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="StatusUse" ReadOnly="True" SortExpression="StatusUse"
                                                    UniqueName="StatusUse" Visible="false" HeaderText="สถานะ" HeaderStyle-Width="100px">
                                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridTemplateColumn HeaderText="สถานะ" UniqueName="TemplateColumn" HeaderStyle-Width="100px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSent_FormName" runat="server" Text='<%#Re_status(Eval("StatusUse")) %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Left" />
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridBoundColumn DataField="TAXID" ReadOnly="True" SortExpression="TAXID"
                                                    UniqueName="TAXID" Visible="True" HeaderText="เลขภาษี USB Token" HeaderStyle-Width="100px">
                                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                                </telerik:GridBoundColumn>
                                            </Columns>
                                        </MasterTableView>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ClientSettings>
                                            <Selecting AllowRowSelect="True" />
                                        </ClientSettings>
                                    </telerik:RadGrid><br />
                                                    </td>
                                                </tr>
                                            </table>
                                        </telerik:RadPageView>
                                    </telerik:RadMultiPage>
                                </td>
                            </tr>
                            <tr>
                                <td class="m-frm" style="width: 896px" valign="top" align="center">
                                    <asp:Button ID="BtnAdd" runat="server" Text="เพิ่มข้อมูลลายเซ็นเจ้าหน้าที่" />
                                    <asp:Button ID="BtnEdit" runat="server" Text="แก้ไขเปลี่ยนแปลงลายเซ็นเจ้าหน้าที่"
                                        Width="250px" /></td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td align="center">
            </td>
    </tr>
    <tr>
        <td align="right">
            &nbsp;
        </td>
    </tr>
</table>
