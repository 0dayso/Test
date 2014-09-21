using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using KoDTicketing.DataAccessLayer;
using KoDTicketingLibrary.DTO;

namespace KoDTicketing.BusinessLayer
{
    /// <summary>
    /// Summary description for VistaBOL
    /// </summary>
    public class VistaBOL
    {
        //Event Event_Mailer_Tracker
        public static DataTable Event_Mailer_Tracker(string _ip,string _date,string _event)
        {
            return VistaDAL._Write_Event(_ip,_date,_event);
        }
        //end
        public static DataTable Select_Audi(String Location)
        {
            return VistaDAL._Select_Audi(Location);
        }

        public static DataTable Select_Play()
        {
            return VistaDAL._Select_Play();
        }

        public static DataTable Select_AuditPlay()
        {
            return VistaDAL._Select_AuditPlay();
        }
        public static DataTable Select_PlayDate(String Location, String filmCode)
        {
            return VistaDAL._Select_PlayDate(Location, filmCode);
        }
        public static DataTable Select_AuditPlayDate(String Location, String filmCode)
        {
            return VistaDAL._Select_AuditPlayDate(Location, filmCode);
        }

        public static DataTable Select_PlayTime(String Location, String filmCode, String PlayDate)
        {
            return VistaDAL._Select_PlayTime(Location, filmCode, PlayDate);
        }
        public static DataTable Select_PlayTime_Audit(String Location, String filmCode, String PlayDate)
        {
            return VistaDAL._Select_PlayTime_Audit(Location, filmCode, PlayDate);
        }
        public static DataTable Select_PlayTime_AuditReport(String Location, String filmCode, String PlayDate)
        {
            return VistaDAL._Select_PlayTime_AuditReport(Location, filmCode, PlayDate);
        }
        public static DataTable Select_Category(String filmCode)
        {
            return VistaDAL._Select_Category(filmCode);
        }

        public static DataSet Select_Category_DS(String filmCode)
        {
            return VistaDAL._Select_Category_DS(filmCode);
        }

        public static int Select_Available_Seats(String Category, String PlayTime)
        {
            return int.Parse(VistaDAL._Select_Available_Seats(Category, PlayTime).Rows[0][0].ToString());
        }

        #region Promostion Code
        public static List<Promotion> GetPromostionCode()
        {
            DataTable dt = VistaDAL.GetPromostionCode();
            List<Promotion> listPromo = new List<Promotion>();
            Promotion objPromotion;
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    if (!String.IsNullOrEmpty(row["Regex"].ToString()))
                    {
                        objPromotion = new Promotion();
                        if (!row.IsNull("Promotion Code"))
                        {
                            objPromotion.PromotionCode = row["Promotion Code"].ToString();
                        }
                        if (!row.IsNull("Start Date"))
                        {
                            objPromotion.StartDate = Convert.ToDateTime(row["Start Date"]);
                        }
                        if (!row.IsNull("End date"))
                        {
                            objPromotion.EndDate = Convert.ToDateTime(row["End date"]);
                        }
                        if (!row.IsNull("Discount %"))
                        {
                            objPromotion.DiscountPercentage = Convert.ToDecimal(row["Discount %"]);
                        }
                        if (!row.IsNull("RegEx"))
                        {
                            objPromotion.RegexValidator = Convert.ToString(row["RegEx"]);
                        }
                        if (!row.IsNull("CO"))
                        {
                            objPromotion.CO = Convert.ToInt16(row["CO"]);
                        }
                        if (!row.IsNull("BZ"))
                        {
                            objPromotion.BZ = Convert.ToInt16(row["BZ"]);
                        }
                        if (!row.IsNull("DM"))
                        {
                            objPromotion.DM = Convert.ToInt16(row["DM"]);
                        }
                        if (!row.IsNull("GL"))
                        {
                            objPromotion.GL = Convert.ToInt16(row["GL"]);
                        }
                        if (!row.IsNull("SL"))
                        {
                            objPromotion.SL = Convert.ToInt16(row["SL"]);
                        }
                        if (!row.IsNull("PL"))
                        {
                            objPromotion.PL = Convert.ToInt16(row["PL"]);
                        }

                        listPromo.Add(objPromotion);
                    }
                }
            }
            return listPromo;
        }
        #endregion

        #region Promostion Code
        public static List<Promotion> GetAllPromostionCode()
        {
            DataTable dt = VistaDAL.GetAllPromostionCode();
            List<Promotion> listPromo = new List<Promotion>();
            Promotion objPromotion;
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    if (!String.IsNullOrEmpty(row["Regex"].ToString()))
                    {
                        objPromotion = new Promotion();
                        if (!row.IsNull("Promotion Code"))
                        {
                            objPromotion.PromotionCode = row["Promotion Code"].ToString();
                        }
                        if (!row.IsNull("Start Date"))
                        {
                            objPromotion.StartDate = Convert.ToDateTime(row["Start Date"]);
                        }
                        if (!row.IsNull("End date"))
                        {
                            objPromotion.EndDate = Convert.ToDateTime(row["End date"]);
                        }
                        if (!row.IsNull("Discount %"))
                        {
                            objPromotion.DiscountPercentage = Convert.ToDecimal(row["Discount %"]);
                        }
                        if (!row.IsNull("RegEx"))
                        {
                            objPromotion.RegexValidator = Convert.ToString(row["RegEx"]);
                        }
                        if (!row.IsNull("CO"))
                        {
                            objPromotion.CO = Convert.ToInt16(row["CO"]);
                        }
                        if (!row.IsNull("BZ"))
                        {
                            objPromotion.BZ = Convert.ToInt16(row["BZ"]);
                        }
                        if (!row.IsNull("DM"))
                        {
                            objPromotion.DM = Convert.ToInt16(row["DM"]);
                        }
                        if (!row.IsNull("GL"))
                        {
                            objPromotion.GL = Convert.ToInt16(row["GL"]);
                        }
                        if (!row.IsNull("SL"))
                        {
                            objPromotion.SL = Convert.ToInt16(row["SL"]);
                        }
                        if (!row.IsNull("PL"))
                        {
                            objPromotion.PL = Convert.ToInt16(row["PL"]);
                        }

                        listPromo.Add(objPromotion);
                    }
                }
            }
            return listPromo;
        }
        #endregion


        public static DataTable Select_Play(string Location, string filmCode, string PlayDate)
        {
            return VistaDAL._Select_Play(Location, filmCode, PlayDate);
        }

        public static DataTable Select_Category_Price(string filmcode, string category)
        {
            return VistaDAL._Select_Category_Price(filmcode, category);
        }
        public static DataTable Select_Newyear_RoyalInfo(String info)
        {
            return VistaDAL._Select_Newyear_RoyalInfo(info);
        }
       
    }

}