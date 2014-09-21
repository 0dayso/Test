using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KoDTicketing.BusinessLayer;
using KoDTicketing.DataAccessLayer;
using System.Data;
using System.Text.RegularExpressions;

public partial class WorldCardOthersPromotion : System.Web.UI.Page
{
    string url;
    protected void Page_Load(object sender, EventArgs e)
    {
        Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("MasterCard-NwcDetail.Aspx page.");
        if (!IsPostBack)
        {
            url = Request.UrlReferrer.ToString();
            divshow.Visible = false;
            div2.Visible = false;
            DataTable dtbankname = TransactionBOL.Select_BankNamenonwc();
            if (dtbankname != null && dtbankname.Rows.Count > 0)
            {
                foreach (DataRow dr in dtbankname.Rows)
                    ddlbankname.Items.Add(new ListItem(dr[0].ToString(), dr[0].ToString()));
            }
        }
        if (url == "http://localhost/KOD.Web_1/MasterCard-NwcPromotions.aspx")
        {
            Session["PROMOTIONMCOTHERS"] = "MCOTHERS";
        }
        else
        {
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Redirect from other source.");
            Session["PROMOTIONMCOTHERS"] = null;
        }
            lblMsgPromotionCode.Visible = false;
    }
    public bool validation()
    {
        if(Txtcardno.Text==""||(Txtcardno.Text.Length<6))
        {
            ClientScript.RegisterStartupScript(typeof(Page), "test", "alert('Please Enter Six Digits of Your Master Card.');", true);
            Txtcardno.Focus();
            return false;
        }
        if (ddlbankname.SelectedValue.ToString() == "Select Bank Name")
        {
            ClientScript.RegisterStartupScript(typeof(Page), "test", "alert('Please select Bank Name');", true);
            ddl_type.Focus();
            return false;
        }
        DataTable dtvalidation = TransactionBOL.Validationnonwc(int.Parse(Txtcardno.Text));
        if (dtvalidation != null && dtvalidation.Rows.Count > 0)
        {
            bool istrue = false;
            foreach (DataRow dr in dtvalidation.Rows)
            {
                if (dr[0].ToString() == ddlbankname.SelectedItem.ToString())
                {
                    istrue = true;
                }
            }
            if (istrue == false)
            {
                ClientScript.RegisterStartupScript(typeof(Page), "test", "alert('Either Card Number or Bank name is incorrect.');", true);
                ddlbankname.Focus();
                return false;
            }
        }
        if (dtvalidation == null || dtvalidation.Rows.Count == 0)
        {
            ClientScript.RegisterStartupScript(typeof(Page), "test", "alert('Sorry, your card is not eligible for this Offer.');", true);
            ddlbankname.Focus();
            return false;
        }
        if (ddl_type.SelectedValue.ToString() == "Select")
        {
            ClientScript.RegisterStartupScript(typeof(Page), "test", "alert('Please Select Promotion Type');", true);
            ddl_type.Focus();
            return false;
        }
        if (divshow.Visible)
        {
            if (ddlnoofpackage.SelectedValue.ToString() == "Select")
            {
                ClientScript.RegisterStartupScript(typeof(Page), "test", "alert('Please Select No of Package');", true);
                ddlnoofpackage.Focus();
                return false;
            }
        }
        if (chkterms.Checked==false)
        {
            ClientScript.RegisterStartupScript(typeof(Page), "test", "alert('Please Accept Terms and Condition');", true);
            chkterms.Focus();
            return false;
        }
        else
        {
            return true;
        }
    }
    public string Encrypt(string val)
    {
        var bytes = System.Text.Encoding.UTF8.GetBytes(val);
        var encBytes = System.Security.Cryptography.ProtectedData.Protect(bytes, new byte[0], System.Security.Cryptography.DataProtectionScope.LocalMachine);
        return Convert.ToBase64String(encBytes);
    }
    protected void btnMCPromotionCode_Click(object sender, EventArgs e)
    {
        validation();
        if (validation())
        {
            if (ddl_type.SelectedValue.ToString() == "Package" && divshow.Visible == true)
            {
                Session["MCOTHERSNOOFPACKAGE"] = ddlnoofpackage.SelectedItem.ToString();
                Session["NWPMCBANKNAME"] = ddlbankname.SelectedItem.ToString();
                Session["NWPMCCARDNO"] = Txtcardno.Text.ToString();
                Session["Package"] = "5597";
                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Bank name:" + ddlbankname.SelectedValue.ToString());
                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Card no:" + Txtcardno.Text.ToString());
                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("no of Package:" + ddlnoofpackage.SelectedItem.ToString());
                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Redirect to Default Page.");
                Response.Redirect("Default.aspx?NWMCP=S&promo=" + Encrypt("MCOTHERS"), false);
            }
            else
            {
                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Bank name:" + ddlbankname.SelectedValue.ToString());
                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Card no:" + Txtcardno.Text.ToString());
                Session["MCOTHERSNOOFPACKAGE"] = null;
                Session["NWTMCBANKNAME"] = ddlbankname.SelectedItem.ToString();
                Session["NWTMCCARDNO"] = Txtcardno.Text.ToString();
                string type = Encrypt("TICKET");
                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Redirect to Default Page.");
                Response.Redirect("Default.aspx?NWMCT=S&promo=" + Encrypt("MCOTHERS"), false);
            }
        }
     
    }
    protected void ddl_type_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_type.Items[1].Selected)
        {
            divshow.Visible = true;
            div2.Visible = true;
        }
        else 
        {
            divshow.Visible = false;
            div2.Visible = false;
        }
    }
}