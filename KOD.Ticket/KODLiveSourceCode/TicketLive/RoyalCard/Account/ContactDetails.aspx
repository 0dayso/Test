<%@ Page Title="" Language="C#" MasterPageFile="~/RoyalCard/Skins/Master/AccountMaster.Master" AutoEventWireup="true" CodeFile="ContactDetails.aspx.cs" Inherits="Royal_Card_Account_ContactDetails" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script type="text/javascript">
    function validUpdate() {
        var chkemail = document.getElementById("<%=emailsnd.ClientID %>");
        var OpttxtEmail = document.getElementById("<%=txtEmail.ClientID %>");
        var txtEmail = document.getElementById("<%=txtEmailAddress.ClientID %>");

        if (chkemail.checked == true && OpttxtEmail.value=="") {
            alert("Please enter your Optional Email ID");
            OpttxtEmail.focus();
            return false;
        }
        if (txtEmail.value=="") {
            alert("Please enter your Optional Email ID");
            txtEmail.focus();
            return false;
        }
//        if((<%Response.Write(txtmobileno.ClientID);%>.value).length<10)
//                {
//                alert("Mobile no should be minimum 10 digits");
//                <%Response.Write(txtmobileno.ClientID);%>.focus();
//                return false;
//                }
                  if((<%Response.Write(txtmobileno.ClientID);%>.value).length>10)
                {
                alert("Mobile Number Should be of 10 digits only");
                <%Response.Write(txtmobileno.ClientID);%>.focus();
                return false;
                }
    }
</script>
<style type="text/css">
        .style3
        {
            font-family: "Trebuchet MS", Arial, Helvetica, sans-serif;
            font-size: 12px;
            color: #3f260a;
            text-decoration: none;
            margin-left: 14px;
            width: 98px;
        }
        .text-small
{
    font: 12px verdana;
    color: #000000;
}
.clickhere
{
    font: 12px Verdana;
    color: #ECA035;
}
    .style6
    {
        width: 137px;
    }
    .style7
    {
        font-family: "Trebuchet MS", Arial, Helvetica, sans-serif;
        font-size: 12px;
        color: #3f260a;
        text-decoration: none;
        margin-left: 14px;
        }
    .style8
    {
        width: 26px;
    }
    .style9
    {
        width: 169px;
    }
    .style10
    {
        float: center;
        width: 169px;
    }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHPageHeading" Runat="Server">
Contact Details
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHPageData" Runat="Server">
<table>
<tr>
<td class="style6">
</td>
</tr>
<tr>
<td class="style7">
Email Address
</td>
<td class="style8">:</td>
<td class="style9">
                <asp:TextBox ID="txtEmailAddress" runat="server" Width="158px" CssClass="text-small" ReadOnly="true" Text=""></asp:TextBox>
                <asp:RequiredFieldValidator ID="re3" ControlToValidate="txtEmailAddress" ValidationGroup="cont"
                    ErrorMessage="Email Address" Display="None" ForeColor="#3f260a" runat="server" />
                <asp:RegularExpressionValidator ID="rg1" ErrorMessage="Valid Email" ControlToValidate="txtEmailAddress"
                    ValidationGroup="cont" Display="None" ForeColor="#3f260a" runat="server" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" />
   <th align="left"> <asp:CheckBox ID="emailsnd" runat="server" Text="Don't send Email" CssClass="style7" 
                    Width="133px"/></th>
            </td>
</tr>

<tr>
<td class="style7">
Email Address(Optional)
</td>
<td class="style8">:</td>
<td class="style9">
                <asp:TextBox ID="txtEmail" runat="server" Width="158px" CssClass="text-small"></asp:TextBox>
                <asp:RegularExpressionValidator ID="re7" ErrorMessage="Valid Email" ControlToValidate="txtEmail"
                    ValidationGroup="cont" Display="None" ForeColor="#3f260a" runat="server" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" />
            </td>
</tr>

<tr>
<td class="style7">
Contact No.
</td>
<td class="style8">:</td>
<td class="style9">
    <asp:TextBox ID="txtISDCode" Text="+91" Width="32px" runat="server" CssClass="text-small"></asp:TextBox>
    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" FilterType="Custom, Numbers" ValidChars="+" TargetControlID="txtISDCode"
                    runat="server">
                </cc1:FilteredTextBoxExtender>
    <asp:TextBox ID="txtContactNo" Width="122px" runat="server" 
        CssClass="text-small" ReadOnly="True" Text=""></asp:TextBox>
        <cc1:FilteredTextBoxExtender ID="F1" FilterType="Numbers" TargetControlID="txtContactNo"
                    runat="server">
                </cc1:FilteredTextBoxExtender>
                <asp:RequiredFieldValidator ID="re2" Text="required!" ControlToValidate="txtContactNo"
                    ValidationGroup="cont" ErrorMessage="Contact No" Display="None" ForeColor="#3f260a"
                    runat="server" />
                    <th align="left">
                    <asp:CheckBox ID="mobilesnd" runat="server" 
        Text="Don't send Message" CssClass="style7" 
                    Width="133px"/></th>
