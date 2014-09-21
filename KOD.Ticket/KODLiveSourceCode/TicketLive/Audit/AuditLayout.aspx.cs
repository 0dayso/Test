using System;
using System.Data;
using System.Web.UI.WebControls;
using KoDTicketing;
using KoDTicketing.BusinessLayer;
using System.Web.UI;
using System.Web;
using System.Web.Services;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Web.Script.Serialization;

public partial class Audit_AuditLayout : System.Web.UI.Page
{
    string Url="";
    public string ocu = "";
    public string otu = "";
    public string unpaid = "";
    DataTable dtaudit = new DataTable();
    public String Session_value;
    public String SessionAuditno;
    protected string[] sessionvalue;
    public string Id="";
    public class jdata
    {
        public string SeatID { get; set; }
        public string AuditNo { get; set; }
        public string ShowName { get; set; }
        public string ShowLocation { get; set; }
        public string ShowDate { get; set; }
        public string ShowTime { get; set; }
        public string Status { get; set; }
        public string Remark { get; set; }
        public string EditTime { get; set; }
        public string SeatDescription { get; set; }
        public string Iscompleted { get; set; }
        public string Category { get; set; }
        public string ERPStatus { get; set; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Show audit start");
        if (Session["shownaudit_detail"].ToString() != "")
        {
            Session_value = Session["shownaudit_detail"].ToString();
            sessionvalue = Session_value.Split(','); 
        }
        if (!IsPostBack)
        {
            DataTable dtdelete = TransactionBOL.Delete_Iscomplete();
            DataTable dt = TransactionBOL.Check_AuditDetails(sessionvalue[3].ToString(), sessionvalue[0].ToString(), DateTime.Now.ToString("dd/MM/yyyy"), sessionvalue[2].ToString());
            if (dt.Rows.Count == 0)
            {
                Session["AuditNo"] = 1;
                SessionAuditno = Session["AuditNo"].ToString();
            }
            else
            {
                Session["AuditNo"] = 2;
                SessionAuditno = Session["AuditNo"].ToString();
            }
        }
        set_seatLayout();
        //booked();
    }
    void set_seatLayout()
    {
        Table objtable = new Table();
        objtable.CellPadding = 0;
        objtable.CellSpacing = 0;
        TableRow objtr = new TableRow();
        TableCell objtd = new TableCell();
        string[] seat_val = new string[4];
        bool isfilled = false;
        if (Session_value != null)
        {
            if (Session_value != "")
            {
                seat_val = Session_value.Split(',');
                isfilled = false;
            }
            else
            {
                Session.Abandon();
                ClientScript.RegisterStartupScript(GetType(), "myscript", "<script>alert('Session Timeout. Please start" +
                            " the transaction again');window.location.href='Default.aspx';</script>");
            }

            if (seat_val.Length < 4)
            {
                throw new Exception("Seat Layout cannot be done as session value is no valid Session: " + (isfilled ? Session_value : Session_value));
            }


            String filmCode = seat_val[0];
            String audiNo = seat_val[1];
            
            DataTable dtrows = GTICKV.SelectRow_AudiWise(filmCode);
            System.Diagnostics.Trace.Assert((dtrows == null), "Rows could not be fetched for selected Audi");

            int maxCol = GTICKV.maxColumns(filmCode);
            int maxRows = GTICKV.maxRows(filmCode);

           // DataTable dtseatlayout = GTICKV.AllSeats(audiNo);
            DataTable dtseatlayout = GTICKV.Audit_AllSeats(audiNo);
           // DataTable dtseatlayout = GTICKV.AuditAllSeats(audiNo);
            System.Diagnostics.Trace.Assert(dtseatlayout == null, "Rows could not be fetched for selected Audi");

            int temptablecellcount = 0, tablecell = 0;
            if (dtseatlayout.Rows.Count > 0 && dtrows.Rows.Count > 0)
            {
                for (int tablerow = 0; tablerow < maxRows; tablerow++)
                {
                    DataRow drseats = dtrows.Rows[tablerow];
                    objtr = new TableRow();
                    objtable.Rows.Add(objtr);
                    objtd = new TableCell();
                    objtd.Text = drseats[5].ToString();
                    objtr.Cells.Add(objtd);
                    for (tablecell = 0 + temptablecellcount; tablecell < maxCol + temptablecellcount; tablecell++)
                    {
                        DataRow drrow = dtseatlayout.Rows[tablecell];
                        objtd = new TableCell();
                        objtd.Attributes.Add("class", "pad");
                        if (drrow[3].ToString() == "1")
                        {
                            if (drrow[14].ToString() != "" && drrow[14].ToString() !=null && int.Parse(drrow[15].ToString())==0)
                            {
                                Url = "Images/Unpaid_seat.gif";
                                //  if (drrow[6].ToString().ToLower() == seat_val[4].ToLower())
                                // {
                                objtd.Text = "<img src='" + Url + "' alt='" + drrow[7] + " - " + drrow[13] + "'  OnClick='myFunction1(this)' id='" + drrow[7] + "-" + drrow[13] + "," + DateTime.Now.ToString("dd/MM/yyyy") + "," + seat_val[2].ToString() +",U"+ "'  />";
                                objtd.ID = "Seat_" + drrow[6] + "_" + drrow[2] + "_" + drrow[7].ToString() + "_" + drrow[0].ToString();
                                otu = otu + "" + drrow[7] + "-" + drrow[13] + "," + DateTime.Now.ToString("dd/MM/yyyy") + "," + seat_val[2].ToString() + ",U" + "" + "~";

                            }
                            else if (drrow[9].ToString() == "1" || drrow[11].ToString() == "1" || drrow[12].ToString() == "1")
                            {
                                Url = "Images/W_Chair.gif";
                                //  if (drrow[6].ToString().ToLower() == seat_val[4].ToLower())
                                // {
                                objtd.Text = "<img src='" + Url + "' alt='" + drrow[7] + " - " + drrow[13] + "'  OnClick='myFunction1(this)' id='" + drrow[7] + "-" + drrow[13] + "," + DateTime.Now.ToString("dd/MM/yyyy") + "," + seat_val[2].ToString() +",B"+ "'  />";
                                objtd.ID = "Seat_" + drrow[6] + "_" + drrow[2] + "_" + drrow[7].ToString() + "_" + drrow[0].ToString();
                                otu = otu + "" + drrow[7] + "-" + drrow[13] + "," + DateTime.Now.ToString("dd/MM/yyyy") + "," + seat_val[2].ToString() + ",B" + "" + "~";
                                
                            }
                            else
                            {
                                if (drrow[6].ToString() == "GLY")
                                {
                                    objtd.Text = "<img src='Images/Gallery_chair.gif' OnClick='myFunction1(this)'  id='" + drrow[7] + "-" + drrow[13] + "," + DateTime.Now.ToString("dd/MM/yyyy") + "," + seat_val[2].ToString() +",V"+ "' />";
                                    otu = otu + "" + drrow[7] + "-" + drrow[13] + "," + DateTime.Now.ToString("dd/MM/yyyy") + "," + seat_val[2].ToString() +",V"+ "" + "~";
                                }
                                else if (drrow[6].ToString() == "CO")
                                {
                                    objtd.Text = "<img src='Images/Copper_chair.gif' OnClick='myFunction1(this)' id='" + drrow[7] + "-" + drrow[13] + "," + DateTime.Now.ToString("dd/MM/yyyy") + "," + seat_val[2].ToString() +",V"+ "'  />";
                                    otu = otu + "" + drrow[7] + "-" + drrow[13] + "," + DateTime.Now.ToString("dd/MM/yyyy") + "," + seat_val[2].ToString() +",V"+ "" + "~";
                                }
                                else if (drrow[6].ToString() == "PL")
                                {
                                    objtd.Text = "<img src='Images/Platinum_chair.gif' OnClick='myFunction1(this)'  id='" + drrow[7] + "-" + drrow[13] + "," + DateTime.Now.ToString("dd/MM/yyyy") + "," + seat_val[2].ToString() +",V"+ "'  />";
                                    otu = otu + "" + drrow[7] + "-" + drrow[13] + "," + DateTime.Now.ToString("dd/MM/yyyy") + "," + seat_val[2].ToString() +",V"+ "" + "~";
                                }
                                else if (drrow[6].ToString() == "BZ")
                                {
                                    objtd.Text = "<img src='Images/Brown_chair.gif' OnClick='myFunction1(this)'  id='" + drrow[7] + "-" + drrow[13] + "," + DateTime.Now.ToString("dd/MM/yyyy") + "," + seat_val[2].ToString() +",V"+ "'  />";
                                    otu = otu + "" + drrow[7] + "-" + drrow[13] + "," + DateTime.Now.ToString("dd/MM/yyyy") + "," + seat_val[2].ToString() +",V"+ "" + "~";
                                }
                                else if (drrow[6].ToString() == "SL")
                                {
                                    objtd.Text = "<img src='Images/Silver_chair.gif' OnClick='myFunction1(this)'  id='" + drrow[7] + "-" + drrow[13] + "," + DateTime.Now.ToString("dd/MM/yyyy") + "," + seat_val[2].ToString() +",V"+ "'  />";
                                    otu = otu + "" + drrow[7] + "-" + drrow[13] + "," + DateTime.Now.ToString("dd/MM/yyyy") + "," + seat_val[2].ToString() +",V"+ "" + "~";
                                }
                                else if (drrow[6].ToString() == "DM")
                                {
                                    objtd.Text = "<img src='Images/Diamond_chair.gif' OnClick='myFunction1(this)'  id='" + drrow[7] + "-" + drrow[13] + "," + DateTime.Now.ToString("dd/MM/yyyy") + "," + seat_val[2].ToString() +",V"+ "' />";
                                    otu = otu + "" + drrow[7] + "-" + drrow[13] + "," + DateTime.Now.ToString("dd/MM/yyyy") + "," + seat_val[2].ToString() +",V"+ "" + "~";
                                }
                                else if (drrow[6].ToString() == "GL")
                                {
                                    objtd.Text = "<img src='Images/Gold_chair.gif' OnClick='myFunction1(this)'  id='" + drrow[7] + "-" + drrow[13] + "," + DateTime.Now.ToString("dd/MM/yyyy") + "," + seat_val[2].ToString() +",V"+ "' />";
                                    otu = otu + "" + drrow[7] + "-" + drrow[13] + "," + DateTime.Now.ToString("dd/MM/yyyy") + "," + seat_val[2].ToString() +",V"+ "" + "~";
                                }
                                //  }
                                //else
                                //    objtd.Text = "<img src='../Images/Gy_Chair.gif' OnClick='myFunction(this)' id='" + drrow[7] + " - " + drrow[13] + ", Price : " + String.Format("{0:#.##}", decimal.Parse(drrow[8].ToString())) + " INR'   />";
                            }
                        }
                        objtr.Cells.Add(objtd);
                    }
                    objtd = new TableCell();
                    objtd.Text = drseats[5].ToString();
                    objtr.Cells.Add(objtd);
                    temptablecellcount = tablecell;
                }
            }
            myform.Controls.Add(objtable);
        }
        else
        {
            Session.Abandon();
            ClientScript.RegisterStartupScript(GetType(), "myscript", "<script>alert('Session Timeout. Please start" +
                " the transaction again');window.location.href='Default.aspx';</script>");
        }
    }
    //void booked()
    //{
    //    string[] occuu=ocu.Split('~');
       
