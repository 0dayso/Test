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

namespace winservice_payment.reversal.report
{
    public class Mail
    {
        string ReportPath;
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
        //sends a mail
        public void sendMail_Net(MailData aMail,string Report)
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
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Find Attachment for Report ");
            DateTime yesterday = DateTime.Now.AddDays(-1);
            string tracedate = yesterday.ToString("MM/dd/yyyy");
            string[] dateYesterday = tracedate.Split('/');
            string date = dateYesterday[1].ToString();
            string Month = dateYesterday[0].ToString();
            string Year = dateYesterday[2].ToString();
            string dateDB = Year + "-" + Month + "-" + date;
            
            if(Report=="Report")
            ReportPath = ConfigurationManager.AppSettings["DetailedReportPath"]+dateDB+".xls";
            if (Report == "Detailed Report")
            ReportPath = ConfigurationManager.AppSettings["DetailedReportPath_Detailed"] + dateDB + ".xls";
               //collect the attachment for the mail. 
                if (File.Exists(ReportPath))
                {
                    Attachment mail = new Attachment(ReportPath);
                    oMsg.Attachments.Add(mail);    
                }
                else
                {
                    Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("There are no file to attach for Report ");
                }

                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Collect all Attachment for Report ");
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
            oSmtp.Timeout = 50000000;
            
            try
            {
                oSmtp.Send(oMsg);
                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Mail sent for Payment Reversal Report ");
            }
            catch (Exception ex)
            {
                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Exception while sending a mail " + ex);
            }
            finally { }
            //release the file for next process.
            oMsg.Attachments.Dispose();
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
