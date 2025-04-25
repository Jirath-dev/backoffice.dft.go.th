<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ViewCertDetail.aspx.vb" Inherits=".ViewCertDetail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <style type="text/css">
        body, table, td { font-size:11pt;}
        td { border:1px solid #cccccc; padding:3px;}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div style="background:#ffffff;">
    <table width="100%" cellpadding="0" cellspacing="0" style="background:#ffffff;" bgcolor="White"><tr><td rowspan="3">
            <h3 align="center">ผลการตรวจสอบคุณสมบัติทางด้านถิ่นกำเนิดของสินค้า<br />
            ด้วยระบบคอมพิวเตอร์</h3></td><td align="right">เลขที่อ้างอิง</td><td>
            <asp:Label ID="lblCertOfOrigin_No" runat="server" Font-Bold="True"></asp:Label>
            </td></tr><tr><td align="right">
            วันที่</td><td>
                    <asp:Label ID="lblCertOfOrigin_date" runat="server" Font-Bold="True"></asp:Label>
                </td></tr><tr><td align="right">วันที่หมดอายุ</td><td>
                <asp:Label ID="lblExpireDate" runat="server" Font-Bold="True"></asp:Label>
                </td></tr></table>
            <table width="100%" cellpadding="0" style="background:#ffffff;"  cellspacing="0"><tr><td class="style1" 
                    width="25%">ผู้ยื่นคำขอฯ</td>
                <td colspan="3">
                    <asp:Label ID="lblCompany_name_thai" runat="server" Font-Bold="True"></asp:Label>
                    <br />
                    <asp:Label ID="lblCompany_name_eng" runat="server" Font-Bold="True"></asp:Label>
                    <br />
                </td></tr><tr><td class="style1">เลขประจำตัวผู้เสียภาษี</td><td colspan="3">
                    <asp:Label ID="lblTaxId" runat="server" Font-Bold="True"></asp:Label>
                    </td></tr><tr><td colspan="4">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                    ได้ผ่านการตรวจสอบคุณสมบัติทางด้านถิ่นกำเนิดของสินค้าที่จะขอใช้สิทธิพิเศษทางด้านภาษีศุลกากร&nbsp; 
                    ด้วยระบบคอมพิวเตอร์ของกรมการค้าต่างประเทศ&nbsp; 
                    ตามข้อมูลอิเล็คทรอนิกส์ของผู้ยื่นคำขอฯแล้ว&nbsp; 
                    เป็นไปตามหลักเกณฑ์ภายใต้กฏว่าด้วยถิ่นกำเนิดสินค้า&nbsp; ตามรายละเอียดดังต่อไปนี้ะเอียดดังต่อไปนี้</td></tr><tr>
                    <td valign="top" >ชื่อสินค้า</td><td colspan="3">
                    <asp:Label ID="lblGood_desc_th" runat="server" Font-Bold="True"></asp:Label>
                    <br />
                    <br />
                    <asp:Label ID="lblGood_name_eng" runat="server" Font-Bold="True"></asp:Label>
                    <br />
                    </td></tr><tr>
                    <td class="style1" valign="top">ประเทศที่ขอใช้สิทธิฯ</td><td width="40%">
                    <asp:Label ID="lblCountry" runat="server" Font-Bold="True"></asp:Label>
                    </td><td align="center">พิกัดศุลกากรที่</td><td>
                    <asp:Label ID="lblHamonized_no" runat="server" Font-Bold="True"></asp:Label>
                    </td></tr><tr>
                    <td class="style1" colspan="4" valign="top">ชื่อรุ่นของสินค้า<br />
                        <br />
                        <asp:Label ID="lblModels" runat="server" Font-Bold="True"></asp:Label>
                        <br />
                        <br />
                    </td></tr><!--tr>
                    <td class="style1" valign="top">ชื่อผู้ผลิตสินค้า</td><td colspan="3" 
                        width="40%">
                    <asp:Label ID="lblCompany_name_th2" runat="server" Font-Bold="True"></asp:Label>
                    </td></tr--></table>
            
    </div>
    <br />
    <center><asp:Label ID="lblMsg" runat="server" ForeColor="#FF3300"></asp:Label>
        <br />
    &nbsp;</center>
    <asp:Label ID="lblCertNo" runat="server" ForeColor="#CCCCCC"></asp:Label>
    </div>
    </form>
</body>
</html>
