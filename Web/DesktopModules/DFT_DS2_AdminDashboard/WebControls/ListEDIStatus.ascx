<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ListEDIStatus.ascx.vb" Inherits=".ListEDIStatus" %>
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
                <telerik:AjaxUpdatedControl ControlID="grdMasterData" />
            </UpdatedControls>
        </telerik:AjaxSetting>
    </ajaxsettings>
</telerik:RadAjaxManager>
<telerik:RadCodeBlock ID="RadCodeBlockFirst" runat="server">

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

</telerik:RadCodeBlock>

<div style="font-size: 9pt; padding-top: 10px; text-align: right">
    <asp:Image ImageAlign="Middle" ID="Image2" runat="server" ImageUrl="../Images/Gear-icon.png" />
    <asp:LinkButton ID="LinkButton1" runat="server">กลับไปหน้าหลัก Admin DashBoard</asp:LinkButton></div>
<h2>
    แบบคำขอที่ไม่ผ่านการตรวจสอบ</h2>
    <div style="font-size:9pt;color:#666666;margin-top:8px;margin-bottom:10px;">แสดงรายละเอียดของแบบคำขอที่ไม่ผ่านการตรวจสอบ</div>
<div style="font-size: 10pt; margin-top: 10px; margin-bottom: 10px; border: 1px solid #cccccc;
    padding: 5px;">
    <table>
        <tr>
            <td>
                วันที่ส่ง: จากวันที่
            </td>
            <td>
                <telerik:raddatepicker ID="txtFromDate" runat="server" Culture="English (United Kingdom)"
                    Skin="Vista">
                    <calendar daynameformat="FirstTwoLetters" firstdayofweek="Sunday" showrowheaders="False"
                        skin="Vista" usecolumnheadersasselectors="False" userowheadersasselectors="False"
                        viewselectortext="x">
                                                                    </calendar>
                    <datepopupbutton hoverimageurl="" imageurl="" />
                </telerik:raddatepicker>
            </td>
            <td>
                ถึงวันที่
            </td>
            <td>
                <telerik:raddatepicker ID="txtToDate" runat="server" Culture="English (United Kingdom)"
                    Skin="Vista">
                    <calendar daynameformat="FirstTwoLetters" firstdayofweek="Sunday" showrowheaders="False"
                        skin="Vista" usecolumnheadersasselectors="False" userowheadersasselectors="False"
                        viewselectortext="x">
                                                                    </calendar>
                    <datepopupbutton hoverimageurl="" imageurl="" />
                </telerik:raddatepicker>
            </td>
            <td>
                <asp:Button ID="btnSearch" runat="server" Text="ค้นหา" />
            </td>
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
        nomasterrecordstext="ไม่พบข้อมูล"
        pagerstyle-alwaysvisible="true" pagerstyle-firstpagetooltip="หน้าแรก" pagerstyle-lastpagetooltip="หน้าสุดท้าย"
        pagerstyle-nextpagetooltip="หน้าต่อไป" pagerstyle-prevpagetooltip="ก่อนหน้านี้"
        pagerstyle-pagertextformat="{4}  หน้า {0} จาก {1} (รายการที่ {2} ถึง {3} จากทั้งหมด {5})">
                                <Columns>

                                     <telerik:GridTemplateColumn UniqueName="RequestViewColumn" HeaderText="&nbsp;">
                                        <ItemStyle HorizontalAlign="Center" Font-Names="Tahoma" Font-Size="10pt" />
                                        <ItemTemplate>
                                            <asp:HyperLink ID="lnkDl" Target="_blank"  NavigateUrl='#'  ImageUrl="~/images/view.gif" runat="server"></asp:HyperLink>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn DataField="invh_run_auto" HeaderText="เลขที่อ้างอิง" Visible="True"
                                        SortExpression="invh_run_auto" UniqueName="invh_run_auto1">
                                        <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="error_message" HeaderText="ประเภท" Visible="True"
                                        SortExpression="error_message" UniqueName="error_message">
                                        <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                    </telerik:GridBoundColumn>
                                     <telerik:GridBoundColumn DataField="error_information" HeaderText="รายละเอียดข้อผิดพลาด/การแก้ไข" Visible="True"
                                        SortExpression="error_information" UniqueName="error_information">
                                        <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="CheckBy" HeaderText="โดย" Visible="True"
                                        SortExpression="CheckBy" UniqueName="CheckBy">
                                        <ItemStyle Font-Names="Tahoma" HorizontalAlign="Center" Font-Size="10pt" />
                                    </telerik:GridBoundColumn>
                                   
                                    <telerik:GridBoundColumn DataField="sent_date" HeaderText="วันที่ส่ง" 
                                        SortExpression="sent_date" UniqueName="sent_date" DataFormatString="{0:dd/MM/yyyy HH:mm}">
                                        <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                    </telerik:GridBoundColumn>
                              
                                    <telerik:GridBoundColumn DataField="form_name" HeaderText="ชื่อฟอร์ม" Visible="True"
                                        SortExpression="form_name" UniqueName="form_name">
                                        <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="company_eng" HeaderText="บริษัท" Visible="True"
                                        SortExpression="company_eng" UniqueName="company_eng">
                                        <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                    </telerik:GridBoundColumn>
                                     <telerik:GridBoundColumn DataField="site_name" HeaderText="สถานที่รับฟอร์ม" Visible="True"
                                        SortExpression="site_name" UniqueName="site_name">
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
<p>&nbsp;</p>