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


namespace winservice_payment.reversal.report
{
    public partial class ScheduledService : ServiceBase
    {
        //connection string to connect with data base.
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
            //This statement is used to set interval to 1 minute (= 60,000 milliseconds)
            timer.Interval = Convert.ToDouble(ConfigurationManager.AppSettings["TimeInterval"]);
            timer.Enabled = true; 
        }

        protected override void OnStop()
        {
            timer.Enabled = false;
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Service Stopped : " + DateTime.Now);    
        }

        private void OnElapsedTime(object source, ElapsedEventArgs e)
        {
            DailyReport_Elapsed();
            Detailed_DailyReport_Elapsed();
        }

        void DailyReport_Elapsed()
        {
            timer.Enabled = false;
            DataSet DetailedReport = YesterdayReport();
            ExcelReport.Export(DetailedReport,"Report");
            Mailer(ConfigurationManager.AppSettings["EmailReport"],"Report");
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Daily Payment Reversal Report mail sends at : " + DateTime.Now);
        }
        void Detailed_DailyReport_Elapsed()
        {
            DataSet DetailedReport = YesterdayReport_Detailed();
            ExcelReport.Export(DetailedReport,"Detailed Report");
            Mailer(ConfigurationManager.AppSettings["EmailReport_Detailed"], "Detailed Report");
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Daily Payment Reversal Report mail sends at : " + DateTime.Now);
            timer.Enabled = true;
        }

        //this function is used for mail.
        void Mailer(String FromMail,string Report)
        {
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Mailer for Payment Reversal Report");
            DateTime yesterday = DateTime.Now.AddDays(-1);
            string tracedate = yesterday.ToString("MM/dd/yyyy");
            string[] dateYesterday = tracedate.Split('/');
            string date = dateYesterday[1].ToString();
            string Month = dateYesterday[0].ToString();
            string Year = dateYesterday[2].ToString();
            //date is converted according to the date of sql data.
            string dateDB = Year + "-" + Month + "-" + date;

            Mail objMail = new Mail();
            MailData objmaildata = new MailData();
            objmaildata.from = ConfigurationManager.AppSettings["Emailfrom"];
            objmaildata.fromName = "";
            objmaildata.to = FromMail.ToString();
            objmaildata.toName = "";
            if (Report == "Report")
            objmaildata.subject =dateDB+" "+"Payment Reversal Report";
            if (Report =="Detailed Report")
            objmaildata.subject = dateDB + " " +"Detailed Payment Reversal Report";
            string BodyMaggage = null;
            if (Report == "Report")
            {

                //this the body of mail.
                BodyMaggage = "<div>"
               + "<p>Dear Sir<br/></p>"
               + "<p>Please find the Yesterday's Daily Report of payment reversal.</p>"
               + "<p>Regards</p><p>KOD TEAM</p>"
               + "</div>"
               ;
            }
            if (Report == "Detailed Report")
            {

                //this the body of mail.
                BodyMaggage = "<div>"
               + "<p>Dear Sir<br/></p>"
               + "<p>Please find the Yesterday's Detailed Daily Report of payment reversal.</p>"
               + "<p>Regards</p><p>KOD TEAM</p>"
               + "</div>"
               ;
            }
            
           

            objmaildata.bodyMessage = BodyMaggage;
            if (Report =="Report")
                objMail.sendMail_Net(objmaildata,"Report");
            if (Report =="Detailed Report")
                objMail.sendMail_Net(objmaildata,"Detailed Report");
           
        }
        //this function collect the data from data base.
        public DataSet YesterdayReport()
        {
            try
            {
                con.Open();
                DateTime yesterday = DateTime.Now.Date.AddDays(-1);
                string tracedate = yesterday.ToString("MM/dd/yyyy");
                string[] dateYesterday = tracedate.Split('/');
                string date = dateYesterday[1].ToString();
                string Month = dateYesterday[0].ToString();
                string Year = dateYesterday[2].ToString();
                string dateDB =Year+"-"+Month+"-"+date;
                
                SqlCommand cmd = new SqlCommand("REVERSALREPORT", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter setdate = cmd.Parameters.Add("@SETDATE", SqlDbType.VarChar);
                setdate.Value = dateDB;
                SqlDataAdapter da = new SqlDataAdapter(cmd); 
                DataSet ds = new DataSet();
                da.Fill(ds);
                return ds;
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
        public DataSet YesterdayReport_Detailed()
        {
            try
            {
                con.Open();
                DateTime yesterday = DateTime.Now.Date.AddDays(-1);
                string tracedate = yesterday.ToString("MM/dd/yyyy");
                string[] dateYesterday = tracedate.Split('/');
                string date = dateYesterday[1].ToString();
                string Month = dateYesterday[0].ToString();
                string Year = dateYesterday[2].ToString();
                string dateDB = Year + "-" + Month + "-" + date;

                SqlCommand cmd = new SqlCommand("select ID,BookingID,DateOfBooking,TimeOfBooking,Play,ShowTime,ShowDate,Category,TotalSeats,TotalAmount,DiscountPercentage,Round(TotalAmount*((100-DiscountPercentage)/100),0)as[Reversed amount],PaymentGateway,Name,MobileNo,EmailID,[Address] FROM[GINC$BookingTransaction_temp] where IsProcessed=1 and DateOfBooking=" + "'" + dateDB + "'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                return ds;
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
