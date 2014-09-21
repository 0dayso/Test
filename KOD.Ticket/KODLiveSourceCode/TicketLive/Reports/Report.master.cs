using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports_Report : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserAuthentication"] == null)
            Server.Transfer("~/Admin/Default.aspx", false);
        else if (Session["UserAuthentication"].ToString() == "rishabh")
        {
            if (!IsPostBack)
            {
                maenu.Items.Remove(maenu.FindItem("Successful"));
                maenu.Items.Remove(maenu.FindItem("Unsuccessful"));
                maenu.Items.Remove(maenu.FindItem("Summary"));
                maenu.Items.Remove(maenu.FindItem("Detailed Summary"));
                maenu.Items.Remove(maenu.FindItem("NewYearReport"));
                maenu.Items.Remove(maenu.FindItem("Valentines Report"));
                maenu.Items.Remove(maenu.FindItem("BollyLand Report"));
            }
        }
    }

    protected void btn_Logout_Click(object sender, EventArgs e)
    {
        Session["UserAuthentication"] = null;
        Session.Abandon();
        Session.RemoveAll();
        Server.Transfer("~/Admin/Default.aspx", false);
    }
}
