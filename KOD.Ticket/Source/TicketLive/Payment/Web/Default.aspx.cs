using System;
using System.Linq;
using _Dialect;
using System.Configuration;

public partial class Payment_Web_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("default page of amex");
        PayNowTest();
    }
    private void PayNowTest()
    {
        try
        {
            VPCRequest connVPCAMEX = new VPCRequest("https://vpos.amxvpos.com/vpcpay");
            connVPCAMEX.setSecureSecret("C12DC6FE16681E9DD3211D2BB0C0BBA2");
            connVPCAMEX.addDigitialOrderField("vpc_Version", "1");
            //url needed for the agent module to replace here        
            connVPCAMEX.addDigitialOrderField("vpc_ReturnURL", System.Configuration.ConfigurationManager.AppSettings["KoDTicketingIPAddress"] + "Payment/Web/CR.aspx");
            connVPCAMEX.addDigitialOrderField("vpc_AccessCode", "D30639FF");
            connVPCAMEX.addDigitialOrderField("vpc_Merchant", "TEST9824533848");
            connVPCAMEX.addDigitialOrderField("vpc_Command", "pay");


            string[] PayDetails = Session["PayDetailsTemp"].ToString().Split('|');
            //decimal amt = (decimal.Parse(PayDetails[2])) * 100;
            //String trnid = PayDetails[1];
            //String orderinfo = PayDetails[3];
            decimal amt = decimal.Parse(Request.QueryString["amt"].ToString()) * 100;
            String trnid = Request.QueryString["transid"].ToString();
            String orderinfo = Request.QueryString["show"].ToString();
            String pin = Request.QueryString["pin"].ToString();
            String street = Request.QueryString["street"].ToString();
            String city = Request.QueryString["city"].ToString();
            String state = Request.QueryString["state"].ToString();
            String tital = Request.QueryString["title"].ToString();
            String fname = Request.QueryString["fname"].ToString();
            String mname = Request.QueryString["mname"].ToString();
            String lname = Request.QueryString["lname"].ToString();
            String country = Request.QueryString["country"].ToString();
            String Amt = amt.ToString();
            if (Amt.Contains("."))
                Amt = Amt.Replace(".00", "");
            connVPCAMEX.addDigitialOrderField("vpc_MerchTxnRef", trnid.ToString());
            connVPCAMEX.addDigitialOrderField("vpc_OrderInfo", orderinfo.ToString());
            connVPCAMEX.addDigitialOrderField("vpc_Amount", Amt.ToString());
            connVPCAMEX.addDigitialOrderField("vpc_AVS_Street01", street);
            connVPCAMEX.addDigitialOrderField("vpc_AVS_PostCode", pin);

            connVPCAMEX.addDigitialOrderField("vpc_BillTo_Title", tital);
            connVPCAMEX.addDigitialOrderField("vpc_BillTo_Firstname", fname);
            connVPCAMEX.addDigitialOrderField("vpc_BillTo_Middlename", mname);
            connVPCAMEX.addDigitialOrderField("vpc_BillTo_Lastname", lname);
            connVPCAMEX.addDigitialOrderField("vpc_AVS_City", city);
            connVPCAMEX.addDigitialOrderField("vpc_AVS_StateProv", state);
            connVPCAMEX.addDigitialOrderField("vpc_AVS_Country", country);

            // Perform the transaction
            String URL = connVPCAMEX.Create3PartyQueryString();
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write(URL);
            Response.Redirect(URL, false);
        }
        catch (Exception ex)
        {
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Amex Test Payment" + ex.Message);
        }

    }
    private void PayNowLive()
    {
        try
        {
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Processing payment... ");
            if (Session["PayDetailsTemp"] != null)
            {
                VPCRequest conn = new VPCRequest("https://vpos.amxvpos.com/vpcpay");
                conn.setSecureSecret("44DD98D32ECD3C1AA7F12A1D0F8B41EA");
                conn.addDigitialOrderField("vpc_Version", "1");
                conn.addDigitialOrderField("vpc_ReturnURL", System.Configuration.ConfigurationManager.AppSettings["KoDTicketingIPAddress"] + "Payment/Web/CR.aspx");                
                conn.addDigitialOrderField("vpc_AccessCode", "0FE6FE77");
                conn.addDigitialOrderField("vpc_Merchant", "9824533848");
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
                String URL = conn.Create3PartyQueryString();
                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Redirecting to: " + URL );
                Response.Redirect(URL, false);
            }
        }
        catch (Exception ex)
        {
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Error in AmEx Payment: "+ ex.Message);
        }
    }

    /* protected void PayNow()
     {
         try
         {
             //// Connect to the Payment Client         
             ////test Server 
             VPCRequest conn = new VPCRequest("https://vpos.amxvpos.com/vpcpay");
             conn.setSecureSecret("C12DC6FE16681E9DD3211D2BB0C0BBA2");
             conn.addDigitialOrderField("vpc_Version", "1");
             //            conn.addDigitialOrderField("vpc_ReturnURL", "http://msticket.kingdomofdreams.in/Payment/Web/ReturnReceipt.aspx");
             conn.addDigitialOrderField("vpc_ReturnURL", "http://localhost:2256/Ticket_Navision/Payment/Web/CR.aspx");
             conn.addDigitialOrderField("vpc_AccessCode", "D30639FF");
             conn.addDigitialOrderField("vpc_Merchant", "TEST9824533848");
             conn.addDigitialOrderField("vpc_Command", "pay");
             ////test server ends
             ////live server
             //VPCRequest conn = new VPCRequest("https://vpos.amxvpos.com/vpcpay");
             //conn.setSecureSecret("44DD98D32ECD3C1AA7F12A1D0F8B41EA");
             //// Add the Digital Order Fields for the desired functionality
             //// Core Transaction Fields
             //conn.addDigitialOrderField("vpc_Version", "1");
             //conn.addDigitialOrderField("vpc_ReturnURL", "http://msticket.kingdomofdreams.in/Payment/Web/CR.aspx");
             //conn.addDigitialOrderField("vpc_AccessCode", "0FE6FE77");
             //conn.addDigitialOrderField("vpc_Merchant", "9824533848");
             //conn.addDigitialOrderField("vpc_Command", "pay");
             ////live server ends
             //pv
             decimal amt = decimal.Parse(Request.QueryString["amt"].ToString()) * 100;
             String trnid = Request.QueryString["transid"].ToString();
             String orderinfo = Request.QueryString["show"].ToString();
             String Amt = amt.ToString();
             if (Amt.Contains('.'))
                 Amt = Amt.Replace(".00", "");
             conn.addDigitialOrderField("vpc_MerchTxnRef", trnid.ToString());
             conn.addDigitialOrderField("vpc_OrderInfo", orderinfo.ToString());
             conn.addDigitialOrderField("vpc_Amount", Amt.ToString());
             // Perform the transaction
             String URL = conn.Create3PartyQueryString();
             Server.Transfer(URL);
         }
         catch (Exception ex)
         {}
     }*/
}
