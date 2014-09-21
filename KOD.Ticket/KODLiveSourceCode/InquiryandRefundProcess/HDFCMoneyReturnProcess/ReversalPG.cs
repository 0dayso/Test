using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Timers;
using System.IO;
using System.Configuration;
using System.Data.SqlClient;
using _TNS;

namespace InquiryandRefundProcess
{
    public partial class ReversalPG : ServiceBase
    {
        public static string HDFCTransUrl = System.Configuration.ConfigurationManager.AppSettings["HDFCTransUrl"].ToString();
        public static string HDFCTransPortalID = System.Configuration.ConfigurationManager.AppSettings["HDFCTransPortalID"].ToString();
        public static string HDFCTranportalPwd = System.Configuration.ConfigurationManager.AppSettings["HDFCTranportalPwd"].ToString();
        public static string HDFCTranInquiry = System.Configuration.ConfigurationManager.AppSettings["HDFCTranInquiry"].ToString();
        public static string HDFCTranRefund = System.Configuration.ConfigurationManager.AppSettings["HDFCTranRefund"].ToString();
        public static string ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MsTicket"].ToString();
        Timer timer = new Timer();

        SqlConnection con = new SqlConnection(ConnectionString);
        SqlCommand command = new SqlCommand();


        static int CounterChkHDFCInqueryValue = 1, CounterChkHDFCRefundValue = 1;

        public ReversalPG()
        {
            InitializeComponent();
        }
        public Boolean Successfull_1(string BookingID)
        {
            long Booking = Convert.ToInt64(BookingID);
            con.Open();
            command.CommandText = "Update [GINC$BookingTransaction_temp] set IsProcessed=1 where BookingID=" + Booking + ""; //'1' Successful
            command.Connection = con;
            command.ExecuteNonQuery();
            con.Close();
            return true;
        }
        public Boolean ExceptioninInquiry_2(string BookingID)
        {
            long Booking = Convert.ToInt64(BookingID);
            con.Open();
            command.CommandText = "Update [GINC$BookingTransaction_temp] set IsProcessed=2 where BookingID=" + Booking + ""; //'2' Exception in Inquiry
            command.Connection = con;
            command.ExecuteNonQuery();
            con.Close();
            return false;
        }
        public Boolean UserPaymentNotSuccess_3(string BookingID)
        {
            long Booking = Convert.ToInt64(BookingID);
            con.Open();
            command.CommandText = "Update [GINC$BookingTransaction_temp] set IsProcessed=3 where BookingID=" + Booking + ""; //'3' Payment not success for the user
            command.Connection = con;
            command.ExecuteNonQuery();
            con.Close();
            return false;
        }
        public Boolean ExceptioninRefund_4(string BookingID)
        {
            long Booking = Convert.ToInt64(BookingID);
            con.Open();
            command.CommandText = "Update [GINC$BookingTransaction_temp] set IsProcessed=4 where BookingID=" + Booking + ""; //'4' Exception in Refund
            command.Connection = con;
            command.ExecuteNonQuery();
            con.Close();
            return false;
        }
        public Boolean RefundNotPossible_5(string BookingID)
        {
            long Booking = Convert.ToInt64(BookingID);
            con.Open();
            command.CommandText = "Update [GINC$BookingTransaction_temp] set IsProcessed=5 where BookingID=" + Booking + ""; //'5' Refund not done/Possible
            command.Connection = con;
            command.ExecuteNonQuery();
            con.Close();
            return false;
        }
        //public void TraceService(string content)
        //{

        //    //set up a filestream
        //    FileStream fs = new FileStream(@"d:/HDFC.txt", FileMode.OpenOrCreate, FileAccess.Write);

        //    //set up a streamwriter for adding text
        //    StreamWriter sw = new StreamWriter(fs);

        //    //find the end of the underlying filestream
        //    sw.BaseStream.Seek(0, SeekOrigin.End);

        //    //add the text
        //    sw.WriteLine(content);
        //    //add the text to the underlying filestream

