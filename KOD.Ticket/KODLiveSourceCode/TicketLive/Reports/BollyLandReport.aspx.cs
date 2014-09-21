using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using KoDTicketing.BusinessLayer;
using KoDUtilities;

public partial class Reports_BollyLandReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnGo_Click(object sender, EventArgs e)
    {
        //if (txt_BookingID.Text == "" && txt_BookingDate.Text == "" && txt_bookingDateTo.Text == "" && txt_Name.Text == "" )
        //{
        //    //lblValidation.Visible = true;
        //    //lblValidation.Text = "Please Enter Either The BookingID, Booking Dates or Name !!";
        //    //txt_Name.Focus();
        //}

        if (txt_BookingDate.Text != "" && txt_bookingDateTo.Text == "")
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

                int Silver = 0; decimal totamt = 0; int Glod = 0;
                string BookingID = txt_BookingID.Text.Length > 0 ? txt_BookingID.Text : "0";
                string pgReceipt = txt_ReceiptNo.Text.Length > 0 ? txt_ReceiptNo.Text : "0";
                string DateOfBookingFrom = txt_BookingDate.Text.Length > 0 ? txt_BookingDate.Text : "0";
                string DateOfBookingTo = txt_bookingDateTo.Text.Length > 0 ? txt_bookingDateTo.Text : "0";
                //string ShowDate = txt_ShowDate.Text.Length > 0 ? txt_ShowDate.Text : "0";
                //string MobileNo = txt_ShowDateTo.Text.Length > 0 ? txt_ShowDateTo.Text : "0";
                string Name = txt_Name.Text.Length > 0 ? txt_Name.Text : "0";
                string Package = ddlPackage.SelectedItem.Text;
                int paymentStatus = Convert.ToInt32(ddlPaymentStatus.SelectedValue);
                DataSet ds = TransactionBOL.Select_Report_tbl_BollyLand(BookingID, DateOfBookingFrom, DateOfBookingTo, Name, pgReceipt, Package, paymentStatus);
                string data = ds.Tables[0].Columns[9].ToString();

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    Btn_Excel.Enabled = true;
                    btnPrint.Disabled = false;
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
                        Silver = Silver + int.Parse(Gr.Cells[3].Text);
                        totamt = totamt + decimal.Parse(Gr.Cells[4].Text);
                        Glod = Glod + int.Parse(Gr.Cells[2].Text);
                    }
                    gv_Report.FooterRow.Cells[1].Text = "Total";
                    gv_Report.FooterRow.Cells[3].Text = Silver.ToString();
                    gv_Report.FooterRow.Cells[2].Text = Glod.ToString();
                    gv_Report.FooterRow.Cells[4].Text = totamt.ToString();
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