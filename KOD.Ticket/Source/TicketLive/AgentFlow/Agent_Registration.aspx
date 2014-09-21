<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Agent_Registration.aspx.cs" MasterPageFile="Master_Login.master" Inherits="AgentFlow_Agent_Registration" %>

<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" runat="Server">
<center><asp:Label ID="Label1" runat="server" Text="Agent Detail" style=" font-size: large; font-family:Arial;"></asp:Label></center>
<asp:Label ID="Label2" runat="server" Text="Contect Details" style="text-decoration:underline"></asp:Label><br /><br />
<table border="1">
<tr>
<td>
Salutation
</td>
<td>:</td>
<td>
<asp:RadioButtonList ID="RdGender" runat="server" RepeatDirection="Horizontal" class="divtext">
     <asp:ListItem Value="Mr." Text="Mr." Selected="True"></asp:ListItem>
     <asp:ListItem Value="Ms" Text="Ms"></asp:ListItem>
     <asp:ListItem Value="Others" Text="Others"></asp:ListItem>
</asp:RadioButtonList>
</td>
</tr>
<tr>
<td>
First Name
</td>
<td>:</td>
<td>
<asp:TextBox ID="txtFirstName"  runat="server"></asp:TextBox>
</td>
<td>
Last Name
</td>
<td>:</td>
<td class="style7">
<asp:TextBox ID="txtLastName" runat="server"></asp:TextBox>
</td>
</tr>
<tr>
<td>
Address
</td>
<td>:</td>
<td>
<asp:TextBox  ID="txtAddress" class="text" runat="server"  TextMode="MultiLine" Height="30px" />
</td>
<td>
City
</td>
<td>:</td>
<td>
<asp:TextBox ID="txtCity" runat="server"></asp:TextBox>
</td>
</tr>
<tr>
<td>
State
</td>
<td>:</td>
<td>
<asp:TextBox ID="DdlCountry" runat="server"></asp:TextBox>
</td>
<td>
Country
</td>
<td>:</td>
<td>
<asp:TextBox ID="txtEmailId" runat="server"></asp:TextBox>
</td>
</tr>
<tr>
<td>
Zip Code
</td>
<td>:</td>
<td>
<asp:TextBox ID="txtpin" runat="server" MaxLength="6"></asp:TextBox>
</td>
<td>
Contact Person
</td>
<td>:</td>
<td>
<asp:TextBox ID="txtMobileNo" runat="server"></asp:TextBox>
</td>
</tr>
<tr>
<td>
Mobile No.
</td>
<td>:</td>
<td>
<asp:TextBox ID="TextBox1" runat="server" MaxLength="6"></asp:TextBox>
</td>
<td>
LandLine No.
</td>
<td>:</td>
<td>
<asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
</td>
</tr>
<tr>
<td>
Email ID
</td>
<td>:</td>
<td>
<asp:TextBox ID="TextBox3" runat="server" MaxLength="6"></asp:TextBox>
</td>
</tr>
</table><br /><br />
<asp:Label ID="Label3" runat="server" Text="Account Details" style="text-decoration:underline"></asp:Label><br /><br />
<table border="1">
<tr>
<td>
Bank Name
</td>
<td>:</td>
<td>
<asp:TextBox ID="TextBox15"  runat="server"></asp:TextBox>
</td>
<td>
Bank Account No.
</td>
<td>:</td>
<td>
<asp:TextBox ID="TextBox9"  runat="server"></asp:TextBox>
</td>
</tr>
<tr>
<td>
Bank Branch Name
</td>
<td>:</td>
<td>
<asp:TextBox ID="TextBox4"  runat="server"></asp:TextBox>
</td>
<td>
Bank Branch Code
</td>
<td>:</td>
<td class="style7">
<asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
</td>
</tr>
<tr>
<td>
Pan Card No.
</td>
<td>:</td>
<td>
<asp:TextBox  ID="TextBox6" class="text" runat="server"/>
</td>
<td>
Pan Card Holder
</td>
<td>:</td>
<td>
<asp:TextBox ID="TextBox7" runat="server"></asp:TextBox>
</td>
</tr>
</table><br/><br/>
<asp:Label ID="Label4" runat="server" Text="Other Details" style="text-decoration:underline"></asp:Label><br /><br />
<table border="1">
<tr>
<td>
Credit Limit
</td>
<td>:</td>
<td>
<asp:TextBox ID="TextBox8"  runat="server"></asp:TextBox>
</td>
<td>
Credit Seats
</td>
<td>:</td>
<td>
<asp:TextBox ID="TextBox10"  runat="server"></asp:TextBox>
</td>
</tr>
<tr>
<td>
Currency Code
</td>
<td>:</td>
<td>
<asp:TextBox ID="TextBox11"  runat="server"></asp:TextBox>
</td>
<td>
Activation Date
</td>
<td>:</td>
<td class="style7">
<asp:TextBox ID="TextBox12" runat="server"></asp:TextBox>
</td>
</tr>
<tr>
<td>
Inactive Date
</td>
<td>:</td>
<td>
<asp:TextBox  ID="TextBox13" class="text" runat="server"/>
</td>
<td>
Area Code
</td>
<td>:</td>
<td>
<asp:TextBox ID="TextBox14" runat="server"></asp:TextBox>
</td>
</tr>
<tr>
<td>
Marketing Executive
</td>
<td>:</td>
<td>
<asp:TextBox ID="TextBox16"  runat="server"></asp:TextBox>
</td>
<td>
Agent Approved By
</td>
<td>:</td>
<td>
<asp:TextBox ID="TextBox17"  runat="server"></asp:TextBox>
</td>
</tr>
<tr>
<td>
Referred By
</td>
<td>:</td>
<td>
<asp:TextBox ID="TextBox18"  runat="server"></asp:TextBox>
</td>
<td>
Security Deposit
</td>
<td>:</td>
<td class="style7">
<asp:TextBox ID="TextBox19" runat="server"></asp:TextBox>
</td>
</tr>
<tr>
<td>
Commission Percentage
</td>
<td>:</td>
<td>
<asp:TextBox  ID="TextBox20" class="text" runat="server"/>
</td>
</tr>
</table>
</asp:Content>
