<%@ Control language="vb" Inherits="NTI.Modules.DFT_EDI2_ChangeSite.ViewDFT_EDI2_ChangeSite" AutoEventWireup="false" Explicit="True" Codebehind="ViewDFT_EDI2_ChangeSite.ascx.vb" %>
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
<div class="m-frm-bdr" style="border:1px solid #999;margin-top:10px;" >
<div class="groupboxheader" style="width:120px;"><font class="groupbox">&nbsp; ค้นหาข้อมูล &nbsp;</font></div>
<div style="font-size:8pt;margin:10px;color:#666;border-bottom:1px solid #ccc;padding-bottom:5px;">
    เมื่อต้องการเปลี่ยนสาขาที่รับฟอร์ม  (1) ให้ทำการใส่ เลขที่อ้างอิง ของรายการที่ต้องการเปลี่ยนสาขา จากนั้นคลิกปุ่ม ค้นหา&nbsp; 
    (2) เมื่อค้นหาพบให้เลือกสาขาไปที่ สาขาใหม่ที่ต้องการรับฟอร์ม จากนั้นคลิกปุ่ม บันทึกการเปลี่ยนสาขา (<font style="color:#f00">* จะทำการเปลี่ยนสถานที่ออกหนังสือรับรองได้เฉพาะคำขอที่รอการตรวจสอบจากเจ้าหน้าที่เท่านั้น</font>)</div>
<table style="margin-left:20px"><tr><td width="150" class="FormLabel">ค้นหาจากเลขที่อ้างอิง:</td><td>
    <asp:TextBox ID="txtRefNo" runat="server" Width="330px"></asp:TextBox>
    </td><td>
        <asp:Button ID="btnSearch" runat="server" Text="ค้นหา" />
    &nbsp;<asp:RequiredFieldValidator  ID="RequiredFieldValidator1" runat="server" 
            ControlToValidate="txtRefNo" ErrorMessage="* ใส่เลขที่อ้างอิง" 
            SetFocusOnError="True"></asp:RequiredFieldValidator>
    </td></tr></table>
        
    <div style="margin:20px;margin-top:5px;"> 
        <telerik:RadGrid  ID="rgRequestForm" runat="server" Visible="false" 
                                                                GridLines="None" PageSize="20" Skin="Office2007" TabIndex="-1" Width="100%">
                                                                <MasterTableView  AutoGenerateColumns="False" ClientDataKeyNames="invh_run_auto,form_type,SentBy,edi_status_id,company_taxno"
                                                                    DataKeyNames="invh_run_auto,form_type,SentBy,edi_status_id,company_taxno" NoMasterRecordsText="ไม่มีรายการคำร้อง" Width="100%" PagerStyle-FirstPageToolTip="หน้าแรก" PagerStyle-LastPageToolTip="หน้าสุดท้าย"  PagerStyle-NextPageToolTip="หน้าต่อไป" PagerStyle-PageSizeLabelText="แสดงหน้าละ" PagerStyle-PrevPageToolTip="ก่อนหน้านี้" PagerStyle-PagerTextFormat="{4}  หน้า {0} จาก {1} (รายการที่ {2} ถึง {3} จากทั้งหมด {5})">
                                                                    <Columns>
                                                                        
                                                                        
                                                                        <telerik:GridBoundColumn DataField="FORM_NAME" HeaderText="ชื่อฟอร์ม" ReadOnly="True"
                                                                            SortExpression="FORM_NAME" UniqueName="FORM_NAME">
                                                                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="invh_run_auto" HeaderText="เลขที่อ้างอิง" ReadOnly="True"
                                                                            SortExpression="invh_run_auto" UniqueName="invh_run_auto">
                                                                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="invoice_no1" HeaderText="Invoice No." ReadOnly="True"
                                                                            SortExpression="invoice_no1" UniqueName="invoice_no1">
                                                                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="company_name" HeaderText="บริษัท" SortExpression="company_name"
                                                                            UniqueName="company_name">
                                                                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="sent_date" HeaderText="วันที่ส่ง" DataFormatString="{0:dd/MM/yyyy}"
                                                                            ReadOnly="True" SortExpression="sent_date" UniqueName="sent_date">
                                                                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                        </telerik:GridBoundColumn>
                                                                    
                                                                        <telerik:GridBoundColumn DataField="site_name" HeaderText="สถานที่ออกหนังสือฯ"
                                                                            ReadOnly="True" SortExpression="site_name" UniqueName="site_name">
                                                                            <ItemStyle Font-Names="Tahoma" ForeColor="Green"   Font-Size="10pt" HorizontalAlign="Center" />
                                                                        </telerik:GridBoundColumn>
                                                                        
                                                                        <telerik:GridBoundColumn DataField="description" HeaderText="ผลการตรวจสอบ" ReadOnly="True"
                                                                            SortExpression="description" UniqueName="description">
                                                                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="CheckDoc_By" HeaderText="ผู้ตรวจ" ReadOnly="True"
                                                                            SortExpression="CheckDoc_By" UniqueName="CheckDoc_By">
                                                                            <HeaderStyle Width="15px" HorizontalAlign="Center" />
                                                                            <ItemStyle Width="15px" HorizontalAlign="Center" />
                                                                       </telerik:GridBoundColumn>
                                                                       <telerik:GridBoundColumn DataField="PrintFormDate" HeaderText="วันที่พิมพ์ฟอร์ม" DataFormatString="{0:dd/MM/yyyy}"
                                                                            ReadOnly="True" SortExpression="PrintFormDate" UniqueName="PrintFormDate">
                                                                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="SentType" HeaderText="&nbsp;" ReadOnly="True"
                                                                            SortExpression="SentType" UniqueName="SentType">
                                                                            <ItemStyle Font-Names="Tahoma" ForeColor="Gray" Font-Size="8pt" />
                                                                        </telerik:GridBoundColumn>
                                                                    </Columns>
                                                                </MasterTableView>
                                                                <HeaderStyle HorizontalAlign="Center" Font-Names="Tahoma" Font-Size="10pt" />
                                                                <ClientSettings EnableRowHoverStyle="True">
                                                                    <Selecting AllowRowSelect="True" />
                                                                </ClientSettings>
                                                            </telerik:RadGrid>
                                                            
