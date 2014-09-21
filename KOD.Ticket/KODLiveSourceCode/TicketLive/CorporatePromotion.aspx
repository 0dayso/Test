<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="CorporatePromotion.aspx.cs" Inherits="CorporatePromotion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" Runat="Server">
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
<table style="position: relative; top: 0px; left: 0px; width: 319px;"100%">
<tr>
<td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
<td>
<b style="font-size: medium;"> Corporate Promotion</b>
</td>
</tr>
</table><br />
<table style="position: relative; top: 0px; left: 0px; width: 317px;"100%">
<tr>
<td></td>
</tr>
<tr>
<td>&nbsp;&nbsp;
<a href="Promotion.aspx"><asp:Image ID="imgAirtelPromo" runat="server" 
        ImageUrl="~/images/airtelPromotion.png" Width="80px" Height="45px"/></a>&nbsp;
<a href="IndigoPromotion.aspx"><asp:Image ID="imgIndigoPromo" runat="server" 
        ImageUrl="~/images/IndigoPromotion.png" Width="80px" Height="45px"/></a>&nbsp;
<a href="MakeMyTripPromotion.aspx">
    <asp:Image ID="imgMMTPromo" runat="server" 
        ImageUrl="~/images/MMTPromotion.png" Width="80px" Height="45px"/><br /><br /></a>
</td>
</tr>
<tr>
<td colspan="2">
<b style="font-size: medium;">&nbsp;Click on the Desired Promotion</b>
</td>
</tr>
</table>
</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>

