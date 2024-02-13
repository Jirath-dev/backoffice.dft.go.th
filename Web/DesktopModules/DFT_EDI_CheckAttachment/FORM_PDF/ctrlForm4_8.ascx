<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlForm4_8.ascx.vb" Inherits=".ctrlForm4_8" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="RadAjaxManager1">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="grdItemData" />
                <telerik:AjaxUpdatedControl ControlID="txtGrossWeight" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="dropDestRemark">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="dropDestRemark" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="dropDestinationCountry">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="dropDestinationCountry" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="txtDepartureDate">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="txtDepartureDate" />
                <telerik:AjaxUpdatedControl ControlID="txtEdiDate" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="grdItemData">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="grdItemData" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="drpG_UNIT_CODE">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="drpG_UNIT_CODE" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="drpQ_UNIT_CODE1">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="drpQ_UNIT_CODE1" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="drpQ_UNIT_CODE2">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="drpQ_UNIT_CODE2" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="drpQ_UNIT_CODE3">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="drpQ_UNIT_CODE3" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="drpQ_UNIT_CODE4">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="drpQ_UNIT_CODE4" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="drpQ_UNIT_CODE5">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="drpQ_UNIT_CODE5" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="chkShowCheck">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="chkShowCheck" />
                <telerik:AjaxUpdatedControl ControlID="txtThirdCountry" />
                <telerik:AjaxUpdatedControl ControlID="txtPlaceExibition" />
                <telerik:AjaxUpdatedControl ControlID="txtBackCountry" />
            </UpdatedControls>
        </telerik:AjaxSetting>
    </AjaxSettings>
</telerik:RadAjaxManager>
<telerik:RadCodeBlock ID="RadCodeBlockFirst" runat="server">
    <script type="text/javascript">
        function ShowViewForm(invh_run_auto, invd_run_auto, action)
        {
            window.radopen("/DesktopModules/DFT_eCO_EDI/Popup/FormItem4_8.aspx?InvHRunAuto=" + invh_run_auto + "&InvDRunAuto=" + invd_run_auto + "&action=" + action, "ViewDialog");
            return false;
        }

        function ShowEditForm(invh_run_auto, invd_run_auto, action) {
            window.radopen("/DesktopModules/DFT_eCO_EDI/Popup/FormItem4_8.aspx?InvHRunAuto=" + invh_run_auto + "&InvDRunAuto=" + invd_run_auto + "&action=" + action, "EditDialog");
            return false;
        }
        
        function ShowInsertForm(){
            window.radopen("/DesktopModules/DFT_eCO_EDI/Popup/FormItem4_8.aspx?action=new", "InsertDialog");
            return false;
        }
        
        function ShowInsertAttachFile()
        {
            window.radopen("/DesktopModules/DFT_eCO_EDI/Popup/FormItemAttachFile.aspx?action=new", "InsertAttachFile");
            return false;
        }
        
         function ShowDeleteFormFile(InvH_Run_Auto, File_ID, Files_name) {
           window.radopen("/DesktopModules/DFT_eCO_EDI/Popup/FormItemDeleteFile.aspx?InvHRunAuto=" + InvH_Run_Auto + "&FileID=" + File_ID + "&Files_name=" + Files_name, "DeleteAttachFile")
           return false;
       }


       function ShowDeleteForm(invh_run_auto, invd_run_auto) {
           window.radopen("/DesktopModules/DFT_eCO_EDI/Popup/FormItemDeleted.aspx?InvHRunAuto=" + invh_run_auto + "&InvDRunAuto=" + invd_run_auto, "DeleteDialog")
           return false;
       }
        
        var updated_grid = true ;
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
            <% if Request.QueryString("signed") = "true" Then %>
            if(updated_grid==true){
             window.setTimeout('RebindGrid1("<%= grdItemData.ClientID %>")',300);
	         updated_grid = false;
	        }
	        <% end if %>
        }
        
        function refreshDocGrid(arg)
        {
            RebindGrid1("<%= GridUploadFile.ClientID %>");
            window.setTimeout('RebindGrid1("<%= GridUploadFile.ClientID %>");',300);
        }
        
        function RebindGrid1(grid_name)
        {
            try{
                var masterTable = $find(grid_name).get_masterTableView();
                masterTable.rebind();
            }catch(e){ }
        } 
        
        function onFocus(sender, eventArgs){
            $find("<%= txtOB_Address.ClientID %>").focus();
        }

        function GetTaxId(taxid) {
            var str_taxid = "";
            try {
                if (taxid.length >= 13) {
                    str_taxid = taxid.substr(0, 13);
                } else if (taxid.length > 10) {
                    str_taxid = taxid.substr(0, 10);
                }else{
                    str_taxid = taxid;
                }
            } catch (ex) { /**/ }
            return str_taxid;
        }

        function checkCertificate(){
		    if(typeof(Page_ClientValidate) == 'function' && Page_ClientValidate()==true){
			    try{ 
                		    var objSign;
                		    var sdataforsign = 'dft' ; 
                		    var objSign = new ActiveXObject("NTISignLib.Signature");
		        	        var result = objSign.SignString(GetTaxId('<%= Session("ssCompany_TaxNo") %>'),sdataforsign);
				            if(result==""){ 
				                return false;
				            }else{
				                ShowLoading();
				                WebForm_DoPostBackWithOptions(new WebForm_PostBackOptions("<%= btnSaveAndSigned.UniqueID %>", "", true, "", "", false, true)) ;
    			                return true;
				            }
            		    }catch(e){
                		    alert('Error: ไม่สามารถลงลายมือชื่ออิเล็กทรอนิกส์ได้ \nกรุณาตรวจสอบ Certificate ให้พร้อมใช้งานให้เรียบร้อยก่อน');
				            return false;
            		    }
		    }else{
			    return false;
		    }
        }
        
     function ShowLoading(){
	   try{
		 if(typeof(Page_ClientValidate) == 'function' && Page_ClientValidate()==true){
		    try{ 
			var loading = document.getElementById("div_load");
			loading.style.display="block";
		    }catch(e){}
		}
	   }catch(e){}
	}  
       
    </script>
</telerik:RadCodeBlock>
<telerik:RadWindowManager ID="RadWindowManager1" Behavior="Close" runat="server" Skin="Office2007" TabIndex="-1">
    <Windows>
        <telerik:RadWindow ID="ViewDialog" runat="server" Title="แสดงรายการสินค้า" ReloadOnShow="True" ShowContentDuringLoad="False" Modal="True"
        VisibleStatusbar="False" Width="800px" Height="830px" />
        
         <telerik:RadWindow ID="DeleteAttachFile" Animation="Slide" runat="server" Title="ลบรายการเอกสาร" ReloadOnShow="True" ShowContentDuringLoad="False" Modal="True"
        Width="450px" Height="160px" VisibleStatusbar="False" />
        
        <telerik:RadWindow ID="EditDialog" runat="server" Title="แก้ไขรายการสินค้า" ReloadOnShow="True" ShowContentDuringLoad="False" Modal="True"
        VisibleStatusbar="False" Width="800px" Height="830px" />
        
         <telerik:RadWindow ID="InsertAttachFile" runat="server" Title="Upload เอกสาร" ReloadOnShow="True" ShowContentDuringLoad="False" Modal="True"
        VisibleStatusbar="False" Width="800px" Height="345px" />
        
        <telerik:RadWindow ID="InsertDialog" runat="server" Title="เพิ่มรายการสินค้า" ReloadOnShow="True" ShowContentDuringLoad="False" Modal="True"
        VisibleStatusbar="False" Width="800px" Height="830px" />
        
        <telerik:RadWindow ID="DeleteDialog" Animation="Slide" runat="server" Title="ลบรายการสินค้า" ReloadOnShow="True" ShowContentDuringLoad="False" Modal="True"
        Width="450px" Height="160px" VisibleStatusbar="False" />
    </Windows>
</telerik:RadWindowManager>
<table id="TABLE1" border="0" cellpadding="0" cellspacing="5" width="100%">
    <tr align="center">
        <td style="text-align: right">
            <table border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td align="right">
                        <asp:Image ID="Image1" runat="server" ImageUrl="~/images/home.gif" />&nbsp;
                    </td>
                    <td align="right" valign="middle">
                        <asp:Button ID="btnHome" runat="server" CausesValidation="False" Text="กลับเมนูหลัก"
                            Width="150px" UseSubmitBehavior="True" />
                        &nbsp;&nbsp;
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
<table cellspacing="5" cellpadding="0" width="100%" border="0">
    <tr align="center">
        <td>
            <font class="FormHeader">คำขอหนังสือรับรองถิ่นกำเนิดสินค้าฟอร์ม ASEAN-KOREA</font></td>
    </tr>
