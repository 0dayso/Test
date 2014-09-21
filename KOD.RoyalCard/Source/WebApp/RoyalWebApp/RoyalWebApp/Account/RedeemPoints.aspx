<%@ Page Language="C#" MasterPageFile="~/Skins/Master/AccountMaster.Master" AutoEventWireup="true" CodeBehind="RedeemPoints.aspx.cs" Inherits="RoyalWebApp.Account.RedeemPoints" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHPageHeading" runat="server">
Redeem Points
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHPageData" runat="server">
<center>
    <table width="60%" cellpadding="3" cellspacing="3" class="divborder">
        <tr>
            <td valign="top" align="left"  width="150px" class="divtext">
                Member Id:
            </td>
            <td valign="top" align="left" class="divtext">
                <asp:Label ID="LblMemberId" runat="server" Style="font-weight: 700"></asp:Label>
            </td>
        </tr>
        <tr>
            <td valign="top" align="left"  width="150px" class="divtext">
                Remaining Points:
            </td>
            <td valign="top" align="left" class="divtext">
                <asp:Label ID="LblRemainingPoints" runat="server"></asp:Label>
            </td>
        </tr>
           <tr>
                <td valign="top" align="left" class="divtext" width="150px">
                After 24 hours Creditable points:</td>
                <td valign="top" align="left" class="divtext">
                    <asp:Label ID="LblAfter24Points" runat="server"></asp:Label>
                </td>
            </tr>
         <tr>
            <td valign="top" align="left"  width="150px" class="divtext">
                Remaining Amount:
            </td>
            <td valign="top" align="left" class="divtext">
                <asp:Label ID="LblRemainingAmount" runat="server"></asp:Label>
            </td>
        </tr>
         <tr>
            <td valign="top" align="left"  width="150px" class="divtext">
            &nbsp;
            </td>
            <td valign="top" align="left">

             <div class="button">
                <a href="#" onclick="Javascript:alert('Redeem section is comming soon '); return false;" target="_blank">
              Redeem Now
                </a><span></span></div>   
               
            </td>
        </tr>
    </table>
    </center>
</asp:Content>
