<%@ Page Language="C#" MasterPageFile="~/Skins/Master/AccountMaster.Master" AutoEventWireup="true"
    CodeBehind="ForgotPassword.aspx.cs" Inherits="RoyalWebApp.Account.ForgotPassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style1
        {
            font-family: "Trebuchet MS", Arial, Helvetica, sans-serif;
            font-size: 12px;
            color: #3f260a;
            text-decoration: none;
            width: 320px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHPageHeading" runat="server">
    Forgot Password
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHPageData" runat="server">
<script language="javascript" type="text/javascript">
    function WaterMark(txtmembershipId, event) {
        var defaultText = "RCM-XXXXXXX";
        // Condition to check textbox length and event type
        if (txtmembershipId.value.length == 0 & event.type == "blur") {
            //if condition true then setting text color and default text in textbox
            txtmembershipId.style.color = "Black";
            txtmembershipId.value = defaultText;
        }
        // Condition to check textbox value and event type
        if (txtmembershipId.value == defaultText & event.type == "focus") {
            txtmembershipId.style.color = "black";
            txtmembershipId.value = "";
        }
    }
</script>
    <script language="javascript" type="text/javascript">
    function validate() 
    {           
                    
                         if($("[id*=txtmembershipId]").val()=="")
                            {
                               alert("Please enter Membership ID");
                               $("[id*=txtmembershipId]").focus();
                                   return false;
                            }
                         if($("[id*=txtmembershipId]").val()=="RCM-XXXXXXX")
                            {
                               alert("Please enter Valid Membership ID");
                               $("[id*=txtmembershipId]").focus();
                                   return false;
                            }
                         if ($("[id*=txtEmailId]").val()== "") 
                            {
                               alert("Please enter Email Id");
                               $("[id*=txtEmailId]").focus();
                                return false;
                              }
                            
                    //var emailPat = /^([a-zA-Z0-9_.+-])+@(([a-zA-Z0-9-])+.)+([a-zA-Z0-9]{2,6})+$/;
                    var emailPat = /^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*/;

                      var emailid = $("[id*=txtEmailId]").val();
                      var matchArray = emailid.match(emailPat);
                      if (matchArray == null) {
                          alert("Please Enter Valid Email ID");
                          $("[id*=txtEmailId]").focus();
                          return false;
                      }                      

       }
    </script>
    <center>
        <table width="100%" cellpadding="3" cellspacing="3" class="divborder">
            <tr>
                <td colspan="2" valign="top" align="left" style="height:50px">
                <asp:Label ID="LblMsg" runat="server" Font-Bold="true" Text="" ForeColor="Red"></asp:Label>  
                                   
                </td>
            </tr>
            <tr >
                <td valign="top" align="right" class="style1">
                    Membership Id:
                </td>
                <td valign="top" align="left">
                    <asp:TextBox ID="txtmembershipId" runat="server" Width="200px"  CssClass="inputbg" input="text" Text="RCM-XXXXXXX" onFocus="WaterMark(this, event);" onBlur="WaterMark(this, event);"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td valign="top" align="Right" class="style1">
                    Email Id
                    <br />
                    (as per our records)</td>
                <td valign="top" align="left">
                    <asp:TextBox ID="txtEmailId" runat="server" Width="200px" CssClass="inputbg"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td valign="top" align="left" class="style1">
                </td>
                <td valign="top" align="left">
                <div class="button">
                        <a href="#">
                            <asp:LinkButton ID="BtnSubmit" runat="server" Width="45px"
                                Text="Submit" OnClientClick="javascript:return validate()" 
                            onclick="BtnSubmit_Click"></asp:LinkButton>
                        </a><span></span>
                    </div>
                </td>
            </tr>
        </table>
        </center>
</asp:Content>
