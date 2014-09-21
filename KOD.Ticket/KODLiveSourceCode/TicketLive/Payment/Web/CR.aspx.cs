using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using _Dialect;
using KoDTicketing;
using KoDTicketing.BusinessLayer;

public partial class Payment_Web_CR : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        GetResponse();
    }
    protected void GetResponse()
    {
        String URL = "";
        String vpc_TxnResponseCode = "";
        String txtRefCode = "";
        String ShoWName = "";
        String stramt = "";
        String ReceiptNo = "";
        String vpc_avsResultCode = "";
        String TranSactNo = "";
        String ResponseCode = "";
        String Amount = "";
        String vpc_3DSstatus = "";
        String vpc_3DSenrolled = "";
        String vpc_AcqAVSRespCode = "";

        try
        {
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Prepare to transact wth AmEx...");

            // Create the VPCRequest object
            VPCRequest conn = new VPCRequest("https://vpos.amxvpos.com/vpcpay");
            //conn.setSecureSecret("C12DC6FE16681E9DD3211D2BB0C0BBA2");

            //Live
            conn.setSecureSecret("44DD98D32ECD3C1AA7F12A1D0F8B41EA");
            // Process the response
            conn.process3PartyResponse(Page.Request.QueryString);

            // Check if the transaction was successful or if there was an error
            vpc_TxnResponseCode = conn.getResultField("vpc_TxnResponseCode", "Unknown");

            // Set the display fields for the receipt with the result fields
            // Core Fields

            // Label_vpc_TxnResponseCode.Text = vpc_TxnResponseCode;

            txtRefCode = conn.getResultField("vpc_MerchTxnRef", "Unknown");
            ShoWName = conn.getResultField("vpc_OrderInfo", "Unknown");

            Amount = conn.getResultField("vpc_Amount", "Unknown");
            ReceiptNo = conn.getResultField("vpc_ReceiptNo", "Unknown");
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("AmEx Receiot No. : " + ReceiptNo);

            //**************Safe Key and AAV Verification************//
            vpc_3DSenrolled = conn.getResultField("vpc_3DSenrolled", "Unknown");
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("vpc_3DSenrolled : " + vpc_3DSenrolled);

            vpc_AcqAVSRespCode = conn.getResultField("vpc_AcqAVSRespCode", "Unknown");
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("vpc_AcqAVSRespCode : " + vpc_AcqAVSRespCode);
            //***********************************************//

            // Address Verification / Advanced Address Verification
            vpc_avsResultCode = conn.getResultField("vpc_AVSResultCode", "Unknown");
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("AmEx transaction Auth result code: " + vpc_avsResultCode);

            // Perform the Capture if the Authorization was successful
            TranSactNo = conn.getResultField("vpc_TransactionNo", "Unknown");
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("AmEx transaction Auth response code: " + vpc_TxnResponseCode);
            string errtxnresponce = PaymentCodesHelper.getTxnResponseCodeDescription(vpc_TxnResponseCode);
            string errAVSResultCode = PaymentCodesHelper.getAVSDescription(vpc_avsResultCode);

            if (vpc_3DSenrolled == "Y")
            {
                vpc_3DSstatus = conn.getResultField("vpc_3DSstatus", "Unknown");
                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("AmEx transaction Auth safe key code vpc_3DSstatus: " + vpc_3DSstatus);
                if (vpc_TxnResponseCode == "0" && (vpc_3DSstatus == "Y" || vpc_3DSstatus == "A"))
                {
                    Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("AmEx authorization successful, starting capture...");
                    // Create a new VPCRequest Object and set the proxy details if required
                    conn = new VPCRequest("https://vpos.amxvpos.com/vpcdps");
                    conn.setProxyHost("");
                    conn.setProxyUser("");
                    conn.setProxyPassword("");
                    conn.setProxyDomain("");

                    //test server
                    // Add the Required Fields
                    //conn.addDigitialOrderField("vpc_Version", "1");
                    //conn.addDigitialOrderField("vpc_AccessCode", "D30639FF");
                    //conn.addDigitialOrderField("vpc_Merchant", "TEST9824533848");
                    //conn.addDigitialOrderField("vpc_User", "kingdomama");
                    //conn.addDigitialOrderField("vpc_Password", "0password");
                    //conn.addDigitialOrderField("vpc_Command", "capture");

                    //live server
                    conn.addDigitialOrderField("vpc_Version", "1");
                    conn.addDigitialOrderField("vpc_AccessCode", "0FE6FE77");
                    conn.addDigitialOrderField("vpc_Merchant", "9824533848");
                    conn.addDigitialOrderField("vpc_User", "kingdomama");
                    conn.addDigitialOrderField("vpc_Password", "0password");
                    conn.addDigitialOrderField("vpc_Command", "capture");

                    conn.addDigitialOrderField("vpc_MerchTxnRef", txtRefCode.Substring(0, txtRefCode.Length - 2) + "-C");
                    conn.addDigitialOrderField("vpc_TransNo", TranSactNo);
                    conn.addDigitialOrderField("vpc_Amount", Amount);
                    // Perform the transaction
                    conn.sendRequest();
                    // Check if the transaction was successful or if there was an error
                    ResponseCode = conn.getResultField("vpc_TxnResponseCode", "Unknown");
                    Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("AmEx transaction Capture response code: " + ResponseCode);
                }
            }
                else
                {
                    if (vpc_TxnResponseCode == "0" && vpc_AcqAVSRespCode == "M")
                    {
                        Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("AmEx authorization successful, starting capture...");
                        // Create a new VPCRequest Object and set the proxy details if required
                        conn = new VPCRequest("https://vpos.amxvpos.com/vpcdps");
                        conn.setProxyHost("");
                        conn.setProxyUser("");
                        conn.setProxyPassword("");
                        conn.setProxyDomain("");

                        //test server
                        // Add the Required Fields
                        //conn.addDigitialOrderField("vpc_Version", "1");
                        //conn.addDigitialOrderField("vpc_AccessCode", "D30639FF");
                        //conn.addDigitialOrderField("vpc_Merchant", "TEST9824533848");
                        //conn.addDigitialOrderField("vpc_User", "kingdomama");
                        //conn.addDigitialOrderField("vpc_Password", "0password");
                        //conn.addDigitialOrderField("vpc_Command", "capture");

                        //live server
                        conn.addDigitialOrderField("vpc_Version", "1");
                        conn.addDigitialOrderField("vpc_AccessCode", "0FE6FE77");
                        conn.addDigitialOrderField("vpc_Merchant", "9824533848");
                        conn.addDigitialOrderField("vpc_User", "kingdomama");
                        conn.addDigitialOrderField("vpc_Password", "0password");
                        conn.addDigitialOrderField("vpc_Command", "capture");

                        conn.addDigitialOrderField("vpc_MerchTxnRef", txtRefCode.Substring(0, txtRefCode.Length - 2) + "-C");
                        conn.addDigitialOrderField("vpc_TransNo", TranSactNo);
                        conn.addDigitialOrderField("vpc_Amount", Amount);
                        // Perform the transaction
                        conn.sendRequest();
                        // Check if the transaction was successful or if there was an error
                        ResponseCode = conn.getResultField("vpc_TxnResponseCode", "Unknown");
                        Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("AmEx transaction Capture response code: " + ResponseCode);
                    }
                }
            /*******************Payement Gateway Error Value Code**********************/
            #region PG_DB
            if (vpc_TxnResponseCode != "0")
            {
                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("PG_DB");
                string bookingid = txtRefCode.Split('_')[1].ToString().Split('~')[0].ToString();
                int i = GTICKBOL.Insert_Payment_DB(errtxnresponce, bookingid, "AMEX");
            }
            else if (vpc_avsResultCode != "X" && vpc_avsResultCode != "Y")
            {
                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("PG_DB");
                string bookingid = txtRefCode.Split('_')[1].ToString().Split('~')[0].ToString();
                int i = GTICKBOL.Insert_Payment_DB(errAVSResultCode, bookingid, "AMEX");
            }
            #endregion PG_DB
            /*********************End******************************/
            //convert amt  
            int amt = (Convert.ToInt32(Amount)) / 100;
            stramt = amt.ToString();
            GTICKBOL gb = new GTICKBOL();
            GTICKV.LogEntry(txtRefCode.Split('_')[0], "Return From AMEX Payment Gateway, amt : " + stramt + ",recieptNO : " + ReceiptNo, "13", txtRefCode.Split('_')[1].Split('~')[0]);
            //***********Url for address Verification*********//
            //URL = "ReturnReceipt.aspx?tid=" + txtRefCode + "&sta=" + vpc_TxnResponseCode + "&amt=" + stramt + "&rec=" + ReceiptNo + "&ResultCode=" + vpc_avsResultCode;
            //************************************************//
            URL = "ReturnReceipt.aspx?tid=" + txtRefCode + "&sta=" + vpc_TxnResponseCode + "&amt=" + stramt + "&rec=" + ReceiptNo + "&Safecode=" + vpc_3DSstatus + "&response=" + vpc_AcqAVSRespCode + "&enroll=" + vpc_3DSenrolled;
            Response.Redirect(URL, false);
        }
        catch (Exception ex)
        {
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Error in AmEx Payment Response: " + ex.Message);
        }
    }
}

