using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace IBSLFormsApp.Model.Libraries
{
    internal class ReqestTokenApi
    {
        public async Task<string> requesttokenapi(string servername, string usr, string pass)
        {
            string urlPostRequest = "";

            if (servername == "uat")
            {
                urlPostRequest = "https://ibsl-uat-api.oic.or.th/connect/token";
            }
            else
            {
                urlPostRequest = "https://ibsl-api.oic.or.th/connect/token";
            }

            /*************** Log Test ***************/
            DebugLog debl = new DebugLog();
            string logTest = "RequestToken: "+ urlPostRequest;
            debl.debuglog(logTest);

            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, urlPostRequest);
            request.Headers.Add("Cookie", "incap_ses_8025_2720202=iWcJZ03wzG1nzDW5dodeb3uEq2QAAAAASRh6i0TbBs9EyO+LzdfdjA==; incap_ses_8025_2750569=kKpbFb0nthDsbje5dodeb0SHq2QAAAAAmHVuVPHxKmCFGoIkqbmOmQ==; visid_incap_2720202=dcupCjQxQEChFqhLxBPLipLHp2QAAAAAQUIPAAAAAADKFCgMimdUg0nkLhWgAoWW; visid_incap_2750569=Apb7zRdKRv6HqfrVt8GthHLMimQAAAAAQUIPAAAAAAAGtwau9ifTMucHnmdc3+lP");
            var collection = new List<KeyValuePair<string, string>>();
            collection.Add(new KeyValuePair<string, string>("username", usr));
            collection.Add(new KeyValuePair<string, string>("password", pass));
            collection.Add(new KeyValuePair<string, string>("grant_type", "password"));
            collection.Add(new KeyValuePair<string, string>("client_id", "4"));
            var content = new FormUrlEncodedContent(collection);
            request.Content = content;
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            //Console.WriteLine(await response.Content.ReadAsStringAsync());
            return await response.Content.ReadAsStringAsync();
        }
    }
}
