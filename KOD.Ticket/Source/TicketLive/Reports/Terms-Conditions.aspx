<%@ Page Language="C#" MasterPageFile="~/Reports/Report.master" AutoEventWireup="true"
    CodeFile="Terms-Conditions.aspx.cs" Inherits="_Terms_Conditions" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Kingdom of Dreams : Ticket Booking</title>
    <link href="../css/style.css" type="text/css" rel="stylesheet" />
    <link rel="shortcut icon" type="image/x-icon" href="../images/favicon.ico" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" runat="Server">
    <span style="font-size: medium; font-weight: bold;">Change - Terms Conditions</span>
    <br />
    <br />
    <table width="80%">
        <tr>
            <td align="right">
                Terms Conditions File :
            </td>
            <td align="left">
                <asp:FileUpload ID="fileShowCast" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <asp:Button ID="btnUpload" runat="server" Text="Upload" OnClick="btnUpload_Click" />
            </td>
        </tr>
    </table>
    <b>
        <asp:Label ID="lblMess" runat="server" EnableViewState="false"></asp:Label></b>
</asp:Content>
