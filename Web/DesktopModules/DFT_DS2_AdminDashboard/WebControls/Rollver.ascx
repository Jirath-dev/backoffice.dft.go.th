<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="Rollver.ascx.vb" Inherits=".Rollver" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<telerik:radajaxmanager id="RadAjaxManager1" runat="server">
    <ajaxsettings>
        <telerik:AjaxSetting AjaxControlID="RadAjaxManager1">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="grdMasterData" />
                <telerik:AjaxUpdatedControl ControlID="btnSearch" />
                <telerik:AjaxUpdatedControl ControlID="PageLoadState" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="PageLoadState">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="PageLoadState" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="btnSearch">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="grdMasterData" />
                <telerik:AjaxUpdatedControl ControlID="PageLoadState" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="grdMasterData">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="grdMasterData" />
            </UpdatedControls>
        </telerik:AjaxSetting>
    </ajaxsettings>
</telerik:radajaxmanager>
<telerik:radcodeblock id="RadCodeBlockFirst" runat="server">

    <script type="text/javascript">
        function popup(url) {
            params = 'width=' + screen.width;
            params += ', height=' + screen.height;
            params += ', top=0, left=0'
            params += ', fullscreen=yes, scrollbars=0, resizable=1';

            newwin = window.open(url, 'windowname4', params);
            if (window.focus) { newwin.focus() }
            return false;
        }

        function openXml(xmlfile) {
            var path = "/XMLFile/Completed/" + xmlfile;
            popup(path);
            return false;
        }

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

</telerik:radcodeblock>

<div style="font-size: 9pt; padding-top: 10px; text-align: right">
    <asp:Image ImageAlign="Middle" ID="Image2" runat="server" ImageUrl="../Images/Gear-icon.png" />
    <asp:LinkButton ID="LinkButton1" runat="server">กลับไปหน้าหลัก Admin DashBoard</asp:LinkButton></div>
<h2>ผลการตรวจสอบฯ ต้นทุน (Rollver)</h2>
<div style="font-size: 10pt; margin-top: 10px; margin-bottom: 10px; border: 1px solid #cccccc;
    padding: 5px;">
    <table>
        <tr>
            <td>
                <b>ค้นหา</b>
                เลขที่ต้นทุน:
            </td>
            <td>
                <asp:TextBox ID="txtCertNo" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:Button ID="btnSearch" runat="server" Text="ค้นหา" />
           </td>
           <td><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ControlToValidate="txtCertNo" ErrorMessage="*" 
                    SetFocusOnError="True"></asp:RequiredFieldValidator>
 </td>
            <td>
                <asp:RadioButtonList ID="optServerType" runat="server" 
                    RepeatDirection="Horizontal" RepeatLayout="Flow">
                    <asp:ListItem Value="1" Selected="True">สบน.</asp:ListItem>
                    <asp:ListItem Value="2">ไฟล์ pdf ต้นทุน</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
    </table>
</div>
<telerik:RadGrid ID="grdMasterData"
    AllowMultiRowSelection="True" runat="server" AllowPaging="True" AllowSorting="True"
    GridLines="None" Skin="Office2007" Font-Names="Tahoma" Font-Size="X-Small" TabIndex="-1"
    PageSize="20">
    <GroupPanel Text="กรณีต้องการจัดกลุ่มข้อมูล ให้ลากชื่อคอลัมน์ที่ต้องการจัดกลุ่มมาใส่ในแถบตรงนี้"></GroupPanel>
    <mastertableview autogeneratecolumns="False" cellspacing="-1" width="100%" datakeynames="certoforigin_no"
        clientdatakeynames="certoforigin_no" nomasterrecordstext="ไม่มีข้อมูล"
        pagerstyle-alwaysvisible="true" pagerstyle-firstpagetooltip="หน้าแรก" pagerstyle-lastpagetooltip="หน้าสุดท้าย"
        pagerstyle-nextpagetooltip="หน้าต่อไป" pagerstyle-prevpagetooltip="ก่อนหน้านี้"
        pagerstyle-pagertextformat="{4}  หน้า {0} จาก {1} (รายการที่ {2} ถึง {3} จากทั้งหมด {5})">
                                <Columns>
                                    <telerik:GridTemplateColumn UniqueName="RequestViewColumn" HeaderText="แสดงไฟล์ pdf" Visible="false">
                                        <ItemStyle HorizontalAlign="Center" Font-Names="Tahoma" Font-Size="10pt" />
                                        <ItemTemplate>
                                            <asp:HyperLink ID="lnkXml" Target="_blank"  NavigateUrl='#' ImageUrl="~/images/view.gif" runat="server"></asp:HyperLink>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn DataField="certoforigin_no" HeaderText="เลขที่ผลการตรวจฯ" Visible="true"
                                        ReadOnly="True" SortExpression="certoforigin_no" UniqueName="certoforigin_no">
                                        <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                    </telerik:GridBoundColumn>
                                    
                                    <telerik:GridBoundColumn DataField="harmonized_no" HeaderText="พิกัดศุลกากร" Visible="True"
                                        SortExpression="harmonized_no" UniqueName="harmonized_no">
                                        <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="company_name_th" HeaderText="ชื่อบริษัท" Visible="True"
                                        SortExpression="company_name_th" UniqueName="company_name_th">
                                        <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="company_name_en" HeaderText="Company Name" Visible="True"
                                        SortExpression="company_name_en" UniqueName="company_name_en">
                                        <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="goods_desc_th" HeaderText="คำอธิบายรายละเอียด" Visible="True"
                                        SortExpression="goods_desc_th" UniqueName="goods_desc_th">
                                        <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="goods_desc_en" HeaderText="Product Description" Visible="True"
                                        SortExpression="goods_desc_en" UniqueName="goods_desc_en">
                                        <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="model" HeaderText="รุ่น(Model)" Visible="True"
                                        SortExpression="model" UniqueName="model">
                                        <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="country_name" HeaderText="ประเทศ" Visible="True"
                                        SortExpression="country_name" UniqueName="country_name">
                                        <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                    </telerik:GridBoundColumn>
                                             <telerik:GridBoundColumn DataField="tax_id" HeaderText="เลขที่ผู้เสียภาษี" Visible="true"
                                        ReadOnly="True" SortExpression="tax_id" UniqueName="tax_id">
                                        <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="certoforigin_date" HeaderText="วันที่อนุมัติ" 
                                        SortExpression="certoforigin_date" UniqueName="certoforigin_date" DataFormatString="{0:dd/MM/yyyy}">
                                        <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Cert_ExpiredDate" HeaderText="วันที่หมดอายุ" 
                                        SortExpression="Cert_ExpiredDate" UniqueName="Cert_ExpiredDate" DataFormatString="{0:dd/MM/yyyy}">
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
<div style="margin-top:15px;"></div>
<asp:HiddenField ID="PageLoadState" Value="0" runat="server" />
