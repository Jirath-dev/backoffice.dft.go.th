<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frmSelectInvh.aspx.vb" Inherits=".frmSelectInvh" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>ค้นหาเลขที่อ้างอิง</title>
    <link href="CSS/skin.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>
                <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="RadAjaxManager1">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="rgCompanyList" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="rgCompanyList">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="rgCompanyList" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>
        <telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">
            <script type="text/javascript">
                function CloseAndRebind(args) {
                GetRadWindow().Close();
                GetRadWindow().BrowserWindow.refreshGrid(args);
                }

                function GetRadWindow() 
                {
                    var oWindow = null;
                    if (window.radWindow) oWindow = window.radWindow; //Will work in Moz in all cases, including clasic dialog
                    else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow; //IE (and Moz as well)

                    return oWindow;
                }

                function CancelEdit() {
                    GetRadWindow().Close();
                }
                
                function returnToParent(sCompany_Taxno,scompany_name,sInvoice_No1,sinvh_run_auto){
                    var oArg = new Object();

                    oArg.Company_Taxno = sCompany_Taxno;
                    oArg.CompanyName_Eng = scompany_name;
                    oArg.InvoiceNo = sInvoice_No1
                    oArg.invh_run_auto = sinvh_run_auto
            
                    var oWnd = GetRadWindow();
                    oWnd.close(oArg);
                }
            </script>
        </telerik:RadCodeBlock>
        <table border="0" cellpadding="0" cellspacing="5" width="100%">
            <tr>
                <td>
                    <table border="0" cellpadding="0" cellspacing="0" class="m-frm-bdr" width="100%">
                        <tr>
                            <td>
                                <table border="0" cellpadding="0" cellspacing="2" style="width: 100%">
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
                                                                                            <font class="groupbox">&nbsp; ค้นหาข้อมูล &nbsp;</font></td>
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
                                                                            <td width="15">
                                                                                &nbsp;</td>
                                                                            <td>
                                                                                <table border="0" cellpadding="0" cellspacing="2" width="100%">
                                                                                    <tr>
                                                                                        <td style="font-size: 12pt; color: #000000; font-family: Times New Roman">
                                                                                            <font class="FormLabel">เลขที่อ้างอิง:</font>
                                                                                            <asp:TextBox ID="txtCompany_Search" runat="server" Width="200px"></asp:TextBox>
                                                                                            <asp:Button ID="btnSearch" runat="server" Text="ค้นหา" ValidationGroup="search" Width="120px" />
                                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtCompany_Search"
                                                                                                Display="Dynamic" ErrorMessage="กรุณาป้อนข้อมูลในการค้นหา" Font-Names="Tahoma"
                                                                                                Font-Size="10pt" SetFocusOnError="True" ValidationGroup="search"></asp:RequiredFieldValidator></td>
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
                    <table id="tblCompanyList" runat="server" border="0" cellpadding="0" cellspacing="0"
                        class="m-frm-bdr" width="100%">
                        <tr>
                            <td>
                                <table style="width: 100%">
                                    <tr>
                                        <td style="width: 100%">
                                            <telerik:RadGrid ID="rgCompanyList" runat="server" AllowPaging="True" AllowSorting="True"
                                                GridLines="None" ShowStatusBar="True" Skin="Office2007" TabIndex="-1" Width="100%">
                                                <MasterTableView AutoGenerateColumns="False" CellSpacing="-1" NoMasterRecordsText="ไม่มีเลขที่อ้างอิงที่ทำการค้นหา"
                                                    Width="100%" DataKeyNames="Company_Taxno,company_name,invoice_no1,invh_run_auto">
                                                    <Columns>
                                                        <telerik:GridTemplateColumn UniqueName="SelectTemplateColumn">
                                                            <ItemTemplate>
                                                                <asp:HyperLink ID="SelectLink" runat="server" ImageUrl="~/images/checked.gif"></asp:HyperLink>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" Width="15px" />
                                                            <ItemStyle HorizontalAlign="Center" Width="15px" />
                                                        </telerik:GridTemplateColumn>
                                                      <telerik:GridBoundColumn DataField="Company_Taxno" HeaderText="เลขประจำตัวผู้เสียภาษี"
                                                            ReadOnly="True" SortExpression="Company_Taxno" UniqueName="Company_Taxno">
                                                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="company_name" HeaderText="ชื่อนิติบุคคล"
                                                            ReadOnly="True" SortExpression="company_name" UniqueName="company_name">
                                                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                                        </telerik:GridBoundColumn>
                                                           <telerik:GridBoundColumn DataField="invoice_no1" HeaderText="เลขที่ Invoice"
                                                            ReadOnly="True" SortExpression="invoice_no1" UniqueName="invoice_no1">
                                                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="invh_run_auto" HeaderText="เลขที่อ้างอิง"
                                                            ReadOnly="True" SortExpression="invh_run_auto" UniqueName="invh_run_auto">
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
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
