using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Configuration;
using KoDTicketing.BusinessLayer;
using KoDTicketing.DataAccessLayer;
using System.Linq;

[WebService(Namespace = "http://msticket.kingdomofdreams.in/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line 
[System.Web.Script.Services.ScriptService]
public class ShowInfo : System.Web.Services.WebService
{
    public Security objSecurity;

    public ShowInfo()
    {

    }

    #region -- Code for Seat Selection Page

    /// <summary>
    /// Maximum Rows and Columns in the Show
    /// </summary>
    /// <param name="audiNo"></param>
    /// <returns></returns>
    [WebMethod]
    [SoapHeader("objSecurity")]
    public DataSet MaxRowColumn(string audiNo)
    {
        try
        {
            if (SecurityHeaderValidation.Validate(objSecurity))
            {
                return VistaDAL.MaxRowColumn(audiNo);
            }
        }
        catch (Exception ex)
        {
            //Log Message
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write(ex.Message);
        }
        return null;
    }

    /// <summary>
    /// DataSet returns the list of Audi in First Table
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    [SoapHeader("objSecurity")]
    public DataSet SelectAudi()
    {
        try
        {
            if (SecurityHeaderValidation.Validate(objSecurity))
            {
                return VistaDAL.SelectAudi();
            }
        }
        catch (Exception ex)
        {
            //Log Message
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write(ex.Message);
        }
        return null;
    }

        /// <summary>
    /// DataSet returns the list of Audi in First Table
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    [SoapHeader("objSecurity")]
    public DataSet SelectAudiByPlay(string ShowCode)
    {
        try
        {
            if (SecurityHeaderValidation.Validate(objSecurity))
            {
                return VistaDAL.SelectAudiByPlay(ShowCode);
            }
        }
        catch (Exception ex)
        {
            //Log Message
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write(ex.Message);
        }
        return null;
    }



    /// <summary>
    /// DataSet returns the list of Plays in First Table
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    [SoapHeader("objSecurity")]
    public DataSet SelectPlay()
    {
        try
        {
            if (SecurityHeaderValidation.Validate(objSecurity))
            {
                DataSet ds = VistaDAL.SelectPlay();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    //foreach (DataRow dr in ds.Tables[0].Rows)
                    //{
                    //    if (!dr[0].ToString().Equals("ZANGOORA"))
                    //    {
                    //        dr.Delete();
                    //    }
                    //}
                    ds.AcceptChanges();
                    return ds;
                }
            }
        }
        catch (Exception ex)
        {
            //Log Message
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write(ex.Message);
        }
        return null;
    }

    /// <summary>
    /// It returns the DataSet with the List Of Shows in First Table
    /// </summary>
    /// <param name="Location"></param>
    /// <returns></returns>
    [WebMethod]
    [SoapHeader("objSecurity")]
    public DataSet SelectShow(string Location)
    {
        try
        {
            if (SecurityHeaderValidation.Validate(objSecurity))
            {
                return VistaDAL.SelectShow(Location);
            }
        }
        catch (Exception ex)
        {
            //Log Message
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write(ex.Message);
        }
        return null;
    }

    /// <summary>
    /// It returns the DataSet with the List Of ShowsDate in First Table
    /// </summary>
    /// <param name="Location"></param>
    /// <param name="ShowCode"></param>
    /// <returns></returns>
    [WebMethod]
    [SoapHeader("objSecurity")]
    public DataSet SelectPlayDate(String Location, String ShowCode)
    {
        try
        {
            if (SecurityHeaderValidation.Validate(objSecurity))
            {
                return VistaDAL.SelectPlayDate(Location, ShowCode);
            }
        }
        catch (Exception ex)
        {
            //Log Message
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write(ex.Message);
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
    [WebMethod]
    [SoapHeader("objSecurity")]
    public DataSet SelectPlayTime(String Location, String ShowCode, String ShowDate)
    {
        try
        {
            if (SecurityHeaderValidation.Validate(objSecurity))
            {
                return VistaDAL.SelectPlayTime(Location, ShowCode, ShowDate);
            }
        }
        catch (Exception ex)
        {
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write(ex.Message);
        }
        return null;
    }

    [WebMethod]
    [SoapHeader("objSecurity")]
    public DataSet SelectCategory(String ShowAllocationID)
    {
        try
        {
            if (SecurityHeaderValidation.Validate(objSecurity))
            {
                return VistaDAL.SelectCategory(ShowAllocationID);
            }
        }
        catch (Exception ex)
        {
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write(ex.Message);
        }
        return null;
    }

    [WebMethod]
    [SoapHeader("objSecurity")]
    public int SelectAvailableSeats(String ShowAllocationID, String Location)
    {
        try
        {
            if (SecurityHeaderValidation.Validate(objSecurity))
            {
                return VistaDAL.SelectAvailableSeats(ShowAllocationID, Location);
            }
        }
        catch (Exception ex)
        {
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write(ex.Message);
        }
        return 0;
    }

    #endregion



    #region -- Code for Seat Layout Page

    [WebMethod]
    [SoapHeader("objSecurity")]
    public DataSet SelectRowAudiWise(string audiNo)
    {
        try
        {
            if (SecurityHeaderValidation.Validate(objSecurity))
            {
                return VistaDAL.SelectRowAudiWise(audiNo);
            }
        }
        catch (Exception ex)
        {
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write(ex.Message);
        }
        return null;
    }

    /// <summary>
    /// Maximum Rows in a Show
    /// </summary>
    /// <param name="audiNo"></param>
    /// <returns></returns>
    [WebMethod]
    [SoapHeader("objSecurity")]
    public int MaxRows(string audiNo)
    {
        try
        {
            if (SecurityHeaderValidation.Validate(objSecurity))
            {
                return VistaDAL.MaxRows(audiNo);
            }
        }
        catch (Exception ex)
        {
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write(ex.Message);
        }
        return 0;
    }

    /// <summary>
    /// Maximum Columns in a Show
    /// </summary>
    /// <param name="audiNo"></param>
    /// <returns></returns>
    [WebMethod]
    [SoapHeader("objSecurity")]
    public int MaxColumns(string audiNo)
    {
        try
        {
            if (SecurityHeaderValidation.Validate(objSecurity))
            {
                return VistaDAL.MaxColumns(audiNo);
            }
        }
        catch (Exception ex)
        {
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write(ex.Message);
        }
        return 0;
    }

    /// <summary>
    /// Select all the Seats of the Audi Show Wise
    /// </summary>
    /// <param name="AudiNo"></param>
    /// <returns></returns>
    [WebMethod]
    [SoapHeader("objSecurity")]
    public DataSet SelectAllSeats(string ShowAllocationID)
    {
        try
        {
            if (SecurityHeaderValidation.Validate(objSecurity))
            {
                return VistaDAL.SelectAllSeats(ShowAllocationID);
            }
        }
        catch (Exception ex)
        {
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write(ex.Message);
        }
        return null;
    }

    [WebMethod]
    [SoapHeader("objSecurity")]
    public DataSet CheckSeatsAndLock(string SeatNo, string showDate, string ShowTime, long TransactionID)
    {
        try
        {
            if (SecurityHeaderValidation.Validate(objSecurity))
            {
                DataTable classcode = VistaDAL._Select_classcode(SeatNo, showDate, ShowTime);//this functon select all the categories of the seats, selected by the user.
                string classcodestring = "";
                for (int i = 0; i < classcode.Rows.Count; i++)
                {
                    classcodestring = classcodestring + classcode.Rows[i]["class Code"].ToString() + ",";
                }
                classcodestring = classcodestring.Remove(classcodestring.Length - 1);
                string[] classcodearray = classcodestring.Split(',');
                var distinctclasscode = (from w in classcodearray     //decide whether categories are same or not
                                     select w).Distinct().ToArray();
                
                string[] SeatKeyNo = SeatNo.Split(',');
                var distinctkeyno = (from w in SeatKeyNo                //decide whether key no are repitative or not
                                     select w).Distinct().ToArray();
                if (SeatKeyNo.Length == distinctkeyno.Length && distinctclasscode.Length==1)
                {
                    return GTICKDAL.CheckSeatsAndLock(TransactionID, SeatNo, showDate, ShowTime);
                }
                else
                {
                    DataSet dsexcep = new DataSet();
                    DataTable dtexcep = new DataTable();
                    dtexcep.Columns.Add("Column1", typeof(int));
                    dtexcep.Rows.Add(0);
                    dsexcep.Tables.Add(dtexcep);
                    return dsexcep;
                }
            }
        }
        catch (Exception ex)
        {
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write(ex.Message);
        }
        return null;
    }
    #endregion

    #region --Transactions

    /// <summary>
    /// Find Maximum Transaction No For the Trasactions
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    [SoapHeader("objSecurity")]
    public long TransactionCounterMax()
    {
        try
        {
            if (SecurityHeaderValidation.Validate(objSecurity))
            {
                return GTICKDAL._TransactionCounter_Max();
            }
        }
        catch (Exception ex)
        {
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write(ex.Message);
        }
        return 0;
    }

    [WebMethod]
    [SoapHeader("objSecurity")]
    public decimal GetSeatPriceSeatKeyNoWise(long TransactionID)
    {
        try
        {
            if (SecurityHeaderValidation.Validate(objSecurity))
            {
                return GTICKDAL.GetSeatPriceSeatKeyNoWise(TransactionID);
            }
        }
        catch (Exception ex)
        {
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write(ex.Message);
        }
        return 0;
    }

    [WebMethod]
    [SoapHeader("objSecurity")]
    //public DataTable FinalBooking()
    public DataSet funFinalBooking(String TransactionID, String Source, String DateOfBooking, String TimeOfBooking, String Location,
        String Play, String ShowTime, String ShowDate, String Category, String PaymentType, String AgentCode, String SeatInfo,
        String MobileNo, String EmailID, String CustomerName, int TotalSeats, decimal TotalAmount, String strPaymentReceiptNo)
    {
        try
        {
            if (SecurityHeaderValidation.Validate(objSecurity))
            {
                DataTable dt = VistaDAL._Select_Time_Api(TransactionID);
                DateTime startdatetime = Convert.ToDateTime(dt.Rows[0][0]);
                if (startdatetime.AddMinutes(25) >= DateTime.UtcNow)
                {
                    TransactionRecord _tr = new TransactionRecord();
                    _tr.BookingID = long.Parse(TransactionID);
                    _tr.Location = Location;
                    _tr.Play = Play.ToUpper();
                    _tr.ShowTime = ShowTime;
                    _tr.ShowDate = ShowDate;
                    _tr.Category = Category;
                    _tr.SeatInfo = SeatInfo;
                    _tr.TotalSeats = TotalSeats;
                    _tr.Name = CustomerName;
                    _tr.MobileNo = MobileNo;
                    _tr.EmailID = EmailID;
                    _tr.AgentCode = AgentCode;
                    _tr.ReceiptNo = strPaymentReceiptNo;
                    _tr.DateOfBooking = DateOfBooking;
                    _tr.TimeOfBooking = TimeOfBooking;
                    _tr.Source = Source;
                    _tr.TotalAmount = TotalAmount;
                    _tr.TotalSeats = TotalSeats;
                    _tr.BookingID = long.Parse(TransactionID);

                    return GTICKDAL.funFinalBooking(_tr);
                }
                else
                {
                    DataSet dsexcep = new DataSet();
                    DataTable dtexcep = new DataTable();
                    dtexcep.Columns.Add("Column1", typeof(string));
                    dtexcep.Rows.Add("Session Time Out");
                    dsexcep.Tables.Add(dtexcep);
                    return dsexcep;
                }
                
            }
        }
        catch (Exception ex)
        {
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write(ex.Message);
        }
        return null;
    }

    #endregion
}