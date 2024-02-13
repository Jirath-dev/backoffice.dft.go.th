<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frmPrintReceipt.aspx.vb" Inherits=".frmPrintReceipt" %>

<%@ Register Assembly="ActiveReports.Web, Version=5.2.1236.2, Culture=neutral, PublicKeyToken=cc4967777c49a3ff"
    Namespace="DataDynamics.ActiveReports.Web" TagPrefix="ActiveReportsWeb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>ใบเสร็จรับเงิน</title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:TextBox ID="txtBill_no" runat="server" Visible="False"></asp:TextBox>
        <asp:TextBox ID="txtSite_ID" runat="server" Visible="False"></asp:TextBox>
        <ActiveReportsWeb:WebViewer ID="WebViewer1" runat="server" Height="500px" Width="100%">
        </ActiveReportsWeb:WebViewer>
    
    </form>
</body>
</html>
