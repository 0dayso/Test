using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using System.Configuration;


namespace winservice_payment.reversal.report
{
    public class ExcelReport
    {
        string ReportPath;
         public static void Export(DataSet Report,string Diffrence)
        {
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Enter to copy the data on excel");
            DateTime yesterday = DateTime.Now.AddDays(-1);
            string tracedate = yesterday.ToString("MM/dd/yyyy");
            string[] dateYesterday = tracedate.Split('/');
            string date = dateYesterday[1].ToString();
            string Month = dateYesterday[0].ToString();
            string Year = dateYesterday[2].ToString();
            string dateDB = Year + "-" + Month + "-" + date;
            ExcelReport e = new ExcelReport();
             if(Diffrence=="Report")
             e.ReportPath = "Payment Reversal Report"+dateDB;
             if (Diffrence == "Detailed Report")
             e.ReportPath = "Detailed Payment Reversal Report"+ dateDB;
            FileStream fs = new FileStream(@"C:/"+e.ReportPath+".xls",FileMode.Create,FileAccess.Write);
            //set up a streamwriter for adding text
           StreamWriter wr = new StreamWriter(fs);
           
          
            try
            {
                int tablecounter;
                int ColumnsNamecounter;
                wr.WriteLine(date + "-" + Month + "-" + Year);
                for (tablecounter = 0; tablecounter < Report.Tables.Count; tablecounter++)
                {
                    //write the column name in file.
                    if (Report.Tables[tablecounter].Rows.Count != 0)
                    {
                        for (ColumnsNamecounter = 0; ColumnsNamecounter < Report.Tables[tablecounter].Columns.Count; ColumnsNamecounter++)
                        {
                            wr.Write(Report.Tables[tablecounter].Columns[ColumnsNamecounter].ToString().ToUpper() + "\t");
                        }
                    }
                    else 
                    {
                        wr.Write("There is no any transection for which payment is reversed.");
                    }


                    wr.WriteLine();
                    int ColumnsNocounter;
                    int RowNoCounter;
                    //write the data of data base in to the file.
                    for (RowNoCounter = 0; RowNoCounter < (Report.Tables[tablecounter].Rows.Count); RowNoCounter++)
                    {
                        for (ColumnsNocounter = 0; ColumnsNocounter < Report.Tables[tablecounter].Columns.Count; ColumnsNocounter++)
                        {
                            if (Report.Tables[tablecounter].Rows[RowNoCounter][ColumnsNocounter].ToString() == "")
                            {
                                wr.Write("0" + "\t");
                            }
                            else if (Report.Tables[tablecounter].Rows[RowNoCounter][ColumnsNocounter] != null)
                            {

                                wr.Write(Report.Tables[tablecounter].Rows[RowNoCounter][ColumnsNocounter] + "\t");
                            }

                            else
                            {
                                wr.Write("\t");
                            }
                        }
                        //go to next line
                        wr.WriteLine();
                    }
                    wr.WriteLine();
                }
                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Copy all data to excel");
                //close file
               wr.Flush();
                wr.Close();
                
                //close the writer
               fs.Close();
            }
            catch (Exception ex)
            {
                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Exception while writting a data to excel : " + ex);
                throw ex;
            }
        }
    }
}

