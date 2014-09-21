using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using System.Configuration;

namespace WinServiceSample
{
    public class ExportToExcel
    {
        public static void Export(DataSet Report)
        {
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Enter to copyright the data on excel");
            int l = 0;
            l = Report.Tables.Count - 1;
            DataTable ReportTable = Report.Tables[l];
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
            string ReportPath = "Accounts Report " + dateYear + "-" + dateMonth + "-" + yesterdaydate;
            StreamWriter wr = new StreamWriter(@"C:/"+ReportPath+".xls");
            try
            {
                for (int i = 0; i < ReportTable.Columns.Count; i++)
                {
                    wr.Write(ReportTable.Columns[i].ToString().ToUpper() + "\t");
                }

                wr.WriteLine();
                //DataRow dr;
                //wr.Write(Report.Columns.Add("Date", typeof(string)) + "\t");
                //wr.Write(Report.Columns.Add("Bronze", typeof(string)) + "\t");
                //wr.Write(Report.Columns.Add("Copper", typeof(string)) + "\t");
                //wr.Write(Report.Columns.Add("Diamond", typeof(string)) + "\t");
                //wr.Write(Report.Columns.Add("Gold", typeof(string)) + "\t");
                //wr.Write(Report.Columns.Add("Platinum", typeof(string)) + "\t");
                //wr.Write(Report.Columns.Add("Silver", typeof(string)) + "\t");
                //wr.Write(Report.Columns.Add("Total Seats", typeof(string)) + "\t");
                //wr.Write(Report.Columns.Add("HDFC", typeof(string)) + "\t");
                //wr.Write(Report.Columns.Add("IDBI", typeof(string)) + "\t");
                //wr.Write(Report.Columns.Add("AMEX", typeof(string)) + "\t");
                //wr.Write(Report.Columns.Add("Total Amount", typeof(string)) + "\t");
                //wr.Write(Report.Columns.Add("Play", typeof(string)) + "\t");
                //dr = Report.NewRow();
                //wr.WriteLine();

                //write rows to excel file
                for (int i = 0; i < (ReportTable.Rows.Count); i++)
                {
                    for (int j = 0; j < ReportTable.Columns.Count; j++)
                    {
                        if (ReportTable.Rows[i][j].ToString() == "")
                        {
                            wr.Write("0" + "\t");
                        }
                        else if (ReportTable.Rows[i][j] != null)
                        {
                            wr.Write(ReportTable.Rows[i][j] + "\t");
                        }

                        else
                        {
                            wr.Write("\t");
                        }
                    }
                    //go to next line
                    wr.WriteLine();
                }
                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Copy all data to excel");
                //close file
                wr.Close();
            }
            catch (Exception ex)
            {
                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Exception while writting a data to excel : " + ex);
                throw ex;
            }
        }
    }
}
