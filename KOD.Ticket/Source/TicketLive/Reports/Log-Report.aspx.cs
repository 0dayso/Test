using System;
using System.Security;
using System.Data;
using System.Web.UI.WebControls;
using KoDUtilities;
using KoDTicketing.BusinessLayer;

public partial class Reports_Log_Report : System.Web.UI.Page
{

    protected void btnGo_Click(object sender, EventArgs e)
    {

        long BookingID = txt_BookingID.Text.Length > 0 ? long.Parse(txt_BookingID.Text) : 0;
        string ReceiptNo = txt_ReceiptNo.Text.Length > 0 ? txt_ReceiptNo.Text : "0";
        string DateOfBooking = txt_BookingDate.Text.Length > 0 ? txt_BookingDate.Text : "0";
        string Location = txt_bookingDateTo.Text.Length > 0 ? txt_bookingDateTo.Text : "0";
        string ShowDate = txt_ShowDate.Text.Length > 0 ? txt_ShowDate.Text : "0";
        string MobileNo = txt_ShowDateTo.Text.Length > 0 ? txt_ShowDateTo.Text : "0";
        string Name = txt_Name.Text.Length > 0 ? txt_Name.Text : "0";
        string PaymentType = dlStatus.SelectedValue;
        string AgentCode = ddlAgent.SelectedValue;
        DataSet ds = TransactionBOL.get_ALL_LogDetails(BookingID, ReceiptNo, DateOfBooking, Location, ShowDate, MobileNo, Name, AgentCode, PaymentType);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            btnPrint.Disabled = false;
            Btn_Excel.Enabled = true;
            gv_Report.DataSource = ds;
            if (dlStatus.SelectedValue != "0")
                lblMess12.Text = ds.Tables[0].Rows.Count + " Records found for <u>" + dlStatus.SelectedItem.Text + "</u>";
        }
        else
        {
            btnPrint.Disabled = true;
            Btn_Excel.Enabled = false;
            lblMess12.Text = "No records Found!";
        }
        gv_Report.DataBind();
    }

    protected void gv_Report_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        long BookingID = long.Parse(e.CommandArgument.ToString());
        DataTable dt = TransactionBOL.get_LogDetails_From_Booking_Status(BookingID);
        if (dt.Rows.Count > 0)
            rep_Details.DataSource = dt;
        else
            lblMess.Text = "No Records Found!";
        rep_Details.DataBind();
        mo12.Show();
    }

    protected void Btn_Excel_Click(object sender, EventArgs e)
    {
        GridViewExportUtil.Export("Record.xls", gv_Report);
    }

    int Seats = 0; decimal totamt = 0;
    protected void gv_Report_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Seats = Seats + int.Parse(((DataRowView)e.Row.DataItem).Row.ItemArray[6].ToString());
            totamt = totamt + decimal.Parse(((DataRowView)e.Row.DataItem).Row.ItemArray[7].ToString());
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            (e.Row.FindControl("lblTotalSeats") as Label).Text = Seats.ToString();
            (e.Row.FindControl("lblTotAmt") as Label).Text = totamt.ToString();
        }
    }
}