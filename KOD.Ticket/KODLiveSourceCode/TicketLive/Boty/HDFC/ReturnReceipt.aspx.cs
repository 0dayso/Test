using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KoDTicketing.BusinessLayer;
using System.Data;
using KoDTicketing.Utilities;
using KoDTicketing;
using System.Configuration;

public partial class Boty_HDFC_ReturnReceipt : System.Web.UI.Page
{
    public static string KoDTicketingIPAddress = System.Configuration.ConfigurationManager.AppSettings["KoDTicketingIPAddress"].ToString();
    protected void Page_Load(object sender, EventArgs e)
    {
         if (!IsPostBack)
        {
             string paymentId, ErrorText, result, postdate, tranid, auth, amt, reference;
            string ErrorNo, udf1, udf2, udf3, udf4, udf5, trackid;

             paymentId = Request["paymentid"] ?? String.Empty;
            ErrorText = Request["ErrorText"] ?? String.Empty;
            ErrorNo = Request["Error"] ?? String.Empty;

            udf1 = Request["udf1"] ?? String.Empty;
            udf2 = Request["udf2"] ?? String.Empty;
            udf3 = Request["udf3"] ?? String.Empty;
            udf4 = Request["udf4"] ?? String.Empty;
            udf5 = Request["udf5"] ?? String.Empty;


             if (ErrorNo == String.Empty)
            {
                result = Request["result"] ?? String.Empty;
                postdate = Request["postdate"] ?? String.Empty;
                tranid = Request["tranid"] ?? String.Empty;
                auth = Request["auth"] ?? String.Empty;
                trackid = Request["trackid"] ?? String.Empty;
                reference = Request["ref"] ?? String.Empty;
                amt = Request["amt"] ?? String.Empty;

            String responseDetails = string.Format("HDFC Response: paymentId[{0}], ErrorText[{1}], result[{2}], postdate[{3}], tranid[{4}], auth[{5}], amt[{6}], reference[{7}], ErrorNo[{8}], udf1[{9}], udf2[{10}], udf3[{11}], udf4[{12}], udf5[{13}], trackid[{14}]",
                                                        paymentId, ErrorText, result, postdate, tranid, auth, amt, reference, ErrorNo, udf1, udf2, udf3, udf4, udf5, trackid);
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write(responseDetails);

                  if (trackid != String.Empty)
                {
                    Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("HDFC Track ID: " + trackid);
                    Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("HDFC Result: " + Request["result"].ToString());
                    string qstring = UpdateResponse(trackid, reference, result, postdate, auth);
                    if (!string.IsNullOrEmpty(qstring))
                    {
                        string redirectURL = "REDIRECT=" + KoDTicketingIPAddress + "Boty/Print-Receipt.aspx" + qstring;
                        Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("HDFC response redirection: " + redirectURL);
                        Response.Write(redirectURL);
                        return;
                    }

                      else
                        Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("HDFC response processing failed and resulting in empty receipt.");
                       }
                else
                    Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("HDFC Response contains NO track ID");
                  Response.Write("REDIRECT=" + KoDTicketingIPAddress + "Boty/Print-Receipt.aspx?err=pay");

                  }
            else
            {
                String errorDetails = string.Format("paymentId[{0}], ErrorText[{1}], ErrorNo[{2}], udf1[{3}], udf2[{4}], udf3[{5}], udf4[{6}], udf5[{7}]",
                   paymentId, ErrorText, ErrorNo, udf1, udf2, udf3, udf4, udf5);
                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("HDFC Response: " + errorDetails);

                  string err;
                if (Request["ErrorText"] != null)
                {
                    err = Request["ErrorText"].ToString();
                }
                else
                {
                    err = "Error occcured in processing payment through HDFC payment gateway.";
                }
                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("HDFC Error Response: " + err);
                Response.Write("REDIRECT=" + KoDTicketingIPAddress + "Boty/Print-Receipt.aspx?err=pay");
            }
        }//!postback
    }
    protected String UpdateResponse(String trackid, String reference, String result, String postdate, String auth)
    {
        System.Text.StringBuilder qstring = new System.Text.StringBuilder();
        try
        {
            TransactionRecord tr = new TransactionRecord();

            string IpAddress = System.Configuration.ConfigurationManager.AppSettings["KoDTicketingIPAddress"];

            #region parsereference

            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("HDFC Transaction reference: " + trackid);
            string refNo = trackid;

            if (refNo.Length < 1)
            {
                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("HDFC Payment Response: Tokenization of reference string did not result in enough sub tokens. --> " + refNo);
                return "?err=pay";
            }

            tr.VLBookingID = refNo;

            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write(string.Format("HDFC Payment Response: Reference[{0}], Booking ID [{1}], Agent Code [{2}] ", tr.ReferenceNo.ToString(), tr.VLBookingID.ToString(), tr.AgentCode));
            #endregion parsereference

            if (Request["amt"] != null)
            {
                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("HDFC Amount: " + Request["amt"].ToString());
                tr.VLTotalAmount = decimal.Parse(Request["amt"].ToString());
            }

            if (Request["paymentid"] != null)
            {
                try
                {
                    Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("HDFC Receipt: " + Request["paymentid"].ToString());
                    tr.VLReceiptNo = Request["paymentid"].ToString();
                }
                catch (Exception ex)
                {
                    Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("HDFC Response: Error parsing receipt.");
                    Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write(ex.Message);
                }
            }


            if (result == "CAPTURED")
            {
                # region CAPTURED
                //string retURL = UpdateResponseByTranId(status, amount, transactionId);
                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("HDFC payment captured.");
                try
                {
                    string VLBookingID = tr.VLBookingID.ToString();
                    TransactionBOL.Get_Boty_Details(tr.VLBookingID, tr.VLReceiptNo);
                    Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Going to Fetch Details of Booking Id from DB.Booking ID where" + VLBookingID);
                    DataTable dt = TransactionBOL.Select_BotyTransaction(tr.VLBookingID.ToString());
                    Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Fetched Details of Booking Id from DB.Booking ID where" + VLBookingID);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Fetched Details of Booking Id from DB.Booking ID where" + dt.Rows[0]);
                        try
                        {

                            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Seats booked against HDFC payment.");
                            qstring.Append("?b=");
                            qstring.Append(tr.VLBookingID.ToString());
                           // ReceiptUtils.ValentinePaymentSuccess(dt.Rows[0], tr.VLReceiptNo.ToString(), tr.BookingID.ToString());
                            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("HDFC Payment successful..." + tr.VLBookingID.ToString() + tr.VLReceiptNo.ToString());
                            GTICKV.LogEntry("HDFC Payment successful...", "10", tr.BookingID.ToString(), tr.ReceiptNo.ToString());

                        }

                        catch (Exception ex)
                        {
                            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("HDFC Error processing receipt post booking. " + ex.Message);
                           // ReceiptUtils.SuccessPaymentResponse(tr.ReceiptNo.ToString(), dt.Rows[0], "");
                            //ReceiptUtils.SuccessPaymentResponse(tr.VLBookingID.ToString(), tr.VLReceiptNo);
                            qstring.Append((qstring.Length == 0) ? "?err=seat" : "&err=seat");
                        }
                        return qstring.ToString();

                    }
                    else
                    {
                        Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("HDFC successful booking but ask customer to call"); ;
                        long BookingID1 = long.Parse(tr.ReferenceNo.ToString());
                        //DataTable dt1 = TransactionBOL.Select_Temptransaction_REFIDWISE(BookingID1);
                        //ReceiptUtils.SuccessPaymentResponse(tr.ReceiptNo.ToString(), dt1.Rows[0], "");
                        //ReceiptUtils.SuccessPaymentResponse(tr.VLBookingID.ToString(), tr.VLReceiptNo);
                        qstring.Append((qstring.Length == 0) ? "?err=seat" : "&err=seat");
                    }
                }
                catch (Exception ex)
                {
                    Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("HDFC Receipt: error getting transaction details - " + ex.Message); qstring.Append((qstring.Length == 0) ? "?err=seat" : "&err=seat");
                }
                //if you reach here problem occurred

                String _refNo = tr.VLBookingID.ToString();
                KoDTicketing.GTICKV.LogEntry(_refNo, "HDFC Payment Not Successful", "17", tr.VLBookingID.ToString());
                //GTICKBOL.ON_Session_out(_refNo);
                KoDTicketing.GTICKV.LogEntry(_refNo, "Seats Unlocked", "18", tr.VLBookingID.ToString());
                qstring.Append("err=pay");

                KoDTicketing.GTICKV.LogEntry(tr.VLReceiptNo.ToString(), "HDFC Error Occurred -- Payment Not Successful", "19", tr.VLBookingID.ToString());
                string BookingID = tr.VLBookingID.ToString();
                try
                {
                    DataTable dt = TransactionBOL.Select_BotyTransaction(BookingID);

                    if (dt.Rows.Count == 0)
                    {
                        ReceiptUtils.FailurePaymentResponse();
                    }
                    else
                    {
                        ReceiptUtils.FailurePaymentResponse(dt.Rows[0]);
                    }
                    qstring.Append((qstring.Length == 1) ? "err=seat" : "&err=seat");
                    return qstring.ToString();
                }

                catch (Exception ex)
                {
                    Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("HDFC Error occurred processing unsuccessful payment..." + ex.Message);

                }
                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("HDFC Final Call");
                Response.Redirect(IpAddress + "Boty/HDFC/FinalCall.aspx" + qstring.ToString(), false);
                #endregion
            }
            else
            {
                GTICKV.LogEntry(tr.VLReceiptNo.ToString(), "HDFC Payment Not Successful", "17", tr.VLBookingID.ToString());
                //GTICKBOL.ON_Session_out(tr.ReferenceNo.ToString());
                GTICKV.LogEntry(tr.VLReceiptNo.ToString(), "Seats Unlocked", "18", tr.VLBookingID.ToString());
                long BookingID = long.Parse(tr.ReferenceNo.ToString());
                DataTable dt = TransactionBOL.Select_Temptransaction_REFIDWISE(BookingID);
                ReceiptUtils.PaymentNotCaptureResponse(tr.ReceiptNo.ToString(), dt.Rows[0], "");
                qstring.Append("?err=pay");
            }
        }
        catch (Exception ex)
        {
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Exception thrown processing HDFC receipt. " + ex.Message);
            qstring.Append("?err=pay");
        }
        return qstring.ToString();

    }
}