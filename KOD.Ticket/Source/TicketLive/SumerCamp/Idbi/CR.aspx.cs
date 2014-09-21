using System;
//using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using KoDTicketing;
using KoDTicketing.Utilities;
using KoDTicketing.BusinessLayer;
using KoDUtilities;
using System.Configuration;
public partial class Sumer_Camp_Idbi_CR : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        System.Text.StringBuilder qstring = new System.Text.StringBuilder();
        int qsCount = Request.QueryString.Count;

        #region processreceipt
        if (qsCount > 0 && Request.QueryString["sta"] != null && Request.QueryString["tid"] != null)
        {
            TransactionRecord tr = new TransactionRecord();
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write(string.Format("IDBI Web payment transaction response [{0}]", Request.QueryString["sta"]));
            tr.Status = Request.QueryString["sta"].ToString().Equals("50020");
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("IDBI Web payment transaction complete. Status: " + (tr.Status ? "Success" : "Failure"));

            string IpAddress = System.Configuration.ConfigurationManager.AppSettings["KoDTicketingIPAddress"];
            #region parsereference

            String refNo = Request.QueryString["tid"].ToString();
            string[] refTokens = refNo.Split('~');
            if (refNo.Length < 2)
            {
                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("IDBI Payment Response: Tokenization of reference string did not result in enough tokens. --> " + refNo);
                return;
            }

            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("IDBI Transaction reference: " + refNo[0].ToString());
           // tr.ReferenceNo = long.Parse(refTokens[0].ToString());

            //string[] refSubTokens = refTokens[1].Split('~');

            //if (refSubTokens.Length < 2)
            //{
            //    Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("IDBI Payment Response: Tokenization of reference substring did not result in enough sub tokens. --> " + refTokens[1].ToString());
            //    return;
            //}

            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("IDBI Booking ID: " + refNo[0]);
            tr.SummerBookingID = refNo[0].ToString();

            //Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("IDBI Agent Code: " + refSubTokens[1]);
            //tr.AgentCode = refSubTokens[1];
            #endregion parsereference
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("IDBI Amount: " + Request.QueryString["amt"].ToString());
            tr.SummerPayableAmount = decimal.Parse(Request.QueryString["amt"].ToString());
            try
            {
                if (true == tr.Status)
                {
                    HandleSuccess(ref tr, ref qstring);
                }
                else //failure
                {
                    //GTICKV.LogEntry(tr.ReferenceNo.ToString(), "Payment Not Successful", "17", tr.BookingID.ToString());
                    //GTICKBOL.ON_Session_out(tr.ReferenceNo.ToString());
                    //GTICKV.LogEntry(tr.ReferenceNo.ToString(), "Seats Unlocked", "18", tr.BookingID.ToString());
                    long BookingID = long.Parse(tr.ReferenceNo.ToString());
                    DataTable dt = TransactionBOL.Select_Temptransaction_REFIDWISE(BookingID);
                    ReceiptUtils.PaymentNotCaptureResponse(tr.ReceiptNo.ToString(), dt.Rows[0], "");
                    qstring.Append("?err=pay");
                }
            }
            catch (Exception ex)
            {
                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write(ex.Message);
                HandleFailure(tr);
                qstring.Append("?err=seat");
            }
        }

        #endregion processreceipt

        Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("IDBI Transaction Complete. " + Request.QueryString["rec"]);
        Server.Transfer("../Print-Receipt.aspx" + qstring.ToString(), false);

    }

    protected void HandleSuccess(ref TransactionRecord tr, ref System.Text.StringBuilder qstring)
    {
        qstring.Clear();

        #region handlesuccess
        GTICKV.LogEntry(tr.ReferenceNo.ToString(), "IDBI Payment successful...", "10", tr.BookingID.ToString(), tr.ReceiptNo.ToString());

        #region parsereceipt
        if (Request.QueryString["rec"] != null)
        {
            try
            {
                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("IDBI Receipt: " + Request.QueryString["rec"]);
                tr.SummerReceiptNo = Request.QueryString["rec"].ToString();
            }
            catch (Exception ex)
            {
                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("IDBI Response: Error parsing receipt.");
                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write(ex.Message);
            }
        }
        #endregion parsereceipt

        try
        {

            ////****** Promo code usecase start here ************
            //KODHelper objKODHelper = new KODHelper();
            //tr = objKODHelper.GetPromotionDetails(tr);
            ////****** Promo code usecase END here ************
            TransactionBOL.Get_Summer_Detail(tr.SummerBookingID, tr.SummerReceiptNo);
            DataTable dt = TransactionBOL.Select_SummerTransaction_REFIDWISE(tr.SummerBookingID.ToString());
            if (dt != null && dt.Rows.Count > 0)
            {
                bool seatsBooked = (dt.Rows[0]["IsPaymentSuccess"].ToString() == "1");
                ReceiptUtils.SummerPaymentResponse(dt.Rows[0], tr.SummerReceiptNo.ToString(), tr.SummerBookingID.ToString(), ConfigurationManager.AppSettings.Get("ConcertRefMailId"), ConfigurationManager.AppSettings.Get("ConcertRefMailId1"));
                    Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Mail send through normal flow");
                    Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Values are " + dt.Rows[0] + " , " + tr.SummerReceiptNo.ToString() + "," + tr.SummerBookingID.ToString());
                    //SendNotificationMailForHotels(seatsBooked, dt.Rows[0], tr.ReferenceNo.ToString(), tr.BookingID.ToString(), tr.ReceiptNo, tr.PromotionCode.ToString());
                    //Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Mail send through Hotel flow");
                qstring.Clear();
                qstring.Append("?b=");
                qstring.Append((seatsBooked) ? dt.Rows[0]["BookingID"].ToString() : dt.Rows[0]["ID"].ToString());
            }
            else
            {
                long BookingID1 = long.Parse(tr.ReferenceNo.ToString());
                DataTable dt1 = TransactionBOL.Select_Temptransaction_REFIDWISE(BookingID1);
                ReceiptUtils.SuccessPaymentResponse(tr.ReceiptNo.ToString(), dt1.Rows[0], "");
                //ReceiptUtils.SuccessPaymentResponse(tr.SummerBookingID.ToString(), tr.SummerReceiptNo);
                qstring.Append("?err=seat");
            }
        }
        catch (Exception ex)
        {
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("IDBI Receipt: error getting transaction details - " + ex.Message);
            HandleFailure(tr);
            long BookingID1 = long.Parse(tr.ReferenceNo.ToString());
            DataTable dt1 = TransactionBOL.Select_Temptransaction_REFIDWISE(BookingID1);
            ReceiptUtils.SuccessPaymentResponse(tr.ReceiptNo.ToString(), dt1.Rows[0], "");
            qstring.Append("?err=seat");
        }
        #endregion handlesuccess
    }
    protected void HandleFailure(TransactionRecord tr)
    {
        #region handlefailure

        //String _refNo = tr.ReferenceNo.ToString();

        KoDTicketing.GTICKV.LogEntry(tr.SummerReceiptNo.ToString(), "IDBI Error Occured -- Payment Not Successful", "19", tr.SummerReceiptNo.ToString());
        long BookingID = long.Parse(tr.SummerBookingID.ToString());
        try
        {
            DataTable dt = TransactionBOL.Select_SummerTransaction_REFIDWISE(tr.SummerBookingID.ToString());

            if (dt.Rows.Count == 0)
            {
                ReceiptUtils.FailurePaymentResponse();
            }
            else
            {
                ReceiptUtils.FailurePaymentResponse(dt.Rows[0]);
            }
        }
        catch (Exception ex)
        {
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Error occurred processing unsuccessful payment through IDBI..." + ex.Message);
        }
        #endregion handlefailure
    }

}