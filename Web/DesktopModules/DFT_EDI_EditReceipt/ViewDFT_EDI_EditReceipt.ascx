<%@ Control Language="vb" Inherits="NTi.Modules.DFT_EDI_EditReceipt.ViewDFT_EDI_EditReceipt"
    AutoEventWireup="false" Explicit="True" Codebehind="ViewDFT_EDI_EditReceipt.ascx.vb" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
</telerik:RadAjaxManager>
<table border="0" cellpadding="1" cellspacing="0" class="m-frm-bdr" width="100%">
    <tr>
        <td>
            <telerik:RadTabStrip ID="RadTabStrip2" runat="server" Skin="Office2007" Width="100%"
                SelectedIndex="1" MultiPageID="RadMultiPage2">
                <Tabs>
                    <telerik:RadTab TabIndex="0" runat="Server" Text="ใบเสร็จสวัสดิการ (ใบเสร็จเขียว)"
                        Selected="True" Font-Bold="true">
                    </telerik:RadTab>
                    <telerik:RadTab TabIndex="1" runat="Server" Text="ใบเสร็จกองทุน (ใบเสร็จเหลือง)"
                        Font-Bold="true">
                    </telerik:RadTab>
                </Tabs>
            </telerik:RadTabStrip>
        </td>
    </tr>
