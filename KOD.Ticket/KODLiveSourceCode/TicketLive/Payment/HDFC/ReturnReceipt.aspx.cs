using System;
using System.Data;
using KoDTicketing;
using KoDTicketing.Utilities;
using KoDTicketing.BusinessLayer;
using KoDUtilities;
using System.Configuration;

public partial class __Payment_Web_ReturnReceipt : System.Web.UI.Page
{
    public static string KoDTicketingIPAddress = System.Configuration.ConfigurationManager.AppSettings["KoDTicketingIPAddress"].ToString();

    private void SendNotificationMailForHotels(bool seatsBooked, DataRow dtRow,string referecneNo, string bookingId, string receiptNo,string Promotioncode)
    {
        // Fetch email Ids from config & Pass as params
        
        try
        {
            string GCabEmail1 = ConfigurationManager.AppSettings.Get("KODHotelPromoGCabsRefEmail1");
            string GCabEmail2 = ConfigurationManager.AppSettings.Get("KODHotelPromoGCabsRefEmail2");
            string KODrefEmail = ConfigurationManager.AppSettings.Get("KODHotelPromoKODRefEmail1");
            string KODrefEmail2 = ConfigurationManager.AppSettings.Get("KODHotelPromoKODRefEmail2");
            if (Promotioncode.ToString() != "" || Promotioncode != null)
            {
                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Mail send to GCAB and REFKOD");
                ReceiptUtils.SuccessPaymentResponseForHotels(seatsBooked, dtRow, referecneNo, bookingId, referecneNo, GCabEmail1, GCabEmail2, KODrefEmail,KODrefEmail2);
            }
        }
        catch (Exception ex)
        {

            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Error is "+ex.Message);
        }
   
       
    }
    
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
                    KoDTicketing.GTICKV.LogEntry(trackid.Split('_')[0], "Payment Getaway Response: " + Request["result"].ToString(), "14", trackid.Split('_')[1].Split('-')[0]);
                    /*******************Payement Gateway Error Value Code**********************/
                    #region PG_DB for ReturnReceipt
                    Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("PG_DB");
                    string bookingid = Request["trackid"].Split('_')[1].ToString().Split('-')[0].ToString();
                    int i = GTICKBOL.Insert_Payment_DB(Request["result"].ToString(), bookingid, "HDFC");
                    #endregion PG_DB for ReturnReceipt
                    /*********************End******************************/
                    string qstring = UpdateResponse(trackid, reference, result, postdate, auth);
                    if (!string.IsNullOrEmpty(qstring))
                    {
                        string redirectURL = "REDIRECT=" + KoDTicketingIPAddress + "Payment/Print-Receipt.aspx" + qstring;
                        Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("HDFC response redirection: " + redirectURL);
                        Response.Write(redirectURL);
                        return;
                    }
                    else
                        Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("HDFC response processing failed and resulting in empty receipt.");
                }
                else
                    Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("HDFC Response contains NO track ID");
                Response.Write("REDIRECT=" + KoDTicketingIPAddress + "Payment/Print-Receipt.aspx?err=pay");
            }
            else
            {
                String errorDetails = string.Format("paymentId[{0}], ErrorText[{1}], ErrorNo[{2}], udf1[{3}], udf2[{4}], udf3[{5}], udf4[{6}], udf5[{7}]",
                   paymentId, ErrorText, ErrorNo, udf1, udf2, udf3, udf4, udf5);
                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("HDFC Response: " + errorDetails);
                /*******************Payement Gateway Error Value Code**********************/
                #region PG_DB for ReturnReceipt
                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("PG_DB");
                string bookingid = Request["trackid"].Split('_')[1].ToString().Split('-')[0].ToString();
                int i = GTICKBOL.Insert_Payment_DB(ErrorText.ToString(), bookingid, "HDFC");
                #endregion PG_DB for ReturnReceipt
                /*********************End******************************/

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
                Response.Write("REDIRECT=" + KoDTicketingIPAddress + "Payment/Print-Receipt.aspx?err=pay");
            }
        }//!postback
    }

    protected String UpdateResponse( String trackid, String reference, String result, String postdate, String auth)
    {
        System.Text.StringBuilder qstring = new System.Text.StringBuilder();
        try
        {            
            TransactionRecord tr = new TransactionRecord();

            string IpAddress = System.Configuration.ConfigurationManager.AppSettings["KoDTicketingIPAddress"];

            #region parsereference
            //HDFC Track ID: 1000109173_1100065925-WEB

            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("HDFC Transaction reference: " + trackid);
            string refNo = trackid;
            string[] refTokens = refNo.Split(new char[] {'_','-'});
            if (refTokens.Length < 3)
            {
                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("HDFC Payment Response: Tokenization of reference string did not result in enough sub tokens. --> " + refNo);
                return "?err=pay";
            }
                
            tr.ReferenceNo = long.Parse(refTokens[0]);
            tr.BookingID = long.Parse(refTokens[1]);
            tr.AgentCode = refTokens[2];

            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write(string.Format("HDFC Payment Response: Reference[{0}], Booking ID [{1}], Agent Code [{2}] ", tr.ReferenceNo.ToString(), tr.BookingID.ToString(), tr.AgentCode));

            #endregion parsereference

            if (Request["amt"] != null)
            {
                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("HDFC Amount: " + Request["amt"].ToString());
                tr.TotalAmount = decimal.Parse(Request["amt"].ToString());
            }

            if (Request["paymentid"] != null)
            {
                try
                {
                    Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("HDFC Receipt: " + Request["paymentid"].ToString());
                    tr.ReceiptNo = Request["paymentid"].ToString();
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
                    //****** Promo code usecase start here ************
                    KODHelper objKODHelper = new KODHelper();
                    tr=objKODHelper.GetPromotionDetails(tr);
                    tr.PaymentGateway = "HDFC";
                    //****** Promo code usecase END here ************
                    TransactionBOL.Update_PaymentStatus(tr);     //Update payment status in temp transection table
                    DataTable dt = TransactionBOL.Get_Transaction_Detail(tr);

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        try
                        {
                            //String dbamount = dt.Rows[0]["TotalAmount"].ToString();
                            //String dbTrackID = dt.Rows[0]["BookingID"].ToString() + "_" + dt.Rows[0]["ReferenceNo"].ToString() + "-" + dt.Rows[0]["AgentCode"].ToString();
                            ////Validating the Booking Amount and Track ID
                            //if (Request.QueryString["amt"].ToString() == dbamount && Request.QueryString["trackid"].ToString() == dbTrackID)
                            //{
                                bool seatsBooked = (int.Parse(dt.Rows[0]["SeatBooked"].ToString()) > 0);
                                bool alreadyProcessed = (dt.Rows[0]["AlreadyProcessed"].ToString() == "1");
                                if (seatsBooked)
                                    Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Seats booked against HDFC payment.");
                                if (!alreadyProcessed)
                                {
                                    if (dt.Rows[0]["PromotionCode"].ToString() == "MMT")
                                    {
                                        DataTable dt1 = TransactionBOL.Select_MMTTransaction_REFIDWISE(tr.BookingID.ToString());
                                        ReceiptUtils.MMTPaymentResponse(dt.Rows[0], dt1.Rows[0], tr.ReceiptNo.ToString(), tr.BookingID.ToString(), ConfigurationManager.AppSettings.Get("ConcertRefMailId2"), ConfigurationManager.AppSettings.Get("ConcertRefMailId3"), dt.Rows[0]["ShowTime"].ToString(), dt.Rows[0]["ShowDate"].ToString());
                                    }
                                    else if (dt.Rows[0]["PromotionCode"].ToString() == "MANA")
                                    {
                                        DataTable dt2 = TransactionBOL.Select_MANATransaction_REFIDWISE(tr.BookingID.ToString());
                                        ReceiptUtils.MANAPaymentResponse(dt.Rows[0], dt2.Rows[0], tr.ReceiptNo.ToString(), tr.BookingID.ToString(), ConfigurationManager.AppSettings.Get("ConcertRefMailId2"), ConfigurationManager.AppSettings.Get("ConcertRefMailId3"), dt.Rows[0]["ShowTime"].ToString(), dt.Rows[0]["ShowDate"].ToString());
                                    }
                                    else if (dt.Rows[0]["PromotionCode"].ToString() == "MCOTHERS" || dt.Rows[0]["PromotionCode"].ToString() == "MCWORLD")
                                    {
                                        DataTable dt3 = TransactionBOL.Select_MCTransaction_REFIDWISE(tr.ReferenceNo.ToString());
                                        ReceiptUtils.MCPaymentResponse(dt.Rows[0], dt3.Rows[0], tr.ReceiptNo.ToString(), tr.BookingID.ToString(), ConfigurationManager.AppSettings.Get("ConcertRefMailId2"), ConfigurationManager.AppSettings.Get("ConcertRefMailId3"), dt.Rows[0]["ShowTime"].ToString(), dt.Rows[0]["ShowDate"].ToString());
                                    }
                                    else if (dt.Rows[0]["PromotionCode"].ToString() == "JHUMROOOFFER")
                                    {
                                        Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Enter into jhumroo offer mail content");
                                        ReceiptUtils.JHUMROOOFFERPaymentResponse(seatsBooked, dt.Rows[0], tr.ReferenceNo.ToString(), tr.BookingID.ToString(), tr.ReceiptNo, "");
                                    }
                                    else
                                        ReceiptUtils.SuccessPaymentResponse(seatsBooked, dt.Rows[0], tr.ReferenceNo.ToString(), tr.BookingID.ToString(), tr.ReceiptNo, "");
                                    Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Mail send through normal flow");
                                    Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Values are " + dt.Rows[0] + " , " + tr.ReferenceNo.ToString() + "," + tr.BookingID.ToString() + "," + tr.PromotionCode.ToString());
                                    SendNotificationMailForHotels(seatsBooked, dt.Rows[0], tr.ReferenceNo.ToString(), tr.BookingID.ToString(), tr.ReceiptNo, tr.PromotionCode.ToString());
                                    Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Mail send through Hotel flow");
                                }                               
                                qstring.Append("?b=");
                                qstring.Append((seatsBooked) ? dt.Rows[0]["BookingID"].ToString() : dt.Rows[0]["ID"].ToString());
                                GTICKV.LogEntry(tr.ReferenceNo.ToString(), "HDFC Payment successful...", "16", tr.BookingID.ToString(), tr.ReceiptNo.ToString());
                            //}
                            //else
                            //{
                            //    Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("HDFC Amounts mismatch customer asked to call to confirm transaction.");
                            //    ReceiptUtils.SuccessPaymentResponse(tr.BookingID.ToString(), tr.ReceiptNo);
                            //}
                        }
                        catch (Exception ex)
                        {
                            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("HDFC Error processing receipt post booking. "+ ex.Message);
                            if (dt != null && dt.Rows.Count > 0 && (Convert.ToDateTime(dt.Rows[0]["DateOfBooking"].ToString()) == Convert.ToDateTime(DateTime.Now.Date.ToShortDateString()) || Convert.ToDateTime(dt.Rows[0]["DateOfBooking"].ToString()) == Convert.ToDateTime(DateTime.Now.Date.AddDays(-1).ToShortDateString())))
                            {
                                ReceiptUtils.SuccessPaymentResponse(tr.ReceiptNo.ToString(), dt.Rows[0], "");
                            }
                            //ReceiptUtils.SuccessPaymentResponse(tr.BookingID.ToString(), tr.ReceiptNo);
                            qstring.Append((qstring.Length == 0) ? "?err=seat" : "&err=seat");
                        }
                    }
                    else
                    {
                        Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("HDFC successful booking but ask customer to call"); ;
                        long BookingID1 = long.Parse(tr.ReferenceNo.ToString());
                        DataTable dt2 = TransactionBOL.Select_Temptransaction_REFIDWISE(BookingID1);
                        if (dt2 != null && dt2.Rows.Count > 0 && (Convert.ToDateTime(dt2.Rows[0]["DateOfBooking"].ToString()) == Convert.ToDateTime(DateTime.Now.Date.ToShortDateString()) || Convert.ToDateTime(dt2.Rows[0]["DateOfBooking"].ToString()) == Convert.ToDateTime(DateTime.Now.Date.AddDays(-1).ToShortDateString())))
                        {
                            ReceiptUtils.SuccessPaymentResponse(tr.ReceiptNo.ToString(), dt2.Rows[0], "");
                        }
                        //ReceiptUtils.SuccessPaymentResponse(tr.BookingID.ToString(), tr.ReceiptNo);
                        qstring.Append((qstring.Length == 0) ? "?err=seat" : "&err=seat");
                    }

                    return qstring.ToString();
                }
                catch (Exception ex)
                {
                    Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("HDFC Receipt: error getting transaction details - " + ex.Message); qstring.Append((qstring.Length == 0) ? "?err=seat" : "&err=seat");
                }
                //if you reach here problem occurred

                String _refNo = tr.ReferenceNo.ToString();
                KoDTicketing.GTICKV.LogEntry(_refNo, "HDFC Payment Not Successful", "25", tr.BookingID.ToString());
                GTICKBOL.ON_Session_out(_refNo);
                KoDTicketing.GTICKV.LogEntry(_refNo, "Seats Unlocked", "26", tr.BookingID.ToString());
                qstring.Append("err=pay");

                KoDTicketing.GTICKV.LogEntry(tr.ReferenceNo.ToString(), "HDFC Error Occurred -- Payment Not Successful", "27", tr.BookingID.ToString());
                long BookingID = long.Parse(tr.ReferenceNo.ToString());
                try
                {
                    DataTable dt = TransactionBOL.Select_Temptransaction_REFIDWISE(BookingID);

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

                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("HDFC Final Call") ;
                Response.Redirect(IpAddress + "Payment/FinalCall.aspx" + qstring.ToString(), false);
#endregion
            } 
            else
            {
                GTICKV.LogEntry(tr.ReferenceNo.ToString(), "User Press Cancel Button", "15", tr.BookingID.ToString());
                GTICKV.LogEntry(tr.ReferenceNo.ToString(), "HDFC Payment Not Successful", "25", tr.BookingID.ToString());
                GTICKBOL.ON_Session_out(tr.ReferenceNo.ToString());
                GTICKV.LogEntry(tr.ReferenceNo.ToString(), "Seats Unlocked", "26", tr.BookingID.ToString());
                long BookingID = long.Parse(tr.ReferenceNo.ToString());
                DataTable dt = TransactionBOL.Select_Temptransaction_REFIDWISE(BookingID);
                if (dt != null && dt.Rows.Count > 0 && (Convert.ToDateTime(dt.Rows[0]["DateOfBooking"].ToString()) == Convert.ToDateTime(DateTime.Now.Date.ToShortDateString()) || Convert.ToDateTime(dt.Rows[0]["DateOfBooking"].ToString()) == Convert.ToDateTime(DateTime.Now.Date.AddDays(-1).ToShortDateString())))
                {
                    ReceiptUtils.PaymentNotCaptureResponse(tr.ReceiptNo.ToString(), dt.Rows[0], "");
                }
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