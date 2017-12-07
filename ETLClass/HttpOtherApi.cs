using System.Net.Http;
using recipeconfigurationservice.ETLClass.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;

namespace recipeconfigurationservice.ETLClass
{
    public class HttpOtherApi  : IHttpOtherApi
    {
        private readonly HttpClient _client = new HttpClient();

        public Task<string> RestCommunication(string method, string url, dynamic json = null)
        {
            switch (method.ToLower())
            {
                case "get":
                return GetApi(url);

                case "post":
                return PostApi(url,json);


            }

            return null;
        }

        private async Task<string> GetApi(string url)
        {
            var responseString = await _client.GetStringAsync(url);

            dynamic jsonDynamic = responseString;

            return jsonDynamic;

        }

        private async Task<string> PostApi(string url,dynamic json)
        {
            var responseString = await _client.PostAsync(url, new StringContent(json, Encoding.UTF8, "application/json"));

           

            return responseString.StatusCode.ToString();

        }
    }
}