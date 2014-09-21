<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Payment_Web_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Kingdom of Dreams</title>
      <script type="text/javascript">
        history.forward();
    </script>
</head>
<body>
   
    <asp:TextBox Visible="false" ID="vpc_MerchTxnRef" runat="server"></asp:TextBox>
    <asp:TextBox Visible="false" ID="vpc_OrderInfo" runat="server"></asp:TextBox>
    <asp:TextBox ID="vpc_Amount" Visible="false" runat="server"></asp:TextBox>
    <asp:Button ID="btnPay" runat="server" Visible="false" Text="Check Out!" />
</body>
</html>
