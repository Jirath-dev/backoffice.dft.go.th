<%@ Control Language="vb" Inherits="NTi.Modules.DFT_Tariff_ECO.ViewDFT_Tariff_ECO" AutoEventWireup="false" Explicit="True" Codebehind="ViewDFT_Tariff_ECO.ascx.vb" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="RadAjaxManager1">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="grdMasterData" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="dropForm">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="dropCountry" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="grdMasterData">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="grdMasterData" />
            </UpdatedControls>
        </telerik:AjaxSetting>
    </AjaxSettings>
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
                                            <td align="left" class="m-frm-hdr">
                                                <table cols="2" border="0" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td nowrap class="groupboxheader">
                                                            <font class="groupbox">&#160;&#160;<asp:Label ID="lblFormHeader" runat="server" />&#160;&#160;</font></td>
                                                        <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
                                                            <td style="background-image: url(<%= ResolveUrl("images") %>/CORNER.gif); background-repeat:no-repeat;">&#160;&#160;&#160;&#160;</td>
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
                                    <table cellspacing="0" cellpadding="2" width="100%" border="0" class="m-frm" cols="3">
                                        <tr>
                                            <td width="84%" class="m-err">
                                                <table width="100%" cellpadding="0" cellspacing="0" dwcopytype="CopyTableColumn">
                                                    <tr>
                                                        <td>
                                                            <font class="FormLabel">ฟอร์ม :&nbsp;</font></td>
                                                        <td>
                                                            <telerik:RadComboBox ID="dropForm" runat="server" DataSourceID="SqlForm" DataTextField="DESCRIPTION"
                                                                DataValueField="CODE" Skin="Office2007" Width="280px" AutoPostBack="True">
                                                            </telerik:RadComboBox>
                                                        </td>
                                                        <td>
                                                            <font class="FormLabel">&nbsp;ประเทศ :&nbsp;</font></td>
                                                        <td>
                                                            <telerik:RadComboBox ID="dropCountry" runat="server" DataTextField="DESCRIPTION"
                                                                DataValueField="CODE" Width="260px">
                                                            </telerik:RadComboBox>
                                                        </td>
                                                        <td>
                                                            <font class="FormLabel">&nbsp;พิกัดศุลกากร :&nbsp;</font>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtTariff" runat="server" MaxLength="12" size="30" Width="100px" />
                                                        </td>
                                                        <td align="left">
                                                            &nbsp;<asp:Button ID="btnSearch" Text="ค้นหา" Style="width: auto;" runat="server" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td class="m-frm" style="height: 19px" valign="top" width="100%">
                                </td>
                            </tr>
                            <tr>
                                <td class="m-frm" style="text-align: center" valign="top" width="100%">
                                    <asp:Label ID="lblMsg" runat="server" Font-Names="Tahoma" Font-Size="10pt" ForeColor="Red"></asp:Label></td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td>
            <telerik:RadGrid ID="grdMasterData" runat="server" AllowSorting="True" 
                GridLines="None" Skin="Office2007" PageSize="20" Font-Names="Tahoma" Font-Size="X-Small">
                <MasterTableView AutoGenerateColumns="False" CellSpacing="-1" PageSize="10" Width="100%" DataKeyNames="tariff_code" ClientDataKeyNames="tariff_code">
                    <Columns>
                        <telerik:GridBoundColumn DataField="tariff_code" HeaderText="พิกัดศุลกากร" ReadOnly="True"
                            SortExpression="tariff_code" UniqueName="tariff_code">
                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="tariff_name" HeaderText="รายละเอียดพิกัดศุลกากร" ReadOnly="True"
                            SortExpression="tariff_name" UniqueName="tariff_name">
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
<asp:SqlDataSource ID="SqlForm" runat="server" ConnectionString="<%$ ConnectionStrings:OriginConnection %>"
    SelectCommand="sp_common_get_formTypeForTariffSearching_NewDS_back" SelectCommandType="StoredProcedure">
</asp:SqlDataSource>