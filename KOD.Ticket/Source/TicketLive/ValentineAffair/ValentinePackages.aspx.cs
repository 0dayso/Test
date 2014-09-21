using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KoDTicketing.BusinessLayer;

public partial class ValentineAffair_ValentinePackages : System.Web.UI.Page
{
    
    public static string KoDTicketingIPAddress = System.Configuration.ConfigurationManager.AppSettings["KoDTicketingIPAddress"].ToString();
    public static string HDFCTransUrl = System.Configuration.ConfigurationManager.AppSettings["HDFCTransUrl"].ToString();
    public static string HDFCTransPortalID = System.Configuration.ConfigurationManager.AppSettings["HDFCTransPortalID"].ToString();
    public static string HDFCTranportalPwd = System.Configuration.ConfigurationManager.AppSettings["HDFCTranportalPwd"].ToString();


    protected void Page_Load(object sender, EventArgs e)
    {
       
    }


    protected void btn_Submit_Click(object sender, EventArgs e)
    {
        string mailer = "";
        if (Request.QueryString["source"] != null)
        {
            mailer = Request.QueryString["source"].ToString();
        }
        string Package = ddl_Package1.SelectedItem.ToString();
        int Quantity = Convert.ToInt16(ddl_Quantity.SelectedValue);
        string[] amt = Package.Split('.');
        decimal TotalAmount = Convert.ToDecimal(amt[1]) * Quantity;
        DateTime DateofBooking = DateTime.Now.Date;
        string ISDCode = "91";
        // String BookingID = KODVL00000; //for the FirstTime//
        TransactionRecord tr = new TransactionRecord();
        tr.VLBookingID = GTICKBOL.ValentineBooking_Max();
        tr.Quantity = Convert.ToInt16(Quantity);
        tr.VLTotalAmount = Convert.ToDecimal(TotalAmount);
        string vlbookingid = MaxBookingId(tr.VLBookingID.ToString());
        tr.VLBookingID = vlbookingid;


        GTICKBOL.ValentineBooking_Details(amt[1], Quantity, Convert.ToDecimal(TotalAmount.ToString()), DateofBooking, tr.VLBookingID.ToString(), txtName.Text.ToString(), txtEmail.Text.ToString(), txtContact.Text.ToString(), false, "", GetIP(), "Valentine_2014", mailer);

        //********Payment GateWay Flow******//

        Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write(tr.VLBookingID.ToString() + "Sending to HDFC Payment Gateway");
        string trackid, amount ;
        string URL = "";
        trackid = tr.VLBookingID.ToString();
        Session["trackid"] = trackid;
        amount = TotalAmount.ToString();
        

        String ErrorUrl = KoDTicketingIPAddress + "ValentineAffair/HDFC/Error.aspx";
        String ResponseUrl = KoDTicketingIPAddress + "ValentineAffair/HDFC/ReturnReceipt.aspx";

        string qrystr = "id=" + HDFCTransPortalID + "&password=" + HDFCTranportalPwd + "&action=1&langid=USA&currencycode=356&amt=" + amount
          + "&responseURL=" + Server.UrlEncode(ResponseUrl) + "&errorURL=" + Server.UrlEncode(ErrorUrl)
          + "&trackid=" + trackid
          + "&udf1=TicketBooking&udf2=" + txtEmail.Text.Trim()
          + "&udf3=" + Server.UrlEncode(ISDCode.ToString().TrimStart('+') + txtContact.Text) + "&udf4=" + Server.UrlEncode(txtName.Text.Trim()) + "&udf5=" + tr.BookingID.ToString();

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
        Response.Redirect(URL, false);

    }

    string MaxBookingId(string vlbookingid)
    {
        string tracId = vlbookingid.ToString();
        string[] dtr = tracId.Split('L');
        int BookingId_Max = Convert.ToInt16(dtr[1]) + 1;
        Char pad = '0';
        return "KODVL" + (BookingId_Max.ToString().PadLeft(5, pad));
    }
    protected string GetIP()
    {
        string ipaddress;
        ipaddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (ipaddress == "" || ipaddress == null)
            ipaddress = Request.ServerVariables["REMOTE_ADDR"];
        return ipaddress;
    }
}