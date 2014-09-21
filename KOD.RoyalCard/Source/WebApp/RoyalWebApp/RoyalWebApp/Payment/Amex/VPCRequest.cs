using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.IO;

namespace _Dialect
{
    public class VPCRequest
    {
        Uri _address;
        SortedList<String, String> _requestFields = new SortedList<String, String>(new VPCStringComparer());
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
                return "3.0.0";
            }
        }

        public void setProxyHost (String URI)
        {
            _proxyhost = URI;
        }

        public void setProxyUser(String Username)
        {
            _proxyuser = Username;
        }

        public void setProxyPassword(String Password)
        {
            _proxypassword = Password;
        }

        public void setProxyDomain(String Domain)
        {
            _proxydomain = Domain;
        }

        public VPCRequest(String URL)
        {
            _address = new Uri(URL);
        }

        public void setSecureSecret(String secret)
        {
            _secureSecret = secret;
        }

        public void addDigitialOrderField(String key, String value)
        {
            _requestFields.Add(key, value);
        }

        public String getResultField(String key, String defValue)
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

        public String getResultField(String key)
        {
            return getResultField(key, "");
        }

        public String formatExpiryDate(String month, String year)
        {
            // We have the expiry month and year, but the Payment Client requires
            // a 4 digit expiry year/month (YYMM). We construct this from the
            // information we already have.

            if (year != null && year.Length > 2)
            {
                year = year.Substring(year.Length - 2);
            }
            else if (year != null && year.Length < 2)
            {
                year = year.PadLeft(2, '0');
            }

            if (month != null && month.Length > 2)
            {
                month = month.Substring(month.Length - 2);
            }
            else if (month != null && month.Length < 2)
            {
                month = month.PadLeft(2, '0');
            }

            return year + month;
        }

        public String getRequestRaw()
        {
            StringBuilder data = new StringBuilder();
            foreach (KeyValuePair<string, string> kvp in _requestFields)
            {
                if (!String.IsNullOrEmpty(kvp.Value))
                {
                    if (data.Length > 0)
                    {
                        data.Append("&");
                    }

                    data.Append(kvp.Key + "=" + HttpUtility.UrlEncode(kvp.Value));
                }
            }
            return data.ToString();
        }

        public void sendRequest()
        {

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

            // Create the web request  
            HttpWebRequest request = WebRequest.Create(_address) as HttpWebRequest;

            // Set type to POST  
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";

            // Create a byte array of the data we want to send  
            byte[] byteData = UTF8Encoding.UTF8.GetBytes(getRequestRaw());

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
                String[] responses = reader.ReadToEnd().Split('&');
                foreach (String responseField in responses)
                {
                    String[] field = responseField.Split('=');
                    _responseFields.Add(field[0], HttpUtility.UrlDecode(field[1]));
                }

            }
        }

        public string getTxnResponseCode()
        {
            return getResultField("vpc_TxnResponseCode");
        }

        private class VPCStringComparer : IComparer<String>
        {
            /*
             <summary>Customised Compare Class</summary>
             <remarks>
             <para>
             The Virtual Payment Client need to use an Ordinal comparison to Sort on 
             the field names to create the MD5 Signature for validation of the message. 
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

        private string CreateMD5Signature(bool useRequest)
        {
            /*
             <summary>Creates a MD5 Signature</summary>
             <param name="RawData">The string used to create the MD5 signautre.</param>
             <returns>A string containing the MD5 signature.</returns>
             */

            StringBuilder sb = new StringBuilder();

            sb.Append(_secureSecret);
            SortedList<String, String> list = (useRequest ? _requestFields : _responseFields);
            foreach (KeyValuePair<string, string> kvp in list)
            {
                if (!String.IsNullOrEmpty(kvp.Value))
                {
                    sb.Append(kvp.Value);
                }
            }

            System.Security.Cryptography.MD5 hasher = System.Security.Cryptography.MD5CryptoServiceProvider.Create();
            byte[] HashValue = hasher.ComputeHash(Encoding.ASCII.GetBytes(sb.ToString()));

            string strHex = "";
            foreach (byte b in HashValue)
            {
                strHex += b.ToString("x2");
            }
            return strHex.ToUpper();
        }

        public String Create3PartyQueryString()
        {
            return _address + "?" + getRequestRaw() + "&vpc_SecureHash=" + CreateMD5Signature(true);
        }

        public void process3PartyResponse(System.Collections.Specialized.NameValueCollection nameValueCollection)
        {
            foreach (string item in nameValueCollection)
            {
                if (!item.Equals("vpc_SecureHash"))
                {
                    _responseFields.Add(item, nameValueCollection[item]);
                }

            }

            if (!CreateMD5Signature(false).Equals(nameValueCollection["vpc_SecureHash"]))
            {
                //throw new Exception("Secure Hash does not match");
                throw new Exception("Amount,TransactionID and Show Name Should not Blank ");

            }

        }
    }
}
