using System.Net.Http;
using recipeconfigurationservice.ETLClass.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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

            }

            return null;
        }

        private async Task<string> GetApi(string url)
        {
            var responseString = await _client.GetStringAsync(url);

            dynamic jsonDynamic = responseString;

            return jsonDynamic;

        }
    }
}