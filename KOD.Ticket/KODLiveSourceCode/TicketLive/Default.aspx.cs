using System;
using System.Data;
using System.Web.UI.WebControls;
using KoDTicketing.BusinessLayer;
using KoDTicketing.DataAccessLayer;


public partial class Kod_SearchBooking : System.Web.UI.Page
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public string hotel = "false";
    public string validity = "";
    bool promotions = true;
    bool package = true;
    protected String Session_value = "";
    public string Decrypt(string val)
    {
        val = val.Replace(" ", "+");
        var bytes = Convert.FromBase64String(val);
        var encBytes = System.Security.Cryptography.ProtectedData.Unprotect(bytes, new byte[0], System.Security.Cryptography.DataProtectionScope.LocalMachine);
        return System.Text.Encoding.UTF8.GetString(encBytes);
    }
    public string Encrypt(string val)
    {
        var bytes = System.Text.Encoding.UTF8.GetBytes(val);
        var encBytes = System.Security.Cryptography.ProtectedData.Protect(bytes, new byte[0], System.Security.Cryptography.DataProtectionScope.LocalMachine);
        return Convert.ToBase64String(encBytes);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //Use when under maintenance.
        //Server.Transfer("~/Error.aspx");
        if ((Request.QueryString["MMT"] == "" || Request.QueryString["MMT"] == null) && (Request.QueryString["MANA"] == "" || Request.QueryString["MANA"] == null) && (Request.QueryString["Indigo"] == "" || Request.QueryString["Indigo"] == null) && (Request.QueryString["MMTD"] == "" || Request.QueryString["MMTD"] == null) && (Request.QueryString["Valentine"] == "" || Request.QueryString["Valentine"] == null) && (Request.QueryString["Hotel"] == "" || Request.QueryString["Hotel"] == null) && (Request.QueryString["Yatra"] == "" || Request.QueryString["yatra"] == null) && (Request.QueryString["WMC"] == "" || Request.QueryString["WMC"] == null) && (Request.QueryString["NWMCT"] == "" || Request.QueryString["NWMCT"] == null))
        {
           // Session["PromotionCode"] = null;
            //Session["Hotel"] = null;
            //Session["Package"] = null;
            promotions = false;
            package = false;
            hotel = "true";
        }
        if (Request.QueryString["Indigo"] != null || Request.QueryString["Hotel"] != null || Request.QueryString["MMTD"] != null || Request.QueryString["Yatra"] != null || Request.QueryString["WMC"] != null || Request.QueryString["NWMCT"] != null)
        {
            //Session["Package"] = null;
            package = false;
            hotel = "false";
        }
        String Enddate = "2013.09.29";
        DateTime End = Convert.ToDateTime(Enddate);
        String Presentdate = Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd");
        DateTime Present = Convert.ToDateTime(Presentdate);
        if (Present <= End)
        {
            validity = "true";
        }


        if (!IsPostBack)
        {
            //if (Request.QueryString["utm_source"] != null)
            //{
            //    GTICKDAL.Data_Tracker(Request.QueryString["utm_source"].ToString(), Request.QueryString["utm_medium"].ToString(),
            //        Request.QueryString["utm_term"].ToString(), Request.QueryString["utm_campaign"].ToString());
            //    Server.Transfer("Default.aspx");
            //}
            if (Request.QueryString["ac"] != null)
            {
                Session["AgentCode"] = Request.QueryString["ac"];
            }
            Load_Play();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    void Load_Play()
    {
        Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Start Load Play");

        ddl_Play.Items.Clear();
        DataTable dtlocation = VistaBOL.Select_Play();
        ddl_Play.Items.Add(new ListItem("Select", "0"));
        if (Request.QueryString["Valentine"] == "s")
        {
            ddl_Play.Items.Add(new ListItem("JHUMROO", "JHUMROO"));
        }
        else if (Request.QueryString["MMTD"] == "s")
        {
            foreach (DataRow dr in dtlocation.Rows)
                if (dr[0].ToString() != "MANA")
                    ddl_Play.Items.Add(new ListItem(dr[0].ToString(), dr[0].ToString()));
        }
        else if (Request.QueryString["MMT"] == "MMTUS")
        {
            foreach (DataRow dr in dtlocation.Rows)
            {
                if (dr[0].ToString() != "MANA")
                    ddl_Play.Items.Add(new ListItem(dr[0].ToString(), dr[0].ToString()));
            }
        }
        else if (Request.QueryString["MANA"] == "ManaPromo")
        {
            foreach (DataRow dr in dtlocation.Rows)
                //if (dr[0].ToString() != "JHUMROO" && dr[0].ToString() != "ZANGOORA")
                if (dr[0].ToString() == "MANA")
                    ddl_Play.Items.Add(new ListItem(dr[0].ToString(), dr[0].ToString()));
        }
        else if (dtlocation != null && dtlocation.Rows.Count > 0)
        {
            foreach (DataRow dr in dtlocation.Rows)
            {
                if (dr[0].ToString() != "NEW YEAR14")
                ddl_Play.Items.Add(new ListItem(dr[0].ToString(), dr[0].ToString()));
            }
        }
        Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("End Load Play");
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddl_Play_SelectedIndexChanged(object sender, EventArgs e)
    {
        //xbol.Parameters.Clear();
        String Play = ddl_Play.SelectedValue;
        dateofshow.Text = "Select";
        ddl_Location.Items.Clear();
        ddl_Location.Items.Add(new ListItem("Select", "0"));
        DataTable dtAudi = VistaBOL.Select_Audi(Play);
        foreach (DataRow dr in dtAudi.Rows)
            ddl_Location.Items.Add(new ListItem(dr[1].ToString(), dr[0].ToString()));

    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddl_Location_SelectedIndexChanged(object sender, EventArgs e)
    {
        String filmCode = ddl_Play.SelectedValue;
        String Location = ddl_Location.SelectedValue;
        dateofshow.Text = "Select";
        ddl_Date.Items.Clear();
        ddl_Date.Items.Add(new ListItem("Select", "0"));
        ddl_Date.Items[0].Selected = true;
        DataTable dtplaydate = VistaBOL.Select_PlayDate(Location, filmCode);
        if (Request.QueryString["MANA"] == "ManaPromo")
        {
            string Enddate = "2013.11.30";
            DateTime Endt = Convert.ToDateTime(Enddate);
            foreach (DataRow dr in dtplaydate.Rows)
            {
                if (!(dr[0].ToString().Equals("2012.12.31")))
                {
                    if (Convert.ToDateTime(dr[0]) <= Endt)
                    {
                        if (Session["Package"].ToString() == "Weekend,Rs.4999")
                        {
                            if (Convert.ToDateTime(dr[0]).DayOfWeek.ToString().ToUpper() == "SUNDAY" || Convert.ToDateTime(dr[0]).DayOfWeek.ToString().ToUpper() == "SATURDAY" || Convert.ToDateTime(dr[0].ToString()).ToString("dd/MM/yyyy") == "09/08/2013" || Convert.ToDateTime(dr[0].ToString()).ToString("dd/MM/yyyy") == "15/08/2013")
                            {
                                ddl_Date.Items.Add(new ListItem(Convert.ToDateTime(dr[0].ToString()).ToString("ddd, MMM dd,yyyy"),
                                Convert.ToDateTime(dr[0].ToString()).ToString("dd/MM/yyyy")));
                            }
                        }
                        else if (Session["Package"].ToString() == "Weekday,Rs.3999")
                        {
                            if (Convert.ToDateTime(dr[0]).DayOfWeek.ToString().ToUpper() != "SUNDAY" && Convert.ToDateTime(dr[0]).DayOfWeek.ToString().ToUpper() != "SATURDAY" && Convert.ToDateTime(dr[0].ToString()).ToString("dd/MM/yyyy") != "15/08/2013" && Convert.ToDateTime(dr[0].ToString()).ToString("dd/MM/yyyy") != "09/08/2013")
                            {
                                ddl_Date.Items.Add(new ListItem(Convert.ToDateTime(dr[0].ToString()).ToString("ddd, MMM dd,yyyy"),
                                Convert.ToDateTime(dr[0].ToString()).ToString("dd/MM/yyyy")));
                            }
                        }
                    }
                }
            }
        }
        else if (Request.QueryString["MMT"] == "MMTUS")
        {
            string Enddate = "2013.11.30";
            DateTime Endt = Convert.ToDateTime(Enddate);
            foreach (DataRow dr in dtplaydate.Rows)
            {
                if (Convert.ToDateTime(dr[0]) <= Endt)
                {
                    if (Convert.ToDateTime(dr[0]).DayOfWeek.ToString().ToUpper() == "SUNDAY" || Convert.ToDateTime(dr[0]).DayOfWeek.ToString().ToUpper() == "SATURDAY" || (Convert.ToDateTime(dr[0]).DayOfWeek.ToString().ToUpper() == "FRIDAY" && ddl_Play.SelectedValue.ToString() == "ZANGOORA"))
                    {
                        ddl_Date.Items.Add(new ListItem(Convert.ToDateTime(dr[0].ToString()).ToString("ddd, MMM dd,yyyy"),
                        Convert.ToDateTime(dr[0].ToString()).ToString("dd/MM/yyyy")));
                    }
                }
            }
        }
        else
        {
            foreach (DataRow dr in dtplaydate.Rows)
                if (!(dr[0].ToString().Equals("2012.12.31")))
                {
                    ddl_Date.Items.Add(new ListItem(Convert.ToDateTime(dr[0].ToString()).ToString("ddd, MMM dd,yyyy"),
                   Convert.ToDateTime(dr[0].ToString()).ToString("dd/MM/yyyy")));
                }
        }
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dateofshow_TextChanged(object sender, EventArgs e)
    {
        String PlayDate;
        if (dateofshow.Text.ToString() == "" || dateofshow.Text.ToString() == "Select")
        {
            PlayDate = "0";
            dateofshow.Text = "Select";
        }
        else
        {
            PlayDate = Convert.ToDateTime(dateofshow.Text.ToString()).ToString("dd/MM/yyyy");
        }
        String filmCode = ddl_Play.SelectedValue;
        //String PlayDate = ddl_Date.SelectedValue;
        //PlayDate = PlayDate.Replace("-", "/");
        String Location = ddl_Location.SelectedValue;

        ddl_ShowTimes.Items.Clear();
        ddl_ShowTimes.Items.Add(new ListItem("Select", "0"));
        ddl_Category.Items.Clear();
        ddl_Category.Items.Add(new ListItem("Select", "0"));
        ddl_Category.Items.Clear();
        ddl_Category.Items.Add(new ListItem("Select", "0"));
        drp_TotalSeats.Items.Clear();
        drp_TotalSeats.Items.Add(new ListItem("Select", "0"));

        foreach (DataRow dr in VistaBOL.Select_PlayTime(Location, filmCode, PlayDate).Rows)
        {
            ddl_ShowTimes.Items.Add(new ListItem(Convert.ToDateTime(dr[0].ToString()).ToShortTimeString(), dr[1].ToString().Trim()));
        }
    }


    /// <summary>
    ///  Fill up the categories based on the Show/Play selected.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddl_ShowTimes_SelectedIndexChanged(object sender, EventArgs e)
    {
        String filmCode = ddl_ShowTimes.SelectedValue;
        ddl_Category.Items.Clear();
        ddl_Category.Items.Add(new ListItem("Select", "0"));
        drp_TotalSeats.Items.Clear();
        drp_TotalSeats.Items.Add(new ListItem("Select", "0"));
        DataSet ds = VistaBOL.Select_Category_DS(filmCode);
        if (promotions==false)
        {
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                ddl_Category.Items.Add(new ListItem(ds.Tables[0].Rows[i][1].ToString() + ", Rs. " + String.Format("{0:#.##}",
                    decimal.Parse(ds.Tables[0].Rows[i][2].ToString())), ds.Tables[0].Rows[i][0].ToString()));
        }
        else
        {
            KoDTicketingLibrary.DTO.Promotion PromoSession = (KoDTicketingLibrary.DTO.Promotion)Session["a" + Decrypt(Request.QueryString["promo"])];
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (ds.Tables[0].Rows[i][0].ToString() == "BZ")
                {
                    if (PromoSession.BZ == 1)
                    {
                        ddl_Category.Items.Add(new ListItem(ds.Tables[0].Rows[i][1].ToString() + ", Rs. " + String.Format("{0:#.##}",
                    decimal.Parse(ds.Tables[0].Rows[i][2].ToString())), ds.Tables[0].Rows[i][0].ToString()));
                    }
                }
                else if (ds.Tables[0].Rows[i][0].ToString() == "CO")
                {
                    if (PromoSession.CO == 1)
                    {
                        ddl_Category.Items.Add(new ListItem(ds.Tables[0].Rows[i][1].ToString() + ", Rs. " + String.Format("{0:#.##}",
                    decimal.Parse(ds.Tables[0].Rows[i][2].ToString())), ds.Tables[0].Rows[i][0].ToString()));
                    }
                }
                else if (ds.Tables[0].Rows[i][0].ToString() == "SL")
                {
                    if (PromoSession.SL == 1)
                    {
                        ddl_Category.Items.Add(new ListItem(ds.Tables[0].Rows[i][1].ToString() + ", Rs. " + String.Format("{0:#.##}",
                    decimal.Parse(ds.Tables[0].Rows[i][2].ToString())), ds.Tables[0].Rows[i][0].ToString()));
                    }
                }
                else if (ds.Tables[0].Rows[i][0].ToString() == "DM")
                {
                    if (PromoSession.DM == 1)
                    {
                        ddl_Category.Items.Add(new ListItem(ds.Tables[0].Rows[i][1].ToString() + ", Rs. " + String.Format("{0:#.##}",
                    decimal.Parse(ds.Tables[0].Rows[i][2].ToString())), ds.Tables[0].Rows[i][0].ToString()));
                    }
                }
                else if (ds.Tables[0].Rows[i][0].ToString() == "PL")
                {
                    if (PromoSession.PL == 1)
                    {
                        ddl_Category.Items.Add(new ListItem(ds.Tables[0].Rows[i][1].ToString() + ", Rs. " + String.Format("{0:#.##}",
                    decimal.Parse(ds.Tables[0].Rows[i][2].ToString())), ds.Tables[0].Rows[i][0].ToString()));
                    }
                }
                else if (ds.Tables[0].Rows[i][0].ToString() == "GL")
                {
                    if (PromoSession.GL == 1)
                    {
                        ddl_Category.Items.Add(new ListItem(ds.Tables[0].Rows[i][1].ToString() + ", Rs. " + String.Format("{0:#.##}",
                    decimal.Parse(ds.Tables[0].Rows[i][2].ToString())), ds.Tables[0].Rows[i][0].ToString()));
                    }
                }
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddl_Category_SelectedIndexChanged(object sender, EventArgs e)
    {
        const ushort MAX_SEATS_PER_TRANSACTION = 10; //needs to be moved to config;
        drp_TotalSeats.Items.Clear();
        drp_TotalSeats.Items.Add(new ListItem("Select", "0"));
        String Category = ddl_Category.SelectedValue;
        String PlayTime = ddl_ShowTimes.SelectedValue;

        int availableSeats = VistaBOL.Select_Available_Seats(Category, PlayTime);



        if (Request.QueryString["Valentine"] == "s")
        {
            drp_TotalSeats.SelectedIndex = 2;
            drp_TotalSeats.Enabled = false;
        }
        else if (Request.QueryString["MANA"] == "ManaPromo")
        {
            int requiredseats = int.Parse(Session["NoofPackages"].ToString()) * 4;
            if (availableSeats == 0 || availableSeats < requiredseats)
            {
                drp_TotalSeats.Items.Add(new ListItem("Sold Out", "0"));
            }
            else if (availableSeats >= requiredseats)
            {
                drp_TotalSeats.Items.Add(new ListItem(requiredseats.ToString(), requiredseats.ToString()));
            }
        }
        else if (Request.QueryString["MMT"] == "MMTUS")
        {
            int requiredseats = int.Parse(Session["NoofPackages"].ToString());
            if (availableSeats == 0 || availableSeats < requiredseats)
            {
                drp_TotalSeats.Items.Add(new ListItem("Sold Out", "0"));
            }
            else if (availableSeats >= requiredseats)
            {
                drp_TotalSeats.Items.Add(new ListItem(requiredseats.ToString(), requiredseats.ToString()));
            }
        }
        else if (Request.QueryString["Yatra"] == "S")
        {
            int requiredseats = int.Parse(Session["NoofTickets"].ToString());
            if (availableSeats == 0 || availableSeats < requiredseats)
            {
                drp_TotalSeats.Items.Add(new ListItem("Sold Out", "0"));
            }
            else if (availableSeats >= requiredseats)
            {
                drp_TotalSeats.Items.Add(new ListItem(requiredseats.ToString(), requiredseats.ToString()));
            }
        }
        else if (Request.QueryString["WMC"] == "S" || Request.QueryString["NWMCT"] == "S")
        {
            const ushort MAX_SEATS = 4;
            if (availableSeats > 0) //if there are seats available...
            {
                //ensure only MAX per transaction are allowed for selection
                availableSeats = (availableSeats > MAX_SEATS) ? 4 : availableSeats;
                for (int i = 1; i <= availableSeats; i++)
                    drp_TotalSeats.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }
            else //otherwise mark sold out
            {
                drp_TotalSeats.Items.Add(new ListItem("Sold Out", "0"));
            }
        }
        //else if (Request.QueryString["NWMCP"] == "S")
        //{
        //    int requiredseats = int.Parse(Session["MCOTHERSNOOFPACKAGE"].ToString()) * 2;
        //    if (availableSeats > 0) //if there are seats available...
        //    {
        //        if (availableSeats >= requiredseats)
        //        {
        //            drp_TotalSeats.Items.Add(new ListItem(requiredseats.ToString(), requiredseats.ToString()));
        //        }
        //    }
        //    else //otherwise mark sold out
        //    {
        //        drp_TotalSeats.Items.Add(new ListItem("Sold Out", "0"));
        //    }
        //}
        else
        {
            if (availableSeats > 0) //if there are seats available...
            {
                //ensure only MAX per transaction are allowed for selection
                availableSeats = (availableSeats > MAX_SEATS_PER_TRANSACTION) ? 10 : availableSeats;
                for (int i = 1; i <= availableSeats; i++)
                    drp_TotalSeats.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }
            else //otherwise mark sold out
            {
                drp_TotalSeats.Items.Add(new ListItem("Sold Out", "0"));
            }
        }
    }

    [System.Web.Services.WebMethod]
    public static string BookingdateValidation(string selectedDate)
    {
        DateTime time = Convert.ToDateTime(selectedDate);
        double remain = time.Subtract(DateTime.Today).TotalDays;
        if (remain >= 60.0)
        {
            return "BOOKING TO OPEN 1 MONTH BEFORE THE SHOW";//"BOOKING TO OPEN 1 MONTH BEFORE THE SHOW";
        }
        return "Welcome";
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btn_Submit_Click(object sender, EventArgs e)
    {
        long TransectionCounter = GTICKBOL.TransactionCounter_Max();
        Session[TransectionCounter.ToString()] = TransectionCounter;
        //***************if routed from other website*****************
        if (Request.QueryString["Router"] == "" || Request.QueryString["Router"] == null)
        {
            Session["Router"] = "";
        }
        else if (Request.QueryString["Router"] != "buzzintown" && Request.QueryString["Router"] != "airfaresau")
        {
            Session["Router"] = "";
        }
        else
        {
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Booking Through" + Request.QueryString["Router"].ToString());
            Session["Router"] = Request.QueryString["Router"].ToString();
        }
        //****************************************************************
        Session["play_Val"] = ddl_Play.SelectedValue;
        Session["play_Val_Location"] = ddl_Location.SelectedItem;
        if (promotions==false)
        {
            Session_value = ddl_Location.SelectedValue + "," + ddl_Play.SelectedValue + "," + Convert.ToDateTime(dateofshow.Text.ToString()).ToString("dd/MM/yyyy") +
            "," + ddl_ShowTimes.SelectedValue + "," + ddl_Category.SelectedValue + "," + drp_TotalSeats.SelectedValue +
            "," + ddl_Location.SelectedItem.Text + "," + ddl_ShowTimes.SelectedItem.Text + "," + ddl_Category.SelectedItem.Text + "," + "" + "," + "" + "," + TransectionCounter; ;
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Render Seat Layout for " + Session_value);
        }
        else
        {
            KoDTicketingLibrary.DTO.Promotion PromoSession = (KoDTicketingLibrary.DTO.Promotion)Session["a" + Decrypt(Request.QueryString["promo"])];
            if (package == false)
            {
                Session_value = ddl_Location.SelectedValue + "," + ddl_Play.SelectedValue + "," + Convert.ToDateTime(dateofshow.Text.ToString()).ToString("dd/MM/yyyy") +
                "," + ddl_ShowTimes.SelectedValue + "," + ddl_Category.SelectedValue + "," + drp_TotalSeats.SelectedValue +
                "," + ddl_Location.SelectedItem.Text + "," + ddl_ShowTimes.SelectedItem.Text + "," + ddl_Category.SelectedItem.Text + "," + PromoSession.PromotionCode.ToString().ToUpper() + "," + "" + "," + TransectionCounter;
            }
            else
            {
                Session_value = ddl_Location.SelectedValue + "," + ddl_Play.SelectedValue + "," + Convert.ToDateTime(dateofshow.Text.ToString()).ToString("dd/MM/yyyy") +
                   "," + ddl_ShowTimes.SelectedValue + "," + ddl_Category.SelectedValue + "," + drp_TotalSeats.SelectedValue +
                   "," + ddl_Location.SelectedItem.Text + "," + ddl_ShowTimes.SelectedItem.Text + "," + ddl_Category.SelectedItem.Text + "," + PromoSession.PromotionCode.ToString().ToUpper() + "," + Session["Package"].ToString() + "," + TransectionCounter;
            }
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Render Seat Layout for " + Session_value);  
        }

        if (Session["Hotel"] == null)
        {
            Session["Hotel"] = "";
        }
        GTICKBOL.Insert_ShowDetail(Session_value, TransectionCounter);
        KoDTicketing.GTICKV.LogEntry(TransectionCounter.ToString(), "User Try to move from Default to Seat layout Page.", "1", "");
        if (Request.QueryString["Router"] == "buzzintown" || Request.QueryString["Router"] == "airfaresau")
        {
            Response.Redirect("Seat-Layout.aspx?SessionId=" + Encrypt(TransectionCounter.ToString()) + "&Router=" + Request.QueryString["Router"], false);
        }
        else
        {
            if (Request.QueryString["promo"] != null)
                Session["a" + Decrypt(Request.QueryString["promo"])] = "";
            Response.Redirect("Seat-Layout.aspx?SessionId=" + Encrypt(TransectionCounter.ToString()), false);
        }
    }
}
