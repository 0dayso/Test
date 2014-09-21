using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Runtime.Remoting.Messaging;

public partial class CheckStatus : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Clear();
        AsyncResult ar = (AsyncResult)Session["result"];
        if (ar.IsCompleted)
            Response.Write("1");
        else
            Response.Write("0");
        Response.End();
    }
}
