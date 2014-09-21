using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Configuration;
using KoDTicketing.BusinessLayer;

namespace KoDTicketing.DataAccessLayer
{
    public class TransactionDAL : DBAccess
    {
        TransactionDAL()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tr"></param>
        /// <returns></returns>
        public static int _Transaction_Temp_Insert(TransactionRecord tr)
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = "if  not EXISTS (Select id from ginc$bookingTransaction_temp where BookingID =" + tr.BookingID + ")" +
                        " begin INSERT INTO [GINC$BookingTransaction_temp] ([BookingID],[ReferenceNo],[Source],[DateOfBooking],[TimeOfBooking]," +
                        " [Location],[Play],[ShowTime],[ShowDate],[Category],[TotalSeats],[TotalAmount],[PaymentType],[CardType],[CardNo]," +
                        " [MobileNo],[EmailID],[Name],[PaymentGateway],[Amex Street],[Amex Pin],[Amex Country],[AgentCode],[BookingType],[Status],[SeatInfo], [Address], [IP], [Remark],[PromotionCode],[DiscountPercentage],[WebPromotionId],RegId,AvailedAmount,AvailedPoints,TopUpAmount,TopUpTransactionId,OptionalEmail,OptionalContact,IsChecked,PlaceOfPick,TimeOfPick,PlaceOfDrop,TimeOfDrop,WantComplimentary,WantComplimentaryDrop,IsProcessed,PayableAmount,[RoutedThrough],[PaymentStatus])VALUES (@BookingID," +
                        " @ReferenceNo,@Source,@DateOfBooking,@TimeOfBooking,@Location,@Play,@ShowTime,@ShowDate,@Category,@TotalSeats," +
                        "@TotalAmount,@PaymentType,@CardType,@CardNo,@MobileNo,@EmailID,@Name, @PaymentGateway,@Street,@Pin,@Country,@AgentCode,@BookingType,@Status,@seatinfo, @Address, @IP, @Remark,@PromotionCode,@DiscountPercentage,@WebPromotionId,@RegId,@AvailedAmount,@AvailedPoints,@TopUpAmount,@TopUpTransactionId,@OptionalEmail,@OptionalContact,@IsChecked,@PlaceOfPick,@TimeOfPick,@PlaceOfDrop,@TimeOfDrop,@WantComplimentary,@WantComplimentaryDrop,@IsProcessed,@PayableAmount,@RoutedThrough,@PaymentStatus)" +
                        " Select @@IDENTITY end else begin 	Select id from ginc$bookingTransaction_temp where BookingID =" + tr.BookingID + " end ";
            command.Parameters.AddWithValue("@BookingID", tr.BookingID);
            command.Parameters.AddWithValue("@ReferenceNo", tr.ReferenceNo);
            command.Parameters.AddWithValue("@Source", tr.Source);
            command.Parameters.AddWithValue("@DateOfBooking", tr.DateOfBooking);
            command.Parameters.AddWithValue("@TimeOfBooking", tr.TimeOfBooking);
            command.Parameters.AddWithValue("@Location", tr.Location);
            command.Parameters.AddWithValue("@Play", tr.Play);
            command.Parameters.AddWithValue("@ShowTime", tr.ShowTime);
            command.Parameters.AddWithValue("@ShowDate", tr.ShowDate);
            command.Parameters.AddWithValue("@Category", tr.Category);
            command.Parameters.AddWithValue("@TotalSeats", tr.TotalSeats);
            command.Parameters.AddWithValue("@TotalAmount", tr.TotalAmount);
            command.Parameters.AddWithValue("@PaymentType", tr.PaymentType);
            command.Parameters.AddWithValue("@CardType", tr.CardType);
            command.Parameters.AddWithValue("@CardNo", tr.CardNo);
            command.Parameters.AddWithValue("@MobileNo", tr.MobileNo);
            command.Parameters.AddWithValue("@EmailID", tr.EmailID);
            command.Parameters.AddWithValue("@Name", tr.Name);
            command.Parameters.AddWithValue("@PaymentGateway", tr.PaymentGateway);
            command.Parameters.AddWithValue("@AgentCode", tr.AgentCode);
            command.Parameters.AddWithValue("@BookingType", tr.BookingType);
            command.Parameters.AddWithValue("@Status", tr.Status);
            command.Parameters.AddWithValue("@seatinfo", tr.SeatInfo);


            //For HDFC Gateway
            command.Parameters.AddWithValue("@Address", tr.Address);
            command.Parameters.AddWithValue("@IP", tr.IP);
            command.Parameters.AddWithValue("@Remark", tr.Remark);
            command.Parameters.AddWithValue("@IsProcessed", tr.IsProcessed);
            command.Parameters.AddWithValue("@PaymentStatus", tr.PaymentStatus);
            //For AMEX Gateway
            if (tr.Pin == null)
                command.Parameters.AddWithValue("@Pin", DBNull.Value);
            else
                command.Parameters.AddWithValue("@Pin", tr.Pin);
            if (tr.Country == null)
                command.Parameters.AddWithValue("@Country", DBNull.Value);
            else
                command.Parameters.AddWithValue("@Country", tr.Country);
            if (tr.Street == null)
                command.Parameters.AddWithValue("@Street", DBNull.Value);
            else
                command.Parameters.AddWithValue("@Street", tr.Street);
            //*********************for routed through another site**********************
            if (tr.router == null)
                command.Parameters.AddWithValue("@RoutedThrough", DBNull.Value);
            else
                command.Parameters.AddWithValue("@RoutedThrough", tr.router);



            //************ Promotion code changes, inserting discount,code in temptable ~~ START*****
            if (tr.PromotionCode == null)
                command.Parameters.AddWithValue("@PromotionCode", DBNull.Value);
            else
            {
                command.Parameters.AddWithValue("@PromotionCode", tr.PromotionCode);
            }
            command.Parameters.AddWithValue("@DiscountPercentage", tr.DiscountPercentage);
            if (tr.PayableAmount == null)
                command.Parameters.AddWithValue("@PayableAmount", DBNull.Value);
            else
            {
                command.Parameters.AddWithValue("@PayableAmount", tr.PayableAmount);
            }

            if (tr.WebPromotionId == null)
                command.Parameters.AddWithValue("@WebPromotionId", DBNull.Value);
            else
                command.Parameters.AddWithValue("@WebPromotionId", tr.WebPromotionId);

            //************ Promotion code changes, inserting discount,code in temptable ~~ END*****

            //************ Royal code changes, inserting discount,code in temptable ~~ START*****
            if (tr.RegId == null)
                command.Parameters.AddWithValue("@RegId", DBNull.Value);
            else
            {
                command.Parameters.AddWithValue("@RegId", tr.RegId);
            }
            if (tr.TopUpTransactionId == null)
                command.Parameters.AddWithValue("@TopUpTransactionId", DBNull.Value);
            else
            {
                command.Parameters.AddWithValue("@TopUpTransactionId", tr.TopUpTransactionId);
            }
            command.Parameters.AddWithValue("@AvailedPoints", tr.AvailedPoints);
            command.Parameters.AddWithValue("@AvailedAmount", tr.AvailedAmount);
            command.Parameters.AddWithValue("@TopUpAmount", tr.TopUpAmount);
            command.Parameters.AddWithValue("@IsChecked", tr.IsChecked);
            command.Parameters.AddWithValue("@PlaceOfPick", tr.PlaceOfPick);
            command.Parameters.AddWithValue("@TimeOfPick", tr.TimeOfPick);
            if (tr.PlaceOfDrop == null)
                command.Parameters.AddWithValue("@PlaceOfDrop", DBNull.Value);
            else
            {
                command.Parameters.AddWithValue("@PlaceOfDrop", tr.PlaceOfDrop);
            }
            if (tr.TimeOfDrop == null)
                command.Parameters.AddWithValue("@TimeOfDrop", DBNull.Value);
            else
            {
                command.Parameters.AddWithValue("@TimeOfDrop", tr.TimeOfDrop);
            }
            command.Parameters.AddWithValue("@WantComplimentary", tr.WantComplimentary);
            if (tr.WantComplimentaryDrop == null)
                command.Parameters.AddWithValue("@WantComplimentaryDrop", DBNull.Value);
            else
            {
                command.Parameters.AddWithValue("@WantComplimentaryDrop", tr.WantComplimentaryDrop);
            }

            if (tr.OptionalContact == null)
                command.Parameters.AddWithValue("@OptionalContact", DBNull.Value);
            else
            {
                command.Parameters.AddWithValue("@OptionalContact", tr.OptionalContact);
            }

            if (tr.OptionalEmail == null)
                command.Parameters.AddWithValue("@OptionalEmail", DBNull.Value);
            else
            {
                command.Parameters.AddWithValue("@OptionalEmail", tr.OptionalEmail);
            }

            //************ Royal code changes, inserting discount,code in temptable ~~ END*****

