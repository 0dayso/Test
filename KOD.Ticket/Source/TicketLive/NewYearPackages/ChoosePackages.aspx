<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ChoosePackages.aspx.cs" Inherits="NewYearPackages_ChoosePackages" MasterPageFile="~/Event.master"  %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../Controls/Footer.ascx" TagName="Footer" TagPrefix="uc1" %>
<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">--%>
<asp:Content ID="head" ContentPlaceHolderID="head" runat="Server">
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
    background: url(../images/ticket-booking-banner-new.jpg) no-repeat;
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

    <%--<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Kingdom of Dreams : Ticket Booking</title>
    <link href="../css/style.css" type="text/css" rel="stylesheet" />
    <link rel="shortcut icon" type="image/x-icon" href="../images/favicon.ico" />--%>
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
</asp:Content>
<%--<body class="home-page-bg">
    <form id="form1" runat="server">  --%>  
    <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
   <%-- <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="True">
    </asp:ScriptManager>--%>
    <div class="wrapper">
        <%--<div class="logo-row">
            <div class="logo">
                <a href="http://kingdomofdreams.in/index.html" target="_blank">
                    <img src="../images/logo.jpg" /></a>
            </div>
        </div>--%>
        <!--logo-row ends here -->
        <div class="seats-main">
            <div class="seats-inside1">
                <div class="ticket-main">
                    <%--<asp:ContentPlaceHolder ID="mainContent" runat="server">
                    </asp:ContentPlaceHolder>--%>

    <cc1:ModalPopupExtender ID="ModalPopupExtender1" PopupControlID="Div1" BackgroundCssClass="modalBackground3"
        CancelControlID="Button2" TargetControlID="chkRoyal" runat="server">
    </cc1:ModalPopupExtender>
    <div id="Div1" class="ModalWindow3" style="display: none; width: 350px; height: 120px;"
        runat="server">
        <div style="overflow: auto; width: 310px; height: 80px; padding: 0px 10px 0px 10px">
        <table>
        <tr>
        <td>
        Please Enter Your Royal Card No. OR Mobile No.
        </td>
        </tr>
        <tr>
        <td></td>
        </tr>
        <tr>
        <td></td>
        </tr>
        <tr>
        <td></td>
        </tr>
        <tr>
        <td>
            <asp:TextBox ID="textroyalinfo" runat="server"></asp:TextBox>
            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2"  FilterType="Custom,UppercaseLetters,LowercaseLetters,Numbers" ValidChars="-"   TargetControlID="textroyalinfo"
                     runat="server">
                </cc1:FilteredTextBoxExtender>
        </td>
        </tr>
        </table>
         </div>
        <hr />
        <asp:Button Text="Close" CssClass="common-button" style="text-transform:none"  runat="server"  ID="Button2" />
        <asp:Button Text="Select Package" CssClass="common-button" runat="server" 
            style="float:right; text-transform:none"  ID="btnvalidation" 
            onclick="btnvalidation_Click"  />
    </div>
                    
                    <table>
                    <tr>
                    <td colspan="5"><asp:LinkButton ID="chkRoyal" ForeColor="#ECA035" runat="server">Are You a Royal Card Holder</asp:LinkButton></td>
                    </tr>
                  <td>
                            <b>Packages</b>
                        </td>
                        <td style="width: 13px">
                            :
                        </td>
                        <td class="style4"><asp:Label ID="couple" runat="server" Text="Couple* Rs."></asp:Label></td>
                        <td class="style2"><asp:Label ID="pricecouple" runat="server" Text="11999" ></asp:Label></td>
                        <td class="style1" align="right">
                        <b><asp:DropDownList ID="ddl_CouplePackage" runat="server" CssClass="text-small" Width="65px">
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
                        <td class="style4"><asp:Label ID="single" runat="server" Text="Single Rs."></asp:Label></td>
                        <td class="style2"><asp:Label ID="pricesingle" runat="server" Text="6999"></asp:Label></td>
                        <td class="style1" align="right">
                        <asp:DropDownList ID="ddl_SinglePackage" runat="server" CssClass="text-small" Width="65px">
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
                        <td class="style4"><asp:Label ID="teen" runat="server" Text="Teens# Rs."></asp:Label></td>
                        <td class="style2"><asp:Label ID="priceteen" runat="server" Text="3999"></asp:Label></td>
                        <td class="style1" align="right">
                          <asp:DropDownList ID="ddl_TeensPackage" runat="server" CssClass="text-small" Width="65px">
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
                        <td class="style4"><asp:Label ID="kids" runat="server" Text="Kids** Rs."></asp:Label></td>
                         <td class="style2"><asp:Label ID="pricekids" runat="server" Text="2999"></asp:Label></td>
                        <td class="style1" align="right">
                        <asp:DropDownList ID="ddl_KidsPackage" runat="server" CssClass="text-small" Width="65px">
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
                <li>Great Indian Nautanki Company Private Limited  is a company engaged in the business of providing entertainment services and promote performing arts through its, well known, entertainment and leisure destination house called as the Kingdom of Dreams(“ hereinafter referred as “KOD” ). KOD comprises of the Nautanki Mahal, Culture Gully, Showshaa Theatre and the IIFA BUZZ Lounge, through which KOD furthers the objective of showcasing live entertainment, handicrafts, Indian culture, etc.</li>
                <li><b>GUEST MUST CARRY VALID PHOTO ID PROOF (ESPECIALLY DATE OF BIRTH PROOF) ALONG WITH THE TICKET FOR THE SMOOTH ENTRY TO THE EVENT.</b> </li>
                <li>Kids Ticket/(s), Teenagers Ticket/(s) and Single Ticket /(s) can be sold only with Couple Ticket /(s),</li>
                <li>Kindly re- check time and schedule of the performances/shows of the ticket. Ticket is strictly non-refundable/non-transferable and is valid for One Person only, unless specified. Ticket once issued will not be reissued,  even if misplaced/ stolen etc.</li>
                <li>Tickets once booked cannot be cancelled or refunded. If KOD cancels the show in advance, they will attempt to contact the guest and offer a free exchange or refund of your tickets. For this reason, make sure when you book tickets, you  have  given accurate contact details. The service charge or any other charges paid for and included in the ticket sale, such as convenience charge or delivery charge paid for booking of the ticket will not be refunded. This ticket is the sole property of KOD. The ticket should be bought from an authorized point of sale only.</li>
                <li>This ticket would only be valid for the Grandest New Year Celebration and cannot be clubbed/ exchanged/ transferred with any other event or service. </li>
                <li>The Ticket for the shows at Nautanki Mahal, if eligible, will be issued separately and accordingly the Terms and Conditions of Nautanki Mahal will be applicable. Kindly read the same carefully. </li>
                <li>Management reserves the right to cancel the show/event or schedule it on an alternate date without assigning any reasons. </li>
                <li>The Artists/Schedule & Timing for the event may change without prior notice. No refunds are allowed for change in the advertised artists.</li>
                <li>The event/show is subject to force majeure conditions. </li>
                <li>The alcohol will not be served to the guests under the age of 25 years. </li>   
                <li>We are not responsible for the loss or theft of any personal belongings or any injury to the ticket holder in the premises/shows. It is the responsibility of the customer to take care of his/ her belongings. </li>         
                <li>All the rates are inclusive of applicable Government taxes as on date (unless specified). </li>
                <li>Outside Food or Beverage are not permitted to be carried inside the premises of Kingdom of Dreams.</li>
                <li>Kingdom of Dreams reserve the right to perform security check on guests at the entry points for security reasons.</li>
                <li>Kingdom of Dreams has a “no re-entry” policy unless allowed through security.</li>
                <li>Parking facilities are available at vehicle owner’s own risk.</li>
                <li>All trademarks, brand names and intellectual property displayed on the ticket, at the venue and the Kingdom of Dreams belong to the Great Indian Nautanki Company Private Limited or to their rightful owners.</li>
                <li>All standard Kingdom of Dreams terms and conditions are applicable.</li>
                <li>All disputes shall be subject to the jurisdiction of Courts in Gurgaon, Haryana. </li>
                <li>The decision of the Management of Kingdom of Dreams shall be final & binding.</li>
                <li>Kingdom of Dreams reserve the Right of Admission. </li></ol><br />
                 <ul>*Only Couples and Families allowed.</ul>
                 <ul>** Children between the age of 5 to 12 years will be classified  as Kids.    </ul>
                 <ul># Children between the age of 13 to 19 years will be classified as Teens. </ul>
            
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
                    onclick="btn_Submit_Click"/></div><br /><br /><br /><br />
            
                    <center><asp:Label ID="lblMess" runat="server" Text=""></asp:Label></center>
                </div>
            </div>
        </div>
        <!-- seats-main ends here -->
    </div>
   <%-- <uc1:Footer ID="Footer1" runat="server" />--%>
    
    
    <!-- wrapper ends here -->
    <%--<asp:DropDownList ID="ddl_Date" runat="server" Width="160px" CssClass="text-small"
                                OnSelectedIndexChanged="ddl_Date_SelectedIndexChanged" AutoPostBack="true" onchange="javascript:validateBookingShow();">
                                <asp:ListItem Value="0">Select</asp:ListItem>
                            </asp:DropDownList>--%>
    <!-- footer-main ends here -->



