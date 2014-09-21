using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RoyalWebApp.Payment.HDFC
{
    public partial class Default : System.Web.UI.Page
    {
        protected String transAmount;
        protected String transshowname;
        protected String transid;
        public static string IpAddress = System.Configuration.ConfigurationManager.AppSettings["HDFCIP"].ToString();
        public static string HDFCTransUrl = System.Configuration.ConfigurationManager.AppSettings["HDFCTransUrl"].ToString();
        public static string HDFCTransPortalID = System.Configuration.ConfigurationManager.AppSettings["HDFCTransPortalID"].ToString();
        public static string HDFCTranportalPwd = System.Configuration.ConfigurationManager.AppSettings["HDFCTranportalPwd"].ToString();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["type"] != null)
            {
                if (Request.QueryString["type"] != "")
                {
                    string URL = "";
                    double amt = (double.Parse(Request.QueryString["amt"].ToString()));
                    transAmount = amt.ToString();
                    transid = Request.QueryString["transid"].ToString();
                    transshowname = Request.QueryString["show"].ToString();
                    String ErrorUrl = IpAddress + "Payment/HDFC/Error.aspx";
                    String ResponseUrl = IpAddress + "Payment/HDFC/ReturnReceipt.aspx";
                    string qrystr = "id=" + HDFCTransPortalID + "&password=" + HDFCTranportalPwd + "&action=1&langid=USA&currencycode=356&amt=" + transAmount
                          + "&responseURL=" + Server.UrlEncode(ResponseUrl) + "&errorURL=" + Server.UrlEncode(ErrorUrl)
                          + "&trackid=" + transid
                          + "&udf1=TicketBooking" + "&udf2=" + transshowname
                          + "&udf3=" + transshowname + "&udf4=" + transshowname + "&udf5=" + transshowname;
                    System.IO.StreamWriter requestWriter = null;
                    System.Net.HttpWebRequest objRequest = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(HDFCTransUrl);	//create a SSL connection object server-to-server
                    objRequest.Method = "POST";
                    objRequest.ContentLength = qrystr.Length;
                    objRequest.ContentType = "application/x-www-form-urlencoded";
                    objRequest.CookieContainer = new System.Net.CookieContainer();
                    try
                    {
                        requestWriter = new System.IO.StreamWriter(objRequest.GetRequestStream());	// here the request is sent to payment gateway
                        requestWriter.Write(qrystr);
                    }
                    catch (Exception ex)
                    {

                    }

                    if (requestWriter != null)
                        requestWriter.Close();
                    System.Net.HttpWebResponse objResponse = (System.Net.HttpWebResponse)objRequest.GetResponse();

                    using (System.IO.StreamReader sr =
                           new System.IO.StreamReader(objResponse.GetResponseStream()))
                    {
                        String NSDLval = sr.ReadToEnd();
                        if (NSDLval.Contains("Invalid User Defined Field"))
                        {
                            return;
                        }
                        if (NSDLval.IndexOf("http") == -1)
                        {
                            return;
                        }                              
                        string strPmtId = NSDLval.Substring(0, NSDLval.IndexOf(":http"));	// Merchant MUST map (update) the Payment ID received with the merchant Track Id in his database at this place.
                        string strPmtUrl = NSDLval.Substring(NSDLval.IndexOf("http"));
                        if (strPmtId != String.Empty && strPmtUrl != String.Empty)
                        {
                            URL = strPmtUrl.ToString() + "?PaymentID=" + strPmtId;
                        }

                        sr.Close();
                        Response.Redirect(URL, false);
                    }
                }
            }
        }
    }
}