<%@ Control Language="vb" Inherits="NTi.Modules.DFT_EDI_CancelInvoice.ViewDFT_EDI_CancelInvoice"
    AutoEventWireup="false" Explicit="True" Codebehind="ViewDFT_EDI_CancelInvoice.ascx.vb" %>
<%@ Register Assembly="WebShortcutControl" Namespace="BeanSoftware" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="RadAjaxManager1">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="rgInvoiceList" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="rgInvoiceList">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="rgInvoiceList" />
            </UpdatedControls>
        </telerik:AjaxSetting>
    </AjaxSettings>
</telerik:RadAjaxManager>
<telerik:RadCodeBlock ID="RadCodeBlockFirst" runat="server">
    <script type="text/javascript">
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
       
       function OnClientClose(oWnd,args) {
            var arg = args.get_argument();

                if(arg) { 
                
                    var vCompany_Taxno = arg.Company_Taxno;
                    var vCompanyName_Eng = arg.CompanyName_Eng;
                    var vInvoiceNo = arg.InvoiceNo;
                    var vinvh_run_auto = arg.invh_run_auto;

                    var Company_Taxno = $find("<%=txtCompany_Taxno.ClientID %>");  
                    Company_Taxno.set_value(vCompany_Taxno);
                    
                    var CompanyName_Eng = $find("<%=txtCompanyName_Eng.ClientID %>");
                    CompanyName_Eng.set_value(vCompanyName_Eng);

                    var InvoiceNo = $find("<%=txtInvoiceNo.ClientID %>");  
                    InvoiceNo.set_value(vInvoiceNo);

                    var invh_run_auto = $find("<%=txtinvh_run_auto.ClientID %>");  
                    invh_run_auto.set_value(vinvh_run_auto);
                    
                    $find("<%= RadAjaxManager1.ClientID %>").ajaxRequest("Search");
                }
                else{
                    var vCompany_Taxno = "";
                    var vCompanyName_Eng = "";
                    var vInvoiceNo = "";
                    var vinvh_run_auto = "";

                    var Company_Taxno = $find("<%=txtCompany_Taxno.ClientID %>");  
                    Company_Taxno.set_value(vCompany_Taxno);
                    
                    var CompanyName_Eng = $find("<%=txtCompanyName_Eng.ClientID %>");
                    CompanyName_Eng.set_value(vCompanyName_Eng);

                    var InvoiceNo = $find("<%=txtInvoiceNo.ClientID %>");  
                    InvoiceNo.set_value(vInvoiceNo);

                    var invh_run_auto = $find("<%=txtinvh_run_auto.ClientID %>");  
                    invh_run_auto.set_value(vinvh_run_auto);

                    $find("<%= RadAjaxManager1.ClientID %>").ajaxRequest("Search");
                }
        }
        
        function ShowCompany() {
            window.radopen("/DesktopModules/DFT_EDI_CancelInvoice/frmSelectCompany.aspx", "SearchDialog");
            return false; 
        }
           function ShowInv() {
            window.radopen("/DesktopModules/DFT_EDI_CancelInvoice/frmSelectInvh.aspx", "SearchDialog");
            return false; 
        }
        function ShowDeleteForm(invoiceno,year) {
           window.radopen("/DesktopModules/DFT_EDI_CancelInvoice/frmCancelInvoice.aspx?invoiceno=" + invoiceno + "&year=" + year , "DeleteDialog")
           return false;
        }

    </script>
</telerik:RadCodeBlock>
<telerik:RadWindowManager ID="RadWindowManager1" Behavior="Close" runat="server" Skin="Office2007" TabIndex="-1">
    <Windows>
        <telerik:RadWindow ID="SearchDialog" OnClientClose="OnClientClose" runat="server" ReloadOnShow="True" ShowContentDuringLoad="False" Modal="True"
        VisibleStatusbar="False" Width="800px" Height="515px" />
        
        <telerik:RadWindow ID="DeleteDialog" Animation="Slide" runat="server" ReloadOnShow="True" ShowContentDuringLoad="False" Modal="True"
        Width="450px" Height="160px" VisibleStatusbar="False" />

    </Windows>
