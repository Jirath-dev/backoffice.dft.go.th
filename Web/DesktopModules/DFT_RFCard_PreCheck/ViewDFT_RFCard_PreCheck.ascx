<%@ Control language="vb" Inherits="NTi.Modules.DFT_RFCard_PreCheck.ViewDFT_RFCard_PreCheck" AutoEventWireup="false" Explicit="True" Codebehind="ViewDFT_RFCard_PreCheck.ascx.vb" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="RadAjaxManager1">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="rgCompanyUserList" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="rgCompanyUserList">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="rgCompanyUserList" />
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
                                                            <font class="groupbox">&#160;&#160;ค้นหารายชื่อนิติบุคคล&#160;&#160;</font></td>
                                                        <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
                                                            <td style="background-image: url(<%= ResolveUrl("images") %>/CORNER.gif); background-repeat: no-repeat;">
                                                                &#160;&#160;&#160;&#160;</td>
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
                            <tr class="m-frm-hdr">
                                <td>
                                    <table style="width: 100%">
                                        <tr>
                                            <td style="width: 100%">
                                                <table cellpadding="0" cellspacing="0" dwcopytype="CopyTableColumn">
                                                    <tr>
                                                        <td>
                                                            <font class="FormLabel">เงื่อนไขการค้นหา :&nbsp;</font></td>
                                                        <td>
                                                            <asp:TextBox ID="txtCompanySearch" runat="server" Width="300px"></asp:TextBox></td>
                                                        <td>
                                                            &nbsp;<asp:Button ID="btnSearchCompany" runat="server" Width="150px"
                                                                Text="ค้นหา" /></td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td class="m-frm" valign="top" width="100%">
                                </td>
                            </tr>
                        </table>
                        <table cellpadding="2" cellspacing="2" style="width: 100%">
                            <tr>
                                <td style="width: 100%">
                                    <telerik:RadGrid ID="rgCompanyUserList" runat="server" AllowPaging="True" AllowSorting="True"
                                        CssClass="GRID-STYLE" GridLines="None" Skin="Office2007" Font-Names="Tahoma"
                                        Font-Size="X-Small" TabIndex="-1">
                                        <MasterTableView AutoGenerateColumns="False" CellSpacing="-1" Width="100%" DataKeyNames="Company_Taxno,company_BranchNo"
                                            ClientDataKeyNames="Company_Taxno,company_BranchNo" NoMasterRecordsText="ไม่พบรายการที่ทำการค้นหา">
                                            <Columns>
                                                <telerik:GridTemplateColumn UniqueName="TemplateViewColumn">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnSelect" ImageUrl="~/images/icon_survey_16px.gif" CommandName="Select"
                                                            runat="server" TabIndex="-1" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" Width="16px" />
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridBoundColumn DataField="Company_Taxno" HeaderText="หมายเลขประจำตัวผู้เสียภาษี"
                                                    ReadOnly="True" SortExpression="Company_Taxno" UniqueName="Company_Taxno">
                                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Company_Juristic" HeaderText="เลขทะเบียนนิติบุคคล"
                                                    ReadOnly="True" SortExpression="Company_Juristic" UniqueName="Company_Juristic">
                                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="company_BranchNo" HeaderText="สาขา" ReadOnly="True"
                                                    SortExpression="company_BranchNo" UniqueName="company_BranchNo">
                                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="CompanyName_Th" HeaderText="ชื่อนิติบุคคล (ไทย)"
                                                    SortExpression="CompanyName_Th" UniqueName="CompanyName_Th">
                                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="CompanyName_Eng" HeaderText="ชื่อนิติบุคคล (อังกฤษ)"
                                                    SortExpression="CompanyName_Eng" UniqueName="CompanyName_Eng">
                                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                                </telerik:GridBoundColumn>
                                            </Columns>
                                        </MasterTableView>
                                        <HeaderStyle HorizontalAlign="Center" Font-Names="Tahoma" Font-Size="10pt" Font-Underline="False" />
                                        <ClientSettings EnableRowHoverStyle="True">
                                            <Selecting AllowRowSelect="True" />
                                        </ClientSettings>
                                    </telerik:RadGrid>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>