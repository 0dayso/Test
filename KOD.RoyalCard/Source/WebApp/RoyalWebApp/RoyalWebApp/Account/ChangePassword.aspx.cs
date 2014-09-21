using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RoyalWebApp.Account
{
    public partial class ChangePassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["RegId"] != null && Session["RegId"] != "")
            {
                BindData();
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }
        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                EntityServiceReference.EntityServiceClient ServiceRefClient = new EntityServiceReference.EntityServiceClient();
                ServiceRefClient.Open();

                if (Session["RegId"] != null && Session["RegId"] != "")
                {

                    int cnfrm = ServiceRefClient.ChangePassword(LblMembershipId.Text.ToString(), txtOldPassword.Text.ToString(), txtNewPassword.Text.ToString());
                    if (cnfrm == 1)
                    {
                        LblMsg.Text = "Password Changed";
                          EntityServiceReference.EntityServiceClient ServiceRefClient2 = new EntityServiceReference.EntityServiceClient();
                          ServiceRefClient2.Open();
                        var arr=ServiceRefClient2.GetUserDetails(Session["RegId"].ToString());
                        if(arr.Length>0)
                        {
                        HTMLMailer(arr[0].Email.ToString(),txtNewPassword.Text.ToString(), Session["RegId"].ToString());
                        }
                        ServiceRefClient2.Close();
                    }
                    else
                    {
                        LblMsg.Text = "Entered Password Is Wrong";
                    }
                }
                ServiceRefClient.Close();            
              }
            catch (Exception ex)
            {
             LblMsg.Text= ex.Message;
            }
        }
        void BindData()
        {
            if (Session["RegId"] != null && Session["RegId"] != "")
            {
                LblMembershipId.Text = Session["RegId"].ToString();
            }
        }

        void HTMLMailer(String ToMail, String pwd, String WebId)
        {
            MailServer objMail = new MailServer();
            MailData objmaildata = new MailData();
            objmaildata.from = "loyalty.programme@kingdomofdreams.co.in";
            objmaildata.fromName = "KOD ROYAL CARD TEAM";

            objmaildata.to = ToMail.ToString();
            objmaildata.toName = "Royal Card Change Password: Mail";
            objmaildata.subject = "Royal Card Change Password Mail";
            string BodyMaggage = "<div style='height:770px; width:727px; background:url(http://royalty.kingdomofdreams.in/Skins/images/Emailer2.jpg) no-repeat;'>"
                + "<div style='text-align:center; padding:260px 290px 50px 65px; font-family:Palatino Linotype, Century Gothic, Arial; font-weight:bold; font-size:14px;'>"
                + "<p>Your holiness " + WebId.ToString() + ",</p>"
                 + "<p>Your New Password is : " + pwd.ToString() + "</p>"
                + "</div></div>"
                ;
            objmaildata.bodyMessage = BodyMaggage;
            objMail.SendMailKOD(objmaildata);

        }    
    }
}