<%@ Control Language="vb" Inherits="NTi.Modules.DFT_EDI_BookingForm_Cancel.ViewDFT_EDI_BookingForm_Cancel"
    AutoEventWireup="false" Explicit="True" Codebehind="ViewDFT_EDI_BookingForm_Cancel.ascx.vb" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="RadAjaxManager1">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="rgRequestForm" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="rgRequestForm">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="rgRequestForm" />
            </UpdatedControls>
        </telerik:AjaxSetting>
    </AjaxSettings>
</telerik:RadAjaxManager>
<telerik:RadCodeBlock ID="RadCodeBlockFirst" runat="server">
    <script type="text/javascript">
        function ShowDeleteForm(invh_run_auto) {
            window.radopen("/DesktopModules/DFT_EDI_BookingForm_Cancel/frmDeleteBookingForm.aspx?InvHRunAuto=" + invh_run_auto , "DeleteDialog")
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
        <telerik:RadWindow ID="DeleteDialog" Animation="Slide" runat="server" ReloadOnShow="True" ShowContentDuringLoad="False" Modal="True"
        Width="450px" Height="160px" VisibleStatusbar="False" />
    </Windows>
</telerik:RadWindowManager>
<table width="100%" border="0" cellpadding="0" cellspacing="5">
    <tr>
        <td>
            <telerik:RadGrid ID="rgRequestForm" runat="server" AllowPaging="True" AllowSorting="True"
                GridLines="None" Skin="Office2007" TabIndex="-1" Width="100%" ShowStatusBar="True"
                PageSize="20">
                <MasterTableView AutoGenerateColumns="False" CellSpacing="-1" ClientDataKeyNames="invh_run_auto"
                    DataKeyNames="invh_run_auto" NoMasterRecordsText="" Width="100%">
                    <Columns>
                        <telerik:GridTemplateColumn UniqueName="TemplateDeleteColumn">
                            <ItemTemplate>
                                <asp:HyperLink ID="DeleteLink" TabIndex="-1" runat="server" ImageUrl="~/images/icon_recyclebin_16px.gif" Text="Delete"></asp:HyperLink>
                            </ItemTemplate>
                            <HeaderStyle Width="15px" />
                            <ItemStyle HorizontalAlign="Center" Width="15px" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridNumericColumn DataField="invh_run_auto" ReadOnly="True" HeaderText="เลขที่คำร้อง"
                            SortExpression="invh_run_auto" UniqueName="invh_run_auto" Aggregate="Count" FooterText="รวม : ">
                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                            <FooterStyle Font-Names="Tahoma" Font-Size="10pt" />
                        </telerik:GridNumericColumn>
                        <telerik:GridBoundColumn DataField="form_name" HeaderText="ฟอร์ม" ReadOnly="True"
                            SortExpression="form_name" UniqueName="form_name">
                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="company_name" HeaderText="บริษัท" SortExpression="company_name"
                            UniqueName="company_name">
                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                        </telerik:GridBoundColumn>
                    </Columns>
                </MasterTableView>
                <HeaderStyle HorizontalAlign="Center" Font-Names="Tahoma" Font-Size="10pt" />
                <ClientSettings EnableRowHoverStyle="True">
                    <Selecting AllowRowSelect="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </td>
    </tr>
</table>
<asp:Label ID="lblRoleID" runat="server" Visible="False"></asp:Label>
