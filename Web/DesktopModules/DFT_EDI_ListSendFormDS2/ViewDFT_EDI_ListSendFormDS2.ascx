<%@ Control language="vb" Inherits="NTI.Modules.DFT_EDI_ListSendFormDS2.ViewDFT_EDI_ListSendFormDS2" AutoEventWireup="false" Explicit="True" Codebehind="ViewDFT_EDI_ListSendFormDS2.ascx.vb" %>
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

         newwin=window.open(url,'windowname4', params);
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
	        return window.showModalDialog("http://edi.dft.go.th/DesktopModules/DFT_eCO_EDI/Popup/ViewErrors.aspx?InvHRunAuto=" + refid, window.self, "dialogHeight:370px;dialogWidth:720px;help:no;status:no;center:yes");
	    }
        
    </script>

</telerik:RadCodeBlock>
<table style="width: 100%" cellpadding="3" cellspacing="3">
    <tr>
        <td>&nbsp;
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
                                                            <font class="groupbox">&nbsp; แสดงรายการแบบคำขอที่ส่งมาทั้งหมด ด้วย Digital Signature)&nbsp;</font></td>
                                                        <telerik:RadCodeBlock ID="RadCodeBlock6" runat="server">
                                                            <td style='background-image: url(<%= ResolveUrl("images") %>/CORNER.gif); background-repeat: no-repeat;'>
                                                                &nbsp; &nbsp;&nbsp;</td>
                                                        </telerik:RadCodeBlock>
                                                        <td align="right">&nbsp;</td>
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
                                <div style="margin:10px;">
                                                                        
                                    <table style="margin-bottom:10px;"><tr><td><font class="FormLabel">ฟอร์ม :</font></td><td>
                                        <telerik:RadComboBox ID="dropFormType" runat="server" DataTextField="form_name" DataValueField="form_type"
                                                                Filter="StartsWith" MarkFirstMatch="True" Skin="Web20" 
                                            Width="400px" Font-Names="Tahoma" Font-Size="8pt" >
                                                            </telerik:RadComboBox></td><td align="right">&nbsp;&nbsp;</td>
                                                            <td align="right"><font class="FormLabel">สถานที่:</font></td><td>
                                        <telerik:RadComboBox ID="ddlSite" runat="server" 
                                            DataTextField="form_name" DataValueField="form_type"
                                                                Filter="StartsWith" MarkFirstMatch="True" Skin="Web20" 
                                             Font-Names="Tahoma" Font-Size="8pt">
                                                            </telerik:RadComboBox></td><td>
                                            &nbsp;</td><td>
                                            &nbsp;</td></tr>
                                    <tr><td><font class="FormLabel">ตั้งแต่:</font></td><td>
                                    <table cellpadding="0" cellspacing="0"><tr><td>
                                    <telerik:RadDatePicker ID="rdpFromDate" runat="server" Culture="English (United Kingdom)"
                                                    Skin="Vista">
                                                    <Calendar DayNameFormat="FirstTwoLetters" FirstDayOfWeek="Sunday" ShowRowHeaders="False"
                                                        Skin="Vista" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                        ViewSelectorText="x">
                                                    </Calendar>
                                                    <DatePopupButton HoverImageUrl="" ImageUrl="" />
                                                </telerik:RadDatePicker>
                                                </td><td>
                                                 <font class="FormLabel">&nbsp;ถึง:</font>
                                                 </td><td>
                                                <telerik:RadDatePicker ID="rdpToDate" runat="server" Culture="English (United Kingdom)"
                                                    Skin="Vista">
                                                    <Calendar DayNameFormat="FirstTwoLetters" FirstDayOfWeek="Sunday" ShowRowHeaders="False"
                                                        Skin="Vista" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                        ViewSelectorText="x">
                                                    </Calendar>
                                                    <DatePopupButton HoverImageUrl="" ImageUrl="" />
                                                </telerik:RadDatePicker>
                                                </td><td>
                                                &nbsp;</td></tr></table>
                                                </td><td>
                                                &nbsp;</td><td>
                                                <font class="FormLabel">ประเภท:</font></td><td>
                                        <telerik:RadComboBox ID="ddlSentBy" runat="server" 
                                                 Filter="StartsWith" MarkFirstMatch="True" Skin="Web20" 
                                            Font-Names="Tahoma" Font-Size="8pt">
                                            <Items>
                                                <telerik:RadComboBoxItem runat="server" Text="---ทั้งหมด---" Value="ALL" />
                                                <telerik:RadComboBoxItem runat="server" 
                                                    Text="ส่งจากเว็บ" Value="1" />
                                                <telerik:RadComboBoxItem runat="server" Text="ส่งจากไฟล์ XML" Value="2" />
                                            </Items>
                                                            </telerik:RadComboBox></td><td>
                                                &nbsp;</td><td>
                                            &nbsp;</td></tr>
                                    <tr><td><font class="FormLabel">Invoice No.:</font></td><td>
                                                <asp:TextBox ID="txtInvoiceNo" runat="server" Width="128px"></asp:TextBox> &nbsp;&nbsp;<font class="FormLabel">เลขที่อ้างอิง:</font> 
                                                <asp:TextBox ID="txtRefNo" runat="server"></asp:TextBox>
                                                </td><td>
                                            &nbsp;</td><td>
                                            <font class="FormLabel">สถานะ:</font></td><td>
                                        <telerik:RadComboBox Width="330px"  ID="ddlStatus" runat="server" Filter="StartsWith" MarkFirstMatch="True" Skin="Web20" 
                                            Font-Names="Tahoma" Font-Size="8pt">
                                            <Items>
                                                <telerik:RadComboBoxItem runat="server" Text="---ทั้งหมด---" Value="ALL" />
                                                <telerik:RadComboBoxItem runat="server" Text="ส่งแล้วรอการตรวจลายมือชื่ออิเล็กทรอนิกส์ (S)" Value="S" />
                                                <telerik:RadComboBoxItem runat="server" Text="ส่งแล้วรอการตรวจสอบจากคอมพิวเตอร์ (W)" Value="W" />
                                                <telerik:RadComboBoxItem Selected="True" runat="server" Text="รอการตรวจสอบจากเจ้าหน้าที่ (Q)" Value="Q" />
                                                <telerik:RadComboBoxItem runat="server" Text="ผ่านการตรวจสอบคำขอและเอกสารแนบโดยเจ้าหน้าที่ (D)" Value="D" />
                                                <telerik:RadComboBoxItem runat="server" Text="ไม่ผ่านการตรวจสอบจากคอมพิวเตอร์ (R)" Value="R" />
                                                <telerik:RadComboBoxItem runat="server" Text="ไม่ผ่านการตรวจสอบจากเจ้าหน้าที่ (N)" Value="N" />
                                                <telerik:RadComboBoxItem runat="server" Text="ยกเลิก (C)" Value="C" />
                                                
                                             </Items>
                                             </telerik:RadComboBox></td><td>
                                                &nbsp;</td><td>
                                                &nbsp;</td></tr>
                                    <tr><td>&nbsp;</td><td>
                                                <asp:Button ID="btnSearch" runat="server" Text="ค้นหา" Width="150px" />
                                                </td><td>
                                            &nbsp;</td><td>
                                            &nbsp;</td><td>
                                                &nbsp;</td><td>
                                                &nbsp;</td><td>
                                                &nbsp;</td></tr></table>
                                    
                                    <telerik:RadGrid ID="rgRequestForm" runat="server" AllowPaging="True"
                                                                GridLines="None" PageSize="20" Skin="Office2007" TabIndex="-1" Width="100%" ShowStatusBar="True" AllowSorting="True">
                                                                <MasterTableView  AutoGenerateColumns="False" ClientDataKeyNames="invh_run_auto,form_type,SentBy,company_taxno,edi_status_id"
                                                                    DataKeyNames="invh_run_auto,form_type,SentBy,company_taxno,edi_status_id" NoMasterRecordsText="ไม่มีรายการคำร้อง" Width="100%" PagerStyle-AlwaysVisible="true" PagerStyle-FirstPageToolTip="หน้าแรก" PagerStyle-LastPageToolTip="หน้าสุดท้าย"  PagerStyle-NextPageToolTip="หน้าต่อไป" PagerStyle-PageSizeLabelText="แสดงหน้าละ" PagerStyle-PrevPageToolTip="ก่อนหน้านี้" PagerStyle-PagerTextFormat="{4}  หน้า {0} จาก {1} (รายการที่ {2} ถึง {3} จากทั้งหมด {5})">
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
                                                                            <ItemStyle Font-Names="Tahoma" Font-Size="8pt" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="invh_run_auto" HeaderText="เลขที่อ้างอิง" ReadOnly="True"
                                                                            SortExpression="invh_run_auto" UniqueName="invh_run_auto">
                                                                            <ItemStyle Font-Names="Tahoma" Font-Size="8pt" HorizontalAlign="Center" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="invoice_no1" HeaderText="Invoice No." ReadOnly="True"
                                                                            SortExpression="invoice_no1" UniqueName="invoice_no1">
                                                                            <ItemStyle Font-Names="Tahoma" Font-Size="8pt" HorizontalAlign="Center" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="company_name" HeaderText="ผู้ขอ" SortExpression="company_name"
                                                                            UniqueName="company_name">
                                                                            <ItemStyle Font-Names="Tahoma" Font-Size="8pt" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="Authorize2" HeaderText="ผู้รับมอบ" ReadOnly="True"
                                                                            SortExpression="Authorize2" UniqueName="Authorize2">
                                                                            <ItemStyle Font-Names="Tahoma" Font-Size="8pt" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="sent_date" HeaderText="วันที่ส่ง" DataFormatString="{0:dd/MM/yyyy HH:mm}"
                                                                            ReadOnly="True" SortExpression="sent_date" UniqueName="sent_date">
                                                                            <ItemStyle Font-Names="Tahoma" Font-Size="8pt" HorizontalAlign="Center" />
                                                                        </telerik:GridBoundColumn>
																		<telerik:GridBoundColumn DataField="site_name" HeaderText="สาขาที่รับ" ReadOnly="True"
                                                                            SortExpression="site_name" UniqueName="site_name">
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                       </telerik:GridBoundColumn>
                                                                       <telerik:GridBoundColumn DataField="CheckDoc_By" HeaderText="ผู้ตรวจสอบ" ReadOnly="True"
                                                                            SortExpression="CheckDoc_By" UniqueName="CheckDoc_By">
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                       </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="DESCRIPTION" HeaderText="สถานะ" ReadOnly="True"
                                                                            SortExpression="DESCRIPTION" UniqueName="DESCRIPTION">
                                                                            <ItemStyle Font-Names="Tahoma" Font-Size="8pt" HorizontalAlign="Center" />
                                                                        </telerik:GridBoundColumn>
