<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Result.aspx.cs" Inherits="Payment_HDFC_Result" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table border="1" align="center"  width="350">
	<tr>
	<th colspan="50" bgcolor="brown" ><font  size = 2 color = White face = verdana >Response Parameters</th>
	</tr>
		<tr>
			<td colspan="35">Transaction Result</td>
			<td><FONT color="green"><b>
                <asp:Label ID="lbltxnresult" runat="server" Text=""></asp:Label> </td>
		</tr>			
		<tr>
			<td colspan="35">Merchant Track ID</td>
			<td><b>
                <asp:Label ID="lbltrackid" runat="server" Text=""></asp:Label> </td>
		</tr>
</table>

<br><br> 
	
<center><A href="HIndex.aspx"><p style="color:blue"><b>Click here to enter another  transaction</b></p></A></center>
    </div>
    </form>
</body>
</html>
