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

public partial class Payment_Idbi_CR : System.Web.UI.Page
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
            string[] refTokens = refNo.Split('_');
            KoDTicketing.GTICKV.LogEntry(refTokens[0], "Payment Getaway Response: " + (tr.Status ? "Success" : "Failure"), "14", "");
            if (refTokens.Length < 2)
            {
                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("IDBI Payment Response: Tokenization of reference string did not result in enough tokens. --> " + refNo);
                return;
            }

            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("IDBI Transaction reference: " + refTokens[0].ToString());
            tr.ReferenceNo = long.Parse(refTokens[0].ToString());

            string[] refSubTokens = refTokens[1].Split('~');

            if (refSubTokens.Length < 2)
            {
                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("IDBI Payment Response: Tokenization of reference substring did not result in enough sub tokens. --> " + refTokens[1].ToString());
                return;
            }

            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("IDBI Booking ID: " + refSubTokens[0]);
            tr.BookingID = long.Parse(refSubTokens[0]);

            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("IDBI Agent Code: " + refSubTokens[1]);
            tr.AgentCode = refSubTokens[1];
            #endregion parsereference
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("IDBI Amount: " + Request.QueryString["amt"].ToString());
            tr.TotalAmount = decimal.Parse(Request.QueryString["amt"].ToString());
            try
            {
                if (true == tr.Status)
                {
                    HandleSuccess(ref tr, ref qstring);
                }
                else //failure
                {
                    GTICKV.LogEntry(tr.ReferenceNo.ToString(), "Payment Not Successful", "25", tr.BookingID.ToString());
                    GTICKBOL.ON_Session_out(tr.ReferenceNo.ToString());
                    GTICKV.LogEntry(tr.ReferenceNo.ToString(), "Seats Unlocked", "26", tr.BookingID.ToString());
                    long BookingID = long.Parse(tr.ReferenceNo.ToString());
                    GTICKV.LogEntry(tr.ReferenceNo.ToString(), "User Press Cancel Button", "15", tr.BookingID.ToString());
                    Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("IDBI: Press cancel button");
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
                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write(ex.Message);
                HandleFailure(tr);
                qstring.Append("?err=seat");
            }
        }

 #endregion processreceipt

        Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("IDBI Transaction Complete. " + Request.QueryString["rec"]);
        Response.Redirect("../Print-Receipt.aspx" + qstring.ToString(), false);
    }

    protected void HandleSuccess(ref TransactionRecord tr, ref System.Text.StringBuilder qstring)
    {
        qstring.Clear();

        #region handlesuccess
        GTICKV.LogEntry(tr.ReferenceNo.ToString(), "IDBI Payment successful...", "16", tr.BookingID.ToString(), tr.ReceiptNo.ToString());

        #region parsereceipt
        if (Request.QueryString["rec"] != null)
        {
            try
            {
                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("IDBI Receipt: " + Request.QueryString["rec"]);
                tr.ReceiptNo= Request.QueryString["rec"].ToString();
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

            //****** Promo code usecase start here ************
            KODHelper objKODHelper = new KODHelper();
            tr = objKODHelper.GetPromotionDetails(tr);
            tr.PaymentGateway = "IDBI";
            //****** Promo code usecase END here ************
            TransactionBOL.Update_PaymentStatus(tr);   //Update payment status in temp transection table
            DataTable dt = TransactionBOL.Get_Transaction_Detail(tr);

            if (dt != null && dt.Rows.Count > 0)
            {
                bool seatsBooked = (dt.Rows[0]["SeatBooked"].ToString() == "1");
                bool alreadyProcessed = (dt.Rows[0]["AlreadyProcessed"].ToString() == "1");
                if (seatsBooked)
                    Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("IDBI Transaction : Seats Booked for " + tr.BookingID.ToString());
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
            }
            else
            {
                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("IDBI successful booking but ask customer to call");
                long BookingID1 = long.Parse(tr.ReferenceNo.ToString());
                DataTable dt1 = TransactionBOL.Select_Temptransaction_REFIDWISE(BookingID1);
                if (dt1 != null && dt1.Rows.Count > 0 && (Convert.ToDateTime(dt1.Rows[0]["DateOfBooking"].ToString()) == Convert.ToDateTime(DateTime.Now.Date.ToShortDateString()) || Convert.ToDateTime(dt1.Rows[0]["DateOfBooking"].ToString()) == Convert.ToDateTime(DateTime.Now.Date.AddDays(-1).ToShortDateString())))
                {
                    ReceiptUtils.SuccessPaymentResponse(tr.ReceiptNo.ToString(), dt1.Rows[0], "");
                }
                //ReceiptUtils.SuccessPaymentResponse(tr.BookingID.ToString(), tr.ReceiptNo);
                qstring.Append("?err=seat");
            }
        }
        catch (Exception ex)
        {
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("IDBI Receipt: error getting transaction details - " + ex.Message);
            HandleFailure(tr);
            long BookingID1 = long.Parse(tr.ReferenceNo.ToString());
            DataTable dt1 = TransactionBOL.Select_Temptransaction_REFIDWISE(BookingID1);
            if (dt1 != null && dt1.Rows.Count > 0 && (Convert.ToDateTime(dt1.Rows[0]["DateOfBooking"].ToString()) == Convert.ToDateTime(DateTime.Now.Date.ToShortDateString()) || Convert.ToDateTime(dt1.Rows[0]["DateOfBooking"].ToString()) == Convert.ToDateTime(DateTime.Now.Date.AddDays(-1).ToShortDateString())))
            {
                ReceiptUtils.SuccessPaymentResponse(tr.ReceiptNo.ToString(), dt1.Rows[0], "");
            }
            qstring.Append("?err=seat");
        }
        #endregion handlesuccess
    }

    protected void HandleFailure(TransactionRecord tr)
    {
        #region handlefailure

        String _refNo = tr.ReferenceNo.ToString();

        KoDTicketing.GTICKV.LogEntry(tr.ReferenceNo.ToString(), "IDBI Error Occured -- Payment Not Successful", "27", tr.ReferenceNo.ToString());
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
        }
        catch (Exception ex)
        {
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Error occurred processing unsuccessful payment through IDBI..." + ex.Message);
        }
        #endregion handlefailure
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
    //protected void UpdateResponseByTranId(String statuscode, String Status, String ReceiptNo)
    //{
    //    string qstring = "";

    //    try
    //    {
    //        TransactionRecord tr = new TransactionRecord();
    //        if (statuscode.ToString().Equals("50020"))
    //        {
    //            GTICKV.LogEntry(merchantReferenceNo, "IDBI Payment successful...", "10", MainBookingID, ReceiptNo);
    //            tr.BookingID = long.Parse(MainBookingID);
    //            tr.ReceiptNo = ReceiptNo;
    //            tr.ReferenceNo = long.Parse(merchantReferenceNo);
    //            tr.AgentCode = AgentCode;
    //            DataTable dt = TransactionBOL.Get_Transaction_Detail(tr);
    //            if (dt.Rows.Count > 0)
    //            {
    //                DataRow dr = dt.Rows[0];
    //                Mail.MailData amiail = new Mail.MailData();
    //                amiail.to = dr["EmailID"].ToString();
    //                amiail.toName = dr["Name"].ToString();
    //                amiail.from = "sales@kingdomofdreams.co.in";
    //                amiail.fromName = "Kingdom of Dreams";
    //                amiail.fromUID = "17230_thegreat_smtp";
    //                amiail.fromPwd = "t1h2e3g4";
    //                amiail.smtpServer = "smtp.qlc.co.in";
    //                amiail.subject = "Booking Receipt";

    //                string BMess = "";
    //                BMess += "Dear " + dr["Name"] + "<br/><br/>";
    //                if (dr["SeatBooked"].ToString() == "1")
    //                {
    //                    BMess += "Your Tickets have been successfully booked, and details are mentioned below...<br/><br/>";
    //                    BMess += "Booking ID : " + dr["BookingID"] + "<br/>";
    //                }
    //                else
    //                {
    //                    BMess += "Your Transaction was successful, but due to some technical reason your seats were not Booked." +
    //                           " Please Contact 0124 - 4528000 to confirm your seats. Below are the details...<br/><br/>";
    //                    BMess += "Booking Id : " + tr.BookingID + "<br/>";
    //                    BMess += "Receipt No : " + tr.ReceiptNo + "<br/>";
    //                }
    //                BMess += "Venue : Kingdom Of Dreams, Gurgaon<br/>";
    //                BMess += "Location : NCR<br/>";
    //                BMess += "Show Name : " + dr["Play"] + "<br/>";
    //                BMess += "Show Date : " + Convert.ToDateTime(dr["ShowDate"].ToString()).ToLongDateString() +
    //                    " at " + Convert.ToDateTime(dr["ShowTime"]).ToShortTimeString() + "<br/>";
    //                BMess += "Seat Info : " + dr["Category"] + " - " + dr["SeatInfo"] + "<br/>";
    //                BMess += "Total Amount : " + dr["TotalAmount"] + "<br/><br/>";
    //                BMess += "Payment Mode : " + dr["PaymentType"] + "<br/>";
    //                BMess += "Booking Date : " + Convert.ToDateTime(dr["DateOfBooking"].ToString()).ToLongDateString() +
    //                    " at " + Convert.ToDateTime(dr["TimeOfBooking"]).ToShortTimeString() + "<br/><br/>";
    //                if (dr["SeatBooked"].ToString() == "1")
    //                {
    //                    BMess += "Please Bring this Booking ID to the Auditorium to collect your tickets" +
    //                       " and also you need to present the same credit card on which the booking has" +
    //                       " been done, if tickets booked with credit card.<br/><br/>" +
    //                       "<b>CANCELLATION/REFUND POLICY<b/><br/>As per policy we do not cancel/refund/change any tickets once booked." +
    //                       "<br/><br/>";
    //                }
    //                BMess += "Regards,<br/>Team<br/>Kingdom of Dreams";
    //                amiail.bodyMessage = BMess;
    //                Mail mail = new Mail();
    //                mail.sendMail_Net(amiail);

    //                GTICKV.LogEntry(merchantReferenceNo, "Mail Sent", "15", dr["BookingID"].ToString());
    //                if (dr["SeatBooked"].ToString() == "1")
    //                {
    //                    BMess = "Your BookingID:" + dr["BookingID"] + " have been confirmed for the date " +
    //                        Convert.ToDateTime(dr["ShowDate"]).ToShortDateString() + " at " + Convert.ToDateTime(dr["ShowTime"]).ToShortTimeString() +
    //                        " For the \"" + dr["Play"] + "\". Please arrive 30mins before the show starts. Rgds, KOD Team";
    //                    qstring = "?b=" + dr["BookingID"].ToString();
    //                }
    //                else
    //                {
    //                    BMess = "BookingID: " + tr.BookingID + ", and receiptNo: " + tr.ReceiptNo +
    //                     ", Please Contact 0124 - 4528000 for Seat Confirmation";
    //                    qstring = "?b=" + dr["ID"].ToString();
    //                }
    //                sendSMS sms = new sendSMS();

    //                sms.SendSMS_Sender(dr["MobileNo"].ToString(), BMess, "KOD");
    //                GTICKV.LogEntry(merchantReferenceNo, "SMS Sent", "16", dr["BookingID"].ToString());

    //            }
    //            else
    //            {
    //                qstring += "?err=seat";
    //            }
    //        }
    //        else
    //        {
    //            GTICKV.LogEntry(merchantReferenceNo, "Payment Not Successful", "17", MainBookingID);
    //            GTICKBOL.ON_Session_out(merchantReferenceNo);
    //            GTICKV.LogEntry(merchantReferenceNo, "Seats Unlocked", "18", MainBookingID);
    //            qstring = "?err=pay";
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        if (qstring == "")
    //            qstring += "?err=seat";
    //        else
    //            qstring += "&err=seat";
    //        //GTICKET.GTICKV.LogEntry(merchantReferenceNo.ToString(), DateTime.Now + " -- Event: Error Occured " + ex.Message);
    //        GTICKV.LogEntry(merchantReferenceNo, "Error Occured --" + ex.Message.Replace("'", ""), "19", "");

    //        Mail.MailData errmaildata = new Mail.MailData();
    //        errmaildata.to = "comcenter@kingdomofdreams.co.in";
    //        //errmaildata.to = "neeraj.verma@gcell.in";
    //        errmaildata.from = "sales@kingdomofdreams.co.in";
    //        errmaildata.fromName = "Kingdom of Dreams";
    //        errmaildata.fromUID = "17230_thegreat_smtp";
    //        errmaildata.fromPwd = "t1h2e3g4";
    //        errmaildata.smtpServer = "smtp.qlc.co.in";

    //        String MailBod = "<b><u>This is a system generated message.</u></b><br/>";
    //        MailBod += "Dear Sir/Madam,<br/>" +
    //           "System has encountered an error while web-booking and the corresponding information is given below.<br/><br/>";

    //        long BookingID = long.Parse(merchantReferenceNo);
    //        DataTable dt = TransactionBOL.Select_Temptransaction_REFIDWISE(BookingID);
    //        if (dt.Rows.Count > 0)
    //        {
    //            DataRow dr = dt.Rows[0];
    //            errmaildata.subject = "BOOKING ERROR - " + dr["Name"].ToString();
    //            MailBod += "<b>User Details :</b><br/>";
    //            MailBod += "Name          : <b>" + dr["Name"] + "</b><br/>";
    //            MailBod += "Contact No    : <b>" + dr["MobileNo"] + "</b><br/>";
    //            MailBod += "Email Address : <b>" + dr["EmailId"] + "</b><br/>";
    //            MailBod += "Booking Date  : <b>" + Convert.ToDateTime(dr["DateOfBooking"].ToString()).ToLongDateString() +
    //                " at " + Convert.ToDateTime(dr["TimeOfBooking"]).ToShortTimeString() + "</b><br/>";
    //            MailBod += "<hr/>";
    //            MailBod += "<b>Show/Seats Selection Details :</b><br/>";
    //            MailBod += "Show               : <b>" + dr["Play"] + "</b><br/>";
    //            MailBod += "Show Date          : <b>" + Convert.ToDateTime(dr["ShowDate"].ToString()).ToLongDateString() +
    //                " at " + Convert.ToDateTime(dr["ShowTime"]).ToShortTimeString() + "</b><br/>";
    //            MailBod += "Category           : <b>" + dr["Category"] + "</b><br/>";
    //            MailBod += "Total No. of Seats : <b>" + dr["TotalSeats"] + "</b><br/>";
    //            MailBod += "Seat Details       : <b>" + dr["SeatInfo"] + "</b><br/>";
    //            MailBod += "<hr/><br/>";
    //        }
    //        else
    //        {
    //            MailBod += "No User Details Available<br/>No Seat information available<br/>";
    //        }
    //        MailBod += "<b>Payment Details :</b><br/>";
    //        if (statuscode == "0")
    //            MailBod += "Payment Status  : <b>Successful</b><br/>";
    //        else
    //            MailBod += "Payment Status  : <b>NOT Successful</b><br/>";
    //        MailBod += "Payment Gateway : <b>AMEX</b><br/>";
    //        MailBod += "Receipt No      : <b>" + ReceiptNo + "</b><br/><br/>";
    //        MailBod += "<b>Total Amount : INR " + Status + "</b><br/>";
    //        MailBod += "Please note that the above seats are not yet confirmed, please book the above seats and inform the customer accordingly.";
    //        MailBod += "<br/><br/>Thanks.";
    //        errmaildata.bodyMessage = MailBod;
    //        Mail mail = new Mail();
    //        mail.sendMail_Net(errmaildata);
    //        GTICKV.LogEntry(merchantReferenceNo, "Mail Sent to Com Center", "20", MainBookingID);
    //    }
    //    finally
    //    {
    //        Server.Transfer("../Print-Receipt.aspx" + qstring, false);
    //    }
    //}
}
