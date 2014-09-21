using System;
using System.Data;
using System.Web.UI.WebControls;
using KoDTicketing.BusinessLayer;
using KoDTicketing.DataAccessLayer;
using KoDTicketingLibrary.DTO;
using System.Collections.Generic;

public partial class JhumrooOffer : System.Web.UI.Page
{
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
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["ac"] != null)
            {
                Session["AgentCode"] = Request.QueryString["ac"];
            }
            Load_Play();
        }
        List<KoDTicketingLibrary.DTO.Promotion> listPromo = VistaBOL.GetPromostionCode();
        int list = listPromo.Count;
        for (int i = 0; i < list; i++)
        {
            if (listPromo[i].PromotionCode.ToString().ToUpper() == "JHUMROOOFFER")
            {
                Session[listPromo[i].PromotionCode] = listPromo[i];
                Session["a" + listPromo[i].PromotionCode] = listPromo[i];
            }
        }
    }
    void Load_Play()
    {
        Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Start Load Play");
        ddl_Play.Items.Clear();
        DataTable dtlocation = VistaBOL.Select_Play();
        ddl_Play.Items.Add(new ListItem("Select", "0"));
        if (dtlocation != null && dtlocation.Rows.Count > 0)
        {
            foreach (DataRow dr in dtlocation.Rows)
            {
                if (dr[0].ToString() == "JHUMROO")
                {
                    ddl_Play.Items.Add(new ListItem(dr[0].ToString(), dr[0].ToString()));
                }
            }
        }
        Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("End Load Play");
    }
    protected void ddl_Play_SelectedIndexChanged(object sender, EventArgs e)
    {
        //xbol.Parameters.Clear();
        String Play = ddl_Play.SelectedValue;
        dateofshow.Text = "Select";
        ddl_Location.Items.Clear();
        ddl_Location.Items.Add(new ListItem("Select", "0"));
        DataTable dtAudi = VistaBOL.Select_Audi(Play);
        foreach (DataRow dr in dtAudi.Rows)
        {
            ddl_Location.Items.Add(new ListItem(dr[1].ToString(), dr[0].ToString()));
        }
    }
    protected void ddl_Location_SelectedIndexChanged(object sender, EventArgs e)
    {
        String filmCode = ddl_Play.SelectedValue;
        String Location = ddl_Location.SelectedValue;
        dateofshow.Text = "Select";
        ddl_Date.Items.Clear();
        ddl_Date.Items.Add(new ListItem("Select", "0"));
        ddl_Date.Items[0].Selected = true;
        DataTable dtplaydate = VistaBOL.Select_PlayDate(Location, filmCode);
        string Enddate = "2014.04.10";
        DateTime Endt = Convert.ToDateTime(Enddate);
        foreach (DataRow dr in dtplaydate.Rows)
        {
            if (!(dr[0].ToString().Equals("2012.12.31")))
            {
                if (Convert.ToDateTime(dr[0]) <= Endt)
                {
                    ddl_Date.Items.Add(new ListItem(Convert.ToDateTime(dr[0].ToString()).ToString("ddd, MMM dd,yyyy"),
                   Convert.ToDateTime(dr[0].ToString()).ToString("dd/MM/yyyy")));
                }
            }
        }
    }
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
        KoDTicketingLibrary.DTO.Promotion PromoSession = (KoDTicketingLibrary.DTO.Promotion)Session["a" + "JHUMROOOFFER"];
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
        if (availableSeats > 0) //if there are seats available...
        {
            //ensure only MAX per transaction are allowed for selection
            availableSeats = (availableSeats > MAX_SEATS_PER_TRANSACTION) ? 10 : availableSeats;
            for (int i = 1; i <= availableSeats; i++)
            {
                if (i == 2 || i == 4 || i == 6 || i == 8 || i == 10)
                {
                    drp_TotalSeats.Items.Add(new ListItem(i.ToString(), i.ToString()));
                }
            }
        }
        else //otherwise mark sold out
        {
            drp_TotalSeats.Items.Add(new ListItem("Sold Out", "0"));
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
    /// 

    protected void btn_Submit_Click(object sender, EventArgs e)
    {
        long TransectionCounter = GTICKBOL.TransactionCounter_Max();
        Session[TransectionCounter.ToString()] = TransectionCounter;
        Session["play_Val"] = ddl_Play.SelectedValue;
        Session["play_Val_Location"] = ddl_Location.SelectedItem;
        Session_value = ddl_Location.SelectedValue + "," + ddl_Play.SelectedValue + "," + Convert.ToDateTime(dateofshow.Text.ToString()).ToString("dd/MM/yyyy") +
       "," + ddl_ShowTimes.SelectedValue + "," + ddl_Category.SelectedValue + "," + drp_TotalSeats.SelectedValue +
       "," + ddl_Location.SelectedItem.Text + "," + ddl_ShowTimes.SelectedItem.Text + "," + ddl_Category.SelectedItem.Text + "," + "JHUMROOOFFER" + "," + "" + "," + TransectionCounter; ;
        Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Render Seat Layout for " + Session_value);
        GTICKBOL.Insert_ShowDetail(Session_value, TransectionCounter);
        Session["a" + "JHUMROOOFFER"] = "";
        Response.Redirect("Seat-Layout.aspx?SessionId=" + Encrypt(TransectionCounter.ToString()), false);
    }
}