using System;
using System.Data;
using GenCode128;
using System.Web;
using KoDTicketing.BusinessLayer;

public partial class Sumer_Camp_Print_Receipt : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Request.QueryString["b"] != null)
            {

                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("[B] Printing Receipt For  " + Request.QueryString["b"].ToString());
                dvDetails.Visible = true;
                dvErrorDetail.Visible = false;
                string SummerBookingID = Request.QueryString["b"].ToString();
                DataTable dt = TransactionBOL.Select_SummerTransaction_REFIDWISE(SummerBookingID);
                if (dt.Rows.Count > 0)
                {
                    Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("[B] Printing Receipt with transaction details for  " + Request.QueryString["b"].ToString());
                    DataRow dr = dt.Rows[0];
                    lblBookingID.Text = dr["BookingId"].ToString();
                    lblReceiptNo.Text = dr["ReceiptId"].ToString();
                    lblquantity.Text = dr["Nooftickets"].ToString();
                    Int32 totalamount = Convert.ToInt32(dr["PayableAmount"]);
                    lbltotalamount.Text = totalamount.ToString();
                    lblPayMode.Text = "Credit/Debit Card";
                    lblDOB.Text = dr["Dateofbooking"].ToString();
                    lblemailid.Text = dr["Email"].ToString();
                    //lblpackagetype.Text = dr["PackageType"].ToString();
                    //lblquantity.Text = dr["PackageQty"].ToString();
                    lblcontactNo.Text = dr["ContactNumber"].ToString();
                    lnlname.Text = dr["Name"].ToString();
                    //DateTime day = Convert.ToDateTime(dr["ShowDate"]);
                    //string day1 = day.Day.ToString() + "/" + day.Month.ToString() + "/" + day.Year.ToString();
                    //lblshowdate.Text = day1;
                    //lblpnr.Text = Session["pnr"].ToString();
                    bool isSuccessfull = Convert.ToBoolean(dr["IsPaymentSuccess"]);
                    if (isSuccessfull)
                    {
                        lbltrnsresponse.Text = "Transaction Successful";
                    }

                    else
                    {
                        lbltrnsresponse.Text = "Transaction Not Successful";
                    }


                }

            }
            else if (Request.QueryString["err"] != null)
            {
                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Received Print Receipt with err value..." + Request.QueryString["err"].ToString());
                string err = Request.QueryString["err"].ToString();
                if (err == "pay")
                {
                    lblFinalMess.Text = "Your transaction was not successful. Please try later.";
                    if (Request["ErrorText"] != null)
                        lblFinalMess.Text += Environment.NewLine + "Transaction failed because" + Request["ErrorText"].ToString();
                }
                else if (err == "seat")
                {
                    lblFinalMess.Text = "Your payment transaction was successful, but your seats were not booked due to technical glitch, please contact (0124)4528000 for Seat Confirmation. Sorry for inconvenience.";
                }
                dvDetails.Visible = false;
                dvErrorDetail.Visible = true;
                lblDate.Text = System.DateTime.Now.ToString();
            }
            else
            {
                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Received Print Receipt without parameter...");
                dvErrorDetail.Visible = true;
                dvDetails.Visible = false;
                lblFinalMess.Text = "Sorry your payment transaction was not successful, please try again after some time.";
            }
            lblDate.Text = "Date : " + System.DateTime.Now.ToString();
        }

        catch (Exception ex)
        {
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Error Printing Receipt For " + ex.Message);
        }
        Session.Clear();

    }
    protected void BtnBackToHome_Click(object sender, EventArgs e)
    {
        Session.Clear();
        Response.Redirect("http://www.kingdomofdreams.in/");
    }
}