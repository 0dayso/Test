<%@ Page MasterPageFile="~/Skins/Master/AccountMaster.Master" Language="C#" AutoEventWireup="true" CodeBehind="ReturnReceipt.aspx.cs"
    Inherits="RoyalWebApp.Payment.Idbi.ReturnReceipt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHPageData" runat="server">
    
    <center style="font-family: Verdana; font-size: small; margin-top: 100px">
        <h2>
            Please wait Transaction is in Progress...
        </h2>
        <img id="ima" runat="server" src="~/images/103.gif" alt="Please Wait" />
        <br />
        Please Do not Close or Refresh this window...
    </center>
</asp:Content>
