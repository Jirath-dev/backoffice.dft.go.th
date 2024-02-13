<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="WebFormReportBySite.aspx.vb" Inherits=".WebFormReportBySite" %>

<%@ Register Assembly="ActiveReports.Web, Version=5.3.1436.2, Culture=neutral, PublicKeyToken=cc4967777c49a3ff"
    Namespace="DataDynamics.ActiveReports.Web" TagPrefix="ActiveReportsWeb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>รายงาน</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table style="width: 100%">
            <tr>
                <td style="width: 100px">
                </td>
            </tr>
            <tr>
                <td align="center" style="width: 100px">
                    <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label></td>
            </tr>
        </table>
        <br />
        &nbsp;</div>
    </form>
</body>
</html>
