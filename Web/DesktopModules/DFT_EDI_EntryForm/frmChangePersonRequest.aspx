<%@ Page Language="vb" AutoEventWireup="false" Codebehind="frmChangePersonRequest.aspx.vb"
    Inherits=".frmChangePersonRequest" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ค้นหาบัตรผู้รับมอบ</title>
    <link href="css/skin.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>
        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        </telerik:RadAjaxManager>
        <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
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
                
                function returnToParent(sCard_ID){
                    var oArg = new Object();
                    
                    oArg.Card_ID = sCard_ID;
            
                    var oWnd = GetRadWindow();
                    oWnd.close(oArg);
                }
            </script>
        </telerik:RadCodeBlock>
        <table width="100%" border="0" cellpadding="0" cellspacing="5">
            <tr>
                <td>
                    <table border="0" cellpadding="1" cellspacing="0" class="m-frm-bdr" width="100%">
                        <tr>
                            <td>
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td class="m-frm" valign="top" width="100%">
                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                <tr>
                                                    <td width="15">
                                                        &nbsp;</td>
                                                    <td>
                                                        <table border="0" cellpadding="0" cellspacing="2" width="100%">
                                                            <tr>
                                                                <td>
                                                                    <font class="FormLabel">เลขประจำตัวผู้เสียภาษี :</font>
                                                                <asp:Label
                                                                        ID="lblCompany_Taxno" runat="server" Font-Names="Tahoma" Font-Size="14pt" ForeColor="#0000C0"></asp:Label></td>
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
                    <%--<div id="divForm" runat="server"></div>--%>
                    <table border="0" cellpadding="1" cellspacing="0" class="m-frm-bdr" width="100%">
                        <tr>
                            <td>
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td class="m-frm" valign="top" width="100%">
                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                <tr>
                                                    <td>
                                                        <table>
                                                            <tr>
                                                                <td colspan="4">
                                                                    <telerik:RadGrid ID="rgCardList" runat="server" AllowPaging="True" AllowSorting="True"
                                                                        GridLines="None" ShowStatusBar="True" Skin="Office2007" TabIndex="-1" Width="100%" PageSize="5">
                                                                        <MasterTableView AutoGenerateColumns="False" CellSpacing="-1" ClientDataKeyNames="card_id"
                                                                            DataKeyNames="card_id" NoMasterRecordsText="ไม่มีรายการใบเสร็จที่ผ่านการชำระแล้ว"
                                                                            Width="100%">
                                                                            <Columns>
                                                                                <telerik:GridTemplateColumn UniqueName="SelectTemplateColumn" HeaderText="เลือก">
                                                                                    <ItemTemplate>
                                                                                        <asp:HyperLink ID="SelectLink" runat="server" ImageUrl="~/images/action_import.gif"></asp:HyperLink>
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                                </telerik:GridTemplateColumn>
                                                                                <telerik:GridBoundColumn DataField="card_id" HeaderText="รหัสบัตร" ReadOnly="True"
                                                                                    SortExpression="card_id" UniqueName="card_id">
                                                                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                                </telerik:GridBoundColumn>
                                                                                <telerik:GridBoundColumn DataField="AuthName_Thai" HeaderText="ชื่อ - สกุล (ไทย)" SortExpression="AuthName_Thai"
                                                                                    UniqueName="AuthName_Thai">
                                                                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                                                                </telerik:GridBoundColumn>
                                                                                <telerik:GridBoundColumn DataField="AuthName" HeaderText="ชื่อ - สกุล (อังกฤษ)" SortExpression="AuthName"
                                                                                    UniqueName="AuthName">
                                                                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                                                                </telerik:GridBoundColumn>
                                                                                <telerik:GridBoundColumn DataField="active_flag" HeaderText="สถานะ" ReadOnly="True"
                                                                                    SortExpression="active_flag" UniqueName="active_flag">
                                                                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                                </telerik:GridBoundColumn>
                                                                                <telerik:GridBoundColumn DataField="expire_date" HeaderText="หมดอายุ" ReadOnly="True"
                                                                                    SortExpression="expire_date" UniqueName="expire_date" DataFormatString="{0:d}">
                                                                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                                </telerik:GridBoundColumn>
                                                                                 <telerik:GridBoundColumn DataField="card_level" HeaderText="เงื่อนไขการมอบอำนาจ" SortExpression="card_level"
                                                                                    UniqueName="card_level">
                                                                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                                                                </telerik:GridBoundColumn>
                                                                                <telerik:GridBoundColumn DataField="card_type" HeaderText="ประเภท" ReadOnly="True"
                                                                                    SortExpression="card_type" UniqueName="card_type">
                                                                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                                </telerik:GridBoundColumn>
                                                                            </Columns>
                                                                        </MasterTableView>
                                                                        <HeaderStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
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
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
