<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs"
    Inherits="RoyalWebApp.Account.ChangePassword" MasterPageFile="~/Skins/Master/AccountMaster.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHPageHeading" runat="server">
   <div style="color:Black"> Change Password </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHPageData" runat="Server">
    <script language="javascript" type="text/javascript">
    function validate() 
    {           
                    
                   if (<%Response.Write(txtOldPassword.ClientID);%>.value == "") 
                    {
                        alert("Please enter old Password");
                       <%Response.Write(txtOldPassword.ClientID);%>.focus();
                        return false;
                    }
                     if (<%Response.Write(txtNewPassword.ClientID);%>.value == "") 
                    {
                        alert("Please enter New Password");
                       <%Response.Write(txtNewPassword.ClientID);%>.focus();
                        return false;
                    }
                    if ((<%Response.Write(txtNewPassword.ClientID);%>.value) != (<%Response.Write(txtConfirmPassword.ClientID);%>.value)) 
                    {
                        alert("Password should be same");
                       <%Response.Write(txtNewPassword.ClientID);%>.focus();
                        return false;
                    }

       }
    </script>
 
    <center>
        <table width="60%" cellpadding="3" cellspacing="3" class="divborder">
            <tr>
                <td colspan="2" valign="top" align="center">
                    <asp:Label ID="LblMsg" runat="server" CssClass="divtext" ForeColor="Red"></asp:Label>
                </td>
            </tr>
            <tr>
                <td valign="top" align="left" class="divtext" width="150px">
                    Membership Id:
                </td>
                <td valign="top" align="left">
                    <asp:Label ID="LblMembershipId" runat="server" CssClass="divtext"></asp:Label>
                </td>
            </tr>
            <tr>
                <td valign="top" align="left" class="divtext" width="150px">
                    Old Password:
                </td>
                <td valign="top" align="left">
                    <asp:TextBox ID="txtOldPassword" runat="server" Width="200px" TextMode="Password"
                        CssClass="inputbg"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td valign="top" align="left" class="divtext" width="150px">
                    New Password:
                </td>
                <td valign="top" align="left">
                    <asp:TextBox ID="txtNewPassword" runat="server" Width="200px" TextMode="Password"
                        CssClass="inputbg"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td valign="top" align="left" class="divtext" width="150px">
                    Confirm Password:
                </td>
                <td valign="top" align="left">
                    <asp:TextBox ID="txtConfirmPassword" runat="server" Width="200px" TextMode="Password"
                        CssClass="inputbg"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td valign="top" align="left" class="divtext" width="150px">
                </td>
                <td valign="top" align="left">
                    <div class="button">
                        <a href="#">
                            <asp:LinkButton ID="BtnSubmit" runat="server" OnClick="BtnSubmit_Click" Width="45px"
                                Text="Submit" OnClientClick="javascript:return validate()"></asp:LinkButton>
                        </a><span></span>
                    </div>
                </td>
            </tr>
        </table>
    </center>
</asp:Content>
