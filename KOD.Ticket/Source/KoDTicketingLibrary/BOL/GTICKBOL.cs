using System;
using System.Data;
using KoDTicketing.DataAccessLayer;

namespace KoDTicketing.BusinessLayer
{
    /// <summary>
    /// Summary description for GTICKBOL
    /// </summary>-
    public class GTICKBOL
    {
        public static DataTable MaxColumn(String filmCode)
        {
            return GTICKDAL._Select_MaxColumn(filmCode);
        }
        public static DataTable AllSeats(String _AudiNo)
        {
            return GTICKDAL._Select_AllSeats(_AudiNo);
        }
        public static DataTable Audit_AllSeats(String _AudiNo)
        {
            return GTICKDAL._Audit_Select_AllSeats(_AudiNo);
        }
        public static DataTable AuditSeatsReport(String _AudiNo, String _ShowDate)
        {
            return GTICKDAL._Select_AuditSeatsReport(_AudiNo, _ShowDate);
        }

        public static DataTable SelectRow_AudiWise(String _filmCode)
        {
            return GTICKDAL._SelectRow_AudiWise(_filmCode);
        }
        public static DataTable select_Seat_Layout(String _AudiNo)
        {
            return GTICKDAL._select_Seat_Layout(_AudiNo);
        }
        public static long Insert_tempTransaction_Table(TransactionRecord _tr)
        {
            return GTICKDAL._Insert_tempTransaction_Table(_tr.KeyNo, _tr.BookingID, _tr.TotalSeats, _tr.Category, _tr.SeatInfo, _tr.ShowDate, _tr.ShowTime);
        }
        public static DataTable SelectTempSessionTable_one(long _TransactionID)
        {
            return GTICKDAL._SelectTempSessionTable_one(_TransactionID);
        }
        public static int ON_Session_out(String _KeyNo)
        {
            return GTICKDAL._ON_Session_out(_KeyNo);
        }
        public static int Check_Seats_BeforeProceed(TransactionRecord _tr)
        {
            return int.Parse(GTICKDAL._Check_Seats_BeforeProceed(_tr.BookingID, _tr.SeatInfo, _tr.Play).Rows[0][0].ToString());
        }
        public static DataTable Select_NewYearSeat(string filmcode, long transectioncounter, int no)
        {
            return GTICKDAL._Select_NewYearSeat(filmcode, transectioncounter, no);
        }
        public static decimal Get_SeatPrice_SeatKeyNoWise(long _TransactionID)
        {
            return decimal.Parse(GTICKDAL._Get_SeatPrice_SeatKeyNoWise(_TransactionID).Rows[0][0].ToString());
        }

        public static DataTable Get_AllSeatPrice_SeatKeyNoWise(long TransactionID)
        {
            return GTICKDAL._Get_AllSeatPrice_SeatKeyNoWise(TransactionID);
        }

        #region -- Transaction ID Counter
        public static long TransactionCounter_Max()
        {
            return GTICKDAL._TransactionCounter_Max();
        }

        #endregion

        #region -- March Promotion Transaction ID Counter
        public static long MarchPromotionTransactionCounter_Max(long ID)
        {
            return GTICKDAL._MarchPromotionTransactionCounter_Max(ID);
        }

        #endregion

        #region API_Security
        public static bool ValidateAgent(string username, string password)
        {
            return GTICKDAL.ValidateAgent(username, password);
        }
        #endregion


        public static string NewYearBooking_Max()
        {
            string ID = GTICKDAL.NewYearBooking_Max();
            if (ID == null || ID.ToString() == "")
            {
                return ("KODNY00000");
            }
            else
            {
                return ID;
            }

        }
        public static string BollyLand_Max()
        {
            string ID = GTICKDAL.BollyLand_Max();
            if (ID == null || ID.ToString() == "")
            {
                return ("KOD/BL00000");
            }
            else
            {
                return ID;
            }

        }
        public static string MMTBooking_Max()
        {
            string ID = GTICKDAL.MMTBooking_Max();
            if (ID == null || ID.ToString() == "")
            {
                return ("KODMMT00000");
            }
            else
            {
                return ID;
            }

        }
        public static string SummerBooking_Max()
        {
            string ID = GTICKDAL.SummerBooking_Max();
            if (ID == null || ID.ToString() == "")
            {
                return ("KODSUM00000");
            }
            else
            {
                return ID;
            }

        }


