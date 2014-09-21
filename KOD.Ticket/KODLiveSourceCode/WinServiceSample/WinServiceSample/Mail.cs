using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;
using System.Net.Mail;
using System.Net;
using System.IO;
using System.Text;
using System.Configuration;

namespace RoyalMail
{
    public class Mail
    {
        public String fileName, imageName;
        private bool check_EMail_Address(string emailID)
        {
            Regex emailregex = new Regex("(?<user>[^@]+)@(?<host>.+)");
            Match m = emailregex.Match(emailID);
            if (m.Success)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// sends a mail
        /// </summary>
        /// <param name="aMail"></param>
        public void sendMail_Net(MailData aMail,string LogsOrReport)
        {
            MailMessage oMsg = new MailMessage();
            // Set the message sender
            oMsg.From = new MailAddress(aMail.from, aMail.fromName);
            // The .To property is a generic collection, // so we can add as many recipients as we like.
            oMsg.To.Add(new MailAddress(aMail.to, aMail.toName));
            //if the mail is sent with cc in recipients
            if (aMail.CCto != null)
            {
                if (aMail.CCto.Contains(";"))
                {
                    string[] mailarr = aMail.CCto.Split(';');
                    for (int i = 0; i < mailarr.Length; i++)
                        oMsg.CC.Add(mailarr[i]);
                }
                else
                    oMsg.CC.Add(aMail.CCto);
            }
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Find Attachment for " + LogsOrReport);
            string yesterdaydate = null;
            Char zero = '0';
            string tracedate = DateTime.Now.ToString("MM/dd/yyyy");
            //string[] fulldate = tracedate.Split('-'); //for local 
            string[] fulldate = tracedate.Split('/'); //for live
            string dateYear = fulldate[2];
            string dateMonth = fulldate[0];
            string date1 = fulldate[1];
            DateTime yesterday = DateTime.Now.AddDays(-1);
            int date = yesterday.Day;
            if (date1 == "01" && dateMonth == "01")
            {
                dateMonth = Convert.ToString(yesterday.Month);
                dateMonth = dateMonth.PadLeft(2, zero);
                dateYear = Convert.ToString(yesterday.Year);
            }
            else if (date1 == "01")
            {
                dateMonth = Convert.ToString(yesterday.Month);
                dateMonth = dateMonth.PadLeft(2, zero);
            }
            yesterdaydate = Convert.ToString(date);
            yesterdaydate = yesterdaydate.PadLeft(2, zero);
            if (LogsOrReport == "Logs")
            {
                int a = 1;
                string LogPath = ConfigurationManager.AppSettings["PickLogPath"] + "." + dateYear + "-" + dateMonth + "-" + yesterdaydate + "." + 1 + ".log";
                if (File.Exists(LogPath))
                {
                    Attachment mail = new Attachment(LogPath);
                    while (mail != null)
                    {
                        oMsg.Attachments.Add(mail);
                        a++;
                        LogPath = ConfigurationManager.AppSettings["PickLogPath"] + "." + dateYear + "-" + dateMonth + "-" + yesterdaydate + "." + a + ".log";
                        if (File.Exists(LogPath))
                        {
                            mail = new Attachment(LogPath);
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                else
                {
                    Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("There are no file to attach for " + LogsOrReport);
                }
            }
            else if (LogsOrReport == "Report")
            {
                string ReportPath = ConfigurationManager.AppSettings["DetailedReportPath"] + " " + dateYear + "-" + dateMonth + "-" + yesterdaydate + ".xls";
                if (File.Exists(ReportPath))
                {
                    Attachment mail = new Attachment(ReportPath);
                    oMsg.Attachments.Add(mail);
                }
                else
                {
                    Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("There are no file to attach for " + LogsOrReport);
                }
            }
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Collect all Attachment for " + LogsOrReport);
            // Set the content
            //Subject of the mail
            oMsg.Subject = aMail.subject;
            //Body of the mail
            oMsg.Body = aMail.bodyMessage;
            //set if the mail is html or not
            oMsg.IsBodyHtml = true;

            SmtpClient oSmtp = new SmtpClient("smtp.gmail.com", 25);
            oSmtp.EnableSsl = true;
            System.Net.NetworkCredential credentials = new NetworkCredential("noreplykod@gmail.com", "k1ngd0m2012");

            oSmtp.UseDefaultCredentials = false;
            oSmtp.Credentials = credentials;
            oSmtp.Timeout = 5000000;
            try
            {
                oSmtp.Send(oMsg);
                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Mail sent for " + LogsOrReport);
            }
            catch (Exception ex)
            {
                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Exception while sending a mail " + ex);
            }
            finally { }
        }
    }

    public class MailData : Mail
    {
        public string to { set; get; }
        public string toName { set; get; }
        public string subject { set; get; }
        public string bodyMessage { set; get; }
        public string from { set; get; }
        public string fromName { set; get; }
        public string smtpServer { set; get; }
        public string fromUID { set; get; }
        public string fromPwd { set; get; }
        public string CCto { get; set; }
        
    }
}
