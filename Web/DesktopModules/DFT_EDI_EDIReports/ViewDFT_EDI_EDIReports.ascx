<%@ Control Language="vb" Inherits="YourCompany.Modules.DFT_EDI_EDIReports.ViewDFT_EDI_EDIReports"
    AutoEventWireup="false" Explicit="True" CodeBehind="ViewDFT_EDI_EDIReports.ascx.vb" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
</telerik:RadAjaxManager>
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
                                                <font class="groupbox">&nbsp;รายงานการยื่นคำร้องด้วย Digital Signature&nbsp;</font>
                                            </td>
                                            <telerik:RadCodeBlock ID="RadCodeBlock6" runat="server">
                                                <td style='background-image: url(<%= ResolveUrl("images") %>/CORNER.gif); background-repeat: no-repeat;'>
                                                    &nbsp; &nbsp;&nbsp;
                                                </td>
                                            </telerik:RadCodeBlock>
                                            <td align="right">
                                                &nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td align="right" class="m-frm-nav">
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr class="m-frm-hdr">
                    <td>
                        <div style="margin: 10px;">
                            <h4>
                                ประเภทรายงาน</h4>
                            <ul>
                                <li><a href='<%=EditUrl("ctlReportByMonth") %>'>
                                    <img src="/desktopmodules/DFT_EDI_EDIReports/images/report1.jpg" alt="View Report"
                                        title="View Report" width="24px" height="24px" />
                                    สรุปจำนวนการขอฟอร์ม ด้วย Digital Signature ในรอบเดือน</a></li>
                                <li><a href='<%=EditUrl("ctlReportSumaryMonth") %>'>
                                    <img src="/desktopmodules/DFT_EDI_EDIReports/images/report1.jpg" alt="View Report"
                                        title="View Report" width="24px" height="24px" />
                                    สรุปจำนวนการขอฟอร์มทั้งหมด แยกตามเดือน</a></li>
                                <li><a href='<%=EditUrl("crlReportbyCompany") %>'>
                                    <img src="/desktopmodules/DFT_EDI_EDIReports/images/report1.jpg" alt="View Report"
                                        title="View Report" width="24px" height="24px" />
                                    สรุปจำนวนการขอฟอร์มทั้งหมด ที่ผ่านและไม่ผ่านการตรวจสอบจากเจ้าหน้าที่ แยกตามชื่อบริษัท</a></li>
                                <li><a href='<%=EditUrl("ctlReportAllFormByCompany") %>'>
                                    <img src="/desktopmodules/DFT_EDI_EDIReports/images/report1.jpg" alt="View Report"
                                        title="View Report" width="24px" height="24px" />
                                    สรุปจำนวนการขอฟอร์มด้วยไฟล์ XML ที่ผ่านการตรวจสอบจากเจ้าหน้าที่ แยกตามชื่อบริษัท</a></li>
                                <li><a href='<%=EditUrl("ctlReportRequestBySentBy") %>'>
                                    <img src="/desktopmodules/DFT_EDI_EDIReports/images/report1.jpg" alt="View Report"
                                        title="View Report" width="24px" height="24px" />
                                    สรุปจำนวนการขอฟอร์มทั้งหมดของทุกบริษัท แยกรูปแบบการขอฟอร์ม</a></li>
                                <li><a href='<%=EditUrl("ctlReportCheckBySentBy") %>'>
                                    <img src="/desktopmodules/DFT_EDI_EDIReports/images/report1.jpg" alt="View Report"
                                        title="View Report" width="24px" height="24px" />
                                    สรุปจำนวนฟอร์มทั้งหมดของทุกบริษัท ที่ผ่านและไม่ผ่านการตรวจสอบโดยเจ้าหน้าที่</a></li>
                            </ul>
                        </div>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
