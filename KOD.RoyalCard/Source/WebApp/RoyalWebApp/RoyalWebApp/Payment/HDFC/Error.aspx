<%@ Page Title="" Language="C#" MasterPageFile="~/Skins/Master/Payment.Master" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="RoyalWebApp.Payment.HDFC.Error" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHPageData" runat="server">
<div>
        <table border="1" align="center" width="350">
            <tr>
                <th colspan="50" bgcolor="brown">
                    <font size="2" color="White" face="verdana">Error Description
                </th>
            </tr>
            <tr>
                <td colspan="35">
                    Amount
                </td>
                <td>
                    <asp:Label ID="lblamt" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="35">
                    PaymentID
                </td>
                <td>
                    <asp:Label ID="lblpymtid" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="35">
                    Track ID
                </td>
                <td>
                    <asp:Label ID="lbltrackid" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="35">
                    Error Description
                </td>
                <td>
                    <font color="red"><b>
                        <asp:Label ID="lblError" runat="server" Text=""></asp:Label></font>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
