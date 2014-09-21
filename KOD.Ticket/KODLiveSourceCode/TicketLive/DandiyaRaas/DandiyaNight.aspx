<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DandiyaNight.aspx.cs" Inherits="DandiyaRaas_DandiyaNight" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../Controls/Footer.ascx" TagName="Footer" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <style type="text/css">
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
        .seats-inside1
{
    width: 846px;
    float: left;
    background: url(../images/ticketbooking_dandia_offer.jpg) no-repeat;
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
    <script type="text/javascript">
        history.forward();
    </script>

    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Kingdom of Dreams : Ticket Booking</title>
    <link href="../css/style.css" type="text/css" rel="stylesheet" />
    <link rel="shortcut icon" type="image/x-icon" href="../images/favicon.ico" />
   <%-- <asp:ContentPlaceHolder ID="head" runat="server">
    </asp:ContentPlaceHolder>--%>
<script type="text/javascript">

    var _gaq = _gaq || [];
    _gaq.push(['_setAccount', 'UA-35374139-1']);
    _gaq.push(['_setDomainName', 'kingdomofdreams.in']);
    _gaq.push(['_trackPageview']);

    (function () {
        var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;
        ga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') + '.google-analytics.com/ga.js';
        var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);
    })();

</script>
</head>

<body class="home-page-bg">
    <form id="form1" runat="server">    
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="True">
    </asp:ScriptManager>
    <div class="wrapper">
        <div class="logo-row">
            <div class="logo">
                <a href="http://kingdomofdreams.in/index.html" target="_blank">
                    <img src="../images/logo.jpg" /></a>
            </div>
        </div>
        <!--logo-row ends here -->
        <div class="seats-main">
            <div class="seats-inside1">
                <div class="ticket-main">
                    <%--<asp:ContentPlaceHolder ID="mainContent" runat="server">
                    </asp:ContentPlaceHolder>--%>

                    
                    
                    <table>
                     <tr>
                  <td>
                            <b>Packages</b>
                        </td>
                        <td style="width: 13px">
                            :
                        </td>
                        <td class="style1" align="right">
                    <asp:DropDownList ID="ddl_Package1" runat="server" CssClass="text-small" Width="200px">
                            <asp:ListItem Value="0">Select</asp:ListItem>
                            <asp:ListItem>Couple Rs.1699</asp:ListItem>
                            <asp:ListItem>Single Rs.1099</asp:ListItem>
                                <%--AutoPostBack="True" OnSelectedIndexChanged="ddl_Location_SelectedIndexChanged">--%>
                            </asp:DropDownList>
                            
                            
                        </td>
                        </tr>
                         <tr>
                        <td><b>Quantity</b></td>
                        <td>:</td>
                        <td class="style1" align="right">
                       <asp:DropDownList ID="ddl_Quantity" runat="server" CssClass="text-small" Width="200px">
                            <asp:ListItem Value="0">Select</asp:ListItem>
                            <asp:ListItem>1</asp:ListItem>
                            <asp:ListItem>2</asp:ListItem>
                            <asp:ListItem>3</asp:ListItem>
                            <asp:ListItem>4</asp:ListItem>
                            <asp:ListItem>5</asp:ListItem>
                            <asp:ListItem>6</asp:ListItem>
                            <asp:ListItem>7</asp:ListItem>
                            <asp:ListItem>8</asp:ListItem>
                            <asp:ListItem>9</asp:ListItem>
                            <asp:ListItem>10</asp:ListItem>
                                <%--AutoPostBack="True" OnSelectedIndexChanged="ddl_Location_SelectedIndexChanged">--%>
                            </asp:DropDownList>
                            
                            
                        </td> 
                    </tr>
                      <tr>
                        <td><b>Date</b></td>
                        <td>:</td>
                        <td class="style1" align="right">
                       <asp:DropDownList ID="ddldate" runat="server" CssClass="text-small" Width="200px">
                            <asp:ListItem Value="0">Select</asp:ListItem>
                           <%-- <asp:ListItem>Thu, Oct 10, 2013</asp:ListItem>
                            <asp:ListItem>Fri, Oct 11, 2013</asp:ListItem>
                            <asp:ListItem>Sat, Oct 12, 2013</asp:ListItem>--%>
                                <%--AutoPostBack="True" OnSelectedIndexChanged="ddl_Location_SelectedIndexChanged">--%>
                            </asp:DropDownList>
                            
                            
                        </td> 
                    </tr>

                     <tr>
                        <td>
                            <b>Name</b>
                        </td>
                        <td style="width: 13px">
                            :
                        </td>
                        <td class="style1">
                            <%--<asp:DropDownList ID="ddl_Date" runat="server" Width="160px" CssClass="text-small"
                                OnSelectedIndexChanged="ddl_Date_SelectedIndexChanged" AutoPostBack="true" onchange="javascript:validateBookingShow();">
                                <asp:ListItem Value="0">Select</asp:ListItem>
                            </asp:DropDownList>--%>
                            <asp:TextBox ID="txtName" Width="195px" runat="server" CssClass="text-small"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="show"
                                ControlToValidate="txtName" Display="None" ErrorMessage="Name" InitialValue="0"
                                ForeColor="#ECA035" />
                                 <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1"  FilterType="Custom,UppercaseLetters,LowercaseLetters" ValidChars=" "   TargetControlID="txtName"
                    runat="server">
                </cc1:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <b>Contact No.</b>
                        </td>
                        <td style="width: 13px">
                            :
                        </td>
                        <td class="style1">
                            <%--<asp:DropDownList ID="ddl_Date" runat="server" Width="160px" CssClass="text-small"
                                OnSelectedIndexChanged="ddl_Date_SelectedIndexChanged" AutoPostBack="true" onchange="javascript:validateBookingShow();">
                                <asp:ListItem Value="0">Select</asp:ListItem>
                            </asp:DropDownList>--%>
                            <asp:TextBox ID="txtContact" Width="195px" runat="server" CssClass="text-small"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="show"
                                ControlToValidate="txtContact" Display="None" ErrorMessage="ContactNo." InitialValue="0"
                                ForeColor="#ECA035" />
                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4"  FilterType="Numbers"    TargetControlID="txtContact"
                    runat="server">
                </cc1:FilteredTextBoxExtender>
                        </td>
                    </tr>
                      <tr>
                        <td>
                            <b>Email Address.</b>
                        </td>
                        <td style="width: 13px">
                            :
                        </td>
                        <td class="style1">
                            <%--<asp:DropDownList ID="ddl_Date" runat="server" Width="160px" CssClass="text-small"
                                OnSelectedIndexChanged="ddl_Date_SelectedIndexChanged" AutoPostBack="true" onchange="javascript:validateBookingShow();">
                                <asp:ListItem Value="0">Select</asp:ListItem>
                            </asp:DropDownList>--%>
                            <asp:TextBox ID="txtEmail" Width="195px" runat="server" CssClass="text-small"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="show"
                                ControlToValidate="txtEmail" Display="None" ErrorMessage="Email" InitialValue="0"
                                ForeColor="#ECA035" />
                        </td>
                    </tr>
                    </table>
                    
                    <asp:CheckBox runat="server" ID="chkterms" />
        <asp:CustomValidator ID="chkvalidato" Display="None" runat="server" EnableClientScript="true"
        ErrorMessage="accept terms and conditions" ClientValidationFunction="terms_Conditions"
        ValidationGroup="show"></asp:CustomValidator>
        I Agree to the Terms and Conditions, to read &nbsp;&nbsp;&nbsp;&nbsp;
    <asp:LinkButton ID="Button1" CssClass="clickhere" runat="server" Text="Click Here" /><br /><br />
  <%--<div><asp:Label ID="Label2" runat="server" Text="For Package Details" ForeColor="Red"></asp:Label>&nbsp; 
  <asp:LinkButton ID="Button2" CssClass="clickhere" runat="server" Text="Click Here"/></div>--%>
    <br />
    <div style="display: none">
        <asp:LinkButton CssClass="clickhere" ID="lnbShowCast" runat="server" Text="Click here"></asp:LinkButton>&nbsp;for
        the Show Cast
        <br />
        <br />
        </div>
      
         
            <cc1:ModalPopupExtender ID="mo12" PopupControlID="dv_pop" BackgroundCssClass="modalBackground2"
        CancelControlID="btnClose" TargetControlID="Button1" runat="server">
    </cc1:ModalPopupExtender>
                 <div id="dv_pop" class="ModalWindow" style="display: none; width: 550px; height: 500px;"
        runat="server">
        <div style="overflow: auto; width: 530px; height: 460px; padding: 0px 10px 0px 10px">
            
           <%-- <b><font color="red">
                <p>
                    * Children of age two years and below, will not be allowed inside Nautanki Mahal Theatre for shows due to High decibel level.</p>
            </font></b>
            <h3>
                CANCELLATION/REFUND POLICY</h3>
            <b><font color="red">
                <p>
                    * As per policy we do not cancel/refund/change any tickets once booked.</p>
            </font></b>
            <p>
                * Performances and Performers of the theater programme are subject to change without
                prior notice.
            </p>
            <p>
                Kingdom of Dreams has the right to make alterations to the advertised theatre programme
                or cast & crew in the event of an illness or unavoidable cause, without prior notice.
            </p>--%>
            <h3>
                <center><u> Terms and Conditions</u></center></h3>
            <ol>
                <li>Ticket once issued can't be reissued if misplaced/ stolen etc.</li>
                <li>All the rates are inclusive of applicable Government tax as on date.</li>
                <li>This ticket would only be valid for this event and cannot be clubbed/exchanged/transferred  with any other event or show.</li>
                <li>Ticket is non-refundable. In case, the event  gets cancelled, Management of the Kingdom of Dream will revalidate the ticket for fresh date or  refund the net ticket price & taxes. Any cancellation will be duly notified through proper channels i.e. by Telephone/ Email/Media/ Internet etc. The service charge or any other charges paid for and included in the ticket sale, such as convenience charge or delivery charge paid for booking of the ticket will not be refunded. The event is subject to force majeure conditions. The management reserves a right to cancel the show or schedule it on an alternate date.</li>
                <li>This ticket  is strictly non-transferable and non refundable. </li>
                <li>Please check date, time and  schedule of the performances for the day. </li>
                <li>Schedule & Timing for the event  may change without prior notice .</li>  
                 <li>Please carry valid ID proof ( Specially Date Of Birth ) along with the ticket  for the smooth entry to the event . The  alcohol will not be served to the guests under the age of 25 years.</li>         
                 <li>Kingdom of Dreams  is not responsible for the loss or theft of any personal belongings or any injury to the ticket holder at the event. It is the responsibility of the customer to take care of his/ her belongings.</li>
                 <li>Outside Food or beverage is not permitted to be carried/consumed  inside Kingdom of Dreams.</li>
                 <li>Parking facilities are available at owner’s own risk.</li>
                 <li>All trademarks, brand names and intellectual property displayed on the ticket, at the venue and the Kingdom  of Dreams  belong to the Great Indian Nautanki Company Private Limited or to their rightful owners.</li>
                 <li>All standard Kingdom  of Dreams  terms and conditions are applicable.</li>
                 <li>All disputes shall be subject to the jurisdiction of Gurgaon Court. Haryana.</li>
                 <li>The decision of the Management of Kingdom of Dreams shall be final & binding in case of any disputes.</li>
                 <li>This ticket is subject to the strict observance of the rules and regulations of the management of the Kingdom of Dreams and of the event organizer.</li>
                 <li>Rights of admission is reserved by Kingdom of Dreams  . Rules of the venue, auditorium and Kingdom of Dreams will apply.</li>
            </ol>
