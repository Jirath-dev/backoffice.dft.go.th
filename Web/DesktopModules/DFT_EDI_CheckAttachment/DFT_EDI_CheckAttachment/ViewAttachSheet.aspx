<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ViewAttachSheet.aspx.vb"
    Inherits=".ViewAttachSheet" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Cache-Control" content="no-cache">
    <meta http-equiv="Pragma" content="no-cache">
    <meta http-equiv="Expires" content="0">
    <title>View Attach Sheet Form D</title>
    <style type="text/css">
        body {
            font-size: 10pt;
            font-family: Tahoma,Sans-Serif, Verdana;
        }

        .rgAltRow td.selected {
            background: #CCFF66;
        }

        .rgRow td.selected {
            background: #CCFF66;
        }

        html, body, form, iframe {
            height: 100%;
            margin: 0px;
            padding: 0px;
            overflow: hidden;
        }
    </style>
</head>
<body onblur="window.focus();">
    <form id="form1" runat="server">
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>
        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
            <ajaxsettings>
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
            </ajaxsettings>
        </telerik:RadAjaxManager>
        <div id="ParentDivElement" style="height: 100%;">
            <telerik:RadSplitter ID="RadSplitter1" runat="server" VisibleDuringInit="False" Width="100%"
                Height="100%" LiveResize="True">
                <telerik:RadPane ID="RadPane1" runat="server" Width="50%" Height="100%">
                    <asp:Panel ID="Panel1" Style="margin: 10px;" runat="server" class="label-msg" Visible="true">
                        <div style="text-align: center">
                            <asp:Label ID="lblCompanyName" runat="server" Text=""></asp:Label>
                        </div>
                        <div style="text-align: center; margin: 5px; font-weight: bold;">
                            ATTACHED SHEET<br />
                            FORM D
                        </div>
                        <div style="text-align: right; margin: 5px; display: none;">
                            <asp:Label ID="lblInvoice" runat="server" Text="">INVOICE NO.:</asp:Label>
                        </div>
                        <telerik:RadGrid ID="GridData" PageSize="15" Skin="Office2007" runat="server" GridLines="None"
                            AllowPaging="True">
                            <mastertableview autogeneratecolumns="false">
                                <Columns>
                                    <telerik:GridTemplateColumn HeaderText="NO." DataField="CaseId" UniqueName="CaseId">
                                        <HeaderStyle HorizontalAlign="Center" Width="40" />
                                        <ItemStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <%#(gridData.PageSize * gridData.CurrentPageIndex) + Container.ItemIndex + 1%>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn HeaderText="PART NAME" DataField="Part_Name" UniqueName="Part_Name">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Left" Width="180" />
                                        <ItemTemplate>
                                           <%-- <a href="#" onclick="changePdf(this,'<%#Eval("CertOfOrgin_No")%>');">
                                                <%#Eval("Part_Name")%>
                                            </a>--%>
                                            <a href="#" onclick="openCertData('<%#Eval("CertOfOrgin_No")%>','<%#Eval("Tax_no")%>','<%#Eval("Country")%>');">
                                                <%#Eval("Part_Name")%>
                                            </a>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn DataField="HS_Code" HeaderText="H.S. CODE" UniqueName="column4">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" Width="80" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn HeaderText="Origin Criterion" UniqueName="Origin Criterion">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" Width="60" />
                                        <ItemTemplate>
                                            <%--<asp:Label ID="Origin" runat="Server" Text='<%#CDec(Eval("Asean_Content")).ToString("#,##0.00")&" %" %>'></asp:Label>--%>
                                            <asp:Label ID="Origin" runat="Server" Text='<%#ConvertOrigin(Eval("Asean_Content"))%>'></asp:Label>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn HeaderText="Gross Weight" UniqueName="Gross_Weight">
                                        <HeaderStyle HorizontalAlign="Center" Width="60" />
                                      <ItemStyle HorizontalAlign="Center" Width="60"/>
                                        <ItemTemplate>
                                            <asp:Label ID="Gross_Weight" runat="Server" Text='<%#CDec(Eval("Gross_Weight")).ToString("#,##0.000") %>'></asp:Label>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn HeaderText="FOB" UniqueName="FOB">
                                        <HeaderStyle HorizontalAlign="Center" Width="60"/>
                                        <ItemStyle HorizontalAlign="Center" Width="60" />
                                        <ItemTemplate>
                                            <asp:Label ID="FOB" runat="Server" Text='<%#CDec(Eval("FOB")).ToString("#,##0.00") %>'></asp:Label>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn HeaderText="FOB" UniqueName="FOB">
                                        <HeaderStyle HorizontalAlign="Center" Width="60"/>
                                        <ItemStyle HorizontalAlign="Center" Width="60" />
                                        <ItemTemplate>
                                            <asp:Label ID="FOB_Third" runat="Server" Text='<%#CDec(Eval("USD_Invoice_New")).ToString("#,##0.00") %>'></asp:Label>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn DataField="Certoforgin_No" HeaderText="เลขที่ต้นทุน" UniqueName="column4x">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" Width="100" />
                                    </telerik:GridBoundColumn>
                                </Columns>
                            </mastertableview>
                            <clientsettings enablerowhoverstyle="True">
                                <Selecting AllowRowSelect="True" />
                            </clientsettings>
                        </telerik:RadGrid>
                        <br />
                        <asp:Label runat="server" ID="lblTotal"></asp:Label>
                    </asp:Panel>
                    <p align="center">
                        <input type="button" onclick="javascript: window.close(); return false;" value="ปิดหน้าต่าง" />
                    </p>
                </telerik:RadPane>
                <telerik:RadSplitBar ID="RadSplitBar1" runat="server" CollapseMode="Both" />
                <telerik:RadPane ID="RadPane2" runat="server" Width="50%" Height="100%">

                    <script type="text/javascript">
                        function openCertData(Certno, taxno, Country) {

                            var url = "ViewCertDetail.aspx?certno=" + Certno + "&TAXNO=" + taxno + "&COUNTRY=" + Country;
                            var myFrame = document.getElementById('PDFFrame');

                            myFrame.src = url;

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

                        function changePdf(obj, filename) {
                            try {
                                var td = obj.parentElement;
                                td.className = "selected";
                            } catch (e) {

                            }
                            var dd = new Date();
                            var myFrame = document.getElementById('PDFFrame');
                            //myFrame.src = "https://edi.dft.go.th/Portals/0/DocumentFiles/<%= LEFT(Request.QueryString("TaxNo"),10) %>/" + filename + ".pdf";
                    myFrame.src = "https://edi.dft.go.th/Portals/0/DocumentFiles/" + GetTaxId('<%= Request.QueryString("TaxNo") %>') + "/" + filename + ".pdf?v=" + dd.getTime();

                }
                    </script>

                    <div style="margin-left: 10px; height: 99%;">
                        <iframe id="PDFFrame" height="100%" width="100%" frameborder="0"></iframe>
                    </div>
                </telerik:RadPane>
            </telerik:RadSplitter>
        </div>
    </form>
</body>
</html>
