<%@ Page Language="vb" AutoEventWireup="false" Codebehind="frmProductItem.aspx.vb"
    Inherits=".frmProductItem" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>รายการสินค้า</title>
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
                                                                        TabIndex="-1" UseSubmitBehavior="False" ValidationGroup="Insert" />
                                                                    <asp:Button ID="btnCancel" runat="server" Text="ESC - ยกเลิก" Width="150px"
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
                                                                    <font class="groupbox">&nbsp; รายละเอียดสินค้า &nbsp;</font></td>
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
                                                    <td>
                                                        <table>
                                                            <tr>
                                                                <td style="text-align: right">
                                                                    <font class="FormLabel">พิกัดสินค้า : </font>
                                                                </td>
                                                                <td>
                                                                    <telerik:RadTextBox ID="txtTariffCode" runat="server" TabIndex="1" Width="290px"
                                                                        Font-Names="Tahoma" Font-Size="10pt"></telerik:RadTextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtTariffCode"
                                                                        Display="Dynamic" ErrorMessage="กรูณาป้อนข้อมูล" Font-Names="Tahoma" Font-Size="10pt"
                                                                        InitialValue="0" ValidationGroup="Insert"></asp:RequiredFieldValidator>
                                                                    <asp:ImageButton ID="imbSearch" runat="server" ImageUrl="~/images/view.gif" /></td>
                                                                <td style="text-align: right">
                                                                    <font class="FormLabel">&nbsp;ต้นทุน : </font>
                                                                </td>
                                                                <td>
                                                                    <telerik:RadTextBox ID="txtCertificateNo" runat="server" TabIndex="-1" Width="250px" Font-Names="Tahoma"
                                                                        Font-Size="10pt" Enabled="False" ReadOnly="True"></telerik:RadTextBox></td>
                                                            </tr>
                                                            <tr>
                                                                <td style="text-align: right">
                                                                    <font class="FormLabel">รายละเอียดสินค้า :</font>
                                                                </td>
                                                                <td colspan="3">
                                                                    <telerik:RadTextBox ID="txtTariffName" runat="server" TabIndex="2" Width="500px"
                                                                        Font-Names="Tahoma" Font-Size="10pt"></telerik:RadTextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtTariffName"
                                                                        ErrorMessage="กรูณาป้อนข้อมูล" Font-Names="Tahoma" Font-Size="10pt" InitialValue="0"
                                                                        ValidationGroup="Insert"></asp:RequiredFieldValidator></td>
                                                            </tr>
                                                            <tr>
                                                                <td style="text-align: right">
                                                                    <font class="FormLabel">Net Weight : </font>
                                                                </td>
                                                                <td>
                                                                    <telerik:RadNumericTextBox ID="txtNetWeight" runat="server" Culture="(Default)" Font-Names="Tahoma"
                                                                        Font-Size="14pt" ForeColor="Blue" MaxLength="20" MaxValue="10000000" MinValue="0"
                                                                        ShowSpinButtons="False" TabIndex="3" Value="0" Width="190px">
                                                                        <EnabledStyle HorizontalAlign="Right" />
                                                                        <NumberFormat DecimalDigits="2" />
                                                                    </telerik:RadNumericTextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtNetWeight"
                                                                        ErrorMessage="กรูณาป้อนข้อมูล" Font-Names="Tahoma" Font-Size="10pt" InitialValue="0"
                                                                        ValidationGroup="Insert"></asp:RequiredFieldValidator></td>
                                                                <td style="text-align: right">
                                                                </td>
                                                                <td>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="text-align: right">
                                                                    <font class="FormLabel">FOB :</font>
                                                                </td>
                                                                <td>
                                                                    <telerik:RadNumericTextBox ID="txtFOB" runat="server" Culture="(Default)" Font-Names="Tahoma"
                                                                        Font-Size="14pt" ForeColor="Blue" MaxLength="20" MaxValue="10000000" MinValue="0"
                                                                        ShowSpinButtons="False" TabIndex="4" Value="0" Width="190px"><EnabledStyle HorizontalAlign="Right" />
                                                                        <NumberFormat DecimalDigits="2" />
                                                                    </telerik:RadNumericTextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtFOB"
                                                                        ErrorMessage="กรูณาป้อนข้อมูล" Font-Names="Tahoma" Font-Size="10pt" InitialValue="0"
                                                                        ValidationGroup="Insert"></asp:RequiredFieldValidator></td>
                                                                <td style="text-align: right">
                                                                </td>
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
        </table>
        <asp:Label ID="lblFormType" runat="server" Visible="False"></asp:Label>
        <asp:Label ID="lblCountry_Code" runat="server" Visible="False"></asp:Label>
        <asp:Label ID="lblCompany_Taxno" runat="server" Visible="False"></asp:Label>
        <asp:Label ID="lblINVH_RUN_AUTO" runat="server" Visible="False"></asp:Label>
        <asp:Label ID="lblSite_ID" runat="server" Visible="False"></asp:Label>
        <asp:Label ID="lblCard_id" runat="server" Visible="False"></asp:Label>
        <asp:Label ID="lblG_UNIT_CODE" runat="server" Visible="False"></asp:Label>
        <asp:Label ID="lblquantity5" runat="server" Visible="False"></asp:Label>
        <asp:Label ID="lblQ_UNIT_CODE5" runat="server" Visible="False"></asp:Label>
        <asp:Label ID="lblGROSS_WEIGHT" runat="server" Visible="False"></asp:Label>
    </form>
</body>
</html>
