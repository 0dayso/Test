using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Configuration;
using KoDTicketing;
using KoDTicketing.BusinessLayer;

namespace KoDTicketing.DataAccessLayer
{
    /// <summary>
    /// Summary description for GTICKDAL
    /// </summary>
    public class GTICKDAL : DBAccess
    {
        private GTICKDAL()
        {
        }

        public static DataTable _Select_MaxColumn(string filmCode)
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = "SELECT [Store No_],[Audi No_]  ,[Description]  ,[Max Rows] as MaxRow  ,[Max Columns] as MaxColumn  ,[Status]  FROM " + DBAccess.table_AudiMaster + "  Where " +
            " [Audi No_]= '" + filmCode + "'";
            return Connection.readTab(command, connWebBooking);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="AudiNo"></param>
        /// <returns></returns>
        public static DataTable _Select_AllSeats(string AudiNo)
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = "SELECT b.[Booking ID],b.[Show Allocation ID],b.[KeyNo],b.[Cell Type],b.[Row No],b.[Column No],b.[Class Code]," +
                " b.[Description],b.[Show Price] ,b.[Booked],b.[Blocked],b.[Lock For Booking],b.[Block For Agent],a.Description" +
                " from " + table_BookingMaster + " b left join " + table_ClassMaster + " a" +
                " on b.[Class Code]=a.[Class Code] where  b.[Show Allocation ID]= '" + AudiNo + "' order by [Booking ID]";
            return Connection.readTab(command, connWebBooking);
        }
        public static DataTable _Audit_Select_AllSeats(string AudiNo)
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = "SELECT b.[Booking ID],b.[Show Allocation ID],b.[KeyNo],b.[Cell Type],b.[Row No],b.[Column No],b.[Class Code]," +
                " b.[Description],b.[Show Price] ,b.[Booked],b.[Blocked],b.[Lock For Booking],b.[Block For Agent],a.Description,b.[Tele Booking ID],b.[Paid Tel Booking]" +
                " from " + table_BookingMaster + " b left join " + table_ClassMaster + " a" +
                " on b.[Class Code]=a.[Class Code] where  b.[Show Allocation ID]= '" + AudiNo + "' order by [Booking ID]";
            return Connection.readTab(command, connWebBooking);
        }
        public static DataTable _Select_AuditSeatsReport(string AudiNo,string ShowDate)
        {
            SqlCommand command = new SqlCommand();
            //command.CommandText = "select a.Seats,a.Description,b.Booked as 'Booked Seats' from(SELECT COUNT (*) as Seats,b.[Description]" +
            //    " from " + table_BookingMaster + " as  a join " + table_ClassMaster + " b" +
            //    " on a.[Class Code]=b.[Class Code] where  a.[Show Allocation ID]= '" + AudiNo + "' and a.[Date]='" + ShowDate + "' group by a.[Class Code],b.[Description])a FULL OUTER JOIN(SELECT COUNT (*) as Booked,b.[Description]" +
            //    " from " + table_BookingMaster + " as  a join " + table_ClassMaster + " b" +
            //    " on a.[Class Code]=b.[Class Code] where  a.[Show Allocation ID]= '" + AudiNo + "' and a.[Date]='" + ShowDate + "' and a.Booked=1  group by a.[Class Code],b.[Description])b on a.[Description]=b.[Description]";
            command.CommandText="select a.Seats,a.Description,a.[Booked Seats],b.[Ticket not Printed] from(select a.Seats,a.Description,b.Booked as 'Booked Seats' from(SELECT COUNT (*) as Seats,b.[Description]" +
                " from " + table_BookingMaster + " as  a join " + table_ClassMaster + " b" +
                " on a.[Class Code]=b.[Class Code] where  a.[Show Allocation ID]= '" + AudiNo + "' and a.[Date]='" + ShowDate + "' group by a.[Class Code],b.[Description])a FULL OUTER JOIN(SELECT COUNT (*) as Booked,b.[Description]" +
                " from " + table_BookingMaster + " as  a join " + table_ClassMaster + " b" +
                " on a.[Class Code]=b.[Class Code] where  a.[Show Allocation ID]= '" + AudiNo + "' and a.[Date]='" + ShowDate + "' and a.Booked=1  group by a.[Class Code],b.[Description])b on a.[Description]=b.[Description])a FULL OUTER JOIN(SELECT COUNT(a.[Booking ID]) as 'Ticket not Printed', b.[Description]"+
" from " + table_BookingMaster + " as  a join " + table_ClassMaster +
" b on a.[Class Code]=b.[Class Code] where  a.[Show Allocation ID]= '" + AudiNo + "' and a.[Date]='" + ShowDate + "'and ((a.[Tele Booking ID] is not NULL or a.[Tele Booking ID]!='') and a.[Paid Tel Booking]=1 and (a.[Ticket Printing Date_Time] is NULL or a.[Ticket Printing Date_Time]='' )) or ((a.[Web Booking ID] is not NULL or a.[Web Booking ID]!='' )and (a.[Ticket Printing Date_Time] is NULL or a.[Ticket Printing Date_Time]='')) group by a.[Class Code],b.[Description])b on a.[Description]=b.[Description]";
            return Connection.readTab(command, connWebBooking);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filmCode"></param>
        /// <returns></returns>
        public static DataTable _SelectRow_AudiWise(string filmCode)
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = "Select * from " + table_AudiRowMaster + " where  [Audi No_] = '" + filmCode + "' ";
            return Connection.readTab(command, connWebBooking);
        }

        public static DataTable _select_Seat_Layout(string AudiNo)
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = "SELECT [Booking ID] ,[Show Allocation ID],[KeyNo],[Cell Type],[Row No],[Column No],[Class Code],[Description] " +
                ",[Show Price],[Booked],[Blocked],[Lock For Booking],[Block For Agent] " +
                " FROM  " + table_BookingMaster + " where " +
                " [Show Allocation ID]= '" + AudiNo + "'  order by [Row No] desc";
            return Connection.readTab(command, connWebBooking);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="KeyNo"></param>
        /// <param name="TransactionID"></param>
        /// <param name="TotalSeats"></param>
        /// <param name="Category"></param>
        /// <param name="SeatNo"></param>
        /// <param name="PlayDate"></param>
        /// <param name="PlayTime"></param>
        /// <returns></returns>
        public static long _Insert_tempTransaction_Table(String KeyNo, long TransactionID, int TotalSeats, String Category, String SeatNo, String PlayDate, String PlayTime)
        {
            SqlCommand command = new SqlCommand();
            command.Parameters.Clear();
            command.CommandText = "Select [Show Price] from " + table_BookingMaster + " where CONVERT(datetime, [Date]) = CONVERT(datetime, '" + PlayDate + "') and Convert(time(0),[Start Time]) = CONVERT(time(0), '" + PlayTime + "') and [KeyNo] = " + KeyNo;
            decimal prine = decimal.Parse(Connection.readTab(command, connWebBooking).Rows[0][0].ToString());
            command = new SqlCommand();
            command.Parameters.Clear();
            command.CommandText = "INSERT INTO [GINC$TempSessionTable]([Transaction ID],[TotalSeats]" +
                ",[SeatCategory],[KeyNo],[SeatNo],[Price],[Status],[showdate],[showtime]) VALUES " +
                "(@transID,@TotalSeats,@SeatCategory,@KeyNo,@SeatNo," + prine + ",'True',@pDate,@pTime) ";
            command.Parameters.AddWithValue("@KeyNo", KeyNo);
            command.Parameters.AddWithValue("@transID", TransactionID);
            command.Parameters.AddWithValue("@TotalSeats", TotalSeats);
            command.Parameters.AddWithValue("@SeatCategory", Category);
            command.Parameters.AddWithValue("@SeatNo", SeatNo);
            command.Parameters.AddWithValue("@pDate", PlayDate);
            command.Parameters.AddWithValue("@pTime", PlayTime);
            Connection.EXECommand(command, connWebBooking);
            return TransactionID;
        }

        public static DataTable _SelectTempSessionTable_one(long TransactionID)
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = "Select * From [GINC$TempSessionTable] where  [Transaction ID] = " + TransactionID;
            return Connection.readTab(command, connWebBooking);
        }

        public static int _ON_Session_out(String KeyNo)
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = "Update " + table_BookingMaster + " set [Lock For Booking] = 0 , [Booking Start Date_Time]='1753-01-01 00:00:00.000'," +
                " [Agent Code]='' where [Agent Code]='" + KeyNo + "'";
            Connection.EXECommand(command, connWebBooking);
            return 1;
        }

        public static DataTable _Check_Seats_BeforeProceed(long TransactionID, String SeatNo, String filmCode)
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = "if exists(Select KeyNo from " + table_BookingMaster + " where  KeyNo in(" + SeatNo + ") and (Booked = 1 "
                + " or [Lock For Booking] = 1 or [Tele Booking ID] != '')  And [Show Allocation ID]='" + filmCode + "') "
                + "Select 0 else begin  Update " + table_BookingMaster + " set [Lock For Booking] = 1 , [Booking Start Date_Time]=getutcdate(),"
                + "[Agent Code]='" + TransactionID + "' where  [Show Allocation ID]='" + filmCode + "' And [KeyNo] in(" + SeatNo + ")  "
                + " select 1 end ";
            return Connection.readTab(command, connWebBooking);
        }
        public static DataTable _Select_NewYearSeat(string filmcode, long transectioncounter,int no)
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = "if (select COUNT(*) from "+table_BookingMaster+" where [Show Allocation ID]='"+filmcode+"' and Booked=0 and [Lock For Booking]=0 and [Tele Booking ID]='')>"+no+""
            + " begin update top(" + no + ") " + table_BookingMaster + " set [Lock For Booking] = 1,[Booking Start Date_Time]=getutcdate(),[Agent Code]='" + transectioncounter + "' OUTPUT Inserted.[Description]" 
            +" where [Show Allocation ID]='"+filmcode+"' and Booked=0 and [Lock For Booking]=0 and [Tele Booking ID]='' end "
            + "else begin select null end";
            return Connection.readTab(command, connWebBooking);
        }

        public static DataTable _Get_SeatPrice_SeatKeyNoWise(long TransactionID)
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = "Select Cast(SUM([Show Price]) as decimal(18,2)) as [Show Price]  from " + table_BookingMaster +
                " where  [Agent Code] = '" + TransactionID + "'";
            return Connection.readTab(command, connWebBooking);
        }


        public static DataTable _Get_AllSeatPrice_SeatKeyNoWise(long TransactionID)
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = "Select Cast([Show Price] as decimal(18,2)) as [Show Price]  from " + table_BookingMaster +
                " where  [Agent Code] = '" + TransactionID + "'";
            return Connection.readTab(command, connWebBooking);
        }

        #region -- Transaction ID Counter
        public static long _TransactionCounter_Max()
        {
            string sqlQuery = "Declare @tranc bigint select @tranc = Max(Counter) from [GINC$TransactionCounter] set @tranc = @tranc + 1"
                    + " insert into [GINC$TransactionCounter] values(@tranc,'','') " +
                    " select Max(Counter) from [GINC$TransactionCounter] ";
            DataTable dt = Connection.readTab(sqlQuery, connMSTicket);
            if ((dt != null) && (dt.Rows.Count > 0))
                return long.Parse(dt.Rows[0][0].ToString());
            throw new Exception("Unable to generate a transaction ID.");
        }
        #endregion

        #region -- March Promotion Transaction ID Counter
        public static long _MarchPromotionTransactionCounter_Max(long ID)
        {
            string sqlQuery = "if not EXISTS (select [Counter] from [GINC$MarchPromoTransactionCounter] where [ID]=" + ID + ") begin Declare @tranc bigint select @tranc = Max(Counter) from [GINC$MarchPromoTransactionCounter] set @tranc = @tranc + 1"
                    + " insert into [GINC$MarchPromoTransactionCounter] ([Counter],[ID]) values(@tranc," + ID + ") " +
                    " select Max(Counter) from [GINC$MarchPromoTransactionCounter]  end else begin (select [Counter] from [GINC$MarchPromoTransactionCounter] where [ID]=" + ID + ") end ";
            DataTable dt = Connection.readTab(sqlQuery, connMSTicket);
            if ((dt != null) && (dt.Rows.Count > 0))
                return long.Parse(dt.Rows[0][0].ToString());
            throw new Exception("Unable to generate a transaction ID.");
        }
        #endregion


        #region -- Booking ID Counter for New Year
        public static string NewYearBooking_Max()
        {

            string sqlQuery = "select Max(BookingId) from [dbo].[tbl_NewYearPackages]";

            DataTable dt = Connection.readTab(sqlQuery, connMSTicket);
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Find MaX Of Booking ID");
            if ((dt != null) && (dt.Rows.Count > 0))
                return (dt.Rows[0][0].ToString());
            return ("KODNY00000");

        }
        #endregion
        #region -- Booking ID Counter for Event
        public static string BollyLand_Max()
        {

            string sqlQuery = "select Max(BookingId) from [dbo].[tbl_BollyLand]";

            DataTable dt = Connection.readTab(sqlQuery, connMSTicket);
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Find MaX Of Booking ID");
            if ((dt != null) && (dt.Rows.Count > 0))
                return (dt.Rows[0][0].ToString());
            return ("KOD/BL00000");

        }
        #endregion

        #region -- Booking ID Counter for Summer camp promotion
        public static string SummerBooking_Max()
        {

            string sqlQuery = "select Max(BookingId) from [dbo].[tbl_summercamp]";

            DataTable dt = Connection.readTab(sqlQuery, connMSTicket);
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Find MaX Of Booking ID");
            if ((dt != null) && (dt.Rows.Count > 0))
                return (dt.Rows[0][0].ToString());
            return ("KODSUM00000");

        }
        #endregion

        #region -- Booking ID Counter for MMT promotion
        public static string MMTBooking_Max()
        {

            string sqlQuery = "select Max(BookingId) from [dbo].[tbl_mmtpromotion]";

            DataTable dt = Connection.readTab(sqlQuery, connMSTicket);
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Find MaX Of Booking ID");
            if ((dt != null) && (dt.Rows.Count > 0))
                return (dt.Rows[0][0].ToString());
            return ("KODMMT00000");

        }
        #endregion

        #region -- Booking ID Details Insert for New Year
        public static void NewYearBooking_Details(short CouplePackage, short SinglePackage, short TeensPackage, short KidsPackage, decimal TotalAmount, DateTime DateofBooking, string BookingID, string Name, string Email, string ContactNo, bool Status, string ReceiptNo,string royalinfo)
        {
            SqlCommand command = new SqlCommand();
            string sqlQuery = "INSERT INTO [dbo].[tbl_NewYearPackages] ([Qty_PackageTypeCouple],[Qty_PackageTypeSingle],[Qty_PackageTypeTeen],[Qty_PackageTypeKid],[TotalAmount],[DateOfBooking],[BookingId],[Name],[EmailId],[ContactNumber],[PGIsPaymentSuccess],[PGReceiptId],[Royal Info]) VALUES "
                + "(" + CouplePackage + "," + SinglePackage + "," + TeensPackage + "," + KidsPackage + "," + TotalAmount + ", getdate() ,'" + BookingID + "','" + Name + "','" + Email + "','" + ContactNo + "','" + Status + "','" + ReceiptNo + "','"+royalinfo+"')";
            command.CommandText = sqlQuery;
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Insert MaX Of Booking ID and Its Details");
            Connection.EXECommand(command, connMSTicket);
        }

        #endregion
        #region -- Booking ID Details Insert for New Year
        public static void BollylandBooking_Details(short GoldPackage, short SilverPackage, decimal TotalAmount, DateTime DateofBooking, string BookingID, string Name, string Email, string ContactNo, bool Status, string ReceiptNo)
        {
            SqlCommand command = new SqlCommand();
            string sqlQuery = "INSERT INTO [dbo].[tbl_BollyLand] ([Qty_Gold],[Qty_silver],[TotalAmount],[DateOfBooking],[BookingId],[Name],[EmailId],[ContactNumber],[PGIsPaymentSuccess],[PGReceiptId]) VALUES "
                + "(" + GoldPackage + "," + SilverPackage + "," + TotalAmount + ", getdate() ,'" + BookingID + "','" + Name + "','" + Email + "','" + ContactNo + "','" + Status + "','" + ReceiptNo+ "')";
            command.CommandText = sqlQuery;
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Insert MaX Of Booking ID and Its Details");
            Connection.EXECommand(command, connMSTicket);
        }

        #endregion

        #region -- Booking ID Details Insert for Summer camp
        public static void SummerBooking_Details(short nooftickets, decimal totalamount, decimal paybleamount, DateTime dateofbooking, string bookingid, string Name, string Email, string ContactNo, string paymentgateway, bool Status, string ReceiptNo)
        {
            SqlCommand command = new SqlCommand();
            string sqlQuery = "INSERT INTO [dbo].[tbl_summercamp] ([Nooftickets],[TotalAmount],[PayableAmount],[Dateofbooking],[BookingId],[Name],[Email],[ContactNumber],[PaymentGateWay],[IsPaymentSuccess],[ReceiptId]) VALUES "
                + "(" + nooftickets + "," + totalamount + "," + paybleamount + ", getdate() ,'" + bookingid + "','" + Name + "','" + Email + "','" + ContactNo + "','" + paymentgateway + "','" + Status + "','" + ReceiptNo + "')";
            command.CommandText = sqlQuery;
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Insert MaX Of Booking ID and Its Details");
            Connection.EXECommand(command, connMSTicket);
        }
        #endregion

        #region -- Booking ID Details Insert for mmt promotion
        public static long MMTBooking_Details(short noofpackage, string pnrno, string promocode, decimal totalamount, decimal paybleamount, DateTime dateofbooking, string bookingid, DateTime day, string Name, string Email, string ContactNo, string paymentgateway, bool Status, string ReceiptNo, string MMTbookingid)
        {
            SqlCommand command = new SqlCommand();
            string sqlQuery = "if not EXISTS(select MMTBookingId from [dbo].[tbl_mmtpromotion] where [BookingId]='" + bookingid + "')"
                + "begin INSERT INTO [dbo].[tbl_mmtpromotion] ([NoofPackages],[PnrNo],[promocode],[TotalAmount],[PayableAmount],[Dateofbooking],[BookingId],[ShowDate],[Name],[Email],[ContactNumber],[PaymentGateWay],[IsPaymentSuccess],[ReceiptId],[MMTBookingId]) VALUES "
                + "(" + noofpackage + ",'" + pnrno + "'," + "'" + promocode + "'," + totalamount + "," + paybleamount + ", getdate() ,'" + bookingid + "','" + day + "','" + Name + "','" + Email + "','" + ContactNo + "','" + paymentgateway + "','" + Status + "','" + ReceiptNo + "','" + MMTbookingid + "') select MMTBookingId from [dbo].[tbl_mmtpromotion] where [BookingId]='" + bookingid + "' end else begin select MMTBookingId from [dbo].[tbl_mmtpromotion] where [BookingId]='" + bookingid + "' end";
            command.CommandText = sqlQuery;
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Insert MaX Of Booking ID and Its Details");
            //Connection.EXECommand(command, connMSTicket);
            DataTable dt = Connection.readTab(command, connMSTicket);
            if (dt.Rows.Count > 0)
            {
                return long.Parse(dt.Rows[0][0].ToString());
            }
            else
            {

                return -1;
            }
        }
        #endregion
        #region -- Booking ID Details Insert for yatra promotion
        public static long YATRABooking_Details(short noofticket, string pnrno, string promocode, string cat, decimal DiscountedPercentage, decimal totalamount, decimal paybleamount, DateTime dateofbooking, string bookingid, DateTime day, string Name, string Email, string ContactNo, string paymentgateway, bool Status, string ReceiptNo,string transid)
        {
            SqlCommand command = new SqlCommand();
            string sqlQuery = "if not EXISTS(select transId from [dbo].[tbl_yatrapromotion] where [BookingId]='" + bookingid + "')"
                + "begin INSERT INTO [dbo].[tbl_yatrapromotion] ([NoofTickets],[PnrNo],[promocode],[Category],[DiscountedPercentage],[TotalAmount],[PayableAmount],[Dateofbooking],[BookingId],[ShowDate],[Name],[Email],[ContactNumber],[PaymentGateWay],[IsPaymentSuccess],[ReceiptId],[transId]) VALUES "
                + "(" + noofticket + ",'" + pnrno + "'," + "'" + promocode + "'," + "'" + cat + "'," + "'" + DiscountedPercentage + "'," + + +totalamount + "," + paybleamount + ", getdate() ,'" + bookingid + "','" + day + "','" + Name + "','" + Email + "','" + ContactNo + "','" + paymentgateway + "','" + Status + "','" + ReceiptNo + "','" + transid + "') select transId from [dbo].[tbl_yatrapromotion] where [BookingId]='" + bookingid + "' end else begin select transId from [dbo].[tbl_yatrapromotion] where [BookingId]='" + bookingid + "' end";
            command.CommandText = sqlQuery;
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Insert MaX Of Booking ID and Its Details");
            //Connection.EXECommand(command, connMSTicket);
            DataTable dt = Connection.readTab(command, connMSTicket);
            if (dt.Rows.Count > 0)
            {
                return long.Parse(dt.Rows[0][0].ToString());
            }
            else
            {

                return -1;
            }
        }
        #endregion

        #region -- Booking ID Details Insert for mana promotion
        public static long MANABooking_Details(short noofpackage, decimal totalamount, decimal paybleamount, DateTime dateofbooking, string bookingid, DateTime day, string Name, string Email, string ContactNo, string paymentgateway, bool Status, string ReceiptNo, string PackageType, string MANAbookingid)
        {
            SqlCommand command = new SqlCommand();
            string sqlQuery = "if not EXISTS(select MANABookingId from [dbo].[tbl_manapromotion] where [BookingId]='" + bookingid + "')"
            + "begin INSERT INTO [dbo].[tbl_manapromotion] ([NoofPackages],[TotalAmount],[PayableAmount],[Dateofbooking],[BookingId],[ShowDate],[Name],[Email],[ContactNumber],[PaymentGateWay],[IsPaymentSuccess],[ReceiptId],[PackageType],[MANABookingId]) VALUES "
                + "(" + noofpackage + "," + totalamount + "," + paybleamount + ", getdate() ,'" + bookingid + "','" + day + "','" + Name + "','" + Email + "','" + ContactNo + "','" + paymentgateway + "','" + Status + "','" + ReceiptNo + "','" + PackageType + "','" + MANAbookingid + "') select MANABookingId from [dbo].[tbl_manapromotion] where [BookingId]='" + bookingid + "' end else begin select MANABookingId from [dbo].[tbl_manapromotion] where [BookingId]='" + bookingid + "' end";
            command.CommandText = sqlQuery;
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Insert MaX Of Booking ID and Its Details TransID : ");
            //Connection.EXECommand(command, connMSTicket);
            DataTable dt = Connection.readTab(command, connMSTicket);
            if (dt.Rows.Count > 0)
            {
                return long.Parse(dt.Rows[0][0].ToString());
            }
            else
            {

                return -1;
            }
        }
        #endregion

        #region -- Booking ID Details Insert for family offer
        public static long FAMILYOFFERBooking_Details(short noofpackage, decimal totalamount, decimal paybleamount, DateTime dateofbooking, string bookingid, DateTime day, string Name, string Email, string ContactNo, string paymentgateway, bool Status, string ReceiptNo, string PackageType, string FAMILYOFFERbookingid,string royalcardno,decimal pcktotalamount,decimal ticketpaybaleamount)
        {
            SqlCommand command = new SqlCommand();
            string sqlQuery = "if not EXISTS(select FAMILYOFFERBookingId from [dbo].[tbl_familyoffer] where [BookingId]='" + bookingid + "')"
            + "begin INSERT INTO [dbo].[tbl_familyoffer] ([NoofPackages],[TicketTotalAmount],[PackagePayableAmount],[Dateofbooking],[BookingId],[ShowDate],[Name],[Email],[ContactNumber],[PaymentGateWay],[IsPaymentSuccess],[ReceiptId],[PackageType],[FAMILYOFFERBookingId],[RoyalCardNo],[PackageTotalAmount],[TicketPayableAmount]) VALUES "
                + "(" + noofpackage + "," + totalamount + "," + paybleamount + ", getdate() ,'" + bookingid + "','" + day + "','" + Name + "','" + Email + "','" + ContactNo + "','" + paymentgateway + "','" + Status + "','" + ReceiptNo + "','" + PackageType + "','" + FAMILYOFFERbookingid + "','" + royalcardno + "'," + pcktotalamount + "," + ticketpaybaleamount + ") select FAMILYOFFERBookingId from [dbo].[tbl_familyoffer] where [BookingId]='" + bookingid + "' end else begin select FAMILYOFFERBookingId from [dbo].[tbl_familyoffer] where [BookingId]='" + bookingid + "' end";
            command.CommandText = sqlQuery;
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Insert MaX Of Booking ID and Its Details TransID : ");
            //Connection.EXECommand(command, connMSTicket);
            DataTable dt = Connection.readTab(command, connMSTicket);
            if (dt.Rows.Count > 0)
            {
                return long.Parse(dt.Rows[0][0].ToString());
            }
            else
            {

                return -1;
            }
        }
        #endregion

        #region -- Booking ID Details Insert for World Card/Non-World Card MC promotion
        public static void McPromotionBooking_Details(short Nooftickets, short noofpackage, decimal totalamount, decimal paybleamount, DateTime dateofbooking, string bookingid, DateTime showdate, string Name, string Email, string ContactNo, string paymentgateway, bool Status, string ReceiptNo, string Type, string PromotionCode, string bank, string card, string promocode)
        {
            SqlCommand command = new SqlCommand();
            string sqlQuery = "INSERT INTO [dbo].[MCPROMOTIONS_DETAIL] ([Nooftickets],[NoofPackages],[TotalAmount],[PayableAmount],[Dateofbooking],[BookingId],[ShowDate],[Name],[Email],[ContactNumber],[PaymentGateWay],[IsPaymentSuccess],[ReceiptId],[Type],[PromotionCode],[Bank Name],[Card No],[Promo Code]) VALUES "
            + "(" + Nooftickets + "," + noofpackage + "," + totalamount + "," + paybleamount + ", getdate() ,'" + bookingid + "','" + showdate + "','" + Name + "','" + Email + "','" + ContactNo + "','" + paymentgateway + "','" + Status + "','" + ReceiptNo + "','" + Type + "','" + PromotionCode + "','" + bank + "','" + card + "','" + promocode + "')";
            command.CommandText = sqlQuery;
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Insert Booking ID and Its Details");
            Connection.EXECommand(command, connMSTicket);
        }
        #endregion
        #region -- Booking ID Details Insert for Agent
        public static long InsertAgentBooking_Details(TransactionRecord tr)
        {
            
            SqlCommand command = new SqlCommand();
            string sqlQuery = "if not EXISTS(select ReferenceNo from [dbo].[tbl_Booking_Detail_Agent] where [ReferenceNo]=" + tr.ReferenceNo + ")"
                + "begin INSERT INTO [MSTicketDB_Live_Latest].[dbo].[tbl_Booking_Detail_Agent] (Agent_UserName,ReferenceNo,BookingID,BookingDate,TimeOfBooking,Location,Play,ShowTime,ShowDate,Category,TotalSeats,TotalAmount,SeatNo,BookingPerson,Status,Payableamount) VALUES "
            + "('" + tr.AgentCode + "'," + tr.ReferenceNo + "," + tr.BookingID + ",'" + tr.DateOfBooking + "','" + tr.TimeOfBooking + "','" + tr.Location + "','" + tr.Play + "','" + tr.ShowTime + "','" + tr.ShowDate + "','" + tr.Category + "','" + tr.TotalSeats + "','" + tr.TotalAmount + "','" + tr.SeatInfo + "','" + tr.Name + "','" + 0 + "','" + tr.TotalAmount + "')" + "select ReferenceNo from [dbo].[tbl_Booking_Detail_Agent] where [ReferenceNo]=" + tr.ReferenceNo + " end else begin select ReferenceNo from [dbo].[tbl_Booking_Detail_Agent] where [ReferenceNo]=" + tr.ReferenceNo + " end";
            command.CommandText = sqlQuery;
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Insert MaX Of Booking ID and Its Details");
            //Connection.EXECommand(command, connMSTicket);
            DataTable dt = Connection.readTab(command, connMSTicket);
            if (dt.Rows.Count > 0)
            {
                return long.Parse(dt.Rows[0][0].ToString());
            }
            else
            {

                return -1;
            }
        }
        #endregion


        #region --check psckage  Details  for mmt promotion
        public static string MMTpackage_check(string pnrno)
        {
            string sqlQuery = "select sum([NoofPackages]) from [dbo].[tbl_mmtpromotion] where [IsPaymentSuccess]=1 and [PnrNo]=" + "'" + pnrno + "'";

            DataTable dt = Connection.readTab(sqlQuery, connMSTicket);
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("check psckage  Details  for mmt promotion");
            if ((dt != null) && (dt.Rows.Count > 0))
                return (dt.Rows[0][0].ToString());
            return ("0");
        }
        #endregion
        #region --check psckage  Details  for yatra promotion
        public static string YATRAPromotion_check(string pnrno)
        {
            string sqlQuery = "select sum([NoofTickets]) from [dbo].[tbl_yatrapromotion] where [IsPaymentSuccess]=1 and [PnrNo]=" + "'" + pnrno + "'";

            DataTable dt = Connection.readTab(sqlQuery, connMSTicket);
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("check psckage  Details  for yatra promotion");
            if ((dt != null) && (dt.Rows.Count > 0))
                return (dt.Rows[0][0].ToString());
            return ("0");
        }
        #endregion

        #region -- HDFC LOG Status
        /// <summary>
        /// Fields Used in order  -- reference id, amount, trackid, URL String
        /// </summary>
        /// <param name="Values"></param>
        public void HDFCLog(params string[] Values)
        {
            SqlCommand command = new SqlCommand();
            //Fields Used  -- reference id, description, status, booking id
            string sqlQuery = "INSERT INTO GINC$HDFCLog ([ReferenceID],[Amount],[trackId],[Datetime],[URLString]) VALUES "
                + " (" + (Values[0] == "" ? 0 : Int64.Parse(Values[0])) + "," + (Values[1] == "" ? 0 : decimal.Parse(Values[1])) + ",'" + Values[2] + "',getdate(),'" + Values[3] + "')";
            command.CommandText = sqlQuery;
            Connection.EXECommand(command, connMSTicket);
        }

        public DataTable HDFCLogCheck(string bookingID)
        {
            SqlCommand command = new SqlCommand();
            string sqlQuery = "Select * from GINC$HDFCLog where referenceID = '" + bookingID + "' order by ID";
            command.CommandText = sqlQuery;
            return Connection.readTab(command, connMSTicket);
        }

        #endregion

        #region API_Methods
        public static bool ValidateAgent(string username, string password)
        {
            try
            {
                SqlCommand objCmd = new SqlCommand();
                objCmd.CommandText = "Select * from tbl_AgentLogin where username = '" + username + "' and password = '" + password + "' and Status =1";
                DataTable objDataTable = new DataTable();
                objDataTable = Connection.readTab(objCmd, DBAccess.connMSTicket);
                if (objDataTable.Rows.Count > 0)
                {
                    //Log a message
                    Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Successful API access by agent: " + username);
                    return true;
                }
                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Unsuccessful API access by agent: " + username);
            }
            catch (Exception ex)
            {
                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("API access error. Agent: " + username);
                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write(ex.Message);
            }
            return false;
        }

        #region -- Code for Seat Selection Page

        /// <summary>
        /// 
        /// </summary>
        /// <param name="TransactionID"></param>
        /// <param name="SeatNo"></param>
        /// <param name="showDate"></param>
        /// <param name="ShowTime"></param>
        /// <returns></returns>
        public static DataSet CheckSeatsAndLock(long TransactionID, string SeatNo, string showDate, string ShowTime)
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = "if exists(Select KeyNo from " + table_BookingMaster + " where  KeyNo in(" + SeatNo + ") and " +
                "(Booked = 1  or [Lock For Booking] = 1 or [Tele Booking ID] != '') and CONVERT(date,Date)= CONVERT(Date,'" + showDate + "')" +
                " and CONVERT(time(0), [Start Time]) =CONVERT(time(0),'" + ShowTime + "') ) Select 0 else " +
                " begin  Update " + table_BookingMaster + " set [Lock For Booking] = 1 , [Booking Start Date_Time]=getutcdate(),[Agent Code]='" +
                TransactionID + "' where CONVERT(datetime, [Date]) = CONVERT(datetime, '" + showDate + "') " +
                " and Convert(time(0),[Start Time]) = CONVERT(time(0), '" + ShowTime + "') and [KeyNo] in(" + SeatNo + ") select 1 end ";
            return Connection.readDataSet(command, connWebBooking);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="TransactionID"></param>
        /// <returns></returns>
        public static decimal GetSeatPriceSeatKeyNoWise(long TransactionID)
        {
            DataTable dt = _Get_SeatPrice_SeatKeyNoWise(TransactionID);
            return (dt != null && dt.Rows.Count > 0) ? decimal.Parse(dt.Rows[0][0].ToString()) : 0;
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="TransactionID"></param>
        /// <param name="Source"></param>
        /// <param name="DateOfBooking"></param>
        /// <param name="TimeOfBooking"></param>
        /// <param name="Location"></param>
        /// <param name="Play"></param>
        /// <param name="ShowTime"></param>
        /// <param name="ShowDate"></param>
        /// <param name="Category"></param>
        /// <param name="PaymentType"></param>
        /// <param name="AgentCode"></param>
        /// <param name="SeatInfo"></param>
        /// <param name="MobileNo"></param>
        /// <param name="EmailID"></param>
        /// <param name="CustomerName"></param>
        /// <param name="TotalSeats"></param>
        /// <param name="TotalAmount"></param>
        /// <param name="strPaymentReceiptNo"></param>
        /// <returns></returns>
        public static DataSet funFinalBooking(TransactionRecord tr)
        {
            DataSet dsexcep = new DataSet();
            DataTable dtexcep = new DataTable();
            try
            {

                int intBookingID = 0;
                int isexecute = 0;
                DataSet objDataSet = new DataSet();
                SqlCommand command = new SqlCommand();
                try
                {
                    Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("funFinalBooking: Updating GCELL temp transaction table.");

                    command.CommandText = "if  not EXISTS (Select id from ginc$bookingTransaction_temp where BookingID =" + tr.BookingID + ")" +
                        " begin INSERT INTO [GINC$BookingTransaction_temp] ([BookingID],[ReferenceNo],[Source],[DateOfBooking],[TimeOfBooking]," +
                        " [Location],[Play],[ShowTime],[ShowDate],[Category],[TotalSeats],[TotalAmount],[PaymentType],[CardType],[CardNo]," +
                        " [MobileNo],[EmailID],[Name],[PaymentGateway],[AgentCode],[BookingType],[Status],[SeatInfo])VALUES (@BookingID," +
                        " @ReferenceNo,@Source,@DateOfBooking,@TimeOfBooking,@Location,@Play,@ShowTime,@ShowDate,@Category,@TotalSeats," +
                        "@TotalAmount,@PaymentType,@CardType,@CardNo,@MobileNo,@EmailID,@Name, @PaymentGateway,@AgentCode,@BookingType,@Status,@seatinfo)" +
                        " Select @@IDENTITY end else begin 	Select id from ginc$bookingTransaction_temp where BookingID =" + tr.BookingID + " end ";
                    command.Parameters.AddWithValue("@BookingID", tr.BookingID);
                    command.Parameters.AddWithValue("@ReferenceNo", tr.BookingID);
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
                    command.Parameters.AddWithValue("@CardType", "NIL");
                    command.Parameters.AddWithValue("@CardNo", "1111222233334444");
                    command.Parameters.AddWithValue("@MobileNo", tr.MobileNo);
                    command.Parameters.AddWithValue("@EmailID", tr.EmailID);
                    command.Parameters.AddWithValue("@Name", tr.Name);
                    command.Parameters.AddWithValue("@PaymentGateway", "API");
                    command.Parameters.AddWithValue("@AgentCode", tr.AgentCode);
                    command.Parameters.AddWithValue("@BookingType", "Web-API");
                    command.Parameters.AddWithValue("@Status", "0");
                    command.Parameters.AddWithValue("@seatinfo", tr.SeatInfo);
                    objDataSet = Connection.APIreadDataSet(command, connMSTicket);
                    if (objDataSet.Tables.Count > 0)
                    {
                        if (objDataSet.Tables[0].Columns.Contains("Exception"))
                            return objDataSet;
                    }
                }
                catch (SqlException ex)
                {
                    //Log Message
                    Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Error in funFinalBooking: " + ex.Message);
                    dsexcep = new DataSet();
                    dtexcep.Columns.Add("Exception", typeof(string));
                    dtexcep.Rows.Add(ex.Message.ToString());
                    dsexcep.Tables.Add(dtexcep);
                    return dsexcep;
                    //return null;
                }

                DataTable dt = Connection.APIreadTab(command, connMSTicket);
                if (dt != null && dt.Rows.Count > 0 && !dt.Columns.Contains("Exception"))
                {
                    intBookingID = int.Parse(dt.Rows[0][0].ToString());
                    Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Web-API Generated transaction ID: " + intBookingID.ToString());
                }
                else
                {
                    Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Web-API Not able to generate transaction ID");
                    dsexcep = new DataSet();
                    dtexcep.Columns.Add("Exception", typeof(string));
                    dtexcep.Rows.Add("Web-API Not able to generate transaction ID");
                    dsexcep.Tables.Add(dtexcep);
                    return dsexcep;
                }

                if (intBookingID != 0)
                {
                    Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("funFinalBooking: Updating Booking Master.");
                    //Updating in the Booking Master
                    bool passed = false;
                    command = new SqlCommand();
                    command.CommandText = "Update " + table_BookingMaster + " WITH(HOLDLOCK,UPDLOCK) Set [Lock For Booking] = 1 , [Booked] = 1 ," +
                                          " [Web Booking ID] = " + intBookingID + " , [Payment Ref_ No_]= " + tr.ReceiptNo.ToUpper() + " , [Booking End Date_Time]='"
                                            + DateTime.Now.ToString("MM/dd/yyyy") + "', [Agent Code] = '" + tr.AgentCode + "' where [Agent Code] = '" + tr.BookingID + "'";
                    DataTable tempData = TransactionBOL.Select_Temptransaction_REFIDWISE(long.Parse(tr.BookingID.ToString()));
                    Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Row Count : " + dt.Rows.Count);
                    if (tempData != null && tempData.Rows.Count > 0)
                    {
                        try
                        {
                            string WebPromotionID = tempData.Rows[0]["WebPromotionId"] != null ? tempData.Rows[0]["WebPromotionId"].ToString() : "";
                            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("WebPromotionID : " + WebPromotionID);
                            decimal TotalAmount = string.IsNullOrEmpty(tempData.Rows[0]["TotalAmount"].ToString()) ? 0 : Convert.ToDecimal(tempData.Rows[0]["TotalAmount"]);
                            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Amount : " + TotalAmount);
                            string Play = tempData.Rows[0]["Play"] != null ? tempData.Rows[0]["Play"].ToString() : "";
                            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Play : " + Play);
                            string MobileNo = tempData.Rows[0]["MobileNo"] != null ? tempData.Rows[0]["MobileNo"].ToString() : "";
                            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Mobile No. : " + MobileNo);
                            string BookingID = tempData.Rows[0]["ID"] != null ? tempData.Rows[0]["ID"].ToString() : "";
                            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Web Booking ID : " + BookingID);
                            string ReferenceNo = tempData.Rows[0]["BookingID"] != null ? tempData.Rows[0]["BookingID"].ToString() : "";
                            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Reference No. : " + ReferenceNo);
                            int Totalseats = string.IsNullOrEmpty(tempData.Rows[0]["TotalSeats"].ToString()) ? 0 : Convert.ToInt32(tempData.Rows[0]["TotalSeats"]);
                            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Total Seats : " + Totalseats);
                            string PaymentGateway = tempData.Rows[0]["PaymentGateway"] != null ? tempData.Rows[0]["PaymentGateway"].ToString() : "";
                            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("PG : " + PaymentGateway);
                            string AgentCode = tr.AgentCode != null ? tr.AgentCode.ToString() : "";
                            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Agent Code : " + AgentCode);
                            string AgentCodeSubcode = "";
                            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Agent Code Sub-code  : " + AgentCodeSubcode);
                            decimal DiscountPercentage = string.IsNullOrEmpty(tempData.Rows[0]["DiscountPercentage"].ToString()) ? 0 : Convert.ToDecimal(tempData.Rows[0]["DiscountPercentage"]);
                            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Discount % : " + DiscountPercentage);
                            string ReceiptNo = string.IsNullOrEmpty(tr.ReceiptNo.ToString()) ? "" : tr.ReceiptNo.ToString();
                            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Receipt No.  : " + ReceiptNo);
                            decimal AvailedAmount = string.IsNullOrEmpty(tempData.Rows[0]["AvailedAmount"].ToString()) ? 0 : Convert.ToDecimal(tempData.Rows[0]["AvailedAmount"]);
                            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Availed Amount  : " + AvailedAmount);
                            decimal AvailedPoints = string.IsNullOrEmpty(tempData.Rows[0]["AvailedPoints"].ToString()) ? 0 : Convert.ToDecimal(tempData.Rows[0]["AvailedPoints"]);
                            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Availed Points  : " + AvailedPoints);
                            string rowsEffected = TransactionBOL.Successful_BookingByMsTicket(TotalAmount, Play, MobileNo, ReferenceNo,
                                Totalseats, PaymentGateway, AgentCode, DiscountPercentage, ReceiptNo, AvailedAmount, AvailedPoints, BookingID, AgentCodeSubcode, WebPromotionID);
                            if (Convert.ToInt32(rowsEffected.ToString()) > 0)
                            {
                                isexecute = Convert.ToInt32(rowsEffected.ToString());
                                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("NMLIVEDB Updated from PG : " + PaymentGateway + " For Booking ID : " + intBookingID);
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
                                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Unable to update NMLIVEDB." + rowsEffected + " For Booking ID : " + intBookingID);
                                dsexcep = new DataSet();
                                dtexcep.Columns.Add("Exception", typeof(string));
                                dtexcep.Rows.Add("Unable to Update Table : " + rowsEffected);
                                dsexcep.Tables.Add(dtexcep);
                                return dsexcep;
                            }

                        }
                        catch (Exception ex)
                        {
                            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Unable to update NMLIVEDB. " + ex.Message + " For Booking ID : " + intBookingID);
                            dsexcep = new DataSet();
                            dtexcep.Columns.Add("Exception", typeof(string));
                            dtexcep.Rows.Add("Unable to Update Table : " + ex.Message.ToString());
                            dsexcep.Tables.Add(dtexcep);
                            return dsexcep;
                        }
                    }
                    else
                    {
                        Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("There is no Data in a row. For Booking ID : " + intBookingID);
                        dsexcep = new DataSet();
                        dtexcep.Columns.Add("Exception", typeof(string));
                        dtexcep.Rows.Add("There is no Data in a row");
                        dsexcep.Tables.Add(dtexcep);
                        return dsexcep;
                    }
                    try
                    {
                        //int isexecute = Connection.EXECommand(command, connWebBooking);
                        if (isexecute > 0)
                        {
                            passed = true;

                            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("funFinalBooking: Updating GCELL transaction table.");
                            command = new SqlCommand();
                            command.CommandText = " insert into GINC$BookingTransaction ([BookingID] ,[ReferenceNo],[Source],[DateOfBooking],[TimeOfBooking]," +
                                "[Location],[Play],[ShowTime],[ShowDate],[Category],[TotalSeats],[TotalAmount],[PaymentType],[CardType],[CardNo]," +
                                "[MobileNo],[EmailID],[Name],[PaymentGateway],[AgentCode],[BookingType]  ,[Status],[SeatInfo],ReceiptNo) " +
                                "(SELECT  [ID]," + tr.BookingID + ",[Source],[DateOfBooking],[TimeOfBooking] ,[Location],[Play],[ShowTime],[ShowDate],[Category]," +
                                "[TotalSeats],[TotalAmount]  ,[PaymentType],[CardType],[CardNo],[MobileNo],[EmailID],[Name],[PaymentGateway] " +
                                ",[AgentCode],[BookingType],'True',[SeatInfo],'" + tr.AgentCode + "'  from [GINC$BookingTransaction_temp] where " +
                                "[BookingID] = " + tr.BookingID + ") Select @@IDENTITY";

                            dt = Connection.readTab(command, connMSTicket);

                            if (dt != null && dt.Rows.Count > 0)
                            {
                                string id = dt.Rows[0][0].ToString();
                                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Web-API Generated a Booking ID: " + id);
                            }
                            else
                            {
                                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("funFinalBooking: Unable to update GCELL transaction table.");
                                dsexcep = new DataSet();
                                dtexcep.Columns.Add("Exception", typeof(string));
                                dtexcep.Rows.Add("Unable to Update Table");
                                dsexcep.Tables.Add(dtexcep);
                                return dsexcep;
                            }
                        }
                        else
                        {
                            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("funFinalBooking: Unable to Update Booking Master Table. IsExecute : "+ isexecute);
                            dsexcep = new DataSet();
                            dtexcep.Columns.Add("Exception", typeof(string));
                            dtexcep.Rows.Add("Unable to Update Table");
                            dsexcep.Tables.Add(dtexcep);
                            return dsexcep;
                        }
                    }
                    catch (SqlException sqlE)
                    {
                        //Log Message
                        Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Error in funFinalBooking: " + sqlE.Message);
                        passed = false;
                        dsexcep = new DataSet();
                        dtexcep.Columns.Add("Exception", typeof(string));
                        dtexcep.Rows.Add(sqlE.Message.ToString());
                        dsexcep.Tables.Add(dtexcep);
                        return dsexcep;
                    }

                    if (!passed)
                    {
                        try
                        {
                            command = new SqlCommand();
                            command.CommandText = "Update " + table_BookingMaster + " WITH(HOLDLOCK,UPDLOCK) Set [Lock For Booking] = 0 , [Booked] = 0 ," +
                                                    " [Web Booking ID] ='', [Payment Ref_ No_]= '',[Booking Start Date_Time]='1753-01-01 00:00:00.000', [Booking End Date_Time]='1753-01-01 00:00:00.000', [Agent Code]='' where [Agent Code] = '" + tr.BookingID + "'";
                            Connection.EXECommand(command, connWebBooking);
                        }
                        catch (SqlException sqlE)
                        {
                            //Log Message that alternate failed too
                            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Error in funFinalBooking: " + sqlE.Message);
                            dsexcep = new DataSet();
                            dtexcep.Columns.Add("Exception", typeof(string));
                            dtexcep.Rows.Add(sqlE.Message.ToString());
                            dsexcep.Tables.Add(dtexcep);
                            return dsexcep;
                        }
                    }

                }
                else
                {
                    try
                    {
                        command = new SqlCommand();
                        command.Parameters.Clear();
                        command.CommandText = "Update " + table_BookingMaster + " WITH(HOLDLOCK,UPDLOCK) Set [Lock For Booking] = 0 , [Booked] = 0 ," +
                                               " [Web Booking ID] ='', [Payment Ref_ No_]= '',[Booking Start Date_Time]='1753-01-01 00:00:00.000', [Booking End Date_Time]='1753-01-01 00:00:00.000', [Agent Code]='' where [Agent Code] = '" + tr.BookingID + "'";
                        Connection.EXECommand(command, connWebBooking);
                    }
                    catch (SqlException sqlE)
                    {
                        //Log Message 
                        Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Error in funFinalBooking: " + sqlE.Message);
                        dsexcep = new DataSet();
                        dtexcep.Columns.Add("Exception", typeof(string));
                        dtexcep.Rows.Add(sqlE.Message.ToString());
                        dsexcep.Tables.Add(dtexcep);
                        return dsexcep;
                    }

                }

                connWebBooking.Close();

                try
                {
                    command = new SqlCommand();
                    command.CommandText = "Select [ID],[BookingID],[ReferenceNo],[Source],[DateOfBooking],[TimeOfBooking],[Location],[Play],[ShowTime],[ShowDate],[Category],[TotalSeats],[TotalAmount],[Status],[SeatInfo] from GINC$BookingTransaction Where BookingID ='" + intBookingID + "'";
                    objDataSet = new DataSet();
                    objDataSet = Connection.readDataSet(command, connMSTicket);
                    return objDataSet;

                }
                catch (SqlException sqlE)
                {
                    //Log Message 
                    Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Error in funFinalBooking: " + sqlE.Message);
                    dsexcep = new DataSet();
                    dtexcep.Columns.Add("Exception", typeof(string));
                    dtexcep.Rows.Add(sqlE.Message.ToString());
                    dsexcep.Tables.Add(dtexcep);
                    return dsexcep;
                }
            }
            catch (Exception ex)
            {
                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Error in funFinalBooking: " + ex.Message);
                dsexcep = new DataSet();
                dtexcep.Columns.Add("Exception", typeof(string));
                dtexcep.Rows.Add(ex.Message.ToString());
                dsexcep.Tables.Add(dtexcep);
                return dsexcep;
            }
            finally
            {
                connWebBooking.Close();
            }
            //return null;
        }


        public static string ValentineBooking_Max()
        {
            string sqlQuery = "select Max(BookingId) from [dbo].[tbl_ValentinePackages]";
            DataTable dt = Connection.readTab(sqlQuery, connMSTicket);
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Find Max of Booking ID");
            if ((dt != null) && (dt.Rows.Count > 0))
                return (dt.Rows[0][0].ToString());
            return ("KODVL00000");
        }
        public static string BotyBooking_Max()
        {
            string sqlQuery = "select Max(BookingID) from [dbo].[tbl_Boty]";
            DataTable dt = Connection.readTab(sqlQuery, connMSTicket);
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Find Max of Booking ID");
            if ((dt != null) && (dt.Rows.Count > 0))
                return (dt.Rows[0][0].ToString());
            return ("BOTY00000");
        }
        public static string EventBooking_Max()
        {
            string sqlQuery = "select Max(KODBookingId) from [dbo].[Event_Transaction]";
            DataTable dt = Connection.readTab(sqlQuery, connMSTicket);
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Find Max of Booking ID");
            if ((dt != null) && (dt.Rows.Count > 0))
                return (dt.Rows[0][0].ToString());
            return ("EVENT00000");
        }
        public static string DandiyaBooking_Max()
        {
            string sqlQuery = "select Max(BookingId) from [dbo].[tbl_Dandiyanight]";
            DataTable dt = Connection.readTab(sqlQuery, connMSTicket);
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Find Max of Booking ID");
            if ((dt != null) && (dt.Rows.Count > 0))
                return (dt.Rows[0][0].ToString());
            return ("KODVL00000");
        }

        //public static void ValentineBooking_Details(short Package1, short Package2, short Package3, decimal TotalAmount, DateTime DateofBooking, string BookingID, string Name, string Email, string Contact, bool Status, string ReceiptNo)
        //{
        //  SqlCommand command = new SqlCommand();
        //    string sqlQuery = "Insert into [dbo].[tbl_ValentinePackages] ([Couple_5999],[Couple_3999],[Couple_3499],[TotalAmount],[DateOfBooking],[BookingId],[Name],[EmailId],[ContactNumber],[PGIsPaymentSuccess],[PGReceiptId]) Values "
        //        + "(" + Package1 + "," + Package2 + "," + Package3 + ", " + TotalAmount + ", getdate() , '" + BookingID + "', '" + Name +"', '" + Email + "', '" + Contact + "', '" + Status +"', '" + ReceiptNo + "')";
        //    command.CommandText = sqlQuery;
        //    Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Insert MaX Of Booking ID and Its Details");
        //    Connection.EXECommand(command, connMSTicket);
          
        //}

        internal static void ValentineBooking_Details(string amt, int Quantity, decimal TotalAmount, DateTime DateofBooking, string BookingID, string Name, string Email, string Contact, bool Status, string ReceiptNo,string ip,string campaignid,string mailer)
        {
            SqlCommand command = new SqlCommand();
            string sqlQuery = "Insert into [dbo].[tbl_ValentinePackages] ([Package],[Quantity],[TotalAmount],[DateOfBooking],[BookingId],[Name],[EmailId],[ContactNumber],[PGIsPaymentSuccess],[PGReceiptId],Ip,[campaign id],[Source]) values"
                + "(" + amt + "," + Quantity + ", " + TotalAmount + ", getdate() , '" + BookingID + "', '" + Name + "', '" + Email + "', '" + Contact + "', '" + Status + "', '" + ReceiptNo + "','"+ip+"','"+campaignid+"','"+mailer+"')";
            command.CommandText = sqlQuery;
              Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Insert MaX Of Booking ID and Its Details");
              Connection.EXECommand(command, connMSTicket);
        }
        internal static int BotyBooking_Details(string formid, string entryid, decimal TotalAmount, DateTime DateofBooking, string BookingID, bool Status, string ReceiptNo)
        {
            SqlCommand command = new SqlCommand();
            string sqlQuery = "Insert into [dbo].[tbl_Boty] ([formID],[EnrtyID],[Amount],[dateoftransection],[bookingID],[PGIsPaymentSuccess],[PGReceiptId]) values"
                + "('" + formid + "','" + entryid + "','" + TotalAmount + "', getdate() , '" + BookingID + "', '"+ Status + "', '" + ReceiptNo + "')";
            command.CommandText = sqlQuery;
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Insert MaX Of Booking ID and Its Details");
            return Connection.EXECommand(command, connMSTicket);
        }
        internal static int EventBooking_Details(string BookingID, string Name, string Email, string Contact_No, string Event_Name, string Url, decimal TotalAmount, DateTime DateofBooking, bool Status, string ReceiptNo,string KODBookingID)
        {
            SqlCommand command = new SqlCommand();
            string sqlQuery = "Insert into [dbo].[Event_Transaction] ([BookingID],[Name],[Email],[Contact_No],[Event_Name],[ReturnUrl],[Amount],[Dateoftransaction],[PGIsPaymentSuccess],[PGReceiptId],[KODBookingId]) values"
                + "('" + BookingID + "','" + Name + "','" + Email + "','" + Contact_No + "','" + Event_Name + "','" + Url + "','" + TotalAmount + "', getdate() ,'" + Status + "', '" + ReceiptNo + "','" + KODBookingID + "')";
            command.CommandText = sqlQuery;
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Insert MaX Of Booking ID and Its Details");
            return Connection.EXECommand(command, connMSTicket);
        }
        internal static void DandiyaBooking_Details(string amt, int Quantity, decimal TotalAmount, DateTime DateofBooking, string BookingID, string Name, string Email, string Contact, bool Status, string ReceiptNo,string type,string eventdate)
        {
            SqlCommand command = new SqlCommand();
            string sqlQuery = "Insert into [dbo].[tbl_Dandiyanight] ([Package],[Quantity],[TotalAmount],[DateOfBooking],[BookingId],[Name],[EmailId],[ContactNumber],[PGIsPaymentSuccess],[PGReceiptId],[Package Type],[Event Date]) values"
                + "(" + amt + "," + Quantity + ", " + TotalAmount + ", getdate() , '" + BookingID + "', '" + Name + "', '" + Email + "', '" + Contact + "', '" + Status + "', '" + ReceiptNo + "','" + type + "','" + eventdate + "')";
            command.CommandText = sqlQuery;
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Insert MaX Of Booking ID and Its Details");
            Connection.EXECommand(command, connMSTicket);
        }

        public static void March_Promotion(string Category, long BookingID, string DateOfBooking, int TotalSeats, decimal TotalAmount, string Package, int issuccess)
        {

            SqlCommand command = new SqlCommand();
            string sqlQuery = "INSERT INTO [dbo].[March_Promotion] ([Package],[BookingId],[DateOfBooking],[TotalAmount],[TotalSeats],[Category],[PGIsPaymentSuccess]) VALUES "
                + "('" + Package + "','" + BookingID + "'," + " getdate() " + ",'" + TotalAmount + "'," + TotalSeats + ",'" + Category + "'," + issuccess +" )";
            command.CommandText = sqlQuery;
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Insert MaX Of Booking ID and Its Details");
            Connection.EXECommand(command, connMSTicket);
        }
        public static void Insert_ShowDetail(string Session_val,long TransectionCountre)
        {

            SqlCommand command = new SqlCommand();
            string sqlQuery = "update [dbo].[GINC$TransactionCounter]  set [Seat_Val]='"+ Session_val + "' where Counter=" + TransectionCountre;
            command.CommandText = sqlQuery;
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Insert MaX Of Booking ID and Its Details");
            Connection.EXECommand(command, connMSTicket);
        }
        public static void Insert_SeatInfo(string seatinfo, long TransectionCountre)
        {

            SqlCommand command = new SqlCommand();
            string sqlQuery = "update [dbo].[GINC$TransactionCounter]  set [Seat_Info]='" + seatinfo + "' where Counter=" + TransectionCountre;
            command.CommandText = sqlQuery;
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Insert MaX Of Booking ID and Its Details");
            Connection.EXECommand(command, connMSTicket);
        }
        public static void Update_AgentBooking(long BookingID) //Update the payment status one for agent booking.
        {

            SqlCommand command = new SqlCommand();
            string sqlQuery = "update [dbo].[tbl_Booking_Detail_Agent]  set [Status]='" + 1 + "' where BookingID=" + BookingID;
            command.CommandText = sqlQuery;
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Insert MaX Of Booking ID and Its Details");
            Connection.EXECommand(command, connMSTicket);
        }
        public static void Update_AgentLogin(string username,string pwd,string ip) //Update the login table for agent i.e. login time and login ip.
        {

            SqlCommand command = new SqlCommand();
            string sqlQuery = "update [dbo].[tbl_Agent_Login]  set [LastLoginIP]='" + ip + "',LastLogin=getdate() where username='" + username + "' And password='"+pwd+"'";
            command.CommandText = sqlQuery;
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Insert MaX Of Booking ID and Its Details");
            Connection.EXECommand(command, connMSTicket);
        }
        public static int Set_finalstatus(int AuditNo, string ShowName, string ShowLocation, string ShowDate, string ShowTime, int Iscompleted)
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = "update [dbo].[ShowAudit_Table] set Iscompleted=1 where [AuditNo]=" + AuditNo + "and [ShowName] = '" + ShowName + "' and [ShowLocation]='" + ShowLocation + "'and [ShowDate]='" + ShowDate + "'and [ShowTime]='" + ShowTime + "'";
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Fetch show Details where" + ShowName + command.CommandText);
            return Connection.EXECommand(command, connMSTicket);
        }
        public static int Delete_audit(string check)
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = "delete from [dbo].[ShowAudit_Table] where SUBSTRING([SeatID], 1, 1)='" + check + "' and Iscompleted='" + 0 + "'";
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Delete extra detail from audit table");
            return Connection.EXECommand(command, connMSTicket);
        }
        public static int Update_audit(DataTable dt_insertvaccant)
        {
            return Connection.bulkinsert(dt_insertvaccant, connMSTicket, "[dbo].[ShowAudit_Table]");
        }
        public static int Insert_Payment_DB(string error, string bookingid, string pgname)
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = "insert into [dbo].[PG_DB] values(" + bookingid + ",'" + error + "','" + pgname + "')";
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("insert error values into PG_DB table");
            return Connection.EXECommand(command, connMSTicket);
        }
    }
        #endregion
}