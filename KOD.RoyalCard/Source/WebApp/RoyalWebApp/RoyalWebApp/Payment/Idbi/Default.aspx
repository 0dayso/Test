<%@ Page  Language="C#"  AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="RoyalWebApp.Payment.Idbi.Default" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
	<title></title>
	 <script language="javascript">
	     function onLoadSubmit() {
	         document.pgtest.submit();
	     }
</script>
</head>
<body onload="onLoadSubmit();" style="display:none;">
	<form name="pgtest" method="post" action="PostRequest.aspx">	
		<table>
			<tr>
				<td>Amount:</td>
				<td><input type="text" name="amount" value="<%=transAmount %>"/>(implied decimals)</td>
			</tr>
			<tr>
				<td>Merchant Reference No:</td>
				<td><input type="text" name="merchant_reference_no" value="<%=transid %>"/></td>
			</tr>
			<tr>
				<td>Order description:</td>
				<td><INPUT TYPE="text" name="order_desc" value="<%=transshowname %>"/></td>
			</tr>
			<tr>
				<td>Currency Code:</td>
				<td><INPUT TYPE="text" name="currency_code" value="356"/></td>
			</tr>
			<tr>
				<td align="center" colspan="2">
				<input type="radio" name="perform" value="initiatePaymentCapture#sale" checked="checked"/> Sale
				&nbsp;&nbsp;&nbsp;
				<input type="radio" name="perform" value="initiatePaymentCapture#preauth"/> Pre Auth
				</td>
			</tr>
			<tr>
				<td align="center" colspan="2">
				<input type="submit" value=" Pay Now"/>
				</td>
			</tr>
		</table>
	</form>

</body>
</html>
