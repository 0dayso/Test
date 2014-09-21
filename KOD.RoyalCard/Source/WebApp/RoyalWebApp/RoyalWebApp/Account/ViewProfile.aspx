<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewProfile.aspx.cs" Inherits="RoyalWebApp.Account.ViewProfile"  MasterPageFile="~/Skins/Master/AccountMaster.master" %>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHPageHeading" runat="server">
<div style="color:Black">View Profile</div>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHPageData" runat="server">
    <center>
       <table width="70%" cellpadding="3" cellspacing="3" class="divborder">    
            <tr>
                <td valign="top" align="left" class="divtext" width="150px">
                    Member Id:</td>
                <td valign="top" align="left">
                    <asp:Label ID="LblMemberId" runat="server" style="font-weight: 700"  class="divtext"></asp:Label>
                </td>
            </tr>
            <tr>
                <td valign="top" align="left" class="divtext" width="150px">
                    Full Name:
                </td>
                <td valign="top" align="left">
                    <asp:Label ID="LblSalutation" runat="server" CssClass="divtext"></asp:Label>
                    <b>
                        <asp:Label ID="LblName" runat="server" CssClass="divtext"></asp:Label>
                    </b>
                </td>
            </tr>
            <tr>
                <td valign="top" align="left" class="divtext" width="180px">
                    Organization:
                </td>
                <td valign="top" align="left">
                    <asp:Label ID="LblCompanyName" runat="server" CssClass="divtext" />
                </td>
            </tr>
            <tr>
                <td valign="top" align="left" class="divtext" width="150px">
                    Designation:
                </td>
                <td valign="top" align="left">
                    <asp:Label ID="LblDesignation" runat="server" CssClass="divtext" />
                </td>
            </tr>
            <tr>
                <td valign="top" align="left" class="divtext" width="150px">
                    Postal Address:
                </td>
                <td valign="top" align="left">
                    <asp:Label ID="LblAddress" runat="server" CssClass="divtext" />
                </td>
            </tr>
            <tr>
                <td valign="top" align="left" class="divtext" width="150px">
                    Email ID:
                </td>
                <td valign="top" align="left">
                    <asp:Label ID="LblEmailId" runat="server" CssClass="divtext" />
                </td>
            </tr>
            <tr>
                <td valign="top" align="left" class="divtext" width="150px">
                    Mobile No:
                </td>
                <td valign="top" align="left">
                    <asp:Label ID="LblMobileNo" runat="server" CssClass="divtext" />
                </td>
            </tr>
           
            <tr>
                <td valign="top" align="left" class="divtext" width="180px">
                    Date Of Birth:
                </td>
                <td valign="top" align="left">
                    <asp:Label ID="LblDob" runat="server" CssClass="divtext" />
                </td>
            </tr>
            <tr>
                <td valign="top" align="left" class="divtext" width="180px">
                    Marital Status:
                </td>
                <td valign="top" align="left">
                    <asp:Label ID="LblStatus" runat="server" CssClass="divtext" />
                </td>
            </tr>
            <tr>
                <td valign="top" align="left" class="divtext" width="180px">
                    Anniversary:
                </td>
                <td valign="top" align="left">
                    <asp:Label ID="LblAnniversary" runat="server" CssClass="divtext" />
                </td>
            </tr>
              <tr>
                <td valign="top" align="left" class="divtext" width="180px">
                   &nbsp;
                </td>
                <td valign="top" align="left">
                   <div class="button">
                <a href="#">
                <asp:LinkButton ID="btnSubmit" runat="server" Width="85px" Text="Edit Profile" 
                           onclick="btnSubmit_Click" ></asp:LinkButton>
                </a><span></span></div>
                </td>
            </tr>
 

        </table>
    </center>
</asp:Content>
