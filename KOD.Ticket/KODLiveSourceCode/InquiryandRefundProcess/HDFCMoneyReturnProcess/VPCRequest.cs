using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.Services.Description;
using System.Web.Util;
using System.Web.Services.Protocols;
using System.Web.Services.Configuration;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Net;
using System.Text;
using System.IO;

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
    public class VPCRequest
    {
        Uri _address;
        SortedList<String, String> _requestFields = new SortedList<String, String>(new VPCStringComparer());
        String _rawResponse;
        SortedList<String, String> _responseFields = new SortedList<String, String>(new VPCStringComparer());
        String _secureSecret;
        String _proxyhost;
        String _proxyuser;
        String _proxypassword;
        String _proxydomain;

        public static string Version
        {
            get
            {
                return "SHA256 1.0";
                //return "3.0.0";
            }
        }

        public void SetProxyHost (String URI)
        {
            _proxyhost = URI;
        }

        public void SetProxyUser(String Username)
        {
            _proxyuser = Username;
        }

        public void SetProxyPassword(String Password)
        {
            _proxypassword = Password;
        }

        public void SetProxyDomain(String Domain)
        {
            _proxydomain = Domain;
        }

        public VPCRequest(String URL)
        {
            _address = new Uri(URL);
        }

        public void SetSecureSecret(String secret)
        {
            _secureSecret = secret;
        }

        public void AddDigitalOrderField(String key, String value)
        {
            _requestFields.Add(key, value);
        }

        public String GetResultField(String key, String defValue)
        {
            String value;
            if (_responseFields.TryGetValue(key, out value))
            {
                return value;
            }
            else
            {
                return defValue;
            }
        }

        public String GetResultField(String key)
        {
            return GetResultField(key, "");
        }

        public String FormatDate(String month, String year)
        {
            // We have the expiry month and year, but the Payment Client requires
            // a 4 digit expiry year/month (YYMM). We construct this from the
            // information we already have.

            if (year != null && year.Length > 2)
            {
                year = year.Substring(year.Length - 2);
            }
            else if (year != null && year != "" && year.Length < 2)
            {
                year = year.PadLeft(2, '0');
            }

            if (month != null && month.Length > 2)
            {
                month = month.Substring(month.Length - 2);
            }
            else if (month != null && month != "" && month.Length < 2)
            {
                month = month.PadLeft(2, '0');
            }

            return year + month;
        }

        private String GetRequestRaw()
        {
            StringBuilder data = new StringBuilder();
            foreach (KeyValuePair<string, string> kvp in _requestFields)
            {
                if (!String.IsNullOrEmpty(kvp.Value))
                {
                    data.Append(kvp.Key + "=" + HttpUtility.UrlEncode(kvp.Value) + "&");
                }
            }
            data.Remove(data.Length - 1, 1); //remove trailing & from string
            return data.ToString();
        }

        public String GetResponseRaw()
        {
            if (!String.IsNullOrEmpty(_rawResponse))
            {
                return _rawResponse;
            }
            else
            {
                return "";
            }
        }

        //_____________________________________________________________________________________________________
        // Two-Party order transaction processing

        public void SendRequest()
        {
            // Setup proxy if needed
            if (!String.IsNullOrEmpty(_proxyhost))
            {
                WebProxy proxy = new WebProxy(_proxyhost, true);
                if (!String.IsNullOrEmpty(_proxyuser))
                {
                    if (String.IsNullOrEmpty(_proxypassword))
                    {
                        _proxypassword = "";
                    }

                    if (String.IsNullOrEmpty(_proxydomain))
                    {
                        proxy.Credentials = new NetworkCredential(_proxyuser, _proxypassword);
                    }
                    else
                    {
                        proxy.Credentials = new NetworkCredential(_proxyuser, _proxypassword, _proxydomain);
                    }
                }
                WebRequest.DefaultWebProxy = proxy;

            }
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Hit to Amex Gateway");
            // Create the web request  
            HttpWebRequest request = WebRequest.Create(_address) as HttpWebRequest;

            // Set type to POST  
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";

            // If vpc_SecureSecret defined, use secure hashing as best practice
            if (!String.IsNullOrEmpty(_secureSecret))
            {
                _requestFields.Add("vpc_SecureHash", CreateSHA256Signature(true));
                _requestFields.Add("vpc_SecureHashType", "SHA256");
            }

            // Create a byte array of the data we want to send  
            byte[] byteData = UTF8Encoding.UTF8.GetBytes(GetRequestRaw());

            // Set the content length in the request headers  
            request.ContentLength = byteData.Length;

            // Write data  
            using (Stream postStream = request.GetRequestStream())
            {
                postStream.Write(byteData, 0, byteData.Length);
            }

            // Get response  
            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                // Get the response stream  
                StreamReader reader = new StreamReader(response.GetResponseStream());

                // Console application output
                _rawResponse = reader.ReadToEnd();
                String[] responses = _rawResponse.Split('&');
                foreach (String responseField in responses)
                {
                    String[] field = responseField.Split('=');
                    _responseFields.Add(field[0], HttpUtility.UrlDecode(field[1]));
                }
            }

            // If vpc_SecureSecret defined, there needs to be a hash returned since we hashed
            // on VPC submission
            if (!String.IsNullOrEmpty(_secureSecret))
            {
                try
                {
                    // Retrieve and remove hash info from results
                    string secureHash = _responseFields["vpc_SecureHash"];
                    string secureHashType = _responseFields["vpc_SecureHashType"];
                    _responseFields.Remove("vpc_SecureHash");
                    _responseFields.Remove("vpc_SecureHashType");

                    // Check if hash returned correctly
                    if (String.IsNullOrEmpty(secureHash))
                    {
                        Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Secure Hash not returned from VPC");
                        throw new Exception("Secure Hash not returned from VPC");
                    }
                    else if (!secureHash.Equals(CreateSHA256Signature(false)))
                    {
                        Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Secure Hash returned from VPC does not match");
                        throw new Exception("Secure Hash returned from VPC does not match");
                    }
                }
                catch (Exception ex)
                {
                    Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("Exception while hit to AMEX Gateway : " + ex);
                }
            }
        }

        public string GetTxnResponseCode()
        {
            return GetResultField("vpc_TxnResponseCode");
        }

        //_____________________________________________________________________________________________________
        // Three-Party order transaction processing

        public String Create3PartyQueryString()
        {
            return _address + "?" + GetRequestRaw() + "&vpc_SecureHash=" + CreateSHA256Signature(true) + "&vpc_SecureHashType=SHA256";
        }

        public void Process3PartyResponse(System.Collections.Specialized.NameValueCollection nameValueCollection)
        {
            foreach (string item in nameValueCollection)
            {
                if (!item.Equals("vpc_SecureHash") && !item.Equals("vpc_SecureHashType"))
                {
                    _responseFields.Add(item, nameValueCollection[item]);
                }

            }

            if (String.IsNullOrEmpty(nameValueCollection["vpc_SecureHash"]))
            {
                throw new Exception("No Secure Hash included in response");
            }
            if (!CreateSHA256Signature(false).Equals(nameValueCollection["vpc_SecureHash"]))
            {
                throw new Exception("Secure Hash does not match");
            }

        }

        //_____________________________________________________________________________________________________

        private class VPCStringComparer : IComparer<String>
        {
            /*
             <summary>Customised Compare Class</summary>
             <remarks>
             <para>
             The Virtual Payment Client need to use an Ordinal comparison to Sort on 
             the field names to create the SHA256 Signature for validation of the message. 
             This class provides a Compare method that is used to allow the sorted list 
             to be ordered using an Ordinal comparison.
             </para>
             </remarks>
             */

            public int Compare(String a, String b)
            {
                /*
                 <summary>Compare method using Ordinal comparison</summary>
                 <param name="a">The first string in the comparison.</param>
                 <param name="b">The second string in the comparison.</param>
                 <returns>An int containing the result of the comparison.</returns>
                 */

                // Return if we are comparing the same object or one of the 
                // objects is null, since we don't need to go any further.
                if (a == b) return 0;
                if (a == null) return -1;
                if (b == null) return 1;

                // Ensure we have string to compare
                string sa = a as string;
                string sb = b as string;

                // Get the CompareInfo object to use for comparing
                System.Globalization.CompareInfo myComparer = System.Globalization.CompareInfo.GetCompareInfo("en-US");
                if (sa != null && sb != null)
                {
                    // Compare using an Ordinal Comparison.
                    return myComparer.Compare(sa, sb, System.Globalization.CompareOptions.Ordinal);
                }
                throw new ArgumentException("a and b should be strings.");
            }
        }

        //______________________________________________________________________________
        // SHA256 Hash Code

        private string CreateSHA256Signature(bool useRequest)
        {
            // Hex Decode the Secure Secret for use in using the HMACSHA256 hasher
            // hex decoding eliminates this source of error as it is independent of the character encoding
            // hex decoding is precise in converting to a byte array and is the preferred form for representing binary values as hex strings. 
            byte[] convertedHash = new byte[_secureSecret.Length / 2];
            for (int i = 0; i < _secureSecret.Length / 2; i++)
            {
                convertedHash[i] = (byte)Int32.Parse(_secureSecret.Substring(i * 2, 2), System.Globalization.NumberStyles.HexNumber);
            }

            // Build string from collection in preperation to be hashed
            StringBuilder sb = new StringBuilder();
            SortedList<String, String> list = (useRequest ? _requestFields : _responseFields);
            foreach (KeyValuePair<string, string> kvp in list)
            {
                if (!String.IsNullOrEmpty(kvp.Value))
                {
                    sb.Append(kvp.Key + "=" + kvp.Value + "&");
                }
            }
            sb.Remove(sb.Length - 1, 1);

            // Create secureHash on string
            string hexHash = "";
            using (HMACSHA256 hasher = new HMACSHA256(convertedHash))
            {
                byte[] hashValue = hasher.ComputeHash(Encoding.UTF8.GetBytes(sb.ToString()));
                foreach (byte b in hashValue)
                {
                    hexHash += b.ToString("X2");
                }
            }
            return hexHash;
        }
    }
}