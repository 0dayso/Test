using System.Data.SqlClient;
using System.Configuration;
using System.Web.Security;
using System.Web.UI.WebControls;
using KoDTicketing.DataAccessLayer;

public partial class Admin_Default : System.Web.UI.Page
{
    protected void Login_Authenticate(object sender, AuthenticateEventArgs e)
    {
            string username = Login.UserName;
            string pwd = Login.Password;
            string sqlUserName = "SELECT * FROM [tbl_AdminLogin] where username = '" + username + "' and password = '" + pwd + "' and " +
                " [role] = 'admin' and Status = 1";
            System.Data.DataTable dt = Connection.readTab(sqlUserName, DBAccess.connMSTicket);
            if (dt != null && dt.Rows.Count > 0)
            {
                Session["UserAuthentication"] = username;
                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("MIS Launched by:  " + username);
                //if ((username == "kodcc"))
                //{
                //    Response.Redirect("~/Reports/SearchSingle.aspx", false);
                //}
                if ((username == "rishabh") || (username == "marketing"))
                {
                    Response.Redirect("~/Reports/HotelsReport.aspx", false);
                }
                else if (username == "audit_test")
                {
                    Response.Redirect("~/Audit/Audit.aspx", false);
                }
                else if (username == "audit_report")
                {
                    Response.Redirect("~/Audit/AuditReport.aspx", false);
                }
                //else if ((username == "kodqa") || (username == "deepa"))
                //{
                //    Session["UserAuthentication"] = "";
                //    Message.Text = "Sorry! Your login or password is incorrect. <br>Please log in again.";
                //}
                else
                {
                    Response.Redirect("~/Reports/Search.aspx", false);
                }
            }
            else
            {
                Session["UserAuthentication"] = "";
                Message.Text = "Sorry! Your login or password is incorrect. <br>Please log in again.";
            }
    }
}
