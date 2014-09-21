<%@ Page Title="" Language="C#" MasterPageFile="~/Skins/Master/AccountMaster.Master" AutoEventWireup="true" CodeBehind="Print-Receipt.aspx.cs" Inherits="ROYALCARD.Account.Print_Receipt" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<script runat="server">
protected void BtnBackHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("UserCard.aspx");
        }


protected void BtnMoreTickets_Click(object sender, EventArgs e)
{
    Response.Redirect("TicketBooking.aspx");
}
protected void BtnInviteOthers_Click(object sender, EventArgs e)
{
    
}

</script>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
      #Image
        {
            background:url(../Skins/images/close4.jpg) no-repeat;
                cursor:pointer;
                background-color:#ECA035;
    		width: 20px;
    		height: 20px;
            border:none;
            margin-left:12px;
        }
        #style3
        {
            font-family: "Trebuchet MS", Arial, Helvetica, sans-serif;
            font-size: 14px;
            color: #3f260a;
            text-decoration: none;
            margin-left: 14px;
            margin-right:12px;
            width: 138px;
        }
         
         #buttonticket{
	float: none;
	background-color:#eed075;
	border-color:Orange;
	color: #231f20;
         }
        
    .buttontickets{
	float: none;
	background-color:#eed075;
	border-color:Orange;
	color: #231f20;
}
    .ModalWindow
        {
            background-color: #eed075;
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
    .style3
        {
            font-family: "Trebuchet MS", Arial, Helvetica, sans-serif;
            font-size: 14px;
            color: #3f260a;
            text-decoration: none;
            margin-left: 14px;
            width: 138px;
        }
        .style5
        {
                font-family: "Trebuchet MS", Arial, Helvetica, sans-serif;
                font-size: 14px;
                color: #3f260a;
                text-decoration: none;             
                margin-left: 14px;
        }
       
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHPageHeading" runat="server">
    Payment Receipt
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHPageData" runat="server">
    <table width="100%" >
<tr>
                    <td colspan="4" style="height: 21px">
                    Transaction Receipt  
                    </td>
                </tr>
                <tr>
                <td valign="top" class="style3">
                        Booking ID
                    </td>
                    <td valign="top" class="style3">
                        <asp:Label ID="lblBookingID" runat="server" Text="12345" />
                    </td>
                    <td valign="top" class="style3">
                        Trans Id:
                    </td>
                    <td valign="top" class="style3">
                        <asp:Label ID="lbltransid" runat="server" Text="12346789"></asp:Label>
                    </td>
                    </tr>
                    <tr>
                    <td valign="top" class="style3">
                        Venue:
                    </td>
                    <td valign="top" class="style3">
                        <asp:Label ID="lblVenue" runat="server" Text="Gurgaon" />
                    </td>
                    <td valign="top" class="style3">
                        Show Name:
                    </td>
                    <td valign="top" class="style3">
                        <asp:Label ID="lblshowname" runat="server" Text="Jhumroo" />
                    </td>
                </tr>
                <tr>
                    <td valign="top" class="style3">
                        Show Date:
                    </td>
                    <td valign="top" class="style3">
                        <asp:Label ID="lblShowDaTE" runat="server" Text="15-11-2012"/>
                    </td>
                    <td valign="top" class="style3">
                        Seat Info:
                    </td>
                    <td valign="top" class="style3">
                        <asp:Label ID="lblSeatInfo" runat="server" Text="s1"/>
                    </td>
                </tr>                
                <tr>
                    <td colspan="4" style="height: 21px">
                        <br />
                        Transaction Details
                    </td>
                </tr>
                <tr>
                    <td valign="top" class="style3">
                        Receipt No:
                    </td>
                    <td valign="top" class="style3">
                        <asp:Label ID="lblIdbiReceiptno" runat="server" Text="987654321"></asp:Label>
                    </td>
                    <td valign="top" class="style3">
                        Amount:
                    </td>
                    <td valign="top" class="style3">
                        <asp:Label ID="lbltranamt" runat="server" Text="799"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td valign="top" class="style3">
                        Payment Mode:
                    </td>
                    <td valign="top" class="style3">
                        <asp:Label ID="lblPayMode" runat="server" Text="Credit Card" />
                    </td>

                    <td valign="top" class="style3">
                     &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Royal Card:
                    </td>
                    <td valign="top" class="style3">
                        <asp:Label ID="lblroyalcard" runat="server" Text="400" />
                    </td>
                </tr>
                <tr>
                <td valign="top" class="style3">
                        Booking Date:
                    </td>
                    <td valign="top" class="style3">
                        <asp:Label ID="lblBookTime" runat="server" Text="6:00:00 PM" />
                    </td>
                    <td valign="top" class="style3">
                     &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Debit/Credit:
                    </td>
                    <td valign="top" class="style3">
                        <asp:Label ID="Label1" runat="server" Text="399" />
                    </td>
                </tr>
                <tr>
                <td class="style3">
                        Trans Details:
                    </td>
                    <td colspan="3" class="style3">
                        <asp:Label ID="lbltrnsresponse" runat="server" Text="Succeed" />
                    </td></tr>
                <tr>
                    <td colspan="4" align="center">
                        <br />
                        <asp:Image ID="img_BarCode" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td colspan="4" class="style5">
                        
                        If Transaction is Successful, Please Bring this Booking ID to the Auditorium to
                        collect your tickets and also you need to present the same credit card on which
                        the booking has been done, if tickets booked with credit card.
                    </td>
                </tr>

</table>
<table>
<tr>
<td>
<div class="button" style="float:right; width: 73px;">
                    <asp:LinkButton ID="BtnPrint" runat="server" 
                         Width="34px" 
                        Text="Print" OnClientClick="window.print()"></asp:LinkButton>
              <span></span>
    </div></td>
    <td></td>
    <td>
    <div class="button" style="float:right; width: 136px;">
                    <asp:LinkButton ID="BtnBackHome" runat="server" 
                         Width="94px" 
                        Text="Back To Home" onclick="BtnBackHome_Click"></asp:LinkButton>
              <span></span>
    </div>
    </td>
    <td></td>
    <td>
    <div class="button" style="float:right; width: 167px;">
                    <asp:LinkButton ID="BtnMoreTickets" runat="server" 
                         Width="124px" 
                        Text="Book More Tickets" onclick="BtnMoreTickets_Click"></asp:LinkButton>
              <span></span>
    </div>
    </td>
    <td></td>
    <td>
    <div class="button" style="float:right; width: 207px;">
                    <asp:LinkButton ID="BtnInviteOthers" runat="server" 
                       
                        Text="Invite Other for the Show" onclick="BtnInviteOthers_Click"></asp:LinkButton>
              <span></span>
    </div>
    </td>
  </tr>
</table>
                <div id="dvErrorDetail" runat="server" visible="false">
        <br />
        <br />
        <asp:Label ID="lblFinalMess" CssClass="error" runat="server"></asp:Label>
        <br />
        <asp:Label ID="lblDate" runat="server"></asp:Label>
    </div>
    <div style="height: 21px">
        <br />
        <b><u>Note :</u></b> Please close the Browser after taking out the Ticket printout,
        to secure your information.
    </div>
    <cc1:ModalPopupExtender ID="mo12" PopupControlID="dv_pop" BackgroundCssClass="modalBackground2"
        CancelControlID="btnClose" TargetControlID="BtnInviteOthers" runat="server">
    </cc1:ModalPopupExtender>
    
    <div id="dv_pop" class="ModalWindow" style="display: none; width: 550px; height: 200px;"
        runat="server">
              
        <div runat="server" style="overflow: auto; width: 530px; height: 160px; padding: 0px 10px 0px 10px">
           
           <div id="showcontainer">
        <p  style="padding-top:60px; color:Red">Thank You ! Your Invitation Has Been Sent Successfully.</p><br />
        </div>
           <table id="mainContainer1">
           <tr><td>
           <center>INVITE OTHER'S FOR THE SHOW</center></td></tr>
           <tr><td>
                <div id="mainContainer">
		<div><u class="style3">Contact No. :</u>&nbsp;&nbsp;&nbsp;&nbsp;<input type="text" class="buttontickets"/>&nbsp;&nbsp;&nbsp;<input type="button" value="Add Another" onClick="addNew()" border="none" class="buttontickets"></input>
        &nbsp;<input type="button" value="Submit" class="buttontickets" id="btnsubmit" runat="server" onclick="setVisibility()"/>
        </div>        
		</div>
        </td>
            </tr>
            </table>
                </div>  
       
    <hr />
        <asp:Button Text="Close" runat="server" ID="btnClose" CssClass="buttontickets"  />

        </div>

        <script type="text/javascript">
            window.onload = function () {
                document.getElementById('showcontainer').style.display = "none";
            }       
                function setVisibility() {
                    document.getElementById('mainContainer1').style.display = "none";
                    document.getElementById('showcontainer').style.display = "inline";
            }
            var counter = 2;
            function addNew() {
                var mainContainer = document.getElementById('mainContainer');
                var newDiv = document.createElement('div');
                
                var newText = document.createElement('input');
                newText.type = "input";
                newText.id = "buttonticket";
                
                var newDelButton = document.createElement('input');
                newDelButton.type = "image";
                newDelButton.id = "Image";
                newDelButton.name = "b1";

                var newlabel = document.createElement('Label');
                newlabel.id = "style3"
                newlabel.appendChild(document.createTextNode("Contact No. : "));

                newDiv.appendChild(newlabel);
                newDiv.appendChild(newText);
                newDiv.appendChild(newDelButton);
                mainContainer.appendChild(newDiv);
                counter++;
                newDelButton.onclick = function () {
                    mainContainer.removeChild(newDiv);
                }
            }
		</script>

    
</asp:Content>
