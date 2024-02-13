<%@ Control language="vb" Inherits="Nti.Modules.DFT_EDI_AddPrint.ViewDFT_EDI_AddPrint" AutoEventWireup="false" Explicit="True" Codebehind="ViewDFT_EDI_AddPrint.ascx.vb" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="dnn" TagName="Audit" Src="~/controls/ModuleAuditControl.ascx" %>
<telerik:RadCodeBlock ID="RadCodeBlockFirst" runat="server">
<script type="text/javascript">
function ShowInsertForm3() {
                        return window.confirm('ต้องการบันทึก คลิกปุ่ม OK ไม่ต้องการ คลิกปุ่ม Cancel');
                        return false;
                }
</script>
</telerik:RadCodeBlock>
<%--<telerik:RadWindowManager ID="RadWindowManager1" runat="server" Behavior="Close">
<Windows>
<telerik:RadWindow ID="InsertDialog" runat="server" Title="ออกใบเสร็จ" ReloadOnShow="True" ShowContentDuringLoad="False" Modal="True"
        VisibleStatusbar="False" Width="900px" Height="550px" style="display:none;" Behavior="Default" InitialBehavior="None" Left="" NavigateUrl="" Top="" />
</Windows>
</telerik:RadWindowManager>--%>
<table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
    <tr>
        <td style="width: 100%">
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr class="m-frm-hdr">
                    <td align="left" class="m-frm-hdr">
                        <table border="0" cellpadding="0" cellspacing="0" cols="2">
                            <tr>
                                <td class="groupboxheader" nowrap="nowrap">
                                    <font class="groupbox">เพิ่มรายชื่อเจ้าหน้าที่และเพิ่มให้ใช้เครื่องพิมพ์คำขอ &nbsp;</font></td>
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
        <td style="width: 100%">
            <asp:Panel ID="PanelCase" runat="server" BorderColor="LightSteelBlue" CssClass="FormLabel"
                GroupingText="เลือกเงื่อนไขการใช้งาน" Width="100%">
                <asp:RadioButtonList ID="rdoCase" runat="server" AutoPostBack="True">
                    <asp:ListItem Selected="True" Value="1">เพิ่มเจ้าหน้าที่เพื่อให้ใช้งานเครื่องพิมพ์</asp:ListItem>
                    <asp:ListItem Value="2">ลบเจ้าหน้าที่</asp:ListItem>
                </asp:RadioButtonList></asp:Panel>
        </td>
    </tr>
    <tr>
        <td style="width: 100%">
            <asp:Panel ID="PanelReq" runat="server" Width="100%" CssClass="FormLabel" GroupingText="กรณีเพิ่มเจ้าหน้าที่เพื่อให้ใช้งานเครื่องพิมพ์" Visible="False">
                <table border="0" cellpadding="0" cellspacing="3" style="width: 100%">
                    <tr>
                        <td style="width: 40%" class="FormLabel">
                        </td>
                        <td style="width: 100px">
                        </td>
                        <td style="width: 100px">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 40%" class="FormLabel">
                            ชื่อ UserName ที่ตั้งตอนเข้าระบบ (ตัวอักษรเล็กใหญ่มีผล)</td>
                        <td style="width: 100px">
                            <asp:TextBox ID="txtAddUserName" runat="server"></asp:TextBox></td>
                        <td style="width: 100px">
                        </td>
                    </tr>
                    <tr>
                        <td class="FormLabel" style="width: 40%">
                            เลือกสาขา</td>
                        <td style="width: 100px">
                            <asp:DropDownList ID="DDL_Site" runat="server" AutoPostBack="True" Width="321px">
                            </asp:DropDownList></td>
                        <td style="width: 100px">
                        </td>
                    </tr>
                    <tr>
                        <td class="FormLabel" style="width: 40%; height: 24px;">
                            ชื่อ UserName ที่จะทำการ Copy เครื่องพิมพ์</td>
                        <td style="width: 100px; height: 24px;">
                            <asp:DropDownList ID="DDLUserName" runat="server" AutoPostBack="True" Width="321px">
                            </asp:DropDownList></td>
                        <td style="width: 100px; height: 24px;">
                            <asp:Button ID="btnAdd" runat="server" Text="เพิ่ม UserName เครื่องพิมพ์" /></td>
                    </tr>
                    <tr>
                        <td style="width: 40%" class="FormLabel">
                            จำนวนเครื่องพิมพ์ทั้งหมดของสาขา</td>
                        <td style="width: 100px">
                            <asp:TextBox ID="txtALLPrintSite" runat="server" Enabled="False"></asp:TextBox></td>
                        <td style="width: 100px">
                        </td>
                    </tr>
                    <tr>
                        <td class="FormLabel" style="width: 40%">
                            เครื่องที่ให้เป็น Default</td>
                        <td style="width: 100px">
                            <asp:TextBox ID="txtFixPrint" runat="server" Enabled="False"></asp:TextBox></td>
                        <td style="width: 100px">
                        </td>
                    </tr>
                    <tr>
                        <td class="FormLabel" style="width: 40%; height: 77px" valign="top">
                            หมายเหตุ</td>
                        <td style="width: 100px; height: 77px" valign="top">
                            <asp:TextBox ID="txtRemark" runat="server" Height="57px" TextMode="MultiLine" Width="307px"></asp:TextBox></td>
                        <td style="width: 100px; height: 77px">
                        </td>
                    </tr>
                    <tr>
                        <td class="FormLabel" colspan="3">
                            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                                <tr>
                                    <td align="center" style="width: 100%">
                                        <asp:Label ID="lblErrorPage" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="FormLabel" colspan="3">
                            *** หมายเหตุ : กรณีที่จะย้ายเจ้าหน้าที่จากอีกสาขา ไปอีกสาขาให้ทำการลบชื่อเจ้าหน้าที่จากสาขาเดิมให้เรียบร้อยก่อน
                            แล้วถึงจะเพิ่มเจ้าหน้าที่ใหม่</td>
                    </tr>
                    <tr>
                        <td class="FormLabel" style="width: 40%">
                        </td>
                        <td style="width: 100px">
                        </td>
                        <td style="width: 100px">
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel ID="PanelDel" runat="server" BackColor="Gainsboro" CssClass="FormLabel"
                GroupingText="กรณีลบข้อมูลเจ้าหน้าที่" Visible="False" Width="100%">
                <table border="0" cellpadding="0" cellspacing="3" style="width: 100%">
                    <tr>
                        <td style="width: 40%" class="FormLabel">
                        </td>
                        <td style="width: 100px">
                        </td>
                        <td style="width: 100px">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 40%" class="FormLabel">
                            ชื่อ UserName ที่ตั้งตอนเข้าระบบ (ตัวอักษรเล็กใหญ่มีผล)</td>
                        <td style="width: 321px">
                            <asp:TextBox ID="txtDelUserName" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDelUserName"
                                Display="Dynamic" ErrorMessage="กรุณาป้อนชื่อ UserName" Font-Names="Tahoma" Font-Size="10pt"
                                SetFocusOnError="True" ValidationGroup="searchDel"></asp:RequiredFieldValidator></td>
                        <td style="width: 100px">
                            <asp:Button ID="btnSearchDel" runat="server" Text="ค้นหา UserName เจ้าหน้าที่" ValidationGroup="searchDel" /></td>
                    </tr>
                    <tr>
                        <td class="FormLabel" style="width: 40%">
                            สาขา</td>
                        <td style="width: 321px">
                            <asp:DropDownList ID="DDLDelSite" runat="server" Width="321px">
                            </asp:DropDownList></td>
                        <td style="width: 100px">
                        </td>
                    </tr>
                    <tr>
                        <td class="FormLabel" style="width: 40%">
                            รายการของเครื่องคำขอ</td>
                        <td style="width: 321px">
                        </td>
                        <td style="width: 100px">
                        </td>
                    </tr>
                    <tr>
                        <td class="FormLabel" colspan="3">
                            <telerik:RadGrid ID="rgRequestForm" runat="server" AllowMultiRowSelection="True"
                                AllowSorting="True" GridLines="None" ShowFooter="True" ShowStatusBar="True" Skin="Office2007"
                                TabIndex="-1" Width="100%">
                                <MasterTableView AutoGenerateColumns="False" ClientDataKeyNames="AutoID" DataKeyNames="AutoID"
                                    NoMasterRecordsText="ไม่มีรายการ">
                                    <Columns>
                                        <telerik:GridNumericColumn Aggregate="Count" DataField="AutoID" FooterText="รวม : "
                                            HeaderText="AutoID" ReadOnly="True" SortExpression="AutoID" UniqueName="AutoID" Visible="False">
                                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                            <FooterStyle Font-Names="Tahoma" Font-Size="10pt" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </telerik:GridNumericColumn>
                                        <telerik:GridBoundColumn DataField="site_id" HeaderText="สาขา"
                                            ReadOnly="True" SortExpression="site_id" UniqueName="site_id">
                                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                            <HeaderStyle Width="10%" />
                                        </telerik:GridBoundColumn>
                                       <telerik:GridBoundColumn DataField="UserName" HeaderText="ชื่อ UserName เจ้าหน้าที่" SortExpression="UserName"
                                            UniqueName="UserName">
                                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                           <HeaderStyle Width="25%" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Num_printer" HeaderText="จำนวนเครื่อง" ReadOnly="True"
                                            SortExpression="Num_printer" UniqueName="Num_printer">
                                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                            <HeaderStyle Width="15%" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="description_nameprinter" HeaderText="ชื่อเครื่อง" ReadOnly="True"
                                            SortExpression="description_nameprinter" UniqueName="description_nameprinter">
                                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Left" />
                                            <HeaderStyle Width="20%" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="remark" HeaderText="หมายเหตุ" ReadOnly="True"
                                            SortExpression="remark" UniqueName="remark">
                                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Left" />
                                            <HeaderStyle Width="30%" />
                                        </telerik:GridBoundColumn>
                                        </Columns>
                                </MasterTableView>
                                <HeaderStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                <ClientSettings AllowKeyboardNavigation="True">
                                    <Selecting AllowRowSelect="True" />
                                    <KeyboardNavigationSettings AllowActiveRowCycle="True" />
                                    <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                                </ClientSettings>
                                <ItemStyle Wrap="False" />
                                <SelectedItemStyle BackColor="DarkOrange" Wrap="True" />
                            </telerik:RadGrid></td>
                    </tr>
                    <tr>
                        <td class="FormLabel" style="width: 40%">
                            รายการของเครื่องใบเสร็จ</td>
                        <td style="width: 100px">
                        </td>
                        <td style="width: 100px">
                        </td>
                    </tr>
                    <tr>
                        <td class="FormLabel" colspan="3">
                            <telerik:RadGrid ID="rgRequestForm2" runat="server" AllowMultiRowSelection="True"
                                AllowSorting="True" GridLines="None" ShowFooter="True" ShowStatusBar="True" Skin="Office2007"
                                TabIndex="-1" Width="100%">
                                <ItemStyle Wrap="False" />
                                <MasterTableView AutoGenerateColumns="False" ClientDataKeyNames="AutoID" DataKeyNames="AutoID"
                                    NoMasterRecordsText="ไม่มีรายการ">
                                    <Columns>
                                        <telerik:GridNumericColumn Aggregate="Count" DataField="AutoID" FooterText="รวม : "
                                            HeaderText="AutoID" ReadOnly="True" SortExpression="AutoID" UniqueName="AutoID" Visible="False">
                                            <FooterStyle Font-Names="Tahoma" Font-Size="10pt" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                        </telerik:GridNumericColumn>
                                        <telerik:GridBoundColumn DataField="site_id" HeaderText="สาขา"
                                            ReadOnly="True" SortExpression="site_id" UniqueName="site_id" HeaderStyle-Width="10%">
                                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="UserName" HeaderText="ชื่อ UserName เจ้าหน้าที่" SortExpression="UserName"
                                            UniqueName="UserName" HeaderStyle-Width="25%">
                                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="description_nameprinter" HeaderText="ชื่อเครื่อง" ReadOnly="True"
                                            SortExpression="description_nameprinter" UniqueName="description_nameprinter" HeaderStyle-Width="15%">
                                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Left" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="IP_UserPrint" HeaderText="เบอร์ IP เครื่อง" ReadOnly="True"
                                            SortExpression="IP_UserPrint" UniqueName="IP_UserPrint" HeaderStyle-Width="15%">
                                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="remark" HeaderText="หมายเหตุ" ReadOnly="True"
                                            SortExpression="remark" UniqueName="remark" HeaderStyle-Width="35%">
                                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Left" />
                                        </telerik:GridBoundColumn>
                                    </Columns>
                                </MasterTableView>
                                <SelectedItemStyle BackColor="DarkOrange" Wrap="True" />
                                <HeaderStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                <ClientSettings AllowKeyboardNavigation="True">
                                    <Selecting AllowRowSelect="True" />
                                    <KeyboardNavigationSettings AllowActiveRowCycle="True" />
                                    <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                                </ClientSettings>
                            </telerik:RadGrid></td>
                    </tr>
                    <tr>
                        <td align="center" class="FormLabel" colspan="3">
                            <asp:Button ID="btnDel" runat="server" Text="ลบข้อมูลเจ้าหน้าที่" OnClientClick="return ShowInsertForm3();" Visible="False" /></td>
                    </tr>
                    <tr>
                        <td class="FormLabel" colspan="3">
                            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                                <tr>
                                    <td align="center" style="width: 100%">
                                        <asp:Label ID="lblErrorDel" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="FormLabel" style="width: 40%">
                        </td>
                        <td style="width: 100px">
                            </td>
                        <td style="width: 100px">
                        </td>
                    </tr>
                    <tr>
                        <td class="FormLabel" style="width: 40%">
                        </td>
                        <td style="width: 100px">
                        </td>
                        <td style="width: 100px">
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </td>
    </tr>
    <tr>
        <td style="width: 100%">
            </td>
    </tr>
    <tr>
        <td style="width: 100%">
        </td>
    </tr>
</table>
<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
</telerik:RadAjaxManager>

