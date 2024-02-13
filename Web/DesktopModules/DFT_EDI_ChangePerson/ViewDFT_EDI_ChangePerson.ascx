<%@ Control language="vb" Inherits="YourCompany.Modules.DFT_EDI_ChangePerson.ViewDFT_EDI_ChangePerson" AutoEventWireup="false" Explicit="True" Codebehind="ViewDFT_EDI_ChangePerson.ascx.vb" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="rgRequestForm">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="rgRequestForm" />
            </UpdatedControls>
        </telerik:AjaxSetting>
    </AjaxSettings>
</telerik:RadAjaxManager>
<telerik:RadCodeBlock ID="RadCodeBlockFirst" runat="server">

    <script type="text/javascript">
        
        function refreshGrid(arg)
        {
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
<table border="0" cellpadding="0" cellspacing="5" width="100%">
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
                                                <asp:Label ID="lblRoleID" runat="server" Text="lblRoleID" Visible="False"></asp:Label></td>
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
                                                            <font class="FormLabel">
                                                                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                                                                    <tr>
                                                                        <td class="FormLabel" style="width: 170px">
                                                                            เลขที่บัตรประจำตัวผู้ส่งออก :</td>
                                                                        <td>
                                                                <asp:TextBox ID="txtSearch" runat="server" MaxLength="9"></asp:TextBox>&nbsp;
                                                                <asp:Button ID="btnSearch" runat="server" Text="ค้นหา" ValidationGroup="search" Width="150px" />&nbsp;<asp:CheckBox
                                                                                ID="chkRePrint" runat="server" Font-Names="Tahoma" Font-Size="10pt" Text="รายการที่พิมพ์แล้ว" />
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtSearch"
                                                                    Display="Dynamic" ErrorMessage="กรุณาป้อนหมายเลขบัตร" Font-Names="Tahoma" Font-Size="10pt"
                                                                    SetFocusOnError="True" ValidationGroup="search"></asp:RequiredFieldValidator></td>
                                                                    </tr>
                                                                </table>
                                                                </font></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="font-size: 12pt; color: #000000; font-family: Times New Roman">
                                                            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                                                                <tr>
                                                                    <td class="FormLabel" style="width: 170px; height: 25px;">
                                                                        ค้นหาจากเลขอ้างอิง :</td>
                                                                    <td style="height: 25px">
                                                                        <asp:TextBox ID="txtSearchNum" runat="server" MaxLength="20"></asp:TextBox>&nbsp;
                                                                        <asp:Button ID="btnSearch2" runat="server" Text="ค้นหา" ValidationGroup="search2" Width="150px" />
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtSearchNum"
                                                                            Display="Dynamic" ErrorMessage="กรุณาป้อนเลขหนังสือรับรองหรือเลขคำร้อง" Font-Names="Tahoma"
                                                                            Font-Size="10pt" SetFocusOnError="True" ValidationGroup="search2"></asp:RequiredFieldValidator></td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="FormLabel" colspan="2" style="height: 25px">
                                                                        (เลขหนังสือรับรองหรือเลขคำร้อง)</td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="15">
                                            </td>
                                            <td>
                                                <table border="0" cellpadding="0" cellspacing="2" width="100%" id="TBLPerson" runat="server">
                                                    <tr>
                                                        <td style="font-size: 12pt; color: #000000; font-family: Times New Roman">
                                                            <font class="FormLabel"><table runat="server" id="Table1" border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                                                                <tr>
                                                                    <td class="FormLabel" style="width: 170px" valign="top">
                                                                        เลือกผู้รับมอบที่ต้องการเปลี่ยน :</td>
                                                                    <td style="width: 380px" valign="top">
                                                                        <telerik:RadComboBox ID="RCBRequestPerson" runat="server" Skin="Vista" Width="369px">
                                                                        </telerik:RadComboBox>
                                                                    </td>
                                                                    <td valign="top">
                                                                <asp:Button ID="btnUpdatePerson" runat="server" Text="บันทึกแก้ไขผู้รับมอบ" ValidationGroup="Gremark" /></td>
                                                                </tr>
                                                            </table>
                                                            </font></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="font-size: 12pt; color: #000000; font-family: Times New Roman">
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtChangeRemark"
                                                                            Display="Dynamic" ErrorMessage="กรุณาป้อนเหตุผลในการเปลี่ยนผู้รับมอบ" Font-Names="Tahoma"
                                                                            Font-Size="10pt" SetFocusOnError="True" ValidationGroup="Gremark"></asp:RequiredFieldValidator>
                                                            <asp:Label ID="lbl_ErrMSG" runat="server" Font-Size="10pt" ForeColor="Red" TabIndex="-1"
                                                                    Visible="False"></asp:Label></td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="15">
                                            </td>
                                            <td>
                                                <table runat="server" id="tblremark" border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                                                    <tr>
                                                        <td class="FormLabel" style="width: 170px" valign="top">
                                                            เหตุผลในการเปลี่ยนผู้รับมอบ :</td>
                                                        <td>
                        <asp:TextBox ID="txtChangeRemark" runat="server" Height="40px" MaxLength="9" TextMode="MultiLine"
                            Width="642px" Visible="False"></asp:TextBox></td>
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
                class="m-frm-bdr" width="100%">
                <tr>
                    <td>
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr class="m-frm-hdr">
                                <td>
                                    <telerik:RadTabStrip ID="RadTabStrip1" runat="server" Font-Names="Tahoma" Font-Size="10pt"
                                        MultiPageID="RadMultiPage1" SelectedIndex="0" Skin="Office2007" Width="100%">
                                        <Tabs>
                                            <telerik:RadTab runat="server" Font-Names="Tahoma" Font-Size="10pt" Selected="True"
                                                Text="ข้อมูลผู้ส่งออก">
                                            </telerik:RadTab>
                                            <telerik:RadTab runat="server" Font-Names="Tahoma" Font-Size="10pt" Text="ข้อมูลผู้รับมอบ">
                                            </telerik:RadTab>
                                            <telerik:RadTab runat="server" Font-Names="Tahoma" Font-Size="10pt" Text="รายการยื่นเอกสารแนบ">
                                            </telerik:RadTab>
                                            <telerik:RadTab runat="server" Font-Names="Tahoma" Font-Size="10pt" Text="บันทึกการกระทำความผิด">
                                            </telerik:RadTab>
                                        </Tabs>
                                    </telerik:RadTabStrip>
                                </td>
                            </tr>
                            <tr>
                                <td style="border-right: #cccccc 1px solid; border-top: #cccccc 1px solid; border-left: #cccccc 1px solid;
                                    border-bottom: #cccccc 1px solid; background-color: #eeeeee">
                                    <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0" TabIndex="-1"
                                        Width="100%">
                                        <telerik:RadPageView ID="RadPageView1" runat="server" TabIndex="-1" Width="100%">
                                            <table border="0" cellpadding="0" cellspacing="2" width="100%">
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
                                                                            <td style="font-size: 12pt; color: #000000; font-family: Times New Roman">
                                                                                <font class="FormLabel">บริษัท:&nbsp;</font><telerik:RadTextBox ID="txtCompanyName_Eng"
                                                                                    runat="server" CssClass="FormFld" EnableEmbeddedSkins="False" ReadOnly="True"
                                                                                    TabIndex="-1" Width="315px">
                                                                                </telerik:RadTextBox>
                                                                                <font class="FormLabel">TaxID:</font>
                                                                                <telerik:RadTextBox ID="txtCompany_Taxno" runat="server" CssClass="FormFld" EnableEmbeddedSkins="False"
                                                                                    ReadOnly="True" TabIndex="-1" Width="150px">
                                                                                </telerik:RadTextBox>
                                                                                <telerik:RadTextBox ID="txtCompany_BranchNo" runat="server" CssClass="FormFld" EnableEmbeddedSkins="False"
                                                                                    ReadOnly="True" TabIndex="-1" Visible="False" Width="150px">
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
                                                                                <font class="FormLabel">กรรมการผู้มีอำนาจ 1.<telerik:RadTextBox ID="txtAuthorize1"
                                                                                    runat="server" CssClass="FormFld" EnableEmbeddedSkins="False" ReadOnly="True"
                                                                                    TabIndex="-1" Width="200px">
                                                                                </telerik:RadTextBox>
                                                                                    2.<telerik:RadTextBox ID="txtAuthorize2" runat="server" CssClass="FormFld" EnableEmbeddedSkins="False"
                                                                                        ReadOnly="True" TabIndex="-1" Width="200px">
                                                                                    </telerik:RadTextBox>
                                                                                    3.<telerik:RadTextBox ID="txtAuthorize3" runat="server" CssClass="FormFld" EnableEmbeddedSkins="False"
                                                                                        ReadOnly="True" TabIndex="-1" Width="200px">
                                                                                    </telerik:RadTextBox></font></td>
                                                                        </tr>
                                                                        <tr style="font-size: 12pt; font-family: Times New Roman">
                                                                            <td>
                                                                                <font class="FormLabel">กรรมการผู้รับมอบอำนาจ 1.<telerik:RadTextBox ID="txtAuthorize4"
                                                                                    runat="server" CssClass="FormFld" EnableEmbeddedSkins="False" ReadOnly="True"
                                                                                    TabIndex="-1" Width="200px">
                                                                                </telerik:RadTextBox>
                                                                                    2.<telerik:RadTextBox ID="txtAuthorize5" runat="server" CssClass="FormFld" EnableEmbeddedSkins="False"
                                                                                        ReadOnly="True" TabIndex="-1" Width="200px">
                                                                                    </telerik:RadTextBox>
                                                                                    3.<telerik:RadTextBox ID="txtAuthorize6" runat="server" CssClass="FormFld" EnableEmbeddedSkins="False"
                                                                                        ReadOnly="True" TabIndex="-1" Width="200px">
                                                                                    </telerik:RadTextBox></font></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <font class="FormLabel">ผู้ลงนาม:
                                                                                    <telerik:RadTextBox ID="txtAuthorize7" runat="server" CssClass="FormFld" EnableEmbeddedSkins="False"
                                                                                        ReadOnly="True" TabIndex="-1" Width="400px">
                                                                                    </telerik:RadTextBox>
                                                                                    ระดับบัตร
                                                                                    <telerik:RadTextBox ID="txtCardLevel" runat="server" CssClass="FormFld" EnableEmbeddedSkins="False"
                                                                                        ReadOnly="True" TabIndex="-1" Width="200px">
                                                                                    </telerik:RadTextBox></font>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                                                                <asp:TextBox ID="txtReturnMsg" runat="server" BackColor="Black" Font-Bold="True"
                                                                                    Font-Names="Tahoma" Font-Size="10pt" ForeColor="Yellow" Height="100px" ReadOnly="True"
                                                                                    TextMode="MultiLine" Width="400px"></asp:TextBox>
                                                                                <asp:TextBox ID="txtReturnMsgBlack" runat="server" BackColor="Black" Font-Bold="True"
                                                                                    Font-Names="Tahoma" Font-Size="10pt" ForeColor="Yellow" Height="100px" ReadOnly="True"
                                                                                    TextMode="MultiLine" Width="400px"></asp:TextBox></td>
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
                                                                                                        <td style='background-image: url(<%= ResolveUrl("images") %>/CORNER.gif); background-repeat: no-repeat;'>
                                                                                                            &nbsp; &nbsp;&nbsp;</td>
                                                                                                    </telerik:RadCodeBlock>
                                                                                                </tr>
                                                                                            </table>
                                                                                            <asp:TextBox ID="TextBox1" runat="server" Visible="False"></asp:TextBox></td>
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
                                                                                        <td>
                                                                                            <table border="0" cellpadding="0" cellspacing="2" width="100%">
                                                                                                <tr>
                                                                                                    <td style="font-size: 12pt; color: #000000; font-family: Times New Roman">
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        <telerik:RadGrid ID="rgRequestForm" runat="server" AllowMultiRowSelection="True"
                                                                                                            AllowSorting="True" GridLines="None" ShowFooter="True" ShowStatusBar="True" Skin="Office2007"
                                                                                                            TabIndex="-1" Width="100%">
                                                                                                            <MasterTableView AutoGenerateColumns="False" ClientDataKeyNames="invh_run_auto" DataKeyNames="invh_run_auto,Check_StatusWeb"
                                                                                                                NoMasterRecordsText="ไม่มีรายการคำร้อง" Width="100%">
                                                                                                                <Columns>
                                                                                                                    <telerik:GridTemplateColumn UniqueName="CheckBoxTemplateColumn">
                                                                                                                        <HeaderStyle Width="15px" HorizontalAlign="Center" />
                                                                                                                        <HeaderTemplate>
                                                                                                                            <asp:CheckBox ID="headerChkbox" OnCheckedChanged="ToggleSelectedState" AutoPostBack="True"
                                                                                                                                runat="server"></asp:CheckBox>
                                                                                                                        </HeaderTemplate>
                                                                                                                        <ItemStyle Width="15px" HorizontalAlign="Center" />
                                                                                                                        <ItemTemplate>
                                                                                                                            <asp:CheckBox ID="chkSelect" OnCheckedChanged="ToggleRowSelection"  AutoPostBack="True"
                                                                                                                                runat="server"></asp:CheckBox>
                                                                                                                        </ItemTemplate>
                                                                                                                    </telerik:GridTemplateColumn>
                                                                                                                    <telerik:GridBoundColumn DataField="form_name" HeaderStyle-Width="100px" HeaderStyle-Wrap="true" HeaderText="ฟอร์ม" ReadOnly="True"
                                                                                                                        SortExpression="form_name" UniqueName="form_name">
                                                                                                                        <ItemStyle Font-Names="Tahoma" Font-Size="10pt"   />
                                                                                                                    </telerik:GridBoundColumn>
                                                                                                                    <telerik:GridNumericColumn Aggregate="Count" DataField="invh_run_auto" FooterText="รวม : "
                                                                                                                        HeaderText="เลขที่อ้างอิง" ReadOnly="True" SortExpression="invh_run_auto" UniqueName="invh_run_auto">
                                                                                                                        <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                                                                        <FooterStyle Font-Names="Tahoma" Font-Size="10pt" />
                                                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                                                    </telerik:GridNumericColumn>
                                                                                                                    <telerik:GridBoundColumn DataField="reference_code2" HeaderText="เลขที่หนังสือรับรอง"
                                                                                                                        ReadOnly="True" SortExpression="reference_code2" UniqueName="reference_code2">
                                                                                                                        <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                                                                    </telerik:GridBoundColumn>
                                                                                                                    <telerik:GridBoundColumn DataField="company_name" HeaderText="ผู้ขอ" SortExpression="company_name"
                                                                                                                        UniqueName="company_name" Visible="False">
                                                                                                                        <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                                                                                                    </telerik:GridBoundColumn>
                                                                                                                    <telerik:GridTemplateColumn HeaderText="วันที่ส่ง" UniqueName="TemplateColumn">
                                                                                                                        <ItemTemplate>
                                                                                                                            <asp:Label ID="lblSent_Date" runat="server" Text='<%#GetDate(Eval("sent_date")) %>'></asp:Label>
                                                                                                                        </ItemTemplate>
                                                                                                                        <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                                                                    </telerik:GridTemplateColumn>
                                                                                                                    <telerik:GridBoundColumn DataField="print_no" Visible="False" HeaderText="ครั้งที่พิมพ์" ReadOnly="True"
                                                                                                                        SortExpression="print_no" UniqueName="print_no">
                                                                                                                        <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                                                                    </telerik:GridBoundColumn>
                                                                                                                    <telerik:GridBoundColumn DataField="print_flag" HeaderText="พิมพ์" ReadOnly="True"
                                                                                                                        SortExpression="print_flag" UniqueName="print_flag">
                                                                                                                        <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                                                                    </telerik:GridBoundColumn>
                                                                                                                    <telerik:GridBoundColumn DataField="reference_code1" Visible="False" HeaderText="เลขที่รับคำร้อง"
                                                                                                                        ReadOnly="True" SortExpression="reference_code1" UniqueName="reference_code1">
                                                                                                                        <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                                                                    </telerik:GridBoundColumn>
                                                                                                                    <telerik:GridBoundColumn DataField="request_person" HeaderStyle-Width="100px" HeaderText="ผู้รับมอบ" ReadOnly="True"
                                                                                                                        SortExpression="request_person" UniqueName="request_person">
                                                                                                                        <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                                                                    </telerik:GridBoundColumn>
                                                                                                                    <telerik:GridBoundColumn DataField="card_id" HeaderText="เลขบัตร" Visible="false" ReadOnly="True"
                                                                                                                        SortExpression="card_id" UniqueName="card_id">
                                                                                                                        <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                                                                    </telerik:GridBoundColumn>
                                                                                                                    <telerik:GridBoundColumn DataField="form_type" HeaderText="form_type" ReadOnly="True"
                                                                                                                        SortExpression="form_type" UniqueName="form_type" Visible="False">
                                                                                                                        <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                                                                    </telerik:GridBoundColumn>
                                                                                                                    <telerik:GridBoundColumn DataField="site_id" HeaderText="site_id" ReadOnly="True"
                                                                                                                        SortExpression="site_id" UniqueName="site_id" Visible="False">
                                                                                                                        <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                                                                    </telerik:GridBoundColumn>
                                                                                                                    <telerik:GridBoundColumn DataField="COMPANY_TAXNO" HeaderText="COMPANY_TAXNO" ReadOnly="True"
                                                                                                                        SortExpression="COMPANY_TAXNO" UniqueName="COMPANY_TAXNO" Visible="False">
                                                                                                                        <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                                                                    </telerik:GridBoundColumn>
                                                                                                                    <telerik:GridBoundColumn DataField="FROM_DATE" HeaderText="FROM_DATE" ReadOnly="True"
                                                                                                                        SortExpression="FROM_DATE" UniqueName="FROM_DATE" Visible="False">
                                                                                                                        <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                                                                    </telerik:GridBoundColumn>
                                                                                                                    <telerik:GridBoundColumn DataField="TO_DATE" HeaderText="TO_DATE" ReadOnly="True"
                                                                                                                        SortExpression="TO_DATE" UniqueName="TO_DATE" Visible="False">
                                                                                                                        <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                                                                    </telerik:GridBoundColumn>
                                                                                                                    <telerik:GridBoundColumn DataField="INVOICE_NO1" HeaderText="NO.INVOICE" ReadOnly="True"
                                                                                                                        SortExpression="INVOICE_NO1" UniqueName="INVOICE_NO1">
                                                                                                                        <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                                                                                                    </telerik:GridBoundColumn>
                                                                                                                    <telerik:GridBoundColumn DataField="Check_StatusWeb" HeaderText="สถานะระบบใหม่-เก่า"
                                                                                                                        ReadOnly="True" SortExpression="Check_StatusWeb" UniqueName="Check_StatusWeb">
                                                                                                                        <ItemStyle Font-Names="Tahoma" Font-Size="10pt" ForeColor="Red" />
                                                                                                                    </telerik:GridBoundColumn>
                                                                                                                    <telerik:GridBoundColumn DataField="DISPLAY_FLAG" HeaderText="DISPLAY_FLAG" ReadOnly="True"
                                                                                                                        SortExpression="DISPLAY_FLAG" UniqueName="DISPLAY_FLAG" Visible="False">
                                                                                                                        <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                                                                    </telerik:GridBoundColumn>
                                                                                                                </Columns>
                                                                                                                <CommandItemSettings AddNewRecordText="" />
                                                                                                            </MasterTableView>
                                                                                                            <HeaderStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                                                            <ClientSettings AllowKeyboardNavigation="True">
                                                                                                                <KeyboardNavigationSettings AllowActiveRowCycle="True" />
                                                                                                                <Scrolling AllowScroll="True" />
                                                                                                            </ClientSettings>
                                                                                                            <ItemStyle Wrap="False" />
                                                                                                            <SelectedItemStyle Wrap="True" />
                                                                                                        </telerik:RadGrid></td>
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
                                        </telerik:RadPageView>
                                        <telerik:RadPageView ID="RadPageView2" runat="server" TabIndex="-1" Width="100%">
                                            <table border="0" cellpadding="0" cellspacing="2" width="100%">
                                                <tr>
                                                    <td>
                                                        <table border="0" cellpadding="0" cellspacing="0" class="m-frm-bdr" width="100%">
                                                            <tr class="m-frm-hdr">
                                                                <td align="left" class="m-frm-hdr">
                                                                    <table border="0" cellpadding="0" cellspacing="0">
                                                                        <tr>
                                                                            <td class="groupboxheader" nowrap="nowrap">
                                                                                <font class="groupbox">&nbsp; รายการบัตรผู้รับมอบ &nbsp;</font></td>
                                                                            <telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">
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
                                                                                <telerik:RadGrid ID="rgCardList" runat="server" AllowPaging="True" AllowSorting="True"
                                                                                    Font-Names="Tahoma" Font-Size="X-Small" GridLines="None" ShowFooter="True" ShowStatusBar="True"
                                                                                    Skin="Office2007" TabIndex="-1" Width="100%">
                                                                                    <MasterTableView AutoGenerateColumns="False" ClientDataKeyNames="card_id" DataKeyNames="card_id"
                                                                                        NoMasterRecordsText="ไม่มีรายการบัตร" Width="100%">
                                                                                        <Columns>
                                                                                            <telerik:GridBoundColumn DataField="card_id" HeaderText="รหัสบัตร" ReadOnly="True"
                                                                                                SortExpression="card_id" UniqueName="card_id">
                                                                                                <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                                            </telerik:GridBoundColumn>
                                                                                            <telerik:GridBoundColumn DataField="active_flag" HeaderText="สถานะ" ReadOnly="True"
                                                                                                SortExpression="active_flag" UniqueName="active_flag">
                                                                                                <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                                            </telerik:GridBoundColumn>
                                                                                            <telerik:GridBoundColumn DataField="expire_date" DataFormatString="{0:dd/MM/yyyy}"
                                                                                                HeaderText="วันหมดอายุ" ReadOnly="True" SortExpression="expire_date" UniqueName="expire_date">
                                                                                                <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                                            </telerik:GridBoundColumn>
                                                                                            <telerik:GridBoundColumn DataField="card_level" HeaderText="ระดับ" SortExpression="card_level"
                                                                                                UniqueName="card_level">
                                                                                                <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                                                                            </telerik:GridBoundColumn>
                                                                                            <telerik:GridBoundColumn DataField="card_type" HeaderText="ประเภท" SortExpression="card_type"
                                                                                                UniqueName="card_type">
                                                                                                <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                                            </telerik:GridBoundColumn>
                                                                                            <telerik:GridBoundColumn DataField="AuthName" HeaderText="ชื่อ (อังกฤษ)" ReadOnly="True"
                                                                                                SortExpression="AuthName" UniqueName="AuthName">
                                                                                                <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                                                                            </telerik:GridBoundColumn>
                                                                                            <telerik:GridBoundColumn DataField="AuthName_Thai" HeaderText="ชื่อ (ไทย)" ReadOnly="True"
                                                                                                SortExpression="AuthName_Thai" UniqueName="AuthName_Thai">
                                                                                                <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                                                                            </telerik:GridBoundColumn>
                                                                                            <telerik:GridBoundColumn DataField="AuthAddress" HeaderText="ที่อยู่" ReadOnly="True"
                                                                                                SortExpression="AuthAddress" UniqueName="AuthAddress">
                                                                                                <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                                                                            </telerik:GridBoundColumn>
                                                                                        </Columns>
                                                                                    </MasterTableView>
                                                                                    <HeaderStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                                    <ClientSettings EnableRowHoverStyle="True">
                                                                                        <Selecting AllowRowSelect="True" />
                                                                                    </ClientSettings>
                                                                                </telerik:RadGrid></td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </telerik:RadPageView>
                                        <telerik:RadPageView ID="RadPageView3" runat="server" TabIndex="-1" Width="100%">
                                            <table border="0" cellpadding="0" cellspacing="2" width="100%">
                                                <tr>
                                                    <td>
                                                        <table border="0" cellpadding="0" cellspacing="0" class="m-frm-bdr" width="100%">
                                                            <tr class="m-frm-hdr">
                                                                <td align="left" class="m-frm-hdr">
                                                                    <table border="0" cellpadding="0" cellspacing="0">
                                                                        <tr>
                                                                            <td class="groupboxheader" nowrap="nowrap">
                                                                                <font class="groupbox">&nbsp; รายการยื่นเอกสารแนบ &nbsp;</font></td>
                                                                            <telerik:RadCodeBlock ID="RadCodeBlock4" runat="server">
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
                                                                                <telerik:RadGrid ID="rgDocAttach" runat="server" AllowPaging="True" AllowSorting="True"
                                                                                    Font-Names="Tahoma" Font-Size="X-Small" GridLines="None" ShowStatusBar="True"
                                                                                    Skin="Office2007" TabIndex="-1" Width="100%">
                                                                                    <MasterTableView AutoGenerateColumns="False" ClientDataKeyNames="invh_run_auto" DataKeyNames="invh_run_auto"
                                                                                        NoMasterRecordsText="ไม่มีรายการเอกสารแนบ" Width="100%">
                                                                                        <Columns>
                                                                                            <telerik:GridBoundColumn DataField="invh_run_auto" HeaderText="เลขที่อ้างอิง" ReadOnly="True"
                                                                                                SortExpression="invh_run_auto" UniqueName="invh_run_auto">
                                                                                                <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                                            </telerik:GridBoundColumn>
                                                                                            <telerik:GridBoundColumn DataField="reference_code2" HeaderText="เลขที่หนังสือ" ReadOnly="True"
                                                                                                SortExpression="reference_code2" UniqueName="reference_code2">
                                                                                                <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                                            </telerik:GridBoundColumn>
                                                                                            <telerik:GridBoundColumn DataField="company_name" HeaderText="บริษัท" ReadOnly="True"
                                                                                                SortExpression="company_name" UniqueName="company_name">
                                                                                                <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                                                                            </telerik:GridBoundColumn>
                                                                                            <telerik:GridBoundColumn DataField="form_name" HeaderText="ฟอร์ม" SortExpression="form_name"
                                                                                                UniqueName="form_name">
                                                                                                <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                                            </telerik:GridBoundColumn>
                                                                                            <telerik:GridBoundColumn DataField="edi_status" HeaderText="การแนบ" SortExpression="edi_status"
                                                                                                UniqueName="edi_status">
                                                                                                <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                                                                            </telerik:GridBoundColumn>
                                                                                            <telerik:GridBoundColumn DataField="OverDay" HeaderText="เกินกำหนด (วัน)" ReadOnly="True"
                                                                                                SortExpression="OverDay" UniqueName="OverDay">
                                                                                                <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                                            </telerik:GridBoundColumn>
                                                                                            <telerik:GridBoundColumn DataField="rep_doc_date" DataFormatString="{0:dd/MM/yyyy}"
                                                                                                HeaderText="วันที่บันทึกการแนบ" ReadOnly="True" SortExpression="rep_doc_date"
                                                                                                UniqueName="rep_doc_date">
                                                                                                <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                                            </telerik:GridBoundColumn>
                                                                                            <telerik:GridBoundColumn DataField="rep_by" HeaderText="ผู้บันทึกการแนบ" ReadOnly="True"
                                                                                                SortExpression="rep_by" UniqueName="rep_by">
                                                                                                <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                                            </telerik:GridBoundColumn>
                                                                                            <telerik:GridBoundColumn DataField="approve_date" DataFormatString="{0:dd/MM/yyyy}"
                                                                                                HeaderText="วันอนุมัติ" ReadOnly="True" SortExpression="approve_date" UniqueName="approve_date">
                                                                                                <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                                            </telerik:GridBoundColumn>
                                                                                            <telerik:GridBoundColumn DataField="approval" HeaderText="ผู้อนุมัติ" ReadOnly="True"
                                                                                                SortExpression="approval" UniqueName="approval">
                                                                                                <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                                            </telerik:GridBoundColumn>
                                                                                            <telerik:GridBoundColumn DataField="approve_by" HeaderText="โดย" ReadOnly="True"
                                                                                                SortExpression="approve_by" UniqueName="approve_by">
                                                                                                <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                                            </telerik:GridBoundColumn>
                                                                                            <telerik:GridBoundColumn DataField="site_id" HeaderText="สถานที่" ReadOnly="True"
                                                                                                SortExpression="site_id" UniqueName="site_id">
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
                                        </telerik:RadPageView>
                                        <telerik:RadPageView ID="RadPageView4" runat="server" TabIndex="-1" Width="100%">
                                            <table border="0" cellpadding="0" cellspacing="2" width="100%">
                                                <tr>
                                                    <td>
                                                        <table border="0" cellpadding="0" cellspacing="0" class="m-frm-bdr" width="100%">
                                                            <tr class="m-frm-hdr">
                                                                <td align="left" class="m-frm-hdr">
                                                                    <table border="0" cellpadding="0" cellspacing="0">
                                                                        <tr>
                                                                            <td class="groupboxheader" nowrap="nowrap">
                                                                                <font class="groupbox">&nbsp; รายการบันทึกการกระทำความผิด &nbsp;</font></td>
                                                                            <telerik:RadCodeBlock ID="RadCodeBlock5" runat="server">
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
                                                                                <telerik:RadGrid ID="rgBackList" runat="server" AllowPaging="True" AllowSorting="True"
                                                                                    Font-Names="Tahoma" Font-Size="X-Small" GridLines="None" ShowStatusBar="True"
                                                                                    Skin="Office2007" TabIndex="-1" Width="100%">
                                                                                    <MasterTableView AutoGenerateColumns="False" NoMasterRecordsText="ไม่มีรายการบันทึกการกระทำความผิด"
                                                                                        Width="100%">
                                                                                        <Columns>
                                                                                            <telerik:GridBoundColumn DataField="Status_Mistake" HeaderText="สถานะ" ReadOnly="True"
                                                                                                SortExpression="Status_Mistake" UniqueName="Status_Mistake">
                                                                                                <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                                            </telerik:GridBoundColumn>
                                                                                            <telerik:GridBoundColumn DataField="Type_Mistake" HeaderText="ประเภท" ReadOnly="True"
                                                                                                SortExpression="Type_Mistake" UniqueName="Type_Mistake">
                                                                                                <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                                            </telerik:GridBoundColumn>
                                                                                            <telerik:GridBoundColumn DataField="Company_Taxno" HeaderText="เลขประจำตัวผู้เสียภาษี"
                                                                                                ReadOnly="True" SortExpression="Company_Taxno" UniqueName="Company_Taxno">
                                                                                                <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                                            </telerik:GridBoundColumn>
                                                                                            <telerik:GridBoundColumn DataField="CompanyName_Eng" HeaderText="ชื่อนิติบุคคล" ReadOnly="True"
                                                                                                SortExpression="CompanyName_Eng" UniqueName="CompanyName_Eng">
                                                                                                <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                                                                            </telerik:GridBoundColumn>
                                                                                            <telerik:GridBoundColumn DataField="Date_Rec_Mistake" DataFormatString="{0:dd/MM/yyyy}"
                                                                                                HeaderText="วันที่ระงับ" ReadOnly="True" SortExpression="Date_Rec_Mistake" UniqueName="Date_Rec_Mistake">
                                                                                                <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                                            </telerik:GridBoundColumn>
                                                                                            <telerik:GridBoundColumn DataField="CountryName" HeaderText="ประเทศ" ReadOnly="True"
                                                                                                SortExpression="CountryName" UniqueName="CountryName">
                                                                                                <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                                            </telerik:GridBoundColumn>
                                                                                            <telerik:GridBoundColumn DataField="form_name" HeaderText="ประเภทฟอร์ม" SortExpression="form_name"
                                                                                                UniqueName="form_name">
                                                                                                <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                                                                            </telerik:GridBoundColumn>
                                                                                            <telerik:GridBoundColumn DataField="Product_Mistake" HeaderText="สินค้า" ReadOnly="True"
                                                                                                SortExpression="Product_Mistake" UniqueName="Product_Mistake">
                                                                                                <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                                                                            </telerik:GridBoundColumn>
                                                                                            <telerik:GridBoundColumn DataField="BlackList_Desc" HeaderText="สาเหตุ" SortExpression="BlackList_Desc"
                                                                                                UniqueName="BlackList_Desc">
                                                                                                <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                                                                            </telerik:GridBoundColumn>
                                                                                            <telerik:GridBoundColumn DataField="Date_Rec_Cancel_Mistake" DataFormatString="{0:dd/MM/yyyy}"
                                                                                                HeaderText="วันที่ยกเลิก" SortExpression="Date_Rec_Cancel_Mistake" UniqueName="Date_Rec_Cancel_Mistake">
                                                                                                <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                                            </telerik:GridBoundColumn>
                                                                                            <telerik:GridBoundColumn DataField="Desc_Cancel_Mistake" HeaderText="บันทึกอนุมัติ"
                                                                                                SortExpression="Desc_Cancel_Mistake" UniqueName="Desc_Cancel_Mistake">
                                                                                                <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
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
<telerik:RadWindowManager ID="RadWindowManager1" runat="server" Behavior="Close"
    Skin="Office2007" TabIndex="-1">
    <Windows>
        <telerik:RadWindow ID="ViewDialog" runat="server" Height="500px" Modal="True" ReloadOnShow="True"
            ShowContentDuringLoad="False" Title="แสดงพิกัดศุลกากร" VisibleStatusbar="False"
            Width="800px">
        </telerik:RadWindow>
        <telerik:RadWindow ID="EditDialog" runat="server" Height="500px" Modal="True" ReloadOnShow="True"
            ShowContentDuringLoad="False" Title="แก้ไขพิกัดศุลกากร" VisibleStatusbar="False"
            Width="800px">
        </telerik:RadWindow>
        <telerik:RadWindow ID="InsertDialog" runat="server" Height="500px" Modal="True" ReloadOnShow="True"
            ShowContentDuringLoad="False" Title="เพิ่มพิกัดศุลกากร" VisibleStatusbar="False"
            Width="800px">
        </telerik:RadWindow>
        <telerik:RadWindow ID="DeleteDialog" runat="server" Animation="Slide" Height="160px"
            Modal="True" ReloadOnShow="True" ShowContentDuringLoad="False" Title="ลบรายการสินค้า"
            VisibleStatusbar="False" Width="450px">
        </telerik:RadWindow>
    </Windows>
</telerik:RadWindowManager>
&nbsp;
