<%@ Page Language="vb" AutoEventWireup="false" Codebehind="BL.aspx.vb" Inherits=".BL" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ข้อมูลใบตราส่งสินค้า(B/L หรือ AWB หรือ FCR)</title>
    <style type="text/css">
        body { font-family:Tahoma;font-size:10pt; background:#666666; }
        .tb-data { border:0px solid #cccccc; }
        .tb-data td { padding:3px; border-bottom:1px dotted #cccccc;  }
        
        .tb-data1 { border:1px solid #cccccc; border-bottom:0px; background:#efefef; }
        .tb-data1 td { padding:3px; border-bottom:1px solid #cccccc; }
        
.tb-data2 { font-size:8pt;border:1px solid #999999;border-left:0px; border-bottom:0px; }  
        .tb-data2 td { vertical-align:top; padding:3px; border-bottom:1px solid #999999; padding-left:5px; border-left:1px solid #999999; }
        .header { background:#F9F; font-weight:bold; vertical-align:top; }
        td .header { border-bottom:3px solid #999999; }
        .data { color:Blue; font-weight:bold;}
        .box { border-bottom:1px solid #cccccc; border-right:0px; }
        .quantity { color:#666666; margin-top:3px; }
        .product-name { margin-bottom:3px; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div style="color: #cccccc; background: #333333; text-align: center; padding: 10px;
            border: 1px solid #666666; border-bottom: 0px;">
            <h3 style="margin: 0px;">
                ข้อมูลใบตราส่งสินค้า (<asp:Label ID="lblBLType" runat="server" Text="B/L"></asp:Label>)</h3>
        </div>
        <asp:DataList ID="dlBL" runat="server" Width="100%">
            <ItemTemplate>
                <div style="background: #ffffff; padding: 15px; margin-bottom: 15px;">
                    <div style="margin-bottom: 8px; text-align: right; font-size: 10px;">
                        <asp:Label ID="Label2" CssClass="data" Text='<%# Eval("document_owner") %>' runat="server"></asp:Label>
                        <asp:Label ID="Label3" CssClass="data" Text='<%# Eval("document_no") %>' runat="server"></asp:Label></div>
                    <table cellpadding="0" class="tb-data1" cellspacing="0" width="100%" style="margin-bottom: 10px;">
                        <tr>
                            <td width="150">
                                <b>
                                    <asp:Label ID="lblBLTypeText" runat="server" Text='<%# Eval("bl_type") %>'></asp:Label></b></td>
                            <td>
                                <asp:Label ID="lblBLNo" CssClass="data" Text='<%# Eval("bl_no") %>' runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="box" style="background: #ffffff">
                                Date of issue:</td>
                            <td class="box" style="background: #ffffff">
                                <asp:Label ID="lblDateIssue" runat="server" Text='<%# Eval("issue_date", "{0:dd/MM/yyyy}") %>'
                                    CssClass="data"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="box" style="background: #ffffff">
                                Shipped on board date:</td>
                            <td class="box" style="background: #ffffff">
                                <asp:Label ID="lblBLDate" CssClass="data" Text='<%# Eval("onboard_date","{0:dd/MM/yyyy}") %>'
                                    runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <table cellpadding="0" class="tb-data" cellspacing="0" width="100%">
                        <tr>
                            <td width="150">
                                Goods consigned from (Exporter&#39;s business name, address, country):</td>
                            <td>
                                <asp:Label ID="lblExporter" runat="server" Text='<%# Eval("shipper") %>' CssClass="data"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <%#Eval("dest_remark") %>
                            </td>
                            <td class="data">
                                <%#Eval("ob_address") %>
                            </td>
                        </tr>
                        <tr>
                            <td width="150">
                                Goods consigned to (Consignee&#39;s name, address, country):</td>
                            <td>
                                <asp:Label ID="lblConsign" runat="server" Text='<%# Eval("consignee") %>' CssClass="data"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Final Destination<br />
                                (Notify party):</td>
                            <td class="data">
                                <asp:Label ID="lblFinalDest" Text='<%# Eval("final_destination") %>' runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="border-top: 1px solid #cccccc;">
                                Port of Loading:</td>
                            <td style="border-top: 1px solid #cccccc;">
                                <asp:Label ID="lblPortLoading" runat="server" Text='<%# Eval("port_of_load") %>'
                                    CssClass="data"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Port of Discharge:</td>
                            <td>
                                <asp:Label ID="lblPortDis" runat="server" Text='<%# Eval("port_of_discharge") %>'
                                    CssClass="data"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Place of Delivery:</td>
                            <td class="data">
                                <asp:Label ID="lblPortDeli" runat="server" Text='<%# Eval("place_of_delivery") %>'
                                    CssClass="data"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="border-top: 1px solid #cccccc; border-bottom: 0px;">
                                Vessel&#39;s name/<br />
                                Aircraft etc.</td>
                            <td class="data" style="border-top: 1px solid #cccccc; border-bottom: 0px;">
                                <asp:Label ID="lblTransBy" Text='<%# Eval("vasel_name") %>' runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <!--table width="100%" border="0" cellpadding="0" cellspacing="0" class="tb-data2" style="margin-top:15px;margin-bottom:15px;">
<tr>
   
    <td class="header">Item<br/>No.</td><td class="header" width="100">Marks and<br />
    numbers of<br />packages</td>
     <td class="header">Number and kind of packages;<br/>description of goods</td>
     <td class="header">Net weight</td>
    <td class="header">Gross weight</td><td class="header">#1</td><td class="header">#2</td><td class="header">#3</td><td class="header">#4</td><td class="header">#5</td>
        </tr-->
                    <%#GetBLProducts(Eval("invh_run_auto"), Eval("bl_no"),Eval("total_quantity1"), Eval("total_quantity2"), Eval("total_quantity3"), Eval("total_quantity4"), Eval("total_quantity5"))%>
                    <!--/table-->
                    <table cellpadding="0" cellspacing="0" class="tb-data2" style="background: #efefef;">
                        <tr>
                            <td width="130">
                                Total Net Weight:</td>
                            <td>
                                <asp:Label ID="lblNetWeight" runat="server" CssClass="data" Text='<%# Eval("total_net_weight","{0:#,###.##0}") %>'></asp:Label>
                                &nbsp;<asp:Label ID="Label1" runat="server" Text='<%# Eval("total_net_weight_unit") %>'></asp:Label></td>
                        </tr>
                        <tr>
                            <td>
                                Total Gross Weight:</td>
                            <td>
                                <asp:Label ID="lblGrossWeight" runat="server" CssClass="data" Text='<%# Eval("total_gross_weight","{0:#,###.##0}") %>'></asp:Label>
                                &nbsp;<asp:Label ID="lblGrossWeightUnit" runat="server" Text='<%# Eval("total_gross_weight_unit") %>'></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Total Quantity #1:</td>
                            <td>
                                <asp:Label ID="lblTotalQuantity1" runat="server" CssClass="data" Text='<%# Eval("total_quantity1", "{0:#,##0.000}") %>'></asp:Label>
                                &nbsp;<asp:Label ID="lblTotalQuantity1Unit" runat="server" Text='<%# Eval("total_quantity1_unit") %>'></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Total Quantity #2:</td>
                            <td>
                                <asp:Label ID="lblTotalQuantity2" runat="server" CssClass="data" Text='<%# Eval("total_quantity2", "{0:#,##0.000}") %>'></asp:Label>
                                &nbsp;<asp:Label ID="lblTotalQuantity2Unit" runat="server" Text='<%# Eval("total_quantity2_unit") %>'></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Total Quantity #3:</td>
                            <td>
                                <asp:Label ID="lblTotalQuantity3" runat="server" CssClass="data" Text='<%# Eval("total_quantity3", "{0:#,##0.000}") %>'></asp:Label>
                                &nbsp;<asp:Label ID="lblTotalQuantity3Unit" runat="server" Text='<%# Eval("total_quantity3_unit") %>'></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Total Quantity #4:</td>
                            <td>
                                <asp:Label ID="lblTotalQuantity4" runat="server" CssClass="data" Text='<%# Eval("total_quantity4", "{0:#,##0.000}") %>'></asp:Label>
                                &nbsp;<asp:Label ID="lblTotalQuantity4Unit" runat="server" Text='<%# Eval("total_quantity4_unit") %>'></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Total Quantity #5:</td>
                            <td>
                                <asp:Label ID="lblTotalQuantity5" runat="server" CssClass="data" Text='<%# Eval("total_quantity5", "{0:#,##0.000}") %>'></asp:Label>
                                &nbsp;<asp:Label ID="lblTotalQuantity5Unit" runat="server" Text='<%# Eval("total_quantity5_unit") %>'></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <br />
                </div>
            </ItemTemplate>
        </asp:DataList>
    </form>
</body>
</html>
