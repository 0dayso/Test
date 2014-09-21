using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports_Report_Single : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["UserAuthentication"] == null)
                Response.Redirect("~/Admin/Default.aspx", false);
        }
        catch (Exception ex)
        {
        }
    }

    protected void btn_Logout_Click(object sender, EventArgs e)
    {
        try
        {
            Session["UserAuthentication"] = null;
            Session.Abandon();
            Session.RemoveAll();
            Response.Redirect("~/Admin/Default.aspx", false);
        }
        catch (Exception ex)
        {
        }
    }
}
