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

public partial class WorldCardOthers : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Master card promotion(non world card start.)");
        if (!IsPostBack)
        {
            lblMsgPromotionCode.Visible = false;
            DataTable dtbankname = TransactionBOL.Select_BankNamenonwc();
            if (dtbankname != null && dtbankname.Rows.Count > 0)
            {
                foreach (DataRow dr in dtbankname.Rows)
                    ddlbankname.Items.Add(new ListItem(dr[0].ToString(), dr[0].ToString()));
            }
        }
    }
    //private void PromotionNotValid()
    //{
    //    lblMsgPromotionCode.Visible = true;
    //    lblMsgPromotionCode.Text = "Promotion Code is not Valid";
    //    txtMCPromotionCode.Text = "";
    //    txtMCPromotionCode.Focus();
    //}
    public string Encrypt(string val)
    {
        var bytes = System.Text.Encoding.UTF8.GetBytes(val);
        var encBytes = System.Security.Cryptography.ProtectedData.Protect(bytes, new byte[0], System.Security.Cryptography.DataProtectionScope.LocalMachine);
        return Convert.ToBase64String(encBytes);
    }
    protected void btnMCPromotionCode_Click(object sender, EventArgs e)
    {

        if (Txtcardno.Text == "" || (Txtcardno.Text.Length < 6))
        {
            lblMsgPromotionCode.Visible = true;
            lblMsgPromotionCode.Text = "Please Enter Six Digits of Your Master Card.";
            Txtcardno.Focus();
            return;
        }
        if (ddlbankname.SelectedValue.ToString() == "Select Bank Name")
        {
            lblMsgPromotionCode.Visible = true;
            lblMsgPromotionCode.Text = "Please select Bank Name";
            ddlbankname.Focus();
            return;
        }
        if (txtMCPromotionCode.Text.Length == 0)
        {
            lblMsgPromotionCode.Visible = true;
            lblMsgPromotionCode.Text = "Please Enter Promotion Code";
            txtMCPromotionCode.Focus();
            return;
        }
        if (chkterms.Checked == false)
        {
            lblMsgPromotionCode.Visible = true;
            lblMsgPromotionCode.Text = "Please Accept Terms and Condition.";
            chkterms.Focus();
            return;
        }
        else
        {
            List<KoDTicketingLibrary.DTO.Promotion> listPromo = VistaBOL.GetPromostionCode();
            String PromotionCode = txtMCPromotionCode.Text;
            int list = listPromo.Count;
            bool istrue = false;
            DataTable dtvalidation = TransactionBOL.Validationnonwc(int.Parse(Txtcardno.Text));
            for (int i = 0; i < list; i++)
            {
                bool isMatchPromo = Regex.IsMatch(PromotionCode, listPromo[i].RegexValidator);
                if (isMatchPromo)
                {
                    if (listPromo[i].PromotionCode.ToString().ToUpper() == "MCOTHERS")
                    {
                        //Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Promotion code:" + txtMCPromotionCode.Text.ToString());
                        //Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Redirect to MasterCard-NwcDetail page.");
                        //Session["MCPROMOCODE"] = txtMCPromotionCode.Text.ToString();
                        Session["PromotionCode"] = listPromo[i];
                        Session[listPromo[i].PromotionCode] = listPromo[i];
                        Session["a" + listPromo[i].PromotionCode] = listPromo[i];
                        Session["Hotel"] = listPromo[i].PromotionCode.ToString();
                        istrue = true;
                        //   Response.Redirect("MasterCard-NwcDetails.aspx");
                    }
                    if (istrue == false)
                    {
                        lblMsgPromotionCode.Visible = true;
                        lblMsgPromotionCode.Text = "Promotion Code is not Valid.";
                        txtMCPromotionCode.Focus();
                        return;
                    }
                    if (dtvalidation != null && dtvalidation.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtvalidation.Rows)
                        {
                            if (dr[0].ToString() == ddlbankname.SelectedItem.ToString())
                            {
                                Session["MCPROMOCODE"] = txtMCPromotionCode.Text.ToString();
                                Session["NWTMCBANKNAME"] = ddlbankname.SelectedItem.ToString();
                                Session["NWTMCCARDNO"] = Txtcardno.Text.ToString();
                                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Card no:" + Txtcardno.ToString());
                                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("bank name:" + ddlbankname.SelectedItem.ToString());
                                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("redirect to default page.");
                                Response.Redirect("Default.aspx?NWMCT=S&promo=" + Encrypt("MCOTHERS"), false);
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
    }
    //PromotionNotValid();
}
