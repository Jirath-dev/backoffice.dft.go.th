<%@ Page Language="vb" AutoEventWireup="false" Codebehind="frmInvoice.aspx.vb" Inherits=".frmInvoice" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Invoice</title>
    <link href="css/skin.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>
        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="rgInvoiceList">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="rgInvoiceList" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>
        <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        </telerik:RadCodeBlock>
        <table border="0" cellpadding="0" cellspacing="5" width="100%">
            <tr>
                <td>
                    <table cellpadding="0" cellspacing="0" style="width: 100%">
                        <tr>
                            <td style="width: 100%">
                                <telerik:RadTabStrip ID="RadTabStrip1" runat="server" Font-Names="Tahoma" Font-Size="10pt"
                                    MultiPageID="RadMultiPage1" SelectedIndex="0" Skin="Office2007" Width="100%">
                                    <Tabs>
                                        <telerik:RadTab runat="server" Font-Names="Tahoma" Font-Size="10pt" Selected="True" Text="ข้อมูล Invoice">
                                        </telerik:RadTab>
                                        <telerik:RadTab runat="server" Font-Names="Tahoma" Font-Size="10pt" Text="ข้อมูลต้นทุน">
                                        </telerik:RadTab>
                                    </Tabs>
                                </telerik:RadTabStrip>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100%">
                                <telerik:RadMultiPage ID="RadMultiPage1" runat="server" TabIndex="-1" Width="100%"
                                    SelectedIndex="0">
                                    <telerik:RadPageView ID="RadPageView1" runat="server" Selected="True" TabIndex="-1"
                                        Width="100%">
                                        <table border="0" cellpadding="1" cellspacing="0" class="m-frm-bdr" width="100%">
                                            <tr>
                                                <td>
                                                    <table style="width: 100%">
                                                        <tr>
                                                            <td style="width: 100%">
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
                                                                                                                <font class="groupbox">&nbsp; รายการ Invoice &nbsp;</font></td>
                                                                                                            <telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">
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
                                                                                                            <td>
                                                                                                                <table>
                                                                                                                    <tr>
                                                                                                                        <td>
                                                                                                                            <font class="FormLabel">เลขที่ Invoice :</font>
                                                                                                                            <asp:TextBox ID="txtInvoice_No" runat="server" Width="200px"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <font class="FormLabel">&nbsp;ปี : </font>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <telerik:RadComboBox ID="ddlYear" runat="server" Filter="StartsWith"
                                                                                                                                Font-Names="Tahoma" Font-Size="10pt" MarkFirstMatch="True" Skin="Web20" TabIndex="3"
                                                                                                                                Width="100px">
                                                                                                                            </telerik:RadComboBox>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            &nbsp;<asp:Button ID="btnSave" runat="server" Text="ยืนยัน"
                                                                                                                                Width="150px" ValidationGroup="Invoice" />
                                                                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtInvoice_No"
                                                                                                                                Display="Dynamic" ErrorMessage="กรุณาป้อนเลขที่ Invoice" Font-Names="Tahoma"
                                                                                                                                Font-Size="10pt" SetFocusOnError="True" ValidationGroup="Invoice"></asp:RequiredFieldValidator>
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
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <telerik:RadGrid ID="rgInvoiceList" runat="server" AllowPaging="True" AllowSorting="True"
                                                        GridLines="None" ShowStatusBar="True" Skin="Office2007" TabIndex="-1" Width="100%">
                                                        <MasterTableView AutoGenerateColumns="False" CellSpacing="-1" NoMasterRecordsText="ไม่มีรายการ Invoice"
                                                            Width="100%" DataKeyNames="edi_year,invoice_no">
                                                            <Columns>
                                                                <telerik:GridTemplateColumn UniqueName="DeleteTemplateColumn" HeaderText="ลบ">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="imbDelete" ImageUrl="~/images/delete.gif" CommandName="ImageDelete" runat="server" />
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridBoundColumn DataField="invoice_no" HeaderText="เลขที่ Invoice" SortExpression="invoice_no"
                                                                    UniqueName="invoice_no">
                                                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn DataField="edi_year" HeaderText="ปี" ReadOnly="True"
                                                                    SortExpression="edi_year" UniqueName="edi_year">
                                                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn DataField="active_flag" HeaderText="สถานะ" ReadOnly="True"
                                                                    SortExpression="active_flag" UniqueName="active_flag">
                                                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn DataField="cancel_by" HeaderText="ผู้ยกเลิก" ReadOnly="True"
                                                                    SortExpression="cancel_by" UniqueName="cancel_by">
                                                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                                                </telerik:GridBoundColumn>
                                                            </Columns>
                                                        </MasterTableView>
                                                        <HeaderStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                        <ClientSettings EnableRowHoverStyle="True">
                                                            <Selecting AllowRowSelect="True" />
                                                        </ClientSettings>
                                                    </telerik:RadGrid></td>
                                            </tr>
                                        </table>
                                    </telerik:RadPageView>
                                    <telerik:RadPageView ID="RadPageView2" runat="server" TabIndex="-1" Width="100%">
                                        <table border="0" cellpadding="1" cellspacing="0" class="m-frm-bdr" width="100%">
                                            <tr>
                                                <td class="FormLabel">
                                                    <table style="width: 100%">
                                                        <tr>
                                                            <td style="width: 100%">
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
                                                                                                                <font class="groupbox">&nbsp; ตรวจสอบต้นทุน &nbsp;</font></td>
                                                                                                            <telerik:RadCodeBlock ID="RadCodeBlock3" runat="server">
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
                                                                                                        <table style="width: 100%" cellpadding="3" cellspacing="3">
                                                                                                            <tr>
                                                                                                                <td valign="top">
                                                                                                                    พิกัดสินค้า :
                                                                                                                        <asp:TextBox ID="txtTariff_Code" runat="server" Width="195px" MaxLength="15"></asp:TextBox>&nbsp;<asp:ImageButton
                                                                                                                        ID="imbSearch" runat="server" ImageUrl="~/images/view.gif" /></td>
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
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="FormLabel">
                                                    <table style="width: 100%" id="tblData" runat="server">
                                                        <tr>
                                                            <td style="width: 100%">
                                                                <telerik:RadGrid ID="rgCerOfOrigin" runat="server" AllowPaging="True" AllowSorting="True"
                                                                    GridLines="None" ShowStatusBar="True" Skin="Office2007" TabIndex="-1" Width="100%"
                                                                    ShowFooter="True">
                                                                    <MasterTableView AutoGenerateColumns="False" CellSpacing="-1" NoMasterRecordsText="ไม่พบรายการข้อมูลต้นทุน"
                                                                        Width="100%">
                                                                        <Columns>
                                                                            <telerik:GridBoundColumn DataField="harmonized_no" HeaderText="พิกัด" SortExpression="harmonized_no"
                                                                                UniqueName="harmonized_no">
                                                                                <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                            </telerik:GridBoundColumn>
                                                                            <telerik:GridBoundColumn DataField="country" HeaderText="ประเทศ"
                                                                                ReadOnly="True" SortExpression="country" UniqueName="country">
                                                                                <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                                                            </telerik:GridBoundColumn>
                                                                            <telerik:GridBoundColumn DataField="xcompany_name" HeaderText="รุ่น" ReadOnly="True"
                                                                                SortExpression="company_name" UniqueName="company_name">
                                                                                <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                                                            </telerik:GridBoundColumn>
                                                                            <telerik:GridBoundColumn DataField="company_name_en" HeaderText="รายละเอียดสินค้า (อังกฤษ)" ReadOnly="True"
                                                                                SortExpression="company_name_en" UniqueName="company_name_en">
                                                                                <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                                                            </telerik:GridBoundColumn>
                                                                            <telerik:GridBoundColumn DataField="company_name_th" HeaderText="รายละเอียดสินค้า (ไทย)" ReadOnly="True"
                                                                                SortExpression="company_name_th" UniqueName="company_name_th">
                                                                                <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                            </telerik:GridBoundColumn>
                                                                            <telerik:GridBoundColumn DataField="certoforigin_date" DataFormatString="{0:dd/MM/yyyy}"
                                                                                HeaderText="วันที่หนังสือรับรอง" SortExpression="certoforigin_date" UniqueName="certoforigin_date">
                                                                                <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                            </telerik:GridBoundColumn>
                                                                            <telerik:GridBoundColumn DataField="certoforigin_no" HeaderText="หมายเลขหนังสือรับรอง"
                                                                                ReadOnly="True" SortExpression="certoforigin_no" UniqueName="certoforigin_no">
                                                                                <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                            </telerik:GridBoundColumn>
                                                                            <telerik:GridBoundColumn DataField="country_code" HeaderText="รหัสประเทศ"
                                                                                ReadOnly="True" SortExpression="country_code" UniqueName="country_code">
                                                                                <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                            </telerik:GridBoundColumn>
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
                                    </telerik:RadPageView>
                                </telerik:RadMultiPage>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <asp:Label ID="lblINVH_RUN_AUTO" runat="server" Visible="False"></asp:Label>
        <asp:Label ID="lblCompany_Taxno" runat="server" Visible="False"></asp:Label>
        <asp:Label ID="lblSite_ID" runat="server" Visible="False"></asp:Label>
        <asp:Label ID="lblCOUNTRY_CODE" runat="server" Visible="False"></asp:Label>
        <asp:Label ID="lblFormType" runat="server" Visible="False"></asp:Label>
    </form>
</body>
</html>
