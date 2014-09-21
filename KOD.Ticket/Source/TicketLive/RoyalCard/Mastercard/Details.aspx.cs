using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KoDTicketing.BusinessLayer;
using System.Data;
using KoDTicketing.Utilities;
using System.Configuration;

public partial class RoyalCard_Mastercard_Detail : System.Web.UI.Page
{
    static string CardNo;
    static string BankName;
    static string CardType;
    //string[] CardNo = new string[3];
    //string[] BankName = new string[3];
    //string[] CardType = new string[3];
    protected void Page_Load(object sender, EventArgs e)
    {
        grayBG.Visible = false;
        showcontainer.Visible = false;
        Container.Visible = false;
        if(CardNo==null||CardNo=="")
        CardNo=Session["cardno"].ToString();
        if (BankName == null || BankName == "")
        BankName=Session["BankName"].ToString();
        if (CardType == null || CardType == "")
        CardType = Session["CardType"].ToString();
        if (!IsPostBack)
        {
            trAnn.Visible = false;
            trAnn1.Visible = false;
            trAnn2.Visible = false;
            for (int i = 1990; i <= 2013; i++)
            {
                ddlyear.Items.Add(i.ToString());
                DropDownList3.Items.Add(i.ToString());
            }
        }
        //Session.Abandon();
    }
    public void clear()
    {
        txtFirstName.Text = "";
        txtLastName.Text = "";
        txtAddress.Text = "";
        txtCity.Text = "";
        DdlCountry.Text = "";
        txtEmailId.Text = "";
        txtpin.Text = "";
        txtMobileNo.Text = "";
    }
    string msg;
    public bool v()
    {
        if (ddlday.SelectedValue.ToString() == "DD")
        {
            ClientScript.RegisterStartupScript(typeof(Page), "test", "alert('Please Select Date on Date Of Birth');", true);
            ddlday.Focus();
            return false;
         }
        if (ddlmonth.SelectedValue.ToString() == "MM")
        {
            ClientScript.RegisterStartupScript(typeof(Page), "test", "alert('Please Select Month on Date Of Birth');", true);
            ddlmonth.Focus();
            return false;
        }
        if (ddlyear.SelectedValue.ToString() == "YYYY")
        {
            ClientScript.RegisterStartupScript(typeof(Page), "test", "alert('Please Select Year on Date Of Birth');", true);
            ddlyear.Focus();
            return false;
        }
        if (RdMartialStatus.SelectedValue.ToString() != "Married" && RdMartialStatus.SelectedValue.ToString() != "Prefer Not to Say" && RdMartialStatus.SelectedValue.ToString() != "Single")
        {
            ClientScript.RegisterStartupScript(typeof(Page), "test", "alert('Please Select Marital Status');", true);
            RdMartialStatus.Focus();
            return false;
        }
        if (trAnn.Visible)
        {
            if (DropDownList1.SelectedValue.ToString() == "DD")
            {
                ClientScript.RegisterStartupScript(typeof(Page), "test", "alert('Please Select Date on Marriage Anniversary');", true);
                ddlday.Focus();
                return false;
            }
            if (DropDownList2.SelectedValue.ToString() == "MM")
            {
                ClientScript.RegisterStartupScript(typeof(Page), "test", "alert('Please Select Month on Marriage Anniversary');", true);
                ddlmonth.Focus();
                return false;
            }
            if (DropDownList3.SelectedValue.ToString() == "YYYY")
            {
                ClientScript.RegisterStartupScript(typeof(Page), "test", "alert('Please Select Year on Marriage Anniversary');", true);
                ddlyear.Focus();
                return false;
            }

        }
        if (ChkTerms.Checked == false)
        {
            ClientScript.RegisterStartupScript(typeof(Page), "test", "alert('Please Check the Terms And Conditions');", true);
            ChkTerms.Focus();
            return false;
        }
        else
        {
            return true;

        }
        
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        
        v();
        if (v())
        {
            DataTable err = TransactionBOL.insertdetail(RdGender.SelectedItem.ToString(), txtFirstName.Text, txtLastName.Text, txtAddress.Text, txtCity.Text, DdlCountry.Text, txtEmailId.Text, txtpin.Text, txtMobileNo.Text, ddlday.SelectedItem + "-" + ddlmonth.SelectedValue + "-" + ddlyear.SelectedValue, RdMartialStatus.SelectedItem.ToString(), DateTime.Now, CardNo, BankName, CardType, DropDownList1.SelectedItem + "-" + DropDownList2.SelectedValue + "-" + DropDownList3.SelectedValue);
            if (err != null && err.Rows.Count > 0)
            {
                foreach (DataRow dr in err.Rows)
                {
                    msg = dr[0].ToString();
                }
                if (msg != "Sorry, You are already enrolled for Membership")
                {
                    DataTable dt = TransactionBOL.Select_RoyalCardMcDetail_REFIDWISE(txtEmailId.Text, txtMobileNo.Text);
                    ReceiptUtils.RoyalCardMcResponse(dt.Rows[0]);
                    ReceiptUtils.RoyalCardMcResponseloyality(dt.Rows[0], ConfigurationManager.AppSettings.Get("royal"));
                }
            }
            lblmsg.Text = msg;
            grayBG.Visible = true;
            showcontainer.Visible = true;
            Container.Visible = true;
            clear();
            Session.Abandon();
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        CardNo = "";
        CardType = "";
        BankName = "";
        Response.Redirect("http://kingdomofdreams.in/");
    }


    protected void RdMartialStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RdMartialStatus.Items[1].Selected)
        {
            trAnn.Visible = true;
            trAnn1.Visible = true;
            trAnn2.Visible = true;
        }
        else
        {
            trAnn.Visible = false;
            trAnn1.Visible = false;
            trAnn2.Visible = false;
        }
    }
}
