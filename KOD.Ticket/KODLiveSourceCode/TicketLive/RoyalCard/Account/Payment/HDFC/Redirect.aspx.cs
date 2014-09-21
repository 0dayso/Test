using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Payment_HDFC_Redirect : System.Web.UI.Page
{

    string paymentId, ErrorText, result, postdate, tranid, auth, amt, refence;
    string ErrorNo, udf1, udf2, udf3, udf4, udf5, trackid;
    protected void Page_Load(object sender, EventArgs e)
    {
        ErrorText = Request["ErrorText"];
        paymentId = Request["paymentid"];
        ErrorNo = Request["Error"];
        udf1 = Request["udf1"];
        udf2 = Request["udf2"];
        udf3 = Request["udf3"];
        udf4 = Request["udf4"];
        udf5 = Request["udf5"];

        Writefile_new("\n***************Redirect********************", Server.MapPath("~"));
        Writefile_new("\n\nDateTime:" + DateTime.Now.ToString("dd/MM/yy HH:mm:ss") + " Reference No:" + udf5 + "Request XML:" + Request.Url.OriginalString, Server.MapPath("~"));

        string IpAddress = System.Configuration.ConfigurationManager.AppSettings["KoDTicketingIPAddress"];
        if (ErrorNo == null)
        {
            result = Request["result"];
            postdate = Request["postdate"];
            tranid = Request["tranid"];
            auth = Request["auth"];
            trackid = Request["trackid"];
            refence = Request["ref"];
            amt = Request["amt"];

            if (result == "CAPTURED")
            {

                Response.Write("REDIRECT=" + IpAddress + "RoyalCard/Account/Payment/HDFC/Result.aspx?paymentId=" + paymentId + "&result=" + result + "&auth=" + auth + "&ref=" + refence + "&amt=" + amt + "&postdate=" + postdate + "&trackid=" + trackid + "&tranid=" + tranid + "&udf1=" + udf1 + "&udf2=" + udf2 + "&udf3=" + udf3 + "&udf4=" + udf4 + "&udf5=" + udf5);
            }
            else
            {
                Response.Write("REDIRECT=" + IpAddress + "RoyalCard/Account/Payment/HDFC/Error.aspx?paymentId=" + paymentId + "&ErrorText=" + result + "&auth=" + auth + "&ref=" + refence + "&amt=" + amt + "&postdate=" + postdate + "&trackid=" + trackid + "&tranid=" + tranid + "&udf1=" + udf1 + "&udf2=" + udf2 + "&udf3=" + udf3 + "&udf4=" + udf4 + "&udf5=" + udf5);
            }
        }
        else
        {
            Response.Write("REDIRECT=" + IpAddress + "RoyalCard/Account/Payment/HDFC/Error.aspx?paymentId=" + paymentId + "&ErrorText=" + ErrorText);
        }

    }

    //Function to write log file

    public static void Writefile_new(string strErrMesg, string strPath)
    {
        try
        {
            strPath = strPath + "//Log";
            string LogFilename = "Log" + "_" + DateTime.Now.ToString("yyyyMMdd");
            System.Data.DataSet Dsfile = new System.Data.DataSet();
            if (System.IO.Directory.Exists(strPath) == false)
            {
                System.IO.DirectoryInfo dirinfo = new System.IO.DirectoryInfo(strPath);
                dirinfo.Create();
                CreateNewXml(ref Dsfile);
                Dsfile.WriteXml(strPath + " \\XmlData.xml");

                System.IO.FileInfo objfile = new System.IO.FileInfo(strPath + "\\" + LogFilename + "_0.log");
                System.IO.StreamWriter Tex = objfile.CreateText();
                Tex.Write(strErrMesg + "\r\n");
                Tex.Close();
                System.GC.Collect();// 
                return;
            }

            if (System.IO.File.Exists(strPath + "\\XmlData.xml") == true)
            {
                System.Data.DataRow[] dr;
                int FileIndex = 0;
                Dsfile.ReadXml(strPath + " \\XmlData.xml");
                dr = Dsfile.Tables[0].Select("SysDate ='" + DateTime.Now.ToString("dd-MM-yyyy") + "'");
                if (dr.Length > 0)
                {
                    FileIndex = Convert.ToInt32(dr[0]["Sysvalue"].ToString());
                }
                else
                {
                    Dsfile.Clear();
                    CreateNewXml(ref Dsfile);
                    Dsfile.WriteXml(strPath + "\\XmlData.xml");
                }

                if (System.IO.File.Exists(strPath + "\\" + LogFilename + "_" + FileIndex + ".log"))
                {
                    System.IO.FileInfo objfile = new System.IO.FileInfo(strPath + "\\" + LogFilename + "_" + FileIndex.ToString() + ".log");
                    if (objfile.Length > 500000)    //523636
                    {
                        System.IO.FileInfo objfile1 = new System.IO.FileInfo(strPath + "\\" + LogFilename + "_" + (FileIndex + 1).ToString() + ".log");
                        System.IO.StreamWriter Tex = objfile1.CreateText();
                        Tex.Write(strErrMesg + "\r\n");
                        Tex.Close();
                        Dsfile.Tables[0].Rows[0]["Sysvalue"] = FileIndex + 1;
                        Dsfile.WriteXml(strPath + "\\XmlData.xml");
                        System.GC.Collect();
                    }
                    else
                    {
                        System.IO.StreamWriter Tex = objfile.AppendText();
                        Tex.Write(strErrMesg + "\r\n");
                        Tex.Close();
                        System.GC.Collect();
                    }
                }
                else
                {
                    System.IO.FileInfo objfile = new System.IO.FileInfo(strPath + "\\" + LogFilename + "_" + FileIndex.ToString() + ".log");
                    System.IO.StreamWriter Tex = objfile.CreateText();
                    Tex.Write(strErrMesg + "\r\n");
                    Tex.Close();
                    System.GC.Collect();
                }
            }
            else
            {
                CreateNewXml(ref Dsfile);
                Dsfile.WriteXml(strPath + "\\XmlData.xml");
                System.IO.FileInfo objfile = new System.IO.FileInfo(strPath + LogFilename + "_0.log");
                System.IO.StreamWriter Tex = objfile.CreateText();
                Tex.Write(strErrMesg + "\r\n");
                Tex.Close();
                System.GC.Collect();
            }
        }
        catch (Exception ex)
        {
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write(ex.Message);
        }
    }
    //Function to create xml file - Used for log file writing
    public static void CreateNewXml(ref System.Data.DataSet ds)
    {
        System.Data.DataTable Dt = new System.Data.DataTable();
        System.Data.DataRow dr;
        Dt.Columns.Add("SysDate", System.Type.GetType("System.String"));
        Dt.Columns.Add("Sysvalue", System.Type.GetType("System.Int32"));
        dr = Dt.NewRow();
        dr["SysDate"] = DateTime.Now.ToString("dd-MM-yyyy");
        dr["Sysvalue"] = 0;
        Dt.Rows.Add(dr);
        ds.Tables.Add(Dt);
    }


}
