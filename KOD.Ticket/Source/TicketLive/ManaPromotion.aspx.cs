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


public partial class ManaPromotion : System.Web.UI.Page
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
        Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("MANA promotion start");
    }
    private void PromotionNotValid()
    {
        lblMsgPromotionCode.Visible = true;
        lblMsgPromotionCode.Text = "Promotion Code is not Valid";
        //txtMANAPromotionCode.Text = "";
        //txtMANAPromotionCode.Focus();
    }
    protected void btnMANAPromotionCode_Click(object sender, EventArgs e)
    {
        decimal TotalAmount = 0; decimal PayableAmount = 0;
        string pack = ddl_package.SelectedValue.ToString();
        List<KoDTicketingLibrary.DTO.Promotion> listPromo = VistaBOL.GetPromostionCode();
       // String PromotionCode = txtMANAPromotionCode.Text;
        int list = listPromo.Count;
        int select = int.Parse(ddl_noofpackage.SelectedValue.ToString());

        if (ddl_package.SelectedItem.ToString() == "Select" && ddl_noofpackage.SelectedItem.ToString() == "Select")
        {
            lblMsgPromotionCode.Visible = true;
            lblMsgPromotionCode.Text = "Please select any Family Package and the number of Packages";
            ddl_package.Focus();
            return;
        }

        if (ddl_package.SelectedItem.ToString() == "Select")
        {
            lblMsgPromotionCode.Visible = true;
            lblMsgPromotionCode.Text = "Please select any Family Package";
            ddl_package.Focus();
            return;
        }
        if (ddl_noofpackage.SelectedItem.ToString()=="Select")
        {
            lblMsgPromotionCode.Visible = true;
            lblMsgPromotionCode.Text = "Please select number of Packages";
            ddl_noofpackage.Focus();
            return;
        }
        else
        {
            for (int i = 0; i < list; i++)
            {
                if (listPromo[i].PromotionCode.ToString().ToUpper() == "MANA")
                {
                    if (ddl_package.SelectedItem.ToString() == "Weekday,Rs.3999")
                    {
                        PayableAmount = Convert.ToDecimal("3999") * Convert.ToDecimal(ddl_noofpackage.SelectedValue);
                        Session["PackageType"] = "Family Package Weekdays";
                    }
                    if (ddl_package.SelectedItem.ToString() == "Weekend,Rs.4999")
                    {
                        PayableAmount = Convert.ToDecimal("4999") * Convert.ToDecimal(ddl_noofpackage.SelectedValue);
                        Session["PackageType"] = "Family Package Weekend";
                    }
                    Session["Package"] = ddl_package.SelectedItem.ToString();
                    //Session["Package"] = ddl_package.SelectedItem.ToString();
                    Session["PromotionCode"] = listPromo[i];
                    Session[listPromo[i].PromotionCode] = listPromo[i];
                    Session["a" + listPromo[i].PromotionCode] = listPromo[i];
                    Session["dtt"] = listPromo[i].EndDate.ToString();
                    Session["Hotel"] = listPromo[i].PromotionCode.ToString();
                    //Session["PayableAmount"] = PayableAmount;
                    Session["NoofPackages"] = ddl_noofpackage.SelectedValue.ToString();
                    Response.Redirect("Default.aspx?MANA=ManaPromo&promo="+Encrypt(listPromo[i].PromotionCode), false);
                }
            }
            PromotionNotValid();
        }
    }
}