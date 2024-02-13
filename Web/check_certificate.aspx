<!doctype html>
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>Digital Signature Demo</title>
<link type="text/css" href="css/hot-sneaks/jquery-ui-1.10.1.custom.css" rel="stylesheet">
	<script type="text/javascript" src="js/jquery-1.9.1.js"></script>
	<script type="text/javascript" src="js/jquery-ui-1.10.1.custom.js"></script>
<script language="javascript" type="text/javascript">  
$(function() {    
	$( "#tabs" ).tabs();  
});  
</script>

   <style type="text/css">
    body, table, td,ul,li,div, p { font-family: Tahoma;font-size:10pt;}
    .tb-info 
    {
        border-top:1px solid #cccccc; 
        margin-top:8px;
    }
    .tb-info tr td
    {
        border-bottom:1px solid #cccccc; 
        border-right:0px solid #cccccc; 
        padding:3px;
    }
</style>

    <script type="text/javascript">
        var user_agent = navigator.userAgent;

        function CheckCertInfo(){
        try{
            var objSign;
            var taxid;

            var txtTaxId = document.getElementById("txtTaxId");
            taxid= txtTaxId.value;

            if(taxid==""){
		window.alert("กรุณาใส่ข้อมูลเลขที่ผู้เสียภาษี (TAXID) ให้เรียบร้อยก่อน");
		txtTaxId.focus();
		return;
            }

            try {
                
                //check javascript
                var obj_msg1 = document.getElementById("msg1");
                obj_msg1.innerHTML = '<img src="Images/icon_check.gif"/> ติดตั้งเรียบร้อยแล้ว';
                obj_msg1.style.color = "#006600";
                
                //check plug in
                var obj_msg2 = document.getElementById("msg2");

                try {
                    objSign = new ActiveXObject("NTISignLib.Signature");

                    obj_msg2.innerHTML = '<img src="Images/icon_check.gif"/> ติดตั้งเรียบร้อยแล้ว (เวอร์ชั่น ' + objSign.Version + ')';
                    obj_msg2.style.color = "#006600";

                    try {
                        objSign.GetCertInfo(taxid);
                        var obj_msg3 = document.getElementById("msg3");
                        //alert(objSign.SerialNo);
                        if (objSign.SerialNo != '') {
                            obj_msg3.innerHTML = '<img src="Images/icon_check.gif"/> ติดตั้งเรียบร้อยแล้ว (Serial No. ' + objSign.SerialNo + ')';
                            obj_msg3.style.color = "#006600";
                            certinfo.innerHTML=CertificateInfo(objSign);
                        } else {
                        obj_msg3.innerHTML = '<div class="ui-widget"><div class="ui-state-error ui-corner-all" style="padding: 0 .7em;"><p><span class="ui-icon ui-icon-alert" style="float: left; margin-right: .3em;"></span><strong>Error:</strong> ไม่พบข้อมูลใบรับรองอิเล็กทรอนิกส์ของเลขที่ภาษี ' + taxid + '<br/>กรุณาติดตั้งใบรับรองอิเล็กทรอนิกส์(Digital Certificate) ของบริษัท ให้เรียบร้อยก่อน</p></div></div>';
                            obj_msg3.style.color = "#f00000";
							certinfo.innerHTML="";
                        }
                    } catch (e) {
                        obj_msg3.innerHTML = 'Error - '+e.message;
                        obj_msg3.style.color = "#f00000";
                    }
                    
                } catch (e) {
                obj_msg2.innerHTML = '<img src="Images/icon_delete.gif"/> กรุณาติดตั้งโปรแกรม Plug-in สำหรับลงลายมือชื่ออิเล็กทรอนิกส์ ให้เรียบร้อยก่อน';
                    obj_msg2.style.color = "#f00000";
                }
                
                    
            } catch (e) {
                var err_msg = e.message.toLowerCase();
                var err_msg01 = "automation server can";

                var result = err_msg.indexOf(err_msg01);

                if (result > -1) {
                    var obj_msg1 = document.getElementById("msg1");
                    obj_msg1.innerHTML = '<img src="Images/icon_delete.gif"/> เกิดข้อผิดพลาด !<br/>ไม่สามารถลงลายมือชื่ออิเล็กทรอนิกส์ได้ \nกรุณาตรวจสอบการติดตั้ง Digital Certificate และโปรแกรม Plug-in สำหรับลงลายมือชื่ออิเล็กทรอนิกส์ ให้พร้อมใช้งานให้เรียบร้อยก่อน\n(คำอธิบาย: ' + e.message + '.)';
                    obj_msg1.style.color = "#f00000";
                    var obj_msg2 = document.getElementById("msg2");
                    obj_msg2.innerHTML = "ให้ตรวจสอบข้อ 1 ให้ผ่านเรียบร้อยก่อน";
                    obj_msg2.style.color = "#f00000";
                } 
            }
            
        } catch (e) {
                alert('Error: เกิดข้อผิดพลาด!\n' + e.message);
            }

        }

        function CertificateInfo(objSign) {
            var cert_info = "";
            try {
                cert_info = "<b>รายละเอียดใบรับรองอิเล็กทรอนิกส์(Digital Certificate) ที่ติดตั้งไว้</b>";
                cert_info = cert_info + "<div style='margin-top:5px;'><table class='tb-info'><tr><td style='width:180px;background:#eeeeee;'>ชื่อบริษัท:</td><td style='background:#eeeeee;'>" + objSign.Company + "</td></tr>";
                cert_info = cert_info + "<tr><td style='background:#eeeeee;'>เลขผู้เสียภาษี:</td><td style='background:#eeeeee;'>" + objSign.TaxID + "</td></tr>";
                cert_info = cert_info + "<tr><td>Serial No:</td><td>" + objSign.SerialNo + "</td></tr>";
                var d = new Date(objSign.IssueDate);
                cert_info = cert_info + "<tr><td>วันที่ออกใบรับรอง:</td><td>" + d.toLocaleDateString() + "</td></tr>";
                var dd = new Date(objSign.ExpireDate);
                cert_info = cert_info + "<tr><td>วันที่หมดอายุ:</td><td>" + dd.toLocaleDateString() + "</td></tr>";
                var status = "";
                if (objSign.IsExpired==true){
                    status = "<font color='#f00000;'>Expired - หมดอายุ</font>";
                }else{
                    status = "<font color='#006600;'>ใช้งานได้ปกติ - ยังไม่หมดอายุ</font>";
                }
                cert_info = cert_info + "<tr><td>สถานะ:</td><td>" + status + "</td></tr>";
                cert_info = cert_info + "<tr><td>มี Private Key:</td><td>" + objSign.HasPrivateKey + "</td></tr>";
                cert_info = cert_info + "<tr><td>Subject:</td><td>" + objSign.Subject + "</td></tr>";
                cert_info = cert_info + "<tr><td>Issue By:</td><td>" + objSign.Issuer + "</td></tr>";
                cert_info = cert_info + "</table></div>";
            } catch (e) {
                return cert_info;
            }
             return cert_info;
        }
        function SignData() {
        try {
            var strData = document.getElementById("txtData").value;
            var taxid = document.getElementById("txtTaxId").value;
            var objTxtSignData = document.getElementById("txtSignData");
            objTxtSignData.value = "";
            if (strData != "") {
                    var objSign;
                    var objSign = new ActiveXObject("NTISignLib.Signature");
                    var result = objSign.SignString(taxid, strData);
                    if (result == "" || result.toUpperCase()=="FALSE") {
                        alert("ERROR: กรุณาตรวจสอบ Certificate ให้พร้อมใช้งานให้เรียบร้อยก่อน");
                        return false;
                    } else {
                       objTxtSignData.value = result;
                       return true;
                    }
                }else{
                    alert("ใส่ข้อความให้เรียบร้อย");
                }
        } catch (e) {
            alert('Error: ไม่สามารถลงลายมือชื่ออิเล็กทรอนิกส์ได้ \nกรุณาตรวจสอบ Certificate ให้พร้อมใช้งานให้เรียบร้อยก่อน');
            return false;
        }
    }
    function VerifyData() {
        try {
            var strData = document.getElementById("txtData").value;
            var strSignData = document.getElementById("txtSignData").value;
            if (strData != "") {
                var objSign;
                var objSign = new ActiveXObject("NTISignLib.Signature");
                var result = objSign.VerifyString(strData, strSignData);
                if (result==true) {
                    alert("OK: Verified");
                    return false;
                } else {
                    alert("ERROR: Invalid.");
                    return true;
                }
            } else {
                alert("ใส่ข้อความให้เรียบร้อย");
            }
        } catch (e) {
            alert('Error: ไม่ตรวจสอบความถูกต้องของข้อมูลได้');
            return false;
        }
    }
