using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System;
using System.Data;
using KoDTicketing.BusinessLayer;
using KoDTicketing.DataAccessLayer;

public partial class Event : System.Web.UI.MasterPage
{
   
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["mailer"] != null)
            {
                VistaBOL.Event_Mailer_Tracker(GetIP(), DateTime.Now.ToString(), Request.QueryString["mailer"]);
            }
        }
        
    }
         //Get IP Address
    protected string GetIP()
    {
        string ipaddress;
        ipaddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (ipaddress == "" || ipaddress == null)
            ipaddress = Request.ServerVariables["REMOTE_ADDR"];
        return ipaddress;
    }

 
    }

