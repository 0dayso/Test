using System;
using System.Data;
using System.Web.UI.WebControls;
using KoDUtilities;
using KoDTicketing.BusinessLayer;

public partial class Search_UnSuccess : System.Web.UI.Page
{
    int Seats = 0; decimal totamt = 0;

    protected void btnGo_Click(object sender, EventArgs e)
    {
if (txt_Name.Text == "" && txt_BookingDate.Text == "" && txt_bookingDateTo.Text == "" && txt_ShowDate.Text == "" && txt_ShowDateTo.Text == "")
        {
            lblValidation.Visible = true;
            lblValidation.Text = "Please Enter Either The Name , Booking Dates or Show Dates !!";
            txt_Name.Focus();
        }
        else if (txt_BookingDate.Text != "" && txt_bookingDateTo.Text == "")
        {
            lblValidation.Visible = true;
            lblValidation.Text = "Please Select the Booking Date To !!";
            txt_bookingDateTo.Focus();
        }
        else if (txt_BookingDate.Text == "" && txt_bookingDateTo.Text != "")
        {
            lblValidation.Visible = true;
            lblValidation.Text = "Please Select the Booking Date From !!";
            txt_BookingDate.Focus();
        }
        else if (txt_ShowDate.Text == "" && txt_ShowDateTo.Text != "")
        {
            lblValidation.Visible = true;
            lblValidation.Text = "Please Select the Show Date From !!";
            txt_ShowDate.Focus();
        }
        else if (txt_ShowDate.Text != "" && txt_ShowDateTo.Text == "")
        {
            lblValidation.Visible = true;
            lblValidation.Text = "Please Select the Show Date To !!";
            txt_ShowDateTo.Focus();
        }
        else
        {
        //Defining the Values to the Object
        TransactionRecord tr = new TransactionRecord();
        tr.BookingID = txt_BookingID.Text.Length > 0 ? long.Parse(txt_BookingID.Text) : 0;
        tr.ReceiptNo = txt_ReceiptNo.Text.Length > 0 ? txt_ReceiptNo.Text : "0";
        tr.DateOfBooking = txt_BookingDate.Text.Length > 0 ? txt_BookingDate.Text : "0";
        //location == bookingDate to
        tr.Location = txt_bookingDateTo.Text.Length > 0 ? txt_bookingDateTo.Text : "0";
        tr.ShowDate = txt_ShowDate.Text.Length > 0 ? txt_ShowDate.Text : "0";
        tr.MobileNo = txt_ShowDateTo.Text.Length > 0 ? txt_ShowDateTo.Text : "0";
        tr.Name = txt_Name.Text.Length > 0 ? txt_Name.Text : "0";
        tr.AgentCode = ddlAgent.SelectedValue;
        if (tr.BookingID != null || tr.BookingID.ToString() != "")
        {
            DataTable chkID1 = TransactionBOL.Select_MarchPromotionTransactionCounter_CounterIDWise(Convert.ToInt64(tr.BookingID.ToString()));
            if (chkID1 != null && chkID1.Rows.Count > 0)
            {
                tr.BookingID = long.Parse(chkID1.Rows[0][1].ToString());
            }
        }
        DataSet ds = TransactionBOL.Select_Report_SearchFromTransactionsTemp_DS(tr);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            btnPrint.Disabled = false;
            Btn_Excel.Enabled = true;
for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                DataTable chkID = TransactionBOL.Select_MarchPromotionTransactionCounter_IDWise(Convert.ToInt64(ds.Tables[0].Rows[i][1]));
                if (chkID.Rows.Count > 0)
                {
                    if (chkID.Rows[0][0].ToString().Substring(0, 2) == "31")
                    {
                        ds.Tables[0].Rows[i][1] = long.Parse(chkID.Rows[0][0].ToString());
                    }

                }
}
            gv_Report.DataSource = ds;
            lblMess.Text = "";
        }
        else
        {
            btnPrint.Disabled = true;
            Btn_Excel.Enabled = false;
            lblMess.Text = "No records Found!";
        }
        gv_Report.DataBind();
}
    }

    protected void Btn_Excel_Click(object sender, EventArgs e)
    {
        GridViewExportUtil.Export("Record.xls", gv_Report);
    }

    protected void btnSettle_Click(object sender, CommandEventArgs e)
    {
        //Defining the Values to the Object
        TransactionRecord tr = new TransactionRecord();
        tr.BookingID = txt_BookingID.Text.Length > 0 ? long.Parse(txt_BookingID.Text) : 0;
        tr.ReceiptNo = txt_ReceiptNo.Text.Length > 0 ? txt_ReceiptNo.Text : "0";
        tr.DateOfBooking = txt_BookingDate.Text.Length > 0 ? txt_BookingDate.Text : "0";
        tr.Location = txt_bookingDateTo.Text.Length > 0 ? txt_bookingDateTo.Text : "0";
        tr.ShowDate = txt_ShowDate.Text.Length > 0 ? txt_ShowDate.Text : "0";
        tr.MobileNo = txt_ShowDateTo.Text.Length > 0 ? txt_ShowDateTo.Text : "0";
        tr.Name = txt_Name.Text.Length > 0 ? txt_Name.Text : "0";
        tr.AgentCode = ddlAgent.SelectedValue;
        tr.ID = int.Parse(lblBookingID.Text);
        tr.SeatInfo = lblCurrentseats.Text + " (" + textRemarks.Text + ")";
        DataSet ds = TransactionBOL.Settle_Transaction_Details(tr);
        gv_Report.DataSource = ds.Tables[0].DefaultView;
        gv_Report.DataBind();
    }

    protected void gv_Report_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        lblBookingID.Text = e.CommandArgument.ToString();
        lblCurrentseats.Text = e.CommandName.ToString(); 
        
        long BookingID = long.Parse(e.CommandArgument.ToString());
        DataTable dt = TransactionBOL.get_LogDetails_From_Booking_Status(BookingID);
        if (dt.Rows.Count > 0)
            rep_Details.DataSource = dt;
        else
            lblMess.Text = "No Records Found!";
        rep_Details.DataBind();

        ModalPopupDetails.Show();
    }

    protected void gv_Report_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lnbSettle = e.Row.FindControl("lnbSettle") as LinkButton;
            lnbSettle.Text = "Settle";
            lnbSettle.Enabled = !(((DataRowView)e.Row.DataItem).Row.ItemArray[13].ToString().ToLower() != "true");
            Seats = Seats + int.Parse(((DataRowView)e.Row.DataItem).Row.ItemArray[7].ToString());
            totamt = totamt + decimal.Parse(((DataRowView)e.Row.DataItem).Row.ItemArray[8].ToString());
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            (e.Row.FindControl("lblTotalSeats") as Label).Text = Seats.ToString();
            (e.Row.FindControl("lblTotAmt") as Label).Text = totamt.ToString();
        }
    }

}
