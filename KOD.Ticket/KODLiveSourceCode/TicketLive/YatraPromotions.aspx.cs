using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using KoDTicketing.BusinessLayer;
using KoDTicketingLibrary.DTO;
using KoDUtilities;

public partial class YatraPromotions : System.Web.UI.Page
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
        lblMsgPromotionCode.Visible = false;
        Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Yatra promotion start");
    }
    private void PromotionNotValid()
    {
        lblMsgPromotionCode.Visible = true;
        lblMsgPromotionCode.Text = "Either Promotion Code or PNR Number is not Valid";
        txtYatraPromotionCode.Text = "";
        txtYatraPNR.Text = "";
        txtYatraPromotionCode.Focus();
    }
    protected void btnMMTPromotionCode_Click(object sender, EventArgs e)
    {
        List<KoDTicketingLibrary.DTO.Promotion> listPromo = VistaBOL.GetPromostionCode();
        string sum = GTICKBOL.YATRAPromotion_check(txtYatraPNR.Text);
        String PNRnumber = txtYatraPNR.Text;
        String PromotionCode = txtYatraPromotionCode.Text;
        int list = listPromo.Count;
        int no = int.Parse(sum);
        int select = int.Parse(ddl_nooftickets.SelectedValue.ToString());
        if (no >= 4)
        {
            lblMsgPromotionCode.Visible = true;
            lblMsgPromotionCode.Text = "Sorry, maximum 4 tickets are allowed on each Booking Id.";
            txtYatraPromotionCode.Text = "";
            txtYatraPNR.Text = "";
            txtYatraPromotionCode.Focus();
            return;

        }
        else if ((no + select) > 4)
        {
            lblMsgPromotionCode.Visible = true;
            lblMsgPromotionCode.Text = "Sorry, you can select only " + (4 - no) + " tickets"; ;
            txtYatraPromotionCode.Text = "";
            txtYatraPNR.Text = "";
            txtYatraPromotionCode.Focus();
            return;

        }
        else if (txtYatraPromotionCode.Text.ToString().Length == 0)
        {
            lblMsgPromotionCode.Visible = true;
            lblMsgPromotionCode.Text = "Please Enter Your Promotion Code";
            txtYatraPromotionCode.Focus();
            return;
        }
        else if (txtYatraPNR.Text == "")
        {
            lblMsgPromotionCode.Visible = true;
            lblMsgPromotionCode.Text = "Please Enter Your PNR Number";
            txtYatraPNR.Focus();
            return;
        }
        else if (chkterms.Checked == false)
        {
            lblMsgPromotionCode.Visible = true;
            lblMsgPromotionCode.Text = "Accept terms and conditions";
            txtYatraPNR.Focus();
            return;
        }
        else
        {

            for (int i = 0; i < list; i++)
            {
                bool isMatchPromo = Regex.IsMatch(PromotionCode, listPromo[i].RegexValidator);
                bool isMatch = Regex.IsMatch(PNRnumber, "(?![Y|y][T|t][0]{11})^([Y|y][T|t])[0-9]{11}$");
                bool isMatch1 = Regex.IsMatch(PNRnumber, "(?![Y|y][T|t][0]{10})^([Y|y][T|t])[0-9]{10}$");
                bool isMatch2 = Regex.IsMatch(PNRnumber, "(?![Y|y][T|t][0]{9})^([Y|y][T|t])[0-9]{9}$");
                bool isMatch3 = Regex.IsMatch(PNRnumber, "(?![Y|y][T|t][0]{8})^([Y|y][T|t])[0-9]{8}$");
                if ((isMatch || isMatch1 || isMatch2 || isMatch3) && isMatchPromo)
                {
                    if (listPromo[i].PromotionCode.ToString().ToUpper() == "YATRA")
                    {
                        Session["PromotionCode"] = listPromo[i];
                        Session[listPromo[i].PromotionCode] = listPromo[i];
                        Session["a" + listPromo[i].PromotionCode] = listPromo[i];
                        Session["Hotel"] = listPromo[i].PromotionCode.ToString();
                        Session["yatrapnr"] = txtYatraPNR.Text.ToUpper();
                        Session["NoofTickets"] = ddl_nooftickets.SelectedItem.ToString();
                        Session["promocode"] = txtYatraPromotionCode.Text.ToUpper();
                        Response.Redirect("Default.aspx?Yatra=S&promo=" + Encrypt(listPromo[i].PromotionCode), false);
                    }
                }
            }
        }
        PromotionNotValid();
    }
}