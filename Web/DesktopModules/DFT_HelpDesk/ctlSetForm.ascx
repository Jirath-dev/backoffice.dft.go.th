<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctlSetForm.ascx.vb" Inherits=".ctlSetForm" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<script src="/js/jquery/jquery.js"></script>

<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="RadAjaxManager1">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="grdForms" />
            </UpdatedControls>
        </telerik:AjaxSetting>
    </AjaxSettings>
</telerik:RadAjaxManager>
<telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        jQuery.noConflict();

        function OpenSetPrinterFormtype(printer_id) {
            window.radopen('/DesktopModules/DFT_HelpDesk/formSetForm.aspx?printer_id=' + printer_id, "PrinterSetting");
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
                                        <font class="groupbox">&#160;&#160;ตั้งค่าเครื่องพิมพ์&#160;&#160;</font></td>
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
                    <table border="0" cellpadding="0" cellspacing="2" class="FormLabel">
                            <tr>
                                <td style="width:200px">สาขา:
                                </td>
                                <td>
                                    <asp:DropDownList runat="server" ID="ddlSite" AutoPostBack="true"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td >ชื่อเครื่องพิมพ์:
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtPrinterName" Width="300px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td >รายละเอียด:
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtDesc" Width="300px" TextMode="MultiLine" Rows="3"></asp:TextBox>
                                </td>
                            </tr>
                             <tr>
                                <td >Active:
                                </td>
                                <td>
                                    <asp:CheckBox runat="server" ID="chkActive" Text="เปิด/ปิด การใช้งาน" />
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
                        <input type="button" id="btnNew" value="เพิ่มฟอร์ม" runat="server" />
                    </div>
                    <telerik:RadGrid ID="grdForms" runat="server" Width="100%"
                        GridLines="None" Skin="Office2007" TabIndex="-1">
                        <MasterTableView AutoGenerateColumns="False" ClientDataKeyNames="printer_formtype_id"
                            DataKeyNames="printer_formtype_id" NoMasterRecordsText="ไม่มีรายการข้อมูล">
                            <Columns>
                                <telerik:GridTemplateColumn UniqueName="PrintTemplateColumn" HeaderText="" Visible="True">
                                    <HeaderStyle Width="20px" HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:ImageButton runat="server" ID="ibtnDelete" ImageUrl="/images/delete.gif" 
                                            OnClientClick="return confirm('ต้องการลบข้อมูลนี้ ใช่หรือไม่');" OnClick="ibtnDelete_Click" CommandArgument='<%#Eval("printer_formtype_id") %>' />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn DataField="form_name" HeaderText="ชื่อฟอร์ม"
                                    ReadOnly="True" SortExpression="form_name" UniqueName="form_name" Visible="true">
                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="print_type_name" HeaderText="ประเภท"
                                    ReadOnly="True" SortExpression="print_type_name" UniqueName="print_type_name">
                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <%--<telerik:GridBoundColumn DataField="description" HeaderText="รายละเอียด"
                                    ReadOnly="True" SortExpression="description" UniqueName="description">
                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn UniqueName="active_flag2" DataField="active_flag" HeaderText="Active">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CheckBox1" runat="server" />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>--%>
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