        //    sw.Flush();
        //    //close the writer
        //    sw.Close();
        //}
        /// <summary>
        /// Mailer Function 
        /// Mail in Case of Successful Refund
        /// </summary>
        /// <param name="args"></param>
        void CustomerMailer(string BookingID, string Name, string Play, string TotalAmount, string TotalSeats, string DateOfBooking, string ShowDate, string Category, string EmailID)
        {
            Mail.Mail objMail = new Mail.Mail();
            Mail.MailData objmaildata = new Mail.MailData();
            objmaildata.from = ConfigurationManager.AppSettings["FromMail"];
            objmaildata.fromName = "KOD";
            objmaildata.to = EmailID;
            objmaildata.toName = Name;
            objmaildata.subject = "Refund Acknowledgement";

            string BodyMaggage = "<div>"
                + "<p>Dear "+ Name +" ,<br/></p>"
                + "<p>Due to some technical reason's your payment was deducted from the account but your " + Play + " shows tickets were not booked.<br/></p>"
                + "<p>So,We have refunded the amount of Rs. " + TotalAmount + " to your account.<br/></p>"
                + "<p>Booking Id : " + BookingID + "<br/></p>"
                + "<p>Amount Refunded : " + TotalAmount + "<br/></p>"
                + "<p>No. of Tickets : " + TotalSeats + "<br/></p>"
                + "<p>Show : " + Play + "<br/></p>"
                + "<p>Category : " + Category + "<br/></p>"
                + "<p>Date of Booking : " + DateOfBooking + "<br/></p>"
                + "<p>Show Date : " + ShowDate + "<br/><br/></p>"
                + "<p>Sorry for inconvenience<br/></p>"
                + "<p>With Regards<br/></p>"
                + "<p>Kingdom of Dreams Team</p>"
                + "<p>Gurgaon</p>"
                + "</div>"
                ;
            objmaildata.bodyMessage = BodyMaggage;
            objMail.sendMail_Net(objmaildata);
        }
        void AdminMailer(string BookingID, string Name, string Play, string TotalAmount, string TotalSeats, string DateOfBooking, string ShowDate, string Category, string EmailID, string ReferenceNo,string Mobile,string address)
        {
            Mail.Mail objMail = new Mail.Mail();
            Mail.MailData objmaildata = new Mail.MailData();
            objmaildata.from = ConfigurationManager.AppSettings["FromMail"];
            objmaildata.fromName = "";
            objmaildata.to = ConfigurationManager.AppSettings["ToMail"];
            objmaildata.toName = "";
            objmaildata.subject = "Refund Acknowledgement";

            string BodyMaggage = "<div>"
                + "<p>Dear Sir ,<br/></p>"
                + "<p>Payment was successfully Reversed for the customer " + Name +" <br/></p>"
                + "<p>Follwoing are details of the customer :<br/><br/></p>"
                + "<p>Booking Id : " + BookingID + "<br/></p>"
                + "<p>Reference Id : " + ReferenceNo + "</p>"
                + "<p>Amount Refunded : " + TotalAmount + "<br/></p>"
                + "<p>No. of Tickets : " + TotalSeats + "<br/></p>"
                + "<p>Show : " + Play + "<br/></p>"
                + "<p>Category : " + Category + "<br/></p>"
                + "<p>Date of Booking : " + DateOfBooking + "<br/></p>"
                + "<p>Show Date : " + ShowDate + "<br/></p>"
                + "<p>Customer Name : " + Name + "<br/></p>"
                + "<p>Email Address : " + EmailID + "<br/></p>"
                + "<p>Address : " + address + "<br/></p>"
                + "<p>Mobile No. : " + Mobile + "<br/><br/></p>"
                + "<p>With Regards<br/></p>"
                + "<p>Kingdom of Dreams Team</p>"
                + "<p>Gurgaon</p>"
                + "</div>"
                ;
            objmaildata.bodyMessage = BodyMaggage;
            objMail.sendMail_Net(objmaildata);
        }

        protected override void OnStart(string[] args)
        {
            //System.Diagnostics.Debugger.Launch();
            timer.Elapsed += new ElapsedEventHandler(OnElapsedTime);
            //timer.Interval = 6000;
            timer.Interval = Convert.ToDouble(ConfigurationManager.AppSettings["TimeInterval"]);
            timer.Enabled = true;
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Service Starts at :" + DateTime.Now);
        }

        protected override void OnStop()
        {
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Stopping Service");
            timer.Enabled = false;
        }


