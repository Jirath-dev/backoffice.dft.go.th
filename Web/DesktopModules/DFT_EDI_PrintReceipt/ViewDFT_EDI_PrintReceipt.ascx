<%@ Control Language="vb" Inherits="NTi.Modules.DFT_EDI_PrintReceipt.ViewDFT_EDI_PrintReceipt"
    AutoEventWireup="false" Explicit="True" CodeBehind="ViewDFT_EDI_PrintReceipt.ascx.vb" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="RadAjaxManager1">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="rgReceiptList" />
                <telerik:AjaxUpdatedControl ControlID="rgReceiptListNew" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="rgReceiptList">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="rgReceiptList" />
                <telerik:AjaxUpdatedControl ControlID="rgReceiptListNew" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="rgReceiptListNew">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="rgReceiptListNew" />
                <telerik:AjaxUpdatedControl ControlID="rgReceiptList" />
            </UpdatedControls>
        </telerik:AjaxSetting>
    </AjaxSettings>
</telerik:RadAjaxManager>
<telerik:RadCodeBlock ID="RadCodeBlockFirst" runat="server">

    <script type="text/javascript">

        function ShowInsertForm(BType) {
            window.radopen("/DesktopModules/DFT_EDI_PrintReceipt/frmReceipt.aspx?BType=" + BType, "InsertDialog");
            return false;
        }
        function refreshGrid(arg) {
            if (!arg) {
                $find("<%= RadAjaxManager1.ClientID %>").ajaxRequest("Rebind");
            }
            else {
                $find("<%= RadAjaxManager1.ClientID %>").ajaxRequest("RebindAndNavigate");
            }
        }
    </script>

