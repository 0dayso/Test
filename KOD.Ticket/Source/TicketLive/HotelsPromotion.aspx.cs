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
    }
    private void PromotionNotValid()
    {
        lblMsgPromotionCode.Visible = true;
        lblMsgPromotionCode.Text = "Promotion Code is not Valid";
        txtHtlPromotionCode.Text = "";
        txtHtlPromotionCode.Focus();
    }
    protected void btnHtlPromotionCode_Click(object sender, EventArgs e)
    {
        List<KoDTicketingLibrary.DTO.Promotion> listPromo = VistaBOL.GetPromostionCode();
        HotelList<string> PromoClass = new HotelList<string>();
        List<string> Promotions = PromoClass.listofHotels();
        string PromotionCode = txtHtlPromotionCode.Text;
            int list = listPromo.Count;
            for (int i = 0; i < list; i++)
            {
                if (Promotions.Contains(listPromo[i].PromotionCode.ToString().ToUpper())) //listPromo[i].PromotionCode.ToString().ToUpper()
                    {
                        bool isMatch = Regex.IsMatch(PromotionCode, listPromo[i].RegexValidator);
                        if (isMatch)
                        {
                            listPromo[i].WebPromotionId = txtHtlPromotionCode.Text.ToUpper();
                            Session["PromotionCode"] = listPromo[i];
                            Session[listPromo[i].PromotionCode] = listPromo[i];
                            Session["a"+listPromo[i].PromotionCode] = listPromo[i];
                            Session["Hotel"] = listPromo[i].PromotionCode.ToString();
                            Response.Redirect("Default.aspx?Hotel=s&promo="+Encrypt(listPromo[i].PromotionCode));
                        }
                    }
            }
        PromotionNotValid();
    }
    //public List<string> Promotions { get; set; }
}

            