<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="RoyalWebApp.Account.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<script language="javascript" type="text/javascript">
    function WaterMark(txtMemberId, event) {
        var defaultText = "RCM-XXXXXXX";
        // Condition to check textbox length and event type
        if (txtMemberId.value.length == 0 & event.type == "blur") {
           //if condition true then setting text color and default text in textbox
          // alert("Please Enter Your ID");
            txtMemberId.style.color = "Black";
            txtMemberId.value = defaultText;
        }
        // Condition to check textbox value and event type
        if (txtMemberId.value == defaultText & event.type == "focus") {
            txtMemberId.style.color = "black";
            txtMemberId.value = "";
        }
    }
</script>
    <meta http-equiv="Content-Type" content="text/html; charset=ISO-8859-1" />
    <title>Best Entertainment Places, Tourism Destination and Tourist Spots in India, Entertainment
        Shows &#8211; Kingdom of Dreams</title>
    <meta name="Description" content="The Best entertainment place in India which brings live entertainment shows, live theatre, Indian cuisine and Indian handicrafts with bollywood style musical at one place. Most incredible tourism destination in India which gives a new meaning to nightlife." />
    <meta name="Keywords" content="best entertainment places, tourist places in india, entertainment, nightlife delhi, tourism destination india, tourist spots india, nightlife in india, live entertainment, entertainment show, bollywood online, bollywood watch, live theatre, Indian cuisine, Indian handicrafts, incredible India" />
    <meta name="Keywords" content="best entertainment places, tourist places in india, entertainment, nightlife delhi, tourism destination india, tourist spots india, nightlife in india, live entertainment, entertainment show, bollwood online, bollwood watch, live theatre, indian cuisine, indian handicrafts, incredible india" />
    <meta name="google-site-verification" content="uyhhZzldovhBWZih7cjw65a2fswRp5n4xQnp2oZ79iM" />
    <link href="../Skins/css/navigation.css" type="text/css" rel="stylesheet" />
    <link href="../Skins/css/scroll-bar.css" type="text/css" rel="stylesheet" />
    <link href="../Skins/css/style.css" type="text/css" rel="stylesheet" />
    <link href="../Skins/css/anylinkcssmenu.css" type="text/css" rel="stylesheet" />
    <link href="../Skins/css/royaltyCard.css" type="text/css" rel="stylesheet" />
    <link href="../Skins/css/scroll-bar.css" type="text/css" rel="stylesheet" />
    <link rel="shortcut icon" type="image/x-icon" href="http://www.kingdomofdreams.in/images/favicon.ico" />
    <script language="javascript" type="text/javascript">
        function validLogin()
         {
//            if ($('[id*=txtMemberId]').val() == "")  {
//                alert("Please enter Membership Id");
//                $('[id*=txtMemberId]').focus();
//                return false;
//            }
//            if ($('[id*=txtMemberId]').val() == "RCM-XXXXXXX") {
//                alert("Please enter valid Membership Id");
//                $('[id*=txtMemberId]').focus();
//                return false;
//            }
//            if ($('[id*=txtPassword]').val() == "")  {
//                alert("Please enter Password");
//                $('[id*=txtPassword]').focus();
//                return false;
//            }
             //         
             alert("This feature is under construction");
        }
    </script>
    <script type="text/javascript">

        $(function () {

            $('#section').jScrollPane();

        });		
    </script>
    <style type="text/css">
        .divbody
        {
            width: 230px;
        }
        .leftsection
        {
            width: 200px;
            display: inline-block;
            margin-left: 15px;
            margin-top: 0;
            margin-bottom: 0;
        }
        .formcont
        {
            width: 220px;
            display: inline-block;
            margin: 8px 0 0 0;
        }
        .leftsection h1
        {
            font: 14px "Monotype Corsiva" , "Times New Roman" , Arial;
            color: #53360a;
            padding: 0px;
            padding-top: 5px;
            margin: 0px auto;
            text-align: center;
        }
        label
        {
            display: inline-block;
            color: #53360a;
            font: 14px "Monotype Corsiva" , "Times New Roman" , Arial;
            width: 85px;
        }
        
        input[type="text"]
        {
            display: inline-block;
            width: 115px;
            border: 1px solid #b37b4b;
            padding: 0 3px;
            height: 16px;
            font: 12px Arial, Helvetica, sans-serif;
        }
        input[type="password"]
        {
            display: inline-block;
            width: 115px;
            border: 1px solid #b37b4b;
            padding: 0 3px;
            height: 16px;
            font: 12px Arial, Helvetica, sans-serif;
        }
        .formcont p, .formcont p a, .formcont p a:hover
        {
            color: #854c21;
            font: 12px "Monotype Corsiva" , "Times New Roman" , Arial;
            padding: 0px;
            margin: 0px;
            display: inline-block;
            text-decoration: none;
            float: left;
        }
        .formcont p a:hover
        {
            text-decoration: underline;
        }
        .formcont span, .formcont span a, .formcont span a:hover
        {
            color: #482912;
            font: 13px "Monotype Corsiva" , "Times New Roman" , Arial;
            padding: 0 5px 0 0;
            margin-right: 5px;
            display: inline-block;
            text-decoration: none;
            float: right;
            margin-left: 0px;
            margin-top: 0px;
        }
        .formcont span a:hover
        {
            text-decoration: underline;
        }
        .home-flash
        {
            background-image: url(../Skins/images/Pattern.png);
            width: 761px; /*margin-left:0px;*/
            height: 290px; /*align:center*/
        }
        .animation_block
        {
            position: relative; /*	left: 50%; top: 50%;
	margin: -300px 0 0 -455px;
*/
        }
        .main_view
        {
            float: left;
            position: relative;
            top: -0.5px;
            left: 0px;
            width: 761px;
        }
        .image_reel
        {
            position: relative;
        }
        .window12
        {
            height: 290px;
            width: 770px;
            overflow: hidden; /*--Hides anything outside of the set width/height--*/
            position: relative;
            top: 0px;
            left: 0px;
        }
        .paging
        {
            position: absolute;
            bottom: 0px;
            right: 5px;
            width: 128px;
            height: 57px;
            z-index: 100; /*--Assures the paging stays on the top layer--*/
            text-align: center;
            line-height: 40px;
            font-size: 14px;
            display: none; /*--Hidden by default, will be later shown with jQuery--*/
        }
        .paging a
        {
            padding: 5px;
            text-decoration: none;
            color: #fff;
        }
        .paging a.active
        {
            font-weight: bold;
            background: #e6b931;
            border: 1px solid #610000;
            -moz-border-radius: 30px;
            -khtml-border-radius: 30px;
            -webkit-border-radius: 30px;
        }
        .paging a:hover
        {
            font-weight: bold;
        }
        .wrapper
        {
            width: 958px;
            margin: 0 auto;
        }
    </style>
    <script language="javascript" type="text/javascript">
    try
    {
   
        function getd(id) {

            return document.getElementById(id);

        }

        function showalert() {

            OpenDiv(getd("PopupMsgAlert"));

        }

        function loadFileToElement() {

            if (window.XMLHttpRequest) {// code for IE7+, Firefox, Chrome, Opera, Safari

                xmlhttp = new XMLHttpRequest();

            }

            else {// code for IE6, IE5

                xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");

            }

            xmlhttp.open("GET", "admin/Notice.xml?qs=" + new Date().getTime(), false);

            xmlhttp.send();

            xmlDoc = xmlhttp.responseXML;

            var x = xmlDoc.getElementsByTagName("notice");

            var DisplayMessage = x[0].getElementsByTagName("display")[0].childNodes[0].nodeValue;

            var Message = x[0].getElementsByTagName("message")[0].childNodes[0].nodeValue;

            if (DisplayMessage == "1") {

                getd("message").innerHTML = Message;

                showalert();

            }

        }
         }
    catch(){
    }
    </script>
    <script type="text/javascript" src="../Skins/js/jquery-1.3.2.min.js"></script>
    <script type="text/javascript" src="../Skins/js/sliderAni.js"></script>
    <script type="text/javascript" src="../Skins/js/anylinkcssmenu.js"></script>
    <script type="text/javascript">
        anylinkcssmenu.init("menu_anchors_class") ////Pass in the CSS class of anchor links (that contain a sub menu)
        anylinkcssmenu.init("anchorclass")
    </script>
