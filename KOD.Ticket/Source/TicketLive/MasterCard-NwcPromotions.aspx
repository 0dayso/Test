<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MasterCard-NwcPromotions.aspx.cs" Inherits="WorldCardOthers" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="Controls/Footer.ascx" TagName="Footer" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<style type="text/css">
    .txtcolor
{
    border-color: #E7C54A;
    color:#E7C54A;
}
 .ModalWindow
        {
            background-color: #000000;
            border-width: 3px;
            border-style: solid;
            border-color: #E7C54A;
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
	height:130px;
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
	height:120px;
	display:none;
	z-index:9999;
	padding:20px;
	top:206px;
	left:406px;
	background-color: #000000;
	
} 
      
       
       
.seats-inside1
{
    width: 846px;
    float: left;
    background: url(images/revisedmastercardpromotion.jpg) no-repeat;
    overflow: auto;
    height: 489px;
    padding-left:245px;
    *padding-left:245px;
}
.style1
{
    width: 207px;
}
    </style>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Kingdom of Dreams : Ticket Booking</title>
    <link href="css/style.css" type="text/css" rel="stylesheet" />
    <link rel="shortcut icon" type="image/x-icon" href="images/favicon.ico" />
   <%-- <asp:ContentPlaceHolder ID="head" runat="server">
    </asp:ContentPlaceHolder>--%>

</head>
<body class="home-page-bg">
    <form id="form1" runat="server">    
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="True">
    </asp:ScriptManager>
    <div class="wrapper">
        <div class="logo-row">
            <div class="logo">
                <a href="http://kingdomofdreams.in/index.html" target="_blank">
                    <img src="images/logo.jpg" /></a>
            </div>
        </div>
        <!--logo-row ends here -->
        <div class="seats-main">
            <div class="seats-inside1">
                <div class="ticket-main"><br /><br /><br /><br /><br /><br /><br /><br />
                 <table>
                      <tr>
                 <td colspan="4">
                 Enter Your Master Card Number Below (First Six Digits):
                 </td>
                 </tr>
                  <tr> 
                    <td colspan="2" ><asp:TextBox ID="Txtcardno" runat="server" MaxLength="6"></asp:TextBox>
                <cc1:FilteredTextBoxExtender ID="F1" FilterType="Numbers" TargetControlID="Txtcardno" runat="server">
                </cc1:FilteredTextBoxExtender></td>
                </tr>
                  <tr>
                 <td colspan="2">
                 Select Your Bank Name:
                 </td>
                 </tr>
                 <tr> 
                    <td colspan="2">
                         <asp:DropDownList ID="ddlbankname" runat="server" Width="150px" >
                         <asp:ListItem Value="Select Bank Name" Text="Select Bank Name"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                 <%-- <tr>
                    <td colspan="5" >
                     <font color="red">
                     <asp:Label ID="Label1" runat="server" Text="">*Please enter your promotion code and click on Submit button</asp:Label></font>
                    </td>
                </tr>--%>
                <tr>
                    <td colspan="2" >
                     Promotion Code:  
                    </td>
                    </tr><tr>
                    <td colspan="2">
                    <asp:TextBox ID="txtMCPromotionCode" Width="140px" runat="server" CssClass="text-small"></asp:TextBox>
                    </td>
                </tr>
                     <td colspan="2">
                <asp:LinkButton ID="TCiNdigo" runat="server" CssClass="clickhere">Terms & Conditions</asp:LinkButton>
                </td>
                </tr>
                <tr>
                    <td colspan="5" >
                     <asp:CheckBox runat="server" ID="chkterms" /> I Agree to the Terms and Conditions.
                    </td>
                </tr>
                 <tr>
                 <tr>
                    <td colspan="5" >
                     <font color="red">
                     <asp:Label ID="lblMsgPromotionCode" runat="server" Text=""></asp:Label></font>
                    </td>
                </tr>
                  <tr>
                <td colspan="2">
                <asp:Button ID="btnMCPromotionCode" runat="server" CssClass="common-button" 
                Text="Submit" onclick="btnMCPromotionCode_Click"/>
                </td>
                </tr>
               </table>
    <cc1:ModalPopupExtender ID="mo12" PopupControlID="dv_pop" BackgroundCssClass="modalBackground2"
        CancelControlID="btnClose" TargetControlID="TCiNdigo" runat="server">
    </cc1:ModalPopupExtender>
     <div id="dv_pop" class="ModalWindow" style="display: none; width: 550px; height: 500px;"
        runat="server">
              
        <div id="Div1" runat="server" style="overflow: auto; width: 530px; height: 460px; padding: 0px 10px 0px 10px">

           
         <font color="red"> <u><center><b>Terms & Conditions</b></center></u></font>
           <p>
           <ol>
              <li>This Offer/scheme is made solely and entirely by Kingdom Of Dreams (hereinafter referred to as “KOD”) for MasterCard Card Holders.</li>
              <li>Offer is valid for both MasterCard holders and Master Card World Card holders.<br />
