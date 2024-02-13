<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrl_AllEditSign.ascx.vb" Inherits=".ctrl_AllEditSign" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="RadAjaxManager1">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="RadCbbSite" />
                <telerik:AjaxUpdatedControl ControlID="RadCbbUser" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="RadCbbSite">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="RadCbbSite" />
                <telerik:AjaxUpdatedControl ControlID="Panel1" />
                <telerik:AjaxUpdatedControl ControlID="RadCbbUser" />
                <telerik:AjaxUpdatedControl ControlID="txtFullName" />
                <telerik:AjaxUpdatedControl ControlID="txtTAXID" />
                <telerik:AjaxUpdatedControl ControlID="LabelDisplay" />
                <telerik:AjaxUpdatedControl ControlID="txtUserNameTemp" />
                <telerik:AjaxUpdatedControl ControlID="FileUploadDoc" />
                <telerik:AjaxUpdatedControl ControlID="PanelStatus" />
                <telerik:AjaxUpdatedControl ControlID="PanelFormType" />
                <telerik:AjaxUpdatedControl ControlID="chb_Noimage" />
                <telerik:AjaxUpdatedControl ControlID="txtRemark" />
                <telerik:AjaxUpdatedControl ControlID="lbl_ErrMSG" />
                <telerik:AjaxUpdatedControl ControlID="txtTemp_Gov_action" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="RadCbbUser">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="RadCbbSite" />
                <telerik:AjaxUpdatedControl ControlID="Panel1" />
                <telerik:AjaxUpdatedControl ControlID="RadCbbUser" />
                <telerik:AjaxUpdatedControl ControlID="txtFullName" />
                <telerik:AjaxUpdatedControl ControlID="txtTAXID" />
                <telerik:AjaxUpdatedControl ControlID="LabelDisplay" />
                <telerik:AjaxUpdatedControl ControlID="txtUserNameTemp" />
                <telerik:AjaxUpdatedControl ControlID="FileUploadDoc" />
                <telerik:AjaxUpdatedControl ControlID="PanelStatus" />
                <telerik:AjaxUpdatedControl ControlID="PanelFormType" />
                <telerik:AjaxUpdatedControl ControlID="chb_Noimage" />
                <telerik:AjaxUpdatedControl ControlID="txtRemark" />
                <telerik:AjaxUpdatedControl ControlID="lbl_ErrMSG" />
                <telerik:AjaxUpdatedControl ControlID="txtTemp_Gov_action" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="CheckBoxListFormType">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="CheckBoxListFormType" />
                <telerik:AjaxUpdatedControl ControlID="chb_Noimage" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="chb_Noimage">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="chb_Noimage" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="BtnAdd">
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="BtnEdit">
        </telerik:AjaxSetting>
    </AjaxSettings>
