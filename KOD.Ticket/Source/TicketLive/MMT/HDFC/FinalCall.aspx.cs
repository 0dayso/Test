using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MMT_HDFC_FinalCall : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        string IpAddress = System.Configuration.ConfigurationManager.AppSettings["KoDTicketingIPAddress"];
        try
        {
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Payment Session Completed.");
            Session.Abandon();
        }
        catch (Exception ex)
        {
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Payment_FinalCall: " + ex.Message);
        }

        Response.Write("<script> location.replace('" + IpAddress + "Payment/Print-Receipt.aspx?b=" + Request.QueryString["b"] + " '); </script>");
        //ClientScript.RegisterStartupScript(GetType(), "MyScript", "<script> location.replace('" + IpAddress + "Payment/Print-Receipt.aspx?b=" + Request.QueryString["b"].ToString() + " '); </script>");

    }
}