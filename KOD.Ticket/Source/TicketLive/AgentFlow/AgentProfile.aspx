<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AgentProfile.aspx.cs" MasterPageFile="Master_Login.master" Inherits="AgentFlow_AgentProfile" %>

<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" runat="Server">
<div style="float:left;">
<table style=" float:left; padding-top:80px; padding-left:20px">
<tr>
<td style="width: 138px"><img src="images/arrow.png" /><asp:LinkButton ID="LinkButton2" runat="server" style="color:#ECA035; text-decoration:none;"><asp:Label ID="Label9" runat="server" Text="Label">My Profile</asp:Label></asp:LinkButton></td>
</tr>
<tr>
<td style="width: 138px"></td>
</tr>
<tr>
<td style="width: 138px"></td>
</tr>
<tr>
<td style="width: 138px"><img src="images/arrow.png" /><asp:LinkButton 
        ID="LinkButton3" runat="server" style="color:#ECA035; text-decoration:none;" 
        onclick="LinkButton3_Click"><asp:Label ID="Label10" runat="server" Text="Label">Book Tickets</asp:Label></asp:LinkButton></td>
</tr>
<tr>
<td style="width: 138px"></td>
</tr>
<tr>
<td style="width: 138px"></td>
</tr>
<tr>
<td style="width: 138px"><img src="images/arrow.png" /><asp:LinkButton ID="LinkButton4" runat="server" style="color:#ECA035; text-decoration:none;"><asp:Label ID="Label11" runat="server" Text="Label">Booking History</asp:Label></asp:LinkButton></td>
</tr>
<tr>
<td style="width: 138px"></td>
</tr>
<tr>
<td style="width: 138px"></td>
</tr>
<tr>
<td style="width: 138px"><img src="images/arrow.png" /><asp:LinkButton ID="LinkButton5" runat="server" style="color:#ECA035; text-decoration:none;"><asp:Label ID="Label8" runat="server" Text="Label">Change Password</asp:Label></asp:LinkButton></td>
</tr>
<tr>
<td style="width: 138px"></td>
</tr>
<tr>
<td style="width: 138px"></td>
</tr>
<tr>
<td style="width: 138px"><img src="images/arrow.png" /><asp:LinkButton 
        ID="LinkButton6" runat="server" style="color:#ECA035; text-decoration:none;" 
        onclick="LinkButton6_Click"><asp:Label ID="Label12" runat="server" Text="Label">Logout</asp:Label></asp:LinkButton></td>
</tr>
</table>
</div>
<div style="padding-top:0px;padding-right:20px;float:right;">
<asp:Label ID="Label3" runat="server" Text="Last Login" style="float:right;text-decoration:underline"></asp:Label><br /><br />
<div style="background-image:url('images/bar.png');height:28px;width:515px;">
<asp:Label ID="Label1" runat="server"  Text="My Profile" style="float:left;padding-top:5px;padding-left:5px;"></asp:Label>
<asp:LinkButton ID="LinkButton1" runat="server"><asp:Label ID="Label2" runat="server" Text="Update Profile" style="float:right;padding-top:5px;padding-right:5px;"></asp:Label></asp:LinkButton>
</div><br/>
<asp:Label ID="Label4" runat="server" Text="Welcome to Rintu Singh" style="float:left; color:White;"></asp:Label><br /><br /><hr />
<asp:Label ID="Label5" runat="server" Text="Contect Details" style="float:left; color:White; text-decoration:underline"></asp:Label><br /><br />
<table border="1">
<tr>
<td>
Address :
</td>
<td>
</td>
<td>
Location :
</td>
<td>
</td>
</tr>
<tr>
<td>
Contect Person :
</td>
<td>
</td>
</tr>
<tr>
<td>
Contect No. :
</td>
<td>
</td>
<td>
Email Address :
</td>
<td>
</td>
</tr>
</table><br /><hr />
<asp:Label ID="Label6" runat="server" Text="Account Details" style="float:left; color:White;text-decoration:underline"></asp:Label><br /><br />
<table border="1">
<tr>
<td>
Account No. :
</td>
<td>
</td>
<td>
Pan Card No. :
</td>
<td>
</td>
</tr>
<tr>
<td>
Branch Name :
</td>
<td>
</td> 
<td>
Pan Card Holder :
</td>
<td>
</td>
</tr>
</table><br /><hr />
<asp:Label ID="Label7" runat="server" Text="Agent Details" style="float:left; color:White;text-decoration:underline"></asp:Label><br /><br />
<table border="1">
<tr>
<td>
Agent Code :
</td>
<td>
</td>
<td>
Sub Code :
</td>
<td>
</td>
<td>
Area Code :
</td>
<td>
</td>
</tr>
<tr>
<td>
Security Deposite :
</td>
<td>
</td> 
<td>
Credit Value :
</td>
<td>
</td>
<td>
Credit Seats :
</td>
<td>
</td>
</tr>
<tr>
<td>
Total Booking :
</td>
<td>
</td>
<td>
Total Booking :
</td>
<td>
</td>
<td>
Current Outstanding :
</td>
<td>
</td>
</tr>
<tr>
<td>
Agent Type :
</td>
<td>
</td> 
<td>
Commision :
</td>
<td>
</td>
<td>
Currency Code :
</td>
<td>
</td>
</tr>
<tr>
<td>
Activation Date :
</td>
<td>
</td>
<td>
Inactive Date :
</td>
<td>
</td>
</tr>
<tr>
<td>
Marketing Executive :
</td>
<td>
</td> 
<td>
Agent Approved By :
</td>
<td>
</td>
</tr>
</table><br /><hr />
</div>
</asp:Content>