</head>
<body class="home-page-bg_login" style="height: 765px;">
    <form id="Form1" runat="server"> <%--defaultbutton="BtnSubmit"--%>
    <div class="wrapper">
        <div class="logo-row">
            <div class="logo">
                <a href="http://www.kingdomofdreams.in/index.html">
                    <img src="../Skins/images/logo1.jpg"></a>
            </div>
            <div class="sociallink" style="margin-top:-22px;">
                <a target="_blank" href="http://www.tripadvisor.in/Attraction_Review-g297615-d1948085-Reviews-Kingdom_of_Dreams-Gurgaon_Haryana.html">
                    <img src="../Skins/images/tripadvisor.jpg" alt="Trip Advisor" height="22" width="22"></a>
                <a href="http://www.facebook.com/kingdomofdreams?ref=ts" target="_blank">
                    <img src="../Skins/nav-btn-new/facebook-icon.jpg" height="23" width="22"></a>&nbsp;
                <a href="http://www.linkedin.com/company/kingdom-of-dreams?trk=fc_badge" target="_blank">
                    <img src="../Skins/nav-btn-new/in-icon.jpg" height="23" width="24"></a> <a href="http://twitter.com/KingdomOfDreams"
                        target="_blank">
                        <img src="../Skins/nav-btn-new/twitter-icn.jpg" height="23" width="25"></a>&nbsp;</div>
        </div>
        <!-- logo-row ends here -->
        <div style="background: url(../Skins/images/tilebgd.png); width: 913px; margin: 0 auto;margin-top:-21px;" >
            <table border="0" align="center" cellpadding="0" cellspacing="0" id="Table_01" runat="server">
                <tr>
                    <td colspan="9">
                        <img src="../Skins/images/default_01.png" width="123" height="37" alt="" /><a href="Login.aspx"><img
                            src="../Skins/images/default_02.png" alt="" width="58" height="37" border="0" /></a><a
                                href="#" class="anchorclass" rel="membershiptiers"><img src="../Skins/images/default_03.png"
                                    alt="" width="146" height="37" border="0" /></a><a href="Benefits.aspx"><img src="../Skins/images/default_04.png"
                                        alt="" width="167" height="37" border="0" /></a><a href="TermsAndConditions.aspx"><img
                                            src="../Skins/images/default_05.png" alt="" width="170" height="37" border="0" /></a><a
                                                href="FAQ.aspx"><img src="../Skins/images/default_06.png" alt="" width="46" height="37"
                                                    border="0" /></a><a href="Feedback.aspx"><img src="../Skins/images/default_07.png"
                                                        alt="" width="80" height="37" border="0" /></a><img src="../Skins/images/default_08.png"
                                                            width="123" height="37" alt="" />
                        <div id="membershiptiers" class="anylinkcss">
                            <ul>
                                <li><a href="PurpleCard.aspx" style="background-color:Brown;">Purple Card</a></li>
                                <li ><a href="BlueCard.aspx" style="background-color:Brown;">Blue Card</a></li>
                                <li ><a href="PlatinumCard.aspx" style="background-color:Brown;">Platinum Card</a></li>
                                <li ><a href="TitaniumCard.aspx" style="background-color:Brown;">Titanium Card</a></li>
                            </ul>
                        </div>
                    </td>
                    <%--<td colspan="7"><img src="../Skins/images/KodNew_01.png" alt="" width="913" height="48" border="0" usemap="#Map2" /></td>--%>
                </tr>
                <tr>
                    <td colspan="2">
                        <img src="../Skins/images/KodNew_02.png" width="72" height="290" alt="" />
                    </td>
                    <td colspan="3" width="761" height="290" style="background-image: url(../Skins/images/Pattern1.png)">
                        <div class="home-flash">
                            <div class="animation_block">
                                <div class="main_view">
                                    <div class="window12">
                                        <div class="image_reel">
                                            <img src="../Skins/images/Banner-01.jpg" alt="" height="290px" width="767" />
                                            <%--<img src="../Skins/images/Banner-02.jpg" alt="" height="290px" width="767" />
                                            <img src="../Skins/images/Banner-03.jpg" alt="" height="290px" width="765" />
                                            <img src="../Skins/images/Banner-04.jpg" alt="" height="290px" width="772" />--%>
                                        </div>
                                    </div>
                                    <div style="display: block;" class="paging">
                                        <%--<a class="active" href="#" rel="1">1</a> <a href="#" rel="2">2</a> <a href="#" rel="3">
                                            3</a> <a href="#" rel="4">4</a>--%>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </td>
                    <td colspan="2">
                        <img src="../Skins/images/KodNew_04.png" width="80" height="290" alt="" />
                    </td>
                </tr>
                <tr>
                    <td colspan="7">
                        <img src="../Skins/images/KodNew_05.png" width="913" height="21" alt="" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <img src="../Skins/images/KodNew_06.png" width="68" height="123" alt="" />
                    </td>
                    <%--<td width="260">
        <div class="divbody">
			<div class="leftsection">
				<h1>MEMBER LOGIN</h1>
			
					<div class="formcont">
						<label>MEMBER ID :</label>
					    <input name="Email" type="text" title="Email" />
					</div>

					<div class="formcont">
						<label>PASSWORD :</label>
					    <input name="Email" type="password" title="Email" />
					</div>

					<div class="formcont">
						<p><a href="#">FORGOT PASSWORD?</a></p>
					    <span><a href="#">ENTER</a></span>
					</div>
				

			</div>

		</div>
        </td>--%>
                    <%--<td width="260" height="123" colspan="2" valign="top" style="background-image: url(../Skins/images/boxImg.png);
                        background-repeat: no-repeat; visibility:hidden;">
                        <div class="divbody">
                            <div class="leftsection" style="margin-top: 7px;">
                               <%-- <center>
                                    <asp:Label ID="lblMsg" runat="server" Text="Label" ForeColor="Red" Visible="false"
                                        Font-Size="Smaller"></asp:Label></center>
                                <div class="formcont">
                                    <label>
                                        MEMBER ID :</label>
                                        <asp:TextBox ID="txtMemberId" runat="server" ToolTip="Enter Member Id" input="text" Text="RCM-XXXXXXX" onFocus="WaterMark(this, event);" onBlur="WaterMark(this, event);" ></asp:TextBox>
                                   </div>
                                <div class="formcont">
                                    <label>
                                        PASSWORD :</label>
                                    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" ToolTip="Enter Password"
                                        Text="Password"></asp:TextBox>
                                        
                                </div>
                                <div class="formcont">
                                    <p>
                                        <%--<a href="ForgotPassword.aspx">FORGOT PASSWORD?</a>
                                        <a  onclick="javascript:return validLogin();">FORGOT PASSWORD?</a></p>
                                    <span><a>
                                    <input type="text" style="display:none"/>
                                        <asp:LinkButton ID="BtnSubmit" runat="server" Font-Size="Large" OnClientClick="javascript:return validLogin();">Enter</asp:LinkButton></a></span>
                                </div>
                                <div class="formcont" style="display: inline">
                                    <p>
                                        <a href="FirstTimeLogin.aspx">FIRST TIME LOGIN</a></p>
                                </div>
                            </div>
                        </div>
                    </td>--%>
                    <td></td>
                    <td colspan="2">
                        <%--<a href="Membership.aspx">
                            <img src="../Skins/images/Experience-Box.png" alt="" width="264" height="123" border="0" /></a>--%>
                    </td>
                    <td colspan="2">
                       
                            <%--<img src="../Skins/images/Special-Offers.png" alt="" width="281" height="123" border="0" />--%>
                    </td>
                    <td>
                        <img src="../Skins/images/KodNew_10.png" width="40" height="123" alt="" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <img src="../Skins/images/spacer.gif" width="68" height="1" alt="" />
                    </td>
                    <td>
                        <img src="../Skins/images/spacer.gif" width="4" height="1" alt="" />
                    </td>
                    <td>
                        <img src="../Skins/images/spacer.gif" width="256" height="1" alt="" />
                    </td>
                    <td>
                        <img src="../Skins/images/spacer.gif" width="264" height="1" alt="" />
                    </td>
                    <td>
                        <img src="../Skins/images/spacer.gif" width="241" height="1" alt="" />
                    </td>
                    <td>
                        <img src="../Skins/images/spacer.gif" width="40" height="1" alt="" />
                    </td>
                    <td>
                        <img src="../Skins/images/spacer.gif" width="40" height="1" alt="" />
                    </td>
                </tr>
            </table>
            <%--<div style="position:absolute; top: 469px; left: 376px;">
            <table><tr>
            <td>
                        <a href="Membership.aspx">
                            <img src="../Skins/images/Experience-Box.png" alt="" width="264" height="123" border="0" /></a>
                    </td>
                    <td colspan="2">
                       
                            <img src="../Skins/images/Special-Offers.png" alt="" width="281" height="123" border="0" />
                    </td></tr>
                    </table>
            </div>--%>
        </div>
        <div class="home-content" style="height: 100px; visibility: hidden">
         
        </div>
    </div>
    <!-- wrapper ends here -->
   
    <div class="footer">
        <div class="new-navigation">
            <div class="new-nav" style=" margin-top:25px;">
                <div class="the-kingdom">
                    <a href="http://www.kingdomofdreams.in/the-kingdom.html">
                        <img src="../Skins/nav-btn-new/the-kingdom.jpg" ></a></div>
                <div class="nautanki-mahal">
                    <a href="http://www.kingdomofdreams.in/nautankimahal.html">
                        <img src="../Skins/nav-btn-new/nautanki-mahal.jpg"></a></div>
                <div class="showshaa-threatre">
                    <a href="http://www.kingdomofdreams.in/showshaa.html">
                        <img src="../Skins/nav-btn-new/showshaa-theatre.jpg"></a></div>
                <div class="culture-gully">
                    <a href="http://www.kingdomofdreams.in/culturegully.html">
                        <img src="../Skins/nav-btn-new/culture-gully.jpg"></a></div>
                <div class="iifa-buzz">
                    <a href="http://www.kingdomofdreams.in/iifa-buzz.html">
                        <img src="../Skins/nav-btn-new/iifa-buzz.jpg"></a></div>
                <br>
                <div class="clr">
                </div>
                <div class="home">
                    <a href="http://www.kingdomofdreams.in/index.html">
                        <img src="../Skins/nav-btn-new/home.jpg" height="17" width="45"></a></div>
                <div class="about">
                    <a href="http://www.kingdomofdreams.in/about-us.html">
                        <img src="../Skins/nav-btn-new/about-us.jpg" height="17" width="77"></a></div>
                <div class="contact">
                    <a href="http://www.kingdomofdreams.in/contact-us.html">
                        <img src="../Skins/nav-btn-new/contact-us.jpg" height="17" width="89"></a></div>
                <div class="lost-found">
                    <a href="http://www.kingdomofdreams.in/lost-and-found.html">
                        <img src="../Skins/nav-btn-new/lost-and-found.jpg"></a></div>
                <div class="blog">
                    <a href="http://blog.kingdomofdreams.in/" target="_blank">
                        <img src="../Skins/nav-btn-new/blog.jpg"></a></div>
                <div class="career">
                    <a href="http://www.kingdomofdreams.in/career.html">
                        <img src="../Skins/nav-btn-new/career.jpg"></a></div>
                <div class="gcell">
                    <a href="http://www.gcell.in/" target="_blank">
                        <img src="../Skins/nav-btn-new/gcell.jpg"></a></div>
            </div>
        </div>
    </div>
    <div id="boxes">
        <div style="top: 438.5px; left: 710px; display: none;" id="PopupMsgAlert" runat="server"
            class="windowAlrtMsg">
            <a class="close" href="javascript:" onclick="closeAlertDiv();">
                <img src="images/close2.png" alt="#" style="float: right; margin: -20px -20pt 0pt 20px;"
                    height="26" width="26"></a>
            <h2>
                <center>
                    NOTICE
                </center>
            </h2>
            <p id="message">
                5 Big parties at Kingdom of Dreams with unlimited food and drinks waiting for you
                on the new years . For bookings Please call on 01244528000</p>
            <script type="text/javascript">

                window.setTimeout("loadFileToElement()", 0);</script>
        </div>
    </div>
    <div style="width: 1920px; height: 977px; display: none; opacity: 0.8;" id="mask">
    </div>
    <%--<map name="Map2" id="Map2">

  <area shape="rect" coords="129,11,177,29" href="#" />
  <area shape="rect" coords="187,11,322,29" href="#" />
  <area shape="rect" coords="331,10,490,30" href="Benefits.aspx" />
  <area shape="rect" coords="499,9,662,31" href="TermsAndConditions.aspx" />
  <area shape="rect" coords="672,9,708,31" href="FAQ.aspx" />
  <area shape="rect" coords="714,10,787,30" href="Feedback.aspx" />
</map>--%>
    <script type="text/javascript">



        var _gaq = _gaq || [];

        _gaq.push(['_setAccount', 'UA-17427160-2']);

        _gaq.push(['_trackPageview']);



        (function () {

            var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;

            ga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') + '.google-analytics.com/ga.js';

            var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);

        })();



    </script>
    </form>
</body>
</html>
