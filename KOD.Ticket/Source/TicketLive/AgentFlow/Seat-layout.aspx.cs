using System;
using System.Data;
using System.Web.UI.WebControls;
using KoDTicketing;
using KoDTicketing.BusinessLayer;

public partial class AgentFlow_Seat_layout : System.Web.UI.Page
{
    string agent = "";
    protected string seatreq = "", cat = "";
    String mc = "";
    String trnsectioncounter = "";
    protected String Session_value;
    protected string[] sessionvalue;
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
        agent = Session["Agent"].ToString();
        if (Session[Decrypt(Request.QueryString["SessionId"])].ToString() != "")
        {
            if (Request.QueryString["SessionId"] != null)
            {
                DataTable dt = TransactionBOL.Select_ShowDetails(Convert.ToInt64(Decrypt(Request.QueryString["SessionId"].ToString())));
                Session_value = dt.Rows[0]["Seat_Val"].ToString();
                sessionvalue = Session_value.Split(',');
                trnsectioncounter = sessionvalue[12];
            }
        }
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
        bool isfilled = false;
        if (Session_value != null)
        {
            if (Session_value != "")
            {
                seat_val = Session_value.Split(',');
                isfilled = false;
            }
            else
            {
                Session.Abandon();
                ClientScript.RegisterStartupScript(GetType(), "myscript", "<script>alert('Session Timeout. Please start" +
                            " the transaction again');window.location.href='Default.aspx';</script>");
            }

            if (seat_val.Length < 6)
            {
                throw new Exception("Seat Layout cannot be done as session value is no valid Session: " + (isfilled ? Session_value : Session_value));
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
                                objtd.Text = "<img src='../Images/R_chair.gif'   />";
                            else if (drrow[10].ToString() == "1")
                                objtd.Text = "<img src='../Images/Gy_chair.gif'   />";
                            else
                            {
                                if (drrow[6].ToString().ToLower() == seat_val[4].ToLower())
                                {
                                    objtd.Text = "<img src='../Images/W_Chair.gif' alt='" + drrow[7] + " - " + drrow[13] + "' title='" + drrow[7] + " - " + drrow[13] + ", Price : " + String.Format("{0:#.##}", decimal.Parse(drrow[8].ToString())) + " INR'  />";
                                    objtd.ID = "Seat_" + drrow[6] + "_" + drrow[2] + "_" + drrow[7].ToString() + "_" + drrow[0].ToString();
                                }
                                else
                                    objtd.Text = "<img src='../Images/Gy_Chair.gif'    />";
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
                " the transaction again');window.location.href='Default.aspx';</script>");
        }
    }
    protected void btnProceed_Click(object sender, EventArgs e)
    {
        if (Session_value != null)
        {
            try
            {
                string[] date = null;
                if (Session_value != "")
                {
                    date = Session_value.Split(',');
                }
                else
                {
                    Response.Redirect("Default.aspx", false);
                }

                if (date.Length < 4)
                {
                    String err = "Cannot render seat layour because seat selection in Session invalid or expired. Session: " + Session_value;
                    Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write(err);
                    throw new Exception(err);
                }
                else
                {
                    string filmCode = date[3].ToString();

                    string[] confimseats = hidtempseats.Value.Split('|');
                    int totalSeats = int.Parse(confimseats[0]);
                    //    if (totalSeats!=Convert.ToInt32(date[5].ToString()))
                    //    {
                    //        Session.Abandon();
                    //        ClientScript.RegisterStartupScript(GetType(), "myscript", "<script>alert('Session Timeout. Please start" +
                    //" the transaction again');window.location.href='Default.aspx';</script>");
                    //    }
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
                    // _tr.BookingID = GTICKBOL.TransactionCounter_Max();
                    _tr.BookingID = Convert.ToInt64(trnsectioncounter);
                    status = GTICKBOL.Check_Seats_BeforeProceed(_tr);
                    if (status == 0)
                    {
                        KoDTicketing.GTICKV.LogEntry(_tr.BookingID.ToString(), "User Detail > " + Session_value + ", Browser Version : " + HiddenBrowser.Value, "2", "");
                        KoDTicketing.GTICKV.LogEntry(_tr.BookingID.ToString(), "Checking Seats Availability", "4", "");
                        ClientScript.RegisterStartupScript(GetType(), "myscript", "<script>alert('The seats you have selected are not available at this time, please select different seats');</script>");
                        Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Setting up the seat layout again as the selected seats are not available. Session : " + Session_value);
                        set_seatLayout();
                        return;
                    }
                    else
                    {
                        KoDTicketing.GTICKV.LogEntry(_tr.BookingID.ToString(), "Seats are available.", "5", "");
                        GTICKBOL.Insert_SeatInfo(Seat_info.TrimEnd(','), _tr.BookingID);
                        Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Request Contact Details for Transaction... : " + Decrypt(Request.QueryString["SessionId"]));
                        Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write(trnsectioncounter);
                        Response.Redirect("ContactDetails.aspx?SessionId=" + Encrypt(trnsectioncounter), false);
                    }
                }
            }
            catch (Exception ex)
            {
                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Seat Layout Page Error: " + ex.Message);
                Session.Abandon();
                ClientScript.RegisterStartupScript(GetType(), "myscript", "<script>alert('Session Timeout. Please start" +
                    " the transaction again');window.location.href='Default.aspx';</script>");
            }
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "myscript", "<script>alert('Session Timeout. Please start" +
                    " the transaction again');window.location.href='Default.aspx';</script>");
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        KoDTicketing.GTICKV.LogEntry(trnsectioncounter.ToString(), "User Press Cancel Button On seat Layout Page.", "3", "");
        Response.Redirect("default.aspx", false);
    }
}