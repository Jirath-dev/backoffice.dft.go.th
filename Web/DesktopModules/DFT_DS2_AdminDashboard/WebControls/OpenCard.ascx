<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="OpenCard.ascx.vb" Inherits=".OpenCard" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
    <ajaxsettings>
        <telerik:AjaxSetting AjaxControlID="RadAjaxManager1">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="grdMasterData" />
                <telerik:AjaxUpdatedControl ControlID="btnSearch" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="btnSearch">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="btnSearch" />
                <telerik:AjaxUpdatedControl ControlID="grdMasterData" />
                <telerik:AjaxUpdatedControl ControlID="btnRegisCardId" />
                <telerik:AjaxUpdatedControl ControlID="txtCardId" />
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
<h2>เพิ่มรหัสบัตรผู้ส่งออก-นำเข้า (Card Id) 
    <br />
    สำหรับใช้ระบบ Digital Signature</h2>
<div style="font-size: 10pt; margin-top: 10px; margin-bottom: 10px; border: 1px solid #cccccc;
    padding: 5px;">
    <table>
        <tr>
            <td>
                รหัสบัตรผู้ส่งออก-นำเข้า:
            </td>
            <td>
                <asp:TextBox ID="txtCardId" runat="server" Columns="15"></asp:TextBox>
            </td>
            <td>
                <asp:Button ID="btnSearch" runat="server" Text="ค้นหา" />
               
            </td>
            <td><asp:Button ID="btnRegisCardId" Enabled="false" runat="server" Text="บันทึกรหัสบัตร" /></td>
            <td> <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ControlToValidate="txtCardId" ErrorMessage="*ใส่รหัสบัตรผู้ส่งออก-นำเข้า" 
                    SetFocusOnError="True"></asp:RequiredFieldValidator></td>
        </tr>
    </table>
</div>
<telerik:RadGrid ID="grdMasterData"  GroupingSettings-RetainGroupFootersVisibility="true"
    AllowMultiRowSelection="True" runat="server" AllowPaging="True" AllowSorting="True"
    GridLines="None" Skin="Office2007" Font-Names="Tahoma" Font-Size="X-Small" TabIndex="-1"
    PageSize="20">
    <GroupPanel Text="กรณีต้องการจัดกลุ่มข้อมูล ให้ลากชื่อคอลัมน์ที่ต้องการจัดกลุ่มมาใส่ในแถบตรงนี้"></GroupPanel>
    <mastertableview  autogeneratecolumns="False" cellspacing="-1" width="100%" datakeynames="card_id"
        clientdatakeynames="card_id" nomasterrecordstext="ไม่มีข้อมูลที่ค้นหา"
        pagerstyle-alwaysvisible="true" pagerstyle-firstpagetooltip="หน้าแรก" pagerstyle-lastpagetooltip="หน้าสุดท้าย"
        pagerstyle-nextpagetooltip="หน้าต่อไป" pagerstyle-prevpagetooltip="ก่อนหน้านี้"
        pagerstyle-pagertextformat="{4}  หน้า {0} จาก {1} (รายการที่ {2} ถึง {3} จากทั้งหมด {5})">
                                <Columns>
                                    <telerik:GridBoundColumn DataField="card_id" HeaderText="รหัสบัตรผู้ส่งออก-นำเข้า" Visible="true"
                                        ReadOnly="True" SortExpression="card_id" UniqueName="card_id">
                                        <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="company_taxno" HeaderText="เลขที่ภาษี" Visible="true"
                                        ReadOnly="True" SortExpression="company_taxno" UniqueName="company_taxno">
                                        <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="AuthName_Thai" HeaderText="ชื่อผูัรับมอบ" 
                                        SortExpression="AuthName_Thai" UniqueName="AuthName_Thai">
                                        <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="active_flag" HeaderText="สถานะ" Visible="True"
                                        SortExpression="active_flag" UniqueName="active_flag">
                                        <ItemStyle Font-Names="Tahoma"  Font-Size="10pt" HorizontalAlign="Center" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="expire_date" HeaderText="วันที่หมดอายุ" Visible="True"
                                        SortExpression="expire_date" UniqueName="expire_date">
                                        <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="commit_name" HeaderText="ชื่อบริษัท" Visible="True"
                                        SortExpression="commit_name" UniqueName="commit_name">
                                        <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="company_internet_ds_edi" HeaderText="เว็บ" Visible="True"
                                        SortExpression="company_internet_ds_edi" UniqueName="company_internet_ds_edi">
                                        <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center"  />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="company_internet_xml_edi" HeaderText="XML" Visible="True"
                                        SortExpression="company_internet_xml_edi" UniqueName="company_internet_xml_edi">
                                        <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center"  />
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