<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frmCancelForm.aspx.vb" Inherits=".frmCancelForm" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>บันทึกเหตุผลที่ไม่ผ่านการตรวจสอบ</title>
    <link href="CSS/skin.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        #RadTextBox1_wrapper {
            display: none;
        }
    </style>
    <script type="text/javascript">
        if (typeof window.event == 'undefined') {
            document.onkeypress = function (e) {
                var test_var = e.target.nodeName.toUpperCase();
                if (e.target.type) var test_type = e.target.type.toUpperCase();
                if ((test_var == 'INPUT' && test_type == 'TEXT') || test_var == 'TEXTAREA') {
                    return e.keyCode;
                } else if (e.keyCode == 8) {
                    e.preventDefault();
                }
            }
        } else {
            document.onkeydown = function () {
                var test_var = event.srcElement.tagName.toUpperCase();
                if (event.srcElement.type) var test_type = event.srcElement.type.toUpperCase();
                if ((test_var == 'INPUT' && test_type == 'TEXT') || test_var == 'TEXTAREA') {
                    return event.keyCode;
                } else if (event.keyCode == 8) {
                    event.returnValue = false;
                }
            }
        }

        function CloseMyWin1() {
            try { window.parent.opener.refreshGrid(); } catch (e) { }
            window.parent.close();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>
        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        </telerik:RadAjaxManager>
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
                }

            </script>
        </telerik:RadCodeBlock>
        <table width="100%" border="0" cellpadding="0" cellspacing="5">
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
                                                    <td width="12%" align="left" class="m-frm-hdr">
                                                        <table cols="2" border="0" cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td nowrap class="groupboxheader">
                                                                    <font class="groupbox">&#160;&#160;ระบุเหตุผลการตรวจสอบ (กรณีไม่ผ่านการตรวจสอบเอกสารโดยเจ้าหน้าที่)&#160;&#160;</font></td>
                                                                <td style="background-image: url(<%= ResolveUrl("../images") %>/CORNER.gif); background-repeat: no-repeat;">&#160;&#160;&#160;&#160;</td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td width="88%" align="left">
                                                        <asp:Label ID="lbl_ErrMSG" runat="server" Font-Names="Tahoma" Font-Size="10pt" ForeColor="Red" />
                                                        <asp:TextBox ID="txtInvHRunAuto" runat="server" Visible="False" />
                                                        <asp:TextBox ID="txtInvDRunAuto" runat="server" Visible="False" /></td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="m-frm" valign="top">
                                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td width="15">&nbsp;</td>
                                                    <td>
                                                        <table border="0" cellpadding="0" cellspacing="3" width="100%">
                                                            <tr>
                                                                <td>
                                                                    <table border="0" cellpadding="1" cellspacing="0" class="m-frm-bdr" width="100%">
                                                                        <tr>
                                                                            <td>
                                                                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                                    <tr class="m-frm-hdr">
                                                                                        <td style="height: 57px">
                                                                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                                                <tr class="m-frm-hdr">
                                                                                                    <td align="left" class="m-frm-hdr" width="88%">
                                                                                                        <table border="0" cellpadding="0" cellspacing="0" cols="2">
                                                                                                            <tr>
                                                                                                                <td class="groupboxheader" nowrap="nowrap">
                                                                                                                    <font class="groupbox">&nbsp; รายละเอียดที่ต้องแก้ไข &nbsp;</font></td>
                                                                                                                <telerik:RadCodeBlock ID="RadCodeBlock10" runat="server">
                                                                                                                    <td style='background-image: url(<%= ResolveUrl("images") %>/CORNER.gif); background-repeat: no-repeat;'>&nbsp; &nbsp;&nbsp;</td>
                                                                                                                </telerik:RadCodeBlock>
                                                                                                            </tr>
                                                                                                        </table>
                                                                                                    </td>
                                                                                                    <td align="left" class="m-frm-nav" width="12%"></td>
                                                                                                </tr>
                                                                                            </table>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td class="m-frm" valign="top" width="100%">
                                                                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                                                <tr>
                                                                                                    <td width="15">&nbsp;</td>
                                                                                                    <td>
                                                                                                        <table border="0" cellpadding="0" cellspacing="2" width="100%">
                                                                                                            <tr>
                                                                                                                <td style="text-align: right">
                                                                                                                    <font class="FormLabel">ช่องที่ 1: </font></td>
                                                                                                                <td>
                                                                                                                    <telerik:RadTextBox ID="txtE01" runat="server" Width="450px">
                                                                                                                    </telerik:RadTextBox>
                                                                                                                    <asp:DropDownList ID="DropDownList1" runat="server" Font-Names="Tahoma" Font-Size="10pt">
                                                                                                                        <asp:ListItem>--- กรุณาระบุเหตุผล ---</asp:ListItem>
                                                                                                                        <asp:ListItem>เหตุผลไม่ผ่านการตรวจสอบ 1</asp:ListItem>
                                                                                                                        <asp:ListItem>เหตุผลไม่ผ่านการตรวจสอบ 2</asp:ListItem>
                                                                                                                        <asp:ListItem>เหตุผลไม่ผ่านการตรวจสอบ 3</asp:ListItem>
                                                                                                                        <asp:ListItem>เหตุผลไม่ผ่านการตรวจสอบ 4</asp:ListItem>
                                                                                                                        <asp:ListItem>เหตุผลไม่ผ่านการตรวจสอบ 5</asp:ListItem>
                                                                                                                    </asp:DropDownList></td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td style="text-align: right">
                                                                                                                    <font class="FormLabel">ช่องที่ 2: </font></td>
                                                                                                                <td>
                                                                                                                    <telerik:RadTextBox ID="txtE02" runat="server" Width="450px">
                                                                                                                    </telerik:RadTextBox>
                                                                                                                    <asp:DropDownList ID="DropDownList2" runat="server" Font-Names="Tahoma" Font-Size="10pt">
                                                                                                                        <asp:ListItem>--- กรุณาระบุเหตุผล ---</asp:ListItem>
                                                                                                                        <asp:ListItem>เหตุผลไม่ผ่านการตรวจสอบ 1</asp:ListItem>
                                                                                                                        <asp:ListItem>เหตุผลไม่ผ่านการตรวจสอบ 2</asp:ListItem>
                                                                                                                        <asp:ListItem>เหตุผลไม่ผ่านการตรวจสอบ 3</asp:ListItem>
                                                                                                                        <asp:ListItem>เหตุผลไม่ผ่านการตรวจสอบ 4</asp:ListItem>
                                                                                                                        <asp:ListItem>เหตุผลไม่ผ่านการตรวจสอบ 5</asp:ListItem>
                                                                                                                    </asp:DropDownList></td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td style="text-align: right">
                                                                                                                    <font class="FormLabel">ช่องที่ 3: </font></td>
                                                                                                                <td>
                                                                                                                    <telerik:RadTextBox ID="txtE03" runat="server" Width="450px">
                                                                                                                    </telerik:RadTextBox>
                                                                                                                    <asp:DropDownList ID="DropDownList3" runat="server" Font-Names="Tahoma" Font-Size="10pt">
                                                                                                                        <asp:ListItem>--- กรุณาระบุเหตุผล ---</asp:ListItem>
                                                                                                                        <asp:ListItem>เหตุผลไม่ผ่านการตรวจสอบ 1</asp:ListItem>
                                                                                                                        <asp:ListItem>เหตุผลไม่ผ่านการตรวจสอบ 2</asp:ListItem>
                                                                                                                        <asp:ListItem>เหตุผลไม่ผ่านการตรวจสอบ 3</asp:ListItem>
                                                                                                                        <asp:ListItem>เหตุผลไม่ผ่านการตรวจสอบ 4</asp:ListItem>
                                                                                                                        <asp:ListItem>เหตุผลไม่ผ่านการตรวจสอบ 5</asp:ListItem>
                                                                                                                    </asp:DropDownList></td>
                                                                                                            </tr>
                                                                                                            <tr style="display: none">
                                                                                                                <td style="text-align: right; display: none">
                                                                                                                    <font class="FormLabel">ช่องที่ 4: </font></td>
                                                                                                                <td style="display: none">
                                                                                                                    <telerik:RadTextBox ID="txtE04" runat="server" Width="450px" Visible="False">
                                                                                                                    </telerik:RadTextBox>
                                                                                                                    <asp:DropDownList ID="DropDownList4" runat="server" Font-Names="Tahoma" Font-Size="10pt" Visible="False">
                                                                                                                        <asp:ListItem>--- กรุณาระบุเหตุผล ---</asp:ListItem>
                                                                                                                        <asp:ListItem>เหตุผลไม่ผ่านการตรวจสอบ 1</asp:ListItem>
                                                                                                                        <asp:ListItem>เหตุผลไม่ผ่านการตรวจสอบ 2</asp:ListItem>
                                                                                                                        <asp:ListItem>เหตุผลไม่ผ่านการตรวจสอบ 3</asp:ListItem>
                                                                                                                        <asp:ListItem>เหตุผลไม่ผ่านการตรวจสอบ 4</asp:ListItem>
                                                                                                                        <asp:ListItem>เหตุผลไม่ผ่านการตรวจสอบ 5</asp:ListItem>
                                                                                                                    </asp:DropDownList></td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td style="text-align: right">
                                                                                                                    <font class="FormLabel">ช่องที่ 5: </font></td>
                                                                                                                <td>
                                                                                                                    <telerik:RadTextBox ID="txtE05" runat="server" Width="450px">
                                                                                                                    </telerik:RadTextBox>
                                                                                                                    <asp:DropDownList ID="DropDownList5" runat="server" Font-Names="Tahoma" Font-Size="10pt">
                                                                                                                        <asp:ListItem>--- กรุณาระบุเหตุผล ---</asp:ListItem>
                                                                                                                        <asp:ListItem>เหตุผลไม่ผ่านการตรวจสอบ 1</asp:ListItem>
                                                                                                                        <asp:ListItem>เหตุผลไม่ผ่านการตรวจสอบ 2</asp:ListItem>
                                                                                                                        <asp:ListItem>เหตุผลไม่ผ่านการตรวจสอบ 3</asp:ListItem>
                                                                                                                        <asp:ListItem>เหตุผลไม่ผ่านการตรวจสอบ 4</asp:ListItem>
                                                                                                                        <asp:ListItem>เหตุผลไม่ผ่านการตรวจสอบ 5</asp:ListItem>
                                                                                                                    </asp:DropDownList></td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td style="text-align: right">
                                                                                                                    <font class="FormLabel">ช่องที่ 6: </font></td>
                                                                                                                <td>
                                                                                                                    <telerik:RadTextBox ID="txtE06" runat="server" Width="450px">
                                                                                                                    </telerik:RadTextBox>
                                                                                                                    <asp:DropDownList ID="DropDownList6" runat="server" Font-Names="Tahoma" Font-Size="10pt">
                                                                                                                        <asp:ListItem>--- กรุณาระบุเหตุผล ---</asp:ListItem>
                                                                                                                        <asp:ListItem>เหตุผลไม่ผ่านการตรวจสอบ 1</asp:ListItem>
                                                                                                                        <asp:ListItem>เหตุผลไม่ผ่านการตรวจสอบ 2</asp:ListItem>
                                                                                                                        <asp:ListItem>เหตุผลไม่ผ่านการตรวจสอบ 3</asp:ListItem>
                                                                                                                        <asp:ListItem>เหตุผลไม่ผ่านการตรวจสอบ 4</asp:ListItem>
                                                                                                                        <asp:ListItem>เหตุผลไม่ผ่านการตรวจสอบ 5</asp:ListItem>
                                                                                                                    </asp:DropDownList></td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td style="text-align: right">
                                                                                                                    <font class="FormLabel">ช่องที่ 7: </font></td>
                                                                                                                <td>
                                                                                                                    <telerik:RadTextBox ID="txtE07" runat="server" Width="450px">
                                                                                                                    </telerik:RadTextBox>
                                                                                                                    <asp:DropDownList ID="DropDownList7" runat="server" Font-Names="Tahoma" Font-Size="10pt">
                                                                                                                        <asp:ListItem>--- กรุณาระบุเหตุผล ---</asp:ListItem>
                                                                                                                        <asp:ListItem>เหตุผลไม่ผ่านการตรวจสอบ 1</asp:ListItem>
                                                                                                                        <asp:ListItem>เหตุผลไม่ผ่านการตรวจสอบ 2</asp:ListItem>
                                                                                                                        <asp:ListItem>เหตุผลไม่ผ่านการตรวจสอบ 3</asp:ListItem>
                                                                                                                        <asp:ListItem>เหตุผลไม่ผ่านการตรวจสอบ 4</asp:ListItem>
                                                                                                                        <asp:ListItem>เหตุผลไม่ผ่านการตรวจสอบ 5</asp:ListItem>
                                                                                                                    </asp:DropDownList></td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td style="text-align: right">
                                                                                                                    <font class="FormLabel">ช่องที่ 8: </font></td>
                                                                                                                <td>
                                                                                                                    <telerik:RadTextBox ID="txtE08" runat="server" Width="450px">
                                                                                                                    </telerik:RadTextBox>
                                                                                                                    <asp:DropDownList ID="DropDownList8" runat="server" Font-Names="Tahoma" Font-Size="10pt">
                                                                                                                        <asp:ListItem>--- กรุณาระบุเหตุผล ---</asp:ListItem>
                                                                                                                        <asp:ListItem>เหตุผลไม่ผ่านการตรวจสอบ 1</asp:ListItem>
                                                                                                                        <asp:ListItem>เหตุผลไม่ผ่านการตรวจสอบ 2</asp:ListItem>
                                                                                                                        <asp:ListItem>เหตุผลไม่ผ่านการตรวจสอบ 3</asp:ListItem>
                                                                                                                        <asp:ListItem>เหตุผลไม่ผ่านการตรวจสอบ 4</asp:ListItem>
                                                                                                                        <asp:ListItem>เหตุผลไม่ผ่านการตรวจสอบ 5</asp:ListItem>
                                                                                                                    </asp:DropDownList></td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td style="text-align: right">
                                                                                                                    <font class="FormLabel">ช่องที่ 9: </font></td>
                                                                                                                <td>
                                                                                                                    <telerik:RadTextBox ID="txtE09" runat="server" Width="450px">
                                                                                                                    </telerik:RadTextBox>
                                                                                                                    <asp:DropDownList ID="DropDownList9" runat="server" Font-Names="Tahoma" Font-Size="10pt">
                                                                                                                        <asp:ListItem>--- กรุณาระบุเหตุผล ---</asp:ListItem>
                                                                                                                        <asp:ListItem>เหตุผลไม่ผ่านการตรวจสอบ 1</asp:ListItem>
                                                                                                                        <asp:ListItem>เหตุผลไม่ผ่านการตรวจสอบ 2</asp:ListItem>
                                                                                                                        <asp:ListItem>เหตุผลไม่ผ่านการตรวจสอบ 3</asp:ListItem>
                                                                                                                        <asp:ListItem>เหตุผลไม่ผ่านการตรวจสอบ 4</asp:ListItem>
                                                                                                                        <asp:ListItem>เหตุผลไม่ผ่านการตรวจสอบ 5</asp:ListItem>
                                                                                                                    </asp:DropDownList></td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td style="text-align: right">
                                                                                                                    <font class="FormLabel">ช่องที่ 10: </font></td>
                                                                                                                <td>
                                                                                                                    <telerik:RadTextBox ID="txtE10" runat="server" Width="450px">
                                                                                                                    </telerik:RadTextBox>
                                                                                                                    <asp:DropDownList ID="DropDownList10" runat="server" Font-Names="Tahoma" Font-Size="10pt">
                                                                                                                        <asp:ListItem>--- กรุณาระบุเหตุผล ---</asp:ListItem>
                                                                                                                        <asp:ListItem>เหตุผลไม่ผ่านการตรวจสอบ 1</asp:ListItem>
                                                                                                                        <asp:ListItem>เหตุผลไม่ผ่านการตรวจสอบ 2</asp:ListItem>
                                                                                                                        <asp:ListItem>เหตุผลไม่ผ่านการตรวจสอบ 3</asp:ListItem>
                                                                                                                        <asp:ListItem>เหตุผลไม่ผ่านการตรวจสอบ 4</asp:ListItem>
                                                                                                                        <asp:ListItem>เหตุผลไม่ผ่านการตรวจสอบ 5</asp:ListItem>
                                                                                                                    </asp:DropDownList></td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td style="text-align: right">
                                                                                                                    <font class="FormLabel">ช่องที่ 11: </font></td>
                                                                                                                <td>
                                                                                                                    <telerik:RadTextBox ID="txtE11" runat="server" Width="450px">
                                                                                                                    </telerik:RadTextBox>
                                                                                                                    <asp:DropDownList ID="DropDownList11" runat="server" Font-Names="Tahoma" Font-Size="10pt">
                                                                                                                        <asp:ListItem>--- กรุณาระบุเหตุผล ---</asp:ListItem>
                                                                                                                        <asp:ListItem>เหตุผลไม่ผ่านการตรวจสอบ 1</asp:ListItem>
                                                                                                                        <asp:ListItem>เหตุผลไม่ผ่านการตรวจสอบ 2</asp:ListItem>
                                                                                                                        <asp:ListItem>เหตุผลไม่ผ่านการตรวจสอบ 3</asp:ListItem>
                                                                                                                        <asp:ListItem>เหตุผลไม่ผ่านการตรวจสอบ 4</asp:ListItem>
                                                                                                                        <asp:ListItem>เหตุผลไม่ผ่านการตรวจสอบ 5</asp:ListItem>
                                                                                                                    </asp:DropDownList></td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td style="text-align: right">
                                                                                                                    <font class="FormLabel">ช่องที่ 12: </font></td>
                                                                                                                <td>
                                                                                                                    <telerik:RadTextBox ID="txtE12" runat="server" Width="450px">
                                                                                                                    </telerik:RadTextBox>
                                                                                                                    <asp:DropDownList ID="DropDownList12" runat="server" Font-Names="Tahoma" Font-Size="10pt">
                                                                                                                        <asp:ListItem>--- กรุณาระบุเหตุผล ---</asp:ListItem>
                                                                                                                        <asp:ListItem>เหตุผลไม่ผ่านการตรวจสอบ 1</asp:ListItem>
                                                                                                                        <asp:ListItem>เหตุผลไม่ผ่านการตรวจสอบ 2</asp:ListItem>
                                                                                                                        <asp:ListItem>เหตุผลไม่ผ่านการตรวจสอบ 3</asp:ListItem>
                                                                                                                        <asp:ListItem>เหตุผลไม่ผ่านการตรวจสอบ 4</asp:ListItem>
                                                                                                                        <asp:ListItem>เหตุผลไม่ผ่านการตรวจสอบ 5</asp:ListItem>
                                                                                                                    </asp:DropDownList></td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td style="text-align: right">
                                                                                                                    <font class="FormLabel">อื่น ๆ: </font></td>
                                                                                                                <td>
                                                                                                                    <telerik:RadTextBox ID="txtE99" runat="server" Width="450px">
                                                                                                                    </telerik:RadTextBox>
                                                                                                                    <asp:DropDownList ID="DropDownList13" runat="server" Font-Names="Tahoma" Font-Size="10pt">
                                                                                                                        <asp:ListItem>--- กรุณาระบุเหตุผล ---</asp:ListItem>
                                                                                                                        <asp:ListItem>เหตุผลไม่ผ่านการตรวจสอบ 1</asp:ListItem>
                                                                                                                        <asp:ListItem>เหตุผลไม่ผ่านการตรวจสอบ 2</asp:ListItem>
                                                                                                                        <asp:ListItem>เหตุผลไม่ผ่านการตรวจสอบ 3</asp:ListItem>
                                                                                                                        <asp:ListItem>เหตุผลไม่ผ่านการตรวจสอบ 4</asp:ListItem>
                                                                                                                        <asp:ListItem>เหตุผลไม่ผ่านการตรวจสอบ 5</asp:ListItem>
                                                                                                                    </asp:DropDownList></td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td>&nbsp;</td>
                                                                                                                <td>&nbsp;</td>
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
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td align="right" colspan="2" rowspan="2" valign="middle">
                                <font class="FormLabel">&#160;&nbsp;
                                    <asp:Button ID="Button1" runat="server" class="m-btn"
                                        TabIndex="27" Text="บันทึกผล" UseSubmitBehavior="False" Width="150px" CausesValidation="False" />&nbsp;</font><font class="FormLabel">&#160;&nbsp; </font>
                                <asp:Button ID="btnCancel" runat="server" class="m-btn" OnClientClick="javascript:CancelEdit();return false;"
                                    TabIndex="27" Text="ปิดหน้าต่าง" UseSubmitBehavior="False" Width="150px" CausesValidation="False" />&nbsp;
                            </td>
                        </tr>
                    </table>
                    <telerik:RadTextBox ID="RadTextBox1" runat="server" BackColor="TRANSPARENT" BorderStyle="None"
                        ClientEvents-OnFocus="onFocus" EnableEmbeddedSkins="false" TabIndex="28" Width="0px"
                        CssClass="FormFld">
                        <ClientEvents OnFocus="onFocus" />
                    </telerik:RadTextBox>
                </td>
            </tr>
        </table>
        <asp:SqlDataSource ID="SqlCountry" runat="server" ConnectionString="<%$ ConnectionStrings:OriginConnection %>"
            SelectCommand="sp_common_get_countryByFormType_NewDS" SelectCommandType="StoredProcedure">
            <SelectParameters>
                <asp:Parameter DefaultValue="FORM1" Name="FORM_TYPE" Type="String" />
            </SelectParameters>
        </asp:SqlDataSource>
        <asp:Label ID="lblRoleID" runat="server" Visible="False"></asp:Label>
        <asp:Label ID="lblSite_ID" runat="server" Visible="False"></asp:Label>
    </form>
</body>
</html>