</table>
<table cellspacing="5" cellpadding="0" width="100%" border="0">
    <tr>
        <td>
            <table class="m-frm-bdr" cellspacing="0" cellpadding="1" width="100%" border="0">
                <tr>
                    <td>
                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                            <tr class="m-frm-hdr">
                                <td>
                                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                        <tr class="m-frm-hdr">
                                            <td class="m-frm-hdr" align="left" width="12%">
                                                <table cellspacing="0" cols="2" cellpadding="0" border="0">
                                                    <tr>
                                                        <td class="groupboxheader" nowrap>
                                                            <font class="groupbox">&nbsp;&nbsp;1. ผู้ขอ&nbsp;&nbsp;</font></td>
                                                        <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
                                                        <td style="background-image: url(<%= ResolveUrl("images") %>/CORNER.gif); background-repeat:no-repeat;">&#160;&#160;&#160;&#160;</td>
                                                    
                                                        </telerik:RadCodeBlock>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="m-frm-nav" align="left" width="88%">
                                                <asp:TextBox ID="txtInvHRunAuto" runat="server" Visible="False"></asp:TextBox></td>
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
                                                            <asp:RequiredFieldValidator ID="rfvRequestPerson" runat="server" class="FormErr"
                                                                ControlToValidate="txtRequestPerson" Display="Static" ErrorMessage="*"></asp:RequiredFieldValidator></td>
                                                        <td>
                                                            <font class="FormLabel">ชื่อ&nbsp;<telerik:RadTextBox ID="txtRequestPerson" runat="server"
                                                                CssClass="FormFld" EnableEmbeddedSkins="False" ReadOnly="true" TabIndex="-1"
                                                                Width="315px">
                                                            </telerik:RadTextBox></font> <font class="FormLabel">&nbsp;
                                                                <asp:RequiredFieldValidator ID="rfvCompanyName" runat="server" class="FormErr" ControlToValidate="txtCompanyName"
                                                                    Display="Static" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                                ในนามของ</font>
                                                            <telerik:RadTextBox ID="txtCompanyName" runat="server" CssClass="FormFld" EnableEmbeddedSkins="False"
                                                                ReadOnly="true" TabIndex="-1" Width="330px">
                                                            </telerik:RadTextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:RequiredFieldValidator ID="rfvCompanyTaxNo" runat="server" class="FormErr" ControlToValidate="txtCompanyTaxNo"
                                                                Display="Static" ErrorMessage="*"></asp:RequiredFieldValidator></td>
                                                        <td>
                                                            <font class="FormLabel">เลขประจำตัวผู้เสียภาษี</font> &nbsp;<telerik:RadTextBox ID="txtCompanyTaxNo"
                                                                runat="server" CssClass="FormFld" EnableEmbeddedSkins="False" ReadOnly="true"
                                                                TabIndex="-1" Width="210px">
                                                            </telerik:RadTextBox>
                                                            <telerik:RadTextBox ID="txtFormNo" runat="server" CssClass="FormFld" EnableEmbeddedSkins="False"
                                                                ReadOnly="true" TabIndex="-1" Text="-" Visible="False" Width="210px">
                                                            </telerik:RadTextBox><font class="FormLabel"></font></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:RequiredFieldValidator ID="rfvCompanyAddress" runat="server" class="FormErr"
                                                                ControlToValidate="txtCompanyAddress" Display="Static" Enabled="True" ErrorMessage="*"></asp:RequiredFieldValidator></td>
                                                        <td>
                                                            <font class="FormLabel">ที่อยู่&nbsp;<telerik:RadTextBox ID="txtCompanyAddress" runat="server"
                                                                CssClass="FormFld" EnableEmbeddedSkins="False" ReadOnly="true" TabIndex="-1"
                                                                Width="727px">
                                                            </telerik:RadTextBox></font>
                                                            <asp:TextBox ID="txtCompanyProvince" runat="server" Visible="False"></asp:TextBox>
                                                            <asp:TextBox ID="txtCompanyCountry" runat="server" Visible="False"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            </td>
                                                        <td>
                                                            <font class="FormLabel">โทรศัพท์</font> &nbsp;<telerik:RadTextBox ID="txtCompanyPhone"
                                                                runat="server" CssClass="FormFld" EnableEmbeddedSkins="False" ReadOnly="true"
                                                                TabIndex="-1" Width="280px">
                                                            </telerik:RadTextBox><font class="FormLabel">&nbsp;&nbsp;
                                                                โทรสาร</font>&nbsp;<telerik:RadTextBox ID="txtCompanyFax" runat="server" CssClass="FormFld"
                                                                    EnableEmbeddedSkins="False" ReadOnly="true" TabIndex="-1" Width="345px">
                                                                </telerik:RadTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                        </td>
                                                        <td class="FormLabel">
                                                            Email
                                                            <telerik:RadTextBox ID="txtCompanyEmail" runat="server" CssClass="FormFld" EnableEmbeddedSkins="False"
                                                                Width="280px" TabIndex="2">
                                                            </telerik:RadTextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:RequiredFieldValidator ID="rfvCardID" runat="server" class="FormErr" ControlToValidate="txtCardID"
                                                                Display="Static" ErrorMessage="*"></asp:RequiredFieldValidator></td>
                                                        <td>
                                                            <font class="FormLabel">
                                                                <table border="0" cellpadding="0" cellspacing="0">
                                                                    <tr>
                                                                        <td class="FormLabel" colspan="2" style="color: #000000">
                                                                            บัตรประจำตัวกรรมการผู้มีอำนาจ/ผู้รับมอบอำนาจ เลขที่ 
                                                                        </td>
                                                                        <td>
                                                                            <telerik:RadTextBox ID="txtCardID" runat="server" CssClass="FormFld" EnableEmbeddedSkins="False"
                                                                                ReadOnly="true" TabIndex="-1" Width="172px">
                                                                            </telerik:RadTextBox></td>
                                                                        <td>
                                                                            &nbsp; &nbsp;<font class="FormLabel"> O/B หรือ C/O&nbsp; </font>
                                                                        </td>
                                                                        <td>
                                                                            <telerik:RadComboBox ID="dropDestRemark" runat="server" Skin="Office2007" Width="114px">
                                                                                <Items>
                                                                                    <telerik:RadComboBoxItem runat="server" Text="- - - - -" />
                                                                                    <telerik:RadComboBoxItem runat="server" Text="O/B" Value="O/B" />
                                                                                    <telerik:RadComboBoxItem runat="server" Text="C/O" Value="C/O" />
                                                                                </Items>
                                                                            </telerik:RadComboBox>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </font>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                        </td>
                                                        <td>
                                                            <font class="FormLabel">ที่อยู่
                                                                <telerik:RadTextBox ID="txtOB_Address" runat="server" CssClass="FormFld" EnableEmbeddedSkins="False"
                                                                    ReadOnly="false" TabIndex="1" Width="726px">
                                                                </telerik:RadTextBox></font></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            </td>
                                                        <td>
                                                            <font class="FormLabel">Email</font>
                                                            <telerik:RadTextBox ID="txtNewEmail_ch01" runat="server" CssClass="FormFld" EnableEmbeddedSkins="False"
                                                                TabIndex="2" Width="280px">
                                                            </telerik:RadTextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                        </td>
                                                        <td>
                                                            <font class="FormLabel">ชื่อผู้รับมอบอำนาจ</font> &nbsp;<telerik:RadTextBox ID="txtAuthorize"
                                                                runat="server" CssClass="FormFld" EnableEmbeddedSkins="False" ReadOnly="True"
                                                                TabIndex="-1" Width="632px">
                                                            </telerik:RadTextBox>&nbsp;<font class="FormLabel"></font></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                        </td>
                                                        <td>
                                                            <asp:Panel ID="PanelSiteCase" runat="server" Width="100%">
                                                                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                                                                    <tr>
                                                                        <td class="FormLabel" style="width: 13%">
                                                                            สาขาที่รับงาน</td>
                                                                        <td class="FormLabel" style="width: 87%">
                                                                            <asp:DropDownList ID="DDLSiteCase" runat="server" Width="300px">
                                                                            </asp:DropDownList></td>
                                                                    </tr>
                                                                </table>
                                                            </asp:Panel>
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
<table cellspacing="5" cellpadding="0" width="100%" border="0">
    <tr>
        <td>
            <table class="m-frm-bdr" cellspacing="0" cellpadding="1" width="100%" border="0">
                <tr>
                    <td>
                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                            <tr class="m-frm-hdr">
                                <td>
                                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                        <tr class="m-frm-hdr">
                                            <td class="m-frm-hdr" align="left" width="28%">
                                                <table cellspacing="0" cols="2" cellpadding="0" border="0">
                                                    <tr>
                                                        <td class="groupboxheader" nowrap>
                                                            <font class="groupbox">&nbsp;&nbsp;2. ผู้ซื้อหรือผู้รับประเทศปลายทาง&nbsp;</font></td>
                                                        <telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">
                                                        <td style="background-image: url(<%= ResolveUrl("images") %>/CORNER.gif); background-repeat:no-repeat;">&#160;&#160;&#160;&#160;</td>
                                                    
                                                        </telerik:RadCodeBlock>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="m-frm-nav" align="left" width="72%">
                                                <font class="FormLabel">( ชื่อ ที่อยู่ ปลายทาง )</font></td>
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
                                                            <asp:RequiredFieldValidator ID="rfvDestinationCompany" runat="server" class="FormErr"
                                                                ControlToValidate="txtDestinationCompany" Display="Static" ErrorMessage="*"></asp:RequiredFieldValidator></td>
                                                        <td>
                                                            <font class="FormLabel">บริษัทผู้ซื้อหรือผู้รับ</font>&nbsp;<telerik:RadTextBox ID="txtDestinationCompany"
                                                                runat="server" CssClass="FormFld" EnableEmbeddedSkins="False" MaxLength="255"
                                                                TabIndex="2" Width="255px">
                                                            </telerik:RadTextBox>
                                                            &nbsp;<font class="FormLabel">
                                                                <asp:RequiredFieldValidator ID="rfvDestinationTaxID" runat="server" class="FormErr"
                                                                    ControlToValidate="txtDestinationTaxID" Display="Static" Enabled="false" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                                เลขประจำตัวผู้เสียภาษี</font>
                                                            <telerik:RadTextBox ID="txtDestinationTaxID" runat="server" CssClass="FormFld" EnableEmbeddedSkins="False"
                                                                MaxLength="30" TabIndex="3" Width="240px">
                                                            </telerik:RadTextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:RequiredFieldValidator ID="rfvDestinationAddress" runat="server" class="FormErr"
                                                                ControlToValidate="txtDestinationAddress" Display="Static" Enabled="false" ErrorMessage="*"></asp:RequiredFieldValidator></td>
                                                        <td>
                                                            <font class="FormLabel">ที่อยู่
                                                                <telerik:RadTextBox ID="txtDestinationAddress" runat="server" CssClass="FormFld"
                                                                    EnableEmbeddedSkins="False" MaxLength="255" TabIndex="4" Width="690px">
                                                                </telerik:RadTextBox></font></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:RequiredFieldValidator ID="rfvDestinationProvince" runat="server" class="FormErr"
                                                                ControlToValidate="txtDestinationProvince" Display="Static" ErrorMessage="*"></asp:RequiredFieldValidator></td>
                                                        <td>
                                                            <font class="FormLabel">เมือง</font> &nbsp;<telerik:RadTextBox ID="txtDestinationProvince"
                                                                runat="server" CssClass="FormFld" EnableEmbeddedSkins="False" MaxLength="50"
                                                                TabIndex="5" Width="305px">
                                                            </telerik:RadTextBox><font class="FormLabel">&nbsp;
                                                                <asp:RequiredFieldValidator ID="rfvDestReceiveCountry" runat="server" class="FormErr"
                                                                    ControlToValidate="txtDestReceiveCountry" Display="Static" ErrorMessage="*"></asp:RequiredFieldValidator>ประเทศ</font>&nbsp;
                                                            <telerik:RadTextBox ID="txtDestReceiveCountry" runat="server" CssClass="FormFld"
                                                                EnableEmbeddedSkins="False" MaxLength="50" TabIndex="6" Width="305px">
                                                            </telerik:RadTextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                        </td>
                                                        <td>
                                                            <table border="0" cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <td>
                                                                        <font class="FormLabel">ประเทศปลายทาง</font>
                                                                    </td>
                                                                    <td>
                                                                        &nbsp;<asp:DropDownList ID="dropDestinationCountry" runat="server" DataSourceID="SqlCountry"
                                                                            DataTextField="DESCRIPTION" DataValueField="CODE" Width="260px">
                                                                        </asp:DropDownList></td>
                                                                    <td>
                                                                        &nbsp; &nbsp;<font class="FormLabel"> O/B หรือ C/O&nbsp;</font></td>
                                                                    <td>
                                                                        <telerik:RadComboBox ID="dropDestRemark1" runat="server" Skin="Office2007" Width="114px">
                                                                            <Items>
                                                                                <telerik:RadComboBoxItem runat="server" Text="- - - - -" />
                                                                                <telerik:RadComboBoxItem runat="server" Text="O/B" Value="O/B" />
                                                                                <telerik:RadComboBoxItem runat="server" Text="C/O" Value="C/O" />
                                                                            </Items>
                                                                        </telerik:RadComboBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:RequiredFieldValidator ID="rfvDestinationPhone" runat="server" class="FormErr"
                                                                ControlToValidate="txtDestinationPhone" Display="Static" Enabled="false" ErrorMessage="*"></asp:RequiredFieldValidator></td>
                                                        <td>
                                                            <font class="FormLabel">โทรศัพท์</font> &nbsp;<telerik:RadTextBox ID="txtDestinationPhone"
                                                                runat="server" CssClass="FormFld" EnableEmbeddedSkins="False" MaxLength="50"
                                                                TabIndex="8" Width="285px">
                                                            </telerik:RadTextBox><font class="FormLabel">&nbsp;
                                                                <asp:RequiredFieldValidator ID="rfvDestinationFax" runat="server" class="FormErr"
                                                                    ControlToValidate="txtDestinationFax" Display="Static" Enabled="false" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                                โทรสาร</font>&nbsp;<telerik:RadTextBox ID="txtDestinationFax" runat="server" CssClass="FormFld"
                                                                    EnableEmbeddedSkins="False" MaxLength="50" TabIndex="9" Width="310px">
                                                                </telerik:RadTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                        </td>
                                                        <td>
                                                            <font class="FormLabel">ที่อยู่ </font>&nbsp;<telerik:RadTextBox ID="txtob_dest_address" runat="server" CssClass="FormFld"
                                                                EnableEmbeddedSkins="False" MaxLength="255" TabIndex="10" Width="690px">
                                                            </telerik:RadTextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                        </td>
                                                        <td>
                                                            <font class="FormLabel">Email</font>
                                                            <telerik:RadTextBox ID="txtDestinationEmail" runat="server" CssClass="FormFld" EnableEmbeddedSkins="False"
                                                                Width="280px" TabIndex="10">
                                                            </telerik:RadTextBox></td>
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
<table cellspacing="5" cellpadding="0" width="100%" border="0">
    <tr>
        <td>
            <table class="m-frm-bdr" cellspacing="0" cellpadding="1" width="100%" border="0">
                <tr>
                    <td>
                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                            <tr class="m-frm-hdr">
                                <td>
                                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                        <tr class="m-frm-hdr">
                                            <td class="m-frm-hdr" align="left">
                                                <table cellspacing="0" cols="2" cellpadding="0" border="0">
                                                    <tr>
                                                        <td class="groupboxheader" nowrap>
                                                            <font class="groupbox">&nbsp;&nbsp;3. ยานพาหนะที่ส่งออก&nbsp;&nbsp;</font></td>
                                                        <telerik:RadCodeBlock ID="RadCodeBlock3" runat="server">
                                                        <td style="background-image: url(<%= ResolveUrl("images") %>/CORNER.gif); background-repeat:no-repeat;">&#160;&#160;&#160;&#160;</td>
                                                    
                                                        </telerik:RadCodeBlock>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="m-frm-nav" align="right">
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
                                                            <asp:RadioButtonList ID="radShipBy" runat="server" class="FormLabel" RepeatColumns="5"
                                                                TabIndex="11" Width="600px">
                                                                <asp:ListItem Selected="True" Value="0">เรือ</asp:ListItem>
                                                                <asp:ListItem Value="1">เครื่องบิน</asp:ListItem>
                                                                <asp:ListItem Value="2">ทางบก</asp:ListItem>
                                                                <asp:ListItem Value="3">ไปรษณีย์</asp:ListItem>
                                                                <asp:ListItem Value="4">นำติดตัว</asp:ListItem>
                                                            </asp:RadioButtonList></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <font class="FormLabel">
                                                                <asp:RequiredFieldValidator ID="rfvtxtTransportBy" runat="server" class="FormErr"
                                                                    ControlToValidate="txtTransportBy" Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator>Means of transport and route (as far as known)</font>
                                                            <telerik:RadTextBox ID="txtTransportBy" runat="server" CssClass="FormFld" EnableEmbeddedSkins="False"
                                                                MaxLength="255" TabIndex="12" Width="472px">
                                                            </telerik:RadTextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <font class="FormLabel"></font>
                                                            <table border="0" cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <td align="right" class="FormLabel">
                                                                        <asp:RequiredFieldValidator ID="rfvtxtDepartureDate" runat="server" class="FormErr"
                                                                            ControlToValidate="txtDepartureDate" Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator>Shipment date</td>
                                                                    <td>
                                                                        <telerik:RadDatePicker ID="txtDepartureDate" runat="server" Skin="Office2007" TabIndex="13" Culture="English (United Kingdom)" AutoPostBack="True">
                                                                            <Calendar Skin="Office2007" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                                                ViewSelectorText="x" DayNameFormat="FirstTwoLetters" FirstDayOfWeek="Sunday" ShowRowHeaders="False">
                                                                            </Calendar>
                                                                            <DatePopupButton HoverImageUrl="" ImageUrl="" TabIndex="13" />
                                                                            <DateInput TabIndex="13" AutoPostBack="True">
                                                                            </DateInput>
                                                                        </telerik:RadDatePicker>
                                                                        &nbsp;</td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <font class="FormLabel">Vessel's name / Aircraft etc.<telerik:RadTextBox ID="txtVaselName"
                                                                runat="server" CssClass="FormFld" EnableEmbeddedSkins="False" MaxLength="255"
                                                                TabIndex="14" Width="472px">
                                                            </telerik:RadTextBox></font></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <font class="FormLabel">
                                                                <asp:RequiredFieldValidator ID="rfvtxtPortDischarge" runat="server" class="FormErr"
                                                                    ControlToValidate="txtPortDischarge" Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator>Port of Discharge</font>&nbsp;
                                                            <telerik:RadTextBox ID="txtPortDischarge" runat="server" CssClass="FormFld" EnableEmbeddedSkins="False"
                                                                MaxLength="255" TabIndex="15" Width="558px">
                                                            </telerik:RadTextBox></td>
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
<table cellspacing="5" cellpadding="0" width="100%" border="0">
    <tr>
        <td>
            <table class="m-frm-bdr" cellspacing="0" cellpadding="1" width="100%" border="0">
                <tr>
                    <td>
                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                            <tr class="m-frm-hdr">
                                <td class="m-frm-hdr" align="left" width="7%">
                                    <table cellspacing="0" cols="2" cellpadding="0" border="0">
                                        <tr>
                                            <td class="groupboxheader" nowrap>
                                                <font class="groupbox">&nbsp;&nbsp;4. รายการสินค้า&nbsp;&nbsp;</font></td>
                                           <telerik:RadCodeBlock ID="RadCodeBlock4" runat="server">
                                                        <td style="background-image: url(<%= ResolveUrl("images") %>/CORNER.gif); background-repeat:no-repeat;">&#160;&#160;&#160;&#160;</td>
                                                    
                                                        </telerik:RadCodeBlock>
                                        </tr>
                                    </table>
                                </td>
                                <td align="left" width="93%">
                                    &nbsp;&nbsp;&nbsp;&nbsp;<asp:Button class="m-btn" ID="btnInsertItem" 
                                        runat="server" name="btnInsertItem"  Text="เพิ่มรายการสินค้า"
                                        Width="150" CausesValidation="false" OnClientClick="return ShowInsertForm();" UseSubmitBehavior="True"></asp:Button>
                                    &nbsp;</td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table>
                            <tr>
                                <td class="FormLabel" style="width: 10px">
                                </td>
                                <td class="FormLabel">
                                    กรณีใช้สกุลเงินอื่น:
                                </td>
                                <td class="FormLabel">
                                    <asp:DropDownList ID="drpI_Currency_Code" runat="server" AutoPostBack="True" DataTextField="DESCRIPTION"
                                        DataValueField="CODE" Width="260px">
                                    </asp:DropDownList>
                                    <asp:TextBox ID="txtHideFOBOther" runat="server" Visible="False"></asp:TextBox></td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="m-frm" valign="top" width="100%">
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    <font class="FormLabel">&nbsp; </font>
                                    <telerik:RadGrid ID="grdItemData" runat="server" AllowPaging="True" AllowSorting="True"
                                        Font-Names="Tahoma" Font-Size="X-Small" ShowFooter="True" GridLines="None" Skin="Office2007" TabIndex="-1" PageSize="40">
                                        <MasterTableView AutoGenerateColumns="False" CellSpacing="-1" ClientDataKeyNames="invh_run_auto,invd_run_auto"
                                            DataKeyNames="invh_run_auto,invd_run_auto" NoMasterRecordsText="ไม่มีรายการสินค้า"
                                            Width="100%">
                                            <Columns>
                                                <telerik:GridTemplateColumn UniqueName="TemplateViewColumn">
                                                    <ItemStyle HorizontalAlign="Center" Width="20px" />
                                                    <ItemTemplate>
                                                        <asp:HyperLink ID="ViewLink" runat="server" ImageUrl="~/images/view.gif" Text="View"></asp:HyperLink>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn UniqueName="TemplateEditColumn">
                                                    <ItemStyle HorizontalAlign="Center" Width="20px" />
                                                    <ItemTemplate>
                                                        <asp:HyperLink ID="EditLink" runat="server" ImageUrl="~/images/edit.gif" Text="Edit"></asp:HyperLink>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn UniqueName="TemplateDeleteColumn">
                                                    <ItemStyle HorizontalAlign="Center" Width="20px" />
                                                    <ItemTemplate>
                                                        <asp:HyperLink ID="DeleteLink" runat="server" ImageUrl="~/images/delete.gif" Text="Delete"></asp:HyperLink>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn HeaderText="ลำดับ" UniqueName="TemplateColumn">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblIndex" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center"/>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridBoundColumn DataField="InvH_Run_Auto" ReadOnly="True" SortExpression="InvH_Run_Auto"
                                                    UniqueName="InvH_Run_Auto" Visible="False">
                                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="InvD_Run_Auto" ReadOnly="True" SortExpression="InvD_Run_Auto"
                                                    UniqueName="InvD_Run_Auto" Visible="False">
                                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="product_name" HeaderText="สินค้า" SortExpression="product_name"
                                                    UniqueName="product_name">
                                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="tariff_code" HeaderText="พิกัดสินค้า" SortExpression="tariff_code"
                                                    UniqueName="tariff_code">
                                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridNumericColumn DataField="net_weight" dataFormatString="{0:#,##0.0000}" HeaderText="ปริมาณ/น้ำหนักสุทธิ (กก.)"
                                                    SortExpression="net_weight" UniqueName="net_weight" Aggregate="Sum">
                                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </telerik:GridNumericColumn>
                                                <telerik:GridNumericColumn DataField="Fob_amt" dataFormatString="{0:#,##0.0000}" HeaderText="มูลค่า US$ (FOB)" SortExpression="Fob_amt"
                                                    UniqueName="Fob_amt_baht" Aggregate="Sum">
                                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </telerik:GridNumericColumn>
                                                <telerik:GridNumericColumn DataField="PriceOtherDetail" dataFormatString="{0:#,##0.0000}" HeaderText="มูลค่าสุกลเงินอื่น (FOB)" SortExpression="PriceOtherDetail"
                                                    UniqueName="PriceOtherDetail" Aggregate="Sum">
                                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </telerik:GridNumericColumn>
                                                <telerik:GridTemplateColumn HeaderText="มูลค่า USD ต่างประเทศ" UniqueName="TemplateColumn"
                                                    DataType="System.Decimal">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGR_USDInvoiceDetail" runat="server" Text='<%#GetDecemalCheck_(Eval("USDInvoiceDetail")) %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                    <FooterTemplate>
                                                        <asp:Label runat="Server" ID="Label2Foot">
                                                        </asp:Label>
                                                    </FooterTemplate>
                                                </telerik:GridTemplateColumn>
                                            </Columns>
                                        </MasterTableView>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ClientSettings>
                                            <Selecting AllowRowSelect="True" />
                                        </ClientSettings>
                                    </telerik:RadGrid></td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    <font class="FormLabel"><font class="FormLabel">
                                        <table>
                                            <tr>
                                                <td class="FormLabel">
                                                    น้ำหนักรวม (Gross Weight)&nbsp;</td>
                                                <td>
                                                    <telerik:RadNumericTextBox ID="txtGrossWeight" runat="server" CssClass="FormFld"
                                                        Culture="(Default)" EnableEmbeddedSkins="False" ForeColor="Blue" MaxLength="20"
                                                        MaxValue="1000000000" MinValue="0" ShowSpinButtons="False" TabIndex="16" Width="180px">
                                                        <NumberFormat AllowRounding="False" DecimalDigits="4" />
                                                        <IncrementSettings InterceptArrowKeys="False" InterceptMouseWheel="False" />
                                                    </telerik:RadNumericTextBox></td>
                                                <td>
                                                    &nbsp;<font class="FormLabel">&nbsp; หน่วย</font>
                                                </td>
                                                <td>
                                                    &nbsp;<asp:DropDownList ID="drpG_UNIT_CODE" runat="server" DataSourceID="Sqlunit"
                                                        DataTextField="DESCRIPTION" DataValueField="CODE" Width="180px">
                                                    </asp:DropDownList></td>
                                                <td class="FormLabel">
                                                    กรณีแสดง Gross Weight แยกตามรายการสินค้า
                                                    <br />
                                                    ไม่ต้องป้อนข้อมูล น้ำหนักรวม (Gross Weight)
                                                </td>
                                            </tr>
                                        </table>
                                    </font></font>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    <table>
                                        <tr>
                                            <td class="FormLabel">
                                                <asp:RequiredFieldValidator ID="rfvtxtQuantity1" runat="server" class="FormErr" ControlToValidate="txtQuantity1"
                                                    Display="Dynamic" ErrorMessage="*" SetFocusOnError="True"></asp:RequiredFieldValidator>ปริมาณ&nbsp;</td>
                                            <td>
                                                <telerik:RadNumericTextBox ID="txtQuantity1" runat="server" CssClass="FormFld" Culture="(Default)"
                                                    EnableEmbeddedSkins="False" ForeColor="Blue" MaxLength="20" MaxValue="1000000000"
                                                    MinValue="0" ShowSpinButtons="False" TabIndex="18" Width="215px">
                                                    <NumberFormat AllowRounding="False" DecimalDigits="4" />
                                                    <IncrementSettings InterceptArrowKeys="False" InterceptMouseWheel="False" />
                                                </telerik:RadNumericTextBox></td>
                                            <td>
                                                &nbsp;<font class="FormLabel">&nbsp; หน่วย</font>
                                            </td>
                                            <td>
                                                &nbsp;<asp:DropDownList ID="drpQ_UNIT_CODE1" runat="server" DataSourceID="Sqlunit"
                                                    DataTextField="DESCRIPTION" DataValueField="CODE" Width="300px">
                                                </asp:DropDownList></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    <table>
                                        <tr>
                                            <td class="FormLabel">
                                                ปริมาณ&nbsp;</td>
                                            <td>
                                                <telerik:RadNumericTextBox ID="txtQuantity2" runat="server" CssClass="FormFld" Culture="(Default)"
                                                    EnableEmbeddedSkins="False" ForeColor="Blue" MaxLength="20" MaxValue="1000000000"
                                                    MinValue="0" ShowSpinButtons="False" TabIndex="20" Width="215px">
                                                    <NumberFormat AllowRounding="False" DecimalDigits="4" />
                                                    <IncrementSettings InterceptArrowKeys="False" InterceptMouseWheel="False" />
                                                </telerik:RadNumericTextBox></td>
                                            <td>
                                                &nbsp;<font class="FormLabel">&nbsp; หน่วย</font>
                                            </td>
                                            <td>
                                                &nbsp;<asp:DropDownList ID="drpQ_UNIT_CODE2" runat="server" DataSourceID="Sqlunit"
                                                    DataTextField="DESCRIPTION" DataValueField="CODE" Width="300px">
                                                </asp:DropDownList></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    <table>
                                        <tr>
                                            <td class="FormLabel">
                                                ปริมาณ&nbsp;</td>
                                            <td>
                                                <telerik:RadNumericTextBox ID="txtQuantity3" runat="server" CssClass="FormFld" Culture="(Default)"
                                                    EnableEmbeddedSkins="False" ForeColor="Blue" MaxLength="20" MaxValue="1000000000"
                                                    MinValue="0" ShowSpinButtons="False" TabIndex="22" Width="215px">
                                                    <NumberFormat AllowRounding="False" DecimalDigits="4" />
                                                    <IncrementSettings InterceptArrowKeys="False" InterceptMouseWheel="False" />
                                                </telerik:RadNumericTextBox></td>
                                            <td>
                                                &nbsp;<font class="FormLabel">&nbsp; หน่วย</font>
                                            </td>
                                            <td>
                                                &nbsp;<asp:DropDownList ID="drpQ_UNIT_CODE3" runat="server" DataSourceID="Sqlunit"
                                                    DataTextField="DESCRIPTION" DataValueField="CODE" Width="300px">
                                                </asp:DropDownList></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    <table>
                                        <tr>
                                            <td class="FormLabel">
                                                ปริมาณ&nbsp;</td>
                                            <td>
                                                <telerik:RadNumericTextBox ID="txtQuantity4" runat="server" CssClass="FormFld" Culture="(Default)"
                                                    EnableEmbeddedSkins="False" ForeColor="Blue" MaxLength="20" MaxValue="1000000000"
                                                    MinValue="0" ShowSpinButtons="False" TabIndex="24" Width="215px">
                                                    <NumberFormat AllowRounding="False" DecimalDigits="4" />
                                                    <IncrementSettings InterceptArrowKeys="False" InterceptMouseWheel="False" />
                                                </telerik:RadNumericTextBox></td>
                                            <td>
                                                &nbsp;<font class="FormLabel">&nbsp; หน่วย</font>
                                            </td>
                                            <td>
                                                &nbsp;<asp:DropDownList ID="drpQ_UNIT_CODE4" runat="server" DataSourceID="Sqlunit"
                                                    DataTextField="DESCRIPTION" DataValueField="CODE" Width="300px">
                                                </asp:DropDownList></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    <table>
                                        <tr>
                                            <td class="FormLabel">
                                                ปริมาณ&nbsp;</td>
                                            <td>
                                                <telerik:RadNumericTextBox ID="txtQuantity5" runat="server" CssClass="FormFld" Culture="(Default)"
                                                    EnableEmbeddedSkins="False" ForeColor="Blue" MaxLength="20" MaxValue="1000000000"
                                                    MinValue="0" ShowSpinButtons="False" TabIndex="26" Width="215px">
                                                    <NumberFormat AllowRounding="False" DecimalDigits="4" />
                                                    <IncrementSettings InterceptArrowKeys="False" InterceptMouseWheel="False" />
                                                </telerik:RadNumericTextBox></td>
                                            <td>
                                                &nbsp;<font class="FormLabel">&nbsp; หน่วย</font>
                                            </td>
                                            <td>
                                                &nbsp;<asp:DropDownList ID="drpQ_UNIT_CODE5" runat="server" DataSourceID="Sqlunit"
                                                    DataTextField="DESCRIPTION" DataValueField="CODE" Width="300px">
                                                </asp:DropDownList></td>
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
<table cellspacing="5" cellpadding="0" width="100%" border="0">
    <tr>
        <td>
            <table class="m-frm-bdr" cellspacing="0" cellpadding="1" width="100%" border="0">
                <tr>
                    <td>
                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                            <tr class="m-frm-hdr">
                                <td>
                                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                        <tr class="m-frm-hdr">
                                            <td class="m-frm-hdr" align="left">
                                                <table cellspacing="0" cols="2" cellpadding="0" border="0">
                                                    <tr>
                                                        <td class="groupboxheader" nowrap>
                                                            <font class="groupbox">&nbsp;&nbsp;5. Invoice ต่างประเทศ/ไทย &nbsp;&nbsp;</font></td>
                                                        <telerik:RadCodeBlock ID="RadCodeBlock5" runat="server">
                                                        <td style="background-image: url(<%= ResolveUrl("images") %>/CORNER.gif); background-repeat:no-repeat;">&#160;&#160;&#160;&#160;</td>
                                                    
                                                        </telerik:RadCodeBlock>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="m-frm-nav" align="right">
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
                                                        </td>
                                                        <td>
                                                            <table border="0" cellpadding="0" cellspacing="2" width="700">
                                                                <tr>
                                                                    <td align="left" style="height: 70px" valign="top" width="400">
                                                                        <font class="FormLabel"><strong>Invoice ต่างประเทศ(ถ้ามี) &nbsp;</strong>
                                                                            <br />
                                                                            ระบุไม่เกิน 13 ตัวอักษรต่อหนึ่งบรรทัด&nbsp;<br />
                                                                            หมายเลข Invoice &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; เช่น WR0457<br />
                                                                            วันที่ Invoice (DD/MM/YYYY) &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; เช่น 16/06/2008</font></td>
                                                                    <td align="left" style="height: 70px" valign="top">
                                                                        <asp:TextBox ID="txtNumInvoice" runat="server" Height="60px" TextMode="MultiLine"
                                                                            Width="299px"></asp:TextBox></td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                        </td>
                                                        <td>
                                                            <font class="FormLabel"><strong>Invoice ไทย</strong></font></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:RequiredFieldValidator ID="rfvInvoiceNo1" runat="server" class="FormErr" ControlToValidate="txtInvoiceNo1"
                                                                Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator></td>
                                                        <td>
                                                            <table border="0" cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <td class="FormLabel">
                                                                        <font class="FormLabel">ใบกำกับสินค้า &nbsp; เลขที่</font>&nbsp;<telerik:RadTextBox
                                                                            ID="txtInvoiceNo1" runat="server" EnableEmbeddedSkins="False"
                                                                            MaxLength="35" TabIndex="29" Width="240px" TextMode="MultiLine">
                                                                        </telerik:RadTextBox>ระบุไม่เกิน 13 ตัวอักษรต่อหนึ่งบรรทัด
                                                                        <font class="FormLabel">
                                                                            <asp:RequiredFieldValidator ID="rfvInvoiceDate1" runat="server" class="FormErr" ControlToValidate="txtInvoiceDate1"
                                                                                Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                                            &nbsp; &nbsp;ลงวันที่</font>
                                                                    </td>
                                                                    <td valign="bottom">
                                                                        &nbsp;<telerik:RadDatePicker ID="txtInvoiceDate1" runat="server" Skin="Office2007"
                                                                            TabIndex="30" Culture="English (United Kingdom)">
                                                                            <Calendar Skin="Office2007" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                                                ViewSelectorText="x" DayNameFormat="FirstTwoLetters" FirstDayOfWeek="Sunday" ShowRowHeaders="False">
                                                                            </Calendar>
                                                                            <DatePopupButton HoverImageUrl="" ImageUrl="" TabIndex="30" />
                                                                            <DateInput TabIndex="30">
                                                                            </DateInput>
                                                                        </telerik:RadDatePicker>
                                                                        &nbsp;</td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                        </td>
                                                        <td>
                                                            <table border="0" cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <td class="FormLabel">
                                                                        <font class="FormLabel">ใบกำกับสินค้า &nbsp; เลขที่</font>&nbsp;<telerik:RadTextBox
                                                                            ID="txtInvoiceNo2" runat="server" EnableEmbeddedSkins="False"
                                                                            MaxLength="35" TabIndex="31" Width="240px" TextMode="MultiLine">
                                                                        </telerik:RadTextBox>ระบุไม่เกิน 13 ตัวอักษรต่อหนึ่งบรรทัด<font class="FormLabel">&nbsp; &nbsp;&nbsp; ลงวันที่</font>
                                                                    </td>
                                                                    <td valign="bottom">
                                                                        &nbsp;<telerik:RadDatePicker ID="txtInvoiceDate2" runat="server" Skin="Office2007"
                                                                            TabIndex="32" Culture="English (United Kingdom)">
                                                                            <Calendar Skin="Office2007" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                                                ViewSelectorText="x" DayNameFormat="FirstTwoLetters" FirstDayOfWeek="Sunday" ShowRowHeaders="False">
                                                                            </Calendar>
                                                                            <DatePopupButton HoverImageUrl="" ImageUrl="" TabIndex="32" />
                                                                            <DateInput TabIndex="32">
                                                                            </DateInput>
                                                                        </telerik:RadDatePicker>
                                                                        &nbsp;</td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                        </td>
                                                        <td>
                                                            <table border="0" cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <td class="FormLabel">
                                                                        <font class="FormLabel">ใบกำกับสินค้า &nbsp; เลขที่</font>&nbsp;<telerik:RadTextBox
                                                                            ID="txtInvoiceNo3" runat="server" EnableEmbeddedSkins="False"
                                                                            MaxLength="35" TabIndex="33" Width="240px" TextMode="MultiLine">
                                                                        </telerik:RadTextBox>ระบุไม่เกิน 13 ตัวอักษรต่อหนึ่งบรรทัด<font class="FormLabel">&nbsp; &nbsp;&nbsp; ลงวันที่</font>
                                                                    </td>
                                                                    <td valign="bottom">
                                                                        &nbsp;<telerik:RadDatePicker ID="txtInvoiceDate3" runat="server" Skin="Office2007"
                                                                            TabIndex="34" Culture="English (United Kingdom)">
                                                                            <Calendar Skin="Office2007" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                                                ViewSelectorText="x" DayNameFormat="FirstTwoLetters" FirstDayOfWeek="Sunday" ShowRowHeaders="False">
                                                                            </Calendar>
                                                                            <DatePopupButton HoverImageUrl="" ImageUrl="" TabIndex="34" />
                                                                            <DateInput TabIndex="34">
                                                                            </DateInput>
                                                                        </telerik:RadDatePicker>
                                                                        &nbsp;</td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                        </td>
                                                        <td>
                                                            <table border="0" cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <td class="FormLabel">
                                                                        <font class="FormLabel">ใบกำกับสินค้า &nbsp; เลขที่</font>&nbsp;<telerik:RadTextBox
                                                                            ID="txtInvoiceNo4" runat="server" EnableEmbeddedSkins="False"
                                                                            MaxLength="35" TabIndex="35" Width="240px" TextMode="MultiLine">
                                                                        </telerik:RadTextBox>ระบุไม่เกิน 13 ตัวอักษรต่อหนึ่งบรรทัด<font class="FormLabel">&nbsp; &nbsp;&nbsp; ลงวันที่</font>
                                                                    </td>
                                                                    <td valign="bottom">
                                                                        &nbsp;<telerik:RadDatePicker ID="txtInvoiceDate4" runat="server" Skin="Office2007"
                                                                            TabIndex="36" Culture="English (United Kingdom)">
                                                                            <Calendar Skin="Office2007" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                                                ViewSelectorText="x" DayNameFormat="FirstTwoLetters" FirstDayOfWeek="Sunday" ShowRowHeaders="False">
                                                                            </Calendar>
                                                                            <DatePopupButton HoverImageUrl="" ImageUrl="" TabIndex="36" />
                                                                            <DateInput TabIndex="36">
                                                                            </DateInput>
                                                                        </telerik:RadDatePicker>
                                                                        &nbsp;</td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                        </td>
                                                        <td>
                                                            <table border="0" cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <td class="FormLabel">
                                                                        <font class="FormLabel">ใบกำกับสินค้า &nbsp; เลขที่</font>&nbsp;<telerik:RadTextBox
                                                                            ID="txtInvoiceNo5" runat="server" EnableEmbeddedSkins="False"
                                                                            MaxLength="35" TabIndex="37" Width="240px" TextMode="MultiLine">
                                                                        </telerik:RadTextBox>ระบุไม่เกิน 13 ตัวอักษรต่อหนึ่งบรรทัด<font class="FormLabel">&nbsp; &nbsp;&nbsp; ลงวันที่</font>
                                                                    </td>
                                                                    <td valign="bottom">
                                                                        &nbsp;<telerik:RadDatePicker ID="txtInvoiceDate5" runat="server" Skin="Office2007"
                                                                            TabIndex="38" Culture="English (United Kingdom)">
                                                                            <Calendar Skin="Office2007" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                                                ViewSelectorText="x" DayNameFormat="FirstTwoLetters" FirstDayOfWeek="Sunday" ShowRowHeaders="False">
                                                                            </Calendar>
                                                                            <DatePopupButton HoverImageUrl="" ImageUrl="" TabIndex="38" />
                                                                            <DateInput TabIndex="38">
                                                                            </DateInput>
                                                                        </telerik:RadDatePicker>
                                                                        &nbsp;</td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td bordercolor="#0">
                                                        </td>
                                                        <td bordercolor="#0">
                                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                <tr>
                                                                    <td align="left" width="105">
                                                                        <font class="FormLabel">ใบตราส่งสินค้า</font></td>
                                                                    <td align="left" width="373">
                                                                        <asp:RadioButtonList ID="radBillType" runat="server" class="FormLabel" RepeatColumns="4"
                                                                            TabIndex="39" Width="400px">
                                                                            <asp:ListItem Selected="True" Value="0">B/L</asp:ListItem>
                                                                            <asp:ListItem Value="1">AWB</asp:ListItem>
                                                                            <asp:ListItem Value="2">ใบรับไปรษณีย์</asp:ListItem>
                                                                            <asp:ListItem Value="3">อื่นๆ</asp:ListItem>
                                                                        </asp:RadioButtonList></td>
                                                                    <td align="left" width="239">
                                                                        <asp:TextBox ID="txtBillTypeOther" runat="server" class="FormFld" MaxLength="20"
                                                                            Width="210"></asp:TextBox></td>
                                                                </tr>
                                                                <tr>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:RequiredFieldValidator ID="rfvBlNo" runat="server" class="FormErr" ControlToValidate="txtBlNo"
                                                                Display="Static" ErrorMessage="*"></asp:RequiredFieldValidator></td>
                                                        <td>
                                                            <table border="0" cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <td>
                                                                        <font class="FormLabel">เลขที่</font>&nbsp;
                                                                        <telerik:RadTextBox ID="txtBlNo" runat="server" CssClass="FormFld" EnableEmbeddedSkins="False"
                                                                            MaxLength="35" TabIndex="40" Width="285px">
                                                                        </telerik:RadTextBox>
                                                                        <font class="FormLabel">&nbsp;
                                                                            <asp:RequiredFieldValidator ID="rfvSailingDate" runat="server" class="FormErr" ControlToValidate="txtSailingDate"
                                                                                Display="Static" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                                            วันที่</font>
                                                                    </td>
                                                                    <td>
                                                                        &nbsp;<telerik:RadDatePicker ID="txtSailingDate" runat="server" Skin="Office2007"
                                                                            TabIndex="41" Culture="English (United Kingdom)">
                                                                            <Calendar Skin="Office2007" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                                                ViewSelectorText="x" DayNameFormat="FirstTwoLetters" FirstDayOfWeek="Sunday" ShowRowHeaders="False">
                                                                            </Calendar>
                                                                            <DatePopupButton HoverImageUrl="" ImageUrl="" TabIndex="41" />
                                                                            <DateInput TabIndex="41">
                                                                            </DateInput>
                                                                        </telerik:RadDatePicker>
                                                                        &nbsp;</td>
                                                                    <td>
                                                                        &nbsp;<font class="FormLabel">&nbsp;
                                                                            วันที่ส่งออก</font>
                                                                    </td>
                                                                    <td>
                                                                        &nbsp;<telerik:RadDatePicker ID="txtEdiDate" runat="server" Skin="Office2007" TabIndex="42" Culture="English (United Kingdom)" Enabled="False">
                                                                            <Calendar Skin="Office2007" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                                                ViewSelectorText="x" DayNameFormat="FirstTwoLetters" FirstDayOfWeek="Sunday" ShowRowHeaders="False">
                                                                            </Calendar>
                                                                            <DatePopupButton HoverImageUrl="" ImageUrl="" TabIndex="42" />
                                                                            <DateInput TabIndex="42">
                                                                            </DateInput>
                                                                        </telerik:RadDatePicker>
                                                                        &nbsp;</td>
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
    </tr>
