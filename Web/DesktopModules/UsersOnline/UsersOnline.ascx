<%@ Control Language="vb" AutoEventWireup="false" Inherits="DotNetNuke.Modules.UsersOnline.UsersOnline" CodeBehind="UsersOnline.ascx.vb" %>
<table cellspacing="0" cellpadding="4" width="100%">
	<tr class="Normal">
		<td>
			<asp:panel runat="server" id="pnlMembership" visible="False">
				<TABLE cellSpacing="0" cellPadding="0" border="0">
					<TR>
						<TD align="center" width="25"><IMG id="imgMembership" height="14" alt="Membership" src="uoGroup1.gif" width="17" align="absMiddle"
								runat="server" resourcekey="imgMembership"></TD>
						<TD class="NormalBold">
							<asp:label id="MembershipLabel" runat="server" resourcekey="MembershipLabel">
								<u>Membership:</u></asp:label></TD>
					</TR>
					<TR>
						<TD align="center" width="25"><IMG id="imgLatest" height="14" alt="Latest New User" src="uoLatest.gif" width="17" align="absMiddle"
								runat="server" resourcekey="imgLatest"></TD>
						<TD class="Normal">
							<asp:label id="LatestUserLabel" runat="server" resourcekey="LatestUserLabel">
								<u>Latest:</u></asp:label>
							<asp:label id="lblLatestUserName" runat="server" cssClass="NormalBold"></asp:label></TD>
					</TR>
					<TR>
						<TD align="center" width="25"><IMG id="imgNewToday" height="14" alt="New Today" src="uoNewToday.gif" width="17" align="absMiddle"
								runat="server" resourcekey="imgNewToday"></TD>
						<TD class="Normal">
							<asp:label id="NewTodayLabel" runat="server" resourcekey="NewTodayLabel">New Today:</asp:label>
							<asp:label id="lblNewToday" runat="server" cssClass="NormalBold"></asp:label></TD>
					</TR>
					<TR>
						<TD align="center" width="25"><IMG id="imgNewYesterday" height="14" alt="New Yesterday" src="uoNewYesterday.gif" width="17"
								align="absMiddle" runat="server" resourcekey="imgNewYesterday"></TD>
						<TD class="Normal">
							<asp:label id="NewYesterdayLabel" runat="server" resourcekey="NewYesterdayLabel">New Yesterday:</asp:label>
							<asp:label id="lblNewYesterday" runat="server" cssClass="NormalBold"></asp:label></TD>
					</TR>
					<TR>
						<TD align="center" width="25"><IMG id="imgUserCount" height="14" alt="Overall Users" src="uoOverall.gif" width="17"
								align="absMiddle" runat="server" resourcekey="imgUserCount"></TD>
						<TD class="Normal">
							<asp:label id="OverallLabel" runat="server" resourcekey="OverallLabel">Overall:</asp:label>
							<asp:label id="lblUserCount" runat="server" cssClass="NormalBold"></asp:label></TD>
					</TR>
				</TABLE>
				<HR>
			</asp:panel>
			<asp:panel runat="server" id="pnlUsersOnline" visible="False">
				<TABLE cellSpacing="0" cellPadding="0" border="0">
					<TR>
						<TD align="center" width="25"><IMG id="imgPeopleOnline" height="14" alt="People Online" src="uoGroup2.gif" width="17"
								align="absMiddle" runat="server" resourcekey="imgPeopleOnline"></TD>
						<TD class="NormalBold">
							<asp:label id="PeopleOnlineLabel" runat="server" resourcekey="PeopleOnlineLabel">
								<u>People Online:</u></asp:label></TD>
					</TR>
					<TR>
						<TD align="center" width="25"><IMG id="imgVisitors" height="14" alt="Visitors" src="uoVisitors.gif" width="17" align="absMiddle"
								runat="server" resourcekey="imgVisitors"></TD>
						<TD class="Normal">
							<asp:label id="VisitorsLabel" runat="server" resourcekey="VisitorsLabel">Visitors:</asp:label>
							<asp:label id="lblGuestCount" runat="server" cssClass="NormalBold"></asp:label></TD>
					</TR>
					<TR>
						<TD align="center" width="25"><IMG id="imgMemberCount" height="14" alt="Members" src="uoMembers.gif" width="17" align="absMiddle"
								runat="server" resourcekey="imgMemberCount"></TD>
						<TD class="Normal">
							<asp:label id="MembersLabel" runat="server" resourcekey="MembersLabel">Members:</asp:label>
							<asp:label id="lblMemberCount" runat="server" cssClass="NormalBold"></asp:label></TD>
					</TR>
					<TR>
						<TD align="center" width="25"><IMG id="imgTotalCount" height="14" alt="Total Users" src="uoTotal.gif" width="17" align="absMiddle"
								runat="server" resourcekey="imgTotalCount"></TD>
						<TD class="Normal">
							<asp:label id="TotalLabel" runat="server" resourcekey="TotalLabel">Total:</asp:label>
							<asp:label id="lblTotalCount" runat="server" cssClass="NormalBold"></asp:label></TD>
					</TR>
				</TABLE>
				<HR>
			</asp:panel>
			<asp:panel runat="server" id="pnlOnlineNow" visible="False">
				<asp:repeater id="rptOnlineNow" runat="server" enableviewstate="False">
					<headertemplate>
						<img id="imgOnlineNow" resourcekey="imgOnlineNow" runat="server" height="14" src='<%# ResolveUrl("uoGroup3.gif") %>' width="17" align="absMiddle" alt="Onine Now" />
						<asp:label id="OnlineNowLabel" runat="server" resourcekey="OnlineNowLabel" cssClass="NormalBold" >
								<u>Online Now:</u></asp:label><br>
					</headertemplate>
					<itemtemplate>
						<asp:label id="lblUserNumber" runat="server" />:&nbsp;<%# DataBinder.Eval(Container.DataItem, "UserName") %>
						<br>
					</itemtemplate>
				</asp:repeater>
			</asp:panel>
		</td>
	</tr>
</table>
