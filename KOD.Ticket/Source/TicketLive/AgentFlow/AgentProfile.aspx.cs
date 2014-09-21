using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AgentFlow_AgentProfile : System.Web.UI.Page
{
    string agent = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        agent = Session["Agent"].ToString();
    }
    protected void LinkButton3_Click(object sender, EventArgs e)
    {
        Response.Redirect("Default.aspx", false);
    }
    protected void LinkButton6_Click(object sender, EventArgs e)
    {
        Session.Clear();
        Response.Redirect("AgentLogin.aspx",false);
    }
}