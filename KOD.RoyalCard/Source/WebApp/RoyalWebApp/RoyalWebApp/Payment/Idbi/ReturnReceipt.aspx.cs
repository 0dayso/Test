using System;


namespace RoyalWebApp.Payment.Idbi
{
    public partial class ReturnReceipt : System.Web.UI.Page
    {
        public string transactionTypeCode, installments, transactionId, amount, exponent, currencyCode, merchantReferenceNo,
        status, eci, pgErrorCode, pgErrorDetail, pgErrorMsg, messageHash, messageHashBuf, messageHashClient;
        public bool hashMatch = false;
        protected string URL = "";
        protected void Page_Load(object sender, EventArgs e)
        {
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
            pgErrorDetail = Request.Form["pg_error_detail"];
            pgErrorMsg = Request.Form["pg_error_msg"];
            messageHash = Request.Form["message_hash"];
            messageHashBuf = System.Configuration.ConfigurationManager.AppSettings["pgInstanceId"] + "|" +
                System.Configuration.ConfigurationManager.AppSettings["merchantId"] + "|" + transactionTypeCode + "|" +
                installments + "|" + transactionId + "|" + amount + "|" + exponent + "|" + currencyCode + "|" +
                merchantReferenceNo + "|" + status + "|" + eci + "|" + pgErrorCode + "|" +
                System.Configuration.ConfigurationManager.AppSettings["hashKey"] + "|";
            messageHashClient = "13:" + enUtility.DoHash(messageHashBuf);

            if (messageHash == messageHashClient)
            {
                hashMatch = true;
            }
            else
            {
                hashMatch = false;
            }
          
            //Url needed for the agent module to replace here
            if (merchantReferenceNo.Split('_').Length > 1)
            {
                URL = "CR.aspx?tid=" + merchantReferenceNo + "&sta=" + status + "&amt=" + amount + "&rec=" + transactionId;
            }
            else
            {

                URL = "CR.aspx?tid=" + merchantReferenceNo + "&sta=" + status + "&amt=" + amount + "&rec=" + transactionId;
            }
            Response.Redirect(URL);
        }
    }
}