<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Botypayment.aspx.cs" Inherits="Boty_Botypayment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../Controls/Footer.ascx" TagName="Footer" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <style type="text/css">
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
    background: url(../images/ticket-booking-band-of-the-year.jpg) no-repeat;
    overflow: auto;
    height: 489px;
    padding-left:245px;
    *padding-left:245px;
}
    .style1
    {
        width: 207px;
    }
        .style2
        {
            width: 95px;
        }
    </style>
    <script type="text/javascript">
        history.forward();
    </script>

    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Kingdom of Dreams : Ticket Booking</title>
    <link href="../css/style.css" type="text/css" rel="stylesheet" />
    <link rel="shortcut icon" type="image/x-icon" href="../images/favicon.ico" />
   <%-- <asp:ContentPlaceHolder ID="head" runat="server">
    </asp:ContentPlaceHolder>--%>
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
                <div class="ticket-main">
                    <%--<asp:ContentPlaceHolder ID="mainContent" runat="server">
                    </asp:ContentPlaceHolder>--%>

                    
                    <br /><br /><br /><br /><br />
                    <table>
                     <tr>
                  <td class="style2">
                            <b>Form ID</b>
                        </td>
                        <td style="width: 13px">
                            :
                        </td>
                        <td class="style1" align="left">
                            <asp:TextBox ID="frmid" runat="server" Enabled="False"></asp:TextBox>
                        </td>
                        </tr>
                         <tr>
                        <td class="style2"><b>Entry ID</b></td>
                        <td>:</td>
                        <td class="style1" align="left">
                           <asp:TextBox ID="entryid" runat="server" Enabled="False"></asp:TextBox> 
                        </td> 
                    </tr>
                     <tr>
                        <td class="style2">
                            <b>Amount</b>
                        </td>
                        <td>
                            :
                        </td>
                        <td class="style1" align="left">
                             <asp:TextBox ID="amt" runat="server" Enabled="False"></asp:TextBox>
                        </td>
                    </tr>
                    </table>
            <div style="float: left">
        <br /><asp:Button ID="btn_Submit" CssClass="common-button" ValidationGroup="show" runat="server"
            Text="Proceed" OnClientClick="javascript:return validInsert();" onclick="btn_Submit_Click" /></div><br /><br /><br /><br />
            <center><asp:Label ID="lblMess" runat="server" Text=""></asp:Label></center>
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
<script language="javascript" type="text/javascript">
    function validInsert() {
        var fid = document.getElementById("<%=frmid.ClientID %>");
        if (fid.value == "") {
            alert("Form Id can not be empty");
            return false;
        }
        var eid = document.getElementById("<%=entryid.ClientID %>");
        if (eid.value == "") {
            alert("Enrty Id can not be empty");
            return false;
        }
    }
</script>
</html>
