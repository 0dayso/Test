using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;

namespace RoyalWebApp
{
    public partial class Feedback : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnFeedback_Click(object sender, EventArgs e)
        {
            Inquiryfrm.Visible = true;
            txtFirstName.Text = "";
            txtmobile.Text = "";
            txtEmail.Text = "";
            txtComments.Text = "";
            txtMembership.Text = "";
        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            bool istrue = Regex.IsMatch(txtmobile.Text.ToString(), "^[0-9]{10}$");
            bool isEmailcorrect = Regex.IsMatch(txtEmail.Text.ToString(), "^([0-9a-zA-Z]+([_.-]?[0-9a-zA-Z]+)*@[0-9a-zA-Z]+[0-9,a-z,A-Z,.,-][.]{1}[a-zA-Z]{2,4})+$");
            if (txtFirstName.Text == "" || txtFirstName.Text.ToString() == null)
            {
                ClientScript.RegisterStartupScript(typeof(Page), "test", "alert('Please Enter Your Name');", true);
                txtFirstName.Focus();
            }
            else if (txtEmail.Text == "" || txtEmail.Text.ToString() == null)
	        {
                ClientScript.RegisterStartupScript(typeof(Page), "test", "alert('Please Enter Your Email ID');", true);
                txtEmail.Focus();
	        }
            else if (isEmailcorrect == false)
            {
                ClientScript.RegisterStartupScript(typeof(Page), "test", "alert('Please Enter valid Email ID');", true);
                txtEmail.Focus();
            }
            else if (txtmobile.Text == "" || txtmobile.Text.ToString() == null)
            {
                ClientScript.RegisterStartupScript(typeof(Page), "test", "alert('Please Enter Your Mobile No.');", true);
                txtmobile.Focus();
            }
            else if (istrue==false)
            {
                 ClientScript.RegisterStartupScript(typeof(Page), "test", "alert('Please Enter valid Mobile No.');", true);
                txtmobile.Focus();
            }
            else if (rdYes.Checked&&(txtMembership.Text==""||txtMembership.Text.ToString()==null))
            {
                    ClientScript.RegisterStartupScript(typeof(Page), "test", "alert('Please Enter valid Membership ID');", true);
                    txtMembership.Focus();
                    txtMembership.Enabled = true;
            }
            else if (txtComments.Text == "" || txtComments.Text.ToString() == null)
            {
                ClientScript.RegisterStartupScript(typeof(Page), "test", "alert('Please Enter Your Comments');", true);
                txtComments.Focus();
            }
            else
            {
                Inquiryfrm.Visible = false;
                if (Session["RegId"] != null && Session["RegId"].ToString() != "")
                {
                    EntityServiceReference.EntityServiceClient ServiceClient = new EntityServiceReference.EntityServiceClient();
                    ServiceClient.Open();
                    var arr = ServiceClient.GetUserDetails(Session["RegId"].ToString());
                    if (arr.Length > 0)
                    {
                        String username = arr[0].FirstName.ToString() + " " + arr[0].LastName.ToString();
                        MailToAdmin(arr[0].Email.ToString());
                        MailToUser(arr[0].Email.ToString(), username.ToString());
                        Lblmsg.Visible = true;
                        Lblmsg.Text = "Thanks for your kind Feedback";
                        txtComments.Text = "";
                        BtnSubmit.Enabled = false;
                        ServiceClient.Close();
                    }
                }

                else
                {
                    Session["RegId"] = "";
                    MailToAdmin(txtEmail.Text.ToString());
                    MailToUser(txtEmail.Text.ToString(), txtFirstName.Text.ToString());
                    Lblmsg.Visible = true;
                    Lblmsg.Text = "Thanks for your kind Feedback";
                }
            }
        }
        protected void MailToAdmin(String FromMail)
        {
            MailServer objMail = new MailServer();
            MailData objmaildata = new MailData();
            objmaildata.from = FromMail.ToString();
            objmaildata.fromName = FromMail.ToString();
            objmaildata.to = "loyalty.programme@kingdomofdreams.co.in";
            //objmaildata.to = "shubham1389@gmail.com";
            //objmaildata.to = "pankesh.vaidvan@gcell.in";
            objmaildata.toName = "Royal Card Feedback: Mail";
            objmaildata.subject = "Royal Card Feedback Mail";
            string BodyMaggage = "";
            if (Session["RegId"].ToString() == "" || Session["RegId"] == null)
            {
                BodyMaggage = "<p>Dear Admin</p>"
                +"<p> Name : "+txtFirstName.Text.ToString()+"</p>"
                +"<p> Email Id: " + FromMail.ToString() + "</p>"
                + "<p> Mobile No. : " + txtmobile.Text.ToString() + "</p>"
                + "<p> Topic : " + Rdtopic.SelectedItem.Text.ToString() + "</p>"
                + "<p> Comment: " + txtComments.Text.ToString() + "</p>"
                + "<p>Regards</p><p> " + txtFirstName.Text.ToString() + "</p>"
                ;
            }
            else
            {
                BodyMaggage = "<p> Member ID: " + Session["RegId"].ToString() + "</p>"
                    + "<p> Name : " + txtFirstName.Text.ToString() + "</p>"
                    + "<p> Email Id: " + FromMail.ToString() + "</p>"
                    + "<p> Mobile No. : " + txtmobile.Text.ToString() + "</p>"
                    + "<p> Topic : " + Rdtopic.SelectedItem.Text.ToString() + "</p>"
                    + "<p> Comment: " + txtComments.Text.ToString() + "</p>"
                    + "<p>Regards</p><p> " + Session["RegId"].ToString() + "</p>"
                    ;
            }
            objmaildata.bodyMessage = BodyMaggage;
            objMail.sendMail_Net(objmaildata);
        }
        protected void MailToUser(String ToMail, String ToName)
        {
            MailServer objMail = new MailServer();
            MailData objmaildata = new MailData();
            objmaildata.from = "loyalty.programme@kingdomofdreams.co.in";
            objmaildata.fromName = "loyalty.programme@kingdomofdreams.co.in";
            objmaildata.to = ToMail.ToString();
            //objmaildata.to = "pankesh.vaidvan@gcell.in";
            objmaildata.toName = "Royal Card Feedback: Mail";
            objmaildata.subject = "Royal Card Feedback Mail";
            string BodyMaggage = "<p>Dear  " + ToName.ToString() + ",</p>"
                + "<p> Thanks for your kind Feedback </p>"
                + "<p>We will get back to you soon</p>"
                + "<p>Regards</p><p> KOD ROYAL CARD TEAM </p>"
                ;
            objmaildata.bodyMessage = BodyMaggage;
            objMail.sendMail_Net(objmaildata);
        }
    }
}