<%@ Control Language="vb" Inherits="NTi.Modules.DFT_EDI_EntryForm.ViewDFT_EDI_EntryForm"
    AutoEventWireup="false" Explicit="True" Codebehind="ViewDFT_EDI_EntryForm.ascx.vb" %>
<%@ Register Assembly="WebShortcutControl" Namespace="BeanSoftware" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="RadAjaxManager1">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="lblForm_Name" />
                <telerik:AjaxUpdatedControl ControlID="txtCompanyName" />
                <telerik:AjaxUpdatedControl ControlID="btnDistribute" />
                <telerik:AjaxUpdatedControl ControlID="lblStatus_Name" />
                <telerik:AjaxUpdatedControl ControlID="lblReferenceCode2" />
                <telerik:AjaxUpdatedControl ControlID="lblSentForm" />
                <telerik:AjaxUpdatedControl ControlID="rgItemDetail" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="rgItemDetail">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="lblStatus_Name" />
                <telerik:AjaxUpdatedControl ControlID="lblReferenceCode2" />
                <telerik:AjaxUpdatedControl ControlID="lblSentForm" />
                <telerik:AjaxUpdatedControl ControlID="rgItemDetail" />
            </UpdatedControls>
        </telerik:AjaxSetting>
    </AjaxSettings>
