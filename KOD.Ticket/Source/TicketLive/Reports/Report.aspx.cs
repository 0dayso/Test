using System;
using System.Data;
using System.Web.UI.WebControls;
using KoDUtilities;
using KoDTicketing.BusinessLayer;

public partial class Report : System.Web.UI.Page
{

    protected void btnGo_Click(object sender, EventArgs e)
    {
        decimal discountamt = 0;
        TransactionRecord tr = new TransactionRecord();
        tr.BookingID = txt_BookingID.Text.Length > 0 ? long.Parse(txt_BookingID.Text) : 0;
        tr.ReceiptNo = txt_ReceiptNo.Text.Length > 0 ? txt_ReceiptNo.Text : "0";
        tr.DateOfBooking = txt_BookingDate.Text.Length > 0 ? txt_BookingDate.Text : "0";
        tr.Location = txt_bookingDateTo.Text.Length > 0 ? txt_bookingDateTo.Text : "0";
        tr.ShowDate = txt_ShowDate.Text.Length > 0 ? txt_ShowDate.Text : "0";
        tr.MobileNo = txt_ShowDateTo.Text.Length > 0 ? txt_ShowDateTo.Text : "0";
        tr.Name = txt_Name.Text.Length > 0 ? txt_Name.Text : "0";
        tr.AgentCode = ddlAgent.SelectedValue;
        DataSet ds = TransactionBOL.Select_Report_FromTransactionTable(tr);
        if (ds.Tables[0].Rows.Count > 0)
        {
            Btn_Excel.Enabled = true;
            btnPrint.Disabled = false;
            //for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            //{
            //    if (ds.Tables[0].Rows[i][10].ToString() == "")
            //    {
            //        discountamt = discountamt + decimal.Parse(dr[2].ToString());
            //    }
            //}
            gv_Report.DataSource = ds.Tables[0];
            lblMess.Text = "";


            gv_Report.DataBind();
            if (ds.Tables[1].Rows.Count > 0)
            {
                string card = "";
                foreach (DataRow dr in ds.Tables[1].Rows)
                {
                    Seats = Seats + int.Parse(dr[0].ToString());
                    totamt = totamt + decimal.Parse(dr[1].ToString());
                    if (dr[2].ToString() !="")
                    {
                        discountamt = discountamt + decimal.Parse(dr[2].ToString());
                        card += "<br/>Amount Received Using " + dr[3] + " : " + dr[2];
                    }
                    //card += "<br/>Amount Received Using " + dr[3] + " : " + dr[2];
                }
                lblTotSeats.Text = card;
            }
            gv_Report.FooterRow.Cells[0].Text = "Total";
            gv_Report.FooterRow.Cells[1].Text = Seats.ToString();
            gv_Report.FooterRow.Cells[2].Text = totamt.ToString();
            gv_Report.FooterRow.Cells[3].Text = discountamt.ToString();
        }
        else
        {
            btnPrint.Disabled = true;
            Btn_Excel.Enabled = false;
            lblMess.Text = "No Records Found!";
        }

    }
    int Seats = 0; decimal totamt = 0;    
    protected void Btn_Excel_Click(object sender, EventArgs e)
    {
        GridViewExportUtil.Export("Record.xls", gv_Report);
    }
}
