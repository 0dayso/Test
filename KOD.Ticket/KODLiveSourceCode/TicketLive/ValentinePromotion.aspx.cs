using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KoDTicketing.BusinessLayer;
using System.Text.RegularExpressions;

public partial class ValentinePromotion : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsgPromotionCode.Visible = false;

    }
    private void PromotionNotValid()
    {
        lblMsgPromotionCode.Visible = true;
        lblMsgPromotionCode.Text = "Promotion Code is not Valid";
        txtPromotionCode.Text = "";
        txtPromotionCode.Focus();
    }
  
    protected void btnPromotionCode_Click(object sender, EventArgs e)
    {
        List<KoDTicketingLibrary.DTO.Promotion> listPromo = VistaBOL.GetPromostionCode();
        string PromotionCode = txtPromotionCode.Text;
        int list = listPromo.Count;
        for (int i = 0; i < list; i++)
        {
            if (listPromo[i].PromotionCode == "VALENTINEMONTH")
            {
                bool isMatch = Regex.IsMatch(PromotionCode, listPromo[i].RegexValidator);
                if (isMatch)
                {
                    listPromo[i].WebPromotionId = txtPromotionCode.Text.ToUpper();
                    Session["PromotionCode"] = listPromo[i];
                    Session["Hotel"] = "VALENTINEMONTH";
                    Response.Redirect("Default.aspx?Valentine=s");
                }
                else
                {
                    PromotionNotValid();
                } 
            }
        } 
    }
}
 