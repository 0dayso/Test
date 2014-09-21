using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using KoDTicketing.BusinessLayer;
using KoDTicketing.DataAccessLayer;

public partial class Royal_Card_Account_TicketBooking : System.Web.UI.Page
{
    public string Decrypt(string val)
    {
        var bytes = Convert.FromBase64String(val);
        var encBytes = System.Security.Cryptography.ProtectedData.Unprotect(bytes, new byte[0], System.Security.Cryptography.DataProtectionScope.LocalMachine);
        return System.Text.Encoding.UTF8.GetString(encBytes);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// 
    protected void Page_Load(object sender, EventArgs e)
    {
        //Use when under maintenance.
        //Server.Transfer("~/Error.aspx");

        if (!IsPostBack)
        {
            //lblRoyalcardBalance.Text = Decrypt(Request.QueryString["RemainingAmount"]);
            //lblRoyalcardPoints.Text = Decrypt(Request.QueryString["RemainingPoints"]);   
            string password = "A87C7B95932E9";
            lblRoyalcardPoints.Text = Common.Decrypt(Server.UrlDecode(Request.QueryString["RemainingPoints"].ToString()), password);
            lblRoyalcardBalance.Text = Common.Decrypt(Server.UrlDecode(Request.QueryString["RemainingAmount"].ToString()), password);
            
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
        ddl_Play.Items.Clear();
        DataTable dtlocation = VistaBOL.Select_Play();
        ddl_Play.Items.Add(new ListItem("Select", "0"));
        if (dtlocation != null && dtlocation.Rows.Count > 0)
        {
            foreach (DataRow dr in dtlocation.Rows)
                ddl_Play.Items.Add(new ListItem(dr[0].ToString(), dr[0].ToString()));
        }
    }
    protected void ddl_Play_SelectedIndexChanged(object sender, EventArgs e)
    {
        //xbol.Parameters.Clear();
        String Play = ddl_Play.SelectedValue;
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
        ddl_Date.Items.Clear();
        ddl_Date.Items.Add(new ListItem("Select", "0"));
        ddl_Date.Items[0].Selected = true;
        DataTable dtplaydate = VistaBOL.Select_PlayDate(Location, filmCode);
        foreach (DataRow dr in dtplaydate.Rows)
            if (!(dr[0].ToString().Equals("2012.12.31")))
            {
                ddl_Date.Items.Add(new ListItem(Convert.ToDateTime(dr[0].ToString()).ToString("ddd, MMM dd,yyyy"),
                    Convert.ToDateTime(dr[0].ToString()).ToString("dd/MM/yyyy")));
            }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddl_Date_SelectedIndexChanged(object sender, EventArgs e)
    {
        String filmCode = ddl_Play.SelectedValue;
        String PlayDate = ddl_Date.SelectedValue;
        PlayDate = PlayDate.Replace("-", "/"); //comment this for live deployment
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
        if (Session["PromotionCode"] == null)
        {
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                ddl_Category.Items.Add(new ListItem(ds.Tables[0].Rows[i][1].ToString() + ", Rs. " + String.Format("{0:#.##}",
                    decimal.Parse(ds.Tables[0].Rows[i][2].ToString())), ds.Tables[0].Rows[i][0].ToString()));
        }
        else
        {
            KoDTicketingLibrary.DTO.Promotion PromoSession = (KoDTicketingLibrary.DTO.Promotion)Session["PromotionCode"];
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

    [System.Web.Services.WebMethod]
    public static string BookingdateValidation(string selectedDate)
    {
        DateTime time = Convert.ToDateTime(selectedDate);
        double remain = time.Subtract(DateTime.Today).TotalDays;
        if (remain >= 30.0)
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
        Session["play_Val"] = ddl_Play.SelectedValue;
        Session["seat_Val"] = ddl_Location.SelectedValue + "," + ddl_Play.SelectedValue + "," + ddl_Date.SelectedValue +
            "," + ddl_ShowTimes.SelectedValue + "," + ddl_Category.SelectedValue + "," + drp_TotalSeats.SelectedValue +
            "," + ddl_Location.SelectedItem.Text + "," + ddl_ShowTimes.SelectedItem.Text + "," + ddl_Category.SelectedItem.Text;
        Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Render Seat Layout for " + Session["seat_Val"].ToString());
        Session["RedeemBalance"] = txtRoyalcardBalance.Text;
        Session["RedeemPoints"] = txtRoyalcardPoints.Text;
        string password = "A87C7B95932E9";
        //Session["Regid"] = Decrypt(Request.QueryString["MemberShipId"]);
        Common.Decrypt(Server.UrlDecode(Request.QueryString["MemberShipId"].ToString()), password);
        Session["Regid"] = Common.Decrypt(Server.UrlDecode(Request.QueryString["MemberShipId"].ToString()), password);
        Session["RPoints"] = lblRoyalcardPoints.Text;
        Session["RBal"] = lblRoyalcardBalance.Text;
        //Session["FirstName"] = Decrypt(Request.QueryString["FirstName"]);
        //Session["LastName"] = Decrypt(Request.QueryString["LastName"]);
        //Session["EmailID"] = Decrypt(Request.QueryString["Email"]);
        //Session["MobileNo"] = Decrypt(Request.QueryString["Mobile"]);
        //Session["Address"] = Decrypt(Request.QueryString["Address"]);

        Session["FirstName"] = Common.Decrypt(Server.UrlDecode(Request.QueryString["FirstName"].ToString()), password);
        Session["LastName"] = Common.Decrypt(Server.UrlDecode(Request.QueryString["LastName"].ToString()), password);
        Session["EmailID"] = Common.Decrypt(Server.UrlDecode(Request.QueryString["Email"].ToString()), password);
        Session["MobileNo"] = Common.Decrypt(Server.UrlDecode(Request.QueryString["Mobile"].ToString()), password);
        Session["Address"] = Common.Decrypt(Server.UrlDecode(Request.QueryString["Address"].ToString()), password);

        string category = ddl_Category.SelectedItem.Text;
        string[] arrCat = category.Split('.');

        decimal TotalAmount = Convert.ToDecimal(drp_TotalSeats.SelectedValue) * Convert.ToDecimal(arrCat[1]);
        Session["TotalAmount"] = TotalAmount;

        Session["PayableAmount"] = TotalAmount - (Convert.ToDecimal(txtRoyalcardBalance.Text) + Convert.ToDecimal(txtRoyalcardPoints.Text));
        Response.Redirect("Seat-Layout.aspx", false);
    }
}