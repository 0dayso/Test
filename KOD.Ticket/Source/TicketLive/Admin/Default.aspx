<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" MasterPageFile="~/Receipt.master" Inherits="Admin_Default" %>

<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" runat="Server">
    <table style="height: 555; width: 99%; border: none;" cellspacing="0" cellpadding="0"
        align="center">
        <tr>
            <td valign="top">
                <!--Login Table -->
                <table width="300" border="0" align="center" cellpadding="1" cellspacing="0" class="outertable">
                    <tr class="outertable">
                        <td height="28" colspan="2" align="center" class="bodybold1">
                            Administration Login
                        </td>
                    </tr>
                    <tr>
                        <td class="tabledata">
                            <asp:Login ID="Login" DisplayRememberMe="false"   runat="server" OnAuthenticate="Login_Authenticate">
                                <LayoutTemplate>
                                    <table border="0" cellpadding="1" cellspacing="0" 
                                        style="border-collapse:collapse;">
                                        <tr>
                                            <td>
                                                <table border="0" cellpadding="0">
                                                    <tr>
                                                        <td align="center" colspan="2">
                                                            &nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">User 
                                                            Name:</asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="UserName" runat="server"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" 
                                                                ControlToValidate="UserName" ErrorMessage="User Name is required." 
                                                                ToolTip="User Name is required." ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>  
                                                     <td align="center" colspan="2"> &nbsp;</td></tr>
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Password:</asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="Password" runat="server" TextMode="Password"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" 
                                                                ControlToValidate="Password" ErrorMessage="Password is required." 
                                                                ToolTip="Password is required." ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <asp:CheckBox ID="RememberMe" runat="server" Text="Remember me next time." />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center" colspan="2" style="color:Red;">
                                                            <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" colspan="2">
                                                            <asp:Button ID="LoginButton" runat="server" CommandName="Login" Text="Log In" 
                                                                ValidationGroup="Login1" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </LayoutTemplate>
                            </asp:Login>
                            <asp:Label ID="ErrorMessage" ForeColor="red" runat="server" />
                            <asp:Label ID="Message" runat="server" />
                        </td>
                    </tr>
                </table>
                <!--End Login Table -->
            </td>
        </tr>
    </table>
</asp:Content>