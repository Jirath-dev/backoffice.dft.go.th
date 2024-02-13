<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ViewCertOfOrigin.aspx.vb"
    Inherits=".ViewCertOfOrigin" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
		html, body, form, iframe
	    {  
	        height: 100%;  
	        margin: 0px;  
	        padding: 0px;  
	        overflow: hidden;  
	    } 
        body, table, td
        {
            font-size: 10pt;
        }
        td
        {
            border: 1px solid #cccccc;
            padding: 3px;
        }
        .text-data 
        {
            color:#0000FF;
        }
        a { text-decoration:none; }
        a:hover{ color: #CC3300; text-decoration:underline; }
        .rgAltRow td.selected { background:#CCFF66;}
        .rgRow td.selected { background:#CCFF66;}
    </style>
    <telerik:radcodeblock runat="server">
        <script type="text/javascript" language="javascript">
        <!--
            function resize_spliter() {
                var splitter = $find("<%= RadSplitter1.ClientID %>");
                //var pane = splitter.getPaneById(paneID);
                var w = GetWidth()-200;
                var h = GetHeight();
                splitter.set_height(h - 2);
                splitter.set_weight(w - 2);
            }
            
//            window.onresize = resize_spliter;
//            window.onload = resize_spliter;


            function GetWidth() {
                var x = 0;
                if (self.innerHeight) {
                    x = self.innerWidth;
                }
                else if (document.documentElement && document.documentElement.clientHeight) {
                    x = document.documentElement.clientWidth;
                }
                else if (document.body) {
                    x = document.body.clientWidth;
                }
                return x;
            }

            function GetHeight() {
                var y = 0;
                if (self.innerHeight) {
                    y = self.innerHeight;

                }
                else if (document.documentElement && document.documentElement.clientHeight) {
                    y = document.documentElement.clientHeight;
                }
                else if (document.body) {
                    y = document.body.clientHeight;
                }
                return y;
            }

        //-->
        </script>
    </telerik:radcodeblock>
</head>
<body style="margin: 0px;" onblur="window.focus();">
    <form id="form1" runat="server" style="margin: 0px;">
        <telerik:radscriptmanager id="RadScriptManager1" runat="server">
    </telerik:radscriptmanager>
        <telerik:radajaxmanager id="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadAjaxManager1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="GridData" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="GridData">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="GridData" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:radajaxmanager>
        <div id="ParentDivElement" style="height: 100%;">
            <telerik:radsplitter id="RadSplitter1" runat="server" visibleduringinit="False" width="100%"
                liveresize="True" height="100%" style="margin-right: 0px">
        <telerik:RadPane ID="RadPane1" runat="server" Width="30%" Height="100%">
   
    <div style="margin:3px;">
   
        <telerik:RadGrid ID="GridData" runat="server" AllowPaging="True" 
            GridLines="None" PageSize="20" Skin="Office2007">
            <MasterTableView AutoGenerateColumns="false">
                <Columns>
                    <telerik:GridTemplateColumn DataField="Invh_run_auto" HeaderText="NO." 
                        UniqueName="Invh_run_auto">
                        <HeaderStyle HorizontalAlign="Center" Width="40" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemTemplate>
                            <%#(gridData.PageSize * gridData.CurrentPageIndex) + Container.ItemIndex + 1%>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                   
                    <telerik:GridTemplateColumn DataField="product_name" HeaderText="PRODUCT DESCRIPTION" 
                        UniqueName="product_name">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <a href="#" 
                                onclick='return changePdf(this,&#039;<%#Eval("CHECK_ASSET")%>&#039;,&#039;<%#Eval("ASSET_SHARES")%>&#039;,&#039;<%#Eval("hasCostPdf")%>&#039;,&#039;<%#Eval("TaxNo")%>&#039;,&#039;<%#Eval("CHECK_ASSET_COUNTRY")%>&#039;,&#039;<%#Eval("ASSET_SHARES_COUNTRY")%>&#039;,&#039;<%#Eval("ASSET_SHARES_TAX")%>&#039;);'>
                            <%#Eval("product_description")%></a>
                        </ItemTemplate>
 	                </telerik:GridTemplateColumn>
                    <telerik:GridBoundColumn DataField="tariff_code" HeaderText="HS Code" ReadOnly="True"
                           SortExpression="tariff_code" UniqueName="tariff_code">
                           <ItemStyle Font-Names="Tahoma" Font-Size="10pt" VerticalAlign="Top"/>
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn visible="true" DataField="ASSET" HeaderText="เลขที่ต้นทุน" ReadOnly="True"
                           SortExpression="ASSET" UniqueName="ASSET">
                           <ItemStyle Font-Names="Tahoma" Font-Size="10pt" VerticalAlign="Top"/>
                    </telerik:GridBoundColumn>
                </Columns>
            </MasterTableView>
            <ClientSettings EnableRowHoverStyle="True">
                <Selecting AllowRowSelect="True" />
            </ClientSettings>
        </telerik:RadGrid>
    
    <br />
    <center><asp:Label ID="lblMsg" runat="server" ForeColor="#FF3300"></asp:Label>
        <br />
    <input type="button" value="ปิดหน้าต่างนี้" style="margin-top:5px;" onclick="javascript:window.close();" />
    </center>
    <asp:Label ID="lblCertNo" runat="server" ForeColor="#CCCCCC"></asp:Label>
     </div>
   
    </telerik:RadPane>
        <telerik:RadSplitBar ID="RadSplitBar1" runat="server" CollapseMode="Both" />
        <telerik:RadPane ID="RadPane2" runat="server" BackColor="#cbe1ef" Width="70%" Height="100%">
            
            <script type="text/javascript">
                function changePdf(obj,certno1,certno2,hasPdf,TAXNO,CHECK_ASSET_COUNTRY,ASSET_SHARES_COUNTRY,ASSET_SHARES_TAX) {
                    var myFrame = document.getElementById('PDFFrame');
                    var cert_no = "";
                    var str_query = "";
                    
                    try{
                        var td = obj.parentElement;
                        td.className="selected";
                    }catch(e){
                     
                    }
                    var dd = new Date();
                    if (certno1 != "") {
		cert_no = certno1;
                        str_query = "?certno="+certno1+"&TAXNO="+TAXNO+"&COUNTRY="+CHECK_ASSET_COUNTRY;
                    } else if (certno2 != "") {
		cert_no = certno2;
                        str_query = "?certno="+certno2+"&TAXNO="+ASSET_SHARES_TAX+"&COUNTRY="+ASSET_SHARES_COUNTRY;
                    }
                    if (hasPdf == "Y") {
                        myFrame.src = "http://10.3.0.109/Portals/0/DocumentFiles/<%= Request.QueryString("TaxNo") %>/" + cert_no + ".pdf?v="+dd.getTime();
                    } else {
                        myFrame.src = "/DesktopModules/DFT_EDI_CheckAttachment/ViewCertDetail.aspx"+str_query;
                    }
                }
            </script>

            <div style="height:99%">
                <iframe id="PDFFrame" height="100%" width="100%" frameborder="0"></iframe>
            </div>
        
        </telerik:RadPane>
    </telerik:radsplitter>
        </div>
    </form>
</body>
</html>
