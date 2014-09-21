using System.Data.SqlClient;
using System.Configuration;
using System.Web.Security;
using System.Web.UI.WebControls;
using KoDTicketing.DataAccessLayer;
using KoDTicketing;
using KoDTicketing.BusinessLayer;

public partial class AgentFlow_AgentLogin : System.Web.UI.Page
{
    protected void Login_Authenticate(object sender, AuthenticateEventArgs e)
    {
        string username = Login.UserName;
        string pwd = Login.Password;
        string sqlUserName = "SELECT * FROM [MSTicketDB_Live_Latest].[dbo].[tbl_Agent_Login] where username = '" + username + "' and password = '" + pwd + "' and " +
            "Status = 1";
        System.Data.DataTable dt = Connection.readTab(sqlUserName, DBAccess.connMSTicket);
        if (dt != null && dt.Rows.Count > 0)
        {
            string ip=GetIP();
            GTICKBOL.Update_AgentLogin(username,pwd,ip);
            Session["Agent"] = username;
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Agent login:  " + username);
            Response.Redirect("AgentProfile.aspx", false);
        }
        else
        {
            Session["UserAuthentication"] = "";
            Message.Text = "Sorry! Your login or password is incorrect. <br>Please log in again.";
        }
    }
    protected string GetIP()   //this function get the ip of the user systems
    {
        string ipaddress;
        ipaddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (ipaddress == "" || ipaddress == null)
            ipaddress = Request.ServerVariables["REMOTE_ADDR"];
        return ipaddress;
    }
}