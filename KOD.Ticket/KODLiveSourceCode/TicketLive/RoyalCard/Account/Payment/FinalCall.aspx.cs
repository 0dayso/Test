using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Payment_FinalCall : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        string IpAddress = System.Configuration.ConfigurationManager.AppSettings["KoDTicketingIPAddress"];
        try
        {
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Payment Session Completed.");
            Session.Abandon();
        }
        catch(Exception ex)
        {
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Payment_FinalCall: " + ex.Message);
        }

        Response.Write("<script> location.replace('" + IpAddress + "RoyalCard/Account/Payment/Print-Receipt.aspx?b=" + Request.QueryString["b"] + " '); </script>");
        //ClientScript.RegisterStartupScript(GetType(), "MyScript", "<script> location.replace('" + IpAddress + "Payment/Print-Receipt.aspx?b=" + Request.QueryString["b"].ToString() + " '); </script>");
    }


}

//public static class ResponseHelper
//{

//    public static void Redirect(this HttpResponse response,

//        string url,

//        string target,

//        string windowFeatures)
//    {



//        if ((String.IsNullOrEmpty(target) ||

//            target.Equals("_self", StringComparison.OrdinalIgnoreCase)) &&

//            String.IsNullOrEmpty(windowFeatures))
//        {



//            Server.Transfer(url);

//        }

//        else
//        {

//            Page page = (Page)HttpContext.Current.Handler;

//            if (page == null)
//            {

//                throw new InvalidOperationException(

//                    "Cannot redirect to new window outside Page context.");

//            }

//            url = page.ResolveClientUrl(url);



//            string script;

//            if (!String.IsNullOrEmpty(windowFeatures))
//            {

//                script = @"window.open(""{0}"", ""{1}"", ""{2}"");";

//            }

//            else
//            {

//                script = @"window.open(""{0}"", ""{1}"");";

//            }



//            script = String.Format(script, url, target, windowFeatures);

//            ScriptManager.RegisterStartupScript(page,

//                typeof(Page),

//                "Redirect",

//                script,

//                true);

//        }

//    }

//}