</telerik:RadAjaxManager>
<telerik:RadCodeBlock ID="RadCodeBlock3" runat="server">

    <script type="text/javascript">
        function ShowRequestPersonForm(){
            window.radopen("/DesktopModules/DFT_EDI_EntryForm/frmChangePersonRequest.aspx", "CardDialog");
            return false;
        }
        
        function ShowCompanyForm(){
            window.radopen("/DesktopModules/DFT_EDI_EntryForm/frmChangeCompany.aspx", "CompanyDialog");
            return false;
        }
        
        function ShowFormType(){
            window.radopen("/DesktopModules/DFT_EDI_EntryForm/frmChangeForm.aspx", "FormDialog");
            return false;
        }
        
        function ShowInsertItemForm(){
            var COUNTRY_CODE = $find("<%=ddlDestination_Country.ClientID %>").get_value();
            var GROSS_WEIGHT = $find("<%=txtQuantity1.ClientID %>").get_value();
            var G_UNIT_CODE = $find("<%=drpQ_UNIT_CODE1.ClientID %>").get_value();
            var quantity5 = $find("<%=txtQuantity2.ClientID %>").get_value();
            var Q_UNIT_CODE5 = $find("<%=drpQ_UNIT_CODE2.ClientID %>").get_value();
            window.radopen("/DesktopModules/DFT_EDI_EntryForm/frmProductItem.aspx?country=" + COUNTRY_CODE + "&g_weight=" + GROSS_WEIGHT + "&g_unit=" + G_UNIT_CODE + "&quan5=" + quantity5 + "&q_unit=" + Q_UNIT_CODE5, "ProductDialog");
            return false;
        }
        
        function ShowEditItem(invh_run_auto,invd_run_auto,tariff_code,certoforigin_no,product_description,net_weight,fob_amt){
            var COUNTRY_CODE = $find("<%=ddlDestination_Country.ClientID %>").get_value();
            var GROSS_WEIGHT = $find("<%=txtQuantity1.ClientID %>").get_value();
            var G_UNIT_CODE = $find("<%=drpQ_UNIT_CODE1.ClientID %>").get_value();
            var quantity5 = $find("<%=txtQuantity2.ClientID %>").get_value();
            var Q_UNIT_CODE5 = $find("<%=drpQ_UNIT_CODE2.ClientID %>").get_value();
            window.radopen("/DesktopModules/DFT_EDI_EntryForm/frmEditProductItem.aspx?h_run_auto=" + invh_run_auto + "&d_run_auto=" + invd_run_auto + "&country=" + COUNTRY_CODE + "&g_weight=" + GROSS_WEIGHT + "&g_unit=" + G_UNIT_CODE + "&quan5=" + quantity5 + "&q_unit=" + Q_UNIT_CODE5 + "&tariff_code=" + tariff_code + "&certoforigin_no=" + certoforigin_no + "&product_description=" + product_description + "&net_weight=" + net_weight + "&fob_amt=" + fob_amt, "ProductDialog");
            return false;
        }
        
        function ShowDeleteItem(invh_run_auto,invd_run_auto){
            window.radopen("/DesktopModules/DFT_EDI_EntryForm/frmDeleteProductItem.aspx?h_run_auto=" + invh_run_auto + "&d_run_auto=" + invd_run_auto, "DeleteDialog");
            return false;
        }
        
        function ShowInvoiceForm(){
            var COUNTRY_CODE = $find("<%=ddlDestination_Country.ClientID %>").get_value();
            window.radopen("/DesktopModules/DFT_EDI_EntryForm/frmInvoice.aspx?country_code=" + COUNTRY_CODE, "InvoiceDialog");
            return false;
        }
        
        function ShowApproveForm(){
            window.radopen("/DesktopModules/DFT_EDI_EntryForm/frmApproved.aspx", "ApprovedDialog");
            return false;
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
        
        function OnClientClose(oWnd,args) {
            var arg = args.get_argument();
                
                var vCard_ID = arg.Card_ID;

                var Card_ID = $find("<%=txtCardID.ClientID %>");
                Card_ID.set_value(vCard_ID);
        }
        
        function refreshGrid(arg){
            if(!arg)
            {
                $find("<%= RadAjaxManager1.ClientID %>").ajaxRequest("Rebind");			        
            }
            else
            {
                $find("<%= RadAjaxManager1.ClientID %>").ajaxRequest("RebindAndNavigate");			        
            }
        }
    </script>

</telerik:RadCodeBlock>
<telerik:RadWindowManager ID="RadWindowManager1" Behavior="Close" runat="server"
    Skin="Office2007" TabIndex="-1">
    <Windows>
        <telerik:RadWindow ID="CardDialog" runat="server" ReloadOnShow="True" ShowContentDuringLoad="False"
            Modal="True" VisibleStatusbar="False" Width="850px" Height="400px" OnClientClose="OnClientClose" />
        <telerik:RadWindow ID="ProductDialog" runat="server" ReloadOnShow="True" ShowContentDuringLoad="False"
            Modal="True" VisibleStatusbar="False" Width="850px" Height="270px" />
        <telerik:RadWindow ID="InvoiceDialog" runat="server" ReloadOnShow="True" ShowContentDuringLoad="False"
            Modal="True" VisibleStatusbar="False" Width="850px" Height="350px" />
        <telerik:RadWindow ID="ApprovedDialog" runat="server" ReloadOnShow="True" ShowContentDuringLoad="False"
            Modal="True" VisibleStatusbar="False" Width="850px" Height="250px" />
        <telerik:RadWindow ID="FormDialog" runat="server" ReloadOnShow="True" ShowContentDuringLoad="False"
            Modal="True" VisibleStatusbar="False" Width="850px" Height="400px" />
        <telerik:RadWindow ID="CompanyDialog" Animation="Slide" runat="server" ReloadOnShow="True"
            ShowContentDuringLoad="False" Modal="True" Width="400px" Height="240px" VisibleStatusbar="False" />
        <telerik:RadWindow ID="DeleteDialog" Animation="Slide" runat="server" ReloadOnShow="True"
            ShowContentDuringLoad="False" Modal="True" Width="450px" Height="150px" VisibleStatusbar="False" />
    </Windows>
</telerik:RadWindowManager>
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
                                                            <font class="groupbox">&nbsp; ระบบงานบันทึกข้อมูลหนังสือรับรองแหล่งกำเนิดสินค้า &nbsp;</font></td>
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
                                                        <td>
                                                            <font class="FormLabel">เลขที่รับคำร้อง :</font>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <table border="0" cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <td>
                                                                        <asp:TextBox ID="txtInvh_Run_Auto1" runat="server" MaxLength="8" Font-Bold="True"
                                                                            Font-Size="16pt" Width="125px"></asp:TextBox>
                                                                        <strong><span style="font-size: 16pt; font-family: Tahoma">-</span></strong>
                                                                        <asp:TextBox ID="txtInvh_Run_Auto2" runat="server" Font-Bold="True" Font-Size="16pt"
                                                                            MaxLength="6" Width="90px"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtInvh_Run_Auto2"
                                                                            Display="Dynamic" ErrorMessage="*" Font-Names="Tahoma" Font-Size="10pt" SetFocusOnError="True"
                                                                            ValidationGroup="search"></asp:RequiredFieldValidator>
                                                                        <asp:Button ID="btnSearch" runat="server" Text="ค้นหา" Width="150px" ValidationGroup="search" />&nbsp;
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblForm_Name" runat="server" Font-Names="Tahoma" Font-Size="14pt"
                                                                            ForeColor="#0000C0"></asp:Label></td>
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
    <tr>
        <td>
            <table id="tblForm" runat="server" border="0" cellpadding="1" cellspacing="0" class="m-frm-bdr"
                width="100%">
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
                                                            <font class="groupbox">&nbsp; บันทึกข้อมูล &nbsp;</font></td>
                                                        <telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">
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
                                                            <font class="FormLabel">บริษัทผู้ส่งออก : </font>
                                                        </td>
                                                        <td>
                                                            <telerik:RadTextBox ID="txtCompanyName" runat="server" TabIndex="1" Width="290px"
                                                                Font-Names="Tahoma" Font-Size="10pt">
                                                            </telerik:RadTextBox></td>
                                                        <td style="text-align: right">
                                                            <font class="FormLabel">ผู้ยื่น Form : </font>
                                                        </td>
                                                        <td>
                                                            <telerik:RadTextBox ID="txtCardID" runat="server" TabIndex="2" Width="120px" Font-Names="Tahoma"
                                                                Font-Size="10pt">
                                                            </telerik:RadTextBox>
                                                            <asp:Button ID="btnChangeRequest" runat="server" Text="เปลี่ยนผู้ยื่น" Width="80px"
                                                                TabIndex="-1" UseSubmitBehavior="False" OnClientClick="return ShowRequestPersonForm();" />
                                                            <asp:Button ID="btnChangeCompany" runat="server" Text="เปลี่ยนบริษัท" Width="80px"
                                                                TabIndex="-1" UseSubmitBehavior="False" OnClientClick="return ShowCompanyForm();" />
                                                            <asp:Button ID="btnChangeForm" runat="server" Text="เปลี่ยนฟอร์ม" Width="80px" TabIndex="-1"
                                                                UseSubmitBehavior="False" OnClientClick="return ShowFormType();" /></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: right">
                                                            <font class="FormLabel">ประเทศปลายทาง : </font>
                                                        </td>
                                                        <td>
                                                            <telerik:RadComboBox ID="ddlDestination_Country" runat="server" Filter="StartsWith"
                                                                Font-Names="Tahoma" Font-Size="10pt" MarkFirstMatch="True" Skin="Web20" Width="290px"
                                                                TabIndex="3">
                                                            </telerik:RadComboBox>
                                                        </td>
                                                        <td style="text-align: right">
                                                            <font class="FormLabel">บริษัทผู้ซื้อ/ผู้รับ : </font>
                                                        </td>
                                                        <td>
                                                            <telerik:RadTextBox ID="RadTextBox1" runat="server" TabIndex="8" Width="350px" Font-Names="Tahoma"
                                                                Font-Size="10pt">
                                                            </telerik:RadTextBox></td>
                                                    </tr>
                                                    <tr id="trOriginCountry" runat="server">
                                                        <td style="text-align: right">
                                                            <font class="FormLabel">ประเทศแหล่งกำเนิด : </font>
                                                        </td>
                                                        <td><telerik:RadComboBox ID="ddlOrigin_Country" runat="server" Filter="StartsWith"
                                                                Font-Names="Tahoma" Font-Size="10pt" MarkFirstMatch="True" Skin="Web20" Width="290px"
                                                                TabIndex="3">
                                                        </telerik:RadComboBox>
                                                        </td>
                                                        <td style="text-align: right">
                                                        </td>
                                                        <td>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: right;">
                                                            <font class="FormLabel">Gross Weight : </font>
                                                        </td>
                                                        <td>
                                                            <telerik:RadNumericTextBox ID="txtQuantity1" runat="server" Culture="(Default)" Font-Names="Tahoma"
                                                                Font-Size="12pt" ForeColor="Blue" MaxLength="20" MaxValue="10000000" MinValue="0"
                                                                ShowSpinButtons="False" TabIndex="4" Value="0" Width="185px">
                                                                <EnabledStyle HorizontalAlign="Right" />
                                                                <NumberFormat DecimalDigits="2" />
                                                            </telerik:RadNumericTextBox>&nbsp;<telerik:RadComboBox ID="drpQ_UNIT_CODE1" runat="server"
                                                                DataSourceID="Sqlunit" DataTextField="DESCRIPTION" DataValueField="CODE" EnableLoadOnDemand="True"
                                                                Filter="StartsWith" MarkFirstMatch="True" Skin="Web20" TabIndex="5" Width="100px"
                                                                Height="300px">
                                                            </telerik:RadComboBox>
                                                        </td>
                                                        <td style="text-align: right;">
                                                            <font class="FormLabel">ที่อยู่ผู้ซื้อ/ผู้รับ : </font>
                                                        </td>
                                                        <td rowspan="3" valign="top">
                                                            <telerik:RadTextBox ID="RadTextBox2" runat="server" TabIndex="9" Width="350px" Font-Names="Tahoma"
                                                                Font-Size="10pt" Height="80px" TextMode="MultiLine">
                                                            </telerik:RadTextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: right">
                                                            <font class="FormLabel">จำนวน/ปริมาณรวม : </font>
                                                        </td>
                                                        <td>
                                                            <telerik:RadNumericTextBox ID="txtQuantity2" runat="server" Culture="(Default)" Font-Names="Tahoma"
                                                                Font-Size="12pt" ForeColor="Blue" MaxLength="20" MaxValue="10000000" MinValue="0"
                                                                ShowSpinButtons="False" TabIndex="6" Value="0" Width="185px">
                                                                <EnabledStyle HorizontalAlign="Right" />
                                                                <NumberFormat DecimalDigits="2" />
                                                            </telerik:RadNumericTextBox>&nbsp;<telerik:RadComboBox ID="drpQ_UNIT_CODE2" runat="server"
                                                                DataSourceID="Sqlunit" DataTextField="DESCRIPTION" DataValueField="CODE" EnableLoadOnDemand="True"
                                                                Filter="StartsWith" MarkFirstMatch="True" Skin="Web20" TabIndex="7" Width="100px"
                                                                Height="300px">
                                                            </telerik:RadComboBox>
                                                        </td>
                                                        <td>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: right">
                                                            <font class="FormLabel">วันที่ส่งออก : </font>
                                                        </td>
                                                        <td>
                                                            <telerik:RadDatePicker ID="txtInvoiceDate1" runat="server" Culture="English (United Kingdom)"
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
                                                        <td>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                        </td>
                                                        <td>
                                                            &nbsp;</td>
                                                        <td>
                                                        </td>
                                                        <td>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4">
                                                            <table id="tblThird" runat="server">
                                                                <tr>
                                                                    <td>
                                                                        <asp:CheckBox ID="chkThird" runat="server" Font-Names="Tahoma" Font-Size="10pt" Text="Thrid Country Invoice" /></td>
                                                                    <td style="text-align: right">
                                                                        <font class="FormLabel">&nbsp;&nbsp; Invoice ประเทศที่ 3 :</font>
                                                                    </td>
                                                                    <td>
                                                                        <telerik:RadTextBox ID="txtThirdInvoiceNo" runat="server" TabIndex="1" Width="290px"
                                                                            Font-Names="Tahoma" Font-Size="10pt">
                                                                        </telerik:RadTextBox></td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="text-align: right">
                                                                        
                                                                    </td>
                                                                    <td style="text-align: right">
                                                                        <font class="FormLabel">มูลค่า :</font>
                                                                    </td>
                                                                    <td>
                                                                        <telerik:RadNumericTextBox ID="txtThirdValue" runat="server" Culture="(Default)"
                                                                            Font-Names="Tahoma" Font-Size="10pt" ForeColor="Blue" MaxLength="20" MaxValue="10000000"
                                                                            MinValue="0" ShowSpinButtons="False" TabIndex="6" Value="0" Width="190px">
                                                                            <EnabledStyle HorizontalAlign="Right" />
                                                                            <NumberFormat DecimalDigits="2" />
                                                                        </telerik:RadNumericTextBox></td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="text-align: right">
                                                                        
                                                                    </td>
                                                                    <td style="text-align: right">
                                                                        <font class="FormLabel">ประเทศผู้ออก :</font>
                                                                    </td>
                                                                    <td>
                                                                        <telerik:RadTextBox ID="txtThirdCountry" runat="server" TabIndex="1" Width="290px"
                                                                            Font-Names="Tahoma" Font-Size="10pt">
                                                                        </telerik:RadTextBox></td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4">
                                                            <table id="tblBack" runat="server">
                                                                <tr>
                                                                    <td>
                                                                        <asp:CheckBox ID="chkBack" runat="server" Font-Names="Tahoma" Font-Size="10pt" Text="Back-to-Back CO" /></td>
                                                                    <td style="text-align: right">
                                                                        <font class="FormLabel">&nbsp; &nbsp;&nbsp; ประเทศต้นทาง :</font>
                                                                    </td>
                                                                    <td>
                                                                        <telerik:RadTextBox ID="txtBackCountry" runat="server" TabIndex="1" Width="294px"
                                                                            Font-Names="Tahoma" Font-Size="10pt">
                                                                        </telerik:RadTextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr id="trBackReferenceNo" runat="server">
                                                                    <td>
                                                                    </td>
                                                                    <td style="text-align: right">
                                                                        <font class="FormLabel">&nbsp; &nbsp;&nbsp; เลขที่หนังสือรับรอง :</font>
                                                                    </td>
                                                                    <td>
                                                                        <telerik:RadTextBox ID="txtBackReferenceNo" runat="server" TabIndex="1" Width="294px"
                                                                            Font-Names="Tahoma" Font-Size="10pt">
                                                                        </telerik:RadTextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr id="trBackQuantity" runat="server">
                                                                    <td>
                                                                    </td>
                                                                    <td style="text-align: right">
                                                                        <font class="FormLabel">&nbsp; &nbsp;&nbsp; ปริมาณ :</font>
                                                                    </td>
                                                                    <td>
                                                                        <telerik:RadNumericTextBox ID="rntBackQuantity" runat="server" Culture="(Default)"
                                                                            Font-Names="Tahoma" Font-Size="10pt" ForeColor="Blue" MaxLength="20" MaxValue="10000000"
                                                                            MinValue="0" ShowSpinButtons="False" TabIndex="6" Value="0" Width="120px">
                                                                            <EnabledStyle HorizontalAlign="Right" />
                                                                            <NumberFormat DecimalDigits="2" />
                                                                        </telerik:RadNumericTextBox>
                                                                        <font class="FormLabel">คงเหลือ</font>
                                                                        <telerik:RadNumericTextBox ID="rntBackBalance" runat="server" Culture="(Default)"
                                                                            Font-Names="Tahoma" Font-Size="10pt" ForeColor="Blue" MaxLength="20" MaxValue="10000000"
                                                                            MinValue="0" ShowSpinButtons="False" TabIndex="6" Value="0" Width="120px">
                                                                            <EnabledStyle HorizontalAlign="Right" />
                                                                            <NumberFormat DecimalDigits="2" />
                                                                        </telerik:RadNumericTextBox></td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4">
                                                            <table cellspacing="3">
                                                                <tr>
                                                                    <td>
                                                                        <cc1:ShortcutButton ID="ShortcutButton1" runat="server" Character="F1" OnClientClick="return ShowInsertItemForm();"
                                                                            Text="ShortcutButton" />
                                                            <asp:Button ID="btnInsertItem" runat="server" Text="F1 - เพิ่ม" Width="100px" OnClientClick="return ShowInsertItemForm();" /></td>
                                                                    <td>
                                                            <asp:Button ID="btnInsertInvoice" runat="server" Text="F4 - Invoice" Width="100px" OnClientClick="return ShowInvoiceForm();" /></td>
                                                                    <td>
                                                            <asp:Button ID="btnSave" runat="server" Text="F7 - บันทึกผล" Width="100px" OnClientClick="return ShowApproveForm();" /></td>
                                                                    <td>
                                                            <asp:Button ID="btnDistribute" runat="server" Text="F8 - จ่ายออก" Width="100px" /></td>
                                                                    <td>
                                                            <asp:Label ID="lblStatus_Name" runat="server" CssClass="FormLabel" Font-Size="14pt" ForeColor="Green"></asp:Label></td>
                                                                    <td>
                                                            <asp:Label ID="lblReferenceCode2" runat="server" CssClass="FormLabel" Font-Size="14pt" ForeColor="#0000C0"></asp:Label></td>
                                                                    <td>
                                                            <asp:Label ID="lblSentForm" runat="server" CssClass="FormLabel" Font-Size="14pt" ForeColor="Red"></asp:Label></td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4">
                                                            <telerik:RadGrid ID="rgItemDetail" runat="server" AllowPaging="True" AllowSorting="True"
                                                                GridLines="None" ShowStatusBar="True" Skin="Office2007" TabIndex="-1" Width="100%">
                                                                <MasterTableView AutoGenerateColumns="False" CellSpacing="-1" DataKeyNames="invh_run_auto,invd_run_auto,tariff_code,certoforigin_no,product_description,net_weight,fob_amt"
                                                                    NoMasterRecordsText="ไม่พบรายการสินค้า" Width="100%">
                                                                    <Columns>
                                                                        <telerik:GridTemplateColumn UniqueName="EditTemplateColumn" HeaderText="แก้ไข">
                                                                            <ItemTemplate>
                                                                                <asp:HyperLink ID="EditLink" runat="server" ImageUrl="~/images/edit.gif"></asp:HyperLink>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                        </telerik:GridTemplateColumn>
                                                                        <telerik:GridTemplateColumn UniqueName="DeleteTemplateColumn" HeaderText="ลบ">
                                                                            <ItemTemplate>
                                                                                <asp:HyperLink ID="DeleteLink" runat="server" ImageUrl="~/images/delete.gif"></asp:HyperLink>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                        </telerik:GridTemplateColumn>
                                                                        <telerik:GridBoundColumn DataField="tariff_code" HeaderText="พิกัดสินค้า" ReadOnly="True"
                                                                            SortExpression="tariff_code" UniqueName="tariff_code">
                                                                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="certoforigin_no" HeaderText="หมายเลขต้นทุน" ReadOnly="True"
                                                                            SortExpression="certoforigin_no" UniqueName="certoforigin_no">
                                                                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="product_description" HeaderText="รายละเอียด"
                                                                            SortExpression="product_description" UniqueName="product_description">
                                                                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="net_weight" HeaderText="น้ำหนักสุทธิ KGS" ReadOnly="True"
                                                                            SortExpression="net_weight" UniqueName="net_weight">
                                                                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Right" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="fob_amt" HeaderText="FOB (US$)" ReadOnly="True"
                                                                            SortExpression="fob_amt" UniqueName="fob_amt">
                                                                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Right" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="gross_weight" HeaderText="ปริมาณไม่ใช่ KGS" ReadOnly="True"
                                                                            SortExpression="gross_weight" UniqueName="gross_weight">
                                                                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Right" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="invd_run_auto" HeaderText="ลำดับที่" ReadOnly="True"
                                                                            SortExpression="invd_run_auto" UniqueName="invd_run_auto">
                                                                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                        </telerik:GridBoundColumn>
                                                                    </Columns>
                                                                </MasterTableView>
                                                                <HeaderStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                <ClientSettings EnableRowHoverStyle="True">
                                                                    <Selecting AllowRowSelect="True" />
                                                                </ClientSettings>
                                                            </telerik:RadGrid>
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
<asp:Label ID="lblCompany_Taxno" runat="server" Visible="False"></asp:Label>
<asp:Label ID="lblForm_Type" runat="server" Visible="False"></asp:Label>
<asp:Label ID="lblEDI_Status_ID" runat="server" Visible="False"></asp:Label>
<asp:Label ID="lblDistributeForm" runat="server" Visible="False"></asp:Label>
<asp:SqlDataSource ID="Sqlunit" runat="server" ConnectionString="<%$ ConnectionStrings:OriginConnection %>"
    SelectCommand="sp_common_get_unit" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
