using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace RoyalWebApp.Account
{
    public partial class ForgotPassword : System.Web.UI.Page
    {string emailid="",pwd="";
        protected void Page_Load(object sender, EventArgs e)
        {
            //LblMsg.Text = "Wrong Member Id or Email Id entered";
        }        
        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            EntityServiceReference.EntityServiceClient ServiceRefClient = new EntityServiceReference.EntityServiceClient();
            ServiceRefClient.Open();
            var arr = ServiceRefClient.ForgotPassword(txtmembershipId.Text.ToString(), txtEmailId.Text.ToString());
            if (arr.Length > 0)
            {
                //LblMsg.Text = arr[0].Password.ToString();
                emailid = txtEmailId.Text.ToString();
                if (arr[0].Value.ToString().Equals("1"))// pwd exist already
                {

                    if (arr[0].Password.ToString().Trim().Equals("") || arr[0].Password.ToString().Trim().Equals(null))// pwd exist already
                     {
                         ////generate pwd
                         //var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                         //var random = new Random();
                         //var pwd = new string(
                         //    Enumerable.Repeat(chars, 8)
                         //              .Select(s => s[random.Next(s.Length)])
                         //              .ToArray());

                         ////update pwd in db
                         //EntityServiceReference.EntityServiceClient ServiceRefClient1 = new EntityServiceReference.EntityServiceClient();
                         //ServiceRefClient1.Open();
                         //int cnfrm = ServiceRefClient1.ChangePassword(txtmembershipId.Text.ToString(), arr[0].Password.ToString(), pwd.ToString());
                         //ServiceRefClient1.Close();
                         //HTMLMailer(emailid.ToString(), pwd.ToString(), txtmembershipId.Text.ToString());
                         //LblMsg.Text = "Your password is mailed at " + emailid.ToString();
                         LblMsg.Text = "Password not found! Please register and verify your membership.";
                     }
                     else
                     {
                         pwd = arr[0].Password.ToString();
                         LblMsg.Text = "Your password is mailed at " + emailid.ToString();
                         // send mail sms
                         HTMLMailer(emailid.ToString(), pwd.ToString(), txtmembershipId.Text.ToString());
                     }                    
                                         
                }
                else
                {
                    LblMsg.Text = "<center>Dear Guest,The Email ID or MembershipID provided by you does not match with our records.<br/>Request you to call at Loyalty Desk for assistance.</center><center>Tel:0124-4847435.</center><center>Email:<a href='mailto:loyalty.programme@kingdomofdreams.co.in'>loyalty.programme@kingdomofdreams.co.in</center></a>";
                } 
            }          
            ServiceRefClient.Close();
        }
       
          void Mailer(String FromMail, String pwd)
        {
            MailServer objMail = new MailServer();
            MailData objmaildata = new MailData();
            objmaildata.from = "loyalty.programme@kingdomofdreams.co.in";
            objmaildata.fromName = "KOD ROYAL CARD TEAM";
            //objmaildata.to = "loyalty.programme@kingdomofdreams.co.in";
            objmaildata.to = FromMail.ToString();
            objmaildata.toName = "Royal Card Forgot Password: Mail";
            objmaildata.subject = "Royal Card Forgot Password Mail";
            string BodyMaggage = "<p> Email Id: " + FromMail.ToString() + "</p>"
                + "<p> New Password: " + pwd.ToString() + "</p>"
                + "<p>Regards</p><p>KOD ROYAL CARD TEAM</p>"
                ;
            objmaildata.bodyMessage = BodyMaggage;
            objMail.sendMail_Net(objmaildata);
        }
          void HTMLMailer(String FromMail, String pwd, String WebId)
          {
              MailServer objMail = new MailServer();
              MailData objmaildata = new MailData();
              objmaildata.from = "loyalty.programme@kingdomofdreams.co.in";
              objmaildata.fromName = "KOD ROYAL CARD TEAM";

              objmaildata.to = FromMail.ToString();
              objmaildata.toName = "Royal Card Forgot Password: Mail";
              objmaildata.subject = "Royal Card Forgot Password Mail";
              string BodyMaggage = "<div style='height:770px; width:727px; background:url(http://royalty.kingdomofdreams.in/Skins/images/Emailer2.jpg) no-repeat;'>"
                  + "<div style='text-align:center; padding:260px 290px 50px 65px; font-family:Palatino Linotype, Century Gothic, Arial; font-weight:bold; font-size:14px;'>"
                  + "<p>Your holiness " + WebId.ToString() + ",</p>"
                   + "<p>Your Password is : " + pwd.ToString() + "</p>"
                  + "</div></div>"
                  ;
              objmaildata.bodyMessage = BodyMaggage;
              objMail.SendMailKOD(objmaildata);

          } 
         
    }
}