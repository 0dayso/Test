using System;
using System.Collections.Generic;
using System.Web;
using System.Text.RegularExpressions;
using System.IO;
using System.Net;

namespace RoyalWebApp
{
    public class SmsServer
    {
          protected string apiUsername = "gcell";
        protected string apiPassword = "cell321";
        protected string responseString = "";

        public string SendSMS_Sender(string mobileNo, string message, string senderID)
        {
            string ozSURL = "http://perfectbulksms.com";
            string ozUser = HttpUtility.UrlEncode(apiUsername); //username for successful login
            string ozPassw = HttpUtility.UrlEncode(apiPassword); //user's password
            string sender1 = senderID;
            string ozRecipients = HttpUtility.UrlEncode(mobileNo); //who will get the message
            string ozMessageData = HttpUtility.UrlEncode(message); //body of message
            string createdURL = ozSURL + "/Sendsmsapi.aspx" +
            "?USERID=" + ozUser +
            "&PASSWORD=" + ozPassw +
            "&SENDERID=" + sender1 +
            "&TO=" + ozRecipients +
            "&MESSAGE=" + ozMessageData;
            try
            {
                HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(createdURL);
                HttpWebResponse myResp = (HttpWebResponse)myReq.GetResponse();
                System.IO.StreamReader respStreamReader = new System.IO.StreamReader(myResp.GetResponseStream());
                responseString = respStreamReader.ReadToEnd();
                respStreamReader.Close();
                myResp.Close();
                return responseString;
            }
            catch (Exception ea)
            {
                responseString = ea.Message.ToString();
            }
            return responseString;
        }

        public string SendSMS_Sender(string mobileNo, string message, string senderID, string myDate, string myTime)
        {
            string ozSURL = "http://perfectbulksms.com";
            string ozUser = HttpUtility.UrlEncode(apiUsername); //username for successful login
            string ozPassw = HttpUtility.UrlEncode(apiPassword); //user's password
            string sender1 = senderID;
            string ozRecipients = HttpUtility.UrlEncode(mobileNo); //who will get the message
            string ozMessageData = HttpUtility.UrlEncode(message); //body of message
            string createdURL = ozSURL + "/Sendsmsapi.asp" +
        "?USERID=" + ozUser +
        "&PASSWORD=" + ozPassw +
        "&SENDERID=" + sender1 +
        "&TO=" + ozRecipients +
        "&MESSAGE=" + ozMessageData +
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