<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PostRequest.aspx.cs" Inherits="RoyalWebApp.Payment.Idbi.PostRequest" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Untitled Page</title>
    <script language="javascript">
    function onLoadSubmit() {
    document.merchantForm.submit();
}
</script>
</head>
<body onload="onLoadSubmit();">
	<br />&nbsp;<br />
	<center><font size="5" color="#3b4455">Transaction is being processed,<br/>Please wait ...</font></center>
	<form name="merchantForm" method="post" action="https://<%=System.Configuration.ConfigurationManager.AppSettings["pgdomain"]%>/AccosaPG/verify.jsp">

	<input type="hidden" name="pg_instance_id" value="<%=System.Configuration.ConfigurationManager.AppSettings["pgInstanceId"]%>" />
	<input type="hidden" name="merchant_id" value="<%=System.Configuration.ConfigurationManager.AppSettings["merchantId"]%>" />
	
	<input type="hidden" name="perform" value="<%=perform%>" />
	<input type="hidden" name="amount" value="<%=amount%>" />
	<input type="hidden" name="merchant_reference_no" value="<%=merchantReferenceNo%>" />
	<input type="hidden" name="order_desc" value="<%=orderDesc%>" />

	<input type="hidden" name="message_hash" value="<%=passwordHashSha1%>"/>

	<noscript>
		<br />&nbsp;<br />
		<center>
		<font size="3" color="#3b4455">
		JavaScript is currently disabled or is not supported by your browser.<br />
		Please click Submit to continue the processing of your transaction.<br />&nbsp;<br />
		<input type="submit" />
		</font>
		</center>
	</noscript>
	</form>
</body>    

</html>


