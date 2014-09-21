<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LeftMenu.ascx.cs" Inherits="RoyalWebApp.Skins.UC.LeftMenu" %>
<%@ Register src="AllCardsList.ascx" tagname="AllCardsList" tagprefix="uc1" %>
<ul>
<li class="divtext"><a href="Benefits.aspx">BENIFITS</a></li>

<li class="divtext"><a href="CardListing.aspx">MEMBERSHIP TIERS</a></li>
 <li><ul>
     <uc1:AllCardsList ID="AllCardsList1" runat="server" />
 </ul></li>
<li class="divtext"><a href="FAQ.aspx">FAQ</a></li>
<li class="divtext"><a href="Feedback.aspx">FEEDBACK</a></li>
</ul>