        public static void NewYearBooking_Details(short CouplePackage, short SinglePackage, short TeensPackage, short KidsPackage, decimal TotalAmount, DateTime DateofBooking, string BookingID, string Name, string Email, string ContactNo, bool Status, string ReceiptNo, string royelinfo)
        {
            GTICKDAL.NewYearBooking_Details(CouplePackage, SinglePackage, TeensPackage, KidsPackage, TotalAmount, DateofBooking, BookingID, Name, Email, ContactNo, Status, ReceiptNo, royelinfo);
        }
        public static void BollylandBooking_Details(short GoldPackage, short SilverPackage, decimal TotalAmount, DateTime DateofBooking, string BookingID, string Name, string Email, string ContactNo, bool Status, string ReceiptNo)
        {
            GTICKDAL.BollylandBooking_Details(GoldPackage, SilverPackage, TotalAmount, DateofBooking, BookingID, Name, Email, ContactNo, Status, ReceiptNo);
        }
        public static long MMTBooking_Details(short noofpackage, string pnrno, string promocode, decimal totalamount, decimal paybleamount, DateTime dateofbooking, string bookingid, DateTime day, string Name, string Email, string ContactNo, string paymentgateway, bool Status, string ReceiptNo, string MMTbookingid)
        {
            return GTICKDAL.MMTBooking_Details(noofpackage, pnrno, promocode, totalamount, paybleamount, dateofbooking, bookingid, day, Name, Email, ContactNo, paymentgateway, Status, ReceiptNo, MMTbookingid);
        }
        public static long YATRABooking_Details(short noofticket, string pnrno, string promocode, string cat, decimal DiscountedPercentage, decimal totalamount, decimal paybleamount, DateTime dateofbooking, string bookingid, DateTime day, string Name, string Email, string ContactNo, string paymentgateway, bool Status, string ReceiptNo, string transid)
        {
            return GTICKDAL.YATRABooking_Details(noofticket, pnrno, promocode, cat, DiscountedPercentage, totalamount, paybleamount, dateofbooking, bookingid, day, Name, Email, ContactNo, paymentgateway, Status, ReceiptNo, transid);
        }
        public static long MANABooking_Details(short noofpackage, decimal totalamount, decimal paybleamount, DateTime dateofbooking, string bookingid, DateTime day, string Name, string Email, string ContactNo, string paymentgateway, bool Status, string ReceiptNo, string PackageType, string MANAbookingid)
        {
            return GTICKDAL.MANABooking_Details(noofpackage, totalamount, paybleamount, dateofbooking, bookingid, day, Name, Email, ContactNo, paymentgateway, Status, ReceiptNo, PackageType, MANAbookingid);
        }
        public static long FAMILYOFFERBooking_Details(short noofpackage, decimal totalamount, decimal paybleamount, DateTime dateofbooking, string bookingid, DateTime day, string Name, string Email, string ContactNo, string paymentgateway, bool Status, string ReceiptNo, string PackageType, string FAMILYOFFERbookingid, string royalcardno, decimal pcktotalamount, decimal ticketpaybaleamount)
        {
            return GTICKDAL.FAMILYOFFERBooking_Details(noofpackage, totalamount, paybleamount, dateofbooking, bookingid, day, Name, Email, ContactNo, paymentgateway, Status, ReceiptNo, PackageType, FAMILYOFFERbookingid, royalcardno, pcktotalamount, ticketpaybaleamount);
        }
        public static void McPromotionBooking_Details(short Nooftickets, short noofpackage, decimal totalamount, decimal paybleamount, DateTime dateofbooking, string bookingid, DateTime showdate, string Name, string Email, string ContactNo, string paymentgateway, bool Status, string ReceiptNo, string Type, string PromotionCode, string bank, string card, string promocode)
        {
            GTICKDAL.McPromotionBooking_Details(Nooftickets, noofpackage, totalamount, paybleamount, dateofbooking, bookingid, showdate, Name, Email, ContactNo, paymentgateway, Status, ReceiptNo, Type, PromotionCode, bank, card, promocode);
        }
        public static long InsertAgentBooking_Details(TransactionRecord tr)
        {
            return GTICKDAL.InsertAgentBooking_Details(tr);
        }
        public static void SummerBooking_Details(short nooftickets, decimal totalamount, decimal paybleamount, DateTime dateofbooking, string bookingid, string Name, string Email, string ContactNo, string paymentgateway, bool Status, string ReceiptNo)
        {
            GTICKDAL.SummerBooking_Details(nooftickets, totalamount, paybleamount, dateofbooking, bookingid, Name, Email, ContactNo, paymentgateway, Status, ReceiptNo);
        }
        public static string MMTpackage_check(string pnrno)
        {
            string sum = GTICKDAL.MMTpackage_check(pnrno);
            if (sum == null || sum.ToString() == "")
            {
                return ("0");
            }
            else
            {
                return sum;
            }
        }
        public static string YATRAPromotion_check(string pnrno)
        {
            string sum = GTICKDAL.YATRAPromotion_check(pnrno);
            if (sum == null || sum.ToString() == "")
            {
                return ("0");
            }
            else
            {
                return sum;
            }
        }
        public static string ValentineBooking_Max()
        {
            string ID = GTICKDAL.ValentineBooking_Max();
            if (ID == null || ID.ToString() == "")
            {
                return ("KODVL00000");
            }
            else
            {
                return ID;
            }
        }
        public static string BotyBooking_Max()
        {
            string ID = GTICKDAL.BotyBooking_Max();
            if (ID == null || ID.ToString() == "")
            {
                return ("BOTY00000");
            }
            else
            {
                return ID;
            }
        }
        public static string EventBooking_Max()
        {
            string ID = GTICKDAL.EventBooking_Max();
            if (ID == null || ID.ToString() == "")
            {
                return ("EVENT00000");
            }
            else
            {
                return ID;
            }
        }
        public static string DandiyaBooking_Max()
        {
            string ID = GTICKDAL.DandiyaBooking_Max();
            if (ID == null || ID.ToString() == "")
            {
                return ("KODVL00000");
            }
            else
            {
                return ID;
            }
        }

