<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TopMenu.ascx.cs" Inherits="RoyalWebApp.Skins.UC.TopMenu" %>
<div class="navigation">  
    <div class="chromestyle" id="chromemenu">
        <ul style="height:60px; *margin-top:0px; vertical-align:bottom;">
            <%if (Session["IsLogged"] != null && Session["IsLogged"] != "")
              {%>
            <li class="li-home home-active"><a href="ViewProfile.aspx" rel="dropmenu1">My Account</a></li>
            <%}
              else
              {%>
            <li class="li-home home-active"> <a href="Login.aspx">Home</a></li>
            <%} %>
            <li class="li-strip">|</li>
            <li class="li-about"><a href="#" rel="dropmenu2">Membership Tiers</a></li>
            <li class="li-strip">|</li>
            <li class="li-gallery"><a href="Benefits.aspx">Membership Benefits</a></li>
            <li class="li-strip">|</li>
            <li class="li-service"><a href="TermsAndConditions.aspx">Terms & Conditions</a></li>
            <li class="li-strip">|</li>
            <li class="li-contact"><a href="FAQ.aspx">Faq's</a></li>
            <li class="li-strip">|</li>
            <li class="li-contact"><a href="Feedback.aspx">Feedback</a></li>
        </ul>
    </div>
    <%if (Session["IsLogged"] != null && Session["IsLogged"] != "")
      {%>
    <div id="dropmenu1" class="dropmenudiv">  
      <a href="ViewProfile.aspx">View Profile</a></li>
        <a href="EditProfile.aspx">Edit Profile</a>
         <a href="ChangePassword.aspx">Change Password</a>
         <a href="UserCard.aspx">My Card</a><a href="TransactionDetails.aspx">My Transaction</a>
        <a href="PointStatement.aspx">Point Statements</a> <%--<a href="RedeemPoints.aspx">Redeem
            Points</a>--%> <a href="TopUp.aspx">Top Up</a> <a href="Logout.aspx">Sign Out</a>
    </div>
    <%}%>
    <div id="dropmenu2" class="dropmenudiv">
        <a href="PurpleCard.aspx">Purple Card</a>
         <a href="BlueCard.aspx">Blue Card</a>
        <%--<a href="GoldCard.aspx">Gold Card</a> --%>
        <a href="PlatinumCard.aspx">Platinum Card</a>
        <a href="TitaniumCard.aspx">Titanium Card</a>
        <%--<a href="PrinceCard.aspx">Prince Card</a>
        <a href="KingCard.aspx">King Card</a>--%>
    </div>
</div>
