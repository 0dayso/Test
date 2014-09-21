<%@ Page Language="C#" MasterPageFile="~/Skins/Master/AccountMaster.Master" AutoEventWireup="true" CodeBehind="FAQ.aspx.cs" Inherits="RoyalWebApp.Account.FAQ" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHPageHeading" runat="server">
    <div style="color:Black">FAQ's </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHPageData" runat="server">
    <div style="width:100%">&nbsp;</div>
     <div class="home-content-left" style="width:100%">   
     <br />       
            <div id="section" class="scroll-pane" style="margin-right:20px; text-align:left; color:Black">
                      <p style="font-size:18px;">Membership</p>
                      <ul class="faqUl">
                <li class="faqLi">Q) <a href="#royalcardprogramme">Who is eligible to become a member of the Kingdom of Dreams Royal Card Programme?</a></li>
                <li class="faqLi">Q) <a href="#becomeamember">What are the formalities that need to be completed, for becoming a member?</a></li>
                <li class="faqLi">Q) <a href="#annualmembership">How much does the Annual Membership cost?</a></li>
                <li class="faqLi">Q) <a href="#membershipcard">When does the member receive the Membership Card?</a></li>
              </ul>
                      <br />
                      <p style="font-size:18px;">Points Earning</p>
                      <ul class="faqUl">
                <li class="faqLi">Q) <a href="#earnpoints">How does the member earn points?</a></li>
                <li class="faqLi">Q) <a href="#pointsrefunds">What happens to the points upon refunds?</a></li>
                <li class="faqLi">Q) <a href="#memberearnpoints">Do members earn points on discounted transactions?</a></li>
              </ul>
                      <br />
                      <p style="font-size:18px;">Points Redemption</p>
                      <ul class="faqUl">
                <li class="faqLi">Q) <a href="#redeempoints">How does the member redeem points?</a></li>
                <li class="faqLi">Q) <a href="#everypoint">What is the value of every point?</a></li>
                <li class="faqLi">Q) <a href="#redemption">How soon can a member redeem the points earned? What is the minimum number of points that the member should have in the account to be applicable for redemption?</a></li>
                <li class="faqLi">Q) <a href="#pointtogether">Can two or more members share their points or pool their points together?</a></li>
               <%-- <li class="faqLi">Q) <a href="#themembership">Can the member transfer the membership to anyone else?</a></li>--%>
                <li class="faqLi">Q) <a href="#cashaswell">Can points be redeemed for cash as well?</a></li>
                <li class="faqLi">Q) <a href="#membershipperiod">What happens to the points at the end of the membership period?</a></li>
                <li class="faqLi">Q) <a href="#valuepoints">Do you have to pay the whole item value with points?</a></li>
              </ul>
                     
                      <br />
                      <br />
                      <br />
                      <br />
                      <a name="royalcardprogramme" id="royalcardprogramme"></a>
                      <p style="font-size:18px;">Membership</p>
                      <p><strong>Q) Who is eligible to become a member of the Kingdom of Dreams Royal Card Programme?</strong> 
                <strong>A)</strong> The Kingdom of Dreams Royal Card Programme is open to all individuals residing in India above the age of 18 years who have carried out an eligible transaction at Kingdom of Dreams. </p>
                      <p><a name="becomeamember" id="becomeamember"></a> <strong>Q)What are the formalities that need to be completed, for becoming a member?</strong>
                <strong>A)</strong> The Membership Application Form must be completed in its entirety and given to the executive at the Royal Cards Desk outside Culture Gully to become a Royal Card Member.  Alternatively you can also enroll on the website.</p>
                      <p><a name="annualmembership" id="annualmembership"></a><strong>Q) How much does the Annual Membership cost?</strong>
                <strong>A)</strong> Nothing .You just need to spend on the entry charge on a single bill to become eligible for the membership. Based on your billed value, different card memberships with several levels of benefits are available<br />
                <br />
                <strong><a name="membershipcard" id="membershipcard"></a>Q) When does the member receive the Membership Card?</strong>
                <strong>A)</strong> The Permanent Membership Card is given instantaneously ,on submitting the filled membership application form.</p>
                      <p> </p>
                      <p style="font-size:18px;">Points Earning</p>
                      <p><strong><a name="earnpoints" id="earnpoints"></a>Q)How does the member earn points?</strong>
                <strong>A)</strong> The member can earn points by recharging his Royal Membership Card for spends inside Kingdom of Dreams &amp; presenting the Membership Card to the cashier before the bill is generated. </p>
                      <p><strong><a name="pointsrefunds" id="pointsrefunds"></a>Q) What happens to the points upon refunds?</strong>
                <strong>A)</strong> When a customer asks for a refund,the points earned on the original purchase are deducted from their point balance.</p>
                      <p><strong><a name="memberearnpoints" id="memberearnpoints"></a>Q) Do members earn points on discounted transactions?</strong><br />
                <strong>A)</strong> No, you can not earn points on discounted transactions.  Anytime a discounted transaction takes place you should not attach the card number to the transaction.</p>
                      <p></p>
                      <p style="font-size:18px;">Points Redemption</p>
                      <p><strong><a name="redeempoints" id="redeempoints"></a>Q)How does the member redeem points?</strong>
                To redeem points, the member must present the Membership Card to the cashier at the Recharge Counter and request for a redemption against the current points balance. </p>
                      <p><strong><a name="everypoint" id="everypoint"></a>Q)What is the value of every point?</strong>
                <strong>A)</strong> The redemption value of every point is Rs 1/- unless otherwise specified by KOD. </p>
                      <p><strong><a name="redemption" id="redemption"></a>Q) How soon can a member redeem the points earned? What is the minimum number of points that the member should have in the account to be applicable for redemption?</strong> 
                <strong>A)</strong> Points earned by members in the transaction carried out can be redeemed by the member after a gap of 24hrs.  There is no minimum points balance criteria for redemptions. <br />
              </p>
                      <strong> <a name="pointtogether" id="pointtogether"></a>Q) Can two or more members share their points or pool their points together?</strong>
                      Yes, pooling or sharing of points between a Primary and an Add-on Card Holder will be allowed. Points earned from charge value loaded on either of the cards will be rewarded with points. <br />
                      Also, Tier upgrade/downgrade as well as membership expiry &amp; points lapsing will be applicable on both cards together. In case of redemption, balance points available can be redeemed by either of the members &ndash; primary or secondary card holders.<br />
                      <br />
                      <%--<strong><a name="themembership" id="themembership"></a>Q) Can the member transfer the membership to anyone else?</strong>
                      <strong>A)</strong> The membership is non transferable and a member cannot transfer the membership to anyone else, under any circumstances .<br />
                      <br />--%>
                      <strong><a name="cashaswell" id="cashaswell"></a>Q)&nbsp;Can points be redeemed for cash as well?</strong>
                      <strong>A)</strong> Points cannot be redeemed against cash under any circumstance.<br />
                      <br />
                      <strong><a name="membershipperiod" id="membershipperiod"></a>Q) What happens to the points at the end of the membership period?</strong>
                      <strong>A)</strong> Points accumulated  shall lapse automatically unless they are redeemed within the membership validity.<br />
                      <br />
                      <strong><a name="valuepoints" id="valuepoints"></a>Q) Do you have to pay the whole item value with points?</strong>
                      <strong>A)</strong> No, a member can redeem as many points as they want in a given transaction. They may choose to pay the whole bill value in points or any part thereof and pay the remaining portion through cash/ credit cards. <br />
                                          
                      <br />
                    </div>
 </div>
</asp:Content>
