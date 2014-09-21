<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MMTPromotion.aspx.cs" Inherits="MMTPromotion_Default" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../Controls/Footer.ascx" TagName="Footer" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<style type="text/css">
    .txtcolor
{
    border-color: #E7C54A;
    color:#E7C54A;
}
 .ModalWindow
        {
            background-color: #000000;
            border-width: 3px;
            border-style: solid;
            border-color: #E7C54A;
            padding: 3px;
            width: 550px;
            height: 500px;
        }
        .ModalWindow2
        {
            background-color: #FFFFFF;
            border-width: 3px;
            border-style: solid;
            border-color: #E7C54A;
            padding: 3px;
        }
        .modalBackground2
        {
            background-color: Gray;
            filter: alpha(opacity=70);
            opacity: 0.7;
        }
      
       
       
.seats-inside1
{
    width: 846px;
    float: left;
    background: url(../images/makemytrippromotion.jpg) no-repeat;
    overflow: auto;
    height: 489px;
    padding-left:245px;
    *padding-left:245px;
}
.style1
{
    width: 207px;
}
    </style>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Kingdom of Dreams : Ticket Booking</title>
    <link href="../css/style.css" type="text/css" rel="stylesheet" />
    <link rel="shortcut icon" type="image/x-icon" href="../images/favicon.ico" />
   <%-- <asp:ContentPlaceHolder ID="head" runat="server">
    </asp:ContentPlaceHolder>--%>

</head>
<body class="home-page-bg">
    <form id="form1" runat="server">    
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="True">
    </asp:ScriptManager>
    <div class="wrapper">
        <div class="logo-row">
            <div class="logo">
                <a href="http://kingdomofdreams.in/index.html" target="_blank">
                    <img src="../images/logo.jpg" /></a>
            </div>
        </div>
        <!--logo-row ends here -->
        <div class="seats-main">
            <div class="seats-inside1">
                <div class="ticket-main"><br /><br /><br /><br /><br /><br /><br /><br /><br />
                <br /><br /><br /><br /><br /><br />
                 <table>
                
                <tr> 
                <td colspan="2" >No. of Package:</td>
                    <td colspan="2" >
                         <asp:DropDownList ID="ddl_noofpackage" runat="server" Width="70">
                         <asp:ListItem Value="1" Text="1"></asp:ListItem>
                         <asp:ListItem Value="2" Text="2"></asp:ListItem>
                          <asp:ListItem Value="3" Text="3"></asp:ListItem>
                         <asp:ListItem Value="4" Text="4"></asp:ListItem>
                        </asp:DropDownList>  
                    </td>
                </tr>

                <tr>
                    <td colspan="2" >
                     Promotion Code:  
                    </td>
                    <td colspan="2">
                    <asp:TextBox ID="txtMMTPromotionCode" Width="140px" runat="server" CssClass="text-small"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                <td colspan="2">
                Booking Id:                   
                </td>
                <td colspan="2">
                <asp:TextBox ID="txtMMTPNR" Width="140px" runat="server" CssClass="text-small"></asp:TextBox>
                </td>
                </tr>
                <tr>
                <td colspan="2">
                Date:                   
                </td>
                <td>
                    <asp:DropDownList ID="ddl_date" runat="server">
                    </asp:DropDownList>
                </td>
                </tr>
                <tr>
                <td colspan="2">
                <asp:LinkButton ID="TCiNdigo" runat="server" CssClass="clickhere">Terms & Conditions</asp:LinkButton>
                </td>
                </tr>
                 <tr>
                    <td colspan="5" >
                     <font color="red">
                     <asp:Label ID="lblMsgPromotionCode" runat="server" Text="Label"></asp:Label></font>
                    </td>
                </tr>
                  <tr>
                <td colspan="2">
                <asp:Button ID="btnMMTPromotionCode" runat="server" CssClass="common-button" 
                Text="Submit" onclick="btnMMTPromotionCode_Click" />
                </td>
                </tr>
               </table>
                              <cc1:ModalPopupExtender ID="mo12" PopupControlID="dv_pop" BackgroundCssClass="modalBackground2"
        CancelControlID="btnClose" TargetControlID="TCiNdigo" runat="server">
    </cc1:ModalPopupExtender>
     <div id="dv_pop" class="ModalWindow" style="display: none; width: 550px; height: 170px;"
        runat="server">
              
        <div id="Div1" runat="server" style="overflow: auto; width: 530px; height: 130px; padding: 0px 10px 0px 10px">

           
         <font color="red"> <u><center><b> Package Details</b></center></u></font>
           <p>1. The validity of offer is 22nd May’2013 to 16th August’2013. Customers can avail the offer till 60 days from the expiry of offer.<br /><br />
                    2. A customer can book maximum 4 packages on one MMT Booking Id.<br /><br />
                    3. All the Terms & Conditions of Jhumroo/Zangoora are also applied.
                    
                    </p>
                </div>  
       
    <hr />
        <asp:Button Text="Close" runat="server" ID="btnClose" CssClass="common-button"  />

        </div>
                 
        
    </div>
     
            
           
            
                    
                </div>
            </div>
        </div>
        <!-- seats-main ends here -->
    </div>
    <uc1:Footer ID="Footer1" runat="server" />
    
    
    <!-- wrapper ends here -->
    <%--<uc1:Footer ID="Footer1" runat="server" />--%>
    <!-- footer-main ends here -->
    </form>
</body>
</html>
