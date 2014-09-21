using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Royal_Card_Skins_Master_Print : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["FirstName"] != null && Request.QueryString["LastName"] != null)
        {
            string password = "A87C7B95932E9";
            LblUserName.Text = "Hi " + Common.Decrypt(Server.UrlDecode(Request.QueryString["FirstName"].ToString()),password) + " " + Common.Decrypt(Server.UrlDecode(Request.QueryString["LastName"].ToString()),password) + " [<a href='http://royalty.kingdomofdreams.in/Account/Logout.aspx'>Sign Out</a> ]";
        }
        else if (Request.QueryString["FirstName"] == null && Request.QueryString["LastName"] == null)
        {
            LblUserName.Text = "Hi " + Session["FirstName"].ToString() + " " + Session["LastName"].ToString() + " [<a href='http://royalty.kingdomofdreams.in/Account/Logout.aspx'>Sign Out</a> ]";
        }

    }
    public string Decrypt(string val)
    {
        var bytes = Convert.FromBase64String(val);
        var encBytes = System.Security.Cryptography.ProtectedData.Unprotect(bytes, new byte[0], System.Security.Cryptography.DataProtectionScope.LocalMachine);
        return System.Text.Encoding.UTF8.GetString(encBytes);
    }
}
