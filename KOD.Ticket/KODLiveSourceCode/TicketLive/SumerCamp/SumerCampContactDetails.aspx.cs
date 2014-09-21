using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KoDTicketing;
using KoDTicketing.BusinessLayer;

public partial class Sumer_Camp_SumerCampContactDetails : System.Web.UI.Page
{
    public static string KoDTicketingIPAddress = System.Configuration.ConfigurationManager.AppSettings["KoDTicketingIPAddress"].ToString();
    public static string HDFCTransUrl = System.Configuration.ConfigurationManager.AppSettings["HDFCTransUrl"].ToString();
    public static string HDFCTransPortalID = System.Configuration.ConfigurationManager.AppSettings["HDFCTransPortalID"].ToString();
    public static string HDFCTranportalPwd = System.Configuration.ConfigurationManager.AppSettings["HDFCTranportalPwd"].ToString();
    protected void Page_Load(object sender, EventArgs e)
    {
        lblttlAmt.Visible = true;
        lblpayAmt.Visible = true;
        Session["NoofTickets"] = Request.QueryString["No"].ToString();
        Session["TotalAmount"]=Convert.ToInt32(Request.QueryString["No"])*5999;
        Session["PayableAmount"] = Convert.ToInt32(Request.QueryString["No"])*5999;
        lblpayAmt.Text = "Total Payable Amount : Rs. " +Session["TotalAmount"].ToString();
        lblttlAmt.Text = "Total Amount : Rs. " + Session["TotalAmount"].ToString();

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        Session["Name"] = txtName.Text;
        Session["Email"] = txtEmailAddress.Text;
        Session["ContactNo"] = txtContactNo.Text;
        //Session["Paymentgateway"] = rbl_CardType.SelectedValue;
        Session["Paymentgateway"] ="HDFC";
        Decimal TotalAmount = Convert.ToDecimal(Session["TotalAmount"].ToString());
        Decimal PayableAmount = Convert.ToDecimal(Session["PayableAmount"].ToString());
        DateTime DateofBooking = DateTime.Now.Date;
        string ISDCode = "91";
        TransactionRecord tr = new TransactionRecord();
        tr.SummerBookingID = GTICKBOL.SummerBooking_Max();
        Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write(tr.SummerBookingID.ToString() + "booking id");
        Session["BookingID"] = tr.SummerBookingID;
        Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write(tr.SummerBookingID.ToString() + "booking id");
        tr.SummerPayableAmount = Convert.ToDecimal(Session["PayableAmount"].ToString());
        //DateTime day = Convert.ToDateTime(Session["day"]);
        MaxBookingId();
        //GTICKBOL.MMTBooking_Details(Convert.ToInt16(Session["NoofPackages"]), Session["pnr"].ToString(), Session["promocode"].ToString(), Convert.ToDecimal(Session["TotalAmount"].ToString()), Convert.ToDecimal(Session["PayableAmount"].ToString()), DateofBooking, Session["BookingID"].ToString(), day, Session["Name"].ToString(), Session["Email"].ToString(), Session["ContactNo"].ToString(), Session["Paymentgateway"].ToString(), false, "");
        GTICKBOL.SummerBooking_Details(Convert.ToInt16(Session["NoofTickets"]), Convert.ToDecimal(Session["TotalAmount"].ToString()), Convert.ToDecimal(Session["PayableAmount"].ToString()), DateofBooking, Session["BookingID"].ToString(),Session["Name"].ToString(), Session["Email"].ToString(), Session["ContactNo"].ToString(), Session["Paymentgateway"].ToString(), false, "");
        ////Parameters for AMEX.........................
        //string Street = txt_street.Text;
        //string Pin = txt_pin.Text;
        //string Country = ddl_country.SelectedValue;
        //string tital = Ddl_title.SelectedValue;
        //string fname = Txtfname.Text;
        //string mname = Txtmname.Text;
        //string lname = Txtlname.Text;
        //string city = Txtcity.Text;
        //string state = Txtstate.Text;
        //string country = txt_country.SelectedValue;


        string trackId, amount;

        trackId = Session["BookingID"].ToString();
        Session["trackId"] = trackId;
        amount = Session["PayableAmount"].ToString();
        Session["amount"] = amount;
        //if (rbl_CardType.SelectedValue == "IDBI")
        //{
        //    string URL = "";
        //    GTICKV.LogEntry(tr.BookingID.ToString(), "Sending to IDBI Payment Gateway", "8", trackId.ToString());
        //    URL = "Idbi/Default.aspx?type=idbi&transid=" + trackId+"~SummerCamp" + "&amt=" + tr.SummerPayableAmount
        //        + "&show=" + "SummerCamp";
        //    Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("url="+URL);
        //    Response.Redirect(URL, false);
        //}
        // Session["PayDetailsTemp"] = tr.BookingID.ToString() + "_" + trackId + "~" + "web" + "|" + tr.MMTPayableAmount + "|" + "Jhumroo";


       // if (rbl_CardType.SelectedValue == "HDFC")
        //{
            string URL = "";
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write(Session["BookingID"].ToString() + "Sending to HDFC Payment Gateway");
            //GTICKV.LogEntry(tr.NYBookingID.ToString(), "Sending to HDFC Payment Gateway", "8", "");


            String ErrorUrl = KoDTicketingIPAddress + "SumerCamp/HDFC/Error.aspx";
            String ResponseUrl = KoDTicketingIPAddress + "SumerCamp/HDFC/ReturnReceipt.aspx";

            //string qrystr = "id=" + HDFCTransPortalID + "&password=" + HDFCTranportalPwd + "&action=1&langid=USA&currencycode=356&amt=" + Server.UrlEncode(amount)
            //    + "&responseURL=" + Server.UrlEncode(ResponseUrl) + "&errorURL=" + Server.UrlEncode(ErrorUrl)
            //    + "&trackid=" + trackId
            //    + "&udf1=TicketBooking&udf2=" + Server.UrlEncode(txtEmailAddress.Text.Trim())
            //    + "&udf3=" + Server.UrlEncode(txtISDCode.Text + txtContactNo.Text) + "&udf4=" + Server.UrlEncode(txtAddress.Text.Trim()) + "&udf5=" + tr.BookingID;

            string qrystr = "id=" + HDFCTransPortalID + "&password=" + HDFCTranportalPwd + "&action=1&langid=USA&currencycode=356&amt=" + amount
               + "&responseURL=" + Server.UrlEncode(ResponseUrl) + "&errorURL=" + Server.UrlEncode(ErrorUrl)
               + "&trackid=" + trackId
               + "&udf1=TicketBooking&udf2=" + txtEmailAddress.Text.Trim()
               + "&udf3=" + Server.UrlEncode(ISDCode.ToString().TrimStart('+') + txtContactNo.Text) + "&udf4=" + Server.UrlEncode(txtName.Text.Trim()) + "&udf5=" + tr.SummerBookingID.ToString();

            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Preparing for HDFC Payment..." + qrystr);

            //Writefile_new("\n***************Initial Request********************", Server.MapPath("~"));
            //Writefile_new("\n\nDateTime:" + DateTime.Now.ToString("dd/MM/yy HH:mm:ss") + " Reference No:" + trackId + "Request XML:" + qrystr, Server.MapPath("~"));

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
                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Excetion while processing HDFC payment: " + trackId + ex.Message);
            }

