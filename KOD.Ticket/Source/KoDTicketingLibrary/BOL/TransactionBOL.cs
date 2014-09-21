using System.Data;
using KoDTicketing.DataAccessLayer;
using System;

namespace KoDTicketing.BusinessLayer
{
    public class TransactionBOL
    {
        public static void Update_PaymentStatus(TransactionRecord tr)
        {
            TransactionDAL.Update_PaymentStatus(tr);
        }
        public static int Transaction_Temp_Insert(TransactionRecord tr)
        {
            return TransactionDAL._Transaction_Temp_Insert(tr);
        }
        public static DataTable Select_Temptransaction_REFIDWISE(long BookingID)
        {
            return TransactionDAL._Select_Temptransaction_REFIDWISE(BookingID);
        }

        public static DataSet Settle_Transaction_Details(TransactionRecord tr)
        {
            return TransactionDAL._Settle_Transaction_Details(tr);
        }

        public static DataTable Get_Transaction_Detail(TransactionRecord tr)
        {
            return TransactionDAL._Get_Transaction_Detail(tr);
        }
        public static DataTable Select_Temptransaction_transactionIDWise(long BookingID)
        {
            return TransactionDAL._Select_Temptransaction_transactionIDWise(BookingID);
        }

        //For March Promotion
        public static DataTable Select_MarchPromotionTransactionCounter_CounterIDWise(long BookingID)
        {
            return TransactionDAL._Select_MarchPromotionTransactionCounter_CounterIDWise(BookingID);
        }

        public static DataTable Select_MarchPromotionTransactionCounter_IDWise(long BookingID)
        {
            return TransactionDAL.Select_MarchPromotionTransactionCounter_IDWise(BookingID);
        }

        public static DataSet Select_Report_FromTransactionTable(TransactionRecord tr)
        {
            return TransactionDAL._Select_Report_FromTransactionTable(tr);
        }
        public static DataTable Select_Report_SearchFromTransactions(TransactionRecord tr)
        {
            return TransactionDAL._Select_Report_SearchFromTransactions(tr);
        }
        public static DataSet Select_Report_SearchFromTransactions_DS(long _BookingID, string _ReceiptNo, string _DateOfBooking, string _Location, string _ShowDate, string _MobileNo, string _Name, string _AgentCode)
        {
            return TransactionDAL._Select_Report_SearchFromTransactions_DS(_BookingID, _ReceiptNo, _DateOfBooking, _Location, _ShowDate, _MobileNo, _Name, _AgentCode);
        }
        public static DataSet Select_Report_SearchFromTransactionsTemp_DS(TransactionRecord tr)
        {
            return TransactionDAL._Select_Report_SearchFromTransactionsTemp_DS(tr);
        }
        public static DataTable get_LogDetails_From_Booking_Status(long _BookingID)
        {
            return TransactionDAL._get_LogDetails_From_Booking_Status(_BookingID);
        }
        public static DataTable get_LogDetails_StatusWise(long _BookingID)
        {
            return TransactionDAL._get_LogDetails_StatusWise(_BookingID);
        }
        public static DataSet get_ALL_LogDetails(long _BookingID, string _ReceiptNo, string _DateOfBooking, string _Location, string _ShowDate, string _MobileNo, string _Name, string _AgentCode, string _PaymentType)
        {
            return TransactionDAL._get_ALL_LogDetails(_BookingID, _ReceiptNo, _DateOfBooking, _Location, _ShowDate, _MobileNo, _Name, _AgentCode, _PaymentType);
        }
        public static int _Voucher_Varification_Update(TransactionRecord r)
        {
            return _Voucher_Varification_Update(r);

        }
        #region Valentine
        //public static DataSet InsertValentineData(TransactionRecord tr)
        //{
        //    return TransactionDAL.InsertValentineData(tr);
        //}
        //public static DataSet UpdateValentineData(long BookingID, int ReceiptNo)
        //{
        //    return UpdateValentineData(BookingID, ReceiptNo);
        //}
        //public static DataSet UpdateValentineData_Tracker(long BookingID)
        //{
        //    return UpdateValentineData_Tracker(BookingID);
        //}
        //public static DataSet SelectValentineData(long BookingID)
        //{
        //    return SelectValentineData(BookingID);
        //}
        //public static DataSet SelectValentineData_Tracker(long BookingID, string Name, string BookingType)
        //{
        //    return SelectValentineData_Tracker( BookingID,  Name, BookingType);
        //}
        #endregion