//Code for Address Verification
//if (vpc_TxnResponseCode == "0" && (vpc_avsResultCode == "Y" || vpc_avsResultCode == "M"))
//            {
//                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("AmEx authorization successful, starting capture...");
//                // Create a new VPCRequest Object and set the proxy details if required
//                conn = new VPCRequest("https://vpos.amxvpos.com/vpcdps");
//                conn.setProxyHost("");
//                conn.setProxyUser("");
//                conn.setProxyPassword("");
//                conn.setProxyDomain("");
//                //test server
//                // Add the Required Fields
//                //conn.addDigitialOrderField("vpc_Version", "1");
//                //conn.addDigitialOrderField("vpc_AccessCode", "D30639FF");
//                //conn.addDigitialOrderField("vpc_Merchant", "TEST9824533848");
//                //conn.addDigitialOrderField("vpc_User", "kingdom");
//                //conn.addDigitialOrderField("vpc_Password", "0password");
//                //conn.addDigitialOrderField("vpc_Command", "capture");

//                //live server
//                conn.addDigitialOrderField("vpc_Version", "1");
//                conn.addDigitialOrderField("vpc_AccessCode", "0FE6FE77");
//                conn.addDigitialOrderField("vpc_Merchant", "9824533848");
//                conn.addDigitialOrderField("vpc_User", "kingdomama");
//                conn.addDigitialOrderField("vpc_Password", "0password");
//                conn.addDigitialOrderField("vpc_Command", "capture");

//                conn.addDigitialOrderField("vpc_MerchTxnRef", txtRefCode.Substring(0, txtRefCode.Length - 2) + "-C");
//                conn.addDigitialOrderField("vpc_TransNo", TranSactNo);
//                conn.addDigitialOrderField("vpc_Amount", Amount);
//                // Perform the transaction
//                conn.sendRequest();
//                // Check if the transaction was successful or if there was an error
//                ResponseCode = conn.getResultField("vpc_TxnResponseCode", "Unknown");
//                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("AmEx transaction Capture response code: " + ResponseCode);

//                ReceiptNo = conn.getResultField("vpc_ReceiptNo", "Unknown");

//           }