</td>
</tr>
<tr>
<td class="style7">
Mobile No.(Optional)
</td>
<td class="style8">:</td>
<td class="style9">
    <asp:TextBox ID="txtisd" Text="+91" Width="32px" runat="server" CssClass="text-small"></asp:TextBox>
    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" FilterType="Custom, Numbers" ValidChars="+" TargetControlID="txtisd"
                    runat="server">
                </cc1:FilteredTextBoxExtender>
    <asp:TextBox ID="txtmobileno" Width="122px" runat="server" 
        CssClass="text-small"></asp:TextBox>
        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" FilterType="Numbers" TargetControlID="txtmobileno"
                    runat="server">
                </cc1:FilteredTextBoxExtender>
                
</td>
</tr>

<tr>
<td class="style7">
<asp:Label ID="lblMode" runat="server" Text="Payment Mode"></asp:Label>
</td>
<td class="style8"><asp:Label ID="lblColon" runat="server" Text=":"></asp:Label></td>
<td class="style9">
                <asp:DropDownList ID="ddlPaymentMode" onchange="Change_Paytype()" runat="server" Width="162px" CssClass="text-small">
                    <asp:ListItem Value="MODE" Text="Select Payment Mode"></asp:ListItem>
                    <asp:ListItem Value="CREDIT" Text="Credit/Debit Card"></asp:ListItem>
               </asp:DropDownList>
                 <asp:RequiredFieldValidator ID="re4" Text="required!" ControlToValidate="ddlPaymentMode"
                    ValidationGroup="cont" ErrorMessage="Payment" InitialValue="MODE" Display="None" ForeColor="#3f260a"
                    runat="server" />
            </td>
</tr>
<tr>
            <td valign="top" align="left" class="style7">
                <div id="tr_CardType" style="display: none" >
                    Card Type :</div>
            </td>
           <td class="style8"></td>
            
            <td class="style9">
            
                <div id="tr_CardType1" style="display: none">
                    <asp:RadioButtonList ID="rbl_CardType" runat="server" Width="172px" CssClass="text-small">
                        <asp:ListItem Value="AMEX" Text="American Express" Selected="True"></asp:ListItem>
                        <asp:ListItem Value="IDBI" Text="IDBI(MASTER/VISA)" />
                        <asp:ListItem Value="HDFC" Text="HDFC(MASTER/VISA)"></asp:ListItem>
                        <asp:ListItem Value="IDBI" Text="OTHERS(MASTER/VISA)"/>
                    </asp:RadioButtonList>
                </div>
            </td>
        </tr>
        <tr>
            <td class="style6">
                &nbsp;
            </td>
        </tr>
         <tr>
            <td colspan="2" align="center" class="style3">
                <asp:Label ID="lblMess" runat="server" CssClass="error"></asp:Label>
            </td>
        </tr>
        <tr>
        <th class="style6">
        <div class="button" style="float:right;">
                    <asp:LinkButton ID="btnBackHome" runat="server" 
                        OnClientClick="javascript:return validUpdate();" Width="97px" 
                        Text="Back To Home" onclick="btnBackHome_Click" ></asp:LinkButton>
              <span></span>
    </div>
        </th>
        <th class="style8"></th>
        <th class="style10">
        <div class="button">
                    <asp:LinkButton ID="btnSubmit" runat="server" 
                        OnClientClick="javascript:return validUpdate();" Width="97px" 
                        ValidationGroup="cont" Text="Proceed" onclick="btnSubmit_Click" ></asp:LinkButton>
              <span></span>
    </div>
        </th>
        <td><asp:ValidationSummary ID="ValidationSummary1" HeaderText="Some Fields were missing..."
                    ShowMessageBox="true" DisplayMode="List" ValidationGroup="cont" ShowSummary="false"
                    runat="server"  class="style3"/></td>
        </tr>
         
</table>

<script type="text/javascript">
    function Change_Paytype() {
        var val = document.getElementById("<%=ddlPaymentMode.ClientID %>").value;
        if (val == "CREDIT") {
            document.getElementById("tr_CardType").style.display = "inline";
            document.getElementById("tr_CardType1").style.display = "inline";
        }
        else {
            document.getElementById("tr_CardType").style.display = "none";
            document.getElementById("tr_CardType1").style.display = "none";
        }
    }
    </script>
</asp:Content>

