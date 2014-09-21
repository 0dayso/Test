using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RoyalWebApp.Payment.Idbi
{
    public partial class PostRequest : System.Web.UI.Page
    {
        public string perform;
        public string amount;
        public string merchantReferenceNo;
        public string orderDesc;
        public string currencyCode;
        public string messagehash;
        public string passwordHashSha1;

        protected void Page_Load(object sender, System.EventArgs e)
        {
            perform = Request.Form["perform"];
            amount = Request.Form["amount"];
            merchantReferenceNo = Request.Form["merchant_reference_no"];
            currencyCode = Request.Form["currency_code"];
            orderDesc = Request.Form["order_desc"];
            messagehash = System.Configuration.ConfigurationManager.AppSettings["pgInstanceId"] + "|" +
                System.Configuration.ConfigurationManager.AppSettings["merchantId"] + "|" + perform + "|" +
                currencyCode + "|" + amount + "|" + merchantReferenceNo + "|" + System.Configuration.ConfigurationManager.AppSettings["hashKey"] + "|";
            passwordHashSha1 = "CURRENCY:7:" + enUtility.DoHash(messagehash);
        }
    }
}