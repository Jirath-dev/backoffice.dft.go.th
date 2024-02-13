<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frmManageQuota.aspx.vb" Inherits=".frmManageQuota" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>โควต้าสินค้าแป้งมันสำปะหลัง</title>
    <link href="css/skin.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>
        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        </telerik:RadAjaxManager>
        <telerik:RadCodeBlock ID="RadCodeBlock4" runat="server">
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
            <tr align="center">
                <td>
                    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                        <td style="width: 50%; text-align: left">
                            <asp:Button ID="btnInsert" runat="server" Text="บันทึก" Width="150px" />
                            <asp:Button ID="btnUpdate" runat="server" Text="บันทึก" Width="150px" />
                        </td>
                        <td style="width: 50%; text-align: right">
                            <asp:Button ID="btnClose" runat="server" Text="ปิดหน้าต่าง" Width="150px" /></td>
                    </table>
                </td>
            </tr>
        </table>
        <table width="100%" border="0" cellspacing="5" cellpadding="0">
            <tr>
                <td>
                    <table cellspacing="0" border="0" class="m-frm-bdr" cellpadding="1" width="100%">
                        <tr>
                            <td>
                                <table cellspacing="0" cellpadding="0" border="0" width="100%">
                                    <tr class="m-frm-hdr">
                                        <td>
                                            <table cellspacing="0" cellpadding="0" border="0" style="width: 100%">
                                                <tr class="m-frm-hdr">
                                                    <td width="28%" align="left" class="m-frm-hdr">
                                                        <table cols="2" border="0" cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td nowrap class="groupboxheader">
                                                                    <font class="groupbox">รายละเอียดการทำรายการ&#160;</font></td>
                                                                <telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">
                                                                    <td style="background-image: url(<%= ResolveUrl("images") %>/CORNER.gif);
                                                                        background-repeat: no-repeat;">
                                                                        &#160;&#160;&#160;&#160;</td>
                                                                </telerik:RadCodeBlock>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="m-frm" valign="top" width="100%">
                                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td width="15">
                                                        &nbsp;</td>
                                                    <td>
                                                        <table>
                                                            <tr>
                                                                <td colspan="3">
                                                                    <asp:Label ID="lbl_ErrMSG" runat="server" Font-Names="Tahoma" Font-Size="10pt" ForeColor="Red"></asp:Label></td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <font class="FormLabel">วันที่:</font></td>
                                                                <td>
                                                                    <font class="FormLabel">ประเภทรายการ:</font></td>
                                                                <td>
                                                                    <font class="FormLabel">ปริมาณ (ตัน):</font>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <telerik:RadDatePicker ID="rdpTSKDate" runat="server" Skin="Vista" Culture="English (United Kingdom)" TabIndex="20" Width="150px"><DateInput TabIndex="20">
                                                                    </DateInput>
                                                                        <Calendar Skin="Vista" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                                            ViewSelectorText="x" DayNameFormat="FirstTwoLetters" FirstDayOfWeek="Sunday" ShowRowHeaders="False">
                                                                        </Calendar>
                                                                        <DatePopupButton HoverImageUrl="" ImageUrl="" TabIndex="20" />
                                                                    </telerik:RadDatePicker>
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="ddlTranTypeStock" runat="server" AutoPostBack="True">
                                                                    </asp:DropDownList></td>
                                                                <td>
                                                                    <telerik:RadNumericTextBox ID="txtTSK_Value" runat="server" Font-Names="Tahoma" Font-Size="10pt" Value="0">
                                                                        <EnabledStyle HorizontalAlign="Right" />
                                                                        <NumberFormat DecimalDigits="4" />
                                                                    </telerik:RadNumericTextBox>
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
        <asp:Label ID="lblTTS_DC" runat="server" Visible="False"></asp:Label><asp:Label ID="lblTSK_ID" runat="server" Visible="False"></asp:Label>
    </form>
</body>
</html>
