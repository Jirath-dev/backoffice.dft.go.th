<%@ Page Language="vb" AutoEventWireup="false" Codebehind="frmChangeForm.aspx.vb"
    Inherits=".frmChangeForm" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>เปลี่ยนฟอร์ม</title>
    <link href="css/skin.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>
        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="rgFormList">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="rgFormList" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
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
            </script>
        </telerik:RadCodeBlock>
        <table width="100%" border="0" cellpadding="0" cellspacing="5">
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
                                                                    <telerik:RadGrid ID="rgFormList" runat="server"
                                                                        GridLines="None" ShowStatusBar="True" Skin="Office2007" TabIndex="-1" Width="100%" PageSize="9" AllowPaging="True">
                                                                        <MasterTableView AutoGenerateColumns="False" CellSpacing="-1" ClientDataKeyNames="form_type"
                                                                            DataKeyNames="form_type" NoMasterRecordsText="ไม่มีรายการใบเสร็จที่ผ่านการชำระแล้ว"
                                                                            Width="100%">
                                                                            <Columns>
                                                                                <telerik:GridTemplateColumn UniqueName="SelectTemplateColumn" HeaderText="เลือก">
                                                                                    <ItemTemplate>
                                                                                        <asp:ImageButton ID="SelectLink" CommandName="selected" ImageUrl="~/images/action_import.gif" runat="server" />
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                                    <ItemStyle HorizontalAlign="Center" Width="20px" />
                                                                                </telerik:GridTemplateColumn>
                                                                                <telerik:GridBoundColumn DataField="form_name" HeaderText="ชื่อฟอร์ม" ReadOnly="True"
                                                                                    SortExpression="form_name" UniqueName="form_name">
                                                                                    <HeaderStyle HorizontalAlign="Center" Width="750px" />
                                                                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt" Width="750px"/>
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
        <asp:Label ID="lblSiteName" runat="server" Visible="False"></asp:Label><asp:Label
            ID="lblInvh_Run_Auto" runat="server" Visible="False"></asp:Label>
    </form>
</body>
</html>
