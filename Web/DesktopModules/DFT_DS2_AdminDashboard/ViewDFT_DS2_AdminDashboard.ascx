<%@ Control language="vb" Inherits="NTI.Modules.DFT_DS2_AdminDashboard.ViewDFT_DS2_AdminDashboard" AutoEventWireup="false" Explicit="True" Codebehind="ViewDFT_DS2_AdminDashboard.ascx.vb" %>
<%@ Register assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI" tagprefix="asp" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<telerik:RadCodeBlock ID="RadCodeBlockSecond" runat="server">
<style type="text/css">
 #tb-data td { vertical-align:top; width:80px; text-align:center; border:1px solid #ffffff; border-bottom:1px solid #eeeeee;padding:3px;font-size:10pt; }
 #tb-data td:hover {  background-color:#eeeeee; border:1px solid #cccccc;  }
</style>
<asp:UpdatePanel ID="UpdatePanel2"  runat="server">
    <ContentTemplate>

    <div id="main-wrapper" style="border:0px solid #eeeeee;margin-bottom:10px;margin-top:10px;font-size:10pt;">
        <div style="margin:10px;">
        <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
        <td><font style="font-size:18pt;"><b>ระบบจัดการข้อมูลระบบ Digital Signature</b></font><br/><b>สำหรับผู้ดูแลระบบ (Administrator Tools)</b></td>
        <td align="right"><asp:Image ID="Image7" runat="server" ImageUrl="Images/dft_edi_co_sgnature_backoffice.png"  /></td>
        </tr>
        </table>

        <table style="font-size:10pt;margin-top:20px;" id="tb-data" cellpadding="0" cellspacing="0"><tr><td width="80">
            <asp:Image ID="Image1" runat="server" ImageUrl="Images/earth.png" /><br />
            <asp:LinkButton ID="LinkButton5" CausesValidation="false" runat="server">เปลี่ยนสาขาที่รับฟอร์ม</asp:LinkButton>
            </td><td width="80">
                <asp:Image ID="Image2" runat="server" ImageUrl="Images/Backup-Green-Button-icon.png" /><br />
            <asp:LinkButton ID="LinkButton1" CausesValidation="false" runat="server">ตรวจสอบไฟล์ XML</asp:LinkButton>
            </td><td width="80">
                <asp:Image ID="Image3" runat="server" ImageUrl="Images/Appointment-Cool-icon.png" /><br />
                <asp:LinkButton ID="LinkButton2" CausesValidation="false" runat="server">ไฟล์เอกสารแนบ PDF</asp:LinkButton>
            </td>
            <td width="80">
                <asp:Image ID="Image6" runat="server" ImageUrl="Images/Appointment-Cool-icon.png" /><br />
                <asp:LinkButton ID="LinkButton6" CausesValidation="false" runat="server">ไฟล์เอกสาร Attach Sheet</asp:LinkButton>
            </td>
            <td width="80">
            <asp:Image ID="Image4" runat="server" ImageUrl="Images/Line-Chart-icon.png" />
                <br />
                <asp:LinkButton ID="linkReport" CausesValidation="false" runat="server">รายงาน (Report)</asp:LinkButton>
            </td><td width="80">
                <asp:Image ID="Image5" runat="server" ImageUrl="Images/Gear-icon.png" /><br />
                <asp:LinkButton ID="LinkButton4"  CausesValidation="false" runat="server">สาขาที่รับฟอร์ม</asp:LinkButton>
            </td>
             </td><td width="80">
                <asp:Image ID="Image16" runat="server" ImageUrl="Images/Gear-icon.png" /><br />
                <asp:LinkButton ID="LinkButton3"  CausesValidation="false" runat="server">ฟอร์มที่เปิดใช้งาน</asp:LinkButton>
            </td>
            <td width="80">
                <asp:Image ID="Image8" runat="server" ImageUrl="Images/Gear-icon.png" /><br />
                <asp:LinkButton ID="linkPrinter"  CausesValidation="false" runat="server">เครื่องพิมพ์ (Printer)</asp:LinkButton>
            </td>
            <td width="80">
                <asp:Image ID="Image9" runat="server" ImageUrl="Images/Gear-icon.png" /><br />
                <asp:LinkButton ID="LinkRollver"  CausesValidation="false" runat="server">ผลการตรวจฯ ต้นทุน (Rollver)</asp:LinkButton>
            </td>
            <td width="80">
                <asp:Image ID="Image10" runat="server" ImageUrl="Images/Gear-icon.png" /><br />
                <asp:LinkButton ID="LinkButton8"  CausesValidation="false" runat="server">ดูแบบคำขอ (ฟอร์ม)</asp:LinkButton>
            </td>
            </tr>
            <tr>
            <td width="80">
                <asp:Image ID="Image11" runat="server" ImageUrl="Images/Gear-icon.png" /><br />
                <asp:LinkButton ID="linkReStatus"  CausesValidation="false" runat="server">ย้อนสถานะแบบคำขอ</asp:LinkButton>
            </td>
            <td width="80">
                <asp:Image ID="Image12" runat="server" ImageUrl="Images/Gear-icon.png" /><br />
                <asp:LinkButton ID="linkRemoveName"  CausesValidation="false" runat="server">ลบชื่อเจ้าหน้าที่ ออกจากการตรวจแบบคำขอ</asp:LinkButton>
            </td>
            <td width="80">
                <asp:Image ID="Image13" runat="server" ImageUrl="Images/Gear-icon.png" /><br />
                <asp:LinkButton ID="LinkButton11"  CausesValidation="false" runat="server">คู่มือการใช้งาน (Manual)</asp:LinkButton>
            </td>
            <td width="80">
                <asp:Image ID="Image14" runat="server" ImageUrl="Images/Gear-icon.png" /><br />
                <asp:LinkButton ID="linkValunteer"  CausesValidation="false" runat="server">ข้อมูลบริษัทสมัครใจ</asp:LinkButton>
            </td>
            <td width="80">
                <asp:Image ID="InvoiceEDI" runat="server" ImageUrl="Images/Gear-icon.png" /><br />
                <asp:LinkButton ID="LinkInvoice"  CausesValidation="false" runat="server">ยกเลิก Invoice</asp:LinkButton>
            </td>
            <td width="80">
                <asp:Image ID="Image15" runat="server" ImageUrl="Images/Gear-icon.png" /><br />
                <asp:LinkButton ID="LinkOpenCard"  CausesValidation="false" runat="server">เปิดใช้งานบัตร Digital Signature</asp:LinkButton>
            </td>
            <td></td>
            <td></td>
            <td></td>
            </tr></table>
