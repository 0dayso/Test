<%@ Page Title="" Language="C#" MasterPageFile="~/RoyalCard/Mastercard/Master/MasterCard.master" AutoEventWireup="true" CodeFile="Details.aspx.cs" Inherits="RoyalCard_Mastercard_Detail" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

<style type="text/css">
    
        .style3
        {
            font-family: "Trebuchet MS", Arial, Helvetica, sans-serif;
            font-size: 12px;
            color: #3f260a;
            text-decoration: none;
            margin-left: 14px;
            width: 98px;
        }
        .text-small
{
    font: 12px verdana;
    color: #000000;
}

    .style6
    {
        width: 137px;
    }
    .style7
    {
        font-family: "Trebuchet MS", Arial, Helvetica, sans-serif;
        font-size: 12px;
        color: #3f260a;
        text-decoration: none;
        margin-left: 14px;
        }
    .style8
    {
        width: 26px;
    }
    .style9
    {
        width: 169px;
    }
    .style10
    {
        float: center;
        width: 169px;
    }
    .style11
    {
        float:left;
    }
     .style12
    {
        float: center;
    }
    
        .ModalWindow
        {
            background-color:E7C54A;
            border-width: 3px;
            border-style: solid;
            border-color: black;
            padding: 3px;
            width: 550px;
            height: 500px;
        }
        .ModalWindow2
        {
            background-color: #FFFFFF;
            border-width: 3px;
            border-style: solid;
            border-color: #E7C54A;
            padding: 3px;
        }
        .modalBackground2
        {
            background-color: Gray;
            filter: alpha(opacity=70);
            opacity: 0.7;
        }
      .grayBox{ 
    position: fixed; 
    top: 0%; 
    left: 0%; 
    width: 100%; 
    height: 100%; 
    background-color: Black; 
    z-index:1001; 
    -moz-opacity: .90; 
    opacity:.90; 
    filter: alpha(opacity=90); 
} 
.box_content { 
    position:absolute;
	width:400px;
	height:140px;
	display:none;
	z-index:9999;
	padding:20px;
	top:200px;
	left:400px;
	background-color: #000000;
	border:solid 1px #FF9900;
} 
.box_content1 { 
    position:absolute;
	width:390px;
	height:130px;
	display:none;
	z-index:9999;
	padding:20px;
	top:206px;
	left:406px;
	background-color: #000000;
	
} 
.buttontickets{
	float: right;
	background-color:#eed075;
	border-color:Orange;
	color: #231f20;
	width:80px;
 font-weight:bold;
         }
    .text
    {
    font-family:Verdana;  
    }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHPageHeading" Runat="Server">
Enrolment Details:
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHPageData" Runat="Server">
<table class="style11">
<tr>
<td class="style7">
Salutation*
</td>
<td class="style8">:</td>
<td class="style7">
    <asp:RadioButtonList ID="RdGender" runat="server" RepeatDirection="Horizontal" class="divtext">
                        <asp:ListItem Value="Mr." Text="Mr." Selected="True"></asp:ListItem>
                        <asp:ListItem Value="Ms" Text="Ms"></asp:ListItem>
                        <asp:ListItem Value="Others" Text="Others"></asp:ListItem>
    </asp:RadioButtonList>
</td>
</tr>
<tr>
<td class="style7">
First Name*
</td>
<td class="style8">:</td>
<td class="style7">
    <asp:TextBox ID="txtFirstName"  runat="server"></asp:TextBox>
    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" FilterType="LowercaseLetters,UppercaseLetters,Custom" ValidChars=" " TargetControlID="txtFirstName"
                    runat="server">
                </cc1:FilteredTextBoxExtender>
</td>
<td class="style7">
Last Name*
</td>
<td class="style8">:</td>
<td class="style7">
    <asp:TextBox ID="txtLastName" runat="server"></asp:TextBox>
    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" FilterType="LowercaseLetters,UppercaseLetters,Custom" ValidChars=" " TargetControlID="txtLastName"
                    runat="server">
                </cc1:FilteredTextBoxExtender>
</td>
</tr>
<tr>
<td class="style7">
Address*
</td>
<td class="style8">:</td>
<td class="style7">
    <asp:TextBox  ID="txtAddress" class="text" runat="server"  TextMode="MultiLine"  
        Height="30px" />
</td>
<td class="style7">
City*
</td>
<td class="style8">:</td>
<td class="style7">
    <asp:TextBox ID="txtCity" runat="server"></asp:TextBox>
     <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" FilterType="LowercaseLetters,UppercaseLetters,Custom" ValidChars=" " TargetControlID="txtCity"
                    runat="server">
                </cc1:FilteredTextBoxExtender>
</td>
</tr>
<tr>
<td class="style7">
Country*
</td>
<td class="style8">:</td>
<td class="style7">
    <asp:TextBox ID="DdlCountry" runat="server" 
        ></asp:TextBox>
    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" FilterType="LowercaseLetters,UppercaseLetters,Custom" ValidChars=" " TargetControlID="DdlCountry"
                    runat="server">
                </cc1:FilteredTextBoxExtender>
