<%@ Control Language="vb" Inherits="NTi.Modules.DFT_EDI_AttachDoc_V2.ViewDFT_EDI_AttachDoc_V2"
    AutoEventWireup="false" Explicit="True" Codebehind="ViewDFT_EDI_AttachDoc_V2.ascx.vb" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
</telerik:RadAjaxManager>
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
                                                Text="รายละเอียดข้อมูลบริษัท และข้อมูลผู้ถือบัตร">
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
                                                                                <font class="groupbox">&nbsp; ข้อมูลนิติบุคคล &nbsp;</font></td>
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
                                                                                <font class="FormLabel">ชื่อบริษัท (ไทย):&nbsp;</font>
                                                                                <telerik:RadTextBox ID="txtCompanyName_Th" runat="server" CssClass="FormFld" EnableEmbeddedSkins="False"
                                                                                    ReadOnly="True" TabIndex="-1" Width="315px">
                                                                                </telerik:RadTextBox>
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
                                                                                        <td>
                                                                                            <font class="FormLabel">2.</font>
                                                                                            <telerik:RadTextBox ID="txtAuthorize2" runat="server" CssClass="FormFld" EnableEmbeddedSkins="False"
                                                                                                ReadOnly="True" TabIndex="-1" Width="250px">
                                                                                            </telerik:RadTextBox>
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
                                                                                        <td>
                                                                                            <font class="FormLabel">5.</font>
                                                                                            <telerik:RadTextBox ID="txtAuthorize5" runat="server" CssClass="FormFld" EnableEmbeddedSkins="False"
                                                                                                ReadOnly="True" TabIndex="-1" Width="250px">
                                                                                            </telerik:RadTextBox>
                                                                                        </td>
                                                                                        <td>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <font class="FormLabel">หมายเหตุกรรมการ:&nbsp;</font>
                                                                                <telerik:RadTextBox ID="txtAuthorize_Remark" runat="server" CssClass="FormFld" EnableEmbeddedSkins="False"
                                                                                    ReadOnly="True" TabIndex="-1" Width="700px">
                                                                                </telerik:RadTextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <font class="FormLabel">ชื่อผู้ถือบัตร:</font>
                                                                                <telerik:RadTextBox ID="txtAuthName" runat="server" CssClass="FormFld" EnableEmbeddedSkins="False"
                                                                                    ReadOnly="True" TabIndex="-1" Width="250px">
                                                                                </telerik:RadTextBox>
                                                                                <font class="FormLabel">&nbsp;หมายเลขบัตร:</font>
                                                                                <telerik:RadTextBox ID="txtCARD_ID" runat="server" CssClass="FormFld" EnableEmbeddedSkins="False"
                                                                                    ReadOnly="True" TabIndex="-1" Width="150px">
                                                                                </telerik:RadTextBox>
                                                                                <font class="FormLabel">ระดับบัตร:</font>
                                                                                <telerik:RadTextBox ID="txtCard_Level" runat="server" CssClass="FormFld" EnableEmbeddedSkins="False"
                                                                                    ReadOnly="True" TabIndex="-1" Width="200px">
                                                                                </telerik:RadTextBox></td>
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
                                                <tr>
                                                    <td>
                                                        <asp:Button ID="btnAttachDoc" runat="server" Text="บันทึกการแนบเอกสาร" Visible="False" /></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <table border="0" cellpadding="1" cellspacing="0" class="m-frm-bdr" width="100%">
                                                            <tr>
                                                                <td>
                                                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                        <tr>
                                                                            <td class="m-frm" valign="top" width="100%">
                                                                                <telerik:RadTabStrip ID="RadTabStrip2" runat="server" Font-Names="Tahoma" Font-Size="10pt"
                                                                                    MultiPageID="RadMultiPage2" SelectedIndex="0" Skin="Office2007" Width="100%">
                                                                                    <Tabs>
                                                                                        <telerik:RadTab runat="server" Font-Names="Tahoma" Font-Size="10pt" Selected="True"
                                                                                            Text="รายการคำร้องเกินกำหนดการแนบเอกสาร 10 วัน">
                                                                                        </telerik:RadTab>
                                                                                    </Tabs>
                                                                                </telerik:RadTabStrip>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="m-frm" valign="top" width="100%">
                                                                                <telerik:RadMultiPage ID="RadMultiPage2" runat="server" SelectedIndex="0" TabIndex="-1"
                                                                                    Width="100%">
                                                                                    <telerik:RadPageView ID="RadPageView2" runat="server" Width="100%">
                                                                                        <telerik:RadGrid ID="rgAttachDocList" runat="server" AllowSorting="True" GridLines="None"
                                                                                            ShowFooter="True" ShowStatusBar="True" Skin="Office2007" TabIndex="-1" Width="100%"
                                                                                            AllowPaging="True" PageSize="20">
                                                                                            <MasterTableView AutoGenerateColumns="False" NoMasterRecordsText="ไม่มีรายการเอกสารแนบตามเงื่อนไขที่ทำการค้นหา"
                                                                                                ClientDataKeyNames="invh_run_auto" DataKeyNames="invh_run_auto" Width="100%">
                                                                                                <GroupByExpressions>
                                                                                                    <telerik:GridGroupByExpression>
                                                                                                        <SelectFields>
                                                                                                            <telerik:GridGroupByField FieldAlias="form_name" FieldName="form_name" HeaderText=" " FormatString="" />
                                                                                                        </SelectFields>
                                                                                                        <GroupByFields>
                                                                                                            <telerik:GridGroupByField FieldAlias="form_name" FieldName="form_name" SortOrder="None" FormatString="" />
                                                                                                        </GroupByFields>
                                                                                                    </telerik:GridGroupByExpression>
                                                                                                </GroupByExpressions>
                                                                                                <Columns>
                                                                                                    <%--<telerik:GridTemplateColumn UniqueName="CheckBoxTemplateColumn">
                                                                                                        <HeaderStyle Width="15px" HorizontalAlign="Center" />
                                                                                                        <HeaderTemplate>
                                                                                                            <asp:CheckBox ID="headerChkbox" runat="server" OnCheckedChanged="ToggleSelectedState" />
                                                                                                        </HeaderTemplate>
                                                                                                        <ItemStyle Width="15px" HorizontalAlign="Center" />
                                                                                                        <ItemTemplate>
                                                                                                            <asp:CheckBox ID="chkSelect" runat="server" OnCheckedChanged="ToggleRowSelection" />
                                                                                                        </ItemTemplate>
                                                                                                    </telerik:GridTemplateColumn>--%>
                                                                                                    <telerik:GridTemplateColumn UniqueName="TemplateColumn" HeaderText="เลขที่หนังสือ">
                                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                                        <ItemStyle Font-Names="Tahoma" Width="100px" Font-Size="10pt" HorizontalAlign="Center" />
                                                                                                        <ItemTemplate>
                                                                                                            <asp:LinkButton ID="lbtReference_Code2" runat="server" Text='<%#EVAL("reference_code2")%>' CommandName="ATTACH"></asp:LinkButton>
                                                                                                        </ItemTemplate>
                                                                                                    </telerik:GridTemplateColumn>
                                                                                                    <telerik:GridNumericColumn Aggregate="Count" DataField="invh_run_auto" FooterText="รวม : "
                                                                                                        HeaderText="เลขที่อ้างอิง" ReadOnly="True" SortExpression="invh_run_auto" UniqueName="invh_run_auto">
                                                                                                        <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                                                        <FooterStyle Font-Names="Tahoma" Font-Size="10pt" />
                                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                                    </telerik:GridNumericColumn>
                                                                                                    <telerik:GridBoundColumn DataField="approve_date" DataFormatString="{0:dd/MM/yyyy}"
                                                                                                        HeaderText="วันอนุมัติ" ReadOnly="True" SortExpression="approve_date" UniqueName="approve_date">
                                                                                                        <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                                                    </telerik:GridBoundColumn>
                                                                                                    <telerik:GridTemplateColumn HeaderText="หมายเลข Invoice" UniqueName="TemplateColumn">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="lblInvoice_No" runat="server" Text='<%#GetInvoice_No(Eval("invh_run_auto")) %>'></asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                        <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                                                                                    </telerik:GridTemplateColumn>
                                                                                                    <telerik:GridBoundColumn DataField="form_name" HeaderText="ฟอร์ม" ReadOnly="True"
                                                                                                        SortExpression="form_name" UniqueName="form_name">
                                                                                                        <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                                                                                    </telerik:GridBoundColumn>
                                                                                                    <telerik:GridBoundColumn DataField="OverDay" HeaderText="เกินกำหนด(วัน)" ReadOnly="True"
                                                                                                        SortExpression="OverDay" UniqueName="OverDay">
                                                                                                        <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Right" />
                                                                                                    </telerik:GridBoundColumn>
                                                                                                    <telerik:GridBoundColumn DataField="rep_doc_date" DataFormatString="{0:dd/MM/yyyy}"
                                                                                                        HeaderText="วันที่บันทึกการแนบ ฯ" ReadOnly="True" SortExpression="rep_doc_date"
                                                                                                        UniqueName="rep_doc_date">
                                                                                                        <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                                                    </telerik:GridBoundColumn>
                                                                                                    <telerik:GridBoundColumn DataField="rep_by" HeaderText="ผู้บันทึกการแนบ ฯ" ReadOnly="True"
                                                                                                        SortExpression="rep_by" UniqueName="rep_by">
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
                                                                                        </telerik:RadGrid></telerik:RadPageView>
                                                                                </telerik:RadMultiPage></td>
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