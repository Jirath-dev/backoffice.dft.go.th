<%@ Control Language="vb" Inherits="Modules.DFT_PrinterSetting.ViewDFT_PrinterSetting" AutoEventWireup="false" Explicit="True" CodeBehind="ViewDFT_PrinterSetting.ascx.vb" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<script src="/js/jquery/jquery.js"></script>

<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="RadAjaxManager1">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="grdPrinters" />
            </UpdatedControls>
        </telerik:AjaxSetting>
    </AjaxSettings>
</telerik:RadAjaxManager>
<telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        jQuery.noConflict();
        jQuery(document).ready(function () {
            jQuery('#btnNew').click(function () {
                OpenSetPrinter('new', '0');
            });
        });
        jQuery.noConflict();
        jQuery(document).ready(function () {
            jQuery('#btnRQNew').click(function () {
                OpenmRequiredr('new', '0');
            });
        });
        function OpenSetPrinter(act, printer_id) {
            window.radopen('/DesktopModules/DFT_PrinterSetting/formSetPrinter.aspx?act=' + act + '&printer_id=' + printer_id, "PrinterSetting");
            return false;
        }
        function OpenmRequiredr(act, Information_id) {
            window.radopen('/DesktopModules/DFT_PrinterSetting/formRequired.aspx?act=' + act + '&Information_id=' + Information_id, "PrinterSetting");
            return false;
        }
        function refreshGrid(arg) {
            if (!arg) {
                $find("<%= RadAjaxManager1.ClientID %>").ajaxRequest("Rebind");
            }
            else {
                $find("<%= RadAjaxManager1.ClientID %>").ajaxRequest("RebindAndNavigate");
            }
        }


        function OpenSetForms(printer_id) {
            window.location = '<%=EditUrl("ctlSetForm")%>' + '&printer_id=' + printer_id;
        }

    </script>
</telerik:RadCodeBlock>
<telerik:RadWindowManager ID="RadWindowManager1" Behavior="Close" runat="server"
    Skin="Office2007" TabIndex="-1">
    <Windows>
        <telerik:RadWindow ID="PrinterSetting" runat="server" Title="เครื่องเพิมพ์" ReloadOnShow="True"
            ShowContentDuringLoad="False" Modal="True" VisibleStatusbar="False" Width="900px"
            Height="400px" />
    </Windows>
