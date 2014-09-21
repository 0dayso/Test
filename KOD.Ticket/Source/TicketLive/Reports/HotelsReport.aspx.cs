using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KoDTicketing.BusinessLayer;
using System.Data;
using KoDUtilities;
using System.Globalization;

public partial class Reports_HotelsReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            List<KoDTicketingLibrary.DTO.Promotion> listPromo = VistaBOL.GetAllPromostionCode();
            int list = listPromo.Count;
            ddlAgent.Items.Add("SELECT");
            for (int i = 0; i < list; i++)
            {
                ddlAgent.Items.Add(listPromo[i].PromotionCode);

            }
            ddlAgent.DataBind();
        }
    }
    protected void btnGo_Click(object sender, EventArgs e)
    {
        int Seats = 0; decimal totamt = 0; decimal discountamt = 0;

        TransactionRecord tr = new TransactionRecord();
        tr.DateOfBooking = txt_BookingDate.Text.Length > 0 ? txt_BookingDate.Text : "0";
        tr.Location = txt_bookingDateTo.Text.Length > 0 ? txt_bookingDateTo.Text : "0";
        tr.AgentCode = ddlAgent.SelectedValue;

        DataSet ds = TransactionBOL.Hotel_Report(tr.DateOfBooking, tr.Location, tr.AgentCode);
        
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            if (tr.AgentCode.ToString() == "MMTDOMESTIC")
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    decimal price;
                    string dts = ds.Tables[0].Rows[i][5].ToString();
                    DateTimeFormatInfo dd = new DateTimeFormatInfo { ShortDatePattern = "dd/MM/yyyy HH:mm:ss", DateSeparator = "/" };
                    DateTime date = Convert.ToDateTime(dts, dd);
                    string dt = date.ToString();
                    DateTime d = Convert.ToDateTime(dt);
                    string day = d.DayOfWeek.ToString().ToUpper();
                    if (day == "SATURDAY" || day == "SUNDAY")
                    {
                        price = decimal.Parse(ds.Tables[0].Rows[i][10].ToString()) - (1000*(int.Parse(ds.Tables[0].Rows[i][7].ToString())));
                        ds.Tables[0].Rows[i][10] = price;
                    }
                    else
                    {
                        price = decimal.Parse(ds.Tables[0].Rows[i][10].ToString()) - (500*(int.Parse(ds.Tables[0].Rows[i][7].ToString())));
                        ds.Tables[0].Rows[i][10] = price;
                    }
                }
            }
            if (tr.AgentCode.ToString() == "SELECT")
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (ds.Tables[0].Rows[i][13].ToString() == "MMTDOMESTIC")
                    {
                        decimal price;
                        string dts = ds.Tables[0].Rows[i][5].ToString();
                        DateTimeFormatInfo dd = new DateTimeFormatInfo { ShortDatePattern = "dd/MM/yyyy HH:mm:ss", DateSeparator = "/" };
                        DateTime date = Convert.ToDateTime(dts, dd);
                        string dt = date.ToString();
                        DateTime d = Convert.ToDateTime(dt);
                        string day = d.DayOfWeek.ToString().ToUpper();
                        if (day == "SATURDAY" || day == "SUNDAY")
                        {
                            price = decimal.Parse(ds.Tables[0].Rows[i][10].ToString()) - (1000 * (int.Parse(ds.Tables[0].Rows[i][7].ToString())));
                            ds.Tables[0].Rows[i][10] = price;
                        }
                        else
                        {
                            price = decimal.Parse(ds.Tables[0].Rows[i][10].ToString()) - (500 * (int.Parse(ds.Tables[0].Rows[i][7].ToString())));
                            ds.Tables[0].Rows[i][10] = price;
                        }
                    }
                }
            }
            Btn_Excel.Enabled = true;
            btnPrint.Disabled = false;
            gv_Report.DataSource = ds.Tables[0];
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
    protected void Btn_Excel_Click(object sender, EventArgs e)
    {
        GridViewExportUtil.Export("Record.xls", gv_Report);
    }
}
