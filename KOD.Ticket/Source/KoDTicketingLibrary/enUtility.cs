using System;
using System.Text;
using System.Security.Cryptography;

namespace KoDTicketing.Utilities
{
    public class enUtility
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string DoHash(string text)
        {
            string myString = text;
            byte[] Data = null;
            Data = Encoding.ASCII.GetBytes(myString);
            SHA1Managed shaM = new SHA1Managed();
            byte[] resultHash = shaM.ComputeHash(Data);
            string resultHexString = "";
            byte b = 0;
            foreach (byte b_loopVariable in resultHash)
            {
                b = b_loopVariable;
                //resultHexString += Conversion.Hex(b);
                resultHexString += b.ToString("X");
            }
            string hashValue = null;
            hashValue = Convert.ToBase64String(resultHash);
            return hashValue;

        }
    }
}