</td>
<td class="style7">
Email ID*
</td>
<td class="style8">:</td>
<td class="style7">
    <asp:TextBox ID="txtEmailId" runat="server"></asp:TextBox>
</td>
</tr>
<tr>
<td class="style7">
Pin Code
</td>
<td class="style8">:</td>
<td class="style7">
    <asp:TextBox ID="txtpin" runat="server" MaxLength="6"></asp:TextBox>
    <cc1:FilteredTextBoxExtender ID="F1" FilterType="Numbers" TargetControlID="txtpin"
                    runat="server">
                </cc1:FilteredTextBoxExtender>
</td>
<td class="style7">
MobileNo*
</td>
<td class="style8">:</td>
<td class="style7">
    <asp:TextBox ID="txtMobileNo" runat="server"></asp:TextBox>
    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" FilterType="Numbers" TargetControlID="txtMobileNo"
                    runat="server">
                </cc1:FilteredTextBoxExtender>
</td>
</tr>
<tr>
<td class="style7">
Date Of Birth*
</td>
<td class="style8">:</td>
<td class="style7">
    <asp:DropDownList ID="ddlday" runat="server" CssClass="inputdropdown">
                        <asp:ListItem Value="DD" Selected="True" Text="DD"></asp:ListItem>
                        <asp:ListItem Value="01" Text="01"></asp:ListItem>
                        <asp:ListItem Value="02" Text="02"></asp:ListItem>
                        <asp:ListItem Value="03" Text="03"></asp:ListItem>
                        <asp:ListItem Value="04" Text="04"></asp:ListItem>
                        <asp:ListItem Value="05" Text="05"></asp:ListItem>
                        <asp:ListItem Value="06" Text="06"></asp:ListItem>
                        <asp:ListItem Value="07" Text="07"></asp:ListItem>
                        <asp:ListItem Value="08" Text="08"></asp:ListItem>
                        <asp:ListItem Value="09" Text="09"></asp:ListItem>
                        <asp:ListItem Value="10" Text="10"></asp:ListItem>
                        <asp:ListItem Value="11" Text="11"></asp:ListItem>
                        <asp:ListItem Value="12" Text="12"></asp:ListItem>
                        <asp:ListItem Value="13" Text="13"></asp:ListItem>
                        <asp:ListItem Value="14" Text="14"></asp:ListItem>
                        <asp:ListItem Value="15" Text="15"></asp:ListItem>
                        <asp:ListItem Value="16" Text="16"></asp:ListItem>
                        <asp:ListItem Value="17" Text="17"></asp:ListItem>
                        <asp:ListItem Value="18" Text="18"></asp:ListItem>
                        <asp:ListItem Value="19" Text="19"></asp:ListItem>
                        <asp:ListItem Value="20" Text="20"></asp:ListItem>
                        <asp:ListItem Value="21" Text="21"></asp:ListItem>
                        <asp:ListItem Value="22" Text="22"></asp:ListItem>
                        <asp:ListItem Value="23" Text="23"></asp:ListItem>
                        <asp:ListItem Value="24" Text="24"></asp:ListItem>
                        <asp:ListItem Value="25" Text="25"></asp:ListItem>
                        <asp:ListItem Value="26" Text="26"></asp:ListItem>
                        <asp:ListItem Value="27" Text="27"></asp:ListItem>
                        <asp:ListItem Value="28" Text="28"></asp:ListItem>
                        <asp:ListItem Value="29" Text="29"></asp:ListItem>
                        <asp:ListItem Value="30" Text="30"></asp:ListItem>
                        <asp:ListItem Value="31" Text="31"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddlmonth" runat="server" CssClass="inputdropdown">
                        <asp:ListItem Value="MM" Selected="True" Text="MM"></asp:ListItem>
                        <asp:ListItem Value="01"  Text="Jan"></asp:ListItem>
                        <asp:ListItem Value="02"  Text="Feb"></asp:ListItem>
                         <asp:ListItem Value="03"  Text="Mar"></asp:ListItem>
                          <asp:ListItem Value="04"  Text="Apr"></asp:ListItem>
                           <asp:ListItem Value="05"  Text="May"></asp:ListItem>
                           <asp:ListItem Value="06"  Text="Jun"></asp:ListItem>
                         <asp:ListItem Value="07"  Text="Jul"></asp:ListItem>
                          <asp:ListItem Value="08"  Text="Aug"></asp:ListItem>
                           <asp:ListItem Value="09"  Text="Sep"></asp:ListItem>
                       <asp:ListItem Value="10"  Text="Oct"></asp:ListItem>
                          <asp:ListItem Value="11"  Text="Nov"></asp:ListItem>
                           <asp:ListItem Value="12"  Text="Dec"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddlyear" runat="server" CssClass="inputdropdown" >
                    <asp:ListItem Value="YYYY" Selected="True" Text="YYYY"></asp:ListItem>
                    </asp:DropDownList>
