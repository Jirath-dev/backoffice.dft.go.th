<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="Report.ascx.vb" Inherits=".Report" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<div style="font-size: 9pt; padding-top: 10px; text-align: right">
    <asp:Image ImageAlign="Middle" ID="Image2" runat="server" ImageUrl="../Images/Gear-icon.png" />
    <asp:LinkButton ID="LinkButton1" runat="server">กลับไปหน้าหลัก Admin DashBoard</asp:LinkButton></div>
<h2>
    รายงาน (Report)</h2>
<div style="font-size: 10pt; margin-top: 10px; margin-bottom: 10px; border: 1px solid #cccccc;
    padding: 5px;">
    <ul>
        <li>
            <asp:LinkButton ID="linkReport1" runat="server">รายงานจำนวนแบบคำขอ แยกตามสาขา</asp:LinkButton>
    </li>
        <li>รายงานจำนวนแบบคำขอ แยกตามประเภทฟอร์ม
    </li>
        <li>รายงานจำนวนแบบคำขอ แยกตามชื่อบริษัท
    </li>
        <li>รายงานสรุปจำนวนการขอฟอร์ม แบบรายเดือน </li>
    </ul>
</div>
