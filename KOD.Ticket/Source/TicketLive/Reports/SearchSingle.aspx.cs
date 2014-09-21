using System;
using System.Data;
using System.Web.UI.WebControls;
using KoDUtilities;
using KoDTicketing.BusinessLayer;

public partial class SearchSingle : System.Web.UI.Page
{

    protected void btnGo_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsPostBack)
            {
                lblMess.Text = "No Records Found!";
                gv_Failed.Dispose();
                gv_Report.Dispose();
                gv_Report.DataBind();
                gv_Failed.DataBind();
            }

            long BookingID = txt_BookingID.Text.Length > 0 ? long.Parse(txt_BookingID.Text) : 0;
            string ReceiptNo = txt_ReceiptNo.Text.Length > 0 ? txt_ReceiptNo.Text : "0";
            string Name = txt_Name.Text.Length > 0 ? txt_Name.Text : "0";
            string today = DateTime.Today.ToShortDateString();
            string daysfromtoday = DateTime.Today.Subtract(TimeSpan.FromDays(30)).ToShortDateString();
            DataSet ds = TransactionBOL.Select_Report_SearchFromTransactions_DS(BookingID, ReceiptNo, daysfromtoday, today, "0", "0", Name, "0");
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                gv_Report.DataSource = ds;
                lblMess.Text = "BOOKINGS IN LAST 30 DAYS";
                gv_Report.DataBind();
            }
            else
            {
                lblMess.Text = "NO BOOKINGS IN LAST 30 DAYS";
            }

            TransactionRecord tr = new TransactionRecord();
            tr.BookingID = txt_BookingID.Text.Length > 0 ? long.Parse(txt_BookingID.Text) : 0;
            tr.ReceiptNo = txt_ReceiptNo.Text.Length > 0 ? txt_ReceiptNo.Text : "0";
            tr.DateOfBooking = daysfromtoday;
            //location is bookingDate to
            tr.Location = today;
            tr.ShowDate = "0";  // ShowDateFrom
            tr.MobileNo = "0";  //used as ShowDateTo

            tr.Name = txt_Name.Text.Length > 0 ? txt_Name.Text : "0";
            ds = TransactionBOL.Select_Report_SearchFromTransactionsTemp_DS(tr);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                gv_Failed.DataSource = ds;
                lblMess2.Text = "FAILED TRANSACTIONS IN LAST 30 DAYS";
                gv_Failed.DataBind();
            }
            else
            {
                lblMess2.Text = "NO FAILED TRANSACTION IN LAST 30 DAYS";
            }

        }
        catch (Exception ex)
        {
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write(ex.Message);
        }
    }

}
