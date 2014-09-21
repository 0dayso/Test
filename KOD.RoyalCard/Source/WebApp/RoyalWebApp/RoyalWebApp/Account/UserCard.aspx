<%@ Page  Language="C#" MasterPageFile="~/Skins/Master/AccountMaster.Master" AutoEventWireup="true" CodeBehind="UserCard.aspx.cs" Inherits="RoyalWebApp.Account.UserCard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
        window.setInterval(BlinkIt, 500);
        var color = "Red";
        function BlinkIt() {
            var blink = document.getElementById("blink");
            color = (color == "Red") ? "White" : "Red";
            blink.style.color = color;
        }
    </script>

    <style type="text/css">
        .style8
        {
            color:#fde791;
            font:12px "Palatino Linotype", "Century Gothic", Arial;
            
            font-weight:bold;
           
        }
   .style9
   {
       border: #8e4d18 1px solid;
       font-weight:bold;
	   *line-height:18px;
       line-height:20px;
   }
        
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHPageHeading" runat="server">
   <div style="color:Black"> My Card </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHPageData" runat="server">
    <br /><br />
<div style="float:left; width:310px; ">  
                      <asp:Image ID="ImgCard" runat="server" AlternateText="Card" />
                      <br />
    <div style="color:#8e4d18;height:50px"><b><asp:Label ID="Label1" runat="server" Text="Name of Guest:-"></asp:Label>
     <asp:Label ID="lblName" runat="server" Text="0" ></asp:Label></b><br />
    <b><asp:Label ID="Label4" runat="server" Text="Member ID:-"></asp:Label>
     <asp:Label ID="lblMemberID" runat="server" Text="0"></asp:Label></b><br />
     <b><asp:Label ID="Label2" runat="server" Text="Date on Card:-"></asp:Label>
     <asp:Label ID="lblDate" runat="server" Text="0"></asp:Label></b></div>
</div>
 
    

            <div style="float:right; width:380px;">
                      <div class="home-content-left01">
                <div id="section" class="scroll-pane01">
                <table border="0" width="380" cellspacing="5" cellpadding="2"  class="divborder">
                <tr>
                    <td valign="top" class="tableBG style8">Available Value</td>
                    <td valign="top" class="tableBG style8">Available Points</td>
                    <td valign="top" class="tableBG style8">24 hr Ledger Balance</td>
                   <%-- <td valign="top" class="tableBG style8">Name</td>--%>
                 <%--   <td valign="top" class="tableBG style8">Total Redemption</td>
                    <td valign="top" class="tableBG style8">Need to Spend for  Up-Gradation</td>--%>
                    </tr>
                    <tr>
                    <td class="style9">
                    <asp:Label ID="lblavailableVal" runat="server" Text="0"></asp:Label>
                    </td>
                    <td class="style9">
                    <asp:Label ID="lblAvailablePoints" runat="server" Text="0" ></asp:Label>
                    </td>
                    <td class="style9">
                   <asp:Label ID="lblAfter24hrs" runat="server" Text="0"  ></asp:Label>
                   </td>
                   <%-- <td class="style9">
                    <asp:Label ID="lblName" runat="server" Text="0"  ></asp:Label>
                   </td>--%>

                   
                    <asp:Label ID="lblTotalRedemption" runat="server" Text="0" Visible="false" ></asp:Label>
                    
                    
                    <asp:Label ID="lblUpdateCard" runat="server" Text="0" Visible="false" ></asp:Label>
                   
                    </tr>
                    
                </table>
               
                </div>
                
                          <%--<table width="300" border="0" cellspacing="5" cellpadding="2"  class="divborder" >
                    <tr>
                    <td valign="top" width="80%">--%>
<asp:GridView ID="GridCardList" Width="100%" runat="server" AllowPaging="false" 
                            EmptyDataText="No Card Found" AutoGenerateColumns="false" Visible="false" >
 <Columns>
          <asp:TemplateField HeaderText="Min Value" >
<ItemTemplate>
<%#DataBinder.Eval(Container.DataItem,"MinimumValue") %>
</ItemTemplate>
</asp:TemplateField>
          <asp:TemplateField HeaderText="Max Value" >
<ItemTemplate>
<%#DataBinder.Eval(Container.DataItem, "MaximumValue")%>
</ItemTemplate>
</asp:TemplateField>
          <asp:TemplateField HeaderText="Spend" >
<ItemTemplate>
<%#DataBinder.Eval(Container.DataItem, "Amount")%>
</ItemTemplate>
</asp:TemplateField>
          <asp:TemplateField HeaderText="Earn Points" >
<ItemTemplate>
<%#DataBinder.Eval(Container.DataItem, "Points")%>
</ItemTemplate>
</asp:TemplateField>

</Columns>
<HeaderStyle CssClass="tableBG " VerticalAlign="Top" HorizontalAlign="Center" />
<RowStyle CssClass="tableBdr tablefontBold" VerticalAlign="Top" HorizontalAlign="Center"/>
</asp:GridView>
                    <%--</td>
                    </tr>
                    </table>--%>
                    <br />
                    <div id="blink">New Feature !!</div>
                    <div class="button" style="float: right; width: 304px;">
                    <a href="#">
                    <asp:LinkButton ID="BtnBookTickets" runat="server" 
                        OnClientClick="javascript:return validUpdate();" Width="241px" 
                        Text="Book Tickets from your Royal Card" onclick="BtnBookTickets_Click"></asp:LinkButton>
                </a><span></span>
            <%--<div style="margin-top: 1em"><font size="1" face="sans-serif"> <blink> Example Heading </blink> </font> </div>--%>
             
                  </div>     </div>
                    
                           
 </div> 
</asp:Content>