<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MakeMyTripPromotion.aspx.cs" Inherits="MakeMyTripPromotion" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
      
       
       
.seats-inside1
{
    width: 846px;
    float: left;
    background: url(images/makemytrippromotiondomestic.jpg) no-repeat;
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
</head>
<body class="home-page-bg">
    <form id="form1" runat="server">    
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="True">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
<ContentTemplate>
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
                
                <table style="position: relative;" width="100%">
            <asp:UpdateProgress ID="uppero" runat="server" DynamicLayout="true">
                        <ProgressTemplate>
                            <div style="height: 355px; background-color: #C79EA7; width: 280px; position: absolute;
                                top: 235px; left: 535px; border-left: 4px solid #A67782; border-right: 4px solid #A67782;
                                color: #FFC419; filter: alpha(opacity=80); opacity: 0.8; text-align: center;
                                z-index: 100">
                                <div style="font-family: Verdana; font-size: 12px; color: Black; font-weight: bold;
                                    margin-top: 95px">
                                    Loading Please Wait...
                                </div>
                            </div>
                        </ProgressTemplate>
                    </asp:UpdateProgress>
      <%--  <tr>
                    <td>
                        <asp:Image ID="Image1" runat="server" ImageUrl="~/images/makemytrip.PNG" Width="270"
                            /><br /><br /><br />
                    </td>
                </tr>--%>
                <tr>
                    <td>
                        <b><font color="red">
                            <asp:Label ID="lblMsgPromotionCode" runat="server" Text="Label"></asp:Label></font></b>
                    </td>
                </tr>
              

                <tr>
                    <td class="style1">
                        <b>Enter Your Promotion Code Below</b> &nbsp&nbsp<b>:</b>
                        
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="txtMMTPromotionCode" Width="160px" runat="server" CssClass="text-small"></asp:TextBox>
                        </td>
                </tr>
                <tr>
                <td class="style1">
                        <b>Enter your MakeMyTrip booking ID here</b> &nbsp&nbsp<b>:</b>                   
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="txtMMTPNR" Width="160px" runat="server" CssClass="text-small"></asp:TextBox>
                        <br />
                        
                        </td>
                        </tr>
                         <tr>
                    <td>
                        <asp:LinkButton ID="TCiNdigo" runat="server" CssClass="clickhere">For Package Details</asp:LinkButton>
                        </td>
                </tr>
                        <tr>
                <td>
                        <asp:Button ID="btnMMTPromotionCode" runat="server" CssClass="common-button" 
                         Text="Submit" onclick="btnMMTPromotionCode_Click" />
                        </td>
                </tr>
                </table>
                
                <cc1:ModalPopupExtender ID="mo12" PopupControlID="dv_pop" BackgroundCssClass="modalBackground2"
        CancelControlID="btnClose" TargetControlID="TCiNdigo" runat="server">
    </cc1:ModalPopupExtender>
     <div id="dv_pop" class="ModalWindow" style="display: none; width: 550px; height: 230px;"
        runat="server">
              
        <div id="Div1" runat="server" style="overflow: auto; width: 530px; height: 190px; padding: 0px 10px 0px 10px">

           
         <font color="red"> <u><b> <center>Terms & Conditions</center></b></u></font>
           <p>1.The discount offer is applicable for all the bookings made till 20th May’2013 with date of travel on or before 14th August’2013.<br /><br />
                2.Customers can avail the offer after within 30 days of completing the travel upon production of boarding card in original.<br /><br />
                    3.These discounts cannot be clubbed with any other promotion and are subject to availability.<br /><br />
                    4. All the Terms & Conditions of Jhumroo/Zangoora are also applied.</p>
                    
                </div>  
       
    <hr />
        <asp:Button Text="Close" runat="server" ID="btnClose" CssClass="common-button"  />

        </div>
                </div>
                </div>
                </div></div>
                </ContentTemplate>
                </asp:UpdatePanel>
                </form>
</body>
</html>
