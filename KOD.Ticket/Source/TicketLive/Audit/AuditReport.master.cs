using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Audit_AuditReport1 : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserAuthentication"] == null)
            Server.Transfer("~/Admin/Default.aspx", false);
    }
    protected void btn_Logout_Click(object sender, EventArgs e)
    {
        Session["UserAuthentication"] = null;
        Session.Abandon();
        Session.RemoveAll();
        Server.Transfer("~/Admin/Default.aspx", false);
    }
}
