using System;
using System.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using KoDTicketing;
using KoDTicketing.BusinessLayer;

namespace KoDTicketing.DataAccessLayer
{
    /// <summary>
    /// Summary description for VistaDAL
    /// </summary>
    public class VistaDAL : DBAccess
    {
        VistaDAL()
        {
        }
        // Event mailer tracker
        public static DataTable _Write_Event(string ip,string date,string ev)
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = "INSERT INTO [dbo].[EventMailerTracker] (Ip,Date,Event) VALUES ('" + ip + "','" + date + "','" + ev + "')";
            return Connection.readTab(command, connMSTicket);
        }
        //end
        public static DataTable _Select_classcode(string SeatNo, string showDate, string ShowTime)
        {
            return Connection.readTab("Select [Class Code] from " + table_BookingMaster + " where  KeyNo in(" + SeatNo + ")" +
                "and CONVERT(date,Date)= CONVERT(Date,'" + showDate + "')" +
                " and CONVERT(time(0), [Start Time]) =CONVERT(time(0),'" + ShowTime + "')", connWebBooking);
        }
        public static DataTable _Select_Time_Api(string TransectionId)
        {
            return Connection.readTab("Select top 1 [Booking Start Date_Time] from " + table_BookingMaster + " where  [Agent Code]='"+TransectionId+"'", connWebBooking);
        }
        public static DataTable _Select_Play()
        {
            return Connection.readTab("select Distinct([Show Code]) from " + table_AudiShowAllocation +
                                    " (UPDLOCK) where  convert(date,Date) >= convert(date,GETDATE()) and [Show Closed]=0", connWebBooking);
        }
        public static DataTable _Select_AuditPlay()
        {
            return Connection.readTab("select Distinct([Show Code]) from " + table_AudiShowAllocation, connWebBooking);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Play"></param>
        /// <returns></returns>
        public static DataTable _Select_Audi(string Play)
        {
            return Connection.readTab("Select [Audi No_],[Description] from " + table_AudiMaster + " where Status = 1 and " +
                " [Audi No_] in (Select distinct([Audi No_]) from " +
                table_AudiShowAllocation +
                    " where [Show Code]='" + Play + "')", connWebBooking);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Location"></param>
        /// <param name="filmCode"></param>
        /// <returns></returns>
        public static DataTable _Select_PlayDate(string Location, string filmCode)
        {
            string sqlQuery = "select Distinct(Convert(nvarchar,Date,102)) as [Date] from " + table_AudiShowAllocation
                + " (UPDLOCK) where [Audi No_] = '" + Location + "' and  [Show Code] = '" + filmCode + "'   and Convert(datetime,[Booking End Date]) >= Convert(datetime, getdate()-1) "
                + " and Convert(datetime,[Booking Start Time]) <= Convert(datetime, getdate()) and [Show Closed] = 0 ";

            return Connection.readTab(sqlQuery, connWebBooking);
        }
        public static DataTable _Select_AuditPlayDate(string Location, string filmCode)
        {
            string sqlQuery = "select Distinct(Convert(nvarchar,Date,102)) as [Date] from " + table_AudiShowAllocation
                + " (UPDLOCK) where [Audi No_] = '" + Location + "' and  [Show Code] = '" + filmCode + "'";

            return Connection.readTab(sqlQuery, connWebBooking);
        }

        ///
        public static DataTable _Select_PlayTime(String Location, String filmCode, String PlayDate)
        {
            string sqlQuery = "Select Convert(nvarchar,[Start Time],108)as [ShowTime], [Show Allocation ID] from " + table_AudiShowAllocation +
            " (UPDLOCK) where  [Audi No_] = '" + Location + "' and [Show Code] = '" + filmCode + "' " +
            " and  Convert(nvarchar,[Date],103) = Convert(varchar,'" + PlayDate + "',103) and [Show Closed] = 0"+
            " and Convert(date,[Web Booking End Date]) > Convert(date, getdate())"+
            " or [Audi No_] = '" + Location + "' and [Show Code] = '" + filmCode + "' and  Convert(nvarchar,[Date],103) = Convert(varchar,'" + PlayDate + "',103) and [Show Closed] = 0"+
            " and ((Convert(date,[Web Booking End Date]) = Convert(date, getdate())) and Convert(time,[Start Time]) >= Convert(time, getdate()))";
            return Connection.readTab(sqlQuery, connWebBooking);
        }
        public static DataTable _Select_PlayTime_Audit(String Location, String filmCode, String PlayDate)
        {
            string sqlQuery = "Select Convert(nvarchar,[Start Time],108)as [ShowTime], [Show Allocation ID] from " + table_AudiShowAllocation +
            " (UPDLOCK) where  [Audi No_] = '" + Location + "' and [Show Code] = '" + filmCode + "' " +
            " and  Convert(nvarchar,[Date],103) = Convert(varchar,'" + PlayDate + "',103) and [Show Closed] = 0" +
            " or [Audi No_] = '" + Location + "' and [Show Code] = '" + filmCode + "' and  Convert(nvarchar,[Date],103) = Convert(varchar,'" + PlayDate + "',103) and [Show Closed] = 0";            
            return Connection.readTab(sqlQuery, connWebBooking);
        }
        public static DataTable _Select_PlayTime_AuditReport(String Location, String filmCode, String PlayDate)
        {
            string sqlQuery = "Select Convert(nvarchar,[Start Time],108)as [ShowTime], [Show Allocation ID] from " + table_AudiShowAllocation +
            " (UPDLOCK) where  [Audi No_] = '" + Location + "' and [Show Code] = '" + filmCode + "' " +
            " and  Convert(nvarchar,[Date],103) = Convert(varchar,'" + PlayDate + "',103) and [Show Closed] = 0" +
            " or [Audi No_] = '" + Location + "' and [Show Code] = '" + filmCode + "' and  Convert(nvarchar,[Date],103) = Convert(varchar,'" + PlayDate + "',103) and [Show Closed] = 0";
            return Connection.readTab(sqlQuery, connWebBooking);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public static DataTable _Select_Category(String filmCode)
        {
            string sqlQuery = "Select Distinct(a.[Class Code]), b.[Description], a.[Show Price] from  " + table_ShowPricing + " a, " + table_ClassMaster + " b";
            sqlQuery += " (UPDLOCK) where a.[Class Code] = b.[Class Code] and [Show Allocation ID]= '" + filmCode + "' and a.[Show Price] <>0  order by [Show Price]";

            return Connection.readTab(sqlQuery, connWebBooking);
        }

        public static DataSet _Select_Category_DS(String filmCode)
        {
            //filter duplicate Prices
            //for live
            //string sqlQuery = "WITH CTE as(SELECT	ROW_NUMBER() OVER(PARTITION BY [Class Code] order by [Starting Date] desc) AS RowID,*"
            //   + "FROM " + table_ShowPricing + " as ShowPrice Where [Show Allocation ID]= '" + filmCode +
            //   "' )select ShowPrice.[Class Code], ClassMaster1.Description,ShowPrice.[Show Price] FROM CTE"
            //   + " AS ShowPrice INNER JOIN " + table_ClassMaster + " AS ClassMaster1 ON"
            //   + " ShowPrice.[Class Code] = ClassMaster1.[Class Code] WHERE RowID=1 and ClassMaster1.[Secondary Category]='" + 0 + "' order by [Show Price]";
            //for local
            string sqlQuery = "WITH CTE as(SELECT	ROW_NUMBER() OVER(PARTITION BY [Class Code] order by [Starting Date] desc) AS RowID,*"
                + "FROM " + table_ShowPricing + " as ShowPrice Where [Show Allocation ID]= '" + filmCode +
                "' )select ShowPrice.[Class Code], ClassMaster1.Description,ShowPrice.[Show Price] FROM CTE"
                + " AS ShowPrice INNER JOIN " + table_ClassMaster + " AS ClassMaster1 ON"
                + " ShowPrice.[Class Code] = ClassMaster1.[Class Code] WHERE RowID=1 order by [Show Price]";

            return Connection.readDataSet(sqlQuery, connWebBooking);
        }

        public static DataTable _Select_Available_Seats(String Category, String filmCode)
        {
            try
            {
                System.Text.StringBuilder sqlQuery = new System.Text.StringBuilder("Select count([Booking ID]) from " + table_BookingMaster + " (UPDLOCK) where ");
                sqlQuery.Append("[Show Allocation ID]='" + filmCode + "' and [Class Code] ='" + Category + "' and ");
                sqlQuery.Append("[Booked] = 0 and [Lock For Booking] = 0 and [Block For Agent] = 0 and [Blocked] = 0  ");
                DataTable dt = Connection.readTab(sqlQuery.ToString(), connWebBooking);
                return dt;
            }
            catch (InvalidOperationException ex)
            {
                throw new Exception("Unable to fetch available seats --> " + ex.Message);
            }
            catch (SqlException ex)
            {
                throw new Exception("Unable to fetch available seats --> " + ex.Message);
            }
        }

        #region API_Methods

        /// <summary>
        /// DataSet returns the list of Audi in First Table
        /// </summary>
        /// <returns></returns>
        //Select Location for the Audi
        public static DataSet SelectAudi()
        {
            try
            {
                //return _Select_Audi("ZANGOORA");
                return Connection.readDataSet("Select * from " + table_AudiMaster + " where Status = 1 and [Audi No_] in (Select distinct([Audi No_]) from "
                   + table_AudiShowAllocation + " where convert(date,Date) >= convert(date,GETDATE()) and [Show Closed]=0 and [Show Code] != 'JHUMROO')", connWebBooking);
            }
            catch (Exception ex)
            {
                //Log Message
                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Error in SelectAudi: " + ex.Message);
            }
            return null;
        }

        /// <summary>
        /// DataSet returns the list of Audi in First Table
        /// </summary>
        /// <returns></returns>
        //Select Location for the Audi
        public static DataSet SelectAudiByPlay(string ShowCode)
        {
            try
            {
                return Connection.readDataSet("Select [Audi No_],[Description] from [NMLIVEDB].[dbo].[Great Indian Nautanki Company$Audi Master] where Status = 1 and " +
                " [Audi No_] in (Select distinct([Audi No_]) from" +
                " [NMLIVEDB].[dbo].[Great Indian Nautanki Company$Audi Show Allocation] " +
                    " where [Show Code]='" + ShowCode + "')", connWebBooking);
            }
            catch (Exception ex)
            {
                //Log Message
                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Error in SelectAudiByPlay: " + ex.Message);
            }
            return null;
        }

        /// <summary>
        /// DataSet returns the list of Plays in First Table
        /// </summary>
        /// <returns></returns>
        //Select Plays
        public static DataSet SelectPlay()
        {
            try
            {
                return Connection.readDataSet("select Distinct([Show Code]) from " + table_AudiShowAllocation +
                                                " (UPDLOCK) where  convert(date,Date) >= convert(date,GETDATE()) and [Show Closed]=0", connWebBooking);
            }
            catch (Exception ex)
            {
                //Log Message
                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Error in MaxRowColumn: " + ex.Message);
            }
            return null;
            //return dbCon.readTab("select Distinct([Show Code]) from " + table_AudiShowAllocation + " (UPDLOCK) where [Audi No_] ='" + xbol.Location + "'", conn2);
        }

        /// <summary>
        /// It returns the DataSet with the List Of Shows in First Table
        /// </summary>
        /// <param name="Location"></param>
        /// <returns></returns>
        //Select Play/Show from the Location
        public static DataSet SelectShow(string Location)
        {
            try
            {

                return Connection.readDataSet("select Distinct([Show Code]) as ShowCode from " + table_AudiShowAllocation + " where [Audi No_] ='" + Location + "'", connWebBooking);

            }
            catch (Exception ex)
            {
                //Log Message
                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Error in SelectShow: " + ex.Message);
            }
            return null;
        }

        /// <summary>
        /// It returns the DataSet with the List Of ShowsDate in First Table
        /// </summary>
        /// <param name="Location"></param>
        /// <param name="ShowCode"></param>
        /// <returns></returns>
        //Select Play/Show Dates
        public static DataSet SelectPlayDate(String Location, String ShowCode)
        {
            try
            {
                string sqlQuery = "select Distinct(Convert(nvarchar,Date,107)) as [Date] from " + table_AudiShowAllocation
                + " where [Audi No_] = '" + Location + "' and  [Show Code] = '" + ShowCode + "'   and Convert(datetime,[Booking End Date]) >= Convert(datetime, getdate()-1) "
                + " and Convert(datetime,[Booking Start Time]) <= Convert(datetime, getdate()) and [Show Closed] = 0 and [Date] !='12/31/2011' ";

                return Connection.readDataSet(sqlQuery, connWebBooking);

            }
            catch (Exception ex)
            {
                //Log Message
                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Error in SelectPlayDate: " + ex.Message);
            }
            return null;
        }

        /// <summary>
        /// It returns the DataSet with the List Of Show Time for Show in First Table
        /// </summary>
        /// <param name="Location"></param>
        /// <param name="ShowCode"></param>
        /// <param name="ShowDate"></param>
        /// <returns></returns>
        //Select Play/Show Time 
        public static DataSet SelectPlayTime(String Location, String ShowCode, String ShowDate)
        {
            ShowDate = DateTime.Parse(ShowDate, System.Globalization.CultureInfo.CreateSpecificCulture("en-IN")).ToString("dd/MM/yyyy");
            string sqlQuery = "Select Convert(nvarchar,[Start Time],108)as [ShowTime], [Show Allocation ID] from " + table_AudiShowAllocation +
                        " (UPDLOCK) where  [Audi No_] = '" + Location + "' and [Show Code] = '" + ShowCode + "' " +
                        " and  Convert(nvarchar,[Date],103) = Convert(nvarchar, '" + ShowDate + "',103) and [Show Closed] = 0";

            return Connection.readDataSet(sqlQuery, connWebBooking);
        }

        //Select Category
        public static DataSet SelectCategory(String ShowAllocationID)
        {
            //sqlQuery = "SELECT TOP (Select COUNT(distinct([Class Code])) FROM " + table_ShowPricing + " where [Show Allocation ID]= '" + ShowAllocationID + "' and [Class Code]!='') ShowPrice.[Class Code], ClassMaster1.Description," +
            //  " ShowPrice.[Show Price] FROM " + table_ShowPricing + " AS ShowPrice INNER JOIN " +
            //  " " + table_ClassMaster + " AS ClassMaster1 ON ShowPrice.[Class Code] = ClassMaster1.[Class Code] " +
            //  " WHERE (ShowPrice.[Show Allocation ID] = '" + ShowAllocationID + "') ORDER BY ShowPrice.[Starting Date] DESC, ShowPrice.[Show Price]";

            string sqlQuery = "WITH CTE as(SELECT	ROW_NUMBER() OVER(PARTITION BY [Class Code] order by [Starting Date] desc) AS RowID,*"
               + "FROM " + table_ShowPricing + " as ShowPrice Where [Show Allocation ID]= '" + ShowAllocationID +
               "'  And [Class Code]!='GLY' )select ShowPrice.[Class Code], ClassMaster1.Description,ShowPrice.[Show Price] FROM CTE"
               + " AS ShowPrice INNER JOIN " + table_ClassMaster + " AS ClassMaster1 ON"
               + " ShowPrice.[Class Code] = ClassMaster1.[Class Code] WHERE RowID=1 and ClassMaster1.[Secondary Category]='" + 0 + "' order by [Show Price]";
            return Connection.readDataSet(sqlQuery, connWebBooking);

        }

        //Select Available Seats
        public static int SelectAvailableSeats(String ShowAllocationID, String Location)
        {

            string sqlQuery = "Select count([Booking ID]) from " + table_BookingMaster + " (UPDLOCK) where ";
            sqlQuery += "[Show Allocation ID]='" + ShowAllocationID + "' and [Class Code] ='" + Location + "' and ";
            sqlQuery += "[Booked] = 0 and [Lock For Booking] = 0 and [Block For Agent] = 0 and [Blocked] = 0  ";
            return int.Parse(Connection.readDataSet(sqlQuery, connWebBooking).Tables[0].Rows[0][0].ToString());

        }

        #endregion

        #region -- Code for Seat Layout Page
        //Select Row Audi Wise
        public static DataSet SelectRowAudiWise(string audiNo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandText = "Select * from " + table_AudiRowMaster + " where  [Audi No_] = '" + audiNo + "' ";
                return Connection.readDataSet(command, connWebBooking);

            }
            catch (Exception ex)
            {
                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Error in SelectRowAudiWise: " + ex.Message);
            }
            return null;
        }

        /// <summary>
        /// Maximum Rows and Columns in the Show
        /// </summary>
        /// <param name="audiNo"></param>
        /// <returns></returns>
        public static DataSet MaxRowColumn(string audiNo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandText = "SELECT [Store No_],[Audi No_]  ,[Description]  ,[Max Rows] as MaxRow  ,[Max Columns] as MaxColumn  ,[Status]  FROM " + table_AudiMaster + "  Where " +
                " [Audi No_]= '" + audiNo + "'";
                return Connection.readDataSet(command, connWebBooking);
            }
            catch (Exception ex)
            {
                //Log Message
                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Error in MaxRowColumn: " + ex.Message);
            }
            return null;
        }

        /// <summary>
        /// Maximum Rows in a Show
        /// </summary>
        /// <param name="audiNo"></param>
        /// <returns></returns>
        public static int MaxRows(string audiNo)
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = "SELECT [Store No_],[Audi No_]  ,[Description]  ,[Max Rows] as MaxRow  ,[Max Columns] as MaxColumn  ,[Status]  FROM " + table_AudiMaster + "  Where " +
            " [Audi No_]= '" + audiNo + "'";
            Connection.readTab(command, connWebBooking);
            return int.Parse(Connection.readTab(command, connWebBooking).Rows[0]["MaxRow"].ToString());
        }

