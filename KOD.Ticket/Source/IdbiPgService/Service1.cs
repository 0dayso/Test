using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text.RegularExpressions;
using System.IO;
using System.Timers;
using System.Configuration;
using System.Configuration.Install;
using System.Data.SqlClient;
using System.Net;
using System.Web;
namespace IdbiPgService
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }
        public static string connMSTicket = System.Configuration.ConfigurationManager.ConnectionStrings["MsTicket"].ToString();
        SqlConnection con = new SqlConnection(connMSTicket);
      
        Timer timer = new Timer();
        protected override void OnStart(string[] args)
        {
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Service Started" );
            timer.Enabled = true;
            timer.Interval = 60000;
            timer.Elapsed += new System.Timers.ElapsedEventHandler(OnElapsedTime);
        }

        protected override void OnStop()
        {
            timer.Enabled = false;
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Service Stoped");
        }
        //*************Get 5 top  ID from IDBI payment gateway**************
        public DataTable getValues()
        {
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Get Value Called");
            try
            {
                con.Open();
                string query = "select top 5 a.[ID],a.[BookingID],a.[Source],a.[TotalAmount],b.[Counter] as 'Counter',a.[DateOfBooking] FROM [dbo].[GINC$BookingTransaction_temp] a left join [dbo].[GINC$MarchPromoTransactionCounter] b on a.[ID]=b.[ID] where a.[PaymentGateway]='IDBI' order by a.[id] desc";
                SqlCommand cmd = new SqlCommand(query, con);
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                con.Close();
                return dt;
            }
            catch (SqlException ex)
            {
                
                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Exception in get value: "+ex);
                con.Close();
                return null;
            }
        }
        //*********************************end here

        //*************Insert Response into IDBI_PG_Details**************
        public void insertValues(string id,string booking_id,string transaction_id,string status,string error_code,string error_detail,string amount,string payment_gateway,string date)
        {
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("insertedvalues called");
            try
            {
                con.Open();
                string query = "if not Exists (select [Booking_Id] FROM [dbo].[IDBI_PG_Details] where Id='" + id + "')" +
                "begin INSERT INTO [dbo].[IDBI_PG_Details] ([Id],[Booking_Id],[Transaction_Id],[Satus],[Error_Code],[Error_Detail],[Amount],[Payment_Gateway],[DateOfBooking]) VALUES ('" + id + "','" + booking_id + "','" + transaction_id + "','" + status + "','" + error_code + "','" + error_detail + "','" + amount + "','" + payment_gateway +"','"+date+ "')" +
                "end else begin update [dbo].[IDBI_PG_Details] set [Id]='" + id + "',[Booking_Id]= '" + booking_id + "' ,[Transaction_Id]='" + transaction_id + "',[Satus]='" + status + "',[Error_Code]='" + error_code + "',[Error_Detail]='" + error_detail + "',[Amount]='" + amount + "',[Payment_Gateway]='" + payment_gateway + "',[DateOfBooking]='" + date + "' where [Id]='" + id + "' end";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (SqlException ex)
            {
                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Exception in inserted values : "+ex);
                con.Close();
            }
        }
        //*********************************end here


        private void OnElapsedTime(object source, ElapsedEventArgs e)
        {
             string perform;
             double  amount;
             string merchantReferenceNo;
             string currencyCode;
             string messagehash;
             string passwordHashSha1; 
             int i=0;
             string id;
             string booking_id;
             DataTable dt = new DataTable();
             dt = getValues();
             string date;
            while (i < 5)
            {
                try
                {
                    
                    perform = "getPaymentResult";
                    currencyCode = "356";
                    date=dt.Rows[i][5].ToString();
                    if (dt.Rows[i][4].ToString() == null ||  dt.Rows[i][4].ToString()=="")
                    {
                        merchantReferenceNo = dt.Rows[i][1].ToString() + "_" + dt.Rows[i][0].ToString() + "~" + dt.Rows[i][2];
                        id = dt.Rows[i][0].ToString();
                    }
                    else
                    {
                        merchantReferenceNo = dt.Rows[i][1].ToString() + "_" + dt.Rows[i][4].ToString() + "~" + dt.Rows[i][2];
                        id = dt.Rows[i][4].ToString();
                    }
                    booking_id = dt.Rows[i][1].ToString();
                    amount = (double.Parse(dt.Rows[i][3].ToString())) * 100;
                    messagehash = System.Configuration.ConfigurationManager.AppSettings["pgInstanceId"] + "|" +
                    System.Configuration.ConfigurationManager.AppSettings["merchantId"] + "|" + perform + "|" +
                    currencyCode + "|" + amount + "|" + merchantReferenceNo + "|" + System.Configuration.ConfigurationManager.AppSettings["hashKey"] + "|";
                    passwordHashSha1 = "CURRENCY:7:" + idbiUtility.DoHash(messagehash);

                    string postData = "pg_instance_id=" + System.Configuration.ConfigurationManager.AppSettings["pgInstanceId"];
                    postData += "&merchant_id=" + System.Configuration.ConfigurationManager.AppSettings["merchantId"];
                    postData += "&perform=" + "getPaymentResult";
                    postData += "&amount=" + amount;
                    postData += "&merchant_reference_no=" + merchantReferenceNo;
                    postData += "&currency_code=" + "356";
                    postData += "&message_hash=" + passwordHashSha1;
                    System.IO.StreamWriter requestWriter = null;
                    System.Net.HttpWebRequest objRequest = (System.Net.HttpWebRequest)System.Net.WebRequest.Create("https://" + System.Configuration.ConfigurationManager.AppSettings["pgdomain"] + "/AccosaPG/PGServer");   //create a SSL connection object server-to-server
                    objRequest.Method = "POST";
                    objRequest.ContentLength = postData.Length;
                    objRequest.ContentType = "application/x-www-form-urlencoded";
                    objRequest.CookieContainer = new System.Net.CookieContainer();
                    try
                    {
                        Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Post Data : "+postData);
                        requestWriter = new System.IO.StreamWriter(objRequest.GetRequestStream());	// here the request is sent to payment gateway  
                        requestWriter.Write(postData);
                    }
                    catch (Exception ex)
                    {
                    }
                    if (requestWriter != null)
                        requestWriter.Close();

                    System.Net.HttpWebResponse objResponse = (System.Net.HttpWebResponse)objRequest.GetResponse();
                    using (System.IO.StreamReader sr =
                    new System.IO.StreamReader(objResponse.GetResponseStream()))
                    {
                        String NSDLval = sr.ReadToEnd();
                        Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Completed response before split : " +NSDLval);
                        if (NSDLval != null || NSDLval != "")
                        {
                        string[] response = NSDLval.Split(new char[] {'&'});
                        for (int j = 0; j <=3;j++)
                        {
                            response[j] = response[j].Split('=')[1];
                            if (response[j] == null || response[j] == "")
                            {
                                response[j] = null;
                                if (response[j] == null || response[j] == "")
                                {
                                    response[j] ="NULL";
                                    
                                }
                                
                            }
                            // alter table [MSTicketDB_Live_Latest].[dbo].[IDBI_PG_Details] alter column [Error_Detail] nvarchar(MAX)
                            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Spilitting loop values : " + response[j]);
                        }
                            string respPg;
                            respPg = response[1] + " : " + DetailList(response[1]).ToString() +","+response[2] + " : " + DetailList(response[2]).ToString();
                            insertValues(id, booking_id, response[0], response[1], response[2], respPg, amount.ToString(), "IDBI", date);
                            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("[" + i + "] Respponse : " + id + booking_id + response[0] + response[1] + response[2] + respPg + amount + "IDBI");
                        }
                        else
                        {
                            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("No Response from PG: ");
                        }
                        Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Response : " + NSDLval);
                        sr.Close();
                    }
                }
                catch (Exception ex)
                {
                    Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Eception on elapsedtime :"+ex.ToString());
                }
                i++;
            }
            
        }
//edited agin for detail from manual
        string DetailList(string code)
        {
            switch(code)
            {
                case "50010":return "Init";
                case "50011": return "Capture Aborted";
                case "50012": return "3DS Start";
                case "50013": return "3DS Completed";
                case "50014": return "3DS Failed";
                case "50015": return "3DS Aborted";
                case "50016": return "Switch Start";
                case "50017": return "Switch Timeout";
                case "50018": return "Switch Aborted";
                case "50020": return "Success";
                case "50021": return "Failed";
                case "50097": return "Test Transaction";
                case "0": return "No Error";
                case "1": return "Call Issuer";
                case "2": return "Contact Switch Admin";
                case "3": return "Retry After Some Time.";
                case "10001": return "Disabled Instance";
                case "10002": return "Test Instance";
                case "10003": return "Instance under Maintenance";
                case "10004": return "Internal Server Error";
                case "10005": return "Invalid Data Sent to Switch";
                case "10006": return "Internal Error caused contact Switch Admin";
                case "10011": return "Disabled Acquirer";
                case "10012": return "Test Acquirer";
                case "10013": return "Acquirer under Maintenance";
                case "10021": return "Disabled Merchant";
                case "10022": return "Test Merchant";
                case "10023": return "Merchant under Maintenance";
                case "10024": return "Bad Input Data in Request";
                case "10025": return "PGInterface not allowed";
                case "10026": return "Merchant velocity check failed";
                case "10030": return "Capture Aborted";
                case "10031": return "Auth Aborted";
                case "10032": return "Card Association not enabled";
                case "10033": return "Card Range not enabled";
                case "10040": return "Transaction not allowed - flow error";
                case "12001": return "Acquirer Server Error";
                case "12002": return "Acquirer Timeout";
                case "12003": return "Acquirer Down";
                case "12004": return "Acquirer Declined";
                case "12005": return "Batch Closed";
                case "12006": return "Totals Mismatched";
                case "12007": return "Unable to settle";
                case "13001": return "Issuer Server Error";
                case "13002": return "Issuer Timeout";
                case "13003": return "Issuer Down";
                case "13004": return "Issuer Declined";
                case "13005": return "Invalid Amount";
                case "13006": return "Issuer Insufficient Funds";
                case "14001": return "3DS Failed";
                case "14002": return "3DS Aborted";
                case "14003": return "MPI Error";
                default: return "null";
            }
        }//end
    }

}
