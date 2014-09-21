using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using KoDTicketing.BusinessLayer;

public partial class MariottPromotion : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsgPromotionCode.Visible = false;
    }
    protected void btnPromotionCode_Click(object sender, EventArgs e)
    {
        List<KoDTicketingLibrary.DTO.Promotion> listPromo = VistaBOL.GetPromostionCode();
        String PromotionCode = txtPromotionCode.Text;
        Int16 CampaignCounter = 1;
        foreach (KoDTicketingLibrary.DTO.Promotion item in listPromo)
        {
            if (txtPromotionCode.Text.ToString().Length == 0)
            {
                lblMsgPromotionCode.Visible = true;
                lblMsgPromotionCode.Text = "Please Enter Your Promotion Code";
                txtPromotionCode.Focus();
            }
            else
            {
                bool isMatch = Regex.IsMatch(PromotionCode, "(?![M,m]+[R,r]+[T,t][0]{5})^([M|m][R|r][T|t])[0-9]{5}$|^([M|m][R|r][T||t])[9]{5}$");
                if (isMatch)
                {
                    item.WebPromotionId = txtPromotionCode.Text.ToUpper();
                    Session["PromotionCode"] = item;
                    Response.Redirect("Default.aspx");
                }
                else
                {
                    if (CampaignCounter == listPromo.Count)
                    {
                        lblMsgPromotionCode.Visible = true;
                        lblMsgPromotionCode.Text = "Promotion Code is not Valid";
                        txtPromotionCode.Text = "";
                        txtPromotionCode.Focus();
                    }
                    else
                    {
                        CampaignCounter++;
                    }
                }
            }
        }
    }
}