<p>
<table style="margin-top:8px;"><tr><td class="FormLabel" width="150">เลือกเปลี่ยนสาขาไปที่:</td><td>
        <asp:DropDownList ID="ddlSite" runat="server" Width="335px">
        </asp:DropDownList>
        </td><td>
            &nbsp;</td></tr><tr><td class="FormLabel" style="vertical-align:top;">เหตุผล:</td><td>
        <asp:TextBox ID="txtComment" runat="server" Height="58px" TextMode="MultiLine" Width="330px"></asp:TextBox><asp:RequiredFieldValidator  ID="RequiredFieldValidator2" runat="server" 
            ControlToValidate="txtComment" ValidationGroup="comment" ErrorMessage="* ใส่เหตุผล" 
            SetFocusOnError="True"></asp:RequiredFieldValidator>
        </td><td>
            &nbsp;</td></tr><tr><td class="FormLabel">&nbsp;</td><td>
            <asp:Button ID="btnSave" runat="server" ValidationGroup="comment" Text="บันทึกการเปลี่ยนสาขา" 
                Enabled="False" />
        <asp:Label ID="lblMsg" CssClass="FormLabel" runat="server" ForeColor="Red" 
                Text="* คำขอนี้ไม่สามารถเปลี่ยนสถานที่ออกหนังสือรับรองได้" Visible="False"></asp:Label>
        </td><td>
            &nbsp;</td></tr></table>
</p>
        </div>

    </div>