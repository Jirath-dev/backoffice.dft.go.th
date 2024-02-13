<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="CtrlCheckHistory.ascx.vb"
    Inherits=".CtrlCheckHistory" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<style type="text/css">
    table.tb-data
    {
        margin-top: 8px;
        border: 1px solid #cccccc;
        border-right: 0px;
        background: #ffffff;
    }
    table.tb-data td
    {
        padding: 3px;
        border-right: 1px solid #cccccc;
        border-bottom: 1px solid #cccccc;
    }
    table td.head
    {
        background: #ececec;
        text-align: center;
        font-weight: bold;
    }

    .sealsign { color:Green; }

</style>
<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="RadAjaxManager1">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="rgRequestForm" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="rgRequestForm">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="rgRequestForm" />
            </UpdatedControls>
        </telerik:AjaxSetting>
    </AjaxSettings>
</telerik:RadAjaxManager>
<telerik:RadCodeBlock ID="RadCodeBlockFirst" runat="server">

    <script type="text/javascript">
        function popup(url) {
            params = 'width=' + screen.width;
            params += ', height=' + screen.height;
            params += ', top=0, left=0'
            params += ', scrollbars=0, resizable=1';

            newwin = window.open(url, 'windowname4', params);
            if (window.focus) { newwin.focus() }
            return false;
        }

        function ShowError(refid) {
            return window.showModalDialog("https://edi.dft.go.th/DesktopModules/DFT_eCO_EDI/Popup/ViewErrors.aspx?InvHRunAuto=" + refid, window.self, "dialogHeight:370px;dialogWidth:720px;help:no;status:no;center:yes");
        }

        function refreshGrid(arg) {
            try {
                //var obj = $get("<%= btnSearch.ClientID %>");
                //obj.click();
                if (!arg) {
                    $find("<%= RadAjaxManager1.ClientID %>").ajaxRequest("Rebind");
                }
                else {
                    $find("<%= RadAjaxManager1.ClientID %>").ajaxRequest("RebindAndNavigate");
                }
            } catch (e) { }
        }

        function clearTextBox(type) {
            if (type == '1') {
                document.getElementById('<%=txtRefNo.ClientID %>').value = '';
            }
            else if (type == '2') {
                document.getElementById('<%=txtInvoiceNo.ClientID %>').value = '';
            }

        }

        function collapsSearch(id) {
            var div = document.getElementById('<%=collapsdiv.ClientID %>');
            var icon = document.getElementById('<%=colIcon.ClientID %>');
            var divMain = document.getElementById('<%=collaps_div_main.ClientID %>');
            var txtTempCollaps = document.getElementById('<%=txtTempCollaps.ClientID %>');
            if (div) {
                if (div.style.display == 'none') {
                    div.style.display = 'block';
                    divMain.style.borderLeft = '1px solid #cccccc';
                    divMain.style.borderBottom = '1px solid #cccccc';
                    divMain.style.borderRight = '1px solid #cccccc';
                    icon.innerHTML = '(-)';
                    txtTempCollaps.value = '1';
                }
                else {
                    div.style.display = 'none';
                    divMain.style.borderLeft = '';
                    divMain.style.borderBottom = '';
                    divMain.style.borderRight = '';
                    icon.innerHTML = '(+)';
                    txtTempCollaps.value = '0'
                }
            }
            clearTextBox('1');
            clearTextBox('2');
        }

    </script>

</telerik:RadCodeBlock>
<div style="display: none;">
    <asp:TextBox ID="txtTempCollaps" runat="server" Text="0">
    </asp:TextBox>
