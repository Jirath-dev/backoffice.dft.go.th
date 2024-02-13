<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FormItemDeleted.aspx.vb" Inherits=".FormItemDeleted" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <script type="text/javascript">
 if (typeof window.event == 'undefined'){
   document.onkeypress = function(e){
 	var test_var=e.target.nodeName.toUpperCase();
 	if (e.target.type) var test_type=e.target.type.toUpperCase();
 	if ((test_var == 'INPUT' && test_type == 'TEXT') || test_var == 'TEXTAREA'){
 	  return e.keyCode;
 	}else if (e.keyCode == 8){
 	  e.preventDefault();
 	}
   }
 }else{
   document.onkeydown = function(){
 	var test_var=event.srcElement.tagName.toUpperCase();
 	if (event.srcElement.type) var test_type=event.srcElement.type.toUpperCase();
 	if ((test_var == 'INPUT' && test_type == 'TEXT') || test_var == 'TEXTAREA'){
 	  return event.keyCode;
 	}else if (event.keyCode == 8){
 	  event.returnValue=false;
 	}
   }
 }
 </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>
        <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
            <script type="text/javascript">
                function CloseAndRebind(args) {
                    GetRadWindow().Close();
                    GetRadWindow().BrowserWindow.refreshGrid(args);
                }

                function GetRadWindow() {
                    var oWindow = null;
                    if (window.radWindow) oWindow = window.radWindow; //Will work in Moz in all cases, including clasic dialog
                    else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow; //IE (and Moz as well)

                    return oWindow;
                }

                function CancelEdit() {
                    GetRadWindow().Close();
                }            </script> 
        </telerik:RadCodeBlock>
        <br />
        <table width="100%">
            <tr>
                <td style="text-align: center">
                    <table>
                        <tr>
                            <td>
                                <asp:Image ID="Image1" runat="server" ImageUrl="~/images/icon_viewstats_32px.gif" />
                            </td>
                            <td style="vertical-align: middle">
                                ท่านต้องการทำการลบข้อมูลใช่หรือไม่ ?
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="text-align: center">
                    <asp:Button ID="btnDelete" runat="server" Text="ยืนยัน" Width="150px" UseSubmitBehavior="False" />
                    <asp:Button ID="btnCancel" runat="server" Text="ยกเลิก" Width="150px" UseSubmitBehavior="False" />
                </td>
            </tr>
        </table>
    
    </div>
        <asp:TextBox ID="txtItariff_code" runat="server" Visible="False" Width="0px"></asp:TextBox><asp:TextBox
            ID="txtIcountry_code" runat="server" Visible="False" Width="0px"></asp:TextBox>
        <asp:TextBox ID="txtCAT" runat="server" Visible="False" Width="0px"></asp:TextBox>
        <asp:TextBox ID="txtform" runat="server" Visible="False" Width="0px"></asp:TextBox>
    </form>
</body>
</html>
