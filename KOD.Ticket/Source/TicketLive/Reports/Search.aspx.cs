using System;
using System.Data;
using System.Web.UI.WebControls;
using KoDUtilities;
using KoDTicketing.BusinessLayer;

public partial class Search : System.Web.UI.Page
{
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
            lblValidation.Visible = false;
            try
            {
                if (Page.IsPostBack)
                {
                    lblMess.Text = "No Records Found!";
                    gv_Report.Dispose();
                    gv_Report.DataBind();
                }

                int Seats = 0; decimal totamt = 0; decimal discountamt = 0; decimal dis = 0;
                long BookingID = txt_BookingID.Text.Length > 0 ? long.Parse(txt_BookingID.Text) : 0;
                string ReceiptNo = txt_ReceiptNo.Text.Length > 0 ? txt_ReceiptNo.Text : "0";
                string DateOfBooking = txt_BookingDate.Text.Length > 0 ? txt_BookingDate.Text : "0";
                string Location = txt_bookingDateTo.Text.Length > 0 ? txt_bookingDateTo.Text : "0";
                string ShowDate = txt_ShowDate.Text.Length > 0 ? txt_ShowDate.Text : "0";
                string MobileNo = txt_ShowDateTo.Text.Length > 0 ? txt_ShowDateTo.Text : "0";
                string Name = txt_Name.Text.Length > 0 ? txt_Name.Text : "0";
                string AgentCode = ddlAgent.SelectedValue;
                DataSet ds = TransactionBOL.Select_Report_SearchFromTransactions_DS(BookingID, ReceiptNo, DateOfBooking, Location, ShowDate, MobileNo, Name, AgentCode);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    Btn_Excel.Enabled = true;
                    btnPrint.Disabled = false;
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        if (ds.Tables[0].Rows[i][10].ToString() == "")
                        {
                            dis = Decimal.Parse(ds.Tables[0].Rows[i][8].ToString());
                            ds.Tables[0].Rows[i][10] = dis;
                            ds.Tables[0].Rows[i][9] = "0.00";
                        }
                    }
                    gv_Report.DataSource = ds;
                    lblMess.Text = "";
                }
                else
                {
                    btnPrint.Disabled = true;
                    Btn_Excel.Enabled = false;
                    lblMess.Text = "No Records Found!";
                }

                gv_Report.DataBind();
                
                if (gv_Report.Rows.Count > 0)
                {
                    foreach (GridViewRow Gr in gv_Report.Rows)
                    {
                        Seats = Seats + int.Parse(Gr.Cells[7].Text);
                        totamt = totamt + decimal.Parse(Gr.Cells[8].Text);
                        discountamt = discountamt + decimal.Parse(Gr.Cells[10].Text);
                    }
                    gv_Report.FooterRow.Cells[6].Text = "Total";
                    gv_Report.FooterRow.Cells[7].Text = Seats.ToString();
                    gv_Report.FooterRow.Cells[8].Text = totamt.ToString();
                    gv_Report.FooterRow.Cells[10].Text = discountamt.ToString();
                }
            }
            catch (Exception ex)
            {
                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write(ex.Message);
            }
        }
    }
    protected void Btn_Excel_Click(object sender, EventArgs e)
    {
        GridViewExportUtil.Export("Record.xls", gv_Report);
    }
}
