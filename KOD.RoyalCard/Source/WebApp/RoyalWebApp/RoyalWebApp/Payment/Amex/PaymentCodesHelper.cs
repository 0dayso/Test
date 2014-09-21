using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace _Dialect
{
    public static class PaymentCodesHelper
    {
        public static string Version
        {
            get
            {
                return "3.0.0";
            }
        }

        public static string getTxnResponseCodeDescription(string TxnResponseCode)
        {

            string result = "";

            if (TxnResponseCode == null || String.Compare(TxnResponseCode, "null", true) == 0 || TxnResponseCode.Equals(""))
            {
                result = "null response";
            }
            else
            {

                switch (TxnResponseCode)
                {
                    case "0": result = "Transaction Successful"; break;
                    case "1": result = "Transaction Declined"; break;
                    case "2": result = "Bank Declined Transaction"; break;
                    case "3": result = "No Reply from Bank"; break;
                    case "4": result = "Expired Card"; break;
                    case "5": result = "Insufficient Funds"; break;
                    case "6": result = "Error Communicating with Bank"; break;
                    case "7": result = "Payment Server detected an error"; break;
                    case "8": result = "Transaction Type Not Supported"; break;
                    case "9": result = "Bank declined transaction (Do not contact Bank)"; break;
                    case "A": result = "Transaction Aborted"; break;
                    case "B": result = "Transaction Declined - Contact the Bank"; break;
                    case "C": result = "Transaction Cancelled"; break;
                    case "D": result = "Deferred transaction has been received and is awaiting processing"; break;
                    case "F": result = "3-D Secure Authentication failed"; break;
                    case "I": result = "Card Security Code verification failed"; break;
                    case "L": result = "Shopping Transaction Locked (Please try the transaction again later)"; break;
                    case "N": result = "Cardholder is not enrolled in Authentication scheme"; break;
                    case "P": result = "Transaction has been received by the Payment Adaptor and is being processed"; break;
                    case "R": result = "Transaction was not processed - Reached limit of retry attempts allowed"; break;
                    case "S": result = "Duplicate SessionID"; break;
                    case "T": result = "Address Verification Failed"; break;
                    case "U": result = "Card Security Code Failed"; break;
                    case "V": result = "Address Verification and Card Security Code Failed"; break;
                    default: result = "Unable to be determined"; break;
                }
            }
            return result;
        }


        // _________________________________________________________________________


        /**
        * This function uses the CSC Result Code retrieved from the Digital
        * Receipt and returns an appropriate description for this code.
        *
        * @param vCSCResultCode string containing the CSC Result Code
        * @return description string containing the appropriate description
        */ 
        public static string getCSCDescription(string CSCResultCode)
        {

            string result = "";
            if (CSCResultCode != null && !CSCResultCode.Equals(""))
            {

                if (String.Compare(CSCResultCode, "Unsupported", true) == 0)
                {
                    result = "CSC not supported or there was no CSC data provided";
                }
                else
                {
                    switch (CSCResultCode)
                    {
                        case "M": result = "Exact code match"; break;
                        case "S": result = "Merchant has indicated that CSC is not present on the card (MOTO situation)"; break;
                        case "P": result = "Code not processed"; break;
                        case "U": result = "Card issuer is not registered and/or certified"; break;
                        case "N": result = "Code invalid or not matched"; break;
                        default: result = "Unable to be determined"; break;
                    }
                }
            }
            else
            {
                result = "null response";
            }
            return result;
        }


        // _________________________________________________________________________


        /**
        * This function uses the AVS Result Code retrieved from the Digital
        * Receipt and returns an appropriate description for this code.
        *
        * @param vAVSResultCode string containing the CSC Result Code
        * @return description string containing the appropriate description
        */
        public static string getAVSDescription(string AVSResultCode)
        {

            string result = "";
            if (AVSResultCode != null && !AVSResultCode.Equals(""))
            {
                if (String.Compare(AVSResultCode, "Unsupported", true) == 0)
                {
                    result = "CSC not supported or there was no CSC data provided";
                }
                else
                {
                    switch (AVSResultCode)
                    {
                        case "X": result = "Exact match - address and 9 digit ZIP/postal code"; break;
                        case "Y": result = "Exact match - address and 5 digit ZIP/postal code"; break;
                        case "S": result = "Service not supported or address not verified (international transaction)"; break;
                        case "G": result = "Issuer does not participate in AVS (international transaction)"; break;
                        case "A": result = "Address match only"; break;
                        case "W": result = "9 digit ZIP/postal code matched, Address not Matched"; break;
                        case "Z": result = "5 digit ZIP/postal code matched, Address not Matched"; break;
                        case "R": result = "Issuer system is unavailable"; break;
                        case "U": result = "Address unavailable or not verified"; break;
                        case "E": result = "Address and ZIP/postal code not provided"; break;
                        case "N": result = "Address and ZIP/postal code not matched"; break;
                        case "0": result = "AVS not requested"; break;
                        default: result = "Unable to be determined"; break;
                    }
                }
            }
            else
            {
                result = "null response";
            }
            return result;
        }
    }
}