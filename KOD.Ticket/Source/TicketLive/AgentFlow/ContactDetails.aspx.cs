using System;
using System.Web;
using KoDTicketing;
using KoDTicketing.BusinessLayer;
using System.Data;
using System.Collections.Generic;
using KoDTicketing.Utilities;
using KoDUtilities;

public partial class AgentFlow_ContactDetails : System.Web.UI.Page
{
    string agent = "";
    protected String Session_value = "";
    protected String SeatVal = "";
    protected DataTable dtseatval;
    protected string[] sessionvalue;
    public string hotel = "";
    public string Decrypt(string val)
    {
        val = val.Replace(" ", "+");
        var bytes = Convert.FromBase64String(val);
        var encBytes = System.Security.Cryptography.ProtectedData.Unprotect(bytes, new byte[0], System.Security.Cryptography.DataProtectionScope.LocalMachine);
        return System.Text.Encoding.UTF8.GetString(encBytes);
    }
    public string Encrypt(string val)
    {
        var bytes = System.Text.Encoding.UTF8.GetBytes(val);
        var encBytes = System.Security.Cryptography.ProtectedData.Protect(bytes, new byte[0], System.Security.Cryptography.DataProtectionScope.LocalMachine);
        return Convert.ToBase64String(encBytes);
    }
    public static string KoDTicketingIPAddress = System.Configuration.ConfigurationManager.AppSettings["KoDTicketingIPAddress"].ToString();
    public static string HDFCTransUrl = System.Configuration.ConfigurationManager.AppSettings["HDFCTransUrl"].ToString();
    public static string HDFCTransPortalID = System.Configuration.ConfigurationManager.AppSettings["HDFCTransPortalID"].ToString();
    public static string HDFCTranportalPwd = System.Configuration.ConfigurationManager.AppSettings["HDFCTranportalPwd"].ToString();
    public static long BookingID;
    protected void Page_Load(object sender, EventArgs e)
    {
        Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write(Decrypt(Request.QueryString["SessionId"]));
        agent = Session["Agent"].ToString();
        if (Session[Decrypt(Request.QueryString["SessionId"])].ToString() != "")
        {
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write(Decrypt(Request.QueryString["SessionId"]));
            if (Request.QueryString["SessionId"] != null || Request.QueryString["SessionId"] != "")
            {
                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write(Decrypt(Request.QueryString["SessionId"]));
                DataTable dtseatval = TransactionBOL.Select_ShowDetails(Convert.ToInt64(Decrypt(Request.QueryString["SessionId"].ToString())));
                Session_value = dtseatval.Rows[0]["Seat_Val"].ToString();
                sessionvalue = Session_value.Split(',');
                SeatVal = dtseatval.Rows[0]["Seat_Info"].ToString();
                hotel = sessionvalue[10];
            }
        }
        else
        {
            Session.Clear();
            Response.Redirect("Default.aspx", false);
        }
        //*******************Promo**************************//
        decimal TotalAmount = 0;
        bool isfilled = false;
        if (Request.QueryString["SessionId"] != null || Request.QueryString["SessionId"].ToString() != "")
        {
            //BookingID = long.Parse(Session["Seat_TransactionID"].ToString());
            //TotalAmount = GTICKBOL.Get_SeatPrice_SeatKeyNoWise(BookingID);
            //Session["TotalAmount"] = decimal.Truncate(TotalAmount);
            string[] strarr = null;
            if (Session_value != "")
            {
                strarr = Session_value.Split(',');
                isfilled = false;
            }
            if (strarr.Length < 6)
            {
                throw new Exception("Contact details page loading cannot be done as session value is no valid Session: " + (isfilled ? Session_value : Session_value));
            }
            string category = strarr[8].ToString();
            string SingleSeatPrice = strarr[9].ToString().Split('.')[1].ToString();
            TotalAmount = Convert.ToDecimal(strarr[9].ToString().Split('.')[1].ToString()) * Convert.ToDecimal(strarr[5].ToString());
            Session["TotalAmount"] = decimal.Truncate(TotalAmount);
            string ShowDate = "";
            string[] datarr = strarr[2].ToString().Split('/');//for live server
            ShowDate = datarr[1] + "/" + datarr[0] + "/" + datarr[2];//for live server

            //string[] datarr = strarr[2].ToString().Split('-');// for dev/local
            //ShowDate = datarr[0] + "/" + datarr[1] + "/" + datarr[2]; // for dev/local

            string day = Convert.ToDateTime(ShowDate).DayOfWeek.ToString().ToUpper();
            //ShowDate = datarr[1] + "/" + datarr[0] + "/" + datarr[2];//for local
            Session["day"] = day;
            if (strarr[10].ToString() == "")
            {
                lblttlAmt.Visible = true;
                lblttlAmt.Text = "Total Payable Amount : Rs. " + Session["TotalAmount"].ToString();
            }
        }
        else
        {
            lblttlAmt.Visible = false;
            lblpayAmt.Visible = false;
            if (sessionvalue[12] != null)
            {
                String KeyNo = Decrypt(sessionvalue[12]);
                GTICKBOL.ON_Session_out(KeyNo);
            }
            Response.Redirect("Default.aspx", false);
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (Decrypt(Request.QueryString["SessionId"]) == sessionvalue[12].ToString() && Session[Decrypt(Request.QueryString["SessionId"])] != "")
        {
            long transid = 0;
            long refno=0;
            TransactionRecord tr = new TransactionRecord();
            try
            {
                #region Session based
                if (Session_value != "" && Request.QueryString["SessionId"] != null)
                {
                    try
                    {
                        string[] strarr;
                        strarr = Session_value.Split(',');
                        Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write(string.Format("Transaction [{0}]  ", strarr[12]));
                        tr.BookingID = long.Parse(strarr[12]);
                        Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write(string.Format("Seats  " + Session_value));
                       // tr.AgentCode = Session["AgentCode"].ToString();
                        tr.AgentCode = "Agent-" + Session["Agent"].ToString();
                        tr.Source = "Web-TicketingAgent";
                        tr.BookingType = "Web-TicketingAgent";
                        //tr.VoucherType = rblVoucher.SelectedValue;
                        //tr.VoucherNo = "";
                        //tr.VoucherBookingID = 0;

                        tr.CardType = "";
                        tr.PaymentGateway = "Web-TicketingAgent";
                        tr.CardNo = "1111222233334444";
                        tr.MobileNo = txtContactNo.Text;
                        tr.Name = txtName.Text;
                        tr.PaymentType = "";
                        tr.DateOfBooking = DateTime.Now.Date.ToShortDateString();
                       // tr.IsProcessed = ;
                       // tr.PaymentStatus = ;
                        tr.router = "";
                        tr.WantComplimentary = false;
                        tr.WantComplimentaryDrop = false;
                        tr.PlaceOfDrop = "";
                        tr.PlaceOfPick = "";
                        tr.TimeOfPick = "";
                        tr.TimeOfDrop = "";
                        tr.EmailID = txtEmailAddress.Text;
                        tr.Status = false;
                        tr.TimeOfBooking = DateTime.Now.ToShortTimeString();
                        tr.TotalSeats = int.Parse(strarr[5].ToString());
                        tr.Category = strarr[8];
                        tr.Location = strarr[6];
                        tr.Play = strarr[1];

                        string[] datarr = strarr[2].ToString().Split('/');//for live server
                        //string[] datarr = strarr[2].ToString().Split('-');// for dev/local

                        tr.ShowDate = datarr[1] + "/" + datarr[0] + "/" + datarr[2];//for live server
                        //tr.ShowDate = datarr[0] + "/" + datarr[1] + "/" + datarr[2]; // for dev/local


                        tr.ShowTime = strarr[7];
                        tr.Day = Convert.ToDateTime(tr.ShowDate).DayOfWeek.ToString();
                        //tr.ShowDate = datarr[1] + "/" + datarr[0] + "/" + datarr[2];//for local
                        tr.Remark = "";
                        tr.TotalAmount = GTICKBOL.Get_SeatPrice_SeatKeyNoWise(tr.BookingID);
                        tr.SeatInfo = SeatVal;
                        tr.Address = txtAddress.Text.Trim();
                        tr.IP = GetIP();
                        Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Date : " + tr.DateOfBooking);
                        transid = TransactionBOL.Transaction_Temp_Insert(tr);
                        tr.ReferenceNo = tr.BookingID;
                        tr.BookingID = transid;
                        refno=GTICKBOL.InsertAgentBooking_Details(tr);
                        GTICKV.LogEntry(tr.ReferenceNo.ToString(), "Starting to write Information to temp Session Table", "7", "");

                    }
                    catch (Exception ex)
                    {
                        Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Transaction Preparation Error: " + ex.Message);
                        ClientScript.RegisterStartupScript(GetType(), "myscript", "<script>alert('Session Timeout. Please start the transaction again');window.location.href='Default.aspx';</script>");
                    }
                    GTICKV.LogEntry(tr.ReferenceNo.ToString(), "Category : " + tr.Category + " ,Seat Info : " + tr.SeatInfo +
                        ", Total Amt : " + tr.TotalAmount, "8", "");

                    if (transid > 0&&refno > 0)
                    {
                        Session["AgentCode"] = null;
                        GTICKV.LogEntry(tr.BookingID.ToString(), "Data Successfully Written to Temp Transaction Table", "9", transid.ToString());
                        string URL = "";
                        DataTable dt = TransactionBOL.Get_Transaction_Detail(tr);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            GTICKBOL.Update_AgentBooking(tr.BookingID);
                            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Payment Redirection: " + URL);
                            Response.Redirect("Print-Receipt.aspx?b=" + tr.BookingID.ToString(), false);
                            Session[Decrypt(Request.QueryString["SessionId"])] = "";
                            bool seatsBooked = (int.Parse(dt.Rows[0]["SeatBooked"].ToString()) > 0);
                            ReceiptUtils.SuccessPaymentResponse(seatsBooked, dt.Rows[0], tr.ReferenceNo.ToString(), tr.BookingID.ToString(), tr.ReceiptNo, "");
                            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Booking successfull"+tr.ReferenceNo);
                        }
                        else
                        {
                            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("booking failed"+tr.ReferenceNo);
                            DataTable dt1 = TransactionBOL.Select_Temptransaction_REFIDWISE(tr.ReferenceNo);
                            if (dt != null && dt.Rows.Count > 0)
                                ReceiptUtils.PaymentNotCaptureResponse(tr.ReceiptNo.ToString(), dt1.Rows[0], "");
                            if (sessionvalue[12] != null)
                            {
                                String KeyNo = Decrypt(sessionvalue[12]);
                                GTICKBOL.ON_Session_out(KeyNo);
                                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("seat releasefor:"+KeyNo);
                            }
                            Session.Clear();
                            ClientScript.RegisterStartupScript(GetType(), "myscript", "<script>alert('Session Timeout. Please start the transaction again');window.location.href='Default.aspx';</script>");
                        }
                    }
                    else
                    {
                        lblMess.Text = "Session Timeout. Please start the transaction again by clicking \"Back\" button";
                        Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Session Timeout. Need to restart transaction");
                        ClientScript.RegisterStartupScript(GetType(), "myscript", "<script>alert('Session Timeout. Please start the transaction again');window.location.href='Default.aspx';</script>");
                    }
                }
                else //no Session[seat_val]
                {
                    ClientScript.RegisterStartupScript(GetType(), "myscript", "<script>alert('Session Timeout. Please start the transaction again');window.location.href='Default.aspx';</script>");
                }
                #endregion
            }
            catch (Exception ex)
            {
                GTICKV.LogEntry(tr.BookingID.ToString(), "Error Occurred - " + ex.Message.Replace("'", ""), "8", transid.ToString());
                ClientScript.RegisterStartupScript(GetType(), "myscript", "<script>alert('Session Timeout. Please start the transaction again');window.location.href='Default.aspx';</script>");
            }
        }
        else
        {
            Session.Clear();
            ClientScript.RegisterStartupScript(GetType(), "myscript", "<script>alert('Session Timeout. Please start the transaction again');window.location.href='Default.aspx';</script>");
        }
    }
    protected void btnBackHome_Click(object sender, EventArgs e)
    {
        GTICKV.LogEntry(sessionvalue[12].ToString(), "User Press Cancel Button On contact detail Page.", "6", "");
        Response.Redirect("Default.aspx", false);
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