    //    Table objtable = new Table();
    //    objtable.CellPadding = 0;
    //    objtable.CellSpacing = 0;
    //    TableRow objtr = new TableRow();
    //    TableCell objtd = new TableCell();
    //    string[] seat_val = new string[4];
    //    //bool isfilled = false;
    //    for (int i = 0; i < occuu.Length-1; i++)
    //    {
    //         string[] finelbook=occuu[i].Split(',');
    //        objtr = new TableRow();
    //        objtable.Rows.Add(objtr);
    //        objtd = new TableCell();
    //        objtd.Text = "<input type='text'  value='" + finelbook[0] + "' id='id1'/>";
                
    //        objtr.Cells.Add(objtd);
    //        objtd = new TableCell();
    //        objtd.Text = "<select id='status" + i + "'><option>Select</option><option>Not Occupied by Customer</option><option>Occupied by Customer</option></select>";
    //        objtr.Cells.Add(objtd);
    //        objtd = new TableCell();
    //        objtd.Text = "<textarea id='remark" + i + "' ></textarea>";
    //        objtr.Cells.Add(objtd);
    //       // temptablecellcount = tablecell;
    //    }

    //   //Div3.Controls.Add(objtable);
    
    //}
  
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Session.Clear();
        Response.Redirect("Audit.aspx", false);
    }