</telerik:RadWindowManager>
<div style="margin: 5px;">
    <table cellspacing="1" cellpadding="0" class="m-frm-bdr" width="100%" border="0">
        <tr class="m-frm-hdr">
            <td>
                <table cellspacing="0" cellpadding="0" border="0" width="100%">
                    <tr class="m-frm-hdr">
                        <td width="88%" align="left" class="m-frm-hdr">
                            <table cols="2" border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td nowrap class="groupboxheader">
                                        <font class="groupbox">&#160;&#160;ระบบงานพิมพ์&#160;&#160;</font></td>
                                    <telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">
                                        <td style="background-image: url(<%= ResolveUrl("images") %>/CORNER.gif); background-repeat: no-repeat;">&#160;&#160;&#160;&#160;</td>
                                    </telerik:RadCodeBlock>
                                </tr>
                            </table>
                        </td>
                        <td width="12%" align="left" class="m-frm-nav"></td>
                    </tr>
                </table>
            </td>
        </tr>

        <tr>
            <td class="m-frm" valign="top" width="100%">
                <div style="border: 1px solid #ccc; padding: 5px; margin: 5px;">
                    <table border="0" cellpadding="0" cellspacing="2">
                        <tr>
                            <td class="FormLable">สาขา:
                            </td>
                            <td>
                                <asp:DropDownList runat="server" ID="ddlSite" AutoPostBack="true"></asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
        <tr>
            <td class="m-frm" style="width: 100%; vertical-align: top;">

                <div style="border: 1px solid #ccc; padding: 5px; margin: 5px;">
                     <div style="padding-bottom: 5px;">

                        <input type="button" id="btnNew" value="เพิ่มเครื่องพิมพ์" />
                    </div>

                    <telerik:RadGrid ID="grdPrinters" runat="server" Width="100%"
                        GridLines="None" Skin="Office2007" TabIndex="-1">
                        <MasterTableView AutoGenerateColumns="False" ClientDataKeyNames="printer_id"
                            DataKeyNames="printer_id" NoMasterRecordsText="ไม่มีรายการข้อมูล">
                            <Columns>
                                <telerik:GridTemplateColumn UniqueName="PrintTemplateColumn" HeaderText="" Visible="True">
                                    <HeaderStyle Width="20px" HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <img src="/images/edit.gif" style="cursor: pointer;" onclick="OpenSetPrinter('edit','<%#Eval("printer_id") %>')" />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <%--<telerik:GridTemplateColumn UniqueName="PrintTemplateColumn" HeaderText="" Visible="True">
                                    <HeaderStyle Width="20px" HorizontalAlign="Center" />
                                    <ItemStyle  HorizontalAlign="Center" />
                                    <ItemTemplate>
                                       <asp:ImageButton runat="server" ID="btnDelete" />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>--%>
                                <telerik:GridBoundColumn DataField="site_name" HeaderText="สาขา"
                                    ReadOnly="True" SortExpression="site_name" UniqueName="site_name" Visible="true">
                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="printer_name" HeaderText="เครื่องพิมพ์"
                                    ReadOnly="True" SortExpression="printer_name" UniqueName="printer_name">
                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <%--                            <telerik:GridBoundColumn DataField="description" HeaderText="ประเภท"
                                ReadOnly="True" SortExpression="description" UniqueName="description">
                                <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>--%>
                                <telerik:GridBoundColumn DataField="description" HeaderText="รายละเอียด"
                                    ReadOnly="True" SortExpression="description" UniqueName="description">
                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn UniqueName="active_flag2" DataField="active_flag" HeaderText="Active">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CheckBox1" runat="server" />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn DataField="forms" HeaderText="จำนวนฟอร์ม"
                                    ReadOnly="True" SortExpression="forms" UniqueName="forms">
                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                    <HeaderStyle Width="100px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn UniqueName="PrintTemplateColumn" HeaderText="กำหนดฟอร์ม" Visible="True">
                                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                                    <ItemStyle Width="100px" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <img src="/images/reports.png" style="cursor: pointer;" onclick="OpenSetForms('<%#Eval("printer_id") %>')" />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn UniqueName="PrintTemplateColumn" HeaderText="กำหนด Users" Visible="True">
                                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                                    <ItemStyle Width="100px" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <img src="/images/icon_hostusers_32px.gif" style="cursor: pointer;" />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                            </Columns>
                        </MasterTableView>
                        <HeaderStyle HorizontalAlign="Center" Font-Names="Tahoma" Font-Size="10pt" />
                        <ItemStyle Wrap="False" />
                        <SelectedItemStyle Wrap="True" BackColor="DarkOrange" />
                    </telerik:RadGrid>
                </div>
            </td>
        </tr>
    </table>
    <table cellspacing="1" cellpadding="0" class="m-frm-bdr" width="100%" border="0">
        <tr class="m-frm-hdr">
            <td>
                <table cellspacing="0" cellpadding="0" border="0" width="100%">
                    <tr class="m-frm-hdr">
                        <td width="88%" align="left" class="m-frm-hdr">
                            <table cols="2" border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td nowrap class="groupboxheader">
                                        <font class="groupbox">&#160;&#160;ระบบงาน&#160;&#160;</font></td>
                                    <telerik:RadCodeBlock ID="RadCodeBlock3" runat="server">
                                        <td style="background-image: url(<%= ResolveUrl("images") %>/CORNER.gif); background-repeat: no-repeat;">&#160;&#160;&#160;&#160;</td>
                                    </telerik:RadCodeBlock>
                                </tr>
                            </table>
                        </td>
                        <td width="12%" align="left" class="m-frm-nav"></td>
                    </tr>
                </table>
            </td>
        </tr>

        <tr>
            <td class="m-frm" valign="top" width="100%">
                <div style="border: 1px solid #ccc; padding: 5px; margin: 5px;">
                    <table border="0" cellpadding="0" cellspacing="2">
                        <tr>
                            <td class="FormLable">ระบบงาน:
                            </td>
                            <td>
                                <asp:DropDownList runat="server" ID="ddlIE" AutoPostBack="true"></asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
        <tr>
            <td class="m-frm" style="width: 100%; vertical-align: top;">

                <div style="border: 1px solid #ccc; padding: 5px; margin: 5px;">
                     <div style="padding-bottom: 5px;">

                        <input type="button" id="btnRQNew" value="เพิ่มเคสที่ได้รับแจ้ง" />
                    </div>

                    <telerik:RadGrid ID="grdlog" runat="server" Width="100%"
                        GridLines="None" Skin="Office2007" TabIndex="-1">
                        <MasterTableView AutoGenerateColumns="False" ClientDataKeyNames="Information_id"
                            DataKeyNames="Information_id" NoMasterRecordsText="ไม่มีรายการข้อมูล">
                            <Columns>
                                <telerik:GridTemplateColumn UniqueName="PrintTemplateColumn" HeaderText="" Visible="True">
                                    <HeaderStyle Width="20px" HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <img src="/images/edit.gif" style="cursor: pointer;" onclick="OpenmRequiredr('edit','<%#Eval("Information_id") %>')" />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <%--<telerik:GridTemplateColumn UniqueName="PrintTemplateColumn" HeaderText="" Visible="True">
                                    <HeaderStyle Width="20px" HorizontalAlign="Center" />
                                    <ItemStyle  HorizontalAlign="Center" />
                                    <ItemTemplate>
                                       <asp:ImageButton runat="server" ID="btnDelete" />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>--%>
                                <telerik:GridBoundColumn DataField="Information_system" HeaderText="ระบบ"
                                    ReadOnly="True" SortExpression="Information_system" UniqueName="Information_system" Visible="true">
                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="User_case" HeaderText="เคสที่ได้รับแจ้ง"
                                    ReadOnly="True" SortExpression="User_case" UniqueName="User_case">
                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <%--                            <telerik:GridBoundColumn DataField="description" HeaderText="ประเภท"
                                ReadOnly="True" SortExpression="description" UniqueName="description">
                                <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>--%>
                                <telerik:GridBoundColumn DataField="description" HeaderText="รายละเอียด"
                                    ReadOnly="True" SortExpression="description" UniqueName="description">
                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="description_fix" HeaderText="วิธีแก้ไข"
                                    ReadOnly="True" SortExpression="description_fix" UniqueName="description_fix">
                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn UniqueName="active_flag2" DataField="active_flag" HeaderText="Active">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CheckBox1" runat="server" />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                              
                                <telerik:GridTemplateColumn UniqueName="username_case" DataField="username_case" HeaderText=" Users ที่แจ้ง" 
                                     SortExpression="username_case" Visible="True" ReadOnly="True">
                                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                                    <ItemStyle Width="100px" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <img src="/images/icon_hostusers_32px.gif" style="cursor: pointer;" />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                            </Columns>
                        </MasterTableView>
                        <HeaderStyle HorizontalAlign="Center" Font-Names="Tahoma" Font-Size="10pt" />
                        <ItemStyle Wrap="False" />
                        <SelectedItemStyle Wrap="True" BackColor="DarkOrange" />
                    </telerik:RadGrid>
                </div>
            </td>
        </tr>
    </table>
</div>
