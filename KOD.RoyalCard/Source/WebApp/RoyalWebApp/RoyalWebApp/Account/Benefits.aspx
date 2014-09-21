<%@ Page Language="C#" MasterPageFile="~/Skins/Master/AccountMaster.Master" AutoEventWireup="true"
    CodeBehind="Benefits.aspx.cs" Inherits="RoyalWebApp.Account.Benefits" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style1
        {
            border: #8e4d18 1px solid;
            width: 285px;
        }
        .style4
        {
            border: #8e4d18 1px solid;
            width: 270px;
        }
        .style6
        {
            border: #8e4d18 1px solid;
            width: 369px;
        }
        .style7
        {
            border: #8e4d18 1px solid;
            width: 306px;
        }
        .style8
        {
            border: #8e4d18 1px solid;
            width: 302px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHPageHeading" runat="server">
    <div style="color:Black"> Membership Benefits Grid </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHPageData" runat="server">
    <!-- show pop up -->
    <script language="javascript" type="text/javascript">
        function showalert() {
            alert("These vouchers should be declared at the Box Office in Kingdom Of Dreams before booking Nautanki Mahal tickets. Your Royal Card will entitle you for tickets to a level higher than the one you pay for");
        }     
    </script>
    <div class="home-content-left">
        <%-- <h3 align="center" style="padding:0px; margin:10px 0px 0px 0px;">&nbsp;</h3>--%>
        <div style="margin-left: -26px; *margin-left: -26px; margin-bottom: 5px; *margin-bottom: 5px;">
            <img src="../Skins/images/ruller.png" width="764" height="20" /></div>
        <table>
            <tr>
                <td valign="top" colspan="8" width="710">
                    <div id="section" class="scroll-pane" style="height: 260px;">
                        <table border="0" cellspacing="3" cellpadding="3" style="font-family: Arial, Helvetica, sans-serif;
                            font-size: 11px; width: 649px; margin-right: 0px; color:Black">
                            <tr>
                                <td valign="top" width="89" class="tableBG tableHeading">
                                    Benefits
                                </td>
                                <td valign="top" width="73" class="tableBG tableHeading">
                                    <a href="PurpleCard.aspx">Purple Card</a>
                                </td>
                                <td valign="top" width="65" class="tableBG tableHeading">
                                    <a href="BlueCard.aspx">Blue Card</a>
                                </td>
                                <td valign="top" width="86" class="tableBG tableHeading">
                                    <a href="PlatinumCard.aspx">Platinum Card</a>
                                </td>
                                <td valign="top" width="83" class="tableBG tableHeading">
                                    <a href="TitaniumCard.aspx">Titanium Card</a>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" class="style4">
                                    Upgrade Criteria
                                </td>
                                <td valign="top" class="style6" >
                                  N/A
                                </td>
                                <td valign="top" class="style7">
                                    Upgrade On<br />
                                    Rs. 25,000
                                </td>
                                <td valign="top" class="style8">
                                    Upgrade On
                                    <br />
                                    Rs. 50,000
                                </td>
                                <td valign="top" class="style1">
                                    Upgrade On
                                    <br />
                                    Rs. 100,000
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" class="style4">
                                    Points Earned on Every Rs.100
                                </td>
                                <td valign="top" class="style6">
                                    7.5%
                                </td>
                                <td valign="top" class="style7">
                                    10%
                                </td>
                                <td valign="top" class="style8">
                                    12.5%
                                </td>
                                <td valign="top" class="style1">
                                    15%
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" class="style4">
                                    Milestone Vouchers 
                                </td>
                                <td valign="top" class="style6" >
                                     N/A
                                </td>
                                <td valign="top" class="style7">
                                    600
                                </td>
                                <td valign="top" class="style8">
                                    1,000
                                </td>
                                <td valign="top" class="style1">
                                    2,500
                                </td>
                            </tr>
                            <%--<tr>
                                <td valign="top" class="style4" title="Valid for 30 Days">
                                 Gift points earned on Birthday
                                </td>
                                <td valign="top" class="style6">
                                    200
                                </td>
                                <td valign="top" class="style7">
                                    300
                                </td>
                                <td valign="top" class="style8">
                                    500
                                </td>
                                <td valign="top" class="style1">
                                    1000
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" class="style4" title="Valid for 30 Days">
                                    Gift points earned on Anniversary
                                </td>
                                <td valign="top" class="style6">
                                    200
                                </td>
                                <td valign="top" class="style7">
                                    300
                                </td>
                                <td valign="top" class="style8">
                                    500
                                </td>
                                <td valign="top" class="style1">
                                    1000
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" class="style4" title="Tickets are used to watch live theater that Kingdom of Dreams stages in Nautanki Mahal.">
                                  Free Nautanki Mahal Tickets
                                </td>
                                <td valign="top" class="style6" >
                                    N/A
                                </td>
                                <td valign="top" class="style7" >
                                    N/A
                                </td>
                                <td valign="top" class="style8">
                                    2 Platinum Tickets
                                </td>
                                <td valign="top" class="style1">
                                    4 Platinum Tickets
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" class="style4" title="Upgrade allows the member to purchase ticket against the price of immediate lower category ticket.(Not applicable on the Lowest & Higest category)">
                                   Free upgrade on Nautanki Mahal tickets
                                </td>
                                <td valign="top" class="style6">
                                    2
                                </td>
                                <td valign="top" class="style7">
                                    4
                                </td>
                                <td valign="top" class="style8">
                                    6
                                </td>
                                <td valign="top" class="style1">
                                    8
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" class="style4" title="It's replica of the primary card which is linked to the primary membership card account.">
                                    Free add-on card
                                </td>
                                <td valign="top" class="style6">
                                    1
                                </td>
                                <td valign="top" class="style7">
                                    1
                                </td>
                                <td valign="top" class="style8">
                                    1
                                </td>
                                <td valign="top" class="style1">
                                    1
                                </td>
                            </tr>--%>
                            <tr>
                                <td valign="top" class="style4">
                                    Maximum Admits Per Card 
                                </td>
                                <td valign="top" class="style6">
                                    4
                                </td>
                                <td valign="top" class="style7">
                                    6
                                </td>
                                <td valign="top" class="style8">
                                    8
                                </td>
                                <td valign="top" class="style1">
                                    10
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" class="style4">
                                    Free Entry to KOD (Culture Gully)
                                </td>
                                <td valign="top" class="style6">
                                    Yes
                                </td>
                                <td valign="top" class="style7">
                                    Yes
                                </td>
                                <td valign="top" class="style8">
                                    Yes
                                </td>
                                <td valign="top" class="style1">
                                    Yes
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" class="style4">
                                 Birthday Wish Points 
                                </td>
                                <td valign="top" class="style6">
                                    200
                                </td>
                                <td valign="top" class="style7">
                                    300
                                </td>
                                <td valign="top" class="style8">
                                    500
                                </td>
                                <td valign="top" class="style1">
                                    1000
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" class="style4">
                                 Anniversary Wish Points  
                                </td>
                                <td valign="top" class="style6">
                                    200
                                </td>
                                <td valign="top" class="style7">
                                    300
                                </td>
                                <td valign="top" class="style8">
                                    500
                                </td>
                                <td valign="top" class="style1">
                                    1000
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" class="style4">
                                   Upgrade Vouchers 
                                </td>
                                <td valign="top" class="style6">
                                    2
                                </td>
                                <td valign="top" class="style7">
                                    4
                                </td>
                                <td valign="top" class="style8">
                                    6
                                </td>
                                <td valign="top" class="style1">
                                    8
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" class="style4">
                                   Mandatory Spend 
                                </td>
                                <td valign="top" class="style6">
                                   Rs. 299
                                </td>
                                <td valign="top" class="style7">
                                    NIL
                                </td>
                                <td valign="top" class="style8">
                                    NIL
                                </td>
                                <td valign="top" class="style1">
                                    NIL
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" class="style4" title="Tickets are used to watch live theater that Kingdom of Dreams stages in Nautanki Mahal.">
                                  Free Tickets to Nautanki Mahal 
                                </td>
                                <td valign="top" class="style6" >
                                    N/A
                                </td>
                                <td valign="top" class="style7" >
                                    N/A
                                </td>
                                <td valign="top" class="style8">
                                    2 Platinum Tickets
                                </td>
                                <td valign="top" class="style1">
                                    4 Platinum Tickets
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" class="style4" title="It's replica of the primary card which is linked to the primary membership card account.">
                                    Add-on Cards
                                </td>
                                <td valign="top" class="style6">
                                    1
                                </td>
                                <td valign="top" class="style7">
                                    1
                                </td>
                                <td valign="top" class="style8">
                                    1
                                </td>
                                <td valign="top" class="style1">
                                    1
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" class="style4">
                                    Free valet parking
                                </td>
                                <td valign="top" class="style6" >
                                   N/A
                                </td>
                                <td valign="top" class="style7" >
                                    N/A
                                </td>
                                <td valign="top" class="style8">
                                    Yes
                                </td>
                                <td valign="top" class="style1">
                                    Yes
                                </td>
                            </tr>
                            
                            <%--<tr>
                                <td valign="top" valign="top" class="style4">
                                    Privilege Entry
                                </td>
                                <td valign="top" class="style6">
                                   Yes
                                </td>
                                <td valign="top" class="style7">
                                   Yes
                                </td>
                                <td valign="top" class="style8">
                                    Yes
                                </td>
                                <td valign="top" class="style1">
                                    Yes
                                </td>
                            </tr>--%>
                            
                            <tr>
                                <td valign="top" class="style4">
                                    Balance Carry Forward
                                </td>
                                <td valign="top" class="style6">
                                    Yes
                                </td>
                                <td valign="top" class="style7">
                                    Yes
                                </td>
                                <td valign="top" class="style8">
                                    Yes
                                </td>
                                <td valign="top" class="style1">
                                    Yes
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" class="style4">
                                 Alert(sms, email, call)
                                </td>
                                <td valign="top" class="style6">
                                    Yes
                                </td>
                                <td valign="top" class="style7">
                                    Yes
                                </td>
                                <td valign="top" class="style8">
                                    Yes
                                </td>
                                <td valign="top" class="style1">
                                    Yes
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <div style="margin-left: -26px; margin-top: 5px; *margin-left: -26px; *margin-top: 5px;">
        <img src="../Skins/images/ruller.png" width="764" height="20" /></div>
</asp:Content>
