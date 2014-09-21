<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="Seat-layout.aspx.cs"
    Inherits="Seat_layout" %>
    <%@ Register Src="~/Controls/Footer.ascx" TagName="Footer" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Kingdom Of Dreams : Ticket Booking</title>

    <script type="text/javascript">
        history.forward();
    </script>

    <link href="../../css/style.css" type="text/css" rel="stylesheet" />
    <link href="../../css/royaltyCard.css" type="text/css" rel="stylesheet" />
    <link rel="shortcut icon" type="image/x-icon" href="../../images/favicon.ico" />
    <style type="text/css">
        .ModalWindow
        {
            background-image: url(images/bar-bg.jpg);
            background-color: #4D103C;
            background-repeat: repeat-x;
            border-width: 3px;
            border-style: solid;
            border-color: #711B5B;
            margin: 10px 10px 3px 10px;
            color: White;
            font-family: Verdana;
            font-size: 12px;
            width: 1020px;
            text-align: left;
        }
        .pad
        {
            height: 10px;
            padding: 2px;
        }
    </style>
</head>
<body class="home-page-bg" onload="a()">
    <form id="form1" runat="server">
    <div class="wrapper_seatlayout">
        <div class="logo-row">
            <div class="logo">
                <a href="http://kingdomofdreams.in/index.html" target="_blank">
                    <img src="../../images/logo.jpg"/></a>
            </div>
        </div>
        <!--logo-row ends here -->
        <div class="seats-main_02">
            <div>
                <input type="hidden" id="hidtempseats1" />
                <asp:HiddenField ID="hidtempseats" runat="server" />
                <asp:HiddenField ID="HiddenBrowser" runat="server" />
                <center>
                    <div class="ModalWindow">
                        <div id="div_seatlayo">
                      <asp:Button ID="btnProceed" CssClass="common-button" runat="server" OnClientClick="return s(true);"
                                Text="Proceed" OnClick="btnProceed_Click" /> &nbsp;<asp:Button ID="btnCancel" CssClass="common-button"
                                    runat="server" Text="Cancel" OnClientClick="s(false);" OnClick="btnCancel_Click" />
                            &nbsp;
                            <img src="../../images/W_chair.gif" align="absmiddle" />&nbsp;Available Seats&nbsp;&nbsp;
                            <img src="../../images/R_chair.gif" align="absmiddle" />&nbsp;Booked seats &nbsp;&nbsp;
                            <img src="../../images/G_chair.gif" align="absmiddle" />&nbsp;Current selection &nbsp;&nbsp;
                            <img src="../../images/Gy_chair.gif" align="absmiddle" />&nbsp;Others Category &nbsp;&nbsp;
                            <%if (Session["play_Val"] != null)
                              {
                                  if (Session["play_Val"].ToString() == "ZANGOORA")
                                  { 
                            %>
                            <a href="../../images/zangoora-seat-layout.jpg" target="_blank" style="color: White">View</a>
                            <%
                                  } if (Session["play_Val"].ToString() == "CONCERT")
                                  { 
                            %>
                            <a href="../../images/kailash-kher-seat-layout.jpg" target="_blank" style="color: White">View</a>
                            <%
                                  }
                                  else 
                                  { 
                            %>
                            <a href="../../images/jhumroo-seat-layout.jpg" target="_blank" style="color: White">View</a>
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

                <script src="../../js/RoyalcardSeats.js" type="text/javascript"></script>

                <script type="text/javascript">
                    function getBrowser() {
                        getDoc("<%= HiddenBrowser.ClientID %>").value = navigator.appVersion;
                    }

                    function a() {
                        getBrowser();
                        //window.setTimeout("gADef('<%=myform.ClientID %>',<%=seatreq %>,'<%=cat %>')", 0);
                        window.setTimeout("selectSeats('<%=myform.ClientID %>',<%=seatreq %>,'<%=cat %>')", 0);
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
            <!-- seats-main ends here -->
        </div>
        <!-- wrapper ends here -->
    </div>
    <uc1:Footer ID="Footer1" runat="server" />
    </form>
    <!-- footer-main ends here -->
</body>
</html>
