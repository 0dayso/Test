using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KoDTicketing.BusinessLayer;
using KoDTicketing.DataAccessLayer;
using System.Data;

public partial class MarchPromotion : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void ddl_Package1_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_Quantity.Items.Clear();
        ddl_Quantity.Items.Add(new ListItem("Select", "0"));
        ddl_ShowTime.Items.Clear();
        ddl_ShowTime.Items.Add(new ListItem("Select", "0"));
        ddl_Date.Items.Clear();
        ddl_Date.Items.Add(new ListItem("Select", "0"));
        if (ddl_Package1.SelectedValue == "Rs.1275")
        {
            ddl_Date.Items.Clear();
            ddl_Date.Items.Add(new ListItem("Select", "0"));
            ddl_Date.Items.Add(new ListItem("20 March, 2013", "1"));
        }
        else if (ddl_Package1.SelectedValue == "Rs.4999")
        {
            ddl_Date.Items.Clear();
            ddl_Date.Items.Add(new ListItem("Select", "0"));
            ddl_Date.Items.Add(new ListItem("17 March, 2013", "1"));
            ddl_Date.Items.Add(new ListItem("24 March, 2013", "2"));
            ddl_Date.Items.Add(new ListItem("31 March, 2013", "3"));
        }
        else
        {
            ddl_Date.Items.Clear();
            ddl_Date.Items.Add(new ListItem("Select", "0"));
        }
    }
   
    protected void ddl_Date_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_Quantity.Items.Clear();
        ddl_ShowTime.Items.Clear();
        ddl_ShowTime.Items.Add(new ListItem("Select", "0"));
        ddl_Quantity.Items.Add(new ListItem("Select", "0"));
        String PlayDate = "";
        String filmCode = ddl_Package1.SelectedValue;
        if (ddl_Date.SelectedItem.Text!="Select")
        {
             PlayDate = Convert.ToDateTime(ddl_Date.SelectedItem.Text).ToString("dd/MM/yyyy");
        }
        
        //PlayDate = PlayDate.Replace("-", "/"); // for local/dev
        String Location = "NMJM";

        foreach (DataRow dr in VistaBOL.Select_Play(Location, filmCode, PlayDate).Rows)
        {
            if (ddl_Package1.SelectedValue == "Rs.1275")
            {
                ddl_ShowTime.Items.Add(new ListItem("2:30 PM", dr[1].ToString().Trim()));
            }
            if (ddl_Package1.SelectedValue == "Rs.4999")
            {
                ddl_ShowTime.Items.Add(new ListItem("7:30 PM", dr[1].ToString().Trim()));
            }
            
        }
    }
    protected void ddl_ShowTime_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_Quantity.Items.Clear();
        ddl_Quantity.Items.Add(new ListItem("Select", "0"));
        if (ddl_ShowTime.SelectedItem.Text != "Select")
        {
            if (ddl_Package1.SelectedValue == "Rs.1275")
            {
                ddl_Quantity.Items.Add(new ListItem("5", "5"));
                ddl_Quantity.Items.Add(new ListItem("6", "6"));
                ddl_Quantity.Items.Add(new ListItem("7", "7"));
                ddl_Quantity.Items.Add(new ListItem("8", "8"));
                ddl_Quantity.Items.Add(new ListItem("9", "9"));
                ddl_Quantity.Items.Add(new ListItem("10", "10"));
            }
            if (ddl_Package1.SelectedValue == "Rs.4999")
            {
                ddl_Quantity.Items.Add(new ListItem("1", "2"));
                ddl_Quantity.Items.Add(new ListItem("2", "4"));
                ddl_Quantity.Items.Add(new ListItem("3", "6"));
                ddl_Quantity.Items.Add(new ListItem("4", "8"));
                ddl_Quantity.Items.Add(new ListItem("5", "10"));
                ddl_Quantity.Items.Add(new ListItem("6", "12"));
                ddl_Quantity.Items.Add(new ListItem("7", "14"));
                ddl_Quantity.Items.Add(new ListItem("8", "16"));
                ddl_Quantity.Items.Add(new ListItem("9", "18"));
                ddl_Quantity.Items.Add(new ListItem("10", "20"));

            }
        }
    }

    protected void btn_Submit_Click(object sender, EventArgs e)
    {
        List<KoDTicketingLibrary.DTO.Promotion> listPromo = VistaBOL.GetPromostionCode();
        Session["Package"] = ddl_Package1.SelectedValue;

        int list = listPromo.Count;
        string category = "";
        string PC = "";
        if (Session["Package"] == "Rs.1275")
        {
            PC = "MONTHOFMARCH";
        }
        else if (Session["Package"] == "Rs.4999")
        {
            PC = "MARCHPROMOTION";
        }
        for (int i = 0; i < list; i++)
        {
            if (listPromo[i].PromotionCode.ToString().ToUpper() == PC.ToString())
            {
                string WebPromotionId = "KOD40MOM";
                listPromo[i].WebPromotionId = WebPromotionId.ToUpper();
                Session["PromotionCode"] = listPromo[i];
                Session["Hotel"] = listPromo[i].PromotionCode.ToString();
                if (listPromo[i].CO == 1)
                {
                    category = "COPPER";

                }
                else if (listPromo[i].SL == 1)
                {
                    category = "SILVER";
                }
                else if (listPromo[i].GL == 1)
                {
                    category = "GOLD";
                }
                else if (listPromo[i].PL == 1)
                {
                    category = "PLATINUM";

                }
                else if (listPromo[i].BZ == 1)
                {
                    category = "BRONZE";
                }
                else if (listPromo[i].DM == 1)
                {
                    category = "DIAMOND";
                }
            }
        }

        String PlayDate = Convert.ToDateTime(ddl_Date.SelectedItem.Text).ToString("dd/MM/yyyy");
        if (Session["Package"] == "Rs.1275")
        {
            Session["seat_Val"] = "NMJM" + "," + "JHUMROO" + "," + PlayDate +
                 "," + ddl_ShowTime.SelectedValue + "," + "SL" + "," + ddl_Quantity.SelectedValue +
                 "," + "Jhumroo @ Nautanki Mahal" + "," + "2:30 PM" + "," + category;
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Render Seat Layout for " + Session["seat_Val"].ToString());
            if (Session["Hotel"] == null)
            {
                Session["Hotel"] = "";
            }
            Response.Redirect("Seat-Layout.aspx?March=s", false);


        }

        else if (Session["Package"] == "Rs.4999")
        {
            Session["seat_Val"] = "NMJM" + "," + "JHUMROO" + "," + PlayDate +
                 "," + ddl_ShowTime.SelectedValue + "," + "GL" + "," + ddl_Quantity.SelectedValue +
                 "," + "Jhumroo @ Nautanki Mahal" + "," + "7:30 PM" + "," + category;
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Render Seat Layout for " + Session["seat_Val"].ToString());
            if (Session["Hotel"] == null)
            {
                Session["Hotel"] = "";
            }
            Response.Redirect("Seat-Layout.aspx?March=s", false);

        }
    }
}
    