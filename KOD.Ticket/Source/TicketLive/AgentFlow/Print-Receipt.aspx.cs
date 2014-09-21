using System;
using System.Data;
using GenCode128;
using System.Web;
using KoDTicketing.BusinessLayer;

public partial class AgentFlow_Print_Receipt : System.Web.UI.Page
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

                            }
                            if (dr["PromotionCode"].ToString() == "VIVANTABYTAJ" || dr["PromotionCode"].ToString() == "OBEROI" || dr["PromotionCode"].ToString() == "TRIDENT" || dr["PromotionCode"].ToString() == "OBEROIDELHI" || dr["PromotionCode"].ToString() == "OCTOBERFEST")
                            {
                                lbltranamt.Text = dr["PayableAmount"].ToString() + "INR";
                            }
                            if (dr["PromotionCode"].ToString() == "OCTOBERFEST")
                            {
                                jhumroooffer.Visible = true;
                                lblpromo.Text = "OCTOBER FEST (Buy one get one free)";
                            }

                            if (dr["PromotionCode"].ToString() == "MMT")
                            {
                                DataTable dt1 = TransactionBOL.Select_MMTTransaction_REFIDWISE(BookingID.ToString());
                                DataRow dr1 = dt1.Rows[0];
                                lbltranamt.Text = decimal.Truncate(Convert.ToDecimal(dr1["PayableAmount"].ToString())) + " INR";
                            }
                            if (dr["PromotionCode"].ToString() == "MANA")
                            {
                                notes.Visible = false;
                                ManaNotes.Visible = true;
                                DataTable dt1 = TransactionBOL.Select_MANATransaction_REFIDWISE(BookingID.ToString());
                                DataRow dr1 = dt1.Rows[0];
                                lbltranamt.Text = decimal.Truncate(Convert.ToDecimal(dr1["PayableAmount"].ToString())) + " INR";
                            }
                            if (dr["PromotionCode"].ToString() == "MCOTHERS" || dr["PromotionCode"].ToString() == "MCWORLD")
                            {
                                DataTable dt1 = TransactionBOL.Select_McTransaction_REFIDWISE(dr["ReferenceNo"].ToString());
                                DataRow dr1 = dt1.Rows[0];
                                if (dr1["Type"].ToString() == "PACKAGE")
                                {
                                    notes.Visible = false;
                                    McNotes.Visible = true;
                                    lbltranamt.Text = decimal.Truncate(Convert.ToDecimal(dr1["PayableAmount"].ToString())) + " INR";
                                }
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
                            //printing bar code here
                            //toBarCode(BookingID.ToString());
                            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("[B] Printing Receipt Done for " + Request.QueryString["b"].ToString());
                        }
                        #endregion
                    }
                    else if (Request.QueryString["err"] != null)
                    {
                        Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Received Print Receipt with err value..." + Request.QueryString["err"].ToString());
                        string err = Request.QueryString["err"].ToString();
                        Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("err value : " + err);
                        if (err == "pay")
                        {
                            lblFinalMess.Text = "Your transaction was not successful. Please try later.";
                            if (Request["ErrorText"] != null)
                                lblFinalMess.Text += Environment.NewLine + "Transaction failed because" + Request["ErrorText"].ToString();
                        }
                        else if (err == "seat")
                        {
                            lblFinalMess.Text = "Your payment transaction was successful, but your seats were not booked due to technical glitch, please contact (0124)4528000 for Seat Confirmation. Sorry for inconvenience.";
                        }
                        dvDetails.Visible = false;
                        dvErrorDetail.Visible = true;
                        lblDate.Text = System.DateTime.Now.ToString();
                    }
                    else
                    {
                        Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Received Print Receipt without parameter...");
                        dvErrorDetail.Visible = true;
                        dvDetails.Visible = false;
                        lblFinalMess.Text = "Sorry your payment transaction was not successful, please try again after some time.";
                    }
                }
                lblDate.Text = "Date : " + System.DateTime.Now.ToString();
            }
        }
        catch (Exception ex)
        {
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Error Printing Receipt For " + ex.Message);
        }
        Session.Clear();

    }
    //private void toBarCode(String text)
    //{
    //    System.Drawing.Image myimage = Code128Rendering.MakeBarcodeImage(text, 1, true);
    //    myimage.Save(Server.MapPath("~/images/sample.jpg"), System.Drawing.Imaging.ImageFormat.Jpeg);
    //    img_BarCode.ImageUrl = "~/images/sample.jpg";
    //}
    protected void BtnBookMoreTickets_Click(object sender, EventArgs e)
    {
        Session.Clear();
        Response.Redirect("AgentLogin.aspx", false);
    }
}