        /// <summary>
        /// Maximum Columns in a Show
        /// </summary>
        /// <param name="audiNo"></param>
        /// <returns></returns>
        public static int MaxColumns(string audiNo)
        {

            SqlCommand command = new SqlCommand();
            command.CommandText = "SELECT [Store No_],[Audi No_]  ,[Description]  ,[Max Rows] as MaxRow  ,[Max Columns] as MaxColumn  ,[Status]  FROM " + table_AudiMaster + "  Where " +
            " [Audi No_]= '" + audiNo + "'";
            Connection.readTab(command, connWebBooking);
            return int.Parse(Connection.readTab(command, connWebBooking).Rows[0]["MaxColumn"].ToString());
        }

        /// <summary>
        /// Select all the Seats of the Audi Show Wise
        /// </summary>
        /// <param name="AudiNo"></param>
        /// <returns></returns>
        public static DataSet SelectAllSeats(string ShowAllocationID)
        {

            SqlCommand command = new SqlCommand();
            command.CommandText = "SELECT b.[Booking ID],b.[Show Allocation ID],b.[KeyNo],b.[Cell Type],b.[Row No],b.[Column No],b.[Class Code]," +
                " b.[Description],b.[Show Price] ,b.[Booked],b.[Blocked],b.[Lock For Booking],b.[Block For Agent],a.Description" +
                " from " + table_BookingMaster + " b left join " + table_ClassMaster + " a" +
                " on b.[Class Code]=a.[Class Code] where  b.[Show Allocation ID]= '" + ShowAllocationID + "' order by [Booking ID]";
            return Connection.readDataSet(command, connWebBooking);

        }
        #endregion

