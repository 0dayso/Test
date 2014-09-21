using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.IO;
using System.Timers;
using System.Net.Mail;
using System.Configuration;
using System.Configuration.Install;
using System.Data.SqlClient;
using System.Web;

namespace WinServiceSample
{
    public partial class ScheduledService : ServiceBase
    {
        public static string connMSTicket = System.Configuration.ConfigurationManager.ConnectionStrings["MsTicket"].ToString();
        SqlConnection con = new SqlConnection(connMSTicket);
        Timer timer = new Timer();
        public ScheduledService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            //handle Elapsed event
            timer.Elapsed += new ElapsedEventHandler(OnElapsedTime);
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Service Started : " + DateTime.Now);
            TraceService("Service Started : " + DateTime.Now);
            //This statement is used to set interval to 1 minute (= 60,000 milliseconds)
            timer.Interval = Convert.ToDouble(ConfigurationManager.AppSettings["TimeInterval"]);
            timer.Enabled = true;
        }

        protected override void OnStop()
        {
            timer.Enabled = false;
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Service Stopped : " + DateTime.Now);
            TraceService("Service Stopped : " + DateTime.Now);
        }

        private void OnElapsedTime(object source, ElapsedEventArgs e)
        {
            Mailer(ConfigurationManager.AppSettings["Emailto1"], "Logs");
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Logs Report mail sends at : " + DateTime.Now);
            TraceService("Logs Report mail sends at : " + DateTime.Now);
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("For Report");
            DailyReport_Elapsed();
        }

        void DailyReport_Elapsed()
        {
            DataSet DetailedReport = YesterdayReport();
            ExportToExcel.Export(DetailedReport);
            Mailer(ConfigurationManager.AppSettings["EmailReport"], "Report");
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Daily Report mail sends at : " + DateTime.Now);
            TraceService("Daily Report mail sends at : " + DateTime.Now);
        }
       
        public static void TraceService(string content)
        {

            //set up a filestream
            FileStream fs = new FileStream(@ConfigurationManager.AppSettings["LogPath"], FileMode.OpenOrCreate, FileAccess.Write);

            //set up a streamwriter for adding text
            StreamWriter sw = new StreamWriter(fs);

            //find the end of the underlying filestream
            sw.BaseStream.Seek(0, SeekOrigin.End);

            //add the text
            sw.WriteLine(content);
            //add the text to the underlying filestream

            sw.Flush();
            //close the writer
            sw.Close();
        }
        void Mailer(String FromMail, string ReportorLogs)
        {
            //timer.Enabled = false;
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Mailer for " + ReportorLogs);
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

            RoyalMail.Mail objMail = new RoyalMail.Mail();
            RoyalMail.MailData objmaildata = new RoyalMail.MailData();
            objmaildata.from = ConfigurationManager.AppSettings["Emailfrom"];
            objmaildata.fromName = "";
            objmaildata.to = FromMail.ToString();
            objmaildata.toName = "";
            objmaildata.subject = dateYear + "-" + dateMonth + "-" + yesterdaydate + " " + ReportorLogs;
            string BodyMaggage = null;
            if (ReportorLogs == "Report")
            {
                BodyMaggage = "<div>"
               + "<p>Dear Sir<br/></p>"
               + "<p>Please find the Yesterday's Daily Report of KOD Shows Booked through Web</p>"
               + "<p>Regards</p><p>KOD TEAM</p>"
               + "</div>"
               ;
            }
            else if (ReportorLogs == "Logs")
            {
                BodyMaggage = "<div>"
               + "<p>Dear Sir<br/></p>"
               + "<p>Please find the Yesterday's Logs of KOD Shows Booked through Web</p>"
               + "<p>Regards</p><p>KOD TEAM</p>"
               + "</div>"
               ;
            }

            objmaildata.bodyMessage = BodyMaggage;
            objMail.sendMail_Net(objmaildata, ReportorLogs);
        }
        public DataSet YesterdayReport()
        {
            try
            {
                //Connection.LogEntry(regid, transactionAmt.ToString(), Date.ToString(), ReceiptNo, TransType.ToString());
                con.Open();
                SqlCommand cmd = new SqlCommand("DetailedReport", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter startDate = cmd.Parameters.Add("@startDOB", SqlDbType.DateTime);
                startDate.Value = DateTime.Now.Date.AddDays(-1);
                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Find Report for Start Date : " + startDate.Value);

                SqlParameter EndDate = cmd.Parameters.Add("@endDOB", SqlDbType.DateTime);
                EndDate.Value = DateTime.Now.Date.AddDays(-1);
                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Find Report for End Date : " + EndDate.Value);

                SqlParameter Agent = cmd.Parameters.Add("@agent", SqlDbType.NVarChar);
                Agent.Value = "All";

                SqlParameter play = cmd.Parameters.Add("@Ply", SqlDbType.NVarChar);
                play.Value = "All";

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet dataset = new DataSet();
                da.Fill(dataset);
                return dataset;
            }
            catch (SqlException ex)
            {
                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("SqlException : " + ex);
                throw ex;
            }
            catch (Exception ex)
            {
                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Exception in Report SP : " + ex);
                throw ex;
            }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Close();
                }
            }
        }
    }
}
