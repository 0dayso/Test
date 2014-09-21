using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

public partial class Admin_AdminMaster : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.UserAgent.IndexOf("AppleWebKit") > 0)
        {
            Request.Browser.Adapters.Clear();
        }
        fotter.Text = ConfigurationManager.AppSettings["PageTitle"].ToString();
        Page.Title = ConfigurationManager.AppSettings["PageTitle"].ToString() + " - Administrator Panel";
        //if (Session["UserAuthentication"] == null)
          //  Server.Transfer("~/Admin/Default.aspx");        
    }

    protected override void AddedControl(Control control, int index)
    {
        if (Request.ServerVariables["http_user_agent"].IndexOf("Safari", StringComparison.CurrentCultureIgnoreCase) != -1)
            this.Page.ClientTarget = "uplevel";

        base.AddedControl(control, index);
    }
}
