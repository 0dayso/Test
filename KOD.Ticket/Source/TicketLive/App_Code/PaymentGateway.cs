using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KoDTicketing.BusinessLayer;
using System.Data;
using System.Web.Services;
using System.Web.Services.Protocols;

/// <summary>
/// Summary description for PaymentGateway
/// </summary>
[WebService(Namespace = "http://msticket.kingdomofdreams.in/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class PaymentGateway : System.Web.Services.WebService {
    public static string KoDTicketingIPAddress = System.Configuration.ConfigurationManager.AppSettings["KoDTicketingIPAddress"].ToString();
    public static string HDFCTransUrl = System.Configuration.ConfigurationManager.AppSettings["HDFCTransUrl"].ToString();
    public static string HDFCTransPortalID = System.Configuration.ConfigurationManager.AppSettings["HDFCTransPortalID"].ToString();
    public static string HDFCTranportalPwd = System.Configuration.ConfigurationManager.AppSettings["HDFCTranportalPwd"].ToString();
    public Security objSecurity;
    public PaymentGateway () {
    }

    [WebMethod(EnableSession=true)]
    [SoapHeader("objSecurity")]
    public string HelloWorld(string BookingID, string Name, string Email, string Contact_No, string Event_Name, string responceURL) {
        //Session["ID"] = entid.ToString();
        DataTable dt = TransactionBOL.Check_EventTransaction(BookingID);
        if (dt.Rows.Count == 0)
        {
            decimal TotalAmount = Convert.ToDecimal("1000");
            DateTime DateofBooking = DateTime.Now.Date;
            string ISDCode = "91";
            // String BookingID = EVENT00000; //for the FirstTime//
            TransactionRecord tr = new TransactionRecord();
            tr.EvtBookingID = GTICKBOL.EventBooking_Max();
            tr.EvtTotalAmount = Convert.ToDecimal(TotalAmount);
            string evtbookingid = MaxBookingId(tr.EvtBookingID.ToString());
            tr.EvtBookingID = evtbookingid;
            int enrty = GTICKBOL.EventBooking_Details(Event_Name + BookingID, Name, Event_Name, Contact_No, Event_Name, responceURL, TotalAmount, DateofBooking, false, "", tr.EvtBookingID.ToString());
            //********Payment GateWay Flow******//
            #region Payment GateWay Flow
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write(tr.EvtBookingID.ToString() + "Sending to HDFC Payment Gateway");
            string trackid, amount;
            string URL = "";
            trackid = tr.EvtBookingID.ToString();
            //Session["trackid"] = trackid;
            amount = TotalAmount.ToString();
            String ErrorUrl = KoDTicketingIPAddress + "EventTransaction/HDFC/Error.aspx";
            String ResponseUrl = KoDTicketingIPAddress + "EventTransaction/HDFC/ReturnReceipt.aspx";

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
                    return "The information submitted contains some invalid character, please avoid using [+,-,#] etc.";
                }

                //Writefile_new("\n***************Initial Response********************", Server.MapPath("~"));
                //Writefile_new("\n\nDateTime:" + DateTime.Now.ToString("dd/MM/yy HH:mm:ss") + " Reference No:" + trackId + "Request XML:" + NSDLval, Server.MapPath("~"));
                if (NSDLval.IndexOf("http") == -1)
                {
                    return "Payment cannot be processed with information provided.";
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
                   return "Invalid Response!";
                }
                sr.Close();
            }
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Payment Redirection: " + URL);
            if (enrty > 0)
                return URL;
            else
                return null;

            #endregion Payment GateWay Flow
           //**********************************//
        }
        else
        {
            return null;
        }
    }
    [WebMethod(EnableSession = true)]
    [SoapHeader("objSecurity")]
    public string FinalStatus(string id){
        DataTable dt = TransactionBOL.Select_EventTransaction(id);
        DataRow dr = dt.Rows[0];
        return dr["PGIsPaymentSuccess"].ToString() + "," + dr["BookingID"].ToString() + "," + dr["PGReceiptId"].ToString();
    }
    public string MaxBookingId(string evtbookingid)
    {
        string tracId = evtbookingid.ToString();
        string[] dtr = tracId.Split('T');
        int BookingId_Max = Convert.ToInt16(dtr[1]) + 1;
        Char pad = '0';
        //Response.Redirect(url)
        return "EVENT" + (BookingId_Max.ToString().PadLeft(6, pad));
    } 
}
