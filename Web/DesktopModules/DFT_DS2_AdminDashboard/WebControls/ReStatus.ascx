<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ReStatus.ascx.vb" Inherits=".ReStatus" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
    <ajaxsettings>
        <telerik:AjaxSetting AjaxControlID="RadAjaxManager1">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="grdMasterData" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="grdMasterData">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="grdMasterData" />
            </UpdatedControls>
        </telerik:AjaxSetting>
    </ajaxsettings>
</telerik:RadAjaxManager>
<telerik:RadCodeBlock ID="RadCodeBlockFirst" runat="server">
    <script type="text/javascript">
        function refreshGrid(arg) {
            try {
                //var obj = $get("<%= btnSearch.ClientID %>");
                //obj.click();
                if (!arg) {
                    $find("<%= RadAjaxManager1.ClientID %>").ajaxRequest("Rebind");
                }
                else {
                    $find("<%= RadAjaxManager1.ClientID %>").ajaxRequest("RebindAndNavigate");
                }
            } catch (e) { }
        }
    </script>

</telerik:RadCodeBlock>
<div style="font-size: 9pt; padding-top: 10px; text-align: right">
    <asp:Image ImageAlign="Middle" ID="Image2" runat="server" ImageUrl="../Images/Gear-icon.png" />
    <asp:LinkButton ID="LinkButton1" runat="server">กลับไปหน้าหลัก Admin DashBoard</asp:LinkButton></div>
<h2>แก้ไขสถานะแบบคำขอ</h2>
<div style="font-size:9pt;color:#666666;">ทำการค้นหาเลขที่อ้างอิงที่ต้องการ  เมื่อพบข้อมูลแล้วสามารถเลือกเปลี่ยนสถานะตามที่ต้องการได้</div>
<div style="font-size: 10pt; margin-top: 20px; margin-bottom: 10px; border: 1px solid #cccccc;
    padding: 5px;">
    <table>
        <tr>
            <td>
                <b>ค้นหาตาม</b>
                เลขที่อ้างอิง:</td>
            <td>
                <asp:TextBox ID="txtRefNo" runat="server"></asp:TextBox>
                &nbsp; หรือ เลขที่ใบกำกับสินค้า:</td>
            <td>
                <asp:TextBox ID="txtRefNo0" runat="server"></asp:TextBox>
            </td>
            <td width="50" align="right">
                <asp:Button ID="btnSearch" runat="server" Text="ค้นหา" />
            </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
           </tr>
        <tr>
            <td>
                สถานะ:</td>
            <td colspan="3">
                <asp:DropDownList ID="ddlStatus" runat="server">
                <asp:ListItem Text="ส่งแล้วรอการตรวจลายมือชื่ออิเล็กทรอนิกส์ - S" Value="S"></asp:ListItem>
                <asp:ListItem Text="ส่งแล้วรอการตรวจสอบจากคอมพิวเตอร์ - W" Value="W"></asp:ListItem>
                <asp:ListItem Text="รอการตรวจสอบจากเจ้าหน้าที่ - Q" Value="Q"></asp:ListItem>
                <asp:ListItem Text="ผ่านการตรวจสอบคำขอและเอกสารแนบโดยเจ้าหน้าที่ - D" Value="D"></asp:ListItem>
                <asp:ListItem Text="ผ่านการตรวจสอบจากเจ้าหน้าที่ - A" Value="A"></asp:ListItem>
                <asp:ListItem Text="ไม่ผ่านการตรวจสอบจากคอมพิวเตอร์ - R" Value="R"></asp:ListItem>
                <asp:ListItem Text="ไม่ผ่านการตรวจสอบจากเจ้าหน้าที่ - N" Value="N"></asp:ListItem>
                </asp:DropDownList>
            &nbsp;<asp:Button ID="btnChangeStatus" runat="server" Enabled="False" 
                    Text="เปลี่ยนสถานะ" />
            </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
           </tr>
    </table>
    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
</div>
<telerik:RadGrid ID="grdMasterData" ShowGroupPanel="true" GroupingSettings-RetainGroupFootersVisibility="true"
    AllowMultiRowSelection="True" runat="server" AllowPaging="True" AllowSorting="True"
    GridLines="None" Skin="Office2007" Font-Names="Tahoma" Font-Size="X-Small" TabIndex="-1"
    PageSize="20">
    <GroupPanel Text="กรณีต้องการจัดกลุ่มข้อมูล ให้ลากชื่อคอลัมน์ที่ต้องการจัดกลุ่มมาใส่ในแถบตรงนี้"></GroupPanel>
    <mastertableview autogeneratecolumns="False" cellspacing="-1" width="100%" datakeynames="invh_run_auto"
        clientdatakeynames="invh_run_auto" nomasterrecordstext="ไม่มีข้อมูลที่ค้นหา"
        pagerstyle-alwaysvisible="true" pagerstyle-firstpagetooltip="หน้าแรก" pagerstyle-lastpagetooltip="หน้าสุดท้าย"
        pagerstyle-nextpagetooltip="หน้าต่อไป" pagerstyle-prevpagetooltip="ก่อนหน้านี้"
        pagerstyle-pagertextformat="{4}  หน้า {0} จาก {1} (รายการที่ {2} ถึง {3} จากทั้งหมด {5})">
                                <Columns>
                                    <telerik:GridBoundColumn DataField="invh_run_auto" HeaderText="เลขที่อ้างอิง" Visible="true"
                                        ReadOnly="True" SortExpression="invh_run_auto" UniqueName="invh_run_auto">
                                        <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="invoice_no1" HeaderText="เลขที่ใบกำกับสินค้า" Visible="true"
                                        ReadOnly="True" SortExpression="invoice_no1" UniqueName="invoice_no1">
                                        <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="edi_status" HeaderText="สถานะ" Visible="True"
                                        SortExpression="edi_status" UniqueName="edi_status">
                                        <ItemStyle Font-Names="Tahoma"  Font-Size="10pt" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="cancel_by" HeaderText="ยกเลิกโดย" Visible="True"
                                        SortExpression="cancel_by" UniqueName="cancel_by">
                                        <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="form_name" HeaderText="ชื่อฟอร์ม" Visible="True"
                                        SortExpression="form_name" UniqueName="form_name">
                                        <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                    </telerik:GridBoundColumn>
                                </Columns>
<PagerStyle FirstPageToolTip="หน้าแรก" NextPageToolTip="หน้าต่อไป" LastPageToolTip="หน้าสุดท้าย" PrevPageToolTip="ก่อนหน้านี้" AlwaysVisible="True" PagerTextFormat="{4}  หน้า {0} จาก {1} (รายการที่ {2} ถึง {3} จากทั้งหมด {5})"></PagerStyle>
                            </mastertableview>
    <headerstyle horizontalalign="Center" font-names="Tahoma" font-size="10pt" />
    <clientsettings reordercolumnsonclient="True" allowdragtogroup="True" allowcolumnsreorder="True">
                <Selecting AllowRowSelect="True"></Selecting>
            </clientsettings>
   
</telerik:RadGrid>
<p>&nbsp;</p>