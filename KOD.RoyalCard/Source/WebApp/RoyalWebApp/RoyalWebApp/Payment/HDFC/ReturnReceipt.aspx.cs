using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RoyalWebApp.Payment.HDFC
{
    public partial class ReturnReceipt : System.Web.UI.Page
    {
        public static string IpAddress = System.Configuration.ConfigurationManager.AppSettings["HDFCIP"].ToString();
        string paymentId, ErrorText, result, postdate, tranid, auth, amt, reference;
        string ErrorNo, udf1, udf2, udf3, udf4, udf5, trackid;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                paymentId = Request["paymentid"] ?? String.Empty;
                ErrorText = Request["ErrorText"] ?? String.Empty;
                ErrorNo = Request["Error"] ?? String.Empty;

                udf1 = Request["udf1"] ?? String.Empty;
                udf2 = Request["udf2"] ?? String.Empty;
                udf3 = Request["udf3"] ?? String.Empty;
                udf4 = Request["udf4"] ?? String.Empty;
                udf5 = Request["udf5"] ?? String.Empty;
                if (ErrorNo == String.Empty)
                {
                    result = Request["result"] ?? String.Empty;
                    postdate = Request["postdate"] ?? String.Empty;
                    tranid = Request["tranid"] ?? String.Empty;
                    auth = Request["auth"] ?? String.Empty;
                    trackid = Request["trackid"] ?? String.Empty;
                    reference = Request["ref"] ?? String.Empty;
                    amt = Request["amt"] ?? String.Empty;

                    String responseDetails = string.Format("HDFC Response: paymentId[{0}], ErrorText[{1}], result[{2}], postdate[{3}], tranid[{4}], auth[{5}], amt[{6}], reference[{7}], ErrorNo[{8}], udf1[{9}], udf2[{10}], udf3[{11}], udf4[{12}], udf5[{13}], trackid[{14}]",
                                                            paymentId, ErrorText, result, postdate, tranid, auth, amt, reference, ErrorNo, udf1, udf2, udf3, udf4, udf5, trackid);
                    if (trackid != String.Empty)
                    {
                        UpdateResponse(trackid, reference, result, postdate, auth);
                    }
                }
                else
                {
                    String errorDetails = string.Format("paymentId[{0}], ErrorText[{1}], ErrorNo[{2}], udf1[{3}], udf2[{4}], udf3[{5}], udf4[{6}], udf5[{7}]",
                       paymentId, ErrorText, ErrorNo, udf1, udf2, udf3, udf4, udf5);

                    string err;
                    if (Request["ErrorText"] != null)
                    {
                        err = Request["ErrorText"].ToString();
                    }
                    else
                    {
                        err = "Error occcured in processing payment through HDFC payment gateway.";
                    }
                }
            }
        }

        private void UpdateResponse(string trackid, string reference, string result, string postdate, string auth)
        {
            string exception = "";
            try
            {
                string IpAddress = System.Configuration.ConfigurationManager.AppSettings["HDFCIP"];

                #region parsereference

                string refNo = trackid;

                #endregion parsereference

                if (Request["amt"] != null)
                {
                    //Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("HDFC Amount: " + Request["amt"].ToString());
                    decimal TotalAmount = decimal.Parse(Request["amt"].ToString());
                }

                if (Request["paymentid"] != null)
                {
                    try
                    {
                        string MMTReceiptNo = Request["paymentid"].ToString();
                    }
                    catch (Exception ex)
                    {
                        exception = ex.Message.ToString();
                    }
                }
                if (result == "CAPTURED")
                {
                    # region CAPTURED
                    try
                    {
                        UpdateTransDB();
                        string qString = "Type=" + udf2.ToUpper() + "&Amount=" + amt.ToString() + "&TransactionId=" + trackid.ToString();
                        //LblMsg.Visible = true;
                        //LblMsg.Text = "<b>Payment Successful</b> <br/>Type:<br/>" + udf2.ToUpper() + "<br/> Amount: " + amt.ToString() + "<br/>Transaction Id: " + trackid.ToString();
                        string redirectURL = "REDIRECT=" + IpAddress + "Payment/HDFC/Print-Receipt.aspx?" + qString;
                        Response.Write(redirectURL);
                        return;
                    }
                    catch (Exception)
                    {
                        throw;
                    }

                    # endregion CAPTURED
                }
                else
                {
                    //LblMsg.Text = "<b>Payment Not Successful</b> <br/>Type:<br/>" + udf2.ToUpper() + "<br/> Amount: " + amt.ToString() + "<br/>Transaction Id: " + trackid.ToString();
                }
            }
            catch (Exception ex)
            {
                exception = ex.Message.ToString();
            }
        }
        void UpdateTransDB()
        {
            // sign up
            if (udf2 != "" && udf2 != "")
            {
                if (udf2.ToString().Equals("RCM-signup"))
                {
                    RoyalWebApp.EntityServiceReference.EntityServiceClient ServiceClient = new RoyalWebApp.EntityServiceReference.EntityServiceClient();
                    ServiceClient.Open();
                    int Confirm = ServiceClient.UpdatePaymentDetails(trackid.ToString(), 1, 1, Convert.ToDecimal(amt.ToString()), trackid.ToString());
                    ServiceClient.Close();
                    //send mail
                    if (!IsPostBack)
                    {
                        HTMLSignUpMailer(trackid);
                    }
                }
                //top up
                if (udf2.ToString().Equals("RCM-topup"))
                {
                    //update top up

                    RoyalWebApp.EntityServiceReference.EntityServiceClient ServiceClient = new RoyalWebApp.EntityServiceReference.EntityServiceClient();
                    ServiceClient.Open();
                    int Confirm = ServiceClient.UpdateTopUpStatus(trackid.ToString(), 1, 0);
                    ServiceClient.Close();
                    if (!IsPostBack)
                    {
                        HTMLTopUpMailer(trackid.ToString(),amt.ToString());
                    }
                }
            }
        }
        void SignUpMailer(String webid)
        {
            RoyalWebApp.EntityServiceReference.EntityServiceClient ServiceClient = new RoyalWebApp.EntityServiceReference.EntityServiceClient();
            ServiceClient.Open();
            var arr = ServiceClient.GetTempUserDetailsByWebId(webid);
            if (arr.Length > 0)
            {
                String Toname = arr[0].FirstName + " " + arr[0].LastName;
                String Toemail = arr[0].Email.ToString();
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
                string BodyMaggage = "<p> Dear : " + Toname.ToString() + "</p>"
                     + "<p> Thanks for registering with us </p>"
                     + "<p> Your  Email Id: " + Toemail.ToString() + "</p>"
                     + "<p> Your  Web Booking Id: " + webid.ToString() + "</p>"
                     + "<p>  We will mail your card soon </p>"
                     + "<p>Regards</p><p>KOD ROYAL CARD TEAM</p>"
                    ;
                objmaildata.bodyMessage = BodyMaggage;
                objMail.sendMail_Net(objmaildata);
            }
        }
        void HTMLTopUpMailer(String transId, String TransAmt)
        {
            RoyalWebApp.EntityServiceReference.EntityServiceClient ServiceClient = new RoyalWebApp.EntityServiceReference.EntityServiceClient();
            ServiceClient.Open();
            if (Session["RegId"] != null && Session["RegId"].ToString() != "")
            {
                var arr = ServiceClient.GetUserDetails(Session["RegId"].ToString());
                if (arr.Length > 0)
                {
                    String Toname = arr[0].FirstName + " " + arr[0].LastName;
                    String Toemail = arr[0].Email.ToString();
                    String webid = arr[0].MemberID.ToString();
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
        void SendTopUpSMS(String WebId, String MobileNo, String transId, String TransAmt)
        {
            SmsServer SmsObj = new RoyalWebApp.SmsServer();
            string Msg = null;
            Msg = "Your holiness " + WebId.ToString() + "," +
                  " Your account is successfully credited with Rs. " + TransAmt.ToString() + " /- with transaction id " + transId.ToString() + "," +
                  " Thank you for being a part of Kingdom";
            SmsObj.SendSMS_Sender(MobileNo, Msg, "KOD");
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
                                     + "<div style='padding:380px 20px 20px 65px; font-family:Palatino Linotype, Century Gothic, Arial; font-weight:bold;'>"
                                     + "<p> Your holiness <b> " + Toname.ToString()
                                     + "</b> </p><p>, Thank you for enrolling with the Royal council, redeem a world of exclusivity! </p>"
                                     + "<p> Your  Web Booking Id is :<b> " + webid.ToString() + "</b></p>"
                                     + "<p> Your activation details will be with you soon. </p>";


                objmaildata.bodyMessage = BodyMaggage;
                objMail.SendMailKOD(objmaildata);

            }
        }
    }
}