    [WebMethod]
    public static string Set_Status(string AuditNo, string ShowName, string ShowLocation, string ShowDate, string ShowTime, string Iscompleted)
    {
        int i = GTICKBOL.Set_finalstatus(Convert.ToInt32(AuditNo),ShowName,ShowLocation, DateTime.Now.ToString("dd/MM/yyyy"), ShowTime, 1);
        if (i != 0)
        {
            return "true";
        }
        else
            return "false";
    }

    /*Insert Method for Booked Seats*/
    //[WebMethod]
    //public static string InsertMethod_booked(object myData)
    //{
    //    SqlConnection connection = new SqlConnection("server=biztrack.co.in;database=MSTicketDB_Live_Latest;uid=sa;pwd=kranti123@123;");
    //    SqlCommand cmd = new SqlCommand("delete from [dbo].[ShowAudit_Table] where ERPStatus='Occupied' and Iscompleted='"+0+"'", connection);
    //    try
    //    {
    //        connection.Open();
    //        cmd.ExecuteNonQuery();
    //    }
    //    catch (Exception ex)
    //    {
    //        return "false";
    //    }
    //    finally
    //    {
    //        connection.Close();
    //    }
    //    JavaScriptSerializer js = new JavaScriptSerializer();
    //    List<jdata> myformElement = js.Deserialize<List<jdata>>(myData.ToString());
    //    DataTable dt_insert = new DataTable();
    //    dt_insert.Columns.Add("timestamp", typeof(string));
    //    dt_insert.Columns.Add("SeatID", typeof(string));
    //    dt_insert.Columns.Add("AuditNo", typeof(int));
    //    dt_insert.Columns.Add("ShowName", typeof(string));
    //    dt_insert.Columns.Add("ShowLocation", typeof(string));
    //    dt_insert.Columns.Add("ShowDate", typeof(string));
    //    dt_insert.Columns.Add("ShowTime", typeof(string));
    //    dt_insert.Columns.Add("Status", typeof(string));
    //    dt_insert.Columns.Add("Remark", typeof(string));
    //    dt_insert.Columns.Add("EditTime", typeof(DateTime));
    //    dt_insert.Columns.Add("SeatDescription", typeof(string));
    //    dt_insert.Columns.Add("IsCompleted", typeof(int));
    //    dt_insert.Columns.Add("Category", typeof(string));
    //    dt_insert.Columns.Add("ERPStatus", typeof(string));
    //    foreach (var gg in myformElement)
    //    {
    //        dt_insert.Rows.Add("",gg.SeatID, Convert.ToInt32(gg.AuditNo), gg.ShowName, gg.ShowLocation, DateTime.Now.ToString("dd/MM/yyyy"), gg.ShowTime, gg.Status, gg.Remark, DateTime.Now.ToString(), gg.SeatDescription, Convert.ToInt32(gg.Iscompleted), gg.Category,"");
    //    }
       
