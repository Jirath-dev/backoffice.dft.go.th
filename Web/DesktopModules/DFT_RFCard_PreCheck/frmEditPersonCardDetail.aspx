<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frmEditPersonCardDetail.aspx.vb" Inherits=".frmEditPersonCardDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>ข้อมูลบัตรประจำตัวผู้นำเข้า - ส่งออก</title>
    <link href="css/skin.css" rel="stylesheet" type="text/css" />
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
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>
        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="rcbCardType">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="txtAuthName_Thai" />
                        <telerik:AjaxUpdatedControl ControlID="txtAuthPersonID" />
                        <telerik:AjaxUpdatedControl ControlID="txtAuthName" />
                        <telerik:AjaxUpdatedControl ControlID="txtAuthAddress" />
                        <telerik:AjaxUpdatedControl ControlID="txtAuthTel" />
                        <telerik:AjaxUpdatedControl ControlID="lblAuthPersonID" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>
        <telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">
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
                }
            </script>
        </telerik:RadCodeBlock>
        <table style="width: 100%">
            <tr>
                <td style="width: 100%">
                    <asp:Panel ID="Panel1" runat="server" BackColor="Honeydew" BorderColor="#CCCCCC"
                        BorderStyle="Solid" BorderWidth="1px" Width="100%">
                        <table style="width: 100%">
                            <tr>
                                <td style="width: 50%">
                                    <asp:Button ID="btnUpdate" runat="server" Text="บันทึก" Width="150px" TabIndex="3" ValidationGroup="Update" />
                                    <asp:Button ID="btnCancel" runat="server" Text="ยกเลิก" Width="150px" TabIndex="4" />
                                    <telerik:RadTextBox ID="RadTextBox1" runat="server" BackColor="Transparent" BorderStyle="None"
                                        EnableEmbeddedSkins="False" TabIndex="5" Width="1px">
                                    </telerik:RadTextBox></td>
                                <td style="width: 50%; text-align: right;">
                                    <asp:CheckBox ID="chkActiveFlag" runat="server" Font-Names="Tahoma" Font-Size="Small" Text="ยกเลิกบัตร" TextAlign="Left" TabIndex="-1" />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 50%">
                                    <asp:Label ID="lblErrMsg" runat="server" Font-Names="Tahoma" Font-Size="10pt" ForeColor="Red"></asp:Label></td>
                                <td style="width: 50%; text-align: right">
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td style="width: 100%">
                    <asp:Panel ID="Panel2" runat="server" BackColor="AliceBlue" BorderColor="#CCCCCC"
                        BorderStyle="Solid" BorderWidth="1px" Width="100%">
                    <table style="width: 100%">
                        <tr>
                            <td style="text-align: right">
                                <font class="FormLabel">TAX ID:</font></td>
                            <td colspan="3">
                                <telerik:RadTextBox ID="txtCompany_Taxno" ReadOnly="true" CssClass="FormFld" runat="server" Width="150px" EnableEmbeddedSkins="False" TabIndex="-1">
                                </telerik:RadTextBox>
                                <font class="FormLabel">สาขา:</font>
                                <telerik:RadTextBox ID="txtCompany_BranchNo" ReadOnly="true" CssClass="FormFld" Width="150px" runat="server" EnableEmbeddedSkins="False" TabIndex="-1">
                                </telerik:RadTextBox>
                                <font class="FormLabel">เลขที่เอกสาร:</font>
                                <telerik:RadTextBox ID="txtForm_ID" ReadOnly="true" CssClass="FormFld" Width="150px" runat="server" EnableEmbeddedSkins="False" TabIndex="-1">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr style="color: #000000">
                            <td style="text-align: right">
                                <font class="FormLabel">ชื่อบริษัท (อังกฤษ):</font></td>
                            <td colspan="3">
                                <telerik:RadTextBox ID="txtCompanyName" ReadOnly="true" CssClass="FormFld" Width="400px" runat="server" EnableEmbeddedSkins="False" TabIndex="-1">
                                </telerik:RadTextBox></td>
                        </tr>
                        <tr>
                            <td style="text-align: right">
                                <font class="FormLabel">รหัสบัตร:</font></td>
                            <td colspan="3">
                                <telerik:RadTextBox ID="txtcard_id" ReadOnly="true" CssClass="FormFld" Width="200px" Text="ไม่ต้องระบุ(เครื่องสร้างให้)" runat="server" EnableEmbeddedSkins="False" TabIndex="-1" Font-Names="Tahoma" Font-Size="10pt">
                                </telerik:RadTextBox>&nbsp;
                                <font class="FormLabel">ประเภทบัตร:&nbsp;</font><telerik:RadComboBox
                                    ID="rcbCardType" runat="server" AllowCustomText="True" DataSourceID="SqlCardType"
                                    DataTextField="card_desc" DataValueField="card_type" EmptyMessage="กรุณาเลือกรายการ"
                                    Filter="StartsWith" Font-Names="Tahoma" Font-Size="10pt" Skin="Office2007" AutoPostBack="True" Enabled="False" TabIndex="-1">
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right">
                                <font class="FormLabel">ชื่อผู้ถือบัตร (ไทย):</font></td>
                            <td colspan="3">
                                <telerik:RadTextBox ID="txtAuthName_Thai" CssClass="FormFld" Width="500px" runat="server" EnableEmbeddedSkins="False" TabIndex="-1">
                                </telerik:RadTextBox></td>
                        </tr>
                        <tr>
                            <td style="text-align: right">
                                <font class="FormLabel">
                                    <asp:Label ID="lblAuthPersonID" runat="server" Text="หมายเลขบัตรประจำตัวประชาชน:"></asp:Label></font></td>
                            <td colspan="3">
                                <telerik:RadTextBox ID="txtAuthPersonID" CssClass="FormFld"  Width="200px" runat="server" EnableEmbeddedSkins="False" ReadOnly="True" TabIndex="-1">
                                </telerik:RadTextBox></td>
                        </tr>
                        <tr>
                            <td style="text-align: right">
                                <font class="FormLabel">ชื่อผู้ถือบัตร (อังกฤษ):</font></td>
                            <td colspan="3">
                                <telerik:RadTextBox ID="txtAuthName" CssClass="FormFld" Width="500px" runat="server" EnableEmbeddedSkins="False" TabIndex="-1">
                                </telerik:RadTextBox></td>
                        </tr>
                        <tr>
                            <td style="text-align: right">
                                <font class="FormLabel">ที่อยู่ผู้ถือบัตร:</font></td>
                            <td colspan="3">
                                <telerik:RadTextBox ID="txtAuthAddress" CssClass="FormFld2" Width="500px" Height="70px" TextMode="MultiLine" runat="server" EnableEmbeddedSkins="False" TabIndex="-1">
                                </telerik:RadTextBox></td>
                        </tr>
                        <tr>
                            <td style="text-align: right">
                                <font class="FormLabel">วันเริ่มต้น:</font></td>
                            <td colspan="3">
                                <telerik:RadDatePicker ID="rdpStartDate" runat="server" Skin="Outlook" TabIndex="-1">
                                    <Calendar Skin="Outlook" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                        ViewSelectorText="x">
                                    </Calendar>
                                    <DatePopupButton HoverImageUrl="" ImageUrl="" TabIndex="-1" CssClass="rcCalPopup rcDisabled" />
                                    <DateInput TabIndex="-1">
                                    </DateInput>
                                </telerik:RadDatePicker>
                                &nbsp;&nbsp;
                                <font class="FormLabel">วันหมดอายุ:</font>
                                <telerik:RadDatePicker ID="rdpExpireDate" runat="server" Skin="Outlook" TabIndex="-1">
                                    <Calendar Skin="Outlook" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                        ViewSelectorText="x">
                                    </Calendar>
                                    <DatePopupButton HoverImageUrl="" ImageUrl="" TabIndex="-1" CssClass="rcCalPopup rcDisabled" />
                                    <DateInput TabIndex="-1">
                                    </DateInput>
                                </telerik:RadDatePicker>
                                
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right">
                                <font class="FormLabel">โทรศัพท์:</font></td>
                            <td colspan="3">
                                <telerik:RadTextBox ID="txtAuthTel" CssClass="FormFld" Width="200px" runat="server" EnableEmbeddedSkins="False" TabIndex="-1">
                                </telerik:RadTextBox></td>
                        </tr>
                        <tr>
                            <td style="text-align: right">
                                <span style="font-size: 10pt">สาขาที่ออกบัตร:</span>
                            </td>
                            <td colspan="3">
                                <telerik:RadComboBox
                                    ID="rcbSite" runat="server" AllowCustomText="True" DataSourceID="SqlSite" DataTextField="site_name"
                                    DataValueField="site_id" EmptyMessage="กรุณาเลือกรายการ" Filter="StartsWith"
                                    Font-Names="Tahoma" Font-Size="10pt" Skin="Office2007" Width="500px" TabIndex="-1">
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right">
                                <font class="FormLabel">เงื่อนไขการมอบอำนาจ:</font></td>
                            <td colspan="3">
                                <asp:CheckBoxList ID="chkCard_level" runat="server" Font-Names="Tahoma" Font-Size="10pt" Width="650px">
                                    <asp:ListItem Value="1">ซื้อและรับแบบพิมพ์ต่างๆ ในนามของผู้มอบอำนาจ</asp:ListItem>
                                    <asp:ListItem Value="2">ยื่นและรับใบอนุญาตและหรือหนังสือรับรองรวมทั้งรับคืนเอกสารอื่นๆ ที่ต้องแก้ไขในนามของผู้มอบอำนาจ</asp:ListItem>
                                    <asp:ListItem Value="3">ลงชื่อในคำขอและหรือคำร้องต่างๆ รวมทั้งลงชื่อให้คำรับรองในแบบพิมพ์หนังสือรับรองต่างๆ ใบอนุญาตฯ และเอกสารหลักฐานประกอบคำขอ และหรือคำร้องดังกล่าวในนามของผู้มอบอำนาจ</asp:ListItem>
                                    <asp:ListItem Value="4">ลงชื่อในหนังสือขอยกเลิก แก้ไข ชี้แจงข้อมูล รวมทั้งขอผ่อนผันการระบุข้อความในหนังสือสำคัญการส่งออก-นำเข้าสินค้าในนามของผู้มอบอำนาจ</asp:ListItem>
                                    <asp:ListItem Value="5">ลงชื่อให้คำรับรองในแบบขอรับการตรวจคุณสมบัติของสินค้าทางด้านถิ่นกำเนิด เอกสารแสดงข้อมูลเกี่ยวกับการผลิตสินค้าการส่งออก การนำเข้าสินค้า และหรือเอกสารต่างๆ ที่ต้องแสดงต่อกรมการค้าต่างประเทศในนามของผู้มอบอำนาจ</asp:ListItem>
                                </asp:CheckBoxList></td>
                        </tr>
                    </table>
                    </asp:Panel>
                </td>
            </tr>
        </table>
        <asp:SqlDataSource ID="SqlCardType" runat="server" ConnectionString="<%$ ConnectionStrings:RegisBackConnection %>"
            SelectCommand="SELECT [card_type], [card_desc] FROM [P_CardType]"></asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlSite" runat="server" ConnectionString="<%$ ConnectionStrings:RegisBackConnection %>"
            SelectCommand="SELECT [site_id], [site_name] FROM [P_Site]"></asp:SqlDataSource>
        <asp:TextBox ID="txtRegis" runat="server" Visible="False" Width="1px"></asp:TextBox>
        <asp:TextBox ID="txtRefNo" runat="server" Visible="False" Width="1px"></asp:TextBox>
        <asp:TextBox ID="txtDetailNo" runat="server" Visible="False" Width="1px"></asp:TextBox>
        <asp:TextBox ID="txtUserName" runat="server" Visible="False" Width="1px"></asp:TextBox>
    </form>
</body>
</html>
