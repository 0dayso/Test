using System;
using System.Web;
using KoDTicketing;
using KoDTicketing.BusinessLayer;
using System.Data;
using System.Collections.Generic;
public partial class ContactDetails : System.Web.UI.Page
{
    protected String Session_value = "";
    protected String SeatVal = "";
    protected DataTable dtseatval;
    protected string[] sessionvalue;
    public string hotel = "";
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
    public static string KoDTicketingIPAddress = System.Configuration.ConfigurationManager.AppSettings["KoDTicketingIPAddress"].ToString();
    public static string HDFCTransUrl = System.Configuration.ConfigurationManager.AppSettings["HDFCTransUrl"].ToString();
    public static string HDFCTransPortalID = System.Configuration.ConfigurationManager.AppSettings["HDFCTransPortalID"].ToString();
    public static string HDFCTranportalPwd = System.Configuration.ConfigurationManager.AppSettings["HDFCTranportalPwd"].ToString();
    public static long BookingID;
    protected void Page_Load(object sender, EventArgs e)
    {
        Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write(Decrypt(Request.QueryString["SessionId"]));
        if (Session[Decrypt(Request.QueryString["SessionId"])].ToString() != "")
        {
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write(Decrypt(Request.QueryString["SessionId"]));
            if (Request.QueryString["SessionId"] != null || Request.QueryString["SessionId"] != "")
            {
                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write(Decrypt(Request.QueryString["SessionId"]));
                DataTable dtseatval = TransactionBOL.Select_ShowDetails(Convert.ToInt64(Decrypt(Request.QueryString["SessionId"].ToString())));
                Session_value = dtseatval.Rows[0]["Seat_Val"].ToString();
                sessionvalue = Session_value.Split(',');
                SeatVal = dtseatval.Rows[0]["Seat_Info"].ToString();
                hotel = sessionvalue[10];
            }  
        }
        else
        {
            Session.Clear();
            Response.Redirect("Default.aspx", false);
        }
        if (sessionvalue[10].ToString() == "")
        {
            chkGCab.Visible = false;
            chkGCabPD.Visible = false;
        }
        else if (sessionvalue[10].ToString() == "PULMAN" || sessionvalue[10].ToString() == "LEELAKEMPINSKI")
        {
            chkGCab.Visible = true;
            chkGCabPD.Visible = false;
        }
        else if (sessionvalue[10].ToString() == "CROWNPLAZA")
        {
            chkGCab.Visible = false;
            chkGCabPD.Visible = true;
        }
        else
        {
            chkGCab.Visible = false;
            chkGCabPD.Visible = false;
        }

        //*******************Promo**************************//
        decimal TotalAmount = 0;
        bool isfilled = false;
        if (Request.QueryString["SessionId"] != null || Request.QueryString["SessionId"].ToString() != "")
        {
            //BookingID = long.Parse(Session["Seat_TransactionID"].ToString());
            //TotalAmount = GTICKBOL.Get_SeatPrice_SeatKeyNoWise(BookingID);
            //Session["TotalAmount"] = decimal.Truncate(TotalAmount);
            string[] strarr = null;
            if (Session_value != "")
            {
                    strarr = Session_value.Split(',');
                    isfilled = false;
            }
            if (strarr.Length < 6)
            {
                throw new Exception("Contact details page loading cannot be done as session value is no valid Session: " + (isfilled ? Session_value:Session_value ));
            }
            string category = strarr[8].ToString();
            string SingleSeatPrice = strarr[9].ToString().Split('.')[1].ToString();
            TotalAmount = Convert.ToDecimal(strarr[9].ToString().Split('.')[1].ToString()) * Convert.ToDecimal(strarr[5].ToString());
            Session["TotalAmount"] = decimal.Truncate(TotalAmount);
            string ShowDate = "";
            string[] datarr = strarr[2].ToString().Split('/');//for live server
            ShowDate = datarr[1] + "/" + datarr[0] + "/" + datarr[2];//for live server

            //string[] datarr = strarr[2].ToString().Split('-');// for dev/local
            //ShowDate = datarr[0] + "/" + datarr[1] + "/" + datarr[2]; // for dev/local

            string day = Convert.ToDateTime(ShowDate).DayOfWeek.ToString().ToUpper();
            //ShowDate = datarr[1] + "/" + datarr[0] + "/" + datarr[2];//for local
            Session["day"] = day;

            Decimal dispercentage;
            decimal PayableAmount = 0;
            //string Enddate = "2014.04.10";
            //DateTime Endt = Convert.ToDateTime(Enddate);
            //String Presentdatte = Convert.ToDateTime(ShowDate).ToString("yyyy-MM-dd");
            //DateTime Presentt = Convert.ToDateTime(Presentdatte);
           
            if (strarr[10].ToString() == "")
            {
                //************Jhumroo Offer (1+1) contact details changes START here************
                //if (strarr[1].ToString() == "JHUMROO" && (strarr[4].ToString() == "GLY" || strarr[4].ToString() == "SL" || strarr[4].ToString() == "GL" || strarr[4].ToString() == "PL" || strarr[4].ToString() == "DM") && Presentt <= Endt)
                //{
                //    lblttlAmt.Visible = true;
                //    lblpayAmt.Visible = true;
                //    string Cat = strarr[4].ToString();
                //    string filmcode = strarr[3].ToString();
                //    decimal price;
                //    decimal total_seats = decimal.Parse(strarr[5].ToString());
                //    DataTable dt;
                //    if (total_seats == 1 || total_seats == 2)
                //    {
                //        dt = VistaBOL.Select_Category_Price(filmcode, Cat);
                //        price = decimal.Parse((dt.Rows[0][0].ToString()));
                //        PayableAmount = decimal.Truncate(price);
                //        Session["PayableAmount"] = PayableAmount;
                //        lblpayAmt.Text = "Total Payable Amount : Rs. " + Session["PayableAmount"].ToString();
                //        lblttlAmt.Text = "Total Amount : Rs. " + Session["TotalAmount"].ToString();
                //    }
                //    else if (total_seats == 3 || total_seats == 4)
                //    {
                //        dt = VistaBOL.Select_Category_Price(filmcode, Cat);
                //        price = decimal.Parse((dt.Rows[0][0].ToString())) * 2;
                //        PayableAmount = decimal.Truncate(price);
                //        Session["PayableAmount"] = PayableAmount;
                //        lblpayAmt.Text = "Total Payable Amount : Rs. " + Session["PayableAmount"].ToString();
                //        lblttlAmt.Text = "Total Amount : Rs. " + Session["TotalAmount"].ToString();
                //    }
                //    else if (total_seats == 5 || total_seats == 6)
                //    {
                //        dt = VistaBOL.Select_Category_Price(filmcode, Cat);
                //        price = decimal.Parse((dt.Rows[0][0].ToString())) * 3;
                //        PayableAmount = decimal.Truncate(price);
                //        Session["PayableAmount"] = PayableAmount;
                //        lblpayAmt.Text = "Total Payable Amount : Rs. " + Session["PayableAmount"].ToString();
                //        lblttlAmt.Text = "Total Amount : Rs. " + Session["TotalAmount"].ToString();
                //    }
                //    else if (total_seats == 7 || total_seats == 8)
                //    {
                //        dt = VistaBOL.Select_Category_Price(filmcode, Cat);
                //        price = decimal.Parse((dt.Rows[0][0].ToString())) * 4;
                //        PayableAmount = decimal.Truncate(price);
                //        Session["PayableAmount"] = PayableAmount;
                //        lblpayAmt.Text = "Total Payable Amount : Rs. " + Session["PayableAmount"].ToString();
                //        lblttlAmt.Text = "Total Amount : Rs. " + Session["TotalAmount"].ToString();
                //    }
                //    else if (total_seats == 9 || total_seats == 10)
                //    {
                //        dt = VistaBOL.Select_Category_Price(filmcode, Cat);
                //        price = decimal.Parse((dt.Rows[0][0].ToString())) * 5;
                //        PayableAmount = decimal.Truncate(price);
                //        Session["PayableAmount"] = PayableAmount;
                //        lblpayAmt.Text = "Total Payable Amount : Rs. " + Session["PayableAmount"].ToString();
                //        lblttlAmt.Text = "Total Amount : Rs. " + Session["TotalAmount"].ToString();
                //    }
                //}
                //************Jhumroo Offer (1+1) contact details changes END here ************
                    lblttlAmt.Visible = true;
                    lblttlAmt.Text = "Total Payable Amount : Rs. " + Session["TotalAmount"].ToString();
            }
            else if (strarr[10].ToString() != "")
            {
                   KoDTicketingLibrary.DTO.Promotion PromoSession = (KoDTicketingLibrary.DTO.Promotion)Session[strarr[10]];
                   
                    if (PromoSession.PromotionCode == "VIVANTABYTAJ")
                    {
                        string Cat = strarr[4].ToString();
                        string category1;
                        string filmcode = strarr[3].ToString();
                        decimal price;
                        decimal total_seats = decimal.Parse(strarr[5].ToString());
                        DataTable dt;
                        if (Cat == "DM")
                        {
                            category1 = "GL"; // pay gold price instead of Diamond
                            dt = VistaBOL.Select_Category_Price(filmcode, category1);
                            price = decimal.Parse(dt.Rows[0][0].ToString());
                            PayableAmount = decimal.Truncate(price * total_seats);
                        }
                        else
                        {
                            PayableAmount = TotalAmount;
                        }

                        Session["PayableAmount"] = PayableAmount;
                        lblttlAmt.Visible = true;
                        lblpayAmt.Visible = true;
                        lblpayAmt.Text = "Total Payable Amount : Rs. " + Session["PayableAmount"].ToString();
                        lblttlAmt.Text = "Total Amount : Rs. " + Session["TotalAmount"].ToString();
                    }
                    else if (PromoSession.PromotionCode == "TRIDENT" || PromoSession.PromotionCode == "OBEROI" || PromoSession.PromotionCode == "EROSMANAGED" || PromoSession.PromotionCode == "CROWNEPLAZAROHINI")                       //for oberoi and trident hotel
                    {
                        string Cat = strarr[4].ToString();
                        string category1;
                        string filmcode = strarr[3].ToString();
                        decimal price;
                        decimal total_seats = decimal.Parse(strarr[5].ToString());
                        DataTable dt;
                        if (Cat == "PL")
                        {
                            category1 = "GL";                                                        // pay gold price instead of Platinum
                            dt = VistaBOL.Select_Category_Price(filmcode, category1);
                            price = decimal.Parse(dt.Rows[0][0].ToString());
                            PayableAmount = decimal.Truncate(price * total_seats);
                        }
                        else if (Cat == "DM")
                        {
                            category1 = "PL";                                                        // pay platinum price instead of Diamond
                            dt = VistaBOL.Select_Category_Price(filmcode, category1);
                            price = decimal.Parse(dt.Rows[0][0].ToString());
                            PayableAmount = decimal.Truncate(price * total_seats);
                        }
                        else
                        {
                            PayableAmount = TotalAmount;
                        }
                        Session["PayableAmount"] = PayableAmount;
                        lblttlAmt.Visible = true;
                        lblpayAmt.Visible = true;
                        lblpayAmt.Text = "Total Payable Amount : Rs. " + Session["PayableAmount"].ToString();
                        lblttlAmt.Text = "Total Amount : Rs. " + Session["TotalAmount"].ToString();
                    }
                    else if (PromoSession.PromotionCode == "OBEROIDELHI")                       //for oberoi and trident hotel
                    {
                        string Cat = strarr[4].ToString();
                        string category1;
                        string filmcode = strarr[3].ToString();
                        decimal price;
                        decimal total_seats = decimal.Parse(strarr[5].ToString());
                        DataTable dt;
                        if (Cat == "GL")
                        {
                            category1 = "SL";                                                        // pay gold price instead of Platinum
                            dt = VistaBOL.Select_Category_Price(filmcode, category1);
                            price = decimal.Parse(dt.Rows[0][0].ToString());
                            PayableAmount = decimal.Truncate(price * total_seats);
                        }
                        else if (Cat == "PL")
                        {
                            category1 = "GL";                                                        // pay platinum price instead of Diamond
                            dt = VistaBOL.Select_Category_Price(filmcode, category1);
                            price = decimal.Parse(dt.Rows[0][0].ToString());
                            PayableAmount = decimal.Truncate(price * total_seats);
                        }
                        else if (Cat == "DM")
                        {
                            category1 = "PL";                                                        // pay platinum price instead of Diamond
                            dt = VistaBOL.Select_Category_Price(filmcode, category1);
                            price = decimal.Parse(dt.Rows[0][0].ToString());
                            PayableAmount = decimal.Truncate(price * total_seats);
                        }
                        else
                        {
                            PayableAmount = TotalAmount;
                        }
                        Session["PayableAmount"] = PayableAmount;
                        lblttlAmt.Visible = true;
                        lblpayAmt.Visible = true;
                        lblpayAmt.Text = "Total Payable Amount : Rs. " + Session["PayableAmount"].ToString();
                        lblttlAmt.Text = "Total Amount : Rs. " + Session["TotalAmount"].ToString();
                    }
                    else if (PromoSession.PromotionCode == "MMTDOMESTIC")
                    {
                        string Cat = strarr[4].ToString();
                        string filmcode = strarr[3].ToString();
                        decimal price;
                        decimal total_seats = decimal.Parse(strarr[5].ToString());
                        DataTable dt;

                        if ((Cat == "GL" || Cat == "PL" || Cat == "DM") && (day == "SUNDAY" || day == "SATURDAY"))
                        {
                            dt = VistaBOL.Select_Category_Price(filmcode, Cat);
                            price = decimal.Parse((dt.Rows[0][0].ToString())) - 1000;
                            PayableAmount = decimal.Truncate(price * total_seats);
                            dispercentage = decimal.Round(((1000 / decimal.Parse((dt.Rows[0][0].ToString()))) * 100), 2, MidpointRounding.AwayFromZero);
                            Session["discount"] = dispercentage;
                        }
                        else if ((Cat == "GL" || Cat == "PL" || Cat == "DM") && (day != "SUNDAY" && day != "SATURDAY"))
                        {
                            dt = VistaBOL.Select_Category_Price(filmcode, Cat);
                            price = decimal.Parse((dt.Rows[0][0].ToString())) - 500;
                            PayableAmount = decimal.Truncate(price * total_seats);
                            dispercentage = decimal.Round(((500 / decimal.Parse((dt.Rows[0][0].ToString()))) * 100), 2, MidpointRounding.AwayFromZero);
                            //dispercentage =decimal.Truncate((500 / decimal.Parse((dt.Rows[0][0].ToString()))) * 100);
                            Session["discount"] = dispercentage;
                        }
                        else
                        {
                            PayableAmount = TotalAmount;
                        }
                        Session["PayableAmount"] = PayableAmount;
                        lblttlAmt.Visible = true;
                        lblpayAmt.Visible = true;
                        lblpayAmt.Text = "Total Payable Amount : Rs. " + Session["PayableAmount"].ToString();
                        lblttlAmt.Text = "Total Amount : Rs. " + Session["TotalAmount"].ToString();
                    }
                    else if (PromoSession.PromotionCode.ToUpper() == "MANA")
                    {
                        if (strarr[11] != "" )
                        {
                            if (PromoSession.PromotionCode.ToUpper() == "MANA" && strarr[11] == "Weekend,Rs.4999")
                            {
                                lblttlAmt.Visible = true;
                                lblpayAmt.Visible = true;
                                decimal ManaTotalAmount = Convert.ToDecimal(Session["TotalAmount"].ToString()) + (1000 * Convert.ToDecimal(strarr[5].ToString()) / 4);
                                Session["PayableAmount"] = Convert.ToDecimal("4999") * Convert.ToDecimal(strarr[5].ToString()) / 4;
                                lblpayAmt.Text = "Total Payable Amount : Rs. " + decimal.Truncate(Convert.ToDecimal(Session["PayableAmount"].ToString()));
                                lblttlAmt.Text = "Total Amount : Rs. " + decimal.Truncate(ManaTotalAmount);
                            }
                            else if (PromoSession.PromotionCode.ToUpper() == "MANA" && strarr[11] == "Weekday,Rs.3999")
                            {
                                lblttlAmt.Visible = true;
                                lblpayAmt.Visible = true;
                                decimal ManaTotalAmount = Convert.ToDecimal(Session["TotalAmount"].ToString()) + (1000 * Convert.ToDecimal(strarr[5].ToString()) / 4);
                                Session["PayableAmount"] = Convert.ToDecimal("3999") * Convert.ToDecimal(strarr[5].ToString()) / 4;
                                lblpayAmt.Text = "Total Payable Amount : Rs. " + decimal.Truncate(Convert.ToDecimal(Session["PayableAmount"].ToString()));
                                lblttlAmt.Text = "Total Amount : Rs. " + decimal.Truncate(ManaTotalAmount);
                            }
                        }
                        else
                        {
                            Session.Abandon();
                            ClientScript.RegisterStartupScript(GetType(), "myscript", "<script>alert('Session Timeout. Please start" +
                " the transaction again');window.location.href='Default.aspx';</script>");
                        }
                    }
                    else if (PromoSession.PromotionCode.ToUpper() == "FAMILYOFFER")
                    {
                        if (strarr[11] != "")
                        {
                            if (PromoSession.PromotionCode.ToUpper() == "FAMILYOFFER" && strarr[11] == "Weekend-Rs.13196")
                            {
                                lblttlAmt.Visible = true;
                                lblpayAmt.Visible = true;
                                decimal FamilyTotalAmount = Convert.ToDecimal(Session["TotalAmount"].ToString()) + (3200 * Convert.ToDecimal(strarr[5].ToString()) / 4);
                                // decimal FamilyTotalAmount = Convert.ToDecimal("13200") * Convert.ToDecimal(strarr[5].ToString()) / 4;
                                Session["PayableAmount"] = Convert.ToDecimal("7999") * Convert.ToDecimal(strarr[5].ToString()) / 4;
                                lblpayAmt.Text = "Total Payable Amount : Rs. " + decimal.Truncate(Convert.ToDecimal(Session["PayableAmount"].ToString()));
                                lblttlAmt.Text = "Total Amount : Rs. " + decimal.Truncate(FamilyTotalAmount);
                            }
                            else if (PromoSession.PromotionCode.ToUpper() == "FAMILYOFFER" && strarr[11] == "Weekday-Rs.9796")
                            {
                                lblttlAmt.Visible = true;
                                lblpayAmt.Visible = true;
                                decimal FamilyTotalAmount = Convert.ToDecimal(Session["TotalAmount"].ToString()) + (2600 * Convert.ToDecimal(strarr[5].ToString()) / 4);
                                //decimal FamilyTotalAmount = Convert.ToDecimal("8600") * Convert.ToDecimal(strarr[5].ToString()) / 4;
                                Session["PayableAmount"] = Convert.ToDecimal("4999") * Convert.ToDecimal(strarr[5].ToString()) / 4;
                                lblpayAmt.Text = "Total Payable Amount : Rs. " + decimal.Truncate(Convert.ToDecimal(Session["PayableAmount"].ToString()));
                                lblttlAmt.Text = "Total Amount : Rs. " + decimal.Truncate(FamilyTotalAmount);
                            }
                        }
                        else
                        {
                            Session.Abandon();
                            ClientScript.RegisterStartupScript(GetType(), "myscript", "<script>alert('Session Timeout. Please start" +
                " the transaction again');window.location.href='Default.aspx';</script>");
                        }
                    }
                    else if (PromoSession.PromotionCode.ToUpper() == "MMT")
                    {
                        if (strarr[11] != "")
                        {
                            if (PromoSession.PromotionCode == "MMT" && strarr[11]== "4500")
                            {
                                lblttlAmt.Visible = true;
                                lblpayAmt.Visible = true;
                                decimal TtlAmount = Convert.ToDecimal(strarr[5].ToString()) * Convert.ToDecimal("6000");
                                decimal PayAmount = Convert.ToDecimal("4500") * Convert.ToDecimal(strarr[5].ToString());
                                lblpayAmt.Text = "Total Payable Amount : Rs. " + PayAmount.ToString();
                                lblttlAmt.Text = "Total Amount : Rs. " + TtlAmount.ToString();
                            }
                        }
                        else
                        {
                            Session.Abandon();
                            ClientScript.RegisterStartupScript(GetType(), "myscript", "<script>alert('Session Timeout. Please start" +
                " the transaction again');window.location.href='Default.aspx';</script>");
                        }
                    }
                    else if (PromoSession.PromotionCode.ToUpper() == "OCTOBERFEST")
                    {
                        lblttlAmt.Visible = true;
                        lblpayAmt.Visible = true;
                        lblpayAmt.Text = "Total Payable Amount : Rs. " + (TotalAmount/2).ToString();
                        lblttlAmt.Text = "Total Amount : Rs. " + TotalAmount.ToString();
                    }
                    else if (PromoSession.PromotionCode.ToUpper() == "JHUMROOOFFER")
                    {
                        lblttlAmt.Visible = true;
                        lblpayAmt.Visible = true;
                        lblpayAmt.Text = "Total Payable Amount : Rs. " + (TotalAmount / 2).ToString();
                        lblttlAmt.Text = "Total Amount : Rs. " + TotalAmount.ToString();
                    }


                    //    else if (PromoSession.PromotionCode.ToUpper() == "MCOTHERS" && strarr[11] != "")
                    //    {
                    //        if (strarr[11] != "")
                    //        {
                    //            if (PromoSession.PromotionCode == "MCOTHERS" && strarr[11] == "5597")
                    //            {
                    //                lblttlAmt.Visible = true;
                    //                lblpayAmt.Visible = true;
                    //                decimal TtlAmount = (Convert.ToDecimal(strarr[5].ToString())/2) * Convert.ToDecimal("6996");
                    //                decimal PayAmount = Convert.ToDecimal("5597") * (Convert.ToDecimal(strarr[5].ToString())/2);
                    //                lblpayAmt.Text = "Total Payable Amount : Rs. " + PayAmount.ToString();
                    //                lblttlAmt.Text = "Total Amount : Rs. " + TtlAmount.ToString();
                    //            }
                    //        }
                    //        else
                    //        {
                    //            Session.Abandon();
                    //            ClientScript.RegisterStartupScript(GetType(), "myscript", "<script>alert('Session Timeout. Please start" +
                    //" the transaction again');window.location.href='Default.aspx';</script>");
                    //        }
                    //    }
                    else
                    {
                        if (PromoSession.PromotionCode == "YATRA")
                        {
                            if (strarr[4].ToString() == "GL" || strarr[4].ToString() == "SL")
                            {
                                PromoSession.DiscountPercentage = 15;
                            }
                            if (strarr[4].ToString() == "DM" || strarr[4].ToString() == "PL")
                            {
                                PromoSession.DiscountPercentage = 20;
                            }
                        }
                        if (PromoSession.PromotionCode == "MCWORLD")
                        {
                            if (strarr[4].ToString() == "PL")
                            {
                                PromoSession.DiscountPercentage = 15;
                            }
                            if (strarr[4].ToString() == "DM")
                            {
                                PromoSession.DiscountPercentage = 25;
                            }
                        }
                        if (PromoSession.PromotionCode == "MCOTHERS")
                        {
                            if (strarr[4].ToString() == "GL")
                            {
                                PromoSession.DiscountPercentage = 20;
                            }
                            if (strarr[4].ToString() == "SL")
                            {
                                PromoSession.DiscountPercentage = 15;
                            }
                        }
                        for (int i = 0; i < Convert.ToInt16(strarr[5].ToString()); i++)
                        {
                            decimal SinglePrice = Convert.ToDecimal(SingleSeatPrice.ToString());
                            decimal DiscountedPrice = SinglePrice - (SinglePrice * PromoSession.DiscountPercentage / 100);
                            DiscountedPrice = decimal.Truncate(DiscountedPrice);
                            if (DiscountedPrice == 1274)
                                DiscountedPrice = DiscountedPrice + 1;
                            else if (DiscountedPrice == 2124)
                                DiscountedPrice = DiscountedPrice + 1;
                            else if (DiscountedPrice == 2974)
                                DiscountedPrice = DiscountedPrice + 1;
                            else if (DiscountedPrice == 4249)
                                DiscountedPrice = DiscountedPrice + 1;
                            PayableAmount += DiscountedPrice;
                        }
                        Session["PayableAmount"] = PayableAmount;
                        lblttlAmt.Visible = true;
                        lblpayAmt.Visible = true;
                        lblpayAmt.Text = "Total Payable Amount : Rs. " + Session["PayableAmount"].ToString();
                        lblttlAmt.Text = "Total Amount : Rs. " + Session["TotalAmount"].ToString();
                    }
                    if (strarr[11] != "")
                    {
                        if (strarr[11] == "Rs.1275")
                        {
                            lblttlAmt.Visible = true;
                            decimal Amount = Convert.ToDecimal("1275") * Convert.ToDecimal(strarr[5].ToString());
                            lblttlAmt.Text = "Total Payable Amount : Rs. " + Amount;
                        }
                        else if (strarr[11] == "Rs.4999")
                        {
                            lblttlAmt.Visible = true;
                            decimal Amount = Convert.ToDecimal("4999") * Convert.ToDecimal(strarr[5].ToString()) / 2;
                            lblttlAmt.Text = "Total Payable Amount : Rs. " + Amount;
                        }
                    }
            }
        }
        else
        {
            lblttlAmt.Visible = false;
            lblpayAmt.Visible = false;
            if (sessionvalue[12] != null)
            {
                String KeyNo = Decrypt(sessionvalue[12]);
                GTICKBOL.ON_Session_out(KeyNo);
            }
            Response.Redirect("Default.aspx", false);
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        //string ShowDate = "";
        //string[] datarr1 = sessionvalue[2].ToString().Split('/');
        //ShowDate = datarr1[1] + "/" + datarr1[0] + "/" + datarr1[2];
        //String Enddate = "2014.04.10";
        //DateTime End = Convert.ToDateTime(Enddate);
        //String Presentdate = Convert.ToDateTime(ShowDate).ToString("yyyy-MM-dd");
        //DateTime Present = Convert.ToDateTime(Presentdate);
        if (Decrypt(Request.QueryString["SessionId"]) == sessionvalue[12].ToString()&& Session[Decrypt(Request.QueryString["SessionId"])] != "")
        {
            long transid = 0;
            TransactionRecord tr = new TransactionRecord();
            tr.Street = txt_street.Text;
            tr.Pin = txt_pin.Text;
            tr.Country = ddl_country.SelectedValue;
            string tital = Ddl_title.SelectedValue;
            string fname = Txtfname.Text;
            string mname = Txtmname.Text;
            string lname = Txtlname.Text;
            string city = Txtcity.Text;
            string state = Txtstate.Text;
            string country = txt_country.SelectedValue;

            try
            {
                #region Session based
                if (Session_value != "" && Request.QueryString["SessionId"] != null)
                {
                    try
                    {
                        string[] strarr;
                        strarr = Session_value.Split(',');
                        Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write(string.Format("Transaction [{0}]  ", strarr[12]));
                        tr.BookingID = long.Parse(strarr[12]);
                        Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write(string.Format("Seats  " + Session_value));
                        if (Session["AgentCode"] != null)
                        {
                            tr.AgentCode = Session["AgentCode"].ToString();
                            tr.Source = "MSAGENT";
                        }
                        else
                        {
                            tr.AgentCode = "WEB";
                            tr.Source = "WEB";
                        }
                        tr.BookingType = "INDIVIDUAL";
                        //tr.VoucherType = rblVoucher.SelectedValue;
                        //tr.VoucherNo = "";
                        //tr.VoucherBookingID = 0;

                        tr.CardType = rbl_CardType.SelectedItem.Text;
                        tr.PaymentGateway = rbl_CardType.SelectedValue;
                        tr.CardNo = "1111222233334444";
                        tr.MobileNo = txtContactNo.Text;
                        tr.Name = txtName.Text;
                        tr.PaymentType = ddlPaymentMode.SelectedItem.Text;
                        tr.DateOfBooking = DateTime.Now.Date.ToShortDateString();
                        tr.IsProcessed = 0;
                        tr.PaymentStatus = 0;
                        //*************if routed from other website***********
                        if (Session["Router"] != null)
                        {
                            tr.router = Session["Router"].ToString();
                        }
                        else
                        {
                            tr.router = "";
                        }
                        //*****************************************************
                        bool istrue = false;
                        Session["Complimentary"] = istrue;
                        Session["ComplimentaryDrop"] = istrue;
                        tr.WantComplimentary = istrue;
                        tr.WantComplimentaryDrop = istrue;
                        tr.PlaceOfDrop = "";
                        tr.PlaceOfPick = "";
                        tr.TimeOfPick = drpHrs.SelectedItem.Text + ":" + drpMins.SelectedItem.Text + " " + drp_Shift.SelectedItem.Text;
                        tr.TimeOfDrop = timed_hours.SelectedItem.Text + ":" + timed_minutes.SelectedItem.Text + " " + timed_formate.SelectedItem.Text;
                        if (strarr[10] == "PULMAN" && chkGCab.Checked)
                        {
                            tr.PlaceOfPick = "PULLMAN GURGAON";
                            tr.TimeOfPick = drpHrs.SelectedItem.Text + ":" + drpMins.SelectedItem.Text + " " + drp_Shift.SelectedItem.Text;
                            istrue = chkGCab.Checked;
                            Session["Complimentary"] = istrue;
                            tr.WantComplimentary = istrue;
                        }
                        if (strarr[10].ToString() == "LEELAKEMPINSKI" && chkGCab.Checked)
                        {
                            tr.PlaceOfPick = "LEELA KEMPINSKI GURGAON";
                            tr.TimeOfPick = drpHrs.SelectedItem.Text + ":" + drpMins.SelectedItem.Text + " " + drp_Shift.SelectedItem.Text;
                            istrue = chkGCab.Checked;
                            Session["Complimentary"] = istrue;
                            tr.WantComplimentary = istrue;
                        }
                        if (strarr[10].ToString() == "CROWNPLAZA" && (chkGCabPick.Checked || chkGCabDrop.Checked))
                        {
                            if (chkGCabPick.Checked)
                            {
                                tr.PlaceOfPick = "CROWNPLAZA GURGAON";
                                tr.TimeOfPick = timep_hours.SelectedItem.Text + ":" + timep_minutes.SelectedItem.Text + " " + timep_formate.SelectedItem.Text;
                                istrue = chkGCabPick.Checked;
                                Session["Complimentary"] = istrue;
                                tr.WantComplimentary = istrue;
                            }
                            if (chkGCabDrop.Checked)
                            {
                                tr.PlaceOfDrop = "KINGDOM OF DREAMS GURGAON";
                                tr.TimeOfDrop = timed_hours.SelectedItem.Text + ":" + timed_minutes.SelectedItem.Text + " " + timed_formate.SelectedItem.Text;
                                istrue = chkGCabDrop.Checked;
                                Session["ComplimentaryDrop"] = istrue;
                                tr.WantComplimentaryDrop = istrue;
                            }
                        }



                        tr.EmailID = txtEmailAddress.Text;
                        tr.Status = false;
                        tr.TimeOfBooking = DateTime.Now.ToShortTimeString();
                        tr.TotalSeats = int.Parse(strarr[5].ToString());
                        tr.Category = strarr[8];
                        tr.Location = strarr[6];
                        tr.Play = strarr[1];

                        string[] datarr = strarr[2].ToString().Split('/');//for live server
                        //string[] datarr = strarr[2].ToString().Split('-');// for dev/local

                        tr.ShowDate = datarr[1] + "/" + datarr[0] + "/" + datarr[2];//for live server
                        //tr.ShowDate = datarr[0] + "/" + datarr[1] + "/" + datarr[2]; // for dev/local


                        tr.ShowTime = strarr[7];
                        tr.Day = Convert.ToDateTime(tr.ShowDate).DayOfWeek.ToString();
                        //tr.ShowDate = datarr[1] + "/" + datarr[0] + "/" + datarr[2];//for local
                        tr.Remark = "";
                        tr.TotalAmount = GTICKBOL.Get_SeatPrice_SeatKeyNoWise(tr.BookingID);
                        tr.SeatInfo = SeatVal;
                        tr.Address = txtAddress.Text.Trim();
                        tr.IP = GetIP();
                        Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Date : " + tr.DateOfBooking);
                        //******Promotion code related changes START*****


                        if (strarr[10] != "")
                        {
                            KoDTicketingLibrary.DTO.Promotion PromoSession = (KoDTicketingLibrary.DTO.Promotion)Session[strarr[10]];
                            if (PromoSession.PromotionCode == "VIVANTABYTAJ")
                            {
                                string Cat = strarr[4].ToString();
                                string category;
                                string filmcode = strarr[3].ToString();
                                decimal price;
                                DataTable dt;
                                if (Cat == "DM")
                                {
                                    category = "GL"; // pay gold price instead of diamond
                                    dt = VistaBOL.Select_Category_Price(filmcode, category);
                                    price = decimal.Parse(dt.Rows[0][0].ToString());
                                    tr.DiscountedAmount = price * (tr.TotalSeats);
                                    tr.PayableAmount = tr.DiscountedAmount;
                                }
                                else
                                {
                                    tr.PayableAmount = tr.TotalAmount;
                                }
                            }
                            else if (PromoSession.PromotionCode == "TRIDENT" || PromoSession.PromotionCode == "OBEROI" || PromoSession.PromotionCode == "CROWNEPLAZAROHINI")                       //for oberoi and trident hotel
                            {
                                string Cat = strarr[4].ToString();
                                string category;
                                string filmcode = strarr[3].ToString();
                                decimal price;
                                DataTable dt;
                                if (Cat == "PL")
                                {
                                    category = "GL";                                                        // pay gold price instead of Platinum
                                    dt = VistaBOL.Select_Category_Price(filmcode, category);
                                    price = decimal.Parse(dt.Rows[0][0].ToString());
                                    tr.DiscountedAmount = price * (tr.TotalSeats);
                                    tr.PayableAmount = tr.DiscountedAmount;
                                }
                                else if (Cat == "DM")
                                {
                                    category = "PL";                                                        // pay platinum price instead of Diamond
                                    dt = VistaBOL.Select_Category_Price(filmcode, category);
                                    price = decimal.Parse(dt.Rows[0][0].ToString());
                                    tr.DiscountedAmount = price * (tr.TotalSeats);
                                    tr.PayableAmount = tr.DiscountedAmount;
                                }
                                else
                                {
                                    tr.PayableAmount = tr.TotalAmount;
                                }

                            }
                            else if (PromoSession.PromotionCode == "OBEROIDELHI")                       //for oberoi and trident hotel
                            {
                                string Cat = strarr[4].ToString();
                                string category;
                                string filmcode = strarr[3].ToString();
                                decimal price;
                                DataTable dt;
                                if (Cat == "GL")
                                {
                                    category = "SL";                                                        // pay gold price instead of Platinum
                                    dt = VistaBOL.Select_Category_Price(filmcode, category);
                                    price = decimal.Parse(dt.Rows[0][0].ToString());
                                    tr.DiscountedAmount = price * (tr.TotalSeats);
                                    tr.PayableAmount = tr.DiscountedAmount;
                                }
                                else if (Cat == "PL")
                                {
                                    category = "GL";                                                        // pay platinum price instead of Diamond
                                    dt = VistaBOL.Select_Category_Price(filmcode, category);
                                    price = decimal.Parse(dt.Rows[0][0].ToString());
                                    tr.DiscountedAmount = price * (tr.TotalSeats);
                                    tr.PayableAmount = tr.DiscountedAmount;
                                }
                                else if (Cat == "DM")
                                {
                                    category = "PL";                                                        // pay platinum price instead of Diamond
                                    dt = VistaBOL.Select_Category_Price(filmcode, category);
                                    price = decimal.Parse(dt.Rows[0][0].ToString());
                                    tr.DiscountedAmount = price * (tr.TotalSeats);
                                    tr.PayableAmount = tr.DiscountedAmount;
                                }
                                else
                                {
                                    tr.PayableAmount = tr.TotalAmount;
                                }

                            }
                            else if (PromoSession.PromotionCode == "MMTDOMESTIC")
                            {
                                string Cat = strarr[4].ToString();
                                string filmcode = strarr[3].ToString();
                                decimal price;
                                DataTable dt;
                                if ((Cat == "GL" || Cat == "PL" || Cat == "DM") && (Session["day"].ToString() == "SUNDAY" || Session["day"].ToString() == "SATURDAY"))
                                {
                                    dt = VistaBOL.Select_Category_Price(filmcode, Cat);
                                    price = decimal.Parse((dt.Rows[0][0]).ToString()) - 1000;
                                    tr.DiscountedAmount = decimal.Truncate(price * (tr.TotalSeats));
                                    tr.PayableAmount = decimal.Truncate(tr.DiscountedAmount);
                                }
                                else if ((Cat == "GL" || Cat == "PL" || Cat == "DM") && (Session["day"].ToString() != "SUNDAY" && Session["day"].ToString() != "SATURDAY"))
                                {
                                    dt = VistaBOL.Select_Category_Price(filmcode, Cat);
                                    price = decimal.Parse((dt.Rows[0][0].ToString())) - 500;
                                    tr.DiscountedAmount = decimal.Truncate(price * (tr.TotalSeats));
                                    tr.PayableAmount = decimal.Truncate(tr.DiscountedAmount);
                                }
                                else
                                {
                                    tr.PayableAmount = tr.TotalAmount;
                                }
                            }
                            else if (PromoSession.PromotionCode == "MANA" && strarr[11] != "")
                            {
                                if (strarr[11] == "Weekday,Rs.3999" && PromoSession.PromotionCode == "MANA")
                                {
                                    decimal amount = 3999 * (Convert.ToDecimal(Session["NoofPackages"]));
                                    tr.DiscountedAmount = (GTICKBOL.Get_SeatPrice_SeatKeyNoWise(tr.BookingID) - (GTICKBOL.Get_SeatPrice_SeatKeyNoWise(tr.BookingID) * PromoSession.DiscountPercentage / 100));
                                    tr.PayableAmount = amount;
                                }
                                else if (strarr[11] == "Weekend,Rs.4999" && PromoSession.PromotionCode == "MANA")
                                {
                                    decimal amount = 4999 * (Convert.ToDecimal(Session["NoofPackages"]));
                                    tr.DiscountedAmount = (GTICKBOL.Get_SeatPrice_SeatKeyNoWise(tr.BookingID) - (GTICKBOL.Get_SeatPrice_SeatKeyNoWise(tr.BookingID) * PromoSession.DiscountPercentage / 100));
                                    tr.PayableAmount = amount;
                                }
                            }
                            else if (PromoSession.PromotionCode == "FAMILYOFFER" && strarr[11] != "")
                            {
                                if (strarr[11] == "Weekday-Rs.9796" && PromoSession.PromotionCode == "FAMILYOFFER")
                                {
                                    //decimal discountamount = ((GTICKBOL.Get_SeatPrice_SeatKeyNoWise(tr.BookingID)) - ((Convert.ToDecimal(Session["TotalAmount"].ToString()) + (2600 * Convert.ToDecimal(strarr[5].ToString()) / 4)) - 4999)) / tr.TotalSeats;
                                    PromoSession.DiscountPercentage = decimal.Round((((GTICKBOL.Get_SeatPrice_SeatKeyNoWise(tr.BookingID) / tr.TotalSeats) - 600) / ((GTICKBOL.Get_SeatPrice_SeatKeyNoWise(tr.BookingID) / tr.TotalSeats)) * 100), 2, MidpointRounding.AwayFromZero);
                                    // PromoSession.DiscountPercentage = decimal.Round((((GTICKBOL.Get_SeatPrice_SeatKeyNoWise(tr.BookingID) - 4797)) / ((GTICKBOL.Get_SeatPrice_SeatKeyNoWise(tr.BookingID) / tr.TotalSeats)) * 100), 2, MidpointRounding.AwayFromZero);
                                    decimal amount = 2400 * (Convert.ToDecimal(Session["NoofPackages"]));
                                    tr.DiscountedAmount = decimal.Round(GTICKBOL.Get_SeatPrice_SeatKeyNoWise(tr.BookingID) - (GTICKBOL.Get_SeatPrice_SeatKeyNoWise(tr.BookingID) * PromoSession.DiscountPercentage / 100));
                                    tr.PayableAmount = amount;
                                }
                                else if (strarr[11] == "Weekend-Rs.13196" && PromoSession.PromotionCode == "FAMILYOFFER")
                                {
                                    //decimal discountamount = ((GTICKBOL.Get_SeatPrice_SeatKeyNoWise(tr.BookingID)) - ((Convert.ToDecimal(Session["TotalAmount"].ToString()) + (3200 * Convert.ToDecimal(strarr[5].ToString()) / 4)) - 7999)) / tr.TotalSeats;
                                    PromoSession.DiscountPercentage = decimal.Round((((GTICKBOL.Get_SeatPrice_SeatKeyNoWise(tr.BookingID) / tr.TotalSeats) - 1200) / ((GTICKBOL.Get_SeatPrice_SeatKeyNoWise(tr.BookingID) / tr.TotalSeats)) * 100), 2, MidpointRounding.AwayFromZero);
                                    //PromoSession.DiscountPercentage = decimal.Round((((GTICKBOL.Get_SeatPrice_SeatKeyNoWise(tr.BookingID) / tr.TotalSeats) - discountamount) / ((GTICKBOL.Get_SeatPrice_SeatKeyNoWise(tr.BookingID) / tr.TotalSeats)) * 100), 2, MidpointRounding.AwayFromZero);
                                    decimal amount = 4800 * (Convert.ToDecimal(Session["NoofPackages"]));
                                    tr.DiscountedAmount = decimal.Round(GTICKBOL.Get_SeatPrice_SeatKeyNoWise(tr.BookingID) - (GTICKBOL.Get_SeatPrice_SeatKeyNoWise(tr.BookingID) * PromoSession.DiscountPercentage / 100));
                                    tr.PayableAmount = amount;
                                }
                            }
                            else if (PromoSession.PromotionCode == "MMT")
                            {
                                if (strarr[11] != "")
                                {
                                    if (strarr[11] == "4500" || PromoSession.PromotionCode == "MMT")
                                    {
                                        decimal amount = 4500 * (Convert.ToDecimal(Session["NoofPackages"]));
                                        tr.DiscountedAmount = (GTICKBOL.Get_SeatPrice_SeatKeyNoWise(tr.BookingID) - (GTICKBOL.Get_SeatPrice_SeatKeyNoWise(tr.BookingID) * PromoSession.DiscountPercentage / 100));
                                        tr.PayableAmount = amount;
                                    }
                                }
                            }
                            else if (PromoSession.PromotionCode == "OCTOBERFEST")
                            {
                                tr.DiscountedAmount = (GTICKBOL.Get_SeatPrice_SeatKeyNoWise(tr.BookingID) - (GTICKBOL.Get_SeatPrice_SeatKeyNoWise(tr.BookingID) * PromoSession.DiscountPercentage / 100));
                                tr.PayableAmount = tr.DiscountedAmount;
                            }
                            else if (PromoSession.PromotionCode == "JHUMROOOFFER")
                            {
                                tr.DiscountedAmount = (GTICKBOL.Get_SeatPrice_SeatKeyNoWise(tr.BookingID) - (GTICKBOL.Get_SeatPrice_SeatKeyNoWise(tr.BookingID) * PromoSession.DiscountPercentage / 100));
                                tr.PayableAmount = tr.DiscountedAmount;
                            }

                            //else if (PromoSession.PromotionCode == "MCOTHERS" && strarr[11] != "")
                            //{
                            //    if (strarr[11] != "")
                            //    {
                            //        if (strarr[11] == "5597" || PromoSession.PromotionCode == "MCOTHERS")
                            //        {
                            //            tr.DiscountPercentage = 20;
                            //            PromoSession.DiscountPercentage = 20;
                            //            decimal amount = 5597 * (Convert.ToDecimal(Session["MCOTHERSNOOFPACKAGE"]));
                            //            tr.DiscountedAmount = (GTICKBOL.Get_SeatPrice_SeatKeyNoWise(tr.BookingID) - (GTICKBOL.Get_SeatPrice_SeatKeyNoWise(tr.BookingID) * PromoSession.DiscountPercentage / 100));
                            //            tr.PayableAmount = amount;
                            //        }
                            //    }
                            //}
                            else
                            {
                                if (PromoSession.PromotionCode == "YATRA")
                                {
                                    if (strarr[4].ToString() == "SL" || strarr[4].ToString() == "GL")
                                    {
                                        PromoSession.DiscountPercentage = 15;
                                    }
                                    if (strarr[4].ToString() == "PL" || strarr[4].ToString() == "DM")
                                    {
                                        PromoSession.DiscountPercentage = 20;
                                    }
                                }
                                if (PromoSession.PromotionCode == "MCWORLD")
                                {
                                    if (strarr[4].ToString() == "PL")
                                    {
                                        PromoSession.DiscountPercentage = 15;
                                    }
                                    if (strarr[4].ToString() == "DM")
                                    {
                                        PromoSession.DiscountPercentage = 25;
                                    }
                                }
                                if (PromoSession.PromotionCode == "MCOTHERS")
                                {
                                    if (strarr[4].ToString() == "GL")
                                    {
                                        PromoSession.DiscountPercentage = 20;
                                    }
                                    if (strarr[4].ToString() == "SL")
                                    {
                                        PromoSession.DiscountPercentage = 15;
                                    }
                                }
                                tr.DiscountedAmount = (GTICKBOL.Get_SeatPrice_SeatKeyNoWise(tr.BookingID) - (GTICKBOL.Get_SeatPrice_SeatKeyNoWise(tr.BookingID) * PromoSession.DiscountPercentage / 100));
                            }
                            tr.PromotionCode = PromoSession.PromotionCode;
                            tr.DiscountPercentage = PromoSession.DiscountPercentage;
                            tr.WebPromotionId = PromoSession.WebPromotionId;
                        }
                        if (sessionvalue[1].ToString() == "NEW YEAR")
                        {
                            tr.PromotionCode = "NEW YEAR PARTY";
                        }
                        //******Promotion code related changes END*****
                        //******Jhumroo Offer (1+1) code related changes*****
                        //if (strarr[1].ToString() == "JHUMROO" && (strarr[4].ToString() == "GLY" || strarr[4].ToString() == "SL" || strarr[4].ToString() == "GL" || strarr[4].ToString() == "PL" || strarr[4].ToString() == "DM") && strarr[10].ToString() == "" && Present <= End)
                        //{
                        //    tr.DiscountedAmount = Convert.ToDecimal(Session["PayableAmount"].ToString());
                        //    tr.PayableAmount = tr.DiscountedAmount;
                        //    tr.DiscountPercentage = 100 - decimal.Round(((tr.PayableAmount * 100) / (GTICKBOL.Get_SeatPrice_SeatKeyNoWise(tr.BookingID))), 2, MidpointRounding.AwayFromZero);
                        //}
                        //******Jhumroo Offer (1+1) code related changes END*****
                        transid = TransactionBOL.Transaction_Temp_Insert(tr);
                        GTICKV.LogEntry(tr.BookingID.ToString(), "Starting to write Information to temp Session Table", "7", "");

                        if ((strarr[11] != "" && strarr[10] != "FAMILYOFFER") || (strarr[10] == "OCTOBERFEST" || strarr[10] == "JHUMROOOFFER"))
                            {
                                transid = GTICKBOL.MarchPromotionTransactionCounter_Max(transid);
                            }
                            if (sessionvalue[1].ToString() == "NEW YEAR")
                            {
                                transid = GTICKBOL.MarchPromotionTransactionCounter_Max(transid);
                            }
                        
                    }
                    catch (Exception ex)
                    {
                        Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Transaction Preparation Error: " + ex.Message);
                        ClientScript.RegisterStartupScript(GetType(), "myscript", "<script>alert('Session Timeout. Please start the transaction again');window.location.href='Default.aspx';</script>");
                    }
                    GTICKV.LogEntry(tr.BookingID.ToString(), "Category : " + tr.Category + " ,Seat Info : " + tr.SeatInfo +
                        ", Total Amt : " + tr.TotalAmount, "8", "");

                    // ******Promotion code send discounted AMOUNT to payment gateway changes START*****
                    if (sessionvalue[10] != "" || sessionvalue[10] != "")
                    {
                        KoDTicketingLibrary.DTO.Promotion ObjPromoSession = (KoDTicketingLibrary.DTO.Promotion)Session[sessionvalue[10]];
                        string[] strarr = Session_value.Split(',');
                        if (ObjPromoSession.PromotionCode == "VIVANTABYTAJ")
                        {
                            string Cat = strarr[4].ToString();
                            string category;
                            string filmcode = strarr[3].ToString();
                            decimal price;
                            DataTable dt;
                            if (Cat == "DM")
                            {
                                category = "GL"; // pay gold price instead of Platinum
                                dt = VistaBOL.Select_Category_Price(filmcode, category);
                                price = decimal.Parse(dt.Rows[0][0].ToString());
                                tr.TotalAmount = price * (tr.TotalSeats);
                                for (int i = 0; i < tr.TotalSeats; i++)
                                    Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Discounted Price For a Ticket" + tr.TotalAmount.ToString());
                            }
                        }
                        else if (ObjPromoSession.PromotionCode == "TRIDENT" || ObjPromoSession.PromotionCode == "OBEROI" || ObjPromoSession.PromotionCode == "CROWNEPLAZAROHINI")                    //for oberoi and trident hotel
                        {
                            string Cat = strarr[4].ToString();
                            string category;
                            string filmcode = strarr[3].ToString();
                            decimal price, DiscountedPrice;
                            DataTable dt;
                            if (Cat == "PL")
                            {
                                category = "GL";                                                      // pay gold price instead of Platinum
                                dt = VistaBOL.Select_Category_Price(filmcode, category);
                                price = decimal.Parse(dt.Rows[0][0].ToString());
                                DiscountedPrice = price;
                                tr.TotalAmount = price * (tr.TotalSeats);
                                for (int i = 0; i < tr.TotalSeats; i++)
                                    Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Discounted Price For a Ticket" + DiscountedPrice.ToString());
                            }
                            else if (Cat == "DM")
                            {
                                category = "PL";                                                      // pay platinum price instead of Diamond
                                dt = VistaBOL.Select_Category_Price(filmcode, category);
                                price = decimal.Parse(dt.Rows[0][0].ToString());
                                DiscountedPrice = price;
                                tr.TotalAmount = price * (tr.TotalSeats);
                                for (int i = 0; i < tr.TotalSeats; i++)
                                    Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Discounted Price For a Ticket" + DiscountedPrice.ToString());
                            }
                        }
                        else if (ObjPromoSession.PromotionCode == "OBEROIDELHI")                    //for oberoi and trident hotel
                        {
                            string Cat = strarr[4].ToString();
                            string category;
                            string filmcode = strarr[3].ToString();
                            decimal price, DiscountedPrice;
                            DataTable dt;
                            if (Cat == "GL")
                            {
                                category = "SL";                                                      // pay gold price instead of Platinum
                                dt = VistaBOL.Select_Category_Price(filmcode, category);
                                price = decimal.Parse(dt.Rows[0][0].ToString());
                                DiscountedPrice = price;
                                tr.TotalAmount = price * (tr.TotalSeats);
                                for (int i = 0; i < tr.TotalSeats; i++)
                                    Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Discounted Price For a Ticket" + DiscountedPrice.ToString());
                            }
                            else if (Cat == "PL")
                            {
                                category = "GL";                                                      // pay platinum price instead of Diamond
                                dt = VistaBOL.Select_Category_Price(filmcode, category);
                                price = decimal.Parse(dt.Rows[0][0].ToString());
                                DiscountedPrice = price;
                                tr.TotalAmount = price * (tr.TotalSeats);
                                for (int i = 0; i < tr.TotalSeats; i++)
                                    Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Discounted Price For a Ticket" + DiscountedPrice.ToString());
                            }
                            else if (Cat == "DM")
                            {
                                category = "PL";                                                      // pay platinum price instead of Diamond
                                dt = VistaBOL.Select_Category_Price(filmcode, category);
                                price = decimal.Parse(dt.Rows[0][0].ToString());
                                DiscountedPrice = price;
                                tr.TotalAmount = price * (tr.TotalSeats);
                                for (int i = 0; i < tr.TotalSeats; i++)
                                    Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Discounted Price For a Ticket" + DiscountedPrice.ToString());
                            }
                        }
                        else if (ObjPromoSession.PromotionCode == "MMTDOMESTIC")
                        {
                            string Cat = strarr[4].ToString();
                            string filmcode = strarr[3].ToString();
                            decimal price;
                            decimal DiscountedPrice;
                            DataTable dt;
                            if ((Cat == "GL" || Cat == "PL" || Cat == "DM") && (Session["day"].ToString() == "SUNDAY" || Session["day"].ToString() == "SATURDAY"))
                            {
                                dt = VistaBOL.Select_Category_Price(filmcode, Cat);
                                price = decimal.Parse((dt.Rows[0][0].ToString())) - 1000;
                                DiscountedPrice = decimal.Truncate(price);
                                tr.TotalAmount = decimal.Truncate(price * (tr.TotalSeats));
                                for (int i = 0; i < tr.TotalSeats; i++)
                                    Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Discounted Price For a Ticket" + DiscountedPrice.ToString());
                            }
                            else if ((Cat == "GL" || Cat == "PL" || Cat == "DM") && (Session["day"].ToString() != "SUNDAY" && Session["day"].ToString() != "SATURDAY"))
                            {
                                dt = VistaBOL.Select_Category_Price(filmcode, Cat);
                                price = decimal.Parse((dt.Rows[0][0].ToString())) - 500;
                                DiscountedPrice = decimal.Truncate(price);
                                tr.TotalAmount = decimal.Truncate(price * (tr.TotalSeats));
                                for (int i = 0; i < tr.TotalSeats; i++)
                                    Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Discounted Price For a Ticket" + DiscountedPrice.ToString());
                            }

                        }
                        else if (ObjPromoSession.PromotionCode == "MANA")
                        {
                            if (strarr[11] != "")
                            {
                                if (ObjPromoSession.PromotionCode.ToUpper() == "MANA" && strarr[11] == "Weekday,Rs.3999")
                                {
                                    decimal amount = 3999 * (Convert.ToDecimal(Session["NoofPackages"]));
                                    tr.TotalAmount = amount;
                                    tr.PayableAmount = amount;
                                    string date4 = tr.ShowDate.ToString();
                                    DateTime day1 = Convert.ToDateTime(date4);
                                    Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("values Mana Weekday Package : " + Convert.ToInt16(Session["NoofPackages"]) + Convert.ToDecimal(Session["TotalAmount"].ToString()) + Convert.ToDecimal(Session["PayableAmount"].ToString()) + Convert.ToDateTime(tr.DateOfBooking.ToString()) + tr.BookingID.ToString() + day1 + tr.Name.ToString() + tr.EmailID.ToString() + tr.MobileNo.ToString() + tr.PaymentGateway.ToString() + false + "" + Session["PackageType"].ToString() + transid.ToString());
                                    transid = GTICKBOL.MANABooking_Details(Convert.ToInt16(Session["NoofPackages"]), Convert.ToDecimal(Session["TotalAmount"].ToString()), Convert.ToDecimal(Session["PayableAmount"].ToString()), Convert.ToDateTime(tr.DateOfBooking.ToString()), tr.BookingID.ToString(), day1, tr.Name.ToString(), tr.EmailID.ToString(), tr.MobileNo.ToString(), tr.PaymentGateway.ToString(), false, "", Session["PackageType"].ToString(), transid.ToString());
                                }
                                else if (strarr[11] == "Weekend,Rs.4999" && ObjPromoSession.PromotionCode == "MANA")
                                {

                                    decimal amount = 4999 * (Convert.ToDecimal(Session["NoofPackages"]));
                                    tr.TotalAmount = amount;
                                    tr.PayableAmount = amount;
                                    string date4 = tr.ShowDate.ToString();
                                    DateTime day1 = Convert.ToDateTime(date4);
                                    Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("values for Mana Weekend Package : " + Convert.ToInt16(Session["NoofPackages"]) + Convert.ToDecimal(Session["TotalAmount"].ToString()) + Convert.ToDecimal(Session["PayableAmount"].ToString()) + Convert.ToDateTime(tr.DateOfBooking.ToString()) + tr.BookingID.ToString() + day1 + tr.Name.ToString() + tr.EmailID.ToString() + tr.MobileNo.ToString() + tr.PaymentGateway.ToString() + false + "" + Session["PackageType"].ToString() + transid.ToString());
                                    transid = GTICKBOL.MANABooking_Details(Convert.ToInt16(Session["NoofPackages"]), Convert.ToDecimal(Session["TotalAmount"].ToString()), Convert.ToDecimal(Session["PayableAmount"].ToString()), Convert.ToDateTime(tr.DateOfBooking.ToString()), tr.BookingID.ToString(), day1, tr.Name.ToString(), tr.EmailID.ToString(), tr.MobileNo.ToString(), tr.PaymentGateway.ToString(), false, "", Session["PackageType"].ToString(), transid.ToString());
                                }
                            }
                        }
                        else if (ObjPromoSession.PromotionCode == "FAMILYOFFER")
                        {
                            if (strarr[11] != "")
                            {
                                if ((ObjPromoSession.PromotionCode.ToUpper() == "FAMILYOFFER" && strarr[11] == "Weekday-Rs.9796") && strarr[4] == "GL")
                                {
                                    decimal pcktotal = 9796 * (Convert.ToDecimal(Session["NoofPackages"]));
                                    decimal tckpaybale = 600 * tr.TotalSeats;
                                    Session["TicketPaybale"] = tckpaybale;
                                    Session["PackageTotal"] = pcktotal;
                                    decimal amount = 4999 * (Convert.ToDecimal(Session["NoofPackages"]));
                                    tr.TotalAmount = amount;
                                    tr.PayableAmount = amount;
                                    string date4 = tr.ShowDate.ToString();
                                    DateTime day1 = Convert.ToDateTime(date4);
                                    Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("values Family Offer Weekday Package : " + Convert.ToInt16(Session["NoofPackages"]) + Convert.ToDecimal(Session["TotalAmount"].ToString()) + Convert.ToDecimal(Session["PayableAmount"].ToString()) + Convert.ToDateTime(tr.DateOfBooking.ToString()) + tr.BookingID.ToString() + day1 + tr.Name.ToString() + tr.EmailID.ToString() + tr.MobileNo.ToString() + tr.PaymentGateway.ToString() + false + "" + Session["PackageType"].ToString() + transid.ToString());
                                    transid = GTICKBOL.FAMILYOFFERBooking_Details(Convert.ToInt16(Session["NoofPackages"]), Convert.ToDecimal(Session["TotalAmount"].ToString()), Convert.ToDecimal(Session["PayableAmount"].ToString()), Convert.ToDateTime(tr.DateOfBooking.ToString()), tr.BookingID.ToString(), day1, tr.Name.ToString(), tr.EmailID.ToString(), tr.MobileNo.ToString(), tr.PaymentGateway.ToString(), false, "", Session["PackageType"].ToString(), transid.ToString(), Session["royalno"].ToString(), Convert.ToDecimal(Session["PackageTotal"].ToString()), Convert.ToDecimal(Session["TicketPaybale"].ToString()));
                                }
                                else if ((strarr[11] == "Weekend-Rs.13196" && ObjPromoSession.PromotionCode == "FAMILYOFFER") && strarr[4] == "GL")
                                {
                                    decimal pcktotal = 13196 * (Convert.ToDecimal(Session["NoofPackages"]));
                                    Session["PackageTotal"] = pcktotal;
                                    decimal tckpaybale = 1200 * tr.TotalSeats;
                                    Session["TicketPaybale"] = tckpaybale;
                                    decimal amount = 7999 * (Convert.ToDecimal(Session["NoofPackages"]));
                                    tr.TotalAmount = amount;
                                    tr.PayableAmount = amount;
                                    string date4 = tr.ShowDate.ToString();
                                    DateTime day1 = Convert.ToDateTime(date4);
                                    Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("values for Family Offer Weekend Package : " + Convert.ToInt16(Session["NoofPackages"]) + Convert.ToDecimal(Session["TotalAmount"].ToString()) + Convert.ToDecimal(Session["PayableAmount"].ToString()) + Convert.ToDateTime(tr.DateOfBooking.ToString()) + tr.BookingID.ToString() + day1 + tr.Name.ToString() + tr.EmailID.ToString() + tr.MobileNo.ToString() + tr.PaymentGateway.ToString() + false + "" + Session["PackageType"].ToString() + transid.ToString());
                                    transid = GTICKBOL.FAMILYOFFERBooking_Details(Convert.ToInt16(Session["NoofPackages"]), Convert.ToDecimal(Session["TotalAmount"].ToString()), Convert.ToDecimal(Session["PayableAmount"].ToString()), Convert.ToDateTime(tr.DateOfBooking.ToString()), tr.BookingID.ToString(), day1, tr.Name.ToString(), tr.EmailID.ToString(), tr.MobileNo.ToString(), tr.PaymentGateway.ToString(), false, "", Session["PackageType"].ToString(), transid.ToString(), Session["royalno"].ToString(), Convert.ToDecimal(Session["PackageTotal"].ToString()), Convert.ToDecimal(Session["TicketPaybale"].ToString()));
                                }
                            }
                        }
                        else if (ObjPromoSession.PromotionCode == "MMT")
                        {
                            if (strarr[11] != "")
                            {
                                if (strarr[11] == "4500" || ObjPromoSession.PromotionCode == "MMT")
                                {
                                    decimal amount = 4500 * (Convert.ToDecimal(Session["NoofPackages"]));
                                    tr.TotalAmount = amount;
                                    tr.PayableAmount = amount;
                                    DateTime day1 = Convert.ToDateTime(tr.ShowDate.ToString());
                                    transid = GTICKBOL.MMTBooking_Details(Convert.ToInt16(Session["NoofPackages"]), Session["pnr"].ToString(), sessionvalue[10], Convert.ToDecimal(Session["TotalAmount"].ToString()), Convert.ToDecimal(Session["PayableAmount"].ToString()), Convert.ToDateTime(tr.DateOfBooking.ToString()), tr.BookingID.ToString(), day1, tr.Name.ToString(), tr.EmailID.ToString(), tr.MobileNo.ToString(), tr.PaymentGateway.ToString(), false, "", transid.ToString());
                                }
                            }
                        }
                        else if (ObjPromoSession.PromotionCode == "OCTOBERFEST")
                        {
                            tr.TotalAmount = tr.TotalAmount / 2;
                            tr.PayableAmount = tr.TotalAmount;  
                        }
                        else if (ObjPromoSession.PromotionCode == "JHUMROOOFFER")
                        {
                            tr.TotalAmount = tr.TotalAmount / 2;
                            tr.PayableAmount = tr.TotalAmount;
                        }
                        
                        //else if (ObjPromoSession.PromotionCode == "MCOTHERS" && strarr[11] != "")
                        //{
                        //    if (strarr[11] != "")
                        //    {
                        //        if (strarr[11] == "5597" || ObjPromoSession.PromotionCode == "MCOTHERS")
                        //        {
                        //            decimal amount = 5597 * (Convert.ToDecimal(Session["MCOTHERSNOOFPACKAGE"]));
                        //            decimal totalamount = 6996 * (Convert.ToDecimal(Session["MCOTHERSNOOFPACKAGE"]));
                        //            tr.TotalAmount = amount;
                        //            tr.PayableAmount = amount;
                        //            DateTime day1 = Convert.ToDateTime(tr.ShowDate.ToString());
                        //            GTICKBOL.McPromotionBooking_Details(Convert.ToInt16(tr.TotalSeats), Convert.ToInt16(int.Parse(Session["MCOTHERSNOOFPACKAGE"].ToString())), Convert.ToDecimal(totalamount.ToString()), Convert.ToDecimal(tr.TotalAmount.ToString()), Convert.ToDateTime(tr.DateOfBooking.ToString()), tr.BookingID.ToString(), Convert.ToDateTime(tr.ShowDate.ToString()), tr.Name.ToString(), tr.EmailID.ToString(), tr.MobileNo.ToString(), tr.PaymentGateway.ToString(), false, "", "PACKAGE", tr.PromotionCode.ToString(), Session["NWPMCBANKNAME"].ToString(), Session["NWPMCCARDNO"].ToString(), Session["MCPROMOCODE"].ToString());
                        //        }
                        //    }
                        //}
                        else
                        {
                            DataTable prices = GTICKBOL.Get_AllSeatPrice_SeatKeyNoWise(tr.BookingID);
                            if (ObjPromoSession.PromotionCode == "YATRA")
                            {
                                if (strarr[4].ToString() == "GL" || strarr[4].ToString() == "SL")
                                {
                                    ObjPromoSession.DiscountPercentage = 15;
                                }
                                if (strarr[4].ToString() == "DM" || strarr[4].ToString() == "PL")
                                {
                                    ObjPromoSession.DiscountPercentage = 20;
                                }
                                tr.DiscountedAmount = decimal.Truncate(tr.DiscountedAmount);
                                if (tr.DiscountedAmount == 1274)
                                    tr.DiscountedAmount = tr.DiscountedAmount + 1;
                                else if (tr.DiscountedAmount == 2124)
                                    tr.DiscountedAmount = tr.DiscountedAmount + 1;
                                else if (tr.DiscountedAmount == 2974)
                                    tr.DiscountedAmount = tr.DiscountedAmount + 1;
                                else if (tr.DiscountedAmount == 4249)
                                    tr.DiscountedAmount = tr.DiscountedAmount + 1;
                                tr.DiscountedAmount = tr.DiscountedAmount;
                                transid = GTICKBOL.YATRABooking_Details(Convert.ToInt16(Session["NoofTickets"]), Session["yatrapnr"].ToString(), Session["promocode"].ToString(), strarr[4].ToString(), tr.DiscountPercentage, tr.TotalAmount, tr.DiscountedAmount, Convert.ToDateTime(tr.DateOfBooking.ToString()), tr.BookingID.ToString(), Convert.ToDateTime(tr.ShowDate.ToString()), tr.Name.ToString(), tr.EmailID.ToString(), tr.MobileNo.ToString(), tr.PaymentGateway.ToString(), false, "", transid.ToString());
                            }
                            if (ObjPromoSession.PromotionCode == "MCWORLD")
                            {
                                if (strarr[4].ToString() == "PL")
                                {
                                    ObjPromoSession.DiscountPercentage = 15;
                                }
                                if (strarr[4].ToString() == "DM")
                                {
                                    ObjPromoSession.DiscountPercentage = 25;
                                }
                                tr.DiscountedAmount = decimal.Truncate(tr.DiscountedAmount);
                                if (tr.DiscountedAmount == 1274)
                                    tr.DiscountedAmount = tr.DiscountedAmount + 1;
                                else if (tr.DiscountedAmount == 2124)
                                    tr.DiscountedAmount = tr.DiscountedAmount + 1;
                                else if (tr.DiscountedAmount == 2974)
                                    tr.DiscountedAmount = tr.DiscountedAmount + 1;
                                else if (tr.DiscountedAmount == 4249)
                                    tr.DiscountedAmount = tr.DiscountedAmount + 1;
                                tr.DiscountedAmount = tr.DiscountedAmount;
                                GTICKBOL.McPromotionBooking_Details(Convert.ToInt16(tr.TotalSeats), 0, Convert.ToDecimal(tr.TotalAmount), Convert.ToDecimal(tr.DiscountedAmount), Convert.ToDateTime(tr.DateOfBooking.ToString()), tr.BookingID.ToString(), Convert.ToDateTime(tr.ShowDate.ToString()), tr.Name.ToString(), tr.EmailID.ToString(), tr.MobileNo.ToString(), tr.PaymentGateway.ToString(), false, "", "TICKET", tr.PromotionCode.ToString(), Session["MCBANKNAME"].ToString(), Session["MCCARDNO"].ToString(), Session["worldcardpromocode"].ToString());
                            }
                            if (ObjPromoSession.PromotionCode == "MCOTHERS")
                            {
                                if (strarr[4].ToString() == "GL")
                                {
                                    ObjPromoSession.DiscountPercentage = 20;
                                }
                                if (strarr[4].ToString() == "SL")
                                {
                                    ObjPromoSession.DiscountPercentage = 15;
                                }
                                tr.DiscountedAmount = decimal.Truncate(tr.DiscountedAmount);
                                if (tr.DiscountedAmount == 1274)
                                    tr.DiscountedAmount = tr.DiscountedAmount + 1;
                                else if (tr.DiscountedAmount == 2124)
                                    tr.DiscountedAmount = tr.DiscountedAmount + 1;
                                else if (tr.DiscountedAmount == 2974)
                                    tr.DiscountedAmount = tr.DiscountedAmount + 1;
                                else if (tr.DiscountedAmount == 4249)
                                    tr.DiscountedAmount = tr.DiscountedAmount + 1;
                                tr.DiscountedAmount = tr.DiscountedAmount;
                                GTICKBOL.McPromotionBooking_Details(Convert.ToInt16(tr.TotalSeats), 0, Convert.ToDecimal(tr.TotalAmount), Convert.ToDecimal(tr.DiscountedAmount), Convert.ToDateTime(tr.DateOfBooking.ToString()), tr.BookingID.ToString(), Convert.ToDateTime(tr.ShowDate.ToString()), tr.Name.ToString(), tr.EmailID.ToString(), tr.MobileNo.ToString(), tr.PaymentGateway.ToString(), false, "", "TICKET", tr.PromotionCode.ToString(), Session["NWTMCBANKNAME"].ToString(), Session["NWTMCCARDNO"].ToString(), Session["MCPROMOCODE"].ToString());
                            }
                            tr.TotalAmount = 0;
                            if (prices != null)
                            {
                                foreach (DataRow dr in prices.Rows)
                                {
                                    decimal SinglePrice = decimal.Parse(dr[0].ToString());
                                    decimal DiscountedPrice = SinglePrice - (SinglePrice * ObjPromoSession.DiscountPercentage / 100);
                                    DiscountedPrice = decimal.Truncate(DiscountedPrice);
                                    if (DiscountedPrice == 1274)
                                        DiscountedPrice = DiscountedPrice + 1;
                                    else if (DiscountedPrice == 2124)
                                        DiscountedPrice = DiscountedPrice + 1;
                                    else if (DiscountedPrice == 2974)
                                        DiscountedPrice = DiscountedPrice + 1;
                                    else if (DiscountedPrice == 4249)
                                        DiscountedPrice = DiscountedPrice + 1;
                                    tr.TotalAmount += DiscountedPrice;
                                    Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Discounted Price For a Ticket" + DiscountedPrice.ToString());
                                }
                            }
                        }
                        if (strarr[11] != "")
                        {
                            if (strarr[11] == "Rs.1275")
                            {
                                decimal amount = 500 * Convert.ToDecimal(tr.TotalSeats);
                                tr.TotalAmount = tr.TotalAmount + amount;
                                GTICKBOL.March_Promotion(tr.Category, tr.BookingID, tr.DateOfBooking, tr.TotalSeats, tr.TotalAmount, strarr[11], 0);
                            }
                            else if (strarr[11] == "Rs.4999")
                            {
                                decimal amount = 2501 * (Convert.ToDecimal(tr.TotalSeats) / 2);
                                tr.TotalAmount = tr.TotalAmount + amount;
                                GTICKBOL.March_Promotion(tr.Category, tr.BookingID, tr.DateOfBooking, tr.TotalSeats, tr.TotalAmount, strarr[11], 0);
                            }
                        }
                    }
                    //*******Promotion code send discounted AMOUNT to payment gateway changes END here **********
                    //*******Jhumroo Offer (1+1) code send discounted AMOUNT to payment gateway changes START here **********
                    //if (sessionvalue[1].ToString() == "JHUMROO" && (sessionvalue[4].ToString() == "GLY" || sessionvalue[4].ToString() == "SL" || sessionvalue[4].ToString() == "GL" || sessionvalue[4].ToString() == "PL" || sessionvalue[4].ToString() == "DM") && sessionvalue[10].ToString() == "" && Present <= End)
                    //{
                    //    decimal amount = Convert.ToDecimal(Session["PayableAmount"].ToString());
                    //    tr.TotalAmount = amount;
                    //    tr.PayableAmount = tr.TotalAmount;
                    //}
                    //*******Jhumroo Offer (1+1) code send discounted AMOUNT to payment gateway changes END here **********
                    if (transid > 0)
                    {
                        Session["AgentCode"] = null;
                        GTICKV.LogEntry(tr.BookingID.ToString(), "Data Successfully Written to Temp Transaction Table", "9", transid.ToString());

                        string URL = "";
                        //Pay Details , Sent To Loyalty Card Page --  CardType,TransID,Amt,ShowName
                        //Session["PayDetailsTemp"] = rblVoucher.SelectedValue + "|" + tr.BookingID.ToString() + "_" + transid + "~" + tr.AgentCode + "|" + tr.TotalAmount + "|" + tr.Play;
                        Session["PayDetailsTemp"] = tr.BookingID.ToString() + "_" + transid + "~" + tr.AgentCode + "|" + tr.TotalAmount + "|" + tr.Play;

                        if (ddlPaymentMode.SelectedValue == "CREDIT")
                        {
                            if (rbl_CardType.SelectedValue == "IDBI")
                            {
                                GTICKV.LogEntry(tr.BookingID.ToString(), "Sending to IDBI Payment Gateway", "12", transid.ToString());

                                /*******************Payement Gateway Error Value Code**********************/
                                #region PG_DB for Contact Details
                                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Contact Details Page PG_DB");
                                int i = GTICKBOL.Insert_Payment_DB("Sending to IDBI Payment Gateway and no logs trace after that", transid.ToString(), "IDBI");
                                #endregion PG_DB for Contact Details
                                /*********************End******************************/

                                URL = "Payment/Idbi/Default.aspx?type=idbi&transid=" + tr.BookingID.ToString() + "_" + transid + "~" + tr.AgentCode + "&amt=" + tr.TotalAmount
                                    + "&show=" + tr.Play;
                            }
                            else if (rbl_CardType.SelectedValue == "AMEX")
                            {
                                GTICKV.LogEntry(tr.BookingID.ToString(), "Sending to AMEX Payment Gateway", "12", transid.ToString());

                                /*******************Payement Gateway Error Value Code**********************/
                                #region PG_DB for Contact Details 
                                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Contact Details Page PG_DB");
                                int i = GTICKBOL.Insert_Payment_DB("Sending to AMEX Payment Gateway and no logs trace after that", transid.ToString(), "AMEX");
                                #endregion PG_DB for Contact Details
                                /*********************End******************************/

                                //tr.TotalAmount = 1;
                                if (ddl_country.SelectedValue == "india")
                                    URL = "Payment/Web/Default.aspx?type=amex&transid=" + tr.BookingID.ToString() + "_" + transid + "~" + tr.AgentCode + "&amt=" + tr.TotalAmount
                                        + "&show=" + tr.Play + "&title=" + "" + "&fname=" + "" + "&mname=" + "" + "&lname=" + "" + "&street=" + "NA" + "&city=" + "NA" + "&state=" + "NA" + "&pin=" + "NA" + "&country=" + "";
                                else
                                    URL = "Payment/Web/Default.aspx?type=amex&transid=" + tr.BookingID.ToString() + "_" + transid + "~" + tr.AgentCode + "&amt=" + tr.TotalAmount
                                            + "&show=" + tr.Play + "&title=" + tital + "&fname=" + fname + "&mname=" + mname + "&lname=" + lname + "&street=" + tr.Street + "&city=" + city + "&state=" + state + "&pin=" + tr.Pin + "&country=" + country;
                            }
                            //**************************************PAYTM GateWay*******************
                            else if (rbl_CardType.SelectedValue == "PAYTM")
                            {
                                //string payment_type_id = ddl_paymentmodetype.SelectedValue.ToString();
                                string payment_type_id;
                                string auth_mode;
                                if (Radio1.Checked == true || Radio2.Checked == true)
                                {
                                    auth_mode = "3D";
                                }
                                else
                                {
                                    auth_mode = "USRPWD";
                                }
                                if (Radio1.Checked)
                                {
                                    payment_type_id = "CC";
                                }
                                else if (Radio2.Checked)
                                {
                                    payment_type_id = "DC";
                                }
                                else if (Radio3.Checked)
                                {
                                    payment_type_id = "NB";
                                }
                                else
                                {
                                    payment_type_id = "PPI";
                                }
                                GTICKV.LogEntry(tr.BookingID.ToString(), "Sending to PAYTM Payment Gateway", "12", transid.ToString());

                                /*******************Payement Gateway Error Value Code**********************/
                                #region PG_DB for Contact Details
                                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Contact Details Page PG_DB");
                                int i = GTICKBOL.Insert_Payment_DB("Sending to PAYTM Payment Gateway and no logs trace after that", transid.ToString(), "PAYTM");
                                #endregion PG_DB for Contact Details
                                /*********************End******************************/
                                URL = "Payment/paytm/PostRequest.aspx?type=paytm&transid=" + tr.BookingID.ToString() + "Z" + transid + "Y" + tr.AgentCode + "&amt=" + tr.TotalAmount.ToString()
                                + "&payment_type_id=" + payment_type_id + "&auth_mode=" + auth_mode + "&name=" + tr.BookingID.ToString();

                            }
                            //**********************************************************************
                            else if (rbl_CardType.SelectedValue == "HDFC")
                            {
                                //string check = gb.HDFCLogCheck(transid.ToString()).Rows[0]["Amount"].ToString();
                                GTICKV.LogEntry(tr.BookingID.ToString(), "Sending to HDFC Payment Gateway", "12", transid.ToString());

                                /*******************Payement Gateway Error Value Code**********************/
                                #region PG_DB for Contact Details
                                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Contact Details Page PG_DB");
                                int i = GTICKBOL.Insert_Payment_DB("Sending to HDFC Payment Gateway and no logs trace after that", transid.ToString(), "HDFC");
                                #endregion PG_DB for Contact Details
                                /*********************End******************************/

                                string trackId, amount;
                                //Random Rnd = new Random();
                                //trackId = Rnd.Next().ToString();		//Merchant Track ID, this is as per merchant logic
                                trackId = tr.BookingID.ToString() + "_" + transid + "-" + tr.AgentCode;
                                Session["trackId"] = trackId;
                                amount = tr.TotalAmount.ToString();
                                Session["amount"] = amount;

                                String ErrorUrl = KoDTicketingIPAddress + "Payment/HDFC/Error.aspx";
                                String ResponseUrl = KoDTicketingIPAddress + "Payment/HDFC/ReturnReceipt.aspx";

                                //string qrystr = "id=" + HDFCTransPortalID + "&password=" + HDFCTranportalPwd + "&action=1&langid=USA&currencycode=356&amt=" + Server.UrlEncode(amount)
                                //    + "&responseURL=" + Server.UrlEncode(ResponseUrl) + "&errorURL=" + Server.UrlEncode(ErrorUrl)
                                //    + "&trackid=" + trackId
                                //    + "&udf1=TicketBooking&udf2=" + Server.UrlEncode(txtEmailAddress.Text.Trim())
                                //    + "&udf3=" + Server.UrlEncode(txtISDCode.Text + txtContactNo.Text) + "&udf4=" + Server.UrlEncode(txtAddress.Text.Trim()) + "&udf5=" + tr.BookingID;

                                string qrystr = "id=" + HDFCTransPortalID + "&password=" + HDFCTranportalPwd + "&action=1&langid=USA&currencycode=356&amt=" + amount
                                   + "&responseURL=" + Server.UrlEncode(ResponseUrl) + "&errorURL=" + Server.UrlEncode(ErrorUrl)
                                   + "&trackid=" + trackId
                                   + "&udf1=TicketBooking&udf2=" + txtEmailAddress.Text.Trim()
                                   + "&udf3=" + Server.UrlEncode(txtISDCode.Text.TrimStart('+') + txtContactNo.Text) + "&udf4=" + Server.UrlEncode(txtAddress.Text.Trim()) + "&udf5=" + tr.BookingID.ToString();

                                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Preparing for HDFC Payment..." + qrystr);

                                //Writefile_new("\n***************Initial Request********************", Server.MapPath("~"));
                                //Writefile_new("\n\nDateTime:" + DateTime.Now.ToString("dd/MM/yy HH:mm:ss") + " Reference No:" + trackId + "Request XML:" + qrystr, Server.MapPath("~"));

                                System.IO.StreamWriter requestWriter = null;
                                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Redirecting for HDFC Payment..." + HDFCTransUrl);
                                System.Net.HttpWebRequest objRequest = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(HDFCTransUrl);	//create a SSL connection object server-to-server
                                objRequest.Method = "POST";
                                objRequest.ContentLength = qrystr.Length;
                                objRequest.ContentType = "application/x-www-form-urlencoded";
                                objRequest.CookieContainer = new System.Net.CookieContainer();
                                try
                                {
                                    Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Processing request for HDFC Payment...");
                                    requestWriter = new System.IO.StreamWriter(objRequest.GetRequestStream());	// here the request is sent to payment gateway
                                    requestWriter.Write(qrystr);
                                }
                                catch (Exception ex)
                                {
                                    Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Excetion while processing HDFC payment: " + trackId + ex.Message);
                                    ClientScript.RegisterStartupScript(GetType(), "myscript", "<script>alert('Session Timeout. Please start the transaction again');window.location.href='Default.aspx';</script>");
                                }

                                if (requestWriter != null)
                                    requestWriter.Close();

                                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Review validation response from HDFC Payment Gateway...");
                                System.Net.HttpWebResponse objResponse = (System.Net.HttpWebResponse)objRequest.GetResponse();

                                //System.Net.CookieContainer responseCookiesContainer = new System.Net.CookieContainer();
                                //foreach (System.Net.Cookie cook in objResponse.Cookies)
                                //{
                                //    responseCookiesContainer.Add(cook);
                                //}

                                using (System.IO.StreamReader sr =
                                       new System.IO.StreamReader(objResponse.GetResponseStream()))
                                {
                                    String NSDLval = sr.ReadToEnd();
                                    Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Response: " + NSDLval);
                                    if (NSDLval.Contains("Invalid User Defined Field"))
                                    {
                                        GTICKV.LogEntry(tr.BookingID.ToString(), "Invalid cherecter is used by User.", "10", transid.ToString());
                                        //lblMess.Text = "The information submitted contains some invalid character, please avoid using [+,-,#] etc.";
                                        ClientScript.RegisterStartupScript(GetType(), "myscript", "<script>alert('The information submitted contains some invalid character, please avoid using [+,-,#] etc. Please start the transaction again');window.location.href='Default.aspx';</script>");
                                        return;
                                    }

                                    //Writefile_new("\n***************Initial Response********************", Server.MapPath("~"));
                                    //Writefile_new("\n\nDateTime:" + DateTime.Now.ToString("dd/MM/yy HH:mm:ss") + " Reference No:" + trackId + "Request XML:" + NSDLval, Server.MapPath("~"));
                                    if (NSDLval.IndexOf("http") == -1)
                                    {
                                        GTICKV.LogEntry(tr.BookingID.ToString(), "error in provided information", "11", transid.ToString());
                                        //lblMess.Text = "Payment cannot be processed with information provided.";
                                        ClientScript.RegisterStartupScript(GetType(), "myscript", "<script>alert('Payment cannot be processed with information provided. Please start the transaction again');window.location.href='Default.aspx';</script>");
                                        return;
                                    }

                                    // gb.HDFCLog(transid.ToString(), "", trackId, "***Initial Response*** : " + NSDLval);                                
                                    string strPmtId = NSDLval.Substring(0, NSDLval.IndexOf(":http"));	// Merchant MUST map (update) the Payment ID received with the merchant Track Id in his database at this place.
                                    string strPmtUrl = NSDLval.Substring(NSDLval.IndexOf("http"));
                                    if (strPmtId != String.Empty && strPmtUrl != String.Empty)
                                    {
                                        URL = strPmtUrl.ToString() + "?PaymentID=" + strPmtId;
                                    }
                                    else
                                    {
                                        //lblMess.Text = "Invalid Response!";
                                        ClientScript.RegisterStartupScript(GetType(), "myscript", "<script>alert('Invalid Response!. Please start the transaction again');window.location.href='Default.aspx';</script>");
                                    }
                                    sr.Close();
                                    TransactionBOL.Transaction_Temp_Insert_PaymentId(tr.BookingID, strPmtId);
                                }

                            }//HDFC
                        }
                        //else if (ddlPaymentMode.SelectedValue == "VOUCHER")
                        //{
                        //    Session["PayDetailsTemp"] = rblVoucher.SelectedValue + "|" + tr.BookingID.ToString() + "_" + transid + "~" + tr.AgentCode + "|" + tr.TotalSeats;
                        //    URL = "~/Payment/Voucher/Voucher.aspx";
                        //}
                        Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Payment Redirection: " + URL);
                        Response.Redirect(URL, false);
                        Session[Decrypt(Request.QueryString["SessionId"])] = "";
                        
                    }
                    else
                    {
                        lblMess.Text = "Session Timeout. Please start the transaction again by clicking \"Back\" button";
                        Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Session Timeout. Need to restart transaction");
                        ClientScript.RegisterStartupScript(GetType(), "myscript", "<script>alert('Session Timeout. Please start the transaction again');window.location.href='Default.aspx';</script>");
                    }
                }
                else //no Session[seat_val]
                {
                    ClientScript.RegisterStartupScript(GetType(), "myscript", "<script>alert('Session Timeout. Please start the transaction again');window.location.href='Default.aspx';</script>");
                }
                #endregion
            }
            catch (Exception ex)
            {
                GTICKV.LogEntry(tr.BookingID.ToString(), "Error Occurred - " + ex.Message.Replace("'", ""), "8", transid.ToString());
                ClientScript.RegisterStartupScript(GetType(), "myscript", "<script>alert('Session Timeout. Please start the transaction again');window.location.href='Default.aspx';</script>");
            }
        }
        else
        {
            Session.Clear();
            ClientScript.RegisterStartupScript(GetType(), "myscript", "<script>alert('Session Timeout. Please start the transaction again');window.location.href='Default.aspx';</script>");
        }
    }
    protected void btnBackHome_Click(object sender, EventArgs e)
    {
        GTICKV.LogEntry(sessionvalue[12].ToString(), "User Press Cancel Button On contact detail Page.", "6","");
        if (sessionvalue[12]!="")
        {
            String KeyNo = sessionvalue[12];
            GTICKBOL.ON_Session_out(KeyNo);
        }
        if (sessionvalue[10]== "")
        {
            if (Session["Router"].ToString() == "buzzintown")
            {
                Session.Clear();
                Response.Redirect("Default.aspx?Router=buzzintown", false);
            }
            else if (Session["Router"].ToString() == "airfaresau")
            {
                Session.Clear();
                Response.Redirect("default.aspx?Router=airfaresau", false);
            }
            else if (Request.QueryString["March"] != null)
            {
                Session.Clear();
                Response.Redirect("MarchPromotion.aspx", false);
            }
            else if(sessionvalue[1]=="NEW YEAR")
            {
                Session.Clear();
                Response.Redirect("NewYear.aspx", false);
            }
            else
            {
                Session.Clear();
                Response.Redirect("Default.aspx", false);
            }
        }
        else if (sessionvalue[10]!="")
        {
            KoDTicketingLibrary.DTO.Promotion PromoSession = (KoDTicketingLibrary.DTO.Promotion)Session[sessionvalue[10]];
            if (PromoSession.PromotionCode.ToString().ToUpper() == "MMT")
            {
                Session.Clear();
                Response.Redirect("MMTUSPromotion.aspx", false);
            }
            else if (PromoSession.PromotionCode.ToString().ToUpper() == "MANA")
            {
                Session.Clear();
                Response.Redirect("ManaPromotion.aspx", false);
            }
            else if (PromoSession.PromotionCode.ToString().ToUpper() == "FAMILYOFFER")
            {
                Session.Clear();
                Response.Redirect("FamilyOffer.aspx", false);
            }
            else if (PromoSession.PromotionCode.ToString().ToUpper() == "MMTDOMESTIC")
            {
                Session.Clear();
                Response.Redirect("MakeMyTripPromotion.aspx", false);
            }
            else if (PromoSession.PromotionCode.ToString().ToUpper() == "YATRA")
            {
                Session.Clear();
                Response.Redirect("YatraPromotions.aspx", false);
            }
            else if (PromoSession.PromotionCode.ToString().ToUpper() == "MCWORLD")
            {
                Session.Clear();
                Response.Redirect("MasterCard-WcPromotions.aspx", false);
            }
            else if (PromoSession.PromotionCode.ToString().ToUpper() == "MCOTHERS")
            {
                Session.Clear();
                Response.Redirect("MasterCard-NwcPromotions.aspx", false);
            }
            else if (PromoSession.PromotionCode.ToString().ToUpper() == "OCTOBERFEST")
            {
                if (Session["Router"].ToString() == "buzzintown")
                {
                    Session.Clear();
                    Response.Redirect("OctoberFest.aspx?Router=buzzintown", false);
                }
                else
                {
                    Session.Clear();
                    Response.Redirect("OctoberFest.aspx", false);
                }
            }
            else if (PromoSession.PromotionCode.ToString().ToUpper() == "JHUMROOOFFER")
            {
                Session.Clear();
                Response.Redirect("JhumrooOffer.aspx", false);
            }
            else
            {
                Session.Clear();
                Response.Redirect("HotelsPromotion.aspx", false);
            }
        }
        else if (Request.QueryString["MC"] != null)
        {
            if (Decrypt(Request.QueryString["MC"]) == "TICKET" || Decrypt(Request.QueryString["MC"]) == "PACKAGE")
            {
                Response.Redirect("MasterCard-NwcPromotions.aspx", false);
            }
            if (Decrypt(Request.QueryString["MC"]) == "WORLD")
            {
                Response.Redirect("MasterCard-WcPromotions.aspx", false);
            }

        }
        else
        {
            Session.Clear();
            Response.Redirect("Default.aspx", false);
        }
    }

    //Get IP Address
    protected string GetIP()
    {
        string ipaddress;
        ipaddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (ipaddress == "" || ipaddress == null)
            ipaddress = Request.ServerVariables["REMOTE_ADDR"];
        return ipaddress;
    }
    
}
