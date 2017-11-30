using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using recipeconfigurationservice.ETLClass.Interface;

namespace recipeconfigurationservice.ETLClass
{
    public class Json : IJson
    {
        public async Task<string> GetValuePath(string jsonDynamic, string path)
        {
            string value = (string)JObject.Parse(jsonDynamic)[path];

            return value;
        }
    }
}