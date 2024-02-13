<%@ Control Inherits="DotNetNuke.Modules.HTML.Settings" CodeFile="Settings.ascx.vb" language="vb" AutoEventWireup="false" Explicit="true" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>
<table cellspacing="0" cellpadding="4" border="0" width=100%>
	<tr>
		<td class="SubHead"><dnn:label id="plReplaceTokens" controlname="chkReplaceTokens" runat="server" /></td>
		<td valign="top">
		    <asp:CheckBox ID="chkReplaceTokens" runat="server" CssClass="NormalTextBox" Checked="false" />
		</td>
	</tr>
</table>