</td>
<td class="style7" runat="server" id="trAnn1">
Anniversary Date*
</td>
<td class="style8" runat="server" id="trAnn2">:</td>
<td class="style7" runat="server" id="trAnn" >
    <asp:DropDownList ID="DropDownList1" runat="server" CssClass="inputdropdown">
                        <asp:ListItem Value="DD" Selected="True" Text="DD"></asp:ListItem>
                        <asp:ListItem Value="01" Text="01"></asp:ListItem>
                        <asp:ListItem Value="02" Text="02"></asp:ListItem>
                        <asp:ListItem Value="03" Text="03"></asp:ListItem>
                        <asp:ListItem Value="04" Text="04"></asp:ListItem>
                        <asp:ListItem Value="05" Text="05"></asp:ListItem>
                        <asp:ListItem Value="06" Text="06"></asp:ListItem>
                        <asp:ListItem Value="07" Text="07"></asp:ListItem>
                        <asp:ListItem Value="08" Text="08"></asp:ListItem>
                        <asp:ListItem Value="09" Text="09"></asp:ListItem>
                        <asp:ListItem Value="10" Text="10"></asp:ListItem>
                        <asp:ListItem Value="11" Text="11"></asp:ListItem>
                        <asp:ListItem Value="12" Text="12"></asp:ListItem>
                        <asp:ListItem Value="13" Text="13"></asp:ListItem>
                        <asp:ListItem Value="14" Text="14"></asp:ListItem>
                        <asp:ListItem Value="15" Text="15"></asp:ListItem>
                        <asp:ListItem Value="16" Text="16"></asp:ListItem>
                        <asp:ListItem Value="17" Text="17"></asp:ListItem>
                        <asp:ListItem Value="18" Text="18"></asp:ListItem>
                        <asp:ListItem Value="19" Text="19"></asp:ListItem>
                        <asp:ListItem Value="20" Text="20"></asp:ListItem>
                        <asp:ListItem Value="21" Text="21"></asp:ListItem>
                        <asp:ListItem Value="22" Text="22"></asp:ListItem>
                        <asp:ListItem Value="23" Text="23"></asp:ListItem>
                        <asp:ListItem Value="24" Text="24"></asp:ListItem>
                        <asp:ListItem Value="25" Text="25"></asp:ListItem>
                        <asp:ListItem Value="26" Text="26"></asp:ListItem>
                        <asp:ListItem Value="27" Text="27"></asp:ListItem>
                        <asp:ListItem Value="28" Text="28"></asp:ListItem>
                        <asp:ListItem Value="29" Text="29"></asp:ListItem>
                        <asp:ListItem Value="30" Text="30"></asp:ListItem>
                        <asp:ListItem Value="31" Text="31"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:DropDownList ID="DropDownList2" runat="server" CssClass="inputdropdown">
                        <asp:ListItem Value="MM" Selected="True" Text="MM"></asp:ListItem>
                        <asp:ListItem Value="01"  Text="Jan"></asp:ListItem>
                        <asp:ListItem Value="02"  Text="Feb"></asp:ListItem>
                         <asp:ListItem Value="03"  Text="Mar"></asp:ListItem>
                          <asp:ListItem Value="04"  Text="Apr"></asp:ListItem>
                           <asp:ListItem Value="05"  Text="May"></asp:ListItem>
                           <asp:ListItem Value="06"  Text="Jun"></asp:ListItem>
                         <asp:ListItem Value="07"  Text="Jul"></asp:ListItem>
                          <asp:ListItem Value="08"  Text="Aug"></asp:ListItem>
                           <asp:ListItem Value="09"  Text="Sep"></asp:ListItem>
                       <asp:ListItem Value="10"  Text="Oct"></asp:ListItem>
                          <asp:ListItem Value="11"  Text="Nov"></asp:ListItem>
                           <asp:ListItem Value="12"  Text="Dec"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:DropDownList ID="DropDownList3" runat="server" CssClass="inputdropdown" >
                    <asp:ListItem Value="YYYY" Selected="True" Text="YYYY"></asp:ListItem>
                    </asp:DropDownList>
                    
                    
</td>
</tr>
<tr>
<td class="style7">
Marital Status*
</td>
<td class="style8">:</td>
<td class="style7">
   <asp:RadioButtonList ID="RdMartialStatus" runat="server" AutoPostBack="true" 
        RepeatDirection="Horizontal" class="divtext" 
        onselectedindexchanged="RdMartialStatus_SelectedIndexChanged" >
   <asp:ListItem Value="Single" Text="Single"></asp:ListItem>
   <asp:ListItem Value="Married" Text="Married"></asp:ListItem>
   <asp:ListItem Value="Prefer Not to Say" Text="Prefer Not to Say"></asp:ListItem>
   </asp:RadioButtonList>
</td>
</tr>
<tr>
<td class="style7"><asp:LinkButton ID="LinkButton1" runat="server">Terms And Conditions</asp:LinkButton></td>
</tr>
</table>
<table class="style11">
<tr>
<td>
<asp:CheckBox ID="ChkTerms" runat="server" />
</td>
<td class="style7">
I have been through and accept the terms and conditions of the programme.
</td>
</tr>
<tr>
<td class="style7">
</td>
</tr>
</table>

