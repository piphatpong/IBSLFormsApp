using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace IBSLFormsApp.Model.Libraries
{
    public class PostPolAsync
    {
        /// <summary>
        /// Post for a REST api
        /// </summary>
        /// 
        /*-- <param name="token">authorization token</param>
        /// <param name="url">REST url for the resource</param>
        /// <param name="content">content</param>
        /// <returns>response from the rest url</returns> --*/
        ///\\\\
        public async Task<JObject> postPolAsync(string token, string url, string content)
        {
            string responseContent = "";

            byte[] data = Encoding.UTF8.GetBytes(content);
            WebRequest request = WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/json";
            request.Headers.Add("Authorization", "Bearer " + token);
            request.ContentLength = data.Length;

            using (Stream stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            try
            {
                WebResponse response = await request.GetResponseAsync();
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    responseContent = reader.ReadToEnd();
                    JObject adResponse =
                        Newtonsoft.Json.JsonConvert.DeserializeObject<JObject>(responseContent);
                    Console.WriteLine("response 1:" + adResponse.ToString());
                    return adResponse;
                }
            }
            catch (WebException webException)
            {
                if (webException.Response != null)
                {
                    using (StreamReader reader = new StreamReader(webException.Response.GetResponseStream()))
                    {
                        responseContent = reader.ReadToEnd();
                        Console.WriteLine("response 2:" + responseContent.ToString());
                        return Newtonsoft.Json.JsonConvert.DeserializeObject<JObject>(responseContent);

                    }
                }
            }

            return (JObject)responseContent.ToString();
        }

    }
}
