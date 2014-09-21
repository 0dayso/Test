using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KoDTicketing.BusinessLayer;
using System.Data;
using System;

namespace KoDUtilities
{
    public class GridViewExportUtil
    {
        public GridViewExportUtil()
        {

        }
        public static void Export(string fileName, GridView gv)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.AddHeader(
            "content-disposition", string.Format("attachment; filename={0}", fileName));
            HttpContext.Current.Response.ContentType = "application/ms-excel";

            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    Table table = new Table();
                    if (gv.HeaderRow != null)
                    {
                        table.Rows.Add(gv.HeaderRow);
                    }

                    foreach (GridViewRow row in gv.Rows)
                    {
                        GridViewExportUtil.PrepareControlForExport(row);
                        table.Rows.Add(row);
                    }
                    if (gv.FooterRow != null)
                    {
                        GridViewExportUtil.PrepareControlForExport(gv.FooterRow);
                        table.Rows.Add(gv.FooterRow);
                    }
                    table.RenderControl(htw);
                    HttpContext.Current.Response.Write(sw.ToString());
                    HttpContext.Current.Response.End();
                }
            }
        }

        /// <summary>
        /// Replace any of the contained controls with literals
        /// </summary>
        /// <param name="control"></param>
        private static void PrepareControlForExport(Control control)
        {
            for (int i = 0; i < control.Controls.Count; i++)
            {
                Control current = control.Controls[i];
                if (current is LinkButton)
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl((current as LinkButton).Text));
                }
                else if (current is ImageButton)
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl((current as ImageButton).AlternateText));
                }
                else if (current is HyperLink)
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl((current as HyperLink).Text));
                }
                else if (current is DropDownList)
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl((current as DropDownList).SelectedItem.Text));
                }
                else if (current is CheckBox)
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl((current as CheckBox).Checked ? "True" : "False"));
                }

                if (current.HasControls())
                {
                    GridViewExportUtil.PrepareControlForExport(current);
                }
            }
        }
    }

    public class KODHelper
    {
        public TransactionRecord GetPromotionDetails(TransactionRecord tr)
        {
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Booking ID Response from PG " + tr.BookingID.ToString());
            if (tr.BookingID.ToString().Substring(0,2)=="31")
            {
                DataTable chkID = TransactionBOL.Select_MarchPromotionTransactionCounter_CounterIDWise(tr.BookingID);
                if (chkID.Rows.Count > 0)
                {
                    Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Booking ID and Conter from March Promotion Table " + chkID.Rows[0][1].ToString() + "," + chkID.Rows[0][0].ToString());
                    if (chkID.Rows[0][0].ToString().Substring(0, 2) == "31")
                    {
                        tr.BookingID = long.Parse(chkID.Rows[0][1].ToString());
                    }
                }
                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Booking ID after Switch " + tr.BookingID.ToString());
            }
            DataTable dttransaction = TransactionBOL.Select_Temptransaction_transactionIDWise(tr.BookingID);
            if (dttransaction.Rows.Count > 0)
            {
                DataRow dr = dttransaction.Rows[0];
                decimal DiscountPercentage = decimal.Parse(dr["DiscountPercentage"].ToString());

                tr.PromotionCode = dr["PromotionCode"].ToString();
                tr.DiscountPercentage = DiscountPercentage;
                tr.WebPromotionId = dr["WebPromotionId"].ToString();

            }
            return tr;
        }

        public TransactionRecord GetRoyalCardDetails(TransactionRecord tr)
        {
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Booking ID Response from PG " + tr.BookingID.ToString());
            if (tr.BookingID.ToString().Substring(0, 2) == "31")
            {
                DataTable chkID = TransactionBOL.Select_MarchPromotionTransactionCounter_CounterIDWise(tr.BookingID);
                if (chkID.Rows.Count > 0)
                {
                    Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Booking ID and Conter from March Promotion Table " + chkID.Rows[0][1].ToString() + "," + chkID.Rows[0][0].ToString());
                    if (chkID.Rows[0][0].ToString().Substring(0, 2) == "31")
                    {
                        tr.BookingID = long.Parse(chkID.Rows[0][1].ToString());
                    }
                }
                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Booking ID after Switch " + tr.BookingID.ToString());
            }
            DataTable dttransaction = TransactionBOL.Select_Temptransaction_transactionIDWise(tr.BookingID);
            if (dttransaction.Rows.Count > 0)
            {
                DataRow dr = dttransaction.Rows[0];
                string RegId = dr["RegId"].ToString();
                if (!string.IsNullOrEmpty(RegId))
                {
                    tr.RegId = dr["RegId"].ToString();
                    if (dr["TopUpTransactionId"] != null)
                    {
                        tr.TopUpTransactionId = dr["TopUpTransactionId"].ToString();
                    }
                    tr.AvailedAmount = Convert.ToDecimal(dr["AvailedAmount"]);
                    tr.AvailedPoints = Convert.ToDecimal(dr["AvailedPoints"]);
                    tr.TopUpAmount = Convert.ToDecimal(dr["TopUpAmount"]);
                    tr.TotalAmount = Convert.ToDecimal(dr["TotalAmount"]);
                    if (dr["Play"] != null)
                    {
                        tr.Play = Convert.ToString(dr["Play"]);
                    }
                    if (dr["MobileNo"] != null)
                    {
                        tr.MobileNo = Convert.ToString(dr["MobileNo"]);
                    }
                    tr.TotalSeats = Convert.ToInt32(dr["TotalSeats"]);
                }
                else
                {
                    Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Reg Id is null");
                }
            }
            return tr;
        }


    }
}
