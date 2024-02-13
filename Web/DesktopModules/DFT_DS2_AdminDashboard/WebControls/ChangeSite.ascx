<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ChangeSite.ascx.vb"
    Inherits=".ChangeSite" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
    <ajaxsettings>
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
    </ajaxsettings>
</telerik:RadAjaxManager>
<div style="font-size: 9pt; padding-top: 10px; text-align: right">
    <asp:Image ImageAlign="Middle" ID="Image2" runat="server" ImageUrl="../Images/Gear-icon.png" />
    <asp:LinkButton ID="LinkButton1" runat="server">กลับไปหน้าหลัก Admin DashBoard</asp:LinkButton></div>
<h2>
    เปลี่ยนสาขาสถานที่รับฟอร์ม</h2>
<div style="font-size: 9pt; color: #666666;">
    สำหรับแบบคำขอที่ส่งด้วยระบบลายมือชื่ออิเล็กทรอนิกส์(Digital Signature) กรณีส่งเข้าสู่ระบบแล้วแต่ต้องการเปลี่ยนสาขาที่รับฟอร์ม
    <br />
    (1) ให้ทำการใส่ เลขที่อ้างอิง ของรายการที่ต้องการเปลี่ยนสาขา จากนั้นคลิกปุ่ม ค้นหา&nbsp;
    <br />
    (2) เมื่อค้นหาพบให้เลือกสาขาไปที่ สาขาใหม่ที่ต้องการรับฟอร์ม จากนั้นคลิกปุ่ม บันทึกการเปลี่ยนสาขา
    <br />
    <font style="color: #f00">* จะทำการเปลี่ยนสาขาสถานที่ออกหนังสือรับรองได้เฉพาะคำขอที่ยังไม่ได้สั่งพิมพ์เท่านั้น</font></div>
<div style="font-size: 10pt; margin-top: 20px; margin-bottom: 10px; border: 1px solid #cccccc;
    padding: 5px;">
    <table>
        <tr>
            <td>
                ค้นหาจากเลขที่อ้างอิง:
            </td>
            <td>
                <asp:TextBox ID="txtRefNo" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:Button ID="btnSearch" runat="server" Text="ค้นหา" />
                <asp:RequiredFieldValidator CssClass="FormLabel" ID="RequiredFieldValidator1" runat="server"
                    ControlToValidate="txtRefNo" ErrorMessage="* ใส่เลขที่อ้างอิง" SetFocusOnError="True"></asp:RequiredFieldValidator>
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                เลือกเปลี่ยนสาขาไปที่:
            </td>
            <td>
                <asp:DropDownList ID="ddlSite" runat="server">
                </asp:DropDownList>
            </td>
            <td>
                <asp:Button ID="btnSave" runat="server" Text="เปลี่ยนสาขาที่รับหนังสือรับรอง" Enabled="False" />
            </td>
            <td>
                <asp:Label ID="lblMsg" CssClass="FormLabel" runat="server" ForeColor="Red" Text="* คำขอนี้ไม่สามารถเปลี่ยนสถานที่ออกหนังสือรับรองได้"
                    Visible="False"></asp:Label>
            </td>
        </tr>
    </table>
</div>
<telerik:RadGrid ID="rgRequestForm" runat="server" Visible="false" GridLines="None"
    PageSize="20" Skin="Office2007" TabIndex="-1" Width="100%">
    <mastertableview autogeneratecolumns="False" clientdatakeynames="invh_run_auto,form_type,SentBy,edi_status_id,company_taxno"
        datakeynames="invh_run_auto,form_type,SentBy,edi_status_id,company_taxno" nomasterrecordstext="ไม่มีรายการคำร้อง"
        width="100%" pagerstyle-firstpagetooltip="หน้าแรก" pagerstyle-lastpagetooltip="หน้าสุดท้าย"
        pagerstyle-nextpagetooltip="หน้าต่อไป" pagerstyle-pagesizelabeltext="แสดงหน้าละ"
        pagerstyle-prevpagetooltip="ก่อนหน้านี้" pagerstyle-pagertextformat="{4}  หน้า {0} จาก {1} (รายการที่ {2} ถึง {3} จากทั้งหมด {5})">
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
                                                                        
                                                                        <telerik:GridBoundColumn DataField="edi_status" HeaderText="ผลการตรวจสอบ" ReadOnly="True"
                                                                            SortExpression="edi_status" UniqueName="edi_status">
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
                                                                </mastertableview>
    <headerstyle horizontalalign="Center" font-names="Tahoma" font-size="10pt" />
    <clientsettings enablerowhoverstyle="True">
                                                                    <Selecting AllowRowSelect="True" />
                                                                </clientsettings>
</telerik:RadGrid>
<p>&nbsp;</p>