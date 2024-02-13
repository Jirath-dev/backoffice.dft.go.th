<%@ Control language="vb" Inherits="NTI.Modules.DFT_EDI2_RemoveCheckUser.ViewDFT_EDI2_RemoveCheckUser" AutoEventWireup="false" Explicit="True" Codebehind="ViewDFT_EDI2_RemoveCheckUser.ascx.vb" %>

<div class="m-frm-bdr" style="border:1px solid #999;margin-top:10px;" >
<div class="groupboxheader" style="width:120px;"><font class="groupbox">&nbsp; ค้นหาข้อมูล &nbsp;</font></div>
<div style="font-size:8pt;margin:10px;color:#666;border-bottom:1px solid #ccc;padding-bottom:5px;">
    ใช้สำหรับตรวจสอบข้อมูลผลการตรวคุณสมบัติทางด้านถิ่นกำเนิดของสินค้า ด้วยระบบคอมพิวเตอร์ (ต้นทุน)</div>
<div style="margin:20px;">
<div class="FormLabel"><b>ค้นหาตามเลขที่อ้างอิง</b>:</div>
<table border="0"><tr><td class="FormLabel">เลขที่อ้างอิง:</td><td>
    <asp:TextBox ID="txtRefNo" runat="server" ValidationGroup="op1"></asp:TextBox>
    </td><td class="FormLabel">
        เปลี่ยนชื่อเป็น</td><td>
    <asp:TextBox ID="txtUserName1" runat="server" ValidationGroup="op1"></asp:TextBox>
    </td><td>
        <asp:Button ID="btnSave1" runat="server" Text="บันทึกการแก้ไข" 
            ValidationGroup="op1" />
    &nbsp;<asp:RequiredFieldValidator CssClass="FormLabel" ID="RequiredFieldValidator1" runat="server" 
            ControlToValidate="txtRefNo" ErrorMessage="* ใส่เลขที่อ้างอิง" 
            ValidationGroup="op1"></asp:RequiredFieldValidator>
    </td></tr></table>

    <div class="FormLabel" style="margin-top:20px;"><b>กรณีค้นหาตามชื่อเจ้าหน้าที่</b>:</div>
<table border="0"><tr><td class="FormLabel"ชื่อเจ้าหน้าที่:</td>ชื่อเจ้าหน้าที่:<td>
    <asp:TextBox ID="txtUserName2" runat="server" ValidationGroup="op2"></asp:TextBox>
    </td><td>
        <asp:Button ID="btnSave2" runat="server" Text="บันทึกการแก้ไข" 
            ValidationGroup="op2" />
    &nbsp;<asp:RequiredFieldValidator CssClass="FormLabel"  ID="RequiredFieldValidator2" runat="server" 
            ControlToValidate="txtUserName2" 
            ErrorMessage="* ใส่ชื่อเจ้าหน้าที่ให้เรียบร้อยก่อน" ValidationGroup="op2"></asp:RequiredFieldValidator>
    </td></tr></table>
  </div>
  <p>&nbsp;</p>
  <p>&nbsp;</p>
</div>
&nbsp;
<div style="border:1px solid #cccccc;">
<div style="margin:20px;">
<h3>ส่วนสำหรับยกเลิกสถานะของ Invoice</h3>
<table><tr><td>เลขที่ภาษี:</td><td>
    <asp:TextBox ID="txtTaxNo" runat="server"></asp:TextBox>
    </td><td>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
            ControlToValidate="txtTaxNo" 
            ErrorMessage="* ใส่ข้อมูล เลขที่ภาษี ให้เรียบร้อยก่อน" SetFocusOnError="True"></asp:RequiredFieldValidator>
    </td></tr><tr><td>เลขที่ใบเสร็จ:</td><td>
    <asp:TextBox ID="txtInvoiceNo" runat="server"></asp:TextBox>
    </td><td>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
            ControlToValidate="txtInvoiceNo" 
            ErrorMessage="* ใส่เลขที่ Invoice ให้เรียบร้อยก่อน" SetFocusOnError="True"></asp:RequiredFieldValidator>
&nbsp;</td></tr><tr><td>&nbsp;</td><td>
        <asp:Button ID="btnInvCancle" runat="server" Text="ยกเลิก" />
        </td><td>&nbsp;</td></tr></table>
</div>
</div>