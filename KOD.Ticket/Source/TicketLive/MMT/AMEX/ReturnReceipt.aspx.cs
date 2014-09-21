using System;
using System;
using System.Data;
using System.Web;
using KoDTicketing.Utilities;
using KoDTicketing.BusinessLayer;
using KoDUtilities;
using System.Configuration;

public partial class MMT_AMEX_ReturnReceipt : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        System.Text.StringBuilder qstring = new System.Text.StringBuilder("?");
        try
        {
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Amex Return Receipt Page MMT");
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
               // string[] refTokens = refNo.Split('_');
                if (refNo.Length < 1)
                {
                    Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("AmEx Response from gateway received but query string does not have information about the transaction refernece: " + refNo);
                    return;
                }

               // tr.ReferenceNo = long.Parse(refTokens[0].ToString());
                //Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("AmEx Transaction reference: " + tr.ReferenceNo.ToString());

               // string[] refSubTokens = refTokens[1].Split('~');
                //if (refTokens.Length < 2)
                //{
                //    Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("AmEx Tokenization of query string did not result in enough sub tokens. --> " + refNo);
                //    return;
                //}

                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("AmExBooking ID: " + refNo);
                tr.MMTBookingID = refNo.ToString();

               // Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("AmEx Agent Code: " + refSubTokens[1]);
                //tr.AgentCode = refSubTokens[1];
                #endregion parsereference

                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("AmEx Amount: " + Request.QueryString["amt"].ToString());
                tr.MMTPayableAmount = decimal.Parse(Request.QueryString["amt"].ToString());

                if (true == tr.Status) //successful
                {
                    #region handlesuccess

                    #region parsereceipt
                    if (Request.QueryString["rec"] != null)
                    {
                        try
                        {
                            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("AmEx Receipt: " + Request.QueryString["rec"]);
                            tr.MMTReceiptNo = Request.QueryString["rec"];
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
                        //KODHelper objKODHelper = new KODHelper();
                        //tr = objKODHelper.GetPromotionDetails(tr);
                        //****** Promo code usecase END here ************
                        TransactionBOL.Get_MMT_Detail(tr.MMTBookingID, tr.MMTReceiptNo);
                        DataTable dt = TransactionBOL.Select_MMTTransaction_REFIDWISE(tr.MMTBookingID.ToString());
                        if (dt.Rows.Count == 0)
                        {
                            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("AMEX successful booking but ask customer to call");
                            long BookingID1 = long.Parse(tr.ReferenceNo.ToString());
                            DataTable dt1 = TransactionBOL.Select_Temptransaction_REFIDWISE(BookingID1);
                            ReceiptUtils.SuccessPaymentResponse(tr.ReceiptNo.ToString(), dt1.Rows[0], "");
                            //ReceiptUtils.SuccessPaymentResponse(tr.BookingID.ToString(), tr.ReceiptNo);
                            qstring.Append("?err=seat");
                        }
                        else
                        {
                            //bool seatsBooked = (int.Parse(dt.Rows[0]["SeatBooked"].ToString()) > 0);
                            //bool alreadyProcessed = (dt.Rows[0]["AlreadyProcessed"].ToString() == "1");
                            //Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write(string.Format("Amex [{0},{1},{2}] Seats Booked.", tr.ReferenceNo.ToString(), tr.BookingID.ToString(), tr.ReceiptNo));
                            
                                ReceiptUtils.MMTPaymentResponse(dt.Rows[0],dt.Rows[0], tr.MMTReceiptNo.ToString(), tr.BookingID.ToString(), ConfigurationManager.AppSettings.Get("ConcertRefMailId2"), ConfigurationManager.AppSettings.Get("ConcertRefMailId3"),"","");
                                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Mail send through normal flow");
                                //Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Values are " + dt.Rows[0] + " , " + tr.MMTReceiptNo.ToString() + "," + tr.BookingID.ToString());
                                //Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Mail send through Hotel flow");
                            
                            qstring.Clear();
                            qstring.Append("?b=");
                            qstring.Append(dt.Rows[0]["BookingId"].ToString());
                        }
                    }
                    catch (Exception ex)
                    {
                        Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Receipt: error getting transaction details - " + ex.Message);
                        //String _refNo = tr.ReferenceNo.ToString();

                        KoDTicketing.GTICKV.LogEntry(tr.ReferenceNo.ToString(), "Amex Error Occured -- Payment Not Successful", "19", tr.MMTBookingID.ToString());
                        //long BookingID = long.Parse(tr.ReferenceNo.ToString());
                        string BookingID = tr.MMTBookingID.ToString();
                        try
                        {
                            DataTable dt = TransactionBOL.Select_MMTTransaction_REFIDWISE(BookingID);

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
                            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Error occurred processing unsuccessful payment through IDBI..." + ex1.Message);
                        }
                    }
                    #endregion handlesuccess
                }
                else //failure
                {
                    #region handlefailure
                    String _refNo = tr.MMTBookingID.ToString();
                   // KoDTicketing.GTICKV.LogEntry(_refNo, "Payment Not Successful", "17", tr.MMTBookingID.ToString());
                    //GTICKBOL.ON_Session_out(_refNo);
                    //KoDTicketing.GTICKV.LogEntry(_refNo, "Seats Unlocked", "18", tr.BookingID.ToString());
                    long BookingID = long.Parse(tr.ReferenceNo.ToString());
                    DataTable dt = TransactionBOL.Select_Temptransaction_REFIDWISE(BookingID);
                    ReceiptUtils.PaymentNotCaptureResponse(tr.ReceiptNo.ToString(), dt.Rows[0], "");
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

        Server.Transfer("~/MMT/Print-Receipt.aspx" + qstring.ToString(), false);

    }
}