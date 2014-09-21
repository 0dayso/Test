using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RoyalWebApp.Skins.UC
{
    public partial class UCLogin : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void BtnSubmit_Click(object sender, ImageClickEventArgs e)
        {
            EntityServiceReference.EntityServiceClient ServiceRefClient = new EntityServiceReference.EntityServiceClient();
            ServiceRefClient.Open();
            var arr = ServiceRefClient.GetUserLogin(txtMemberId.Text.ToString(), txtPassword.Text.ToString());
            if (arr.Length > 0)
            {
                if (arr[0].Value.ToString().Equals("2"))
                {
                    Session["IsLogged"] = "1";
                    Session["RegId"] = arr[0].ERPMemberId.ToString();
                    Response.Redirect("UserCard.aspx");
                    
                }
                else
                {
                    lblMsg.Visible = true;
                    lblMsg.Text = "Invalid Member Id or Password";
                }
            }
            ServiceRefClient.Close();
        } 
        
    }
}