        public static void Redeem_Points(string RegID, decimal RedeemAmount, decimal RedeemPoints, decimal TotalAmount, string Play, string CustomerNo, string ReferenceNO, int NoOfTickets)
        {
            TransactionDAL.Redeem_Points(RegID, RedeemAmount, RedeemPoints, TotalAmount, Play, CustomerNo, ReferenceNO, NoOfTickets);
        }

        public static DataSet Detailed_Report(DateTime startDateOfBooking, DateTime EndDateOfBooking, string AgentCode, string Play)
        {
            return TransactionDAL.Detailed_Report(startDateOfBooking, EndDateOfBooking, AgentCode, Play);
        }

        public static DataSet Hotel_Report(string startDateOfBooking, string EndDateOfBooking, string HotelName)
        {
            return TransactionDAL.Hotel_Report(startDateOfBooking, EndDateOfBooking, HotelName);
        }

        public static string Card_Transaction(string regid, decimal transactionAmt, System.DateTime Date, string ReceiptNo, int TransType)
        {
            return TransactionDAL.Card_Transaction(regid, transactionAmt, Date, ReceiptNo, TransType);
        }

        public static void Top_UP(string TransID)
        {
            TransactionDAL.Top_Up(TransID);
        }

        public static DataSet Select_Report_tbl_NewYearPackages(string BookingID, string DateOfBookingFrom, string DateOfBookingTo, string Name, string pgReceipt, string Package, int paymentStatus)
        {
            return TransactionDAL.Select_Report_tbl_NewYearPackages(BookingID, DateOfBookingFrom, DateOfBookingTo, Name, pgReceipt, Package, paymentStatus);
        }
        public static DataSet Select_Report_tbl_Digitalkasos(string DateFrom, string DateTo)
        {
            return TransactionDAL.Select_Report_tbl_Digitalkasos(DateFrom, DateTo);
        }
        public static DataSet Select_Report_tbl_BollyLand(string BookingID, string DateOfBookingFrom, string DateOfBookingTo, string Name, string pgReceipt, string Package, int paymentStatus)
        {
            return TransactionDAL.Select_Report_tbl_BollyLand(BookingID, DateOfBookingFrom, DateOfBookingTo, Name, pgReceipt, Package, paymentStatus);
        }

        public static void Get_NewYear_Detail(string BookingId, string ReceiptNo)
        {
            TransactionDAL.Get_NewYear_Detail(BookingId, ReceiptNo);
        }
        public static void Get_BollyLand_Detail(string BookingId, string ReceiptNo)
        {
            TransactionDAL.Get_BollyLand_Detail(BookingId, ReceiptNo);
        }
        public static void Get_MMT_Detail(string BookingId, string ReceiptNo)
        {
            TransactionDAL.Get_MMT_Detail(BookingId, ReceiptNo);
        }
        public static void Get_Summer_Detail(string BookingId, string ReceiptNo)
        {
            TransactionDAL.Get_Summer_Detail(BookingId, ReceiptNo);
        }

        public static DataTable Select_NewYearTransaction_REFIDWISE(string NYBookingID)
        {
            return TransactionDAL._Select_NewYearTransaction_REFIDWISE(NYBookingID);
        }
        public static DataTable Select_BollyLandTransaction_REFIDWISE(string NYBookingID)
        {
            return TransactionDAL._Select_BollyLandTransaction_REFIDWISE(NYBookingID);
        }

