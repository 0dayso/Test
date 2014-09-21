using System;
using System.Data;
using System.Web.UI.WebControls;
using KoDTicketing;
using KoDTicketing.BusinessLayer;

public partial class Seat_layout : System.Web.UI.Page
{
    protected string seatreq = "", cat = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            set_seatLayout();  
        }
    }
    void set_seatLayout()
    {
        Table objtable = new Table();
        objtable.CellPadding = 0;
        objtable.CellSpacing = 0;
        TableRow objtr = new TableRow();
        TableCell objtd = new TableCell();
        string[] seat_val = new string[8];

        if (Session["seat_Val"] != null)
        {
            seat_val = Session["seat_Val"].ToString().Split(',');
            if (seat_val.Length < 6)
            {
                throw new Exception("Seat Layout cannot be done as session value is no valid Session: " + Session["seat_Val"].ToString());
            }

            String filmCode = seat_val[0];
            String audiNo = seat_val[3];
            cat = seat_val[4];
            seatreq = seat_val[5];

            DataTable dtrows = GTICKV.SelectRow_AudiWise(filmCode);
            System.Diagnostics.Trace.Assert((dtrows == null), "Rows could not be fetched for selected Audi");           

            int maxCol = GTICKV.maxColumns(filmCode);
            int maxRows = GTICKV.maxRows(filmCode);

            DataTable dtseatlayout = GTICKV.AllSeats(audiNo);
            System.Diagnostics.Trace.Assert(dtseatlayout == null, "Rows could not be fetched for selected Audi");

            int temptablecellcount = 0, tablecell = 0;
            if (dtseatlayout.Rows.Count > 0 && dtrows.Rows.Count > 0)
            {
                //for each row up until maximum
                for (int tablerow = 0; tablerow < maxRows; tablerow++)
                {
                    DataRow drseats = dtrows.Rows[tablerow];
                    objtr = new TableRow();
                    objtable.Rows.Add(objtr);
                    objtd = new TableCell();
                    objtd.Text = drseats[5].ToString();
                    objtr.Cells.Add(objtd);
                    for (tablecell = 0 + temptablecellcount; tablecell < maxCol + temptablecellcount; tablecell++)
                    {
                        DataRow drrow = dtseatlayout.Rows[tablecell];
                        objtd = new TableCell();
                        objtd.Attributes.Add("class", "pad");
                        if (drrow[3].ToString() == "1")
                        {
                            if (drrow[9].ToString() == "1" || drrow[11].ToString() == "1" || drrow[12].ToString() == "1")
                                objtd.Text = "<img src='../../images/R_chair.gif'   />";
                            else if (drrow[10].ToString() == "1")
                                objtd.Text = "<img src='../../images/Gy_chair.gif'   />";
                            else
                            {
                                if (drrow[6].ToString().ToLower() == seat_val[4].ToLower())
                                {
                                    objtd.Text = "<img src='../../images/W_chair.gif' alt='" + drrow[7] + " - " + drrow[13] + "' title='" + drrow[7] + " - " + drrow[13] + ", Price : " + String.Format("{0:#.##}", decimal.Parse(drrow[8].ToString())) + " INR'  />";
                                    objtd.ID = "Seat_" + drrow[6] + "_" + drrow[2] + "_" + drrow[7].ToString() + "_" + drrow[0].ToString();
                                }
                                else
                                    objtd.Text = "<img src='../../images/Gy_chair.gif'    />";
                            }
                        }
                        objtr.Cells.Add(objtd);
                    }
                    objtd = new TableCell();
                    objtd.Text = drseats[5].ToString();
                    objtr.Cells.Add(objtd);
                    temptablecellcount = tablecell;
                }
            }
            myform.Controls.Add(objtable);
        }
        else
        {
            Session.Abandon();
            ClientScript.RegisterStartupScript(GetType(), "myscript", "<script>alert('Session Timeout. Please start" +
                " the transaction again');window.location.href='TicketBooking.aspx';</script>");
        }
    }
    protected void btnProceed_Click(object sender, EventArgs e)
    {
        if (Session["seat_Val"] != null)
        {
            try
            {

                string[] date = Session["seat_Val"].ToString().Split(',');
                if (date.Length < 4)
                {
                    String err = "Cannot render seat layour because seat selection in Session invalid or expired. Session: " + Session["seat_Val"].ToString();
                    Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write(err);
                    throw new Exception(err);
                }
                else
                {
                    string filmCode = date[3].ToString();

                    string[] confimseats = hidtempseats.Value.Split('|');
                    int totalSeats = int.Parse(confimseats[0]);

                    string strchktempseat = "", Seat_info = "";
                    for (int u = 0; u < totalSeats; u++)
                    {
                        strchktempseat += confimseats[2 + u].Split('_')[0] + ",";
                        Seat_info += confimseats[2 + u].Split('_')[1] + ",";
                    }
                    string SeatNo = strchktempseat.TrimEnd(',');

                    int status;
                    TransactionRecord _tr = new TransactionRecord();
                    //assign filmCode from Session
                    _tr.Play = filmCode;
                    _tr.TotalSeats = totalSeats;
                    //Generate Transaction ID
                    _tr.SeatInfo = SeatNo;
                    _tr.BookingID = GTICKBOL.TransactionCounter_Max();
                    status = GTICKBOL.Check_Seats_BeforeProceed(_tr);
                    if (status == 0)
                    {
                        KoDTicketing.GTICKV.LogEntry(_tr.BookingID.ToString(), "User Detail > " + Session["seat_Val"].ToString() + ", Browser Version : " + HiddenBrowser.Value, "1", "");
                        KoDTicketing.GTICKV.LogEntry(_tr.BookingID.ToString(), "Checking Seats Availability", "2", "");
                        ClientScript.RegisterStartupScript(GetType(), "myscript", "<script>alert('The seats you have selected are not available at this time, please select different seats');</script>");
                        Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Setting up the seat layout again as the selected seats are not available. Session : " + Session["seat_Val"].ToString());
                        set_seatLayout();
                        return;
                    }
                    else
                    {
                        KoDTicketing.GTICKV.LogEntry(_tr.BookingID.ToString(), "Seats are available.", "3", "");
                        Session["Seat_info"] = Seat_info.TrimEnd(',');
                        Session["Seat_TransactionID"] = _tr.BookingID;
                        Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Request Contact Details for Transaction... : " + Session["Seat_TransactionID"].ToString());
                        Response.Redirect("ContactDetails.aspx", false);
                    }
                }
            }
            catch (Exception ex)
            {
                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Seat Layout Page Error: " + ex.Message);
                Session.Abandon();
                ClientScript.RegisterStartupScript(GetType(), "myscript", "<script>alert('Session Timeout. Please start" +
                    " the transaction again');window.location.href='TicketBooking.aspx';</script>");
            }
        }


    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        string password = "A87C7B95932E9";
        String RoyalBal = Session["RBal"].ToString();
        String RoyalPoints = Session["RPoints"].ToString();
        String FN=Session["FirstName"].ToString();
        String LN = Session["LastName"].ToString();
        String Email = Session["EmailID"].ToString();
        String MobNo = Session["MobileNo"].ToString();
        String Address = Session["Address"].ToString();
        String RegID = Session["Regid"].ToString();
        Response.Redirect("TicketBooking.aspx?RemainingAmount=" + Server.UrlEncode(Common.Encrypt(RoyalBal, password)) + "&RemainingPoints=" + Server.UrlEncode(Common.Encrypt(RoyalPoints, password)) + "&FirstName=" + Server.UrlEncode(Common.Encrypt(FN, password)) + "&LastName=" + Server.UrlEncode(Common.Encrypt(LN, password)) + "&Email=" + Server.UrlEncode(Common.Encrypt(Email, password)) + "&Mobile=" + Server.UrlEncode(Common.Encrypt(MobNo, password)) + "&Address=" + Server.UrlEncode(Common.Encrypt(Address, password)) + "&MemberShipId=" + Server.UrlEncode(Common.Encrypt(RegID, password)), false);
        Session.Clear();

    }
    public string Encrypt(string val)
    {
        var bytes = System.Text.Encoding.UTF8.GetBytes(val);
        var encBytes = System.Security.Cryptography.ProtectedData.Protect(bytes, new byte[0], System.Security.Cryptography.DataProtectionScope.LocalMachine);
        return Convert.ToBase64String(encBytes);
    }
}