<%--<table>
<tr><td class="style6"></td></tr>
<tr><td class="style6"></td></tr>
<tr><td class="style6"></td></tr>
<tr><td class="style6"></td></tr>
<tr>
<th class="style10">--%>

<div class="button">
<asp:LinkButton ID="btnSubmit" runat="server" Width="97px"  Text="Submit" 
        OnClientClick="javascript:return validInsert()" onclick="btnSubmit_Click"  ></asp:LinkButton>
<span></span>
</div>


<%--</th>
</tr>
</table>--%>

<%--<table class="style11">
<tr>
<td colspan="5">
<font color="red">
    <asp:Label ID="lblmsg" runat="server" Visible="true"></asp:Label>
</font>
</td>
</tr>
</table>--%>

<%--<div id="grayBG1" style="display:none;">ggggg</div>--%>

 <cc1:ModalPopupExtender ID="mo12" PopupControlID="dv_pop" BackgroundCssClass="modalBackground2"
        CancelControlID="btnClose" TargetControlID="LinkButton1" runat="server">
    </cc1:ModalPopupExtender>
    <div id="dv_pop" class="ModalWindow" style="display: none; width: 550px; height: 500px;"
        runat="server">
        <div style="overflow: auto; width: 530px; height: 460px; padding: 0px 10px 0px 10px;">
