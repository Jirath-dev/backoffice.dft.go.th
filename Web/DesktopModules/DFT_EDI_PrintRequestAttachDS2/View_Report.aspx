<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="View_Report.aspx.vb" Inherits=".View_ReportDS2" %>

<%@ Register Assembly="ActiveReports.Web, Version=5.3.1436.2, Culture=neutral, PublicKeyToken=cc4967777c49a3ff"
    Namespace="DataDynamics.ActiveReports.Web" TagPrefix="ActiveReportsWeb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server" >
    <title>Untitled Page</title>
	<style  runat="server" type="text/css">
		html, body, form, iframe
	    {  
	        height: 100%;  
	        margin: 0px;  
	        padding: 0px;  
	        overflow: hidden;  
	    } 
	</style>
</head>
<body style="margin:0px;">
    <form id="form1" runat="server">
    <div style="height:100%">
        <ActiveReportsWeb:WebViewer ID="WebViewer_Report" runat="server" Height="100%" Width="100%">
        </ActiveReportsWeb:WebViewer>
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
            <tr>
                <td style="width: 10%">
                </td>
                <td align="center" style="width: 80%">
                    <asp:Label ID="lblErrorReport" runat="server" Font-Bold="True" ForeColor="#CC3333"></asp:Label></td>
                <td style="width: 10%">
                </td>
            </tr>
        </table>
    </div>
	<OBJECT id=arv codeBase=bin/arview2.cab#version=-1,-1,-1,-1 
height=0 
width=0 
classid=CLSID:8569d715-ff88-44ba-8d1d-ad3e59543dde></OBJECT>
    <asp:label id="Label1" runat="server" text="Label" Visible="False"></asp:label>
        <br />
        <asp:TextBox ID="TextBox1" runat="server" Height="81px" TextMode="MultiLine" Width="826px" Visible="False"></asp:TextBox>
    </form>
    

</body>
</html>

