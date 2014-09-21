using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KoDTicketing;
using KoDTicketing.BusinessLayer;
using System.Data;

public partial class Royal_Card_Account_ContactDetails : System.Web.UI.Page
{
    public static string KoDTicketingIPAddress = System.Configuration.ConfigurationManager.AppSettings["KoDTicketingIPAddress"].ToString();
    public static string HDFCTransUrl = System.Configuration.ConfigurationManager.AppSettings["HDFCTransUrl"].ToString();
    public static string HDFCTransPortalID = System.Configuration.ConfigurationManager.AppSettings["HDFCTransPortalID"].ToString();
    public static string HDFCTranportalPwd = System.Configuration.ConfigurationManager.AppSettings["HDFCTranportalPwd"].ToString();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtEmailAddress.Text = Session["EmailID"].ToString();
            txtContactNo.Text = Session["MobileNo"].ToString();
            if (Session["PayableAmount"] != null && Session["PayableAmount"].ToString() == "0")
            {
                lblMode.Visible = false;
                ddlPaymentMode.Visible = false;
                re4.Visible = false;
                lblColon.Visible = false;
            }
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        long transid = 0;
        TransactionRecord tr = new TransactionRecord();
        try
        {
            #region Session based
            if (Session["seat_Val"] != null && Session["Seat_TransactionID"] != null)
            {
                try
                {
                    Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write(string.Format("Transaction [{0}]  Seats [{1}]", Session["Seat_TransactionID"].ToString(), Session["seat_Val"].ToString()));
                    tr.BookingID = long.Parse(Session["Seat_TransactionID"].ToString());
                    string[] strarr = Session["seat_Val"].ToString().Split(',');
                    if (Session["AgentCode"] != null)
                    {
                        tr.AgentCode = Session["AgentCode"].ToString();
                        tr.Source = "MSAGENT";
                    }
                    else
                    {
                        tr.AgentCode = "WEB";
                        tr.Source = "WEB";
                    }
                    tr.BookingType = "INDIVIDUAL";
                    //tr.VoucherType = rblVoucher.SelectedValue;
                    //tr.VoucherNo = "";
                    //tr.VoucherBookingID = 0;
                    tr.CardType = rbl_CardType.SelectedItem.Text;
                    tr.PaymentGateway = rbl_CardType.SelectedValue;
                    tr.CardNo = "1111222233334444";
                    tr.MobileNo = txtContactNo.Text;
                    tr.Name = Session["FirstName"].ToString() + Session["LastName"].ToString();
                    tr.PaymentType = ddlPaymentMode.SelectedItem.Text;
                    tr.DateOfBooking = DateTime.Now.Date.ToShortDateString();

                    bool istrue = emailsnd.Checked;
                    Session["Istrue"] = istrue;
                    tr.IsChecked = istrue;
                    tr.EmailID = txtEmailAddress.Text;

                    Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("IsChecked"+istrue);

                    tr.PlaceOfPick = "";
                    tr.TimeOfPick = ""; 
                    Session["Complimentary"] = "false";
                    tr.WantComplimentary = false;

                    tr.Status = false;
                    tr.TimeOfBooking = DateTime.Now.ToShortTimeString();

                    tr.TotalSeats = int.Parse(strarr[5].ToString());
                    tr.Category = strarr[8];
                    tr.Location = strarr[6];
                    tr.Play = strarr[1];
                    //replace "-"with "/"
                    //string[] datarr = strarr[2].ToString().Split('/'); // for live server
                    string[] datarr = strarr[2].ToString().Split('-'); // for dev/local
                    //replace datarr[0] to datarr[1]
                    //tr.ShowDate = datarr[1] + "/" + datarr[0] + "/" + datarr[2]; // for live server
                     tr.ShowDate = datarr[0] + "/" + datarr[1] + "/" + datarr[2]; // for dev/local

                    tr.ShowTime = strarr[7];
                    tr.Day = Convert.ToDateTime(tr.ShowDate).DayOfWeek.ToString();
                    // for dev/local, swap month & date above after day has been calculated above
                    //comment below two lines
                    //tr.ShowDate = tr.ShowDate.Replace(datarr[1], datarr[0]);
                    //tr.ShowDate = tr.ShowDate.Replace("30", datarr[1]);
                    tr.Remark = "";
                    tr.TotalAmount = GTICKBOL.Get_SeatPrice_SeatKeyNoWise(tr.BookingID);
                    Session["TotalAmount"] = tr.TotalAmount;
                    tr.SeatInfo = Session["Seat_info"].ToString();
                    tr.Address = Session["Address"].ToString().Trim();
                    //+ Session["Address2"].ToString()
                    tr.IP = GetIP();

                    //******Promotion code related changes START*****

                    if (Session["PromotionCode"] != null)
                    {
                        KoDTicketingLibrary.DTO.Promotion PromoSession = (KoDTicketingLibrary.DTO.Promotion)Session["PromotionCode"];
                        tr.PromotionCode = PromoSession.PromotionCode;
                        tr.DiscountPercentage = PromoSession.DiscountPercentage;
                        tr.WebPromotionId = PromoSession.WebPromotionId;
                        tr.DiscountedAmount = (GTICKBOL.Get_SeatPrice_SeatKeyNoWise(tr.BookingID) - (GTICKBOL.Get_SeatPrice_SeatKeyNoWise(tr.BookingID) * PromoSession.DiscountPercentage / 100));
                    }
                    //******Promotion code related changes END*****





                    //******RoyalCard related changes START*****

                    tr.RegId = Session["Regid"].ToString();
                    tr.AvailedAmount = Convert.ToDecimal(Session["RedeemBalance"]);
                    tr.AvailedPoints = Convert.ToDecimal(Session["RedeemPoints"]);
                    tr.TopUpAmount = tr.TotalAmount - (Convert.ToDecimal(Session["RedeemPoints"]) + Convert.ToDecimal(Session["RedeemBalance"]));
                    if (tr.TopUpAmount != 0)
                        tr.TopUpTransactionId = TransactionBOL.Card_Transaction(tr.RegId, tr.TopUpAmount, DateTime.Now, tr.RegId, 0);
                    tr.OptionalEmail = txtEmail.Text;
                    tr.OptionalContact = txtmobileno.Text;
                    //******RoyalCard related changes END*****



                    transid = TransactionBOL.Transaction_Temp_Insert(tr);
                }
                catch (Exception ex)
                {
                    Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Transaction Preparation Error: " + ex.Message);
                }
                GTICKV.LogEntry(tr.BookingID.ToString(), "Category : " + tr.Category + " ,Seat Info : " + tr.SeatInfo +
                    ", Total Amt : " + tr.TotalAmount, "6", "");

                //******Promotion code send discounted AMOUNT to payment gateway changes START*****
                if (Session["PromotionCode"] != null)
                {
                    KoDTicketingLibrary.DTO.Promotion ObjPromoSession = (KoDTicketingLibrary.DTO.Promotion)Session["PromotionCode"];
                    tr.TotalAmount = 0;
                    DataTable prices = GTICKBOL.Get_AllSeatPrice_SeatKeyNoWise(tr.BookingID);
                    if (prices != null)
                    {
                        foreach (DataRow dr in prices.Rows)
                        {
                            decimal SinglePrice = decimal.Parse(dr[0].ToString());
                            decimal DiscountedPrice = SinglePrice - (SinglePrice * ObjPromoSession.DiscountPercentage / 100);
                            DiscountedPrice = decimal.Truncate(DiscountedPrice);
                            if (DiscountedPrice == 1274)
                                DiscountedPrice = DiscountedPrice + 1;
                            else if (DiscountedPrice == 2124)
                                DiscountedPrice = DiscountedPrice + 1;
                            else if (DiscountedPrice == 2974)
                                DiscountedPrice = DiscountedPrice + 1;
                            else if (DiscountedPrice == 4249)
                                DiscountedPrice = DiscountedPrice + 1;
                            tr.TotalAmount += DiscountedPrice;
                            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Discounted Price For a Ticket" + DiscountedPrice.ToString());
                        }
                    }

                }
                //*******Promotion code send discounted AMOUNT to payment gateway changes END here **********


                tr.TotalAmount = (tr.TotalAmount) - (tr.AvailedPoints + tr.AvailedAmount);

                if (transid > 0)
                {
                    Session["AgentCode"] = null;
                    GTICKV.LogEntry(tr.BookingID.ToString(), "Data Successfully Written to Temp Transaction Table", "7", transid.ToString());
                    if (Session["PayableAmount"].ToString() == "0")
                    {
                        Session["BookingID"] = tr.BookingID;
                        Session["ID"] = transid.ToString();
                        Response.Redirect("Payment/Print-Receipt.aspx");
                    }
                    else
                    {
                        string URL = "";
                        //Pay Details , Sent To Loyalty Card Page --  CardType,TransID,Amt,ShowName
                        //Session["PayDetailsTemp"] = rblVoucher.SelectedValue + "|" + tr.BookingID.ToString() + "_" + transid + "~" + tr.AgentCode + "|" + tr.TotalAmount + "|" + tr.Play;
                        Session["PayDetailsTemp"] = tr.BookingID.ToString() + "_" + transid + "~" + tr.AgentCode + "|" + tr.TotalAmount + "|" + tr.Play;

                        if (ddlPaymentMode.SelectedValue == "CREDIT")
                        {
                            if (rbl_CardType.SelectedValue == "IDBI")
                            {
                                GTICKV.LogEntry(tr.BookingID.ToString(), "Sending to IDBI Payment Gateway", "8", transid.ToString());
                                URL = "../../Payment/Idbi/Default.aspx?type=idbi&transid=" + tr.BookingID.ToString() + "_" + transid + "~" + tr.AgentCode + "~royal_card_payment_idbi" + "&amt=" + tr.TotalAmount
                                    + "&show=" + tr.Play;
                            }
                            else if (rbl_CardType.SelectedValue == "AMEX")
                            {
                                GTICKV.LogEntry(tr.BookingID.ToString(), "Sending to AMEX Payment Gateway", "8", transid.ToString());
                                URL = "Payment/Web/Default.aspx?type=amex&transid=" + tr.BookingID.ToString() + "_" + transid + "~" + tr.AgentCode + "&amt=" + tr.TotalAmount
                                    + "&show=" + tr.Play;
                            }
                            else if (rbl_CardType.SelectedValue == "HDFC")
                            {

                                //string check = gb.HDFCLogCheck(transid.ToString()).Rows[0]["Amount"].ToString();
                                GTICKV.LogEntry(tr.BookingID.ToString(), "Sending to HDFC Payment Gateway", "8", transid.ToString());
                                string trackId, amount;
                                //Random Rnd = new Random();
                                //trackId = Rnd.Next().ToString();		//Merchant Track ID, this is as per merchant logic
                                trackId = tr.BookingID.ToString() + "_" + transid + "-" + tr.AgentCode;
                                Session["trackId"] = trackId;
                                amount = tr.TotalAmount.ToString();
                                Session["amount"] = amount;

                                String ErrorUrl = KoDTicketingIPAddress + "RoyalCard/Account/Payment/HDFC/Error.aspx";
                                String ResponseUrl = KoDTicketingIPAddress + "RoyalCard/Account/Payment/HDFC/ReturnReceipt.aspx";

                                //string qrystr = "id=" + HDFCTransPortalID + "&password=" + HDFCTranportalPwd + "&action=1&langid=USA&currencycode=356&amt=" + Server.UrlEncode(amount)
                                //    + "&responseURL=" + Server.UrlEncode(ResponseUrl) + "&errorURL=" + Server.UrlEncode(ErrorUrl)
                                //    + "&trackid=" + trackId
                                //    + "&udf1=TicketBooking&udf2=" + Server.UrlEncode(txtEmailAddress.Text.Trim())
                                //    + "&udf3=" + Server.UrlEncode(txtISDCode.Text + txtContactNo.Text) + "&udf4=" + Server.UrlEncode(txtAddress.Text.Trim()) + "&udf5=" + tr.BookingID;

                                string qrystr = "id=" + HDFCTransPortalID + "&password=" + HDFCTranportalPwd + "&action=1&langid=USA&currencycode=356&amt=" + amount
                                   + "&responseURL=" + Server.UrlEncode(ResponseUrl) + "&errorURL=" + Server.UrlEncode(ErrorUrl)
                                   + "&trackid=" + trackId
                                   + "&udf1=TicketBooking&udf2=" + txtEmailAddress.Text.Trim()
                                   + "&udf3=" + Server.UrlEncode(txtISDCode.Text.TrimStart('+') + txtContactNo.Text) + "&udf4=" + Server.UrlEncode(tr.Address.ToString()) + "&udf5=" + tr.BookingID.ToString();

                                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Preparing for HDFC Payment..." + qrystr);

                                //Writefile_new("\n***************Initial Request********************", Server.MapPath("~"));
                                //Writefile_new("\n\nDateTime:" + DateTime.Now.ToString("dd/MM/yy HH:mm:ss") + " Reference No:" + trackId + "Request XML:" + qrystr, Server.MapPath("~"));

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

                                //System.Net.CookieContainer responseCookiesContainer = new System.Net.CookieContainer();
                                //foreach (System.Net.Cookie cook in objResponse.Cookies)
                                //{
                                //    responseCookiesContainer.Add(cook);
                                //}

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

                                    //Writefile_new("\n***************Initial Response********************", Server.MapPath("~"));
                                    //Writefile_new("\n\nDateTime:" + DateTime.Now.ToString("dd/MM/yy HH:mm:ss") + " Reference No:" + trackId + "Request XML:" + NSDLval, Server.MapPath("~"));
                                    if (NSDLval.IndexOf("http") == -1)
                                    {
                                        lblMess.Text = "Payment cannot be processed with information provided.";
                                        return;
                                    }

                                    // gb.HDFCLog(transid.ToString(), "", trackId, "***Initial Response*** : " + NSDLval);                                
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

                            }//HDFC
                        }
                        //else if (ddlPaymentMode.SelectedValue == "VOUCHER")
                        //{
                        //    Session["PayDetailsTemp"] = rblVoucher.SelectedValue + "|" + tr.BookingID.ToString() + "_" + transid + "~" + tr.AgentCode + "|" + tr.TotalSeats;
                        //    URL = "~/Payment/Voucher/Voucher.aspx";
                        //}
                        Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Payment Redirection: " + URL);
                        Response.Redirect(URL, false);

                    }
                }
                else
                {
                    lblMess.Text = "Session Timeout. Please start the transaction again by clicking \"Back\" button";
                    Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Session Timeout. Need to restart transaction");
                }
            }
            else //no Session[seat_val]
            {
                ClientScript.RegisterStartupScript(GetType(), "myscript", "<script>alert('Session Timeout. Please start the transaction again');window.location.href='TicketBooking.aspx';</script>");
            }
            #endregion
        }
        catch (Exception ex)
        {
            GTICKV.LogEntry(tr.BookingID.ToString(), "Error Occurred - " + ex.Message.Replace("'", ""), "8", transid.ToString());
        }
    }
    protected void btnBackHome_Click(object sender, EventArgs e)
    {
        if (Session["Seat_TransactionID"] != null)
        {
            String KeyNo = Session["Seat_TransactionID"].ToString();
            GTICKBOL.ON_Session_out(KeyNo);
        }
        string password = "A87C7B95932E9";
        String RoyalBal = Session["RBal"].ToString();
        String RoyalPoints = Session["RPoints"].ToString();
        String FN = Session["FirstName"].ToString();
        String LN = Session["LastName"].ToString();
        String Email = Session["EmailID"].ToString();
        String MobNo = Session["MobileNo"].ToString();
        String Address = Session["Address"].ToString();
        String RegID = Session["Regid"].ToString();
        Response.Redirect("TicketBooking.aspx?RemainingAmount=" + Server.UrlEncode(Common.Encrypt(RoyalBal, password)) + "&RemainingPoints=" + Server.UrlEncode(Common.Encrypt(RoyalPoints, password)) + "&FirstName=" + Server.UrlEncode(Common.Encrypt(FN, password)) + "&LastName=" + Server.UrlEncode(Common.Encrypt(LN, password)) + "&Email=" + Server.UrlEncode(Common.Encrypt(Email, password)) + "&Mobile=" + Server.UrlEncode(Common.Encrypt(MobNo, password)) + "&Address=" + Server.UrlEncode(Common.Encrypt(Address, password)) + "&MemberShipId=" + Server.UrlEncode(Common.Encrypt(RegID, password)), false);
    }

    public string Encrypt(string val)
    {
        var bytes = System.Text.Encoding.UTF8.GetBytes(val);
        var encBytes = System.Security.Cryptography.ProtectedData.Protect(bytes, new byte[0], System.Security.Cryptography.DataProtectionScope.LocalMachine);
        return Convert.ToBase64String(encBytes);
    }
    //Get IP Address
    protected string GetIP()
    {
        string ipaddress;
        ipaddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (ipaddress == "" || ipaddress == null)
            ipaddress = Request.ServerVariables["REMOTE_ADDR"];
        return ipaddress;
    }
}