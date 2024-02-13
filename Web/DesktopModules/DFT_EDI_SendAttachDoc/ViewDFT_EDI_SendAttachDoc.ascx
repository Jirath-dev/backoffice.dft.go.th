<%@ Control language="vb" Inherits="NTi.Modules.DFT_EDI_SendAttachDoc.ViewDFT_EDI_SendAttachDoc" AutoEventWireup="false" Explicit="True" Codebehind="ViewDFT_EDI_SendAttachDoc.ascx.vb" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
</telerik:RadAjaxManager>
<table width="100%" border="0" cellspacing="3" cellpadding="0">
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
                                            <td width="88%" align="left" class="m-frm-hdr">
                                                <table cols="2" border="0" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td nowrap class="groupboxheader">
                                                            <font class="groupbox">&#160;&#160;ค้นหา&#160;&#160;</font></td>
                                                        <telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">
                                                            <td style="background-image: url(<%= ResolveUrl("images") %>/CORNER.gif); background-repeat: no-repeat;">
                                                                &#160;&#160;&#160;&#160;</td>
                                                        </telerik:RadCodeBlock>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td width="12%" align="left" class="m-frm-nav">
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
                                                <table width="100%" border="0" cellspacing="2" cellpadding="0">
                                                    <tr>
                                                        <td>
                                                            <font class="FormLabel">หมายเลขหนังสือรับรอง : </font>
                                                            <asp:TextBox ID="txtReferenceCode2" runat="server" Font-Names="Tahoma" Font-Size="14pt"
                                                                MaxLength="15" Width="150px"></asp:TextBox>
                                                            <asp:Button ID="btnSearch" runat="server" Text="ค้นหา" ValidationGroup="search" Width="150px" />
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtReferenceCode2"
                                                                ErrorMessage="กรุณาป้อนเลขที่หนังสือรับรอง" Font-Names="Tahoma" Font-Size="10pt"
                                                                ValidationGroup="search"></asp:RequiredFieldValidator></td>
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
<table id="tblHeader" runat="server" cellpadding="0" cellspacing="0">
    <tr>
        <td>
            <table width="100%" border="0" cellspacing="3" cellpadding="0">
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
                                                                        <font class="groupbox">&#160;&#160; ผู้ขอ&#160;&#160;</font></td>
                                                                    <telerik:RadCodeBlock ID="RadCodeBlock3" runat="server">
                                                                        <td style="background-image: url(<%= ResolveUrl("images") %>/CORNER.gif); background-repeat: no-repeat;">
                                                                            &#160;&#160;&#160;&#160;</td>
                                                                    </telerik:RadCodeBlock>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td width="88%" align="left" class="m-frm-nav">
                                                            &nbsp;</td>
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
                                                            <table width="100%" border="0" cellspacing="2" cellpadding="0">
                                                                <tr>
                                                                    <td>
                                                                        <font class="FormLabel">ชื่อ :
                                                                            <asp:Label ID="lblRequestPerson" runat="server" CssClass="FormFld"></asp:Label></font>
                                                                        <font class="FormLabel">&nbsp;ในนามของ : </font>
                                                                        <asp:Label ID="lblCompanyName" runat="server" CssClass="FormFld"></asp:Label></td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <font class="FormLabel">เลขประจำตัวผู้เสียภาษี : </font>
                                                                        <asp:Label ID="lblCompanyTaxNo" runat="server" CssClass="FormFld"></asp:Label><font
                                                                            class="FormLabel"></font></td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <font class="FormLabel">ที่อยู่ :
                                                                            <asp:Label ID="lblCompanyAddress" runat="server" CssClass="FormFld"></asp:Label></font></td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <font class="FormLabel">จังหวัด : </font>
                                                                        <asp:Label ID="lblCompanyProvice" runat="server" CssClass="FormFld"></asp:Label>
                                                                        <font class="FormLabel">ประเทศ :</font>
                                                                        <asp:Label ID="lblCompanyCountry" runat="server" CssClass="FormFld"></asp:Label></td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <font class="FormLabel">โทรศัพท์ : </font>
                                                                        <asp:Label ID="lblCompanyPhone" runat="server" CssClass="FormFld"></asp:Label><font
                                                                            class="FormLabel">&#160;&nbsp; โทรสาร :</font>
                                                                        <asp:Label ID="lblCompanyFax" runat="server" CssClass="FormFld"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <font class="FormLabel">บัตรประจำตัวกรรมการผู้มีอำนาจ/ผู้รับมอบอำนาจ เลขที่ :</font>
                                                                        <asp:Label ID="lblCardID" runat="server" CssClass="FormFld"></asp:Label>
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
            <table width="100%" border="0" cellspacing="3" cellpadding="0">
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
                                                        <td width="28%" align="left" class="m-frm-hdr">
                                                            <table cols="2" border="0" cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <td nowrap class="groupboxheader">
                                                                        <font class="groupbox">&#160;&#160; ผู้ซื้อหรือผู้รับประเทศปลายทาง&#160;</font></td>
                                                                    <telerik:RadCodeBlock ID="RadCodeBlock4" runat="server">
                                                                        <td style="background-image: url(<%= ResolveUrl("images") %>/CORNER.gif); background-repeat: no-repeat;">
                                                                            &#160;&#160;&#160;&#160;</td>
                                                                    </telerik:RadCodeBlock>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td width="72%" align="left" class="m-frm-nav">
                                                            <font class="FormLabel">( ชื่อ ที่อยู่ ปลายทาง )</font></td>
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
                                                            <table width="100%" border="0" cellspacing="2" cellpadding="0">
                                                                <tr>
                                                                    <td>
                                                                        <font class="FormLabel">บริษัทผู้ซื้อหรือผู้รับ</font>
                                                                        <asp:Label ID="lblDestinationCompany" runat="server" CssClass="FormFld"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <font class="FormLabel">ที่อยู่</font>
                                                                        <asp:Label ID="lblDestinationAddress" runat="server" CssClass="FormFld"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <font class="FormLabel">เมือง</font>
                                                                        <asp:Label ID="lblDestinationProvince" runat="server" CssClass="FormFld"></asp:Label>
                                                                        <font class="FormLabel">ประเทศ</font>
                                                                        <asp:Label ID="lblDestReceiveCountry" runat="server" CssClass="FormFld"></asp:Label></td>
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
            <table width="100%" border="0" cellspacing="3" cellpadding="0">
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
                                                        <td align="left" class="m-frm-hdr">
                                                            <table cols="2" border="0" cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <td nowrap class="groupboxheader">
                                                                        <font class="groupbox">&#160;&#160; ยานพาหนะที่ส่งออก&#160;&#160;</font></td>
                                                                    <telerik:RadCodeBlock ID="RadCodeBlock5" runat="server">
                                                                        <td style="background-image: url(<%= ResolveUrl("images") %>/CORNER.gif); background-repeat: no-repeat;">
                                                                            &#160;&#160;&#160;&#160;</td>
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
                                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                    <tr>
                                                        <td width="15">
                                                            &nbsp;</td>
                                                        <td>
                                                            <table width="100%" border="0" cellspacing="2" cellpadding="0">
                                                                <tr>
                                                                    <td>
                                                                        <asp:RadioButtonList ID="radShipBy" runat="server" class="FormLabel" Width="600px"
                                                                            RepeatColumns="5" TabIndex="16" Enabled="False">
                                                                            <asp:ListItem Value="0" Selected="True">เรือ</asp:ListItem>
                                                                            <asp:ListItem Value="1">เครื่องบิน</asp:ListItem>
                                                                            <asp:ListItem Value="2">ทางบก</asp:ListItem>
                                                                            <asp:ListItem Value="3">ไปรษณีย์</asp:ListItem>
                                                                            <asp:ListItem Value="4">นำติดตัว</asp:ListItem>
                                                                        </asp:RadioButtonList></td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <font class="FormLabel">Means of transport and route (as far as known)</font>
                                                                        <asp:Label ID="lblTransportBy" runat="server" CssClass="FormFld"></asp:Label></td>
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
            <table width="100%" border="0" cellspacing="3" cellpadding="0">
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
                                                        <td align="left" class="m-frm-hdr">
                                                            <table cols="2" border="0" cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <td nowrap class="groupboxheader">
                                                                        <font class="groupbox">&#160;&#160;ใบกำกับสินค้า &#160;&#160;</font></td>
                                                                    <telerik:RadCodeBlock ID="RadCodeBlock6" runat="server">
                                                                        <td style="background-image: url(<%= ResolveUrl("images") %>/CORNER.gif); background-repeat: no-repeat;">
                                                                            &#160;&#160;&#160;&#160;</td>
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
                                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                    <tr>
                                                        <td width="15">
                                                            &nbsp;</td>
                                                        <td style="text-align: left">
                                                            <table width="100%" border="0" cellspacing="2" cellpadding="0" style="text-align: left">
                                                                <tr>
                                                                    <td style="text-align: left">
                                                                        <font class="FormLabel">Invoice ต่างประเทศ(ถ้ามี)</font>
                                                                        <asp:Label ID="lblInvoiceBoard" runat="server" CssClass="FormFld"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="text-align: left">
                                                                        <table>
                                                                            <tr>
                                                                                <td align="right">
                                                                                    <font class="FormLabel">ใบกำกับสินค้า เลขที่</font>
                                                                                    <asp:Label ID="lblInvoiceNo1" runat="server" CssClass="FormFld"></asp:Label>
                                                                                    <font class="FormLabel">&nbsp; ลงวันที่</font>
                                                                                </td>
                                                                                <td>
                                                                                    <telerik:RadDatePicker ID="txtInvoiceDate1" runat="server" Skin="Office2007" TabIndex="32"
                                                                                        Culture="English (United Kingdom)" Enabled="False">
                                                                                        <Calendar Skin="Office2007" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                                                            ViewSelectorText="x" DayNameFormat="FirstTwoLetters" FirstDayOfWeek="Sunday"
                                                                                            ShowRowHeaders="False">
                                                                                        </Calendar>
                                                                                        <DatePopupButton HoverImageUrl="" ImageUrl="" TabIndex="32" />
                                                                                        <DateInput TabIndex="32">
                                                                                        </DateInput>
                                                                                    </telerik:RadDatePicker>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="text-align: left">
                                                                        <table>
                                                                            <tr>
                                                                                <td class="FormLabel" align="right">
                                                                                    ใบกำกับสินค้า เลขที่
                                                                                    <asp:Label ID="lblInvoiceNo2" runat="server" CssClass="FormFld"></asp:Label><font
                                                                                        class="FormLabel">&#160; ลงวันที่</font>
                                                                                </td>
                                                                                <td>
                                                                                    <telerik:RadDatePicker ID="txtInvoiceDate2" runat="server" Skin="Office2007" TabIndex="34"
                                                                                        Culture="English (United Kingdom)" Enabled="False">
                                                                                        <Calendar Skin="Office2007" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                                                            ViewSelectorText="x" DayNameFormat="FirstTwoLetters" FirstDayOfWeek="Sunday"
                                                                                            ShowRowHeaders="False">
                                                                                        </Calendar>
                                                                                        <DatePopupButton HoverImageUrl="" ImageUrl="" TabIndex="34" />
                                                                                        <DateInput TabIndex="34">
                                                                                        </DateInput>
                                                                                    </telerik:RadDatePicker>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="text-align: left">
                                                                        <table>
                                                                            <tr>
                                                                                <td class="FormLabel" align="right">
                                                                                    ใบกำกับสินค้า เลขที่
                                                                                    <asp:Label ID="lblInvoiceNo3" runat="server" CssClass="FormFld"></asp:Label><font
                                                                                        class="FormLabel">&#160; ลงวันที่ </font>
                                                                                </td>
                                                                                <td>
                                                                                    <telerik:RadDatePicker ID="txtInvoiceDate3" runat="server" Skin="Office2007" TabIndex="36"
                                                                                        Culture="English (United Kingdom)" Enabled="False">
                                                                                        <Calendar Skin="Office2007" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                                                            ViewSelectorText="x" DayNameFormat="FirstTwoLetters" FirstDayOfWeek="Sunday"
                                                                                            ShowRowHeaders="False">
                                                                                        </Calendar>
                                                                                        <DatePopupButton HoverImageUrl="" ImageUrl="" TabIndex="36" />
                                                                                        <DateInput TabIndex="36">
                                                                                        </DateInput>
                                                                                    </telerik:RadDatePicker>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="text-align: left">
                                                                        <table>
                                                                            <tr>
                                                                                <td align="right">
                                                                                    <font class="FormLabel">ใบกำกับสินค้า เลขที่</font>
                                                                                    <asp:Label ID="lblInvoiceNo4" runat="server" CssClass="FormFld"></asp:Label><font
                                                                                        class="FormLabel">&#160; ลงวันที่</font></td>
                                                                                <td>
                                                                                    <telerik:RadDatePicker ID="txtInvoiceDate4" runat="server" Skin="Office2007" TabIndex="38"
                                                                                        Culture="English (United Kingdom)" Enabled="False">
                                                                                        <Calendar Skin="Office2007" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                                                            ViewSelectorText="x" DayNameFormat="FirstTwoLetters" FirstDayOfWeek="Sunday"
                                                                                            ShowRowHeaders="False">
                                                                                        </Calendar>
                                                                                        <DatePopupButton HoverImageUrl="" ImageUrl="" TabIndex="38" />
                                                                                        <DateInput TabIndex="38">
                                                                                        </DateInput>
                                                                                    </telerik:RadDatePicker>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="text-align: left">
                                                                        <table>
                                                                            <tr>
                                                                                <td align="right">
                                                                                    <font class="FormLabel">ใบกำกับสินค้า เลขที่</font>
                                                                                    <asp:Label ID="lblInvoiceNo5" runat="server" CssClass="FormFld"></asp:Label><font
                                                                                        class="FormLabel">&#160; ลงวันที่</font></td>
                                                                                <td>
                                                                                    <telerik:RadDatePicker ID="txtInvoiceDate5" runat="server" Skin="Office2007" TabIndex="40"
                                                                                        Culture="English (United Kingdom)" Enabled="False">
                                                                                        <Calendar Skin="Office2007" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                                                            ViewSelectorText="x" DayNameFormat="FirstTwoLetters" FirstDayOfWeek="Sunday"
                                                                                            ShowRowHeaders="False">
                                                                                        </Calendar>
                                                                                        <DatePopupButton HoverImageUrl="" ImageUrl="" TabIndex="40" />
                                                                                        <DateInput TabIndex="40">
                                                                                        </DateInput>
                                                                                    </telerik:RadDatePicker>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                                            <tr>
                                                                                <td align="left">
                                                                                    <font class="FormLabel">&#160;ใบตราส่งสินค้า</font></td>
                                                                                <td align="left">
                                                                                    <asp:RadioButtonList ID="radBillType" runat="server" Width="400px" Class="FormLabel"
                                                                                        RepeatColumns="4" TabIndex="41" Enabled="False">
                                                                                        <asp:ListItem Value="0" Selected="True">B/L</asp:ListItem>
                                                                                        <asp:ListItem Value="1">AWB</asp:ListItem>
                                                                                        <asp:ListItem Value="2">ใบรับไปรษณีย์</asp:ListItem>
                                                                                        <asp:ListItem Value="3">อื่นๆ</asp:ListItem>
                                                                                    </asp:RadioButtonList></td>
                                                                                <td align="left">
                                                                                    <asp:Label ID="lblBillTypeOther" runat="server" CssClass="FormFld"></asp:Label></td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <table>
                                                                            <tr>
                                                                                <td class="FormLabel" align="right">
                                                                                    เลขที่ &nbsp;
                                                                                    <asp:Label ID="lblBlNo" runat="server" CssClass="FormFld"></asp:Label>
                                                                                    <font class="FormLabel">&#160;วันที่</font>
                                                                                </td>
                                                                                <td>
                                                                                    <telerik:RadDatePicker ID="txtSailingDate" runat="server" Skin="Office2007" TabIndex="43"
                                                                                        Culture="English (United Kingdom)" Enabled="False">
                                                                                        <Calendar Skin="Office2007" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                                                            ViewSelectorText="x" DayNameFormat="FirstTwoLetters" FirstDayOfWeek="Sunday"
                                                                                            ShowRowHeaders="False">
                                                                                        </Calendar>
                                                                                        <DatePopupButton HoverImageUrl="" ImageUrl="" TabIndex="43" />
                                                                                        <DateInput TabIndex="43">
                                                                                        </DateInput>
                                                                                    </telerik:RadDatePicker>
                                                                                </td>
                                                                                <td align="right" class="FormLabel">
                                                                                    วันที่ส่งออก</td>
                                                                                <td>
                                                                                    <telerik:RadDatePicker ID="txtEdiDate" runat="server" Skin="Office2007" TabIndex="44"
                                                                                        Culture="English (United Kingdom)" Enabled="False">
                                                                                        <Calendar Skin="Office2007" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                                                            ViewSelectorText="x" DayNameFormat="FirstTwoLetters" FirstDayOfWeek="Sunday"
                                                                                            ShowRowHeaders="False">
                                                                                        </Calendar>
                                                                                        <DatePopupButton HoverImageUrl="" ImageUrl="" TabIndex="44" />
                                                                                        <DateInput TabIndex="44">
                                                                                        </DateInput>
                                                                                    </telerik:RadDatePicker>
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
            <table width="100%" border="0" cellspacing="3" cellpadding="0">
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
                                                        <td width="38%" align="left" class="m-frm-hdr">
                                                            <table cols="2" border="0" cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <td nowrap class="groupboxheader">
                                                                        <font class="groupbox">&#160;&#160;เอกสารแนบ &#160;&#160;</font></td>
                                                                    <telerik:RadCodeBlock ID="RadCodeBlock7" runat="server">
                                                                        <td style="background-image: url(<%= ResolveUrl("images") %>/CORNER.gif); background-repeat: no-repeat;">
                                                                            &#160;&#160;&#160;&#160;</td>
                                                                    </telerik:RadCodeBlock>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td width="62%" align="left">
                                                            &nbsp;</td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="m-frm" valign="top" width="100%">
                                                <table width="100%" border="0" cellspacing="2" cellpadding="0">
                                                    <tr>
                                                        <td width="25">
                                                            &nbsp;</td>
                                                        <td>
                                                            <font class="FormLabel">เอกสารที่แนบประกอบการพิจารณา</font>
                                                            <asp:Label ID="lblAttachFile" runat="server" CssClass="FormFld"></asp:Label>
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
            <table width="100%" border="0" cellspacing="3" cellpadding="0">
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
                                                        <td align="left" class="m-frm-hdr">
                                                            <table cols="2" border="0" cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <td nowrap class="groupboxheader">
                                                                        <font class="groupbox">&#160;&#160;แหล่งที่ผลิต &#160;&#160;</font></td>
                                                                    <telerik:RadCodeBlock ID="RadCodeBlock8" runat="server">
                                                                        <td style="background-image: url(<%= ResolveUrl("images") %>/CORNER.gif); background-repeat: no-repeat;">
                                                                            &#160;&#160;&#160;&#160;</td>
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
                                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                    <tr>
                                                        <td width="25">
                                                            &nbsp;</td>
                                                        <td>
                                                            <table width="100%" border="0" cellspacing="2" cellpadding="0">
                                                                <tr>
                                                                    <td>
                                                                        <font class="FormLabel">ชื่อโรงงาน</font>
                                                                        <asp:Label ID="lblFactory" runat="server" CssClass="FormFld"></asp:Label>
                                                                        <font class="FormLabel">เลขประจำตัวผู้เสียภาษี</font>
                                                                        <asp:Label ID="lblFactoryTaxID" runat="server" CssClass="FormFld"></asp:Label></td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <font class="FormLabel">ตั้งอยู่ที่</font>
                                                                        <asp:Label ID="lblFactoryAddress" runat="server" CssClass="FormFld"></asp:Label></td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <font class="FormLabel">จังหวัด</font>
                                                                        <asp:Label ID="lblFactoryProvince" runat="server" CssClass="FormFld"></asp:Label>
                                                                        <font class="FormLabel">&#160;ประเทศ</font>
                                                                        <asp:Label ID="lblFactoryCountry" runat="server" CssClass="FormFld"></asp:Label></td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <font class="FormLabel">โทรศัพท์</font>
                                                                        <asp:Label ID="lblFactoryPhone" runat="server" CssClass="FormFld"></asp:Label>
                                                                        <font class="FormLabel">&#160;โทรสาร</font>
                                                                        <asp:Label ID="lblFactoryFax" runat="server" CssClass="FormFld"></asp:Label>
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
        <td valign="top">
            <table width="100%" border="0" cellspacing="3" cellpadding="0">
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
                                                        <td width="88%" align="left" class="m-frm-hdr">
                                                            <table cols="2" border="0" cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <td nowrap class="groupboxheader">
                                                                        <font class="groupbox">&#160;&#160;สถานะ&#160;&#160;</font></td>
                                                                    <telerik:RadCodeBlock ID="RadCodeBlock9" runat="server">
                                                                        <td style="background-image: url(<%= ResolveUrl("images") %>/CORNER.gif); background-repeat: no-repeat;">
                                                                            &#160;&#160;&#160;&#160;</td>
                                                                    </telerik:RadCodeBlock>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td width="12%" align="left" class="m-frm-nav">
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
                                                            <table width="300" border="0" cellspacing="2" cellpadding="0">
                                                                <tr>
                                                                    <td style="text-align: center">
                                                                        <asp:Label ID="lblStatus" runat="server" CssClass="FormFld"></asp:Label></td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="text-align: center">
                                                                        <asp:Label ID="lblRep_Status" runat="server" CssClass="FormFld"></asp:Label></td>
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
            <table width="100%" border="0" cellspacing="3" cellpadding="0">
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
                                                        <td width="88%" align="left" class="m-frm-hdr">
                                                            <table cols="2" border="0" cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <td nowrap class="groupboxheader">
                                                                        <font class="groupbox">&#160;&#160;รายละเอียดหนังสือรับรอง&#160;&#160;</font></td>
                                                                    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
                                                                        <td style="background-image: url(<%= ResolveUrl("images") %>/CORNER.gif); background-repeat: no-repeat;">
                                                                            &#160;&#160;&#160;&#160;</td>
                                                                    </telerik:RadCodeBlock>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td width="12%" align="left" class="m-frm-nav">
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
                                                            <table width="100%" border="0" cellspacing="2" cellpadding="0">
                                                                <tr>
                                                                    <td>
                                                                        <font class="FormLabel">เลขที่อ้างอิง :</font>
                                                                        <asp:Label ID="lblInvh_Run_Auto" runat="server" CssClass="FormFld"></asp:Label></td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <font class="FormLabel">เลขที่หนังสือรับรอง :</font>
                                                                        <asp:Label ID="lblReferenceCode2" runat="server" CssClass="FormFld"></asp:Label></td>
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
            <table width="100%" border="0" cellspacing="3" cellpadding="0">
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
                                                        <td width="88%" align="left" class="m-frm-hdr">
                                                            <table cols="2" border="0" cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <td nowrap class="groupboxheader">
                                                                        <font class="groupbox">&#160;&#160;ส่งแก้ไขเอกสาร&#160;&#160;</font></td>
                                                                    <telerik:RadCodeBlock ID="RadCodeBlock10" runat="server">
                                                                        <td style="background-image: url(<%= ResolveUrl("images") %>/CORNER.gif); background-repeat: no-repeat;">
                                                                            &#160;&#160;&#160;&#160;</td>
                                                                    </telerik:RadCodeBlock>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td width="12%" align="left" class="m-frm-nav">
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
                                                            <table width="100%" border="0" cellspacing="2" cellpadding="0">
                                                                <tr>
                                                                    <td style="text-align: center">
                                                                        <asp:Button ID="btnEditDocAttach" runat="server" Text="ส่งแก้ไขเอกสาร" Width="200px" Font-Names="Tahoma" Font-Size="10pt" /></td>
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
<asp:Label ID="lblRoleID" runat="server" Visible="False"></asp:Label>