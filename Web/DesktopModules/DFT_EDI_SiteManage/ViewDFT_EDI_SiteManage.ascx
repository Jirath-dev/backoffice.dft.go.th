<%@ Control Language="vb" Inherits="NTI.Modules.DFT_EDI_SiteManage.ViewDFT_EDI_SiteManage"
    AutoEventWireup="false" Explicit="True" CodeBehind="ViewDFT_EDI_SiteManage.ascx.vb" %>
<div class="m-frm-bdr" style="border: 1px solid #999; margin-top: 10px;">
    <div class="groupboxheader" style="width: 450px;">
        <font class="groupbox">&nbsp;กำหนดสาขาสถานที่สำหรับรับหนังสือรับรอง &nbsp;</font></div>
    <div style="font-size: 8pt; margin: 10px; color: #666; border-bottom: 1px solid #ccc;
        padding-bottom: 5px;">
        ใช้สำหรับกำหนดสถานะเพื่อ เปิด-ปิด สาขาสถานที่สำหรับรับแบบคำขอหนังสือรับรอง 
        เพื่อการตรวจสอบสำหรับเจ้าหน้าที่</div>
    <div style="margin: 20px;text-align:center">
        
        <table>
            <tr>
                <td align="center">
                    <b>สาขา</b></td>
                <td align="center">
                    &nbsp;</td>
                <td align="center">
                    <b>สาขาที่เปิดใช้งานเรียบร้อยแล้ว</b></td>
            </tr>
            <tr>
                <td class="FormLabel">
                    <asp:ListBox ID="lstSiteClose" runat="server" style="color:Red" Height="250px" Width="300px" 
                        Rows="20">
                    </asp:ListBox>
                </td>
                <td align="center">
                    <asp:Button ID="btnAdd" style="color:Green"  runat="server" Width="100px" Text=" เปิดใช้งาน -> " />
                    <br />
                    <asp:Button ID="btnRemove" style="color:Red" runat="server" Width="100px" Text=" <- ปิดใช้งาน " />
                
                </td>
                <td>
                    <asp:ListBox ID="lstSiteOpen" runat="server" style="color:Green" Height="250px" Width="300px" 
                        Rows="20"></asp:ListBox>
                    &nbsp;
                </td>
            </tr>
        </table>
        &nbsp;<asp:Label ID="lblMsg" runat="server" ForeColor="Red"></asp:Label>
        <br />
    </div>
</div>