</script>
</head>
<body>
<div id="tabs">
<ul> 
<li><a href="#tabs-1">ติดตั้ง(Setup)</a></li>    
<li><a href="#tabs-2">ตรวจสอบ(Verify)</a></li> 
<li><a href="#tabs-3">ทดสอบ(Test)</a></li>
</ul>
<div id="tabs-1">
<b>ติดตั้งใบรับรองอิเล็กทรอนิกส์และโปรแกรม Plug-ins ที่เกี่ยวข้อง:</b><br/>
============================<br/>
<ul>
<li>ติตตั้งใบรับรองอิเล็กทรอนิกส์(Digital Certificate) หรือติดตั้ง USB Token ในเครื่องฯ ให้เรียบร้อยก่อน</li>
    <li>ติดตั้งโปรแกรม Plug-ins สำหรับลงลายมือชื่ออิเล็กทรอนิกส์  [<a href="http://edi.dft.go.th/download/SignNutCom.zip">ดาวน์โหลดได้ที่นี่...</a>]</li>
    <li>การกำหนดค่า Security Settings - Trusted site zone สำหรับ Internet Explore</li>
</ul>
</div>
<div id="tabs-2">
<b>ตรวจสอบ Digital Certificate:</b><br/>
============================<br/>
<br/>
    <div style="margin-top:10px;font-size:10pt;">
