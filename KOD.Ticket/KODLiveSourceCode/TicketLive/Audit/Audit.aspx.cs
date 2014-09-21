using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KoDUtilities;
using KoDTicketing.BusinessLayer;
using System.Data;

public partial class Audit_Audit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["ac"] != null)
            {
                Session["AgentCode"] = Request.QueryString["ac"];
            }
            Load_Play();
        }
    }
    void Load_Play()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add(new DataColumn("ShowName", typeof(string)));
        dt.Columns.Add(new DataColumn("Location", typeof(string)));
        dt.Columns.Add(new DataColumn("Show Date", typeof(string)));
        dt.Columns.Add(new DataColumn("Show Time", typeof(string)));
        dt.Columns.Add(new DataColumn("Show Time Code", typeof(string)));
        dt.Columns.Add(new DataColumn("locationcode", typeof(string)));
        AuditReport ar = new AuditReport();
        DateTime PlayDate = DateTime.Now;
        ar.Date = Convert.ToDateTime(PlayDate.ToString());
        Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Start Load Play");
        DataTable dtlocation = VistaBOL.Select_Play();
        if (dtlocation != null && dtlocation.Rows.Count > 0)
        {
            foreach (DataRow dr in dtlocation.Rows)
            {
                ar.ShowName = dr[0].ToString();
                DataTable dtAudi = VistaBOL.Select_Audi(ar.ShowName.ToString());
                foreach (DataRow dr1 in dtAudi.Rows)
                {
                    ar.LocationText = dr1[1].ToString();
                    ar.LocationValue = dr1[0].ToString();
                    DataTable drtime = VistaBOL.Select_PlayTime_Audit(ar.LocationValue, ar.ShowName, ar.Date.ToString("dd/MM/yyyy"));
                    foreach (DataRow dr2 in drtime.Rows)
                    {
                        ar.Timetext = Convert.ToDateTime(dr2[0].ToString());
                        ar.Timevalue = dr2[1].ToString();
                        dt.Rows.Add(ar.ShowName, ar.LocationText, ar.Date.ToString("ddd, MMM dd,yyyy"), ar.Timetext.ToShortTimeString(), ar.Timevalue, ar.LocationValue);
                    }
                }
        
            }
        }
        DataSet ds = new DataSet();
        ds.Tables.Add(dt);
        GridView_ShowDetail.DataSource = ds.Tables[0];
        GridView_ShowDetail.DataBind();
    }
    //        //List<AuditReport> ls = new List<AuditReport>()
    //        //{
    //        //new AuditReport{ShowName=ar.ShowName.ToString(),LoctionText=ar.LoctionText.ToString(),Date=ar.Date,Time1=ar.Time1}
    //        //};

    protected void GridViewShowDetail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "NewAudit")
        {
            Session["shownaudit_detail"] = e.CommandArgument.ToString();
            String Session_value;
            string[] sessionvalue;
            Session_value = Session["shownaudit_detail"].ToString();
            sessionvalue = Session_value.Split(',');
            DataTable dt = TransactionBOL.Check_AuditCount(sessionvalue[3].ToString(), sessionvalue[0].ToString(), DateTime.Now.ToString("dd/MM/yyyy"), sessionvalue[2].ToString());
            if (dt.Rows.Count == 2)
            {
                Page.ClientScript.RegisterStartupScript(typeof(Page), "Query", "alert('Already have 2 audit' !);", true);
                Label1.Visible = true;
                Label1.Text = "This show already have 2 audit";
            }
            else
            {
                Response.Redirect("AuditLayout.aspx", false);
            }
        }
    }
}
