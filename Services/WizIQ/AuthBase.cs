using System;
using System.Collections.Generic;
using System.Web;
using System.Text;
using System.Security.Cryptography;

namespace Drossey.Services.WizIQ
{
    /// <summary>
    /// Summary description for AuthBase
    /// </summary>
    public class AuthBase
    {

        public struct RequestObject
        {
            public string AccessKeyID;
            public string TimeStamp;
            public string ObjectType;//(content | test | class)
            public string MethodName;
        }
        protected string unreservedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_.~";


        //public string GenerateSignature(string secretAccessKey,string userName,string password)
        public string GenerateSignature(string secretAccessKey, RequestObject requestObject)
        {

            //string signatureBase = GenerateSignatureBase(url, consumerKey, token, tokenSecret, callBackUrl, oauthVerifier, httpMethod, timeStamp, nonce, HMACSHA1SignatureType, out normalizedUrl, out normalizedRequestParameters);
            string signatureBase = "access_key=" + requestObject.AccessKeyID + "&timestamp=" + requestObject.TimeStamp + "&method=" + requestObject.ObjectType;
            HMACSHA1 hmacsha1 = new HMACSHA1();
            hmacsha1.Key = Encoding.ASCII.GetBytes(string.Format("{0}", UrlEncode(secretAccessKey)));

            return GenerateSignatureUsingHash(signatureBase, hmacsha1);

        }
        public string GenerateSignature(string secretAccessKey, Dictionary<string, string> requestParameters)
        {
            string signatureBase = string.Empty;

            foreach (var item in requestParameters)
            {
                if (signatureBase.Length>0)
                    signatureBase += "&";
                 signatureBase+=item.Key+"="+item.Value;
            }
            //string signatureBase = GenerateSignatureBase(url, consumerKey, token, tokenSecret, callBackUrl, oauthVerifier, httpMethod, timeStamp, nonce, HMACSHA1SignatureType, out normalizedUrl, out normalizedRequestParameters);
            //string signatureBase = "AccessKeyID=" + requestObject.AccessKeyID + "&TimeStamp=" + requestObject.TimeStamp + "&ObjectType=" + requestObject.ObjectType;
            HMACSHA1 hmacsha1 = new HMACSHA1();
            hmacsha1.Key = Encoding.ASCII.GetBytes(string.Format("{0}", UrlEncode(secretAccessKey)));

            return GenerateSignatureUsingHash(signatureBase, hmacsha1);

        }

        public string UrlEncode(string value)
        {
            StringBuilder result = new StringBuilder();

            foreach (char symbol in value)
            {
                if (unreservedChars.IndexOf(symbol) != -1)
                {
                    result.Append(symbol);
                }
                else
                {
                    result.Append('%' + String.Format("{0:X2}", (int)symbol));
                }
            }

            return result.ToString();
        }
        public string GenerateSignature(string accessKeyID, string secretAccessKey, string timeStamp, string MethodName)
        {

            //string signatureBase = GenerateSignatureBase(url, consumerKey, token, tokenSecret, callBackUrl, oauthVerifier, httpMethod, timeStamp, nonce, HMACSHA1SignatureType, out normalizedUrl, out normalizedRequestParameters);
            string signatureBase = "access_key=" + accessKeyID + "&timestamp=" + timeStamp + "&method=" + MethodName;
            HMACSHA1 hmacsha1 = new HMACSHA1();
            hmacsha1.Key = Encoding.ASCII.GetBytes(string.Format("{0}", UrlEncode(secretAccessKey)));

            return GenerateSignatureUsingHash(signatureBase, hmacsha1);

        }

        /// <summary>
        /// Generate the signature value based on the given signature base and hash algorithm
        /// </summary>
        /// <param name="signatureBase">The signature based as produced by the GenerateSignatureBase method or by any other means</param>
        /// <param name="hash">The hash algorithm used to perform the hashing. If the hashing algorithm requires initialization or a key it should be set prior to calling this method</param>
        /// <returns>A base64 string of the hash value</returns>
        public string GenerateSignatureUsingHash(string signatureBase, HashAlgorithm hash)
        {
            return ComputeHash(hash, signatureBase);
        }

        /// <summary>
        /// Helper function to compute a hash value
        /// </summary>
        /// <param name="hashAlgorithm">The hashing algoirhtm used. If that algorithm needs some initialization, like HMAC and its derivatives, they should be initialized prior to passing it to this function</param>
        /// <param name="data">The data to hash</param>
        /// <returns>a Base64 string of the hash value</returns>
        private string ComputeHash(HashAlgorithm hashAlgorithm, string data)
        {
            if (hashAlgorithm == null)
            {
                throw new ArgumentNullException("hashAlgorithm");
            }

            if (string.IsNullOrEmpty(data))
            {
                throw new ArgumentNullException("data");
            }

            byte[] dataBuffer = System.Text.Encoding.ASCII.GetBytes(data);
            byte[] hashBytes = hashAlgorithm.ComputeHash(dataBuffer);

            return Convert.ToBase64String(hashBytes);
        }


        public static void AssignNewKey()
        {
            const int PROVIDER_RSA_FULL = 1;
            const string CONTAINER_NAME = "KeyContainer";
            CspParameters cspParams;

            cspParams = new CspParameters(PROVIDER_RSA_FULL);
            cspParams.KeyContainerName = CONTAINER_NAME;
            cspParams.Flags = CspProviderFlags.UseMachineKeyStore;
            cspParams.ProviderName = "Microsoft Strong Cryptographic Provider";
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(cspParams);

            rsa.PersistKeyInCsp = false;

            string publicPrivateKeyXML = rsa.ToXmlString(true);
            string publicOnlyKeyXML = rsa.ToXmlString(false);
            // do stuff with keys...
        }
        public static string GenerateTimeStamp()
        {

           var date= UnixTimestampFromDateTime(DateTime.Now);


            // Default implementation of UNIX time of the current UTC time
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }

        public static long UnixTimestampFromDateTime(DateTime date)
        {
            long unixTimestamp = date.Ticks - new DateTime(1970, 1, 1).Ticks;
            unixTimestamp /= TimeSpan.TicksPerSecond;
            return unixTimestamp;
        }


    }//end class
}