            if (requestWriter != null)
                requestWriter.Close();

            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Review validation response from HDFC Payment Gateway...");
            System.Net.HttpWebResponse objResponse = (System.Net.HttpWebResponse)objRequest.GetResponse();

            //System.Net.CookieContainer responseCookiesContainer = new System.Net.CookieContainer();
            //foreach (System.Net.Cookie cook in objResponse.Cookies)
            //{
            //    responseCookiesContainer.Add(cook);
            //}

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
            Response.Redirect(URL, false);


       // }
        //else if (rbl_CardType.SelectedValue == "AMEX")
        //{
        //    string URL = "";

        //    GTICKV.LogEntry(tr.BookingID.ToString(), "Sending to AMEX Payment Gateway", "8", trackId.ToString());
        //    if (ddl_country.SelectedValue == "india")
        //        URL = "AMEX/Default.aspx?type=amex&transid=" + trackId + "&amt=" + tr.MMTPayableAmount
        //            + "&show=" + "" + "&title=" + "" + "&fname=" + "" + "&mname=" + "" + "&lname=" + "" + "&street=" + "NA" + "&city=" + "NA" + "&state=" + "NA" + "&pin=" + "NA" + "&country=" + "";
        //    else
        //        URL = "AMEX/Default.aspx?type=amex&transid=" + trackId + "&amt=" + tr.MMTPayableAmount
        //                + "&show=" + "" + "&title=" + tital + "&fname=" + fname + "&mname=" + mname + "&lname=" + lname + "&street=" + Street + "&city=" + city + "&state=" + state + "&pin=" + Pin + "&country=" + country;
        //    Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Payment Redirection: " + URL);
        //    Response.Redirect(URL, false);
        //    Response.Redirect(URL, false);
        //}
    }
    protected void btnBackHome_Click(object sender, EventArgs e)
    {
        Session.Clear();
        Response.Redirect("http://kingdomofdreams.co.in/SummerCamp.html",false);
    }
    void MaxBookingId()
    {
        string tracId = Session["BookingID"].ToString();
        string[] dtr = tracId.Split('M');
        int BookingId_Max = Convert.ToInt16(dtr[1]) + 1;
        Char pad = '0';
        Session["BookingID"] = "KODSUM" + (BookingId_Max.ToString().PadLeft(5, pad));

    }
}