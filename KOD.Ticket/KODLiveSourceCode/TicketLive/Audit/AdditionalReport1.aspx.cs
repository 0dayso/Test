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

public partial class Audit_AdditionalReport1 : System.Web.UI.Page
{
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
        Btn_ShowAuditNo1.Enabled = false;
        lblValidation.Visible = false;
        try
        {
            btnPrint.Disabled = false;
            Btn_Excel.Enabled = true;
            string ShowName = ddl_Play.SelectedValue;
            string ShowLocation = ddl_Location.SelectedValue;
            string ShowDate = dateofshow.Text.Length > 0 ? dateofshow.Text : "0";
            string ShowTime = ddl_ShowTimes.SelectedValue;
            string ShowTime1 = ddl_ShowTimes.SelectedItem.Text;
            string ShowDate1 = Convert.ToDateTime(dateofshow.Text.ToString()).ToString("dd/MM/yyyy");
            DataTable dtauditno1report = TransactionBOL.AuditNumber1Report(ShowTime1, ShowDate1, ShowName, ShowLocation);
            if (dtauditno1report != null && dtauditno1report.Rows.Count > 0)
            {
                gv_Report.DataSource = dtauditno1report;
                gv_Report.DataBind();
            }
            else
            {
                btnPrint.Disabled = true;
                Btn_Excel.Enabled = false;
                lblMess.Text = "No Records Found!";
            }
        }
        catch (Exception ex)
        {
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write(ex.Message);
        }

    }
    protected void gv_Report_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }

    protected void gv_Report_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }


    protected void gv_Report_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void Btn_Excel_Click(object sender, EventArgs e)
    {
        GridViewExportUtil.Export("Record.xls", gv_Report);
    }
}

