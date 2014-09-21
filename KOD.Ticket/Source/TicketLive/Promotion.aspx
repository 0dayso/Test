<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true"
    CodeFile="Promotion.aspx.cs" Inherits="Promotion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style1
        {
            height: 33px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="position: relative; width="100%">
                <tr>
                    <td>
                        <asp:Image ID="Image1" runat="server" ImageUrl="~/images/airtel-logo.jpg" Width="50"
                            Height="40" />&nbsp &nbsp &nbsp<b style="font-size: medium">AirTel Promotion</b>
                    </td>
                </tr>
               
            </table>
            <table style="position: relative; width="100%">
             <tr>
                <td>&nbsp&nbsp&nbsp&nbsp&nbsp
                <asp:Image ID="ImgAirtelOffer" runat="server" ImageUrl="~/images/AirtelOffer.jpg" />
                </td>
                </tr>
                <tr>
                    <td>
                        <b><font color="red">
                            <asp:Label ID="lblMsgPromotionCode" runat="server" Text="Label"></asp:Label></font><b>
                    </td>
                </tr>
                <tr>
                    <td class="style1">
                        <b>Enter Your Promotion Code Below</b> &nbsp&nbsp<b>:</b>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="txtPromotionCode" Width="160px" runat="server" CssClass="text-small"></asp:TextBox>
                        <br></br>
                        
                        <asp:Button ID="btnPromotionCode" CssClass="common-button" runat="server" Text="Submit"
                            OnClick="btnPromotionCode_Click" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
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
