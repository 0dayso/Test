using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RoyalWebApp.Account
{
    public partial class PointStatement : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["RegId"] != null && Session["RegId"] != "")
            {
                BindData();
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }
        void BindData()
        {
            EntityServiceReference.EntityServiceClient ServiceClient = new EntityServiceReference.EntityServiceClient();
            ServiceClient.Open();
            if (Session["RegId"] != null && Session["RegId"] != "")
            {
                var arr = ServiceClient.GetPointStatusByMemberId(Session["RegId"].ToString());
                if (arr.Length > 0)
                {
                    GridPointList.DataSource = arr;
                    GridPointList.DataBind();
                }
            }
            ServiceClient.Close();
        }

    }
}