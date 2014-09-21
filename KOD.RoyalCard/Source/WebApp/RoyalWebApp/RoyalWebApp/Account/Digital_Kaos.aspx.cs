using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Threading;


namespace RoyalWebApp.Account
{
    public partial class Digital_Kaos : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["MSTicketConnection"].ConnectionString);
        String Gender = "";
        public string myscript = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                grayBG.Visible = false;
                showcontainer.Visible = false;
                Container.Visible = false;
                lb_digitalmsg.Visible = false;
                Session["mailerid"] = "";
                //****************this is used to trace the user redirect to Digital_Kaos*********************
                #region for Royal_MailerTracker
                string Ip = GetIP();
                string cmd = "INSERT INTO [dbo].[Royal_MailerTracker](IP,Date,Source) OUTPUT Inserted.ID,inserted.EMail,inserted.IP,inserted.Name,inserted.Date VALUES('" + Ip + "',getdate(),'Digital_kaos')";
                SqlCommand scmd = new SqlCommand(cmd, con);
                try
                {
                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd, con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    Session["mailerid"] = dt.Rows[0][0].ToString();
                }
                catch (SqlException ex)
                {
                }
                finally
                {
                    con.Close();
                }
                #endregion for Royal_MailerTracker
                //**************************************************************************************************
                bindyear();
                trAnn.Visible = false;
            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            String WebMemberId = "";
            // get max id
            try
            {
                EntityServiceReference.EntityServiceClient ServiceClientInsert2 = new EntityServiceReference.EntityServiceClient();
                ServiceClientInsert2.Open();
                var MobileNo = ServiceClientInsert2.MobilenoExist(txtMobileNo.Text.ToString());

                EntityServiceReference.EntityServiceClient ServiceClientInsert1 = new EntityServiceReference.EntityServiceClient();
                ServiceClientInsert1.Open();
                var emailconfirm = ServiceClientInsert1.EmailExists(txtEmailId.Text.ToString());
                if (emailconfirm > 0)
                {
                    ClientScript.RegisterStartupScript(typeof(Page), "test", "alert('Email Already Exist..!!');", true);
                }
                else if (MobileNo > 0)
                {
                    ClientScript.RegisterStartupScript(typeof(Page), "test", "alert('MobileNo. Already Exist..!!');", true);
                }
                else
                {
                    DateTime dtdoa;
                    String Dob = ddlmonth.SelectedValue.ToString() + "/" + ddlday.SelectedValue.ToString() + "/" + ddlyear.SelectedValue.ToString();
                    String Doa = DdlMonthAnniversary.SelectedValue.ToString() + "/" + DdlDayAnniversary.SelectedValue.ToString() + "/" + DdlYearAnniversary.SelectedValue.ToString();
                    DateTime dtdob = Convert.ToDateTime(Dob);
                    dtdoa = Convert.ToDateTime(Doa);
                    txtDesignation.Text = "";
                    txtPassword.Text = "";
                    Decimal paidamount = Convert.ToDecimal(0.00);
                    if (RdGender.SelectedValue.ToString().Equals("Mr."))
                    {
                        Gender = "Male";
                    }
                    else
                    {
                        Gender = "Female";
                    }
                    if (RdMartialStatus.SelectedValue.ToString().Equals("Married"))
                    {
                        dtdoa = Convert.ToDateTime(Doa);
                    }
                    else
                    {
                        dtdoa = DateTime.Parse("01/01/1753");
                    }
                    EntityServiceReference.EntityServiceClient ServiceClientInsert = new EntityServiceReference.EntityServiceClient();
                    ServiceClientInsert.Open();
                    var confirm = ServiceClientInsert.registeruserwithoutpayment(WebMemberId, RdGender.SelectedValue.ToString(), txtFirstName.Text.ToString(), txtLastName.Text.ToString(), txtAddress.Text.ToString(), txtAddress.Text.ToString(), txtCity.Text.ToString(), txtCountry.Text.ToString(), txtMobileNo.Text.ToString(), txtMobileNo.Text.ToString(), txtEmailId.Text.ToString(), dtdob, dtdoa, RdMartialStatus.SelectedValue.ToString(), Gender, paidamount, DateTime.Now, "0", "-", txtEmailId.Text.ToString(), txtPassword.Text.ToString(), txtDesignation.Text.ToString(), false, false);
                    if (confirm.Length > 0)
                    {
                        WebMemberId = confirm[0].tempwebid.Value.ToString();
                        //****************this is used to trace the useer redirect from mailer*********************
                        #region for update Royal_MailerTracker
                        string str = "update [dbo].[Royal_MailerTracker] set Name='" + txtFirstName.Text + "',EMail='" + txtEmailId.Text + "',WebID=" + WebMemberId + ",MobileNo='" + txtMobileNo.Text + "' where ID=" + Convert.ToInt64(Session["mailerid"].ToString());
                        SqlCommand scmd = new SqlCommand(str, con);
                        try
                        {
                            con.Open();
                            scmd.ExecuteNonQuery();
                        }
                        catch (SqlException ex)
                        {
                        }
                        finally
                        {
                            con.Close();
                        }
                        #endregion for update Royal_MailerTracker
                        //**********************************************************************************************
                        HTMLSignUpMailer(WebMemberId);
                        if (Convert.ToInt32(WebMemberId) > 0)
                        {
                            grayBG.Visible = true;
                            showcontainer.Visible = true;
                            Container.Visible = true;
                            lb_digitalmsg.Visible = true;
                            lb_digitalmsg.Text = "Enrollment No: " + WebMemberId + "" + "<br/> Thank you for Registering with us. Please collect your card from loyalty helpdesk.";
                            ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript: digital_kaos(); ", true);
                        }
                    }
                    ServiceClientInsert.Close();
                    ServiceClientInsert1.Close();
                    ServiceClientInsert2.Close();
                }
            }
            catch (Exception ex)
            {
                LblMsg.Text = ex.Message;
            }
        }
        protected void btnclose_Click(object sender, EventArgs e)
        {
            grayBG.Visible = false;
            showcontainer.Visible = false;
            Container.Visible = false;
            lb_digitalmsg.Visible = false;
            Response.Redirect("http://localhost/RoyalWebApp/Account/Digital_Kaos.aspx");
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
                objmaildata.fromName = "Royal Card Programme";
                objmaildata.CCto = "loyalty.programme@kingdomofdreams.co.in";
                //objmaildata.to = "loyalty.programme@kingdomofdreams.co.in";
                objmaildata.to = Toemail.ToString();
                objmaildata.toName = Toname.ToString();
                objmaildata.subject = "Welcome to the world of Royalty and exclusivity";
                string BodyMaggage = "<div style='height:990px; width:700px;'>"
                                     + "<div style='padding:380px 20px 20px 65px; font-family:Palatino Linotype, Century Gothic, Arial;'>"
                                     + "<p align=left> Website Enrollment No:  " + webid.ToString() + "</p>"
                                     + "<p align=left> Dear  " + Toname.ToString()
                                     + "</p><p>Congratulations!  We are delighted to welcome you to The Royal Card membership program of Kingdom of Dreams. The programme has been thoughtfully designed to offer you an unmatched experience to fulfill your entertainment needs.</p>"
                                     + "<p>As a Royal Purple Card member, you have access to a world of privileges and services which includes, amongst others, like separate entry podium into Kingdom of Dreams, bonus points on special occasions, upgrade vouchers and much more!</p>"
                                     + "<p><b>Your membership card will be delivered to you in person on your visit to Kingdom of Dreams. Please carry a copy of this email along with yourself for verification purpose.</b></p>"
                                     + "<p>We look forward to an enduring association with you. Please find attached detailed benefits of the membership programme.</p>"
                                     + "<p align=left>Sincerely,<br/>Loyalty Department, Kingdom of Dreams<br/>Tel: 0124-4847435<br/>Email Id: loyalty.programme@kingdomofdreams.co.in<br/>Website: http://royalty.kingdomofdreams.in</p>"
                                     + "<p>*All the benefits and privileges of Royal Membership Card can be availed only after receiving Royal Card. Please collect your card from Kingdom of Dreams and present your card before each transaction you made at Kingdom of Dreams. </p>";
                objmaildata.bodyMessage = BodyMaggage;
                objMail.SendMailKOD(objmaildata);
                // send sign up SMS
                SendSignUpSMS(webid.ToString(), ToMobile.ToString());
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
        void bindyear()
        {
            Common libcls = new Common();
            ddlyear.DataSource = libcls.BindYear18Years();
            ddlyear.DataBind();
            Common libcls2 = new Common();
            DdlYearAnniversary.DataSource = libcls2.BindYear();
            DdlYearAnniversary.DataBind();
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
        protected string GetIP()
        {
            string ipaddress;
            ipaddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (ipaddress == "" || ipaddress == null)
                ipaddress = Request.ServerVariables["REMOTE_ADDR"];
            return ipaddress;
        }
    }
}
