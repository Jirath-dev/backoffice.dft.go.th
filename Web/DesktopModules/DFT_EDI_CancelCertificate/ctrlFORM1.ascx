<%@ Control Language="vb" AutoEventWireup="false" Codebehind="ctrlFORM1.ascx.vb"
    Inherits=".ctrlFORM1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="RadAjaxManager1">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="grdItemData" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="dropDestRemark">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="dropDestRemark" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="dropDestinationCountry">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="dropDestinationCountry" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="grdItemData">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="grdItemData" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="drpG_UNIT_CODE">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="drpG_UNIT_CODE" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="drpQ_UNIT_CODE1">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="drpQ_UNIT_CODE1" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="drpQ_UNIT_CODE2">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="drpQ_UNIT_CODE2" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="drpQ_UNIT_CODE3">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="drpQ_UNIT_CODE3" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="drpQ_UNIT_CODE4">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="drpQ_UNIT_CODE4" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="drpQ_UNIT_CODE5">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="drpQ_UNIT_CODE5" />
            </UpdatedControls>
        </telerik:AjaxSetting>
    </AjaxSettings>
</telerik:RadAjaxManager>
<telerik:RadCodeBlock ID="RadCodeBlockFirst" runat="server">
    <script type="text/javascript">
        function ShowViewForm(invh_run_auto, invd_run_auto, action)
        {
            window.radopen("/DesktopModules/DFT_EDI_CancelCertificate/Popup/FormItem1.aspx?InvHRunAuto=" + invh_run_auto + "&InvDRunAuto=" + invd_run_auto + "&action=" + action, "ViewDialog");
            return false;
        }

        function onFocus(sender, eventArgs){
            $find("<%= txtOB_Address.ClientID %>").focus();
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
<telerik:RadWindowManager ID="RadWindowManager1" Behavior="Close" runat="server" Skin="Office2007" TabIndex="-1">
    <Windows>
        <telerik:RadWindow ID="ViewDialog" runat="server" Title="แสดงรายการสินค้า" ReloadOnShow="True" ShowContentDuringLoad="False" Modal="True"
        VisibleStatusbar="False" Width="800px" Height="450px" />
    </Windows>
</telerik:RadWindowManager>
<table width="100%" border="0" cellspacing="2" cellpadding="0">
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
                                                            <font class="groupbox">&#160;&#160;1. ผู้ขอ&#160;&#160;</font></td>
                                                        <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
                                                            <td style="background-image: url(<%= ResolveUrl("images") %>/CORNER.gif); background-repeat: no-repeat;">
                                                                &#160;&#160;&#160;&#160;</td>
                                                        </telerik:RadCodeBlock>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td width="88%" align="left" class="m-frm-nav">
                                                &nbsp;<asp:TextBox ID="txtInvHRunAuto" runat="server" Visible="False" Width="1px"></asp:TextBox></td>
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
                                                            <font class="FormLabel">ชื่อ&nbsp;<telerik:RadTextBox ID="txtRequestPerson" runat="server"
                                                                CssClass="FormFld" EnableEmbeddedSkins="False" ReadOnly="True" TabIndex="-1"
                                                                Width="315px">
                                                            </telerik:RadTextBox></font> <font class="FormLabel">&nbsp; ในนามของ</font>
                                                            <telerik:RadTextBox ID="txtCompanyName" runat="server" CssClass="FormFld" EnableEmbeddedSkins="False"
                                                                ReadOnly="True" TabIndex="-1" Width="330px">
                                                            </telerik:RadTextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <font class="FormLabel">เลขประจำตัวผู้เสียภาษี</font> &nbsp;<telerik:RadTextBox ID="txtCompanyTaxNo"
                                                                runat="server" CssClass="FormFld" EnableEmbeddedSkins="False" ReadOnly="True"
                                                                TabIndex="-1" Width="210px">
                                                            </telerik:RadTextBox><font class="FormLabel">&#160;&nbsp;</font><telerik:RadTextBox
                                                                ID="txtFormNo" runat="server" CssClass="FormFld" EnableEmbeddedSkins="False"
                                                                TabIndex="-1" Width="210px" Visible="False" ReadOnly="True">
                                                            </telerik:RadTextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <font class="FormLabel">ที่อยู่&nbsp;<telerik:RadTextBox ID="txtCompanyAddress" runat="server"
                                                                CssClass="FormFld" EnableEmbeddedSkins="False" ReadOnly="True" TabIndex="-1"
                                                                Width="727px">
                                                            </telerik:RadTextBox></font>
                                                            <asp:TextBox ID="txtCompanyProvince" runat="server" Visible="False" TabIndex="-1" />
                                                            <asp:TextBox ID="txtCompanyCountry" runat="server" Visible="False" TabIndex="-1" /></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <font class="FormLabel">โทรศัพท์</font> &nbsp;<telerik:RadTextBox ID="txtCompanyPhone"
                                                                runat="server" CssClass="FormFld" EnableEmbeddedSkins="False" ReadOnly="True"
                                                                TabIndex="-1" Width="280px">
                                                            </telerik:RadTextBox><font class="FormLabel">&#160;&nbsp; โทรสาร</font>&nbsp;<telerik:RadTextBox
                                                                ID="txtCompanyFax" runat="server" CssClass="FormFld" EnableEmbeddedSkins="False"
                                                                ReadOnly="True" TabIndex="-1" Width="345px">
                                                            </telerik:RadTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <font class="FormLabel">
                                                                <table cellpadding="0" cellspacing="0">
                                                                    <tr>
                                                                        <td colspan="2">
                                                                            <font class="FormLabel">บัตรประจำตัวกรรมการผู้มีอำนาจ/ผู้รับมอบอำนาจ เลขที่ &nbsp;</font></td>
                                                                        <td>
                                                                            <telerik:RadTextBox ID="txtCardID" runat="server" CssClass="FormFld" EnableEmbeddedSkins="False"
                                                                                ReadOnly="True" TabIndex="-1" Width="172px">
                                                                            </telerik:RadTextBox></td>
                                                                        <td>
                                                                            &nbsp; &#160;<font class="FormLabel"> O/B หรือ C/O&nbsp; </font>
                                                                        </td>
                                                                        <td>
                                                                            <telerik:RadComboBox ID="dropDestRemark" runat="server" Skin="Office2007" Width="114px">
                                                                                <Items>
                                                                                    <telerik:RadComboBoxItem runat="server" Text="- - - - -" />
                                                                                    <telerik:RadComboBoxItem runat="server" Text="O/B" Value="O/B" />
                                                                                    <telerik:RadComboBoxItem runat="server" Text="C/O" Value="C/O" />
                                                                                </Items>
                                                                            </telerik:RadComboBox>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </font>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <font class="FormLabel">ที่อยู่
                                                                <telerik:RadTextBox ID="txtOB_Address" runat="server" CssClass="FormFld" EnableEmbeddedSkins="False"
                                                                    TabIndex="1" Width="726px" ReadOnly="True">
                                                                </telerik:RadTextBox></font></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <font class="FormLabel">ชื่อผู้รับมอบอำนาจ</font> &nbsp;<telerik:RadTextBox ID="txtAuthorize"
                                                                runat="server" CssClass="FormFld" EnableEmbeddedSkins="False" ReadOnly="True"
                                                                TabIndex="-1" Width="632px">
                                                            </telerik:RadTextBox>&#160;<font class="FormLabel"></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <font class="FormLabel">Email</font> &nbsp;<telerik:RadTextBox ID="txtComapnyEmail"
                                                                runat="server" CssClass="FormFld" EnableEmbeddedSkins="False" TabIndex="2" Width="280px"
                                                                ReadOnly="True">
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
<table width="100%" border="0" cellspacing="2" cellpadding="0">
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
                                                            <font class="groupbox">&#160;&#160;2. ผู้ซื้อหรือผู้รับประเทศปลายทาง&#160;</font></td>
                                                        <telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">
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
                                                            <font class="FormLabel">บริษัทผู้ซื้อหรือผู้รับ</font>&nbsp;<telerik:RadTextBox ID="txtDestinationCompany"
                                                                runat="server" CssClass="FormFld" EnableEmbeddedSkins="False" MaxLength="255"
                                                                Width="255px" TabIndex="2" ReadOnly="True">
                                                            </telerik:RadTextBox>
                                                            &nbsp;&nbsp;<font class="FormLabel"> เลขประจำตัวผู้เสียภาษี</font>
                                                            <telerik:RadTextBox ID="txtDestinationTaxID" runat="server" CssClass="FormFld" EnableEmbeddedSkins="False"
                                                                MaxLength="30" Width="240px" TabIndex="3" ReadOnly="True">
                                                            </telerik:RadTextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <font class="FormLabel">ที่อยู่
                                                                <telerik:RadTextBox ID="txtDestinationAddress" runat="server" CssClass="FormFld"
                                                                    EnableEmbeddedSkins="False" MaxLength="255" Width="690px" TabIndex="4" ReadOnly="True">
                                                                </telerik:RadTextBox></font></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <font class="FormLabel">เมือง</font> &nbsp;<telerik:RadTextBox ID="txtDestinationProvince"
                                                                runat="server" CssClass="FormFld" EnableEmbeddedSkins="False" MaxLength="50"
                                                                Width="305px" TabIndex="5" ReadOnly="True">
                                                            </telerik:RadTextBox><font class="FormLabel">&#160; ประเทศ</font>&#160;
                                                            <telerik:RadTextBox ID="txtDestReceiveCountry" runat="server" CssClass="FormFld"
                                                                EnableEmbeddedSkins="False" MaxLength="100" Width="305px" TabIndex="6" ReadOnly="True">
                                                            </telerik:RadTextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <table cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <td>
                                                                        <font class="FormLabel">ประเทศปลายทาง</font>&nbsp;</td>
                                                                    <td>
                                                                        <telerik:RadComboBox ID="dropDestinationCountry" runat="server" AllowCustomText="True"
                                                                            EnableLoadOnDemand="True" Filter="StartsWith" MarkFirstMatch="True" Width="260px"
                                                                            Skin="Office2007" DataSourceID="SqlCountry" DataTextField="DESCRIPTION" DataValueField="CODE"
                                                                            TabIndex="7">
                                                                        </telerik:RadComboBox>
                                                                    </td>
                                                                    <td>
                                                                        &nbsp; &nbsp; <font class="FormLabel">O/B หรือ C/O</font>&nbsp;</td>
                                                                    <td>
                                                                        <telerik:RadComboBox ID="dropDestRemark1" runat="server" Skin="Office2007" Width="114px"
                                                                            TabIndex="8">
                                                                            <Items>
                                                                                <telerik:RadComboBoxItem runat="server" Text="- - - - -" />
                                                                                <telerik:RadComboBoxItem runat="server" Text="O/B" Value="O/B" />
                                                                                <telerik:RadComboBoxItem runat="server" Text="C/O" Value="C/O" />
                                                                            </Items>
                                                                        </telerik:RadComboBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <font class="FormLabel">ที่อยู่</font>
                                                            <telerik:RadTextBox ID="txtOB_Dest_Address" runat="server" CssClass="FormFld" EnableEmbeddedSkins="False"
                                                                MaxLength="255" Width="690px" TabIndex="9" ReadOnly="True">
                                                            </telerik:RadTextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <font class="FormLabel">โทรศัพท์</font> &nbsp;<telerik:RadTextBox ID="txtDestinationPhone"
                                                                runat="server" CssClass="FormFld" EnableEmbeddedSkins="False" MaxLength="50"
                                                                Width="285px" TabIndex="10" ReadOnly="True">
                                                            </telerik:RadTextBox><font class="FormLabel">&#160;&nbsp; โทรสาร</font>&nbsp;<telerik:RadTextBox
                                                                ID="txtDestinationFax" runat="server" CssClass="FormFld" EnableEmbeddedSkins="False"
                                                                MaxLength="50" Width="310px" TabIndex="11" ReadOnly="True">
                                                            </telerik:RadTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <font class="FormLabel">Email</font>
                                                            <telerik:RadTextBox ID="txtDestinationEmail" runat="server" CssClass="FormFld" EnableEmbeddedSkins="False"
                                                                MaxLength="50" Width="305px" TabIndex="12" ReadOnly="True">
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
<table width="100%" border="0" cellspacing="2" cellpadding="0">
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
                                                            <font class="groupbox">&#160;&#160;3. ยานพาหนะที่ส่งออก&#160;&#160;</font></td>
                                                        <telerik:RadCodeBlock ID="RadCodeBlock3" runat="server">
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
                                                                RepeatColumns="5" TabIndex="13">
                                                                <asp:ListItem Value="0" Selected="True">เรือ</asp:ListItem>
                                                                <asp:ListItem Value="1">เครื่องบิน</asp:ListItem>
                                                                <asp:ListItem Value="2">ทางบก</asp:ListItem>
                                                                <asp:ListItem Value="3">ไปรษณีย์</asp:ListItem>
                                                                <asp:ListItem Value="4">นำติดตัว</asp:ListItem>
                                                            </asp:RadioButtonList></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <font class="FormLabel">Means of transport and route (as far as known)</font>&nbsp;
                                                            <telerik:RadTextBox ID="txtTransportBy" runat="server" CssClass="FormFld" EnableEmbeddedSkins="False"
                                                                MaxLength="255" Width="472px" TabIndex="14" ReadOnly="True">
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
<table width="100%" border="0" cellspacing="2" cellpadding="0">
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
                                            <td width="7%" align="left" class="m-frm-hdr">
                                                <table cols="2" border="0" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td nowrap class="groupboxheader">
                                                            <font class="groupbox">&#160;&#160;4. รายการสินค้า&#160;&#160;</font></td>
                                                        <telerik:RadCodeBlock ID="RadCodeBlock4" runat="server">
                                                            <td style="background-image: url(<%= ResolveUrl("images") %>/CORNER.gif); background-repeat: no-repeat;">
                                                                &#160;&#160;&#160;&#160;</td>
                                                        </telerik:RadCodeBlock>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td width="93%" align="left">
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td class="m-frm" valign="top" width="100%">
                                    <table width="100%" border="0" cellspacing="2" cellpadding="0">
                                        <tr>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                <font class="FormLabel"> </font>
                                                <telerik:RadGrid ID="grdItemData" runat="server" AllowPaging="True" AllowSorting="True"
                                                    GridLines="None" Skin="Office2007" Font-Names="Tahoma" Font-Size="X-Small" TabIndex="-1" ShowFooter="True">
                                                    <MasterTableView TabIndex="-1" AutoGenerateColumns="False" CellSpacing="-1" Width="100%"
                                                        DataKeyNames="invh_run_auto,invd_run_auto" ClientDataKeyNames="invh_run_auto,invd_run_auto"
                                                        NoMasterRecordsText="ไม่มีรายการสินค้า">
                                                        <Columns>
                                                            <telerik:GridTemplateColumn UniqueName="TemplateViewColumn">
                                                                <ItemStyle HorizontalAlign="Center" Width="15px" />
                                                                <ItemTemplate>
                                                                    <asp:HyperLink ID="ViewLink" TabIndex="-1" runat="server" ImageUrl="~/images/view.gif"
                                                                        Text="View"></asp:HyperLink>
                                                                </ItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridBoundColumn DataField="InvH_Run_Auto" ReadOnly="True" SortExpression="InvH_Run_Auto"
                                                                UniqueName="InvH_Run_Auto" Visible="False">
                                                                <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="InvD_Run_Auto" ReadOnly="True" SortExpression="InvD_Run_Auto"
                                                                UniqueName="InvD_Run_Auto" Visible="False">
                                                                <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="product_name" ReadOnly="True" HeaderText="สินค้า"
                                                                SortExpression="product_name" UniqueName="product_name">
                                                                <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="tariff_code" ReadOnly="True" HeaderText="พิกัดสินค้า"
                                                                SortExpression="tariff_code" UniqueName="tariff_code">
                                                                <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridNumericColumn DataField="net_weight" ReadOnly="True" HeaderText="ปริมาณ/น้ำหนักสุทธิ (กก.)"
                                                                SortExpression="net_weight" UniqueName="net_weight" Aggregate="Sum" FooterText="รวม : ">
                                                                <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Right" />
                                                                <FooterStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Right" />
                                                            </telerik:GridNumericColumn>
                                                            <telerik:GridNumericColumn DataField="Fob_amt" ReadOnly="True" HeaderText="มูลค่า US$ (FOB)"
                                                                SortExpression="Fob_amt" UniqueName="Fob_amt_baht" Aggregate="Sum" FooterText="รวม : ">
                                                                <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Right" />
                                                                <FooterStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Right" />
                                                            </telerik:GridNumericColumn>
                                                        </Columns>
                                                    </MasterTableView>
                                                    <HeaderStyle HorizontalAlign="Center" Font-Names="Tahoma" Font-Size="10pt" />
                                                    <ClientSettings>
                                                        <Selecting AllowRowSelect="True" />
                                                    </ClientSettings>
                                                </telerik:RadGrid></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                <font class="FormLabel"><font class="FormLabel">
                                                    <table cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td class="FormLabel">
                                                                น้ำหนักรวม (Gross Weight)&nbsp;</td>
                                                            <td>
                                                                <telerik:RadNumericTextBox ID="txtGrossWeight" runat="server" CssClass="FormFld"
                                                                    Culture="(Default)" EnableEmbeddedSkins="False" ForeColor="Blue" MaxLength="20"
                                                                    MaxValue="1000000" MinValue="-1000000" ShowSpinButtons="False" Type="Currency"
                                                                    Width="180px" TabIndex="13">
                                                                </telerik:RadNumericTextBox></td>
                                                            <td>
                                                                &nbsp;<font class="FormLabel">&#160; หน่วย</font>&nbsp;</td>
                                                            <td>
                                                                <telerik:RadComboBox ID="drpG_UNIT_CODE" runat="server" EnableLoadOnDemand="True"
                                                                    Filter="StartsWith" MarkFirstMatch="True" Width="100px" Skin="Office2007" DataSourceID="Sqlunit"
                                                                    DataTextField="DESCRIPTION" DataValueField="CODE" TabIndex="14">
                                                                </telerik:RadComboBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </font></font>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                                <table cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td colspan="4">
                                                            <font class="FormLabel">เลือกประเภทของน้ำหนักรวมที่จะแสดงในช่องที่ 9</font>&nbsp;
                                                        </td>
                                                        <td colspan="1">
                                                            <telerik:RadComboBox ID="dropWeightDisplay" runat="server" Skin="Office2007" TabIndex="15">
                                                                <Items>
                                                                    <telerik:RadComboBoxItem runat="server" Text="GROSS WEIGHT" Value="GW" />
                                                                    <telerik:RadComboBoxItem runat="server" Text="NET WEIGHT" Value="NW" />
                                                                    <telerik:RadComboBoxItem runat="server" Text="GROSS WEIGHT - NET WEIGHT" Value="GW-NW" />
                                                                </Items>
                                                            </telerik:RadComboBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                                <table cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td>
                                                            <font class="FormLabel">ปริมาณ&nbsp;</font></td>
                                                        <td>
                                                            <telerik:RadNumericTextBox ID="txtQuantity1" runat="server" CssClass="FormFld" Culture="(Default)"
                                                                EnableEmbeddedSkins="False" ForeColor="Blue" MaxLength="20" MaxValue="1000000"
                                                                MinValue="-1000000" ShowSpinButtons="False" Type="Currency" Width="215px" TabIndex="16">
                                                            </telerik:RadNumericTextBox></td>
                                                        <td>
                                                            <font class="FormLabel">&nbsp;&nbsp; หน่วย</font>&nbsp;</td>
                                                        <td>
                                                            <telerik:RadComboBox ID="drpQ_UNIT_CODE1" runat="server" EnableLoadOnDemand="True"
                                                                Filter="StartsWith" MarkFirstMatch="True" Width="100px" Skin="Office2007" DataSourceID="Sqlunit"
                                                                DataTextField="DESCRIPTION" DataValueField="CODE" TabIndex="17">
                                                            </telerik:RadComboBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                <table cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td class="FormLabel">
                                                            ปริมาณ&nbsp;</td>
                                                        <td>
                                                            <telerik:RadNumericTextBox ID="txtQuantity2" runat="server" CssClass="FormFld" Culture="(Default)"
                                                                EnableEmbeddedSkins="False" ForeColor="Blue" MaxLength="20" MaxValue="1000000"
                                                                MinValue="-1000000" ShowSpinButtons="False" Type="Currency" Width="215px" TabIndex="18">
                                                            </telerik:RadNumericTextBox></td>
                                                        <td>
                                                            &nbsp;<font class="FormLabel">&#160; หน่วย</font>&nbsp;</td>
                                                        <td>
                                                            <telerik:RadComboBox ID="drpQ_UNIT_CODE2" runat="server" EnableLoadOnDemand="True"
                                                                Filter="StartsWith" MarkFirstMatch="True" Width="100px" Skin="Office2007" DataSourceID="Sqlunit"
                                                                DataTextField="DESCRIPTION" DataValueField="CODE" TabIndex="19">
                                                            </telerik:RadComboBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                <table cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td class="FormLabel">
                                                            ปริมาณ&nbsp;</td>
                                                        <td>
                                                            <telerik:RadNumericTextBox ID="txtQuantity3" runat="server" CssClass="FormFld" Culture="(Default)"
                                                                EnableEmbeddedSkins="False" ForeColor="Blue" MaxLength="20" MaxValue="1000000"
                                                                MinValue="-1000000" ShowSpinButtons="False" Type="Currency" Width="215px" TabIndex="20">
                                                            </telerik:RadNumericTextBox></td>
                                                        <td>
                                                            &nbsp;<font class="FormLabel">&#160; หน่วย</font>&nbsp;</td>
                                                        <td>
                                                            <telerik:RadComboBox ID="drpQ_UNIT_CODE3" runat="server" EnableLoadOnDemand="True"
                                                                Filter="StartsWith" MarkFirstMatch="True" Width="100px" Skin="Office2007" DataSourceID="Sqlunit"
                                                                DataTextField="DESCRIPTION" DataValueField="CODE" TabIndex="21">
                                                            </telerik:RadComboBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                <table cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td class="FormLabel">
                                                            ปริมาณ&nbsp;</td>
                                                        <td>
                                                            <telerik:RadNumericTextBox ID="txtQuantity4" runat="server" CssClass="FormFld" Culture="(Default)"
                                                                EnableEmbeddedSkins="False" ForeColor="Blue" MaxLength="20" MaxValue="1000000"
                                                                MinValue="-1000000" ShowSpinButtons="False" Type="Currency" Width="215px" TabIndex="22">
                                                            </telerik:RadNumericTextBox></td>
                                                        <td>
                                                            &nbsp;<font class="FormLabel">&#160; หน่วย</font>&nbsp;</td>
                                                        <td>
                                                            <telerik:RadComboBox ID="drpQ_UNIT_CODE4" runat="server" EnableLoadOnDemand="True"
                                                                Filter="StartsWith" MarkFirstMatch="True" Width="100px" Skin="Office2007" DataSourceID="Sqlunit"
                                                                DataTextField="DESCRIPTION" DataValueField="CODE" TabIndex="23">
                                                            </telerik:RadComboBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                <table cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td class="FormLabel">
                                                            ปริมาณ&nbsp;</td>
                                                        <td>
                                                            <telerik:RadNumericTextBox ID="txtQuantity5" runat="server" CssClass="FormFld" Culture="(Default)"
                                                                EnableEmbeddedSkins="False" ForeColor="Blue" MaxLength="20" MaxValue="1000000"
                                                                MinValue="-1000000" ShowSpinButtons="False" Type="Currency" Width="215px" TabIndex="24">
                                                            </telerik:RadNumericTextBox></td>
                                                        <td>
                                                            &nbsp;<font class="FormLabel">&#160; หน่วย</font>&nbsp;</td>
                                                        <td>
                                                            <telerik:RadComboBox ID="drpQ_UNIT_CODE5" runat="server" EnableLoadOnDemand="True"
                                                                Filter="StartsWith" MarkFirstMatch="True" Width="100px" Skin="Office2007" DataSourceID="Sqlunit"
                                                                DataTextField="DESCRIPTION" DataValueField="CODE" TabIndex="25">
                                                            </telerik:RadComboBox>
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
<table width="100%" border="0" cellspacing="2" cellpadding="0">
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
                                                            <font class="groupbox">&#160;&#160;5.ใบกำกับสินค้า &#160;&#160;</font></td>
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
                                                            <table border="0" cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <td>
                                                                        <font class="FormLabel">ใบกำกับสินค้า&#160;&#160; เลขที่</font>&nbsp;<telerik:RadTextBox
                                                                            ID="txtInvoiceNo1" runat="server" CssClass="FormFld" EnableEmbeddedSkins="False"
                                                                            MaxLength="35" Width="402px" TabIndex="28" ReadOnly="True">
                                                                        </telerik:RadTextBox>&nbsp;<font class="FormLabel"> &nbsp; &nbsp;ลงวันที่</font>&nbsp;</td>
                                                                    <td>
                                                                        &nbsp;<telerik:RadDatePicker ID="txtInvoiceDate1" runat="server" Skin="Office2007"
                                                                            Culture="English (United Kingdom)" TabIndex="29">
                                                                            <Calendar Skin="Office2007" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                                                ViewSelectorText="x" DayNameFormat="FirstTwoLetters" FirstDayOfWeek="Sunday"
                                                                                ShowRowHeaders="False">
                                                                            </Calendar>
                                                                            <DatePopupButton HoverImageUrl="" ImageUrl="" TabIndex="29" />
                                                                            <DateInput TabIndex="29">
                                                                            </DateInput>
                                                                            <ClientEvents OnPopupOpening="InitializePopup" />
                                                                        </telerik:RadDatePicker>
                                                                        &nbsp;</td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <table border="0" cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <td>
                                                                        <font class="FormLabel">ใบกำกับสินค้า&#160;&#160; เลขที่</font>&nbsp;<telerik:RadTextBox
                                                                            ID="txtInvoiceNo2" runat="server" CssClass="FormFld" EnableEmbeddedSkins="False"
                                                                            MaxLength="35" Width="402px" TabIndex="30" ReadOnly="True">
                                                                        </telerik:RadTextBox><font class="FormLabel">&#160; &#160;&#160; ลงวันที่</font>&nbsp;</td>
                                                                    <td>
                                                                        &nbsp;<telerik:RadDatePicker ID="txtInvoiceDate2" runat="server" Skin="Office2007"
                                                                            Culture="English (United Kingdom)" TabIndex="31">
                                                                            <Calendar Skin="Office2007" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                                                ViewSelectorText="x" DayNameFormat="FirstTwoLetters" FirstDayOfWeek="Sunday"
                                                                                ShowRowHeaders="False">
                                                                            </Calendar>
                                                                            <DatePopupButton HoverImageUrl="" ImageUrl="" TabIndex="31" />
                                                                            <DateInput TabIndex="31">
                                                                            </DateInput>
                                                                            <ClientEvents OnPopupOpening="InitializePopup" />
                                                                        </telerik:RadDatePicker>
                                                                        &nbsp;</td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <table border="0" cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <td>
                                                                        <font class="FormLabel">ใบกำกับสินค้า&#160;&#160; เลขที่</font>&nbsp;<telerik:RadTextBox
                                                                            ID="txtInvoiceNo3" runat="server" CssClass="FormFld" EnableEmbeddedSkins="False"
                                                                            MaxLength="35" Width="402px" TabIndex="32" ReadOnly="True">
                                                                        </telerik:RadTextBox><font class="FormLabel">&#160; &#160;&#160; ลงวันที่</font>&nbsp;</td>
                                                                    <td>
                                                                        &nbsp;<telerik:RadDatePicker ID="txtInvoiceDate3" runat="server" Skin="Office2007"
                                                                            Culture="English (United Kingdom)" TabIndex="33">
                                                                            <Calendar Skin="Office2007" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                                                ViewSelectorText="x" DayNameFormat="FirstTwoLetters" FirstDayOfWeek="Sunday"
                                                                                ShowRowHeaders="False">
                                                                            </Calendar>
                                                                            <DatePopupButton HoverImageUrl="" ImageUrl="" TabIndex="33" />
                                                                            <DateInput TabIndex="33">
                                                                            </DateInput>
                                                                            <ClientEvents OnPopupOpening="InitializePopup" />
                                                                        </telerik:RadDatePicker>
                                                                        &nbsp;</td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <table border="0" cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <td>
                                                                        <font class="FormLabel">ใบกำกับสินค้า&#160;&#160; เลขที่</font>&nbsp;<telerik:RadTextBox
                                                                            ID="txtInvoiceNo4" runat="server" CssClass="FormFld" EnableEmbeddedSkins="False"
                                                                            MaxLength="35" Width="402px" TabIndex="34" ReadOnly="True">
                                                                        </telerik:RadTextBox><font class="FormLabel">&#160; &#160;&#160; ลงวันที่</font>&nbsp;</td>
                                                                    <td>
                                                                        &nbsp;<telerik:RadDatePicker ID="txtInvoiceDate4" runat="server" Skin="Office2007"
                                                                            Culture="English (United Kingdom)" TabIndex="35">
                                                                            <Calendar Skin="Office2007" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                                                ViewSelectorText="x" DayNameFormat="FirstTwoLetters" FirstDayOfWeek="Sunday"
                                                                                ShowRowHeaders="False">
                                                                            </Calendar>
                                                                            <DatePopupButton HoverImageUrl="" ImageUrl="" TabIndex="35" />
                                                                            <DateInput TabIndex="35">
                                                                            </DateInput>
                                                                            <ClientEvents OnPopupOpening="InitializePopup" />
                                                                        </telerik:RadDatePicker>
                                                                        &nbsp;</td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <table border="0" cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <td>
                                                                        <font class="FormLabel">ใบกำกับสินค้า&#160;&#160; เลขที่</font>&nbsp;<telerik:RadTextBox
                                                                            ID="txtInvoiceNo5" runat="server" CssClass="FormFld" EnableEmbeddedSkins="False"
                                                                            MaxLength="35" Width="402px" TabIndex="36" ReadOnly="True">
                                                                        </telerik:RadTextBox><font class="FormLabel">&#160; &#160;&#160; ลงวันที่</font>&nbsp;</td>
                                                                    <td>
                                                                        &nbsp;<telerik:RadDatePicker ID="txtInvoiceDate5" runat="server" Skin="Office2007"
                                                                            Culture="English (United Kingdom)" TabIndex="37">
                                                                            <Calendar Skin="Office2007" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                                                ViewSelectorText="x" DayNameFormat="FirstTwoLetters" FirstDayOfWeek="Sunday"
                                                                                ShowRowHeaders="False">
                                                                            </Calendar>
                                                                            <DatePopupButton HoverImageUrl="" ImageUrl="" TabIndex="37" />
                                                                            <DateInput TabIndex="37">
                                                                            </DateInput>
                                                                            <ClientEvents OnPopupOpening="InitializePopup" />
                                                                        </telerik:RadDatePicker>
                                                                        &nbsp;</td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td bordercolor="0">
                                                            <table border="0" cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <td width="105" align="left">
                                                                        <font class="FormLabel">ใบตราส่งสินค้า</font></td>
                                                                    <td width="373" align="left">
                                                                        <asp:RadioButtonList ID="radBillType" runat="server" Width="400px" Class="FormLabel"
                                                                            RepeatColumns="4" TabIndex="38">
                                                                            <asp:ListItem Value="0" Selected="True">B/L</asp:ListItem>
                                                                            <asp:ListItem Value="1">AWB</asp:ListItem>
                                                                            <asp:ListItem Value="2">ใบรับไปรษณีย์</asp:ListItem>
                                                                            <asp:ListItem Value="3">อื่นๆ</asp:ListItem>
                                                                        </asp:RadioButtonList></td>
                                                                    <td width="239" align="left">
                                                                        <asp:TextBox ID="txtBillTypeOther" runat="server" Class="FormFld" Width="210" MaxLength="20"
                                                                            TabIndex="39" ReadOnly="True" /></td>
                                                                    <tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <table border="0" cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <td>
                                                                        <font class="FormLabel">เลขที่</font>&nbsp;
                                                                        <telerik:RadTextBox ID="txtBlNo" runat="server" CssClass="FormFld" EnableEmbeddedSkins="False"
                                                                            MaxLength="35" Width="285px" TabIndex="40" ReadOnly="True">
                                                                        </telerik:RadTextBox>&nbsp;<font class="FormLabel"> วันที่</font>&nbsp;</td>
                                                                    <td>
                                                                        &nbsp;<telerik:RadDatePicker ID="txtSailingDate" runat="server" Skin="Office2007"
                                                                            Culture="English (United Kingdom)" TabIndex="41">
                                                                            <Calendar Skin="Office2007" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                                                ViewSelectorText="x" DayNameFormat="FirstTwoLetters" FirstDayOfWeek="Sunday"
                                                                                ShowRowHeaders="False">
                                                                            </Calendar>
                                                                            <DatePopupButton HoverImageUrl="" ImageUrl="" TabIndex="41" />
                                                                            <DateInput TabIndex="41">
                                                                            </DateInput>
                                                                            <ClientEvents OnPopupOpening="InitializePopup" />
                                                                        </telerik:RadDatePicker>
                                                                        &nbsp;</td>
                                                                    <td>
                                                                        &nbsp;<font class="FormLabel">&#160; วันที่ส่งออก</font>&nbsp;</td>
                                                                    <td>
                                                                        &nbsp;<telerik:RadDatePicker ID="txtEdiDate" runat="server" Skin="Office2007" Culture="English (United Kingdom)"
                                                                            TabIndex="42">
                                                                            <Calendar Skin="Office2007" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                                                ViewSelectorText="x" DayNameFormat="FirstTwoLetters" FirstDayOfWeek="Sunday"
                                                                                ShowRowHeaders="False">
                                                                            </Calendar>
                                                                            <DatePopupButton HoverImageUrl="" ImageUrl="" TabIndex="42" />
                                                                            <DateInput TabIndex="42">
                                                                            </DateInput>
                                                                            <ClientEvents OnPopupOpening="InitializePopup" />
                                                                        </telerik:RadDatePicker>
                                                                        &nbsp;</td>
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
<table width="100%" border="0" cellspacing="2" cellpadding="0">
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
                                                            <font class="groupbox">&#160;&#160;6.เอกสารที่แนบ &#160;&#160;</font></td>
                                                        <telerik:RadCodeBlock ID="RadCodeBlock6" runat="server">
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
                                                <font class="FormLabel">เอกสารที่แนบประกอบการพิจารณา</font>&nbsp;<telerik:RadTextBox
                                                    ID="txtAttachFile" runat="server" CssClass="FormFld2" EnableEmbeddedSkins="False"
                                                    MaxLength="500" Rows="3" TextMode="MultiLine" Width="500px" TabIndex="43" ReadOnly="True">
                                                </telerik:RadTextBox>
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
<table width="100%" border="0" cellspacing="2" cellpadding="0">
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
                                                            <font class="groupbox">&#160;&#160;7.แหล่งที่ผลิต &#160;&#160;</font></td>
                                                        <telerik:RadCodeBlock ID="RadCodeBlock7" runat="server">
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
                                                            <font class="FormLabel">ชื่อโรงงาน &nbsp;<telerik:RadTextBox ID="txtFactory" runat="server"
                                                                CssClass="FormFld" EnableEmbeddedSkins="False" Width="300px" TabIndex="-1" ReadOnly="True">
                                                            </telerik:RadTextBox>&#160;&nbsp;เลขประจำตัวผู้เสียภาษี</font>
                                                            <telerik:RadTextBox ID="txtFactoryTaxID" runat="server" CssClass="FormFld" EnableEmbeddedSkins="False"
                                                                MaxLength="30" Width="225px" TabIndex="-1" ReadOnly="True">
                                                            </telerik:RadTextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <font class="FormLabel">ตั้งอยู่ที่</font>
                                                            <telerik:RadTextBox ID="txtFactoryAddress" runat="server" CssClass="FormFld" EnableEmbeddedSkins="False"
                                                                MaxLength="255" Width="640px" TabIndex="-1" ReadOnly="True">
                                                            </telerik:RadTextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <font class="FormLabel">จังหวัด</font>&nbsp;<telerik:RadTextBox ID="txtFactoryProvince"
                                                                runat="server" CssClass="FormFld" EnableEmbeddedSkins="False" MaxLength="50"
                                                                Width="265px" TabIndex="-1" ReadOnly="True">
                                                            </telerik:RadTextBox><font class="FormLabel">&#160;&nbsp;ประเทศ</font>
                                                            <telerik:RadTextBox ID="txtFactoryCountry" runat="server" CssClass="FormFld" EnableEmbeddedSkins="False"
                                                                MaxLength="50" Width="303px" TabIndex="-1" ReadOnly="True">
                                                            </telerik:RadTextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <font class="FormLabel">โทรศัพท์</font>&nbsp;<telerik:RadTextBox ID="txtFactoryPhone"
                                                                runat="server" CssClass="FormFld" EnableEmbeddedSkins="False" MaxLength="50"
                                                                Width="255px" TabIndex="-1" ReadOnly="True">
                                                            </telerik:RadTextBox><font class="FormLabel">&#160;&nbsp;โทรสาร</font>&nbsp;<telerik:RadTextBox
                                                                ID="txtFactoryFax" runat="server" CssClass="FormFld" EnableEmbeddedSkins="False"
                                                                MaxLength="50" Width="305px" TabIndex="-1" ReadOnly="True">
                                                            </telerik:RadTextBox>
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
<asp:SqlDataSource ID="SqlCountry" runat="server" ConnectionString="<%$ ConnectionStrings:OriginConnection %>"
    SelectCommand="sp_common_get_countryByFormType" SelectCommandType="StoredProcedure">
    <SelectParameters>
        <asp:Parameter DefaultValue="FORM1" Name="FORM_TYPE" Type="String" />
    </SelectParameters>
</asp:SqlDataSource>
<asp:SqlDataSource ID="Sqlunit" runat="server" ConnectionString="<%$ ConnectionStrings:OriginConnection %>"
    SelectCommand="sp_common_get_unit" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