        private void OnElapsedTime(object source, ElapsedEventArgs e)
        {
            try
            {
                timer.Enabled = false;
                con.Open();                    //2013-09-01  and CONVERT(Time,TimeOfBooking) < DateAdd(minute,-30, Convert(Time,GetDate())) and PaymentGateway = 'HDFC'
                SqlCommand cmd = new SqlCommand();
                string findtime = DateTime.Now.TimeOfDay.Hours.ToString();
                int findMinute = DateTime.Now.TimeOfDay.Minutes;
                if (findMinute < 30 && (findtime == "0" || findtime == "00"))
                {
                    DateTime yesterday = DateTime.Now.AddDays(-1);
                    string tracedate = yesterday.ToString("MM/dd/yyyy");
                    string[] dateYesterday = tracedate.Split('/');//for Live
                    //string[] dateYesterday = tracedate.Split('-');//for local
                    string date = dateYesterday[1].ToString();
                    string Month = dateYesterday[0].ToString();
                    string Year = dateYesterday[2].ToString();
                    string dateDB = Year + "-" + Month + "-" + date;
                    cmd.CommandText = "select [BookingID],[ID],[Source],[TotalAmount],[DiscountPercentage],[ReceiptNo],[Play],[TotalSeats],[Name],[EmailID],[MobileNo],[Address],[Category],[ShowDate],[DateOfBooking],[IsProcessed],[PaymentGateway] from [GINC$BookingTransaction_temp] where DateOfBooking='" + dateDB + "' and CONVERT(Time,TimeOfBooking) < DateAdd(minute,-30, Convert(Time,GetDate())) and IsProcessed=0";
                    cmd.Connection = con;
                }

                else
                {
                    cmd.CommandText = "select [BookingID],[ID],[Source],[TotalAmount],[DiscountPercentage],[ReceiptNo],[Play],[TotalSeats],[Name],[EmailID],[MobileNo],[Address],[Category],[ShowDate],[DateOfBooking],[IsProcessed],[PaymentGateway] from [GINC$BookingTransaction_temp] where DateOfBooking=Convert(Date,GetDate()) and CONVERT(Time,TimeOfBooking) < DateAdd(minute,-30, Convert(Time,GetDate())) and IsProcessed=0";
                    cmd.Connection = con;
                }
                SqlDataAdapter RunCommand = new SqlDataAdapter(cmd);
                DataTable StoreCommandData = new DataTable();
                RunCommand.Fill(StoreCommandData);
                con.Close();
                if (StoreCommandData.Rows.Count > 0)
                {
                    BookingDetails(StoreCommandData);
                }
                else
                {
                    Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("There is no data in a Data Row");
                }
                timer.Enabled = true;
            }
            catch (SqlException ex)
            {
                timer.Enabled = true;
                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Exception in SQL QUERY "+ ex);
            }
            catch (Exception ex)
            {
                timer.Enabled = true;
                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Exception while connecting to DB" + ex);
            }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Close();
                }
            }     
        }

        public void BookingDetails(DataTable StoreData)
        {
            for (int CounterBookingID = 0; CounterBookingID < StoreData.Rows.Count; CounterBookingID++)
            {
                string BookingID = StoreData.Rows[CounterBookingID]["BookingID"].ToString();
                string ReferenceNo = StoreData.Rows[CounterBookingID]["ID"].ToString();
                string TotalAmount = StoreData.Rows[CounterBookingID]["TotalAmount"].ToString();
                string ReceiptNo = StoreData.Rows[CounterBookingID]["ReceiptNo"].ToString();
                string Name = StoreData.Rows[CounterBookingID]["Name"].ToString();
                string Source = StoreData.Rows[CounterBookingID]["Source"].ToString();
                string PaymentGateway = StoreData.Rows[CounterBookingID]["PaymentGateway"].ToString().ToUpper();
                string TotalSeats = StoreData.Rows[CounterBookingID]["TotalSeats"].ToString();
                string EmailID = StoreData.Rows[CounterBookingID]["EmailID"].ToString();
                string MobileNo = StoreData.Rows[CounterBookingID]["MobileNo"].ToString();
                string Address = StoreData.Rows[CounterBookingID]["Address"].ToString();
                string Category = StoreData.Rows[CounterBookingID]["Category"].ToString();
                string ShowDate = StoreData.Rows[CounterBookingID]["ShowDate"].ToString();
                string DateOfBooking = StoreData.Rows[CounterBookingID]["DateOfBooking"].ToString();
                string Play = StoreData.Rows[CounterBookingID]["Play"].ToString();
                Decimal DiscountPercentage = Convert.ToDecimal(StoreData.Rows[CounterBookingID]["DiscountPercentage"].ToString());
                
                if (TotalAmount.Contains("."))
                    TotalAmount = TotalAmount.Replace(".00", "");
                int Amount = Convert.ToInt32(TotalAmount);
                int DiscPercent = Convert.ToInt32(Amount * DiscountPercentage / 100);
                Amount = Amount - DiscPercent;
                TotalAmount = Convert.ToDecimal(Amount).ToString()+".00";

                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Reversal Process for  BookingID : " + BookingID + "  ReferenceNo : " + ReferenceNo + "  Payment Gateway No. : " + ReceiptNo + "  PaymentGateway : " + PaymentGateway + " Amount :" + TotalAmount);
                if (PaymentGateway == "HDFC")
                {
                    Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("REVERSAL FLOW FOR HDFC GATEWAY");
                    if (ReceiptNo != "")
                    {
                        Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("REFUND FLOW FOR  :  BOOKING ID  : " + BookingID + "  REFERENCENO  : " + ReferenceNo + "  AMOUNT  :  " + TotalAmount + "  RECEIPT NO. :  " + ReceiptNo +"  NAME  :  " + Name);
                        Boolean Result = InquiryHDFC(BookingID, ReferenceNo, TotalAmount, ReceiptNo, Name);
                        Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("HDFC FINAL RESULT : " + Result);
                        if (Result == true)
                        {
                            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Refund is Successful for Booking ID " + BookingID);
                            CustomerMailer(BookingID, Name, Play, TotalAmount, TotalSeats, DateOfBooking, ShowDate, Category, EmailID);
                            AdminMailer(BookingID, Name, Play, TotalAmount, TotalSeats, DateOfBooking, ShowDate, Category, EmailID, ReferenceNo, MobileNo, Address);
                        }
                        else
                        {
                            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Refund Process is not Successful for Booking ID " + BookingID);
                        }
                    }
                    else
                    {
                        Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Receipt No. Does Not Exist for BookingID : " + BookingID);
                    }
                }
                if (PaymentGateway == "AMEX")
                {
                    Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("REVERSAL FLOW FOR AMEX GATEWAY");
                    if ((BookingID != "" || BookingID != null) && (ReferenceNo != "" || ReferenceNo != null))
                    {
                        Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("REFUND FLOW FOR  :  BOOKING ID  : " + BookingID + "  REFERENCENO  : " + ReferenceNo + "  AMOUNT  :  "+TotalAmount+ "  NAME  :  " + Name);
                        Boolean Result = AmexQuery(BookingID, ReferenceNo, TotalAmount);
                        Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("AMEX FINAL RESULT : " + Result);
                        if (Result == true)
                        {
                            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Refund is Successful for Booking ID " + BookingID);
                            CustomerMailer(BookingID, Name, Play, TotalAmount, TotalSeats, DateOfBooking, ShowDate, Category, EmailID);
                            AdminMailer(BookingID, Name, Play, TotalAmount, TotalSeats, DateOfBooking, ShowDate, Category, EmailID,ReferenceNo,MobileNo,Address);
                        }
                        else
                        {
                            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Refund Process is not Successful for Booking ID " + BookingID);
                        }
                    }
                    else
                    {
                        Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Mandatory fields are missing");
                    }
                }
                if (PaymentGateway == "IDBI")
                {
                    Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("REVERSAL FLOW FOR IDBI GATEWAY");
                }
            }
        }

        /// <summary>
        /// Function Inquiry HDFC
        /// </summary>
        /// <param name="BookingID"></param>
        /// <param name="ReferenceNo"></param>
        /// <param name="TotalAmount"></param>
        /// <param name="ReceiptNo"></param>
        public Boolean InquiryHDFC(string BookingID, string ReferenceNo, string TotalAmount, string ReceiptNo, string Name)
        {
            string ReqTranportalId = "<id>" + HDFCTransPortalID + "</id>";
            string ReqTranportalPassword = "<password>" + HDFCTranportalPwd + "</password>";
            string INQAction = "<action>" + HDFCTranInquiry + "</action>";
            string INQTranAmt = "<amt>" + TotalAmount + "</amt>";
            string INQTrackId = "<trackid>" + BookingID + "_" + ReferenceNo + "-WEB</trackid>";
            string INQudf5 = "<udf5>" + BookingID + "</udf5>";
            string INQTransId = "<transid>" + ReceiptNo + "</transid>";
            //string ReqTranportalId = "<id>70002588</id>";
            //string ReqTranportalPassword = "<password>70002588</password>";
            //string INQAction = "<action>8</action>";
            ////string INQTransId = "<transid>2540096292320960</transid>";
            //string INQTranAmt = "<amt>749.00</amt>";
            //string INQTrackId = "<trackid></trackid>";
            //string INQudf5 = "<udf5>1000176470</udf5>";
            //string INQTransId = "<transid>9588865191530220</transid>";

            string data = ReqTranportalId + ReqTranportalPassword + INQAction + INQTranAmt + INQTransId + INQTrackId + INQudf5;
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("HDFC Inquiry Data Value : " + data);
            string TranInqResponse = FetchDATA(data);
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Inquiry Data Received After HDFC Hit  : "+TranInqResponse);
            while (true)
            {
                if (TranInqResponse == null || TranInqResponse == "")
                {
                    if (CounterChkHDFCInqueryValue <= 3)
                    {
                        Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Counter value for HDFC Inquiry : " + CounterChkHDFCInqueryValue);
                        TranInqResponse = FetchDATA(data);
                        CounterChkHDFCInqueryValue++;
                    }
                    else
                    {
                        Boolean IsHDFCInquirySuccess=ExceptioninInquiry_2(BookingID);
                        Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Update ISProcessed Value to ' 2 ' and InquirySuccess : " + IsHDFCInquirySuccess+ " for BookingID : " + BookingID);  
                        return IsHDFCInquirySuccess;
                    }
                }
                if (TranInqResponse != null && TranInqResponse != "")
                {
                    string[] INQCheck = GetStringInBetween(TranInqResponse, "<result>", "</result>", false, false);
                    Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("INQCheck Value for Inquiry :" + INQCheck[0]);
                    if (INQCheck[0] == "CAPTURED" || INQCheck[0] == "APPROVED" || INQCheck[0] == "SUCCESS")
                    {
                        Boolean IsSuccess = RefundHDFC(BookingID, ReferenceNo, TotalAmount, ReceiptNo, Name);
                        Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Value Return from ReturnHDFC i.e. IsSuccess : " + IsSuccess);
                        if (IsSuccess == true)
                        {
                            return true;
                        }
                        else
                            return false;
                    }
                    else
                    {
                       Boolean IsPaymentSuccess = UserPaymentNotSuccess_3(BookingID);
                       Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Update ISProcessed Value to ' 3 ' and Payment Success : " + IsPaymentSuccess + " for BookingID : " + BookingID);  
                       return IsPaymentSuccess;
                    }
                }
            }
        }

        /// <summary>
        /// Function Refund HDFC
        /// </summary>
        /// <param name="BookingID"></param>
        /// <param name="ReferenceNo"></param>
        /// <param name="TotalAmount"></param>
        /// <param name="ReceiptNo"></param>
        /// <param name="Name"></param>
        public Boolean RefundHDFC(string BookingID, string ReferenceNo, string TotalAmount, string ReceiptNo, string Name)
        {
            string ReqTranportalId = "<id>" + HDFCTransPortalID + "</id>";
            string ReqTranportalPassword = "<password>" + HDFCTranportalPwd + "</password>";
            string INQAction = "<action>" + HDFCTranRefund + "</action>";
            string INQTranAmt = "<amt>" + TotalAmount + "</amt>";
            string INQudf5 = "<udf5>" + BookingID + "</udf5>";
            string member = "<member>" + Name + "</member>";
            string INQTransId = "<transid>" + ReceiptNo + "</transid>";
            //string ReqTranportalId = "<id>70002588</id>";
            //string ReqTranportalPassword = "<password>70002588</password>";
            //string INQAction = "<action>8</action>";
            ////string INQTransId = "<transid>2540096292320960</transid>";
            //string INQTranAmt = "<amt>749.00</amt>";
            ////string INQTrackId = "<trackid></trackid>";
            //string INQudf5 = "<udf5>1000176470</udf5>";
            //string member = "<member>Gaurav</member>";
            //string INQTransId = "<transid>9588865191530220</transid>";

            string data = ReqTranportalId + ReqTranportalPassword + INQAction + INQTranAmt + INQTransId + member + INQudf5;
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("HDFC Refund  Data Value: " + data);
            string TranInqResponse = FetchDATA(data);
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Refund Data Received After HDFC Hit : " + TranInqResponse);
            while (true)
            {
                if (TranInqResponse == null || TranInqResponse == "")
                {
                    if (CounterChkHDFCRefundValue <= 3)
                    {
                        Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Counter value for HDFC Refund : " + CounterChkHDFCInqueryValue);
                        TranInqResponse = FetchDATA(data);
                        CounterChkHDFCRefundValue++;
                    }
                    else
                    {
                       Boolean IsHDFCRefundException = ExceptioninRefund_4(BookingID);
                       Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Update ISProcessed Value to ' 4 ' and HDFC Refund Exception : " + IsHDFCRefundException + " for BookingID : " + BookingID);
                       return IsHDFCRefundException;
                    }
                }
                if (TranInqResponse != null && TranInqResponse != "")
                {
                    string[] INQCheck = GetStringInBetween(TranInqResponse, "<result>", "</result>", false, false);
                    Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("INQCheck Value for Refund :" + INQCheck[0]);
                    if (INQCheck[0] == "CAPTURED" || INQCheck[0] == "APPROVED" || INQCheck[0] == "SUCCESS")
                    {
                        Boolean IsSuccess=Successfull_1(BookingID);
                        Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Update ISProcessed Value to ' 1 ' and Success : " + IsSuccess + " for BookingID : " + BookingID);
                        return IsSuccess;
                    }
                    else
                    {
                        Boolean IsRefundPossible = RefundNotPossible_5(BookingID);
                        Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Update ISProcessed Value to ' 5 ' and RefundPossible : " + IsRefundPossible + " for BookingID : " + BookingID);
                        return IsRefundPossible;
                    }
                }
            }
        }


        public static string FetchDATA(string RequestData)
        {
            try
            {
                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Information Send to HDFC Gateway : " + RequestData);
                /* This is Payment Gateway Test URL where merchant sends request. This is test enviornment URL, 
                production URL will be different and will be shared by Bank during production movement */
                string url = HDFCTransUrl;
                System.IO.StreamWriter myWriter = null;
                // it will open a http connection with provided url
                System.Net.HttpWebRequest objRequest = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);//send data using objxmlhttp object
                objRequest.Method = "POST";
                objRequest.ContentLength = RequestData.Length;
                objRequest.ContentType = "application/x-www-form-urlencoded";//to set content type
                myWriter = new System.IO.StreamWriter(objRequest.GetRequestStream());
                myWriter.Write(RequestData);
                myWriter.Close();

                string TranInqResponse;
                System.Net.HttpWebResponse objResponse = (System.Net.HttpWebResponse)objRequest.GetResponse();
                using (System.IO.StreamReader sr = new System.IO.StreamReader(objResponse.GetResponseStream()))
                {
                    /* The variable declartion where PG response wil be received*/
                    TranInqResponse = sr.ReadToEnd();
                    //receive the responce from objResponse object
                    Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Information Received from HDFC Gateway : " + TranInqResponse);
                    return TranInqResponse;
                }
            }
            catch (Exception ex)
            {
                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Exception While Hit HDFC GATEWAY: " + ex);
                return "";
            }
        }

        public static string[] GetStringInBetween(string strSource, string strBegin, string strEnd, bool includeBegin, bool includeEnd)
        {
            string[] result = { "", "" };
            int iIndexOfBegin = strSource.IndexOf(strBegin);
            if (iIndexOfBegin != -1)
            {
                // include the Begin string if desired

                if (includeBegin)
                {
                    iIndexOfBegin -= strBegin.Length;
                }
                strSource = strSource.Substring(iIndexOfBegin + strBegin.Length);
                int iEnd = strSource.IndexOf(strEnd);
                if (iEnd != -1)
                {  // include the End string if desired
                    if (includeEnd)
                    { iEnd += strEnd.Length; }
                    result[0] = strSource.Substring(0, iEnd);
                    // advance beyond this segment
                    if (iEnd + strEnd.Length < strSource.Length)
                    { result[1] = strSource.Substring(iEnd + strEnd.Length); }
                }
            }
            else
            // stay where we are
            { result[1] = strSource; }
            return result;
        }//String function end


        public VPCRequest AmexConfig()
        {
                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Collect Information for AMEX Gateway");
                VPCRequest conn = new VPCRequest(_TNS.Properties.Settings.Default.PaymentServerURL);
                // Configure the proxy details (if needed)
                conn.SetProxyHost(_TNS.Properties.Settings.Default.ProxyHost);
                conn.SetProxyUser(_TNS.Properties.Settings.Default.ProxyUser);
                conn.SetProxyPassword(_TNS.Properties.Settings.Default.ProxyPassword);
                conn.SetProxyDomain(_TNS.Properties.Settings.Default.ProxyDomain);
                conn.SetSecureSecret(_TNS.Properties.Settings.Default.vpc_SecureSecret);
                // Add the Digital Order Fields for the functionality you wish to use
                // Core Transaction Fields
                conn.AddDigitalOrderField("vpc_Version", _TNS.Properties.Settings.Default.vpc_Version);
                conn.AddDigitalOrderField("vpc_AccessCode", _TNS.Properties.Settings.Default.vpc_AccessCode);
                conn.AddDigitalOrderField("vpc_Merchant", _TNS.Properties.Settings.Default.vpc_Merchant);
                conn.AddDigitalOrderField("vpc_User", _TNS.Properties.Settings.Default.vpc_User);
                conn.AddDigitalOrderField("vpc_Password", _TNS.Properties.Settings.Default.vpc_Password);
                return conn;   

                //Configure the Hard coded details (if needed)
                //VPCRequest conn = new VPCRequest("https://vpos.amxvpos.com/vpcdps");
                //conn.SetProxyHost("");
                //conn.SetProxyUser("");
                //conn.SetProxyPassword("");
                //conn.SetProxyDomain("");
                //conn.SetSecureSecret("44DD98D32ECD3C1AA7F12A1D0F8B41EA");

                         
        }

        public Boolean AmexQuery(string BookingID, string ReferenceNo,string TotalAmount)
        {
            VPCRequest conn = AmexConfig();
            string MerchantRefNo = BookingID + "_" + ReferenceNo + "~WEB";
            conn.AddDigitalOrderField("vpc_Command", "queryDR");
            conn.AddDigitalOrderField("vpc_MerchTxnRef", MerchantRefNo);
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Amex Reversal Payment process for MerchantRefNo. : " + MerchantRefNo);
            // Perform the transaction
            conn.SendRequest();
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Returning form Amex Gateway");
            string ResponseResult=null;
            int CounterChkAmexQueryValue;
            for (CounterChkAmexQueryValue = 1; CounterChkAmexQueryValue <= 2; CounterChkAmexQueryValue++)
            {
                string vpc_TxnResponseCode = conn.GetResultField("vpc_TxnResponseCode", "Unknown");
                ResponseResult = PaymentCodesHelper.GetTxnResponseCodeDescription(vpc_TxnResponseCode);
                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Response Code : "+ResponseResult);
                if (ResponseResult == "" || ResponseResult == null||ResponseResult=="null response")
                {
                    conn.SendRequest();
                }
                else
                {
                    StringBuilder InquiryDetails = new StringBuilder();
                    InquiryDetails.Append("These are the Inquiry Details for Booking ID and Reference :" + BookingID + "," + ReferenceNo + "<br/>");
                    InquiryDetails.Append(" vpc_TxnResponseCode :" + conn.GetResultField("vpc_TxnResponseCode", "Unknown") + "<br/>");
                    InquiryDetails.Append(" vpc_MerchTxnRef :" + conn.GetResultField("vpc_MerchTxnRef", "Unknown") + "<br/>");
                    InquiryDetails.Append(" vpc_Merchant :" + conn.GetResultField("vpc_Merchant", "Unknown") + "<br/>");
                    InquiryDetails.Append(" vpc_OrderInfo :" + conn.GetResultField("vpc_OrderInfo", "Unknown") + "<br/>");
                    InquiryDetails.Append(" vpc_Amount :" + conn.GetResultField("vpc_Amount", "Unknown") + "<br/>");
                    InquiryDetails.Append(" vpc_DRExists :" + conn.GetResultField("vpc_DRExists", "Unknown"));
                    InquiryDetails.Append(" vpc_FoundMultipleDRs :" + conn.GetResultField("vpc_FoundMultipleDRs", "Unknown") + "<br/>");
                    InquiryDetails.Append(" vpc_Message :" + conn.GetResultField("vpc_Message", "Unknown") + "<br/>");
                    InquiryDetails.Append(" vpc_AcqResponseCode :" + conn.GetResultField("vpc_AcqResponseCode", "Unknown") + "<br/>");
                    InquiryDetails.Append(" vpc_TransactionNo :" + conn.GetResultField("vpc_TransactionNo", "Unknown") + "<br/>");
                    InquiryDetails.Append(" vpc_ReceiptNo :" + conn.GetResultField("vpc_ReceiptNo", "Unknown") + "<br/>");
                    InquiryDetails.Append(" vpc_AuthorizeId :" + conn.GetResultField("vpc_AuthorizeId", "Unknown") + "<br/>");
                    InquiryDetails.Append(" vpc_BatchNo :" + conn.GetResultField("vpc_BatchNo", "Unknown") + "<br/>");
                    InquiryDetails.Append(" vpc_TicketNo :" + conn.GetResultField("vpc_TicketNo", "Unknown") + "<br/>");
                    InquiryDetails.Append(" vpc_Card :" + conn.GetResultField("vpc_Card", "Unknown") + "<br/>");
                    InquiryDetails.Append(" Response Result :" + ResponseResult + "<br/>");
                    Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Inquiry Details for Amex : " + InquiryDetails);
                    string vpc_avsResultCode = conn.GetResultField("vpc_AVSResultCode", "Unknown");
                    string TransactionNo = conn.GetResultField("vpc_TransactionNo", "Unknown");
                    Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("vpc_AVSResultCode : " + vpc_avsResultCode + " and vpc_TransactionNo : " + TransactionNo);
                    if (vpc_TxnResponseCode == "0" && (vpc_avsResultCode == "Y" || vpc_avsResultCode == "M"))
                    {
                        if ((MerchantRefNo != "" || MerchantRefNo != null) && (TotalAmount != "" || TotalAmount != null) && (TransactionNo != "" || TransactionNo != null))
                        {
                            Boolean Result = AmexRefund(BookingID, ReferenceNo, TotalAmount, TransactionNo);
                            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Value Return from AmexRefund i.e. IsSuccess : " + Result);
                            if (Result == true)
                            {
                                return true;
                            }
                            else
                                return false;
                        }
                        else
                        {
                            return false;
                        }  
                    }
                    else
                    {
                        Boolean IsPaymentSuccess = UserPaymentNotSuccess_3(BookingID);
                        Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Update ISProcessed Value to ' 3 ' and Inquiry Payment Success : " + IsPaymentSuccess + " for BookingID : " + BookingID);
                        return IsPaymentSuccess;
                    }
            
                }               
          }//for loop end
            if (CounterChkAmexQueryValue == 3)
            {
                Boolean IsHDFCInquirySuccess = ExceptioninInquiry_2(BookingID);
                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Update ISProcessed Value to ' 2 ' and Inquiry Payment Success : " + IsHDFCInquirySuccess + " for BookingID : " + BookingID);
                return IsHDFCInquirySuccess;
            }
            return false;
        }

        private Boolean AmexRefund(string BookingID, string ReferenceNo, string TotalAmount, string TransactionNo)
        {
            VPCRequest conn = AmexConfig();
            string MerchantRefNo = BookingID + "_" + ReferenceNo + "~WEB";
            string vpc_TransNo = TransactionNo;
            if (TotalAmount.Contains("."))
                TotalAmount = TotalAmount.Replace(".00", "");
            TotalAmount = Convert.ToString(Convert.ToInt32(TotalAmount) * 100);
            string vpc_Amount = TotalAmount;
            conn.AddDigitalOrderField("vpc_Command", "refund");
            conn.AddDigitalOrderField("vpc_MerchTxnRef", MerchantRefNo);
            conn.AddDigitalOrderField("vpc_TransNo", vpc_TransNo);
            conn.AddDigitalOrderField("vpc_Amount", vpc_Amount);
            conn.SendRequest();
            string ResponseResult=null;
            int CounterChkAmexRefundValue;
            for (CounterChkAmexRefundValue = 1; CounterChkAmexRefundValue <= 2; CounterChkAmexRefundValue++)
            {
                string vpc_TxnResponseCode = conn.GetResultField("vpc_TxnResponseCode", "Unknown");
                ResponseResult = PaymentCodesHelper.GetTxnResponseCodeDescription(vpc_TxnResponseCode);
                if (ResponseResult == "" || ResponseResult == null)
                {
                    conn.SendRequest();
                }
                else
                {
                    StringBuilder RefundDetails = new StringBuilder();
                    RefundDetails.Append(" These are the Refund Details for Booking ID and Reference : " + BookingID + "," + ReferenceNo + "<br/>");
                    RefundDetails.Append(" vpc_TxnResponseCode : " + conn.GetResultField("vpc_TxnResponseCode", "Unknown") + "<br/>");
                    RefundDetails.Append(" vpc_MerchTxnRef : " + conn.GetResultField("vpc_MerchTxnRef", "Unknown") + "<br/>");
                    RefundDetails.Append(" vpc_Merchant : " + conn.GetResultField("vpc_Merchant", "Unknown") + "<br/>");
                    RefundDetails.Append(" vpc_OrderInfo : " + conn.GetResultField("vpc_OrderInfo", "Unknown") + "<br/>");
                    RefundDetails.Append(" vpc_Amount : " + conn.GetResultField("vpc_Amount", "Unknown") + "<br/>");
                    RefundDetails.Append(" vpc_DRExists :" + conn.GetResultField("vpc_DRExists", "Unknown") + "<br/>");
                    RefundDetails.Append(" vpc_FoundMultipleDRs : " + conn.GetResultField("vpc_FoundMultipleDRs", "Unknown") + "<br/>");
                    RefundDetails.Append(" vpc_Message : " + conn.GetResultField("vpc_Message", "Unknown") + "<br/>");
                    RefundDetails.Append(" vpc_AcqResponseCode : " + conn.GetResultField("vpc_AcqResponseCode", "Unknown") + "<br/>");
                    RefundDetails.Append(" vpc_TransactionNo : " + conn.GetResultField("vpc_TransactionNo", "Unknown") + "<br/>");
                    RefundDetails.Append(" vpc_ReceiptNo : " + conn.GetResultField("vpc_ReceiptNo", "Unknown") + "<br/>");
                    RefundDetails.Append(" vpc_AuthorizeId : " + conn.GetResultField("vpc_AuthorizeId", "Unknown") + "<br/>");
                    RefundDetails.Append(" vpc_BatchNo : " + conn.GetResultField("vpc_BatchNo", "Unknown") + "<br/>");
                    RefundDetails.Append(" vpc_TicketNo : " + conn.GetResultField("vpc_TicketNo", "Unknown") + "<br/>");
                    RefundDetails.Append(" vpc_Card : " + conn.GetResultField("vpc_Card", "Unknown") + "<br/>");
                    RefundDetails.Append(" Response Result :" + ResponseResult + "<br/>");
                    RefundDetails.Append(" vpc_AuthorisedAmount : " + conn.GetResultField("vpc_AuthorisedAmount", "Unknown") + "<br/>");
                    RefundDetails.Append(" vpc_CapturedAmount : " + conn.GetResultField("vpc_CapturedAmount", "Unknown") + "<br/>");
                    RefundDetails.Append(" vpc_RefundedAmount : " + conn.GetResultField("vpc_RefundedAmount", "Unknown") + "<br/>");
                    Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Refund Details for Amex : " + RefundDetails);

                    string vpc_avsResultCode = conn.GetResultField("vpc_AVSResultCode", "Unknown");
                    string Transaction = conn.GetResultField("vpc_TransactionNo", "Unknown");
                    Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("vpc_AVSResultCode : " + vpc_avsResultCode + " and vpc_TransactionNo : " + TransactionNo);
                    if (vpc_TxnResponseCode == "0")//&& (vpc_avsResultCode == "Y" || vpc_avsResultCode == "M")
                    {
                        Boolean IsSuccess = Successfull_1(BookingID);
                        Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Update ISProcessed Value to ' 1 ' and Payment Successful : " + IsSuccess + " for BookingID : " + BookingID);
                        return IsSuccess;
                    }
                    else
                    {
                        Boolean IsRefundPossible = RefundNotPossible_5(BookingID);
                        Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Update ISProcessed Value to ' 5 ' and Refund : " + IsRefundPossible + " for BookingID : " + BookingID);
                        return IsRefundPossible;
                    }
                }  
            }
            if (CounterChkAmexRefundValue == 3)
            {
                Boolean IsAmexException = ExceptioninRefund_4(BookingID);
                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Update ISProcessed Value to ' 4 ' and Refund : " + IsAmexException + " for BookingID : " + BookingID);
                return IsAmexException;
            }
            return false;
        }
    }
}
