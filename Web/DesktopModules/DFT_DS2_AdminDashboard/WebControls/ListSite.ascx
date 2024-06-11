<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ListSite.ascx.vb" Inherits=".ListSite" %>
<h2>จัดการข้อมูหนังสือรับรอง</h2>
<div class="m-frm-bdr" style="border: 1px solid #999; margin-top: 10px;">
    <div class="groupboxheader" style="width: 450px;">
        <font class="groupbox">&nbsp;กำหนดหนังสือรับรอง &nbsp;</font></div>
    <div style="font-size: 8pt; margin: 10px; color: #666; border-bottom: 1px solid #ccc;
        padding-bottom: 5px;">
        ใช้สำหรับกำหนดสถานะเพื่อ เปิด-ปิด คำขอหนังสือรับรอง 
        เพื่อการตรวจสอบสำหรับเจ้าหน้าที่</div>
    <div style="margin: 20px;text-align:center">
        
        <table>
            <tr>
                <td align="center">
                    <b>ฟอร์ม</b></td>
                <td align="center">
                    &nbsp;</td>
                <td align="center">
                    <b>ฟอร์มที่เปิดใช้งานเรียบร้อยแล้ว</b></td>
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

    <%--<div style="font-size:10pt;margin-top:10px;border:1px solid #cccccc;padding:15px;">
    <h2 style="margin-bottom:10px;">เพิ่มสาขาใหม่ (New Site)</h2>
        ชื่อสาขาที่รับหนังสือ: <asp:TextBox ID="txtSiteName" runat="server"></asp:TextBox> 
        &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
            ControlToValidate="txtSiteName" ErrorMessage="ใส่ชื่อสาขาที่รับหนังสือ" 
            SetFocusOnError="True"></asp:RequiredFieldValidator>
        <br />
        รหัสสาขา:
        <asp:TextBox ID="txtSiteCode" runat="server"></asp:TextBox>
&nbsp;
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
            ControlToValidate="txtSiteCode" ErrorMessage="ใส่รหัสสาขา" 
            SetFocusOnError="True"></asp:RequiredFieldValidator>
        <br />
        <br />
        <asp:Button ID="Button1" runat="server" Text="บันทึกข้อมูล" />
        &nbsp;
        <p>&nbsp;</p>
        </div>--%>