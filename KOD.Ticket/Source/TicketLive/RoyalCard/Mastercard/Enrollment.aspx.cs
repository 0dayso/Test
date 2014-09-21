using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KoDTicketing.BusinessLayer;
using KoDTicketing.DataAccessLayer;
using System.Data;

public partial class RoyalCard_Mastercard_Enrollment : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            grayBG.Visible = false;
            showcontainer.Visible = false;
            Container.Visible = false;
            lblmsg.Visible = false;
            DataTable dtbankname = TransactionBOL.Select_BankName();
         if (dtbankname != null && dtbankname.Rows.Count > 0)
         {
            foreach (DataRow dr in dtbankname.Rows)
                ddlbankname.Items.Add(new ListItem(dr[0].ToString(), dr[0].ToString()));
         }
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (Txtcardno.Text=="")
        {
            lblmsg.Visible = true;
            lblmsg.Text = "Please Enter Six Digits of Your World Card-Master Card.";
            Txtcardno.Focus();
            return;
        }
        if (Txtcardno.Text.Length<6)
        {
            lblmsg.Visible = true;
            lblmsg.Text = "Please Enter Six Digits of Your World Card-Master Card.";
            Txtcardno.Focus();
            return;
        }
        if (ddlbankname.SelectedItem.ToString() == "Select Bank Name")
        {
            lblmsg.Visible = true;
            lblmsg.Text = "Please select Bank Name";
            ddlbankname.Focus();
            return;
        }
        else 
        {
            DataTable dtvalidation = TransactionBOL.Validation(int.Parse(Txtcardno.Text));
            if (dtvalidation != null && dtvalidation.Rows.Count > 0)
            {
                foreach (DataRow dr in dtvalidation.Rows)
                {
                    if (dr[0].ToString() == ddlbankname.SelectedItem.ToString())
                    {
                        Session["cardno"] = Txtcardno.Text;
                        Session["BankName"] = ddlbankname.SelectedItem;
                        Session["CardType"] = "Platinum";
                        Response.Redirect("Details.aspx");
                    }
                    else
                    {
                        lblmsg.Visible = true;
                        lblmsg.Text = "Either Card Number or Bank name is incorrect.";
                        Txtcardno.Focus();
                        return;
                    }
                }
            }
            else
            {
                grayBG.Visible = true;
                showcontainer.Visible = true;
                Container.Visible = true;
            }
        
        }
    }
    protected void btnclose_Click(object sender, EventArgs e)
    {
        grayBG.Visible = false;
        showcontainer.Visible = false;
        Container.Visible = false;
        Txtcardno.Text = "";
        lblmsg.Visible = false;
    }
    //protected void btnproceed_Click(object sender, EventArgs e)
    //{
    //    Session["cardno"] = Txtcardno.Text;
    //    Session["BankName"] = ddlbankname.SelectedItem;
    //    Session["CardType"] = "Purple";
    //    Response.Redirect("Details.aspx");
    //}
}