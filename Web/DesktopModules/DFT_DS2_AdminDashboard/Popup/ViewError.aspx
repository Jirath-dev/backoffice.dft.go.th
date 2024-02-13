<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ViewError.aspx.vb" Inherits=".ViewError" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>แสดงสถานะของแบบคำขอ(EDI Status)</title>
    <style type="text/css">
        body
        {
            font-family: Tahoma,Verdana,MS-Sans-Serif;
            font-size: 10pt;
        }
        td { padding:5px; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div style="margin: 10px;">
        <h2 style="border-bottom:2px solid #cccccc;padding-bottom:3px;">
            สถานะของแบบคำขอ</h2>
        <div style="margin-bottom:10px;"><b>เลขที่อ้างอิงแบบคำขอ:</b> 
            <asp:Label ID="txtInvHRunAuto" runat="server" Text=""></asp:Label></div>
        <asp:DataGrid ID="grdErrorsData" Skin="Office2007" runat="server" ShowFooter="False"
            Width="100%" AutoGenerateColumns="False">
            <HeaderStyle BackColor="#ccffff" Font-Bold="true" Height="30" />
            <Columns>
                <asp:BoundColumn DataField="checking_by_desc" HeaderText="ตรวจสอบโดย" />
                <asp:BoundColumn DataField="error_code" HeaderText="รหัส" />
                <asp:BoundColumn DataField="error_message" HeaderText="ผลการตรวจ" />
                <asp:BoundColumn DataField="error_information" HeaderText="รายละเอียดการตรวจสอบ" />
            </Columns>
        </asp:DataGrid><asp:Label ID="lblMsg" runat="server" Visible="false" Text="">ไม่พบข้อมูลรายละเอียด<br/></asp:Label>
        <br />
        <input type="button" name="btnCancel" id="btnCancel" class="m-btn" onclick="javascript:window.returnValue = false; window.self.close();"
            value="ปิดฟอร์ม" />
    </div>
    </form>
</body>
</html>