<%--
            <h3>
                HUDA RESIDENTS SCHEME</h3>
            <p>
                Huda residents are entitled to get 25% discount on show tickets of Zangoora (subject
                to booking done 7 days prior to the show only from the box office counter). They
                also have to bring the Allotment Letter from HUDA and it has to be in the name of
                the person who has come to buy the tickets at the counter along with his ID Proof.
                Only 4pax allowed against 1 allotment letter and for all 4 members id proof’s are
                required. No internet booking is entitled for discounts. Ticket holders must carry
                their proof of residence, photo ID proof for verification at time of entry. Documents:
                Electric Bill / Telephone Bill / Ration Card / PAN Card / Passport / Driving License
                / Photo ID / Voter ID card and for Kids above the required age in the policy, school
                ID card will be required.
            </p>
            <h3>
                CANCELLATION AND POSTPONE POLICY
            </h3>
            <p>
                Occasionally, performances are cancelled or postponed. Should this occur, we will
                attempt to contact you to inform you of refund or exchange procedures for the scheduled
                performances / event. For exact instructions on any cancelled or postponed performance,
                please contact us at 0124 – 4528000</p>
            <h3>
                PAYMENT METHODS</h3>
            <p>
                Kingdom of Dream accepts several methods of payment to accommodate your needs. Kingdomofdreams.co.in
                accepts all the major credit cards to facilitate the same. Tickets are also available
                at the box office or can be availed at call centre at: 0124 - 4528000 Located at
                Kingdom of Dreams.</p>
            <h3>
                CURRENCY</h3>
            <p>
                All ticket prices for events that occur in India are stated in Rupees. All the payments
                are accepted at the box office and online in Rupees.</p>
            <h3>
                PRIVACY POLICY</h3>
            <p>
                The Great Indian Nautanki company Private Limited recognises the importance of protecting
                your privacy. We are committed to ensuring the continued integrity and security
                of the personal information you entrust to us.</p>
            <p>
                We appreciate that the success of our business is largely dependent upon a relationship
                of trust being established and maintained with past, current and prospective customers
                with whom we conduct business. We will therefore continue to collect and manage
                your personal information with a high degree of diligence and care.</p>
            <h3>
                STORAGE AND SECURITY OF YOUR PERSONAL INFORMATION</h3>
            <p>
                We will take reasonable steps to keep the personal information that we hold about
                you secure to ensure that it is protected from loss, unauthorised access, use, modification
                or disclosure.</p>
            <p>
                Your personal information is stored within secure systems that are protected in
                controlled facilities. Our employees and authorised agents are obliged to respect
                the confidentiality of any personal information held by us.</p>
            <h3>
                OUR WEBSITE</h3>
            <p>
                We use our best efforts to ensure that information received via our websites remains
                secured within our systems. We are regularly reviewing developments in online security,
                however users should be aware that there are inherent risks in transmitting information
                across the internet. Information transmitted via our websites is protected by a
                encryption technology.</p>
            <p>
                The information is used in an aggregate form and generally no personal information
                is collected by the third party service provider.
            </p>
            <h3>
                NOTE</h3>
            <p>
                We as a merchant shall be under no liability whatsoever in respect of any loss or
                damage arising directly or indirectly out of the decline of authorization for any
                Transaction, on Account of the Cardholder having exceeded the preset limit mutually
                agreed by us with our acquiring bank from time to time
            </p>--%>
        </div>
        <hr />
        <asp:Button Text="Close" runat="server" CssClass="common-button" ID="btnClose" />
    </div>
       <asp:Label ID="Label1" runat="server" Text="*Only Couples and Families allowed" ForeColor="Red" Visible="false"></asp:Label>
                    
            
            <div style="float: left">
        <br /><asp:Button ID="btn_Submit" CssClass="common-button" ValidationGroup="show" runat="server"
            Text="Proceed"  OnClientClick="javascript:return validInsert();" 
                    onclick="btn_Submit_Click"/></div><br /><br /><br /><br />
            
                    <center><asp:Label ID="lblMess" runat="server" Text=""></asp:Label></center>
                </div>
            </div>
        </div>
        <!-- seats-main ends here -->
    </div>
    <uc1:Footer ID="Footer1" runat="server" />
    
    
    <!-- wrapper ends here -->
    <%--<uc1:Footer ID="Footer1" runat="server" />--%>
    <!-- footer-main ends here -->
    </form>
