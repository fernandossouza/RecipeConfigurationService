using System;
using System.Collections;
using System.Web;
using System.Threading.Tasks;
using recipeconfigurationservice.Model;
using recipeconfigurationservice.ETLClass.Interface;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft;
using Newtonsoft.Json;

namespace recipeconfigurationservice.ETLClass
{
    public class TransformApi : TransformFactory
    {
        private ApiConfiguration _apiConfiguration;

        private readonly IHttpOtherApi _httpOtherApi;

        private readonly ApiLoad _apiLoad;

        private readonly IJson _json;

        public TransformApi (ApiConfiguration apiConfiguration,ApiLoad apiLoad, IHttpOtherApi httpOtherApi, IJson json)
        {
            _apiConfiguration = apiConfiguration;
            _httpOtherApi = httpOtherApi;
            _apiLoad = apiLoad;
            _json = json;

        }
        public override async Task<bool> Extract(ObjExtract objExtract)
        {
            //Obtém a url
            var url = await ConstructUrlExtract(objExtract.jsonExtractDynamic);
            
            // Comunicaçao com a api externa
            string jsonRetornApiString = await _httpOtherApi.RestCommunication(_apiConfiguration.method,url);

           

            // Criação do dicionario com os valores da extração
            var dicExtractApi = await ComposeDictionaryOfExtract(jsonRetornApiString,objExtract.dicExtract);

            return true;
        }
        public override async Task<string> Load(Dictionary<string,string> dicExtract)
        {
            // converte para json
            var jsonExtract = JsonConvert.SerializeObject(dicExtract);
             //Obtém a url
            var url = await ConstructUrlLoad(jsonExtract);

            var jsonSend = await ConstructJsonDynamic(jsonExtract);
            
            // Comunicaçao com a api externa
            string returnApi = await _httpOtherApi.RestCommunication(_apiLoad.method,url,jsonSend);

            // Pendencia
            return returnApi;
        }

        private async Task<string> ConstructUrlExtract(dynamic jsonExtract)
        {
            UriBuilder url = new UriBuilder(_apiConfiguration.endPoint);
            var query = HttpUtility.ParseQueryString(url.Query);
            foreach(var input in _apiConfiguration.input)
            {
                var value =  await GetValueParameter(input.type,input.path,input.value,jsonExtract);
                query[input.nameParameter] = value;
            }
            url.Query = query.ToString();

            return url.ToString();
        }

        private async Task<string> ConstructUrlLoad(dynamic json)
        {
            UriBuilder url = new UriBuilder(_apiLoad.endPoint);
            var query = HttpUtility.ParseQueryString(url.Query);
            foreach(var parameter in _apiLoad.parameterLoad.Where(x=> !string.IsNullOrEmpty(x.queryParameter)))
            {
                var value =  await GetValueParameter(parameter.type,parameter.localName,parameter.value,json);
                query[parameter.queryParameter] = value;
            }
            url.Query = query.ToString();

            return url.ToString();
        }

        private async Task<string> ConstructJsonDynamic(dynamic json)
        {
            Dictionary<string,object> dicJson = new Dictionary<string, object>();
            foreach(var parameter in _apiLoad.parameterLoad.Where(x=> !string.IsNullOrEmpty(x.jsonName)))
            {
                var value =  await GetValueParameter(parameter.type,parameter.localName,parameter.value,json);
                dicJson.Add(parameter.jsonName,GetTypeData(value,parameter.typeData));
            }

            return JsonConvert.SerializeObject(dicJson);
        }

        private object GetTypeData(string value, ETypeData typeData)
        {
            if(string.IsNullOrEmpty(value))
                return null;

            switch(typeData)
            {
                case ETypeData.Bool:
                return Convert.ToBoolean(value);

                case ETypeData.String:
                return value;

                case ETypeData.Integer:
                return Convert.ToInt64(value);
            }

            return null;
        }

        private async Task<IDictionary<string,string>> ComposeDictionaryOfExtract(string jsonString
        , Dictionary<string,string> dicExtract)
        {           
            foreach(var output in _apiConfiguration.output)
            {
                var value = await _json.GetValuePath(jsonString,output.path);

                dicExtract.Add(output.localName,value);
            }
            return dicExtract;
        }

    }
}