        #region Promostion Code
        public static DataTable GetPromostionCode()
        {
            return Connection.readTab("SELECT [Promotion Code],[Start Date] ,[End date] ,[Discount %],[RegEx],[CO],[DM],[BZ],[PL],[SL],[GL] from " + table_PromotionMaster +
                                     " (UPDLOCK) WHERE GETDATE () BETWEEN  [Start Date] AND [End date]", connWebBooking);
        }
        #endregion

        #region Promostion Code
        public static DataTable GetAllPromostionCode()
        {
            return Connection.readTab("SELECT [Promotion Code],[Start Date] ,[End date] ,[Discount %],[RegEx],[CO],[DM],[BZ],[PL],[SL],[GL] from " + table_PromotionMaster, connWebBooking);
        }
        #endregion


        internal static DataTable _Select_Play(string Location, string filmCode, string PlayDate)
        {
         string sqlQuery =  "Select Convert(nvarchar,[Start Time],108)as [ShowTime], [Show Allocation ID] from " + table_AudiShowAllocation +
                              " (UPDLOCK) where  [Audi No_] = '" + Location + "' " +
                               " and  Convert(nvarchar,[Date],103) = Convert(varchar,'" + PlayDate + "',103) and [Show Closed] = 0"+
                                " and Convert(date,[Web Booking End Date]) > Convert(date, getdate())"+
                                 " or [Audi No_] = '" + Location + "' and  Convert(nvarchar,[Date],103) = Convert(varchar,'" + PlayDate + "',103) and [Show Closed] = 0" +
                              " and ((Convert(date,[Web Booking End Date]) = Convert(date, getdate())) and Convert(time,[Start Time]) >= Convert(time, getdate()))";
                           return Connection.readTab(sqlQuery, connWebBooking);


        }

        internal static DataTable _Select_Category_Price(string filmcode, string category)
        {
            string sqlQuery = "Select [Show Price] from " + table_ShowPricing;
            sqlQuery += " (UPDLOCK) where [Show Allocation ID]= '" + filmcode + "' and [Show Price] <>0 and [Class Code] = '" + category + "'";

            return Connection.readTab(sqlQuery, connWebBooking);

        }
        public static DataTable _Select_Newyear_RoyalInfo(string info)
        {
            return Connection.readTab("select * from [NMLIVEDB].[dbo].[Great Indian Nautanki Company$Contact] "+
                                    "where  No_='"+info+"'or [Mobile Phone No_]='"+info+"'", connWebBooking);
        }
       
    }

}