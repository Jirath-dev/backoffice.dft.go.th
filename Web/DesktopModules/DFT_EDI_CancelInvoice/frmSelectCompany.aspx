<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frmSelectCompany.aspx.vb" Inherits=".frmSelectCompany" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>ค้นหาบริษัท</title>
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
                
                function returnToParent(sCompany_Taxno,sCompanyName_Eng){
                    var oArg = new Object();
                    
                    oArg.Company_Taxno = sCompany_Taxno;
                    oArg.CompanyName_Eng = sCompanyName_Eng;
            
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
                                                                                            <font class="FormLabel">หมายเลขประจำตัวผู้เสียภาษีหรือชื่อนิติบุคคล (ไทย/อังกฤษ):</font>
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
                                                <MasterTableView AutoGenerateColumns="False" CellSpacing="-1" NoMasterRecordsText="ไม่มีรายการนิติบุคคลที่ทำการค้นหา"
                                                    Width="100%" DataKeyNames="Company_Taxno,CompanyName_Eng,CompanyName_Th">
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
                                                        <telerik:GridBoundColumn DataField="CompanyName_Eng" HeaderText="ชื่อนิติบุคคล (อังกฤษ)"
                                                            ReadOnly="True" SortExpression="CompanyName_Eng" UniqueName="CompanyName_Eng">
                                                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="CompanyName_Th" HeaderText="ชื่อนิติบุคคล (ไทย)"
                                                            ReadOnly="True" SortExpression="CompanyName_Th" UniqueName="CompanyName_Th">
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
