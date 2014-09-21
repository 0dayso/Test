using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;
using System.Net.Mail;
using System.Net;
using System.IO;

namespace RoyalWebApp
{
    public class MailServer
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
        public void sendMail_Net(MailData aMail)
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
            // Set the content
            //Subject of the mail
            oMsg.Subject = aMail.subject;
            //Body of the mail
            oMsg.Body = aMail.bodyMessage;
            //set if the mail is html or not
            oMsg.IsBodyHtml = true;
            //set mail credentials like smtpserver, username and password for authentication
            //SmtpClient oSmtp = new SmtpClient("gcell-in-smtp.mail.lotuslive.com", 465);
            //oSmtp.EnableSsl = true;
            //System.Net.NetworkCredential credentials = new NetworkCredential("partnerships@highstreetlabels.com", "HSLPS@7788");
            //SmtpClient oSmtp = new SmtpClient("smtp.gmail.com", 587);
            SmtpClient oSmtp = new SmtpClient("smtp.gmail.com", 25);
            oSmtp.EnableSsl = true;
            System.Net.NetworkCredential credentials = new NetworkCredential("kingdomofdreams.royalcard@gmail.com", "gcell@123");

            oSmtp.UseDefaultCredentials = false;
            oSmtp.Credentials = credentials;

            try
            {
                oSmtp.Send(oMsg);
            }
            catch (Exception EX)
            { }
            finally { }
        }

        public void SendMailKOD(MailData aMail)
        {
            MailMessage oMsg = new MailMessage();

            oMsg.From = new MailAddress(aMail.from, aMail.fromName);
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
            oMsg.Subject = aMail.subject;
            oMsg.Body = aMail.bodyMessage;
            Attachment at = new Attachment(System.Web.HttpContext.Current.Server.MapPath("~/Skins/images/Royal Card Grid.png"));
            oMsg.Attachments.Add(at);
            oMsg.IsBodyHtml = true;
            SmtpClient oSmtp = new SmtpClient("smtp.qlc.co.in", 25);
            //oSmtp.EnableSsl = true;
            System.Net.NetworkCredential credentials = new NetworkCredential("17230_thegreat_smtp", "t1h2e3g4");

            oSmtp.UseDefaultCredentials = false;
            oSmtp.Credentials = credentials;

            try
            {
                oSmtp.Send(oMsg);
            }
            catch (Exception EX)
            { }
            finally { }
        }

    }

    public class MailData : MailServer
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