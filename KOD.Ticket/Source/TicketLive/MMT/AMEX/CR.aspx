﻿<%@ Page Language="C#" MasterPageFile="~/PG.master" AutoEventWireup="true" CodeFile="CR.aspx.cs"
    Inherits="MMT_AMEX_CR" %>

<asp:Content ID="content" runat="server" ContentPlaceHolderID="Content1">
    <center style="font-family: Verdana; font-size: small; margin-top: 100px">
        <h2>
            Please wait Transaction is in Progress...
        </h2>
        <img id="ima" runat="server" src="~/images/103.gif" alt="Please Wait" />
        <br />
        Please Do not Close or Refresh this window...
    </center>
</asp:Content>
