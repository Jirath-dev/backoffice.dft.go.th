<%@ Control Language="vb" AutoEventWireup="false" Codebehind="ctrlCompanyDetailPreCheck.ascx.vb"
    Inherits=".ctrlCompanyDetailPreCheck" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="RadAjaxManager1">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="rgNoExpiredCardList" />
                <telerik:AjaxUpdatedControl ControlID="rgExpiredCardList" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="rgNoExpiredCardList">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="rgNoExpiredCardList" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="rgExpiredCardList">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="rgExpiredCardList" />
            </UpdatedControls>
        </telerik:AjaxSetting>
    </AjaxSettings>
</telerik:RadAjaxManager>
<telerik:RadWindowManager ID="RadWindowManager1" Behavior="Close" runat="server"
    Skin="Office2007" TabIndex="-1">
    <Windows>
        <telerik:RadWindow ID="EditDialog" runat="server" ReloadOnShow="True" ShowContentDuringLoad="False"
            Modal="True" VisibleStatusbar="False" Width="900px" Height="500px" />
    </Windows>
</telerik:RadWindowManager>
<telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">

    <script type="text/javascript">
        function ShowEditCardForm(cardno, taxno) {
            window.radopen("/DesktopModules/DFT_RFCard_PreCheck/frmEditPersonCardDetail.aspx?keycode1=" + cardno + "&keycode2=" + taxno + "&action=edit", "EditDialog");
            return false;
        }
        
        function ShowEditCompanyCardForm(cardno, taxno) {
            window.radopen("/DesktopModules/DFT_RFCard_PreCheck/frmEditCompanyCardDetail.aspx?keycode1=" + cardno + "&keycode2=" + taxno + "&action=edit", "EditDialog");
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
    </script>

</telerik:RadScriptBlock>
<table cellpadding="3" cellspacing="3" style="width: 100%">
    <tr>
        <td style="width: 100%">
            <table cellpadding="0" cellspacing="0" border="0" width="100%">
                <tr>
                    <td style="width: 100%;">
                        <telerik:RadTabStrip ID="RadTabStrip1" runat="server" Skin="Office2007" Width="100%"
                            SelectedIndex="0" MultiPageID="RadMultiPage1" Font-Names="Tahoma" Font-Size="10pt">
                            <Tabs>
                                <telerik:RadTab runat="server" Text="รายละเอียดนิติบุคคล" Selected="True" Font-Names="Tahoma"
                                    Font-Size="10pt">
                                </telerik:RadTab>
                                <telerik:RadTab runat="server" Text="รายการบัตรประจำตัว (ยังไม่หมดอายุ)" Font-Names="Tahoma"
                                    Font-Size="10pt">
                                </telerik:RadTab>
                                <telerik:RadTab runat="server" Text="รายการบัตรประจำตัวที่ถูกยกเลิก/หมดอายุ" Font-Names="Tahoma"
                                    Font-Size="10pt">
                                </telerik:RadTab>
                            </Tabs>
                        </telerik:RadTabStrip>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #eeeeee; border-width: 1px; border-style: solid; border-color: #CCCCCC;">
                        <telerik:RadMultiPage ID="RadMultiPage1" runat="server" Width="100%" SelectedIndex="0"
                            TabIndex="-1">
                            <telerik:RadPageView ID="RadPageView1" runat="server" Width="100%" TabIndex="-1">
                                <table width="100%" border="0" cellspacing="5" cellpadding="0">
                                    <tr>
                                        <td>
                                            <table border="0" cellpadding="0" cellspacing="0" class="m-frm-bdr" width="100%">
                                                <tr class="m-frm-hdr">
                                                    <td align="left" class="m-frm-hdr">
                                                        <table border="0" cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td class="groupboxheader" nowrap="nowrap">
                                                                    <font class="groupbox">&nbsp; ข้อมูลนิติบุคคล &nbsp;</font></td>
                                                                <telerik:RadCodeBlock ID="RadCodeBlock3" runat="server">
                                                                    <td style='background-image: url(<%= ResolveUrl("images") %>/CORNER.gif); background-repeat: no-repeat'>
                                                                        &nbsp; &nbsp;&nbsp;</td>
                                                                </telerik:RadCodeBlock>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr class="m-frm-hdr">
                                                    <td align="left">
                                                        <table border="0" cellpadding="0" cellspacing="2" width="100%">
                                                            <tr>
                                                                <td style="font-size: 12pt; color: #000000; font-family: Times New Roman">
                                                                    <font class="FormLabel">ชื่อบริษัท (ไทย):&nbsp;</font>
                                                                    <telerik:RadTextBox ID="txtCompanyName_Th" runat="server" CssClass="FormFld" EnableEmbeddedSkins="False"
                                                                        ReadOnly="True" TabIndex="-1" Width="315px">
                                                                    </telerik:RadTextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="font-size: 12pt; color: #000000; font-family: Times New Roman">
                                                                    <font class="FormLabel">ชื่อบริษัท (อังกฤษ):&nbsp;</font>
                                                                    <telerik:RadTextBox ID="txtCompanyName_Eng" runat="server" CssClass="FormFld" EnableEmbeddedSkins="False"
                                                                        ReadOnly="True" TabIndex="-1" Width="315px">
                                                                    </telerik:RadTextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="font-size: 12pt; color: #000000; font-family: Times New Roman">
                                                                    <font class="FormLabel">เลขประจำตัวผู้เสียภาษี:</font>
                                                                    <telerik:RadTextBox ID="txtCompany_Taxno" runat="server" CssClass="FormFld" EnableEmbeddedSkins="False"
                                                                        ReadOnly="True" TabIndex="-1" Width="150px">
                                                                    </telerik:RadTextBox>
                                                                    <font class="FormLabel">เลขทะเบียนพาณิชย์:</font>
                                                                    <telerik:RadTextBox ID="txtCompany_Juristic" runat="server" CssClass="FormFld" EnableEmbeddedSkins="False"
                                                                        ReadOnly="True" TabIndex="-1" Width="150px">
                                                                    </telerik:RadTextBox>
                                                                    <telerik:RadTextBox ID="txtCompany_BranchNo" runat="server" CssClass="FormFld" EnableEmbeddedSkins="False"
                                                                        ReadOnly="True" TabIndex="-1" Visible="False" Width="1px">
                                                                    </telerik:RadTextBox>
                                                                </td>
                                                            </tr>
                                                            <tr style="font-size: 12pt; font-family: Times New Roman">
                                                                <td style="font-size: 12pt; font-family: Times New Roman">
                                                                    <font class="FormLabel">ที่อยู่ (ไทย) :</font>
                                                                    <telerik:RadTextBox ID="txtCompany_AddressTh" runat="server" CssClass="FormFld" EnableEmbeddedSkins="False"
                                                                        ReadOnly="True" TabIndex="-1" Width="680px">
                                                                    </telerik:RadTextBox></td>
                                                            </tr>
                                                            <tr style="font-size: 12pt; font-family: Times New Roman">
                                                                <td style="font-size: 12pt; font-family: Times New Roman">
                                                                    <font class="FormLabel">จังหวัด (ไทย):</font>
                                                                    <telerik:RadTextBox ID="txtCompany_Province_Thai" runat="server" CssClass="FormFld"
                                                                        EnableEmbeddedSkins="False" ReadOnly="True" TabIndex="-1" Width="200px">
                                                                    </telerik:RadTextBox></td>
                                                            </tr>
                                                            <tr style="font-size: 12pt; font-family: Times New Roman">
                                                                <td style="font-size: 12pt; font-family: Times New Roman">
                                                                    <font class="FormLabel">ที่อยู่ (อังกฤษ):&nbsp;</font>
                                                                    <telerik:RadTextBox ID="txtCompany_AddressEng" runat="server" CssClass="FormFld"
                                                                        EnableEmbeddedSkins="False" ReadOnly="True" TabIndex="-1" Width="680px">
                                                                    </telerik:RadTextBox></td>
                                                            </tr>
                                                            <tr style="font-size: 12pt; font-family: Times New Roman">
                                                                <td style="font-size: 12pt; font-family: Times New Roman">
                                                                    <font class="FormLabel">จังหวัด (อังกฤษ):</font>
                                                                    <telerik:RadTextBox ID="txtCompany_Province_Eng" runat="server" CssClass="FormFld"
                                                                        EnableEmbeddedSkins="False" ReadOnly="True" TabIndex="-1" Width="200px">
                                                                    </telerik:RadTextBox></td>
                                                            </tr>
                                                            <tr style="font-size: 12pt; font-family: Times New Roman">
                                                                <td>
                                                                    <font class="FormLabel">โทรศัพท์:</font>
                                                                    <telerik:RadTextBox ID="txtCompany_Phone" runat="server" CssClass="FormFld" EnableEmbeddedSkins="False"
                                                                        ReadOnly="True" TabIndex="-1" Width="150px">
                                                                    </telerik:RadTextBox>
                                                                    <font class="FormLabel">&nbsp;โทรสาร:</font>
                                                                    <telerik:RadTextBox ID="txtCompany_Fax" runat="server" CssClass="FormFld" EnableEmbeddedSkins="False"
                                                                        ReadOnly="True" TabIndex="-1" Width="150px">
                                                                    </telerik:RadTextBox>
                                                                </td>
                                                            </tr>
                                                            <tr style="font-size: 12pt; font-family: Times New Roman">
                                                                <td>
                                                                    <table cellpadding="0" cellspacing="0">
                                                                        <tr>
                                                                            <td>
                                                                                <font class="FormLabel">กรรมการผู้มีอำนาจ</font>&nbsp;</td>
                                                                            <td>
                                                                                <font class="FormLabel">1.</font>
                                                                                <telerik:RadTextBox ID="txtAuthorize1" runat="server" CssClass="FormFld" EnableEmbeddedSkins="False"
                                                                                    ReadOnly="True" TabIndex="-1" Width="300px">
                                                                                </telerik:RadTextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                            </td>
                                                                            <td>
                                                                                <font class="FormLabel">2.</font>&nbsp;<telerik:RadTextBox ID="txtAuthorize2" runat="server"
                                                                                    CssClass="FormFld" EnableEmbeddedSkins="False" ReadOnly="True" TabIndex="-1"
                                                                                    Width="300px">
                                                                                </telerik:RadTextBox></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                            </td>
                                                                            <td>
                                                                                <font class="FormLabel">3.</font>
                                                                                <telerik:RadTextBox ID="txtAuthorize3" runat="server" CssClass="FormFld" EnableEmbeddedSkins="False"
                                                                                    ReadOnly="True" TabIndex="-1" Width="300px">
                                                                                </telerik:RadTextBox></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                            </td>
                                                                            <td>
                                                                                <font class="FormLabel">4.</font>
                                                                                <telerik:RadTextBox ID="txtAuthorize4" runat="server" CssClass="FormFld" EnableEmbeddedSkins="False"
                                                                                    ReadOnly="True" TabIndex="-1" Width="300px">
                                                                                </telerik:RadTextBox></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                            </td>
                                                                            <td>
                                                                                <font class="FormLabel">5.</font>
                                                                                <telerik:RadTextBox ID="txtAuthorize5" runat="server" CssClass="FormFld" EnableEmbeddedSkins="False"
                                                                                    ReadOnly="True" TabIndex="-1" Width="300px">
                                                                                </telerik:RadTextBox></td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <font class="FormLabel">หมายเหตุกรรมการ:&nbsp;</font>
                                                                    <telerik:RadTextBox ID="txtAuthorize_Remark" runat="server" CssClass="FormFld" EnableEmbeddedSkins="False"
                                                                        ReadOnly="True" TabIndex="-1" Width="700px">
                                                                    </telerik:RadTextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <font class="FormLabel">Company Internet :</font>
                                                                    <telerik:RadTextBox ID="txtCompany_Internet" runat="server" CssClass="FormFld" EnableEmbeddedSkins="False"
                                                                        ReadOnly="True" TabIndex="-1" Width="50px">
                                                                    </telerik:RadTextBox></td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </telerik:RadPageView>
                            <telerik:RadPageView ID="RadPageView2" runat="server" Width="100%" TabIndex="-1">
                                <table width="100%" border="0" cellspacing="5" cellpadding="0">
                                    <tr>
                                        <td>
                                            <table border="0" cellpadding="0" cellspacing="0" class="m-frm-bdr" width="100%">
                                                <tr class="m-frm-hdr">
                                                    <td align="left" class="m-frm-hdr">
                                                        <table border="0" cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td class="groupboxheader" nowrap="nowrap">
                                                                    <font class="groupbox">&nbsp; รายการบัตรประจำตัวผู้ส่งออก - นำเข้าสินค้า (ยังไม่หมดอายุ)
                                                                        &nbsp;</font></td>
                                                                <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
                                                                    <td style='background-image: url(<%= ResolveUrl("images") %>/CORNER.gif); background-repeat: no-repeat'>
                                                                        &nbsp; &nbsp;&nbsp;</td>
                                                                </telerik:RadCodeBlock>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr class="m-frm-hdr">
                                                    <td align="left">
                                                        <table border="0" cellpadding="0" cellspacing="2" width="100%">
                                                            <tr>
                                                                <td>
                                                                    <telerik:RadGrid ID="rgNoExpiredCardList" runat="server" Font-Names="Tahoma" Font-Size="X-Small"
                                                                        GridLines="None" ShowStatusBar="True" Skin="Office2007" TabIndex="-1" Width="100%">
                                                                        <MasterTableView AutoGenerateColumns="False" CellSpacing="-1" ClientDataKeyNames="card_id,company_taxno"
                                                                            DataKeyNames="card_id,company_taxno" NoMasterRecordsText="ไม่มีรายการบัตรประจำตัวผู้นำเข้าส่งออกที่ได้รับการอนุมัติ"
                                                                            Width="100%">
                                                                            <Columns>
                                                                                <telerik:GridTemplateColumn UniqueName="TemplateViewColumn">
                                                                                    <ItemTemplate>
                                                                                        <asp:HyperLink ID="ViewLink" runat="server" ImageUrl="~/images/edit.gif" Text="View"></asp:HyperLink>
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle HorizontalAlign="Center" Width="15px" />
                                                                                </telerik:GridTemplateColumn>
                                                                                <telerik:GridBoundColumn DataField="card_id" HeaderText="รหัสบัตร" SortExpression="card_id"
                                                                                    UniqueName="card_id">
                                                                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                                </telerik:GridBoundColumn>
                                                                                <telerik:GridBoundColumn DataField="card_type" HeaderText="ประเภท" SortExpression="card_type"
                                                                                    UniqueName="card_type">
                                                                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                                </telerik:GridBoundColumn>
                                                                                <telerik:GridBoundColumn DataField="AuthName_Thai" HeaderText="ชื่อผู้ถือบัตร (ไทย)"
                                                                                    SortExpression="AuthName_Thai" UniqueName="AuthName_Thai">
                                                                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                                                                </telerik:GridBoundColumn>
                                                                                <telerik:GridBoundColumn DataField="AuthName" HeaderText="ชื่อผู้ถือบัตร (อังกฤษ)"
                                                                                    SortExpression="AuthName" UniqueName="AuthName">
                                                                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                                                                </telerik:GridBoundColumn>
                                                                                <telerik:GridBoundColumn DataField="Start_Date" DataFormatString="{0:dd/MM/yyyy}"
                                                                                    HeaderText="วันออกบัตร" SortExpression="Start_Date" UniqueName="Start_Date">
                                                                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                                </telerik:GridBoundColumn>
                                                                                <telerik:GridBoundColumn DataField="expire_Date" DataFormatString="{0:dd/MM/yyyy}"
                                                                                    HeaderText="วันหมดอายุ" SortExpression="expire_Date" UniqueName="expire_Date">
                                                                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                                </telerik:GridBoundColumn>
                                                                                <telerik:GridBoundColumn DataField="site_id" HeaderText="สถานที่ออกบัตร" SortExpression="site_id"
                                                                                    UniqueName="site_id">
                                                                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                                </telerik:GridBoundColumn>
                                                                                <telerik:GridBoundColumn DataField="Form_ID" HeaderText="คำร้อง" SortExpression="Form_ID"
                                                                                    UniqueName="Form_ID">
                                                                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                                </telerik:GridBoundColumn>
                                                                                <telerik:GridBoundColumn DataField="active_flag" HeaderText="สถานะ" SortExpression="active_flag"
                                                                                    UniqueName="active_flag">
                                                                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                                </telerik:GridBoundColumn>
                                                                            </Columns>
                                                                        </MasterTableView>
                                                                        <HeaderStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                        <ClientSettings EnableRowHoverStyle="True">
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
                            </telerik:RadPageView>
                            <telerik:RadPageView ID="RadPageView3" runat="server" Width="100%" TabIndex="-1">
                                <table width="100%" border="0" cellspacing="5" cellpadding="0">
                                    <tr>
                                        <td>
                                            <table border="0" cellpadding="0" cellspacing="0" class="m-frm-bdr" width="100%">
                                                <tr class="m-frm-hdr">
                                                    <td align="left" class="m-frm-hdr">
                                                        <table border="0" cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td class="groupboxheader" nowrap="nowrap">
                                                                    <font class="groupbox">&nbsp; รายการบัตรประจำตัวผู้ส่งออก - นำเข้าสินค้า (หมดอายุ) &nbsp;</font></td>
                                                                <telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">
                                                                    <td style='background-image: url(<%= ResolveUrl("images") %>/CORNER.gif); background-repeat: no-repeat'>
                                                                        &nbsp; &nbsp;&nbsp;</td>
                                                                </telerik:RadCodeBlock>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr class="m-frm-hdr">
                                                    <td align="left">
                                                        <table border="0" cellpadding="0" cellspacing="2" width="100%">
                                                            <tr>
                                                                <td>
                                                                    <telerik:RadGrid ID="rgExpiredCardList" runat="server" Font-Names="Tahoma" Font-Size="X-Small"
                                                                        GridLines="None" ShowStatusBar="True" Skin="Office2007" TabIndex="-1" Width="100%">
                                                                        <MasterTableView AutoGenerateColumns="False" CellSpacing="-1" ClientDataKeyNames="card_id,company_taxno"
                                                                            DataKeyNames="card_id,company_taxno" NoMasterRecordsText="ไม่มีรายการบัตรประจำตัวผู้นำเข้าส่งออกที่ได้รับการอนุมัติ"
                                                                            Width="100%">
                                                                            <Columns>
                                                                                <telerik:GridTemplateColumn UniqueName="TemplateViewColumn">
                                                                                    <ItemTemplate>
                                                                                        <asp:HyperLink ID="ViewLink" runat="server" ImageUrl="~/images/edit.gif" Text="View"></asp:HyperLink>
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle HorizontalAlign="Center" Width="15px" />
                                                                                </telerik:GridTemplateColumn>
                                                                                <telerik:GridBoundColumn DataField="card_id" HeaderText="รหัสบัตร" SortExpression="card_id"
                                                                                    UniqueName="card_id">
                                                                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                                </telerik:GridBoundColumn>
                                                                                <telerik:GridBoundColumn DataField="card_type" HeaderText="ประเภท" SortExpression="card_type"
                                                                                    UniqueName="card_type">
                                                                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                                </telerik:GridBoundColumn>
                                                                                <telerik:GridBoundColumn DataField="AuthName_Thai" HeaderText="ชื่อผู้ถือบัตร (ไทย)"
                                                                                    SortExpression="AuthName_Thai" UniqueName="AuthName_Thai">
                                                                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                                                                </telerik:GridBoundColumn>
                                                                                <telerik:GridBoundColumn DataField="AuthName" HeaderText="ชื่อผู้ถือบัตร (อังกฤษ)"
                                                                                    SortExpression="AuthName" UniqueName="AuthName">
                                                                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                                                                </telerik:GridBoundColumn>
                                                                                <telerik:GridBoundColumn DataField="Start_Date" DataFormatString="{0:dd/MM/yyyy}"
                                                                                    HeaderText="วันออกบัตร" SortExpression="Start_Date" UniqueName="Start_Date">
                                                                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                                </telerik:GridBoundColumn>
                                                                                <telerik:GridBoundColumn DataField="expire_Date" DataFormatString="{0:dd/MM/yyyy}"
                                                                                    HeaderText="วันหมดอายุ" SortExpression="expire_Date" UniqueName="expire_Date">
                                                                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                                </telerik:GridBoundColumn>
                                                                                <telerik:GridBoundColumn DataField="site_id" HeaderText="สถานที่ออกบัตร" SortExpression="site_id"
                                                                                    UniqueName="site_id">
                                                                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                                </telerik:GridBoundColumn>
                                                                                <telerik:GridBoundColumn DataField="Form_ID" HeaderText="คำร้อง" SortExpression="Form_ID"
                                                                                    UniqueName="Form_ID">
                                                                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                                </telerik:GridBoundColumn>
                                                                                <telerik:GridBoundColumn DataField="active_flag" HeaderText="สถานะ" SortExpression="active_flag"
                                                                                    UniqueName="active_flag">
                                                                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                                </telerik:GridBoundColumn>
                                                                            </Columns>
                                                                        </MasterTableView>
                                                                        <HeaderStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                        <ClientSettings EnableRowHoverStyle="True">
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
                            </telerik:RadPageView>
                        </telerik:RadMultiPage>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
<asp:Label ID="lblSite_ID" runat="server" Visible="False"></asp:Label>