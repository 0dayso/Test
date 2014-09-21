using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KoDTicketing.BusinessLayer;
using System.Data;

public partial class Boty_Botypayment : System.Web.UI.Page
{
    public static string KoDTicketingIPAddress = System.Configuration.ConfigurationManager.AppSettings["KoDTicketingIPAddress"].ToString();
    public static string HDFCTransUrl = System.Configuration.ConfigurationManager.AppSettings["HDFCTransUrl"].ToString();
    public static string HDFCTransPortalID = System.Configuration.ConfigurationManager.AppSettings["HDFCTransPortalID"].ToString();
    public static string HDFCTranportalPwd = System.Configuration.ConfigurationManager.AppSettings["HDFCTranportalPwd"].ToString();
    public string Decrypt(string val)
    {
        val = val.Replace(" ", "+");
        var bytes = Convert.FromBase64String(val);
        var encBytes = System.Security.Cryptography.ProtectedData.Unprotect(bytes, new byte[0], System.Security.Cryptography.DataProtectionScope.LocalMachine);
        return System.Text.Encoding.UTF8.GetString(encBytes);
    }
    public string Encrypt(string val)
    {
        var bytes = System.Text.Encoding.UTF8.GetBytes(val);
        var encBytes = System.Security.Cryptography.ProtectedData.Protect(bytes, new byte[0], System.Security.Cryptography.DataProtectionScope.LocalMachine);
        return Convert.ToBase64String(encBytes);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write(Request.QueryString["form_id"] + "Form id for Boty");
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write(Request.QueryString["entry_id"] + "Entry id for Boty");
            frmid.Text = Request.QueryString["form_id"];
            entryid.Text = Request.QueryString["entry_id"];
            amt.Text = "1000 Rs.";
        }
    }
    protected void btn_Submit_Click(object sender, EventArgs e)
    {
        DataTable dt = TransactionBOL.Check_BotyTransaction(entryid.Text);
        if (dt.Rows.Count == 0)
        {
            decimal TotalAmount = Convert.ToDecimal("1000");
            DateTime DateofBooking = DateTime.Now.Date;
            string ISDCode = "91";
            // String BookingID = KODVL00000; //for the FirstTime//
            TransactionRecord tr = new TransactionRecord();
            tr.VLBookingID = GTICKBOL.BotyBooking_Max();
            tr.VLTotalAmount = Convert.ToDecimal(TotalAmount);
            string vlbookingid = MaxBookingId(tr.VLBookingID.ToString());
            tr.VLBookingID = vlbookingid;

            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write(tr.VLBookingID.ToString() + " booking id for boty which have entry id " + Request.QueryString["entry_id"]);
            int enrty = GTICKBOL.BotyBooking_Details(frmid.Text, entryid.Text, TotalAmount, DateofBooking, tr.VLBookingID.ToString(), false, "");

            //********Payment GateWay Flow******//

            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write(tr.VLBookingID.ToString() + "Sending to HDFC Payment Gateway");
            string trackid, amount;
            string URL = "";
            trackid = tr.VLBookingID.ToString();
            Session["trackid"] = trackid;
            amount = TotalAmount.ToString();


            String ErrorUrl = KoDTicketingIPAddress + "Boty/HDFC/Error.aspx";
            String ResponseUrl = KoDTicketingIPAddress + "Boty/HDFC/ReturnReceipt.aspx";

            string qrystr = "id=" + HDFCTransPortalID + "&password=" + HDFCTranportalPwd + "&action=1&langid=USA&currencycode=356&amt=" + amount
              + "&responseURL=" + Server.UrlEncode(ResponseUrl) + "&errorURL=" + Server.UrlEncode(ErrorUrl)
              + "&trackid=" + trackid
              + "&udf1=TicketBooking&udf2=" + "".Trim()
              + "&udf3=" + Server.UrlEncode(ISDCode.ToString().TrimStart('+') + "") + "&udf4=" + Server.UrlEncode("".Trim()) + "&udf5=" + tr.BookingID.ToString();

            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Preparing for HDFC Payment..." + qrystr);

            System.IO.StreamWriter requestWriter = null;
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Redirecting for HDFC Payment..." + HDFCTransUrl);
            System.Net.HttpWebRequest objRequest = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(HDFCTransUrl);	//create a SSL connection object server-to-server
            objRequest.Method = "POST";
            objRequest.ContentLength = qrystr.Length;
            objRequest.ContentType = "application/x-www-form-urlencoded";
            objRequest.CookieContainer = new System.Net.CookieContainer();

            try
            {
                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Processing request for HDFC Payment...");
                requestWriter = new System.IO.StreamWriter(objRequest.GetRequestStream());	// here the request is sent to payment gateway
                requestWriter.Write(qrystr);
            }
            catch (Exception ex)
            {
                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Excetion while processing HDFC payment: " + trackid + ex.Message);
            }

            if (requestWriter != null)
                requestWriter.Close();

            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Review validation response from HDFC Payment Gateway...");
            System.Net.HttpWebResponse objResponse = (System.Net.HttpWebResponse)objRequest.GetResponse();

            using (System.IO.StreamReader sr =
                   new System.IO.StreamReader(objResponse.GetResponseStream()))
            {
                String NSDLval = sr.ReadToEnd();
                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Response: " + NSDLval);
                if (NSDLval.Contains("Invalid User Defined Field"))
                {
                    lblMess.Text = "The information submitted contains some invalid character, please avoid using [+,-,#] etc.";
                    return;
                }

                //Writefile_new("\n***************Initial Response********************", Server.MapPath("~"));
                //Writefile_new("\n\nDateTime:" + DateTime.Now.ToString("dd/MM/yy HH:mm:ss") + " Reference No:" + trackId + "Request XML:" + NSDLval, Server.MapPath("~"));
                if (NSDLval.IndexOf("http") == -1)
                {
                    lblMess.Text = "Payment cannot be processed with information provided.";
                    return;
                }

                // gb.HDFCLog(transid.ToString(), "", trackId, "***Initial Response*** : " + NSDLval);                                
                string strPmtId = NSDLval.Substring(0, NSDLval.IndexOf(":http"));	// Merchant MUST map (update) the Payment ID received with the merchant Track Id in his database at this place.
                string strPmtUrl = NSDLval.Substring(NSDLval.IndexOf("http"));
                if (strPmtId != String.Empty && strPmtUrl != String.Empty)
                {
                    URL = strPmtUrl.ToString() + "?PaymentID=" + strPmtId;
                }
                else
                {
                    lblMess.Text = "Invalid Response!";
                }
                sr.Close();
            }
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Payment Redirection: " + URL);
            if (enrty > 0)
                Response.Redirect(URL, false);
        }
        else 
        {
            ClientScript.RegisterStartupScript(GetType(), "myscript", "<script>alert('Entry Id Already Exist. Please start" +
                               " the transaction again');window.location.href='http://boty.in/register-here/';</script>");
        }
    }
    string MaxBookingId(string vlbookingid)
    {
        string tracId = vlbookingid.ToString();
        string[] dtr = tracId.Split('Y');
        int BookingId_Max = Convert.ToInt16(dtr[1]) + 1;
        Char pad = '0';
        return "BOTY" + (BookingId_Max.ToString().PadLeft(5, pad));
    }
}