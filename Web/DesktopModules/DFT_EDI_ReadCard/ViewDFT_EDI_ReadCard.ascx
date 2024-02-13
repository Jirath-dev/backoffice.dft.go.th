<%@ Control Language="vb" Inherits="NTi.Modules.DFT_EDI_ReadCard.ViewDFT_EDI_ReadCard"
    AutoEventWireup="false" Explicit="True" Codebehind="ViewDFT_EDI_ReadCard.ascx.vb" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="RadAjaxManager1">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="rgReceiptList" />
            </UpdatedControls>
        </telerik:AjaxSetting>
    </AjaxSettings>
</telerik:RadAjaxManager>
<telerik:RadCodeBlock ID="RadCodeBlockFirst" runat="server">

    <script type="text/javascript">
       function popup(url)
        {
         params  = 'width=580';
         params += ', height=385';
         params += ', top=' + (screen.availHeight-400)/2  + ', left=' + ((screen.availWidth-580)-40)
         params += ', scrollbars=0, resizable=0';

         newwin=window.open(url,'windowname4', params);
         if (window.focus) {newwin.focus()}
         return false;
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
                            <tr class="m-frm-hdr">
                                <td>
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                        <tr class="m-frm-hdr">
                                            <td align="left" class="m-frm-hdr">
                                                <table border="0" cellpadding="0" cellspacing="0" cols="2">
                                                    <tr>
                                                        <td class="groupboxheader" nowrap="nowrap">
                                                            <font class="groupbox">&nbsp; ค้นหาข้อมูล &nbsp;</font></td>
                                                        <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
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
                                                <table border="0" cellpadding="0" cellspacing="2" width="100%">
                                                    <tr>
                                                        <td style="font-size: 12pt; color: #000000; font-family: Times New Roman">
                                                            <font class="FormLabel">เลขที่บัตรประจำตัวผู้ส่งออก :
                                                                <asp:TextBox ID="txtSearch" runat="server" MaxLength="9"></asp:TextBox>
                                                                <asp:Button ID="btnSearch" runat="server" Text="ค้นหา" ValidationGroup="search" Width="150px" />
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtSearch"
                                                                    Display="Dynamic" ErrorMessage="กรุณาป้อนหมายเลขบัตร" Font-Names="Tahoma" Font-Size="10pt"
                                                                    SetFocusOnError="True" ValidationGroup="search"></asp:RequiredFieldValidator></font></td>
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
            <table id="tblCardDetail" runat="server" border="0" cellpadding="0" cellspacing="0"
                width="100%" class="m-frm-bdr">
                <tr>
                    <td>
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr class="m-frm-hdr">
                                <td style="height: 45px">
                                    <telerik:RadTabStrip ID="RadTabStrip1" runat="server" Skin="Office2007" Width="100%"
                                        SelectedIndex="0" MultiPageID="RadMultiPage1" Font-Names="Tahoma" Font-Size="10pt">
                                        <Tabs>
                                            <telerik:RadTab runat="server" Text="ข้อมูลผู้ส่งออก" Selected="True" Font-Names="Tahoma"
                                                Font-Size="10pt">
                                            </telerik:RadTab>
                                        </Tabs>
                                    </telerik:RadTabStrip>
                                </td>
                            </tr>
                            <tr>
                                <td style="background-color: #eeeeee; border-width: 1px; border-style: solid; border-color: #CCCCCC;">
                                    <telerik:RadMultiPage ID="RadMultiPage1" runat="server" Width="100%" SelectedIndex="0"
                                        TabIndex="-1">
                                        <telerik:RadPageView ID="RadPageView1" runat="server" Width="100%" TabIndex="-1">
                                            <table border="0" cellpadding="0" cellspacing="2" width="100%">
                                                <tr>
                                                    <td>
                                                        <table border="0" cellpadding="0" cellspacing="0" class="m-frm-bdr" width="100%">
                                                            <tr class="m-frm-hdr">
                                                                <td align="left" class="m-frm-hdr">
                                                                    <table border="0" cellpadding="0" cellspacing="0">
                                                                        <tr>
                                                                            <td class="groupboxheader" nowrap="nowrap">
                                                                                <font class="groupbox">&nbsp; ข้อมูลผู้ส่งออก &nbsp;</font></td>
                                                                            <telerik:RadCodeBlock ID="RadCodeBlock3" runat="server">
                                                                                <td style='background-image: url(<%= ResolveUrl("images") %>/CORNER.gif); background-repeat: no-repeat'>
                                                                                    &nbsp; &nbsp;&nbsp;</td>
                                                                            </telerik:RadCodeBlock>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr class="m-frm-hdr">
                                                                <td align="left">
                                                                    <table border="0" cellpadding="0" cellspacing="2" width="100%">
                                                                        <tr>
                                                                            <td>
                                                                                <font class="FormLabel">ชื่อผู้ถือบัตร:</font>
                                                                                <telerik:RadTextBox ID="txtAuthName" runat="server" CssClass="FormFld" EnableEmbeddedSkins="False"
                                                                                    ReadOnly="True" TabIndex="-1" Width="200px">
                                                                                </telerik:RadTextBox>
                                                                                <font class="FormLabel">&nbsp;ระดับบัตร:</font>
                                                                                <telerik:RadTextBox ID="txtCard_Level" runat="server" CssClass="FormFld" EnableEmbeddedSkins="False"
                                                                                    ReadOnly="True" TabIndex="-1" Width="200px">
                                                                                </telerik:RadTextBox></td>
                                                                            <td rowspan="9" valign="top" style="padding-right: 10px; padding-left: 10px; padding-bottom: 10px; padding-top: 10px">
                                                                                <asp:Image ID="Image1" runat="server" width="150" BorderStyle="Outset" /></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="font-size: 12pt; color: #000000; font-family: Times New Roman">
                                                                                <font class="FormLabel">ชื่อบริษัท (ไทย):&nbsp;</font>
                                                                                <telerik:RadTextBox ID="txtCompanyName_Th" runat="server" CssClass="FormFld" EnableEmbeddedSkins="False"
                                                                                    ReadOnly="True" TabIndex="-1" Width="315px">
                                                                                </telerik:RadTextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="font-size: 12pt; color: #000000; font-family: Times New Roman">
                                                                                <font class="FormLabel">ชื่อบริษัท (อังกฤษ):&nbsp;</font>
                                                                                <telerik:RadTextBox ID="txtCompanyName_Eng" runat="server" CssClass="FormFld" EnableEmbeddedSkins="False"
                                                                                    ReadOnly="True" TabIndex="-1" Width="315px">
                                                                                </telerik:RadTextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="font-size: 12pt; color: #000000; font-family: Times New Roman">
                                                                                <font class="FormLabel">เลขประจำตัวผู้เสียภาษี:</font>
                                                                                <telerik:RadTextBox ID="txtCompany_Taxno" runat="server" CssClass="FormFld" EnableEmbeddedSkins="False"
                                                                                    ReadOnly="True" TabIndex="-1" Width="150px">
                                                                                </telerik:RadTextBox>
                                                                                <font class="FormLabel">เลขทะเบียนพาณิชย์:</font>
                                                                                <telerik:RadTextBox ID="txtCompany_Juristic" runat="server" CssClass="FormFld" EnableEmbeddedSkins="False"
                                                                                    ReadOnly="True" TabIndex="-1" Width="150px">
                                                                                </telerik:RadTextBox>
                                                                                <telerik:RadTextBox ID="txtCompany_BranchNo" runat="server" CssClass="FormFld" EnableEmbeddedSkins="False"
                                                                                    ReadOnly="True" TabIndex="-1" Visible="False" Width="1px">
                                                                                </telerik:RadTextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr style="font-size: 12pt; font-family: Times New Roman">
                                                                            <td style="font-size: 12pt; font-family: Times New Roman">
                                                                                <font class="FormLabel">ที่อยู่:&nbsp;</font>
                                                                                <telerik:RadTextBox ID="txtCompany_Address" runat="server" CssClass="FormFld" EnableEmbeddedSkins="False"
                                                                                    ReadOnly="True" TabIndex="-1" Width="600px">
                                                                                </telerik:RadTextBox></td>
                                                                        </tr>
                                                                        <tr style="font-size: 12pt; font-family: Times New Roman">
                                                                            <td>
                                                                                <font class="FormLabel">โทรศัพท์:</font>
                                                                                <telerik:RadTextBox ID="txtCompany_Phone" runat="server" CssClass="FormFld" EnableEmbeddedSkins="False"
                                                                                    ReadOnly="True" TabIndex="-1" Width="150px">
                                                                                </telerik:RadTextBox>
                                                                                <font class="FormLabel">&nbsp;โทรสาร:</font>
                                                                                <telerik:RadTextBox ID="txtCompany_Fax" runat="server" CssClass="FormFld" EnableEmbeddedSkins="False"
                                                                                    ReadOnly="True" TabIndex="-1" Width="150px">
                                                                                </telerik:RadTextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr style="font-size: 12pt; font-family: Times New Roman">
                                                                            <td>
                                                                                <table cellpadding="0" cellspacing="0">
                                                                                    <tr>
                                                                                        <td>
                                                                                            <font class="FormLabel">กรรมการผู้มีอำนาจ</font>&nbsp;</td>
                                                                                        <td>
                                                                                            <font class="FormLabel">1.</font>
                                                                                            <telerik:RadTextBox ID="txtAuthorize1" runat="server" CssClass="FormFld" EnableEmbeddedSkins="False"
                                                                                                ReadOnly="True" TabIndex="-1" Width="250px">
                                                                                            </telerik:RadTextBox>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                        </td>
                                                                                        <td>
                                                                                            <font class="FormLabel">2.</font>
                                                                                            <telerik:RadTextBox ID="txtAuthorize2" runat="server" CssClass="FormFld" EnableEmbeddedSkins="False"
                                                                                                ReadOnly="True" TabIndex="-1" Width="250px">
                                                                                            </telerik:RadTextBox>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                        </td>
                                                                                        <td>
                                                                                            <font class="FormLabel">3.</font>
                                                                                            <telerik:RadTextBox ID="txtAuthorize3" runat="server" CssClass="FormFld" EnableEmbeddedSkins="False"
                                                                                                ReadOnly="True" TabIndex="-1" Width="250px">
                                                                                            </telerik:RadTextBox>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                        </td>
                                                                                        <td>
                                                                                            <font class="FormLabel">4.</font>
                                                                                            <telerik:RadTextBox ID="txtAuthorize4" runat="server" CssClass="FormFld" EnableEmbeddedSkins="False"
                                                                                                ReadOnly="True" TabIndex="-1" Width="250px">
                                                                                            </telerik:RadTextBox>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                        </td>
                                                                                        <td>
                                                                                            <font class="FormLabel">5.</font>
                                                                                            <telerik:RadTextBox ID="txtAuthorize5" runat="server" CssClass="FormFld" EnableEmbeddedSkins="False"
                                                                                                ReadOnly="True" TabIndex="-1" Width="250px">
                                                                                            </telerik:RadTextBox>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <font class="FormLabel">หมายเหตุกรรมการ:&nbsp;</font>
                                                                                <telerik:RadTextBox ID="txtAuthorize_Remark" runat="server" CssClass="FormFld" EnableEmbeddedSkins="False"
                                                                                    ReadOnly="True" TabIndex="-1" Width="600px">
                                                                                </telerik:RadTextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                                                                <asp:TextBox ID="txtReturnMsg" runat="server" Font-Bold="True" Font-Names="Tahoma"
                                                                                    Font-Size="10pt" ForeColor="Red" Height="100px" TextMode="MultiLine" Width="460px"></asp:TextBox></td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </telerik:RadPageView>
                                    </telerik:RadMultiPage></td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
<asp:Label ID="lblRoleID" runat="server" Visible="False"></asp:Label>