using System;
using System.Data;
using System.Web;
using KoDTicketing.Utilities;
using KoDTicketing.BusinessLayer;
using KoDUtilities;

public partial class Payment_Web_ReturnReceipt : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        System.Text.StringBuilder qstring = new System.Text.StringBuilder("?");
        try
        {
            int qsCount = Request.QueryString.Count;

            if (qsCount > 0 && Request.QueryString["sta"] != null && Request.QueryString["tid"] != null)
            {
                TransactionRecord tr = new TransactionRecord();

                #region parsereference
                //tr.Status = Request.QueryString["sta"].ToString().Equals("0");
                tr.Status = false;
                if (Request.QueryString["sta"].ToString().Equals("0") && (Request.QueryString["ResultCode"].ToString().Equals("Y") || Request.QueryString["ResultCode"].ToString().Equals("M")))
                {
                    tr.Status = true;
                }
                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("AmEx Web payment transaction complete. Status: " + (tr.Status ? "Success" : "Failure"));

                String refNo = Request.QueryString["tid"].ToString();
                string[] refTokens = refNo.Split('_');
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
                        tr = objKODHelper.GetRoyalCardDetails(tr);
                        tr = objKODHelper.GetPromotionDetails(tr);
                        //****** Promo code/RoyalCard usecase END here ************


                        if (tr.TopUpAmount != 0)
                        {
                            TransactionBOL.Top_UP(tr.TopUpTransactionId);
                        }
                        tr.AvailedAmount += tr.TopUpAmount;
                        if (tr.AvailedAmount != 0 || tr.AvailedPoints != 0)
                        {
                            TransactionBOL.Redeem_Points(tr.RegId, tr.AvailedAmount, tr.AvailedPoints, tr.TotalAmount, tr.Play, tr.MobileNo, tr.ReferenceNo.ToString(), tr.TotalSeats);
                        } 
            
                        //****** Promo code usecase END here ************
                        DataTable dt = TransactionBOL.Get_Transaction_Detail(tr);
                        if (dt.Rows.Count == 0)
                        {
                            long BookingID1 = long.Parse(tr.ReferenceNo.ToString());
                            DataTable dt1 = TransactionBOL.Select_Temptransaction_REFIDWISE(BookingID1);
                            ReceiptUtils.SuccessPaymentResponse(tr.ReceiptNo.ToString(), dt1.Rows[0], "");
                            //ReceiptUtils.SuccessPaymentResponse(tr.BookingID.ToString(), tr.ReceiptNo);
                        }
                        else
                        {
                            bool seatsBooked = (int.Parse(dt.Rows[0]["SeatBooked"].ToString()) > 0);
                            bool alreadyProcessed = (dt.Rows[0]["AlreadyProcessed"].ToString() == "1");
                            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write(string.Format("Amex [{0},{1},{2}] Seats Booked.", tr.ReferenceNo.ToString(), tr.BookingID.ToString(), tr.ReceiptNo));
                            if (!alreadyProcessed)
                            {
                                ReceiptUtils.SuccessPaymentResponse(seatsBooked, dt.Rows[0], tr.ReferenceNo.ToString(), tr.BookingID.ToString(), tr.ReceiptNo, System.Configuration.ConfigurationManager.AppSettings["RoyalCardAdminID"]);
                            }
                            qstring.Clear();
                            qstring.Append("?b=");
                            qstring.Append((seatsBooked) ? dt.Rows[0]["BookingID"].ToString() : dt.Rows[0]["ID"].ToString());
                        }
                    }
                    catch (Exception ex)
                    {
                        Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Receipt: error getting transaction details - " + ex.Message);
                    }
                    #endregion handlesuccess
                }
                else //failure
                {
                    #region handlefailure
                    String _refNo = tr.ReferenceNo.ToString();
                    KoDTicketing.GTICKV.LogEntry(_refNo, "Payment Not Successful", "17", tr.BookingID.ToString());
                    GTICKBOL.ON_Session_out(_refNo);
                    KoDTicketing.GTICKV.LogEntry(_refNo, "Seats Unlocked", "18", tr.BookingID.ToString());
                    long BookingID = long.Parse(tr.ReferenceNo.ToString());
                    DataTable dt = TransactionBOL.Select_Temptransaction_REFIDWISE(BookingID);
                    ReceiptUtils.PaymentNotCaptureResponse(tr.ReceiptNo.ToString(), dt.Rows[0], "");
                    qstring.Append("err=pay");

                    KoDTicketing.GTICKV.LogEntry(tr.ReferenceNo.ToString(), "Error Occured -- Payment Not Successful", "19", tr.BookingID.ToString());
                    //long BookingID = long.Parse(tr.ReferenceNo.ToString());
                    try
                    {
                        //DataTable dt = TransactionBOL.Select_Temptransaction_REFIDWISE(BookingID);

                        if (dt.Rows.Count == 0)
                        {
                            ReceiptUtils.FailurePaymentResponse();
                        }
                        else
                        {
                            ReceiptUtils.FailurePaymentResponse(dt.Rows[0]);
                        }
 
                        qstring.Append((qstring.Length == 1) ? "err=seat" : "&err=seat");
                    }
                    catch (Exception ex)
                    {
                        Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Error occurred processing unsuccessful payment..." + ex.Message);
                    }
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

        Response.Redirect("~/RoyalCard/Account/Payment/Print-Receipt.aspx" + qstring.ToString(), false);
    }
}