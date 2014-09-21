using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using KoDTicketing.BusinessLayer;

public partial class NewYearPackages_Print_Receipt : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblCouple.Visible = false;
        lblCoupleQnty.Visible = false;
        lblKids.Visible = false;
        lblKidsQnty.Visible = false;
        lblSingle.Visible = false;
        lblSingleQnty.Visible = false;
        lblTeens.Visible = false;
        lblTeensQnty.Visible = false;
        try
        {
            if (Request.QueryString["b"] != null)
            {

                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("[B] Printing Receipt For  " + Request.QueryString["b"].ToString());
                dvDetails.Visible = true;
                dvErrorDetail.Visible = false;
                string NYBookingID = Request.QueryString["b"].ToString();
                DataTable dt = TransactionBOL.Select_NewYearTransaction_REFIDWISE(NYBookingID);
                if (dt.Rows.Count > 0)
                {
                    Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("[B] Printing Receipt with transaction details for  " + Request.QueryString["b"].ToString());
                    DataRow dr = dt.Rows[0];
                    lblBookingID.Text = dr["BookingId"].ToString();
                    lblReceiptNo.Text = dr["PGReceiptId"].ToString();
                    lbltotalamount.Text = dr["TotalAmount"].ToString();
                    lblPayMode.Text = "Credit/Debit Card";
                    DateTime dob = Convert.ToDateTime(dr["DateOfBooking"].ToString()).ToLocalTime();
                    lblDOB.Text = dob.ToString();
                    lblemailid.Text = dr["EmailId"].ToString();
                    if (Convert.ToInt16(dr["Qty_PackageTypeCouple"]) > 0)
                    {
                        lblCouple.Visible = true;
                        lblCoupleQnty.Visible = true;
                        lblCoupleQnty.Text = Convert.ToInt16(dr["Qty_PackageTypeCouple"]).ToString();
                    }
                    if (Convert.ToInt16(dr["Qty_PackageTypeSingle"]) > 0)
                    {
                        lblSingle.Visible = true;
                        lblSingleQnty.Visible = true;
                        lblSingleQnty.Text = Convert.ToInt16(dr["Qty_PackageTypeSingle"]).ToString();
                    }
                    if (Convert.ToInt16(dr["Qty_PackageTypeTeen"]) > 0)
                    {
                        lblTeens.Visible = true;
                        lblTeensQnty.Visible = true;
                        lblTeensQnty.Text = Convert.ToInt16(dr["Qty_PackageTypeTeen"]).ToString();
                    }
                    if (Convert.ToInt16(dr["Qty_PackageTypeKid"]) > 0)
                    {
                        lblKids.Visible = true;
                        lblKidsQnty.Visible = true;
                        lblKidsQnty.Text = Convert.ToInt16(dr["Qty_PackageTypeKid"]).ToString();
                    }
                    //lblpackagetype.Text = dr["PackageType"].ToString();
                    //lblquantity.Text = dr["PackageQty"].ToString();
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