<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PrintTopMenu.ascx.cs" Inherits="Royal_Card_Skins_UC_PrintTopMenu" %>
<div class="navigation">  
    <div class="chromestyle" id="chromemenu">
        <ul style="height:60px; *margin-top:0px; vertical-align:bottom;">
            
            <li class="li-home home-active"><a href="http://royalty.kingdomofdreams.in/Account/ViewProfile.aspx" rel="dropMenu1">My Account</a></li>
            <li class="li-strip">|</li>
            <li class="li-about"><a href="#" rel="dropMenu2">Membership Tiers</a></li>
            <li class="li-strip">|</li>
            <li class="li-gallery"><a href="http://royalty.kingdomofdreams.in/Account/Benefits.aspx">Membership Benefits</a></li>
            <li class="li-strip">|</li>
            <li class="li-service"><a href="http://royalty.kingdomofdreams.in/Account/TermsAndConditions.aspx">Terms & Conditions</a></li>
            <li class="li-strip">|</li>
            <li class="li-contact"><a href="http://royalty.kingdomofdreams.in/Account/FAQ.aspx">Faq's</a></li>
            <li class="li-strip">|</li>
            <li class="li-contact"><a href="http://royalty.kingdomofdreams.in/Account/Feedback.aspx">Feedback</a></li>
        </ul>
    </div>
    
    <div id="dropMenu1" class="dropmenudiv">  
      <a href="http://royalty.kingdomofdreams.in/Account/ViewProfile.aspx">View Profile</a></li>
        <a href="http://royalty.kingdomofdreams.in/Account/EditProfile.aspx">Edit Profile</a>
         <a href="http://royalty.kingdomofdreams.in/Account/ChangePassword.aspx">Change Password</a>
         <a href="http://royalty.kingdomofdreams.in/Account/UserCard.aspx">My Card</a>
         <a href="http://122.248.242.95/account/TransactionDetails.aspx">My Transaction</a>
        <a href="http://royalty.kingdomofdreams.in/Account/PointStatement.aspx">Point Statements</a> 
        <%--<a href="http://royalty.kingdomofdreams.in/Account/RedeemPoints.aspx">Redeem Points</a>--%> 
        <a href="http://royalty.kingdomofdreams.in/Account/TopUp.aspx">Top Up</a> 
        <a href="http://royalty.kingdomofdreams.in/Account/Logout.aspx">Sign Out</a>
    </div>
    <div id="dropMenu2" class="dropmenudiv">
        <a href="http://royalty.kingdomofdreams.in/Account/PurpleCard.aspx">Purple Card</a> 
        <a href="http://royalty.kingdomofdreams.in/Account/BlueCard.aspx">Blue Card</a>
        <a href="http://royalty.kingdomofdreams.in/Account/GoldCard.aspx">Gold Card</a> 
        <a href="http://royalty.kingdomofdreams.in/Account/PlatinumCard.aspx">Platinum Card</a>
        <a href="http://royalty.kingdomofdreams.in/Account/TitaniumCard.aspx">Titanium Card</a> 
        <a href="http://royalty.kingdomofdreams.in/Account/PrinceCard.aspx">Prince Card</a>
        <a href="http://royalty.kingdomofdreams.in/Account/KingCard.aspx">King Card</a>
    </div>
</div>