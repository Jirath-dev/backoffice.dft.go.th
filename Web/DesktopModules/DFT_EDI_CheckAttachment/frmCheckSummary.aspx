<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frmCheckSummary.aspx.vb" Inherits=".frmCheckSummary" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>สรุปจำนวนคำขอฟอร์มแยกตามประเภทของฟอร์ม </title>
    <link href="CSS/skin.css" rel="stylesheet" />
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
    <style type="text/css">
        table.tb-data {
            margin-top: 8px;
            border: 1px solid #cccccc;
            border-right: 0px;
            background: #ffffff;
        }

            table.tb-data td {
                padding: 3px;
                border-right: 1px solid #cccccc;
                border-bottom: 1px solid #cccccc;
            }

        table td.head {
            background: #ececec;
            text-align: center;
            font-weight: bold;
        }

        .sealsign {
            color: Green;
        }
    </style>
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
                                                    <td class="FormLabel">
                                                        <table style="margin-bottom: 4px;">
                                                            <tr>
                                                                <td align="right">
                                                                    <font class="FormLabel">ฟอร์ม :</font>
                                                                </td>
                                                                <td>
                                                                    <telerik:RadComboBox ID="dropFormType" runat="server" DataTextField="form_nameUsd" DataValueField="form_type"
                                                                        Filter="StartsWith" MarkFirstMatch="True" Skin="Web20" Width="400px" Font-Names="Tahoma"
                                                                        Font-Size="10pt">
                                                                    </telerik:RadComboBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <font class="FormLabel">วันที่ตรวจ ตั้งแต่ :</font>
                                                                </td>
                                                                <td>
                                                                    <table cellpadding="0" cellspacing="0">
                                                                        <tr>
                                                                            <td>
                                                                                <telerik:RadDatePicker ID="rdpFromDate" runat="server" Culture="English (United Kingdom)"
                                                                                    Skin="Vista">
                                                                                    <Calendar DayNameFormat="FirstTwoLetters" FirstDayOfWeek="Sunday" ShowRowHeaders="False"
                                                                                        Skin="Vista" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                                                        ViewSelectorText="x">
                                                                                    </Calendar>
                                                                                    <DatePopupButton HoverImageUrl="" ImageUrl="" />
                                                                                </telerik:RadDatePicker>
                                                                            </td>
                                                                            <td>
                                                                                <font class="FormLabel">&nbsp;ถึง :&nbsp;</font>
                                                                            </td>
                                                                            <td>
                                                                                <telerik:RadDatePicker ID="rdpToDate" runat="server" Culture="English (United Kingdom)"
                                                                                    Skin="Vista">
                                                                                    <Calendar DayNameFormat="FirstTwoLetters" FirstDayOfWeek="Sunday" ShowRowHeaders="False"
                                                                                        Skin="Vista" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                                                        ViewSelectorText="x">
                                                                                    </Calendar>
                                                                                    <DatePopupButton HoverImageUrl="" ImageUrl="" />
                                                                                </telerik:RadDatePicker>
                                                                            </td>
                                                                            <td align="right">
                                                                                <asp:Button ID="btnSearch" runat="server" Text="ค้นหา" Width="150px" />
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
                                            
                                            <div style="padding: 15px;border-top:solid 1px #ccc; border-bottom:solid 1px #ccc">
                                                <asp:Label ID="lblSummary" runat="server" Text=""></asp:Label>
                                            </div>
                                           
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div style="padding: 10px; text-align: right">
                                                <asp:Button ID="btnCancel" runat="server" OnClientClick="javascript:CancelEdit();return false;"
                                                    TabIndex="27" Text="ปิดหน้าต่าง" UseSubmitBehavior="False" Width="150px" CausesValidation="False" />&nbsp;
                                            </div>
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