</body>
<script language="javascript" type="text/javascript">
             function validInsert()
             {
             if (<% Response.Write(ddl_Package1.ClientID);%>.value=="0")
                {
                alert("Please Select atleast One Package");
             <%Response.Write(ddl_Package1.ClientID);%>.focus();
                return false;
                }
                 if (<% Response.Write(ddl_Quantity.ClientID);%>.value=="0")
                {
                alert("Please Select the Quantity");
             <%Response.Write(ddl_Quantity.ClientID);%>.focus();
                return false;
                }
                 if (<% Response.Write(ddldate.ClientID);%>.value=="0")
                {
                alert("Please Select the Date");
             <%Response.Write(ddldate.ClientID);%>.focus();
                return false;
                }
//                  if (<% Response.Write(ddl_Package1.ClientID);%>.value=="Couple Rs.1699")
//                {
//                      if (<% Response.Write(ddl_Quantity.ClientID);%>.value=="1"||<% Response.Write(ddl_Quantity.ClientID);%>.value=="3"||<% Response.Write(ddl_Quantity.ClientID);%>.value=="5"||<% Response.Write(ddl_Quantity.ClientID);%>.value=="7"||<% Response.Write(ddl_Quantity.ClientID);%>.value=="9")
//                    {
//                    alert("Please Select the Quantity as the muliple of two");
//                 <%Response.Write(ddl_Quantity.ClientID);%>.focus();
//                    return false;
//                    }
//                }
                if (<% Response.Write(txtName.ClientID);%>.value=="")
                {
                alert("Please Enter Your Name");
             <%Response.Write(txtName.ClientID);%>.focus();
                return false;
                }
                 if (<% Response.Write(txtContact.ClientID);%>.value=="")
                {
                alert("Please Enter Your Contact No.");
             <%Response.Write(txtContact.ClientID);%>.focus();
                return false;
                }
                if((<%Response.Write(txtContact.ClientID);%>.value).length>10)
                {
                alert("Contact Number Should not be greater than 10 digits");
                <%Response.Write(txtContact.ClientID);%>.focus();
                return false;
                }
                if((<%Response.Write(txtContact.ClientID);%>.value).length<10)
                {
                alert("Contact Number Should be atleast of 10 digits");
                <%Response.Write(txtContact.ClientID);%>.focus();
                return false;
                }
                 if (<%Response.Write(txtEmail.ClientID);%>.value == "") 
                    {
                        alert("Please Enter Your Email Id");
                       <%Response.Write(txtEmail.ClientID);%>.focus();
                        return false;
                    }
                    //var emailPat = /^([a-zA-Z0-9_.+-])+@(([a-zA-Z0-9-])+.)+([a-zA-Z0-9]{2,6})+$/;
                      var emailPat = /^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*/;
                    
                    var emailid = <%Response.Write(txtEmail.ClientID);%>.value;
                    var matchArray = emailid.match(emailPat);
                    if (matchArray == null)
                    {
                        alert("Please Enter Valid Email ID");
                        <%Response.Write(txtEmail.ClientID);%>.focus();
                        return false;
                    }   
                   if (chkterms.checked == false)
                {
                    alert("Please Check the Terms And Conditions");
                     return false;
                }
              }
              function ChangeImage() {
                    window.open('../images/ValentineCreative.jpg', '_blank', 'tools=0,status=0,history=0,width=500,height=500,top=50,left=50');
                    }
               </script>
</html>
