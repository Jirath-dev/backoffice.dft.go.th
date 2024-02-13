<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="RemoveName.ascx.vb" Inherits=".RemoveName" %>
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
<div style="font-size: 9pt; padding-top: 10px; text-align: right">
    <asp:Image ImageAlign="Middle" ID="Image2" runat="server" ImageUrl="../Images/Gear-icon.png" />
    <asp:LinkButton ID="LinkButton1" runat="server">กลับไปหน้าหลัก Admin DashBoard</asp:LinkButton></div>
<h2>ลบชื่อเจ้าหน้าที่ออกจากการตรวจแบบคำขอ</h2>
<div style="font-size: 10pt; margin-top: 10px; margin-bottom: 10px; border: 1px solid #cccccc;
    padding: 5px;">
    <table>
        <tr>
            <td>
                วันที่:             </td>
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
                สาขา:</td>
            <td>
                <asp:DropDownList ID="ddlSite" runat="server">
                </asp:DropDownList>
            </td>
            <td style="width:60px;">
                <asp:Button ID="btnSearch" runat="server" Text="ค้นหา" />
            </td>
            <td style="width:130px;text-align:right;border-left:1px solid #eeeeee;">
                หรือกรณีรู้เลขที่อ้างอิง:</td>
            <td>
                <asp:TextBox ID="txtRefNo" runat="server" Columns="16"></asp:TextBox>
            </td>
            <td>
                <asp:Button ID="btnRemove" runat="server" Text="ลบชื่อออก" />
            </td>
            </tr>
    </table>
    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
</div>

<telerik:RadGrid ID="grdMasterData" ShowGroupPanel="true" GroupingSettings-RetainGroupFootersVisibility="true"
    AllowMultiRowSelection="True" runat="server" AllowPaging="True" AllowSorting="True"
    GridLines="None" Skin="Office2007" Font-Names="Tahoma" Font-Size="X-Small" TabIndex="-1"
    PageSize="20" AllowAutomaticDeletes="true">
    <GroupPanel Text="กรณีต้องการจัดกลุ่มข้อมูล ให้ลากชื่อคอลัมน์ที่ต้องการจัดกลุ่มมาใส่ในแถบตรงนี้"></GroupPanel>
    <mastertableview autogeneratecolumns="False" cellspacing="-1" width="100%" datakeynames="invh_run_auto"
        clientdatakeynames="invh_run_auto" nomasterrecordstext="ไม่มีข้อมูล"
        pagerstyle-alwaysvisible="true" pagerstyle-firstpagetooltip="หน้าแรก" pagerstyle-lastpagetooltip="หน้าสุดท้าย"
        pagerstyle-nextpagetooltip="หน้าต่อไป" pagerstyle-prevpagetooltip="ก่อนหน้านี้"
        pagerstyle-pagertextformat="{4}  หน้า {0} จาก {1} (รายการที่ {2} ถึง {3} จากทั้งหมด {5})" CommandItemDisplay="Top">
        
        <CommandItemTemplate>
                <div style="padding: 5px 5px;">
                    เลือกคำสั่งที่ต้องการทำงาน&nbsp;&nbsp;&nbsp;&nbsp;
 <asp:LinkButton ID="linkDelete" OnClientClick="javascript:return confirm('คุณแน่ใจว่าต้องการลบชื่อผู้ตรวจสอบออกจากแบบคำขอเลขที่อ้างอิงนี้ใช้หรือไม่?')"
 runat="server" CommandName="DeleteSelected"><asp:Image ID="Image1" runat="server" ImageUrl="../Images/Delete.gif" BorderStyle="None" ImageAlign="Middle" />ลบชื่อออกจากแบบคำขอที่เลือกไว้</asp:LinkButton>&nbsp;&nbsp;
                </div>
            </CommandItemTemplate>

        
                                <Columns>
                                    <telerik:GridBoundColumn DataField="CheckDoc_by" HeaderText="ชื่อเจ้าหน้าที่" Visible="true"
                                        ReadOnly="True" SortExpression="CheckDoc_by" UniqueName="CheckDoc_by">
                                        <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="invh_run_auto" HeaderText="เลขที่อ้างอิง" Visible="true"
                                        ReadOnly="True" SortExpression="invh_run_auto" UniqueName="invh_run_auto">
                                        <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                    </telerik:GridBoundColumn>
                                     <telerik:GridBoundColumn DataField="invoice_no1" HeaderText="เลขที่ใบกำกับสินค้า (Invoice No.)" 
                                        SortExpression="invoice_no1" UniqueName="invoice_no1">
                                        <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="form_name" HeaderText="ชื่อฟอร์ม" 
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
<asp:HiddenField ID="PageLoadState" Value="0" runat="server" />