</table>
<table border="0" cellpadding="1" cellspacing="0" class="m-frm-bdr" width="100%">
    <tr>
        <td>
            <telerik:RadMultiPage ID="RadMultiPage2" runat="server" Width="100%" SelectedIndex="0"
                TabIndex="-1">
                <telerik:RadPageView ID="RadPageView2" runat="server" Width="100%" TabIndex="-1">
                    <table id="Table5" runat="server" border="0" cellpadding="0" cellspacing="3" width="100%">
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
                                                                                <font class="groupbox">&nbsp; ค้นหาใบเสร็จรับเงินที่มีปัญหา &nbsp;</font></td>
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
                                                                    <table border="0" cellpadding="0" cellspacing="2">
                                                                        <tr>
                                                                            <td>
                                                                                <font class="FormLabel">เลขที่ใบเสร็จรับเงิน :</font>
                                                                                <asp:TextBox ID="txtReceiptSearch" runat="server" Font-Bold="True" Font-Names="Tahoma"
                                                                                    Font-Size="12pt" ForeColor="Blue" Width="130px">2009-0000000</asp:TextBox>&nbsp;
                                                                            </td>
                                                                            <td>
                                                                                <font class="FormLabel">สถานที่ออกใบเสร็จ :</font>
                                                                                <telerik:RadComboBox ID="rcbSitePlus" runat="server" EnableLoadOnDemand="True" Skin="Office2007"
                                                                                    TabIndex="7" Width="260px">
                                                                                </telerik:RadComboBox>
                                                                            </td>
                                                                            <td>
                                                                                &nbsp;
                                                                                <asp:Button ID="btnSearch" runat="server" Text="ค้นหา" Width="100px" />
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
                    <table id="tblData1" runat="server" border="0" cellpadding="0" cellspacing="3" width="100%">
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
                                                                                <font class="groupbox">&nbsp; ข้อมูลใบเสร็จรับเงิน &nbsp;</font></td>
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
                                                                            <td>
                                                                                <font class="FormLabel">เลขที่ใบเสร็จ</font>
                                                                            </td>
                                                                            <td>
                                                                                <font class="FormLabel">วันที่ออกใบเสร็จ</font>
                                                                            </td>
                                                                            <td>
                                                                                <font class="FormLabel">ชื่อลูกค้า</font>
                                                                            </td>
                                                                            <td>
                                                                                <font class="FormLabel">สถานที่</font>
                                                                            </td>
                                                                            <td>
                                                                                <font class="FormLabel">ผู้ออกใบเสร็จ</font>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <telerik:RadTextBox ID="txtBill_No" runat="server" EnableEmbeddedSkins="False" ReadOnly="True"
                                                                                    Font-Bold="True" Font-Names="Tahoma" Font-Size="10pt" ForeColor="Blue">
                                                                                </telerik:RadTextBox>
                                                                            </td>
                                                                            <td>
                                                                                <telerik:RadTextBox ID="txtBill_Date" runat="server" EnableEmbeddedSkins="False"
                                                                                    ReadOnly="True" Font-Bold="True" Font-Names="Tahoma" Font-Size="10pt" ForeColor="Blue">
                                                                                </telerik:RadTextBox>
                                                                            </td>
                                                                            <td>
                                                                                <telerik:RadTextBox ID="txtReceipt_Name" runat="server" EnableEmbeddedSkins="False"
                                                                                    Width="350px" Font-Bold="True" Font-Names="Tahoma" Font-Size="10pt" ForeColor="Blue">
                                                                                </telerik:RadTextBox>
                                                                            </td>
                                                                            <td>
                                                                                <telerik:RadTextBox ID="txtSite_ID" runat="server" EnableEmbeddedSkins="False" ReadOnly="True"
                                                                                    Width="70px" Font-Bold="True" Font-Names="Tahoma" Font-Size="10pt" ForeColor="Blue">
                                                                                </telerik:RadTextBox>
                                                                            </td>
                                                                            <td>
                                                                                <telerik:RadTextBox ID="txtReceipt_By" runat="server" EnableEmbeddedSkins="False"
                                                                                    ReadOnly="False" Font-Bold="True" Font-Names="Tahoma" Font-Size="10pt" ForeColor="Blue">
                                                                                </telerik:RadTextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="text-align: right">
                                                                                <font class="FormLabel">หมายเหตุ</font>
                                                                            </td>
                                                                            <td colspan="2">
                                                                                <telerik:RadTextBox ID="txtRemark" runat="server" EnableEmbeddedSkins="False" Width="100%"
                                                                                    Font-Bold="True" Font-Names="Tahoma" Font-Size="10pt" ForeColor="Blue">
                                                                                </telerik:RadTextBox>
                                                                            </td>
                                                                            <td>
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
                    <table id="tblData2" runat="server" border="0" cellpadding="0" cellspacing="3" width="100%">
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
                                                                                <font class="groupbox">&nbsp; รายละเอียดใบเสร็จรับเงิน &nbsp;</font></td>
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
                                                                            <td>
                                                                                <font class="FormLabel">เลขที่คำร้อง</font>
                                                                            </td>
                                                                            <td>
                                                                                <font class="FormLabel">เลขที่หนังสือรับรองฯ</font></td>
                                                                            <td>
                                                                                <font class="FormLabel">จำนวน</font>
                                                                            </td>
                                                                            <td>
                                                                                <font class="FormLabel">ราคา/ชุด</font>
                                                                            </td>
                                                                            <td>
                                                                                <font class="FormLabel">เป็นเงิน</font>
                                                                            </td>
                                                                            <td>
                                                                                <font class="FormLabel">ยอดเงินรวม (ตัวอักษร)</font>
                                                                            </td>
                                                                            <td>
                                                                                <font class="FormLabel">rpt_set</font>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <telerik:RadTextBox ID="txtInvh_run_auto" runat="server" EnableEmbeddedSkins="False"
                                                                                    ReadOnly="True" SelectionOnFocus="SelectAll">
                                                                                </telerik:RadTextBox>
                                                                            </td>
                                                                            <td>
                                                                                <telerik:RadTextBox ID="txtReference_code2" runat="server" EnableEmbeddedSkins="False"
                                                                                    Width="125px" ReadOnly="True" SelectionOnFocus="SelectAll">
                                                                                </telerik:RadTextBox>
                                                                            </td>
                                                                            <td>
                                                                                <telerik:RadTextBox ID="txtTotal_set" runat="server" EnableEmbeddedSkins="False"
                                                                                    Text="1" Width="50px" ReadOnly="True" SelectionOnFocus="SelectAll">
                                                                                    <EnabledStyle HorizontalAlign="Right" />
                                                                                </telerik:RadTextBox>
                                                                            </td>
                                                                            <td>
                                                                                <telerik:RadTextBox ID="txtSet_price" runat="server" EnableEmbeddedSkins="False"
                                                                                    Text="30.00" Width="70px" ReadOnly="True" SelectionOnFocus="SelectAll">
                                                                                    <EnabledStyle HorizontalAlign="Right" />
                                                                                </telerik:RadTextBox>
                                                                            </td>
                                                                            <td>
                                                                                <telerik:RadTextBox ID="txtAmt" runat="server" EnableEmbeddedSkins="False" Text="30.00"
                                                                                    Width="100px" ReadOnly="True" SelectionOnFocus="SelectAll">
                                                                                    <EnabledStyle HorizontalAlign="Right" />
                                                                                </telerik:RadTextBox>
                                                                            </td>
                                                                            <td>
                                                                                <telerik:RadTextBox ID="txtAmt_bahttext" runat="server" EnableEmbeddedSkins="False"
                                                                                    Width="300px" ReadOnly="True" SelectionOnFocus="SelectAll">
                                                                                </telerik:RadTextBox>
                                                                            </td>
                                                                            <td>
                                                                                <telerik:RadTextBox ID="txtRpt_set" runat="server" EnableEmbeddedSkins="False" Width="50px"
                                                                                    Enabled="False" ReadOnly="True" Text="1">
                                                                                    <EnabledStyle HorizontalAlign="Center" />
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
                    <table id="tblData3" runat="server" border="0" cellpadding="0" cellspacing="3" width="100%">
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
                                                                                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                                                                                    <tr>
                                                                                        <td style="width: 70%">
                                                                                            <asp:Button ID="btnInsert" runat="server" Text="เพิ่มข้อมูล" Width="120px" /><asp:Button
                                                                                                ID="btnUpdate" runat="server" Text="แก้ไขข้อมูล" Width="120px" /><asp:Button ID="btnDelete"
                                                                                                    runat="server" Text="ลบข้อมูล" Width="120px" />
                                                                                            <asp:Button ID="btnCreateReceive" runat="server" Text="สร้างใบเสร็จ (กรณีพิมพ์หนังสือฯเดิม)"
                                                                                                Width="200px" /></td>
                                                                                        <td style="width: 30%; text-align: right;">
                                                                                            <asp:Button ID="btnOK" runat="server" Text="ตกลง" Width="120px" /><asp:Button ID="btnCancel"
                                                                                                runat="server" Text="ยกเลิก" Width="120px" /></td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <telerik:RadGrid ID="rgBill_Receipt_Detail" runat="server" AllowSorting="True" Font-Names="Tahoma"
                                                                                    Font-Size="X-Small" GridLines="None" Skin="Office2007" TabIndex="-1">
                                                                                    <MasterTableView AutoGenerateColumns="False" DataKeyNames="invh_run_auto,reference_code2,total_set,set_price,amt,amt_bahttext,rpt_set"
                                                                                        NoMasterRecordsText="ไม่มีคำร้องขอออกหนังสือรับรองถิ่นกำเนิดสินค้า" Width="100%">
                                                                                        <Columns>
                                                                                            <telerik:GridTemplateColumn UniqueName="TemplateColumn" HeaderText="เลขที่หนังสือ">
                                                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                                                <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                                                <ItemTemplate>
                                                                                                    <asp:LinkButton ID="lbtInvh_run_auto" runat="server" Text='<%#EVAL("invh_run_auto")%>'
                                                                                                        CommandName="SELECTED"></asp:LinkButton>
                                                                                                </ItemTemplate>
                                                                                            </telerik:GridTemplateColumn>
                                                                                            <telerik:GridBoundColumn DataField="invh_run_auto" HeaderText="เลขที่คำร้อง" ReadOnly="True"
                                                                                                SortExpression="invh_run_auto" UniqueName="invh_run_auto" Visible="False">
                                                                                                <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                                            </telerik:GridBoundColumn>
                                                                                            <telerik:GridBoundColumn DataField="reference_code2" HeaderText="เลขที่หนังสือรับรองฯ"
                                                                                                ReadOnly="True" SortExpression="reference_code2" UniqueName="reference_code2">
                                                                                                <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                                            </telerik:GridBoundColumn>
                                                                                            <telerik:GridBoundColumn DataField="total_set" HeaderText="จำนวน" ReadOnly="True"
                                                                                                SortExpression="total_set" UniqueName="total_set">
                                                                                                <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Right" />
                                                                                            </telerik:GridBoundColumn>
                                                                                            <telerik:GridBoundColumn DataField="set_price" HeaderText="ราคา/ชุด" ReadOnly="True"
                                                                                                SortExpression="set_price" UniqueName="set_price">
                                                                                                <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Right" />
                                                                                            </telerik:GridBoundColumn>
                                                                                            <telerik:GridBoundColumn DataField="amt" HeaderText="เป็นเงิน" ReadOnly="True" SortExpression="amt"
                                                                                                UniqueName="amt">
                                                                                                <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Right" />
                                                                                            </telerik:GridBoundColumn>
                                                                                            <telerik:GridBoundColumn DataField="amt_bahttext" HeaderText="ยอดเงินรวม (ตัวอักษร)"
                                                                                                ReadOnly="True" SortExpression="amt_bahttext" UniqueName="amt_bahttext">
                                                                                                <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                                                                            </telerik:GridBoundColumn>
                                                                                            <telerik:GridBoundColumn DataField="rpt_set" HeaderText="rpt_set" ReadOnly="True"
                                                                                                SortExpression="rpt_set" UniqueName="rpt_set">
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
                </telerik:RadPageView>
                <telerik:RadPageView ID="RadPageView3" runat="server" Width="100%" TabIndex="-1">
                    <table id="Table4" runat="server" border="0" cellpadding="0" cellspacing="3" width="100%">
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
                                                                                <font class="groupbox">&nbsp; ค้นหาใบเสร็จรับเงินที่มีปัญหา &nbsp;</font></td>
                                                                            <telerik:RadCodeBlock ID="RadCodeBlock4" runat="server">
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
                                                                    <table border="0" cellpadding="0" cellspacing="2">
                                                                        <tr>
                                                                            <td>
                                                                                <font class="FormLabel">เลขที่ใบเสร็จรับเงิน :</font>
                                                                                <asp:TextBox ID="txtReceiptSearch_v2" runat="server" Font-Bold="True" Font-Names="Tahoma"
                                                                                    Font-Size="12pt" ForeColor="Blue" Width="130px">2009-0000000</asp:TextBox>&nbsp;
                                                                            </td>
                                                                            <td>
                                                                                <font class="FormLabel">สถานที่ออกใบเสร็จ :</font>
                                                                                <telerik:RadComboBox ID="rcbSitePlus_v2" runat="server" EnableLoadOnDemand="True"
                                                                                    Skin="Office2007" TabIndex="7" Width="260px">
                                                                                </telerik:RadComboBox>
                                                                            </td>
                                                                            <td>
                                                                                &nbsp;
                                                                                <asp:Button ID="btnSearch_v2" runat="server" Text="ค้นหา" Width="100px" />
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
                    <table id="Table1" runat="server" border="0" cellpadding="0" cellspacing="3" width="100%">
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
                                                                                <font class="groupbox">&nbsp; ข้อมูลใบเสร็จรับเงิน &nbsp;</font></td>
                                                                            <telerik:RadCodeBlock ID="RadCodeBlock5" runat="server">
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
                                                                            <td>
                                                                                <font class="FormLabel">เลขที่ใบเสร็จ</font>
                                                                            </td>
                                                                            <td>
                                                                                <font class="FormLabel">วันที่ออกใบเสร็จ</font>
                                                                            </td>
                                                                            <td>
                                                                                <font class="FormLabel">ชื่อลูกค้า</font>
                                                                            </td>
                                                                            <td>
                                                                                <font class="FormLabel">สถานที่</font>
                                                                            </td>
                                                                            <td>
                                                                                <font class="FormLabel">ผู้ออกใบเสร็จ</font>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <telerik:RadTextBox ID="txtBill_No_v2" runat="server" EnableEmbeddedSkins="False"
                                                                                    ReadOnly="True" Font-Bold="True" Font-Names="Tahoma" Font-Size="10pt" ForeColor="Blue">
                                                                                </telerik:RadTextBox>
                                                                            </td>
                                                                            <td>
                                                                                <telerik:RadTextBox ID="txtBill_Date_v2" runat="server" EnableEmbeddedSkins="False"
                                                                                    ReadOnly="True" Font-Bold="True" Font-Names="Tahoma" Font-Size="10pt" ForeColor="Blue">
                                                                                </telerik:RadTextBox>
                                                                            </td>
                                                                            <td>
                                                                                <telerik:RadTextBox ID="txtReceipt_Name_v2" runat="server" EnableEmbeddedSkins="False"
                                                                                    Width="350px" Font-Bold="True" Font-Names="Tahoma" Font-Size="10pt" ForeColor="Blue">
                                                                                </telerik:RadTextBox>
                                                                            </td>
                                                                            <td>
                                                                                <telerik:RadTextBox ID="txtSite_ID_v2" runat="server" EnableEmbeddedSkins="False"
                                                                                    ReadOnly="True" Width="70px" Font-Bold="True" Font-Names="Tahoma" Font-Size="10pt"
                                                                                    ForeColor="Blue">
                                                                                </telerik:RadTextBox>
                                                                            </td>
                                                                            <td>
                                                                                <telerik:RadTextBox ID="txtReceipt_By_v2" runat="server" EnableEmbeddedSkins="False"
                                                                                    ReadOnly="False" Font-Bold="True" Font-Names="Tahoma" Font-Size="10pt" ForeColor="Blue">
                                                                                </telerik:RadTextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="text-align: right">
                                                                                <font class="FormLabel">หมายเหตุ</font>
                                                                            </td>
                                                                            <td colspan="2">
                                                                                <telerik:RadTextBox ID="txtRemark_v2" runat="server" EnableEmbeddedSkins="False"
                                                                                    Width="100%" Font-Bold="True" Font-Names="Tahoma" Font-Size="10pt" ForeColor="Blue">
                                                                                </telerik:RadTextBox>
                                                                            </td>
                                                                            <td>
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
                    <table id="Table2" runat="server" border="0" cellpadding="0" cellspacing="3" width="100%">
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
                                                                                <font class="groupbox">&nbsp; รายละเอียดใบเสร็จรับเงิน &nbsp;</font></td>
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
                                                                <td width="15">
                                                                    &nbsp;</td>
                                                                <td>
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <font class="FormLabel">เลขที่คำร้อง</font>
                                                                            </td>
                                                                            <td>
                                                                                <font class="FormLabel">เลขที่หนังสือรับรองฯ</font></td>
                                                                            <td>
                                                                                <font class="FormLabel">จำนวน</font>
                                                                            </td>
                                                                            <td>
                                                                                <font class="FormLabel">ราคา/ชุด</font>
                                                                            </td>
                                                                            <td>
                                                                                <font class="FormLabel">เป็นเงิน</font>
                                                                            </td>
                                                                            <td>
                                                                                <font class="FormLabel">ยอดเงินรวม (ตัวอักษร)</font>
                                                                            </td>
                                                                            <td>
                                                                                <font class="FormLabel">rpt_set</font>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <telerik:RadTextBox ID="txtInvh_run_auto_v2" runat="server" EnableEmbeddedSkins="False"
                                                                                    ReadOnly="True" SelectionOnFocus="SelectAll">
                                                                                </telerik:RadTextBox>
                                                                            </td>
                                                                            <td>
                                                                                <telerik:RadTextBox ID="txtReference_code2_v2" runat="server" EnableEmbeddedSkins="False"
                                                                                    Width="125px" ReadOnly="True" SelectionOnFocus="SelectAll">
                                                                                </telerik:RadTextBox>
                                                                            </td>
                                                                            <td>
                                                                                <telerik:RadTextBox ID="txtTotal_set_v2" runat="server" EnableEmbeddedSkins="False"
                                                                                    Text="1" Width="50px" ReadOnly="True" SelectionOnFocus="SelectAll">
                                                                                    <EnabledStyle HorizontalAlign="Right" />
                                                                                </telerik:RadTextBox>
                                                                            </td>
                                                                            <td>
                                                                                <telerik:RadTextBox ID="txtSet_price_v2" runat="server" EnableEmbeddedSkins="False"
                                                                                    Text="30.00" Width="70px" ReadOnly="True" SelectionOnFocus="SelectAll">
                                                                                    <EnabledStyle HorizontalAlign="Right" />
                                                                                </telerik:RadTextBox>
                                                                            </td>
                                                                            <td>
                                                                                <telerik:RadTextBox ID="txtAmt_v2" runat="server" EnableEmbeddedSkins="False" Text="30.00"
                                                                                    Width="100px" ReadOnly="True" SelectionOnFocus="SelectAll">
                                                                                    <EnabledStyle HorizontalAlign="Right" />
                                                                                </telerik:RadTextBox>
                                                                            </td>
                                                                            <td>
                                                                                <telerik:RadTextBox ID="txtAmt_bahttext_v2" runat="server" EnableEmbeddedSkins="False"
                                                                                    Width="300px" ReadOnly="True" SelectionOnFocus="SelectAll">
                                                                                </telerik:RadTextBox>
                                                                            </td>
                                                                            <td>
                                                                                <telerik:RadTextBox ID="txtRpt_set_v2" runat="server" EnableEmbeddedSkins="False"
                                                                                    Width="50px" Enabled="False" ReadOnly="True" Text="1">
                                                                                    <EnabledStyle HorizontalAlign="Center" />
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
                    <table id="Table3" runat="server" border="0" cellpadding="0" cellspacing="3" width="100%">
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
                                                                                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                                                                                    <tr>
                                                                                        <td style="width: 70%">
                                                                                            <asp:Button ID="btnInsert_v2" runat="server" Text="เพิ่มข้อมูล" Width="120px" /><asp:Button
                                                                                                ID="btnUpdate_v2" runat="server" Text="แก้ไขข้อมูล" Width="120px" /><asp:Button ID="btnDelete_v2"
                                                                                                    runat="server" Text="ลบข้อมูล" Width="120px" />
                                                                                            <asp:Button ID="btnCreateReceive_v2" runat="server" Text="สร้างใบเสร็จ (กรณีพิมพ์หนังสือฯเดิม)"
                                                                                                Width="200px" />
                                                                                        </td>
                                                                                        <td style="width: 30%; text-align: right;">
                                                                                            <asp:Button ID="btnOK_v2" runat="server" Text="ตกลง" Width="120px" /><asp:Button
                                                                                                ID="btnCancel_v2" runat="server" Text="ยกเลิก" Width="120px" />
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <telerik:RadGrid ID="rgBill_Receipt_Detail_v2" runat="server" AllowSorting="True"
                                                                                    Font-Names="Tahoma" Font-Size="X-Small" GridLines="None" Skin="Office2007" TabIndex="-1">
                                                                                    <MasterTableView AutoGenerateColumns="False" DataKeyNames="invh_run_auto,reference_code2,total_set,set_price,amt,amt_bahttext,rpt_set"
                                                                                        NoMasterRecordsText="ไม่มีคำร้องขอออกหนังสือรับรองถิ่นกำเนิดสินค้า" Width="100%">
                                                                                        <Columns>
                                                                                            <telerik:GridTemplateColumn UniqueName="TemplateColumn" HeaderText="เลขที่หนังสือ">
                                                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                                                <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                                                <ItemTemplate>
                                                                                                    <asp:LinkButton ID="lbtInvh_run_auto" runat="server" Text='<%#EVAL("invh_run_auto")%>'
                                                                                                        CommandName="SELECTED"></asp:LinkButton>
                                                                                                </ItemTemplate>
                                                                                            </telerik:GridTemplateColumn>
                                                                                            <telerik:GridBoundColumn DataField="invh_run_auto" HeaderText="เลขที่คำร้อง" ReadOnly="True"
                                                                                                SortExpression="invh_run_auto" UniqueName="invh_run_auto" Visible="False">
                                                                                                <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                                            </telerik:GridBoundColumn>
                                                                                            <telerik:GridBoundColumn DataField="reference_code2" HeaderText="เลขที่หนังสือรับรองฯ"
                                                                                                ReadOnly="True" SortExpression="reference_code2" UniqueName="reference_code2">
                                                                                                <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                                            </telerik:GridBoundColumn>
                                                                                            <telerik:GridBoundColumn DataField="total_set" HeaderText="จำนวน" ReadOnly="True"
                                                                                                SortExpression="total_set" UniqueName="total_set">
                                                                                                <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Right" />
                                                                                            </telerik:GridBoundColumn>
                                                                                            <telerik:GridBoundColumn DataField="set_price" HeaderText="ราคา/ชุด" ReadOnly="True"
                                                                                                SortExpression="set_price" UniqueName="set_price">
                                                                                                <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Right" />
                                                                                            </telerik:GridBoundColumn>
                                                                                            <telerik:GridBoundColumn DataField="amt" HeaderText="เป็นเงิน" ReadOnly="True" SortExpression="amt"
                                                                                                UniqueName="amt">
                                                                                                <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Right" />
                                                                                            </telerik:GridBoundColumn>
                                                                                            <telerik:GridBoundColumn DataField="amt_bahttext" HeaderText="ยอดเงินรวม (ตัวอักษร)"
                                                                                                ReadOnly="True" SortExpression="amt_bahttext" UniqueName="amt_bahttext">
                                                                                                <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                                                                            </telerik:GridBoundColumn>
                                                                                            <telerik:GridBoundColumn DataField="rpt_set" HeaderText="rpt_set" ReadOnly="True"
                                                                                                SortExpression="rpt_set" UniqueName="rpt_set">
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
                </telerik:RadPageView>
            </telerik:RadMultiPage>
        </td>
    </tr>
</table>
<asp:Label ID="lblRoleID" runat="server" Visible="False"></asp:Label>
<asp:Button ID="btnGetData1" runat="server" BackColor="Transparent" BorderColor="Transparent"
    BorderStyle="None" TabIndex="-1" Width="10px" />
<asp:Button ID="btnGetData2" runat="server" BackColor="Transparent" BorderColor="Transparent"
    BorderStyle="None" TabIndex="-1" Width="10px" />
<asp:Button ID="btnGetData1_v2" runat="server" BackColor="Transparent" BorderColor="Transparent"
    BorderStyle="None" TabIndex="-1" Width="10px" />
<asp:Button ID="btnGetData2_v2" runat="server" BackColor="Transparent" BorderColor="Transparent"
    BorderStyle="None" TabIndex="-1" Width="10px" />
