<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ListForm.ascx.vb" Inherits=".ListForm" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="RadAjaxManager1">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="rgRequestForm" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="rgRequestForm">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="rgRequestForm" />
            </UpdatedControls>
        </telerik:AjaxSetting>
    </AjaxSettings>
</telerik:RadAjaxManager>
<telerik:RadCodeBlock ID="RadCodeBlockFirst" runat="server">

    <script type="text/javascript">
       function popup(url)
        {
         params  = 'width='+screen.width;
         params += ', height='+screen.height;
         params += ', top=0, left=0'
         params += ', fullscreen=yes, scrollbars=0, resizable=1';

         var newwin=window.open(url,'windowname4', params);
         if (window.focus) {newwin.focus()}
         return false;
        }
        
       function refreshGrid(arg)
        {
	        try{
            		var obj = $get("<%= btnSearch.ClientID %>");
            		obj.click();
	        }catch(e){ }
	    }

	    function ShowError(refid) {
	        return window.showModalDialog("/DesktopModules/DFT_DS2_AdminDashboard/Popup/ViewError.aspx?InvHRunAuto=" + refid, window.self, "dialogHeight:370px;dialogWidth:720px;help:no;status:no;center:yes");
	    }
        
    </script>

</telerik:RadCodeBlock>
<div style="font-size: 9pt; padding-top: 10px; text-align: right">
    <asp:Image ImageAlign="Middle" ID="Image2" runat="server" ImageUrl="../Images/Gear-icon.png" />
    <asp:LinkButton ID="LinkButton1" runat="server">กลับไปหน้าหลัก Admin DashBoard</asp:LinkButton></div>
<h2>
    แสดงรายการแบบคำขอ