<telerik:GridBoundColumn DataField="printDate" HeaderText="เริ่มตรวจ" DataFormatString="{0:dd/MM/yyyy HH:mm}"
                                                                            ReadOnly="True" SortExpression="printDate" UniqueName="printDate">
                                                                            <ItemStyle Font-Names="Tahoma" Font-Size="8pt" HorizontalAlign="Center" />
                                                                        </telerik:GridBoundColumn>
<telerik:GridBoundColumn DataField="checkDoc_Date" HeaderText="ตรวจเสร็จ" DataFormatString="{0:dd/MM/yyyy HH:mm}"
                                                                            ReadOnly="True" SortExpression="checkDoc_Date" UniqueName="checkDoc_Date">
                                                                            <ItemStyle Font-Names="Tahoma" Font-Size="8pt" HorizontalAlign="Center" />
                                                                        </telerik:GridBoundColumn>
<telerik:GridBoundColumn DataField="checkTotalTime" HeaderText="เวลาที่ใช้" 
                                                                            ReadOnly="True" SortExpression="checkTotalTime" UniqueName="checkTotalTime">
                                                                            <ItemStyle Font-Names="Tahoma" Font-Size="8pt" HorizontalAlign="Center" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="SentType" HeaderText="&nbsp;" ReadOnly="True"
                                                                            SortExpression="SentType" UniqueName="SentType">
                                                                            <ItemStyle Font-Names="Tahoma" ForeColor="Gray" Font-Size="8pt" />
                                                                        </telerik:GridBoundColumn>
                                                                    </Columns>
                                                                </MasterTableView>
                                                                <HeaderStyle HorizontalAlign="Center" Font-Names="Tahoma" VerticalAlign="Middle" Height="30px" Font-Size="8pt" />
                                                                <ClientSettings EnableRowHoverStyle="True">
                                                                    <Selecting AllowRowSelect="True" />
                                                                </ClientSettings>
                                                            </telerik:RadGrid>
                                                            
                                            </div>                
                                    
                                </td>
                            </tr>
                            <tr>
                                <td class="m-frm"  valign="top" width="100%">
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                        <tr>
                                            <td>
                                                <table border="0" cellpadding="0" cellspacing="2" width="100%">
                                                    <tr>
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
<asp:Label ID="lblRoleID" runat="server" Visible="false"></asp:Label>
