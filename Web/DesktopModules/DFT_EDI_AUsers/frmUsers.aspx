<%@ Page Language="vb" AutoEventWireup="false" Codebehind="frmUsers.aspx.vb" Inherits=".frmUsers" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>จัดการบุคคลกร</title>
    <link href="css/skin.css" rel="stylesheet" type="text/css" />
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
            <tr align="center">
                <td>
                    <font class="FormHeader">ข้อมูลบุคคลากร</font></td>
            </tr>
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
                                                                    <font class="groupbox">&#160;&#160;รายละเอียดบุคคลากร&#160;&#160;</font></td>
                                                                <td style="background-image: url(<%= ResolveUrl("images") %>/CORNER.gif);
                                                                    background-repeat: no-repeat;">
                                                                    &#160;&#160;&#160;&#160;</td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td width="88%" align="left">
                                                        <asp:Label ID="lbl_ErrMSG" runat="server" Font-Names="Tahoma" Font-Size="10pt" ForeColor="Red" /></td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="m-frm" valign="top">
                                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td width="15">
                                                        &nbsp;</td>
                                                    <td>
                                                        <table width="100%" border="0" cellspacing="2" cellpadding="0">
                                                            <tr>
                                                                <td align="right">
                                                                    <font class="FormLabel">User Login Name :</font>
                                                                </td>
                                                                <td>
                                                                    <telerik:RadTextBox ID="txtUserName" runat="server" CssClass="FormFld" EnableEmbeddedSkins="false" MaxLength="20" Width="200px" TabIndex="1">
                                                                    </telerik:RadTextBox>
                                                                    <asp:RequiredFieldValidator ID="rfvTariffCode" Display="Dynamic"
                                                                        runat="server" ControlToValidate="txtUserName" ErrorMessage="*" SetFocusOnError="True" Font-Names="Tahoma" Font-Size="10pt" ValidationGroup="Save" /></td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <font class="FormLabel">Password :</font></td>
                                                                <td>
                                                                    <telerik:RadTextBox ID="txtPassword" runat="server" CssClass="FormFld" EnableEmbeddedSkins="false" MaxLength="20" Width="200px" TabIndex="1">
                                                                    </telerik:RadTextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="Dynamic"
                                                                        runat="server" ControlToValidate="txtPassword" ErrorMessage="*" SetFocusOnError="True" Font-Names="Tahoma" Font-Size="10pt" ValidationGroup="Save" />
                                                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtPassword"
                                                                        ControlToValidate="txtConfirmPassword" ErrorMessage="รหัสผ่าน และยืนยันรหัสผ่านไม่ตรงกัน"
                                                                        Font-Names="Tahoma" Font-Size="10pt" ValidationGroup="Save"></asp:CompareValidator></td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <font class="FormLabel">Confirm Password :</font></td>
                                                                <td>
                                                                    <telerik:RadTextBox ID="txtConfirmPassword" runat="server" CssClass="FormFld" EnableEmbeddedSkins="false" MaxLength="20" Width="200px" TabIndex="1">
                                                                    </telerik:RadTextBox></td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <font class="FormLabel">ชื่อ :</font></td>
                                                                <td>
                                                                    <telerik:RadTextBox ID="txtFirstName" runat="server" CssClass="FormFld" EnableEmbeddedSkins="false" MaxLength="20" Width="200px" TabIndex="1">
                                                                    </telerik:RadTextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" Display="Dynamic"
                                                                        runat="server" ControlToValidate="txtFirstName" ErrorMessage="*" SetFocusOnError="True" Font-Names="Tahoma" Font-Size="10pt" ValidationGroup="Save" />
                                                                    <font class="FormLabel">นามสกุล :</font>
                                                                    <telerik:RadTextBox ID="txtLastName" runat="server" CssClass="FormFld" EnableEmbeddedSkins="false" MaxLength="20" Width="200px" TabIndex="1">
                                                                    </telerik:RadTextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" Display="Dynamic"
                                                                        runat="server" ControlToValidate="txtLastName" ErrorMessage="*" SetFocusOnError="True" Font-Names="Tahoma" Font-Size="10pt" ValidationGroup="Save" /></td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <font class="FormLabel">สังกัด :</font></td>
                                                                <td>
                                                                    <telerik:RadComboBox ID="rcbSite" runat="server" DataSourceID="SqlSite" DataTextField="site_name"
                                                                        DataValueField="site_id" EmptyMessage="กรุณาเลือกรายการ" Filter="StartsWith"
                                                                        Font-Names="Tahoma" Font-Size="10pt" MarkFirstMatch="True" Skin="Web20" Width="300px">
                                                                    </telerik:RadComboBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" Display="Dynamic"
                                                                        runat="server" ControlToValidate="rcbSite" ErrorMessage="*" SetFocusOnError="True" Font-Names="Tahoma" Font-Size="10pt" ValidationGroup="Save" /></td>
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
                    <table cellspacing="0" border="0" class="m-frm-bdr" cellpadding="1" width="100%">
                        <tr>
                            <td>
                                <table cellspacing="0" cellpadding="0" border="0" width="100%">
                                    <tr class="m-frm-hdr">
                                        <td>
                                            <table cellspacing="0" cellpadding="0" border="0" width="100%">
                                                <tr class="m-frm-hdr">
                                                    <td width="80%" align="left" class="m-frm-hdr">
                                                        <table cols="2" border="0" cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td nowrap class="groupboxheader">
                                                                    <font class="groupbox">&nbsp; สิทธิ์การใช้งาน &nbsp;</font></td>
                                                                <td style="background-image: url(<%= ResolveUrl("images") %>/CORNER.gif);
                                                                    background-repeat: no-repeat;">
                                                                    &nbsp; &nbsp;&nbsp;</td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td width="20%" align="left">
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr style="font-size: 12pt; color: #000000">
                                        <td class="m-frm" valign="top">
                                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td width="15">
                                                        &nbsp;</td>
                                                    <td>
                                                        <table width="100%" border="0" cellspacing="2" cellpadding="0">
                                                            <tr>
                                                                <td>
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
                    <asp:Button ID="btnInsert" Text="บันทึกเพิ่มบุคคลากร" runat="server" TabIndex="26" ValidationGroup="Save" />
                    <asp:Button ID="btnSave" Text="บันทึกแก้ไขบุคคลากร" runat="server" TabIndex="26" ValidationGroup="Save" />
                    <asp:Button ID="btnCancel" runat="server" TabIndex="27" Text="ยกเลิก" UseSubmitBehavior="False" Width="150px" CausesValidation="False" />
                </td>
            </tr>
        </table>
        <asp:SqlDataSource ID="SqlSite" runat="server" ConnectionString="<%$ ConnectionStrings:OriginConnection %>"
            SelectCommand="SELECT [site_id], [site_name] FROM [site] ORDER BY [site_name]"></asp:SqlDataSource>
    </form>
</body>
</html>
