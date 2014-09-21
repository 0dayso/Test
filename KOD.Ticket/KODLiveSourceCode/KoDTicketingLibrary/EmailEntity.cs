using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.ComponentModel;
using System.Web;
using System.Configuration;
using System.IO;
using System.Collections;
using System.Collections.Specialized;
using System.Net.Mime;

/*
 * 
 *  <add key="TemplateFolderPath" value="~\Template\"/>
    <!--smtp section-->
    <add key="SmtpServer" value="smtp.gmail.com"/>
    <add key="SmtpServerUserName" value="highstreetlabels.webmaster@gmail.com"/>
    <add key="SmtpServerPassword" value="h7777777"/>
    <add key="SmtpPortnumber" value="587"/>
    <add key="EmailEncoding" value="utf-8"/>
    <add key="MailTo" value="highstreetlabels.webmaster@gmail.com"/>
    <add key="InternalInvoiceMailId" value="neeraj.verma@gcell.in"/>
    <!--<add key="MailTo" value="QMSupport@VESTusa.com"/>-->
    <!--<add key="MailTo" value="sankalpgoel@gmail.com"/>-->
    <!--smtp section end-->
 * */


namespace KoDTicketing.Utilities
{
    [Serializable]
    public class EmailEntity
    {
        public IList<string> EmailTo { get; set; }
        public string EmailFrom { get; set; }
        public string EmailSubject { get; set; }
        public string EmailBody { get; set; }
        public string EmailCC { get; set; }
        public string EmailBCC { get; set; }
        public Boolean IsHtml { get; set; }
        public Stream EmailAttachement { get; set; }
        public string AttachedfileName { get; set; }
        public AlternateView AlternateView { get; set; }
    }

    [Serializable]
    public class Template
    {
        private string _TemplateName;

        public string TemplateName
        {
            get { return _TemplateName; }
            set { _TemplateName = value; }
        }

        private string _TemplateBody;

        public string TemplateBody
        {
            get { return _TemplateBody; }
            set { _TemplateBody = value; }
        }


    }


    public static class SMTPEmailDispatch
    {

        private static void SendCompletedCallback(object sender, AsyncCompletedEventArgs e)
        {
            // Get the unique identifier for this asynchronous operation.
            String token = (string)e.UserState;

            if (e.Cancelled)
            {
                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write(string.Format("[{0}] Send canceled.", token));
            }
            if (e.Error != null)
            {
                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write(string.Format("[{0}] {1}", token, e.Error.ToString()));
            }
            else
            {
                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write(string.Format("Message sent. [{0}]", token));
            }
        }

        public static bool Send(EmailEntity em)
        {
            SmtpSetting site = GetSmtpSetting();
            if (site == null)
                return false;

            short connectionLimit = site.SmtpServerConnectionLimit;

            SmtpClient client = new SmtpClient();
            System.Net.NetworkCredential credential = new System.Net.NetworkCredential();
            credential.Password = site.SmtpServerPassword;
            credential.UserName = site.SmtpServerUserName;
            //client.Port = Convert.ToInt32(ConfigurationManager.AppSettings.Get("SmtpPortnumber"));
            client.Port = int.Parse(site.SmtpPortNumber);
            client.Host = site.SmtpServer;
            client.Credentials = credential;
            client.EnableSsl = true;

            try
            {
                MailMessage message = new MailMessage();
                message.From = new MailAddress(em.EmailFrom);
                foreach (string address in em.EmailTo)
                {
                    message.To.Add(new MailAddress(address));
                }
                message.Subject = em.EmailSubject;
                //
                if (em.AlternateView != null)
                {
                    message.AlternateViews.Add(em.AlternateView);
                }
                else
                {
                    message.Body = em.EmailBody;
                }
                //message.IsBodyHtml = em.IsHtml;
                message.IsBodyHtml = true;
                if ((em.EmailAttachement != null) && (!string.IsNullOrEmpty(em.AttachedfileName)))
                {
                    message.Attachments.Add(new Attachment(em.EmailAttachement, em.AttachedfileName));
                }
                // Set the body encoding
                try { message.BodyEncoding = Encoding.GetEncoding(site.EmailEncoding); }
                catch { message.BodyEncoding = Encoding.UTF8; }
                // Set the method that is called back when the send operation ends.
                //client.SendCompleted += new SendCompletedEventHandler(SendCompletedCallback);
                //string userState = "KoD Ticket Booking Email Notification";
                //client.SendAsync(message, userState);
                client.Send(message);

                //Console.WriteLine("Sending message... press c to cancel mail. Press any other key to exit.");
                //string answer = Console.ReadLine();
                //// If the user canceled the send, and mail hasn't been sent yet,
                //// then cancel the pending operation.
                //if (answer.StartsWith("c"))
                //{
                //    client.SendAsyncCancel();
                //}
                // Clean up.
                message.Dispose();
                Console.WriteLine("Goodbye.");

            }
            catch (System.Exception e)
            {
                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write(e.Message);
                throw e;
            }

            return true;
        }

