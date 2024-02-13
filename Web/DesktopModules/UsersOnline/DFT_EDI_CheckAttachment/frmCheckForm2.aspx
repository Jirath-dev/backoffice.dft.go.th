<%@ Page Language="vb" AutoEventWireup="false" Codebehind="frmCheckForm2.aspx.vb"
    Inherits=".frmCheckForm2" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>รายละเอียดคำร้องจากไฟล์ XML</title>
    <link href="CSS/skin.css" rel="stylesheet" type="text/css" />
    <style id="headerscript" runat="server" type="text/css">
        html, body, form, iframe
	    {  
	        height: 100%;  
	        margin: 0px;  
	        padding: 0px;  
	        overflow: hidden;  
	    } 
        .label-msg
        {
            width: 98%;
            color: #ff0000;
            font-weight: bold;
            padding: 5px;
            background: #FF9;
            border: 1px solid #666666;
        }
        .check-result
        {
            padding: 5px;
            font-size: 10pt;
            color: #ffffff;
             background:#5a7fa5;
            text-align:center;
        }
       #RadTabStrip1 a:hover { color:#f00; }
    </style>
</head>
<body onload="SendValueToPdf();">
    <form id="form1" runat="server">
        <telerik:radscriptmanager id="RadScriptManager1" runat="server">
    </telerik:radscriptmanager>
        <telerik:radajaxmanager id="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadAjaxManager1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdItemData" />
                    <telerik:AjaxUpdatedControl ControlID="grdAttachFile" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdItemData">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdItemData" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdAttachFile">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdAttachFile" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:radajaxmanager>
        <telerik:radcodeblock id="RadCodeBlockFirst" runat="server">

        <script type="text/javascript">
                function ShowViewForm(invh_run_auto, invd_run_auto, action)
                {
                    window.radopen("/DesktopModules/DFT_EDI_CheckAttachment/POPUP/frmCheckItem_Form1.aspx?InvHRunAuto=" + invh_run_auto + "&InvDRunAuto=" + invd_run_auto + "&action=" + action, "ViewDialog");
                    return false;
                }

                function refreshGrid(arg)
                {
                    if(!arg)
                    {
                        $find("<%= RadAjaxManager1.ClientID %>").ajaxRequest("Rebind");			        
                    }
                    else
                    {
                        $find("<%= RadAjaxManager1.ClientID %>").ajaxRequest("RebindAndNavigate");			        
                    }
                }
                
                function ShowCommentForm()
                {
                    window.radopen("/DesktopModules/DFT_EDI_CheckAttachment/frmCancelForm.aspx?RefNo=<%= Request.QueryString("RefNo") %>", "CommentDialog");
                    return false;
                }
                
                function CloseMyWin(){
                    try{window.opener.refreshGrid();}catch(e){}
	                window.close();
                }
                
                function ShowRestatus(){
                     var w = 600;
                     var h = 320;
                     var winl = (screen.width - w)/2;
                     var wint = ((screen.height - h)/2)-20;
                     var winprops = 'height='+h+',width='+w+',top='+wint+',left='+winl+',scrollbars=1,resizable=1';
   
                     objWin = window.open("ReCheckStatus.aspx?id=<%= Request.QueryString("RefNo") %>",null,winprops);
                     objWin.focus();
                     
                     return false;
                }
                               
                function GetTaxId(taxid) {
                    var str_taxid = "";
                    try {
                        if (taxid.length >= 13) {
                            str_taxid = taxid.substr(0, 13);
                        } else if (taxid.length > 10) {
                            str_taxid = taxid.substr(0, 10);
                        } else {
                            str_taxid = taxid;
                        }
                    } catch (ex) { /**/ }
                    return str_taxid;
                }
                
                 function checkCertificate(){ 
			        try{
                		    var objSign;
                		    var sdataforsign = 'dft_seal_sign' ; 
                		    var objSign = new ActiveXObject("NTISignLib.Signature");
							//alert(document.getElementById('<%=UserTaxID.ClientID %>').value);
							var result = objSign.SignString(document.getElementById('<%=UserTaxID.ClientID %>').value,sdataforsign);
				            if(result==""){ 
				                alert('ไม่สามารถลงลายมือชื่ออิเล็กทรอนิกส์ได้ \nกรุณาตรวจสอบ Certificate ให้พร้อมใช้งานให้เรียบร้อยก่อน');
				                return false;
				            }else{
				                //ShowLoading();
	  			                return confirm('คุณแน่ใจว่าต้องการบันทึกผลการ ผ่านการตรวจสอบเอกสาร พร้อมลงลายมือชื่ออิเล็กทรอนิกส์ให้กับข้อมูลนี้ ใช่หรือไม่? \n\nกรุณาเลือกคลิกปุ่ม [OK]=บันทึกผลผ่านการตรวจสอบ \nหรือปุ่ม [Cancel]=ยกเลิก');
				            }
            		    }catch(e){
                		    alert('เกิดข้อผิดพลาด! ไม่สามารถลงลายมือชื่ออิเล็กทรอนิกส์ได้ \nกรุณาตรวจสอบ Certificate ให้พร้อมใช้งานให้เรียบร้อยก่อน');
				            return false;
            		    }
		           }
                                
                function ViewCertOfOrigin(){
                     var w = screen.width-300;
                     var h = screen.height - 350;
                     var winl = (screen.width - w)/2;
                     var wint = ((screen.height - h)/2)-20;
                     var winprops = 'height='+h+',width='+w+',top='+wint+',left='+winl+',scrollbars=1,resizable=1';
                
                        objWin = window.open("ViewCertOfOrigin.aspx?taxno="+GetTaxId('<%= Request.QueryString("TaxNo") %>')+"&invh_run_auto=<%= Request.QueryString("RefNo") %>",null,winprops);
                        objWin.focus();
                  }
        </script>

    </telerik:radcodeblock>
        <telerik:radwindowmanager id="RadWindowManager1" behavior="Close" runat="server"
            skin="Office2007" tabindex="-1" overlay="True">
        <Windows>
            <telerik:RadWindow ID="ViewDialog" runat="server" Title="แสดงรายการสินค้า" ReloadOnShow="True"
                ShowContentDuringLoad="False" Modal="True" VisibleStatusbar="False" Width="800px"
                Height="400px" Style="display: none;" InitialBehavior="None" Left="" NavigateUrl=""
                Top="" />
            <telerik:RadWindow ID="CommentDialog" runat="server" Title="แสดงรายการสินค้า" ReloadOnShow="True"
                ShowContentDuringLoad="False" Modal="False" Behaviors="Minimize,Close,Move" VisibleStatusbar="False"
                Width="800px" Height="520px" />
        </Windows>
    </telerik:radwindowmanager>
        <div id="ParentDivElement" style="height: 100%;">
            <telerik:radsplitter id="RadSplitter1" runat="server" visibleduringinit="False" width="100%"
                height="100%" liveresize="True">
        xxxxxxxxx
        <telerik:RadPane ID="RadPane1" BackColor="#cbe1ef" runat="server" Width="50%" Height="100%"><asp:Panel ID="Panel1" runat="server" class="label-msg" Visible="False"><asp:Image ID="Image1" runat="server" ImageAlign="AbsMiddle" ImageUrl="~/images/yellow-warning.gif" /><asp:Label
                    ID="lblMsg" runat="server" Font-Size="Small"></asp:Label></asp:Panel>
                    <asp:Panel ID="panelSealSign" runat="server" Font-Size="10pt" Visible="True">
            <div style="background:orange;padding:5px;text-align:center;color:#ffffff;">
            แบบคำขอนี้เป็นระบบ  SEAL & SIGN จำเป็นต้องใช้ USB Token สำหรับการบันทึกผลการอนุมัติเท่านั้น
            </div>
            </asp:Panel>
                    <table border="0" cellpadding="0" width="97%" cellspacing="0">
                <tr>
                    <td valign="top" style="width: 310px">
                        <table width="100%" border="0" cellspacing="3" cellpadding="0">
                            <tr>
                                <td style="width: 300px">
                                    <table cellspacing="0" border="0" class="m-frm-bdr" cellpadding="1" width="100%">
                                        <tr>
                                            <td>
                                                <table cellspacing="0" cellpadding="0" border="0" width="100%">
                                                    <tr class="m-frm-hdr">
                                                        <td>
                                                            <table cellspacing="0" cellpadding="0" border="0" width="100%">
                                                                <tr class="m-frm-hdr">
                                                                    <td width="100%" align="left" class="m-frm-hdr">
                                                                        <table border="0" cellpadding="0" cellspacing="0">
                                                                            <tr>
                                                                                <td nowrap class="groupboxheader">
                                                                                    <font class="groupbox">&#160;&#160;รายละเอียดหนังสือรับรอง&#160;&#160;</font>
                                                                                </td>
                                                                                <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
                                                                                    <td style="background-image: url(<%= ResolveUrl("images") %>/CORNER.gif); background-repeat: no-repeat;">
                                                                                        &#160;&#160;&#160;&#160;
                                                                                    </td>
                                                                                </telerik:RadCodeBlock>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="m-frm" valign="top" width="100%">
                                                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                <tr>
                                                                    <td>
                                                                        <table width="100%" border="0" cellspacing="2" cellpadding="0">
                                                                            <tr>
                                                                                <td>
                                                                                    <div style="text-align: center; margin-top: 8px; margin-bottom: 8px; height: 25px;">
                                                                                        <font class="FormLabel">เลขที่อ้างอิง :</font>
                                                                                        <asp:Label ID="lblInvh_Run_Auto" runat="server" ForeColor="#0000C0"></asp:Label><asp:Label
                                                                                            ID="lblFormType" runat="server"></asp:Label></div>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td valign="top">
                        <asp:Panel ID="panelResult" runat="server">
                            <table width="97%" border="0" cellspacing="3" cellpadding="0">
                                <tr>
                                    <td>
                                        <table cellspacing="0" border="0" class="m-frm-bdr" cellpadding="1" width="100%">
                                            <tr>
                                                <td>
                                                    <table cellspacing="0" cellpadding="0" border="0" width="100%">
                                                        <tr class="m-frm-hdr">
                                                            <td>
                                                                <table cellspacing="0" cellpadding="0" border="0" width="100%">
                                                                    <tr class="m-frm-hdr">
                                                                        <td width="88%" align="left" class="m-frm-hdr">
                                                                            <table cols="2" border="0" cellpadding="0" cellspacing="0">
                                                                                <tr>
                                                                                    <td nowrap class="groupboxheader">
                                                                                        <font class="groupbox">&#160;&#160;บันทึกผลการตรวจสอบเอกสาร&#160;&#160;</font>
                                                                                    </td>
                                                                                    <telerik:RadCodeBlock ID="RadCodeBlock9" runat="server">
                                                                                        <td style="background-image: url(<%= ResolveUrl("images") %>/CORNER.gif); background-repeat: no-repeat;">
                                                                                            &#160;&#160;&#160;&#160;
                                                                                        </td>
                                                                                    </telerik:RadCodeBlock>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                        <td width="12%" align="left" class="m-frm-nav">
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="m-frm" valign="top" width="100%">
                                                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                    <tr>
                                                                        <td width="15">
                                                                            &#160;
                                                                        </td>
                                                                        <td>
                                                                            <table border="0" cellspacing="2" cellpadding="0">
                                                                                <tr>
                                                                                    <td style="text-align: center">
                                                                                        <div style="margin-top: 8px; margin-bottom: 8px;">
                                                                                             <asp:Button ID="Button1" runat="server" Text="ผ่านการตรวจสอบ" OnClientClick="return confirm('คุณแน่ใจว่าต้องการกำหนดให้แบบฟอร์มนี้ ผ่านการตรวจสอบเอกสาร ใช่หรือไม่? \nกรุณาเลือกคลิกปุ่ม [OK]=ผ่านการตรวจสอบ หรือปุ่ม [Cancel]=ไม่ผ่าน');"
                                                                                                Width="120px" Font-Names="Tahoma" Font-Size="10pt" BackColor="#80FF80" /><asp:Button ID="btnSignOK" runat="server" BackColor="#80FF80" Font-Names="Tahoma" 
                                                                                                Font-Size="10pt" style="margin-bottom:5px;width:230px;" 
                                                                                                OnClientClick="return checkCertificate();" 
                                                                                                Text="ผ่านการตรวจสอบ พร้อมลงลายมือชื่อฯ" Visible="True" /> &#160;<asp:Button
                                                                                                    ID="btnNotApproved" runat="server" Text="ไม่ผ่านการตรวจสอบ" Width="135px" Font-Names="Tahoma"
                                                                                                    Font-Size="10pt" ValidationGroup="NotApprove" OnClientClick="return ShowCommentForm();"
                                                                                                    BackColor="#FF8000" /></div>
                                                                                                    <asp:Button ID="btnSignData" runat="server" CausesValidation="False" UseSubmitBehavior="True" Width="0px" style="width:0px;display:none;" />
                                                                                                    <asp:HiddenField ID="DataText" runat="server" />
                                                                                                    <asp:HiddenField ID="DataSigned" runat="server" />
                                                                                                    <asp:HiddenField ID="Signed" runat="server" Value="false" />
                                                                                                    <asp:HiddenField ID="UserTaxID" runat="server" Value="" />
                                                                                                    <asp:HiddenField ID="UserName" runat="server" Value="" />
                                                                                                    <asp:HiddenField ID="SignID" runat="server" Value="" />
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <asp:Label ID="lblCheckResult" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
                    <asp:Panel runat="server" ID="panelInfo" Style="border-top: 1px solid #cccccc; border-bottom: 2px solid #cccccc;
                font-size: 10pt; margin: 3px; padding: 5px; margin-right:18px; background: #ffc888">
                <asp:Label runat="server" ID="lblResultCheck"></asp:Label>
                <asp:Label runat="server" ID="lblValunteer"></asp:Label>
                <div style="border-bottom: 1px solid #ffffff; margin-bottom: 5px; margin-top: 5px;"></div><div style="text-align:center"><asp:Button runat="server" CausesValidation="false" UseSubmitBehavior="false" OnClientClick="return ShowRestatus();" Visible="false" ID="btnReStatus" Text="ย้อนสถานะผลการตรวจสอบ" /></div></asp:Panel>
                <div style="margin: 3px; margin-right: 20px;">
                <asp:Panel ID="panelFile" runat="server">
                <telerik:RadGrid ID="grdAttachFile" runat="server" Font-Names="Tahoma" Font-Size="X-Small"
                    GridLines="None" Skin="Office2007" TabIndex="-1"><MasterTableView AutoGenerateColumns="False" CellSpacing="-1" ClientDataKeyNames="FileID"
                        DataKeyNames="InvH_Run_Auto,FileID,FilesName" NoMasterRecordsText="ไม่มีรายการเอกสาร"><Columns><telerik:GridBoundColumn DataField="FileID" ReadOnly="True" SortExpression="FileID"
                                UniqueName="FileID" Visible="False"><ItemStyle Font-Names="Tahoma" Font-Size="10pt" /></telerik:GridBoundColumn><telerik:GridBoundColumn DataField="InvH_Run_Auto" ReadOnly="True" SortExpression="InvH_Run_Auto"
                                UniqueName="InvH_Run_Auto" Visible="False"><ItemStyle Font-Names="Tahoma" Font-Size="10pt" /></telerik:GridBoundColumn><telerik:GridBoundColumn DataField="FilesName" HeaderText="Hide" SortExpression="FilesName"
                                UniqueName="FilesName" Visible="False"><ItemStyle Font-Names="Tahoma" Font-Size="10pt" /></telerik:GridBoundColumn><telerik:GridTemplateColumn UniqueName="TemplateViewColumn" HeaderText=""><HeaderStyle Width="20" /><ItemStyle Font-Names="Tahoma" Font-Size="10pt" /><ItemTemplate><a href="#" onclick="changePdf('<%#EVAL("FilesName") %>')"><asp:Image ID="Image2" ImageUrl="~/images/view.gif" ToolTip="View" runat="server" /></a></ItemTemplate></telerik:GridTemplateColumn><telerik:GridTemplateColumn UniqueName="TemplateViewColumn" HeaderText="ประเภท"><ItemStyle Font-Names="Tahoma" Font-Size="10pt" /><ItemTemplate>
                                    <%#Eval("AttachType")%>
                                </ItemTemplate></telerik:GridTemplateColumn><telerik:GridTemplateColumn UniqueName="TemplateViewColumn" HeaderText="ชื่อไฟล์เอกสาร"><ItemStyle Font-Names="Tahoma" Font-Size="10pt" /><ItemTemplate><a href="#" onclick="changePdf('<%#EVAL("FilesName") %>')"><%#EVAL("Descript")%> </a></ItemTemplate></telerik:GridTemplateColumn></Columns></MasterTableView><HeaderStyle HorizontalAlign="Center" Font-Names="Tahoma" Font-Size="10pt" /><ClientSettings><Selecting AllowRowSelect="True" /></ClientSettings></telerik:RadGrid>
                                
                                <div class="RadGrid RadGrid_Office2007" style="font-family: Tahoma; font-size: X-Small;"><telerik:RadCodeBlock ID="RadCodeBlock11" runat="server"><table border="0" class="rgMasterTable" cellpadding="0" cellspacing="0" style="width: 100%;
                            table-layout: auto; empty-cells: show;"><asp:Panel runat="server" ID="PanelInv"><tr class="rgRow"><td style="width: 16px;"><a target="PDFFrame" href="Invoice.aspx?RefNo=<%= Request.QueryString("RefNo") %>"><asp:Image ID="Image3" BorderStyle="None" ImageUrl="~/images/view.gif" ToolTip="View"
                                                ImageAlign="AbsMiddle" runat="server" /></a></td><td><a target="PDFFrame" href="Invoice.aspx?RefNo=<%= Request.QueryString("RefNo") %>">ใบกำกับสินค้า(Invoice)</a> </td></tr></asp:Panel><asp:Panel runat="server" ID="PanelBL"><tr class="rgRow"><td style="width: 16px;"><a target="PDFFrame" href="BL.aspx?RefNo=<%= Request.QueryString("RefNo") %>"><asp:Image ID="Image4" BorderStyle="None" ImageUrl="~/images/view.gif" ToolTip="View"
                                                ImageAlign="AbsMiddle" runat="server" /></a></td><td><a target="PDFFrame" href="BL.aspx?RefNo=<%= Request.QueryString("RefNo") %>">หลักฐานการส่งสินค้า(B/L หรือ AWB หรือ FCR)</a> </td></tr></asp:Panel></table></telerik:RadCodeBlock></div>
                                
                                                    </asp:Panel>
                    <asp:label ID="lblValunteer2" runat="server" Font-Size="10pt" ForeColor="Green"><div style="text-align:center">*** บริษัท สมัครใจ ***</div></asp:label>

                                <telerik:RadCodeBlock ID="RadCodeBlock3" runat="server">
                                <% If Request.QueryString("form_type").ToUpper() <> "FORM2" Then%>
                                            <div style="padding: 5px; padding-left: 8px; border: 1px solid #666666; border-top: 2px solid #ff0000;
                    background: #ffffff;">
                    <table width="100%" cellpadding="0" cellspacing="0"><tr><td align="center" style="font-size: 8pt; color: #666666;">
                ผลการตรวจสอบคุณสมบัติทางด้านถิ่นกำเนิดของสินค้า: &nbsp;<asp:Button
                                    ID="btnViewRollver" runat="server" Text="แสดงต้นทุน" OnClientClick="return ViewCertOfOrigin();return false;"
                                    CausesValidation="false" UseSubmitBehavior="false" /></td></tr></table></div>
                                 <% End If%>
                                 </telerik:RadCodeBlock>
                                    <telerik:RadCodeBlock ID="RadCodeBlock2" runat="server"><script type="text/javascript">
                        function SendValueToPdf() {
                            var myFrame = document.getElementById('PDF_FRAME');
                            myFrame.src = "frmFormPDFPage.aspx?INVH_RUN_AUTO=" + '<%= Request.QueryString("RefNo") %>';
                        }

                        function ShowRequestToPdf() {
                            var myFrame = document.getElementById('PDF_FRAME');
                            myFrame.src = "/DesktopModules/DFT_EDI_PrintRequestAttachDS2/View_Report.aspx?action=viewprint&SendCell=" + '<%= Request.QueryString("RefNo") %>' + "&CellType=" + '<%= Request.QueryString("form_type") %>' + "&SendSiteID=ST-001&radioForm=0";
                        }

                        function Show2Pdf() {
                            SendValueToPdf();
                            SendRequestToPdf();
                        }
                        
                        function SendRequestToPdf() {
                            //var myFrame2 = document.getElementById('PDF_REQUEST_FRAME');
                            //myFrame2.src = "/DesktopModules/DFT_EDI_PrintRequestAttachDS2/View_Report.aspx?action=viewprint&SendCell="+'<%= Request.QueryString("RefNo") %>'+"&CellType=" + '<%= Request.QueryString("form_type") %>' + "&SendSiteID=ST-001&radioForm=0";
                            var myFrame = document.getElementById('PDFFrame');
                            myFrame.src = "/DesktopModules/DFT_EDI_PrintRequestAttachDS2/View_Report.aspx?action=viewprint&SendCell=" + '<%= Request.QueryString("RefNo") %>' + "&CellType=" + '<%= Request.QueryString("form_type") %>' + "&SendSiteID=ST-001&radioForm=0";
                        }
                        
                    </script></telerik:RadCodeBlock>
                    <div id="tabMenu" style="border-bottom:3px solid #333;margin-top:3px;">
                <telerik:RadTabStrip AutoPostBack="false" Font-Names="Tahoma" Skin="Default" ID="RadTabStrip1" runat="server">
                 <Tabs>
                      <telerik:RadTab PostBack="false" runat="server"  NavigateUrl="javascript:SendValueToPdf();" Text='แบบฟอร์ม'>
                      </telerik:RadTab>
                      <telerik:RadTab PostBack="false" runat="server" Text="แบบคำร้อง" NavigateUrl="javascript:ShowRequestToPdf();">
                      </telerik:RadTab>
                      <telerik:RadTab PostBack="false" runat="server" Text="แบบฟอร์มและแบบคำร้อง" 
                          NavigateUrl="javascript:Show2Pdf();" >
                      </telerik:RadTab>
                  </Tabs>
                </telerik:RadTabStrip>
                </div>
					<div style="height:700px;">
					<iframe runat="server" height="100%" width="100%" frameborder="0" name="PDF_FRAME"
                    id="PDF_FRAME"></iframe>
					</div>
					</telerik:RadPane>
        <telerik:RadSplitBar ID="RadSplitBar1" runat="server" CollapseMode="Both" />
        <telerik:RadPane ID="RadPane2" runat="server" Width="50%" Height="100%"><script type="text/javascript">
                function changePdf(filename) {
					var dd = new Date();
                    var myFrame = document.getElementById('PDFFrame');
                    myFrame.src = "http://10.3.0.109/Portals/0/DocumentFiles/" + filename+ "?v="+dd.getTime();
                }
            </script>
			<div style="height:99%">
			<iframe id="PDFFrame" name="PDFFrame" height="100%" width="100%" frameborder="0"></iframe>
			</div>
			</telerik:RadPane>
    </telerik:radsplitter>
        </div>
        <asp:Label ID="lblRoleID" runat="server" Visible="False"></asp:Label>
        <asp:Label ID="lblSite_ID" runat="server" Visible="False"></asp:Label>
        <asp:Label ID="lblUser_ID" runat="server" Visible="False"></asp:Label>
        <asp:HiddenField ID="START_CHCEK_TIME" runat="server" />
    </form>
</body>
</html>
