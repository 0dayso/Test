using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RoyalWebApp.Skins.Master
{
    public partial class AccMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["IsLogged"] != null && Session["IsLogged"] != "")
            {
                EntityServiceReference.EntityServiceClient ServiceRefClient = new EntityServiceReference.EntityServiceClient();
                ServiceRefClient.Open();
                var arr = ServiceRefClient.GetUserDetails(Session["RegId"].ToString());

                LblUserName.Text = "Hi " + arr[0].FirstName.ToString() + " " + arr[0].LastName.ToString() + " [<a href='Logout.aspx'>Sign Out</a> ]";
                ServiceRefClient.Close();
            }
            else
            {
                LblUserName.Text = "";
            }
        }
    }
}