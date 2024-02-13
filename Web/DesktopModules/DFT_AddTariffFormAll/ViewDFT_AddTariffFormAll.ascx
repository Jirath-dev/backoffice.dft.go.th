<%@ Control Language="vb" Inherits="YourCompany.Modules.DFT_AddTariffFormAll.ViewDFT_AddTariffFormAll"
    AutoEventWireup="false" Explicit="True" Codebehind="ViewDFT_AddTariffFormAll.ascx.vb" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="dnn" TagName="Audit" Src="~/controls/ModuleAuditControl.ascx" %>
<telerik:RadCodeBlock ID="RadCodeBlockFirst" runat="server">

    <script type="text/javascript">
        function ShowViewForm(tariff_code, country_code, action,selectform)
        {
            window.radopen("<%=SiteUrl%>" + "/DesktopModules/DFT_AddTariffFormAll/FormAdd.aspx?Itariff_code=" + tariff_code + "&Icountry_code=" + country_code + "&action=" + action + "&selectform=" + selectform, "ViewDialog");
            return false;
        }
        function ShowViewForm21(tariff_code, country_code, action,selectform,scat)
        {
            window.radopen("<%=SiteUrl%>" + "/DesktopModules/DFT_AddTariffFormAll/FormAdd.aspx?Itariff_code=" + tariff_code + "&Icountry_code=" + country_code + "&action=" + action + "&selectform=" + selectform + "&Icat=" + scat, "ViewDialog");
            return false;
        }
        function ShowEditForm(tariff_code, country_code, action,selectform) {
            window.radopen("<%=SiteUrl%>" + "/DesktopModules/DFT_AddTariffFormAll/FormAdd.aspx?Itariff_code=" + tariff_code + "&Icountry_code=" + country_code + "&action=" + action + "&selectform=" + selectform, "EditDialog");
            return false;
        }
        function ShowEditForm21(tariff_code, country_code, action,selectform,scat) {
            window.radopen("<%=SiteUrl%>" + "/DesktopModules/DFT_AddTariffFormAll/FormAdd.aspx?Itariff_code=" + tariff_code + "&Icountry_code=" + country_code + "&action=" + action + "&selectform=" + selectform + "&Icat=" + scat, "EditDialog");
            return false;
        }
        function ShowInsertForm(){
            window.radopen("<%=SiteUrl%>" + "/DesktopModules/DFT_AddTariffFormAll/FormAdd.aspx?action=new", "InsertDialog");
            return false;
        }

       function ShowDeleteForm(tariff_code, country_code, sform) {
           window.radopen("<%=SiteUrl%>" + "/DesktopModules/DFT_AddTariffFormAll/Popup/FormItemDeleted.aspx?Itariff_code=" + tariff_code + "&Icountry_code=" + country_code + "&Iform=" + sform , "DeleteDialog")
           return false;
       }
        function ShowDeleteForm21(tariff_code, country_code, scat, sform) {
           window.radopen("<%=SiteUrl%>" + "/DesktopModules/DFT_AddTariffFormAll/Popup/FormItemDeleted.aspx?Itariff_code=" + tariff_code + "&Icountry_code=" + country_code + "&Icat=" + scat + "&Iform=" + sform , "DeleteDialog")
           return false;
       }
        function onFocus(sender, eventArgs){
            $find("<%= txtNumTariff.ClientID %>").focus();
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
        
       function InitializePopup(sender, eventArgs) {
                if (sender.isEmpty()) {
                    eventArgs.set_cancelCalendarSynchronization(true);
                    var popup = eventArgs.get_popupControl();
                    if (popup == sender.get_calendar()) {
                        if (popup.get_selectedDates().length == 0) {
                            var todaysDate = new Date();
                            var todayTriplet = [todaysDate.getFullYear(), todaysDate.getMonth() + 1, todaysDate.getDate()];
                            popup.selectDate(todayTriplet, true);
                        }
                    }
                    else {
                        var time = popup.getTime();
                        if (!time) {
                            time = new Date();
                            time.setHours(12);
                            popup.setTime(time);
                        }
                    }
                }
            }
    </script>

</telerik:RadCodeBlock>
&nbsp;<table border="0" cellpadding="2" cellspacing="2" style="width: 100%">
    <tr>
        <td style="width: 15%; background-color: skyblue;" class="FormLabel" valign="top">
            ฟอร์ม</td>
        <td style="width: 70%" valign="top">
            <telerik:RadComboBox ID="DDLListForm" runat="server" AutoPostBack="True" DataSourceID="SqlDataSource1"
                DataTextField="form_name" DataValueField="form_type" Skin="Vista" Width="70%"
                TabIndex="-1">
            </telerik:RadComboBox>
        </td>
        <td style="width: 15%">
        </td>
    </tr>
    <tr>
        <td style="width: 15%; background-color: skyblue;" class="FormLabel" valign="top">
            ประเทศ</td>
        <td style="width: 70%" valign="top">
            <telerik:RadComboBox ID="DDLcountry" runat="server" BackColor="Transparent" DataTextField="DESCRIPTION"
                DataValueField="CODE" Width="50%" TabIndex="-1" Skin="Vista">
            </telerik:RadComboBox>
        </td>
        <td style="width: 15%">
        </td>
    </tr>
    <tr>
        <td style="width: 15%; background-color: skyblue;" class="FormLabel">
            พิกัดศุลกากร</td>
        <td style="width: 70%">
            <telerik:RadTextBox ID="txtNumTariff" runat="server" Width="260px">
            </telerik:RadTextBox>
            <asp:Button ID="btnSearch" runat="server" Text="ค้นหาพิกัดศุลกากร" Width="120px"
                TabIndex="1" />
            <asp:Button ID="btnAddTariffForm" runat="server" Text="เพิ่มพิกัดศุลกากร" OnClientClick="return ShowInsertForm();"
                Width="120px" TabIndex="2" /></td>
        <td style="width: 15%">
        </td>
    </tr>
    <tr>
        <td style="width: 15%">
        </td>
        <td style="width: 70%">
            <asp:Label ID="lblMsg" runat="server" Font-Names="Tahoma" Font-Size="10pt" ForeColor="Red"></asp:Label></td>
        <td style="width: 15%">
        </td>
    </tr>
    <tr>
        <td colspan="3">
            <telerik:RadGrid ID="GridTariff" runat="server" Skin="Vista" PageSize="40" TabIndex="-1">
                <MasterTableView AutoGenerateColumns="False" PageSize="10" Width="100%" DataKeyNames="tariff_code,country_code"
                    ClientDataKeyNames="tariff_code">
                    <Columns>
                        <telerik:GridTemplateColumn UniqueName="TemplateViewColumn">
                            <ItemStyle HorizontalAlign="Center" Width="20px" />
                            <ItemTemplate>
                                <asp:HyperLink ID="ViewLink" TabIndex="-1" runat="server" ImageUrl="~/images/view.gif"
                                    Text="View"></asp:HyperLink>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn UniqueName="TemplateEditColumn">
                            <ItemStyle HorizontalAlign="Center" Width="20px" />
                            <ItemTemplate>
                                <asp:HyperLink ID="EditLink" TabIndex="-1" runat="server" ImageUrl="~/images/edit.gif"
                                    Text="Edit"></asp:HyperLink>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn UniqueName="TemplateDeleteColumn">
                            <ItemStyle HorizontalAlign="Center" Width="20px" />
                            <ItemTemplate>
                                <asp:HyperLink ID="DeleteLink" TabIndex="-1" runat="server" ImageUrl="~/images/delete.gif"
                                    Text="Delete"></asp:HyperLink>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="tariff_code" HeaderText="พิกัดศุลกากร" ReadOnly="True"
                            SortExpression="tariff_code" UniqueName="tariff_code">
                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                            <HeaderStyle Width="14%" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="country_code" HeaderText="ประเทศ" Visible="False"
                            ReadOnly="True" SortExpression="country_code" UniqueName="country_code">
                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="cat" HeaderText="cat" Visible="False" ReadOnly="True"
                            SortExpression="cat" UniqueName="cat">
                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="tariff_name" HeaderText="รายละเอียดพิกัดศุลกากร"
                            ReadOnly="True" SortExpression="tariff_name" UniqueName="tariff_name">
                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="check_digit" HeaderText="จำนวนพิกัดศุลกากร" ReadOnly="True"
                            SortExpression="check_digit" UniqueName="check_digit">
                            <ItemStyle Font-Names="Tahoma" HorizontalAlign="Center" Font-Size="10pt" />
                            <HeaderStyle Width="14%" />
                        </telerik:GridBoundColumn>
                    </Columns>
                </MasterTableView>
                <HeaderStyle HorizontalAlign="Center" Font-Names="Tahoma" Font-Size="10pt" />
                <ClientSettings EnableRowHoverStyle="True">
                    <Selecting AllowRowSelect="True" />
                    <Scrolling AllowScroll="True" />
                </ClientSettings>
            </telerik:RadGrid>
            <telerik:RadGrid ID="Grid2" runat="server" Skin="Vista" PageSize="40" TabIndex="-1"
                Visible="False">
                <MasterTableView AutoGenerateColumns="False" PageSize="10" Width="100%" DataKeyNames="tariff_code,country_code,cat"
                    ClientDataKeyNames="tariff_code">
                    <Columns>
                        <telerik:GridTemplateColumn UniqueName="TemplateViewColumn">
                            <ItemTemplate>
                                <asp:HyperLink ID="ViewLink" runat="server" ImageUrl="~/images/view.gif" TabIndex="-1"
                                    Text="View"></asp:HyperLink>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="20px" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn UniqueName="TemplateEditColumn">
                            <ItemTemplate>
                                <asp:HyperLink ID="EditLink" runat="server" ImageUrl="~/images/edit.gif" TabIndex="-1"
                                    Text="Edit"></asp:HyperLink>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="20px" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn UniqueName="TemplateDeleteColumn">
                            <ItemTemplate>
                                <asp:HyperLink ID="DeleteLink" runat="server" ImageUrl="~/images/delete.gif" TabIndex="-1"
                                    Text="Delete"></asp:HyperLink>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="20px" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="tariff_code" HeaderText="พิกัดศุลกากร" ReadOnly="True"
                            SortExpression="tariff_code" UniqueName="tariff_code">
                            <HeaderStyle Width="14%" />
                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="country_code" HeaderText="ประเทศ" Visible="False"
                            ReadOnly="True" SortExpression="country_code" UniqueName="country_code">
                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="cat" HeaderText="cat" Visible="False" ReadOnly="True"
                            SortExpression="cat" UniqueName="cat">
                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="tariff_name" HeaderText="รายละเอียดพิกัดศุลกากร"
                            ReadOnly="True" SortExpression="tariff_name" UniqueName="tariff_name">
                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="check_digit" HeaderText="จำนวนพิกัดศุลกากร" ReadOnly="True"
                            SortExpression="check_digit" UniqueName="check_digit">
                            <HeaderStyle Width="14%" />
                            <ItemStyle Font-Names="Tahoma" HorizontalAlign="Center" Font-Size="10pt" />
                        </telerik:GridBoundColumn>
                    </Columns>
                </MasterTableView>
                <HeaderStyle HorizontalAlign="Center" Font-Names="Tahoma" Font-Size="10pt" />
                <ClientSettings EnableRowHoverStyle="True">
                    <Selecting AllowRowSelect="True" />
                    <Scrolling AllowScroll="True" />
                </ClientSettings>
            </telerik:RadGrid></td>
    </tr>
</table>
<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:EDIConnection %>"
    SelectCommand="SELECT     form_type, form_name, reserved_record, price, ShowOrder, ShowOrder_Report FROM dbo.form_type WHERE (form_type NOT IN ('ALL', 'FORM1_5', 'FORM3_1', 'FORM4_1'))">
</asp:SqlDataSource>
<asp:Label ID="lblRoleID" runat="server" Text="lblRoleID" Visible="False" TabIndex="-1"></asp:Label>
<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="DDLListForm">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="DDLListForm" />
                <telerik:AjaxUpdatedControl ControlID="DDLcountry" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="RadAjaxManager1">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="GridTariff" />
                <telerik:AjaxUpdatedControl ControlID="Grid2" />
                <telerik:AjaxUpdatedControl ControlID="RadWindowManager1" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="RadWindowManager1">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="RadWindowManager1" />
            </UpdatedControls>
        </telerik:AjaxSetting>
    </AjaxSettings>
</telerik:RadAjaxManager>
<telerik:RadWindowManager ID="RadWindowManager1" Behavior="Close" runat="server"
    Skin="Office2007" TabIndex="-1">
    <Windows>
        <telerik:RadWindow ID="ViewDialog" runat="server" Title="แสดงพิกัดศุลกากร" ReloadOnShow="True"
            ShowContentDuringLoad="False" Modal="True" VisibleStatusbar="False" Width="800px"
            Height="500px" />
        <telerik:RadWindow ID="EditDialog" runat="server" Title="แก้ไขพิกัดศุลกากร" ReloadOnShow="True"
            ShowContentDuringLoad="False" Modal="True" VisibleStatusbar="False" Width="800px"
            Height="500px" />
        <telerik:RadWindow ID="InsertDialog" runat="server" Title="เพิ่มพิกัดศุลกากร" ReloadOnShow="True"
            ShowContentDuringLoad="False" Modal="True" VisibleStatusbar="False" Width="800px"
            Height="500px" />
        <telerik:RadWindow ID="DeleteDialog" Animation="Slide" runat="server" Title="ลบรายการสินค้า"
            ReloadOnShow="True" ShowContentDuringLoad="False" Modal="True" Width="450px"
            Height="160px" VisibleStatusbar="False" />
    </Windows>
</telerik:RadWindowManager>
