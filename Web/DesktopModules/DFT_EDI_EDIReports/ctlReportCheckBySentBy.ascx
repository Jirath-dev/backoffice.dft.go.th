<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctlReportCheckBySentBy.ascx.vb"
    Inherits=".ctlReportCheckBySentBy" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
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
                                                <font class="groupbox">&nbsp;รายงานการยื่นคำร้องด้วย Digital Signature&nbsp;</font>
                                            </td>
                                            <telerik:RadCodeBlock ID="RadCodeBlock6" runat="server">
                                                <td style='background-image: url(<%= ResolveUrl("images") %>/CORNER.gif); background-repeat: no-repeat;'>
                                                    &nbsp; &nbsp;&nbsp;
                                                </td>
                                            </telerik:RadCodeBlock>
                                            <td align="right">
                                                &nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td align="right" class="m-frm-nav">
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr class="m-frm-hdr">
                    <td>
                        <table style="margin-bottom: 10px;margin-top:10px;">
                            <tr>
                                <td class="FormLabel">
                                    ประเภทรายงาน
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlSelectReport" runat="server" Width="500px" AutoPostBack="true">
                                        <asp:ListItem Text="สรุปจำนวนการขอฟอร์ม ด้วย Digital Signature ในรอบเดือน" Value="ctlReportByMonth"></asp:ListItem>
                                        <asp:ListItem Text="สรุปจำนวนการขอฟอร์มทั้งหมด แยกตามเดือน" Value="ctlReportSumaryMonth"></asp:ListItem>
                                        <asp:ListItem Text="สรุปจำนวนการขอฟอร์มทั้งหมด ที่ผ่านและไม่ผ่านการตรวจสอบจากเจ้าหน้าที่ แยกตามชื่อบริษัท"
                                            Value="crlReportbyCompany"></asp:ListItem>
                                        <asp:ListItem Text="สรุปจำนวนการขอฟอร์มด้วยไฟล์ XML ที่ผ่านการตรวจสอบจากเจ้าหน้าที่ แยกตามชื่อบริษัท"
                                            Value="ctlReportAllFormByCompany"></asp:ListItem>
                                        <asp:ListItem Text="สรุปจำนวนการขอฟอร์มทั้งหมดของทุกบริษัท แยกรูปแบบการขอฟอร์ม" Value="ctlReportRequestBySentBy"></asp:ListItem>
                                        <asp:ListItem Text="สรุปจำนวนฟอร์มทั้งหมดของทุกบริษัท ที่ผ่านและไม่ผ่านการตรวจสอบโดยเจ้าหน้าที่"
                                            Value="ctlReportCheckBySentBy"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                        <hr />
                        <div style="margin: 10px;">
                            <div style="text-align: center; padding: 5px">
                                <asp:Label runat="server" ID="lblMsg" ForeColor="Red" Visible="false">ไม่พบรายการข้อมูลที่ต้องการแสดง
                                </asp:Label>
                            </div>
                            <div style="padding: 5px">
                                <telerik:RadGrid ID="RadGrid1" Visible="False" runat="server" CellPadding="1" CellSpacing="1"
                                    Skin="Office2007" ShowFooter="True" AutoGenerateColumns="False" GridLines="None">
                                    <MasterTableView>
                                        <RowIndicatorColumn>
                                            <HeaderStyle Width="20px"></HeaderStyle>
                                        </RowIndicatorColumn>
                                        <ExpandCollapseColumn>
                                            <HeaderStyle Width="20px"></HeaderStyle>
                                        </ExpandCollapseColumn>
                                        <FooterStyle Font-Bold="true" HorizontalAlign="Left" ForeColor="Black" />
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="sent_by" HeaderText="รูปแบบการขอฟอร์ม" ReadOnly="True"
                                                SortExpression="sent_by" UniqueName="sent_by">
                                                <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Left" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridNumericColumn Aggregate="Sum" HeaderText="ผ่าน" DataField="total_ok"
                                                DataFormatString="{0:N0}" SortExpression="total_ok" UniqueName="total_ok">
                                                <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Right" />
                                            </telerik:GridNumericColumn>
                                            <telerik:GridNumericColumn Aggregate="Sum" HeaderText="ไม่ผ่าน" DataField="total_err"
                                                DataFormatString="{0:N0}" SortExpression="total_err" UniqueName="total_err">
                                                <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Right" />
                                            </telerik:GridNumericColumn>
                                            <telerik:GridNumericColumn Aggregate="Sum" HeaderText="รวม" DataField="total" DataFormatString="{0:N0}"
                                                SortExpression="total" UniqueName="total">
                                                <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Right" />
                                            </telerik:GridNumericColumn>
                                        </Columns>
                                    </MasterTableView>
                                </telerik:RadGrid>
                            </div>
                            <div style="text-align: right">
                                <asp:ImageButton ID="ImageButton1" Visible="false" ImageUrl="/DesktopModules/DFT_EDI_EDIReports/images/excel.gif"
                                    runat="server" Style="float: right; margin-top: 5px;" AlternateText="Export"
                                    ToolTip="Export" />
                            </div>
                        </div>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
