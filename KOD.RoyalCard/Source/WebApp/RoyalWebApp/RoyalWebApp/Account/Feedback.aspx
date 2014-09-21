<%@ Page Title="" Language="C#" MasterPageFile="~/Skins/Master/AccountMaster.Master" AutoEventWireup="true" CodeBehind="Feedback.aspx.cs" Inherits="RoyalWebApp.Feedback" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style type="text/css">
.divborder1 {
	font-family:"Trebuchet MS", Arial, Helvetica, sans-serif;	
	color:#3f260a;	
	font-size:12px;
	padding:3px;
	text-align:center;
    width: 95%;
    margin-top:50px;
}
</style>
<script type="text/javascript">
    function Radio_Click() {
        var rdYes = document.getElementById("<%=rdYes.ClientID %>");
        var textBox = document.getElementById("<%=txtMembership.ClientID %>");
        if (rdYes.checked) {
            textBox.disabled=false;
            textBox.focus();
        }
        else if (!rdYes.checked) {
            textBox.disabled = true;
        }
    }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHPageHeading" runat="server">
<div style="color:Black">Feedback</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHPageData" runat="server">
<asp:Label ID="Lblmsg" runat="server" Visible="false" Style="font-weight: bold; color:red;"></asp:Label>
<table id="KODdetails" style="color:black; margin-top:0">
<tr>
<td>
<b>Kingdom of Dreams</b>
</td>
</tr>
<tr>
<td>
<b>Great Indian Nautanki Company Pvt. Ltd.</b>
</td>
</tr>
<tr>
<td>
<b>Auditorium Complex,Sector-29,</b>
</td>
</tr>
<tr>
<td>
<b>Gurgaon 122001, Haryana, India</b>
</td>
</tr>
<tr>
<td>
<b>Phone: 0124-4847435/9654124167</b>
</td>
</tr>
<tr>
<td>
<b>Fax: 0124-4847405</b>
</td>
</tr>
<tr>
<td>
<b>E-Mail: loyalty.programme@kingdomofdreams.co.in</b>
</td>
</tr>
</table>
<div class="button" style="float :left; width: 304px;">
                    <a href="#">
                    <asp:LinkButton ID="btnFeedback" runat="server" Width="160px" 
                     Text="Send us your Feedback" onclick="btnFeedback_Click" ></asp:LinkButton>
                </a><span></span>
</div>
<div id="Inquiryfrm" width="80%" class="divborder1" visible="false" runat="server">
<table> 
<tr>
<td align="left">
<b>Name : *</b>
</td>
<td>
<asp:TextBox ID="txtFirstName" runat="server" CssClass="inputbg" />
</td>
</tr>
<tr>
<td align="left">
<b>Email : *</b>
</td>
<td>
<asp:TextBox ID="txtEmail" runat="server" CssClass="inputbg" />
</td>
</tr>
<tr>
<td align="left">
<b>Mobile No: *</b>
</td>
<td>
<asp:TextBox ID="txtmobile" runat="server" CssClass="inputbg"/>
</td>
</tr>
<tr>
<td align="left">
<b>Membership Status :</b>
</td>
<td>
<asp:RadioButton ID="rdYes" runat="server" Text = "Yes" GroupName = "Radio" onclick = "Radio_Click()" />
<asp:RadioButton ID="rdNo" runat="server" Text = "No" GroupName = "Radio"  Checked="true" onclick = "Radio_Click()"/>
</td>
</tr>
<tr>
<td align="left">
<b>Membership No : </b>
</td>
<td>
<asp:TextBox ID="txtMembership" runat="server" CssClass="inputbg" Enabled="false" />
</td>
</tr>
<tr>
<td align="left">
<b>Topic : </b>
</td>
<td align="right">
<asp:RadioButtonList ID="Rdtopic" runat="server" RepeatDirection="Horizontal" Width="300px">
                        <asp:ListItem Value="Com" Text="Complaints" Selected="True"></asp:ListItem>
                        <asp:ListItem Value="qur" Text="Queries"></asp:ListItem>
                        <asp:ListItem Value="sugg" Text="Suggestion"></asp:ListItem>
                    </asp:RadioButtonList>
</td>
</tr>
<tr>
<td align="left">
 <b>Enter Comments : *</b>
</td>
<td align="right">
<asp:TextBox ID="txtComments"  runat="server" CssClass="inputbg" TextMode="MultiLine" Rows="10" 
        Columns="50"  Height="120px" Width="320px" 
        ></asp:TextBox>
</td>
</tr>
<tr>
<td></td>
<td align="center" colspan="2">
 <div class="button">
                <a href="#">
                <asp:LinkButton ID="BtnSubmit" runat="server"  Width="65px" Text="Submit" 
                 onclick="BtnSubmit_Click"
                   ></asp:LinkButton>
         </a><span></span></div>
</td>
</tr>
</table>            
</div>

</asp:Content>
