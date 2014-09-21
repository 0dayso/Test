using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using _Dialect;

namespace RoyalWebApp.Payment.Idbi
{
    public partial class CR : System.Web.UI.Page
    {
        public string transactionId, MainBookingID, amount, merchantReferenceNo, status, AgentCode, ShoWName;
        int amt = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            merchantReferenceNo = Request.QueryString["tid"].ToString();
            status = Request.QueryString["sta"].ToString();
            amount = Request.QueryString["amt"].ToString();
            //convert amt  
            amt = (Convert.ToInt32(amount)) / 100;
            // transactionId = Request.QueryString["rec"].ToString();
            if (merchantReferenceNo.Split('_').Length > 1)
            {
                String[] strarr = new String[100];
                strarr = merchantReferenceNo.Split('_');
                transactionId = strarr[0].ToString();
                ShoWName = strarr[1].ToString();
                GetResponse();
            }
        }
        protected void GetResponse()
        {
            if (status.ToString().Equals("50020"))
            {
                //Pay Details , Sent To Loyelty Card Page --  CardType,TransID,Amt,ShowName
                UpdateTransDB();
                DivResult.InnerHtml = "<b>Payment Successful</b> <br/>Type:<br/>" + ShoWName.ToUpper() + "<br/> Amount: " + amt.ToString() + "<br/>Transaction Id: " + transactionId.ToString();
                //Response.Write("<b>Payment Successful</b> <br/>Type:<br/>" + ShoWName.ToUpper() + "<br/> Amount: " + amt.ToString() + "<br/>Transaction Id: " + transactionId.ToString());
            }
            else
            {
                DivResult.InnerHtml = "<b>Payment Not Successful</b> <br/>Type:<br/>" + ShoWName.ToUpper() + "<br/> Amount: " + amt.ToString() + "<br/>Transaction Id: " + transactionId.ToString();
                //Response.Write("<b>Payment Not Successful</b> <br/>Type:<br/>" + ShoWName.ToUpper() + "<br/> Amount: " + amt.ToString() + "<br/>Transaction Id: " + transactionId.ToString());
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
                    int Confirm = ServiceClient.UpdatePaymentDetails(transactionId.ToString(), 1, 1, Convert.ToDecimal(amt.ToString()), transactionId.ToString());
                    ServiceClient.Close();
                    //send mail
                    if (!IsPostBack)
                    {
                        HTMLSignUpMailer(transactionId);
                    }
                }
                //top up
                if (ShoWName.ToString().Equals("RCM-topup"))
                {
                    //update top up

                    RoyalWebApp.EntityServiceReference.EntityServiceClient ServiceClient = new RoyalWebApp.EntityServiceReference.EntityServiceClient();
                    ServiceClient.Open();
                    int Confirm = ServiceClient.UpdateTopUpStatus(transactionId.ToString(), 1, 0);
                    ServiceClient.Close();
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