    //    connection.Open();
    //    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connection))
    //    {
            
    //        bulkCopy.DestinationTableName ="[dbo].[ShowAudit_Table]";
    //        try
    //        {
    //            bulkCopy.WriteToServer(dt_insert);
    //        }
    //        catch (Exception ex)
    //        {
    //            return "false";
    //        }
    //        finally
    //        {
    //            connection.Close();
    //        }
    //    }
    //    return "true";  
        
    //}

    /* Insert Method for vaccant seats*/
    [WebMethod]
    public static string InsertMethod_Vacant(object myData_vaccant)
    {
        string check="";
        JavaScriptSerializer js = new JavaScriptSerializer();
        List<jdata> myformElement = js.Deserialize<List<jdata>>(myData_vaccant.ToString());
        DataTable dt_insertvaccant = new DataTable();
        dt_insertvaccant.Columns.Add("timestamp", typeof(string));
        dt_insertvaccant.Columns.Add("SeatID", typeof(string));
        dt_insertvaccant.Columns.Add("AuditNo", typeof(int));
        dt_insertvaccant.Columns.Add("ShowName", typeof(string));
        dt_insertvaccant.Columns.Add("ShowLocation", typeof(string));
        dt_insertvaccant.Columns.Add("ShowDate", typeof(string));
        dt_insertvaccant.Columns.Add("ShowTime", typeof(string));
        dt_insertvaccant.Columns.Add("Status", typeof(string));
        dt_insertvaccant.Columns.Add("Remark", typeof(string));
        dt_insertvaccant.Columns.Add("EditTime", typeof(DateTime));
        dt_insertvaccant.Columns.Add("SeatDescription", typeof(string));
        dt_insertvaccant.Columns.Add("IsCompleted", typeof(int));
        dt_insertvaccant.Columns.Add("Category", typeof(string));
        dt_insertvaccant.Columns.Add("ERPStatus", typeof(string));
        foreach (var vac in myformElement)
        {
            dt_insertvaccant.Rows.Add("", vac.SeatID, Convert.ToInt32(vac.AuditNo), vac.ShowName, vac.ShowLocation, DateTime.Now.ToString("dd/MM/yyyy"), vac.ShowTime, vac.Status, vac.Remark, DateTime.Now.ToString(), vac.SeatDescription, Convert.ToInt32(vac.Iscompleted), vac.Category,vac.ERPStatus);
            check=vac.SeatID.Substring(0, 1);
        }
        int i = GTICKBOL.Delete_audit(check);
        int j = GTICKBOL.Update_audit(dt_insertvaccant);
        if (j == 0)
        {
            return "false";
        }
        else
        {
            return "true";
        }
    }

