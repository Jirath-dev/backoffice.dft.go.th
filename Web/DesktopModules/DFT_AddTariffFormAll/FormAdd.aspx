<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FormAdd.aspx.vb" Inherits=".FormAdd" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<link href="../../Popup/skin.css" rel="stylesheet" type="text/css" />
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    
</head>
<body>
<telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

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
                
                function onFocus(sender, eventArgs){
                    $find("<%= txtNumTariff.ClientID %>").focus();
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
    <form id="form1" runat="server">
        <div>
            <table border="1" cellpadding="3" cellspacing="3" style="width: 100%">
                <tr>
                    <td style="width: 100%; background-color: #6699cc;" align="center">
                        <asp:Label ID="lblHeader" runat="server" Font-Bold="True" ForeColor="Black" TabIndex="-1"></asp:Label></td>
                </tr>
                <tr>
                    <td style="width: 100%">
                        <table style="width: 100%">
                            <tr>
                                <td style="width: 30%; background-color: mintcream;">
                                    ชื่อฟอร์ม</td>
                                <td style="width: 70%">
                                    <telerik:RadComboBox ID="DDLListForm" runat="server" AutoPostBack="True" DataSourceID="SqlDataSource1"
                                        DataTextField="form_name" DataValueField="form_type" Skin="Vista" Width="70%"
                                        TabIndex="-1">
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="FormLabel" style="width: 30%; background-color: mintcream;">
                                    ประเทศ</td>
                                <td style="width: 70%">
                                    <telerik:RadComboBox ID="DDLcountry" runat="server" AutoPostBack="True" BackColor="Transparent"
                                        DataTextField="DESCRIPTION" DataValueField="CODE" Width="50%" TabIndex="-1" Skin="Vista">
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="FormLabel" style="width: 30%; background-color: mintcream;">
                                    Code ประเทศ</td>
                                <td style="width: 70%">
                                    <asp:TextBox ID="txtCodecountry" runat="server" BackColor="Gainsboro" Enabled="False"
                                        Width="145px" TabIndex="-1"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td class="FormLabel" style="width: 30%; background-color: mintcream;">
                                    เลขพิกัดศุลกากร</td>
                                <td style="width: 70%">
                                    <telerik:RadTextBox ID="txtNumTariff" runat="server" Width="260px">
                                    </telerik:RadTextBox>
                                    <asp:RequiredFieldValidator ID="RqtxtNumTariff" runat="server" class="FormErr" ControlToValidate="txtNumTariff"
                                        Display="Dynamic" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="GBtncheck"></asp:RequiredFieldValidator></td>
                            </tr>
                            <tr>
                                <td class="FormLabel" style="width: 30%; background-color: mintcream;" valign="top">
                                    รายละเอียดพิกัดศุลกากร</td>
                                <td style="width: 70%" valign="top">
                                    <asp:TextBox ID="txttariff_name" runat="server" Height="100px" TextMode="MultiLine"
                                        Width="390px" TabIndex="1"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="Rqtxttariff_name" runat="server" class="FormErr"
                                        ControlToValidate="txttariff_name" Display="Dynamic" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="GBtncheck"></asp:RequiredFieldValidator></td>
                            </tr>
                            <tr>
                                <td class="FormLabel" style="width: 30%; background-color: mintcream;">
                                    จำนวนพิกัดศุลกากร (Check Digit)</td>
                                <td style="width: 70%">
                                    <asp:TextBox ID="txtcheck_digit" runat="server" TabIndex="2"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="Rqtxtcheck_digit" runat="server" class="FormErr"
                                        ControlToValidate="txtcheck_digit" Display="Dynamic" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="GBtncheck"></asp:RequiredFieldValidator></td>
                            </tr>
                            <tr>
                                <td class="FormLabel" style="width: 30%; background-color: mintcream;">
                                    rate_desc</td>
                                <td style="width: 70%">
                                    <asp:TextBox ID="txtrate_desc" runat="server" TabIndex="3"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td class="FormLabel" style="width: 30%; background-color: mintcream">
                                    CAT <span style="color: red">(เฉพาะฟอร์ม ซีโอ Textile)</span></td>
                                <td style="width: 70%">
                                    <asp:TextBox ID="txtCAT" runat="server" Enabled="False" TabIndex="-1"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td align="center" class="FormLabel" colspan="2">
                                </td>
                            </tr>
                            <tr>
                                <td align="center" class="FormLabel" colspan="2">
                                    <asp:Button ID="btnAdd" runat="server" Text="เพิ่มพิกัดศุลกากร" Width="130px" TabIndex="4" ValidationGroup="GBtncheck" /><asp:Button
                                        ID="btnSave" runat="server" Text="บันทึกพิกัดศุลกากร" Width="130px" TabIndex="5" ValidationGroup="GBtncheck" /><asp:Button
                                            ID="btnCancel" runat="server" Text="ยกเลิก" Width="130px" TabIndex="6" /></td>
                            </tr>
                            <tr>
                                <td colspan="2" align="center">
                                    <asp:Label ID="lbl_ErrMSG" runat="server" Font-Bold="True" ForeColor="Red" TabIndex="-1"></asp:Label>
                                    <asp:TextBox ID="txtTemp_tariff_code" runat="server" Visible="False" TabIndex="-1"></asp:TextBox>
                                    <asp:TextBox ID="txtTemp_country_code" runat="server" Visible="False" TabIndex="-1"></asp:TextBox>
                                    <asp:TextBox ID="txtTemp_form" runat="server" Visible="False" TabIndex="-1"></asp:TextBox>
                                    <asp:TextBox ID="txtTemp_CAT" runat="server" TabIndex="-1" Visible="False"></asp:TextBox></td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:EDIConnection %>"
            SelectCommand="SELECT     form_type, form_name, reserved_record, price, ShowOrder, ShowOrder_Report FROM dbo.form_type WHERE (form_type NOT IN ('ALL','FORM1_5', 'FORM3_1', 'FORM4_1'))">
        </asp:SqlDataSource>
        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="DDLListForm">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="DDLListForm" />
                        <telerik:AjaxUpdatedControl ControlID="DDLcountry" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="DDLcountry">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="DDLListForm" />
                        <telerik:AjaxUpdatedControl ControlID="DDLcountry" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>
        
    </form>
</body>
</html>