</div>
<table style="width: 100%" cellpadding="3" cellspacing="3">
    <tr>
        <td>
            <table id="tblRefList" runat="server" border="0" cellpadding="1" cellspacing="0"
                class="m-frm-bdr" width="100%">
                <tr>
                    <td>
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr class="m-frm-hdr">
                                <td>
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                        <tr class="m-frm-hdr">
                                            <td align="left" class="m-frm-hdr">
                                                <table border="0" cellpadding="0" cellspacing="0" cols="2">
                                                    <tr>
                                                        <td class="groupboxheader" nowrap="nowrap">
                                                            <font class="groupbox">&nbsp; ข้อมูลคำร้องและเอกสารแนบที่ตรวจสอบเรียบร้อยแล้ว &nbsp;</font>
                                                        </td>
                                                        <telerik:RadCodeBlock ID="RadCodeBlock6" runat="server">
                                                            <td style='background-image: url(<%= ResolveUrl("images") %>/CORNER.gif); background-repeat: no-repeat;'>
                                                                &nbsp; &nbsp;&nbsp;
                                                            </td>
                                                        </telerik:RadCodeBlock>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td align="right" class="m-frm-nav">
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td class="m-frm" valign="top" width="100%">
                                    <div style="margin: 10px;">
                                        <div style="font-size: 10pt; margin-top: 10px; margin-bottom: 10px;">
                                            แสดงผลรายการแบบฟอร์มคำร้อง และเอกสารแนบที่ตรวจสอบเรียบร้อยแล้ว</div>
                                        <div style="font-size: 10pt; margin: 5px;">
                                            <span runat="server" id="colIcon" style="color: #999999; font-size: 10px;" onclick="collapsSearch('collapsdiv');"
                                                onmouseover="this.style.cursor='pointer';">(+)</span> <a href="#" onclick="collapsSearch('collapsdiv');">
                                                    ค้นหาขั้นสูง</a> &nbsp;<span class="FormLabel">(ค้นหาจากเลขที่อ้างอิง หรือเลขที่ Invoice)</span>
                                        </div>
                                        <div id="collaps_div_main" runat="server" style="margin-bottom: 6px; font-size: 9pt;
                                            padding-bottom: 1px; border-top: 1px solid #cccccc;">
                                            <div id="collapsdiv" runat="server" style="padding: 6px 6px 1px 6px; margin-bottom: 2px;
                                                display: none">
                                                <table style="margin-bottom: 5px;">
                                                    <tr>
                                                        <td align="right">
                                                            <font class="FormLabel">เลขที่อ้างอิง :</font>
                                                        </td>
                                                        <td valign="middle">
                                                            <asp:TextBox ID="txtRefNo" runat="server" Enabled="true"></asp:TextBox>
                                                            <img src="/DesktopModules/DFT_EDI_CheckAttachment/images/Delete.gif" width="15px"
                                                                height="15px" alt="" title="ลบเลขที่อ้างอิง" onclick="clearTextBox('1');" onmouseover="this.style.cursor='pointer';" />
                                                        </td>
                                                        <td align="right">
                                                            <font class="FormLabel">เลขที่ Invoice :</font>
                                                        </td>
                                                        <td valign="middle">
                                                            <asp:TextBox ID="txtInvoiceNo" runat="server" Enabled="true"></asp:TextBox>
                                                            <img src="/DesktopModules/DFT_EDI_CheckAttachment/images/Delete.gif" width="15px"
                                                                height="15px" alt="" title="ลบเลขที่ Invoice" onclick="clearTextBox('2');" onmouseover="this.style.cursor='pointer';" />
                                                        </td>
                                                    </tr>
                                                </table>
                                                <dl style="padding-top: 2px; margin-top: 0px; margin-bottom: 2px;">
                                                    <dt>หมายเหตุการค้นหา </dt>
                                                    <dd>
                                                        - <font style="color: Red; font-size: 11px; font-weight: normal;">การค้นหาตามเลขที่อ้างอิง
                                                            หรือเลขที่ Invoice ไม่จำเป็นต้องต้องระบุวันที่</font></dd>
                                                    <dd>
                                                        - <font style="color: Red; font-size: 11px; font-weight: normal;">หากต้องการค้นหาตามวันที่
                                                            กรุณาลบเลขที่อ้างอิง และเลขที่ Invoice ก่อนการค้นหา</font>
                                                    </dd>
                                                </dl>
                                                <div style="text-align: right; font-size: 11px; font-weight: normal; margin: 1px;">
                                                    <a href="#" onclick="collapsSearch('collapsdiv');">(-)ซ่อน</a>
                                                </div>
                                            </div>
                                        </div>
                                        <table style="margin-bottom: 4px;">
                                            <tr>
                                                <td align="right">
                                                    <font class="FormLabel">ฟอร์ม :</font>
                                                </td>
                                                <td>
                                                    <telerik:RadComboBox ID="dropFormType" runat="server" DataTextField="form_nameUsd" DataValueField="form_type"
                                                        Filter="StartsWith" MarkFirstMatch="True" Skin="Web20" Width="400px" Font-Names="Tahoma"
                                                        Font-Size="10pt">
                                                    </telerik:RadComboBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <font class="FormLabel">วันที่ตรวจ ตั้งแต่ :</font>
                                                </td>
                                                <td>
                                                    <table cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td>
                                                                <telerik:RadDatePicker ID="rdpFromDate" runat="server" Culture="English (United Kingdom)"
                                                                    Skin="Vista">
                                                                    <Calendar DayNameFormat="FirstTwoLetters" FirstDayOfWeek="Sunday" ShowRowHeaders="False"
                                                                        Skin="Vista" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                                        ViewSelectorText="x">
                                                                    </Calendar>
                                                                    <DatePopupButton HoverImageUrl="" ImageUrl="" />
                                                                </telerik:RadDatePicker>
                                                            </td>
                                                            <td>
                                                                <font class="FormLabel">&nbsp;ถึง :&nbsp;</font>
                                                            </td>
                                                            <td>
                                                                <telerik:RadDatePicker ID="rdpToDate" runat="server" Culture="English (United Kingdom)"
                                                                    Skin="Vista">
                                                                    <Calendar DayNameFormat="FirstTwoLetters" FirstDayOfWeek="Sunday" ShowRowHeaders="False"
                                                                        Skin="Vista" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                                        ViewSelectorText="x">
                                                                    </Calendar>
                                                                    <DatePopupButton HoverImageUrl="" ImageUrl="" />
                                                                </telerik:RadDatePicker>
                                                            </td>
                                                            <td align="right">
                                                                <asp:Button ID="btnSearch" runat="server" Text="ค้นหา" Width="150px" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                            <tr>
                                                <td>
                                                    <table border="0" cellpadding="0" cellspacing="2" width="100%">
                                                        <tr>
                                                            <td>
                                                                <div style="margin-bottom: 10px; font-size: 10pt;">
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <b>จำนวนฟอร์มทั้งหมด</b>&nbsp;
                                                                            </td>
                                                                            <td style="width: 30px;">
                                                                                <asp:Label ID="lblTotalForm" runat="server" Font-Bold="True" ForeColor="#000099"
                                                                                    Text="0"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <b>ผ่านการตรวจสอบ</b> =
                                                                                <asp:Label ID="lblOK" runat="server" Font-Bold="True" ForeColor="#009933" Text="0"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <b>/ ไม่ผ่าน</b> =
                                                                                <asp:Label ID="lblNO" runat="server" Font-Bold="True" ForeColor="Red" Text="0"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                                <telerik:RadGrid ID="rgRequestForm" runat="server" AllowPaging="True" GridLines="None"
                                                                    PageSize="20" Skin="Office2007" TabIndex="-1" Width="100%" ShowStatusBar="True"
                                                                    AllowSorting="True"  ShowGroupPanel="true" GroupingSettings-RetainGroupFootersVisibility="true">
                                                                    <GroupPanel Text="กรณีต้องการจัดกลุ่มข้อมูล ให้ลากชื่อคอลัมน์ที่ต้องการจัดกลุ่มมาใส่ในแถบตรงนี้"></GroupPanel>
                                                                    <MasterTableView AutoGenerateColumns="False" ClientDataKeyNames="invh_run_auto,form_type,SentBy,edi_status_id,company_taxno,SealSign,SignImageID"
                                                                        DataKeyNames="invh_run_auto,form_type,SentBy,edi_status_id,company_taxno,SealSign,SignImageID" NoMasterRecordsText="ไม่มีรายการคำร้อง"
                                                                        Width="100%" PagerStyle-AlwaysVisible="true" PagerStyle-FirstPageToolTip="หน้าแรก"
                                                                        PagerStyle-LastPageToolTip="หน้าสุดท้าย" PagerStyle-NextPageToolTip="หน้าต่อไป"
                                                                        PagerStyle-PageSizeLabelText="แสดงหน้าละ" PagerStyle-PrevPageToolTip="ก่อนหน้านี้"
                                                                        PagerStyle-PagerTextFormat="{4}  หน้า {0} จาก {1} (รายการที่ {2} ถึง {3} จากทั้งหมด {5})" > <%--GroupsDefaultExpanded="false"--%>
                                                                        <Columns>
                                                                            <telerik:GridTemplateColumn UniqueName="ViewTemplateColumn">
                                                                                <HeaderStyle Width="15px" HorizontalAlign="Center" />
                                                                                <ItemStyle Width="15px" HorizontalAlign="Center" />
                                                                                <ItemTemplate>
                                                                                    <asp:HyperLink ID="ViewLink" ImageUrl="~/images/view.gif" Target="_blank" runat="server"></asp:HyperLink>
                                                                                </ItemTemplate>
                                                                            </telerik:GridTemplateColumn>
                                                                            <telerik:GridBoundColumn DataField="FORM_NAME" HeaderText="ชื่อฟอร์ม" ReadOnly="True"
                                                                                SortExpression="FORM_NAME" UniqueName="FORM_NAME">
                                                                                <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                                                            </telerik:GridBoundColumn>
                                                                            <telerik:GridBoundColumn DataField="invh_run_auto" HeaderText="เลขที่อ้างอิง" ReadOnly="True"
                                                                                SortExpression="invh_run_auto" UniqueName="invh_run_auto">
                                                                                <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                            </telerik:GridBoundColumn>
                                                                            <telerik:GridBoundColumn DataField="invoice_no1" HeaderText="Invoice No." ReadOnly="True"
                                                                                SortExpression="invoice_no1" UniqueName="invoice_no1">
                                                                                <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                                                            </telerik:GridBoundColumn>
                                                                            <telerik:GridBoundColumn DataField="company_name" HeaderText="ผู้ขอ" SortExpression="company_name"
                                                                                UniqueName="company_name">
                                                                                <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                                                            </telerik:GridBoundColumn>
                                                                            <telerik:GridBoundColumn DataField="sent_date" HeaderText="วันที่ส่ง" DataFormatString="{0:dd/MM/yyyy}"
                                                                                ReadOnly="True" SortExpression="sent_date" UniqueName="sent_date">
                                                                                <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                            </telerik:GridBoundColumn>
                                                                            <telerik:GridBoundColumn DataField="CheckDoc_Date" HeaderText="วันที่ตรวจสอบ" DataFormatString="{0:dd/MM/yyyy}"
                                                                                ReadOnly="True" SortExpression="CheckDoc_Date" UniqueName="CheckDoc_Date">
                                                                                <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                            </telerik:GridBoundColumn>
                                                                            <telerik:GridBoundColumn DataField="reference_code1" HeaderText="เลขที่รับคำร้อง"
                                                                                ReadOnly="True" SortExpression="reference_code1" UniqueName="reference_code1">
                                                                                <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                            </telerik:GridBoundColumn>
                                                                            <telerik:GridBoundColumn DataField="PrintFormDate" HeaderText="วันที่พิมพ์ฟอร์ม"
                                                                                DataFormatString="{0:dd/MM/yyyy}" ReadOnly="True" SortExpression="PrintFormDate"
                                                                                UniqueName="PrintFormDate">
                                                                                <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                            </telerik:GridBoundColumn>
                                                                            <telerik:GridBoundColumn DataField="edi_status_id" HeaderText="ผลการตรวจสอบ" ReadOnly="True"
                                                                                SortExpression="edi_status_id" UniqueName="edi_status_id">
                                                                                <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                            </telerik:GridBoundColumn>
                                                                            <telerik:GridBoundColumn DataField="CheckDoc_By" HeaderText="ผู้ตรวจ" ReadOnly="True"
                                                                                SortExpression="CheckDoc_By" UniqueName="CheckDoc_By">
                                                                                <HeaderStyle Width="15px" HorizontalAlign="Center" />
                                                                                <ItemStyle Width="15px" HorizontalAlign="Center" />
                                                                            </telerik:GridBoundColumn>
                                                                            <telerik:GridBoundColumn DataField="SentType" HeaderText="&nbsp;" ReadOnly="True"
                                                                                SortExpression="SentType" UniqueName="SentType">
                                                                                <ItemStyle Font-Names="Tahoma" ForeColor="Gray" Font-Size="8pt" />
                                                                            </telerik:GridBoundColumn>
                                                                        </Columns>
                                                                    </MasterTableView>
                                                                    <HeaderStyle HorizontalAlign="Center" Font-Names="Tahoma" Font-Size="10pt" />
                                                                    <ClientSettings EnableRowHoverStyle="True" ReorderColumnsOnClient="True" AllowDragToGroup="True"
                                                                    AllowColumnsReorder="True">
                                                                        <Selecting AllowRowSelect="True"></Selecting>
                                                                    </ClientSettings>
                                                                    <GroupingSettings ShowUnGroupButton="true" />
                                                                </telerik:RadGrid>
                                                                <asp:ImageButton ID="ImageButton1" ImageUrl="/DesktopModules/DFT_EDI_CheckAttachment/images/excel.gif"
                                                                    runat="server" Style="float: right; margin-top: 5px;" AlternateText="Export"
                                                                    ToolTip="Export" />
                                                                <br />
                                                                <div style="margin-bottom: 5px; font-size: 10pt;">
                                                                    สรุปจำนวนคำขอฟอร์มแยกตามประเภทของฟอร์ม</div>
                                                                <asp:Label ID="lblSummary" runat="server" Text=""></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
</td> </tr> </table>
<asp:Label ID="lblRoleID" runat="server" Visible="False"></asp:Label>