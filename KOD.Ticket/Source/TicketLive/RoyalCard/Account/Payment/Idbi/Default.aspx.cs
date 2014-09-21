using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

public partial class Payment_Idbi_Default : System.Web.UI.Page
{
    protected String transAmount;
    protected String transshowname;
    protected String transid; 

    protected void Page_Load(object sender, EventArgs e)
    {
      
        if (Request.QueryString["type"] != null && Request.QueryString["type"] != "")
        {
            string amtintext = Request.QueryString["amt"] ?? "0";
            double amt = (double.Parse(amtintext)) * 100;
            transAmount = amt.ToString();
            transid = Request.QueryString["transid"] ?? "";
            transshowname = Request.QueryString["show"] ?? "";
        }
    }

}
