using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/*
Copyright Statement
Copyright © 2008 Transaction Network Services Inc ("TNS").  All rights reserved.

Disclaimer
This document is provided by TNS on the basis that you will treat it as confidential. No part of this document may be reproduced or copied in any form by any means without the written permission of TNS. Unless otherwise expressly agreed in writing, the information contained in this document is subject to change without notice and TNS assumes no responsibility for any alteration to, or any error or other deficiency, in this document. 
All intellectual property rights in the Document and the TNS products and service referred to therein (“TNS IPR”) in addition to all extracts and things derived from any part of the Document are owned by TNS and will be assigned to TNS on their creation. You will protect all TNS IPR in a manner that is equal to the protection that you provide to your own intellectual property.  You will notify TNS immediately in writing where you become aware of a breach of TNS IPR.
The names “TNS”, any product names of TNS including “Dialect” and “QSI Payments” and all similar words are trademarks of TNS and you must not use that name or any similar name.
TNS may at its sole discretion terminate the rights granted in this document with immediate effect by notifying you in writing and you will thereupon return (or destroy and certify that destruction to TNS) all copies and extracts of the Document in its possession or control.
TNS does not warrant the accuracy or completeness of the Document or its content or its usefulness to you or your merchant cardholders. To the maximum extent permitted by law, all conditions and warranties implied by law (including without limitation as to fitness for any particular purpose) are excluded.  Where the exclusion is not effective, TNS limits its liability to £100 or the resupply of the Document (at TNS's option). Data used in examples and sample data files are intended to be fictional and any resemblance to real persons or companies is entirely coincidental.
TNS does not indemnify you or any third party in relation to the content of this document or any use of such content. 
Any mention of any product not owned by TNS does not constitute an endorsement of that product.
This document is governed by the laws of England and Wales and is intended to be legally binding.
 */

namespace _TNS
{
    public static class PaymentCodesHelper
    {
        public static string Version
        {
            get
            {
                return "SHA256 1.0";
            }
        }

        public static string GetTxnResponseCodeDescription(string TxnResponseCode)
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
                    case "?": result = "Response Unknown"; break;
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
                    case "B": result = "Transaction Blocked - The Verification Security Level of the 3-D Secure transaction is insufficient to allow processing to continue."; break;
                    case "C": result = "Transaction Cancelled"; break;
                    case "D": result = "Deferred transaction has been received and is awaiting processing"; break;
                    case "E": result = "Issuer Returned a Referral Response"; break;
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

        public static string GetCSCDescription(string CSCResultCode)
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

        public static string GetAVSDescription(string AVSResultCode)
        {
            string result = "";
            if (AVSResultCode != null && !AVSResultCode.Equals(""))
            {
                if (String.Compare(AVSResultCode, "Unsupported", true) == 0)
                {
                    result = "AVS not supported or there was no AVS data provided";
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

        /**
        * This function uses the Verification Status Code returned by in
        * the Authentication data of a transaction or Authentication request.
        *
        * @param VerStatus string containing the VerStatus Result Code
        * @return description string containing the appropriate description
        */

        public static string GetVerStatusDescription(string VerStatus)
        {
            string result = "";
            if (VerStatus != null && !VerStatus.Equals(""))
            {
                if (String.Compare(VerStatus, "Unsupported", true) == 0)
                {
                    result = "Authentication not supported or there was no Authentication data provided";
                }
                else
                {
                    switch (VerStatus)
                    {
                        case "Y": result = "Cardholder was successfully authenticated."; break;
                        case "E": result = "Cardholder is not enrolled."; break;
                        case "N": result = "Cardholder was not verified."; break;
                        case "U": result = "Issuer system error."; break;
                        case "F": result = "Data supplied in the request was invalid."; break;
                        case "A": result = "Authentication of the merchant credentials to the Directory Server failed."; break;
                        case "D": result = "Error communicating with the Directory Server."; break;
                        case "C": result = "Card type is not supported for authentication."; break;
                        case "S": result = "Response received from the Issuer was invalid."; break;
                        case "P": result = "Error parsing input from Issuer."; break;
                        case "I": result = "Internal Payment Server system error."; break;
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
