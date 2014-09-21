using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RoyalWebApp.Account
{
    public partial class EditProfile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["RegId"] == null || Session["RegId"] == "")
            {
                Response.Redirect("Login.aspx");
            }
            else
            {
                if (!IsPostBack)
                {
                    bindyear();
                    BindData();
                   
                }
            }
        }

        void BindData()
        {
            EntityServiceReference.EntityServiceClient ServiceClient = new EntityServiceReference.EntityServiceClient();
            ServiceClient.Open();
            if (Session["RegId"] != null && Session["RegId"] != "")
            {
                var arr = ServiceClient.GetUserDetails(Session["RegId"].ToString());
                if (arr.Length > 0)
                {
                    txtFirstName.Text = arr[0].FirstName.ToString();
                    txtLastName.Text = arr[0].LastName.ToString();
                    txtCity.Text = arr[0].City.ToString();

                    txtAddress.Text = arr[0].Address.ToString();
                    txtMobileNo.Text = arr[0].Mobile.ToString();
                    if (!arr[0].DOB.ToString().Equals(""))
                    {
                        String[] arrDob = new String[arr[0].DOB.Length];
                        arrDob = arr[0].DOB.ToString().Split('/');
                        ddlday.SelectedValue = arrDob[0].ToString();
                        ddlmonth.SelectedValue = arrDob[1].ToString();
                        ddlyear.SelectedValue = arrDob[2].ToString();
                    }
                    if (!arr[0].AnniversaryDate.ToString().Equals(""))
                    {

                        String[] arrAnn = new String[arr[0].AnniversaryDate.Length];
                        arrAnn = arr[0].AnniversaryDate.ToString().Split('/');
                        DdlDayAnniversary.SelectedValue = arrAnn[0].ToString();
                        DdlMonthAnniversary.SelectedValue = arrAnn[1].ToString();
                        DdlYearAnniversary.SelectedValue = arrAnn[2].ToString();
                    }
                    txtDesignation.Text = arr[0].Designation.ToString();
                    TxtEmail.Text= arr[0].Email.ToString();
                    DdlCountry.SelectedItem.Text = arr[0].County.ToString();
                    RdMartialStatus.SelectedValue = arr[0].MaritalStatus.ToString();
                    RdGender.SelectedValue = arr[0].Salutation.ToString();
                    if (RdMartialStatus.SelectedValue.ToString().Equals("Married"))
                    {
                        trAnn.Visible = true;
                    }
                    else
                    {
                        trAnn.Visible = false;
                    }
                }
            }
        }
        void bindyear()
        {
            Common libcls = new Common(); 
            ddlyear.DataSource=libcls.BindYear18Years();
            ddlyear.DataBind();
            Common libcls2 = new Common();
            DdlYearAnniversary.DataSource = libcls2.BindYear();
            DdlYearAnniversary.DataBind();
        }
        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
           
            if (Session["RegId"] != null && Session["RegId"] != "")
            {
                
               
                EntityServiceReference.EntityServiceClient ServiceClient = new EntityServiceReference.EntityServiceClient();
                ServiceClient.Open();
                DateTime dtdoa;
                String Dob = ddlmonth.SelectedValue.ToString() + "/" + ddlday.SelectedValue.ToString() + "/" + ddlyear.SelectedValue.ToString();
                String Doa = DdlDayAnniversary.SelectedValue.ToString() + "/" + DdlMonthAnniversary.SelectedValue.ToString() + "/" + DdlYearAnniversary.SelectedValue.ToString();
                DateTime dtdob = Convert.ToDateTime(Dob);
                if (RdMartialStatus.SelectedValue.ToString().Equals("Married"))
                {
                     dtdoa = Convert.ToDateTime(Doa);
                }
                else
                {
                     dtdoa =System.DateTime.Now;
                }
               
                int Confirm = ServiceClient.UpdateUserDetails(Session["RegId"].ToString(), RdGender.SelectedItem.Text.ToString(), txtFirstName.Text.ToString(), txtLastName.Text.ToString(), txtAddress.Text.ToString(), "", txtCity.Text.ToString(), DdlCountry.SelectedItem.Text.ToString(), txtMobileNo.Text.ToString(), TxtEmail.Text.ToString(), dtdob, dtdoa, RdMartialStatus.SelectedValue.ToString(), txtDesignation.Text.ToString(), txtMobileNo.Text.ToString());
                if (Confirm == 1)
                {
                    LblMsg.Text = "Record Updated";
                    HTMLSignUpMailer(Session["RegId"].ToString());
                
                }
                ServiceClient.Close();
              
            }
           
        }
        void HTMLSignUpMailer(String webid)
        {
            RoyalWebApp.EntityServiceReference.EntityServiceClient ServiceClient = new RoyalWebApp.EntityServiceReference.EntityServiceClient();
            ServiceClient.Open();
            var arr = ServiceClient.GetUserDetails(webid);
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
                objmaildata.fromName = "Royal Card Programme";
                objmaildata.CCto = "loyalty.programme@kingdomofdreams.co.in";
                //objmaildata.to = "loyalty.programme@kingdomofdreams.co.in";
                objmaildata.to = Toemail.ToString();
                objmaildata.toName = Toname.ToString();
                objmaildata.subject = "Welcome to the world of Royalty and exclusivity";
                string BodyMaggage = "<div style='height:990px; width:700px;'>"
                                     + "<div style='padding:380px 20px 20px 65px; font-family:Palatino Linotype, Century Gothic, Arial; font-weight:bold;'>"
                                    // + "<p align=left> Reference No: <b> " + webid.ToString() + "</b></p>"
                                     + "<p align=left> Dear <b> " + Toname.ToString()
                                     + "</b></p><p><b>Your Details Have Successfully Modified.</b></p>"
                                    // + "<p>Your membership card is under process. You shall soon receive a call from our loyalty cell, informing you </br>about the details of your membership. </p>"
                                   //  + "<p>We look forward to an enduring association with you. Please find below the details of privileges & </br>benefits of your membership.</p>"
                                   //  + "<p align=left>Sincerely,</p>"
                                     + "<p align=left>Loyalty Department,Kingdom of Dreams </p>"
                                     + "<p align=left>Tel: 0124-4847435</p>"
                                     + "<p align=left>Email: loyalty.programme@kingdomofdreams.co.in </p>"
                                     + "<p align=left>Website: royalty.kingdomofdreams.co.in </p>";


                objmaildata.bodyMessage = BodyMaggage;
                objMail.SendMailKOD(objmaildata);
            }
        }

        protected void RdMartialStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (RdMartialStatus.Items[1].Selected)
            {
                trAnn.Visible = true;
            }
            else
            {
                trAnn.Visible = false;
            }
        }
    }
}