using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KoDTicketing;
using KoDTicketing.BusinessLayer;
using System.Data;
using KoDTicketing.DataAccessLayer;

public partial class NewYearPackages_ChoosePackages : System.Web.UI.Page
{
    public static string KoDTicketingIPAddress = System.Configuration.ConfigurationManager.AppSettings["KoDTicketingIPAddress"].ToString();
    public static string HDFCTransUrl = System.Configuration.ConfigurationManager.AppSettings["HDFCTransUrl"].ToString();
    public static string HDFCTransPortalID = System.Configuration.ConfigurationManager.AppSettings["HDFCTransPortalID"].ToString();
    public static string HDFCTranportalPwd = System.Configuration.ConfigurationManager.AppSettings["HDFCTranportalPwd"].ToString();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string d1 = "Tue, Dec 31,2013";
            if (DateTime.Now.Date > DateTime.Parse(d1))
            {
                ddl_CouplePackage.Items.Clear();
                ddl_SinglePackage.Items.Clear();
                ddl_TeensPackage.Items.Clear();
                ddl_KidsPackage.Items.Clear();
                ddl_CouplePackage.Items.Add("0");
                ddl_SinglePackage.Items.Add("0");
                ddl_TeensPackage.Items.Add("0");
                ddl_KidsPackage.Items.Add("0");
            } 
        }
    }
    protected void btn_Submit_Click(object sender, EventArgs e)
    {
        int CouplePackage = Convert.ToInt16(ddl_CouplePackage.SelectedValue);
        int SinglePackage = Convert.ToInt16(ddl_SinglePackage.SelectedValue);
        int TeensPackage = Convert.ToInt16(ddl_TeensPackage.SelectedValue);
        int KidsPackage = Convert.ToInt16(ddl_KidsPackage.SelectedValue);
        decimal CoupleAmount = Convert.ToDecimal(CouplePackage * Convert.ToInt32(pricecouple.Text));
        decimal SingleAmount = Convert.ToDecimal(SinglePackage * Convert.ToInt32(pricesingle.Text));
        decimal TeensAmount = Convert.ToDecimal(TeensPackage * Convert.ToInt32(priceteen.Text));
        decimal KidsAmount = Convert.ToDecimal(KidsPackage * Convert.ToInt32(pricekids.Text));
        decimal TotalAmount = CoupleAmount + SingleAmount + TeensAmount + KidsAmount;
        DateTime DateofBooking = DateTime.Now.Date;
        string ISDCode = "91";
        TransactionRecord tr = new TransactionRecord();
        tr.NYBookingID = GTICKBOL.NewYearBooking_Max();
        tr.QntyCouple = Convert.ToInt16(CouplePackage);
        tr.QntySingle = Convert.ToInt16(SinglePackage);
        tr.QntyTeens = Convert.ToInt16(TeensPackage);
        tr.QntyKids = Convert.ToInt16(KidsPackage);
        tr.NYTotalAmount = Convert.ToDecimal(TotalAmount.ToString());
        string nybookingid=MaxBookingId(tr.NYBookingID.ToString());
        tr.NYBookingID = nybookingid;
        GTICKBOL.NewYearBooking_Details(Convert.ToInt16(CouplePackage), Convert.ToInt16(SinglePackage), Convert.ToInt16(TeensPackage), Convert.ToInt16(KidsPackage), Convert.ToDecimal(TotalAmount.ToString()), DateofBooking, tr.NYBookingID.ToString(), txtName.Text, txtEmail.Text, txtContact.Text, false, "", textroyalinfo.Text);
        //Payment Gateway Flow

        Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write(tr.NYBookingID.ToString() + "Sending to HDFC Payment Gateway");
        string trackId, amount;
        string URL = "";
        trackId = tr.NYBookingID;
        amount = TotalAmount.ToString();
        String ErrorUrl = KoDTicketingIPAddress + "NewYearPackages/HDFC/Error.aspx";
        String ResponseUrl = KoDTicketingIPAddress + "NewYearPackages/HDFC/ReturnReceipt.aspx";
        string qrystr = "id=" + HDFCTransPortalID + "&password=" + HDFCTranportalPwd + "&action=1&langid=USA&currencycode=356&amt=" + amount
           + "&responseURL=" + Server.UrlEncode(ResponseUrl) + "&errorURL=" + Server.UrlEncode(ErrorUrl)
           + "&trackid=" + trackId
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
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Excetion while processing HDFC payment: " + trackId + ex.Message);
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

            if (NSDLval.IndexOf("http") == -1)
            {
                lblMess.Text = "Payment cannot be processed with information provided.";
                return;
            }
                                
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
                        

    string MaxBookingId(string nybookingid)
    {
        string tracId = nybookingid.ToString();
        string[] dtr= tracId.Split('Y');
        int BookingId_Max = Convert.ToInt16(dtr[1]) + 1;
        Char pad = '0';
        return "KODNY" + (BookingId_Max.ToString().PadLeft(5,pad));
       
        
    }
    protected void btnvalidation_Click(object sender, EventArgs e)
    {
       // DataTable dtroyalinfo = VistaBOL.Select_Newyear_RoyalInfo(textroyalinfo.Text);
        DataTable dtroyalinfo = TransactionBOL.selectifo_royal(textroyalinfo.Text);
        if (textroyalinfo.Text == "")
        {
            ClientScript.RegisterStartupScript(typeof(Page), "test", "alert('Please enter Royal Card No. or Mobile No.');", true);
            pricecouple.Text = "11999";
            pricesingle.Text = "6999";
            priceteen.Text = "3999";
            pricekids.Text = "2999";
        }
        else if (dtroyalinfo.Rows.Count > 0)
        {
            pricecouple.Text = "9999";
            pricesingle.Text = "5999";
            priceteen.Text = "3999";
            pricekids.Text = "2999";
        }
        else
        {
            ClientScript.RegisterStartupScript(typeof(Page), "test", "alert('Royal Card No. or Mobile No. not matched.');", true);
            pricecouple.Text = "11999";
            pricesingle.Text = "6999";
            priceteen.Text = "3999";
            pricekids.Text = "2999";
            textroyalinfo.Text = "";
        }
    }
}