    /* Insert Method for unpaid telebooking seats*/
    //[WebMethod]
    //public static string InsertMethod_Unpaid(object myData)
    //{
    //    SqlConnection connection = new SqlConnection("server=biztrack.co.in;database=MSTicketDB_Live_Latest;uid=sa;pwd=kranti123@123;");
    //    SqlCommand cmd = new SqlCommand("delete from [dbo].[ShowAudit_Table] where ERPStatus='Unpaid Tele Booking' and Iscompleted='" + 0 + "'", connection);
    //    try
    //    {
    //        connection.Open();
    //        cmd.ExecuteNonQuery();
    //    }
    //    catch (Exception ex)
    //    {
    //        return "false";
    //    }
    //    finally
    //    {
    //        connection.Close();
    //    }
    //    JavaScriptSerializer js = new JavaScriptSerializer();
    //    List<jdata> myformElement = js.Deserialize<List<jdata>>(myData.ToString());
    //    DataTable dt_insert = new DataTable();
    //    dt_insert.Columns.Add("timestamp", typeof(string));
    //    dt_insert.Columns.Add("SeatID", typeof(string));
    //    dt_insert.Columns.Add("AuditNo", typeof(int));
    //    dt_insert.Columns.Add("ShowName", typeof(string));
    //    dt_insert.Columns.Add("ShowLocation", typeof(string));
    //    dt_insert.Columns.Add("ShowDate", typeof(string));
    //    dt_insert.Columns.Add("ShowTime", typeof(string));
    //    dt_insert.Columns.Add("Status", typeof(string));
    //    dt_insert.Columns.Add("Remark", typeof(string));
    //    dt_insert.Columns.Add("EditTime", typeof(DateTime));
    //    dt_insert.Columns.Add("SeatDescription", typeof(string));
    //    dt_insert.Columns.Add("IsCompleted", typeof(int));
    //    dt_insert.Columns.Add("Category", typeof(string));
    //    dt_insert.Columns.Add("ERPStatus", typeof(string));
    //    foreach (var gg in myformElement)
    //    {
    //        dt_insert.Rows.Add("", gg.SeatID, Convert.ToInt32(gg.AuditNo), gg.ShowName, gg.ShowLocation, DateTime.Now.ToString("dd/MM/yyyy"), gg.ShowTime, gg.Status, gg.Remark, DateTime.Now.ToString(), gg.SeatDescription, Convert.ToInt32(gg.Iscompleted), gg.Category, "Unpaid Tele Booking");

    //    }

    //    connection.Open();
    //    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connection))
    //    {

