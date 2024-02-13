<%@ Control language="vb" Inherits="NTi.Modules.DFT_EDI_AUsers.ViewDFT_EDI_AUsers" AutoEventWireup="false" Explicit="True" Codebehind="ViewDFT_EDI_AUsers.ascx.vb" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="rgUserList">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="rgUserList" />
            </UpdatedControls>
        </telerik:AjaxSetting>
    </AjaxSettings>
</telerik:RadAjaxManager>
<telerik:RadCodeBlock ID="RadCodeBlockFirst" runat="server">
    <script type="text/javascript">
        function ShowViewForm(user_id, action)
        {
            window.radopen("/DesktopModules/DFT_EDI_AUsers/frmUsers.aspx?userid=" + user_id + "&action=" + action, "ViewDialog");
            return false;
        }

        function ShowEditForm(user_id, action) {
            window.radopen("/DesktopModules/DFT_EDI_AUsers/frmUsers.aspx?userid=" + user_id + "&action=" + action, "EditDialog");
            return false;
        }
        
        function ShowInsertForm(){
            window.radopen("/DesktopModules/DFT_EDI_AUsers/frmUsers.aspx?action=new", "InsertDialog");
            return false;
        }

       function ShowDeleteForm(user_id) {
           window.radopen("/DesktopModules/DFT_EDI_AUsers/frmDeleteUser.aspx?userid=" + user_id, "DeleteDialog")
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
<telerik:RadWindowManager ID="RadWindowManager1" Behavior="Close" runat="server" Skin="Office2007" TabIndex="-1">
    <Windows>
        <telerik:RadWindow ID="ViewDialog" runat="server" Title="แสดงรายการสินค้า" ReloadOnShow="True" ShowContentDuringLoad="False" Modal="True"
        VisibleStatusbar="False" Width="800px" Height="300px" />
        
        <telerik:RadWindow ID="EditDialog" runat="server" Title="แก้ไขรายการสินค้า" ReloadOnShow="True" ShowContentDuringLoad="False" Modal="True"
        VisibleStatusbar="False" Width="800px" Height="300px" />
        
        <telerik:RadWindow ID="InsertDialog" runat="server" Title="เพิ่มรายการสินค้า" ReloadOnShow="True" ShowContentDuringLoad="False" Modal="True"
        VisibleStatusbar="False" Width="800px" Height="300px" />
        
        <telerik:RadWindow ID="DeleteDialog" Animation="Slide" runat="server" Title="ลบรายการสินค้า" ReloadOnShow="True" ShowContentDuringLoad="False" Modal="True"
        Width="450px" Height="160px" VisibleStatusbar="False" />
    </Windows>
</telerik:RadWindowManager>
<table border="0" cellpadding="0" cellspacing="5" width="100%">
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
                                                            <font class="groupbox">&nbsp; รายการบุคคลากรและสังกัด &nbsp;</font></td>
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
                                                        <td style="font-size: 12pt; color: #000000; font-family: Times New Roman">
                                                            <asp:Label ID="lblReturnMsg" runat="server" Font-Names="Tahoma" Font-Size="10pt" ForeColor="#0000C0"></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="font-size: 12pt; color: #000000; font-family: Times New Roman">
                                                            <font class="FormLabel">&nbsp;สังกัด :&nbsp;<asp:DropDownList ID="ddlSite" runat="server">
                                                            </asp:DropDownList>&nbsp;<asp:ImageButton
                                                                    ID="imbSearch" runat="server" ImageUrl="~/images/search.gif" /></font></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="font-size: 12pt; color: #000000; font-family: Times New Roman">
                                                            <telerik:RadGrid ID="rgUserList" runat="server" AllowPaging="True" AllowSorting="True"
                                                                GridLines="None" ShowStatusBar="True" Skin="Office2007" TabIndex="-1" Width="100%">
                                                                <MasterTableView AutoGenerateColumns="False" CellSpacing="-1" ClientDataKeyNames="user_id"
                                                                    DataKeyNames="user_id" NoMasterRecordsText="ไม่มีรายการคำร้อง" Width="100%">
                                                                    <GroupByExpressions>
                                                                        <telerik:GridGroupByExpression>
                                                                            <SelectFields>
                                                                                <telerik:GridGroupByField FieldAlias="site_id" FieldName="site_id" HeaderValueSeparator=" สังกัด : "></telerik:GridGroupByField>
                                                                            </SelectFields>
                                                                            <GroupByFields>
                                                                                <telerik:GridGroupByField FieldName="site_id" FieldAlias="site_id" ></telerik:GridGroupByField>
                                                                            </GroupByFields>
                                                                        </telerik:GridGroupByExpression>
                                                                    </GroupByExpressions>
                                                                    <Columns>
                                                                        <telerik:GridTemplateColumn UniqueName="TemplateViewColumn">
                                                                            <ItemStyle HorizontalAlign="Center"/>
                                                                            <HeaderStyle Width="15px" />
                                                                            <ItemTemplate>
                                                                                <asp:HyperLink ID="ViewLink" TabIndex="-1" runat="server" ImageUrl="~/images/view.gif" Text="View"></asp:HyperLink>
                                                                            </ItemTemplate>
                                                                        </telerik:GridTemplateColumn>
                                                                        <telerik:GridTemplateColumn UniqueName="TemplateEditColumn">
                                                                            <ItemStyle HorizontalAlign="Center"/>
                                                                            <HeaderStyle Width="15px" />
                                                                            <ItemTemplate>
                                                                                <asp:HyperLink ID="EditLink" TabIndex="-1" runat="server" ImageUrl="~/images/edit.gif" Text="Edit"></asp:HyperLink>
                                                                            </ItemTemplate>
                                                                        </telerik:GridTemplateColumn>
                                                                        <telerik:GridTemplateColumn UniqueName="TemplateDeleteColumn">
                                                                            <ItemStyle HorizontalAlign="Center"/>
                                                                            <HeaderStyle Width="15px" />
                                                                            <ItemTemplate>
                                                                                <asp:HyperLink ID="DeleteLink" TabIndex="-1" runat="server" ImageUrl="~/images/delete.gif" Text="Delete"></asp:HyperLink>
                                                                            </ItemTemplate>
                                                                        </telerik:GridTemplateColumn>
                                                                        <telerik:GridTemplateColumn UniqueName="TemplateRoleColumn">
                                                                            <ItemStyle HorizontalAlign="Center"/>
                                                                            <HeaderStyle Width="15px" />
                                                                            <ItemTemplate>
                                                                                <asp:HyperLink ID="RoleLink" TabIndex="-1" runat="server" ImageUrl="~/images/icon_securityroles_16px.gif" Text="Delete"></asp:HyperLink>
                                                                            </ItemTemplate>
                                                                        </telerik:GridTemplateColumn>
                                                                        <telerik:GridBoundColumn DataField="user_id" HeaderText="Login Name" SortExpression="user_id" UniqueName="user_id">
                                                                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="user_name" HeaderText="ชื่อ - สกุล" SortExpression="user_name" UniqueName="user_name">
                                                                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt"  />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="site_id" HeaderText="สังกัด" SortExpression="site_id" UniqueName="site_id">
                                                                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center"/>
                                                                        </telerik:GridBoundColumn>
                                                                        <%--<telerik:GridBoundColumn DataField="user_level" HeaderText="User Level" ReadOnly="True"
                                                                            SortExpression="user_level" UniqueName="user_level">
                                                                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center"/>
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="right_level" HeaderText="ระดับสิทธิ์" SortExpression="right_level" UniqueName="right_level">
                                                                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center"/>
                                                                        </telerik:GridBoundColumn>--%>
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
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>