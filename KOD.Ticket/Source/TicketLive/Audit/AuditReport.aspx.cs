using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using KoDUtilities;
using KoDTicketing.BusinessLayer;
using System.Globalization;
using KoDTicketing;

public class List1
{
   public int totalnoofseat;
   public string category;
   public int totalnoofbookseat;
   public int tcktnotprinted;
}
public class List2
{
    public int auditno1;
    public int auditno2;
    public string category;
}
//public class GridList
//{
//    public int Seats;
//    public string Category;
//    public int AuditNo1;
//    public int AuditNo2;
//    public int BookedSeats;
//    public int TicketnotPrinted;//editable
//    public int ActualbookedSeats; //calculated
//    public float Occupancy;//Calculated
//    public int FoilsasperERP;//editable
//    public int Upgrade;
//    public int EffectiveOccupants;//calculated
//    public int FoilDiffAsERP;//calculated
//    public int preinterval;//calculated
//    public int postinterval;//calculated
//}

public partial class Audit_AuditReport : System.Web.UI.Page
{ 
   //static  List<GridList> CalculatedList = new List<GridList>(); 
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Load_Play();
        }
    }
    void Load_Play()
    {
        ddl_Play.Items.Clear();
        DataTable dtlocation = VistaBOL.Select_AuditPlay();
        ddl_Play.Items.Add(new ListItem("Select", "0"));
        if (dtlocation != null && dtlocation.Rows.Count > 0)
        {
            foreach (DataRow dr in dtlocation.Rows)
                ddl_Play.Items.Add(new ListItem(dr[0].ToString(), dr[0].ToString()));
        }
    }
    protected void ddl_Play_SelectedIndexChanged(object sender, EventArgs e)
    {
        String Play = ddl_Play.SelectedValue;
        dateofshow.Text = "Select";
        ddl_Location.Items.Clear();
        ddl_Location.Items.Add(new ListItem("Select", "0"));
        DataTable dtAudi = VistaBOL.Select_Audi(Play);
        foreach (DataRow dr in dtAudi.Rows)
            ddl_Location.Items.Add(new ListItem(dr[1].ToString(), dr[0].ToString()));
    }
    protected void ddl_Location_SelectedIndexChanged(object sender, EventArgs e)
    {
        String filmCode = ddl_Play.SelectedValue;
        String Location = ddl_Location.SelectedValue;
        dateofshow.Text = "Select";
        ddl_Date.Items.Clear();
        ddl_Date.Items.Add(new ListItem("Select", "0"));
        ddl_Date.Items[0].Selected = true;
        DataTable dtplaydate = VistaBOL.Select_AuditPlayDate(Location, filmCode);
        foreach (DataRow dr in dtplaydate.Rows)
            if (!(dr[0].ToString().Equals("2012.12.31")))
            {
                ddl_Date.Items.Add(new ListItem(Convert.ToDateTime(dr[0].ToString()).ToString("ddd, MMM dd,yyyy"),
               Convert.ToDateTime(dr[0].ToString()).ToString("dd/MM/yyyy")));
            }
    }
    protected void dateofshow_TextChanged(object sender, EventArgs e)
    {
        String PlayDate;
        if (dateofshow.Text.ToString() == "" || dateofshow.Text.ToString() == "Select")
        {
            PlayDate = "0";
            dateofshow.Text = "Select";
        }
        else
        {
            PlayDate = Convert.ToDateTime(dateofshow.Text.ToString()).ToString("dd/MM/yyyy");
        }
        String filmCode = ddl_Play.SelectedValue;
        String Location = ddl_Location.SelectedValue;
        ddl_ShowTimes.Items.Clear();
        ddl_ShowTimes.Items.Add(new ListItem("Select", "0"));
        foreach (DataRow dr in VistaBOL.Select_PlayTime_AuditReport(Location, filmCode, PlayDate).Rows)
        {
            ddl_ShowTimes.Items.Add(new ListItem(Convert.ToDateTime(dr[0].ToString()).ToShortTimeString(), dr[1].ToString().Trim()));
        }
    }

    protected void btnCreateAudit_Click(object sender, EventArgs e)
    {
        Btn_CreateAudit.Enabled = false;
        string ShowName = ddl_Play.SelectedValue;
        string ShowLocation = ddl_Location.SelectedValue;
        string ShowDate = dateofshow.Text.Length > 0 ? dateofshow.Text : "0";
        string ShowTime = ddl_ShowTimes.SelectedValue;
        string ShowTime1 = ddl_ShowTimes.SelectedItem.Text;
        string ShowDate1 = Convert.ToDateTime(dateofshow.Text.ToString()).ToString("dd/MM/yyyy");
        DataTable dtreport = GTICKV.AuditSeatsReport(ShowTime, ShowDate);
        DataTable dtauditnoreport = TransactionBOL.AuditNumberReport(ShowDate1, ShowName, ShowLocation, 1,ShowTime1);
        List<List1> listofgridview = new List<List1>();
        foreach (DataRow row in dtreport.Rows)
        {
            List1 gd = new List1();
            gd.totalnoofseat = Convert.ToInt32(row[0]);
            gd.category = Convert.ToString(row[1]);
            if (row[2].ToString()=="")
            {
                gd.totalnoofbookseat = 0;
            }
            else
            {
                gd.totalnoofbookseat = Convert.ToInt32(row[2]);
            }
            if (row[3].ToString() == "")
            {
                gd.tcktnotprinted = 0;
            }
            else
            {
                gd.tcktnotprinted = Convert.ToInt32(row[3]);
            }
            listofgridview.Add(gd);
        }
        List<List2> listofgridview1 = new List<List2>();
            foreach (DataRow row in dtauditnoreport.Rows)
            {
                List2 gd = new List2();
                gd.auditno1 = Convert.ToInt32(row[0]);
                gd.auditno2 = Convert.ToInt32(row[1]);
                gd.category = Convert.ToString(row[2]);
                listofgridview1.Add(gd);
            }
        var query = from u in listofgridview
                    join k in listofgridview1 on u.category equals k.category into p
                    from h in p.DefaultIfEmpty()
                    select new { Seats = u.totalnoofseat, Category = u.category, AuditNo1 = (h == null ? 0 : h.auditno1), AuditNo2 = (h == null ? 0 : h.auditno2), BookedSeats = u.totalnoofbookseat,TicketnotPrinted=u.tcktnotprinted};
        
        gv_Report.DataSource = query;
        gv_Report.DataBind();
        btnGridCalculation.Visible = true;
        
        if (dateofshow.Text == "" || dateofshow.Text == "Select" || dateofshow.Text == null)
        {
            Label1.Visible = true;
            Label1.Text = "Please Enter Either The Show Dates !!";
            dateofshow.Focus();
        }
        else
        {
            Label1.Visible = false;
            GridView_ShowDetail.DataSource = FlipDataSet(c());
            GridView_ShowDetail.DataBind();
        }

    }
    protected void gv_Report_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
    
    protected void gv_Report_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        
    }
    public DataSet c()
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable("Company");
        DataRow dr;
        string s = dateofshow.Text;
        DateTimeFormatInfo dd = new DateTimeFormatInfo { ShortDatePattern = "dd/MM/yyyy HH:mm:ss", DateSeparator = "/" };
        DateTime date = Convert.ToDateTime(s, dd);
        string dt1 = date.ToString();
        DateTime d = Convert.ToDateTime(dt1);
        string day = d.DayOfWeek.ToString();
        dt.Columns.Add(new DataColumn("Day", typeof(string)));
        dt.Columns.Add(new DataColumn("Show Date", typeof(string)));
        dt.Columns.Add(new DataColumn("Show Name", typeof(string)));
        dt.Columns.Add(new DataColumn("Show Time", typeof(string)));
        dr = dt.NewRow();
        dr[0] = day;
        dr[1] = dateofshow.Text;
        dr[2] = ddl_Play.SelectedItem.ToString();
        dr[3] = ddl_ShowTimes.SelectedItem.Text;
        dt.Rows.Add(dr);
        ds.Tables.Add(dt);
        return ds;
    }
    public DataSet FlipDataSet(DataSet my_DataSet)
    {
        DataSet ds = new DataSet();
        foreach (DataTable dt in my_DataSet.Tables)
        {
            DataTable table = new DataTable();
            for (int i = 0; i <= dt.Rows.Count; i++)
            {
                table.Columns.Add(Convert.ToString(i));
            }
            DataRow r = null;
            for (int k = 0; k < dt.Columns.Count; k++)
            {
                r = table.NewRow();
                r[0] = dt.Columns[k].ToString();
                for (int j = 1; j <= dt.Rows.Count; j++)
                    r[j] = dt.Rows[j - 1][k];
                table.Rows.Add(r);
            }
            ds.Tables.Add(table);
        }
        return ds;
    }
    protected void gv_Report_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    
    protected void btnGridCalculation_Click(object sender, EventArgs e)
    {
        gv_Report.Visible = false;
        btnPrint.Disabled = false;
        Btn_Excel.Enabled = true;
        btnGridCalculation.Visible = false;
        
        
        
        int seats = 0; int audit1 = 0; int audit2 = 0; int booked = 0; int tckt = 0; int actualbooked = 0; decimal occupancy = 0; int foilsaspererp = 0;
        int upgrade = 0; int effectiveoccupants = 0; int foilsasdifferp = 0; int preinterval = 0; int postinterval = 0;

        DataTable dt = new DataTable();
        dt.Columns.Add("Seats",typeof(int));
        dt.Columns.Add("Category", typeof(string));
        dt.Columns.Add("AuditNo1", typeof(int));
        dt.Columns.Add("AuditNo2", typeof(int));
        dt.Columns.Add("BookedSeats", typeof(int));
        dt.Columns.Add("TicketnotPrinted", typeof(int));
        dt.Columns.Add("Actual Sold as per ERP", typeof(int));
        dt.Columns.Add("Occupanccy", typeof(decimal));
        dt.Columns.Add("Foils as per Gate",typeof(int));
        dt.Columns.Add("Up-grade",typeof(int));
        dt.Columns.Add("Effective Occupants",typeof(int));
        dt.Columns.Add("Foil Diff As ERP",typeof(int));
        dt.Columns.Add("Pre-Interval",typeof(int));
        dt.Columns.Add("Post-Interval", typeof(int));

        foreach (GridViewRow row in gv_Report.Rows)
        {
            int Seats=Convert.ToInt32(row.Cells[0].Text);
            string Category=Convert.ToString(row.Cells[1].Text);
            int CountPreInterval=Convert.ToInt32(row.Cells[2].Text);
            int CountPostInterval=Convert.ToInt32(row.Cells[3].Text);
            int SoldasperERP=Convert.ToInt32(row.Cells[4].Text);
            int TicketnotPrinted = Convert.ToInt32(row.Cells[5].Text); 
            int FoilsasperGate; int Upgrade;
            //if ((row.Cells[5].FindControl("TextBox1") as TextBox).Text == "")
            //{
            //    TicketnotPrinted = 0;
            //}
            //else
            //{
            //    TicketnotPrinted = Convert.ToInt32((row.Cells[5].FindControl("TextBox1") as TextBox).Text);
            //}
            int ActualSoldasperERP = (Convert.ToInt32(row.Cells[4].Text) - Convert.ToInt32(row.Cells[5].Text));
            decimal Occupanccy=Math.Round((Convert.ToDecimal(ActualSoldasperERP / Convert.ToDecimal(row.Cells[0].Text)) * 100),2);
            if ((row.Cells[8].FindControl("TextBox2") as TextBox).Text == "")
            {
                FoilsasperGate = 0;
            }
            else 
            {
                FoilsasperGate = Convert.ToInt32((row.Cells[8].FindControl("TextBox2") as TextBox).Text);
            }
            if ((row.Cells[9].FindControl("TextBox3") as TextBox).Text == "")
            {
                Upgrade = 0;
            }
            else
            {
                Upgrade = Convert.ToInt32((row.Cells[9].FindControl("TextBox3") as TextBox).Text);
            }
            int EffectiveOccupants=FoilsasperGate + Upgrade;
            int FoilDiffAsERP = FoilsasperGate - ActualSoldasperERP;
            int PreInterval = (Convert.ToInt32(row.Cells[2].Text) - EffectiveOccupants);
            int PostInterval = (Convert.ToInt32(row.Cells[3].Text) - EffectiveOccupants);
            dt.Rows.Add(Seats, Category, CountPreInterval, CountPostInterval, SoldasperERP, TicketnotPrinted, ActualSoldasperERP, Occupanccy, FoilsasperGate, Upgrade, EffectiveOccupants, FoilDiffAsERP, PreInterval, PostInterval);
        }
        DataSet ds = new DataSet();
        ds.Tables.Add(dt);
        if (ds.Tables[0].Rows.Count > 0)
        {
            GridView_FinalDetail.DataSource = ds.Tables[0];
            GridView_FinalDetail.DataBind();
        }
        if (ds.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                seats = seats + int.Parse(dr[0].ToString());
                audit1 = audit1 + int.Parse(dr[2].ToString());
                audit2 = audit2 + int.Parse(dr[3].ToString());
                booked = booked + int.Parse(dr[4].ToString());
                tckt = tckt + int.Parse(dr[5].ToString());
                actualbooked = actualbooked + int.Parse(dr[6].ToString());
                occupancy = occupancy + decimal.Parse(dr[7].ToString());
                foilsaspererp = foilsaspererp + int.Parse(dr[8].ToString());
                upgrade = upgrade + int.Parse(dr[9].ToString());
                effectiveoccupants = effectiveoccupants + int.Parse(dr[10].ToString());
                foilsasdifferp = foilsasdifferp + int.Parse(dr[11].ToString());
                preinterval = preinterval + int.Parse(dr[12].ToString());
                postinterval = postinterval + int.Parse(dr[13].ToString());
            }
            GridView_FinalDetail.FooterRow.Cells[0].Text = seats.ToString();
            GridView_FinalDetail.FooterRow.Cells[2].Text = audit1.ToString();
            GridView_FinalDetail.FooterRow.Cells[3].Text = audit2.ToString();
            GridView_FinalDetail.FooterRow.Cells[4].Text = booked.ToString();
            GridView_FinalDetail.FooterRow.Cells[5].Text = tckt.ToString();
            GridView_FinalDetail.FooterRow.Cells[6].Text = actualbooked.ToString();
            GridView_FinalDetail.FooterRow.Cells[7].Text = (Math.Round((Convert.ToDecimal(actualbooked) / Convert.ToDecimal(seats) * 100), 2)).ToString();
            GridView_FinalDetail.FooterRow.Cells[8].Text = foilsaspererp.ToString();
            GridView_FinalDetail.FooterRow.Cells[9].Text = upgrade.ToString();
            GridView_FinalDetail.FooterRow.Cells[10].Text = effectiveoccupants.ToString();
            GridView_FinalDetail.FooterRow.Cells[11].Text = foilsasdifferp.ToString();
            GridView_FinalDetail.FooterRow.Cells[12].Text = preinterval.ToString();
            GridView_FinalDetail.FooterRow.Cells[13].Text = postinterval.ToString();
        }
        
    }
    protected void Btn_Excel_Click(object sender, EventArgs e)
    {
        GridViewExportUtil.Export("Record.xls", GridView_FinalDetail);
    }
}

