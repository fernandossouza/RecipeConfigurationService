using System;
using System.Collections;
using System.Web;
using System.Threading.Tasks;
using recipeconfigurationservice.Model;
using recipeconfigurationservice.ETLClass.Interface;
using System.Collections.Generic;

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
            var url = await ConstructUrl(objExtract.jsonExtractDynamic);
            
            // Comunicaçao com a api externa
            string jsonRetornApiString = await _httpOtherApi.RestCommunication(_apiConfiguration.method,url);

            if( jsonRetornApiString == null)
            {
                // adicionar Erro Pendencia
            }

            // Criação do dicionario com os valores da extração
            var dicExtractApi = await ComposeDictionaryOfExtract(jsonRetornApiString,objExtract.dicExtract);

            return true;
        }
        public override async Task<bool> Load(Dictionary<string,string> dicExtract)
        {
            // Pendencia
            return true;
        }

        private async Task<string> ConstructUrl(dynamic jsonExtract)
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