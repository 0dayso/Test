<%@ Page Title="" Language="C#" MasterPageFile="~/Skins/Master/AccountMaster.Master" AutoEventWireup="true" CodeBehind="ContactDetails.aspx.cs" Inherits="ROYALCARD.Account.ContactDetails" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<script runat="server">
protected void BtnBackHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("TicketBooking.aspx");
        }

protected void BtnSubmit_Click(object sender, EventArgs e)
{
    Response.Redirect("Print-Receipt.aspx");
}
</script>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHPageHeading" runat="server">
    Contact Details
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHPageData" runat="server">
    <table>
<tr>
<td class="style6">
</td>
</tr>
<tr>
<td class="style7">
Email Address
</td>
<td>:</td>
<td>
                <asp:TextBox ID="txtEmailAddress" runat="server" Width="158px" CssClass="text-small" ReadOnly="true" Text="abc@abc.com"></asp:TextBox>
                <asp:RequiredFieldValidator ID="re3" ControlToValidate="txtEmailAddress" ValidationGroup="cont"
                    ErrorMessage="Email Address" Display="None" ForeColor="#3f260a" runat="server" />
                <asp:RegularExpressionValidator ID="rg1" ErrorMessage="Valid Email" ControlToValidate="txtEmailAddress"
                    ValidationGroup="cont" Display="None" ForeColor="#3f260a" runat="server" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" />
    <asp:CheckBox ID="emailsnd" runat="server" Text="Don't send Email" CssClass="style7" 
                    Width="116px"/>
            </td>
</tr>

<tr>
<td class="style7">
Email Address(Optional)
</td>
<td>:</td>
<td>
                <asp:TextBox ID="TextBox1" runat="server" Width="158px" CssClass="text-small"></asp:TextBox>
                <asp:RequiredFieldValidator ID="re6" ControlToValidate="txtEmailAddress" ValidationGroup="cont"
                    ErrorMessage="Email Address" Display="None" ForeColor="#3f260a" runat="server" />
                <asp:RegularExpressionValidator ID="re7" ErrorMessage="Valid Email" ControlToValidate="txtEmailAddress"
                    ValidationGroup="cont" Display="None" ForeColor="#3f260a" runat="server" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" />
            </td>
</tr>

<tr>
<td class="style7">
Contact No.
</td>
<td>:</td>
<td>
    <asp:TextBox ID="txtISDCode" Text="+91" Width="32px" runat="server" CssClass="text-small"></asp:TextBox>
    <asp:TextBox ID="txtContactNo" Width="122px" runat="server" 
        CssClass="text-small" ReadOnly="True" Text="9999999999"></asp:TextBox>
        <cc1:FilteredTextBoxExtender ID="F1" FilterType="Numbers" TargetControlID="txtContactNo"
                    runat="server">
                </cc1:FilteredTextBoxExtender>
                <asp:RequiredFieldValidator ID="re2" Text="required!" ControlToValidate="txtContactNo"
                    ValidationGroup="cont" ErrorMessage="Contact No" Display="None" ForeColor="#3f260a"
                    runat="server" />
                    <asp:CheckBox ID="mobilesnd" runat="server" 
        Text="Don't send Message" CssClass="style7" 
                    Width="133px"/>
</td>
</tr>
<tr>
<td class="style7">
Mobile No.(Optional)
</td>
<td>:</td>
<td>
    <asp:TextBox ID="txtisd" Text="+91" Width="32px" runat="server" CssClass="text-small"></asp:TextBox>
    <asp:TextBox ID="txtmobileno" Width="122px" runat="server" 
        CssClass="text-small"></asp:TextBox>
        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" FilterType="Numbers" TargetControlID="txtmobileno"
                    runat="server">
                </cc1:FilteredTextBoxExtender>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Text="required!" ControlToValidate="txtmobileno"
                    ValidationGroup="cont" ErrorMessage="Contact No" Display="None" ForeColor="#3f260a"
                    runat="server" />
</td>
</tr>

<tr>
<td class="style7">
Payment Mode
</td>
<td>:</td>
<td>
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
           <td></td>
            
            <td>
            
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
                    <asp:LinkButton ID="BtnBackHome" runat="server" 
                        OnClientClick="javascript:return validUpdate();" Width="97px" 
                        Text="Back To Home" onclick="BtnBackHome_Click"></asp:LinkButton>
              <span></span>
    </div>
        </th>
        <th></th>
        <th>
        <div class="button" style="float:right;">
                    <asp:LinkButton ID="BtnSubmit" runat="server" 
                        OnClientClick="javascript:return validUpdate();" Width="97px" 
                        Text="Proceed" onclick="BtnSubmit_Click"></asp:LinkButton>
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