<p align="justif"><strong><u>Terms & Conditions</u> </strong></p>
<p align="justify"><b>Great Indian Nautanki Company Private Limited</b>  is a company engaged in the business of providing entertainment services and promoting performing arts through its, well known, entertainment and leisure destination called the Kingdom of Dreams ("said Premise" or "KOD"). KOD comprises of the Nautanki Mahal, Culture Gully, Show‐Shaa Theatre and the IIFA Buzz Lounge, through which KOD furthers the objective of showcasing live entertainment, handicrafts, Indian culture etc.The said Premise also has restaurants, bars, food & beverage stalls and venues for conducting events.</p>        
<p align="left"><strong><u>THE PROGRAMME</u> </strong></p>
<p align="left">The 'Royal Card Membership' programme (hereinafter referred to as the 'programme') is brought to Master Card World Card holders by The Kingdom of Dreams, Leisure Valley, Sector 29, Gurgaon, Haryana‐122002, and India (hereinafter referred to as "Kingdom of Dreams"</p>
<p align="left">The programme allows below mentioned Royal Card holders to earn and accumulate points, every time they add monetary value to the card, which can be used against food and beverage, retail purchases in Culture Gully & purchase of tickets for shows at the Nautanki Mahal.</p>
<p align="justify"><strong><u>ELIGIBILITY FOR THE PROGRAMME</u></strong></p>
<ul>
<p align="justify"> &#8226;The programme is open only to individuals who are Indian nationals of 18 years or above (Defined as individuals who have been accepted as members by Kingdom of Dreams and who have accepted the programme membership’s terms and conditions).</p>
<p align="justify"> &#8226;All the fields mentioned in the membership form must be filled by the member to become eligible, so as to gain membership and benefits to the programme.</p>
<p align="justify"> &#8226;For application All World Card members will have to apply through online registration process only. The applicant has to keep in mind that the information filled/provided by him is correct and accurate. KOD reserves the right to terminate the membership, if at any stage it comes to know that the information provided by the member is incorrect or fabricated.</p>
<p align="justify"> &#8226;Only one Platinum Card will be issued to every Master Card World Card Holder.</p>
<p align="justify"> &#8226;This offer is valid only for World Card members residing in Delhi-NCR.</p>
<p align="justify"> &#8226;Companies, partnership firms, unincorporated associations, groups or other such entities cannot enroll in the Royal Card programme.</p>
<p align="justify"> &#8226;This programme is not open to any of the Kingdom of Dreams employees, or their immediate relatives (spouse, children, parents, brothers and sisters). The programme is also not open to any franchisees of Kingdom of Dreams, their associates and all the Kingdom of Dreams employees who are associated with this programme directly or indirectly without permission of administration (Loyalty Manager).</p>
<p align="justify"> &#8226;Kingdom of Dreams reserves the right to grant or refuse membership to the programme.</p>
<p align="justify"><strong>Note:- 1) All members are requested to inform us in advance about the date and time of their visit to collect the membership card.</strong></p>
<p align="justify"><strong>2)  A Royal Platinum Card member would become a titanium member on spent of Rs 100000 through his Royal Platinum Card.</strong></p>
</ul>
<p align="Left"><strong><u>ENROLLMENT</u></strong></p>
<ul>
<p align="justify"> &#8226;Enrollment and participation in the programme is voluntary.</p>
<p align="justify">&#8226;Membership can be obtained by successfully filling up the application form available on our website royalty.kingdomofdreams.in or from the Loyalty desk at kingdom of Dreams. The
individual should fully qualify the eligibility criteria mentioned in the terms and conditions of the
Royal Card Membership Programme.</p>
<p align="justify">&#8226;There are 2 types of membership cards offered under the programme –</p>
<p align="justify">(i)	<b>Primary card</b> – Where the individual alone is enrolled into the programme.</p>
<p align ="justify">(ii)	<b>Add‐On card</b> – In addition to the primary card, another add‐on membership card is
offered to the member, wherein the add‐on card is linked to the primary membership card account and functions in the same way the Primary card functions.</p>
<p align ="justify">&#8226;All membership benefits, expiry of membership or points, card upgrade or downgrade criteria will be collectively applicable to the primary membership card as well as the linked add‐on card/s issued. On tier upgrade to the next membership level both cards will be upgraded together. Similarly, at the time of membership expiry, both cards will expire together.</p>
<p align="justify">&#8226;There is no annual participation fee for the programme.</p>
<p align="justify">&#8226;Any change of information as provided in the Royal Card Membership Programme enrollment form must be notified to the Kingdom of Dreams Programme in writing.</p>
</ul>
<p align="Left"> <strong><u>MEMBERSHIP CARD LEVELS AND BENEFITS</u></strong></p>
<ul>
<p align="justify"> &#8226;Kingdom of Dreams reserves the right to add, modify or delete tier level benefits and/or qualifications at its sole discretion, with or without notice.</p>
<p align="justify"> &#8226;In order to upgrade to a Titanium tier, a member has to complete that tier’s minimum amount of spend on recharging the membership card.</p>
<p align="justify"> &#8226;Eligible points are the base points that are earned by the Royal Card members against eligible transactions (for details please refer to Accrual of points below). All cumulative points rewarded as bonuses to members are ineligible towards tier accrual.</p>
<p align="justify"> &#8226;One point is equivalent to one rupee.</p>
<p align="justify"> &#8226;Member's tier period is a 12‐month rolling period starting from the date of membership. If any member fails to visit twice in a year (in a rolling period) his card will not be continued after the rolling period.</p>
<p align="justify"> &#8226;The benefits offered by Kingdom of Dreams Royal Card Programme are solely at the discretion of Kingdom of Dreams.</p>
<p align="justify"> &#8226;The current benefits and privileges offered by Royal Card Membership Programme vary across the membership tiers.</p>
</ul>
<p align="Left"> <strong><u>MEMBERSHIP TIERS:PLATINUM AND TITANIUM</u></strong></p>
<p align="Left"> <strong>1‐Royal Platinum Card [LEVEL 3 (Rs.50, 000‐Rs.99, 999)]</strong></p>
<p align="Left">All guests who have achieved a cumulative spend of Rs.50, 000 at Kingdom of Dreams, are eligible  for Royal Platinum Card membership. The Platinum members will be awarded with 12.5 points for every INR 100 (one hundred only) recharge at Kingdom of Dreams.</p>                     
<p align="left"><strong>BENEFITS</strong></p>
<ul>
<p align="justify"> &#8226;No Entry charge to Kingdom of Dreams (Culture Gully).</p>
<p align="justify"> &#8226;Earn points at the rate of 12.5% on every recharge of Rs.100.</p>
<p align="justify"> &#8226;No minimum spend criteria, spend as much as you want & balance will be carried forward.</p>
<p align="justify"> &#8226;Eligible for 6 upgrade vouchers.</p>
<p align="justify"> &#8226;Eligible for Birthday vouchers/points worth Rs.500 (Points valid for 30 days from DOB).</p>
<p align="justify"> &#8226;Eligible for Anniversary vouchers/points worth Rs.500 (Points valid for 30 days from Date of anniversary).</p>
<p align="justify"> &#8226;Maximum 8 people can be permitted through one Royal Platinum Card.</p>
<p align="justify"> &#8226;1 free Add‐On card offered to the members.</p>
<p align="justify"> &#8226;2 free Platinum Tickets for the Nautanki Mahal (Jhumroo/Zangoora Show).</p>
<p align="justify"> &#8226;Free Valet Parking.</p>
</ul>
<p align="Left"> <strong>2‐Royal Titanium Card [LEVEL 4 (Rs.1, 00,000 onwards)]</strong></p>
<p align="Left">All guests who have achieved a cumulative spend of Rs.1, 00, 000 at Kingdom of Dreams, are eligible to be upgraded to Royal Titanium Card membership. The Titanium Members will be awarded with 15 points for every INR 100 (one hundred only) recharge at Kingdom of Dreams.</p>                     
<p align="left"><strong>BENEFITS</strong></p>
<ul>
<p align="justify"> &#8226;No Entry charge to Kingdom of Dreams (Culture Gully).</p>
<p align="justify"> &#8226;Points earning is at the rate of 15% on every recharge of Rs.100.</p>
<p align="justify"> &#8226;No minimum spend criteria, spend as much as you want & balance will be carried forward.</p>
<p align="justify"> &#8226;Eligible for 8 upgrade vouchers.</p>
<p align="justify"> &#8226;Eligible for Birthday vouchers/points worth Rs.1,000 ( Points valid for 30 days from DOB).</p>
<p align="justify"> &#8226;Eligible for Anniversary vouchers/points worth Rs.1,000 ( Points valid for 30 days from Date of anniversary).</p>
<p align="justify"> &#8226;Maximum 10 people can be permitted through one Royal Titanium Card.</p>
<p align="justify"> &#8226;1 free Add‐On card offered to members.</p>
<p align="justify"> &#8226;4 Free Platinum Tickets for the Nautanki Mahal (Jhumroo/Zangoora Show).</p>
<p align="justify"> &#8226;Free Valet Parking.</p>
</ul>
<p align="Left"> <strong><u>OTHER BENEFITS ON UPGRADATION</u></strong></p>
<ul>
<p align="justify"> &#8226;On getting automatic enrollment in the Royal Platinum Card, the member will be eligible for the gift points worth INR 1,000 in the Royal Platinum Card, redeemable at Kingdom of Dreams.</p>
<p align="justify"> &#8226;On achieving a cumulative spends of Rs. 1,00,000 the member will be eligible for the gift points worth INR 2,500 in the Royal Titanium Card, redeemable at Kingdom of Dreams.</p>
</ul>
<p align="Left"> <strong><u>ACCRUAL OF POINTS</u></strong></p>
<ul>
<p align="justify"> &#8226;12.5% to 15% base points will be earned by the members for each recharge made through their Royal Card at Kingdom of Dreams. These points may vary and will be applicable for certain defined products/categories. These products/categories can change from points earned at the sole discretion of Kingdom of Dreams.</p>
<p align="justify"> &#8226;While making purchases at the Kingdom of Dreams, members need to present their Membership Card to the cashier before billing. Accordingly, points earned on eligible spends will be credited to their membership account upon presentation of their "Royal Card".</p>
<p align="justify"> &#8226;Points accumulated by a member on his/ her Royal Card cannot be combined or used in
conjunction with any other offers at the time of redemption.</p>
<p align="justify"> &#8226;The points do not constitute property of the member and are not transferable by operation of law or otherwise to any other person or entity except to an Add‐On card linked to the primary membership card.</p>
<p align="justify"> &#8226;Members cannot accrue points for any purchases incurred prior to his/her membership date.</p>
<p align="justify"> &#8226;In the event of voluntary closure of Royal Card Membership by the member, the points accumulated on his/her Royal Card can be redeemed within 3 months of closure, otherwise these will automatically lapse. In the event of cancellation of the Royal Card Membership Card for any reason, all the points accumulated will stand forfeited. If the Royal Card Membership Card is blocked or suspended by Kingdom of Dreams for any reason whatsoever, then the points
accumulated shall stand forfeited, but may be reinstated at the discretion of Kingdom of Dreams if the use of Royal Card Membership Card is reinstated.</p>
<p align="justify"> &#8226;The allotment and redemption of points will be solely at the discretion of the Kingdom of
Dreams management.</p>
<p align="justify"> &#8226;Kingdom of Dreams’ computation of points shall be final, conclusive and binding and will not be liable to be disputed or questioned, except in case of manifest error.</p>
</ul>
<p align="Left"> <strong><u>IMPORTANT GUIDELINES</u></strong></p>
<ul>
<p align="justify"> &#8226;Please ask the executive at the billing counter/Box office to recharge the Royal card before making any payment.</p>
<p align="justify"> &#8226;Please present your Royal card to the executive at the box office before printing of the tickets.</p>
<p align="justify"> &#8226;Make your payments only through your Royal Card, to earn loyalty points and avail benefits of the tier up‐gradation.</p>
<p align="justify"> &#8226;Any recharge on the Royal Card is non‐refundable and non‐transferable.</p>
<p align="justify"> &#8226;The loyalty points and recharge value will lapse on the last day of the validity of the Royal Card.</p>
<p align="justify"> &#8226;The Royal Card or accumulated Loyalty points cannot be used to purchase a smart card.</p>
<p align="justify"> &#8226;Loyalty points of two or more cards cannot be used together in purchasing.</p>
<p align="justify"> &#8226;Upgrade vouchers will not be applicable on the highest and lowest category of the tickets to Nautanki Mahal.</p>
<p align="justify"> &#8226;The points accrued and credited will be available for redemption only after a period of 24 hrs from the time the points were earned.</p>
<p align="justify"> &#8226;No claim will be entertained after purchasing & printing of the tickets.</p>
</ul>
<p align="Left"> <strong><u>POINTS WILL NOT BE AWARDED OR EARNED IN THE FOLLOWING CASES: ‐</u></strong></p>
<ul>
<p align="justify"> &#8226;Special offers / promotions / items excluded by the management. Kingdom of Dreams reserves the right to withdraw any one or all the cards issued by them. On a card being withdrawn, Kingdom of Dreams will be responsible for the card holder only to the extent of the points in the credit of the cardholder’s membership account.</p>
<p align="justify"> &#8226;Bonus points like Birthday Vouchers/Points can be earned on special product categories.
These categories can change at the sole discretion of Kingdom of Dreams.</p>
<p align="justify"> &#8226;Base points can be earned on the purchase of certain defined products. These products may vary, based on the discretion of Kingdom of Dreams.</p>
<p align="justify"> &#8226;Members cannot transfer any points to any another person.</p>
</ul>
<p align="Left"> <strong><u>REDEMPTION OF POINTS</u></strong></p>
<ul>
<p align="justify"> &#8226;The points accrued can only be redeemed by the member themselves.</p>
<p align="justify"> &#8226;The points can be redeemed at all the counters available inside the premises of Kingdom of Dreams to pay for a variety of products and services available in the Nautanki Mahal or Culture Gully.</p>
<p align="justify"> &#8226;The points accrued and credited will be available to be redeemed only after 24 hrs from the time the points were earned.</p>
<p align="justify"> &#8226;All rewards are subject to availability and certain restrictions may apply.</p>
<p align="justify"> &#8226;Any gift vouchers issued to members are valid for redemption only at the Kingdom of
Dreams.</p>
<p align="justify"> &#8226;Member's signature is subject to verification at the time of purchase or redemption.</p>
<p align="justify"> &#8226;No cash refund will be entertained for purchases made on your membership card.</p>
<p align="justify"> &#8226;If the gift vouchers issued against the points are lost, Kingdom of Dreams will not be responsible and no duplicate gift vouchers will be issued in this regards.</p>
<p align="justify"> &#8226;Points once redeemed against a purchase can in no event be re‐credited.</p>
<p align="justify"> &#8226;Points cannot be redeemed for cash.</p>
<p align="justify"> &#8226;No accumulation or redemption of points will be permissible if, on the relevant date, the card has been withdrawn, cancelled, is liable to be cancelled or if there is any breach of any clause of the terms and conditions. The points will lapse on the date of closure/cancellation of card.</p>
<p align="justify"> &#8226;On redemption, points redeemed would be automatically deducted from the accumulated points in the member’s card.</p>
<p align="justify"> &#8226;Points cannot be redeemed on special occasions e.g. 31st December (special occasions to be defined by Kingdom of Dreams as and when required).</p>
</ul>
<p align="Left"> <strong><u>TERMINATION</u></strong></p>
<ul>
<p align="justify"> &#8226;The member may opt out of the programme by providing a written intimation to
Kingdom of Dreams.</p>
<p align="justify"> &#8226;The membership will be valid for 2 years from the date of enrollment, which will be extended incase the Royal Card holder carries out a re‐charge of monetary value at Kingdom of Dreams within 12 months of his membership expiry date. In case the member is unable to fulfill the membership extension criteria, the membership will expire on the completion of 2 years from the date of enrollment in the programme.</p>
<p align="justify"> &#8226;On membership expiry, all balance points available in the member’s account, earned during the membership period will also expire, if not redeemed.</p>
<p align="justify"> &#8226;Kingdom of Dreams reserves the right to refuse membership to any applicant without assigning any reason.</p>
</ul>
<p align="Left"> <strong><u>GENERAL</u></strong></p>
<ul>
<p align="justify"> &#8226;Fraud or abuse concerning the Royal Card Membership Programme is subject to appropriate administrative and/or legal action by Kingdom of Dreams, including termination of membership.</p>
<p align="justify"> &#8226;Information provided by a member on enrollment may be used by the management of
Kingdom of Dreams for administrative and/or marketing purposes.</p>
<p align="justify"> &#8226;Kingdom of Dreams reserves the right to terminate the membership, if it any point of time, comes to know that the information provided by the member is false or fabricated.</p>
<p align="justify"> &#8226;Kingdom of Dreams reserves the right to alter the terms and conditions as well as rules and regulations governing this programme or cancel this programme without prior notice from time to time.</p>
<p align="justify"> &#8226;Kingdom of Dreams reserves the right to refuse to award points or withdraw points or refuse the right to redeem points accumulated for any breach of these conditions or failure to pay for the purchases.</p>
<p align="justify"> &#8226;Any dispute concerning goods or services received as rewards under the programme shall be settled between the member and the designated Kingdom of Dreams representative. In such a situation the ruling of the Kingdom of Dreams representative will be liable for the legal action.</p>
<p align="justify"> &#8226;All queries in relation to the programme may be addressed to the Royal Card Programme
Manager at loyalty.programme@kingdomofdreams.co.in.</p>
<p align="justify"> &#8226;The membership card remains the property of Kingdom of Dreams and the cardholder will be the custodian of the same.</p>
<p align="justify"> &#8226;Membership cards lost shall be the sole responsibility of the cardholder and such loss should be intimated to Kingdom of Dreams immediately. The establishment will not be responsible for any lost or stolen cards.</p>
<p align="justify"> &#8226;If the membership card is lost/stolen or damaged, a new card will be issued to the member wherein a charge of Rs.50/‐ will be applicable to the member, for issuing of the duplicate card.</p>
<p align="justify"> &#8226;This membership card cannot be used as a credit card for any purchase.</p>
<p align="justify"> &#8226;All disputes in respect of this programme shall be subject to the exclusive jurisdiction of courts at Gurgaon, Haryana only.</p>
<p align="justify"> &#8226;The Kingdom of Dreams Royal Card membership will be allotted purely at the discretion of the management and the final decision on all matters relating to the card shall rest with  Kingdom of Dreams.</p>
<p align="justify"> &#8226;This programme is subject to force majeure conditions.</p>
<p align="justify"> &#8226;All decisions of Kingdom of Dreams pertaining to the programme including but not limited to procedure for enrollment of members to programme, accumulation of points, redemption of points, etc shall be final and binding on all the members of programme.</p>
<p align="justify"> &#8226;Kingdom of Dreams reserves the right to terminate the programme at any time and/or change, modify, withdraw or extend the terms of programme and/or terms and conditions or replace wholly or partially by another programme, without any prior notice and no correspondence in this regard shall be entertained.</p>
<p align="justify"> &#8226;Kingdom of Dreams reserves the right to disqualify any member of programme if it has reasonable grounds to believe the participant has breached any of these terms and conditions.</p>
<p align="justify"> &#8226;The decision of the management of Kingdom of Dreams shall be final and binding.</p>
<p align="justify"> &#8226;The Royal card value/cash or points accumulated by a member on his/her Royal card cannot be combined or used in conjunction with any other offers at the time of redemption.</p>
</ul>
<p align="center" style="color:Red">END </p>
</div>

        <hr />
        <asp:Button Text="Close" runat="server" Class="buttontickets" ID="btnClose" />
    </div>

     <div id="grayBG" runat="server" class="grayBox" style="display:inline;">
   <div id="showcontainer" runat="server" class="box_content" style="border:solid 1px #FF9900;display:inline;"  >
    </div>         
