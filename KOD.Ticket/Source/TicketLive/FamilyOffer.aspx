<%@ Page Title="" Language="C#" MasterPageFile="~/Event.master" AutoEventWireup="true" CodeFile="FamilyOffer.aspx.cs" Inherits="HotelsPromotion" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

   <script type="text/javascript">

       var _gaq = _gaq || [];
       _gaq.push(['_setAccount', 'UA-35374139-1']);
       _gaq.push(['_setDomainName', 'kingdomofdreams.in']);
       _gaq.push(['_trackPageview']);

       (function () {
           var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;
           ga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') + '.google-analytics.com/ga.js';
           var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);
       })();

</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <div class="wrapper">

 <div class="seats-main_02">
            <div class="seats-inside">
                <div class="ticket-main">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="position: relative;" width="100%">
            
                <tr>
                    <td align="center">
                        <b style="font-size: medium">Family Offer</b>
                    </td>
                </tr>
               
            </table>
            <br /><br />
            <table style="position: relative;" width="100%">
            <asp:UpdateProgress ID="uppero" runat="server" DynamicLayout="true">
                        <ProgressTemplate>
                            <div style="height: 300px; background-color: #C79EA7; width: 280px; position: absolute;
                                top: 265px; left: 535px; border-left: 4px solid #A67782; border-right: 4px solid #A67782;
                                color: #FFC419; filter: alpha(opacity=80); opacity: 0.8; text-align: center;
                                z-index: 100">
                                <div style="font-family: Verdana; font-size: 12px; color: Black; font-weight: bold;
                                    margin-top: 95px">
                                    Loading Please Wait...
                                </div>
                            </div>
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                <tr>
                    <td colspan="6">
                        Please Enter Your Royal Card No. OR Mobile No.&nbsp:
                    </td>
                    </tr>
                    <tr>
                    <td colspan="6">
                        <asp:TextBox ID="textroyalinfo" runat="server"></asp:TextBox>
            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2"  FilterType="Custom,UppercaseLetters,LowercaseLetters,Numbers" ValidChars="-"   TargetControlID="textroyalinfo"
                     runat="server">
                </cc1:FilteredTextBoxExtender></td></tr>
                            <tr>
                <td colspan="6">Package Type:</td></tr>
                <tr>
                <td colspan="6">
                <asp:DropDownList ID="ddl_package" runat="server" Width="150">
                <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                <asp:ListItem Value="1" Text="Weekday-Rs.9796"></asp:ListItem>
                 <asp:ListItem Value="2" Text="Weekend-Rs.13196"></asp:ListItem>
                </asp:DropDownList>
                </td>
                </tr>
                <tr> 
                <td colspan="6" >No. of Package:</td></tr>
                <tr>
                    <td colspan="6" >
                         <asp:DropDownList ID="ddl_noofpackage" runat="server" Width="83px">
                         <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                         <asp:ListItem Value="1" Text="1"></asp:ListItem>
                         <asp:ListItem Value="2" Text="2"></asp:ListItem>
                          <asp:ListItem Value="3" Text="3"></asp:ListItem>
                         <asp:ListItem Value="4" Text="4"></asp:ListItem>
                          <asp:ListItem Value="5" Text="5"></asp:ListItem>
                        </asp:DropDownList>  
                    </td>
                </tr>
                 <tr>
                    <td colspan="5" >
                     <font color="red">
                     <asp:Label ID="lblMsgPromotionCode" runat="server" Text="Label"></asp:Label></font>
                    </td>
                </tr>
                <tr><td></td></tr>
                <tr>
                <td colspan="2">
                <br />
                        <asp:Button Text="Submit" CssClass="common-button" runat="server" 
            style="float:left; text-transform:none"  ID="btnvalidation" 
            onclick="btnvalidation_Click"  />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    </div>
    </div>
    </div>
    <!-- seats-main ends here -->
    </div>
    <!-- wrapper ends here -->
</asp:Content>