            DataTable dt = Connection.readTab(command, connMSTicket);
            if (dt.Rows.Count > 0)
            {
                return int.Parse(dt.Rows[0][0].ToString());
            }
            else
            {

                return -1;
            }
        }
        /// <summary>
        /// To Insert the HDFC Payment ID into the GINC$BookingTransaction_temp corresponding to the Booking ID
        /// </summary>
        /// <param name="BookingID"></param>
        /// <param name="strPmtId">This Parameter stores the HDFC Payment ID</param>
        public static void Transaction_Temp_Insert_PaymentId(long BookingID, string strPmtId)
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = "UPDATE [GINC$BookingTransaction_temp] SET [ReceiptNo]='" + strPmtId + "' where [BookingID]=" + BookingID + "";
            Connection.EXECommand(command, connMSTicket);
        }
        public static void Update_PaymentStatus(TransactionRecord tr)
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = "UPDATE [GINC$BookingTransaction_temp] SET [PaymentStatus]=1 where [BookingID]=" + tr.ReferenceNo + "";
            Connection.EXECommand(command, connMSTicket);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        public static int _Voucher_Varification_Update(TransactionRecord r)
        {
            SqlCommand command = new SqlCommand();
            string voucher = r.VoucherNo.Replace("|", "','");
            command.CommandText = "declare @Noofvouchers int Select @Noofvouchers = Count(id) from ginc$VoucherIntegrationDetails where"
                + " status = 0 and Active = 1 and Validity_Start_Date <= getdate() and Validity_End_Date >= getdate() and"
                + " Week_day =" + r.Day + "  and Category ='" + r.Category + "'  and  VoucherSerialNo+'-'+voucher_Code"
                + " in('" + voucher + "') if(@Noofvouchers = " + r.TotalSeats + ") begin update  ginc$VoucherIntegrationDetails set"
                + " Voucher_Used_Date= getdate(),Status='1' where VoucherSerialNo+'-'+voucher_Code in('" + voucher + "') "
                + " update  ginc$bookingTransaction_temp set Voucher_Type = '" + r.VoucherType + "',Voucher_No='" + r.VoucherNo + "',"
                + " Voucher_Booking_id='" + r.VoucherBookingID + "',PaymentGateway='',CardType='',CardNo='' , status=1 where ID =  " + r.BookingID
                + "	Select @@Rowcount end else begin select -1 end";
            DataTable dt = Connection.readTab(command, connMSTicket);
            if (dt.Rows.Count > 0)
            {
                return int.Parse(dt.Rows[0][0].ToString());
            }
            else
                return -1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tr"></param>
        /// <returns></returns>
        public static DataTable _Get_Transaction_Detail(TransactionRecord tr)
        {
            SqlCommand command = new SqlCommand();
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Booking ID Response from PG " + tr.BookingID.ToString());
            DataTable chkID = TransactionBOL.Select_MarchPromotionTransactionCounter_IDWise(tr.BookingID);
            if (chkID.Rows.Count > 0)
            {
                if (chkID.Rows[0][0].ToString().Substring(0, 2) == "31")
                {
                    tr.BookingID = long.Parse(chkID.Rows[0][0].ToString());
                }

            }
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Booking ID after converting " + tr.BookingID.ToString());
            Connection.LogEntry(tr.ReferenceNo.ToString(), "Updating Navision-Booking Master Table...", "17", tr.BookingID.ToString());
            int chkdata = 0;
            int exisitng_ticket_booked_count = 0;//to check if the request is again from payment gateway
            try
            {
                ushort attempts = 0;
                do
                {
                    string[] AgentCodeTokens = tr.AgentCode.Split('-');
                    string webpromoID = string.Empty;
                    if (tr.WebPromotionId == null)
                        webpromoID = "''";
                    else
                        webpromoID = "'" + tr.WebPromotionId + "'";
                    if (AgentCodeTokens.Length == 1 && AgentCodeTokens[0] == "WEB")
                    {
                        DataTable dtGcell_count = new DataTable();

                        command.CommandText = "Select Count(1) from " + table_BookingMaster + " WITH(NOLOCK) WHERE [Lock For Booking] = 1 AND [Booked] = 1 AND" +
                                                " [Web Promotion Id] = " + webpromoID + " AND [Web Disc %] = " + tr.DiscountPercentage.ToString() + " AND [Web Booking ID] = '"
                                                + tr.BookingID.ToString() + "' AND [Payment Ref_ No_]= '" + tr.ReferenceNo.ToString() + "' AND  [Agent Code] = ''";
                        dtGcell_count = Connection.readTab(command, connWebBooking);

                        if (dtGcell_count.Rows.Count > 0)
                        {
                            exisitng_ticket_booked_count = int.Parse(dtGcell_count.Rows[0][0].ToString());
                        }
                        if (exisitng_ticket_booked_count > 0)
                        {
                            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write(string.Format("Payment Gateway freez case detected : [{0}]", command.CommandText));
                            chkdata = 1;
                            /*******************Payement Gateway Error Value Code**********************/
                            #region PG_DB for Payment Gateway freez case
                            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Payment Gateway freez case detected entry in PG_DB");
                            int j = GTICKBOL.Insert_Payment_DB("Payment Gateway freez case detected", tr.BookingID.ToString(), tr.PaymentGateway.ToString());
                            #endregion PG_DB for Payment Gateway freez case
                            /*********************End******************************/
                        }
                        command.CommandText = "Update " + table_BookingMaster + " WITH(HOLDLOCK,UPDLOCK) Set [Lock For Booking] = 1 , [Booked] = 1 ," +
                                    " [Web Promotion Id] = " + webpromoID +
                                    ", [Web Disc %] = " + tr.DiscountPercentage.ToString() +
                                    ", [Web Booking ID] = " + tr.BookingID.ToString() + " , [Payment Ref_ No_]= " + tr.ReferenceNo.ToString() + " , [Booking End Date_Time]=getutcdate()," +
                                    " [Agent Code] = '' where  [Agent Code] = '" + tr.ReferenceNo.ToString() + "'";
                    }
                    else
                    {
                        command.CommandText = "Update " + table_BookingMaster + " WITH(HOLDLOCK,UPDLOCK) Set [Lock For Booking] = 1 , [Booked] = 1 ," +
                             " [Web Promotion Id] = " + webpromoID +
                             ", [Web Disc %] = " + tr.DiscountPercentage.ToString() +
                            ", [Web Booking ID] = " + tr.BookingID.ToString() + " , [Payment Ref_ No_]= " + tr.ReferenceNo.ToString() + " , [Booking End Date_Time]=getutcdate()," +
                            " [Agent Code] = '" + AgentCodeTokens[0] + "', [Agent Sub-Code]='" +
                            AgentCodeTokens[1] + "' where  [Agent Code] = '" + tr.ReferenceNo.ToString() + "'";
                    }

                    Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write(string.Format("Executing attempt {0}: [{1}]", attempts.ToString(), command.CommandText));
                    if (chkdata == 0)
                    {
                        //chkdata = Connection.EXECommand(command, connWebBooking);
                        DataTable dt = TransactionBOL.Select_Temptransaction_REFIDWISE(long.Parse(tr.ReferenceNo.ToString()));
                        if (chkID.Rows.Count > 0)
                        {
                            if (chkID.Rows[0][0].ToString().Substring(0, 2) == "31")
                            {
                                dt.Rows[0]["ID"] = long.Parse(chkID.Rows[0][0].ToString());
                            }

                        }
                        Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Booking ID after converting for updating booking master " + dt.Rows[0]["ID"].ToString());
                        Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Row Count : " + dt.Rows.Count);
                        Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Date of booking for updating booking master:" + Convert.ToDateTime(dt.Rows[0]["DateOfBooking"].ToString()));
                        Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Present Date:" + Convert.ToDateTime(DateTime.Now.Date.ToShortDateString()));
                        Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Present Date:" + Convert.ToDateTime(DateTime.Now.Date.AddDays(-1).ToShortDateString()));
                        if (dt != null && dt.Rows.Count > 0 && (Convert.ToDateTime(dt.Rows[0]["DateOfBooking"].ToString()) == Convert.ToDateTime(DateTime.Now.Date.ToShortDateString()) || Convert.ToDateTime(dt.Rows[0]["DateOfBooking"].ToString()) == Convert.ToDateTime(DateTime.Now.Date.AddDays(-1).ToShortDateString())))
                        {
                            try
                            {
                                string WebPromotionID = dt.Rows[0]["WebPromotionId"] != null ? dt.Rows[0]["WebPromotionId"].ToString() : "";
                                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("WebPromotionID : " + WebPromotionID);
                                decimal TotalAmount = string.IsNullOrEmpty(dt.Rows[0]["TotalAmount"].ToString()) ? 0 : Convert.ToDecimal(dt.Rows[0]["TotalAmount"]);
                                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Amount : " + TotalAmount);
                                string Play = dt.Rows[0]["Play"] != null ? dt.Rows[0]["Play"].ToString() : "";
                                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Play : " + Play);
                                string MobileNo = dt.Rows[0]["MobileNo"] != null ? dt.Rows[0]["MobileNo"].ToString() : "";
                                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Mobile No. : " + MobileNo);
                                string BookingID = dt.Rows[0]["ID"] != null ? dt.Rows[0]["ID"].ToString() : "";
                                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Web Booking ID : " + BookingID);
                                string ReferenceNo = dt.Rows[0]["BookingID"] != null ? dt.Rows[0]["BookingID"].ToString() : "";
                                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Reference No. : " + ReferenceNo);
                                int Totalseats = string.IsNullOrEmpty(dt.Rows[0]["TotalSeats"].ToString()) ? 0 : Convert.ToInt32(dt.Rows[0]["TotalSeats"]);
                                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Total Seats : " + Totalseats);
                                string PaymentGateway = dt.Rows[0]["PaymentGateway"] != null ? dt.Rows[0]["PaymentGateway"].ToString() : "";
                                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("PG : " + PaymentGateway);
                                string AgentCode = AgentCodeTokens[0] != null ? AgentCodeTokens[0].ToString() : "";
                                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Agent Code : " + AgentCode);
                                string AgentCodeSubcode = string.Empty;
                                if (AgentCodeTokens.Length == 1 && AgentCodeTokens[0] == "WEB")
                                {
                                    AgentCodeSubcode = "";
                                }
                                else
                                {
                                    AgentCodeSubcode = AgentCodeTokens[1] != null ? AgentCodeTokens[1].ToString() : "";
                                }
                                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Agent Code Sub-code  : " + AgentCodeSubcode);
                                decimal DiscountPercentage = string.IsNullOrEmpty(dt.Rows[0]["DiscountPercentage"].ToString()) ? 0 : Convert.ToDecimal(dt.Rows[0]["DiscountPercentage"]);
                                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Discount % : " + DiscountPercentage);
                                string ReceiptNo = string.IsNullOrEmpty(tr.ReceiptNo.ToString()) ? "" : tr.ReceiptNo.ToString();
                                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Receipt No.  : " + ReceiptNo);
                                decimal AvailedAmount = string.IsNullOrEmpty(dt.Rows[0]["AvailedAmount"].ToString()) ? 0 : Convert.ToDecimal(dt.Rows[0]["AvailedAmount"]);
                                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Availed Amount  : " + AvailedAmount);
                                decimal AvailedPoints = string.IsNullOrEmpty(dt.Rows[0]["AvailedPoints"].ToString()) ? 0 : Convert.ToDecimal(dt.Rows[0]["AvailedPoints"]);
                                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Availed Points  : " + AvailedPoints);
                                string isexecuted = TransactionBOL.Successful_BookingByMsTicket(TotalAmount, Play, MobileNo, ReferenceNo,
                                    Totalseats, PaymentGateway, AgentCode, DiscountPercentage, ReceiptNo, AvailedAmount, AvailedPoints, BookingID, AgentCodeSubcode, WebPromotionID);
                                if (Convert.ToInt32(isexecuted.ToString()) > 0)
                                {
                                    chkdata = Convert.ToInt32(isexecuted.ToString());
                                    Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("NMLIVEDB Updated from PG : " + PaymentGateway);
                                    SqlCommand cmdlog = new SqlCommand("select [Pre Booking Receipt No_],[Booking ID] from [NMLIVEDB].[dbo].[Great Indian Nautanki Company$Booking Master]  where [Web Booking ID]='" + BookingID + "'", connWebBooking);
                                    cmdlog.Connection.Open();
                                    SqlDataAdapter dadplog = new SqlDataAdapter(cmdlog);
                                    DataTable dtbllog = new DataTable();
                                    dadplog.Fill(dtbllog);
                                    if (dtbllog.Rows.Count > 0)
                                    {
                                        for (int i = 0; i < dtbllog.Rows.Count; i++)
                                        {
                                            DataRow drlog = dtbllog.Rows[i];
                                            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Updated pre booking recipt no in booking master and prebooking recipt no is " + drlog["Pre Booking Receipt No_"] + "," + drlog["Booking ID"]);
                                        }
                                    }
                                    else
                                    {
                                        Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Unable to fetch information for web integration");
                                    }
                                    cmdlog.Connection.Close();
                                }
                                else
                                {
                                    Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Unable to update NMLIVEDB." + isexecuted);
                                    /*******************Payement Gateway Error Value Code**********************/
                                    #region PG_DB for Unable to update NMLIVEDB
                                    Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Unable to update NMLIVEDB Entry in PG_DB");
                                    int j = GTICKBOL.Insert_Payment_DB("Unable to update NMLIVEDB", BookingID, PaymentGateway);
                                    #endregion PG_DB for Unable to update NMLIVEDB
                                    /*********************End******************************/
                                }

                            }
                            catch (Exception ex)
                            {
                                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Unable to update NMLIVEDB. " + ex.Message);
                                /*******************Payement Gateway Error Value Code**********************/
                                #region PG_DB for Unable to update NMLIVEDB
                                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Unable to update NMLIVEDB Entry in PG_DB");
                                int j = GTICKBOL.Insert_Payment_DB("Unable to update NMLIVEDB", tr.BookingID.ToString(), tr.PaymentGateway.ToString());
                                #endregion PG_DB for Unable to update NMLIVEDB
                                /*********************End******************************/
                            }
                        }
                    }
                    attempts++;
                }
                while (chkdata == 0 && attempts < 3);

            }
            catch (Exception ex)
            {
                Connection.LogEntry(tr.ReferenceNo.ToString(), "Error occurred while adding transaction-- " + ex, "18", tr.BookingID.ToString());
            }
            finally
            {
                if (connWebBooking.State == ConnectionState.Open)
                    connWebBooking.Close();
            }
            DataTable dtNav = new DataTable();

            if (chkdata > 0)
            {
                if (exisitng_ticket_booked_count == 0)
                {
                    Connection.LogEntry(tr.ReferenceNo.ToString(), "Transaction added to Navision-Booking Master", "19", tr.BookingID.ToString());
                }
            }
            else
            {
                Connection.LogEntry(tr.ReferenceNo.ToString(), "FAILURE: NOT ABLE TO UPDATE Navision-Booking Master", "18", tr.BookingID.ToString());
                command.CommandText = " Select *,[SeatBooked] = 0 From  [GINC$BookingTransaction_temp] where [ID] = " + tr.BookingID.ToString();
                dtNav = Connection.readTab(command, connMSTicket);
                return dtNav;
            }

            //Move succesful transactions into new table...

            DataTable dtGcell = new DataTable();

            command = new SqlCommand();
            try
            {
                command = new SqlCommand();

                Connection.LogEntry(tr.ReferenceNo.ToString(), "Writing Data To GCELL-Transaction Table", "20", tr.BookingID.ToString());
                if (exisitng_ticket_booked_count == 0)
                {
                    command.CommandText = " if  not EXISTS (Select id from ginc$bookingTransaction where [ReferenceNo] ='" + tr.ReferenceNo.ToString() + "') begin " +
                        " insert into GINC$BookingTransaction ([BookingID] ,[ReferenceNo],[Source],[DateOfBooking],[TimeOfBooking]," +
                        " [Location],[Play],[ShowTime],[ShowDate],[Category],[TotalSeats],[TotalAmount],[PaymentType],[CardType],[CardNo]," +
                        " [MobileNo],[EmailID],[Name],[PaymentGateway],[Amex Street],[Amex Pin],[Amex Country],[AgentCode],[BookingType],[Status],[SeatInfo], ReceiptNo, [Address],[IP],[Remark],[PromotionCode],[DiscountPercentage],[WebPromotionId],[RegId],[AvailedAmount],[AvailedPoints],[TopUpAmount],[TopUpTransactionId],[OptionalEmail],[OptionalContact],[IsChecked],[PlaceOfPick],[TimeOfPick],[PlaceOfDrop],[TimeOfDrop],[WantComplimentary],[WantComplimentaryDrop],[PayableAmount],[RoutedThrough]) " +
                        " (SELECT  " + tr.BookingID.ToString() + "," + tr.ReferenceNo.ToString() + ",[Source],'" + DateTime.Now.Date.ToShortDateString() + "','" +
                        DateTime.Now.ToShortTimeString() + "' ,[Location],[Play],[ShowTime],[ShowDate],[Category], [TotalSeats],[TotalAmount]  ," +
                        " [PaymentType],[CardType],[CardNo],[MobileNo],[EmailID],[Name],[PaymentGateway],[Amex Street],[Amex Pin],[Amex Country],[AgentCode],[BookingType],'True'," +
                        " [SeatInfo], '" + tr.ReceiptNo + "', [Address], [IP], [Remark] ,[PromotionCode],[DiscountPercentage],[WebPromotionId],[RegId],[AvailedAmount],[AvailedPoints],[TopUpAmount],[TopUpTransactionId],[OptionalEmail],[OptionalContact],[IsChecked],[PlaceOfPick],[TimeOfPick],[PlaceOfDrop],[TimeOfDrop],[WantComplimentary],[WantComplimentaryDrop],[PayableAmount],[RoutedThrough] from [GINC$BookingTransaction_temp] where  [BookingID] = " + tr.ReferenceNo.ToString() + ")  end " +
                        " Select *, [SeatBooked] = 1, [AlreadyProcessed] = 0 from [GINC$BookingTransaction] where [BookingID] ='" + tr.BookingID.ToString() + "'";
                }
                else
                {
                    command.CommandText = " Select *, [SeatBooked] = 1, [AlreadyProcessed] = 1 from [GINC$BookingTransaction] where [BookingID] ='" + tr.BookingID.ToString() + "'";
                }

                dtGcell = Connection.readTab(command, connMSTicket);

                if (exisitng_ticket_booked_count == 0 && tr.PromotionCode == "MARCHPROMOTION" || tr.PromotionCode == "MONTHOFMARCH")
                {
                    SqlCommand comand = new SqlCommand();
                    comand.CommandText = "Update [dbo].[March_Promotion] set [PGIsPaymentSuccess]=1 where [BookingId]='" + tr.ReferenceNo.ToString() + "'";
                    Connection.EXECommand(comand, connMSTicket);
                }
                if (exisitng_ticket_booked_count == 0 && tr.PromotionCode == "MMT")
                {
                    SqlCommand comand = new SqlCommand();
                    comand.CommandText = "Update [dbo].[tbl_mmtpromotion] set IsPaymentSuccess=1 ,[ReceiptId]='" + tr.ReceiptNo.ToString() + "' where [BookingId] = '" + tr.ReferenceNo.ToString() + "'";
                    Connection.EXECommand(comand, connMSTicket);
                    Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("update mmt promotion table");
                }
                if (exisitng_ticket_booked_count == 0 && tr.PromotionCode == "MANA")
                {
                    SqlCommand comand = new SqlCommand();
                    comand.CommandText = "Update [dbo].[tbl_manapromotion] set IsPaymentSuccess=1 ,[ReceiptId]='" + tr.ReceiptNo.ToString() + "' where [BookingId] = '" + tr.ReferenceNo.ToString() + "'";
                    Connection.EXECommand(comand, connMSTicket);
                }
                if (exisitng_ticket_booked_count == 0 && tr.PromotionCode == "YATRA")
                {
                    SqlCommand comand = new SqlCommand();
                    comand.CommandText = "Update [dbo].[tbl_yatrapromotion] set IsPaymentSuccess=1 ,[ReceiptId]='" + tr.ReceiptNo.ToString() + "' where [BookingId] = '" + tr.ReferenceNo.ToString() + "'";
                    Connection.EXECommand(comand, connMSTicket);
                }
                if (exisitng_ticket_booked_count == 0 && (tr.PromotionCode == "MCOTHERS" || tr.PromotionCode == "MCWORLD"))
                {
                    SqlCommand comand = new SqlCommand();
                    comand.CommandText = "Update [dbo].[MCPROMOTIONS_DETAIL] set IsPaymentSuccess=1 ,[ReceiptId]='" + tr.ReceiptNo.ToString() + "' where [BookingId] = '" + tr.ReferenceNo.ToString() + "'";
                    Connection.EXECommand(comand, connMSTicket);
                }
                if (exisitng_ticket_booked_count == 0)
                {
                    SqlCommand com = new SqlCommand();
                    com.CommandText = "UPDATE [GINC$BookingTransaction_temp] SET [IsProcessed] = 6 where [BookingID]=" + tr.ReferenceNo + "";
                    Connection.EXECommand(com, connMSTicket);
                    Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Update Isprocessed to 6 (Successful Transaction) in booking Transaction_temp table for Reference ID" + tr.ReferenceNo);
                }

                if (dtGcell.Rows.Count > 0)
                {
                    Connection.LogEntry(tr.ReferenceNo.ToString(), "Successfully Written Data To GCELL-Transaction Table", "21", tr.BookingID.ToString());

                    string _totalSeats = "0";
                    decimal totalSeats = 0;

                    if (dtGcell.Rows[0]["TotalSeats"] != null)
                    {
                        totalSeats = decimal.Parse(dtGcell.Rows[0]["TotalSeats"].ToString()) == 0 ? 1 : decimal.Parse(dtGcell.Rows[0]["TotalSeats"].ToString());
                        _totalSeats = totalSeats.ToString();
                    }

                    //Connection.LogEntry(tr.ReferenceNo.ToString(), "Writing Data To SAN CRM", "15", tr.BookingID.ToString());
                    command = new SqlCommand();
                    command.CommandText = "exec ImportMaster '" + dtGcell.Rows[0]["MobileNo"] + "', '" + dtGcell.Rows[0]["Name"] + "','-','" + dtGcell.Rows[0]["EmailID"]
                    + "','WEB','Ticket Book from Internet','" + dtGcell.Rows[0]["BookingID"] + "','" +
                    Convert.ToDateTime(dtGcell.Rows[0]["ShowDate"]).ToShortDateString() + " " + Convert.ToDateTime(dtGcell.Rows[0]["ShowTime"]).ToShortTimeString()
                    + "','" + _totalSeats + "','" + dtGcell.Rows[0]["Category"] + "','" + decimal.Parse(dtGcell.Rows[0]["TotalAmount"].ToString()) + " / " + totalSeats + "','" + dtGcell.Rows[0]["TotalAmount"] + "','" + dtGcell.Rows[0]["SeatInfo"] + "'";

                    Connection.EXECommand(command, connWebCRM);
                }
                else
                {
                    Connection.LogEntry(tr.ReferenceNo.ToString(), "Unable to write Data To GCELL-Transaction Table", "22", tr.BookingID.ToString());
                    /*******************Payement Gateway Error Value Code**********************/
                    #region PG_DB for Unable to write Data To GCELL-Transaction Table
                    Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Unable to write Data To GCELL-Transaction Table Entry in PG_DB");
                    int j = GTICKBOL.Insert_Payment_DB("Unable to write Data To GCELL-Transaction Table", tr.BookingID.ToString(), tr.PaymentGateway.ToString());
                    #endregion PG_DB for Unable to write Data To GCELL-Transaction Table
                    /*********************End******************************/
                }
                return dtGcell;
            }
            catch (Exception ex)
            {
                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("" + ex.Message);
            }
            finally
            {
                if (connMSTicket.State == ConnectionState.Open)
                    connMSTicket.Close();
            }
            return dtNav;
        }

        public static void Get_NewYear_Detail(string BookingId, string ReceiptNo)
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = "Update [dbo].[tbl_NewYearPackages] set PGIsPaymentSuccess=1 ,[PGReceiptId]='" + ReceiptNo.ToString() + "' where [BookingID] = '" + BookingId.ToString() + "'";
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Update Details of PGIsPaymentSuccess and PGReceiptId from DB where Booking ID:" + BookingId + "Receipt No." + ReceiptNo);
            Connection.EXECommand(command, connMSTicket);
        }
        public static void Get_BollyLand_Detail(string BookingId, string ReceiptNo)
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = "Update [dbo].[tbl_BollyLand] set PGIsPaymentSuccess=1 ,[PGReceiptId]='" + ReceiptNo.ToString() + "' where [BookingID] = '" + BookingId.ToString() + "'";
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Update Details of PGIsPaymentSuccess and PGReceiptId from DB where Booking ID:" + BookingId + "Receipt No." + ReceiptNo);
            Connection.EXECommand(command, connMSTicket);
        }
        public static void Get_MMT_Detail(string BookingId, string ReceiptNo)
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = "Update [dbo].[tbl_mmtpromotion] set IsPaymentSuccess=1 ,[ReceiptId]='" + ReceiptNo.ToString() + "' where [BookingId] = '" + BookingId.ToString() + "'";
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Update Details of IsPaymentSuccess and ReceiptId from DB where Booking ID:" + BookingId + "Receipt No." + ReceiptNo);
            Connection.EXECommand(command, connMSTicket);
        }

        public static void Get_Summer_Detail(string BookingId, string ReceiptNo)
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = "Update [dbo].[tbl_summercamp] set IsPaymentSuccess=1 ,[ReceiptId]='" + ReceiptNo.ToString() + "' where [BookingId] = '" + BookingId.ToString() + "'";
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Update Details of IsPaymentSuccess and ReceiptId from DB where Booking ID:" + BookingId + "Receipt No." + ReceiptNo);
            Connection.EXECommand(command, connMSTicket);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="BookingID"></param>
        /// <returns></returns>
        public static DataTable _Select_Temptransaction_REFIDWISE(long BookingID)
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = "Select * from [GINC$BookingTransaction_temp] where [BookingID] = " + BookingID.ToString();
            return Connection.readTab(command, connMSTicket);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="BookingID"></param>
        /// <returns></returns>
        public static DataTable _Select_Temptransaction_transactionIDWise(long BookingID)
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = "if Exists (Select ID from [GINC$BookingTransaction] where [BookingID] = " + BookingID.ToString() + " ) "
                + " Begin Select *,[SeatBooked]=1 from [GINC$BookingTransaction] where [BookingID] = " + BookingID.ToString()
                + " end Else begin SELECT *,[SeatBooked]=0  FROM [GINC$BookingTransaction_temp] where ID = " + BookingID.ToString() + " end";
            return Connection.readTab(command, connMSTicket);
        }


        /// <summary>
        /// For March Promotion
        /// </summary>
        /// <param name="BookingID"></param>
        /// <returns></returns>
        public static DataTable _Select_MarchPromotionTransactionCounter_CounterIDWise(long BookingID)
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = "select Counter,ID FROM [GINC$MarchPromoTransactionCounter] where Counter=" + BookingID;
            DataTable dt = Connection.readTab(command, connMSTicket);
            return Connection.readTab(command, connMSTicket);
        }

        public static DataTable Select_MarchPromotionTransactionCounter_IDWise(long BookingID)
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = "select Counter,ID FROM [GINC$MarchPromoTransactionCounter] where ID=" + BookingID;
            DataTable dt = Connection.readTab(command, connMSTicket);
            return Connection.readTab(command, connMSTicket);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tr"></param>
        /// <returns></returns>
        public static DataSet _Select_Report_FromTransactionTable(TransactionRecord tr)
        {
            DataSet ds = new DataSet();
            SqlCommand command = new SqlCommand();
            string sqlQuery = "Select [Category],SUM([TotalSeats]) as [No Of Seats],SUM([TotalAmount]) as 'Total Amount' from [GINC$BookingTransaction] " +
                " where  id > 0 ";
            if (tr.AgentCode == "1")
                sqlQuery += " and AgentCode = 'WEB' ";
            if (tr.AgentCode == "2")
                sqlQuery += " and AgentCode != 'WEB' ";
            if (tr.BookingID != 0)
                sqlQuery += " and BookingID like '%" + tr.BookingID + "%'";
            if (tr.ReceiptNo != "0")
                sqlQuery += " and ReceiptNo like '%" + tr.ReceiptNo + "%'";
            if (tr.Name != "0")
                sqlQuery += " and  [Name] like '%" + tr.Name + "%' ";
            if (tr.DateOfBooking != "0")
                sqlQuery += " and  Convert(Datetime,[DateOfBooking]) >= Convert(datetime, '" + tr.DateOfBooking + "') ";
            if (tr.Location != "0")
                sqlQuery += " and  Convert(Datetime,[DateOfBooking]) <= Convert(datetime, '" + tr.Location + "') ";
            if (tr.ShowDate != "0")
                sqlQuery += " and  Convert(Datetime,[ShowDate]) >= Convert(datetime, '" + tr.ShowDate + "') ";
            if (tr.MobileNo != "0")
                sqlQuery += " and  Convert(Datetime,[ShowDate]) <= Convert(datetime, '" + tr.MobileNo + "')";
            sqlQuery += " GROUP BY [Category]  order by [Category] Select SUM([TotalSeats]) as [TotalSeats], SUM([TotalAmount])as TotalAmount" +
                ",PaymentGateway  from [GINC$BookingTransaction] where id > 0 ";
            if (tr.AgentCode == "1")
                sqlQuery += " and AgentCode = 'WEB' ";
            if (tr.AgentCode == "2")
                sqlQuery += " and AgentCode != 'WEB' ";
            if (tr.BookingID != 0)
                sqlQuery += " and BookingID like '%" + tr.BookingID + "%'";
            if (tr.ReceiptNo != "0")
                sqlQuery += " and ReceiptNo like '%" + tr.ReceiptNo + "%'";
            if (tr.Name != "0")
                sqlQuery += " and  [Name] like '%" + tr.Name + "%' ";
            if (tr.DateOfBooking != "0")
                sqlQuery += " and  Convert(Datetime,[DateOfBooking]) >= Convert(datetime, '" + tr.DateOfBooking + "') ";
            if (tr.Location != "0")
                sqlQuery += " and  Convert(Datetime,[DateOfBooking]) <= Convert(datetime, '" + tr.Location + "') ";
            if (tr.ShowDate != "0")
                sqlQuery += " and  Convert(Datetime,[ShowDate]) >= Convert(datetime, '" + tr.ShowDate + "') ";
            if (tr.MobileNo != "0")
                sqlQuery += " and  Convert(Datetime,[ShowDate]) <= Convert(datetime, '" + tr.MobileNo + "')";

            sqlQuery += "Group by PaymentGateway";
            command.CommandText = sqlQuery;
            return Connection.readDataSet(sqlQuery, connMSTicket);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="tr"></param>
        /// <returns></returns>
        public static DataTable _Select_Report_SearchFromTransactions(TransactionRecord tr, bool details = true)
        {
            string sqlQuery = "SELECT row_number() over (order by ID) as ID,[BookingID],[ReceiptNo],Convert(nvarchar,[DateOfBooking],107) as [DateOfBooking]" +
                " ,Convert(nvarchar,[TimeOfBooking],108) as [TimeOfBooking] ,[Location],[Play],Convert(nvarchar,[ShowDate],107) as [ShowDate]" +
                " ,Convert(nvarchar,[ShowTime],108) as [ShowTime] ,[Category],[TotalSeats],[TotalAmount],[CardType],[Name],";
            if (details)
                sqlQuery += "[MobileNo],[EmailID],";

            sqlQuery += "[PaymentGateway],[SeatInfo] FROM [GINC$BookingTransaction] where" +
                " BookingID like '%" + tr.BookingID + "%' and ReceiptNo like '%" + tr.ReceiptNo + "%' and [Name] like '%" + tr.Name + "%'";

            if (tr.DateOfBooking != "")
                sqlQuery += " and Convert(Datetime,[DateOfBooking]) like Convert(datetime, '" + tr.DateOfBooking + "')";
            if (tr.ShowDate != "")
                sqlQuery += " and Convert(Datetime,[ShowDate]) like Convert(datetime, '" + tr.ShowDate + "')";
            sqlQuery += " ORDER BY BookingID";

            return Connection.readTab(sqlQuery, connMSTicket);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tr"></param>
        /// <returns></returns>
        public static DataTable _Select_Report_SearchFromTransactionsTemp(TransactionRecord tr, bool details = true)
        {
            string sqlQuery = "SELECT row_number() over (order by ID) as ID,[BookingID],Convert(nvarchar,[DateOfBooking],107) as [DateOfBooking]" +
                " ,Convert(nvarchar,[TimeOfBooking],108) as [TimeOfBooking] ,[Location],[Play],Convert(nvarchar,[ShowDate],107) as [ShowDate]" +
                " ,Convert(nvarchar,[ShowTime],108) as [ShowTime] ,[Category],[TotalSeats],[TotalAmount],[CardType],[Name],";

            if (details)
                sqlQuery += "[MobileNo],[EmailID],";

            sqlQuery += "[PaymentGateway],[SeatInfo] FROM [GINC$BookingTransaction_temp] where" +
                " BookingID like '%" + tr.BookingID + "%' and [Name] like '%" + tr.Name + "%' ";

            if (tr.DateOfBooking != "")
                sqlQuery += " and Convert(Datetime,[DateOfBooking]) like Convert(datetime, '" + tr.DateOfBooking + "')";
            if (tr.ShowDate != "")
                sqlQuery += " and Convert(Datetime,[ShowDate]) like Convert(datetime, '" + tr.ShowDate + "')";
            sqlQuery += " ORDER BY BookingID";
            return Connection.readTab(sqlQuery, connMSTicket);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_BookingID"></param>
        /// <param name="_ReceiptNo"></param>
        /// <param name="_DateOfBooking"></param>
        /// <param name="_Location"></param>
        /// <param name="_ShowDate"></param>
        /// <param name="_MobileNo"></param>
        /// <param name="_Name"></param>
        /// <param name="_AgentCode"></param>
        /// <returns></returns>
        public static DataSet _Select_Report_SearchFromTransactions_DS(long _BookingID, string _ReceiptNo, string _DateOfBookingFrom, string _DateOfBookingTo, string _ShowDateFrom, string _ShowDateTo, string _Name, string _AgentCode, bool details = true)
        {
            string sqlQuery = "SELECT row_number() over (order by ID desc) as [S.N.],[BookingID] as [Booking ID],Case  when [ReceiptNo] like('%-%') then" +
                " '-' else [ReceiptNo] end as [Receipt No],(Convert(nvarchar,[DateOfBooking],105)+' '+ Convert(nvarchar(5),[TimeOfBooking],108))" +
                " as [Booking Date] ,[Play], (Convert(nvarchar,[ShowDate],105)+' '+ Convert(nvarchar(5),[ShowTime],108)) as [Show Date],[Category]" +
                ",[TotalSeats] as [Seats], [TotalAmount] as 'Amount (INR)',PaymentGateway as [Card Type],[Name]" +
                " as [Customer Name]";
            if (details)
                sqlQuery += ",[MobileNo],[EmailID]";
            sqlQuery += ",Replace([SeatInfo],',',', ') as [Seat Info] ";
            if (_AgentCode != "1")
                sqlQuery += ", [AgentCode] ";
            sqlQuery += " FROM [GINC$BookingTransaction] where   ID > 0 ";
            if (_AgentCode == "1")
                sqlQuery += " and AgentCode = 'WEB' ";
            if (_AgentCode == "2")
                sqlQuery += " and AgentCode != 'WEB' ";
            if (_BookingID != 0)
                sqlQuery += " and BookingID like '%" + _BookingID + "%'";
            if (_ReceiptNo != "0")
                sqlQuery += " and ReceiptNo like '%" + _ReceiptNo + "%'";
            if (_Name != "0")
                sqlQuery += " and  [Name] like '%" + _Name + "%' ";
            if (_DateOfBookingFrom != "0")
                sqlQuery += " and  Convert(Datetime,[DateOfBooking]) >= Convert(datetime, '" + _DateOfBookingFrom + "') ";
            if (_DateOfBookingTo != "0")
                sqlQuery += " and  Convert(Datetime,[DateOfBooking]) <= Convert(datetime, '" + _DateOfBookingTo + "') ";
            if (_ShowDateFrom != "0")
                sqlQuery += " and  Convert(Datetime,[ShowDate]) >= Convert(datetime, '" + _ShowDateFrom + "') ";
            if (_ShowDateTo != "0")
                sqlQuery += " and  Convert(Datetime,[ShowDate]) <= Convert(datetime, '" + _ShowDateTo + "')";
            sqlQuery += " ORDER BY BookingID";


            return Connection.readDataSet(sqlQuery, connMSTicket);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tr"></param>
        /// <returns></returns>
        public static DataSet _Select_Report_SearchFromTransactionsTemp_DS(TransactionRecord tr)
        {
            SqlCommand command = new SqlCommand();
            string sqlQuery = "Create Table #_Total(RowNumber   Int,ID   bigint,BookingID bigint,ErrorTxt  nvarchar(max),PaymentGateway nvarchar(50))" +
                "Insert Into #_Total Select * From((SELECT * FROM (SELECT ROW_NUMBER() OVER (PARTITION BY bookingID  ORDER BY ID DESC) AS RowNumber,ID,BookingID,ErrorTxt,PaymentGateway FROM [dbo].[PG_DB]) T WHERE RowNumber = 1 ) )as k " +
                "SELECT row_number() over (order by a.ID desc) as SNO,a.ID as 'Booking ID', a.ReferenceNo as 'Receipt No',(Convert(nvarchar," +
                "a.[DateOfBooking],105)+' '+ Convert(nvarchar(5),a.[TimeOfBooking],108)) as [Booking Date] ,a.[Play], (Convert(nvarchar,a.[ShowDate],105)+" +
                "' '+ Convert(nvarchar(5),a.[ShowTime],108)) as [Show Date],a.[Category],a.[TotalSeats] as [Seats], a.[TotalAmount]," +
                "a.PaymentGateway as [Card Type],a.[Name] as [Customer Name],a.[MobileNo],a.[EmailID],Replace(a.[SeatInfo],',',', ') as [SeatInfo], a.[AgentCode],b.ErrorTxt,f.[Error_Detail] as [Utility Result] ";
            sqlQuery += " FROM [GINC$BookingTransaction_temp] as a left join #_Total as b ON cast(a.ID as nvarchar)=b.bookingID left join [IDBI_PG_Details] as f on a.ID=f.[Id] where a.id > 0 and a.bookingid <> a.ReferenceNo and a.id not in(select bookingid from [GINC$BookingTransaction]) " +
                "and a.id not in (select b.Id from [GINC$BookingTransaction] a join [GINC$MarchPromoTransactionCounter] b on a.bookingid=b.counter)";
            if (tr.AgentCode == "1")
                sqlQuery += " and a.AgentCode = 'WEB' ";
            if (tr.AgentCode == "2")
                sqlQuery += " and a.AgentCode != 'WEB' ";
            if (tr.BookingID != 0)
                sqlQuery += " and a.ID like '%" + tr.BookingID + "%'";
            if (tr.ReceiptNo != "0")
                sqlQuery += " and a.ReferenceNo like '%" + tr.ReceiptNo + "%'";
            if (tr.Name != "0")
                sqlQuery += " and  a.[Name] like '%" + tr.Name + "%' ";
            if (tr.DateOfBooking != "0")
                sqlQuery += " and  Convert(Datetime,a.[DateOfBooking]) >= Convert(datetime, '" + tr.DateOfBooking + "') ";
            if (tr.Location != "0")
                sqlQuery += " and  Convert(Datetime,a.[DateOfBooking]) <= Convert(datetime, '" + tr.Location + "') ";
            if (tr.ShowDate != "0")
                sqlQuery += " and  Convert(Datetime,a.[ShowDate]) >= Convert(datetime, '" + tr.ShowDate + "') ";
            if (tr.MobileNo != "0")
                sqlQuery += " and  Convert(Datetime,a.[ShowDate]) <= Convert(datetime, '" + tr.MobileNo + "')";
            sqlQuery += " ORDER BY a.ID";
            //string sqlQuery = "SELECT GINC$BookingTransaction_temp.ID, GINC$BookingTransaction_temp.Source, GINC$BookingTransaction_temp.DateOfBooking, GINC$BookingTransaction_temp.TimeOfBooking, GINC$BookingTransaction_temp.ShowDate, GINC$BookingTransaction_temp.ShowTime, GINC$BookingTransaction_temp.Play, GINC$BookingTransaction_temp.TotalAmount, GINC$BookingTransaction_temp.TotalSeats, GINC$BookingTransaction_temp.Category, GINC$BookingTransaction_temp.PaymentType, GINC$BookingTransaction_temp.CardType, GINC$BookingTransaction_temp.MobileNo, GINC$BookingTransaction_temp.EmailID, GINC$BookingTransaction_temp.Name, GINC$BookingTransaction_temp.PaymentGateway, GINC$BookingTransaction_temp.AgentCode, GINC$BookingTransaction_temp.Status, GINC$BookingTransaction_temp.SeatInfo FROM GINC$BookingTransaction_temp WHERE GINC$BookingTransaction_temp.ID NOT IN (SELECT GINC$BookingTransaction.BookingID FROM GINC$BookingTransaction WHERE GINC$BookingTransaction.DateOfBooking > '2012-05-01') and GINC$BookingTransaction_temp.DateOfBooking > '2012-05-01'";
            command.CommandText = sqlQuery;
            command.CommandTimeout = 6000;
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("unsucessfull query: " + sqlQuery);
            return Connection.readDataSet(command, connMSTicket);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_BookingID"></param>
        /// <returns></returns>
        public static DataTable _get_LogDetails_From_Booking_Status(long _BookingID)
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = "Select * from ginc$bookingStatus where id > 0 and referenceid in(Select Distinct(referenceid)" +
                " From ginc$bookingStatus where BookingID ='" + _BookingID + "') order by Date";
            return Connection.readTab(command, connMSTicket);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_BookingID"></param>
        /// <param name="_ReceiptNo"></param>
        /// <param name="_DateOfBooking"></param>
        /// <param name="_Location"></param>
        /// <param name="_ShowDate"></param>
        /// <param name="_MobileNo"></param>
        /// <param name="_Name"></param>
        /// <param name="_AgentCode"></param>
        /// <param name="_PaymentType"></param>
        /// <returns></returns>
        public static DataSet _get_ALL_LogDetails(long _BookingID, string _ReceiptNo, string _DateOfBooking, string _Location, string _ShowDate, string _MobileNo, string _Name, string _AgentCode, string _PaymentType)
        {
            string sqlQuery = "SELECT [BookingID],Case  when [ReceiptNo] like('%-%') then '-' else [ReceiptNo] end as [ReceiptNo],(Convert(nvarchar," +
                " [DateOfBooking],105)+' '+ Convert(nvarchar(5),[TimeOfBooking],108)) as [Booking Date] ,[Play], (Convert(nvarchar,[ShowDate],105)" +
                "+' '+ Convert(nvarchar(5),[ShowTime],108)) as [Show Date] ,[Category],[TotalSeats],[TotalAmount],PaymentGateway as [Card Type]," +
                " [MobileNo],[EmailID],[Name],[PaymentGateway],[SeatInfo],AgentCode,sta='S' FROM [GINC$BookingTransaction] where id > 0 ";
            if (_AgentCode == "1")
                sqlQuery += " and AgentCode = 'WEB' ";
            if (_AgentCode == "2")
                sqlQuery += " and AgentCode != 'WEB' ";
            if (_BookingID != 0)
                sqlQuery += " and BookingID like '%" + _BookingID + "%'";
            if (_ReceiptNo != "0")
                sqlQuery += " and ReceiptNo like '%" + _ReceiptNo + "%'";
            if (_Name != "0")
                sqlQuery += " and  [Name] like '%" + _Name + "%' ";
            if (_DateOfBooking != "0")
                sqlQuery += " and  Convert(Datetime,[DateOfBooking]) >= Convert(datetime, '" + _DateOfBooking + "') ";
            if (_Location != "0")
                sqlQuery += " and  Convert(Datetime,[DateOfBooking]) <= Convert(datetime, '" + _Location + "') ";
            if (_ShowDate != "0")
                sqlQuery += " and  Convert(Datetime,[ShowDate]) >= Convert(datetime, '" + _ShowDate + "') ";
            if (_MobileNo != "0")
                sqlQuery += " and  Convert(Datetime,[ShowDate]) <= Convert(datetime, '" + _MobileNo + "')";
            if (_PaymentType != "0")
            {
                sqlQuery += " and BookingID in ";
                switch (_PaymentType)
                {
                    case "1":
                        sqlQuery += "(Select Distinct(BookingID) From ginc$bookingStatus where referenceid in " +
                        "( Select Distinct(referenceid) from ginc$bookingStatus where status > 0) and bookingid !='')";
                        break;
                    case "2":
                        sqlQuery += "(Select Distinct(BookingID) from ginc$bookingStatus where referenceid not in" +
                            " (Select distinct(referenceid)  From ginc$bookingStatus where  status = 9) and bookingid !='' ) ";
                        break;
                    case "3":
                        sqlQuery += "( Select Distinct(BookingID) From ginc$bookingStatus where referenceid in " +
                        "( Select Distinct(referenceid) from ginc$bookingStatus where  status = 17 or status = 9) and bookingid !='' )";
                        break;
                    case "4":
                        sqlQuery += " (Select Distinct(BookingID) From ginc$bookingStatus where referenceid in " +
                        "( Select Distinct(referenceid) from ginc$bookingStatus where status = 17) and bookingid !='' )";
                        break;
                    case "5":
                        sqlQuery += " (Select Distinct(BookingID) From ginc$bookingStatus where referenceid in " +
                        "( Select Distinct(referenceid) from ginc$bookingStatus where status = 15) and bookingid !='' )";
                        break;
                    case "6":
                        sqlQuery += " (Select Distinct(BookingID) From ginc$bookingStatus where referenceid in " +
                        "( Select Distinct(referenceid) from ginc$bookingStatus where status = 19) and bookingid !='' )";
                        break;
                }
            }

            sqlQuery += "union SELECT ID,Case  when [ReferenceNo] like('%-%') then '-' else [ReferenceNo] end as [ReceiptNo],(Convert(nvarchar, " +
                "[DateOfBooking],105)+' '+ Convert(nvarchar(5),[TimeOfBooking],108)) as [Booking Date] ,[Play], (Convert(nvarchar,[ShowDate],105)+' " +
                "'+ Convert(nvarchar(5),[ShowTime],108)) as [Show Date] ,[Category],[TotalSeats],[TotalAmount],PaymentGateway as [Card Type],[MobileNo],[EmailID],[Name], [PaymentGateway]," +
                "[SeatInfo],AgentCode,sta='F' FROM [GINC$BookingTransaction_temp] where id > 0 and id not in(select bookingid from" +
                " [GINC$BookingTransaction]) ";
            if (_AgentCode == "1")
                sqlQuery += " and AgentCode = 'WEB' ";
            if (_AgentCode == "2")
                sqlQuery += " and AgentCode != 'WEB' ";
            if (_BookingID != 0)
                sqlQuery += " and ID like '%" + _BookingID + "%'";
            if (_ReceiptNo != "0")
                sqlQuery += " and ReferenceNo like '%" + _ReceiptNo + "%'";
            if (_Name != "0")
                sqlQuery += " and  [Name] like '%" + _Name + "%' ";
            if (_DateOfBooking != "0")
                sqlQuery += " and  Convert(Datetime,[DateOfBooking]) >= Convert(datetime, '" + _DateOfBooking + "') ";
            if (_Location != "0")
                sqlQuery += " and  Convert(Datetime,[DateOfBooking]) <= Convert(datetime, '" + _Location + "') ";
            if (_ShowDate != "0")
                sqlQuery += " and  Convert(Datetime,[ShowDate]) >= Convert(datetime, '" + _ShowDate + "') ";
            if (_MobileNo != "0")
                sqlQuery += " and  Convert(Datetime,[ShowDate]) <= Convert(datetime, '" + _MobileNo + "')";
            if (_PaymentType != "0")
            {
                sqlQuery += " and ID in ";
                switch (_PaymentType)
                {
                    case "1":
                        sqlQuery += "(Select Distinct(BookingID) From ginc$bookingStatus where referenceid in " +
                        "( Select Distinct(referenceid) from ginc$bookingStatus where status > 0) and bookingid !='')";
                        break;
                    case "2":
                        sqlQuery += "(Select Distinct(BookingID) from ginc$bookingStatus where referenceid not in" +
                            " (Select distinct(referenceid)  From ginc$bookingStatus where  status = 9) and bookingid !='' ) ";
                        break;
                    case "3":
                        sqlQuery += "( Select Distinct(BookingID) From ginc$bookingStatus where referenceid in " +
                        "( Select Distinct(referenceid) from ginc$bookingStatus where  status = 17 or status = 9) and bookingid !='' )";
                        break;
                    case "4":
                        sqlQuery += " (Select Distinct(BookingID) From ginc$bookingStatus where referenceid in " +
                        "( Select Distinct(referenceid) from ginc$bookingStatus where status = 17) and bookingid !='' )";
                        break;
                    case "5":
                        sqlQuery += " (Select Distinct(BookingID) From ginc$bookingStatus where referenceid in " +
                        "( Select Distinct(referenceid) from ginc$bookingStatus where status = 15) and bookingid !='')";
                        break;
                    case "6":
                        sqlQuery += " (Select Distinct(BookingID) From ginc$bookingStatus where referenceid in " +
                        "( Select Distinct(referenceid) from ginc$bookingStatus where status = 19) and bookingid !='' )";
                        break;
                }
            }
            sqlQuery += " order by BookingID desc";
            return Connection.readDataSet(sqlQuery, connMSTicket);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_BookingID"></param>
        /// <returns></returns>
        public static DataTable _get_LogDetails_StatusWise(long _BookingID)
        {
            SqlCommand command = new SqlCommand();
            string sqlQuery = String.Empty;
            switch (_BookingID)
            {
                case 1:
                    sqlQuery = "Select Distinct(bookingid) From ginc$bookingStatus where referenceid in " +
                    "( Select Distinct(referenceid) from ginc$bookingStatus where status > 0) and bookingid !='' order by bookingid desc";
                    break;
                case 2:
                    sqlQuery = "Select Distinct(bookingid) from ginc$bookingStatus where referenceid not in" +
                        " (Select distinct(referenceid)  From ginc$bookingStatus where  status = 9) and bookingid !='' order by bookingid desc ";
                    break;
                case 3:
                    sqlQuery = " Select Distinct(bookingid) From ginc$bookingStatus where referenceid in " +
                    "( Select Distinct(referenceid) from ginc$bookingStatus where  status = 17 or status = 9) and bookingid !='' order by bookingid";
                    break;
                case 4:
                    sqlQuery = " Select Distinct(bookingid) From ginc$bookingStatus where referenceid in " +
                    "( Select Distinct(referenceid) from ginc$bookingStatus where status = 17) and bookingid !='' order by bookingid";
                    break;
                case 5:
                    sqlQuery = " Select Distinct(bookingid) From ginc$bookingStatus where referenceid in " +
                    "( Select Distinct(referenceid) from ginc$bookingStatus where status = 15) and bookingid !='' order by bookingid";
                    break;
                case 6:
                    sqlQuery = " Select Distinct(bookingid) From ginc$bookingStatus where referenceid in " +
                    "( Select Distinct(referenceid) from ginc$bookingStatus where status = 19) and bookingid !='' order by bookingid";
                    break;
            }
            command.CommandText = sqlQuery;
            return Connection.readTab(command, connMSTicket);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tr"></param>
        /// <returns></returns>
        public static DataSet _Settle_Transaction_Details(TransactionRecord tr)
        {
            string sqlQuery = "Update Ginc$BookingTransaction_temp set [SeatInfo] = '" + tr.SeatInfo + "', Status = 0  where id = " + tr.ID.ToString() +
                " SELECT row_number() over (order by ID desc) as SNO,ID,ReferenceNo,Convert(nvarchar,[DateOfBooking],107) as [DateOfBooking]" +
                " ,Convert(nvarchar,[TimeOfBooking],108) as [TimeOfBooking] ,[Location],[Play],Convert(nvarchar,[ShowDate],107) as [ShowDate]" +
                " ,Convert(nvarchar,[ShowTime],108) as [ShowTime] ,[Category],[TotalSeats],[TotalAmount],[CardType],[MobileNo],[EmailID],[Name]," +
                " [PaymentGateway],[SeatInfo],Status FROM [GINC$BookingTransaction_temp] where AgentCode = 'WEB' and id > 0 and  bookingid <> ReferenceNo " +
                " and id not in(select bookingid from [GINC$BookingTransaction]) ";
            if (tr.BookingID != 0)
                sqlQuery += " and ID like '%" + tr.BookingID.ToString() + "%'";
            if (tr.ReceiptNo != "0")
                sqlQuery += " and ReceiptNo like '%" + tr.ReceiptNo + "%'";
            if (tr.Name != "0")
                sqlQuery += " and  [Name] like '%" + tr.Name + "%' ";
            if (tr.DateOfBooking != "0")
                sqlQuery += " and  Convert(Datetime,[DateOfBooking]) >= Convert(datetime, '" + tr.DateOfBooking + "') ";
            if (tr.Location != "0")
                sqlQuery += " and  Convert(Datetime,[DateOfBooking]) <= Convert(datetime, '" + tr.Location + "') ";
            if (tr.ShowDate != "0")
                sqlQuery += " and  Convert(Datetime,[ShowDate]) >= Convert(datetime, '" + tr.ShowDate + "') ";
            if (tr.MobileNo != "0")
                sqlQuery += " and  Convert(Datetime,[ShowDate]) <= Convert(datetime, '" + tr.MobileNo + "')";

            return Connection.readDataSet(sqlQuery, connMSTicket);
        }

        //#region Valentine
        //public static DataSet InsertValentineData(TransactionRecord tr)
        //{
        //    string sqlQuery = "INSERT INTO [tab_valentineOffer]([TempID],[BaseAmt],[NoOfCouples],[TotalAmt],[EventName],[PaymantGateway],[PName]" +
        //       ",[PContact],[PEmail],[PAddress],[IsSuccess],[Issued]) values (" + tr.ReferenceNo.ToString() + ",3499," + tr.NoOfCouple.ToString() + "," + tr.TotalAmount.ToString() + ",'" + tr.Play + "','" +
        //        tr.PaymentGateway + "','" + tr.Name + "','" + tr.MobileNo + "','" + tr.EmailID + "','" + tr.Address + "',0,0)" +
        //       " Select @@Identity";
        //    return Connection.readDataSet(sqlQuery, connMSTicket);
        //}
        //public static DataSet UpdateValentineData(long BookingID, int ReceiptNo)
        //{
        //    string sqlQuery = "Update [tab_valentineOffer] set [IsSuccess] = 1 , RecieptNo = '" + ReceiptNo + "' where BookingID = " + BookingID +
        //       " Select * from tab_valentineOffer  where BookingID = " + BookingID;
        //    return Connection.readDataSet(sqlQuery, connMSTicket);
        //}
        //public static DataSet UpdateValentineData_Tracker(long BookingID)
        //{
        //    string sqlQuery = "Update [tab_valentineOffer] set [Issued] = 1  where BookingID = " + BookingID + " Select * from tab_valentineOffer ";
        //    return Connection.readDataSet(sqlQuery, connMSTicket);
        //}
        //public static DataSet SelectValentineData(long BookingID)
        //{
        //    string sqlQuery = " Select * from tab_valentineOffer  where BookingID = " + BookingID;
        //    return Connection.readDataSet(sqlQuery, connMSTicket);
        //}
        //public static DataSet SelectValentineData_Tracker(long BookingID, string Name, string BookingType)
        //{
        //    string sqlQuery = " Select * from tab_valentineOffer  where BookingID > 0 ";
        //    if (BookingID != 0)
        //        sqlQuery += " and BookingID like '%" + BookingID + "%'";
        //    if (Name != "0")
        //        sqlQuery += " and PName like '%" + Name + "%'";
        //    if (BookingType != "2")
        //        sqlQuery += " and isSuccess = " + BookingType;
        //    return Connection.readDataSet(sqlQuery, connMSTicket);
        //}
        //#endregion

        public static void Redeem_Points(string RegID, Decimal RedeemAmount, Decimal RedeemPoints, Decimal TotalAmount, string Play, string CustomerNo, string ReferenceNO, int NoOfTickets)
        {
            try
            {
                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Redeem Points Start");
                //string CONNSTR = "Data Source=122.180.79.190; Initial Catalog=NMLIVEDB; User Id=WEB; Password=K1ng@123";
                //Connection.LogEntry(RegID, RedeemAmount.ToString(), RedeemPoints.ToString(), TotalAmount.ToString(), Play, CustomerNo, ReferenceNO, NoOfTickets.ToString());
                SqlCommand cmd = new SqlCommand("Proc_RedeemPoints", connWebBooking);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter MemberID = cmd.Parameters.Add("@RegId", SqlDbType.NVarChar);
                MemberID.Value = RegID;

                SqlParameter RedeemAmt = cmd.Parameters.Add("@Amount", SqlDbType.Decimal);
                RedeemAmt.Value = RedeemAmount;

                SqlParameter RedeemPnts = cmd.Parameters.Add("@Points", SqlDbType.Decimal);
                RedeemPnts.Value = RedeemPoints;

                SqlParameter TtlAmt = cmd.Parameters.Add("@TotalAmount", SqlDbType.Decimal);
                TtlAmt.Value = TotalAmount;

                SqlParameter play = cmd.Parameters.Add("@Play", SqlDbType.VarChar);
                play.Value = Play;

                SqlParameter MobNo = cmd.Parameters.Add("@CustomerNo", SqlDbType.NVarChar);
                MobNo.Value = CustomerNo;

                SqlParameter RefNO = cmd.Parameters.Add("@ReferenceNO", SqlDbType.NVarChar);
                RefNO.Value = ReferenceNO;

                SqlParameter Tickets = cmd.Parameters.Add("@NoOfTickets", SqlDbType.Int);
                Tickets.Value = NoOfTickets;

                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Redeem Points End");
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (connWebBooking.State != ConnectionState.Closed)
                {
                    connWebBooking.Close();
                }
            }

        }

        public static string Card_Transaction(string regid, decimal transactionAmt, DateTime Date, string ReceiptNo, int TransType)
        {
            try
            {
                //Connection.LogEntry(regid, transactionAmt.ToString(), Date.ToString(), ReceiptNo, TransType.ToString());

                SqlCommand cmd = new SqlCommand("Proc_CardTransaction", connWebBooking);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter MemberID = cmd.Parameters.Add("@MemberId", SqlDbType.NVarChar);
                MemberID.Value = regid;

                SqlParameter TransAMT = cmd.Parameters.Add("@TransactionAmt", SqlDbType.Decimal);
                TransAMT.Value = transactionAmt;

                SqlParameter TransDate = cmd.Parameters.Add("@TransactionDate", SqlDbType.DateTime);
                TransDate.Value = Date;

                SqlParameter Receipt = cmd.Parameters.Add("@ReceiptNo", SqlDbType.VarChar);
                Receipt.Value = ReceiptNo;

                SqlParameter TransactionType = cmd.Parameters.Add("@TransactionType", SqlDbType.Int);
                TransactionType.Value = TransType;

                cmd.Connection.Open();
                IDataReader dr = cmd.ExecuteReader();
                string TransactionId = String.Empty;
                if (dr.Read())
                {
                    TransactionId = dr["Transaction_Id"].ToString();
                }

                return TransactionId;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (connWebBooking.State != ConnectionState.Closed)
                {
                    connWebBooking.Close();
                }
            }
        }

        public static void Top_Up(string TransID)
        {
            try
            {
                //Connection.LogEntry(TransID);

                SqlCommand cmd = new SqlCommand("Proc_TopUp", connWebBooking);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter TranasctionID = cmd.Parameters.Add("@TrasactionId", SqlDbType.VarChar);
                TranasctionID.Value = TransID;

                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (connWebBooking.State != ConnectionState.Closed)
                {
                    connWebBooking.Close();
                }
            }
        }

        public static DataSet Detailed_Report(DateTime startDateOfBooking, DateTime EndDateOfBooking, string AgentCode, string Play)
        {
            try
            {
                //Connection.LogEntry(regid, transactionAmt.ToString(), Date.ToString(), ReceiptNo, TransType.ToString());

                SqlCommand cmd = new SqlCommand("DetailedReport", connMSTicket);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter startDate = cmd.Parameters.Add("@startDOB", SqlDbType.DateTime);
                startDate.Value = startDateOfBooking;

                SqlParameter EndDate = cmd.Parameters.Add("@endDOB", SqlDbType.DateTime);
                EndDate.Value = EndDateOfBooking;

                SqlParameter Agent = cmd.Parameters.Add("@agent", SqlDbType.NVarChar);
                Agent.Value = AgentCode;

                SqlParameter play = cmd.Parameters.Add("@Ply", SqlDbType.NVarChar);
                play.Value = Play;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet dataset = new DataSet();
                da.Fill(dataset);
                return dataset;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (connWebBooking.State != ConnectionState.Closed)
                {
                    connWebBooking.Close();
                }
            }
        }

        public static DataSet Hotel_Report(string startDateOfBooking, string EndDateOfBooking, string HotelName)
        {
            string sqlQuery = "SELECT row_number() over (order by ID desc) as [S.N.],[BookingID] as [Booking ID],Case  when [ReceiptNo] like('%-%') then" +
                " '-' else [ReceiptNo] end as [Receipt No],(Convert(nvarchar,[DateOfBooking],105)+' '+ Convert(nvarchar(5),[TimeOfBooking],108))" +
                " as [Booking Date] ,[Play], (Convert(nvarchar,[ShowDate],105)+' '+ Convert(nvarchar(5),[ShowTime],108)) as [Show Date],[Category]" +
                ",[TotalSeats] as [Seats], [TotalAmount] as 'Amount (INR)',CAST([DiscountPercentage] as decimal(18,2)) as 'Discounted %age',CAST(Round(TotalAmount*((100-DiscountPercentage)/100),0) as decimal(18,2)) as [Discounted Amount],PaymentGateway as [Card Type],[Name]" +
                " as [Customer Name], [PromotionCode] FROM [GINC$BookingTransaction] where   ID > 0 ";
            if (HotelName == "SELECT")
                sqlQuery += "and PromotionCode!=''";
            if (HotelName != "SELECT")
                sqlQuery += " and PromotionCode = '" + HotelName + "'";
            if (startDateOfBooking != "0")
                sqlQuery += " and  Convert(Datetime,[DateOfBooking]) >= Convert(datetime, '" + startDateOfBooking + "') ";
            if (EndDateOfBooking != "0")
                sqlQuery += " and  Convert(Datetime,[DateOfBooking]) <= Convert(datetime, '" + EndDateOfBooking + "') ";

            sqlQuery += " ORDER BY BookingID";


            return Connection.readDataSet(sqlQuery, connMSTicket);
        }

        public static DataSet Select_Report_tbl_NewYearPackages(string BookingID, string BookingDateFrom, string BookingDateTo, string Name, string pgReceipt, string Package, int paymentStatus)
        {
            string sqlQuery = "SELECT row_number() over (order by ID desc) as [S.N.],[BookingId] as [Booking ID],[Qty_PackageTypeCouple] as Couple,[Qty_PackageTypeSingle] as Single,[Qty_PackageTypeTeen] as Teens,[Qty_PackageTypeKid] as Kids, " +
                    "[TotalAmount] as Amount,Convert(nvarchar,[DateOfBooking],100) as [Date]," +
                    "[Name]as Name, [EmailId] as Email, [ContactNumber] as Phone,[PGIsPaymentSuccess] as [Payment Status],[Royal Info] as [Royal Card Detail], PGReceiptId " +
                    "from dbo.tbl_NewYearPackages where ID > 0 and YEAR([DateOfBooking])=2013 and MONTH([DateOfBooking])=12 and DAY([DateOfBooking])>2";

            if (BookingID != "0" || string.IsNullOrEmpty(BookingID))
                sqlQuery += "and BookingID like '%" + BookingID + "%'";
            if (pgReceipt != "0" || string.IsNullOrEmpty(pgReceipt))
                sqlQuery += " and PGReceiptId like '%" + pgReceipt + "%'";
            if (Name != "0" || string.IsNullOrEmpty(Name))
                sqlQuery += " and  [Name] like '%" + Name + "%' ";
            if (BookingDateTo != "0" || string.IsNullOrEmpty(BookingDateTo))
                sqlQuery += " and  Convert(Date,[DateOfBooking]) <= Convert(Date, '" + BookingDateTo + "') ";
            if (BookingDateFrom != "0" || string.IsNullOrEmpty(BookingDateFrom))
                sqlQuery += " and  Convert(Date,[DateOfBooking]) >= Convert(Date, '" + BookingDateFrom + "') ";
            if (paymentStatus == 0 || string.IsNullOrEmpty(Package))    // 0 stands for failure
                sqlQuery += "and PGIsPaymentSuccess = 'false'";
            if (paymentStatus == 1)                                    // 1 stands for success, 2 for both success & failure
                sqlQuery += "and PGIsPaymentSuccess = 'true'";
            //if (Package != "Select" || string.IsNullOrEmpty(Package))
            //    sqlQuery += "and PackageType like '%" + Package + "%'";

            sqlQuery += " ORDER BY BookingID";

            return Connection.readDataSet(sqlQuery, connMSTicket);
        }
        public static DataSet Select_Report_tbl_BollyLand(string BookingID, string BookingDateFrom, string BookingDateTo, string Name, string pgReceipt, string Package, int paymentStatus)
        {
            string sqlQuery = "SELECT row_number() over (order by ID desc) as [S.N.],[BookingId] as [Booking ID],[Qty_Gold] as Gold,[Qty_silver] as Silver, " +
                    "[TotalAmount] as Amount,Convert(nvarchar,[DateOfBooking],100) as [Date]," +
                    "[Name]as Name, [EmailId] as Email, [ContactNumber] as Phone,[PGIsPaymentSuccess] as [Payment Status],PGReceiptId " +
                    "from dbo.tbl_BollyLand where ID > 0";

            if (BookingID != "0" || string.IsNullOrEmpty(BookingID))
                sqlQuery += "and BookingID like '%" + BookingID + "%'";
            if (pgReceipt != "0" || string.IsNullOrEmpty(pgReceipt))
                sqlQuery += " and PGReceiptId like '%" + pgReceipt + "%'";
            if (Name != "0" || string.IsNullOrEmpty(Name))
                sqlQuery += " and  [Name] like '%" + Name + "%' ";
            if (BookingDateTo != "0" || string.IsNullOrEmpty(BookingDateTo))
                sqlQuery += " and  Convert(Date,[DateOfBooking]) <= Convert(Date, '" + BookingDateTo + "') ";
            if (BookingDateFrom != "0" || string.IsNullOrEmpty(BookingDateFrom))
                sqlQuery += " and  Convert(Date,[DateOfBooking]) >= Convert(Date, '" + BookingDateFrom + "') ";
            if (paymentStatus == 0 || string.IsNullOrEmpty(Package))    // 0 stands for failure
                sqlQuery += "and PGIsPaymentSuccess = 'false'";
            if (paymentStatus == 1)                                    // 1 stands for success, 2 for both success & failure
                sqlQuery += "and PGIsPaymentSuccess = 'true'";
            //if (Package != "Select" || string.IsNullOrEmpty(Package))
            //    sqlQuery += "and PackageType like '%" + Package + "%'";

            sqlQuery += " ORDER BY BookingID";

            return Connection.readDataSet(sqlQuery, connMSTicket);
        }

        public static DataTable _Select_NewYearTransaction_REFIDWISE(string NYBookingID)
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = "Select * from [dbo].[tbl_NewYearPackages] where [BookingID] = '" + NYBookingID.ToString() + "'";
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Fetch Details of Booking Id from DB.Booking ID where" + NYBookingID + command.CommandText);
            return Connection.readTab(command, connMSTicket);
        }
        public static DataTable _Select_BollyLandTransaction_REFIDWISE(string NYBookingID)
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = "Select * from [dbo].[tbl_BollyLand] where [BookingID] = '" + NYBookingID.ToString() + "'";
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Fetch Details of Booking Id from DB.Booking ID where" + NYBookingID + command.CommandText);
            return Connection.readTab(command, connMSTicket);
        }

        public static DataTable _Select_MMTTransaction_REFIDWISE(string MMTBookingID)
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = "Select * from [dbo].[tbl_mmtpromotion] where [MMTBookingId] = '" + MMTBookingID.ToString() + "'";
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Fetch Details of Booking Id from mmt DB.Booking ID where" + MMTBookingID + command.CommandText);
            return Connection.readTab(command, connMSTicket);
        }
        public static DataTable _Select_MCTransaction_REFIDWISE(string BookingID) // For all Payment Gateways Containing Booking ID as a Parameter
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = "Select * from [dbo].[MCPROMOTIONS_DETAIL] where [BookingId] = '" + BookingID.ToString() + "'";
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Fetch Details of Booking Id from DB.Booking ID where" + BookingID + command.CommandText);
            return Connection.readTab(command, connMSTicket);
        }
        public static DataTable _Select_McTransaction_REFIDWISE(string BookingID) // For Print Receipt Containing Reference Number as a Parameter
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = "Select * from [dbo].[MCPROMOTIONS_DETAIL] where [BookingId] = '" + BookingID.ToString() + "'";
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Fetch Details of Booking Id from DB.Booking ID where" + BookingID + command.CommandText);
            return Connection.readTab(command, connMSTicket);
        }

        public static DataTable _Select_MANATransaction_REFIDWISE(string MANABookingID)
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = "Select * from [dbo].[tbl_manapromotion] where [MANABookingId] = '" + MANABookingID.ToString() + "'";
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Fetch Details of Booking Id from DB.Booking ID where" + MANABookingID + command.CommandText);
            return Connection.readTab(command, connMSTicket);
        }
        public static DataTable _Select_SummerTransaction_REFIDWISE(string SummerBookingID)
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = "Select * from [dbo].[tbl_summercamp] where [BookingId] = '" + SummerBookingID.ToString() + "'";
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Fetch Details of Booking Id from DB.Booking ID where" + SummerBookingID + command.CommandText);
            return Connection.readTab(command, connMSTicket);
        }
        public static DataTable _Select_RoyalCardMcDetail_REFIDWISE(string email, string mno)
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = "Select * from [dbo].[royalcardmcdetails_insert] where [Email] = '" + email.ToString() + "' and [MobileNo]='" + mno + "'";
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Fetch enroolment detail from table where" + mno + email + command.CommandText);
            return Connection.readTab(command, connMSTicket);
        }
        //***************royal card changes for master card offer*********
        public static DataTable _Select_BankName()
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = "select Distinct([Owner ICA Name]) from Table_McBankList where Type='WORLD'";
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Fetch bank name for world card from table where" + command.CommandText);
            return Connection.readTab(command, connMSTicket);
        }
        public static DataTable _Select_BankNamenonwc()
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = "select Distinct([Owner ICA Name]) from Table_McBankList where Type='ROW'";
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Fetch bank name for non world card from table where" + command.CommandText);
            return Connection.readTab(command, connMSTicket);
        }
        public static DataTable _Validation(int cardno)
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = "select ([Owner ICA Name]) from Table_McBankList where Bin=" + cardno + "and Type='WORLD'";
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Fetch bank detail for world card from table where" + command.CommandText);
            return Connection.readTab(command, connMSTicket);
        }
        public static DataTable _Validationnonwc(int cardno)
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = "select ([Owner ICA Name]) from Table_McBankList where Bin=" + cardno + "and Type='ROW'";
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Fetch bank detail for non world card from table where" + command.CommandText);
            return Connection.readTab(command, connMSTicket);
        }
        //*********************************************************************

        internal static void Get_Valentine_Details(string BookingId, string ReceiptNo)
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = "Update [dbo].[tbl_ValentinePackages] set PGIsPaymentSuccess=1 ,[PGReceiptId]='" + ReceiptNo.ToString() + "' where [BookingID] = '" + BookingId.ToString() + "'";
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Update Details of PGIsPaymentSuccess and PGReceiptId from DB where Booking ID:" + BookingId + "Receipt No." + ReceiptNo);
            Connection.EXECommand(command, connMSTicket);
        }
        internal static void Get_Boty_Details(string BookingId, string ReceiptNo)
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = "Update [dbo].[tbl_Boty] set PGIsPaymentSuccess=1 ,[PGReceiptId]='" + ReceiptNo.ToString() + "' where [BookingID] = '" + BookingId.ToString() + "'";
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Update Details of PGIsPaymentSuccess and PGReceiptId from DB where Booking ID:" + BookingId + "Receipt No." + ReceiptNo);
            Connection.EXECommand(command, connMSTicket);
        }
        internal static void Get_Dandiya_Details(string BookingId, string ReceiptNo)
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = "Update [dbo].[tbl_Dandiyanight] set PGIsPaymentSuccess=1 ,[PGReceiptId]='" + ReceiptNo.ToString() + "' where [BookingID] = '" + BookingId.ToString() + "'";
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Update Details of PGIsPaymentSuccess and PGReceiptId from DB where Booking ID:" + BookingId + "Receipt No." + ReceiptNo);
            Connection.EXECommand(command, connMSTicket);
        }


        internal static DataTable Select_ValentineTransaction(string VLBookingID)
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = "Select * from [dbo].[tbl_ValentinePackages] where [BookingID] = '" + VLBookingID.ToString() + "'";
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Fetch Details of Booking Id from DB.Booking ID where" + VLBookingID + command.CommandText);
            return Connection.readTab(command, connMSTicket);
        }
        internal static DataTable Select_BotyTransaction(string VLBookingID)
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = "Select * from [dbo].[tbl_Boty] where [bookingID] = '" + VLBookingID.ToString() + "'";
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Fetch Details of Booking Id from DB.Booking ID where" + VLBookingID + command.CommandText);
            return Connection.readTab(command, connMSTicket);
        }
        internal static DataTable Check_BotyTransaction(string entryid)
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = "Select * from [dbo].[tbl_Boty] where [EnrtyID] = '" + entryid.ToString() + "'";
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Fetch Details of Booking Id from DB.Booking ID where" + entryid + command.CommandText);
            return Connection.readTab(command, connMSTicket);
        }
        internal static DataTable Select_DandiyaTransaction(string VLBookingID)
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = "Select * from [dbo].[tbl_Dandiyanight] where [BookingID] = '" + VLBookingID.ToString() + "'";
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Fetch Details of Booking Id from DB.Booking ID where" + VLBookingID + command.CommandText);
            return Connection.readTab(command, connMSTicket);
        }
        public static DataTable Select_ShowDetails(long transectionCounter)
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = "Select * from [dbo].[GINC$TransactionCounter] where [Counter] = " + transectionCounter;
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Fetch show Details where" + transectionCounter + command.CommandText);
            return Connection.readTab(command, connMSTicket);
        }
        public static DataTable Check_AuditDetails(string ShowName, string ShowLocation, string ShowDate, string ShowTime)
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = "Select * from [dbo].[ShowAudit_Table] where [ShowName] = '" + ShowName + "' and [ShowLocation]='" + ShowLocation + "'and [ShowDate]='" + ShowDate + "'and [ShowTime]='" + ShowTime + "'and [Iscompleted]=1";
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Fetch show Details where" + ShowName + command.CommandText);
            return Connection.readTab(command, connMSTicket);
        }
        public static DataTable Delete_Iscomplete()
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = "Delete from [dbo].[ShowAudit_Table] where [Iscompleted] =0";
            return Connection.readTab(command, connMSTicket);
        }
        //public static DataTable Set_finalstatus(int AuditNo,string ShowName,string ShowLocation,string ShowDate,DateTime ShowTime,int Iscompleted)
        //{
        //    SqlCommand command = new SqlCommand();
        //    command.CommandText = "update [dbo].[ShowAudit_Table] set Iscompleted=1 where [AuditNo]=" + AuditNo + "and [ShowName] = '" + ShowName + "' and [ShowLocation]='" + ShowLocation + "'and [ShowDate]='" + ShowDate + "'and [ShowTime]='" + ShowTime + "'";
        //    Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Fetch show Details where" + ShowName + command.CommandText);
        //    return Connection.readTab(command, connMSTicket);
        //}
        public static DataTable Check_AuditCount(string ShowName, string ShowLocation, string ShowDate, string ShowTime)
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = "Select distinct [AuditNo] from [dbo].[ShowAudit_Table] where [ShowName] = '" + ShowName + "' and [ShowLocation]='" + ShowLocation + "'and [ShowDate]='" + ShowDate + "'and [ShowTime]='" + ShowTime + "'and [Iscompleted]=1";
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Fetch show Details where" + ShowName + command.CommandText);
            return Connection.readTab(command, connMSTicket);
        }
        public static DataTable AuditNumberReport(String ShowDate1, String ShowName, String ShowLocation, int Iscompleted, string ShowTime1)
        {
            string status = "Occupied by Customer";
            SqlCommand command = new SqlCommand();
            command.CommandText = "SELECT sum(case when [ShowDate]='" + ShowDate1 + "' and [AuditNo]=1 and [Iscompleted]=" + Iscompleted + " and Status='" + status + "' and [ShowLocation]='" + ShowLocation + "' and [ShowTime]='" + ShowTime1 + "'then 1 else 0 end) as AuditNo1," +
"sum(case when [ShowDate]='" + ShowDate1 + "' and [AuditNo]=2 and Iscompleted=" + Iscompleted + " and [Status]='" + status + "'and [ShowLocation]='" + ShowLocation + "' then 1 else 0 end) as AuditNo2,[Category] FROM [dbo].[ShowAudit_Table] where [Category] is not null group by Category";
            return Connection.readTab(command, connMSTicket);
        }

        public static DataTable AuditNumber1Report(string ShowTime1, String ShowDate1, String ShowName, String ShowLocation)
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = "SELECT row_number() over (order by [SeatDescription] ) as SerialNo, [SeatDescription],[Status],[Remark],[ERPStatus] FROM [dbo].[ShowAudit_Table] where ([Status]!='Select' or [Remark]!='') and [ShowName]='" + ShowName + "' and [ShowLocation]='" + ShowLocation + "' and [ShowDate]='" + ShowDate1 + "' and [ShowTime]='" + ShowTime1 + "' and [AuditNo]=1";
            return Connection.readTab(command, connMSTicket);
        }
        public static DataTable AuditNumber2Report(string ShowTime1, String ShowDate1, String ShowName, String ShowLocation)
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = "SELECT row_number() over (order by [SeatDescription] ) as SerialNo, [SeatDescription],[Status],[Remark],[ERPStatus] FROM [dbo].[ShowAudit_Table] where ([Status]!='Select' or [Remark]!='') and [ShowName]='" + ShowName + "' and [ShowLocation]='" + ShowLocation + "' and [ShowDate]='" + ShowDate1 + "' and [ShowTime]='" + ShowTime1 + "' and [AuditNo]=2";
            return Connection.readTab(command, connMSTicket);
        }

        public static DataTable Insert_AuditDetails(string SeatID, int AuditNo, string ShowName, string ShowLocation, string ShowDate, string ShowTime, string Status, string Remark, DateTime EditTime, string SeatDescription, int Iscompleted, string Category)
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = "if not Exists (select [SeatID] FROM [MSTicketDB_Live_Latest].[dbo].[ShowAudit_Table] where SeatID='" + SeatID + "')" +
            "begin INSERT INTO [MSTicketDB_Live_Latest].[dbo].[ShowAudit_Table]([SeatID],[AuditNo],[ShowName],[ShowLocation],[ShowDate],[ShowTime],[Status],[Remark],[EditTime],[SeatDescription],[Iscompleted],[Category]) values ('" + SeatID + "'," + AuditNo + ",'" + ShowName + "','" + ShowLocation + "','" + ShowDate + "','" + ShowTime + "','" + Status + "','" + Remark + "','" + EditTime + "','" + SeatDescription + "'," + Iscompleted + ",'" + Category + "')" +
            "end else begin update [MSTicketDB_Live_Latest].[dbo].[ShowAudit_Table] set SeatID='" + SeatID + "',AuditNo= " + AuditNo + " ,ShowName='" + ShowName + "',ShowLocation='" + ShowLocation + "',ShowDate='" + ShowDate + "',ShowTime='" + ShowTime + "',Status='" + Status + "',Remark='" + Remark + "',EditTime='" + EditTime + "',SeatDescription='" + SeatDescription + "',Iscompleted=" + Iscompleted + ",Category='" + Category + "' where SeatID='" + SeatID + "' end";
            //command.CommandText = "insert into [dbo].[ShowAudit_Table]([SeatID],[AuditNo],[ShowName],[ShowLocation],[ShowDate],[ShowTime],[Status],[Remark],[EditTime],[SeatDescription],[Iscompleted]) values('"+SeatID+"','"+AuditNo+"','"+ShowName+"','"+ShowLocation+"','"+ShowDate+"','"+ShowTime+"','"+Status+"','"+Remark+"','"+EditTime+"','"+SeatDescription+"','"+Iscompleted+"')";
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Fetch show Details where" + ShowName + command.CommandText);
            return Connection.readTab(command, connMSTicket);
        }
        public static DataTable Clear_AuditDetails(string SeatID)
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = "Delete from [dbo].[ShowAudit_Table] where [SeatID] ='" + SeatID + "'";
            return Connection.readTab(command, connMSTicket);
        }
        public static DataSet Select_Report_tbl_ValentinePackages(string BookingID, string BookingDateFrom, string BookingDateTo, string Name, string pgReceipt, string Package, int paymentStatus)
        {
            string sqlQuery = "SELECT row_number() over (order by ID desc) as [S.N.],[BookingId] as [Booking ID]," +
                   "[TotalAmount] as Amount,Quantity,Convert(nvarchar,[DateOfBooking],100) as [Date],Package," +
                   "[Name]as Name, [EmailId] as Email, [ContactNumber] as Phone,[PGIsPaymentSuccess] as [Payment Status], PGReceiptId " +
                   "from dbo.tbl_ValentinePackages where ID > 0 and YEAR([DateOfBooking])=2014";
            if (BookingID != "0" || string.IsNullOrEmpty(BookingID))
                sqlQuery += "and BookingID like '%" + BookingID + "%'";
            if (pgReceipt != "0" || string.IsNullOrEmpty(pgReceipt))
                sqlQuery += " and PGReceiptId like '%" + pgReceipt + "%'";
            if (Name != "0" || string.IsNullOrEmpty(Name))
                sqlQuery += " and  [Name] like '%" + Name + "%' ";
            if (BookingDateTo != "0" || string.IsNullOrEmpty(BookingDateTo))
                sqlQuery += " and  Convert(Date,[DateOfBooking]) <= Convert(Date, '" + BookingDateTo + "') ";
            if (BookingDateFrom != "0" || string.IsNullOrEmpty(BookingDateFrom))
                sqlQuery += " and  Convert(Date,[DateOfBooking]) >= Convert(Date, '" + BookingDateFrom + "') ";
            if (paymentStatus == 0 || string.IsNullOrEmpty(Package))    // 0 stands for failure
                sqlQuery += "and PGIsPaymentSuccess = 'false'";
            if (paymentStatus == 1)                                    // 1 stands for success, 2 for both success & failure
                sqlQuery += "and PGIsPaymentSuccess = 'true'";
            if (Package == "Couple Rs.5999")
                sqlQuery += "and Package='5999'";
            if (Package == "Couple Rs.3999")
                sqlQuery += "and Package='3999'";
            if (Package == "Couple Rs.3499")
                sqlQuery += "and Package='3499'";
            sqlQuery += " ORDER BY BookingID";

            return Connection.readDataSet(sqlQuery, connMSTicket);
        }
        public static DataTable _insertdetail(string gender, string fname, string lname, string address, string city, string country, string email, string pin, string mno, string dob, string mstatus, DateTime date, string cardno, string bankname, string cardtype, string anniversary)
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = "declare @err varchar(max) if  not EXISTS (Select * from royalcardmcdetails_insert where MobileNo ='" + mno + "'" + "or Email='" + email + "'" + ")" +
                        " begin INSERT INTO [royalcardmcdetails_insert] ([Gender],[FirstName],[LastName],[Address],[City],[Country],[Email],[Pin],[MobileNo],[DateOfBirth],[MaritalStatus],[Date],[CardNo],[BankName],[CardType],[Marriage Anniversary])VALUES (@gender," +
                        " @fname,@lname,@address,@city,@country,@email,@pin,@mno,@dob,@mstatus,@date,@cardno,@bankname,@cardtype,@anniversary)" + "set @err='Your application for Royal platinum Membership is under process. You shall shortly receive a verification call from our end updating you on your membership status.'" +
                        " end else set @err='Sorry, You are already enrolled for Membership' select @err";
            command.Parameters.AddWithValue("@gender", gender);
            command.Parameters.AddWithValue("@fname", fname);
            command.Parameters.AddWithValue("@lname", lname);
            command.Parameters.AddWithValue("@address", address);
            command.Parameters.AddWithValue("@city", city);
            command.Parameters.AddWithValue("@country", country);
            command.Parameters.AddWithValue("@email", email);
            command.Parameters.AddWithValue("@mno", mno);
            command.Parameters.AddWithValue("@dob", dob);
            command.Parameters.AddWithValue("@mstatus", mstatus);
            command.Parameters.AddWithValue("@date", date);
            command.Parameters.AddWithValue("@cardno", cardno);
            command.Parameters.AddWithValue("@bankname", bankname);
            command.Parameters.AddWithValue("@cardtype", cardtype);
            if (pin == null)
                command.Parameters.AddWithValue("@pin", DBNull.Value);
            else
                command.Parameters.AddWithValue("@pin", pin);
            if (anniversary == null)
                command.Parameters.AddWithValue("@anniversary", DBNull.Value);
            else
                command.Parameters.AddWithValue("@anniversary", anniversary);
            DataTable dt = Connection.readTab(command, connMSTicket);
            return dt;
        }

        public static string Successful_BookingByMsTicket(decimal Amount, string Play, string CustomerNo, string referenceID, int TotalSeats, string Gateway, string AgentCode, decimal discountPercent, string ReceiptNo, decimal RoyalAmount, decimal RoyalPoints, string BookingID, string AgentCodeSubcode, string WebPromotionID)
        {
            try
            {
                //Connection.LogEntry(TransID);

                SqlCommand cmd = new SqlCommand("Proc_SeatBookedByMsTicket", connWebBooking);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter Ply = cmd.Parameters.Add("@Play", SqlDbType.VarChar);
                Ply.Value = Play;

                SqlParameter ReferenceID = cmd.Parameters.Add("@ReferenceNO", SqlDbType.NVarChar);
                ReferenceID.Value = referenceID;

                SqlParameter Receipt = cmd.Parameters.Add("@ReceiptNo", SqlDbType.NVarChar);
                Receipt.Value = ReceiptNo;

                SqlParameter TransAMT = cmd.Parameters.Add("@TotalAmount", SqlDbType.Decimal);
                TransAMT.Value = Amount;

                SqlParameter MobileNo = cmd.Parameters.Add("@CustomerNo", SqlDbType.NVarChar);
                MobileNo.Value = CustomerNo;

                SqlParameter Seats = cmd.Parameters.Add("@NoOfTickets", SqlDbType.Int);
                Seats.Value = TotalSeats;

                SqlParameter PG = cmd.Parameters.Add("@PaymentGateway", SqlDbType.NVarChar);
                PG.Value = Gateway;

                SqlParameter Agent = cmd.Parameters.Add("@AgentCode", SqlDbType.NVarChar);
                Agent.Value = AgentCode;

                SqlParameter Booking = cmd.Parameters.Add("@WEBBookingID", SqlDbType.NVarChar);
                Booking.Value = BookingID;

                SqlParameter Agentsubcode = cmd.Parameters.Add("@AgentCodeSubcode", SqlDbType.NVarChar);
                Agentsubcode.Value = AgentCodeSubcode;

                SqlParameter webpromotion = cmd.Parameters.Add("@WebPromotionID", SqlDbType.NVarChar);
                webpromotion.Value = WebPromotionID;

                SqlParameter RedeemAmt = cmd.Parameters.Add("@Amount", SqlDbType.Decimal);
                RedeemAmt.Value = RoyalAmount;

                SqlParameter RedeemPnts = cmd.Parameters.Add("@Points", SqlDbType.Decimal);
                RedeemPnts.Value = RoyalPoints;

                SqlParameter discountPercentage = cmd.Parameters.Add("@DiscountedPercent", SqlDbType.Decimal);
                discountPercentage.Value = discountPercent;

                cmd.Connection.Open();
                cmd.CommandTimeout = 180;
                //cmd.ExecuteNonQuery();
                SqlDataReader dr = cmd.ExecuteReader();
                string resultout = string.Empty;
                if (dr.Read())
                {
                    resultout = dr["Message"].ToString();
                }
                cmd.Connection.Close();
                return resultout;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (connWebBooking.State != ConnectionState.Closed)
                {
                    connWebBooking.Close();
                }
            }
        }
        public static DataTable selectifo_royal(string info)
        {
            try
            {
                //Connection.LogEntry(TransID);

                SqlCommand cmd = new SqlCommand("Select_Royaelinfo", connWebBooking);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter Info = cmd.Parameters.Add("@info", SqlDbType.VarChar);
                Info.Value = info;

                cmd.Connection.Open();
                cmd.CommandTimeout = 180;
                SqlDataReader dr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                cmd.Connection.Close();
                return dt;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (connWebBooking.State != ConnectionState.Closed)
                {
                    connWebBooking.Close();
                }
            }
        }

        public static DataSet Select_Report_tbl_Digitalkasos(string DateFrom, string DateTo)
        {
            string sqlQuery = "SELECT * from [dbo].[Royal_MailerTracker] where [WebID] is not null";
            if (DateTo != "0" || string.IsNullOrEmpty(DateTo))
                sqlQuery += " and  Convert(Date,[Date]) <= Convert(Date, '" + DateTo + "') ";
            if (DateFrom != "0" || string.IsNullOrEmpty(DateFrom))
                sqlQuery += " and  Convert(Date,[Date]) >= Convert(Date, '" + DateFrom + "') ";
            sqlQuery += " order by id desc";
            return Connection.readDataSet(sqlQuery, connMSTicket);
        }
    }
}
