using System;
using System.Data;
using System.IO;
using KoDTicketing.BusinessLayer;
using KoDTicketing.DataAccessLayer;

namespace KoDTicketing
{
    enum SeatLayoutColumnNames { };
    /// <summary>
    /// Summary description for GTICK
    /// </summary>
    /// 
    public class GTICKV
    {
        private GTICKV()
        {
            //_ticket.filmCode = ShowID;
            //_ticket.AudiNo = ShowAllocationID;
        }

        /// Maximum Rows in a Show
        public static int maxRows(String _filmCode)
        {
            return int.Parse(GTICKBOL.MaxColumn(_filmCode).Rows[0]["MaxRow"].ToString());
        }

        /// Maximum Columns in a Show
        public static int maxColumns(String _filmCode)
        {
            return int.Parse(GTICKBOL.MaxColumn(_filmCode).Rows[0]["MaxColumn"].ToString());
        }

        /// All Seats in a Show
        public static DataTable AllSeats(String _AudiNo)
        {
            return GTICKBOL.AllSeats(_AudiNo);
        }
        public static DataTable Audit_AllSeats(String _AudiNo)
        {
            return GTICKBOL.Audit_AllSeats(_AudiNo);
        }
        public static DataTable AuditSeatsReport(String _AudiNo,String _ShowDate)
        {
            return GTICKBOL.AuditSeatsReport(_AudiNo,_ShowDate);
        }
        
        public static DataTable SelectRow_AudiWise(String _filmCode)
        {
            return GTICKBOL.SelectRow_AudiWise(_filmCode);
        }

        public static DataTable select_Seat_Layout(String _SeatNo)
        {
            return GTICKBOL.select_Seat_Layout(_SeatNo);
        }

        #region -- Transaction Status
        /// <summary>
        /// Fields Used in order  -- reference id, description, status, booking id
        /// </summary>
        /// <param name="Values"></param>
        public static void LogEntry(params string[] Values)
        {           
            Connection.LogEntry(Values);
        }

        #endregion
    }
}