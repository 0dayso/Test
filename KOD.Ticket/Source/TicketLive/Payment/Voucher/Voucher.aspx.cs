using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Runtime.Remoting.Messaging;
using KoDTicketing;
using KoDTicketing.Utilities;
using KoDTicketing.BusinessLayer;
using KoDUtilities;

public partial class Voucher : System.Web.UI.Page
{
    string[] PayDetailsTemp = null;
    //string str = "CCD|1000055151_1100033492~WEB|2";
    //6500033492
    string MainBookingID = "", merchantReferenceNo = "", AgentCode = "", qstring = "", confirmationNumber = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["PayDetailsTemp"] != null)
        {
            if (!IsPostBack)
            {
                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Processing Voucher...");
                PayDetailsTemp = Session["PayDetailsTemp"].ToString().Split('|');
                DataTable dtVals = new DataTable();
                DataColumn dtcol = new DataColumn("id", typeof(int));
                dtVals.Columns.Add(dtcol);
                for (int u = 0; u < int.Parse(PayDetailsTemp[2]); u++)
                {
                    DataRow dr = dtVals.NewRow();
                    dr[0] = u;
                    dtVals.Rows.Add(dr);
                }
                rep_Vouchers.DataSource = dtVals;
                rep_Vouchers.DataBind();
            }
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "myscript", "<script>alert('Session Timeout. Please start the transaction again');window.location.href='../../Default.aspx';</script>");
        }
    }

    protected void btnVarify_Click(object sender, EventArgs e)
    {
        //varify the vaoucher Nos from databse and execute this routine
        if (Session["seat_Val"] != null)
        {
            if (Session["PayDetailsTemp"] != null)
            {
                TransactionRecord tr = new TransactionRecord();
                PayDetailsTemp = Session["PayDetailsTemp"].ToString().Split('|');
                string[] Straa = Session["seat_Val"].ToString().Split(',');
                string BookingID = PayDetailsTemp[1].Split('_')[1].Split('~')[0];
                tr.BookingID = long.Parse(BookingID);
                tr.TotalSeats = int.Parse(PayDetailsTemp[2]);
                tr.Category = Straa[4];
                string[] datarr = Straa[2].Split('/');
                tr.ShowDate = datarr[1] + "/" + datarr[0] + "/" + datarr[2];                
                tr.Day = Convert.ToDateTime(tr.ShowDate).DayOfWeek.ToString();
                switch (tr.Day.ToLower())
                {
                    case "monday":
                    case "tuesday":
                    case "wednesday":
                    case "thursday":
                    case "friday":
                        tr.Day = "1";
                        break;
                    case "saturday":
                    case "sunday":
                        tr.Day = "2";
                        break;
                }
                string VoucherNo = "";
                for (int U = 0; U < rep_Vouchers.Items.Count; U++)
                {
                    VoucherNo += ((TextBox)rep_Vouchers.Items[U].FindControl("txtSerials")).Text + "-" + ((TextBox)rep_Vouchers.Items[U].FindControl("txtvasls")).Text + "|";
                }
                tr.VoucherType = PayDetailsTemp[0];
                tr.VoucherNo = VoucherNo.TrimEnd('|');
                tr.VoucherBookingID = long.Parse("65" + BookingID.Remove(0, 2));
                if (TransactionBOL._Voucher_Varification_Update(tr) > 0)
                {
                    IAsyncResult ar = DoSomethingAsync("abc");
                    Session["result"] = ar;
                    Server.Transfer("~/Payment/Please-Wait.aspx", false);
                }
                else
                {
                    lblMess.Text = "Voucher No(s). Provided above are not valid!";
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "myscript", "<script>alert('Session Timeout. Please start the transaction again');window.location.href='../../Default.aspx';</script>");
            }
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "myscript", "<script>alert('Session Timeout. Please start the transaction again');window.location.href='../../Default.aspx';</script>");

        }
    }
    private IAsyncResult DoSomethingAsync(string someParameter)
    {
        DoSomethingDelegate doSomethingDelegate = new DoSomethingDelegate(DoSomething);
        IAsyncResult ar = doSomethingDelegate.BeginInvoke(someParameter, ref confirmationNumber, new AsyncCallback(MyCallback), null);
        return ar;
    }
    private delegate void DoSomethingDelegate(string someParameter, ref string confirmationNumber);
    private void MyCallback(IAsyncResult ar)
    {
        AsyncResult aResult = (AsyncResult)ar;
        DoSomethingDelegate doSomethingDelegate = (DoSomethingDelegate)aResult.AsyncDelegate;
        doSomethingDelegate.EndInvoke(ref confirmationNumber, ar);
    }
    private void DoSomething(string someParameter, ref string confirmationNumber)
    {
        UpdateResponseByTranId();
        confirmationNumber = "DONE!";
        Session["confirmationNumber"] = confirmationNumber;
    }

    protected string UpdateResponseByTranId()
    {
        try
        {
            TransactionRecord tr = new TransactionRecord();
            tr.BookingID = tr.VoucherBookingID;
            tr.ReceiptNo = "''";
            tr.ReferenceNo = long.Parse(PayDetailsTemp[1].Split('_')[0]);
            tr.AgentCode = PayDetailsTemp[1].Split('_')[1].Split('~')[1];
            //****** Promo code usecase start here ************
            KODHelper objKODHelper = new KODHelper();
            tr = objKODHelper.GetPromotionDetails(tr);
            //****** Promo code usecase END here ************
            DataTable dt = TransactionBOL.Get_Transaction_Detail(tr);
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                bool seatsBooked = (dt.Rows[0]["SeatBooked"].ToString() == "1");
                bool alreadyProcessed = (dt.Rows[0]["AlreadyProcessed"].ToString() == "1");
                if (seatsBooked)
                    Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Voucher Transaction : Seats Booked for " + tr.BookingID.ToString());
                if (!alreadyProcessed)
                {
                    ReceiptUtils.SuccessPaymentResponse(seatsBooked, dt.Rows[0], tr.ReferenceNo.ToString(), tr.BookingID.ToString(), tr.ReceiptNo,"");
                }
            }
         }
        catch (Exception ex)
        {
            if (qstring == "")
                qstring += "?err=seat";
            else
                qstring += "&err=seat";
            GTICKV.LogEntry(merchantReferenceNo, "Error Occured --" + ex.Message.Replace("'", ""), "19", MainBookingID);
        }
        Session["bookid"] = qstring;


        return qstring;
    }
    protected void btnBackHome_Click(object sender, EventArgs e)
    {
        GTICKBOL gb = new GTICKBOL();
        if (Session["Seat_TransactionID"] != null)
        {
            String KeyNo = Session["Seat_TransactionID"].ToString();
            GTICKBOL.ON_Session_out(KeyNo);
        }
        Server.Transfer("../../Default.aspx");
    }
}
