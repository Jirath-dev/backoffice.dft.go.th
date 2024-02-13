<%@ Control Language="vb" Inherits="YourCompany.Modules.DFT_EDI_Report_Chapter.ViewDFT_EDI_Report_Chapter"
    AutoEventWireup="false" Explicit="True" CodeBehind="ViewDFT_EDI_Report_Chapter.ascx.vb" %>
<%@ Register TagPrefix="dnn" TagName="Audit" Src="~/controls/ModuleAuditControl.ascx" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="btnSearch">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="rgShow" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="rgShow">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="rgShow" />
            </UpdatedControls>
        </telerik:AjaxSetting>
    </AjaxSettings>
</telerik:RadAjaxManager>
<table width="100%">
    <tr>
        <td>
            <table border="0" cellpadding="0" cellspacing="0" class="m-frm-bdr" width="100%">
                <tr class="m-frm-hdr">
                    <td align="left" class="m-frm-hdr">
                        <table border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td class="groupboxheader" nowrap="nowrap">
                                    <font class="groupbox">&nbsp; รายงานสรุปการออกหนังสือรับรอง (Group by Chapter) </font>
                                </td>
                                <telerik:RadCodeBlock ID="RadCodeBlock3" runat="server">
                                    <td style='background-image: url(<%= ResolveUrl("images") %>/CORNER.gif); background-repeat: no-repeat'>
                                        &nbsp; &nbsp;&nbsp;
                                    </td>
                                </telerik:RadCodeBlock>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr class="m-frm-hdr">
                    <td align="left" class="m-frm-hdr">
                        <table border="0" cellpadding="0" cellspacing="2">
                            <tr>
                                <td>
                                    <font class="FormLabel">&nbsp;&nbsp;Chapter :</font>
                                    <asp:TextBox ID="txtChapter" runat="server" MaxLength="2" Width="41px"></asp:TextBox>
                                    <font class="FormLabel">&nbsp;&nbsp;เดือน :</font>
                                    <asp:DropDownList ID="ddlMonthFrom" runat="server" Font-Names="Tahoma" 
                                        Font-Size="10pt">
                                        <asp:ListItem Value="1">มกราคม</asp:ListItem>
                                        <asp:ListItem Value="2">กุมภาพันธ์</asp:ListItem>
                                        <asp:ListItem Value="3">มีนาคม</asp:ListItem>
                                        <asp:ListItem Value="4">เมษายน</asp:ListItem>
                                        <asp:ListItem Value="5">พฤษภาคม</asp:ListItem>
                                        <asp:ListItem Value="6">มิถุนายน</asp:ListItem>
                                        <asp:ListItem Value="7">กรกฎาคม</asp:ListItem>
                                        <asp:ListItem Value="8">สิงหาคม</asp:ListItem>
                                        <asp:ListItem Value="9">กันยายน</asp:ListItem>
                                        <asp:ListItem Value="10">ตุลาคม</asp:ListItem>
                                        <asp:ListItem Value="11">พฤษจิกายน</asp:ListItem>
                                        <asp:ListItem Value="12">ธันวาคม</asp:ListItem>
                                    </asp:DropDownList>
                                    <font class="FormLabel">&nbsp;&nbsp;ถึงเดือน :</font>
                                     <asp:DropDownList ID="ddlMonthTo" runat="server" Font-Names="Tahoma" 
                                        Font-Size="10pt">
                                        <asp:ListItem Value="1">มกราคม</asp:ListItem>
                                        <asp:ListItem Value="2">กุมภาพันธ์</asp:ListItem>
                                        <asp:ListItem Value="3">มีนาคม</asp:ListItem>
                                        <asp:ListItem Value="4">เมษายน</asp:ListItem>
                                        <asp:ListItem Value="5">พฤษภาคม</asp:ListItem>
                                        <asp:ListItem Value="6">มิถุนายน</asp:ListItem>
                                        <asp:ListItem Value="7">กรกฎาคม</asp:ListItem>
                                        <asp:ListItem Value="8">สิงหาคม</asp:ListItem>
                                        <asp:ListItem Value="9">กันยายน</asp:ListItem>
                                        <asp:ListItem Value="10">ตุลาคม</asp:ListItem>
                                        <asp:ListItem Value="11">พฤษจิกายน</asp:ListItem>
                                        <asp:ListItem Value="12">ธันวาคม</asp:ListItem>
                                    </asp:DropDownList>
                                    <font class="FormLabel">&nbsp;&nbsp;ปี (ค.ศ.) :</font>
                                    <asp:TextBox ID="txtYear" runat="server" MaxLength="4" Width="75px"></asp:TextBox>
                                    &nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="btnSearch" runat="server" Text="ค้นหา" Width="150px" UseSubmitBehavior="False"
                                        Font-Names="Tahoma" Font-Size="10pt" />
                                    &nbsp;
                                    <asp:Button ID="btnPrint" runat="server" Text="พิมพ์รายงาน (Excel)" Width="150px"
                                        UseSubmitBehavior="False" Font-Names="Tahoma" Font-Size="10pt" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td align="right" class="m-frm-nav">
                    </td>
                </tr>
                <tr class="m-frm-hdr">
                    <td align="left">
                        <table border="0" cellpadding="0" cellspacing="2" width="100%">
                            <tr>
                                <td>
                                    <telerik:RadGrid ID="rgShow" runat="server" CssClass="GRID-STYLE" GridLines="None"
                                        Skin="Office2007" Font-Names="Tahoma" Font-Size="X-Small" TabIndex="-1" ShowFooter="True"
                                        AutoGenerateColumns="False">
                                        <MasterTableView ClientDataKeyNames="CHAPTER" DataKeyNames="CHAPTER" NoMasterRecordsText="ไม่พบข้อมูล Chapter">
                                            <Columns>
                                                <telerik:GridBoundColumn DataField="CHAPTER" HeaderText="CHAPTER." ReadOnly="True"
                                                    SortExpression="CHAPTER" UniqueName="CHAPTER">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="YEAR" HeaderText="ปี" ReadOnly="True" SortExpression="YEAR"
                                                    UniqueName="YEAR">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="MONTH" HeaderText="เดือน" ReadOnly="True" SortExpression="MONTH"
                                                    UniqueName="MONTH">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="FORM_NAME" HeaderText="ชื่อฟอร์ม" ReadOnly="True"
                                                    SortExpression="FORM_NAME" UniqueName="FORM_NAME">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Left" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridNumericColumn DataField="FORM_COUNT" HeaderText="จำนวน (ฉบับ)" ReadOnly="True"
                                                    SortExpression="FORM_COUNT" UniqueName="FORM_COUNT" Aggregate="Sum" FooterText="รวมทั้งสิ้น : "
                                                    DataFormatString="{0:N0}">
                                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Right" />
                                                    <FooterStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Right" />
                                                </telerik:GridNumericColumn>
                                                <telerik:GridNumericColumn DataField="FOB" HeaderText="มูลค่า (USD)" ReadOnly="True"
                                                    SortExpression="FOB" UniqueName="FOB" Aggregate="Sum" FooterText="รวมทั้งสิ้น : "
                                                    DataFormatString="{0:N4}">
                                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Right" />
                                                    <FooterStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Right" />
                                                </telerik:GridNumericColumn>
                                                <telerik:GridBoundColumn DataField="COUNTRY" HeaderText="ประเทศ" ReadOnly="True"
                                                    SortExpression="COUNTRY" UniqueName="COUNTRY">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
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
