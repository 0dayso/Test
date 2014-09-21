using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace RoyalWebApp.Payment.Idbi
{
    public partial class Default : System.Web.UI.Page
    {
        protected String transAmount;
        protected String transshowname;
        protected String transid;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["type"] != null)
            {
                if (Request.QueryString["type"] != "")
                {
                    double amt = (double.Parse(Request.QueryString["amt"].ToString())) * 100;
                    transAmount = amt.ToString();
                    transid = Request.QueryString["transid"].ToString();
                    transshowname = Request.QueryString["show"].ToString();
                }
            }
            ////Updated On May 04 ,2011
            //if (Session["PayDetailsTemp"] != null)
            //{
            //    string[] PayDetails = Session["PayDetailsTemp"].ToString().Split('|');
            //    double amt = (double.Parse(PayDetails[2])) * 100;
            //    transAmount = amt.ToString();
            //    transid = PayDetails[1];
            //    transshowname = PayDetails[3];
            //}
        }
    }
}
