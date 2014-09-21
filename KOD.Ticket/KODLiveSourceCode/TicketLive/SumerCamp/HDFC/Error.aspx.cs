using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SumerCamp_HDFC_Error : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            string paymentId, ErrorText, result, postdate, tranid, auth, amt, reference;
            string ErrorNo, udf1, udf2, udf3, udf4, udf5, trackid;

            paymentId = Request["paymentid"] ?? String.Empty;
            ErrorText = Request["ErrorText"] ?? String.Empty;
            ErrorNo = Request["Error"] ?? String.Empty;

            udf1 = Request["udf1"] ?? String.Empty;
            udf2 = Request["udf2"] ?? String.Empty;
            udf3 = Request["udf3"] ?? String.Empty;
            udf4 = Request["udf4"] ?? String.Empty;
            udf5 = Request["udf5"] ?? String.Empty;

            result = Request["result"] ?? String.Empty;
            postdate = Request["postdate"] ?? String.Empty;
            tranid = Request["tranid"] ?? String.Empty;
            auth = Request["auth"] ?? String.Empty;
            trackid = Request["trackid"] ?? String.Empty;
            reference = Request["ref"] ?? String.Empty;
            amt = Request["amt"] ?? String.Empty;

            String responseDetails = string.Format("HDFC Error Page: paymentId[{0}], ErrorText[{1}], result[{2}], postdate[{3}], tranid[{4}], auth[{5}], amt[{6}], reference[{7}], ErrorNo[{8}], udf1[{9}], udf2[{10}], udf3[{11}], udf4[{12}], udf5[{13}], trackid[{14}]",
                                        paymentId, ErrorText, result, postdate, tranid, auth, amt, reference, ErrorNo, udf1, udf2, udf3, udf4, udf5, trackid);

            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write(responseDetails);
            lblError.Text = "HDFC Payment Gateway reported an issue with transaction. Please contact Kingdom Of Dreams for more information at: (0124)4528000";
            lblamt.Text = amt;
            lblpymtid.Text = paymentId;
            lbltrackid.Text = trackid;
        }
        catch (Exception ex)
        {
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write(ex.Message);
        }


    }
}