</table>
<table cellspacing="5" cellpadding="0" width="100%" border="0">
    <tr>
        <td>
            <table class="m-frm-bdr" cellspacing="0" cellpadding="1" width="100%" border="0">
                <tr>
                    <td>
                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                            <tr class="m-frm-hdr">
                                <td>
                                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                        <tr class="m-frm-hdr">
                                            <td class="m-frm-hdr" align="left" width="38%">
                                                <table cellspacing="0" cols="2" cellpadding="0" border="0">
                                                    <tr>
                                                        <td class="groupboxheader" nowrap>
                                                            <font class="groupbox">&nbsp;&nbsp;6. &nbsp;&nbsp;</font></td>
                                                        <telerik:RadCodeBlock ID="RadCodeBlock6" runat="server">
                                                        <td style="background-image: url(<%= ResolveUrl("images") %>/CORNER.gif); background-repeat:no-repeat;">&#160;&#160;&#160;&#160;</td>
                                                    
                                                        </telerik:RadCodeBlock>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td align="left" width="62%">
                                                &nbsp;</td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td class="m-frm" valign="top" width="100%">
                                    <table border="0" cellpadding="0" cellspacing="2" width="100%">
                                        <tr>
                                            <td width="700">
                                                <font class="FormLabel">&nbsp;เอกสารที่แนบประกอบการพิจารณา</font>&nbsp;
                                                <telerik:RadTextBox ID="txtAttachFile" runat="server" CssClass="FormFld" EnableEmbeddedSkins="False"
                                                    MaxLength="100" TabIndex="43" Width="450px">
                                                </telerik:RadTextBox></td>
                                            <td>
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td>
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table border="0" cellpadding="0" cellspacing="2">
                                                    <tr>
                                                        <td align="left" valign="top" width="160">
                                                            <font class="FormLabel">&nbsp;เลือกเพื่อกรอกลงฟอร์ม ช่อง 13</font>
                                                        </td>
                                                        <td align="left" style="width: 241px"><font class="FormLabel">
                                                            <asp:CheckBoxList ID="chkShowCheck" runat="server" AutoPostBack="True">
                                                                <asp:ListItem Value="0">Third-Country Invoicing   <br> (ระบุชื่อ+ประเทศผู้ออก Invoice)</asp:ListItem>
                                                                <asp:ListItem Value="1">Exhibition <br> (ระบุชื่องานและสถานที่จัดงาน)</asp:ListItem>
                                                                <asp:ListItem Value="2">Back-to-Back CO           <br>   (ระบุประเทศต้นทาง)</asp:ListItem>
                                                            </asp:CheckBoxList></font></td>
                                                        <td align="left" valign="top" width="320">
                                                            <font class="FormLabel">
                                                                <br />
                                                                <telerik:RadTextBox ID="txtThirdCountry" runat="server" CssClass="FormFld" EnableEmbeddedSkins="False"
                                                                    MaxLength="250" TabIndex="44" Width="400px">
                                                                </telerik:RadTextBox><br />
                                                                <telerik:RadTextBox ID="txtPlaceExibition" runat="server" CssClass="FormFld" EnableEmbeddedSkins="False"
                                                                    MaxLength="250" TabIndex="46" Width="400px">
                                                                </telerik:RadTextBox><br />
                                                                <telerik:RadTextBox ID="txtBackCountry" runat="server" CssClass="FormFld" EnableEmbeddedSkins="False"
                                                                    MaxLength="150" TabIndex="45" Width="400px">
                                                                </telerik:RadTextBox></font></td>
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
<table cellspacing="5" cellpadding="0" width="100%" border="0">
    <tr>
        <td>
            <table class="m-frm-bdr" cellspacing="0" cellpadding="1" width="100%" border="0">
                <tr>
                    <td>
                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                            <tr class="m-frm-hdr">
                                <td>
                                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                        <tr class="m-frm-hdr">
                                            <td class="m-frm-hdr" align="left">
                                                <table cellspacing="0" cols="2" cellpadding="0" border="0">
                                                    <tr>
                                                        <td class="groupboxheader" nowrap>
                                                            <font class="groupbox">&nbsp;&nbsp;7. &nbsp;&nbsp;</font></td>
                                                       <telerik:RadCodeBlock ID="RadCodeBlock7" runat="server">
                                                        <td style="background-image: url(<%= ResolveUrl("images") %>/CORNER.gif); background-repeat:no-repeat;">&#160;&#160;&#160;&#160;</td>
                                                    
                                                        </telerik:RadCodeBlock>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="m-frm-nav" align="right">
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td class="m-frm" valign="top" width="100%">
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                        <tr>
                                            <td width="25">
                                                &nbsp;</td>
                                            <td>
                                                <table border="0" cellpadding="0" cellspacing="2" width="100%">
                                                    <tr>
                                                        <td>
                                                            <font class="FormLabel">ข้าพเจ้าขอให้คำรับรองว่า: - </font>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <font class="FormLabel">&nbsp;&nbsp;&nbsp;&nbsp;(1) สินค้าตามรายการดังกล่าวข้างต้นเป็นสินค้าที่ผลิตและมีถิ่นกำเนิดในประเทศไทย
                                                                โดยผลิต/ซื้อจาก ( โรงงาน/บริษัท/ห้าง/ร้าน )</font></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <font class="FormLabel">&nbsp; &nbsp;&nbsp;
                                                                <asp:RequiredFieldValidator ID="rfvFactory" runat="server" class="FormErr" ControlToValidate="txtFactory"
                                                                    Display="Static" ErrorMessage="*"></asp:RequiredFieldValidator>&nbsp;<telerik:RadTextBox
                                                                        ID="txtFactory" runat="server" CssClass="FormFld" EnableEmbeddedSkins="False"
                                                                        TabIndex="47" Width="300px">
                                                                    </telerik:RadTextBox>&nbsp;
                                                                <asp:RequiredFieldValidator ID="rfvFactoryTaxID" runat="server" class="FormErr" ControlToValidate="txtFactoryTaxID"
                                                                    Display="Static" Enabled="false" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                                เลขประจำตัวผู้เสียภาษี</font>
                                                            <telerik:RadTextBox ID="txtFactoryTaxID" runat="server" CssClass="FormFld" EnableEmbeddedSkins="False"
                                                                MaxLength="30" TabIndex="48" Width="225px">
                                                            </telerik:RadTextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <font class="FormLabel">&nbsp; &nbsp;
                                                                <asp:RequiredFieldValidator ID="rfvFactoryAddress" runat="server" class="FormErr"
                                                                    ControlToValidate="txtFactoryAddress" Display="Static" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                                ตั้งอยู่ที่</font>
                                                            <telerik:RadTextBox ID="txtFactoryAddress" runat="server" CssClass="FormFld" EnableEmbeddedSkins="False"
                                                                MaxLength="255" TabIndex="49" Width="640px">
                                                            </telerik:RadTextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <font class="FormLabel">&nbsp; &nbsp;
                                                                <asp:RequiredFieldValidator ID="rfvFactoryProvince" runat="server" class="FormErr"
                                                                    ControlToValidate="txtFactoryProvince" Display="Static" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                                จังหวัด</font>&nbsp;<telerik:RadTextBox ID="txtFactoryProvince" runat="server" CssClass="FormFld"
                                                                    EnableEmbeddedSkins="False" MaxLength="50" TabIndex="50" Width="265px">
                                                                </telerik:RadTextBox><font class="FormLabel">&nbsp;
                                                                    <asp:RequiredFieldValidator ID="rfvFactoryCountry" runat="server" class="FormErr"
                                                                        ControlToValidate="txtFactoryCountry" Display="Static" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                                    ประเทศ</font>
                                                            <telerik:RadTextBox ID="txtFactoryCountry" runat="server" CssClass="FormFld" EnableEmbeddedSkins="False"
                                                                MaxLength="50" TabIndex="51" Width="303px">
                                                            </telerik:RadTextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <font class="FormLabel">&nbsp; &nbsp;&nbsp;&nbsp;
                                                                โทรศัพท์</font>&nbsp;<telerik:RadTextBox ID="txtFactoryPhone" runat="server" CssClass="FormFld"
                                                                    EnableEmbeddedSkins="False" MaxLength="50" TabIndex="52" Width="255px">
                                                                </telerik:RadTextBox><font class="FormLabel">&nbsp;&nbsp;
                                                                    โทรสาร</font>&nbsp;<telerik:RadTextBox ID="txtFactoryFax" runat="server" CssClass="FormFld"
                                                                        EnableEmbeddedSkins="False" MaxLength="50" TabIndex="53" Width="305px">
                                                                    </telerik:RadTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <font class="FormLabel">&nbsp;&nbsp;&nbsp;&nbsp;(2) การขอหนังสือรับรองฉบับนี้หากเป็นการขอซ้ำข้าพเจ้ายินดีให้กรมการค้าต่างประเทศระงับหนังสือรับรองฉบับที่ออกก่อนหน้านี้และฉบับนี้
                                                            </font>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <font class="FormLabel">&nbsp;&nbsp;&nbsp;&nbsp;(3) ข้าพเจ้ายินดีให้ความร่วมมือในการตรวจสอบต้นทุนและขั้นตอนการผลิต
                                                                โดยหากพิสูจน์ได้ว่าสินค้าไม่มีการผลิตในประเทศไทย หรือ ผลิตไม่ถูกต้องตามกฎหมายว่าด้วยถิ่นกำเนิดสินค้า
                                                                ข้าพเจ้ายินยอมให้กรมการค้าต่างประเทศยกเลิก ระงับหรือเพิกถอนหนังสือรับรองฉบับนี้
                                                                และให้ถอนชื่อออกจากทะเบียนผู้ขอหนังสือรับรองฯ โดยทันที</font></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <font class="FormLabel">&nbsp;&nbsp;&nbsp;&nbsp;(4) เอกสาร หลักฐานและรายละเอียดดังกล่าวข้างต้นเป็นความจริงทุกประการ
                                                                หากคำรับรองตาม (1)-(3) เป็นความเท็จ ข้าพเจ้ายินยอมให้กรมการค้าต่างประเทศดำเนินคดีตามกฎหมาย</font></td>
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
<telerik:RadAjaxPanel  ID="panelAttach" runat="server" Width="100%">
<table border="0" cellpadding="0" cellspacing="5" width="100%">
    <tr>
        <td>
            <table border="0" cellpadding="1" cellspacing="0" class="m-frm-bdr" width="100%">
                <tr>
                    <td>
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr class="m-frm-hdr">
                                <td>
                                    <table border="0" cellpadding="0" cellspacing="0">
                                        <tr class="m-frm-hdr">
                                            <td align="left" class="m-frm-hdr">
                                                <table border="0" cellpadding="0" cellspacing="0" cols="2">
                                                    <tr>
                                                        <td class="groupboxheader" nowrap="nowrap">
                                                            <font class="groupbox">&nbsp; 8.&nbsp; เอกสารที่แนบ &nbsp;</font></td>
                                                        <telerik:RadCodeBlock ID="RadCodeBlock8" runat="server">
                                                            <td style='background-image: url(<%= ResolveUrl("images") %>/CORNER.gif); background-repeat: no-repeat;'>
                                                                &nbsp; &nbsp;&nbsp;</td>
                                                        </telerik:RadCodeBlock>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td align="left" width="80%">
                                                &nbsp;
                                                <asp:Button ID="btnAttach" runat="server" CausesValidation="False" class="m-btn"
                                                    OnClientClick="return ShowInsertAttachFile();" TabIndex="-1" Text="เอกสารที่แนบ"
                                                    UseSubmitBehavior="True" />&nbsp;
                                                <asp:Label ID="lblErrMsgAttach" runat="server" CssClass="m-err"></asp:Label></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td class="m-frm" valign="top" width="100%">
                                    <telerik:RadGrid ID="GridUploadFile" runat="server" AllowPaging="True" AllowSorting="True"
                                        Font-Names="Tahoma" Font-Size="X-Small" GridLines="None" Skin="Office2007" TabIndex="-1">
                                        <MasterTableView AutoGenerateColumns="False" CellSpacing="-1" ClientDataKeyNames="FileID"
                                            DataKeyNames="InvH_Run_Auto, FileID, FilesName" NoMasterRecordsText="ไม่มีรายการเอกสาร" Width="100%">
                                            <Columns>
                                                <telerik:GridTemplateColumn UniqueName="TemplateViewColumn">
                                                    <ItemStyle HorizontalAlign="Center" Width="20px" />
                                                    <ItemTemplate>
                                                        <asp:HyperLink ID="ViewFile" runat="server" ImageUrl="~/images/view.gif" Text="View"></asp:HyperLink>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn UniqueName="TemplateDeleteColumn">
                                                    <ItemStyle HorizontalAlign="Center" Width="20px" />
                                                    <ItemTemplate>
                                                        <asp:HyperLink ID="DeleteFile" runat="server" ImageUrl="~/images/delete.gif" Text="Delete"></asp:HyperLink>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridBoundColumn DataField="FileID" ReadOnly="True" SortExpression="FileID"
                                                    UniqueName="FileID" Visible="False">
                                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="InvH_Run_Auto" ReadOnly="True"
                                                    SortExpression="InvH_Run_Auto" UniqueName="InvH_Run_Auto" Visible="False">
                                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="FilesName" HeaderText="Hide"
                                                    SortExpression="FilesName" UniqueName="FilesName" Visible="False">
                                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                                </telerik:GridBoundColumn>
                                                 <telerik:GridBoundColumn DataField="AttachType" HeaderText="ประเภท" SortExpression="AttachType"
                                                    UniqueName="AttachType">
                                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="MySubStr" HeaderText="ชื่อไฟล์เอกสาร"
                                                    SortExpression="MySubStr" UniqueName="MySubStr">
                                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Descript" HeaderText="คำอธิบาย"
                                                    SortExpression="Descript" UniqueName="Descript">
                                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                                </telerik:GridBoundColumn>
                                            </Columns>
                                        </MasterTableView>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ClientSettings>
                                            <Selecting AllowRowSelect="True" />
                                        </ClientSettings>
                                    </telerik:RadGrid></td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
