using System;
using System.Data;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Net;
using System.IO;
using System.Threading;
using System.Web;

namespace KoDTicketing.Utilities
{
    /// <summary>
    /// Send SMS(Comma Separated)--> http://perfectbulksms.com/Sendsmsapi.aspx?USERID=username&PASSWORD=password&SENDERID=ABC&TO=9999999999,9899999999&MESSAGE=Good Morning
    /// 
    /// Schedule API (Comma Separated):
    ///     http://perfectbulksms.com/Shedulesmsapi.aspx?USERID=username&PASSWORD=password&SENDERID=ABC&TO=9999999999,9899999999&MESSAGE=Good Morning&SHEDULE=201008181225 
    ///     SHEDULE: Shedule date time in yyyyMMddhhmm format(i.e yyyy=year,MM=Month,dd=Day,hh=Hour,mm=Minute)
    /// </summary>
    public class sendSMS
    {
        protected const string SMSServiceURI = "http://luna.a2wi.co.in:7501/failsafe/HttpLink";
        protected const string SMSapiUsername = "514620";
        protected const string SMSapiPassword = "nau@1";

        protected string responseString = "";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mobileNo"></param>
        /// <param name="message"></param>
        /// <param name="senderID"></param>
        /// <returns></returns>
        public string SendSMS_Sender(string mobileNo, string message, string senderID)
        {
            
            string ozUser = HttpUtility.UrlEncode(SMSapiUsername); //username for successful login
            string ozPassw = HttpUtility.UrlEncode(SMSapiPassword); //user's password
            string sender1 = senderID;
            string ozRecipients = HttpUtility.UrlEncode(mobileNo); //who will get the message
            string ozMessageData = System.Web.HttpUtility.UrlPathEncode(message); //body of message
            string createdURL = SMSServiceURI +
            "?aid=" + ozUser +
            "&pin=" + ozPassw +
            "&mnumber=" + ozRecipients +
            "&message=" + ozMessageData;
            try
            {
                HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(createdURL);
                HttpWebResponse myResp = (HttpWebResponse)myReq.GetResponse();
                System.IO.StreamReader respStreamReader = new System.IO.StreamReader(myResp.GetResponseStream());
                responseString = respStreamReader.ReadToEnd();
                respStreamReader.Close();
                myResp.Close();
                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("try sms");
                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("sms:" + responseString);
                return responseString;
            }
            catch (Exception ea)
            {
                responseString = ea.Message.ToString();
                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("sms:" + responseString);
            }
            return responseString;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mobileNo"></param>
        /// <param name="message"></param>
        /// <param name="senderID"></param>
        /// <param name="myDate"></param>
        /// <param name="myTime"></param>
        /// <returns></returns>
        public string SendSMS_Sender(string mobileNo, string message, string senderID, string myDate, string myTime)
        {
              string createdURL = SMSServiceURI +
                                "?USERID=" + SMSapiUsername +
                                "&PASSWORD=" + SMSapiPassword +
                                "&SENDERID=" + senderID +
                                "&TO=" + mobileNo +
                                "&MESSAGE=" + message +
                                "&SHEDULE=" + myDate + "-" + myTime;
            try
            {
                HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(createdURL);
                HttpWebResponse myResp = (HttpWebResponse)myReq.GetResponse();
                System.IO.StreamReader respStreamReader = new System.IO.StreamReader(myResp.GetResponseStream());
                responseString = respStreamReader.ReadToEnd();
                respStreamReader.Close();
                myResp.Close();
            }
            catch (Exception ea)
            {
                responseString = ea.Message.ToString();
            }
            return responseString;
        }

    }
}