</telerik:RadWindowManager>
<table width="100%" border="0" cellspacing="3" cellpadding="0">
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
                                                            <font class="groupbox">&#160;&#160;ค้นหา&#160;&#160;</font></td>
                                                        <telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">
                                                            <td style="background-image: url(<%= ResolveUrl("images") %>/CORNER.gif); background-repeat: no-repeat;">
                                                                &#160;&#160;&#160;&#160;</td>
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
                                                &nbsp;</td>
                                            <td>
                                                <table width="70%" border="0" cellspacing="2" cellpadding="0">
                                                    <tr>
                                                        <td style="text-align: right">
                                                            <font class="FormLabel">บริษัท :</font>
                                                            <telerik:RadTextBox ID="txtCompanyName_Eng" runat="server" Font-Names="Tahoma" Font-Size="12pt"
                                                                Width="400px">
                                                            </telerik:RadTextBox>
                                                        </td>
                                                        <td>
                                                            <cc1:ShortcutButton ID="btnSelectCompany" runat="server" Character="F6" Text="เลือกบริษัท [F6]"
                                                                Width="150px" OnClientClick="ShowCompany(); return false;" /></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: right">
                                                            <font class="FormLabel">เลขประจำตัวผู้เสียภาษี :</font>
                                                            <telerik:RadTextBox ID="txtCompany_Taxno" runat="server" Font-Names="Tahoma" Font-Size="12pt"
                                                                Width="300px">
                                                            </telerik:RadTextBox></td>
                                                        <td>
                                                            <cc1:ShortcutButton ID="btnClear" runat="server" Character="ESC" Text="ล้างข้อมูล [ESC]"
                                                                Width="150px" /></td>
                                                    </tr>
                                                  
                                                    <tr>
                                                        <td style="text-align: right">
                                                            <font class="FormLabel">เลขที่ Invoice :</font>
                                                            <telerik:RadTextBox ID="txtInvoiceNo" runat="server" Font-Names="Tahoma" Font-Size="12pt"
                                                                Width="300px">
                                                            </telerik:RadTextBox>
                                                        </td>
                                                        <td>
                                                            <cc1:ShortcutButton ID="btnSearch" runat="server" Character="F9" Text="ค้นหา [F9]"
                                                                Width="150px" /></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: right">
                                                            <font class="FormLabel">เลขที่อ้างอิง :</font>
                                                            <telerik:RadTextBox ID="txtinvh_run_auto" runat="server" Font-Names="Tahoma" Font-Size="12pt"
                                                                Width="300px">
                                                            </telerik:RadTextBox>
                                                        </td>
                                                      <td>
                                                            <cc1:ShortcutButton ID="btnSearchInv" runat="server" Character="F6" Text="ค้นหาเลขที่อ้างอิง"
                                                                Width="150px" OnClientClick="ShowInv(); return false;" /></td>
                                                     
                                                    </tr>

                                                    <tr>
                                                        <td style="text-align: center">
                                                            <asp:Panel ID="Panel1" runat="server" BackColor="Transparent" Font-Names="Tahoma"
                                                                Font-Size="10pt" GroupingText="ระบบ" Width="250px">
                                                                <asp:RadioButtonList ID="rblSearchType" runat="server" Font-Names="Tahoma" Font-Size="10pt"
                                                                    RepeatDirection="Horizontal" Width="100%">
                                                                    <asp:ListItem Selected="True" Value="0">EDI</asp:ListItem>
                                                                    <asp:ListItem Value="1">MANUAL</asp:ListItem>
                                                                </asp:RadioButtonList></asp:Panel>
                                                        </td>
                                                        <td>
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
<table border="0" cellpadding="0" cellspacing="3" width="100%">
    <tr>
        <td>
            <table border="0" cellpadding="1" cellspacing="0" class="m-frm-bdr" width="100%">
                <tr>
                    <td>
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr class="m-frm-hdr">
                                <td>
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                        <tr class="m-frm-hdr">
                                            <td align="left" class="m-frm-hdr" width="88%">
                                                <table border="0" cellpadding="0" cellspacing="0" cols="2">
                                                    <tr>
                                                        <td class="groupboxheader" nowrap="nowrap">
                                                            <font class="groupbox">&nbsp; รายการ Invoice &nbsp;</font></td>
                                                        <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
                                                            <td style='background-image: url(<%= ResolveUrl("images") %>/CORNER.gif); background-repeat: no-repeat;'>
                                                                &nbsp; &nbsp;&nbsp;</td>
                                                        </telerik:RadCodeBlock>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td align="left" class="m-frm-nav" width="12%">
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td class="m-frm" valign="top" width="100%">
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                        <tr>
                                            <td width="15">
                                                &nbsp;</td>
                                            <td>
                                                <table border="0" cellpadding="0" cellspacing="2" width="100%">
                                                    <tr>
                                                        <td>
                                                            <cc1:ShortcutButton ID="btnDelete" runat="server" Character="F7" Text="ยกเลิก [F7]"
                                                                Width="150px" /></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <telerik:RadGrid ID="rgInvoiceList" runat="server" AllowPaging="True" AllowSorting="True"
                                                                CssClass="GRID-STYLE" Font-Names="Tahoma" Font-Size="X-Small" GridLines="None"
                                                                Skin="Office2007" TabIndex="-1">
                                                                <MasterTableView AutoGenerateColumns="False" DataKeyNames="invoice_no,edi_year" NoMasterRecordsText="ไม่พบรายการ Invoice ของบริษัทที่ทำการค้นหา"
                                                                    Width="100%">
                                                                    <Columns>
                                                                        <telerik:GridTemplateColumn UniqueName="TemplateViewColumn" HeaderText="ยกเลิก">
                                                                            <ItemTemplate>
                                                                                <asp:ImageButton ID="btnCancelInvoice" runat="server" CommandName="CancelInvoice" ImageUrl="~/images/cancel.gif"
                                                                                    TabIndex="-1" />
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                                                            <ItemStyle HorizontalAlign="Center" Width="50px" />
                                                                        </telerik:GridTemplateColumn>
                                                                        
                                                                        <telerik:GridBoundColumn DataField="invh_run_auto" HeaderText="เลขที่อ้างอิง" ReadOnly="True"
                                                                            SortExpression="invh_run_auto" UniqueName="invh_run_auto">
                                                                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="invoice_no" HeaderText="เลขที่ Invoice" ReadOnly="True"
                                                                            SortExpression="invoice_no" UniqueName="invoice_no">
                                                                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                        </telerik:GridBoundColumn>
                                                                          <telerik:GridBoundColumn DataField="active_flag" HeaderText="สถานะ" ReadOnly="True" 
                                                                            SortExpression="active_flag" UniqueName="active_flag">
                                                                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="edi_year" HeaderText="ปี" ReadOnly="True"
                                                                            SortExpression="edi_year" UniqueName="edi_year">
                                                                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                        </telerik:GridBoundColumn>
                                                                    </Columns>
                                                                </MasterTableView>
                                                                <HeaderStyle Font-Names="Tahoma" Font-Size="10pt" Font-Underline="False" HorizontalAlign="Center" />
                                                                <ClientSettings EnableRowHoverStyle="True">
                                                                    <Selecting AllowRowSelect="True" />
                                                                </ClientSettings>
                                                            </telerik:RadGrid>
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
