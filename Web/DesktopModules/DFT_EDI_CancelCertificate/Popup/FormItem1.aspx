<%@ Page Language="vb" AutoEventWireup="false" Codebehind="FormItem1.aspx.vb" Inherits=".FormItem1" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<link href="../Popup/skin.css" rel="stylesheet" type="text/css" />
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../CSS/skin.css" rel="stylesheet" type="text/css" />
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
                
                function onFocus(){
                   $find("<%= txtTariffCode.ClientID %>").focus();
                }
            </script>
        </telerik:RadCodeBlock>
        <br />
        <table width="100%" border="0" cellpadding="0" cellspacing="5">
            <tr align="center">
                <td>
                    <font class="FormHeader">ใบรายการสินค้า</font></td>
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
                                                                    <font class="groupbox">&#160;&#160;<asp:Label ID="lblHeader" class="FormLabel" runat="server" />&#160;&#160;</font></td>
                                                                <td style="background-image: url(<%= ResolveUrl("../images") %>/CORNER.gif); background-repeat:no-repeat;">&#160;&#160;&#160;&#160;</td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td width="88%" align="left" class="m-frm-nav">
                                                        <asp:Label ID="lbl_ErrMSG" class="FormErr" runat="server" /><asp:TextBox ID="txtInvHRunAuto"
                                                            runat="server" Visible="False" /><asp:TextBox ID="txtInvDRunAuto" runat="server"
                                                                Visible="False" /></td>
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
                                                                <td>
                                                                    <asp:RequiredFieldValidator ID="rfvTariffCode" Display="Dynamic" class="FormErr" runat="server"
                                                                        ControlToValidate="txtTariffCode" ErrorMessage="*" SetFocusOnError="True" /></td>
                                                                <td>
                                                                    <font class="FormLabel">พิกัดสินค้า</font>&nbsp;<telerik:RadTextBox ID="txtTariffCode" runat="server" CssClass="FormFld" EnableEmbeddedSkins="false"
                                                                        MaxLength="20" Width="200px" TabIndex="1">
                                                                    </telerik:RadTextBox>
                                                                    <font class="FormLabel">&nbsp;
                                                                        <asp:RequiredFieldValidator ID="rfvProductName" Display="Dynamic" class="FormErr"
                                                                            runat="server" ControlToValidate="txtProductName" ErrorMessage="*" SetFocusOnError="True" />
                                                                        ชื่อสินค้า</font>
                                                                    <telerik:RadTextBox ID="txtProductName" runat="server" CssClass="FormFld" EnableEmbeddedSkins="false"
                                                                        MaxLength="255" Width="220px" TabIndex="2">
                                                                    </telerik:RadTextBox></td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:RequiredFieldValidator ID="rfvProductDescription" Display="Dynamic" class="FormErr"
                                                                        runat="server" ControlToValidate="txtProductDescription" ErrorMessage="*" SetFocusOnError="True" /></td>
                                                                <td>
                                                                    <font class="FormLabel">รายละเอียดสินค้า</font>&nbsp;<telerik:RadTextBox ID="txtProductDescription"
                                                                        runat="server" CssClass="FormFld2" EnableEmbeddedSkins="false" MaxLength="300"
                                                                        Rows="3" SelectionOnFocus="SelectAll" TabIndex="3" TextMode="MultiLine" Width="440px">
                                                                    </telerik:RadTextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:RequiredFieldValidator ID="rfvNetWeight" Display="Dynamic" class="FormErr" runat="server"
                                                                        ControlToValidate="txtNetWeight" ErrorMessage="*" SetFocusOnError="True" /></td>
                                                                <td>
                                                                    <table cellpadding="0" cellspacing="0">
                                                                        <tr>
                                                                            <td colspan="2">
                                                                                <font class="FormLabel">น้ำหนักสุทธิ</font> 
                                                                            </td>
                                                                            <td>
                                                                                <telerik:RadNumericTextBox ID="txtNetWeight"
                                                                        runat="server" CssClass="FormFld" Culture="Thai (Thailand)" EnableEmbeddedSkins="False"
                                                                        ForeColor="Blue" MaxValue="1000000" MinValue="-1000000" ShowSpinButtons="False"
                                                                        Skin="Office2007" TabIndex="6" Width="180px">
                                                                    </telerik:RadNumericTextBox></td>
                                                                            <td>
                                                                        <asp:RequiredFieldValidator ID="rfvUnitCode2" Display="Dynamic" class="FormErr" runat="server"
                                                                            ControlToValidate="dropUnitCode2" ErrorMessage="*" SetFocusOnError="True" /></td>
                                                                            <td>
                                                                                &nbsp;<font class="FormLabel">&#160;
                                                                        หน่วย</font>
                                                                            </td>
                                                                            <td>
                                                                   <telerik:RadComboBox ID="dropUnitCode2" runat="server" EnableLoadOnDemand="True"
                                                     Filter="StartsWith" MarkFirstMatch="True" Width="100px" Skin="Office2007" DataSourceID="Sqlunit" DataTextField="DESCRIPTION" DataValueField="CODE" TabIndex="7">
                                                    </telerik:RadComboBox>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:RequiredFieldValidator ID="rfvFOBAmt" Display="Dynamic" class="FormErr" runat="server"
                                                                        ControlToValidate="txtFOBAmt" ErrorMessage="*" SetFocusOnError="True" /></td>
                                                                <td>
                                                                    <font class="FormLabel">มูลค่า US$ (FOB)</font>&nbsp;<telerik:RadNumericTextBox ID="txtFOBAmt"
                                                                        runat="server" CssClass="FormFld" Culture="Thai (Thailand)" EnableEmbeddedSkins="False"
                                                                        ForeColor="Blue" MaxValue="1000000" MinValue="-1000000" ShowSpinButtons="False"
                                                                        Skin="Office2007" TabIndex="8" Width="158px" MaxLength="20">
                                                                    </telerik:RadNumericTextBox></td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                </td>
                                                                <td>
                                                                    <table cellpadding="0" cellspacing="0">
                                                                        <tr>
                                                                            <td>
                                                                                <font class="FormLabel">แสดงมูลค่า </font>&nbsp;</td>
                                                                            <td>
                                                                                <telerik:RadComboBox ID="dropFOBDisplay" runat="server">
                                                                                    <Items>
                                                                                        <telerik:RadComboBoxItem runat="server" Text="แสดงในหนังสือรับรอง" Value="Display" />
                                                                                        <telerik:RadComboBoxItem runat="server" Text="ไม่แสดงในหนังสือรับรอง" Value="NoDisplay" />
                                                                                    </Items>
                                                                                </telerik:RadComboBox>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:RequiredFieldValidator ID="rfvMarks" Display="Dynamic" Enabled="false" class="FormErr"
                                                                        runat="server" ControlToValidate="txtMarks" ErrorMessage="*" SetFocusOnError="True" /></td>
                                                                <td>
                                                                    <font class="FormLabel">Marks</font>&nbsp;<telerik:RadTextBox ID="txtMarks"
                                                                        runat="server" CssClass="FormFld2" EnableEmbeddedSkins="false" MaxLength="500"
                                                                        Rows="3" SelectionOnFocus="SelectAll" TabIndex="9" TextMode="MultiLine" Width="520px">
                                                                    </telerik:RadTextBox></td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                </td>
                                                                <td>
                                                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                        <tr>
                                                                            <td colspan="2">
                                                                                <asp:RadioButtonList ID="radMaterialType" runat="server" class="FormLabel" RepeatColumns="3" TabIndex="10">
                                                                                    <asp:ListItem Value="0" Selected="True">เป็นวัสดุที่นำเข้าจากต่างประเทศ</asp:ListItem>
                                                                                    <asp:ListItem Value="1">เป็นรายการวัสดุภายในประเทศ</asp:ListItem>
                                                                                </asp:RadioButtonList></td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2" rowspan="8">
                                                                    <font class="FormLabel"></font> 
                                                                    <font class="FormLabel"></font> 
                                                                    <font class="FormLabel"></font> 
                                                                    <font class="FormLabel"></font> </td>
                                                            </tr>
                                                            <tr>
                                                            </tr>
                                                            <tr>
                                                            </tr>
                                                            <tr>
                                                            </tr>
                                                            <tr>
                                                            </tr>
                                                            <tr>
                                                            </tr>
                                                            <tr>
                                                            </tr>
                                                            <tr>
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
            <td style="text-align: right">
                                <asp:Button ID="btnCancel" runat="server" TabIndex="27" Text="ยกเลิก" UseSubmitBehavior="False" Width="150px" CausesValidation="False" /></td>
        </table>
