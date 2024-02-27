using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace IBSLFormsApp.Model.Libraries
{
    internal class ApiPostPolicy
    {

        public async Task<string> apiPostPolicy(string token, string url, JObject content)
        {
            dynamic data = "";

            // send POST request with RestSharp https://ibsl-uat-api.oic.or.th //

            RestClient client = new RestClient("https://ibsl-uat-api.oic.or.th/api/policies");

            //RestClient client = new RestClient("https://ibsl-uat-api.oic.or.th/");
            //var request = new RestRequest("/api/policies", Method.Post);

            var request = new RestRequest();
            request.Method = Method.Post;

            string strJsonContent = JsonConvert.SerializeObject(content);

            string strJsonClBank = strJsonContent.Replace(" ", "");

            //string parsedJson = JsonConvert.DeserializeObject(strJsonClBank);

            Console.WriteLine(strJsonClBank);

            request.AddHeader("Content-Type", "application/json");
            //request.AddHeader("Accept", "application/json");
            request.AddHeader("Authorization", "Bearer " + token);
            //request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(strJsonClBank);
            //request.AddHeader("My-Custom-Header", "foobar");
            //request.AddBody(parsedJson);
            //request.AddStringBody(parsedJson, DataFormat.Json);

            try
            {

                RestResponse response = await client.ExecuteAsync(request);
                HttpStatusCode statusCode = response.StatusCode;

                Console.WriteLine("res: " + statusCode);
                // deserialize json string response to JsonNode object
                //data = System.Text.Json.JsonSerializer.Deserialize<JsonNode>(response.Content!)!;
            }
            catch (Exception ex)
            {
            }

            return null;
        }


    }


}
