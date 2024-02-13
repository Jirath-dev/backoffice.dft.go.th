<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frmApproved.aspx.vb" Inherits=".frmApproved" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>บันทึกผล</title>
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
                
                function InitializePopup(sender, eventArgs) {
                    if (sender.isEmpty()) {
                        eventArgs.set_cancelCalendarSynchronization(true);
                        var popup = eventArgs.get_popupControl();
                        if (popup == sender.get_calendar()) {
                            if (popup.get_selectedDates().length == 0) {
                                var todaysDate = new Date();
                                var todayTriplet = [todaysDate.getFullYear(), todaysDate.getMonth() + 1, todaysDate.getDate()];
                                popup.selectDate(todayTriplet, true);
                            }
                        }
                        else {
                            var time = popup.getTime();
                            if (!time) {
                                time = new Date();
                                time.setHours(12);
                                popup.setTime(time);
                            }
                        }
                    }
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
                                                    <td>
                                                        <table border="0" cellpadding="0" cellspacing="2" width="100%">
                                                            <tr>
                                                                <td>
                                                                    <asp:Button ID="btnSave" runat="server" Text="F9 - บันทึก" Width="150px"
                                                                        TabIndex="-1" UseSubmitBehavior="False" />
                                                                    <asp:Button ID="btnCancel" runat="server" Text="ESC - ออก (ไม่บันทึก)" Width="150px"
                                                                        TabIndex="-1" UseSubmitBehavior="False" /></td>
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
                                    <tr class="m-frm-hdr">
                                        <td>
                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                <tr class="m-frm-hdr">
                                                    <td align="left" class="m-frm-hdr">
                                                        <table border="0" cellpadding="0" cellspacing="0" cols="2">
                                                            <tr>
                                                                <td class="groupboxheader" nowrap="nowrap">
                                                                    <font class="groupbox">&nbsp; ผลการตรวจสอบ &nbsp;</font></td>
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
                                                    <td width="15">
                                                        &nbsp;</td>
                                                    <td align="center">
                                                        <table>
                                                            <tr>
                                                                <td colspan="2">
                                                                    <asp:RadioButtonList ID="rblType" runat="server" RepeatDirection="Horizontal"
                                                                        Width="500px">
                                                                        <asp:ListItem Selected="True" Value="A">อนุมัติ</asp:ListItem>
                                                                        <asp:ListItem Value="N">ต้องแก้ไข</asp:ListItem>
                                                                        <asp:ListItem Value="C">ยกเลิก</asp:ListItem>
                                                                        <asp:ListItem Value="E">อนุมัติ/แก้ไข</asp:ListItem>
                                                                    </asp:RadioButtonList></td>
                                                            </tr>
                                                            <tr>
                                                                <td style="text-align: right">
                                                                    <font class="FormLabel">วันที่ : </font>
                                                                </td>
                                                                <td align="left">
                                                                    <telerik:RadDatePicker ID="rdpApprovedDate" runat="server" Culture="English (United Kingdom)"
                                                                        Skin="Office2007" TabIndex="10">
                                                                        <Calendar DayNameFormat="FirstTwoLetters" FirstDayOfWeek="Sunday" ShowRowHeaders="False"
                                                                            Skin="Office2007" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                                            ViewSelectorText="x">
                                                                        </Calendar>
                                                                        <DatePopupButton HoverImageUrl="" ImageUrl="" TabIndex="10" />
                                                                        <DateInput TabIndex="10">
                                                                        </DateInput>
                                                                        <ClientEvents OnPopupOpening="InitializePopup" />
                                                                    </telerik:RadDatePicker>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="text-align: right">
                                                                    <font class="FormLabel">เลขที่หนังสือรับรอง : </font>
                                                                </td>
                                                                <td align="left"><telerik:RadTextBox ID="txtReferenceCode2" runat="server" TabIndex="1" Width="200px"
                                                                        Font-Names="Tahoma" Font-Size="10pt">
                                                                </telerik:RadTextBox></td>
                                                            </tr>
                                                            <tr>
                                                                <td style="text-align: right">
                                                                    <font class="FormLabel">เหตุผล :</font>
                                                                </td>
                                                                <td align="left"><telerik:RadTextBox ID="txtRemark" runat="server" TabIndex="1" Width="400px"
                                                                        Font-Names="Tahoma" Font-Size="10pt">
                                                                </telerik:RadTextBox></td>
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
        <asp:Label ID="lblINVH_RUN_AUTO" runat="server" Visible="False"></asp:Label>
        <asp:Label ID="lblSite_ID" runat="server" Visible="False"></asp:Label>
        <asp:Label ID="lblUserName" runat="server" Visible="False"></asp:Label>
    </form>
</body>
</html>