<asp:SqlDataSource ID="Sqlunit" runat="server" ConnectionString="<%$ ConnectionStrings:OriginConnection %>"
SelectCommand="sp_common_get_unit" SelectCommandType="StoredProcedure">
</asp:SqlDataSource>
<asp:SqlDataSource ID="SqlDataLetter" runat="server" ConnectionString="<%$ ConnectionStrings:OriginConnection %>"
SelectCommand="sp_common_get_letter" SelectCommandType="StoredProcedure">
<SelectParameters>
        <asp:Parameter DefaultValue="FORM1" Name="FORM_TYPE" Type="String" />
    </SelectParameters>
</asp:SqlDataSource>
    <telerik:RadNumericTextBox ID="txtPercentImport2"
                                                                        runat="server" CssClass="FormFld" Culture="Thai (Thailand)" EnableEmbeddedSkins="False"
                                                                        ForeColor="Blue" MaxValue="1000000" MinValue="-1000000" ShowSpinButtons="False"
                                                                        Skin="Office2007" TabIndex="3" Width="40px" BackColor="Transparent" Enabled="False" MaxLength="20" Visible="False">
                                </telerik:RadNumericTextBox>
        <telerik:RadNumericTextBox ID="txtPercentImport1"
                                                                        runat="server" CssClass="FormFld" Culture="Thai (Thailand)" EnableEmbeddedSkins="False"
                                                                        ForeColor="Blue" MaxValue="1000000" MinValue="-1000000" ShowSpinButtons="False"
                                                                        Skin="Office2007" TabIndex="3" Width="40px" BackColor="Transparent" Enabled="False" MaxLength="20" Visible="False">
                                </telerik:RadNumericTextBox>
    </form>
</body>
</html>
