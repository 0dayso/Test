<%@ Page Title="" Language="C#" MasterPageFile="~/Skins/Master/AccountMaster.Master" AutoEventWireup="true" CodeBehind="SeatLayout.aspx.cs" Inherits="ROYALCARD.Account.SeatLayout" %>
<script runat="server">
protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("TicketBooking.aspx");
        }

protected void BtnProceed_Click(object sender, EventArgs e)
{
    Response.Redirect("ContactDetails.aspx");
}
</script>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Kingdom Of Dreams : Ticket Booking</title>

    <script type="text/javascript">
        history.forward();
    </script>

    <link href="~/Skins/css/style.css" type="text/css" rel="stylesheet" />
    <style type="text/css">
        
        .pad
        {
            height: 10px;
            padding: 2px;
        }
        .seats-main_02
{
    width: 98%;
    height:auto;
    
}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHPageHeading" runat="server">
Seat Layout
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHPageData" runat="server">
<div class="seats-main_02">
            <div>
                <input type="hidden" id="hidtempseats1" />
                <asp:HiddenField ID="hidtempseats" runat="server" />
                <asp:HiddenField ID="HiddenBrowser" runat="server" />
                <center>
                    <div class="ModalWindow">
                        <div id="div_seatlayo">
                        <div class="button">
                        <a href="#">
                    <asp:LinkButton ID="BtnProceed" runat="server"  Width="60px" Text="Proceed" onclick="BtnProceed_Click" 
                    ></asp:LinkButton>
                    </a><span></span>
                    </div>
                    <div class="button">
                    <a href="#">
                    <asp:LinkButton ID="btnCancel" runat="server"  Width="60px" Text="Cancel" 
                            OnClientClick="s(false);" onclick="btnCancel_Click"  
                    ></asp:LinkButton>
                    </a><span></span>
                    </div>         
                        <br />
                            
                            <img src="../Skins/images/W_chair.gif" align="absmiddle" />&nbsp;Available Seats&nbsp;&nbsp;
                            <img src="../Skins/images/R_chair.gif" align="absmiddle" />&nbsp;Booked seats &nbsp;&nbsp;
                            <img src="../Skins/images/G_chair.gif" align="absmiddle" />&nbsp;Current selection &nbsp;&nbsp;
                            <img src="../Skins/images/Gy_chair.gif" align="absmiddle" />&nbsp;Others Category &nbsp;&nbsp;
                            <%if (Session["play_Val"] != null)
                              {
                                  if (Session["play_Val"].ToString() == "ZANGOORA")
                                  { 
                            %>
                            <a href="/Skins/images/zangoora-seat-layout.jpg" target="_blank" style="color: White">View</a>
                            <%
                                  } if (Session["play_Val"].ToString() == "CONCERT")
                                  { 
                            %>
                            <a href="/Skins/images/kailash-kher-seat-layout.jpg" target="_blank" style="color: White">View</a>
                            <%
                                  }
                                  else 
                                  { 
                            %>
                            <a href="/Skins/images/jhumroo-seat-layout.jpg" target="_blank" style="color: White">View</a>
                            <%
                                }
                              } %>
                            &nbsp; 3D - Seat Layout
                        </div>
                        <label id="lblpleasewit" style="display: none; font-family: Verdana">
                            Please Wait...</label>
                    </div>
                    <div id="myform" runat="server" />
                </center>

                <script src="/Skins/js/seats.js" type="text/javascript"></script>

                <script type="text/javascript">
                    function getBrowser() {
                        getDoc("<%= HiddenBrowser.ClientID %>").value = navigator.appVersion;
                    }

                    function a() {
                        getBrowser();


                        <%
                        if (Session["play_Val"] != null && Session["play_Val"].ToString() == "CONCERT")
                        {
                        %>
                            alert('To view exact location of seat, please click on 3D View link.');
                        <%
                        }
                        %>

                    }
                    function s(c) {

                        if (c) {

                            if (lTS('hidtempseats1')) {
                                getDoc("<%= hidtempseats.ClientID %>").value = getDoc("hidtempseats1").value;
                                getDoc("div_seatlayo").style.display = "none";
                                getDoc("lblpleasewit").style.display = "block";
                                return true;
                            }
                            else
                                return false;
                        }
                        else {
                            getDoc("div_seatlayo").style.display = "none";
                            getDoc("lblpleasewit").style.display = "block";
                        }

                    }
                </script>
                </div>
                </div>
</asp:Content>
