<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Please-Wait.aspx.cs" Inherits="Payment_Please_Wait" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script type="text/javascript" language="javascript">
	<!--
        var pollInterval = 1000;
        var tim = 10;
        var checkStatusUrl = "checkStatus.aspx";
        var req;

        // this tells the wait page to check the status every so often
        window.setInterval("checkStatus()", pollInterval);       
        
        function checkStatus() {
            createRequester();

            if (req != null) {
                req.onreadystatechange = process;
                req.open("GET", checkStatusUrl, true);
                req.send(null);
            }
        }

        function process() {
            if (req.readyState == 4) {
                // only if "OK"
                if (req.status == 200) {
                    if (req.responseText == "1") {
                        // a "1" means it is done, so here is where you redirect
                        // to the confirmation page

                        document.location.replace("Print-Receipt.aspx");
                    }
                    // NOTE: any status other than 200 or any response other than
                    // "1" require no action
                }
            }
        }

        /*
        Note that this tries several methods of creating the XmlHttpRequest object,
        depending on the browser in use. Also note that as of this writing, the
        Opera browser does not support the XmlHttpRequest.
        */
        function createRequester() {
            try {
                req = new ActiveXObject("Msxml2.XMLHTTP");
            }
            catch (e) {
                try {
                    req = new ActiveXObject("Microsoft.XMLHTTP");
                }
                catch (oc) {
                    req = null;
                }
            }

            if (!req && typeof XMLHttpRequest != "undefined") {
                req = new XMLHttpRequest();
            }

            return req;
        }
	//-->
    </script>

</head>
<body>
    <form id="Form1" method="post" runat="server">
    <center style="font-family: Verdana; font-size: small; margin-top: 100px">
        <h2>
            Please wait Transaction is in Progress...
        </h2>
        
        <img id="ima" runat="server" src="~/images/103.gif" alt="Please Wait" />
        <br />
        Please do not Close or Refresh this window...
    </center>
    </form>
</body>
</html>
