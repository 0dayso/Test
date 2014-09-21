<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserRegisterationSuccess.aspx.cs" Inherits="RoyalWebApp.UserRegisterationSuccess" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
           <style type=
           "text/css">
           .home-page-bg {
	background:url(../Skins/images/LoginPage_home-page-bg.jpg) no-repeat center top #000000;
}
           </style>
		<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
		<title>Kingdom of Dreams :: Royalty Card</title>
		<meta name="Description" content="" />
		<meta name="Keywords" content="" />
    <link href="~/Skins/css/royaltyCard.css" rel="stylesheet" type="text/css" />    	
		</head>
	<body class="home-page-bg">
    <form id="form1" runat="server">   
<div class="wrapper">
          <div class="logo-row" style= "background:url(../Skins/images/logo.jpg) no-repeat;">
            
  </div>    
        <div class="contentDiv" style="margin-top:-30px; vertical-align:top;">
        <!--PV Middle Content -->
        <center>
         <table width="60%" cellpadding="3" cellspacing="3" class="divborder" style="margin-top:60px;">        
         <tr>           
            <td valign="top" align="center" colspan="3"  class="divtext" style=" height:30px;">
             <center>
             <div class="button">
                <a href="Login.aspx">
               Go Back To Home
                </a><span></span></div>
                </center>
               </td>
        </tr>
         <tr>
            <td valign="top" align="left" class="style1">
          
          <asp:Label ID="LblMsg" runat="server" Font-Bold="true"></asp:Label>
    <center>
    <div id="DivResult" runat="server" style="height:200px; width:270px; margin-top:60;" >
    <div style="color:Red; font-size:15"><b>
Thank you for Registering !<br />
Please check your Email registered with us for further information.
</b></div>
    </div>
    </center>
           </td>
           </tr>
           </table>
           </center>
             <!--PV End Middle Content --> 
                </div>
     
  </div>
     
</form>
</body>
</html>