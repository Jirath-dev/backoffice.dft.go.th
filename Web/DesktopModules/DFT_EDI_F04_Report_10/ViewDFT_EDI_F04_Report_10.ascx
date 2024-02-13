<%@ Control Language="vb" Inherits="DFT_EDI_F04_Report_10.ViewDFT_EDI_F04_Report_10" AutoEventWireup="false" Explicit="True" CodeBehind="ViewDFT_EDI_F04_Report_10.ascx.vb" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<link href="CSS/skin.css" rel="stylesheet" type="text/css" />
<script src="/js/jquery/jquery.js"></script>
<script src="/js/jquery/jquery-ui.js"></script>
<link href="/js/jquery/jquery-ui.css" rel="stylesheet" />
<telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        jQuery.noConflict();

    </script>
</telerik:RadCodeBlock>
<div>
    <h3>ระบบรายงานการเข้าใช้บริการกรมการค้าต่างประเทศ</h3>
</div>
<table class="FormLabel">
    <tr>
        <td>ตั้งแต่วันที่ :
        </td>
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
        <td>ถึง :
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
    </tr>
    <tr>
        <td>สาขา :
        </td>
        <td colspan="2">
            <asp:DropDownList ID="ddlSiteID" runat="server">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td>ประเภท :
        </td>
        <td colspan="2">
            <asp:RadioButtonList runat="server" ID="rdoUserType" RepeatDirection="Horizontal">
                <asp:ListItem Value ="0" Text="รายบุคคล" Selected="True"></asp:ListItem>
                <asp:ListItem Value ="1" Text="รายบริษัท"></asp:ListItem>
            </asp:RadioButtonList>
        </td>
    </tr>
</table>
<div style="margin: 3px;">
    <asp:Button ID="btnSearch" runat="server" Text="ค้นหา" Width="100px" />
    <asp:Button ID="btnExcel" runat="server" Text="Export Execl" Width="100px" />
    <asp:Button ID="btnPDF" runat="server" Text="Export PDF" Width="100px" />
</div>
<div style="margin: 5px;">
    <telerik:RadGrid ID="rgRequestForm" runat="server" AllowPaging="True" GridLines="None"
        PageSize="20" Skin="Office2007" TabIndex="-1" Width="100%" ShowStatusBar="True"
        AllowSorting="True" groupingsettings-retaingroupfootersvisibility="true">
        <MasterTableView AutoGenerateColumns="False" ClientDataKeyNames="" NoMasterRecordsText="ไม่มีรายการคำร้อง"
            Width="100%" PagerStyle-AlwaysVisible="true" PagerStyle-FirstPageToolTip="หน้าแรก"
            PagerStyle-LastPageToolTip="หน้าสุดท้าย" PagerStyle-NextPageToolTip="หน้าต่อไป"
            PagerStyle-PageSizeLabelText="แสดงหน้าละ" PagerStyle-PrevPageToolTip="ก่อนหน้านี้"
            PagerStyle-PagerTextFormat="{4}  หน้า {0} จาก {1} (รายการที่ {2} ถึง {3} จากทั้งหมด {5})">
            <Columns>
                <telerik:GridBoundColumn DataField="card_id" HeaderText="เลขที่บัตร/เลขผู้เสียภาษี" ReadOnly="True"
                    SortExpression="card_id" UniqueName="card_id">
                    <HeaderStyle Width="15px" HorizontalAlign="Center" />
                    <ItemStyle Width="15px" HorizontalAlign="center" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="AuthName_Thai" HeaderText="ชื่อผู้มาใช้บริการ" ReadOnly="True"
                    SortExpression="AuthName_Thai" UniqueName="AuthName_Thai">
                    <HeaderStyle Width="15px" HorizontalAlign="center" />
                    <ItemStyle Width="15px" HorizontalAlign="left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="site_name" HeaderText="สาขา" ReadOnly="True"
                    SortExpression="site_name" UniqueName="site_name">
                    <HeaderStyle Width="15px" HorizontalAlign="Center" />
                    <ItemStyle Width="15px" HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="active_date" HeaderText="วันที่มาใช้บริการ" ReadOnly="True" visible="false"
                    SortExpression="active_date" UniqueName="active_date">
                    <HeaderStyle Width="15px" HorizontalAlign="Center" />
                    <ItemStyle Width="15px" HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridTemplateColumn HeaderText="วันที่มาใช้บริการ" >
				<HeaderStyle Width="15px" HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label ID="lblactive_date" runat="server" Text='<%#Eval("active_date") %>'></asp:Label>
                        <%--<asp:Label ID="Label1" runat="server" Text='<%#ConvertToThaiDate(Eval("active_date")) %>'></asp:Label>--%>
                    </ItemTemplate>
                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="center" />
                </telerik:GridTemplateColumn>
            </Columns>
        </MasterTableView>
        <HeaderStyle HorizontalAlign="Center" Font-Names="Tahoma" Font-Size="10pt" />
        <ClientSettings EnableRowHoverStyle="True" ReorderColumnsOnClient="True" AllowDragToGroup="True"
            AllowColumnsReorder="True">
            <Selecting AllowRowSelect="True"></Selecting>
        </ClientSettings>
    </telerik:RadGrid>
</div>
<div style="text-align: center; margin: 5px;">
    <asp:Label ID="lbl_error_msg" runat="server" Visible="false" Font-Size="Large"></asp:Label>
</div>
