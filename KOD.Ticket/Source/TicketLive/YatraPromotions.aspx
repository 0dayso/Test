<%@ Page Language="C#" AutoEventWireup="true" CodeFile="YatraPromotions.aspx.cs" Inherits="YatraPromotions" %>
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
    background: url(images/yatrapromotion.jpg) no-repeat;
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
    <script language="javascript" type="text/javascript">
        history.forward();
    </script>
    
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
                <div class="ticket-main"><br /><br /><br /><br /><br /><br /><br /><br /><br />
                <br /><br /><br /><br /><br /><br />
                 <table>
                
                <tr> 
                <td colspan="2" >No. of Tickets:</td>
                    <td colspan="2" >
                         <asp:DropDownList ID="ddl_nooftickets" runat="server" Width="70">
                         <asp:ListItem Value="1" Text="1"></asp:ListItem>
                         <asp:ListItem Value="2" Text="2"></asp:ListItem>
                          <asp:ListItem Value="3" Text="3"></asp:ListItem>
                         <asp:ListItem Value="4" Text="4"></asp:ListItem>
                        </asp:DropDownList>  
                    </td>
                </tr>

                <tr>
                    <td colspan="2" >
                     Promotion Code:  
                    </td>
                    <td colspan="2">
                    <asp:TextBox ID="txtYatraPromotionCode" Width="140px" runat="server" CssClass="text-small"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                <td colspan="2">
                Booking Id:                   
                </td>
                <td colspan="2">
                <asp:TextBox ID="txtYatraPNR" Width="140px" runat="server" CssClass="text-small"></asp:TextBox>
                </td>
                </tr>
              <%--  <tr>
                <td colspan="2">
                Date:                   
                </td>
                <td>
                    <asp:DropDownList ID="ddl_date" runat="server">
                    </asp:DropDownList>
                </td>
                </tr>
                <tr>--%>
               
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
                    <td colspan="5" >
                     <font color="red">
                     <asp:Label ID="lblMsgPromotionCode" runat="server" Text="Label"></asp:Label></font>
                    </td>
                </tr>
                  <tr>
                <td colspan="2">
                <asp:Button ID="btnMMTPromotionCode" runat="server" CssClass="common-button" 
                Text="Submit" onclick="btnMMTPromotionCode_Click" />
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
           <li>This Offer is made solely and entirely by KOD for Yatra customers using Yatra’s Indian website www.Yatra.com.</li>
           <li>Offer is valid for all Yatra customers making Flight, Flight plus Hotels and Domestic Hotels bookings on www.Yatra.com.</li>
           <li> The offer can be passed on to friends and relatives by the client who has made booking on Yatra.com provided the person who is availing the offer is able to produce the booking reference.</li>
           <li>The offer is valid only on online bookings made on www.Yatra.com.</li>
           <li>Discount will be applicable subject to use of Yatra Booking Id as deal code at the time of redemption with KOD.</li>
           <li>Offer is valid on all Flight, Flight plus Hotels and Domestic Hotels bookings made for ……. days with Yatra between ……………, 2013 to ……… 2013 (will specify the dates). The offer can be redeemed one month from the date of expiry of the offer. Yatra is a promotion partner and shall not be liable for any claims arising out of product or services of KOD. Any claims related to the offer shall be made against KOD.</li>
           <li>This offer once availed cannot be cancelled.</li>
           <li>For offline purchase of tickets at KOD, the passenger needs to bring the copy of Yatra e-ticket containing the booking id at the time of purchasing the tickets at KOD. For online purchase of KOD tickets, in order to avail of the discount, the customer must input the correct Yatra booking ID at the time of booking tickets with KOD. Neither Yatra nor KOD shall be liable for the failure of the customer to input the correct Yatra booking ID.</li>
           <li>A customer can book up to 4 discounted KOD tickets on one booking id.</li>
           <li>The discount will be applicable on per ticket basis subject to maximum of four tickets per booking ID.</li>
           <li>The discount amount cannot be cashed.</li>
           <li>The discount offer cannot be combined / clubbed with any other ongoing offers, discounts or promotions on KOD.</li>
           <li>The offer shall be subject to the availability of tickets with KOD.</li>
           <li>By booking the tickets with KOD, the customer accepts all terms and conditions specified on website and on tickets of the KOD.</li>
           <li>Yatra shall not be liable for any loss or damage that may be suffered, or for any personal injury that may be suffered as a result of the offer.</li>
           <li>In the event of cancellation of show by KOD, KOD displays the information about such cancellation and rescheduling of show on its website and the customers may visit KOD’s website for any related information.</li>
           <li>In no event shall the customer be entitled to the refund of discount amount in case of no-show.</li>
           <li>In the event of cancellation of booking with Yatra, the booking ID shall become invalid and the same cannot be used for the purposes of claiming any discount under the said offer.</li>
           <li>KOD and Yatra reserve the right to jointly change/modify/add/delete any of the terms and conditions of the offer.</li>
           <li>In case of any query pertaining to the offer, please email us at info@kingdomofdreams.co.in or Call Toll Free @ 0124-6677000.</li>
           <li>KOD and Yatra have right to terminate the offer (subject to clause 10) at any point without prior communication or liability.</li>
           <li>Yatra and KOD reserve the right to deny the offer on the grounds of suspicion or abuse of the offer by any customer.</li>
           <li>Yatra and KOD cannot be held liable for any act and omission attributable to force majeure events.</li>
           <li>All disputes arising under the offer are subject jurisdiction of courts of Gurgaon.</li>
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
