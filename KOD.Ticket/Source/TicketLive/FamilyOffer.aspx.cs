using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KoDTicketing.BusinessLayer;
using KoDTicketingLibrary.DTO;
using System.Text.RegularExpressions;
using KoDUtilities;
using System.Data;

public partial class HotelsPromotion : System.Web.UI.Page
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
        Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Family Offer start");
         string enddt = "Thu, Apr 10,2014";
         if (DateTime.Now.Date > DateTime.Parse(enddt))
         {
             textroyalinfo.Visible = false;
             ddl_noofpackage.Items.Clear();
             ddl_package.Items.Clear();
             ddl_noofpackage.Items.Add("Select");
             ddl_package.Items.Add("Select");
         }
    }
    private void OfferNotValid()
    {
        lblMsgPromotionCode.Visible = true;
        lblMsgPromotionCode.Text = "Royal Card No is not Valid";
        textroyalinfo.Focus();
        return;
    }
    protected void btnvalidation_Click(object sender, EventArgs e)
    {
        DataTable dtroyalinfo = TransactionBOL.selectifo_royal(textroyalinfo.Text);

        if (textroyalinfo.Text == "")
        {
            lblMsgPromotionCode.Visible = true;
            lblMsgPromotionCode.Text = "Please enter Royal Card No. or Mobile No.";
            textroyalinfo.Focus();
            return;
            //ClientScript.RegisterStartupScript(typeof(Page), "test", "alert('Please enter Royal Card No. or Mobile No.');", true);
        }
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
        if (ddl_noofpackage.SelectedItem.ToString() == "Select")
        {
            lblMsgPromotionCode.Visible = true;
            lblMsgPromotionCode.Text = "Please select number of Packages";
            ddl_noofpackage.Focus();
            return;
        }
        else if (dtroyalinfo.Rows.Count > 0)
        {
            Session["royalno"] = textroyalinfo.Text;
            decimal TotalAmount = 0; decimal PayableAmount = 0;
            List<KoDTicketingLibrary.DTO.Promotion> listPromo = VistaBOL.GetPromostionCode();
            int list = listPromo.Count;
            string pack = ddl_package.SelectedValue.ToString();
            int select = int.Parse(ddl_noofpackage.SelectedValue.ToString());
            for (int i = 0; i < list; i++)
            {
                if (listPromo[i].PromotionCode.ToString().ToUpper() == "FAMILYOFFER")
                {
                    if (Session["royalno"].ToString() != null && Session["royalno"].ToString() != "")
                    {
                        if (ddl_package.SelectedItem.ToString() == "Weekday-Rs.9796")
                        {
                            PayableAmount = Convert.ToDecimal("4999") * Convert.ToDecimal(ddl_noofpackage.SelectedValue);
                            Session["PackageType"] = "Family Package Weekdays";
                        }
                        if (ddl_package.SelectedItem.ToString() == "Weekend-Rs.13196")
                        {
                            PayableAmount = Convert.ToDecimal("7999") * Convert.ToDecimal(ddl_noofpackage.SelectedValue);
                            Session["PackageType"] = "Family Package Weekend";
                        }
                        Session["Package"] = ddl_package.SelectedItem.ToString();
                        Session["PromotionCode"] = listPromo[i];
                        Session[listPromo[i].PromotionCode] = listPromo[i];
                        Session["a" + listPromo[i].PromotionCode] = listPromo[i];
                        Session["dtt"] = listPromo[i].EndDate.ToString();
                        Session["Hotel"] = listPromo[i].PromotionCode.ToString();
                        Session["NoofPackages"] = ddl_noofpackage.SelectedValue.ToString();
                        Response.Redirect("Default.aspx?FAMILYOFFER=FamilyOffer&promo=" + Encrypt(listPromo[i].PromotionCode), false);
                    }
                }
            }   
        }
        else
        {
            OfferNotValid();
            //ClientScript.RegisterStartupScript(typeof(Page), "test", "alert('Royal Card No. or Mobile No. not matched.');", true);
        }
    }
}
    