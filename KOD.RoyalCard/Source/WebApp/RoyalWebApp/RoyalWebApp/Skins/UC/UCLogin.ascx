<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCLogin.ascx.cs" Inherits="RoyalWebApp.Skins.UC.UCLogin" %>
<script language="javascript" type="text/javascript">
    function validLogin() 
    {           
                if($('[id*=txtMemberId]').val()=="")
                {
                alert("Please enter Membership Id");
                $('[id*=txtMemberId]').focus();
                return false;
                }
                if($('[id*=txtPassword]').val()=="")
                {
                alert("Please enter Password");
                $('[id*=txtPassword]').focus();
                return false;
                } 
       }
</script>
<table width="255" border="0" align="center" cellpadding="0" cellspacing="1" class="divtextlogin">
    <tr>
        <td colspan="2">
            &nbsp;<asp:Label ID="lblMsg" runat="server" Visible="false" CssClass="divtext"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="divtext" style="padding-left: 8px;">
            Member Id:
        </td>
        <td>
            <span class="username">
                <asp:TextBox ID="txtMemberId" runat="server" CssClass="inputBdr" Height="20px" ToolTip="Enter Member Id"
                    Width="150px"></asp:TextBox>
            </span>
        </td>
    </tr>
    <tr>
        <td class="divtext" style="padding-left: 10px;">
            Password:
        </td>
        <td>
            <span class="password">
                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="inputBdr"
                    ToolTip="Enter Password" Text="Password" Height="20px" Width="150px"></asp:TextBox>
            </span>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <table width="90%" border="0" cellspacing="0" cellpadding="0">
                <tr>
                    <td>
                        <img src="../Skins/images/arrow.png" width="11" height="7" />
                    </td>
                    <td class="forgotPassword">
                        <a href="ForgotPassword.aspx" title="Click to get your password">Forgot Password?</a>&nbsp;
                    </td>
                    <td align="right">
                        <asp:ImageButton ID="BtnSubmit" runat="server" ImageUrl="../images/enter01.png" Width="80"
                            Height="30" OnClick="BtnSubmit_Click" OnClientClick="javascript:return validLogin();" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <img src="../Skins/images/arrow.png" width="11" height="7" />
                    </td>
                    <td class="forgotPassword">
                        <a href="FirstTimeLogin.aspx">First Time Login</a>
                    </td>
                </tr>
                <tr>
                    <td>
                        <img src="../Skins/images/arrow.png" width="11" height="7" />
                    </td>
                    <td class="forgotPassword">
                        <a href="MemberShip.aspx">Become a Member</a>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
