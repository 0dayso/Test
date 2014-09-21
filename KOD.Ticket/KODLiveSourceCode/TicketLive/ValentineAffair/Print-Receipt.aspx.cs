using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KoDTicketing.BusinessLayer;
using System.Data;

public partial class ValentineAffair_PrintReceipt : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblPackage1.Visible = false;
        lblPackage1Qnty.Visible = false;
        //lblPackage2.Visible = false;
        //lblPackage2Qnty.Visible = false;
        //lblPackage3.Visible = false;
        //lblPackage3Qnty.Visible = false;

        try
        {
            if (Request.QueryString["b"] != null)
            {

                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("[B] Printing Receipt For  " + Request.QueryString["b"].ToString());
                dvDetails.Visible = true;
                dvErrorDetail.Visible = false;
                 string VLBookingID = Request.QueryString["b"].ToString();
                DataTable dt = TransactionBOL.Select_ValentineTransaction(VLBookingID);

                 if (dt.Rows.Count > 0)
                {
                    Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("[B] Printing Receipt with transaction details for  " + Request.QueryString["b"].ToString());
                    DataRow dr = dt.Rows[0];
                    lblBookingID.Text = dr["BookingId"].ToString();
                    lblReceiptNo.Text = dr["PGReceiptId"].ToString();
                    lblquantity.Text = dr["Quantity"].ToString();
                    lbltotalamount.Text = dr["TotalAmount"].ToString();
                    lblPayMode.Text = "Credit/Debit Card";
                    lblDOB.Text = dr["DateOfBooking"].ToString();
                    lblemailid.Text = dr["EmailId"].ToString();
                     if (Convert.ToInt16(dr["Package"]) > 0)
                    {
                        lblPackage1.Visible = true;
                        lblPackage1Qnty.Visible = true;
                        lblPackage1Qnty.Text =(dr["Package"]).ToString();
                    }
                    //  if (Convert.ToInt16(dr["Couple_3999"]) > 0)
                    //{
                    //    lblPackage2.Visible = true;
                    //    lblPackage2Qnty.Visible = true;
                    //    lblPackage2Qnty.Text = Convert.ToInt16(dr["Couple_3999"]).ToString();
                    //}
                    //  if (Convert.ToInt16(dr["Couple_3499"]) > 0)
                    //{
                    //    lblPackage3.Visible = true;
                    //    lblPackage3Qnty.Visible = true;
                    //    lblPackage3Qnty.Text = Convert.ToInt16(dr["Couple_3499"]).ToString();
                    //}
                      lblcontactNo.Text = dr["ContactNumber"].ToString();
                      lnlname.Text = dr["Name"].ToString();
                       bool isSuccessfull = Convert.ToBoolean(dr["PGIsPaymentSuccess"]);
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



}