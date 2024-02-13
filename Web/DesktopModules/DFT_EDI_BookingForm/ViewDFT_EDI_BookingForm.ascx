<%@ Control Language="vb" Inherits="NTi.Modules.DFT_EDI_BookingForm.ViewDFT_EDI_BookingForm"
    AutoEventWireup="false" Explicit="True" Codebehind="ViewDFT_EDI_BookingForm.ascx.vb" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="rgRequestForm">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="rgRequestForm" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="rgCardList">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="rgCardList" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="rgDocAttach">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="rgDocAttach" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="rgBackList">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="rgBackList" />
            </UpdatedControls>
        </telerik:AjaxSetting>
    </AjaxSettings>
</telerik:RadAjaxManager>
<table width="100%" border="0" cellpadding="0" cellspacing="5">
    <tr>
        <td id="tdCard" runat="server">
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
                                                            <font class="FormLabel">เลขที่บัตรประจำตัวผู้ส่งออก :</font>
                                                            <asp:TextBox ID="txtSearch" runat="server" MaxLength="9"></asp:TextBox>
                                                            <asp:Button ID="btnSearch" runat="server" Text="ค้นหา" Width="150px" ValidationGroup="search" />
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtSearch"
                                                                Display="Dynamic" ErrorMessage="กรุณาป้อนหมายเลขบัตร" Font-Names="Tahoma" Font-Size="10pt"
                                                                SetFocusOnError="True" ValidationGroup="search"></asp:RequiredFieldValidator>
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
                                            <telerik:RadTab runat="server" Text="ข้อมูลผู้รับมอบ" Font-Names="Tahoma" Font-Size="10pt">
                                            </telerik:RadTab>
                                            <telerik:RadTab runat="server" Text="รายการยื่นเอกสารแนบ" Font-Names="Tahoma" Font-Size="10pt">
                                            </telerik:RadTab>
                                            <telerik:RadTab runat="server" Text="บันทึกการกระทำความผิด" Font-Names="Tahoma" Font-Size="10pt">
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
                                                                                <font class="groupbox">&nbsp; รายละเอียดบริษัท &nbsp;</font></td>
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
                                                                                    runat="server" CssClass="FormFld" EnableEmbeddedSkins="False" TabIndex="-1" Width="315px"
                                                                                    ReadOnly="True">
                                                                                </telerik:RadTextBox>
                                                                                <font class="FormLabel">TaxID:</font>
                                                                                <telerik:RadTextBox ID="txtCompany_Taxno" runat="server" CssClass="FormFld" EnableEmbeddedSkins="False"
                                                                                    TabIndex="-1" Width="150px" ReadOnly="True">
                                                                                </telerik:RadTextBox>
                                                                                <font class="FormLabel">สาขา:</font>
                                                                                <telerik:RadTextBox ID="txtCompany_BranchNo" runat="server" CssClass="FormFld" EnableEmbeddedSkins="False"
                                                                                    TabIndex="-1" Width="50px" ReadOnly="True">
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
                                                                            <td style="height: 30px">
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
                                                                                <table cellpadding="0" cellspacing="0">
                                                                                    <tr>
                                                                                        <td>
                                                                                            <font class="FormLabel">กรรมการผู้มีอำนาจ</font></td>
                                                                                        <td>
                                                                                            <font class="FormLabel">1.</font>
                                                                                            <telerik:RadTextBox ID="txtAuthorize1" runat="server" CssClass="FormFld" EnableEmbeddedSkins="False"
                                                                                                TabIndex="-1" Width="200px" ReadOnly="True">
                                                                                            </telerik:RadTextBox></td>
                                                                                        <td>
                                                                                            <font class="FormLabel">2.</font>
                                                                                            <telerik:RadTextBox ID="txtAuthorize2" runat="server" CssClass="FormFld" EnableEmbeddedSkins="False"
                                                                                                TabIndex="-1" Width="200px" ReadOnly="True">
                                                                                            </telerik:RadTextBox></td>
                                                                                        <td>
                                                                                            <font class="FormLabel">3.</font>
                                                                                            <telerik:RadTextBox ID="txtAuthorize3" runat="server" CssClass="FormFld" EnableEmbeddedSkins="False"
                                                                                                TabIndex="-1" Width="200px" ReadOnly="True">
                                                                                            </telerik:RadTextBox></td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                        </td>
                                                                                        <td>
                                                                                            <font class="FormLabel">4.</font>
                                                                                            <telerik:RadTextBox ID="txtAuthorize4" runat="server" CssClass="FormFld" EnableEmbeddedSkins="False"
                                                                                                TabIndex="-1" Width="200px" ReadOnly="True">
                                                                                            </telerik:RadTextBox></td>
                                                                                        <td>
                                                                                            <font class="FormLabel">5.</font>
                                                                                            <telerik:RadTextBox ID="txtAuthorize5" runat="server" CssClass="FormFld" EnableEmbeddedSkins="False"
                                                                                                TabIndex="-1" Width="200px" ReadOnly="True">
                                                                                            </telerik:RadTextBox></td>
                                                                                        <td>
                                                                                            <font class="FormLabel">6.</font>
                                                                                            <telerik:RadTextBox ID="txtAuthorize6" runat="server" CssClass="FormFld" EnableEmbeddedSkins="False"
                                                                                                TabIndex="-1" Width="200px" ReadOnly="True">
                                                                                            </telerik:RadTextBox></td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <font class="FormLabel">ชื่อผู้ถือบัตร:</font>
                                                                                <telerik:RadTextBox ID="txtAuthName_Eng" runat="server" CssClass="FormFld" EnableEmbeddedSkins="False"
                                                                                    TabIndex="-1" Width="400px" ReadOnly="True">
                                                                                </telerik:RadTextBox>
                                                                                <font class="FormLabel">ระดับบัตร</font>
                                                                                <telerik:RadTextBox ID="txtCardLevel" runat="server" CssClass="FormFld" EnableEmbeddedSkins="False"
                                                                                    TabIndex="-1" Width="200px" ReadOnly="True">
                                                                                </telerik:RadTextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                                                                <asp:TextBox ID="txtReturnMsg" runat="server" BackColor="Black" ForeColor="Yellow"
                                                                                    Height="100px" TextMode="MultiLine" Width="400px" Font-Bold="True" Font-Names="Tahoma"
                                                                                    Font-Size="10pt" ReadOnly="True"></asp:TextBox>
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
                                                        <table id="tblBlackList" runat="server" border="0" cellpadding="0" cellspacing="0" class="m-frm-bdr" width="100%">
                                                            <tr class="m-frm-hdr">
                                                                <td align="left" class="m-frm-hdr">
                                                                    <table border="0" cellpadding="0" cellspacing="0">
                                                                        <tr>
                                                                            <td class="groupboxheader" nowrap="nowrap">
                                                                                <font class="groupbox">&nbsp; บันทึกการกระทำความผิด &nbsp;</font></td>
                                                                            <telerik:RadCodeBlock ID="RadCodeBlock7" runat="server">
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
                                                                            <td style="font-size: 12pt; color: #000000; font-family: Times New Roman; text-align: center;">
                                                                                <asp:Label ID="lblBlackList_Msg" runat="server" Font-Bold="True" Font-Names="Tahoma"
                                                                                    Font-Size="10pt" ForeColor="Red"></asp:Label></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="font-size: 12pt; color: #000000; font-family: Times New Roman; text-align: center">
                                                                                <telerik:RadGrid ID="grdBlackListTab1" runat="server" AllowPaging="True" AllowSorting="True"
                                                                                    Font-Names="Tahoma" Font-Size="X-Small" GridLines="None" Skin="Office2007" TabIndex="-1">
                                                                                    <MasterTableView AutoGenerateColumns="False" CellSpacing="-1" NoMasterRecordsText="ไม่มีรายการสินค้า"
                                                                                        TabIndex="-1" Width="100%">
                                                                                        <Columns>
                                                                                            <telerik:GridBoundColumn DataField="Type_Mistake" HeaderText="ประเภท" ReadOnly="True"
                                                                                                SortExpression="Type_Mistake" UniqueName="Type_Mistake">
                                                                                                <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" VerticalAlign="Top" />
                                                                                            </telerik:GridBoundColumn>
                                                                                            <telerik:GridBoundColumn DataField="Date_Rec_Mistake" DataFormatString="{0:dd/MM/yyyy}"
                                                                                                HeaderText="วันที่ระงับ" ReadOnly="True" SortExpression="Date_Rec_Mistake" UniqueName="Date_Rec_Mistake">
                                                                                                <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" VerticalAlign="Top" />
                                                                                            </telerik:GridBoundColumn>
                                                                                            <telerik:GridBoundColumn DataField="CountryName" HeaderText="ประเทศ" ReadOnly="True"
                                                                                                SortExpression="CountryName" UniqueName="CountryName">
                                                                                                <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" VerticalAlign="Top" />
                                                                                            </telerik:GridBoundColumn>
                                                                                            <telerik:GridBoundColumn DataField="Country_Mistake" HeaderText="ประเทศ" ReadOnly="True"
                                                                                                SortExpression="Country_Mistake" UniqueName="Country_Mistake" Visible="false">
                                                                                                <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" VerticalAlign="Top" />
                                                                                            </telerik:GridBoundColumn>
                                                                                            <telerik:GridBoundColumn DataField="form_name" HeaderText="ประเภทฟอร์ม" SortExpression="form_name"
                                                                                                UniqueName="form_name">
                                                                                                <ItemStyle Font-Names="Tahoma" Font-Size="10pt" VerticalAlign="Top" />
                                                                                            </telerik:GridBoundColumn>
                                                                                            <telerik:GridBoundColumn DataField="Form_Mistake" HeaderText="ประเภทฟอร์ม" SortExpression="Form_Mistake"
                                                                                                UniqueName="Form_Mistake" Visible="false">
                                                                                                <ItemStyle Font-Names="Tahoma" Font-Size="10pt" VerticalAlign="Top" />
                                                                                            </telerik:GridBoundColumn>
                                                                                            <telerik:GridBoundColumn DataField="Product_Mistake" HeaderText="สินค้า" ReadOnly="True"
                                                                                                SortExpression="Product_Mistake" UniqueName="Product_Mistake">
                                                                                                <ItemStyle Font-Names="Tahoma" Font-Size="10pt" VerticalAlign="Top" />
                                                                                            </telerik:GridBoundColumn>
                                                                                            <telerik:GridBoundColumn DataField="BlackList_Desc" HeaderText="สาเหตุ" SortExpression="BlackList_Desc"
                                                                                                UniqueName="BlackList_Desc">
                                                                                                <ItemStyle Font-Names="Tahoma" Font-Size="10pt" VerticalAlign="Top" />
                                                                                            </telerik:GridBoundColumn>
                                                                                        </Columns>
                                                                                    </MasterTableView>
                                                                                    <HeaderStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                                    <ClientSettings>
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
                                                                                        <td>
                                                                                            <table border="0" cellpadding="0" cellspacing="2" width="100%">
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        <table>
                                                                                                            <tr>
                                                                                                                <td>
                                                                                                                    <font class="FormLabel">จำนวน :</font>
                                                                                                                    <asp:TextBox ID="txtFormNum" runat="server" Width="100px"></asp:TextBox></td>
                                                                                                                <td>
                                                                                                                    <font class="FormLabel">ฟอร์ม :</font>
                                                                                                                    <telerik:RadComboBox ID="rcbFormType" runat="server" Filter="StartsWith" MarkFirstMatch="True"
                                                                                                                        Skin="Web20" Font-Names="Tahoma" Font-Size="10pt" Width="400px">
                                                                                                                    </telerik:RadComboBox>
                                                                                                                </td>
                                                                                                                <td>
                                                                                                                    <asp:Button ID="btnInsertItem" runat="server" Text="เพิ่มรายการ" Width="120px" /></td>
                                                                                                                <td>
                                                                                                                    <font class="FormLabel">จำนวนพิมพ์ :</font></td>
                                                                                                                <td>
                                                                                                                    <telerik:RadNumericTextBox ID="txtNumber" runat="server" MaxValue="20" MinValue="1"
                                                                                                                        Width="50px" EnableEmbeddedSkins="False" Value="1">
                                                                                                                        <NumberFormat DecimalDigits="0" />
                                                                                                                        <EnabledStyle HorizontalAlign="Center" />
                                                                                                                    </telerik:RadNumericTextBox></td>
                                                                                                                <td>
                                                                                                                    <asp:Button ID="btnPrintBarcode" runat="server" Text="พิมพ์" /></td>
                                                                                                            </tr>
                                                                                                        </table>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        <telerik:RadGrid ID="rgRequestForm" runat="server" AllowPaging="True" AllowSorting="True"
                                                                                                            GridLines="None" Skin="Office2007" TabIndex="-1" Width="100%" ShowStatusBar="True"
                                                                                                            PageSize="5" ShowFooter="True">
                                                                                                            <MasterTableView AutoGenerateColumns="False" CellSpacing="-1" ClientDataKeyNames="invh_run_auto"
                                                                                                                DataKeyNames="invh_run_auto" NoMasterRecordsText="" Width="100%">
                                                                                                                <GroupByExpressions>
                                                                                                                    <telerik:GridGroupByExpression>
                                                                                                                        <SelectFields>
                                                                                                                            <telerik:GridGroupByField FieldAlias="form_name" FieldName="form_name" HeaderValueSeparator=" ฟอร์ม: "
                                                                                                                                FormatString="" HeaderText=""></telerik:GridGroupByField>
                                                                                                                        </SelectFields>
                                                                                                                        <GroupByFields>
                                                                                                                            <telerik:GridGroupByField FieldName="form_name" SortOrder="Descending" FieldAlias="form_name"
                                                                                                                                FormatString="" HeaderText=""></telerik:GridGroupByField>
                                                                                                                        </GroupByFields>
                                                                                                                    </telerik:GridGroupByExpression>
                                                                                                                </GroupByExpressions>
                                                                                                                <Columns>
                                                                                                                    <telerik:GridBoundColumn DataField="form_name" HeaderText="ฟอร์ม" ReadOnly="True"
                                                                                                                        SortExpression="form_name" UniqueName="form_name" Visible="False">
                                                                                                                        <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                                                                                                    </telerik:GridBoundColumn>
                                                                                                                    <telerik:GridNumericColumn DataField="invh_run_auto" ReadOnly="True" HeaderText="เลขที่คำร้อง"
                                                                                                                        SortExpression="invh_run_auto" UniqueName="invh_run_auto" Aggregate="Count" FooterText="รวม : ">
                                                                                                                        <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                                                                        <FooterStyle Font-Names="Tahoma" Font-Size="10pt" />
                                                                                                                    </telerik:GridNumericColumn>
                                                                                                                    <telerik:GridBoundColumn DataField="company_name" HeaderText="บริษัท" SortExpression="company_name"
                                                                                                                        UniqueName="company_name">
                                                                                                                        <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                                                                                                    </telerik:GridBoundColumn>
                                                                                                                    <telerik:GridBoundColumn DataField="AuthName_Thai" HeaderText="ผู้ยื่น" ReadOnly="True"
                                                                                                                        SortExpression="AuthName_Thai" UniqueName="AuthName_Thai">
                                                                                                                        <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                                                                                                    </telerik:GridBoundColumn>
                                                                                                                </Columns>
                                                                                                            </MasterTableView>
                                                                                                            <HeaderStyle HorizontalAlign="Center" Font-Names="Tahoma" Font-Size="10pt" />
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
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </telerik:RadPageView>
                                        <telerik:RadPageView ID="RadPageView2" runat="server" Width="100%" TabIndex="-1">
                                            <table width="100%" border="0" cellspacing="2" cellpadding="0">
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
                                                                                    Font-Names="Tahoma" Font-Size="X-Small" GridLines="None" Skin="Office2007" TabIndex="-1"
                                                                                    Width="100%" ShowStatusBar="True">
                                                                                    <MasterTableView AutoGenerateColumns="False" CellSpacing="-1" ClientDataKeyNames="card_id"
                                                                                        DataKeyNames="card_id" NoMasterRecordsText="ไม่มีรายการบัตร" Width="100%">
                                                                                        <Columns>
                                                                                            <telerik:GridBoundColumn DataField="card_id" HeaderText="รหัสบัตร" ReadOnly="True"
                                                                                                SortExpression="card_id" UniqueName="card_id">
                                                                                                <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                                            </telerik:GridBoundColumn>
                                                                                            <telerik:GridBoundColumn DataField="active_flag" HeaderText="สถานะ" ReadOnly="True"
                                                                                                SortExpression="active_flag" UniqueName="active_flag">
                                                                                                <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                                            </telerik:GridBoundColumn>
                                                                                            <telerik:GridBoundColumn DataField="expire_date" HeaderText="วันหมดอายุ" ReadOnly="True"
                                                                                                SortExpression="expire_date" UniqueName="expire_date" DataFormatString="{0:d}">
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
                                                                                    <HeaderStyle HorizontalAlign="Center" Font-Names="Tahoma" Font-Size="10pt" />
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
                                        <telerik:RadPageView ID="RadPageView3" runat="server" Width="100%" TabIndex="-1">
                                            <table width="100%" border="0" cellspacing="2" cellpadding="0">
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
                                                                                    Font-Names="Tahoma" Font-Size="X-Small" GridLines="None" Skin="Office2007" TabIndex="-1"
                                                                                    Width="100%" ShowStatusBar="True">
                                                                                    <MasterTableView AutoGenerateColumns="False" CellSpacing="-1" ClientDataKeyNames="invh_run_auto"
                                                                                        DataKeyNames="invh_run_auto" NoMasterRecordsText="ไม่มีรายการเอกสารแนบ" Width="100%">
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
                                                                                            <telerik:GridBoundColumn DataField="rep_doc_date" HeaderText="วันี่บันทึกการแนบ"
                                                                                                ReadOnly="True" SortExpression="rep_doc_date" UniqueName="rep_doc_date" DataFormatString="{0:d}">
                                                                                                <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                                            </telerik:GridBoundColumn>
                                                                                            <telerik:GridBoundColumn DataField="rep_by" HeaderText="ผู้บันทึกการแนบ" ReadOnly="True"
                                                                                                SortExpression="rep_by" UniqueName="rep_by">
                                                                                                <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                                            </telerik:GridBoundColumn>
                                                                                            <telerik:GridBoundColumn DataField="approve_date" HeaderText="วันอนุมัติ" ReadOnly="True"
                                                                                                SortExpression="approve_date" UniqueName="approve_date" DataFormatString="{0:d}">
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
                                                                                    <HeaderStyle HorizontalAlign="Center" Font-Names="Tahoma" Font-Size="10pt" />
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
                                        <telerik:RadPageView ID="RadPageView4" runat="server" Width="100%" TabIndex="-1">
                                            <table width="100%" border="0" cellspacing="2" cellpadding="0">
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
                                                                                    Font-Names="Tahoma" Font-Size="X-Small" GridLines="None" Skin="Office2007" TabIndex="-1"
                                                                                    Width="100%" ShowStatusBar="True">
                                                                                    <MasterTableView AutoGenerateColumns="False" CellSpacing="-1" ClientDataKeyNames=""
                                                                                        DataKeyNames="" NoMasterRecordsText="ไม่มีรายการบันทึกการกระทำความผิด" Width="100%">
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
                                                                                            <telerik:GridBoundColumn DataField="Date_Rec_Mistake" HeaderText="วันที่ระงับ" ReadOnly="True"
                                                                                                SortExpression="Date_Rec_Mistake" UniqueName="Date_Rec_Mistake" DataFormatString="{0:d}">
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
                                                                                            <telerik:GridBoundColumn DataField="Date_Rec_Cancel_Mistake" HeaderText="วันที่ยกเลิก"
                                                                                                SortExpression="Date_Rec_Cancel_Mistake" UniqueName="Date_Rec_Cancel_Mistake"
                                                                                                DataFormatString="{0:d}">
                                                                                                <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                                            </telerik:GridBoundColumn>
                                                                                            <telerik:GridBoundColumn DataField="Desc_Cancel_Mistake" HeaderText="บันทึกอนุมัติ"
                                                                                                SortExpression="Desc_Cancel_Mistake" UniqueName="Desc_Cancel_Mistake">
                                                                                                <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                                                                            </telerik:GridBoundColumn>
                                                                                        </Columns>
                                                                                    </MasterTableView>
                                                                                    <HeaderStyle HorizontalAlign="Center" Font-Names="Tahoma" Font-Size="10pt" />
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
<asp:TextBox ID="txtCard_Id" runat="server" Visible="False" Width="1px"></asp:TextBox>
<asp:Label ID="lblRoleID" runat="server" Visible="False"></asp:Label>