        public static DataTable Select_MMTTransaction_REFIDWISE(string MMTBookingID)
        {
            return TransactionDAL._Select_MMTTransaction_REFIDWISE(MMTBookingID);
        }
        public static DataTable Select_MCTransaction_REFIDWISE(string BookingID) // For all Payment Gateways Containing Booking ID as a Parameter
        {
            return TransactionDAL._Select_MCTransaction_REFIDWISE(BookingID);
        }
        public static DataTable Select_McTransaction_REFIDWISE(string BookingID) // For Print Receipt Containing Reference Number as a Parameter
        {
            return TransactionDAL._Select_McTransaction_REFIDWISE(BookingID);
        }
        public static DataTable Select_MANATransaction_REFIDWISE(string MANABookingID)
        {
            return TransactionDAL._Select_MANATransaction_REFIDWISE(MANABookingID);
        }
        public static DataTable Select_FAMILYOFFERTransaction_REFIDWISE(string FAMILYOFFERBookingID)
        {
            return TransactionDAL._Select_FAMILYOFFERTransaction_REFIDWISE(FAMILYOFFERBookingID);
        }
        public static DataTable Select_SummerTransaction_REFIDWISE(string SummerBookingID)
        {
            return TransactionDAL._Select_SummerTransaction_REFIDWISE(SummerBookingID);
        }
        //***************royal card changes for master card offer*********
        public static DataTable Select_BankName()
        {
            return TransactionDAL._Select_BankName();
        }
        public static DataTable Select_BankNamenonwc()
        {
            return TransactionDAL._Select_BankNamenonwc();
        }
        public static DataTable Validation(int cardno)
        {
            return TransactionDAL._Validation(cardno);
        }
        public static DataTable Validationnonwc(int cardno)
        {
            return TransactionDAL._Validationnonwc(cardno);
        }
        //*********************************************************************



        public static void Get_Valentine_Details(string BookingId, string ReceiptNo)
        {
            TransactionDAL.Get_Valentine_Details(BookingId, ReceiptNo);
        }
        public static void Get_Boty_Details(string BookingId, string ReceiptNo)
        {
            TransactionDAL.Get_Boty_Details(BookingId, ReceiptNo);
        }
        public static void Get_Event_Details(string BookingId, string ReceiptNo)
        {
            TransactionDAL.Get_Event_Details(BookingId, ReceiptNo);
        }
        public static void Get_Dandiya_Details(string BookingId, string ReceiptNo)
        {
            TransactionDAL.Get_Dandiya_Details(BookingId, ReceiptNo);
        }

        public static DataTable Select_ValentineTransaction(string VLBookingID)
        {
            return TransactionDAL.Select_ValentineTransaction(VLBookingID);
        }
        public static DataTable Select_BotyTransaction(string VLBookingID)
        {
            return TransactionDAL.Select_BotyTransaction(VLBookingID);
        }
        public static DataTable Select_EventTransaction(string EvtBookingID)
        {
            return TransactionDAL.Select_EventTransaction(EvtBookingID);
        }
        public static DataTable Check_BotyTransaction(string entryid)
        {
            return TransactionDAL.Check_BotyTransaction(entryid);
        }
        public static DataTable Check_EventTransaction(string BookingID)
        {
            return TransactionDAL.Check_EventTransaction(BookingID);
        }
        public static DataTable Select_DandiyaTransaction(string VLBookingID)
        {
            return TransactionDAL.Select_DandiyaTransaction(VLBookingID);
        }

        public static DataSet Select_Report_tbl_ValentinePackages(string BookingID, string DateOfBookingFrom, string DateOfBookingTo, string Name, string pgReceipt, string Package, int paymentStatus)
        {
            return TransactionDAL.Select_Report_tbl_ValentinePackages(BookingID, DateOfBookingFrom, DateOfBookingTo, Name, pgReceipt, Package, paymentStatus);
        }

