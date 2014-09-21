<%@ Page Language="C#" AutoEventWireup="true" CodeFile="wait.aspx.cs" Inherits="wait" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>    
     <center style="font-family: Verdana; font-size: small; margin-top: 100px">
        <h2>
            Please wait Transaction is in Progress...
        </h2>
        <img id="ima" runat="server" src="~/images/103.gif" alt="Please Wait" />
        <br />
        Please do not Close or Refresh this window...
    </center>
    </div>
    </form>
</body>
</html>