</telerik:RadAjaxManager>
<br />
<table border="0" cellpadding="0" cellspacing="5" width="100%">
    <tr align="center">
        <td>
            <font class="FormHeader"></font>
        </td>
    </tr>
    <tr>
        <td>
            <table border="0" cellpadding="1" cellspacing="0" class="m-frm-bdr" width="100%">
                <tr>
                    <td>
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr class="m-frm-hdr">
                                <td style="width: 896px">
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                        <tr class="m-frm-hdr">
                                            <td align="left" class="m-frm-hdr" width="12%">
                                                <table border="0" cellpadding="0" cellspacing="0" cols="2">
                                                    <tr>
                                                        <td class="groupboxheader" nowrap="nowrap">
                                                            <font class="groupbox">&nbsp;<asp:Label ID="lblHeader" runat="server" CssClass="groupbox">รายละเอียดข้อมูล&nbsp;&nbsp;</asp:Label></font></td>
                                                        <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
                                                            <td style='background-image: url(<%= ResolveUrl("../images") %>/groupbox/CORNER.gif);
                                                                background-repeat: no-repeat;'>
                                                                &nbsp; &nbsp;&nbsp;</td>
                                                        </telerik:RadCodeBlock>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td align="left" class="m-frm-nav" width="88%">
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr class="m-frm-hdr">
                                <td style="width: 896px">
                                    <telerik:RadTabStrip ID="RadTabStrip1" runat="server" Font-Names="Tahoma" Font-Size="10pt"
                                        MultiPageID="RadMultiPage1" SelectedIndex="0" Skin="Office2007" Width="100%">
                                        <Tabs>
                                            <telerik:RadTab runat="server" Font-Names="Tahoma" Font-Size="10pt" Selected="True"
                                                Text="ข้อมูลรายเซ็นเจ้าหน้าที่">
                                            </telerik:RadTab>
                                        </Tabs>
                                    </telerik:RadTabStrip>
                                    <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0" TabIndex="-1"
                                        Width="100%">
                                        <telerik:RadPageView ID="RadPageView1" runat="server" TabIndex="-1" Width="100%">
                                            <table border="0" cellpadding="0" cellspacing="4" class="FormLabel" style="width: 100%">
                                                <tr>
                                                    <td style="width: 15%" valign="top">
                                                        สาขาของกรม :</td>
                                                    <td style="width: 40%" valign="top">
                                                        <telerik:RadComboBox ID="RadCbbSite" runat="server" AutoPostBack="True" DataTextField="site_name"
                                                            DataValueField="site_id" Width="350px" ForeColor="Blue" Skin="WebBlue">
                                                        </telerik:RadComboBox>
                                                    </td>
                                                    <td style="width: 45%" align="center" rowspan="6" valign="top">
                                                        <asp:Panel ID="Panel1" runat="server" CssClass="FormLabel" ForeColor="Blue" GroupingText="รูปภาพ"
                                                            HorizontalAlign="Center" Width="100%">
                                                            <asp:Image ID="Image1" runat="server" Height="189px" Width="340px" /><br />
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 15%" valign="top">
                                                        ชื่อ User :</td>
                                                    <td style="width: 40%" valign="top">
                                                        <telerik:RadComboBox ID="RadCbbUser" runat="server" AutoPostBack="True" Width="300px" ForeColor="Blue" Skin="WebBlue" Filter="StartsWith">
                                                        </telerik:RadComboBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 15%" valign="top">
                                                        ชื่อ-นามสกุล</td>
                                                    <td style="width: 40%" valign="top">
                                                        <table border="0" cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td>
                                                        <asp:TextBox ID="txtFullName" runat="server" Width="310px"></asp:TextBox></td>
                                                                <td>
                                                                    <asp:RequiredFieldValidator ID="RequiredName" runat="server" ControlToValidate="txtFullName"
                                                                        ErrorMessage="*" SetFocusOnError="True" ValidationGroup="GAdd"></asp:RequiredFieldValidator></td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 15%" valign="top">
                                                        เลข Tax USB</td>
                                                    <td style="width: 40%" valign="top">
                                                        <table border="0" cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td>
                                                        <asp:TextBox ID="txtTAXID" runat="server" Width="260px"></asp:TextBox></td>
                                                                <td>
                                                                    <asp:RequiredFieldValidator ID="RequiredTax" runat="server" ControlToValidate="txtTAXID"
                                                                        ErrorMessage="*" SetFocusOnError="True" ValidationGroup="GAdd"></asp:RequiredFieldValidator></td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 15%" valign="top">
                                                        <asp:Label ID="LabelDisplay" runat="server" Text="ชื่อ Display" Visible="False"></asp:Label></td>
                                                    <td style="width: 40%" valign="top">
                                                        <table border="0" cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td>
                                                        <asp:TextBox ID="txtUserNameTemp" runat="server" Width="260px" Visible="False"></asp:TextBox></td>
                                                                <td>
                                                                    <asp:RequiredFieldValidator ID="RequiredDisplay" runat="server" ControlToValidate="txtUserNameTemp"
                                                                        ErrorMessage="*" SetFocusOnError="True" ValidationGroup="GAdd"></asp:RequiredFieldValidator></td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 15%" valign="top">
                                                        ไฟล์รูปภาพ :</td>
                                                    <td style="width: 40%" valign="top">
                                                        <asp:FileUpload ID="FileUploadDoc" runat="server" ForeColor="Blue" Width="347px" /></td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3" valign="top">
                                                        <asp:Panel ID="PanelStatus" runat="server" Width="100%">
                                                            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                                                                <tr>
                                                                    <td style="width: 15%" valign="middle">
                                                                        สถานะ ลายเซ็น :</td>
                                                                    <td colspan="3" valign="middle">
                                                                        <table border="0" cellspacing="2" style="width: 100%">
                                                                            <tr>
                                                                                <td style="width: 230px">
                                                                        <asp:RadioButtonList ID="rdolistStatus" runat="server" RepeatDirection="Horizontal">
                                                                            <asp:ListItem Value="1">ใช้งานปรกติ</asp:ListItem>
                                                                            <asp:ListItem Value="2">ระงับการใช้งาน</asp:ListItem>
                                                                        </asp:RadioButtonList></td>
                                                                                <td>
                                                                        <asp:Button ID="btnChangeStatus" runat="server"
                                                                            Text="เปลี่ยนสถานะลายเซ็น" ValidationGroup="ED" /></td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        <telerik:RadComboBox ID="RadComboBox_UserNameTemp" runat="server" Width="300px" ForeColor="Blue" Skin="WebBlue" Visible="False">
                                                    </telerik:RadComboBox>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3" valign="top">
                                                        <asp:Panel ID="PanelFormType" runat="server" GroupingText="ฟอร์มที่ต้องใช้งาน" Width="100%">
                                                            <table border="0" cellpadding="3" cellspacing="3">
                                                                <tr>
                                                                    <td style="width: 40px">
                                                                    </td>
                                                                    <td>
                                                                        <asp:CheckBoxList ID="CheckBoxListFormType" runat="server" AutoPostBack="True" DataTextField="form_name"
                                                                            DataValueField="form_type" RepeatColumns="2">
                                                                        </asp:CheckBoxList></td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 15%" valign="top">
                                                    </td>
                                                    <td style="width: 40%" valign="top">
                                                        <asp:CheckBox ID="chb_Noimage" runat="server" Text="บันทึกข้อมูลแบบ ไม่ upload ลายเซ็น"
                                                            Visible="False" AutoPostBack="True" /></td>
                                                    <td style="width: 45%" valign="top">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 15%" valign="top">
                                                        หมายเหตุ :</td>
                                                    <td style="width: 40%" valign="top">
                                                        <asp:TextBox ID="txtRemark" runat="server" Height="100px" TextMode="MultiLine" Width="300px"></asp:TextBox></td>
                                                    <td style="width: 45%" valign="top">
                                                        <table border="0" cellpadding="0" cellspacing="2" style="width: 100%">
                                                            <tr>
                                                                <td valign="top">
                                    <asp:Button ID="BtnAdd" runat="server" Text="เพิ่มข้อมูลลายเซ็นเจ้าหน้าที่" Width="210px" ValidationGroup="GAdd" /><asp:Button ID="BtnEdit" runat="server" Text="บันทึกข้อมูลลายเซ็นเจ้าหน้าที่"
                                        Width="210px" ValidationGroup="GAdd" /></td>
                                                                <td valign="top">
                                    <asp:Button ID="BtnCancle" runat="server" Text="ยกเลิกการทำรายการ"
                                        Width="210px" /></td>
                                                            </tr>
                                                            <tr>
                                                                <td valign="top">
                                    <asp:Button ID="BtnDel" runat="server" Text="ลบข้อมูลลายเซ็นเจ้าหน้าที่"
                                        Width="210px" OnClientClick="return confirm('ต้องการลบข้อมูลนี้ ใช่หรือไม่?');" /></td>
                                                                <td>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 15%" valign="top">
                                                    </td>
                                                    <td style="width: 40%" valign="top">
                                                    </td>
                                                    <td style="width: 45%" valign="top">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center" colspan="3" valign="top">
            <asp:Label ID="lbl_ErrMSG" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label></td>
                                                </tr>
                                            </table>
                                        </telerik:RadPageView>
                                    </telerik:RadMultiPage>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" class="m-frm" style="width: 896px" valign="top">
                                    <asp:TextBox ID="txtTemp_Gov_action" runat="server" Visible="False" Width="40px"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td align="center" class="m-frm" style="width: 896px" valign="top">
                                </td>
                            </tr>
                            <tr>
                                <td align="center" class="m-frm" style="width: 896px" valign="top">
                                </td>
                            </tr>
                            <tr>
                                <td class="m-frm" style="width: 896px" valign="top" align="center">
                                    &nbsp;&nbsp;
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td align="center">
            </td>
    </tr>
    <tr>
        <td align="right">
            &nbsp;
        </td>
    </tr>
</table>