<div style="margin-bottom:5px;color:#666666;">ใส่เลขที่ผู้เสียภาษีของบริษัทตามที่ขอ Digital Certificate</div>
เลขประจำตัวผู้เสียภาษี(TaxId): <input type="text" value="<% Response.Write(Request.QueryString("taxid")) %>" name="txtTaxId" id="txtTaxId" /> 
<input type="button" value="Check" onClick="javascript:CheckCertInfo();" />
<br/><br/>
    <p><b>
    ข้อมูลตรวจสอบการติดตั้ง Digital Certificate และโปรแกรม Plug in 
    <br />
    สำหรับลงลายมือชื่ออิเล็กทรอนิกส์(Digital Signature)</b></p>
    <table class="tb-info">
    <tr><td style="background:#eeeeee;">&nbsp;</td><td style="background:#eeeeee;width:430px;"><b>ขั้นตอน</b></td><td style="background:#eeeeee;"><b>ผลการตรวจสอบ</b></td></tr>
    <tr><td valign="top" style="border-bottom:1px solid #cccccc;padding-bottom:5px;">1.</td><td valign="top" style="border-bottom:1px solid #cccccc;padding-bottom:5px;">การกำหนดค่า Security Settings - Trusted site zone 
        สำหรับ Internet Explore</td><td valign="top" style="border-bottom:1px solid #cccccc;padding-bottom:5px;"><div id="msg1"></div></td></tr>
        <tr>
            <td valign="top" style="border-bottom:1px solid #cccccc;padding-bottom:5px;">2.</td><td valign="top" style="border-bottom:1px solid #cccccc;padding-bottom:5px;">
        การติดตั้งโปรแกรม Plug in สำหรับลงลายมือชื่ออิเล็กทรอนิกส์</td><td valign="top" style="border-bottom:1px solid #cccccc;padding-bottom:5px;">
            <div id="msg2">-&nbsp;</div></td>
        </tr>
        <tr><td valign="top">
        3.</td><td valign="top">
        การติดตั้งใบรับรองอิเล็กทรอนิกส์(Digital Certificate)</td><td><div id="msg3">-&nbsp;</div></td></tr>
        <!--tr><td>
        4.</td><td>
            ทดสอบการอัพโหลดไฟล์เอกสาร(pdf) พร้อมลงลายมือชื่ออิเล็กทรอนิกส์</td><td><div id="msg4">-&nbsp;</div></td></tr-->
            </table>
            <div style="color:#666666;text-align:left;font-size:8pt;margin-top:8px;margin-left:20px;"><b>สัญลักษณ์:</b> <img  src="Images/icon_check.gif" style="border-width:0px;" /> หมายถึง ผ่านการตรวจสอบ  &nbsp; <img src="Images/icon_delete.gif" style="border-width:0px;" /> หมายถึง ไม่ผ่านการตรวจสอบ</div>
    </div>
    <p>&nbsp;</p>
    <div style="font-size:10pt;margin-top:10px;" id="certinfo"></div>
    <p>&nbsp;</p>  
  </div>
 
 
 <div id="tabs-3">
 <h2 style="margin:0px;">Digital Signature - Client</h2>
    <div style="margin-bottom:20px;">สำหรับการลงลายมือชื่ออิเล็กทรอนิกส์ ด้วย Certificate ที่ต้องติดตั้งที่เครื่องคอมพิวเตอร์ของผู้ใช้งานที่ใช้ส่งข้อมูล</div>
    เลขประจำตัวผู้เสียภาษี(TaxId): <input type="text" name="txtTaxId" id="Text1" value="<% Response.Write(Request.QueryString("taxid")) %>" />  <font color="gray" style="font-size:8pt;">ค้นหาข้อมูล Certificate ของบริษัทจาก TaxId (เลขผู้เสียภาษี)</font>
<br />
<br />
String Data:<br />
<textarea rows="3" cols="100" style="width:100%" id="txtData">Data for Sign.</textarea>
<br /><br />
<input type="button" value="ทดสอบลงลายมือชื่ออิเล็กทรอนิกส์(Sign)" onclick="return SignData();" />&nbsp; &nbsp;
<br />
<br />
Result:
<br />
<textarea cols="100" style="width:100%" rows="10" id="txtSignData" name="txtSignData"></textarea>
        <br /><br />
<input type="button" value="ตรวจสอบลายมือชื่ออิเล็กทรอนิกส์(Verify)" onclick="return VerifyData();" />
 </div> 
 
 </div>
 <div style="clear:both"> 
    <p style="color:#999999;font-size:8pt;">&copy;2013 <a href="http://www.nti.co.th" target="_blank" title="New Technology Information Co.,Ltd.">NTi</a>. All rights reserved.</p>
 </div>
</body>
</html>