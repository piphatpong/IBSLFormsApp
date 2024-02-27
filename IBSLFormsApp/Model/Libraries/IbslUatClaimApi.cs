using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Threading.Tasks;

namespace IBSLFormsApp.Model.Libraries
{
    internal class IbslUatClaimApi
    {
        public async Task<string> ibsuatclaimapi(string token, string url, JObject content)
        {
            /*************** Log Test ***************/
            DebugLog debl = new DebugLog();
            string logTest = "ibsl-claim-api: " + url;
            debl.debuglog(logTest);

            var options = new RestClientOptions(url)
            {
                MaxTimeout = -1,
            };
            var client = new RestClient(options);
            var request = new RestRequest("/api/claims", Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", token);

            //request.AddHeader("Cookie", "incap_ses_897_2720202=+mhrFKlCSm65hySODslyDDeKv2QAAAAAH5kBFpeMnJbzl6N0VSJ24Q==; visid_incap_2720202=auUAI8fNSlS0nYYznl6WezeKv2QAAAAAQUIPAAAAAADnI8WmJkWv5lASBKihxGX/");

            var body = JsonConvert.SerializeObject(content);
            //Console.WriteLine("token: " + token);

            request.AddStringBody(body, DataFormat.Json);
            RestResponse response = await client.ExecuteAsync(request);
            dynamic parsedJson = JsonConvert.DeserializeObject(response.Content);
            string encrypeOut = JsonConvert.SerializeObject(parsedJson, Formatting.Indented);
            //Console.WriteLine("Postman response: " + encrypeOut);

            return encrypeOut;
        }

    }
}
