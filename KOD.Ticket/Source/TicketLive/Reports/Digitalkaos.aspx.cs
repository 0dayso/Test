using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KoDUtilities;
using System.Data;
using KoDTicketing.BusinessLayer;

public partial class Reports_Digitalkaos : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnGo_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsPostBack)
            {
                lblMess.Text = "No Records Found!";
                gv_Report.Dispose();
                gv_Report.DataBind();
            }
            string DateFrom = txt_Date.Text.Length > 0 ? txt_Date.Text : "0";
            string DateTo = txt_DateTo.Text.Length > 0 ? txt_DateTo.Text : "0";
            DataSet ds = TransactionBOL.Select_Report_tbl_Digitalkasos(DateFrom, DateTo);
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
        }
        catch (Exception ex)
        {
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write(ex.Message);
        }
    }
    protected void Btn_Excel_Click(object sender, EventArgs e)
    {
        GridViewExportUtil.Export("Record.xls", gv_Report);
    }
}