</telerik:RadAjaxPanel>
<table cellSpacing="5" cellPadding="0" width="100%" border="0">
    <tr>
        <td align="center">
            <asp:Label ID="lbl_ErrMSG" runat="server"></asp:Label></td>
    </tr>
				<tr>
					<td align="center"><asp:button OnClientClick="javascript:ShowLoading();"  id="btnSave" onclick="btnSave_Click" runat="server" Width="150" name="btnSave"
							Text="บันทึกฟอร์มคำร้อง" UseSubmitBehavior="True"></asp:button>
							
							<!-- DS2 edited -->
            <asp:Button ID="btnSaveAndSigned" runat="server" Text="บันทึกฟอร์มคำร้อง และลงลายมือชื่ออิเล็กทรอนิกส์"
                UseSubmitBehavior="True" Visible="False" OnClientClick="javascript:return checkCertificate();" /> <asp:Button
                ID="btnSignData" runat="server" CausesValidation="False" UseSubmitBehavior="True" Width="0px" style="width:0px;display:none;" />
                <asp:HiddenField ID="DataText" runat="server" /><asp:HiddenField ID="DataSigned" runat="server" />
                <asp:HiddenField ID="Signed" runat="server" Value="false" />
                <div style="display:none;font-size:12px;color:#333333;margin:10px;" id="div_load"><img alt="" src="/images/loading3.gif" align="absmiddle"/> กำลังบันทึกข้อมูล...</div>
            <!-- end DS2 -->
							
							</td>
				</tr>
