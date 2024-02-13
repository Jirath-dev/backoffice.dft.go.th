<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FormALLDetail.aspx.vb" Inherits=".FormALLDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<link href="../Popup/skin.css" rel="stylesheet" type="text/css" />
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
        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        </telerik:RadAjaxManager>
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
    function CloseAndRebind(args) {
                GetRadWindow().Close();
                GetRadWindow().BrowserWindow.refreshGrid(args);
                
                }
                function CancelEdit() {
                    GetRadWindow().Close();
                }
                function GetRadWindow() {
                    var oWindow = null;
                    if (window.radWindow) oWindow = window.radWindow; //Will work in Moz in all cases, including clasic dialog
                    else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow; //IE (and Moz as well)

                    return oWindow;
                }
                function InitializePopup(sender, eventArgs) {
                if (sender.isEmpty()) {
                    eventArgs.set_cancelCalendarSynchronization(true);
                    var popup = eventArgs.get_popupControl();
                    if (popup == sender.get_calendar()) {
                        if (popup.get_selectedDates().length == 0) {
                            var todaysDate = new Date();
                            var todayTriplet = [todaysDate.getFullYear(), todaysDate.getMonth() + 1, todaysDate.getDate()];
                            popup.selectDate(todayTriplet, true);
                        }
                    }
                    else {
                        var time = popup.getTime();
                        if (!time) {
                            time = new Date();
                            time.setHours(12);
                            popup.setTime(time);
                        }
                    }
                }
            }
    </script>
    </telerik:RadCodeBlock>
    </div>
    <table width="100%" border="0" cellpadding="0" cellspacing="5" id="TABLE1">
    <tr align="center"> 
      <td><font class=FormHeader>ใบรายการสินค้า</font></td>
    </tr>
    <tr> 
      <td><table cellspacing="0" border="0" class="m-frm-bdr" cellpadding="1" width="100%">
          <tr> 
            <td> <table cellspacing="0" cellpadding="0" border="0" width="100%">
                <tr class="m-frm-hdr"> 
                  <td > <table cellspacing="0" cellpadding="0" border="0" width="100%">
                      <tr class="m-frm-hdr"> 
                        <td width="12%" align="left" class="m-frm-hdr"><table cols=2 border=0 cellpadding=0 cellspacing=0>
                            <tr> 
                              <td nowrap class=groupboxheader><font class=groupbox>&#160;&#160;<asp:label id="lblHeader"  class=FormLabel runat="server"  />&#160;&#160;</font></td>
                              <td style="background-image: url(<%= ResolveUrl("../images") %>/groupbox/CORNER.gif); background-repeat:no-repeat;">&#160;&#160;&#160;&#160;</td>
                            </tr>
                          </table></td>
                        <td width="88%" align="left" class="m-frm-nav"><asp:label id="lbl_ErrMSG"  class=FormErr runat="server"  /><asp:textbox ID=txtInvHRunAuto runat="server" Visible="False" /><asp:textbox ID=txtInvDRunAuto runat="server" Visible="False" />&nbsp;
                        </td>
                      </tr>
                    </table></td>
                </tr>
                <tr> 
                  <td class="m-frm" valign="top" width="100%"><table width="100%" border="0" cellspacing="0" cellpadding="0">
                      <tr> 
                        <td width="15">&nbsp;</td>
                        <td ><table width="100%" border="0" cellspacing="2" cellpadding="0">
                            <tr> 
                                <td>
                                    <asp:requiredfieldvalidator id="rfvTariffCode" Display="Static" class=FormErr runat="server" ControlToValidate="txtTariffCode" ErrorMessage="*" /></td>
                              <td> <font class=FormLabel>พิกัดสินค้า</font>
                                  &nbsp;<telerik:RadTextBox ID="txtTariffCode" runat="server" CssClass="FormFld" EnableEmbeddedSkins="false"
                                      MaxLength="20" TabIndex="1" Width="200px" Enabled="False">
                                      <%--<ClientEvents OnLoad="onFocus" /> --%>
                                  </telerik:RadTextBox><font class=FormLabel>&nbsp; 
                                <asp:requiredfieldvalidator id="rfvProductName" Display="Static" class=FormErr runat="server" ControlToValidate="txtProductName" ErrorMessage="*" />
                                ชื่อสินค้า</font> 
                                  <telerik:RadTextBox ID="txtProductName" runat="server" CssClass="FormFld" EnableEmbeddedSkins="false"
                                      MaxLength="255" TabIndex="2" Width="220px" Enabled="False">
                                  </telerik:RadTextBox></td>
                            </tr>
                            <tr> 
                                <td>
                                    <asp:requiredfieldvalidator ID="rfvProductDescription" Display="Static" class=FormErr runat="server" ControlToValidate="txtProductDescription" ErrorMessage="*" /></td>
                              <td><table width="100%" border="0" cellpadding="0" cellspacing="0">
                                  <tr> 
                                    <td width="20%" valign="top"> <font class=FormLabel>รายละเอียดสินค้า</font> 
                                    </td>
                                    <td width="80%"><asp:textbox ID=txtProductDescription runat="server" class=FormFld2  TextMode="MultiLine"  Rows="4" Width="440"    MaxLength="500" TabIndex="3" /></td>
                                  </tr>
                                </table></td>
                            </tr>
                          </table></td>
                      </tr>
                    </table></td>
                </tr>
              </table></td>
          </tr>
        </table></td>
    </tr>
    <td></td>
    <tr> 
      <td align="right">
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
          <tr> 
            <td>&nbsp;</td>
            <td rowspan="2" align="right" valign="middle"><asp:button name="btnSave" id="btnSave" class="m-btn" Text="บันทึกแก้ไขรายการสินค้า" runat="server" TabIndex="9" UseSubmitBehavior="True"  />&#160; 
                <asp:Button ID="btnCancel" runat="server" CausesValidation="False" class="m-btn"
                    TabIndex="10" Text="ยกเลิก" UseSubmitBehavior="True" Width="150px" /></td>
          </tr>
          <tr> 
            <td>&nbsp;</td>
          </tr>
        </table>
          <%--<telerik:RadTextBox ID="RadTextBox1" runat="server" BackColor="TRANSPARENT" BorderStyle="None"
              ClientEvents-OnFocus="onFocus" EnableEmbeddedSkins="false" TabIndex="11" Width="1px">
              <ClientEvents OnFocus="onFocus" />
          </telerik:RadTextBox></td>--%>
    </tr>
  </table>
    </form>
</body>
</html>
