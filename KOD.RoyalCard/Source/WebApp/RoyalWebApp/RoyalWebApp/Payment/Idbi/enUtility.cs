using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Security.Cryptography;

namespace RoyalWebApp.Payment.Idbi
{
    public class enUtility
    {

        //web.config section 

        //public string pgInstanceId = "94734111";
        //public string merchantId = "98893642";
        //public string hashKey = "58AAD7F4BC0DB3CF";
        //public string pgdomain = "https://testipg.eazy2pay.com";
        //public string requestperformSale = "initiatePaymentCapture#sale";
        //public string requestperformPreAuth = "initiatePaymentCapture#preauth";
        //public string requestcurrencyCode = "356"; 
        public string pgInstanceId = "61382171";
        public string merchantId = "89750570";
        public string hashKey = "A0B6CE54F68E06A4";
        public string pgdomain = "https://pg.eazy2pay.com";
        public string requestperformSale = "initiatePaymentCapture#sale";
        public string requestperformPreAuth = "initiatePaymentCapture#preauth";
        public string requestcurrencyCode = "356";

        public static string DoHash(string text)
        {
            string myString = text;
            byte[] Data = null;
            Data = Encoding.ASCII.GetBytes(myString);
            SHA1Managed shaM = new SHA1Managed();
            byte[] resultHash = shaM.ComputeHash(Data);
            string resultHexString = "";
            byte b = 0;
            foreach (byte b_loopVariable in resultHash)
            {
                b = b_loopVariable;
                //resultHexString += Conversion.Hex(b);
                resultHexString += b.ToString("X");
            }
            string hashValue = null;
            hashValue = Convert.ToBase64String(resultHash);
            return hashValue;
        }


    }
}