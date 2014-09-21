<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="MariottPromotion.aspx.cs" Inherits="MariottPromotion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<style type="text/css">
        .style1
        {
            height: 33px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="position: relative; width="100%">
                <tr>
                    <td>
                        <asp:Image ID="Image1" runat="server" ImageUrl="~/images/Marriott.jpg" Width="70px"
                            Height="50px" />&nbsp &nbsp &nbsp<b style="font-size: medium">Marriott Promotion</b>
                    </td>
                </tr>
               
            </table>
            <table style="position: relative; top: 0px; left: 0px; height: 161px;"100%">
             <tr>
                <%--<td>&nbsp&nbsp&nbsp&nbsp&nbsp
                <asp:Image ID="ImgAirtelOffer" runat="server" ImageUrl="~/images/AirtelOffer.jpg" />
                </td>--%>
                </tr>
              
                <tr>
                    <td>
                        <b><font color="red">
                        <br />
                        <br />
                            <asp:Label ID="lblMsgPromotionCode" runat="server" Text="Label"></asp:Label></font><b>
                    </td>
                </tr>
                <tr>
              
                    <td class="style1">
                <br />
                <br />
                        <b>Enter Your Promotion Code Below</b> &nbsp&nbsp<b>:</b>
                    </td>
                </tr>
              
                <tr>
                    <td>
                    <br />
                   
                        <asp:TextBox ID="txtPromotionCode" Width="160px" runat="server" CssClass="text-small"></asp:TextBox>
                        <br>
                        <br>
                       
                       
                        <asp:Button ID="btnPromotionCode" runat="server" CssClass="common-button" 
                             Text="Submit" onclick="btnPromotionCode_Click" />
                        <br>
                        <br></br>
                        <br>
                        <br></br>
                        <br></br>
                        <br></br>
                        </br>
                        </br>
                        </br>
                        </br>
                        
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

