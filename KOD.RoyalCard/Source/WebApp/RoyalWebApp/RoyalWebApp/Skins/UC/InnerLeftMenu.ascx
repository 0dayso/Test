<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="InnerLeftMenu.ascx.cs" Inherits="RoyalWebApp.Skins.UC.InnerLeftMenu" %>
<%@ Register src="AllCardsList.ascx" tagname="AllCardsList" tagprefix="uc1" %>
<table cellpadding="2" cellspacing="2">      
  <tr>
  <td valign="top">
 <ul>
  <li class="divtext"><a href="Login.aspx">HOME</a></li>
 <li class="divtext"><a href="ViewProfile.aspx">MY ACCOUNT</a></li>
 <li><ul>
<li class="divtext"><a href="EditProfile.aspx">EDIT PROFILE</a></li>

<li class="divtext"><a href="ChangePassword.aspx">CHANGE PASSWORD</a></li>

<li class="divtext"><a href="PointStatement.aspx">POINTS STATUS</a></li>

<li class="divtext"><a href="RedeemPoints.aspx">REDEEM POINTS</a></li>

<li class="divtext"><a href="TopUp.aspx">TOP UP</a></li>

<li class="divtext"><a href="Logout.aspx">LOGOUT</a></li>
</ul></li>
<li class="divtext"><a href="Benefits.aspx">BENIFITS</a></li>

<li class="divtext"><a href="CardListing.aspx">MEMBERSHIP TIERS</a></li>
 <li><ul>
     <uc1:AllCardsList ID="AllCardsList1" runat="server" />
 </ul></li>
<li class="divtext"><a href="FAQ.aspx">FAQ</a></li>
<li class="divtext"><a href="Feedback.aspx">FEEDBACK</a></li>
</ul>
</td>
</tr>
</table>