        public static void Transaction_Temp_Insert_PaymentId(long BookingID, string strPmtId)
        {
            TransactionDAL.Transaction_Temp_Insert_PaymentId(BookingID, strPmtId);
        }
        public static DataTable insertdetail(string gender, string fname, string lname, string address, string city, string country, string email, string pin, string mno, string dob, string mstatus, DateTime date, string cardno, string bankname, string cardtype, string anniversary)
        {
            return TransactionDAL._insertdetail(gender, fname, lname, address, city, country, email, pin, mno, dob, mstatus, date, cardno, bankname, cardtype, anniversary);
        }
        public static DataTable Select_RoyalCardMcDetail_REFIDWISE(string email, string mno)
        {
            return TransactionDAL._Select_RoyalCardMcDetail_REFIDWISE(email, mno);
        }

        public static string Successful_BookingByMsTicket(decimal Amount, string Play, string CustomerNo, string referenceID, int TotalSeats, string Gateway, string AgentCode, decimal discountPercent, string ReceiptNo, decimal RoyalAmount, decimal RoyalPoints, string BookingID, string AgentCodeSubcode, string WebPromotionID)
        {
            return TransactionDAL.Successful_BookingByMsTicket(Amount, Play, CustomerNo, referenceID, TotalSeats, Gateway, AgentCode, discountPercent, ReceiptNo, RoyalAmount, RoyalPoints, BookingID, AgentCodeSubcode, WebPromotionID);
        }
        public static DataTable Select_ShowDetails(long TranesctionCounter)
        {
            return TransactionDAL.Select_ShowDetails(TranesctionCounter);
        }
        public static DataTable Delete_Iscomplete()
        {
            return TransactionDAL.Delete_Iscomplete();
        }
        public static DataTable Check_AuditDetails(string ShowName, string ShowLocation, string ShowDate, string ShowTime)
        {
            return TransactionDAL.Check_AuditDetails(ShowName, ShowLocation, ShowDate, ShowTime);
        }
        //public static DataTable Set_finalstatus(int AuditNo, string ShowName, string ShowLocation, string ShowDate, DateTime ShowTime, int Iscompleted)
        //{
        //    return TransactionDAL.Set_finalstatus(AuditNo, ShowName, ShowLocation, ShowDate, ShowTime, Iscompleted);
        //}
        public static DataTable Check_AuditCount(string ShowName, string ShowLocation, string ShowDate, string ShowTime)
        {
            return TransactionDAL.Check_AuditCount(ShowName, ShowLocation, ShowDate, ShowTime);
        }
        public static DataTable AuditNumberReport(string ShowDate1, String ShowName, String ShowLocation, int Iscompleted, string ShowTime1)
        {
            return TransactionDAL.AuditNumberReport(ShowDate1, ShowName, ShowLocation, Iscompleted, ShowTime1);
        }
        public static DataTable AuditNumber1Report(string ShowTime1, String ShowDate1, String ShowName, String ShowLocation)
        {
            return TransactionDAL.AuditNumber1Report(ShowTime1, ShowDate1, ShowName, ShowLocation);
        }
        public static DataTable AuditNumber2Report(string ShowTime1, String ShowDate1, String ShowName, String ShowLocation)
        {
            return TransactionDAL.AuditNumber2Report(ShowTime1, ShowDate1, ShowName, ShowLocation);
        }
        public static DataTable Insert_AuditDetails(string SeatID, int AuditNo, string ShowName, string ShowLocation, string ShowDate, string ShowTime, string Status, string Remark, DateTime EditTime, string SeatDescription, int Iscompleted, string Category)
        {
            return TransactionDAL.Insert_AuditDetails(SeatID, AuditNo, ShowName, ShowLocation, ShowDate, ShowTime, Status, Remark, EditTime, SeatDescription, Iscompleted, Category);
        }
        public static DataTable Clear_AuditDetails(string SeatID)
        {
            return TransactionDAL.Clear_AuditDetails(SeatID);
        }
        public static DataTable selectifo_royal(string info)
        {
            return TransactionDAL.selectifo_royal(info);
        }
    }

}
