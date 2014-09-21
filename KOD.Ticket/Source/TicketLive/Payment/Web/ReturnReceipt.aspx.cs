using System;
using System.Data;
using System.Web;
using KoDTicketing.Utilities;
using KoDTicketing.BusinessLayer;
using KoDUtilities;
using System.Configuration;

public partial class Payment_Web_ReturnReceipt : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        System.Text.StringBuilder qstring = new System.Text.StringBuilder("?");
        try
        {
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Amex Return Receipt Page");
            int qsCount = Request.QueryString.Count;

            if (qsCount > 0 && Request.QueryString["sta"] != null && Request.QueryString["tid"] != null)
            {
                TransactionRecord tr = new TransactionRecord();

                #region parsereference
                //tr.Status = Request.QueryString["sta"].ToString().Equals("0");
                tr.Status = false;
                if (Request.QueryString["enroll"].ToString().Equals("Y"))
                {
                    if (Request.QueryString["sta"].ToString().Equals("0") && (Request.QueryString["Safecode"].ToString().Equals("Y") || Request.QueryString["Safecode"].ToString().Equals("A")))
                    {
                        tr.Status = true;
                    }
                }
                else
                {
                    if (Request.QueryString["sta"].ToString().Equals("0") && Request.QueryString["response"].ToString().Equals("M"))
                    {
                        tr.Status = true;
                    }
                }
                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("AmEx Web payment transaction complete. Status: " + (tr.Status ? "Success" : "Failure"));
                String refNo = Request.QueryString["tid"].ToString();
                string[] refTokens = refNo.Split('_');
                KoDTicketing.GTICKV.LogEntry(refTokens[0], "Payment Getaway Response: " + (tr.Status ? "Success" : "Failure"), "14", "");
                if (refTokens.Length < 2)
                {
                    Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("AmEx Response from gateway received but query string does not have information about the transaction refernece: " + refNo);
                    return;
                }

                tr.ReferenceNo = long.Parse(refTokens[0].ToString());
                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("AmEx Transaction reference: " + tr.ReferenceNo.ToString());

                string[] refSubTokens = refTokens[1].Split('~');
                if (refTokens.Length < 2)
                {
                    Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("AmEx Tokenization of query string did not result in enough sub tokens. --> " + refNo);
                    return;
                }

                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("AmExBooking ID: " + refSubTokens[0]);
                tr.BookingID = long.Parse(refSubTokens[0]);

                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("AmEx Agent Code: " + refSubTokens[1]);
                tr.AgentCode = refSubTokens[1];
                #endregion parsereference

                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("AmEx Amount: " + Request.QueryString["amt"].ToString());
                tr.TotalAmount = decimal.Parse(Request.QueryString["amt"].ToString());

                if (true == tr.Status) //successful
                {
                    #region handlesuccess

                    #region parsereceipt
                    if (Request.QueryString["rec"] != null)
                    {
                        try
                        {
                            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("AmEx Receipt: " + Request.QueryString["rec"]);
                            tr.ReceiptNo = Request.QueryString["rec"];
                        }
                        catch (Exception ex)
                        {
                            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Amex Resposne: Error parsing receipt.");
                            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write(ex.Message);

                        }
                    }
                    #endregion parsereceipt

                    try
                    {
                        Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("AmEX Payment Successful. Ensuring the seats are reserved...");
                        //****** Promo code usecase start here ************
                        KODHelper objKODHelper = new KODHelper();
                        tr = objKODHelper.GetPromotionDetails(tr);
                        tr.PaymentGateway = "AMEX";
                        //****** Promo code usecase END here ************
                        TransactionBOL.Update_PaymentStatus(tr);   //Update payment status in temp transection table
                        DataTable dt = TransactionBOL.Get_Transaction_Detail(tr);
                        
                        if (dt.Rows.Count == 0)
                        {
                            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("AMEX successful booking but ask customer to call");
                            long BookingID1 = long.Parse(tr.ReferenceNo.ToString());
                            DataTable dt1 = TransactionBOL.Select_Temptransaction_REFIDWISE(BookingID1);
                            if (dt1 != null && dt1.Rows.Count > 0 && (Convert.ToDateTime(dt1.Rows[0]["DateOfBooking"].ToString()) == Convert.ToDateTime(DateTime.Now.Date.ToShortDateString()) || Convert.ToDateTime(dt1.Rows[0]["DateOfBooking"].ToString()) == Convert.ToDateTime(DateTime.Now.Date.AddDays(-1).ToShortDateString())))
                            {
                                ReceiptUtils.SuccessPaymentResponse(tr.ReceiptNo.ToString(), dt1.Rows[0], "");
                            }
                            //ReceiptUtils.SuccessPaymentResponse(tr.BookingID.ToString(), tr.ReceiptNo);
                            qstring.Append("?err=seat");
                        }
                        else
                        {
                            bool seatsBooked = (int.Parse(dt.Rows[0]["SeatBooked"].ToString()) > 0);
                            bool alreadyProcessed = (dt.Rows[0]["AlreadyProcessed"].ToString() == "1");
                            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write(string.Format("Amex [{0},{1},{2}] Seats Booked.", tr.ReferenceNo.ToString(), tr.BookingID.ToString(), tr.ReceiptNo));
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
                                else if (dt.Rows[0]["PromotionCode"].ToString() == "FAMILYOFFER")
                                {
                                    DataTable dtfamilyoffer = TransactionBOL.Select_FAMILYOFFERTransaction_REFIDWISE(tr.BookingID.ToString());
                                    ReceiptUtils.FAMILYOFFERPaymentResponse(dt.Rows[0], dtfamilyoffer.Rows[0], tr.ReceiptNo.ToString(), tr.BookingID.ToString(), ConfigurationManager.AppSettings.Get("ConcertRefMailId2"), ConfigurationManager.AppSettings.Get("ConcertRefMailId3"), dt.Rows[0]["ShowTime"].ToString(), dt.Rows[0]["ShowDate"].ToString());
                                }
                                else if (dt.Rows[0]["PromotionCode"].ToString() == "JHUMROOOFFER")
                                {
                                    ReceiptUtils.JHUMROOOFFERPaymentResponse(seatsBooked, dt.Rows[0], tr.ReferenceNo.ToString(), tr.BookingID.ToString(), tr.ReceiptNo, "");
                                }
                                else
                                {
                                    ReceiptUtils.SuccessPaymentResponse(seatsBooked, dt.Rows[0], tr.ReferenceNo.ToString(), tr.BookingID.ToString(), tr.ReceiptNo, "");
                                }
                                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Mail send through normal flow");
                                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Values are " + dt.Rows[0] + " , " + tr.ReferenceNo.ToString() + "," + tr.BookingID.ToString() + "," + tr.PromotionCode.ToString());
                                SendNotificationMailForHotels(seatsBooked, dt.Rows[0], tr.ReferenceNo.ToString(), tr.BookingID.ToString(), tr.ReceiptNo, tr.PromotionCode.ToString());
                                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Mail send through Hotel flow");
                            }
                            qstring.Clear();
                            qstring.Append("?b=");
                            qstring.Append((seatsBooked) ? dt.Rows[0]["BookingID"].ToString() : dt.Rows[0]["ID"].ToString());
                            KoDTicketing.GTICKV.LogEntry(tr.ReferenceNo.ToString(), "AMEX Payment successful...", "16", tr.BookingID.ToString(), tr.ReceiptNo.ToString());
                        }
                    }
                    catch (Exception ex)
                    {
                        Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Receipt: error getting transaction details - " + ex.Message);
                        String _refNo = tr.ReferenceNo.ToString();

                        KoDTicketing.GTICKV.LogEntry(tr.ReferenceNo.ToString(), "Amex Error Occured -- Payment Not Successful", "27", tr.ReferenceNo.ToString());
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
                            qstring.Append("?err=seat");
                        }
                        catch (Exception ex1)
                        {
                            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Error occurred processing unsuccessful payment through Amex..." + ex1.Message);
                        }
                    }
                    #endregion handlesuccess
                }
                else //failure
                {
                    #region handlefailure
                    String _refNo = tr.ReferenceNo.ToString();
                    KoDTicketing.GTICKV.LogEntry(_refNo, "Payment Not Successful", "25", tr.BookingID.ToString());
                    GTICKBOL.ON_Session_out(_refNo);
                    KoDTicketing.GTICKV.LogEntry(_refNo, "Seats Unlocked", "26", tr.BookingID.ToString());
                    long BookingID = long.Parse(tr.ReferenceNo.ToString());
                    DataTable dt = TransactionBOL.Select_Temptransaction_REFIDWISE(BookingID);
                    if (dt != null && dt.Rows.Count > 0 && (Convert.ToDateTime(dt.Rows[0]["DateOfBooking"].ToString()) == Convert.ToDateTime(DateTime.Now.Date.ToShortDateString()) || Convert.ToDateTime(dt.Rows[0]["DateOfBooking"].ToString()) == Convert.ToDateTime(DateTime.Now.Date.AddDays(-1).ToShortDateString())))
                    {
                        ReceiptUtils.PaymentNotCaptureResponse(tr.ReceiptNo.ToString(), dt.Rows[0], "");
                    }
                    qstring.Append("err=pay");
                    #endregion handlefailure
                }
            }
            else
            {
                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Response from gateway received but query string does not have information about the transaction.");
                qstring.Append((qstring.Length == 1) ? "err=seat" : "&err=seat");
            }
        }
        catch (Exception ex)
        {
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Exception thrown processing receipt. " + ex.Message);
            qstring.Append((qstring.Length == 1) ? "err=seat" : "&err=seat");
        }

        Response.Redirect("~/Payment/Print-Receipt.aspx" + qstring.ToString(), false);
    }

    //Function to send email to Gcab and KodRef
    private void SendNotificationMailForHotels(bool seatsBooked, DataRow dtRow, string referecneNo, string bookingId, string receiptNo, string Promotioncode)
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
                ReceiptUtils.SuccessPaymentResponseForHotels(seatsBooked, dtRow, referecneNo, bookingId, referecneNo, GCabEmail1, GCabEmail2, KODrefEmail, KODrefEmail2);
            }
        }
        catch (Exception ex)
        {

            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Error is " + ex.Message);
        }
    }
}