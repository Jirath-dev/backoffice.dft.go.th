<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ViewErrors.aspx.vb" Inherits=".ViewErrors" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<link href="../Popup/skin.css" rel="stylesheet" type="text/css" />
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">

        <script type="text/javascript">
            function CloseAndRebind(args) {
                GetRadWindow().Close();
                GetRadWindow().BrowserWindow.refreshGrid(args);
            }

            function GetRadWindow() {
                var oWindow = null;
                if (window.radWindow) oWindow = window.radWindow; //Will work in Moz in all cases, including clasic dialog
                else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow; //IE (and Moz as well)

                return oWindow;
            }

            function CancelEdit() {
                GetRadWindow().Close();
                return false;
            }
        </script>

    </telerik:RadCodeBlock>
</head>
<body>
    <form id="form1" runat="server">
        <div style="display:none;">
            <asp:TextBox ID="txtInvHRunAuto" runat="server" Visible="False" />
            <asp:Label ID="lblHeader" class="FormLabel" runat="server" />
        </div>
       <div class="m-frm-bdr" style="margin: 5px; padding: 5px;">
            <div class="m-frm-hdr">
                <%--<table cellspacing="0" cellpadding="0" border="0" width="100%">
                    <tr class="m-frm-hdr">
                        <td width="7%" align="left" class="m-frm-hdr">
                            <table cols="2" border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td nowrap class="groupboxheader">
                                        <font class="groupbox">&#160;&#160;<asp:Label ID="lblHeader" class="FormLabel" runat="server" />&#160;&#160;</font></td>
                                    <td style="background-image: url(<%= ResolveUrl("../images") %>/groupbox/CORNER.gif); background-repeat: no-repeat;">&#160;&#160;&#160;&#160;</td>
                                </tr>
                            </table>
                        </td>
                        <td width="93%" align="left">
                            <asp:TextBox ID="txtInvHRunAuto" runat="server" Visible="False" /></td>
                    </tr>
                </table>--%>
                <div>
                    <asp:DataGrid ID="grdErrorsData" runat="server" Class="GRID-STYLE" Height="30px"
                        ShowFooter="True" Width="100%" AutoGenerateColumns="False">
                        <HeaderStyle CssClass="HEADER-STYLE"></HeaderStyle>
                        <FooterStyle CssClass="FOOTER-STYLE"></FooterStyle>
                        <Columns>
                            <%--<asp:BoundColumn DataField="checking_by_desc" HeaderText="ตรวจสอบโดย" HeaderStyle-ForeColor="white" />--%>
                            <asp:BoundColumn DataField="checking_date" HeaderText="วันที่" HeaderStyle-ForeColor="white" DataFormatString="{0:dd/MM/yyyy [HH:mm]}" />
                            <%--<asp:BoundColumn DataField="Cancel_By" HeaderText="ผู้ตรวจ" HeaderStyle-ForeColor="white" />--%>
                            <%--<asp:BoundColumn DataField="error_code" HeaderText="รหัส" HeaderStyle-ForeColor="white" />--%>
                            <asp:BoundColumn DataField="error_message" HeaderText="ผลการตรวจ" HeaderStyle-ForeColor="white" />
                            <asp:BoundColumn DataField="error_information" HeaderText="รายละเอียดการตรวจสอบ" HeaderStyle-ForeColor="white" />
                        </Columns>
                    </asp:DataGrid>
                    <div style="text-align:center;">
                        <asp:Label runat="server" ID="lblMessage"></asp:Label>
                    </div>
                </div>
                <div style="text-align: right; padding: 10px 0">
                    <input type="button" name="btnCancel" id="btnCancel" class="m-btn"
                        onclick="javascript: window.close();"
                        value="ปิดหน้าต่าง" />
                </div>
            </div>
        </div>
    </form>
</body>
</html>
