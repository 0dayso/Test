using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KoDUtilities;
using KoDTicketing.BusinessLayer;
using System.Data;

public partial class Reports_DetailedReport : System.Web.UI.Page
{

    protected void btnGo_Click(object sender, EventArgs e)
    {
        int Bronze = 0;
        int Copper = 0;
        int diamond = 0;
        int silver = 0;
        int gold = 0;
        int Platinum = 0;
        int webSeats = 0;
        decimal totalSeats = 0;
        decimal HDFC = 0;
        decimal IDBI = 0;
        decimal Amex = 0;
        decimal webAmount = 0;
        decimal TotalAmount = 0;
        string play = null;

        TransactionRecord tr = new TransactionRecord();
        tr.startDateDetailed = Convert.ToDateTime(txt_BookingDate.Text.Length > 0 ? txt_BookingDate.Text : Convert.ToString(DateTime.Now.Date));
        tr.EndDateDetailed = Convert.ToDateTime(txt_bookingDateTo.Text.Length > 0 ? txt_bookingDateTo.Text : Convert.ToString(DateTime.Now.Date));
        tr.AgentCode = ddlAgent.SelectedItem.Text;
        tr.Play = ddlPlay.SelectedItem.Text;

        DataSet ds = TransactionBOL.Detailed_Report(tr.startDateDetailed, tr.EndDateDetailed, tr.AgentCode, tr.Play);
        int i = 0;
        i = ds.Tables.Count - 1;
        if (ds.Tables[i].Rows.Count > 0)
        {
            Btn_Excel.Enabled = true;
            btnPrint.Disabled = false;
            gv_Report.DataSource = ds.Tables[i];
            lblMess.Text = "";
            gv_Report.DataBind();
            
            if (ds.Tables[i].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[i].Rows)
                {
                    Bronze = Bronze + int.Parse(dr[1].ToString());
                    Copper = Copper + int.Parse(dr[2].ToString());
                    diamond = diamond + int.Parse(dr[3].ToString());
                    gold = gold + int.Parse(dr[4].ToString());
                    Platinum = Platinum + int.Parse(dr[5].ToString());
                    silver = silver + int.Parse(dr[6].ToString());
                    webSeats = webSeats + int.Parse(dr[7].ToString());
                    totalSeats = totalSeats + decimal.Parse(dr[8].ToString());
                    HDFC = HDFC + decimal.Parse(dr[9].ToString());
                    IDBI = IDBI + decimal.Parse(dr[10].ToString());
                    Amex = Amex + decimal.Parse(dr[11].ToString());
                    webAmount = webAmount + decimal.Parse(dr[12].ToString());
                    TotalAmount = TotalAmount + decimal.Parse(dr[13].ToString());                    
                }
               
            }
            gv_Report.FooterRow.Cells[0].Text = "Total";
            gv_Report.FooterRow.Cells[1].Text = Bronze.ToString();
            gv_Report.FooterRow.Cells[2].Text = Copper.ToString();
            gv_Report.FooterRow.Cells[3].Text =diamond.ToString(); 
            gv_Report.FooterRow.Cells[4].Text = gold.ToString();
            gv_Report.FooterRow.Cells[5].Text = Platinum.ToString();
            gv_Report.FooterRow.Cells[6].Text = silver.ToString();
            gv_Report.FooterRow.Cells[7].Text = webSeats.ToString();
            gv_Report.FooterRow.Cells[8].Text = totalSeats.ToString();
            gv_Report.FooterRow.Cells[9].Text = HDFC.ToString();
            gv_Report.FooterRow.Cells[10].Text = IDBI.ToString();
            gv_Report.FooterRow.Cells[11].Text = Amex.ToString();
            gv_Report.FooterRow.Cells[12].Text = webAmount.ToString();
            gv_Report.FooterRow.Cells[13].Text = TotalAmount.ToString();
            gv_Report.FooterRow.Cells[14].Text = play;
        }
        else
        {
            btnPrint.Disabled = true;
            Btn_Excel.Enabled = false;
            gv_Report.DataBind();
            lblMess.Text = "No Records Found!";
        }

    }
    protected void Btn_Excel_Click(object sender, EventArgs e)
    {
        GridViewExportUtil.Export("Record.xls", gv_Report);
    }


    protected void Page_Load(object sender, EventArgs e)
    {
    }
}
