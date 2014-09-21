<%@ Page Title="" Language="C#" MasterPageFile="~/Receipt.master" AutoEventWireup="true"
    CodeFile="Print-Receipt.aspx.cs" Inherits="Payment_Print_Receipt" %>

<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" runat="Server">
    <div id="dvDetails" runat="server">
        <div id="DivPrint" style="overflow: auto; width: 100%">
            <table width="100%">
                <tr>
                    <td colspan="4" style="height: 21px">
                        <b>TRANSACTION RECEIPT</b>
                        <br />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td valign="top">
                        Booking ID
                    </td>
                    <td valign="top">
                        <asp:Label ID="lblBookingID" runat="server" />
                    </td>
                    <td valign="top">
                        Trans Id:
                    </td>
                    <td valign="top">
                        <asp:Label ID="lbltransid" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td valign="top">
                        Venue:
                    </td>
                    <td valign="top">
                        <asp:Label ID="lblVenue" runat="server" />
                    </td>
                    <td valign="top">
                        Show Name:
                    </td>
                    <td valign="top">
                        <asp:Label ID="lblshowname" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td valign="top">
                        Show Date:
                    </td>
                    <td valign="top">
                        <asp:Label ID="lblShowDaTE" runat="server" />
                    </td>
                    <td valign="top">
                        Seat Info:
                    </td>
                    <td valign="top">
                        <asp:Label ID="lblSeatInfo" runat="server" />
                    </td>
                </tr> 
                <tr id="jhumroooffer" runat="server" visible="false">
                     <td valign="top"  >
                        Promo Offer:
                    </td>
                    <td valign="top">
                        <asp:Label ID="lblpromo" runat="server" />
                    </td>
                </tr>                  
                <tr>
                    <td valign="top" colspan="4">
                        <br />
                        TRANSACTION DETAILS
                    </td>
                </tr>
                <tr>
                    <td valign="top">
                        Receipt No:
                    </td>
                    <td valign="top">
                        <asp:Label ID="lblIdbiReceiptno" runat="server"></asp:Label>
                    </td>
                    <td valign="top">
                        Amount:
                    </td>
                    <td valign="top">
                        <asp:Label ID="lbltranamt" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td valign="top">
                        Payment Mode :
                    </td>
                    <td valign="top">
                        <asp:Label ID="lblPayMode" runat="server" />
                    </td>
                    <td valign="top">
                        Booking Date:
                    </td>
                    <td valign="top">
                        <asp:Label ID="lblBookTime" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        Trans Details:
                    </td>
                    <td colspan="3">
                        <asp:Label ID="lbltrnsresponse" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td colspan="4" align="center">
                        <br />
                        <asp:Image ID="img_BarCode" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td colspan="4" id="notes" runat="server">
                        &nbsp;<br />
                        If Transaction is Successful, Please Bring this Booking ID to the Auditorium to
                        collect your tickets and also you need to present the same credit card on which
                        the booking has been done, if tickets booked with credit card.
                    </td>
</tr>
<tr>
                    <td colspan="4" id="ManaNotes" runat="server" visible="false">
                     &nbsp;<br />
                    Please bring this Booking ID to the Box Office counter to collect your tickets and Rs.1000/- value food coupon/card, Valid on show date only.
                    </td>
                    <td colspan="4" id="McNotes" runat="server" visible="false">
                        &nbsp;<br />
                        Please bring this Booking ID to the Box Office counter to collect your tickets and Rs.2000/- Food and retail value coupon per package , Rs. 998/- Reverse Bungee couple tickets per package , Valid on show date only.
                    </td>
                </tr>
            </table>
        </div>
        <br />
        <div>
            <input type="button" id="BtnPrint" class="common-button" value="Print" onclick="javascript:printPreviewDiv('DivPrint')" />
           &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
           
           <asp:Button ID="BtnBackToHome" class="common-button" runat="server" 
                Text="Back To Home" onclick="BtnBackToHome_Click" />
                &nbsp
                <asp:Button ID="BtnBackToPromotion" class="common-button" runat="server" 
                Text="Promotion" onclick="BtnBackToPromotion_Click" />
                &nbsp
                <asp:Button ID="BtnBookMoreTickets" class="common-button-MoreTickets" runat="server" 
                Text="Book More Tickets" Height="26px" Width="200px" 
                onclick="BtnBookMoreTickets_Click" />
        </div>
    </div>
    <div id="dvErrorDetail" runat="server" visible="false">
        <br />
        <br />
        <asp:Label ID="lblFinalMess" CssClass="error" runat="server"></asp:Label>
        <br />
        <asp:Label ID="lblDate" runat="server"></asp:Label>
    </div>
    <div>
        <br />
        <b><u>Note :</u></b> Please close the Browser after taking out the Ticket printout,
        to secure your information.
        <br /><br />
<a href="http://122.248.242.95/RoyalWebAppLatest/Account/Membership.aspx"><asp:Image ID="Image1" runat="server" ImageUrl="../images/booking_kod(3).png" style="height:32px;Width:72px;margin-left:257px;" /></a><br />
<h4 style="margin-left: 119px;">Get upto 50% discount on your next visit to KOD*</h4>

    </div>
</asp:Content>