        private static SmtpSetting GetSmtpSetting()
        {
            try
            {
                SmtpSetting setting = new SmtpSetting();
                //setting.SmtpServer = ConfigurationManager.AppSettings.Get("SmtpServer").ToString();
                //setting.SmtpServerUserName = ConfigurationManager.AppSettings.Get("SmtpServerUserName").ToString();
                //setting.SmtpServerPassword = ConfigurationManager.AppSettings.Get("SmtpServerPassword").ToString();
                //setting.SmtpPortNumber = ConfigurationManager.AppSettings.Get("SmtpPortNumber").ToString();
                //setting.EmailEncoding = ConfigurationManager.AppSettings.Get("EmailEncoding").ToString();

                //const string mailSenderAccount = "sales@kingdomofdreams.co.in";
                //const string mailSenderAccountName = "Kingdom of Dreams";
                //const string mailEmailAccountID = "comcenter@kingdomofdreams.co.in";
                //const string mailEmailAccountSMTPServer = "gcell-in-smtp.mail.lotuslive.com:465";
                //const string mailEmailAccountPassword = "KingdomC@2011";

                setting.SmtpServer = "smtp.gmail.com";
                setting.SmtpServerUserName = "noreplykod@gmail.com";
                setting.SmtpServerPassword = "k1ngd0m2012";
                setting.SmtpPortNumber = "587";
                setting.EmailEncoding = "utf-8"; 

                //setting.SmtpServer = "gcell-in-smtp.mail.lotuslive.com";
                //setting.SmtpServerUserName = "comcenter@kingdomofdreams.co.in";
                //setting.SmtpServerPassword = "KingdomC@2011";
                //setting.SmtpPortNumber = "465";
                //setting.EmailEncoding = "utf-8";
                return setting;
            }
            catch (System.Exception ex)
            {
                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write(ex.Message);
                return null;
            }
        }

        private class SmtpSetting
        {
            public string SmtpServer = string.Empty;
            public string SmtpServerUserName = string.Empty;
            public string SmtpServerPassword = string.Empty;
            public string SmtpPortNumber = string.Empty;
            public short SmtpServerConnectionLimit = 5;
            public int WaitSecondsWhenConnectionLimitExceeds = 15;
            public bool SmtpServerUsingNtlm = false;
            public bool SmtpServerRequiredLogin = false;
            public string EmailEncoding = "utf - 8";
        }

    }

    public static class FileSystemEmailTemplate
    {
        private static Template GetTemplate(string templateName)
        {
            //Get template from filesystem and return a Template Object
            try
            {
                string pathPlaceHolderInTemplate = "#Path#";
                string dayandMonthPlaceHolderInTemplate = "#DAYANDMONTH#";
                string yearPlaceHolderInTemplate = "#YEAR#";




                string path = HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings.Get("TemplateFolderPath").ToString());
                string fileContents;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(string.Format(@"{0}{1}.txt", path, templateName)))
                {
                    fileContents = sr.ReadToEnd();
                }
                if (!string.IsNullOrWhiteSpace(fileContents))
                {
                    fileContents = fileContents.Replace(pathPlaceHolderInTemplate, path);
                    fileContents = fileContents.Replace(dayandMonthPlaceHolderInTemplate, DateTime.Now.ToString("dd MMM"));
                    fileContents = fileContents.Replace(yearPlaceHolderInTemplate, DateTime.Now.Year.ToString());
                }