<%--
    </form>
</body>--%>
<script language="javascript" type="text/javascript">
             function validInsert()
             {
                var packag = document.getElementById("<%=ddl_CouplePackage.ClientID %>");
                var singlpackag = document.getElementById("<%=ddl_SinglePackage.ClientID %>");
                if (packag.value=="0")
                {
                alert("Please Select atleast One Couple Package");
                packag.focus();
                return false;
            }
            if (singlpackag.value > 2 * packag.value) {
                alert("Single Package can not be greater than twice of Selected Couple Package");
                singlpackag.value = "0";
                singlpackag.focus();
                return false;
                }
                var name = document.getElementById("<%=txtName.ClientID %>");
                if (name.value=="")
                {
                alert("Please Enter Your Name");
                name.focus();
                return false;
                }
                var cont = document.getElementById("<%=txtContact.ClientID %>");
                 if (cont.value=="")
                {
                alert("Please Enter Your Contact No.");
                cont.focus();
                return false;
                }
                if((cont.value).length>10)
                {
                alert("Contact Number Should not be greater than 10 digits");
                cont.focus();
                return false;
                }
                if((cont.value).length<10)
                {
                alert("Contact Number Should be atleast of 10 digits");
                cont.focus();
                return false;
                }
                var mail = document.getElementById("<%=txtEmail.ClientID %>");
                 if (mail.value == "") 
                    {
                        alert("Please Enter Your Email Id");
                       mail.focus();
                        return false;
                    }
                    //var emailPat = /^([a-zA-Z0-9_.+-])+@(([a-zA-Z0-9-])+.)+([a-zA-Z0-9]{2,6})+$/;
                      var emailPat = /^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*/;
                    
                    var emailid = mail.value;
                    var matchArray = emailid.match(emailPat);
                    if (matchArray == null)
                    {
                        alert("Please Enter Valid Email ID");
                        mail.focus();
                        return false;
                    }
                    var chk = document.getElementById("<%=chkterms.ClientID %>");
                   if (chk.checked == false)
                   {
                    alert("Please Check the Terms And Conditions");
                     return false;
                   }
              }
               </script>

</asp:Content>
