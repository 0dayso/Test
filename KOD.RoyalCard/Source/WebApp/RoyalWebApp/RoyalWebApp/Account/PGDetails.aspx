<%@ Page  Language="C#" MasterPageFile="~/Skins/Master/AccountMaster.Master" AutoEventWireup="true" CodeBehind="PGDetails.aspx.cs" Inherits="RoyalWebApp.Account.PGDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">   
    <style type="text/css">
        .style1
        {
            font-family: "Trebuchet MS", Arial, Helvetica, sans-serif;
            font-size: 12px;
            color: #3f260a;
            text-decoration: none;
            width: 78px;
            text-align: left;
        }
        .style2
        {
            width: 78px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHPageHeading" runat="server">
Payment Details
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHPageData" runat="server">
  <center>
    <table width="60%" cellpadding="3" cellspacing="3" class="divborder">        
         <tr>           
            <td valign="top" align="center" colspan="3"  class="divtext" style=" height:30px;">
               <asp:Label ID="LblError" runat="server" Style="font-weight: bold; color:red;"></asp:Label></td>
        </tr>
         <tr>
            <td valign="top" align="left" class="style1">
                Member Id:
            </td>
            <td valign="top" align="left" class="divtext">
                &nbsp;<asp:Label ID="LblMemberId" runat="server" Style="font-weight: bold"></asp:Label>
                <asp:Label ID="LblTransId" runat="server" Style=" display:none;" Visible="false"></asp:Label>
             </td>
            <td valign="top" align="left" class="divtext">
                &nbsp;</td>
        </tr>
         <tr>
            <td valign="top" align="left" class="style1">
                Type :
            </td>
            <td valign="top" align="left" class="divtext">
                <asp:Label ID="LblType" runat="server"></asp:Label>
             </td>
            <td valign="top" align="left" class="divtext">
                &nbsp;</td>
        </tr>     
         <tr>
            <td valign="top" align="left" class="style1">
                Amount :</td>
            <td valign="top" align="left" class="divtext">
                <asp:Label ID="LblAmount" runat="server"></asp:Label>/-
             </td>
            <td valign="top" align="left" class="divtext">
                &nbsp;</td>
        </tr>     
        <tr>
            <td valign="top" class="style1" align="right">         
                  Card Type:</td>
            <td valign="bottom" class="divtext" align="left">         
                  <asp:RadioButtonList ID="rbl_CardType" runat="server" CssClass="divtext">                
                    <asp:ListItem Value="AMEX" Text="American Express" Selected="True"> </asp:ListItem>
                    <asp:ListItem Value="HDFC" Text="HDFC(MASTER/VISA)" />
                    <asp:ListItem Value="IDBI" Text="MASTER/VISA"></asp:ListItem>                  
                </asp:RadioButtonList>
              
            </td>
            <td valign="bottom" class="divtext" align="left">         
                  <img src="../Skins/images/amex.jpg" width="160px" height="50px" style="vertical-align:middle;" /></td>
        </tr>
        <tr>
            <td valign="top" class="style1">         
             
                &nbsp;</td>
            <td valign="top" class="divtext">         
                 <div class="button">
                <a href="#">
                <asp:LinkButton ID="btnSubmit" runat="server"  OnClick="BtnSubmit_Click"  Width="55px" Text="Proceed" ></asp:LinkButton>
                </a><span></span></div></td>
            <td valign="top" class="divtext">         
                 &nbsp;</td>
        </tr>
        <tr>
            <td align="left" class="style2">              
                <asp:ValidationSummary ID="ValidationSummary1" HeaderText="Some Fields were missing..."
                    ShowMessageBox="true" DisplayMode="List" ValidationGroup="cont" ShowSummary="false"
                    runat="server" />
            </td>
            <td align="left">              
                <asp:Label ID="lblMess" runat="server" CssClass="error"></asp:Label>
            </td>
            <td align="left">              
                &nbsp;</td>
        </tr>
    </table>
    </center>
</asp:Content>