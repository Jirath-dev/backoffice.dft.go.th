<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ListPDF.ascx.vb" Inherits=".ListPDF" %>
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
    แสดงประวัติการส่งไฟล์เอกสารแนบ PDF</h2>
<div style="font-size: 10pt; margin-top: 10px; margin-bottom: 10px; border: 1px solid #cccccc;
    padding: 5px;">
    <table>
        <tr>
            <td>
                วันที่อัพโหลดไฟล์ pdf: จากวันที่
            </td>
            <td>
                <telerik:RadDatePicker ID="txtFromDate" runat="server" Culture="English (United Kingdom)"
                    Skin="Vista">
                    <calendar daynameformat="FirstTwoLetters" firstdayofweek="Sunday" showrowheaders="False"
                        skin="Vista" usecolumnheadersasselectors="False" userowheadersasselectors="False"
                        viewselectortext="x">
                                                                    </calendar>
                    <datepopupbutton hoverimageurl="" imageurl="" />
                </telerik:RadDatePicker>
            </td>
            <td>
                ถึงวันที่
            </td>
            <td>
                <telerik:RadDatePicker ID="txtToDate" runat="server" Culture="English (United Kingdom)"
                    Skin="Vista">
                    <calendar daynameformat="FirstTwoLetters" firstdayofweek="Sunday" showrowheaders="False"
                        skin="Vista" usecolumnheadersasselectors="False" userowheadersasselectors="False"
                        viewselectortext="x">
                                                                    </calendar>
                    <datepopupbutton hoverimageurl="" imageurl="" />
                </telerik:RadDatePicker>
            </td>
            <td>
                <asp:Button ID="btnSearch" runat="server" Text="ค้นหา" />
            </td>
            </ta></tr>
    </table>
    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
</div>
<telerik:RadGrid ID="grdMasterData" ShowGroupPanel="true" GroupingSettings-RetainGroupFootersVisibility="true"
    AllowMultiRowSelection="True" runat="server" AllowPaging="True" AllowSorting="True"
    GridLines="None" Skin="Office2007" Font-Names="Tahoma" Font-Size="X-Small" TabIndex="-1"
    PageSize="20">
    <GroupPanel Text="กรณีต้องการจัดกลุ่มข้อมูล ให้ลากชื่อคอลัมน์ที่ต้องการจัดกลุ่มมาใส่ในแถบตรงนี้"></GroupPanel>
    <mastertableview autogeneratecolumns="False" cellspacing="-1" width="100%" datakeynames="fileid,invh_run_auto,FilesName,CreateDate"
        clientdatakeynames="fileid,invh_run_auto" nomasterrecordstext="ไม่มีไฟล์ข้อมูล xml"
        pagerstyle-alwaysvisible="true" pagerstyle-firstpagetooltip="หน้าแรก" pagerstyle-lastpagetooltip="หน้าสุดท้าย"
        pagerstyle-nextpagetooltip="หน้าต่อไป" pagerstyle-prevpagetooltip="ก่อนหน้านี้"
        pagerstyle-pagertextformat="{4}  หน้า {0} จาก {1} (รายการที่ {2} ถึง {3} จากทั้งหมด {5})">
                                <Columns>

                                     <telerik:GridTemplateColumn UniqueName="RequestViewColumn" HeaderText="แสดงแบบคำขอ">
                                        <ItemStyle HorizontalAlign="Center" Font-Names="Tahoma" Font-Size="10pt" />
                                        <ItemTemplate>
                                            <asp:HyperLink ID="lnkDl" Target="_blank"  NavigateUrl='#'  ImageUrl="~/images/view.gif" runat="server"></asp:HyperLink>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn UniqueName="RequestViewColumn" HeaderText="แสดงไฟล์ pdf">
                                        <ItemStyle HorizontalAlign="Center" Font-Names="Tahoma" Font-Size="10pt" />
                                        <ItemTemplate>
                                            <asp:HyperLink ID="lnkXml" Target="_blank"  NavigateUrl='#' ImageUrl="~/images/view.gif" runat="server"></asp:HyperLink>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn DataField="FilesName" HeaderText="ชื่อไฟล์ pdf" Visible="true"
                                        ReadOnly="True" SortExpression="FilesName" UniqueName="FilesName">
                                        <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Descript" HeaderText="คำอธิบาย" Visible="true"
                                        ReadOnly="True" SortExpression="Descript" UniqueName="Descript">
                                        <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="CreateDate" HeaderText="วันที่บันทึกข้อมูล" 
                                        SortExpression="CreateDate" UniqueName="CreateDate" DataFormatString="{0:dd/MM/yyyy}">
                                        <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="invh_run_auto" HeaderText="เลขที่อ้างอิง" Visible="True"
                                        SortExpression="invh_run_auto" UniqueName="invh_run_auto1">
                                        <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Document_type" HeaderText="ประเภทเอกสาร" Visible="True"
                                        SortExpression="Document_type" UniqueName="Document_type">
                                        <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="company_eng" HeaderText="บริษัท" Visible="True"
                                        SortExpression="company_eng" UniqueName="company_eng">
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
