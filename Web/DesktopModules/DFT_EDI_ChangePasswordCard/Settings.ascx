<%@ Control Language="vb" AutoEventWireup="false" Inherits="YourCompany.Modules.DFT_EDI_ChangePasswordCard.Settings"
    CodeBehind="Settings.ascx.vb" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>
<table cellspacing="0" cellpadding="2" border="0" summary="M100MS17A_B Settings Design Table">
    <tr>
        <td class="SubHead" width="150">
            ระบบ
        </td>
        <td>
            <asp:RadioButtonList ID="rdoSystemType" runat="server">
                <asp:ListItem Text="EDI" Value="EDI"></asp:ListItem>
                <asp:ListItem Text="TRADING" Value="TRADING"></asp:ListItem>
            </asp:RadioButtonList>
        </td>
    </tr>
</table>
