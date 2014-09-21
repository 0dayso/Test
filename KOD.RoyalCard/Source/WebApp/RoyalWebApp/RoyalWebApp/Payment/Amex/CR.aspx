<%@ Page Title="" Language="C#" MasterPageFile="~/Skins/Master/Payment.Master" AutoEventWireup="true" CodeBehind="CR.aspx.cs" Inherits="RoyalWebApp.Payment.Amex.CR1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHPageData" runat="server">
    <asp:Label ID="LblMsg" runat="server" Font-Bold="true"></asp:Label>
    <center>
    <div id="DivResult" runat="server" style="height:200px; width:270px; margin-top:60;" >
    <% GetResponse(); %>
    </div>
    </center>
</asp:Content>