For Master Card World Cardholders:- 			&nbsp;Diamond Category- 25%<br /> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Platinum Category- 15%<br />

For other than Master card World Cardholders:-		&nbsp;Gold Category- 20%<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Silver Category- 15%
</li>
              <li>Discount will be applicable subject to use of MasterCard identification as deal code at the time of redemption with KOD along with proper Promo at the time of redemption with KOD.</li>
              <li>Offer is valid between 28-Sep-13 to 31-May-14(hereinafter referred to as “offer period”). In order to be eligible for the offer, the bookings must be between 28-Sep-13 to 31-May-14.</li>
              <li>In order to be eligible for the discount, the redemption with KOD must be done on or before 31-May-14.</li>
              <li>MasterCard is a promotion partner and shall not be liable for any claims arising out of product or services of KOD. Any claims related to the offer shall be made against KOD subject to standard terms and conditions as applicable on tickets.</li>
              <li>This offer once booked cannot be cancelled.</li>
              <li>Offer is not valid on infant (0-2 years) tickets.</li>
              <li>For offline purchase of tickets at KOD, the passenger needs to come to KOD box office and present a valid MasterCard debit/credit card and authorize the charge on the same.</li>
              <li>For online purchase of KOD tickets, in order to avail the discount, the customer must input the correct MasterCard number at the time of booking tickets with KOD. Neither MasterCard nor KOD shall be liable for the failure of the customer to input the correct number.</li>
              <li>A customer can book up to 4 discounted KOD tickets per transaction. A customer can do a maximum of one transaction a day.</li>
              <li>The discount will be applicable on per ticket/package basis subject to maximum of four tickets/packages per booking ID.</li>
              <li>The discount amount cannot be en-cashed.</li>
              <li>The discount offer cannot be combined with any other ongoing offers, discounts or promotions at KOD.</li>
              <li>The offer shall be subject to the availability of tickets at KOD.</li>
              <li>By booking the tickets with KOD, the customer accepts all terms and conditions specified on www.kingdomofdreams.in and behind the ticket.</li>
              <li>MasterCard shall not be liable for any loss or damage that may be suffered, or for any personal injury that may be suffered as a result of the offer.</li>
            <li>In the event of cancellation of show by KOD, KOD displays the information about such cancellation and rescheduling of show on its website and the customers may visit KOD’s website for any related information.</li>
            <li>In no event shall the customer be entitled to the refund of discount amount in case of no-show.</li>
            <li>In the event of expiry of MasterCard card, the same cannot be used for the purposes of claiming any discount under the said offer.</li>
            <li>KOD and MasterCard reserve the right to jointly change/modify/add/delete any of the terms and conditions of the offer without prior communication.</li>
            <li>In case of any query pertaining to the offer, please email us at info@kingdomofdreams.co.in or Call @0124-6677000.</li>
            <li>KOD and MasterCard reserve the right to terminate/discontinue the offer at any point without prior communication and without assigning any reason even before the offer period.</li>
            <li>MasterCard and KOD reserve the right to deny the offer on the grounds of suspicion or abuse of the offer by any customer.</li>
            <li>MasterCard and KOD cannot be held liable for any act and omission attributable to force majeure events.</li>
            <li>All disputes arising under the offer are subject to jurisdiction of courts in Gurgaon.</li>
            <li>The customer agrees and undertakes to indemnify and keep indemnified KOD, its associates, employees and agencies from any suit, claim, cost or damages arising out of his conduct in availing the offer during the offer period.</li>
            <li>In all matters relating to the offer, the decision of management shall be final and binding in all respects.</li>
              </ol>
               </p>
                </div>  
       
    <hr />
        <asp:Button Text="Close" runat="server" ID="btnClose" CssClass="common-button"  />

        </div>
                 
        
    </div>
     
            
           
            
                    
                </div>
            </div>
        </div>
        <!-- seats-main ends here -->
 <%-- <script type="text/javascript">
      function Changeing() {
          var val = document.getElementById("<%=ddl_type.ClientID %>").value;
          var val2 = document.getElementById("<%=divshow.ClientID %>");
          var val3 = document.getElementById("<%=div2.ClientID %>");
          if (val == "Package") {
              val2.style.display = "inline";
              val3.style.display = "inline";
          } else {
              val2.style.display = "none";
              val3.style.display = "none";
          }

      }
    </script>--%>

     <%-- <div id="grayBG" runat="server" class="grayBox" style="display:inline;">
   <div id="showcontainer" runat="server" class="box_content" style="border:solid 1px #FF9900;display:inline;"  >
    </div>         
<div id="Container" class="box_content1" runat="server" style="background-color:White;display:inline;">
<center><p style: style="color: #000000"><b>“Sorry, to book for later date please visit again after x days”</b></p></center>      
</div></div>--%>




    <uc1:Footer ID="Footer1" runat="server" />
    
    
    <!-- wrapper ends here -->
    <%--<uc1:Footer ID="Footer1" runat="server" />--%>
    <!-- footer-main ends here -->
    </form>
</body>
</html>

