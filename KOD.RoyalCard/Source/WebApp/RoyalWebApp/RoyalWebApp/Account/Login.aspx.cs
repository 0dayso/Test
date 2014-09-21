using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RoyalWebApp.Account
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            //EntityServiceReference.EntityServiceClient ServiceRefClient = new EntityServiceReference.EntityServiceClient();
            //ServiceRefClient.Open();
            //var arr = ServiceRefClient.GetUserLogin(txtMemberId.Text.ToString(), txtPassword.Text.ToString());
            //if (arr.Length > 0)
            //{
            //    if (arr[0].Value.ToString().Equals("2"))
            //    {
            //        Session["IsLogged"] = "1";
            //        Session["RegId"] = arr[0].ERPMemberId.ToString();
            //        Response.Redirect("UserCard.aspx");
            //    }
            //    else
            //    {
                  
            //     ClientScript.RegisterStartupScript(typeof(Page), "test", "alert('Enter Correct MembershipId & Password');",true);
                     
            //        //lblMsg.Visible = true;
            //        //lblMsg.Text = "Invalid Member Id or Password";
            //    }
            //}
            //ServiceRefClient.Close();
        }
    }
}