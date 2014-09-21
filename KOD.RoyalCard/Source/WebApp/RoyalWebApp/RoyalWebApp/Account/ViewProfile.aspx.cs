using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace RoyalWebApp.Account
{   /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public partial class ViewProfile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["RegId"] == null || Session["RegId"] == "")
            {
                Response.Redirect("Login.aspx");
            }
            else
            {
                BindData();
            }
        }

        void BindData()
        {
            EntityServiceReference.EntityServiceClient ServiceClient = new EntityServiceReference.EntityServiceClient();
            ServiceClient.Open();
            if (Session["RegId"] != null && Session["RegId"] != "")
            {
                var arr = ServiceClient.GetUserDetails(Session["RegId"].ToString());
                if (arr.Length > 0)
                {
                    LblMemberId.Text = arr[0].MemberID.ToString();
                    LblName.Text = arr[0].FirstName.ToString() + " " + arr[0].LastName.ToString();
                    LblAddress.Text = arr[0].Address.ToString();
                    LblMobileNo.Text = arr[0].Mobile.ToString();
                    LblDob.Text = arr[0].DOB.ToString();
                    LblAnniversary.Text = arr[0].AnniversaryDate.ToString();
                    LblDesignation.Text = arr[0].Designation.ToString();
                    LblEmailId.Text = arr[0].Email.ToString();
                    LblStatus.Text = arr[0].MaritalStatus.ToString();
                }
            }         
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            Response.Redirect("EditProfile.aspx");
        }
    }
}