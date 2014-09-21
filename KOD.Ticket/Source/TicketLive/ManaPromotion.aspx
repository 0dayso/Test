<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ManaPromotion.aspx.cs" Inherits="ManaPromotion" %>
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
    background: url(images/ticket-booking-bgg.jpg) no-repeat;
    overflow: auto;
    height: 550px;
    padding-left:245px;
    *padding-left:245px;
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
                <div class="ticket-main"><br />
                 <table>
                <tr>
                <td colspan="2">Family Package:</td>
                <td colspan="2">
                <asp:DropDownList ID="ddl_package" runat="server" Width="150">
                <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                <asp:ListItem Value="1" Text="Weekday,Rs.3999"></asp:ListItem>
                 <asp:ListItem Value="2" Text="Weekend,Rs.4999"></asp:ListItem>
                </asp:DropDownList>
                </td>
                </tr>
                <tr> 
                <td colspan="2" >No. of Package:</td>
                    <td colspan="2" >
                         <asp:DropDownList ID="ddl_noofpackage" runat="server" Width="83px">
                         <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                         <asp:ListItem Value="1" Text="1"></asp:ListItem>
                         <asp:ListItem Value="2" Text="2"></asp:ListItem>
                          <asp:ListItem Value="3" Text="3"></asp:ListItem>
                         <asp:ListItem Value="4" Text="4"></asp:ListItem>
                          <asp:ListItem Value="5" Text="5"></asp:ListItem>
                        </asp:DropDownList>  
                    </td>
                </tr>

                <%--tr>
                    <td colspan="2" >
                     Promotion Code:  
                    </td>
                    <td colspan="2">
                    <asp:TextBox ID="txtMANAPromotionCode" Width="140px" runat="server" CssClass="text-small"></asp:TextBox>
                    </td>
                </tr--%>
                
              <%--  <tr>
                <td colspan="2">
                Date:                   
                </td>
                <td>
                    <asp:DropDownList ID="ddl_date" runat="server">
                    </asp:DropDownList>
                </td>
                </tr>--%>
               <%--<tr>
               
              <td colspan="2">
                <asp:LinkButton ID="TCiNdigo" runat="server" CssClass="clickhere">Terms & Conditions</asp:LinkButton>
                </td>
                </tr>
                 <tr>
                    <td colspan="5" >
                     <asp:CheckBox runat="server" ID="chkterms" /> I Agree to the Terms and Conditions.
                    </td>
                </tr>--%>
                 <tr>
                    <td colspan="5" >
                     <font color="red">
                     <asp:Label ID="lblMsgPromotionCode" runat="server" Text="Label"></asp:Label></font>
                    </td>
                </tr>
                  <tr>
                <td colspan="2">
                <asp:Button ID="btnMANAPromotionCode" runat="server" CssClass="common-button" 
                Text="Submit" onclick="btnMANAPromotionCode_Click" />
                </td>
                </tr>
               </table>
   <%-- <cc1:modalpopupextender ID="mo12" PopupControlID="dv_pop" BackgroundCssClass="modalBackground2"
        CancelControlID="btnClose" TargetControlID="TCiNdigo" runat="server">
    </cc1:modalpopupextender>
     <div id="dv_pop" class="ModalWindow" style="display: none; width: 550px; height: 500px;"
        runat="server">
              
        <div id="Div1" runat="server" style="overflow: auto; width: 530px; height: 460px; padding: 0px 10px 0px 10px">

           
         <font color="red"> <u><center><b>Terms & Conditions</b></center></u></font>
           <p>
           <ol>
           <li>Offer is valid for MakeMyTrip customers making UAE-India-UAE round trip flight bookings online with
                MakeMyTrip through MakeMytrip’s UAE and US website http://www.makemytrip.ae/ And US website
                http://us.makemytrip.com</li>
              <li> Discount will be applicable subject to use of MakeMyTrip Booking Id as deal code at the time of
                redemption with KOD along with Promo Code: MMTKOD00001 at the time of redemption with KOD</li>
              <li> Offer is valid on above mentioned Flight bookings made with MakeMyTrip between 28th May, 2013 to
                27th August, 2013. In order to be eligible for the offer, the travel must be between 28th May, 2013 to 30th
                November, 2013.</li>
              <li>In order to be eligible for the discount, the redemption with KOD must be done on or before 30th
                November, 2013.</li>
              <li>This offer once booked cannot be cancelled.</li>
              <li>Offer is not valid on infant (0-2 years) tickets.</li>
              <li>For offline purchase of tickets at KOD, the passenger needs to bring the copy of MakeMyTrip eticket
                containing the booking id at the time of purchasing the tickets at KOD.</li>
              <li>For online purchase of KOD tickets, in order to avail of the discount, the customer must input the
                correct MakeMyTrip booking ID at the time of booking tickets with KOD. Neither MakeMyTrip nor KOD
                shall be liable for the failure of the customer to input the correct MakeMyTrip booking ID.</li>
              <li> A customer can book up to 4 discounted KOD packages on one booking id.</li>
              <li>The discount will be applicable on per package basis subject to maximum of four packages per booking ID.</li>
              <li>The discount amount cannot be cashed.</li>
              <li>The discount offer cannot be combined with any other ongoing offers, discounts or promotions on KOD.</li>
              <li>The offer shall be subject to the availability of tickets with KOD.</li>
              <li>By booking the tickets with KOD, the customer accepts all terms and conditions specified on
                www.kingdomofdreams.in and behind the ticket.</li>
              <li> In the event of cancellation of show by KOD, KOD displays the information about such cancellation and
                rescheduling of show on its website and the customers may visit KOD’s website for any related information.</li>
              <li> In no event shall the customer be entitled to the refund of discount amount in case of no-show.</li>
              <li>In the event of cancellation of booking with MakeMyTrip, the booking ID shall become invalid and the
                same cannot be used for the purposes of claiming any discount under the said offer.</li>
              <li>KOD and MakeMyTrip reserve the right to jointly change/modify/add/delete any of the terms and conditions of the offer.</li>
              <li>In case of any query pertaining to the offer, please email us at info@kingdomofdreams.co.in or Call @0124-6677000</li>
              <li>KOD and MakeMyTrip have right to terminate the offer at any point without prior communication or liability.</li>
              <li>Web bookings are available for the next two months only from the current date onwards.
                  If you want to book the tickets for a later date then please visit us again.</li>

              </ol>
               </p>
                </div>  
       
    <hr />
        <asp:Button Text="Close" runat="server" ID="btnClose" CssClass="common-button"  />

        </div>
                 
        
    </div>--%>
     
            
           
            
                    
                </div>
            </div>
        </div>
        <!-- seats-main ends here -->
    </div>

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
