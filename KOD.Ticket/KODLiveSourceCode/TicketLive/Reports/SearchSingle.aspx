<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="SearchSingle.aspx.cs"
    MasterPageFile="~/Reports/ReportSingle.master"  Inherits="SearchSingle" %>

<asp:Content ID="head" runat="server" ContentPlaceHolderID="head">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Kingdom of Dreams : Ticket Booking</title>
    <link href="../css/style.css" type="text/css" rel="stylesheet" />
    <link rel="shortcut icon" type="image/x-icon" href="../images/favicon.ico" />
</asp:Content>
<asp:Content ID="mainContent" runat="server" ContentPlaceHolderID="mainContent">
    <asp:UpdatePanel ID="upd1" runat="server">
        <ContentTemplate>
            <span style="font-size: medium; font-weight: bold;">Report - WebBooking Transaction
            </span>
            <br />
            <br />
            <table width="100%">
                <tr>
                    <td align="right">
                        Booking ID
                    </td>
                    <td>
                        :
                    </td>
                    <td>
                        <asp:TextBox ID="txt_BookingID" runat="server" />
                    </td>
                    <td align="right">
                        Receipt No
                    </td>
                    <td>
                        :
                    </td>
                    <td>
                        <asp:TextBox ID="txt_ReceiptNo" runat="server" />
                    </td>
                    <td align="right">
                        Name
                    </td>
                    <td>
                        :
                    </td>
                    <td>
                        <asp:TextBox ID="txt_Name" runat="server" />
                    </td>
                    <td colspan="3">
                        <asp:Button ID="btnGo" runat="server" Text="  Fetch Data  " OnClick="btnGo_Click" />
                    </td>
                </tr>
            </table>
            <b>
                <asp:Label ID="lblMess" runat="server"></asp:Label></b>
            <br />
            <div id="divBooked" style="font-family: Verdana;">
                <asp:GridView ID="gv_Report" FooterStyle-Font-Bold="true" Width="100%" RowStyle-Font-Size="11px" HeaderStyle-Font-Size="12px"
                    FooterStyle-BorderWidth="2px" HeaderStyle-BorderWidth="2px" ShowFooter="true"
                    AutoGenerateColumns="true" runat="server"  />
            </div>
            <b>
                <asp:Label ID="lblMess2" runat="server"></asp:Label></b>
            <br />
            <div id="divFailed" style="font-family: Verdana;">
                <asp:GridView ID="gv_Failed" FooterStyle-Font-Bold="true" Width="100%" RowStyle-Font-Size="11px" HeaderStyle-Font-Size="12px"
                    FooterStyle-BorderWidth="2px" HeaderStyle-BorderWidth="2px" ShowFooter="true"
                    AutoGenerateColumns="true" runat="server"  />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
