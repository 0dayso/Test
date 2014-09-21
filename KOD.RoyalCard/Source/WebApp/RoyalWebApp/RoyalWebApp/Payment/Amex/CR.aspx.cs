using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using _Dialect;

namespace RoyalWebApp.Payment.Amex
{
    public partial class CR1 : System.Web.UI.Page
    {
        protected String URL = "";
        String vpc_TxnResponseCode = "";
        String txtRefCode = "";
        String ShoWName = "";
        String stramt = "";
        String ReceiptNo = "";
        String vpc_avsResultCode = "";
        String TranSactNo = "";
        String ResponseCode = "";
        String Amount = "";
        protected void Page_Load(object sender, EventArgs e)
        {          
                GetResponse();           
        }
        protected void GetResponse()
        {
            // Create the VPCRequest object
            VPCRequest conn = new VPCRequest("https://vpos.amxvpos.com/vpcpay");
            //test
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

            // Label_TxnResponseCodeDesc.Text = PaymentCodesHelper.getTxnResponseCodeDescription(Label_vpc_TxnResponseCode.Text);

            // Address Verification / Advanced Address Verification
            vpc_avsResultCode = conn.getResultField("vpc_AVSResultCode", "Unknown");

            // Perform the Capture if the Authorization was successful
            TranSactNo = conn.getResultField("vpc_TransactionNo", "Unknown");
            if (vpc_TxnResponseCode == "0" && (vpc_avsResultCode == "Y" || vpc_avsResultCode == "M"))
            {
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
                //conn.addDigitialOrderField("vpc_User", "kingdom");
                //conn.addDigitialOrderField("vpc_Password", "0password");
                //conn.addDigitialOrderField("vpc_Command", "capture");

                ////live server
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
            }
            //convert amt  
            int amt = (Convert.ToInt32(Amount)) / 100;
            stramt = amt.ToString();
            //pv
            if (vpc_TxnResponseCode.ToString().Equals("0"))
            {
                //Pay Details , Sent To Loyelty Card Page --  CardType,TransID,Amt,ShowName
                UpdateTransDB();
                Response.Write("<b>Payment successful</b> <br/><br/>" + ShoWName.ToUpper() + "<br/> Amount: " + stramt.ToString() + "<br/>Transaction Id: " + txtRefCode.ToString());
              
            }
            else
            {
                Response.Write("Payment Not successful</b> <br/><br/>" + ShoWName.ToUpper() + "<br/> Amount: " + stramt.ToString() + "<br/>Transaction Id: " + txtRefCode.ToString());
            }
        }
        void UpdateTransDB()
        {
            // sign up
            if (ShoWName != "" && ShoWName != "")
            {
                if (ShoWName.ToString().Equals("RCM-signup"))
                {
                    RoyalWebApp.EntityServiceReference.EntityServiceClient ServiceClient = new RoyalWebApp.EntityServiceReference.EntityServiceClient();
                    ServiceClient.Open();
                    int Confirm = ServiceClient.UpdatePaymentDetails(txtRefCode.ToString(), 1, 1, Convert.ToDecimal(stramt.ToString()), txtRefCode.ToString());
                    ServiceClient.Close();
                    // send mail 
                    if (!IsPostBack)
                    {
                        HTMLSignUpMailer(txtRefCode.ToString());
                    }
                }
                //top up
                if (ShoWName.ToString().Equals("RCM-topup"))
                {
                    //update top up

                    RoyalWebApp.EntityServiceReference.EntityServiceClient ServiceClient = new RoyalWebApp.EntityServiceReference.EntityServiceClient();
                    ServiceClient.Open();
                    int Confirm = ServiceClient.UpdateTopUpStatus(txtRefCode.ToString(), 1, 0);
                    ServiceClient.Close();
                    // send mail SMS
                    if (!IsPostBack)
                    {
                        HTMLTopUpMailer(txtRefCode, stramt);
                    }
                }
            }
        }
        void HTMLSignUpMailer(String webid)
        {
            RoyalWebApp.EntityServiceReference.EntityServiceClient ServiceClient = new RoyalWebApp.EntityServiceReference.EntityServiceClient();
            ServiceClient.Open();
            var arr = ServiceClient.GetTempUserDetailsByWebId(webid);
            if (arr.Length > 0)
            {
                String Toname = arr[0].FirstName + " " + arr[0].LastName;
                String Toemail = arr[0].Email.ToString();
                String ToMobile = arr[0].Mobile.ToString();
                ServiceClient.Close();
                // Mailer(txtEmailId.Text.ToString(), WebMemberId, txtFirstName.Text.ToString() + txtLastName.Text.ToString(), txtPassword.Text.ToString());

                RoyalWebApp.MailServer objMail = new RoyalWebApp.MailServer();
                RoyalWebApp.MailData objmaildata = new RoyalWebApp.MailData();
                objmaildata.from = "loyalty.programme@kingdomofdreams.co.in";
                objmaildata.fromName = "KOD ROYAL CARD TEAM";
                //objmaildata.to = "loyalty.programme@kingdomofdreams.co.in";
                objmaildata.to = Toemail.ToString();
                objmaildata.toName = Toname.ToString();
                objmaildata.subject = "Royal Card Membership Mail";
                string BodyMaggage = "<div style='height:990px; width:700px; background:url(http://royalty.kingdomofdreams.in/Skins/images/EmailerBg.jpg) no-repeat;'>"
                                     + "<div style='padding:380px 20px 20px 65px; font-family:Palatino Linotype, Century Gothic, Arial; font-weight:bold; font-size:14px;'>"
                                     + "<p> Your holiness <b> " + webid.ToString() +"</b></p>";


                objmaildata.bodyMessage = BodyMaggage;
                objMail.SendMailKOD(objmaildata);

                // send sign up SMS
                SendSignUpSMS(webid.ToString(), ToMobile.ToString());

            }
        }      
        void HTMLTopUpMailer(String transId, String TransAmt)
        {
             RoyalWebApp.EntityServiceReference.EntityServiceClient ServiceClient = new RoyalWebApp.EntityServiceReference.EntityServiceClient();
            ServiceClient.Open();
            if (Session["RegId"] != null && Session["RegId"] != "")
            {
                var arr = ServiceClient.GetUserDetails(Session["RegId"].ToString());
                if (arr.Length > 0)
                {
                    String Toname = arr[0].FirstName + " " + arr[0].LastName;
                    String Toemail = arr[0].Email.ToString();
                    String webid =arr[0].MemberID.ToString();
                     String ToMobile = arr[0].Mobile.ToString();
                    ServiceClient.Close();
                    // Mailer(txtEmailId.Text.ToString(), WebMemberId, txtFirstName.Text.ToString() + txtLastName.Text.ToString(), txtPassword.Text.ToString());

                    RoyalWebApp.MailServer objMail = new RoyalWebApp.MailServer();
                    RoyalWebApp.MailData objmaildata = new RoyalWebApp.MailData();
                    objmaildata.from = "loyalty.programme@kingdomofdreams.co.in";
                    objmaildata.fromName = "KOD ROYAL CARD TEAM";
                    //objmaildata.to = "loyalty.programme@kingdomofdreams.co.in";
                    objmaildata.to = Toemail.ToString();
                    objmaildata.toName = Toname.ToString();
                    objmaildata.subject = "Royal Card Top Up Mail";
                    string BodyMaggage = "<div style='height:770px; width:727px; background:url(http://royalty.kingdomofdreams.in/Skins/images/Emailer2.jpg) no-repeat;'>"
                        + "<div style='text-align:center; padding:260px 290px 50px 65px; font-family:Palatino Linotype, Century Gothic, Arial; font-weight:bold; font-size:14px;'>"
                        + "Your holiness " + webid.ToString() + ",<br/>"
                         + "Your account is successfully credited with Rs. " + TransAmt.ToString() + " /- with transaction id " + transId.ToString() 
                        + "</div></div>"
                        ;
                    objmaildata.bodyMessage = BodyMaggage;
                    objMail.SendMailKOD(objmaildata);

                    // send SMS
                    SendTopUpSMS(webid, ToMobile, transId, TransAmt);
                }
            }
        }
        void SendSignUpSMS(String WebId, String MobileNo)
        {
            SmsServer SmsObj = new RoyalWebApp.SmsServer();
            string Msg = null;
            Msg = "Your holiness " + WebId.ToString() + "," +
                  " Thank you for enrolling with the Royal council," +
                  "redeem a world of exclusivity! Your activation details will be delivered soon.";
            SmsObj.SendSMS_Sender(MobileNo, Msg, "KOD");
        }
        void SendTopUpSMS(String WebId, String MobileNo,String transId, String TransAmt)
        {
            SmsServer SmsObj = new RoyalWebApp.SmsServer();
            string Msg = null;
            Msg = "Your holiness " + WebId.ToString() + "," +
                  " Your account is successfully credited with Rs. " + TransAmt.ToString() + " /- with transaction id " + transId.ToString() +","+
                  " Thank you for being a part of Kingdom";
            SmsObj.SendSMS_Sender(MobileNo, Msg, "KOD");
        }
    }
}