<p>&nbsp;</p>
<p>&nbsp;</p>
        </div>
    </div>

<div style="font-size:10pt;display:none;"><b>Recycle IIS Aplication Pool 
    <br />
    </b>
    <div style="font-size:9pt;margin-top:5px;margin-bottom:3px;">ใช้สำหรับทำการ recycle application pool ของเว็บไซต์ 
    ซึ่งจะทำให้เว้บไซต์เริ่มต้นการทำงานใหม่ทันที
    <br />
    โดยจะไม่มีผลกระทบกับเว็บไซต์อื่น</div>
    <br />
    เว็บไซต์: 
    <asp:DropDownList ID="lstAppName" runat="server">
    <asp:ListItem Text="backoffice.dft.go.th" Value="backoffice.dft.go.th"></asp:ListItem>
    <asp:ListItem Text="edi.dft.go.th" Value="edi.dft.go.th"></asp:ListItem>
    </asp:DropDownList>
    &nbsp;<asp:Button ID="btnReAppPool" runat="server" Text="Recycle" />
    <br />
    <asp:Label ID="lblRecycleAppMsg" runat="server"></asp:Label>
</div>


<p>
    <asp:PlaceHolder ID="phModule" runat="server"></asp:PlaceHolder>
</p>
    </ContentTemplate>
    </asp:UpdatePanel>
</telerik:RadCodeBlock>