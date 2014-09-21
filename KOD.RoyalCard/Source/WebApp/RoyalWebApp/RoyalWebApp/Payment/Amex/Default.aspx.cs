using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using _Dialect;

namespace RoyalWebApp.Payment.Amex
{
    public partial class Default : System.Web.UI.Page
    {
       protected void Page_Load(object sender, EventArgs e)
        {
            //PayNowTest();
            PayNowLive();
        }
        private void PayNowTest()
        {
            try
            {
                VPCRequest conn = new VPCRequest("https://vpos.amxvpos.com/vpcpay");
                conn.setSecureSecret("C12DC6FE16681E9DD3211D2BB0C0BBA2");
                conn.addDigitialOrderField("vpc_Version", "1");
                //url needed for the agent module to replace here        
               // conn.addDigitialOrderField("vpc_ReturnURL", "http://localhost:2483/Payment/Amex/CR.aspx");
              //  conn.addDigitialOrderField("vpc_ReturnURL", "http://122.248.250.72/Payment/Amex/CR.aspx");
                conn.addDigitialOrderField("vpc_ReturnURL", "http://royalty.kingdomofdreams.in/Payment/Amex/CR.aspx");


                conn.addDigitialOrderField("vpc_AccessCode", "D30639FF");
                conn.addDigitialOrderField("vpc_Merchant", "TEST9824533848");
                conn.addDigitialOrderField("vpc_Command", "pay");

                decimal amt = decimal.Parse(Request.QueryString["amt"].ToString()) * 100;
                String trnid = Request.QueryString["transid"].ToString();
                String orderinfo = Request.QueryString["show"].ToString();
                String Amt = amt.ToString();
                if (Amt.Contains("."))
                    Amt = Amt.Replace(".00", "");
                conn.addDigitialOrderField("vpc_MerchTxnRef", trnid.ToString());
                conn.addDigitialOrderField("vpc_OrderInfo", orderinfo.ToString());
                conn.addDigitialOrderField("vpc_Amount", Amt.ToString());
                // Perform the transaction
                String URL = conn.Create3PartyQueryString();
                Response.Redirect(URL);
            }
            catch (Exception ex)
            { }

        }
        private void PayNowLive()
        {
            try
            {

                VPCRequest conn = new VPCRequest("https://vpos.amxvpos.com/vpcpay");
                conn.setSecureSecret("44DD98D32ECD3C1AA7F12A1D0F8B41EA");
                conn.addDigitialOrderField("vpc_Version", "1");
                //url needed for the agent module to replace here
               // conn.addDigitialOrderField("vpc_ReturnURL", "http://msticket.kingdomofdreams.in/Payment/Web/CR.aspx");
                conn.addDigitialOrderField("vpc_ReturnURL", "http://royalty.kingdomofdreams.in/Payment/Amex/CR.aspx");
                conn.addDigitialOrderField("vpc_AccessCode", "0FE6FE77");
                conn.addDigitialOrderField("vpc_Merchant", "9824533848");
                conn.addDigitialOrderField("vpc_Command", "pay");

                //Updated on May 04, 2011
                //string[] PayDetails = Session["PayDetailsTemp"].ToString().Split('|');
                //decimal amt = (decimal.Parse(PayDetails[2])) * 100;
                //String trnid = PayDetails[1];
                //String orderinfo = PayDetails[3];



                decimal amt = decimal.Parse(Request.QueryString["amt"].ToString()) * 100;
                String trnid = Request.QueryString["transid"].ToString();
                String orderinfo = Request.QueryString["show"].ToString();
                String Amt = amt.ToString();
                if (Amt.Contains("."))
                    Amt = Amt.Replace(".00", "");

                conn.addDigitialOrderField("vpc_MerchTxnRef", trnid.ToString());
                conn.addDigitialOrderField("vpc_OrderInfo", orderinfo.ToString());
                conn.addDigitialOrderField("vpc_Amount", Amt.ToString());
                // Perform the transaction
                String URL = conn.Create3PartyQueryString();
                Response.Redirect(URL);

            }
            catch (Exception ex)
            { }
        }
    }
}