</table>
<table cellspacing="5" width="100%">
    <tr>
        <td>
            <p style="text-align: right">
                <span><span class="SubHead" style="color: #6495ed">Screen ID:</span></span> E28</p>
        </td>
    </tr>
</table>
<asp:SqlDataSource ID="SqlCountry" runat="server" ConnectionString="<%$ ConnectionStrings:OriginConnection %>"
    SelectCommand="sp_common_get_countryByFormTypeForm4_8_NewDS" SelectCommandType="StoredProcedure">
    <SelectParameters>
        <asp:Parameter DefaultValue="FORM4_8" Name="FORM_TYPE" Type="String" />
    </SelectParameters>
    </asp:SqlDataSource>
<asp:SqlDataSource ID="Sqlunit" runat="server" ConnectionString="<%$ ConnectionStrings:OriginConnection %>"
SelectCommand="sp_common_get_unit_NewDS" SelectCommandType="StoredProcedure">
</asp:SqlDataSource>
<telerik:RadTextBox ID="RadTextBox1" runat="server" BackColor="TRANSPARENT" BorderStyle="None"
    ClientEvents-OnFocus="onFocus" EnableEmbeddedSkins="false" TabIndex="54" width="1px">
<ClientEvents OnFocus="onFocus"></ClientEvents>
</telerik:RadTextBox>
<br />
<br />
                                                                        <asp:TextBox ID="txtInvoiceBoard" runat="server" class="FormFld2" MaxLength="250"
                                                                            Rows="5" TabIndex="28" TextMode="MultiLine" Width="250" Visible="False"></asp:TextBox>
                                                                        <telerik:RadNumericTextBox ID="txtUSDInvoice" runat="server" CssClass="FormFld" Culture="(Default)"
                                                                            EnableEmbeddedSkins="False" ForeColor="Blue" MaxLength="20" MaxValue="100000000"
                                                                            MinValue="0" ShowSpinButtons="False" TabIndex="25" Width="215px" Visible="False">
                                                                            <NumberFormat AllowRounding="False" DecimalDigits="4" />
                                                                            <IncrementSettings InterceptArrowKeys="False" InterceptMouseWheel="False" />
                                                                        </telerik:RadNumericTextBox>
