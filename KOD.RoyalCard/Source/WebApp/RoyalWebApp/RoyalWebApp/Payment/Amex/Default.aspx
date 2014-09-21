<%@ Page Title="" Language="C#" MasterPageFile="~/Skins/Master/Payment.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="RoyalWebApp.Payment.Amex.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
 <script type="text/javascript">
     history.forward();
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHPageData" runat="server">
<asp:TextBox Visible="false" ID="vpc_MerchTxnRef" runat="server"></asp:TextBox>
    <asp:TextBox Visible="false" ID="vpc_OrderInfo" runat="server"></asp:TextBox>
    <asp:TextBox ID="vpc_Amount" Visible="false" runat="server"></asp:TextBox>
    <asp:Button ID="btnPay" runat="server" Visible="false" Text="Check Out!" />
</asp:Content>
