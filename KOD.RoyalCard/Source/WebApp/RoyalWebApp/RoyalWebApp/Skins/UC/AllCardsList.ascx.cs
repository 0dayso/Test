using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RoyalWebApp.Skins.UC
{
    public partial class AllCardsList : System.Web.UI.UserControl
    {
        String DivTxt = "";
        protected void Page_Load(object sender, EventArgs e)
        {

        }
       protected void BindData()
        {
            EntityServiceReference.EntityServiceClient ServiceClient=new EntityServiceReference.EntityServiceClient();
                
            ServiceClient.Open();
            if (Session["RegId"] != null && Session["RegId"] != "")
            {
                var arr = ServiceClient.GetAllCardsDetails();
                for (int i = 0; i <= arr.Length-1; i++)
                {
                    DivTxt += "<li><a href='CardListing.aspx?type=" + arr[i].Code.ToString() + "'>" + arr[i].Description.ToString() + "</a></li>";
                }
                    Response.Write(DivTxt.ToString());
            }
        }
    }
}