<div id="Container" class="box_content1" runat="server" style="background-color:White;display:inline;">
<center><p style="color: #000000"><b><asp:Label ID="lblmsg" runat="server" Visible="true"></asp:Label></b></p></center> 
<br /><br />
<hr />
<asp:Button ID="Button1" class="buttontickets" runat="server" Text="OK" onclick="Button1_Click" 
        />
<%--<asp:Button ID="btnproceed" class="buttontickets" runat="server" Text="Proceed" 
        onclick="btnproceed_Click" />--%>
</div></div>
    <script language="javascript" type="text/javascript" src="~/js/jquery-1.8.2.min.js"></script>
    <script language="javascript" type="text/javascript">
       function validInsert() {
         
            if ($("[id*=txtFirstName]").val() == "") {
                alert("Please enter First name");
                $("[id*=txtFirstName]").focus();
                return false;
            }
            if ($("[id*=txtLastName]").val() == "") {
                alert("Please enter Last name");
                $("[id*=txtLastName]").focus();
                return false;
            }
            if ($("[id*=txtAddress]").val() == "") {
                alert("Please enter your Address");
                $("[id*=txtAddress]").focus();
                return false;
            }
            if ($("[id*=txtCity]").val() == "") {
                alert("Please enter your City");
                $("[id*=txtCity]").focus();
                return false;
            }
            if ($("[id*=DdlCountry]").val() == "") {
                alert("Please enter your Country");
                $("[id*=DdlCountry]").focus();
                return false;
            }
            if ($("[id*=txtEmailId]").val() == "") {
                alert("Please enter Email Id");
                $("[id*=txtEmailId]").focus();
                return false;
            }
           
            var emailPat = /^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*/;
            var emailid = $("[id*=txtEmailId]").val();
            var matchArray = emailid.match(emailPat);
            if (matchArray == null) {
                alert("Please Enter Valid Email ID");
                $("[id*=txtEmailId]").focus();
                return false;
            }
            if (($("[id*=txtpin]").val()).length < 6 && ($("[id*=txtpin]").val()).length>0) {
                alert("Pin no should be minimum 6 digits");
                $("[id*=txtpin]").focus();
                return false;
            }
           
            if ($("[id*=txtMobileNo]").val() == "") {
                alert("Please enter mobile no");
                $("[id*=txtMobileNo]").focus();
                return false;
            }
            if (IsNumeric($("[id*=txtMobileNo]").val()) == false) {
                alert("Please enter Numeric Only");
                $("[id*=txtMobileNo]").focus();
                return false;
            }

            if (($("[id*=txtMobileNo]").val()).length < 10) {
                alert("Mobile no should be minimum 10 digits");
                $("[id*=txtMobileNo]").focus();
                return false;
            }
            if (($("[id*=txtMobileNo]").val()).length > 10) {
                alert("Mobile Number Should be of 10 digits only");
                $("[id*=txtMobileNo]").focus();
                return false;
            }
        }
    </script>
    <script language="JavaScript" type="text/javascript">

        function IsNumeric(strString)
        //  check for valid numeric strings	
        {
            //   var strValidChars = "0123456789.-";
            var strValidChars = "0123456789";
            var strChar;
            var blnResult = true;

            if (strString.length == 0) return false;

            //  test strString consists of valid characters listed above
            for (i = 0; i < strString.length && blnResult == true; i++) {
                strChar = strString.charAt(i);
                if (strValidChars.indexOf(strChar) == -1) {
                    blnResult = false;
                }
            }
            return blnResult;
        }
    </script>   
</asp:Content>

