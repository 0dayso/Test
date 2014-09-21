<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BollyLand.aspx.cs" Inherits="BollyLand_BollyLand" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../Controls/Footer.ascx" TagName="Footer" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
        .ModalWindow3
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
         .modalBackground3
        {
            background-color: Gray;
            filter: alpha(opacity=70);
            opacity: 0.7;
        }
        .seats-inside1
{
    width: 846px;
    float: left;
    background: url(../images/bollyland_booking.jpg) no-repeat;
    overflow: auto;
    height: 489px;
    padding-left:245px;
    *padding-left:245px;
}
    .style1
    {
        width: 326px;
    }
    .style2
    {
        width: 589px;
    }
    .style3
    {
        width: 165px;
    }
    .style4
    {
        width: 1825px;
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
                  <td>
                            <b>Category</b>
                        </td>
                        <td style="width: 13px">
                            :
                        </td>
                        <td class="style4"><asp:Label ID="Gold" runat="server" Text="Gold Rs."></asp:Label></td>
                        <td class="style2"><asp:Label ID="pricegold" runat="server" Text="5499" ></asp:Label></td>
                        <td class="style1" align="right">
                        <b><asp:DropDownList ID="ddl_GoldPackage" runat="server" CssClass="text-small" Width="65px">
                            <asp:ListItem Value="0"> 0</asp:ListItem>
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
                        <td></td>
                        <td></td>
                        <td class="style4"><asp:Label ID="Silver" runat="server" Text="Silver Rs."></asp:Label></td>
                        <td class="style2"><asp:Label ID="pricesilver" runat="server" Text="1999"></asp:Label></td>
                        <td class="style1" align="right">
                        <asp:DropDownList ID="ddl_SilverPackage" runat="server" CssClass="text-small" Width="65px">
                            <asp:ListItem Value="0"> 0</asp:ListItem>
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
                    
                  
                     </table>
                           <table>    
                        <tr>
                        <td>
                            <b>Name</b>
                        </td>
                        <td style="width: 13px">
                            :
                        </td>
                        
                        <td></td>
                        
                        <td class="style3">
                            <%--AutoPostBack="True" OnSelectedIndexChanged="ddl_Location_SelectedIndexChanged">--%>
                            <asp:TextBox ID="txtName" Width="200px" runat="server" CssClass="text-small"></asp:TextBox>
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
                        
                        <td></td>
                        
                        <td class="style3">
                            <%--AutoPostBack="True" OnSelectedIndexChanged="ddl_Location_SelectedIndexChanged">--%>
                            <asp:TextBox ID="txtContact" Width="200px" runat="server" CssClass="text-small"></asp:TextBox>
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
                        
                        <td></td>
                        
                        <td class="style3">
                            <%--AutoPostBack="True" OnSelectedIndexChanged="ddl_Location_SelectedIndexChanged">--%>
                            <asp:TextBox ID="txtEmail" Width="200px" runat="server" CssClass="text-small"></asp:TextBox>
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
    <asp:LinkButton ID="Button1" CssClass="clickhere" runat="server" Text="Click Here" /><br />
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
            
           <%--AutoPostBack="True" OnSelectedIndexChanged="ddl_Location_SelectedIndexChanged">--%>
            <h3>
                <center><u> Terms and Conditions</u></center></h3>
            <ol>
                <li>Great Indian Nautanki Company Private Limited  is a company engaged in the business of providing entertainment services and promote performing arts through its, well known, entertainment and leisure destination house called as the Kingdom of Dreams(“ hereinafter referred as “KOD” ). KOD comprises of the Nautanki Mahal, Culture Gully, Showshaa Theatre and the IIFA BUZZ Lounge, through which KOD furthers the objective of showcasing live entertainment, handicrafts, Indian culture, etc. </li>
                <li>“Bollyland Music Festival” (hereinafter referred as “Event”) is organized by Viacom 18 Media Pvt. Ltd. ("Organizers") and is open to all persons who are aged 15 years and above. Entry to the Event implies acceptance of all the terms and conditions of the Event. In case of entry to the Event by any persons under the age of 18 years (minors), such entry to the Event by any such entrant shall be with the prior consent of the parent/guardians and will be deemed as entry with the prior consent and knowledge of the entrant's parents.</li>
                <li>The Event is scheduled to be conducted on 20th December 2013 from 7:30 PM at Kingdom Of Dreams, Gurgaon.</li>
                <li>In order to purchase the entry passes for the Event, an entrant may visit the Kingdom of Dreams website or the Box Office at Kingdom of Dreams to pre-book an entry to the Event. Upon receipt of payment, the entrant will be sent an acknowledgement receipt by way of email.</li>
                <li>Appropriate wrist-bands will be issued at the event which will enable access to the designated areas – Gold and Silver category.</li>
                <li>In order to avail the entry bands, the entrants must collect the same from the Box Office at Kingdom of Dreams, on the date of the Event upon presentation of Acknowledgement Receipt appropriate and credible ID proof and age proof ("Entry Bands") and sign a release form as may be required by the Organizers or GINC.</li>
                <li><b>ENTRANTS MUST CARRY VALID PHOTO ID PROOF (ESPECIALLY DATE OF BIRTH PROOF) ALONG WITH THE TICKET FOR THE SMOOTH ENTRY TO THE EVENT</b></li>
                <li>In the event the entrants do not collect the Entry Bands on the date of the Event, the Organizers may forfeit the Entry Bands. It is the entrant's responsibility to ensure that their personal details (name, address, date of birth, email id, ID Proof) are always updated and accurate in order to be able to receive the Acknowledgement Receipt and/or Entry Bands in time. Failure of the entrants to carry the Acknowledgement Receipt and/or their identity/age proof shall disentitle them to get any Entry Bands. Subject to availability, the Entry Bands may also be made available on the date of the Event against payment at the Box Office. </li>
                <li>The Artists/Schedule & Timing for the show may change without prior notice. No refunds are allowed for change in the advertised artists.</li>
                <li>The Entry Bands shall not be transferable in favor of any other person except for the entrant in whose name the Entry Bands has been issued. The Entry Bands shall not be redeemable for cash. The Organizers/GINC in this behalf shall entertain no request. Entry to the Event is permitted only through the authorized Entry Bands issued and authorized by the Organizers/ GINC.</li>
                <li>Parking facilities are available at vehicle owner’s own risk.</li>   
                <li>The Entry Bands shall entitle only the entry to the Event. All other incidental costs including travel and boarding, tips, meals, telephone, beverages charges {alcoholic or non-alcoholic} merchandise purchases, if any, that may arise from attending the Event will be borne by the entrant themselves.</li>         
                <li>The GINC shall not be responsible in case of any problem such as breakdown of machinery, disruption in the Event, inconvenience/ hindrance caused to the Event.</li>
                <li>GINC takes no responsibility or liability for the entrants arriving on time to the designated venues for the Event. GINC shall not be responsible or liable to the entrant in the event the entrants are denied entry to the venues if the venues are fully occupied. Each entrant has to abide by the security measures and security checks as may be applicable at the venue at the Event. Any person stepping outside the designated area of their bands shall be at their sole risk and consequences. The entrants may not be allowed to re-enter the designated area of the Event without an Entry Bands at any given point of time. The entrants should not remove, detach, amputate, tamper or fiddle around with the Entry Bands in any manner. If it is found that the Entry Bands have been removed, detached, tampered or fiddled, such entrants shall not be allowed to enter/re-enter the Event or asked to exit the Event.</li>
                <li>Importantly, persons having age of 25 years and below shall not be allowed to enter the "bar" area at the Event and/or purchase any alcohol products available at the bar/the Event. In case any person having age of 25 years or below is caught drinking alcohol or in breach of this clause, such entrant(s) shall be required to forthwith exit from the Event. Further, persons having age of 25 years but below 25 years shall only be served "beer" but no hard liquor shall be served to such person at the Event. Smoking shall be allowed only in the smoking lounges that may be available at certain venues of the Event. Drugs are strictly prohibited at the Event and persons who are found in possession or consuming Drugs at the Event shall not be allowed entry or asked to exit from the Event.</li>
                <li>All standard Kingdom of Dreams terms and conditions are applicable.</li>
                <li>Cameras, any form of recording instruments, arms and ammunition, eatables, bottled water, beverages, alcohol are not allowed from outside the event. </li>
                <li>Outside Food or Beverage are not permitted to be carried inside the premises of Kingdom of Dreams.</li>
                <li>GINC or any of its agent, officers, and employees shall not be responsible for any injury, damage, theft, losses or cost suffered at or as a result of the event or any part of it.</li>
                <li>All the rates are inclusive of applicable Government taxes as on date (unless specified).</li>
                <li>GINC shall not be responsible for the quality or deficiency etc. of the Event and no claim or request, whatsoever, in this respect shall be entertained by GINC.</li>
                <li>The Organizers/GINC  reserve the right to change/modify (i) terms and conditions; (ii) date, venues or timings of the Event or (iii) the artists that are like to perform at the Event, at any time at their own discretion and without any prior notice and without assigning any reason. The Organizers/GINC have the right to suspend, postpone, cease, close or terminate this Event at any given point of time at its option and discretion. </li>
                <li>GINC shall not entertain any questions, correspondence, enquiries on the manner in which the Event is conducted or otherwise from any person whatsoever.</li>
                <li>Apart from the entry to the Event, the entrant or his/her legal heirs will have no other rights or claims against the Organizers/GINC.</li>
                <li>All trademarks, brand names and intellectual property displayed on the ticket, at the venue and the Kingdom of Dreams belong to the Great Indian Nautanki Company Private Limited or to their rightful owners.</li>
                <li>In case of any dispute or difference in respect of this Event, the decision of the Organizers shall be final and binding on all concerned.</li>
                <li>The Event and these Terms & Conditions will be governed, construed and interpreted under the laws of India. Entrants agree to be bound by these Terms & Conditions and by the decisions of the GINC, which are final and binding in all respects. GINC reserves the right to change these Terms & Conditions at any time, in its sole discretion, and to suspend or cancel the Event or any entrant's participation in the Event should viruses, bugs, unauthorized human intervention or other causes beyond the GINC control affect the administration, security or proper play of the Event or the GINC otherwise becomes (as determined in its sole discretion) incapable of running the Event as planned. Entrants who violate these Terms & Conditions, tamper with the operation of the Event or engage in any conduct that is detrimental or unfair to GINC, the Event or any other entrant (in each case as determined in GINC sole discretion) are subject to disqualification from entry into the Event. </li>
                <li>All disputes shall be subject to the jurisdiction of Courts in Gurgaon, Haryana. </li>
                <li>The decision of the Management of Kingdom of Dreams shall be final & binding.</li>
                <li>Kingdom of Dreams reserve the Right of Admission. </li></ol><br />
                
            
<%--<asp:DropDownList ID="ddl_Date" runat="server" Width="160px" CssClass="text-small"
                                OnSelectedIndexChanged="ddl_Date_SelectedIndexChanged" AutoPostBack="true" onchange="javascript:validateBookingShow();">
                                <asp:ListItem Value="0">Select</asp:ListItem>
                            </asp:DropDownList>--%>
        </div>
        <hr />
        <asp:Button Text="Close" runat="server" CssClass="common-button" ID="btnClose" />
    </div>
                    <%--<asp:DropDownList ID="ddl_Date" runat="server" Width="160px" CssClass="text-small"
                                OnSelectedIndexChanged="ddl_Date_SelectedIndexChanged" AutoPostBack="true" onchange="javascript:validateBookingShow();">
                                <asp:ListItem Value="0">Select</asp:ListItem>
                            </asp:DropDownList>--%>
                    
            
            <div style="float: left">
        <br /><asp:Button ID="btn_Submit" CssClass="common-button" ValidationGroup="show" runat="server"
            Text="Proceed"  OnClientClick="javascript:return validInsert();" 
            onclick="btn_Submit_Click"  /></div><br /><br /><br /><br />
            
                    <center><asp:Label ID="lblMess" runat="server" Text=""></asp:Label></center>
                </div>
            </div>
        </div>
        <!-- seats-main ends here -->
    </div>
    <uc1:Footer ID="Footer1" runat="server" />
    
    
    <!-- wrapper ends here -->
    <%--<asp:DropDownList ID="ddl_Date" runat="server" Width="160px" CssClass="text-small"
                                OnSelectedIndexChanged="ddl_Date_SelectedIndexChanged" AutoPostBack="true" onchange="javascript:validateBookingShow();">
                                <asp:ListItem Value="0">Select</asp:ListItem>
                            </asp:DropDownList>--%>
    <!-- footer-main ends here -->
    </form>
</body>
<script language="javascript" type="text/javascript">
    function validInsert() {
        var gpackag = document.getElementById("<%=ddl_GoldPackage.ClientID %>");
        var spackag = document.getElementById("<%=ddl_SilverPackage.ClientID %>");
        if (gpackag.value == "0" && spackag.value == "0") {
            alert("Please Select atleast One Category ");
            gpackag.focus();
            return false;
        }
        var name = document.getElementById("<%=txtName.ClientID %>");
        if (name.value == "") {
            alert("Please Enter Your Name");
            name.focus();
            return false;
        }
        var cont = document.getElementById("<%=txtContact.ClientID %>");
        if (cont.value == "") {
            alert("Please Enter Your Contact No.");
            cont.focus();
            return false;
        }
        if ((cont.value).length > 10) {
            alert("Contact Number Should not be greater than 10 digits");
            cont.focus();
            return false;
        }
        if ((cont.value).length < 10) {
            alert("Contact Number Should be atleast of 10 digits");
            cont.focus();
            return false;
        }
        var mail = document.getElementById("<%=txtEmail.ClientID %>");
        if (mail.value == "") {
            alert("Please Enter Your Email Id");
            mail.focus();
            return false;
        }
        //var emailPat = /^([a-zA-Z0-9_.+-])+@(([a-zA-Z0-9-])+.)+([a-zA-Z0-9]{2,6})+$/;
        var emailPat = /^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*/;

        var emailid = mail.value;
        var matchArray = emailid.match(emailPat);
        if (matchArray == null) {
            alert("Please Enter Valid Email ID");
            mail.focus();
            return false;
        }
        var chk = document.getElementById("<%=chkterms.ClientID %>");
        if (chk.checked == false) {
            alert("Please Check the Terms And Conditions");
            return false;
        }
    }
               </script>

</html>
