<%@ Control language="vb" Inherits="YourCompany.Modules.DFT_EDI_ChangeDataDetail.ViewDFT_EDI_ChangeDataDetail" AutoEventWireup="false" Explicit="True" Codebehind="ViewDFT_EDI_ChangeDataDetail.ascx.vb" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="dnn" TagName="Audit" Src="~/controls/ModuleAuditControl.ascx" %>
<telerik:radajaxmanager id="RadAjaxManager1" runat="server">
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="RadAjaxManager1">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="grdItemData" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        
    </AjaxSettings>
</telerik:radajaxmanager>
<telerik:RadCodeBlock ID="RadCodeBlockFirst" runat="server">
<script type="text/javascript">
function ShowEditForm(invh_run_auto, invd_run_auto, action) {
            window.radopen("<%=SiteUrl%>" + "/DesktopModules/DFT_EDI_ChangeDataDetail/Popup/FormALLDetail.aspx?InvHRunAuto=" + invh_run_auto + "&InvDRunAuto=" + invd_run_auto + "&action=" + action, "EditDialog");
            return false;
        }
        var updated_grid = true;
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
            <% if Request.QueryString("signed")="true" then %>
            if(updated_grid==true){
             window.setTimeout('RebindGrid1("<%= grdItemData.ClientID %>")',300);
	         updated_grid = false;
	        }
	        <% end if %>
        }
        
        
        function RebindGrid1(grid_name)
        {
            try{
                var masterTable = $find(grid_name).get_masterTableView();
                masterTable.rebind();
            }catch(e){ }
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
<telerik:RadWindowManager ID="RadWindowManager1" runat="server">
<Windows>
        <telerik:RadWindow ID="EditDialog" runat="server" Title="แก้ไขรายการสินค้า" ReloadOnShow="True" ShowContentDuringLoad="False" Modal="True"
        VisibleStatusbar="False" Width="700px" Height="400px" />
        
    </Windows>
</telerik:RadWindowManager>
<br />
<table border="0" cellpadding="1" cellspacing="0" class="m-frm-bdr" width="100%">
    <tr>
        <td>
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr class="m-frm-hdr">
                    <td>
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr class="m-frm-hdr">
                                <td align="left" class="m-frm-hdr">
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
                                <td width="15">
                                    &nbsp;</td>
                                <td>
                                    <table border="0" cellpadding="0" cellspacing="2">
                                        <tr>
                                            <td>
                                                <font class="FormLabel">หมายเลขคำร้อง :</font>&nbsp;&nbsp;<asp:TextBox ID="txtSearchValue" runat="server" Font-Bold="True" Font-Names="Tahoma"
                                                    Font-Size="12pt" ForeColor="Blue" Width="170px"></asp:TextBox>&nbsp;
                                            </td>
                                            <td>
                                                <font class="FormLabel"></font>
                                                <asp:CheckBox ID="chkUseRef2" runat="server" Font-Names="Tahoma" Font-Size="10pt"
                                                    Text="ใช้เลขที่หนังสือรับรอง" /></td>
                                            <td>
                                                &nbsp;
                                                <asp:Button ID="btnSearchForm" runat="server" Text="ค้นหา" Width="150px" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td width="15">
                                </td>
                                <td>
                                    <table id="tblData1" runat="server" border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                                        <tr>
                                            <td class="FormLabel" style="width: 105px">
                                                ชื่อบริษัท</td>
                                            <td class="FormLabel" colspan="2">
                                                <asp:TextBox ID="txtCom" runat="server" Enabled="False" Width="380px"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td class="FormLabel" style="width: 105px">
                                                เลขบัตร</td>
                                            <td class="FormLabel" colspan="2">
                                                <asp:TextBox ID="txtCard_id" runat="server" Enabled="False" Width="230px"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td class="FormLabel" style="width: 105px">
                                                ฟอร์ม :</td>
                                            <td class="FormLabel" colspan="2">
                                                <asp:TextBox ID="txtform" runat="server" Enabled="False" Width="360px"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td class="FormLabel" style="width: 105px">
                                            </td>
                                            <td class="FormLabel" colspan="2">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="FormLabel" style="width: 105px">
                                            </td>
                                            <td class="FormLabel" colspan="2">
                                                รายการสินค้า</td>
                                        </tr>
                                        <tr>
                                            <td class="FormLabel" colspan="3">
                                                <telerik:RadGrid ID="grdItemData" runat="server" AllowPaging="True" AllowSorting="True"
                                                    Font-Names="Tahoma" Font-Size="X-Small" GridLines="None" PageSize="40" ShowFooter="True"
                                                    Skin="Office2007" TabIndex="-1">
                                                    <MasterTableView AutoGenerateColumns="False" CellSpacing="-1" ClientDataKeyNames="invh_run_auto,invd_run_auto"
                                                        DataKeyNames="invh_run_auto,invd_run_auto" NoMasterRecordsText="ไม่มีรายการสินค้า"
                                                        Width="100%">
                                                        <Columns>
                                                            <telerik:GridTemplateColumn UniqueName="TemplateViewColumn" Visible="false">
                                                                <ItemStyle HorizontalAlign="Center" Width="20px" />
                                                                <ItemTemplate>
                                                                    <asp:HyperLink ID="ViewLink" runat="server" ImageUrl="~/images/view.gif" Text="View"></asp:HyperLink>
                                                                </ItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridTemplateColumn UniqueName="TemplateEditColumn">
                                                                <ItemStyle HorizontalAlign="Center" Width="20px" />
                                                                <ItemTemplate>
                                                                    <asp:HyperLink ID="EditLink" runat="server" ImageUrl="~/images/edit.gif" Text="Edit"></asp:HyperLink>
                                                                </ItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridTemplateColumn UniqueName="TemplateDeleteColumn" Visible="false">
                                                                <ItemStyle HorizontalAlign="Center" Width="20px" />
                                                                <ItemTemplate>
                                                                    <asp:HyperLink ID="DeleteLink" runat="server" ImageUrl="~/images/delete.gif" Text="Delete"></asp:HyperLink>
                                                                </ItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridTemplateColumn HeaderText="ลำดับ" UniqueName="TemplateColumn" ItemStyle-Width="10%">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblIndex" runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridBoundColumn DataField="InvH_Run_Auto" ReadOnly="True" SortExpression="InvH_Run_Auto"
                                                                UniqueName="InvH_Run_Auto" Visible="False">
                                                                <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="InvD_Run_Auto" ReadOnly="True" SortExpression="InvD_Run_Auto"
                                                                UniqueName="InvD_Run_Auto" Visible="False">
                                                                <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="product_name" HeaderText="สินค้า" SortExpression="product_name"
                                                                UniqueName="product_name">
                                                                <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="tariff_code" HeaderText="พิกัดสินค้า" SortExpression="tariff_code"
                                                                UniqueName="tariff_code" ItemStyle-Width="30%">
                                                                <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                                            </telerik:GridBoundColumn>
                                                            <%--<telerik:GridNumericColumn Aggregate="Sum" DataField="net_weight" DataFormatString="{0:#,##0.0000}"
                                                                HeaderText="ปริมาณ/น้ำหนักสุทธิ (กก.)" SortExpression="net_weight" UniqueName="net_weight">
                                                                <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Right" />
                                                                <FooterStyle HorizontalAlign="Right" />
                                                            </telerik:GridNumericColumn>
                                                            <telerik:GridNumericColumn Aggregate="Sum" DataField="Fob_amt" DataFormatString="{0:#,##0.0000}"
                                                                HeaderText="มูลค่า US$ (FOB)" SortExpression="Fob_amt" UniqueName="Fob_amt_baht">
                                                                <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Right" />
                                                                <FooterStyle HorizontalAlign="Right" />
                                                            </telerik:GridNumericColumn>
                                                            <telerik:GridBoundColumn DataField="WeightDisplayHeader" HeaderText="ประเภทของน้ำหนักรวม"
                                                                SortExpression="WeightDisplayHeader" UniqueName="WeightDisplayHeader">
                                                                <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                            </telerik:GridBoundColumn>--%>
                                                        </Columns>
                                                    </MasterTableView>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ClientSettings>
                                                        <Selecting AllowRowSelect="True" />
                                                    </ClientSettings>
                                                </telerik:RadGrid></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 105px" class="FormLabel">
                                                </td>
                                            <td class="FormLabel" colspan="2">
                                                </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 105px" class="FormLabel" valign="top">
                                                </td>
                                            <td class="FormLabel" colspan="2">
                                                </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 105px" class="FormLabel" valign="top">
                                                </td>
                                            <td class="FormLabel" colspan="2">
                                                </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 105px" valign="top">
                                            </td>
                                            <td colspan="2">
                                                <asp:Label ID="lbl_ErrMSG" runat="server" Font-Names="Tahoma" Font-Size="10pt" ForeColor="Red"></asp:Label></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td width="15">
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td width="15">
                                </td>
                                <td>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