                Template template = new Template();
                template.TemplateName = templateName;
                template.TemplateBody = fileContents;
                return template;
            }
            catch (System.Exception e)
            {
                throw e;
            }
        }

        public static EmailEntity GetTemplatedMessage(string templateName, EmailEntity message, StringDictionary namevalue)
        {
            string emailAddressPlaceHolder = "#EmailAddress#";
            Template template = GetTemplate(templateName);
            if (message != null)
            {
                if (template != null)
                {
                    if (namevalue != null)
                    {
                        foreach (DictionaryEntry de in namevalue)
                        {
                            template.TemplateBody = template.TemplateBody.Replace(de.Key.ToString(), de.Value.ToString());
                        }
                        message.EmailBody = template.TemplateBody;
                        message.EmailBody = message.EmailBody.Replace(emailAddressPlaceHolder, message.EmailTo[0]);
                    }
                }

                AlternateView mailView = null;

                if (templateName.Equals("verify-account"))
                {
                    mailView = GetAlternateViewForVerfication(message);
                }
                else if (templateName.Equals("welcome"))
                {
                    mailView = GetAlternateViewForWelcome(message);
                }

                message.AlternateView = mailView;
            }
            return message;
        }

        private static AlternateView GetAlternateViewForWelcome(EmailEntity message)
        {
            string path = HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings.Get("TemplateFolderPath").ToString());

            string emptygifPath = "images/empty.gif";
            string topjpgPath = "images/top.jpg";
            string logogifPath = "images/logo.gif";
            //string cheersjpgPath = "images/cheers.jpg";
            //string welcomebtnjpgPath = "images/welcomebtm.jpg";
            string logobottomjpgPath = "images/logo_bottom.jpg";
            string bottomjpgPath = "images/bottom.jpg";

            string emailjpgPath = "images/email.jpg";
            string welcomejpgPath = "images/welcome.jpg";
            string shadowjpgPath = "images/shadow.jpg";
            string gplusjpgPath = "images/gplus.jpg";
            string icofacebookjpgPath = "images/ico_facebook.jpg";
            string icotwitterjpgPath = "images/ico_twitter.jpg";


            string emptygifContentId = "emptygif";
            string topjpgContentId = "topjpg";
            string logogifContentId = "logogif";
            //string cheersjpgContentId = "cheersjpg";
            //string welcomebtnjpgContentId = "welcomebtnjpg";
            string logobottomjpgContentId = "logobottomjpg";
            string bottomjpgContentId = "bottomjpg";

            string emailjpgContentId = "emailjpg";
            string welcomejpgContentId = "welcomejpg";
            string shadowjpgContentId = "shadowjpg";
            string gplusjpgContentId = "gplusjpg";
            string icofacebookjpgContentId = "icofacebookjpg";
            string icotwitterjpgContentId = "icotwitterjpg";

            LinkedResource emptygifResource = new LinkedResource(path + emptygifPath);
            emptygifResource.ContentId = emptygifContentId;

            LinkedResource topjpgResource = new LinkedResource(path + topjpgPath);
            topjpgResource.ContentId = topjpgContentId;

            LinkedResource logogifResource = new LinkedResource(path + logogifPath);
            logogifResource.ContentId = logogifContentId;

            LinkedResource emailjpgResource = new LinkedResource(path + emailjpgPath);
            emailjpgResource.ContentId = emailjpgContentId;

            LinkedResource welcomejpgResource = new LinkedResource(path + welcomejpgPath);
            welcomejpgResource.ContentId = welcomejpgContentId;

            LinkedResource logobottomjpgResource = new LinkedResource(path + logobottomjpgPath);
            logobottomjpgResource.ContentId = logobottomjpgContentId;

            LinkedResource bottomjpgResource = new LinkedResource(path + bottomjpgPath);
            bottomjpgResource.ContentId = bottomjpgContentId;

            LinkedResource shadowjpgResource = new LinkedResource(path + shadowjpgPath);
            shadowjpgResource.ContentId = shadowjpgContentId;

            LinkedResource gplusjpgResource = new LinkedResource(path + gplusjpgPath);
            gplusjpgResource.ContentId = gplusjpgContentId;

            LinkedResource icofacebookjpgResource = new LinkedResource(path + icofacebookjpgPath);
            icofacebookjpgResource.ContentId = icofacebookjpgContentId;

            LinkedResource icotwitterjpgResource = new LinkedResource(path + icotwitterjpgPath);
            icotwitterjpgResource.ContentId = icotwitterjpgContentId;


            AlternateView mailView = AlternateView.CreateAlternateViewFromString(message.EmailBody, null, MediaTypeNames.Text.Html);
            mailView.LinkedResources.Add(emptygifResource);
            mailView.LinkedResources.Add(topjpgResource);
            mailView.LinkedResources.Add(logogifResource);
            mailView.LinkedResources.Add(emailjpgResource);
            mailView.LinkedResources.Add(welcomejpgResource);
            mailView.LinkedResources.Add(logobottomjpgResource);
            mailView.LinkedResources.Add(bottomjpgResource);

            mailView.LinkedResources.Add(shadowjpgResource);
            mailView.LinkedResources.Add(gplusjpgResource);
            mailView.LinkedResources.Add(icofacebookjpgResource);
            mailView.LinkedResources.Add(icotwitterjpgResource);

            return mailView;
        }

        private static AlternateView GetAlternateViewForVerfication(EmailEntity message)
        {
            string path = HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings.Get("TemplateFolderPath").ToString());

            string emptygifPath = "images/empty.gif";
            string topjpgPath = "images/top.jpg";
            string logogifPath = "images/logo.gif";
            string cheersjpgPath = "images/cheers.jpg";
            string welcomebtnjpgPath = "images/welcomebtm.jpg";
            string logobottomjpgPath = "images/logo_bottom.jpg";
            string bottomjpgPath = "images/bottom.jpg";

            string emptygifContentId = "emptygif";
            string topjpgContentId = "topjpg";
            string logogifContentId = "logogif";
            string cheersjpgContentId = "cheersjpg";
            string welcomebtnjpgContentId = "welcomebtnjpg";
            string logobottomjpgContentId = "logobottomjpg";
            string bottomjpgContentId = "bottomjpg";

            LinkedResource emptygifResource = new LinkedResource(path + emptygifPath);
            emptygifResource.ContentId = emptygifContentId;

            LinkedResource topjpgResource = new LinkedResource(path + topjpgPath);
            topjpgResource.ContentId = topjpgContentId;

            LinkedResource logogifResource = new LinkedResource(path + logogifPath);
            logogifResource.ContentId = logogifContentId;

            LinkedResource cheersjpgResource = new LinkedResource(path + cheersjpgPath);
            cheersjpgResource.ContentId = cheersjpgContentId;

            LinkedResource welcomebtnjpgResource = new LinkedResource(path + welcomebtnjpgPath);
            welcomebtnjpgResource.ContentId = welcomebtnjpgContentId;

            LinkedResource logobottomjpgResource = new LinkedResource(path + logobottomjpgPath);
            logobottomjpgResource.ContentId = logobottomjpgContentId;

            LinkedResource bottomjpgResource = new LinkedResource(path + bottomjpgPath);
            bottomjpgResource.ContentId = bottomjpgContentId;

            AlternateView mailView = AlternateView.CreateAlternateViewFromString(message.EmailBody, null, MediaTypeNames.Text.Html);
            mailView.LinkedResources.Add(emptygifResource);
            mailView.LinkedResources.Add(topjpgResource);
            mailView.LinkedResources.Add(logogifResource);
            mailView.LinkedResources.Add(cheersjpgResource);
            mailView.LinkedResources.Add(welcomebtnjpgResource);
            mailView.LinkedResources.Add(logobottomjpgResource);
            mailView.LinkedResources.Add(bottomjpgResource);
            return mailView;
        }

    }
}
