    /*********************************************************************
* Kingdom Of Dreams
*****************************************************************

Name of the Program : KoD Ticketing - Handle Response from HDFC Payment Gateway 
Page Description    : Receives response from Payment Gateway and handles the same
Response parameters : Result,Ref,Transaction id, Payment id,Auth Code, Track ID,
                  Amount, UDF1-UDF5
Values from Session : No
Values to Session   : No
Created by          : FSS Payment Gateway Team
Created On          : 19-04-2011
Version             : Version 1.0

***************************************************************** 
**/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Payment_HDFC_GetHandleHDFCResponse : System.Web.UI.Page
{
    public static string HDFCTransPortalID = System.Configuration.ConfigurationManager.AppSettings["HDFCTransPortalID"].ToString();
    public static string HDFCTranportalPwd = System.Configuration.ConfigurationManager.AppSettings["HDFCTranportalPwd"].ToString();
    public static string KoDTicketingIPAddress = System.Configuration.ConfigurationManager.AppSettings["KoDTicketingIPAddress"].ToString();

/*Variable Declaration*/
    string strResponceIP, TranInqResponse, ResPaymentId, ResResult, ResErrorText, ResPosdate, ResTranId, ResAuth, ResAmount, ResErrorNo, ResTrackID, ResRef, Resudf1, Resudf2, Resudf3, Resudf4, Resudf5;

    public void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("GetHandleHDFCResponse");

            /* Capture the IP Address from where the response has been received */
            strResponceIP = HttpContext.Current.Request.UserHostAddress;

            /* Check whether the IP Address from where response is received is PG IP */
            if (strResponceIP != "221.134.101.174" && strResponceIP != "221.134.101.169")
            {
                //IF ip address recevied is not Payment Gateway IP Address
                /*
                IMPORTAN NOTE - IF IP ADDRESS MISMATCHES, ME LOGS DETAILS IN LOGS,
                UPDATES MERCHANT DATABASE WITH PAYMENT FAILURE, REDIRECTS CUSTOMER 
                ON FAILURE PAGE WITH RESPECTIVE MESSAGE
                */
                /*
                to get the IP Address in case of proxy server used
                 string strIpAddress = Request.ServerVariables("HTTP_X_FORWARDED_FOR");
                */
                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("GetHandleHDFCResponse Result: ip address recevied is not Payment Gateway IP Address");
                string redirectURL = KoDTicketingIPAddress + "Payment/HDFC/FailedTRAN.aspx?Message=";
                Response.Write("REDIRECT=" + redirectURL + "--IP MISSMATCH-- Response IP Address is: " + strResponceIP);
            }
            else
            {

                //====================================================================================================================================	
                ResErrorText = Request["ErrorText"];		//Error Text/message
                ResPaymentId = Request["paymentid"];	//Payment Id
                ResTrackID = Request["trackid"];		//Merchant Track ID
                ResErrorNo = Request["Error"];			//Error Number

                //To collect transaction result
                ResResult = Request["result"];			//Transaction Result
                ResPosdate = Request["postdate"];		//Postdate
                //To collect Payment Gateway Transaction ID, this value will be used in dual verification request
                ResTranId = Request["tranid"];			//Transaction ID
                ResAuth = Request["auth"];				//Auth Code					
                ResRef = Request["ref"];				//Reference Number also called Seq Number
                //To collect amount from response
                ResAmount = Request["amt"];				//Transaction Amount
 
                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("GetHandleHDFCResponse Result: " + ResResult);

                Resudf1 = Request["udf1"];				//UDF1
                Resudf2 = Request["udf2"];				//UDF2
                Resudf3 = Request["udf3"];				//UDF3
                Resudf4 = Request["udf4"];				//UDF4
                Resudf5 = Request["udf5"];				//UDF5
                //LIST OF PARAMETERS RECEIVED BY MERCHANT FROM PAYMENT GATEWAY ENDS HERE 
                //====================================================================================================================================	
 ;

                /* Merchant (ME) checks, if error number is NOT present, then create Dual Verification 
                request, send to Paymnent Gateway. ME SHOULD ONLY USE PAYMENT GATEWAY TRAN ID FOR DUAL
                VERIFICATION */
                /* NOTE - MERCHANT MUST LOG THE RESPONSE RECEIVED IN LOGS AS PER BEST PRACTICE */
                if (ResErrorNo == null)
                {
                    //check result is captured or approved i.e. successful
                    if (ResResult == "CAPTURED" || ResResult == "APPROVED")//If resulr is CAPTURED or APPROVED then below Code is execute for dual inquiry 
                    {
                        //result is successful, hence create dual verification request
                        Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("GetHandleHDFCResponse Payment Successful ");

                        //ID given by bank to Merchant (Tranportal ID), same iD that was passed in initial request
                        //string ReqTranportalId = "<id>" + HDFCTransPortalID + "</id>";
                        string ReqTranportalId = "<id>90001457</id>";

                        //Password given by bank to merchant (Tranportal Password), same password that was passed in initial request
                        //string ReqTranportalPassword = "<password>" + HDFCTranportalPwd + "</password>";
                        string ReqTranportalPassword = "<password>password1</password>";

                        // Pass DUAL VERIFICATION action code, always pass "8" for DUAL VERIFICATION
                        string INQAction = "<action>8</action>";

                        //Pass PG Transaction ID for Dual Verification
                        string INQTransId = "<transid>" + ResTranId + "</transid>";

                        //create string for request of input parameters
                        string INQRequest = ReqTranportalId + ReqTranportalPassword + INQAction + INQTransId;

                        //DUAL VERIFIACTION URL, this is test environment URL, contact bank for production DUAL Verification URL
                        Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("GetHDFCResponse: DUAL VERIFICATION");
                        string INQUrl = "https://securepgtest.fssnet.co.in/pgway/servlet/TranPortalXMLServlet";

                        try
                        {
                            //create a SSL connection xmlhttp formated object server-to-server
                            System.IO.StreamWriter myWriter = null;
                            // it will open a http connection with provided url
                            System.Net.HttpWebRequest objRequest = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(INQUrl);//send data using objxmlhttp object
                            objRequest.Method = "POST";
                            objRequest.ContentLength = INQRequest.Length;
                            objRequest.ContentType = "application/x-www-form-urlencoded";//to set content type
                            myWriter = new System.IO.StreamWriter(objRequest.GetRequestStream());
                            myWriter.Write(INQRequest);//send data
                            myWriter.Close();//closed the myWriter object

                            System.Net.HttpWebResponse objResponse = (System.Net.HttpWebResponse)objRequest.GetResponse();
                            //receive the responce from objxmlhttp object 
                            using (System.IO.StreamReader sr = new System.IO.StreamReader(objResponse.GetResponseStream()))
                            {
                                TranInqResponse = sr.ReadToEnd();

                                string[] INQCheck = GetStringInBetween(TranInqResponse, "<result>", "</result>", false, false);//This line will check if any error in TranInqResponse 		
                                if (INQCheck[0] == "CAPTURED" || INQCheck[0] == "APPROVED")
                                {
                                    //XML response received for DUAL VERIFICATION.
                                    /* 
                                    NOTE - MERCHANT MUST LOG THE RESPONSE RECEIVED IN LOGS AS PER BEST PRACTICE
                                    */
                                    Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("GetHandleHDFCResponse Payment Successful ");

                                    //Collect DUAL VERIFICATION RESULT
                                    string[] INQResResult = GetStringInBetween(TranInqResponse, "<result>", "</result>", false, false);//It will give DualInquiry Result 
                                    string[] INQResAmount = GetStringInBetween(TranInqResponse, "<amt>", "</amt>", false, false);//It will give Amount
                                    string[] INQResTrackId = GetStringInBetween(TranInqResponse, "<trackid>", "</trackid>", false, false);//It will give TrackID ENROLLED

                                    //MERCHANT CAN GET ALL VERIFICATION RESULT PARAMETERS USING BELOW CODE 
                                    
                                    string[] INQResAutht=GetStringInBetween(TranInqResponse,"<auth>","</auth>",false,false);//It will give Auth 
                                    string[] INQResRef=GetStringInBetween(TranInqResponse,"<ref>","</ref>",false,false);//It will give Ref.NO.
                                    string[] INQResAvr=GetStringInBetween(TranInqResponse,"<avr>","</avr>",false,false);//It will give AVR 
                                    string[] INQResPostdate=GetStringInBetween(TranInqResponse,"<postdate>","</postdate>",false,false);//It will give  postdate
                                    string[] INQResTranid=GetStringInBetween(TranInqResponse,"<tranid>","</tranid>",false,false);//It will give tranid
                                    string[] INQResPayid=GetStringInBetween(TranInqResponse,"<payid>","</payid>",false,false);//It will give payid
                                    string[] INQResUdf1=GetStringInBetween(TranInqResponse,"<udf1>","</udf1>",false,false);//It will give udf1
                                    string[] INQResUdf2=GetStringInBetween(TranInqResponse,"<udf2>","</udf2>",false,false);//It will give udf2
                                    string[] INQResUdf3=GetStringInBetween(TranInqResponse,"<udf3>","</udf3>",false,false);//It will give udf3
                                    string[] INQResUdf4=GetStringInBetween(TranInqResponse,"<udf4>","</udf4>",false,false);//It will give udf4
                                    string[] INQResUdf5=GetStringInBetween(TranInqResponse,"<udf5>","</udf5>",false,false);//It will give udf5
                                    
                                    /*
                                    IMPORTANT NOTE - MERCHANT DOES RESPONSE HANDLING AND VALIDATIONS OF 
                                    TRACK ID, AMOUNT AT THIS PLACE. THEN ONLY MERCHANT SHOULD UPDATE 
                                    TRANACTION PAYMENT STATUS IN MERCHANT DATABASE AT THIS POSITION 
                                    AND THEN REDIRECT CUSTOMER ON RESULT PAGE
                                    */

                                    /* !!IMPORTANT INFORMATION!!
                                    During redirection, ME can pass the values as per ME requirement.
                                    NOTE: NO PROCESSING should be done on the RESULT PAGE basis of values passed in the RESULT PAGE from this page. 
                                    ME does all validations on the responseURL page and then redirects the customer to RESULT 
                                    PAGE ONLY FOR RECEIPT PRESENTATION/TRANSACTION STATUS CONFIRMATION
                                    For demonstration purpose the result and track id are passed to Result page
                                    */
                                    Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("GetHandleHDFCResponse Showing Status...");

                                    string redirectURL = KoDTicketingIPAddress + "Payment/Print-Receipt.aspx";
                                    Response.Write("REDIRECT=" + redirectURL  + "Transaction Success [" + INQResResult[0] + "]&ResTrackId=" + INQResTrackId[0] + "&ResAmount=" + INQResAmount[0]);
                                }
                                else
                                {
                                    /*
                                    ERROR IN TRANSACTION PROCESSING
                                    IMPORTANT NOTE - MERCHANT SHOULD UPDATE 
                                    TRANACTION PAYMENT STATUS IN MERCHANT DATABASE AT THIS POSITION 
                                    AND THEN REDIRECT CUSTOMER ON RESULT PAGE
                                    */
                                    string redirectURL = KoDTicketingIPAddress + "Payment/HDFC/FailedTRAN.aspx?Message=";
                                    Response.Write("REDIRECT=" + redirectURL + "Transaction Failed&ResTrackId=" + ResTrackID + "&ResAmount=" + ResAmount + "&ResError=" + INQCheck[0]);
                                }
                            }
                        }
                        catch (Exception Ex)
                        {
                            Response.Write(Ex.Message);// any excpetion occurred for above code exception throws here
                        }
                    }
                    else
                    {
                        /*
                         IMPORTANT NOTE - MERCHANT SHOULD UPDATE 
                         TRANACTION PAYMENT STATUS IN MERCHANT DATABASE AT THIS POSITION 
                         AND THEN REDIRECT CUSTOMER ON RESULT PAGE
                         */
                        string redirectURL = KoDTicketingIPAddress + "Payment/HDFC/StatusTRAN.aspx?ResResult="; 
                        Response.Write("REDIRECT=" + redirectURL + "=Transaction Failed [" + ResResult + "]&ResTrackId=" + ResTrackID + "&ResAmount=" + ResAmount);

                    }
                }
                else
                {
                    /*
                    ERROR IN TRANSACTION PROCESSING
                    IMPORTANT NOTE - MERCHANT SHOULD UPDATE 
                    TRANACTION PAYMENT STATUS IN MERCHANT DATABASE AT THIS POSITION 
                    AND THEN REDIRECT CUSTOMER ON RESULT PAGE
                     */
                    string redirectURL = KoDTicketingIPAddress + "Payment/HDFC/FailedTRAN.aspx?Message=";
                    Response.Write("REDIRECT=" + redirectURL + "Transaction Failed&ResTrackId=" + ResTrackID + "&ResAmount=" + ResAmount + "&ResError=" + ResErrorText);
                }
            }
        }
        catch (Exception Ex)
        {
            Response.Write(Ex.Message);// any excpetion occurred for above code exception throws here
        }

    }//page load event ends here

    //GetStringInBetween function to find string between two string/two tag
    public static string[] GetStringInBetween(string strSource, string strBegin, string strEnd, bool includeBegin, bool includeEnd)
    {
        string[] result = { "", "" };
        int iIndexOfBegin = strSource.IndexOf(strBegin);
        if (iIndexOfBegin != -1)
        {
            // include the Begin string if desired

            if (includeBegin)
            {
                iIndexOfBegin -= strBegin.Length;
            }
            strSource = strSource.Substring(iIndexOfBegin + strBegin.Length);
            int iEnd = strSource.IndexOf(strEnd);
            if (iEnd != -1)
            {  // include the End string if desired
                if (includeEnd)
                { iEnd += strEnd.Length; }
                result[0] = strSource.Substring(0, iEnd);
                // advance beyond this segment
                if (iEnd + strEnd.Length < strSource.Length)
                { result[1] = strSource.Substring(iEnd + strEnd.Length); }
            }
        }
        else
        // stay where we are
        { result[1] = strSource; }
        return result;
    }//String function end
}