    //        bulkCopy.DestinationTableName = "[dbo].[ShowAudit_Table]";
    //        try
    //        {
    //            bulkCopy.WriteToServer(dt_insert);
    //        }
    //        catch (Exception ex)
    //        {
    //            return "false";
    //        }
    //        finally
    //        {
    //            connection.Close();
    //        }
    //    }
    //    return "true";

    //}
   // [WebMethod]
    //public static string Delete_Booked(string SeatID)
    //{
    //    DataTable dtclear = TransactionBOL.Clear_AuditDetails(SeatID);
    //    if (dtclear != null)
    //        return "true";
    //    else
    //        return "false";
    //}
    //[WebMethod]
    //public static string Delete_Vacant(string SeatID)
    //{
    //    DataTable dtclear = TransactionBOL.Clear_AuditDetails(SeatID);
    //    if (dtclear != null)
    //        return "true";
    //    else
    //        return "false";
    //}
    //protected void btnProceed_Click(object sender, EventArgs e)
    //{
    //   // Label4.Text = "";
    //    int i = GTICKBOL.Set_finalstatus(Convert.ToInt32(Session["AuditNo"]), sessionvalue[3].ToString(), sessionvalue[0].ToString(), DateTime.Now.ToString("dd/MM/yyyy"), sessionvalue[2].ToString(), 1);
    //    if (i == 0)
    //    {
    //       // ClientScript.RegisterStartupScript(typeof(Page), "test", "alert('Please Select Date on Date Of Birth');", true);
    //        //Label4.Text = "Audit is not saved please try again";
    //    }
    //    else
    //    {
    //        Session.Clear();
    //        Response.Redirect("Audit.aspx", false);
    //    }
    //}
    
    //protected void btnedit_Click(object sender, EventArgs e)
    //{
    //    DataTable dtclear = TransactionBOL.Clear_AuditDetails(TextBox3.Text + "_" + Session["AuditNo"]);
    //}
    //protected void btneditvaccant_Click(object sender, EventArgs e)
    //{
    //    DataTable dtclear = TransactionBOL.Clear_AuditDetails(TextBox2.Text + "_" + Session["AuditNo"]);
    //}
    //protected void btnvacant_Click(object sender, EventArgs e)
    //{
    //    Id = TextBox2.Text;
    //    //  Div2.Visible = true;
    //    DataTable dtinsert = TransactionBOL.Insert_AuditDetails(TextBox2.Text + "_" + Session["AuditNo"], Convert.ToInt32(Session["AuditNo"]), sessionvalue[3].ToString(), sessionvalue[0].ToString(), DateTime.Now.ToString("dd/MM/yyyy"), sessionvalue[2].ToString(), DropDownList2.SelectedItem.Text, Textarea1.InnerText, DateTime.Now, TextBox1.Text, 0, Category1.Text);
    //    if (dtinsert != null)
    //    {
    //        Label4.Text = "Data is Successfully added";
    //        UpdatePanel2.Update();
    //    }
    //    else
    //    {
    //        Label4.Text = "Data is not added please try again";
    //    }

    //    Textarea1.InnerText = "";
    //    DropDownList2.SelectedIndex = 0;
    //    UpdatePanel1.Update();

    //}
    //protected void btnbooked_Click(object sender, EventArgs e)
    //{
    //    Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("test=" + Label1.Text.ToString());
    //    DataTable dtinsert = TransactionBOL.Insert_AuditDetails(TextBox3.Text + "_" + Session["AuditNo"], Convert.ToInt32(Session["AuditNo"]), sessionvalue[3].ToString(), sessionvalue[0].ToString(), DateTime.Now.ToString("dd/MM/yyyy"), sessionvalue[2].ToString(), DropDownList1.SelectedItem.Text, dd1.InnerText, DateTime.Now, Label1.Text, 0,Category2.Text);
    //    if (dtinsert != null)
    //    {
    //        //Div2.Visible = true;
    //        Label4.Text = "Data is Successfully added";
    //    }
    //    else
    //    {
    //       // Div2.Visible = true;
    //        Label4.Text = "Data is not added please try again";
    //    }
    //    dd1.InnerText = "";
    //    DropDownList1.SelectedIndex = 0;
    //}
}
   
   
