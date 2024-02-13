<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="Report1.ascx.vb" Inherits=".Report1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<div style="font-size: 9pt; padding-top: 10px; text-align: right">
    <asp:Image ImageAlign="Middle" ID="Image2" runat="server" ImageUrl="../../Images/Gear-icon.png" />
    <asp:LinkButton ID="LinkButton1" runat="server">กลับไปหน้าหลัก Admin DashBoard</asp:LinkButton></div>
<h2>รายงานจำนวนแบบคำขอ แยกตามสาขา</h2>
<div style="font-size: 10pt; margin-top: 10px; margin-bottom: 10px; border: 1px solid #cccccc;
    padding: 5px;">
    <table>
        <tr>
            <td>
                ช่วงวันที่&nbsp; เริ่มต้น:
            </td>
            <td>
                <telerik:RadDatePicker ID="txtFromDate" runat="server" Culture="English (United Kingdom)"
                    Skin="Vista">
                    <calendar daynameformat="FirstTwoLetters" firstdayofweek="Sunday" showrowheaders="False"
                        skin="Vista" usecolumnheadersasselectors="False" userowheadersasselectors="False"
                        viewselectortext="x">
                                                                    </calendar>
                    <datepopupbutton hoverimageurl="" imageurl="" />
                </telerik:RadDatePicker>
            </td>
            <td>
                ถึงวันที่
            </td>
            <td>
                <telerik:RadDatePicker ID="txtToDate" runat="server" Culture="English (United Kingdom)"
                    Skin="Vista">
                    <calendar daynameformat="FirstTwoLetters" firstdayofweek="Sunday" showrowheaders="False"
                        skin="Vista" usecolumnheadersasselectors="False" userowheadersasselectors="False"
                        viewselectortext="x">
                                                                    </calendar>
                    <datepopupbutton hoverimageurl="" imageurl="" />
                </telerik:RadDatePicker>
            </td>
            <td>
                <asp:Button  ID="btnSearch" runat="server" Text="ค้นหา" />
            </td>
            </ta></tr>
        <tr>
            <td>
                สถานะ:</td>
            <td colspan="4">
                <asp:DropDownList ID="ddlStatus" runat="server">
                <asp:ListItem Text="ส่งแล้วรอการตรวจลายมือชื่ออิเล็กทรอนิกส์ - S" Value="S"></asp:ListItem>
                <asp:ListItem Text="ส่งแล้วรอการตรวจสอบจากคอมพิวเตอร์ - W" Value="W"></asp:ListItem>
                <asp:ListItem Text="รอการตรวจสอบจากเจ้าหน้าที่ - Q" Value="Q"></asp:ListItem>
                <asp:ListItem Text="ผ่านการตรวจสอบคำขอและเอกสารแนบโดยเจ้าหน้าที่ - D" Value="D"></asp:ListItem>
                <asp:ListItem Text="ผ่านการตรวจสอบจากเจ้าหน้าที่ - A" Value="A"></asp:ListItem>
                <asp:ListItem Text="ไม่ผ่านการตรวจสอบจากคอมพิวเตอร์ - R" Value="R"></asp:ListItem>
                <asp:ListItem Text="ไม่ผ่านการตรวจสอบจากเจ้าหน้าที่ - N" Value="N"></asp:ListItem>
                
                </asp:DropDownList>
            </td>
            </tr>
    </table>
    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
</div>