<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Invoice.aspx.vb" Inherits=".Invoice" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>ข้อมูลใบกำกับสินค้า(Invoice)</title>
    <style type="text/css">
        body { font-family:Tahoma;font-size:10pt; background:#666666; }
        .tb-data { border:0px solid #cccccc; }
        .tb-data td { padding:3px; border-bottom:1px dotted #cccccc;  }
        
        .tb-data1 { border:1px solid #cccccc; border-bottom:0px; background:#efefef; }
        .tb-data1 td { padding:3px; border-bottom:1px solid #cccccc; }
        
        .tb-data2 { font-size:8pt;border:1px solid #999999;border-left:0px; border-bottom:0px; }
        .tb-data2 td { vertical-align:top; padding:3px; border-bottom:1px solid #999999; padding-left:5px; border-left:1px solid #999999; }
        .header { background:#99CCFF; font-weight:bold; vertical-align:top; }
        td .header { border-bottom:3px solid #999999; }
        .data { color:Blue; font-weight:bold; }
        .box { border-bottom:1px solid #cccccc; border-right:0px; }
        .quantity { color:#666666; margin-top:3px; }
        .product-name { margin-bottom:10px; }
        .product-title { margin-bottom:10px; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    
    <div style="color:#cccccc;background:#333333;text-align:center;padding:10px;border:1px solid #666666;border-bottom:0px;"><h3 style="margin:0px;">ข้อมูลใบกำกับสินค้า (Invoice)</h3></div>
    
        <asp:DataList ID="dlInvoice" runat="server" Width="100%">
        <ItemTemplate>
        <div style="background:#ffffff;padding:15px;margin-bottom:15px;">
        <div style="margin-bottom:5px;font-size:8pt;text-align:center;color:#666666;">ใบกำกับสินค้า ลำดับที่ #<asp:Label ID="lblInvNo" Text='<%# Container.ItemIndex + 1 %>' runat="server"></asp:Label></div>
         <table cellpadding="0" class="tb-data1" cellspacing="0" width="100%" style="margin-bottom:10px;">
    <tr>
        <td width="150"><b>Invoice No.:</b></td>
        <td>
            <asp:Label ID="lblInvoiceNo" CssClass="data" Text='<%# Eval("invoice_no") %>' runat="server"></asp:Label>
                </td>
    </tr>
    <tr>
        <td class="box" style="background:#ffffff">Invoice Date:</td>
        <td class="box" style="background:#ffffff"><asp:Label ID="lblInvoiceDate" CssClass="data" Text='<%# Eval("invoice_date", "{0:dd/MM/yyyy}") %>' runat="server"></asp:Label></td>
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
            <td width="150">
                Goods consigned to
                <br />
                (Consignee&#39;s name, address, country):</td>
            <td>
                <asp:Label ID="lblConsign" runat="server" CssClass="data" 
                    Text='<%# Eval("destination_address") %>'></asp:Label>
            </td>
        </tr>
    <tr>
        <td style="border-top:1px solid #cccccc;">Port of Loading:</td>
        <td style="border-top:1px solid #cccccc;">
            <asp:Label ID="lblPortLoading" runat="server"  Text='<%# Eval("from_port") %>' CssClass="data"></asp:Label>
                    </td>
    </tr>
    <tr>
        <td>Port of Discharge:</td>
        <td>
            <asp:Label ID="lblPortDis" runat="server"  Text='<%# Eval("port_discharge") %>' CssClass="data"></asp:Label>
                    </td>
    </tr>
    <tr>
        <td style="border-top:1px solid #cccccc;border-bottom:none;">Vessel&#39;s name/<br/>Aircraft etc.</td>
        <td style="border-top:1px solid #cccccc;border-bottom:none;" class="data">
            <asp:Label ID="lblTransBy" runat="server" Text='<%# Eval("vasel_name") %>' CssClass="data"></asp:Label>
                    </td>
    </tr>
    <tr>
        <td style="background:#ffffff;border-top:1px solid #999999;">Shipped on Board:</td>
        <td style="background:#ffffff;border-top:1px solid #999999;">
            <asp:Label ID="lblETD" runat="server" Text='<%# Eval("etd", "{0:dd/MM/yyyy}") %>' CssClass="data"></asp:Label>
                    </td>
    </tr>
</table>

<!--table width="100%" border="0" cellpadding="0" cellspacing="0" class="tb-data2" style="margin-top:15px;margin-bottom:15px;">
<tr bgcolor="#99CCFF"><td class="header">Item<br />No.</td><td class="header" width="100">Marks and 
    <br />
        numbers
    of 
    <br />
    packages</td><td class="header">Number and kind of packages;<br/>description of goods</td>
    <td class="header">
        Net weight</td>
    <td class="header">
        Gross weight</td>
        <td class="header">Amount FOB<br />
                        (USD)</td><td class="header">#1</td><td class="header">#2</td><td class="header">#3</td><td class="header">#4</td><td class="header">#5</td></tr-->
<%#GetInvoiceProducts(Eval("invh_run_auto"), Eval("invoice_no"), Eval("total_quantity1"), Eval("total_quantity2"), Eval("total_quantity3"), Eval("total_quantity4"), Eval("total_quantity5"))%>
<!--/table-->
<table style="display:none;margin-top:20px;"><tr><td style="vertical-align:top"><table cellpadding="0" class="tb-data2" cellspacing="0" style="background:#efefef;">
    <tr>
        <td width="130">
            Total Net Weight:</td>
        <td>
            <asp:Label ID="lblNetWeight" CssClass="data" runat="server" Text='<%# Eval("total_net_weight","{0:#,###.##0}") %>'></asp:Label>
&nbsp;<asp:Label ID="Label3" runat="server" Text='<%# Eval("total_net_weight_unit") %>'></asp:Label></td>
    </tr>
    <tr>
        <td>
            Total Gross Weight:</td>
        <td>
            <asp:Label ID="lblGrossWeight" CssClass="data" runat="server" Text='<%# Eval("total_gross_weight","{0:#,###.##0}") %>'></asp:Label>
&nbsp;<asp:Label ID="Label2" runat="server" Text='<%# Eval("total_gross_weight_unit") %>'></asp:Label></td>
    </tr>

    <tr>
        <td>
            Total Quantity #1:</td>
        <td>
            <asp:Label ID="lblTotalQuantity1" CssClass="data" runat="server" Text='<%# Eval("total_quantity1","{0:#,##0}") %>'></asp:Label>
            &nbsp;<asp:Label ID="lblTotalQuantity1Unit" runat="server" Text='<%# Eval("total_quantity1_unit") %>'></asp:Label></td>
    </tr>
    <tr>
        <td>
            Total Quantity #2:</td>
        <td>
            <asp:Label ID="lblTotalQuantity2" CssClass="data" runat="server" Text='<%# Eval("total_quantity2","{0:#,##0}") %>'></asp:Label>
            &nbsp;<asp:Label ID="lblTotalQuantity2Unit" runat="server" Text='<%# Eval("total_quantity2_unit") %>'></asp:Label></td>
    </tr>
    <tr>
        <td>
            Total Quantity #3:</td>
        <td>
            <asp:Label ID="lblTotalQuantity3" CssClass="data" runat="server" Text='<%# Eval("total_quantity3","{0:#,##0}") %>'></asp:Label>
            &nbsp;<asp:Label ID="lblTotalQuantity3Unit" runat="server" Text='<%# Eval("total_quantity3_unit") %>'></asp:Label></td>
    </tr>
    <tr>
        <td>
            Total Quantity #4:</td>
        <td>
            <asp:Label ID="lblTotalQuantity4" CssClass="data" runat="server" Text='<%# Eval("total_quantity4","{0:#,##0}") %>'></asp:Label>
            &nbsp;<asp:Label ID="lblTotalQuantity4Unit" runat="server" Text='<%# Eval("total_quantity4_unit") %>'></asp:Label></td>
    </tr>
    <tr>
        <td>
            Total Quantity #5:</td>
        <td>
            <asp:Label ID="lblTotalQuantity5" CssClass="data" runat="server" Text='<%# Eval("total_quantity5","{0:#,##0}") %>'></asp:Label>
            &nbsp;<asp:Label ID="lblTotalQuantity5Unit" runat="server" Text='<%# Eval("total_quantity5_unit") %>'></asp:Label></td>
    </tr>
    </table></td>
    <td style="vertical-align:top;">
    <table cellpadding="0" class="tb-data2" cellspacing="0" style="background:#efefef;margin-left:20px;">
    <tr>
        <td width="130">
            Total FOB:</td>
        <td>
            <asp:Label ID="lblTotalFOB" runat="server" CssClass="data" Text='<%# Eval("total_fob","{0:#,###.#0}") %>'></asp:Label>
&nbsp;USD</td>
    </tr>
    <tr>
        <td>
            CIF:</td>
        <td>
            <asp:Label ID="lblTotalCIF" runat="server" CssClass="data" Text='<%# Eval("total_cif","{0:#,##0.#0}") %>'></asp:Label>
&nbsp;USD</td>
    </tr>
    <tr>
        <td>
            CFR:</td>
        <td>
            <asp:Label ID="lblTotalCFR" runat="server" CssClass="data" Text='<%# Eval("total_cfr","{0:#,##0.#0}") %>'></asp:Label>
&nbsp;USD</td>
    </tr>
    </table></td></tr></table>
    
    <br />
    </div>
     </ItemTemplate>
   </asp:DataList>
   
   <div style="background:#ffffcc;padding:15px;margin-bottom:15px;">
   <b>สรุปข้อมูลทั้งหมด :</b>
   <table style="margin-top:10px;"><tr><td style="vertical-align:top"><table cellpadding="0" class="tb-data2" cellspacing="0" style="background:#ffffff;">
    <tr>
        <td width="130">
            Total Net Weight:</td>
        <td>
            <asp:Label ID="lblSumNetWeight" CssClass="data" runat="server" Text='0'></asp:Label>
&nbsp;<asp:Label ID="lblSumNetWeightUnit" runat="server" Text=''></asp:Label></td>
    </tr>
    <tr>
        <td>
            Total Gross Weight:</td>
        <td>
            <asp:Label ID="lblSumGrossWeight" CssClass="data" runat="server" Text='0'></asp:Label>
&nbsp;<asp:Label ID="lblSumGrossWeightUnit" runat="server" Text=''></asp:Label></td>
    </tr>
     <tr>
        <td>
            Total Quantity #1:</td>
        <td>
            <asp:Label ID="lblSumQuantity1" CssClass="data" runat="server" Text='0'></asp:Label>
            &nbsp;<asp:Label ID="lblSumQuantity1Unit" runat="server" Text=''></asp:Label></td>
    </tr>
    <tr>
        <td>
            Total Quantity #2:</td>
        <td>
            <asp:Label ID="lblSumQuantity2" CssClass="data" runat="server" Text='0'></asp:Label>
            &nbsp;<asp:Label ID="lblSumQuantity2Unit" runat="server" Text=''></asp:Label></td>
    </tr>
    <tr>
        <td>
            Total Quantity #3:</td>
        <td>
            <asp:Label ID="lblSumQuantity3" CssClass="data" runat="server" Text='0'></asp:Label>
            &nbsp;<asp:Label ID="lblSumQuantity3Unit" runat="server" Text=''></asp:Label></td>
    </tr>
    <tr>
        <td>
            Total Quantity #4:</td>
        <td>
            <asp:Label ID="lblSumQuantity4" CssClass="data" runat="server" Text='0'></asp:Label>
            &nbsp;<asp:Label ID="lblSumQuantity4Unit" runat="server" Text=''></asp:Label></td>
    </tr>
    <tr>
        <td>
            Total Quantity #5:</td>
        <td>
            <asp:Label ID="lblSumQuantity5" CssClass="data" runat="server" Text='0'></asp:Label>
            &nbsp;<asp:Label ID="lblSumQuantity5Unit" runat="server" Text=''></asp:Label></td>
    </tr>
    
    </table></td>
    <td style="vertical-align:top;">
    <table cellpadding="0" class="tb-data2" cellspacing="0" style="background:#ffffff;margin-left:20px;">
    <tr>
        <td width="130">
            FOB:</td>
        <td>
            <asp:Label ID="lblSumFOB" runat="server" CssClass="data" Text='0'></asp:Label>
&nbsp;USD</td>
    </tr>
    <tr>
        <td>
            CIF:</td>
        <td>
            <asp:Label ID="lblSumCIF" runat="server" CssClass="data" Text='0'></asp:Label>
&nbsp;USD</td>
    </tr>
    <tr>
        <td>
            CFR:</td>
        <td>
            <asp:Label ID="lblSumCFR" runat="server" CssClass="data" Text='0'></asp:Label>
&nbsp;USD</td>
    </tr>
    </table></td></tr></table>
    <p>
        <asp:HiddenField ID="hSumPackagePerInvoice" Value="0" runat="server" />
       </p>
    <p>&nbsp;</p>
   </div>
    </form>
</body>
</html>
