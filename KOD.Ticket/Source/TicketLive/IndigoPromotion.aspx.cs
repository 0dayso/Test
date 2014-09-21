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

public partial class IndigoPromotion : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsgPromotionCode.Visible = false;
    }
    private void PromotionNotValid()
    {
        lblMsgPromotionCode.Visible = true;
        lblMsgPromotionCode.Text = "Either Promotion Code or PNR Number is not Valid";
        txtIndigoPromotionCode.Text = "";
        txtIndigoPNR.Text = "";
        txtIndigoPromotionCode.Focus();
    }
    protected void btnIndigoPromotionCode_Click(object sender, EventArgs e)
    {
        List<KoDTicketingLibrary.DTO.Promotion> listPromo = VistaBOL.GetPromostionCode();
        String PNRnumber = txtIndigoPNR.Text;
        String PromotionCode = txtIndigoPromotionCode.Text;
        int list = listPromo.Count;
        if (txtIndigoPromotionCode.Text.ToString().Length == 0)
        {
            lblMsgPromotionCode.Visible = true;
            lblMsgPromotionCode.Text = "Please Enter Your Promotion Code";
            txtIndigoPromotionCode.Focus();
            return;
        }
        else if (txtIndigoPNR.Text == "")
        {
            lblMsgPromotionCode.Visible = true;
            lblMsgPromotionCode.Text = "Please Enter Your PNR Number";
            txtIndigoPNR.Focus();
            return;
        }
        else
        {
            for (int i = 0; i < list; i++)
            {
                bool isMatchPromo = Regex.IsMatch(PromotionCode, listPromo[i].RegexValidator);
                bool isMatch = Regex.IsMatch(PNRnumber, "^[A-Z a-z]{6}$");
                if (isMatch && isMatchPromo)
                {
                    if (listPromo[i].PromotionCode.ToString().ToUpper() == "INDIGOSL" || listPromo[i].PromotionCode.ToString().ToUpper() == "INDIGODM" || listPromo[i].PromotionCode.ToString().ToUpper() == "INDIGOGL" || listPromo[i].PromotionCode.ToString().ToUpper() == "INDIGOPL")
                    {
                        listPromo[i].WebPromotionId = txtIndigoPromotionCode.Text.ToUpper();
                        Session["PromotionCode"] = listPromo[i];
                        Response.Redirect("Default.aspx?Indigo=s");
                    }
                }
            }
        }
        PromotionNotValid();
    }
}
        //foreach (KoDTicketingLibrary.DTO.Promotion item in listPromo)
        //{
        //    if (txtIndigoPromotionCode.Text.ToString().Length == 0)
        //    {
        //        lblMsgPromotionCode.Visible = true;
        //        lblMsgPromotionCode.Text = "Please Enter Your Promotion Code";
        //        txtIndigoPromotionCode.Focus();
        //        return;
        //    }
        //    else if (txtIndigoPNR.Text == "")
        //        {
        //            lblMsgPromotionCode.Visible = true;
        //            lblMsgPromotionCode.Text = "Please Enter Your PNR Number";
        //            txtIndigoPNR.Focus();
        //            return;
        //        }

        //        else
        //        {
        //            bool isMatchPromo = Regex.IsMatch(PromotionCode, item.RegexValidator);
        //            bool isMatch = Regex.IsMatch(PNRnumber, "^[A-Z a-z]{6}$");
        //            if (isMatch && isMatchPromo)
        //            {
        //                item.WebPromotionId = txtIndigoPromotionCode.Text.ToUpper();
        //                Session["PromotionCode"] = item;
        //                Response.Redirect("Default.aspx");
        //            }
        //            else
        //            {
        //                if (CampaignCounter == listPromo.Count)
        //                {
        //                    lblMsgPromotionCode.Visible = true;
        //                    lblMsgPromotionCode.Text = "Either Promotion Code or PNR Number is not Valid";
        //                    txtIndigoPromotionCode.Text = "";
        //                    txtIndigoPNR.Text = "";
        //                    txtIndigoPromotionCode.Focus();
        //                }
        //                else
        //                {
        //                    CampaignCounter++;
        //                }
        //            }

        //        }
        //}