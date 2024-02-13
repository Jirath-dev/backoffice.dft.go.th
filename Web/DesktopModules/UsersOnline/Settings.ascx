<%@ Control Inherits="DotNetNuke.Modules.UsersOnline.Settings" CodeBehind="Settings.ascx.vb" language="vb" AutoEventWireup="false" Explicit="true" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>
<table cellSpacing=0 cellPadding=4 border=0>
	<tr>
		<td class="SubHead" width="150"><dnn:label id="plShowMembership" text="Show Membership:" controlname="chkShowMembership" runat="server" /></td>
		<td vAlign=top><asp:checkbox id=chkShowMembership CssClass="NormalBold" runat="server"></asp:checkbox></td>
	</tr>
	<tr>
		<td class="SubHead" width="150"><dnn:label id="plShowPeopleOnline" text="Show People Online:" controlname="chkShowPeopleOnline" runat="server" /></td>
		<td vAlign=top><asp:checkbox id=chkShowPeopleOnline CssClass="NormalBold" runat="server"></asp:checkbox></td>
	</tr>
	<tr>
		<td class="SubHead" width="150"><dnn:label id="plShowUsersOnline" text="Show Users Online:" controlname="chkShowUsersOnline" runat="server" /></td>
		<td vAlign=top><asp:checkbox id=chkShowUsersOnline CssClass="NormalBold" runat="server"></asp:checkbox></td>
	</tr>
</table>
