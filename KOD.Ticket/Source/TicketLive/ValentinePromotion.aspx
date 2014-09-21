<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ValentinePromotion.aspx.cs" Inherits="ValentinePromotion" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Src="Controls/Footer.ascx" TagName="Footer" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<style type="text/css">
    .seats
{
    width: 846px;
    float: left;
    background: url(images/Valentine-bg.jpg) no-repeat;
    overflow: auto;
    height: 489px;
    padding-left:245px;
    *padding-left:245px;
}
</style>
  <script type="text/javascript">
        history.forward();
    </script>

    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Kingdom of Dreams : Ticket Booking</title>
    <link href="css/style.css" type="text/css" rel="stylesheet" />
    <link rel="shortcut icon" type="image/x-icon" href="images/favicon.ico" />
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
                    <img src="images/logo.jpg" /></a>
            </div>
        </div>
        <div class="seats-main">
            <div class="seats">
                <div class="ticket-main">
                 <table style="position: relative; width="100%">
                <tr align="center">
                    <td >
                   &nbsp &nbsp &nbsp&nbsp &nbsp <b style="font-size: medium">Valentine Promotion</b>
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
               <td></td>
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
               <td></td>
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
                        <b><font color="red">
                            <asp:Label ID="lblMsgPromotionCode" runat="server" Text="Label"></asp:Label></font><b>
                    </td>
                </tr>
            <tr><td></td></tr>
             <tr><td></td></tr>

                <tr>
                    <td class="style1">
                        <b>Enter Your Promotion Code Below</b> &nbsp&nbsp<b>:</b>
                    </td>
                </tr>
                <tr><td></td></tr>
                <tr>
                    <td>
                        <asp:TextBox ID="txtPromotionCode" Width="160px" runat="server" CssClass="text-small"></asp:TextBox>
                        <br></br>
                      
                      
                        
                        <asp:Button ID="btnPromotionCode" CssClass="common-button" runat="server" Text="Submit" 
                         OnClick="btnPromotionCode_Click" />
                    </td>
                </tr>
               
            </table>

               </div>
            </div>
        </div>
        <!-- seats-main ends here -->
    </div>
    <uc1:Footer ID="Footer1" runat="server" />
    <!-- footer-main ends here -->
    </form>
</body>
</html>
