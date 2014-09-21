using System;
using KoDTicketing.Utilities;

public partial class Payment_Idbi_ReturnReceipt : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        string transactionTypeCode, installments, transactionId, amount, exponent, currencyCode, merchantReferenceNo,
        status, eci, pgErrorCode, pgErrorDetail, pgErrorMsg, messageHash, messageHashBuf, messageHashClient;
        bool hashMatch = false;
        string URL = "";        
        //fill response from idbi
        transactionTypeCode = Request.Form["transaction_type_code"];
        installments = Request.Form["installments"];
        transactionId = Request.Form["transaction_id"];
        amount = Request.Form["amount"];
        exponent = Request.Form["exponent"];
        currencyCode = Request.Form["currency_code"];
        merchantReferenceNo = Request.Form["merchant_reference_no"];
        status = Request.Form["status"];
        eci = Request.Form["3ds_eci"];
        pgErrorCode = Request.Form["pg_error_code"];
        Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("IDBI: " + pgErrorCode);
        pgErrorDetail = Request.Form["pg_error_detail"];
        Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("IDBI:  " + pgErrorDetail);
        pgErrorMsg = Request.Form["pg_error_msg"];
        Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("IDBI: " + pgErrorMsg);
        messageHash = Request.Form["message_hash"];

        messageHashBuf = System.Configuration.ConfigurationManager.AppSettings["pgInstanceId"] + "|" +
            System.Configuration.ConfigurationManager.AppSettings["merchantId"] + "|" + transactionTypeCode + "|" +
            installments + "|" + transactionId + "|" + amount + "|" + exponent + "|" + currencyCode + "|" +
            merchantReferenceNo + "|" + status + "|" + eci + "|" + pgErrorCode + "|" +
            System.Configuration.ConfigurationManager.AppSettings["hashKey"] + "|";
        Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("IDBI: " + messageHashBuf);

        messageHashClient = "13:" + enUtility.DoHash(messageHashBuf);
        hashMatch = (messageHash == messageHashClient);
        Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("IDBI hash " + (hashMatch ? "matched" : "mismatched"));

       //Url needed for the agent module to replace here
        if (merchantReferenceNo.Contains("RCM-topup") || merchantReferenceNo.Contains("RCM-signup"))
        {

            URL = "http://royalty.kingdomofdreams.in/Payment/Idbi/cr.aspx?tid=" + merchantReferenceNo + "&sta=" + status + "&amt=" + amount + "&rec=" + transactionId;

        }
        else
        {

            if (merchantReferenceNo.Split('_').Length > 2)
            {
                URL = "http://www.kodagent.com/ReturnReceipt.aspx?tid=" + merchantReferenceNo + "&sta=" + status + "&amt=" + amount + "&rec=" + transactionId;
            }
            else
            {
                KoDTicketing.GTICKV.LogEntry(merchantReferenceNo.Split('_')[0].ToString(), "Return From IDBI Payment Gateway, amt : " + amount + ",recieptNO : " + transactionId, "9", merchantReferenceNo.Split('_')[1].Split('~')[0]);
                URL = "CR.aspx?tid=" + merchantReferenceNo + "&sta=" + status + "&amt=" + amount + "&rec=" + transactionId;
            }
        }
        Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("IDBI Payment Redirecting..." + URL);
        Response.Redirect(URL, false);
    }
}