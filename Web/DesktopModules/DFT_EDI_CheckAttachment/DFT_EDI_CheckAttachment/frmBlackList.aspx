<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frmBlackList.aspx.vb" Inherits=".frmBlackList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>แสดงข้อมูล BlackList/WatchList</title>
    <link href="CSS/skin.css" rel="stylesheet" />
    <%--        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        </telerik:RadAjaxManager>--%>
    <telerik:radcodeblock id="RadCodeBlock2" runat="server">

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
    </telerik:radcodeblock>
</head>
<body>
    <form id="form1" runat="server">
        <telerik:radscriptmanager id="RadScriptManager1" runat="server">
        </telerik:radscriptmanager>
        <div class="m-frm-bdr" style="padding:1px;">
            <div >
                <table border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td nowrap class="groupboxheader">
                            <font class="groupbox">&#160;&#160;ข้อมูล (BlackList/WatchList)&#160;&#160;</font>
                        </td>
                        <telerik:radcodeblock id="RadCodeBlock3" runat="server">
                    <td style="background-image: url(<%= ResolveUrl("images") %>/CORNER.gif); background-repeat: no-repeat;">
                         &#160;&#160;&#160;&#160;
                    </td>
                    </telerik:radcodeblock>
                    </tr>
                </table>


            </div>
            <div style="margin-top:3px;">
                <table>
                    <tr>
                        <td>
                            <telerik:radgrid id="grdBlackList" runat="server" font-names="Tahoma" font-size="X-Small"
                                gridlines="None" skin="Office2007" tabindex="-1">
                                <MasterTableView AutoGenerateColumns="False" CellSpacing="-1" ClientDataKeyNames="Auto_Run"
                                    DataKeyNames="Auto_Run" NoMasterRecordsText="ไม่มีข้อมูล BlackList/WatchList">
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="CompanyName_Th" ReadOnly="True" SortExpression="CompanyName_Th"
                                            UniqueName="CompanyName_Th" HeaderText="ชื่อนิติบุคคล">
                                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Type_Mistake" ReadOnly="True" SortExpression="Type_Mistake"
                                            UniqueName="Type_Mistake" HeaderText="ประเภท">
                                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                        </telerik:GridBoundColumn>
                                         <telerik:GridTemplateColumn UniqueName="Date_Rec_Mistake" HeaderText="วันที่ระงับ">
                                                <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDate_Rec_Mistake" runat="server" Text='<%# set_dateFormat(Eval("Date_Rec_Mistake"))%>'></asp:Label>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn DataField="CountryName" ReadOnly="True" SortExpression="CountryName"
                                            UniqueName="CountryName" HeaderText="ประเทศ">
                                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="form_name" ReadOnly="True" SortExpression="form_name"
                                            UniqueName="form_name" HeaderText="ประเภทฟอร์ม">
                                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Product_Mistake" ReadOnly="True" SortExpression="Product_Mistake"
                                            UniqueName="Product_Mistake" HeaderText="สินค้า">
                                           <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="BlackList_Desc" ReadOnly="True" SortExpression="BlackList_Desc"
                                            UniqueName="BlackList_Desc" HeaderText="สาเหตุ">
                                            <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                        </telerik:GridBoundColumn>
                                    </Columns>
                                </MasterTableView>
                                <HeaderStyle HorizontalAlign="Center" Font-Names="Tahoma" Font-Size="10pt" />
                            </telerik:radgrid>
                        </td>
                    </tr>
                </table>
            </div>
            <div style="text-align: right; padding:3px;" >
                <asp:Button ID="btnCancel" runat="server" class="m-btn" OnClientClick="javascript:CancelEdit();return false;"
                    TabIndex="27" Text="ปิดหน้าต่าง" UseSubmitBehavior="False" Width="150px" CausesValidation="False" />&nbsp;
            </div>
        </div>
    </form>
</body>
</html>
