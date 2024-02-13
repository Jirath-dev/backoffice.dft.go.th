<%@ Control Language="vb" Inherits="NTi.Modules.DFT_EDI_F03_Normal.ViewDFT_EDI_F03_Normal"
    AutoEventWireup="false" Explicit="True" CodeBehind="ViewDFT_EDI_F03_Normal.ascx.vb" %>
<asp:Panel ID="panel_ds1" runat="server">
    <table cellpadding="4" width="120px">
        <tr>
            <td style="text-align: center">
                <asp:HyperLink ID="HyperLink1" runat="server" ImageUrl="~/images/fileprint.png" NavigateUrl="~/ระบบงานออกหนงสอรบรองถนกำเนดสนคา/ระบบงานรบหนงสอรบรองEDI/พมพหนงสอรบรอง/tabid/60/Default.aspx"></asp:HyperLink>
                <br />
                <asp:HyperLink ID="HyperLink2" runat="server" Font-Names="Tahoma" Font-Size="10pt"
                    NavigateUrl="~/ระบบงานออกหนงสอรบรองถนกำเนดสนคา/ระบบงานรบหนงสอรบรองEDI/พมพหนงสอรบรอง/tabid/60/Default.aspx">พิมพ์หนังสือรับรอง</asp:HyperLink>
            </td>
        </tr>
        <tr runat="server" id="trprintrecive_edi">
            <td style="text-align: center">
                <asp:HyperLink ID="HyperLink3" runat="server" ImageUrl="~/images/reports.png" NavigateUrl="~/ระบบงานออกหนงสอรบรองถนกำเนดสนคา/ระบบงานรบหนงสอรบรองEDI/ออกใบเสรจรบเงน/tabid/61/Default.aspx"></asp:HyperLink>
                <br />
                <asp:HyperLink ID="HyperLink4" runat="server" Font-Names="Tahoma" Font-Size="10pt"
                    NavigateUrl="~/ระบบงานออกหนงสอรบรองถนกำเนดสนคา/ระบบงานรบหนงสอรบรองEDI/ออกใบเสรจรบเงน/tabid/61/Default.aspx">ออกใบเสร็จ</asp:HyperLink>
            </td>
        </tr>
        <tr>
            <td style="text-align: center">
                <asp:HyperLink ID="HyperLink5" runat="server" ImageUrl="~/images/green-ok.gif" NavigateUrl="/ระบบงานออกหนงสอรบรองถนกำเนดสนคา/ระบบงานรบหนงสอรบรองEDI/บนทกผลการตรวจสอบเฉพาะทผาน/tabid/62/Default.aspx"></asp:HyperLink>
                <br />
                <asp:HyperLink ID="HyperLink6" runat="server" Font-Names="Tahoma" Font-Size="10pt"
                    NavigateUrl="/ระบบงานออกหนงสอรบรองถนกำเนดสนคา/ระบบงานรบหนงสอรบรองEDI/บนทกผลการตรวจสอบเฉพาะทผาน/tabid/62/Default.aspx">บันทึกผล<br />(ผ่านการอนุมัติ)</asp:HyperLink>
            </td>
        </tr>
        <tr runat="server" id="trprintrepeat_edi">
            <td style="text-align: center">
                <asp:HyperLink ID="HyperLink19" runat="server" ImageUrl="~/images/reports.png" NavigateUrl="/ระบบงานออกหนงสอรบรองถนกำเนดสนคา/ระบบงานรบหนงสอรบรองEDI/พมพใบเสรจพมพซำ/tabid/63/Default.aspx"></asp:HyperLink>
                <br />
                <asp:HyperLink ID="HyperLink20" runat="server" Font-Names="Tahoma" Font-Size="10pt"
                    NavigateUrl="/ระบบงานออกหนงสอรบรองถนกำเนดสนคา/ระบบงานรบหนงสอรบรองEDI/พมพใบเสรจพมพซำ/tabid/63/Default.aspx">พิมพ์ใบเสร็จ<br />(พิมพ์ซ้ำ)</asp:HyperLink>
            </td>
        </tr>
    </table>
</asp:Panel>
<asp:Panel runat="server" ID="panel_ds2" Visible="false">
    <table cellpadding="4" width="120px">
        <tr>
            <td style="text-align: center">
                <asp:HyperLink ID="HyperLink77" runat="server" ImageUrl="~/images/checkdoc.png" NavigateUrl="~/tabid/124/Default.aspx"></asp:HyperLink>
                <br />
                <asp:HyperLink ID="HyperLink88" runat="server" Font-Names="Tahoma" Font-Size="10pt"
                    NavigateUrl="~/tabid/124/Default.aspx">ตรวจสอบแบบฟอร์ม และเอกสารแนบ</asp:HyperLink>
            </td>
        </tr>
        <tr>
            <td style="text-align: center">
                <asp:HyperLink ID="HyperLink7" runat="server" ImageUrl="~/images/fileprint.png" NavigateUrl="~/tabid/60/Default.aspx"></asp:HyperLink>
                <br />
                <asp:HyperLink ID="HyperLink8" runat="server" Font-Names="Tahoma" Font-Size="10pt"
                    NavigateUrl="~/tabid/60/Default.aspx">พิมพ์หนังสือรับรอง</asp:HyperLink>
            </td>
        </tr>
        <tr runat="server" id="trprintrecive">
            <td style="text-align: center">
                <asp:HyperLink ID="HyperLink9" runat="server" ImageUrl="~/images/reports.png" NavigateUrl="~/tabid/61/Default.aspx"></asp:HyperLink>
                <br />
                <asp:HyperLink ID="HyperLink10" runat="server" Font-Names="Tahoma" Font-Size="10pt"
                    NavigateUrl="~/tabid/61/Default.aspx">ออกใบเสร็จ</asp:HyperLink>
            </td>
        </tr>
        <tr>
            <td style="text-align: center">
                <asp:HyperLink ID="HyperLink11" runat="server" ImageUrl="~/images/green-ok.gif" NavigateUrl="/tabid/62/Default.aspx"></asp:HyperLink>
                <br />
                <asp:HyperLink ID="HyperLink12" runat="server" Font-Names="Tahoma" Font-Size="10pt"
                    NavigateUrl="/tabid/62/Default.aspx">บันทึกผล<br />(ผ่านการอนุมัติ)</asp:HyperLink>
            </td>
        </tr>
        <tr>
            <td style="text-align: center">
                <asp:HyperLink ID="HyperLink55" runat="server" ImageUrl="~/images/attachdoc.gif" NavigateUrl="/tabid/64/Default.aspx"></asp:HyperLink>
                <br />
                <asp:HyperLink ID="HyperLink66" runat="server" Font-Names="Tahoma" Font-Size="10pt"
                    NavigateUrl="/tabid/64/Default.aspx">การแนบเอกสาร</asp:HyperLink>
            </td>
        </tr>
        <tr runat="server" id="trprintrepeat">
            <td style="text-align: center">
                <asp:HyperLink ID="HyperLink13" runat="server" ImageUrl="~/images/reports.png" NavigateUrl="/tabid/63/Default.aspx"></asp:HyperLink>
                <br />
                <asp:HyperLink ID="HyperLink14" runat="server" Font-Names="Tahoma" Font-Size="10pt"
                    NavigateUrl="/tabid/63/Default.aspx">พิมพ์ใบเสร็จ<br />(พิมพ์ซ้ำ)</asp:HyperLink>
            </td>
        </tr>
    </table>
</asp:Panel>