</h2>
<div style="font-size: 10pt; margin-top: 10px; margin-bottom: 10px; border: 1px solid #cccccc;
    padding: 5px;">
    <table style="margin-bottom: 10px;">
        <tr>
            <td>
                <font class="FormLabel">ฟอร์ม :</font>
            </td>
            <td>
                <asp:DropDownList ID="ddlFormType" runat="server">
                </asp:DropDownList>
            </td>
            <td align="right">
                &nbsp;&nbsp;
            </td>
            <td align="right">
                <font class="FormLabel">สถานที่:</font>
            </td>
            <td>
                <asp:DropDownList ID="ddlSite" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <font class="FormLabel">ตั้งแต่:</font>
            </td>
            <td>
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <telerik:RadDatePicker ID="rdpFromDate" runat="server" Culture="English (United Kingdom)"
                                Skin="Vista">
                                <Calendar DayNameFormat="FirstTwoLetters" FirstDayOfWeek="Sunday" ShowRowHeaders="False"
                                    Skin="Vista" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                    ViewSelectorText="x">
                                </Calendar>
                                <DatePopupButton HoverImageUrl="" ImageUrl="" />
                            </telerik:RadDatePicker>
                        </td>
                        <td>
                            <font class="FormLabel">&nbsp;ถึง:</font>
                        </td>
                        <td>
                            <telerik:RadDatePicker ID="rdpToDate" runat="server" Culture="English (United Kingdom)"
                                Skin="Vista">
                                <Calendar DayNameFormat="FirstTwoLetters" FirstDayOfWeek="Sunday" ShowRowHeaders="False"
                                    Skin="Vista" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                    ViewSelectorText="x">
                                </Calendar>
                                <DatePopupButton HoverImageUrl="" ImageUrl="" />
                            </telerik:RadDatePicker>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                <font class="FormLabel">ประเภท:</font>
            </td>
            <td>
                <asp:DropDownList ID="ddlSentBy" runat="server">
                    <asp:ListItem Value="ALL">---ทั้งหมด---</asp:ListItem>
                    <asp:ListItem Value="1">ส่งจากเว็บ</asp:ListItem>
                    <asp:ListItem Value="2">ส่งจากไฟล์ XML</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <font class="FormLabel">Invoice No.:</font>
            </td>
            <td>
                <asp:TextBox ID="txtInvoiceNo" runat="server" Width="128px"></asp:TextBox>
                &nbsp;&nbsp;<font class="FormLabel">เลขที่อ้างอิง:</font>
                <asp:TextBox ID="txtRefNo" runat="server"></asp:TextBox>
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                <font class="FormLabel">สถานะ:</font>
            </td>
            <td>
                <asp:DropDownList ID="ddlStatus" runat="server">
                    <asp:ListItem Value="ALL">---ทั้งหมด---</asp:ListItem>
                    <asp:ListItem Value="S">ส่งแล้วรอการตรวจลายมือชื่ออิเล็กทรอนิกส์ (S)</asp:ListItem>
                    <asp:ListItem Value="W">ส่งแล้วรอการตรวจสอบจากคอมพิวเตอร์ (W)</asp:ListItem>
                    <asp:ListItem Selected="True" Value="Q">รอการตรวจสอบจากเจ้าหน้าที่ (Q)</asp:ListItem>
                    <asp:ListItem Value="D">ผ่านการตรวจสอบคำขอและเอกสารแนบโดยเจ้าหน้าที่ (D)</asp:ListItem>
                    <asp:ListItem Value="R">ไม่ผ่านการตรวจสอบจากคอมพิวเตอร์ (R)</asp:ListItem>
                    <asp:ListItem Value="N">ไม่ผ่านการตรวจสอบจากเจ้าหน้าที่ (N)</asp:ListItem>
                    <asp:ListItem Value="C">ยกเลิก (C)</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                <asp:Button ID="btnSearch" runat="server" Text="ค้นหา" Width="150px" />
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
</div>
<telerik:RadGrid ID="grdMasterData" Font-Size="8pt" runat="server" AllowPaging="True" GridLines="None"
    PageSize="20" Skin="Office2007" TabIndex="-1" Width="100%" ShowStatusBar="True"
    AllowSorting="True" ShowGroupPanel="true" GroupingSettings-RetainGroupFootersVisibility="true">
    <GroupPanel Text="กรณีต้องการจัดกลุ่มข้อมูล ให้ลากชื่อคอลัมน์ที่ต้องการจัดกลุ่มมาใส่ในแถบตรงนี้"></GroupPanel>
    <MasterTableView  AutoGenerateColumns="False" ClientDataKeyNames="invh_run_auto"
        DataKeyNames="invh_run_auto,form_type,SentBy,company_taxno,edi_status_id,description" NoMasterRecordsText="ไม่มีรายการคำร้อง"
        Width="100%" PagerStyle-AlwaysVisible="true" PagerStyle-FirstPageToolTip="หน้าแรก"
        PagerStyle-LastPageToolTip="หน้าสุดท้าย" PagerStyle-NextPageToolTip="หน้าต่อไป"
        PagerStyle-PageSizeLabelText="แสดงหน้าละ" PagerStyle-PrevPageToolTip="ก่อนหน้านี้"
        PagerStyle-PagerTextFormat="{4}  หน้า {0} จาก {1} (รายการที่ {2} ถึง {3} จากทั้งหมด {5})">
        <Columns>
            <telerik:GridTemplateColumn UniqueName="ViewTemplateColumn">
                <HeaderStyle Width="15px" HorizontalAlign="Center" />
                <ItemStyle Width="15px" HorizontalAlign="Center" />
                <ItemTemplate>
                    <asp:HyperLink ID="ViewLink" ImageUrl="~/images/view.gif" Target="_blank" runat="server"></asp:HyperLink>
                </ItemTemplate>
            </telerik:GridTemplateColumn>
            <telerik:GridBoundColumn DataField="FORM_NAME" HeaderText="ชื่อฟอร์ม" ReadOnly="True"
                SortExpression="FORM_NAME" UniqueName="FORM_NAME">
             </telerik:GridBoundColumn>
            <telerik:GridBoundColumn DataField="invh_run_auto" HeaderText="เลขที่อ้างอิง" ReadOnly="True"
                SortExpression="invh_run_auto" UniqueName="invh_run_auto">
                <ItemStyle HorizontalAlign="Center" />
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn DataField="invoice_no1" HeaderText="Invoice No." ReadOnly="True"
                SortExpression="invoice_no1" UniqueName="invoice_no1">
                <ItemStyle HorizontalAlign="Center" />
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn DataField="company_name" HeaderText="บริษัท" SortExpression="company_name"
                UniqueName="company_name">
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn DataField="Authorize2" HeaderText="ผู้รับมอบ" ReadOnly="True"
                SortExpression="Authorize2" UniqueName="Authorize2">
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn DataField="sent_date" HeaderText="วันที่ส่ง" DataFormatString="{0:dd/MM/yyyy HH:mm}"
                ReadOnly="True" SortExpression="sent_date" UniqueName="sent_date">
                <ItemStyle HorizontalAlign="Center" />
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn DataField="checkdoc_date" HeaderText="วันที่ตรวจ" DataFormatString="{0:dd/MM/yyyy HH:mm}"
                ReadOnly="True" SortExpression="checkdoc_date" UniqueName="checkdoc_date">
                <ItemStyle HorizontalAlign="Center" />
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn DataField="CheckDoc_By" HeaderText="ผู้ตรวจสอบ" ReadOnly="True"
                SortExpression="CheckDoc_By" UniqueName="CheckDoc_By">
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
            </telerik:GridBoundColumn>    
            <telerik:GridTemplateColumn HeaderText="สถานะ" ReadOnly="True" UniqueName="DESCRIPTION">
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle Font-Names="Tahoma" Font-Size="8pt" HorizontalAlign="Center" />
                <ItemTemplate>
                    <asp:HyperLink ID="ViewStatus" runat="server"></asp:HyperLink>
                </ItemTemplate>
            </telerik:GridTemplateColumn>  
            <telerik:GridBoundColumn DataField="site_name" HeaderText="สาขาที่รับ" ReadOnly="True"
                SortExpression="site_name" UniqueName="site_name">
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
            </telerik:GridBoundColumn>    
            <telerik:GridBoundColumn DataField="SentType" HeaderText="&nbsp;" ReadOnly="True"
                SortExpression="SentType" UniqueName="SentType">
                <ItemStyle Font-Names="Tahoma" ForeColor="Gray" Font-Size="8pt" />
            </telerik:GridBoundColumn>
        </Columns>
    </MasterTableView>
    <HeaderStyle HorizontalAlign="Center" Font-Names="Tahoma" VerticalAlign="Middle"
        Height="30px" />
    <clientsettings reordercolumnsonclient="True" allowdragtogroup="True" allowcolumnsreorder="True">
                <Selecting AllowRowSelect="True"></Selecting>
            </clientsettings>

</telerik:RadGrid>
<div style="font-size:9pt;color:#666666;margin-top:10px;text-align:right;">
<table><tr><td style="background:Red;width:10px;">&nbsp;</td><td>ไม่ผ่านการตรวจสอบจากเจ้าหน้าที่&nbsp;&nbsp;</td><td style="background:#FF9900;width:10px;">&nbsp;</td><td>ไม่ผ่านการตรวจสอบจากคอมพิวเตอร์&nbsp;&nbsp;</td>
    <td style="background:Yellow;width:10px;">&nbsp;</td><td>&nbsp;ยกเลิก</td></tr></table></div>
<p>&nbsp;</p>