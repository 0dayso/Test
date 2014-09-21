using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RoyalWebApp.Payment.HDFC
{
    public partial class Print_Receipt : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["TransactionId"]!="")
            {
                receipt.Visible = true;
                lblisSuccessful.Text = "Payment Successful";
                lbltype.Text = Request.QueryString["Type"].ToString();
                lblAmount.Text = Request.QueryString["Amount"].ToString();
                lbltransID.Text = Request.QueryString["TransactionId"].ToString();
            }
        }
    }
}