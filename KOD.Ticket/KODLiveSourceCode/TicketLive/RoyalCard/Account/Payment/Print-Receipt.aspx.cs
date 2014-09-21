using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KoDTicketing.BusinessLayer;
using System.Data;
using GenCode128;
using KoDTicketing.Utilities;
using System.Data.SqlClient;

public partial class Royal_Card_Account_Payment_Print_Receipt : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Request.UrlReferrer != null)
                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write(Request.UrlReferrer.ToString());

            if (!IsPostBack)
            {
                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Printing Receipt Parameters: " + Request.QueryString.Count.ToString());
                if (Session["bookid"] != null) //Voucher
                {
                    string bookid = Session["bookid"].ToString();
                    Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Printing Receipt for purchase by voucher. Booking Id: " + bookid);
                    Session["bookid"] = null;
                    Session.Abandon();
                    Server.Transfer("Print-Receipt.aspx" + bookid);
                }
                else
                {
                    if (Request.QueryString["b"] != null)
                    {
                        #region paramB
                        Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("[B] Printing Receipt For  " + Request.QueryString["b"].ToString());

                        long BookingID = long.Parse(Request.QueryString["b"].ToString());
                        DataTable dt = TransactionBOL.Select_Temptransaction_transactionIDWise(BookingID);
                        if (dt.Rows.Count > 0)
                        {
                            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("[B] Printing Receipt with transaction details for  " + Request.QueryString["b"].ToString());
                            dvDetails.Visible = true;
                            dvErrorDetail.Visible = false;

                            DataRow dr = dt.Rows[0];
                            int chkstatus = int.Parse(dr["SeatBooked"].ToString());
                            lblVenue.Text = "Kingdom of Dreams, Gurgaon";
                            lblshowname.Text = dr["play"].ToString();
                            lblSeatInfo.Text = dr["Category"] + " - " + dr["SeatInfo"].ToString();
                            lblPayMode.Text = dr["PaymentType"].ToString();
                            //lbltranamt.Text = dr["TotalAmount"].ToString() + " INR";


                            decimal DiscountPercentage = decimal.Parse(dr["DiscountPercentage"].ToString());
                            decimal tktAmount = decimal.Parse(dr["TotalAmount"].ToString());
                            int numberOfSeats = int.Parse(dr["TotalSeats"].ToString());
                            if (DiscountPercentage > 0)
                            {
                                decimal amtAfterDeduction = 0;
                                decimal SingleTicketPrice = tktAmount / numberOfSeats;

                                decimal DiscountedPrice = SingleTicketPrice - (SingleTicketPrice * DiscountPercentage / 100);
                                DiscountedPrice = decimal.Truncate(DiscountedPrice);
                                if (DiscountedPrice == 1274)
                                    DiscountedPrice = DiscountedPrice + 1;
                                else if (DiscountedPrice == 2124)
                                    DiscountedPrice = DiscountedPrice + 1;
                                else if (DiscountedPrice == 2974)
                                    DiscountedPrice = DiscountedPrice + 1;
                                else if (DiscountedPrice == 4249)
                                    DiscountedPrice = DiscountedPrice + 1;

                                amtAfterDeduction = DiscountedPrice * numberOfSeats;

                                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Total Amount Price For a Ticket" + amtAfterDeduction.ToString());

                                lbltranamt.Text = Convert.ToString(amtAfterDeduction) + " INR";
                            }
                            else
                            {
                                lbltranamt.Text = dr["TotalAmount"].ToString() + " INR";
                                //added two fields
                                lblCardType.Text = Session["PayableAmount"].ToString() + "INR";
                                String Points = Session["RedeemPoints"].ToString();
                                String Bal = Session["RedeemBalance"].ToString();
                                Double RBal = Convert.ToDouble(Bal);
                                Double RPoints = Convert.ToDouble(Points);
                                //Double Total = RBal + RPoints;
                                lblroyalcard.Text = RBal.ToString();
                                lblroyalPoints.Text = RPoints.ToString();
                            }







                            if (chkstatus > 0)
                            {
                                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("[B] Printing Receipt with seats booked for " + Request.QueryString["b"].ToString());
                                lblBookingID.Text = dr["BookingID"].ToString();
                                lblIdbiReceiptno.Text = dr["ReceiptNo"].ToString();
                                lbltransid.Text = dr["ReferenceNo"].ToString();
                            }
                            else
                            {
                                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("[B] Printing Receipt with NO seats booked for " + Request.QueryString["b"].ToString());
                                dvErrorDetail.Visible = true;
                                lblIdbiReceiptno.Text = dr["ReferenceNo"].ToString();
                                lbltransid.Text = dr["BookingID"].ToString();
                                lblFinalMess.Text = "Your Transaction was successful, but your seats were not booked for some technical reason, please contact 0124 - 4528000 for Seat Confirmation";
                            }
                            lbltrnsresponse.Text = "Transaction Successful";
                            lblBookTime.Text = Convert.ToDateTime(dr["DateOfBooking"]).ToLongDateString() + " at " + Convert.ToDateTime(dr["TimeOfBooking"]).ToShortTimeString();
                            lblShowDaTE.Text = Convert.ToDateTime(dr["ShowDate"]).ToLongDateString() + " at " + Convert.ToDateTime(dr["ShowTime"]).ToShortTimeString();
                            
                            
                            ////*********Reedem Value when Top up the Payable Amount*********//
                            //string[] strarr = Session["seat_Val"].ToString().Split(',');
                            //Session["TotalSeats"] = int.Parse(strarr[5].ToString());

                            //string RegID = Session["Regid"].ToString();
                            //Decimal RedeemAmount = decimal.Parse(dr["TopUpAmount"].ToString());
                            //Decimal RedeemPoints = decimal.Parse(dr["AvailedPoints"].ToString());
                            //Decimal TotalAmount = decimal.Parse(dr["TotalAmount"].ToString());
                            //string Play = dr["play"].ToString();
                            //string CustomerNo = Session["MobileNo"].ToString();
                            //string ReferenceNO = (long.Parse(dr["BookingID"].ToString())).ToString();
                            //int NoOfTickets = Convert.ToInt32(Session["TotalSeats"].ToString());
                            //Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Print Receipt 2 redeem");
                            //TransactionBOL.Redeem_Points(RegID, RedeemAmount, RedeemPoints, TotalAmount, Play, CustomerNo, ReferenceNO, NoOfTickets);
                            ////****************//
                            
                            //printing bar code here
                            toBarCode(BookingID.ToString());
                            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("[B] Printing Receipt Done for " + Request.QueryString["b"].ToString());
                        }
                        #endregion
                    }
                    else if (Request.QueryString["err"] != null)
                    {
                        Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Received Print Receipt with err value..." + Request.QueryString["err"].ToString());
                        string err = Request.QueryString["err"].ToString();
                        if (err == "pay")
                        {
                            lblFinalMess.Text = "Your transaction was not successful. Please try later.";
                            if (Request["ErrorText"] != null)
                                lblFinalMess.Text += Environment.NewLine + "Transaction failed because" + Request["ErrorText"].ToString();
                        }
                        else if (err == "seat")
                        {
                            lblFinalMess.Text = "Your payment transaction was successful, but your seats were not booked due to technical glitch, please contact (0124)4528000 for Seat Confirmation. Sorry for inconvenience.";
                            //*********Reedem Value when Top up the Payable Amount*********//
                            //long BookingID = long.Parse(Session["BookingID"].ToString());
                            //DataTable dt = TransactionBOL.Select_Temptransaction_transactionIDWise(BookingID);
                            //DataRow dr = dt.Rows[0];
                            //string RegID = Session["Regid"].ToString();
                            //Decimal RedeemAmount = decimal.Parse(dr["TopUpAmount"].ToString());
                            //Decimal RedeemPoints = decimal.Parse(dr["AvailedPoints"].ToString());
                            //Decimal TotalAmount = decimal.Parse(dr["TotalAmount"].ToString());
                            //string Play = dr["play"].ToString();
                            //string CustomerNo = Session["MobileNo"].ToString();
                            //string ReferenceNO = (long.Parse(dr["BookingID"].ToString())).ToString();
                            //int NoOfTickets = Convert.ToInt32(Session["TotalSeats"].ToString());
                            //Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Print Receipt 3 redeem");
                            //TransactionBOL.Redeem_Points(RegID, RedeemAmount, RedeemPoints, TotalAmount, Play, CustomerNo, ReferenceNO, NoOfTickets);
                            ////****************//
                        }
                        dvDetails.Visible = false;
                        dvErrorDetail.Visible = true;
                        lblDate.Text = System.DateTime.Now.ToString();
                    }
                    else
                    {
                        Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Payable Amount Session = "+ Session["PayableAmount"].ToString());
                        if (Session["PayableAmount"] != null || Session["PayableAmount"].ToString() == "0")
                        {
                            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("[B] Printing Receipt For  " + Session["ID"].ToString());

                            long BookingID = long.Parse(Session["ID"].ToString());


                            TransactionRecord tr = new TransactionRecord();
                            tr.BookingID = BookingID;
                            tr.AgentCode = "WEB";
                            tr.ReferenceNo = long.Parse(Session["BookingID"].ToString());

                            string[] strarr = Session["seat_Val"].ToString().Split(',');
                            Session["TotalSeats"] = int.Parse(strarr[5].ToString());

                            string RegID = Session["Regid"].ToString();
                            Decimal RedeemAmount = Convert.ToDecimal(Session["RedeemBalance"].ToString());
                            Decimal RedeemPoints = Convert.ToDecimal(Session["RedeemPoints"].ToString());
                            Decimal TotalAmount = Convert.ToDecimal(Session["TotalAmount"].ToString());
                            string Play = Session["play_Val"].ToString();
                            string CustomerNo = Session["MobileNo"].ToString();
                            string ReferenceNO = (long.Parse(Session["BookingID"].ToString())).ToString();
                            int NoOfTickets = Convert.ToInt32(Session["TotalSeats"].ToString());
                            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Print Receipt 1 redeem");
                            TransactionBOL.Redeem_Points(RegID, RedeemAmount, RedeemPoints, TotalAmount, Play, CustomerNo, ReferenceNO, NoOfTickets);
                            DataTable dtTr = TransactionBOL.Get_Transaction_Detail(tr);

                            if (dtTr != null && dtTr.Rows.Count > 0)
                            {
                                try
                                {
                                    bool seatsBooked = (int.Parse(dtTr.Rows[0]["SeatBooked"].ToString()) > 0);
                                    bool alreadyProcessed = (dtTr.Rows[0]["AlreadyProcessed"].ToString() == "1");
                                    if (!alreadyProcessed)
                                    {
                                        ReceiptUtils.SuccessPaymentResponse(seatsBooked, dtTr.Rows[0], tr.ReferenceNo.ToString(), tr.BookingID.ToString(), tr.ReceiptNo, System.Configuration.ConfigurationManager.AppSettings["RoyalCardAdminID"]);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("RoyalCard Payment Receipt - No Money Payed" + ex.Message);
                                    //ReceiptUtils.SuccessPaymentResponse(tr.BookingID.ToString(), tr.ReceiptNo);
                                    ReceiptUtils.SuccessPaymentResponse(tr.ReceiptNo.ToString(), dtTr.Rows[0], "");
                                }
                            }

                            DataTable dt = TransactionBOL.Select_Temptransaction_transactionIDWise(BookingID);
                            if (dt.Rows.Count > 0)
                            {

                                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("[B] Printing Receipt with transaction details for  " + Session["ID"].ToString());
                                dvDetails.Visible = true;
                                dvErrorDetail.Visible = false;

                                DataRow dr = dt.Rows[0];
                                int chkstatus = int.Parse(dr["SeatBooked"].ToString());
                                lblBookingID.Text = dr["BookingID"].ToString();
                                lbltransid.Text = dr["ReferenceNo"].ToString();
                                lblVenue.Text = "Kingdom of Dreams, Gurgaon";
                                lblshowname.Text = dr["play"].ToString();
                                lblShowDaTE.Text = Convert.ToDateTime(dr["ShowDate"]).ToLongDateString() + " at " + Convert.ToDateTime(dr["ShowTime"]).ToShortTimeString();
                                lblSeatInfo.Text = dr["Category"] + " - " + dr["SeatInfo"].ToString();
                                lblIdbiReceiptno.Text = dr["ReceiptNo"].ToString();
                                lblPayMode.Text = "RoyalCard";
                                lbltranamt.Text = dr["TotalAmount"].ToString() + " INR";
                                lblCardType.Text = Session["PayableAmount"].ToString() + " INR";
                                String Points = Session["RedeemPoints"].ToString();
                                String Bal = Session["RedeemBalance"].ToString();
                                Double RBal = Convert.ToDouble(Bal);
                                Double RPoints = Convert.ToDouble(Points);
                                //Double Total = RBal + RPoints;
                                lblroyalcard.Text = RBal.ToString();
                                lblroyalPoints.Text = RPoints.ToString();
                                lblBookTime.Text = Convert.ToDateTime(dr["DateOfBooking"]).ToLongDateString() + " at " + Convert.ToDateTime(dr["TimeOfBooking"]).ToShortTimeString();
                                lbltrnsresponse.Text = "Transaction Successful";

                                toBarCode(BookingID.ToString());
                                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("[B] Printing Receipt Done for " + Session["ID"].ToString());

                            }
                        }
                        else
                        {
                            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Received Print Receipt without parameter...");
                            dvErrorDetail.Visible = true;
                            dvDetails.Visible = false;
                            lblFinalMess.Text = "Sorry your payment transaction was not successful, please try again after some time.";
                        }
                    }
                }
                lblDate.Text = "Date : " + System.DateTime.Now.ToString();
            }
        }
        catch (Exception ex)
        {
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Error Printing Receipt For " + ex.Message);
        }

    }
    private void toBarCode(String text)
    {
        System.Drawing.Image myimage = Code128Rendering.MakeBarcodeImage(text, 1, true);
        myimage.Save(Server.MapPath("~/images/sample.jpg"), System.Drawing.Imaging.ImageFormat.Jpeg);
        img_BarCode.ImageUrl = "~/images/sample.jpg";
    }
    protected void BtnBackToHome_Click(object sender, System.EventArgs e)
    {
        Response.Redirect("http://royalty.kingdomofdreams.in/Account/UserCard.aspx");
    }
    protected void BtnMoreTickets_Click(object sender, System.EventArgs e)
    {
        string password = "A87C7B95932E9";
        String RoyalBal = (Convert.ToDouble(Session["RBal"].ToString())-Convert.ToDouble(Session["RedeemBalance"].ToString())).ToString();
        String RoyalPoints = (Convert.ToDouble(Session["RPoints"].ToString()) - Convert.ToDouble(Session["RedeemPoints"].ToString())).ToString();
        String FN = Session["FirstName"].ToString();
        String LN = Session["LastName"].ToString();
        String Email = Session["EmailID"].ToString();
        String MobNo = Session["MobileNo"].ToString();
        String Address = Session["Address"].ToString();
        String RegID = Session["Regid"].ToString();
        Response.Redirect("../TicketBooking.aspx?RemainingAmount=" + Server.UrlEncode(Common.Encrypt(RoyalBal, password)) + "&RemainingPoints=" + Server.UrlEncode(Common.Encrypt(RoyalPoints, password)) + "&FirstName=" + Server.UrlEncode(Common.Encrypt(FN, password)) + "&LastName=" + Server.UrlEncode(Common.Encrypt(LN, password)) + "&Email=" + Server.UrlEncode(Common.Encrypt(Email, password)) + "&Mobile=" + Server.UrlEncode(Common.Encrypt(MobNo, password)) + "&Address=" + Server.UrlEncode(Common.Encrypt(Address, password)) + "&MemberShipId=" + Server.UrlEncode(Common.Encrypt(RegID, password)), false);
    }
    public string Encrypt(string val)
    {
        var bytes = System.Text.Encoding.UTF8.GetBytes(val);
        var encBytes = System.Security.Cryptography.ProtectedData.Protect(bytes, new byte[0], System.Security.Cryptography.DataProtectionScope.LocalMachine);
        return Convert.ToBase64String(encBytes);
    }
}