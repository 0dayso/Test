<%@ Page Title="" Language="C#" MasterPageFile="~/Skins/Master/Payment.Master" AutoEventWireup="true" CodeBehind="Print-Receipt.aspx.cs" Inherits="RoyalWebApp.Payment.HDFC.Print_Receipt" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHPageData" runat="server">
<center>
<div id="DivResult" runat="server" style="height:200px; width:270px; margin-top:60;" >
    <table runat="server" visible="false" id="receipt">
    <tr>
    <td colspan='2'>
        <asp:Label ID="lblisSuccessful" runat="server"></asp:Label>
    </td>
    </tr>
    <tr>
    <td>
    Type:
    </td>
    <td>
        <asp:Label ID="lbltype" runat="server" ></asp:Label>
    </td>
    </tr>
     <tr>
    <td>
    Amount:
    </td>
    <td>
        <asp:Label ID="lblAmount" runat="server" ></asp:Label>
    </td>
    </tr>
     <tr>
    <td>
    Transaction ID :
    </td>
    <td>
        <asp:Label ID="lbltransID" runat="server" ></asp:Label>
    </td>
    </tr>
    </table>
    </div>
    </center>
</asp:Content>
