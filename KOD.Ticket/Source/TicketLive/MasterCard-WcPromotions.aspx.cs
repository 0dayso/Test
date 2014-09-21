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

public partial class MasterCardPromotions : System.Web.UI.Page
{
    public string Decrypt(string val)
    {
        val = val.Replace(" ", "+");
        var bytes = Convert.FromBase64String(val);
        var encBytes = System.Security.Cryptography.ProtectedData.Unprotect(bytes, new byte[0], System.Security.Cryptography.DataProtectionScope.LocalMachine);
        return System.Text.Encoding.UTF8.GetString(encBytes);
    }
    public string Encrypt(string val)
    {
        var bytes = System.Text.Encoding.UTF8.GetBytes(val);
        var encBytes = System.Security.Cryptography.ProtectedData.Protect(bytes, new byte[0], System.Security.Cryptography.DataProtectionScope.LocalMachine);
        return Convert.ToBase64String(encBytes);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Master card World Card promotion start.");
        if (!IsPostBack)
        {
            lblMsgPromotionCode.Visible = false;
            DataTable dtbankname = TransactionBOL.Select_BankName();
            if (dtbankname != null && dtbankname.Rows.Count > 0)
            {
                foreach (DataRow dr in dtbankname.Rows)
                    ddlbankname.Items.Add(new ListItem(dr[0].ToString(), dr[0].ToString()));
            }
        }

    }
    protected void btnMCPromotionCode_Click(object sender, EventArgs e)
    {
        if (Txtcardno.Text == "")
        {
            lblMsgPromotionCode.Visible = true;
            lblMsgPromotionCode.Text = "Please Enter Six Digits of Your World Card-Master Card.";
            Txtcardno.Focus();
            return;
        }
        if (Txtcardno.Text.Length < 6)
        {
            lblMsgPromotionCode.Visible = true;
            lblMsgPromotionCode.Text = "Please Enter Six Digits of Your World Card-Master Card.";
            Txtcardno.Focus();
            return;
        }
        if (ddlbankname.SelectedItem.ToString() == "Select Bank Name")
        {
            lblMsgPromotionCode.Visible = true;
            lblMsgPromotionCode.Text = "Please select Bank Name";
            ddlbankname.Focus();
            return;
        }
        if (Txtpromotioncode.Text == "")
        {
            lblMsgPromotionCode.Visible = true;
            lblMsgPromotionCode.Text = "Please Enter Promotion Code.";
            Txtpromotioncode.Focus();
            return;
        }
        if (chkterms.Checked==false)
        {
            lblMsgPromotionCode.Visible = true;
            lblMsgPromotionCode.Text = "Please Accept Terms and Condition.";
            chkterms.Focus();
            return;
        }
      
        else
        {
            List<KoDTicketingLibrary.DTO.Promotion> listPromo = VistaBOL.GetPromostionCode();
            String PromotionCode = Txtpromotioncode.Text;
            int list = listPromo.Count;
            bool istrue = false;
            DataTable dtvalidation = TransactionBOL.Validation(int.Parse(Txtcardno.Text));
            for (int i = 0; i < list; i++)
            {
                bool isMatchPromo = Regex.IsMatch(PromotionCode, listPromo[i].RegexValidator);
                if (isMatchPromo)
                {
                    if (listPromo[i].PromotionCode.ToString().ToUpper() == "MCWORLD")
                    {
                        Session["PromotionCode"] = listPromo[i];
                        Session[listPromo[i].PromotionCode] = listPromo[i];
                        Session["a" + listPromo[i].PromotionCode] = listPromo[i];
                        Session["Hotel"] = listPromo[i].PromotionCode.ToString();
                        istrue = true;
                    }
                }
            }
            if(istrue==false)
            {
                    lblMsgPromotionCode.Visible = true;
                    lblMsgPromotionCode.Text = "Promotion Code is not Valid.";
                    Txtpromotioncode.Focus();
                    return;
            }
            if (dtvalidation != null && dtvalidation.Rows.Count > 0)
            {
                foreach (DataRow dr in dtvalidation.Rows)
                {
                    if (dr[0].ToString() == ddlbankname.SelectedItem.ToString())
                    {
                        Session["worldcardpromocode"] = Txtpromotioncode.Text.ToString();
                        Session["MCBANKNAME"] = ddlbankname.SelectedItem.ToString();
                        Session["MCCARDNO"] = Txtcardno.Text.ToString();
                        Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Card no:"+Txtcardno.ToString());
                        Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("bank name:"+ddlbankname.SelectedItem.ToString());
                        Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("redirect to default page.");
                        Response.Redirect("Default.aspx?WMC=S&promo=" + Encrypt("MCWORLD"), false);
                    }
                    else
                    {
                        lblMsgPromotionCode.Visible = true;
                        lblMsgPromotionCode.Text = "Either Card Number or Bank name is incorrect.";
                        Txtcardno.Focus();
                        return;
                    }
                }
            }
            else
            {
                lblMsgPromotionCode.Visible = true;
                lblMsgPromotionCode.Text = "Sorry, your card is not eligible for this Offer.";
                Txtcardno.Focus();
                return;
            }
        }
    }
}