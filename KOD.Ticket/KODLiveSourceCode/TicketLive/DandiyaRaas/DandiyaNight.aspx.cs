using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KoDTicketing.BusinessLayer;


public partial class DandiyaRaas_DandiyaNight : System.Web.UI.Page
{
    public static string KoDTicketingIPAddress = System.Configuration.ConfigurationManager.AppSettings["KoDTicketingIPAddress"].ToString();
    public static string HDFCTransUrl = System.Configuration.ConfigurationManager.AppSettings["HDFCTransUrl"].ToString();
    public static string HDFCTransPortalID = System.Configuration.ConfigurationManager.AppSettings["HDFCTransPortalID"].ToString();
    public static string HDFCTranportalPwd = System.Configuration.ConfigurationManager.AppSettings["HDFCTranportalPwd"].ToString();


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string d1 = "Thu, Oct 10,2013"; string d2 = "Fri, Oct 11,2013"; string d3 = "Sat, Oct 12,2013";
            if (DateTime.Now.ToString("ddd, MMM dd,yyyy") == d1 || DateTime.Now < DateTime.Parse(d1))
                ddldate.Items.Add(new ListItem(d1, d1));
            if (DateTime.Now.ToString("ddd, MMM dd,yyyy") == d2 || DateTime.Now < DateTime.Parse(d2))
                ddldate.Items.Add(new ListItem(d2, d2));
            if (DateTime.Now.ToString("ddd, MMM dd,yyyy") == d3 || DateTime.Now < DateTime.Parse(d3))
                ddldate.Items.Add(new ListItem(d3, d3));
        }
    }


    protected void btn_Submit_Click(object sender, EventArgs e)
    {
        TransactionRecord tr = new TransactionRecord();
        tr.VLBookingID = GTICKBOL.DandiyaBooking_Max();
        string Package = ddl_Package1.SelectedItem.ToString();
        int Quantity = Convert.ToInt16(ddl_Quantity.SelectedValue);
        string[] amt = Package.Split('.');
        decimal TotalAmount = Convert.ToDecimal(amt[1]) * Quantity;
        Session["TotalAmount" + tr.VLBookingID] = TotalAmount;
        Session["Quantity" + tr.VLBookingID] = Quantity;
        Session["Package" + tr.VLBookingID] = Package;
        Session["Name" + tr.VLBookingID] = txtName.Text;
        Session["Email" + tr.VLBookingID] = txtEmail.Text;
        Session["Contact" + tr.VLBookingID] = txtContact.Text;
        DateTime DateofBooking = DateTime.Now.Date;
        string ISDCode = "91";
        // String BookingID = KODVL00000; //for the FirstTime//
        Session["BookingID" + tr.VLBookingID] = tr.VLBookingID;
        tr.Quantity = Convert.ToInt16(Session["Quantity" + tr.VLBookingID]);
        tr.VLTotalAmount = Convert.ToDecimal(Session["TotalAmount" + tr.VLBookingID].ToString());
        MaxBookingId(Session["BookingID" + tr.VLBookingID].ToString());

        GTICKBOL.DandiyaBooking_Details(amt[1], Quantity, Convert.ToDecimal(Session["TotalAmount" + tr.VLBookingID].ToString()), DateofBooking, Session["BookingID" + tr.VLBookingID].ToString(), Session["Name" + tr.VLBookingID].ToString(), Session["Email" + tr.VLBookingID].ToString(), Session["Contact" + tr.VLBookingID].ToString(), false, "",ddl_Package1.SelectedItem.Text,ddldate.SelectedItem.Text);

        //********Payment GateWay Flow******//

        Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write(Session["BookingID" + tr.VLBookingID].ToString() + "Sending to HDFC Payment Gateway");
        string trackid, amount;
        string URL = "";
        trackid = Session["BookingID" + tr.VLBookingID].ToString();
        Session["trackid" + tr.VLBookingID] = trackid;
        amount = Session["TotalAmount" + tr.VLBookingID].ToString();
        Session["amount" + tr.VLBookingID] = amount;

        String ErrorUrl = KoDTicketingIPAddress + "DandiyaRaas/HDFC/Error.aspx";
        String ResponseUrl = KoDTicketingIPAddress + "DandiyaRaas/HDFC/ReturnReceipt.aspx";

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

    void MaxBookingId(string id)
    {
        string tracId = id;
        string[] dtr = tracId.Split('N');
        int BookingId_Max = Convert.ToInt16(dtr[1]) + 1;
        Char pad = '0';
        Session["BookingID"+id] = "KODDN" + (BookingId_Max.ToString().PadLeft(5, pad));
    }
}