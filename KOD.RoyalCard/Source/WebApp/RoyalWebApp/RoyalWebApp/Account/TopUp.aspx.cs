using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RoyalWebApp.Account
{
    public partial class TopUp : System.Web.UI.Page
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
        protected void BtnRecharge_Click(object sender, EventArgs e)
        {
            //go to pg page
            Response.Redirect("PGDetails.aspx?Type=topup&amt=" + txtAmount.Text.ToString());
        }

        void BindData()
        {
            EntityServiceReference.EntityServiceClient ServiceClient = new EntityServiceReference.EntityServiceClient();
            ServiceClient.Open();
            if (Session["RegId"] != null && Session["RegId"] != "")
            {
                LblMemberId.Text = Session["RegId"].ToString();
                var arr = ServiceClient.GetCardsBalanceByMemberId(Session["RegId"].ToString());
                if (arr.Length > 0)
                {
                    LblRemainingPoints.Text = arr[0].RemainingPoints.ToString();
                    LblRemainingAmount.Text = arr[0].RemainingAmount.ToString();
                    LblAfter24Points.Text = arr[0].After24HourPoints.ToString();
                }
            }
            ServiceClient.Close();
        }
    }
}