</telerik:RadCodeBlock>
&nbsp;
<telerik:RadWindowManager ID="RadWindowManager1" Behavior="Close" runat="server"
    Skin="Office2007" TabIndex="-1">
    <Windows>
        <telerik:RadWindow ID="InsertDialog" runat="server" Title="ออกใบเสร็จ" ReloadOnShow="True"
            ShowContentDuringLoad="False" Modal="True" VisibleStatusbar="False" Width="900px"
            Height="550px" />
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
                                                            <font class="groupbox">&nbsp; ค้นหาข้อมูล &nbsp;</font></td>
                                                        <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
                                                            <td style='background-image: url(<%= ResolveUrl("images") %>/CORNER.gif); background-repeat: no-repeat;'>&nbsp; &nbsp;&nbsp;</td>
                                                        </telerik:RadCodeBlock>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td align="right" class="m-frm-nav"></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td class="m-frm" valign="top" width="100%">
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                        <tr>
                                            <td width="15">&nbsp;</td>
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
                                <td>
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
                                            <table width="100%" border="0" cellspacing="2" cellpadding="0">
                                                <tr>
                                                    <td>
                                                        <table border="0" cellpadding="0" cellspacing="0" class="m-frm-bdr" width="100%">
                                                            <tr class="m-frm-hdr">
                                                                <td align="left" class="m-frm-hdr">
                                                                    <table border="0" cellpadding="0" cellspacing="0">
                                                                        <tr>
                                                                            <td class="groupboxheader" nowrap="nowrap">
                                                                                <font class="groupbox">&nbsp; ผู้ขอ &nbsp;</font></td>
                                                                            <telerik:RadCodeBlock ID="RadCodeBlock3" runat="server">
                                                                                <td style='background-image: url(<%= ResolveUrl("images") %>/CORNER.gif); background-repeat: no-repeat'>&nbsp; &nbsp;&nbsp;</td>
                                                                            </telerik:RadCodeBlock>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr class="m-frm-hdr">
                                                                <td align="left">
                                                                    <table border="0" cellpadding="0" cellspacing="2" width="100%">
                                                                        <tr>
                                                                            <td style="font-size: 12pt; color: #000000; font-family: Times New Roman">
                                                                                <font class="FormLabel">บริษัท:&nbsp;</font>
                                                                                <telerik:RadTextBox ID="txtCompanyName_Eng"
                                                                                    runat="server" CssClass="FormFld" EnableEmbeddedSkins="False" TabIndex="-1" Width="315px"
                                                                                    ReadOnly="True">
                                                                                </telerik:RadTextBox>
                                                                                <font class="FormLabel">TaxID:</font>
                                                                                <telerik:RadTextBox ID="txtCompany_Taxno" runat="server" CssClass="FormFld" EnableEmbeddedSkins="False"
                                                                                    TabIndex="-1" Width="150px" ReadOnly="True">
                                                                                </telerik:RadTextBox>
                                                                                <telerik:RadTextBox ID="txtCompany_BranchNo" runat="server" CssClass="FormFld" EnableEmbeddedSkins="False"
                                                                                    TabIndex="-1" Width="150px" ReadOnly="True" Visible="False">
                                                                                </telerik:RadTextBox></td>
                                                                        </tr>
                                                                        <tr style="font-size: 12pt; font-family: Times New Roman">
                                                                            <td style="font-size: 12pt; font-family: Times New Roman">
                                                                                <font class="FormLabel">ที่อยู่:&nbsp;</font>
                                                                                <telerik:RadTextBox ID="txtCompany_Address" runat="server" CssClass="FormFld" EnableEmbeddedSkins="False"
                                                                                    ReadOnly="True" TabIndex="-1" Width="680px">
                                                                                </telerik:RadTextBox></td>
                                                                        </tr>
                                                                        <tr style="font-size: 12pt; font-family: Times New Roman">
                                                                            <td>
                                                                                <font class="FormLabel">โทรศัพท์:</font>
                                                                                <telerik:RadTextBox ID="txtCompany_Phone" runat="server" CssClass="FormFld" EnableEmbeddedSkins="False"
                                                                                    TabIndex="-1" Width="150px" ReadOnly="True">
                                                                                </telerik:RadTextBox>
                                                                                <font class="FormLabel">&nbsp;โทรสาร:</font>
                                                                                <telerik:RadTextBox ID="txtCompany_Fax" runat="server" CssClass="FormFld" EnableEmbeddedSkins="False"
                                                                                    TabIndex="-1" Width="150px" ReadOnly="True">
                                                                                </telerik:RadTextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr style="font-size: 12pt; font-family: Times New Roman">
                                                                            <td>
                                                                                <font class="FormLabel">กรรมการผู้มีอำนาจ 1.<telerik:RadTextBox ID="txtAuthorize1"
                                                                                    runat="server" CssClass="FormFld" EnableEmbeddedSkins="False" TabIndex="-1" Width="200px"
                                                                                    ReadOnly="True">
                                                                                </telerik:RadTextBox>
                                                                                    2.<telerik:RadTextBox ID="txtAuthorize2" runat="server" CssClass="FormFld" EnableEmbeddedSkins="False"
                                                                                        TabIndex="-1" Width="200px" ReadOnly="True">
                                                                                    </telerik:RadTextBox>
                                                                                    3.<telerik:RadTextBox ID="txtAuthorize3" runat="server" CssClass="FormFld" EnableEmbeddedSkins="False"
                                                                                        TabIndex="-1" Width="200px" ReadOnly="True">
                                                                                    </telerik:RadTextBox></font></td>
                                                                        </tr>
                                                                        <tr style="font-size: 12pt; font-family: Times New Roman">
                                                                            <td>
                                                                                <font class="FormLabel">กรรมการผู้รับมอบอำนาจ 1.<telerik:RadTextBox ID="txtAuthorize4"
                                                                                    runat="server" CssClass="FormFld" EnableEmbeddedSkins="False" TabIndex="-1" Width="200px"
                                                                                    ReadOnly="True">
                                                                                </telerik:RadTextBox>
                                                                                    2.<telerik:RadTextBox ID="txtAuthorize5" runat="server" CssClass="FormFld" EnableEmbeddedSkins="False"
                                                                                        TabIndex="-1" Width="200px" ReadOnly="True">
                                                                                    </telerik:RadTextBox>
                                                                                    3.<telerik:RadTextBox ID="txtAuthorize6" runat="server" CssClass="FormFld" EnableEmbeddedSkins="False"
                                                                                        TabIndex="-1" Width="200px" ReadOnly="True">
                                                                                    </telerik:RadTextBox></font></td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <table id="tblRefList" runat="server" border="0" cellpadding="1" cellspacing="0"
                                                            class="m-frm-bdr" width="100%">
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
                                                                                                        <font class="groupbox">&nbsp; ข้อมูลคำขอ &nbsp;</font></td>
                                                                                                    <telerik:RadCodeBlock ID="RadCodeBlock6" runat="server">
                                                                                                        <td style='background-image: url(<%= ResolveUrl("images") %>/CORNER.gif); background-repeat: no-repeat;'>&nbsp; &nbsp;&nbsp;</td>
                                                                                                    </telerik:RadCodeBlock>
                                                                                                </tr>
                                                                                            </table>
                                                                                        </td>
                                                                                        <td align="right" class="m-frm-nav"></td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <telerik:RadTabStrip ID="RadTabStrip2" runat="server" Skin="Office2007" Width="100%"
                                                                                    SelectedIndex="1" MultiPageID="RadMultiPage2">
                                                                                    <Tabs>
                                                                                        <telerik:RadTab TabIndex="0" runat="Server" Text="ใบเสร็จสวัสดิการ (ใบเสร็จเขียว)" Font-Bold="true">
                                                                                        </telerik:RadTab>
                                                                                        <telerik:RadTab TabIndex="1" runat="Server" Text="ใบเสร็จกองทุน (ใบเสร็จเหลือง)" Selected="true" Font-Bold="true">
                                                                                        </telerik:RadTab>
                                                                                    </Tabs>
                                                                                </telerik:RadTabStrip>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 100%">
                                                                                <telerik:RadMultiPage ID="RadMultiPage2" runat="server" Width="100%" SelectedIndex="0"
                                                                                    TabIndex="-1">
                                                                                    <telerik:RadPageView ID="RadPageView2" runat="server" Width="100%" TabIndex="-1">
                                                                                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <table border="0" cellpadding="0" cellspacing="2" width="100%">
                                                                                                        <tr>
                                                                                                            <td style="font-size: 12pt; color: #000000; font-family: Times New Roman">
                                                                                                                <asp:Button ID="btnPrint" runat="server" Text="ออกใบเสร็จ" OnClientClick="return ShowInsertForm('1');"
                                                                                                                    Width="100px" />
                                                                                                                &nbsp;
                                                                                                                <asp:Label ID="lblRecords_Count" runat="server" Font-Names="Tahoma" Font-Size="10pt"
                                                                                                                    ForeColor="Blue" Visible="False"></asp:Label></td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td style="font-size: 12pt; color: #000000; font-family: Times New Roman">
                                                                                                                <telerik:RadGrid ID="rgReceiptList" runat="server" GridLines="None" Skin="Office2007"
                                                                                                                    TabIndex="-1" Width="100%" ShowStatusBar="True">
                                                                                                                    <MasterTableView AutoGenerateColumns="False" ClientDataKeyNames="invh_run_auto" DataKeyNames="invh_run_auto"
                                                                                                                        NoMasterRecordsText="ไม่มีรายการคำร้อง" Width="100%">
                                                                                                                        <Columns>
                                                                                                                            <telerik:GridTemplateColumn UniqueName="CheckBoxTemplateColumn">
                                                                                                                                <HeaderStyle Width="15px" HorizontalAlign="Center" />
                                                                                                                                <ItemStyle Width="15px" HorizontalAlign="Center" />
                                                                                                                                <ItemTemplate>
                                                                                                                                    <asp:CheckBox ID="chkSelect" OnCheckedChanged="ToggleRowSelection" AutoPostBack="True"
                                                                                                                                        runat="server"></asp:CheckBox>
                                                                                                                                </ItemTemplate>
                                                                                                                            </telerik:GridTemplateColumn>
                                                                                                                            <telerik:GridBoundColumn DataField="form_name" HeaderText="ชื่อฟอร์ม" ReadOnly="True"
                                                                                                                                SortExpression="form_name" UniqueName="form_name">
                                                                                                                                <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                                                                                                            </telerik:GridBoundColumn>
                                                                                                                            <telerik:GridBoundColumn DataField="invh_run_auto" HeaderText="เลขที่อ้างอิง" ReadOnly="True"
                                                                                                                                SortExpression="invh_run_auto" UniqueName="invh_run_auto">
                                                                                                                                <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                                                                            </telerik:GridBoundColumn>
                                                                                                                            <telerik:GridBoundColumn DataField="reference_code2" HeaderText="เลขที่หนังสือรับรอง"
                                                                                                                                ReadOnly="True" SortExpression="reference_code2" UniqueName="reference_code2">
                                                                                                                                <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                                                                            </telerik:GridBoundColumn>
                                                                                                                            <telerik:GridBoundColumn DataField="request_person" HeaderText="ผู้รับมอบ" ReadOnly="True"
                                                                                                                                SortExpression="request_person" UniqueName="request_person">
                                                                                                                                <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                                                                            </telerik:GridBoundColumn>
                                                                                                                            <telerik:GridBoundColumn DataField="sent_date" HeaderText="วันที่ส่ง" SortExpression="sent_date"
                                                                                                                                UniqueName="sent_date" DataFormatString="{0:dd/MM/yyyy}">
                                                                                                                                <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                                                                            </telerik:GridBoundColumn>
                                                                                                                            <telerik:GridBoundColumn DataField="totalPrintPage" HeaderText="จำนวนชุด" ReadOnly="True"
                                                                                                                                SortExpression="totalPrintPage" UniqueName="totalPrintPage">
                                                                                                                                <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                                                                            </telerik:GridBoundColumn>
                                                                                                                            <telerik:GridBoundColumn DataField="price" HeaderText="ราคาต่อชุด" ReadOnly="True"
                                                                                                                                SortExpression="price" UniqueName="price">
                                                                                                                                <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Right" />
                                                                                                                            </telerik:GridBoundColumn>
                                                                                                                        </Columns>
                                                                                                                    </MasterTableView>
                                                                                                                    <HeaderStyle HorizontalAlign="Center" Font-Names="Tahoma" Font-Size="10pt" />
                                                                                                                    <ClientSettings EnableRowHoverStyle="True">
                                                                                                                        <Selecting AllowRowSelect="True" />
                                                                                                                        <Scrolling AllowScroll="True" />
                                                                                                                    </ClientSettings>
                                                                                                                </telerik:RadGrid></td>
                                                                                                        </tr>
                                                                                                    </table>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </telerik:RadPageView>
                                                                                    <telerik:RadPageView ID="RadPageView3" runat="server" Width="100%" TabIndex="-1" Selected="true">
                                                                                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <table border="0" cellpadding="0" cellspacing="2" width="100%">
                                                                                                        <tr>
                                                                                                            <td style="font-size: 12pt; color: #000000; font-family: Times New Roman">
                                                                                                                <asp:Button ID="btnPrintNew" runat="server" Text="ออกใบเสร็จ" OnClientClick="return ShowInsertForm('2');"
                                                                                                                    Width="100px" />
                                                                                                                &nbsp;
                                                                                                                <asp:Label ID="Label1" runat="server" Font-Names="Tahoma" Font-Size="10pt" ForeColor="Blue"></asp:Label></td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td style="font-size: 12pt; color: #000000; font-family: Times New Roman">
                                                                                                                <telerik:RadGrid ID="rgReceiptListNew" runat="server" GridLines="None" Skin="Office2007"
                                                                                                                    TabIndex="-1" Width="100%" ShowStatusBar="True">
                                                                                                                    <MasterTableView AutoGenerateColumns="False" ClientDataKeyNames="invh_run_auto" DataKeyNames="invh_run_auto"
                                                                                                                        NoMasterRecordsText="ไม่มีรายการคำร้อง" Width="100%">
                                                                                                                        <Columns>
                                                                                                                            <telerik:GridTemplateColumn UniqueName="CheckBoxTemplateColumn">
                                                                                                                                <HeaderStyle Width="15px" HorizontalAlign="Center" />
                                                                                                                                <ItemStyle Width="15px" HorizontalAlign="Center" />
                                                                                                                                <ItemTemplate>
                                                                                                                                    <asp:CheckBox ID="chkSelect" OnCheckedChanged="ToggleRowSelection" AutoPostBack="True"
                                                                                                                                        runat="server"></asp:CheckBox>
                                                                                                                                </ItemTemplate>
                                                                                                                            </telerik:GridTemplateColumn>
                                                                                                                            <telerik:GridBoundColumn DataField="form_name" HeaderText="ชื่อฟอร์ม" ReadOnly="True"
                                                                                                                                SortExpression="form_name" UniqueName="form_name">
                                                                                                                                <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                                                                                                            </telerik:GridBoundColumn>
                                                                                                                            <telerik:GridBoundColumn DataField="invh_run_auto" HeaderText="เลขที่อ้างอิง" ReadOnly="True"
                                                                                                                                SortExpression="invh_run_auto" UniqueName="invh_run_auto">
                                                                                                                                <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                                                                            </telerik:GridBoundColumn>
                                                                                                                            <telerik:GridBoundColumn DataField="reference_code2" HeaderText="เลขที่หนังสือรับรอง"
                                                                                                                                ReadOnly="True" SortExpression="reference_code2" UniqueName="reference_code2">
                                                                                                                                <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                                                                            </telerik:GridBoundColumn>
                                                                                                                            <telerik:GridBoundColumn DataField="request_person" HeaderText="ผู้รับมอบ" ReadOnly="True"
                                                                                                                                SortExpression="request_person" UniqueName="request_person">
                                                                                                                                <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                                                                            </telerik:GridBoundColumn>
                                                                                                                            <telerik:GridBoundColumn DataField="sent_date" HeaderText="วันที่ส่ง" SortExpression="sent_date"
                                                                                                                                UniqueName="sent_date" DataFormatString="{0:dd/MM/yyyy}">
                                                                                                                                <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                                                                            </telerik:GridBoundColumn>
                                                                                                                            <telerik:GridBoundColumn DataField="totalPrintPage" HeaderText="จำนวนชุด" ReadOnly="True"
                                                                                                                                SortExpression="totalPrintPage" UniqueName="totalPrintPage">
                                                                                                                                <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                                                                            </telerik:GridBoundColumn>
                                                                                                                            <telerik:GridBoundColumn DataField="price" HeaderText="ราคาต่อชุด" ReadOnly="True"
                                                                                                                                SortExpression="price" UniqueName="price">
                                                                                                                                <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Right" />
                                                                                                                            </telerik:GridBoundColumn>
                                                                                                                        </Columns>
                                                                                                                    </MasterTableView>
                                                                                                                    <HeaderStyle HorizontalAlign="Center" Font-Names="Tahoma" Font-Size="10pt" />
                                                                                                                    <ClientSettings EnableRowHoverStyle="True">
                                                                                                                        <Selecting AllowRowSelect="True" />
                                                                                                                        <Scrolling AllowScroll="True" />
                                                                                                                    </ClientSettings>
                                                                                                                </telerik:RadGrid></td>
                                                                                                        </tr>
                                                                                                    </table>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </telerik:RadPageView>
                                                                                </telerik:RadMultiPage>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </telerik:RadPageView>
                                    </telerik:RadMultiPage>
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
<asp:Label ID="lblLoadType" runat="server" Visible="False"></asp:Label>
