using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RoyalWebApp.Account
{
    public partial class PGDetails : System.Web.UI.Page   
    {
        protected void Page_Load(object sender, EventArgs e)
        {          
                BindData();           
        }      
        void BindData()
        {
            if (Request.QueryString["Type"] != null && Request.QueryString["Type"] != "")
                {
                    if (Request.QueryString["Type"].ToString().Equals("signup"))
                    {
                        LblMemberId.Text = Request.QueryString["WebId"].ToString();                       
                        LblType.Text = "RCM-signup";
                        LblAmount.Text = "500";
                    }
                    else if (Request.QueryString["Type"].ToString().Equals("topup"))
                    {
                        if (Request.QueryString["amt"] != null && Request.QueryString["amt"] != "")
                        {
                            if (Session["RegId"] != null && Session["RegId"] != "")
                            {
                                LblMemberId.Text = Session["RegId"].ToString();
                                LblType.Text = "RCM-topup";
                                LblAmount.Text = Request.QueryString["amt"].ToString();
                            }
                            else
                            {
                                Response.Redirect("Login.aspx");
                            }
                        }
                    }
                }               
         }
        void InsertTransDetails()
        {
            if (Request.QueryString["Type"] != null && Request.QueryString["Type"] != "")
            {
                if (Request.QueryString["Type"].ToString().Equals("signup"))
                {
                    //Pay Details , membership payment details update
                    EntityServiceReference.EntityServiceClient ServiceClient = new EntityServiceReference.EntityServiceClient();
                    ServiceClient.Open();
                    int Confirm = ServiceClient.UpdatePaymentDetails(LblMemberId.Text.ToString(), 0, 0, Convert.ToDecimal(LblAmount.Text.ToString()), LblMemberId.Text.ToString());
                    ServiceClient.Close();

                    //PG 
                    if (rbl_CardType.SelectedValue == "IDBI")
                    {
                        Response.Redirect("../Payment/Idbi/Default.aspx?type=idbi&transid=" + LblMemberId.Text.ToString() + "_" + LblType.Text.ToString()  +"&amt=" + LblAmount.Text.ToString() + "&show=" + LblType.Text.ToString());
                    }
                    else if (rbl_CardType.SelectedValue == "AMEX")
                    {
                        Response.Redirect("../Payment/Amex/Default.aspx?type=amex&transid=" + LblMemberId.Text.ToString() + "&amt=" + LblAmount.Text.ToString() + "&show=" + LblType.Text.ToString());
                    }
                    // end PG
                }
                else if (Request.QueryString["Type"].ToString().Equals("topup"))
                {
                    // top up insert
                    //Pay Details , membership payment details update
                    EntityServiceReference.EntityServiceClient ServiceClient = new EntityServiceReference.EntityServiceClient();
                    ServiceClient.Open();
                    // trans type 0=recharge, 1=payment
                    var Confirm = ServiceClient.InsertTopUpDetails(LblMemberId.Text.ToString(),Convert.ToDecimal(LblAmount.Text.ToString()),DateTime.Now.Date,LblMemberId.Text.ToString(),0);
                    if (Confirm[0].Transaction_Id != "")
                    {
                        LblTransId.Text = Confirm[0].Transaction_Id.ToString();
                    }
                    ServiceClient.Close();

                    //PG 
                    if (rbl_CardType.SelectedValue == "IDBI")
                    {
                        Response.Redirect("../Payment/Idbi/Default.aspx?type=idbi&transid=" + LblTransId.Text.ToString() + "_" + LblType.Text.ToString() + "&amt=" + LblAmount.Text.ToString() + "&show=" + LblType.Text.ToString());
                    }
                    else if (rbl_CardType.SelectedValue == "AMEX")
                    {                        
                        Response.Redirect("../Payment/Amex/Default.aspx?type=amex&transid=" + LblTransId.Text.ToString() + "&amt=" + LblAmount.Text.ToString() + "&show=" + LblType.Text.ToString());
                    }
                    else if (rbl_CardType.SelectedValue == "HDFC")
                    {
                        Response.Redirect("../Payment/HDFC/Default.aspx?type=hdfc&transid=" + LblTransId.Text.ToString() + "&amt=" + LblAmount.Text.ToString() + "&show=" + LblType.Text.ToString());
                    }
                    // end PG
                }
            }
        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                InsertTransDetails();               
            }
            catch (Exception ex)
            {
                LblError.Text = ex.Message.ToString();
            }   
        }
    }
  
}