<%@ Page Language="vb" AutoEventWireup="false" Codebehind="frmReceipt.aspx.vb" Inherits=".frmReceipt" %>

<%@ Register Assembly="ActiveReports.Web, Version=5.3.1436.2, Culture=neutral, PublicKeyToken=cc4967777c49a3ff"
    Namespace="DataDynamics.ActiveReports.Web" TagPrefix="ActiveReportsWeb" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ใบเสร็จรับเงิน</title>
    <link href="css/skin.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <telerik:radscriptmanager id="RadScriptManager1" runat="server">
        </telerik:radscriptmanager>
        <telerik:radcodeblock id="RadCodeBlock2" runat="server">
            <script type="text/javascript">
                function CloseAndRebind(args) {
                    GetRadWindow().Close();
                    GetRadWindow().BrowserWindow.refreshGrid(args);
                    //GetRadWindow.setUrl("http://mysite.backoffice.dft.go.th/tabid/60/Default.aspx");
                }

                function GetRadWindow() 
                {
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
        <table border="0" cellpadding="0" cellspacing="5" width="100%">
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
                                                    <td align="left" class="m-frm-hdr">
                                                        <table border="0" cellpadding="0" cellspacing="0" cols="2">
                                                            <tr>
                                                                <td class="groupboxheader" nowrap="nowrap">
                                                                    <font class="groupbox">&nbsp; ใบเสร็จรับเงิน &nbsp;</font></td>
                                                                <telerik:radcodeblock id="RadCodeBlock1" runat="server">
                                                                    <td style='background-image: url(<%= ResolveUrl("images") %>/CORNER.gif); background-repeat: no-repeat;'>
                                                                        &nbsp; &nbsp;&nbsp;</td>
                                                                </telerik:radcodeblock>
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
                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                <tr>
                                                    <td>
                                                        <table border="0" cellpadding="0" cellspacing="2" width="100%">
                                                            <tr>
                                                                <td class="FormLabel">
                                                                    <font class="FormLabel">
                                                                        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                                                                            <tr>
                                                                                <td class="FormLabel" style="width: 30%">
                                                                                    เลขที่ใบเสร็จ :&nbsp;
                                                                                    <telerik:radtextbox id="txtReceiptNo" runat="server" cssclass="FormFld" enableembeddedskins="False"
                                                                                        readonly="True" tabindex="-1" text="AUTO" width="150px">
                                                                    </telerik:radtextbox>
                                                                                </td>
                                                                                <td class="FormLabel" style="width: 8%">
                                                                                </td>
                                                                                <td class="FormLabel">
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="FormLabel" colspan="3">
                                                                                    <table>
                                                                                        <tr>
                                                                                            <td class="FormLabel">
                                                                                                เครื่องพิมพ์&nbsp;
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:RadioButtonList ID="rdblReceiptPrinter" runat="server" RepeatDirection="horizontal" RepeatColumns="1">
                                                                                                </asp:RadioButtonList></td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </font>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <font class="FormLabel">วันที่ :&nbsp;</font>
                                                                    <telerik:raddatepicker id="txtReceiptDate" runat="server" culture="English (United States)"
                                                                        skin="Office2007" enabled="False" width="170px">
                                                                        <DatePopupButton HoverImageUrl="" ImageUrl="" />
                                                                        <Calendar Skin="Office2007" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                                            ViewSelectorText="x">
                                                                        </Calendar>
                                                                    </telerik:raddatepicker>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    &nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <telerik:radgrid id="rgReceiptList" runat="server" allowsorting="True" gridlines="None"
                                                                        showstatusbar="True" skin="Office2007" tabindex="-1" width="100%" showfooter="True">
                                                                        <MasterTableView AutoGenerateColumns="False" ClientDataKeyNames="invh_run_auto"
                                                                            DataKeyNames="invh_run_auto" NoMasterRecordsText="ไม่มีรายการคำร้อง" Width="100%">
                                                                            <Columns>
                                                                                <telerik:GridBoundColumn DataField="form_name" HeaderText="ชื่อฟอร์ม" ReadOnly="True"
                                                                                    SortExpression="form_name" UniqueName="form_name">
                                                                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt" />
                                                                                </telerik:GridBoundColumn>
                                                                                <telerik:GridBoundColumn DataField="invh_run_auto" HeaderText="เลขที่อ้างอิง" ReadOnly="True"
                                                                                    SortExpression="invh_run_auto" UniqueName="invh_run_auto">
                                                                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                                </telerik:GridBoundColumn>
                                                                                <telerik:GridBoundColumn DataField="reference_code2" HeaderText="เลขที่หนังสือรับรอง"
                                                                                    ReadOnly="True" SortExpression="reference_code2" UniqueName="reference_code2">
                                                                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                                </telerik:GridBoundColumn>
                                                                                <telerik:GridBoundColumn DataField="sent_date" DataFormatString="{0:dd/MM/yyyy}" HeaderText="วันที่ส่ง"
                                                                                    SortExpression="sent_date" UniqueName="sent_date">
                                                                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                                </telerik:GridBoundColumn>
                                                                                <telerik:GridBoundColumn DataField="totalPrintPage" HeaderText="จำนวนชุด" ReadOnly="True"
                                                                                    SortExpression="totalPrintPage" UniqueName="totalPrintPage">
                                                                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                                </telerik:GridBoundColumn>
                                                                                <telerik:GridBoundColumn DataField="price" HeaderText="ราคาต่อชุด" ReadOnly="True"
                                                                                    SortExpression="price" UniqueName="price" DataFormatString="{0:N}">
                                                                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Right" />
                                                                                </telerik:GridBoundColumn>
                                                                                <telerik:GridBoundColumn DataField="Amount" HeaderText="ราคาต่อชุด" ReadOnly="True"
                                                                                    SortExpression="Amount" UniqueName="Amount" DataFormatString="{0:N}">
                                                                                    <ItemStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Right" />
                                                                                </telerik:GridBoundColumn>
                                                                            </Columns>
                                                                        </MasterTableView>
                                                                        <HeaderStyle Font-Names="Tahoma" Font-Size="10pt" HorizontalAlign="Center" />
                                                                        <ClientSettings EnableRowHoverStyle="True">
                                                                            <Selecting AllowRowSelect="True" />
                                                                            <Scrolling AllowScroll="True" />
                                                                        </ClientSettings>
                                                                    </telerik:radgrid>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <table cellpadding="0" cellspacing="0">
                                                                        <tr>
                                                                            <td>
                                                                                <font class="FormLabel">วิธีการชำระเงิน : </font>&nbsp;</td>
                                                                            <td>
                                                                                <asp:RadioButtonList ID="rblPayType" runat="server" RepeatDirection="Horizontal">
                                                                                    <asp:ListItem Selected="True" Value="0">เงินสด</asp:ListItem>
                                                                                    <asp:ListItem Value="1">บัตรเครดิต</asp:ListItem>
                                                                                </asp:RadioButtonList>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <font class="FormLabel">หมายเหตุ :&nbsp;</font>
                                                                    <telerik:radtextbox id="txtRemark" runat="server" cssclass="FormFld" enableembeddedskins="False"
                                                                        readonly="True" tabindex="-1" width="300px">
                                                                    </telerik:radtextbox>
                                                                    &nbsp;
                                                                    <asp:Button ID="btnSave" Width="100px" runat="server" Text="บันทึก" />
                                                                    <asp:Button ID="btnCancel" Width="100px" runat="server" Text="ยกเลิก" /></td>
                                                            </tr>
                                                            <tr>
                                                                <td style="text-align: center">
                                                                    <asp:Label ID="lbl_ErrMSG" runat="server" Font-Names="Tahoma" Font-Size="10pt" ForeColor="Red"></asp:Label></td>
                                                            </tr>
                                                        </table>
                                                        <telerik:radtextbox id="txtCompanyName" runat="server" cssclass="FormFld" enableembeddedskins="False"
                                                            tabindex="-1" width="150px" visible="False">
                                                        </telerik:radtextbox>
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
        <activereportsweb:webviewer id="WebViewer1" runat="server" height="60%" width="100%"
            visible="False">
        </activereportsweb:webviewer>
    </form>
</body>
</html>