        //public static void ValentineBooking_Details(short Package1, short Package2, short Package3, decimal TotalAmount, DateTime DateofBooking, string BookingID, string Name, string Email, string Contact, bool Status, string ReceiptNo)
        //{
        //    GTICKDAL.ValentineBooking_Details(Package1, Package2, Package3, TotalAmount, DateofBooking, BookingID, Name, Email, Contact, Status, ReceiptNo);
        //}


        public static void ValentineBooking_Details(string amt, int Quantity, decimal TotalAmount, DateTime DateofBooking, string BookingID, string Name, string Email, string Contact, bool Status, string ReceiptNo, string ip, string campaignid, string mailer)
        {
            GTICKDAL.ValentineBooking_Details(amt, Quantity, TotalAmount, DateofBooking, BookingID, Name, Email, Contact, Status, ReceiptNo, ip, campaignid, mailer);
        }
        public static int BotyBooking_Details(string formid, string entryid, decimal TotalAmount, DateTime DateofBooking, string BookingID, bool Status, string ReceiptNo)
        {
            return GTICKDAL.BotyBooking_Details(formid, entryid, TotalAmount, DateofBooking, BookingID, Status, ReceiptNo);
        }
        public static int EventBooking_Details(string BookingID, string Name, string Email, string Contact_No, string Event_Name, string Url , decimal TotalAmount, DateTime DateofBooking, bool Status, string ReceiptNo, string KODBookingID)
        {
            return GTICKDAL.EventBooking_Details(BookingID, Name, Email, Contact_No, Event_Name, Url, TotalAmount, DateofBooking, Status, ReceiptNo, KODBookingID);
        }
        public static void DandiyaBooking_Details(string amt, int Quantity, decimal TotalAmount, DateTime DateofBooking, string BookingID, string Name, string Email, string Contact, bool Status, string ReceiptNo, string type, string eventdate)
        {
            GTICKDAL.DandiyaBooking_Details(amt, Quantity, TotalAmount, DateofBooking, BookingID, Name, Email, Contact, Status, ReceiptNo, type, eventdate);
        }
        public static void March_Promotion(string Category, long BookingID, string DateOfBooking, int TotalSeats, decimal TotalAmount, string Package, int issuccess)
        {
            GTICKDAL.March_Promotion(Category, BookingID, DateOfBooking, TotalSeats, TotalAmount, Package, issuccess);
        }
        public static void Insert_ShowDetail(string session_val, long TransectionCounter)
        {
            GTICKDAL.Insert_ShowDetail(session_val, TransectionCounter);
        }
        public static void Insert_SeatInfo(string seatinfo, long TransectionCounter)
        {
            GTICKDAL.Insert_SeatInfo(seatinfo, TransectionCounter);
        }
        public static void Update_AgentBooking(long BookingID)
        {
            GTICKDAL.Update_AgentBooking(BookingID);
        }
        public static void Update_AgentLogin(string username, string pwd, string ip)
        {
            GTICKDAL.Update_AgentLogin(username, pwd, ip);
        }
        public static int Set_finalstatus(int AuditNo, string ShowName, string ShowLocation, string ShowDate, string ShowTime, int Iscompleted)
        {
            return GTICKDAL.Set_finalstatus(AuditNo, ShowName, ShowLocation, ShowDate, ShowTime, Iscompleted);
        }
        public static int Delete_audit(string check)
        {
            return GTICKDAL.Delete_audit(check);
        }
        public static int Update_audit(DataTable dt_insertvaccant)
        {
            return GTICKDAL.Update_audit(dt_insertvaccant);
        }
        public static int Insert_Payment_DB(string error, string bookingid, string pgname)
        {
            return GTICKDAL.Insert_Payment_DB(error, bookingid, pgname);
        }
    }

}