using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;
using System.Collections.Specialized;

namespace Drossey.Services.WizIQ
{
    /// <summary>
    /// Summary description for WiZiQRequest
    /// </summary>
    public class HttpRequest
    {
        public enum Method { GET, POST };

        public HttpRequest()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        /// <summary>
        /// Submit a web request to WiZiQ REST API.
        /// </summary>
        /// <param name="method">GET or POST</param>
        /// <param name="endpointUrl">The method url</param>
        /// <param name="requestParameters">Data to post (in NameValue format)</param>
        /// <returns>The web server response.</returns>
        /// 
        public string WiZiQWebRequest(string endpointUrl, Dictionary<string, string> requestParameters)
        {
            string returnData = "";
            string postData = "";

            if (requestParameters.Count > 0)
            {
                foreach (var item in requestParameters)
                {
                    if (postData.Length > 0)
                        postData += "&";
                    postData += item.Key + "=" + WebUtility.UrlEncode(item.Value);
                }
            }

            Method method = Method.POST;
            returnData = WebRequest(method, endpointUrl, postData);

            return returnData;
        }

        /// <summary>
        /// Web Request Wrapper
        /// </summary>
        /// <param name="method">Http Method</param>
        /// <param name="url">Full url to the web resource</param>
        /// <param name="postData">Data to post in querystring format</param>
        /// <returns>The web server response.</returns>
        public string WebRequest(Method method, string url, string postData)
        {
            HttpWebRequest webRequest = null;
            StreamWriter requestWriter = null;
            string responseData = "";
            webRequest = System.Net.WebRequest.Create(url) as HttpWebRequest;
            webRequest.Method = method.ToString();
            //webRequest.ServicePoint.Expect100Continue = false;
            webRequest.ContentType = "application/x-www-form-urlencoded";
            //POST the data.
            requestWriter = new StreamWriter(webRequest.GetRequestStream());
            try
            {
                requestWriter.Write(postData);
            }
            catch
            {
                throw;
            }
            finally
            {
                requestWriter.Close();
                requestWriter = null;
            }
            responseData = GetWebResponse(webRequest);
            webRequest = null;
            return responseData;
        }

        /// <summary>
        /// Process the web response.
        /// </summary>
        /// <param name="webRequest">The request object.</param>
        /// <returns>The response data.</returns>
        private string GetWebResponse(HttpWebRequest webRequest)
        {
            StreamReader responseReader = null;
            string responseData = "";
            try
            {
                responseReader = new StreamReader(webRequest.GetResponse().GetResponseStream());
                responseData = responseReader.ReadToEnd();
            }
            catch
            {
                throw;
            }
            finally
            {
                //webRequest.GetResponse().GetResponseStream().Close();
                //responseReader.Close();
                //responseReader = null;
            }
            return responseData;
        }
        
        public string WiZiQWebRequest(string endpointUrl, NameValueCollection requestParameters, string postFilePath)
        {
            string response = string.Empty;
            if (string.IsNullOrEmpty(postFilePath))
            { 
                string postData = "";
                var dict = new Dictionary<string, string>();
                foreach (string key in requestParameters)
                    {
                            dict.Add(key, requestParameters[key]);
                    }
                if (dict.Count > 0)
                {  
                    foreach (var item in dict)
                    {
                        if (postData.Length > 0)
                            postData += "&";
                        postData += item.Key + "=" + WebUtility.UrlEncode(item.Value);
                    }
                }
                Method method = Method.POST;
                response = WebRequest(method, endpointUrl, postData);
            }
            //else
            //{               
            //    UploadFile file = new UploadFile(postFilePath);              
            //    HttpWebRequest req = System.Net.WebRequest.Create(endpointUrl) as HttpWebRequest;
            //    HttpWebResponse resp = HttpUploadHelper.Upload(req, file, requestParameters);

            //    using (Stream s = resp.GetResponseStream())
            //    using (StreamReader sr = new StreamReader(s))
            //    {
            //        response = sr.ReadToEnd();
            //    }                
            //}
            return response;
            
        }
    }
}
