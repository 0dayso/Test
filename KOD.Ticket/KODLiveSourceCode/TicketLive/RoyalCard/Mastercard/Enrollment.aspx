<%@ Page Title="" Language="C#" MasterPageFile="~/RoyalCard/Mastercard/Master/MasterCard1.master" AutoEventWireup="true" CodeFile="Enrollment.aspx.cs" Inherits="RoyalCard_Mastercard_Enrollment" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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
.buttontickets{
	float: right;
	background-color:#eed075;
	border-color:Orange;
	color: #231f20;
	width:80px;
 font-weight:bold;
         }
         .buttontickets1{
	float:left;
	background-color:#eed075;
	border-color:Orange;
	color: #231f20;
	width:80px;
 font-weight:bold;
         }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHPageHeading" Runat="Server">
Master Card Details:
<br /><br />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHPageData" Runat="Server">
<table>
<tr>
<td class="style6">
</td>
</tr>
<tr>
<td class="style7">
Enter First 6 Digits of Your World Card-Master Card :
</td>
</tr>
<tr>
<td>
<asp:TextBox ID="Txtcardno" runat="server" MaxLength="6"></asp:TextBox>
<cc1:FilteredTextBoxExtender ID="F1" FilterType="Numbers" TargetControlID="Txtcardno" runat="server">
</cc1:FilteredTextBoxExtender>
</td>
</tr>
<tr>
<td class="style6">
</td>
</tr>
<tr>
<td class="style6">
</td>
</tr>

<tr>
<td class="style7">
<asp:Label ID="lblbankname" runat="server" Text="Select Your Bank Name :"></asp:Label>
</td>
</tr>
<tr>
<td class="style7">
<asp:DropDownList ID="ddlbankname" runat="server" Width="162px" CssClass="text-small">
<asp:ListItem Value="select" Text="Select Bank Name"></asp:ListItem>    
</asp:DropDownList>
</td>
</tr>
<tr>
<td class="style6">
</td>
</tr>
<tr>
<td class="style6">
</td>
</tr>
<tr>
<td class="style6">
</td>
</tr>
<tr>
<td class="style6">
</td>
</tr>
<tr>
<td class="style6">
</td>
</tr>
</table>
<table>
<tr>
<th class="style6">
<div class="buttonp" style="float:left;">
<asp:LinkButton ID="btnSubmit" runat="server" Width="97px"  Text="Proceed" 
        onclick="btnSubmit_Click"></asp:LinkButton>
<span></span>
</div>
</th>
<th class="style8"></th>
<th class="style10"></th>
<th class="style10"></th>
<th class="style10"></th>
<th class="style10"></th>
<th class="style10"></th>
<th class="style10"></th>
<th class="style10"></th>
<th class="style10"></th>
<th class="style10"></th>
<th class="style10"></th>
<th class="style10"></th>
<th class="style10"></th>
<th class="style10"></th>
<th class="style10"></th>
<th class="style10"></th>
<th class="style10"></th>
<th class="style10"></th>
<th class="style10"></th>
<th class="style10"></th>
<th class="style10"></th>
<th class="style10"></th>
<th class="style10"></th>
<th class="style10"></th>
<th class="style10"></th>
<th class="style10">
<div class="buttonp" style="float:right;">
        <asp:LinkButton ID="btnBackHome" runat="server"  Width="97px" Text="Back To Home"  ></asp:LinkButton>
<span></span>
</div>
</th>
</tr>
</table>
<table>
<tr>
<td class="style6">
<font color="red">
    <asp:Label ID="lblmsg" runat="server" Text="Label" Visible="true"></asp:Label>
</font>
</td>
</tr>
</table>

   <div id="grayBG" runat="server" class="grayBox" style="display:inline;">
   <div id="showcontainer" runat="server" class="box_content" style="border:solid 1px #FF9900;display:inline;"  >
    </div>         
<div id="Container" class="box_content1" runat="server" style="background-color:White;display:inline;">
<center><p style="color: #000000"><b>Sorry, your card is not eligible for this Offer.</b></p></center> 
<br /><br />
<hr />
<asp:Button ID="btnclose" class="buttontickets1" runat="server" Text="Close" 
        onclick="btnclose_Click" />
<%--<asp:Button ID="btnproceed" class="buttontickets" runat="server" Text="Proceed" 
        onclick="btnproceed_Click" />--%>
</div></div>


</asp:Content>

