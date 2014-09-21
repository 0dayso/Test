using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace KoDTicketing.DataAccessLayer
{

    public class DBAccess : IDisposable
    {
        //Live DB Changes
        //const string ConnectionStringMSTicket = "server=RAHULZWORLD\\RRZSQLR2;database=MSTicketDB_Live;uid=ticketAdmin;pwd=a2FS6CPm;";
        ////const string ConnectionStringMSTicket = "Server={0};Database=MsticketDB_Live;User ID=ticketAdmin;Password=a2FS6CPm";
        //const string ConnectionStringWebCRM = "server=182.71.214.114,1348;database=webcrm;uid=sancrm;pwd=K0d@2012;";
        //const string ConnectionStringWebBooking = "server=122.180.79.187;database=BOXOTEST;uid=WEBBOOKING;pwd=K1ng@123; Min Pool Size=0; Max Pool Size=100; Connect Timeout=600;Pooling=false;Connection Reset=true;";
        //const string ConnectionStringSMSdB = "server=bsql.securehostdns.com,1533;database=SMSKingdomdb;uid=smskod;pwd=sms124@KOD; Min Pool Size=0; Max Pool Size=100; Connect Timeout=600;Pooling=false;Connection Reset=true;";


        //These to be removed
        //const string ConnectionStringMSTicket = "server=biztrack.co.in;database=MSTicketDB_Live_Latest;uid=sa;pwd=kranti123@123;";
        const string ConnectionStringMSTicket = "Server={0};Database=MsticketDB_Live;User ID=ticketAdmin;Password=a2FS6CPm";
        const string ConnectionStringWebCRM = "server=182.71.214.114,1348;database=webcrm;uid=sancrm;pwd=K0d@2012;";
        //const string ConnectionStringWebBooking = "server=122.180.79.189;database=NMLIVEDB;uid=WEB;pwd=K1ng@123; Min Pool Size=0; Max Pool Size=100; Connect Timeout=600;Pooling=false;Connection Reset=true;";
        const string ConnectionStringWebBooking = "server=122.180.79.187;database=NMLIVEDB;uid=WEBBOOKING;pwd=Amus1c@lc0m3dy0n$t@g3; Min Pool Size=0; Max Pool Size=100; Connect Timeout=600;Pooling=false;Connection Reset=true;";
        const string ConnectionStringSMSdB = "server=bsql.securehostdns.com,1533;database=SMSKingdomdb;uid=smskod;pwd=sms124@KOD; Min Pool Size=0; Max Pool Size=100; Connect Timeout=600;Pooling=false;Connection Reset=true;";



        static SqlConnection _connMSTicket;
        static SqlConnection _connWebBooking;
        static SqlConnection _connWebCRM;
        static SqlConnection _connSMSdB;




        /*Live Tables*/
        static String _table_AudiMaster = "[NMLIVEDB].[dbo].[Great Indian Nautanki Company$Audi Master]";
        static String _table_AudiShowAllocation = "[NMLIVEDB].[dbo].[Great Indian Nautanki Company$Audi Show Allocation]";
        static String _table_ClassMaster = "[NMLIVEDB].[dbo].[Great Indian Nautanki Company$Class Master]";
        static String _table_BookingMaster = "[NMLIVEDB].[dbo].[Great Indian Nautanki Company$Booking Master]";
        static String _table_AudiRowMaster = "[NMLIVEDB].[dbo].[Great Indian Nautanki Company$Audi Row Master]";
        static String _table_ShowPricing = "[NMLIVEDB].[dbo].[Great Indian Nautanki Company$Show Pricing]";
        static String _table_PromotionMaster = "[NMLIVEDB].[dbo].[Great Indian Nautanki Company$Promotion Master]";

        //These tables to be removed
        //static String _table_AudiMaster = "[NMTESTING].[dbo].[NMTESTING$Audi Master]";
        //static String _table_AudiShowAllocation = "[NMTESTING].[dbo].[NMTESTING$Audi Show Allocation]";
        //static String _table_ClassMaster = "[NMTESTING].[dbo].[NMTESTING$Class Master]";
        //static String _table_BookingMaster = "[NMTESTING].[dbo].[NMTESTING$Booking Master]";
        //static String _table_AudiRowMaster = "[NMTESTING].[dbo].[NMTESTING$Audi Row Master]";
        //static String _table_ShowPricing = "[NMTESTING].[dbo].[NMTESTING$Show Pricing]";
        //static String _table_PromotionMaster = "[NMTESTING].[dbo].[NMTESTING$Promotion Master]";
       



        public static SqlConnection connMSTicket { get { return _connMSTicket ?? new SqlConnection(string.Format(ConnectionStringMSTicket, System.Environment.MachineName)); } }
        public static SqlConnection connWebBooking { get { return _connWebBooking ?? new SqlConnection(ConnectionStringWebBooking); } }
        public static SqlConnection connSMSdB { get { return _connSMSdB ?? new SqlConnection(ConnectionStringSMSdB); } }
        public static SqlConnection connWebCRM { get { return _connWebCRM ?? new SqlConnection(ConnectionStringWebCRM); } }

        protected static String table_AudiMaster { get { return _table_AudiMaster; } }
        protected static String table_AudiShowAllocation { get { return _table_AudiShowAllocation; } }
        protected static String table_ClassMaster { get { return _table_ClassMaster; } }
        protected static String table_BookingMaster { get { return _table_BookingMaster; } }
        protected static String table_AudiRowMaster { get { return _table_AudiRowMaster; } }
        protected static String table_ShowPricing { get { return _table_ShowPricing; } }

        #region Promostion Code

        protected static String table_PromotionMaster { get { return _table_PromotionMaster; } }
        #endregion

        public DBAccess()
        {
            _connMSTicket = new SqlConnection(ConnectionStringMSTicket);
            _connWebBooking = new SqlConnection(ConnectionStringWebBooking);
            _connSMSdB = new SqlConnection(ConnectionStringSMSdB);
            _connWebCRM = new SqlConnection(ConnectionStringWebCRM);
        }

        #region -- Transaction Status
        /// <summary>
        /// Fields Used in order  -- reference id, description, status, booking id
        /// </summary>
        /// <param name="Values"></param>
        public static void LogEntry(params string[] Values)
        {
            SqlCommand command = new SqlCommand();
            //Fields Used  -- reference id, description, status, booking id

            String sqlQuery = "Insert into GINC$BookingStatus values('" + Values[0].Replace("'", "") + "','" + Values[1].Replace("'", "") + "'," +
             Values[2].Replace("'", "") + ",getdate(),'" + Values[3].Replace("'", "") + "')";
            if (Values[2].ToString() == "16")
                sqlQuery += " Update [GINC$BookingTransaction_temp] set ReferenceNo = '" + Values[0].Replace("'", "") + "', Status = 1 where BookingID = " +
                    Values[3].ToString().Replace("'", "");

            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write(sqlQuery);
            command.CommandText = sqlQuery;
            Connection.EXECommand(command, connMSTicket);
        }

        #endregion

        public void Dispose()
        {
        }
    }
}
